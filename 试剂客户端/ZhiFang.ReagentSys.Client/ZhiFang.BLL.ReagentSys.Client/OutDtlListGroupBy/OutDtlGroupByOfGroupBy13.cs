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
    /// 按出库单汇总
    /// 使用部门+出库单号+出库时间
    /// </summary>
    class OutDtlGroupByOfGroupBy13 : OutDtlListGroupByStrategy
    {
        public override IList<ReaBmsOutDtl> SearchReaDtlListOfGroupBy(IDReaGoodsDao IDReaGoodsDao, IList<ReaBmsOutDtl> outDtlList)
        {
            IList<ReaBmsOutDtl> dtlList = outDtlList.GroupBy(p => new
            {
                p.DeptID,
                p.DataAddTime,
                p.OutDocNo
            }).Select(g => new ReaBmsOutDtl
            {
                DeptID = g.Key.DeptID,
                DeptName = g.ElementAt(0).DeptName,
                OutDocNo = g.ElementAt(0).OutDocNo,     //出库单号
                DataAddTime = g.ElementAt(0).DataAddTime,
                SumTotal = g.Sum(k => k.SumTotal)
                
            }).ToList();
            return dtlList;
        }
    }
}
