

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.BloodTransfusion
{
    /// <summary>
    ///
    /// </summary>
    public interface IBLStatTotal : IBGenericManager<LStatTotal>
    {
        #region 质量指标分类类型统计
        /// <summary>
        /// 获取某一质量指标类型的指的年份及指定的月份的不合格总数及标本总量
        /// </summary>
        /// <param name="classificationId">统计结果分类Id:LStatTotalClassification对应的Id值</param>
        /// <param name="qitype">质量指标类型:QualityIndicatorType对应的Id值</param>
        /// <param name="year">指定的年份;如"2019"</param>
        /// <param name="month">指定的月份;如"2019-01"</param>
        /// <returns></returns>
        BaseResultDataValue SearchFailedTotalAndSpecimenTotalOfYearAndMonth(string classificationId, string qitype, string year, string month);
        /// <summary>
        /// 获取某一质量指标类型的多统计纬度的统计数据
        /// </summary>
        /// <param name="classificationId">统计结果分类Id:LStatTotalClassification对应的Id值</param>
        /// <param name="qitype">质量指标类型:QualityIndicatorType对应的Id值</param>
        /// <param name="statDateType">查询类型:QualityIndicatorSearchType的Id值</param>
        /// <param name="sadimension">各质量指标分类类型对应的统计维度的Id值</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        BaseResultDataValue SearchSPSAQualityIndicatorTypeOfEChart(string classificationId, string qitype, string statDateType, string sadimension, string startDate, string endDate, string where, string sort);
        /// <summary>
        /// 获取某一质量指标类型的多统计纬度的统计数据
        /// </summary>
        /// <param name="classificationId">统计结果分类Id:LStatTotalClassification对应的Id值</param>
        /// <param name="qitype">质量指标类型:QualityIndicatorType对应的Id值</param>
        /// <param name="statDateType">查询类型:QualityIndicatorSearchType的Id值</param>
        /// <param name="sadimension">各质量指标分类类型对应的统计维度的Id值</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<SPSAQualityIndicatorType> SearchSPSAQualityIndicatorTypeList(string classificationId, string qitype, string statDateType, string sadimension, string startDate, string endDate, string where, string sort, int page, int limit);
        #endregion
    }
}