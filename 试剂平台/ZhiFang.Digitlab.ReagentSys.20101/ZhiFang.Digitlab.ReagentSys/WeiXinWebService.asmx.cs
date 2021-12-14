using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IBLL.ReagentSys;
using Spring.Context;
using Spring.Context.Support;
using ZhiFang.Digitlab.ReagentSys.BusinessObject;

namespace ZhiFang.Digitlab.ReagentSys
{
    /// <summary>
    /// WeiXinWebService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class WeiXinWebService : System.Web.Services.WebService
    {

        [WebMethod]
        public BaseResultDataValue PushAddBmsCenOrderDoc(long Id)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                IApplicationContext context = ContextRegistry.GetContext();
                IBBmsCenOrderDoc IBBmsCenOrderDoc = (IBBmsCenOrderDoc)context.GetObject("BBmsCenOrderDoc");
                tempBaseResultDataValue = IBBmsCenOrderDoc.BmsCenOrderDocAndPush((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction, Id);

            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug("PushAddBmsCenOrderDoc.ex:" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        [WebMethod]
        public BaseResultDataValue BmsCenOrderDocReviewAndPush(long Id)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                IApplicationContext context = ContextRegistry.GetContext();
                IBBmsCenOrderDoc IBBmsCenOrderDoc = (IBBmsCenOrderDoc)context.GetObject("BBmsCenOrderDoc");
                tempBaseResultDataValue = IBBmsCenOrderDoc.BmsCenOrderDocReviewAndPush((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction, Id);

            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug("BmsCenOrderDocReviewAndPush.ex:" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
    }
}
