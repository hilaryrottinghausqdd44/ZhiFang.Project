using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace ZhiFang.Util 
{
    /// <summary>
    /// ShowMessage 的摘要说明
    /// </summary>
    public class ShowMessage 
    {

        public static int messageTableWidth = 300;
        public ShowMessage() 
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }



        #region "显示提示信息"
        
        /// <summary>
        /// 显示提示信息
        /// </summary>
        /// <param name="message">提示内容</param>
        /// <returns></returns>
        public static string getShowMessageHTML(string message)
        {
            string html = "<div id='divMessage'><table width=220 border=0 cellpadding=0 align=center cellspacing=0>\r\n" +
                "<tr><td height=10></td></tr>\r\n" +
                "</table>";
            html += "<table width=" + messageTableWidth.ToString() + " bgcolor='#BDBBBC' background='/Images/Sys/MessageBox_Bg02.gif' cellSpacing=0 cellPadding=0 style='border-right: #BCC2DD 1px solid; border-top: #717171 1px solid; border-left: #717171 1px solid; border-bottom: #717171 1px solid;'>";
            html += "<tr height=26 bgcolor=#7E7F83><td align=left colspan='2'><b><font color=white>&nbsp;&nbsp;";
            html += "信息提示";
            html += "</font></b></td>\r\n";
            html += "<td align=right valign=middle><a style='cursor:hand' onclick=\"javascript:document.all('divMessage').style.display='none';\">×</a>&nbsp;</td>";
            html += "</tr>\r\n";
            html += "<tr><td rowspan=2 valign=middle align=center width=10></td>";
            html += "<td width='" + (messageTableWidth - 10).ToString() + "' height=50 valign=top align=center>" + message + "</td></tr>";
            html += "</table><br></div>";
            return html;
        }


        /// <summary>
        /// 显示提示信息
        /// </summary>
        /// <param name="message">提示内容</param>
        /// <param name="type">类型：notice 成功；warn 警告；error 错误</param>
        /// <returns></returns>

        public static string getShowMessageHTML(string message, string type)
        {
            string html = "<div id='divMessage'><table width=220 border=0 cellpadding=0 align=center cellspacing=0>\r\n" +
                "<tr><td height=10></td></tr>\r\n" +
                "</table>";
            html += "<table width=" + messageTableWidth.ToString() + " bgcolor='#BDBBBC' background='/Images/Sys/MessageBox_Bg02.gif' cellSpacing=0 cellPadding=0 style='border-right: #BCC2DD 1px solid; border-top: #717171 1px solid; border-left: #717171 1px solid; border-bottom: #717171 1px solid;'>";
            html += "<tr height=26 bgcolor=#7E7F83><td align=left colspan='2'><b><font color=white>&nbsp;&nbsp;";
            switch (type)
            {
                case "notice":
                    html += "Tips";
                    break;
                case "warn":
                    html += "Warning";
                    break;
                case "error":
                    html += "Error";
                    break;
            }
            html += "</font></b></td>\r\n";
            html += "<td align=right valign=middle><a style='cursor:hand' onclick=\"javascript:document.all('divMessage').style.display='none';\">×</a>&nbsp;</td>";
            html += "</tr>\r\n";
            html += "<tr><td rowspan=2 valign=middle align=center width=10></td>";
            html += "<td width='" + (messageTableWidth - 10).ToString() + "' height=50 valign=top align=center>" + message + "</td></tr>";
            html += "</table><br></div>";
            return html;
        }



        /// <summary>
        /// 显示提示信息
        /// </summary>
        /// <param name="message">提示内容</param>
        /// <param name="type">类型：notice 成功；warn 警告；error 错误</param>
        /// <param name="url">返回链接</param>
        /// <returns></returns>

        public static string getShowMessageHTML(string message, string type, string url)
        {
            string html = "<div id='divMessage'><table width=220 border=0 cellpadding=0 align=center cellspacing=0>\r\n" +
                "<tr><td height=10></td></tr>\r\n" +
                "</table>";
            html += "<table width=" + messageTableWidth.ToString() + " bgcolor='#BDBBBC' background='/Images/Sys/MessageBox_Bg02.gif' cellSpacing=0 cellPadding=0 style='border-right: #BCC2DD 1px solid; border-top: #717171 1px solid; border-left: #717171 1px solid; border-bottom: #717171 1px solid;'>";
            html += "<tr height=26 bgcolor=#7E7F83><td align=left colspan='2'><b><font color=white>&nbsp;&nbsp;";
            switch (type)
            {
                case "notice":
                    html += "Tips";
                    break;
                case "warn":
                    html += "Warning";
                    break;
                case "error":
                    html += "Error";
                    break;
            }
            html += "</font></b></td>\r\n";
            html += "<td align=right valign=middle><a style='cursor:hand' onclick=\"javascript:document.all('divMessage').style.display='none';\">×</a>&nbsp;</td>";
            html += "</tr>\r\n";
            html += "<tr><td rowspan=2 valign=middle align=center width=10></td>";
            html += "<td width='" + (messageTableWidth - 10).ToString() + "' height=50 valign=top align=center>" + message + "</td></tr>";
            if (url != "")
            {
                html += "<tr><td align=right><a href='" + url + "'>Back</a>&nbsp;</td></tr>";
            }
            html += "</table><br></div>";
            return html;
        }


        public static void showMessage(Page page, string message)
        {
            string html = getShowMessageHTML(message);
            ScriptManager.RegisterStartupScript(page, page.GetType(), "提示", html, true);
        }




        #endregion
    }
}
