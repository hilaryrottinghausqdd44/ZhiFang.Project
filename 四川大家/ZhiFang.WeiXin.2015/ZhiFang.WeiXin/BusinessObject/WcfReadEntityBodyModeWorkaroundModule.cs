using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace ZhiFang.WeiXin
{
    public class WcfReadEntityBodyModeWorkaroundModule : IHttpModule
    {
        public void Dispose() { }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += context_BeginRequest;
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            //This will force the HttpContext.Request.ReadEntityBody to be "Classic" and will ensure compatibility..    
            Stream stream = (sender as HttpApplication).Request.InputStream;
        }
    }
}
