using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client.ViewObject.Statistics
{
    [DataContract]
    public class EChartsVO
    {
        public EChartsVO() { }

        /// <summary>
        /// 库房ID
        /// </summary>
        [DataMember]
        public virtual string StorageID { get; set; }
        /// <summary>
        /// 库房名称
        /// </summary>
        [DataMember]
        public virtual string StorageName { get; set; }
        /// <summary>
        /// 供应商Id
        /// </summary>
        [DataMember]
        public virtual string ReaCompanyID { get; set; }
        /// <summary>
        /// 供应商编码
        /// </summary>
        [DataMember]
        public virtual string ReaCompCode { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        [DataMember]
        public virtual string ReaCompanyName { get; set; }
        /// <summary>
        /// (所有供货商/库房/等)总金额
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? AllSumTotal { get; set; }
        /// <summary>
        /// (某一供货商/库房/等)总金额
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? SumTotal { get; set; }
        /// <summary>
        /// (某一供货商/库房/等)金额占总比=SumTotal/AllSumTotal*100
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? SumTotalPercent { get; set; }
        /// <summary>
        /// (所有供货商/库房/等)总数量
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? AllGoodsQty { get; set; }
        /// <summary>
        /// (某一供货商/库房/等)总数量
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? GoodsQty { get; set; }
        /// <summary>
        /// (某一供货商/库房/等)总数量占总比=GoodsQty/AllGoodsQty*100
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? GoodsQtyPercent { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        [DataMember]
        public virtual string ProdOrgName { get; set; }
        /// <summary>
        /// 一级分类
        /// </summary>
        [DataMember]
        public virtual string GoodsClass { get; set; }
        /// <summary>
        /// 二级分类
        /// </summary>
        [DataMember]
        public virtual string GoodsClassType { get; set; }
        /// <summary>
        /// 仪器ID
        /// </summary>
        [DataMember]
        public virtual string TestEquipID { get; set; }
        /// <summary>
        /// 仪器名称
        /// </summary>
        [DataMember]
        public virtual string TestEquipName { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        [DataMember]
        public virtual string DeptID { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        [DataMember]
        public virtual string DeptName { get; set; }
        /// <summary>
        /// 检测类型
        /// </summary>
        [DataMember]
        public virtual string TestType { get; set; }
        [DataMember]
        public virtual string TestItemID { get; set; }
        [DataMember]
        public virtual string TestItemCName { get; set; }
        [DataMember]
        public virtual string ReaGoodsNo { get; set; }
        [DataMember]
        public virtual string GoodsCName { get; set; }
        [DataMember]
        public virtual string GoodsUnit { get; set; }
        [DataMember]
        public virtual string UnitMemo { get; set; }
    }
}
