using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LisOperateAuthorize

    /// <summary>
    /// LisOperateAuthorize object for NHibernate mapped table 'Lis_OperateAuthorize'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "LisOperateAuthorize", ShortCode = "LisOperateAuthorize", Desc = "")]
    public class LisOperateAuthorize : BaseEntity
    {
        #region Member Variables

        protected string _operateType;
        protected long? _operateTypeID;
        protected int _authorizeType;
        protected string _authorizeInfo;
        protected long? _authorizeUserID;
        protected string _authorizeUser;
        protected long? _operateUserID;
        protected string _operateUser;
        protected string _operateHost;
        protected string _operateAddress;
        protected bool _isUse;
        protected DateTime? _beginTime;
        protected DateTime? _endTime;
        protected bool _isOnlyUseTime;
        protected bool _day0;
        protected bool _day1;
        protected bool _day2;
        protected bool _day3;
        protected bool _day4;
        protected bool _day5;
        protected bool _day6;
        protected DateTime? _dataUpdateTime;
        protected IList<LisOperateASection> _lisOperateASectionList;


        #endregion

        #region Constructors

        public LisOperateAuthorize() { }

        public LisOperateAuthorize(long labID, string operateType, long operateTypeID, int authorizeType, string authorizeInfo, long authorizeUserID, string authorizeUser, long operateUserID, string operateUser, string operateHost, string operateAddress, bool isUse, DateTime beginTime, DateTime endTime, bool isOnlyUseTime, bool day0, bool day1, bool day2, bool day3, bool day4, bool day5, bool day6, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._operateType = operateType;
            this._operateTypeID = operateTypeID;
            this._authorizeType = authorizeType;
            this._authorizeInfo = authorizeInfo;
            this._authorizeUserID = authorizeUserID;
            this._authorizeUser = authorizeUser;
            this._operateUserID = operateUserID;
            this._operateUser = operateUser;
            this._operateHost = operateHost;
            this._operateAddress = operateAddress;
            this._isUse = isUse;
            this._beginTime = beginTime;
            this._endTime = endTime;
            this._isOnlyUseTime = isOnlyUseTime;
            this._day0 = day0;
            this._day1 = day1;
            this._day2 = day2;
            this._day3 = day3;
            this._day4 = day4;
            this._day5 = day5;
            this._day6 = day6;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "OperateType", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string OperateType
        {
            get { return _operateType; }
            set { _operateType = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OperateTypeID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? OperateTypeID
        {
            get { return _operateTypeID; }
            set { _operateTypeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AuthorizeType", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int AuthorizeType
        {
            get { return _authorizeType; }
            set { _authorizeType = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AuthorizeInfo", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string AuthorizeInfo
        {
            get { return _authorizeInfo; }
            set { _authorizeInfo = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "AuthorizeUserID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? AuthorizeUserID
        {
            get { return _authorizeUserID; }
            set { _authorizeUserID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AuthorizeUser", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string AuthorizeUser
        {
            get { return _authorizeUser; }
            set { _authorizeUser = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OperateUserID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? OperateUserID
        {
            get { return _operateUserID; }
            set { _operateUserID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OperateUser", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string OperateUser
        {
            get { return _operateUser; }
            set { _operateUser = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OperateHost", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string OperateHost
        {
            get { return _operateHost; }
            set { _operateHost = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OperateAddress", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string OperateAddress
        {
            get { return _operateAddress; }
            set { _operateAddress = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsUse", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
        {
            get { return _isUse; }
            set { _isUse = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BeginTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? BeginTime
        {
            get { return _beginTime; }
            set { _beginTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "EndTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsOnlyUseTime", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsOnlyUseTime
        {
            get { return _isOnlyUseTime; }
            set { _isOnlyUseTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Day0", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool Day0
        {
            get { return _day0; }
            set { _day0 = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Day1", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool Day1
        {
            get { return _day1; }
            set { _day1 = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Day2", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool Day2
        {
            get { return _day2; }
            set { _day2 = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Day3", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool Day3
        {
            get { return _day3; }
            set { _day3 = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Day4", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool Day4
        {
            get { return _day4; }
            set { _day4 = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Day5", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool Day5
        {
            get { return _day5; }
            set { _day5 = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Day6", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool Day6
        {
            get { return _day6; }
            set { _day6 = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        //[DataMember]
        //[DataDesc(CName = "Lis_OperateASection 操作授权对应小组", ShortCode = "LisOperateASectionList", Desc = "Lis_OperateASection 操作授权对应小组")]
        //public virtual IList<LisOperateASection> LisOperateASectionList
        //{
        //	get
        //	{
        //		if (_lisOperateASectionList == null)
        //		{
        //			_lisOperateASectionList = new List<LisOperateASection>();
        //		}
        //		return _lisOperateASectionList;
        //	}
        //	set { _lisOperateASectionList = value; }
        //}


        #endregion
    }
    #endregion
}