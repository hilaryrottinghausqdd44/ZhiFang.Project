using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.RBAC
{
	#region RBACUser

	/// <summary>
	/// RBACUser object for NHibernate mapped table 'RBAC_User'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "帐户", ClassCName = "RBACUser", ShortCode = "RBACUser", Desc = "帐户")]
	public class RBACUser : BaseEntity
	{
		#region Member Variables
		
        protected string _useCode;
        protected string _account;
        protected string _pWD;
        protected bool _enMPwd;
        protected bool _pwdExprd;
        protected bool _accExprd;
        protected bool _accLock;
        protected int _auUnlock;
        protected DateTime? _accLockDt;
        protected DateTime? _loginTime;
        protected DateTime? _accBeginTime;
        protected DateTime? _accEndTime;
        protected string _standCode;
        protected string _cName;
        protected string _eName;
        protected string _sName;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected string _comment;
        protected bool? _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
		protected HREmployee _hREmployee;

		#endregion

		#region Constructors

		public RBACUser() { }

		public RBACUser( long labID, string useCode, string account, string pWD, bool enMPwd, bool pwdExprd, bool accExprd, bool accLock, int auUnlock, DateTime accLockDt, DateTime loginTime, DateTime accBeginTime, DateTime accEndTime, string standCode, string cName, string eName, string sName, string shortcode, string pinYinZiTou, string comment, bool? isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, HREmployee hREmployee )
		{
			this._labID = labID;
			this._useCode = useCode;
			this._account = account;
			this._pWD = pWD;
			this._enMPwd = enMPwd;
			this._pwdExprd = pwdExprd;
			this._accExprd = accExprd;
			this._accLock = accLock;
			this._auUnlock = auUnlock;
			this._accLockDt = accLockDt;
			this._loginTime = loginTime;
			this._accBeginTime = accBeginTime;
			this._accEndTime = accEndTime;
			this._standCode = standCode;
			this._cName = cName;
			this._eName = eName;
			this._sName = sName;
			this._shortcode = shortcode;
			this._pinYinZiTou = pinYinZiTou;
			this._comment = comment;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._hREmployee = hREmployee;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "代码", ShortCode = "UseCode", Desc = "代码", ContextType = SysDic.All, Length = 50)]
        public virtual string UseCode
		{
			get { return _useCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for UseCode", value, value.ToString());
				_useCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "用户帐号", ShortCode = "Account", Desc = "用户帐号", ContextType = SysDic.All, Length = 20)]
        public virtual string Account
		{
			get { return _account; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Account", value, value.ToString());
				_account = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "密码", ShortCode = "PWD", Desc = "密码", ContextType = SysDic.All, Length = 20)]
        public virtual string PWD
		{
			get { return _pWD; }
			set
			{
				if ( value != null && value.Length > 32)
					throw new ArgumentOutOfRangeException("Invalid value for PWD", value, value.ToString());
				_pWD = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "允许修改密码", ShortCode = "EnMPwd", Desc = "允许修改密码", ContextType = SysDic.All, Length = 1)]
        public virtual bool EnMPwd
		{
			get { return _enMPwd; }
			set { _enMPwd = value; }
		}

        [DataMember]
        [DataDesc(CName = "密码永不过期", ShortCode = "PwdExprd", Desc = "密码永不过期", ContextType = SysDic.All, Length = 1)]
        public virtual bool PwdExprd
		{
			get { return _pwdExprd; }
			set { _pwdExprd = value; }
		}

        [DataMember]
        [DataDesc(CName = "账号永不过期", ShortCode = "AccExprd", Desc = "账号永不过期", ContextType = SysDic.All, Length = 1)]
        public virtual bool AccExprd
		{
			get { return _accExprd; }
			set { _accExprd = value; }
		}

        [DataMember]
        [DataDesc(CName = "帐号被锁定", ShortCode = "AccLock", Desc = "帐号被锁定", ContextType = SysDic.All, Length = 1)]
        public virtual bool AccLock
		{
			get { return _accLock; }
			set { _accLock = value; }
		}

        [DataMember]
        [DataDesc(CName = "自动解锁", ShortCode = "AuUnlock", Desc = "自动解锁", ContextType = SysDic.All, Length = 4)]
        public virtual int AuUnlock
		{
			get { return _auUnlock; }
			set { _auUnlock = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "锁定日期", ShortCode = "AccLockDt", Desc = "锁定日期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? AccLockDt
		{
			get { return _accLockDt; }
			set { _accLockDt = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "上次登录时间", ShortCode = "LoginTime", Desc = "上次登录时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? LoginTime
		{
			get { return _loginTime; }
			set { _loginTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "帐号启用时间", ShortCode = "AccBeginTime", Desc = "帐号启用时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? AccBeginTime
		{
			get { return _accBeginTime; }
			set { _accBeginTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "帐号到期时间", ShortCode = "AccEndTime", Desc = "帐号到期时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? AccEndTime
		{
			get { return _accEndTime; }
			set { _accEndTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "标准代码", ShortCode = "StandCode", Desc = "标准代码", ContextType = SysDic.All, Length = 50)]
        public virtual string StandCode
		{
			get { return _standCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for StandCode", value, value.ToString());
				_standCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "CName", Desc = "名称", ContextType = SysDic.All, Length = 50)]
        public virtual string CName
		{
			get { return _cName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
				_cName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "英文名称", ShortCode = "EName", Desc = "英文名称", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 50)]
        public virtual string SName
		{
			get { return _sName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SName", value, value.ToString());
				_sName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "快捷码", ShortCode = "Shortcode", Desc = "快捷码", ContextType = SysDic.All, Length = 20)]
        public virtual string Shortcode
		{
			get { return _shortcode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Shortcode", value, value.ToString());
				_shortcode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "汉字拼音字头", ShortCode = "PinYinZiTou", Desc = "汉字拼音字头", ContextType = SysDic.All, Length = 50)]
        public virtual string PinYinZiTou
		{
			get { return _pinYinZiTou; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for PinYinZiTou", value, value.ToString());
				_pinYinZiTou = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "描述", ShortCode = "Comment", Desc = "描述", ContextType = SysDic.All, Length = 16)]
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
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool? IsUse
		{
			get {
                if (_isUse.HasValue)
                    return _isUse;
                else
                    return true;
            }
			set { _isUse = value; }
		}

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
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
        [DataDesc(CName = "员工", ShortCode = "HREmployee", Desc = "员工")]
		public virtual HREmployee HREmployee
		{
			get { return _hREmployee; }
			set { _hREmployee = value; }
		}

        
		#endregion
	}
	#endregion
}