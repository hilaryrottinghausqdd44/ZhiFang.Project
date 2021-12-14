using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodBagRecordDtl

	/// <summary>
	/// BloodBagRecordDtl object for NHibernate mapped table 'Blood_BagRecordDtl'.
	/// </summary>
	[DataContract]
	[DataDesc(CName = "血袋核对记录明细表", ClassCName = "BloodBagRecordDtl", ShortCode = "BloodBagRecordDtl", Desc = "血袋核对记录明细表")]
	public class BloodBagRecordDtl : BaseEntity
	{
		#region Member Variables

		protected long _recordType;
		protected long _bobjectID;
		protected string _itemResult;
		protected double _numberItemResult;
		protected long? _creatorID;
		protected string _creatorName;
		protected bool _visible;
		protected int _dispOrder;
		protected string _memo;
		protected BloodBagRecordItem _bloodBagRecordItem;
		protected BloodBagRecordType _bloodBagRecordType;
		protected BloodStyle _bloodStyle;

		#endregion

		#region Constructors

		public BloodBagRecordDtl() { }

		public BloodBagRecordDtl(long labID, long recordType, long bobjectID, string itemResult, double numberItemResult, long creatorID, string creatorName, bool visible, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp, string memo, BloodBagRecordItem bloodBagRecordItem, BloodBagRecordType bloodBagRecordType, BloodStyle bloodStyle)
		{
			this._labID = labID;
			this._recordType = recordType;
			this._bobjectID = bobjectID;
			this._itemResult = itemResult;
			this._numberItemResult = numberItemResult;
			this._creatorID = creatorID;
			this._creatorName = creatorName;
			this._visible = visible;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._memo = memo;
			this._bloodBagRecordItem = bloodBagRecordItem;
			this._bloodBagRecordType = bloodBagRecordType;
			this._bloodStyle = bloodStyle;
		}

		#endregion

		#region Public Properties


		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "记录类型", ShortCode = "RecordType", Desc = "记录类型", ContextType = SysDic.All, Length = 8)]
		public virtual long RecordType
		{
			get { return _recordType; }
			set { _recordType = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "业务明细ID，可能是入库明细ID,库存记录ID,出库明细ID,血袋交接明细ID等", ShortCode = "BobjectID", Desc = "业务明细ID，可能是入库明细ID,库存记录ID,出库明细ID,血袋交接明细ID等", ContextType = SysDic.All, Length = 8)]
		public virtual long BobjectID
		{
			get { return _bobjectID; }
			set { _bobjectID = value; }
		}

		[DataMember]
		[DataDesc(CName = "结果值", ShortCode = "ItemResult", Desc = "结果值", ContextType = SysDic.All, Length = 200)]
		public virtual string ItemResult
		{
			get { return _itemResult; }
			set
			{
				if (value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ItemResult", value, value.ToString());
				_itemResult = value;
			}
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "数字型结果值", ShortCode = "NumberItemResult", Desc = "数字型结果值", ContextType = SysDic.All, Length = 8)]
		public virtual double NumberItemResult
		{
			get { return _numberItemResult; }
			set { _numberItemResult = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "创建者", ShortCode = "CreatorID", Desc = "创建者", ContextType = SysDic.All, Length = 8)]
		public virtual long? CreatorID
		{
			get { return _creatorID; }
			set { _creatorID = value; }
		}

		[DataMember]
		[DataDesc(CName = "创建者姓名", ShortCode = "CreatorName", Desc = "创建者姓名", ContextType = SysDic.All, Length = 50)]
		public virtual string CreatorName
		{
			get { return _creatorName; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for CreatorName", value, value.ToString());
				_creatorName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "是否使用", ShortCode = "Visible", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
		public virtual bool Visible
		{
			get { return _visible; }
			set { _visible = value; }
		}

		[DataMember]
		[DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
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
		[DataDesc(CName = "血袋记录明细字典表", ShortCode = "BloodBagRecordItem", Desc = "血袋记录明细字典表")]
		public virtual BloodBagRecordItem BloodBagRecordItem
		{
			get { return _bloodBagRecordItem; }
			set { _bloodBagRecordItem = value; }
		}

		[DataMember]
		[DataDesc(CName = "血袋记录类型字典表", ShortCode = "BloodBagRecordType", Desc = "血袋记录类型字典表")]
		public virtual BloodBagRecordType BloodBagRecordType
		{
			get { return _bloodBagRecordType; }
			set { _bloodBagRecordType = value; }
		}

		[DataMember]
		[DataDesc(CName = "血制品字典", ShortCode = "BloodStyle", Desc = "血制品字典")]
		public virtual BloodStyle BloodStyle
		{
			get { return _bloodStyle; }
			set { _bloodStyle = value; }
		}


		#endregion
	}
	#endregion
}