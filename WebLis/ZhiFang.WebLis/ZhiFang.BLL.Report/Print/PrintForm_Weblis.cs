using System;
using System.Collections.Generic;
using System.Linq;
using ZhiFang.IDAL;
using ZhiFang.DALFactory;
using ZhiFang.Common.Public;
using System.Data;
using System.IO;

using FastReport;
using FastReport.Export.Html;
using ZhiFang.Tools;
using ZhiFang.Common.Dictionary;
using ZhiFang.IBLL.Common;

namespace ZhiFang.BLL.Report
{
    public class PrintFrom_Weblis : ShowFrom, ZhiFang.IBLL.Report.IBPrintFrom_Weblis
    {

        ZhiFang.BLL.Report.ReportFormFull rffb = new ReportFormFull();
        ZhiFang.BLL.Report.ReportItemFull rifb = new ReportItemFull();
        ZhiFang.BLL.Report.ReportMicroFull rmfb = new ReportMicroFull();
        ZhiFang.BLL.Report.ReportMarrowFull rmarrowfb = new ReportMarrowFull();
        private readonly IDReportFormMerge dalmerge = DalFactory<IDReportFormMerge>.GetDalByClassName("ReportFormMerge");
        private readonly IDataCache dataCache = DalFactory<IDataCache>.GetDalByClassName("DataCache");
        ZhiFang.IBLL.Common.BaseDictionary.IBPGroupPrint PGroupPrint = ZhiFang.BLLFactory.BLLFactory<ZhiFang.IBLL.Common.BaseDictionary.IBPGroupPrint>.GetBLL();
        ZhiFang.IBLL.Common.BaseDictionary.IBPrintFormat pfb = ZhiFang.BLLFactory.BLLFactory<ZhiFang.IBLL.Common.BaseDictionary.IBPrintFormat>.GetBLL();
        private ZhiFang.Common.Dictionary.ReportFormTitle titelflag = ZhiFang.Common.Dictionary.ReportFormTitle.center;
        public List<string> PrintHtml(string FormNo, ReportFormTitle Flag, string ResultFlag)
        {
            try
            {
                Model.ReportFormFull rff_m = new Model.ReportFormFull();
                Model.ReportItemFull rif_m = new Model.ReportItemFull();
                Model.ReportMicroFull rmf_m = new Model.ReportMicroFull();
                Model.ReportMarrowFull rmarrowf_m = new Model.ReportMarrowFull();
                DataSet dsri;
                rff_m.ReportFormID = FormNo;
                DataSet dsrf = rffb.GetList(rff_m);
                DataTable dtrf = new DataTable("frform");
                DataTable dtri = new DataTable();
                FastReport.Report report = new FastReport.Report();
                //HTMLExport htmlexport = new HTMLExport();
                //htmlexport.SubFolder = false;
                //htmlexport.SinglePage = true;
                //htmlexport.Navigator = false;
                string modelname = null;
                List<string> l = new List<string>();
                if (dsrf.Tables.Count > 0 && dsrf.Tables[0].Rows.Count > 0)
                {
                    dtrf = dsrf.Tables[0];
                    dtrf.TableName = "frform";
                    switch ((SectionType)Convert.ToInt32(dsrf.Tables[0].Rows[0]["SECTIONTYPE"].ToString()))
                    {
                        case SectionType.all:
                            #region Normal
                            rif_m.ReportFormID = FormNo;
                            dsri = rifb.GetList(rif_m);
                            if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                            {
                                dtri = dsri.Tables[0];
                            }
                            modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, Flag);
                            if (modelname.Trim().Length < 0)
                            {
                                return null;
                                //return "暂无匹配模板！";
                            }
                            dtri.TableName = "fritem";
                            report.Load(modelname);
                            RegeditData(dtri, ref report, modelname);
                            RegeditImage(dsrf.Tables[0].Rows[0], ref report);
                            break;
                            #endregion
                        case SectionType.Normal:
                            #region Normal
                            rif_m.ReportFormID = FormNo;
                            dsri = rifb.GetList(rif_m);
                            if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                            {
                                dtri = dsri.Tables[0];
                            }
                            modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, Flag);
                            if (modelname == null || modelname.Trim().Length < 0)
                            {
                                return null;
                                //return "暂无匹配模板！";
                            }
                            dtri.TableName = "fritem";
                            report.Load(modelname);
                            RegeditData(dtri, ref report, modelname);
                            RegeditImage(dsrf.Tables[0].Rows[0], ref report);
                            break;
                            #endregion
                        case SectionType.Micro:
                            #region Micro
                            rmf_m.ReportFormID = FormNo;
                            dsri = rmfb.GetList(rmf_m);
                            if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                            {
                                dtri = dsri.Tables[0];
                            }
                            modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, Flag);
                            if (modelname.Trim().Length < 0)
                            {
                                return null;
                                //return "暂无匹配模板！";
                            }
                            dtri.TableName = "frmicro";
                            report.Load(modelname);
                            RegeditData(dtri, ref report, modelname);
                            RegeditImage(dsrf.Tables[0].Rows[0], ref report);
                            break;
                            #endregion
                        case SectionType.NormalIncImage:
                            #region NormalIncImage
                            rif_m.ReportFormID = FormNo;
                            dsri = rifb.GetList(rif_m);
                            if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                            {
                                dtri = dsri.Tables[0];
                            }
                            modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, Flag);
                            if (modelname == null || modelname.Trim().Length < 0)
                            {
                                return null;
                                //return "暂无匹配模板！";
                            }
                            dtri.TableName = "fritem";
                            report.Load(modelname);
                            RegeditData(dtri, ref report, modelname);
                            RegeditImage(dsrf.Tables[0].Rows[0], ref report);
                            break;
                            #endregion
                        case SectionType.MicroIncImage:
                            #region MicroIncImage
                            rmf_m.ReportFormID = FormNo;
                            dsri = rmfb.GetList(rmf_m);
                            if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                            {
                                dtri = dsri.Tables[0];
                            }
                            modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, Flag);
                            if (modelname.Trim().Length < 0)
                            {
                                return null;
                                //return "暂无匹配模板！";
                            }
                            dtri.TableName = "frmicro";
                            report.Load(modelname);
                            RegeditData(dtri, ref report, modelname);
                            RegeditImage(dsrf.Tables[0].Rows[0], ref report);
                            break;
                            #endregion
                        case SectionType.CellMorphology:
                            #region CellMorphology:
                            rmarrowf_m.ReportFormID = FormNo;
                            dsri = rmarrowfb.GetList(rmarrowf_m);
                            if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                            {
                                dtri = dsri.Tables[0];
                            }
                            modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, Flag);
                            report.Load(modelname);
                            for (int i = 0; i < dtri.Rows.Count; i++)
                            {
                                TextObject textM = (TextObject)report.FindObject("TextM" + dtri.Rows[i]["ItemNo"].ToString());
                                if (textM != null)
                                {
                                    if (dtri.Columns.Contains("ReportValue"))
                                    {
                                        textM.Text += dtri.Rows[i]["ReportValue"].ToString();
                                    }
                                    if (dtri.Columns.Contains("ReportDesc"))
                                    {
                                        textM.Text += dtri.Rows[i]["ReportDesc"].ToString();
                                    }
                                    if (dtri.Columns.Contains("ReportText"))
                                    {
                                        textM.Text += dtri.Rows[i]["ReportText"].ToString();
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
                            RegeditImage(dsrf.Tables[0].Rows[0], ref report);
                            break;
                            #endregion
                        case SectionType.FishCheck:
                            #region FishCheck
                            rmarrowf_m.ReportFormID = FormNo;
                            dsri = rmarrowfb.GetList(rmarrowf_m);
                            if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                            {
                                dtri = dsri.Tables[0];
                            }
                            modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, Flag);
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
                            RegeditImage(dsrf.Tables[0].Rows[0], ref report);
                            #endregion
                            break;
                        case SectionType.SensorCheck:
                            rif_m.ReportFormID = FormNo;
                            dsri = rifb.GetList(rif_m);
                            if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                            {
                                dtri = dsri.Tables[0];
                            }
                            break;
                        case SectionType.ChromosomeCheck:
                            rif_m.ReportFormID = FormNo;
                            dsri = rifb.GetList(rif_m);
                            if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                            {
                                dtri = dsri.Tables[0];
                            }
                            break;
                        case SectionType.PathologyCheck:
                            rif_m.ReportFormID = FormNo;
                            dsri = rifb.GetList(rif_m);
                            if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                            {
                                dtri = dsri.Tables[0];
                            }
                            break;
                        default:
                            rif_m.ReportFormID = FormNo;
                            dsri = rifb.GetList(rif_m);
                            if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                            {
                                dtri = dsri.Tables[0];
                            }
                            break;
                    }
                    FastReport.TextObject txtAdd = (FastReport.TextObject)report.FindObject("txtAdd");
                    string txtAddTitle = "";
                    if (txtAdd != null)
                    {
                        txtAddTitle = txtAdd.Text;
                    }
                    if (Flag == ReportFormTitle.center)
                    {
                        FastReport.TextObject texttitle = (FastReport.TextObject)report.FindObject("TextTitle");
                        if (texttitle != null)
                        {
                            texttitle.Text = ConfigHelper.GetConfigString("CenterName") + txtAddTitle + texttitle.Text;
                        }
                        FastReport.PictureObject p = (FastReport.PictureObject)report.FindObject("Picture1");
                        if (p != null)
                        {
                            //byte[] poperatorimage;
                            //System.IO.MemoryStream aaa = new System.IO.MemoryStream(pcheckerimage);
                            //System.Drawing.Image a ;
                            //a = p.Image;
                            //a.Save(System.AppDomain.CurrentDomain.BaseDirectory + @"ReportPrint\PrintImage\logo.jpg");
                            //p.ImageLocation = @"D:\project\ljjPJ\vs2008\Report\Report\TmpHtmlPath\aaa.files\aaa.png";
                            p.Visible = true;
                        }
                        FastReport.PictureObject p1 = (FastReport.PictureObject)report.FindObject("logo");
                        if (p1 != null)
                        {
                            //byte[] poperatorimage;
                            //System.IO.MemoryStream aaa = new System.IO.MemoryStream(pcheckerimage);
                            //System.Drawing.Image a ;
                            //a = p.Image;
                            //a.Save(System.AppDomain.CurrentDomain.BaseDirectory + @"ReportPrint\PrintImage\logo.jpg");
                            //p.ImageLocation = @"D:\project\ljjPJ\vs2008\Report\Report\TmpHtmlPath\aaa.files\aaa.png";
                            p1.Visible = true;
                        }
                    }
                    if (Flag == ReportFormTitle.client)
                    {
                        FastReport.TextObject texttitle = (FastReport.TextObject)report.FindObject("TextTitle");
                        if (texttitle != null)
                        {
                            texttitle.Text = dsrf.Tables[0].Rows[0]["CLIENTNAME"].ToString() + txtAddTitle + texttitle.Text;
                        }
                        FastReport.PictureObject p = (FastReport.PictureObject)report.FindObject("Picture1");
                        if (p != null)
                        {
                            //System.Drawing.Image a;
                            //a = p.Image;
                            //a.Save(System.AppDomain.CurrentDomain.BaseDirectory + @"ReportPrint\PrintImage\logo.jpg");
                            p.Visible = false;
                        }
                        FastReport.PictureObject p1 = (FastReport.PictureObject)report.FindObject("logo");
                        if (p1 != null)
                        {
                            p1.Visible = false;
                        }
                    }
                    report.RegisterData(dtrf.DataSet);
                    report.Prepare();



                    switch (ResultFlag)
                    {
                        case "URL":
                            #region Mht
                            FastReport.Export.Mht.MHTExport mhte = new FastReport.Export.Mht.MHTExport();
                            mhte.ImageFormat = FastReport.Export.Html.ImageFormat.Jpeg;
                            mhte.CurPage = 10;
                            //report.c
                            report.Export(mhte, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormImageDir") + "\\" + FormNo.Replace(" 00:00:00", "").Replace(",", "&") + "@" + Flag.ToString() + ".mht");
                            //mhte..GeneratedFiles
                            #endregion
                            string Mhtinnerhtml = ReadContext(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormImageDir") + "\\" + FormNo.Replace(" 00:00:00", "").Replace(",", "&") + "@" + Flag.ToString() + ".mht");
                            for (int i = 2; i < 100; i++)
                            {
                                if (Mhtinnerhtml.IndexOf("<a name=3D\"PageN" + i + "\"></a>") > 0)
                                {
                                    Mhtinnerhtml = Mhtinnerhtml.Replace("<a name=3D\"PageN" + i + "\"></a>", "<p style=\"page-break-after:always\">&nbsp;</p>");
                                }
                                else
                                {
                                    break;
                                }
                                if (Mhtinnerhtml.IndexOf("<a name=\"PageN" + i + "\"></a>") > 0)
                                {
                                    Mhtinnerhtml = Mhtinnerhtml.Replace("<a name=\"PageN" + i + "\"></a>", "<p style=\"page-break-after:always\">&nbsp;</p>");
                                }
                                else
                                {
                                    break;
                                }
                            }
                            WriteContext(Mhtinnerhtml, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormImageDir") + "\\" + FormNo.Replace(" 00:00:00", "").Replace(",", "&") + "@" + Flag.ToString() + ".mht");

                            l.Add(ConfigHelper.GetConfigString("LocalhostURL") + "\\" + ConfigHelper.GetConfigString("ReportFormImageDir") + "\\" + FormNo.Replace(" 00:00:00", "").Replace(",", "&") + "@" + Flag.ToString() + ".mht"); break;
                        case "CONTEXT":
                            #region Html
                            FastReport.Export.Html.HTMLExport htmle = new HTMLExport();
                            htmle.SinglePage = true;
                            htmle.Navigator = false;
                            htmle.SubFolder = false;
                            report.Export(htmle, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormImageDir") + "\\" + FormNo.Replace(" 00:00:00", "").Replace(",", "&") + "@" + Flag.ToString() + ".html");
                            string htmleinnerhtml = ReadContext(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormImageDir") + "\\" + FormNo.Replace(" 00:00:00", "").Replace(",", "&") + "@" + Flag.ToString() + ".html");
                            int stylei = 0;
                            while (htmleinnerhtml.IndexOf(".s" + stylei + " {") >= 0)
                            {
                                htmleinnerhtml = htmleinnerhtml.Replace(".s" + stylei + " {", ".s" + stylei + FormNo.Replace(" 00:00:00", "") + " {");
                                htmleinnerhtml = htmleinnerhtml.Replace("class=\"s" + stylei + "\"", "class=\"s" + stylei + FormNo.Replace(" 00:00:00", "") + "\"");
                                stylei++;
                            }
                            for (int i = 2; i < 100; i++)
                            {
                                if (htmleinnerhtml.IndexOf("<a name=\"PageN" + i + "\"></a>") > 0)
                                {
                                    htmleinnerhtml = htmleinnerhtml.Replace("<a name=\"PageN" + i + "\"></a>", "<p style=\"page-break-after:always\">&nbsp;</p>").Replace("class=\"page_break\"", "");
                                }
                                else
                                {
                                    break;
                                }
                            }
                            WriteContext(htmleinnerhtml, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormImageDir") + "\\" + FormNo.Replace(" 00:00:00", "").Replace(",", "&") + "@" + Flag.ToString() + ".html");
                            #endregion
                            //Common.FilesHelper.WriteContext(aaa, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + Common.ConfigHelper.GetConfigString("ReportFormImageDir") + "\\" + FormNo.Replace(" 00:00:00", "").Replace(",", "&") + "@" + Flag.ToString() + ".mht");

                            l.Add(htmleinnerhtml);
                            break;
                        case "IMAGE":
                            #region Image
                            FastReport.Export.Image.ImageExport tmpjpg = new FastReport.Export.Image.ImageExport();
                            string imagetype = "jpeg";
                            if (ConfigHelper.GetConfigInt("JpegQuality") != 0)
                            {
                                tmpjpg.JpegQuality = Convert.ToInt32(ConfigHelper.GetConfigInt("JpegQuality"));
                            }

                            switch (ConfigHelper.GetConfigString("ReportFormImageType").ToUpper())
                            {
                                case "JPEG": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg; imagetype = "jpeg"; break;
                                case "JPG": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg; imagetype = "jpg"; break;
                                case "GIF": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Gif; imagetype = "gif"; break;
                                case "BMP": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Bmp; imagetype = "bmp"; break;
                                case "PNG": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Png; imagetype = "png"; break;
                                case "TIFF": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Tiff; imagetype = "tiff"; break;
                                default: tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg; imagetype = "jpeg"; break;
                            }
                            report.Export(tmpjpg, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormImageDir") + "\\" + FormNo.Replace(" 00:00:00", "").Replace(",", "&") + "@" + Flag.ToString() + "." + imagetype + "");
                            //l.Add(Common.ConfigHelper.GetConfigString("LocalhostURL") + "\\" + Common.ConfigHelper.GetConfigString("ReportFormImageDir") + "\\" + FormNo.Replace(" 00:00:00", "").Replace(",", "&") + "@" + Flag.ToString() + ".jpeg");
                            l.Add(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormImageDir") + "\\" + FormNo.Replace(" 00:00:00", "").Replace(",", "&") + "@" + Flag.ToString() + ".jpeg");
                            #endregion
                            break;
                        default: break;
                    }
                    report.Dispose();
                    return l;
                    //Encoding utf1 = Encoding.GetEncoding("UTF-8");
                    //string tmphtml = utf1.GetString(m.ToArray());
                    //string sub = tmphtml.Substring(tmphtml.IndexOf(FormNo.Replace(" 00:00:00", "") + ".files"), tmphtml.IndexOf(".png") + 4 - tmphtml.IndexOf(FormNo.Replace(" 00:00:00", "") + ".files"));
                    //tmphtml = tmphtml.Replace(sub, @"PrintImage\logo.jpg");
                    //System.AppDomain.CurrentDomain.BaseDirectory + "\\" + Common.ConfigHelper.GetConfigString("ReportFormImageDir") + "\\" + FormNo.Replace(" 00:00:00", "").Replace(",", "&") + "@" + Flag.ToString() + ".mht";
                    //return tmpjpg.GeneratedFiles;
                    //return ".." + "\\" + Common.ConfigHelper.GetConfigString("ReportFormImageDir") + "\\" + FormNo.Replace(" 00:00:00", "").Replace(",", "|") + "." + imagetype + "";                    
                }
                return null;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Info(e.ToString() + "---------" + e.StackTrace.ToString() + "---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
                return null;
            }
        }
        public List<string> PrintMergeHtml(string FormNo, ReportFormTitle Flag, string ResultFlag)
        {
            try
            {
                DataSet dataSet;
                FastReport.Report report = new FastReport.Report();
                //FastReport.Export.Image.ImageExport tmpjpg = new FastReport.Export.Image.ImageExport();
                //tmpjpg.PageNumbers
                report.Clear();

                dataSet = this.dalmerge.GetModelDataFrFormAll(FormNo.Split(',')).DataSet;
                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    //string imagetype = "jpeg";
                    //switch (Common.ConfigHelper.GetConfigString("ReportFormImageType").ToUpper())
                    //{
                    //    case "JPEG": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg; imagetype = "jpeg"; break;
                    //    case "JPG": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg; imagetype = "jpg"; break;
                    //    case "GIF": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Gif; imagetype = "gif"; break;
                    //    case "BMP": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Bmp; imagetype = "bmp"; break;
                    //    case "PNG": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Png; imagetype = "png"; break;
                    //    case "TIFF": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Tiff; imagetype = "tiff"; break;
                    //    default: tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg; imagetype = "jpeg"; break;
                    //}
                    if (Flag == ReportFormTitle.BatchPrint)
                    {
                        report.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("XSLModelURL") + "\\套打合并.frx");
                    }
                    else
                    {
                        report.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("XSLModelURL") + "\\合并.frx");
                    }
                    FastReport.TextObject txtAdd = (FastReport.TextObject)report.FindObject("txtAdd");
                    string txtAddTitle = "";
                    if (txtAdd != null)
                    {
                        txtAddTitle = txtAdd.Text;
                    }
                    if (Flag == ReportFormTitle.center)
                    {
                        FastReport.TextObject texttitle = (FastReport.TextObject)report.FindObject("TextTitle");
                        if (texttitle != null)
                        {
                            texttitle.Text = ConfigHelper.GetConfigString("CenterName") + txtAddTitle + texttitle.Text;

                        }
                        FastReport.PictureObject p = (FastReport.PictureObject)report.FindObject("Picture1");
                        if (p != null)
                        {
                            p.Visible = true;
                        }
                    }
                    if (Flag == ReportFormTitle.client)
                    {
                        FastReport.TextObject texttitle = (FastReport.TextObject)report.FindObject("TextTitle");
                        if (texttitle != null)
                        {
                            texttitle.Text = dataSet.Tables[0].Rows[0]["CLIENTNAME"].ToString() + txtAddTitle + texttitle.Text;
                        }
                        FastReport.PictureObject p = (FastReport.PictureObject)report.FindObject("Picture1");
                        if (p != null)
                        {
                            p.Visible = false;
                        }
                    }
                    dataSet.Tables[0].TableName = "frformall";
                    report.RegisterData(dataSet);
                    report.Prepare();

                    FastReport.Export.Mht.MHTExport mhte = new FastReport.Export.Mht.MHTExport();
                    mhte.ImageFormat = FastReport.Export.Html.ImageFormat.Jpeg;
                    report.Export(mhte, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormImageDir") + "\\" + FormNo.Replace(" 00:00:00", "").Replace(",", "&") + "@" + Flag.ToString() + ".mht");

                    report.Dispose();

                    string aaa = ReadContext(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormImageDir") + "\\" + FormNo.Replace(" 00:00:00", "").Replace(",", "&") + "@" + Flag.ToString() + ".mht");
                    for (int i = 2; i < 100; i++)
                    {
                        if (aaa.IndexOf("<a name=3D\"PageN" + i + "\"></a>") > 0)
                        {
                            aaa = aaa.Replace("<a name=3D\"PageN" + i + "\"></a>", "<p style=\"page-break-after:always\">&nbsp;</p>");
                        }
                        else
                        {
                            break;
                        }
                    }
                    WriteContext(aaa, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormImageDir") + "\\" + FormNo.Replace(" 00:00:00", "").Replace(",", "&") + "@" + Flag.ToString() + ".mht");
                    List<string> l = new List<string>();
                    l.Add(ConfigHelper.GetConfigString("LocalhostURL") + "\\" + ConfigHelper.GetConfigString("ReportFormImageDir") + "\\" + FormNo.Replace(" 00:00:00", "").Replace(",", "&") + "@" + Flag.ToString() + ".mht");
                    return l;

                    //report.Export(tmpjpg, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + Common.ConfigHelper.GetConfigString("ReportFormImageDir") + "\\" + FormNo.Replace(" 00:00:00", "").Replace(",", "&") + "." + imagetype + "");                    
                    //return tmpjpg.GeneratedFiles;
                    //return "..\\"+Common.ConfigHelper.GetConfigString("ReportFormImageDir")+"\\" + FormNo.Replace(" 00:00:00", "") + "." + imagetype + "";
                }
                return null;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Info(e.StackTrace.ToString() + "---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
                return null;
            }
        }

        public List<string> PrintMergeEnHtml(string FormNo, ReportFormTitle Flag, string ResultFlag)
        {
            try
            {
                DataSet dataSet;
                FastReport.Report report = new FastReport.Report();
                //FastReport.Export.Image.ImageExport tmpjpg = new FastReport.Export.Image.ImageExport();
                //tmpjpg.PageNumbers
                report.Clear();

                dataSet = this.dalmerge.GetModelDataFrFormAll(FormNo.Split(',')).DataSet;
                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    report.RegisterData(dataSet);
                    report.Prepare();
                    //string imagetype = "jpeg";
                    //switch (Common.ConfigHelper.GetConfigString("ReportFormImageType").ToUpper())
                    //{
                    //    case "JPEG": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg; imagetype = "jpeg"; break;
                    //    case "JPG": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg; imagetype = "jpg"; break;
                    //    case "GIF": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Gif; imagetype = "gif"; break;
                    //    case "BMP": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Bmp; imagetype = "bmp"; break;
                    //    case "PNG": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Png; imagetype = "png"; break;
                    //    case "TIFF": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Tiff; imagetype = "tiff"; break;
                    //    default: tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg; imagetype = "jpeg"; break;
                    //}
                    if (Flag == ReportFormTitle.BatchPrint)
                    {
                        report.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("XSLModelURL") + "\\套打合并英文.frx");
                    }
                    else
                    {
                        report.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("XSLModelURL") + "\\合并英文.frx");
                    }
                    FastReport.TextObject txtAdd = (FastReport.TextObject)report.FindObject("txtAdd");
                    string txtAddTitle = "";
                    if (txtAdd != null)
                    {
                        txtAddTitle = txtAdd.Text;
                    }
                    if (Flag == ReportFormTitle.center)
                    {
                        FastReport.TextObject texttitle = (FastReport.TextObject)report.FindObject("TextTitle");
                        if (texttitle != null)
                        {
                            texttitle.Text = ConfigHelper.GetConfigString("CenterName") + txtAddTitle + texttitle.Text;
                        }
                        FastReport.PictureObject p = (FastReport.PictureObject)report.FindObject("Picture1");
                        if (p != null)
                        {
                            p.Visible = true;
                        }
                    }
                    if (Flag == ReportFormTitle.client)
                    {
                        FastReport.TextObject texttitle = (FastReport.TextObject)report.FindObject("TextTitle");
                        if (texttitle != null)
                        {
                            texttitle.Text = dataSet.Tables[0].Rows[0]["CLIENTNAME"].ToString() + txtAddTitle + texttitle.Text;
                        }
                        FastReport.PictureObject p = (FastReport.PictureObject)report.FindObject("Picture1");
                        if (p != null)
                        {
                            p.Visible = false;
                        }
                    }
                    FastReport.Export.Mht.MHTExport mhte = new FastReport.Export.Mht.MHTExport();
                    mhte.ImageFormat = FastReport.Export.Html.ImageFormat.Jpeg;
                    report.Export(mhte, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormImageDir") + "\\" + FormNo.Replace(" 00:00:00", "").Replace(",", "&") + "@" + Flag.ToString() + ".mht");

                    report.Dispose();

                    string aaa = ReadContext(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormImageDir") + "\\" + FormNo.Replace(" 00:00:00", "").Replace(",", "&") + "@" + Flag.ToString() + ".mht");
                    for (int i = 2; i < 100; i++)
                    {
                        if (aaa.IndexOf("<a name=3D\"PageN" + i + "\"></a>") > 0)
                        {
                            aaa = aaa.Replace("<a name=3D\"PageN" + i + "\"></a>", "<p style=\"page-break-after:always\">&nbsp;</p>");
                        }
                        else
                        {
                            break;
                        }
                    }
                    WriteContext(aaa, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormImageDir") + "\\" + FormNo.Replace(" 00:00:00", "").Replace(",", "&") + "@" + Flag.ToString() + ".mht");
                    List<string> l = new List<string>();
                    l.Add(ConfigHelper.GetConfigString("LocalhostURL") + "\\" + ConfigHelper.GetConfigString("ReportFormImageDir") + "\\" + FormNo.Replace(" 00:00:00", "").Replace(",", "&") + "@" + Flag.ToString() + ".mht");
                    return l;

                    //report.Export(tmpjpg, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + Common.ConfigHelper.GetConfigString("ReportFormImageDir") + "\\" + FormNo.Replace(" 00:00:00", "").Replace(",", "&") + "." + imagetype + "");
                    //report.Dispose();
                    //return tmpjpg.GeneratedFiles;
                    //return "..\\" + Common.ConfigHelper.GetConfigString("ReportFormImageDir") + "\\" + FormNo.Replace(" 00:00:00", "") + "." + imagetype + "";
                }
                return null;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Info(e.StackTrace.ToString() + "---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
                return null;
            }
        }
        public List<string> PrintHtml(string FormNo, ReportFormTitle Flag)
        {
            try
            {
                Model.ReportFormFull rff_m = new Model.ReportFormFull();
                Model.ReportItemFull rif_m = new Model.ReportItemFull();
                Model.ReportMicroFull rmf_m = new Model.ReportMicroFull();
                Model.ReportMarrowFull rmarrowf_m = new Model.ReportMarrowFull();
                DataSet dsri;
                rff_m.ReportFormID = FormNo;
                DataSet dsrf = rffb.GetList(rff_m);
                DataTable dtrf = new DataTable("frform");
                DataTable dtri = new DataTable();
                FastReport.Report report = new FastReport.Report();
                //HTMLExport htmlexport = new HTMLExport();
                //htmlexport.SubFolder = false;
                //htmlexport.SinglePage = true;
                //htmlexport.Navigator = false;
                string modelname = null;
                if (dsrf.Tables.Count > 0 && dsrf.Tables[0].Rows.Count > 0)
                {
                    dtrf = dsrf.Tables[0];
                    dtrf.TableName = "frform";
                    switch ((SectionType)Convert.ToInt32(dsrf.Tables[0].Rows[0]["SECTIONTYPE"].ToString()))
                    {
                        case SectionType.all:
                            #region Normal
                            rif_m.ReportFormID = FormNo;
                            dsri = rifb.GetList(rif_m);
                            if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                            {
                                dtri = dsri.Tables[0];
                            }
                            modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, Flag);
                            if (modelname.Trim().Length < 0)
                            {
                                return null;
                                //return "暂无匹配模板！";
                            }
                            dtri.TableName = "fritem";
                            report.Load(modelname);
                            RegeditData(dtri, ref report, modelname);
                            RegeditImage(dsrf.Tables[0].Rows[0], ref report);
                            break;
                            #endregion
                        case SectionType.Normal:
                            #region Normal
                            rif_m.ReportFormID = FormNo;
                            dsri = rifb.GetList(rif_m);
                            if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                            {
                                dtri = dsri.Tables[0];
                            }
                            modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, Flag);
                            if ((modelname == null || modelname == "") || modelname.Trim().Length < 0)
                            {
                                return null;
                                //return "暂无匹配模板！";
                            }
                            dtri.TableName = "fritem";
                            report.Load(modelname);
                            RegeditData(dtri, ref report, modelname);
                            RegeditImage(dsrf.Tables[0].Rows[0], ref report);
                            break;
                            #endregion
                        case SectionType.Micro:
                            #region Micro
                            rmf_m.ReportFormID = FormNo;
                            dsri = rmfb.GetList(rmf_m);
                            if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                            {
                                dtri = dsri.Tables[0];
                            }
                            modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, Flag);
                            if (modelname.Trim().Length < 0)
                            {
                                return null;
                                //return "暂无匹配模板！";
                            }
                            dtri.TableName = "frmicro";
                            report.Load(modelname);
                            RegeditData(dtri, ref report, modelname);
                            RegeditImage(dsrf.Tables[0].Rows[0], ref report);
                            break;
                            #endregion
                        case SectionType.NormalIncImage:
                            #region NormalIncImage
                            rif_m.ReportFormID = FormNo;
                            dsri = rifb.GetList(rif_m);
                            if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                            {
                                dtri = dsri.Tables[0];
                            }
                            modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, Flag);
                            if (modelname == null || modelname.Trim().Length < 0)
                            {
                                return null;
                                //return "暂无匹配模板！";
                            }
                            dtri.TableName = "fritem";
                            report.Load(modelname);
                            RegeditData(dtri, ref report, modelname);
                            RegeditImage(dsrf.Tables[0].Rows[0], ref report);
                            break;
                            #endregion
                        case SectionType.MicroIncImage:
                            #region MicroIncImage
                            rmf_m.ReportFormID = FormNo;
                            dsri = rmfb.GetList(rmf_m);
                            if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                            {
                                dtri = dsri.Tables[0];
                            }
                            modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, Flag);
                            if (modelname.Trim().Length < 0)
                            {
                                return null;
                                //return "暂无匹配模板！";
                            }
                            dtri.TableName = "frmicro";
                            report.Load(modelname);
                            RegeditData(dtri, ref report, modelname);
                            RegeditImage(dsrf.Tables[0].Rows[0], ref report);
                            break;
                            #endregion
                        case SectionType.CellMorphology:
                            #region CellMorphology:
                            rmarrowf_m.ReportFormID = FormNo;
                            dsri = rmarrowfb.GetList(rmarrowf_m);
                            if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                            {
                                dtri = dsri.Tables[0];
                            }
                            modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, Flag);
                            report.Load(modelname);
                            for (int i = 0; i < dtri.Rows.Count; i++)
                            {
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
                            RegeditImage(dsrf.Tables[0].Rows[0], ref report);
                            break;
                            #endregion
                        case SectionType.FishCheck:
                            rmarrowf_m.ReportFormID = FormNo;
                            dsri = rmarrowfb.GetList(rmarrowf_m);
                            if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                            {
                                dtri = dsri.Tables[0];
                            }
                            modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, Flag);
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
                            RegeditImage(dsrf.Tables[0].Rows[0], ref report);
                            break;
                        case SectionType.SensorCheck:
                            rif_m.ReportFormID = FormNo;
                            dsri = rifb.GetList(rif_m);
                            if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                            {
                                dtri = dsri.Tables[0];
                            }
                            break;
                        case SectionType.ChromosomeCheck:
                            rif_m.ReportFormID = FormNo;
                            dsri = rifb.GetList(rif_m);
                            if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                            {
                                dtri = dsri.Tables[0];
                            }
                            break;
                        case SectionType.PathologyCheck:
                            //rif_m.ReportFormID = FormNo;
                            //dsri = rifb.GetList(rif_m);
                            rmarrowf_m.ReportFormID = FormNo;
                            dsri = rmarrowfb.GetList(rmarrowf_m);
                            if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                            {
                                dtri = dsri.Tables[0];
                            }
                            modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, Flag);
                            if ((modelname == null || modelname == "") || modelname.Trim().Length < 0)
                            {
                                return null;
                                //return "暂无匹配模板！";
                            }
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
                            RegeditImage(dsrf.Tables[0].Rows[0], ref report);
                            break;
                        default:
                            rif_m.ReportFormID = FormNo;
                            dsri = rifb.GetList(rif_m);
                            if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                            {
                                dtri = dsri.Tables[0];
                            }
                            break;
                    }
                    FastReport.TextObject txtAdd = (FastReport.TextObject)report.FindObject("txtAdd");
                    string txtAddTitle = "";
                    if (txtAdd != null)
                    {
                        txtAddTitle = txtAdd.Text;
                    }
                    if (Flag == ReportFormTitle.center)
                    {
                        FastReport.TextObject texttitle = (FastReport.TextObject)report.FindObject("TextTitle");
                        if (texttitle != null)
                        {
                            texttitle.Text = ZhiFang.Common.Public.ConfigHelper.GetConfigString("CenterName") + txtAddTitle + "";
                        }
                        FastReport.PictureObject p = (FastReport.PictureObject)report.FindObject("Picture1");
                        if (p != null)
                        {
                            //byte[] poperatorimage;
                            //System.IO.MemoryStream aaa = new System.IO.MemoryStream(pcheckerimage);
                            //System.Drawing.Image a ;
                            //a = p.Image;
                            //a.Save(System.AppDomain.CurrentDomain.BaseDirectory + @"ReportPrint\PrintImage\logo.jpg");
                            //p.ImageLocation = @"D:\project\ljjPJ\vs2008\Report\Report\TmpHtmlPath\aaa.files\aaa.png";
                            p.Visible = true;
                        }
                        FastReport.PictureObject p1 = (FastReport.PictureObject)report.FindObject("logo");
                        if (p1 != null)
                        {
                            //byte[] poperatorimage;
                            //System.IO.MemoryStream aaa = new System.IO.MemoryStream(pcheckerimage);
                            //System.Drawing.Image a ;
                            //a = p.Image 
                            //a.Save(System.AppDomain.CurrentDomain.BaseDirectory + @"ReportPrint\PrintImage\logo.jpg");
                            //p.ImageLocation = @"D:\project\ljjPJ\vs2008\Report\Report\TmpHtmlPath\aaa.files\aaa.png";
                            p1.Visible = true;
                        }
                    }
                    if (Flag == ReportFormTitle.client)
                    {
                        FastReport.TextObject texttitle = (FastReport.TextObject)report.FindObject("TextTitle");
                        if (texttitle != null)
                        {
                            texttitle.Text = dsrf.Tables[0].Rows[0]["CLIENTNAME"].ToString() + txtAddTitle + "";
                        }
                        FastReport.PictureObject p = (FastReport.PictureObject)report.FindObject("Picture1");
                        if (p != null)
                        {
                            //System.Drawing.Image a;
                            //a = p.Image;
                            //a.Save(System.AppDomain.CurrentDomain.BaseDirectory + @"ReportPrint\PrintImage\logo.jpg");
                            p.Visible = false;
                        }
                        FastReport.PictureObject p1 = (FastReport.PictureObject)report.FindObject("logo");
                        if (p1 != null)
                        {
                            p1.Visible = false;
                        }
                    }
                    report.RegisterData(dtrf.DataSet);
                    report.Prepare();


                    //MemoryStream m = new MemoryStream();
                    //report.Export(htmlexport, System.AppDomain.CurrentDomain.BaseDirectory + "\\"+ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormImageDir")+"\\"+FormNo.Replace(" 00:00:00","")+".html");
                    //htmlexport.Print
                    FastReport.Export.Mht.MHTExport mhte = new FastReport.Export.Mht.MHTExport();
                    //FastReport.Export.Image.ImageExport tmpjpg = new FastReport.Export.Image.ImageExport();
                    //string imagetype = "jpeg";
                    //if (Common.Public.ConfigHelper.GetConfigInt("JpegQuality") != 0)
                    //{
                    //    tmpjpg.JpegQuality = Common.Public.ConfigHelper.GetConfigInt("JpegQuality");
                    //}

                    //switch (ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormImageType").ToUpper())
                    //{
                    //    case "JPEG": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg; imagetype = "jpeg"; break;
                    //    case "JPG": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg; imagetype = "jpg"; break;
                    //    case "GIF": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Gif; imagetype = "gif"; break;
                    //    case "BMP": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Bmp; imagetype = "bmp"; break;
                    //    case "PNG": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Png; imagetype = "png"; break;
                    //    case "TIFF": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Tiff; imagetype = "tiff"; break;
                    //    default: tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg; imagetype = "jpeg"; break;
                    //}
                    //report.Export(tmpjpg, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormImageDir") + "\\" + FormNo.Replace(" 00:00:00", "").Replace(",", "&") + "@" + Flag.ToString() + "." + imagetype + "");
                    mhte.ImageFormat = FastReport.Export.Html.ImageFormat.Jpeg;
                    report.Export(mhte, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormImageDir") + "\\" + FormNo.Replace(" 00:00:00", "").Replace(",", "&") + "@" + Flag.ToString() + ".mht");
                    //report.Export(htmlexport, m);
                    report.Dispose();
                    string aaa = this.ReadContext(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormImageDir") + "\\" + FormNo.Replace(" 00:00:00", "").Replace(",", "&") + "@" + Flag.ToString() + ".mht");
                    for (int i = 2; i < 100; i++)
                    {
                        if (aaa.IndexOf("<a name=3D\"PageN" + i + "\"></a>") > 0)
                        {
                            aaa = aaa.Replace("<a name=3D\"PageN" + i + "\"></a>", "<p style=\"page-break-after:always\">&nbsp;</p>");
                        }
                        else
                        {
                            break;
                        }
                    }
                    WriteContext(aaa, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormImageDir") + "\\" + FormNo.Replace(" 00:00:00", "").Replace(",", "&") + "@" + Flag.ToString() + ".mht");
                    List<string> l = new List<string>();
                    l.Add(ZhiFang.Common.Public.ConfigHelper.GetConfigString("LocalhostURL") + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormImageDir") + "\\" + FormNo.Replace(" 00:00:00", "").Replace(",", "&") + "@" + Flag.ToString() + ".mht");
                    return l;
                }
                return null;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(e.ToString() + "---------" + e.StackTrace.ToString() + "---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
                return null;
            }
        }
        public void WriteContext(string Context, string path)
        {
            StreamWriter sw = new StreamWriter(path);
            sw.Write(Context);
            sw.Close();
            sw.Dispose();
        }
        private string ReadContext(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string context = sr.ReadToEnd();
            fs.Close();
            sr.Close();
            sr.Dispose();
            fs.Dispose();
            return context;
        }
        public List<string> PrintMergeHtml(string FormNo, ReportFormTitle Flag)
        {
            ZhiFang.Common.Log.Log.Info("PrintMergeHtml");
            try
            {
                DataSet dataSet;
                FastReport.Report report = new FastReport.Report();
                //FastReport.Export.Image.ImageExport tmpjpg = new FastReport.Export.Image.ImageExport();
                //tmpjpg.PageNumbers
                report.Clear();
                dataSet = this.dalmerge.GetModelDataFrFormAll(FormNo.Split(','), " order by ReportItemID ").DataSet;
                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    string imagetype = "jpeg";
                    //switch (ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormImageType").ToUpper())
                    //{
                    //    case "JPEG": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg; imagetype = "jpeg"; break;
                    //    case "JPG": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg; imagetype = "jpg"; break;
                    //    case "GIF": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Gif; imagetype = "gif"; break;
                    //    case "BMP": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Bmp; imagetype = "bmp"; break;
                    //    case "PNG": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Png; imagetype = "png"; break;
                    //    case "TIFF": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Tiff; imagetype = "tiff"; break;

                    //    default: tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg; imagetype = "jpeg"; break;
                    //}
                    if (Flag == ReportFormTitle.BatchPrint)
                    {
                        report.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelURL") + "\\套打合并.frx");
                    }
                    else
                    {
                        report.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelURL") + "\\合并.frx");
                    }
                    ZhiFang.Common.Log.Log.Info(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelURL") + "\\合并.frx");
                    FastReport.TextObject txtAdd = (FastReport.TextObject)report.FindObject("txtAdd");
                    string txtAddTitle = "";
                    if (txtAdd != null)
                    {
                        txtAddTitle = txtAdd.Text;
                    }
                    if (Flag == ReportFormTitle.center)
                    {
                        FastReport.TextObject texttitle = (FastReport.TextObject)report.FindObject("TextTitle");
                        if (texttitle != null)
                        {
                            texttitle.Text = ZhiFang.Common.Public.ConfigHelper.GetConfigString("CenterName") + txtAddTitle + "";
                        }
                        FastReport.PictureObject p = (FastReport.PictureObject)report.FindObject("Picture1");
                        if (p != null)
                        {
                            p.Visible = true;
                        }
                    }
                    if (Flag == ReportFormTitle.client)
                    {
                        FastReport.TextObject texttitle = (FastReport.TextObject)report.FindObject("TextTitle");
                        if (texttitle != null)
                        {
                            texttitle.Text = dataSet.Tables[0].Rows[0]["CLIENTNAME"].ToString() + txtAddTitle + "";
                        }
                        FastReport.PictureObject p = (FastReport.PictureObject)report.FindObject("Picture1");
                        if (p != null)
                        {
                            p.Visible = false;
                        }
                    }
                    dataSet.Tables[0].TableName = "frformall";
                    report.RegisterData(dataSet);
                    report.Prepare();
                    FastReport.Export.Mht.MHTExport mhte = new FastReport.Export.Mht.MHTExport();
                    mhte.ImageFormat = FastReport.Export.Html.ImageFormat.Jpeg;
                    report.Export(mhte, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormImageDir") + "\\" + FormNo.Replace(" 00:00:00", "").Replace(",", "__") + "@" + Flag.ToString() + ".mht");

                    report.Dispose();

                    string aaa = this.ReadContext(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormImageDir") + "\\" + FormNo.Replace(" 00:00:00", "").Replace(",", "__") + "@" + Flag.ToString() + ".mht");
                    for (int i = 2; i < 100; i++)
                    {
                        if (aaa.IndexOf("<a name=3D\"PageN" + i + "\"></a>") > 0)
                        {
                            aaa = aaa.Replace("<a name=3D\"PageN" + i + "\"></a>", "<p style=\"page-break-after:always\">&nbsp;</p>");
                        }
                        else
                        {
                            break;
                        }
                    }
                    WriteContext(aaa, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormImageDir") + "\\" + FormNo.Replace(" 00:00:00", "").Replace(",", "__") + "@" + Flag.ToString() + ".mht");
                    List<string> l = new List<string>();
                    l.Add(ZhiFang.Common.Public.ConfigHelper.GetConfigString("LocalhostURL") + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormImageDir") + "\\" + FormNo.Replace(" 00:00:00", "").Replace(",", "__") + "@" + Flag.ToString() + ".mht");
                    return l;

                    //report.Export(tmpjpg, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormImageDir") + "\\" + FormNo.Replace(" 00:00:00", "").Replace(",", "&") + "." + imagetype + "");                    
                    //return tmpjpg.GeneratedFiles;
                    //return "..\\"+ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormImageDir")+"\\" + FormNo.Replace(" 00:00:00", "") + "." + imagetype + "";
                }
                return null;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.ToString() + "---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
                return null;
            }
        }
        public List<string> PrintMergePdf(string FormNo, ReportFormTitle Flag)
        {
            ZhiFang.Common.Log.Log.Info("PrintMergePdf@" + Flag);
            try
            {
                DataSet dataSet;
                FastReport.Report report = new FastReport.Report();
                //FastReport.Export.Image.ImageExport tmpjpg = new FastReport.Export.Image.ImageExport();
                //tmpjpg.PageNumbers
                report.Clear();
                dataSet = this.dalmerge.GetModelDataFrFormAll(FormNo.Split(',')).DataSet;
                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    string imagetype = "jpeg";
                  
                    if (Flag == ReportFormTitle.BatchPrint)
                    {
                        report.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelURL") + "\\套打合并.frx");
                    }
                    else
                    {
                        report.Load(System.AppDomain.CurrentDomain.BaseDirectory + ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelURL") + "\\合并.frx");
                    }
                    ZhiFang.Common.Log.Log.Info(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelURL") + "\\合并.frx");
                    FastReport.TextObject txtAdd = (FastReport.TextObject)report.FindObject("txtAdd");
                    string txtAddTitle = "";
                    if (txtAdd != null)
                    {
                        txtAddTitle = txtAdd.Text;
                    }
                    if (Flag == ReportFormTitle.center)
                    {
                        ZhiFang.Common.Log.Log.Debug("@1");
                        FastReport.TextObject texttitle = (FastReport.TextObject)report.FindObject("TextTitle");
                        if (texttitle != null)
                        {
                            //texttitle.Text = ZhiFang.Common.Public.ConfigHelper.GetConfigString("CenterName") + txtAddTitle + "";
                            string flag = ZhiFang.Common.Public.ConfigHelper.GetConfigString("InnerReportTitleFlag");
                            if (flag.ToUpper() == "TRUE")
                            {
                                texttitle.Text = dataSet.Tables[0].Rows[0]["WebLisOrgName"] + txtAddTitle + "";
                            }
                            else
                            {
                                texttitle.Text = ZhiFang.Common.Public.ConfigHelper.GetConfigString("CenterTitle");
                            }
                        }
                        FastReport.PictureObject p = (FastReport.PictureObject)report.FindObject("Picture1");
                        if (p != null)
                        {
                            p.Visible = true;
                        }
                    }
                    if (Flag == ReportFormTitle.client)
                    {
                        ZhiFang.Common.Log.Log.Debug("@2");
                        FastReport.TextObject texttitle = (FastReport.TextObject)report.FindObject("TextTitle");
                        if (texttitle != null)
                        {
                            texttitle.Text = dataSet.Tables[0].Rows[0]["CLIENTNAME"].ToString() + txtAddTitle + "";

                        }
                        FastReport.PictureObject p = (FastReport.PictureObject)report.FindObject("Picture1");
                        if (p != null)
                        {
                            p.Visible = false;
                        }
                        FastReport.PageHeaderBand rt = (FastReport.PageHeaderBand)report.FindObject("PageHeader1");
                        if (rt != null)
                        {
                            ZhiFang.Common.Log.Log.Debug("@3");
                            rt.Visible = false;
                        }

                    }
                    #region 样本类型
                    FastReport.TextObject Pallsampletypenametext = (FastReport.TextObject)report.FindObject("TxtAllSampleTypeName");
                    
                    if (Pallsampletypenametext != null)
                    {
                        string allsampletypename = "";
                        for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                        {
                            if(allsampletypename.IndexOf(dataSet.Tables[0].Rows[i]["SampleTypename"].ToString().Trim()+",")<0)
                            {
                                allsampletypename += dataSet.Tables[0].Rows[i]["SampleTypename"].ToString().Trim() + ",";
                            }
                        }
                        if (allsampletypename.Length > 0)
                            allsampletypename = allsampletypename.Substring(0, allsampletypename.Length - 1);
                        Pallsampletypenametext.Text = allsampletypename;
                    }
                    #endregion
                    #region 电子签名
                    DateTime receivedate = Convert.ToDateTime(dataSet.Tables[0].Rows[0]["RECEIVEDATE"].ToString());
                    string filepath = ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportIncludeImage")+"\\" + receivedate.Year + "\\" + receivedate.Month + "\\" + receivedate.Day + "\\" + dataSet.Tables[0].Rows[0]["FORMNO"].ToString();
                    ZhiFang.Common.Log.Log.Debug("合并报告单电子签名路径：" + filepath);
                    if (Directory.Exists(filepath))
                    {
                        FastReport.PictureObject PNOperatorText = (FastReport.PictureObject)report.FindObject("PNOperatorText");
                        if (PNOperatorText != null)
                        {
                            string[] PNOperatorTextimagelist = Directory.GetFiles(filepath, "*NOperator*", System.IO.SearchOption.AllDirectories);
                            if (PNOperatorTextimagelist.Length > 0)
                            {
                                PNOperatorText.ImageLocation = PNOperatorTextimagelist[0];
                                PNOperatorText.Visible = true;
                                ZhiFang.Common.Log.Log.Info("NOperator图片名称" + PNOperatorTextimagelist[0]);
                            }
                            else
                            {
                                ZhiFang.Common.Log.Log.Info("NOperator图片在" + filepath + "下未找到");
                            }

                        }
                        FastReport.PictureObject PTechniciantext = (FastReport.PictureObject)report.FindObject("PTechniciantext");
                        if (PTechniciantext != null)
                        {
                            string[] PTechniciantextimagelist = Directory.GetFiles(filepath, "*technician*", System.IO.SearchOption.AllDirectories);
                            if (PTechniciantextimagelist.Length > 0)
                            {
                                PTechniciantext.ImageLocation = PTechniciantextimagelist[0];
                                PTechniciantext.Visible = true;
                                ZhiFang.Common.Log.Log.Info("technician图片名称" + PTechniciantextimagelist[0]);
                            }
                            else
                            {
                                ZhiFang.Common.Log.Log.Info("technician图片在" + filepath + "下未找到");
                            }

                        }
                        FastReport.PictureObject Pcheckertext = (FastReport.PictureObject)report.FindObject("Pcheckertext");
                        if (Pcheckertext != null)
                        {
                            string[] Pcheckertextimagelist = Directory.GetFiles(filepath, "*Checker*", System.IO.SearchOption.AllDirectories);
                            if (Pcheckertextimagelist.Length > 0)
                            {
                                Pcheckertext.ImageLocation = Pcheckertextimagelist[0];
                                Pcheckertext.Visible = true;
                                ZhiFang.Common.Log.Log.Info("Checker图片名称" + Pcheckertextimagelist[0]);
                            }
                            else
                            {
                                ZhiFang.Common.Log.Log.Info("Checker图片在" + filepath + "下未找到");
                            }

                        }
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug("合并报告单电子签名路径不存在：" + filepath);
                    }                   

                    #endregion
                    dataSet.Tables[0].TableName = "frformall";
                    report.RegisterData(dataSet);
                    report.Prepare();
                    FastReport.Export.Pdf.PDFExport pdfexport = new FastReport.Export.Pdf.PDFExport();
                  
                    //string pdfFilePath = System.AppDomain.CurrentDomain.BaseDirectory + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + FormNo.Replace(" 00:00:00", "").Replace(",", "__") + "@" + Flag.ToString() + ".pdf";
                    //report.Export(pdfexport, pdfFilePath);
                    //report.Dispose();
                    //List<string> l = new List<string>();
                    //l.Add(ZhiFang.Common.Public.ConfigHelper.GetConfigString("LocalhostURL") + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + FormNo.Replace(" 00:00:00", "").Replace(",", "__") + "@" + Flag.ToString() + ".pdf");
                    
                    //生成报告改成GUID的方式,避免每次加载文件去访问同一个文件 
                    string pdfName = ZhiFang.BLL.Common.GUIDHelp.GetGUIDString();
                    string tmpdate = DateTime.Now.Year.ToString()+"\\"+DateTime.Now.Month.ToString() + "\\"+DateTime.Now.Day.ToString() + "\\";
                    string pdfFilePath = System.AppDomain.CurrentDomain.BaseDirectory + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\"+tmpdate + pdfName + "@" + Flag.ToString() + ".pdf";
                    //判断并创建目录
                    if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + tmpdate))
                    {
                        Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + tmpdate);
                    }
                    report.Export(pdfexport, pdfFilePath);
                    report.Dispose();
                    List<string> l = new List<string>();
                    l.Add(ZhiFang.Common.Public.ConfigHelper.GetConfigString("LocalhostURL") + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + tmpdate + pdfName + "@" + Flag.ToString() + ".pdf");
                    return l;
                }
                return null;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.ToString() + "---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
                return null;
            }
        }
        public List<string> PrintMergeEnHtml(string FormNo, ReportFormTitle Flag)
        {
            try
            {
                DataSet dataSet;
                FastReport.Report report = new FastReport.Report();
                //FastReport.Export.Image.ImageExport tmpjpg = new FastReport.Export.Image.ImageExport();
                //tmpjpg.PageNumbers
                report.Clear();
                dataSet = this.dalmerge.GetModelDataFrFormAll(FormNo.Split(',')).DataSet;
                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    report.RegisterData(dataSet);
                    report.Prepare();
                    //string imagetype = "jpeg";
                    //switch (ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormImageType").ToUpper())
                    //{
                    //    case "JPEG": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg; imagetype = "jpeg"; break;
                    //    case "JPG": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg; imagetype = "jpg"; break;
                    //    case "GIF": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Gif; imagetype = "gif"; break;
                    //    case "BMP": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Bmp; imagetype = "bmp"; break;
                    //    case "PNG": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Png; imagetype = "png"; break;
                    //    case "TIFF": tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Tiff; imagetype = "tiff"; break;
                    //    default: tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg; imagetype = "jpeg"; break;
                    //}
                    if (Flag == ReportFormTitle.BatchPrint)
                    {
                        report.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelURL") + "\\套打合并英文.frx");
                    }
                    else
                    {
                        report.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelURL") + "\\合并英文.frx");
                    }
                    FastReport.TextObject txtAdd = (FastReport.TextObject)report.FindObject("txtAdd");
                    string txtAddTitle = "";
                    if (txtAdd != null)
                    {
                        txtAddTitle = txtAdd.Text;
                    }
                    if (Flag == ReportFormTitle.center)
                    {

                        FastReport.TextObject texttitle = (FastReport.TextObject)report.FindObject("TextTitle");
                        if (texttitle != null)
                        {
                            texttitle.Text = ZhiFang.Common.Public.ConfigHelper.GetConfigString("CenterName") + txtAddTitle + "";
                        }
                        FastReport.PictureObject p = (FastReport.PictureObject)report.FindObject("Picture1");
                        if (p != null)
                        {
                            p.Visible = true;
                        }
                    }
                    if (Flag == ReportFormTitle.client)
                    {
                        FastReport.TextObject texttitle = (FastReport.TextObject)report.FindObject("TextTitle");
                        if (texttitle != null)
                        {
                            texttitle.Text = dataSet.Tables[0].Rows[0]["CLIENTNAME"].ToString() + txtAddTitle + "";
                        }
                        FastReport.PictureObject p = (FastReport.PictureObject)report.FindObject("Picture1");
                        if (p != null)
                        {
                            p.Visible = false;
                        }
                    }
                    FastReport.Export.Mht.MHTExport mhte = new FastReport.Export.Mht.MHTExport();
                    mhte.ImageFormat = FastReport.Export.Html.ImageFormat.Jpeg;
                    report.Export(mhte, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormImageDir") + "\\" + FormNo.Replace(" 00:00:00", "").Replace(",", "&") + "@" + Flag.ToString() + ".mht");

                    report.Dispose();

                    string aaa = this.ReadContext(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormImageDir") + "\\" + FormNo.Replace(" 00:00:00", "").Replace(",", "&") + "@" + Flag.ToString() + ".mht");
                    for (int i = 2; i < 100; i++)
                    {
                        if (aaa.IndexOf("<a name=3D\"PageN" + i + "\"></a>") > 0)
                        {
                            aaa = aaa.Replace("<a name=3D\"PageN" + i + "\"></a>", "<p style=\"page-break-after:always\">&nbsp;</p>");
                        }
                        else
                        {
                            break;
                        }
                    }
                    WriteContext(aaa, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormImageDir") + "\\" + FormNo.Replace(" 00:00:00", "").Replace(",", "&") + "@" + Flag.ToString() + ".mht");
                    List<string> l = new List<string>();
                    l.Add(ZhiFang.Common.Public.ConfigHelper.GetConfigString("LocalhostURL") + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormImageDir") + "\\" + FormNo.Replace(" 00:00:00", "").Replace(",", "&") + "@" + Flag.ToString() + ".mht");
                    return l;

                    //report.Export(tmpjpg, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormImageDir") + "\\" + FormNo.Replace(" 00:00:00", "").Replace(",", "&") + "." + imagetype + "");
                    //report.Dispose();
                    //return tmpjpg.GeneratedFiles;
                    //return "..\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormImageDir") + "\\" + FormNo.Replace(" 00:00:00", "") + "." + imagetype + "";
                }
                return null;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(e.ToString() + "---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
                return null;
            }
        }
        public Model.PrintFormat GetPrintModelInfo(string FormNo)
        {
            throw new NotImplementedException();
        }
        private string FindMode(DataRow dr, DataTable table, ReportFormTitle flag)
        {
            return FindMode(dr, table, null, flag);
        }
        private string FindMode(DataRow dr, DataTable table)
        {
            return FindMode(dr, table, null, this.titelflag);
        }
        private string FindMode(DataRow dr, DataTable table, DataTable table1, ReportFormTitle flag)
        {
            try
            {
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
                if (Convert.ToInt32(flag) > 2)
                {
                    t.SickTypeNo = Convert.ToInt32(flag) - 2;
                }
                table4 = this.PGroupPrint.GetList(t).Tables[0];
                if (table4.Rows.Count <= 0)
                {
                    return null;
                }
                else
                {
                    bool fitem = false;
                    bool fclient = false;
                    bool fitemcount = false;
                    string str = "";
                    int itemcount = 0;
                    string where = "";
                    ZhiFang.Common.Log.Log.Info("报告模板查询_特殊项目配置个数：" + table4.Select(" SpecialtyItemNo is null ").Count().ToString());
                    if (table4.Select(" SpecialtyItemNo is null ").Count() != table4.Rows.Count)
                    {

                        if (table != null && table.Rows.Count > 0)
                        {
                            for (int num = 0; num < table.Rows.Count; num++)
                            {
                                str = str + " SpecialtyItemNo = " + table.Rows[num]["itemno"].ToString() + " or ";
                            }
                        }
                        if (table1 != null && table1.Rows.Count > 0)
                        {
                            for (int num = 0; num < table1.Rows.Count; num++)
                            {
                                str = str + " SpecialtyItemNo = " + table1.Rows[num]["itemno"].ToString() + " or ";
                            }
                        }
                        str = str.Substring(0, str.LastIndexOf(" or "));
                        ZhiFang.Common.Log.Log.Info("报告模板查询_匹配特殊项目：" + str);
                        if (table4.Select(" " + str).Count() > 0)
                        {
                            fitem = true;
                        }
                    }
                    if (table4.Select(" clientno is null ").Count() != table4.Rows.Count && dr["clientno"] != DBNull.Value)
                    {
                        if (table4.Select(" clientno=" + dr["clientno"].ToString()).Count() > 0)
                        {
                            fclient = true;
                        }
                    }
                    if (table4.Select(" ItemMinNumber is null and ItemMaxNumber is null ").Count() != table4.Rows.Count)
                    {
                        if (table != null)
                        {
                            itemcount += table.Rows.Count;
                        }
                        if (table1 != null)
                        {
                            itemcount += table1.Rows.Count;
                        }
                        if (table4.Select(" ItemMinNumber<=" + itemcount + " and ItemMaxNumber>=" + itemcount).Count() > 0)
                        {
                            fitemcount = true;
                        }
                    }
                    if (fitem == false && fclient && fitemcount)
                    {
                        where = " clientno=" + dr["clientno"].ToString() + " and ( ItemMinNumber<=" + itemcount + " and ItemMaxNumber>=" + itemcount + " ) ";
                    }
                    if (fitem == false && fclient == false && fitemcount)
                    {
                        where = " ( ItemMinNumber<=" + itemcount + " and ItemMaxNumber>=" + itemcount + " ) ";
                    }
                    if (fitem == false && fclient == false && fitemcount == false)
                    {
                        where = " 1=1 ";
                    }
                    if (fitem && fclient == false && fitemcount)
                    {
                        where = " " + str + " and ( ItemMinNumber<=" + itemcount + " and ItemMaxNumber>=" + itemcount + " ) ";
                    }
                    if (fitem && fclient == false && fitemcount == false)
                    {
                        where = " " + str;
                    }
                    if (fitem && fclient && fitemcount == false)
                    {
                        where = " " + str + " and clientno=" + dr["clientno"].ToString();
                    }
                    if (fitem && fclient && fitemcount)
                    {
                        where = " " + str + " and clientno=" + dr["clientno"].ToString() + " and  ( ItemMinNumber<=" + itemcount + " and ItemMaxNumber>=" + itemcount + " ) ";
                    }
                    DataRow[] dra = table4.Select(where, "Sort asc,Id desc");
                    ZhiFang.Common.Log.Log.Info("报告模板查询Sub：" + where + ",排序：Sort asc,Id desc");
                    if (dra.Count() > 0)
                    {
                        formatPrint = dra[0]["PrintFormatNo"].ToString();
                        ZhiFang.Common.Log.Log.Info("报告模板ID：" + formatPrint);
                    }
                    else
                    {

                    }
                }
                try
                {
                    if (formatPrint != "")
                    {
                        Model.PrintFormat pf_m = pfb.GetModel(formatPrint);
                        string showmodel = pf_m.PintFormatAddress.ToString().Trim() + "\\" + pf_m.Id + "\\" + pf_m.Id + ".FRX";
                        ZhiFang.Common.Log.Log.Info("模板名称:" + showmodel);
                        // D:\zhifang\92-检验之星8\WebLis\WebLis\//XSL\Creat\病理组织\7\7.FRX
                        formatPrint = ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelURL") + "\\" + showmodel);
                        formatPrint = formatPrint.Replace("//", "");
                        ZhiFang.Common.Log.Log.Info("模板路径:" + formatPrint);
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Info("模板不存在");
                    }
                    //formatPrint = p.Server.MapPath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("LocalhostURL") + ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelURL") + "\\" + showmodel);
                }
                catch (Exception e)
                {
                    ZhiFang.Common.Log.Log.Debug(e.StackTrace.ToString() + "---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
                    return "";
                }
                return formatPrint;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.StackTrace.ToString() + "---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
                return "";
            }
        }
        private void RegeditData(DataTable table, ref FastReport.Report report, string formatPrint)
        {
            try
            {
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
                    if (((obj2 != null) && Validate.IsInt(obj2.Text.Trim())) && (Convert.ToInt32(obj2.Text.Trim()) < table.Rows.Count))
                    {
                        int PageSize = int.Parse(obj2.Text.Trim());
                        int PageCount = (table.Rows.Count / PageSize) + 1;
                        if (PageCount > 0)
                        {
                            table2 = table.Clone();
                            table3 = table.Clone();
                            for (int Pageindex = 0; Pageindex < PageCount; Pageindex++)
                            {
                                for (int i = Pageindex * PageSize; i < ((Pageindex + 1) * PageSize); i++)
                                {
                                    if (i >= table.Rows.Count)
                                    {
                                        break;
                                    }
                                    if ((Pageindex % 2) == 0)
                                    {
                                        table2.Rows.Add(table.Rows[i].ItemArray);
                                    }
                                    else
                                    {
                                        table3.Rows.Add(table.Rows[i].ItemArray);
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
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.StackTrace.ToString() + "---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
            }
        }
        public ReportFormTitle TitleFlag
        {
            set { titelflag = value; }
            get { return titelflag; }
        }
        public List<string> PrintHtml(string FormNo)
        {
            return PrintHtml(FormNo, this.titelflag);
        }
        public List<string> PrintMergeHtml(string FormNo)
        {
            return PrintMergeHtml(FormNo, this.titelflag);
        }
        public List<string> PrintMergeEnHtml(string FormNo)
        {
            return PrintMergeEnHtml(FormNo, this.titelflag);
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
