using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ZhiFang.WebLis.ServerContract
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IWCFSubscriptionService”。
    [ServiceContract(CallbackContract = typeof(IPushClient), Namespace = "ZhiFang.Digitlab.Service")]
    public interface ITestService
    {
        [OperationContract]
        bool RegisterClient();

        [OperationContract(Name = "RegisterClient_Account_Password")]
        bool RegisterClient(string Account, string Password);
    }
    public interface IPushClient
    {
        [OperationContract(IsOneWay = true)]
        void SendMessage(string message);
    }
}
