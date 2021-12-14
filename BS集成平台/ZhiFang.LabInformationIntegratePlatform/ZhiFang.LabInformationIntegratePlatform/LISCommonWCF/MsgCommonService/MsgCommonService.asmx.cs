using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LIIP.ViewObject.Request;

namespace ZhiFang.LabInformationIntegratePlatform.LISCommonWCF.MsgCommonService
{
    /// <summary>
    /// MsgCommonService1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class MsgCommonService_WebService : System.Web.Services.WebService
    {
        //MsgCommonService baseclass = new MsgCommonService();
        [WebMethod]
        public BaseResultDataValue MsgSend(SCMsg_OTTH entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                entity.SCMsg = null;
                IApplicationContext context = ContextRegistry.GetContext();
                object MsgCommonService = context.GetObject("ZhiFang.LabInformationIntegratePlatform.LISCommonWCF.MsgCommonService.MsgCommonService");
                IMsgCommonService IMsgCommonService = (IMsgCommonService)MsgCommonService;
                brdv = IMsgCommonService.MsgSend(entity);
            }
           catch(Exception e)
            {
                ZhiFang.Common.Log.Log.Error("MsgCommonService_WebService.MsgSend.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "错误信息：服务异常！";
            }
            return brdv;
        }

        //[WebMethod]
        //public BaseResultDataValue MsgHandleSearch(SCMsg_OTTH_Search entity)
        //{
        //    BaseResultDataValue brdv = new BaseResultDataValue();
        //    try
        //    {
        //        //entity.SCMsg = null;
        //        IApplicationContext context = ContextRegistry.GetContext();
        //        object MsgCommonService = context.GetObject("ZhiFang.LabInformationIntegratePlatform.LISCommonWCF.MsgCommonService.MsgCommonService");
        //        IMsgCommonService IMsgCommonService = (IMsgCommonService)MsgCommonService;
        //        brdv = IMsgCommonService.MsgHandleSearch(entity);
        //    }
        //    catch (Exception e)
        //    {
        //        ZhiFang.Common.Log.Log.Error("MsgCommonService_WebService.MsgHandleSearch.异常：" + e.ToString());
        //        brdv.success = false;
        //        brdv.ErrorInfo = "错误信息：服务异常！";
        //    }
        //    return brdv;
        //}
    }
}
