using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using ZhiFang.DALFactory;
using ZhiFang.Common.Public;
using System.Data;
using System.IO;
//using System.Web.UI;
using ZhiFang.Model;
using ZhiFang.IBLL.Report;
using System.Collections;
using FastReport;
using FastReport.Export.Pdf;
using FastReport.Export.Html;
using ZhiFang.Common.Dictionary;
using ZhiFang.Common.Log;
using ZhiFang.IBLL.Common.BaseDictionary;
using System.Runtime.InteropServices;
using System.Configuration;

namespace ZhiFang.BLL.Report
{
    public class ShowFrom : ZhiFang.IBLL.Report.IBShowFrom
    {
        #region IBShowFrom 成员
        protected ALLReportForm arfb = new ALLReportForm();
        protected Model.PrintFormat format = new Model.PrintFormat();
        protected int PrintFormatNo;
        protected readonly IDReportItem item = DalFactory<IDReportItem>.GetDalByClassName("ReportItem");
        //protected readonly ZhiFang.IBLL.Report.IBPrintHtml printhtml = ZhiFang.BLLFactory.BLLFactory<IBPrintHtml>.GetBLL("PrintHtml");
        protected readonly ZhiFang.IBLL.Report.IBReportForm ibrf = ZhiFang.BLLFactory.BLLFactory<IBReportForm>.GetBLL("ReportForm");
        protected readonly ZhiFang.IBLL.Report.IBUserReportFormDataListShowConfig iburfdlsc = ZhiFang.BLLFactory.BLLFactory<IBUserReportFormDataListShowConfig>.GetBLL("UserReportFormDataListShowConfig");
        public string ShowReportForm(string FromNo, string ShowReportFormName)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// by spf 2010-08-27
        /// 得到模板号
        /// </summary>
        /// <param name="printformatno"></param>
        /// <returns></returns>
        public int GetPrintFormatNo(int printformatno)
        {
            return printformatno > 0 ? printformatno : 0;
        }

        /// <summary>
        /// xslt
        /// </summary>
        /// <param name="FromNo"></param>
        /// <param name="SectionNo"></param>
        /// <param name="PageName"></param>
        /// <param name="ShowType"></param>
        /// <returns></returns>
        public string ShowReportForm(string FromNo, int SectionNo, string PageName, int ShowType)
        {
            ZhiFang.Common.Log.Log.Info("这是ShowReportForm（）方法：ShowFrom.cs");
            ZhiFang.IBLL.Common.BaseDictionary.IBPGroup pg = ZhiFang.BLLFactory.BLLFactory<ZhiFang.IBLL.Common.BaseDictionary.IBPGroup>.GetBLL("IBPGroup");
            Model.PGroup pgm = pg.GetModel(SectionNo, (int)ZhiFang.Common.Dictionary.SectionTypeVisible.Visible);
            //string xslstr = SectionTypeCommon.GetFormat(((Common.SectionType)pgm.SectionType).ToString());
            DataSet ds = new DataSet();

            string showmodel = "Normal.XSLT";
            try
            {
                SortedList al = this.ShowFormTypeList(pgm, PageName);
                if (al.Count <= 0)
                {
                    return "显示模板配置文件错误！";
                }
                else
                {
                    try
                    {
                        showmodel = al.GetKey(ShowType).ToString();
                    }
                    catch
                    {
                        showmodel = al.GetKey(0).ToString();
                    }
                }
            }
            catch
            {
                return "显示模板配置错误！";
            }
            switch (((ZhiFang.Common.Dictionary.SectionType)pgm.SectionType))
            {
                case ZhiFang.Common.Dictionary.SectionType.Normal:
                    #region xslt
                    {
                        DataTable Fdt = arfb.GetFromInfo(FromNo);
                        if (Fdt.Rows.Count > 0)
                        {
                            Fdt = this.SetUserImage(Fdt);
                            DataTable Idt = arfb.GetFromItemList(FromNo);
                            string tmphtml = TransXMLToHtml.TransformXMLIntoHtml(ZhiFang.Common.Public.TransDataToXML.MergeXML(ZhiFang.Common.Public.TransDataToXML.TransformDTIntoXML(Fdt, "WebReportFile", "ReportForm"), ZhiFang.Common.Public.TransDataToXML.TransformDTIntoXML(Idt, "WebReportFile", "ReportItem"), "WebReportFile"), ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelShowURL") + "\\" + showmodel));
                            return tmphtml;

                        }
                        break;
                    }
                    #endregion
                case ZhiFang.Common.Dictionary.SectionType.Micro:
                    #region 没写
                    {
                        arfb.GetFromMicroInfo(FromNo); break;
                    }
                    #endregion
                case ZhiFang.Common.Dictionary.SectionType.NormalIncImage:
                    #region xslt
                    {
                        DataTable Fdt = arfb.GetFromInfo(FromNo);
                        if (Fdt.Rows.Count > 0)
                        {
                            Fdt = this.SetUserImage(Fdt);
                            DataTable Idt = arfb.GetFromItemList(FromNo);
                            string tmphtml = TransXMLToHtml.TransformXMLIntoHtml(ZhiFang.Common.Public.TransDataToXML.MergeXML(ZhiFang.Common.Public.TransDataToXML.TransformDTIntoXML(Fdt, "WebReportFile", "ReportForm"), ZhiFang.Common.Public.TransDataToXML.TransformDTIntoXML(Idt, "WebReportFile", "ReportItem"), "WebReportFile"), ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelShowURL") + "\\" + showmodel));
                            return tmphtml;

                        }
                        break;
                        //DataTable Fdt = arfb.GetFromInfo(FromNo);
                        //if (Fdt.Rows.Count > 0)
                        //{
                        //    Fdt = this.SetUserImage(Fdt);
                        //    DataTable Idt = arfb.GetFromItemList(FromNo);
                        //    DataTable Gdt = arfb.GetFromGraphList(FromNo);
                        //    DataTable ReportGraph = new DataTable();
                        //    ReportGraph.Columns.Add("Url");
                        //    ReportGraph.Columns.Add("GraphName");
                        //    ReportGraph.Columns.Add("GraphNo");
                        //    ReportGraph.Columns.Add("Type");
                        //    ReportGraph.Columns.Add("GraphData");
                        //    for (int i = 0; i < Gdt.Rows.Count; i++)
                        //    {
                        //        DateTime tmpdatetime = Convert.ToDateTime(Fdt.Rows[0]["RECEIVEDATE"].ToString().Trim());
                        //        string tmpdir =ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath( ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormImagesURL")) + "\\" + tmpdatetime.Year.ToString() + "\\" + tmpdatetime.Month.ToString() + "\\" + tmpdatetime.Day.ToString() + "\\" + SectionNo.ToString().Trim() + "\\" + FromNo.Trim();
                        //        string imagetype = ".jpg";
                        //        switch (Gdt.Rows[i]["GraphNo"].ToString().Trim())
                        //        {
                        //            case "4": imagetype = ".gif"; break;
                        //            case "5": imagetype = ".jpg"; break;
                        //            case "6": imagetype = ".bmp"; break;
                        //            case "7": imagetype = ".gif"; break;
                        //        }
                        //        string tmpfilename = Gdt.Rows[i]["GraphNo"].ToString().Trim() + "_" + Gdt.Rows[i]["GraphName"].ToString().Trim() + imagetype;
                        //        if (Gdt.Rows[0]["Graphjpg"] != DBNull.Value)
                        //        {
                        //            if (!FilesHelper.CheckDirFile(tmpdir, tmpfilename))
                        //            {
                        //                FilesHelper.CreatDirFile(tmpdir, tmpfilename, (Byte[])(Gdt.Rows[0]["Graphjpg"]));
                        //            }
                        //            ReportGraph.Rows.Add(ZhiFang.Common.Public.ConfigHelper.GetConfigString("LocalhostURL") + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormImagesURL") + "\\" + tmpdatetime.Year.ToString() + "\\" + tmpdatetime.Month.ToString() + "\\" + tmpdatetime.Day.ToString() + "\\" + SectionNo.ToString().Trim() + "\\" + FromNo.Trim(), Gdt.Rows[i]["GraphName"].ToString().Trim(), Gdt.Rows[i]["GraphNo"].ToString().Trim(), imagetype, "");
                        //        }
                        //    }
                        //    string tmphtml = TransXMLToHtml.TransformXMLIntoHtml(ZhiFang.Common.Public.TransDataToXML.MergeXML(ZhiFang.Common.Public.TransDataToXML.MergeXML(ZhiFang.Common.Public.TransDataToXML.TransformDTIntoXML(Fdt, "WebReportFile", "ReportForm"), ZhiFang.Common.Public.TransDataToXML.TransformDTIntoXML(Idt, "WebReportFile", "ReportItem"), "WebReportFile"), ZhiFang.Common.Public.TransDataToXML.TransformDTIntoXML(ReportGraph, "WebReportFile", "ReportGraph"), "WebReportFile"),ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath( ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelShowURL") + "\\" + showmodel));
                        //    return tmphtml;
                        //}
                        //break;
                    }
                    #endregion
                case ZhiFang.Common.Dictionary.SectionType.CellMorphology:
                    #region xslt
                    {
                        DataTable Fdt = arfb.GetFromInfo(FromNo);
                        if (Fdt.Rows.Count > 0)
                        {
                            Fdt = this.SetUserImage(Fdt);
                            DataTable Idt = arfb.GetMarrowItemList(FromNo);
                            DataTable Gdt = arfb.GetFromGraphList(FromNo);
                            DataTable ReportGraph = new DataTable();
                            ReportGraph.Columns.Add("Url");
                            ReportGraph.Columns.Add("GraphName");
                            ReportGraph.Columns.Add("GraphNo");
                            ReportGraph.Columns.Add("Type");
                            ReportGraph.Columns.Add("GraphData");
                            for (int i = 0; i < Gdt.Rows.Count; i++)
                            {
                                DateTime tmpdatetime = Convert.ToDateTime(Fdt.Rows[0]["RECEIVEDATE"].ToString().Trim());
                                string tmpdir = ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormImagesURL")) + "\\" + tmpdatetime.Year.ToString() + "\\" + tmpdatetime.Month.ToString() + "\\" + tmpdatetime.Day.ToString() + "\\" + SectionNo.ToString().Trim() + "\\" + FromNo.Trim();
                                string imagetype = ".jpg";
                                switch (Gdt.Rows[i]["GraphNo"].ToString().Trim())
                                {
                                    case "4": imagetype = ".gif"; break;
                                    case "5": imagetype = ".jpg"; break;
                                    case "6": imagetype = ".bmp"; break;
                                    case "7": imagetype = ".gif"; break;
                                }
                                string tmpfilename = Gdt.Rows[i]["GraphNo"].ToString().Trim() + "_" + Gdt.Rows[i]["GraphName"].ToString().Trim() + imagetype;
                                if (Gdt.Rows[0]["Graphjpg"] != DBNull.Value)
                                {
                                    if (!FilesHelper.CheckDirFile(tmpdir, tmpfilename))
                                    {
                                        FilesHelper.CreatDirFile(tmpdir, tmpfilename, (Byte[])(Gdt.Rows[0]["Graphjpg"]));
                                    }
                                    ReportGraph.Rows.Add(ZhiFang.Common.Public.ConfigHelper.GetConfigString("LocalhostURL") + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormImagesURL") + "\\" + tmpdatetime.Year.ToString() + "\\" + tmpdatetime.Month.ToString() + "\\" + tmpdatetime.Day.ToString() + "\\" + SectionNo.ToString().Trim() + "\\" + FromNo.Trim(), Gdt.Rows[i]["GraphName"].ToString().Trim(), Gdt.Rows[i]["GraphNo"].ToString().Trim(), imagetype, "");
                                }
                            }
                            //string tmphtml = TransXMLToHtml.TransformXMLIntoHtml(ZhiFang.Common.Public.TransDataToXML.MergeXML(ZhiFang.Common.Public.TransDataToXML.MergeXML(ZhiFang.Common.Public.TransDataToXML.TransformDTIntoXML(Fdt, "WebReportFile", "ReportForm"), ZhiFang.Common.Public.TransDataToXML.TransformDTIntoXML(Idt, "WebReportFile", "ReportMarrow"), "WebReportFile"), ZhiFang.Common.Public.TransDataToXML.TransformDTIntoXML(ReportGraph, "WebReportFile", "ReportGraph"), "WebReportFile"),ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath( ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelShowURL") + "\\" + showmodel));
                            //return tmphtml;
                            System.Xml.XmlDocument xmldoc = (ZhiFang.Common.Public.TransDataToXML.MergeXML(ZhiFang.Common.Public.TransDataToXML.MergeXML(ZhiFang.Common.Public.TransDataToXML.TransformDTIntoXML(Fdt, "WebReportFile", "ReportForm"), ZhiFang.Common.Public.TransDataToXML.TransformDTIntoXML(Idt, "WebReportFile", "ReportMarrow"), "WebReportFile"), ZhiFang.Common.Public.TransDataToXML.TransformDTIntoXML(ReportGraph, "WebReportFile", "ReportGraph"), "WebReportFile"));
                            return xmldoc.InnerXml;
                        }
                        break;
                    }
                    #endregion
                default:
                    {
                        DataTable Fdt = arfb.GetFromInfo(FromNo);
                        if (Fdt.Rows.Count > 0)
                        {
                            DataTable Idt = arfb.GetFromItemList(FromNo);
                        }
                        break;
                    }
            }
            return "";
        }

        /// <summary>
        /// ShowForm ShowReportForm_Weblis FRX XSLT
        /// </summary>
        /// <param name="FromNo"></param>
        /// <param name="SectionNo"></param>
        /// <param name="PageName"></param>
        /// <param name="ShowType"></param>
        /// <param name="sectiontype"></param>
        /// <returns></returns>
        public string ShowReportForm_Weblis(string FromNo, int SectionNo, string PageName, int ShowType, int sectiontype)
        {
            ZhiFang.Common.Log.Log.Info("这是ShowReportForm_Weblis()方法：ShowFrom.cs");
            DataSet ds = new DataSet();
            ALLReportForm_Weblis arfb_w = new ALLReportForm_Weblis();
            string showmodel = "Normal.XSLT";

            ZhiFang.IBLL.Report.IBReportFormFull rffb = ZhiFang.BLLFactory.BLLFactory<IBReportFormFull>.GetBLL("ReportFormFull");
            ZhiFang.IBLL.Report.IBReportItemFull rifb = ZhiFang.BLLFactory.BLLFactory<IBReportItemFull>.GetBLL("ReportItemFull");
            ZhiFang.IBLL.Report.IBReportMicroFull rmfb = ZhiFang.BLLFactory.BLLFactory<IBReportMicroFull>.GetBLL("ReportMicroFull");
            ZhiFang.IBLL.Report.IBReportMarrowFull rmarrowfb = ZhiFang.BLLFactory.BLLFactory<IBReportMarrowFull>.GetBLL("ReportMarrowFull");
            ZhiFang.IBLL.Common.BaseDictionary.IBCLIENTELE cl = ZhiFang.BLLFactory.BLLFactory<IBCLIENTELE>.GetBLL();
            Model.CLIENTELE cl_m = new Model.CLIENTELE();
            Model.ReportFormFull rff_m = new Model.ReportFormFull();
            Model.ReportItemFull rif_m = new Model.ReportItemFull();
            Model.ReportMicroFull rmf_m = new Model.ReportMicroFull();
            Model.ReportMarrowFull rmarrowf_m = new Model.ReportMarrowFull();
            DataSet dsri;
            rff_m.ReportFormID = FromNo;
            DataSet dsrf = rffb.GetList(rff_m);
            //添加送检单位列
            ZhiFang.Common.Log.Log.Info("查询送检单位，ReportFormFull.WeblisSourceOrgId。");
            dsrf.Tables[0].Columns.Add("WEBLISSOURCEORGNAME", typeof(string));
            if (dsrf != null && dsrf.Tables.Count > 0 && dsrf.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsrf.Tables[0].Rows.Count; i++)
                {
                    try
                    {
                        cl_m = cl.GetModel(Convert.ToInt64(dsrf.Tables[0].Rows[i]["WeblisSourceOrgId"]));
                        dsrf.Tables[0].Rows[i]["WEBLISSOURCEORGNAME"] = cl_m.CNAME;
                    }
                    catch
                    {
                        ZhiFang.Common.Log.Log.Info("请检测ReportFormFull.WeblisSourceOrgId。");
                    }
                        
                   
                }
            }
            DataTable dtrf = new DataTable("frform");
            DataTable dtri = new DataTable();
            FastReport.Report report = new FastReport.Report();
            try
            {
                SortedList al = this.ShowFormTypeList(sectiontype, PageName);
                if (al.Count <= 0)
                {
                    return "显示模板配置文件错误！";
                }
                else
                {
                    try
                    {
                        showmodel = al.GetKey(ShowType).ToString();
                    }
                    catch
                    {
                        showmodel = al.GetKey(0).ToString();
                    }
                }
            }
            catch
            {
                return "显示模板配置错误！";
            }
            if (dsrf.Tables.Count > 0 && dsrf.Tables[0].Rows.Count > 0)
            {
                ZhiFang.Common.Log.Log.Info("显示模板：" + showmodel);
                dtrf = dsrf.Tables[0];
                dtrf.TableName = "frform";
                string modelname = "";
                switch (((ZhiFang.Common.Dictionary.SectionType)sectiontype))
                {
                    case ZhiFang.Common.Dictionary.SectionType.Normal:
                        {
                            #region Normal
                            DataTable Fdt = dtrf;
                            if (Fdt.Rows.Count > 0)
                            {
                                //Fdt = this.SetUserImage(Fdt);
                                DataTable Idt = arfb_w.GetFromItemList(FromNo);
                                string tmphtml = TransXMLToHtml.TransformXMLIntoHtml(ZhiFang.Common.Public.TransDataToXML.MergeXML(ZhiFang.Common.Public.TransDataToXML.TransformDTIntoXML(Fdt, "WebReportFile", "ReportForm"), ZhiFang.Common.Public.TransDataToXML.TransformDTIntoXML(Idt, "WebReportFile", "ReportItem"), "WebReportFile"), ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelShowURL") + "\\" + showmodel));
                                return tmphtml;

                            }
                            break;
                            #endregion
                        }
                    case ZhiFang.Common.Dictionary.SectionType.Micro:
                        {
                            #region Micro
                            rmf_m.ReportFormID = FromNo;
                            dsri = rmfb.GetList(rmf_m);
                            if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                            {
                                dtri = dsri.Tables[0];
                            }
                            modelname = ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelShowURL") + "\\" + showmodel);
                            report.Load(modelname);
                            if (modelname.Trim().Length <= 0)
                            {
                                return null;
                                //return "暂无匹配模板！";
                            }
                            dtri.TableName = "frmicro";
                            report.Load(modelname);
                            RegeditData(dtri, ref report, modelname);

                            report.RegisterData(dtrf.DataSet);
                            RegeditImage(dtrf.Rows[0], ref report);
                            report.Prepare();
                            FastReport.Export.Image.ImageExport tmpjpg = new FastReport.Export.Image.ImageExport();
                            string imagetype = "jpeg";
                            if (ZhiFang.Common.Public.ConfigHelper.GetConfigInt("JpegQuality") != 0)
                            {
                                tmpjpg.JpegQuality = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("JpegQuality");
                            }

                            switch (ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFileType").ToUpper())
                            {
                                case "JPEG": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg; imagetype = "jpeg"; break;
                                case "JPG": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg; imagetype = "jpg"; break;
                                case "GIF": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Gif; imagetype = "gif"; break;
                                case "BMP": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Bmp; imagetype = "bmp"; break;
                                case "PNG": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Png; imagetype = "png"; break;
                                case "TIFF": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Tiff; imagetype = "tiff"; break;
                                default: tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg; imagetype = "jpeg"; break;
                            }
                            report.Export(tmpjpg, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + FromNo.Replace(" 00:00:00", "").Replace(",", "&") + "@." + imagetype + "");
                            report.Dispose();

                            List<string> imagepath = tmpjpg.GeneratedFiles;
                            string tmphtml = "";
                            foreach (string a in imagepath)
                            {
                                tmphtml += "<img src=\"" + "../" + a.Substring(a.LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")), a.Length - a.LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir"))) + "\"  /><br>";
                            }
                            return tmphtml;
                            break;
                            #endregion
                        }
                    case ZhiFang.Common.Dictionary.SectionType.NormalIncImage:
                        {
                            #region NormalIncImage
                            DataTable Fdt = dtrf;
                            if (Fdt.Rows.Count > 0)
                            {
                                //Fdt = this.SetUserImage(Fdt);
                                DataTable Idt = arfb_w.GetFromItemList(FromNo);
                                string tmphtml = TransXMLToHtml.TransformXMLIntoHtml(ZhiFang.Common.Public.TransDataToXML.MergeXML(ZhiFang.Common.Public.TransDataToXML.TransformDTIntoXML(Fdt, "WebReportFile", "ReportForm"), ZhiFang.Common.Public.TransDataToXML.TransformDTIntoXML(Idt, "WebReportFile", "ReportItem"), "WebReportFile"), ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelShowURL") + "\\" + showmodel));
                                return tmphtml;

                            }
                            break;
                            //DataTable Fdt = arfb.GetFromInfo(FromNo);
                            //if (Fdt.Rows.Count > 0)
                            //{
                            //    Fdt = this.SetUserImage(Fdt);
                            //    DataTable Idt = arfb_w.GetFromItemList(FromNo);
                            //    DataTable Gdt = arfb_w.GetFromGraphList(FromNo);
                            //    DataTable ReportGraph = new DataTable();
                            //    ReportGraph.Columns.Add("Url");
                            //    ReportGraph.Columns.Add("GraphName");
                            //    ReportGraph.Columns.Add("GraphNo");
                            //    ReportGraph.Columns.Add("Type");
                            //    ReportGraph.Columns.Add("GraphData");
                            //    for (int i = 0; i < Gdt.Rows.Count; i++)
                            //    {
                            //        DateTime tmpdatetime = Convert.ToDateTime(Fdt.Rows[0]["RECEIVEDATE"].ToString().Trim());
                            //        string tmpdir =ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath( ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormImagesURL")) + "\\" + tmpdatetime.Year.ToString() + "\\" + tmpdatetime.Month.ToString() + "\\" + tmpdatetime.Day.ToString() + "\\" + SectionNo.ToString().Trim() + "\\" + FromNo.Trim();
                            //        string imagetype = ".jpg";
                            //        switch (Gdt.Rows[i]["GraphNo"].ToString().Trim())
                            //        {
                            //            case "4": imagetype = ".gif"; break;
                            //            case "5": imagetype = ".jpg"; break;
                            //            case "6": imagetype = ".bmp"; break;
                            //            case "7": imagetype = ".gif"; break;
                            //        }
                            //        string tmpfilename = Gdt.Rows[i]["GraphNo"].ToString().Trim() + "_" + Gdt.Rows[i]["GraphName"].ToString().Trim() + imagetype;
                            //        if (Gdt.Rows[0]["Graphjpg"] != DBNull.Value)
                            //        {
                            //            if (!FilesHelper.CheckDirFile(tmpdir, tmpfilename))
                            //            {
                            //                FilesHelper.CreatDirFile(tmpdir, tmpfilename, (Byte[])(Gdt.Rows[0]["Graphjpg"]));
                            //            }
                            //            ReportGraph.Rows.Add(ZhiFang.Common.Public.ConfigHelper.GetConfigString("LocalhostURL") + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormImagesURL") + "\\" + tmpdatetime.Year.ToString() + "\\" + tmpdatetime.Month.ToString() + "\\" + tmpdatetime.Day.ToString() + "\\" + SectionNo.ToString().Trim() + "\\" + FromNo.Trim(), Gdt.Rows[i]["GraphName"].ToString().Trim(), Gdt.Rows[i]["GraphNo"].ToString().Trim(), imagetype, "");
                            //        }
                            //    }
                            //    string tmphtml = TransXMLToHtml.TransformXMLIntoHtml(ZhiFang.Common.Public.TransDataToXML.MergeXML(ZhiFang.Common.Public.TransDataToXML.MergeXML(ZhiFang.Common.Public.TransDataToXML.TransformDTIntoXML(Fdt, "WebReportFile", "ReportForm"), ZhiFang.Common.Public.TransDataToXML.TransformDTIntoXML(Idt, "WebReportFile", "ReportItem"), "WebReportFile"), ZhiFang.Common.Public.TransDataToXML.TransformDTIntoXML(ReportGraph, "WebReportFile", "ReportGraph"), "WebReportFile"),ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath( ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelShowURL") + "\\" + showmodel));
                            //    return tmphtml;
                            //}
                            //break;
                            #endregion
                        }
                    case ZhiFang.Common.Dictionary.SectionType.MicroIncImage:
                        {
                            #region MicroIncImage
                            rmf_m.ReportFormID = FromNo;
                            dsri = rmfb.GetList(rmf_m);
                            if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                            {
                                dtri = dsri.Tables[0];
                            }
                            modelname = ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelShowURL") + "\\" + showmodel);
                            report.Load(modelname);
                            if (modelname.Trim().Length <= 0)
                            {
                                return null;
                                //return "暂无匹配模板！";
                            }
                            dtri.TableName = "frmicro";
                            report.Load(modelname);
                            RegeditData(dtri, ref report, modelname);

                            report.RegisterData(dtrf.DataSet);
                            RegeditImage(dtrf.Rows[0], ref report);
                            report.Prepare();
                            FastReport.Export.Image.ImageExport tmpjpg = new FastReport.Export.Image.ImageExport();
                            string imagetype = "jpeg";
                            if (ZhiFang.Common.Public.ConfigHelper.GetConfigInt("JpegQuality") != 0)
                            {
                                tmpjpg.JpegQuality = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("JpegQuality");
                            }

                            switch (ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFileType").ToUpper())
                            {
                                case "JPEG": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg; imagetype = "jpeg"; break;
                                case "JPG": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg; imagetype = "jpg"; break;
                                case "GIF": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Gif; imagetype = "gif"; break;
                                case "BMP": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Bmp; imagetype = "bmp"; break;
                                case "PNG": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Png; imagetype = "png"; break;
                                case "TIFF": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Tiff; imagetype = "tiff"; break;
                                default: tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg; imagetype = "jpeg"; break;
                            }
                            report.Export(tmpjpg, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + FromNo.Replace(" 00:00:00", "").Replace(",", "&") + "@." + imagetype + "");
                            report.Dispose();

                            List<string> imagepath = tmpjpg.GeneratedFiles;
                            string tmphtml = "";
                            foreach (string a in imagepath)
                            {
                                tmphtml += "<img src=\"" + "../" + a.Substring(a.LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")), a.Length - a.LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir"))) + "\"  /><br>";
                            }
                            return tmphtml;
                            break;
                            #endregion
                        }
                    case ZhiFang.Common.Dictionary.SectionType.CellMorphology:
                        {
                            #region CellMorphology
                            rmarrowf_m.ReportFormID = FromNo;
                            dsri = rmarrowfb.GetList(rmarrowf_m);
                            if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                            {
                                dtri = dsri.Tables[0];
                            }
                            string[] modelList = showmodel.Split(';');
                            if (modelList.Length > 1)
                            {
                                showmodel = "";
                                for (int i = 0; i < modelList.Length; i++)
                                {
                                    if (dtrf.Rows[0]["sectionno"].ToString().Trim() == modelList[i].Split(',')[0])
                                    {
                                        showmodel = modelList[i].Split(',')[1];
                                    }
                                }
                                if (showmodel == "")
                                {
                                    showmodel = modelList[0].Split(',')[1];
                                }
                            }
                            modelname = ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelShowURL") + "\\" + showmodel);
                            report.Load(modelname);
                            for (int i = 0; i < dtri.Rows.Count; i++)
                            {
                                if (dtri.Columns.Contains("ItemNo"))
                                {
                                    if (dtri.Columns.Contains("CItemNo"))
                                    {
                                        CheckBoxObject CheckBox = (CheckBoxObject)report.FindObject("CheckBoxM" + dtri.Rows[i]["ItemNo"].ToString() + "C" + dtri.Rows[i]["CItemNo"].ToString());

                                        if (CheckBox != null)
                                        {
                                            CheckBox.Checked = true;
                                        }
                                    }
                                    TextObject text = (TextObject)report.FindObject("TextM" + dtri.Rows[i]["ItemNo"].ToString());

                                    if (text != null)
                                    {
                                        if (dtri.Columns.Contains("ReportValue"))
                                        {
                                            text.Text += dtri.Rows[i]["ReportValue"].ToString();
                                        }
                                        if (dtri.Columns.Contains("ReportDesc"))
                                        {
                                            text.Text += dtri.Rows[i]["ReportDesc"].ToString();
                                        }
                                        if (dtri.Columns.Contains("ReportText"))
                                        {
                                            text.Text += dtri.Rows[i]["ReportText"].ToString();
                                        }
                                    }
                                    TextObject textBN = (TextObject)report.FindObject("TextBN" + dtri.Rows[i]["ItemNo"].ToString());
                                    if (textBN != null)
                                    {
                                        if (dtri.Columns.Contains("BloodNum"))
                                        {
                                            textBN.Text += dtri.Rows[i]["BloodNum"].ToString();
                                        }
                                    }
                                    TextObject textBP = (TextObject)report.FindObject("TextBP" + dtri.Rows[i]["ItemNo"].ToString());
                                    if (textBP != null)
                                    {
                                        if (dtri.Columns.Contains("BloodPercent"))
                                        {
                                            textBP.Text += dtri.Rows[i]["BloodPercent"].ToString();
                                        }
                                    }
                                    TextObject textMN = (TextObject)report.FindObject("TextMN" + dtri.Rows[i]["ItemNo"].ToString());
                                    if (textMN != null)
                                    {
                                        if (dtri.Columns.Contains("MarrowNum"))
                                        {
                                            textMN.Text += dtri.Rows[i]["MarrowNum"].ToString();
                                        }
                                    }
                                    TextObject textMP = (TextObject)report.FindObject("TextMP" + dtri.Rows[i]["ItemNo"].ToString());
                                    if (textMP != null)
                                    {
                                        if (dtri.Columns.Contains("MarrowPercent"))
                                        {
                                            textMP.Text += dtri.Rows[i]["MarrowPercent"].ToString();
                                        }
                                    }
                                    TextObject textBD = (TextObject)report.FindObject("TextBD" + dtri.Rows[i]["ItemNo"].ToString());
                                    if (textBD != null)
                                    {
                                        if (dtri.Columns.Contains("BloodDesc"))
                                        {
                                            textBD.Text += dtri.Rows[i]["BloodDesc"].ToString();
                                        }
                                    }
                                    TextObject textMD = (TextObject)report.FindObject("TextMD" + dtri.Rows[i]["ItemNo"].ToString());
                                    if (textMD != null)
                                    {
                                        if (dtri.Columns.Contains("MarrowDesc"))
                                        {
                                            textMD.Text += dtri.Rows[i]["MarrowDesc"].ToString();
                                        }
                                    }
                                    TextObject textRR = (TextObject)report.FindObject("TextRR" + dtri.Rows[i]["ItemNo"].ToString());
                                    if (textRR != null)
                                    {
                                        if (dtri.Columns.Contains("RefRange"))
                                        {
                                            textRR.Text += dtri.Rows[i]["RefRange"].ToString();
                                        }
                                    }
                                }
                            }
                            report.RegisterData(dtrf.DataSet);
                            RegeditImage(dtrf.Rows[0], ref report);
                            report.Prepare();
                            FastReport.Export.Image.ImageExport tmpjpg = new FastReport.Export.Image.ImageExport();
                            string imagetype = "jpeg";
                            if (ZhiFang.Common.Public.ConfigHelper.GetConfigInt("JpegQuality") != 0)
                            {
                                tmpjpg.JpegQuality = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("JpegQuality");
                            }

                            switch (ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFileType").ToUpper())
                            {
                                case "JPEG": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg; imagetype = "jpeg"; break;
                                case "JPG": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg; imagetype = "jpg"; break;
                                case "GIF": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Gif; imagetype = "gif"; break;
                                case "BMP": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Bmp; imagetype = "bmp"; break;
                                case "PNG": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Png; imagetype = "png"; break;
                                case "TIFF": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Tiff; imagetype = "tiff"; break;
                                default: tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg; imagetype = "jpeg"; break;
                            }
                            report.Export(tmpjpg, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + FromNo.Replace(" 00:00:00", "").Replace(",", "&") + "@." + imagetype + "");
                            report.Dispose();

                            List<string> imagepath = tmpjpg.GeneratedFiles;
                            string tmphtml = "";
                            foreach (string a in imagepath)
                            {
                                tmphtml += "<img src=\"" + "../" + a.Substring(a.LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")), a.Length - a.LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir"))) + "\"  /><br>";
                            }
                            return tmphtml;
                            break;
                            #endregion
                        }
                    case ZhiFang.Common.Dictionary.SectionType.FishCheck:
                        {
                            #region FishCheck
                            rmarrowf_m.ReportFormID = FromNo;
                            dsri = rmarrowfb.GetList(rmarrowf_m);
                            if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                            {
                                dtri = dsri.Tables[0];
                            }
                            string[] modelList = showmodel.Split(';');
                            if (modelList.Length > 1)
                            {
                                showmodel = "";
                                for (int i = 0; i < modelList.Length; i++)
                                {
                                    if (dtrf.Rows[0]["sectionno"].ToString().Trim() == modelList[i].Split(',')[0])
                                    {
                                        showmodel = modelList[i].Split(',')[1];
                                    }
                                }
                                if (showmodel == "")
                                {
                                    showmodel = modelList[0].Split(',')[1];
                                }
                            }
                            modelname = ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelShowURL") + "\\" + showmodel);
                            report.Load(modelname);
                            for (int i = 0; i < dtri.Rows.Count; i++)
                            {
                                if (dtri.Columns.Contains("ItemNo"))
                                {
                                    if (dtri.Columns.Contains("CItemNo"))
                                    {
                                        CheckBoxObject CheckBox = (CheckBoxObject)report.FindObject("CheckBoxM" + dtri.Rows[i]["ItemNo"].ToString() + "C" + dtri.Rows[i]["CItemNo"].ToString());

                                        if (CheckBox != null)
                                        {
                                            CheckBox.Checked = true;
                                        }
                                    }
                                    TextObject text = (TextObject)report.FindObject("TextM" + dtri.Rows[i]["ItemNo"].ToString());

                                    if (text != null)
                                    {
                                        if (dtri.Columns.Contains("ReportValue"))
                                        {
                                            text.Text += dtri.Rows[i]["ReportValue"].ToString();
                                        }
                                        if (dtri.Columns.Contains("ReportDesc"))
                                        {
                                            text.Text += dtri.Rows[i]["ReportDesc"].ToString();
                                        }
                                        if (dtri.Columns.Contains("ReportText"))
                                        {
                                            text.Text += dtri.Rows[i]["ReportText"].ToString();
                                        }
                                    }
                                }
                            }
                            report.RegisterData(dtrf.DataSet);
                            RegeditImage(dtrf.Rows[0], ref report);
                            report.Prepare();
                            FastReport.Export.Image.ImageExport tmpjpg = new FastReport.Export.Image.ImageExport();
                            string imagetype = "jpeg";
                            if (ZhiFang.Common.Public.ConfigHelper.GetConfigInt("JpegQuality") != 0)
                            {
                                tmpjpg.JpegQuality = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("JpegQuality");
                            }

                            switch (ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFileType").ToUpper())
                            {
                                case "JPEG": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg; imagetype = "jpeg"; break;
                                case "JPG": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg; imagetype = "jpg"; break;
                                case "GIF": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Gif; imagetype = "gif"; break;
                                case "BMP": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Bmp; imagetype = "bmp"; break;
                                case "PNG": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Png; imagetype = "png"; break;
                                case "TIFF": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Tiff; imagetype = "tiff"; break;
                                default: tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg; imagetype = "jpeg"; break;
                            }
                            report.Export(tmpjpg, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + FromNo.Replace(" 00:00:00", "").Replace(",", "&") + "@." + imagetype + "");
                            report.Dispose();

                            List<string> imagepath = tmpjpg.GeneratedFiles;
                            string tmphtml = "";
                            foreach (string a in imagepath)
                            {
                                tmphtml += "<img src=\"" + "../" + a.Substring(a.LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")), a.Length - a.LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir"))) + "\"  /><br>";
                            }
                            return tmphtml;
                            break;
                            #endregion
                        }
                    case ZhiFang.Common.Dictionary.SectionType.SensorCheck:
                        {
                            #region SensorCheck
                            rmarrowf_m.ReportFormID = FromNo;
                            dsri = rmarrowfb.GetList(rmarrowf_m);
                            if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                            {
                                dtri = dsri.Tables[0];
                            }
                            string[] modelList = showmodel.Split(';');
                            if (modelList.Length > 1)
                            {
                                showmodel = "";
                                for (int i = 0; i < modelList.Length; i++)
                                {
                                    if (dtrf.Rows[0]["sectionno"].ToString().Trim() == modelList[i].Split(',')[0])
                                    {
                                        showmodel = modelList[i].Split(',')[1];
                                    }
                                }
                                if (showmodel == "")
                                {
                                    showmodel = modelList[0].Split(',')[1];
                                }
                            }
                            modelname = ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelShowURL") + "\\" + showmodel);
                            report.Load(modelname);
                            for (int i = 0; i < dtri.Rows.Count; i++)
                            {
                                if (dtri.Columns.Contains("ItemNo"))
                                {
                                    if (dtri.Columns.Contains("CItemNo"))
                                    {
                                        CheckBoxObject CheckBox = (CheckBoxObject)report.FindObject("CheckBoxM" + dtri.Rows[i]["ItemNo"].ToString() + "C" + dtri.Rows[i]["CItemNo"].ToString());

                                        if (CheckBox != null)
                                        {
                                            CheckBox.Checked = true;
                                        }
                                    }
                                    TextObject text = (TextObject)report.FindObject("TextM" + dtri.Rows[i]["ItemNo"].ToString());

                                    if (text != null)
                                    {
                                        if (dtri.Columns.Contains("ReportValue"))
                                        {
                                            text.Text += dtri.Rows[i]["ReportValue"].ToString();
                                        }
                                        if (dtri.Columns.Contains("ReportDesc"))
                                        {
                                            text.Text += dtri.Rows[i]["ReportDesc"].ToString();
                                        }
                                        if (dtri.Columns.Contains("ReportText"))
                                        {
                                            text.Text += dtri.Rows[i]["ReportText"].ToString();
                                        }
                                    }
                                }
                            }
                            report.RegisterData(dtrf.DataSet);
                            RegeditImage(dtrf.Rows[0], ref report);
                            report.Prepare();
                            FastReport.Export.Image.ImageExport tmpjpg = new FastReport.Export.Image.ImageExport();
                            string imagetype = "jpeg";
                            if (ZhiFang.Common.Public.ConfigHelper.GetConfigInt("JpegQuality") != 0)
                            {
                                tmpjpg.JpegQuality = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("JpegQuality");
                            }

                            switch (ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFileType").ToUpper())
                            {
                                case "JPEG": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg; imagetype = "jpeg"; break;
                                case "JPG": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg; imagetype = "jpg"; break;
                                case "GIF": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Gif; imagetype = "gif"; break;
                                case "BMP": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Bmp; imagetype = "bmp"; break;
                                case "PNG": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Png; imagetype = "png"; break;
                                case "TIFF": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Tiff; imagetype = "tiff"; break;
                                default: tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg; imagetype = "jpeg"; break;
                            }
                            report.Export(tmpjpg, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + FromNo.Replace(" 00:00:00", "").Replace(",", "&") + "@." + imagetype + "");
                            report.Dispose();

                            List<string> imagepath = tmpjpg.GeneratedFiles;
                            string tmphtml = "";
                            foreach (string a in imagepath)
                            {
                                tmphtml += "<img src=\"" + "../" + a.Substring(a.LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")), a.Length - a.LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir"))) + "\"  /><br>";
                            }
                            return tmphtml;
                            break;
                            #endregion
                        }
                    case ZhiFang.Common.Dictionary.SectionType.ChromosomeCheck:
                        {
                            #region ChromosomeCheck
                            rmarrowf_m.ReportFormID = FromNo;
                            dsri = rmarrowfb.GetList(rmarrowf_m);
                            if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                            {
                                dtri = dsri.Tables[0];
                            }
                            string[] modelList = showmodel.Split(';');
                            if (modelList.Length > 1)
                            {
                                showmodel = "";
                                for (int i = 0; i < modelList.Length; i++)
                                {
                                    if (dtrf.Rows[0]["sectionno"].ToString().Trim() == modelList[i].Split(',')[0])
                                    {
                                        showmodel = modelList[i].Split(',')[1];
                                    }
                                }
                                if (showmodel == "")
                                {
                                    showmodel = modelList[0].Split(',')[1];
                                }
                            }
                            modelname = ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelShowURL") + "\\" + showmodel);
                            report.Load(modelname);
                            for (int i = 0; i < dtri.Rows.Count; i++)
                            {
                                if (dtri.Columns.Contains("ItemNo"))
                                {
                                    if (dtri.Columns.Contains("CItemNo"))
                                    {
                                        CheckBoxObject CheckBox = (CheckBoxObject)report.FindObject("CheckBoxM" + dtri.Rows[i]["ItemNo"].ToString() + "C" + dtri.Rows[i]["CItemNo"].ToString());

                                        if (CheckBox != null)
                                        {
                                            CheckBox.Checked = true;
                                        }
                                    }
                                    TextObject text = (TextObject)report.FindObject("TextM" + dtri.Rows[i]["ItemNo"].ToString());

                                    if (text != null)
                                    {
                                        if (dtri.Columns.Contains("ReportValue"))
                                        {
                                            text.Text += dtri.Rows[i]["ReportValue"].ToString();
                                        }
                                        if (dtri.Columns.Contains("ReportDesc"))
                                        {
                                            text.Text += dtri.Rows[i]["ReportDesc"].ToString();
                                        }
                                        if (dtri.Columns.Contains("ReportText"))
                                        {
                                            text.Text += dtri.Rows[i]["ReportText"].ToString();
                                        }
                                    }
                                }
                            }
                            report.RegisterData(dtrf.DataSet);
                            RegeditImage(dtrf.Rows[0], ref report);
                            report.Prepare();
                            FastReport.Export.Image.ImageExport tmpjpg = new FastReport.Export.Image.ImageExport();
                            string imagetype = "jpeg";
                            if (ZhiFang.Common.Public.ConfigHelper.GetConfigInt("JpegQuality") != 0)
                            {
                                tmpjpg.JpegQuality = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("JpegQuality");
                            }

                            switch (ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFileType").ToUpper())
                            {
                                case "JPEG": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg; imagetype = "jpeg"; break;
                                case "JPG": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg; imagetype = "jpg"; break;
                                case "GIF": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Gif; imagetype = "gif"; break;
                                case "BMP": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Bmp; imagetype = "bmp"; break;
                                case "PNG": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Png; imagetype = "png"; break;
                                case "TIFF": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Tiff; imagetype = "tiff"; break;
                                default: tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg; imagetype = "jpeg"; break;
                            }
                            report.Export(tmpjpg, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + FromNo.Replace(" 00:00:00", "").Replace(",", "&") + "@." + imagetype + "");
                            report.Dispose();

                            List<string> imagepath = tmpjpg.GeneratedFiles;
                            string tmphtml = "";
                            foreach (string a in imagepath)
                            {
                                tmphtml += "<img src=\"" + "../" + a.Substring(a.LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")), a.Length - a.LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir"))) + "\"  /><br>";
                            }
                            return tmphtml;
                            break;
                            #endregion
                        }
                    case ZhiFang.Common.Dictionary.SectionType.PathologyCheck:
                        {
                            #region PathologyCheck
                            rmarrowf_m.ReportFormID = FromNo;
                            dsri = rmarrowfb.GetList(rmarrowf_m);
                            if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                            {
                                dtri = dsri.Tables[0];
                            }
                            string[] modelList = showmodel.Split(';');
                            if (modelList.Length > 1)
                            {
                                showmodel = "";
                                for (int i = 0; i < modelList.Length; i++)
                                {
                                    if (dtrf.Rows[0]["sectionno"].ToString().Trim() == modelList[i].Split(',')[0])
                                    {
                                        showmodel = modelList[i].Split(',')[1];
                                    }
                                }
                                if (showmodel == "")
                                {
                                    showmodel = modelList[0].Split(',')[1];
                                }
                            }
                            modelname = ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelShowURL") + "\\" + showmodel);
                            report.Load(modelname);
                            for (int i = 0; i < dtri.Rows.Count; i++)
                            {
                                if (dtri.Columns.Contains("ItemNo"))
                                {
                                    if (dtri.Columns.Contains("CItemNo"))
                                    {
                                        CheckBoxObject CheckBox = (CheckBoxObject)report.FindObject("CheckBoxM" + dtri.Rows[i]["ItemNo"].ToString() + "C" + dtri.Rows[i]["CItemNo"].ToString());

                                        if (CheckBox != null)
                                        {
                                            CheckBox.Checked = true;
                                        }
                                    }
                                    TextObject text = (TextObject)report.FindObject("TextM" + dtri.Rows[i]["ItemNo"].ToString());

                                    if (text != null)
                                    {
                                        if (dtri.Columns.Contains("ReportValue"))
                                        {
                                            text.Text += dtri.Rows[i]["ReportValue"].ToString();
                                        }
                                        if (dtri.Columns.Contains("ReportDesc"))
                                        {
                                            text.Text += dtri.Rows[i]["ReportDesc"].ToString();
                                        }
                                        if (dtri.Columns.Contains("ReportText"))
                                        {
                                            text.Text += dtri.Rows[i]["ReportText"].ToString();
                                        }
                                    }
                                }
                            }
                            report.RegisterData(dtrf.DataSet);
                            RegeditImage(dtrf.Rows[0], ref report);
                            report.Prepare();
                            FastReport.Export.Image.ImageExport tmpjpg = new FastReport.Export.Image.ImageExport();
                            string imagetype = "jpeg";
                            if (ZhiFang.Common.Public.ConfigHelper.GetConfigInt("JpegQuality") != 0)
                            {
                                tmpjpg.JpegQuality = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("JpegQuality");
                            }

                            switch (ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFileType").ToUpper())
                            {
                                case "JPEG": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg; imagetype = "jpeg"; break;
                                case "JPG": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg; imagetype = "jpg"; break;
                                case "GIF": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Gif; imagetype = "gif"; break;
                                case "BMP": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Bmp; imagetype = "bmp"; break;
                                case "PNG": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Png; imagetype = "png"; break;
                                case "TIFF": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Tiff; imagetype = "tiff"; break;
                                default: tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg; imagetype = "jpeg"; break;
                            }
                            report.Export(tmpjpg, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + FromNo.Replace(" 00:00:00", "").Replace(",", "&") + "@." + imagetype + "");
                            report.Dispose();

                            List<string> imagepath = tmpjpg.GeneratedFiles;
                            string tmphtml = "";
                            foreach (string a in imagepath)
                            {
                                tmphtml += "<img src=\"" + "../" + a.Substring(a.LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")), a.Length - a.LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir"))) + "\"  /><br>";
                            }
                            return tmphtml;
                            break;
                            #endregion
                        }
                    default:
                        {
                            DataTable Fdt = arfb.GetFromInfo(FromNo);
                            if (Fdt.Rows.Count > 0)
                            {
                                DataTable Idt = arfb.GetFromItemList(FromNo);
                            }
                            break;
                        }
                }
            }
            return "";
        }
        public SortedList ShowFormTypeList(int SectionType, string PageName)
        {
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
        public SortedList ShowFormTypeList(Model.PGroup pgm, string PageName)
        {
            SortedList al = new SortedList();
            DataSet ds = iburfdlsc.ShowFormTypeList("");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (((ZhiFang.Common.Dictionary.SectionType)pgm.SectionType).ToString().Trim() == ds.Tables[0].Rows[i]["ReportType"].ToString().Trim() && PageName.Trim() == ds.Tables[0].Rows[i]["PageName"].ToString().Trim())
                    {
                        al.Add(ds.Tables[0].Rows[i]["XSLName"].ToString().Trim(), ds.Tables[0].Rows[i]["Name"].ToString().Trim());
                    }
                }
            }
            return al;
        }
        public SortedList ShowFormTypeList(Model.PGroup pgm)
        {
            SortedList al = new SortedList();
            DataSet ds = iburfdlsc.ShowFormTypeList("");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (((ZhiFang.Common.Dictionary.SectionType)pgm.SectionType).ToString().Trim() == ds.Tables[0].Rows[i]["ReportType"].ToString().Trim())
                    {
                        al.Add(ds.Tables[0].Rows[i]["XSLName"].ToString().Trim(), ds.Tables[0].Rows[i]["Name"].ToString().Trim());
                    }
                }
            }
            return al;
        }
        public SortedList ShowFormTypeList(string FromNo, int SectionNo)
        {
            ZhiFang.IBLL.Common.BaseDictionary.IBPGroup pg = ZhiFang.BLLFactory.BLLFactory<ZhiFang.IBLL.Common.BaseDictionary.IBPGroup>.GetBLL("IBPGroup");
            Model.PGroup pgm = pg.GetModel(SectionNo, (int)ZhiFang.Common.Dictionary.SectionTypeVisible.Visible);

            return ShowFormTypeList(pgm);
        }
        public SortedList ShowFormTypeList(string FromNo, int SectionNo, string PageName)
        {
            ZhiFang.IBLL.Common.BaseDictionary.IBPGroup pg = ZhiFang.BLLFactory.BLLFactory<ZhiFang.IBLL.Common.BaseDictionary.IBPGroup>.GetBLL("IBPGroup");
            Model.PGroup pgm = pg.GetModel(SectionNo, (int)ZhiFang.Common.Dictionary.SectionTypeVisible.Visible);

            return ShowFormTypeList(pgm, PageName);
        }
        public SortedList ShowClassList(string PageName)
        {
            ZhiFang.Common.Log.Log.Info("这是ShowClassList(方法)：ShowFrom.cs");
            SortedList al = new SortedList();
            DataSet ds = iburfdlsc.ShowClassList("");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (PageName.Trim() == ds.Tables[0].Rows[i]["PageName"].ToString().Trim())
                    {
                        al.Add(i.ToString().Trim(), ds.Tables[0].Rows[i]["Name"].ToString().Trim());
                    }
                }
            }
            return al;
        }
        public DataTable SetUserImage(DataTable Fdt)
        {
            ZhiFang.Common.Log.Log.Info("这是SetUserImage(方法)：ShowFrom.cs");
            Fdt.Columns.Add("TechnicianUrl", typeof(string));
            Fdt.Columns.Add("OperatorUrl", typeof(string));
            Fdt.Columns.Add("CheckerUrl", typeof(string));
            Fdt.Columns.Add("PrintOperUrl", typeof(string));
            if (Fdt.Rows[0]["Technician"].ToString().Trim() != "")
            {
                string tmpdir = ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("UserImagesURL"));
                string imagetype = ".jpg";
                string tmpfilename = Fdt.Rows[0]["Technician"].ToString().Trim() + imagetype;
                if (!FilesHelper.CheckDirFile(tmpdir, tmpfilename))
                {
                    DataTable dtimage = arfb.GetFromUserImage(Fdt.Rows[0]["Technician"].ToString().Trim());
                    if (dtimage.Rows.Count > 0 && dtimage.Rows[0]["userimage"] != DBNull.Value)
                    {
                        FilesHelper.CreatDirFile(tmpdir, tmpfilename, (Byte[])(dtimage.Rows[0]["userimage"]));
                        Fdt.Rows[0]["TechnicianUrl"] = ZhiFang.Common.Public.ConfigHelper.GetConfigString("LocalhostURL") + ZhiFang.Common.Public.ConfigHelper.GetConfigString("UserImagesURL") + "\\" + tmpfilename;
                    }
                }
                else
                {
                    Fdt.Rows[0]["TechnicianUrl"] = ZhiFang.Common.Public.ConfigHelper.GetConfigString("LocalhostURL") + ZhiFang.Common.Public.ConfigHelper.GetConfigString("UserImagesURL") + "\\" + tmpfilename;
                }
            }
            if (Fdt.Rows[0]["Operator"].ToString().Trim() != "")
            {
                string tmpdir = ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("UserImagesURL"));
                string imagetype = ".jpg";
                string tmpfilename = Fdt.Rows[0]["Operator"].ToString().Trim() + imagetype;
                if (!FilesHelper.CheckDirFile(tmpdir, tmpfilename))
                {
                    DataTable dtimage = arfb.GetFromUserImage(Fdt.Rows[0]["Operator"].ToString().Trim());
                    if (dtimage.Rows.Count > 0 && dtimage.Rows[0]["userimage"] != DBNull.Value)
                    {
                        FilesHelper.CreatDirFile(tmpdir, tmpfilename, (Byte[])(dtimage.Rows[0]["userimage"]));
                        Fdt.Rows[0]["OperatorUrl"] = ZhiFang.Common.Public.ConfigHelper.GetConfigString("LocalhostURL") + ZhiFang.Common.Public.ConfigHelper.GetConfigString("UserImagesURL") + "\\" + tmpfilename;
                    }
                }
                else
                {
                    Fdt.Rows[0]["OperatorUrl"] = ZhiFang.Common.Public.ConfigHelper.GetConfigString("LocalhostURL") + ZhiFang.Common.Public.ConfigHelper.GetConfigString("UserImagesURL") + "\\" + tmpfilename;
                }
            }
            if (Fdt.Rows[0]["Checker"].ToString().Trim() != "")
            {
                string tmpdir = ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("UserImagesURL"));
                string imagetype = ".jpg";
                string tmpfilename = Fdt.Rows[0]["Checker"].ToString().Trim() + imagetype;
                if (!FilesHelper.CheckDirFile(tmpdir, tmpfilename))
                {
                    DataTable dtimage = arfb.GetFromUserImage(Fdt.Rows[0]["Checker"].ToString().Trim());
                    if (dtimage.Rows.Count > 0 && dtimage.Rows[0]["userimage"] != DBNull.Value)
                    {
                        FilesHelper.CreatDirFile(tmpdir, tmpfilename, (Byte[])(dtimage.Rows[0]["userimage"]));
                        Fdt.Rows[0]["CheckerUrl"] = ZhiFang.Common.Public.ConfigHelper.GetConfigString("LocalhostURL") + ZhiFang.Common.Public.ConfigHelper.GetConfigString("UserImagesURL") + "\\" + tmpfilename;
                    }
                }
                else
                {
                    Fdt.Rows[0]["CheckerUrl"] = ZhiFang.Common.Public.ConfigHelper.GetConfigString("LocalhostURL") + ZhiFang.Common.Public.ConfigHelper.GetConfigString("UserImagesURL") + "\\" + tmpfilename;
                }
            }
            if (Fdt.Rows[0]["PrintOper"].ToString().Trim() != "")
            {
                string tmpdir = ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("UserImagesURL"));
                string imagetype = ".jpg";
                string tmpfilename = Fdt.Rows[0]["PrintOper"].ToString().Trim() + imagetype;
                if (!FilesHelper.CheckDirFile(tmpdir, tmpfilename))
                {
                    DataTable dtimage = arfb.GetFromUserImage(Fdt.Rows[0]["PrintOper"].ToString().Trim());
                    if (dtimage.Rows.Count > 0 && dtimage.Rows[0]["userimage"] != DBNull.Value)
                    {
                        FilesHelper.CreatDirFile(tmpdir, tmpfilename, (Byte[])(dtimage.Rows[0]["userimage"]));
                        Fdt.Rows[0]["PrintOperUrl"] = ZhiFang.Common.Public.ConfigHelper.GetConfigString("LocalhostURL") + ZhiFang.Common.Public.ConfigHelper.GetConfigString("UserImagesURL") + "\\" + tmpfilename;
                    }
                }
                else
                {
                    Fdt.Rows[0]["PrintOperUrl"] = ZhiFang.Common.Public.ConfigHelper.GetConfigString("LocalhostURL") + ZhiFang.Common.Public.ConfigHelper.GetConfigString("UserImagesURL") + "\\" + tmpfilename;
                }
            }
            return Fdt;
        }

        public string ShowReportFormFrx(string FromNo, int SectionNo, string PageName, int ShowType)
        {
            ZhiFang.Common.Log.Log.Info("这是ShowReportFormFrx(方法)：ShowFrom.cs");
            ZhiFang.IBLL.Common.BaseDictionary.IBPGroup pg = ZhiFang.BLLFactory.BLLFactory<ZhiFang.IBLL.Common.BaseDictionary.IBPGroup>.GetBLL("IBPGroup");
            Model.PGroup pgm = pg.GetModel(SectionNo, (int)ZhiFang.Common.Dictionary.SectionTypeVisible.Visible);
            string xslstr = ZhiFang.BLL.Common.SectionTypeCommon.GetFormat(((ZhiFang.Common.Dictionary.SectionType)pgm.SectionType).ToString());
            DataSet ds = new DataSet();
            FastReport.Report reportfrx = new FastReport.Report();
            DataTable Fdt = arfb.GetFromInfo(FromNo).Copy();
            Fdt.TableName = "frform";
            DataSet dsform = new DataSet();
            dsform.Tables.Add(Fdt);
            reportfrx.RegisterData(dsform);
            //PDFExport export = new PDFExport();
            HTMLExport htmlexport = new HTMLExport();
            //htmlexport.SubFolder = false;
            htmlexport.SinglePage = true;
            htmlexport.Navigator = false;
            string showmodel = "Normal.frx";
            switch (((ZhiFang.Common.Dictionary.SectionType)pgm.SectionType).ToString())
            {
                case "Normal":
                    {
                        if (Fdt.Rows.Count > 0)
                        {
                            reportfrx.Load(System.AppDomain.CurrentDomain.BaseDirectory + showmodel);
                            Fdt = this.SetUserImage(Fdt);
                            DataTable Idt = arfb.GetFromItemList(FromNo).Copy();
                            Idt.TableName = "fritem";
                            DataSet dsitem = new DataSet();
                            dsitem.Tables.Add(Idt);
                            reportfrx.RegisterData(dsitem);
                            reportfrx.Prepare();

                            if (FilesHelper.CheckAndCreatDir(System.AppDomain.CurrentDomain.BaseDirectory + @"TmpHtmlPath\"))
                            {
                                MemoryStream m = new MemoryStream();
                                //reportfrx.Export(htmlexport, System.AppDomain.CurrentDomain.BaseDirectory + @"TmpHtmlPath\" + FromNo.ToString() + ".html");
                                //htmlexport.Print
                                reportfrx.Export(htmlexport, m);
                                Encoding utf1 = Encoding.GetEncoding("UTF-8");
                                return utf1.GetString(m.ToArray());
                            }
                            // free resources used by report
                            reportfrx.Dispose();

                            return "";
                            //return tmphtml;

                        }
                        break;
                    }
                case "Micro":
                    {
                        arfb.GetFromMicroInfo(FromNo); break;
                    }
                case "NormalIncImage":
                    {
                        //DataTable Fdt = arfb.GetFromInfo(FromNo);
                        if (Fdt.Rows.Count > 0)
                        {
                            Fdt = this.SetUserImage(Fdt);
                            DataTable Idt = arfb.GetFromItemList(FromNo);
                            DataTable Gdt = arfb.GetFromGraphList(FromNo);
                            DataTable ReportGraph = new DataTable();
                            ReportGraph.Columns.Add("Url");
                            ReportGraph.Columns.Add("GraphName");
                            ReportGraph.Columns.Add("GraphNo");
                            ReportGraph.Columns.Add("Type");
                            ReportGraph.Columns.Add("GraphData");
                            for (int i = 0; i < Gdt.Rows.Count; i++)
                            {
                                DateTime tmpdatetime = Convert.ToDateTime(Fdt.Rows[0]["RECEIVEDATE"].ToString().Trim());
                                string tmpdir = ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormImagesURL")) + "\\" + tmpdatetime.Year.ToString() + "\\" + tmpdatetime.Month.ToString() + "\\" + tmpdatetime.Day.ToString() + "\\" + SectionNo.ToString().Trim() + "\\" + FromNo.Trim();
                                string imagetype = ".jpg";
                                switch (Gdt.Rows[i]["GraphNo"].ToString().Trim())
                                {
                                    case "4": imagetype = ".gif"; break;
                                    case "5": imagetype = ".jpg"; break;
                                    case "6": imagetype = ".bmp"; break;
                                    case "7": imagetype = ".gif"; break;
                                }
                                string tmpfilename = Gdt.Rows[i]["GraphNo"].ToString().Trim() + "_" + Gdt.Rows[i]["GraphName"].ToString().Trim() + imagetype;
                                if (Gdt.Rows[0]["Graphjpg"] != DBNull.Value)
                                {
                                    if (!FilesHelper.CheckDirFile(tmpdir, tmpfilename))
                                    {
                                        FilesHelper.CreatDirFile(tmpdir, tmpfilename, (Byte[])(Gdt.Rows[0]["Graphjpg"]));
                                    }
                                    ReportGraph.Rows.Add(ZhiFang.Common.Public.ConfigHelper.GetConfigString("LocalhostURL") + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormImagesURL") + "\\" + tmpdatetime.Year.ToString() + "\\" + tmpdatetime.Month.ToString() + "\\" + tmpdatetime.Day.ToString() + "\\" + SectionNo.ToString().Trim() + "\\" + FromNo.Trim(), Gdt.Rows[i]["GraphName"].ToString().Trim(), Gdt.Rows[i]["GraphNo"].ToString().Trim(), imagetype, "");
                                }
                            }
                            string tmphtml = TransXMLToHtml.TransformXMLIntoHtml(ZhiFang.Common.Public.TransDataToXML.MergeXML(ZhiFang.Common.Public.TransDataToXML.MergeXML(ZhiFang.Common.Public.TransDataToXML.TransformDTIntoXML(Fdt, "WebReportFile", "ReportForm"), ZhiFang.Common.Public.TransDataToXML.TransformDTIntoXML(Idt, "WebReportFile", "ReportItem"), "WebReportFile"), ZhiFang.Common.Public.TransDataToXML.TransformDTIntoXML(ReportGraph, "WebReportFile", "ReportGraph"), "WebReportFile"), ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelShowURL") + "\\" + showmodel));
                            return tmphtml;
                        }
                        break;
                    }
                default:
                    {
                        //DataTable Fdt = arfb.GetFromInfo(FromNo);
                        if (Fdt.Rows.Count > 0)
                        {
                            DataTable Idt = arfb.GetFromItemList(FromNo);
                        }
                        break;
                    }
            }
            return "";
        }
        private void RegeditData(DataTable table, ref FastReport.Report report, string formatPrint)
        {
            ZhiFang.Common.Log.Log.Info("这是RegeditData（）方法:ShowFrom.cs");
            DataSet set3;
            DataTable table2;
            DataSet set4;
            DataTable table3;
            set3 = new DataSet();
            table2 = new DataTable("fritem1");
            set4 = new DataSet();
            table3 = new DataTable("fritem2");
            table.Columns.Add("RowId", typeof(string));
            int num2 = 0;
            while (num2 < table.Rows.Count)
            {
                table.Rows[num2]["RowId"] = (num2 + 1).ToString();
                num2++;
            }
            if (formatPrint.IndexOf("自动单双") >= 0)
            {
                SubreportObject obj3;
                SubreportObject obj4;
                SubreportObject obj5;
                LineObject obj6;
                TextObject obj2 = (TextObject)report.FindObject("Row");
                if (((obj2 != null) && ZhiFang.Tools.Validate.IsInt(obj2.Text.Trim())) && (Convert.ToInt32(obj2.Text.Trim()) < table.Rows.Count))
                {
                    int num3 = int.Parse(obj2.Text.Trim());
                    int num4 = (table.Rows.Count / num3) + 1;
                    if (num4 > 0)
                    {
                        table2 = table.Clone();
                        table3 = table.Clone();
                        for (int num = 0; num < num4; num++)
                        {
                            for (num2 = num * num3; num2 < ((num + 1) * num3); num2++)
                            {
                                if (num2 >= table.Rows.Count)
                                {
                                    break;
                                }
                                if ((num % 2) == 0)
                                {
                                    table2.Rows.Add(table.Rows[num2].ItemArray);
                                }
                                else
                                {
                                    table3.Rows.Add(table.Rows[num2].ItemArray);
                                }
                            }
                        }
                    }
                    table2.TableName = "fritem1";
                    set3.Tables.Add(table2);
                    table3.TableName = "fritem2";
                    set4.Tables.Add(table3);
                    obj3 = (SubreportObject)report.FindObject("Subreport1");
                    obj3.Visible = false;
                    obj4 = (SubreportObject)report.FindObject("Subreport2");
                    obj4.Visible = true;
                    obj5 = (SubreportObject)report.FindObject("Subreport3");
                    obj5.Visible = true;
                    obj6 = (LineObject)report.FindObject("Line7");
                    if (obj6 != null)
                    {
                        obj6.Visible = true;
                    }
                }
                else
                {
                    table2 = table.Copy();
                    table3 = table.Copy();
                    table2.TableName = "fritem1";
                    set3.Tables.Add(table2);
                    table3.TableName = "fritem2";
                    set4.Tables.Add(table3);
                    obj3 = (SubreportObject)report.FindObject("Subreport1");
                    obj3.Visible = true;
                    obj4 = (SubreportObject)report.FindObject("Subreport2");
                    obj4.Visible = false;
                    obj5 = (SubreportObject)report.FindObject("Subreport3");
                    obj5.Visible = false;
                    obj6 = (LineObject)report.FindObject("Line7");
                    if (obj6 != null)
                    {
                        obj6.Visible = false;
                    }
                }
                report.RegisterData(table.DataSet);
                report.RegisterData(set3);
                report.RegisterData(set4);
            }
            else
            {
                report.RegisterData(table.DataSet);
            }

            report.RegisterData(table.DataSet);
        }
        //图片信息
        public void RegeditImage(DataRow dataRow, ref FastReport.Report report)
        {
            ZhiFang.Common.Log.Log.Info("这是RegeditImage（）方法：ShowFrom.cs");
            try
            {
                string path = "";
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportIncludeImage") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportIncludeImage").Trim() != "")
                {
                    if (dataRow != null && dataRow.Table.Columns.Contains("ReceiveDate") && dataRow["ReceiveDate"] != null && dataRow["ReceiveDate"].ToString().Trim() != "")
                    {
                        DateTime datetime = Convert.ToDateTime(dataRow["ReceiveDate"].ToString().Trim());
                        string date = "";
                        date = datetime.ToString("yyyy-MM-dd");
                        string[] ArrayDate = date.Split('-');
                        string linshiFormNo = "";
                        try
                        {
                            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("LinSFormNo") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("LinSFormNo").Trim() != "" && ZhiFang.Common.Public.ConfigHelper.GetConfigString("LinSFormNo").Trim() == "1")
                            {//TestTypeNo   检测类型编号
                                //SampleNo	样本号
                                linshiFormNo = datetime.Year + "-" + ArrayDate[1] + "-" + ArrayDate[2] + ";" + dataRow["SectionNo"] + ";" +
                                    dataRow["TestTypeNo"] + ";" + dataRow["SampleNo"];
                            }
                            else
                            {
                                linshiFormNo = dataRow["FormNo"].ToString();
                            }
                        }
                        catch (Exception)
                        {
                            linshiFormNo = dataRow["FormNo"].ToString();
                        }
                        path = ConfigHelper.GetConfigString("ReportIncludeImage").Trim() + "\\" + Convert.ToDateTime(datetime.ToString("yyyy-MM-dd")).Year + "\\" + Convert.ToDateTime(datetime.ToString("yyyy-MM-dd")).Month + "\\" + Convert.ToDateTime(datetime.ToString("yyyy-MM-dd")).Day + "\\" + linshiFormNo + "\\";
                        ZhiFang.Common.Log.Log.Info(path);
                        if (ZhiFang.Common.Public.FilesHelper.CheckDirectory(path))
                        {
                            string[] files = Directory.GetFiles(path);
                            for (int i = 0; i < files.Count(); i++)
                            {
                                string[] a = files[i].Split('@');
                                if (a.Count() > 1)
                                {
                                    if (a[3] == "NameImage")
                                    {
                                        //= (PictureObject)report.FindObject("PV" + (i + 1).ToString());
                                        PictureObject p = (PictureObject)report.FindObject(a[3] + a[5]);
                                        ZhiFang.Common.Log.Log.Info("NameImage图片" + a[3] + a[5]);
                                        //if (p != null)
                                        //{
                                        //    byte[] tmpa = (byte[])File.ReadAllBytes(path+"\\"+files[i]);
                                        //    if (tmpa != null && tmpa.Length > 0)
                                        //    {
                                        //        System.IO.MemoryStream aaa = new System.IO.MemoryStream(tmpa);
                                        //        System.Drawing.Image ia = Image.FromStream(aaa);
                                        //        p.Image = ia;
                                        //    }
                                        //}
                                        if (p != null)
                                        {
                                            p.ImageLocation = files[i];
                                            ZhiFang.Common.Log.Log.Info("NameImage图片名称" + files[i]);
                                        }
                                    }
                                    if (a[3] == "RFGraphData")
                                    {
                                        PictureObject p = (PictureObject)report.FindObject(a[3] + (int.Parse(a[2]) + 1).ToString());
                                        ZhiFang.Common.Log.Log.Info("RFGraphData图片" + a[3] + (int.Parse(a[2]) + 1).ToString());
                                        //if (p != null)
                                        //{
                                        //    byte[] tmpa = (byte[])File.ReadAllBytes(path + "\\" + files[i]);
                                        //    if (tmpa != null && tmpa.Length > 0)
                                        //    {
                                        //        System.IO.MemoryStream aaa = new System.IO.MemoryStream(tmpa);
                                        //        System.Drawing.Image ia = Image.FromStream(aaa);
                                        //        p.Image = ia;
                                        //    }
                                        //}
                                        if (p != null)
                                        {
                                            p.ImageLocation = files[i];
                                            ZhiFang.Common.Log.Log.Info("RFGraphData图片名称" + files[i]);
                                        }
                                    }
                                    if (a[3] == "S_RequestVItem")
                                    {
                                        PictureObject p = (PictureObject)report.FindObject("PV" + (int.Parse(a[2]) + 1).ToString());
                                        ZhiFang.Common.Log.Log.Info("S_RequestVItem图片PV" + (int.Parse(a[2]) + 1).ToString());
                                        //if (p != null)
                                        //{
                                        //    byte[] tmpa = (byte[])File.ReadAllBytes(path + "\\" + files[i]);
                                        //    if (tmpa != null && tmpa.Length > 0)
                                        //    {
                                        //        System.IO.MemoryStream aaa = new System.IO.MemoryStream(tmpa);
                                        //        System.Drawing.Image ia = Image.FromStream(aaa);
                                        //        p.Image = ia;
                                        //    }
                                        //}
                                        if (p != null)
                                        {
                                            p.ImageLocation = files[i];
                                            ZhiFang.Common.Log.Log.Info("S_RequestVItem图片名称" + files[i]);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(e.ToString() + "---------" + e.StackTrace.ToString() + "---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
            }
        }
        #endregion



        #region 读取xml配置展示报告
        /// <summary>
        /// 读取xml配置展示报告
        /// </summary>
        /// <param name="FromNo"></param>
        /// <param name="PageName"></param>
        /// <param name="ShowType"></param>
        /// <param name="SectionType"></param>
        /// <param name="TemplateType">返回执行模版类型方法</param>
        /// <returns>模版路径</returns>
        public string ShowReportXML(string FromNo, string PageName, string ShowType, string SectionType, out int TemplateType)
        {
            //判断大组 大组转换类型
            int SectionT = Convert.ToInt32(SectionType);
            //TemplateType调用相应模版类型方法：0 xslt(xml)、1 FR3(Deliph)、2 FRX(FastReport)
            TemplateType = 0;
            //读取配置文件
            string ShowModel = "Normal.XSLT";
            int ShowT = ZhiFang.Common.Public.Valid.ToInt(ShowType);
            try
            {
                SortedList al = this.ShowFormTypeList(Convert.ToInt32(SectionType), PageName);
                if (al.Count <= 0)
                { return "显示模板配置文件错误！"; }
                else
                {
                    try
                    { ShowModel = al.GetKey(ShowT).ToString(); }
                    catch
                    { ShowModel = al.GetKey(0).ToString(); }
                }
                //读取配置 依照后缀名 判断展示方式 16,TCT.frx;11,TCT.frx
                string[] Model = ShowModel.ToUpper().Trim().Split('.');
                TemplateType = SwitchType(TemplateType, Model);
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("异常信息:" + ex.ToString());
                return "显示模板配置错误！";
            }
            return ShowModel;
        }
        /// <summary>
        /// 模版类
        /// </summary>
        /// <param name="TemplateType"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        private static int SwitchType(int TemplateType, string[] Model)
        {
            switch (Model[Model.Length - 1])//变更 取最后的
            {
                case "XSLT":
                    TemplateType = 0;
                    break;
                case "FR3":
                    TemplateType = 1;
                    break;
                case "FRX":
                    TemplateType = 2;
                    break;
                default:
                    ZhiFang.Common.Log.Log.Info("ReportFromShowXslConfig配置问题");
                    break;
            }
            return TemplateType;
        }
        #endregion

        #region 读取数据库配置展示报告
        /// <summary>
        /// 读取数据库配置展示报告
        /// </summary>
        /// <param name="FromNo"></param>
        /// <param name="dtrf"></param>
        /// <param name="dtTalbe"></param>
        /// <param name="SectionType"></param>
        /// <param name="TemplateType">返回执行模版类型方法</param>
        /// <returns>模版路径</returns>
        public string ShowReportSqlDB(string FromNo, DataTable dtrf, DataTable dtTalbe, string SectionType, out int TemplateType)
        {
            TemplateType = 0;
            string ShowModel = "Normal.XSLT";
            string path = System.AppDomain.CurrentDomain.BaseDirectory + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\";
            ShowModel = this.FindMode(dtrf.Rows[0], dtTalbe, this.titelflag);
            //读取配置 依照后缀名 判断展示方式 16,TCT.frx;11,TCT.frx
            string[] Model = ShowModel.ToUpper().Trim().Split('.');
            TemplateType = SwitchType(TemplateType, Model);
            return ShowModel;
        }
        #endregion
        #region  判断大组
        /// <summary>
        /// 判断大组 小组
        /// </summary>
        /// <param name="FromNo"></param>
        /// <param name="SectionType"></param>
        /// <param name="dtrf"></param>
        /// <param name="dtTable">项目表</param>
        /// <param name="dtGraph">图片</param>
        /// <returns>返回表名</returns>
        public string CheckSupergroup(string FromNo, int SectionType, out DataTable dtrf, out DataTable dtTable, out DataTable dtGraph)
        {
            ALLReportForm_Weblis arfb_w = new ALLReportForm_Weblis();
            string ShowName = "ReportItem";
            ZhiFang.IBLL.Report.IBReportFormFull rffb = ZhiFang.BLLFactory.BLLFactory<IBReportFormFull>.GetBLL("ReportFormFull");
            ZhiFang.IBLL.Report.IBReportItemFull rifb = ZhiFang.BLLFactory.BLLFactory<IBReportItemFull>.GetBLL("ReportItemFull");
            ZhiFang.IBLL.Report.IBReportMicroFull rmfb = ZhiFang.BLLFactory.BLLFactory<IBReportMicroFull>.GetBLL("ReportMicroFull");
            ZhiFang.IBLL.Report.IBReportMarrowFull rmarrowfb = ZhiFang.BLLFactory.BLLFactory<IBReportMarrowFull>.GetBLL("ReportMarrowFull");
            ZhiFang.IBLL.Common.BaseDictionary.IBCLIENTELE cl = ZhiFang.BLLFactory.BLLFactory<IBCLIENTELE>.GetBLL();
            Model.CLIENTELE cl_m = new Model.CLIENTELE();
            Model.ReportFormFull rff_m = new Model.ReportFormFull();
            Model.ReportItemFull rif_m = new Model.ReportItemFull();
            Model.ReportMicroFull rmf_m = new Model.ReportMicroFull();
            Model.ReportMarrowFull rmarrowf_m = new Model.ReportMarrowFull();
            rff_m.ReportFormID = FromNo;
            DataSet dsrf = rffb.GetList(rff_m);
            try
            {
                //添加送检单位列
                dsrf.Tables[0].Columns.Add("WEBLISSOURCEORGNAME", typeof(string));
                if (dsrf != null && dsrf.Tables.Count > 0 && dsrf.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsrf.Tables[0].Rows.Count; i++)
                    {
                        cl_m = cl.GetModel(Convert.ToInt64(dsrf.Tables[0].Rows[i]["WeblisSourceOrgId"]));
                        dsrf.Tables[0].Rows[i]["WEBLISSOURCEORGNAME"] = cl_m.CNAME;
                    }
                }
            }
            catch (Exception)
            {
            }

            dtrf = dsrf.Tables[0];
            dtTable = null;
            dtGraph = null;
            switch (((ZhiFang.Common.Dictionary.SectionType)SectionType))
            {
                case ZhiFang.Common.Dictionary.SectionType.Normal:
                    dtTable = arfb_w.GetFromItemList(FromNo);
                    ShowName = "ReportItem";
                    //modelType = "CHAM";
                    break;
                case ZhiFang.Common.Dictionary.SectionType.Micro:
                    rmf_m.ReportFormID = FromNo;
                    dtTable = rmfb.GetList(rmf_m).Tables[0];
                    ShowName = "ReportMico";
                    //modelType = "MICROBE";
                    break;
                case ZhiFang.Common.Dictionary.SectionType.NormalIncImage:
                    ShowName = "ReportItem";
                    dtTable = arfb_w.GetFromItemList(FromNo);
                    break;
                case ZhiFang.Common.Dictionary.SectionType.MicroIncImage:
                    dtTable = arfb_w.GetFromItemList(FromNo);
                    ShowName = "ReportItem";
                    //modelType = "MICROBE";
                    break;
                case ZhiFang.Common.Dictionary.SectionType.CellMorphology:
                    dtTable = arfb.GetMarrowItemList(FromNo);
                    dtGraph = arfb.GetFromGraphList(FromNo);
                    //modelType = "MARROW";
                    break;
                case ZhiFang.Common.Dictionary.SectionType.FishCheck:
                    ShowName = "ReportMarrow";
                    //modelType = "MARROW";
                    rmarrowf_m.ReportFormID = FromNo;
                    dtTable = rmarrowfb.GetList(rmarrowf_m).Tables[0];
                    break;
                case ZhiFang.Common.Dictionary.SectionType.SensorCheck:
                    rmarrowf_m.ReportFormID = FromNo;
                    dtTable = rmarrowfb.GetList(rmarrowf_m).Tables[0];
                    //modelType = "MARROW";
                    break;
                case ZhiFang.Common.Dictionary.SectionType.ChromosomeCheck:
                    rmarrowf_m.ReportFormID = FromNo;
                    dtTable = rmarrowfb.GetList(rmarrowf_m).Tables[0];
                    // modelType = "MARROW";
                    break;
                case ZhiFang.Common.Dictionary.SectionType.PathologyCheck:
                    rmarrowf_m.ReportFormID = FromNo;
                    dtTable = rmarrowfb.GetList(rmarrowf_m).Tables[0];
                    // modelType = "MARROW";
                    break;
            }
            return ShowName;
        }
        #endregion

        #region 判断小组
        public string GetTeam(string ShowModel,DataTable dtrf)
        {

            string[] modelList = ShowModel.Split(';');
            if (modelList.Length > 1)
            {
                ShowModel = "";
                for (int i = 0; i < modelList.Length; i++)
                {
                    if (dtrf.Rows[0]["sectionno"].ToString().Trim() == modelList[i].Split(',')[0])
                    {
                        ShowModel = modelList[i].Split(',')[1];
                    }
                }
                if (ShowModel == "")
                {
                    ShowModel = modelList[0].Split(',')[1];
                }
            }
            return ShowModel;
        }
        #endregion

        #region xslt显示
        public string ShowReportXSLT(DataTable dtrf, DataTable dtTable, string ShowName, string ShowModel)
        {
            if (ShowName == "")
            {
                ShowName = "ReportItem";
            }
            string tmphtml = TransXMLToHtml.TransformXMLIntoHtml(ZhiFang.Common.Public.TransDataToXML.MergeXML(ZhiFang.Common.Public.TransDataToXML.TransformDTIntoXML(dtrf, "WebReportFile", "ReportForm"), ZhiFang.Common.Public.TransDataToXML.TransformDTIntoXML(dtTable, "WebReportFile", "" + ShowName + ""), "WebReportFile"), ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelShowURL") + "\\" + ShowModel));
            return tmphtml;
        }
        #endregion
        #region FRX显示
        public string ShowReportFRX(string FromNo, DataTable dtrf, DataTable dtTable, string ShowName, string ShowModel)
        {
            FastReport.Report report = new FastReport.Report();
            ShowModel = ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelShowURL") + "\\" + ShowModel);
            report.Load(ShowModel);
            if (ShowModel.Trim().Length <= 0)
            {
                return null;
                //return "暂无匹配模板！";
            }
            dtrf.TableName = "frform";
            dtTable.TableName = "frmicro";
            report.Load(ShowModel);
            RegeditData(dtTable, ref report, ShowModel);

            report.RegisterData(dtrf.DataSet);
            RegeditImage(dtrf.Rows[0], ref report);
            report.Prepare();
            FastReport.Export.Image.ImageExport tmpjpg = new FastReport.Export.Image.ImageExport();
            string imagetype = "jpeg";
            if (ZhiFang.Common.Public.ConfigHelper.GetConfigInt("JpegQuality") != 0)
            {
                tmpjpg.JpegQuality = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("JpegQuality");
            }

            switch (ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFileType").ToUpper())
            {
                case "JPEG": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg; imagetype = "jpeg"; break;
                case "JPG": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg; imagetype = "jpg"; break;
                case "GIF": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Gif; imagetype = "gif"; break;
                case "BMP": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Bmp; imagetype = "bmp"; break;
                case "PNG": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Png; imagetype = "png"; break;
                case "TIFF": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Tiff; imagetype = "tiff"; break;
                default: tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg; imagetype = "jpeg"; break;
            }
            report.Export(tmpjpg, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + FromNo.Replace(" 00:00:00", "").Replace(",", "&") + "@." + imagetype + "");
            report.Dispose();

            List<string> imagepath = tmpjpg.GeneratedFiles;
            string tmphtml = "";
            foreach (string a in imagepath)
            {
                tmphtml += "<img src=\"" + "../" + a.Substring(a.LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")), a.Length - a.LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir"))) + "\"  /><br>";
            }
            return tmphtml;

        }
        #endregion
        #region FR3显示
        /// <summary>
        /// 标题
        /// </summary>
        private ZhiFang.Common.Dictionary.ReportFormTitle titelflag = ZhiFang.Common.Dictionary.ReportFormTitle.center;
       
        #endregion
        /// <summary>
        /// 返回路径
        /// </summary>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        public string GetImagePath(DataRow dataRow)
        {
            try
            {
                string path = "";
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportIncludeImage") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportIncludeImage").Trim() != "")
                {
                    if (dataRow != null && dataRow.Table.Columns.Contains("ReceiveDate") && dataRow["ReceiveDate"] != null && dataRow["ReceiveDate"].ToString().Trim() != "")
                    {
                        DateTime datetime = Convert.ToDateTime(dataRow["ReceiveDate"].ToString().Trim());
                        path = ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportIncludeImage").Trim() + "\\" + datetime.Year + "\\" + datetime.Month + "\\" + datetime.Day + "\\" + dataRow["FormNo"].ToString() + "\\";
                    }
                }
                return path;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(e.ToString() + "---------" + e.StackTrace.ToString() + "---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
                return null;
            }
        }
        ZhiFang.IBLL.Common.BaseDictionary.IBPGroupPrint PGroupPrint = ZhiFang.BLLFactory.BLLFactory<ZhiFang.IBLL.Common.BaseDictionary.IBPGroupPrint>.GetBLL();
        ZhiFang.IBLL.Common.BaseDictionary.IBPrintFormat pfb = ZhiFang.BLLFactory.BLLFactory<ZhiFang.IBLL.Common.BaseDictionary.IBPrintFormat>.GetBLL();
        /// <summary>
        /// 数据库找模版
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="table"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        private string FindMode(DataRow dr, DataTable table, ReportFormTitle flag)
        {
            return FindMode(dr, table, null, flag);
        }
        private string FindMode(DataRow dr, DataTable table, DataTable table1, ReportFormTitle flag)
        {
            Log.Info("这是FindMode()方法:ShowFrom.cs");
            try
            {
                string path = "";
                int h = 0;
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportIncludeImage") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportIncludeImage").Trim() != "")
                {
                    if (dr != null && dr.Table.Columns.Contains("ReceiveDate") && dr["ReceiveDate"] != null && dr["ReceiveDate"].ToString().Trim() != "")
                    {
                        DateTime datetime = Convert.ToDateTime(dr["ReceiveDate"].ToString().Trim());
                        path = ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportIncludeImage").Trim() + "\\" + datetime.Year + "\\" + datetime.Month + "\\" + datetime.Day + "\\" + dr["FormNo"].ToString() + "\\";
                        if (ZhiFang.Common.Public.FilesHelper.CheckDirectory(path))
                        {
                            string[] files = Directory.GetFiles(path);

                            for (int i = 0; i < files.Length; i++)
                            {
                                if (files[i].ToString().IndexOf("S_RequestVItem") > 0)
                                {
                                    h = h + 1;
                                }
                            }
                        }
                    }
                }
                DataTable table4;
                string str5;
                string formatPrint = "";
                Model.PGroupPrint t = new Model.PGroupPrint();
                try
                {
                    t.SectionNo = int.Parse(dr["SectionNo"].ToString());
                }
                catch
                {
                    t.SectionNo = -1;
                }
                t.UseFlag = 1;
                if (flag == ReportFormTitle.BatchPrint)
                {
                    t.BatchPrint = 1;
                }
                if (flag == ReportFormTitle.center)
                {
                    t.ModelTitleType = 1;
                }
                if (flag == ReportFormTitle.client)
                {
                    t.ModelTitleType = 0;
                }
                if (Convert.ToInt32(flag) > 2)
                {
                    t.SickTypeNo = Convert.ToInt32(flag) - 2;
                }
                table4 = this.PGroupPrint.GetList(t).Tables[0];
                if (table4.Rows.Count <= 0)
                {
                    return null;
                }
                DataTable tmp = new DataTable();
                if (h > 0)
                {
                    if (table4 != null && table4.Rows.Count > 0)
                    {
                        DataRow[] dra = table4.Select();
                        formatPrint = PGroupPrint.PrintFormatFilter_Weblis(dra, h);
                    }
                    DataRow[] dra1 = table4.Select("PrintFormatNo=" + formatPrint);
                    tmp = table4.Clone();
                    DataRow dr1 = dra1[0];
                    tmp.ImportRow(dr1);
                }
                else
                {
                    tmp = table4;
                }
                if (tmp.Select("SpecialtyItemNo is null", "Sort").Count<DataRow>() != tmp.Rows.Count)
                {
                    if (tmp.Select("SpecialtyItemNo is null and ItemMaxNumber is null and ItemMinNumber is null ", "Sort").Count<DataRow>() != tmp.Rows.Count)
                    {
                        str5 = "";
                        if (table != null && table.Rows.Count > 0)
                        {
                            for (int num = 0; num < table.Rows.Count; num++)
                            {
                                str5 = str5 + table.Rows[num]["itemno"].ToString() + ",";
                            }
                        }
                        if (table1 != null && table1.Rows.Count > 0)
                        {
                            for (int num = 0; num < table1.Rows.Count; num++)
                            {
                                str5 = str5 + table1.Rows[num]["itemno"].ToString() + ",";
                            }
                        }
                        if (str5 != "")
                        {
                            str5 = str5.Substring(0, str5.LastIndexOf(","));
                        }
                        if ((tmp.Select("clientno is null ", "Sort").Count<DataRow>() == tmp.Rows.Count) || (dr["clientno"].ToString().Trim().Length < 0))
                        {
                            formatPrint = this.PGroupPrint.GetFormatPrint(str5, tmp);
                        }
                        else
                        {
                            formatPrint = this.PGroupPrint.GetFormatPrint(Convert.ToInt32(dr["clientno"].ToString()), str5, tmp);
                        }
                    }
                }
                else
                {

                    if ((tmp.Select("clientno is null ", "Sort").Count<DataRow>() != tmp.Rows.Count) && (dr["clientno"].ToString().Trim().Length >= 0))
                    {
                        formatPrint = tmp.Select(" 1=1 ", " Sort asc,Id desc ")[0]["PrintFormatNo"].ToString().Trim();
                    }
                    if (dr["clientno"] != DBNull.Value)
                    {

                        formatPrint = this.PGroupPrint.GetFormatPrint(new int?(Convert.ToInt32(dr["clientno"].ToString())), tmp);
                    }
                    else
                    {
                        int? clientno = null;
                        formatPrint = this.PGroupPrint.GetFormatPrint(clientno, tmp);
                    }
                }
                string format = "";
                try
                {
                    Model.PrintFormat pf_m = pfb.GetModel(formatPrint);
                    string showmodel = pf_m.PintFormatAddress.ToString().Trim() + "\\" + pf_m.Id + "\\" + pf_m.Id + ".fr3";
                    formatPrint = ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelURL") + "\\" + showmodel);
                    format = formatPrint.Replace("//", "").ToString();
                    ZhiFang.Common.Log.Log.Info("模板路径：" + format);
                }
                catch (Exception e)
                {
                    ZhiFang.Common.Log.Log.Debug(e.StackTrace.ToString() + "---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
                    return "";
                }
                return format;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(e.StackTrace.ToString() + "---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
                return "";
            }
        }

        public DataTable GerReportFormAndItemData(string fromNo, int sectiontype, out DataTable dsrf_Out)
        {
            dsrf_Out = null;
            return null;
        }

        public string GetTemplatePath(DataTable dtrf, DataTable dtri, string pageName, int showType, int sectiontype)
        {
            return "";
        }
    }
}
