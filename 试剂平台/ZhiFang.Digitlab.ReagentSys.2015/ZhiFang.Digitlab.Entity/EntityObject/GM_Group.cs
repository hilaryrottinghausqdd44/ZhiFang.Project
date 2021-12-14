using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
    #region GMGroup

    /// <summary>
    /// GMGroup object for NHibernate mapped table 'GM_Group'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "小组表", ClassCName = "GMGroup", ShortCode = "GMGroup", Desc = "小组表")]
    public class GMGroup : BaseEntity
    {
        #region Member Variables

        protected string _name;
        protected string _sName;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected int _isVirtualGroup;
        protected int _functionalTypesOfGroup;
        protected int _dispOrder;
        protected string _comment;
        protected bool _isUse;
        protected DateTime? _dataUpdateTime;
        protected HRDept _hRDept;
        protected BSpecialty _bSpecialty;
        protected GMGroupType _gMGroupType;
        protected IList<EPEquipItem> _ePEquipItemList;
        protected IList<GMGroupEquip> _gMGroupEquipList;
        protected IList<GMGroupItem> _gMGroupItemList;

        #endregion

        #region Constructors

        public GMGroup() { }

        public GMGroup(long labID, string name, string sName, string shortcode, string pinYinZiTou, int isVirtualGroup, int functionalTypesOfGroup, int dispOrder, string comment, bool isUse, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, HRDept hRDept, BSpecialty bSpecialty, GMGroupType gMGroupType)
        {
            this._labID = labID;
            this._name = name;
            this._sName = sName;
            this._shortcode = shortcode;
            this._pinYinZiTou = pinYinZiTou;
            this._isVirtualGroup = isVirtualGroup;
            this._functionalTypesOfGroup = functionalTypesOfGroup;
            this._dispOrder = dispOrder;
            this._comment = comment;
            this._isUse = isUse;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._hRDept = hRDept;
            this._bSpecialty = bSpecialty;
            this._gMGroupType = gMGroupType;
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
        [DataDesc(CName = "是否虚拟小组", ShortCode = "IsVirtualGroup", Desc = "是否虚拟小组", ContextType = SysDic.All, Length = 4)]
        public virtual int IsVirtualGroup
        {
            get { return _isVirtualGroup; }
            set { _isVirtualGroup = value; }
        }

        [DataMember]
        [DataDesc(CName = "小组专业功能类型", ShortCode = "FunctionalTypesOfGroup", Desc = "小组专业功能类型", ContextType = SysDic.All, Length = 4)]
        public virtual int FunctionalTypesOfGroup
        {
            get { return _functionalTypesOfGroup; }
            set { _functionalTypesOfGroup = value; }
        }

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Comment", Desc = "备注", ContextType = SysDic.All, Length = 8000)]
        public virtual string Comment
        {
            get { return _comment; }
            set
            {
                if (value != null && value.Length > 8000)
                    throw new ArgumentOutOfRangeException("Invalid value for Comment", value, value.ToString());
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "部门", ShortCode = "HRDept", Desc = "部门")]
        public virtual HRDept HRDept
        {
            get { return _hRDept; }
            set { _hRDept = value; }
        }

        [DataMember]
        [DataDesc(CName = "专业表", ShortCode = "BSpecialty", Desc = "专业表")]
        public virtual BSpecialty BSpecialty
        {
            get { return _bSpecialty; }
            set { _bSpecialty = value; }
        }

        [DataMember]
        [DataDesc(CName = "小组类型", ShortCode = "GMGroupType", Desc = "小组类型")]
        public virtual GMGroupType GMGroupType
        {
            get { return _gMGroupType; }
            set { _gMGroupType = value; }
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

        [DataMember]
        [DataDesc(CName = "小组仪器", ShortCode = "GMGroupEquipList", Desc = "小组仪器")]
        public virtual IList<GMGroupEquip> GMGroupEquipList
        {
            get
            {
                if (_gMGroupEquipList == null)
                {
                    _gMGroupEquipList = new List<GMGroupEquip>();
                }
                return _gMGroupEquipList;
            }
            set { _gMGroupEquipList = value; }
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

        
        #endregion
    }
    #endregion
}