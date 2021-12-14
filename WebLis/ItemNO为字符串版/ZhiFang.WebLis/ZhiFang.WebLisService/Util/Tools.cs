using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;


namespace ZhiFang.WebLisService.Util
{
    public class Tools
    {
        public Tools()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}



        /// <summary>
        /// 数据导出
        /// Response.ContentType指定文件类型 可以为application/ms-excel application/ms-word application/ms-txt application/ms-html 或其他浏览器可直接支持文档
        /// 引用页面要加属性EnableEventValidation="false",并在后台方法重载事件:
        /// public override void VerifyRenderingInServerForm(Control control){}
        /// 导出才不会出错
        /// </summary>
        /// <param name="fileName">要,但不要加扩展名</param>
        /// <param name="gv"></param>
        public static void exportGridViewDataToExcel(Page page, string fileName, GridView dsResult)
        {
            try
            {
                //定义文档类型、字符编码
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Buffer = true;
                //下面这行很重要， attachment 参数表示作为附件下载，您可以改成 online在线打开
                //filename=FileFlow.xls 指定输出文件的名称，注意其扩展名和指定文件类型相符，可以为：.doc .xls .txt .htm
                //文件名称要加密,这样汉字文件名称才不会乱码(文件名称只取前八位)
                string filenameExport = System.Web.HttpUtility.UrlEncode(fileName) + ".xls";
                //设置输出参数
                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + filenameExport);
                HttpContext.Current.Response.Charset = "gb2312";//导出数据的编码不能用utf-8,否则会乱码
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
                HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
                //以文本流的形式输出统计数据
                dsResult.RenderControl(oHtmlTextWriter);
                page.Response.Write(oStringWriter.ToString());
                try
                {
                    page.Response.End();
                }
                finally
                {
                }
            }
            catch (System.Exception ex)
            {
                throw (ex);
                //ECDS.Util.JScript.Alert(page, ex.Message);
            }
        }



        /// <summary>
        /// 数据导出
        /// Response.ContentType指定文件类型 可以为application/ms-excel application/ms-word application/ms-txt application/ms-html 或其他浏览器可直接支持文档
        /// 
        /// </summary>
        /// <param name="fileName">要</param>
        /// <param name="gv"></param>
        public static void exportDataGridDataToExcel(Page page, string fileName, DataGrid dsResult)
        {
            try
            {
                //定义文档类型、字符编码
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Buffer = true;
                //下面这行很重要， attachment 参数表示作为附件下载，您可以改成 online在线打开
                //filename=FileFlow.xls 指定输出文件的名称，注意其扩展名和指定文件类型相符，可以为：.doc .xls .txt .htm
                //文件名称要加密,这样汉字文件名称才不会乱码(文件名称只取前八位)
                string filenameExport = System.Web.HttpUtility.UrlEncode(fileName) + ".xls";
                //设置输出参数
                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + filenameExport);
                HttpContext.Current.Response.Charset = "gb2312";//导出数据的编码不能用utf-8,否则会乱码
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
                HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
                //以文本流的形式输出统计数据
                dsResult.RenderControl(oHtmlTextWriter);
                page.Response.Write(oStringWriter.ToString());
                try
                {
                    page.Response.End();
                }
                finally
                {
                }
            }
            catch (System.Exception ex)
            {
                ZhiFang.Util.JScript.Alert(page, ex.Message);
            }
        }



        /// <summary>
        /// 从页面保存的HttpCookie里获取用户的登录帐号:
        /// 如果帐号已经过期,返回null
        /// </summary>
        /// <returns></returns>
        public static string getUserNameFromHttpCookie(Page page)
        {
            return Tools.getParamFromHttpCookie(page, "UserName");
        }


        /// <summary>
        /// 从页面保存的HttpCookie里获取指定的HttpCookie内容:
        /// 如果帐号已经过期,返回null
        /// </summary>
        /// <returns></returns>
        public static string getParamFromHttpCookie(Page page, string cookieName)
        {
            string cookieValue = null;
            HttpCookie cookieUserName = page.Request.Cookies[cookieName];
            if (cookieUserName != null)
            {
                cookieValue = cookieUserName.Value;
            }
            return cookieValue;
        }



        /// <summary>
        /// 从页面URL(Request)中获取指定参数的值:
        /// 将参数值转换成安全的参数,防止SQL注入
        /// 如果参数不存在,或者参数的类型不一致,返回null
        /// </summary>
        /// <param name="page">页面</param>
        /// <param name="paramName">参数名称</param>
        /// <returns>字符串</returns>
        public static string getSafeRequest(Page page, string paramName)
        {
            string paramValue = null;
            if (page.Request[paramName] != null)
            {
                paramValue = page.Request[paramName].ToString();
                //先将单引号'的内码chr(27)替换成单引号'
                paramValue = paramValue.Replace("chr(27)", "'");
                paramValue = paramValue.Replace("Chr(27)", "'");
                //防止SQL注入
                paramValue = paramValue.Replace("'", "''");
            }
            return paramValue;
        }



        /// <summary>
        /// 获取系统的启动目录
        /// 目录都是以\结束
        /// </summary>
        /// <returns></returns>
        public static string getSystemPath()
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory;
            return path;
        }



        /// <summary>
        /// 获取系统的XML临时文件存放目录:在虚拟目录(根目录)下的XML目录
        /// 目录都是以\结束
        /// </summary>
        /// <returns></returns>
        public static string getXmlSavePath()
        {
            string path = getSystemPath();
            path += "XML\\";
            return path;
        }



        /// <summary>
        /// 获取模块树的XML临时文件存放目录:系统的XML临时文件存放目录下的Modal目录
        /// 目录都是以\结束
        /// </summary>
        /// <returns></returns>
        public static string getModalXmlSavePath()
        {
            string path = getXmlSavePath();
            path += "Modal\\";
            return path;
        }



        /// <summary>
        /// 转换绝对路径为相对路径(相对于某页面的启动相对目录)
        /// 即在当前页面下,能在目录前面加../访问到指定文件的相对目录
        /// </summary>
        /// <param name="page">页面</param>
        /// <param name="diskPath">磁盘文件(完整的路径,可以带加文件名称)</param>
        /// <returns></returns>
        public static string convertDiskPathToRelativePath(Page page, string diskPath)
        {
            //磁盘文件的基本信息
            System.IO.FileInfo fileInfoDiskPath = new System.IO.FileInfo(diskPath);
            //页面的路径基本信息
            string pagePath = page.Server.MapPath(page.Request.Url.LocalPath);
            pagePath = pagePath.Replace("/", "\\");
            System.IO.FileInfo fileInfoPagePath = new System.IO.FileInfo(pagePath);
            //系统的启动路径
            string systemPath = ZhiFang.WebLisService.Util.Tools.getSystemPath();
            //相对目录
            string relativePath = diskPath.Replace(systemPath, "");
            string[] path1 = fileInfoDiskPath.DirectoryName.Split(new char[] { '\\' });
            string[] path2 = systemPath.Split(new char[] { '\\' });
            int addNum = path1.Length - path2.Length + 1;//加1因为系统目录后面是目录分隔符\
            for (int i = 0; i < addNum; i++)
            {
                relativePath = "../" + relativePath;
            }
            relativePath = relativePath.Replace("\\", "/");
            return relativePath;
        }




    }
}
