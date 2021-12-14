using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client.ViewObject
{
    public class ReaBmsCenOrderDtlVOOfReport
    {
        public ReaBmsCenOrderDtlVOOfReport() { }

        public virtual string ProdGoodsNo { get; set; }
        public virtual string ReaGoodsNo { get; set; }
        public virtual string CenOrgGoodsNo { get; set; }

        public virtual string ReaGoodsName { get; set; }
        public virtual string GoodsUnit { get; set; }
        public virtual string UnitMemo { get; set; }
        public virtual string ProdOrgName { get; set; }
        public virtual string Memo { get; set; }
        public virtual string CompanyName { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? ReqGoodsQty { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? GoodsQty { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? Price { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? SumTotal { get; set; }

        public virtual string CurrentQty { get; set; }
    }
}
