using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.WeiXin.Entity;
using ZhiFang.WeiXin.IDAO;

namespace ZhiFang.WeiXin.DAO.NHB
{	
	public class BAccountHospitalSearchContextDao :  ZhiFang.DAO.NHB.Base.BaseDaoNHB<BAccountHospitalSearchContext, long>, IDBAccountHospitalSearchContextDao
	{
        public bool DeleteByAccountID(long AccountID)
        {
            this.DeleteByHql(" From BAccountHospitalSearchContext bahsc where bahsc.AccountID=" + AccountID);
            return true;
        }
    } 
}