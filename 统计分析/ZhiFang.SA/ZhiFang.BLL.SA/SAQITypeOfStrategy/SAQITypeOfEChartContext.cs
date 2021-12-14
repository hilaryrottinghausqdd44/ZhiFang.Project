using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.SA;

namespace ZhiFang.BLL.SA.SAQITypeOfStrategy
{
    /// <summary>
    /// 质量指标分类类型图表的Context
    /// </summary>
    class SAQITypeOfEChartContext
    {
        private IList<SPSAQualityIndicatorType> _nfList = new List<SPSAQualityIndicatorType>();
        private string _qIndicatorType = "";
        private string _sadimension = "";
        private QITypeOfEChartStrategy _ofEChartStrategy = null;  //对象组合

        /// <summary>
        /// 获取EChart数据使用
        /// </summary>
        /// <param name="nfList"></param>
        /// <param name="qitype">质量指标类型:QualityIndicatorType对应的Id值</param>
        /// <param name="sadimension">各质量指标分类类型对应的统计维度的Id值</param>
        /// <param name="strategy">某一具体的策略对象</param>
        public SAQITypeOfEChartContext(IList<SPSAQualityIndicatorType> nfList, string qitype, string sadimension, QITypeOfEChartStrategy strategy)
        {
            this._nfList = nfList;
            this._qIndicatorType = qitype;
            this._sadimension = sadimension;
            this._ofEChartStrategy = strategy;
        }
        public BaseResultDataValue GetSAQualityIndicatorTypeOfEChar()
        {
            BaseResultDataValue baseResultDataValue = this._ofEChartStrategy.GetSAQualityIndicatorTypeOfEChart(this._nfList, this._qIndicatorType, this._sadimension);
            return baseResultDataValue;
        }
        public IList<SPSAQualityIndicatorType> GetSAQualityIndicatorTypeOfList(string startDate, string endDate)
        {
            IList<SPSAQualityIndicatorType> saList = new List<SPSAQualityIndicatorType>();
            saList = this._ofEChartStrategy.GetSAQualityIndicatorTypeOfList(this._nfList, this._qIndicatorType, this._sadimension, startDate, endDate);
            return saList;
        }

    }
}
