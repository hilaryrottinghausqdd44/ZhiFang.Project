using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
    #region GMGroupType

    /// <summary>
    /// GMGroupType object for NHibernate mapped table 'GM_GroupType'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "小组类型", ClassCName = "GMGroupType", ShortCode = "GMGroupType", Desc = "小组类型")]
    public class GMGroupType : BaseEntity
    {
        #region Member Variables

        protected string _name;
        protected string _sName;
        protected string _useCode;
        protected string _standCode;
        protected string _deveCode;
        protected string _url;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected string _comment;
        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected IList<GMGroup> _gMGroupList;
        protected IList<GMGroupTypeOperate> _gMGroupTypeOperateList;

        #endregion

        #region Constructors

        public GMGroupType() { }

        public GMGroupType(long labID, string name, string sName, string useCode, string standCode, string deveCode, string url, string shortcode, string pinYinZiTou, string comment, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._name = name;
            this._sName = sName;
            this._useCode = useCode;
            this._standCode = standCode;
            this._deveCode = deveCode;
            this._url = url;
            this._shortcode = shortcode;
            this._pinYinZiTou = pinYinZiTou;
            this._comment = comment;
            this._isUse = isUse;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "Name", Desc = "名称", ContextType = SysDic.All, Length = 40)]
        public virtual string Name
        {
            get { return _name; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for Name", value, value.ToString());
                _name = value;
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
        [DataDesc(CName = "代码", ShortCode = "UseCode", Desc = "代码", ContextType = SysDic.All, Length = 50)]
        public virtual string UseCode
        {
            get { return _useCode; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for UseCode", value, value.ToString());
                _useCode = value;
            }
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
        [DataDesc(CName = "资源定位", ShortCode = "Url", Desc = "资源定位", ContextType = SysDic.All, Length = 60)]
        public virtual string Url
        {
            get { return _url; }
            set
            {
                _url = value;
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
        [DataDesc(CName = "汉字拼音字头", ShortCode = "PinYinZiTou", Desc = "汉字拼音字头", ContextType = SysDic.All, Length = 50)]
        public virtual string PinYinZiTou
        {
            get { return _pinYinZiTou; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for PinYinZiTou", value, value.ToString());
                _pinYinZiTou = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Comment", Desc = "备注", ContextType = SysDic.All, Length = 16)]
        public virtual string Comment
        {
            get { return _comment; }
            set
            {
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
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "小组表", ShortCode = "GMGroupList", Desc = "小组表")]
        public virtual IList<GMGroup> GMGroupList
        {
            get
            {
                if (_gMGroupList == null)
                {
                    _gMGroupList = new List<GMGroup>();
                }
                return _gMGroupList;
            }
            set { _gMGroupList = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "GMGroupTypeOperateList", Desc = "")]
        public virtual IList<GMGroupTypeOperate> GMGroupTypeOperateList
        {
            get
            {
                if (_gMGroupTypeOperateList == null)
                {
                    _gMGroupTypeOperateList = new List<GMGroupTypeOperate>();
                }
                return _gMGroupTypeOperateList;
            }
            set { _gMGroupTypeOperateList = value; }
        }


        #endregion
    }
    #endregion
}