using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.IBLL.Report;
using ZhiFang.Common.Public;
using System.Collections;
using ZhiFang.Common.Log;
using ZhiFang.BLL.Report.Print;

namespace ZhiFang.BLL.Report
{
    public class ShowReport : IBLL.Report.IBShowReport
    {

        protected ALLReportForm arfb = new ALLReportForm();
        protected readonly ZhiFang.IBLL.Report.IBUserReportFormDataListShowConfig iburfdlsc = ZhiFang.BLLFactory.BLLFactory<IBUserReportFormDataListShowConfig>.GetBLL("UserReportFormDataListShowConfig");
        /// <summary>
        /// 获取报告单、报告子项数据
        /// </summary>
        /// <param name="fromNo"></param>
        /// <param name="sectiontype"></param>
        /// <returns></returns>
        public DataTable GetReportFormAndItemData(string fromNo, int sectiontype, out DataTable dsrf_Out)
        {
            ZhiFang.IBLL.Report.IBReportFormFull rffb = ZhiFang.BLLFactory.BLLFactory<IBReportFormFull>.GetBLL("ReportFormFull");
            ZhiFang.IBLL.Report.IBReportItemFull rifb = ZhiFang.BLLFactory.BLLFactory<IBReportItemFull>.GetBLL("ReportItemFull");
            ZhiFang.IBLL.Report.IBReportMicroFull rmfb = ZhiFang.BLLFactory.BLLFactory<IBReportMicroFull>.GetBLL("ReportMicroFull");
            ZhiFang.IBLL.Report.IBReportMarrowFull rmarrowfb = ZhiFang.BLLFactory.BLLFactory<IBReportMarrowFull>.GetBLL("ReportMarrowFull");

            Model.ReportFormFull rff_m = new Model.ReportFormFull();
            Model.ReportItemFull rif_m = new Model.ReportItemFull();
            Model.ReportMicroFull rmf_m = new Model.ReportMicroFull();
            Model.ReportMarrowFull rmarrowf_m = new Model.ReportMarrowFull();
            DataSet dsri;
            rff_m.ReportFormID = fromNo;
            //DataSet dsrf = rffb.GetList(rff_m);
            BLL.Report.ReportFormFull rff = new ReportFormFull();
            DataSet dsrf = rff.GetListByView(rff_m);

            DataTable dtrf = new DataTable("frform");
            DataTable dtri = new DataTable();

            if (dsrf.Tables.Count > 0 && dsrf.Tables[0].Rows.Count > 0)
            {
                dtrf = dsrf.Tables[0];
                dtrf.TableName = "frform";
                switch (((ZhiFang.Common.Dictionary.SectionType)sectiontype))
                {
                    case ZhiFang.Common.Dictionary.SectionType.Normal:
                        {
                            #region Normal
                            rif_m.ReportFormID = fromNo;
                            dsri = rifb.GetList(rif_m);
                            if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                            {
                                dtri = dsri.Tables[0];
                            }
                            break;
                            #endregion
                        }
                    case ZhiFang.Common.Dictionary.SectionType.Micro:
                        {
                            #region Micro
                            rmf_m.ReportFormID = fromNo;
                            //dsri = rmfb.GetList(rmf_m);
                            dsri = rmfb.GetReportMicroGroupList(fromNo);
                            if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                            {
                                dtri = dsri.Tables[0];
                            }
                            

                            break;
                            #endregion
                        }
                    case ZhiFang.Common.Dictionary.SectionType.NormalIncImage:
                        {
                            #region NormalIncImage
                            try
                            {
                                rif_m.ReportFormID = fromNo;
                                dsri = rifb.GetList(rif_m);
                                if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                                {
                                    dtri = dsri.Tables[0];
                                }
                            }
                            catch (Exception ex)
                            {
                                ZhiFang.Common.Log.Log.Debug("NormalIncImage异常信息:" + ex.ToString());

                            }
                            break;
                            #endregion
                        }
                    case ZhiFang.Common.Dictionary.SectionType.MicroIncImage:
                        {
                            #region MicroIncImage
                            rmf_m.ReportFormID = fromNo;
                            dsri = rmfb.GetList(rmf_m);
                            if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                            {
                                dtri = dsri.Tables[0];
                            }
                            break;
                            #endregion
                        }
                    case ZhiFang.Common.Dictionary.SectionType.CellMorphology:
                        {
                            #region CellMorphology
                            rmarrowf_m.ReportFormID = fromNo;
                            dsri = rmarrowfb.GetList(rmarrowf_m);
                            if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                            {
                                dtri = dsri.Tables[0];
                            }

                            break;
                            #endregion
                        }
                    case ZhiFang.Common.Dictionary.SectionType.FishCheck:
                        {
                            #region FishCheck
                            rmarrowf_m.ReportFormID = fromNo;
                            dsri = rmarrowfb.GetList(rmarrowf_m);
                            if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                            {
                                dtri = dsri.Tables[0];
                            }

                            break;
                            #endregion
                        }
                    case ZhiFang.Common.Dictionary.SectionType.SensorCheck:
                        {
                            #region SensorCheck
                            rif_m.ReportFormID = fromNo;
                            dsri = rifb.GetList(rif_m);
                            if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                            {
                                dtri = dsri.Tables[0];
                            }

                            break;
                            #endregion
                        }
                    case ZhiFang.Common.Dictionary.SectionType.ChromosomeCheck:
                        {
                            #region ChromosomeCheck
                            rmarrowf_m.ReportFormID = fromNo;
                            dsri = rmarrowfb.GetList(rmarrowf_m);
                            if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                            {
                                dtri = dsri.Tables[0];
                            }
                            break;
                            #endregion
                        }
                    case ZhiFang.Common.Dictionary.SectionType.PathologyCheck:
                        {
                            #region PathologyCheck
                            rmarrowf_m.ReportFormID = fromNo;
                            dsri = rmarrowfb.GetList(rmarrowf_m);
                            if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                            {
                                dtri = dsri.Tables[0];
                            }

                            break;
                            #endregion
                        }
                    default:
                        {
                            DataTable Fdt = arfb.GetFromInfo(fromNo);
                            if (Fdt.Rows.Count > 0)
                            {
                                DataTable Idt = arfb.GetFromItemList(fromNo);
                                dtri = Idt;
                            }
                            break;
                        }
                }
            }
            dsrf_Out = dtrf;
            return dtri;
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

            string showmodel = "Normal.XSLT";
            string modulePath = "";
            //string tmphtml = "";
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

            if (dtrf != null && dtrf.Rows.Count > 0)
            {

                string modelname = "";

                switch (((ZhiFang.Common.Dictionary.SectionType)sectiontype))
                {
                    #region Normal
                    case ZhiFang.Common.Dictionary.SectionType.Normal:

                        modelname = ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelShowURL") + "\\" + showmodel);
                        modulePath = modelname.Replace("//", "");
                        //modulePath = modulePath.Replace("//", "\\");
                        //tmphtml = PrintReportFormCommon.CreatHtmlContextNormal(dtrf, dtri, modulePath);
                        break;
                    #endregion

                    #region Micro
                    case ZhiFang.Common.Dictionary.SectionType.Micro:
                        {
                            modelname = ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelShowURL") + "\\" + showmodel);
                            modulePath = modelname.Replace("//", "");
                            //modulePath = modulePath.Replace("//", "\\");
                            //tmphtml = PrintReportFormCommon.CreatHtmlContextMicro(dtrf, dtri, modulePath);
                            break;
                        }
                    #endregion

                    #region NormalIncImage
                    case ZhiFang.Common.Dictionary.SectionType.NormalIncImage:
                        {
                            modelname = ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelShowURL") + "\\" + showmodel);
                            modulePath = modelname.Replace("//", "");
                            //modulePath = modulePath.Replace("\\", "\\");
                            //tmphtml=PrintReportFormCommon.CreatHtmlContextNormal(
                        }
                        break;
                    #endregion

                    #region MicroIncImage
                    case ZhiFang.Common.Dictionary.SectionType.MicroIncImage:

                        modelname = ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelShowURL") + "\\" + showmodel);

                        modulePath = modelname.Replace("//", "");
                        //modulePath = modulePath.Replace("//", "\\");
                        //tmphtml = PrintReportFormCommon.creathtmlc
                        break;
                    #endregion

                    #region CellMorphology
                    case ZhiFang.Common.Dictionary.SectionType.CellMorphology:
                        modelname = ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelShowURL") + "\\" + showmodel);

                        modulePath = modelname.Replace("//", "");
                        //tmphtml=PrintReportFormCommon.CreatHtmlContextNormal(
                        break;
                    #endregion

                    #region FishCheck
                    case ZhiFang.Common.Dictionary.SectionType.FishCheck:
                        modelname = ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelShowURL") + "\\" + showmodel);
                        modulePath = modelname.Replace("//", "");
                        //tmphtml=PrintReportFormCommon.CreatHtmlContextNormal(
                        break;
                    #endregion

                    #region SensorCheck
                    case ZhiFang.Common.Dictionary.SectionType.SensorCheck:

                        modelname = ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelShowURL") + "\\" + showmodel);

                        modulePath = modelname.Replace("//", "");
                        //tmphtml=PrintReportFormCommon.CreatHtmlContextNormal(
                        break;
                    #endregion

                    #region ChromosomeCheck
                    case ZhiFang.Common.Dictionary.SectionType.ChromosomeCheck:

                        modelname = ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelShowURL") + "\\" + showmodel);

                        modulePath = modelname.Replace("//", "");
                        //tmphtml=PrintReportFormCommon.CreatHtmlContextNormal(
                        break;
                    #endregion

                    #region PathologyCheck
                    case ZhiFang.Common.Dictionary.SectionType.PathologyCheck:

                        modelname = ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelShowURL") + "\\" + showmodel);

                        modulePath = modelname.Replace("//", "");
                        //tmphtml=PrintReportFormCommon.CreatHtmlContextNormal(
                        break;
                    #endregion
                }

            }

            return modulePath;
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
            if (System.IO.Path.GetExtension(templateName).ToUpper() == ".FRX")
            {
                tempHtml = PrintReportFormCommon.CreatHtmlContextFRX(dtrf, dtri, templatePath);
            }
            else if (System.IO.Path.GetExtension(templateName).ToUpper() == ".FR3")
            {
                tempHtml = PrintReportFormCommon.CreatHtmlContextFR3(dtrf, dtri, templatePath);
            }
            else if (System.IO.Path.GetExtension(templateName).ToUpper() == ".XSLT")
            {
                tempHtml = PrintReportFormCommon.CreatHtmlContextXslt(dtrf, dtri, templatePath);
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
        public SortedList ShowFormTypeList(int SectionType, string PageName)
        {
            Log.Info("这是ShowFormTypeList()方法:ShowReport.cs");
            SortedList al = new SortedList();
            DataSet ds = iburfdlsc.ShowFormTypeList("");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (((ZhiFang.Common.Dictionary.SectionType)SectionType).ToString().Trim() == ds.Tables[0].Rows[i]["ReportType"].ToString().Trim() && PageName.Trim() == ds.Tables[0].Rows[i]["PageName"].ToString().Trim())
                    {
                        al.Add(ds.Tables[0].Rows[i]["XSLName"].ToString().Trim(), ds.Tables[0].Rows[i]["Name"].ToString().Trim());
                    }
                }
            }
            return al;
        }

    }
}
