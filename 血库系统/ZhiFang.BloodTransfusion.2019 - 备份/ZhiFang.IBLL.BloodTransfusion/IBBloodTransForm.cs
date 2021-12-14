

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.BloodTransfusion
{
    /// <summary>
    ///
    /// </summary>
    public interface IBBloodTransForm : IBGenericManager<BloodTransForm>
    {
        /// <summary>
        /// 新增输血记录过程登记(支持批量登记)
        /// </summary>
        /// <param name="outDtlList">发血血袋集合信息</param>
        /// <param name="transForm">各发血血袋输血记录基本信息</param>
        /// <param name="transfusionAntriesList">发血血袋输血记录项(病人体征信息)集合</param>
        /// <param name="adverseReactionList">发血血袋不良反应选择项集合</param>
        /// <param name="clinicalMeasuresList">发血血袋临床处理措施集合</param>
        /// <param name="clinicalResults">发血血袋临床处理结果信息</param>
        /// <param name="clinicalResultsDesc">发血血袋临床处理结果描述信息</param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultDataValue AddBloodTransFormAndDtlList(IList<BloodBOutItem> outDtlList, BloodTransForm transForm, IList<BloodTransItem> transfusionAntriesList, IList<BloodTransItem> adverseReactionList, IList<BloodTransItem> clinicalMeasuresList, BloodTransItem clinicalResults, BloodTransItem clinicalResultsDesc, long empID, string empName);

        /// <summary>
        /// 更新发血血袋的输血记录过程信息(仅支持单个发血血袋)
        /// </summary>
        /// <param name="transForm">发血血袋的待更新输血记录主单信息</param>
        /// <param name="transfusionAntriesList">发血血袋的输血记录项(病人体征信息)集合</param>
        /// <param name="adverseReactionList">发血血袋新增的不良反应选择项集合</param>
        /// <param name="clinicalMeasuresList">发血血袋新增的临床处理措施集合</param>
        /// <param name="clinicalResults">发血血袋的待更新临床处理结果信息</param>
        /// <param name="clinicalResultsDesc">发血血袋的待更新临床处理结果描述信息</param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultDataValue EditBloodTransFormAndDtlList(BloodTransForm transForm, IList<BloodTransItem> transfusionAntriesList, IList<BloodTransItem> adverseReactionList, IList<BloodTransItem> clinicalMeasuresList, BloodTransItem clinicalResults, BloodTransItem clinicalResultsDesc, long empID, string empName);

        /// <summary>
        /// (批量修改)通过选择的多个发血明细Id(多血袋)获取输血过程记录基本登记信息
        /// </summary>
        /// <param name="outDtlIdStr"></param>
        /// <returns></returns>
        BloodTransForm SearchBloodTransFormByOutDtlIdStr(string outDtlIdStr,ref int batchSign);
        /// <summary>
        /// (分步批量登记(分为输血结束前登记及输血结束登记))
        /// (批量修改)输血记录过程登记信息
        /// </summary>
        /// <param name="outDtlList">发血血袋集合信息</param>
        /// <param name="transForm">各发血血袋输血记录基本信息</param>
        /// <param name="transfusionAntriesList">发血血袋输血记录项(病人体征信息)集合</param>
        /// <param name="adverseReactionList">发血血袋不良反应选择项集合</param>
        /// <param name="clinicalMeasuresList">发血血袋临床处理措施集合</param>
        /// <param name="clinicalResults">发血血袋临床处理结果信息</param>
        /// <param name="clinicalResultsDesc">发血血袋临床处理结果描述信息</param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <param name="transAddType">输血登记方式:1:不区分输血结束前及输血结束的批量登记;2:输血结束前批量登记;3:输血结束批量登记(记录项结果值按本次提交进行覆盖更新)</param>
        /// <returns></returns>
        BaseResultDataValue EditBatchTransFormAndDtlListByOutDtlList(IList<BloodBOutItem> outDtlList, BloodTransForm transForm, IList<BloodTransItem> transfusionAntriesList, IList<BloodTransItem> adverseReactionList, IList<BloodTransItem> clinicalMeasuresList, BloodTransItem clinicalResults, BloodTransItem clinicalResultsDesc, long empID, string empName, string transAddType);

    }
}