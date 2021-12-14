using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;

namespace ZhiFang.BLL.ReagentSys.Client.QtyListGroupBy
{
    abstract class QtyListGroupByStrategy
    {
        public abstract IList<ReaBmsQtyDtl> SearchReaBmsQtyDtlListOfGroupBy(IDReaGoodsDao IDReaGoodsDao,IList<ReaBmsQtyDtl> tempQtyList);
    }
}
