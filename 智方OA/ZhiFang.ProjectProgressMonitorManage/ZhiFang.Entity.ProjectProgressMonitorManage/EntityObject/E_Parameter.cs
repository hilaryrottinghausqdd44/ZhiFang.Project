using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;
using System;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
    #region EParameter

    /// <summary>
    /// BParameter object for NHibernate mapped table 'B_Parameter'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "参数表", ClassCName = "BParameter", ShortCode = "BParameter", Desc = "参数表")]
    public class EParameter : BaseEntityService
    {
        #region Member Variables

        protected string _cname;
        protected string _sName;
        protected string _paraType;
        protected string _paraNo;
        protected string _paraValue;
        protected string _paraDesc;
        protected string _shortcode;
        protected int _dispOrder;
        protected string _pinYinZiTou;
        protected bool _isUse;
        protected bool _isUserSet;
        protected DateTime? _dataUpdateTime;

        #endregion

        #region Constructors

        public EParameter() { }

        public EParameter(long labID, string cname, string sName, string paraType, string paraNo, string paraValue, string paraDesc, string shortcode, int dispOrder, string pinYinZiTou, bool isUse, bool isUserSet, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._cname = cname;
            this._sName = sName;
            this._paraType = paraType;
            this._paraNo = paraNo;
            this._paraValue = paraValue;
            this._paraDesc = paraDesc;
            this._shortcode = shortcode;
            this._dispOrder = dispOrder;
            this._pinYinZiTou = pinYinZiTou;
            this._isUse = isUse;
            this._isUserSet = isUserSet;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "CName", Desc = "名称", ContextType = SysDic.All, Length = 100)]
        public virtual string CName
        {
            get { return _cname; }
            set
            {
                _cname = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 100)]
        public virtual string SName
        {
            get { return _sName; }
            set
            {;
                _sName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "参数类型", ShortCode = "ParaType", Desc = "参数类型", ContextType = SysDic.All, Length = 100)]
        public virtual string ParaType
        {
            get { return _paraType; }
            set
            {
                _paraType = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "参数编码", ShortCode = "ParaNo", Desc = "参数编码", ContextType = SysDic.All, Length = 50)]
        public virtual string ParaNo
        {
            get { return _paraNo; }
            set
            {
                _paraNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "参数值", ShortCode = "ParaValue", Desc = "参数值", ContextType = SysDic.All, Length =Int32.MaxValue)]
        public virtual string ParaValue
        {
            get { return _paraValue; }
            set
            {
                _paraValue = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "参数说明", ShortCode = "ParaDesc", Desc = "参数说明", ContextType = SysDic.All, Length = 1000)]
        public virtual string ParaDesc
        {
            get { return _paraDesc; }
            set
            {
                _paraDesc = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "快捷码", ShortCode = "Shortcode", Desc = "快捷码", ContextType = SysDic.All, Length = 20)]
        public virtual string Shortcode
        {
            get { return _shortcode; }
            set
            {
                _shortcode = value;
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
        [DataDesc(CName = "汉字拼音字头", ShortCode = "PinYinZiTou", Desc = "汉字拼音字头", ContextType = SysDic.All, Length = 50)]
        public virtual string PinYinZiTou
        {
            get { return _pinYinZiTou; }
            set
            {
                _pinYinZiTou = value;
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
        [DataDesc(CName = "是否允许用户设置", ShortCode = "IsUserSet", Desc = "是否允许用户设置", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUserSet
        {
            get { return _isUserSet; }
            set { _isUserSet = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }
        #endregion
    }
    #endregion
}