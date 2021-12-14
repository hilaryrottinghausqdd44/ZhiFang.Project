using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using ZhiFang.IBLL.Report;
using ZhiFang.BLLFactory;
using ZhiFang.Model;
using ZhiFang.Model.UiModel;
using System.Data;
//using ZhiFang.BLL.Report.Print;
using Common;
//using FastReport.Export.Mht;
//using FastReport;
using System.IO;
using ZhiFang.BLL.Common;
using FastReport;
using FastReport.Export.Mht;
using ZhiFang.IBLL.Common.BaseDictionary;
using Newtonsoft.Json;
using System.Web;


namespace ZhiFang.WebLis.ServiceWCF
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ReportFromService : ServerContract.IReportFromService
    {
        private readonly IBNRequestForm rfb = BLLFactory<IBNRequestForm>.GetBLL("NRequestForm");
        private readonly IBReportFormFull ibrff = BLLFactory<IBReportFormFull>.GetBLL("ReportFormFull");
        Tools.ListTool LT = new Tools.ListTool();
        private readonly IBShowFrom showform = BLLFactory<IBShowFrom>.GetBLL("ShowFrom");
        private readonly IBShowReport showReport = BLLFactory<IBShowReport>.GetBLL("ShowReport");
        IBLL.Common.BaseDictionary.IBBusinessLogicClientControl blcc = BLLFactory<IBBusinessLogicClientControl>.GetBLL();
        /// <summary>
        /// 获取报告查询列表
        /// </summary>
        /// <param name="reportformfull"></param>
        /// <param name="wherestr"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public BaseResultDataValue SelectReportList(string wherestr, int page, int rows, string sort, string order)
        {

            BaseResultDataValue brdv = new BaseResultDataValue();
            //EntityListEasyUI<UiReportForm_N> easyui = new EntityListEasyUI<UiReportForm_N>();
            EntityListEasyUI<Model.ReportFormFull> easyui = new EntityListEasyUI<Model.ReportFormFull>();
            try
            {
                #region 验证
                if (wherestr == null)
                    return null;
                wherestr = ZhiFang.Tools.Validate.ReplaceWhereString(wherestr);
                if (wherestr == string.Empty)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "where串错误";
                    return brdv;
                }


                //默认五千条
                int ReportSelMaxNum = 5000;
                var max = ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportSelMaxNum").Trim();

                if (max != null || max != "")
                {
                    ReportSelMaxNum = Convert.ToInt32(max);
                }

                try
                {
                    if (ibrff.Count(wherestr) > ReportSelMaxNum)
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = "返回记录数大于配置的条数";
                        return brdv;
                    }
                }
                catch (Exception e)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = e.ToString();
                    Model.ReportFormFull model = new ReportFormFull();
                    //brdv = SelectReportList2(model, page.ToString(), rows.ToString());
                    return brdv;
                }
                #endregion
                string sqlsort = "";
                if (sort != null && sort.Trim() != "" && sort.Trim().ToLower() != "null")
                {
                    sqlsort = " order by " + sort.Replace(";", "").Replace("'", "");
                    if (order != null && order.Trim() != "" && order.Trim().ToLower() != "null")
                    {
                        sqlsort += " " + order.Replace(";", "").Replace("'", "");

                    }
                }
                else
                {
                    sqlsort = "  order by RECEIVEDATE desc ";
                }
                ZhiFang.Common.Log.Log.Debug("SelectReportList.wherestr+sqlsort=" + wherestr + sqlsort);
                DataSet ds = ibrff.GetList(wherestr + sqlsort);
                if (ds == null)
                {
                    return null;
                }
                DataTable tempDt = ds.Tables[0];
                //foreach (DataRow dr in tempDt.Rows)
                //{
                //    dr[
                //}
                //ZhiFang.Common.Log.Log.Error("222" + "+++" + reportFormFullList.Count.ToString());
                //List<UiReportForm_N> reportFormFullList =// LT.GetListColumns<UiReportForm_N>(tempDt);
                //ZhiFang.Common.Log.Log.Error("111" + "+++" + tempDt.Rows.Count.ToString());
                List<Model.ReportFormFull> reportFormFullList = ibrff.DataTableToList(tempDt);
                //if (reportFormFullList.Count == 0)
                //    return null;
                //ZhiFang.Common.Log.Log.Error("222" + "+++" + reportFormFullList.Count.ToString());
                List<Model.ReportFormFull> Result = LT.Pagination<Model.ReportFormFull>(page, rows, reportFormFullList);
                //ZhiFang.Common.Log.Log.Error("333" + "+++" + Result.Count.ToString());
                easyui.rows = Result;
                easyui.total = reportFormFullList.Count;

                brdv.success = true;
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(easyui);

                //结果查看和报告打印预览后进行打印次数增加
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("ExportResultAddPrintCountFlag") == "1")
                {
                    IBReportFormFull ibrff = BLLFactory<IBReportFormFull>.GetBLL("ReportFormFull");
                    for (int i = 0; i < tempDt.Rows.Count; i++)
                    {
                        ibrff.UpdatePrintTimesByReportFormID(tempDt.Rows[i]["ReportFormID"].ToString().Trim());
                    }
                }
            }
            catch (Exception)
            {

            }
            return brdv;
        }

        public BaseResultDataValue SelectReportList2(string Startdate, string Enddate, string CLIENTNO, string SECTIONNO, string CNAME, string GENDERNAME, string SAMPLENO, string PATNO, string SICKTYPENO, string statues, string ZDY10, string PERSONID, string LIKESEARCH, string serialno, string clientcode, string collectStartdate, string collectEnddate, string noperdateStart, string noperdateEnd, string checkdateStart, string checkdateEnd, int page, int rows, string sort, string order, string abnormalstatues)
        {
            return SelectReportListByPara(Startdate, Enddate, CLIENTNO, SECTIONNO, CNAME, GENDERNAME, SAMPLENO, PATNO, SICKTYPENO, statues, ZDY10, PERSONID, LIKESEARCH, serialno, clientcode, collectStartdate, collectEnddate, noperdateStart, noperdateEnd, checkdateStart, checkdateEnd, page, rows, sort, order, abnormalstatues);
        }
        /// <summary>
        /// 获取报告查询列表
        /// </summary>
        /// <param name="reportformfull"></param>
        /// <param name="model"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public BaseResultDataValue SelectReportListByPara(string Startdate, string Enddate, string CLIENTNO, string SECTIONNO, string CNAME, string GENDERNAME, string SAMPLENO, string PATNO, string SICKTYPENO, string statues, string ZDY10, string PERSONID, string LIKESEARCH, string serialno, string clientcode, string collectStartdate, string collectEnddate, string noperdateStart, string noperdateEnd, string checkdateStart, string checkdateEnd, int page, int rows, string sort, string order, string abnormalstatues)
        {
            //ZhiFang.Common.Log.Log.Info("aaaaaaaaa!W!");
            if (CLIENTNO == null)
            {
                List<Model.CLIENTELE> lc = new List<Model.CLIENTELE>();
                try
                {
                    ZhiFang.WebLis.Class.User user = new ZhiFang.WebLis.Class.User();
                    if (ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser") == null || ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser").Trim() == "")
                    {
                        string alertStr = "未登录，请登陆后继续！";
                        ZhiFang.Common.Log.Log.Info(alertStr);
                        return null;
                    }
                    else
                    {
                        string UserId = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser");
                        user = new ZhiFang.WebLis.Class.User(UserId);
                    }

                    string fields = "CLIENTELE.CNAME,CLIENTELE.ClIENTNO";
                    string where = "";
                    EntityList<Model.CLIENTELE> cl = blcc.GetBusinessLogicClientList(new Model.BusinessLogicClientControl { Account = user.Account.Trim(), SelectedFlag = true, Flag = 0 }, 1, 5000, fields, where, "1");

                    #region 增加默认权限
                    string deptCode = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangdeptCode");
                    IBLL.Common.BaseDictionary.IBCLIENTELE ibctl = BLLFactory<ZhiFang.IBLL.Common.BaseDictionary.IBCLIENTELE>.GetBLL();
                    if (deptCode != "" && deptCode != null)
                    {
                        Model.CLIENTELE model = ibctl.GetModel(long.Parse(deptCode));

                        if (model != null && cl.list != null)
                        {
                            cl.list.Add(model);
                        }
                    }
                    #endregion
                    //brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(cl) ;
                    lc = cl.list.ToList();
                    foreach (var c in lc)
                    {
                        if (CLIENTNO == null)
                        {
                            CLIENTNO = c.ClIENTNO;
                        }
                        else
                        {
                            CLIENTNO += "," + c.ClIENTNO;
                        }
                    }
                }
                catch (Exception e)
                {
                    ZhiFang.Common.Log.Log.Error(e.ToString());
                    return null;
                }
            }
            Model.ReportFormFull modelSearch = ConvertToReportFormFull(Startdate, Enddate, CLIENTNO, SECTIONNO, CNAME, GENDERNAME, SAMPLENO, PATNO, SICKTYPENO, statues, ZDY10, PERSONID, LIKESEARCH, serialno, clientcode, collectStartdate, collectEnddate, noperdateStart, noperdateEnd, checkdateStart, checkdateEnd);
            if (abnormalstatues != null && abnormalstatues.Trim() != "")
            {
                modelSearch.ResultStatus = int.Parse(abnormalstatues);
            }
            BaseResultDataValue brdv = new BaseResultDataValue();
            //EntityListEasyUI<UiReportForm_N> easyui = new EntityListEasyUI<UiReportForm_N>();
            EntityListEasyUI<Model.ReportFormFull> easyui = new EntityListEasyUI<Model.ReportFormFull>();
            try
            {
                #region 验证
                if (Startdate == null && Enddate == null && collectStartdate == null && collectEnddate == null && noperdateStart == null && noperdateEnd == null && checkdateStart == null && checkdateEnd == null)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "查询日期不能为空";
                    return brdv;
                }
                //默认五千条
                int ReportSelMaxNum = 5000;
                if (ConfigHelper.GetConfigInt("ReportSelMaxNum").HasValue)
                {
                    ReportSelMaxNum = (int)ConfigHelper.GetConfigInt("ReportSelMaxNum");
                }
                try
                {
                    //model.Startdate = Convert.ToDateTime("2014-12-1");
                    //model.Enddate = Convert.ToDateTime("2015-2-1");
                    int count = ibrff.GetTotalCount(modelSearch);
                    ZhiFang.Common.Log.Log.Debug("SelectReportListByPara:count=" + count + "@ReportSelMaxNum=" + ReportSelMaxNum);
                    if (count > ReportSelMaxNum)
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = "返回记录数大于配置的条数";
                        return brdv;
                    }
                }
                catch (Exception e)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = e.ToString();
                    return brdv;
                }
                #endregion
                string sqlsort = "";
                if (sort != null && sort.Trim() != "" && sort.Trim().ToLower() != "null")
                {
                    sqlsort = " " + sort.Replace(";", "").Replace("'", "");
                    if (order != null && order.Trim() != "" && order.Trim().ToLower() != "null")
                    {
                        sqlsort += " " + order.Replace(";", "").Replace("'", "");

                    }
                }
                else
                {
                    sqlsort = "  RECEIVEDATE desc ";
                }
                ZhiFang.Common.Log.Log.Debug("SelectReportListByPara.sqlsort=" + sqlsort);
                DataSet ds = ibrff.GetList(ReportSelMaxNum, modelSearch, sqlsort);
                if (ds == null)
                {
                    return null;
                }
                DataTable tempDt = ds.Tables[0];
                //foreach (DataRow dr in tempDt.Rows)
                //{
                //    dr[
                //}
                //ZhiFang.Common.Log.Log.Error("222" + "+++" + reportFormFullList.Count.ToString());
                //List<UiReportForm_N> reportFormFullList =// LT.GetListColumns<UiReportForm_N>(tempDt);
                //ZhiFang.Common.Log.Log.Error("111" + "+++" + tempDt.Rows.Count.ToString());
                List<Model.ReportFormFull> reportFormFullList = ibrff.DataTableToList(tempDt);
                //if (reportFormFullList.Count == 0)
                //    return null;
                //ZhiFang.Common.Log.Log.Error("222" + "+++" + reportFormFullList.Count.ToString());

                List<Model.ReportFormFull> Result = LT.Pagination<Model.ReportFormFull>(page, rows, reportFormFullList);
                //ZhiFang.Common.Log.Log.Error("333" + "+++" + Result.Count.ToString());
                easyui.rows = Result;
                easyui.total = reportFormFullList.Count;

                brdv.success = true;
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(easyui);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("SelectReportListByPara异常：" + e.ToString());
                brdv.ErrorInfo = e.ToString();
                //throw;
            }
            return brdv;
        }

        /// <summary>
        /// 佛山报告查询列表  ganwh add 2015-5-11
        /// </summary> 
        /// <param name="Startdate"></param>
        /// <param name="Enddate"></param>
        /// <param name="CLIENTNO"></param>
        /// <param name="SECTIONNO"></param>
        /// <param name="CNAME"></param>
        /// <param name="GENDERNAME"></param>
        /// <param name="SAMPLENO"></param>
        /// <param name="PATNO"></param>
        /// <param name="statues"></param>
        /// <param name="ZDY10"></param>
        /// <param name="PERSONID"></param>
        /// <param name="LIKESEARCH"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public BaseResultDataValue SelectReportListFoshan(string Startdate, string Enddate, string CLIENTNO, string SECTIONNO, string CNAME, string GENDERNAME, string SAMPLENO, string PATNO, string statues, string ZDY10, string PERSONID, string LIKESEARCH, int page, int rows)
        {
            Model.ReportFormFull model1 = ConvertToReportFormFull(Startdate, Enddate, CLIENTNO, SECTIONNO, CNAME, GENDERNAME, SAMPLENO, PATNO, null, statues, ZDY10, PERSONID, LIKESEARCH, null, null, null, null, null, null, null, null);
            //Model.ReportFormFull model = new ReportFormFull();
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (Startdate == null && Enddate == null)
            {
                brdv.success = false;
                return brdv;
            }

            //EntityListEasyUI<UiReportForm_N> easyui = new EntityListEasyUI<UiReportForm_N>();
            EntityListEasyUI<Model.ReportFormFull> easyui = new EntityListEasyUI<Model.ReportFormFull>();
            try
            {
                #region 验证
                //默认五千条
                int ReportSelMaxNum = 5000;
                if (ConfigHelper.GetConfigBool("ReportSelMaxNum"))
                {
                    ReportSelMaxNum = (int)ConfigHelper.GetConfigInt("ReportSelMaxNum");
                }

                try
                {
                    if (ibrff.GetTotalCount(model1) > ReportSelMaxNum)
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = "返回记录数大于配置的条数";
                        return brdv;
                    }
                }
                catch (Exception e)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = e.ToString();
                    return brdv;
                }
                #endregion
                DataSet ds = ibrff.GetList(model1, "foshan");
                if (ds == null)
                {
                    return null;
                }
                DataTable tempDt = ds.Tables[0];

                List<Model.ReportFormFull> reportFormFullList = ibrff.DataTableToList(tempDt, "foshan");

                List<Model.ReportFormFull> Result = LT.Pagination<Model.ReportFormFull>(page, rows, reportFormFullList);
                easyui.rows = Result;
                easyui.total = reportFormFullList.Count;

                brdv.success = true;
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(easyui);
            }
            catch (Exception)
            {

                throw;
            }
            return brdv;
        }

        private Model.ReportFormFull ConvertToReportFormFull(string Startdate, string Enddate, string CLIENTNO, string SECTIONNO, string CNAME, string GENDERNAME, string SAMPLENO, string PATNO, string SICKTYPENO, string PRINTTIMES, string ZDY10, string PERSONID, string LIKESEARCH, string serialno, string clientcode, string collectStartdate, string collectEnddate, string noperdateStart, string noperdateEnd, string checkdateStart, string checkdateEnd)
        {
            ZhiFang.Common.Log.Log.Debug("noperdateStart:" + noperdateStart + ",noperdateEnd:" + noperdateEnd);
            Model.ReportFormFull tempModel = new ReportFormFull();

            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("LIKESEARCH") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("LIKESEARCH").Trim() != "")
            {
                tempModel.LIKESEARCH = ZhiFang.Common.Public.ConfigHelper.GetConfigString("LIKESEARCH").Trim();
            }
            if (Startdate != null)
                tempModel.Startdate = DateTime.Parse(Startdate);
            if (Enddate != null)
                tempModel.Enddate = DateTime.Parse(Enddate);
            if (collectStartdate != null)
                tempModel.collectStartdate = DateTime.Parse(collectStartdate);
            if (collectEnddate != null)
                tempModel.collectEnddate = DateTime.Parse(collectEnddate);
            if (noperdateStart != null)
                tempModel.noperdateStart = DateTime.Parse(noperdateStart);
            if (noperdateEnd != null)
                tempModel.noperdateEnd = DateTime.Parse(noperdateEnd);
            if (checkdateStart != null)
                tempModel.CheckStartDate = DateTime.Parse(checkdateStart);
            if (checkdateEnd != null)
                tempModel.CheckEndDate = DateTime.Parse(checkdateEnd);
            if (CLIENTNO == null || CLIENTNO.Contains(",") == false)
                tempModel.CLIENTNO = CLIENTNO;
            else
                tempModel.ClientList = CLIENTNO;
            if (SECTIONNO != null)
                tempModel.SECTIONNO = SECTIONNO;
            if (CNAME != null)
            {
                tempModel.CNAME = CNAME;
            }
            if (GENDERNAME != null)
                tempModel.GENDERNAME = GENDERNAME;
            if (SAMPLENO != null)
                tempModel.SAMPLENO = SAMPLENO;
            if (PATNO != null)
                tempModel.PATNO = PATNO;
            if (PRINTTIMES != null)
                tempModel.PRINTTIMES = int.Parse(PRINTTIMES);
            if (ZDY10 != null)
                tempModel.ZDY10 = ZDY10;
            if (PERSONID != null)
                tempModel.PERSONID = PERSONID;
            if (LIKESEARCH != null)
                tempModel.LIKESEARCH = LIKESEARCH;
            if (serialno != null)
                tempModel.serialno = serialno;
            if (clientcode != null)
                tempModel.clientcode = clientcode;
            if (SICKTYPENO != null && SICKTYPENO.Trim() != "")
                tempModel.SICKTYPENO = SICKTYPENO;

            return tempModel;
        }

        /// <summary>
        /// 根据报告ID，浏览结果
        /// </summary>
        /// <param name="reportformId"></param>
        /// <param name="sectionNo"></param>
        /// <param name="sectionType"></param>      
        /// <returns></returns>
        public BaseResultDataValue GetPreviewReportResultById(string reportformId, int sectionNo, int sectionType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            try
            {
                #region 验证
                if (reportformId == null || reportformId == "")
                    return null;
                if (sectionNo == 0 || sectionType == 0)
                    return null;

                #endregion

                #region 赋值

                string urlPageName = "TechnicianPrint1.aspx";
                string urlShowType = "0";
                string result = string.Empty;
                int st = ZhiFang.Common.Public.Valid.ToInt(urlShowType);
                int sn = ZhiFang.Common.Public.Valid.ToInt(sectionNo.ToString());
                #endregion
                DataTable dtrf = new DataTable();
                DataTable dtri = new DataTable();
                dtri = showReport.GetReportFormAndItemData(reportformId, sectionType, out dtrf);

                //string modelPath = @"D:\源代码\1-产品目录\92-检验之星8\WebLis\WebLis\XSL\Show\NormalIncImage.XSLT";
                string modelPath = showReport.GetTemplatePath(dtrf, dtri, urlPageName, int.Parse(urlShowType), sectionType);
                result = showReport.GetResult(dtrf, dtri, modelPath);
                brdv.success = true;
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(result);
                //结果查看和报告打印预览后进行打印次数增加
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("ExportResultAddPrintCountFlag") =="1")
                {
                    IBReportFormFull ibrff = BLLFactory<IBReportFormFull>.GetBLL("ReportFormFull");
                    ibrff.UpdatePrintTimesByReportFormID(reportformId);
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("GetPreviewReportResultById.异常：" + e.ToString());
                brdv.ErrorInfo = "GetPreviewReportResultById.异常：" + e.ToString();
                brdv.success = false;
            }
            return brdv;
        }

        public BaseResultDataValue GetPreviewReportImageById(string reportformId)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            try
            {
                string imagepath = "";
                List<string> imagepathList = new List<string>();
                //DateTime receivedate;
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportIncludeImage") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportIncludeImage").Trim() != "")
                {
                    Model.ReportFormFull reportFormFull = ibrff.GetModel(reportformId);
                    DateTime datetime = DateTime.Now;
                    if (reportFormFull != null)
                        datetime = (DateTime)reportFormFull.RECEIVEDATE;
                    string date = "";
                    date = datetime.ToString("yyyy-MM-dd");
                    string[] ArrayDate = date.Split('-');
                    string linshiFormNo = "";

                    try
                    {
                        if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("LinSFormNo") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("LinSFormNo").Trim() != "" && ZhiFang.Common.Public.ConfigHelper.GetConfigString("LinSFormNo").Trim() == "1")
                        {//TestTypeNo   检测类型编号
                            //SampleNo	样本号
                            linshiFormNo = datetime.Year + "-" + ArrayDate[1] + "-" + ArrayDate[2] + ";" + reportFormFull.SECTIONNO + ";" +
                               reportFormFull.TESTTYPENO + ";" + reportFormFull.SAMPLENO;
                        }
                        else
                        {
                            linshiFormNo = reportFormFull.FormNo2.ToString();
                        }
                    }
                    catch (Exception)
                    {
                        linshiFormNo = reportFormFull.FormNo2.ToString();
                    }
                    string path = ConfigHelper.GetConfigString("ReportIncludeImage").Trim() + "\\" + Convert.ToDateTime(datetime.ToString("yyyy-MM-dd")).Year + "\\" + Convert.ToDateTime(datetime.ToString("yyyy-MM-dd")).Month + "\\" + Convert.ToDateTime(datetime.ToString("yyyy-MM-dd")).Day + "\\" + linshiFormNo + "\\";
                    string[] ReportIncludeImage = ConfigHelper.GetConfigString("ReportIncludeImage").Trim().Split('\\');
                    string path2 = ReportIncludeImage[ReportIncludeImage.Length - 1] + "\\" + Convert.ToDateTime(datetime.ToString("yyyy-MM-dd")).Year + "\\" + Convert.ToDateTime(datetime.ToString("yyyy-MM-dd")).Month + "\\" + Convert.ToDateTime(datetime.ToString("yyyy-MM-dd")).Day + "\\" + linshiFormNo + "\\";
                    ZhiFang.Common.Log.Log.Info(path);
                    if (System.IO.Directory.Exists(path))
                    {
                        string[] files = Directory.GetFiles(path);
                        List<string> copyFileNameList = new List<string>();

                        #region 复制图片,重命名
                        foreach (string file in files)
                        {
                            if (file.IndexOf("NameImage") < 0 && file.IndexOf("COPY") > 0)
                            {
                                string[] fileName = file.Split('\\');
                                copyFileNameList.Add(fileName[fileName.Length - 1]);
                            }
                        }
                        if (copyFileNameList.Count == 0)
                        {
                            for (int i = 0; i < files.Count(); i++)
                            {
                                string[] fileName = files[i].ToString().Split('\\');
                                string[] a = files[i].Split('@');
                                if (a.Length > 1)
                                {
                                    if (a[3] == "RFGraphData")//结果图片
                                    {
                                        File.Copy(Path.Combine(path, fileName[fileName.Length - 1]), Path.Combine(path, "COPY" + i.ToString() + ".jpg"), false);

                                        imagepathList.Add((path2 + "COPY" + i.ToString() + ".jpg"));
                                    }
                                }
                                //if (files[i].ToString().IndexOf("COPY") == -1)
                                //{
                                //    File.Copy(Path.Combine(path, fileName[fileName.Length - 1]), Path.Combine(path, "COPY" + i.ToString() + ".jpg"), false);

                                //    imagepathList.Add((path2 + "COPY" + i.ToString() + ".jpg"));
                                //}


                            }
                        }
                        else
                        {
                            foreach (string copyFile in copyFileNameList)
                            {
                                imagepathList.Add(path2 + copyFile);
                            }
                        }
                        #endregion


                    }
                    brdv.success = true;
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(imagepathList);

                }
                else
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "没有获取到配置";
                    brdv.ResultDataValue = "";
                }

            }
            catch (Exception e)
            {
                brdv.ErrorInfo = e.ToString();
                brdv.success = false;

            }
            return brdv;
        }

        /// <summary>
        /// 根据报告ID，浏览报告
        /// </summary>
        /// <param name="reportformId"></param>
        /// <param name="sectionNo"></param>
        /// <param name="sectionType"></param>
        /// <returns></returns>
        public BaseResultDataValue GetPreviewReportById(string reportformId, int sectionNo, int sectionType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                #region 验证
                if (reportformId == null || reportformId == "")
                    return null;
                if (sectionNo == 0 || sectionType == 0)
                    return null;

                #endregion

                #region 赋值

                string urlPageName = "TechnicianPrint1.aspx";
                string urlShowType = "0";
                string result = string.Empty;
                int st = ZhiFang.Common.Public.Valid.ToInt(urlShowType);
                int sn = ZhiFang.Common.Public.Valid.ToInt(sectionNo.ToString());
                #endregion

                DataTable dtrf = new DataTable();
                DataTable dtri = new DataTable();
                dtri = showReport.GetReportFormAndItemData(reportformId, sectionType, out dtrf);

                string modelpath = showReport.GetTemplatePath(dtrf, dtri, urlPageName, int.Parse(urlShowType), sectionType);
                result = showReport.GetResult(dtrf, dtri, modelpath);
                brdv.success = true;
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(result);
            }
            catch (Exception)
            {

                throw;
            }
            return brdv;
        }

        public BaseResultDataValue testAAA(int page, int rows)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = true;


            brdv.ResultDataValue = "测试";
            return brdv;
        }

        #region 报告导出
        #region 获取报告地址
        public BaseResultDataValue GetReportItem(VIEW_ReportItemFull jsonentity)
        {
            ZhiFang.Common.Log.Log.Info("调用服务成功！");
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                ZhiFang.Common.Log.Log.Info("执行语句1");
                IBView_ReportItemFull ibvtif = BLLFactory<IBView_ReportItemFull>.GetBLL("BView_ReportItemFull");
                ZhiFang.Common.Log.Log.Info("执行语句2");
                FastReport.Report report = new FastReport.Report();
                ZhiFang.Common.Log.Log.Info("执行语句3");
                string strTable = "";
                DataSet ds = new DataSet();
                ZhiFang.Common.Log.Log.Info("开始获取报告地址");
                ds = ibvtif.GetViewItemFull(jsonentity);
                //返回数据列表或是生成html,返回文件地址
                if (jsonentity.TYPE == "reportjsonlist")
                {
                    brdv.ResultDataFormatType = "json";
                    brdv.ResultDataValue = DataSetToJson.ToJson(ds.Tables[0]);
                    BaseResultDataSet brds = new BaseResultDataSet();
                    brds.total = ds.Tables[0].Rows.Count;
                    brds.rows = ds.Tables[0];
                    brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(brds);

                }
                else if (jsonentity.TYPE == "reporthtmlurl")
                {
                    if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ItemShowExcel")))
                        Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ItemShowExcel"));
                    report.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ItemShowExcel") + "\\ReportItemFullShow.frx");
                    DataTable dt = ds.Tables[0];
                    dt.TableName = "Table";
                    GC.Collect();
                    report.RegisterData(dt.DataSet);
                    DataBand data = report.FindObject("Data1") as DataBand;
                    data.DataSource = report.GetDataSource("Table");
                    report.Prepare();
                    MHTExport export = new MHTExport();
                    FastReport.Export.Html.HTMLExport aa = new FastReport.Export.Html.HTMLExport();
                    aa.SinglePage = true;
                    aa.SubFolder = false;
                    aa.Navigator = false;
                    string url = DateTime.Now.ToString().Replace(" ", "_").Replace(":", "_").Replace("/", "_") + ".html";
                    report.Export(aa, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + url);
                    report.Dispose();
                    strTable = "/" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "//" + url;

                    string strjson = "" + strTable + "#" + DownLoadReportExcel(ds) + "";

                    //ViewState["fielPath"] = strTable;
                    brdv.success = true;
                    brdv.ResultDataValue = strjson;
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("获取报告地址出错:" + ex.ToString() + "@@@@@@@@@@" + ex.StackTrace);
                brdv.success = false;
                brdv.ErrorInfo = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ex.ToString() + "@@@@@@@@@@" + ex.StackTrace;
                ZhiFang.Common.Log.Log.Debug(brdv.ErrorInfo);
            }
            ZhiFang.Common.Log.Log.Info("获取报告地址结束");
            return brdv;
        }
        #endregion

        #region 报告下载
        public string DownLoadReportExcel(DataSet ds)
        {
            // IBView_ReportItemFull ibvtif = BLLFactory<IBView_ReportItemFull>.GetBLL("BView_ReportItemFull");
            BaseResultDataValue brdv = new BaseResultDataValue();
            FastReport.Report report = new FastReport.Report();
            string strTable = "";
            //DataSet ds = new DataSet();
            try
            {
                //ds = ibvtif.GetViewItemFull(jsonentity);
                string url = DateTime.Now.ToString().Replace(" ", "_").Replace(":", "_").Replace("/", "_") + ".xls";
                string strPath = System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\" + url;
                if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel")))
                    Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel"));

                report.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ItemShowExcel") + "\\ReportItemFullForExcel.frx");
                DataTable dt = ds.Tables[0];//ExcelFile
                dt.TableName = "Table";
                GC.Collect();
                report.RegisterData(dt.DataSet);
                DataBand data = report.FindObject("Data1") as DataBand;
                data.DataSource = report.GetDataSource("Table");
                report.Prepare();
                FastReport.Export.Xml.XMLExport export = new FastReport.Export.Xml.XMLExport();
                report.Export(export, strPath);
                report.Dispose();
                strTable = "/" + ConfigHelper.GetConfigString("SaveExcel") + "/" + url;
                brdv.success = true;
                brdv.ResultDataValue = strTable;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = DateTime.Now.ToString("YYYYMMDD HH:mm:ss") + ex.ToString();
                ZhiFang.Common.Log.Log.Debug(brdv.ErrorInfo);
            }
            return strTable;
        }
        #endregion
        #region 报告下载
        public BaseResultDataValue DownLoadReportExcel(VIEW_ReportItemFull jsonentity)
        {
            IBView_ReportItemFull ibvtif = BLLFactory<IBView_ReportItemFull>.GetBLL("BView_ReportItemFull");
            BaseResultDataValue brdv = new BaseResultDataValue();
            FastReport.Report report = new FastReport.Report();
            string strTable = "";
            DataSet ds = new DataSet();
            try
            {
                ds = ibvtif.GetViewItemFull(jsonentity);
                string url = DateTime.Now.ToString().Replace(" ", "_").Replace(":", "_").Replace("/", "_") + ".xls";
                string strPath = System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\" + url;
                if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel")))
                    Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel"));

                report.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ItemShowExcel") + "\\ReportItemFullForExcel.frx");
                DataTable dt = ds.Tables[0];//ExcelFile
                dt.TableName = "Table";
                report.RegisterData(ds);
                DataBand data = report.FindObject("Data1") as DataBand;
                data.DataSource = report.GetDataSource("Table");
                report.Prepare();
                FastReport.Export.Xml.XMLExport export = new FastReport.Export.Xml.XMLExport();
                report.Export(export, strPath);
                report.Dispose();
                strTable = "/" + ConfigHelper.GetConfigString("SaveExcel") + "/" + url;
                brdv.success = true;
                brdv.ResultDataValue = strTable;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = DateTime.Now.ToString("YYYYMMDD HH:mm:ss") + ex.ToString();
                ZhiFang.Common.Log.Log.Debug(brdv.ErrorInfo);
            }
            return brdv;
        }

        /// <summary>
        /// 获取报告查询列表导出Excel
        /// </summary>
        /// <param name="reportformfull"></param>
        /// <param name="model"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public BaseResultDataValue DownloadReportExcelByPara(string Startdate, string Enddate, string CLIENTNO, string SECTIONNO, string CNAME, string GENDERNAME, string SAMPLENO, string PATNO, string SICKTYPENO, string statues, string ZDY10, string PERSONID, string LIKESEARCH, string serialno, string clientcode, string collectStartdate, string collectEnddate, string noperdateStart, string noperdateEnd, string checkdateStart, string checkdateEnd, int page, int rows, string sort, string order, string abnormalstatues)
        {
            //ZhiFang.Common.Log.Log.Info("aaaaaaaaa!W!");
            if (CLIENTNO == null)
            {
                List<Model.CLIENTELE> lc = new List<Model.CLIENTELE>();
                try
                {
                    ZhiFang.WebLis.Class.User user = new ZhiFang.WebLis.Class.User();
                    if (ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser") == null || ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser").Trim() == "")
                    {
                        string alertStr = "未登录，请登陆后继续！";
                        ZhiFang.Common.Log.Log.Info(alertStr);
                        return null;
                    }
                    else
                    {
                        string UserId = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser");
                        user = new ZhiFang.WebLis.Class.User(UserId);
                    }

                    string fields = "CLIENTELE.CNAME,CLIENTELE.ClIENTNO";
                    string where = "";
                    EntityList<Model.CLIENTELE> cl = blcc.GetBusinessLogicClientList(new Model.BusinessLogicClientControl { Account = user.Account.Trim(), SelectedFlag = true, Flag = 0 }, 1, 5000, fields, where, "1");

                    #region 增加默认权限
                    string deptCode = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangdeptCode");
                    IBLL.Common.BaseDictionary.IBCLIENTELE ibctl = BLLFactory<ZhiFang.IBLL.Common.BaseDictionary.IBCLIENTELE>.GetBLL();
                    if (deptCode != "" && deptCode != null)
                    {
                        Model.CLIENTELE model = ibctl.GetModel(long.Parse(deptCode));

                        if (model != null && cl.list != null)
                        {
                            cl.list.Add(model);
                        }
                    }
                    #endregion
                    //brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(cl) ;
                    lc = cl.list.ToList();
                    foreach (var c in lc)
                    {
                        if (CLIENTNO == null)
                        {
                            CLIENTNO = c.ClIENTNO;
                        }
                        else
                        {
                            CLIENTNO += "," + c.ClIENTNO;
                        }
                    }
                }
                catch (Exception e)
                {
                    ZhiFang.Common.Log.Log.Error(e.ToString());
                    return null;
                }
            }
            Model.ReportFormFull modelSearch = ConvertToReportFormFull(Startdate, Enddate, CLIENTNO, SECTIONNO, CNAME, GENDERNAME, SAMPLENO, PATNO, SICKTYPENO, statues, ZDY10, PERSONID, LIKESEARCH, serialno, clientcode, collectStartdate, collectEnddate, noperdateStart, noperdateEnd, checkdateStart, checkdateEnd);
            if (abnormalstatues != null && abnormalstatues.Trim() != "" && abnormalstatues.Trim() != "0")
            {
                modelSearch.ZDY5 = abnormalstatues;
            }
            BaseResultDataValue brdv = new BaseResultDataValue();
            //EntityListEasyUI<UiReportForm_N> easyui = new EntityListEasyUI<UiReportForm_N>();
            EntityListEasyUI<Model.ReportFormFull> easyui = new EntityListEasyUI<Model.ReportFormFull>();
            try
            {
                #region 验证
                if (Startdate == null && Enddate == null && collectStartdate == null && collectEnddate == null && noperdateStart == null && noperdateEnd == null && checkdateStart == null && checkdateEnd == null)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "查询日期不能为空";
                    return brdv;
                }

                #endregion
                string sqlsort = "";
                if (sort != null && sort.Trim() != "" && sort.Trim().ToLower() != "null")
                {
                    sqlsort = " " + sort.Replace(";", "").Replace("'", "");
                    if (order != null && order.Trim() != "" && order.Trim().ToLower() != "null")
                    {
                        sqlsort += " " + order.Replace(";", "").Replace("'", "");

                    }
                }
                else
                {
                    sqlsort = "  RECEIVEDATE desc ";
                }
                ZhiFang.Common.Log.Log.Debug("DownloadReportExceltByPara.sqlsort=" + sqlsort);
                DataSet ds = ibrff.GetList(10000, modelSearch, sqlsort);
                if (ds == null)
                {
                    return null;
                }
                FastReport.Report report = new FastReport.Report();
                //DataTable tempDt = ds.Tables[0];
                //tempDt.TableName = "ReportFormFull";

                string labcode = CLIENTNO != null ? CLIENTNO : "";
                string basepath = System.AppDomain.CurrentDomain.BaseDirectory;
                string path = "ResultForExcel" + "\\DownloadReportExcelByPara\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\" + DateTime.Now.Day + "\\";
                string filename = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + "@" + labcode + ".xls";//DateTime.Now.ToString().Replace(" ", "_").Replace(":", "_").Replace("/", "_") + ".xls";
                string modelpathfile = basepath + "ModelPathFrx" + "\\ReportFormListToExcel.frx";
                if (!Directory.Exists(basepath + path))
                    Directory.CreateDirectory(basepath + path);

                report.Load(modelpathfile);
                DataTable dt = ds.Tables[0];//ExcelFile
                dt.TableName = "ReportFormFull";
                report.RegisterData(ds);
                report.Prepare();
                //FastReport.Export.Xml.XMLExport export = new FastReport.Export.Xml.XMLExport();
                ZhiFang.Common.Log.Log.Error("DownloadReportExcelByPara.basepath + path + filename：" + basepath + path + filename);
                FastReport.Export.Xml.XMLExport export = new FastReport.Export.Xml.XMLExport();
                report.Export(export, basepath + path + filename);
                report.Dispose();
                string strTable = "/" + path + filename;

                brdv.success = true;
                brdv.ResultDataValue = strTable;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("DownloadReportExcelByPara异常：" + e.ToString());
                brdv.ErrorInfo = e.ToString();
                //throw;
            }
            return brdv;
        }
        #endregion


        #endregion

        #region 太和个人检验情况统计
        #region 获取报告地址
        public BaseResultDataValue GetStaticPersonTestItemPriceItem(ZhiFang.Model.StaticPersonTestItemPrice jsonentity)
        {
            //StaticPersonTestItemPrice ibvtif = BLLFactory<StaticPersonTestItemPrice>.GetBLL("StaticPersonTestItemPrice");
            BaseResultDataValue brdv = new BaseResultDataValue();
            FastReport.Report report = new FastReport.Report();
            //StaticPersonTestItemPrice sptip = JsonConvert.DeserializeObject<StaticPersonTestItemPrice>(jsonentity);
            string strTable = "";
            //ZhiFang.Model.StaticPersonTestItemPrice model = sptip;
            DataSet ds = new DataSet();
            try
            {
                Model.StaticPersonTestItemPrice model = new StaticPersonTestItemPrice();
                model.OperdateBegin = jsonentity.OperdateBegin;
                model.OperdateEnd = jsonentity.OperdateEnd;
                model.OperdateEnd = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")).AddDays(1).ToShortDateString();
                model.BCName = jsonentity.BCName;
                model.BarCode = jsonentity.BarCode;
                model.ClientName = jsonentity.ClientName;
                model.DCName = jsonentity.DCName;
                model.PatNo = jsonentity.PatNo;
                ds = rfb.GetStaticPersonTestItemPriceList(model);
                float money = 0;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["Price"] != null && ds.Tables[0].Rows[i]["Price"].ToString() != "")
                    {
                        money += float.Parse(ds.Tables[0].Rows[i]["Price"].ToString());
                    }
                }
                DataRow dr = ds.Tables[0].NewRow();
                dr["ClientName"] = "汇总";

                dr["Price"] = money.ToString("F2");
                ds.Tables[0].Rows.Add(dr);

                //返回数据列表或是生成html,返回文件地址
                if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ItemShowExcel")))
                    Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ItemShowExcel"));
                report.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ItemShowExcel") + "\\StaticPersonTestItemPrice.frx");
                DataTable dt = ds.Tables[0];
                dt.TableName = "Table";
                GC.Collect();
                report.RegisterData(dt.DataSet);
                DataBand data = report.FindObject("Data1") as DataBand;
                data.DataSource = report.GetDataSource("Table");
                report.Prepare();
                MHTExport export = new MHTExport();
                FastReport.Export.Html.HTMLExport aa = new FastReport.Export.Html.HTMLExport();
                aa.SinglePage = true;
                aa.SubFolder = false;
                aa.Navigator = false;
                string url = DateTime.Now.ToString().Replace(" ", "_").Replace(":", "_").Replace("/", "_") + ".html";
                report.Export(aa, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + url);
                report.Dispose();
                strTable = "/" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "/" + url;

                string strjson = "" + strTable + "#" + DownLoadStaticPersonTestItemPriceExcel(ds) + "";

                //ViewState["fielPath"] = strTable;
                brdv.success = true;
                brdv.ResultDataValue = strjson;

            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = DateTime.Now.ToString("YYYYMMDD HH:mm:ss") + ex.ToString();
                ZhiFang.Common.Log.Log.Debug(brdv.ErrorInfo);
            }
            return brdv;
        }

        public string DownLoadStaticPersonTestItemPriceExcel(DataSet ds)
        {
            // IBView_ReportItemFull ibvtif = BLLFactory<IBView_ReportItemFull>.GetBLL("BView_ReportItemFull");
            BaseResultDataValue brdv = new BaseResultDataValue();
            FastReport.Report report = new FastReport.Report();
            string strTable = "";
            //DataSet ds = new DataSet();
            try
            {
                //ds = ibvtif.GetViewItemFull(jsonentity);
                string url = DateTime.Now.ToString().Replace(" ", "_").Replace(":", "_").Replace("/", "_") + ".xls";
                string strPath = System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\" + url;
                if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel")))
                    Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel"));

                report.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ItemShowExcel") + "\\StaticPersonTestItemPrice.frx");
                DataTable dt = ds.Tables[0];//ExcelFile
                dt.TableName = "Table";
                GC.Collect();
                report.RegisterData(dt.DataSet);
                DataBand data = report.FindObject("Data1") as DataBand;
                data.DataSource = report.GetDataSource("Table");
                report.Prepare();
                FastReport.Export.Xml.XMLExport export = new FastReport.Export.Xml.XMLExport();
                report.Export(export, strPath);
                report.Dispose();
                strTable = "/" + ConfigHelper.GetConfigString("SaveExcel") + "/" + url;
                brdv.success = true;
                brdv.ResultDataValue = strTable;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = DateTime.Now.ToString("YYYYMMDD HH:mm:ss") + ex.ToString();
                ZhiFang.Common.Log.Log.Debug(brdv.ErrorInfo);
            }
            return strTable;
        }
        #endregion

        #region 下载
        public BaseResultDataValue DownLoadStaticPersonTestItemPriceExcel(StaticPersonTestItemPrice jsonentity)
        {

            //Model.StaticPersonTestItemPrice ibvtif = BLLFactory<Model.StaticPersonTestItemPrice>.GetBLL("StaticPersonTestItemPrice");
            BaseResultDataValue brdv = new BaseResultDataValue();
            FastReport.Report report = new FastReport.Report();
            Model.StaticPersonTestItemPrice model = new StaticPersonTestItemPrice(); model.OperdateBegin = jsonentity.OperdateBegin;
            model.OperdateBegin = jsonentity.OperdateBegin;
            model.OperdateEnd = jsonentity.OperdateEnd;
            model.OperdateEnd = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")).AddDays(1).ToShortDateString();
            model.BCName = jsonentity.BCName;
            model.BarCode = jsonentity.BarCode;
            model.ClientName = jsonentity.ClientName;
            model.DCName = jsonentity.DCName;
            model.PatNo = jsonentity.PatNo;
            //var page = (int)jsonentity.page;
            //var rows = (int)jsonentity.rows;
            string strTable = "";
            DataSet ds = new DataSet();
            ds = rfb.GetStaticPersonTestItemPriceList(model);
            float money = 0;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["Price"] != null && ds.Tables[0].Rows[i]["Price"].ToString() != "")
                {
                    money += float.Parse(ds.Tables[0].Rows[i]["Price"].ToString());
                }
            }
            DataRow dr = ds.Tables[0].NewRow();
            dr["ClientName"] = "汇总";
            dr["Age"] = "";
            dr["OperDate"] = "";
            dr["ParItemNo"] = "";
            dr["Price"] = money.ToString();
            ds.Tables[0].Rows.Add(dr);
            try
            {
                brdv.ResultDataFormatType = "json";
                brdv.ResultDataValue = DataSetToJson.ToJson(ds.Tables[0]);
                BaseResultDataSet brds = new BaseResultDataSet();
                brds.total = ds.Tables[0].Rows.Count;
                brds.rows = ds.Tables[0];
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(brds);
                string url = DateTime.Now.ToString().Replace(" ", "_").Replace(":", "_").Replace("/", "_") + ".xls";
                string strPath = System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\" + url;
                if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel")))
                    Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel"));
                report.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ItemShowExcel") + "\\StaticPersonTestItemPrice.frx");
                DataTable dt = ds.Tables[0];//ExcelFile
                dt.TableName = "Table";
                report.RegisterData(ds);
                DataBand data = report.FindObject("Data1") as DataBand;
                data.DataSource = report.GetDataSource("Table");
                report.Prepare();
                FastReport.Export.Xml.XMLExport export = new FastReport.Export.Xml.XMLExport();
                report.Export(export, strPath);
                report.Dispose();
                strTable = "/" + ConfigHelper.GetConfigString("SaveExcel") + "/" + url;
                brdv.success = true;
                brdv.ResultDataValue = strTable;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = DateTime.Now.ToString("YYYYMMDD HH:mm:ss") + ex.ToString();
                ZhiFang.Common.Log.Log.Debug(brdv.ErrorInfo);
            }
            return brdv;
        }
        #endregion

        #region 打印
        public BaseResultDataValue GetStaticPersonTestItemPricePrint(StaticPersonTestItemPrice jsonentity)
        {
            throw new NotImplementedException();
        }
        #endregion
        #endregion

        ///<summary>
        ///Liwk 2015-09-01 add 报表导出
        ///
        #region 工作量统计报表

        #region 获取报告地址
        public BaseResultDataValue GetStaticRecOrgSamplePrice(string StartDate, string EndDate, int rows, int page, string labName, string TestItem)
        {
            IBNRequestForm ibvtif = BLLFactory<IBNRequestForm>.GetBLL("NRequestForm");
            BaseResultDataValue brdv = new BaseResultDataValue();
            FastReport.Report report = new FastReport.Report();
            DataSet ds = new DataSet();
            string strTable = "";
            try
            {
                Model.StaticRecOrgSamplePrice model = new StaticRecOrgSamplePrice();
                model.OperDateBegin = StartDate;
                model.OperDateEnd = EndDate;
                model.ClientName = labName;
                model.CName = TestItem;
                ds = ibvtif.GetStaticRecOrgSamplePrice(model);

                int num = 0;
                int money = 0;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    num += int.Parse(ds.Tables[0].Rows[i]["SampleNum"].ToString());
                    money += int.Parse(ds.Tables[0].Rows[i]["ItemTotalPrice"].ToString());
                }
                DataRow dr = ds.Tables[0].NewRow();
                dr["SampleNum"] = num;
                dr["ItemTotalPrice"] = money;
                //dr["Price"] = "";
                //dr["ParItemNo"] = "";
                dr["ClientName"] = "汇总";
                ds.Tables[0].Rows.Add(dr);

                if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ItemShowExcel")))
                    Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ItemShowExcel"));
                report.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ItemShowExcel") + "\\workcountFullShow.frx");
                DataTable dt = ds.Tables[0];
                dt.TableName = "Table";
                GC.Collect();
                report.RegisterData(dt.DataSet);
                DataBand data = report.FindObject("Data1") as DataBand;
                data.DataSource = report.GetDataSource("Table");
                report.Prepare();
                MHTExport export = new MHTExport();
                FastReport.Export.Html.HTMLExport aa = new FastReport.Export.Html.HTMLExport();
                aa.SinglePage = true;
                aa.SubFolder = false;
                aa.Navigator = false;
                string url = DateTime.Now.ToString().Replace(" ", "_").Replace(":", "_").Replace("/", "_") + ".html";
                report.Export(aa, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + url);
                report.Dispose();
                strTable = "/" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "/" + url;
                string str = "workcountFullShow";
                string strjson = "" + strTable + "#" + DownLoadExcel(ds, str) + "";

                //ViewState["fielPath"] = strTable;
                brdv.success = true;
                brdv.ResultDataValue = strjson;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = DateTime.Now.ToString("YYYYMMDD HH:mm:ss") + ex.ToString();
                ZhiFang.Common.Log.Log.Debug(brdv.ErrorInfo);
            }
            return brdv;
        }

        public BaseResultDataValue GetBarcodePrice(string StartDate, string EndDate, int rows, int page, string labName, string DateType)
        {
            IBNRequestForm ibvtif = BLLFactory<IBNRequestForm>.GetBLL("NRequestForm");
            BaseResultDataValue brdv = new BaseResultDataValue();
            FastReport.Report report = new FastReport.Report();
            DataSet ds = new DataSet();
            string strTable = "";
            try
            {
                Model.StaticRecOrgSamplePrice model = new StaticRecOrgSamplePrice();
                model.OperDateBegin = StartDate;
                model.OperDateEnd = EndDate;
                model.ClientName = labName;
                model.DateType = DateType;
                ds = ibvtif.GetBarcodePrice(model);

                int num = 0;
                float money = 0;
                float temp = 0;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    num += int.Parse(ds.Tables[0].Rows[i]["Barcode"].ToString());
                    if (ds.Tables[0].Rows[i]["Price"] != null && ds.Tables[0].Rows[i]["Price"].ToString() != "")
                    {
                        temp = float.Parse(ds.Tables[0].Rows[i]["Price"].ToString());
                        money += temp;
                    }
                }
                double mon = Math.Round(money, 2);
                DataRow dr = ds.Tables[0].NewRow();
                dr["Barcode"] = num;
                dr["Price"] = mon;
                //dr["Price"] = "";
                dr["OperDate"] = "";
                dr["ClientName"] = "汇总";
                ds.Tables[0].Rows.Add(dr);

                if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ItemShowExcel")))
                    Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ItemShowExcel"));
                report.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ItemShowExcel") + "\\BarcodeCount.frx");
                DataTable dt = ds.Tables[0];
                dt.TableName = "Table";
                GC.Collect();
                report.RegisterData(dt.DataSet);
                DataBand data = report.FindObject("Data1") as DataBand;
                data.DataSource = report.GetDataSource("Table");
                report.Prepare();
                MHTExport export = new MHTExport();
                FastReport.Export.Html.HTMLExport aa = new FastReport.Export.Html.HTMLExport();
                aa.SinglePage = true;
                aa.SubFolder = false;
                aa.Navigator = false;
                string url = DateTime.Now.ToString().Replace(" ", "_").Replace(":", "_").Replace("/", "_") + ".html";
                report.Export(aa, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + url);
                report.Dispose();
                strTable = "/" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "/" + url;
                string str = "BarcodeCount";
                string strjson = "" + strTable + "#" + DownLoadExcel(ds, str) + "";

                //ViewState["fielPath"] = strTable;
                brdv.success = true;
                brdv.ResultDataValue = strjson;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = DateTime.Now.ToString("YYYYMMDD HH:mm:ss") + ex.ToString();
                ZhiFang.Common.Log.Log.Debug(brdv.ErrorInfo);
            }
            return brdv;
        }

        /// <summary>
        /// 太和 医院操作人员工作量统计 ganwh add 2015-11-2
        /// </summary>
        /// <param name="OperDateSart"></param>
        /// <param name="OperDateEnd"></param>
        /// <param name="ClientNo"></param>
        /// <param name="Operator"></param>
        /// <param name="DateType"></param>
        /// <returns></returns>
        public BaseResultDataValue GetOperatorWorkCountExcel(string OperDateSart, string OperDateEnd, string ClientNo, string Operator, string DateType)
        {
            IBNRequestForm ibvtif = BLLFactory<IBNRequestForm>.GetBLL("NRequestForm");
            BaseResultDataValue brdv = new BaseResultDataValue();
            FastReport.Report report = new FastReport.Report();
            DataSet ds = new DataSet();
            string strTable = "";
            try
            {
                Model.StaticRecOrgSamplePrice model = new StaticRecOrgSamplePrice();
                model.OperDateBegin = OperDateSart;
                model.OperDateEnd = OperDateEnd;
                model.ClientNo = ClientNo;
                model.Operator = Operator;
                model.DateType = DateType;

                ds = ibvtif.GetOpertorWorkCount(model, 0, 0);

                int num = 0;
                float money = 0;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    num += int.Parse(ds.Tables[0].Rows[i]["BarcodeNum"].ToString());
                    if (ds.Tables[0].Rows[i]["SumMoney"] != null && ds.Tables[0].Rows[i]["SumMoney"].ToString() != "")
                        money += float.Parse(ds.Tables[0].Rows[i]["SumMoney"].ToString());
                }
                DataRow dr = ds.Tables[0].NewRow();
                dr["BarcodeNum"] = num;
                dr["SumMoney"] = money.ToString("F2");

                dr["OperDate"] = "汇总";
                ds.Tables[0].Rows.Add(dr);

                if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ItemShowExcel")))
                    Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ItemShowExcel"));
                report.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ItemShowExcel") + "\\OperatorWorkCount.frx");
                DataTable dt = ds.Tables[0];
                dt.TableName = "Table";
                GC.Collect();
                report.RegisterData(dt.DataSet);
                DataBand data = report.FindObject("Data1") as DataBand;
                data.DataSource = report.GetDataSource("Table");
                report.Prepare();
                MHTExport export = new MHTExport();
                FastReport.Export.Html.HTMLExport aa = new FastReport.Export.Html.HTMLExport();
                aa.SinglePage = true;
                aa.SubFolder = false;
                aa.Navigator = false;
                string url = DateTime.Now.ToString().Replace(" ", "_").Replace(":", "_").Replace("/", "_") + ".html";
                report.Export(aa, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + url);
                report.Dispose();
                strTable = "/" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "/" + url;
                string str = "OperatorWorkCount";
                string strjson = "" + strTable + "#" + DownLoadExcel(ds, str) + "";

                //ViewState["fielPath"] = strTable;
                brdv.success = true;
                brdv.ResultDataValue = strjson;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = DateTime.Now.ToString("YYYYMMDD HH:mm:ss") + ex.ToString();
                ZhiFang.Common.Log.Log.Debug(brdv.ErrorInfo);
            }
            return brdv;
        }

        private string DownLoadExcel(DataSet ds, string str)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            FastReport.Report report = new FastReport.Report();
            string strTable = "";
            try
            {
                string url = DateTime.Now.ToString().Replace(" ", "_").Replace(":", "_").Replace("/", "_") + ".xls";
                string strPath = System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\" + url;
                if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel")))
                    Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel"));

                report.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ItemShowExcel") + "\\" + str + ".frx");
                DataTable dt = ds.Tables[0];//ExcelFile
                dt.TableName = "Table";
                GC.Collect();
                report.RegisterData(dt.DataSet);
                DataBand data = report.FindObject("Data1") as DataBand;
                data.DataSource = report.GetDataSource("Table");
                report.Prepare();
                FastReport.Export.Xml.XMLExport export = new FastReport.Export.Xml.XMLExport();
                report.Export(export, strPath);
                report.Dispose();
                strTable = "/" + ConfigHelper.GetConfigString("SaveExcel") + "/" + url;
                brdv.success = true;
                brdv.ResultDataValue = strTable;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = DateTime.Now.ToString("YYYYMMDD HH:mm:ss") + ex.ToString();
                ZhiFang.Common.Log.Log.Debug(brdv.ErrorInfo);
            }
            return strTable;
        }
        #endregion

        #region 下载
        public BaseResultDataValue DownLoadStaticRecOrgSamplePriceExcel(string StartDate, string EndDate, int rows, int page, string labName, string TestItem)
        {

            //Model.StaticRecOrgSamplePrice ibvtif = BLLFactory<Model.StaticRecOrgSamplePrice>.GetBLL("StaticRecOrgSamplePrice");
            IBNRequestForm ibvtif = BLLFactory<IBNRequestForm>.GetBLL("NRequestForm");
            Model.StaticRecOrgSamplePrice model = new StaticRecOrgSamplePrice();
            BaseResultDataValue brdv = new BaseResultDataValue();
            FastReport.Report report = new FastReport.Report();
            string strTable = "";
            DataSet ds = rfb.GetStaticRecOrgSamplePrice(model, rows, page);
            try
            {
                brdv.ResultDataFormatType = "json";
                brdv.ResultDataValue = DataSetToJson.ToJson(ds.Tables[0]);
                BaseResultDataSet brds = new BaseResultDataSet();
                brds.total = ds.Tables[0].Rows.Count;
                brds.rows = ds.Tables[0];
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(brds);
                string url = DateTime.Now.ToString().Replace(" ", "_").Replace(":", "_").Replace("/", "_") + ".xls";
                string strPath = System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\" + url;
                if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel")))
                    Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel"));
                report.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ItemShowExcel") + "\\ReportItemFullForExcel.frx");
                DataTable dt = ds.Tables[0];//ExcelFile
                dt.TableName = "Table";
                report.RegisterData(ds);
                DataBand data = report.FindObject("Data1") as DataBand;
                data.DataSource = report.GetDataSource("Table");
                report.Prepare();
                FastReport.Export.Xml.XMLExport export = new FastReport.Export.Xml.XMLExport();
                report.Export(export, strPath);
                report.Dispose();
                strTable = "/" + ConfigHelper.GetConfigString("SaveExcel") + "/" + url;
                brdv.success = true;
                brdv.ResultDataValue = strTable;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = DateTime.Now.ToString("YYYYMMDD HH:mm:ss") + ex.ToString();
                ZhiFang.Common.Log.Log.Debug(brdv.ErrorInfo);
            }
            return brdv;
        }
        #endregion


        #region 打印
        #endregion

        #endregion

        /// <summary>
        /// 删除报告单
        /// </summary>
        /// <param name="ReportFormID"></param>
        /// <returns></returns>
        public BaseResult DeleteReportForm(string ReportFormID)
        {
            BaseResult br = new BaseResult();
            string strBool = string.Empty;
            IBReportFormFull ibrff = BLLFactory<IBReportFormFull>.GetBLL("ReportFormFull");

            ReportFormFull rff = ibrff.GetModel(ReportFormID);
            //判断报告是否存在
            if (rff != null)
            {
                if (ibrff.Delete(ReportFormID) > 0)
                {
                    //br.success = true;
                    strBool += "true";

                }
            }

            IBReportItemFull ibrif = BLLFactory<IBReportItemFull>.GetBLL("ReportItemFull");
            DataSet ds = ibrif.GetList("ReportFormID='" + ReportFormID + "'");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ibrif.DeleteByWhere("ReportFormID='" + ReportFormID + "'") > 0)
                {
                    strBool += "true";
                }
            }

            IBReportMarrowFull ibmf = BLLFactory<IBReportMarrowFull>.GetBLL("ReportMarrowFull");
            DataSet ds1 = ibmf.GetList("ReportFormID='" + ReportFormID + "'");
            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                if (ibmf.DeleteByWhere("ReportFormID='" + ReportFormID + "'") > 0)
                {
                    strBool += "true";
                }
            }


            IBReportMicroFull ibmff = BLLFactory<IBReportMicroFull>.GetBLL("ReportMicroFull");
            DataSet ds2 = ibmff.GetList("ReportFormID='" + ReportFormID + "'");
            if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
            {
                if (ibmff.DeleteByWhere("ReportFormID='" + ReportFormID + "'") > 0)
                {
                    strBool += "true";
                }
            }

            if (strBool.Contains("true"))
            {
                br.success = true;
            }
            else
                br.success = false;
            return br;
        }

        /// <summary>
        /// 获取报告查询列表
        /// </summary>
        /// <param name="reportformfull"></param>
        /// <param name="wherestr"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public BaseResultDataValue SelectReportListByPerson_Barcode_Name(string Barcode, string Name, int page, int rows, string sort, string order)
        {

            BaseResultDataValue brdv = new BaseResultDataValue();
            //EntityListEasyUI<UiReportForm_N> easyui = new EntityListEasyUI<UiReportForm_N>();
            EntityListEasyUI<Model.ReportFormFull> easyui = new EntityListEasyUI<Model.ReportFormFull>();
            try
            {
                string wheresql = "";
                #region 验证

                if (Barcode == null || Barcode == string.Empty || Barcode.Trim() == "")
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "Barcode错误";
                    return brdv;
                }
                Barcode = ZhiFang.Tools.Validate.ReplaceWhereString(Barcode);

                if (Name == null || Name == string.Empty || Name.Trim() == "")
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "Name错误";
                    return brdv;
                }
                Name = ZhiFang.Tools.Validate.ReplaceWhereString(Name);
                wheresql = " SerialNo='" + Barcode.Trim() + "' and CName='" + Name.Trim() + "' ";
                //默认五千条
                int ReportSelMaxNum = 5000;
                var max = ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportSelMaxNum").Trim();

                if (max != null || max != "")
                {
                    ReportSelMaxNum = Convert.ToInt32(max);
                }

                try
                {
                    if (ibrff.Count(wheresql) > ReportSelMaxNum)
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = "返回记录数大于配置的条数";
                        return brdv;
                    }
                }
                catch (Exception e)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = e.ToString();
                    Model.ReportFormFull model = new ReportFormFull();
                    //brdv = SelectReportList2(model, page.ToString(), rows.ToString());
                    return brdv;
                }
                #endregion
                string sqlsort = "";
                if (sort != null && sort.Trim() != "" && sort.Trim().ToLower() != "null")
                {
                    sqlsort = " order by " + sort.Replace(";", "").Replace("'", "");
                    if (order != null && order.Trim() != "" && order.Trim().ToLower() != "null")
                    {
                        sqlsort += " " + order.Replace(";", "").Replace("'", "");

                    }
                }
                else
                {
                    sqlsort = "  order by RECEIVEDATE desc ";
                }
                ZhiFang.Common.Log.Log.Debug("SelectReportList.wherestr+sqlsort=" + wheresql + sqlsort);
                DataSet ds = ibrff.GetList(wheresql + sqlsort);
                if (ds == null)
                {
                    return null;
                }
                DataTable tempDt = ds.Tables[0];
                List<Model.ReportFormFull> reportFormFullList = ibrff.DataTableToList(tempDt);
                List<Model.ReportFormFull> Result = LT.Pagination<Model.ReportFormFull>(page, rows, reportFormFullList);
                easyui.rows = Result;
                easyui.total = reportFormFullList.Count;

                brdv.success = true;
                brdv.ResultDataValue = ZhiFang.BLL.Common.JsonHelp.JsonDotNetSerializer(easyui);
            }
            catch (Exception)
            {

            }
            return brdv;
        }
        public bool PersonSearch_ValidateCode_PKI(string tmpvalidatecode)
        {
            if (HttpContext.Current.Session["ValidateCode"] != null && HttpContext.Current.Session["ValidateCode"].ToString().Trim() != "")
            {
                if (tmpvalidatecode != null && tmpvalidatecode.Trim() != "")
                {
                    if (HttpContext.Current.Session["ValidateCode"].ToString().Trim().ToLower() == tmpvalidatecode.Trim().ToLower())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
