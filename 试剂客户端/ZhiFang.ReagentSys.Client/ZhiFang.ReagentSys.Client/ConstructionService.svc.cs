using System.Collections.Generic;
using System.Reflection;
using System.ServiceModel.Activation;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.ReagentSys.Client
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ConstructionService : ConstructionServiceCommon, ZhiFang.ReagentSys.Client.ServerContract.IConstructionService
    {
        #region 服务成员
        protected override string AssemblyName
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Name;
            }
        }

        protected override IList<string> InterfaceList
        {
            get
            {
                return new List<string> { "IConstructionService", "IReagentSysService", "IRBACService", "ISingleTableService" };
            }
        }

        #endregion

        
    }
}
