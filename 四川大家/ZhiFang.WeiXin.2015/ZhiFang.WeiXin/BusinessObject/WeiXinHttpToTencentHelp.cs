using System;
using System.Collections.Generic;
using System.Web;
using System.Net;
using System.IO;
using System.Text;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace ZhiFang.WeiXin.BusinessObject
{
    public class WeiXinHttpToTencentHelp
    {
        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            //直接确认，否则打不开    
            return true;
        }

        public static string Post(string xml, string url, bool isUseCert, int timeout)
        {
            System.GC.Collect();//垃圾回收，回收没有正常关闭的http连接

            string result = "";//返回结果

            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream reqStream = null;

            try
            {
                //设置最大连接数
                ServicePointManager.DefaultConnectionLimit = 5000;
                //设置https验证方式
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                            new RemoteCertificateValidationCallback(CheckValidationResult);
                }

                /***************************************************************
                * 下面设置HttpWebRequest的相关属性
                * ************************************************************/
                request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "POST";
                request.Timeout = timeout * 5000;

                //设置代理服务器
                //WebProxy proxy = new WebProxy();                          //定义一个网关对象
                //proxy.Address = new Uri(WxPayConfig.PROXY_URL);              //网关服务器端口:端口
                //request.Proxy = proxy;

                //设置POST的数据类型和长度
                request.ContentType = "text/xml";
                byte[] data = System.Text.Encoding.UTF8.GetBytes(xml);
                request.ContentLength = data.Length;

                //是否使用证书
                if (isUseCert)
                {
                    //string path = HttpContext.Current.Request.PhysicalApplicationPath;
                    ZhiFang.Common.Log.Log.Debug("PayBase.SSLCERT_PATH:"+ PayBase.SSLCERT_PATH);
                    X509Certificate2 cert = new X509Certificate2(PayBase.SSLCERT_PATH, PayBase.SSLCERT_PASSWORD, X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.Exportable);
                    request.ClientCertificates.Add(cert);
                    request.Timeout = 60000;
                    ZhiFang.Common.Log.Log.Debug("WxPayApi.PostXml used cert");
                }
                
                //往服务器写入数据
                reqStream = request.GetRequestStream();
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();

                //获取服务端返回
                response = (HttpWebResponse)request.GetResponse();

                //获取服务端返回数据
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                result = sr.ReadToEnd().Trim();
                sr.Close();
            }
            catch (System.Threading.ThreadAbortException e)
            {
                ZhiFang.Common.Log.Log.Error("HttpService。Thread - caught ThreadAbortException - resetting.");
                ZhiFang.Common.Log.Log.Error("Exception message："+ e.Message);
                System.Threading.Thread.ResetAbort();
            }
            catch (WebException e)
            {
                ZhiFang.Common.Log.Log.Error("HttpService异常："+e.ToString());
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    ZhiFang.Common.Log.Log.Error("HttpService.StatusCode : " + ((HttpWebResponse)e.Response).StatusCode);
                    ZhiFang.Common.Log.Log.Error("HttpService.StatusDescription : " + ((HttpWebResponse)e.Response).StatusDescription);
                }
                throw new Exception(e.ToString());
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("HttpService异常:"+ e.ToString());
                throw new Exception(e.ToString());
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

        public static string Post(string PostData, string DataType,string url, bool isUseCert, int timeout, bool IsVerification = false)
        {
            System.GC.Collect();//垃圾回收，回收没有正常关闭的http连接

            string result = "";//返回结果

            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream reqStream = null;

            try
            {
                //设置最大连接数
                ServicePointManager.DefaultConnectionLimit =5000;
                if (IsVerification)
                {
                    //设置https验证方式
                    if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                    {
                        ServicePointManager.ServerCertificateValidationCallback =
                                new RemoteCertificateValidationCallback(CheckValidationResult);
                    }
                }
                /***************************************************************
                * 下面设置HttpWebRequest的相关属性
                * ************************************************************/
                request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "POST";
                request.Timeout = timeout * 5000;

                //设置代理服务器
                //WebProxy proxy = new WebProxy();                          //定义一个网关对象
                //proxy.Address = new Uri(WxPayConfig.PROXY_URL);              //网关服务器端口:端口
                //request.Proxy = proxy;

                //设置POST的数据类型和长度
                if (DataType.Trim().ToUpper() == "XML")
                {
                    request.ContentType = "text/xml";
                }
                if (DataType.Trim().ToUpper() == "JSON")
                {
                    request.ContentType = "application/x-www-form-urlencoded";
                }
                byte[] data = System.Text.Encoding.UTF8.GetBytes(PostData);
                request.ContentLength = data.Length;

                //是否使用证书
                if (isUseCert)
                {
                    //string path = HttpContext.Current.Request.PhysicalApplicationPath;
                    ZhiFang.Common.Log.Log.Debug("PayBase.SSLCERT_PATH:" + PayBase.SSLCERT_PATH);
                    X509Certificate2 cert = new X509Certificate2(PayBase.SSLCERT_PATH, PayBase.SSLCERT_PASSWORD);
                    request.ClientCertificates.Add(cert);
                    request.Timeout = 60000;
                    ZhiFang.Common.Log.Log.Debug("WxPayApi.PostXml used cert");
                }
                
                //往服务器写入数据
                reqStream = request.GetRequestStream();
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();

                //获取服务端返回
                response = (HttpWebResponse)request.GetResponse();

                //获取服务端返回数据
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                result = sr.ReadToEnd().Trim();
                sr.Close();
            }
            catch (System.Threading.ThreadAbortException e)
            {
                ZhiFang.Common.Log.Log.Error("HttpService。Thread - caught ThreadAbortException - resetting.");
                ZhiFang.Common.Log.Log.Error("Exception message：" + e.Message);
                System.Threading.Thread.ResetAbort();
            }
            catch (WebException e)
            {
                ZhiFang.Common.Log.Log.Error("HttpService异常：" + e.ToString());
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    ZhiFang.Common.Log.Log.Error("HttpService.StatusCode : " + ((HttpWebResponse)e.Response).StatusCode);
                    ZhiFang.Common.Log.Log.Error("HttpService.StatusDescription : " + ((HttpWebResponse)e.Response).StatusDescription);
                }
                throw new Exception(e.ToString());
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("HttpService异常:" + e.ToString());
                throw new Exception(e.ToString());
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

        /// <summary>
        /// 向指定的 URL POST 数据，并返回页面
        /// </summary>
        /// <param name="uriString">POST URL</param>
        /// <param name="postString">POST 的 数据</param>
        /// <param name="postStringEncoding">POST 数据的 CharSet</param>
        /// <param name="dataEncoding">页面的 CharSet</param>
        /// <returns>页面的源文件</returns>
      

        /// <summary>
        /// 处理http GET请求，返回数据
        /// </summary>
        /// <param name="url">请求的url地址</param>
        /// <returns>http GET成功后返回的数据，失败抛WebException异常</returns>
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
                    ServicePointManager.ServerCertificateValidationCallback =
                            new RemoteCertificateValidationCallback(CheckValidationResult);
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
                ZhiFang.Common.Log.Log.Error("WeiXinHttpToTencentHelp.Get.HttpService.Thread - caught ThreadAbortException - resetting.");
                ZhiFang.Common.Log.Log.Error("WeiXinHttpToTencentHelp.Get.Exception message: " + e.Message);
                System.Threading.Thread.ResetAbort();
            }
            catch (WebException e)
            {
                ZhiFang.Common.Log.Log.Error("WeiXinHttpToTencentHelp.Get.HttpService.WebException异常：" + e.ToString());
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    ZhiFang.Common.Log.Log.Error("WeiXinHttpToTencentHelp.Get.HttpService.StatusCode : " + ((HttpWebResponse)e.Response).StatusCode);
                    ZhiFang.Common.Log.Log.Error("WeiXinHttpToTencentHelp.Get.HttpService.StatusDescription : " + ((HttpWebResponse)e.Response).StatusDescription);
                }
                throw e;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("WeiXinHttpToTencentHelp.Get.HttpService.Exception异常：" + e.ToString());
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

        /// <summary>
        /// 处理http GET请求，返回数据
        /// </summary>
        /// <param name="url">请求的url地址</param>
        /// <returns>http GET成功后返回的数据，失败抛WebException异常</returns>
        public static string Get(string url,bool IsVerification=false)
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
                if (IsVerification)
                {
                    //设置https验证方式
                    if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                    {
                        ServicePointManager.ServerCertificateValidationCallback =
                                new RemoteCertificateValidationCallback(CheckValidationResult);
                    }
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
                ZhiFang.Common.Log.Log.Error("WeiXinHttpToTencentHelp.Get.HttpService.Thread - caught ThreadAbortException - resetting.");
                ZhiFang.Common.Log.Log.Error("WeiXinHttpToTencentHelp.Get.Exception message: " + e.ToString());
                System.Threading.Thread.ResetAbort();
            }
            catch (WebException e)
            {
                ZhiFang.Common.Log.Log.Error("WeiXinHttpToTencentHelp.Get.HttpService.WebException异常：" + e.ToString());
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    ZhiFang.Common.Log.Log.Error("WeiXinHttpToTencentHelp.Get.HttpService.StatusCode : " + ((HttpWebResponse)e.Response).StatusCode);
                    ZhiFang.Common.Log.Log.Error("WeiXinHttpToTencentHelp.Get.HttpService.StatusDescription : " + ((HttpWebResponse)e.Response).StatusDescription);
                }
                throw e;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("WeiXinHttpToTencentHelp.Get.HttpService.Exception异常：" + e.ToString());
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
    }
}
