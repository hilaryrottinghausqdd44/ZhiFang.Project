using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
	#region LBItemExp

	/// <summary>
	/// LBItemExp object for NHibernate mapped table 'LB_ItemExp'.
	/// </summary>
	[DataContract]
	[DataDesc(CName = "", ClassCName = "LBItemExp", ShortCode = "LBItemExp", Desc = "")]
	public class LBItemExp : BaseEntity
	{
		#region Member Variables

		protected int _expType;
		protected long? _sectionID;
		protected bool _isUse;
		protected int _dispOrder;
		protected int _dispHeight;
		protected bool _isHyperText;
		protected bool _isTemplate;
		protected string _templateInfo;
		protected DateTime? _dataUpdateTime;
		protected LBItem _lBItem;


		#endregion

		#region Constructors

		public LBItemExp() { }

		public LBItemExp(long labID, int expType, long sectionID, bool isUse, int dispOrder, int dispHeight, bool isHyperText, bool isTemplate, string templateInfo, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, LBItem lBItem)
		{
			this._labID = labID;
			this._expType = expType;
			this._sectionID = sectionID;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._dispHeight = dispHeight;
			this._isHyperText = isHyperText;
			this._isTemplate = isTemplate;
			this._templateInfo = templateInfo;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._lBItem = lBItem;
		}

		#endregion

		#region Public Properties


		[DataMember]
		[DataDesc(CName = "特殊 类型 0：大文本", ShortCode = "ExpType", Desc = "特殊 类型 0：大文本", ContextType = SysDic.All, Length = 4)]
		public virtual int ExpType
		{
			get { return _expType; }
			set { _expType = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "SectionID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? SectionID
		{
			get { return _sectionID; }
			set { _sectionID = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "IsUse", Desc = "", ContextType = SysDic.All, Length = 1)]
		public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "DispHeight", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int DispHeight
		{
			get { return _dispHeight; }
			set { _dispHeight = value; }
		}

		[DataMember]
		[DataDesc(CName = "采用上下标超文本", ShortCode = "IsHyperText", Desc = "采用上下标超文本", ContextType = SysDic.All, Length = 1)]
		public virtual bool IsHyperText
		{
			get { return _isHyperText; }
			set { _isHyperText = value; }
		}

		[DataMember]
		[DataDesc(CName = "采用快捷模板", ShortCode = "IsTemplate", Desc = "采用快捷模板", ContextType = SysDic.All, Length = 1)]
		public virtual bool IsTemplate
		{
			get { return _isTemplate; }
			set { _isTemplate = value; }
		}

		[DataMember]
		[DataDesc(CName = "快捷模板内容", ShortCode = "TemplateInfo", Desc = "快捷模板内容", ContextType = SysDic.All, Length = 16)]
		public virtual string TemplateInfo
		{
			get { return _templateInfo; }
			set { _templateInfo = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "LBItem", Desc = "")]
		public virtual LBItem LBItem
		{
			get { return _lBItem; }
			set { _lBItem = value; }
		}


		#endregion
	}
	#endregion
}