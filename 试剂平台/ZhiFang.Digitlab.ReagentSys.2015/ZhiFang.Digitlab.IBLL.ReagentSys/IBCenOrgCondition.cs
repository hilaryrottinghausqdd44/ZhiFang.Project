

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity.ReagentSys;

namespace ZhiFang.Digitlab.IBLL.ReagentSys
{
	/// <summary>
	///
	/// </summary>
	public  interface IBCenOrgCondition : IBGenericManager<CenOrgCondition>
	{

        bool ValidateUpperAndLowerLevel(long upperID, long lowerID);
   
	}
}