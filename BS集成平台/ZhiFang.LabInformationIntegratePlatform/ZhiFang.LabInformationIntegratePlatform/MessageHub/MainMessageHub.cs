using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Spring.Context;
using Spring.Context.Support;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.LabInformationIntegratePlatform.MessageHub
{
    public class MainMessageHub : Hub
    {
        public static IHubCallerConnectionContext<dynamic> clist;
        //public static List<ClientStationModel> ClientList = new List<ClientStationModel>();

        public static SignaIRClientList ClientList = new SignaIRClientList();

        public override Task OnConnected()
        {
            AddClient();
            //HttpContext.Current.Application["ClientList"] = ClientList;
            return base.OnConnected();
        }
        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            //Clients.All.broadcastMessage(name, message);
            //foreach (var a in Clients)
            //{

            //}

        }
        public string SendMessages(string[] ToUserEmpIdList, string Message, string FormUserEmpId, string FormUserEmpName)
        {
            try
            {
                if (ToUserEmpIdList == null || ToUserEmpIdList.Length <= 0)
                {
                    Clients.All.broadcastMessage(FormUserEmpId, Message);
                }
                else
                {
                    foreach (string touseraccount in ToUserEmpIdList)
                    {
                        var touserlist = ClientList.ClientStationList.Where(a => a.EmpId == touseraccount);
                        if (touserlist != null && touserlist.Count() > 0)
                        {
                            foreach (var touser in touserlist)
                            {
                                //Clients.Client(touser.ConnectionId).ReceiveMessage(FormUserEmpId, FormUserEmpName, Message);
                                foreach (var cid in touser.ConnectionIdList)
                                {
                                    Clients.Client(cid).ReceiveMessage(FormUserEmpId, FormUserEmpName, Message);
                                }
                            }
                        }
                        else
                        {
                            ZhiFang.Common.Log.Log.Error("MainMessageHub.SendMessages.touseraccount：已离线！");
                        }
                    }
                }
                return "发送完成！";
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("MainMessageHub.SendMessages.异常：" + e.ToString());
                return "MainMessageHub.SendMessages.异常：" + e.ToString();
            }
        }
        public string SendMessages_CV(string[] ToUserEmpIdList, string Message, string FormUserEmpId, string FormUserEmpName, string SCMsgId, string SCMsgTypeCode)
        {
            try
            {

                if (ToUserEmpIdList == null || ToUserEmpIdList.Length <= 0)
                {
                    Clients.All.broadcastMessage(FormUserEmpId, Message);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug("MainMessageHub.SendMessages_CV.ToUserEmpIdList：" + string.Join(",", ToUserEmpIdList) + ";Message:" + Message + ";FormUserEmpId:" + FormUserEmpId);
                    foreach (string EmpId in ToUserEmpIdList)
                    {
                        var touserlist = ClientList.ClientStationList.Where(a => a.EmpId == EmpId);
                        if (touserlist != null && touserlist.Count() > 0)
                        {
                            foreach (var touser in touserlist)
                            {
                                //Clients.Client(touser.ConnectionId).ReceiveMessageByEmpId_CV(FormUserEmpId, FormUserEmpName, Message, SCMsgId, SCMsgTypeCode);
                                //Clients.Client(touser.ConnectionId).ReceiveMessage(FormUserEmpId, FormUserEmpName, Message);
                                foreach (var cid in touser.ConnectionIdList)
                                {
                                    Clients.Client(cid).ReceiveMessageByEmpId_CV(FormUserEmpId, FormUserEmpName, Message, SCMsgId, SCMsgTypeCode);
                                }
                            }
                        }
                        else
                        {
                            ZhiFang.Common.Log.Log.Error("MainMessageHub.SendMessages_CV.EmpID：" + EmpId + "已离线！");
                        }
                    }
                }
                return "发送完成！";
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("MainMessageHub.SendMessages_CV.异常：" + e.ToString());
                return "MainMessageHub.SendMessages_CV.异常：" + e.ToString();
            }
        }
        public string SendMessagesByDeptId_CV(string ToDeptId, string Message, string FormUserEmpId, string SCMsgId, string SCMsgTypeCode)
        {
            try
            {
                if (ToDeptId == null || ToDeptId.Trim().Length <= 0)
                {
                    return "部门ID为空！";
                }

                if (Message == null || Message.Trim().Length <= 0)
                {
                    return "消息体为空！";
                }

                if (FormUserEmpId == null || FormUserEmpId.Trim().Length <= 0)
                {
                    return "部门ID为空！";
                }
                string FormUserEmpName = "";
                if (ClientList.ClientStationList.Count(a => a.EmpId.ToString() == FormUserEmpId) > 0)
                {
                    FormUserEmpName = ClientList.ClientStationList.Where(a => a.EmpId.ToString() == FormUserEmpId).ElementAt(0).EmpName;
                }
                ZhiFang.Common.Log.Log.Debug("MainMessageHub.SendMessagesByDeptId_CV.ToDeptId：" + string.Join(",", ToDeptId) + ";Message:" + Message + ";FormUserEmpId:" + FormUserEmpId);
                IApplicationContext context = ContextRegistry.GetContext();
                IBLL.LIIP.IBCVCriticalValueEmpIdDeptLink IBCVCriticalValueEmpIdDeptLink = (IBLL.LIIP.IBCVCriticalValueEmpIdDeptLink)context.GetObject("BCVCriticalValueEmpIdDeptLink");
                var tmplist = IBCVCriticalValueEmpIdDeptLink.SearchListByHQL(" DeptId= " + ToDeptId);
                if (tmplist != null && tmplist.Count() > 0)
                {
                    for (int i = 0; i < tmplist.Count(); i++)
                    {
                        if (ClientList.ClientStationList.Count(a => a.EmpId == tmplist[i].EmpID.ToString() && a.StatusTypeId == 1) > 0)
                        {
                            List<string> cidlist = ClientList.ClientStationList.Where(a => a.EmpId == tmplist[i].EmpID.ToString()).ElementAt(0).ConnectionIdList;
                            foreach (var c in cidlist)
                            {
                                Clients.Client(c).ReceiveMessageByEmpId_CV(FormUserEmpId, FormUserEmpName, Message, SCMsgId, SCMsgTypeCode);
                                ZhiFang.Common.Log.Log.Debug("MainMessageHub.SendMessagesByDeptId_CV.ConnectionId:" + c + ";FormUserEmpId：" + FormUserEmpId + ";FormUserEmpName:" + FormUserEmpName + ";Message:" + Message + ";SCMsgId:" + SCMsgId + ";SCMsgTypeCode:" + SCMsgTypeCode);
                            }
                        }
                        else
                        {
                            ZhiFang.Common.Log.Log.Error("MainMessageHub.SendMessagesByDeptId_CV.部门ID: " + ToDeptId + "的科室的用户:EmpId=" + tmplist[i].EmpID.ToString() + "不在线！");
                        }
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("MainMessageHub.SendMessagesByDeptId_CV.部门ID: " + ToDeptId + "的科室没有可以通知的用户！");
                    return "部门ID:" + ToDeptId + "的科室没有可以通知的用户！";
                }

                return "发送完成！";
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("MainMessageHub.SendMessagesByDeptId_CV.异常：" + e.ToString());
                return "MainMessageHub.SendMessagesByDeptId_CV.异常：" + e.ToString();
            }
        }

        //public static string SendMessagesByDeptId_CV_Static(string ToDeptId, string Message, string FormUserEmpId)
        //{
        //    try
        //    {
        //        if (ToDeptId == null || ToDeptId.Trim().Length <= 0)
        //        {
        //            return "部门ID为空！";
        //        }

        //        if (Message == null || Message.Trim().Length <= 0)
        //        {
        //            return "消息体为空！";
        //        }

        //        if (FormUserEmpId == null || FormUserEmpId.Trim().Length <= 0)
        //        {
        //            return "部门ID为空！";
        //        }
        //        string FormUserEmpName = "";
        //        if (ClientList.Count(a => a.EmpId.ToString() == FormUserEmpId) > 0)
        //        {
        //            FormUserEmpName = ClientList.Where(a => a.EmpId.ToString() == FormUserEmpId).ElementAt(0).EmpName;
        //        }
        //        ZhiFang.Common.Log.Log.Debug("MainMessageHub.SendMessagesByDeptId_CV.ToDeptId：" + string.Join(",", ToDeptId) + ";Message:" + Message + ";FormUserEmpId:" + FormUserEmpId);
        //        IApplicationContext context = ContextRegistry.GetContext();
        //        IBLL.LIIP.IBCVCriticalValueEmpIdDeptLink IBCVCriticalValueEmpIdDeptLink = (IBLL.LIIP.IBCVCriticalValueEmpIdDeptLink)context.GetObject("BCVCriticalValueEmpIdDeptLink");
        //        var tmplist = IBCVCriticalValueEmpIdDeptLink.SearchListByHQL(" DeptId= " + ToDeptId);
        //        if (tmplist != null && tmplist.Count() > 0)
        //        {
        //            for (int i = 0; i < tmplist.Count(); i++)
        //            {
        //                if (ClientList.Count(a => a.EmpId == tmplist[i].EmpID.ToString()) > 0)
        //                {
        //                    clist.Clie
        //                    //Clients.Client(ClientList.Where(a => a.EmpId == tmplist[i].EmpID.ToString()).ElementAt(0).ConnectionId).ReceiveMessageByEmpId_CV(FormUserEmpId, FormUserEmpName, Message);
        //                    ZhiFang.Common.Log.Log.Debug("MainMessageHub.SendMessagesByDeptId_CV.ConnectionId:" + ClientList.Where(a => a.EmpId == tmplist[i].EmpID.ToString()).ElementAt(0).ConnectionId + ";FormUserEmpId：" + FormUserEmpId + ";FormUserEmpName:" + FormUserEmpName + ";Message:" + Message);
        //                }
        //                else
        //                {
        //                    ZhiFang.Common.Log.Log.Error("MainMessageHub.SendMessagesByDeptId_CV.部门ID: " + ToDeptId + "的科室的用户:EmpId=" + tmplist[i].EmpID.ToString() + "不在线！");
        //                }
        //            }
        //        }
        //        else
        //        {
        //            ZhiFang.Common.Log.Log.Error("MainMessageHub.SendMessagesByDeptId_CV.部门ID: " + ToDeptId + "的科室没有可以通知的用户！");
        //            return "部门ID:" + ToDeptId + "的科室没有可以通知的用户！";
        //        }

        //        return "发送完成！";
        //    }
        //    catch (Exception e)
        //    {
        //        ZhiFang.Common.Log.Log.Error("MainMessageHub.SendMessagesByDeptId_CV.异常：" + e.ToString());
        //        return "MainMessageHub.SendMessagesByDeptId_CV.异常：" + e.ToString();
        //    }
        //}
        public override Task OnDisconnected(bool stopCalled)
        {
            try
            {
               if(ZhiFang.Common.Public.ConfigHelper.GetConfigString("SignaIRLogFlag")=="1")
                    ZhiFang.Common.Log.Log.Info("MainMessageHub.OnDisconnected.Context.ConnectionId" + Context.ConnectionId + ",IP:" + BusinessObject.Utils.IPHelper.GetClientIPByWebSocket(Context));

                for (int i = 0; i < ClientList.ClientStationList.Count; i++)
                {
                    if (ClientList.ClientStationList[i] == null)
                    {
                        ZhiFang.Common.Log.Log.Info($"MainMessageHub.OnDisconnected.ClientList[{i}]为空！");
                        continue;
                    }
                    if (ClientList.ClientStationList[i].ConnectionIdList == null || ClientList.ClientStationList[i].ConnectionIdList.Count <= 0)
                    {
                        ZhiFang.Common.Log.Log.Info("MainMessageHub.OnDisconnected.ClientList[i].ConnectionIdList,为空！i=" + i + ",EmpId" + ClientList.ClientStationList[i].EmpId + ",EmpName=" + ClientList.ClientStationList[i].EmpName + ",StationIP=" + ClientList.ClientStationList[i].StationIP);
                        continue;
                    }
                    for (int j = ClientList.ClientStationList[i].ConnectionIdList.Count - 1; j >= 0; j--)
                    {
                        if (ClientList.ClientStationList[i].ConnectionIdList[j] == Context.ConnectionId)
                        {
                            //ClientList.Remove(a);
                            ClientList.ClientStationList[i].ConnectionIdList.RemoveAt(j);
                            ZhiFang.Common.Log.Log.Info("MainMessageHub.OnDisconnected.用户" + ClientList.ClientStationList[i].EmpName + "(" + ClientList.ClientStationList[i].EmpId + ")客户端连接ID：" + Context.ConnectionId + "断开！");
                        }
                        if (ClientList.ClientStationList[i].ConnectionIdList == null || ClientList.ClientStationList[i].ConnectionIdList.Count == 0)
                        {
                            ClientList.ClientStationList[i].StatusTypeId = 0;
                            ZhiFang.Common.Log.Log.Info("MainMessageHub.OnDisconnected.用户" + ClientList.ClientStationList[i].EmpName + "(" + ClientList.ClientStationList[i].EmpId + ")退出！");
                        }
                    }
                }

                //foreach (var a in ClientList)
                //{
                //    foreach (var c in a.ConnectionIdList)
                //    {
                //        if (c == Context.ConnectionId)
                //        {
                //            //ClientList.Remove(a);
                //            a.ConnectionIdList.Remove(c);
                //            ZhiFang.Common.Log.Log.Info("MainMessageHub.OnDisconnected.用户" + a.EmpName + "(" + a.EmpId + ")客户端连接ID："+c +"断开！");
                //        }
                //        if (a.ConnectionIdList == null || a.ConnectionIdList.Count == 0)
                //        {
                //            a.StatusTypeId = 0;
                //            ZhiFang.Common.Log.Log.Info("MainMessageHub.OnDisconnected.用户" + a.EmpName + "(" + a.EmpId + ")退出！");
                //        }
                //    } 
                //}
                //HttpContext.Current.Application["ClientList"] = ClientList;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("MainMessageHub.OnDisconnected.异常：" + e.ToString());
            }
            return base.OnDisconnected(stopCalled);
        }
        public override Task OnReconnected()
        {
            ZhiFang.Common.Log.Log.Info($"MainMessageHub.OnReconnected.Context.ConnectionId{Context.ConnectionId}, IP: " + BusinessObject.Utils.IPHelper.GetClientIPByWebSocket(Context));
            try
            {
                if (ClientList != null && ClientList.ClientStationList != null && ClientList.ClientStationList.Count > 0)
                {
                    foreach (var a in ClientList.ClientStationList)
                    {
                        foreach (var c in a.ConnectionIdList)
                        {
                            if (c == Context.ConnectionId)
                            {
                                //ClientList.Remove(a);
                                a.StatusTypeId = 1;
                                ZhiFang.Common.Log.Log.Info("MainMessageHub.OnReconnected.用户" + a.EmpName + "(" + a.EmpId + ")客户端连接ID：" + c + "重新连接！");
                            }
                            else
                            {
                                AddClient();
                            }
                        }
                    }
                }
                else
                {
                    AddClient();
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("MainMessageHub.OnReconnected.异常：" + e.ToString());
            }
            return base.OnReconnected();
        }
        public static void CloseALL()
        {
            IHubContext _hubContext = GlobalHost.ConnectionManager.GetHubContext<MainMessageHub>();
            _hubContext.Clients.All.ForceRestart();
        }
        private void AddClient()
        {
            //ZhiFang.Common.Log.Log.Debug("HttpContext.Current.Request.Cookies.AllKeys:" + string.Join(",", Context.Request.Cookies.Keys));
            //ZhiFang.Common.Log.Log.Debug("HttpContext.Current.Request.Cookies.AllKeys:" + string.Join(",", HttpContext.Current.Request.Cookies.AllKeys));
            //string EmpId = ZhiFang.Common.Public.Cookie.CookieHelper.Read(DicCookieSession.EmployeeID);
            //string EmpName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(DicCookieSession.EmployeeName);
            string EmpId = Context.Request.Cookies[DicCookieSession.EmployeeID].Value;
            string EmpName = Context.Request.Cookies[DicCookieSession.EmployeeName].Value;
            try
            {               
                if (!long.TryParse(EmpId, out var empid) || empid <= 0)
                {
                    ZhiFang.Common.Log.Log.Error("MainMessageHub.OnConnected：未登录！无法建立连接！EmpId:" + (string.IsNullOrEmpty(EmpId) ? "" : EmpId) + ",IP:" + BusinessObject.Utils.IPHelper.GetClientIPByWebSocket(Context));
                    throw new Exception("未登录！无法建立连接！");
                }
                if (ClientList.ClientStationList == null)
                {
                    ClientList.ClientStationList = new List<ClientStationModel>();
                }
                if (ClientList.ClientStationList.Count(a => a.EmpId == EmpId) <= 0)
                {
                    ClientStationModel tmpcs = new ClientStationModel();
                    tmpcs.EmpName = EmpName;
                    tmpcs.EmpId = EmpId;
                    //tmpcs.StationIP = BusinessObject.Utils.IPHelper.GetClientIP();                 
                    //tmpcs.ClientStationTypeId = HttpContext.Current.Request.Cookies.AllKeys.Contains("ClientTypeFlag") ? long.Parse(HttpContext.Current.Request.Cookies["ClientStationStationTypeId"].Value.ToString()) : 1;
                    //tmpcs.StationName = HttpContext.Current.Request.Cookies.AllKeys.Contains("StationName") ? HttpContext.Current.Request.Cookies["ClientStationStationTypeId"].Value.ToString() : "";
                    //tmpcs.DeptId = ZhiFang.Common.Public.Cookie.CookieHelper.Read(DicCookieSession.HRDeptID);
                    //tmpcs.DeptName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(DicCookieSession.HRDeptName);

                    tmpcs.StationIP = BusinessObject.Utils.IPHelper.GetClientIPByWebSocket(Context);
                    tmpcs.ClientStationTypeId = Context.Request.Cookies.Keys.Contains("ClientTypeFlag") ? long.Parse(Context.Request.Cookies["ClientStationStationTypeId"].Value.ToString()) : 1;
                    tmpcs.StationName = Context.Request.Cookies.Keys.Contains("StationName") ? Context.Request.Cookies["ClientStationStationTypeId"].Value.ToString() : "";
                    tmpcs.DeptId = Context.Request.Cookies[DicCookieSession.HRDeptID].Value;
                    tmpcs.DeptName = Context.Request.Cookies[DicCookieSession.HRDeptName].Value;

                    if (tmpcs.ConnectionIdList == null || tmpcs.ConnectionIdList.Count <= 0)
                    {
                        tmpcs.ConnectionIdList = new List<string>() { Context.ConnectionId };
                    }
                    else
                    {
                        if (!tmpcs.ConnectionIdList.Contains(Context.ConnectionId))
                        {
                            tmpcs.ConnectionIdList.Add(Context.ConnectionId);
                        }
                    }
                    tmpcs.LoginTime = DateTime.Now;
                    tmpcs.StatusTypeId = 1;

                    ClientList.ClientStationList.Add(tmpcs);
                    clist = Clients;
                    //ClientList.clist = Clients;

                    // string aaa=ZhiFang.Common.Public.
                    ZhiFang.Common.Log.Log.Debug("MainMessageHub.OnConnected.用户 :" + tmpcs.EmpName + "(" + tmpcs.EmpId + ") 上线,IP:" + tmpcs.StationIP + ",站点:" + tmpcs.StationName + ",所属部门:" + tmpcs.DeptName + "(" + tmpcs.EmpId + "),Context.ConnectionId:" + Context.ConnectionId);
                }
                else
                {
                    var cs = ClientList.ClientStationList.Where(a => a.EmpId == EmpId).ElementAt(0);
                    if (cs.ConnectionIdList == null || cs.ConnectionIdList.Count <= 0)
                    {
                        cs.ConnectionIdList = new List<string>() { Context.ConnectionId };
                    }
                    else
                    {
                        if (!cs.ConnectionIdList.Contains(Context.ConnectionId))
                        {
                            cs.ConnectionIdList.Add(Context.ConnectionId);
                        }
                    }
                    cs.StatusTypeId = 1;

                    ZhiFang.Common.Log.Log.Debug("MainMessageHub.OnConnected.用户 :" + cs.EmpName + "(" + cs.EmpId + ") 上线,IP:" + cs.StationIP + ",站点:" + cs.StationName + ",所属部门:" + cs.DeptName + "(" + cs.EmpId + ")");
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("MainMessageHub.OnConnected.AddClient 异常 :" +e.ToString());
            }
        }
    }
    public class ClientStationModel
    {
        /// <summary>
        /// 暂时用字符串，是否要记录每个链接的IP地址？
        /// </summary>
        public List<string> ConnectionIdList { get; set; }

        //员工ID
        public string EmpId { get; set; }
        //员工姓名
        public string EmpName { get; set; }
        //所属部门ID
        public string DeptId { get; set; }
        //所属部门名称
        public string DeptName { get; set; }
        //危急值管理部门列表
        public Dictionary<long, string> CriticalValueDeptList { get; set; }
        //站点IP
        public string StationIP { get; set; }
        //站点名称
        public string StationName { get; set; }
        //类型
        public long ClientStationTypeId { get; set; }//1普通用户，2仪器，3站点
        //登录时间
        public DateTime LoginTime { get; set; }
        //退出时间
        public DateTime LogOutTime { get; set; }

        /// <summary>
        /// 0未连接，1已连接。
        /// </summary>
        public int StatusTypeId { get; set; }
    }

    public class SignaIRClientList
    {
        private List<ClientStationModel> _ClientList = new List<ClientStationModel>();
        public List<ClientStationModel> ClientStationList
        {
            get
            {

                return _ClientList;
            }
            set
            {
                _ClientList = value;
            }
        }
        public IHubCallerConnectionContext<dynamic> clist { get; set; }
        public DateTime DataTimeStamp { get; set; }
    }
}