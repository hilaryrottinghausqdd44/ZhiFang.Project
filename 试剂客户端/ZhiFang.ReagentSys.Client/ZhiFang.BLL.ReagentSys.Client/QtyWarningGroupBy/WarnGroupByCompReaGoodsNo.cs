using System;
using System.Collections.Generic;
using System.Linq;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using ZhiFang.ReagentSys.Client.Common;

namespace ZhiFang.BLL.ReagentSys.Client.QtyWarningGroupBy
{
    /// <summary>
    /// 按供应商及货品编码
    /// </summary>
    class WarnGroupByCompReaGoodsNo : QtyWarningGroupByStrategy
    {
        public override IList<ReaBmsQtyDtl> SearchQtyWarningListOfGroupBy(IDReaGoodsDao IDReaGoodsDao, IList<ReaBmsQtyDtl> tempQtyList, IList<ReaBmsOutDtl> outDtlList, IList<ReaOpenBottleOperDoc> oBottleList, int warningType, float storePercent, string comparisonType)
        {
            IList<ReaBmsQtyDtl> resultList = new List<ReaBmsQtyDtl>();
            var qtyGroupByList = tempQtyList.GroupBy(p => new
            {
                p.ReaCompanyID,
                p.ReaGoodsNo,
                p.GoodsUnit
            });
            Dictionary<long, ReaGoods> goodsList = new Dictionary<long, ReaGoods>();
            ReaGoods goods = null;
            long goodId = 0;
            double sumGoodsQty = 0;
            foreach (var qtyGroupBy in qtyGroupByList)
            {
                goodId = 0;
                goods = null;
                ReaBmsQtyDtl qtyVO = ClassMapperHelp.GetMapper<ReaBmsQtyDtl, ReaBmsQtyDtl>(qtyGroupBy.ElementAt(0));
                sumGoodsQty = qtyGroupBy.Where(a => a.GoodsQty.HasValue == true).Sum(a => a.GoodsQty.Value);

                //需要考虑开瓶管理未使用完的库存量
                double sumGoodsQty2 = oBottleList.GroupBy(p => new
                {
                    p.ReaBmsQtyDtl.ReaCompanyID,
                    p.ReaBmsQtyDtl.ReaGoodsNo,
                    p.ReaBmsQtyDtl.GoodsUnit
                }).Count();
                sumGoodsQty = sumGoodsQty + sumGoodsQty2;

                qtyVO.GoodsQty = sumGoodsQty;
                qtyVO.SumTotal = qtyVO.GoodsQty * qtyVO.Price;// qtyGroupBy.Sum(a => a.SumTotal.Value);
                goodId = qtyGroupBy.ElementAt(0).GoodsID.Value;
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
