using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
    #region BSoftWareUpdateHistory

    /// <summary>
    /// BSoftWareUpdateHistory object for NHibernate mapped table 'B_SoftWareUpdateHistory'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BSoftWareUpdateHistory", ShortCode = "BSoftWareUpdateHistory", Desc = "")]
    public class BSoftWareUpdateHistory : BaseEntity
    {
        #region Member Variables

        protected string _softWareCode;
        protected string _softWareName;
        protected string _softWareVersion;
        protected string _softWareNewVersion;
        protected int _updateType;
        protected long? _updateUserID;
        protected string _updateUser;
        protected string _updateMemo;
        protected DateTime? _updateTime;
        protected string _nodeName;
        protected string _nodeIP;
        protected string _comment;

        #endregion

        #region Constructors

        public BSoftWareUpdateHistory() { }

        public BSoftWareUpdateHistory(long labID, string softWareCode, string softWareName, string softWareVersion, string softWareNewVersion, int updateType, long updateUserID, string updateUser, string updateMemo, DateTime updateTime, string nodeName, string nodeIP, string comment, DateTime dataAddTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._softWareCode = softWareCode;
            this._softWareName = softWareName;
            this._softWareVersion = softWareVersion;
            this._softWareNewVersion = softWareNewVersion;
            this._updateType = updateType;
            this._updateUserID = updateUserID;
            this._updateUser = updateUser;
            this._updateMemo = updateMemo;
            this._updateTime = updateTime;
            this._nodeName = nodeName;
            this._nodeIP = nodeIP;
            this._comment = comment;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "SoftWareCode", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string SoftWareCode
        {
            get { return _softWareCode; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for SoftWareCode", value, value.ToString());
                _softWareCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SoftWareName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string SoftWareName
        {
            get { return _softWareName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for SoftWareName", value, value.ToString());
                _softWareName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SoftWareVersion", Desc = "", ContextType = SysDic.All, Length = 30)]
        public virtual string SoftWareVersion
        {
            get { return _softWareVersion; }
            set
            {
                if (value != null && value.Length > 30)
                    throw new ArgumentOutOfRangeException("Invalid value for SoftWareVersion", value, value.ToString());
                _softWareVersion = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SoftWareNewVersion", Desc = "", ContextType = SysDic.All, Length = 30)]
        public virtual string SoftWareNewVersion
        {
            get { return _softWareNewVersion; }
            set
            {
                if (value != null && value.Length > 30)
                    throw new ArgumentOutOfRangeException("Invalid value for SoftWareNewVersion", value, value.ToString());
                _softWareNewVersion = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UpdateType", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int UpdateType
        {
            get { return _updateType; }
            set { _updateType = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "UpdateUserID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? UpdateUserID
        {
            get { return _updateUserID; }
            set { _updateUserID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UpdateUser", Desc = "", ContextType = SysDic.All, Length = 30)]
        public virtual string UpdateUser
        {
            get { return _updateUser; }
            set
            {
                if (value != null && value.Length > 30)
                    throw new ArgumentOutOfRangeException("Invalid value for UpdateUser", value, value.ToString());
                _updateUser = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UpdateMemo", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string UpdateMemo
        {
            get { return _updateMemo; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for UpdateMemo", value, value.ToString());
                _updateMemo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "UpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? UpdateTime
        {
            get { return _updateTime; }
            set { _updateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "NodeName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string NodeName
        {
            get { return _nodeName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for NodeName", value, value.ToString());
                _nodeName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "NodeIP", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string NodeIP
        {
            get { return _nodeIP; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for NodeIP", value, value.ToString());
                _nodeIP = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Comment", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string Comment
        {
            get { return _comment; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for Comment", value, value.ToString());
                _comment = value;
            }
        }


        #endregion
    }
    #endregion
}