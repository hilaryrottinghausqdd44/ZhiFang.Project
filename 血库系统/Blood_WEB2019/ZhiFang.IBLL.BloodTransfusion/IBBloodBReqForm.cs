

using System.Collections.Generic;
using System.IO;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.BloodTransfusion
{
    /// <summary>
    ///
    /// </summary>
    public interface IBBloodBReqForm : IBGenericManager<BloodBReqForm, string>
    {
        #region 医生站
        IList<BloodBReqForm> SearchBloodBReqFormListByHql(string where, string sort, int page, int limit);
        EntityList<BloodBReqForm> SearchBloodBReqFormEntityListByHql(string where, string sort, int page, int limit);
        IList<BloodBReqForm> SearchBloodBReqFormListByJoinHql(string where, string sort, int page, int limit);
        EntityList<BloodBReqForm> SearchBloodBReqFormEntityListByJoinHql(string where, string sort, int page, int limit);
        /// <summary>
        /// 定制新增医嘱申请主单信息及申请明细信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="bloodsConfigVO"></param>
        /// <param name="curDoctor"></param>
        /// <param name="addBreqItemList"></param>
        /// <param name="addResultList"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultDataValue AddBloodBReqFormAndDtl(BloodBReqForm entity, BloodsConfig bloodsConfigVO, SysCurUserInfo curDoctor, IList<BloodBReqItem> addBreqItemList, IList<BloodBReqFormResult> addResultList, long empID, string empName);
        /// <summary>
        /// 定制修改医嘱申请主单信息及申请明细信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tempArray"></param>
        /// <param name="addBreqItemList"></param>
        /// <param name="editBreqItemList"></param>
        /// <param name="addResultList"></param>
        /// <param name="editResultList"></param>
        /// <returns></returns>
        BaseResultBool EditBloodBReqFormAndDtlByField(ref BloodBReqForm entity, BloodsConfig bloodsConfigVO, SysCurUserInfo curDoctor, string[] tempArray, IList<BloodBReqItem> addBreqItemList, IList<BloodBReqItem> editBreqItemList, IList<BloodBReqFormResult> addResultList, IList<BloodBReqFormResult> editResultList, long empID, string empName);
        /// <summary>
        /// 按医嘱申请单号进行医嘱确认提交操作
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tempArray"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultBool EditBloodBReqFormOfConfirmApplyByReqFormId(BloodBReqForm entity, BloodsConfig bloodsConfigVO, string[] tempArray, long empID, string empName);
        /// <summary>
        /// 医嘱申请打印
        /// </summary>
        /// <param name="reaReportClass"></param>
        /// <param name="id"></param>
        /// <param name="labID"></param>
        /// <param name="labCName"></param>
        /// <param name="breportType"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <param name="frx"></param>
        /// <param name="pdfFileName"></param>
        /// <returns></returns>
        Stream SearchPdfReportOfTypeById(string reaReportClass, string id, long labID, string labCName, long empID, string empName, string breportType, string frx, ref string pdfFileName);
        BaseResultBool SearchBusinessReportOfPDFJSById(string reaReportClass, string id, long labID, string labCName, long empID, string empName, string breportType, string frx, ref string pdfFileName);
        BaseResultDataValue SearchImageReportToBase64String(string reaReportClass, string id, long labID, string labCName, long empID, string empName, string breportType, string frx, ref string fileName);
        /// <summary>
        /// 物理删除医嘱申请单信息
        /// </summary>
        /// <param name="reqFormId"></param>
        /// <returns></returns>
        BaseResultBool DeleteBloodBReqForm(string reqFormId);
        /// <summary>
        /// 更新用血申请的打印总数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        BaseResultBool UpdateBloodBReqFormPrintTotalById(string id);
        #endregion

        #region 护士站
        /// <summary>
        /// 通过血袋号)获取输血申请综合查询的BloodBReqForm(HQL)
        /// </summary>
        /// <param name="wardId">病区编码</param>
        /// <param name="where"></param>
        /// <param name="bbagCode"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
		EntityList<BloodBReqForm> SearchBloodBReqFormListByBBagCodeAndHql(string wardId, string where, string scanCodeField,string bbagCode, string sort, int page, int limit);

        #endregion

        /// <summary>
        /// 按申请信息建立病区与科室关系
        /// </summary>
        /// <returns></returns>
        BaseResultBool AddWarpAndDept();
    }
}