using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
	#region OSDoctorOrderForm

	/// <summary>
	/// OSDoctorOrderForm object for NHibernate mapped table 'OS_DoctorOrderForm'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "医生医嘱单", ClassCName = "OSDoctorOrderForm", ShortCode = "OSDoctorOrderForm", Desc = "医生医嘱单")]
	public class OSDoctorOrderForm : BaseEntity
	{
		#region Member Variables
		
        protected long _areaID;
        protected long _hospitalID;
        protected string _hospitalName;
        protected long? _doctorAccountID;
        protected long? _doctorWeiXinUserID;
        protected string _doctorName;
        protected string _doctorOpenID;
        protected long _userAccountID;
        protected long _userWeiXinUserID;
        protected string _userName;
        protected string _userOpenID;
        protected string _featureCode;
        protected long _status;
        protected long _age;
        protected long _ageUnitID;
        protected string _ageUnitName;
        protected long _sexID;
        protected string _sexName;
        protected long? _deptID;
        protected string _deptName;
        protected string _patNo;
        protected string _memo;
        protected int _dispOrder;
        protected bool _isUse;
        protected DateTime? _dataUpdateTime;
        protected long? _TypeID;
        protected string _TypeName;
        protected bool _CollectionFlag;
        protected double _CollectionPrice;
        protected string _DoctMobileCode;
        protected string _UserMobileCode;

        #endregion

        #region Constructors

        public OSDoctorOrderForm() { }

		public OSDoctorOrderForm( long areaID, long hospitalID, string hospitalName, long doctorAccountID, long doctorWeiXinUserID, string doctorName, string doctorOpenID, long userAccountID, long userWeiXinUserID, string userName, string userOpenID, string featureCode, long status, long age, long ageUnitID, string ageUnitName, long sexID, string sexName, long deptID, string deptName, string patNo, string memo, int dispOrder, bool isUse, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp )
		{
			this._areaID = areaID;
			this._hospitalID = hospitalID;
			this._hospitalName = hospitalName;
			this._doctorAccountID = doctorAccountID;
			this._doctorWeiXinUserID = doctorWeiXinUserID;
			this._doctorName = doctorName;
			this._doctorOpenID = doctorOpenID;
			this._userAccountID = userAccountID;
			this._userWeiXinUserID = userWeiXinUserID;
			this._userName = userName;
			this._userOpenID = userOpenID;
			this._featureCode = featureCode;
			this._status = status;
			this._age = age;
			this._ageUnitID = ageUnitID;
			this._ageUnitName = ageUnitName;
			this._sexID = sexID;
			this._sexName = sexName;
			this._deptID = deptID;
			this._deptName = deptName;
			this._patNo = patNo;
			this._memo = memo;
			this._dispOrder = dispOrder;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
		}

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
        public virtual long? DoctorAccountID
		{
			get { return _doctorAccountID; }
			set { _doctorAccountID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医生微信ID", ShortCode = "DoctorWeiXinUserID", Desc = "医生微信ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? DoctorWeiXinUserID
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

        [DataMember]
        [DataDesc(CName = "特征码", ShortCode = "FeatureCode", Desc = "特征码", ContextType = SysDic.All, Length = 20)]
        public virtual string FeatureCode
		{
			get { return _featureCode; }
			set
			{
				_featureCode = value;
			}
		}

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
        public virtual long? DeptID
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
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "类型ID", ShortCode = "TypeID", Desc = "类型ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? TypeID
        {
            get { return _TypeID; }
            set { _TypeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "类型名称", ShortCode = "TypeName", Desc = "类型名称", ContextType = SysDic.All, Length = 8)]
        public virtual string TypeName
        {
            get { return _TypeName; }
            set { _TypeName = value; }
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

        [DataMember]
        [DataDesc(CName = "医生电话", ShortCode = "DoctMobileCode", Desc = "医生电话", ContextType = SysDic.All, Length = 500)]
        public virtual string DoctMobileCode
        {
            get { return _DoctMobileCode; }
            set
            {
                _DoctMobileCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "用户电话", ShortCode = "UserMobileCode", Desc = "采样费用金额", ContextType = SysDic.All, Length = 500)]
        public virtual string UserMobileCode
        {
            get { return _UserMobileCode; }
            set
            {
                _UserMobileCode = value;
            }
        }
        #endregion
    }
	#endregion
}