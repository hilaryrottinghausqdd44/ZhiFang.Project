using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LIIP;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LIIP
{
	/// <summary>
	///
	/// </summary>
	public  interface IBSAccountRegister : IBGenericManager<SAccountRegister>
	{
		BaseResultDataValue AddEntity(SAccountRegister entity);
		BaseResultDataValue ST_UDTO_ApprovalSAccountRegister(SAccountRegister entity, bool IsPass);
        bool SetOpenIdByEmpId(string weiXinMiniOpenID, string EmpId);
    }
}