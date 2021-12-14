using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
    [DataContract]
    /// <summary>
    /// 财务收入报表
    /// </summary>
    [DataDesc(CName = "项目统计报表(以医生帐户Id+套餐项目ID作分组)", ClassCName = "UserConsumerItemDetails", ShortCode = "UserConsumerItemDetails", Desc = "项目统计报表(以医生帐户Id+套餐项目ID作分组)")]
    public class UserConsumerItemDetails : BaseEntity
    {
        public UserConsumerItemDetails() { }
        /// <summary>
        /// 分组列,开单医生Id+套餐项目Id
        /// </summary>
        [DataMember]
        public virtual string GroupingKey { get; set; }
        /// <summary>
        /// 开单医生
        /// </summary>
        [DataMember]
        public virtual string DoctorName { get; set; }
        /// <summary>
        /// 所属医院
        /// </summary>
        [DataMember]
        public virtual string HospitalCName { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        [DataMember]
        public virtual string AreaCName { get; set; }
        /// <summary>
        /// 套餐名称
        /// </summary>
        [DataMember]
        public virtual string ItemCName { get; set; }
        /// <summary>
        /// 数量(开单医生对某套餐项目的开单数量)
        /// </summary>
        [DataMember]
        public virtual int OrderFormCount { get; set; }
        /// <summary>
        /// 市场价格
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? MarketPrice { get; set; }
        /// <summary>
        /// 大家价格
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? GreatMasterPrice { get; set; }
        /// <summary>
        /// 收入(折扣价格*数量)
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? Price { get; set; }
        
    }
}
