using System;
using System.ServiceModel.Activation;
using ZhiFang.Entity.Base;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.ReagentSys.Client
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class RBACService : RBACServiceCommon, ServerContract.IRBACService
    {
        public BaseResultDataValue RBAC_RJ_SearchRBACRowFilterTreeByModuleOperID(string id)
        {
            throw new NotImplementedException();
        }

        public BaseResultDataValue RBAC_UDTO_SearchModuleBySessionHREmpIDAndCookieModuleID()
        {
            throw new NotImplementedException();
        }
    }
}
