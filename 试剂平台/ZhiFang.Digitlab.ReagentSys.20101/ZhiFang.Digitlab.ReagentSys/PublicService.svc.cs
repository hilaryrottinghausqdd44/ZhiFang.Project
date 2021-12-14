using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Web;
using System.IO;
using System.Reflection;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Common.Public;
using ZhiFang.Digitlab.ServiceCommon;

namespace ZhiFang.Digitlab.ReagentSys
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]

    public class PublicService : PublicServiceCommon, ZhiFang.Digitlab.ReagentSys.ServerContract.IPublicService
    {

    }
}
