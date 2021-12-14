using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client.ViewObject
{
    [DataContract]
    [DataDesc(CName = "库存标志处理VO", ClassCName = "QtyMarkVO", ShortCode = "QtyMarkVO", Desc = "库存标志处理VO")]
    public class QtyMarkVO
    {
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "指定库房ID", ShortCode = "StorageID", Desc = "指定库房ID")]
        public virtual long? StorageID { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "指定货品编码集合", ShortCode = "ReaGoodNoList", Desc = "指定货品编码集合")]
        public virtual IList<string> ReaGoodNoList { get; set; }

        [DataMember]
        [DataDesc(CName = "库存标志更新值", ShortCode = "QtyDtlMark", Desc = "库存标志更新值", ContextType = SysDic.All, Length = 4)]
        public virtual int QtyDtlMark { get; set; }

    }
}
