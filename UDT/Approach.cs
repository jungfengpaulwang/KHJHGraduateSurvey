using FISCA.UDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JH_KH_GraduateSurvey.UDT
{
    [FISCA.UDT.TableName("ischool.jh_kh.graduate_survey_approach")]
    public class Approach : ActiveRecord
    {
        /// <summary>
        /// 填報學年度
        /// </summary>
        [JH_KH_GraduateSurvey.UDT.Field(Field = "survey_year", Indexed = true, Caption = "填報學年度")]
        public int SurveyYear { get; set; }

        /// <summary>
        /// 學生系統編號
        /// </summary>
        [JH_KH_GraduateSurvey.UDT.Field(Field = "ref_student_id", Indexed = true, Caption = "學生系統編號")]
        public int StudentID { get; set; }

        /// <summary>
        /// 升學與就業情形
        /// </summary>
        [JH_KH_GraduateSurvey.UDT.Field(Field = "q1", Indexed = false, Caption = "升學與就業情形")]
        public int Q1 { get; set; }

        /// <summary>
        /// 升學-就讀學校情形
        /// </summary>
        [JH_KH_GraduateSurvey.UDT.Field(Field = "q2", Indexed = false, Caption = "升學-就讀學校情形")]
        public int? Q2 { get; set; }

        /// <summary>
        /// 升學-入學方式
        /// </summary>
        [JH_KH_GraduateSurvey.UDT.Field(Field = "q3", Indexed = false, Caption = "升學-入學方式")]
        public int? Q3 { get; set; }

        /// <summary>
        /// 升學-學制別
        /// </summary>
        [JH_KH_GraduateSurvey.UDT.Field(Field = "q4", Indexed = false, Caption = "升學-學制別")]
        public int? Q4 { get; set; }

        /// <summary>
        /// 未升學未就業-動向
        /// </summary>
        [JH_KH_GraduateSurvey.UDT.Field(Field = "q5", Indexed = false, Caption = "未升學未就業-動向")]
        public int? Q5 { get; set; }

        /// <summary>
        /// 最後匯入時間
        /// </summary>
        [JH_KH_GraduateSurvey.UDT.Field(Field = "last_update_time", Indexed = false, Caption = "最後匯入時間")]
        public DateTime LastUpdateTime { get; set; }

        internal static void RaiseAfterUpdateEvent()
        {
            if (Approach.AfterUpdate != null)
                Approach.AfterUpdate(null, EventArgs.Empty);
        }

        internal static event EventHandler AfterUpdate;

        /// <summary>
        /// 已統計？
        /// </summary>
        //[JH_KH_GraduateSurvey.UDT.Field(Field = "is_calculated", Indexed = false, Caption = "已統計")]
        //public bool IsCalculated { get; set; }
    }
}
