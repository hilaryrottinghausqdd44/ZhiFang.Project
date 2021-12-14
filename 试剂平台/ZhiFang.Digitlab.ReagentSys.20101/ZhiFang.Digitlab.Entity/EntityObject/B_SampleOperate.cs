using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
    #region BSampleOperate

    /// <summary>
    /// BSampleOperate object for NHibernate mapped table 'B_SampleOperate'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "样本操作", ClassCName = "BSampleOperate", ShortCode = "BSampleOperate", Desc = "样本操作")]
    public class BSampleOperate : BaseEntity
    {
        #region Member Variables

        protected long? _operateObjectID;
        protected long? _operaterID;
        protected string _operater;
        protected DateTime? _operateTime;
        protected string _operateHost;
        protected string _operateMemo;
        protected string _zdy1;
        protected string _zdy2;
        protected string _zdy3;
        protected string _zdy4;
        protected string _zdy5;
        protected BOperateObjectType _bOperateObjectType;
        protected BSampleOperateType _operateType;
        protected int[] _bSampleList;

        #endregion

        #region Constructors

        public BSampleOperate() { }

        public BSampleOperate(long labID, long operateObjectID, long operaterID, string operater, DateTime operateTime, string operateHost, string operateMemo, string zdy1, string zdy2, string zdy3, string zdy4, string zdy5, DateTime dataAddTime, byte[] dataTimeStamp, BOperateObjectType bOperateObjectType, BSampleOperateType operateType)
        {
            this._labID = labID;
            this._operateObjectID = operateObjectID;
            this._operaterID = operaterID;
            this._operater = operater;
            this._operateTime = operateTime;
            this._operateHost = operateHost;
            this._operateMemo = operateMemo;
            this._zdy1 = zdy1;
            this._zdy2 = zdy2;
            this._zdy3 = zdy3;
            this._zdy4 = zdy4;
            this._zdy5 = zdy5;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
            this._bOperateObjectType = bOperateObjectType;
            this._operateType = operateType;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "操作对象ID", ShortCode = "OperateObjectID", Desc = "操作对象ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? OperateObjectID
        {
            get { return _operateObjectID; }
            set { _operateObjectID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "操作人ID", ShortCode = "OperaterID", Desc = "操作人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? OperaterID
        {
            get { return _operaterID; }
            set { _operaterID = value; }
        }

        [DataMember]
        [DataDesc(CName = "操作人", ShortCode = "Operater", Desc = "操作人", ContextType = SysDic.All, Length = 30)]
        public virtual string Operater
        {
            get { return _operater; }
            set
            {
                if (value != null && value.Length > 30)
                    throw new ArgumentOutOfRangeException("Invalid value for Operater", value, value.ToString());
                _operater = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "操作时间", ShortCode = "OperateTime", Desc = "操作时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? OperateTime
        {
            get { return _operateTime; }
            set { _operateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "操作计算机", ShortCode = "OperateHost", Desc = "操作计算机", ContextType = SysDic.All, Length = 50)]
        public virtual string OperateHost
        {
            get { return _operateHost; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for OperateHost", value, value.ToString());
                _operateHost = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "操作说明", ShortCode = "OperateMemo", Desc = "操作说明", ContextType = SysDic.All, Length = 8000)]
        public virtual string OperateMemo
        {
            get { return _operateMemo; }
            set
            {
                _operateMemo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "备注1", ShortCode = "Zdy1", Desc = "备注1", ContextType = SysDic.All, Length = 200)]
        public virtual string Zdy1
        {
            get { return _zdy1; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for Zdy1", value, value.ToString());
                _zdy1 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "备注2", ShortCode = "Zdy2", Desc = "备注2", ContextType = SysDic.All, Length = 200)]
        public virtual string Zdy2
        {
            get { return _zdy2; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for Zdy2", value, value.ToString());
                _zdy2 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "备注3", ShortCode = "Zdy3", Desc = "备注3", ContextType = SysDic.All, Length = 200)]
        public virtual string Zdy3
        {
            get { return _zdy3; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for Zdy3", value, value.ToString());
                _zdy3 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "备注4", ShortCode = "Zdy4", Desc = "备注4", ContextType = SysDic.All, Length = 200)]
        public virtual string Zdy4
        {
            get { return _zdy4; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for Zdy4", value, value.ToString());
                _zdy4 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "备注5", ShortCode = "Zdy5", Desc = "备注5", ContextType = SysDic.All, Length = 200)]
        public virtual string Zdy5
        {
            get { return _zdy5; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for Zdy5", value, value.ToString());
                _zdy5 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "操作对象类型", ShortCode = "BOperateObjectType", Desc = "操作对象类型")]
        public virtual BOperateObjectType BOperateObjectType
        {
            get { return _bOperateObjectType; }
            set { _bOperateObjectType = value; }
        }

        [DataMember]
        [DataDesc(CName = "样本操作类型", ShortCode = "OperateType", Desc = "样本操作类型")]
        public virtual BSampleOperateType OperateType
        {
            get { return _operateType; }
            set { _operateType = value; }
        }

        public virtual int[] BSampleList
        {
            get { return _bSampleList; }
            set { _bSampleList = value; }
        }

        #endregion
    }
    #endregion
}