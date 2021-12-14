using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region BParaItem

    /// <summary>
    /// BParaItem object for NHibernate mapped table 'B_ParaItem'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "参数个性设置值", ClassCName = "BParaItem", ShortCode = "BParaItem", Desc = "参数个性设置值")]
    public class BParaItem : BaseEntity
    {
        #region Member Variables

        protected string _paraNo;
        protected long? _objectID;
        protected string _objectName;
        protected string _paraValue;
        protected int _dispOrder;
        protected long? _operaterID;
        protected string _operater;
        protected long? _finalOperaterID;
        protected string _finalOperater;
        protected bool _isUse;
        protected DateTime? _dataUpdateTime;
        protected BPara _bPara;


        #endregion

        #region Constructors

        public BParaItem() { }

        public BParaItem(long labID, string paraNo, long objectID, string objectName, string paraValue, int dispOrder, long operaterID, string operater, long finalOperaterID, string finalOperater, bool isUse, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BPara bPara)
        {
            this._labID = labID;
            this._paraNo = paraNo;
            this._objectID = objectID;
            this._objectName = objectName;
            this._paraValue = paraValue;
            this._dispOrder = dispOrder;
            this._operaterID = operaterID;
            this._operater = operater;
            this._finalOperaterID = finalOperaterID;
            this._finalOperater = finalOperater;
            this._isUse = isUse;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._bPara = bPara;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "参数编号", ShortCode = "ParaNo", Desc = "参数编号", ContextType = SysDic.All, Length = 100)]
        public virtual string ParaNo
        {
            get { return _paraNo; }
            set { _paraNo = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "对应的对象ID", ShortCode = "ObjectID", Desc = "对应的对象ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ObjectID
        {
            get { return _objectID; }
            set { _objectID = value; }
        }

        [DataMember]
        [DataDesc(CName = "对应的对象名称", ShortCode = "ObjectName", Desc = "对应的对象名称", ContextType = SysDic.All, Length = 200)]
        public virtual string ObjectName
        {
            get { return _objectName; }
            set { _objectName = value; }
        }

        [DataMember]
        [DataDesc(CName = "参数值", ShortCode = "ParaValue", Desc = "参数值", ContextType = SysDic.All, Length = -1)]
        public virtual string ParaValue
        {
            get { return _paraValue; }
            set { _paraValue = value; }
        }

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "操作者ID", ShortCode = "OperaterID", Desc = "操作者ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? OperaterID
        {
            get { return _operaterID; }
            set { _operaterID = value; }
        }

        [DataMember]
        [DataDesc(CName = "操作者姓名", ShortCode = "Operater", Desc = "操作者姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string Operater
        {
            get { return _operater; }
            set { _operater = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "最终操作者ID", ShortCode = "FinalOperaterID", Desc = "最终操作者ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? FinalOperaterID
        {
            get { return _finalOperaterID; }
            set { _finalOperaterID = value; }
        }

        [DataMember]
        [DataDesc(CName = "最终操作者", ShortCode = "FinalOperater", Desc = "最终操作者", ContextType = SysDic.All, Length = 50)]
        public virtual string FinalOperater
        {
            get { return _finalOperater; }
            set { _finalOperater = value; }
        }

        [DataMember]
        [DataDesc(CName = "IsUse", ShortCode = "IsUse", Desc = "IsUse", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
        {
            get { return _isUse; }
            set { _isUse = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "DataUpdateTime", ShortCode = "DataUpdateTime", Desc = "DataUpdateTime", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "系统参数表", ShortCode = "BPara", Desc = "系统参数表")]
        public virtual BPara BPara
        {
            get { return _bPara; }
            set { _bPara = value; }
        }


        #endregion
    }
    #endregion
}