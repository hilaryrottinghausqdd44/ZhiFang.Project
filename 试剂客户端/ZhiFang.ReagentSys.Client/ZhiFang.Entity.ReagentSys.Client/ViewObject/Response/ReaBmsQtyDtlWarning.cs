using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
    [DataContract]
    /// <summary>
    /// 财务收入报表
    /// </summary>
    [DataDesc(CName = "库存预警VO", ClassCName = "ReaBmsQtyDtlWarning", ShortCode = "ReaBmsQtyDtlWarning", Desc = "库存预警VO")]
    public class ReaBmsQtyDtlWarning
    {
        public ReaBmsQtyDtlWarning() { }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual long? Id { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual long? LabID { get; set; }

        [DataMember]
        public virtual int BarCodeType { get; set; }

        [DataMember]
        public virtual string GroupByKey { get; set; }

        [DataMember]
        public virtual string CompanyName { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual long? GoodsID { get; set; }

        [DataMember]
        public virtual string GoodsName { get; set; }

        [DataMember]
        public virtual string UnitMemo { get; set; }

        [DataMember]
        public virtual string LotNo { get; set; }

        [DataMember]
        public virtual string StorageName { get; set; }

        [DataMember]
        public virtual string PlaceName { get; set; }

        [DataMember]
        public virtual string GoodsUnit { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? GoodsQty { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? SumTotal { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual DateTime? ProdDate { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual DateTime? InvalidDate { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? StoreUpper { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? StoreLower { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? CurLowerWarning { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? CurUpperWarning { get; set; }
        [DataMember]
        public virtual string ReaGoodsNo { get; set; }
        [DataMember]
        public virtual string ProdGoodsNo { get; set; }
        [DataMember]
        public virtual string CenOrgGoodsNo { get; set; }
        [DataMember]
        public virtual string GoodsNo { get; set; }
    }
}
