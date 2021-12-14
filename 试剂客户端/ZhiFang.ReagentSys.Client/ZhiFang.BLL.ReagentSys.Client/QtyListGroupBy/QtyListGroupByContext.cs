using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;

namespace ZhiFang.BLL.ReagentSys.Client.QtyListGroupBy
{
    class QtyListGroupByContext
    {
        private IList<ReaBmsQtyDtl> _nfList = new List<ReaBmsQtyDtl>();
        private QtyListGroupByStrategy dimensionStrategy = null;  //对象组合
        private IDReaGoodsDao IDReaGoodsDao = null;
        /// <summary>
        /// 获取List集合使用
        /// </summary>
        /// <param name="nfList"></param>
        /// <param name="strategy"></param>
        public QtyListGroupByContext(IDReaGoodsDao IDReaGoodsDao, IList<ReaBmsQtyDtl> nfList, QtyListGroupByStrategy strategy)
        {
            this._nfList = nfList;
            this.dimensionStrategy = strategy;
            this.IDReaGoodsDao = IDReaGoodsDao;
        }
        public IList<ReaBmsQtyDtl> SearchReaBmsQtyDtlListOfGroupBy()
        {
            IList<ReaBmsQtyDtl> nfList2 = this.dimensionStrategy.SearchReaBmsQtyDtlListOfGroupBy(this.IDReaGoodsDao, this._nfList);
            return nfList2;
        }
    }
}
