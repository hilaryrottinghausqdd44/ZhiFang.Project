using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBSamplingGroup

    /// <summary>
    /// LBSamplingGroup object for NHibernate mapped table 'LB_SamplingGroup'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "采样组", ClassCName = "LBSamplingGroup", ShortCode = "LBSamplingGroup", Desc = "采样组")]
    public class LBSamplingGroup : BaseEntity
    {
        #region Member Variables

        protected string _cName;
        protected long? _sampleTypeID;
        protected string _sName;
        protected string _sCode;
        protected long? _specialtyID;
        protected long? _destinationID;
        //protected string _destination;
        protected string _synopsis;
        protected int _printCount;
        protected string _affixTubeFlag;
        protected string _prepInfo;
        protected int _virtualNo;
        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected LBTcuvete _lBTcuvete;
        //protected IList<LBSamplingItem> _lBSamplingItemList; 
        //protected long? _superGroupID;


        #endregion

        #region Constructors

        public LBSamplingGroup() { }

        public LBSamplingGroup(string cName, long sampleTypeID, string sName, string sCode, string synopsis, int printCount, string affixTubeFlag, string prepInfo, int virtualNo, bool isUse, int dispOrder, long labID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, LBTcuvete lBTcuvete, long? specialtyID, long? destinationID)
        {
            this._cName = cName;
            this._sampleTypeID = sampleTypeID;
            this._sName = sName;
            this._sCode = sCode;
            this._synopsis = synopsis;
            this._printCount = printCount;
            this._affixTubeFlag = affixTubeFlag;
            this._prepInfo = prepInfo;
            this._virtualNo = virtualNo;
            this._isUse = isUse;
            this._dispOrder = dispOrder;
            this._labID = labID;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._lBTcuvete = lBTcuvete;
            this._specialtyID = specialtyID;
            this._destinationID = destinationID;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "CName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string CName
        {
            get { return _cName; }
            set { _cName = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "SampleTypeID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? SampleTypeID
        {
            get { return _sampleTypeID; }
            set { _sampleTypeID = value; }
        }

        /*[DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "SuperGroupID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? SuperGroupID
        {
            get { return _superGroupID; }
            set { _superGroupID = value; }
        }*/

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "SpecialtyID", Desc = "专业组ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? SpecialtyID
        {
            get { return _specialtyID; }
            set { _specialtyID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DestinationID", Desc = "送检目的地ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? DestinationID
        {
            get { return _destinationID; }
            set { _destinationID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string SName
        {
            get { return _sName; }
            set { _sName = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string SCode
        {
            get { return _sCode; }
            set { _sCode = value; }
        }

        /* [DataMember]
         [DataDesc(CName = "", ShortCode = "Destination", Desc = "", ContextType = SysDic.All, Length = 50)]
         public virtual string Destination
         {
             get { return _destination; }
             set { _destination = value; }
         }*/

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Synopsis", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Synopsis
        {
            get { return _synopsis; }
            set { _synopsis = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PrintCount", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int PrintCount
        {
            get { return _printCount; }
            set { _printCount = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AffixTubeFlag", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string AffixTubeFlag
        {
            get { return _affixTubeFlag; }
            set { _affixTubeFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PrepInfo", Desc = "", ContextType = SysDic.All, Length = 300)]
        public virtual string PrepInfo
        {
            get { return _prepInfo; }
            set { _prepInfo = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "VirtualNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int VirtualNo
        {
            get { return _virtualNo; }
            set { _virtualNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsUse", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
        {
            get { return _isUse; }
            set { _isUse = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LBTcuvete", Desc = "")]
        public virtual LBTcuvete LBTcuvete
        {
            get { return _lBTcuvete; }
            set { _lBTcuvete = value; }
        }

        //[DataMember]
        //[DataDesc(CName = "采样组项目", ShortCode = "LBSamplingItemList", Desc = "采样组项目")]
        //public virtual IList<LBSamplingItem> LBSamplingItemList
        //{
        //    get
        //    {
        //        if (_lBSamplingItemList == null)
        //        {
        //            _lBSamplingItemList = new List<LBSamplingItem>();
        //        }
        //        return _lBSamplingItemList;
        //    }
        //    set { _lBSamplingItemList = value; }
        //}


        #endregion
    }
    #endregion
}