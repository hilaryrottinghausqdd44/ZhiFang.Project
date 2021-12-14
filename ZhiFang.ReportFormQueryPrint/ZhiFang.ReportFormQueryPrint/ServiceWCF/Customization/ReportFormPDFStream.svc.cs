using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using ZhiFang.ReportFormQueryPrint.BLL;
using ZhiFang.ReportFormQueryPrint.Common;

namespace ZhiFang.ReportFormQueryPrint.ServiceWCF.Customization
{
    /// <summary>
    /// pdf转文件流服务
    /// </summary>
    [ServiceContract(Namespace = "ZhiFang.ReportFormQueryPrint.ServiceWCF.Customization")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ReportFormPDFStream
    {
        
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/getPdfStreamBySerialNo/SerialNo={SerialNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public Stream getPdfStreamBySerialNo(string SerialNo)
        {
            //通过url访问wcf服务，通过SerialNo获取pdf返回文件流
            FileStream fileStream = null;
            try
            {   
                string path = "";
                if (string.IsNullOrEmpty(SerialNo))
                {
                    ZhiFang.Common.Log.Log.Debug("ZhiFang.ReportFormQueryPrint.ServiceWCF.Customization.getPdfStreamByPath:请传入正确的条码号");
                }
                else
                {
                    string[] sArray = SerialNo.Split('.');
                    ZhiFang.Common.Log.Log.Debug("ZhiFang.ReportFormQueryPrint.ServiceWCF.Customization.getPdfStreamByPath:SerialNo:" + SerialNo);

                    ZhiFang.ReportFormQueryPrint.BLL.Print.PrintReportForm bprf = new BLL.Print.PrintReportForm();
                    
                    ZhiFang.ReportFormQueryPrint.BLL.BALLReportForm barf = new BLL.BALLReportForm();
                    DataSet ds = barf.GetList_FormFull("", "SerialNo='" + sArray[0] + "'");
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string ReportFormID = ds.Tables[0].Rows[0]["ReportFormID"].ToString();
                        string SectionNo = ds.Tables[0].Rows[0]["SectionNo"].ToString();
                        string SectionType = ds.Tables[0].Rows[0]["SectionType"].ToString();

                        var listreportformfile = bprf.CreatReportFormFiles(new List<string>() { ReportFormID }, ReportFormTitle.center, ReportFormFileType.PDF, SectionType, 0);
                        path = System.AppDomain.CurrentDomain.BaseDirectory + listreportformfile[0].PDFPath;
                        ZhiFang.Common.Log.Log.Debug("ZhiFang.ReportFormQueryPrint.ServiceWCF.Customization.getPdfStreamByPath:path:" + path);
                        fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                        WebOperationContext.Current.OutgoingResponse.ContentType = "application/pdf";
                        WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=\"" + 111 + "\"");
                    }

                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
            }
            return fileStream;
        }
    }
}
