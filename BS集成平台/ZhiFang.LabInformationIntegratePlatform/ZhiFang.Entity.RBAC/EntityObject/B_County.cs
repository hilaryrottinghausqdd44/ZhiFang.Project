using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.RBAC
{
	#region BCounty

	/// <summary>
	/// BCounty object for NHibernate mapped table 'B_County'.
	/// </summary>
	[DataContract]
	[DataDesc(CName = "", ClassCName = "BCounty", ShortCode = "BCounty", Desc = "")]
	public class BCounty : BaseEntityService
	{
		#region Member Variables

		protected string _name;
		protected string _sName;
		protected string _shortcode;
		protected string _pinYinZiTou;
		protected string _comment;
		protected bool _isUse;
		protected BCity _bCity;

		#endregion

		#region Constructors

		public BCounty() { }

		public BCounty(long labID, string name, string sName, string shortcode, string pinYinZiTou, string comment, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp, BCity bCity)
		{
			this._labID = labID;
			this._name = name;
			this._sName = sName;
			this._shortcode = shortcode;
			this._pinYinZiTou = pinYinZiTou;
			this._comment = comment;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bCity = bCity;
		}

		#endregion

		#region Public Properties


		[DataMember]
		[DataDesc(CName = "", ShortCode = "Name", Desc = "", ContextType = SysDic.All, Length = 40)]
		public virtual string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "SName", Desc = "", ContextType = SysDic.All, Length = 40)]
		public virtual string SName
		{
			get { return _sName; }
			set { _sName = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "Shortcode", Desc = "", ContextType = SysDic.All, Length = 40)]
		public virtual string Shortcode
		{
			get { return _shortcode; }
			set { _shortcode = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "PinYinZiTou", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string PinYinZiTou
		{
			get { return _pinYinZiTou; }
			set { _pinYinZiTou = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "Comment", Desc = "", ContextType = SysDic.All, Length = 16)]
		public virtual string Comment
		{
			get { return _comment; }
			set { _comment = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "IsUse", Desc = "", ContextType = SysDic.All, Length = 1)]
		public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

		[DataMember]
		[DataDesc(CName = "城市", ShortCode = "BCity", Desc = "城市")]
		public virtual BCity BCity
		{
			get { return _bCity; }
			set { _bCity = value; }
		}


		#endregion
	}
	#endregion
}