using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZhiFang.ProjectProgressMonitorManage.HttpModule
{
    public class ContentTypeModule : IHttpModule
    {
        #region IHttpModule 成员

        public void Dispose()
        {
            
        }

        public void Init(HttpApplication context)
        {
            context.Response.ContentType = "application/json; charset=utf-8";
        }

        #endregion
    }
}