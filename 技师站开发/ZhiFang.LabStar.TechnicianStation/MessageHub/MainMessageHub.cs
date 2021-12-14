using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Spring.Context;
using Spring.Context.Support;
using ZhiFang.LabStar.Common;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.LabStar.TechnicianStation
{
    public class MainMessageHub : Hub
    {
        public static IHubCallerConnectionContext<dynamic> clist;

        public static SignaIRClientList ClientList = new SignaIRClientList();

        public override Task OnConnected()
        {
            AddClient();
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

        public string SendMessages(string[] toUserEmpIdList, string message, string formUserEmpID, string formUserEmpName)
        {
            try
            {
                if (toUserEmpIdList == null || toUserEmpIdList.Length <= 0)
                {
                    Clients.All.broadcastMessage(formUserEmpID, message);
                }
                else
                {
                    foreach (string toUserAccount in toUserEmpIdList)
                    {
                        var toUserList = ClientList.ClientStationList.Where(a => a.EmpID == toUserAccount);
                        if (toUserList != null && toUserList.Count() > 0)
                        {
                            foreach (var toUser in toUserList)
                            {
                                foreach (var cid in toUser.ConnectionIdList)
                                {
                                    Clients.Client(cid).ReceiveMessage(formUserEmpID, formUserEmpName, message);
                                }
                            }
                        }
                        else
                        {
                            ZhiFang.LabStar.Common.LogHelp.Error("MainMessageHub.SendMessages.touseraccount：已离线！");
                        }
                    }
                }
                return "发送完成！";
            }
            catch (Exception e)
            {
                ZhiFang.LabStar.Common.LogHelp.Error("MainMessageHub.SendMessages.异常：" + e.ToString());
                return "MainMessageHub.SendMessages.异常：" + e.ToString();
            }
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            try
            {
               if(ZhiFang.Common.Public.ConfigHelper.GetConfigString("SignaIRLogFlag")=="1")
                    ZhiFang.LabStar.Common.LogHelp.Info("MainMessageHub.OnDisconnected.Context.ConnectionId" + Context.ConnectionId + ",IP:" + IPHelper.GetClientIPByWebSocket(Context));

                for (int i = 0; i < ClientList.ClientStationList.Count; i++)
                {
                    if (ClientList.ClientStationList[i] == null)
                    {
                        ZhiFang.LabStar.Common.LogHelp.Info($"MainMessageHub.OnDisconnected.ClientList[{i}]为空！");
                        continue;
                    }
                    if (ClientList.ClientStationList[i].ConnectionIdList == null || ClientList.ClientStationList[i].ConnectionIdList.Count <= 0)
                    {
                        ZhiFang.LabStar.Common.LogHelp.Info("MainMessageHub.OnDisconnected.ClientList[i].ConnectionIdList,为空！i=" + i + ",EmpId" + ClientList.ClientStationList[i].EmpID + ",EmpName=" + ClientList.ClientStationList[i].EmpName + ",StationIP=" + ClientList.ClientStationList[i].StationIP);
                        continue;
                    }
                    for (int j = ClientList.ClientStationList[i].ConnectionIdList.Count - 1; j >= 0; j--)
                    {
                        if (ClientList.ClientStationList[i].ConnectionIdList[j] == Context.ConnectionId)
                        {
                            //ClientList.Remove(a);
                            ClientList.ClientStationList[i].ConnectionIdList.RemoveAt(j);
                            ZhiFang.LabStar.Common.LogHelp.Info("MainMessageHub.OnDisconnected.用户" + ClientList.ClientStationList[i].EmpName + "(" + ClientList.ClientStationList[i].EmpID + ")客户端连接ID：" + Context.ConnectionId + "断开！");
                        }
                        if (ClientList.ClientStationList[i].ConnectionIdList == null || ClientList.ClientStationList[i].ConnectionIdList.Count == 0)
                        {
                            ClientList.ClientStationList[i].StatusTypeID = 0;
                            ZhiFang.LabStar.Common.LogHelp.Info("MainMessageHub.OnDisconnected.用户" + ClientList.ClientStationList[i].EmpName + "(" + ClientList.ClientStationList[i].EmpID + ")退出！");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ZhiFang.LabStar.Common.LogHelp.Error("MainMessageHub.OnDisconnected.异常：" + e.ToString());
            }
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            ZhiFang.LabStar.Common.LogHelp.Info($"MainMessageHub.OnReconnected.Context.ConnectionId{Context.ConnectionId}, IP: " + IPHelper.GetClientIPByWebSocket(Context));
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
                                a.StatusTypeID = 1;
                                ZhiFang.LabStar.Common.LogHelp.Info("用户" + a.EmpName + "(" + a.EmpID + ")客户端连接ID：" + c + "重新连接！");
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
                ZhiFang.LabStar.Common.LogHelp.Error("MainMessageHub.OnReconnected.异常：" + e.ToString());
            }
            return base.OnReconnected();
        }

        public static void CloseALL()
        {
            IHubContext _hubContext = GlobalHost.ConnectionManager.GetHubContext<MainMessageHub>();
            _hubContext.Clients.All.ForceRestart();
        }

        //新增在线用户
        private void AddClient()
        {
            string empID = Context.Request.Cookies[DicCookieSession.EmployeeID].Value;
            string empName = Context.Request.Cookies[DicCookieSession.EmployeeName].Value;
            long longEmpID= 0;
            if (!long.TryParse(empID, out longEmpID) || longEmpID <= 0)
            {
                ZhiFang.LabStar.Common.LogHelp.Error("用户未登录！无法建立连接！EmpId:" + (string.IsNullOrEmpty(empID) ? "" : empID) + ",IP:" + IPHelper.GetClientIPByWebSocket(Context));
                throw new Exception("用户未登录！无法建立连接！");
            }
            if (ClientList.ClientStationList == null)
            {
                ClientList.ClientStationList = new List<ClientStationModel>();
            }
            if (ClientList.ClientStationList.Count(p => p.EmpID == empID) <= 0)
            {
                ClientStationModel tmpcs = new ClientStationModel();
                tmpcs.EmpName = empName;
                tmpcs.EmpID = empID;
                tmpcs.StationIP = IPHelper.GetClientIPByWebSocket(Context);
                tmpcs.ClientStationTypeID = Context.Request.Cookies.Keys.Contains("ClientTypeFlag") ? long.Parse(Context.Request.Cookies["ClientStationStationTypeId"].Value.ToString()) : 1;
                tmpcs.StationName = Context.Request.Cookies.Keys.Contains("StationName") ? Context.Request.Cookies["ClientStationStationTypeId"].Value.ToString() : "";
                tmpcs.DeptID = Context.Request.Cookies[DicCookieSession.HRDeptID].Value;
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
                tmpcs.StatusTypeID = 1;

                ClientList.ClientStationList.Add(tmpcs);
                clist = Clients;

                ZhiFang.LabStar.Common.LogHelp.Debug("用户 :" + tmpcs.EmpName + "(" + tmpcs.EmpID + ") 上线,IP:" + tmpcs.StationIP + ",站点:" + tmpcs.StationName + ",所属部门:" + tmpcs.DeptName + "(" + tmpcs.EmpID + "),Context.ConnectionId:" + Context.ConnectionId);
            }
            else
            {
                var cs = ClientList.ClientStationList.Where(a => a.EmpID == empID).ElementAt(0);
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
                cs.StatusTypeID = 1;

                ZhiFang.LabStar.Common.LogHelp.Debug("用户 :" + cs.EmpName + "(" + cs.EmpID + ") 上线,IP:" + cs.StationIP + ",站点:" + cs.StationName + ",所属部门:" + cs.DeptName + "(" + cs.EmpID+ ")");
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
        public string EmpID { get; set; }
        //员工姓名
        public string EmpName { get; set; }
        //所属部门ID
        public string DeptID { get; set; }
        //所属部门名称
        public string DeptName { get; set; }
        //危急值管理部门列表
        public Dictionary<long, string> CriticalValueDeptList { get; set; }
        //站点IP
        public string StationIP { get; set; }
        //站点名称
        public string StationName { get; set; }
        //类型
        public long ClientStationTypeID { get; set; }//1普通用户，2仪器，3站点
        //登录时间
        public DateTime LoginTime { get; set; }
        //退出时间
        public DateTime LogoutTime { get; set; }

        /// <summary>
        /// 0未连接，1已连接。
        /// </summary>
        public int StatusTypeID { get; set; }
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