using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ZhiFang.WeiXin.Common
{
    public class GetFilePath
    {
        public static System.Web.UI.Page p = new System.Web.UI.Page();
        public static string GetPhysicsFilePath(string path)
        {
            try
            {
                if (HttpContext.Current == null)
                {
                    if (System.Environment.CurrentDirectory == AppDomain.CurrentDomain.BaseDirectory)
                    {
                        path = AppDomain.CurrentDomain.BaseDirectory + "//" + path;
                    }
                    else
                    {
                        path = AppDomain.CurrentDomain.BaseDirectory + "//" + path;
                    }
                }
                else
                {
                    path = HttpContext.Current.Server.MapPath("~/" + path + "/");
                }
                return path;
            }
            catch (Exception e)
            {
                return "";
            }
        }
        public static string GetBinFilePath(string path)
        {
            try
            {
                if (System.Environment.CurrentDirectory == AppDomain.CurrentDomain.BaseDirectory)
                {
                    path = AppDomain.CurrentDomain.BaseDirectory +"//"+ path;
                }
                else
                {
                    path = AppDomain.CurrentDomain.BaseDirectory + "Bin/" + "//" + path;
                }
                return path;
            }
            catch (Exception e)
            {
                return "";
            }            
        }
        public static string GetFilePathByFullyQualifiedName(string path)
        {
            string MyPath = System.IO.Path.GetDirectoryName(
                        System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName) + "//" + path;
            return MyPath;
        }
        public static string WinDiretory(string WinUrl)
        {
            return AppDomain.CurrentDomain.BaseDirectory + WinUrl;
        }
        public static string WebDiretory(string WebUrl)
        {
            return p.Server.MapPath(WebUrl);
        }
    }
}
