using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;
using ZhiFang.WebLis.ServerContract;

namespace ZhiFang.WebLis.ServiceWCF
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“WCFSubscriptionService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 WCFSubscriptionService.svc 或 WCFSubscriptionService.svc.cs，然后开始调试。
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class WCFSubscriptionService : IWCFSubscriptionService
    {
        #region ISubscriptionService 成员

        public bool RegisterClient()
        {
            try
            {
                var client = OperationContext.Current.GetCallbackChannel<IPushClient>();
                var id = OperationContext.Current.SessionId;
                //ZhiFang.Common.Log.Log.Debug("" + id + " 已注册.");
                //((IPushClient)OperationContext.Current.Channel).SessionId = id;
                // OperationContext.Current.Channel.Closing += new EventHandler(Channel_Closing);
                //ClientCallbackList.Add(client);
                return true;
            }
            catch (Exception e)
            {
                //ZhiFang.Common.Log.Log.Debug(e.ToString());
                return false;
            }
        }
        public bool RegisterClient(string Account, string Password)
        {
            try
            {
                var client = OperationContext.Current.GetCallbackChannel<IPushClient>();
                var id = OperationContext.Current.SessionId;
                //ZhiFang.Common.Log.Log.Debug("" + id + " 已注册." + "账户名：" + Account);
                //((IPushClient)OperationContext.Current.Channel).SessionId = id;
                // OperationContext.Current.Channel.Closing += new EventHandler(Channel_Closing);
                //ClientCallbackList.Add(client);
                return true;
            }
            catch (Exception e)
            {
                //ZhiFang.Common.Log.Log.Debug(e.ToString());
                return false;
            }
        }
        #endregion
    }
}
