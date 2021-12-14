using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ZhiFang.ReportFormQueryPrint.Model;

namespace ZhiFang.ReportFormQueryPrint.ServerContract
{
    [ServiceContract(Namespace = "ZhiFang.ReportFormQueryPrint")]
    public interface IServiceTest
    {
        /// <summary>
        /// Hello world
        /// </summary>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/HelloWorld?name={name}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("查询报告单 /GetDeptList?Where={Where}&fields={fields}&page={page}&limit={limit}")]
        [OperationContract]
        BaseResultDataValue HelloWorld(string name);
    }
}
