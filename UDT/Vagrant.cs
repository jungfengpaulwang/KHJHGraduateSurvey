using FISCA.UDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JH_KH_GraduateSurvey.UDT
{
    [FISCA.UDT.TableName("ischool.jh_kh.graduate_survey_vagrant")]
    public class Vagrant : ActiveRecord
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
        /// 畢業生目前動向
        /// </summary>
        [JH_KH_GraduateSurvey.UDT.Field(Field = "q1", Indexed = false, Caption = "畢業生目前動向")]
        public int Q1 { get; set; }

        /// <summary>
        /// 是否需要教育部協助
        /// </summary>
        [JH_KH_GraduateSurvey.UDT.Field(Field = "q2", Indexed = false, Caption = "是否需要教育部協助")]
        public int? Q2 { get; set; }

        /// <summary>
        /// 備註
        /// </summary>
        [JH_KH_GraduateSurvey.UDT.Field(Field = "memo", Indexed = false, Caption = "備註")]
        public string Memo { get; set; }

        /// <summary>
        /// 最後匯入時間
        /// </summary>
        [JH_KH_GraduateSurvey.UDT.Field(Field = "last_update_time", Indexed = false, Caption = "最後匯入時間")]
        public DateTime LastUpdateTime { get; set; }

        internal static void RaiseAfterUpdateEvent()
        {
            if (Vagrant.AfterUpdate != null)
                Vagrant.AfterUpdate(null, EventArgs.Empty);
        }

        internal static event EventHandler AfterUpdate;

        /// <summary>
        /// 已統計？
        /// </summary>
        //[JH_KH_GraduateSurvey.UDT.Field(Field = "is_calculated", Indexed = false, Caption = "已統計")]
        //public bool IsCalculated { get; set; }
    }
}
