using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace ZhiFang.ReportFormQueryPrint.ServiceWCF.Customization
{
    [ServiceContract(Namespace = "ZhiFang.ReportFormQueryPrint")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class beijingwuzhoufubao
    {
        private readonly ZhiFang.ReportFormQueryPrint.BLL.BReportForm brf = new BLL.BReportForm();
        private readonly ZhiFang.ReportFormQueryPrint.BLL.BALLReportForm barf = new BLL.BALLReportForm();
        private readonly ZhiFang.ReportFormQueryPrint.BLL.BReportItem bri = new BLL.BReportItem();
        private readonly ZhiFang.ReportFormQueryPrint.BLL.Print.BShowFrom bsf = new BLL.Print.BShowFrom();
        private readonly ZhiFang.ReportFormQueryPrint.BLL.Print.PrintReportForm bprf = new BLL.Print.PrintReportForm();
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SelectReport", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public VO SelectReport(string vaa01, string start, string end, string key)
        {
            VO tmpvo = new VO();
            string sqlstr = " 1=1 ";
            #region 验证

            string MD5Key = Common.ConfigHelper.GetConfigString("MD5Key_wuzhoufubao");
            
            if (key == null || key.Trim() != MD5Key)
            {
                tmpvo.Code = MsgCode.秘钥不正确;
                tmpvo.Msg = "秘钥不正确";
                ZhiFang.Common.Log.Log.Debug("SelectReport,秘钥不正确,参数key：" + key+ "@MD5Key:"+ MD5Key);
                return tmpvo;
            }
            else
            {
                ZhiFang.Common.Log.Log.Debug("SelectReport,参数key：" + key);
            }

            if (vaa01 != null && vaa01.Trim() != "")
            {
                ZhiFang.Common.Log.Log.Debug("SelectReport,参数vaa01：" + vaa01);
                sqlstr += " and PatNo='" + vaa01 + "' ";
            }
            else
            {
                tmpvo.Code = MsgCode.系统异常;
                tmpvo.Msg = "系统异常"+ "参数vaa01：为空！";
                ZhiFang.Common.Log.Log.Debug("SelectReport,参数vaa01：为空！");
                return tmpvo;
            }
            if (start != null && start.Trim() != "")
            {
                ZhiFang.Common.Log.Log.Debug("SelectReport,参数start：" + start);
                sqlstr += " and Receivedate >='" + start + "' ";
            }
            else
            {
                //tmovo.Code = MsgCode.系统异常;
                //tmovo.Msg = "系统异常" + "参数start：为空！"; ;
                ZhiFang.Common.Log.Log.Debug("SelectReport,参数start：为空！");
            }
            if (end != null && end.Trim() != "")
            {
                ZhiFang.Common.Log.Log.Debug("SelectReport,参数end：" + end);
                sqlstr += " and Receivedate <='" + end + "' ";
            }
            else
            {
                //tmovo.Code = MsgCode.系统异常;
                //tmovo.Msg = "系统异常" + "参数end：为空！"; 
                ZhiFang.Common.Log.Log.Debug("SelectReport,参数end：为空！");
            }
            
            #endregion

            try
            {
                //报告发布程序的状态字段
                sqlstr += " and reportsend='1' ORDER by Receivedate DESC  ";
                string urlModel = " ReceiveDate,SectionNo,TestTypeNo,SampleNo,SectionName,ReportFormID,PatNo ";
                DataSet ds = new DataSet();
                string countwhere = sqlstr.ToUpper();
                if (countwhere.IndexOf("ORDER") >= 0)
                {
                    countwhere = countwhere.Substring(0, countwhere.IndexOf("ORDER"));
                }
                int dsCount = barf.GetCountFormFull(countwhere);

                //ZhiFang.Common.Log.Log.Debug("urlType_dsCount" + dsCount);
                int _reportformlimit = 5000;
                if (Common.ConfigHelper.GetConfigInt("SearchReportFormLimit") != null)
                {
                    _reportformlimit = Common.ConfigHelper.GetConfigInt("SearchReportFormLimit").Value;
                }
                if (dsCount > _reportformlimit)
                {
                    tmpvo.Code = MsgCode.系统异常;
                    tmpvo.Msg = "系统异常：" + dsCount + "数据超过" + _reportformlimit + "条,请从新选择查询条件!";
                    return tmpvo;
                }
                ds = barf.GetList_FormFull(urlModel, sqlstr);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    tmpvo.count = ds.Tables[0].Rows.Count.ToString();
                    //ZhiFang.Common.Log.Log.Debug("1");
                    VOData[] vodata = new VOData[ds.Tables[0].Rows.Count];
                    //ZhiFang.Common.Log.Log.Debug("2");
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        vodata[i] = new VOData();
                        vodata[i].vaa01 = ds.Tables[0].Rows[i]["PatNo"].ToString();
                        //ZhiFang.Common.Log.Log.Debug("3");
                        vodata[i].ReportSn = ds.Tables[0].Rows[i]["ReportFormID"].ToString();
                        //ZhiFang.Common.Log.Log.Debug("4");

                        DataTable dtitem = bri.GetReportItemList_DataTable(ds.Tables[0].Rows[i]["ReportFormID"].ToString());
                        List<string> tmplist = new List<string>();
                        if (dtitem != null && dtitem.Rows.Count > 0)
                        {
                            for (int itemi = 0; itemi < dtitem.Rows.Count; itemi++)
                            {
                                if (!tmplist.Contains(dtitem.Rows[itemi]["ParItemName"].ToString()))
                                {
                                    tmplist.Add(dtitem.Rows[itemi]["ParItemName"].ToString());
                                }
                            }
                            if (tmplist.Count > 0)
                            {
                                vodata[i].ReportType = string.Join(";", tmplist.ToArray());
                            }
                            else
                            {
                                vodata[i].ReportType = "";
                            }
                        }
                        else
                        {
                            vodata[i].ReportType = "";
                        }
                        //ZhiFang.Common.Log.Log.Debug("5");
                        if (ds.Tables[0].Rows[i]["ReceiveDate"] != null && ds.Tables[0].Rows[i]["ReceiveDate"].ToString().Trim() != "")
                        {
                            vodata[i].ReportTime = Convert.ToDateTime(ds.Tables[0].Rows[i]["ReceiveDate"].ToString().Trim()).ToString("yyyy-MM-dd");
                        }
                        //ZhiFang.Common.Log.Log.Debug("6");
                        //string reportpath = System.AppDomain.CurrentDomain.BaseDirectory + Common.ConfigHelper.GetConfigString("ReportFormURL") + "\\" + vodata[i].ReportTime + "\\" + vodata[i].ReportSn + ".pdf";
                        string reportpath = "http://"+System.Web.HttpContext.Current.Request.Url.Host+ System.Web.HttpContext.Current.Request.ApplicationPath+"/"+ Common.ConfigHelper.GetConfigString("ReportFormURL") + "/" + vodata[i].ReportTime + "/" + vodata[i].ReportSn + ".pdf";
                        //ZhiFang.Common.Log.Log.Debug("7");
                        vodata[i].ReportUrl = reportpath;
                        //ZhiFang.Common.Log.Log.Debug("8");
                    }
                    tmpvo.DataArray = vodata;
                    //ZhiFang.Common.Log.Log.Debug("9");
                    tmpvo.Code = MsgCode.查询成功;
                    tmpvo.Msg = "查询成功";
                }
                else
                {
                    tmpvo.count = "0";
                    tmpvo.Code = MsgCode.没有记录;
                    tmpvo.Msg = "没有记录";
                }
                return tmpvo;
            }
            catch (Exception e)
            {
                tmpvo.Code = MsgCode.系统异常;
                tmpvo.Msg = "系统异常";
                ZhiFang.Common.Log.Log.Debug("SelectReport,系统异常："+ e.ToString());
                return tmpvo;
            }
        }

        // 在此处添加更多操作并使用 [OperationContract] 标记它们
    }
    public class VO
    {
        public string count{ get; set; }
        public VOData[] DataArray { get; set; }
        public string Code { get; set; }
        public string Msg { get; set; }
    }
    public class VOData
    {
        public string vaa01 { get; set; }
        public string ReportSn { get; set; }
        public string ReportType { get; set; }
        public string ReportTime { get; set; }
        public string ReportUrl { get; set; }
    }
    public  class MsgCode
    {
        public static string 查询成功 = "200";
        public static string 没有记录 = "400";
        public static string 秘钥不正确 = "401";
        public static string 系统异常 = "404";
    }
}
