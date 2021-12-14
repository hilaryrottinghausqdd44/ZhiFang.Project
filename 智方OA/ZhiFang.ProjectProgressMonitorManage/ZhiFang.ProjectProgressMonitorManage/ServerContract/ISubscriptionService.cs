using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ZhiFang.ServiceCommon.RBAC;
using ZhiFang.ProjectProgressMonitorManage.IClient;

namespace ZhiFang.ProjectProgressMonitorManage.ServerContract
{
    [ServiceContract(CallbackContract = typeof(IPushClient), Namespace = "ZhiFang.ProjectProgressMonitorManage")]
    public interface ISubscriptionService
    {
        //[ServiceContractDescription(Name = "注册订阅客户端", Desc = "注册订阅客户端", Url = "ConstructionService.svc/RegisterClient", Get = "", Post = "", Return = "bool", ReturnType = "bool")]
        [OperationContract]
        bool RegisterClient();
        //[ServiceContractDescription(Name = "注册订阅客户端", Desc = "注册订阅客户端", Url = "ConstructionService.svc/RegisterClient", Get = "", Post = "", Return = "bool", ReturnType = "bool")]
        [OperationContract(Name="RegisterClient_Account_Password")]
        bool RegisterClient(string Account, string Password);
    }
}
