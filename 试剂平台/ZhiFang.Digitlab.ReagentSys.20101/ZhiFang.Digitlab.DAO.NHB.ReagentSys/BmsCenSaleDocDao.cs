using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IDAO;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Digitlab.IDAO.ReagentSys;

namespace ZhiFang.Digitlab.DAO.NHB.ReagentSys
{	
	public class BmsCenSaleDocDao : BaseDaoNHB<BmsCenSaleDoc, long>, IDBmsCenSaleDocDao
	{
        public IList<BmsCenSaleDoc> GetBmsCenSaleDocListBySQL(string sql)
        {
            IList<BmsCenSaleDoc> listBmsCenSaleDoc = null;
            this.Session.CreateQuery(sql);
            return listBmsCenSaleDoc;
        }
	} 
}