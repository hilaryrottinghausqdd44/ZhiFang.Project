using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client.ViewObject
{
    /// <summary>
    /// 客户端验收条码明细封装定制VO
    /// </summary>
    [DataContract]
    [DataDesc(CName = "客户端验收条码明细封装定制VO", ClassCName = "ReaGoodsBarcodeOperationVO", ShortCode = "ReaGoodsBarcodeOperationVO", Desc = "客户端验收条码明细封装定制VO")]
    public class ReaGoodsBarcodeOperationVO
    {
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "货品条码操作ID", ShortCode = "Id", Desc = "货品条码操作ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? Id { get; set; }

        [DataMember]
        [DataDesc(CName = "业务单据号", ShortCode = "BDocNo", Desc = "业务单据号", ContextType = SysDic.All, Length = 100)]
        public virtual string BDocNo { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "业务主单ID", ShortCode = "BDocID", Desc = "业务主单ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? BDocID { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "业务明细ID", ShortCode = "BDtlID", Desc = "业务明细ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? BDtlID { get; set; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "库存ID", ShortCode = "QtyDtlID", Desc = "库存ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? QtyDtlID { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "操作类型", ShortCode = "OperTypeID", Desc = "操作类型", ContextType = SysDic.All, Length = 8)]
        public virtual long? OperTypeID { get; set; }

        [DataMember]
        [DataDesc(CName = "系统内部盒条码", ShortCode = "SysPackSerial", Desc = "系统内部盒条码", ContextType = SysDic.All, Length = 150)]
        public virtual string SysPackSerial { get; set; }

        [DataMember]
        [DataDesc(CName = "第三方盒条码", ShortCode = "OtherPackSerial", Desc = "第三方盒条码", ContextType = SysDic.All, Length = 150)]
        public virtual string OtherPackSerial { get; set; }

        [DataMember]
        [DataDesc(CName = "一维使用盒条码", ShortCode = "UsePackSerial", Desc = "一维使用盒条码", ContextType = SysDic.All, Length = 150)]
        public virtual string UsePackSerial { get; set; }
        [DataMember]
        [DataDesc(CName = "二维使用盒条码", ShortCode = "UsePackQRCode", Desc = "二维使用盒条码", ContextType = SysDic.All, Length = 150)]
        public virtual string UsePackQRCode { get; set; }
        [DataMember]
        [DataDesc(CName = "货品批号", ShortCode = "LotNo", Desc = "货品批号", ContextType = SysDic.All, Length = 100)]
        public virtual string LotNo { get; set; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "签收标志：2接收、3拒收", ShortCode = "ReceiveFlag", Desc = "签收标志：2接收、3拒收", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReceiveFlag { get; set; }
        [DataMember]
        [DataDesc(CName = "厂商货品编号", ShortCode = "ProdGoodsNo", Desc = "厂商货品编号", ContextType = SysDic.All, Length = 100)]
        public virtual string ProdGoodsNo { get; set; }
        [DataMember]
        [DataDesc(CName = "货品平台编号", ShortCode = "GoodsNo", Desc = "货品平台编号", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsNo { get; set; }
        [DataMember]
        [DataDesc(CName = "供应商货品编号", ShortCode = "CenOrgGoodsNo", Desc = "供应商货品编号", ContextType = SysDic.All, Length = 100)]
        public virtual string CenOrgGoodsNo { get; set; }
        [DataMember]
        [DataDesc(CName = "产品编号", ShortCode = "ReaGoodsNo", Desc = "产品编号", ContextType = SysDic.All, Length = 100)]
        public virtual string ReaGoodsNo { get; set; }
        [DataMember]
        [DataDesc(CName = "产品序号", ShortCode = "GoodsSort", Desc = "产品序号", ContextType = SysDic.All, Length = 4)]
        public virtual int GoodsSort { get; set; }
        [DataMember]
        [DataDesc(CName = "供应商编码", ShortCode = "ReaCompCode", Desc = "供应商编码", ContextType = SysDic.All, Length = 100)]
        public virtual string ReaCompCode { get; set; }
        [DataMember]
        [DataDesc(CName = "条码类型", ShortCode = "BarCodeType", Desc = "条码类型", ContextType = SysDic.All, Length = 4)]
        public virtual int BarCodeType { get; set; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "供应商货品机构关系ID", ShortCode = "CompGoodsLinkID", Desc = "供应商货品机构关系ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? CompGoodsLinkID { get; set; }

    }
}
