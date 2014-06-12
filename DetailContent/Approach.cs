using Campus.Windows;
using FISCA.Data;
using FISCA.Permission;
using FISCA.UDT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace JH_KH_GraduateSurvey.DetailContent
{
    [FeatureCode("ischool.jh_kh.detail_content.graduate_survey_approach", "畢業學生進路")]
    public partial class Approach_DetailContent : FISCA.Presentation.DetailContent
    {
        //  驗證資料物件
        private ErrorProvider _Errors;

        //  背景載入 UDT 資料物件
        private BackgroundWorker _BGWLoadData;
        private BackgroundWorker _BGWSaveData;

        //  監控 UI 資料變更
        private ChangeListener _Listener;

        //  正在下載的資料之主鍵，用於檢查是否下載他人資料，若 _RunningKey != PrimaryKey 就再下載乙次
        private string _RunningKey;
        private string _CurrentSchoolYear;

        private AccessHelper Access;
        private QueryHelper Query;
        private bool form_loaded;        

        public Approach_DetailContent()
        {
            InitializeComponent();

            this.Group = "畢業學生進路";
            _RunningKey = "";

            this.Load += new EventHandler(Form_Load);
            this.form_loaded = false;
            _Errors = new ErrorProvider();
            _Listener = new ChangeListener();
            _Listener.Add(new DataGridViewSource(this.dgvData));
            _Listener.Add(new TextBoxSource(this.txtSurveyYear));
            //_Listener.Add(new ComboBoxSource(this.cboSemester, ComboBoxSource.ListenAttribute.Text));
            //this.IsPublic.ValueChanged += new EventHandler(IsPublic_ValueChanged);
            //_Listener.Add(new CheckBoxSource(this.IsPublic));
            _Listener.StatusChanged += new EventHandler<ChangeEventArgs>(Listener_StatusChanged);

            this.dgvData.CellEnter += new DataGridViewCellEventHandler(dgvData_CellEnter);
            this.dgvData.CurrentCellDirtyStateChanged += new EventHandler(dgvData_CurrentCellDirtyStateChanged);
            this.dgvData.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dgvData_EditingControlShowing);
            this.dgvData.DataError += new DataGridViewDataErrorEventHandler(dgvData_DataError);
            this.dgvData.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvData_ColumnHeaderMouseClick);
            this.dgvData.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvData_RowHeaderMouseClick);
            this.dgvData.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvData_MouseClick);

            _BGWLoadData = new BackgroundWorker();
            _BGWLoadData.DoWork += new DoWorkEventHandler(_BGWLoadData_DoWork);
            _BGWLoadData.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_BGWLoadData_RunWorkerCompleted);

            _BGWSaveData = new BackgroundWorker();
            _BGWSaveData.DoWork += new DoWorkEventHandler(_BGWSaveData_DoWork);
            _BGWSaveData.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_BGWSaveData_RunWorkerCompleted);
        }

        private void Form_Load(object sender, EventArgs e)
        {
            Access = new AccessHelper();
            Query = new QueryHelper();

            this.InitSchoolYear();
            this.form_loaded = true;
            //this._BGWLoadData.RunWorkerAsync();
        }
        private void InitSchoolYear()
        {
            int school_year;
            if (int.TryParse(K12.Data.School.DefaultSchoolYear, out school_year))
            {
                this.txtSurveyYear.Text = school_year.ToString();
            }
            else
            {
                this.txtSurveyYear.Text = (DateTime.Today.Year - 1911).ToString();
            }
            this._CurrentSchoolYear = this.txtSurveyYear.Text;
        }

        private void dgvData_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            this.dgvData.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgvData_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 1 && dgvData.SelectedCells.Count == 1)
            {
                dgvData.BeginEdit(true);  //Raise EditingControlShowing Event !
                if (dgvData.CurrentCell != null && dgvData.CurrentCell.GetType().ToString() == "System.Windows.Forms.DataGridViewComboBoxCell")
                    (dgvData.EditingControl as ComboBox).DroppedDown = true;  //自動拉下清單
            }
        }

        private void dgvData_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //if (this.dgvData.CurrentCell.ColumnIndex == 1)
            //{
            //    if (e.Control is DataGridViewComboBoxEditingControl)
            //    {
            //        ComboBox comboBox = e.Control as ComboBox;

            //        comboBox.DropDownStyle = ComboBoxStyle.DropDown;
            //        comboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            //        comboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //        comboBox.SelectedIndexChanged += new EventHandler(comboBox_SelectedIndexChanged);
            //    }
            //}
        }

        //private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    (sender as ComboBox).SelectedIndexChanged -= new EventHandler(comboBox_SelectedIndexChanged);

        //    if ((sender as ComboBox).SelectedItem == null)
        //        return;
        //}

        private void dgvData_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgvData.CurrentCell = null;
            dgvData.Rows[e.RowIndex].Selected = true;
        }

        private void dgvData_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgvData.CurrentCell = null;
        }

        private void dgvData_MouseClick(object sender, MouseEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            DataGridView.HitTestInfo hit = dgv.HitTest(e.X, e.Y);

            if (hit.Type == DataGridViewHitTestType.TopLeftHeader)
            {
                dgvData.CurrentCell = null;
                dgvData.SelectAll();
            }
        }

        private void dgvData_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void Listener_StatusChanged(object sender, ChangeEventArgs e)
        {
            if (UserAcl.Current[typeof(Approach_DetailContent)].Editable)
                SaveButtonVisible = e.Status == ValueStatus.Dirty;
            else
                this.SaveButtonVisible = false;

            CancelButtonVisible = e.Status == ValueStatus.Dirty;
        }

        private void _BGWLoadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                this.RefreshUI(null);
                this.lblMessage.Text = e.Error.Message;
                this.Loading = false;
                return;
            }

            if (_RunningKey != PrimaryKey)
            {
                this.Loading = true;
                this._RunningKey = PrimaryKey;
                this._BGWLoadData.RunWorkerAsync();
            }
            else
            {
                this.RefreshUI(e.Result);
            }
        }

        private void _BGWLoadData_DoWork(object sender, DoWorkEventArgs e)
        {
            string SQL = string.Format(@"select q1 as 升學與就業情形, q2 as 升學：就讀學校情形, q3 as 升學：入學方式, q4 as 升學：學制別, q5 as 未升學未就業：動向, survey_year as 填報學年度 from $ischool.jh_kh.graduate_survey_approach where ref_student_id={0} order by last_update_time DESC", this._RunningKey);

            DataTable dataTable = Query.Select(SQL);

            e.Result = dataTable;
        }

        //  檢視不同資料項目即呼叫此方法，PrimaryKey 為資料項目的 Key 值。
        protected override void OnPrimaryKeyChanged(EventArgs e)
        {
            if (!this._BGWLoadData.IsBusy)
            {
                this.Loading = true;
                this._RunningKey = PrimaryKey;
                this._BGWLoadData.RunWorkerAsync();
            }
        }

        //  更新資料項目內 UI 的資料
        private void RefreshUI(object result)
        {
            _Listener.SuspendListen();

            this.dgvData.Rows.Clear();
            this.txtSurveyYear.Text = string.Empty;

            if (result == null)
            {
                ResetOverrideButton();
                return;
            }
            else
                this.lblMessage.Text = string.Empty;

            DataTable dataTable = result as DataTable;
            if (dataTable == null)
            {
                ResetOverrideButton();
                this.lblMessage.Text = "異常發生。";
                return;
            }
            DataRow row = dataTable.NewRow();
            if (dataTable.Rows.Count == 0)
            {
                dataTable.ImportRow(row);
            }
            else
            {
                row = dataTable.Rows[0];
                this.txtSurveyYear.Text = row["填報學年度"] + "";
            }
            int column_index = 0;
            foreach (DataColumn column in dataTable.Columns)
            {
                if (column.ColumnName == "填報學年度")
                    continue;

                List<object> source = new List<object>();

                source.Add(column.ColumnName + "");
                source.Add(null);

                int idx = this.dgvData.Rows.Add(source.ToArray());
                DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)(this.dgvData.Rows[idx].Cells[1]);

                switch (column_index)
                {
                    case 0 :
                        cell.DataSource = new string[] { "", "1", "2", "3" };
                        break;
                    case 1:
                        cell.DataSource = new string[] { "", "1", "2", "3", "4", "5", "6", "7", "8" };
                        break;
                    case 2:
                        cell.DataSource = new string[] { "", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
                        break;
                    case 3:
                        cell.DataSource = new string[] { "", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
                        break;
                    case 4:
                        cell.DataSource = new string[] { "", "1", "2", "3", "4", "5", "6" };
                        break;
                }
                if (cell.DataSource != null)
                {
                    if ((cell.DataSource as string[]).Contains(row[column] + ""))
                        cell.Value = row[column] + "";
                }
                column_index += 1;
            }
            this.dgvData.CurrentCell = null;
            this.Loading = false;
            ResetOverrideButton();
        }

        protected override void OnCancelButtonClick(EventArgs e)
        {
            if (!_BGWLoadData.IsBusy)
            {
                this._Errors.SetError(this.txtSurveyYear, string.Empty);
                foreach(DataGridViewRow row in this.dgvData.Rows)
                {
                    if (row.IsNewRow)
                        continue;

                    row.Cells[1].ErrorText = string.Empty;
                }
                this._BGWLoadData.RunWorkerAsync();
            }
        }

        private bool Is_Validated()
        {
            bool is_validated = true;

            uint survey_year;
            if (uint.TryParse(this.txtSurveyYear.Text.Trim(), out survey_year))
                this._Errors.SetError(this.txtSurveyYear, string.Empty);
            else
            {
                this._Errors.SetError(this.txtSurveyYear, "請填正整數之民國年。");
                is_validated = false;
            }

            string string_Q1 = this.dgvData.Rows[0].Cells[1].Value + "";
            string string_Q2 = this.dgvData.Rows[1].Cells[1].Value + "";
            string string_Q3 = this.dgvData.Rows[2].Cells[1].Value + "";
            string string_Q4 = this.dgvData.Rows[3].Cells[1].Value + "";
            string string_Q5 = this.dgvData.Rows[4].Cells[1].Value + "";

            int int_Q1;

            if (string.IsNullOrEmpty(string_Q1))
            {
                this.dgvData.Rows[0].Cells[1].ErrorText = "必填。";
                return false;
            }
            else
            {
                this.dgvData.Rows[0].Cells[1].ErrorText = string.Empty;
                int_Q1 = int.Parse(string_Q1);
            }
            //  升學
            if (int_Q1 == 1)
            {
                if (string.IsNullOrEmpty(string_Q2))
                {
                    this.dgvData.Rows[1].Cells[1].ErrorText = "「升學與就業情形」填寫 1 時，「升學：就讀學校情形」必填。";
                    is_validated = false;
                }
                else
                {
                    this.dgvData.Rows[1].Cells[1].ErrorText = string.Empty;
                }
                if (string.IsNullOrEmpty(string_Q3))
                {
                    this.dgvData.Rows[2].Cells[1].ErrorText = "「升學與就業情形」填寫 1 時，「升學：入學方式」必填。";
                    is_validated = false;
                }
                else
                {
                    this.dgvData.Rows[2].Cells[1].ErrorText = string.Empty;
                }
                if (string.IsNullOrEmpty(string_Q4))
                {
                    this.dgvData.Rows[3].Cells[1].ErrorText = "「升學與就業情形」填寫 1 時，「升學：學制別」必填。";
                    is_validated = false;
                }
                else
                {
                    this.dgvData.Rows[3].Cells[1].ErrorText = string.Empty;
                }
            }
            //  升學或就業
            if (int_Q1 == 1 || int_Q1 == 2)
            {
                if (!string.IsNullOrEmpty(string_Q5))
                {
                    this.dgvData.Rows[4].Cells[1].ErrorText = "「升學與就業情形」填寫 1~2 時，「未升學未就業：動向」不得填寫。";
                    is_validated = false;
                }
                else
                {
                    this.dgvData.Rows[4].Cells[1].ErrorText = string.Empty;
                }
            }
            //  未升學未就業：動向，必須填寫
            if (int_Q1 == 3)
            {
                if (string.IsNullOrEmpty(string_Q5))
                {
                    this.dgvData.Rows[4].Cells[1].ErrorText = "「升學與就業情形」填寫 3 時，「未升學未就業：動向」必填。";
                    is_validated = false;
                }
                else
                {
                    this.dgvData.Rows[4].Cells[1].ErrorText = string.Empty;
                }
            }
            //  非升學
            if (int_Q1 == 3 || int_Q1 == 2)
            {
                //  升學：就讀學校情形，不得填寫
                if (!string.IsNullOrEmpty(string_Q2))
                {
                    this.dgvData.Rows[1].Cells[1].ErrorText = "「升學與就業情形」填寫 2~3 時，「升學：就讀學校情形」不得填寫。";
                    is_validated = false;
                }
                else
                {
                    this.dgvData.Rows[1].Cells[1].ErrorText = string.Empty;
                }
                //  升學：入學方式，不得填寫
                if (!string.IsNullOrEmpty(string_Q2))
                {
                    this.dgvData.Rows[2].Cells[1].ErrorText = "「升學與就業情形」填寫 2~3 時，「升學：入學方式」不得填寫。";
                    is_validated = false;
                }
                else
                {
                    this.dgvData.Rows[2].Cells[1].ErrorText = string.Empty;
                }
                //  升學：學制別，不得填寫
                if (!string.IsNullOrEmpty(string_Q2))
                {
                    this.dgvData.Rows[3].Cells[1].ErrorText = "「升學與就業情形」填寫 2~3 時，「升學：學制別」不得填寫。";
                    is_validated = false;
                }
                else
                {
                    this.dgvData.Rows[3].Cells[1].ErrorText = string.Empty;
                }
            }

            return is_validated;
        }

        protected override void OnSaveButtonClick(EventArgs e)
        {
            this.dgvData.CurrentCell = null;
            if (!Is_Validated())
            {
                MessageBox.Show("請先修正錯誤。");
                return;
            }
            this.Loading = true;

            string string_Q1 = this.dgvData.Rows[0].Cells[1].Value + "";
            string string_Q2 = this.dgvData.Rows[1].Cells[1].Value + "";
            string string_Q3 = this.dgvData.Rows[2].Cells[1].Value + "";
            string string_Q4 = this.dgvData.Rows[3].Cells[1].Value + "";
            string string_Q5 = this.dgvData.Rows[4].Cells[1].Value + "";

            int int_QQ;

            UDT.Approach approach = new UDT.Approach();

            approach.LastUpdateTime = DateTime.Now;
            approach.StudentID = int.Parse(this.PrimaryKey);
            approach.SurveyYear = int.Parse(this.txtSurveyYear.Text.Trim());
            approach.Q1 = int.Parse(this.dgvData.Rows[0].Cells[1].Value + "");
            if (int.TryParse(string_Q2, out int_QQ))
                approach.Q2 = int_QQ;
            else
                approach.Q2 = null;
            if (int.TryParse(string_Q3, out int_QQ))
                approach.Q3 = int_QQ;
            else
                approach.Q3 = null;
            if (int.TryParse(string_Q4, out int_QQ))
                approach.Q4 = int_QQ;
            else
                approach.Q4 = null;
            if (int.TryParse(string_Q5, out int_QQ))
                approach.Q5 = int_QQ;
            else
                approach.Q5 = null;

            this._BGWSaveData.RunWorkerAsync(approach);
        }

        private void _BGWSaveData_DoWork(object sender, DoWorkEventArgs e)
        {
            UDT.Approach approach = e.Argument as UDT.Approach;
            SaveUDT(approach);
        }

        private void _BGWSaveData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this._BGWLoadData.RunWorkerAsync();
        }

        private void SaveUDT(UDT.Approach approach)
        {
            List<UDT.Approach> records = Access.Select<UDT.Approach>(string.Format("ref_student_id={0}", this._RunningKey));

            if (records.Count > 0)
                records.ForEach(x => x.Deleted = true);

            records.Add(approach);
            records.SaveAll();

                //if (scattendExts.Count > 0)
                //{
                //    CourseRecord courseRecord = Course.SelectByID(PrimaryKey);
                //    Dictionary<string, StudentRecord> dicStudentRecords = Student.SelectByIDs(scattendExts.Select(x => x.StudentID.ToString())).ToDictionary(x => x.ID);

                //    LogSaver logBatch = ApplicationLog.CreateLogSaverInstance();
                //    foreach (SCAttendExt deletedRecord in scattendExts)
                //    {
                //        StudentRecord student = dicStudentRecords[deletedRecord.StudentID.ToString()];

                //        StringBuilder sb = new StringBuilder();
                //        sb.Append("學生「" + student.Name + "」，學號「" + student.StudentNumber + "」");
                //        sb.AppendLine("被刪除一筆「修課記錄」。");
                //        sb.AppendLine("詳細資料：");
                //        sb.Append("開課「" + courseRecord.Name + "」\n");
                //        sb.Append("報告小組「" + deletedRecord.Group + "」\n");
                //        sb.Append("停修「" + (deletedRecord.IsCancel ? "是" : "否") + "」\n");

                //        logBatch.AddBatch("管理學生修課.刪除", "刪除", "course", PrimaryKey, sb.ToString());
                //    }
                //    logBatch.LogBatch(true);
                //}
        }

        private void ResetOverrideButton()
        {
            SaveButtonVisible = false;
            CancelButtonVisible = false;

            _Listener.Reset();
            _Listener.ResumeListen();
        }
    }

    /// <summary>
    /// Condition 下僅能有乙個 RootElement，所以：僅能有乙個欄位條件。故多欄位條件請使用：QueryString
    /// </summary>
    public class Condition : FISCA.UDT.Condition.ICondition
    {
        //<Condition>
        //    <In FieldName="ref_course_id">
        //        <Value>391</Value>
        //        <Value>385</Value>
        //    </In>
        //    <In FieldName="ref_student_id">
        //        <Value>10416</Value>
        //    </In>
        //</Condition>
        private Dictionary<string, List<string>> dicFields;
        public Condition(Dictionary<string, List<string>> Fields) 
        {
            this.dicFields = Fields;
        }
        
        System.Xml.XmlElement FISCA.UDT.Condition.ICondition.GetCondtionElement()
        {
            if (this.dicFields.Count == 0)
                return null;

            XmlDocument xmlDocument = new XmlDocument();
            XmlElement rootElement = xmlDocument.CreateElement("Condition");
            foreach(KeyValuePair<string, List<string>> kv in this.dicFields)
            {
                XmlElement firstElement = xmlDocument.CreateElement("In");
                firstElement.SetAttribute("FieldName", kv.Key); 
                foreach(string value in kv.Value)
                {
                    XmlElement secondElement = xmlDocument.CreateElement("Value");
                    secondElement.InnerText = value + "";
                    firstElement.AppendChild(secondElement);
                }
                rootElement.AppendChild(firstElement);
            }
            xmlDocument.AppendChild(rootElement);
            return xmlDocument.DocumentElement;
        }
    }
}
