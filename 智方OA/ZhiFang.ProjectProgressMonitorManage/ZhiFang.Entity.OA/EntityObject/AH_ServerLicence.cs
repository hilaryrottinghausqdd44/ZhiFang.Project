using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.OA.ViewObject.Request;

namespace ZhiFang.Entity.OA
{
    #region AHServerLicence

    /// <summary>
    /// AHServerLicence object for NHibernate mapped table 'AH_ServerLicence'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "服务器授权", ClassCName = "AHServerLicence", ShortCode = "AHServerLicence", Desc = "服务器授权")]
    public class AHServerLicence : AHLicence
    {
        #region Member Variables

        protected long? _pClientID;
        protected string _pClientName;
        protected long? _pContractID;
        protected string _lRNo1;
        protected string _lRNo2;
        protected long _status;
        protected long? _applyID;
        protected string _applyName;
        protected DateTime? _applyDataTime;
        protected string _applyInfo;
        protected long? _oneAuditID;
        protected string _oneAuditName;
        protected DateTime? _oneAuditDataTime;
        protected string _oneAuditInfo;
        protected long? _twoAuditID;
        protected string _twoAuditName;
        protected DateTime? _twoAuditDataTime;
        protected string _twoAuditInfo;
        protected DateTime? _genDateTime;
        protected string _comment;
        protected bool _isUse;
        //protected string _licenceKey;
        protected string _lRNo;
        private bool _IsSpecially;

        #endregion

        #region Constructors

        public AHServerLicence() { }

        public AHServerLicence(long labID, long? pClientID, string pClientName, long? pContractID,  string lRNo1,  string lRNo2, long status, long applyID, string applyName, DateTime applyDataTime, string applyInfo, long? oneAuditID, string oneAuditName, DateTime oneAuditDataTime, string oneAuditInfo, long? twoAuditID, string twoAuditName, DateTime twoAuditDataTime, string twoAuditInfo, DateTime genDateTime, string comment, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp, string licenceKey, string lRNo)
        {
            this._labID = labID;
            this._pClientID = pClientID;
            this._pClientName = pClientName;
            this._pContractID = pContractID;
            this._lRNo1 = lRNo1;
            this._lRNo2 = lRNo2;
            this._status = status;
            this._applyID = applyID;
            this._applyName = applyName;
            this._applyDataTime = applyDataTime;
            this._applyInfo = applyInfo;
            this._oneAuditID = oneAuditID;
            this._oneAuditName = oneAuditName;
            this._oneAuditDataTime = oneAuditDataTime;
            this._oneAuditInfo = oneAuditInfo;
            this._twoAuditID = twoAuditID;
            this._twoAuditName = twoAuditName;
            this._twoAuditDataTime = twoAuditDataTime;
            this._twoAuditInfo = twoAuditInfo;
            this._genDateTime = genDateTime;
            this._comment = comment;
            this._isUse = isUse;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
            //this._licenceKey = licenceKey;
            this._lRNo = lRNo;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "客户ID", ShortCode = "PClientID", Desc = "客户ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? PClientID
        {
            get { return _pClientID; }
            set { _pClientID = value; }
        }

        [DataMember]
        [DataDesc(CName = "客户名称", ShortCode = "PClientName", Desc = "客户名称", ContextType = SysDic.All, Length = 100)]
        public virtual string PClientName
        {
            get { return _pClientName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for PClientName", value, value.ToString());
                _pClientName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "合同ID", ShortCode = "PContractID", Desc = "合同ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? PContractID
        {
            get { return _pContractID; }
            set { _pContractID = value; }
        }
        [DataMember]
        [DataDesc(CName = "主服务器授权申请号", ShortCode = "LRNo1", Desc = "主服务器授权申请号", ContextType = SysDic.All, Length = 10)]
        public virtual string LRNo1
        {
            get { return _lRNo1; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for LRNo1", value, value.ToString());
                _lRNo1 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "备份服务器授权申请号", ShortCode = "LRNo2", Desc = "备份服务器授权申请号", ContextType = SysDic.All, Length = 10)]
        public virtual string LRNo2
        {
            get { return _lRNo2; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for LRNo2", value, value.ToString());
                _lRNo2 = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "流程状态", ShortCode = "Status", Desc = "流程状态", ContextType = SysDic.All, Length = 8)]
        public virtual long Status
        {
            get { return _status; }
            set { _status = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "申请人ID", ShortCode = "ApplyID", Desc = "申请人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ApplyID
        {
            get { return _applyID; }
            set { _applyID = value; }
        }

        [DataMember]
        [DataDesc(CName = "申请人名称", ShortCode = "ApplyName", Desc = "申请人名称", ContextType = SysDic.All, Length = 50)]
        public virtual string ApplyName
        {
            get { return _applyName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ApplyName", value, value.ToString());
                _applyName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ApplyDataTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ApplyDataTime
        {
            get { return _applyDataTime; }
            set { _applyDataTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "申请备注", ShortCode = "ApplyInfo", Desc = "申请备注", ContextType = SysDic.All, Length = 300)]
        public virtual string ApplyInfo
        {
            get { return _applyInfo; }
            set
            {
                if (value != null && value.Length > 300)
                    throw new ArgumentOutOfRangeException("Invalid value for ApplyInfo", value, value.ToString());
                _applyInfo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "一审人ID", ShortCode = "OneAuditID", Desc = "一审人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? OneAuditID
        {
            get { return _oneAuditID; }
            set { _oneAuditID = value; }
        }

        [DataMember]
        [DataDesc(CName = "一审人名称", ShortCode = "OneAuditName", Desc = "一审人名称", ContextType = SysDic.All, Length = 50)]
        public virtual string OneAuditName
        {
            get { return _oneAuditName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for OneAuditName", value, value.ToString());
                _oneAuditName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "一审时间", ShortCode = "OneAuditDataTime", Desc = "一审时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? OneAuditDataTime
        {
            get { return _oneAuditDataTime; }
            set { _oneAuditDataTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "一审意见", ShortCode = "OneAuditInfo", Desc = "一审意见", ContextType = SysDic.All, Length = 300)]
        public virtual string OneAuditInfo
        {
            get { return _oneAuditInfo; }
            set
            {
                if (value != null && value.Length > 300)
                    throw new ArgumentOutOfRangeException("Invalid value for OneAuditInfo", value, value.ToString());
                _oneAuditInfo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "特殊审批人ID", ShortCode = "TwoAuditID", Desc = "特殊审批人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? TwoAuditID
        {
            get { return _twoAuditID; }
            set { _twoAuditID = value; }
        }

        [DataMember]
        [DataDesc(CName = "特殊审批人名称", ShortCode = "TwoAuditName", Desc = "特殊审批人名称", ContextType = SysDic.All, Length = 50)]
        public virtual string TwoAuditName
        {
            get { return _twoAuditName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for TwoAuditName", value, value.ToString());
                _twoAuditName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "特殊审批时间", ShortCode = "TwoAuditDataTime", Desc = "特殊审批时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? TwoAuditDataTime
        {
            get { return _twoAuditDataTime; }
            set { _twoAuditDataTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "特殊审批意见", ShortCode = "TwoAuditInfo", Desc = "特殊审批意见", ContextType = SysDic.All, Length = 300)]
        public virtual string TwoAuditInfo
        {
            get { return _twoAuditInfo; }
            set
            {
                if (value != null && value.Length > 300)
                    throw new ArgumentOutOfRangeException("Invalid value for TwoAuditInfo", value, value.ToString());
                _twoAuditInfo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "授权时间", ShortCode = "GenDateTime", Desc = "授权时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? GenDateTime
        {
            get { return _genDateTime; }
            set { _genDateTime = value; }
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
        [DataDesc(CName = "", ShortCode = "LRNo", Desc = "", ContextType = SysDic.All, Length = 30)]
        public virtual string LRNo
        {
            get { return _lRNo; }
            set
            {
                if (value != null && value.Length > 30)
                    throw new ArgumentOutOfRangeException("Invalid value for LRNo", value, value.ToString());
                _lRNo = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "特殊审批标记", ShortCode = "IsSpecially", Desc = "特殊审批标记", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsSpecially
        {
            get { return _IsSpecially; }
            set { _IsSpecially = value; }
        }

        [DataMember]
        [DataDesc(CName = "授权客户名称", ShortCode = "LicenceClientName", Desc = "授权客户名称", ContextType = SysDic.All, Length = 500)]
        public virtual string LicenceClientName { get; set; }

        [DataMember]
        [DataDesc(CName = "授权编码", ShortCode = "LicenceCode", Desc = "授权编码")]
        public virtual string LicenceCode { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual DateTime? LicenceDate { get; set; }

        #endregion
    }
    #endregion
}