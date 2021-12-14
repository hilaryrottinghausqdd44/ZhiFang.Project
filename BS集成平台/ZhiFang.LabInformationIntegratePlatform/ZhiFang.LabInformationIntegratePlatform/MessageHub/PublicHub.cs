using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.LabInformationIntegratePlatform.MessageHub
{
    public class PublicHub : Hub
    {
        public static List<ClientModel> ClientList = new List<ClientModel>();
        public void Hello()
        {
            Clients.All.hello();
        }
        public override Task OnConnected()
        {

            clist = Clients;
            ClientModel client = new ClientModel
            {
                ClientName = Context.Request.Cookies.ContainsKey(DicCookieSession.UserAccount) ? Context.RequestCookies[DicCookieSession.UserAccount].Value.ToString() : Guid.NewGuid().ToString(),//获取远程计算机名
                ClientIP = Context.Request.Cookies.ContainsKey("UserIP") ? Context.RequestCookies["UserIP"].Value.ToString() : Guid.NewGuid().ToString(),
                ClientTypeFlag = Context.Request.Cookies.ContainsKey("ClientTypeFlag") ? int.Parse(Context.RequestCookies["ClientTypeFlag"].Value.ToString()) : 0,
                ConnectionId = Context.ConnectionId,
                LoginTime = DateTime.Now
            };
            foreach (ClientModel m in ClientList)
            {
                if (m.ClientName == client.ClientName)
                {
                    ClientList.Remove(m);
                    break;
                }
            }
            ClientList.Add(client);
            string output = string.Format("PublicHub.OnConnected.用户 {0} 上线,IP:{1}", client.ClientName, client.ClientIP);
            ZhiFang.Common.Log.Log.Debug(output);
            return base.OnConnected();
        }

        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(name, message);
            //foreach (var a in Clients)
            //{

            //}

        }
        public string SendMessages(string[] ToUserAccountList, string message, string FormUserAccount)
        {
            try
            {
                if (ToUserAccountList == null || ToUserAccountList.Length <= 0)
                {
                    Clients.All.broadcastMessage(FormUserAccount, message);
                }
                else
                {
                    foreach (string touseraccount in ToUserAccountList)
                    {
                        var touserlist = ClientList.Where(a => a.ClientName == touseraccount);
                        if (touserlist != null && touserlist.Count() > 0)
                        {
                            foreach (var touser in touserlist)
                            {
                                Clients.Client(touser.ConnectionId).ReceiveMessage(FormUserAccount, message);
                            }
                        }
                        else
                        {
                            ZhiFang.Common.Log.Log.Error("PublicHub.SendMessages.touseraccount：已离线！");
                        }
                    }
                }
                return "发送完成！";
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("PublicHub.SendMessages.异常：" + e.ToString());
                return "PublicHub.SendMessages.异常：" + e.ToString();
            }
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            try
            {
                foreach (var a in ClientList)
                {
                    if (a.ConnectionId == Context.ConnectionId)
                    {
                        ClientList.Remove(a);
                    }
                }

            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("OnDisconnected.异常：" + e.ToString());
            }
            return base.OnDisconnected(stopCalled);
        }
        public static IHubCallerConnectionContext<dynamic> clist;
    }

    public class ClientModel
    {
        public string ClientIP { get; set; }
        public string ClientName { get; set; }
        public string ConnectionId { get; set; }
        public int ClientTypeFlag { get; set; }//0普通用户，1仪器，2站点
        [JsonConverter(typeof(JsonConvertClass))]
        public DateTime LoginTime { get; set; }
        [JsonConverter(typeof(JsonConvertClass))]
        public DateTime LogOutTime { get; set; }
    }
}