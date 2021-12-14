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
    /// 按最大包装单位综合统计VO
    /// </summary>
    public class ReaGoodsOfMaxGonvertQtyVO
    {
        public ReaGoodsOfMaxGonvertQtyVO() { }

        public virtual string ReaGoodsNo { get; set; }
        public virtual string GoodsCName { get; set; }
        public virtual string GoodsUnit { get; set; }
        public virtual string UnitMemo { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public virtual string ProdOrgName { get; set; }
        /// <summary>
        /// 一级分类
        /// </summary>
        public virtual string GoodsClass { get; set; }
        /// <summary>
        /// 二级分类
        /// </summary>
        public virtual string GoodsClassType { get; set; }
        /// <summary>
        /// 所属部门
        /// </summary>
        public virtual string DeptName { get; set; }
        /// <summary>
        /// 适用机型
        /// </summary>
        public virtual string SuitableType { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public virtual string ReaCompanyName { get; set; }
        /// <summary>
        /// 采购单价(元)
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? Price { get; set; }
        /// <summary>
        /// 理论测试数(取机构货品)
        /// </summary>
        public virtual int TestCount { get; set; }
        /// <summary>
        /// 理论月用量(取机构货品)
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? MonthlyUsage { get; set; }
        /// <summary>
        /// 库存低限
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? StoreLower { get; set; }
        /// <summary>
        /// 库存上限
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? StoreUpper { get; set; }
        /// <summary>
        /// 机构货品订货数
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? OrderCount { get; set; }
        /// <summary>
        /// 机构货品验收接收数
        /// </summary>
        public virtual double? AcceptCount { get; set; }
        /// <summary>
        /// 机构货品验收拒收数
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? RefuseCount { get; set; }
        /// <summary>
        /// 机构货品验收总数(接收数+拒收数)
        /// </summary>
        public virtual double? ConfirmCount { get; set; }
        /// <summary>
        /// 机构货品入库数
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? InCount { get; set; }
        /// <summary>
        /// 当前库存量
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? CurQtyCount { get; set; }
        /// <summary>
        /// 当前订货未到货数量(订货总数-已验收总数)
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? UndeliveredCount { get; set; }
        /// <summary>
        /// 移库领用数
        /// </summary>
        public virtual double? TransferCount { get; set; }
        /// <summary>
        /// 出库(上机)使用数量
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? TestEquipOutCount { get; set; }
        /// <summary>
        /// 项目总收入
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? SumTotal { get; set; }
        /// <summary>
        /// 对应项目测试数(取仪器项目试剂关系信息的TestCount)
        /// </summary>
        public virtual int EquipTestCount { get; set; }
    }
}
