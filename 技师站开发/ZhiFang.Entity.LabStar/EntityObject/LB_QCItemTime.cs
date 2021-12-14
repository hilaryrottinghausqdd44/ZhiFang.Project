using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBQCItemTime

    /// <summary>
    /// LBQCItemTime object for NHibernate mapped table 'LB_QCItemTime'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "LBQCItemTime", ShortCode = "LBQCItemTime", Desc = "")]
    public class LBQCItemTime : BaseEntity
    {
        #region Member Variables

        protected DateTime _startDate;
        protected DateTime? _endDate;
        protected double _target;
        protected double _sD;
        protected double _cCV;
        protected double _hValue;
        protected double _lValue;
        protected double _hHValue;
        protected double _lLValue;
        protected string _descAll;
        protected int _specialType;
        protected int _iIndex;
        protected string _manuQCRange;
        protected string _manuQCInfo;
        protected string _qCItemTimeDesc;
        protected string _comment;
        protected string _diagMethod;
        protected string _unit;
        protected string _testDesc;
        protected string _qCWithInfo;
        protected double _qCWithRange;
        protected long? _userID;
        protected DateTime? _dataUpdateTime;
        protected LBQCItem _lBQCItem;
        protected LBQCMatTime _lBQCMatTime;


        #endregion

        #region Constructors

        public LBQCItemTime() { }

        public LBQCItemTime(long labID, DateTime startDate, DateTime endDate, double target, double sD, double cCV, double hValue, double lValue, double hHValue, double lLValue, string descAll, int specialType, int iIndex, string manuQCRange, string manuQCInfo, string qCItemTimeDesc, string comment, string diagMethod, string unit, string testDesc, string qCWithInfo, double qCWithRange, long userID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, LBQCItem lBQCItem, LBQCMatTime lBQCMatTime)
        {
            this._labID = labID;
            this._startDate = startDate;
            this._endDate = endDate;
            this._target = target;
            this._sD = sD;
            this._cCV = cCV;
            this._hValue = hValue;
            this._lValue = lValue;
            this._hHValue = hHValue;
            this._lLValue = lLValue;
            this._descAll = descAll;
            this._specialType = specialType;
            this._iIndex = iIndex;
            this._manuQCRange = manuQCRange;
            this._manuQCInfo = manuQCInfo;
            this._qCItemTimeDesc = qCItemTimeDesc;
            this._comment = comment;
            this._diagMethod = diagMethod;
            this._unit = unit;
            this._testDesc = testDesc;
            this._qCWithInfo = qCWithInfo;
            this._qCWithRange = qCWithRange;
            this._userID = userID;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._lBQCItem = lBQCItem;
            this._lBQCMatTime = lBQCMatTime;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "开始日期", ShortCode = "StartDate", Desc = "开始日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
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
        [DataDesc(CName = "靶值", ShortCode = "Target", Desc = "靶值", ContextType = SysDic.All, Length = 8)]
        public virtual double Target
        {
            get { return _target; }
            set { _target = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "标准差", ShortCode = "SD", Desc = "标准差", ContextType = SysDic.All, Length = 8)]
        public virtual double SD
        {
            get { return _sD; }
            set { _sD = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "CCV", ShortCode = "CCV", Desc = "CCV", ContextType = SysDic.All, Length = 8)]
        public virtual double CCV
        {
            get { return _cCV; }
            set { _cCV = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "警告高", ShortCode = "HValue", Desc = "警告高", ContextType = SysDic.All, Length = 8)]
        public virtual double HValue
        {
            get { return _hValue; }
            set { _hValue = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "警告低", ShortCode = "LValue", Desc = "警告低", ContextType = SysDic.All, Length = 8)]
        public virtual double LValue
        {
            get { return _lValue; }
            set { _lValue = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "失控高", ShortCode = "HHValue", Desc = "失控高", ContextType = SysDic.All, Length = 8)]
        public virtual double HHValue
        {
            get { return _hHValue; }
            set { _hHValue = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "失控低", ShortCode = "LLValue", Desc = "失控低", ContextType = SysDic.All, Length = 8)]
        public virtual double LLValue
        {
            get { return _lLValue; }
            set { _lLValue = value; }
        }

        [DataMember]
        [DataDesc(CName = "定性描述", ShortCode = "DescAll", Desc = "定性描述", ContextType = SysDic.All, Length = 500)]
        public virtual string DescAll
        {
            get { return _descAll; }
            set { _descAll = value; }
        }

        [DataMember]
        [DataDesc(CName = "特殊处理", ShortCode = "SpecialType", Desc = "特殊处理", ContextType = SysDic.All, Length = 4)]
        public virtual int SpecialType
        {
            get { return _specialType; }
            set { _specialType = value; }
        }

        [DataMember]
        [DataDesc(CName = "顺序", ShortCode = "IIndex", Desc = "顺序", ContextType = SysDic.All, Length = 4)]
        public virtual int IIndex
        {
            get { return _iIndex; }
            set { _iIndex = value; }
        }

        [DataMember]
        [DataDesc(CName = "厂家质控范围描述", ShortCode = "ManuQCRange", Desc = "厂家质控范围描述", ContextType = SysDic.All, Length = 200)]
        public virtual string ManuQCRange
        {
            get { return _manuQCRange; }
            set { _manuQCRange = value; }
        }

        [DataMember]
        [DataDesc(CName = "厂家质控项目信息", ShortCode = "ManuQCInfo", Desc = "厂家质控项目信息", ContextType = SysDic.All, Length = 200)]
        public virtual string ManuQCInfo
        {
            get { return _manuQCInfo; }
            set { _manuQCInfo = value; }
        }

        [DataMember]
        [DataDesc(CName = "质控项目时效说明", ShortCode = "QCItemTimeDesc", Desc = "质控项目时效说明", ContextType = SysDic.All, Length = 200)]
        public virtual string QCItemTimeDesc
        {
            get { return _qCItemTimeDesc; }
            set { _qCItemTimeDesc = value; }
        }

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Comment", Desc = "备注", ContextType = SysDic.All, Length = 500)]
        public virtual string Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }

        [DataMember]
        [DataDesc(CName = "检验方法", ShortCode = "DiagMethod", Desc = "检验方法", ContextType = SysDic.All, Length = 50)]
        public virtual string DiagMethod
        {
            get { return _diagMethod; }
            set { _diagMethod = value; }
        }

        [DataMember]
        [DataDesc(CName = "单位", ShortCode = "Unit", Desc = "单位", ContextType = SysDic.All, Length = 50)]
        public virtual string Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }

        [DataMember]
        [DataDesc(CName = "检验说明", ShortCode = "TestDesc", Desc = "检验说明", ContextType = SysDic.All, Length = 50)]
        public virtual string TestDesc
        {
            get { return _testDesc; }
            set { _testDesc = value; }
        }

        [DataMember]
        [DataDesc(CName = "联合质控类型", ShortCode = "QCWithInfo", Desc = "联合质控类型", ContextType = SysDic.All, Length = 10)]
        public virtual string QCWithInfo
        {
            get { return _qCWithInfo; }
            set { _qCWithInfo = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "靶值关联范围", ShortCode = "QCWithRange", Desc = "靶值关联范围", ContextType = SysDic.All, Length = 8)]
        public virtual double QCWithRange
        {
            get { return _qCWithRange; }
            set { _qCWithRange = value; }
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
        [DataDesc(CName = "", ShortCode = "LBQCItem", Desc = "")]
        public virtual LBQCItem LBQCItem
        {
            get { return _lBQCItem; }
            set { _lBQCItem = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LBQCMatTime", Desc = "")]
        public virtual LBQCMatTime LBQCMatTime
        {
            get { return _lBQCMatTime; }
            set { _lBQCMatTime = value; }
        }


        #endregion
    }
    #endregion
}