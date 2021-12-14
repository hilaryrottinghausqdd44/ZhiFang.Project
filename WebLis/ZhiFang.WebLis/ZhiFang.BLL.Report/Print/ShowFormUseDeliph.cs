using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using ZhiFang.IBLL.Report;
using ZhiFang.DALFactory;
using System.Data;
using System.Collections;
using ZhiFang.Common.Public;
using FastReport;
using System.IO;
using FastReport.Export.Html;
using System.Runtime.InteropServices;
using System.Configuration;
using ZhiFang.Common.Log;
using ZhiFang.Common.Dictionary;
using ZhiFang.IDAL;
using ZhiFang.IBLL.Common;
using ZhiFang.DALFactory;
using ZhiFang.IBLL.Common.BaseDictionary;
using System.Xml;
using ZhiFang.BLL.Report.Print;

namespace ZhiFang.BLL.Report
{
    public class ShowFormUseDeliph : ZhiFang.IBLL.Report.IBShowFrom
    {
        #region IBShowFrom 成员
        [DllImport("ModelPrint.dll", EntryPoint = "PrintReport")]
        public static extern Boolean PrintReport(IntPtr ConnectionString, IntPtr ModelName, IntPtr ModelType, IntPtr PrintID, IntPtr SaveType, IntPtr SavePath, IntPtr PicturePath, IntPtr LogPath, IntPtr where);
        protected ALLReportForm arfb = new ALLReportForm();
        ZhiFang.BLL.Report.ReportFormFull rffb = new ReportFormFull();
        ZhiFang.BLL.Report.ReportItemFull rifb = new ReportItemFull();
        ZhiFang.BLL.Report.ReportMicroFull rmfb = new ReportMicroFull();
        ZhiFang.BLL.Report.ReportMarrowFull rmarrowfb = new ReportMarrowFull();
        private readonly IDReportFormMerge dalmerge = DalFactory<IDReportFormMerge>.GetDalByClassName("ReportFormMerge");
        ZhiFang.IBLL.Common.BaseDictionary.IBPGroupPrint PGroupPrint = ZhiFang.BLLFactory.BLLFactory<ZhiFang.IBLL.Common.BaseDictionary.IBPGroupPrint>.GetBLL();
        ZhiFang.IBLL.Common.BaseDictionary.IBPrintFormat pfb = ZhiFang.BLLFactory.BLLFactory<ZhiFang.IBLL.Common.BaseDictionary.IBPrintFormat>.GetBLL();
        protected readonly ZhiFang.IBLL.Report.IBUserReportFormDataListShowConfig iburfdlsc = ZhiFang.BLLFactory.BLLFactory<IBUserReportFormDataListShowConfig>.GetBLL("UserReportFormDataListShowConfig");
        public string ShowReportForm(string FromNo, string ShowReportFormName)
        {
            throw new NotImplementedException();
        }
        public int GetPrintFormatNo(int printformatno)
        {
            return printformatno > 0 ? printformatno : 0;
        }
        private ZhiFang.Common.Dictionary.ReportFormTitle titelflag = ZhiFang.Common.Dictionary.ReportFormTitle.center;

        public ReportFormTitle TitleFlag
        {
            set { titelflag = value; }
            get { return titelflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="table"></param>
        /// <param name="flag">台头</param>
        /// <returns></returns>
        private string FindMode(DataRow dr, DataTable table, ReportFormTitle flag)
        {
            return FindMode(dr, table, null, flag);
        }
        private string FindMode(DataRow dr, DataTable table, DataTable table1, ReportFormTitle flag)
        {
            Log.Info("这是FindMode()方法:ShowFormUseDeliph.cs");
            try
            {
                System.IO.TextWriter tw0 = new System.IO.StringWriter();
                //table.WriteXml(tw0);
                //string xml0 = tw0.ToString();
                // Log.Info("xml0:" + xml0);
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
                                Log.Info("path:" + files[i]);
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
                    Log.Info("SectionNo:" + dr["SectionNo"].ToString());
                    t.SectionNo = int.Parse(dr["SectionNo"].ToString());
                }
                catch
                {
                    Log.Info("SectionNo:-1");
                    t.SectionNo = -1;
                }
                t.UseFlag = 1;
                Log.Info("flag:" + flag);
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
                //dt写成Xml结构
                System.IO.TextWriter tw = new System.IO.StringWriter();
                table4.WriteXml(tw);
                string xml = tw.ToString();

                Log.Info("table4.count:" + table4.Rows.Count);
                Log.Info("table4.XML:" + xml);
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

                //dt写成Xml结构
                System.IO.TextWriter tw2 = new System.IO.StringWriter();
                tmp.WriteXml(tw2);
                string xml2 = tw.ToString();
                Log.Info("tmp.XML:" + xml);

                if (tmp.Select("SpecialtyItemNo is null", "Sort").Count<DataRow>() != tmp.Rows.Count)
                {
                    Log.Info("SpecialtyItemNo is null:1");
                    if (tmp.Select("SpecialtyItemNo is null and ItemMaxNumber is null and ItemMinNumber is null ", "Sort").Count<DataRow>() != tmp.Rows.Count)
                    {
                        Log.Info("SpecialtyItemNo is null and ItemMaxNumber is null and ItemMinNumber is null:1");
                        str5 = "";
                        if (table != null && table.Rows.Count > 0)
                        {
                            Log.Info("table>0:" + table.Rows.Count);
                            for (int num = 0; num < table.Rows.Count; num++)
                            {
                                str5 = str5 + table.Rows[num]["itemno"].ToString() + ",";
                            }
                        }
                        if (table1 != null && table1.Rows.Count > 0)
                        {
                            Log.Info("table1>0:" + table1.Rows.Count);
                            for (int num = 0; num < table1.Rows.Count; num++)
                            {
                                str5 = str5 + table1.Rows[num]["itemno"].ToString() + ",";
                            }
                        }
                        if (str5 != "")
                        {
                            Log.Info("str5 != ''");
                            str5 = str5.Substring(0, str5.LastIndexOf(","));
                            Log.Info("str5:" + str5);
                        }
                        if ((tmp.Select("clientno is null ", "Sort").Count<DataRow>() == tmp.Rows.Count) || (dr["clientno"].ToString().Trim().Length < 0))
                        {
                            Log.Info("clientno is null:1");
                            formatPrint = this.PGroupPrint.GetFormatPrint(str5, tmp);
                            Log.Info("formatPrint:" + formatPrint);
                        }
                        else
                        {
                            Log.Info("clientno is null:2");
                            formatPrint = this.PGroupPrint.GetFormatPrint(Convert.ToInt32(dr["clientno"].ToString()), str5, tmp);
                        }
                    }
                }
                else
                {
                    Log.Info("SpecialtyItemNo is null:2");
                    if ((tmp.Select("clientno is null ", "Sort").Count<DataRow>() != tmp.Rows.Count) && (dr["clientno"].ToString().Trim().Length >= 0))
                    {
                        formatPrint = tmp.Select(" 1=1 ", " Sort asc,Id desc ")[0]["PrintFormatNo"].ToString().Trim();
                    }
                    if (dr["clientno"] != DBNull.Value)
                    {
                        Log.Info("dr['clientno'] != DBNull.Value:1");
                        formatPrint = this.PGroupPrint.GetFormatPrint(new int?(Convert.ToInt32(dr["clientno"].ToString())), tmp);
                    }
                    else
                    {
                        Log.Info("dr['clientno'] != DBNull.Value:2");
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
            //return "手工免疫.fr3";
        }

        /// <summary>
        /// xslt   Micro(pgm.SectionType)没有实现
        /// </summary>
        /// <param name="FromNo"></param>
        /// <param name="SectionNo"></param>
        /// <param name="PageName"></param>
        /// <param name="ShowType"></param>
        /// <returns></returns>
        public string ShowReportForm(string FromNo, int SectionNo, string PageName, int ShowType)
        {
            Log.Info("这是ShowReportForm()方法:ShowFormUseDeliph.cs");
            ZhiFang.IBLL.Common.BaseDictionary.IBPGroup pg = ZhiFang.BLLFactory.BLLFactory<ZhiFang.IBLL.Common.BaseDictionary.IBPGroup>.GetBLL("IBPGroup");
            Model.PGroup pgm = pg.GetModel(SectionNo, (int)ZhiFang.Common.Dictionary.SectionTypeVisible.Visible);
            //string xslstr = SectionTypeCommon.GetFormat(((Common.SectionType)pgm.SectionType).ToString());
            DataSet ds = new DataSet();
            #region 找xml.config模版
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
            #endregion
            switch (((ZhiFang.Common.Dictionary.SectionType)pgm.SectionType))
            {
                case ZhiFang.Common.Dictionary.SectionType.Normal:
                    #region
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
                    #region
                    {
                        arfb.GetFromMicroInfo(FromNo); break;
                    }
                    #endregion
                case ZhiFang.Common.Dictionary.SectionType.NormalIncImage:
                    #region
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
                case ZhiFang.Common.Dictionary.SectionType.CellMorphology:
                    #region
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
        /// 用Fr3显示 有fr3与xsl  Template参数判断
        /// </summary>
        /// <param name="FromNo"></param>
        /// <param name="SectionNo"></param>
        /// <param name="PageName"></param>
        /// <param name="ShowType"></param>
        /// <param name="sectiontype"></param>
        /// <returns></returns>
        public string ShowReportForm_Weblis(string FromNo, int SectionNo, string PageName, int ShowType, int sectiontype)
        {
            ZhiFang.Common.Log.Log.Info("这是ShowReportForm_Weblis()方法：ShowFromUseDeliph.cs");
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
            bool result = false;
            rff_m.ReportFormID = FromNo;
            DataSet dsrf = rffb.GetList(rff_m);
            #region 添加送检单位列
            //dsrf.Tables[0].Columns.Add("WEBLISSOURCEORGNAME", typeof(string));
            //if (dsrf != null && dsrf.Tables.Count > 0 && dsrf.Tables[0].Rows.Count > 0)
            //{
            //    for (int i = 0; i < dsrf.Tables[0].Rows.Count; i++)
            //    {
            //        cl_m = cl.GetModel(Convert.ToInt64(dsrf.Tables[0].Rows[i]["WeblisSourceOrgId"]));
            //        dsrf.Tables[0].Rows[i]["WEBLISSOURCEORGNAME"] = cl_m.CNAME;
            //    }
            //} 
            #endregion
            DataTable dtrf = new DataTable("frform");
            DataTable dtri = new DataTable();
            try
            {
                Log.Info("sectiontype:" + sectiontype + "PageName:" + PageName);
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
                    ZhiFang.Common.Log.Log.Info("显示模板:" + showmodel);
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("异常信息:" + ex.ToString());
                return "显示模板配置错误！";
            }
            if (dsrf.Tables.Count > 0 && dsrf.Tables[0].Rows.Count > 0)
            {
                dtrf = dsrf.Tables[0];
                dtrf.TableName = "frform";
                string modelType = "";
                string modelname = "";
                string path = System.AppDomain.CurrentDomain.BaseDirectory + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\";
                Log.Info("小组类型(sectiontype):" + ((ZhiFang.Common.Dictionary.SectionType)sectiontype).ToString());
                switch (((ZhiFang.Common.Dictionary.SectionType)sectiontype))
                {
                    case ZhiFang.Common.Dictionary.SectionType.Normal:
                        {
                            #region Normal
                            DataTable Fdt = dtrf;
                            if (Fdt.Rows.Count > 0)
                            {
                                Log.Info("Template:" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("Template"));
                                //读取Config 来抉择使用默认模版是打印模版
                                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("Template") == "1")
                                {
                                    rif_m.ReportFormID = rff_m.ReportFormID;
                                    dsri = rifb.GetList(rif_m);
                                    if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                                    {
                                        dtri = dsri.Tables[0];
                                    }
                                    modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, this.titelflag);
                                    string model = modelname.Replace("//", "");
                                    if (modelname.Trim().Length <= 0)
                                    {
                                        return null;
                                        //return "暂无匹配模板！";
                                    }
                                    modelType = "CHAM";
                                    string imagetype = "jpg";
                                    IntPtr ConnectionString = Marshal.StringToHGlobalAnsi(ConfigurationManager.ConnectionStrings["WebLisDB"].ToString());
                                    IntPtr modelNameDeliph = Marshal.StringToHGlobalAnsi(model);
                                    IntPtr modelTypeDeliph = Marshal.StringToHGlobalAnsi(modelType);
                                    IntPtr PrintID = Marshal.StringToHGlobalAnsi(FromNo);
                                    IntPtr saveType = Marshal.StringToHGlobalAnsi(imagetype);
                                    IntPtr SavePath = Marshal.StringToHGlobalAnsi(path);
                                    IntPtr Path = Marshal.StringToHGlobalAnsi(GetImagePath(dsrf.Tables[0].Rows[0]));
                                    Log.Info("生成文件PrintReport:Columns" + dsrf.Tables[0].Columns);
                                    IntPtr where = Marshal.StringToHGlobalAnsi(null);
                                    IntPtr LogPath = Marshal.StringToHGlobalAnsi(System.AppDomain.CurrentDomain.BaseDirectory + "Log\\");
                                    result = PrintReport(ConnectionString, modelNameDeliph, modelTypeDeliph, PrintID, saveType, SavePath, Path, LogPath, where);
                                    string tmphtml = "";
                                    if (result)
                                    {
                                        string savePath = path + FromNo.Replace(" 00:00:00", "000000");
                                        string[] files = Directory.GetFiles(savePath);
                                        List<string> filelist = new List<string>();
                                        for (int i = 0; i < files.Length; i++)
                                        {
                                            FileInfo fi = new FileInfo(files[i]);
                                            filelist.Add(files[i]); ;
                                        }
                                        foreach (string a in filelist)
                                        {
                                          //string aa = new  System.Web.UI.Page().Request.ApplicationPath;
                                            tmphtml += "<img src=\"" + "../" + a.Substring(a.LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")), a.Length - a.LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir"))) + "\"  /><br>";
                                        }
                                        return tmphtml;
                                    }
                                }
                                else
                                {
                                    DataTable Idt = arfb_w.GetFromItemList(FromNo);
                                    string tmphtml = TransXMLToHtml.TransformXMLIntoHtml(ZhiFang.Common.Public.TransDataToXML.MergeXML(ZhiFang.Common.Public.TransDataToXML.TransformDTIntoXML(Fdt, "WebReportFile", "ReportForm"), ZhiFang.Common.Public.TransDataToXML.TransformDTIntoXML(Idt, "WebReportFile", "ReportItem"), "WebReportFile"), ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelShowURL") + "\\" + showmodel));
                                    return tmphtml;
                                }
                            }
                            break;
                            #endregion
                        }
                    case ZhiFang.Common.Dictionary.SectionType.Micro:
                        {
                            #region Micro
                            rmf_m.ReportFormID = FromNo;
                            string whereStr = "";
                            dsri = rmfb.GetList(rmf_m);
                            if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                            {
                                dtri = dsri.Tables[0];
                            }
                            //读取Config 来抉择使用默认模版是打印模版
                            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("Template") == "1")
                            {
                                modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, this.titelflag);
                            }
                            else
                            {
                                modelname = ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelShowURL") + "\\" + showmodel);
                            }
                            string model = modelname.Replace("//", "");

                            if (modelname.Trim().Length <= 0)
                            {
                                return null;
                                //return "暂无匹配模板！";
                            }

                            modelType = "MICROBE";
                            string imagetype = "jpg";
                            IntPtr ConnectionString = Marshal.StringToHGlobalAnsi(ConfigurationManager.ConnectionStrings["WebLisDB"].ToString());
                            IntPtr modelNameDeliph = Marshal.StringToHGlobalAnsi(model);
                            IntPtr modelTypeDeliph = Marshal.StringToHGlobalAnsi(modelType);
                            IntPtr PrintID = Marshal.StringToHGlobalAnsi(FromNo);
                            IntPtr saveType = Marshal.StringToHGlobalAnsi(imagetype);
                            IntPtr SavePath = Marshal.StringToHGlobalAnsi(path);
                            IntPtr Path = Marshal.StringToHGlobalAnsi(GetImagePath(dsrf.Tables[0].Rows[0]));
                            IntPtr where = Marshal.StringToHGlobalAnsi(whereStr);
                            IntPtr LogPath = Marshal.StringToHGlobalAnsi(System.AppDomain.CurrentDomain.BaseDirectory + "Log\\");
                            result = PrintReport(ConnectionString, modelNameDeliph, modelTypeDeliph, PrintID, saveType, SavePath, Path, LogPath, where);
                            string tmphtml = "";
                            if (result)
                            {
                                string savePath = path + FromNo.Replace(" 00:00:00", "000000");
                                string[] files = Directory.GetFiles(savePath);
                                List<string> filelist = new List<string>();
                                for (int i = 0; i < files.Length; i++)
                                {
                                    FileInfo fi = new FileInfo(files[i]);
                                    filelist.Add(files[i]); ;
                                }
                                foreach (string a in filelist)
                                {
                                    tmphtml += "<img src=\"" + "../" + a.Substring(a.LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")), a.Length - a.LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir"))) + "\"  /><br>";
                                }
                                return tmphtml;
                            }
                            break;
                            #endregion
                        }
                    case ZhiFang.Common.Dictionary.SectionType.NormalIncImage:
                        {
                            #region NormalIncImage
                            try
                            {

                                DataTable Fdt = dtrf;
                                if (Fdt.Rows.Count > 0)
                                {
                                    //读取Config 来抉择使用默认模版是打印模版
                                    if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("Template") == "1")
                                    {
                                        rif_m.ReportFormID = rff_m.ReportFormID;
                                        dsri = rifb.GetList(rif_m);
                                        if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                                        {
                                            dtri = dsri.Tables[0];
                                        }
                                        modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, this.titelflag);
                                        string model = modelname.Replace("//", "");
                                        if (modelname.Trim().Length <= 0)
                                        {
                                            return null;
                                            //return "暂无匹配模板！";
                                        }
                                        modelType = "CHAM";
                                        string imagetype = "jpg";
                                        IntPtr ConnectionString = Marshal.StringToHGlobalAnsi(ConfigurationManager.ConnectionStrings["WebLisDB"].ToString());
                                        IntPtr modelNameDeliph = Marshal.StringToHGlobalAnsi(model);
                                        IntPtr modelTypeDeliph = Marshal.StringToHGlobalAnsi(modelType);
                                        IntPtr PrintID = Marshal.StringToHGlobalAnsi(FromNo);
                                        IntPtr saveType = Marshal.StringToHGlobalAnsi(imagetype);
                                        IntPtr SavePath = Marshal.StringToHGlobalAnsi(path);
                                        IntPtr Path = Marshal.StringToHGlobalAnsi(GetImagePath(dsrf.Tables[0].Rows[0]));
                                        IntPtr where = Marshal.StringToHGlobalAnsi(null);
                                        IntPtr LogPath = Marshal.StringToHGlobalAnsi(System.AppDomain.CurrentDomain.BaseDirectory + "Log\\");
                                        result = PrintReport(ConnectionString, modelNameDeliph, modelTypeDeliph, PrintID, saveType, SavePath, Path, LogPath, where);
                                        string tmphtml = "";
                                        if (result)
                                        {
                                            string savePath = path + FromNo.Replace(" 00:00:00", "000000");
                                            string[] files = Directory.GetFiles(savePath);
                                            List<string> filelist = new List<string>();
                                            for (int i = 0; i < files.Length; i++)
                                            {
                                                FileInfo fi = new FileInfo(files[i]);
                                                filelist.Add(files[i]); ;
                                            }
                                            foreach (string a in filelist)
                                            {
                                                tmphtml += "<img src=\"" + "../" + a.Substring(a.LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")), a.Length - a.LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir"))) + "\"  /><br>";
                                            }
                                            return tmphtml;
                                        }
                                    }
                                    else
                                    {
                                        //Fdt = this.SetUserImage(Fdt);
                                        ZhiFang.Common.Log.Log.Info("显示模板路径：" + ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelShowURL") + "\\" + showmodel));
                                        DataTable Idt = arfb_w.GetFromItemList(FromNo);
                                        string tmphtml = TransXMLToHtml.TransformXMLIntoHtml(ZhiFang.Common.Public.TransDataToXML.MergeXML(ZhiFang.Common.Public.TransDataToXML.TransformDTIntoXML(Fdt, "WebReportFile", "ReportForm"), ZhiFang.Common.Public.TransDataToXML.TransformDTIntoXML(Idt, "WebReportFile", "ReportItem"), "WebReportFile"), ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelShowURL") + "\\" + showmodel));
                                        return tmphtml;
                                    }

                                }
                            }
                            catch (Exception ex)
                            {
                                ZhiFang.Common.Log.Log.Debug("NormalIncImage异常信息:" + ex.ToString());
                                return "";
                            }
                            break;
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
                            string whereStr = "";
                            //读取Config 来抉择使用默认模版是打印模版
                            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("Template") == "1")
                            {
                                modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, this.titelflag);

                            }
                            else
                            {
                                modelname = ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelShowURL") + "\\" + showmodel);
                            }
                            string model = modelname.Replace("//", "");
                            if (modelname.Trim().Length <= 0)
                            {
                                return null;
                                //return "暂无匹配模板！";
                            }
                            modelType = "MICROBE";
                            IntPtr ConnectionString = Marshal.StringToHGlobalAnsi(ConfigurationManager.ConnectionStrings["WebLisDB"].ToString());
                            IntPtr modelNameDeliph = Marshal.StringToHGlobalAnsi(model);
                            IntPtr modelTypeDeliph = Marshal.StringToHGlobalAnsi(modelType);
                            IntPtr PrintID = Marshal.StringToHGlobalAnsi(FromNo);
                            IntPtr SavePath = Marshal.StringToHGlobalAnsi(path);
                            IntPtr Path = Marshal.StringToHGlobalAnsi(GetImagePath(dsrf.Tables[0].Rows[0]));
                            IntPtr LogPath = Marshal.StringToHGlobalAnsi(System.AppDomain.CurrentDomain.BaseDirectory + "\\Log\\");
                            string imagetype = "jpg";
                            IntPtr where = Marshal.StringToHGlobalAnsi(whereStr);
                            IntPtr saveType = Marshal.StringToHGlobalAnsi(imagetype);
                            result = PrintReport(ConnectionString, modelNameDeliph, modelTypeDeliph, PrintID, saveType, SavePath, Path, LogPath, where);
                            string tmphtml = "";
                            if (result)
                            {
                                string savePath = path + FromNo.Replace(" 00:00:00", "000000");
                                string[] files = Directory.GetFiles(savePath);
                                List<string> filelist = new List<string>();
                                for (int i = 0; i < files.Length; i++)
                                {
                                    FileInfo fi = new FileInfo(files[i]);
                                    filelist.Add(files[i]); ;
                                }
                                foreach (string a in filelist)
                                {
                                    tmphtml += "<img src=\"" + "../" + a.Substring(a.LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")), a.Length - a.LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir"))) + "\"  /><br>";
                                }
                                return tmphtml;
                            }
                            break;
                            #endregion
                        }
                    case ZhiFang.Common.Dictionary.SectionType.CellMorphology:
                        {
                            #region CellMorphology
                            rmarrowf_m.ReportFormID = FromNo;
                            string whereStr = "";
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
                            //读取Config 来抉择使用默认模版是打印模版
                            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("Template") == "1")
                            {
                                modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, this.titelflag);
                            }
                            else
                            {
                                modelname = ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelShowURL") + "\\" + showmodel);
                            }
                            string model = modelname.Replace("//", "");
                            modelType = "MARROW";
                            IntPtr where = Marshal.StringToHGlobalAnsi(whereStr);
                            IntPtr ConnectionString = Marshal.StringToHGlobalAnsi(ConfigurationManager.ConnectionStrings["WebLisDB"].ToString());
                            IntPtr modelNameDeliph = Marshal.StringToHGlobalAnsi(model);
                            IntPtr modelTypeDeliph = Marshal.StringToHGlobalAnsi(modelType);
                            IntPtr PrintID = Marshal.StringToHGlobalAnsi(FromNo);
                            IntPtr SavePath = Marshal.StringToHGlobalAnsi(path);
                            IntPtr Path = Marshal.StringToHGlobalAnsi(GetImagePath(dsrf.Tables[0].Rows[0]));
                            IntPtr LogPath = Marshal.StringToHGlobalAnsi(System.AppDomain.CurrentDomain.BaseDirectory + "\\Log\\");
                            string imagetype = "jpg";
                            IntPtr saveType = Marshal.StringToHGlobalAnsi(imagetype);
                            result = PrintReport(ConnectionString, modelNameDeliph, modelTypeDeliph, PrintID, saveType, SavePath, Path, LogPath, where);
                            string tmphtml = "";
                            if (result)
                            {
                                string savePath = path + FromNo.Replace(" 00:00:00", "000000");
                                string[] files = Directory.GetFiles(savePath);
                                List<string> filelist = new List<string>();
                                for (int i = 0; i < files.Length; i++)
                                {
                                    FileInfo fi = new FileInfo(files[i]);
                                    filelist.Add(files[i]); ;
                                }
                                foreach (string a in filelist)
                                {
                                    tmphtml += "<img src=\"" + "../" + a.Substring(a.LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")), a.Length - a.LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir"))) + "\"  /><br>";
                                }
                                return tmphtml;
                            }
                            break;
                            #endregion
                        }
                    case ZhiFang.Common.Dictionary.SectionType.FishCheck:
                        {
                            #region FishCheck
                            rmarrowf_m.ReportFormID = FromNo;
                            string whereStr = "";
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
                            //读取Config 来抉择使用默认模版是打印模版
                            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("Template") == "1")
                            {
                                modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, this.titelflag);
                            }
                            else
                            {
                                modelname = ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelShowURL") + "\\" + showmodel);
                            }
                            string model = modelname.Replace("//", "");
                            modelType = "MARROW";
                            IntPtr where = Marshal.StringToHGlobalAnsi(whereStr);
                            IntPtr ConnectionString = Marshal.StringToHGlobalAnsi(ConfigurationManager.ConnectionStrings["WebLisDB"].ToString());
                            IntPtr modelNameDeliph = Marshal.StringToHGlobalAnsi(model);
                            IntPtr modelTypeDeliph = Marshal.StringToHGlobalAnsi(modelType);
                            IntPtr PrintID = Marshal.StringToHGlobalAnsi(FromNo);
                            IntPtr SavePath = Marshal.StringToHGlobalAnsi(path);
                            IntPtr Path = Marshal.StringToHGlobalAnsi(GetImagePath(dsrf.Tables[0].Rows[0]));
                            IntPtr LogPath = Marshal.StringToHGlobalAnsi(System.AppDomain.CurrentDomain.BaseDirectory + "\\Log\\");
                            string imagetype = "jpg";
                            IntPtr saveType = Marshal.StringToHGlobalAnsi(imagetype);
                            result = PrintReport(ConnectionString, modelNameDeliph, modelTypeDeliph, PrintID, saveType, SavePath, Path, LogPath, where);
                            string tmphtml = "";
                            if (result)
                            {
                                string savePath = path + FromNo.Replace(" 00:00:00", "000000");
                                string[] files = Directory.GetFiles(savePath);
                                List<string> filelist = new List<string>();
                                for (int i = 0; i < files.Length; i++)
                                {
                                    FileInfo fi = new FileInfo(files[i]);
                                    filelist.Add(files[i]); ;
                                }
                                foreach (string a in filelist)
                                {
                                    tmphtml += "<img src=\"" + "../" + a.Substring(a.LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")), a.Length - a.LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir"))) + "\"  /><br>";
                                }
                                return tmphtml;
                            }
                            break;
                            #endregion
                        }
                    case ZhiFang.Common.Dictionary.SectionType.SensorCheck:
                        {
                            #region SensorCheck
                            rif_m.ReportFormID = FromNo;
                            dsri = rifb.GetList(rif_m);
                            if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                            {
                                dtri = dsri.Tables[0];
                            }
                            string whereStr = "";
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
                            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("Template") == "1")
                            {
                                modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, this.titelflag);
                            }
                            else
                            {
                                modelname = ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelShowURL") + "\\" + showmodel);
                            }
                            string model = modelname.Replace("//", "");
                            modelType = "CHAM";
                            IntPtr where = Marshal.StringToHGlobalAnsi(whereStr);
                            IntPtr ConnectionString = Marshal.StringToHGlobalAnsi(ConfigurationManager.ConnectionStrings["WebLisDB"].ToString());
                            IntPtr modelNameDeliph = Marshal.StringToHGlobalAnsi(model);
                            IntPtr modelTypeDeliph = Marshal.StringToHGlobalAnsi(modelType);
                            IntPtr PrintID = Marshal.StringToHGlobalAnsi(FromNo);
                            IntPtr SavePath = Marshal.StringToHGlobalAnsi(path);
                            IntPtr Path = Marshal.StringToHGlobalAnsi(GetImagePath(dsrf.Tables[0].Rows[0]));
                            IntPtr LogPath = Marshal.StringToHGlobalAnsi(System.AppDomain.CurrentDomain.BaseDirectory + "\\Log\\");
                            string imagetype = "jpg";
                            IntPtr saveType = Marshal.StringToHGlobalAnsi(imagetype);
                            result = PrintReport(ConnectionString, modelNameDeliph, modelTypeDeliph, PrintID, saveType, SavePath, Path, LogPath, where);
                            string tmphtml = "";
                            if (result)
                            {
                                string savePath = path + FromNo.Replace(" 00:00:00", "000000");
                                string[] files = Directory.GetFiles(savePath);
                                List<string> filelist = new List<string>();
                                for (int i = 0; i < files.Length; i++)
                                {
                                    FileInfo fi = new FileInfo(files[i]);
                                    filelist.Add(files[i]); ;
                                }
                                foreach (string a in filelist)
                                {
                                    tmphtml += "<img src=\"" + "../" + a.Substring(a.LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")), a.Length - a.LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir"))) + "\"  /><br>";
                                }
                                return tmphtml;
                            }
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
                            string whereStr = "";
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
                            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("Template") == "1")
                            {
                                modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, this.titelflag);
                            }
                            else
                            {
                                modelname = ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelShowURL") + "\\" + showmodel);
                            }
                            string model = modelname.Replace("//", "");
                            modelType = "MARROW";
                            IntPtr where = Marshal.StringToHGlobalAnsi(whereStr);
                            IntPtr ConnectionString = Marshal.StringToHGlobalAnsi(ConfigurationManager.ConnectionStrings["WebLisDB"].ToString());
                            IntPtr modelNameDeliph = Marshal.StringToHGlobalAnsi(model);
                            IntPtr modelTypeDeliph = Marshal.StringToHGlobalAnsi(modelType);
                            IntPtr PrintID = Marshal.StringToHGlobalAnsi(FromNo);
                            IntPtr SavePath = Marshal.StringToHGlobalAnsi(path);
                            IntPtr Path = Marshal.StringToHGlobalAnsi(GetImagePath(dsrf.Tables[0].Rows[0]));
                            IntPtr LogPath = Marshal.StringToHGlobalAnsi(System.AppDomain.CurrentDomain.BaseDirectory + "\\Log\\");
                            string imagetype = "jpg";
                            IntPtr saveType = Marshal.StringToHGlobalAnsi(imagetype);
                            result = PrintReport(ConnectionString, modelNameDeliph, modelTypeDeliph, PrintID, saveType, SavePath, Path, LogPath, where);
                            string tmphtml = "";
                            if (result)
                            {
                                string savePath = path + FromNo.Replace(" 00:00:00", "000000");
                                string[] files = Directory.GetFiles(savePath);
                                List<string> filelist = new List<string>();
                                for (int i = 0; i < files.Length; i++)
                                {
                                    FileInfo fi = new FileInfo(files[i]);
                                    filelist.Add(files[i]); ;
                                }
                                foreach (string a in filelist)
                                {
                                    tmphtml += "<img src=\"" + "../" + a.Substring(a.LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")), a.Length - a.LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir"))) + "\"  /><br>";
                                }
                                return tmphtml;
                            }
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
                            string whereStr = "";
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
                            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("Template") == "1")
                            {
                                modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, this.titelflag);
                            }
                            else
                            {
                                modelname = ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelShowURL") + "\\" + showmodel);
                            }
                            string model = modelname.Replace("//", "");
                            modelType = "MARROW";
                            IntPtr where = Marshal.StringToHGlobalAnsi(whereStr);
                            IntPtr ConnectionString = Marshal.StringToHGlobalAnsi(ConfigurationManager.ConnectionStrings["WebLisDB"].ToString());
                            IntPtr modelNameDeliph = Marshal.StringToHGlobalAnsi(model);
                            IntPtr modelTypeDeliph = Marshal.StringToHGlobalAnsi(modelType);
                            IntPtr PrintID = Marshal.StringToHGlobalAnsi(FromNo);
                            IntPtr SavePath = Marshal.StringToHGlobalAnsi(path);
                            IntPtr Path = Marshal.StringToHGlobalAnsi(GetImagePath(dsrf.Tables[0].Rows[0]));
                            IntPtr LogPath = Marshal.StringToHGlobalAnsi(System.AppDomain.CurrentDomain.BaseDirectory + "\\Log\\");
                            string imagetype = "jpg";
                            IntPtr saveType = Marshal.StringToHGlobalAnsi(imagetype);
                            result = PrintReport(ConnectionString, modelNameDeliph, modelTypeDeliph, PrintID, saveType, SavePath, Path, LogPath, where);
                            string tmphtml = "";
                            if (result)
                            {
                                string savePath = path + FromNo.Replace(" 00:00:00", "000000");
                                string[] files = Directory.GetFiles(savePath);
                                List<string> filelist = new List<string>();
                                for (int i = 0; i < files.Length; i++)
                                {
                                    FileInfo fi = new FileInfo(files[i]);
                                    filelist.Add(files[i]); ;
                                }
                                foreach (string a in filelist)
                                {
                                    tmphtml += "<img src=\"" + "../" + a.Substring(a.LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")), a.Length - a.LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir"))) + "\"  /><br>";
                                }
                                return tmphtml;
                            }
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
            Log.Info("这是ShowFormTypeList()方法:ShowFormUseDeliph.cs");
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

        /// <summary>
        /// deliph   Micro(pgm.SectionType)没写
        /// </summary>
        /// <param name="FromNo"></param>
        /// <param name="SectionNo"></param>
        /// <param name="PageName"></param>
        /// <param name="ShowType"></param>
        /// <returns></returns>
        public string ShowReportFormFrx(string FromNo, int SectionNo, string PageName, int ShowType)
        {
            Log.Info("这是ShowReportFormFrx()方法:ShowFormUseDeliph.cs");
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
                    #region
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
                    #endregion
                case "Micro":
                    #region
                    {
                        arfb.GetFromMicroInfo(FromNo); break;
                    }
                    #endregion
                case "NormalIncImage":
                    #region
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
                    #endregion
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
        #endregion


        public string ShowReportXML(string FromNo, string PageName, string ShowType, string SectionType, out int ShowFunction)
        {
            throw new NotImplementedException();
        }

        public string ShowReportSqlDB(string FromNo, DataTable dtrf, DataTable dtTable, string SectionType, out int TemplateType)
        {
            throw new NotImplementedException();
        }

        public string CheckSupergroup(string FromNo, int SectionType, out DataTable dtrf, out DataTable dtTable, out DataTable dtGraph)
        {
            throw new NotImplementedException();
        }

        public string ShowReportXSLT(DataTable dtrf, DataTable dtTable, string ShowName, string ShowModel)
        {
            throw new NotImplementedException();
        }

        public string ShowReportFRX(string FromNo, DataTable dtrf, DataTable dtTable, string ShowName, string ShowModel)
        {
            throw new NotImplementedException();
        }

        public string ShowReportFR3(string FromNo, DataTable dtrf, DataTable dtTable, string ShowName, string ShowModel)
        {
            throw new NotImplementedException();
        }
    }
}
