using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.ServiceCommon;
using ZhiFang.Common.Public;
using ZhiFang.Digitlab.ReagentSys.ServerContract;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Web;

namespace ZhiFang.Digitlab.ReagentSys
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class RBACService : RBACServiceCommon, ZhiFang.Digitlab.ReagentSys.ServerContract.IRBACService
    {


        public BaseResultDataValue RBAC_UDTO_SearchModuleBySessionHREmpIDAndCookieModuleID()
        {
            throw new NotImplementedException();
        }
    }
}
