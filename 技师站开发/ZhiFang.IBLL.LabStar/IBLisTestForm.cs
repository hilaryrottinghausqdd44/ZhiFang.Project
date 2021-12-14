using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.Entity.LabStar.ViewObject.Response;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBLisTestForm : IBGenericManager<LisTestForm>
    {
        SysCookieValue SysCookieValue { get; set; }

        BaseResultDataValue CreateNewSampleNoByOldSampleNo(long sectionID, string testDate, string curSampleNo);

        BaseResultDataValue QueryNextSampleNoByCurSampleNo(string curSampleNo);

        IList<string> BatchCreateNewSampleNoByOldSampleNo(string curSampleNo, int SampleNoCount);

        IList<LisTestForm> QueryLisTestForm(string strHqlWhere, string fields);

        EntityList<LisTestForm> QueryLisTestForm(string strHqlWhere, string order, int start, int count, string fields);

        EntityPageList<LisTestForm> QueryLisTestFormCurPage(long id, string strHqlWhere, string order, int start, int count, string fields);

        EntityList<LisTestForm> QueryLisTestFormBySampleNo(string beginSampleNo, string endSampleNo, string strHqlWhere, string order, int start, int count, string fields);

        EntityList<LisTestForm> QueryLisTestFormAndItemNameList(string beginSampleNo, string endSampleNo, string strHqlWhere, string order, int start, int count, string fields, int isOrderItem, int itemNameType);

        EntityList<LisTestForm> QueryWillConfirmLisTestForm(string beginSampleNo, string endSampleNo, string itemResultFlag, string strHqlWhere, string order, int start, int count, string fields);

        BaseResultDataValue AddSingleLisTestForm(LisTestForm testForm, IList<LisTestItem> listTestItem, bool isCreateSampleNo);

        BaseResultDataValue AddBatchLisTestForm(string sampleInfo, string testDate, long sectionID, string startSampleNo, int sampleCount);

        BaseResultDataValue UpdateBatchLisTestForm(IList<string[]> listArray);

        BaseResultDataValue LisTestFormEditByField(LisTestForm testForm, string testFormFields, string patientFields);

        BaseResultDataValue EditBatchLisTestItemResult(long testFormID, IList<LisTestItem> listTestItemResult, string testItemFileds);

        BaseResultDataValue DeleteBatchLisTestForm(string delIDList, bool isReceiveDelete, bool isResultDelete);

        BaseResultDataValue QueryLisTestFormIsDelete(string delIDList);

        BaseResultDataValue EditLisTestFormTestTime(long testFormID);

        BaseResultDataValue EditLisTestFormTestTimeByDelTestItem(long testFormID);

        BaseResultDataValue EditLisTestItemAfterTreatment(long testFormID);

        BaseResultDataValue EditLisTestItemAfterTreatment(LisTestForm testForm, IList<LisTestItem> listTestItem);

        BaseResultDataValue AddBatchLisTestItem(long testFormID, IList<LisTestItem> listAddTestItem, string testItemFileds, bool isRepPItem);

        BaseResultDataValue AddBatchLisTestItemResult(long testFormID, IList<LisTestItem> listAddTestItem, bool isAddItem);

        BaseResultDataValue AddBatchLisTestItemResult(IList<LisTestItem> listAddTestItem, bool isAddItem);

        BaseResultDataValue DeleteBatchLisTestItem(long testFormID, string delIDList);

        BaseResultDataValue DeleteBatchLisTestItem(long testItemID);

        /// <summary>
        /// 删除检验单项目
        /// </summary>
        /// <param name="testFormID">检验单ID</param>
        /// <param name="itemIDList">检验项目单ID</param>
        /// <param name="isDelNoResultItem">仅删除结果为空的项目</param>
        /// <param name="isDelNoOrderItem">仅删除非医嘱项目</param>
        /// <returns></returns>
        BaseResultDataValue DeleteBatchLisTestItem(long testFormID, string itemIDList, bool isDelNoResultItem, bool isDelNoOrderItem);

        BaseResultDataValue EditBatchLisTestFormDeleteCancel(long testFormID);

        BaseResultDataValue GetLisTestFormCalcItem(long testFormID);

        /// <summary>
        /// 判断检验单中计算项目是否需要删除
        /// </summary>
        /// <param name="testFormID">检验单ID</param>
        /// <returns></returns>
        BaseResultDataValue DeleteLisTestFormCalcItem(long testFormID);

        /// <summary>
        /// 检验样本合并
        /// </summary>
        /// <param name="fromTestFormID">检验源样本ID</param>
        /// <param name="toTestForm">检验目标样本</param>
        /// <param name="strFromTestItemID">检验项目信息ID（注意不是项目ID，是LisTestItem的ID）</param>
        /// <param name="mergeType">合并类型（1 只合并样本信息，2 只合并样本项目信息，3 合并样本和样本项目信息）</param>
        /// <param name="isSampleNoMerge">是否合并样本号相关信息</param>
        /// <param name="isSerialNoMerge">是否合并条码相关信息</param>
        /// <param name="isDelFormTestItem">合并后是否删除源样本项目</param>
        /// <param name="isDelFormTestForm">合并后源样本下项目为空时，是否删除源样本</param>
        /// <returns></returns>
        BaseResultDataValue EditLisTestFormInfoMerge(long fromTestFormID, LisTestForm toTestForm, string strTestItemID, int mergeType, bool isSampleNoMerge, bool isSerialNoMerge, bool isDelFormTestItem, bool isDelFormTestForm);

        EntityList<LBMergeItemFormVO> QueryItemMergeFormInfo(long itemID, string beginDate, string endDate);

        EntityList<LBMergeItemVO> QueryItemMergeItemInfo(long itemID, string patNo, string cName, string beginDate, string endDate, string isMerge);

        BaseResultDataValue EditMergeItemInfo(long toTestFormID, string strLisTestItemID);

        BaseResultDataValue EditMergeItemInfo(IList<LBMergeItemVO> listLBMergeItemVO);

        BaseResultDataValue EditLisTestFormZFSysCheckStatus(long testFormID, int checkFlag, string checkInfo);

        BaseResultDataValue EditLisTestFormBatchCheckByTestFormID(string testFormIDList, long empID, string empName, string memoInfo);

        BaseResultDataValue EditLisTestFormCheckByTestFormID(long testFormID, long empID, string empName, string memoInfo);

        BaseResultDataValue EditLisTestFormCheckCancelByTestFormID(long testFormID, long empID, string empName, string memoInfo);

        BaseResultDataValue EditLisTestFormBatchConfirmByTestFormID(string testFormIDList, long empID, string empName, long? mainTesterId, string mainTester, string memoInfo);

        BaseResultDataValue EditLisTestFormConfirmByTestFormID(long testFormID, long empID, string empName, long? mainTesterId, string mainTester, string memoInfo, bool isCheckTestFormInfo);

        BaseResultDataValue EditLisTestFormConfirmCancelByTestFormID(long testFormID, string memoInfo);

        BaseResultDataValue EditLisTestFormSampleNoByTargetSampleNo(long sectionID, string curTestDate, string curMinSampleNo, int sampleCount, string targetTestDate, string targetMinSampleNo);

        BaseResultDataValue EditLisTestFormReCheckCancel(string testFormIDList, bool isClearRedoDesc, bool isClearRedoValues, string memoInfo);

        BaseResultDataValue EditLisTestItemReCheck(long testFormID, IList<LisTestItem> listReCheckTestItem, string memoInfo);

        BaseResultDataValue EditLisTestFormReCheckCancel(long testFormID, bool isClearRedoDesc, bool isClearRedoValues, string memoInfo);

        BaseResultDataValue EditLisTestItemReCheckCancel(long testFormID, string testItemIDList, bool isClearRedoDesc, bool isClearRedoValues, string memoInfo);

        BaseResultDataValue EditDisposeLisTestItemPanicValue(string testFormMsgIDList, int msgSendFlag, string msgSendInfo, SysCookieValue sysCookieValue);

        BaseResultDataValue EditPanicValuePhoneCallInfo(string testFormMsgIDList, int phoneCallFlag, string phoneNumber, string phoneReceiver, string phoneCallInfo, SysCookieValue sysCookieValue);

        BaseResultDataValue EditPanicValueReadInfo(string testFormMsgIDList, SysCookieValue sysCookieValue);

        BaseResultDataValue AddTestFormByQuickReceive(string barCode, long sectionID, string strReceiveDate, string sampleNo);

        BaseResultBool CheckSampleConvertStatus(long testFormID, long QCMatID);

        /// <summary>
        /// 更新检验单打印次数
        /// </summary>
        /// <param name="testFormID">检验单ID</param>
        /// <returns></returns>
        BaseResultDataValue EditLisTestFormPrintCount(string testFormID);

        BaseResult SampleConvertQCMaterial(long testFormID, long qCMatID);

        EntityList<LisTestItemVO> DZQueryLisTestItem(long TestFormId);

        #region 历史对比

        BaseResultDataValue EditLisTestItemResultHistoryCompareByTestFormID(long testFormID);

        EntityList<LisTestItem> EditLisTestItemResultHistoryCompareByTestItem(LisTestForm testForm, ref IList<LisTestItem> listTestItem);

        #endregion

    }
}