using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaConfirm
{
    [DataContract]
    [DataDesc(CName = "供货验收定制供货VO", ClassCName = "ReaBmsCenSaleDocVO", ShortCode = "ReaBmsCenSaleDocVO", Desc = "供货验收定制供货VO")]
    public class ReaBmsCenSaleDocVO
    {
        public ReaBmsCenSaleDocVO() { }

        [DataMember]
        [DataDesc(CName = "供货验收供货主单信息", ShortCode = "ReaBmsCenSaleDoc", Desc = "供货验收供货主单信息")]
        public virtual ReaBmsCenSaleDoc ReaBmsCenSaleDoc { get; set; }

        //[DataMember]
        //[DataDesc(CName = "供货验收定制供货明细VOList", ShortCode = "ReaBmsCenSaleDtlOfConfirmVOList", Desc = "供货验收定制供货明细VOList")]
        //public virtual IList<ReaBmsCenSaleDtlOfConfirmVO> ReaBmsCenSaleDtlOfConfirmVOList { get; set; }

        [DataMember]
        [DataDesc(CName = "供货验收定制供货明细VOList", ShortCode = "ReaSaleDtlOfConfirmVOListStr", Desc = "供货验收定制供货明细VOList", ContextType = SysDic.All, Length = Int32.MaxValue)]
        public virtual string ReaBmsCenSaleDtlOfConfirmVOListStr { get; set; }
    }

    /// <summary>
    /// 客户端供货验收定制供货明细VO
    /// </summary>
    [DataContract]
    [DataDesc(CName = "供货验收定制供货明细VO", ClassCName = "ReaSaleDtlOfConfirmVO", ShortCode = "ReaSaleDtlOfConfirmVO", Desc = "客户端供货验收定制供货明细VO")]
    public class ReaSaleDtlOfConfirmVO : ReaBmsCenSaleDtl
    {
        public ReaSaleDtlOfConfirmVO() { }
        [DataMember]
        [DataDesc(CName = "货品信息", ShortCode = "ReaGoods", Desc = "货品信息")]
        public virtual ReaGoods ReaGoods { get; set; }
        [DataMember]
        [DataDesc(CName = "购进数量", ShortCode = "DtlGoodsQty", Desc = "购进数量", ContextType = SysDic.All)]
        public virtual double DtlGoodsQty { get; set; }
        [DataMember]
        [DataDesc(CName = "已接收数", ShortCode = "AcceptCount", Desc = "已接收数", ContextType = SysDic.All, Length = 4)]
        public virtual double ReceivedCount { get; set; }

        [DataMember]
        [DataDesc(CName = "已拒收数", ShortCode = "RejectedCount", Desc = "已拒收数", ContextType = SysDic.All, Length = 4)]
        public virtual double RejectedCount { get; set; }

        [DataMember]
        [DataDesc(CName = "可验收数", ShortCode = "ConfirmCount", Desc = "可验收数", ContextType = SysDic.All, Length = 4)]
        public virtual double ConfirmCount { get; set; }

        [DataMember]
        [DataDesc(CName = "已验收标志", ShortCode = "AcceptFlag", Desc = "已验收标志", ContextType = SysDic.All, Length = 4)]
        public virtual bool AcceptFlag { get; set; }

        [DataMember]
        [DataDesc(CName = "供货明细条码关系集合Str", ShortCode = "ReaBmsCenSaleDtlLinkVOListStr", Desc = "供货明细条码关系集合Str", ContextType = SysDic.All, Length = Int32.MaxValue)]
        public virtual string ReaBmsCenSaleDtlLinkVOListStr { get; set; }

        [DataMember]
        [DataDesc(CName = "验货明细条码关系集合Str", ShortCode = "ReaBmsCenSaleDtlConfirmLinkVOListStr", Desc = "验货明细条码关系集合Str", ContextType = SysDic.All, Length = Int32.MaxValue)]
        public virtual string ReaBmsCenSaleDtlConfirmLinkVOListStr { get; set; }

        [DataMember]
        [DataDesc(CName = "订货单号", ShortCode = "OrderDocNo", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string OrderDocNo { get; set; }

    }
}
