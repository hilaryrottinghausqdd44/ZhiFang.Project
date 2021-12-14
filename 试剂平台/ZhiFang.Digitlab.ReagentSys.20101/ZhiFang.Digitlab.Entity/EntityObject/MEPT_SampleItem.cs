using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEPTSampleItem

	/// <summary>
	/// MEPTSampleItem object for NHibernate mapped table 'MEPT_SampleItem'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "样本单项目", ClassCName = "MEPTSampleItem", ShortCode = "MEPTSampleItem", Desc = "样本单项目")]
	public class MEPTSampleItem : BaseEntity
	{
		#region Member Variables
		
        protected int _receiveFlag;
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
        protected MEPTGetReportTime _mEPTGetReportTime;
        protected MEPTOrderItem _mEPTOrderItem;
        protected MEPTSampleForm _sampleFrom;
        protected IList<MEGroupSampleItem> _mEGroupSampleItemList;

        #endregion

        #region Constructors

        public MEPTSampleItem() { }

        public MEPTSampleItem(long labID, int receiveFlag, string zDY1, string zDY2, string zDY3, string zDY4, string zDY5, int dispOrder, int deleteFlag, int transitFlag, int errorFlag, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, ItemAllItem itemAllItem, MEPTGetReportTime mEPTGetReportTime, MEPTOrderItem mEPTOrderItem, MEPTSampleForm sampleFrom)
        {
            this._labID = labID;
            this._receiveFlag = receiveFlag;
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
            this._mEPTGetReportTime = mEPTGetReportTime;
            this._mEPTOrderItem = mEPTOrderItem;
            this._sampleFrom = sampleFrom;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "核收状态", ShortCode = "ReceiveFlag", Desc = "核收状态", ContextType = SysDic.All, Length = 4)]
        public virtual int ReceiveFlag
        {
            get { return _receiveFlag; }
            set { _receiveFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "自定义1", ShortCode = "ZDY1", Desc = "自定义1", ContextType = SysDic.All, Length = 200)]
        public virtual string ZDY1
        {
            get { return _zDY1; }
            set
            {
                if (value != null && value.Length > 200)
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
                if (value != null && value.Length > 200)
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
                if (value != null && value.Length > 200)
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
                if (value != null && value.Length > 200)
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
                if (value != null && value.Length > 200)
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
        [DataDesc(CName = "取单时间", ShortCode = "MEPTGetReportTime", Desc = "取单时间")]
        public virtual MEPTGetReportTime MEPTGetReportTime
        {
            get { return _mEPTGetReportTime; }
            set { _mEPTGetReportTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "医嘱单项目", ShortCode = "MEPTOrderItem", Desc = "医嘱单项目")]
        public virtual MEPTOrderItem MEPTOrderItem
        {
            get { return _mEPTOrderItem; }
            set { _mEPTOrderItem = value; }
        }

        [DataMember]
        [DataDesc(CName = "样本单", ShortCode = "SampleFrom", Desc = "样本单")]
        public virtual MEPTSampleForm SampleFrom
        {
            get { return _sampleFrom; }
            set { _sampleFrom = value; }
        }

        [DataMember]
        [DataDesc(CName = "小组样本项目", ShortCode = "MEGroupSampleItemList", Desc = "小组样本项目")]
        public virtual IList<MEGroupSampleItem> MEGroupSampleItemList
        {
            get
            {
                if (_mEGroupSampleItemList == null)
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