using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using ZhiFang.ReportFormQueryPrint.Common;
using ZhiFang.ReportFormQueryPrint.Model;

namespace ZhiFang.ReportFormQueryPrint.ServiceWCF
{
    [ServiceContract(Namespace = "ZhiFang.ReportFormQueryPrint.ServiceWCF")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class LabStarService
    {
        private readonly ZhiFang.ReportFormQueryPrint.BLL.BALLReportForm barf = new BLL.BALLReportForm();
        private readonly ZhiFang.ReportFormQueryPrint.BLL.Print.PrintReportForm bprf = new BLL.Print.PrintReportForm();
        private readonly ZhiFang.ReportFormQueryPrint.BLL.BReportForm brf = new BLL.BReportForm();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ReportFormIds">对应reportformfull表的ReportFormId，对应视图里是RFID</param>
        /// <param name="LabID"></param>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetReportFormPdfByReportFormId", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue GetReportFormPdfByReportFormId(string ReportFormIds,long LabID)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            ZhiFang.Common.Log.Log.Debug("LabStarService.GetReportFormPdfByReportFormId.入参：.ReportFormIds:" + ReportFormIds + ", LabId: " + LabID);
            try
            {
                if (string.IsNullOrEmpty(ReportFormIds))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "ReportFormIds不能为空";
                    return brdv;
                }
                string LabStarIsNeedLabId = ConfigHelper.GetConfigString("LabStarIsNeedLabId");
                string[] ReportFormIdarr = ReportFormIds.Split(',');
                string formWhere = "RFID in ('" + string.Join("','", ReportFormIdarr) + "')";
                long paramLabID = -1;
                if (!string.IsNullOrEmpty(LabStarIsNeedLabId) && LabStarIsNeedLabId=="1")
                {
                    //开启LabID条件，查询报告条件和模板条件添加labid
                    ZhiFang.Common.Log.Log.Debug("LabStarService.GetReportFormPdfByReportFormId.开启LabID条件");
                    formWhere += " and LabID="+ LabID;
                    paramLabID = LabID;
                }
                DataSet formList = brf.GetListTopByWhereAndSort(1000, formWhere, "");//查询报告单
                if (formList != null && formList.Tables != null && formList.Tables.Count > 0 && formList.Tables[0].Rows.Count > 0)
                {
                    List<string> list = new List<string>(ReportFormIds.Split(','));
                    for (int i = 0; i < formList.Tables[0].Rows.Count; i++)
                    {
                        list.Add(formList.Tables[0].Rows[i]["ReportFormID"].ToString());
                    }
                    var listreportformfile = bprf.LabStarCreatReportFormFiles(list, ReportFormTitle.center, ReportFormFileType.PDF, "1", paramLabID);
                    if (listreportformfile.Count > 0)
                    {
                        for (int i = 0; i < listreportformfile.Count; i++)
                        {
                            listreportformfile[i].PDFPath = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/" + listreportformfile[i].PDFPath.Replace(@"\", "/");
                        }
                        brdv.ResultDataValue = ZhiFang.ReportFormQueryPrint.Common.JsonSerializer.JsonDotNetSerializerNoEnterSpace(listreportformfile);
                        brdv.success = true;
                    }
                    else
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = "生成pdf失败：" + ReportFormIds + ", LabId:" + LabID;
                    }
                }
                else
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "未查询到报告，ReportFormIds：" + ReportFormIds + ", LabId:" + LabID;
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("LabStarService.GetReportFormPdfByReportFormId.异常:ReportFormIds:" + ReportFormIds + ", LabId:" + LabID+";" + e.ToString() );
                brdv.success = false;
                brdv.ErrorInfo = e.ToString();
            }
            return brdv;
        }
        /// <summary>
        /// 根据testFormID调labstar服务更新打印次数
        /// </summary>
        /// <param name="testFormID"></param>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateLisTestFormPrintCount", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue UpdateLisTestFormPrintCount(string testFormID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (testFormID.Trim() == "" || testFormID == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "testFormID参数不能为空！";
                return baseResultDataValue;
            }
            JObject resultroot = new JObject();
            try
            {
                ZhiFang.Common.Log.Log.Debug("自助打印调用第三方接口获取查询条件: testFormID=" + testFormID);
                DateTime dt = DateTime.Now;
                string postdatastr = "{\"testFormID\": \"" + testFormID + "\" }";
                
                //string urlwhere = "?testFormID="+ testFormID;
                           ;
                ZhiFang.Common.Log.Log.Debug("自助打印调用第三方接口获取查询条件接口参数:" + postdatastr);
                string url = "http://localhost/ZhiFang.LabStar.TechnicianStation";
                if (string.IsNullOrEmpty(ConfigHelper.GetConfigString("LabStarUrl")))
                {
                    url = ConfigHelper.GetConfigString("LabStarUrl");
                }
                url += "/ServerWCF/LabStarService.svc/LS_UDTO_UpdateLisTestFormPrintCount";
                string resultstr = HttpRequestHelp.Post(postdatastr, "json", url, 10);
                ZhiFang.Common.Log.Log.Debug("UpdateLisTestFormPrintCount调用技师站接口获取查询条件接口结果:" + resultstr);
                resultroot = JsonConvert.DeserializeObject(resultstr) as JObject;
                if (resultstr == "")
                {
                    ZhiFang.Common.Log.Log.Debug("UpdateLisTestFormPrintCount调用技师站接口失败: testFormID=" + testFormID);
                }
                if (resultroot["success"] == null || resultroot["success"].ToString().ToLower() == "false")
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "ZhiFang.ReportFormQueryPrint.ServiceWCF.LabStarService.UpdateLisTestFormPrintCount:" + resultroot["ErrorInfo"].ToString();
                    return baseResultDataValue;
                }
                else
                {
                    baseResultDataValue.success = true;
                    baseResultDataValue.ResultDataValue = "更新成功";
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "ZhiFang.ReportFormQueryPrint.ServiceWCF.LabStarService.UpdateLisTestFormPrintCount:" + ex.ToString();
                return baseResultDataValue;
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 根据报告单id服务更新打印次数
        /// </summary>
        /// <param name="reportFormId"></param>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateReportFormPrintTimes", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue UpdateReportFormPrintTimes(string ReportFormIds)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (ReportFormIds.Trim() == "" || ReportFormIds == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "ReportFormIds参数不能为空！";
                return baseResultDataValue;
            }
            try
            {
                string LabStarIsNeedLabId = ConfigHelper.GetConfigString("LabStarIsNeedLabId");
                string[] ReportFormIdarr = ReportFormIds.Split(',');
                string formWhere = "RFID in ('" + string.Join("','", ReportFormIdarr) + "')";
                long paramLabID = -1;
                if (!string.IsNullOrEmpty(LabStarIsNeedLabId) && LabStarIsNeedLabId == "1")
                {
                    //开启LabID条件，查询报告条件和模板条件添加labid
                    ZhiFang.Common.Log.Log.Debug("LabStarService.UpdateReportFormPrintTimes.开启LabID条件");
                    string LabID = Common.Cookie.CookieHelper.Read("000100");
                    if (!string.IsNullOrEmpty(LabID))
                    {
                        formWhere += " and LabID=" + LabID;
                        paramLabID = long.Parse(LabID);
                    }
                }
                DataSet formList = brf.GetListTopByWhereAndSort(1000, formWhere, "");//查询报告单
                if (formList != null && formList.Tables != null && formList.Tables.Count > 0 && formList.Tables[0].Rows.Count > 0)
                {
                    //更新打印次数
                    string[] reportformlist = new string[formList.Tables[0].Rows.Count] ;
                    for (int i = 0; i < formList.Tables[0].Rows.Count; i++)
                    {
                        reportformlist[i]=(formList.Tables[0].Rows[i]["ReportFormID"].ToString());
                    }
                    
                    string uluserCName = Common.Cookie.CookieHelper.Read("000301");
                    bool flag = brf.UpdatePrintTimes(reportformlist, uluserCName);
                    if (flag)
                    {
                        baseResultDataValue.success = true;
                    }
                    else
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "LabStarService.UpdateReportFormPrintTimes更新打印次数失败：" + ReportFormIds ;
                    }
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "未查询到报告，ReportFormIds：" + ReportFormIds ;
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                ZhiFang.Common.Log.Log.Debug("ZhiFang.ReportFormQueryPrint.ServiceWCF.LabStarService.UpdateLisTestFormPrintCount:" + ex.ToString());
                baseResultDataValue.ErrorInfo = "ZhiFang.ReportFormQueryPrint.ServiceWCF.LabStarService.UpdateLisTestFormPrintCount:" + ex.ToString();
                return baseResultDataValue;
            }
            return baseResultDataValue;
        }
    }
}
