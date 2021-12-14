using System;
using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaConfirm;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaBmsCenSaleDocConfirm : IBGenericManager<ReaBmsCenSaleDocConfirm>
    {
        BaseResultBool EditOfJudgeIsSameOrg(int secAccepterType, string compID, string secAccepterAccount, string secAccepterPwd, RBACUser rbacUser);
        /// <summary>
        /// 新增客户端手工验收
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dtAddList"></param>
        /// <param name="secAccepterType">验收双确认方式</param>
        /// <param name="codeScanningMode"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultDataValue AddReaSaleDocConfirmOfManualInput(ReaBmsCenSaleDocConfirm entity, IList<ReaSaleDtlConfirmVO> dtAddList, int secAccepterType, string codeScanningMode, long empID, string empName);
        /// <summary>
        /// 编辑更新客户端手工验收信息
        /// </summary>
        /// <param name="tempArray"></param>
        /// <param name="dtAddList"></param>
        /// <param name="dtEditList"></param>
        /// <param name="secAccepterType">验收双确认方式</param>
        /// <param name="codeScanningMode"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <param name="fieldsDtl"></param>
        /// <returns></returns>
        BaseResultBool EditReaSaleDocConfirmOfManualInput(string[] tempArray, IList<ReaSaleDtlConfirmVO> dtAddList, IList<ReaSaleDtlConfirmVO> dtEditList, int secAccepterType, string codeScanningMode, long empID, string empName, string fieldsDtl);

        /// <summary>
        /// 新增客户端订单验收
        /// </summary>
        /// <param name="dtAddList"></param>
        /// <param name="secAccepterType">验收双确认方式</param>
        /// <param name="codeScanningMode"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultDataValue AddReaSaleDocConfirmOfOrder(IList<ReaSaleDtlConfirmVO> dtAddList, int secAccepterType, string codeScanningMode, long empID, string empName);
        /// <summary>
        /// 编辑更新客户端订单验收
        /// </summary>
        /// <param name="tempArray"></param>
        /// <param name="dtAddList"></param>
        /// <param name="dtEditList"></param>
        /// <param name="secAccepterType">验收双确认方式</param>
        /// <param name="codeScanningMode"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <param name="fieldsDtl"></param>
        /// <returns></returns>
        BaseResultBool EditReaSaleDocConfirmOfOrder(string[] tempArray, IList<ReaSaleDtlConfirmVO> dtAddList, IList<ReaSaleDtlConfirmVO> dtEditList, int secAccepterType, string codeScanningMode, long empID, string empName, string fieldsDtl);

        /// <summary>
        /// 对验收单进行确认验收操作
        /// </summary>
        /// <param name="tempArray"></param>
        /// <param name="secAccepterType">验收双确认方式</param>
        /// <param name="codeScanningMode"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultBool EditReaBmsCenSaleDocConfirmOfConfirmType(string[] tempArray, int secAccepterType, string codeScanningMode, long empID, string empName, string confirmType);
        /// <summary>
        /// 更新客户端验收信息
        /// </summary>
        /// <param name="tempArray"></param>
        /// <param name="secAccepterType">验收双确认方式</param>
        /// <param name="codeScanningMode"></param>
        /// <param name="fieldsDtl"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultBool EditReaSaleDocConfirmAndDtl(string[] tempArray, IList<ReaBmsCenSaleDtlConfirm> dtEditList, int secAccepterType, string codeScanningMode, string fieldsDtl, long empID, string empName);
        void AddReaCheckInOperation(ReaBmsCenSaleDocConfirm entity, long empID, string empName);

        BaseResultDataValue AddReaSaleDocConfirmOfSale(IList<ReaSaleDtlConfirmVO> dtAddList, int secAccepterType, string codeScanningMode, long empID, string empName);
        BaseResultBool EditReaSaleDocConfirmOfSale(string[] tempArray, IList<ReaSaleDtlConfirmVO> dtAddList, IList<ReaSaleDtlConfirmVO> dtEditList, int secAccepterType, string codeScanningMode, long empID, string empName, string fieldsDtl);

    }
}