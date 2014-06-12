using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using FISCA.Data;
//using FISCA.LogAgent;
using FISCA.Presentation.Controls;
using System.Threading.Tasks;
using Aspose.Cells;
using System.IO;

namespace JH_KH_GraduateSurvey.Export
{
    /// <summary>
    /// 使用者選擇欄位
    /// </summary>
    public partial class Approach_Export : BaseForm
    {
        private QueryHelper Query;
        private List<string> selectedFields;
        private List<string> RealOnlyFields = new List<string>() { "身分證號", "姓名", "填報學年度", "升學與就業情形", "升學：就讀學校情形", "升學：入學方式", "升學：學制別", "未升學未就業：動向" };

        public List<string> StudentIDs { set; get; }
        public List<string> ClassIDs { set; get; }
        public string SourceType { set; get; }

        public Approach_Export() 
        {
            InitializeComponent();

            this.Load += new System.EventHandler(this.Form_Load);
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            this.selectedFields = new List<string>();
            this.Query = new QueryHelper();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            this.ResetSelectedFields();
            this.InitSchoolYear();
        }

        private void InitSchoolYear()
        {
            int school_year;
            if (int.TryParse(K12.Data.School.DefaultSchoolYear, out school_year))
            {
                this.nudSchoolYear.Value = decimal.Parse(school_year.ToString());
            }
            else
            {
                this.nudSchoolYear.Value = decimal.Parse((DateTime.Today.Year - 1911).ToString());
            }
        }

        //private void SaveLog(DataTable dataTable)
        //{
        //    LogSaver logBatch = ApplicationLog.CreateLogSaverInstance();
        //    List<string> system_IDs = new List<string>();
        //    List<string> Fields = new List<string>();

        //    if (this.selectedFields == null || this.selectedFields.Count == 0)
        //        return;

        //    dataTable.Columns.Cast<DataColumn>().ToList().ForEach((x) =>
        //    {
        //        if (this.selectedFields.Contains(x.ColumnName) && !Fields.Contains(x.ColumnName)) 
        //            Fields.Add(x.ColumnName);
        //    });
        //    dataTable.Rows.Cast<DataRow>().ToList().ForEach(x => system_IDs.Add(x[this.keyField] + ""));
        //    string strSystemIDs = String.Join(",", system_IDs);
        //    string strFields = String.Join(",", Fields);
        //    string strCategory = string.Empty;

        //    if (this.Text.IndexOf("學生") > 0) strCategory = "student";
        //    if (this.Text.IndexOf("班級") > 0) strCategory = "class";
        //    if (this.Text.IndexOf("教師") > 0) strCategory = "teacher";
        //    if (this.Text.IndexOf("課程") > 0) strCategory = "course";

        //    logBatch.AddBatch(this.Text, strCategory, this.keyField + "：" + strSystemIDs, "匯出欄位：" + strFields);
        //    try
        //    {
        //        logBatch.LogBatch(true);
        //    }
        //    catch
        //    {

        //    }
        //}

        private void ResetSelectedFields()
        {
            this.selectedFields.Clear();
            foreach (ListViewItem item in this.FieldContainer.Items)
            {
                item.Checked = chkSelectAll.Checked;
                if (this.RealOnlyFields.Contains(item.Text))
                    item.Checked = true;
                if (item.Checked)
                    if (!this.selectedFields.Contains(item.Text))
                        this.selectedFields.Add(item.Text);                
            }
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            this.ResetSelectedFields();
        }

        private void FieldContainer_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            int current_item_index = e.Item.Index;
            if (e.Item.Checked)
            {
                if (!this.selectedFields.Contains(e.Item.Text))
                    this.selectedFields.Add(e.Item.Text);
            }
            else
            {
                if (this.RealOnlyFields.Contains(e.Item.Text))
                    this.FieldContainer.Items[current_item_index].Checked = true;
                else
                    this.selectedFields.Remove(e.Item.Text);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (!this.radioAllStudentInOneFile.Checked && !this.radioOneClassInOneFile.Checked)
            {
                MessageBox.Show("請先選擇「儲存檔案方式」。");
                return;
            }
            string filePath = string.Empty;
            System.Windows.Forms.FolderBrowserDialog folder = new FolderBrowserDialog();
            do
            {
                DialogResult dr = folder.ShowDialog();
                if (dr == DialogResult.OK)
                    filePath = folder.SelectedPath;
                if (dr == DialogResult.Cancel)
                    return;
            } while (!System.IO.Directory.Exists(filePath));

            this.circularProgress.Visible = true;
            this.circularProgress.IsRunning = true;
            this.btnExport.Enabled = false;

            string school_year = this.nudSchoolYear.Value.ToString();
            Task<string> task = Task <string>.Factory.StartNew(() =>
            {
                try
                {
                    string SQL = string.Empty;
                    if (this.SourceType.ToLower() == "student")
                    {
                        SQL = string.Format("select table_a.id_number as 身分證號, table_a.class_name as 畢業班級, table_a.seat_no as 座號, table_a.student_number as 學號, table_a.name as 姓名, table_a.permanent_phone as 戶籍電話, table_a.contact_phone as 聯絡電話, table_a.sms_phone as 行動電話, table_a.other_phones_1 as 其它電話1, table_a.other_phones_2 as 其它電話2, table_a.other_phones_3 as 其它電話3, table_a.監護人電話, table_a.父親電話, table_a.母親電話, {0} as 填報學年度, table_b.q1 as 升學與就業情形, table_b.q2 as 升學：就讀學校情形, table_b.q3 as 升學：入學方式, table_b.q4 as 升學：學制別, table_b.q5 as 未升學未就業：動向 from (select student.id as student_id, class.class_name, student.seat_no, student.student_number, student.name, student.id_number, student.permanent_phone, student.contact_phone, student.sms_phone, xpath_string(student.other_phones,'PhoneNumber[1]') as other_phones_1, xpath_string(student.other_phones,'PhoneNumber[2]') as other_phones_2, xpath_string(student.other_phones,'PhoneNumber[3]') as other_phones_3, class.id as class_id, xpath_string(custodian_other_info,'Phone') as 監護人電話, xpath_string(father_other_info,'Phone') as 父親電話, xpath_string(mother_other_info,'Phone') as 母親電話 from student left join class on class.id=student.ref_class_id) as table_a left join (select ref_student_id as student_id, survey_year, q1, q2, q3, q4, q5, last_update_time from $ischool.jh_kh.graduate_survey_approach where survey_year={0}) as table_b on table_b.student_id=table_a.student_id where table_a.student_id in ({1}) order by class_name, seat_no, student_number, last_update_time DESC", school_year, string.Join(",", this.StudentIDs));
                    }
                    else if (this.SourceType.ToLower() == "class")
                    {
                        SQL = string.Format("select table_a.id_number as 身分證號, table_a.class_name as 畢業班級, table_a.seat_no as 座號, table_a.student_number as 學號, table_a.name as 姓名, table_a.permanent_phone as 戶籍電話, table_a.contact_phone as 聯絡電話, table_a.sms_phone as 行動電話, table_a.other_phones_1 as 其它電話1, table_a.other_phones_2 as 其它電話2, table_a.other_phones_3 as 其它電話3, table_a.監護人電話, table_a.父親電話, table_a.母親電話, {0} as 填報學年度, table_b.q1 as 升學與就業情形, table_b.q2 as 升學：就讀學校情形, table_b.q3 as 升學：入學方式, table_b.q4 as 升學：學制別, table_b.q5 as 未升學未就業：動向 from (select student.id as student_id, class.class_name, student.seat_no, student.student_number, student.name, student.id_number, student.permanent_phone, student.contact_phone, student.sms_phone, xpath_string(student.other_phones,'PhoneNumber[1]') as other_phones_1, xpath_string(student.other_phones,'PhoneNumber[2]') as other_phones_2, xpath_string(student.other_phones,'PhoneNumber[3]') as other_phones_3, class.id as class_id, xpath_string(custodian_other_info,'Phone') as 監護人電話, xpath_string(father_other_info,'Phone') as 父親電話, xpath_string(mother_other_info,'Phone') as 母親電話 from student left join class on class.id=student.ref_class_id) as table_a left join (select ref_student_id as student_id, survey_year, q1, q2, q3, q4, q5, last_update_time from $ischool.jh_kh.graduate_survey_approach where survey_year={0}) as table_b on table_b.student_id=table_a.student_id where table_a.class_id in ({1}) order by class_name, seat_no, student_number, last_update_time DESC", school_year, string.Join(",", this.ClassIDs));
                    }

                    DataTable dataTable = Query.Select(SQL);
                    string fileName = string.Empty;
                    if (this.radioAllStudentInOneFile.Checked)
                    {
                        fileName = school_year + "學年度國中畢業學生進路填報表" + DateTime.Now.ToString(" yyyy-MM-dd_HH_mm_ss") + ".xls";
                        Workbook workbook = this.ToWorkbook(dataTable, false, this.selectedFields);
                        Worksheet worksheet = workbook.Worksheets[0];
                        worksheet.Name = school_year + "學年度國中畢業學生進路填報表";
                        
                        this.AddComments(worksheet);
                        worksheet.AutoFitColumns();
                        workbook.Save(Path.Combine(filePath, fileName), FileFormatType.Excel2003);
                        return filePath;
                    }
                    else
                    {
                        DataSet dataSet = new DataSet();
                        DataTable newDataTable = new DataTable();

                        string className = string.Empty;
                        foreach (DataRow dataRow in dataTable.Rows)
                        {
                            if (className != (dataRow["畢業班級"] + "").Trim())
                            {
                                newDataTable = dataTable.Clone();
                                newDataTable.TableName = ReplaceString((dataRow["畢業班級"] + "").Trim());

                                dataSet.Tables.Add(newDataTable);

                                className = (dataRow["畢業班級"] + "").Trim();
                            }
                            newDataTable.ImportRow(dataRow);
                        }
                        Dictionary<string, Workbook> workbooks = this.ToWorkbooks(dataSet, true, this.selectedFields);
                        foreach(KeyValuePair<string, Workbook> kv in workbooks)
                        {
                            Worksheet worksheet = kv.Value.Worksheets[0];
                            worksheet.Name = school_year + "學年度國中畢業學生進路填報表";
                            this.AddComments(worksheet);
                            worksheet.AutoFitColumns();
                            fileName = school_year + "學年度" + kv.Key + "班國中畢業學生進路填報表" + DateTime.Now.ToString(" yyyy-MM-dd_HH_mm_ss") + ".xls";
                            kv.Value.Save(Path.Combine(filePath, fileName), FileFormatType.Excel2003);
                        }
                        return filePath;
                    }
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            });
            task.ContinueWith((x) =>
            {
                this.circularProgress.Visible = false;
                this.circularProgress.IsRunning = false;
                this.btnExport.Enabled = true;

                if (x.Exception != null)
                {
                    MessageBox.Show(x.Exception.InnerException.Message);
                    return;
                }
                
                System.Diagnostics.Process.Start(x.Result);
            }, System.Threading.CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private byte? GetColumnIndex(Worksheet worksheet, string columnName)
        {
            for (byte i = 0; i <= worksheet.Cells.MaxDataColumn; i++)
            {
                if ((worksheet.Cells[0, i].Value + "").Trim().ToLower() == columnName.Trim().ToLower())
                    return i;
            }
            return null;
        }

        private void AddComments(Worksheet worksheet)
        {
            string columnName = "升學與就業情形";
            string note = "填代碼 1~3。(1：升學；2：就業；3：未升學未就業)\n填「1」者，請續填「升學：就讀學校情形」、「升學：入學方式」、「升學：學制別」。\n填「3」者，請續填「未升學未就業：動向」。";
            if (this.selectedFields.Contains(columnName))
            {
                byte? columnIndex = this.GetColumnIndex(worksheet, columnName);
                if (columnIndex.HasValue)
                    this.AddComment(worksheet, note, columnIndex.Value);
            }
            columnName = "升學：就讀學校情形";
            note = "填代碼 1~8。(1：公立高中；2：私立高中；3：公立高職；4：私立高職；5：五專；6：軍事學校；7：赴國外或大陸就學；8：其他)";
            if (this.selectedFields.Contains(columnName))
            {
                byte? columnIndex = this.GetColumnIndex(worksheet, columnName);
                if (columnIndex.HasValue)
                    this.AddComment(worksheet, note, columnIndex.Value);
            }
            columnName = "升學：入學方式";
            note = "填代碼 1~9。(1：申請入學；2：甄選入學；3：登記分發；4：輔導分發(實用技能學程)；5：個別招生；6：技優保送；7：免試入學；8：十二年安置；9：其他)";
            if (this.selectedFields.Contains(columnName))
            {
                byte? columnIndex = this.GetColumnIndex(worksheet, columnName);
                if (columnIndex.HasValue)
                    this.AddComment(worksheet, note, columnIndex.Value);
            }
            columnName = "升學：學制別";
            note = "填代碼 1~9。(1：職業類科；2：綜合高中；3：普通高中；4：建教合作班；5：實用技能學程(日)；6：實用技能學程(夜)；7：進修學校；8：五專；9：其他)";
            if (this.selectedFields.Contains(columnName))
            {
                byte? columnIndex = this.GetColumnIndex(worksheet, columnName);
                if (columnIndex.HasValue)
                    this.AddComment(worksheet, note, columnIndex.Value);
            }
            columnName = "未升學未就業：動向";
            note = "填代碼 1~6。(1：失聯；2：在家；3：重考；4：出國；5：參加職訓；6：其他)";
            if (this.selectedFields.Contains(columnName))
            {
                byte? columnIndex = this.GetColumnIndex(worksheet, columnName);
                if (columnIndex.HasValue)
                    this.AddComment(worksheet, note, columnIndex.Value);
            }
                
        }

        private void RemoveComment(Worksheet worksheet)
        {
            for (int i = 0; i < worksheet.Comments.Count; i++)
                worksheet.Comments.RemoveAt(i);
        }

        private void AddComment(Worksheet worksheet, string note, byte columnIndex)
        {
            int commentIndex = worksheet.Comments.Add(0, columnIndex);
            Comment comment = worksheet.Comments[commentIndex];    
            comment.Note = note;
            comment.WidthCM = 20;
            //comment.AutoSize = true;
        }

        private string ReplaceString(string oString)
        {
            return oString.Replace("：", "꞉").Replace(":", "꞉").Replace("/", "⁄").Replace("／", "⁄").Replace(@"\", "∖").Replace("＼", "∖").Replace("?", "_").Replace("？", "_").Replace("*", "✻").Replace("＊", "✻").Replace("<", "〈").Replace("＜", "〈").Replace(">", "〉").Replace("＞", "〉").Replace("\"", "''").Replace("”", "''").Replace("|", "ㅣ").Replace("｜", "ㅣ");
        }

        private Workbook ToWorkbook(DataTable dataTable, bool autoFitColumns, List<string> SelectedFields)
        {
            Workbook wb = new Workbook();

            if (dataTable == null || dataTable.Rows.Count == 0)
                return wb;

            int i = -1;
            for (int columnIndex = 0; columnIndex < dataTable.Columns.Count; columnIndex++)
            {
                if (SelectedFields != null && !SelectedFields.Contains(dataTable.Columns[columnIndex].ColumnName))
                    continue;

                i++;
                wb.Worksheets[0].Cells[0, i].PutValue(dataTable.Columns[columnIndex].ColumnName);
            }

            if (dataTable.Rows.Count == 0)
                return wb;

            for (int rowIndex = 0; rowIndex < dataTable.Rows.Count; rowIndex++)
            {
                i = -1;
                for (int columnIndex = 0; columnIndex < dataTable.Columns.Count; columnIndex++)
                {
                    if (SelectedFields != null && !SelectedFields.Contains(dataTable.Columns[columnIndex].ColumnName))
                        continue;

                    i++;
                    wb.Worksheets[0].Cells[rowIndex + 1, i].PutValue(dataTable.Rows[rowIndex][columnIndex] + "");
                }
            }
            if (autoFitColumns)
                wb.Worksheets[0].AutoFitColumns();

            wb.Worksheets[0].Name = dataTable.TableName;
            return wb;
        }

        public Dictionary<string, Workbook> ToWorkbooks(DataSet dataSet, bool autoFitColumns, List<string> SelectedFields)
        {
            Dictionary<string, Workbook> workbooks = new Dictionary<string, Workbook>();

            if (dataSet == null || dataSet.Tables.Count == 0)
                return workbooks;

            foreach (DataTable dataTable in dataSet.Tables)
                workbooks.Add(dataTable.TableName, this.ToWorkbook(dataTable, autoFitColumns, SelectedFields));

            return workbooks;
        }

        private void radioAllStudentInOneFile_Click(object sender, EventArgs e)
        {
            this.radioOneClassInOneFile.Checked = !this.radioAllStudentInOneFile.Checked;
        }

        private void radioOneClassInOneFile_Click(object sender, EventArgs e)
        {
            this.radioAllStudentInOneFile.Checked = !this.radioOneClassInOneFile.Checked;
        }
    }    
}