using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaGoodsScanCode
{
    /// <summary>
    /// 客户端货品扫码的货品信息VO
    /// </summary>
    [DataContract]
    [DataDesc(CName = "客户端货品扫码的货品信息VO", ClassCName = "ReaBarCodeVO", ShortCode = "ReaBarCodeVO", Desc = "客户端货品扫码的货品信息VO")]
    public class ReaBarCodeVO
    {
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "货品ID", ShortCode = "ReaGoodsID", Desc = "货品ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReaGoodsID { get; set; }
        [DataMember]
        [DataDesc(CName = "中文名", ShortCode = "CName", Desc = "中文名", ContextType = SysDic.All, Length = 200)]
        public virtual string CName { get; set; }
        [DataMember]
        [DataDesc(CName = "中文简称", ShortCode = "SName", Desc = "中文简称", ContextType = SysDic.All, Length = 200)]
        public virtual string SName { get; set; }
        [DataMember]
        [DataDesc(CName = "英文名", ShortCode = "EName", Desc = "英文名", ContextType = SysDic.All, Length = 200)]
        public virtual string EName { get; set; }
        [DataMember]
        [DataDesc(CName = "单位", ShortCode = "UnitName", Desc = "单位", ContextType = SysDic.All, Length = 10)]
        public virtual string UnitName { get; set; }
        [DataMember]
        [DataDesc(CName = "单位描述（规格）", ShortCode = "UnitMemo", Desc = "单位描述（规格）", ContextType = SysDic.All, Length = 200)]
        public virtual string UnitMemo { get; set; }
        [DataMember]
        [DataDesc(CName = "批准文号", ShortCode = "ApproveDocNo", Desc = "批准文号", ContextType = SysDic.All, Length = 200)]
        public virtual string ApproveDocNo { get; set; }
        [DataMember]
        [DataDesc(CName = "注册号", ShortCode = "RegistNo", Desc = "注册号", ContextType = SysDic.All, Length = 200)]
        public virtual string RegistNo { get; set; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "注册日期", ShortCode = "RegistDate", Desc = "注册日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? RegistDate { get; set; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "注册证有效期", ShortCode = "RegistNoInvalidDate", Desc = "注册证有效期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? RegistNoInvalidDate { get; set; }
        [DataMember]
        [DataDesc(CName = "条码类型字段0无条码1盒条码2批条码", ShortCode = "BarCodeType", Desc = "条码类型字段0无条码1盒条码2批条码", ContextType = SysDic.All, Length = 8)]
        public virtual long BarCodeType { get; set; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "供应商货品关系ID", ShortCode = "CompGoodsLinkID", Desc = "供应商货品关系ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? CompGoodsLinkID { get; set; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "(货品机构关系)价格", ShortCode = "Price", Desc = "(货品机构关系)价格", ContextType = SysDic.All, Length = 8)]
        public virtual double Price { get; set; }
        [DataMember]
        [DataDesc(CName = "(货品机构关系)招标号", ShortCode = "BiddingNo", Desc = "(货品机构关系)招标号", ContextType = SysDic.All, Length = 100)]
        public virtual string BiddingNo { get; set; }
        [DataMember]
        [DataDesc(CName = "货品批号", ShortCode = "LotNo", Desc = "货品批号", ContextType = SysDic.All, Length = 100)]
        public virtual string LotNo { get; set; }
        [DataMember]
        [DataDesc(CName = "有效期", ShortCode = "InvalidDate", Desc = "有效期", ContextType = SysDic.All, Length = 20)]
        public virtual string InvalidDate { get; set; }
        [DataMember]
        [DataDesc(CName = "一维盒条码", ShortCode = "UsePackSerial", Desc = "一维盒条码", ContextType = SysDic.All, Length = 150)]
        public virtual string UsePackSerial { get; set; }
        [DataMember]
        [DataDesc(CName = "二维盒条码", ShortCode = "UsePackQRCode", Desc = "二维盒条码", ContextType = SysDic.All, Length = 150)]
        public virtual string UsePackQRCode { get; set; }
        [DataMember]
        [DataDesc(CName = "系统内部盒条码", ShortCode = "SysPackSerial", Desc = "系统内部盒条码", ContextType = SysDic.All, Length = 150)]
        public virtual string SysPackSerial { get; set; }
        [DataMember]
        [DataDesc(CName = "第三方盒条码", ShortCode = "OtherPackSerial", Desc = "第三方盒条码", ContextType = SysDic.All, Length = 150)]
        public virtual string OtherPackSerial { get; set; }
        [DataMember]
        [DataDesc(CName = "产品编号", ShortCode = "ReaGoodsNo", Desc = "产品编号", ContextType = SysDic.All, Length = 100)]
        public virtual string ReaGoodsNo { get; set; }
        [DataMember]
        [DataDesc(CName = "厂商产品编号", ShortCode = "ProdGoodsNo", Desc = "厂商产品编号", ContextType = SysDic.All, Length = 100)]
        public virtual string ProdGoodsNo { get; set; }
        [DataMember]
        [DataDesc(CName = "供货商货品编码", ShortCode = "CenOrgGoodsNo", Desc = "供货商货品编码")]
        public virtual string CenOrgGoodsNo { get; set; }
        [DataMember]
        [DataDesc(CName = "货品平台编号", ShortCode = "GoodsNo", Desc = "货品平台编号", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsNo { get; set; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "明细总数", ShortCode = "GoodsQty", Desc = "明细总数", ContextType = SysDic.All, Length = 8)]
        public virtual double GoodsQty { get; set; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "最小包装单位条码数", ShortCode = "MinBarCodeQty", Desc = "最小包装单位条码数", ContextType = SysDic.All, Length = 8)]
        public virtual double? MinBarCodeQty { get; set; }
        [DataMember]
        [DataDesc(CName = "当前序号", ShortCode = "CurDispOrder", Desc = "当前序号", ContextType = SysDic.All, Length = 8)]
        public virtual int CurDispOrder { get; set; }
    }
    /// <summary>
    /// 客户端货品扫码VO
    /// </summary>
    [DataContract]
    [DataDesc(CName = "客户端货品扫码VO", ClassCName = "ReaGoodsScanCodeVO", ShortCode = "ReaGoodsScanCodeVO", Desc = "客户端货品扫码VO")]
    public class ReaGoodsScanCodeVO
    {
        [DataMember]
        [DataDesc(CName = "货品扫码的货品信息VOList", ShortCode = "ReaBarCodeVOList", Desc = "货品扫码的货品信息VOList")]
        public virtual IList<ReaBarCodeVO> ReaBarCodeVOList { get; set; }

        [DataMember]
        public bool BoolFlag { get; set; }
        [DataMember]
        [DataDesc(CName = "货品扫码错误信息", ShortCode = "ErrorInfo", Desc = "货品扫码错误信息", ContextType = SysDic.All, Length = 100)]
        public virtual string ErrorInfo { get; set; }

    }
}
