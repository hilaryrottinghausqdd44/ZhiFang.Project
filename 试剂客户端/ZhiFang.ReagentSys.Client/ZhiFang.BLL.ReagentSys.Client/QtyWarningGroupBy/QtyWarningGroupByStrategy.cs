using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;

namespace ZhiFang.BLL.ReagentSys.Client.QtyWarningGroupBy
{
    abstract class QtyWarningGroupByStrategy
    {
        public abstract IList<ReaBmsQtyDtl> SearchQtyWarningListOfGroupBy(IDReaGoodsDao IDReaGoodsDao, IList<ReaBmsQtyDtl> tempQtyList, IList<ReaBmsOutDtl> outDtlList, IList<ReaOpenBottleOperDoc> oBottleList, int warningType, float storePercent, string comparisonType);
    }
}
