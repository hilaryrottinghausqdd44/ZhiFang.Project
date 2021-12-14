using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity.ReagentSys;

namespace ZhiFang.Digitlab.IDAO.ReagentSys
{
	public interface IDBmsCenSaleDocDao : IDBaseDao<BmsCenSaleDoc, long>
	{
        IList<BmsCenSaleDoc> GetBmsCenSaleDocListBySQL(string sql);
	} 
}