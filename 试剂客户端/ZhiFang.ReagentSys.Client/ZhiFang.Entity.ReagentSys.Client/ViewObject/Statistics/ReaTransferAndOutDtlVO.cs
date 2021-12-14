using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client.ViewObject.Statistics
{
    /// <summary>
    /// 移库领用及使用返回统计VO
    /// </summary>
    public class ReaTransferAndOutDtlVO
    {
        public ReaTransferAndOutDtlVO() { }
        /// <summary>
        /// 货品编码
        /// </summary>
        public virtual string ReaGoodsNo { get; set; }
        /// <summary>
        /// 货品名称
        /// </summary>
        public virtual string GoodsCName { get; set; }
        /// <summary>
        /// 包装单位
        /// </summary>
        public virtual string GoodsUnit { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public virtual string UnitMemo { get; set; }
        /// <summary>
        /// 领用科室
        /// </summary>
        public virtual string DeptName { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public virtual string ReaCompanyName { get; set; }
        /// <summary>
        /// 批号
        /// </summary>
        public virtual string LotNo { get; set; }
        /// <summary>
        /// 效期
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual DateTime? InvalidDate { get; set; }
        /// <summary>
        /// 移库领用数
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? TransferCount { get; set; }
        /// <summary>
        /// 出库(上机)使用数量
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? TestEquipOutCount { get; set; }
        /// <summary>
        /// 当前库存量
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? CurQtyCount { get; set; }
    }
}
