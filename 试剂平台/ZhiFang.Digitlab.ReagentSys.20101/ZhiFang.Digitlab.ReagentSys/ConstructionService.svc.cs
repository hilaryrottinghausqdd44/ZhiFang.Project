using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Reflection;
using ZhiFang.Digitlab.ServiceCommon;
using ZhiFang.Digitlab.Entity;
using Newtonsoft.Json;
using ZhiFang.Common.Log;
using System.Web;
using System.IO;
using ZhiFang.Common.Public;
using System.Web.Script.Serialization;
using System.Globalization;

namespace ZhiFang.Digitlab.ReagentSys
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ConstructionService : ConstructionServiceCommon, ZhiFang.Digitlab.ReagentSys.ServerContract.IConstructionService
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
