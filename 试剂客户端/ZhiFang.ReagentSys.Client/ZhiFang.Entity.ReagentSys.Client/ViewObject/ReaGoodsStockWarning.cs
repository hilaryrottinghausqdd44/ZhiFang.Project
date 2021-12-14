using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client.ViewObject
{
    /// <summary>
    /// 试剂库存预警查询字段
    /// 四川大家投屏需求，使用此实体
    /// </summary>
    public class ReaGoodsStockWarning: BaseEntity
    {
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "本地供应商ID", ShortCode = "ReaCompanyID", Desc = "本地供应商ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReaCompanyID { get; set; }

        [DataMember]
        [DataDesc(CName = "供应商名称", ShortCode = "CompanyName", Desc = "供应商名称", ContextType = SysDic.All, Length = 100)]
        public virtual string CompanyName { get; set; }

        [DataMember]
        [DataDesc(CName = "一级分类", ShortCode = "GoodsClass", Desc = "一级分类", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsClass { get; set; }

        [DataMember]
        [DataDesc(CName = "二级分类", ShortCode = "GoodsClassType", Desc = "二级分类", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsClassType { get; set; }
        
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "货品ID", ShortCode = "GoodsID", Desc = "货品ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? GoodsID { get; set; }

        [DataMember]
        [DataDesc(CName = "货品中文名", ShortCode = "GoodsCName", Desc = "货品中文名", ContextType = SysDic.All, Length = 200)]
        public virtual string GoodsCName { get; set; }

        [DataMember]
        [DataDesc(CName = "机构货品编号", ShortCode = "ReaGoodsNo", Desc = "机构货品编号", ContextType = SysDic.All, Length = 100)]
        public virtual string ReaGoodsNo { get; set; }

        [DataMember]
        [DataDesc(CName = "货品批号", ShortCode = "LotNo", Desc = "货品批号", ContextType = SysDic.All, Length = 100)]
        public virtual string LotNo { get; set; }

        [DataMember]
        [DataDesc(CName = "单位", ShortCode = "UnitName", Desc = "单位", ContextType = SysDic.All, Length = 100)]
        public virtual string UnitName { get; set; }

        [DataMember]
        [DataDesc(CName = "单位描述（规格）", ShortCode = "UnitMemo", Desc = "单位描述（规格）", ContextType = SysDic.All, Length = 200)]
        public virtual string UnitMemo { get; set; }
        
        [DataMember]
        [DataDesc(CName = "生成厂家", ShortCode = "ProdOrgName", Desc = "生成厂家", ContextType = SysDic.All, Length = 100)]
        public virtual string ProdOrgName { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "库存数量", ShortCode = "GoodsQty", Desc = "库存数量", ContextType = SysDic.All, Length = 8)]
        public virtual double? GoodsQty { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "单价", ShortCode = "Price", Desc = "单价", ContextType = SysDic.All, Length = 8)]
        public virtual double? Price { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "库存总计金额", ShortCode = "SumTotal", Desc = "库存总计金额", ContextType = SysDic.All, Length = 8)]
        public virtual double? SumTotal { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "InvalidDate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? InvalidDate { get; set; }

        [DataMember]
        [DataDesc(CName = "库存预警：低预警、高预警、正常、未设置（按货品显示）", ShortCode = "StockWarning", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string StockWarningByGoods { get; set; }

        [DataMember]
        [DataDesc(CName = "库存预警：低预警、高预警、正常、未设置（按货品批号、单位、效期等显示的预警）", ShortCode = "StockWarning", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string StockWarning { get; set; }

        [DataMember]
        [DataDesc(CName = "效期预警：正常、已过期、将过期、未设置", ShortCode = "ValidWarning", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ValidDateWarning { get; set; }
    }
}
