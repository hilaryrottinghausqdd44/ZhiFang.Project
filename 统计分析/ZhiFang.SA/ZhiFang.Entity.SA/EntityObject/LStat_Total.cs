using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.SA
{
    #region LStatTotal

    /// <summary>
    /// LStatTotal object for NHibernate mapped table 'LStat_Total'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "LStatTotal", ShortCode = "LStatTotal", Desc = "")]
    public class LStatTotal : BaseEntity
    {
        #region Member Variables

        protected long _classificationId;
        protected string _classificationName;
        protected long _statType;
        protected string _statName;
        protected string _statDName;
        protected long _statDValue;
        protected string _statDValueDesc;
        protected long _statDValue2;
        protected string _statDValueDesc2;
        protected long _statDateType;
        protected DateTime? _statDateBegin;
        protected DateTime? _statDateEnd;
        protected string _statKey;
        protected string _statValue;
        protected DateTime? _dataUpdateTime;

        #endregion

        #region Constructors

        public LStatTotal() { }

        public LStatTotal(long classificationId, string classificationName, long statType, string statName, string statDName, long statDValue, string statDValueDesc, long statDValue2, string statDValueDesc2, long statDateType, DateTime statDateBegin, DateTime statDateEnd, string statKey, string statValue, long labID, DateTime dataAddTime, DateTime dataUpdateTime)
        {
            this._labID = labID;
            this._classificationId = classificationId;
            this._classificationName = classificationName;

            this._statType = statType;
            this._statName = statName;
            this._statDName = statDName;
            this._statDValue = statDValue;
            this._statDValueDesc = statDValueDesc;
            this._statDValue2 = statDValue2;
            this._statDValueDesc2 = statDValueDesc2;
            this._statDateType = statDateType;
            this._statDateBegin = statDateBegin;
            this._statDateEnd = statDateEnd;

            this._statKey = statKey;
            this._statValue = statValue;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
        }

        #endregion

        #region Public Properties
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "ͳ�ƽ������Id", ShortCode = "ClassificationId", Desc = "ͳ�ƽ������Id", ContextType = SysDic.All, Length = 8)]
        public virtual long ClassificationId
        {
            get { return _classificationId; }
            set { _classificationId = value; }
        }

        [DataMember]
        [DataDesc(CName = "ͳ�ƽ����������", ShortCode = "ClassificationName", Desc = "ͳ�ƽ����������", ContextType = SysDic.All, Length = 50)]
        public virtual string ClassificationName
        {
            get { return _classificationName; }
            set
            {
                _classificationName = value;
            }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "ͳ��ָ��(����)Id", ShortCode = "StatType", Desc = "ͳ��ָ��(����)Id", ContextType = SysDic.All, Length = 8)]
        public virtual long StatType
        {
            get { return _statType; }
            set { _statType = value; }
        }

        [DataMember]
        [DataDesc(CName = "ͳ��ָ��(����)����", ShortCode = "StatName", Desc = "ͳ��ָ��(����)����", ContextType = SysDic.All, Length = 50)]
        public virtual string StatName
        {
            get { return _statName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for StatName", value, value.ToString());
                _statName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "ͳ��γ��", ShortCode = "StatDName", Desc = "ͳ��γ��", ContextType = SysDic.All, Length = 50)]
        public virtual string StatDName
        {
            get { return _statDName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for StatDName", value, value.ToString());
                _statDName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "ͳ��γ��ֵ", ShortCode = "StatDValue", Desc = "ͳ��γ��ֵ", ContextType = SysDic.All, Length = 8)]
        public virtual long StatDValue
        {
            get { return _statDValue; }
            set { _statDValue = value; }
        }

        [DataMember]
        [DataDesc(CName = "ͳ��γ��ֵ����", ShortCode = "StatDValueDesc", Desc = "ͳ��γ��ֵ����", ContextType = SysDic.All, Length = 50)]
        public virtual string StatDValueDesc
        {
            get { return _statDValueDesc; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for StatDValueDesc", value, value.ToString());
                _statDValueDesc = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "ͳ��γ��ֵ2", ShortCode = "StatDValue2", Desc = "ͳ��γ��ֵ2", ContextType = SysDic.All, Length = 8)]
        public virtual long StatDValue2
        {
            get { return _statDValue2; }
            set { _statDValue2 = value; }
        }

        [DataMember]
        [DataDesc(CName = "ͳ��γ��ֵ����2", ShortCode = "StatDValueDesc2", Desc = "ͳ��γ��ֵ����2", ContextType = SysDic.All, Length = 50)]
        public virtual string StatDValueDesc2
        {
            get { return _statDValueDesc2; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for StatDValueDesc2", value, value.ToString());
                _statDValueDesc2 = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "ͳ����������", ShortCode = "StatDateType", Desc = "ͳ����������", ContextType = SysDic.All, Length = 4)]
        public virtual long StatDateType
        {
            get { return _statDateType; }
            set { _statDateType = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "ͳ�ƿ�ʼ����", ShortCode = "StatDateBegin", Desc = "ͳ�ƿ�ʼ����", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? StatDateBegin
        {
            get { return _statDateBegin; }
            set { _statDateBegin = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "ͳ�ƽ�������", ShortCode = "StatDateEnd", Desc = "ͳ�ƽ�������", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? StatDateEnd
        {
            get { return _statDateEnd; }
            set { _statDateEnd = value; }
        }

        [DataMember]
        [DataDesc(CName = "��ͳ������ֵ", ShortCode = "StatKey", Desc = "��ͳ������ֵ", ContextType = SysDic.All, Length = 500)]
        public virtual string StatKey
        {
            get { return _statKey; }
            set
            {
                _statKey = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "ͳ�ƽ����Ϣ(JSON�ַ���)", ShortCode = "StatValue", Desc = "ͳ�ƽ����Ϣ(JSON�ַ���)", ContextType = SysDic.All, Length = Int32.MaxValue)]
        public virtual string StatValue
        {
            get { return _statValue; }
            set
            {
                _statValue = value;
            }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }


        #endregion
    }
    #endregion
}