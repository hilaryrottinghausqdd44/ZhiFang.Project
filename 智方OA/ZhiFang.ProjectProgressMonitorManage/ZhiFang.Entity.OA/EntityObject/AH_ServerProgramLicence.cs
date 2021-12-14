using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.OA.ViewObject.Request;

namespace ZhiFang.Entity.OA
{
    #region AHServerProgramLicence

    /// <summary>
    /// AHServerProgramLicence object for NHibernate mapped table 'AH_ServerProgramLicence'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "服务器程序授权明细", ClassCName = "AHServerProgramLicence", ShortCode = "AHServerProgramLicence", Desc = "服务器程序授权明细")]
    public class AHServerProgramLicence : AHLicence
    {
        #region Member Variables

        protected long? _programID;
        protected string _programName;
        protected long? _sNo;
        protected long? _licenceTypeId;
        protected string _sQH;
        //protected string _licenceKey;
        protected DateTime? _licenceDate;
        protected string _comment;
        protected bool _isUse;
        protected int _dispOrder;
        protected long? _serverLicenceID;
        protected string _nodeTableName;
        protected string _licenceKey1;
        protected string _licenceKey2;

        #endregion

        #region Constructors

        public AHServerProgramLicence() { }

        public AHServerProgramLicence(long labID, long? programID, string programName, long? sNo, long? licenceTypeId, string sQH, DateTime licenceDate, string comment, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp, int dispOrder, long? serverLicenceID, string nodeTableName, string licenceKey1, string licenceKey2)
        {
            this._labID = labID;
            this._programID = programID;
            this._programName = programName;
            this._sNo = sNo;
            this._licenceTypeId = licenceTypeId;
            this._sQH = sQH;
            this._licenceDate = licenceDate;
            this._comment = comment;
            this._isUse = isUse;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
            this._dispOrder = dispOrder;
            this._serverLicenceID = serverLicenceID;
            this._nodeTableName = nodeTableName;
            this._licenceKey1 = licenceKey1;
            this._licenceKey2 = licenceKey2;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "程序主键ID", ShortCode = "ProgramID", Desc = "程序主键ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ProgramID
        {
            get { return _programID; }
            set { _programID = value; }
        }

        [DataMember]
        [DataDesc(CName = "程序名称", ShortCode = "ProgramName", Desc = "程序名称", ContextType = SysDic.All, Length = 100)]
        public virtual string ProgramName
        {
            get { return _programName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for ProgramName", value, value.ToString());
                _programName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "SNo", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? SNo
        {
            get { return _sNo; }
            set { _sNo = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "授权类型", ShortCode = "LicenceTypeId", Desc = "授权类型", ContextType = SysDic.All, Length = 8)]
        public virtual long? LicenceTypeId
        {
            get { return _licenceTypeId; }
            set { _licenceTypeId = value; }
        }

        [DataMember]
        [DataDesc(CName = "授权号", ShortCode = "SQH", Desc = "授权号", ContextType = SysDic.All, Length = 20)]
        public virtual string SQH
        {
            get { return _sQH; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for SQH", value, value.ToString());
                _sQH = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "LicenceDate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? LicenceDate
        {
            get { return _licenceDate; }
            set { _licenceDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Comment", Desc = "备注", ContextType = SysDic.All, Length = 300)]
        public virtual string Comment
        {
            get { return _comment; }
            set
            {
                if (value != null && value.Length > 300)
                    throw new ArgumentOutOfRangeException("Invalid value for Comment", value, value.ToString());
                _comment = value;
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
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ServerLicenceID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? ServerLicenceID
        {
            get { return _serverLicenceID; }
            set { _serverLicenceID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "NodeTableName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string NodeTableName
        {
            get { return _nodeTableName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for NodeTableName", value, value.ToString());
                _nodeTableName = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "主服务器授权Key", ShortCode = "LicenceKey1", Desc = "主服务器授权Key", ContextType = SysDic.All, Length = 50)]
        public virtual string LicenceKey1
        {
            get { return _licenceKey1; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for LicenceKey1", value, value.ToString());
                _licenceKey1 = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "备份服务器授权Key", ShortCode = "LicenceKey2", Desc = "备份服务器授权Key", ContextType = SysDic.All, Length = 50)]
        public virtual string LicenceKey2
        {
            get { return _licenceKey2; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for LicenceKey2", value, value.ToString());
                _licenceKey2 = value;
            }
        }
        #endregion
    }
    #endregion
}