using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.ReaConfirm
{
    /// <summary>
    /// 客户端订单验收定制订单明细VO
    /// </summary>
    [DataContract]
    [DataDesc(CName = "客户端订单验收定制订单明细VO", ClassCName = "ReaOrderDtlVO", ShortCode = "ReaOrderDtlVO", Desc = "客户端订单验收定制订单明细VO")]
    public class ReaOrderDtlVO : BmsCenOrderDtl
    {
        public ReaOrderDtlVO() { }

        [DataMember]
        [DataDesc(CName = "是否盒条码", ShortCode = "BarCodeMgr", Desc = "是否盒条码", ContextType = SysDic.All, Length = 4)]
        public virtual int BarCodeMgr { get; set; }
        [DataMember]
        [DataDesc(CName = "货品信息", ShortCode = "ReaGoods", Desc = "货品信息")]
        public virtual ReaGoods ReaGoods { get; set; }

        [DataMember]
        [DataDesc(CName = "已接收数", ShortCode = "AcceptCount", Desc = "已接收数", ContextType = SysDic.All, Length = 4)]
        public virtual float ReceivedCount { get; set; }

        [DataMember]
        [DataDesc(CName = "已拒收数", ShortCode = "RejectedCount", Desc = "已拒收数", ContextType = SysDic.All, Length = 4)]
        public virtual float RejectedCount { get; set; }

        [DataMember]
        [DataDesc(CName = "可验收数", ShortCode = "ConfirmCount", Desc = "可验收数", ContextType = SysDic.All, Length = 4)]
        public virtual float ConfirmCount { get; set; }

        [DataMember]
        [DataDesc(CName = "已验收标志", ShortCode = "AcceptFlag", Desc = "已验收标志", ContextType = SysDic.All, Length = 4)]
        public virtual bool AcceptFlag { get; set; }

        [DataMember]
        [DataDesc(CName = "验货明细条码关系集合Str", ShortCode = "ReaBmsCenSaleDtlConfirmLinkVOListStr", Desc = "验货明细条码关系集合Str", ContextType = SysDic.All, Length = Int32.MaxValue)]
        public virtual string ReaBmsCenSaleDtlConfirmLinkVOListStr { get; set; }
    }
}
