using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBLisTestItem : IBGenericManager<LisTestItem>
    {
        IList<LisTestItem> QueryLisTestItem(string strHqlWhere, string fields);

        EntityList<LisTestItem> QueryLisTestItem(string strHqlWhere, string order, int start, int count, string fields);

        EntityList<LisTestItem> QueryLisTestItem(string beginSampleNo, string endSampleNo, string strHqlWhere, string order, int start, int count, string fields);

        IList<LisTestItem> QueryLisTestItem(string strHqlWhere);

        EntityList<LisTestItem> QueryLisTestItem(string strHqlWhere, string order, int start, int count);

        bool QueryIsExistTestItemResult(long sectionID, long itemID);

        string QueryCommonItemByTestFormID(string testFormIDList);

        LisTestItem AddLisTestCalcItem(LisTestForm lisTestForm, LisTestItem lisTestItem, LBItem calcItem);

        BaseResultDataValue EditLisTestItemResultByDilution(string testItemID, double? dilutionTimes);

        BaseResultDataValue EditLisTestItemResultByOffset(string testItemOffsetInfo);

        /// <summary>
        /// 编辑并获取项目参考值范围
        /// </summary>
        /// <param name="testForm"></param>
        /// <param name="listTestItem"></param>
        /// <returns></returns>
        BaseResultDataValue EditLisTestItemReferenceValueRange(IBLisTestForm IBLisTestForm, LisTestForm testForm, ref IList<LisTestItem> listTestItem);
    }
}