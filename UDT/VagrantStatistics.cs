//using FISCA.UDT;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace JH_KH_GraduateSurvey.UDT
//{
//    [FISCA.UDT.TableName("ischool.jh_kh.graduate_survey_vagrant_statistics")]
//    public class VagrantStatistics : ActiveRecord
//    {
//        /// <summary>
//        /// 填報學年度
//        /// </summary>
//        [JH_KH_GraduateSurvey.UDT.Field(Field = "survey_year", Indexed = true, Caption = "填報學年度")]
//        public int SurveyYear { get; set; }

//        /// <summary>
//        /// 畢業生目前動向
//        /// </summary>
//        [JH_KH_GraduateSurvey.UDT.Field(Field = "q1", Indexed = false, Caption = "畢業生目前動向")]
//        public int Q1 { get; set; }

//        /// <summary>
//        /// 是否需要教育部協助
//        /// </summary>
//        [JH_KH_GraduateSurvey.UDT.Field(Field = "q2", Indexed = false, Caption = "是否需要教育部協助")]
//        public int Q2 { get; set; }

//        /// <summary>
//        /// 最後統計時間
//        /// </summary>
//        [JH_KH_GraduateSurvey.UDT.Field(Field = "last_calculate_time", Indexed = false, Caption = "最後統計時間")]
//        public DateTime LastCalculateTime { get; set; }

//        /// <summary>
//        /// 最後上傳時間
//        /// </summary>
//        [JH_KH_GraduateSurvey.UDT.Field(Field = "last_upload_time", Indexed = false, Caption = "最後上傳時間")]
//        public DateTime LastUploadTime { get; set; }

//        /// <summary>
//        /// 已上傳？
//        /// </summary>
//        [JH_KH_GraduateSurvey.UDT.Field(Field = "is_uploaded", Indexed = false, Caption = "已上傳")]
//        public bool IsUploaded { get; set; }
//    }
//}
