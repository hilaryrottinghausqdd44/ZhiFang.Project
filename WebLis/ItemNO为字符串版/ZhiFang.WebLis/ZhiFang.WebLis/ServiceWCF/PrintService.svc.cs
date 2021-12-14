using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using ZhiFang.IBLL.Report;
using ZhiFang.IBLL.Common.BaseDictionary;
using ZhiFang.BLLFactory;
using ZhiFang.Model;
using System.Data;
using ZhiFang.Model.UiModel;
using System.Collections;
using Newtonsoft.Json;
using ZhiFang.Common.Public;
using System.Web;
using ZhiFang.Common.Dictionary;
using System.IO;
using System.Runtime.InteropServices;
using System.Configuration;
using ZhiFang.BLL.Report.Print;
using Microsoft.Win32;
using System.Diagnostics;

namespace ZhiFang.WebLis.ServiceWCF
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class PrintService : ZhiFang.WebLis.ServerContract.IPrintService
    {
        private readonly IBItemMergeRule ibim = BLLFactory<IBItemMergeRule>.GetBLL();
        private readonly IBPrintFrom_Weblis Printform_Weblis = BLLFactory<IBPrintFrom_Weblis>.GetBLL("PrintFrom_Weblis");
        private readonly IBPrintFrom_Weblis Printform_WeblisFr3 = BLLFactory<IBPrintFrom_Weblis>.GetBLL("PrintUseDeliph");
        //public List<string> Print_NRequestForm(string PatientName, string CollectStartDate, string CollectEndDate, string AddStartDate, string AddEndDate, string Doctor, string WebLisSourceOrgID, string PatNo, string SampleTypeNo)
        //{
        //    List<string> ReportList = new List<string>();
        //    return ReportList;
        //}       

        /*
        /// <summary>
        /// 报告打印
        /// </summary>
        /// <param name="reportformId"></param>
        /// <param name="printtype"></param>
        /// <param name="reportformtitle"></param>
        /// <returns></returns>
        public BaseResultDataValue ReportPrint(string reportformId, string printtype, string reportformtitle)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            #region 验证
            if (reportformId == null || reportformId == "")
                return null;
            if (printtype == null || printtype == "")
                return null;
            if (reportformtitle == null || reportformtitle == "")
                return null;
            #endregion

            #region 打印报告

            try
            {
                List<string> ReportFormContextList = null;
                string title = "0";
                switch (reportformtitle)
                {
                    case "Center": title = "0"; break;
                    case "Client": title = "1"; break;
                    case "Batch": title = "2"; break;
                    case "MenZhen": title = "3"; break;
                    case "ZhuYuan": title = "4"; break;
                    case "TiJian": title = "5"; break;
                    default: title = "0"; break;
                }
                ReportFormContextList = this.PrintHtml(reportformId.Split(','), title);              

                var settings = new JsonSerializerSettings();
                string aa = JsonConvert.SerializeObject(ReportFormContextList, Formatting.Indented, settings);

                brdv.success = true;
                brdv.ResultDataValue = aa;
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.ToString();
                brdv.ResultDataValue = "";
            }
            #endregion

            return brdv;
        }
        */

        /// <summary>
        /// 报告打印
        /// </summary>
        /// <param name="reportformId">报告单ID</param>
        /// <param name="reportformtitle">报告抬头</param>
        /// <param name="reportformfiletype">报告文件类型（预留）</param>
        /// <param name="printtype">打印类型（预留）</param>
        /// <returns></returns>
        public BaseResultDataValue ReportPrint(string reportformId, string reportformtitle, string reportformfiletype, string printtype)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            #region 验证
            if (reportformId == null || reportformId == "")
                return null;
            if (printtype == null || printtype == "")
                return null;
            if (reportformtitle == null || reportformtitle == "")
                return null;
            #endregion

            #region 打印报告

            try
            {
                List<string> ReportFormContextList = null;
                string title = "0";
                switch (reportformtitle.ToUpper())
                {
                    case "CENTER": title = "0"; break;
                    case "CLIENT": title = "1"; break;
                    case "BATCH": title = "2"; break;
                    case "MENZHEN": title = "3"; break;
                    case "ZHUYUAN": title = "4"; break;
                    case "TIJIAN": title = "5"; break;
                    default: title = "0"; break;
                }
                //报告上传过程中提前生成的报告
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("isUpLoadGeneReport") == "1")
                {
                    ZhiFang.Common.Log.Log.Info("isUpLoadGeneReport：1");
                    ZhiFang.BLL.Report.ReportFormFull rffb = new ZhiFang.BLL.Report.ReportFormFull();
                    Model.ReportFormFull rff_m = new Model.ReportFormFull();
                    rff_m.ReportFormID = reportformId; //"_138723_25_1_9_2011-04-21 00:00:00";
                    DataSet dsrf = rffb.GetList(rff_m);
                    if (dsrf != null && dsrf.Tables[0].Rows.Count > 0)
                        ReportFormContextList = PrintReportFormCommon.FindReportFormFiles(dsrf.Tables[0].Rows[0], (ZhiFang.Common.Dictionary.ReportFormTitle)Convert.ToInt32(title), title);
                }
                else
                    ReportFormContextList = this.PrintHtml(reportformId.Split(','), title, reportformfiletype);


                string aa = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(ReportFormContextList);

                brdv.success = true;
                brdv.ResultDataValue = aa;
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.ToString();
                brdv.ResultDataValue = "";
            }
            #endregion
            /**/
            return brdv;
        }

        #region 报告合并
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ReportFormIDs">报告单ID，通过逗号分隔</param>
        /// <param name="Reportformtitle">报告抬头,CENTER或CLIENT或其它</param>
        /// <returns></returns>
        public BaseResultDataValue GetReportFullMerge(string ReportFormIDs, string Reportformtitle)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                //string reportformid1s = ReportFormIDs;//"_1000003_2_1_714_2012-12-13 00:00:00,_1000005_2_1_716_2012-12-13 00:00:00,_1000011_2_1_717_2012-12-13 00:00:00";
                ZhiFang.Common.Log.Log.Debug("GetReportFullMerge.ReportFormIDs：" + ReportFormIDs.ToString() + ";Reportformtitle:" + Reportformtitle);
                string reportformtitle = Reportformtitle;//"CENTER";
                List<string> ReportFormContextList = null;
                string[] ImageList = null;                
                string PrintType = "A4";
                string title = "0";
                switch (reportformtitle.ToUpper())
                {
                    case "CENTER": title = "0"; break;
                    case "CLIENT": title = "1"; break;
                    case "BATCH": title = "2"; break;
                    case "MENZHEN": title = "3"; break;
                    case "ZHUYUAN": title = "4"; break;
                    case "TIJIAN": title = "5"; break;
                    default: title = "0"; break;
                }

                List<string> reportFormIds = ReportFormIDs.Split('|').ToList();//ReportFormIDs.Split(',').ToList();//ReportFormIdGroupByCnameOrPatno(reportformid1s.Split(','), GroupByCnameOrPatNo);
                if (PrintType != null && PrintType == "A4")
                {
                    ImageList = this.PrintMergeHtml(reportFormIds, title);
                }
                ReportFormContextList = ImageList.ToList();
                string aa = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(ReportFormContextList);
                brdv.ResultDataValue = aa;
                brdv.success = true;
                return brdv;
            }
            catch (Exception ex)
            {
                brdv.ErrorInfo = ex.ToString();
                brdv.success = false;
                ZhiFang.Common.Log.Log.Debug("GetReportFullMerge异常："+ex.ToString());
                return brdv;
            }
        }

        /// <summary>
        /// 判断多个reportformIDs中,相同的Cname或PatNo放在同一条记录,用逗号隔开
        /// </summary>
        /// <param name="reportformIDs"></param>
        /// <param name="GroupByCnameOrPatNo">PatNo或CName</param>
        /// <returns></returns>
        public BaseResultDataValue ReportFormIdGroupByCnameOrPatno(string reportformIDs, string GroupByCnameOrPatNo)
        {
            IBReportFormFull ibrff = BLLFactory<IBReportFormFull>.GetBLL("ReportFormFull");
            List<Model.ReportFormFull> reportmodels = new List<ReportFormFull>();
            string[] bb = reportformIDs.Split(',');
            for (int i = 0; i < bb.Length; i++)
            {
                Model.ReportFormFull model = ibrff.GetModel(bb[i].ToString());
                if (model != null)
                    reportmodels.Add(model);
            }

            List<Model.ReportFormFull> temps = new List<Model.ReportFormFull>();
            Model.ReportFormFull temp;
            #region 根据名字分组
            if (GroupByCnameOrPatNo == "CName" && reportmodels.Count > 0)
            {
                List<string> cnamelist = new List<string>();
                var groupresult = reportmodels.GroupBy(a => a.CNAME + "_" + a.SERIALNO);

                foreach (var cnameserialno in groupresult)
                {
                    var tmprequest = reportmodels.FindAll(p => p.CNAME + "_" + p.SERIALNO == cnameserialno.Key);
                    //1血清6血浆7全血2尿液4胸水5腹水9脑脊液
                    var tmplist2 = tmprequest.Where(a => a.SAMPLETYPENO == 2);
                    foreach (var tmp in tmplist2)
                    {
                        ReportFormFull tmprff = new ReportFormFull();
                        tmprff.ReportFormID = tmp.ReportFormID;
                        tmprff.CNAME = tmp.CNAME;
                        tmprff.RECEIVEDATE = tmp.RECEIVEDATE;
                        tmprff.SAMPLENO = tmp.SAMPLENO;
                        if (tmprff.ReportFormID != null)
                            temps.Add(tmprff);
                    }
                    var tmplist4 = tmprequest.Where(a => a.SAMPLETYPENO == 4);
                    foreach (var tmp in tmplist4)
                    {
                        ReportFormFull tmprff = new ReportFormFull();
                        tmprff.ReportFormID = tmp.ReportFormID;
                        tmprff.CNAME = tmp.CNAME;
                        tmprff.RECEIVEDATE = tmp.RECEIVEDATE;
                        tmprff.SAMPLENO = tmp.SAMPLENO;
                        if (tmprff.ReportFormID != null)
                            temps.Add(tmprff);
                    }
                    var tmplist5 = tmprequest.Where(a => a.SAMPLETYPENO == 5);
                    foreach (var tmp in tmplist5)
                    {
                        ReportFormFull tmprff = new ReportFormFull();
                        tmprff.ReportFormID = tmp.ReportFormID;
                        tmprff.CNAME = tmp.CNAME;
                        tmprff.RECEIVEDATE = tmp.RECEIVEDATE;
                        tmprff.SAMPLENO = tmp.SAMPLENO;
                        if (tmprff.ReportFormID != null)
                            temps.Add(tmprff);
                    }
                    var tmplist9 = tmprequest.Where(a => a.SAMPLETYPENO == 9);
                    foreach (var tmp in tmplist9)
                    {
                        ReportFormFull tmprff = new ReportFormFull();
                        tmprff.ReportFormID = tmp.ReportFormID;
                        tmprff.CNAME = tmp.CNAME;
                        tmprff.RECEIVEDATE = tmp.RECEIVEDATE;
                        tmprff.SAMPLENO = tmp.SAMPLENO;
                        if (tmprff.ReportFormID != null)
                            temps.Add(tmprff);
                    }
                    var tmplist6 = tmprequest.Where(a => a.SAMPLETYPENO == 6);
                    IBReportItemFull ibrif = BLLFactory<IBReportItemFull>.GetBLL("ReportItemFull");
                    List<string> outreportformid6 = new List<string>();
                    foreach (var tmp in tmplist6)
                    {
                        DataSet dsitem = ibrif.GetList(" ReportFormID='" + tmp.ReportFormID + "' ");
                        if (dsitem != null && dsitem.Tables.Count > 0 && dsitem.Tables[0].Rows.Count > 0)
                        {
                            if (!(dsitem.Tables[0].Rows.Count == 1 && dsitem.Tables[0].Rows[0]["ITEMNO"].ToString().Trim() == "10010280"))
                            {
                                ReportFormFull tmprff = new ReportFormFull();
                                tmprff.ReportFormID = tmp.ReportFormID;
                                tmprff.CNAME = tmp.CNAME;
                                tmprff.RECEIVEDATE = tmp.RECEIVEDATE;
                                tmprff.SAMPLENO = tmp.SAMPLENO;

                                if (tmprff.ReportFormID != null)
                                {
                                    temps.Add(tmprff);
                                    outreportformid6.Add(tmp.ReportFormID);
                                }
                            }
                        }
                    }

                    var tmplistMerge = tmprequest.Where(a => a.SAMPLETYPENO == 1 || a.SAMPLETYPENO == 6 || a.SAMPLETYPENO == 7);
                    temp = new ReportFormFull();
                    foreach (var tmp in tmplistMerge)
                    {
                        if (!outreportformid6.Contains(tmp.ReportFormID.Trim()))
                        {
                            if (temp.ReportFormID == null)
                            {
                                temp.ReportFormID = tmp.ReportFormID;
                                temp.CNAME = tmp.CNAME;
                                temp.RECEIVEDATE = tmp.RECEIVEDATE;
                                temp.SAMPLENO = tmp.SAMPLENO;
                            }
                            else
                            {
                                temp.ReportFormID += "," + tmp.ReportFormID;
                                temp.CNAME = tmp.CNAME;
                                temp.RECEIVEDATE = tmp.RECEIVEDATE;
                                temp.SAMPLENO = tmp.SAMPLENO;
                            }
                        }                        
                    }
                    if (temp.ReportFormID != null)
                        temps.Add(temp);
                }

                //var querys = reportmodels.GroupBy(p => new { p.CNAME }).Select(g => new { cname = g.Key });
                //foreach (var q in querys)
                //{
                //    cnamelist.Add(q.cname.ToString().Split('=')[1].Split('}')[0].Trim());
                //}

                //foreach (string cname in cnamelist)
                //{
                //    temp = new ReportFormFull();
                //    foreach (var reportModel in reportmodels.FindAll(p => p.CNAME == cname))
                //    {
                //        if (temp.ReportFormID == null)
                //        {
                //            temp.ReportFormID = reportModel.ReportFormID;
                //            temp.CNAME = reportModel.CNAME;
                //            temp.RECEIVEDATE = reportModel.RECEIVEDATE;
                //            temp.SAMPLENO = reportModel.SAMPLENO;
                //        }
                //        else
                //        {
                //            temp.ReportFormID += "," + reportModel.ReportFormID;
                //            temp.CNAME = reportModel.CNAME;
                //            temp.RECEIVEDATE = reportModel.RECEIVEDATE;
                //            temp.SAMPLENO = reportModel.SAMPLENO;
                //        }
                //    }
                //    if (temp.ReportFormID != null)
                //        temps.Add(temp);
                //}
            }
            #endregion
            #region 根据病历号分组
            if (GroupByCnameOrPatNo == "PatNo" && reportmodels.Count > 0)
            {
                List<string> patNolist = new List<string>();
                var querys = reportmodels.GroupBy(p => new { p.PATNO }).Select(g => new { patno = g.Key });
                foreach (var q in querys)
                {
                    patNolist.Add(q.patno.ToString().Split('=')[1].Split('}')[0].Trim());
                }

                foreach (string patNo in patNolist)
                {
                    temp = new ReportFormFull();
                    foreach (var reportModel in reportmodels.FindAll(p => p.PATNO == patNo))
                    {
                        if (temp.ReportFormID == null)
                        {
                            temp.ReportFormID = reportModel.ReportFormID;
                            temp.CNAME = reportModel.CNAME;
                            temp.RECEIVEDATE = reportModel.RECEIVEDATE;
                            temp.SAMPLENO = reportModel.SAMPLENO;
                        }
                        else
                        {
                            temp.ReportFormID += "," + reportModel.ReportFormID;
                            temp.CNAME = reportModel.CNAME;
                            temp.RECEIVEDATE = reportModel.RECEIVEDATE;
                            temp.SAMPLENO = reportModel.SAMPLENO;
                        }
                    }
                    if (temp.ReportFormID != null)
                        temps.Add(temp);
                }
            }
            #endregion
            //return temps;

            BaseResultDataValue brdv = new BaseResultDataValue();
            EntityListEasyUI<Model.ReportFormFull> bResult = new EntityListEasyUI<ReportFormFull>();
            bResult.total = temps.Count;
            bResult.rows = temps;
            string aa = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(bResult);
            brdv.ResultDataValue = aa;

            brdv.success = true;
            return brdv;
        }

        public BaseResultDataValue GetReportFullMergeEn(string reportformids)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            ////reportformids = "";
            //PrintMergeEnHtml(reportformids, titelflag);
            return brdv;
        }

        private string[] PrintMergeHtml(List<string> ReportFormIDs, string title)
        {

            IBReportFormFull ibrff = BLLFactory<IBReportFormFull>.GetBLL("ReportFormFull");
            ZhiFang.Common.Log.Log.Info("这是PrintMergeHtml()方法：PrintService.svc");
            //ZhiFang.Common.Log.Log.Info(title.ToString() + "---------" + ReportFormID.GetByIndex(0).ToString() + "---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
            List<string> tmp = new List<string>();
            List<string> tmplist = new List<string>();

            for (int i = 0; i < ReportFormIDs.Count; i++)
            {

                if (ConfigHelper.GetConfigString("MergeModelType").ToString() == "frx")
                {
                    tmp = Printform_Weblis.PrintMergePdf(ReportFormIDs[i], (ZhiFang.Common.Dictionary.ReportFormTitle)Convert.ToInt32(title));
                }
                if (tmp != null)
                {
                    for (int j = 0; j < tmp.Count; j++)
                    {
                        tmplist.Add(tmp[j]);
                    }
                }
            }


            string[] tmphtml = new string[tmplist.Count];
            for (int i = 0; i < tmplist.Count; i++)
            {
                //tmphtml[i] = "../" + tmplist[i].Substring(tmplist[i].LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")), tmplist[i].Length - tmplist[i].LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")));
                tmphtml[i] = "../" + tmplist[i].Substring(tmplist[i].LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")), tmplist[i].Length - tmplist[i].LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")));
            }
            return tmphtml;
        }

        #endregion

        public List<string> PrintHtml(string[] ReportFormID, string title)
        {
            return this.PrintHtml(ReportFormID, title, ZhiFang.Common.Dictionary.ReportFormFileType.JPEG.ToString());
        }

        public List<string> PrintHtml(string[] ReportFormID, string title, string reportformfiletype)
        {
            IBPrintReportForm ibprf = BLLFactory<IBPrintReportForm>.GetBLL("Print.PrintReportForm");
            List<string> tmp;
            List<string> tmplist = new List<string>();
            tmp = ibprf.PrintReportFormHtml(ReportFormID.ToList<string>(), (ZhiFang.Common.Dictionary.ReportFormTitle)Convert.ToInt32(title), (ZhiFang.Common.Dictionary.ReportFormFileType)Enum.Parse(typeof(ZhiFang.Common.Dictionary.ReportFormFileType), reportformfiletype));
            if (tmp != null)
            {
                for (int j = 0; j < tmp.Count; j++)
                {
                    tmplist.Add(tmp[j]);
                }
            }

            List<string> tmphtml = new List<string>();
            string appurl = System.Web.HttpContext.Current.Request.ApplicationPath.ToString();
            string applocalroot = System.AppDomain.CurrentDomain.BaseDirectory;//获取程序根目录
            //string imagesurl2 = imagesurl1.Replace(tmpRootDir, ""); //转换成相对路径
            //imagesurl2 = imagesurl2.Replace(@"\", @"/");
            //return imagesurl2;

            for (int i = 0; i < tmplist.Count; i++)
            {
                tmphtml.Add(tmplist[i].Replace(applocalroot, "").Replace(@"\", @"/"));
                //tmphtml.Add(appurl + tmplist[i].Replace(applocalroot, "").Replace(@"\", @"/"));
                //tmphtml.Add("../" + tmplist[i].Substring(tmplist[i].LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")), tmplist[i].Length - tmplist[i].LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir"))));
            }
            return tmphtml;
        }

        /// <summary>
        /// 增加一个模板
        /// </summary>
        /// <param name="jsonentity"></param>
        /// <returns></returns>
        public BaseResultDataValue AddReportModelFile()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {

                brdv.ErrorInfo = "保存成功！";
                brdv.ResultDataValue = "{id:123}";
                brdv.success = true;
                // string pfno = "";
                //DataSet ds= ibPrintFormat.GetList(jsonentity);
                //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                //{
                //pfno = ds.Tables[0].Rows[0]["id"].ToString() ;
                string tmpfilename = "";
                string aaa = HttpContext.Current.Request.Form["aaa"];
                HttpPostedFile fileaaa = HttpContext.Current.Request.Files["FileUpload1"];
                HttpPostedFile file = HttpContext.Current.Request.Files["FileUpload2"];
                int len = file.ContentLength;
                if (len > 0 && !string.IsNullOrEmpty(file.FileName))
                {
                    tmpfilename = file.FileName;

                    ZhiFang.Common.Log.Log.Info("模版文件名tmpfilename：" + tmpfilename);
                    string fileclass = System.IO.Path.GetExtension(tmpfilename).ToUpper().Trim();
                    if (!string.IsNullOrEmpty(tmpfilename) && (fileclass == ".XSL" || fileclass == ".XSLT" || fileclass == ".FRX" || fileclass == ".FR3"))
                    {
                        string savepath = System.AppDomain.CurrentDomain.BaseDirectory + ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelURL") + "\\" + System.IO.Path.GetFileNameWithoutExtension(tmpfilename) + "\\" + Guid.NewGuid().ToString() + "\\";
                        if (ZhiFang.Common.Public.FilesHelper.CheckAndCreatDir(savepath))
                        {
                            string filepath = System.IO.Path.Combine(savepath, tmpfilename);
                            file.SaveAs(filepath);
                            ZhiFang.Common.Log.Log.Info("filepath:" + filepath);
                            ZhiFang.Common.Log.Log.Info("filename:" + tmpfilename);
                        }

                        //MemoryStream tempMemoryStream = new MemoryStream(tempBuf);
                        //System.Drawing.Image tempImge = System.Drawing.Image.FromStream(tempMemoryStream);
                        //tempImge.Save(tempPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        ;
                    }
                }

            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error(ex.ToString());
                brdv.ErrorInfo = "保存失败！" + ex.ToString();
                brdv.success = false;
            }
            return brdv;
        }

        /// <summary>
        /// 记录打印次数
        /// </summary>
        /// <param name="ReportFormID"></param>
        /// <returns></returns>
        public BaseResult UpdatePrintTimeByReportFormID(string ReportFormID)
        {
            BaseResult br = new BaseResult();
            try
            {
                IBReportFormFull ibrff = BLLFactory<IBReportFormFull>.GetBLL("ReportFormFull");
                br.success = ibrff.UpdatePrintTimesByReportFormID(ReportFormID);
            }
            catch (Exception e)
            {

                br.success = false;
                br.ErrorInfo = e.ToString();
            }
            return br;
        }

        /// <summary>
        /// 报告下载
        /// </summary>
        /// <param name="reportformId">报告ID，根据分号隔开</param>
        /// <param name="reportformtitle">抬头</param>
        /// <param name="filePath">客户端存放路径</param>
        /// <returns></returns>
        public BaseResultDataValue DownLoadReport(Model.DownloadReportParam jsonentity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                ZhiFang.Common.Log.Log.Debug("DownLoadReport.1" + jsonentity.reportformtitle + "@" + jsonentity.reportformIds);               
                string reportformtitle = jsonentity.reportformtitle;
                string reportformIds = jsonentity.reportformIds;
                string applocalroot = System.AppDomain.CurrentDomain.BaseDirectory;
                string guidstring = Guid.NewGuid().ToString();
                string tmppath = applocalroot + "TmpReportImagePath\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\";
                string zipfilepath = tmppath + guidstring + "\\";
                ZhiFang.Common.Log.Log.Debug("DownLoadReport.2.tmppath" + tmppath + "@zipfilepath" + zipfilepath);
                ZhiFang.Common.Log.Log.Debug("DownLoadReport.3.aaa" + applocalroot + "TmpReportImagePath\\" + DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + "\\");
                //if (Directory.Exists(applocalroot + "TmpReportImagePath\\" + DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + "\\"))
                //    Directory.Delete(applocalroot + "TmpReportImagePath\\" + DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + "\\", true);

                ZhiFang.Common.Public.FilesHelper.CheckAndCreatDir(tmppath);
                ZhiFang.Common.Public.FilesHelper.CheckAndCreatDir(zipfilepath);
                ZhiFang.Common.Log.Log.Debug("DownLoadReport.4.tmppath" + tmppath + "@zipfilepath" + zipfilepath);

                List<string> ReportFormContextList = new List<string>();
                string title = "0";
                switch (reportformtitle.ToUpper())
                {
                    case "CENTER": title = "0"; break;
                    case "CLIENT": title = "1"; break;
                    case "BATCH": title = "2"; break;
                    case "MENZHEN": title = "3"; break;
                    case "ZHUYUAN": title = "4"; break;
                    case "TIJIAN": title = "5"; break;
                    default: title = "0"; break;
                }
                foreach (string reportformId in reportformIds.Split(';'))
                {
                    if (reportformId.Trim() == "")
                    {
                        continue;
                    }
                    List<string> ReportFormContextListTemp = new List<string>();
                    ZhiFang.BLL.Report.ReportFormFull rffb = new ZhiFang.BLL.Report.ReportFormFull();
                    Model.ReportFormFull rff_m = new Model.ReportFormFull();
                    rff_m.ReportFormID = reportformId; //"_138723_25_1_9_2011-04-21 00:00:00";
                    ZhiFang.Common.Log.Log.Info("reportformId=" + reportformId);
                    DataSet dsrf = rffb.GetList(rff_m);
                    if (dsrf != null && dsrf.Tables[0].Rows.Count > 0)
                    {
                        ReportFormContextListTemp = PrintReportFormCommon.FindReportFormFiles(dsrf.Tables[0].Rows[0], (ZhiFang.Common.Dictionary.ReportFormTitle)Convert.ToInt32(title), title);
                    }

                    //多个报告打包到Zip
                    foreach (string reportformContext in ReportFormContextListTemp)
                    {
                        string newfilename = "";
                        if (ZhiFang.Common.Public.FilesHelper.CheckAndCreatDir(tmppath))
                        {
                            newfilename = zipfilepath + dsrf.Tables[0].Rows[0]["CNAME"].ToString() + "-" + dsrf.Tables[0].Rows[0]["FORMNO"].ToString() + "-" + dsrf.Tables[0].Rows[0]["SERIALNO"].ToString() + ".PDF";
                            ZhiFang.Common.Log.Log.Debug("DownLoadReport.newfilename=" + newfilename);

                            File.Copy(applocalroot + reportformContext, newfilename, true);
                            ReportFormContextList.Add(newfilename);
                        }
                    }

                    //更新下载状态
                    rffb.UpdateDownLoadState(reportformId);
                }
                string Pdfurl = "";

                foreach (string reportform in ReportFormContextList)
                {
                    if (Pdfurl == "")
                    {
                        Pdfurl = reportform;
                    }
                    else
                    {
                        Pdfurl += ";" + reportform;
                    }
                }
                #region 压缩报告文件


                ///文件压缩以后的地址
                string ZipFileUrl = "";//".zip";

                //if (Pdfurl != "" && Pdfurl.Contains(';'))
                //{
                //    FileInfo fileinfo = new FileInfo(Pdfurl.Split(';')[0]);

                //    //Pdfurl.Split(';')[0].Replace(fileinfo.Extension, ".zip");
                //}
                //else
                //{
                //    FileInfo fileinfo = new FileInfo(Pdfurl);
                //    ZipFileUrl = Pdfurl.Replace(fileinfo.Extension, ".zip");
                //}
                ZhiFang.Common.Log.Log.Debug("报告pdf文件打包下载，目录：" + zipfilepath + "下有" + Directory.GetFiles(zipfilepath, "*.pdf").Length + "份pdf文件。");
                ZipFileUrl = tmppath + guidstring + ".zip";
                ZhiFang.BLL.Common.ZipHelp.CreateZipFile(zipfilepath, ZipFileUrl, true);
                #endregion

                ZhiFang.Common.Log.Log.Info("DownLoadReport.压缩文件路径:" + ZipFileUrl);
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(ZipFileUrl.Replace(applocalroot, ""));
                brdv.success = true;

            }
            catch (Exception ex)
            {
                brdv.success = false;
                ZhiFang.Common.Log.Log.Info("DownLoadReport.压缩错误信息:" + ex.ToString());
            }
            return brdv;

        }

    }
}
