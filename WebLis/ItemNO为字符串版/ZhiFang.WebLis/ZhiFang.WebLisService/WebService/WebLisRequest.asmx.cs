using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;

using ECDS.Common;

namespace ZhiFang.WebLisService.WebService
{
    /// <summary>
    /// 负责检验单的上传和下载
    /// </summary>
    public class WebLisRequest : System.Web.Services.WebService
    {

        #region 登陆验证部分
        /// <summary>
        /// 客户端用户登录
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="Password">密码</param>
        /// <param name="ErrDescription">错误信息</param>
        /// <returns>返回结果</returns>
        [WebMethod]
        public string UserLogin(string UserName, string Password, out string ErrDescription)
        {
            ErrDescription = "";
            return "";
        }
        #endregion


        #region 检验申请单部分


        /// <summary>
        /// 查询检验申请单的列表XML
        /// </summary>
        /// <param name="whereSQL"></param>
        /// <returns></returns>
        public string getRequestFormItemList(string whereSQL, out string errorMsg)
        {
            errorMsg = "";
            return WL.Common.ReportData.DownloadReportFormItemList(whereSQL, out errorMsg);
        }



        /// <summary>
        /// 申请单上传（将上传的申请单保存到数据库中）
        /// </summary>
        /// <param name="token">Token是用户的令牌，是一个字符串，由用户的身份信息和调用时间等通过加密、解密后组成</param>
        /// <param name="xmlData">xml数据</param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns>
        /// 0 成功
        /// 负数:失败
        ///     -1:系统出现异常
        ///     -100:非法调用(如用户身份不合法等)
        /// </returns>
        [WebMethod]
        public int UpLoadRequestFromBytes(string token, byte[] xmlData, out string errorMsg)
        {
            errorMsg = "";
            bool validate = WL.Common.Tools.checkCallWebServiceUserValidate(token, out errorMsg);
            if (validate == false)
            {
                Log.Error(errorMsg);
                return -100;
            }

            errorMsg = "";
            return WL.Common.RequestData.UpLoadRequestFromBytes(xmlData, out errorMsg);
        }


        /// <summary>
        /// 从数据库中，根据医院分类和查询条件得出数据集返回XML
		/// </summary>
        /// <param name="token">Token是用户的令牌，是一个字符串，由用户的身份信息和调用时间等通过加密、解密后组成</param>
        /// <param name="OrgName">医院分类</param>
		/// <param name="WhereClause">查询条件</param>
        /// <param name="errorMsg">错误信息</param>
        /// <param name="returnValue">返回的结构列表字符串xml</param>
        /// <returns>
        /// 0 成功
        /// 负数:失败
        ///     -1:系统出现异常
        ///     -100:非法调用(如用户身份不合法等)
        /// </returns>
        [WebMethod]
        public int DownloadRequestFormList(string token, string OrgName, string WhereClause, out string errorMsg, out string returnValue)
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
            return -1;
        }



        /// <summary>
        /// 根据查询条件获取检验申请单NRequestForm和NRequestItem列表
        /// <param name="token">Token是用户的令牌，是一个字符串，由用户的身份信息和调用时间等通过加密、解密后组成</param>
        /// <param name="whereSQL">查询条件，如果是空串，则返回所有的数据，尽量不要传空串</param>
        /// <param name="errorMsg">错误信息</param>
        /// <param name="returnValue">返回的结构列表字符串xml</param>
        /// <returns>
        /// 0 成功
        /// 负数:失败
        ///     -1:系统出现异常
        ///     -100:非法调用(如用户身份不合法等)
        /// </returns>
        [WebMethod]
        public int DownloadRequestFormItemList(string token, string whereSQL, out string errorMsg, out string returnValue)
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
                returnValue = getRequestFormItemList(whereSQL, out errorMsg);
            }
            catch (System.Exception ex)
            {
                errorMsg = "根据SerialNo获取检验报告单ReportForm和ReportItem列表:" + ex.Message;
                returnValue = "";
                return -1;
            }
            return 0;

        }



        /// <summary>
        /// 根据检验申请单编号获取检验申请单NRequestForm和NRequestItem列表
        /// </summary>
        /// <param name="token">Token是用户的令牌，是一个字符串，由用户的身份信息和调用时间等通过加密、解密后组成</param>
        /// <param name="SerialNo">检验单号</param>
        /// <param name="errorMsg">错误信息</param>
        /// <param name="returnValue">返回的结构列表字符串xml</param>
        /// <returns>
        /// 0 成功
        /// 负数:失败
        ///     -1:系统出现异常
        ///     -100:非法调用(如用户身份不合法等)
        /// </returns>
        [WebMethod]
        public int DownloadReportFormListBySerialNo(string token, string SerialNo, out string errorMsg, out string returnValue)
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
                if (SerialNo == "")
                {
                    errorMsg = "根据检验申请单编号获取检验申请单NRequestForm和NRequestItem列表:参数SerialNo应该有内容!";
                    returnValue = "";
                    return -2;
                }
                string whereSQL = "SerialNo='" + SerialNo + "'";
                returnValue = getRequestFormItemList(whereSQL, out errorMsg);
            }
            catch (System.Exception ex)
            {
                errorMsg = "根据检验申请单编号获取检验申请单NRequestForm和NRequestItem列表:" + ex.Message;
                returnValue = "";
                return -1;
            }
            return 0;
        }



        /// <summary>
        /// 根据NRequestFormNo获取检验申请单NRequestForm和NRequestItem列表
        /// </summary>
        /// <param name="token">Token是用户的令牌，是一个字符串，由用户的身份信息和调用时间等通过加密、解密后组成</param>
        /// <param name="SerialNo">检验单号</param>
        /// <param name="errorMsg">错误信息</param>
        /// <param name="returnValue">返回的结构列表字符串xml</param>
        /// <returns>
        /// 0 成功
        /// 负数:失败
        ///     -1:系统出现异常
        ///     -100:非法调用(如用户身份不合法等)
        /// </returns>
        [WebMethod]
        public int DownloadReportFormListByNRequestFormNo(string token, string NRequestFormNo, out string errorMsg, out string returnValue)
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
                if (NRequestFormNo == "")
                {
                    errorMsg = "根据NRequestFormNo获取检验申请单NRequestForm和NRequestItem列表:参数NRequestFormNo应该有内容!";
                    returnValue = "";
                    return -2;
                }
                string whereSQL = "NRequestFormNo='" + NRequestFormNo + "'";
                returnValue = getRequestFormItemList(whereSQL, out errorMsg);
            }
            catch (System.Exception ex)
            {
                errorMsg = "根据NRequestFormNo获取检验申请单NRequestForm和NRequestItem列表:" + ex.Message;
                returnValue = "";
                return -1;
            }
            return 0;
        }







        #endregion


    }
}
