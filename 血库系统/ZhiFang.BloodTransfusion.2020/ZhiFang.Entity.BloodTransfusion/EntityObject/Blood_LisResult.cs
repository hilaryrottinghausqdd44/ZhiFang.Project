using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodLisResult

	/// <summary>
	/// BloodLisResult object for NHibernate mapped table 'Blood_LisResult'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "Lis相关检验结果表", ClassCName = "BloodLisResult", ShortCode = "BloodLisResult", Desc = "Lis相关检验结果表")]
	public class BloodLisResult : BaseEntity
	{
		#region Member Variables
		
        protected long? _bobjectType;
        protected long _bObjectID;
        protected string _barCode;
        protected long? _receiveId;
        protected string _receiver;
        protected DateTime? _receiveTime;
        protected long? _testOperId;
        protected string _testOperCName;
        protected DateTime? _testOperTime;
        protected long? _checkId;
        protected string _checker;
        protected DateTime? _checkTime;
        protected string _itemLisResult;
        protected string _itemResult;
        protected string _itemUnit;
        protected string _reference;
        protected string _resultHint;
        protected bool _visible;
        protected int _dispOrder;
		protected BloodBTestItem _bloodBTestItem;
		protected BloodPatinfo _bloodPatinfo;

		#endregion

		#region Constructors

		public BloodLisResult() { }

		public BloodLisResult( long labID, long bobjectType, long bObjectID, string barCode, long receiveId, string receiver, DateTime receiveTime, long testOperId, string testOperCName, DateTime testOperTime, long checkId, string checker, DateTime checkTime, string itemLisResult, string itemResult, string itemUnit, string reference, string resultHint, bool visible, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp, BloodBTestItem bloodBTestItem, BloodPatinfo bloodPatinfo )
		{
			this._labID = labID;
			this._bobjectType = bobjectType;
			this._bObjectID = bObjectID;
			this._barCode = barCode;
			this._receiveId = receiveId;
			this._receiver = receiver;
			this._receiveTime = receiveTime;
			this._testOperId = testOperId;
			this._testOperCName = testOperCName;
			this._testOperTime = testOperTime;
			this._checkId = checkId;
			this._checker = checker;
			this._checkTime = checkTime;
			this._itemLisResult = itemLisResult;
			this._itemResult = itemResult;
			this._itemUnit = itemUnit;
			this._reference = reference;
			this._resultHint = resultHint;
			this._visible = visible;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bloodBTestItem = bloodBTestItem;
			this._bloodPatinfo = bloodPatinfo;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "业务类型", ShortCode = "BobjectType", Desc = "业务类型", ContextType = SysDic.All, Length = 8)]
		public virtual long? BobjectType
		{
			get { return _bobjectType; }
			set { _bobjectType = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "业务对象ID", ShortCode = "BObjectID", Desc = "业务对象ID", ContextType = SysDic.All, Length = 8)]
        public virtual long BObjectID
		{
			get { return _bObjectID; }
			set { _bObjectID = value; }
		}

        [DataMember]
        [DataDesc(CName = "条码号", ShortCode = "BarCode", Desc = "条码号", ContextType = SysDic.All, Length = 50)]
        public virtual string BarCode
		{
			get { return _barCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for BarCode", value, value.ToString());
				_barCode = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "样本接收人Id", ShortCode = "ReceiveId", Desc = "样本接收人Id", ContextType = SysDic.All, Length = 8)]
		public virtual long? ReceiveId
		{
			get { return _receiveId; }
			set { _receiveId = value; }
		}

        [DataMember]
        [DataDesc(CName = "样本接收人", ShortCode = "Receiver", Desc = "样本接收人", ContextType = SysDic.All, Length = 50)]
        public virtual string Receiver
		{
			get { return _receiver; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Receiver", value, value.ToString());
				_receiver = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "样本接收时间", ShortCode = "ReceiveTime", Desc = "样本接收时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ReceiveTime
		{
			get { return _receiveTime; }
			set { _receiveTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "检验人员Id ", ShortCode = "TestOperId", Desc = "检验人员Id ", ContextType = SysDic.All, Length = 8)]
		public virtual long? TestOperId
		{
			get { return _testOperId; }
			set { _testOperId = value; }
		}

        [DataMember]
        [DataDesc(CName = "检验人", ShortCode = "TestOperCName", Desc = "检验人", ContextType = SysDic.All, Length = 50)]
        public virtual string TestOperCName
		{
			get { return _testOperCName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for TestOperCName", value, value.ToString());
				_testOperCName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "检验时间", ShortCode = "TestOperTime", Desc = "检验时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? TestOperTime
		{
			get { return _testOperTime; }
			set { _testOperTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审核人员Id", ShortCode = "CheckId", Desc = "审核人员Id", ContextType = SysDic.All, Length = 8)]
		public virtual long? CheckId
		{
			get { return _checkId; }
			set { _checkId = value; }
		}

        [DataMember]
        [DataDesc(CName = "审核人", ShortCode = "Checker", Desc = "审核人", ContextType = SysDic.All, Length = 50)]
        public virtual string Checker
		{
			get { return _checker; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Checker", value, value.ToString());
				_checker = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = " 审核时间", ShortCode = "CheckTime", Desc = " 审核时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? CheckTime
		{
			get { return _checkTime; }
			set { _checkTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "Lis原始结果", ShortCode = "ItemLisResult", Desc = "Lis原始结果", ContextType = SysDic.All, Length = 500)]
        public virtual string ItemLisResult
		{
			get { return _itemLisResult; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for ItemLisResult", value, value.ToString());
				_itemLisResult = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "检验结果", ShortCode = "ItemResult", Desc = "检验结果", ContextType = SysDic.All, Length = 500)]
        public virtual string ItemResult
		{
			get { return _itemResult; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for ItemResult", value, value.ToString());
				_itemResult = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "单位", ShortCode = "ItemUnit", Desc = "单位", ContextType = SysDic.All, Length = 500)]
        public virtual string ItemUnit
		{
			get { return _itemUnit; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for ItemUnit", value, value.ToString());
				_itemUnit = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "参考值范围", ShortCode = "Reference", Desc = "参考值范围", ContextType = SysDic.All, Length = 500)]
        public virtual string Reference
		{
			get { return _reference; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for Reference", value, value.ToString());
				_reference = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "高低值状态", ShortCode = "ResultHint", Desc = "高低值状态", ContextType = SysDic.All, Length = 50)]
        public virtual string ResultHint
		{
			get { return _resultHint; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ResultHint", value, value.ToString());
				_resultHint = value;
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
        [DataDesc(CName = "检验项目表 ", ShortCode = "BloodBTestItem", Desc = "检验项目表 ")]
		public virtual BloodBTestItem BloodBTestItem
		{
			get { return _bloodBTestItem; }
			set { _bloodBTestItem = value; }
		}

        [DataMember]
        [DataDesc(CName = "病人就诊记录信息表", ShortCode = "BloodPatinfo", Desc = "病人就诊记录信息表")]
		public virtual BloodPatinfo BloodPatinfo
		{
			get { return _bloodPatinfo; }
			set { _bloodPatinfo = value; }
		}

        
		#endregion
	}
	#endregion
}