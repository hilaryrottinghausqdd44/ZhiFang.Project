using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ZhiFang.LabStar.Common
{
    public class HTTPRequest
    {
        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            //直接确认，否则打不开    
            return true;
        }

        public static string Get(string url)
        {
            System.GC.Collect();
            string result = "";

            HttpWebRequest request = null;
            HttpWebResponse response = null;

            //请求url以获取数据
            try
            {
                //设置最大连接数
                ServicePointManager.DefaultConnectionLimit = 5000;
                //设置https验证方式
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                }

                /***************************************************************
                * 下面设置HttpWebRequest的相关属性
                * ************************************************************/
                request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "GET";

                //设置代理
                //WebProxy proxy = new WebProxy();
                //proxy.Address = new Uri(WxPayConfig.PROXY_URL);
                //request.Proxy = proxy;

                //获取服务器返回
                response = (HttpWebResponse)request.GetResponse();

                //获取HTTP返回数据
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                result = sr.ReadToEnd().Trim();
                sr.Close();
            }
            catch (System.Threading.ThreadAbortException e)
            {
                ZhiFang.Common.Log.Log.Error("HttpRequestHelp.Get.HttpService.Thread - caught ThreadAbortException - resetting.");
                ZhiFang.Common.Log.Log.Error("HttpRequestHelp.Get.Exception message: " + e.Message);
                System.Threading.Thread.ResetAbort();
            }
            catch (WebException e)
            {
                ZhiFang.Common.Log.Log.Error("HttpRequestHelp.Get.HttpService.WebException异常：" + e.ToString());
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    ZhiFang.Common.Log.Log.Error("HttpRequestHelp.Get.HttpService.StatusCode : " + ((HttpWebResponse)e.Response).StatusCode);
                    ZhiFang.Common.Log.Log.Error("HttpRequestHelp.Get.HttpService.StatusDescription : " + ((HttpWebResponse)e.Response).StatusDescription);
                }
                throw e;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("HttpRequestHelp.Get.HttpService.Exception异常：" + e.ToString());
                throw e;
            }
            finally
            {
                //关闭连接和流
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return result;
        }

        public static string Get(string url, out CookieCollection cookies)
        {
            System.GC.Collect();
            string result = "";
            cookies = new CookieCollection();
            HttpWebRequest request = null;
            HttpWebResponse response = null;

            //请求url以获取数据
            try
            {
                //设置最大连接数
                ServicePointManager.DefaultConnectionLimit = 5000;
                //设置https验证方式
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                }

                /***************************************************************
                * 下面设置HttpWebRequest的相关属性
                * ************************************************************/
                request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "GET";

                //设置代理
                //WebProxy proxy = new WebProxy();
                //proxy.Address = new Uri(WxPayConfig.PROXY_URL);
                //request.Proxy = proxy;

                //获取服务器返回
                response = (HttpWebResponse)request.GetResponse();
                cookies = response.Cookies;
                //获取HTTP返回数据
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                result = sr.ReadToEnd().Trim();
                sr.Close();
            }
            catch (System.Threading.ThreadAbortException e)
            {
                ZhiFang.Common.Log.Log.Error("HttpRequestHelp.Get.HttpService.Thread - caught ThreadAbortException - resetting.");
                ZhiFang.Common.Log.Log.Error("HttpRequestHelp.Get.Exception message: " + e.Message);
                System.Threading.Thread.ResetAbort();
            }
            catch (WebException e)
            {
                ZhiFang.Common.Log.Log.Error("HttpRequestHelp.Get.HttpService.WebException异常：" + e.ToString());
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    ZhiFang.Common.Log.Log.Error("HttpRequestHelp.Get.HttpService.StatusCode : " + ((HttpWebResponse)e.Response).StatusCode);
                    ZhiFang.Common.Log.Log.Error("HttpRequestHelp.Get.HttpService.StatusDescription : " + ((HttpWebResponse)e.Response).StatusDescription);
                }
                throw e;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("HttpRequestHelp.Get.HttpService.Exception异常：" + e.ToString());
                throw e;
            }
            finally
            {
                //关闭连接和流
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return result;
        }

        public static string WebRequestHttpPost(string url, string para, string contenttype)
        {
            string resultString = "0";
            try
            {
                ZhiFang.LabStar.Common.LogHelp.Info("服务请求开始，URL：" + url);
                ZhiFang.LabStar.Common.LogHelp.Info("服务请求参数：" + para);
                ZhiFang.LabStar.Common.LogHelp.Info("服务请求ContentType：" + contenttype);
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Method = "POST";
                if (string.IsNullOrEmpty(contenttype))
                    request.ContentType = "text/html";
                else
                    request.ContentType = contenttype;

                byte[] bytePara = UTF8Encoding.UTF8.GetBytes(para.ToString());
                request.ContentLength = bytePara.Length;

                using (Stream postStream = request.GetRequestStream())
                {
                    postStream.Write(bytePara, 0, bytePara.Length);
                }

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string resultStr = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    ZhiFang.LabStar.Common.LogHelp.Info("服务请求返回值：" + resultStr);
                    if (resultStr != null && resultStr.Length > 0)
                    {
                        ZhiFang.Entity.Base.BaseResultDataValue brdv = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<ZhiFang.Entity.Base.BaseResultDataValue>(resultStr);
                        resultString = (brdv != null && brdv.success) ? "0" : "-1";
                    }
                    else
                        resultString = "-1";
                }
                ZhiFang.LabStar.Common.LogHelp.Info("服务请求结束");
            }
            catch (Exception ex)
            {
                resultString = "-1";
                ZhiFang.LabStar.Common.LogHelp.Error("服务请求异常：" + ex.Message);
            }
            return resultString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">服务地址</param>
        /// <param name="para">参数</param>
        /// <param name="contenttype"></param>
        /// <param name="memoInfo">预留</param>
        /// <returns></returns>
        public static ZhiFang.Entity.Base.BaseResultDataValue WebRequestHttpPost(string url, string para, string contenttype, string memoInfo)
        {
            ZhiFang.Entity.Base.BaseResultDataValue brdv = new ZhiFang.Entity.Base.BaseResultDataValue();
            try
            {
                ZhiFang.LabStar.Common.LogHelp.Info("服务请求开始，URL：" + url);
                ZhiFang.LabStar.Common.LogHelp.Info("服务请求参数：" + para);
                ZhiFang.LabStar.Common.LogHelp.Info("服务请求ContentType：" + contenttype);
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Method = "POST";
                if (string.IsNullOrEmpty(contenttype))
                    request.ContentType = "text/html";
                else
                    request.ContentType = contenttype;

                byte[] bytePara = UTF8Encoding.UTF8.GetBytes(para.ToString());
                request.ContentLength = bytePara.Length;

                using (Stream postStream = request.GetRequestStream())
                {
                    postStream.Write(bytePara, 0, bytePara.Length);
                }

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string resultStr = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    ZhiFang.LabStar.Common.LogHelp.Info("服务请求返回值：" + resultStr);
                    if (resultStr != null && resultStr.Length > 0)
                    {
                        brdv = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<ZhiFang.Entity.Base.BaseResultDataValue>(resultStr);
                    }
                    else
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = "服务请求返回值为空！";
                    }
                }
                ZhiFang.LabStar.Common.LogHelp.Info("服务请求结束");
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "服务请求异常：" + ex.Message;
                ZhiFang.LabStar.Common.LogHelp.Error(brdv.ErrorInfo);
            }
            return brdv;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">服务地址</param>
        /// <param name="para">参数</param>
        /// <param name="contenttype"></param>
        /// <param name="memoInfo">预留</param>
        /// <returns></returns>
        public static ZhiFang.Entity.Base.BaseResultDataValue WebRequestHttpPostNotFormatting(string url, string para, string contenttype, string memoInfo)
        {
            ZhiFang.Entity.Base.BaseResultDataValue brdv = new ZhiFang.Entity.Base.BaseResultDataValue();
            try
            {
                ZhiFang.LabStar.Common.LogHelp.Info("服务请求开始，URL：" + url);
                ZhiFang.LabStar.Common.LogHelp.Info("服务请求参数：" + para);
                ZhiFang.LabStar.Common.LogHelp.Info("服务请求ContentType：" + contenttype);
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Method = "POST";
                if (string.IsNullOrEmpty(contenttype))
                    request.ContentType = "text/html";
                else
                    request.ContentType = contenttype;

                byte[] bytePara = UTF8Encoding.UTF8.GetBytes(para.ToString());
                request.ContentLength = bytePara.Length;

                using (Stream postStream = request.GetRequestStream())
                {
                    postStream.Write(bytePara, 0, bytePara.Length);
                }

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string resultStr = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    ZhiFang.LabStar.Common.LogHelp.Info("服务请求返回值：" + resultStr);
                    if (resultStr != null && resultStr.Length > 0)
                    {
                        brdv.success = true;
                        brdv.ResultDataValue = resultStr;
                    }
                    else
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = "服务请求返回值为空！";
                    }
                }
                ZhiFang.LabStar.Common.LogHelp.Info("服务请求结束");
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "服务请求异常：" + ex.Message;
                ZhiFang.LabStar.Common.LogHelp.Error(brdv.ErrorInfo);
            }
            return brdv;
        }
        public static ZhiFang.Entity.Base.BaseResultDataValue WebRequestHttpGet(string url, string para, string contenttype)
        {
            ZhiFang.Entity.Base.BaseResultDataValue brdv = new ZhiFang.Entity.Base.BaseResultDataValue();
            try
            {
                ZhiFang.LabStar.Common.LogHelp.Info("服务请求开始，URL：" + url);
                ZhiFang.LabStar.Common.LogHelp.Info("服务请求参数：" + para);
                ZhiFang.LabStar.Common.LogHelp.Info("服务请求ContentType：" + contenttype);
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url + "?" + para);
                request.Method = "GET";
                if (string.IsNullOrEmpty(contenttype))
                    request.ContentType = "text/html";
                else
                    request.ContentType = contenttype;

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string resultStr = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    ZhiFang.LabStar.Common.LogHelp.Info("服务请求返回值：" + resultStr);
                    if (resultStr != null && resultStr.Length > 0)
                        brdv = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<ZhiFang.Entity.Base.BaseResultDataValue>(resultStr);
                }
                ZhiFang.LabStar.Common.LogHelp.Info("服务请求结束");
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "服务请求异常：" + ex.Message;
                ZhiFang.LabStar.Common.LogHelp.Error("服务请求异常：" + ex.Message);
            }
            return brdv;
        }
    }
}
