using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.ReaConfirm;

namespace ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.ReaGoodsScanCode
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
        [DataDesc(CName = "货品编号", ShortCode = "GoodsNo", Desc = "货品编号", ContextType = SysDic.All, Length = 50)]
        public virtual string GoodsNo { get; set; }
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
        [DataDesc(CName = "是否盒条码", ShortCode = "BarCodeMgr", Desc = "是否盒条码", ContextType = SysDic.All, Length = 4)]
        public virtual int BarCodeMgr { get; set; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "货品机构关系ID", ShortCode = "ReaGoodsOrgLinkID", Desc = "货品机构关系ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReaGoodsOrgLinkID { get; set; }
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
        [DataDesc(CName = "厂商产品编号", ShortCode = "ProdGoodsNo", Desc = "厂商产品编号", ContextType = SysDic.All, Length = 50)]
        public virtual string ProdGoodsNo { get; set; }
        [DataMember]
        [DataDesc(CName = "系统内部盒条码", ShortCode = "SysPackSerial", Desc = "系统内部盒条码", ContextType = SysDic.All, Length = 100)]
        public virtual string SysPackSerial { get; set; }
        [DataMember]
        [DataDesc(CName = "第三方盒条码", ShortCode = "OtherPackSerial", Desc = "第三方盒条码", ContextType = SysDic.All, Length = 100)]
        public virtual string OtherPackSerial { get; set; }
        [DataMember]
        [DataDesc(CName = "使用盒条码", ShortCode = "UsePackSerial", Desc = "使用盒条码", ContextType = SysDic.All, Length = 100)]
        public virtual string UsePackSerial { get; set; }
    }
    /// <summary>
    /// 客户端货品扫码VO
    /// </summary>
    [DataContract]
    [DataDesc(CName = "客户端货品扫码VO", ClassCName = "ReaGoodsScanCodeVO", ShortCode = "ReaGoodsScanCodeVO", Desc = "客户端货品扫码VO")]
    public class ReaGoodsScanCodeVO
    {
        [DataMember]
        [DataDesc(CName = "货品扫码的货品信息VO", ShortCode = "ReaBarCodeVOList", Desc = "货品扫码的货品信息VO")]
        public virtual IList<ReaBarCodeVO> ReaBarCodeVOList { get; set; }
        //[DataMember]
        [DataMember]
        public bool BoolFlag { get; set; }
        [DataMember]
        [DataDesc(CName = "货品扫码错误信息", ShortCode = "ErrorInfo", Desc = "货品扫码错误信息", ContextType = SysDic.All, Length = 100)]
        public virtual string ErrorInfo { get; set; }

    }
}
