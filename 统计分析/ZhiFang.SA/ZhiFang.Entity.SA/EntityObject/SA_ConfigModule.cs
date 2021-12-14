using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.SA
{
    #region SAConfigModule

    /// <summary>
    /// 统计分析模块配置信息表
    /// </summary>
    [DataContract]
    [DataDesc(CName = "模块配置信息表", ClassCName = "SAConfigModule", ShortCode = "SAConfigModule", Desc = "模块配置信息表")]
    public class SAConfigModule : BaseEntity
    {
        #region Member Variables

        protected long _moduleId;
        protected string _moduleNo;
        protected string _moduleUrl;
        protected string _moduleParam;
        protected string _cName;
        protected string _sName;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected string _comment;
        protected string _filePath;
        protected bool _isUse;
        protected bool _isDefault;


        #endregion

        #region Constructors

        public SAConfigModule() { }

        public SAConfigModule(long labID, long moduleId, string moduleNo, string moduleUrl, string moduleParam, string cName, string sName, string shortcode, string pinYinZiTou, string comment, string filePath, bool isUse, bool isDefault, DateTime dataAddTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._moduleId = moduleId;
            this._moduleNo = moduleNo;
            this._moduleUrl = moduleUrl;
            this._moduleParam = moduleParam;
            this._cName = cName;
            this._sName = sName;
            this._shortcode = shortcode;
            this._pinYinZiTou = pinYinZiTou;
            this._comment = comment;
            this._filePath = filePath;
            this._isUse = isUse;
            this._isDefault = isDefault;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "所属模块Id", ShortCode = "ModuleId", Desc = "所属模块Id", ContextType = SysDic.All, Length = 8)]
        public virtual long ModuleId
        {
            get { return _moduleId; }
            set { _moduleId = value; }
        }

        [DataMember]
        [DataDesc(CName = "所属模块编码", ShortCode = "ModuleNo", Desc = "所属模块编码", ContextType = SysDic.All, Length = 100)]
        public virtual string ModuleNo
        {
            get { return _moduleNo; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for ModuleNo", value, value.ToString());
                _moduleNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "模块入口URL", ShortCode = "ModuleUrl", Desc = "模块入口URL", ContextType = SysDic.All, Length = 180)]
        public virtual string ModuleUrl
        {
            get { return _moduleUrl; }
            set
            {
                if (value != null && value.Length > 180)
                    throw new ArgumentOutOfRangeException("Invalid value for ModuleUrl", value, value.ToString());
                _moduleUrl = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "模块入口参数", ShortCode = "ModuleParam", Desc = "模块入口参数", ContextType = SysDic.All, Length = 180)]
        public virtual string ModuleParam
        {
            get { return _moduleParam; }
            set
            {
                if (value != null && value.Length > 180)
                    throw new ArgumentOutOfRangeException("Invalid value for ModuleParam", value, value.ToString());
                _moduleParam = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "配置模块名称", ShortCode = "CName", Desc = "配置模块名称", ContextType = SysDic.All, Length = 80)]
        public virtual string CName
        {
            get { return _cName; }
            set
            {
                if (value != null && value.Length > 80)
                    throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
                _cName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SName", Desc = "", ContextType = SysDic.All, Length = 80)]
        public virtual string SName
        {
            get { return _sName; }
            set
            {
                if (value != null && value.Length > 80)
                    throw new ArgumentOutOfRangeException("Invalid value for SName", value, value.ToString());
                _sName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Shortcode", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string Shortcode
        {
            get { return _shortcode; }
            set
            {
                _shortcode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PinYinZiTou", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string PinYinZiTou
        {
            get { return _pinYinZiTou; }
            set
            {
                _pinYinZiTou = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "配置内容", ShortCode = "Comment", Desc = "配置内容", ContextType = SysDic.All, Length = Int32.MaxValue)]
        public virtual string Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "存放路径", ShortCode = "FilePath", Desc = "存放路径", ContextType = SysDic.All, Length = 200)]
        public virtual string FilePath
        {
            get { return _filePath; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for FilePath", value, value.ToString());
                _filePath = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "是否启用", ShortCode = "IsUse", Desc = "是否启用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
        {
            get { return _isUse; }
            set { _isUse = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否公共模板", ShortCode = "IsDefault", Desc = "是否公共模板", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsDefault
        {
            get { return _isDefault; }
            set { _isDefault = value; }
        }


        #endregion
    }
    #endregion
}