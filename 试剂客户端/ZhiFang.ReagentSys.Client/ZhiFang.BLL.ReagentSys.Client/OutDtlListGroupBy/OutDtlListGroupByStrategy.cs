using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;

namespace ZhiFang.BLL.ReagentSys.Client.OutDtlListGroupBy
{
    abstract class OutDtlListGroupByStrategy
    {
        public abstract IList<ReaBmsOutDtl> SearchReaDtlListOfGroupBy(IDReaGoodsDao IDReaGoodsDao,IList<ReaBmsOutDtl> tempQtyList);
    }
}
