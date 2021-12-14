using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LisOperate

    /// <summary>
    /// LisOperate object for NHibernate mapped table 'Lis_Operate'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "样本操作记录", ClassCName = "LisOperate", ShortCode = "LisOperate", Desc = "样本操作记录")]
    public class LisOperate : BaseEntity
    {
        #region Member Variables

        protected DateTime? _partitionDate;
        protected string _operateObject;
        protected long? _operateObjectID;
        protected long? _operateFormID;
        protected string _operateType;
        protected long? _operateTypeID;
        protected string _operateMemoAuto;
        protected string _dataInfo;
        protected string _operateMemo;
        protected long? _operateUserID;
        protected string _operateUser;
        protected string _operateHost;
        protected string _operateAddress;
        protected string _operateHostType;
        protected long? _relationUserID;
        protected string _relationUser;
        protected long? _operateDeptID;
        protected string _operateDept;
        protected string _barCode;
        protected string _operateName;
        protected DateTime? _tranceTime;
        protected bool _isTrance;
        protected int _iOFlag;
        protected DateTime? _iOTime;
        protected long? _iOUserID;
        protected string _iOUserName;
        protected DateTime? _dataUpdateTime;


        #endregion

        #region Constructors

        public LisOperate() { }

        public LisOperate(long labID, DateTime partitionDate, string operateObject, long operateObjectID, long operateFormID, string operateType, long operateTypeID, string operateMemoAuto, string dataInfo, string operateMemo, long operateUserID, string operateUser, string operateHost, string operateAddress, string operateHostType, long relationUserID, string relationUser, long operateDeptID, string operateDept, string barCode, string operateName, DateTime tranceTime, bool isTrance, int iOFlag, DateTime iOTime, long iOUserID, string iOUserName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._partitionDate = partitionDate;
            this._operateObject = operateObject;
            this._operateObjectID = operateObjectID;
            this._operateFormID = operateFormID;
            this._operateType = operateType;
            this._operateTypeID = operateTypeID;
            this._operateMemoAuto = operateMemoAuto;
            this._dataInfo = dataInfo;
            this._operateMemo = operateMemo;
            this._operateUserID = operateUserID;
            this._operateUser = operateUser;
            this._operateHost = operateHost;
            this._operateAddress = operateAddress;
            this._operateHostType = operateHostType;
            this._relationUserID = relationUserID;
            this._relationUser = relationUser;
            this._operateDeptID = operateDeptID;
            this._operateDept = operateDept;
            this._barCode = barCode;
            this._operateName = operateName;
            this._tranceTime = tranceTime;
            this._isTrance = isTrance;
            this._iOFlag = iOFlag;
            this._iOTime = iOTime;
            this._iOUserID = iOUserID;
            this._iOUserName = iOUserName;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "分区日期", ShortCode = "PartitionDate", Desc = "分区日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? PartitionDate
        {
            get { return _partitionDate; }
            set { _partitionDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "业务表名称", ShortCode = "OperateObject", Desc = "业务表名称", ContextType = SysDic.All, Length = 100)]
        public virtual string OperateObject
        {
            get { return _operateObject; }
            set { _operateObject = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "关联业务表主键ID", ShortCode = "OperateObjectID", Desc = "关联业务表主键ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? OperateObjectID
        {
            get { return _operateObjectID; }
            set { _operateObjectID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "关联业务主表ID", ShortCode = "OperateFormID", Desc = "关联业务主表ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? OperateFormID
        {
            get { return _operateFormID; }
            set { _operateFormID = value; }
        }

        [DataMember]
        [DataDesc(CName = "操作类型", ShortCode = "OperateType", Desc = "操作类型", ContextType = SysDic.All, Length = 100)]
        public virtual string OperateType
        {
            get { return _operateType; }
            set { _operateType = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "操作类型ID", ShortCode = "OperateTypeID", Desc = "操作类型ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? OperateTypeID
        {
            get { return _operateTypeID; }
            set { _operateTypeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "操作内容自动记录", ShortCode = "OperateMemoAuto", Desc = "操作内容自动记录", ContextType = SysDic.All, Length = 16)]
        public virtual string OperateMemoAuto
        {
            get { return _operateMemoAuto; }
            set { _operateMemoAuto = value; }
        }

        [DataMember]
        [DataDesc(CName = "操作数据备份", ShortCode = "DataInfo", Desc = "操作数据备份", ContextType = SysDic.All, Length = 16)]
        public virtual string DataInfo
        {
            get { return _dataInfo; }
            set { _dataInfo = value; }
        }

        [DataMember]
        [DataDesc(CName = "操作内容", ShortCode = "OperateMemo", Desc = "操作内容", ContextType = SysDic.All, Length = 16)]
        public virtual string OperateMemo
        {
            get { return _operateMemo; }
            set { _operateMemo = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "操作人员ID", ShortCode = "OperateUserID", Desc = "操作人员ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? OperateUserID
        {
            get { return _operateUserID; }
            set { _operateUserID = value; }
        }

        [DataMember]
        [DataDesc(CName = "操作人员", ShortCode = "OperateUser", Desc = "操作人员", ContextType = SysDic.All, Length = 100)]
        public virtual string OperateUser
        {
            get { return _operateUser; }
            set { _operateUser = value; }
        }

        [DataMember]
        [DataDesc(CName = "操作站点", ShortCode = "OperateHost", Desc = "操作站点", ContextType = SysDic.All, Length = 100)]
        public virtual string OperateHost
        {
            get { return _operateHost; }
            set { _operateHost = value; }
        }

        [DataMember]
        [DataDesc(CName = "操作站点地址", ShortCode = "OperateAddress", Desc = "操作站点地址", ContextType = SysDic.All, Length = 100)]
        public virtual string OperateAddress
        {
            get { return _operateAddress; }
            set { _operateAddress = value; }
        }

        [DataMember]
        [DataDesc(CName = "操作站点类型", ShortCode = "OperateHostType", Desc = "操作站点类型", ContextType = SysDic.All, Length = 100)]
        public virtual string OperateHostType
        {
            get { return _operateHostType; }
            set { _operateHostType = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "业务相关人员ID", ShortCode = "RelationUserID", Desc = "业务相关人员ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? RelationUserID
        {
            get { return _relationUserID; }
            set { _relationUserID = value; }
        }

        [DataMember]
        [DataDesc(CName = "业务相关人员", ShortCode = "RelationUser", Desc = "业务相关人员", ContextType = SysDic.All, Length = 100)]
        public virtual string RelationUser
        {
            get { return _relationUser; }
            set { _relationUser = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "操作部门科室ID", ShortCode = "OperateDeptID", Desc = "操作部门科室ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? OperateDeptID
        {
            get { return _operateDeptID; }
            set { _operateDeptID = value; }
        }

        [DataMember]
        [DataDesc(CName = "操作部门科室", ShortCode = "OperateDept", Desc = "操作部门科室", ContextType = SysDic.All, Length = 100)]
        public virtual string OperateDept
        {
            get { return _operateDept; }
            set { _operateDept = value; }
        }

        [DataMember]
        [DataDesc(CName = "条码号", ShortCode = "BarCode", Desc = "条码号", ContextType = SysDic.All, Length = 100)]
        public virtual string BarCode
        {
            get { return _barCode; }
            set { _barCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "操作标识名称", ShortCode = "OperateName", Desc = "操作标识名称", ContextType = SysDic.All, Length = 100)]
        public virtual string OperateName
        {
            get { return _operateName; }
            set { _operateName = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "迁移时间", ShortCode = "TranceTime", Desc = "迁移时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? TranceTime
        {
            get { return _tranceTime; }
            set { _tranceTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "迁移标志", ShortCode = "IsTrance", Desc = "迁移标志", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsTrance
        {
            get { return _isTrance; }
            set { _isTrance = value; }
        }

        [DataMember]
        [DataDesc(CName = "数据发送标志", ShortCode = "IOFlag", Desc = "数据发送标志", ContextType = SysDic.All, Length = 4)]
        public virtual int IOFlag
        {
            get { return _iOFlag; }
            set { _iOFlag = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "送据发送时间", ShortCode = "IOTime", Desc = "送据发送时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? IOTime
        {
            get { return _iOTime; }
            set { _iOTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据发送人员ID", ShortCode = "IOUserID", Desc = "数据发送人员ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? IOUserID
        {
            get { return _iOUserID; }
            set { _iOUserID = value; }
        }

        [DataMember]
        [DataDesc(CName = "数据发送人员姓名", ShortCode = "IOUserName", Desc = "数据发送人员姓名", ContextType = SysDic.All, Length = 100)]
        public virtual string IOUserName
        {
            get { return _iOUserName; }
            set { _iOUserName = value; }
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