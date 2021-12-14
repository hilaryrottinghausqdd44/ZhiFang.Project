using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.SA;

namespace ZhiFang.BLL.SA.SAQITypeOfStrategy
{
    /// <summary>
    /// 质量指标分类类型图表的Stategy  
    /// </summary>
    abstract class QITypeOfEChartStrategy
    {
        public abstract BaseResultDataValue GetSAQualityIndicatorTypeOfEChart(IList<SPSAQualityIndicatorType> nfList, string qIndicatorType, string sadimension);

        public abstract IList<SPSAQualityIndicatorType> GetSAQualityIndicatorTypeOfList(IList<SPSAQualityIndicatorType> nfList, string qitype, string sadimension, string startDate, string endDate);
    }
}
