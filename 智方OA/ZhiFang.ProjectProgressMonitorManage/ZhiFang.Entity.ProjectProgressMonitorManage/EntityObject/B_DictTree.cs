using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;
using System;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
    #region BDictTree

    /// <summary>
    /// BDictTree object for NHibernate mapped table 'F_TypeTree'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "类型树", ClassCName = "BDictTree", ShortCode = "BDictTree", Desc = "类型树")]
    public class BDictTree : BaseEntityService
    {
        #region Member Variables

        protected long? _parentID;
        protected string _cName;
        protected string _sName;
        protected string _shortcode;
        protected string _memo;
        protected int _dispOrder;
        protected bool _isUse;
        protected string _creatorName;
        protected string _standCode;
        protected string _deveCode;
        protected DateTime? _dataUpdateTime;
        protected HREmployee _creator;
        protected IList<FFile> _fFileList;

        #endregion

        #region Constructors

        public BDictTree() { }

        public BDictTree(long labID, long parentID, string cName, string sName, string shortcode, string memo, int dispOrder, bool isUse, string creatorName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, HREmployee creator, string standCode, string deveCode)
        {
            this._labID = labID;
            this._parentID = parentID;
            this._cName = cName;
            this._sName = sName;
            this._shortcode = shortcode;
            this._memo = memo;
            this._dispOrder = dispOrder;
            this._isUse = isUse;
            this._creatorName = creatorName;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._creator = creator;
            this._standCode = standCode;
            this._deveCode = deveCode;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "树形结构父级ID", ShortCode = "ParentID", Desc = "树形结构父级ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ParentID
        {
            get { return _parentID; }
            set { _parentID = value; }
        }

        [DataMember]
        [DataDesc(CName = "类型树名称", ShortCode = "CName", Desc = "类型树名称", ContextType = SysDic.All, Length = 100)]
        public virtual string CName
        {
            get { return _cName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
                _cName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 40)]
        public virtual string SName
        {
            get { return _sName; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for SName", value, value.ToString());
                _sName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "快捷码", ShortCode = "Shortcode", Desc = "快捷码", ContextType = SysDic.All, Length = 20)]
        public virtual string Shortcode
        {
            get { return _shortcode; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Shortcode", value, value.ToString());
                _shortcode = value;
            }
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
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
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
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "标准代码", ShortCode = "StandCode", Desc = "标准代码", ContextType = SysDic.All, Length = 50)]
        public virtual string StandCode
        {
            get { return _standCode; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for StandCode", value, value.ToString());
                _standCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "开发商标准代码", ShortCode = "DeveCode", Desc = "开发商标准代码", ContextType = SysDic.All, Length = 50)]
        public virtual string DeveCode
        {
            get { return _deveCode; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for DeveCode", value, value.ToString());
                _deveCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "员工", ShortCode = "Creator", Desc = "员工")]
        public virtual HREmployee Creator
        {
            get { return _creator; }
            set { _creator = value; }
        }

        [DataMember]
        [DataDesc(CName = "文档表", ShortCode = "FFileList", Desc = "文档表")]
        public virtual IList<FFile> FFileList
        {
            get
            {
                if (_fFileList == null)
                {
                    _fFileList = new List<FFile>();
                }
                return _fFileList;
            }
            set { _fFileList = value; }
        }


        #endregion
    }
    #endregion
}