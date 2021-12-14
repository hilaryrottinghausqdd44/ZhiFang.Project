using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Collections;
using FastReport;
using FastReport.Export.Pdf;
using FastReport.Export.Html;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.Factory;
using ZhiFang.ReportFormQueryPrint.Common;
using System.Runtime.InteropServices;
namespace ZhiFang.ReportFormQueryPrint.BLL.Print
{
    public class BShowRequestFrom 
    {
        protected BALLReportForm arfb = new BALLReportForm();
        protected Model.PrintFormat format = new Model.PrintFormat();
        protected int PrintFormatNo;
        protected readonly IDReportItem item = DalFactory<IDReportItem>.GetDal("ReportItem");
        protected readonly BReportForm ibrf = new BReportForm();
        private readonly ZhiFang.ReportFormQueryPrint.BLL.BRFGraphData brfgd = new BLL.BRFGraphData();
        public SortedList ShowFormTypeList(int SectionType, string PageName)
        {
            SortedList al = new SortedList();
            DataSet ds = UserReportFormDataListShowConfig.ShowFormTypeList("");
            ZhiFang.Common.Log.Log.Debug("BShowFrom.ShowFormTypeList:SectionType=" + ((Common.SectionType)SectionType).ToString().Trim() + ";PageName=" + PageName);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    int tmpi = -1;
                    try
                    {
                        tmpi = (int)Enum.Parse(typeof(Common.SectionType), ds.Tables[0].Rows[i]["ReportType"].ToString().Trim()); //(Common.SectionType)Enum.Parse(typeof(Common.SectionType), ds.Tables[0].Rows[i]["ReportType"].ToString().Trim(), true);// ds.Tables[0].Rows[i]["ReportType"].ToString().Trim()
                    }
                    catch(Exception e)
                    {
                        ZhiFang.Common.Log.Log.Debug("BShowFrom.ShowFormTypeList字符串转枚举异常："+e.ToString());
                    }
                    //ZhiFang.Common.Log.Log.Debug("BShowFrom.ShowFormTypeList.for:SectionTypem=" + ((Common.SectionType)SectionType).ToString().Trim() + ";SectionTypet=" + ds.Tables[0].Rows[i]["XSLName"].ToString().Trim() + ";PageName=" + ds.Tables[0].Rows[i]["PageName"].ToString().Trim());
                    //ZhiFang.Common.Log.Log.Debug("BShowFrom.ShowFormTypeList.for:tmpi=" + tmpi + ";SectionTypei=" + SectionType + ";PageName=" + ds.Tables[0].Rows[i]["PageName"].ToString().Trim());
                    if (tmpi == SectionType && PageName.Trim() == ds.Tables[0].Rows[i]["PageName"].ToString().Trim())
                    {
                        al.Add(ds.Tables[0].Rows[i]["XSLName"].ToString().Trim(), ds.Tables[0].Rows[i]["Name"].ToString().Trim());
                    }
                }
            }
            return al;
           
        }
        /// <summary>
        /// 返回html结果
        /// </summary>
        /// <param name="ReportFormNo">6.6数据库中为四个关键字组合用";"连接，2009数据库中为FORMNO字段</param>
        /// <param name="SectionNo">小组编号</param>
        /// <param name="PageName">xml配置文件中的页面名称</param>
        /// <param name="ShowType">xml配置文件中的页面下的样式名称</param>
        /// <param name="sectiontype">小组类型</param>
        /// <returns></returns>
        public string ShowRequestFormResult(string ReportFormNo, int SectionNo, string PageName, int ShowType,int SectionType)
        {
            try
            {
                ZhiFang.Common.Log.Log.Debug("ShowRequestFormResult");
                string result = string.Empty;
                DataTable dtrf = new DataTable();
                DataTable dtri = new DataTable();
                DataTable dtrg = new DataTable();
                dtri = GetReportFormAndItemData(ReportFormNo, SectionType, out dtrf,out dtrg);
                ZhiFang.Common.Log.Log.Debug("BShowFrom.ShowReportFormResult:dtrf="+dtrf.Rows.Count + "dtri=" + dtri.Rows.Count);
                string modelPath = this.GetTemplatePath(dtrf, dtri, PageName, ShowType, SectionType);
                ZhiFang.Common.Log.Log.Debug("BShowFrom.ShowReportFormResult:modelPath=" + modelPath);
                if (dtrg != null && dtrg.Rows.Count > 0)
                {
                    ZhiFang.Common.Log.Log.Debug("ShowReportFormResult1");
                    result = this.GetResult(dtrf, dtri, dtrg, modelPath);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug("ShowReportFormResult2");
                    result = this.GetResult(dtrf, dtri, modelPath);
                }
                return result;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("BShowFrom.ShowReportFormResult异常信息:"+ex.ToString() );
                return "";
            }
        }
        /// <summary>
        /// 获取模板存放路径
        /// </summary>
        /// <param name="dtrf"></param>
        /// <param name="dtri"></param>
        /// <param name="pageName"></param>
        /// <param name="showType"></param>
        /// <param name="sectiontype"></param>
        /// <returns></returns>
        public string GetTemplatePath(DataTable dtrf, DataTable dtri, string pageName, int showType, int sectiontype)
        {
            ZhiFang.Common.Log.Log.Debug("BShowFrom.GetTemplatePath:pageName=" + pageName+ ";showType=" + showType + ";sectiontype=" + sectiontype);
            string showmodel = "Normal.XSLT";
            string modulePath = "";
            //string tmphtml = "";

            if (sectiontype == 2 || sectiontype == 4)
            {
                if (dtrf.Columns.Contains("STestType") &&dtrf.Rows[0]["STestType"] != null && dtrf.Rows[0]["STestType"].ToString().Trim() != "")
                {
                    if (dtrf.Rows[0]["STestType"].ToString().Trim() == "20")
                    {
                        sectiontype = 2;
                    }
                    if (dtrf.Rows[0]["STestType"].ToString().Trim() == "21")
                    {
                        sectiontype = 2;
                    }
                    if (dtrf.Rows[0]["STestType"].ToString().Trim() == "22")
                    {
                        sectiontype = 1;
                    }
                }
            }

            SortedList al = this.ShowFormTypeList(sectiontype, pageName);
            if (al.Count <= 0)
            {
                return "显示模板配置文件错误！";
            }
            else
            {
                try
                {
                    showmodel = al.GetKey(showType).ToString();
                }
                catch
                {
                    showmodel = al.GetKey(0).ToString();
                }
            }
            ZhiFang.Common.Log.Log.Debug("BShowFrom.GetTemplatePath:showmodel=" + showmodel);
            if (dtrf != null && dtrf.Rows.Count > 0)
            {

                string modelname = "";

                switch (((ZhiFang.ReportFormQueryPrint.Common.SectionType)sectiontype))
                {
                    #region Normal
                    case ZhiFang.ReportFormQueryPrint.Common.SectionType.Normal:
                        {
                            modulePath = ZhiFang.ReportFormQueryPrint.Common.GetFilePath.GetPhysicsFilePath(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("ModelShowURL")) + showmodel;
                        }
                        break;
                    #endregion

                    #region Micro
                    case ZhiFang.ReportFormQueryPrint.Common.SectionType.Micro:
                        {
                            //if (dtrf.Rows[0]["STestType"] != null && dtrf.Rows[0]["STestType"].ToString().Trim() != "")
                            //{
                            //    if (dtrf.Rows[0]["STestType"].ToString().Trim() == "20")
                            //    {
                            //        modulePath = ZhiFang.ReportFormQueryPrint.Common.GetFilePath.GetPhysicsFilePath(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("ModelShowURL")) + showmodel;
                            //    }
                            //    if (dtrf.Rows[0]["STestType"].ToString().Trim() == "21")
                            //    {
                            //        modulePath = ZhiFang.ReportFormQueryPrint.Common.GetFilePath.GetPhysicsFilePath(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("ModelShowURL")) + showmodel;
                            //    }
                            //    if (dtrf.Rows[0]["STestType"].ToString().Trim() == "22")
                            //    {
                            //        modulePath = ZhiFang.ReportFormQueryPrint.Common.GetFilePath.GetPhysicsFilePath(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("ModelShowURL")) + showmodel;
                            //    }
                            //}
                            //else
                            //{
                                modulePath = ZhiFang.ReportFormQueryPrint.Common.GetFilePath.GetPhysicsFilePath(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("ModelShowURL")) + showmodel;
                            //}
                        }
                        break;
                    #endregion

                    #region TestGroupMicroSmear
                    case ZhiFang.ReportFormQueryPrint.Common.SectionType.TestGroupMicroSmear:
                        {
                            modulePath = ZhiFang.ReportFormQueryPrint.Common.GetFilePath.GetPhysicsFilePath(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("ModelShowURL")) + showmodel;
                        }
                        break;
                    #endregion

                    #region TestGroupMicroSmearExt
                    case ZhiFang.ReportFormQueryPrint.Common.SectionType.TestGroupMicroSmearExt:
                        {
                            modulePath = ZhiFang.ReportFormQueryPrint.Common.GetFilePath.GetPhysicsFilePath(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("ModelShowURL")) + showmodel;
                        }
                        break;
                    #endregion

                    #region TestGroupMicroCultureAssayAntibioticSusceptibility
                    case ZhiFang.ReportFormQueryPrint.Common.SectionType.TestGroupMicroCultureAssayAntibioticSusceptibility:
                        {
                            modulePath = ZhiFang.ReportFormQueryPrint.Common.GetFilePath.GetPhysicsFilePath(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("ModelShowURL")) + showmodel;
                        }
                        break;
                    #endregion

                    #region TestGroupMicroCultureAssayAntibioticSusceptibilityExt
                    case ZhiFang.ReportFormQueryPrint.Common.SectionType.TestGroupMicroCultureAssayAntibioticSusceptibilityExt:
                        {
                            modulePath = ZhiFang.ReportFormQueryPrint.Common.GetFilePath.GetPhysicsFilePath(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("ModelShowURL")) + showmodel;
                        }
                        break;
                    #endregion

                    #region TestGroupMicroOtherTest
                    case ZhiFang.ReportFormQueryPrint.Common.SectionType.TestGroupMicroOtherTest:
                        {
                            modulePath = ZhiFang.ReportFormQueryPrint.Common.GetFilePath.GetPhysicsFilePath(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("ModelShowURL")) + showmodel;
                        }
                        break;
                    #endregion

                    #region NormalIncImage
                    case ZhiFang.ReportFormQueryPrint.Common.SectionType.NormalIncImage:
                        {
                            modulePath = ZhiFang.ReportFormQueryPrint.Common.GetFilePath.GetPhysicsFilePath(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("ModelShowURL")) + showmodel;
                        }
                        break;
                    #endregion

                    #region MicroIncImage
                    case ZhiFang.ReportFormQueryPrint.Common.SectionType.MicroIncImage:
                        {
                            modulePath = ZhiFang.ReportFormQueryPrint.Common.GetFilePath.GetPhysicsFilePath(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("ModelShowURL")) + showmodel;
                        }
                        break;
                    #endregion

                    #region CellMorphology
                        {
                            modulePath = ZhiFang.ReportFormQueryPrint.Common.GetFilePath.GetPhysicsFilePath(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("ModelShowURL")) + showmodel;
                        }
                        break;
                        #endregion

                    #region FishCheck
                    case ZhiFang.ReportFormQueryPrint.Common.SectionType.FishCheck:
                        {
                            modulePath = ZhiFang.ReportFormQueryPrint.Common.GetFilePath.GetPhysicsFilePath(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("ModelShowURL")) + showmodel;
                        }
                        break;
                    #endregion

                    #region SensorCheck
                    case ZhiFang.ReportFormQueryPrint.Common.SectionType.SensorCheck:
                        {
                            modulePath = ZhiFang.ReportFormQueryPrint.Common.GetFilePath.GetPhysicsFilePath(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("ModelShowURL")) + showmodel;
                        }
                        break;
                    #endregion

                    #region ChromosomeCheck
                    case ZhiFang.ReportFormQueryPrint.Common.SectionType.ChromosomeCheck:
                        {
                            modulePath = ZhiFang.ReportFormQueryPrint.Common.GetFilePath.GetPhysicsFilePath(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("ModelShowURL")) + showmodel;
                        }
                        break;
                    #endregion

                    #region PathologyCheck
                    case ZhiFang.ReportFormQueryPrint.Common.SectionType.PathologyCheck:
                        {
                            modulePath = ZhiFang.ReportFormQueryPrint.Common.GetFilePath.GetPhysicsFilePath(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("ModelShowURL")) + showmodel;
                        }
                        break;
                    default:
                        {
                            modulePath = ZhiFang.ReportFormQueryPrint.Common.GetFilePath.GetPhysicsFilePath(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("ModelShowURL")) + showmodel;
                        }
                        break;
                    #endregion
                }

            }

            return modulePath;
        }
        /// <summary>
        /// 获取报告单、报告子项数据
        /// </summary>
        /// <param name="FormNo"></param>
        /// <param name="sectiontype"></param>
        /// <returns></returns>
        public DataTable GetReportFormAndItemData(string FormNo, int sectiontype, out DataTable dsrf_Out,out DataTable dtg_Out)
        {
            BRequestForm rffb = new BRequestForm();
            BRequestItem rifb = new BRequestItem();
            BRequestMicro rmfb = new BRequestMicro();
            BRequestMarrow rmarrowfb = new BRequestMarrow();

            Model.RequestForm rff_m = new Model.RequestForm();
            Model.RequestItem rif_m = new Model.RequestItem();
            Model.RequestMicro rmf_m = new Model.RequestMicro();
            Model.RequestMarrow rmarrowf_m = new Model.RequestMarrow();
           // DataSet dsri=new DataSet();
            DataTable dtrf = new DataTable("frform");
            dtrf = rffb.GetListByDataSource(FormNo);
            DataTable dtri = new DataTable();
            dtg_Out = null;
            if (dtrf.Rows.Count > 0)
            {                
                dtrf.TableName = "frform";
                switch (((ZhiFang.ReportFormQueryPrint.Common.SectionType)sectiontype))
                {
                    case ZhiFang.ReportFormQueryPrint.Common.SectionType.Normal:
                        {
                            #region Normal
                            try
                            {
                                dtri = rifb.GetRequestItemList_DataTable(FormNo);
                            }
                            catch (Exception ex)
                            {
                                ZhiFang.Common.Log.Log.Debug("GetReportFormAndItemData.Normal异常信息:" + ex.ToString());

                            }
                            break;
                            #endregion
                        }
                    case ZhiFang.ReportFormQueryPrint.Common.SectionType.Micro:
                        {
                            #region Micro
                            try
                            {
                                //dsri = rmfb.GetList(rmf_m);
                                if (dtrf.Columns.Contains("STestType"))
                                {
                                    if (dtrf.Rows[0]["STestType"] != null && dtrf.Rows[0]["STestType"].ToString().Trim() != "")
                                    {
                                        if (dtrf.Rows[0]["STestType"].ToString().Trim() == "20")
                                        {
                                            dtri = rmfb.GetRequestMicroGroupListForSTestType(FormNo);
                                        }
                                        if (dtrf.Rows[0]["STestType"].ToString().Trim() == "21")
                                        {
                                            dtri = rmfb.GetRequestMicroGroupListForSTestType(FormNo);
                                        }
                                        if (dtrf.Rows[0]["STestType"].ToString().Trim() == "22")
                                        {
                                            dtri = rifb.GetRequestItemList_DataTable(FormNo);
                                        }
                                    }
                                    else
                                    {
                                        dtri = rmfb.GetRequestMicroGroupList(FormNo);
                                    }
                                }
                                else
                                {
                                    dtri = rmfb.GetRequestMicroGroupList(FormNo);
                                }
                            }
                            catch (Exception ex)
                            {
                                ZhiFang.Common.Log.Log.Debug("GetReportFormAndItemData.Micro异常信息:" + ex.ToString());

                            }
                            break;
                            #endregion
                        }
                    case ZhiFang.ReportFormQueryPrint.Common.SectionType.TestGroupMicroSmear:
                        {
                            #region TestGroupMicroSmear
                            try
                            {
                                //dsri = rmfb.GetList(rmf_m);
                                dtri = rmfb.GetRequestMicroGroupList(FormNo);
                            }
                            catch (Exception ex)
                            {
                                ZhiFang.Common.Log.Log.Debug("GetReportFormAndItemData.Micro异常信息:" + ex.ToString());

                            }
                            break;
                            #endregion
                        }
                    case ZhiFang.ReportFormQueryPrint.Common.SectionType.TestGroupMicroSmearExt:
                        {
                            #region TestGroupMicroSmearExt
                            try
                            {
                                //dsri = rmfb.GetList(rmf_m);
                                dtri = rmfb.GetRequestMicroGroupList(FormNo);
                            }
                            catch (Exception ex)
                            {
                                ZhiFang.Common.Log.Log.Debug("GetReportFormAndItemData.Micro异常信息:" + ex.ToString());

                            }
                            break;
                            #endregion
                        }
                    case ZhiFang.ReportFormQueryPrint.Common.SectionType.TestGroupMicroCultureAssayAntibioticSusceptibility:
                        {
                            #region TestGroupMicroCultureAssayAntibioticSusceptibility
                            try
                            {
                                //dsri = rmfb.GetList(rmf_m);
                                dtri = rmfb.GetRequestMicroGroupList(FormNo);
                            }
                            catch (Exception ex)
                            {
                                ZhiFang.Common.Log.Log.Debug("GetReportFormAndItemData.Micro异常信息:" + ex.ToString());

                            }
                            break;
                            #endregion
                        }
                    case ZhiFang.ReportFormQueryPrint.Common.SectionType.TestGroupMicroCultureAssayAntibioticSusceptibilityExt:
                        {
                            #region TestGroupMicroCultureAssayAntibioticSusceptibilityExt
                            try
                            {
                                //dsri = rmfb.GetList(rmf_m);
                                dtri = rmfb.GetRequestMicroGroupList(FormNo);
                            }
                            catch (Exception ex)
                            {
                                ZhiFang.Common.Log.Log.Debug("GetReportFormAndItemData.Micro异常信息:" + ex.ToString());
                            }
                            break;
                            #endregion
                        }
                    case ZhiFang.ReportFormQueryPrint.Common.SectionType.TestGroupMicroOtherTest:
                        {
                            #region Micro
                            try
                            {
                                //dsri = rmfb.GetList(rmf_m);
                                dtri = rmfb.GetRequestMicroGroupList(FormNo);
                            }
                            catch (Exception ex)
                            {
                                ZhiFang.Common.Log.Log.Debug("GetReportFormAndItemData.Micro异常信息:" + ex.ToString());

                            }
                            break;
                            #endregion
                        }
                    case ZhiFang.ReportFormQueryPrint.Common.SectionType.NormalIncImage:
                        {
                            #region NormalIncImage
                            try
                            {
                                dtri = rifb.GetRequestItemList_DataTable(FormNo);
                                DataSet tmpdsimages= brfgd.GetListByReportPublicationID(FormNo);
                                if (tmpdsimages != null && tmpdsimages.Tables.Count>0)
                                    dtg_Out = tmpdsimages.Tables[0];
                                if (dtg_Out != null && dtg_Out.Rows.Count > 0)
                                {                                    
                                    dtg_Out.Columns.Add("Base64StrContent");
                                    for (int i = 0; i < dtg_Out.Rows.Count; i++)
                                    {
                                        ZhiFang.Common.Log.Log.Debug("GetReportFormAndItemData,pointtype:" + dtg_Out.Rows[i]["pointtype"].ToString().Trim());
                                        if (dtg_Out.Rows[i]["pointtype"] != null && dtg_Out.Rows[i]["pointtype"].ToString().Trim() == "8")
                                        {

                                        }
                                        else
                                        {
                                            if (dtg_Out.Rows[i]["FilePath"] != null && dtg_Out.Rows[i]["FilePath"].ToString().Trim() != "")
                                            {

                                                string CurrentUploadFilePath = dtg_Out.Rows[i]["FilePath"].ToString().Trim();
                                                if (File.Exists(CurrentUploadFilePath))
                                                {
                                                    try
                                                    {
                                                        dtg_Out.Rows[i]["Base64StrContent"] = Base64Help.EncodingFileToString(dtg_Out.Rows[i]["FilePath"].ToString().Trim());
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        ZhiFang.Common.Log.Log.Error("DownloadReportFormImageFile:" + ex.ToString());
                                                        dtg_Out.Rows[i]["Base64StrContent"] = "";
                                                    }
                                                }
                                                else
                                                {
                                                    dtg_Out.Rows[i]["Base64StrContent"] = "";
                                                    ZhiFang.Common.Log.Log.Debug("DownloadReportFormImageFile:未找到图片文件（" + CurrentUploadFilePath + ")");
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    ZhiFang.Common.Log.Log.Debug("GetReportFormAndItemData.DownloadReportFormImageFile:未找到报告图片列表ReportFormId：" + FormNo);
                                }
                            }
                            catch (Exception ex)
                            {
                                ZhiFang.Common.Log.Log.Debug("GetReportFormAndItemData.NormalIncImage异常信息:" + ex.ToString());

                            }
                            break;
                            #endregion
                        }
                    case ZhiFang.ReportFormQueryPrint.Common.SectionType.MicroIncImage:
                        {
                            #region MicroIncImage
                            //dsri = rmfb.GetList(rmf_m);
                            try
                            {
                            dtri = rmfb.GetRequestMicroGroupList(FormNo);
                            }
                            catch (Exception ex)
                            {
                                ZhiFang.Common.Log.Log.Debug("GetReportFormAndItemData.MicroIncImage异常信息:" + ex.ToString());

                            }
                            break;
                            #endregion
                        }
                    case ZhiFang.ReportFormQueryPrint.Common.SectionType.CellMorphology:
                        {
                            #region CellMorphology
                            try
                            {
                            dtri = rmarrowfb.GetRequestItemList_DataTable(FormNo);
                            }
                            catch (Exception ex)
                            {
                                ZhiFang.Common.Log.Log.Debug("GetReportFormAndItemData.CellMorphology异常信息:" + ex.ToString());

                            }
                            break;
                            #endregion
                        }
                    case ZhiFang.ReportFormQueryPrint.Common.SectionType.FishCheck:
                        {
                            #region FishCheck
                            try
                            {
                                dtri = rmarrowfb.GetRequestItemList_DataTable(FormNo);
                            }
                            catch (Exception ex)
                            {
                                ZhiFang.Common.Log.Log.Debug("GetReportFormAndItemData.FishCheck异常信息:" + ex.ToString());

                            }
                            break;
                            #endregion
                        }
                    case ZhiFang.ReportFormQueryPrint.Common.SectionType.SensorCheck:
                        {
                            #region SensorCheck
                            try
                            {
                                dtri = rmarrowfb.GetRequestItemList_DataTable(FormNo);
                            }
                            catch (Exception ex)
                            {
                                ZhiFang.Common.Log.Log.Debug("GetReportFormAndItemData.SensorCheck异常信息:" + ex.ToString());

                            }
                            break;
                            #endregion
                        }
                    case ZhiFang.ReportFormQueryPrint.Common.SectionType.ChromosomeCheck:
                        {
                            #region ChromosomeCheck
                            try
                            {
                                dtri = rmarrowfb.GetRequestItemList_DataTable(FormNo);
                            }
                            catch (Exception ex)
                            {
                                ZhiFang.Common.Log.Log.Debug("GetReportFormAndItemData.ChromosomeCheck异常信息:" + ex.ToString());

                            }
                            break;
                            #endregion
                        }
                    case ZhiFang.ReportFormQueryPrint.Common.SectionType.PathologyCheck:
                        {
                            #region PathologyCheck
                            try
                            {
                                dtri = rmarrowfb.GetRequestItemList_DataTable(FormNo);
                            }
                            catch (Exception ex)
                            {
                                ZhiFang.Common.Log.Log.Debug("GetReportFormAndItemData.PathologyCheck异常信息:" + ex.ToString());

                            }
                            break;
                            #endregion
                        }
                    default:
                        {
                            try
                            {
                                dtri = rifb.GetRequestItemList_DataTable(FormNo);
                            }
                            catch (Exception ex)
                            {
                                ZhiFang.Common.Log.Log.Debug("GetReportFormAndItemData.default异常信息:" + ex.ToString());

                            }
                            break;
                        }
                }
            }
            dsrf_Out = dtrf;
            return dtri;
        }
        /// <summary>
        /// 根据数据和模板地址，获取html结果
        /// </summary>
        /// <param name="dtrf"></param>
        /// <param name="dtri"></param>
        /// <param name="templatePath"></param>
        /// <returns></returns>
        public string GetResult(DataTable dtrf, DataTable dtri, string templatePath)
        {
            string tempHtml = "";
            string templateName = getTemplateName(templatePath);
            PrintReportFormCommon prfc = new PrintReportFormCommon();
            if (System.IO.Path.GetExtension(templateName).ToUpper() == ".FRX")
            {
                ZhiFang.Common.Log.Log.Debug("ZhiFang.ReportFormQueryPrint.BLL.Print.BShowRequestFrom.GetResult.frx");
                tempHtml = prfc.CreatShowContextByFRX(dtrf, dtri, templatePath);
            }
            else if (System.IO.Path.GetExtension(templateName).ToUpper() == ".XSLT")
            {
                ZhiFang.Common.Log.Log.Debug("ZhiFang.ReportFormQueryPrint.BLL.Print.BShowRequestFrom.GetResult.XSLT");
                tempHtml = prfc.CreatHtmlContextXslt(dtrf, dtri, templatePath);
            }
            return tempHtml;
        }
        public string GetResult(DataTable dtrf, DataTable dtri, DataTable dtrg, string templatePath)
        {
            string tempHtml = "";
            string templateName = getTemplateName(templatePath);
            PrintReportFormCommon prfc = new PrintReportFormCommon();
            if (System.IO.Path.GetExtension(templateName).ToUpper() == ".FRX")
            {
                tempHtml = prfc.CreatShowContextByFRX(dtrf, dtri, dtrg, templatePath);
            }
            else if (System.IO.Path.GetExtension(templateName).ToUpper() == ".XSLT")
            {
                tempHtml = prfc.CreatHtmlContextXslt(dtrf, dtri, dtrg, templatePath);
            }
            return tempHtml;
        }
        private string getTemplateName(string templatePath)
        {
            if (templatePath == null || templatePath == "")
                return "";
            string[] tempArr = templatePath.Split('\\');
            string templateName = tempArr[tempArr.Length - 1];
            return templateName;

        }

        /// <summary>
        /// 返回html结果
        /// </summary>
        /// <param name="ReportFormNo">6.6数据库中为四个关键字组合用";"连接，2009数据库中为FORMNO字段</param>
        /// <param name="SectionNo">小组编号</param>
        /// <param name="PageName">xml配置文件中的页面名称</param>
        /// <param name="ShowType">xml配置文件中的页面下的样式名称</param>
        /// <param name="sectiontype">小组类型</param>
        /// <returns></returns>
        public string ShowRequestFormResultByReportTemp(string ReportFormNo, int SectionNo, string PageName, int ShowType, int SectionType)
        {
            try
            {
                ZhiFang.Common.Log.Log.Debug("ShowRequestFormResult");
                string result = string.Empty;
                DataTable dtrf = new DataTable();
                DataTable dtri = new DataTable();
                DataTable dtrg = new DataTable();
                dtri = GetReportFormAndItemDataByReportTemp(ReportFormNo, SectionType, out dtrf, out dtrg);
                ZhiFang.Common.Log.Log.Debug("BShowFrom.ShowReportFormResult:dtrf=" + dtrf.Rows.Count + "dtri=" + dtri.Rows.Count);
                string modelPath = this.GetTemplatePath(dtrf, dtri, PageName, ShowType, SectionType);
                ZhiFang.Common.Log.Log.Debug("BShowFrom.ShowReportFormResult:modelPath=" + modelPath);
                if (dtrg != null && dtrg.Rows.Count > 0)
                {
                    ZhiFang.Common.Log.Log.Debug("ShowReportFormResult1");
                    result = this.GetResult(dtrf, dtri, dtrg, modelPath);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug("ShowReportFormResult2");
                    result = this.GetResult(dtrf, dtri, modelPath);
                }
                return result;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("BShowFrom.ShowReportFormResult异常信息:" + ex.ToString());
                return "";
            }
        }

        public DataTable GetReportFormAndItemDataByReportTemp(string FormNo, int sectiontype, out DataTable dsrf_Out, out DataTable dtg_Out)
        {
            BRequestForm rffb = new BRequestForm();
            BRequestItem rifb = new BRequestItem();
            BRequestMicro rmfb = new BRequestMicro();
            BRequestMarrow rmarrowfb = new BRequestMarrow();

            Model.RequestForm rff_m = new Model.RequestForm();
            Model.RequestItem rif_m = new Model.RequestItem();
            Model.RequestMicro rmf_m = new Model.RequestMicro();
            Model.RequestMarrow rmarrowf_m = new Model.RequestMarrow();
            // DataSet dsri=new DataSet();
            DataTable dtrf = new DataTable("frform");
            dtrf = rffb.GetListByDataSource(FormNo);
            DataTable dtri = new DataTable();
            dtg_Out = null;
            if (dtrf.Rows.Count > 0)
            {
                dtrf.TableName = "frform";
                switch (((ZhiFang.ReportFormQueryPrint.Common.SectionType)sectiontype))
                {
                    case ZhiFang.ReportFormQueryPrint.Common.SectionType.Normal:
                        {
                            #region Normal
                            try
                            {
                                dtri = rifb.GetRequestItemList_DataTableByReportTemp(FormNo);
                            }
                            catch (Exception ex)
                            {
                                ZhiFang.Common.Log.Log.Debug("GetReportFormAndItemData.Normal异常信息:" + ex.ToString());

                            }
                            break;
                            #endregion
                        }
                    case ZhiFang.ReportFormQueryPrint.Common.SectionType.Micro:
                        {
                            #region Micro
                            try
                            {
                                //dsri = rmfb.GetList(rmf_m);
                                if (dtrf.Columns.Contains("STestType"))
                                {
                                    if (dtrf.Rows[0]["STestType"] != null && dtrf.Rows[0]["STestType"].ToString().Trim() != "")
                                    {
                                        if (dtrf.Rows[0]["STestType"].ToString().Trim() == "20")
                                        {
                                            dtri = rmfb.GetRequestMicroGroupListForSTestType(FormNo);
                                        }
                                        if (dtrf.Rows[0]["STestType"].ToString().Trim() == "21")
                                        {
                                            dtri = rmfb.GetRequestMicroGroupListForSTestType(FormNo);
                                        }
                                        if (dtrf.Rows[0]["STestType"].ToString().Trim() == "22")
                                        {
                                            dtri = rifb.GetRequestItemList_DataTableByReportTemp(FormNo);
                                        }
                                    }
                                    else
                                    {
                                        dtri = rmfb.GetRequestMicroGroupList(FormNo);
                                    }
                                }
                                else
                                {
                                    dtri = rmfb.GetRequestMicroGroupList(FormNo);
                                }
                            }
                            catch (Exception ex)
                            {
                                ZhiFang.Common.Log.Log.Debug("GetReportFormAndItemData.Micro异常信息:" + ex.ToString());

                            }
                            break;
                            #endregion
                        }
                    case ZhiFang.ReportFormQueryPrint.Common.SectionType.TestGroupMicroSmear:
                        {
                            #region TestGroupMicroSmear
                            try
                            {
                                //dsri = rmfb.GetList(rmf_m);
                                dtri = rmfb.GetRequestMicroGroupList(FormNo);
                            }
                            catch (Exception ex)
                            {
                                ZhiFang.Common.Log.Log.Debug("GetReportFormAndItemData.Micro异常信息:" + ex.ToString());

                            }
                            break;
                            #endregion
                        }
                    case ZhiFang.ReportFormQueryPrint.Common.SectionType.TestGroupMicroSmearExt:
                        {
                            #region TestGroupMicroSmearExt
                            try
                            {
                                //dsri = rmfb.GetList(rmf_m);
                                dtri = rmfb.GetRequestMicroGroupList(FormNo);
                            }
                            catch (Exception ex)
                            {
                                ZhiFang.Common.Log.Log.Debug("GetReportFormAndItemData.Micro异常信息:" + ex.ToString());

                            }
                            break;
                            #endregion
                        }
                    case ZhiFang.ReportFormQueryPrint.Common.SectionType.TestGroupMicroCultureAssayAntibioticSusceptibility:
                        {
                            #region TestGroupMicroCultureAssayAntibioticSusceptibility
                            try
                            {
                                //dsri = rmfb.GetList(rmf_m);
                                dtri = rmfb.GetRequestMicroGroupList(FormNo);
                            }
                            catch (Exception ex)
                            {
                                ZhiFang.Common.Log.Log.Debug("GetReportFormAndItemData.Micro异常信息:" + ex.ToString());

                            }
                            break;
                            #endregion
                        }
                    case ZhiFang.ReportFormQueryPrint.Common.SectionType.TestGroupMicroCultureAssayAntibioticSusceptibilityExt:
                        {
                            #region TestGroupMicroCultureAssayAntibioticSusceptibilityExt
                            try
                            {
                                //dsri = rmfb.GetList(rmf_m);
                                dtri = rmfb.GetRequestMicroGroupList(FormNo);
                            }
                            catch (Exception ex)
                            {
                                ZhiFang.Common.Log.Log.Debug("GetReportFormAndItemData.Micro异常信息:" + ex.ToString());
                            }
                            break;
                            #endregion
                        }
                    case ZhiFang.ReportFormQueryPrint.Common.SectionType.TestGroupMicroOtherTest:
                        {
                            #region Micro
                            try
                            {
                                //dsri = rmfb.GetList(rmf_m);
                                dtri = rmfb.GetRequestMicroGroupList(FormNo);
                            }
                            catch (Exception ex)
                            {
                                ZhiFang.Common.Log.Log.Debug("GetReportFormAndItemData.Micro异常信息:" + ex.ToString());

                            }
                            break;
                            #endregion
                        }
                    case ZhiFang.ReportFormQueryPrint.Common.SectionType.NormalIncImage:
                        {
                            #region NormalIncImage
                            try
                            {
                                dtri = rifb.GetRequestItemList_DataTableByReportTemp(FormNo);
                                DataSet tmpdsimages = brfgd.GetListByReportPublicationID(FormNo);
                                if (tmpdsimages != null && tmpdsimages.Tables.Count > 0)
                                    dtg_Out = tmpdsimages.Tables[0];
                                if (dtg_Out != null && dtg_Out.Rows.Count > 0)
                                {
                                    dtg_Out.Columns.Add("Base64StrContent");
                                    for (int i = 0; i < dtg_Out.Rows.Count; i++)
                                    {
                                        ZhiFang.Common.Log.Log.Debug("GetReportFormAndItemData,pointtype:" + dtg_Out.Rows[i]["pointtype"].ToString().Trim());
                                        if (dtg_Out.Rows[i]["pointtype"] != null && dtg_Out.Rows[i]["pointtype"].ToString().Trim() == "8")
                                        {

                                        }
                                        else
                                        {
                                            if (dtg_Out.Rows[i]["FilePath"] != null && dtg_Out.Rows[i]["FilePath"].ToString().Trim() != "")
                                            {

                                                string CurrentUploadFilePath = dtg_Out.Rows[i]["FilePath"].ToString().Trim();
                                                if (File.Exists(CurrentUploadFilePath))
                                                {
                                                    try
                                                    {
                                                        dtg_Out.Rows[i]["Base64StrContent"] = Base64Help.EncodingFileToString(dtg_Out.Rows[i]["FilePath"].ToString().Trim());
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        ZhiFang.Common.Log.Log.Error("DownloadReportFormImageFile:" + ex.ToString());
                                                        dtg_Out.Rows[i]["Base64StrContent"] = "";
                                                    }
                                                }
                                                else
                                                {
                                                    dtg_Out.Rows[i]["Base64StrContent"] = "";
                                                    ZhiFang.Common.Log.Log.Debug("DownloadReportFormImageFile:未找到图片文件（" + CurrentUploadFilePath + ")");
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    ZhiFang.Common.Log.Log.Debug("GetReportFormAndItemData.DownloadReportFormImageFile:未找到报告图片列表ReportFormId：" + FormNo);
                                }
                            }
                            catch (Exception ex)
                            {
                                ZhiFang.Common.Log.Log.Debug("GetReportFormAndItemData.NormalIncImage异常信息:" + ex.ToString());

                            }
                            break;
                            #endregion
                        }
                    case ZhiFang.ReportFormQueryPrint.Common.SectionType.MicroIncImage:
                        {
                            #region MicroIncImage
                            //dsri = rmfb.GetList(rmf_m);
                            try
                            {
                                dtri = rmfb.GetRequestMicroGroupList(FormNo);
                            }
                            catch (Exception ex)
                            {
                                ZhiFang.Common.Log.Log.Debug("GetReportFormAndItemData.MicroIncImage异常信息:" + ex.ToString());

                            }
                            break;
                            #endregion
                        }
                    case ZhiFang.ReportFormQueryPrint.Common.SectionType.CellMorphology:
                        {
                            #region CellMorphology
                            try
                            {
                                dtri = rmarrowfb.GetRequestItemList_DataTable(FormNo);
                            }
                            catch (Exception ex)
                            {
                                ZhiFang.Common.Log.Log.Debug("GetReportFormAndItemData.CellMorphology异常信息:" + ex.ToString());

                            }
                            break;
                            #endregion
                        }
                    case ZhiFang.ReportFormQueryPrint.Common.SectionType.FishCheck:
                        {
                            #region FishCheck
                            try
                            {
                                dtri = rmarrowfb.GetRequestItemList_DataTable(FormNo);
                            }
                            catch (Exception ex)
                            {
                                ZhiFang.Common.Log.Log.Debug("GetReportFormAndItemData.FishCheck异常信息:" + ex.ToString());

                            }
                            break;
                            #endregion
                        }
                    case ZhiFang.ReportFormQueryPrint.Common.SectionType.SensorCheck:
                        {
                            #region SensorCheck
                            try
                            {
                                dtri = rmarrowfb.GetRequestItemList_DataTable(FormNo);
                            }
                            catch (Exception ex)
                            {
                                ZhiFang.Common.Log.Log.Debug("GetReportFormAndItemData.SensorCheck异常信息:" + ex.ToString());

                            }
                            break;
                            #endregion
                        }
                    case ZhiFang.ReportFormQueryPrint.Common.SectionType.ChromosomeCheck:
                        {
                            #region ChromosomeCheck
                            try
                            {
                                dtri = rmarrowfb.GetRequestItemList_DataTable(FormNo);
                            }
                            catch (Exception ex)
                            {
                                ZhiFang.Common.Log.Log.Debug("GetReportFormAndItemData.ChromosomeCheck异常信息:" + ex.ToString());

                            }
                            break;
                            #endregion
                        }
                    case ZhiFang.ReportFormQueryPrint.Common.SectionType.PathologyCheck:
                        {
                            #region PathologyCheck
                            try
                            {
                                dtri = rmarrowfb.GetRequestItemList_DataTable(FormNo);
                            }
                            catch (Exception ex)
                            {
                                ZhiFang.Common.Log.Log.Debug("GetReportFormAndItemData.PathologyCheck异常信息:" + ex.ToString());

                            }
                            break;
                            #endregion
                        }
                    default:
                        {
                            try
                            {
                                dtri = rifb.GetRequestItemList_DataTableByReportTemp(FormNo);
                            }
                            catch (Exception ex)
                            {
                                ZhiFang.Common.Log.Log.Debug("GetReportFormAndItemData.default异常信息:" + ex.ToString());

                            }
                            break;
                        }
                }
            }
            dsrf_Out = dtrf;
            return dtri;
        }
    }
}
