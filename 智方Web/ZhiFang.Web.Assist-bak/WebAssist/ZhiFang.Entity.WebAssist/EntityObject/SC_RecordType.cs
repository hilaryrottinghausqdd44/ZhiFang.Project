using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.WebAssist
{
	#region SCRecordType

	/// <summary>
	/// SCRecordType object for NHibernate mapped table 'SC_RecordType'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "SCRecordType", ShortCode = "SCRecordType", Desc = "")]
	public class SCRecordType : BaseEntity
	{
		#region Member Variables
		
        protected int _contentTypeID;
        protected string _cName;
        protected string _testItemCode;
        protected string _sampleTypeCode;
        protected string _typeCode;
        protected string _sName;
        protected string _shortCode;
        protected string _pinYinZiTou;
        protected int _dispOrder;
        protected string _description;
		protected string _bGColor;
		protected string _memo;
        protected bool _isUse;
        protected DateTime? _dataUpdateTime;
		
		#endregion

		#region Constructors

		public SCRecordType() { }

		public SCRecordType( long labID, int contentTypeID, string cName, string typeCode, string sName, string shortCode, string pinYinZiTou, int dispOrder, string description, string memo, bool isUse, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._contentTypeID = contentTypeID;
			this._cName = cName;
			this._typeCode = typeCode;
			this._sName = sName;
			this._shortCode = shortCode;
			this._pinYinZiTou = pinYinZiTou;
			this._dispOrder = dispOrder;
			this._description = description;
			this._memo = memo;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
		}

        #endregion

        #region Public Properties

        [DataMember]
        [DataDesc(CName = "SampleTypeCode", ShortCode = "SampleTypeCode", Desc = "SampleTypeCode", ContextType = SysDic.All, Length = 60)]
        public virtual string SampleTypeCode
        {
            get { return _sampleTypeCode; }
            set { _sampleTypeCode = value; }
        }
        [DataMember]
        [DataDesc(CName = "TestItemCode", ShortCode = "TestItemCode", Desc = "TestItemCode", ContextType = SysDic.All, Length =60)]
        public virtual string TestItemCode
        {
            get { return _testItemCode; }
            set { _testItemCode = value; }
        }

        [DataMember]
		[DataDesc(CName = "BGColor", ShortCode = "BGColor", Desc = "BGColor", ContextType = SysDic.All, Length = 60)]
		public virtual string BGColor
		{
			get { return _bGColor; }
			set
			{
				_bGColor = value;
			}
		}

		[DataMember]
        [DataDesc(CName = "", ShortCode = "ContentTypeID", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ContentTypeID
		{
			get { return _contentTypeID; }
			set { _contentTypeID = value; }
		}

        [DataMember]
        [DataDesc(CName = "CName", ShortCode = "CName", Desc = "CName", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "TypeCode", ShortCode = "TypeCode", Desc = "TypeCode", ContextType = SysDic.All, Length = 50)]
        public virtual string TypeCode
		{
			get { return _typeCode; }
			set { _typeCode = value; }
		}

        [DataMember]
        [DataDesc(CName = "SName", ShortCode = "SName", Desc = "SName", ContextType = SysDic.All, Length = 80)]
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
        [DataDesc(CName = "ShortCode", ShortCode = "ShortCode", Desc = "ShortCode", ContextType = SysDic.All, Length = 50)]
        public virtual string ShortCode
		{
			get { return _shortCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ShortCode", value, value.ToString());
				_shortCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "PinYinZiTou", ShortCode = "PinYinZiTou", Desc = "PinYinZiTou", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "DispOrder", ShortCode = "DispOrder", Desc = "DispOrder", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "Description", ShortCode = "Description", Desc = "Description", ContextType = SysDic.All, Length =int.MaxValue)]
        public virtual string Description
		{
			get { return _description; }
			set
			{
				_description = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "Memo", ShortCode = "Memo", Desc = "Memo", ContextType = SysDic.All, Length = int.MaxValue)]
        public virtual string Memo
		{
			get { return _memo; }
			set
			{
				_memo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "IsUse", ShortCode = "IsUse", Desc = "IsUse", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "DataUpdateTime", ShortCode = "DataUpdateTime", Desc = "DataUpdateTime", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

		#endregion

		#region 自定义属性
		[DataMember]
		[DataDesc(CName = "TestItemCName", ShortCode = "TestItemCName", Desc = "TestItemCName", ContextType = SysDic.All, Length = 120)]
		public virtual string TestItemCName { get; set; }

		[DataMember]
		[DataDesc(CName = "SampleTypeCName", ShortCode = "SampleTypeCName", Desc = "SampleTypeCName", ContextType = SysDic.All, Length = 120)]
		public virtual string SampleTypeCName { get; set; }
		#endregion

	}
	#endregion
}