

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.IBLL.Business
{
	/// <summary>
	///
	/// </summary>
	public  interface IBGMGroupType : IBGenericManager<GMGroupType>
	{
        BaseResultTree GetGroupTreeByEmpId(long empid);
	}
}