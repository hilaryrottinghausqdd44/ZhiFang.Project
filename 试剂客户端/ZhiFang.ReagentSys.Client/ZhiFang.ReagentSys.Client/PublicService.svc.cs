using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using ZhiFang.ServiceCommon.RBAC;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using Spring.Context;
using ZhiFang.IBLL.ReagentSys.Client;
using Spring.Context.Support;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.ReagentSys.Client
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]

    public class PublicService : PublicServiceCommon, ZhiFang.ReagentSys.Client.ServerContract.IPublicService
    {
       
    }
}
