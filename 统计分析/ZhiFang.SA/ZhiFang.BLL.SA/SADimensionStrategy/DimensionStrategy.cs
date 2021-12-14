using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.SA;

namespace ZhiFang.BLL.SA.SADimensionStrategy
{
    /// <summary>
    /// 质量指标分类类型-统计纬度的Stategy  
    /// </summary>
    abstract class DimensionStrategy
    {
        public abstract IList<SPSAQualityIndicatorType> GetSPSAQualityIndicatorTypeOfGroupBy(IList<SPSAQualityIndicatorType> nfList, string qIndicatorType);
    }
}
