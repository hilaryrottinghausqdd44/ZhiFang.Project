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
    /// 血袋接收登记-
    /// 包含血袋接收及血袋血袋外观,血袋完整性信息
    /// </summary>
    [DataContract]
    public class BloodBagHandoverVO
    {
        public BloodBagHandoverVO() { }

        /// <summary>
        /// 血袋接收信息
        /// </summary>
        [DataMember]
        [DataDesc(CName = "血袋接收信息", ShortCode = "BloodBagHandover", Desc = "血袋接收信息")]
        public virtual BloodBagOperation BloodBagHandover { get; set; }

        [DataMember]
        [DataDesc(CName = "血袋外观信息", ShortCode = "BloodAppearance", Desc = "血袋外观信息")]
        public virtual BloodBagOperationDtl BloodAppearance { get; set; }

        [DataMember]
        [DataDesc(CName = "血袋完整性", ShortCode = "BloodIntegrity", Desc = "血袋完整性")]
        public virtual BloodBagOperationDtl BloodIntegrity { get; set; }

    }
}
