using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LisQCComments

    /// <summary>
    /// LisQCComments object for NHibernate mapped table 'Lis_QCComments'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "LisQCComments", ShortCode = "LisQCComments", Desc = "")]
    public class LisQCComments : BaseEntity
    {
        #region Member Variables

        protected string _typeName;
        protected long? _equipID;
        protected long? _itemID;
        protected DateTime? _beginDate;
        protected DateTime? _endDate;
        protected string _qCInfo;
        protected string _qCComment;
        protected string _operator;
        protected long? _operatorID;
        protected DateTime? _dataUpdateTime;
        protected LBQCItem _lBQCItem;
        protected LBQCMaterial _lBQCMaterial;


        #endregion

        #region Constructors

        public LisQCComments() { }

        public LisQCComments(long labID, string typeName, long equipID, long itemID, DateTime beginDate, DateTime endDate, string qCInfo, string qCComment, string _operator, long operatorID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, LBQCItem lBQCItem, LBQCMaterial lBQCMaterial)
        {
            this._labID = labID;
            this._typeName = typeName;
            this._equipID = equipID;
            this._itemID = itemID;
            this._beginDate = beginDate;
            this._endDate = endDate;
            this._qCInfo = qCInfo;
            this._qCComment = qCComment;
            this._operator = _operator;
            this._operatorID = operatorID;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._lBQCItem = lBQCItem;
            this._lBQCMaterial = lBQCMaterial;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "TypeName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string TypeName
        {
            get { return _typeName; }
            set { _typeName = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "EquipID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? EquipID
        {
            get { return _equipID; }
            set { _equipID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ItemID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? ItemID
        {
            get { return _itemID; }
            set { _itemID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BeginDate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? BeginDate
        {
            get { return _beginDate; }
            set { _beginDate = value; }
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
        [DataDesc(CName = "", ShortCode = "QCInfo", Desc = "", ContextType = SysDic.All, Length = 1000)]
        public virtual string QCInfo
        {
            get { return _qCInfo; }
            set { _qCInfo = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "QCComment", Desc = "", ContextType = SysDic.All, Length = 1000)]
        public virtual string QCComment
        {
            get { return _qCComment; }
            set { _qCComment = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Operator", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Operator
        {
            get { return _operator; }
            set { _operator = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OperatorID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? OperatorID
        {
            get { return _operatorID; }
            set { _operatorID = value; }
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
        [DataDesc(CName = "", ShortCode = "LBQCItem", Desc = "")]
        public virtual LBQCItem LBQCItem
        {
            get { return _lBQCItem; }
            set { _lBQCItem = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LBQCMaterial", Desc = "")]
        public virtual LBQCMaterial LBQCMaterial
        {
            get { return _lBQCMaterial; }
            set { _lBQCMaterial = value; }
        }


        #endregion
    }
    #endregion
}