using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
    #region ReaMonthUsageStatisticsDoc

    /// <summary>
    /// ReaMonthUsageStatisticsDoc object for NHibernate mapped table 'Rea_MonthUsageStatisticsDoc'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "ReaMonthUsageStatisticsDoc", ShortCode = "ReaMonthUsageStatisticsDoc", Desc = "")]
    public class ReaMonthUsageStatisticsDoc : BaseEntity
    {
        #region Member Variables

        protected string _docNo;
        protected long _roundTypeId;
        protected string _roundTypeName;
        protected string _round;
        protected DateTime? _startDate;
        protected DateTime? _endDate;
        protected long? _deptID;
        protected string _deptName;
        protected long _typeID;
        protected string _typeName;
        protected string _zX1;
        protected string _zX2;
        protected string _zX3;
        protected int _dispOrder;
        protected string _memo;
        protected bool _visible;
        protected long _createrID;
        protected string _createrName;
        protected DateTime _dataUpdateTime;

        #endregion

        #region Constructors

        public ReaMonthUsageStatisticsDoc() { }

        public ReaMonthUsageStatisticsDoc(long labID, string docNo, string round, DateTime startDate, DateTime endDate, long deptID, string deptName, long typeID, string typeName, string zX1, string zX2, string zX3, int dispOrder, string memo, bool visible, long createrID, string createrName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._docNo = docNo;
            this._round = round;
            this._startDate = startDate;
            this._endDate = endDate;
            this._deptID = deptID;
            this._deptName = deptName;
            this._typeID = typeID;
            this._typeName = typeName;
            this._zX1 = zX1;
            this._zX2 = zX2;
            this._zX3 = zX3;
            this._dispOrder = dispOrder;
            this._memo = memo;
            this._visible = visible;
            this._createrID = createrID;
            this._createrName = createrName;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "统计类型Id", ShortCode = "TypeID", Desc = "统计类型Id", ContextType = SysDic.All, Length = 8)]
        public virtual long TypeID
        {
            get { return _typeID; }
            set { _typeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "统计类型名称", ShortCode = "TypeName", Desc = "统计类型名称", ContextType = SysDic.All, Length = 50)]
        public virtual string TypeName
        {
            get { return _typeName; }
            set
            {
                _typeName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "统计周期类型Id", ShortCode = "RoundTypeId", Desc = "统计周期类型Id", ContextType = SysDic.All, Length = 8)]
        public virtual long RoundTypeId
        {
            get { return _roundTypeId; }
            set { _roundTypeId = value; }
        }

        [DataMember]
        [DataDesc(CName = "统计周期类型", ShortCode = "RoundTypeName", Desc = "统计周期类型", ContextType = SysDic.All, Length = 50)]
        public virtual string RoundTypeName
        {
            get { return _roundTypeName; }
            set
            {
                _roundTypeName = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "DocNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string DocNo
        {
            get { return _docNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for DocNo", value, value.ToString());
                _docNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Round", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Round
        {
            get { return _round; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Round", value, value.ToString());
                _round = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "StartDate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "EndDate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DeptID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? DeptID
        {
            get { return _deptID; }
            set { _deptID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DeptName", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string DeptName
        {
            get { return _deptName; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for DeptName", value, value.ToString());
                _deptName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZX1", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX1
        {
            get { return _zX1; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ZX1", value, value.ToString());
                _zX1 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZX2", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX2
        {
            get { return _zX2; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ZX2", value, value.ToString());
                _zX2 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZX3", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX3
        {
            get { return _zX3; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ZX3", value, value.ToString());
                _zX3 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Memo", Desc = "", ContextType = SysDic.All, Length = int.MaxValue)]
        public virtual string Memo
        {
            get { return _memo; }
            set
            {
                _memo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Visible", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CreaterID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long CreaterID
        {
            get { return _createrID; }
            set { _createrID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CreaterName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string CreaterName
        {
            get { return _createrName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for CreaterName", value, value.ToString());
                _createrName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }


        #endregion
    }
    #endregion
}