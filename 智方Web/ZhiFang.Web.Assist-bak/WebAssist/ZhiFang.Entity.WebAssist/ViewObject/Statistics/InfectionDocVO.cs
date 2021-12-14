
using Newtonsoft.Json;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.WebAssist
{
    /// <summary>
    /// 院感统计主单VO
    /// 供评价报告表使用
    /// </summary>
    [DataContract]
    public class InfectionDocVO 
    {
        /// <summary>
        /// 空值
        /// </summary>
        public string NullV { get; set; }

        /// <summary>
        /// 样例:■全院   □院感科监测   □科室监测
        /// </summary>
        public InfectionDocVO() { }
        [DataMember]
        [DataDesc(CName = "监测级别", ShortCode = "MonitorType", Desc = "监测级别", ContextType = SysDic.All, Length = 20)]
        public virtual string MonitorType { get; set; }

        [DataMember]
        [DataDesc(CName = "科室名称", ShortCode = "DeptCName", Desc = "科室名称", ContextType = SysDic.All, Length = 50)]
        public virtual string DeptCName { get; set; }

        [DataMember]
        [DataDesc(CName = "监测类型", ShortCode = "RecordTypeNo", Desc = "监测类型", ContextType = SysDic.All, Length = 50)]
        public virtual string RecordTypeNo { get; set; }

        [DataMember]
        [DataDesc(CName = "监测类型名称", ShortCode = "RecordTypeCName", Desc = "监测类型名称", ContextType = SysDic.All, Length = 50)]
        public virtual string RecordTypeCName { get; set; }

        [DataMember]
        [DataDesc(CName = "开始时间", ShortCode = "StartDate", Desc = "开始时间", ContextType = SysDic.All, Length = 20)]
        public virtual string StartDate { get; set; }

        [DataMember]
        [DataDesc(CName = "结束时间", ShortCode = "EndDate", Desc = "结束时间", ContextType = SysDic.All, Length = 20)]
        public virtual string EndDate { get; set; }

        /// <summary>
        /// 2020
        /// </summary>
        [DataMember]
        [DataDesc(CName = "统计年", ShortCode = "Year", Desc = "统计年", ContextType = SysDic.All, Length = 20)]
        public virtual string Year { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [DataDesc(CName = "统计季度", ShortCode = "Quarterly", Desc = "统计季度", ContextType = SysDic.All, Length = 50)]
        public virtual string Quarterly { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [DataDesc(CName = "统计年加季度", ShortCode = "YearQuarterly", Desc = "统计年加季度", ContextType = SysDic.All, Length = 50)]
        public virtual string YearQuarterly { get; set; }

        /// <summary>
        /// 样例：2020年1-9月
        /// </summary>
        [DataMember]
        [DataDesc(CName = "统计年月", ShortCode = "YMRange", Desc = "统计年月", ContextType = SysDic.All, Length = 20)]
        public virtual string YMRange { get; set; }
        
        /// <summary>
        /// 2020-01-01 - 2020-11-04
        /// </summary>
        [DataMember]
        [DataDesc(CName = "时间范围", ShortCode = "DateRange", Desc = "时间范围", ContextType = SysDic.All, Length = 40)]
        public virtual string DateRange { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "总共采样数", ShortCode = "SumSamplesCount", Desc = "总共采样数", ContextType = SysDic.All, Length = 8)]
        public virtual double? SumSamplesCount { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "总共合格数", ShortCode = "SumQualifiedCount", Desc = "总共合格数", ContextType = SysDic.All, Length = 8)]
        public virtual double? SumQualifiedCount { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "总共合格率%", ShortCode = "SumQualifiedRate", Desc = "总共合格率%", ContextType = SysDic.All, Length = 8)]
        public virtual double? SumQualifiedRate { get; set; }

        /// <summary>
        /// 样例:
        /// 卫生评价：
        ///     按照《医院消毒卫生标准》或《消毒与灭菌效果的评价方法与标准卫生标准》评价，本次监测共检测医务人员1705人，合格1620人，合格率95.01%。
        /// 需要补全 共检测医务人员1705人，合格1620人，合格率95.01%。
        /// </summary>
        [DataMember]
        [DataDesc(CName = "卫生评价", ShortCode = "HygieneInfo", Desc = "卫生评价", ContextType = SysDic.All, Length = 300)]
        public virtual string HygieneInfo { get; set; }

    }
}
