using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.RBAC
{
    #region BTDAppComponentsRef

    /// <summary>
    /// BTDAppComponentsRef object for NHibernate mapped table 'BT_D_AppComponentsRef'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "应用组件引用关系", ClassCName = "BTDAppComponentsRef", ShortCode = "BTDAppComponentsRef", Desc = "应用组件引用关系")]
    public class BTDAppComponentsRef : BaseEntityService
    {
        #region Member Variables

        protected long? _refAppComID;
        protected string _refAppComIncID;
        protected string _creator;
        protected string _modifier;
        protected DateTime? _dataUpdateTime;
        protected BTDAppComponents _bTDAppComponents;

        #endregion

        #region Constructors

        public BTDAppComponentsRef() { }

        public BTDAppComponentsRef(long labID, long refAppComID, string refAppComIncID, string creator, string modifier, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BTDAppComponents bTDAppComponents)
        {
            this._labID = labID;
            this._refAppComID = refAppComID;
            this._refAppComIncID = refAppComIncID;
            this._creator = creator;
            this._modifier = modifier;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._bTDAppComponents = bTDAppComponents;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "引用组件ID", ShortCode = "RefAppComID", Desc = "引用组件ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? RefAppComID
        {
            get { return _refAppComID; }
            set { _refAppComID = value; }
        }

        [DataMember]
        [DataDesc(CName = "引用组件内部ID", ShortCode = "RefAppComIncID", Desc = "引用组件内部ID", ContextType = SysDic.All, Length = 100)]
        public virtual string RefAppComIncID
        {
            get { return _refAppComIncID; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for RefAppComIncID", value, value.ToString());
                _refAppComIncID = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "创建者", ShortCode = "Creator", Desc = "创建者", ContextType = SysDic.All, Length = 20)]
        public virtual string Creator
        {
            get { return _creator; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Creator", value, value.ToString());
                _creator = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "修改者", ShortCode = "Modifier", Desc = "修改者", ContextType = SysDic.All, Length = 20)]
        public virtual string Modifier
        {
            get { return _modifier; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Modifier", value, value.ToString());
                _modifier = value;
            }
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
        [DataDesc(CName = "应用组件", ShortCode = "BTDAppComponents", Desc = "应用组件")]
        public virtual BTDAppComponents BTDAppComponents
        {
            get { return _bTDAppComponents; }
            set { _bTDAppComponents = value; }
        }


        #endregion
    }
    #endregion
}