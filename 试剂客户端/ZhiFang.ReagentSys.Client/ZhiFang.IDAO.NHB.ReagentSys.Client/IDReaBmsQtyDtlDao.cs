using ZhiFang.IDAO.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.Response;
using System.Collections.Generic;
using ZhiFang.Entity.Base;

namespace ZhiFang.IDAO.NHB.ReagentSys.Client
{
    public interface IDReaBmsQtyDtlDao : IDBaseDao<ReaBmsQtyDtl, long>
    {
        /// <summary>
        /// 库存预警
        /// </summary>
        /// <param name="warningType"></param>
        /// <param name="groupByType"></param>
        /// <param name="storePercent"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        IList<ReaBmsQtyDtlWarning> SearchReaBmsQtyDtlWarningList(int warningType, int groupByType, float storePercent, string strWhere);
        /// <summary>
        /// 批条码打印次数更新
        /// </summary>
        /// <param name="batchList"></param>
        /// <returns></returns>
        BaseResultBool UpdatePrintCount(IList<long> batchList);
        /// <summary>
        /// 获取某一库存货品的当前库存数描述处理
        /// </summary>
        /// <param name="qtyList"></param>
        /// <param name="reaGoodsNo"></param>
        /// <returns>"1箱;10盒;3支"</returns>
        string GetCurrentQtyInfo(IList<ReaBmsQtyDtl> qtyList, string reaGoodsNo);
        /// <summary>
        /// 获取库存查询结果,支持按部门货品及机构货品的数据项进行过滤
        /// </summary>
        /// <param name="qtyHql">库存查询条件</param>
        /// <param name="deptGoodsHql">部门货品查询条件</param>
        /// <param name="reaGoodsHql">机构货品查询条件</param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        IList<ReaBmsQtyDtl> SearchReaBmsQtyDtlListByHql(string qtyHql, string deptGoodsHql, string reaGoodsHql, string sort, int page, int limit);
        /// <summary>
        /// 获取库存查询结果,支持按部门货品及机构货品的数据项进行过滤
        /// </summary>
        /// <param name="qtyHql">库存查询条件</param>
        /// <param name="deptGoodsHql">部门货品查询条件</param>
        /// <param name="reaGoodsHql">机构货品查询条件</param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<ReaBmsQtyDtl> SearchReaBmsQtyDtlEntityListByHql(string qtyHql, string deptGoodsHql, string reaGoodsHql, string sort, int page, int limit);
        /// <summary>
        /// 联合查询库存信息及机构货品信息
        /// </summary>
        /// <param name="qtyHql"></param>
        /// <param name="deptGoodsHql"></param>
        /// <param name="reaGoodsHql"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        IList<ReaBmsQtyDtl> SearchReaBmsQtyDtlAndReaGoodsListByAllJoinHql(string qtyHql, string deptGoodsHql, string reaGoodsHql, string sort, int page, int limit);
        /// <summary>
        /// 入库移库,获取库存货品库存数大于0的入库库存记录信息(HQL)
        /// </summary>
        /// <param name="docHql"></param>
        /// <param name="qtyHql"></param>
        /// <param name="reaGoodsHql"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<ReaBmsQtyDtl> SearchReaBmsQtyDtlEntityListOfQtyGEZeroByJoinHql(string where, string inDtlHql, string qtyHql, string reaGoodsHql, string sort, int page, int limit);
        /// <summary>
        /// 入库移库,获取库存货品库存数大于0的入库库存记录信息(HQL)
        /// </summary>
        /// <param name="docHql"></param>
        /// <param name="qtyHql"></param>
        /// <param name="reaGoodsHql"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        IList<ReaBmsQtyDtl> SearchReaBmsQtyDtlListOfQtyGEZeroByJoinHql(string where, string inDtlHql, string qtyHql, string reaGoodsHql, string sort, int page, int limit);

        /// <summary>
        /// 获取货运单号，根据入库明细表里的[供货验收明细单ID]找到Rea_BmsCenSaleDocConfirm_供货验收单表里的货运单号字段TransportNo
        /// </summary>
        /// <param name="SaleDtlConfirmID">供货验收明细单ID</param>
        /// <returns>货运单号</returns>
        string GetTransportNo(long SaleDtlConfirmID);
    }
}