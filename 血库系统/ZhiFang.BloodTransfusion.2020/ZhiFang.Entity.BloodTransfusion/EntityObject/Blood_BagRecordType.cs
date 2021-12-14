using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodBagRecordType

	/// <summary>
	/// BloodBagRecordType object for NHibernate mapped table 'Blood_BagRecordType'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "血袋记录类型字典表", ClassCName = "BloodBagRecordType", ShortCode = "BloodBagRecordType", Desc = "血袋记录类型字典表")]
	public class BloodBagRecordType : BaseEntity
	{
		#region Member Variables
		
        protected int _contentTypeID;
        protected string _cName;
		protected string _sName;
		protected string _shortcode;
		protected string _pinYinZiTou;
		protected string _typeCode;
        protected int _dispOrder;
        protected string _memo;
        protected bool _isUse;
        protected DateTime? _dataUpdateTime;
		protected long? _transTypeId;
		#endregion

		#region Constructors

		public BloodBagRecordType() { }

		public BloodBagRecordType( long labID, int contentTypeID, long? transTypeId, string cName, string sName, string shortcode, string pinYinZiTou, string typeCode, int dispOrder, string memo, bool isUse, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._contentTypeID = contentTypeID;
			this._transTypeId = transTypeId;
			this._cName = cName;
			this._sName = sName;
			this._shortcode = shortcode;
			this._pinYinZiTou = pinYinZiTou;
			this._typeCode = typeCode;
			this._dispOrder = dispOrder;
			this._memo = memo;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "输血记录类型", ShortCode = "TransTypeId", Desc = "输血记录类型", ContextType = SysDic.Number, Length = 8)]
		public virtual long? TransTypeId
		{
			get
			{
				return _transTypeId;
			}
			set { _transTypeId = value; }
		}
		[DataMember]
        [DataDesc(CName = "内容分类ID", ShortCode = "ContentTypeID", Desc = "内容分类ID", ContextType = SysDic.All, Length = 4)]
        public virtual int ContentTypeID
		{
			get { return _contentTypeID; }
			set { _contentTypeID = value; }
		}

        [DataMember]
        [DataDesc(CName = "记录分类名称", ShortCode = "CName", Desc = "记录分类名称", ContextType = SysDic.All, Length = 50)]
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
		[DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 80)]
		public virtual string SName
		{
			get { return _sName; }
			set
			{
				if (value != null && value.Length > 80)
					throw new ArgumentOutOfRangeException("Invalid value for SName", value, value.ToString());
				_sName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "快捷码", ShortCode = "ShortCode", Desc = "快捷码", ContextType = SysDic.All, Length = 50)]
		public virtual string ShortCode
		{
			get { return _shortcode; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ShortCode", value, value.ToString());
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
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for PinYinZiTou", value, value.ToString());
				_pinYinZiTou = value;
			}
		}
		[DataMember]
        [DataDesc(CName = "记录分类编码", ShortCode = "TypeCode", Desc = "记录分类编码", ContextType = SysDic.All, Length = 50)]
        public virtual string TypeCode
		{
			get { return _typeCode; }
			set { _typeCode = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否可用", ShortCode = "DispOrder", Desc = "是否可用", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = 214748364)]
        public virtual string Memo
		{
			get { return _memo; }
			set
			{
				_memo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "是否可用", ShortCode = "IsUse", Desc = "是否可用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据修改时间", ShortCode = "DataUpdateTime", Desc = "数据修改时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

		#endregion
	}
	#endregion
}