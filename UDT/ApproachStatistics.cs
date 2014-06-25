//using FISCA.UDT;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace JH_KH_GraduateSurvey.UDT
//{
//    [FISCA.UDT.TableName("ischool.jh_kh.graduate_survey_approach_statistics")]
//    public class ApproachStatistics : ActiveRecord
//    {
//        /// <summary>
//        /// 填報學年度
//        /// </summary>
//        [JH_KH_GraduateSurvey.UDT.Field(Field = "survey_year", Indexed = true, Caption = "填報學年度")]
//        public int SurveyYear { get; set; }

//        /// <summary>
//        /// 升學與就業情形
//        /// </summary>
//        [JH_KH_GraduateSurvey.UDT.Field(Field = "q1", Indexed = false, Caption = "升學與就業情形")]
//        public int Q1 { get; set; }

//        /// <summary>
//        /// 升學-就讀學校情形
//        /// </summary>
//        [JH_KH_GraduateSurvey.UDT.Field(Field = "q2", Indexed = false, Caption = "升學-就讀學校情形")]
//        public int Q2 { get; set; }

//        /// <summary>
//        /// 升學-入學方式
//        /// </summary>
//        [JH_KH_GraduateSurvey.UDT.Field(Field = "q3", Indexed = false, Caption = "升學-入學方式")]
//        public int Q3 { get; set; }

//        /// <summary>
//        /// 升學-學制別
//        /// </summary>
//        [JH_KH_GraduateSurvey.UDT.Field(Field = "q4", Indexed = false, Caption = "升學-學制別")]
//        public int Q4 { get; set; }

//        /// <summary>
//        /// 未升學未就業-動向
//        /// </summary>
//        [JH_KH_GraduateSurvey.UDT.Field(Field = "q5", Indexed = false, Caption = "未升學未就業-動向")]
//        public int Q5 { get; set; }

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
