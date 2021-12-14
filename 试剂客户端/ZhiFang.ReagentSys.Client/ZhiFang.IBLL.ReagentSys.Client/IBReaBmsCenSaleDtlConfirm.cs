using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaConfirm;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaBmsCenSaleDtlConfirm : IBGenericManager<ReaBmsCenSaleDtlConfirm>
    {

        #region 客户端订单验收
        /// <summary>
        /// (验收入库)获取客户端验收明细及验收条码明细信息
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="order"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<ReaSaleDtlConfirmVO> SearchReaDtlConfirmVOOfStoreInByHQL(string strHqlWhere, string order, int page, int limit);

        /// <summary>
        /// 订单/供货单验收,获取某一供货单的供货明细集合信息(可验收数大于0)
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="order"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="confirmType">订单验收:order;供货单验收:sale</param>
        /// <returns></returns>
        EntityList<ReaSaleDtlConfirmVO> SearchBmsCenSaleDtlConfirmVOOfConfirmTypeByHQL(string strHqlWhere, string order, int page, int limit, string confirmType);

        /// <summary>
        /// 新增验收货品明细
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dtAddList"></param>
        /// <returns></returns>
        BaseResultBool AddDtlConfirmOfList(ReaBmsCenSaleDocConfirm entity, IList<ReaSaleDtlConfirmVO> dtAddList, string codeScanningMode, long empID, string empName);
        /// <summary>
        /// 编辑更新验收货品明细
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dtEditList"></param>
        /// <returns></returns>
        BaseResultBool EditDtlConfirmOfList(ReaBmsCenSaleDocConfirm entity, IList<ReaSaleDtlConfirmVO> dtEditList, string fieldsDtl, string codeScanningMode, long empID, string empName);
        /// <summary>
        /// 验货明细的保存验证判断
        /// </summary>
        /// <param name="voDtlList"></param>
        /// <param name="codeScanningMode">扫码模式(严格模式:strict,混合模式：mixing)</param>
        /// <returns></returns>
        BaseResultBool EditBmsCenSaleDtlConfirmValid(IList<ReaSaleDtlConfirmVO> voDtlList, string codeScanningMode);
        /// <summary>
        /// 订单验收保存前验证处理
        /// </summary>
        /// <param name="voDtlList"></param>
        /// <param name="codeScanningMode">扫码模式(严格模式:strict,混合模式：mixing)</param>
        /// <returns></returns>
        BaseResultBool EditBmsCenSaleDtlConfirmValidOfOrder(IList<ReaSaleDtlConfirmVO> voDtlList, string codeScanningMode);

        /// <summary>
        /// 订单验收,选择某一订单判断订单是否可以新增验收或继续验收
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        BaseResultBool SearchOrderIsConfirmOfByOrderId(long orderId, long confirmId);
        /// <summary>
        /// 供货验收保存前验证处理
        /// </summary>
        /// <param name="voDtlList"></param>
        /// <param name="codeScanningMode">扫码模式(严格模式:strict,混合模式：mixing)</param>
        /// <returns></returns>
        BaseResultBool EditBmsCenSaleDtlConfirmValidOfSale(IList<ReaSaleDtlConfirmVO> voDtlList, string codeScanningMode);
        /// <summary>
        /// 供货单验收,选择某一供货单判断供货单是否可以新增验收或继续验收
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        BaseResultBool SearchReaSaleIsConfirmOfBySaleDocID(long saleDocID, long confirmId);
        /// <summary>
        /// 客户端验收明细删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="confirmSourceType"></param>
        /// <returns></returns>
        BaseResultBool DelReaBmsCenSaleDtlConfirm(long id, string confirmSourceType, long empID, string empName);
        #endregion

        /// <summary>
        /// 关联货品表查询供货验收明细
        /// </summary>
        EntityList<ReaBmsCenSaleDtlConfirm> GetReaBmsCenSaleDtlConfirmListByHql(string strHqlWhere, string sort, int page, int limit);
    }
}