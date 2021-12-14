using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
    [DataContract]
    public class BRuleNumber: BaseEntity
    {
        public BRuleNumber() { }
        [DataMember]
        public string strSubNumber { get; set; }
        [DataMember]
        public bool isMaxNum { get; set; }
    }
}
