using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace ZhiFang.LabStar.TechnicianStation.ServerWCF
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class TestService : ITestService
    {
        public void DoWork()
        {

        }

        public string SendCommMessages(string MessageInfo, string MessageType, string FormUserEmpId, string FormUserEmpName)
        {
            try
            {
                if (MessageInfo == null || MessageInfo.Trim().Length <= 0)
                {
                    return "消息体为空！";
                }

                if (FormUserEmpId == null || FormUserEmpId.Trim().Length <= 0)
                {
                    return "发送者ID为空！";
                }

                //if (FormUserEmpName == null || FormUserEmpName.Trim().Length <= 0)
                //{
                //    return "发送者名称为空！";
                //}



                var OnlineUserList = MainMessageHub.ClientList.ClientStationList.Where(a => a.ClientStationTypeID == 1 && a.StatusTypeID == 1);
                //ZhiFang.Common.Log.Log.Debug("IMService.SendMessagesByDeptId_OTTH.在线人员列表：" + ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(OnlineUserList));
                if (OnlineUserList.Count(a => a.EmpID == FormUserEmpId) > 0)
                {
                    List<string> cidlist = OnlineUserList.Where(a => a.EmpID == FormUserEmpId).ElementAt(0).ConnectionIdList;
                    foreach (var c in cidlist)
                    {
                        MainMessageHub.clist.Client(c).ReceiveCommMessage(FormUserEmpId, FormUserEmpName, MessageInfo, MessageType);
                        ZhiFang.Common.Log.Log.Debug("SendCommMessages.ConnectionId:" + c + ";FormUserEmpId：" + FormUserEmpId + ";FormUserEmpName:" + FormUserEmpName + ";Message:" + MessageInfo);
                    }
                }

                return "发送完成！";
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("SendCommMessages.异常：" + e.ToString());
                return "SendCommMessages.异常：" + e.ToString();
            }
        }

    }
}
