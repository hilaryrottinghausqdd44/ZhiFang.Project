using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using ZhiFang.ReagentSys.Client.Common;

namespace ZhiFang.BLL.ReagentSys.Client.OutDtlListGroupBy
{
    /// <summary>
    /// 按货品规格
    /// </summary>
    class OutDtlGroupByReaGoodsNoGoodsUnit : OutDtlListGroupByStrategy
    {
        public override IList<ReaBmsOutDtl> SearchReaDtlListOfGroupBy(IDReaGoodsDao IDReaGoodsDao, IList<ReaBmsOutDtl> outDtlList)
        {
            IList<ReaBmsOutDtl> dtlList = outDtlList.GroupBy(p => new
            {
                p.ReaGoodsNo,
                p.GoodsUnit
            }).Select(g => new ReaBmsOutDtl
            {
                ReaGoodsNo = g.Key.ReaGoodsNo,
                ProdGoodsNo = g.ElementAt(0).ProdGoodsNo,
                GoodsNo = g.ElementAt(0).GoodsNo,
                GoodsID = g.ElementAt(0).GoodsID,
                GoodsCName = g.ElementAt(0).GoodsCName,
                GoodsUnit = g.ElementAt(0).GoodsUnit,
                UnitMemo = g.ElementAt(0).UnitMemo,
                BarCodeType = g.ElementAt(0).BarCodeType,
                ReqGoodsQty = g.Sum(k => k.ReqGoodsQty),
                GoodsQty = g.Sum(k => k.GoodsQty),
                SumTotal = g.Sum(k => k.SumTotal),
                Price = g.Sum(k => k.GoodsQty) > 0 ? g.Sum(k => k.SumTotal) / g.Sum(k => k.GoodsQty) : 0,
                Memo = g.ElementAt(0).Memo,
                SName = g.ElementAt(0).SName,
                ProdOrgName = g.ElementAt(0).ProdOrgName,
                TestCount = g.ElementAt(0).TestCount
            }).ToList();
            return dtlList;
        }
    }
}
