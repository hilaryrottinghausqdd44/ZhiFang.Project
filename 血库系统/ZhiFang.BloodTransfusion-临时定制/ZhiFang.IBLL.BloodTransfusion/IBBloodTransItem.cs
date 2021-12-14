

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.BloodTransfusion
{
    /// <summary>
    ///
    /// </summary>
    public interface IBBloodTransItem : IBGenericManager<BloodTransItem>
    {
        /// <summary>
        /// 通过输血记录主单Id获取输血记录的病人体征信息
        /// </summary>
        /// <param name="transFormId"></param>
        /// <returns></returns>
        EntityList<BloodTransItem> SearchBloodTransItemListByTransFormId(long transFormId);
        /// <summary>
        /// (输血过程记录登记)通过输血过程内容分类ID或输血过程记录主单ID获取临床处理结果/临床处理结果描述信息
        /// </summary>
        /// <param name="contentTypeId">5:临床处理结果;6:临床处理结果描述;</param>
        /// <param name="transFormId">输血过程记录主单Id</param>
        /// <returns></returns>
        BloodTransItem GetBloodTransItemByContentTypeID(long contentTypeId, string transFormId);

        /// <summary>
        /// 新增发血血袋输血记录项(病人体征信息)
        /// </summary>
        /// <param name="brdv"></param>
        /// <param name="transForm"></param>
        /// <param name="transfusionAntriesList"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        void AddTransfusionAntriesList(ref BaseResultDataValue brdv, BloodTransForm transForm, IList<BloodTransItem> transfusionAntriesList, long empID, string empName);
        /// <summary>
        /// 新增发血血袋不良反应选择项集合信息
        /// </summary>
        /// <param name="brdv"></param>
        /// <param name="transForm"></param>
        /// <param name="adverseReactionList"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        void AddAdverseReactionList(ref BaseResultDataValue brdv, BloodTransForm transForm, IList<BloodTransItem> adverseReactionList, long empID, string empName);
        /// <summary>
        /// 新增发血血袋临床处理措施集合信息
        /// </summary>
        /// <param name="brdv"></param>
        /// <param name="transForm"></param>
        /// <param name="clinicalMeasuresList"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        void AddClinicalMeasuresList(ref BaseResultDataValue brdv, BloodTransForm transForm, IList<BloodTransItem> clinicalMeasuresList, long empID, string empName);
        /// <summary>
        /// 新增发血血袋临床处理结果信息
        /// </summary>
        /// <param name="brdv"></param>
        /// <param name="transForm"></param>
        /// <param name="clinicalResults"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        void AddClinicalResults(ref BaseResultDataValue brdv, BloodTransForm transForm, BloodTransItem clinicalResults, long empID, string empName);
        /// <summary>
        /// 新增发血血袋临床处理结果描述信息
        /// </summary>
        /// <param name="brdv"></param>
        /// <param name="transForm"></param>
        /// <param name="clinicalResultsDesc"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        void AddClinicalResultsDesc(ref BaseResultDataValue brdv, BloodTransForm transForm, BloodTransItem clinicalResultsDesc, long empID, string empName);

        /// <summary>
        /// 更新发血血袋输血记录项(病人体征信息)
        /// </summary>
        /// <param name="brdv"></param>
        /// <param name="transForm"></param>
        /// <param name="transfusionAntriesList"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        void EditTransfusionAntriesList(ref BaseResultDataValue brdv, BloodTransForm transForm, IList<BloodTransItem> transfusionAntriesList, long empID, string empName);
        /// <summary>
        /// 更新发血血袋临床处理结果信息
        /// </summary>
        /// <param name="brdv"></param>
        /// <param name="transForm"></param>
        /// <param name="clinicalResults"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        void EditClinicalResults(ref BaseResultDataValue brdv, BloodTransForm transForm, BloodTransItem clinicalResults, long empID, string empName);
        /// <summary>
        /// 更新发血血袋临床处理结果描述信息
        /// </summary>
        /// <param name="brdv"></param>
        /// <param name="transForm"></param>
        /// <param name="clinicalResultsDesc"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        void EditClinicalResultsDesc(ref BaseResultDataValue brdv, BloodTransForm transForm, BloodTransItem clinicalResultsDesc, long empID, string empName);

        #region 批量修改录入
        /// <summary>
        /// (批量修改录入)通过选择的多个发血明细Id(多血袋)获取输血过程记录病人体征登记信息
        /// </summary>
        /// <param name="outDtlIdStr"></param>
        /// <returns></returns>
        EntityList<BloodTransItem> SearchPatientSignsByOutDtlIdStr(string outDtlIdStr);

        /// <summary>
        /// (批量修改录入)通过选择的多个发血明细Id(多血袋)获取输血过程记录的某一不良反应分类的不良反应选择项信息
        /// </summary>
        /// <param name="outDtlIdStr">发血血袋明细记录ID的字符串(123,1234)</param>
        /// <param name="recordTypeId">某一不良反应分类ID</param>
        /// <returns></returns>
        EntityList<BloodTransItem> SearchAdverseReactionOptionsByOutDtlIdStr(string outDtlIdStr, long recordTypeId, string where, ref int batchSign);

        /// <summary>
        /// (批量修改录入)通过选择的多个发血明细Id(多血袋)获取输血过程记录的临床处理措施信息
        /// </summary>
        /// <param name="outDtlIdStr">发血血袋明细记录ID的字符串(123,1234)</param>
        /// <param name="where">where</param>
        /// <returns></returns>
        EntityList<BloodTransItem> SearchClinicalMeasuresByOutDtlIdStr(string outDtlIdStr, string where, ref int batchSign);

        /// <summary>
        /// (批量修改录入)通过选择的多个发血明细Id(多血袋)获取输血过程记录临床处理结果登记信息
        /// </summary>
        /// <param name="outDtlIdStr"></param>
        /// <returns></returns>
        BloodTransItem SearchClinicalResultsByOutDtlIdStr(string outDtlIdStr, string where, ref int batchSign);

        /// <summary>
        /// (批量修改录入)通过选择的多个发血明细Id(多血袋)获取输血过程记录临床处理结果描述登记信息
        /// </summary>
        /// <param name="outDtlIdStr"></param>
        /// <returns></returns>
        BloodTransItem SearchClinicalResultsDescByOutDtlIdStr(string outDtlIdStr, string where, ref int batchSign);
        /// <summary>
        /// 按选择的发血血袋明细ID,批量删除某一不良反应分类的所有不良反应症状记录信息
        /// </summary>
        /// <param name="outDtlIdStr">发血血袋明细记录ID的字符串(123,1234)</param>
        /// <param name="recordTypeId">某一不良反应分类ID</param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultBool DelBatchTransItemByAdverseReactions(string outDtlIdStr, long recordTypeId, long empID, string empName);
        /// <summary>
        /// 按选择的发血血袋明细ID,批量删除其所有的临床处理结果记录信息", Desc = "按选择的发血血袋明细ID,批量删除其所有的临床处理措施记录信息
        /// </summary>
        /// <param name="outDtlIdStr">发血血袋明细记录ID的字符串(123,1234)</param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultBool DelBatchTransItemByClinicalMeasures(string outDtlIdStr, long empID, string empName);

        /// <summary>
        /// (批量修改录入)更新输血记录项的病人体征信息
        /// </summary>
        /// <param name="editTransForm"></param>
        /// <param name="transfusionAntriesList"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultDataValue EditBatchTransfusionAntriesByOutDtlList(BloodTransForm editTransForm, IList<BloodTransItem> transfusionAntriesList, long empID, string empName);
        /// <summary>
        /// (批量修改录入)更新输血记录的不良反应症状信息
        /// </summary>
        /// <param name="editTransForm"></param>
        /// <param name="adverseReactionList"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultDataValue EditBatchAdverseReactionByOutDtlList(BloodTransForm editTransForm, IList<BloodTransItem> adverseReactionList, long empID, string empName);
        /// <summary>
        /// (批量修改录入)更新输血记录的临床处理措施信息
        /// </summary>
        /// <param name="editTransForm"></param>
        /// <param name="clinicalMeasuresList"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultDataValue EditBatchClinicalMeasuresByOutDtlList(BloodTransForm editTransForm, IList<BloodTransItem> clinicalMeasuresList, long empID, string empName);
        /// <summary>
        /// (批量修改录入)更新输血记录的临床处理结果信息
        /// </summary>
        /// <param name="editTransForm"></param>
        /// <param name="clinicalResults"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultDataValue EditBatchClinicalResultsByOutDtlListr(BloodTransForm editTransForm, BloodTransItem clinicalResults, long empID, string empName);

        /// <summary>
        /// (批量修改录入)更新输血记录的临床处理结果描述信息
        /// </summary>
        /// <param name="editTransForm"></param>
        /// <param name="clinicalResultsDesc"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultDataValue EditBatchClinicalResultsDescByOutDtlList(BloodTransForm editTransForm, BloodTransItem clinicalResultsDesc, long empID, string empName);
        
        #endregion

    }
}