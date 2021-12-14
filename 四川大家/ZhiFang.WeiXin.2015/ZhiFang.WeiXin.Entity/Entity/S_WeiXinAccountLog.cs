using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
	#region SWeiXinAccountLog

	/// <summary>
	/// SWeiXinAccountLog object for NHibernate mapped table 'S_WeiXinAccountLog'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "SWeiXinAccountLog", ShortCode = "SWeiXinAccountLog", Desc = "")]
	public class SWeiXinAccountLog : BaseEntity
	{
		#region Member Variables
		
        protected long _weiXinUserID;
        protected string _weiXinAccountOpenID;
        protected string _userName;
        protected string _operateName;
        protected string _operateType;
        protected string _iP;
        protected int _infoLevel;
        protected string _comment;
		

		#endregion

		#region Constructors

		public SWeiXinAccountLog() { }

		public SWeiXinAccountLog( long weiXinUserID, string weiXinAccountOpenID, string userName, string operateName, string operateType, string iP, int infoLevel, string comment, DateTime dataAddTime, byte[] dataTimeStamp )
		{
			this._weiXinUserID = weiXinUserID;
			this._weiXinAccountOpenID = weiXinAccountOpenID;
			this._userName = userName;
			this._operateName = operateName;
			this._operateType = operateType;
			this._iP = iP;
			this._infoLevel = infoLevel;
			this._comment = comment;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "WeiXinUserID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long WeiXinUserID
		{
			get { return _weiXinUserID; }
			set { _weiXinUserID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "WeiXinAccountOpenID", Desc = "", ContextType = SysDic.All, Length = 30)]
        public virtual string WeiXinAccountOpenID
		{
			get { return _weiXinAccountOpenID; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for WeiXinAccountOpenID", value, value.ToString());
				_weiXinAccountOpenID = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UserName", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string UserName
		{
			get { return _userName; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for UserName", value, value.ToString());
				_userName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OperateName", Desc = "", ContextType = SysDic.All, Length = 512)]
        public virtual string OperateName
		{
			get { return _operateName; }
			set
			{
				if ( value != null && value.Length > 512)
					throw new ArgumentOutOfRangeException("Invalid value for OperateName", value, value.ToString());
				_operateName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OperateType", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string OperateType
		{
			get { return _operateType; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for OperateType", value, value.ToString());
				_operateType = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IP", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string IP
		{
			get { return _iP; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for IP", value, value.ToString());
				_iP = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "InfoLevel", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int InfoLevel
		{
			get { return _infoLevel; }
			set { _infoLevel = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Comment", Desc = "", ContextType = SysDic.All, Length = 16)]
        public virtual string Comment
		{
			get { return _comment; }
			set
			{
				if ( value != null && value.Length > 16)
					throw new ArgumentOutOfRangeException("Invalid value for Comment", value, value.ToString());
				_comment = value;
			}
		}

		
		#endregion
	}
	#endregion
}