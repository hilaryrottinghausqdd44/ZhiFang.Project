using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;

namespace ZhiFang.BLL.ReagentSys.Client.QtyWarningGroupBy
{
    class QtyWarningGroupByContext
    {
        private IList<ReaBmsQtyDtl> _nfList = new List<ReaBmsQtyDtl>();
        private QtyWarningGroupByStrategy dimensionStrategy = null;  //对象组合
        private IDReaGoodsDao IDReaGoodsDao = null;
        /// <summary>
        /// 获取List集合使用
        /// </summary>
        /// <param name="nfList"></param>
        /// <param name="strategy"></param>
        public QtyWarningGroupByContext(IDReaGoodsDao IDReaGoodsDao, IList<ReaBmsQtyDtl> nfList, QtyWarningGroupByStrategy strategy)
        {
            this._nfList = nfList;
            this.dimensionStrategy = strategy;
            this.IDReaGoodsDao = IDReaGoodsDao;
        }
        public IList<ReaBmsQtyDtl> SearchQtyWarningListOfGroupBy(IList<ReaBmsOutDtl> outDtlList, IList<ReaOpenBottleOperDoc> oBottleList, int warningType, float storePercent, string comparisonType)
        {
            IList<ReaBmsQtyDtl> nfList2 = this.dimensionStrategy.SearchQtyWarningListOfGroupBy(this.IDReaGoodsDao, this._nfList, outDtlList, oBottleList, warningType, storePercent, comparisonType);
            return nfList2;
        }
    }
}
