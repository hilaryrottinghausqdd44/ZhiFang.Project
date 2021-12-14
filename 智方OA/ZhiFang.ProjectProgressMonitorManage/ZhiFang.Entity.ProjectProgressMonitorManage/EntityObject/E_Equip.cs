using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;
using System;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
    #region EEquip

    /// <summary>
    /// EEquip object for NHibernate mapped table 'E_Equip'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "仪器表", ClassCName = "EEquip", ShortCode = "EEquip", Desc = "仪器表")]
    public class EEquip : BaseEntity
    {
        #region Member Variables

        protected string _cName;
        protected string _eName;
        protected string _sName;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected string _useCode;
        protected string _comment;
        protected string _sectionName;
        protected bool _isUse;
        protected int _dispOrder;
        protected string _equipNo;
        protected string _factoryName;
        protected string _factoryOutNo;
        protected string _storeArea;
        protected DateTime? _enableDate;
        protected DateTime? _calibrateDate;
        protected DateTime? _dataUpdateTime;
        protected PDict _equipType;
        protected IList<ETemplet> _eTempletList;

        #endregion

        #region Constructors

        public EEquip() { }

        public EEquip(long labID, long equipTypeID, string cName, string eName, string sName, string shortcode, string pinYinZiTou, string useCode, string comment, string sectionName, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._cName = cName;
            this._eName = eName;
            this._sName = sName;
            this._shortcode = shortcode;
            this._pinYinZiTou = pinYinZiTou;
            this._useCode = useCode;
            this._comment = comment;
            this._sectionName = sectionName;
            this._isUse = isUse;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [DataDesc(CName = "仪器名称", ShortCode = "CName", Desc = "仪器名称", ContextType = SysDic.All, Length = 100)]
        public virtual string CName
        {
            get { return _cName; }
            set { _cName = value; }
        }

        [DataMember]
        [DataDesc(CName = "英文名称", ShortCode = "EName", Desc = "英文名称", ContextType = SysDic.All, Length = 50)]
        public virtual string EName
        {
            get { return _eName; }
            set { _eName = value; }
        }

        [DataMember]
        [DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 50)]
        public virtual string SName
        {
            get { return _sName; }
            set { _sName = value; }
        }

        [DataMember]
        [DataDesc(CName = "快捷码", ShortCode = "Shortcode", Desc = "快捷码", ContextType = SysDic.All, Length = 50)]
        public virtual string Shortcode
        {
            get { return _shortcode; }
            set { _shortcode = value; }
        }

        [DataMember]
        [DataDesc(CName = "汉字拼音字头", ShortCode = "PinYinZiTou", Desc = "汉字拼音字头", ContextType = SysDic.All, Length = 50)]
        public virtual string PinYinZiTou
        {
            get { return _pinYinZiTou; }
            set { _pinYinZiTou = value; }
        }

        [DataMember]
        [DataDesc(CName = "代码", ShortCode = "UseCode", Desc = "代码", ContextType = SysDic.All, Length = 50)]
        public virtual string UseCode
        {
            get { return _useCode; }
            set { _useCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "描述", ShortCode = "Comment", Desc = "描述", ContextType = SysDic.All, Length = 4000)]
        public virtual string Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }

        [DataMember]
        [DataDesc(CName = "对应检验小组", ShortCode = "SectionName", Desc = "对应检验小组", ContextType = SysDic.All, Length = 100)]
        public virtual string SectionName
        {
            get { return _sectionName; }
            set { _sectionName = value; }
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
        [DataDesc(CName = "仪器编码", ShortCode = "EquipNo", Desc = "仪器编码", ContextType = SysDic.All, Length = 200)]
        public virtual string EquipNo
        {
            get { return _equipNo; }
            set { _equipNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "厂商", ShortCode = "FactoryName", Desc = "厂商", ContextType = SysDic.All, Length = 200)]
        public virtual string FactoryName
        {
            get { return _factoryName; }
            set { _factoryName = value; }
        }

        [DataMember]
        [DataDesc(CName = "出厂编号", ShortCode = "FactoryOutNo", Desc = "出厂编号", ContextType = SysDic.All, Length = 200)]
        public virtual string FactoryOutNo
        {
            get { return _factoryOutNo; }
            set { _factoryOutNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "放置区域", ShortCode = "StoreArea", Desc = "放置区域", ContextType = SysDic.All, Length = 200)]
        public virtual string StoreArea
        {
            get { return _storeArea; }
            set { _storeArea = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "启用日期", ShortCode = "EnableDate", Desc = "启用日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? EnableDate
        {
            get { return _enableDate; }
            set { _enableDate = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "校准效期", ShortCode = "CalibrateDate", Desc = "校准效期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? CalibrateDate
        {
            get { return _calibrateDate; }
            set { _calibrateDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "仪器模板表", ShortCode = "ETempletList", Desc = "仪器模板表")]
        public virtual IList<ETemplet> ETempletList
        {
            get
            {
                if (_eTempletList == null)
                {
                    _eTempletList = new List<ETemplet>();
                }
                return _eTempletList;
            }
            set { _eTempletList = value; }
        }

        [DataMember]
        [DataDesc(CName = "字典表", ShortCode = "EquipType", Desc = "字典表")]
        public virtual PDict EquipType
        {
            get { return _equipType; }
            set { _equipType = value; }
        }


        #endregion
    }
    #endregion
}