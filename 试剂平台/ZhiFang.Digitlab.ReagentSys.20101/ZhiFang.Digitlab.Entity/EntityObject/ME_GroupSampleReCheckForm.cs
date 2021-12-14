using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEGroupSampleReCheckForm

	/// <summary>
	/// MEGroupSampleReCheckForm object for NHibernate mapped table 'ME_GroupSampleReCheckForm'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "复检记录", ClassCName = "MEGroupSampleReCheckForm", ShortCode = "MEGroupSampleReCheckForm", Desc = "复检记录")]
	public class MEGroupSampleReCheckForm : BaseEntity
	{
		#region Member Variables
		
        protected long? _groupID;
        protected long? _sampleFormID;
        protected DateTime? _gTestDate;
        protected string _gSampleNo;
        protected int _gTestNo;
        protected long? _gSampleTypeID;
        protected string _gSampleInfo;
        protected string _testComment;
        protected string _formMemo;
        protected string _zDY1;
        protected string _zDY2;
        protected string _zDY3;
        protected string _zDY4;
        protected string _zDY5;
        protected DateTime? _dataUpdateTime;
        protected int _sort;
		protected BReCheckReason _bReCheckReason;
		protected MEGroupSampleForm _mEGroupSampleForm;
		protected IList<MEGroupSampleReCheckItem> _mEGroupSampleReCheckItemList; 

		#endregion

		#region Constructors

		public MEGroupSampleReCheckForm() { }

		public MEGroupSampleReCheckForm( long groupID, long sampleFormID, DateTime gTestDate, string gSampleNo, int gTestNo, long gSampleTypeID, string gSampleInfo, string testComment, string formMemo, string zDY1, string zDY2, string zDY3, string zDY4, string zDY5, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, int sort, BReCheckReason bReCheckReason, MEGroupSampleForm mEGroupSampleForm )
		{
			this._groupID = groupID;
			this._sampleFormID = sampleFormID;
			this._gTestDate = gTestDate;
			this._gSampleNo = gSampleNo;
			this._gTestNo = gTestNo;
			this._gSampleTypeID = gSampleTypeID;
			this._gSampleInfo = gSampleInfo;
			this._testComment = testComment;
			this._formMemo = formMemo;
			this._zDY1 = zDY1;
			this._zDY2 = zDY2;
			this._zDY3 = zDY3;
			this._zDY4 = zDY4;
			this._zDY5 = zDY5;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._sort = sort;
			this._bReCheckReason = bReCheckReason;
			this._mEGroupSampleForm = mEGroupSampleForm;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "小组ID", ShortCode = "GroupID", Desc = "小组ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? GroupID
		{
			get { return _groupID; }
			set { _groupID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "样本单ID", ShortCode = "SampleFormID", Desc = "样本单ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? SampleFormID
		{
			get { return _sampleFormID; }
			set { _sampleFormID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "检测日期", ShortCode = "GTestDate", Desc = "检测日期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? GTestDate
		{
			get { return _gTestDate; }
			set { _gTestDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "小组检测编号", ShortCode = "GSampleNo", Desc = "小组检测编号", ContextType = SysDic.All, Length = 20)]
        public virtual string GSampleNo
		{
			get { return _gSampleNo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for GSampleNo", value, value.ToString());
				_gSampleNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "日检测样本序号", ShortCode = "GTestNo", Desc = "日检测样本序号", ContextType = SysDic.All, Length = 4)]
        public virtual int GTestNo
		{
			get { return _gTestNo; }
			set { _gTestNo = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "样本类型", ShortCode = "GSampleTypeID", Desc = "样本类型", ContextType = SysDic.All, Length = 8)]
		public virtual long? GSampleTypeID
		{
			get { return _gSampleTypeID; }
			set { _gSampleTypeID = value; }
		}

        [DataMember]
        [DataDesc(CName = "小组样本描述", ShortCode = "GSampleInfo", Desc = "小组样本描述", ContextType = SysDic.All, Length = 50)]
        public virtual string GSampleInfo
		{
			get { return _gSampleInfo; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for GSampleInfo", value, value.ToString());
				_gSampleInfo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "检验备注", ShortCode = "TestComment", Desc = "检验备注", ContextType = SysDic.All, Length = 16)]
        public virtual string TestComment
		{
			get { return _testComment; }
			set
			{
				_testComment = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "FormMemo", Desc = "备注", ContextType = SysDic.All, Length = 16)]
        public virtual string FormMemo
		{
			get { return _formMemo; }
			set
			{
				_formMemo = value;
			}
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
        [DataDesc(CName = "自定义4", ShortCode = "ZDY4", Desc = "自定义4", ContextType = SysDic.All, Length = 200)]
        public virtual string ZDY4
		{
			get { return _zDY4; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY4", value, value.ToString());
				_zDY4 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "自定义5", ShortCode = "ZDY5", Desc = "自定义5", ContextType = SysDic.All, Length = 200)]
        public virtual string ZDY5
		{
			get { return _zDY5; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ZDY5", value, value.ToString());
				_zDY5 = value;
			}
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
        [DataDesc(CName = "复检检验序号", ShortCode = "Sort", Desc = "复检检验序号", ContextType = SysDic.All, Length = 4)]
        public virtual int Sort
		{
			get { return _sort; }
			set { _sort = value; }
		}

        [DataMember]
        [DataDesc(CName = "复检原因", ShortCode = "BReCheckReason", Desc = "复检原因")]
		public virtual BReCheckReason BReCheckReason
		{
			get { return _bReCheckReason; }
			set { _bReCheckReason = value; }
		}

        [DataMember]
        [DataDesc(CName = "小组样本单", ShortCode = "MEGroupSampleForm", Desc = "小组样本单")]
		public virtual MEGroupSampleForm MEGroupSampleForm
		{
			get { return _mEGroupSampleForm; }
			set { _mEGroupSampleForm = value; }
		}

        [DataMember]
        [DataDesc(CName = "复检项目", ShortCode = "MEGroupSampleReCheckItemList", Desc = "复检项目")]
		public virtual IList<MEGroupSampleReCheckItem> MEGroupSampleReCheckItemList
		{
			get
			{
				if (_mEGroupSampleReCheckItemList==null)
				{
					_mEGroupSampleReCheckItemList = new List<MEGroupSampleReCheckItem>();
				}
				return _mEGroupSampleReCheckItemList;
			}
			set { _mEGroupSampleReCheckItemList = value; }
		}

        
		#endregion
	}
	#endregion
}