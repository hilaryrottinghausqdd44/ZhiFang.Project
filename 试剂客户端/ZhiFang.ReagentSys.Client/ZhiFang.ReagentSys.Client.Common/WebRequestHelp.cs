using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ZhiFang.ReagentSys.Client.Common
{
    public class WebRequestHelp
    {
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
                if (DataType.Trim().ToUpper() == "HTML")
                {
                    request.ContentType = "text/html";
                }
                if (DataType.Trim().ToUpper() == "XML")
                {
                    request.ContentType = "text/xml";
                }
                if (DataType.Trim().ToUpper() == "JSON")
                {
                    request.ContentType = "application/json";
                }
                if (DataType.Trim().ToUpper() == "JSON2")
                {
                    request.ContentType = "text/json;charset=UTF-8";
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
        /// Post 提交调用抓取
        /// </summary>
        /// <param name="url">提交地址</param>
        /// <param name="param">参数信息</param>
        /// <returns>string</returns>
        public static string webRequestPost(string url, string param)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>(); //参数列表
            parameters.Add("postData", param);
            // byte[] bs = Encoding.UTF8.GetBytes(BuildQuery(parameters, "utf8"));
            byte[] bs = Encoding.UTF8.GetBytes("postData={\"test\":\"11\"}");
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);// + "?"+ param
            req.Method = "POST";
            req.Timeout = 100 * 5000;
            req.ContentType = "application/json";//;charset=utf-8
            req.Headers.Set("Pragma", "no-cache");
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
        /// <summary>
        /// 处理http GET请求，返回数据
        /// </summary>
        /// <param name="url">请求的url地址</param>
        /// <returns>http GET成功后返回的数据，失败抛WebException异常</returns>
        public static string Get(string url, int timeout)
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
                request.Timeout = timeout * 5000;
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
        /// <summary>
        /// 组装请求参数
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string BuildQuery(IDictionary<string, string> parameters, string encode)
        {
            StringBuilder postData = new StringBuilder();
            bool hasParam = false;
            IEnumerator<KeyValuePair<string, string>> dem = parameters.GetEnumerator();
            while (dem.MoveNext())
            {
                string name = dem.Current.Key;
                string value = dem.Current.Value;
                // 忽略参数名或参数值为空的参数
                if (!string.IsNullOrEmpty(name))
                {
                    if (hasParam)
                    {
                        postData.Append("&");
                    }
                    postData.Append(name);
                    postData.Append("=");
                    if (encode == "gb2312")
                    {
                        postData.Append(HttpUtility.UrlEncode(value, Encoding.GetEncoding("gb2312")));
                    }
                    else if (encode == "utf8")
                    {
                        //postData.Append(HttpUtility.UrlEncode(value, Encoding.UTF8));
                        postData.Append(value);
                    }
                    else
                    {
                        postData.Append(value);
                    }
                    hasParam = true;
                }
            }
            ZhiFang.Common.Log.Log.Debug("BuildQuery.postData:" + postData.ToString());
            return postData.ToString();
        }


        /// <summary>
        /// 使用WebClient方式Post请求网址
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postString"></param>
        /// <returns></returns>
        public static string WebClientPost(string url, System.Collections.Specialized.NameValueCollection postString)
        {
            System.Net.WebClient myClient = new System.Net.WebClient();

            myClient.Encoding = System.Text.Encoding.UTF8; 
            myClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");//采取POST方式必须加的header，如果改为GET方式的话就去掉这句话即可  
            byte[] responseData = myClient.UploadValues(url, "POST", postString);
            string jsonText = Encoding.UTF8.GetString(responseData);//解码  

            return jsonText;
        }

        /// <summary>
        /// Http Get请求
        /// </summary>
        /// <param name="url">请求地址</param> 
        /// <param name="para">请求参数</param>
        /// <returns>-1失败；成功返回请求到的信息</returns>
        public static BaseResultDataValueLIIP WebHttpGet(string url, string para)
        {
            BaseResultDataValueLIIP tempResult = new BaseResultDataValueLIIP();
            try
            {
                ZhiFang.Common.Log.Log.Info("服务请求开始，URL：" + url);
                ZhiFang.Common.Log.Log.Info("服务请求参数：" + para);

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url + (para == "" ? "" : "?") + para);
                httpWebRequest.Method = "GET";
                httpWebRequest.ContentType = "text/html;charset=UTF-8";

                WebResponse webResponse = httpWebRequest.GetResponse();
                HttpWebResponse httpWebResponse = (HttpWebResponse)webResponse;
                System.IO.Stream stream = httpWebResponse.GetResponseStream();
                System.IO.StreamReader streamReader = new System.IO.StreamReader(stream, Encoding.GetEncoding("UTF-8"));
                string resultString = streamReader.ReadToEnd(); //请求返回的数据
                //ZhiFang.Common.Log.Log.Info("服务请求返回值：" + resultString);
                streamReader.Close();
                stream.Close();

                JObject jo = (JObject)JsonConvert.DeserializeObject(resultString);
                bool success = Convert.ToBoolean(jo["success"].ToString());
                string ResultDataValue = jo["ResultDataValue"].ToString();
                string ErrorInfo = jo["ErrorInfo"].ToString();
                string ResultDataFormatType = jo["ResultDataFormatType"].ToString();

                tempResult.success = success;
                tempResult.ErrorInfo = ErrorInfo;
                tempResult.ResultDataFormatType = ResultDataFormatType;
                tempResult.ResultDataValue = ResultDataValue;

                ZhiFang.Common.Log.Log.Info("服务请求返回success=" + success);
            }
            catch (Exception ex)
            {
                tempResult.success = false;
                tempResult.ErrorInfo = ex.Message;
                ZhiFang.Common.Log.Log.Error("WebHttpGet服务请求异常：", ex);
            }
            ZhiFang.Common.Log.Log.Info("服务请求结束");
            return tempResult;
        }

    }

    public class BaseResultDataValueLIIP
    {
        private bool successFlag = true;
        private string errorInfo = "";
        private string resultdataformattype = "JSON";

        public bool success
        {
            get { return successFlag; }
            set { successFlag = value; }
        }

        public string ErrorInfo
        {
            get { return errorInfo; }
            set { errorInfo = value; }
        }

        public string ResultDataFormatType
        {
            get { return resultdataformattype; }
            set { resultdataformattype = value; }
        }

        public string ResultDataValue { get; set; }
    }

}
