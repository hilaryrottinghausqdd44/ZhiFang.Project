using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Reflection;

namespace ZhiFang.WeiXin.Common
{
    public class SessionHelper
    {
        #region Session
        //public static HttpContext SessionContext = HttpContext.Current;
        public static bool SetSessionValue(string key, string value)
        {
            HttpContext.Current.Session[key] = value;
            return true;
        }

        public static bool SetSessionValue<T>(string key, T tempobject)
        {
            HttpContext.Current.Session[key] = tempobject;
            return true;
        }

        public static bool SetSessionValue<T>(T tempObject, string propertyName, string key)
        {
            PropertyInfo tempPropertyInfo = tempObject.GetType().GetProperty(propertyName);
            if (tempPropertyInfo != null)
            {
                HttpContext.Current.Session[key] = tempPropertyInfo.GetValue(tempObject, null);
            }
            return true;
        }

        public static string GetSessionValue(string key)
        {
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Session[key] != null)
                {
                    return HttpContext.Current.Session[key].ToString();
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        public static Object GetSessionObjectValue(string key)
        {
            if (HttpContext.Current.Session[key] != null)
            {
                return HttpContext.Current.Session[key];
            }
            else
            {
                return null;
            }
        }
        #endregion

    }
}
