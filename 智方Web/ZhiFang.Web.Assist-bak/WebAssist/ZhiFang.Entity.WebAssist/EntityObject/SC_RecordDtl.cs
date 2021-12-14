using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.WebAssist
{
	#region SCRecordDtl

	/// <summary>
	/// SCRecordDtl object for NHibernate mapped table 'SC_RecordDtl'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "SCRecordDtl", ShortCode = "SCRecordDtl", Desc = "")]
	public class SCRecordDtl : BaseEntity
	{
		#region Member Variables
		
        protected string _recordDtlNo;
        protected long? _bObjectID;
        protected long? _contentTypeID;

        protected string _itemResult;
        protected double _numberItemResult;
        protected bool _visible;
        protected string _memo;
        protected int _dispOrder;
		protected string _testItemCode;

		protected SCRecordType _sCRecordType;
		protected SCRecordTypeItem _sCRecordTypeItem;

		#endregion

		#region Constructors

		public SCRecordDtl() { }

		public SCRecordDtl( long labID, string recordDtlNo, long bObjectID, long contentTypeID,  string itemResult, double numberItemResult, bool visible, string memo, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp, SCRecordType sCRecordType, SCRecordTypeItem sCRecordTypeItem)
		{
			this._labID = labID;
			this._recordDtlNo = recordDtlNo;
			this._bObjectID = bObjectID;
			this._contentTypeID = contentTypeID;
			this._itemResult = itemResult;
			this._numberItemResult = numberItemResult;
			this._visible = visible;
			this._memo = memo;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;

			this._sCRecordType = sCRecordType;
			this._sCRecordTypeItem = sCRecordTypeItem;
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// 当记录项存在多个检验项目时，存储当前选择的项目编码
		/// </summary>
		[DataMember]
		[DataDesc(CName = "TestItemCode", ShortCode = "TestItemCode", Desc = "TestItemCode", ContextType = SysDic.All, Length = 50)]
		public virtual string TestItemCode
		{
			get { return _testItemCode; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for TestItemCode", value, value.ToString());
				_testItemCode = value;
			}
		}

		[DataMember]
        [DataDesc(CName = "RecordDtlNo", ShortCode = "RecordDtlNo", Desc = "RecordDtlNo", ContextType = SysDic.All, Length = 50)]
        public virtual string RecordDtlNo
		{
			get { return _recordDtlNo; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for RecordDtlNo", value, value.ToString());
				_recordDtlNo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "BObjectID", ShortCode = "BObjectID", Desc = "BObjectID", ContextType = SysDic.All, Length = 8)]
		public virtual long? BObjectID
		{
			get { return _bObjectID; }
			set { _bObjectID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "ContentTypeID", ShortCode = "ContentTypeID", Desc = "ContentTypeID", ContextType = SysDic.All, Length = 8)]
		public virtual long? ContentTypeID
		{
			get { return _contentTypeID; }
			set { _contentTypeID = value; }
		}
        [DataMember]
        [DataDesc(CName = "ItemResult", ShortCode = "ItemResult", Desc = "ItemResult", ContextType = SysDic.All, Length = 300)]
        public virtual string ItemResult
		{
			get { return _itemResult; }
			set
			{
				if ( value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for ItemResult", value, value.ToString());
				_itemResult = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "NumberItemResult", ShortCode = "NumberItemResult", Desc = "NumberItemResult", ContextType = SysDic.All, Length = 8)]
        public virtual double NumberItemResult
		{
			get { return _numberItemResult; }
			set { _numberItemResult = value; }
		}

        [DataMember]
        [DataDesc(CName = "Visible", ShortCode = "Visible", Desc = "Visible", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
		{
			get { return _visible; }
			set { _visible = value; }
		}

        [DataMember]
        [DataDesc(CName = "Memo", ShortCode = "Memo", Desc = "Memo", ContextType = SysDic.All, Length =Int32.MaxValue)]
        public virtual string Memo
		{
			get { return _memo; }
			set
			{
				_memo = value;
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
		[DataDesc(CName = "SCRecordType", ShortCode = "SCRecordType", Desc = "SCRecordType")]
		public virtual SCRecordType SCRecordType
		{
			get { return _sCRecordType; }
			set { _sCRecordType = value; }
		}

		[DataMember]
		[DataDesc(CName = "SCRecordTypeItem", ShortCode = "SCRecordTypeItem", Desc = "SCRecordTypeItem")]
		public virtual SCRecordTypeItem SCRecordTypeItem
		{
			get { return _sCRecordTypeItem; }
			set { _sCRecordTypeItem = value; }
		}
		#endregion

		#region 自定义属性

		protected SCRecordItemLink _sCRecordItemLink;
		[DataMember]
		[DataDesc(CName = "SCRecordItemLink", ShortCode = "SCRecordItemLink", Desc = "SCRecordItemLink")]
		public virtual SCRecordItemLink SCRecordItemLink
		{
			get { return _sCRecordItemLink; }
			set { _sCRecordItemLink = value; }
		}
		#endregion

	}
	#endregion
}