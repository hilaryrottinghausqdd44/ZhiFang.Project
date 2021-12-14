using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZhiFang.Entity.SA
{
    /// <summary>
    /// 统计结果表的统计条件实体
    /// </summary>
    [DataContract]
    public class LStatTotalWhere
    {
        public LStatTotalWhere() { }

        /// <summary>
        /// 质量指标分类类型
        /// </summary>
        [DataMember]
        public virtual string QIndicatorType { get; set; }
        /// <summary>
        /// 统计日期类型;对应LStatTotalStatDateType的ID值         
        /// </summary>
        [DataMember]
        public virtual string StatDateType { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        [DataMember]
        public virtual string StartDate { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        [DataMember]
        public virtual string EndDate { get; set; }
        /// <summary>
        /// 某一质量指标分类类型对应的统计纬度
        /// </summary>
        [DataMember]
        public virtual string Latitude { get; set; }
    }
}
