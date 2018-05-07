namespace JiraManager
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btn_getJiraData = new System.Windows.Forms.Button();
            this.tb_jiranumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_endDay = new System.Windows.Forms.TextBox();
            this.tb_startDay = new System.Windows.Forms.TextBox();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.monthCalendar2 = new System.Windows.Forms.MonthCalendar();
            this.button1 = new System.Windows.Forms.Button();
            this.tb_progress = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mi_managerquery = new System.Windows.Forms.ToolStripMenuItem();
            this.jiraPageInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.tb_sqltext = new System.Windows.Forms.TextBox();
            this.tb_result = new System.Windows.Forms.TextBox();
            this.btn_run = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.jiraPageInfoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_getJiraData
            // 
            this.btn_getJiraData.Location = new System.Drawing.Point(995, 55);
            this.btn_getJiraData.Name = "btn_getJiraData";
            this.btn_getJiraData.Size = new System.Drawing.Size(104, 23);
            this.btn_getJiraData.TabIndex = 0;
            this.btn_getJiraData.Text = "获取JIRA数据";
            this.btn_getJiraData.UseVisualStyleBackColor = true;
            this.btn_getJiraData.Visible = false;
            this.btn_getJiraData.Click += new System.EventHandler(this.btn_GetJiraDataClickAsync);
            // 
            // tb_jiranumber
            // 
            this.tb_jiranumber.Location = new System.Drawing.Point(12, 43);
            this.tb_jiranumber.Multiline = true;
            this.tb_jiranumber.Name = "tb_jiranumber";
            this.tb_jiranumber.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_jiranumber.Size = new System.Drawing.Size(540, 135);
            this.tb_jiranumber.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "JiraNumber";
            // 
            // tb_endDay
            // 
            this.tb_endDay.Location = new System.Drawing.Point(831, 197);
            this.tb_endDay.Name = "tb_endDay";
            this.tb_endDay.Size = new System.Drawing.Size(102, 21);
            this.tb_endDay.TabIndex = 29;
            this.tb_endDay.Click += new System.EventHandler(this.tb_endDay_Click);
            // 
            // tb_startDay
            // 
            this.tb_startDay.Location = new System.Drawing.Point(713, 197);
            this.tb_startDay.Name = "tb_startDay";
            this.tb_startDay.Size = new System.Drawing.Size(102, 21);
            this.tb_startDay.TabIndex = 28;
            this.tb_startDay.Click += new System.EventHandler(this.tb_startDay_Click);
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(713, 218);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 31;
            this.monthCalendar1.Visible = false;
            this.monthCalendar1.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateSelected);
            // 
            // monthCalendar2
            // 
            this.monthCalendar2.Location = new System.Drawing.Point(831, 218);
            this.monthCalendar2.Name = "monthCalendar2";
            this.monthCalendar2.TabIndex = 30;
            this.monthCalendar2.Visible = false;
            this.monthCalendar2.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar2_DateSelected);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1024, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 36;
            this.button1.Text = "清理缓存 ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tb_progress
            // 
            this.tb_progress.Location = new System.Drawing.Point(596, 43);
            this.tb_progress.Multiline = true;
            this.tb_progress.Name = "tb_progress";
            this.tb_progress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_progress.Size = new System.Drawing.Size(374, 133);
            this.tb_progress.TabIndex = 37;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1260, 25);
            this.menuStrip1.TabIndex = 38;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mi_managerquery});
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // mi_managerquery
            // 
            this.mi_managerquery.Name = "mi_managerquery";
            this.mi_managerquery.Size = new System.Drawing.Size(124, 22);
            this.mi_managerquery.Text = "管理查询";
            this.mi_managerquery.Click += new System.EventHandler(this.mi_managerquery_Click);
            // 
            // jiraPageInfoBindingSource
            // 
            this.jiraPageInfoBindingSource.DataSource = typeof(JiraManager.Entities.JiraPageInfo);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(15, 197);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(412, 20);
            this.comboBox1.TabIndex = 39;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // tb_sqltext
            // 
            this.tb_sqltext.Location = new System.Drawing.Point(15, 248);
            this.tb_sqltext.Multiline = true;
            this.tb_sqltext.Name = "tb_sqltext";
            this.tb_sqltext.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_sqltext.Size = new System.Drawing.Size(569, 232);
            this.tb_sqltext.TabIndex = 40;
            // 
            // tb_result
            // 
            this.tb_result.Location = new System.Drawing.Point(704, 234);
            this.tb_result.Multiline = true;
            this.tb_result.Name = "tb_result";
            this.tb_result.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_result.Size = new System.Drawing.Size(464, 246);
            this.tb_result.TabIndex = 41;
            // 
            // btn_run
            // 
            this.btn_run.Location = new System.Drawing.Point(607, 262);
            this.btn_run.Name = "btn_run";
            this.btn_run.Size = new System.Drawing.Size(75, 23);
            this.btn_run.TabIndex = 42;
            this.btn_run.Text = "分析";
            this.btn_run.UseVisualStyleBackColor = true;
            this.btn_run.Click += new System.EventHandler(this.btn_run_ClickAsync);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1260, 511);
            this.Controls.Add(this.btn_run);
            this.Controls.Add(this.monthCalendar1);
            this.Controls.Add(this.monthCalendar2);
            this.Controls.Add(this.tb_result);
            this.Controls.Add(this.tb_sqltext);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.tb_progress);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tb_endDay);
            this.Controls.Add(this.tb_startDay);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_jiranumber);
            this.Controls.Add(this.btn_getJiraData);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "JiraManager";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.jiraPageInfoBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_getJiraData;
        private System.Windows.Forms.TextBox tb_jiranumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_endDay;
        private System.Windows.Forms.TextBox tb_startDay;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.MonthCalendar monthCalendar2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tb_progress;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mi_managerquery;
        private System.Windows.Forms.BindingSource jiraPageInfoBindingSource;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox tb_sqltext;
        private System.Windows.Forms.TextBox tb_result;
        private System.Windows.Forms.Button btn_run;
    }
}

