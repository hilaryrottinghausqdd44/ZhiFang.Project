using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity.ViewObject.Request
{
    [DataContract]
    [DataDesc(CName = "医生开单", ClassCName = "OSDoctorOrderFormVO", ShortCode = "OSDoctorOrderFormVO", Desc = "医生开单")]
    public class OSDoctorOrderFormVO
    {
        #region Member Variables

        protected long _areaID;
        protected long _hospitalID;
        protected string _hospitalName;
        protected long _doctorAccountID;
        protected long _doctorWeiXinUserID;
        protected string _doctorName;
        protected string _doctorOpenID;
        protected long _userAccountID;
        protected long _userWeiXinUserID;
        protected string _userName;
        protected string _userOpenID;
        //protected string _featureCode;
        protected long _status;
        protected long _age;
        protected long _ageUnitID;
        protected string _ageUnitName;
        protected long _sexID;
        protected string _sexName;
        protected long _deptID;
        protected string _deptName;
        protected string _patNo;
        protected string _memo;
        protected bool _CollectionFlag;
        protected double _CollectionPrice;


        #endregion

        #region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "区域ID", ShortCode = "AreaID", Desc = "区域ID", ContextType = SysDic.All, Length = 8)]
        public virtual long AreaID
        {
            get { return _areaID; }
            set { _areaID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医院ID", ShortCode = "HospitalID", Desc = "医院ID", ContextType = SysDic.All, Length = 8)]
        public virtual long HospitalID
        {
            get { return _hospitalID; }
            set { _hospitalID = value; }
        }

        [DataMember]
        [DataDesc(CName = "医院名称", ShortCode = "HospitalName", Desc = "医院名称", ContextType = SysDic.All, Length = 20)]
        public virtual string HospitalName
        {
            get { return _hospitalName; }
            set
            {
                _hospitalName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医生账户信息ID", ShortCode = "DoctorAccountID", Desc = "医生账户信息ID", ContextType = SysDic.All, Length = 8)]
        public virtual long DoctorAccountID
        {
            get { return _doctorAccountID; }
            set { _doctorAccountID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医生微信ID", ShortCode = "DoctorWeiXinUserID", Desc = "医生微信ID", ContextType = SysDic.All, Length = 8)]
        public virtual long DoctorWeiXinUserID
        {
            get { return _doctorWeiXinUserID; }
            set { _doctorWeiXinUserID = value; }
        }

        [DataMember]
        [DataDesc(CName = "医生姓名", ShortCode = "DoctorName", Desc = "医生姓名", ContextType = SysDic.All, Length = 20)]
        public virtual string DoctorName
        {
            get { return _doctorName; }
            set
            {
                _doctorName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DoctorOpenID", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string DoctorOpenID
        {
            get { return _doctorOpenID; }
            set
            {
                _doctorOpenID = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "用户账户信息ID", ShortCode = "UserAccountID", Desc = "用户账户信息ID", ContextType = SysDic.All, Length = 8)]
        public virtual long UserAccountID
        {
            get { return _userAccountID; }
            set { _userAccountID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "用户微信ID", ShortCode = "UserWeiXinUserID", Desc = "用户微信ID", ContextType = SysDic.All, Length = 8)]
        public virtual long UserWeiXinUserID
        {
            get { return _userWeiXinUserID; }
            set { _userWeiXinUserID = value; }
        }

        [DataMember]
        [DataDesc(CName = "用户姓名", ShortCode = "UserName", Desc = "用户姓名", ContextType = SysDic.All, Length = 20)]
        public virtual string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "用户OpenID", ShortCode = "UserOpenID", Desc = "用户OpenID", ContextType = SysDic.All, Length = 50)]
        public virtual string UserOpenID
        {
            get { return _userOpenID; }
            set
            {
                _userOpenID = value;
            }
        }

        //[DataMember]
        //[DataDesc(CName = "特征码", ShortCode = "FeatureCode", Desc = "特征码", ContextType = SysDic.All, Length = 20)]
        //public virtual string FeatureCode
        //{
        //    get { return _featureCode; }
        //    set
        //    {
        //        _featureCode = value;
        //    }
        //}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医嘱单状态", ShortCode = "Status", Desc = "医嘱单状态", ContextType = SysDic.All, Length = 8)]
        public virtual long Status
        {
            get { return _status; }
            set { _status = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "年龄", ShortCode = "Age", Desc = "年龄", ContextType = SysDic.All, Length = 8)]
        public virtual long Age
        {
            get { return _age; }
            set { _age = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "年龄单位ID", ShortCode = "AgeUnitID", Desc = "年龄单位ID", ContextType = SysDic.All, Length = 8)]
        public virtual long AgeUnitID
        {
            get { return _ageUnitID; }
            set { _ageUnitID = value; }
        }

        [DataMember]
        [DataDesc(CName = "年龄单位名称", ShortCode = "AgeUnitName", Desc = "年龄单位名称", ContextType = SysDic.All, Length = 20)]
        public virtual string AgeUnitName
        {
            get { return _ageUnitName; }
            set
            {
                _ageUnitName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "性别ID", ShortCode = "SexID", Desc = "性别ID", ContextType = SysDic.All, Length = 8)]
        public virtual long SexID
        {
            get { return _sexID; }
            set { _sexID = value; }
        }

        [DataMember]
        [DataDesc(CName = "性别名称", ShortCode = "SexName", Desc = "性别名称", ContextType = SysDic.All, Length = 20)]
        public virtual string SexName
        {
            get { return _sexName; }
            set
            {
                _sexName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "科室ID", ShortCode = "DeptID", Desc = "科室ID", ContextType = SysDic.All, Length = 8)]
        public virtual long DeptID
        {
            get { return _deptID; }
            set { _deptID = value; }
        }

        [DataMember]
        [DataDesc(CName = "科室名称", ShortCode = "DeptName", Desc = "科室名称", ContextType = SysDic.All, Length = 20)]
        public virtual string DeptName
        {
            get { return _deptName; }
            set
            {
                _deptName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "病历号", ShortCode = "PatNo", Desc = "病历号", ContextType = SysDic.All, Length = 20)]
        public virtual string PatNo
        {
            get { return _patNo; }
            set
            {
                _patNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = 500)]
        public virtual string Memo
        {
            get { return _memo; }
            set
            {
                _memo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "采样费用标记", ShortCode = "CollectionFlag", Desc = "采样费用标记", ContextType = SysDic.All, Length = 500)]
        public virtual bool CollectionFlag
        {
            get { return _CollectionFlag; }
            set
            {
                _CollectionFlag = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "采样费用金额", ShortCode = "CollectionPrice", Desc = "采样费用金额", ContextType = SysDic.All, Length = 500)]
        public virtual double CollectionPrice
        {
            get { return _CollectionPrice; }
            set
            {
                _CollectionPrice = value;
            }
        }

        #endregion
        [DataMember]
        public List<OSDoctorOrderItemVO> OrderItem { get; set; }
    }

    [DataContract]
    [DataDesc(CName = "医生开单项目", ClassCName = "OSDoctorOrderItemVO", ShortCode = "OSDoctorOrderItemVO", Desc = "医生开单项目")]
    public class OSDoctorOrderItemVO
    {
        
        protected long? _recommendationItemProductID;
        protected long? _itemID;
        protected string _ItemNo;

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "特推项目产品ID", ShortCode = "RecommendationItemProductID", Desc = "特推项目产品ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? RecommendationItemProductID
        {
            get { return _recommendationItemProductID; }
            set { _recommendationItemProductID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "项目ID", ShortCode = "ItemID", Desc = "项目ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ItemID
        {
            get { return _itemID; }
            set { _itemID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "项目No", ShortCode = "ItemNo", Desc = "项目No", ContextType = SysDic.All, Length = 8)]
        public virtual string ItemNo
        {
            get { return _ItemNo; }
            set { _ItemNo = value; }
        }
    }
}
