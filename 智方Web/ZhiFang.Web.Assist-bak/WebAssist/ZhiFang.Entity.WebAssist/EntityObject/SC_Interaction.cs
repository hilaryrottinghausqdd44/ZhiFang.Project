using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.WebAssist
{
    #region SCInteraction

    /// <summary>
    /// SCInteraction object for NHibernate mapped table 'SC_Interaction'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "公共交流表", ClassCName = "SCInteraction", ShortCode = "SCInteraction", Desc = "公共交流表")]
    public class SCInteraction : BaseEntity
    {
        #region Member Variables

        protected long? _bobjectID;
        protected string _contents;
        protected long _senderID;
        protected string _senderName;
        protected long? _receiverID;
        protected string _receiverName;
        protected bool _hasAttachment;
        protected string _memo;
        protected bool _isUse;
        private string _BusinessModuleCode;
        protected bool _isCommunication;


        #endregion

        #region Constructors

        public SCInteraction() { }

        public SCInteraction(long labID, long bobjectID, string contents, long senderID, string senderName, long receiverID, string receiverName, bool hasAttachment, string memo, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._bobjectID = bobjectID;
            this._contents = contents;
            this._senderID = senderID;
            this._senderName = senderName;
            this._receiverID = receiverID;
            this._receiverName = receiverName;
            this._hasAttachment = hasAttachment;
            this._memo = memo;
            this._isUse = isUse;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "业务对象ID", ShortCode = "BobjectID", Desc = "业务对象ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? BobjectID
        {
            get { return _bobjectID; }
            set { _bobjectID = value; }
        }

        [DataMember]
        [DataDesc(CName = "内容", ShortCode = "Contents", Desc = "内容", ContextType = SysDic.All, Length = -1)]
        public virtual string Contents
        {
            get { return _contents; }
            set
            {
                
                _contents = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "发件人", ShortCode = "SenderID", Desc = "发件人", ContextType = SysDic.All, Length = 8)]
        public virtual long SenderID
        {
            get { return _senderID; }
            set { _senderID = value; }
        }

        [DataMember]
        [DataDesc(CName = "发件人姓名", ShortCode = "SenderName", Desc = "发件人姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string SenderName
        {
            get { return _senderName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for SenderName", value, value.ToString());
                _senderName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "接收人", ShortCode = "ReceiverID", Desc = "接收人", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReceiverID
        {
            get { return _receiverID; }
            set { _receiverID = value; }
        }

        [DataMember]
        [DataDesc(CName = "收件人姓名", ShortCode = "ReceiverName", Desc = "收件人姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string ReceiverName
        {
            get { return _receiverName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ReceiverName", value, value.ToString());
                _receiverName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "是否带附件", ShortCode = "HasAttachment", Desc = "是否带附件", ContextType = SysDic.All, Length = 1)]
        public virtual bool HasAttachment
        {
            get { return _hasAttachment; }
            set { _hasAttachment = value; }
        }

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = 500)]
        public virtual string Memo
        {
            get { return _memo; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for Memo", value, value.ToString());
                _memo = value;
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
        [DataDesc(CName = "业务模块代码", ShortCode = "BusinessModuleCode", Desc = "业务模块代码", ContextType = SysDic.All, Length = 1)]
        public virtual string BusinessModuleCode
        {
            get { return _BusinessModuleCode; }
            set { _BusinessModuleCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "话题标志", ShortCode = "IsCommunication", Desc = "话题标志", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsCommunication
        {
            get { return _isCommunication; }
            set { _isCommunication = value; }
        }

        #endregion
    }
    #endregion
}