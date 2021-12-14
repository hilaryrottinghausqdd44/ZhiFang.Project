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
    /// 按使用出库明细:领用部门ID+供应商ID+使用仪器ID+货品产品编码+包装单位+规格+批号+效期+出库人ID+使用时间
    /// </summary>
     class OutDtlGroupByOfGroupBy12 : OutDtlListGroupByStrategy
    {
        public override IList<ReaBmsOutDtl> SearchReaDtlListOfGroupBy(IDReaGoodsDao IDReaGoodsDao, IList<ReaBmsOutDtl> outDtlList)
        {
            IList<ReaBmsOutDtl> dtlList = outDtlList.GroupBy(p => new
            {
                p.DeptID,
                p.ReaCompanyID,
                p.TestEquipID,
                p.ReaGoodsNo,
                p.GoodsUnit,
                p.UnitMemo,
                p.LotNo,
                p.InvalidDate,
                p.CreaterID,
                p.DataAddTime,
                p.TransportNo,
                p.OutDocNo,
                p.SName,
                p.TestCount
            }).Select(g => new ReaBmsOutDtl
            {
                DeptID = g.Key.DeptID,
                DeptName = g.ElementAt(0).DeptName,
                ReaCompanyID = g.Key.ReaCompanyID,
                CompGoodsLinkID = g.ElementAt(0).CompGoodsLinkID,
                CompanyName = g.ElementAt(0).CompanyName,
                ReaGoodsNo = g.Key.ReaGoodsNo,
                ProdGoodsNo = g.ElementAt(0).ProdGoodsNo,
                GoodsNo = g.ElementAt(0).GoodsNo,
                CenOrgGoodsNo = g.ElementAt(0).CenOrgGoodsNo,

                GoodsID = g.ElementAt(0).GoodsID,
                GoodsCName = g.ElementAt(0).GoodsCName,
                GoodsUnit = g.ElementAt(0).GoodsUnit,
                UnitMemo = g.ElementAt(0).UnitMemo,
                LotNo = g.Key.LotNo,
                InvalidDate = g.ElementAt(0).InvalidDate,
                BarCodeType = g.ElementAt(0).BarCodeType,
                TestEquipID = g.ElementAt(0).TestEquipID,
                TestEquipName = g.ElementAt(0).TestEquipName,
                CreaterID = g.ElementAt(0).CreaterID,
                CreaterName = g.ElementAt(0).CreaterName,
                DataAddTime = g.ElementAt(0).DataAddTime,
                ReqGoodsQty = g.Sum(k => k.ReqGoodsQty),
                GoodsQty = g.Sum(k => k.GoodsQty),
                SumTotal = g.Sum(k => k.SumTotal),
                Price = g.Sum(k => k.GoodsQty) > 0 ? g.Sum(k => k.SumTotal) / g.Sum(k => k.GoodsQty) : 0,//g.ElementAt(0).Price,
                ProdDate = g.ElementAt(0).ProdDate,
                TaxRate = g.ElementAt(0).TaxRate,
                Memo = g.ElementAt(0).Memo,
                TransportNo = g.ElementAt(0).TransportNo,
                OutDocNo = g.ElementAt(0).OutDocNo,     //出库单号
                SName = g.ElementAt(0).SName,    //货品简称
                ProdOrgName = g.ElementAt(0).ProdOrgName,   //品牌
                TestCount = g.ElementAt(0).TestCount    //测试数
            }).ToList();
            return dtlList;
        }
    }
}
