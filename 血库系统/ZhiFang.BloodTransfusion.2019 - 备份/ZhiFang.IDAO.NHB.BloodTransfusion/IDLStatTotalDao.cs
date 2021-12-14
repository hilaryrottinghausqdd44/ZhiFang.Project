using ZhiFang.IDAO.Base;
using ZhiFang.Entity.BloodTransfusion;
using System.Collections.Generic;

namespace ZhiFang.IDAO.NHB.BloodTransfusion
{
    public interface IDLStatTotalDao : IDBaseDao<LStatTotal, long>
    {
        /// <summary>
        /// 获取质量指标类型统计原始数据源
        /// </summary>
        /// <param name="searchEntity"></param>
        /// <param name="strWhere">""</param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        IList<SPSAQualityIndicatorType> SearchSPSAQualityIndicatorTypeList(SPSAQualityIndicatorType searchEntity, string where, string order, int page, int count);

    }
}