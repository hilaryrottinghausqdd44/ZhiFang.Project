using System.ComponentModel;
using System.Web.Services;
using System.Web.Services.Protocols;

using ECDS.Common;
using System.Web.UI.MobileControls;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Configuration;
using System.Xml;
using System.Text;
using System.Data;
using System.IO;
using ZhiFang.Common.Dictionary;
using Common;

namespace ZhiFang.WebLisService.WebService
{
    /// <summary>
    /// WebLisRepor 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org1/")]
    //[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    //下面一行是为了DELPHI调用时参数传入值不为NULL(但在IE浏览页面出错！！！)
    [SoapRpcService(RoutingStyle = SoapServiceRoutingStyle.SoapAction)]
    //解决上一行出现的错误
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    public class WebLisReport : System.Web.Services.WebService
    {
        [WebMethod]
        public string HelloWorld(string name)
        {
            Log.Info("WebLisReport--HelloWorld:" + name);
            return "Hello World " + name;
        }

        #region 检验报告部分


        /// <summary>
        /// 查询报告单的主表列表
        /// </summary>
        /// <param name="whereSQL"></param>
        /// <returns></returns>
        public string getReportFormList(string whereSQL)
        {
            return WL.Common.ReportData.DownloadReportFormList(whereSQL);
        }



        /// <summary>
        /// 查询报告单的子表列表
        /// </summary>
        /// <param name="whereSQL"></param>
        /// <returns></returns>
        public string getReportItemList(string whereSQL, out string errorMsg)
        {
            errorMsg = "";
            return WL.Common.ReportData.DownloadReportFormItemList(whereSQL, out errorMsg);
        }



        /// <summary>
        /// 上传检验报告,2009版本使用
        /// 
        /// </summary>
        /// <param name="token">Token是用户的令牌，是一个字符串，由用户的身份信息和调用时间等通过加密、解密后组成</param>
        /// <param name="xmlData">xml数据</param>
        /// <param name="pdfdata">pdf检验报告</param>
        /// <param name="pdfdata_td">套打pdf检验报告</param>
        /// <param name="fileData">其他文件，例如jpg,frp，word,rtf等</param>
        /// <param name="fileType">其他文件的类型</param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns>
        /// 0 成功
        /// 负数:失败
        ///     -1:系统出现异常
        ///     -100:非法调用(如用户身份不合法等)
        /// </returns>
        [WebMethod]
        public int UpLoadReportFromBytes(string token, byte[] xmlData, byte[] pdfdata, byte[] pdfdata_td, byte[] fileData, string fileType, out string errorMsg)
        {
            //errorMsg = "";
            //Log.Debug("UpLoadReportFromBytes上传开始！");
            ////bool validate = true;
            ////validate = WL.Common.Tools.checkCallWebServiceUserValidate(token, out errorMsg);
            ////if (validate == false)
            ////{
            ////    Log.Error(errorMsg);
            ////    return -100;
            ////}
            ////errorMsg = "";
            //return WL.Common.ReportData.UpLoadReportDataFromBytes(xmlData, pdfdata, pdfdata_td, fileData, fileType, out errorMsg);

            ReportFormWebService rfws = new ReportFormWebService();
            return rfws.UpLoadReportFromBytes(token, xmlData, pdfdata, pdfdata_td, fileData, fileType,out errorMsg);
        }

        [WebMethod]
        public bool UpLoadReportImage(string ImageName, byte[] ImageData, out string errorMsg)
        {
            //Log.Info("UpLoadReportImage上传开始!");
            try
            {
                errorMsg = "";
                Log.Info("开始保存报告图片！");
                if (ImageName != "" && ImageName != null && ImageData != null)
                {
                    Log.Info(ImageName);
                    if (ImageData.Length > 0)
                    {
                        if (ConfigurationManager.AppSettings["ReportIncludeImage"] != null && ConfigurationManager.AppSettings["ReportIncludeImage"].ToString().Trim().Length > 0)
                        {
                            //F:\WEBLIS\WebLisSXTY\reportFile/2014/11/3/2014-11-03@@@_284850_4_1_Cs01_2014-11-03,1.jpg
                            //F:\WEBLIS\WebLisSXTY\reportFile/2014/9 /4/2014-09-04@@@_279125_4_1_1409030210_2014-09-04,1.jpg
                            string[] p = ImageName.Split('@');
                            Log.Error("ImageName" + ImageName);
                            Log.Error("报告图片存放路径：" + ConfigurationManager.AppSettings["ReportIncludeImage"].ToString().Trim());
                            string path = ConfigurationManager.AppSettings["ReportIncludeImage"].ToString().Trim();
                            string path1 = @path + "/" + Convert.ToDateTime(p[1]).Year + "/" + Convert.ToDateTime(p[1]).Month + "/" + Convert.ToDateTime(p[1]).Day + "/" + p[0] + "/" + p[3].Split('_')[1] + "/";
                            Log.Error("p[3].Split('_')[1]" + p[3].Split('_')[1]);
                            string tmpfilename = ImageName.Replace(p[1], "").Replace(p[0], "");
                            if (clsCommon.FilesHelper.CreatDirFile(path1, tmpfilename, ImageData))
                            {
                                Log.Error("保存报告图片成功！" + path1 + "@@@" + tmpfilename);
                            }
                            else
                            {
                                Log.Error("保存报告图片失败！" + path1 + "@@@" + tmpfilename);
                            }
                        }
                    }
                    return true;
                }
                else
                {
                    Log.Error("没有报告图片！");
                }
                return true;
            }
            catch (Exception ex)
            {
                errorMsg = ex.ToString();
                Log.Error("保存报告图片错误！" + ex.ToString());
                return false;
            }
            //errorMsg = "";

        }
        /// 
        /// <summary>
        /// 上传检验报告,新增报告图片上传功能
        /// 
        /// </summary>
        /// <param name="token">Token是用户的令牌，是一个字符串，由用户的身份信息和调用时间等通过加密、解密后组成</param>
        /// <param name="xmlData">xml数据</param>
        /// <param name="pdfdata">pdf检验报告</param>
        /// <param name="pdfdata_td">套打pdf检验报告</param>
        /// <param name="fileData">其他文件，例如jpg,frp，word,rtf等</param>
        /// <param name="ImageNameList">报告所需图片的名称集合</param>
        /// <param name="ImageByteList">报告所需图片的数据流集合</param>
        /// <param name="fileType">其他文件的类型</param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns>
        /// 0 成功
        /// 负数:失败
        ///     -1:系统出现异常
        ///     -100:非法调用(如用户身份不合法等)
        /// </returns>
        [WebMethod]
        public int UpLoadReportFromBytes_ImageList(string token, byte[] xmlData, byte[] pdfdata, byte[] pdfdata_td, byte[] fileData, List<string> ImageNameList, List<byte[]> ImageByteList, string fileType, out string errorMsg)
        {
            errorMsg = "";
            //bool validate = true;
            //validate = WL.Common.Tools.checkCallWebServiceUserValidate(token, out errorMsg);
            //if (validate == false)
            //{
            //    Log.Error(errorMsg);
            //    return -100;
            //}
            //errorMsg = "";
            try
            {
                Log.Error("开始保存报告图片！");
                if (ImageNameList != null && ImageByteList != null)
                {
                    Log.Error("报告图片列表不为空！个数：" + ImageNameList.Count);
                    if (ImageNameList.Count > 0 && ImageByteList.Count > 0 && ImageNameList.Count == ImageByteList.Count)
                    {
                        Log.Error("报告图片数组名称个数：" + ImageNameList.Count + "@@@图片流个数：" + ImageByteList.Count);
                        if (ConfigurationManager.AppSettings["ReportIncludeImage"] != null && ConfigurationManager.AppSettings["ReportIncludeImage"].ToString().Trim().Length > 0)
                        {
                            Log.Error("报告图片存放路径：" + ConfigurationManager.AppSettings["ReportIncludeImage"].ToString().Trim());
                            string path = ConfigurationManager.AppSettings["ReportIncludeImage"].ToString().Trim();
                            int j = 0;
                            for (int i = 0; i < ImageNameList.Count; i++)
                            {
                                string path1 = "";
                                string tmpfilename = "";
                                if (ImageNameList[i].IndexOf('@') > -1)
                                {
                                    Log.Info(ImageNameList[i]);
                                    Log.Info("ImageNameList的Length:" + ImageNameList.Count+".i:"+i);
                                    string[] p = ImageNameList[i].Split('@');
                                    Log.Info("p的Length:" + p.Length);
                                    //if (p[3].Split('_').Length > 1)
                                    //{
                                    //    path1 = @path + "/" + Convert.ToDateTime(p[1]).Year + "/" + Convert.ToDateTime(p[1]).Month + "/" + Convert.ToDateTime(p[1]).Day + "/" + p[0] + "/" + p[3].Split('_')[1] + "/";

                                    //}
                                    //else
                                    //{
                                        path1 = @path + "/" + Convert.ToDateTime(p[1]).Year + "/" + Convert.ToDateTime(p[1]).Month + "/" + Convert.ToDateTime(p[1]).Day + "/" + p[0] + "/";

                                    //}
                                    tmpfilename = ImageNameList[i].Replace(p[1], "").Replace(p[0], "");                                    
                                    Log.Info("path1:" + path1 + ".tmpfilename:" + tmpfilename);
                                }
                                else
                                {
                                    
                                    string NewFileName = ImageNameList[i].Split('_')[5].Split(',')[0].ToString().Replace("：", ":");
                                    Log.Info("NewFileName：" + NewFileName);
                                    j = j + 1;
                                    path = ConfigurationManager.AppSettings["jpgorpdf"].ToString().Trim();
                                    path1 = @path + "/" + Convert.ToDateTime(NewFileName).Year + "/" + Convert.ToDateTime(NewFileName).Month + "/" + Convert.ToDateTime(NewFileName).Day + "/" + NewFileName.ToString().Replace(":", "：") + "/" + ImageNameList[i].Split('_')[1];
                                    tmpfilename = ImageNameList[i];
                                }
                                Log.Info(" ImageByteList的Length:" + ImageByteList.Count+".i:"+i);
                                if (clsCommon.FilesHelper.CreatDirFile(path1, tmpfilename, ImageByteList[i]))
                                {
                                    Log.Error("保存报告图片成功！" + path1 + "@@@" + tmpfilename);
                                }
                                else
                                {
                                    Log.Error("保存报告图片失败！" + path1 + "@@@" + tmpfilename);
                                }

                            }
                        }
                        else
                        {
                            Log.Error("报告图片存放路径为空！");
                        }
                    }
                    else
                    {
                        errorMsg = "保存报告图片错误！报告图片数量同报告图片名称数量不统一！";
                    }
                }
                else
                {
                    Log.Error("没有报告图片！");
                }
            }
            catch (Exception e)
            {
                Log.Error("保存报告图片错误！" + e.ToString());
                errorMsg = "保存报告图片错误！" + e.ToString();
            }
            return WL.Common.ReportData.UpLoadReportDataFromBytes(xmlData, pdfdata, pdfdata_td, fileData, fileType, out errorMsg);
        }
        /// <summary>
        /// 远程报告状态更新
        /// </summary>
        /// <param name="weblisflag"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        [WebMethod]
        public bool UpdateReportStatus(string barcode, string weblisflag, out string errmsg)
        {
            errmsg = "";
            return WL.Common.ReportData.UpdateReportStatus(barcode, weblisflag, out errmsg);
        }


        /// <summary>
        /// 根据查询条件获取检验报告单ReportForm列表
        /// </summary>
        /// <param name="token">Token是用户的令牌，是一个字符串，由用户的身份信息和调用时间等通过加密、解密后组成</param>
        /// <param name="whereSQL">查询条件，如果是空串，则返回所有的数据，尽量不要传空串</param>
        /// <param name="errorMsg">错误信息</param>
        /// <param name="returnValue">返回的结构列表字符串</param>
        /// <returns>
        /// 0 成功
        /// 负数:失败
        ///     -1:系统出现异常
        ///     -100:非法调用(如用户身份不合法等)
        /// </returns>
        [WebMethod]
        public int DownloadReportFormList(string token, string whereSQL, out string errorMsg, out string returnValue)
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

                returnValue = getReportFormList(whereSQL);
            }
            catch (System.Exception ex)
            {
                errorMsg = "" + ex.Message;
                returnValue = "";
                return -1;
            }
            return 0;
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
                returnValue = getReportFormList(whereSQL);
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
        /// 根据医院分类和查询条件获取检验报告单ReportForm列表
        /// </summary>
        /// <param name="token">Token是用户的令牌，是一个字符串，由用户的身份信息和调用时间等通过加密、解密后组成</param>
        /// <param name="OrgName">医院分类,可以查多个，用逗号分隔</param>
        /// <param name="WhereClause">查询条件</param>
        /// <param name="errorMsg">错误信息</param>
        /// <param name="returnValue">返回的结构列表字符串</param>
        /// <returns>
        /// 0 成功
        /// 负数:失败
        ///     -1:系统出现异常
        ///     -100:非法调用(如用户身份不合法等)
        /// </returns>
        [WebMethod]
        public int DownloadReportFormListByOrg(string token, string OrgName, string WhereClause, out string errorMsg, out string returnValue)
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
                string whereSQL = "";
                if (OrgName != "全部医院")
                {
                    if (OrgName.IndexOf(",") < 0)
                        WhereClause = WhereClause + " and clientname='" + OrgName + "'";
                    else
                    {
                        string[] allOrgs = OrgName.Split(",".ToCharArray());
                        string orgWheres = "";
                        foreach (string eachorg in allOrgs)
                        {
                            orgWheres = orgWheres + " or clientname='" + eachorg + "'";
                        }
                        if (orgWheres.Length > 3)
                            orgWheres = orgWheres.Substring(4);
                        WhereClause = WhereClause + " and (" + orgWheres + ")";
                    }
                }
                whereSQL = WhereClause;
                returnValue = getReportFormList(whereSQL);
            }
            catch (System.Exception ex)
            {
                errorMsg = "" + ex.Message;
                returnValue = "";
                return -1;
            }
            return 0;
        }





        /// <summary>
        /// 根据查询条件获取检验报告单ReportForm和ReportItem列表
        /// <param name="token">Token是用户的令牌，是一个字符串，由用户的身份信息和调用时间等通过加密、解密后组成</param>
        /// <param name="whereSQL">查询条件，如果是空串，则返回所有的数据，尽量不要传空串</param>
        /// <param name="errorMsg">错误信息</param>
        /// <param name="returnValue">返回的结构列表字符串</param>
        /// <returns>
        /// 0 成功
        /// 负数:失败
        ///     -1:系统出现异常
        ///     -100:非法调用(如用户身份不合法等)
        /// </returns>
        [WebMethod]
        public int DownloadReportFormItemList(string token, string whereSQL, out string errorMsg, out string returnValue)
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
                returnValue = getReportItemList(whereSQL, out errorMsg);
            }
            catch (System.Exception ex)
            {
                errorMsg = "根据查询条件获取检验报告单ReportForm和ReportItem列表出错:" + ex.Message;
                returnValue = "";
                return -1;
            }
            return 0;
        }



        /// <summary>
        /// 根据SerialNo获取检验报告单ReportForm和ReportItem列表
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
        public int DownloadReportFormItemListBySerialNo(string token, string SerialNo, out string errorMsg, out string returnValue)
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
                    errorMsg = "根据SerialNo获取检验报告单ReportForm和ReportItem列表出错:参数SerialNo应该有内容!";
                    returnValue = "";
                    return -2;
                }
                string whereSQL = "SerialNo='" + SerialNo + "'";
                returnValue = getReportItemList(whereSQL, out errorMsg);
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
                returnValue = getReportItemList(whereSQL, out errorMsg);
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
        /// 根据FORMNO获取检验报告单ReportForm和ReportItem列表
        /// </summary>
        /// <param name="token">Token是用户的令牌，是一个字符串，由用户的身份信息和调用时间等通过加密、解密后组成</param>
        /// <param name="FormNO"></param>
        /// <param name="errorMsg">错误信息</param>
        /// <param name="returnValue">返回的结构列表字符串</param>
        /// <returns>
        /// 0 成功
        /// 负数:失败
        ///     -1:系统出现异常
        ///     -100:非法调用(如用户身份不合法等)
        /// </returns>
        [WebMethod]
        public int DownloadReportFormItemListByFormNO(string token, string FormNO, out string errorMsg, out string returnValue)
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
                if (FormNO == "")
                {
                    errorMsg = "根据FormNO获取检验报告单ReportForm和ReportItem列表出错:参数FormNO应该有内容!";
                    returnValue = "";
                    return -2;
                }
                string whereSQL = "FormNO='" + FormNO + "'";
                returnValue = getReportItemList(whereSQL, out errorMsg);
            }
            catch (System.Exception ex)
            {
                errorMsg = "根据FormNO获取检验报告单ReportForm和ReportItem列表:" + ex.Message;
                returnValue = "";
                return -1;
            }
            return 0;
        }



        /// <summary>
        /// 根据送检单位ClientNO获取检验报告单ReportForm和ReportItem列表
        /// </summary>
        /// <param name="token">Token是用户的令牌，是一个字符串，由用户的身份信息和调用时间等通过加密、解密后组成</param>
        /// <param name="ClientNO">送检单位编号</param>
        /// <param name="errorMsg">错误信息</param>
        /// <param name="returnValue">返回的结构列表字符串</param>
        /// <returns>
        /// 0 成功
        /// 负数:失败
        ///     -1:系统出现异常
        ///     -100:非法调用(如用户身份不合法等)
        /// </returns>
        [WebMethod]
        public int DownloadReportFormItemListByClientNO(string token, string ClientNO, out string errorMsg, out string returnValue)
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
                if (ClientNO == "")
                {
                    errorMsg = "根据送检单位ClientNO获取检验报告单ReportForm和ReportItem列表出错:参数ClientNO应该有内容!";
                    returnValue = "";
                    return -2;
                }
                string whereSQL = "ClientNO='" + ClientNO + "'";
                returnValue = getReportItemList(whereSQL, out errorMsg);
            }
            catch (System.Exception ex)
            {
                errorMsg = "根据送检单位ClientNO获取检验报告单ReportForm和ReportItem列表:" + ex.Message;
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
        /// 根据检验单号获取PDF文件
        /// </summary>
        /// <param name="token">Token是用户的令牌，是一个字符串，由用户的身份信息和调用时间等通过加密、解密后组成</param>
        /// <param name="SerialNo">检验单号</param>
        /// <param name="errorMsg">错误信息</param>
        /// <param name="returnValue">返回的结构列表byte[]</param>
        /// <returns>
        /// 0 成功
        /// 负数:失败
        ///     -1:系统出现异常
        ///     -100:非法调用(如用户身份不合法等)
        /// </returns>
        [WebMethod]
        public int DownloadPDFFileBySerialNo(string token, string SerialNo, out string errorMsg, out byte[] returnValue)
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
                if (SerialNo == "")
                {
                    errorMsg = "根据检验单号" + SerialNo + "获取PDF文件的内容流出错:SerialNo参数应该有值!";
                    return -2;
                }
                returnValue = WL.Common.ReportData.DownloadPDFFileBySerialNo(SerialNo);
            }
            catch (System.Exception ex)
            {
                errorMsg = "根据检验单号" + SerialNo + "获取PDF文件的内容流出错:" + ex.Message;
                return -1;
            }
            return 0;
        }








        #endregion


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
            //UserInfo user = new UserInfo();
            return "";// user.ConfimUser(UserName, Password, out ErrDescription);
        }

        #endregion

    }
}
