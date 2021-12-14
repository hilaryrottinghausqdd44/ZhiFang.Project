using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace ZhiFang.Tools
{
    public class WebRequestHelp
    {
        /// <summary>
        /// Post 提交调用抓取
        /// </summary>
        /// <param name="url">提交地址</param>
        /// <param name="param">参数</param>
        /// <returns>string</returns>
        public static string webRequestPost(string url, string param)
        {
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(param);

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url + "?" + param);
            req.Method = "Post";
            req.Timeout = 120 * 1000;
            req.ContentType = "application/x-www-form-urlencoded;";
            req.ContentLength = bs.Length;

            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(bs, 0, bs.Length);
                reqStream.Flush();
            }
            using (WebResponse wr = req.GetResponse())
            {
                //在这里对接收到的页面内容进行处理 

                Stream strm = wr.GetResponseStream();

                StreamReader sr = new StreamReader(strm, System.Text.Encoding.UTF8);

                string line;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                while ((line = sr.ReadLine()) != null)
                {
                    sb.Append(line + System.Environment.NewLine);
                }
                sr.Close();
                strm.Close();
                return sb.ToString();
            }
        }

        public static string Post(string PostData, string DataType, string url, int timeout)
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

                /***************************************************************
                * 下面设置HttpWebRequest的相关属性
                * ************************************************************/
                request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "POST";
                request.Timeout = timeout * 5000;

                //设置POST的数据类型和长度
                if (DataType.Trim().ToUpper() == "XML")
                {
                    request.ContentType = "text/xml";
                }
                if (DataType.Trim().ToUpper() == "JSON")
                {
                    request.ContentType = "application/json";
                }
                byte[] data = System.Text.Encoding.UTF8.GetBytes(PostData);
                request.ContentLength = data.Length;

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
                ZhiFang.Common.Log.Log.Error("WebRequestHelp.Get.HttpService.Thread - caught ThreadAbortException - resetting.");
                ZhiFang.Common.Log.Log.Error("WebRequestHelp.Get.Exception message: " + e.Message);
                System.Threading.Thread.ResetAbort();
            }
            catch (WebException e)
            {
                ZhiFang.Common.Log.Log.Error("WebRequestHelp.Get.HttpService.WebException异常：" + e.ToString());
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    ZhiFang.Common.Log.Log.Error("WebRequestHelp.Get.HttpService.StatusCode : " + ((HttpWebResponse)e.Response).StatusCode);
                    ZhiFang.Common.Log.Log.Error("WebRequestHelp.Get.HttpService.StatusDescription : " + ((HttpWebResponse)e.Response).StatusDescription);
                }
                throw e;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("WebRequestHelp.Get.HttpService.Exception异常：" + e.ToString());
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
        public static string Get(string url, bool IsVerification = false)
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
                ZhiFang.Common.Log.Log.Error("WebRequestHelp.Get.HttpService.Thread - caught ThreadAbortException - resetting.");
                ZhiFang.Common.Log.Log.Error("WebRequestHelp.Get.Exception message: " + e.ToString());
                System.Threading.Thread.ResetAbort();
            }
            catch (WebException e)
            {
                ZhiFang.Common.Log.Log.Error("WebRequestHelp.Get.HttpService.WebException异常：" + e.ToString());
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    ZhiFang.Common.Log.Log.Error("WebRequestHelp.Get.HttpService.StatusCode : " + ((HttpWebResponse)e.Response).StatusCode);
                    ZhiFang.Common.Log.Log.Error("WebRequestHelp.Get.HttpService.StatusDescription : " + ((HttpWebResponse)e.Response).StatusDescription);
                }
                throw e;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("WebRequestHelp.Get.HttpService.Exception异常：" + e.ToString());
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

        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            //直接确认，否则打不开    
            return true;
        }
    }
}
