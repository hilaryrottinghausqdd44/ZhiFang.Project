

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.IBLL.RBAC
{
	/// <summary>
	///
	/// </summary>
	public  interface IBBSex : ZhiFang.IBLL.Base.IBGenericManager<BSex>
	{
        BSex GetSexByName(IList<BSex> listSex, string sname);
    }
}