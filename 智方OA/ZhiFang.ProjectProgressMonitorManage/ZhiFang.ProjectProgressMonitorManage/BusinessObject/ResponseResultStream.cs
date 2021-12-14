using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using ZhiFang.ProjectProgressMonitorManage.BusinessObject;

namespace ZhiFang.ProjectProgressMonitorManage
{
    public static class ResponseResultStream
    {
        /// <summary>
        /// 前台通过form.submit带有file时服务返回信息的封装处理
        /// </summary>
        /// <param name="id">附件的Id</param>
        /// <param name="errorInfo">错误提示信息</param>
        /// <returns></returns>
        public static Stream GetResultInfoOfStream(string resultInfo)
        {
            MemoryStream memoryStream = null;
            byte[] infoByte = Encoding.UTF8.GetBytes(resultInfo);
            memoryStream = new MemoryStream(infoByte);
            Encoding code = Encoding.GetEncoding("UTF-8");
            System.Web.HttpContext.Current.Response.ContentEncoding = code;
            WebOperationContext.Current.OutgoingResponse.ContentType = "text/plain";
            return memoryStream;
        }
        /// <summary>
        /// 获取附件文件不存在时的错误提示文件处理
        /// </summary>
        /// <param name="id">附件的Id</param>
        /// <param name="errorInfo">错误提示信息</param>
        /// <returns></returns>
        public static MemoryStream GetErrMemoryStreamInfo(long id, string errorInfo)
        {
            MemoryStream memoryStream = null;
            string fileName = "ErrFile.html";
            if (String.IsNullOrEmpty(errorInfo))
                errorInfo = "文件不存在!请联系管理员。";
            StringBuilder strb = new StringBuilder("<div class='alert alert-warning' style='margin:40px 20px;text-align:center;padding-top:40px;padding-bottom:40px;'><h4>错误提示信息</h4><p style='color: red; padding: 5px; word -break:break-all; word - wrap:break-word; '>");
            strb.Append(errorInfo);
            strb.Append("</p></div>");
            byte[] infoByte = Encoding.UTF8.GetBytes(strb.ToString());
            memoryStream = new MemoryStream(infoByte);
            Encoding code = Encoding.GetEncoding("UTF-8");
            System.Web.HttpContext.Current.Response.ContentEncoding = code;
            //fileName = EncodeFileName.ToEncodeFileName(fileName);
            WebOperationContext.Current.OutgoingResponse.ContentType = "text/html; charset=UTF-8";
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=\"" + fileName + "\"");
            return memoryStream;
        }
    }
}