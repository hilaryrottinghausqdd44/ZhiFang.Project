using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections;
using ZhiFang.IBLL.Common;
using ZhiFang.BLLFactory;
using ZhiFang.IBLL.Report;
using System.Reflection;
using ZhiFang.Common.Public;
using System.Web.Script.Serialization;
using Tools;
using ZhiFang.Common.Log;

namespace ZhiFang.WebLis.Ashx
{
    /// <summary>
    /// SelectReportPrint 的摘要说明
    /// </summary>
    public class SelectReportPrint : IHttpHandler
    {
        Tools.ListTool LT = new Tools.ListTool();
        public readonly IBPrintFrom_Weblis Printform_Weblis = BLLFactory<IBPrintFrom_Weblis>.GetBLL("PrintFrom_Weblis");
        public readonly IBPrintFrom_Weblis Printform_WeblisFr3 = BLLFactory<IBPrintFrom_Weblis>.GetBLL("PrintUseDeliph");
        private readonly IBReportFormFull ibrff = BLLFactory<IBReportFormFull>.GetBLL("ReportFormFull");
        private readonly IBShowFrom showform = BLLFactory<IBShowFrom>.GetBLL("ShowFrom");
        private readonly IBLL.Report.Other.IBView ibv = BLLFactory<IBLL.Report.Other.IBView>.GetBLL("Other.BView");
        public void ProcessRequest(HttpContext context)
        {
            string[] Msg = new string[1];
            string urlType = context.Request.QueryString["Type"];

            if (urlType == "SelectReport")
            {
                #region 查询报告
                string urlWhere = context.Request.QueryString["Where"];
                string urlModel = context.Request.QueryString["fields"];
                string urlPage = context.Request.QueryString["page"];
                string urlLimit = context.Request.QueryString["limit"];

                if (urlWhere.Length < 4)
                {
                    Msg[0] = "Error:请选择查询条件!";
                    context.Response.Write("Error:请选择查询条件!");
                    return;
                }
                if (urlModel.Length < 1)
                {
                    Msg[0] = "Error:请选择要查询的数据!";
                    context.Response.Write("Error:请选择要查询的数据!");
                    return;
                }


                //urlModel = "WeblisSourceOrgId,CNAME,ReportFormID,RECEIVEDATE,PRINTTIMES";
                //urlWhere = " RECEIVEDATE between '2013-10-27' and '2013-10-28' ";

                Model.ReportFormFull model = new Model.ReportFormFull();
                DataSet ds = new DataSet();
                if (string.IsNullOrEmpty(urlWhere))
                {
                    urlWhere = " 1=1 ";
                }
                ds = ibrff.GetList(urlWhere);
                //List<Model.ReportFormFull> ils = GetList<Model.ReportFormFull>(ds.Tables[0]);
                List<Model.ReportFormFull> ils = LT.GetListColumns<Model.ReportFormFull>(ds.Tables[0]);
                //List<Model.ReportFormFull> Result = Pagination<Model.ReportFormFull>(context, ils);
                List<Model.ReportFormFull> Result = LT.Pagination<Model.ReportFormFull>(Convert.ToInt32(urlPage), Convert.ToInt32(urlLimit), ils);
                var settings = new JsonSerializerSettings();
                settings.ContractResolver = new LimitPropsContractResolver(urlModel.Split(','));
                string aa = JsonConvert.SerializeObject(Result, Formatting.Indented, settings);
                context.Response.Write("{\"total\":" + ils.Count + ",\"rows\":" + aa + "}");
                context.Response.ContentType = "text/plain";
                #endregion
            }
            else if (urlType == "Preview")
            {
                #region  预览报告
                string urlReportFormID = context.Request.QueryString["ReportFormID"];
                string urlSectionNo = context.Request.QueryString["SectionNo"];
                string urlSectionType = context.Request.QueryString["SectionType"];
                string ulModelType = context.Request.QueryString["ModelType"];
                string urlPageName = "TechnicianPrint1.aspx";
                string urlSorg = "0";
                string urlShowType = "0";


                //string urlReportFormID = "_2160720_2_1_2111_2014-03-27 00:00:00";
                //string urlSectionNo = "2";
                //string urlSectionType = "1";
                try
                {//<?xml version="1.0" encoding="utf-8"?>
                    string[] aaa = new string[2];
                    int st = ZhiFang.Common.Public.Valid.ToInt(urlShowType);
                    int sn = ZhiFang.Common.Public.Valid.ToInt(urlSectionNo);
                    //ZhiFang.Common.Log.Log.Debug(DateTime.Now.ToString("YYYYMMDD HH:mm:ss")+"aaa[0]");
                    //if (int.Parse(urlShowType) >= 0)
                    //{
                    //if (ConfigHelper.GetConfigString("modelType").ToString() == "JPEG")
                    if (ulModelType == "jpg")
                    {
                        //用Fr3显示 找模版去数据库
                        aaa[0] = "暂不支持Fr3模版！";// showformFr3.ShowReportForm_Weblis(urlReportFormID, sn, urlPageName, st, Convert.ToInt32(urlSectionType));

                    }
                    else
                    {   //用xslt显示 找模版去xml配置文件
                        aaa[0] = showform.ShowReportForm_Weblis(urlReportFormID, sn, urlPageName, st, Convert.ToInt32(urlSectionType));
                    }
                  
                    if (aaa[0].IndexOf("<html") >= 0)
                    {
                        aaa[0] = aaa[0].Substring(aaa[0].IndexOf("<html"), aaa[0].Length - aaa[0].IndexOf("<html"));
                    }
                    aaa[0] = aaa[0].Replace("\r\n ", " ");
                    //aaa[1] = ShowTools.ShowFormTypeList(FormNo, sn, PageName);
                    context.Response.Write(aaa[0]);
                }
                catch (Exception e)
                {
                    ZhiFang.Common.Log.Log.Debug(DateTime.Now.ToString("YYYYMMDD HH:mm:ss") + e.ToString());

                }
                #endregion
            }
            else if (urlType == "Print")
            {
                #region 打印报告
                string ReportFormID = context.Request.QueryString["ReportFormID"];
                string ReportFormTitle = context.Request.QueryString["ReportFormTitle"];
                string PrintType = context.Request.QueryString["PrintType"];

                ZhiFang.Common.Log.Log.Info("这是ReportPrint_Image(方法)：ReportPrint.ashx");
                try
                {
                    string[] ImageList = null;
                    string title = "0";
                    if (ReportFormTitle != null)
                    {
                        if (ReportFormTitle == "Center")
                        {
                            title = "0";
                        }
                        else
                        {
                            if (ReportFormTitle == "Client")
                            {
                                title = "1";
                            }
                            else
                            {
                                if (ReportFormTitle == "Batch")
                                {
                                    title = "2";
                                }
                                else
                                {
                                    if (ReportFormTitle == "MenZhen")
                                    {
                                        title = "3";
                                    }
                                    else
                                    {
                                        if (ReportFormTitle == "ZhuYuan")
                                        {
                                            title = "4";
                                        }
                                        else
                                        {
                                            if (ReportFormTitle == "TiJian")
                                            {
                                                title = "5";
                                            }
                                            else
                                            {
                                                //Response.Write("title---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (PrintType != null && PrintType == "A5")
                        {
                            if (ReportFormTitle != null)
                            {
                                ImageList = this.PrintHtml(ReportFormID.Split(','), title);
                            }
                        }
                        else
                        {
                            string[] tmp = ReportFormID.Split(',');
                            SortedList sl = new SortedList();
                            string tmpserial = "";
                            for (int i = 0; i < tmp.Length; i++)
                            {
                                if (tmp[i].Split(';').Length > 1)
                                {
                                    if (tmpserial != tmp[i].Split(';')[1])
                                    {
                                        sl.Add(tmp[i].Split(';')[1], tmp[i].Split(';')[0]);
                                        tmpserial = tmp[i].Split(';')[1];
                                    }
                                    else
                                    {
                                        sl[tmp[i].Split(';')[1]] += ',' + tmp[i].Split(';')[0];
                                    }
                                }
                            }
                            if (PrintType != null && PrintType == "A4")
                            {
                                if (ReportFormTitle != null)
                                {
                                    ImageList = this.PrintMergeHtml(sl, title);
                                }
                            }
                            if (PrintType != null && PrintType == "EA4")
                            {
                                if (ReportFormTitle != null)
                                {
                                    ImageList = this.PrintMergeEnHtml(sl, title);
                                }
                            }
                        }
                    }
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(ImageList, Formatting.Indented, settings);
                    context.Response.Write(aa); ;
                }
                catch
                {

                }
                #endregion
            }
            else if (urlType == "Test")
            {
                #region 返回时间
                string ToDay = "";
                string ThisWeek = "";
                string startWeek = "";
                string endWeek = "";
                string ThisMonth = "";
                int month = 0;
                int dayCount = 0;
                //yyyy-MM-dd HH:mm:ss:
                string aa = " 1=1 ";
                ZhiFang.Common.Log.Log.Info("长度:" + aa.Length);


                ///本日
                DateTime dtime = DateTime.Now;
                ToDay = " between '" + DateTime.Now.ToString("yyyy-MM-dd") + "' and '" + DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59'";
                ZhiFang.Common.Log.Log.Info("本日:" + ToDay);

                ///本周 
                startWeek = dtime.AddDays(1 - Convert.ToInt32(dtime.DayOfWeek.ToString("d"))).ToString("yyyy-MM-dd");
                endWeek = dtime.AddDays(1 - Convert.ToInt32(dtime.DayOfWeek.ToString("d"))).AddDays(6).ToString("yyyy-MM-dd") + " 23:59:59";
                ThisWeek = " between '" + startWeek + "' and '" + endWeek + "'";
                ZhiFang.Common.Log.Log.Info("本周:" + ThisWeek);

                ///本月
                month = dtime.Date.Month;
                dayCount = DateTime.DaysInMonth(dtime.Date.Year, month);

                ThisMonth = " between '" + dtime.AddDays(1 - (dtime.Day)).ToString("yyyy-MM-dd") + "' and '" + dtime.AddDays(1 - (dtime.Day)).AddDays(dayCount - 1).ToString("yyyy-MM-dd") + " 23:59:59'";
                ZhiFang.Common.Log.Log.Info("本月:" + ThisMonth);

                context.Response.Write(ToDay + "-------" + ThisWeek + "--------" + ThisMonth);





                //本月最后一天时间    
                //DateTime dt_Last = dt_First.AddDays(dayCount - 1);


                #endregion
            }
            else if (urlType == "Previews")
            {
                #region  预览报告整理后
                //string urlReportFormID = context.Request.QueryString["ReportFormID"];
                //string urlSectionNo = context.Request.QueryString["SectionNo"];
                //string urlSectionType = context.Request.QueryString["SectionType"];
                string urlPageName = "TechnicianPrint1.aspx";
                string urlSorg = "0";
                string urlShowType = "0";
                string urlReportFormID = "_2160720_2_1_2111_2014-03-27 00:00:00";
                string urlSectionNo = "2";
                string urlSectionType = "1";

                //标准参数
                string FromNo = string.Empty;
                string SectionNo = string.Empty;
                string PageName = string.Empty;
                string ShowType = string.Empty;
                string SectionType = string.Empty;

                FromNo = urlReportFormID;
                SectionNo = urlSectionNo;
                PageName = urlPageName;
                ShowType = urlShowType;
                SectionType = urlSectionType;

                Log.Info(DateTime.Now.ToLongDateString() + "FromNo:" + FromNo + "SectionNo:" + SectionNo + "PageName:" + PageName + "ShowType:" + ShowType + "SectionType:" + SectionType);

                string[] aaa = new string[2];
                int emplateType = 0; //模版类型方法
                string ShowModel = "";
                string ShowName = "";
                DataTable dtrf = new DataTable();
                DataTable dtTable = new DataTable();
                DataTable dtGraph = new DataTable();
                try
                {
                    //取数据
                    //判断是否调用打印模版0不调用 1调用  改为是否从数据库读取配置0从XML1从数据库
                    if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("Template") == "0")
                    {
                        Log.Info(DateTime.Now.ToLongDateString() + " 从数据库读取选择模版（Template）:" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("Template"));
                        //读取xmlConfig
                        //返回路径ShowModel,返回执行方法emplateType
                        ShowModel = showform.ShowReportXML(FromNo, PageName, ShowType, SectionType, out  emplateType);
                        Log.Info(DateTime.Now.ToLongDateString() + " ShowModel:" + ShowModel);

                        ShowName = showform.CheckSupergroup(FromNo, Convert.ToInt32(SectionType), out dtrf, out dtTable, out dtGraph);
                        Log.Info(DateTime.Now.ToLongDateString() + " ShowName:" + ShowName);
                        if (emplateType == 0)
                        {
                            aaa[0] = showform.ShowReportXSLT(dtrf, dtTable, ShowName, ShowModel);
                        }
                        else if (emplateType == 1)
                        {
                            aaa[0] = "暂不支持FR3模版!"; //showform.ShowReportFR3(FromNo, dtrf, dtTable, ShowName, ShowModel);
                        }
                        else if (emplateType == 2)
                        {
                            aaa[0] = showform.ShowReportFRX(FromNo, dtrf, dtTable, ShowName, ShowModel);
                        }
                        else
                        {
                            Log.Info("配置有错误！");
                        }
                    }
                    else if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("Template") == "1")
                    {
                        //读取数据库
                        Log.Info(DateTime.Now.ToLongDateString() + " 从Xml文件读取选择模版Template:" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("Template"));
                        ShowName = showform.CheckSupergroup(FromNo, Convert.ToInt32(SectionType), out dtrf, out dtTable, out dtGraph);

                        Log.Info(DateTime.Now.ToLongDateString() + " ShowName:" + ShowName);

                        ShowModel = showform.ShowReportSqlDB(FromNo, dtrf, dtTable, SectionType, out  emplateType);
                        Log.Info(DateTime.Now.ToLongDateString() + " ShowModel:" + ShowModel);

                        aaa[0] = "暂不支持FR3模版!"; //showform.ShowReportFR3(FromNo, dtrf, dtTable, ShowName, ShowModel);

                        switch (emplateType)
                        {
                            case 0:
                                aaa[0] = showform.ShowReportXSLT(dtrf, dtTable, ShowName, ShowModel);
                                break;
                            case 1:
                                aaa[0] = "暂不支持FR3模版!"; //showform.ShowReportFR3(FromNo, dtrf, dtTable, ShowName, ShowModel);
                                break;
                            case 2:
                                aaa[0] = showform.ShowReportFRX(FromNo, dtrf, dtTable, ShowName, ShowModel);
                                break;
                            default:
                                Log.Info("配置有错误！");
                                break;
                        }
                    }
                    else
                    {
                        Log.Info(DateTime.Now + ":请在Web.Config中配置Template！");
                    }
                    context.Response.Write(aaa[0]);
                }
                catch (Exception ex)
                {
                    Log.Info(ex.TargetSite + ex.Message);
                    context.Response.Write("Error:报告获取失败！");
                }
                #endregion
            }
            else if (urlType == "History")
            {
                #region 历史对比
                //('21085266;1124;item;2011-01-05')
                //PatNo varchar(20), --病历号
                //ItemNo varchar(20), --项目号
                //Check varchar(10), --SectionType
                //StarRd varchar(50), --起始日期
                //EndRd varchar(50) --截止日期
                //param[0]:21085266param[1]:900param[2]itemparam[3]2010-10-05param[4]2011-01-05

                string urlPatNo = context.Request.QueryString["PatNo"];
                string urlItemNo = context.Request.QueryString["ItemNo"];
                string urlTable = context.Request.QueryString["Table"];
                string urlWhere = context.Request.QueryString["Where"];
                string PatNo = string.Empty;
                string ItemNo = string.Empty;
                string Table = string.Empty;
                string Where = string.Empty;

                PatNo = urlPatNo;
                ItemNo = urlItemNo;
                Table = urlTable;
                Where = urlWhere;

                DataSet ds = new DataSet();
                string[] param = new string[4];

                //string[] url = context.Request.QueryString["primary"].Split(';');
                if (PatNo.Length < 1)
                {
                    Msg[0] = "Error:病历号空!";
                    context.Response.Write("Error:病历号空!");
                    return;
                }
                if (ItemNo.Length < 1)
                {
                    Msg[0] = "Error:项目编码空!";
                    context.Response.Write("Error:项目编码空!");
                    return;
                }
                if (Table.Length < 1)
                {
                    Msg[0] = "Error:表名空!";
                    context.Response.Write("Error:表名空!");
                    return;
                }
                if (Where.Length < 1)
                {
                    Msg[0] = "Error:条件空!";
                    context.Response.Write("Error:条件空!");
                    Log.Info("历史对比有:" + ds.Tables[0].Rows.Count.ToString() + "条.");
                    return;
                }
                param[0] = PatNo;
                param[1] = ItemNo;
                param[2] = Table;
                param[3] = " and "+ Where;

                Log.Info("历史对比参数：病历号:" + param[0] + ";项目编码:" + param[1] + ";表名称:" + param[2] + ";开始时间:" + param[3]);

                ds = ibv.GetReportValue(param);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Log.Info("历史对比有:" + ds.Tables[0].Rows.Count.ToString() + "条.");
                    //List<Model.ReportFormFull> ils = LT.GetListColumns<Model.ReportFormFull>(ds.Tables[0]);
                    List<History> ls = LT.GetListColumns<History>(ds.Tables[0]);
                    context.Response.Write(ZhiFang.BLL.Common.JsonHelp.Json<History>(ls));
                }
                else
                {
                    //new JavaScriptSerializer().Serialize(Msg)
                    Msg[0] = "";
                    context.Response.Write("Error:无对比记录!");
                    Log.Info("无对比记录!");
                }
                #endregion
            }

        }
        #region 打印
        private string[] PrintMergeHtml(SortedList ReportFormID, string title)
        {
            ZhiFang.Common.Log.Log.Info("这是PrintMergeHtml()方法：ReportPrint.ashx");
            ZhiFang.Common.Log.Log.Info(title.ToString() + "---------" + ReportFormID.GetByIndex(0).ToString() + "---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
            List<string> tmp;
            List<string> tmplist = new List<string>();
            for (int i = 0; i < ReportFormID.Count; i++)
            {
                if (ConfigHelper.GetConfigString("modelType").ToString() == "fr3")
                {
                    tmp = Printform_WeblisFr3.PrintMergeHtml(ReportFormID.GetByIndex(i).ToString(), (ZhiFang.Common.Dictionary.ReportFormTitle)Convert.ToInt32(title));
                }
                else
                {
                    tmp = Printform_Weblis.PrintMergeHtml(ReportFormID.GetByIndex(i).ToString(), (ZhiFang.Common.Dictionary.ReportFormTitle)Convert.ToInt32(title));

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
                tmphtml[i] = "../" + tmplist[i].Substring(tmplist[i].LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")), tmplist[i].Length - tmplist[i].LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")));
            }
            return tmphtml;
        }
        private string[] PrintMergeEnHtml(SortedList ReportFormID, string title)
        {
            ZhiFang.Common.Log.Log.Info("这是PrintMergeEnHtml()方法：ReportPrint.ashx");
            ZhiFang.Common.Log.Log.Info(title.ToString() + "E---------" + ReportFormID.GetByIndex(0).ToString() + "E---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
            List<string> tmp;
            List<string> tmplist = new List<string>();
            for (int i = 0; i < ReportFormID.Count; i++)
            {
                if (ConfigHelper.GetConfigString("modelType").ToString() == "fr3")
                {
                    ZhiFang.Common.Log.Log.Info("这是PrintMergeEnHtml()方法-fr3：ReportPrint.ashx");
                    tmp = Printform_WeblisFr3.PrintMergeHtml(ReportFormID.GetByIndex(i).ToString(), (ZhiFang.Common.Dictionary.ReportFormTitle)Convert.ToInt32(title));
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("这是PrintMergeEnHtml()方法-frx：ReportPrint.ashx");
                    tmp = Printform_Weblis.PrintMergeHtml(ReportFormID.GetByIndex(i).ToString(), (ZhiFang.Common.Dictionary.ReportFormTitle)Convert.ToInt32(title));
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
                tmphtml[i] = "../" + tmplist[i].Substring(tmplist[i].LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")), tmplist[i].Length - tmplist[i].LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")));
            }
            return tmphtml;
        }
        public string[] PrintHtml(string[] ReportFormID, string title)
        {
            ZhiFang.Common.Log.Log.Info("这是PrintHtml()方法：ReportPrint.ashx");
            List<string> tmp;
            List<string> tmplist = new List<string>();
            for (int i = 0; i < ReportFormID.Length; i++)
            {
                if (ConfigHelper.GetConfigString("modelType").ToString() == "fr3")
                {
                    ZhiFang.Common.Log.Log.Info("这是PrintHtml()方法-fr3：ReportPrint.ashx");
                    tmp = Printform_WeblisFr3.PrintHtml(ReportFormID[i].Split(';')[0], (ZhiFang.Common.Dictionary.ReportFormTitle)Convert.ToInt32(title));
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("这是PrintHtml()方法-frx：ReportPrint.ashx");
                    tmp = Printform_Weblis.PrintHtml(ReportFormID[i].Split(';')[0], (ZhiFang.Common.Dictionary.ReportFormTitle)Convert.ToInt32(title));
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
                tmphtml[i] = "../" + tmplist[i].Substring(tmplist[i].LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")), tmplist[i].Length - tmplist[i].LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")));
            }
            return tmphtml;
        }
        #endregion
        public List<T> GetList<T>(DataTable table)
        {
            List<T> list = new List<T>();
            T t = default(T);
            PropertyInfo[] propertypes = null;
            string tempName = string.Empty;
            foreach (DataRow row in table.Rows)
            {
                t = Activator.CreateInstance<T>();
                propertypes = t.GetType().GetProperties();
                foreach (PropertyInfo pro in propertypes)
                {
                    tempName = pro.Name;
                    if (table.Columns.Contains(tempName))
                    {
                        object value = row[tempName];
                        if (value.GetType() == typeof(System.DBNull))
                        {
                            value = null;
                        }
                        pro.SetValue(t, value, null);
                    }
                }
                list.Add(t);
            }
            return list;
        }

        public class xulie
        {
            public int Count { get; set; }
            public List<Model.ReportFormFull> Rows { get; set; }
        }
        public class History
        {
            public string ReceiveDate {get; set;}
            public string ReportValue { get; set; }

        }

        /// <summary>
        /// List分页
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ls"></param>
        /// <returns></returns>
        private static List<T> Pagination<T>(HttpContext context, List<T> ls)
        {

            int page = Convert.ToInt32(context.Request.QueryString["page"]);
            int limit = Convert.ToInt32(context.Request.QueryString["limit"]);
            //int page = 2;
            //int row = 3;
            ZhiFang.Common.Log.Log.Info("第:" + page + "页，显示:" + limit + "条");
            List<T> Result = new List<T>();
            int rowCount = page * limit;
            if (ls.Count >= rowCount)
            {
                for (int i = rowCount - limit; i < rowCount; i++)
                {
                    ZhiFang.Common.Log.Log.Info("1：" + ls[i].ToString());
                    Result.Add(ls[i]);
                }
            }
            else
            {
                if (ls.Count > rowCount - limit)
                {
                    for (int i = rowCount - limit; i < ls.Count; i++)
                    {
                        ZhiFang.Common.Log.Log.Info("2：" + ls[i].ToString());
                        Result.Add(ls[i]);
                    }
                }
            }
            return Result;
        }
        public class LimitPropsContractResolver : DefaultContractResolver
        {

            string[] props = null;

            public LimitPropsContractResolver(string[] props)
            {

                //指定要序列化属性的清单

                this.props = props;

            }

            //REF: http://james.newtonking.com/archive/2009/10/23/efficient-json-with-json-net-reducing-serialized-json-size.aspx
                
            protected override IList<JsonProperty> CreateProperties(Type type,

            MemberSerialization memberSerialization)
            {

                IList<JsonProperty> list =

                base.CreateProperties(type, memberSerialization);

                //只保留清单有列出的属性

                return list.Where(p => props.Contains(p.PropertyName)).ToList();

            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}