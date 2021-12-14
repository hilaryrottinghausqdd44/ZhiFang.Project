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
    /// 输血申请综合查询:血袋接收&回收VO
    /// 包含血袋接收及血袋回收信息
    /// 惟一标识血袋：BBagCode+PCode
    /// </summary>
    [DataContract]
    public class BloodBagOperationVO
    {
        public BloodBagOperationVO() { }
        /// <summary>
        /// 血袋号
        /// </summary>
        [DataMember]
        public virtual string BBagCode { get; set; }
        /// <summary>
        /// 产品码
        /// </summary>
        [DataMember]
        public virtual string PCode { get; set; }
        /// <summary>
        /// 惟一码
        /// </summary>
        [DataMember]
        public virtual string B3Code { get; set; }

        /// <summary>
        /// 血袋信息
        /// </summary>
        [DataMember]
        [DataDesc(CName = "Bloodstyle", ShortCode = "Bloodstyle", Desc = "Bloodstyle")]
        public virtual Bloodstyle Bloodstyle { get; set; }

        /// <summary>
        /// 申请单信息
        /// </summary>
        [DataMember]
        [DataDesc(CName = "BloodBReqForm", ShortCode = "BloodBReqForm", Desc = "申请单信息")]
        public virtual BloodBReqForm BloodBReqForm { get; set; }

        /// <summary>
        /// 血袋发血明细信息
        /// </summary>
        [DataMember]
        [DataDesc(CName = "BloodBOutItem", ShortCode = "BloodBOutItem", Desc = "血袋发血明细信息")]
        public virtual BloodBOutItem BloodBOutItem { get; set; }
        /// <summary>
        /// 血袋接收信息
        /// </summary>
        [DataMember]
        [DataDesc(CName = "BloodBagHandover", ShortCode = "BloodBagHandover", Desc = "血袋接收信息")]
        public virtual BloodBagOperation BloodBagHandover { get; set; }

        /// <summary>
        /// 血袋回收信息
        /// </summary>
        [DataMember]
        [DataDesc(CName = "BloodBagRecover", ShortCode = "BloodBagRecover", Desc = "血袋回收信息")]
        public virtual BloodBagOperation BloodBagRecover { get; set; }

    }
}
