using JiraManager.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JiraManager
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.jmanager.OnAfterGetWebData += delegate (object a, EventArgs b)
            {
                this.tb_progress.AppendText(a.ToString() + "\r\n");
            };
        }

        private JManager jmanager = new JManager();
        private async void btn_GetJiraDataClickAsync(object sender, EventArgs e)
        {
            this.btn_getJiraData.Enabled = false;
            this.tb_progress.Text = "";
            this.tb_progress.AppendText("数据获取开始\r\n");
            string jiranumbers = string.Join(",", this.tb_jiranumber.Lines);
            string str = jiranumbers.Replace("\r", "").Replace("\n", "")
                .Replace("\t", "").Replace(" ", "")
                .Replace("，", ",").Replace(",,", ",")
                .Trim(",".ToCharArray());
            var jstr = str.Split(",".ToCharArray());
            if (jstr.Length == 0)
            {
                MessageBox.Show("JiraNumber 不能为空。");
                return;
            }
            var jiraInfoList = await jmanager.GetJiraDataList(jstr);
            this.tb_progress.AppendText("数据获取完成");
            this.btn_getJiraData.Enabled = true;
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            this.tb_startDay.Text = e.Start.ToString("yyyy-MM-dd");
            this.monthCalendar1.Visible = false;
        }

        private void monthCalendar2_DateSelected(object sender, DateRangeEventArgs e)
        {
            this.tb_endDay.Text = e.Start.ToString("yyyy-MM-dd");
            this.monthCalendar2.Visible = false;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.jmanager.initDataBase();
            this.jmanager.ClearDataInDb();
            this.refleshData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.jmanager.ClearDataInDb();
            MessageBox.Show("清理完毕");
        }

        private void mi_managerquery_Click(object sender, EventArgs e)
        {

            var form = new QueryListForm();
            form.parentForm = this;
            form.ShowDialog();
        }
        public void refleshData()
        {
            var datalist = jmanager.GetSqlFromDb();
            this.comboBox1.DataSource = datalist;
            comboBox1.ValueMember = "SqlText";
            comboBox1.DisplayMember = "Title";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.tb_sqltext.Text = this.comboBox1.SelectedValue?.ToString();
        }

        private void tb_startDay_Click(object sender, EventArgs e)
        {
            this.monthCalendar1.Show();
        }

        private void tb_endDay_Click(object sender, EventArgs e)
        {
            this.monthCalendar2.Show();
        }

        private async void btn_run_ClickAsync(object sender, EventArgs e)
        {
            this.btn_run.Enabled = false;
            try
            {
                this.tb_result.Text = "";
                this.tb_progress.Text = "";
                var startTime = string.IsNullOrEmpty(this.tb_startDay.Text) ? DateTime.Now : DateTime.Parse(this.tb_startDay.Text);
                var endTime = string.IsNullOrEmpty(this.tb_endDay.Text) ? DateTime.Now : DateTime.Parse(this.tb_endDay.Text);
                endTime = endTime.Date.AddDays(1);
                string jiranumbers = string.Join(",", this.tb_jiranumber.Lines);
                string str = jiranumbers.Replace("\r", "").Replace("\n", "")
                    .Replace("\t", "").Replace(" ", "")
                    .Replace("，", ",").Replace(",,", ",")
                    .Trim(",".ToCharArray());
                var jarr = str.Split(",".ToCharArray());
                if (jarr.Length == 0)
                {
                    MessageBox.Show("JiraNumber 不能为空。");
                    return;
                }
                this.tb_progress.AppendText("数据获取开始\r\n");
                await jmanager.GetJiraDataList(jarr);
                this.tb_progress.AppendText("数据获取完成");
                var datalist = this.jmanager.GetSqlResultFromDb(startTime, endTime, this.tb_sqltext.Text, jarr);
                if (datalist != null && datalist.Rows.Count > 0)
                {
                    for (var i = 0; i < datalist.Rows.Count; i++)
                    {
                        this.tb_result.AppendText(datalist.Rows[i][0]?.ToString() +", "+
                            "\r\n");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.btn_run.Enabled = true;
            }
        }
    }
}
