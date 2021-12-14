using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
    #region ItemAllItem

    /// <summary>
    /// ItemAllItem object for NHibernate mapped table 'Item_AllItem'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "所有项目", ClassCName = "ItemAllItem", ShortCode = "ItemAllItem", Desc = "所有项目")]
    public class ItemAllItem : BaseEntity
    {
        #region Member Variables

        protected string _cName;
        protected string _eName;
        protected string _sName;
        protected int _itemType;
        protected string _unit;
        protected string _refRange;
        protected int _valueType;
        protected string _samplingRequire;
        protected string _clinicalSignificance;
        protected int _chargeType;
        protected decimal _itemCharge;
        protected string _comment;
        protected bool _isUse;
        protected int _dispOrder;
        protected string _useCode;
        protected string _standCode;
        protected string _deveCode;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected int _precision;
        protected DateTime? _dataUpdateTime;
        protected bool _isHisOrder;
        protected BSpecialty _bSpecialty;

        protected IList<GMGroupItem> _gMGroupItemList;
        protected IList<EPEquipItem> _ePEquipItemList;

        #endregion

        #region Constructors

        public ItemAllItem() { }

        public ItemAllItem(long labID, string cName, string eName, string sName, int itemType, string unit, string refRange, int valueType, string samplingRequire, string clinicalSignificance, int chargeType, decimal itemCharge, string comment, bool isUse, int dispOrder, string useCode, string standCode, string deveCode, string shortcode, string pinYinZiTou, int precision, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, bool isHisOrder, BSpecialty bSpecialty)
        {
            this._labID = labID;
            this._cName = cName;
            this._eName = eName;
            this._sName = sName;
            this._itemType = itemType;
            this._unit = unit;
            this._refRange = refRange;
            this._valueType = valueType;
            this._samplingRequire = samplingRequire;
            this._clinicalSignificance = clinicalSignificance;
            this._chargeType = chargeType;
            this._itemCharge = itemCharge;
            this._comment = comment;
            this._isUse = isUse;
            this._dispOrder = dispOrder;
            this._useCode = useCode;
            this._standCode = standCode;
            this._deveCode = deveCode;
            this._shortcode = shortcode;
            this._pinYinZiTou = pinYinZiTou;
            this._precision = precision;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._isHisOrder = isHisOrder;
            this._bSpecialty = bSpecialty;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "CName", Desc = "名称", ContextType = SysDic.All, Length = 100)]
        public virtual string CName
        {
            get { return _cName; }
            set
            {
                _cName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "英文名称", ShortCode = "EName", Desc = "英文名称", ContextType = SysDic.All, Length = 100)]
        public virtual string EName
        {
            get { return _eName; }
            set
            {
                _eName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 100)]
        public virtual string SName
        {
            get { return _sName; }
            set
            {
                _sName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "项目类型", ShortCode = "ItemType", Desc = "项目类型", ContextType = SysDic.All, Length = 4)]
        public virtual int ItemType
        {
            get { return _itemType; }
            set { _itemType = value; }
        }

        [DataMember]
        [DataDesc(CName = "结果单位", ShortCode = "Unit", Desc = "结果单位", ContextType = SysDic.All, Length = 50)]
        public virtual string Unit
        {
            get { return _unit; }
            set
            {
                _unit = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "默认参考范围", ShortCode = "RefRange", Desc = "默认参考范围", ContextType = SysDic.All, Length = 500)]
        public virtual string RefRange
        {
            get { return _refRange; }
            set
            {
                _refRange = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "结果类型", ShortCode = "ValueType", Desc = "结果类型", ContextType = SysDic.All, Length = 4)]
        public virtual int ValueType
        {
            get { return _valueType; }
            set { _valueType = value; }
        }

        [DataMember]
        [DataDesc(CName = "采样要求", ShortCode = "SamplingRequire", Desc = "采样要求", ContextType = SysDic.All, Length = 8000)]
        public virtual string SamplingRequire
        {
            get { return _samplingRequire; }
            set
            {
                _samplingRequire = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "临床意义", ShortCode = "ClinicalSignificance", Desc = "临床意义", ContextType = SysDic.All, Length = 8000)]
        public virtual string ClinicalSignificance
        {
            get { return _clinicalSignificance; }
            set
            {
                _clinicalSignificance = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "收费类型", ShortCode = "ChargeType", Desc = "收费类型", ContextType = SysDic.All, Length = 4)]
        public virtual int ChargeType
        {
            get { return _chargeType; }
            set { _chargeType = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "项目价格", ShortCode = "ItemCharge", Desc = "项目价格", ContextType = SysDic.All, Length = 9)]
        public virtual decimal ItemCharge
        {
            get { return _itemCharge; }
            set { _itemCharge = value; }
        }

        [DataMember]
        [DataDesc(CName = "描述", ShortCode = "Comment", Desc = "描述", ContextType = SysDic.All, Length = 8000)]
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
        [DataDesc(CName = "代码", ShortCode = "UseCode", Desc = "代码", ContextType = SysDic.All, Length = 100)]
        public virtual string UseCode
        {
            get { return _useCode; }
            set
            {
                _useCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "标准代码", ShortCode = "StandCode", Desc = "标准代码", ContextType = SysDic.All, Length = 100)]
        public virtual string StandCode
        {
            get { return _standCode; }
            set
            {
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
                _deveCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "快捷码", ShortCode = "Shortcode", Desc = "快捷码", ContextType = SysDic.All, Length = 100)]
        public virtual string Shortcode
        {
            get { return _shortcode; }
            set
            {
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
                _pinYinZiTou = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "精度", ShortCode = "Precision", Desc = "精度", ContextType = SysDic.All, Length = 4)]
        public virtual int Precision
        {
            get { return _precision; }
            set { _precision = value; }
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
        [DataDesc(CName = "是否是医嘱项目（开申请单）", ShortCode = "IsHisOrder", Desc = "是否是医嘱项目（开申请单）", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsHisOrder
        {
            get { return _isHisOrder; }
            set { _isHisOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "专业表", ShortCode = "BSpecialty", Desc = "专业表")]
        public virtual BSpecialty BSpecialty
        {
            get { return _bSpecialty; }
            set { _bSpecialty = value; }
        }

        
                
        [DataMember]
        [DataDesc(CName = "小组项目", ShortCode = "GMGroupItemList", Desc = "小组项目")]
        public virtual IList<GMGroupItem> GMGroupItemList
        {
            get
            {
                if (_gMGroupItemList == null)
                {
                    _gMGroupItemList = new List<GMGroupItem>();
                }
                return _gMGroupItemList;
            }
            set { _gMGroupItemList = value; }
        }

        [DataMember]
        [DataDesc(CName = "仪器项目关系表", ShortCode = "EPEquipItemList", Desc = "仪器项目关系表")]
        public virtual IList<EPEquipItem> EPEquipItemList
        {
            get
            {
                if (_ePEquipItemList == null)
                {
                    _ePEquipItemList = new List<EPEquipItem>();
                }
                return _ePEquipItemList;
            }
            set { _ePEquipItemList = value; }
        }

        
        #endregion
    }
    #endregion
}