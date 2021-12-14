using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ZhiFang.LIIP.Common
{


    /// <summary>    /// Cookie 操作帮助类    /// </summary>    
    public class CookieHelper
    {
        #region 写cookie值
        /// <summary>        
        /// 写cookie值        
        /// </summary>        
        /// <param name="strName">名称</param>        
        /// <param name="strValue">值</param>        
        public static void Write(string strName, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            else
            {
                HttpContext.Current.Request.Cookies[strName].Value = HttpUtility.UrlEncode(strValue, Encoding.GetEncoding("UTF-8")); ;
            }
            cookie.Value = HttpUtility.UrlEncode(strValue, Encoding.GetEncoding("UTF-8"));
            HttpContext.Current.Response.AppendCookie(cookie);

        }
        /// <summary>        
        /// 写cookie值        
        /// </summary>        
        /// <param name="strName">名称</param>        
        /// <param name="strValue">值</param>        
        /// <param name="doMain">域  例如:contoso.com</param>        
        public static void WriteWithDomain(string strName, string strValue, string doMain)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = HttpUtility.UrlEncode(strValue, Encoding.GetEncoding("UTF-8"));
            cookie.Domain = doMain;
            HttpContext.Current.Response.AppendCookie(cookie);
        }
        /// <summary>        
        /// 写cookie值        
        /// </summary>        
        /// <param name="strName">名称</param>        
        /// <param name="key">键名</param>        
        /// <param name="strValue">值</param>        
        public static void Write(string strName, string key, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie[key] = HttpUtility.UrlEncode(strValue, Encoding.GetEncoding("UTF-8"));
            HttpContext.Current.Response.AppendCookie(cookie);
        }
        ///<summary>       
        /// 写cookie值        
        ///</summary>        
        ///<param name="strName">名称</param>       
        /// <param name="key">键名</param>      
        /// <param name="strValue">值</param>      
        /// <param name="doMain">域  例如:contoso.com</param>  
        public static void WriteWithDomain(string strName, string key, string strValue, string doMain)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Domain = doMain;
            cookie[key] = HttpUtility.UrlEncode(strValue, Encoding.GetEncoding("UTF-8"));
            HttpContext.Current.Response.AppendCookie(cookie);
        }
        /// <summary>        
        /// 写cookie值        
        /// </summary>       
        /// <param name="strName">名称</param>        
        /// <param name="strValue">值</param>       
        /// <param name="strValue">过期时间(分钟)</param>     
        public static void Write(string strName, string strValue, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = HttpUtility.UrlEncode(strValue, Encoding.GetEncoding("UTF-8"));
            cookie.Expires = System.DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }
        /// <summary>        
        /// 写cookie值        
        /// </summary>        
        /// <param name="strName">名称</param>        
        /// <param name="strValue">值</param>        
        /// <param name="strValue">过期时间(分钟)</param>        
        /// <param name="doMain">域  例如:contoso.com</param>        
        public static void WriteWithDomain(string strName, string strValue, int expires, string doMain)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Domain = doMain;
            cookie.Value = HttpUtility.UrlEncode(strValue, Encoding.GetEncoding("UTF-8"));
            cookie.Expires = System.DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }
        /// <summary>        
        /// 写cookie值        
        /// </summary>        
        /// <param name="strName">名称</param>        
        /// <param name="key">键名</param>        
        /// <param name="strValue">值</param>        
        /// <param name="expires">过期时间(分钟)</param>       
        public static void Write(string strName, string key, string strValue, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie[key] = HttpUtility.UrlEncode(strValue, Encoding.GetEncoding("UTF-8"));
            cookie.Expires = System.DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }
        /// <summary>        
        /// 写cookie值        
        /// </summary>        
        /// <param name="strName">名称</param>        
        /// <param name="key">键名</param>       
        /// <param name="strValue">值</param>        
        /// <param name="expires">过期时间(分钟)</param>        
        /// <param name="doMain">域  例如:contoso.com</param>        
        public static void WriteWithDomain(string strName, string key, string strValue, int expires, string doMain)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Domain = doMain;
            cookie[key] = HttpUtility.UrlEncode(strValue, Encoding.GetEncoding("UTF-8"));
            cookie.Expires = System.DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }
        #endregion
        #region 读cookie值
        /// <summary>        
        /// 读cookie值        
        /// </summary>        
        /// <param name="strName">名称</param>        
        /// <returns>cookie值</returns>        
        public static string Read(string strName)
        {
            if (HttpContext.Current == null) return null;
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
            {
                return HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[strName].Value.ToString(), Encoding.GetEncoding("UTF-8"));
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>        
        /// 读cookie值        
        /// </summary>        
        /// <param name="strName">名称</param>        
        /// <param name="key">键名</param>        
        /// <returns>cookie值</returns>        
        public static string Read(string strName, string key)
        {
            if (HttpContext.Current == null) return null;
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
            {
                return HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[strName][key].ToString(), Encoding.GetEncoding("UTF-8"));
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion
        #region Cookie 删除
        /// <summary>        
        /// 删除        
        /// </summary>        
        /// <param name="name">名称</param>        
        public static void Remove(string name)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[name] != null)
            {
                HttpCookie myCookie = new HttpCookie(name);
                myCookie.Expires = DateTime.Now.AddMinutes(-1);
                HttpContext.Current.Response.Cookies.Add(myCookie);
            }
        }
        /// <summary>        
        /// 删除        
        /// </summary>        
        /// <param name="name">名称</param>        
        /// <param name="key">二级建名称</param>        
        public static void Remove(string name, string key)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[name] != null && !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies[name][key]))
            {
                string[] temp = HttpContext.Current.Request.Cookies[name].Value.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
                List<string> list = new List<string>();
                foreach (string item in temp)
                {
                    if (item.StartsWith(key))
                    {
                        continue;
                    }
                    else
                    {
                        list.Add(item);
                    }
                }
                Write(name, string.Join("&", list.ToArray()));
            }
        }
        #endregion
    }

    //public void Write()
    //{
    //    //不用域名，通用性            
    //    VCommons.Http.CookieHelper.Write("test", "name", "bobo123");
    //    //加域名，ＩＰ地址形式            
    //    VCommons.Http.CookieHelper.WriteWithDomain("test2", "nameByDomain", "bobo>Domain>IP", "127.0.0.1");
    //    //加域名，可以用localhost进行本地调试          
    //    VCommons.Http.CookieHelper.WriteWithDomain("test3", "nameByDomain", "bobo>localhost", "localhost");
    //}
    //public void Clear()
    //{
    //    //清除cookies名称为test的所有cookies元素    
    //    VCommons.Http.CookieHelper.Remove("test");
    //    //将指定名称下的指定键值设置成空        
    //    VCommons.Http.CookieHelper.Write("test", "name", string.Empty);
    //    VCommons.Http.CookieHelper.Write("test2", "nameByDomain", string.Empty);
    //    VCommons.Http.CookieHelper.Write("test3", "nameByDomain", string.Empty);
    //}

}
