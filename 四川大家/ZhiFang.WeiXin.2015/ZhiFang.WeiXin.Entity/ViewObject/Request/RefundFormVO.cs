using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity.ViewObject.Request
{
    [DataContract]
    public class OrderFormRefundVO
    {
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public long Id { get; set; }
        [DataMember]
        public string OrderFormCode { get; set; }
        [DataMember]
        public string Reason { get; set; }
        [DataMember]
        public bool Result { get; set; }
    }
    [DataContract]
    public class RefundFormVO
    {
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public  long Id { get; set; }
        [DataMember]
        public  string RefundFormCode { get; set; }
        [DataMember]
        public  string Reason { get; set; }
        [DataMember]
        public  bool Result { get; set; }
    }
    [DataContract]
    public class RefundFormApplyVO : RefundFormVO
    {
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public double RefundPrice { get; set; }
    }
    [DataContract]
    public class RefundFormThreeReviewVO : RefundFormVO
    {
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public long RefundType { get; set; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public long BankID { get; set; }
        [DataMember]
        public string BankAccount { get; set; }
        [DataMember]
        public string BankTransFormCode { get; set; }
    }

}
