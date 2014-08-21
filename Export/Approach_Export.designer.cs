namespace JH_KH_GraduateSurvey.Export
{
    partial class Approach_Export
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "身分證號"}, -1, System.Drawing.Color.Red, System.Drawing.Color.Empty, null);
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("畢業班級");
			System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("座號");
			System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("學號");
			System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem(new string[] {
            "姓名"}, -1, System.Drawing.Color.Red, System.Drawing.Color.Empty, null);
			System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("戶籍電話");
			System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("聯絡電話");
			System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("行動電話");
			System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem("其它電話1");
			System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem("其它電話2");
			System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem("其它電話3");
			System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem("監護人電話");
			System.Windows.Forms.ListViewItem listViewItem13 = new System.Windows.Forms.ListViewItem("父親電話");
			System.Windows.Forms.ListViewItem listViewItem14 = new System.Windows.Forms.ListViewItem("母親電話");
			System.Windows.Forms.ListViewItem listViewItem15 = new System.Windows.Forms.ListViewItem(new string[] {
            "填報學年度"}, -1, System.Drawing.Color.Red, System.Drawing.Color.Empty, null);
			System.Windows.Forms.ListViewItem listViewItem16 = new System.Windows.Forms.ListViewItem(new string[] {
            "升學與就業情形"}, -1, System.Drawing.Color.Red, System.Drawing.Color.Empty, null);
			System.Windows.Forms.ListViewItem listViewItem17 = new System.Windows.Forms.ListViewItem(new string[] {
            "升學：就讀學校情形"}, -1, System.Drawing.Color.Red, System.Drawing.Color.Empty, null);
			System.Windows.Forms.ListViewItem listViewItem18 = new System.Windows.Forms.ListViewItem(new string[] {
            "升學：入學方式"}, -1, System.Drawing.Color.Red, System.Drawing.Color.Empty, null);
			System.Windows.Forms.ListViewItem listViewItem19 = new System.Windows.Forms.ListViewItem(new string[] {
            "升學：學制別"}, -1, System.Drawing.Color.Red, System.Drawing.Color.Empty, null);
			System.Windows.Forms.ListViewItem listViewItem20 = new System.Windows.Forms.ListViewItem(new string[] {
            "未升學未就業：動向"}, -1, System.Drawing.Color.Red, System.Drawing.Color.Empty, null);
			System.Windows.Forms.ListViewItem listViewItem21 = new System.Windows.Forms.ListViewItem(new string[] {
            "是否需要教育部協助"}, -1, System.Drawing.Color.Red, System.Drawing.Color.Empty, null);
			System.Windows.Forms.ListViewItem listViewItem22 = new System.Windows.Forms.ListViewItem(new string[] {
            "備註"}, -1, System.Drawing.Color.Red, System.Drawing.Color.Empty, null);
			this.chkSelectAll = new DevComponents.DotNetBar.Controls.CheckBoxX();
			this.btnExport = new DevComponents.DotNetBar.ButtonX();
			this.btnExit = new DevComponents.DotNetBar.ButtonX();
			this.FieldContainer = new System.Windows.Forms.ListView();
			this.lblExplanation = new DevComponents.DotNetBar.LabelX();
			this.circularProgress = new DevComponents.DotNetBar.Controls.CircularProgress();
			this.nudSchoolYear = new System.Windows.Forms.NumericUpDown();
			this.lblSchoolYear = new DevComponents.DotNetBar.LabelX();
			this.radioAllStudentInOneFile = new System.Windows.Forms.RadioButton();
			this.radioOneClassInOneFile = new System.Windows.Forms.RadioButton();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.nudSchoolYear)).BeginInit();
			this.SuspendLayout();
			// 
			// chkSelectAll
			// 
			this.chkSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.chkSelectAll.AutoSize = true;
			this.chkSelectAll.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.chkSelectAll.BackgroundStyle.Class = "";
			this.chkSelectAll.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.chkSelectAll.Checked = true;
			this.chkSelectAll.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkSelectAll.CheckValue = "Y";
			this.chkSelectAll.Location = new System.Drawing.Point(136, 15);
			this.chkSelectAll.Name = "chkSelectAll";
			this.chkSelectAll.Size = new System.Drawing.Size(54, 21);
			this.chkSelectAll.TabIndex = 3;
			this.chkSelectAll.Text = "全選";
			this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
			// 
			// btnExport
			// 
			this.btnExport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnExport.AutoSize = true;
			this.btnExport.BackColor = System.Drawing.Color.Transparent;
			this.btnExport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnExport.Location = new System.Drawing.Point(298, 356);
			this.btnExport.Name = "btnExport";
			this.btnExport.Size = new System.Drawing.Size(75, 25);
			this.btnExport.TabIndex = 4;
			this.btnExport.Text = "匯出";
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			// 
			// btnExit
			// 
			this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnExit.AutoSize = true;
			this.btnExit.BackColor = System.Drawing.Color.Transparent;
			this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnExit.Location = new System.Drawing.Point(383, 356);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(75, 25);
			this.btnExit.TabIndex = 5;
			this.btnExit.Text = "關閉";
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// FieldContainer
			// 
			this.FieldContainer.CheckBoxes = true;
			this.FieldContainer.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.FieldContainer.HideSelection = false;
			listViewItem1.StateImageIndex = 0;
			listViewItem2.StateImageIndex = 0;
			listViewItem3.StateImageIndex = 0;
			listViewItem4.StateImageIndex = 0;
			listViewItem5.StateImageIndex = 0;
			listViewItem6.StateImageIndex = 0;
			listViewItem7.StateImageIndex = 0;
			listViewItem8.StateImageIndex = 0;
			listViewItem9.StateImageIndex = 0;
			listViewItem10.StateImageIndex = 0;
			listViewItem11.StateImageIndex = 0;
			listViewItem12.StateImageIndex = 0;
			listViewItem13.StateImageIndex = 0;
			listViewItem14.StateImageIndex = 0;
			listViewItem15.StateImageIndex = 0;
			listViewItem16.StateImageIndex = 0;
			listViewItem17.StateImageIndex = 0;
			listViewItem18.StateImageIndex = 0;
			listViewItem19.StateImageIndex = 0;
			listViewItem20.StateImageIndex = 0;
			listViewItem21.StateImageIndex = 0;
			listViewItem22.StateImageIndex = 0;
			this.FieldContainer.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10,
            listViewItem11,
            listViewItem12,
            listViewItem13,
            listViewItem14,
            listViewItem15,
            listViewItem16,
            listViewItem17,
            listViewItem18,
            listViewItem19,
            listViewItem20,
            listViewItem21,
            listViewItem22});
			this.FieldContainer.Location = new System.Drawing.Point(34, 51);
			this.FieldContainer.Name = "FieldContainer";
			this.FieldContainer.Size = new System.Drawing.Size(424, 260);
			this.FieldContainer.TabIndex = 19;
			this.FieldContainer.UseCompatibleStateImageBehavior = false;
			this.FieldContainer.View = System.Windows.Forms.View.List;
			this.FieldContainer.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.FieldContainer_ItemChecked);
			// 
			// lblExplanation
			// 
			this.lblExplanation.AutoSize = true;
			this.lblExplanation.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.lblExplanation.BackgroundStyle.Class = "";
			this.lblExplanation.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.lblExplanation.Location = new System.Drawing.Point(34, 15);
			this.lblExplanation.Name = "lblExplanation";
			this.lblExplanation.Size = new System.Drawing.Size(101, 21);
			this.lblExplanation.TabIndex = 20;
			this.lblExplanation.Text = "請選擇匯出欄位";
			// 
			// circularProgress
			// 
			this.circularProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.circularProgress.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.circularProgress.BackgroundStyle.Class = "";
			this.circularProgress.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.circularProgress.Location = new System.Drawing.Point(249, 358);
			this.circularProgress.Name = "circularProgress";
			this.circularProgress.Size = new System.Drawing.Size(43, 23);
			this.circularProgress.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
			this.circularProgress.TabIndex = 33;
			this.circularProgress.Visible = false;
			// 
			// nudSchoolYear
			// 
			this.nudSchoolYear.Location = new System.Drawing.Point(334, 13);
			this.nudSchoolYear.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
			this.nudSchoolYear.Name = "nudSchoolYear";
			this.nudSchoolYear.Size = new System.Drawing.Size(66, 25);
			this.nudSchoolYear.TabIndex = 35;
			this.nudSchoolYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// lblSchoolYear
			// 
			this.lblSchoolYear.AutoSize = true;
			this.lblSchoolYear.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.lblSchoolYear.BackgroundStyle.Class = "";
			this.lblSchoolYear.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.lblSchoolYear.Location = new System.Drawing.Point(254, 16);
			this.lblSchoolYear.Name = "lblSchoolYear";
			this.lblSchoolYear.Size = new System.Drawing.Size(74, 21);
			this.lblSchoolYear.TabIndex = 34;
			this.lblSchoolYear.Text = "填報學年度";
			// 
			// radioAllStudentInOneFile
			// 
			this.radioAllStudentInOneFile.AutoSize = true;
			this.radioAllStudentInOneFile.BackColor = System.Drawing.Color.Transparent;
			this.radioAllStudentInOneFile.Location = new System.Drawing.Point(138, 323);
			this.radioAllStudentInOneFile.Name = "radioAllStudentInOneFile";
			this.radioAllStudentInOneFile.Size = new System.Drawing.Size(130, 21);
			this.radioAllStudentInOneFile.TabIndex = 1;
			this.radioAllStudentInOneFile.Tag = "2";
			this.radioAllStudentInOneFile.Text = "所有學生一個檔案";
			this.radioAllStudentInOneFile.UseVisualStyleBackColor = false;
			this.radioAllStudentInOneFile.Click += new System.EventHandler(this.radioAllStudentInOneFile_Click);
			// 
			// radioOneClassInOneFile
			// 
			this.radioOneClassInOneFile.AutoSize = true;
			this.radioOneClassInOneFile.BackColor = System.Drawing.Color.Transparent;
			this.radioOneClassInOneFile.Location = new System.Drawing.Point(283, 323);
			this.radioOneClassInOneFile.Name = "radioOneClassInOneFile";
			this.radioOneClassInOneFile.Size = new System.Drawing.Size(130, 21);
			this.radioOneClassInOneFile.TabIndex = 0;
			this.radioOneClassInOneFile.Tag = "1";
			this.radioOneClassInOneFile.Text = "一個班級一個檔案";
			this.radioOneClassInOneFile.UseVisualStyleBackColor = false;
			this.radioOneClassInOneFile.Click += new System.EventHandler(this.radioOneClassInOneFile_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Location = new System.Drawing.Point(34, 325);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(86, 17);
			this.label1.TabIndex = 36;
			this.label1.Text = "儲存檔案方式";
			// 
			// Approach_Export
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CaptionAntiAlias = false;
			this.ClientSize = new System.Drawing.Size(492, 393);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.radioAllStudentInOneFile);
			this.Controls.Add(this.radioOneClassInOneFile);
			this.Controls.Add(this.nudSchoolYear);
			this.Controls.Add(this.lblSchoolYear);
			this.Controls.Add(this.circularProgress);
			this.Controls.Add(this.lblExplanation);
			this.Controls.Add(this.FieldContainer);
			this.Controls.Add(this.btnExport);
			this.Controls.Add(this.btnExit);
			this.Controls.Add(this.chkSelectAll);
			this.DoubleBuffered = true;
			this.Name = "Approach_Export";
			this.Text = "";
			this.TitleText = "匯出資料至 Excel";
			((System.ComponentModel.ISupportInitialize)(this.nudSchoolYear)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        public DevComponents.DotNetBar.ButtonX btnExport;
        public DevComponents.DotNetBar.ButtonX btnExit;
        public DevComponents.DotNetBar.Controls.CheckBoxX chkSelectAll;
        public DevComponents.DotNetBar.LabelX lblExplanation;
        public System.Windows.Forms.ListView FieldContainer;
        private DevComponents.DotNetBar.Controls.CircularProgress circularProgress;
        public System.Windows.Forms.NumericUpDown nudSchoolYear;
        public DevComponents.DotNetBar.LabelX lblSchoolYear;
        private System.Windows.Forms.RadioButton radioAllStudentInOneFile;
        private System.Windows.Forms.RadioButton radioOneClassInOneFile;
        private System.Windows.Forms.Label label1;
    }
}