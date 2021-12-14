using System;
using System.Collections.Generic;
using System.Linq;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using ZhiFang.ReagentSys.Client.Common;

namespace ZhiFang.BLL.ReagentSys.Client.QtyWarningGroupBy
{
    /// <summary>
    /// 不合并，按库存记录
    /// </summary>
    class WarnGroupByReaBmsQtyDtl : QtyWarningGroupByStrategy
    {
        public override IList<ReaBmsQtyDtl> SearchQtyWarningListOfGroupBy(IDReaGoodsDao IDReaGoodsDao, IList<ReaBmsQtyDtl> tempQtyList, IList<ReaBmsOutDtl> outDtlList, IList<ReaOpenBottleOperDoc> oBottleList, int warningType, float storePercent, string comparisonType)
        {
            IList<ReaBmsQtyDtl> resultList = new List<ReaBmsQtyDtl>();
            Dictionary<long, ReaGoods> goodsList = new Dictionary<long, ReaGoods>();
            ReaGoods goods = null;
            long goodId = 0;
            double sumGoodsQty = 0;
            foreach (var qtyVO2 in tempQtyList)
            {
                ReaBmsQtyDtl qtyVO = ClassMapperHelp.GetMapper<ReaBmsQtyDtl, ReaBmsQtyDtl>(qtyVO2);
                sumGoodsQty = qtyVO.GoodsQty.Value;
                goodId = qtyVO.GoodsID.Value;
                if (!goodsList.ContainsKey(goodId))
                {
                    goods = IDReaGoodsDao.Get(goodId, false);
                    goodsList.Add(goodId, goods);
                }
                else
                {
                    goods = goodsList[goodId];
                }
                if (goods.StoreUpper.HasValue)
                    qtyVO.StoreUpper = goods.StoreUpper.Value;
                if (goods.StoreLower.HasValue)
                    qtyVO.StoreLower = goods.StoreLower;
                if (goods.MonthlyUsage.HasValue)
                    qtyVO.MonthlyUsage = goods.MonthlyUsage.Value;

                bool isStockWarning = QtyWarningGroupByHelp.SearchIsStockWarning(outDtlList, goods, warningType, storePercent, comparisonType, sumGoodsQty, qtyVO);
                if (isStockWarning == true)
                {
                    ReaBmsQtyDtl qtyVO1 = QtyWarningGroupByHelp.SearchSetComparisonValue(outDtlList, goods, warningType, storePercent, comparisonType, sumGoodsQty, qtyVO);
                    resultList.Add(qtyVO1);
                }
            }
            return resultList;
        }
    }
}
