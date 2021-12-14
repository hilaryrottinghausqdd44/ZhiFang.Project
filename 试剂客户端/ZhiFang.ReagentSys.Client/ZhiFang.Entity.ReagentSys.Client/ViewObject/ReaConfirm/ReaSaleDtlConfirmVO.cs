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
    /// 客户端验收明细定制VO
    /// </summary>
    [DataContract]
    [DataDesc(CName = "客户端验收明细定制VO", ClassCName = "ReaSaleDtlConfirmVO", ShortCode = "ReaSaleDtlConfirmVO", Desc = "客户端验收明细定制VO")]
    public class ReaSaleDtlConfirmVO : BaseEntity
    {
        public ReaSaleDtlConfirmVO() { }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "待入库货品按库房试剂关系默认指定库房ID", ShortCode = "StorageID", Desc = "待入库货品按库房试剂关系默认指定库房ID")]
        public virtual long? StorageID { get; set; }

        [DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "待入库货品按库房试剂关系默认指定库房", ShortCode = "StorageCName", Desc = "待入库货品按库房试剂关系默认指定库房")]
        public virtual string StorageCName { get; set; }

        [DataMember]
        [DataDesc(CName = "待入库货品的库房试剂关系集合", ShortCode = "ReaStorageList", Desc = "待入库货品的库房试剂关系集合")]
        public virtual string ReaStorageList { get; set; }//IList<ReaStorageGoodsLink>

        [DataMember]
        [DataDesc(CName = "当次验货明细条码关系集合(验收新增/编辑封装用)", ShortCode = "ReaStorageGoodsLinkList", Desc = "当次验货明细条码关系集合")]
        public virtual IList<ReaGoodsBarcodeOperation> ReaGoodsBarcodeOperationList { get; set; }//

        [DataMember]
        [DataDesc(CName = "货品中文简称", ShortCode = "ReaGoodsSName", Desc = "货品中文简称", ContextType = SysDic.All, Length = 100)]
        public virtual string ReaGoodsSName { get; set; }
        [DataMember]
        [DataDesc(CName = "货品英文名", ShortCode = "ReaGoodsEName", Desc = "货品英文名", ContextType = SysDic.All, Length = 100)]
        public virtual string ReaGoodsEName { get; set; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "已接收数", ShortCode = "AcceptCount", Desc = "已接收数", ContextType = SysDic.All)]
        public virtual double ReceivedCount { get; set; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "已拒收数", ShortCode = "RejectedCount", Desc = "已拒收数", ContextType = SysDic.All)]
        public virtual double RejectedCount { get; set; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "可验收数", ShortCode = "ConfirmCount", Desc = "可验收数", ContextType = SysDic.All)]
        public virtual double ConfirmCount { get; set; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "(订单明细/供货明细)购进数量", ShortCode = "DtlGoodsQty", Desc = "(订单明细/供货明细)购进数量", ContextType = SysDic.All)]
        public virtual double DtlGoodsQty { get; set; }
        [DataMember]
        [DataDesc(CName = "产品序号", ShortCode = "GoodsSort", Desc = "产品序号", ContextType = SysDic.All)]
        public virtual int GoodsSort { get; set; }
        [DataMember]
        [DataDesc(CName = "供货明细条码关系集合Str", ShortCode = "ReaBmsCenSaleDtlLinkVOListStr", Desc = "供货明细条码关系集合Str", ContextType = SysDic.All, Length = Int32.MaxValue)]
        public virtual string ReaBmsCenSaleDtlLinkVOListStr { get; set; }
        [DataMember]
        [DataDesc(CName = "供货验收明细单信息", ShortCode = "ReaBmsCenSaleDtlConfirm", Desc = "供货验收明细单信息")]
        public virtual ReaBmsCenSaleDtlConfirm ReaBmsCenSaleDtlConfirm { get; set; }
        [DataMember]
        [DataDesc(CName = "验货已扫码记录集合(封装ReaGoodsBarcodeOperationVO)", ShortCode = "ReaBmsCenSaleDtlConfirmLinkVOListStr", Desc = "验货已扫码记录集合", ContextType = SysDic.All, Length = Int32.MaxValue)]
        public virtual string ReaBmsCenSaleDtlConfirmLinkVOListStr { get; set; }
        [DataMember]
        [DataDesc(CName = "入库已扫码记录集合(封装ReaGoodsBarcodeOperationVO)", ShortCode = "ReaBmsInDtlLinkListStr", Desc = "入库已扫码记录集合", ContextType = SysDic.All, Length = Int32.MaxValue)]
        public virtual string ReaBmsInDtlLinkListStr { get; set; }
        [DataMember]
        [DataDesc(CName = "当次扫码记录集合", ShortCode = "CurReaGoodsScanCodeList", Desc = "当次扫码记录集合", ContextType = SysDic.All, Length = Int32.MaxValue)]
        public virtual string CurReaGoodsScanCodeList { get; set; }
    }
}
