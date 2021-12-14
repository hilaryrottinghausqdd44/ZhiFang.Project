using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LIIP
{
	#region IntergrateSystemSet

	/// <summary>
	/// IntergrateSystemSet object for NHibernate mapped table 'IntergrateSystemSet'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "分布式系统设置表", ClassCName = "IntergrateSystemSet", ShortCode = "IntergrateSystemSet", Desc = "分布式系统设置表")]
	public class IntergrateSystemSet : BaseEntityService
	{
		#region Member Variables
		
        protected string _systemName;
        protected string _systemCode;
        protected string _systemHost;
        protected string _systemEntryAddress;
        protected string _pinYinZiTou;
        protected string _memo;
        protected int _dispOrder;
        protected bool _isUse;
		

		#endregion

		#region Constructors

		public IntergrateSystemSet() { }

		public IntergrateSystemSet( string systemName, string systemCode, string systemHost, string systemEntryAddress, string pinYinZiTou, string memo, int dispOrder, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp )
		{
			this._systemName = systemName;
			this._systemCode = systemCode;
			this._systemHost = systemHost;
			this._systemEntryAddress = systemEntryAddress;
			this._pinYinZiTou = pinYinZiTou;
			this._memo = memo;
			this._dispOrder = dispOrder;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "系统名称", ShortCode = "SystemName", Desc = "系统名称", ContextType = SysDic.All, Length = 100)]
        public virtual string SystemName
		{
			get { return _systemName; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for SystemName", value, value.ToString());
				_systemName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "系统编码", ShortCode = "SystemCode", Desc = "系统编码", ContextType = SysDic.All, Length = 50)]
        public virtual string SystemCode
		{
			get { return _systemCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SystemCode", value, value.ToString());
				_systemCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "系统主地址：192.168.0.102/ZhiFang.OA   后面不带斜杠。", ShortCode = "SystemHost", Desc = "系统主地址：192.168.0.102/ZhiFang.OA   后面不带斜杠。", ContextType = SysDic.All, Length = 500)]
        public virtual string SystemHost
		{
			get { return _systemHost; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for SystemHost", value, value.ToString());
				_systemHost = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "系统入口地址", ShortCode = "SystemEntryAddress", Desc = "系统入口地址", ContextType = SysDic.All, Length = 500)]
        public virtual string SystemEntryAddress
		{
			get { return _systemEntryAddress; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for SystemEntryAddress", value, value.ToString());
				_systemEntryAddress = value;
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
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = -1)]
        public virtual string Memo
		{
			get { return _memo; }
			set
			{
				_memo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
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

        public virtual DateTime DataUpdateTime { get; set; }


        #endregion
    }
	#endregion
}