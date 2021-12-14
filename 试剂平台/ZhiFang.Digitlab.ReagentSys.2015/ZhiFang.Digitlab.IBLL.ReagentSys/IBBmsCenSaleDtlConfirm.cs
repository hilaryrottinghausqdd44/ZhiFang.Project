using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.ReaConfirm;
using ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.ReaGoodsScanCode;
using ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.Response;

namespace ZhiFang.Digitlab.IBLL.ReagentSys
{
    /// <summary>
    ///
    /// </summary>
    public interface IBBmsCenSaleDtlConfirm : IBGenericManager<BmsCenSaleDtlConfirm>
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
        EntityList<BmsCenSaleDtlConfirmVO> SearchReaDtlConfirmVOOfStoreInByHQL(string strHqlWhere, string order, int page, int limit);
        /// <summary>
        /// (验收)获取客户端验收明细及验收条码明细信息
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="order"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<BmsCenSaleDtlConfirmVO> SearchBmsCenSaleDtlConfirmVOOfConfirmByHQL(string strHqlWhere, string order, int page, int limit);
        /// <summary>
        /// 客户端订单验收定制(过滤已验收的验收货品明细)
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="order"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<BmsCenSaleDtlConfirmVO> SearchBmsCenSaleDtlConfirmVOOfOrderByHQL(string strHqlWhere, string order, int page, int limit, string confirmType);

        /// <summary>
        /// 新增验收货品明细
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dtAddList"></param>
        /// <returns></returns>
        BaseResultBool AddDtlConfirmOfList(BmsCenSaleDocConfirm entity, IList<BmsCenSaleDtlConfirmVO> dtAddList, string codeScanningMode, long empID, string empName);
        /// <summary>
        /// 编辑更新验收货品明细
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dtEditList"></param>
        /// <returns></returns>
        BaseResultBool EditDtlConfirmOfList(BmsCenSaleDocConfirm entity, IList<BmsCenSaleDtlConfirmVO> dtEditList, string fieldsDtl, string codeScanningMode, long empID, string empName);
        /// <summary>
        /// 验货明细的保存验证判断
        /// </summary>
        /// <param name="voDtlList"></param>
        /// <returns></returns>
        BaseResultBool EditBmsCenSaleDtlConfirmValid(IList<BmsCenSaleDtlConfirmVO> voDtlList, string codeScanningMode);
        /// <summary>
        /// 订单验收保存前验证处理
        /// </summary>
        /// <param name="voDtlList"></param>
        /// <returns></returns>
        BaseResultBool EditBmsCenSaleDtlConfirmValidOfOrder(IList<BmsCenSaleDtlConfirmVO> voDtlList, string codeScanningMode);
        /// <summary>
        /// 客户端验收明细删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="confirmSourceType"></param>
        /// <returns></returns>
        BaseResultBool DelReaBmsCenSaleDtlConfirm(long id, string confirmSourceType, long empID, string empName);
        /// <summary>
        /// 判断订单是否可以新增验收或继续验收
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        BaseResultBool SearchOrderIsConfirmOfByOrderId(long orderId);
        #endregion
    }
}