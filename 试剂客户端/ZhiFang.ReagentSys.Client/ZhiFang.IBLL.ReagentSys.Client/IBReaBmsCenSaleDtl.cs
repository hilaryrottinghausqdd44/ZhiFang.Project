using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaConfirm;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaSale;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaBmsCenSaleDtl : Base.IBGenericManager<ReaBmsCenSaleDtl>
    {
        /// <summary>
        /// 供货明细保存的验证处理
        /// </summary>
        /// <param name="dtlSaveList"></param>
        /// <returns></returns>
        BaseResultBool EditSaleDtlListOfValid(IList<ReaBmsCenSaleDtl> dtlSaveList);
        /// <summary>
        /// 供货明细保存的验证处理
        /// </summary>
        /// <param name="dtlVOSaveList"></param>
        /// <returns></returns>
        BaseResultBool EditSaleDtlListOfVOValid(IList<ReaBmsCenSaleDtlVO> dtlVOSaveList);
        /// <summary>
        /// 新增供货明细信息
        /// </summary>
        /// <param name="dtlAddList"></param>
        /// <param name="saleDoc"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultBool AddSaleDtlList(ReaBmsCenSaleDoc saleDoc, IList<ReaBmsCenSaleDtl> dtlAddList, long empID, string empName);
        /// <summary>
        /// 修改供货明细信息
        /// </summary>
        /// <param name="saleDoc"></param>
        /// <param name="dtlEditList"></param>
        /// <returns></returns>
        BaseResultBool UpdateSaleDtlList(ReaBmsCenSaleDoc saleDoc, IList<ReaBmsCenSaleDtl> dtlEditList, string fields, long empID, string empName);
        /// <summary>
        /// 针对订单转供单的供单，一订单对应多供单，在保存供单信息之前，进行校验供货数量
        /// 验证成功会修改订单的状态、已供数量、未供数量
        /// 验证失败不可以进行后续操作，前台直接提示错误信息
        /// </summary>
        /// <param name="saleDoc"></param>
        /// <param name="dtlAddList"></param>
        /// <param name="dtlEditList"></param>
        /// <returns></returns>
        BaseResultBool CompareAndGetOrderDtlQty(ReaBmsCenSaleDoc saleDoc, IList<ReaBmsCenSaleDtl> dtlAddList, IList<ReaBmsCenSaleDtl> dtlEditList);
        BaseResultBool AddDtlListOfVO(ReaBmsCenSaleDoc saleDoc, IList<ReaBmsCenSaleDtlVO> dtlVOAddList, long empID, string empName);
        /// <summary>
        /// 供货验收,获取某一供货单的供货明细集合信息(可验收数大于0)
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="saleDocID"></param>
        /// <param name="order"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<ReaSaleDtlOfConfirmVO> SearchReaBmsCenSaleDtlOfConfirmVOListBySaleDocID(string strHqlWhere, string order, int page, int limit, long saleDocID);
        /// <summary>
        /// 供货审核通过后生成供货条码信息处理
        /// </summary>
        /// <param name="saleDocId"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultBool AddCreateBarcodeInfoOfSaleDocId(long saleDocId, long empID, string empName);
        /// <summary>
        /// 获取供货明细及供货主单信息
        /// </summary>
        /// <param name="docHql"></param>
        /// <param name="dtlHql"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        IList<ReaBmsCenSaleDtl> SearchDtlAdnDocListByHQL(string docHql, string dtlHql, string reaGoodsHql, string sort, int page, int limit);
        /// <summary>
        /// 获取供货明细及供货主单信息
        /// </summary>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<ReaBmsCenSaleDtl> SearchNewEntityListByHQL(string where, string sort, int page, int limit);
        /// <summary>
        /// 供货明细汇总统计
        /// </summary>
        /// <param name="docHql"></param>
        /// <param name="dtlHql"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<ReaBmsCenSaleDtl> SearchReaBmsCenSaleDtlSummaryByHQL(int groupType, string docHql, string dtlHql, string reaGoodsHql, string sort, int page, int limit);
        /// <summary>
        /// 新增客户端提取平台供货商的供货单信息
        /// </summary>
        /// <param name="saleDtlList"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultDataValue AddDtlListOfPlatformExtract(ref IList<ReaBmsCenSaleDtl> saleDtlList, long empID, string empName);
    }
}