using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar.ViewObject.Response
{
    [DataContract]
    [DataDesc(CName = "采样分管医嘱分组VO", ClassCName = "LisOrderFormItemVo", ShortCode = "LisOrderFormItemVo", Desc = "采样分管医嘱分组VO")]
    public class LisOrderFormItemVo
    {
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public long OrderFormID { get; set; }

        [DataMember]
        public string OrderFormNo { get; set; }

        [DataMember]
        public string CName { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public long PatID { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public long SickTypeID { get; set; }

        /// <summary>
        /// 是否分管
        /// </summary>
        [DataMember]
        public bool IsGrouping { get; set; }

        /// <summary>
        /// 条码生成方式：1.从HIS申请单获取。2.LIS生成
        /// </summary>
        [DataMember]
        public int SerialGenerationType { get; set; }


        [DataMember]
        public List<GroupingOrderItemVo> groupingOrderItemVos { get; set; }
    }

    [DataContract]
    public class GroupingOrderItemVo
    {
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public long OrderItemID { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public long OrdersItemID { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public long OrderFormID { get; set; }

        [DataMember]
        public string OrderFormNo { get; set; }

        [DataMember]
        public int OrderItemExecFlag { get; set; }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public long SickTypeID { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public long? ItemStatusID { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public long? SampleTypeID { get; set; }

        /// <summary>
        /// 采样组ID
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public long SamplingGroupID { get; set; }

        /// <summary>
        /// 项目排序
        /// </summary>
        [DataMember]
        public int CollectSort { get; set; }

        [DataMember]
        public string ItemCName { get; set; }

        [DataMember]
        public bool IsGroupItem { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public long? GroupItemID { get; set; }
        /// <summary>
        /// 是否合并检验报告
        /// </summary>
        [DataMember]
        public bool IsAutoUnion { get; set; }
        /// <summary>
        /// 是否属于多个采样组
        /// </summary>
        [DataMember]
        public bool IsMoreSamplingGroup { get; set; }
        /// <summary>
        /// 分管类型属性:1.分管成功，2.无采样组，3.没有默认采样组,4.不参与分管
        /// </summary>
        [DataMember]
        public string SampleGroupingType { get; set; }
        /// <summary>
        /// 虚拟采样量
        /// </summary>
        [DataMember]
        public int VirtualItemNo { get; set; }

        /// <summary>
        /// 条码号
        /// </summary>
        [DataMember] 
        public string BarCode { get; set; }
        /// <summary>
        /// 是否预制条码
        /// </summary>
        [DataMember] 
        public bool IsPrep { get; set; }
        /// <summary>
        /// 采样部位
        /// </summary>
        [DataMember]
        public string CollectPart { get; set; }
        [DataMember]
        public DateTime PartitionDate { get; set; }

        [DataMember]
        public float Charge { get; set; }

        [DataMember]
        public int IsCheckFee { get; set; }

    }

}
