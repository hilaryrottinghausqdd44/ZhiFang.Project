using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity.ReagentSys
{
	#region CenMsg

	/// <summary>
	/// CenMsg object for NHibernate mapped table 'CenMsg'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "平台消息表", ClassCName = "CenMsg", ShortCode = "CenMsg", Desc = "平台消息表")]
	public class CenMsg : BaseEntity
	{
		#region Member Variables
		
        protected DateTime? _msgDate;
        protected string _msgValue;
        protected int _urgentFlag;
        protected string _urgentFlagName;
        protected int _status;
        protected string _statusName;
        protected string _tradeUser;
        protected DateTime? _tradeTime;
        protected string _tradeDesc;
        protected string _tradeNodeName;
        protected string _tradeNodeIPAdress;
        protected int _iOFlag;
		protected CenOrg _from;
		protected CenOrg _to;

		#endregion

		#region Constructors

		public CenMsg() { }

		public CenMsg( DateTime msgDate, string msgValue, int urgentFlag, string urgentFlagName, int status, string statusName, string tradeUser, DateTime tradeTime, string tradeDesc, string tradeNodeName, string tradeNodeIPAdress, int iOFlag, CenOrg from, CenOrg to )
		{
			this._msgDate = msgDate;
			this._msgValue = msgValue;
			this._urgentFlag = urgentFlag;
			this._urgentFlagName = urgentFlagName;
			this._status = status;
			this._statusName = statusName;
			this._tradeUser = tradeUser;
			this._tradeTime = tradeTime;
			this._tradeDesc = tradeDesc;
			this._tradeNodeName = tradeNodeName;
			this._tradeNodeIPAdress = tradeNodeIPAdress;
			this._iOFlag = iOFlag;
			this._from = from;
			this._to = to;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "消息时间", ShortCode = "MsgDate", Desc = "消息时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? MsgDate
		{
			get { return _msgDate; }
			set { _msgDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "保管帐名称", ShortCode = "MsgValue", Desc = "保管帐名称", ContextType = SysDic.All, Length = 2000)]
        public virtual string MsgValue
		{
			get { return _msgValue; }
			set
			{
				_msgValue = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "紧急标志", ShortCode = "UrgentFlag", Desc = "紧急标志", ContextType = SysDic.All, Length = 4)]
        public virtual int UrgentFlag
		{
			get { return _urgentFlag; }
			set { _urgentFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "紧急标志描述", ShortCode = "UrgentFlagName", Desc = "紧急标志描述", ContextType = SysDic.All, Length = 10)]
        public virtual string UrgentFlagName
		{
			get { return _urgentFlagName; }
			set
			{
				_urgentFlagName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "状态", ShortCode = "Status", Desc = "状态", ContextType = SysDic.All, Length = 4)]
        public virtual int Status
		{
			get { return _status; }
			set { _status = value; }
		}

        [DataMember]
        [DataDesc(CName = "状态描述", ShortCode = "StatusName", Desc = "状态描述", ContextType = SysDic.All, Length = 10)]
        public virtual string StatusName
		{
			get { return _statusName; }
			set
			{
				_statusName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "处理人", ShortCode = "TradeUser", Desc = "处理人", ContextType = SysDic.All, Length = 50)]
        public virtual string TradeUser
		{
			get { return _tradeUser; }
			set
			{
				_tradeUser = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "处理时间", ShortCode = "TradeTime", Desc = "处理时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? TradeTime
		{
			get { return _tradeTime; }
			set { _tradeTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "处理意见", ShortCode = "TradeDesc", Desc = "处理意见", ContextType = SysDic.All, Length = 1000)]
        public virtual string TradeDesc
		{
			get { return _tradeDesc; }
			set
			{
				_tradeDesc = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "处理站点名", ShortCode = "TradeNodeName", Desc = "处理站点名", ContextType = SysDic.All, Length = 100)]
        public virtual string TradeNodeName
		{
			get { return _tradeNodeName; }
			set
			{
				_tradeNodeName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "处理IP", ShortCode = "TradeNodeIPAdress", Desc = "处理IP", ContextType = SysDic.All, Length = 50)]
        public virtual string TradeNodeIPAdress
		{
			get { return _tradeNodeIPAdress; }
			set
			{
				_tradeNodeIPAdress = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "数据上传标志", ShortCode = "IOFlag", Desc = "数据上传标志", ContextType = SysDic.All, Length = 4)]
        public virtual int IOFlag
		{
			get { return _iOFlag; }
			set { _iOFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "From", Desc = "")]
		public virtual CenOrg From
		{
			get { return _from; }
			set { _from = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "To", Desc = "")]
		public virtual CenOrg To
		{
			get { return _to; }
			set { _to = value; }
		}

        
		#endregion
	}
	#endregion
}