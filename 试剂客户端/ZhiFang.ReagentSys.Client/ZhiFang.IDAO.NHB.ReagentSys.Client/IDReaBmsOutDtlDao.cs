using ZhiFang.IDAO.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.Entity.Base;
using System.Collections.Generic;

namespace ZhiFang.IDAO.NHB.ReagentSys.Client
{
    public interface IDReaBmsOutDtlDao : IDBaseDao<ReaBmsOutDtl, long>
    {
        /// <summary>
        /// 出库汇总统计报表
        /// </summary>
        /// <param name="docHql"></param>
        /// <param name="dtlHql"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        IList<ReaBmsOutDtl> SearchReaBmsOutDtlSummaryByHQL(string docHql, string dtlHql, string reaGoodsHql, string sort, int page, int limit);
        /// <summary>
        /// 按出库主单+出库明细+机构货品+(部门货品)连接查询
        /// </summary>
        /// <param name="docHql"></param>
        /// <param name="dtlHql"></param>
        /// <param name="deptGoodsHql"></param>
        /// <param name="reaGoodsHql"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        IList<ReaBmsOutDtl> SearchOutDocAndDtlListByAllJoinHql(string docHql, string dtlHql, string deptGoodsHql, string reaGoodsHql, int page, int limit, string sort);
        /// <summary>
        /// 按查询条件连接查询
        /// </summary>
        /// <param name="docHql"></param>
        /// <param name="dtlHql"></param>
        /// <param name="deptGoodsHql"></param>
        /// <param name="reaGoodsHql"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        IList<ReaBmsOutDtl> SearchReaBmsOutDtlListByJoinHql(string docHql, string dtlHql, string deptGoodsHql, string reaGoodsHql, int page, int limit, string sort);
        /// <summary>
        /// 按查询条件连接查询
        /// </summary>
        /// <param name="docHql"></param>
        /// <param name="dtlHql"></param>
        /// <param name="deptGoodsHql"></param>
        /// <param name="reaGoodsHql"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        EntityList<ReaBmsOutDtl> SearchReaBmsOutDtlEntityListByJoinHql(string docHql, string dtlHql, string deptGoodsHql, string reaGoodsHql, int page, int limit, string sort);

        IList<ReaBmsOutDtl> SearchListByJoinHql(string dtlHql, string reaGoodHql, string sort, int page, int limit);

        /// <summary>
        /// 智方试剂平台查询使用，hasLabId传false，不增加LabID的默认条件
        /// </summary>
        EntityList<ReaBmsOutDtl> GetListByHQL(string strHqlWhere, string Order, int start, int count, bool hasLabId);
    }
}