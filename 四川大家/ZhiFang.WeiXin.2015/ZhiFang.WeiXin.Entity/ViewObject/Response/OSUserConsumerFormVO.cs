using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity.ViewObject.Response
{
    [DataContract]
    [DataDesc(CName = "用户订单VO", ClassCName = "VO_OSUserConsumerFormVO", ShortCode = "VO_OSUserConsumerFormVO", Desc = "用户订单VO")]
    public class OSUserConsumerFormVO : BaseEntity
    {
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "区域ID", ShortCode = "AreaID", Desc = "区域ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? AD { get; set; }

        [DataMember]
        [DataDesc(CName = "医院ID", ShortCode = "HospitalID", Desc = "医院ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? HD { get; set; }

        [DataMember]
        [DataDesc(CName = "消费单编号", ShortCode = "OSUserConsumerFormCode", Desc = "消费单编号", ContextType = SysDic.All, Length = 30)]
        public virtual string UCFC
        {
            get;
            set;
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "申请单ID", ShortCode = "NRQFID", Desc = "申请单ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? NRQFID
        {
            get;
            set;
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DOFID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? DOFID
        {
            get;
            set;
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医生账户信息ID", ShortCode = "DoctorAccountID", Desc = "医生账户信息ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? DAID
        {
            get;
            set;
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医生微信ID", ShortCode = "WeiXinUserID", Desc = "医生微信ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? WeiXinUserID
        {
            get;
            set;
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DoctorOpenID", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string DoctorOpenID
        {
            get;
            set;
        }

        [DataMember]
        [DataDesc(CName = "医生姓名", ShortCode = "DoctorName", Desc = "医生姓名", ContextType = SysDic.All, Length = 20)]
        public virtual string DN
        {
            get;
            set;
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "市场价格", ShortCode = "MarketPrice", Desc = "市场价格", ContextType = SysDic.All, Length = 8)]
        public virtual double? MP
        {
            get;
            set;
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "大家价格", ShortCode = "GreatMasterPrice", Desc = "大家价格", ContextType = SysDic.All, Length = 8)]
        public virtual double? GMP
        {
            get;
            set;
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "折扣价格", ShortCode = "DiscountPrice", Desc = "折扣价格", ContextType = SysDic.All, Length = 8)]
        public virtual double? DP
        {
            get;
            set;
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "折扣率", ShortCode = "Discount", Desc = "折扣率", ContextType = SysDic.All, Length = 8)]
        public virtual double? Discount
        {
            get;
            set;
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "实际金额", ShortCode = "Price", Desc = "实际金额", ContextType = SysDic.All, Length = 8)]
        public virtual double? P
        {
            get;
            set;
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "咨询费", ShortCode = "AdvicePrice", Desc = "咨询费", ContextType = SysDic.All, Length = 8)]
        public virtual double? AP
        {
            get;
            set;
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "用户账户信息ID", ShortCode = "UserAccountID", Desc = "用户账户信息ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? UAID
        {
            get;
            set;
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "用户微信ID", ShortCode = "UserWeiXinUserID", Desc = "用户微信ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? UWXUID
        {
            get;
            set;
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医生奖金结算记录ID", ShortCode = "OSDoctorBonusID", Desc = "医生奖金结算记录ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? OSDBID
        {
            get;
            set;
        }

        [DataMember]
        [DataDesc(CName = "用户姓名", ShortCode = "UserName", Desc = "用户姓名", ContextType = SysDic.All, Length = 20)]
        public virtual string UN
        {
            get;
            set;
        }

        [DataMember]
        [DataDesc(CName = "用户OpenID", ShortCode = "UserOpenID", Desc = "用户OpenID", ContextType = SysDic.All, Length = 50)]
        public virtual string UOID
        {
            get;
            set;
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "订单状态", ShortCode = "Status", Desc = "订单状态", ContextType = SysDic.All, Length = 8)]
        public virtual long? Status
        {
            get;
            set;
        }

        [DataMember]
        [DataDesc(CName = "消费码", ShortCode = "PayCode", Desc = "消费码", ContextType = SysDic.All, Length = 50)]
        public virtual string PC
        {
            get;
            set;
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "采血站点ID", ShortCode = "OrgID", Desc = "采血站点ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? OID
        {
            get;
            set;
        }

        [DataMember]
        [DataDesc(CName = "采血站点组织机构代码", ShortCode = "WeblisOrgID", Desc = "采血站点组织机构代码", ContextType = SysDic.All, Length = 50)]
        public virtual string WeblisOrgID
        {
            get;
            set;
        }

        [DataMember]
        [DataDesc(CName = "采血站点名称", ShortCode = "WeblisOrgName", Desc = "采血站点名称", ContextType = SysDic.All, Length = 50)]
        public virtual string WeblisOrgName
        {
            get;
            set;
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "EmpID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? EmpID
        {
            get;
            set;
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EmpName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string EmpName
        {
            get;
            set;
        }

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = 500)]
        public virtual string Memo
        {
            get;
            set;
        }

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get;
            set;
        }

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
        {
            get;
            set;
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get;
            set;
        }
    }
}
