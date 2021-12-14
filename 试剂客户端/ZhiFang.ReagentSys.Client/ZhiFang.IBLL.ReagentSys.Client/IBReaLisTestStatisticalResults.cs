

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
    public interface IBReaLisTestStatisticalResults : IBGenericManager<ReaLisTestStatisticalResults>
    {
        /// <summary>
        /// 试剂客户端,从LIS系统导入检测结果
        /// </summary>
        /// <param name="testType">检测类型(LisTestType的ID,多个时为:Common,Review)</param>
        /// <param name="beginDate">检测开始日期</param>
        /// <param name="endDate">检测结束日期</param>
        /// <param name="equipIDStr">检测仪器编码</param> 
        /// <param name="lisEquipCodeStr">检测仪器LIS编码</param>
        /// <param name="where">获取(合并)检测结果后的过滤条件</param>
        /// <param name="order">获取(合并)检测结果后的排序</param>
        /// <param name="isCover">当次提取的结果是否覆盖原已提取的检测结果</param>
        /// <returns></returns>
        BaseResultBool SaveReaLisTestStatisticalResults(string testType, string beginDate, string endDate, string equipIDStr, string lisEquipCodeStr, string where, string order, bool isCover);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupType">分组类型:1:按使用仪器+检测类型+项目Id分组</param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="dtlHql"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        IList<ReaLisTestStatisticalResults> SearchTestStatisticalResultsListByJoinHql(int groupType, string startDate, string endDate, string dtlHql, int page, int limit, string sort);
        /// <summary>
        /// 消耗比对分析,项目检测量
        /// </summary>
        /// <param name="groupType"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="dtlHql"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        EntityList<ReaLisTestStatisticalResults> SearchTestStatisticalResultsEntityListByJoinHql(int groupType, string startDate, string endDate, string dtlHql, int page, int limit, string sort);
        /// <summary>
        /// 消耗比对分析,项目检测量ECharts图表
        /// </summary>
        /// <param name="statisticType"></param>
        /// <param name="showZero"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="dtlHql"></param>
        /// <param name="deptGoodsHql"></param>
        /// <param name="reaGoodsHql"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        BaseResultDataValue SearchLisResultsEChartsVOByHql(int statisticType, bool showZero, string startDate, string endDate, string dtlHql, string deptGoodsHql, string reaGoodsHql, string sort);
    }
}