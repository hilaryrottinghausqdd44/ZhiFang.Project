using System;
using System.Collections.Generic;
using System.Linq;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using ZhiFang.ReagentSys.Client.Common;

namespace ZhiFang.BLL.ReagentSys.Client.QtyWarningGroupBy
{
    /// <summary>
    /// 按货品规格(库存总量
    /// </summary>
    class WarnGroupByReaGoodsNoGoodsUnit : QtyWarningGroupByStrategy
    {
        public override IList<ReaBmsQtyDtl> SearchQtyWarningListOfGroupBy(IDReaGoodsDao IDReaGoodsDao, IList<ReaBmsQtyDtl> tempQtyList, IList<ReaBmsOutDtl> outDtlList, IList<ReaOpenBottleOperDoc> oBottleList, int warningType, float storePercent, string comparisonType)
        {
            IList<ReaBmsQtyDtl> resultList = new List<ReaBmsQtyDtl>();
            var qtyGroupByList = tempQtyList.GroupBy(p => new
            {
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
                    p.ReaBmsQtyDtl.ReaGoodsNo,
                    p.ReaBmsQtyDtl.GoodsUnit
                }).Count();
                sumGoodsQty = sumGoodsQty + sumGoodsQty2;

                qtyVO.GoodsQty = sumGoodsQty;
                qtyVO.SumTotal = qtyGroupBy.Where(a => a.SumTotal.HasValue == true).Sum(a => a.SumTotal.Value);
                if (qtyVO.GoodsID.HasValue)
                    goodId = qtyVO.GoodsID.Value;
                if (!goodsList.ContainsKey(goodId))
                {
                    goods = IDReaGoodsDao.Get(goodId, false);
                    if (goods != null)
                        goodsList.Add(goodId, goods);
                }
                else
                {
                    goods = goodsList[goodId];
                }
                if (goods == null)
                    ZhiFang.Common.Log.Log.Warn("获取库存记录ID的机构货品信息为空!qtyId:" + qtyVO.Id + ",GoodsID:" + goodId);
                if (goods != null)
                {
                    if (goods.StoreUpper.HasValue)
                        qtyVO.StoreUpper = goods.StoreUpper.Value;
                    if (goods.StoreLower.HasValue)
                        qtyVO.StoreLower = goods.StoreLower;
                    if (goods.MonthlyUsage.HasValue)
                        qtyVO.MonthlyUsage = goods.MonthlyUsage.Value;
                }

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
