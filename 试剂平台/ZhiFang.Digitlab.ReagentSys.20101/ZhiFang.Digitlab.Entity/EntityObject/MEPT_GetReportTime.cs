using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEPTGetReportTime

	/// <summary>
	/// MEPTGetReportTime object for NHibernate mapped table 'MEPT_GetReportTime'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "取单时间", ClassCName = "MEPTGetReportTime", ShortCode = "MEPTGetReportTime", Desc = "取单时间")]
    public class MEPTGetReportTime : BaseEntity
	{
		#region Member Variables		
        
        protected string _cName;
        protected string _sName;
        protected int _dispOrder;
        protected string _comment;
        protected bool _isUse;
        protected string _pinYinZiTou;
        protected string _timeComment;
        protected string _allTimeComment;
        protected int _needTime;
        protected DateTime? _dataUpdateTime;
        protected MEPTBSpecialTimeType _mEPTBSpecialTimeType;
		protected IList<MEPTGetReportTimeOfItem> _mEPTGetReportTimeOfItemList;
        protected IList<MEPTGetReportTimeOfSickType> _mEPTGetReportTimeOfSickTypeList;
        protected IList<MEPTSampleItem> _mEPTSampleItemList;

        #endregion

        #region Constructors

        public MEPTGetReportTime() { }

        public MEPTGetReportTime(long labID, string cName, string sName, string pinYinZiTou, string timeComment, string allTimeComment, int dispOrder, string comment, bool isUse, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, MEPTBSpecialTimeType mEPTBSpecialTimeType)
        {
            this._labID = labID;
            this._cName = cName;
            this._sName = sName;
            this._pinYinZiTou = pinYinZiTou;
            this._timeComment = timeComment;
            this._allTimeComment = allTimeComment;
            this._dispOrder = dispOrder;
            this._comment = comment;
            this._isUse = isUse;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._mEPTBSpecialTimeType = mEPTBSpecialTimeType;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "CName", Desc = "名称", ContextType = SysDic.All, Length = 50)]
        public virtual string CName
        {
            get { return _cName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
                _cName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 50)]
        public virtual string SName
        {
            get { return _sName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for SName", value, value.ToString());
                _sName = value;
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
        [DataDesc(CName = "时间描述", ShortCode = "TimeComment", Desc = "时间描述", ContextType = SysDic.All, Length = 500)]
        public virtual string TimeComment
        {
            get { return _timeComment; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for TimeComment", value, value.ToString());
                _timeComment = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "时间总述", ShortCode = "AllTimeComment", Desc = "时间总述", ContextType = SysDic.All, Length = 500)]
        public virtual string AllTimeComment
        {
            get { return _allTimeComment; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for AllTimeComment", value, value.ToString());
                _allTimeComment = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "具体需要的检验时间,int表示分钟", ShortCode = "NeedTime", Desc = "具体需要的检验时间,int表示分钟", ContextType = SysDic.All, Length = 4)]
        public virtual int NeedTime
        {
            get { return _needTime; }
            set { _needTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "说明", ShortCode = "Comment", Desc = "说明", ContextType = SysDic.All, Length = 16)]
        public virtual string Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
        {
            get { return _isUse; }
            set { _isUse = value; }
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
        [DataDesc(CName = "特定时间类型", ShortCode = "MEPTBSpecialTimeType", Desc = "特定时间类型")]
        public virtual MEPTBSpecialTimeType MEPTBSpecialTimeType
        {
            get { return _mEPTBSpecialTimeType; }
            set { _mEPTBSpecialTimeType = value; }
        }

        [DataMember]
        [DataDesc(CName = "取单时间-项目关系表", ShortCode = "MEPTGetReportTimeOfItemList", Desc = "取单时间-项目关系表")]
        public virtual IList<MEPTGetReportTimeOfItem> MEPTGetReportTimeOfItemList
        {
            get
            {
                if (_mEPTGetReportTimeOfItemList == null)
                {
                    _mEPTGetReportTimeOfItemList = new List<MEPTGetReportTimeOfItem>();
                }
                return _mEPTGetReportTimeOfItemList;
            }
            set { _mEPTGetReportTimeOfItemList = value; }
        }

        [DataMember]
        [DataDesc(CName = "取单时间-就诊类型关系", ShortCode = "MEPTGetReportTimeOfSickTypeList", Desc = "取单时间-就诊类型关系")]
        public virtual IList<MEPTGetReportTimeOfSickType> MEPTGetReportTimeOfSickTypeList
        {
            get
            {
                if (_mEPTGetReportTimeOfSickTypeList == null)
                {
                    _mEPTGetReportTimeOfSickTypeList = new List<MEPTGetReportTimeOfSickType>();
                }
                return _mEPTGetReportTimeOfSickTypeList;
            }
            set { _mEPTGetReportTimeOfSickTypeList = value; }
        }

        [DataMember]
        [DataDesc(CName = "样本单项目", ShortCode = "MEPTSampleItemList", Desc = "样本单项目")]
        public virtual IList<MEPTSampleItem> MEPTSampleItemList
        {
            get
            {
                if (_mEPTSampleItemList == null)
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