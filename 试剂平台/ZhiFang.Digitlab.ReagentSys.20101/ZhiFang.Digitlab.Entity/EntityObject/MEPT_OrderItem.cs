using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEPTOrderItem

	/// <summary>
	/// MEPTOrderItem object for NHibernate mapped table 'MEPT_OrderItem'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "医嘱单项目", ClassCName = "MEPTOrderItem", ShortCode = "MEPTOrderItem", Desc = "医嘱单项目")]
	public class MEPTOrderItem : BaseEntity
	{
		#region Member Variables
		
        protected int _itemCoefficient;
        protected int _itemSource;
        protected long _sampleTypeID;
        protected decimal _charge;
        protected int _samplingFlag;
        protected int _splitFlag;
        protected string _zDY1;
        protected string _zDY2;
        protected string _zDY3;
        protected string _zDY4;
        protected string _zDY5;
        protected int _dispOrder;
        protected int _deleteFlag;
        protected int _transitFlag;
        protected int _errorFlag;
        protected DateTime? _dataUpdateTime;
		protected ItemAllItem _itemAllItem;
		protected MEPTOrderForm _mEPTOrderForm;
		protected IList<MEPTSampleItem> _mEPTSampleItemList;

		#endregion

		#region Constructors

		public MEPTOrderItem() { }

		public MEPTOrderItem( long labID, int itemCoefficient, int itemSource, long sampleTypeID, decimal charge, int samplingFlag, int splitFlag, string zDY1, string zDY2, string zDY3, string zDY4, string zDY5, int dispOrder, int deleteFlag, int transitFlag, int errorFlag, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, ItemAllItem itemAllItem, MEPTOrderForm mEPTOrderForm )
		{
			this._labID = labID;
			this._itemCoefficient = itemCoefficient;
			this._itemSource = itemSource;
			this._sampleTypeID = sampleTypeID;
			this._charge = charge;
			this._samplingFlag = samplingFlag;
            this._splitFlag = splitFlag;
			this._zDY1 = zDY1;
			this._zDY2 = zDY2;
			this._zDY3 = zDY3;
			this._zDY4 = zDY4;
			this._zDY5 = zDY5;
			this._dispOrder = dispOrder;
			this._deleteFlag = deleteFlag;
			this._transitFlag = transitFlag;
			this._errorFlag = errorFlag;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._itemAllItem = itemAllItem;
			this._mEPTOrderForm = mEPTOrderForm;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "项目系数", ShortCode = "ItemCoefficient", Desc = "项目系数", ContextType = SysDic.All, Length = 4)]
        public virtual int ItemCoefficient
		{
			get { return _itemCoefficient; }
			set { _itemCoefficient = value; }
		}

        [DataMember]
        [DataDesc(CName = "项目来源", ShortCode = "ItemSource", Desc = "项目来源", ContextType = SysDic.All, Length = 4)]
        public virtual int ItemSource
		{
			get { return _itemSource; }
			set { _itemSource = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "样本类型ID", ShortCode = "SampleTypeID", Desc = "样本类型ID", ContextType = SysDic.All, Length = 8)]
		public virtual long SampleTypeID
		{
			get { return _sampleTypeID; }
			set { _sampleTypeID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "费用", ShortCode = "Charge", Desc = "费用", ContextType = SysDic.All, Length = 8)]
        public virtual decimal Charge
		{
			get { return _charge; }
			set { _charge = value; }
		}

        [DataMember]
        [DataDesc(CName = "采样标志", ShortCode = "SamplingFlag", Desc = "采样标志", ContextType = SysDic.All, Length = 4)]
        public virtual int SamplingFlag
		{
			get { return _samplingFlag; }
			set { _samplingFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "拆分标志", ShortCode = "SplitFlag", Desc = "拆分标志", ContextType = SysDic.All, Length = 4)]
        public virtual int SplitFlag
		{
            get { return _splitFlag; }
            set { _splitFlag = value; }
		}
        [DataMember]
        [DataDesc(CName = "自定义1", ShortCode = "ZDY1", Desc = "自定义1", ContextType = SysDic.All, Length = 80)]
        public virtual string ZDY1
		{
			get { return _zDY1; }
			set
			{
				if ( value != null && value.Length > 80)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY1", value, value.ToString());
				_zDY1 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "自定义2", ShortCode = "ZDY2", Desc = "自定义2", ContextType = SysDic.All, Length = 20)]
        public virtual string ZDY2
		{
			get { return _zDY2; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY2", value, value.ToString());
				_zDY2 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "自定义3", ShortCode = "ZDY3", Desc = "自定义3", ContextType = SysDic.All, Length = 20)]
        public virtual string ZDY3
		{
			get { return _zDY3; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY3", value, value.ToString());
				_zDY3 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "自定义4", ShortCode = "ZDY4", Desc = "自定义4", ContextType = SysDic.All, Length = 20)]
        public virtual string ZDY4
		{
			get { return _zDY4; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY4", value, value.ToString());
				_zDY4 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "自定义5", ShortCode = "ZDY5", Desc = "自定义5", ContextType = SysDic.All, Length = 20)]
        public virtual string ZDY5
		{
			get { return _zDY5; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY5", value, value.ToString());
				_zDY5 = value;
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
        [DataDesc(CName = "删除标志", ShortCode = "DeleteFlag", Desc = "删除标志", ContextType = SysDic.All, Length = 4)]
        public virtual int DeleteFlag
		{
			get { return _deleteFlag; }
			set { _deleteFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "迁移标志", ShortCode = "TransitFlag", Desc = "迁移标志", ContextType = SysDic.All, Length = 4)]
        public virtual int TransitFlag
		{
			get { return _transitFlag; }
			set { _transitFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "错误标志", ShortCode = "ErrorFlag", Desc = "错误标志", ContextType = SysDic.All, Length = 4)]
        public virtual int ErrorFlag
		{
			get { return _errorFlag; }
			set { _errorFlag = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "所有项目", ShortCode = "ItemAllItem", Desc = "所有项目")]
		public virtual ItemAllItem ItemAllItem
		{
			get { return _itemAllItem; }
			set { _itemAllItem = value; }
		}

        [DataMember]
        [DataDesc(CName = "医嘱单", ShortCode = "MEPTOrderForm", Desc = "医嘱单")]
		public virtual MEPTOrderForm MEPTOrderForm
		{
			get { return _mEPTOrderForm; }
			set { _mEPTOrderForm = value; }
		}

        [DataMember]
        [DataDesc(CName = "样本单项目", ShortCode = "MEPTSampleItemList", Desc = "样本单项目")]
		public virtual IList<MEPTSampleItem> MEPTSampleItemList
		{
			get
			{
				if (_mEPTSampleItemList==null)
				{
					_mEPTSampleItemList = new List<MEPTSampleItem>();
				}
				return _mEPTSampleItemList;
			}
			set { _mEPTSampleItemList = value; }
		}

        
		#endregion
	}
	#endregion
}