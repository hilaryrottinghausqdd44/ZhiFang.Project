
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.WebAssist
{
    /// <summary>
    /// 院感统计报表
    /// 按科室
    /// 按季度
    /// 评价报告表
    /// </summary>
    [DataContract]
    public class GKOfDeptEvaluation : InfectionDtlVO
    {
        public GKOfDeptEvaluation() { }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "科室编码", ShortCode = "DeptId", Desc = "科室编码", ContextType = SysDic.All, Length = 8)]
        public virtual long? DeptId { get; set; }

        [DataMember]
        [DataDesc(CName = "科室名称", ShortCode = "DeptCName", Desc = "科室名称", ContextType = SysDic.All, Length = 50)]
        public virtual string DeptCName { get; set; }

        [DataMember]
        [DataDesc(CName = "季度", ShortCode = "Quarterly", Desc = "季度", ContextType = SysDic.All, Length = 50)]
        public virtual string Quarterly { get; set; }

        [DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "监测日期", ShortCode = "MonitoringDate", Desc = "监测日期", ContextType = SysDic.All, Length =20)]
        public virtual string MonitoringDate { get; set; }

        /// <summary>
        /// 场所：只有监测类型为“空气培养”的才需要，读取的是空气培养的房间名称;
        /// </summary>
        [DataMember]
        [DataDesc(CName = "场所", ShortCode = "Place", Desc = "场所", ContextType = SysDic.All, Length = 50)]
        public virtual string Place { get; set; }

        /// <summary>
        /// 医疗器材名称:取的是监测类型的第一项记录项显示名称;
        /// </summary>
        [DataMember]
        [DataDesc(CName = "医疗器材名称", ShortCode = "MedicalEquipment", Desc = "医疗器材名称", ContextType = SysDic.All, Length = 50)]
        public virtual string MedicalEquipment{ get; set; }

        /// <summary>
        /// 微生物学结果: 取的是监测类型的第一项记录项的检验描述结果;
        /// </summary>
        [DataMember]
        [DataDesc(CName = "微生物学结果", ShortCode = "TestResult", Desc = "微生物学结果", ContextType = SysDic.All, Length = 200)]
        public virtual string TestResult { get; set; }

        /// <summary>
        /// 细菌总数: 取的是监测类型的第一项记录项的细菌数结果;
        /// </summary>
        [DataMember]
        [DataDesc(CName = "细菌总数", ShortCode = "MicroCount", Desc = "细菌总数", ContextType = SysDic.All, Length = 200)]
        public virtual string MicroCount{ get; set; }

        /// <summary>
        /// 取评估判定结果
        /// </summary>
        [DataMember]
        [DataDesc(CName = "检测评价", ShortCode = "TestEvaluation", Desc = "检测评价", ContextType = SysDic.All, Length = 200)]
        public virtual string TestEvaluation{ get; set; }

        /// <summary>
        /// 取的是登记申请单的采样人
        /// </summary>
        [DataMember]
        [DataDesc(CName = "监测者", ShortCode = "TestCName", Desc = "监测者", ContextType = SysDic.All, Length = 50)]
        public virtual string TestCName { get; set; }

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = 500)]
        public virtual string Memo { get; set; }
    }
}
