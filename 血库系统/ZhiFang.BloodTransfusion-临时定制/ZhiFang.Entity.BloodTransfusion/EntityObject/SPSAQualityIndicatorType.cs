using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{

    /// <summary>
    /// 获取质量指标类型统计数据源实体
    /// </summary>
    [DataContract]
    [DataDesc(CName = "获取质量指标类型统计数据源实体", ClassCName = "SPSAQualityIndicatorType", ShortCode = "SPSAQualityIndicatorType", Desc = "获取质量指标类型统计数据源实体")]
    public class SPSAQualityIndicatorType //: BaseEntity
    {
        public SPSAQualityIndicatorType() { }

        /// <summary>
        /// 开始日期
        /// </summary>
        [DataMember]
        public virtual string StartDate { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        [DataMember]
        public virtual string EndDate { get; set; }
        /// <summary>
        /// 年份:某一质量指标类型的某一年
        /// </summary>
        [DataMember]
        public virtual string Year { get; set; }
        /// <summary>
        /// 季度:某一年的某一季度
        /// </summary>
        [DataMember]
        public virtual string Quarter { get; set; }
        /// <summary>
        /// 月份:某一年的某一月
        /// </summary>
        [DataMember]
        public virtual string Month { get; set; }
        /// <summary>
        /// 样本条码
        /// </summary>
        [DataMember]
        public virtual string SerialNo { get; set; }
        /// <summary>
        /// 质量指标分类类型Id,对应QualityIndicatorType的ID
        /// </summary>
        [DataMember]
        public virtual string QIndicatorTypeId { get; set; }
        /// <summary>
        /// 质量指标分类类型名称
        /// </summary>
        [DataMember]
        public virtual string QIndicatorTypeCName { get; set; }
        /// <summary>
        /// 质量指标类型字典Id
        /// </summary>
        [DataMember]
        public virtual string PhrasesWatchClassID { get; set; }
        /// <summary>
        /// 质量指标类型字典名称
        /// </summary>
        [DataMember]
        public virtual string PhrasesWatchClassCName { get; set; }
        /// <summary>
        /// <summary>
        /// 质量指标字典Id
        /// </summary>
        [DataMember]
        public virtual string RefuseID { get; set; }
        /// <summary>
        /// 质量指标字典名称
        /// </summary>
        [DataMember]
        public virtual string RefuseCName { get; set; }
        /// <summary>
        /// 就诊类型ID
        /// </summary>
        [DataMember]
        public virtual string SickTypeNo { get; set; }
        /// <summary>
        /// 就诊类型名称
        /// </summary>
        [DataMember]
        public virtual string SickTypeCName { get; set; }
        /// <summary>
        /// 样本类型编码
        /// </summary>
        [DataMember]
        public virtual string SampleTypeNo { get; set; }
        /// <summary>
        /// 样本类型名称
        /// </summary>
        [DataMember]
        public virtual string SampleTypeCName { get; set; }
        /// <summary>
        /// 科室编码
        /// </summary>
        [DataMember]
        public virtual string DeptNo { get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>
        [DataMember]
        public virtual string DeptCName { get; set; }
        /// <summary>
        /// 医生编码
        /// </summary>
        [DataMember]
        public virtual string DoctorNo { get; set; }
        /// <summary>
        /// 医生名称
        /// </summary>
        [DataMember]
        public virtual string DoctorCName { get; set; }
        /// <summary>
        /// 采样人ID
        /// </summary>
        [DataMember]
        public virtual string CollecterID { get; set; }
        /// <summary>
        /// 采样人名称
        /// </summary>
        [DataMember]
        public virtual string Collecter { get; set; }
        /// <summary>
        /// 采样日期时间
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual DateTime? CollectTime { get; set; }
        /// <summary>
        /// 送检人名称
        /// </summary>
        [DataMember]
        public virtual string NurseSendCarrier { get; set; }
        /// <summary>
        /// 送检日期时间
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual DateTime? NurseSendTime { get; set; }
        /// <summary>
        /// 条码生成时间:以此数据项作为日期范围查询项
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual DateTime? FlagDateDelete { get; set; }
        /// <summary>
        /// 病区编码
        /// </summary>
        [DataMember]
        public virtual string DistrictNo { get; set; }
        /// <summary>
        /// 病区名称
        /// </summary>
        [DataMember]
        public virtual string DistrictCName { get; set; }
        /// <summary>
        /// 病床编码
        /// </summary>
        [DataMember]
        public virtual string WardNo { get; set; }
        /// <summary>
        /// 病床名称
        /// </summary>
        [DataMember]
        public virtual string WardCName { get; set; }
        /// <summary>
        /// 不合格标本数
        /// 某一段日期范围内某一质量指标类型的某一统计纬度的标本数
        /// </summary>
        [DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        public virtual double? FailedAmount { get; set; }
        /// <summary>
        ///不合格标本总数
        ///某一段日期范围内某一统计纬度的所有质量指标类型的标本总数
        /// </summary>
        [DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        public virtual double? FailedTotal { get; set; }
        /// <summary>
        ///不合格占比
        ///不合格标本数/不合格标本总数*100%
        /// </summary>
        [DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        public virtual double? FailedRate { get; set; }
        /// <summary>
        ///标本总量
        ///某一段日期范围内某一统计纬度的所有标本总数
        /// </summary>
        [DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        public virtual double? SpecimenTotal { get; set; }
        /// <summary>
        ///全院标本总量
        ///某一段日期范围内某一统计纬度的所有标本总数
        /// </summary>
        [DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        public virtual double? AllTotal { get; set; }
        /// <summary>
        ///标本总量占比
        ///某一段日期范围内的所有标本总数
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual double? SpecimenTotalRate { get; set; }

    }
}
