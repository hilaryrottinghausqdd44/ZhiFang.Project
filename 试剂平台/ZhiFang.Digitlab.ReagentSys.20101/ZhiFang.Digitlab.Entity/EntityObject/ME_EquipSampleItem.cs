using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEEquipSampleItem

	/// <summary>
	/// MEEquipSampleItem object for NHibernate mapped table 'ME_EquipSampleItem'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "仪器样本项目", ClassCName = "MEEquipSampleItem", ShortCode = "MEEquipSampleItem", Desc = "仪器样本项目")]
	public class MEEquipSampleItem : BaseEntity
	{
		#region Member Variables
		
        protected int _testType;
        protected string _itemChannel;
        protected string _eReportValue;
        protected string _eResultStatus;
        protected string _eResultAlarm;
        protected string _eTestComment;
        protected string _eOriginalValue;
        protected string _eOriginalResultStatus;
        protected string _eOriginalResultAlarm;
        protected string _eOriginalTestComment;
        protected int _eTestState;
        protected int _itemResultFlag;
        protected string _zDY1;
        protected string _zDY2;
        protected string _zDY3;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
		protected ItemAllItem _itemAllItem;
		protected MEEquipSampleForm _mEEquipSampleForm;
		protected IList<MEGroupSampleItem> _mEGroupSampleItemList; 
		//protected IList<QCDValue> _qCDValueList;

		#endregion

		#region Constructors

		public MEEquipSampleItem() { }

		public MEEquipSampleItem( long labID, int testType, string itemChannel, string eReportValue, string eResultStatus, string eResultAlarm, string eTestComment, string eOriginalValue, string eOriginalResultStatus, string eOriginalResultAlarm, string eOriginalTestComment, int eTestState, int itemResultFlag, string zDY1, string zDY2, string zDY3, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, ItemAllItem itemAllItem, MEEquipSampleForm mEEquipSampleForm )
		{
			this._labID = labID;
			this._testType = testType;
			this._itemChannel = itemChannel;
			this._eReportValue = eReportValue;
			this._eResultStatus = eResultStatus;
			this._eResultAlarm = eResultAlarm;
			this._eTestComment = eTestComment;
			this._eOriginalValue = eOriginalValue;
			this._eOriginalResultStatus = eOriginalResultStatus;
			this._eOriginalResultAlarm = eOriginalResultAlarm;
			this._eOriginalTestComment = eOriginalTestComment;
			this._eTestState = eTestState;
			this._itemResultFlag = itemResultFlag;
			this._zDY1 = zDY1;
			this._zDY2 = zDY2;
			this._zDY3 = zDY3;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._itemAllItem = itemAllItem;
			this._mEEquipSampleForm = mEEquipSampleForm;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "检验结果类型", ShortCode = "TestType", Desc = "检验结果类型", ContextType = SysDic.All, Length = 4)]
        public virtual int TestType
		{
			get { return _testType; }
			set { _testType = value; }
		}

        [DataMember]
        [DataDesc(CName = "通道号", ShortCode = "ItemChannel", Desc = "通道号", ContextType = SysDic.All, Length = 50)]
        public virtual string ItemChannel
		{
			get { return _itemChannel; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ItemChannel", value, value.ToString());
				_itemChannel = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "仪器结果", ShortCode = "EReportValue", Desc = "仪器结果", ContextType = SysDic.All, Length = 300)]
        public virtual string EReportValue
		{
			get { return _eReportValue; }
			set
			{
				if ( value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for EReportValue", value, value.ToString());
				_eReportValue = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "仪器结果状态", ShortCode = "EResultStatus", Desc = "仪器结果状态", ContextType = SysDic.All, Length = 20)]
        public virtual string EResultStatus
		{
			get { return _eResultStatus; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for EResultStatus", value, value.ToString());
				_eResultStatus = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "仪器结果警告", ShortCode = "EResultAlarm", Desc = "仪器结果警告", ContextType = SysDic.All, Length = 20)]
        public virtual string EResultAlarm
		{
			get { return _eResultAlarm; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for EResultAlarm", value, value.ToString());
				_eResultAlarm = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "仪器结果备注", ShortCode = "ETestComment", Desc = "仪器结果备注", ContextType = SysDic.All, Length = 16)]
        public virtual string ETestComment
		{
			get { return _eTestComment; }
			set
			{
				_eTestComment = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "通讯原始结果", ShortCode = "EOriginalValue", Desc = "通讯原始结果", ContextType = SysDic.All, Length = 300)]
        public virtual string EOriginalValue
		{
			get { return _eOriginalValue; }
			set
			{
				if ( value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for EOriginalValue", value, value.ToString());
				_eOriginalValue = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "通讯原始结果状态", ShortCode = "EOriginalResultStatus", Desc = "通讯原始结果状态", ContextType = SysDic.All, Length = 20)]
        public virtual string EOriginalResultStatus
		{
			get { return _eOriginalResultStatus; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for EOriginalResultStatus", value, value.ToString());
				_eOriginalResultStatus = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "通讯原始结果警告", ShortCode = "EOriginalResultAlarm", Desc = "通讯原始结果警告", ContextType = SysDic.All, Length = 20)]
        public virtual string EOriginalResultAlarm
		{
			get { return _eOriginalResultAlarm; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for EOriginalResultAlarm", value, value.ToString());
				_eOriginalResultAlarm = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "仪器结果备注", ShortCode = "EOriginalTestComment", Desc = "仪器结果备注", ContextType = SysDic.All, Length = 16)]
        public virtual string EOriginalTestComment
		{
			get { return _eOriginalTestComment; }
			set
			{
				_eOriginalTestComment = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "仪器检测状态", ShortCode = "ETestState", Desc = "仪器检测状态", ContextType = SysDic.All, Length = 4)]
        public virtual int ETestState
		{
			get { return _eTestState; }
			set { _eTestState = value; }
		}

        [DataMember]
        [DataDesc(CName = "结果导入标志", ShortCode = "ItemResultFlag", Desc = "结果导入标志", ContextType = SysDic.All, Length = 4)]
        public virtual int ItemResultFlag
		{
			get { return _itemResultFlag; }
			set { _itemResultFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "自定义1", ShortCode = "ZDY1", Desc = "自定义1", ContextType = SysDic.All, Length = 200)]
        public virtual string ZDY1
		{
			get { return _zDY1; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY1", value, value.ToString());
				_zDY1 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "自定义2", ShortCode = "ZDY2", Desc = "自定义2", ContextType = SysDic.All, Length = 200)]
        public virtual string ZDY2
		{
			get { return _zDY2; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY2", value, value.ToString());
				_zDY2 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "自定义3", ShortCode = "ZDY3", Desc = "自定义3", ContextType = SysDic.All, Length = 200)]
        public virtual string ZDY3
		{
			get { return _zDY3; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY3", value, value.ToString());
				_zDY3 = value;
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
        [DataDesc(CName = "仪器样本单", ShortCode = "MEEquipSampleForm", Desc = "仪器样本单")]
		public virtual MEEquipSampleForm MEEquipSampleForm
		{
			get { return _mEEquipSampleForm; }
			set { _mEEquipSampleForm = value; }
		}

        [DataMember]
        [DataDesc(CName = "小组样本项目", ShortCode = "MEGroupSampleItemList", Desc = "小组样本项目")]
		public virtual IList<MEGroupSampleItem> MEGroupSampleItemList
		{
			get
			{
				if (_mEGroupSampleItemList==null)
				{
					_mEGroupSampleItemList = new List<MEGroupSampleItem>();
				}
				return _mEGroupSampleItemList;
			}
			set { _mEGroupSampleItemList = value; }
		}

      

        
		#endregion
	}
	#endregion
}