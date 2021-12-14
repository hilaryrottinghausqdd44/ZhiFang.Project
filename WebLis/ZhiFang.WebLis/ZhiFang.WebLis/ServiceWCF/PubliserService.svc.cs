using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ZhiFang.WebLis.ServerContract;
using System.ServiceModel.Activation;

namespace ZhiFang.WebLis.ServiceWCF
{

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class PubliserService : IPubliserService
    {
        public static List<IPublisherEvents> ClientCallbackList = new List<IPublisherEvents>();

        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool RegisterClient(string id)
        {

            try
            {
                IPublisherEvents client = OperationContext.Current.GetCallbackChannel<IPublisherEvents>();
                ZhiFang.Common.Log.Log.Debug("" + id + " 已注册.");
                //((IPublisherEvents)OperationContext.Current.Channel).SessionId = id;
                OperationContext.Current.Channel.Closing += new EventHandler(Channel_Closing);

                lock (ClientCallbackList)
                {
                    ClientCallbackList.Add(client);
                }
                return true;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(e.ToString());
                return false;
            }

        }
        void Channel_Closing(object sender, EventArgs e)
        {
            lock (ClientCallbackList)
            {
                ClientCallbackList.Remove((IPublisherEvents)sender);
            }
        }

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UnRegisterClinet(string id)
        {
            try
            {
                IPublisherEvents client = OperationContext.Current.GetCallbackChannel<IPublisherEvents>();
                ZhiFang.Common.Log.Log.Debug("" + id + " 已注销.");
                //((IPublisherEvents)OperationContext.Current.Channel).SessionId = id;
                OperationContext.Current.Channel.Closing += new EventHandler(Channel_Closing);
                lock (ClientCallbackList)
                {
                    ClientCallbackList.Remove(client);
                }
                return true;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(e.ToString());
                return false;
            }
        }
        
    }
}
