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
    [DataDesc(CName = "财务收入报表", ClassCName = "FinanceIncome", ShortCode = "FinanceIncome", Desc = "财务收入报表")]
    public class FinanceIncome: BaseEntity
    {
        public FinanceIncome() { }
        
        /// <summary>
        /// (客户)用户姓名
        /// </summary>
        [DataMember]
        public virtual string UserName { get; set; }
        /// <summary>
        /// 用户订单编号
        /// </summary>
        [DataMember]
        public virtual string UOFCode { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [DataMember]
        public virtual string SexName { get; set; }
        /// <summary>
        /// 开单日期
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual DateTime? BillingDate { get; set; }
        /// <summary>
        /// 采样日期
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual DateTime? SamplingDate { get; set; }

        /// <summary>
        /// 市场价格
        /// </summary>
        /// [JsonConverter(typeof(JsonConvertClass))]
        [DataMember]
        public virtual double? MarketPrice { get; set; }
        /// <summary>
        /// 大家价格
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? GreatMasterPrice { get; set; }
        /// <summary>
        /// 实际金额
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? Price { get; set; }
        /// <summary>
        /// 咨询费
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? AdvicePrice { get; set; }
        /// <summary>
        /// 咨询费率
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? AdvicePriceRate { get; set; }
        /// <summary>
        /// 开单医生姓名
        /// </summary>
        [DataMember]
        public virtual string DoctorName { get; set; }
        /// <summary>
        /// 退费单编号
        /// </summary>
        [DataMember]
        public virtual string MRefundFormCode { get; set; }
        /// <summary>
        /// 退费金额
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? RefundPrice { get; set; }
    }
}
