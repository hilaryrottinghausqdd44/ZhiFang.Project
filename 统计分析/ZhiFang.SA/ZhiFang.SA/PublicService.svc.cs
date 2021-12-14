using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using ZhiFang.ServiceCommon.RBAC;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.SA;
using Spring.Context;
using ZhiFang.IBLL.SA;
using Spring.Context.Support;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.SA
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]

    public class PublicService : PublicServiceCommon, ZhiFang.SA.ServerContract.IPublicService
    {
       
    }
}
