using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region CenOrgCondition

	/// <summary>
	/// CenOrgCondition object for NHibernate mapped table 'CenOrgCondition'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "CenOrgCondition", ShortCode = "CenOrgCondition", Desc = "")]
	public class CenOrgCondition : BaseEntity
	{
		#region Member Variables
		
        protected long _orgID1;
        protected long _orgID2;
        protected string _memo;
        protected string _confirmToken;
        protected string _confirmUserNo;
        protected string _confirmUserName;
        protected string _confirmUserPassWord;
        protected string _confirmUrl;
        protected string _orgAlias1;
        protected string _orgAlias2;
        protected string _customerCode;
        protected string _customerAccount;
        protected DateTime? _dataUpdateTime;
		

		#endregion

		#region Constructors

		public CenOrgCondition() { }

		public CenOrgCondition( long orgID1, long orgID2, string memo, string confirmToken, string confirmUserNo, string confirmUserName, string confirmUserPassWord, string confirmUrl, string orgAlias1, string orgAlias2, string customerCode, string customerAccount, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp )
		{
			this._orgID1 = orgID1;
			this._orgID2 = orgID2;
			this._memo = memo;
			this._confirmToken = confirmToken;
			this._confirmUserNo = confirmUserNo;
			this._confirmUserName = confirmUserName;
			this._confirmUserPassWord = confirmUserPassWord;
			this._confirmUrl = confirmUrl;
			this._orgAlias1 = orgAlias1;
			this._orgAlias2 = orgAlias2;
			this._customerCode = customerCode;
			this._customerAccount = customerAccount;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OrgID1", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long OrgID1
		{
			get { return _orgID1; }
			set { _orgID1 = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OrgID2", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long OrgID2
		{
			get { return _orgID2; }
			set { _orgID2 = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Memo", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string Memo
		{
			get { return _memo; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for Memo", value, value.ToString());
				_memo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ConfirmToken", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ConfirmToken
		{
			get { return _confirmToken; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for ConfirmToken", value, value.ToString());
				_confirmToken = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ConfirmUserNo", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ConfirmUserNo
		{
			get { return _confirmUserNo; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for ConfirmUserNo", value, value.ToString());
				_confirmUserNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ConfirmUserName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ConfirmUserName
		{
			get { return _confirmUserName; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for ConfirmUserName", value, value.ToString());
				_confirmUserName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ConfirmUserPassWord", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ConfirmUserPassWord
		{
			get { return _confirmUserPassWord; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for ConfirmUserPassWord", value, value.ToString());
				_confirmUserPassWord = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ConfirmUrl", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ConfirmUrl
		{
			get { return _confirmUrl; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for ConfirmUrl", value, value.ToString());
				_confirmUrl = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OrgAlias1", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string OrgAlias1
		{
			get { return _orgAlias1; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for OrgAlias1", value, value.ToString());
				_orgAlias1 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OrgAlias2", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string OrgAlias2
		{
			get { return _orgAlias2; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for OrgAlias2", value, value.ToString());
				_orgAlias2 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "实验室在供应商系统中的编码", ShortCode = "CustomerCode", Desc = "实验室在供应商系统中的编码", ContextType = SysDic.All, Length = 100)]
        public virtual string CustomerCode
		{
			get { return _customerCode; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for CustomerCode", value, value.ToString());
				_customerCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "实验室在供应商系统中的账户", ShortCode = "CustomerAccount", Desc = "实验室在供应商系统中的账户", ContextType = SysDic.All, Length = 100)]
        public virtual string CustomerAccount
		{
			get { return _customerAccount; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for CustomerAccount", value, value.ToString());
				_customerAccount = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

		
		#endregion
	}
	#endregion
}