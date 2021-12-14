using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.ReaConfirm;

namespace ZhiFang.Digitlab.IBLL.ReagentSys
{
    /// <summary>
    ///
    /// </summary>
    public interface IBBmsCenSaleDocConfirm : IBGenericManager<BmsCenSaleDocConfirm>
    {
        BaseResultDataValue AddBmsCenSaleDocConfirm(IList<BmsCenSaleDtlConfirm> listEntity, string secAccepterID, string secAccepterName, string accepterMemo);

        #region 客户端验收

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
        BaseResultDataValue AddReaSaleDocConfirmOfManualInput(BmsCenSaleDocConfirm entity, IList<BmsCenSaleDtlConfirmVO> dtAddList, int secAccepterType, string codeScanningMode, long empID, string empName);
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
        BaseResultBool EditReaSaleDocConfirmOfManualInput(string[] tempArray, IList<BmsCenSaleDtlConfirmVO> dtAddList, IList<BmsCenSaleDtlConfirmVO> dtEditList, int secAccepterType,string codeScanningMode, long empID, string empName, string fieldsDtl);
        /// <summary>
        /// 对客户端手工验收单进行确认验收
        /// </summary>
        /// <param name="tempArray"></param>
        /// <param name="secAccepterType">验收双确认方式</param>
        /// <param name="codeScanningMode"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultBool EditConfirmOfManualInput(string[] tempArray, int secAccepterType, string codeScanningMode, long empID, string empName);
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
        BaseResultBool EditReaSaleDocConfirmAndDtl(string[] tempArray, int secAccepterType,string codeScanningMode, string fieldsDtl, long empID, string empName);

        /// <summary>
        /// 新增客户端订单验收
        /// </summary>
        /// <param name="dtAddList"></param>
        /// <param name="secAccepterType">验收双确认方式</param>
        /// <param name="codeScanningMode"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultDataValue AddReaSaleDocConfirmOfOrder(IList<BmsCenSaleDtlConfirmVO> dtAddList, int secAccepterType, string codeScanningMode, long empID, string empName);
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
        BaseResultBool EditReaSaleDocConfirmOfOrder(string[] tempArray, IList<BmsCenSaleDtlConfirmVO> dtAddList, IList<BmsCenSaleDtlConfirmVO> dtEditList, int secAccepterType, string codeScanningMode, long empID, string empName, string fieldsDtl); 
        #endregion
    }
}