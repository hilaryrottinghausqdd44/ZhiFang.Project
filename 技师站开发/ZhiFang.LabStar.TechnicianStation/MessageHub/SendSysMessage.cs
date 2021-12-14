using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZhiFang.Common.Public;

namespace ZhiFang.LabStar.TechnicianStation
{
    public class SendSysMessage
    {
        public static string SendSysMessageDelegate(object messageEntity, string messageType, string sectionID, IList<string> toEmpIDList)
        {
            try
            {
                string messageInfo = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(messageEntity);
                ZhiFang.LabStar.Common.LogHelp.Info("收到通讯结果消息：" + messageInfo);
                if (messageInfo == null || messageInfo.Trim().Length <= 0)
                {
                    return "消息体为空！";
                }

                if (toEmpIDList == null || toEmpIDList.Count <= 0)
                {
                    return "接收者ID为空！";
                }

                //查找普通用户和在线用户
                var OnlineUserList = MainMessageHub.ClientList.ClientStationList.Where(a => a.ClientStationTypeID == 1 && a.StatusTypeID == 1);
                foreach (string empID in toEmpIDList)
                {
                    if (OnlineUserList.Count(a => a.EmpID == empID) > 0)
                    {
                        ClientStationModel csm = OnlineUserList.Where(a => a.EmpID == empID).ElementAt(0);
                        IList<string> listConnectID = csm.ConnectionIdList;
                        foreach (var connectID in listConnectID)
                        {
                            MainMessageHub.clist.Client(connectID).ReceiveCommMessage(empID, csm.EmpName, messageInfo, messageType);
                            ZhiFang.Common.Log.Log.Debug("SendCommMessages.ConnectionId:" + connectID + ";ToEmpID：" + empID + ";ToEmpName:" + csm.EmpName + ";ToDeptName:" + csm.DeptName + ";Message:" + messageInfo);
                        }
                    }
                }

                return "发送完成！";
            }
            catch (Exception ex)
            {
                ZhiFang.LabStar.Common.LogHelp.Error("SendCommMessages.异常：" + ex.ToString());
                return "SendCommMessages.异常：" + ex.ToString();
            }

        }
    }
}