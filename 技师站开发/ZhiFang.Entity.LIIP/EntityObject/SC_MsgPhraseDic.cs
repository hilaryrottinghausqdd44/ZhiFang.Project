using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LIIP
{
    #region SCMsgPhraseDic

    /// <summary>
    /// SCMsgPhraseDic object for NHibernate mapped table 'SC_MsgPhraseDic'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "公共消息短语字典", ClassCName = "SCMsgPhraseDic", ShortCode = "SCMsgPhraseDic", Desc = "公共消息短语字典")]
    public class SCMsgPhraseDic : BaseEntity
    {
        #region Member Variables

        protected string _context;
        protected string _code;
        protected int _visible;
        protected long? _msgTypeID;
        protected string _msgTypeCName;
        protected string _msgTypeCode;
        protected string _memo;
        protected int _dispOrder;
        protected bool _isUse;
        protected long _creatorID;
        protected string _creatorName;
        protected DateTime? _dataUpdateTime;


        #endregion

        #region Constructors

        public SCMsgPhraseDic() { }

        public SCMsgPhraseDic(long labID, string context, string code, int visible, long msgTypeID, string msgTypeCName, string msgTypeCode, string memo, int dispOrder, bool isUse, long creatorID, string creatorName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._context = context;
            this._code = code;
            this._visible = visible;
            this._msgTypeID = msgTypeID;
            this._msgTypeCName = msgTypeCName;
            this._msgTypeCode = msgTypeCode;
            this._memo = memo;
            this._dispOrder = dispOrder;
            this._isUse = isUse;
            this._creatorID = creatorID;
            this._creatorName = creatorName;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "短语内容", ShortCode = "Context", Desc = "短语内容", ContextType = SysDic.All, Length = 500)]
        public virtual string Context
        {
            get { return _context; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for Context", value, value.ToString());
                _context = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "短语编码", ShortCode = "Code", Desc = "短语编码", ContextType = SysDic.All, Length = 20)]
        public virtual string Code
        {
            get { return _code; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Code", value, value.ToString());
                _code = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "是否可用", ShortCode = "Visible", Desc = "是否可用", ContextType = SysDic.All, Length = 4)]
        public virtual int Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "MsgTypeID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? MsgTypeID
        {
            get { return _msgTypeID; }
            set { _msgTypeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "消息类型名称", ShortCode = "MsgTypeCName", Desc = "消息类型名称", ContextType = SysDic.All, Length = 100)]
        public virtual string MsgTypeCName
        {
            get { return _msgTypeCName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for MsgTypeCName", value, value.ToString());
                _msgTypeCName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "消息类型代码", ShortCode = "MsgTypeCode", Desc = "消息类型代码", ContextType = SysDic.All, Length = 50)]
        public virtual string MsgTypeCode
        {
            get { return _msgTypeCode; }
            set { _msgTypeCode = value; }
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
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
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
        [DataDesc(CName = "创建者", ShortCode = "CreatorID", Desc = "创建者", ContextType = SysDic.All, Length = 8)]
        public virtual long CreatorID
        {
            get { return _creatorID; }
            set { _creatorID = value; }
        }

        [DataMember]
        [DataDesc(CName = "创建者姓名", ShortCode = "CreatorName", Desc = "创建者姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string CreatorName
        {
            get { return _creatorName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for CreatorName", value, value.ToString());
                _creatorName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据修改时间", ShortCode = "DataUpdateTime", Desc = "数据修改时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }


        #endregion
    }
    #endregion
}