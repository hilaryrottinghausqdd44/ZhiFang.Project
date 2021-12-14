using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.OA.ViewObject.Request;

namespace ZhiFang.Entity.OA
{
    #region AHServerEquipLicence

    /// <summary>
    /// AHServerEquipLicence object for NHibernate mapped table 'AH_ServerEquipLicence'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "服务器仪器授权", ClassCName = "AHServerEquipLicence", ShortCode = "AHServerEquipLicence", Desc = "服务器仪器授权")]
    public class AHServerEquipLicence : AHServerEquipLicenceBase
    {
        #region Member Variables

        protected long? _equipID;
        protected string _equipName;
        protected long? _sNo;
        protected long? _licenceTypeId;
        protected string _sQH;
        protected string _licenceKey;
        protected DateTime? _licenceDate;
        protected string _comment;
        protected bool _isUse;
        protected int _dispOrder;
        protected long? _serverLicenceID;
        protected string _nodeTableName;
        protected string _programName;
        protected string _equipversion;
        #endregion

        #region Constructors

        public AHServerEquipLicence() { }

        public AHServerEquipLicence(long labID, long? equipID, string equipName, long? sNo, long? licenceTypeId, string sQH, string licenceKey, DateTime? licenceDate, string comment, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp, int dispOrder, long? serverLicenceID, string nodeTableName,string programName)
        {
            this._labID = labID;
            this._equipID = equipID;
            this._equipName = equipName;
            this._sNo = sNo;
            this._licenceTypeId = licenceTypeId;
            this._sQH = sQH;
            this._licenceKey = licenceKey;
            this._licenceDate = licenceDate;
            this._comment = comment;
            this._isUse = isUse;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
            this._dispOrder = dispOrder;
            this._serverLicenceID = serverLicenceID;
            this._nodeTableName = nodeTableName;
            this._programName = programName;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [DataDesc(CName = "系统程序名(仪器型号)", ShortCode = "Equipversion", Desc = "系统程序名", ContextType = SysDic.All, Length = 50)]
        public virtual string Equipversion {
            get { return _equipversion; }
            set { _equipversion = value; }
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
        [DataDesc(CName = "仪器名称", ShortCode = "EquipName", Desc = "仪器名称", ContextType = SysDic.All, Length = 100)]
        public virtual string EquipName
        {
            get { return _equipName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for EquipName", value, value.ToString());
                _equipName = value;
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
        [DataDesc(CName = "授权Key", ShortCode = "LicenceKey", Desc = "授权Key", ContextType = SysDic.All, Length = 50)]
        public virtual string LicenceKey
        {
            get { return _licenceKey; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for LicenceKey", value, value.ToString());
                _licenceKey = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "截至日期", ShortCode = "LicenceDate", Desc = "截至日期", ContextType = SysDic.All, Length = 8)]
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
        [DataDesc(CName = "用户程序名", ShortCode = "ProgramName", Desc = "用户程序名", ContextType = SysDic.All, Length = 50)]
        public virtual string ProgramName
        {
            get { return _programName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ProgramName", value, value.ToString());
                _programName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "上次授权时间", ShortCode = "", Desc = "上次授权时间", ContextType = SysDic.All, Length = 50)]
        public virtual string AHBeforeDateTime { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "上次授权类型", ShortCode = "", Desc = "上次授权类型", ContextType = SysDic.All, Length = 50)]
        public virtual long? AHBeforeLicenceTypeId { get; set; }

        [DataMember]
        [DataDesc(CName = "上次授权类型名称", ShortCode = "", Desc = "上次授权类型名称", ContextType = SysDic.All, Length = 50)]
        public virtual string AHBeforeLicenceTypeName { get; set; }

        [DataMember]
        [DataDesc(CName = "上次SQH", ShortCode = "", Desc = "上次SQH", ContextType = SysDic.All, Length = 50)]
        public virtual string AHBeforeSQH { get; set; }

        #endregion
    }
    #endregion
}