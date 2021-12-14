using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
    #region BloodBagRecordItem

    /// <summary>
    /// BloodBagRecordItem object for NHibernate mapped table 'Blood_BagRecordItem'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "血袋记录明细字典表", ClassCName = "BloodBagRecordItem", ShortCode = "BloodBagRecordItem", Desc = "血袋记录明细字典表")]
    public class BloodBagRecordItem : BaseEntity
    {
        #region Member Variables

        protected string _itemCode;
        protected string _itemName;
        protected string _sName;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected string _itemEditInfo;
        protected int _dispOrder;
        protected bool _isUse;
        protected DateTime? _dataUpdateTime;
        protected BloodBagRecordType _bloodBagRecordType;

        #endregion

        #region Constructors

        public BloodBagRecordItem() { }

        public BloodBagRecordItem(long labID, string itemCode, string itemName, string sName, string shortcode, string pinYinZiTou, string itemEditInfo, int dispOrder, bool isUse, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BloodBagRecordType bloodBagRecordType)
        {
            this._labID = labID;
            this._itemCode = itemCode;
            this._itemName = itemName;
            this._sName = sName;
            this._shortcode = shortcode;
            this._pinYinZiTou = pinYinZiTou;
            this._itemEditInfo = itemEditInfo;
            this._dispOrder = dispOrder;
            this._isUse = isUse;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._bloodBagRecordType = bloodBagRecordType;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "录入项编码", ShortCode = "ItemCode", Desc = "录入项编码", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "录入项名称", ShortCode = "CName", Desc = "录入项名称", ContextType = SysDic.All, Length = 50)]
        public virtual string CName
        {
            get { return _itemName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
                _itemName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 80)]
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
        [DataDesc(CName = "快捷码", ShortCode = "ShortCode", Desc = "快捷码", ContextType = SysDic.All, Length = 50)]
        public virtual string ShortCode
        {
            get { return _shortcode; }
            set
            {
                if (value != null && value.Length > 50)
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
        [DataDesc(CName = "辅助录入信息", ShortCode = "ItemEditInfo", Desc = "辅助录入信息", ContextType = SysDic.All, Length = 214748364)]
        public virtual string ItemEditInfo
        {
            get { return _itemEditInfo; }
            set
            {
                _itemEditInfo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "是否可用", ShortCode = "DispOrder", Desc = "是否可用", ContextType = SysDic.All, Length = 4)]
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
        [DataDesc(CName = "数据修改时间", ShortCode = "DataUpdateTime", Desc = "数据修改时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "血袋记录类型字典表", ShortCode = "BloodBagRecordType", Desc = "血袋记录类型字典表")]
        public virtual BloodBagRecordType BloodBagRecordType
        {
            get { return _bloodBagRecordType; }
            set { _bloodBagRecordType = value; }
        }

        #endregion
    }
    #endregion
}