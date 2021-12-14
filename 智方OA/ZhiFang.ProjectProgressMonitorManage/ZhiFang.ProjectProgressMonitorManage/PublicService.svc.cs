using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.IO;
using System.Web;
using ZhiFang.Common.Public;
using ZhiFang.Entity.RBAC;
using ZhiFang.ServiceCommon.RBAC;
using System.Reflection;
using ZhiFang.Entity.Base;
using ZhiFang.DBUpdate.PM;

namespace ZhiFang.ProjectProgressMonitorManage
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class PublicService : PublicServiceCommon, ZhiFang.ProjectProgressMonitorManage.ServerContract.IPublicService, ZhiFang.ProjectProgressMonitorManage.ServerContract.IPublicServiceWebHttpBinding
    {
        public BaseResultDataValue GetServiceSystemVersion()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = false;
            try
            {
                brdv.ResultDataValue = ZhiFang.Common.Public.AssemblyHelp.GetAssemblyVersion(System.Reflection.Assembly.GetExecutingAssembly());
                brdv.success = true;                
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.ToString();
            }
            return brdv;
        }
        public BaseResultDataValue GetServiceSystemFileVersion()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = false;
            try
            {
                object v = ZhiFang.Common.Public.AssemblyHelp.GetAssemblyCustomAttributes(System.Reflection.Assembly.GetExecutingAssembly(), typeof(AssemblyFileVersionAttribute));
                if (v != null && v.ToString() != "")
                {
                    brdv.ResultDataValue = ((AssemblyFileVersionAttribute)v).Version;
                }
                brdv.success = true;
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = e.ToString();
            }
            return brdv;
        }

        public BaseResultDataValue Public_UDTO_DBVersionUpate(string curDBVersion)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (string.IsNullOrEmpty(curDBVersion))
            {
                curDBVersion = "1.0.0.0";
                //curDBVersion = HttpContext.Current.Application["SYS_DBVersion"].ToString();
            }
            brdv.success = ZhiFang.DBUpdate.PM.DBUpdate.DataBaseUpdate(curDBVersion);
            return brdv;
        }
    }
    /// <summary>
    /// 解决高版本VS（vs2010以上）表单提交问题
    /// </summary>
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