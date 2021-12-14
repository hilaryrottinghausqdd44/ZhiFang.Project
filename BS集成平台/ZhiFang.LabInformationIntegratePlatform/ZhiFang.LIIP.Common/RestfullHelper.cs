using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ZhiFang.Common.Log;

namespace ZhiFang.LIIP.Common
{
    public static class RestfullHelper
    {
        // System.Net.ServicePointManage = 512;
        public static string InvkerRestServiceByGet(string url, string param)
        {
            HttpWebRequest request = WebRequest.Create(url + "?" + param) as HttpWebRequest;
            request.Method = "GET";
            request.ContentType = "application/json; charset=utf-8";
            request.Accept = "*/*";
            request.Timeout = 20000;
            string result = null;
            // Get response
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                // Get the response stream
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                result = reader.ReadToEnd();
            }
            return result;
        }
        public static string InvkerRestServiceByGet(string url)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            request.ContentType = "application/json; charset=utf-8";
            request.Accept = "*/*";
            request.Timeout = 20000;
            string result = null;
            // Get response
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                // Get the response stream
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                result = reader.ReadToEnd();
            }
            return result;
        }

        public static string InvkerRestServicePost(string xml, string url, bool isUseCert, int timeout, string SSLCERT_PATH, string SSLCERT_PASSWORD)
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
                    //ServicePointManager.ServerCertificateValidationCallback =
                      //      new RemoteCertificateValidationCallback(CheckValidationResult);
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
                    ZhiFang.Common.Log.Log.Debug("SSLCERT_PATH:" + SSLCERT_PATH);
                    X509Certificate2 cert = new X509Certificate2(SSLCERT_PATH, SSLCERT_PASSWORD, X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.Exportable);
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

        public static string InvkerRestServicePost(string PostData, string DataType, string url, bool isUseCert, int timeout, string SSLCERT_PATH, string SSLCERT_PASSWORD, bool IsVerification = false)
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
                if (IsVerification)
                {
                    //设置https验证方式
                    if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                    {
                       // ServicePointManager.ServerCertificateValidationCallback =
                         //       new RemoteCertificateValidationCallback(CheckValidationResult);
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
                    ZhiFang.Common.Log.Log.Debug("SSLCERT_PATH:" + SSLCERT_PATH);
                    X509Certificate2 cert = new X509Certificate2(SSLCERT_PATH, SSLCERT_PASSWORD);
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

        public static string InvkerRestServicePost(string PostData, string DataType, string url, int timeout)
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
                if (DataType.Trim().ToUpper() == "TEXT")
                {
                    request.ContentType = "text/plain";
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

        public static string InvkerRestServicePost(string PostData, string DataType, string url, int timeout,Dictionary<string,string> headlist)
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
                if (DataType.Trim().ToUpper() == "TEXT")
                {
                    request.ContentType = "text/plain";
                }
                if (headlist != null && headlist.Count > 0)
                {
                    for (int i = 0; i < headlist.Count; i++)
                    {
                        request.Headers[headlist.ElementAt(i).Key] = headlist.ElementAt(i).Value;
                    }
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
        public static string MD5Sign(string str)
        {
           Log.Debug("MD5Sign.str:" + str);
            var md5 = MD5.Create();
            var bs = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            var sb = new StringBuilder();
            foreach (byte b in bs)
            {
                sb.Append(b.ToString("x2"));
            }
            //所有字符转为大写
            return sb.ToString().ToLower();
        }
    }
}
