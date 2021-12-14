using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using ZhiFang.LabInformationIntegratePlatform.MessageHub;

namespace ZhiFang.LabInformationIntegratePlatform
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            try
            {
                AreaRegistration.RegisterAllAreas();
                GlobalConfiguration.Configure(WebApiConfig.Register);
                FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
                RouteConfig.RegisterRoutes(RouteTable.Routes);
                //BundleConfig.RegisterBundles(BundleTable.Bundles);
                ZhiFang.Common.Log.Log.Debug("启动！");
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug($"Application_Start.异常:{e.ToString()}");
            }
        }
        protected void Application_End(object sender, EventArgs e)
        {
            ZhiFang.Common.Log.Log.Debug("回收开始！");
            ZhiFang.Common.Log.Log.Debug("回收结束！");
        }
    }
}
