using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ZhiFang.Entity.Base;
using Newtonsoft.Json;

namespace ZhiFang.WeiXin.Entity.ViewObject.Response
{
    #region OSDoctorOrderFormVO

    /// <summary>
    /// OSDoctorOrderForm object for NHibernate mapped table 'OS_DoctorOrderForm'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "医生医嘱单VO", ClassCName = "VO_OSDoctorOrderForm", ShortCode = "VO_OSDoctorOrderForm", Desc = "医生医嘱单VO")]
    public class OSDoctorOrderFormVO : BaseEntity
    {

        #region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "区域ID", ShortCode = "AreaID", Desc = "区域ID", ContextType = SysDic.All, Length = 8)]
        public virtual long AD { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医院ID", ShortCode = "HospitalID", Desc = "医院ID", ContextType = SysDic.All, Length = 8)]
        public virtual long HD { get; set; }

        [DataMember]
        [DataDesc(CName = "医院名称", ShortCode = "HospitalName", Desc = "医院名称", ContextType = SysDic.All, Length = 20)]
        public virtual string HN { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医生账户信息ID", ShortCode = "DoctorAccountID", Desc = "医生账户信息ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? DAD { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医生微信ID", ShortCode = "DoctorWeiXinUserID", Desc = "医生微信ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? WXD { get; set; }

        [DataMember]
        [DataDesc(CName = "医生姓名", ShortCode = "DoctorName", Desc = "医生姓名", ContextType = SysDic.All, Length = 20)]
        public virtual string DN { get; set; }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DoctorOpenID", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string DOD { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "用户账户信息ID", ShortCode = "UserAccountID", Desc = "用户账户信息ID", ContextType = SysDic.All, Length = 8)]
        public virtual long UAD { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "用户微信ID", ShortCode = "UserWeiXinUserID", Desc = "用户微信ID", ContextType = SysDic.All, Length = 8)]
        public virtual long UWD { get; set; }

        [DataMember]
        [DataDesc(CName = "用户姓名", ShortCode = "UserName", Desc = "用户姓名", ContextType = SysDic.All, Length = 20)]
        public virtual string UN { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "用户OpenID", ShortCode = "UserOpenID", Desc = "用户OpenID", ContextType = SysDic.All, Length = 50)]
        public virtual string UOD { get; set; }

        [DataMember]
        [DataDesc(CName = "特征码", ShortCode = "FeatureCode", Desc = "特征码", ContextType = SysDic.All, Length = 20)]
        public virtual string FC { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医嘱单状态", ShortCode = "Status", Desc = "医嘱单状态", ContextType = SysDic.All, Length = 8)]
        public virtual long SS { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "年龄", ShortCode = "Age", Desc = "年龄", ContextType = SysDic.All, Length = 8)]
        public virtual long Age { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "年龄单位ID", ShortCode = "AgeUnitID", Desc = "年龄单位ID", ContextType = SysDic.All, Length = 8)]
        public virtual long AUD { get; set; }

        [DataMember]
        [DataDesc(CName = "年龄单位名称", ShortCode = "AgeUnitName", Desc = "年龄单位名称", ContextType = SysDic.All, Length = 20)]
        public virtual string AUN { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "性别ID", ShortCode = "SexID", Desc = "性别ID", ContextType = SysDic.All, Length = 8)]
        public virtual long SD { get; set; }

        [DataMember]
        [DataDesc(CName = "性别名称", ShortCode = "SexName", Desc = "性别名称", ContextType = SysDic.All, Length = 20)]
        public virtual string SN { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "科室ID", ShortCode = "DeptID", Desc = "科室ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? DD { get; set; }

        [DataMember]
        [DataDesc(CName = "科室名称", ShortCode = "DeptName", Desc = "科室名称", ContextType = SysDic.All, Length = 20)]
        public virtual string DPN { get; set; }

        [DataMember]
        [DataDesc(CName = "病历号", ShortCode = "PatNo", Desc = "病历号", ContextType = SysDic.All, Length = 20)]
        public virtual string PN { get; set; }
        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = 500)]
        public virtual string MM { get; set; }

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DO { get; set; }

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IU { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DUT { get; set; }
        [DataMember]
        [DataDesc(CName = "采样标记", ShortCode = "CF", Desc = "采样标记", ContextType = SysDic.All, Length = 500)]
        public bool CF { get; set; }
        [DataMember]
        [DataDesc(CName = "采样金额", ShortCode = "CP", Desc = "采样金额", ContextType = SysDic.All, Length = 500)]
        public double CP { get; set; }
        [DataMember]
        [DataDesc(CName = "医嘱单类型", ShortCode = "TI", Desc = "医嘱单类型", ContextType = SysDic.All, Length = 500)]
        public long? TI { get; set; }
        [DataMember]
        [DataDesc(CName = "医嘱单类型名称", ShortCode = "TN", Desc = "医嘱单类型名称", ContextType = SysDic.All, Length = 500)]
        public string TN { get; set; }



        #endregion
    }
    #endregion
}
