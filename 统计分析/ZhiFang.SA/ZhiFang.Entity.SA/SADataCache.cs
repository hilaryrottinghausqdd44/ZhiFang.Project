using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ZhiFang.Entity.SA
{
    public static class SADataCache
    {
        /// <summary>
        /// 正在新增保存到数据库统计结果的Key集合:防止多用户同时调用同一查询服务时,重复保存相同的统计结果
        /// (1)在查询服务按业务数据获取到统计结果后,统计结果需要保存到统计结果前添加;
        /// (2)在将统计结果保存到统计结果成功后移除;
        /// Key值:如质量指标类型的统计结果按"ClassificationId+_+StatType+_+StatDateType+_+StatDValue+_+StatDateBegin+_+StatDateEnd"组合成Key值
        /// </summary>
        public static IList<string> AddLStatTotalKeyList = new List<string>();

        /// <summary>
        /// 统计结果数据缓存
        /// Key值:如质量指标类型的统计结果按"ClassificationId+_+StatType+_StatDValue+_+StatDateType+_+StatDateBegin+_+StatDateEnd"组合成Key值
        /// Value值:LStatTotal实体对象
        /// </summary>
        public static Dictionary<string, LStatTotal> LStatTotalDataCache = new Dictionary<string, LStatTotal>();
        /// <summary>
        /// 统计结果数据缓存
        /// </summary>
        //public static HttpApplication LStatTotalDataCache = new HttpApplication();

    }
}
