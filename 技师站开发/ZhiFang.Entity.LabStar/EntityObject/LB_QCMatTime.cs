using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBQCMatTime

    /// <summary>
    /// LBQCMatTime object for NHibernate mapped table 'LB_QCMatTime'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "LBQCMatTime", ShortCode = "LBQCMatTime", Desc = "")]
    public class LBQCMatTime : BaseEntity
    {
        #region Member Variables

        protected string _lotNo;
        protected string _manu;
        protected DateTime? _begindate;
        protected DateTime? _endDate;
        protected DateTime? _notUseDate;
        protected string _canUseDateInfo;
        protected string _manuQCInfo;
        protected string _manuQCStoreInfo;
        protected string _qCDesc;
        protected string _comment;
        protected long? _userID;
        protected DateTime? _dataUpdateTime;
        protected LBQCMaterial _lBQCMaterial;
        //protected IList<LBQCItemTime> _lBQCItemTimeList; 


        #endregion

        #region Constructors

        public LBQCMatTime() { }

        public LBQCMatTime(long labID, string lotNo, string manu, DateTime begindate, DateTime endDate, DateTime notUseDate, string canUseDateInfo, string manuQCInfo, string manuQCStoreInfo, string qCDesc, string comment, long userID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, LBQCMaterial lBQCMaterial)
        {
            this._labID = labID;
            this._lotNo = lotNo;
            this._manu = manu;
            this._begindate = begindate;
            this._endDate = endDate;
            this._notUseDate = notUseDate;
            this._canUseDateInfo = canUseDateInfo;
            this._manuQCInfo = manuQCInfo;
            this._manuQCStoreInfo = manuQCStoreInfo;
            this._qCDesc = qCDesc;
            this._comment = comment;
            this._userID = userID;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._lBQCMaterial = lBQCMaterial;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "批号", ShortCode = "LotNo", Desc = "批号", ContextType = SysDic.All, Length = 50)]
        public virtual string LotNo
        {
            get { return _lotNo; }
            set { _lotNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "厂家", ShortCode = "Manu", Desc = "厂家", ContextType = SysDic.All, Length = 50)]
        public virtual string Manu
        {
            get { return _manu; }
            set { _manu = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "开始日期", ShortCode = "Begindate", Desc = "开始日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? Begindate
        {
            get { return _begindate; }
            set { _begindate = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "截止日期", ShortCode = "EndDate", Desc = "截止日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "失效日期", ShortCode = "NotUseDate", Desc = "失效日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? NotUseDate
        {
            get { return _notUseDate; }
            set { _notUseDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "有效期描述", ShortCode = "CanUseDateInfo", Desc = "有效期描述", ContextType = SysDic.All, Length = 50)]
        public virtual string CanUseDateInfo
        {
            get { return _canUseDateInfo; }
            set { _canUseDateInfo = value; }
        }

        [DataMember]
        [DataDesc(CName = "厂家质控物描述", ShortCode = "ManuQCInfo", Desc = "厂家质控物描述", ContextType = SysDic.All, Length = 200)]
        public virtual string ManuQCInfo
        {
            get { return _manuQCInfo; }
            set { _manuQCInfo = value; }
        }

        [DataMember]
        [DataDesc(CName = "存储说明", ShortCode = "ManuQCStoreInfo", Desc = "存储说明", ContextType = SysDic.All, Length = 500)]
        public virtual string ManuQCStoreInfo
        {
            get { return _manuQCStoreInfo; }
            set { _manuQCStoreInfo = value; }
        }

        [DataMember]
        [DataDesc(CName = "质控时效描述", ShortCode = "QCDesc", Desc = "质控时效描述", ContextType = SysDic.All, Length = 500)]
        public virtual string QCDesc
        {
            get { return _qCDesc; }
            set { _qCDesc = value; }
        }

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Comment", Desc = "备注", ContextType = SysDic.All, Length = 500)]
        public virtual string Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "操作者", ShortCode = "UserID", Desc = "操作者", ContextType = SysDic.All, Length = 8)]
        public virtual long? UserID
        {
            get { return _userID; }
            set { _userID = value; }
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
        [DataDesc(CName = "", ShortCode = "LBQCMaterial", Desc = "")]
        public virtual LBQCMaterial LBQCMaterial
        {
            get { return _lBQCMaterial; }
            set { _lBQCMaterial = value; }
        }

        //[DataMember]
        //[DataDesc(CName = "", ShortCode = "LBQCItemTimeList", Desc = "")]
        //public virtual IList<LBQCItemTime> LBQCItemTimeList
        //{
        //	get
        //	{
        //		if (_lBQCItemTimeList==null)
        //		{
        //			_lBQCItemTimeList = new List<LBQCItemTime>();
        //		}
        //		return _lBQCItemTimeList;
        //	}
        //	set { _lBQCItemTimeList = value; }
        //}


        #endregion
    }
    #endregion
}