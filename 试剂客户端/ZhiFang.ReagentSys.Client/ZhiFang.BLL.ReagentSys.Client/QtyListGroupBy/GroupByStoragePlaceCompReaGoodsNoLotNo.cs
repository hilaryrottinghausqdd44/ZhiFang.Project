using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using ZhiFang.ReagentSys.Client.Common;

namespace ZhiFang.BLL.ReagentSys.Client.QtyListGroupBy
{
    /// <summary>
    /// 按按库房货架供货商+货品+货品批号
    /// </summary>
    class GroupByStoragePlaceCompReaGoodsNoLotNo : QtyListGroupByStrategy
    {
        public override IList<ReaBmsQtyDtl> SearchReaBmsQtyDtlListOfGroupBy(IDReaGoodsDao IDReaGoodsDao, IList<ReaBmsQtyDtl> tempQtyList)
        {
            IList<ReaBmsQtyDtl> qtyList = new List<ReaBmsQtyDtl>();
            if (tempQtyList != null && tempQtyList.Count > 0)
            {
                var groupBy = tempQtyList.GroupBy(p => new
                {
                    p.StorageID,
                    p.PlaceID,
                    p.ReaCompanyID,
                    p.ReaGoodsNo,                    
                    p.GoodsUnit,
                    p.LotNo
                });
                Dictionary<long, ReaGoods> goodsList = new Dictionary<long, ReaGoods>();
                foreach (var model in groupBy)
                {
                    ReaBmsQtyDtl qty = ClassMapperHelp.GetMapper<ReaBmsQtyDtl, ReaBmsQtyDtl>(model.ElementAt(0));
                    qty.GoodsQty = model.Where(a => a.GoodsQty.HasValue == true).Sum(k => k.GoodsQty);
                    //qty.SumTotal = model.Sum(k => k.SumTotal);
                    ////平均价格
                    //if (qty.GoodsQty.HasValue && qty.GoodsQty.Value > 0)
                    //    qty.Price = qty.SumTotal / qty.GoodsQty;
                    //else
                    //    qty.Price = 0;
                    qty.SumTotal = qty.GoodsQty * qty.Price;
                    long goodId = model.ElementAt(0).GoodsID.Value;
                    if (!goodsList.ContainsKey(goodId))
                    {
                        ReaGoods goods = IDReaGoodsDao.Get(goodId, false);
                        if (goods != null)
                        {
                            goodsList.Add(goodId, goods);
                            qty = QtyListGroupByHelp.SearchStoreUpperAndLowerValue(qty, goods);
                        }
                    }
                    else
                    {
                        qty = QtyListGroupByHelp.SearchStoreUpperAndLowerValue(qty, goodsList[goodId]);
                    }
                    qtyList.Add(qty);
                }
            }
            return qtyList;
        }
    }
}
