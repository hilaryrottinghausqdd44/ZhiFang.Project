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
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class TestService : ZhiFang.WebLis.ServerContract.ITestService
    {
       public bool RegisterClient()
        {
            try
            {
                var client = OperationContext.Current.GetCallbackChannel<IPushClient>();
                var id = OperationContext.Current.SessionId;
                //ZhiFang.Common.Log.Log.Debug("" + id + " 已注册.");
                //((IPushClient)OperationContext.Current.Channel).SessionId = id;
                //OperationContext.Current.Channel.Closing += new EventHandler(Channel_Closing);
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
        /*
         #region ISubscriptionService 成员

        public bool RegisterClient()
        {
            try
            {
                var client = OperationContext.Current.GetCallbackChannel<IPushClient>();
                var id = OperationContext.Current.SessionId;
                ZhiFang.Common.Log.Log.Debug("" + id + " 已注册.");
                //((IPushClient)OperationContext.Current.Channel).SessionId = id;
                OperationContext.Current.Channel.Closing += new EventHandler(Channel_Closing);
                ClientCallbackList.Add(client);
                return true;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(e.ToString());
                return false;
            }
        }
        public bool RegisterClient(string Account,string Password)
        {
            try
            {
                var client = OperationContext.Current.GetCallbackChannel<IPushClient>();
                var id = OperationContext.Current.SessionId;
                ZhiFang.Common.Log.Log.Debug("" + id + " 已注册."+"账户名："+Account);
                //((IPushClient)OperationContext.Current.Channel).SessionId = id;
                OperationContext.Current.Channel.Closing += new EventHandler(Channel_Closing);
                ClientCallbackList.Add(client);
                return true;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(e.ToString());
                return false;
            }
        }
        #endregion


        //public static Dictionary<string,IPushClient> ClientCallbackList { get; set; }

        public static List<IPushClient> ClientCallbackList { get; set; }

        public SubscriptionService()
        {
            ClientCallbackList = new List<IPushClient>();
        }

        void Channel_Closing(object sender, EventArgs e)
        {
            lock (ClientCallbackList)
            {
                ClientCallbackList.Remove((IPushClient)sender);
                //ZhiFang.Common.Log.Log.Debug("" + ((IPushClient)sender).SessionId + " 已注销.");
            }
        }

        public void Dispose()
        {
            ClientCallbackList.Clear();
        }

        public void SendMessageService(string message)
        {
            if (message != null && message.Trim().Length > 0)
            {
                var list = ClientCallbackList;
                if (list == null || list.Count == 0)
                    return;
                lock (list)
                {
                    foreach (var client in list)
                    {
                        // Broadcast
                        client.SendMessage(message);
                    }
                }

            }
        }
        public static void SendMessageCenter(string message)
        {
            if (message != null && message.Trim().Length > 0)
            {
                var list = ClientCallbackList;
                if (list == null || list.Count == 0)
                    return;
                lock (list)
                {
                    foreach (var client in list)
                    {
                        client.SendMessage(message);
                    }
                }
            }
        }
        public static void SendMessageCenter<T>(T a,string message,long ModuleId)
        {
            if (message != null && message.Trim().Length > 0)
            {
                var list = ClientCallbackList;
                if (list == null || list.Count == 0)
                    return;
                if (typeof(T).Name == "")
                {
 
                }

                lock (list)
                {
                    foreach (var client in list)
                    {
                        // Broadcast
                        client.SendMessage(message);
                    }
                }
            }
        }
         */
    }
}
