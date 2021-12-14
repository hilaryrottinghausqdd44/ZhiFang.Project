using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.RBAC
{
    #region RBACPreconditions

    /// <summary>
    /// RBACPreconditions object for NHibernate mapped table 'RBAC_Preconditions'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "预置条件", ClassCName = "RBACPreconditions", ShortCode = "RBACPreconditions", Desc = "预置条件")]
    public class RBACPreconditions : BaseEntity
    {
        #region Member Variables
        protected string _cName;
        protected string _eName;
        protected string _serviceURLCName;
        protected bool _isUse;
        protected DateTime? _dataUpdateTime;
        protected string _valueType;
        protected int _dispOrder;
        protected string _entityCName;
        protected string _entityCode;
        protected RBACModuleOper _rBACModuleOper;
        #endregion

        #region Constructors

        public RBACPreconditions() { }

        public RBACPreconditions(long labID, string cName, string eName, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._cName = cName;
            this._eName = eName;
            this._isUse = isUse;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties
        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }
        [DataMember]
        [DataDesc(CName = "值类型", ShortCode = "ValueType", Desc = "值类型", ContextType = SysDic.All, Length = 200)]
        public virtual string ValueType
        {
            get { return _valueType; }
            set
            {
                _valueType = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "预置条件显示名称", ShortCode = "CName", Desc = "预置条件显示名称", ContextType = SysDic.All, Length = 200)]
        public virtual string CName
        {
            get { return _cName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
                _cName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "预置条件名称", ShortCode = "EName", Desc = "预置条件名称", ContextType = SysDic.All, Length = 200)]
        public virtual string EName
        {
            get { return _eName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for EName", value, value.ToString());
                _eName = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
        {
            get { return _isUse; }
            set { _isUse = value; }
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
        [DataDesc(CName = "实体对象名称", ShortCode = "EntityCName", Desc = "实体对象名称", ContextType = SysDic.All, Length = 60)]
        public virtual string EntityCName
        {
            get { return _entityCName; }
            set
            {
                _entityCName = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "实体编码", ShortCode = "EntityCode", Desc = "实体编码", ContextType = SysDic.All, Length = 200)]
        public virtual string EntityCode
        {
            get { return _entityCode; }
            set
            {
                _entityCode = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "模块操作", ShortCode = "RBACModuleOper", Desc = "模块操作")]
        public virtual RBACModuleOper RBACModuleOper
        {
            get { return _rBACModuleOper; }
            set { _rBACModuleOper = value; }
        }

        #endregion
    }
    #endregion
}