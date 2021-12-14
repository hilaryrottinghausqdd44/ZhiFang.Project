using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client.ViewObject.Response
{
    /// <summary>
    /// 获取采购申请货品库存数量VO
    /// </summary>
    public class ReaGoodsCurrentQtyVO
    {
        public ReaGoodsCurrentQtyVO() { }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual long CurGoodsId { get; set; }
        /// <summary>
        /// 货品当前库存数量描述
        /// </summary>
        public virtual string CurrentQty { get; set; }
        /// <summary>
        /// 货品当前库存数量
        /// </summary>
        public virtual double? GoodsQty { get; set; }
    }
}
