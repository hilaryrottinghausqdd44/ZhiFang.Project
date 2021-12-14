using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Web;
using ZhiFang.ReportFormQueryPrint.Common;
using ZhiFang.ReportFormQueryPrint.Model;
using ZhiFang.ReportFormQueryPrint.ServerContract;
using System.Xml;
using ZhiFang.ReportFormQueryPrint.Model.VO;
using ZhiFang.ReportFormQueryPrint.BLL.Print;
using ZhiFang.ReportFormQueryPrint.BLL;
using System.IO;
using System.Collections;
using ZhiFang.ReportFormQueryPrint.IDAL;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Speech.Synthesis;
using System.Configuration;
using Newtonsoft.Json.Linq;
using static ZhiFang.ReportFormQueryPrint.Common.Cookie;

namespace ZhiFang.ReportFormQueryPrint.ServiceWCF
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ReportFormService : IReportFormService
    {
        //public readonly IBPrintUseDeliph Printform_WeblisFr3 = BLLFactory<IBPrintUseDeliph>.GetBLL("PrintUseDeliph");
        //protected BALLReportForm ibv = new ALLReportForm();
        // ListTool LT = new ListTool();
        private readonly ZhiFang.ReportFormQueryPrint.BLL.BReportForm brf = new BLL.BReportForm();
        private readonly ZhiFang.ReportFormQueryPrint.BLL.BALLReportForm barf = new BLL.BALLReportForm();
        private readonly ZhiFang.ReportFormQueryPrint.BLL.BNRequestForm bnrf = new BLL.BNRequestForm();
        private readonly ZhiFang.ReportFormQueryPrint.BLL.BNRequestItem bnri = new BLL.BNRequestItem();
        private readonly ZhiFang.ReportFormQueryPrint.BLL.BReportItem bri = new BLL.BReportItem();
        private readonly ZhiFang.ReportFormQueryPrint.BLL.Print.BShowFrom bsf = new BLL.Print.BShowFrom();
        private readonly ZhiFang.ReportFormQueryPrint.BLL.Print.PrintReportForm bprf = new BLL.Print.PrintReportForm();
        private readonly ZhiFang.ReportFormQueryPrint.BLL.BRequestForm bqf = new BLL.BRequestForm();
        private readonly ZhiFang.ReportFormQueryPrint.BLL.BRequestItem bqi = new BLL.BRequestItem();
        private readonly ZhiFang.ReportFormQueryPrint.BLL.BRequestMarrow bqma = new BLL.BRequestMarrow();
        private readonly ZhiFang.ReportFormQueryPrint.BLL.BRequestMicro bqmi = new BLL.BRequestMicro();
        private readonly ZhiFang.ReportFormQueryPrint.BLL.Print.PrintRequestForm bprqf = new BLL.Print.PrintRequestForm();
        private readonly ZhiFang.ReportFormQueryPrint.BLL.Print.BShowRequestFrom bsrf = new BLL.Print.BShowRequestFrom();
        private readonly ZhiFang.ReportFormQueryPrint.BLL.BRFPReportFormPrintOperation BRFRFPO = new BLL.BRFPReportFormPrintOperation();
        private readonly ZhiFang.ReportFormQueryPrint.BLL.BSiteOperationRecords BSiteOperationRecords = new BLL.BSiteOperationRecords();
        public static string SCMsgVodioFilePath = "SCMsgVodioFile";
        //private readonly IBShowFrom showform = BLLFactory<IBShowFrom>.GetBLL("ShowFrom");
        //private readonly IBShowFrom showformFr3 = BLLFactory<IBShowFrom>.GetBLL("ShowFormUseDeliph");
        public BaseResultDataValue SelectReport(string Where, string fields, int page, int limit, string SerialNo)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                #region 如果查询间隔时间内的查询次数超过某个值则返回空数据
                int selectTimes = 1;

                if (SessionHelper.GetSessionObjectValue("startTime") == null)
                {
                    SessionHelper.SetSessionValue("startTime", DateTime.Now);
                }
                else
                {

                    //获取差值
                    DateTime startTime = (DateTime)SessionHelper.GetSessionObjectValue("startTime");
                    DateTime endTime = DateTime.Now;
                    TimeSpan ts1 = new TimeSpan(startTime.Ticks);
                    TimeSpan ts2 = new TimeSpan(endTime.Ticks);
                    TimeSpan ts3 = ts1.Subtract(ts2).Duration();
                    double dateDiff = ts3.TotalMilliseconds;
                    int selectReportMillSecond = 1000;
                    if (Common.ConfigHelper.GetConfigInt("SelectReportMillSecond") != null)
                    {
                        selectReportMillSecond = Common.ConfigHelper.GetConfigInt("SelectReportMillSecond").Value;
                    }
                    //小于配置时间则返回错误信息
                    if (dateDiff < selectReportMillSecond)
                    {
                        if (SessionHelper.GetSessionObjectValue("selectTimes") == null)
                        {
                            SessionHelper.SetSessionValue("selectTimes", 1);
                        }
                        else
                        {
                            selectTimes += Convert.ToInt32(SessionHelper.GetSessionValue("selectTimes"));
                            if (selectTimes >= 10)
                            {
                                brdv.ErrorInfo = "Error:查询数据过于频繁，请稍后查询";
                                brdv.success = false;
                                return brdv;
                            }
                            SessionHelper.SetSessionValue("selectTimes", selectTimes);
                        }

                    }
                    else
                    {
                        //更新session记录的查询时间
                        SessionHelper.SetSessionValue("startTime", null);
                        //更新session记录的查询次数
                        SessionHelper.SetSessionValue("selectTimes", null);
                    }
                }
                #endregion
                #region 查询报告
                bool v = SqlInjectHelper.CheckKeyWord(Where);
                if (v)
                {
                    brdv.ErrorInfo = "Error:存在SQL注入请注意传入条件!";
                    brdv.success = false;
                    return brdv;
                }
                string urlWhere = Where.Replace("\"", "'");

                string urlModel = fields;
                int urlPage = page;
                int urlLimit = limit;
                //ZhiFang.Common.Log.Log.Debug("urlType_urlModel" + urlModel);

                if (string.IsNullOrEmpty(urlWhere))
                {
                    urlWhere = " 1=1 ";
                }

                if (urlWhere.Length < 4)
                {
                    brdv.ErrorInfo = "Error:请选择查询条件!";
                    brdv.success = false;
                    return brdv;
                }
                //报告发布程序的状态字段
                //urlWhere += " and resultsend='1' ";

                if (urlModel.Length < 1)
                {
                    brdv.ErrorInfo = "Error:请选择要查询的数据!";
                    brdv.success = false;
                    return brdv;
                }

                DataSet ds = new DataSet();

                //string countwhere = urlWhere.ToUpper();
                string countwhere = urlWhere;
                if (countwhere.IndexOf("ORDER") >= 0)
                {
                    countwhere = countwhere.Substring(0, countwhere.IndexOf("ORDER"));
                }
                // if (countwhere.IndexOf("order") >= 0)
                //{ countwhere = countwhere.Substring(0, countwhere.IndexOf("order"));         }

                int dsCount = 0;
                if (string.IsNullOrWhiteSpace(SerialNo))
                {
                    dsCount = barf.GetCountFormFull(countwhere);
                }
                else
                {
                    dsCount = bnri.GetCountFormFull(SerialNo + "and SamplingGroupno<>'' ");
                }



                //ZhiFang.Common.Log.Log.Debug("urlType_dsCount" + dsCount);
                int _reportformlimit = 5000;
                if (Common.ConfigHelper.GetConfigInt("SearchReportFormLimit") != null)
                {
                    _reportformlimit = Common.ConfigHelper.GetConfigInt("SearchReportFormLimit").Value;
                }
                if (dsCount > _reportformlimit)
                {
                    brdv.ErrorInfo = "Error:" + dsCount + "数据超过" + _reportformlimit + "条,请从新选择查询条件!";
                    brdv.success = false;
                    return brdv;
                }

                if (dsCount <= 0)
                {
                    brdv.ResultDataValue = "{\"total\":" + dsCount + ",\"rows\":[]}"; ;
                    brdv.success = true;
                    return brdv;
                }

                if (string.IsNullOrWhiteSpace(SerialNo))
                {
                    ds = barf.GetList_FormFull(urlModel, urlWhere);
                    #region 威海市自助打印定制
                    //自助打印次数限制，根据6.6库的printtimes限制，如果6.6库的printtimes大于1，也不让打印
                    //if (urlWhere.Contains("isnull(clientprint,0)"))
                    //{
                    //    ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66.ALLReportForm aLLReportForm = new DAL.MSSQL.MSSQL66.ALLReportForm();
                    //    IDReportForm dal = Factory.DalFactory<IDReportForm>.GetDal("ReportForm", "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66", ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("LISConnectionString"));
                    //    DataSet dataSet66 = dal.GetListTopByWhereAndSort(1000, urlWhere, "");

                    //    if (dataSet66 != null && dataSet66.Tables.Count > 0 && dataSet66.Tables[0].Rows.Count > 0)
                    //    {
                    //        for (int i = 0; i < dataSet66.Tables[0].Rows.Count; i++)
                    //        {
                    //            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    //            {
                    //                if (ds.Tables[0].Rows[j]["ReceiveDate"].ToString() == dataSet66.Tables[0].Rows[i]["ReceiveDate"].ToString() && ds.Tables[0].Rows[j]["SectionNo"].ToString() == dataSet66.Tables[0].Rows[i]["SectionNo"].ToString() && ds.Tables[0].Rows[j]["TestTypeNo"].ToString() == dataSet66.Tables[0].Rows[i]["TestTypeNo"].ToString() && ds.Tables[0].Rows[j]["SampleNo"].ToString() == dataSet66.Tables[0].Rows[i]["SampleNo"].ToString())
                    //                {
                    //                    if (dataSet66.Tables[0].Rows[i]["PrintTimes"] != null && !string.IsNullOrEmpty(dataSet66.Tables[0].Rows[i]["PrintTimes"].ToString()) && int.Parse(dataSet66.Tables[0].Rows[i]["PrintTimes"].ToString()) > 0)
                    //                    {
                    //                        if (ds.Tables[0].Rows[j]["PrintTimes"] == null || string.IsNullOrEmpty(ds.Tables[0].Rows[j]["PrintTimes"].ToString()) || (int.Parse(dataSet66.Tables[0].Rows[i]["PrintTimes"].ToString()) > int.Parse(ds.Tables[0].Rows[j]["PrintTimes"].ToString())))
                    //                        {
                    //                            ReportFormFull formfull = new ReportFormFull();
                    //                            formfull.ReportPublicationID = long.Parse(ds.Tables[0].Rows[j]["ReportFormID"].ToString());
                    //                            formfull.PrintTimes = int.Parse(dataSet66.Tables[0].Rows[i]["PrintTimes"].ToString());
                    //                            formfull.ClientPrint = int.Parse(dataSet66.Tables[0].Rows[i]["PrintTimes"].ToString());
                    //                            brf.UpdateReportFormFull(formfull);
                    //                        }

                    //                    }
                    //                    break;
                    //                }
                    //            }
                    //        }
                    //    }
                    //    ds = barf.GetList_FormFull(urlModel, urlWhere);
                    //}
                    #endregion
                }
                else
                {
                    ds = bnri.GetList_FormFull("SerialNo", SerialNo + "and SamplingGroupno<>'' ");
                    string reportFormWhere = "serialno in (";
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        reportFormWhere += "'" + item[0].ToString() + "',";
                    }
                    reportFormWhere = reportFormWhere.Substring(0, reportFormWhere.Length - 1) + ")";
                    ds = barf.GetList_FormFull(urlModel, reportFormWhere + " and " + Where);
                }


                //ZhiFang.Common.Log.Log.Debug("urlType_dsCount_ds" + ds.Tables[0].Rows.Count);

                //List<Model.ReportFormFull> ils = GetList<Model.ReportFormFull>(ds.Tables[0]);
                List<ReportFormVO> ils = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<ReportFormVO>(ds.Tables[0]);
                //ZhiFang.Common.Log.Log.Debug("urlType_dsCount_ds_ils" + ils.Count);
                //List<Model.ReportFormFull> Result = Pagination<Model.ReportFormFull>(context, ils);
                //for(int i=0;i<d)


                List<ReportFormVO> Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.Pagination<ReportFormVO>(Convert.ToInt32(urlPage), Convert.ToInt32(urlLimit), ils);

                //ZhiFang.Common.Log.Log.Debug("urlType_dsCount_ds_ils_Result" + Result.Count);
                var settings = new JsonSerializerSettings();

                string aa = JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented, settings);
                //ZhiFang.Common.Log.Log.Debug("urlType_dsCount_ds_ils_Result_aa" + aa.Length);

                brdv.ResultDataValue = "{\"total\":" + ils.Count + ",\"rows\":" + aa + "}";
                brdv.success = true;
                return brdv;
                #endregion
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("class:ReportFormService,方法：SelectReport");
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                brdv.ErrorInfo = "查询条件错误，请查看查询条件格式是否正确";
                brdv.success = false;
                return brdv;
            }

        }

        public BaseResultDataValue SelectReportSort(string Where, string fields, int page, int limit, string SerialNo, string sort)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                if (!String.IsNullOrWhiteSpace(sort))
                {
                    //sort--[{"property":"CNAME","direction":"DESC"}],去掉不相关符号
                    JArray tempJArray = JArray.Parse(sort);
                    List<string> SortList = new List<string>();
                    string SortStr = "";

                    string returnStr = "";
                    foreach (var tempObject in tempJArray)
                    {
                        SortStr = tempObject["property"].ToString() + " " + tempObject["direction"].ToString().ToUpper();
                        // int tempIndex = SortStr.IndexOf(".");
                        // FirstStr = SortStr.Substring(0, tempIndex);
                        // StringBuilder tempStringBuilder = new StringBuilder(SortStr);
                        // tempStringBuilder.Replace(FirstStr, FirstStr.ToLower(), 0, tempIndex);
                        SortList.Add(SortStr);
                    }
                    if (SortList.Count > 0)
                    {
                        returnStr = string.Join(",", SortList.ToArray());
                    }

                    string result = sort.Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "").Replace("\"", "");

                    //returnStr格式-->"CNAME DESC,TestDate ASC"

                    string sortWhere = Where;
                    //Where有setting页面设置字段排序和无设置排序分别处理
                    if (sortWhere.IndexOf("BY") >= 0)
                    {
                        string sortWhere1 = sortWhere.Substring(0, sortWhere.IndexOf("BY") + 3);
                        string sortWhere2 = sortWhere.Substring(sortWhere.IndexOf("BY") + 3);
                        //最终条件=初始条件截止到order by+点击列名的排序+setting页面设置的字段排序
                        Where = sortWhere1 + returnStr + "," + sortWhere2;
                    }
                    else
                    {
                        Where += " ORDER BY " + returnStr;
                    }
                }
                brdv = SelectReport(Where, fields, page, limit, SerialNo);
                return brdv;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("ZhiFang.ReportFormQueryPrint.ServiceWCF.ReportFormService,方法：SelectReportSort");
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                brdv.ErrorInfo = "查询条件错误，请查看查询条件格式是否正确";
                brdv.success = false;
                return brdv;
            }

        }
        public BaseResultDataValue PreviewReport(string ReportFormID, string SectionNo, string SectionType, string ModelType, string sortFields)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            try
            {
                #region 中日定制
                bool isBloodGlucose= ZhongRiBloodGlucoseTesting(ReportFormID, SectionType,out brdv,0, SectionNo,ModelType, sortFields, "preview");
                if (isBloodGlucose)
                {
                    return brdv;
                }
                #endregion
                brdv = this.PreviewReportExtPageName(ReportFormID, SectionNo, SectionType, ModelType, null, sortFields,"");
                return brdv;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("PreviewReport出错：" + e.ToString());
                brdv.ErrorInfo = "PreviewReport出错：" + e.ToString();
                brdv.success = false;
                return brdv;
            }
        }

        public BaseResultDataValue PreviewReportExtPageName(string ReportFormID, string SectionNo, string SectionType, string ModelType, string PageName, string sortFields,string SerialNos)
        {
            ZhiFang.Common.Log.Log.Debug("PreviewReportExtPageName.ReportFormID:" + ReportFormID + ", SectionNo:" + SectionNo + ", SectionType:" + SectionType + ", ModelType:" + ModelType + ",PageName:" + PageName);
            BaseResultDataValue brdv = new BaseResultDataValue();

            try
            {
                string urlReportFormID = ReportFormID;
                string urlSectionNo = SectionNo;
                int urlSectionType = int.Parse(SectionType);
                if (int.Parse(SectionType.Substring(0, 1)) > 5)
                {
                    ModelType = "pdf";
                }
                string ulModelType = ModelType;

                string urlPageName = "TechnicianPrint1.aspx";
                if (PageName != null && PageName.Trim() != "")
                    urlPageName = PageName;

                string urlSorg = "0";
                string urlShowType = "0";
                string LPreview = "<HTML>报告预览</HTML>";
                
                int st = Convert.ToInt32(urlShowType);
                long sn = Convert.ToInt64(urlSectionNo);
                string tmp = "";
                //if (ulModelType == "report")
                //{
                //    ZhiFang.Common.Log.Log.Debug("Preview_report");
                //    //去数据库找模版
                //    var l = bprf.CreatReportFormFiles(new List<string>() { urlReportFormID }, ReportFormTitle.center, ReportFormFileType.JPEG, SectionType);
                //    if (l.Count > 0)
                //    {
                //        tmp = l[0];
                //    }
                //}
                if (ulModelType == "result")
                {
                    ZhiFang.Common.Log.Log.Debug("Preview_result");
                    List<string> sortFieldsList = null;
                    if (!string.IsNullOrEmpty(sortFields))
                    {
                        sortFieldsList = new List<string>(sortFields.Split(';'));
                    }
                    //去XmlConfig找模版
                    if (string.IsNullOrEmpty(SerialNos))
                    {
                        tmp = bsf.ShowReportFormResult(urlReportFormID, sn, urlPageName, st, urlSectionType, sortFieldsList);
                    }
                    else
                    {
                        tmp = bsf.ShowReportFormResultZhongri(urlReportFormID, sn, urlPageName, st, urlSectionType, sortFieldsList, SerialNos);
                    }
                    
                }

                if (ulModelType == "pdf")
                {
                    ZhiFang.Common.Log.Log.Debug("Preview_pdf");
                    //返回pdf文件
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<iframe width=\"100%\" height=\"100%\" ");
                    string dir = urlReportFormID.Split(new char[] { ';' })[0];
                    sb.Append("src=\"ReportFiles/" + dir + "/" + urlReportFormID + ".pdf\" ");
                    sb.Append("height=\"100%\" width=\"100%\" frameborder=\"0\" ");
                    sb.Append("style=\"overflow:hidden;overflow-x:hidden;overflow-y:hidden;height:100%;width:100%;position:absolute;top:0px;left:0px;right:0px;bottom:0px\"> ");
                    sb.Append("</iframe>");
                    tmp = sb.ToString();
                }

                if (tmp.IndexOf("<html") >= 0)
                {
                    tmp = tmp.Substring(tmp.IndexOf("<html"), tmp.Length - tmp.IndexOf("<html"));
                }
                tmp = tmp.Replace("\r\n ", " ");
                brdv.success = true;
                brdv.ResultDataValue = tmp;
                return brdv;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("PreviewReportExtPageName出错：" + e.ToString());
                brdv.ErrorInfo = "PreviewReportExtPageName出错：" + e.ToString();
                brdv.success = false;
                return brdv;
            }
        }

        public BaseResultDataValue PrintReport(string ReportFormID, string ReportFormTitle, string PrintType)
        {
            throw new NotImplementedException();
        }

        public BaseResultDataValue ResultHistory(string PatNo, string ItemNo, string Table, string Where)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                DataTable dt = new DataTable();
                string[] param = new string[4];

                //string[] url = context.Request.QueryString["primary"].Split(';');
                if (PatNo.Length < 1)
                {
                    brdv.ErrorInfo = "Error:病历号空!";
                    brdv.success = false;
                    return brdv;
                }
                if (ItemNo.Length < 1)
                {
                    brdv.ErrorInfo = "Error:项目编码空!";
                    brdv.success = false;
                    return brdv;
                }
                if (Table.Length < 1)
                {
                    brdv.ErrorInfo = "Error:表名空!";
                    brdv.success = false;
                    return brdv;
                }
                if (Where.Length < 1)
                {
                    brdv.ErrorInfo = "Error:条件空!";
                    brdv.success = false;
                    return brdv;
                }
                param[0] = PatNo;
                param[1] = ItemNo;
                param[2] = Table;
                param[3] = " and " + Where;

                //Log.Info("历史对比参数：病历号:" + param[0] + ";项目编码:" + param[1] + ";表名称:" + param[2] + ";开始时间:" + param[3]);

                dt = barf.GetReportValue(param, null);

                if (dt != null && dt.Rows.Count > 0)
                {
                    //Log.Info("历史对比有:" + ds.Tables[0].Rows.Count.ToString() + "条.");
                    //List<Model.ReportFormFull> ils = LT.GetListColumns<Model.ReportFormFull>(ds.Tables[0]);
                    List<HistoryVO> ls = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<HistoryVO>(dt);
                    var settings = new JsonSerializerSettings();

                    string aa = JsonConvert.SerializeObject(ls, Newtonsoft.Json.Formatting.Indented, settings);
                    brdv.ResultDataValue = aa;
                    brdv.success = true;
                }
                else
                {
                    brdv.ErrorInfo = "Error:无对比记录!";
                    brdv.success = false;
                }
                return brdv;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                brdv.ErrorInfo = "ResultHistory:" + ex.ToString();
                brdv.success = false;
                return brdv;
            }
        }

        public BaseResult ReportFormAddPrintTimes(string reportformidstr)
        {
            Model.BaseResult br = new Model.BaseResult();
            //获得的客户端ip
            OperationContext context = OperationContext.Current;
            MessageProperties properties = context.IncomingMessageProperties;
            RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
            string ip = endpoint.Address.ToString();
            string uluserCName = Common.Cookie.CookieHelper.Read("ULUserCName");
            try
            {
                string[] reportformlist = reportformidstr.Split(',');
                bool Ms66Flag = true;
                bool flag = true;
                if (ConfigHelper.GetConfigString("IsLisAddPrintTime").Equals("1")) //向Lis添加打印次数
                {
                    DataTable dsreportform = new DataTable();
                    dsreportform = brf.GetReportFormList(reportformlist);
                    if (dsreportform == null || dsreportform.Rows.Count <= 0)
                    {
                        br.success = false;
                        br.ErrorInfo = "LIS中没有找到报告单";
                    }
                    List<string> reportformidlist66 = new List<string>();
                    for (int i = 0; i < dsreportform.Rows.Count; i++)
                    {
                        reportformidlist66.Add(dsreportform.Rows[i]["Receivedate"].ToString().Trim() + ";" + dsreportform.Rows[i]["SectionNo"].ToString().Trim() + ";" + dsreportform.Rows[i]["TestTypeNo"].ToString().Trim() + ";" + dsreportform.Rows[i]["SampleNo"].ToString().Trim());
                    }
                    IDReportForm dal = Factory.DalFactory<IDReportForm>.GetDal("ReportForm", "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66", ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("LISConnectionString"));//LIS库打标记
                    Ms66Flag = dal.UpdatePrintTimes(reportformidlist66.ToArray(), uluserCName);
                    ZhiFang.Common.Log.Log.Debug("IP:" + ip + "向Lis添加打印次数:" + Ms66Flag);
                    Ms66Flag = dal.UpdateClientPrintTimes(reportformidlist66.ToArray());//外围打印标记
                    ZhiFang.Common.Log.Log.Debug("IP:" + ip + "向Lis添加外围打印次数:" + Ms66Flag);
                }

                flag = brf.UpdatePrintTimes(reportformlist, uluserCName);
                ZhiFang.Common.Log.Log.Debug("IP:" + ip + "添加打印次数:reportformID:" + reportformidstr);
                flag = brf.UpdateClientPrintTimes(reportformlist);
                ZhiFang.Common.Log.Log.Debug("IP:" + ip + "添加外围打印次数:reportformID:" + reportformidstr);

                if (Ms66Flag && flag)
                {
                    br.success = true;
                }
                else
                {
                    br.success = false;
                }
            }
            catch (Exception e)
            {
                br.success = false;
                br.ErrorInfo = e.ToString();
            }
            return br;
        }
        public BaseResultDataValue BlendPDF(string fileList, string pageType)
        {

            string path = HttpContext.Current.Server.MapPath(".").Replace("ServiceWCF", "");

            string[] file = fileList.Split(',');
            string[] Type = pageType.Split(',');
            for (int i = 0; i < file.Length; i++)
            {
                file[i] = path + file[i].ToString().Replace("/", "\\");
            }
            BaseResultDataValue brdv = new BaseResultDataValue();

            try
            {
                string mergePath = path + "Merge\\" + DateTime.Now.ToString("yyyy-MM-dd");
                #region 删除10天前的临时报告文件
                //if (System.IO.Directory.Exists(path + "Merge\\" + DateTime.Now.AddDays(-90).ToString("yyyy-MM-dd")))
                //{
                //    System.IO.Directory.Delete(path + "Merge\\" + DateTime.Now.AddDays(-90).ToString("yyyy-MM-dd"), true);
                //}
                #endregion
                if (!System.IO.Directory.Exists(mergePath))
                {
                    System.IO.Directory.CreateDirectory(mergePath);
                }

                string pdfName = Guid.NewGuid().ToString() + ".pdf";

                string outFile = mergePath + "\\" + pdfName;

                PDFMergeHelp.Blend(file, Type, outFile);

                brdv.ResultDataValue = "Merge/" + DateTime.Now.ToString("yyyy-MM-dd") + "/" + pdfName;
                brdv.success = true;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                brdv.ErrorInfo = "DobuleA5MergeA4PDFFiles:" + ex.ToString();
                brdv.success = false;
                return brdv;
            }
            return brdv;
        }
        public BaseResultDataValue DobuleA5MergeA4PDFFiles(string fileList)
        {

            string path = HttpContext.Current.Server.MapPath(".").Replace("ServiceWCF", "");

            string[] file = fileList.Split(',');

            for (int i = 0; i < file.Length; i++)
            {
                file[i] = path + file[i].ToString().Replace("/", "\\");
            }
            BaseResultDataValue brdv = new BaseResultDataValue();

            try
            {
                string mergePath = path + "Merge\\" + DateTime.Now.ToString("yyyy-MM-dd");
                #region 删除10天前的临时报告文件
                //if (System.IO.Directory.Exists(path + "Merge\\" + DateTime.Now.AddDays(-90).ToString("yyyy-MM-dd")))
                //{
                //    System.IO.Directory.Delete(path + "Merge\\" + DateTime.Now.AddDays(-90).ToString("yyyy-MM-dd"), true);
                //}
                #endregion
                if (!System.IO.Directory.Exists(mergePath))
                {
                    System.IO.Directory.CreateDirectory(mergePath);
                }

                string pdfName = Guid.NewGuid().ToString() + ".pdf";

                string outFile = mergePath + "\\" + pdfName;

                PDFMergeHelp.DobuleA5MergeA4PDFFiles(file, outFile);

                brdv.ResultDataValue = "Merge/" + DateTime.Now.ToString("yyyy-MM-dd") + "/" + pdfName;
                brdv.success = true;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                brdv.ErrorInfo = "DobuleA5MergeA4PDFFiles:" + ex.ToString();
                brdv.success = false;
                return brdv;
            }
            return brdv;
        }

        public BaseResultDataValue DobuleA5MergeA4PDFFilesPost(string fileList)
        {
            return this.DobuleA5MergeA4PDFFiles(fileList);
        }

        public BaseResultDataValue Dobule32KMerge16KPDFFiles(string fileList)
        {

            string path = HttpContext.Current.Server.MapPath(".").Replace("ServiceWCF", "");

            string[] file = fileList.Split(',');

            for (int i = 0; i < file.Length; i++)
            {
                file[i] = path + file[i].ToString().Replace("/", "\\");
            }
            BaseResultDataValue brdv = new BaseResultDataValue();

            try
            {

                string mergePath = path + "Merge\\" + DateTime.Now.ToString("yyyy-MM-dd");

                if (!System.IO.Directory.Exists(mergePath))
                {
                    System.IO.Directory.CreateDirectory(mergePath);
                }
                string pdfName = Guid.NewGuid().ToString() + ".pdf";

                string outFile = mergePath + "\\" + pdfName;

                PDFMergeHelp.Dobule32KMerge16KPDFFiles(file, outFile);

                brdv.ResultDataValue = "Merge/" + DateTime.Now.ToString("yyyy-MM-dd") + "/" + pdfName;
                brdv.success = true;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                brdv.ErrorInfo = "Dobule32KMerge16KPDFFiles:" + ex.ToString();
                brdv.success = false;
                return brdv;
            }
            return brdv;
        }

        public BaseResultDataValue Dobule32KMerge16KPDFFilesPost(string fileList)
        {
            return Dobule32KMerge16KPDFFiles(fileList);
        }

        public BaseResultDataValue MergeA4PDFFiles(string fileList)
        {

            string path = HttpContext.Current.Server.MapPath(".").Replace("ServiceWCF", "");

            string[] file = fileList.Split(',');

            for (int i = 0; i < file.Length; i++)
            {
                file[i] = path + file[i].ToString().Replace("/", "\\");
            }
            BaseResultDataValue brdv = new BaseResultDataValue();

            try
            {
                string mergePath = path + "Merge\\" + DateTime.Now.ToString("yyyy-MM-dd");
                #region 删除10天前的临时报告文件
                if (System.IO.Directory.Exists(path + "Merge\\" + DateTime.Now.AddDays(-90).ToString("yyyy-MM-dd")))
                {
                    System.IO.Directory.Delete(path + "Merge\\" + DateTime.Now.AddDays(-90).ToString("yyyy-MM-dd"), true);
                }
                #endregion
                if (!System.IO.Directory.Exists(mergePath))
                {
                    System.IO.Directory.CreateDirectory(mergePath);
                }

                string pdfName = Guid.NewGuid().ToString() + ".pdf";

                string outFile = mergePath + "\\" + pdfName;

                PDFMergeHelp.MergeA4PDFFiles(file, outFile);

                brdv.ResultDataValue = "Merge/" + DateTime.Now.ToString("yyyy-MM-dd") + "/" + pdfName;
                brdv.success = true;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                brdv.ErrorInfo = "MergeA4PDFFiles:" + ex.ToString();
                brdv.success = false;
                return brdv;
            }
            return brdv;
        }

        public BaseResultDataValue Merge16KPDFFiles(string fileList)
        {

            string path = HttpContext.Current.Server.MapPath(".").Replace("ServiceWCF", "");

            string[] file = fileList.Split(',');

            for (int i = 0; i < file.Length; i++)
            {
                file[i] = path + file[i].ToString().Replace("/", "\\");
            }
            BaseResultDataValue brdv = new BaseResultDataValue();

            try
            {
                string mergePath = path + "Merge\\" + DateTime.Now.ToString("yyyy-MM-dd");
                #region 删除10天前的临时报告文件
                if (System.IO.Directory.Exists(path + "Merge\\" + DateTime.Now.AddDays(-90).ToString("yyyy-MM-dd")))
                {
                    System.IO.Directory.Delete(path + "Merge\\" + DateTime.Now.AddDays(-90).ToString("yyyy-MM-dd"), true);
                }
                #endregion
                if (!System.IO.Directory.Exists(mergePath))
                {
                    System.IO.Directory.CreateDirectory(mergePath);
                }

                string pdfName = Guid.NewGuid().ToString() + ".pdf";

                string outFile = mergePath + "\\" + pdfName;

                PDFMergeHelp.Merge16KPDFFiles(file, outFile);

                brdv.ResultDataValue = "Merge/" + DateTime.Now.ToString("yyyy-MM-dd") + "/" + pdfName;
                brdv.success = true;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                brdv.ErrorInfo = "Merge16KPDFFiles:" + ex.ToString();
                brdv.success = false;
                return brdv;
            }
            return brdv;
        }

        public BaseResultDataValue GetReportFormPDFByReportFormID(string ReportFormID, string SectionNo, string SectionType, int flag)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = false;
            try
            {
                ZhiFang.Common.Log.Log.Debug("GetReportFormPDFByReportFormID.ReportFormID:" + ReportFormID + ", SectionNo:" + SectionNo + ", SectionType:" + SectionType + ", flag:" + flag + "");
                #region 中日定制-糖耐量
                bool isBloodGlucose=ZhongRiBloodGlucoseTesting(ReportFormID, SectionType,out brdv, flag, SectionNo,"","","pdf");
                if (isBloodGlucose)
                {
                    return brdv;
                }
                #endregion
                
                var listreportformfile = bprf.CreatReportFormFiles(new List<string>() { ReportFormID }, ReportFormTitle.center, ReportFormFileType.PDF, SectionType, flag);
                //var listreportformfile = bprf.TianJinXueYanSuoCreatReportFormFiles(new List<string>() { ReportFormID }, ReportFormTitle.center, ReportFormFileType.PDF, SectionType, flag);
                if (listreportformfile.Count > 0)
                {
                    brdv.ResultDataValue = ZhiFang.ReportFormQueryPrint.Common.JsonSerializer.JsonDotNetSerializerNoEnterSpace(listreportformfile[0]);
                    brdv.success = true;
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("GetReportFormPDFByReportFormID.异常:" + e.ToString() + ".ReportFormID:" + ReportFormID + ", SectionNo:" + SectionNo + ", SectionType:" + SectionType + ", flag:" + flag);
                brdv.success = false;
                brdv.ErrorInfo = "程序异常请查看系统日志！";
                //brdv.ErrorInfo = e.ToString();
            }
            ZhiFang.Common.Log.Log.Debug("GetReportFormPDFByReportFormID.ReportFormID:" + ReportFormID + ";返回报告路径结束");
            return brdv;
        }

        public BaseResultDataValue GetReportFormPDFByReportFormIDTest(string ReportFormID, string SectionNo, string SectionType, int flag, int nextIndex)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = false;
            int a = 0;
            try
            {
                brdv = bprf.CreatReportFormFilesTest(new List<string>() { ReportFormID }, ReportFormTitle.center, ReportFormFileType.PDF, SectionType, nextIndex, flag);
                //if (l.Count > 0)
                //{
                //    brdv.ResultDataValue = ZhiFang.ReportFormQueryPrint.Common.JsonSerializer.JsonDotNetSerializerNoEnterSpace(l[0]);
                //    brdv.success = true;
                //}
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("GetReportFormPDFByReportFormIDTest.异常:" + e.ToString() + ".ReportFormID:" + ReportFormID + ", SectionNo:" + SectionNo + ", SectionType:" + SectionType + ", flag:" + flag);
                brdv.success = false;
                //brdv.ErrorInfo = "程序异常请查看系统日志！";
                brdv.ErrorInfo = "GetReportFormPDFByReportFormIDTest.异常:" + e.ToString() + ".ReportFormID:" + ReportFormID + ", SectionNo:" + SectionNo + ", SectionType:" + SectionType + ", flag:" + flag;
            }
            return brdv;
        }


        /// <summary>
        /// 修改配置文件
        /// </summary>
        /// <param name="fileName">配置文件名称，带扩展名</param>
        /// <param name="outMergeFile"></param>

        public BaseResultDataValue LoadConfig(string fileName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "XmlConfig\\" + fileName;



            DataSet ds = new DataSet();
            ds.ReadXml(path);

            brdv.ResultDataValue = ZhiFang.ReportFormQueryPrint.Common.ToJson.Dataset2Json(ds);

            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            brdv.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeXmlNode(doc);
            brdv.success = true;
            return brdv;
        }

        /// <summary>
        /// 保存配置文件
        /// </summary>
        /// <param name="fileName">配置文件名称，带扩展名</param>
        /// <param name="outMergeFile"></param>
        public BaseResultDataValue SaveConfig(string fileName, string configStr)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            try
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Info("SaveConfig.操作者IP地址:" + ip);

                XmlDocument doc = Newtonsoft.Json.JsonConvert.DeserializeXmlNode(configStr);
                string path = System.AppDomain.CurrentDomain.BaseDirectory + "XmlConfig\\" + fileName;
                doc.Save(path);
                brdv.success = true;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = ex.Message;
            }

            return brdv;
        }

        /// <summary>
        /// 合并报告内容
        /// </summary>
        /// <param name="formNoList">ReportFormID列表，以逗号分隔</param>
        /// <param name="outMergeFile">合并后A4pdf路径</param>
        public BaseResultDataValue MergerReportContent(string formNoList)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            string[] str = formNoList.Split(',');

            DataTable dtItem = barf.GetReportItemList(str[0]).Clone();
            DataTable drMicro = barf.GetFromMicroItemList(str[0]).Clone();

            foreach (var a in str)
            {
                DataTable dti = barf.GetReportItemList(a);
                DataTable dtm = barf.GetFromMicroItemList(a);

                foreach (DataRow dr in dti.Rows)
                {
                    dtItem.Rows.Add(dr.ItemArray);
                }

                foreach (DataRow dr in dtm.Rows)
                {
                    drMicro.Rows.Add(dr.ItemArray);
                }
            }

            DataTable dtForm = barf.GetFromInfo(str[0]);

            string modulePath = System.AppDomain.CurrentDomain.BaseDirectory + "XSL\\合并.frx";
            PrintReportFormCommon prfc = new PrintReportFormCommon();
            prfc.CreatPdfContextFRX(dtForm, dtItem, drMicro, modulePath);
            return brdv;
        }

        /// <summary>
        /// 删除报告文件
        /// </summary>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        public BaseResultDataValue DeleteReportPDFFile(string startDate, string endDate)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = false;
            string path = System.AppDomain.CurrentDomain.BaseDirectory + SysContractPara.ReportFormFilePath;
            //单元测试用
            //string path = "F:/智方项目/报告项目/ZhiFang.ReportFormQueryPrint/ZhiFang.ReportFormQueryPrint/" + SysContractPara.ReportFormFilePath;
            DirectoryInfo di = new DirectoryInfo(@path);
            DirectoryInfo[] fileArry = di.GetDirectories();
            Hashtable hashTable = new Hashtable();
            List<Hashtable> ListHash = new List<Hashtable>();
            List<string> fileList = new List<string>();

            //删除下载报告时生成的PDf压缩包
            string rarpath = System.AppDomain.CurrentDomain.BaseDirectory + "RarReportFormFiles\\";
            DirectoryInfo rarpathi = new DirectoryInfo(@rarpath);
            FileInfo[] rarpathArry = rarpathi.GetFiles();
            try
            {
                foreach (var d in fileArry)
                {
                    if (d.Name != "sampleDetailedList")
                    {
                        var fileTime = DateTime.Parse(d.Name);
                        var start = DateTime.Parse(startDate);
                        var end = DateTime.Parse(endDate);
                        if (fileTime >= start && fileTime <= end)
                        {
                            fileList = new List<string>();
                            hashTable = new Hashtable();
                            hashTable.Add("Folder", d.Name);
                            brdv.success = true;
                            FileInfo[] fi = d.GetFiles();
                            foreach (var list in fi)
                            {
                                fileList.Add(list.Name);
                            }
                            hashTable.Add("file", fileList);
                            d.Delete(true);
                            ListHash.Add(hashTable);
                        }
                    }

                }

                foreach (var a in rarpathArry)
                {
                    a.Delete();
                }

                string ListHashd = ZhiFang.ReportFormQueryPrint.Common.JsonSerializer.JsonDotNetSerializerNoEnterSpace(ListHash);
                brdv.ResultDataValue = ListHashd;
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.ToString();
            }

            return brdv;
        }

        public BaseResultDataValue ResultMhistory(List<string> ReportFormID, string PatNo, List<string> SectionType, string Where)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BALLReportForm bal = new BALLReportForm();
            brdv.success = false;
            if (Where == "" || Where == null)
            {
                Where = " 1=1 ";
            }
            try
            {
                //List<string> relist = new List<string>();
                StringBuilder sb = new StringBuilder();
                for (var i = 0; i < ReportFormID.Count; i++)
                {
                    DataSet deptlist = bal.ResultMhistory(ReportFormID[i], PatNo, SectionType[i], Where);
                    string c = "";
                    if (i < ReportFormID.Count - 1)
                    {
                        c = ",";
                    }
                    for (var a = 0; a < deptlist.Tables.Count; a++)
                    {
                        var Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<ALLReportFromVo>(deptlist.Tables[a]);
                        var settings = new JsonSerializerSettings();
                        string aa = JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented, settings);
                        string b = "";
                        if (a < deptlist.Tables.Count - 1)
                        {
                            b = ",";
                        }
                        sb.Append(aa + b);
                    }
                    sb.Append(c);
                }
                brdv.ResultDataValue = "[" + sb.ToString() + "]";
                brdv.success = true;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("ResultMhistory.异常:" + e.ToString() + ".ReportFormID:" + ReportFormID);
                brdv.success = false;
                //brdv.ErrorInfo = "程序异常请查看系统日志！";
                brdv.ErrorInfo = e.ToString();
            }
            return brdv;
        }

        public BaseResultDataValue ResultDataTimeMhistory(string PatNO, string Where)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BALLReportForm bal = new BALLReportForm();
            brdv.success = false;
            List<string> ReportFormID = new List<string>();
            List<string> SectionType = new List<string>();
            try
            {
                var data = bal.ResultDataTimeMhistory(PatNO, Where);
                if (data.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow mDr in data.Tables[0].Rows)
                    {
                        ReportFormID.Add(mDr["ReportFormID"].ToString());
                        SectionType.Add(mDr["SecretType"].ToString());
                    }
                    brdv = ResultMhistory(ReportFormID, null, SectionType, null);
                }
                else
                {
                    brdv.success = false;
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("ResultDataTimeMhistory.异常:" + e.ToString() + ".PatNo:" + PatNO + ".Where:" + Where);
                brdv.success = false;
                //brdv.ErrorInfo = "程序异常请查看系统日志！";
                brdv.ErrorInfo = e.ToString();
            }
            return brdv;
        }

        public BaseResultDataValue GetMergReportFromByReportFormIdList(string ReportFormIdList, string SectionType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = false;
            try
            {
                if (SectionType == null || SectionType.Trim() == "")
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "SectionType参数为空！";
                    return brdv;
                }

                if (SectionType.Trim() != "1")
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "只支持普通生化类报告合并！";
                    return brdv;
                }

                if (ReportFormIdList == null || ReportFormIdList.Trim() == "")
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "ReportFormIdList参数为空！";
                    return brdv;
                }

                ZhiFang.Common.Log.Log.Debug("GetMergReportFromByReportFormIdList.ReportFormIdList:" + ReportFormIdList + ", SectionType:" + SectionType);
                List<ReportFormFilesVO> listreportformfile = bprf.GetMergReportFromByReportFormIdList(ReportFormIdList, SectionType);
                if (listreportformfile.Count > 0)
                {
                    brdv.ResultDataValue = ZhiFang.ReportFormQueryPrint.Common.JsonSerializer.JsonDotNetSerializerNoEnterSpace(listreportformfile[0]);
                    brdv.success = true;
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("GetMergReportFromByReportFormIdList.异常:" + e.ToString() + ".ReportFormIdList:" + ReportFormIdList + ", SectionType:" + SectionType);
                brdv.success = false;
                brdv.ErrorInfo = e.ToString();
            }
            return brdv;
        }

        /// <summary>
        /// 查询requestFrom报告单
        /// </summary>
        /// <param name="Where"></param>
        /// <param name="fields"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="SerialNo"></param>
        /// <returns></returns>
        public BaseResultDataValue SelectRequest(string Where, string fields, int page, int limit, string SerialNo)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                #region 查询报告
                string urlWhere = Where.Replace("\"", "'");

                string urlModel = fields;
                int urlPage = page;
                int urlLimit = limit;
                //ZhiFang.Common.Log.Log.Debug("urlType_urlModel" + urlModel);

                if (string.IsNullOrEmpty(urlWhere))
                {
                    urlWhere = " 1=1 ";
                }

                if (urlWhere.Length < 4)
                {
                    brdv.ErrorInfo = "Error:请选择查询条件!";
                    brdv.success = false;
                    return brdv;
                }
                //报告发布程序的状态字段
                //urlWhere += " and resultsend='1' ";

                if (urlModel.Length < 1)
                {
                    brdv.ErrorInfo = "Error:请选择要查询的数据!";
                    brdv.success = false;
                    return brdv;
                }

                DataSet ds = new DataSet();

                string countwhere = urlWhere.ToUpper();
                //string countwhere = urlWhere;
                if (countwhere.IndexOf("ORDER") >= 0)
                {
                    countwhere = countwhere.Substring(0, countwhere.IndexOf("ORDER"));
                }
                // if (countwhere.IndexOf("order") >= 0)
                //{ countwhere = countwhere.Substring(0, countwhere.IndexOf("order"));         }

                int dsCount = 0;
                if (string.IsNullOrWhiteSpace(SerialNo))
                {
                    dsCount = bqf.GetCountFormFull(countwhere);
                }
                else
                {
                    dsCount = bnri.GetCountFormFull(SerialNo + "and SamplingGroupno<>'' ");
                }
                int _reportformlimit = 5000;
                if (Common.ConfigHelper.GetConfigInt("SearchReportFormLimit") != null)
                {
                    _reportformlimit = Common.ConfigHelper.GetConfigInt("SearchReportFormLimit").Value;
                }
                if (dsCount > _reportformlimit)
                {
                    brdv.ErrorInfo = "Error:" + dsCount + "数据超过" + _reportformlimit + "条,请从新选择查询条件!";
                    brdv.success = false;
                    return brdv;
                }
                if (dsCount <= 0)
                {
                    brdv.ResultDataValue = "{\"total\":" + dsCount + ",\"rows\":[]}"; ;
                    brdv.success = true;
                    return brdv;
                }
                if (string.IsNullOrWhiteSpace(SerialNo))
                {
                    ds = bqf.GetList_FormFull(urlModel, urlWhere);
                }
                else
                {
                    ds = bnri.GetList_FormFull("SerialNo", SerialNo + "and SamplingGroupno<>'' ");
                    string reportFormWhere = "serialno in (";
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        reportFormWhere += "'" + item[0].ToString() + "',";
                    }
                    reportFormWhere = reportFormWhere.Substring(0, reportFormWhere.Length - 1) + ")";
                    ds = bqf.GetList_FormFull(urlModel, reportFormWhere + " and " + Where);
                }
                List<RequestFormVO> ils = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<RequestFormVO>(ds.Tables[0]);

                List<RequestFormVO> Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.Pagination<RequestFormVO>(Convert.ToInt32(urlPage), Convert.ToInt32(urlLimit), ils);

                var settings = new JsonSerializerSettings();
                string aa = JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented, settings);
                brdv.ResultDataValue = "{\"total\":" + ils.Count + ",\"rows\":" + aa + "}";
                brdv.success = true;
                return brdv;
                #endregion
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                brdv.ErrorInfo = "SelectRequestOrReport:" + ex.ToString();
                brdv.success = false;
                return brdv;
            }

        }

        public BaseResultDataValue SelectRequestSort(string Where, string fields, int page, int limit, string SerialNo, string sort)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                if (!String.IsNullOrWhiteSpace(sort))
                {
                    //sort--[{"property":"CNAME","direction":"DESC"}],去掉不相关符号
                    string result = sort.Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "").Replace("\"", "");
                    string finalColumnSort = "";
                    //finalColumnSort格式-->"CNAME DESC"
                    string[] string1 = result.Split(',');
                    for (int i = 0; i < string1.Length; i++)
                    {
                        string[] string2 = string1[i].Split(':');
                        finalColumnSort += string2[1] + " ";
                    }
                    string sortWhere = Where;
                    //Where有setting页面设置字段排序和无设置排序分别处理
                    if (sortWhere.IndexOf("BY") >= 0)
                    {
                        string sortWhere1 = sortWhere.Substring(0, sortWhere.IndexOf("BY") + 3);
                        string sortWhere2 = sortWhere.Substring(sortWhere.IndexOf("BY") + 3);
                        //最终条件=初始条件截止到order by+点击列名的排序+setting页面设置的字段排序
                        Where = sortWhere1 + finalColumnSort + "," + sortWhere2;
                    }
                    else
                    {
                        Where += " ORDER BY " + finalColumnSort;
                    }
                }
                brdv = SelectRequest(Where, fields, page, limit, SerialNo);
                return brdv;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("ZhiFang.ReportFormQueryPrint.ServiceWCF.ReportFormService,方法：SelectRequestSort");
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                brdv.ErrorInfo = "查询条件错误，请查看查询条件格式是否正确";
                brdv.success = false;
                return brdv;
            }

        }

        public BaseResultDataValue GetRequestFormPDFByReportFormID(string ReportFormID, string SectionNo, string SectionType, int flag)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = false;
            try
            {
                ZhiFang.Common.Log.Log.Debug("GetRequestFormPDFByReportFormID.ReportFormID:" + ReportFormID + ", SectionNo:" + SectionNo + ", SectionType:" + SectionType + ", flag:" + flag + "");
                var listreportformfile = bprqf.CreatRequestFormFiles(new List<string>() { ReportFormID }, ReportFormTitle.center, ReportFormFileType.PDF, SectionType, 1);
                if (listreportformfile.Count > 0)
                {
                    brdv.ResultDataValue = ZhiFang.ReportFormQueryPrint.Common.JsonSerializer.JsonDotNetSerializerNoEnterSpace(listreportformfile[0]);
                    brdv.success = true;
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("GetRequestFormPDFByReportFormID.异常:" + e.ToString() + ".ReportFormID:" + ReportFormID + ", SectionNo:" + SectionNo + ", SectionType:" + SectionType + ", flag:" + flag);
                brdv.success = false;
                //brdv.ErrorInfo = "程序异常请查看系统日志！";
                brdv.ErrorInfo = e.ToString();
            }
            return brdv;
        }
        public BaseResultDataValue PreviewRequest(string ReportFormID, string SectionNo, string SectionType, string ModelType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            try
            {
                brdv = this.PreviewRequestExtPageName(ReportFormID, SectionNo, SectionType, ModelType, null);
                return brdv;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("PreviewRequest出错：" + e.ToString());
                brdv.ErrorInfo = "PreviewRequest出错：" + e.ToString();
                brdv.success = false;
                return brdv;
            }
        }

        public BaseResultDataValue PreviewRequestExtPageName(string ReportFormID, string SectionNo, string SectionType, string ModelType, string PageName)
        {
            ZhiFang.Common.Log.Log.Debug("PreviewRepquestExtPageName.ReportFormID:" + ReportFormID + ", SectionNo:" + SectionNo + ", SectionType:" + SectionType + ", ModelType:" + ModelType + ",PageName:" + PageName);
            BaseResultDataValue brdv = new BaseResultDataValue();

            try
            {
                string urlReportFormID = ReportFormID;
                string urlSectionNo = SectionNo;
                int urlSectionType = int.Parse(SectionType);
                if (int.Parse(SectionType.Substring(0, 1)) > 5)
                {
                    ModelType = "pdf";
                }
                string ulModelType = ModelType;

                string urlPageName = "TechnicianPrint1.aspx";
                if (PageName != null && PageName.Trim() != "")
                    urlPageName = PageName;

                string urlSorg = "0";
                string urlShowType = "0";
                string LPreview = "<HTML>报告预览</HTML>";

                int st = Convert.ToInt32(urlShowType);
                int sn = Convert.ToInt32(urlSectionNo);
                string tmp = "";
                //if (ulModelType == "report")
                //{
                //    ZhiFang.Common.Log.Log.Debug("Preview_report");
                //    //去数据库找模版
                //    var l = bprf.CreatReportFormFiles(new List<string>() { urlReportFormID }, ReportFormTitle.center, ReportFormFileType.JPEG, SectionType);
                //    if (l.Count > 0)
                //    {
                //        tmp = l[0];
                //    }
                //}
                if (ulModelType == "result")
                {
                    ZhiFang.Common.Log.Log.Debug("Preview_result");
                    //去XmlConfig找模版
                    tmp = bsrf.ShowRequestFormResult(urlReportFormID, sn, urlPageName, st, urlSectionType);
                }

                if (ulModelType == "pdf")
                {
                    ZhiFang.Common.Log.Log.Debug("Preview_pdf");
                    //返回pdf文件
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<iframe width=\"100%\" height=\"100%\" ");
                    string dir = urlReportFormID.Split(new char[] { ';' })[0];
                    sb.Append("src=\"ReportFiles/" + dir + "/" + urlReportFormID + ".pdf\" ");
                    sb.Append("height=\"100%\" width=\"100%\" frameborder=\"0\" ");
                    sb.Append("style=\"overflow:hidden;overflow-x:hidden;overflow-y:hidden;height:100%;width:100%;position:absolute;top:0px;left:0px;right:0px;bottom:0px\"> ");
                    sb.Append("</iframe>");
                    tmp = sb.ToString();
                }

                if (tmp.IndexOf("<html") >= 0)
                {
                    tmp = tmp.Substring(tmp.IndexOf("<html"), tmp.Length - tmp.IndexOf("<html"));
                }
                tmp = tmp.Replace("\r\n ", " ");
                brdv.success = true;
                brdv.ResultDataValue = tmp;
                return brdv;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("PreviewRepquestExtPageName出错：" + e.ToString());
                brdv.ErrorInfo = "PreviewRepquestExtPageName出错：" + e.ToString();
                brdv.success = false;
                return brdv;
            }
        }

        public BaseResultDataValue RequestIsPrintNullValues(string ReportFormID, string SectionType, string QueryType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BALLReportForm bal = new BALLReportForm();
            BRequestForm brqf = new BRequestForm();
            brdv.success = false;

            try
            {
                bool isok = false;
                if (QueryType == "REQUEST")
                {
                    isok = brqf.RequestIsPrintNullValues(ReportFormID, SectionType);
                }
                else
                {
                    isok = bal.ReportIsPrintNullValues(ReportFormID, SectionType);
                }

                brdv.success = isok;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("ReuqestIsPrintNullValues.异常:" + e.ToString() + ".ReportFormID:" + ReportFormID);
                brdv.success = false;
                //brdv.ErrorInfo = "程序异常请查看系统日志！";
                brdv.ErrorInfo = e.ToString();
            }
            return brdv;
        }

        public BaseResult RequestFormAddPrintTimes(string reportformidstr)
        {
            Model.BaseResult br = new Model.BaseResult();
            //获得的客户端ip
            OperationContext context = OperationContext.Current;
            MessageProperties properties = context.IncomingMessageProperties;
            RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
            string ip = endpoint.Address.ToString();
            try
            {
                string[] reportformlist = reportformidstr.Split(',');
                bool Ms66Flag = true;
                bool flag = true;
                if (ConfigHelper.GetConfigString("IsLisAddPrintTime").Equals("1")) //向Lis添加打印次数
                {
                    DataTable dsreportform = new DataTable();
                    dsreportform = brf.GetReportFormList(reportformlist);
                    if (dsreportform == null || dsreportform.Rows.Count <= 0)
                    {
                        br.success = false;
                        br.ErrorInfo = "LIS中没有找到报告单";
                    }
                    List<string> reportformidlist66 = new List<string>();
                    for (int i = 0; i < dsreportform.Rows.Count; i++)
                    {
                        reportformidlist66.Add(dsreportform.Rows[i]["Receivedate"].ToString().Trim() + ";" + dsreportform.Rows[i]["SectionNo"].ToString().Trim() + ";" + dsreportform.Rows[i]["TestTypeNo"].ToString().Trim() + ";" + dsreportform.Rows[i]["SampleNo"].ToString().Trim());
                    }
                    IDRequestForm dal = Factory.DalFactory<IDRequestForm>.GetDal("RequestForm", "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66", ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("LISConnectionString"));//LIS库打标记
                    Ms66Flag = dal.UpdatePrintTimes(reportformidlist66.ToArray());
                    ZhiFang.Common.Log.Log.Debug("IP:" + ip + "向Lis添加打印次数:" + Ms66Flag);
                    Ms66Flag = dal.UpdateClientPrintTimes(reportformidlist66.ToArray());//外围打印标记
                    ZhiFang.Common.Log.Log.Debug("IP:" + ip + "向Lis添加外围打印次数:" + Ms66Flag);
                }

                flag = bqf.UpdatePrintTimes(reportformlist);
                ZhiFang.Common.Log.Log.Debug("IP:" + ip + "添加打印次数:reportformID:" + reportformidstr);
                flag = bqf.UpdateClientPrintTimes(reportformlist);
                ZhiFang.Common.Log.Log.Debug("IP:" + ip + "添加外围打印次数:reportformID:" + reportformidstr);
                if (Ms66Flag && flag)
                {
                    br.success = true;
                }
                else
                {
                    br.success = false;
                }
            }
            catch (Exception e)
            {
                br.success = false;
                br.ErrorInfo = e.ToString();
            }
            return br;
        }

        public BaseResultDataValue RequestResultMhistory(List<string> ReportFormID, string PatNo, List<string> SectionType, string Where)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BRequestForm bal = new BRequestForm();
            brdv.success = false;
            if (Where == "" || Where == null)
            {
                Where = " 1=1 ";
            }
            try
            {
                //List<string> relist = new List<string>();
                StringBuilder sb = new StringBuilder();
                for (var i = 0; i < ReportFormID.Count; i++)
                {
                    DataSet deptlist = bal.ResultMhistory(ReportFormID[i], PatNo, SectionType[i], Where);
                    string c = "";
                    if (i < ReportFormID.Count - 1)
                    {
                        c = ",";
                    }
                    for (var a = 0; a < deptlist.Tables.Count; a++)
                    {
                        var Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<ALLReportFromVo>(deptlist.Tables[a]);
                        var settings = new JsonSerializerSettings();
                        string aa = JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented, settings);
                        string b = "";
                        if (a < deptlist.Tables.Count - 1)
                        {
                            b = ",";
                        }
                        sb.Append(aa + b);
                    }
                    sb.Append(c);
                }
                brdv.ResultDataValue = "[" + sb.ToString() + "]";
                brdv.success = true;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("ResultMhistory.异常:" + e.ToString() + ".ReportFormID:" + ReportFormID);
                brdv.success = false;
                //brdv.ErrorInfo = "程序异常请查看系统日志！";
                brdv.ErrorInfo = e.ToString();
            }
            return brdv;
        }

        public BaseResultDataValue RequestResultDataTimeMhistory(string PatNO, string Where)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BRequestForm bal = new BRequestForm();
            brdv.success = false;
            List<string> ReportFormID = new List<string>();
            List<string> SectionType = new List<string>();
            try
            {
                var data = bal.ResultDataTimeMhistory(PatNO, Where);
                if (data.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow mDr in data.Tables[0].Rows)
                    {
                        ReportFormID.Add(mDr["ReportFormID"].ToString());
                        SectionType.Add(mDr["SecretType"].ToString());
                    }
                    brdv = RequestResultMhistory(ReportFormID, null, SectionType, null);
                }
                else
                {
                    brdv.success = false;
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("ResultDataTimeMhistory.异常:" + e.ToString() + ".PatNo:" + PatNO + ".Where:" + Where);
                brdv.success = false;
                //brdv.ErrorInfo = "程序异常请查看系统日志！";
                brdv.ErrorInfo = e.ToString();
            }
            return brdv;
        }

        public BaseResultDataValue GetReportFormPDFByReportFormIDANDTemplate(string ReportFormID, string SectionNo, string SectionType, int flag, string Template, string QueryType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = false;
            try
            {
                ZhiFang.Common.Log.Log.Debug("GetReportFormPDFByReportFormIDANDTemplate.ReportFormID:" + ReportFormID + ", SectionNo:" + SectionNo + ", SectionType:" + SectionType + ", flag:" + flag + "" + ",Template" + Template + ",QueryType" + QueryType);
                var listreportformfile = new List<ReportFormFilesVO>();
                if (QueryType == "REQUEST")
                {
                    listreportformfile = bprqf.CreatRequestFormFilesBYSpid(new List<string>() { ReportFormID }, ReportFormTitle.center, ReportFormFileType.PDF, SectionType, flag, Template);
                }
                else
                {
                    listreportformfile = bprf.CreatReportFormFilesBYSpid(new List<string>() { ReportFormID }, ReportFormTitle.center, ReportFormFileType.PDF, SectionType, flag, Template);
                }

                if (listreportformfile.Count > 0)
                {
                    brdv.ResultDataValue = ZhiFang.ReportFormQueryPrint.Common.JsonSerializer.JsonDotNetSerializerNoEnterSpace(listreportformfile[0]);
                    brdv.success = true;
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("GetReportFormPDFByReportFormIDANDTemplate.异常:" + e.ToString() + ".ReportFormID:" + ReportFormID + ", SectionNo:" + SectionNo + ", SectionType:" + SectionType + ", flag:" + flag + ",Template" + Template + ",QueryType" + QueryType);
                brdv.success = false;
                //brdv.ErrorInfo = "程序异常请查看系统日志！";
                brdv.ErrorInfo = e.ToString();
            }
            return brdv;
        }

        public BaseResultDataValue GetReportFromByReportFormID(string idList, string fields, string strWhere)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = false;
            BReportForm bal = new BReportForm();
            List<string> ReportFormID = new List<string>();
            try
            {
                if (idList == null || idList.Trim() == "")
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "idList参数为空！";
                    return brdv;
                }
                if (fields == null || fields.Trim() == "")
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "fields参数为空！";
                    return brdv;
                }
                string[] idLists = idList.Split(',');
                DataSet dataSet = new DataSet();
                foreach (var id in idLists)
                {
                    string idL = id.Replace("__", "");
                    idL = idL.Replace("_", ";");
                    ReportFormID.Add(idL);
                    var ds = bal.GetReportFromByReportFormID(ReportFormID, fields, strWhere);
                    dataSet.Merge(ds);
                    ReportFormID.Clear();
                }
                //var ds= bal.GetReportFromByReportFormID(ReportFormID, fields, strWhere);
                List<ReportFormVO> ils = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<ReportFormVO>(dataSet.Tables[0]);
                var settings = new JsonSerializerSettings();
                string aa = JsonConvert.SerializeObject(ils, Newtonsoft.Json.Formatting.Indented, settings);
                brdv.ResultDataValue = "{\"total\":" + ils.Count + ",\"rows\":" + aa + "}";
                brdv.success = true;
                return brdv;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("GetReportFromByReportFormID.异常:" + e.ToString() + ".idList:" + idList + ".fields:" + fields + ".strWhere:" + strWhere);
                brdv.success = false;
                //brdv.ErrorInfo = "程序异常请查看系统日志！";
                brdv.ErrorInfo = e.ToString();
            }
            return brdv;
        }

        public BaseResultDataValue GetRequestFromByReportFormID(string idList, string fields, string strWhere)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = false;
            BRequestForm bal = new BRequestForm();
            List<string> ReportFormID = new List<string>();
            try
            {
                if (idList == null || idList.Trim() == "")
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "idList参数为空！";
                    return brdv;
                }
                if (fields == null || fields.Trim() == "")
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "fields参数为空！";
                    return brdv;
                }
                string[] idLists = idList.Split(',');
                foreach (var id in idLists)
                {
                    idList = id.Replace("__", "");
                    idList = idList.Replace("_", ";");
                    ReportFormID.Add(idList);
                }
                var ds = bal.GetRequestFromByReportFormID(ReportFormID, fields, strWhere);
                List<ReportFormVO> ils = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<ReportFormVO>(ds.Tables[0]);
                var settings = new JsonSerializerSettings();
                string aa = JsonConvert.SerializeObject(ils, Newtonsoft.Json.Formatting.Indented, settings);
                brdv.ResultDataValue = "{\"total\":" + ils.Count + ",\"rows\":" + aa + "}";
                brdv.success = true;
                return brdv;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("GetRequestFromByReportFormID.异常:" + e.ToString() + ".idList:" + idList + ".fields:" + fields + ".strWhere:" + strWhere);
                brdv.success = false;
                //brdv.ErrorInfo = "程序异常请查看系统日志！";
                brdv.ErrorInfo = e.ToString();
            }
            return brdv;
        }

        public BaseResult ReportFormAddUpdatePrintTimes(string reportformidstr)
        {
            ZhiFang.Common.Log.Log.Debug("ReportFormAddUpdatePrintTimes.reportformidstr:" + reportformidstr);
            Model.BaseResult br = new Model.BaseResult();
            //获得的客户端ip
            OperationContext context = OperationContext.Current;
            MessageProperties properties = context.IncomingMessageProperties;
            RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
            string uluserCName = Common.Cookie.CookieHelper.Read("ULUserCName");
            string ip = endpoint.Address.ToString();
            try
            {
                string[] reportformlist = reportformidstr.Split(',');
                bool Ms66Flag = true;
                bool flag = true;
                if (ConfigHelper.GetConfigString("IsMEGroupSampleFormAddPrintTime").Equals("1"))
                {//打印微生物时向框架微生物添加打印次数
                    ZhiFang.Common.Log.Log.Debug("增加框架微生物打印次数.开始");
                    DataTable dsreportform = new DataTable();
                    dsreportform = brf.GetReportFormList(reportformlist);
                    if (dsreportform == null || dsreportform.Rows.Count <= 0)
                    {
                        br.success = false;
                        br.ErrorInfo = "LIS中没有找到报告单";
                    }
                    for (int i = 0; i < dsreportform.Rows.Count; i++)
                    {
                        if (dsreportform.Rows[i]["SectionType"].ToString() == "2" || dsreportform.Rows[i]["SectionType"].ToString() == "4")
                        {
                            if (dsreportform.Rows[i]["STestType"].ToString() != null && dsreportform.Rows[i]["STestType"].ToString() != "")
                            {
                                DAL.MSSQL.MSSQL66.ME_GroupSampleForm ME_GroupSampleForm = new DAL.MSSQL.MSSQL66.ME_GroupSampleForm();
                                int isok = ME_GroupSampleForm.UpDateMEPrintCount(dsreportform.Rows[i]["FormNo"].ToString(), dsreportform.Rows[i]["ReceiveDate"].ToString(), null);
                                if (isok > 0)
                                {
                                    ZhiFang.Common.Log.Log.Debug("成功增加框架微生物打印次数FormNo:" + dsreportform.Rows[i]["FormNo"].ToString());
                                }
                            }
                        }

                    }
                    ZhiFang.Common.Log.Log.Debug("增加框架微生物打印次数.结束");
                }
                if (ConfigHelper.GetConfigString("IsMEGroupSampleFormAddPrintTime").Equals("2"))
                {//打印微生物时向框架微生物添加打印次数----IsMEGroupSampleFormAddPrintTime=2针对邢台市人民医院
                    ZhiFang.Common.Log.Log.Debug("增加框架微生物打印次数.开始");
                    DataTable dsreportform = new DataTable();
                    dsreportform = brf.GetReportFormList(reportformlist);
                    if (dsreportform == null || dsreportform.Rows.Count <= 0)
                    {
                        br.success = false;
                        br.ErrorInfo = "LIS中没有找到报告单";
                    }
                    for (int i = 0; i < dsreportform.Rows.Count; i++)
                    {
                        if (dsreportform.Rows[i]["SectionType"].ToString() == "2" || dsreportform.Rows[i]["SectionType"].ToString() == "4")
                        {
                            if (dsreportform.Rows[i]["GroupSampleFormID"].ToString() != null && dsreportform.Rows[i]["GroupSampleFormID"].ToString() != "")
                            {
                                DAL.MSSQL.MSSQL66.ME_GroupSampleForm ME_GroupSampleForm = new DAL.MSSQL.MSSQL66.ME_GroupSampleForm();
                                int isok = ME_GroupSampleForm.UpDateMEPrintCount(dsreportform.Rows[i]["GroupSampleFormID"].ToString(), dsreportform.Rows[i]["ReceiveDate"].ToString(), null);
                                if (isok > 0)
                                {
                                    ZhiFang.Common.Log.Log.Debug("成功增加框架微生物打印次数GroupSampleFormID:" + dsreportform.Rows[i]["GroupSampleFormID"].ToString());
                                }
                            }
                        }

                    }
                    ZhiFang.Common.Log.Log.Debug("增加框架微生物打印次数.结束");
                }
                if (ConfigHelper.GetConfigString("IsLisAddPrintTime").Equals("1")) //向Lis添加打印次数
                {
                    DataTable dsreportform = new DataTable();
                    dsreportform = brf.GetReportFormList(reportformlist);
                    if (dsreportform == null || dsreportform.Rows.Count <= 0)
                    {
                        br.success = false;
                        br.ErrorInfo = "LIS中没有找到报告单";
                    }
                    List<string> reportformidlist66 = new List<string>();
                    for (int i = 0; i < dsreportform.Rows.Count; i++)
                    {
                        reportformidlist66.Add(dsreportform.Rows[i]["Receivedate"].ToString().Trim() + ";" + dsreportform.Rows[i]["SectionNo"].ToString().Trim() + ";" + dsreportform.Rows[i]["TestTypeNo"].ToString().Trim() + ";" + dsreportform.Rows[i]["SampleNo"].ToString().Trim());
                    }
                    IDReportForm dal = Factory.DalFactory<IDReportForm>.GetDal("ReportForm", "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66", ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("LISConnectionString"));//LIS库打标记
                    Ms66Flag = dal.UpdatePrintTimes(reportformidlist66.ToArray(), uluserCName);
                    ZhiFang.Common.Log.Log.Debug("IP:" + ip + "向Lis添加打印次数:" + Ms66Flag);
                    Ms66Flag = dal.UpdateClientPrintTimes(reportformidlist66.ToArray());//外围打印标记
                    ZhiFang.Common.Log.Log.Debug("IP:" + ip + "向Lis添加外围打印次数:" + Ms66Flag);
                }

                flag = brf.UpdatePrintTimes(reportformlist, uluserCName);

                //向打印记录表增加数据开始
                RFPReportFormPrintOperation brfEntity = new RFPReportFormPrintOperation();
                var EmpID = Common.Cookie.CookieHelper.Read("ULUserNo");
                var EmpName = Common.Cookie.CookieHelper.Read("ULUserCName");
                foreach (string a in reportformlist)
                {
                    string[] aaa = a.Split(';');
                    if (aaa.Count() > 1)
                    {
                        brfEntity.ReceiveDate = Convert.ToDateTime(aaa[0]);
                        brfEntity.SectionNo = int.Parse(aaa[1]);
                        brfEntity.TestTypeNo = int.Parse(aaa[2]);
                        brfEntity.SampleNo = aaa[3];
                    }
                    else
                    {
                        brfEntity.BobjectID = long.Parse(a);
                    }
                    if (EmpID != null && EmpID != "")
                    {
                        brfEntity.EmpID = long.Parse(EmpID);
                    }
                    if (EmpName != null && EmpName != "")
                    {
                        brfEntity.EmpName = EmpName;
                    }else{
                        brfEntity.EmpName =ip;
                    }
                    BRFRFPO.Add(brfEntity);
                }
                //向打印记录表增加数据结束
                //增加站点信息开始
                if (!string.IsNullOrEmpty(ConfigHelper.GetConfigString("IsAddSiteInfoRecord")) && ConfigHelper.GetConfigString("IsAddSiteInfoRecord").Equals("1"))
                {
                    //根据电脑名字和ip判断如果不存在则增加一条记录
                    bool recordIsExist = BSiteOperationRecords.isExist("", ip);
                    if (!recordIsExist)
                    {
                        BSiteOperationRecords.addSiteOperationRecord("ReportFormAddUpdatePrintTimes", ip, "");
                    }
                }
                //增加站点信息结束

                ZhiFang.Common.Log.Log.Debug("IP:" + ip + "添加打印次数:reportformID:" + reportformidstr);
                //flag = brf.UpdateClientPrintTimes(reportformlist);
                //ZhiFang.Common.Log.Log.Debug("IP:" + ip + "添加外围打印次数:reportformID:" + reportformidstr);
                if (Ms66Flag && flag)
                {
                    br.success = true;
                }
                else
                {
                    br.success = false;
                }
            }
            catch (Exception e)
            {
                br.success = false;
                br.ErrorInfo = e.ToString();
            }
            return br;
        }

        public BaseResult RequestFormAddUpdatePrintTimes(string reportformidstr)
        {
            Model.BaseResult br = new Model.BaseResult();
            //获得的客户端ip
            OperationContext context = OperationContext.Current;
            MessageProperties properties = context.IncomingMessageProperties;
            RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
            string ip = endpoint.Address.ToString();
            try
            {
                string[] reportformlist = reportformidstr.Split(',');
                bool Ms66Flag = true;
                bool flag = true;
                if (ConfigHelper.GetConfigString("IsLisAddPrintTime").Equals("1")) //向Lis添加打印次数
                {
                    DataTable dsreportform = new DataTable();
                    dsreportform = brf.GetReportFormList(reportformlist);
                    if (dsreportform == null || dsreportform.Rows.Count <= 0)
                    {
                        br.success = false;
                        br.ErrorInfo = "LIS中没有找到报告单";
                    }
                    List<string> reportformidlist66 = new List<string>();
                    for (int i = 0; i < dsreportform.Rows.Count; i++)
                    {
                        reportformidlist66.Add(dsreportform.Rows[i]["Receivedate"].ToString().Trim() + ";" + dsreportform.Rows[i]["SectionNo"].ToString().Trim() + ";" + dsreportform.Rows[i]["TestTypeNo"].ToString().Trim() + ";" + dsreportform.Rows[i]["SampleNo"].ToString().Trim());
                    }
                    IDRequestForm dal = Factory.DalFactory<IDRequestForm>.GetDal("RequestForm", "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66", ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("LISConnectionString"));//LIS库打标记
                    Ms66Flag = dal.UpdatePrintTimes(reportformidlist66.ToArray());
                    ZhiFang.Common.Log.Log.Debug("IP:" + ip + "向Lis添加打印次数:" + Ms66Flag);
                    Ms66Flag = dal.UpdateClientPrintTimes(reportformidlist66.ToArray());//外围打印标记
                    ZhiFang.Common.Log.Log.Debug("IP:" + ip + "向Lis添加外围打印次数:" + Ms66Flag);
                }

                flag = bqf.UpdatePrintTimes(reportformlist);
                ZhiFang.Common.Log.Log.Debug("IP:" + ip + "添加打印次数:reportformID:" + reportformidstr);
                //flag = bqf.UpdateClientPrintTimes(reportformlist);
                //ZhiFang.Common.Log.Log.Debug("IP:" + ip + "添加外围打印次数:reportformID:" + reportformidstr);
                if (Ms66Flag && flag)
                {
                    br.success = true;
                }
                else
                {
                    br.success = false;
                }
            }
            catch (Exception e)
            {
                br.success = false;
                br.ErrorInfo = e.ToString();
            }
            return br;
        }

        public BaseResult ReportFormAddUpdateClientPrintTimes(string reportformidstr)
        {
            ZhiFang.Common.Log.Log.Debug("ReportFormAddUpdateClientPrintTimes.reportformidstr:" + reportformidstr);
            Model.BaseResult br = new Model.BaseResult();
            //获得的客户端ip
            OperationContext context = OperationContext.Current;
            MessageProperties properties = context.IncomingMessageProperties;
            RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
            string ip = endpoint.Address.ToString();
            string uluserCName = Common.Cookie.CookieHelper.Read("ULUserCName");
            try
            {
                string[] reportformlist = reportformidstr.Split(',');
                bool Ms66Flag = true;
                bool flag = true;
                if (ConfigHelper.GetConfigString("IsMEGroupSampleFormAddPrintTime").Equals("1"))
                {//打印微生物时向框架微生物添加打印次数
                    ZhiFang.Common.Log.Log.Debug("增加框架微生物打印次数.开始");
                    DataTable dsreportform = new DataTable();
                    dsreportform = brf.GetReportFormList(reportformlist);
                    if (dsreportform == null || dsreportform.Rows.Count <= 0)
                    {
                        br.success = false;
                        br.ErrorInfo = "LIS中没有找到报告单";
                    }
                    for (int i = 0; i < dsreportform.Rows.Count; i++)
                    {
                        if (dsreportform.Rows[i]["SectionType"].ToString() == "2" || dsreportform.Rows[i]["SectionType"].ToString() == "4")
                        {
                            if (dsreportform.Rows[i]["STestType"].ToString() != null && dsreportform.Rows[i]["STestType"].ToString() != "")
                            {
                                DAL.MSSQL.MSSQL66.ME_GroupSampleForm ME_GroupSampleForm = new DAL.MSSQL.MSSQL66.ME_GroupSampleForm();
                                int isok = ME_GroupSampleForm.UpDateMEPrintCount(dsreportform.Rows[i]["FormNo"].ToString(), dsreportform.Rows[i]["ReceiveDate"].ToString(), null);
                                if (isok > 0)
                                {
                                    ZhiFang.Common.Log.Log.Debug("成功增加框架微生物打印次数FormNo:" + dsreportform.Rows[i]["FormNo"].ToString());
                                }
                            }
                        }

                    }
                    ZhiFang.Common.Log.Log.Debug("增加框架微生物打印次数.结束");
                }
                ZhiFang.Common.Log.Log.Debug("ReportFormAddUpdateClientPrintTimes.IsLisAddPrintTime:" + ConfigHelper.GetConfigString("IsLisAddPrintTime"));
                if (ConfigHelper.GetConfigString("IsLisAddPrintTime").Equals("1")) //向Lis添加打印次数
                {
                    DataTable dsreportform = new DataTable();
                    dsreportform = brf.GetReportFormList(reportformlist);
                    if (dsreportform == null || dsreportform.Rows.Count <= 0)
                    {
                        br.success = false;
                        br.ErrorInfo = "LIS中没有找到报告单";
                    }
                    List<string> reportformidlist66 = new List<string>();
                    for (int i = 0; i < dsreportform.Rows.Count; i++)
                    {
                        reportformidlist66.Add(dsreportform.Rows[i]["Receivedate"].ToString().Trim() + ";" + dsreportform.Rows[i]["SectionNo"].ToString().Trim() + ";" + dsreportform.Rows[i]["TestTypeNo"].ToString().Trim() + ";" + dsreportform.Rows[i]["SampleNo"].ToString().Trim());
                    }
                    IDReportForm dal = Factory.DalFactory<IDReportForm>.GetDal("ReportForm", "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66", ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("LISConnectionString"));//LIS库打标记
                    Ms66Flag = dal.UpdatePrintTimes(reportformidlist66.ToArray(), uluserCName);
                    ZhiFang.Common.Log.Log.Debug("IP:" + ip + "向Lis添加打印次数:" + Ms66Flag);
                    Ms66Flag = dal.UpdateClientPrintTimes(reportformidlist66.ToArray());//外围打印标记
                    ZhiFang.Common.Log.Log.Debug("IP:" + ip + "向Lis添加外围打印次数:" + Ms66Flag);
                }

                //flag = brf.UpdatePrintTimes(reportformlist);
                //ZhiFang.Common.Log.Log.Debug("IP:" + ip + "添加打印次数:reportformID:" + reportformidstr);
                flag = brf.UpdateClientPrintTimes(reportformlist);
                //向打印记录表增加数据开始
                if (flag)
                {
                    RFPReportFormPrintOperation brfEntity = new RFPReportFormPrintOperation();
                    var EmpID = ip;
                    var EmpName = System.Net.Dns.GetHostName();
                    foreach (string reportformid in reportformlist)
                    {
                        string[] fourField = reportformid.Split(';');
                        if (fourField.Count() > 1)
                        {
                            brfEntity.ReceiveDate = Convert.ToDateTime(fourField[0]);
                            brfEntity.SectionNo = int.Parse(fourField[1]);
                            brfEntity.TestTypeNo = int.Parse(fourField[2]);
                            brfEntity.SampleNo = fourField[3];
                        }
                        else
                        {
                            brfEntity.BobjectID = long.Parse(reportformid);
                        }
                        if (EmpID != null && EmpID != "")
                        {
                            brfEntity.EmpID = long.Parse(EmpID);
                        }
                        if (EmpName != null && EmpName != "")
                        {
                            brfEntity.EmpName = EmpName;
                        }
                        BRFRFPO.Add(brfEntity);
                    }
                }
                //向打印记录表增加数据结束
                //增加站点信息开始
                if (!string.IsNullOrEmpty(ConfigHelper.GetConfigString("IsAddSiteInfoRecord")) && ConfigHelper.GetConfigString("IsAddSiteInfoRecord").Equals("1"))
                {
                    //根据电脑名字和ip判断如果不存在则增加一条记录
                    bool recordIsExist = BSiteOperationRecords.isExist("", ip);
                    if (!recordIsExist)
                    {
                        BSiteOperationRecords.addSiteOperationRecord("ReportFormAddUpdatePrintTimes", ip, "");
                    }
                }
                //增加站点信息结束
                ZhiFang.Common.Log.Log.Debug("IP:" + ip + "添加外围打印次数:reportformID:" + reportformidstr);
                if (Ms66Flag && flag)
                {
                    br.success = true;
                }
                else
                {
                    br.success = false;
                }
            }
            catch (Exception e)
            {
                br.success = false;
                br.ErrorInfo = e.ToString();
            }
            return br;
        }

        public BaseResult RequestFormAddUpdateClientPrintTimes(string reportformidstr)
        {
            Model.BaseResult br = new Model.BaseResult();
            //获得的客户端ip
            OperationContext context = OperationContext.Current;
            MessageProperties properties = context.IncomingMessageProperties;
            RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
            string ip = endpoint.Address.ToString();
            try
            {
                string[] reportformlist = reportformidstr.Split(',');
                bool Ms66Flag = true;
                bool flag = true;
                if (ConfigHelper.GetConfigString("IsLisAddPrintTime").Equals("1")) //向Lis添加打印次数
                {
                    DataTable dsreportform = new DataTable();
                    dsreportform = brf.GetReportFormList(reportformlist);
                    if (dsreportform == null || dsreportform.Rows.Count <= 0)
                    {
                        br.success = false;
                        br.ErrorInfo = "LIS中没有找到报告单";
                    }
                    List<string> reportformidlist66 = new List<string>();
                    for (int i = 0; i < dsreportform.Rows.Count; i++)
                    {
                        reportformidlist66.Add(dsreportform.Rows[i]["Receivedate"].ToString().Trim() + ";" + dsreportform.Rows[i]["SectionNo"].ToString().Trim() + ";" + dsreportform.Rows[i]["TestTypeNo"].ToString().Trim() + ";" + dsreportform.Rows[i]["SampleNo"].ToString().Trim());
                    }
                    IDRequestForm dal = Factory.DalFactory<IDRequestForm>.GetDal("RequestForm", "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66", ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("LISConnectionString"));//LIS库打标记
                    Ms66Flag = dal.UpdatePrintTimes(reportformidlist66.ToArray());
                    ZhiFang.Common.Log.Log.Debug("IP:" + ip + "向Lis添加打印次数:" + Ms66Flag);
                    Ms66Flag = dal.UpdateClientPrintTimes(reportformidlist66.ToArray());//外围打印标记
                    ZhiFang.Common.Log.Log.Debug("IP:" + ip + "向Lis添加外围打印次数:" + Ms66Flag);
                }

                //flag = bqf.UpdatePrintTimes(reportformlist);
                //ZhiFang.Common.Log.Log.Debug("IP:" + ip + "添加打印次数:reportformID:" + reportformidstr);
                flag = bqf.UpdateClientPrintTimes(reportformlist);

                ZhiFang.Common.Log.Log.Debug("IP:" + ip + "添加外围打印次数:reportformID:" + reportformidstr);
                if (Ms66Flag && flag)
                {
                    br.success = true;
                }
                else
                {
                    br.success = false;
                }
            }
            catch (Exception e)
            {
                br.success = false;
                br.ErrorInfo = e.ToString();
            }
            return br;
        }

        public BaseResultDataValue deleteClientPrint(List<string> formno)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BReportForm brform = new BReportForm();
            if (formno == null || formno.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传递参数不能为空";
                return brdv;
            }

            try
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Info("deleteClientPrint.操作者IP地址:" + ip);

                brdv.success = true;
                foreach (var item in formno)
                {
                    int flag = brform.UpdateClientPrint(item);
                    if (flag <= 0)
                    {
                        brdv.success = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                brdv.success = false;
                brdv.ErrorInfo = ex.Message.ToString();
            }
            return brdv;
        }

        public BaseResultDataValue deletePrintTimes(List<string> formno)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BReportForm brform = new BReportForm();
            if (formno == null || formno.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传递参数不能为空";
                return brdv;
            }

            try
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Info("deletePrintTimes.操作者IP地址:" + ip);

                brdv.success = true;
                foreach (var item in formno)
                {
                    int flag = brform.UpdatePrintTimes(item);
                    if (flag <= 0)
                    {
                        brdv.success = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                brdv.success = false;
                brdv.ErrorInfo = ex.Message.ToString();
            }
            return brdv;
        }


        public BaseResultDataValue PreviewRequestByReportTemp(string ReportFormID, string SectionNo, string SectionType, string ModelType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            try
            {
                brdv = this.PreviewRequestExtPageNameByReportTemp(ReportFormID, SectionNo, SectionType, ModelType, null);
                return brdv;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("PreviewRequest出错：" + e.ToString());
                brdv.ErrorInfo = "PreviewRequest出错：" + e.ToString();
                brdv.success = false;
                return brdv;
            }
        }

        public BaseResultDataValue PreviewRequestExtPageNameByReportTemp(string ReportFormID, string SectionNo, string SectionType, string ModelType, string PageName)
        {
            ZhiFang.Common.Log.Log.Debug("PreviewRepquestExtPageName.ReportFormID:" + ReportFormID + ", SectionNo:" + SectionNo + ", SectionType:" + SectionType + ", ModelType:" + ModelType + ",PageName:" + PageName);
            BaseResultDataValue brdv = new BaseResultDataValue();

            try
            {
                string urlReportFormID = ReportFormID;
                string urlSectionNo = SectionNo;
                int urlSectionType = int.Parse(SectionType);
                if (int.Parse(SectionType.Substring(0, 1)) > 5)
                {
                    ModelType = "pdf";
                }
                string ulModelType = ModelType;

                string urlPageName = "TechnicianPrint1.aspx";
                if (PageName != null && PageName.Trim() != "")
                    urlPageName = PageName;

                string urlSorg = "0";
                string urlShowType = "0";
                string LPreview = "<HTML>报告预览</HTML>";

                int st = Convert.ToInt32(urlShowType);
                int sn = Convert.ToInt32(urlSectionNo);
                string tmp = "";
                //if (ulModelType == "report")
                //{
                //    ZhiFang.Common.Log.Log.Debug("Preview_report");
                //    //去数据库找模版
                //    var l = bprf.CreatReportFormFiles(new List<string>() { urlReportFormID }, ReportFormTitle.center, ReportFormFileType.JPEG, SectionType);
                //    if (l.Count > 0)
                //    {
                //        tmp = l[0];
                //    }
                //}
                if (ulModelType == "result")
                {
                    ZhiFang.Common.Log.Log.Debug("Preview_result");
                    //去XmlConfig找模版
                    tmp = bsrf.ShowRequestFormResultByReportTemp(urlReportFormID, sn, urlPageName, st, urlSectionType);
                }

                if (ulModelType == "pdf")
                {
                    ZhiFang.Common.Log.Log.Debug("Preview_pdf");
                    //返回pdf文件
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<iframe width=\"100%\" height=\"100%\" ");
                    string dir = urlReportFormID.Split(new char[] { ';' })[0];
                    sb.Append("src=\"ReportFiles/" + dir + "/" + urlReportFormID + ".pdf\" ");
                    sb.Append("height=\"100%\" width=\"100%\" frameborder=\"0\" ");
                    sb.Append("style=\"overflow:hidden;overflow-x:hidden;overflow-y:hidden;height:100%;width:100%;position:absolute;top:0px;left:0px;right:0px;bottom:0px\"> ");
                    sb.Append("</iframe>");
                    tmp = sb.ToString();
                }

                if (tmp.IndexOf("<html") >= 0)
                {
                    tmp = tmp.Substring(tmp.IndexOf("<html"), tmp.Length - tmp.IndexOf("<html"));
                }
                tmp = tmp.Replace("\r\n ", " ");
                brdv.success = true;
                brdv.ResultDataValue = tmp;
                return brdv;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("PreviewRepquestExtPageName出错：" + e.ToString());
                brdv.ErrorInfo = "PreviewRepquestExtPageName出错：" + e.ToString();
                brdv.success = false;
                return brdv;
            }
        }

        public BaseResultDataValue DownloadthePDFByReportFormID(string ReportFormID, string SectionType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = false;
            int flag = 0;
            string[] ReportFormIDarr = ReportFormID.Split('*');//拆分
            string[] SectionTypearr = SectionType.Split(';');//拆分
            var itemDirectory = System.AppDomain.CurrentDomain.BaseDirectory;//获取项目物理路径
            try
            {
                ZhiFang.Common.Log.Log.Debug("DownloadthePDFByReportFormID.ReportFormID:" + ReportFormID + ";SectionType:" + SectionType);

                //要下载的PDF文件名
                List<string> RarPDFfiles = new List<string>();
                //查询报告单是否生成如果未生成则生成报告单并且返回报告单路径
                List<string> PDFfiles = new List<string>();
                //要压缩的文件夹名称
                DateTime now = DateTime.Now;
                string rarfiles = now.ToString("yyyy-MM-dd") + ZhiFang.ReportFormQueryPrint.Common.GUIDHelp.GetGUIDString();

                for (int i = 0; i < ReportFormIDarr.Count(); i++)
                {
                    var listreportformfile = bprf.CreatReportFormFiles(new List<string>() { ReportFormIDarr[i] }, ReportFormTitle.center, ReportFormFileType.PDF, SectionTypearr[i], flag);
                    if (listreportformfile.Count > 0)
                    {
                        PDFfiles.Add(itemDirectory + listreportformfile[0].PDFPath);

                        //查询报告单信息拼接下载PDF文件名
                        ReportForm reportForm = brf.GetModel(ReportFormIDarr[i]);
                        if (reportForm.paritemname.Length > 10)
                        {
                            reportForm.paritemname = reportForm.paritemname.Substring(0, 9);
                        }
                        //去除非法字符
                        StringBuilder rBuilder = new StringBuilder(reportForm.paritemname);
                        foreach (char rInvalidChar in Path.GetInvalidFileNameChars())
                        {
                            rBuilder.Replace(rInvalidChar.ToString(), string.Empty);
                        }
                        
                        RarPDFfiles.Add(itemDirectory + "RarReportFormFiles\\" + rarfiles + "\\" + reportForm.ReceiveDate.ToString().Replace("/", "-").Split(' ')[0] + "_" + reportForm.PatNo + "_" + reportForm.CName + "_" + rBuilder.ToString() + "_" + reportForm.FormNo.Replace(";", "-") + ".PDF");

                    }
                }
                //创建存放打包文件夹的文件夹RarReportFormFiles
                if (!Directory.Exists(itemDirectory + "RarReportFormFiles"))//判断文件夹是否存在 
                {
                    Directory.CreateDirectory(itemDirectory + "RarReportFormFiles");//不存在则创建文件夹 
                }
                //创建要打包的文件夹
                if (!Directory.Exists(itemDirectory + "RarReportFormFiles\\" + rarfiles))//判断文件夹是否存在 
                {
                    Directory.CreateDirectory(itemDirectory + "RarReportFormFiles\\" + rarfiles);//不存在则创建文件夹 
                }

                //复制生成的报告单到RarReportFormFiles文件夹
                for (int i = 0; i < PDFfiles.Count(); i++)
                {
                    if (!File.Exists(RarPDFfiles[i]))
                    {
                        File.Copy(PDFfiles[i], RarPDFfiles[i]);
                    }
                }
                List<string> rpasd = new List<string>();
                rpasd.Add(itemDirectory + "RarReportFormFiles\\" + rarfiles);
                bool isZip = DotNetZipHelp.CompressMulti(rpasd, itemDirectory + "RarReportFormFiles\\" + rarfiles + ".zip", false);
                //bool isZip = ZipHelp.PackageFolder(itemDirectory + "RarReportFormFiles\\" + rarfiles, itemDirectory + "RarReportFormFiles\\" + rarfiles + ".zip", true);
                if (isZip)
                {
                    for (int i = 0; i < RarPDFfiles.Count(); i++)
                    {
                        if (File.Exists(RarPDFfiles[i]))
                        {
                            File.Delete(RarPDFfiles[i]);
                        }
                    }
                    Directory.Delete(itemDirectory + "RarReportFormFiles\\" + rarfiles);
                    brdv.ResultDataValue = ZhiFang.ReportFormQueryPrint.Common.JsonSerializer.JsonDotNetSerializerNoEnterSpace("RarReportFormFiles/" + rarfiles + ".zip");
                    brdv.success = true;
                }
                else
                {
                    brdv.ErrorInfo = "压缩失败";
                    brdv.success = false;
                }


            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("DownloadthePDFByReportFormID.异常:" + e.ToString() + ".ReportFormID:" + ReportFormID);
                brdv.success = false;
                //brdv.ErrorInfo = "程序异常请查看系统日志！";
                brdv.ErrorInfo = e.ToString();
            }
            return brdv;
        }

        public BaseResultDataValue GetSectionPrintStrPageNameBySectionNo(string SectionNo)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = false;
            BSectionPrint bsp = new BSectionPrint();
            try
            {
                SectionPrint sp = bsp.GetSectionPrintBySectionOne(SectionNo);
                if (sp.PrintFormat.IndexOf("A5") > -1)
                {
                    brdv.ResultDataValue = ZhiFang.ReportFormQueryPrint.Common.JsonSerializer.JsonDotNetSerializerNoEnterSpace("A5");
                    brdv.success = true;
                }
                else if (sp.PrintFormat.IndexOf("A4") > -1)
                {
                    brdv.ResultDataValue = ZhiFang.ReportFormQueryPrint.Common.JsonSerializer.JsonDotNetSerializerNoEnterSpace("A4");
                    brdv.success = true;
                }
                else
                {
                    brdv.ResultDataValue = ZhiFang.ReportFormQueryPrint.Common.JsonSerializer.JsonDotNetSerializerNoEnterSpace("双A5");
                    brdv.success = true;
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("GetSectionPrintStrPageNameBySectionNo.异常:" + e.ToString() + ".SectionNo:" + SectionNo);
                brdv.success = false;
                //brdv.ErrorInfo = "程序异常请查看系统日志！";
                brdv.ErrorInfo = e.ToString();
            }
            return brdv;
        }

        public BaseResultDataValue GetCreatePUserESignature(string SqlWhere)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = false;
            BPUser bP = new BPUser();
            try
            {
                int isCreate = bP.GetCreatePUserESignature(SqlWhere);
                if (isCreate == 1)
                {
                    brdv.success = true;
                }
                else
                {
                    brdv.success = false;
                }

            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("GetCreatePUserESignature.异常:" + e.ToString() + ".SqlWhere:" + SqlWhere);
                brdv.success = false;
                //brdv.ErrorInfo = "程序异常请查看系统日志！";
                brdv.ErrorInfo = e.ToString();
            }
            return brdv;
        }

        public BaseResultDataValue SelectReportRequest(string Where, string fields, int page, int limit, string SerialNo)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                #region 查询报告（检验中和检验后）
                string urlWhere = Where.Replace("\"", "'");

                string urlModel = fields;
                int urlPage = page;
                int urlLimit = limit;
                //ZhiFang.Common.Log.Log.Debug("urlType_urlModel" + urlModel);

                if (string.IsNullOrEmpty(urlWhere))
                {
                    urlWhere = " 1=1 ";
                }

                if (urlWhere.Length < 4)
                {
                    brdv.ErrorInfo = "Error:请选择查询条件!";
                    brdv.success = false;
                    return brdv;
                }
                //报告发布程序的状态字段
                //urlWhere += " and resultsend='1' ";

                if (urlModel.Length < 1)
                {
                    brdv.ErrorInfo = "Error:请选择要查询的数据!";
                    brdv.success = false;
                    return brdv;
                }

                DataSet ds = new DataSet();

                //string countwhere = urlWhere.ToUpper();
                string countwhere = urlWhere;
                if (countwhere.IndexOf("ORDER") >= 0)
                {
                    countwhere = countwhere.Substring(0, countwhere.IndexOf("ORDER"));
                }
                // if (countwhere.IndexOf("order") >= 0)
                //{ countwhere = countwhere.Substring(0, countwhere.IndexOf("order"));         }

                int dsCount = 0;
                if (string.IsNullOrWhiteSpace(SerialNo))
                {
                    dsCount = dsCount + bqf.GetCountFormFull(countwhere);
                    dsCount = dsCount + barf.GetCountFormFull(countwhere);
                }
                else
                {
                    dsCount = bnri.GetCountFormFull(SerialNo + "and SamplingGroupno<>'' ");
                }



                //ZhiFang.Common.Log.Log.Debug("urlType_dsCount" + dsCount);
                int _reportformlimit = 5000;
                if (Common.ConfigHelper.GetConfigInt("SearchReportFormLimit") != null)
                {
                    _reportformlimit = Common.ConfigHelper.GetConfigInt("SearchReportFormLimit").Value;
                }
                if (dsCount > _reportformlimit)
                {
                    brdv.ErrorInfo = "Error:" + dsCount + "数据超过" + _reportformlimit + "条,请从新选择查询条件!";
                    brdv.success = false;
                    return brdv;
                }

                if (dsCount <= 0)
                {
                    brdv.ResultDataValue = "{\"total\":" + dsCount + ",\"rows\":[]}"; ;
                    brdv.success = true;
                    return brdv;
                }

                if (string.IsNullOrWhiteSpace(SerialNo))
                {

                    DataSet ds1 = bqf.GetList_FormFull(urlModel, urlWhere);
                    ds = barf.GetList_FormFull(urlModel, urlWhere);

                    ds.Tables[0].Merge(ds1.Tables[0]);
                }
                else
                {
                    ds = bnri.GetList_FormFull("SerialNo", SerialNo + "and SamplingGroupno<>'' ");
                    string reportFormWhere = "serialno in (";
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        reportFormWhere += "'" + item[0].ToString() + "',";
                    }
                    reportFormWhere = reportFormWhere.Substring(0, reportFormWhere.Length - 1) + ")";
                    ds = barf.GetList_FormFull(urlModel, reportFormWhere + " and " + Where);
                    DataSet ds2 = bqf.GetList_FormFull(urlModel, reportFormWhere + " and " + Where);
                    ds.Tables[0].Merge(ds2.Tables[0]);
                }


                //ZhiFang.Common.Log.Log.Debug("urlType_dsCount_ds" + ds.Tables[0].Rows.Count);

                //List<Model.ReportFormFull> ils = GetList<Model.ReportFormFull>(ds.Tables[0]);
                List<ReportFormVO> ils = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<ReportFormVO>(ds.Tables[0]);
                //ZhiFang.Common.Log.Log.Debug("urlType_dsCount_ds_ils" + ils.Count);
                //List<Model.ReportFormFull> Result = Pagination<Model.ReportFormFull>(context, ils);
                //for(int i=0;i<d)


                List<ReportFormVO> Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.Pagination<ReportFormVO>(Convert.ToInt32(urlPage), Convert.ToInt32(urlLimit), ils);

                //ZhiFang.Common.Log.Log.Debug("urlType_dsCount_ds_ils_Result" + Result.Count);
                var settings = new JsonSerializerSettings();

                string aa = JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented, settings);
                //ZhiFang.Common.Log.Log.Debug("urlType_dsCount_ds_ils_Result_aa" + aa.Length);

                brdv.ResultDataValue = "{\"total\":" + ils.Count + ",\"rows\":" + aa + "}";
                brdv.success = true;
                return brdv;
                #endregion
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                brdv.ErrorInfo = "SelectReport:" + ex.ToString();
                brdv.success = false;
                return brdv;
            }

        }

        public BaseResultDataValue GetSampleReportFromByReportFormIdList(string ReportFormIdList, string SectionType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = false;
            try
            {
                if (SectionType == null || SectionType.Trim() == "")
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "SectionType参数为空！";
                    return brdv;
                }


                if (ReportFormIdList == null || ReportFormIdList.Trim() == "")
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "ReportFormIdList参数为空！";
                    return brdv;
                }

                ZhiFang.Common.Log.Log.Debug("GetSampleReportFromByReportFormIdList.ReportFormIdList:" + ReportFormIdList + ", SectionType:" + SectionType);
                List<ReportFormFilesVO> listreportformfile = bprf.GetSampleReportFromByReportFormIdList(ReportFormIdList, SectionType);
                if (listreportformfile.Count > 0)
                {
                    brdv.ResultDataValue = ZhiFang.ReportFormQueryPrint.Common.JsonSerializer.JsonDotNetSerializerNoEnterSpace(listreportformfile[0]);
                    brdv.success = true;
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("GetSampleReportFromByReportFormIdList.异常:" + e.ToString() + ".ReportFormIdList:" + ReportFormIdList + ", SectionType:" + SectionType);
                brdv.success = false;
                brdv.ErrorInfo = e.ToString();
            }
            return brdv;
        }

        public BaseResultDataValue GetReportFormFullByReportFormID(string ReportFormID)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = false;
            if ("" == ReportFormID || null == ReportFormID)
            {
                ZhiFang.Common.Log.Log.Debug("GetReportFormFullByReportFormID.传入参数ReportFormID为空！");
                brdv.ErrorInfo = "传入参数ReportFormID为空！";
                brdv.success = false;
                return null;
            }
            try
            {
                ZhiFang.Common.Log.Log.Debug("GetReportFormFullByReportFormID.ReportFormID:" + ReportFormID);
                DataSet reportFormFull = brf.GetReportFormFullByReportFormID(ReportFormID);
                List<ReportFormFull> Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<ReportFormFull>(reportFormFull.Tables[0]);
                var settings = new JsonSerializerSettings();
                string aa = JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented, settings);
                brdv.ResultDataValue = "{\"count\":" + Result.Count + ",\"list\":" + aa + "}";
                brdv.success = true;

            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("GetReportFormFullByReportFormID.异常:" + e.ToString() + ".ReportFormIdList:" + ReportFormID);
                brdv.success = false;
                brdv.ErrorInfo = e.ToString();
            }
            return brdv;

        }

        public BaseResultDataValue GetReportItemFullByReportFormID(string ReportFormID)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = false;
            if ("" == ReportFormID || null == ReportFormID)
            {
                ZhiFang.Common.Log.Log.Debug("GetReportItemFullByReportFormID.传入参数ReportFormID为空！");
                brdv.ErrorInfo = "传入参数ReportFormID为空！";
                brdv.success = false;
                return null;
            }
            try
            {
                ZhiFang.Common.Log.Log.Debug("GetReportItemFullByReportFormID.ReportFormID:" + ReportFormID);
                DataSet reportFormFull = bri.GetReportItemByReportFormID(ReportFormID);
                List<ReportItemFull> Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<ReportItemFull>(reportFormFull.Tables[0]);
                var settings = new JsonSerializerSettings();
                string aa = JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented, settings);
                brdv.ResultDataValue = "{\"count\":" + Result.Count + ",\"list\":" + aa + "}";
                brdv.success = true;

            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("GetReportItemFullByReportFormID.异常:" + e.ToString() + ".ReportFormIdList:" + ReportFormID);
                brdv.success = false;
                brdv.ErrorInfo = e.ToString();
            }
            return brdv;
        }

        public BaseResultDataValue GetReportMicroFullByReportFormID(string ReportFormID)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = false;
            if ("" == ReportFormID || null == ReportFormID)
            {
                ZhiFang.Common.Log.Log.Debug("GetReportMicroFullByReportFormID.传入参数ReportFormID为空！");
                brdv.ErrorInfo = "传入参数ReportFormID为空！";
                brdv.success = false;
                return null;
            }
            try
            {
                ZhiFang.Common.Log.Log.Debug("GetReportMicroFullByReportFormID.ReportFormID:" + ReportFormID);
                BReportMicro brm = new BReportMicro();
                DataSet reportFormFull = brm.GetReportMicroFullByReportFormId(ReportFormID);
                List<ReportMicroFull> Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<ReportMicroFull>(reportFormFull.Tables[0]);
                var settings = new JsonSerializerSettings();
                string aa = JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented, settings);
                brdv.ResultDataValue = "{\"count\":" + Result.Count + ",\"list\":" + aa + "}";
                brdv.success = true;

            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("GetReportMicroFullByReportFormID.异常:" + e.ToString() + ".ReportFormIdList:" + ReportFormID);
                brdv.success = false;
                brdv.ErrorInfo = e.ToString();
            }
            return brdv;
        }

        public BaseResultDataValue GetReportMarrowFullByReportFormID(string ReportFormID)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = false;
            if ("" == ReportFormID || null == ReportFormID)
            {
                ZhiFang.Common.Log.Log.Debug("GetReportMarrowFullByReportFormID.传入参数ReportFormID为空！");
                brdv.ErrorInfo = "传入参数ReportFormID为空！";
                brdv.success = false;
                return null;
            }
            try
            {
                ZhiFang.Common.Log.Log.Debug("GetReportMarrowFullByReportFormID.ReportFormID:" + ReportFormID);
                BReportMarrow brm = new BReportMarrow();
                DataSet reportFormFull = brm.GetReportMarrowFullByReportFormID(ReportFormID);
                List<ReportMarrowFull> Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<ReportMarrowFull>(reportFormFull.Tables[0]);
                var settings = new JsonSerializerSettings();
                string aa = JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented, settings);
                brdv.ResultDataValue = "{\"count\":" + Result.Count + ",\"list\":" + aa + "}";
                brdv.success = true;

            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("GetReportMarrowFullByReportFormID.异常:" + e.ToString() + ".ReportFormIdList:" + ReportFormID);
                brdv.success = false;
                brdv.ErrorInfo = e.ToString();
            }
            return brdv;
        }

        public BaseResultDataValue UpdateReportFormFull(List<ReportFormFull> models)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (models == null || models.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数为空";
                return brdv;
            }
            try
            {
                foreach (var item in models)
                {
                    brf.UpdateReportFormFull(item);
                }
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.Message.ToString();
                ZhiFang.Common.Log.Log.Debug(e.ToString());
            }
            return brdv;
        }

        public BaseResultDataValue UpdateReportItemFull(List<ReportItemFull> models)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (models == null || models.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数为空";
                return brdv;
            }
            try
            {
                foreach (var item in models)
                {
                    bri.UpdateReportItemFull(item);
                }
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.Message.ToString();
                ZhiFang.Common.Log.Log.Debug(e.ToString());
            }
            return brdv;
        }

        public BaseResultDataValue UpdateReportMicroFull(List<ReportMicroFull> models)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (models == null || models.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数为空";
                return brdv;
            }
            try
            {
                BReportMicro brm = new BReportMicro();
                foreach (var item in models)
                {
                    brm.UpdateReportMicroFull(item);
                }
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.Message.ToString();
                ZhiFang.Common.Log.Log.Debug(e.ToString());
            }
            return brdv;
        }

        public BaseResultDataValue UpdateReportMarrowFull(List<ReportMarrowFull> models)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (models == null || models.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数为空";
                return brdv;
            }
            try
            {
                BReportMarrow brm = new BReportMarrow();
                foreach (var item in models)
                {
                    brm.UpdateReportMarrowFull(item);
                }
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.Message.ToString();
                ZhiFang.Common.Log.Log.Debug(e.ToString());
            }
            return brdv;
        }

        public BaseResultDataValue AddSC_Operation(List<Model.SC_Operation> models)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (models == null || models.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数为空";
                return brdv;
            }
            try
            {
                BSC_Operation brm = new BSC_Operation();
                foreach (var item in models)
                {
                    brm.Add(item);
                }
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.Message.ToString();
                ZhiFang.Common.Log.Log.Debug(e.ToString());
            }
            return brdv;
        }

        public BaseResultDataValue CreatVoice(string txt)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                if (txt == null || txt.Trim() == "")
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "参数错误！";
                    return brdv;
                }
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Debug("CreatVoice.txt:" + txt + ",IP:" + ip);
                string basepath = System.AppDomain.CurrentDomain.BaseDirectory + SCMsgVodioFilePath + "\\";
                if (FilesHelper.CheckAndCreatDir(basepath))
                {
                    string filename = txt + ".wav";
                    using (var speechSyn = new SpeechSynthesizer())
                    {

                        if (FilesHelper.CheckDirFile(basepath, filename))
                        {

                        }
                        else
                        {
                            speechSyn.Volume = 60;//音量
                            speechSyn.Rate = 0;//语速
                            speechSyn.SetOutputToDefaultAudioDevice();
                            speechSyn.SetOutputToWaveFile(basepath + filename);//"D:\\Record.wav"
                            speechSyn.Speak(txt);
                        }
                    }

                    brdv.ResultDataValue = SCMsgVodioFilePath + "\\" + filename;
                    brdv.success = true;
                    return brdv;
                }
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "查询错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("CreatVoice.异常：" + ex.ToString());
            }
            return brdv;
        }

        public BaseResultDataValue UpdateWebConfig(List<webconfigVo> model)
        {
            ZhiFang.Common.Log.Log.Debug("调用修改webconfig配置文件开始.UpdateWebConfig");
            BaseResultDataValue brdv = new BaseResultDataValue();
            XmlDocument doc = new XmlDocument();
            if (model == null || model.Count == 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数为空!";
                return brdv;
            }
            try
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Info("UpdateWebConfig.操作者IP地址:" + ip);

                string path = HttpContext.Current.Request.ApplicationPath;
                doc.Load(System.AppDomain.CurrentDomain.BaseDirectory + "Web.config");
                XmlNode node;
                XmlElement element;
                node = doc.SelectSingleNode("//appSettings");
                foreach (var item in model)
                {
                    element = (XmlElement)node.SelectSingleNode("//add[@key='" + item.key + "']");
                    element.SetAttribute("value", item.value);
                }
                doc.Save(System.AppDomain.CurrentDomain.BaseDirectory + "Web.config");

                brdv.success = true;
                brdv.ResultDataValue = "";
                ZhiFang.Common.Log.Log.Debug("调用修改webconfig配置文件结束.UpdateWebConfig");
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = ex.Message;
                ZhiFang.Common.Log.Log.Debug("调用修改webconfig配置文件错误.UpdateWebConfig.error:" + ex.Message);
            }
            return brdv;
        }

        public BaseResultDataValue LoadWebConfig(string key)
        {
            ZhiFang.Common.Log.Log.Debug("调用查询webconfig配置文件开始.LoadWebConfig");
            BaseResultDataValue brdv = new BaseResultDataValue();
            List<string> keylist = new List<string>();
            keylist.Add("DBSourceType");
            keylist.Add("ReportFormQueryPrintConnectionString");
            keylist.Add("HistoryConnectionString");
            keylist.Add("BackupsConnectionString");
            keylist.Add("LISConnectionString");
            keylist.Add("IsLisAddPrintTime");
            keylist.Add("IsMEGroupSampleFormAddPrintTime");
            keylist.Add("LabStarConnectionString");
            keylist.Add("StaticUserNo");
            keylist.Add("PointType");
            keylist.Add("IsUseFrxGeneratePDF");
            if (key != null && !key.Equals(""))
            {
                keylist.Add(key);
            }
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("key");
            dataTable.Columns.Add("value");
            try
            {
                for (var i = 0; i < keylist.Count; i++)
                {
                    string value = ConfigHelper.GetConfigString(keylist[i]);
                    ZhiFang.Common.Log.Log.Debug("LoadWebConfig.获取webconfig文件中<" + keylist[i] + ">参数的值");
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        ZhiFang.Common.Log.Log.Debug("LoadWebConfig.webconfig文件无<" + keylist[i] + ">参数相关配置");
                    }
                }
                for (var i = 0; i < keylist.Count; i++)
                {
                    string value = ConfigHelper.GetConfigString(keylist[i]).ToString();
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["key"] = keylist[i];
                    dataRow["value"] = value;
                    dataTable.Rows.Add(dataRow);
                }
                brdv.success = true;
                brdv.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(dataTable);
                ZhiFang.Common.Log.Log.Debug("调用查询webconfig配置文件结束.LoadWebConfig");
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = ex.Message;
                ZhiFang.Common.Log.Log.Debug("调用查询webconfig配置文件错误.LoadWebConfig.error:" + ex.Message);
            }

            return brdv;
        }

        public BaseResultDataValue DBupdate(string Version)
        {
            ZhiFang.Common.Log.Log.Debug("数据库升级开始.DBupdate");
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
                string ip = endpoint.Address.ToString();
                ZhiFang.Common.Log.Log.Info("DBupdate.操作者IP地址:" + ip);

                bool result = ZhiFang.ReportFormQueryPrint.DBUpdate.DBUpdate.DataBaseUpdate("");

                brdv.success = result;
                //brdv.ResultDataValue = result;

                ZhiFang.Common.Log.Log.Debug("数据库升级结束.DBupdate");
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = ex.Message;
                ZhiFang.Common.Log.Log.Debug("数据库升级错误.DBupdate.error:" + ex.Message);
            }

            return brdv;
        }

        public BaseResultDataValue GetDBVersion(string Version)
        {
            ZhiFang.Common.Log.Log.Debug("获取程序集以及数据库版本开始.GetDBVersion");
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                Dictionary<string, string> VersionComparison = DBUpdate.DBUpdate.GetVersionComparison();
                string dbVersion = DBUpdate.DBUpdate.GetDataBaseCurVersion();
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("ProcedureVersion");
                dataTable.Columns.Add("DBVersion");
                foreach (var item in VersionComparison)
                {
                    Console.WriteLine(item.Key + item.Value);
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ProcedureVersion"] = item.Key;
                    dataRow["DBVersion"] = dbVersion;
                    dataTable.Rows.Add(dataRow);
                }
                brdv.success = true;
                brdv.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(dataTable); ;

                ZhiFang.Common.Log.Log.Debug("获取程序集以及数据库版本结束.GetDBVersion");
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = ex.Message;
                ZhiFang.Common.Log.Log.Debug("获取程序集以及数据库版本错误.GetDBVersion.error:" + ex.Message);
            }

            return brdv;
        }

        public BaseResultDataValue StaticUserLogin(string Account)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = false;
            if (Account == null || Account == "")
            {
                brdv.success = false;
                brdv.ErrorInfo = "用户名不能为空";
                ZhiFang.Common.Log.Log.Debug("StaticUserLogin:用户名不能为空");
                return brdv;
            }
            //获得的客户端ip
            OperationContext context = OperationContext.Current;
            MessageProperties properties = context.IncomingMessageProperties;
            RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            //endpoint.Address + ";" + endpoint.Port.ToString(); //地址和端口
            string ip = endpoint.Address.ToString();
            try
            {
                if (ConfigHelper.GetConfigString("StaticUserNo").Equals(Account))
                {
                    brdv.success = true;
                    Common.Cookie.CookieHelper.Write("webconfigStaticUser", Account);
                    ZhiFang.Common.Log.Log.Info("StaticUserLogin.查询到用户.StaticUserNo:" + Account + " IP地址:" + ip);
                }
                else
                {
                    brdv.success = false;
                    ZhiFang.Common.Log.Log.Debug("StaticUserLogin.未查询到用户");
                }
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.Message.ToString();
                ZhiFang.Common.Log.Log.Error("StaticUserLogin.静态用户登录错误:" + e.ToString());
                return brdv;
            }

            return brdv;
        }

        public BaseResultDataValue LabStarResultMhistory(string Where)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BALLReportForm bal = new BALLReportForm();
            brdv.success = false;
            if (Where == "" || Where == null)
            {
                brdv.ErrorInfo = "where不允许为空";
                return brdv;
            }
            try
            {
                DataSet ds = bal.LabStarResultMhistory(Where);
                StringBuilder sb = new StringBuilder();
                for (var i = 0; i < ds.Tables.Count; i++)
                {
                    var Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<ALLReportFromVo>(ds.Tables[i]);
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented, settings);
                    string a = "";
                    if (i < ds.Tables.Count - 1)
                    {
                        a = ",";
                    }
                    sb.Append(aa + a);
                }
                if (sb.ToString().Length > 0 && sb.ToString() != "[]")
                {
                    brdv.ResultDataValue = "[" + sb.ToString() + "]";
                }
                else
                {
                    brdv.ResultDataValue = "";
                }

                brdv.success = true;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("LabStarResultMhistory.异常:" + e.ToString());
                brdv.success = false;
                //brdv.ErrorInfo = "程序异常请查看系统日志！";
                brdv.ErrorInfo = e.ToString();
            }
            return brdv;
        }

        public BaseResultDataValue NewLabStarResultMhistory(string Where)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BALLReportForm bal = new BALLReportForm();
            brdv.success = false;
            if (Where == "" || Where == null)
            {
                brdv.ErrorInfo = "where不允许为空";
                return brdv;
            }
            try
            {
                DataSet ds = bal.NewLabStarResultMhistory(Where);
                if (ds != null && ds.Tables != null && ds.Tables.Count>0 && ds.Tables[0].Rows.Count > 0)
                {
                    brdv.ResultDataValue = JsonConvert.SerializeObject(ds.Tables[0]);
                }
                else
                {
                    brdv.ResultDataValue = "";
                }

                brdv.success = true;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("NewLabStarResultMhistory.异常:" + e.ToString());
                brdv.success = false;
                //brdv.ErrorInfo = "程序异常请查看系统日志！";
                brdv.ErrorInfo = e.ToString();
            }
            return brdv;
        }

        public BaseResultDataValue LabStarResultHistory(string PatNo, string ItemNo, string Table, string Where)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                DataTable dt = new DataTable();
                string[] param = new string[4];

                if (PatNo.Length < 1)
                {
                    brdv.ErrorInfo = "Error:病历号空!";
                    brdv.success = false;
                    return brdv;
                }
                if (ItemNo.Length < 1)
                {
                    brdv.ErrorInfo = "Error:项目编码空!";
                    brdv.success = false;
                    return brdv;
                }
                if (Table.Length < 1)
                {
                    brdv.ErrorInfo = "Error:表名空!";
                    brdv.success = false;
                    return brdv;
                }
                if (Where.Length < 1)
                {
                    brdv.ErrorInfo = "Error:条件空!";
                    brdv.success = false;
                    return brdv;
                }
                param[0] = PatNo;
                param[1] = ItemNo;
                param[2] = Table;
                param[3] = " and " + Where;


                dt = barf.LabStarGetReportValue(param, null);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<HistoryVO> ls = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<HistoryVO>(dt);
                    var settings = new JsonSerializerSettings();

                    string aa = JsonConvert.SerializeObject(ls, Newtonsoft.Json.Formatting.Indented, settings);
                    brdv.ResultDataValue = aa;
                    brdv.success = true;
                }
                else
                {
                    brdv.ErrorInfo = "Error:无对比记录!";
                    brdv.success = false;
                }
                return brdv;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                brdv.ErrorInfo = "ResultHistory:" + ex.ToString();
                brdv.success = false;
                return brdv;
            }
        }

        public BaseResultDataValue SampleStateTailAfter(string ReportFormId)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                if (null == ReportFormId || "".Equals(ReportFormId))
                {
                    brdv.ErrorInfo = "Error:报告单ID为空!";
                    brdv.success = false;
                    return brdv;
                }
                List<SampleStateVo> ssv = barf.SampleStateTailAfter(ReportFormId);
                if (ssv != null && ssv.Count > 0)
                {
                    var settings = new JsonSerializerSettings();
                    string aa = JsonConvert.SerializeObject(ssv, Newtonsoft.Json.Formatting.Indented, settings);
                    brdv.ResultDataValue = "{\"total\":" + ssv.Count + ",\"rows\":" + aa + "}"; ;
                    brdv.success = true;
                }
                else
                {
                    brdv.ErrorInfo = "Error:没有找到样本状态信息!";
                    brdv.success = false;
                }
                return brdv;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("SampleStateTailAfter.Error:" + ex.ToString());
                brdv.ErrorInfo = "SampleStateTailAfter:" + ex.ToString();
                brdv.success = false;
                return brdv;
            }
        }

        public BaseResultDataValue SelfhelpCustomizationServiceGetWhere(string barCode)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (barCode.Trim() == "" || barCode == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "barCode参数不能为空！";
                return baseResultDataValue;
            }
            JObject resultroot = new JObject();
            try
            {
                ZhiFang.Common.Log.Log.Debug("自助打印调用第三方接口获取查询条件: barCode=" + barCode);
                DateTime dt = DateTime.Now;
                string postdatastr = "paraJson: {\"barCode\": \"" + barCode + "\", \"deviceCode\": \"8000001609029651\",\"useCityCode\": \"371000\",\"idCard\": \"\",\"hospitalCode\": \"494430085\",\"channelCode\": \"01\",\"medStepCode\": \"000000\",\"deptCode\": \"9900\", \"useTime\": \"" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "\",\"returnValueType\": \"01\" }" +
                            "\r\n appid: 69y5223kcei8kx6ye371" +
                            "\r\n time:1583768492332" +
                            "\r\n nonce_str: 0HMRLO9SBTPIUC1" +
                            "\r\n sign: 8cLx9AwzJiyVg7vG8bOOPRVgx2g=";
                string urlwhere = "?paraJson={\"barCode\": \"" + barCode + "\", \"deviceCode\": \"8000001609029651\",\"useCityCode\": \"371000\",\"idCard\": \"\",\"hospitalCode\": \"494430085\",\"channelCode\": \"01\",\"medStepCode\": \"000000\",\"deptCode\": \"9900\", \"useTime\": \"" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "\",\"returnValueType\": \"01\" }" +
                            "&appid=69y5223kcei8kx6ye371" +
                            "&time=1583768492332" +
                            "&nonce_str=0HMRLO9SBTPIUC1" +
                            "&sign=8cLx9AwzJiyVg7vG8bOOPRVgx2g=";
                ZhiFang.Common.Log.Log.Debug("自助打印调用第三方接口获取查询条件接口参数:" + postdatastr);

                string resultstr = HttpRequestHelp.Post(postdatastr, "json", ConfigHelper.GetConfigString("SelfHelpGetWhereURL") + urlwhere, 10);
                ZhiFang.Common.Log.Log.Debug("自助打印调用第三方接口获取查询条件接口结果:" + resultstr);
                resultroot = JsonConvert.DeserializeObject(resultstr) as JObject;
                if (resultstr == "")
                {
                    ZhiFang.Common.Log.Log.Debug("自助打印调用第三方接口失败: barCode=" + barCode);
                }
                if (resultroot["success"] == null || resultroot["success"].ToString().ToLower() == "false")
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "自助打印调用第三方接口错误！:" + resultroot["msg"].ToString().ToLower();
                    return baseResultDataValue;
                }
                else
                {
                    baseResultDataValue.success = true;
                    baseResultDataValue.ResultDataValue = resultroot["data"].ToString();
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "自助打印调用第三方接口错误！:" + ex.ToString();
                return baseResultDataValue;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue SelfhelpCustomizationServiceGetWhereJiNan(string barCode)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (barCode.Trim() == "" || barCode == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "健康码参数不能为空！";
                return baseResultDataValue;
            }
            JObject resultroot = new JObject();
            try
            {
                ZhiFang.Common.Log.Log.Debug("SelfhelpCustomizationServiceGetWhereJiNan.自助打印调用第三方接口获取查询条件: barCode=" + barCode);
                DateTime dt = DateTime.Now;
                string postdatastr = "consumerAppId:mcu" +
                            "\r\n serviceName:mcu-api-readQRCode" +
                            "\r\n params:{\"klx\":\"05\",\"ywid\":\"jnszxyy370100\",\"ewmid\":\"" + barCode + "\",\"deviceCode\":\"dzjkk370100\",\"channelCode\":\"02\",\"medStepCode\":\"00000011\",\"deptCode\":\"0500\"}";
                string urlwhere = "?consumerAppId=mcu" +
                            "&serviceName=mcu-api-readQRCode" +
                            "&params={\"klx\":\"05\",\"ywid\":\"jnszxyy370100\",\"ewmid\":\"" + barCode + "\",\"deviceCode\":\"dzjkk370100\",\"channelCode\":\"02\",\"medStepCode\":\"00000011\",\"deptCode\":\"0500\"}";
                ZhiFang.Common.Log.Log.Debug("SelfhelpCustomizationServiceGetWhereJiNan.自助打印调用第三方接口获取查询条件接口参数:" + postdatastr);

                string resultstr = HttpRequestHelp.Post(postdatastr, "other", ConfigHelper.GetConfigString("SelfHelpGetWhereURL") + urlwhere, 10);
                ZhiFang.Common.Log.Log.Debug("SelfhelpCustomizationServiceGetWhereJiNan.自助打印调用第三方接口获取查询条件接口结果:" + resultstr);
                resultroot = JsonConvert.DeserializeObject(resultstr) as JObject;
                if (resultstr == "")
                {
                    ZhiFang.Common.Log.Log.Debug("SelfhelpCustomizationServiceGetWhereJiNan.自助打印调用第三方接口失败: barCode=" + barCode);
                }
                if (resultroot["success"] == null || resultroot["success"].ToString().ToLower() == "false")
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "SelfhelpCustomizationServiceGetWhereJiNan.自助打印调用第三方接口错误！:" + resultroot["msg"].ToString().ToLower();
                    return baseResultDataValue;
                }
                else
                {
                    baseResultDataValue.success = true;
                    baseResultDataValue.ResultDataValue = resultroot["data"].ToString();
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "自助打印调用第三方接口错误！:" + ex.ToString();
                return baseResultDataValue;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue SelectNRequestForm(string Where, string fields, int page, int limit, string sort, string SerialNo)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                #region 查询报告
                string urlWhere = Where.Replace("\"", "'");

                string urlModel = fields;
                int urlPage = page;
                int urlLimit = limit;
                //ZhiFang.Common.Log.Log.Debug("urlType_urlModel" + urlModel);

                if (string.IsNullOrEmpty(urlWhere))
                {
                    urlWhere = " 1=1 ";
                }

                if (urlWhere.Length < 4)
                {
                    brdv.ErrorInfo = "Error:请选择查询条件!";
                    brdv.success = false;
                    return brdv;
                }
                //报告发布程序的状态字段
                //urlWhere += " and resultsend='1' ";

                if (urlModel.Length < 1)
                {
                    brdv.ErrorInfo = "Error:请选择要查询的数据!";
                    brdv.success = false;
                    return brdv;
                }

                DataSet ds = new DataSet();

                //string countwhere = urlWhere.ToUpper();
                string countwhere = urlWhere;
                if (countwhere.IndexOf("ORDER") >= 0)
                {
                    countwhere = countwhere.Substring(0, countwhere.IndexOf("ORDER"));
                }
                // if (countwhere.IndexOf("order") >= 0)
                //{ countwhere = countwhere.Substring(0, countwhere.IndexOf("order"));         }

                int dsCount = 0;
                DAL.MSSQL.MSSQL66.NRequestForm NRequestFormBLL = new DAL.MSSQL.MSSQL66.NRequestForm();
                dsCount = NRequestFormBLL.GetCountForm(countwhere);

                int _reportformlimit = 5000;
                if (Common.ConfigHelper.GetConfigInt("SearchReportFormLimit") != null)
                {
                    _reportformlimit = Common.ConfigHelper.GetConfigInt("SearchReportFormLimit").Value;
                }
                if (dsCount > _reportformlimit)
                {
                    brdv.ErrorInfo = "Error:" + dsCount + "数据超过" + _reportformlimit + "条,请从新选择查询条件!";
                    brdv.success = false;
                    return brdv;
                }

                if (dsCount <= 0)
                {
                    brdv.ResultDataValue = "{\"total\":" + dsCount + ",\"rows\":[]}"; ;
                    brdv.success = true;
                    return brdv;
                }

                ds = NRequestFormBLL.GetList_FormFull(urlModel, urlWhere, CommonServiceMethod.GetSortHQL(sort));

                List<NRequestForm> ils = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<NRequestForm>(ds.Tables[0]);
                List<NRequestForm> Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.Pagination<NRequestForm>(Convert.ToInt32(urlPage), Convert.ToInt32(urlLimit), ils);
                var settings = new JsonSerializerSettings();
                string aa = JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented, settings);

                brdv.ResultDataValue = "{\"total\":" + ils.Count + ",\"rows\":" + aa + "}";
                brdv.success = true;
                return brdv;
                #endregion
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                brdv.ErrorInfo = "SelectNRequestForm:" + ex.ToString();
                brdv.success = false;
                return brdv;
            }
        }

        public BaseResultDataValue SelectRequestFormAndReportForm(string Where, string fields, int page, int limit, string sort, string SerialNo)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                #region 查询报告
                string urlWhere = Where.Replace("\"", "'");

                string urlModel = fields;
                int urlPage = page;
                int urlLimit = limit;
                //ZhiFang.Common.Log.Log.Debug("urlType_urlModel" + urlModel);

                if (string.IsNullOrEmpty(urlWhere))
                {
                    urlWhere = " 1=1 ";
                }

                if (urlWhere.Length < 4)
                {
                    brdv.ErrorInfo = "Error:请选择查询条件!";
                    brdv.success = false;
                    return brdv;
                }
                //报告发布程序的状态字段
                //urlWhere += " and resultsend='1' ";

                if (urlModel.Length < 1)
                {
                    brdv.ErrorInfo = "Error:请选择要查询的数据!";
                    brdv.success = false;
                    return brdv;
                }

                DataSet ds = new DataSet();

                //string countwhere = urlWhere.ToUpper();
                string countwhere = urlWhere;
                if (countwhere.IndexOf("ORDER") >= 0)
                {
                    countwhere = countwhere.Substring(0, countwhere.IndexOf("ORDER"));
                }
                DAL.MSSQL.MSSQL66.NRequestForm NRequestFormBLL = new DAL.MSSQL.MSSQL66.NRequestForm();
                DataSet reportds = barf.GetList_FormFull(urlModel, " SerialNo is not null and " + urlWhere);
                DataSet requestds = bqf.GetList_FormFull(urlModel, " FromQCL <> '全部提取' and SerialNo is not null and " + urlWhere);
                DataSet nrequestds = NRequestFormBLL.GetList_NReportFormFull(urlModel, "SerialNo is not null and " + urlWhere, CommonServiceMethod.GetSortHQL(sort));
                List<ReportFormVO> reportdslist = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<ReportFormVO>(reportds.Tables[0]);
                List<ReportFormVO> requestdslist = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<ReportFormVO>(requestds.Tables[0]);
                List<ReportFormVO> nrequestdslist = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<ReportFormVO>(nrequestds.Tables[0]);
                List<ReportFormVO> ReportFormVOList = new List<ReportFormVO>();
                if ((reportdslist != null && reportdslist.Count > 0) || (requestdslist != null && requestdslist.Count > 0) || (nrequestdslist != null && nrequestdslist.Count > 0))
                {
                    for (int i = 0; i < reportdslist.Count; i++)
                    {
                        reportdslist[i].ReportStatus = "检验完成";
                        ReportFormVOList.Add(reportdslist[i]);
                    }
                    for (int i = 0; i < requestdslist.Count; i++)
                    {

                        requestdslist[i].ReportStatus = "检验中";
                        ReportFormVOList.Add(requestdslist[i]);
                    }
                    for (int i = 0; i < nrequestdslist.Count; i++)
                    {

                        nrequestdslist[i].ReportStatus = "未上机";
                        ReportFormVOList.Add(nrequestdslist[i]);
                    }
                }
                else
                {
                    brdv.ResultDataValue = "{\"total\":" + 0 + ",\"rows\":[]}"; ;
                    brdv.success = true;
                    return brdv;

                }

                List<ReportFormVO> Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.Pagination<ReportFormVO>(Convert.ToInt32(urlPage), Convert.ToInt32(urlLimit), ReportFormVOList);
                var settings = new JsonSerializerSettings();
                string aa = JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented, settings);

                brdv.ResultDataValue = "{\"total\":" + ReportFormVOList.Count + ",\"rows\":" + aa + "}";
                brdv.success = true;
                return brdv;
                #endregion
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                brdv.ErrorInfo = "SelectRequestFormAndReportForm:" + ex.ToString();
                brdv.success = false;
                return brdv;
            }
        }

        public BaseResultDataValue SelectRequestFormAndReportFormCount(string Where, string fields, int page, int limit, string sort, string SerialNo)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                #region 查询报告
                string urlWhere = Where.Replace("\"", "'");

                string urlModel = fields;
                int urlPage = page;
                int urlLimit = limit;
                //ZhiFang.Common.Log.Log.Debug("urlType_urlModel" + urlModel);

                if (string.IsNullOrEmpty(urlWhere))
                {
                    urlWhere = " 1=1 ";
                }

                if (urlWhere.Length < 4)
                {
                    brdv.ErrorInfo = "Error:请选择查询条件!";
                    brdv.success = false;
                    return brdv;
                }
                //报告发布程序的状态字段
                //urlWhere += " and resultsend='1' ";

                if (urlModel.Length < 1)
                {
                    brdv.ErrorInfo = "Error:请选择要查询的数据!";
                    brdv.success = false;
                    return brdv;
                }

                DataSet ds = new DataSet();

                //string countwhere = urlWhere.ToUpper();
                string countwhere = urlWhere;
                if (countwhere.IndexOf("ORDER") >= 0)
                {
                    countwhere = countwhere.Substring(0, countwhere.IndexOf("ORDER"));
                }
                DAL.MSSQL.MSSQL66.NRequestForm NRequestFormBLL = new DAL.MSSQL.MSSQL66.NRequestForm();
                DataSet reportds = barf.GetList_FormFull(urlModel, "SerialNo is not null and " + urlWhere);
                DataSet requestds = bqf.GetList_FormFull(urlModel, " FromQCL <> '全部提取' and SerialNo is not null and " + urlWhere);
                DataSet nrequestds = NRequestFormBLL.GetList_NReportFormFull(urlModel, "SerialNo is not null and " + urlWhere, CommonServiceMethod.GetSortHQL(sort));
                List<ReportFormVO> reportdslist = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<ReportFormVO>(reportds.Tables[0]);
                List<ReportFormVO> requestdslist = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<ReportFormVO>(requestds.Tables[0]);
                List<ReportFormVO> nrequestdslist = ZhiFang.ReportFormQueryPrint.Common.DtToJson.GetListColumns<ReportFormVO>(nrequestds.Tables[0]);
                List<ReportFormVO> ReportFormVOList = new List<ReportFormVO>();
                if ((reportdslist != null && reportdslist.Count > 0) || (requestdslist != null && requestdslist.Count > 0) || (nrequestdslist != null && nrequestdslist.Count > 0))
                {
                    for (int i = 0; i < reportdslist.Count; i++)
                    {
                        ReportFormVOList.Add(reportdslist[i]);
                    }
                    for (int i = 0; i < requestdslist.Count; i++)
                    {
                        ReportFormVOList.Add(requestdslist[i]);
                    }
                    for (int i = 0; i < nrequestdslist.Count; i++)
                    {
                        ReportFormVOList.Add(nrequestdslist[i]);
                    }
                }
                else
                {
                    brdv.ResultDataValue = "{\"total\":" + 0 + ",\"rows\":[]}"; ;
                    brdv.success = true;
                    return brdv;
                }
                var groupformVOs = ReportFormVOList.GroupBy(a => new { a.CNAME, a.PatNo });
                List<ReportFormVO> lestReportFormVOList = new List<ReportFormVO>();
                foreach (var item in groupformVOs)
                {
                    ReportFormVO reportFormVO = item.First();
                    reportFormVO.ItemNum = item.Count();
                    lestReportFormVOList.Add(reportFormVO);
                }
                List<ReportFormVO> Result = ZhiFang.ReportFormQueryPrint.Common.DtToJson.Pagination<ReportFormVO>(Convert.ToInt32(urlPage), Convert.ToInt32(urlLimit), lestReportFormVOList);
                var settings = new JsonSerializerSettings();
                string aa = JsonConvert.SerializeObject(Result, Newtonsoft.Json.Formatting.Indented, settings);

                brdv.ResultDataValue = "{\"total\":" + lestReportFormVOList.Count + ",\"rows\":" + aa + "}";
                brdv.success = true;
                return brdv;
                #endregion
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                brdv.ErrorInfo = "SelectRequestFormAndReportFormCount:" + ex.ToString();
                brdv.success = false;
                return brdv;
            }
        }

        public BaseResultDataValue ZipAndDowanloadThePDF(string FolderPath)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            var itemDirectory = System.AppDomain.CurrentDomain.BaseDirectory;//获取项目物理路径
            //rpasd要打包的目标文件夹完整路径
            string fullPath = itemDirectory + "RarReportFormFiles\\" + FolderPath;
            List<string> rpasd = new List<string>();
            rpasd.Add(fullPath);
            try
            {
                bool isZip = DotNetZipHelp.CompressMulti(rpasd, fullPath + ".zip", false);
                if (isZip)
                {
                    //压缩成功后删除文件夹里的文件
                    foreach (string d in Directory.GetFileSystemEntries(fullPath))
                    {
                        if (File.Exists(d))
                        {
                            {
                                FileInfo fi = new FileInfo(d);
                                if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                                    fi.Attributes = FileAttributes.Normal;
                                File.Delete(d);//直接删除其中的文件  
                            }
                        }
                    }
                    Directory.Delete(itemDirectory + "RarReportFormFiles\\" + FolderPath);
                    brdv.ResultDataValue = ZhiFang.ReportFormQueryPrint.Common.JsonSerializer.JsonDotNetSerializerNoEnterSpace("RarReportFormFiles/" + FolderPath + ".zip");
                    brdv.success = true;
                }
                else
                {
                    brdv.ErrorInfo = "压缩失败";
                    brdv.success = false;
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("ZipAndDowanloadThePDF.异常:" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = e.ToString();
            }
            brdv.ResultDataValue = ZhiFang.ReportFormQueryPrint.Common.JsonSerializer.JsonDotNetSerializerNoEnterSpace("RarReportFormFiles/" + FolderPath + ".zip");

            return brdv;
        }

        public BaseResultDataValue MoveThePDFByReportFormID(List<string> Reportfields, string ReportFormID, string Folderfield)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = false;
            string[] ReportFormIDarr = ReportFormID.Split('*');//拆分
            var itemDirectory = System.AppDomain.CurrentDomain.BaseDirectory;//获取项目物理路径

            //要下载的PDF文件名
            List<string> RarPDFfiles = new List<string>();
            try
            {
                //通过报告id查询报告信息并拼接下载pdf文件名
                for (int i = 0; i < ReportFormIDarr.Count(); i++)
                {
                    //查询报告单信息拼接下载PDF文件名
                    ReportForm reportForm = brf.GetModel(ReportFormIDarr[i]);
                    if (reportForm.paritemname.Length > 10)
                    {
                        reportForm.paritemname = reportForm.paritemname.Substring(0, 9);
                    }

                    RarPDFfiles.Add(itemDirectory + "RarReportFormFiles\\" + Folderfield + "\\" + reportForm.ReceiveDate.ToString().Replace("/", "-").Split(' ')[0] + "_" + reportForm.PatNo + "_" + reportForm.CName + "_" + reportForm.paritemname + "_" + reportForm.FormNo.Replace(";", "-") + ".PDF");
                }

                //创建存放打包文件夹的文件夹RarReportFormFiles
                if (!Directory.Exists(itemDirectory + "RarReportFormFiles"))//判断文件夹是否存在 
                {
                    Directory.CreateDirectory(itemDirectory + "RarReportFormFiles");//不存在则创建文件夹 
                }
                //创建要打包的文件夹
                if (!Directory.Exists(itemDirectory + "RarReportFormFiles\\" + Folderfield))//判断文件夹是否存在 
                {
                    Directory.CreateDirectory(itemDirectory + "RarReportFormFiles\\" + Folderfield);//不存在则创建文件夹 
                }


                //复制生成的报告单到RarReportFormFiles文件夹
                for (int i = 0; i < Reportfields.Count(); i++)
                {
                    Reportfields[i] = itemDirectory + Reportfields[i];
                    if (!File.Exists(RarPDFfiles[i]))
                    {
                        File.Copy(Reportfields[i], RarPDFfiles[i]);
                        brdv.success = true;
                    }
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("MoveThePDFByReportFormID.异常:" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = e.ToString();
            }

            return brdv;
        }

        public bool ZhongRiBloodGlucoseTesting(string ReportFormID, string SectionType, out BaseResultDataValue brdv, int flag , string SectionNo, string ModelType, string sortFields, string previewOrPdf)
        {
            bool matchFlag = false;
            brdv = new BaseResultDataValue();
            #region 中日定制-糖耐量
            string BloodGlucoseTestingConfiguration = ConfigHelper.GetConfigString("BloodGlucoseTestingConfiguration");
            if (!string.IsNullOrWhiteSpace(BloodGlucoseTestingConfiguration) && BloodGlucoseTestingConfiguration == "1")
            {
                BRequestForm bqf = new BLL.BRequestForm();
                DataTable reportform = brf.GetListByDataSource(ReportFormID);

                if (reportform != null && reportform.Rows.Count > 0)
                {
                    if (reportform.Rows[0]["zdy4"] == null || reportform.Rows[0]["zdy4"].ToString().Trim() == "")
                    {
                        return false;
                    }
                    DateTime ReceiveDate = DateTime.Parse(reportform.Rows[0]["zdy4"].ToString().Trim());
                    string receiveDate = ReceiveDate.ToString("yyyy/MM/d");

                    string where = "zdy9='" + reportform.Rows[0]["ZDY9"].ToString() + "' and zdy4>='" + receiveDate + "' and zdy4<'" + receiveDate + " 23:59:59'";
                    //查NRequestForm表，对应病人当天数据
                    List<NRequestForm> NRequestFormList = bnrf.GetModelList_FormFull("", where);
                    if (NRequestFormList != null && NRequestFormList.Count > 0)
                    {

                        List<string> serialNos = new List<string>();
                        for (int i = 0; i < NRequestFormList.Count; i++)
                        {
                            serialNos.Add(NRequestFormList[i].SerialNo);

                        }
                        #region 先判断本分报告是不是特殊报告，如果不是直接不走此流程
                        List<Model.NRequestItem> NRequestItemList1 = bnri.GetModelList("SerialNo in ('" + string.Join("','", serialNos) + "') and Paritemno in (101010,101111,101112,101113,101114,101115)");//糖耐量
                        List<Model.NRequestItem> NRequestItemList2 = bnri.GetModelList("SerialNo in ('" + string.Join("','", serialNos) + "') and Paritemno in (701036,701043,701039,701041,701045,701110,701035,701037)");//胰岛素
                        List<Model.NRequestItem> NRequestItemList3 = bnri.GetModelList("SerialNo in ('" + string.Join("','", serialNos) + "') and Paritemno in (406117,406129,406130,406131,701100)");//C肽
                        List<Model.NRequestItem> NRequestItem1 = NRequestItemList1.Where(a => (a.SerialNo.ToString() == reportform.Rows[0]["SerialNo"].ToString())).ToList();
                        List<Model.NRequestItem> NRequestItem2 = NRequestItemList2.Where(a => (a.SerialNo.ToString() == reportform.Rows[0]["SerialNo"].ToString())).ToList();
                        List<Model.NRequestItem> NRequestItem3 = NRequestItemList3.Where(a => (a.SerialNo.ToString() == reportform.Rows[0]["SerialNo"].ToString())).ToList();
                        bool isSpecial = false;
                        string errorInfo = "各时间点血糖报告尚未全部完成，请稍后再试，全部完成后系统将把各时点血糖合并打印。";
                        //对应NRequestItem表,特殊项目号，并且两项以上
                        if (NRequestItemList1 != null && NRequestItemList1.Count >= 2 && NRequestItem1.Count>0)
                        {
                            isSpecial = true;
                        }
                        else if(NRequestItemList2 != null && NRequestItemList2.Count >= 2 && NRequestItem2.Count > 0)
                        {
                            NRequestItemList1 = NRequestItemList2;
                            errorInfo = "各时间点胰岛素报告尚未全部完成，请稍后再试，全部完成后系统将把各时点胰岛素合并打印。";
                            isSpecial = true;
                            ZhiFang.Common.Log.Log.Debug("ZhongRiBloodGlucoseTesting.胰岛素检测");
                        }
                        else if (NRequestItemList3 != null && NRequestItemList3.Count >= 2 && NRequestItem3.Count > 0)
                        {
                            NRequestItemList1 = NRequestItemList3;
                            errorInfo = "各时间点C肽报告尚未全部完成，请稍后再试，全部完成后系统将把各时点C肽合并打印。";
                            isSpecial = true;
                            ZhiFang.Common.Log.Log.Debug("ZhongRiBloodGlucoseTesting.C肽检测");
                        }
                        else
                        {
                            return false;
                        }
                        #endregion
                        #region 是特殊报告
                        if (isSpecial)
                        {
                            ZhiFang.Common.Log.Log.Debug("ZhongRiBloodGlucoseTesting.特殊项目2项及以上");
                            //项目可能开重复，重复的项目如果有一个对应的检验单签收或核收，那么其他项目则作废
                            var itemGroups = NRequestItemList1.GroupBy(a => a.ParItemNo);//按项目号分组
                            List<string> itemSerialNoList = new List<string>();//核收的项目申请单号集合
                            ZhiFang.Common.Log.Log.Debug("itemGroups.Count" + itemGroups.Count());
                            foreach (var group in itemGroups)
                            {
                                //ZhiFang.Common.Log.Log.Debug("group.Key" + group.Key); 
                                foreach (var item in group)
                                {
                                    if (item.RECEIVEFLAG==1) {
                                        itemSerialNoList.Add(item.SerialNo);
                                        
                                    }
                                }
                            }
                            //判断所有特殊项目是否全部核收
                            if (itemSerialNoList.Count != itemGroups.Count())
                            {
                                brdv.success = false;
                                brdv.ErrorInfo = errorInfo;
                                return true;
                            }
                            //特殊项目全部核收再查看报告单是否有在检验中
                            ZhiFang.Common.Log.Log.Debug("ZhongRiBloodGlucoseTesting.特殊项目全部核收");
                            int requestFormCount = bqf.GetCountFormFull("zdy9='" + reportform.Rows[0]["ZDY9"].ToString() + "'  and SerialNo in ('" + string.Join("','", itemSerialNoList) + "')");
                            if (requestFormCount > 0)
                            {
                                ZhiFang.Common.Log.Log.Debug("ZhongRiBloodGlucoseTesting.报告单有"+ requestFormCount + "份正在检验中");
                                brdv.success = false;
                                brdv.ErrorInfo = errorInfo;
                                return true;
                            }
                            //均完成后，判断是否全部二审
                            int finalSendCount = barf.GetCountFormFull("zdy9='" + reportform.Rows[0]["ZDY9"].ToString() + "'  and SerialNo in ('" + string.Join("','", itemSerialNoList) + "')");
                            if (finalSendCount != itemSerialNoList.Count)
                            {
                                ZhiFang.Common.Log.Log.Debug("ZhongRiBloodGlucoseTesting.报告单没有全部二审");
                                brdv.success = false;
                                brdv.ErrorInfo = errorInfo;
                                return true;
                            }
                            //所有报告检验完成，开始合成
                            string SerialNos = string.Join(",", itemSerialNoList);
                            ZhiFang.Common.Log.Log.Debug("ZhongRiBloodGlucoseTesting.所有报告检验完成，开始合成："+ SerialNos);
                            if (previewOrPdf=="preview")
                            { 
                                //如果都检验完成则合并所有item到一张报告里
                                brdv = this.PreviewReportExtPageName(ReportFormID, SectionNo, SectionType, ModelType, null, sortFields, SerialNos);
                            }
                            else
                            {
                                //如果都检验完成则合并所有item到一张pdf里
                                var listreportformfile1 = bprf.CreatReportFormFilesGlucoseToleranceZhongRi(new List<string>() { ReportFormID }, ReportFormTitle.center, ReportFormFileType.PDF, SectionType, SerialNos, flag);
                                if (listreportformfile1.Count > 0)
                                {
                                    brdv.ResultDataValue = ZhiFang.ReportFormQueryPrint.Common.JsonSerializer.JsonDotNetSerializerNoEnterSpace(listreportformfile1[0]);
                                    brdv.success = true;
                                }
                            }
                            return true;
                        }
                        #endregion
                    }
                }
            }
            #endregion
            return matchFlag;
        }
        public BaseResultDataValue LabStarSelectReportSort(string Where, string fields, int page, int limit, string SerialNo, string sort)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                if (!String.IsNullOrWhiteSpace(sort))
                {
                    //sort--[{"property":"CNAME","direction":"DESC"}],去掉不相关符号
                    JArray tempJArray = JArray.Parse(sort);
                    List<string> SortList = new List<string>();
                    string SortStr = "";

                    string returnStr = "";
                    foreach (var tempObject in tempJArray)
                    {
                        SortStr = tempObject["property"].ToString() + " " + tempObject["direction"].ToString().ToUpper();
                        // int tempIndex = SortStr.IndexOf(".");
                        // FirstStr = SortStr.Substring(0, tempIndex);
                        // StringBuilder tempStringBuilder = new StringBuilder(SortStr);
                        // tempStringBuilder.Replace(FirstStr, FirstStr.ToLower(), 0, tempIndex);
                        SortList.Add(SortStr);
                    }
                    if (SortList.Count > 0)
                    {
                        returnStr = string.Join(",", SortList.ToArray());
                    }

                    //string result = sort.Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "").Replace("\"", "");
                    
                    string sortWhere = Where;
                    //Where有setting页面设置字段排序和无设置排序分别处理
                    if (sortWhere.IndexOf("BY") >= 0)
                    {
                        string sortWhere1 = sortWhere.Substring(0, sortWhere.IndexOf("BY") + 3);
                        string sortWhere2 = sortWhere.Substring(sortWhere.IndexOf("BY") + 3);
                        //最终条件=初始条件截止到order by+点击列名的排序+setting页面设置的字段排序
                        Where = sortWhere1 + returnStr + "," + sortWhere2;
                    }
                    else
                    {
                        Where += " ORDER BY " + returnStr;
                    }
                }
                string labidWhere = "";
                if (Common.ConfigHelper.GetConfigString("LabStarIsNeedLabId") != null && Common.ConfigHelper.GetConfigString("LabStarIsNeedLabId") == "1")
                {
                    var labid = CookieHelper.Read("000100");
                    ZhiFang.Common.Log.Log.Debug("LabID:" + labid);
                    //查询条件添加LabID
                    if (!string.IsNullOrEmpty(labid))
                    {
                        labidWhere += " and LabID=" + labid;
                    }
                }
                if (Where.IndexOf("ORDER BY") >= 0)
                {
                    string sortWhere1 = Where.Substring(0, Where.IndexOf("ORDER BY"));
                    string sortWhere2 = Where.Substring(Where.IndexOf("ORDER BY") + 8);
                    //最终条件=初始条件截止到order by+点击列名的排序+setting页面设置的字段排序
                    Where = sortWhere1 + labidWhere + " ORDER BY " + sortWhere2;
                }
                else
                {
                    Where += labidWhere;
                }
                brdv = SelectReport(Where, fields, page, limit, SerialNo);
                return brdv;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("ZhiFang.ReportFormQueryPrint.ServiceWCF.ReportFormService,方法：LabStarSelectReportSort");
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                brdv.ErrorInfo = "查询条件错误，请查看查询条件格式是否正确";
                brdv.success = false;
                return brdv;
            }

        }
    }
}
