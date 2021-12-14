using System;

namespace ZhiFang.ReagentSys.Client
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ZhiFang.Entity.ReagentSys.Client.BParameterCache.ApplicationCache = this;
            if (BParameterCache.ApplicationCache == null && ZhiFang.Entity.ReagentSys.Client.BParameterCache.ApplicationCache != null)
            {
                BParameterCache.ApplicationCache = ZhiFang.Entity.ReagentSys.Client.BParameterCache.ApplicationCache;
            }
            else if (BParameterCache.ApplicationCache == null)
            {
                BParameterCache.ApplicationCache = this;
            }
        }
        protected void Application_End(object sender, EventArgs e)
        {
            ZhiFang.Entity.ReagentSys.Client.BParameterCache.RemoveAllCache();
            BParameterCache.RemoveAllCache();
        }
    }
}
