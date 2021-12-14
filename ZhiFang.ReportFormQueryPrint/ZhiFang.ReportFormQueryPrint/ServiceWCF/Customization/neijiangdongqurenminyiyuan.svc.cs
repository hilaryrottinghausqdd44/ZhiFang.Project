using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using System.Xml;
using ZhiFang.ReportFormQueryPrint.Common;
using ZhiFang.ReportFormQueryPrint.IDAL;

namespace ZhiFang.ReportFormQueryPrint.ServiceWCF.Customization
{
    [ServiceContract(Namespace = "ZhiFang.ReportFormQueryPrint")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class neijiangdongqurenminyiyuan
    {
        private readonly ZhiFang.ReportFormQueryPrint.BLL.BReportForm brf = new BLL.BReportForm();
        private readonly ZhiFang.ReportFormQueryPrint.BLL.BALLReportForm barf = new BLL.BALLReportForm();
        private readonly ZhiFang.ReportFormQueryPrint.BLL.BReportItem bri = new BLL.BReportItem();
        private readonly ZhiFang.ReportFormQueryPrint.BLL.Print.BShowFrom bsf = new BLL.Print.BShowFrom();
        private readonly ZhiFang.ReportFormQueryPrint.BLL.Print.PrintReportForm bprf = new BLL.Print.PrintReportForm();
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetReportFoemListByReportId", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public neijiangRportFormSuccessVO GetReportFoemListByReportId(string startAt, string endAt, string patientid)
        {
            ZhiFang.Common.Log.Log.Debug("内江东兴区人民医院.根据病人唯一标识号获取病人信息.GetReportFoemListByReportId开始");
            neijiangRportFormSuccessVO successVO = new neijiangRportFormSuccessVO();
            if (startAt == "" || startAt == null)
            {
                ZhiFang.Common.Log.Log.Debug("参数startAt为空!");
                successVO.status = "-1";
                successVO.msg = "参数startAt为空!";
                successVO.data = null;
                return successVO;
            }
            if (endAt == "" || endAt == null)
            {
                ZhiFang.Common.Log.Log.Debug("参数endAt为空!");
                successVO.status = "-1";
                successVO.msg = "参数sndAt为空!";
                successVO.data = null;
                return successVO;
            }
            if (patientid == "" || patientid == null)
            {
                ZhiFang.Common.Log.Log.Debug("参数patientid为空!");
                successVO.status = "-1";
                successVO.msg = "参数patientid为空!";
                successVO.data = null;
                return successVO;
            }
            try
            {
                ZhiFang.Common.Log.Log.Debug("GetReportFoemListByReportId.StartAt:" + startAt + " EndAt:" + endAt + " PatientId:" + patientid);
                string urlWhere = " ";   
                urlWhere += "zdy6 = '" + patientid + "' and (ReceiveDate >='" + startAt + "' and ReceiveDate <='" + endAt + "')";
                DataSet ds = barf.GetList_FormFull("ReportFormID,Receivedate,SectionNo,TestTypeNo,SampleNo,zdy2,CheckDate,CheckTime", urlWhere);

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        reportdata reportdata = new reportdata();
                        reportdata.reportId = ds.Tables[0].Rows[i]["ReportFormID"].ToString();
                        reportdata.reportDateTime = ds.Tables[0].Rows[i]["CheckDate"].ToString().Split(' ')[0]+" " + ds.Tables[0].Rows[i]["CheckTime"].ToString().Split(' ')[1];
                        reportdata.reportType = "检验";
                        reportdata.applyid = ds.Tables[0].Rows[i]["zdy2"].ToString();

                        successVO.status = "200";
                        successVO.msg = "OK";
                        if (successVO.data == null) {
                            successVO.data = new List<reportdata>();
                        }
                        successVO.data.Add(reportdata);
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug("GetReportFoemListByReportId.没找到报告,请检查日期及查询条件。");
                    successVO.status = "-1";
                    successVO.msg = "没找到报告,请检查日期及查询条件。";
                    successVO.data = null;
                    return successVO;
                }
                
                ZhiFang.Common.Log.Log.Debug("内江东兴区人民医院.根据病人唯一标识号获取病人信息.GetReportFoemListByReportId结束");
                return successVO;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("GetReportFoemListByReportId异常:" + e.ToString());
                successVO.status = "-1";
                successVO.msg = e.ToString();
                successVO.data = null;
                return successVO;
            }
        }

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetReportFoemPDFByReportId_Base64String", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public neijiangRportFormPdfSuccessVO GetReportFoemPDFByReportId_Base64String(string reportId)
        {
            ZhiFang.Common.Log.Log.Debug("内江东兴区人民医院.GetReportFoemPDFByReportId_Base64String生成PDFBase64字符串.开始");
            neijiangRportFormPdfSuccessVO successVO = new neijiangRportFormPdfSuccessVO();
            if (reportId == "" || reportId == null)
            {
                ZhiFang.Common.Log.Log.Debug("参数ReportId为空!");
                successVO.status = "-1";
                successVO.msg = "参数ReportId为空!";
                successVO.data = null;
                return successVO;
            }
           
            ZhiFang.Common.Log.Log.Debug("内江东兴区人民医院.GetReportFoemPDFByReportId_Base64String.reportId:" + reportId );
            List<string> fields = new List<string>();
            List<string> values = new List<string>();
            List<string> order = new List<string>();
            int flag = 0;
            try
            {
                //要查询的字段
                fields.Add("ReceiveDate");
                fields.Add("SectionNo");
                fields.Add("TestTypeNo");
                fields.Add("SampleNo");
                //要查询字段的值
                string[] aa = reportId.Split(';');
                values.Add(aa[0]);
                values.Add(aa[1]);
                values.Add(aa[2]);
                values.Add(aa[3]);
                string urlWhere = " ";
                for (int i = 0; i < fields.Count; i++)
                {
                    urlWhere += fields[i] + "='" + values[i] + "' and ";
                }
                urlWhere = urlWhere.Substring(0, urlWhere.Length - 4);
                
                DataSet ds = barf.GetList_FormFull("ReportFormID,Receivedate,SectionNo,TestTypeNo,SampleNo,zdy6", urlWhere);
                ZhiFang.ReportFormQueryPrint.BLL.Print.PrintReportForm bprf = new BLL.Print.PrintReportForm();
                List<string> rfids = new List<string>();
                var itemDirectory = System.AppDomain.CurrentDomain.BaseDirectory;//获取项目物理路径
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        rfids.Add(ds.Tables[0].Rows[i]["ReportFormID"].ToString());
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug("GetReportFoemPDFByReportId_Base64String.没找到报告,请检查报告日期及查询条件。");
                    successVO.status = "-1";
                    successVO.msg = "没找到报告,请检查报告日期及查询条件。";
                    successVO.data = null;
                    return successVO;
                }
                ZhiFang.Common.Log.Log.Debug("GetReportFormPDFBitsByDS.rfids:" + string.Join(",", rfids));
                var listreportformfile = bprf.CreatReportFormFiles(rfids, ReportFormTitle.center, ReportFormFileType.PDF, "1", flag);
                if (listreportformfile.Count > 0)
                {
                    
                    for (int i = 0; i < listreportformfile.Count; i++)
                    {
                       
                        byte[] byteArray = ByteHelper.File2Bytes(itemDirectory + listreportformfile[i].PDFPath.Replace(@"\", "/"));//文件转成byte二进制数组
                        string JarContent = Convert.ToBase64String(byteArray);//将二进制转成string类型

                        reportformpdf reportdata = new reportformpdf();
                        reportdata.fileType = "pdf";
                        reportdata.reportFile = JarContent;

                        successVO.status = "200";
                        successVO.msg = "OK";
                        successVO.data = reportdata;
                     

                        if (File.Exists(itemDirectory + listreportformfile[i].PDFPath.Replace(@"\", "/")))
                        {
                            File.Delete(itemDirectory + listreportformfile[i].PDFPath.Replace(@"\", "/"));
                        }
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug("GetReportFoemPDFByReportId_Base64String.未查找到报告单");
                    successVO.status = "-1";
                    successVO.msg = "未查找到报告单";
                    successVO.data = null;
                    return successVO;
                }
                ZhiFang.Common.Log.Log.Debug("内江东兴区人民医院.GetReportFoemPDFByReportId_Base64String生成PDFBase64字符串.结束");
                return successVO;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("内江东兴区人民医院.GetReportFoemPDFByReportId_Base64String异常:" + e.ToString());
                successVO.status = "-1";
                successVO.msg = e.ToString();
                successVO.data = null;
                return successVO;
            }
            
        }

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateReportPrintTimesByReportID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public bool UpdateReportPrintTimesByReportID(string userId, string cardType, string cardNo, string reportId) {


            ZhiFang.Common.Log.Log.Debug("内江东兴区人民医院.UpdateReportPrintTimesByReportID增加打印次数.开始");
            if (userId == "" || userId == null)
            {
                ZhiFang.Common.Log.Log.Debug("参数userId为空!");
                return false;
            }
            if (cardType == "" || cardType == null)
            {
                ZhiFang.Common.Log.Log.Debug("参数cardType为空!");
                return false;
            }
            if (cardNo == "" || cardNo == null)
            {
                ZhiFang.Common.Log.Log.Debug("参数cardNo为空!");
                return false;
            }
            if (reportId == "" || reportId == null)
            {
                ZhiFang.Common.Log.Log.Debug("参数reportId为空!");
                return false;
            }
            try
            {
                string ip = HttpContext.Current.Request.UserHostAddress;
                bool success = false;
                string[] reportformlist = reportId.Split(',');
                bool Ms66Flag = true;
                bool flag = true;
                #region 向lis加入打印次数
                if (ConfigHelper.GetConfigString("IsLisAddPrintTime").Equals("1")) //向Lis添加打印次数
                {
                    DataTable dsreportform = new DataTable();
                    dsreportform = brf.GetReportFormList(reportformlist);
                    if (dsreportform == null || dsreportform.Rows.Count <= 0)
                    {
                        success = false;
                        ZhiFang.Common.Log.Log.Debug("UpdateReportPrintTimesByReportID.LIS中没有找到报告单!");
                    }
                    List<string> reportformidlist66 = new List<string>();
                    for (int i = 0; i < dsreportform.Rows.Count; i++)
                    {
                        reportformidlist66.Add(dsreportform.Rows[i]["Receivedate"].ToString().Trim() + ";" + dsreportform.Rows[i]["SectionNo"].ToString().Trim() + ";" + dsreportform.Rows[i]["TestTypeNo"].ToString().Trim() + ";" + dsreportform.Rows[i]["SampleNo"].ToString().Trim());
                    }
                    IDReportForm dal = Factory.DalFactory<IDReportForm>.GetDal("ReportForm", "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66", ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("LISConnectionString"));//LIS库打标记
                    Ms66Flag = dal.UpdatePrintTimes(reportformidlist66.ToArray(),"");
                    ZhiFang.Common.Log.Log.Debug("UpdateReportPrintTimesByReportID.IP:" + ip + "向Lis添加打印次数:" + Ms66Flag);
                    Ms66Flag = dal.UpdateClientPrintTimes(reportformidlist66.ToArray());//外围打印标记
                    ZhiFang.Common.Log.Log.Debug("UpdateReportPrintTimesByReportID.IP:" + ip + "向Lis添加外围打印次数:" + Ms66Flag);
                }
                #endregion

                flag = brf.UpdatePrintTimes(reportformlist,"");
                ZhiFang.Common.Log.Log.Debug("UpdateReportPrintTimesByReportID.IP:" + ip + "添加打印次数:reportId:" + reportId);
                flag = brf.UpdateClientPrintTimes(reportformlist);
                ZhiFang.Common.Log.Log.Debug("UpdateReportPrintTimesByReportID.IP:" + ip + "添加外围打印次数:reportId:" + reportId);
                if (Ms66Flag && flag)
                {
                    success = true;
                }
                else
                {
                    success = false;
                }
                
                ZhiFang.Common.Log.Log.Debug("内江东兴区人民医院.UpdateReportPrintTimesByReportID增加打印次数.结束");
                return success;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("UpdateReportPrintTimesByReportID异常:" + e.ToString());
                return false;
            }
        }

        /// <summary>
        /// 调用接口
        /// </summary>
        /// <param name="PostData"></param>
        /// <param name="DataType"></param>
        /// <param name="url"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        //public  string Post(string PostData, string DataType, string url, int timeout)
        //{
        //    System.GC.Collect();//垃圾回收，回收没有正常关闭的http连接

        //    string result = "";//返回结果

        //    HttpWebRequest request = null;
        //    HttpWebResponse response = null;
        //    Stream reqStream = null;

        //    try
        //    {
        //        //设置最大连接数
        //        ServicePointManager.DefaultConnectionLimit = 5000;

        //        /***************************************************************
        //        * 下面设置HttpWebRequest的相关属性
        //        * ************************************************************/
        //        request = (HttpWebRequest)WebRequest.Create(url);

        //        request.Method = "POST";
        //        request.Timeout = timeout * 5000;

        //        //设置POST的数据类型和长度
        //        if (DataType.Trim().ToUpper() == "XML")
        //        {
        //            request.ContentType = "text/xml";
        //        }
        //        if (DataType.Trim().ToUpper() == "JSON")
        //        {
        //            request.ContentType = "application/json";
        //        }
        //        byte[] data = System.Text.Encoding.UTF8.GetBytes(PostData);
        //        request.ContentLength = data.Length;

        //        //往服务器写入数据
        //        reqStream = request.GetRequestStream();
        //        reqStream.Write(data, 0, data.Length);
        //        reqStream.Close();

        //        //获取服务端返回
        //        response = (HttpWebResponse)request.GetResponse();

        //        //获取服务端返回数据
        //        StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
        //        result = sr.ReadToEnd().Trim();
        //        sr.Close();
        //    }
        //    catch (System.Threading.ThreadAbortException e)
        //    {
        //        ZhiFang.Common.Log.Log.Error("HttpService。Thread - caught ThreadAbortException - resetting.");
        //        ZhiFang.Common.Log.Log.Error("Exception message：" + e.Message);
        //        System.Threading.Thread.ResetAbort();
        //    }
        //    catch (WebException e)
        //    {
        //        ZhiFang.Common.Log.Log.Error("HttpService异常：" + e.ToString());
        //        if (e.Status == WebExceptionStatus.ProtocolError)
        //        {
        //            ZhiFang.Common.Log.Log.Error("HttpService.StatusCode : " + ((HttpWebResponse)e.Response).StatusCode);
        //            ZhiFang.Common.Log.Log.Error("HttpService.StatusDescription : " + ((HttpWebResponse)e.Response).StatusDescription);
        //        }
        //        throw new Exception(e.ToString());
        //    }
        //    catch (Exception e)
        //    {
        //        ZhiFang.Common.Log.Log.Error("HttpService异常:" + e.ToString());
        //        throw new Exception(e.ToString());
        //    }
        //    finally
        //    {
        //        //关闭连接和流
        //        if (response != null)
        //        {
        //            response.Close();
        //        }
        //        if (request != null)
        //        {
        //            request.Abort();
        //        }
        //    }
        //    return result;
        //}


    }

    public class neijiangRportFormSuccessVO
    {
        
        public string status { get; set; }
        public string msg { get; set; }
        public List<reportdata> data { get; set; }
    }

    public class reportdata {
        public string reportId { get; set; }
        public string reportDateTime { get; set; }
        public string reportType { get; set; }
        public string applyid { get; set; }
    }

    public class neijiangRportFormPdfSuccessVO
    {

        public string status { get; set; }
        public string msg { get; set; }
        public reportformpdf data { get; set; }
    }

    public class reportformpdf
    {
        public string fileType { get; set; }
        public string reportFile { get; set; }
      
    }

}
