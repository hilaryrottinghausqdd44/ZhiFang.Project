using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.WebAssist
{
    /// <summary>
    /// 院感统计明细VO
    /// </summary>
    [DataContract]
    public class InfectionDtlVO
    {
        [DataMember]
        [DataDesc(CName = "DispOrder", ShortCode = "DispOrder", Desc = "DispOrder", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "手卫生采样数", ShortCode = "HandHSamplesCount", Desc = "手卫生采样数", ContextType = SysDic.All, Length = 8)]
        public virtual double? HandHSamplesCount { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "手卫生合格数", ShortCode = "HandHQualifiedCount", Desc = "合格数", ContextType = SysDic.All, Length = 8)]
        public virtual double? HandHQualifiedCount { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "手卫生合格率%", ShortCode = "HandHQualifiedRate", Desc = "手卫生合格率%", ContextType = SysDic.All, Length = 8)]
        public virtual double? HandHQualifiedRate { get; set; }


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "空气培养采样数", ShortCode = "AirCSamplesCount", Desc = "空气培养采样数", ContextType = SysDic.All, Length = 8)]
        public virtual double? AirCSamplesCount { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "空气培养合格数", ShortCode = "AirCQualifiedCount", Desc = "空气培养合格数", ContextType = SysDic.All, Length = 8)]
        public virtual double? AirCQualifiedCount { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "空气培养合格率%", ShortCode = "AirCQualifiedRate", Desc = "空气培养合格率%", ContextType = SysDic.All, Length = 8)]
        public virtual double? AirCQualifiedRate { get; set; }


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "物体表面采样数", ShortCode = "SurfaceSamplesCount", Desc = "物体表面采样数", ContextType = SysDic.All, Length = 8)]
        public virtual double? SurfaceSamplesCount { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "物体表面合格数", ShortCode = "SurfaceQualifiedCount", Desc = "物体表面合格数", ContextType = SysDic.All, Length = 8)]
        public virtual double? SurfaceQualifiedCount { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "物体表面合格率%", ShortCode = "SurfaceQualifiedRate", Desc = "物体表面合格率%", ContextType = SysDic.All, Length = 8)]
        public virtual double? SurfaceQualifiedRate { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "消毒剂采样数", ShortCode = "DisinfectantSamplesCount", Desc = "消毒剂采样数", ContextType = SysDic.All, Length = 8)]
        public virtual double? DisinfectantSamplesCount { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "消毒剂合格数", ShortCode = "DisinfectantQualifiedCount", Desc = "消毒剂合格数", ContextType = SysDic.All, Length = 8)]
        public virtual double? DisinfectantQualifiedCount { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "消毒剂合格率%", ShortCode = "DisinfectantQualifiedRate", Desc = "消毒剂合格率%", ContextType = SysDic.All, Length = 8)]
        public virtual double? DisinfectantQualifiedRate { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "透析液及透析用水采样数", ShortCode = "DialysateSamplesCount", Desc = "透析液及透析用水采样数", ContextType = SysDic.All, Length = 8)]
        public virtual double? DialysateSamplesCount { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "透析液及透析用水合格数", ShortCode = "DialysateQualifiedCount", Desc = "透析液及透析用水合格数", ContextType = SysDic.All, Length = 8)]
        public virtual double? DialysateQualifiedCount { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "透析液及透析用水合格率%", ShortCode = "DialysateQualifiedRate", Desc = "透析液及透析用水合格率%", ContextType = SysDic.All, Length = 8)]
        public virtual double? DialysateQualifiedRate { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医疗器材采样数", ShortCode = "MedicalESamplesCount", Desc = "医疗器材采样数", ContextType = SysDic.All, Length = 8)]
        public virtual double? MedicalESamplesCount { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医疗器材合格数", ShortCode = "MedicalEQualifiedCount", Desc = "医疗器材合格数", ContextType = SysDic.All, Length = 8)]
        public virtual double? MedicalEQualifiedCount { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医疗器材合格率%", ShortCode = "MedicalEQualifiedRate", Desc = "医疗器材合格率%", ContextType = SysDic.All, Length = 8)]
        public virtual double? MedicalEQualifiedRate { get; set; }


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "污水采样数", ShortCode = "SewageSamplesCount", Desc = "污水采样数", ContextType = SysDic.All, Length = 8)]
        public virtual double? SewageSamplesCount { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "污水合格数", ShortCode = "SewageQualifiedCount", Desc = "污水合格数", ContextType = SysDic.All, Length = 8)]
        public virtual double? SewageQualifiedCount { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "污水合格率%", ShortCode = "SewageQualifiedRate", Desc = "污水合格率%", ContextType = SysDic.All, Length = 8)]
        public virtual double? SewageQualifiedRate { get; set; }



        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "其他采样数", ShortCode = "OtherSamplesCount", Desc = "其他采样数", ContextType = SysDic.All, Length = 8)]
        public virtual double? OtherSamplesCount { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "其他合格数", ShortCode = "OtherQualifiedCount", Desc = "其他合格数", ContextType = SysDic.All, Length = 8)]
        public virtual double? OtherQualifiedCount { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "其他合格率%", ShortCode = "OtherQualifiedRate", Desc = "其他合格率%", ContextType = SysDic.All, Length = 8)]
        public virtual double? OtherQualifiedRate { get; set; }

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

    }
}
