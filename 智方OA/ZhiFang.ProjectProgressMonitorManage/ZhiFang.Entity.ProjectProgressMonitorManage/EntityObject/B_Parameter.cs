using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;
using System;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
    #region BParameter

    /// <summary>
    /// BParameter object for NHibernate mapped table 'B_Parameter'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "参数表", ClassCName = "BParameter", ShortCode = "BParameter", Desc = "参数表")]
    public class BParameter : BaseEntityService
    {
        #region Member Variables

        protected string _name;
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
        protected PDict _pDict;

        #endregion

        #region Constructors

        public BParameter() { }

        public BParameter(long labID, string name, string sName, string paraType, string paraNo, string paraValue, string paraDesc, string shortcode, int dispOrder, string pinYinZiTou, bool isUse, bool isUserSet, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, PDict pDict)
        {
            this._labID = labID;
            this._name = name;
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
            this._pDict = pDict;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "Name", Desc = "名称", ContextType = SysDic.All, Length = 100)]
        public virtual string Name
        {
            get { return _name; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Name", value, value.ToString());
                _name = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 100)]
        public virtual string SName
        {
            get { return _sName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for SName", value, value.ToString());
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
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for ParaType", value, value.ToString());
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
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ParaNo", value, value.ToString());
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
                if (value != null && value.Length >1002400 )
                    throw new ArgumentOutOfRangeException("Invalid value for ParaValue", value, value.ToString());
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
                if (value != null && value.Length > 1000)
                    throw new ArgumentOutOfRangeException("Invalid value for ParaDesc", value, value.ToString());
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
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Shortcode", value, value.ToString());
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
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for PinYinZiTou", value, value.ToString());
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

        [DataMember]
        [DataDesc(CName = "字典表", ShortCode = "PDict", Desc = "字典表")]
        public virtual PDict PDict
        {
            get { return _pDict; }
            set { _pDict = value; }
        }


        #endregion
    }
    #endregion
}