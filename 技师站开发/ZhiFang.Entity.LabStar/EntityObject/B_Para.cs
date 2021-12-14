using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region BPara

    /// <summary>
    /// BPara object for NHibernate mapped table 'B_Para'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BPara", ShortCode = "BPara", Desc = "")]
    public class BPara : BaseEntity
    {
        #region Member Variables

        protected string _paraNo;
        protected string _cName;
        protected string _sName;
        protected string _typeCode;
        protected string _paraType;
        protected string _paraGroup;
        protected string _paraValue;
        protected string _paraDesc;
        protected string _paraEditInfo;
        protected string _systemCode;
        protected string _shortCode;
        protected int _dispOrder;
        protected string _pinYinZiTou;
        protected bool _bVisible;
        protected bool _isUse;
        protected string _paraVER;
        protected long? _operaterID;
        protected string _operater;
        protected DateTime? _dataUpdateTime;
        protected string _paraMainClassCode;
        protected string _paraMainClassName;
        protected IList<BParaItem> _bParaItemList;


        #endregion

        #region Constructors

        public BPara() { }

        public BPara(long labID, string paraNo, string cName, string sName, string typeCode, string paraType, string paraGroup, string paraValue, string paraDesc, string paraEditInfo, string systemCode, string shortCode, int dispOrder, string pinYinZiTou, bool bVisible, bool isUse, string paraVER, long operaterID, string operater, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, string paraMainClassCode, string paraMainClassName)
        {
            this._labID = labID;
            this._paraNo = paraNo;
            this._cName = cName;
            this._sName = sName;
            this._typeCode = typeCode;
            this._paraType = paraType;
            this._paraGroup = paraGroup;
            this._paraValue = paraValue;
            this._paraDesc = paraDesc;
            this._paraEditInfo = paraEditInfo;
            this._systemCode = systemCode;
            this._shortCode = shortCode;
            this._dispOrder = dispOrder;
            this._pinYinZiTou = pinYinZiTou;
            this._bVisible = bVisible;
            this._isUse = isUse;
            this._paraVER = paraVER;
            this._operaterID = operaterID;
            this._operater = operater;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._paraMainClassCode = paraMainClassCode;
            this._paraMainClassName = paraMainClassName;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "参数编号", ShortCode = "ParaNo", Desc = "参数编号", ContextType = SysDic.All, Length = 100)]
        public virtual string ParaNo
        {
            get { return _paraNo; }
            set { _paraNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "参数名称", ShortCode = "CName", Desc = "参数名称", ContextType = SysDic.All, Length = 200)]
        public virtual string CName
        {
            get { return _cName; }
            set { _cName = value; }
        }

        [DataMember]
        [DataDesc(CName = "参数简称", ShortCode = "SName", Desc = "参数简称", ContextType = SysDic.All, Length = 200)]
        public virtual string SName
        {
            get { return _sName; }
            set { _sName = value; }
        }

        [DataMember]
        [DataDesc(CName = "参数枚举类型名", ShortCode = "TypeCode", Desc = "参数枚举类型名", ContextType = SysDic.All, Length = 100)]
        public virtual string TypeCode
        {
            get { return _typeCode; }
            set { _typeCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "参数类型", ShortCode = "ParaType", Desc = "参数类型", ContextType = SysDic.All, Length = 100)]
        public virtual string ParaType
        {
            get { return _paraType; }
            set { _paraType = value; }
        }

        [DataMember]
        [DataDesc(CName = "参数组", ShortCode = "ParaGroup", Desc = "参数组", ContextType = SysDic.All, Length = 100)]
        public virtual string ParaGroup
        {
            get { return _paraGroup; }
            set { _paraGroup = value; }
        }

        [DataMember]
        [DataDesc(CName = "参数值", ShortCode = "ParaValue", Desc = "参数值", ContextType = SysDic.All, Length = -1)]
        public virtual string ParaValue
        {
            get { return _paraValue; }
            set { _paraValue = value; }
        }

        [DataMember]
        [DataDesc(CName = "参数描述", ShortCode = "ParaDesc", Desc = "参数描述", ContextType = SysDic.All, Length = 2000)]
        public virtual string ParaDesc
        {
            get { return _paraDesc; }
            set { _paraDesc = value; }
        }

        [DataMember]
        [DataDesc(CName = "参数编辑信息", ShortCode = "ParaEditInfo", Desc = "参数编辑信息", ContextType = SysDic.All, Length = -1)]
        public virtual string ParaEditInfo
        {
            get { return _paraEditInfo; }
            set { _paraEditInfo = value; }
        }

        [DataMember]
        [DataDesc(CName = "系统相关性", ShortCode = "SystemCode", Desc = "系统相关性", ContextType = SysDic.All, Length = 200)]
        public virtual string SystemCode
        {
            get { return _systemCode; }
            set { _systemCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "快捷码", ShortCode = "ShortCode", Desc = "快捷码", ContextType = SysDic.All, Length = 100)]
        public virtual string ShortCode
        {
            get { return _shortCode; }
            set { _shortCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "拼音字头", ShortCode = "PinYinZiTou", Desc = "拼音字头", ContextType = SysDic.All, Length = 100)]
        public virtual string PinYinZiTou
        {
            get { return _pinYinZiTou; }
            set { _pinYinZiTou = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否显示", ShortCode = "BVisible", Desc = "是否显示", ContextType = SysDic.All, Length = 1)]
        public virtual bool BVisible
        {
            get { return _bVisible; }
            set { _bVisible = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否采用", ShortCode = "IsUse", Desc = "是否采用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
        {
            get { return _isUse; }
            set { _isUse = value; }
        }

        [DataMember]
        [DataDesc(CName = "参数版本", ShortCode = "ParaVER", Desc = "参数版本", ContextType = SysDic.All, Length = 50)]
        public virtual string ParaVER
        {
            get { return _paraVER; }
            set { _paraVER = value; }
        }

        [DataMember]
        [DataDesc(CName = "参数大类代码", ShortCode = "ParaMainClassCode", Desc = "参数大类代码", ContextType = SysDic.All, Length = 100)]
        public virtual string ParaMainClassCode
        {
            get { return _paraMainClassCode; }
            set { _paraMainClassCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "参数大类名称", ShortCode = "ParaMainClassName", Desc = "参数大类名称", ContextType = SysDic.All, Length = 200)]
        public virtual string ParaMainClassName
        {
            get { return _paraMainClassName; }
            set { _paraMainClassName = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "操作者ID", ShortCode = "OperaterID", Desc = "操作者ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? OperaterID
        {
            get { return _operaterID; }
            set { _operaterID = value; }
        }

        [DataMember]
        [DataDesc(CName = "操作者姓名", ShortCode = "Operater", Desc = "操作者姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string Operater
        {
            get { return _operater; }
            set { _operater = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "DataUpdateTime", ShortCode = "DataUpdateTime", Desc = "DataUpdateTime", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "参数来源类别", ShortCode = "ParaSource", Desc = "0出厂参数，1默认参数，2个性参数", ContextType = SysDic.All, Length = 4)]
        public virtual int ParaSource { get; set; }


        //[DataMember]
        //[DataDesc(CName = "参数个性设置值", ShortCode = "BParaItemList", Desc = "参数个性设置值")]
        //public virtual IList<BParaItem> BParaItemList
        //{
        //    get
        //    {
        //        if (_bParaItemList == null)
        //        {
        //            _bParaItemList = new List<BParaItem>();
        //        }
        //        return _bParaItemList;
        //    }
        //    set { _bParaItemList = value; }
        //}

        #endregion
    }
    #endregion
}