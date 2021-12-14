using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;
using ZhiFang.ReportFormQueryPrint.Common;
using ZhiFang.ReportFormQueryPrint.Model;
using ZhiFang.ReportFormQueryPrint.Model.VO;
using ZhiFang.ReportFormQueryPrint.ServerContract;

namespace ZhiFang.ReportFormQueryPrint.ServiceWCF
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ReportFormHistoryService : IReportFormHistoryService
    {

        private readonly ZhiFang.ReportFormQueryPrint.BLL.BHistoryReportForm barf = new BLL.BHistoryReportForm();
        private readonly ZhiFang.ReportFormQueryPrint.BLL.BNRequestItem bnri = new BLL.BNRequestItem();
        private readonly ZhiFang.ReportFormQueryPrint.BLL.Print.PrintHistoryReportForm bprf = new BLL.Print.PrintHistoryReportForm();
        private readonly ZhiFang.ReportFormQueryPrint.BLL.Print.BHistoryShowFrom bsf = new BLL.Print.BHistoryShowFrom();

        public BaseResultDataValue SelectReport(string Where, string fields, int page, int limit, string SerialNo)
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
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                brdv.ErrorInfo = "SelectReport:" + ex.ToString();
                brdv.success = false;
                return brdv;
            }
        }
        public BaseResultDataValue SelectReportSort(string Where, string fields, int page, int limit, string SerialNo, string sort)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try {
                if (!String.IsNullOrWhiteSpace(sort))
                {
                    //sort--[{"property":"CNAME","direction":"DESC"}],去掉不相关符号
                    string result = sort.Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "").Replace("\"", "");
                    string finalColumnSort = "";
                    //finalColumnSort格式-->"CNAME DESC"
                    string[] string1 = result.Split(',');
                    for (int i=0;i<string1.Length;i++)
                    {
                        string[] string2=string1[i].Split(':');
                        finalColumnSort += string2[1]+" ";
                    }                  
                    string sortWhere = Where;
                    //Where有setting页面设置字段排序和无设置排序分别处理
                    if (sortWhere.IndexOf("BY") >= 0)
                    {
                        string sortWhere1 = sortWhere.Substring(0, sortWhere.IndexOf("BY")+3);
                        string sortWhere2 = sortWhere.Substring(sortWhere.IndexOf("BY")+3);
                        //最终条件=初始条件截止到order by+点击列名的排序+setting页面设置的字段排序
                        Where = sortWhere1 + finalColumnSort + "," + sortWhere2;
                    }
                    else
                    {
                        Where +=" ORDER BY " + finalColumnSort;
                    }
                }
                brdv = SelectReport(Where, fields, page, limit, SerialNo);
                return brdv;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("ZhiFang.ReportFormQueryPrint.ServiceWCF.ReportFormHistoryService,方法：SelectReportSort");
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                brdv.ErrorInfo = "查询条件错误，请查看查询条件格式是否正确";
                brdv.success = false;
                return brdv;
            }
            
        }
        public BaseResultDataValue PreviewReport(string ReportFormID, string SectionNo, string SectionType, string ModelType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            try
            {
                brdv = this.PreviewReportExtPageName(ReportFormID, SectionNo, SectionType, ModelType, null);
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

        public BaseResultDataValue PreviewReportExtPageName(string ReportFormID, string SectionNo, string SectionType, string ModelType, string PageName)
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
                    tmp = bsf.ShowReportFormResult(urlReportFormID, sn, urlPageName, st, urlSectionType);
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

        public BaseResultDataValue GetReportFormPDFByReportFormID(string ReportFormID, string SectionNo, string SectionType, int flag)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = false;
            try
            {
                ZhiFang.Common.Log.Log.Debug("GetReportFormPDFByReportFormID.ReportFormID:" + ReportFormID + ", SectionNo:" + SectionNo + ", SectionType:" + SectionType + ", flag:" + flag + "");
                var listreportformfile = bprf.CreatReportFormFiles(new List<string>() { ReportFormID }, ReportFormTitle.center, ReportFormFileType.PDF, SectionType, flag);
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
                //brdv.ErrorInfo = "程序异常请查看系统日志！";
                brdv.ErrorInfo = e.ToString();
            }
            return brdv;
        }

        public BaseResult ReportFormAddUpdatePrintTimes(string reportformidstr)
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
                bool flag = true;                

                flag = barf.UpdatePrintTimes(reportformlist);
                ZhiFang.Common.Log.Log.Debug("IP:" + ip + "添加打印次数:reportformID:" + reportformidstr);
                if ( flag)
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
    }
}
