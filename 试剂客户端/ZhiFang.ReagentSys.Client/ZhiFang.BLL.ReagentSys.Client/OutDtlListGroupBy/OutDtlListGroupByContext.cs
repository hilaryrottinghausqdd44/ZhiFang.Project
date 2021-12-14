using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;

namespace ZhiFang.BLL.ReagentSys.Client.OutDtlListGroupBy
{
    class OutDtlListGroupByContext
    {
        private IList<ReaBmsOutDtl> _nfList = new List<ReaBmsOutDtl>();
        private OutDtlListGroupByStrategy dimensionStrategy = null;  //对象组合
        private IDReaGoodsDao IDReaGoodsDao = null;
        /// <summary>
        /// 获取List集合使用
        /// </summary>
        /// <param name="nfList"></param>
        /// <param name="strategy"></param>
        public OutDtlListGroupByContext(IDReaGoodsDao IDReaGoodsDao, IList<ReaBmsOutDtl> nfList, OutDtlListGroupByStrategy strategy)
        {
            this._nfList = nfList;
            this.dimensionStrategy = strategy;
            this.IDReaGoodsDao = IDReaGoodsDao;
        }
        public IList<ReaBmsOutDtl> SearchReaDtlListOfGroupBy()
        {
            IList<ReaBmsOutDtl> nfList2 = this.dimensionStrategy.SearchReaDtlListOfGroupBy(this.IDReaGoodsDao, this._nfList);
            return nfList2;
        }
    }
}
