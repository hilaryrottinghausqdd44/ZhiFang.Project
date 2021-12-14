using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.WebAssist
{
    #region SCRecordTypeItem

    /// <summary>
    /// SCRecordTypeItem object for NHibernate mapped table 'SC_RecordTypeItem'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "SCRecordTypeItem", ShortCode = "SCRecordTypeItem", Desc = "")]
    public class SCRecordTypeItem : BaseEntity
    {
        #region Member Variables

        protected string _itemCode;
        protected string _cName;
        protected string _sName;
        protected string _shortCode;
        protected string _pinYinZiTou;
        protected string _itemEditInfo;
        protected string _description;
        protected string _bGColor;
        protected string _defaultValue;
        protected string _itemXType;
        protected string _itemUnit;
        protected string _memo;
        protected int _dispOrder;
        protected bool _isUse;
        protected DateTime? _dataUpdateTime;
        protected SCRecordType _sCRecordType;

        #endregion

        #region Constructors

        public SCRecordTypeItem() { }

        public SCRecordTypeItem(long labID, string itemCode, string itemName, string sName, string shortCode, string pinYinZiTou, string itemEditInfo, string defaultValue, string description, string memo, int dispOrder, bool isUse, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, SCRecordType sCRecordType)
        {
            this._labID = labID;
            this._itemCode = itemCode;
            this._cName = itemName;
            this._sName = sName;
            this._shortCode = shortCode;
            this._pinYinZiTou = pinYinZiTou;
            this._itemEditInfo = itemEditInfo;
            this._defaultValue = defaultValue;
            this._description = description;
            this._memo = memo;
            this._dispOrder = dispOrder;
            this._isUse = isUse;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._sCRecordType = sCRecordType;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [DataDesc(CName = "BGColor", ShortCode = "BGColor", Desc = "BGColor", ContextType = SysDic.All, Length = 60)]
        public virtual string BGColor
        {
            get { return _bGColor; }
            set
            {
                _bGColor = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "ItemXType", ShortCode = "ItemXType", Desc = "ItemXType", ContextType = SysDic.All, Length = 50)]
        public virtual string ItemXType
        {
            get { return _itemXType; }
            set
            {
                _itemXType = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "ItemUnit", ShortCode = "ItemUnit", Desc = "ItemUnit", ContextType = SysDic.All, Length = 50)]
        public virtual string ItemUnit
        {
            get { return _itemUnit; }
            set
            {
                _itemUnit = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "DefaultValue", ShortCode = "DefaultValue", Desc = "DefaultValue", ContextType = SysDic.All, Length = 50)]
        public virtual string DefaultValue
        {
            get { return _defaultValue; }
            set
            {
                _defaultValue = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "ItemCode", ShortCode = "ItemCode", Desc = "ItemCode", ContextType = SysDic.All, Length = 50)]
        public virtual string ItemCode
        {
            get { return _itemCode; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ItemCode", value, value.ToString());
                _itemCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "CName", ShortCode = "CName", Desc = "CName", ContextType = SysDic.All, Length = 50)]
        public virtual string CName
        {
            get { return _cName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
                _cName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "SName", ShortCode = "SName", Desc = "SName", ContextType = SysDic.All, Length = 80)]
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
        [DataDesc(CName = "ShortCode", ShortCode = "ShortCode", Desc = "ShortCode", ContextType = SysDic.All, Length = 50)]
        public virtual string ShortCode
        {
            get { return _shortCode; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ShortCode", value, value.ToString());
                _shortCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "PinYinZiTou", ShortCode = "PinYinZiTou", Desc = "PinYinZiTou", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "ItemEditInfo", ShortCode = "ItemEditInfo", Desc = "ItemEditInfo", ContextType = SysDic.All, Length = int.MaxValue)]
        public virtual string ItemEditInfo
        {
            get { return _itemEditInfo; }
            set
            {
                _itemEditInfo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "Description", ShortCode = "Description", Desc = "Description", ContextType = SysDic.All, Length = int.MaxValue)]
        public virtual string Description
        {
            get { return _description; }
            set
            {
                _description = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "Memo", ShortCode = "Memo", Desc = "Memo", ContextType = SysDic.All, Length = int.MaxValue)]
        public virtual string Memo
        {
            get { return _memo; }
            set
            {
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
        [DataDesc(CName = "", ShortCode = "IsUse", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
        {
            get { return _isUse; }
            set { _isUse = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SCRecordType", Desc = "")]
        public virtual SCRecordType SCRecordType
        {
            get { return _sCRecordType; }
            set { _sCRecordType = value; }
        }
        #endregion

        #region 自定义属性
        [DataMember]
        [DataDesc(CName = "TestItemCName", ShortCode = "TestItemCName", Desc = "TestItemCName", ContextType = SysDic.All, Length = 120)]
        public virtual string TestItemCName { get; set; }

        #endregion


    }
    #endregion

}