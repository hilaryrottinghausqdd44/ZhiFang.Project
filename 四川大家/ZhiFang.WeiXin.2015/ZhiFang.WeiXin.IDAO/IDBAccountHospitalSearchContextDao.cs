using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.WeiXin.Entity;

namespace ZhiFang.WeiXin.IDAO
{
	public interface IDBAccountHospitalSearchContextDao : ZhiFang.IDAO.Base.IDBaseDao<BAccountHospitalSearchContext, long>
	{
       bool  DeleteByAccountID(long AccountID);
	} 
}