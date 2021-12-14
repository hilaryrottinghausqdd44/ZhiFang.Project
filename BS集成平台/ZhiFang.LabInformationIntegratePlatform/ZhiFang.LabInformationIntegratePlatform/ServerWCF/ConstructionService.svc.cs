using System.Collections.Generic;
using System.Reflection;
using System.ServiceModel.Activation;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ConstructionService : ConstructionServiceCommon, ServerContract.IConstructionService
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
                return new List<string> { "IConstructionService", "IGeneSysService", "IRBACService", "ISingleTableService" };
            }
        }

        #endregion

        
    }
}
