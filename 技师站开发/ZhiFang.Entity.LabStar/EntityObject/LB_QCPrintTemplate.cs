using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBQCPrintTemplate

    /// <summary>
    /// LBQCPrintTemplate object for NHibernate mapped table 'LB_QCPrintTemplate'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "质控打印模板表", ClassCName = "LBQCPrintTemplate", ShortCode = "LBQCPrintTemplate", Desc = "质控打印模板表")]
    public class LBQCPrintTemplate : BaseEntity
    {
        #region Member Variables

        protected string _typeName;
        protected string _printTemplateName;
        protected long? _equipID;
        protected string _equipModule;
        protected int? _qCDataType;
        protected int _qCCountInDay;
        protected int _levelCount;
        protected long? _itemID;
        protected long? _qCMatID;
        protected int _dispOrder;
        protected long? _userID;
        protected DateTime? _dataUpdateTime;


        #endregion

        #region Constructors

        public LBQCPrintTemplate() { }

        public LBQCPrintTemplate(long labID, string typeName, string printTemplateName, long equipID, string equipModule, int? qCDataType, int qCCountInDay, int levelCount, long itemID, long qCMatID, int dispOrder, long userID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._typeName = typeName;
            this._printTemplateName = printTemplateName;
            this._equipID = equipID;
            this._equipModule = equipModule;
            this._qCDataType = qCDataType;
            this._qCCountInDay = qCCountInDay;
            this._levelCount = levelCount;
            this._itemID = itemID;
            this._qCMatID = qCMatID;
            this._dispOrder = dispOrder;
            this._userID = userID;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "日质控 仪器日质控 月质控 多浓度质控", ShortCode = "TypeName", Desc = "日质控 仪器日质控 月质控 多浓度质控", ContextType = SysDic.All, Length = 50)]
        public virtual string TypeName
        {
            get { return _typeName; }
            set { _typeName = value; }
        }

        [DataMember]
        [DataDesc(CName = "日质控* 仪器日质控* 月质控* 多浓度质控*", ShortCode = "PrintTemplateName", Desc = "日质控* 仪器日质控* 月质控* 多浓度质控*", ContextType = SysDic.All, Length = 50)]
        public virtual string PrintTemplateName
        {
            get { return _printTemplateName; }
            set { _printTemplateName = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "仪器ID", ShortCode = "EquipID", Desc = "仪器ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? EquipID
        {
            get { return _equipID; }
            set { _equipID = value; }
        }

        [DataMember]
        [DataDesc(CName = "仪器模块", ShortCode = "EquipModule", Desc = "仪器模块", ContextType = SysDic.All, Length = 50)]
        public virtual string EquipModule
        {
            get { return _equipModule; }
            set { _equipModule = value; }
        }

        [DataMember]
        [DataDesc(CName = "0：靶值标准差， 1：定性 2：值范围", ShortCode = "QCDataType", Desc = "0：靶值标准差， 1：定性 2：值范围", ContextType = SysDic.All, Length = 4)]
        public virtual int? QCDataType
        {
            get { return _qCDataType; }
            set { _qCDataType = value; }
        }

        [DataMember]
        [DataDesc(CName = "不用， ", ShortCode = "QCCountInDay", Desc = "不用， ", ContextType = SysDic.All, Length = 4)]
        public virtual int QCCountInDay
        {
            get { return _qCCountInDay; }
            set { _qCCountInDay = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LevelCount", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int LevelCount
        {
            get { return _levelCount; }
            set { _levelCount = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "项目ID", ShortCode = "ItemID", Desc = "项目ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ItemID
        {
            get { return _itemID; }
            set { _itemID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "不用，  质控物ID", ShortCode = "QCMatID", Desc = "不用，  质控物ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? QCMatID
        {
            get { return _qCMatID; }
            set { _qCMatID = value; }
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


        #endregion
    }
    #endregion
}