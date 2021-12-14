using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LIIP
{
	#region SAccountRegister

	/// <summary>
	/// SAccountRegister object for NHibernate mapped table 'S_AccountRegister'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "SAccountRegister", ShortCode = "SAccountRegister", Desc = "")]
	public class SAccountRegister : BaseEntityService
	{
		#region Member Variables
		
        protected long? _empID;
        protected long? _weiXinAccountID;
        protected string _name;
        protected string _eName;
        protected string _idNumber;
        protected DateTime? _birthday;
        protected long _sexID;
        protected string _sexName;
        protected long _hospitalID;
        protected string _hospitalCode;
        protected long _iconsID;
        protected string _provinceName;
        protected string _cityName;
        protected long _provinceID;
        protected long _countyID;
        protected string _countyName;
        protected long _cityID;
        protected string _mobileTel;
        protected string _address;
        protected string _eMAIL;
        protected string _mAILNO;
        protected string _faxNo;
        protected string _comment;
        protected bool _isUse;
        protected DateTime? _dataUpdateTime;
        protected long _approvalID;
        protected string _approvalName;
        protected DateTime? _approvalDateTime;
        protected string _approvalInfo;
        protected long _applySourceTypeID;
        protected string _applySourceTypeName;
        protected string _idInfoNo;
        protected long _idInfoTypeId;
        protected long _statusId;
        protected string _statusName;
        protected string _account;
        protected string _passWord;
		private string _ApplyInfo;
		private List<string> _RolesIDList;
		private List<string> _HospitalIdIDList;
		private string _HospitalName;


		#endregion

		#region Constructors

		public SAccountRegister() { }

		public SAccountRegister( long labID, long empID, long weiXinAccountID, string name, string eName, string idNumber, DateTime birthday, long sexID, string sexName, long hospitalID, string hospitalCode, long iconsID, string provinceName, string cityName, long provinceID, long countyID, string countyName, long cityID, string mobileTel, string address, string eMAIL, string mAILNO, string faxNo, string comment, bool isUse, byte[] dataTimeStamp, DateTime dataAddTime, DateTime dataUpdateTime, long approvalID, string approvalName, DateTime approvalDateTime, string approvalInfo, long applySourceTypeID, string applySourceTypeName, string idInfoNo, long idInfoTypeId, long statusId, string statusName, string account, string passWord )
		{
			this._labID = labID;
			this._empID = empID;
			this._weiXinAccountID = weiXinAccountID;
			this._name = name;
			this._eName = eName;
			this._idNumber = idNumber;
			this._birthday = birthday;
			this._sexID = sexID;
			this._sexName = sexName;
			this._hospitalID = hospitalID;
			this._hospitalCode = hospitalCode;
			this._iconsID = iconsID;
			this._provinceName = provinceName;
			this._cityName = cityName;
			this._provinceID = provinceID;
			this._countyID = countyID;
			this._countyName = countyName;
			this._cityID = cityID;
			this._mobileTel = mobileTel;
			this._address = address;
			this._eMAIL = eMAIL;
			this._mAILNO = mAILNO;
			this._faxNo = faxNo;
			this._comment = comment;
			this._isUse = isUse;
			this._dataTimeStamp = dataTimeStamp;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._approvalID = approvalID;
			this._approvalName = approvalName;
			this._approvalDateTime = approvalDateTime;
			this._approvalInfo = approvalInfo;
			this._applySourceTypeID = applySourceTypeID;
			this._applySourceTypeName = applySourceTypeName;
			this._idInfoNo = idInfoNo;
			this._idInfoTypeId = idInfoTypeId;
			this._statusId = statusId;
			this._statusName = statusName;
			this._account = account;
			this._passWord = passWord;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "EmpID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? EmpID
		{
			get { return _empID; }
			set { _empID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "WeiXinAccountID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? WeiXinAccountID
		{
			get { return _weiXinAccountID; }
			set { _weiXinAccountID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Name", Desc = "", ContextType = SysDic.All, Length = 30)]
        public virtual string Name
		{
			get { return _name; }
			set
			{
				_name = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string EName
		{
			get { return _eName; }
			set
			{
				_eName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IdNumber", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string IdNumber
		{
			get { return _idNumber; }
			set
			{
				_idNumber = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Birthday", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? Birthday
		{
			get { return _birthday; }
			set { _birthday = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "SexID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long SexID
		{
			get { return _sexID; }
			set { _sexID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SexName", Desc = "", ContextType = SysDic.All, Length = 20)]
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
        [DataDesc(CName = "", ShortCode = "HospitalID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long HospitalID
		{
			get { return _hospitalID; }
			set { _hospitalID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HospitalCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string HospitalCode
		{
			get { return _hospitalCode; }
			set
			{
				_hospitalCode = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "IconsID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long IconsID
		{
			get { return _iconsID; }
			set { _iconsID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ProvinceName", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string ProvinceName
		{
			get { return _provinceName; }
			set
			{
				_provinceName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CityName", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string CityName
		{
			get { return _cityName; }
			set
			{
				_cityName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ProvinceID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long ProvinceID
		{
			get { return _provinceID; }
			set { _provinceID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CountyID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long CountyID
		{
			get { return _countyID; }
			set { _countyID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CountyName", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string CountyName
		{
			get { return _countyName; }
			set
			{
				_countyName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CityID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long CityID
		{
			get { return _cityID; }
			set { _cityID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "MobileTel", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string MobileTel
		{
			get { return _mobileTel; }
			set
			{
				_mobileTel = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Address", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string Address
		{
			get { return _address; }
			set
			{
				_address = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EMAIL", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string EMAIL
		{
			get { return _eMAIL; }
			set
			{
				_eMAIL = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "MAILNO", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string MAILNO
		{
			get { return _mAILNO; }
			set
			{
				_mAILNO = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FaxNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string FaxNo
		{
			get { return _faxNo; }
			set
			{				
				_faxNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Comment", Desc = "", ContextType = SysDic.All, Length = 1000)]
        public virtual string Comment
		{
			get { return _comment; }
			set
			{
				_comment = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsUse", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ApprovalID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long ApprovalID
		{
			get { return _approvalID; }
			set { _approvalID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ApprovalName", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string ApprovalName
		{
			get { return _approvalName; }
			set
			{
				_approvalName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ApprovalDateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ApprovalDateTime
		{
			get { return _approvalDateTime; }
			set { _approvalDateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ApprovalInfo", Desc = "", ContextType = SysDic.All, Length = 300)]
        public virtual string ApprovalInfo
		{
			get { return _approvalInfo; }
			set
			{
				_approvalInfo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ApplySourceTypeID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long ApplySourceTypeID
		{
			get { return _applySourceTypeID; }
			set { _applySourceTypeID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ApplySourceTypeName", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string ApplySourceTypeName
		{
			get { return _applySourceTypeName; }
			set
			{
				_applySourceTypeName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IdInfoNo", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string IdInfoNo
		{
			get { return _idInfoNo; }
			set
			{
				_idInfoNo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "IdInfoTypeId", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long IdInfoTypeId
		{
			get { return _idInfoTypeId; }
			set { _idInfoTypeId = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "StatusId", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long StatusId
		{
			get { return _statusId; }
			set { _statusId = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "StatusName", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string StatusName
		{
			get { return _statusName; }
			set
			{
				_statusName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Account", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Account
		{
			get { return _account; }
			set
			{
				_account = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PassWord", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string PassWord
		{
			get { return _passWord; }
			set
			{
				_passWord = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "申请备注", ShortCode = "ApplyInfo", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string ApplyInfo
		{
			get { return _ApplyInfo; }
			set
			{
				_ApplyInfo = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "角色ID列表", ShortCode = "RolesIDList", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual List<string> RolesIDList
		{
			get { return _RolesIDList; }
			set
			{
				_RolesIDList = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "所属医院ID列表", ShortCode = "HospitalIdIDList", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual List<string> HospitalIdIDList
		{
			get { return _HospitalIdIDList; }
			set
			{
				_HospitalIdIDList = value;
			}
		}
		[DataMember]
		public virtual string HospitalName { get => _HospitalName; set => _HospitalName = value; }
		#endregion
	}
	#endregion
}