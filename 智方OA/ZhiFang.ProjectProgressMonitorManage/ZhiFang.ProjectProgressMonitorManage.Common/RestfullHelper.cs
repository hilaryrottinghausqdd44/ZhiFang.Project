using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace ZhiFang.ProjectProgressMonitorManage.Common
{
    public static class RestfullHelper
    {
       // System.Net.ServicePointManage = 512;
        public static string InvkerRestServiceByGet(string url, string param)
        {
            HttpWebRequest request = WebRequest.Create(url+"?"+param) as HttpWebRequest;
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

        
    }
}
