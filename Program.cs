using FISCA;
using FISCA.Permission;
using FISCA.Presentation;
using FISCA.UDT;
using K12.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JH_KH_GraduateSurvey
{
    public class Program
    {
        [MainMethod()]
        public static void StartUp()
        {
            SyncUDTSchema();
            Init();
        }

        private static void SyncUDTSchema()
        {
            SchemaManager Manager = new SchemaManager(FISCA.Authentication.DSAServices.DefaultConnection);

            Manager.SyncSchema(new UDT.Vagrant());
            Manager.SyncSchema(new UDT.Approach());
        }

        private static void Init()
        {
        #region 資料項目
            #region 畢業學生進路
            Catalog detail = RoleAclSource.Instance["學生"]["資料項目"];
            detail.Add(new DetailItemFeature(typeof(DetailContent.Approach_DetailContent)));

            if (UserAcl.Current[typeof(DetailContent.Approach_DetailContent)].Viewable)
                NLDPanels.Student.AddDetailBulider<DetailContent.Approach_DetailContent>();
            #endregion

            #region 未升學未就業畢業學生動向
            Catalog detail1 = RoleAclSource.Instance["學生"]["資料項目"];
            detail1.Add(new DetailItemFeature(typeof(DetailContent.Vagrant_DetailContent)));

            if (UserAcl.Current[typeof(DetailContent.Vagrant_DetailContent)].Viewable)
                NLDPanels.Student.AddDetailBulider<DetailContent.Vagrant_DetailContent>();
            #endregion
        #endregion

            #region 匯入

            //MotherForm.RibbonBarItems["學生", "資料統計"]["匯入"].Image = Properties.Resources.Import_Image;
        //MotherForm.RibbonBarItems["學生", "資料統計"]["匯入"].Size = RibbonBarButton.MenuButtonSize.Large;

            #region  畢業學生進路
            Catalog button_importApproach = RoleAclSource.Instance["學生"]["功能按鈕"];
            button_importApproach.Add(new RibbonFeature("Student_Button_Import_GraduateSurveyApproach", "匯入畢業學生進路"));
            MotherForm.RibbonBarItems["學生", "資料統計"]["匯入"]["其它相關匯入"]["匯入畢業學生進路"].Enable = UserAcl.Current["Student_Button_Import_GraduateSurveyApproach"].Executable;
            MotherForm.RibbonBarItems["學生", "資料統計"]["匯入"]["其它相關匯入"]["匯入畢業學生進路"].Click += delegate
            {
                new Import.Approach_Import().Execute();
            };
            #endregion

            #region  未升學未就業畢業學生動向
            Catalog button_importVagrant = RoleAclSource.Instance["學生"]["功能按鈕"];
            button_importVagrant.Add(new RibbonFeature("Student_Button_Import_GraduateSurveyVagrant", "匯入未升學未就業畢業學生動向"));
            MotherForm.RibbonBarItems["學生", "資料統計"]["匯入"]["其它相關匯入"]["匯入未升學未就業畢業學生動向"].Enable = UserAcl.Current["Student_Button_Import_GraduateSurveyVagrant"].Executable;
            MotherForm.RibbonBarItems["學生", "資料統計"]["匯入"]["其它相關匯入"]["匯入未升學未就業畢業學生動向"].Click += delegate
            {
                new Import.Vagrant_Import().Execute();
            };
            #endregion

        #endregion
            
        #region 匯出

            //MotherForm.RibbonBarItems["學生", "資料統計"]["匯出"].Image = Properties.Resources.Export_Image;
            //MotherForm.RibbonBarItems["學生", "資料統計"]["匯出"].Size = RibbonBarButton.MenuButtonSize.Large;

            #region  畢業學生進路
            Catalog button_exportApproach = RoleAclSource.Instance["學生"]["功能按鈕"];
            button_exportApproach.Add(new RibbonFeature("Student_Button_Export_GraduateSurveyApproach", "匯出畢業學生進路"));
            MotherForm.RibbonBarItems["學生", "資料統計"]["匯出"]["其它相關匯出"]["匯出畢業學生進路"].Enable = UserAcl.Current["Student_Button_Export_GraduateSurveyApproach"].Executable;
            MotherForm.RibbonBarItems["學生", "資料統計"]["匯出"]["其它相關匯出"]["匯出畢業學生進路"].Click += delegate
            {
                List<string> Sources = K12.Presentation.NLDPanels.Student.SelectedSource;

                if (Sources.Count > 0)
                {
                    Export.Approach_Export form = new Export.Approach_Export();

                    form.StudentIDs = Sources;
                    form.SourceType = "student";
                    form.ShowDialog();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("請先選取學生。");
                }
            };

            Catalog button_exportApproach_class = RoleAclSource.Instance["班級"]["功能按鈕"];
            button_exportApproach_class.Add(new RibbonFeature("Class_Button_Export_GraduateSurveyApproach", "匯出畢業學生進路"));
            MotherForm.RibbonBarItems["班級", "資料統計"]["匯出"]["匯出畢業學生進路"].Enable = UserAcl.Current["Class_Button_Export_GraduateSurveyApproach"].Executable;
            MotherForm.RibbonBarItems["班級", "資料統計"]["匯出"]["匯出畢業學生進路"].Click += delegate
            {
                List<string> Sources = K12.Presentation.NLDPanels.Class.SelectedSource;

                if (Sources.Count > 0)
                {
                    Export.Approach_Export form = new Export.Approach_Export();

                    form.ClassIDs = Sources;
                    form.SourceType = "class";
                    form.ShowDialog();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("請先選取班級。");
                }
            };
            #endregion

            #region  未升學未就業畢業學生動向
            Catalog button_exportVagrant = RoleAclSource.Instance["學生"]["功能按鈕"];
            button_exportVagrant.Add(new RibbonFeature("Student_Button_Export_GraduateSurveyVagrant", "匯出未升學未就業畢業學生動向"));
            MotherForm.RibbonBarItems["學生", "資料統計"]["匯出"]["其它相關匯出"]["匯出未升學未就業畢業學生動向"].Enable = UserAcl.Current["Student_Button_Export_GraduateSurveyVagrant"].Executable;
            MotherForm.RibbonBarItems["學生", "資料統計"]["匯出"]["其它相關匯出"]["匯出未升學未就業畢業學生動向"].Click += delegate
            {
                List<string> Sources = K12.Presentation.NLDPanels.Student.SelectedSource;

                if (Sources.Count > 0)
                {
                    Export.Vagrant_Export form = new Export.Vagrant_Export();

                    form.StudentIDs = Sources;
                    form.SourceType = "student";
                    form.ShowDialog();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("請先選取學生。");
                }
            };

            Catalog button_exportVagrant_class = RoleAclSource.Instance["班級"]["功能按鈕"];
            button_exportVagrant_class.Add(new RibbonFeature("Class_Button_Export_GraduateSurveyVagrant", "匯出未升學未就業畢業學生動向"));
            MotherForm.RibbonBarItems["班級", "資料統計"]["匯出"]["匯出未升學未就業畢業學生動向"].Enable = UserAcl.Current["Class_Button_Export_GraduateSurveyVagrant"].Executable;
            MotherForm.RibbonBarItems["班級", "資料統計"]["匯出"]["匯出未升學未就業畢業學生動向"].Click += delegate
            {
                List<string> Sources = K12.Presentation.NLDPanels.Class.SelectedSource;

                if (Sources.Count > 0)
                {
                    Export.Vagrant_Export form = new Export.Vagrant_Export();

                    form.ClassIDs = Sources;
                    form.SourceType = "class";
                    form.ShowDialog();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("請先選取班級。");
                }
            };
            #endregion

        #endregion

        #region 報表

            #region 畢業學生進路統計表

            Catalog button_GraduationRequirement = RoleAclSource.Instance["教務作業"]["功能按鈕"];
            button_GraduationRequirement.Add(new RibbonFeature("Senate_Button_Export_GraduateSurveyApproach", "列印畢業學生進路統計表"));

            var templateManager = MotherForm.RibbonBarItems["教務作業", "畢業學生相關調查"]["報表"];
            templateManager.Size = RibbonBarButton.MenuButtonSize.Large;
            templateManager.Image = Properties.Resources.paste_64;
            templateManager["畢業學生進路統計表"].Enable = UserAcl.Current["Senate_Button_Export_GraduateSurveyApproach"].Executable;
            templateManager["畢業學生進路統計表"].Click += delegate
            {
                (new Report.Approach_Report()).ShowDialog();
            };

            #endregion
        #endregion
        }
    }
}
