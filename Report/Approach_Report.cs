using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aspose.Words;
using FISCA.Data;
using FISCA.Presentation.Controls;
using FISCA.UDT;

namespace JH_KH_GraduateSurvey.Report
{
    public partial class Approach_Report : BaseForm
    {
        private AccessHelper Access;
        private QueryHelper Query;
        
        public Approach_Report()
        {
            InitializeComponent();

            this.Load += new EventHandler(Form_Load);

            Access = new AccessHelper();
            Query = new QueryHelper();

            this.InitSchoolYear();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            this.circularProgress.Visible = false;
            this.circularProgress.IsRunning = false;
        }

        private void InitSchoolYear()
        {
            int DefaultSchoolYear;
            if (int.TryParse(K12.Data.School.DefaultSchoolYear, out DefaultSchoolYear))
            {
                this.nudSchoolYear.Value = decimal.Parse(DefaultSchoolYear.ToString());
            }
            else
            {
                this.nudSchoolYear.Value = decimal.Parse((DateTime.Today.Year - 1911).ToString());
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MailMerge_MergeField(object sender, Aspose.Words.Reporting.MergeFieldEventArgs e)
        {
            #region 科目成績
             
            #endregion
        }

        //報表產生完成後，儲存並且開啟
        private void Completed(string inputReportName, Document inputDoc)
        {
            SaveFileDialog sd = new SaveFileDialog();
            sd.Title = "另存新檔";
            sd.FileName = inputReportName + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".doc";
            sd.Filter = "Word檔案 (*.doc)|*.doc|所有檔案 (*.*)|*.*";
            sd.AddExtension = true;
            if (sd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    inputDoc.Save(sd.FileName, Aspose.Words.SaveFormat.Doc);
                    System.Diagnostics.Process.Start(sd.FileName);
                }
                catch
                {
                    MsgBox.Show("指定路徑無法存取。", "建立檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string survey_year = this.nudSchoolYear.Value + "";
            this.btnPrint.Enabled = false;
            this.circularProgress.Visible = true;
            this.circularProgress.IsRunning = true;

            Task<Document> task = Task<Document>.Factory.StartNew(() =>
            {
                MemoryStream template = new MemoryStream(Properties.Resources.高雄市國中畢業生進路統計報表樣版);
                Document doc = new Document();
                Document dataDoc = new Document(template, "", LoadFormat.Doc, "");
                dataDoc.MailMerge.MergeField += new Aspose.Words.Reporting.MergeFieldEventHandler(MailMerge_MergeField);
                dataDoc.MailMerge.RemoveEmptyParagraphs = true;
                doc.Sections.Clear();
                List<string> keys = new List<string>();
                List<object> values = new List<object>();
                Dictionary<string, object> mergeKeyValue = new Dictionary<string, object>();
                List<UDT.Approach> Records = this.Access.Select<UDT.Approach>("survey_year=" + survey_year);
                if (Records.Count == 0)
                    throw new Exception("本年度無填報資料。");

                decimal A1_sum = Records.Count; decimal A2_sum = 0; decimal A3_sum = 0; decimal A4_sum = 0;
                decimal B1_sum = 0; decimal B2_sum = 0; decimal B3_sum = 0; decimal B4_sum = 0;
                decimal C1_sum = 0; decimal C2_sum = 0; decimal C3_sum = 0; decimal C4_sum = 0;
                decimal D1_sum = 0; decimal D2_sum = 0; decimal D3_sum = 0; decimal D4_sum = 0;

                decimal E2_sum = 0; decimal F2_sum = 0; decimal G2_sum = 0; decimal H2_sum = 0; decimal I2_sum = 0; 
                decimal E3_sum = 0; decimal F3_sum = 0; decimal G3_sum = 0; decimal H3_sum = 0; decimal I3_sum = 0; decimal J3_sum = 0;

                decimal K4_sum = 0; decimal L4_sum = 0; decimal M4_sum = 0; decimal N4_sum = 0; decimal O4_sum = 0; decimal P4_sum = 0;
                decimal Q4_sum = 0; decimal R4_sum = 0; decimal S4_sum = 0;

                decimal E4_sum = 0; decimal F4_sum = 0; decimal G4_sum = 0; decimal H4_sum = 0; decimal I4_sum = 0; decimal J4_sum = 0;
                foreach (UDT.Approach record in Records)
                {
                    //  升學或就業情形
                    if (record.Q1 == 1)
                        B1_sum += 1;
                    if (record.Q1 == 2)
                        C1_sum += 1;
                    if (record.Q1 == 3)
                        D1_sum += 1;

                    //  就讀學校
                    if (record.Q2 == 1)
                        B2_sum += 1;
                    if (record.Q2 == 2)
                        C2_sum += 1;
                    if (record.Q2 == 3)
                        D2_sum += 1;
                    if (record.Q2 == 4)
                        E2_sum += 1;
                    if (record.Q2 == 5)
                        F2_sum += 1;
                    if (record.Q2 == 6)
                        G2_sum += 1;
                    if (record.Q2 == 7)
                        H2_sum += 1;
                    if (record.Q2 == 8)
                        I2_sum += 1;
                    //  學制別
                    if (record.Q3 == 1)
                        B3_sum += 1;
                    if (record.Q3 == 2)
                        C3_sum += 1;
                    if (record.Q3 == 3)
                        D3_sum += 1;
                    if (record.Q3 == 4)
                        E3_sum += 1;
                    if (record.Q3 == 5)
                        F3_sum += 1;
                    if (record.Q3 == 6)
                        G3_sum += 1;
                    if (record.Q3 == 7)
                        H3_sum += 1;
                    if (record.Q3 == 8)
                        I3_sum += 1;
                    if (record.Q3 == 9)
                        J3_sum += 1;

                    //  入學方式
                    if (record.Q4 == 1)
                        B4_sum += 1;
                    if (record.Q4 == 2)
                        C4_sum += 1;
                    if (record.Q4 == 3)
                        D4_sum += 1;
                    if (record.Q4 == 4)
                        E4_sum += 1;
                    if (record.Q4 == 5)
                        F4_sum += 1;
                    if (record.Q4 == 6)
                        G4_sum += 1;
                    if (record.Q4 == 7)
                        H4_sum += 1;
                    if (record.Q4 == 8)
                        I4_sum += 1;
                    if (record.Q4 == 9)
                        J4_sum += 1;
                    if (record.Q4 == 10)
                        K4_sum += 1;
                    if (record.Q4 == 11)
                        L4_sum += 1;
                    if (record.Q4 == 12)
                        M4_sum += 1;
                    if (record.Q4 == 13)
                        N4_sum += 1;
                    if (record.Q4 == 14)
                        O4_sum += 1;
                    if (record.Q4 == 15)
                        P4_sum += 1;
                    if (record.Q4 == 16)
                        Q4_sum += 1;
                    if (record.Q4 == 17)
                        R4_sum += 1;
                    if (record.Q4 == 18)
                        S4_sum += 1;                      

                }

                #region 全校畢業學生升學就業情形
                mergeKeyValue.Add("A1", A1_sum);
                mergeKeyValue.Add("B1", B1_sum);
                mergeKeyValue.Add("C1", C1_sum);
                mergeKeyValue.Add("D1", D1_sum);
                mergeKeyValue.Add("B1/A1", Math.Round(B1_sum * 100 / A1_sum, 2, MidpointRounding.AwayFromZero));
                mergeKeyValue.Add("C1/A1", Math.Round(C1_sum * 100 / A1_sum, 2, MidpointRounding.AwayFromZero));
                mergeKeyValue.Add("D1/A1", Math.Round(D1_sum * 100 / A1_sum, 2, MidpointRounding.AwayFromZero));
                #endregion

                #region 全校畢業學生升學之就讀學校情形
                A2_sum = B2_sum + C2_sum + D2_sum + E2_sum + F2_sum + G2_sum + H2_sum + I2_sum;
                mergeKeyValue.Add("A2", A2_sum);
                mergeKeyValue.Add("B2", B2_sum);
                mergeKeyValue.Add("C2", C2_sum);
                mergeKeyValue.Add("D2", D2_sum);
                mergeKeyValue.Add("E2", E2_sum);
                mergeKeyValue.Add("F2", F2_sum);
                mergeKeyValue.Add("G2", G2_sum);
                mergeKeyValue.Add("H2", H2_sum);
                mergeKeyValue.Add("I2", I2_sum);
                mergeKeyValue.Add("B2/A2", A2_sum > 0 ? Math.Round(B2_sum * 100 / A2_sum, 2, MidpointRounding.AwayFromZero) : 0);
                mergeKeyValue.Add("C2/A2", A2_sum > 0 ? Math.Round(C2_sum * 100 / A2_sum, 2, MidpointRounding.AwayFromZero) : 0);
                mergeKeyValue.Add("D2/A2", A2_sum > 0 ? Math.Round(D2_sum * 100 / A2_sum, 2, MidpointRounding.AwayFromZero) : 0);
                mergeKeyValue.Add("E2/A2", A2_sum > 0 ? Math.Round(E2_sum * 100 / A2_sum, 2, MidpointRounding.AwayFromZero) : 0);
                mergeKeyValue.Add("F2/A2", A2_sum > 0 ? Math.Round(F2_sum * 100 / A2_sum, 2, MidpointRounding.AwayFromZero) : 0);
                mergeKeyValue.Add("G2/A2", A2_sum > 0 ? Math.Round(G2_sum * 100 / A2_sum, 2, MidpointRounding.AwayFromZero) : 0);
                mergeKeyValue.Add("H2/A2", A2_sum > 0 ? Math.Round(H2_sum * 100 / A2_sum, 2, MidpointRounding.AwayFromZero) : 0);
                mergeKeyValue.Add("I2/A2", A2_sum > 0 ? Math.Round(I2_sum * 100 / A2_sum, 2, MidpointRounding.AwayFromZero) : 0);
                #endregion

                #region 學制別
                A3_sum = B3_sum + C3_sum + D3_sum + E3_sum + F3_sum + G3_sum + H3_sum + I3_sum + J3_sum;
                mergeKeyValue.Add("A3", A3_sum);
                mergeKeyValue.Add("B3", B3_sum);
                mergeKeyValue.Add("C3", C3_sum);
                mergeKeyValue.Add("D3", D3_sum);
                mergeKeyValue.Add("E3", E3_sum);
                mergeKeyValue.Add("F3", F3_sum);
                mergeKeyValue.Add("G3", G3_sum);
                mergeKeyValue.Add("H3", H3_sum);
                mergeKeyValue.Add("I3", I3_sum);
                mergeKeyValue.Add("J3", J3_sum);
                mergeKeyValue.Add("B3/A3", A3_sum > 0 ? Math.Round(B4_sum * 100 / A3_sum, 2, MidpointRounding.AwayFromZero) : 0);
                mergeKeyValue.Add("C3/A3", A3_sum > 0 ? Math.Round(C4_sum * 100 / A3_sum, 2, MidpointRounding.AwayFromZero) : 0);
                mergeKeyValue.Add("D3/A3", A3_sum > 0 ? Math.Round(D4_sum * 100 / A3_sum, 2, MidpointRounding.AwayFromZero) : 0);
                mergeKeyValue.Add("E3/A3", A3_sum > 0 ? Math.Round(E4_sum * 100 / A3_sum, 2, MidpointRounding.AwayFromZero) : 0);
                mergeKeyValue.Add("F3/A3", A3_sum > 0 ? Math.Round(F4_sum * 100 / A3_sum, 2, MidpointRounding.AwayFromZero) : 0);
                mergeKeyValue.Add("G3/A3", A3_sum > 0 ? Math.Round(G4_sum * 100 / A3_sum, 2, MidpointRounding.AwayFromZero) : 0);
                mergeKeyValue.Add("H3/A3", A3_sum > 0 ? Math.Round(H4_sum * 100 / A3_sum, 2, MidpointRounding.AwayFromZero) : 0);
                mergeKeyValue.Add("I3/A3", A3_sum > 0 ? Math.Round(I4_sum * 100 / A3_sum, 2, MidpointRounding.AwayFromZero) : 0);
                mergeKeyValue.Add("J3/A3", A3_sum > 0 ? Math.Round(J4_sum * 100 / A3_sum, 2, MidpointRounding.AwayFromZero) : 0);
                #endregion

                #region 入學方式
                A4_sum = B4_sum + C4_sum + D4_sum + E4_sum + F4_sum + G4_sum + H4_sum + I4_sum + J4_sum +
                         K4_sum + L4_sum + M4_sum + N4_sum + O4_sum + P4_sum + Q4_sum + R4_sum + S4_sum;

                mergeKeyValue.Add("A4", A4_sum);

                mergeKeyValue.Add("B4", B4_sum);
                mergeKeyValue.Add("C4", C4_sum);
                mergeKeyValue.Add("D4", D4_sum);
                mergeKeyValue.Add("E4", E4_sum);
                mergeKeyValue.Add("F4", F4_sum);
                mergeKeyValue.Add("G4", G4_sum);
                mergeKeyValue.Add("H4", H4_sum);
                mergeKeyValue.Add("I4", I4_sum);
                mergeKeyValue.Add("J4", J4_sum);
                mergeKeyValue.Add("K4", K4_sum);
                mergeKeyValue.Add("L4", L4_sum);
                mergeKeyValue.Add("M4", M4_sum);
                mergeKeyValue.Add("N4", N4_sum);
                mergeKeyValue.Add("O4", O4_sum);
                mergeKeyValue.Add("P4", P4_sum);
                mergeKeyValue.Add("Q4", Q4_sum);
                mergeKeyValue.Add("R4", R4_sum);
                mergeKeyValue.Add("S4", S4_sum);

                mergeKeyValue.Add("B4/A4", A4_sum > 0 ? Math.Round(B4_sum * 100 / A4_sum, 2, MidpointRounding.AwayFromZero) : 0);
                mergeKeyValue.Add("C4/A4", A4_sum > 0 ? Math.Round(C4_sum * 100 / A4_sum, 2, MidpointRounding.AwayFromZero) : 0);
                mergeKeyValue.Add("D4/A4", A4_sum > 0 ? Math.Round(D4_sum * 100 / A4_sum, 2, MidpointRounding.AwayFromZero) : 0);
                mergeKeyValue.Add("E4/A4", A4_sum > 0 ? Math.Round(E4_sum * 100 / A4_sum, 2, MidpointRounding.AwayFromZero) : 0);
                mergeKeyValue.Add("F4/A4", A4_sum > 0 ? Math.Round(F4_sum * 100 / A4_sum, 2, MidpointRounding.AwayFromZero) : 0);
                mergeKeyValue.Add("G4/A4", A4_sum > 0 ? Math.Round(G4_sum * 100 / A4_sum, 2, MidpointRounding.AwayFromZero) : 0);
                mergeKeyValue.Add("H4/A4", A4_sum > 0 ? Math.Round(H4_sum * 100 / A4_sum, 2, MidpointRounding.AwayFromZero) : 0);
                mergeKeyValue.Add("I4/A4", A4_sum > 0 ? Math.Round(I4_sum * 100 / A4_sum, 2, MidpointRounding.AwayFromZero) : 0);
                mergeKeyValue.Add("J4/A4", A4_sum > 0 ? Math.Round(J4_sum * 100 / A4_sum, 2, MidpointRounding.AwayFromZero) : 0);

                mergeKeyValue.Add("K4/A4", A4_sum > 0 ? Math.Round(K4_sum * 100 / A4_sum, 2, MidpointRounding.AwayFromZero) : 0);
                mergeKeyValue.Add("L4/A4", A4_sum > 0 ? Math.Round(L4_sum * 100 / A4_sum, 2, MidpointRounding.AwayFromZero) : 0);
                mergeKeyValue.Add("M4/A4", A4_sum > 0 ? Math.Round(M4_sum * 100 / A4_sum, 2, MidpointRounding.AwayFromZero) : 0);
                mergeKeyValue.Add("N4/A4", A4_sum > 0 ? Math.Round(N4_sum * 100 / A4_sum, 2, MidpointRounding.AwayFromZero) : 0);
                mergeKeyValue.Add("O4/A4", A4_sum > 0 ? Math.Round(O4_sum * 100 / A4_sum, 2, MidpointRounding.AwayFromZero) : 0);
                mergeKeyValue.Add("P4/A4", A4_sum > 0 ? Math.Round(P4_sum * 100 / A4_sum, 2, MidpointRounding.AwayFromZero) : 0);
                mergeKeyValue.Add("Q4/A4", A4_sum > 0 ? Math.Round(Q4_sum * 100 / A4_sum, 2, MidpointRounding.AwayFromZero) : 0);
                mergeKeyValue.Add("R4/A4", A4_sum > 0 ? Math.Round(R4_sum * 100 / A4_sum, 2, MidpointRounding.AwayFromZero) : 0);
                mergeKeyValue.Add("S4/A4", A4_sum > 0 ? Math.Round(S4_sum * 100 / A4_sum, 2, MidpointRounding.AwayFromZero) : 0);
                #endregion

                //  學校代碼及名稱
                mergeKeyValue.Add("學校代碼", K12.Data.School.Code);
                mergeKeyValue.Add("填報學校", K12.Data.School.ChineseName);                

                foreach (string key in mergeKeyValue.Keys)
                {
                    keys.Add(key);
                    values.Add(mergeKeyValue[key]);
                }
                
                dataDoc.MailMerge.Execute(keys.ToArray(), values.ToArray());
                doc.Sections.Add(doc.ImportNode(dataDoc.Sections[0], true));
                return doc;
            });
            task.ContinueWith((x) =>
            {
                this.btnPrint.Enabled = true;
                this.circularProgress.Visible = false;
                this.circularProgress.IsRunning = false;

                if (x.Exception != null)
                    MessageBox.Show(x.Exception.InnerException.Message);
                else
                    Completed("國中畢業學生進路調查填報表格", x.Result);
            }, System.Threading.CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}