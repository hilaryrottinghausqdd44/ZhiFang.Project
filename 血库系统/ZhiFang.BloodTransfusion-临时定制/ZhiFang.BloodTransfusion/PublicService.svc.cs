using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using ZhiFang.ServiceCommon.RBAC;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using Spring.Context;
using ZhiFang.IBLL.BloodTransfusion;
using Spring.Context.Support;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.BloodTransfusion
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]

    public class PublicService : PublicServiceCommon, ZhiFang.BloodTransfusion.ServerContract.IPublicService
    {
       
    }
}
