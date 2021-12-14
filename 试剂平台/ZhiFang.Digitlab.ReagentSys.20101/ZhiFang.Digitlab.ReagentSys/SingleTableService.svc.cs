using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.ServiceCommon;
using ZhiFang.Common.Public;
using ZhiFang.Digitlab.IBLL.Business;

namespace ZhiFang.Digitlab.ReagentSys
{      
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SingleTableService : SingleTableServiceCommon, ZhiFang.Digitlab.ReagentSys.ServerContract.ISingleTableService
    {
       
    }
}
