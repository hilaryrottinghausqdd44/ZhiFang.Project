using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBDict

    /// <summary>
    /// LBDict object for NHibernate mapped table 'LB_Dict'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "LBDict", ShortCode = "LBDict", Desc = "")]
    public class LBDict : BaseEntity
    {
        #region Member Variables

        protected string _dictType;
        protected string _dictTypeName;
        protected string _cName;
        protected string _dictCode;
        protected string _eName;
        protected string _sName;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected string _dictInfo;
        protected int _ilevel;
        protected string _colorValue;
        protected string _colorDefault;
        protected string _dictValue;
        protected string _dictValueDefault;
        protected string _comment;
        protected bool _isUse;
        protected bool _isDefault;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;


        #endregion

        #region Constructors

        public LBDict() { }

        public LBDict(long labID, string dictType, string dictTypeName, string cName, string dictCode, string eName, string sName, string shortcode, string pinYinZiTou, string dictInfo, int ilevel, string colorValue, string colorDefault, string dictValue, string dictValueDefault, string comment, bool isUse, bool isDefault, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._dictType = dictType;
            this._dictTypeName = dictTypeName;
            this._cName = cName;
            this._dictCode = dictCode;
            this._eName = eName;
            this._sName = sName;
            this._shortcode = shortcode;
            this._pinYinZiTou = pinYinZiTou;
            this._dictInfo = dictInfo;
            this._ilevel = ilevel;
            this._colorValue = colorValue;
            this._colorDefault = colorDefault;
            this._dictValue = dictValue;
            this._dictValueDefault = dictValueDefault;
            this._comment = comment;
            this._isUse = isUse;
            this._isDefault = isDefault;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "DictType", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string DictType
        {
            get { return _dictType; }
            set { _dictType = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DictTypeName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string DictTypeName
        {
            get { return _dictTypeName; }
            set { _dictTypeName = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string CName
        {
            get { return _cName; }
            set { _cName = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DictCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string DictCode
        {
            get { return _dictCode; }
            set { _dictCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string EName
        {
            get { return _eName; }
            set { _eName = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string SName
        {
            get { return _sName; }
            set { _sName = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Shortcode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Shortcode
        {
            get { return _shortcode; }
            set { _shortcode = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PinYinZiTou", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string PinYinZiTou
        {
            get { return _pinYinZiTou; }
            set { _pinYinZiTou = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DictInfo", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string DictInfo
        {
            get { return _dictInfo; }
            set { _dictInfo = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Ilevel", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Ilevel
        {
            get { return _ilevel; }
            set { _ilevel = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ColorValue", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ColorValue
        {
            get { return _colorValue; }
            set { _colorValue = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ColorDefault", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ColorDefault
        {
            get { return _colorDefault; }
            set { _colorDefault = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DictValue", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string DictValue
        {
            get { return _dictValue; }
            set { _dictValue = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DictValueDefault", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string DictValueDefault
        {
            get { return _dictValueDefault; }
            set { _dictValueDefault = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Comment", Desc = "", ContextType = SysDic.All, Length = 16)]
        public virtual string Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsUse", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
        {
            get { return _isUse; }
            set { _isUse = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsDefault", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsDefault
        {
            get { return _isDefault; }
            set { _isDefault = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }


        #endregion
    }
    #endregion
}