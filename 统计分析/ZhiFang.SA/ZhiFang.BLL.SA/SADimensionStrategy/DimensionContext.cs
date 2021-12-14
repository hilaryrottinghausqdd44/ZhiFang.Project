using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.SA;

namespace ZhiFang.BLL.SA.SADimensionStrategy
{
    /// <summary>
    /// 质量指标分类类型统计纬度的Context
    /// </summary>
    class DimensionContext
    {
        private IList<SPSAQualityIndicatorType> _nfList = new List<SPSAQualityIndicatorType>();
        private string _qIndicatorType = "";
        private DimensionStrategy dimensionStrategy = null;  //对象组合

        /// <summary>
        /// 获取List集合使用
        /// </summary>
        /// <param name="nfList"></param>
        /// <param name="qIndicatorType">质量指标类型:QualityIndicatorType对应的Id值</param>
        /// <param name="strategy"></param>
        public DimensionContext(IList<SPSAQualityIndicatorType> nfList, string qIndicatorType, DimensionStrategy strategy)
        {
            this._nfList = nfList;
            this._qIndicatorType = qIndicatorType;
            this.dimensionStrategy = strategy;
        }
        public IList<SPSAQualityIndicatorType> GetSPSAQualityIndicatorTypeOfGroupBy()
        {
            IList<SPSAQualityIndicatorType> nfList2 = this.dimensionStrategy.GetSPSAQualityIndicatorTypeOfGroupBy(this._nfList, this._qIndicatorType);
            return nfList2;
        }
    }
}
