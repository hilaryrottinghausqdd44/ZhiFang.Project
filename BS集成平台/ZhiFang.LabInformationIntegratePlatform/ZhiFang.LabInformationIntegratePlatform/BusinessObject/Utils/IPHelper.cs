using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace ZhiFang.LabInformationIntegratePlatform.BusinessObject.Utils
{
    public class IPHelper
    {
        public static string GetClientIP()
        {
            string IP4Address = String.Empty;
            try
            {

                foreach (IPAddress IPA in Dns.GetHostAddresses(HttpContext.Current.Request.UserHostAddress))
                {
                    if (IPA.AddressFamily.ToString() == "InterNetwork")
                    {
                        IP4Address = IPA.ToString();
                        break;
                    }
                }

                if (IP4Address != String.Empty)
                {
                    return IP4Address;
                }

                foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
                {
                    if (IPA.AddressFamily.ToString() == "InterNetwork")
                    {
                        IP4Address = IPA.ToString();
                        break;
                    }
                }
                if (IP4Address.Trim() == "::1")
                    IP4Address = "127.0.0.1";
                return IP4Address;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("IPHelper.GetClientIP.异常：" + e.ToString());
                return String.Empty;
            }
        }

        public static string GetClientIPByWebSocket(HubCallerContext context)
        {
            try
            {
                string IP4Address = String.Empty;
                if (context.Request.Environment.Keys.Contains("server.RemoteIpAddress"))
                    IP4Address = context.Request.Environment["server.RemoteIpAddress"].ToString();

                if (IP4Address.Trim() == "::1")
                    IP4Address = "127.0.0.1";
                return IP4Address;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("IPHelper.GetClientIPByWebSocket.异常：" + e.ToString());
                return String.Empty;
            }
        }

        /// <summary>
        /// 检查IP地址格式
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>  
        public static bool IsIP(string ip)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
    }
}