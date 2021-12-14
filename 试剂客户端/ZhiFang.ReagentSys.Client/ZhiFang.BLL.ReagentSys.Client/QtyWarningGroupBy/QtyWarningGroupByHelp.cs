using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.ReagentSys.Client.Common;

namespace ZhiFang.BLL.ReagentSys.Client.QtyWarningGroupBy
{
    public class QtyWarningGroupByHelp
    {
       
        public static ReaBmsQtyDtl SearchStoreUpperAndLowerValue(ReaBmsQtyDtl entity, ReaGoods goods)
        {
            if (goods == null) return entity;

            if (goods.StoreUpper.HasValue)
                entity.StoreUpper = goods.StoreUpper.Value;
            if (goods.StoreLower.HasValue)
                entity.StoreLower = goods.StoreLower;
            entity.StoreLower = goods.StoreLower;
            entity.StoreLower = goods.StoreLower;
            //库存所剩理论测试数计算:库存数量乘以基础数据中每包装单位理论测试数
            if (goods.TestCount > 0)
                entity.StocSurplusTestQty = entity.GoodsQty * goods.TestCount;
            return entity;
        }
        public static bool SearchIsStockWarning(ReaGoods goods, double? goodsQty, int warningType, float storePercent)
        {
            bool isStockWarning = false;
            switch (warningType)
            {
                case 1://低库存
                    if (goods.StoreLower.HasValue && goods.StoreLower.Value > 0)
                    {
                        var curLowerWarning = goods.StoreLower.Value * storePercent / 100;
                        if (goodsQty < curLowerWarning)
                            isStockWarning = true;
                    }
                    break;
                case 2://高库存
                    if (goods.StoreUpper.HasValue && goods.StoreUpper.Value > 0)
                    {
                        var curUpperWarning = goods.StoreUpper.Value * storePercent / 100;
                        if (goodsQty > curUpperWarning)
                            isStockWarning = true;
                    }
                    break;
                default:

                    break;
            }
            return isStockWarning;
        }
        public static bool SearchIsStockWarning(double comparisonValue, double? goodsQty, int warningType, float storePercent)
        {
            bool isStockWarning = false;
            switch (warningType)
            {
                case 1://低库存
                    if (comparisonValue > 0)
                    {
                        var curLowerWarning = comparisonValue * storePercent / 100;
                        if (goodsQty < curLowerWarning)
                            isStockWarning = true;
                    }
                    break;
                case 2://高库存
                    if (comparisonValue > 0)
                    {
                        var curUpperWarning = comparisonValue * storePercent / 100;
                        if (goodsQty > curUpperWarning)
                            isStockWarning = true;
                    }
                    break;
                default:

                    break;
            }
            return isStockWarning;
        }
        public static bool SearchIsStockWarningOfMonthlyUsage(ReaGoods goods, double? goodsQty, int warningType, float storePercent)
        {
            bool isStockWarning = false;
            switch (warningType)
            {
                case 1://低库存
                    if (goods.MonthlyUsage.HasValue && goods.MonthlyUsage.Value > 0)
                    {
                        var curLowerWarning = goods.MonthlyUsage.Value * storePercent / 100;
                        if (goodsQty < curLowerWarning)
                            isStockWarning = true;
                    }
                    break;
                case 2://高库存
                    if (goods.MonthlyUsage.HasValue && goods.MonthlyUsage.Value > 0)
                    {
                        var curUpperWarning = goods.StoreUpper.Value * storePercent / 100;
                        if (goodsQty > curUpperWarning)
                            isStockWarning = true;
                    }
                    break;
                default:

                    break;
            }
            return isStockWarning;
        }
        public static void SearchGetComparisonValue(IList<ReaBmsOutDtl> outDtlList2, string comparisonType, ref double comparisonValue)
        {
            if (comparisonType == QtyWarningComparisonValueType.上月使用量.Key)
            {
                if (outDtlList2 != null && outDtlList2.Count() > 0)
                {
                    comparisonValue = outDtlList2.Sum(p => p.GoodsQty);
                }
            }
            else if (comparisonType == QtyWarningComparisonValueType.月均使用量.Key)
            {
                if (outDtlList2 != null && outDtlList2.Count() > 0)
                {
                    comparisonValue = outDtlList2.Sum(p => p.GoodsQty);
                    //先按年月分组,计算该日期范围内有几个自然月
                    var outDtlGroupBy = outDtlList2.ToList().GroupBy(p => new
                    {
                        YearMonth = p.DataAddTime.Value.ToString("yyyy-MM")
                    });
                    int month = outDtlGroupBy.Count();
                    if (month > 1)
                    {
                        comparisonValue = comparisonValue / month;
                    }
                }
            }
            else if (comparisonType == QtyWarningComparisonValueType.月用量最大值.Key)
            {
                if (outDtlList2 != null && outDtlList2.Count() > 0)
                {
                    //先按年月分组
                    var outDtlGroupBy = outDtlList2.ToList().GroupBy(p => new
                    {
                        YearMonth = p.DataAddTime.Value.ToString("yyyy-MM")
                    });
                    //获取月用量最大值
                    foreach (var groupBy in outDtlGroupBy)
                    {
                        var tempValue = groupBy.Sum(p => p.GoodsQty);
                        if (tempValue > comparisonValue)
                            comparisonValue = tempValue;
                    }
                }
            }
            if (comparisonValue > 0)
                comparisonValue = ConvertQtyHelp.ConvertQty(comparisonValue, 3);
        }
        public static bool SearchIsStockWarning(IList<ReaBmsOutDtl> outDtlList, ReaGoods goods, int warningType, float storePercent, string comparisonType, double sumGoodsQty, ReaBmsQtyDtl qtyVO)
        {
            bool isStockWarning = false;
            if (comparisonType == QtyWarningComparisonValueType.库存预设值.Key)
            {
                //预警类型(1:低库存：2：高库存)
                if (warningType == 1 && qtyVO.StoreLower.HasValue)
                {
                    isStockWarning = true;
                }
                else if (warningType == 2 && qtyVO.StoreUpper.HasValue)
                {
                    isStockWarning = true;
                }

                if (isStockWarning == true) isStockWarning = QtyWarningGroupByHelp.SearchIsStockWarning(goods, sumGoodsQty, warningType, storePercent);
            }
            else if (comparisonType == QtyWarningComparisonValueType.理论月用量.Key)
            {
                isStockWarning = QtyWarningGroupByHelp.SearchIsStockWarningOfMonthlyUsage(goods, sumGoodsQty, warningType, storePercent);
            }
            else
            {
                double comparisonValue = 0;
                var goodId2 = qtyVO.GoodsID;
                var outDtlList2 = outDtlList.Where(p => p.GoodsID == goodId2).ToList();
                QtyWarningGroupByHelp.SearchGetComparisonValue(outDtlList2, comparisonType, ref comparisonValue);
                isStockWarning = QtyWarningGroupByHelp.SearchIsStockWarning(comparisonValue, sumGoodsQty, warningType, storePercent);
            }
            return isStockWarning;
        }
        public static ReaBmsQtyDtl SearchSetComparisonValue(IList<ReaBmsOutDtl> outDtlList, ReaGoods goods, int warningType, float storePercent, string comparisonType, double sumGoodsQty, ReaBmsQtyDtl qtyVO)
        {
            if (comparisonType == QtyWarningComparisonValueType.库存预设值.Key)
            {
                switch (warningType)
                {
                    case 1://低库存
                        if (goods.StoreLower.HasValue)
                            qtyVO.ComparisonValue = goods.StoreLower.Value * storePercent / 100; ;
                        break;
                    case 2://高库存
                        if (goods.StoreUpper.HasValue)
                            qtyVO.ComparisonValue = goods.StoreUpper.Value * storePercent / 100;
                        break;
                    default:
                        break;
                }
            }
            else if (comparisonType == QtyWarningComparisonValueType.理论月用量.Key)
            {
                if (goods.MonthlyUsage.HasValue)
                    qtyVO.ComparisonValue = goods.MonthlyUsage.Value * storePercent / 100;
            }
            else
            {
                double comparisonValue = 0;
                var goodId = qtyVO.GoodsID;
                var outDtlList2 = outDtlList.Where(p => p.GoodsID == goodId).ToList();
                QtyWarningGroupByHelp.SearchGetComparisonValue(outDtlList2, comparisonType, ref comparisonValue);
                qtyVO.ComparisonValue = comparisonValue * storePercent / 100;
            }
            return qtyVO;
        }
    }
}
