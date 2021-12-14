using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using ZhiFang.WeiXin.Entity;

namespace ZhiFang.WeiXin.ServerContract
{
    [ServiceContract(Namespace = "ZhiFang.WeiXin")]
    public interface IReportFormWeiXinService
    {
        [OperationContract]
        long UpLoadRF(BSearchAccountReportForm entity);
    }
}
