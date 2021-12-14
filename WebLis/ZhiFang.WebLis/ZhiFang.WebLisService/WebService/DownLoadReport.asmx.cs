using System.ComponentModel;
using System.Web.Services;
using System.Web.Services.Protocols;
using System;
using ECDS.Common;

namespace ZhiFang.WebLisService.WebService
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

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
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
            bool validate = WL.Common.Tools.checkCallWebServiceUserValidate(token, out errorMsg);
            if (validate == false)
            {
                Log.Error(errorMsg);
                return -100;
            }

            errorMsg = "";
            returnValue = "";
            try
            {
                if (SerialNo == "")
                {
                    errorMsg = "参数SerialNo应该有内容!";
                    returnValue = "";
                    return -2;
                }
                string whereSQL = "SerialNo='" + SerialNo + "'";
                returnValue = WL.Common.ReportData.DownloadReportFormList(whereSQL);
            }
            catch (System.Exception ex)
            {
                errorMsg = "根据检验报告单编号获取检验报告单ReportForm列表出错:" + ex.Message;
                returnValue = "";
                return -1;
            }
            return 0;



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
            bool validate = WL.Common.Tools.checkCallWebServiceUserValidate(token, out errorMsg);
            if (validate == false)
            {
                Log.Error(errorMsg);
                return -100;
            }

            errorMsg = "";
            returnValue = "";
            try
            {
                if (ReportFormID == "")
                {
                    errorMsg = "根据ReportFormID获取检验报告单ReportForm和ReportItem列表出错:参数ReportFormID应该有内容!";
                    returnValue = "";
                    return -2;
                }
                string whereSQL = "ReportFormID='" + ReportFormID + "'";
                returnValue = WL.Common.ReportData.DownloadReportFormItemList(whereSQL, out errorMsg);
            }
            catch (System.Exception ex)
            {
                errorMsg = "根据ReportFormID获取检验报告单ReportForm和ReportItem列表:" + ex.Message;
                returnValue = "";
                return -1;
            }
            return 0;
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
            bool validate = WL.Common.Tools.checkCallWebServiceUserValidate(token, out errorMsg);
            if (validate == false)
            {
                Log.Error(errorMsg);
                return -100;
            }

            errorMsg = "";
            returnValue = null;
            try
            {
                returnValue = WL.Common.ReportData.downLoadPDFFileContent(fileName);
            }
            catch (System.Exception ex)
            {
                errorMsg = "获取文件" + fileName + "的内容流出错:" + ex.Message;
                return -1;
            }
            return 0;
        }
        /// <summary>
        /// 根据医嘱编号、申请单号返回PDF报告单文件
        /// </summary>
        /// <param name="token">Token是用户的令牌，是一个字符串，由用户的身份信息和调用时间等通过加密、解密后组成</param>
        /// <param name="OldSerialNo">医嘱编号</param>
        /// <param name="SerialNo">申请单号</param>
        /// <param name="errorMsg">错误信息</param>
        /// <param name="returnValue">返回的结构列表字符串</param>
        /// <returns>
        /// 0 成功
        /// 负数:失败
        ///     -1:系统出现异常
        ///     -100:非法调用(如用户身份不合法等)
        /// </returns>
        [WebMethod]
        public int DownloadReportFormBy_OldSerialNo_SerialNo(string token, string OldSerialNo, string SerialNo, out string errorMsg, out byte[] returnValue)
        {
            Log.Info(String.Format("下载报告列表开始token={0},SerialNo={1},OldSerialNo={2}", token, SerialNo, OldSerialNo));
            errorMsg = "";
            returnValue = null;
            bool validate = WL.Common.Tools.checkCallWebServiceUserValidate(token, out errorMsg);
            if (validate == false)
            {
                Log.Error(errorMsg);
                return -100;
            }
            try
            {
                if (SerialNo ==null && SerialNo.Trim() == "")
                {
                    errorMsg = "参数SerialNo为空!";
                    returnValue = null;
                    return -2;
                }
                if (OldSerialNo == null && OldSerialNo.Trim() == "")
                {
                    errorMsg = "参数OldSerialNo为空!";
                    returnValue = null;
                    return -2;
                }
                string whereSQL = "SerialNo='" + SerialNo.Replace("\'", "\'\'") + "' and OldSerialNo='" + OldSerialNo.Replace("\'", "\'\'") + "'";
                returnValue = WL.Common.ReportData.DownloadReportFormBy_HisOrder_SerialNo(whereSQL);
                if (returnValue == null)
                {
                    errorMsg = "无报告或报告文件!";
                    return 0;
                }
            }
            catch (System.Exception ex)
            {
                errorMsg = "根据检验报告单编号获取检验报告单ReportForm列表出错:" + ex.Message;
                returnValue = null;
                return -1;
            }
            return 0;
        }
        /// <summary>
        /// 根据医院代码、申请单号返回PDF报告单文件
        /// </summary>
        /// <param name="token">Token是用户的令牌，是一个字符串，由用户的身份信息和调用时间等通过加密、解密后组成</param>
        /// <param name="OldSerialNo">医院代码</param>
        /// <param name="SerialNo">申请单号</param>
        /// <param name="errorMsg">错误信息</param>
        /// <param name="returnValue">返回的结构列表字符串</param>
        /// <returns>
        /// 0 成功
        /// 负数:失败
        ///     -1:系统出现异常
        ///     -100:非法调用(如用户身份不合法等)
        /// </returns>
        [WebMethod]
        public int DownloadReportFormBy_ClientNo_SerialNo(string token, string ClientNo, string SerialNo, out string errorMsg, out byte[] returnValue)
        {
            Log.Info(String.Format("下载报告列表开始token={0},SerialNo={1},ClientNo={2}", token, SerialNo, ClientNo));
            errorMsg = "";
            returnValue = null;
            bool validate = WL.Common.Tools.checkCallWebServiceUserValidate(token, out errorMsg);
            if (validate == false)
            {
                Log.Error(errorMsg);
                return -100;
            }
            try
            {
                if (SerialNo == null && SerialNo.Trim() == "")
                {
                    errorMsg = "参数SerialNo为空!";
                    returnValue = null;
                    return -2;
                }
                if (ClientNo == null && ClientNo.Trim() == "")
                {
                    errorMsg = "参数ClientNo为空!";
                    returnValue = null;
                    return -2;
                }
                string whereSQL = "SerialNo='" + SerialNo + "' and ClientNo='" + ClientNo + "'";
                returnValue = WL.Common.ReportData.DownloadReportFormBy_HisOrder_SerialNo(whereSQL);
                if (returnValue == null)
                {
                    errorMsg = "无报告或报告文件!";
                    return 0;
                }
            }
            catch (System.Exception ex)
            {
                errorMsg = "根据检验报告单编号获取检验报告单ReportForm列表出错:" + ex.Message;
                returnValue = null;
                return -1;
            }
            return 0;
        }
    }
}
