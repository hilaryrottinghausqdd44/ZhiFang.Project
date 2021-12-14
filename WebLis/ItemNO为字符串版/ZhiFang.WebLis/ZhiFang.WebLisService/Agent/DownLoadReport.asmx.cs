using System.ComponentModel;
using System.Web.Services;
using System.Web.Services.Protocols;
using System;
using ECDS.Common;
using WSInvoke;

namespace ZhiFang.WebLisService.Agent
{
    /// <summary>
    /// DownLoadReport 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class DownLoadReport : System.Web.Services.WebService
    {

        public string wsfAddr = LIS.CacheConfig.Util.Readcfg.ReadINIConfig("DownPDFAddr").ToString();

        /// <summary>
        /// 根据检验报告单编号获取检验报告单ReportForm列表
        /// </summary>
        /// <param name="token">Token是用户的令牌，是一个字符串，由用户的身份信息和调用时间等通过加密、解密后组成</param>
        /// <param name="SerialNo">检验单号</param>
        /// <param name="errorMsg">错误信息</param>
        /// <param name="returnValue">返回的结构列表字符串</param>
        /// <returns>
        /// 0 成功
        /// 负数:失败
        ///     -1:系统出现异常
        ///     -100:非法调用(如用户身份不合法等)
        /// </returns>
        [WebMethod]
        public int DownloadReportFormListBySerialNo(string token, string SerialNo, out string errorMsg, out string returnValue)
        {
            Log.Info(String.Format("下载报告列表开始token={0},SerialNo={1}", token, SerialNo));
            errorMsg = "";
            returnValue = "";

            object[] args = new object[4];
            args[0] = token;
            args[1] = SerialNo;
            args[2] = errorMsg;
            args[3] = returnValue;


            Log.Info(String.Format("连接远程服务{0}", wsfAddr));
            int rs = (int)WebServiceHelper.InvokeWebService(wsfAddr, "DownloadReportFormListBySerialNo", args);

            return rs;



        }


        /// <summary>
        /// 根据ReportFormID获取检验报告单ReportForm和ReportItem列表
        /// </summary>
        /// <param name="token">Token是用户的令牌，是一个字符串，由用户的身份信息和调用时间等通过加密、解密后组成</param>
        /// <param name="ReportFormID"></param>
        /// <param name="errorMsg">错误信息</param>
        /// <param name="returnValue">返回的结构列表字符串</param>
        /// <returns>
        /// 0 成功
        /// 负数:失败
        ///     -1:系统出现异常
        ///     -100:非法调用(如用户身份不合法等)
        /// </returns>
        [WebMethod]
        public int DownloadReportFormItemListByReportFormID(string token, string ReportFormID, out string errorMsg, out string returnValue)
        {
            errorMsg = "";
            returnValue = "";

            object[] args = new object[4];
            args[0] = token;
            args[1] = ReportFormID;
            args[2] = errorMsg;
            args[3] = returnValue;


            Log.Info(String.Format("连接远程服务{0}", wsfAddr));
            int rs = (int)WebServiceHelper.InvokeWebService(wsfAddr, "DownloadReportFormItemListByReportFormID", args);

            return rs;
        }



        /// <summary>
        /// 下载某个文件(比如PDF文件),返回流
        /// </summary>
        /// <param name="token">Token是用户的令牌，是一个字符串，由用户的身份信息和调用时间等通过加密、解密后组成</param>
        /// <param name="fileName">对应下载XML数据格式的PDFFILE节点的内容</param>
        /// <param name="errorMsg">错误信息</param>
        /// <param name="returnValue">返回的结构列表byte[]</param>
        /// <returns>
        /// 0 成功
        /// 负数:失败
        ///     -1:系统出现异常
        ///     -100:非法调用(如用户身份不合法等)
        /// </returns>
        [WebMethod]
        public int downLoadPDFFileContent(string token, string fileName, out string errorMsg, out byte[] returnValue)
        {
            errorMsg = "";
            returnValue = null;

            object[] args = new object[4];
            args[0] = token;
            args[1] = fileName;
            args[2] = errorMsg;
            args[3] = returnValue;


            Log.Info(String.Format("连接远程服务{0}", wsfAddr));
            int rs = (int)WebServiceHelper.InvokeWebService(wsfAddr, "downLoadPDFFileContent", args);

            return 0;
        }
    }
}
