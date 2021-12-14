using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.WebAssist.ServerWCF
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]

    public class PublicService : PublicServiceCommon, ZhiFang.WebAssist.ServerContract.IPublicService
    {
       
    }
}
