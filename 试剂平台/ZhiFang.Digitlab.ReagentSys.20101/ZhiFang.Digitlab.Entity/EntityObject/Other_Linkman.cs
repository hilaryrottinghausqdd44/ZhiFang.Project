using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region OtherLinkman

	/// <summary>
	/// OtherLinkman object for NHibernate mapped table 'Other_Linkman'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "联系人管理", ClassCName = "OtherLinkman", ShortCode = "OtherLinkman", Desc = "联系人管理")]
    public class OtherLinkman : BaseEntity
	{
		#region Member Variables
		
        
        protected long? _clientID;
        protected long? _sexID;
        protected long? _nationalityID;
        protected long? _animalID;
        protected long? _constellationID;
        protected long? _educationLevelID;
        protected long? _maritalStatusID;
        protected long? _healthStatusID;
        protected long? _creatorID;
        protected long? _positionID;
        protected string _position;
        protected string _name;
        protected DateTime? _birthday;
        protected string _dept;
        protected string _officeTel;
        protected string _mobileTel;
        protected string _homeTel;
        protected string _tel;
        protected string _qQ;
        protected string _email;
        protected string _address;
        protected string _idNumber;
        protected string _hobby;
        protected string _comment;
        protected string _focusConcern;
        protected string _interests;
        protected bool _isUse;
        protected string _creator;
        

		#endregion

		#region Constructors

		public OtherLinkman() { }

		public OtherLinkman( long labID, long clientID, long sexID, long nationalityID, long animalID, long constellationID, long educationLevelID, long maritalStatusID, long healthStatusID, long creatorID, long positionID, string position, string name, DateTime birthday, string dept, string officeTel, string mobileTel, string homeTel, string tel, string qQ, string email, string address, string idNumber, string hobby, string comment, string focusConcern, string interests, bool isUse, string creator, DateTime dataAddTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._clientID = clientID;
			this._sexID = sexID;
			this._nationalityID = nationalityID;
			this._animalID = animalID;
			this._constellationID = constellationID;
			this._educationLevelID = educationLevelID;
			this._maritalStatusID = maritalStatusID;
			this._healthStatusID = healthStatusID;
			this._creatorID = creatorID;
			this._positionID = positionID;
			this._position = position;
			this._name = name;
			this._birthday = birthday;
			this._dept = dept;
			this._officeTel = officeTel;
			this._mobileTel = mobileTel;
			this._homeTel = homeTel;
			this._tel = tel;
			this._qQ = qQ;
			this._email = email;
			this._address = address;
			this._idNumber = idNumber;
			this._hobby = hobby;
			this._comment = comment;
			this._focusConcern = focusConcern;
			this._interests = interests;
			this._isUse = isUse;
			this._creator = creator;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties



        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "客户ID", ShortCode = "ClientID", Desc = "客户ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ClientID
		{
			get { return _clientID; }
			set { _clientID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "性别ID", ShortCode = "SexID", Desc = "性别ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? SexID
		{
			get { return _sexID; }
			set { _sexID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "民族ID", ShortCode = "NationalityID", Desc = "民族ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? NationalityID
		{
			get { return _nationalityID; }
			set { _nationalityID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "属相ID", ShortCode = "AnimalID", Desc = "属相ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? AnimalID
		{
			get { return _animalID; }
			set { _animalID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "星座ID", ShortCode = "ConstellationID", Desc = "星座ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ConstellationID
		{
			get { return _constellationID; }
			set { _constellationID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "学历ID", ShortCode = "EducationLevelID", Desc = "学历ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? EducationLevelID
		{
			get { return _educationLevelID; }
			set { _educationLevelID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "婚姻状况ID", ShortCode = "MaritalStatusID", Desc = "婚姻状况ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? MaritalStatusID
		{
			get { return _maritalStatusID; }
			set { _maritalStatusID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "健康状况ID", ShortCode = "HealthStatusID", Desc = "健康状况ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? HealthStatusID
		{
			get { return _healthStatusID; }
			set { _healthStatusID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "员工ID主键", ShortCode = "CreatorID", Desc = "员工ID主键", ContextType = SysDic.All, Length = 8)]
        public virtual long? CreatorID
		{
			get { return _creatorID; }
			set { _creatorID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "职务ID", ShortCode = "PositionID", Desc = "职务ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? PositionID
		{
			get { return _positionID; }
			set { _positionID = value; }
		}

        [DataMember]
        [DataDesc(CName = "职务", ShortCode = "Position", Desc = "职务", ContextType = SysDic.All, Length = 200)]
        public virtual string Position
		{
			get { return _position; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for Position", value, value.ToString());
				_position = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "姓名", ShortCode = "Name", Desc = "姓名", ContextType = SysDic.All, Length = 40)]
        public virtual string Name
		{
			get { return _name; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for Name", value, value.ToString());
				_name = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "出生日期", ShortCode = "Birthday", Desc = "出生日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? Birthday
		{
			get { return _birthday; }
			set { _birthday = value; }
		}

        [DataMember]
        [DataDesc(CName = "部门", ShortCode = "Dept", Desc = "部门", ContextType = SysDic.All, Length = 100)]
        public virtual string Dept
		{
			get { return _dept; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for Dept", value, value.ToString());
				_dept = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "单位电话", ShortCode = "OfficeTel", Desc = "单位电话", ContextType = SysDic.All, Length = 40)]
        public virtual string OfficeTel
		{
			get { return _officeTel; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for OfficeTel", value, value.ToString());
				_officeTel = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "移动电话", ShortCode = "MobileTel", Desc = "移动电话", ContextType = SysDic.All, Length = 40)]
        public virtual string MobileTel
		{
			get { return _mobileTel; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for MobileTel", value, value.ToString());
				_mobileTel = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "家庭电话", ShortCode = "HomeTel", Desc = "家庭电话", ContextType = SysDic.All, Length = 40)]
        public virtual string HomeTel
		{
			get { return _homeTel; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for HomeTel", value, value.ToString());
				_homeTel = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "其他电话", ShortCode = "Tel", Desc = "其他电话", ContextType = SysDic.All, Length = 40)]
        public virtual string Tel
		{
			get { return _tel; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for Tel", value, value.ToString());
				_tel = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "QQ号", ShortCode = "QQ", Desc = "QQ号", ContextType = SysDic.All, Length = 40)]
        public virtual string QQ
		{
			get { return _qQ; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for QQ", value, value.ToString());
				_qQ = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "邮件地址", ShortCode = "Email", Desc = "邮件地址", ContextType = SysDic.All, Length = 40)]
        public virtual string Email
		{
			get { return _email; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for Email", value, value.ToString());
				_email = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "家庭地址", ShortCode = "Address", Desc = "家庭地址", ContextType = SysDic.All, Length = 200)]
        public virtual string Address
		{
			get { return _address; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for Address", value, value.ToString());
				_address = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "身份证号", ShortCode = "IdNumber", Desc = "身份证号", ContextType = SysDic.All, Length = 40)]
        public virtual string IdNumber
		{
			get { return _idNumber; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for IdNumber", value, value.ToString());
				_idNumber = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "爱好", ShortCode = "Hobby", Desc = "爱好", ContextType = SysDic.All, Length = 16)]
        public virtual string Hobby
		{
			get { return _hobby; }
			set
			{
				if ( value != null && value.Length > 16)
					throw new ArgumentOutOfRangeException("Invalid value for Hobby", value, value.ToString());
				_hobby = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Comment", Desc = "备注", ContextType = SysDic.All, Length = 16)]
        public virtual string Comment
		{
			get { return _comment; }
			set
			{
				if ( value != null && value.Length > 8000)
					throw new ArgumentOutOfRangeException("Invalid value for Comment", value, value.ToString());
				_comment = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "关心重点", ShortCode = "FocusConcern", Desc = "关心重点", ContextType = SysDic.All, Length = 16)]
        public virtual string FocusConcern
		{
			get { return _focusConcern; }
			set
			{
				if ( value != null && value.Length > 16)
					throw new ArgumentOutOfRangeException("Invalid value for FocusConcern", value, value.ToString());
				_focusConcern = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "利益诉求", ShortCode = "Interests", Desc = "利益诉求", ContextType = SysDic.All, Length = 16)]
        public virtual string Interests
		{
			get { return _interests; }
			set
			{
				if ( value != null && value.Length > 16)
					throw new ArgumentOutOfRangeException("Invalid value for Interests", value, value.ToString());
				_interests = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

        [DataMember]
        [DataDesc(CName = "创建者", ShortCode = "Creator", Desc = "创建者", ContextType = SysDic.All, Length = 20)]
        public virtual string Creator
		{
			get { return _creator; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Creator", value, value.ToString());
				_creator = value;
			}
		}

		#endregion
	}
	#endregion
}