using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.NHB.ReagentSys.Client
{
	public interface IDReaDeptGoodsDao : IDBaseDao<ReaDeptGoods, long>
	{
		EntityList<ReaGoods> SearchReaGoodsListByHQL(int page, int limit, string where, string sort);
	} 
}