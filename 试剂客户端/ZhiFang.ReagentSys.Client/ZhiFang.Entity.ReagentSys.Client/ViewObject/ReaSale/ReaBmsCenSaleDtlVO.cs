using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaSale
{
    /// <summary>
    /// 客户端供货明细VO
    /// </summary>
    [DataContract]
    [DataDesc(CName = "客户端供货明细VO", ClassCName = "ReaBmsCenSaleDtlVO", ShortCode = "ReaBmsCenSaleDtlVO", Desc = "客户端供货明细VO")]
    public class ReaBmsCenSaleDtlVO
    {
        public ReaBmsCenSaleDtlVO() { }

        [DataMember]
        [DataDesc(CName = "供货明细信息", ShortCode = "ReaBmsCenSaleDtl", Desc = "供货明细信息")]
        public virtual ReaBmsCenSaleDtl ReaBmsCenSaleDtl { get; set; }

        [DataMember]
        [DataDesc(CName = "供货明细的条码关系集合", ShortCode = "BarcodeOperationList", Desc = "供货明细的条码关系集合")]
        public virtual IList<ReaGoodsBarcodeOperation> BarcodeOperationList { get; set; }
    }
}
