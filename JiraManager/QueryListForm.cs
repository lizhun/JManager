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
    public partial class QueryListForm : Form
    {
        public QueryListForm()
        {
            InitializeComponent();
        }
        private JManager jmanager = new JManager();

        public MainForm parentForm;
        private void QueryListForm_Load(object sender, EventArgs e)
        {
            this.refleshData();
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            var queryform = new QueryManagerForm();
            queryform.parentForm = this;
            queryform.ShowDialog();
        }

        public void refleshData()
        {
            var datalist = jmanager.GetSqlFromDb();
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DataSource = datalist;
        }


        private void btn_quit_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void dataGridView1_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            DataTable dt = (DataTable)this.dataGridView1.DataSource;
            var rowindex = e.RowIndex;
            var queryform = new QueryManagerForm();
            queryform.parentForm = this;
            queryform.setdata(dt.Rows[rowindex][0].ToString(),
                dt.Rows[rowindex][1].ToString());
            queryform.ShowDialog();
        }

        private void QueryListForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.parentForm != null)
            {
                this.parentForm.refleshData();
            }
        }
    }
}
