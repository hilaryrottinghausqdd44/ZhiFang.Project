using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LIIP;
using ZhiFang.Entity.LIIP.ViewObject.Request;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LIIP
{
	/// <summary>
	///
	/// </summary>
	public  interface IBSCMsgHandle : IBGenericManager<SCMsgHandle>
	{
        BaseResultDataValue Add(SCMsgHandle Entity);
		BaseResultDataValue AddSCMsgHandle_ZF_LAB_START_CV(SCMsgHandle entity);
	}
}