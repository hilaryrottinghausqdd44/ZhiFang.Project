using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodTransRecordTypeItem

	/// <summary>
	/// BloodTransRecordTypeItem object for NHibernate mapped table 'Blood_TransRecordTypeItem'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BloodTransRecordTypeItem", ShortCode = "BloodTransRecordTypeItem", Desc = "")]
	public class BloodTransRecordTypeItem : BaseEntity
	{
		#region Member Variables
		
        protected string _transItemCode;
        protected string _cName;
        protected string _sName;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected string _transItemEditInfo;
        protected int _dispOrder;
        protected bool _isVisible;
        protected DateTime? _dataUpdateTime;
		protected BloodTransRecordType _bloodTransRecordType;
		
		#endregion

		#region Constructors

		public BloodTransRecordTypeItem() { }

		public BloodTransRecordTypeItem( long labID, string transItemCode, string transItemName, string sName, string shortcode, string pinYinZiTou, string transItemEditInfo, int dispOrder, bool isVisible, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BloodTransRecordType bloodTransRecordType )
		{
			this._labID = labID;
			this._transItemCode = transItemCode;
			this._cName = transItemName;
			this._sName = sName;
			this._shortcode = shortcode;
			this._pinYinZiTou = pinYinZiTou;
			this._transItemEditInfo = transItemEditInfo;
			this._dispOrder = dispOrder;
			this._isVisible = isVisible;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bloodTransRecordType = bloodTransRecordType;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "记录项编码", ShortCode = "TransItemCode", Desc = "记录项编码", ContextType = SysDic.All, Length = 50)]
        public virtual string TransItemCode
		{
			get { return _transItemCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for TransItemCode", value, value.ToString());
				_transItemCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "记录项名称", ShortCode = "CName", Desc = "记录项名称", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "简称", ShortCode = "SName", Desc = "记录项名称", ContextType = SysDic.All, Length = 80)]
        public virtual string SName
		{
			get { return _sName; }
			set
			{
				if ( value != null && value.Length > 80)
					throw new ArgumentOutOfRangeException("Invalid value for SName", value, value.ToString());
				_sName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "快捷码", ShortCode = "Shortcode", Desc = "快捷码", ContextType = SysDic.All, Length = 50)]
        public virtual string Shortcode
		{
			get { return _shortcode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Shortcode", value, value.ToString());
				_shortcode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "拼音字头", ShortCode = "PinYinZiTou", Desc = "拼音字头", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "其他信息", ShortCode = "TransItemEditInfo", Desc = "其他信息", ContextType = SysDic.All)]
        public virtual string TransItemEditInfo
		{
			get { return _transItemEditInfo; }
			set
			{
				_transItemEditInfo = value;
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
        [DataDesc(CName = "是否使用", ShortCode = "IsVisible", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsVisible
		{
			get { return _isVisible; }
			set { _isVisible = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "更新时间", ShortCode = "DataUpdateTime", Desc = "更新时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "所属内容分类", ShortCode = "BloodTransRecordType", Desc = "所属内容分类")]
		public virtual BloodTransRecordType BloodTransRecordType
		{
			get { return _bloodTransRecordType; }
			set { _bloodTransRecordType = value; }
		}

		#endregion
	}
	#endregion
}