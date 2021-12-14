using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
    /// <summary>
    /// 血袋跟踪统计
    /// 输血申请综合查询:将血袋按各操作类型合并统计
    /// 操作类型包括(入库,复检,申请信息,配血信息,发血信息,接收信息,输血过程记录信息,回收信息)
    /// 按血袋名称为列,各操作记录信息合并成行的数据格式
    /// </summary>
    [DataContract]
    public class BloodBagTrackingVO
    {
        public BloodBagTrackingVO() { }

        /// <summary>
        /// 操作类型Id:对应BloodBagTrackingType的Id
        /// 入库,复检,申请,配血,发血,接收,输血过程记录,回收
        /// </summary>
        [DataMember]
        public virtual string OperaTypeId { get; set; }
        /// <summary>
        /// 操作类型名称:对应BloodBagTrackingType的Name
        /// </summary>
        [DataMember]
        public virtual string OperaTypeCName { get; set; }
        /// <summary>
        /// 血袋跟踪列英文名称(血袋跟踪列信息)
        /// </summary>
        [DataMember]
        public virtual string TrackingEName { get; set; }
        /// <summary>
        /// 血袋跟踪列中文名称(血袋跟踪列信息)
        /// </summary>
        [DataMember]
        public virtual string TrackingCName { get; set; }
        ///// <summary>
        ///// 血袋跟踪信息(按血袋分列)
        ///// </summary>
        //[DataMember]
        //public virtual string TrackingInfo { get; set; }

        ///// <summary>
        ///// 血袋号
        ///// </summary>
        //[DataMember]
        //public virtual string BBagCode { get; set; }

    }
}
