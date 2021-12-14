using System;
using System.Web.Services;
using ZhiFang.Entity.Base;

namespace ZhiFang.WebAssist
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
        public BaseResultDataValue PushAddReaBmsCenOrderDoc(long Id)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            //try
            //{
            //    IApplicationContext context = ContextRegistry.GetContext();
            //    IBReaBmsCenOrderDoc IBReaBmsCenOrderDoc = (IBReaBmsCenOrderDoc)context.GetObject("BReaBmsCenOrderDoc");
            //    tempBaseResultDataValue = IBReaBmsCenOrderDoc.ReaBmsCenOrderDocAndPush((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction, Id);

            //}
            //catch (Exception ex)
            //{
            //    tempBaseResultDataValue.success = false;
            //    tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
            //    ZhiFang.Common.Log.Log.Debug("PushAddReaBmsCenOrderDoc.ex:" + ex.ToString());
            //    //throw new Exception(ex.Message);
            //}
            return tempBaseResultDataValue;
        }

        [WebMethod]
        public BaseResultDataValue ReaBmsCenOrderDocReviewAndPush(long Id)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            //try
            //{
            //    IApplicationContext context = ContextRegistry.GetContext();
            //    IBReaBmsCenOrderDoc IBReaBmsCenOrderDoc = (IBReaBmsCenOrderDoc)context.GetObject("BReaBmsCenOrderDoc");
            //    tempBaseResultDataValue = IBReaBmsCenOrderDoc.ReaBmsCenOrderDocReviewAndPush((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction, Id);

            //}
            //catch (Exception ex)
            //{
            //    tempBaseResultDataValue.success = false;
            //    tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
            //    ZhiFang.Common.Log.Log.Debug("ReaBmsCenOrderDocReviewAndPush.ex:" + ex.ToString());
            //    //throw new Exception(ex.Message);
            //}
            return tempBaseResultDataValue;
        }
    }
}
