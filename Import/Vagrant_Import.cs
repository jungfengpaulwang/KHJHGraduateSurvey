//using EMBA.DocumentValidator;
//using EMBA.Import;
//using EMBA.Validator;
//using FISCA.Data;
//using FISCA.UDT;
//using K12.Data;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Xml.Linq;

//namespace JH_KH_GraduateSurvey.Import
//{
//    class Vagrant_Import : ImportWizard
//    {
//        ImportOption mOption;
//        AccessHelper Access;
//        QueryHelper Query;
//        List<UDT.Vagrant> ExistingRecords;
//        Dictionary<string, Dictionary<string, string>> dicStudents;

//        public Vagrant_Import()
//        {
//            this.IsSplit = false;
//            this.ShowAdvancedForm = false;
//            this.ValidateRuleFormater = XDocument.Parse(Properties.Resources.format);
//            //this.CustomValidate = null;
//            //this.SplitThreadCount = 5;
//            //this.SplitSize = 3000;

//            this.Access = new AccessHelper();
//            this.Query = new QueryHelper();
//            this.ExistingRecords = new List<UDT.Vagrant>();
//            this.dicStudents = new Dictionary<string, Dictionary<string, string>>();

//            this.CustomValidate = (Rows, Messages) =>
//            {
//                CustomValidator(Rows, Messages);
//            };
//        }

//        public void CustomValidator(List<IRowStream> Rows, RowMessages Messages)
//        {
//            DataTable dataTables = this.Query.Select("select id_number, id, name from student");
//            foreach (DataRow row in dataTables.Rows)
//            {
//                if (string.IsNullOrWhiteSpace(row["id_number"] + ""))
//                    continue;

//                if (!this.dicStudents.ContainsKey(row["id_number"] + ""))
//                    this.dicStudents.Add(row["id_number"] + "", new Dictionary<string, string>() { { row["id"] + "", row["name"] + "" } });
//            }
//            Rows.ForEach((x) =>
//            {
//                string id_number = x.GetValue("身分證號").Trim();
//                string name = x.GetValue("姓名").Trim();
//                string q1_string = x.GetValue("畢業生目前動向").Trim();
//                string q2_string = x.GetValue("是否需要教育部協助").Trim();
//                string q3_string = x.GetValue("備註").Trim();
//                int q1_int;

//                //  「身分證號」必須存在於系統
//                if (!this.dicStudents.ContainsKey(id_number))
//                {
//                    Messages[x.Position].MessageItems.Add(new MessageItem(EMBA.Validator.ErrorType.Error, EMBA.Validator.ValidatorType.Row, "身分證號不存在。"));
//                }
//                //  「身分證號」必須與「姓名」一致
//                else
//                {
//                    if (this.dicStudents[id_number].ElementAt(0).Value.Trim().ToLower() != name.ToLower())
//                        Messages[x.Position].MessageItems.Add(new MessageItem(EMBA.Validator.ErrorType.Error, EMBA.Validator.ValidatorType.Row, "使用身分證號驗證學生姓名錯誤。"));
//                }
//                //  若「畢業生目前動向」有填寫並且為 7
//                if (int.TryParse(q1_string, out q1_int))
//                {
//                    if (q1_int == 7)
//                    {
//                        //  是否需要教育部協助，必須填寫且值為 是；否
//                        if (q2_string != "是" && q2_string != "否")
//                        {
//                            Messages[x.Position].MessageItems.Add(new MessageItem(EMBA.Validator.ErrorType.Error, EMBA.Validator.ValidatorType.Row, "「畢業生目前動向」填寫 7 時，「是否需要教育部協助」必須填寫「是」或「否」。"));
//                        }
//                        if (q2_string == "是" && string.IsNullOrWhiteSpace(q3_string))
//                        {
//                            Messages[x.Position].MessageItems.Add(new MessageItem(EMBA.Validator.ErrorType.Error, EMBA.Validator.ValidatorType.Row, "「是否需要教育部協助」填寫「是」時，請於「備註」欄填寫聯絡電話。"));
//                        }
//                    }
//                    if (q1_int == 8)
//                    {
//                        //  備註，必須填寫
//                        if (string.IsNullOrWhiteSpace(q3_string))
//                        {
//                            Messages[x.Position].MessageItems.Add(new MessageItem(EMBA.Validator.ErrorType.Error, EMBA.Validator.ValidatorType.Row, "「畢業生目前動向」填寫 8 時，「備註」必須註明失聯原因。"));
//                        }
//                    }
//                    if (q1_int == 9)
//                    {
//                        //  備註，必須填寫
//                        if (string.IsNullOrWhiteSpace(q3_string))
//                        {
//                            Messages[x.Position].MessageItems.Add(new MessageItem(EMBA.Validator.ErrorType.Error, EMBA.Validator.ValidatorType.Row, "「畢業生目前動向」填寫 9 時，「備註」必須註明情況。"));
//                        }
//                    }
//                    if (q1_int != 7)
//                    {
//                        //  是否需要教育部協助，不得填寫
//                        if (!string.IsNullOrEmpty(q2_string))
//                        {
//                            Messages[x.Position].MessageItems.Add(new MessageItem(EMBA.Validator.ErrorType.Error, EMBA.Validator.ValidatorType.Row, "「畢業生目前動向」非填寫 7 時，「是否需要教育部協助」不得填寫。"));
//                        }
//                    }
//                }
//            });
//        }

//        public override ImportAction GetSupportActions()
//        {
//            return ImportAction.InsertOrUpdate;
//        }

//        public override System.Xml.Linq.XDocument GetValidateRule()
//        {
//            return XDocument.Parse(Properties.Resources.Vagrant_Import);
//        }

//        public override string Import(List<EMBA.DocumentValidator.IRowStream> Rows)
//        {
//            this.ExistingRecords = Access.Select<UDT.Vagrant>();
//            //  要新增的 Record 
//            List<UDT.Vagrant> insertRecords = new List<UDT.Vagrant>();
//            //  要更新的 Record
//            List<UDT.Vagrant> updateRecords = new List<UDT.Vagrant>();
//            foreach (IRowStream row in Rows)
//            {
//                string id_number = row.GetValue("身分證號").Trim();
//                string school_year = row.GetValue("填報學年度").Trim();

//                string q1_string = row.GetValue("畢業生目前動向").Trim();
//                string q2_string = row.GetValue("是否需要教育部協助").Trim();
//                string q3_string = row.GetValue("備註").Trim();
                
//                int student_id = int.Parse(this.dicStudents[id_number].ElementAt(0).Key);

//                UDT.Vagrant record = new UDT.Vagrant();
//                IEnumerable<UDT.Vagrant> filterRecords = new List<UDT.Vagrant>();
//                filterRecords = ExistingRecords.Where(x => x.StudentID == student_id);

//                if (filterRecords.Count() > 0)
//                {
//                    record = filterRecords.OrderByDescending(x => x.LastUpdateTime).ElementAt(0);
//                    updateRecords.Add(record);
//                }
//                else
//                {
//                    insertRecords.Add(record);
//                }

//                int q1 = int.Parse(q1_string);

//                record.StudentID = student_id;
//                record.SurveyYear = int.Parse(school_year);
//                record.Q1 = q1;
//                if (q2_string == "是")
//                    record.Q2 = 1;
//                else if (q2_string == "否")
//                    record.Q2 = 0;
//                else
//                    record.Q2 = null;
//                record.Memo = q3_string;

//                record.LastUpdateTime = DateTime.Now;
//            }
//            //  新增
//            List<string> insertedIDs = new List<string>();
//            try
//            {
//                insertedIDs = insertRecords.SaveAll();
//            }
//            catch (System.Exception e)
//            {
//                System.Windows.Forms.MessageBox.Show(e.Message);
//                return e.Message;
//            }
//            //  更新
//            List<string> updatedIDs = new List<string>();
//            try
//            {
//                updatedIDs = updateRecords.SaveAll();
//            }
//            catch (System.Exception e)
//            {
//                System.Windows.Forms.MessageBox.Show(e.Message);
//                return e.Message;
//            }

//            //  RaiseEvent
//            if (insertedIDs.Count > 0 || updatedIDs.Count > 0)
//            {
//                //IEnumerable<string> uids = insertedIDs.Union(updatedIDs);
//                UDT.Vagrant.RaiseAfterUpdateEvent();
//            }
//            return "你成功了。";
//        }

//        public override void Prepare(ImportOption Option)
//        {
//            mOption = Option;
//        }
//    }
//}
