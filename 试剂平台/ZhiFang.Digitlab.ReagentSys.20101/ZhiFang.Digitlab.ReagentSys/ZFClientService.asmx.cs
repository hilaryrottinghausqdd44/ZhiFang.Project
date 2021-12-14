using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IBLL.ReagentSys;
using ZhiFang.Digitlab.IBLL.RBAC;
using Spring.Context;
using Spring.Context.Support;
using ZhiFang.Digitlab.ReagentSys.BusinessObject;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.ReagentSys;

namespace ZhiFang.Digitlab.ReagentSys
{
    /// <summary>
    /// ZFClientService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class ZFClientService : System.Web.Services.WebService
    {

        [WebMethod]
        public BaseResultDataValue RS_UDTO_AutoCheckOrderDoc(long orderDocId)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                string timeSec = ZhiFang.Common.Public.ConfigHelper.GetConfigString("AutoCheckOrderDocTime").Trim();
                if (string.IsNullOrEmpty(timeSec))
                    timeSec = "600";
                TimerCheckOrderDoc myTimer = new TimerCheckOrderDoc();
                myTimer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);
                myTimer.Interval = int.Parse(timeSec) * 1000;
                myTimer.OrderDocID = orderDocId;
                myTimer.Enabled = true;
                ZhiFang.Common.Log.Log.Info("------- 开始启动订单自动审核ID:" + myTimer.OrderDocID + " ----------------");
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        [WebMethod]
        public void  RS_UDTO_AutoCheckOrderDocPush(long orderDocId)
        {
            ZhiFang.Common.Log.Log.Info("------- 订单微信推送" + " ----------------");
            IApplicationContext context = ContextRegistry.GetContext();
            IBBmsCenOrderDoc IBBmsCenOrderDocWinXin = (IBBmsCenOrderDoc)context.GetObject("BBmsCenOrderDoc");
            BaseResultDataValue brdv = IBBmsCenOrderDocWinXin.BmsCenOrderDocCheckedAndPush((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction, orderDocId);
            string info = brdv.success ? "成功" : "失败";
            ZhiFang.Common.Log.Log.Info("------- 订单微信推送" + info + " ----------------");
        }

        private void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            ((TimerCheckOrderDoc)source).Enabled = false;
            IApplicationContext context = ContextRegistry.GetContext();
            IBBmsCenOrderDoc IBBmsCenOrderDoc = (IBBmsCenOrderDoc)context.GetObject("BBmsCenOrderDoc");
            ZhiFang.Common.Log.Log.Info("------- 开始执行订单自动审核 ----------------");
            try
            {
                long id = ((TimerCheckOrderDoc)source).OrderDocID;
                ZhiFang.Common.Log.Log.Info("------- 自动审核订单ID:" + id.ToString() + " ----------------");
                if (id > 0)
                {
                    BaseResultDataValue baseResultDataValue = IBBmsCenOrderDoc.EditAutoCheckBmsCenOrderDocByID(id);
                    if (baseResultDataValue.success)
                    {
                        ZhiFang.Common.Log.Log.Info("------- 自动订单审核成功后判断是否同步到第三方系统" + " ----------------");
                        ReagentService rs = new ReagentService();
                        rs.IBBmsCenOrderDoc = IBBmsCenOrderDoc;
                        rs.IBCenOrg = (IBCenOrg)context.GetObject("BCenOrg");
                        rs.IBCenOrgCondition = (IBCenOrgCondition)context.GetObject("BCenOrgCondition");
                        BaseResultDataValue brdv = rs.RS_UDTO_OrderSaveToOtherSystem(id);
                        string info = brdv.success ? "成功" : "失败：" + brdv.ErrorInfo;
                        ZhiFang.Common.Log.Log.Info("------- 订单同步到第三方系统" + info + " ----------------");
                        ServiceReference_ZFClient.ZFClientServiceSoapClient zfService = new ServiceReference_ZFClient.ZFClientServiceSoapClient();
                        zfService.RS_UDTO_AutoCheckOrderDocPush(id);
                    }
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Info("订单自动审核错误信息：" + ex.Message);
            }
            ZhiFang.Common.Log.Log.Info("------- 结束执行订单自动审核 ----------------");
        } 
    }
}
