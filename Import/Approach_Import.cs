using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using EMBA.DocumentValidator;
using EMBA.Import;
using EMBA.Validator;
using FISCA.Data;
using FISCA.LogAgent;
using FISCA.UDT;

namespace JH_KH_GraduateSurvey.Import
{
    class Approach_Import : ImportWizard
    {
        private StringBuilder strLog = new StringBuilder();
        private ImportOption mOption;
        private AccessHelper Access;
        private QueryHelper Query;
        private List<UDT.Approach> ExistingRecords;
        private Dictionary<string, Dictionary<string, string>> dicStudents;

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
                string q3_string = x.GetValue("升學：學制別").Trim();
                string q4_string = x.GetValue("升學：入學方式").Trim();
                string q5_string = x.GetValue("未升學未就業：動向").Trim();
                string q6_string = x.GetValue("是否需要教育部協助").Trim();
                string memo = x.GetValue("備註").Trim();

                int q1_int;
                int q2_int;
                int q3_int;
                int q4_int;
                int q5_int;

                #region 檢查身分證號
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
                #endregion

                #region  檢查「升學與就業情形」
                if (int.TryParse(q1_string, out q1_int))
                {
                    //有填寫並且為 1
                    if (q1_int == 1)
                    {
                        //  升學：就讀學校情形，必須填寫且值為 1~8
                        if (!(int.TryParse(q2_string, out q2_int) && (q2_int > 0 && q2_int < 9)))
                        {
                            Messages[x.Position].MessageItems.Add(new MessageItem(EMBA.Validator.ErrorType.Error, EMBA.Validator.ValidatorType.Row, "「升學與就業情形」填寫 1 時，「升學：就讀學校情形」必須填寫 1~8。"));
                        }
                        //  升學：學制別，必須填寫且值為 1~9
                        if (!(int.TryParse(q3_string, out q3_int) && (q3_int > 0 && q3_int < 10)))
                        {
                            Messages[x.Position].MessageItems.Add(new MessageItem(EMBA.Validator.ErrorType.Error, EMBA.Validator.ValidatorType.Row, "「升學與就業情形」填寫 1 時，「升學：學制別」必須填寫 1~9。"));
                        }
                        //  升學：入學方式，必須填寫且值為 1~18
                        if (!(int.TryParse(q4_string, out q4_int) && (q4_int > 0 && q4_int < 19)))
                        {
                            Messages[x.Position].MessageItems.Add(new MessageItem(EMBA.Validator.ErrorType.Error, EMBA.Validator.ValidatorType.Row, "「升學與就業情形」填寫 1 時，「升學：入學方式」必須填寫 1~18。"));
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
                #endregion

                #region 檢查就讀學校
                if (int.TryParse(q2_string, out q2_int))
                {
                    if (q2_int == 5)
                    {
                        if (int.TryParse(q3_string, out q3_int))
                        {
                            if (q3_int != 8)
                                Messages[x.Position].MessageItems.Add(new MessageItem(EMBA.Validator.ErrorType.Error, EMBA.Validator.ValidatorType.Row, "「就讀學校填」填寫 5 時，學制別僅填8。"));
                        }

                        if (int.TryParse(q4_string, out q4_int))
                        {
                            List<int> Contents = new List<int>() {16,17};

                            if (!Contents.Contains(q4_int))
                                Messages[x.Position].MessageItems.Add(new MessageItem(EMBA.Validator.ErrorType.Error, EMBA.Validator.ValidatorType.Row, "「就讀學校填」填寫 5 時，入學方式僅填16、17。"));
                        }
                    }

                    if (q2_int == 6)
                    {
                        if (int.TryParse(q3_string, out q3_int))
                        {
                            if (q3_int != 9)
                                Messages[x.Position].MessageItems.Add(new MessageItem(EMBA.Validator.ErrorType.Error, EMBA.Validator.ValidatorType.Row, "「就讀學校填」填寫 6 時，學制別僅填9。"));
                        }

                        if (int.TryParse(q4_string, out q4_int))
                        {
                            if (q4_int != 3)
                                Messages[x.Position].MessageItems.Add(new MessageItem(EMBA.Validator.ErrorType.Error, EMBA.Validator.ValidatorType.Row, "「就讀學校填」填寫 6 時，入學方式僅填3。"));
                        }
                    }

                    if (q2_int == 7 || q2_int == 8)
                    {
                        if (int.TryParse(q3_string, out q3_int))
                        {
                            if (q3_int != 9)
                                Messages[x.Position].MessageItems.Add(new MessageItem(EMBA.Validator.ErrorType.Error, EMBA.Validator.ValidatorType.Row, "「就讀學校填」填寫 7、8 時，學制別僅填9。"));
                        }

                        if (int.TryParse(q4_string, out q4_int))
                        {
                            if (q4_int != 18)
                                Messages[x.Position].MessageItems.Add(new MessageItem(EMBA.Validator.ErrorType.Error, EMBA.Validator.ValidatorType.Row, "「就讀學校填」填寫 7、8 時，入學方式僅填18。"));
                        }
                    }
                }
                #endregion

                #region 檢查未升學未就業動向
                if (int.TryParse(q5_string, out q5_int))
                {
                    if (q5_int == 2)
                    {
                        #region 若為在家需填寫「是」「否」需教育部協助選項。
                        if (string.IsNullOrEmpty(q6_string))
                            Messages[x.Position].MessageItems.Add(new MessageItem(EMBA.Validator.ErrorType.Error, EMBA.Validator.ValidatorType.Row, "「未升學未就業：動向」為2在家,請選填「是」「否」需教育部協助選項。"));
                        #endregion

                        #region 若填寫「是」，需在「備註」欄填寫聯絡電話及通訊地址
                        if (q6_string.Equals("是") && string.IsNullOrEmpty(memo))
                            Messages[x.Position].MessageItems.Add(new MessageItem(EMBA.Validator.ErrorType.Error, EMBA.Validator.ValidatorType.Row, "「未升學未就業：動向」為2在家,請選填「是」「否」需教育部協助選項,需教育部協助者請於「備註」欄填寫聯絡電話及通訊地址。"));
                        #endregion
                    }

                    #region 若為1失聯，請於「備註」欄中註明失聯原因
                    if (q5_int == 1)
                        if (string.IsNullOrEmpty(memo))
                            Messages[x.Position].MessageItems.Add(new MessageItem(EMBA.Validator.ErrorType.Error, EMBA.Validator.ValidatorType.Row, "「未升學未就業：動向」為1失聯,請於「備註」欄中註明失聯原因(如家長不知學生去向、電話空號等)。"));
                    #endregion

                    #region 若為6其他，請於「備註」欄中註明情況。
                    if (q5_int == 6)
                        if (string.IsNullOrEmpty(memo))
                            Messages[x.Position].MessageItems.Add(new MessageItem(EMBA.Validator.ErrorType.Error, EMBA.Validator.ValidatorType.Row, "「未升學未就業：動向」為6其他,請於「備註」欄中註明情況。"));
                    #endregion
                }
                #endregion

                #region 檢查需教育部協助
                if (!string.IsNullOrEmpty(q6_string))
                {
                    if (!(q6_string.Equals("是") || q6_string.Equals("否")))
                        Messages[x.Position].MessageItems.Add(new MessageItem(EMBA.Validator.ErrorType.Error, EMBA.Validator.ValidatorType.Row, "「需教育部協助」僅能填入「是」或「否」。"));
                }
                #endregion
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
            strLog.Clear();
            strLog.AppendLine("詳細資料：");

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
                string q3_string = row.GetValue("升學：學制別").Trim();
                string q4_string = row.GetValue("升學：入學方式").Trim();
                string q5_string = row.GetValue("未升學未就業：動向").Trim();
                string q6_string = row.GetValue("是否需要教育部協助").Trim();
                string memo = row.GetValue("備註").Trim();

                strLog.AppendLine("身分證號「" + id_number + "」填報學年度「" + school_year + "」升學與就業情形「" + q1_string + "」升學：就讀學校情形「" + q2_string + "」升學：入學方式「" + q3_string + "」升學：學制別「" + q4_string + "」未升學未就業：動向「" + q5_string + "」是否需要教育部協助「" + q6_string + "」備註「" + memo + "」");

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

                record.Q6 = q6_string;
                record.Memo = memo;

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

            ApplicationLog.Log("高雄市國中畢業學生進路調查.匯入", "匯入畢業學生進路","student","", strLog.ToString());

            return "你成功了。";
        }

        public override void Prepare(ImportOption Option)
        {
            mOption = Option;
        }
    }
}
