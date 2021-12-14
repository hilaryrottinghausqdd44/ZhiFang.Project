using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using ZhiFang.ReportFormQueryPrint.Model;
using ZhiFang.ReportFormQueryPrint.ServerContract;

namespace ZhiFang.ReportFormQueryPrint.ServiceWCF
{   
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ServiceTest : IServiceTest
    {
        public Model.BaseResultDataValue HelloWorld(string name)
        {
            BaseResultDataValue a = new BaseResultDataValue();
            a.ResultDataValue = "helloWorld!" + name;
            a.success = true;
            return a;
        }
    }
}
