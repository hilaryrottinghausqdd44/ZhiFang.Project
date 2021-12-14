using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Common.Public
{
    public class ScriptStr
    {
        public static string Alert(string content)
        {
            return "<script>alert('" + content + "');</script>";
        }
        public static string AlertAndRedirect(string content, string url)
        {
            return "<script>alert('" + content + "'); document.location.href=" + url + ";</script>";
        }
        public static string AlertAndCloseAndRedirect(string content, string url)
        {
            return "<script>alert('" + content + "');window.close(); opener.location.href=" + url + ";</script>";
        }
        public static string AlertAndClose(string content)
        {
            return "<script>alert('" + content + "');window.close();</script>";
        }
        public static string AlertAndFunction(string content, string function)
        {
            return "<script>alert('" + content + "');" + function + "</script>";
        }
        public static string AlertAndCloseAndFunction(string content, string function)
        {
            return "<script>alert('" + content + "');window.close();" + function + "</script>";
        } 
    }
}
