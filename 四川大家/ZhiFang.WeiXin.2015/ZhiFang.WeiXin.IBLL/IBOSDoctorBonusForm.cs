using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.WeiXin.Entity;
using ZhiFang.IBLL.Base;
using ZhiFang.WeiXin.Entity.ViewObject.Response;
using ZhiFang.Entity.Base;
using System.IO;

namespace ZhiFang.WeiXin.IBLL
{
    /// <summary>
    ///
    /// </summary>
    public interface IBOSDoctorBonusForm : IBGenericManager<OSDoctorBonusForm>
    {
        SysWeiXinPayToUser.PayToUser PayToUserFunc { get; set; }
        /// <summary>
        /// 检查奖金记录里是否还有未打款
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool SearchCheckIsUpdatePayed(long id);
        /// <summary>
        /// 通过结算周期获取医生奖金结算单及结算记录明细的结算申请数据
        /// </summary>
        /// <param name="bonusFormRound"></param>
        /// <returns></returns>
        OSDoctorBonusApply SearchSettlementApplyInfoByBonusFormRound(string bonusFormRound, long empID, string empName);
        /// <summary>
        /// 物理删除医生奖金结算单并同时删除医生奖金结算记录,操作记录及附件信息
        /// </summary>
        /// <param name="longOSDoctorBonusFormID">医生奖金结算单Id</param>
        /// <returns></returns>
        BaseResultBool DelOSDoctorBonusFormAndDetails(long longOSDoctorBonusFormID);
        /// <summary>
        /// 批量选择的医生奖金记录检查并打款操作处理
        /// </summary>
        /// <param name="applyEntity"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultBool UpdateOSDoctorBonusListPayStatus(OSDoctorBonusApply applyEntity, long empID, string empName);
        /// <summary>
        /// 新增结算制单
        /// </summary>
        /// <param name="applyEntity"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultDataValue AddOSDoctorBonusFormAndDetails(OSDoctorBonusApply applyEntity, long empID, string empName);
        /// <summary>
        /// 审核流程处理
        /// </summary>
        /// <param name="applyEntity"></param>
        /// <param name="tempArray"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultBool UpdateOSDoctorBonusFormAndDetails(OSDoctorBonusApply applyEntity, string[] tempArray, long empID, string empName);
        BaseResultDataValue ExcelToPdfFile(long id, bool isPreview, string templetName, ref string fileName);
        FileStream GetExportExcelOSDoctorBonusFormDetail(string where, ref string fileName);
    }
}