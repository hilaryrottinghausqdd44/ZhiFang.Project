using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaConfirm
{
    /// <summary>
    /// 客户端订单验收定制订单明细VO
    /// </summary>
    [DataContract]
    [DataDesc(CName = "客户端订单验收定制订单明细VO", ClassCName = "ReaOrderDtlOfConfirmVO", ShortCode = "ReaOrderDtlOfConfirmVO", Desc = "客户端订单验收定制订单明细VO")]
    public class ReaOrderDtlOfConfirmVO : ReaBmsCenOrderDtl
    {
        public ReaOrderDtlOfConfirmVO() { }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "货品信息", ShortCode = "ReaGoods", Desc = "货品信息")]
        public virtual ReaGoods ReaGoods { get; set; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "购进数量", ShortCode = "DtlGoodsQty", Desc = "购进数量", ContextType = SysDic.All)]
        public virtual double DtlGoodsQty { get; set; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "已接收数", ShortCode = "AcceptCount", Desc = "已接收数", ContextType = SysDic.All, Length = 8)]
        public virtual double ReceivedCount { get; set; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "已拒收数", ShortCode = "RejectedCount", Desc = "已拒收数", ContextType = SysDic.All, Length = 8)]
        public virtual double RejectedCount { get; set; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "可验收数", ShortCode = "ConfirmCount", Desc = "可验收数", ContextType = SysDic.All, Length = 8)]
        public virtual double ConfirmCount { get; set; }
        [DataMember]
        [DataDesc(CName = "已验收标志", ShortCode = "AcceptFlag", Desc = "已验收标志", ContextType = SysDic.All, Length = 4)]
        public virtual bool AcceptFlag { get; set; }
        [DataMember]
        [DataDesc(CName = "验货明细条码关系集合Str", ShortCode = "ReaBmsCenSaleDtlConfirmLinkVOListStr", Desc = "验货明细条码关系集合Str", ContextType = SysDic.All, Length = Int32.MaxValue)]
        public virtual string ReaBmsCenSaleDtlConfirmLinkVOListStr { get; set; }

        #region 导入供货订单用的数据项
        [DataMember]
        [DataDesc(CName = "供货单号", ShortCode = "SaleDocNo", Desc = "供货单号", ContextType = SysDic.All, Length = 100)]
        public virtual string SaleDocNo { get; set; }
        [DataMember]
        [DataDesc(CName = "供货商平台机构码", ShortCode = "ReaServerCompCode", Desc = "供货商平台机构码", ContextType = SysDic.All, Length = 100)]
        public virtual string ReaServerCompCode { get; set; }
        [DataMember]
        [DataDesc(CName = "供货商机构码", ShortCode = "ReaCompCode", Desc = "供货商机构码", ContextType = SysDic.All, Length = 100)]
        public virtual string ReaCompCode { get; set; }

        [DataMember]
        [DataDesc(CName = "批号", ShortCode = "LotNo", Desc = "批号", ContextType = SysDic.All, Length = 100)]
        public virtual string LotNo { get; set; }
        [DataMember]
        [DataDesc(CName = "生产日期", ShortCode = "ProdDate", Desc = "生产日期", ContextType = SysDic.All, Length = 20)]
        public virtual string ProdDate { get; set; }
        [DataMember]
        [DataDesc(CName = "有效期至", ShortCode = "InvalidDate", Desc = "有效期至", ContextType = SysDic.All, Length = 20)]
        public virtual string InvalidDate { get; set; }
        [DataMember]
        [DataDesc(CName = "注册证号", ShortCode = "RegisterNo", Desc = "注册证号", ContextType = SysDic.All, Length = 100)]
        public virtual string RegisterNo { get; set; }
        [DataMember]
        [DataDesc(CName = "温度保存条件", ShortCode = "StorageType", Desc = "温度保存条件", ContextType = SysDic.All, Length = 200)]
        public virtual string StorageType { get; set; }
        [DataMember]
        [DataDesc(CName = "本次供货数", ShortCode = "SaleGoodsQty", Desc = "本次供货数", ContextType = SysDic.All, Length = 8)]
        public virtual double SaleGoodsQty { get; set; } 
        #endregion


    }
}
