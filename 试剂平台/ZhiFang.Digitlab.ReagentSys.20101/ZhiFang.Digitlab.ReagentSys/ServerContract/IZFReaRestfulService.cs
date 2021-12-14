using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.ServiceModel;
using System.Web;
using System.ServiceModel.Web;
using System.ServiceModel.Channels;

namespace ZhiFang.Digitlab.ReagentSys.ServerContract
{
    [ServiceContract]
    public interface IZFReaRestfulService
    {
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WebRequestHttpPostWMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        string WebRequestHttpPostWMS(string saleDocNo);

        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "saleDocNo={saleDocNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        string WebRequestHttpGetWMS(string saleDocNo);
    }
}
