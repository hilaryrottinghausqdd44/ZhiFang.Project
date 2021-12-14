using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.NHB.ReagentSys.Client
{
    public interface IDReaLisTestStatisticalResultsDao_SQL : IDBaseDao<ReaLisTestStatisticalResults, long>
    {
        /// <summary>
        /// 获取LIS检测结果
        /// </summary>
        /// <param name="testType">检测类型(LisTestType的ID,多个时为:Common,Review)</param>
        /// <param name="beginDate">检测开始日期</param>
        /// <param name="endDate">检测结束日期</param>
        /// <param name="equipNo">检测仪器编码</param>
        /// <param name="where">获取(合并)检测结果后的过滤条件</param>
        /// <param name="order">获取(合并)检测结果后的排序</param>
        /// <returns></returns>
        DataSet SelectLisTestStatisticalResultsList(string testType, string beginDate, string endDate, string equipNo, string where, string order);
    }
}
