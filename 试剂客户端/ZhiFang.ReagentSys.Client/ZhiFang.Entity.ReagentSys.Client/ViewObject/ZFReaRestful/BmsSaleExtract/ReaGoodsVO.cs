using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client.ViewObject.ZFReaRestful.BmsSaleExtract
{
    /// <summary>
    /// 提取平台供货单后,客户端获取对照的供应商与货品关系信息VO
    /// </summary>
    [DataContract]
    public class ReaGoodsVO
    {
        [DataMember]
        [DataDesc(CName = "条码类型字段0无条码1盒条码2批条码", ShortCode = "BarCodeType", Desc = "条码类型字段0无条码1盒条码2批条码", ContextType = SysDic.All, Length = 4)]
        public virtual int BarCodeType { get; set; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "本地货品ID", ShortCode = "ReaGoodsID", Desc = "本地货品ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReaGoodsID { get; set; }
        [DataMember]
        [DataDesc(CName = "本地货品名称", ShortCode = "ReaGoodsName", Desc = "本地货品名称", ContextType = SysDic.All, Length = 200)]
        public virtual string ReaGoodsName { get; set; }
        [DataMember]
        [DataDesc(CName = "中文简称", ShortCode = "SName", Desc = "中文简称", ContextType = SysDic.All, Length = 200)]
        public virtual string SName { get; set; }
        [DataMember]
        [DataDesc(CName = "英文名", ShortCode = "EName", Desc = "英文名", ContextType = SysDic.All, Length = 200)]
        public virtual string EName { get; set; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "货品机构关系ID", ShortCode = "CompGoodsLinkID", Desc = "货品机构关系ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? CompGoodsLinkID { get; set; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "(货品机构关系)合同单价", ShortCode = "ContractPrice", Desc = "(货品机构关系)合同单价", ContextType = SysDic.All, Length = 8)]
        public virtual double ContractPrice { get; set; }
        [DataMember]
        [DataDesc(CName = "货品编码", ShortCode = "ReaGoodsNo", Desc = "货品编码", ContextType = SysDic.All, Length = 100)]
        public virtual string ReaGoodsNo { get; set; }
        [DataMember]
        [DataDesc(CName = "厂商货品编码", ShortCode = "ProdGoodsNo", Desc = "厂商货品编码", ContextType = SysDic.All, Length = 100)]
        public virtual string ProdGoodsNo { get; set; }

        [DataMember]
        [DataDesc(CName = "供货商货品编码", ShortCode = "CenOrgGoodsNo", Desc = "供货商货品编码", ContextType = SysDic.All, Length = 100)]
        public virtual string CenOrgGoodsNo { get; set; }
        [DataMember]
        [DataDesc(CName = "货品平台编码", ShortCode = "GoodsNo", Desc = "货品平台编码", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsNo { get; set; }

    }
}
