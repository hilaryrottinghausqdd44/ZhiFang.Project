

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.Statistics;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaEquipTestItemReaGoodLink : IBGenericManager<ReaEquipTestItemReaGoodLink>
    {
        IList<ReaEquipTestItemReaGoodLink> SearchNewListByHQL(string where, string sort, int page, int limit);
        EntityList<ReaEquipTestItemReaGoodLink> SearchNewEntityListByHQL(string where, string sort, int page, int limit);
        /// <summary>
        /// 消耗比对分析,试剂理论消耗量/消耗比对分析
        /// </summary>
        /// <param name="statisticType">统计类型:1为理论消耗量;2为消耗比对分析;</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="equipIdStr">统计的仪器IDStr:如123,2345,3455</param>
        /// <param name="goodsIdStr">统计的试剂IDStr:如123,2345,3455</param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="sortType">1:按试剂项目列表时,排序:仪器-试剂-项目-检测类型;2: 按项目试剂列表时,排序:仪器-项目-试剂--检测类型</param>
        /// <param name="isMergeOfItem">同一仪器相同试剂不同项目的结果按项目合并</param>
        /// <returns></returns>
        EntityList<ConsumptionComparisonAnalysisVO> SearchConsumptionComparisonAnalysisVOListByHql(int statisticType, string startDate, string endDate, string equipIdStr, string goodsIdStr, int page, int limit, string sortType, bool isMergeOfItem);
        /// <summary>
        /// 消耗比对分析,试剂理论消耗量ECharts图表
        /// </summary>
        /// <param name="statisticType">按仪器试剂显示:1;按仪器试剂项目显示:2;</param>
        /// <param name="showZero"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="dtlHql"></param>
        /// <param name="equipIdStr"></param>
        /// <param name="goodsIdStr"></param>
        /// <param name="isMergeOfItem"></param>
        /// <returns></returns>
        BaseResultDataValue SearChconsumeTheoryEChartsVOByHql(int statisticType, bool showZero, string startDate, string endDate, string dtlHql, string equipIdStr, string goodsIdStr, bool isMergeOfItem);
        /// <summary>
        /// 消耗比对分析,消耗比对分析ECharts图表
        /// </summary>
        /// <param name="statisticType"></param>
        /// <param name="showZero"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="dtlHql"></param>
        /// <param name="equipIdStr"></param>
        /// <param name="goodsIdStr"></param>
        /// <param name="isMergeOfItem"></param>
        /// <returns></returns>
        BaseResultDataValue SearChconsumeComparisonEChartsVOByHql(int statisticType, bool showZero, string startDate, string endDate, string dtlHql, string equipIdStr, string goodsIdStr, bool isMergeOfItem);
    }
}