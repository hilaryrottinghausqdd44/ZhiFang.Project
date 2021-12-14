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
    /// 货品条码打印VO
    /// </summary>
    [DataContract]
    [DataDesc(CName = "条码打印VO", ClassCName = "ReaGoodsPrintBarCodeVO", ShortCode = "ReaGoodsPrintBarCodeVO", Desc = "条码打印VO")]
    public class ReaGoodsPrintBarCodeVO
    {
        [DataMember]
        [DataDesc(CName = "条码类型字段0无条码1盒条码2批条码", ShortCode = "BarCodeType", Desc = "条码类型字段0无条码1盒条码2批条码", ContextType = SysDic.All, Length = 4)]
        public virtual long BarCodeType { get; set; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "货品ID", ShortCode = "GoodsID", Desc = "货品ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? GoodsID { get; set; }
        [DataMember]
        [DataDesc(CName = "中文名", ShortCode = "GoodsName", Desc = "中文名", ContextType = SysDic.All, Length = 200)]
        public virtual string GoodsName { get; set; }
        [DataMember]
        [DataDesc(CName = "中文简称", ShortCode = "SName", Desc = "中文简称", ContextType = SysDic.All, Length = 200)]
        public virtual string SName { get; set; }
        [DataMember]
        [DataDesc(CName = "英文名", ShortCode = "EName", Desc = "英文名", ContextType = SysDic.All, Length = 200)]
        public virtual string EName { get; set; }
        [DataMember]
        [DataDesc(CName = "产品简码", ShortCode = "ShortCode", Desc = "产品简码", ContextType = SysDic.All, Length = 200)]
        public virtual string ShortCode { get; set; }
        [DataMember]
        [DataDesc(CName = "单位", ShortCode = "GoodsUnit", Desc = "单位", ContextType = SysDic.All, Length = 10)]
        public virtual string GoodsUnit { get; set; }
        [DataMember]
        [DataDesc(CName = "单位描述（规格）", ShortCode = "UnitMemo", Desc = "单位描述（规格）", ContextType = SysDic.All, Length = 200)]
        public virtual string UnitMemo { get; set; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "价格", ShortCode = "Price", Desc = "价格", ContextType = SysDic.All, Length = 8)]
        public virtual double? Price { get; set; }
        [DataMember]
        [DataDesc(CName = "父使用盒条码", ShortCode = "PUsePackSerial", Desc = "父使用盒条码", ContextType = SysDic.All, Length = 150)]
        public virtual string PUsePackSerial { get; set; }
        [DataMember]
        [DataDesc(CName = "一维使用盒条码", ShortCode = "UsePackSerial", Desc = "一维使用盒条码", ContextType = SysDic.All, Length = 150)]
        public virtual string UsePackSerial { get; set; }
        [DataMember]
        [DataDesc(CName = "二维使用盒条码", ShortCode = "UsePackQRCode", Desc = "二维使用盒条码", ContextType = SysDic.All, Length = 150)]
        public virtual string UsePackQRCode { get; set; }

        [DataMember]
        [DataDesc(CName = "一维批号条码", ShortCode = "LotSerial", Desc = "一维批号条码", ContextType = SysDic.All, Length = 150)]
        public virtual string LotSerial { get; set; }
        [DataMember]
        [DataDesc(CName = "二维批号条码", ShortCode = "LotQRCode", Desc = "二维批号条码", ContextType = SysDic.All, Length = 150)]
        public virtual string LotQRCode { get; set; }
        [DataMember]
        [DataDesc(CName = "货品批号", ShortCode = "LotNo", Desc = "货品批号", ContextType = SysDic.All, Length = 100)]
        public virtual string LotNo { get; set; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "有效期", ShortCode = "InvalidDate", Desc = "有效期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? InvalidDate { get; set; }
        [DataMember]
        [DataDesc(CName = "品牌编号", ShortCode = "ProdOrgNo", Desc = "品牌编号", ContextType = SysDic.All, Length = 50)]
        public virtual string ProdOrgNo { get; set; }
        [DataMember]
        [DataDesc(CName = "供应商编号", ShortCode = "CompOrgNo", Desc = "供应商编号", ContextType = SysDic.All, Length = 50)]
        public virtual string CompOrgNo { get; set; }
        [DataMember]
        [DataDesc(CName = "单据号(入库总单号/供货总单号)", ShortCode = "SaleDocNo", Desc = "单据号", ContextType = SysDic.All, Length = 50)]
        public virtual string SaleDocNo { get; set; }
        [DataMember]
        [DataDesc(CName = "一级分类", ShortCode = "GoodsClass", Desc = "一级分类", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsClass { get; set; }
        [DataMember]
        [DataDesc(CName = "二级分类", ShortCode = "GoodsClassType", Desc = "二级分类", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsClassType { get; set; }
        [DataDesc(CName = "当前序号", ShortCode = "DispOrder", Desc = "当前序号", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder { get; set; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数量", ShortCode = "GoodsQty", Desc = "条码数量", ContextType = SysDic.All, Length = 8)]
        public virtual double? GoodsQty { get; set; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "最小包装单位条码数", ShortCode = "MinBarCodeQty", Desc = "最小包装单位条码数", ContextType = SysDic.All, Length = 8)]
        public virtual double? MinBarCodeQty { get; set; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "总计金额", ShortCode = "SumTotal", Desc = "总计金额", ContextType = SysDic.All, Length = 8)]
        public virtual double? SumTotal { get; set; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "生产日期", ShortCode = "ProdDate", Desc = "生产日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ProdDate { get; set; }
        [DataMember]
        [DataDesc(CName = "招标号", ShortCode = "BiddingNo", Desc = "招标号", ContextType = SysDic.All, Length = 100)]
        public virtual string BiddingNo { get; set; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "注册证有效期", ShortCode = "RegisterInvalidDate", Desc = "注册证有效期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? RegisterInvalidDate { get; set; }
        [DataMember]
        [DataDesc(CName = "注册证编号", ShortCode = "RegisterNo", Desc = "注册证编号", ContextType = SysDic.All, Length = 100)]
        public virtual string RegisterNo { get; set; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "条码操作记录主ID", ShortCode = "Id", Desc = "条码操作记录主ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? Id { get; set; }
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
        [DataDesc(CName = "", ShortCode = "PrintCount", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int PrintCount { get; set; }

        [DataMember]
        [DataDesc(CName = "分组列:货品ID+批号", ShortCode = "GroupValue", Desc = "货品ID+批号", ContextType = SysDic.All, Length = 100)]
        public virtual string GroupValue { get; set; }
        [DataMember]
        [DataDesc(CName = "产品编号", ShortCode = "ReaGoodsNo", Desc = "产品编号", ContextType = SysDic.All, Length = 100)]
        public virtual string ReaGoodsNo { get; set; }
        [DataMember]
        [DataDesc(CName = "厂商产品编号", ShortCode = "ProdGoodsNo", Desc = "厂商产品编号", ContextType = SysDic.All, Length = 100)]
        public virtual string ProdGoodsNo { get; set; }
        [DataMember]
        [DataDesc(CName = "供货商货品编号", ShortCode = "CenOrgGoodsNo", Desc = "供货商货品编号", ContextType = SysDic.All, Length = 100)]
        public virtual string CenOrgGoodsNo { get; set; }
        [DataMember]
        [DataDesc(CName = "货品平台编号", ShortCode = "GoodsNo", Desc = "货品平台编号", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsNo { get; set; }
    }

}
