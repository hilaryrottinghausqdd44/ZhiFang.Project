using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
	#region PClientLinker

	/// <summary>
	/// PClientLinker object for NHibernate mapped table 'P_ClientLinker'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "联系人", ClassCName = "PClientLinker", ShortCode = "PClientLinker", Desc = "联系人")]
	public class PClientLinker : BaseEntity
	{
		#region Member Variables
		
        protected string _name;
        protected string _eName;
        protected string _sex;
        protected string _phoneNum;
        protected DateTime? _brithday;
        protected string _birthplace;
        protected string _address;
        protected string _phoneNum2;
        protected string _telPhone;
        protected string _email;
        protected string _qQ;
        protected string _weiXin;
        protected string _postion;
        protected int _dispOrder;
        protected string _comment;
        protected bool _isUse;
		protected PClient _pClient;

		#endregion

		#region Constructors

		public PClientLinker() { }

		public PClientLinker( string name, string eName, string sex, string phoneNum, DateTime brithday, string birthplace, string address, string phoneNum2, string telPhone, string email, string qQ, string weiXin, string postion, int dispOrder, string comment, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp, PClient pClient )
		{
			this._name = name;
			this._eName = eName;
			this._sex = sex;
			this._phoneNum = phoneNum;
			this._brithday = brithday;
			this._birthplace = birthplace;
			this._address = address;
			this._phoneNum2 = phoneNum2;
			this._telPhone = telPhone;
			this._email = email;
			this._qQ = qQ;
			this._weiXin = weiXin;
			this._postion = postion;
			this._dispOrder = dispOrder;
			this._comment = comment;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._pClient = pClient;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "Name", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Name
		{
			get { return _name; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Name", value, value.ToString());
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
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for EName", value, value.ToString());
				_eName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Sex", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string Sex
		{
			get { return _sex; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for Sex", value, value.ToString());
				_sex = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PhoneNum", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string PhoneNum
		{
			get { return _phoneNum; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for PhoneNum", value, value.ToString());
				_phoneNum = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Brithday", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? Brithday
		{
			get { return _brithday; }
			set { _brithday = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Birthplace", Desc = "", ContextType = SysDic.All, Length = 80)]
        public virtual string Birthplace
		{
			get { return _birthplace; }
			set
			{
				if ( value != null && value.Length > 80)
					throw new ArgumentOutOfRangeException("Invalid value for Birthplace", value, value.ToString());
				_birthplace = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Address", Desc = "", ContextType = SysDic.All, Length = 80)]
        public virtual string Address
		{
			get { return _address; }
			set
			{
				if ( value != null && value.Length > 80)
					throw new ArgumentOutOfRangeException("Invalid value for Address", value, value.ToString());
				_address = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PhoneNum2", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string PhoneNum2
		{
			get { return _phoneNum2; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for PhoneNum2", value, value.ToString());
				_phoneNum2 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TelPhone", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string TelPhone
		{
			get { return _telPhone; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for TelPhone", value, value.ToString());
				_telPhone = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Email", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Email
		{
			get { return _email; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Email", value, value.ToString());
				_email = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "QQ", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string QQ
		{
			get { return _qQ; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for QQ", value, value.ToString());
				_qQ = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "WeiXin", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string WeiXin
		{
			get { return _weiXin; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for WeiXin", value, value.ToString());
				_weiXin = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Postion", Desc = "", ContextType = SysDic.All, Length = 30)]
        public virtual string Postion
		{
			get { return _postion; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for Postion", value, value.ToString());
				_postion = value;
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
        [DataDesc(CName = "备注", ShortCode = "Comment", Desc = "备注", ContextType = SysDic.All, Length = -1)]
        public virtual string Comment
		{
			get { return _comment; }
			set
			{
				_comment = value;
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
        [DataDesc(CName = "客户", ShortCode = "PClient", Desc = "客户")]
		public virtual PClient PClient
		{
			get { return _pClient; }
			set { _pClient = value; }
		}

        
		#endregion
	}
	#endregion
}