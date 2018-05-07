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
    public partial class QueryManagerForm : Form
    {
        public QueryManagerForm()
        {
            InitializeComponent();
        }
        private JManager jmanager = new JManager();

        public QueryListForm parentForm;
        private void btn_save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tb_title.Text)) {
                MessageBox.Show("Title 不能为空");
            }
            this.jmanager.SaveSqlToDb(this.oldTitle,this.tb_title.Text, this.tb_sqltext.Text);
            this.oldTitle = this.tb_title.Text;
            if (this.parentForm != null) {
                this.parentForm.refleshData();
            }
            this.Close();
        }

        private void btn_quit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private string oldTitle = "";

        public void setdata(string title, string sqltext) {
            this.tb_title.Text= title;
            this.tb_sqltext.Text = sqltext;
            this.oldTitle = title;
        }
        private void QueryManagerForm_Load(object sender, EventArgs e)
        {

        }

        private void QueryManagerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.parentForm != null)
            {
                this.parentForm.refleshData();
            }
        }
    }
}
