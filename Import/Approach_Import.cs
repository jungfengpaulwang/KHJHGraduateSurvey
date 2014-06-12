using EMBA.DocumentValidator;
using EMBA.Import;
using EMBA.Validator;
using FISCA.Data;
using FISCA.UDT;
using K12.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JH_KH_GraduateSurvey.Import
{
    class Approach_Import : ImportWizard
    {
        ImportOption mOption;
        AccessHelper Access;
        QueryHelper Query;
        List<UDT.Approach> ExistingRecords;
        Dictionary<string, Dictionary<string, string>> dicStudents;

        public Approach_Import()
        {
            this.IsSplit = false;
            this.ShowAdvancedForm = false;
            this.ValidateRuleFormater = XDocument.Parse(Properties.Resources.format);
            //this.CustomValidate = null;
            //this.SplitThreadCount = 5;
            //this.SplitSize = 3000;

            this.Access = new AccessHelper();
            this.Query = new QueryHelper();
            this.ExistingRecords = new List<UDT.Approach>();
            this.dicStudents = new Dictionary<string, Dictionary<string, string>>();

            this.CustomValidate = (Rows, Messages) =>
            {
                CustomValidator(Rows, Messages);
            };
        }

        public void CustomValidator(List<IRowStream> Rows, RowMessages Messages)
        {
            DataTable dataTables = this.Query.Select("select id_number, id, name from student");
            foreach (DataRow row in dataTables.Rows)
            {
                if (string.IsNullOrWhiteSpace(row["id_number"] + ""))
                    continue;

                if (!this.dicStudents.ContainsKey(row["id_number"] + ""))
                    this.dicStudents.Add(row["id_number"] + "", new Dictionary<string, string>() {{ row["id"] + "", row["name"] + "" }});
            }
            Rows.ForEach((x) =>
            {
                string id_number = x.GetValue("身分證號").Trim();
                string name = x.GetValue("姓名").Trim();
                string q1_string = x.GetValue("升學與就業情形").Trim();
                string q2_string = x.GetValue("升學：就讀學校情形").Trim();
                string q3_string = x.GetValue("升學：入學方式").Trim();
                string q4_string = x.GetValue("升學：學制別").Trim();
                string q5_string = x.GetValue("未升學未就業：動向").Trim();
                int q1_int;
                int q2_int;
                int q3_int;
                int q4_int;
                int q5_int;

                //  「身分證號」必須存在於系統
                if (!this.dicStudents.ContainsKey(id_number))
                {
                    Messages[x.Position].MessageItems.Add(new MessageItem(EMBA.Validator.ErrorType.Error, EMBA.Validator.ValidatorType.Row, "身分證號不存在。"));
                }
                //  「身分證號」必須與「姓名」一致
                else
                {
                    if (this.dicStudents[id_number].ElementAt(0).Value.Trim().ToLower() != name.ToLower())
                        Messages[x.Position].MessageItems.Add(new MessageItem(EMBA.Validator.ErrorType.Error, EMBA.Validator.ValidatorType.Row, "使用身分證號驗證學生姓名錯誤。"));
                }
                //  若「升學與就業情形」有填寫並且為 1
                if (int.TryParse(q1_string, out q1_int))
                {
                    if (q1_int == 1)
                    {
                        //  升學：就讀學校情形，必須填寫且值為 1~8
                        if (!(int.TryParse(q2_string, out q2_int) && (q2_int > 0 && q2_int < 9)))
                        {
                            Messages[x.Position].MessageItems.Add(new MessageItem(EMBA.Validator.ErrorType.Error, EMBA.Validator.ValidatorType.Row, "「升學與就業情形」填寫 1 時，「升學：就讀學校情形」必須填寫 1~8。"));
                        }
                        //  升學：入學方式，必須填寫且值為 1~9
                        if (!(int.TryParse(q3_string, out q3_int) && (q3_int > 0 && q3_int < 10)))
                        {
                            Messages[x.Position].MessageItems.Add(new MessageItem(EMBA.Validator.ErrorType.Error, EMBA.Validator.ValidatorType.Row, "「升學與就業情形」填寫 1 時，「升學：入學方式」必須填寫 1~9。"));
                        }
                        //  升學：學制別，必須填寫且值為 1~9
                        if (!(int.TryParse(q4_string, out q4_int) && (q4_int > 0 && q4_int < 10)))
                        {
                            Messages[x.Position].MessageItems.Add(new MessageItem(EMBA.Validator.ErrorType.Error, EMBA.Validator.ValidatorType.Row, "「升學與就業情形」填寫 1 時，「升學：學制別」必須填寫 1~9。"));
                        }
                    }
                    //  升學 或 就業
                    if (q1_int == 1 || q1_int == 2)
                    {
                        //  未升學未就業：動向，不得填寫
                        if (!string.IsNullOrEmpty(q5_string))
                        {
                            Messages[x.Position].MessageItems.Add(new MessageItem(EMBA.Validator.ErrorType.Error, EMBA.Validator.ValidatorType.Row, "「升學與就業情形」填寫 1~2 時，「未升學未就業：動向」不得填寫。"));
                        }
                    }
                    if (q1_int == 3)
                    {
                        //  未升學未就業：動向，必須填寫且值為 1~6
                        if (!(int.TryParse(q5_string, out q5_int) && (q5_int > 0 && q5_int < 7)))
                        {
                            Messages[x.Position].MessageItems.Add(new MessageItem(EMBA.Validator.ErrorType.Error, EMBA.Validator.ValidatorType.Row, "「升學與就業情形」填寫 3 時，「未升學未就業：動向」必須填寫 1~6。"));
                        }
                    }
                    //  非升學
                    if (q1_int == 3 || q1_int == 2)
                    {
                        //  升學：就讀學校情形，不得填寫
                        if (!string.IsNullOrEmpty(q2_string))
                        {
                            Messages[x.Position].MessageItems.Add(new MessageItem(EMBA.Validator.ErrorType.Error, EMBA.Validator.ValidatorType.Row, "「升學與就業情形」填寫 2~3 時，「升學：就讀學校情形」不得填寫。"));
                        }
                        //  升學：入學方式，不得填寫
                        if (!string.IsNullOrEmpty(q3_string))
                        {
                            Messages[x.Position].MessageItems.Add(new MessageItem(EMBA.Validator.ErrorType.Error, EMBA.Validator.ValidatorType.Row, "「升學與就業情形」填寫 2~3 時，「升學：入學方式」不得填寫。"));
                        }
                        //  升學：學制別，不得填寫
                        if (!string.IsNullOrEmpty(q4_string))
                        {
                            Messages[x.Position].MessageItems.Add(new MessageItem(EMBA.Validator.ErrorType.Error, EMBA.Validator.ValidatorType.Row, "「升學與就業情形」填寫 2~3 時，「升學：學制別」不得填寫。"));
                        }
                    }
                }
            });
        }

        public override ImportAction GetSupportActions()
        {
            return ImportAction.InsertOrUpdate;
        }

        public override System.Xml.Linq.XDocument GetValidateRule()
        {
            return XDocument.Parse(Properties.Resources.Approach_Import);
        }

        public override string Import(List<EMBA.DocumentValidator.IRowStream> Rows)
        {
            this.ExistingRecords = Access.Select<UDT.Approach>();
            //  要新增的 Record 
            List<UDT.Approach> insertRecords = new List<UDT.Approach>();
            //  要更新的 Record
            List<UDT.Approach> updateRecords = new List<UDT.Approach>();
            foreach (IRowStream row in Rows)
            {
                string id_number = row.GetValue("身分證號").Trim();
                string school_year = row.GetValue("填報學年度").Trim();

                string q1_string = row.GetValue("升學與就業情形").Trim();
                string q2_string = row.GetValue("升學：就讀學校情形").Trim();
                string q3_string = row.GetValue("升學：入學方式").Trim();
                string q4_string = row.GetValue("升學：學制別").Trim();
                string q5_string = row.GetValue("未升學未就業：動向").Trim();

                int q2_int;
                int q3_int;
                int q4_int;
                int q5_int;

                int student_id = int.Parse(this.dicStudents[id_number].ElementAt(0).Key);

                UDT.Approach record = new UDT.Approach();
                IEnumerable<UDT.Approach> filterRecords = new List<UDT.Approach>();
                filterRecords = ExistingRecords.Where(x => x.StudentID == student_id);

                if (filterRecords.Count() > 0)
                {
                    record = filterRecords.OrderByDescending(x => x.LastUpdateTime).ElementAt(0);
                    updateRecords.Add(record);
                }
                else
                {
                    insertRecords.Add(record);
                }

                int q1 = int.Parse(q1_string);
                
                record.StudentID = student_id;
                record.SurveyYear = int.Parse(school_year);
                record.Q1 = q1;

                if (int.TryParse(q2_string, out q2_int))
                    record.Q2 = q2_int;
                else
                    record.Q2 = null;

                if (int.TryParse(q3_string, out q3_int))
                    record.Q3 = q3_int;
                else
                    record.Q3 = null;

                if (int.TryParse(q4_string, out q4_int))
                    record.Q4 = q4_int;
                else
                    record.Q4 = null;

                if (int.TryParse(q5_string, out q5_int))
                    record.Q5 = q5_int;
                else
                    record.Q5 = null;

                record.LastUpdateTime = DateTime.Now;
            }
            //  新增
            List<string> insertedIDs = new List<string>();
            try
            {
                insertedIDs = insertRecords.SaveAll();
            }
            catch (System.Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
                return e.Message;
            }
            //  更新
            List<string> updatedIDs = new List<string>();
            try
            {
                updatedIDs = updateRecords.SaveAll();
            }
            catch (System.Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
                return e.Message;
            }

            //  RaiseEvent
            if (insertedIDs.Count > 0 || updatedIDs.Count > 0)
            {
                //IEnumerable<string> uids = insertedIDs.Union(updatedIDs);
                UDT.Approach.RaiseAfterUpdateEvent();
            }
            return "你成功了。";
        }

        public override void Prepare(ImportOption Option)
        {
            mOption = Option;
        }
    }
}
