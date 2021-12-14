using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBSpecialty

    /// <summary>
    /// LBSpecialty object for NHibernate mapped table 'LB_Specialty'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "LBSpecialty", ShortCode = "LBSpecialty", Desc = "")]
    public class LBSpecialty : BaseEntity
    {
        #region Member Variables

        protected string _cName;
        protected string _eName;
        protected string _sName;
        protected string _useCode;
        protected string _standCode;
        protected string _deveCode;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected string _comment;
        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected IList<LBSection> _lBSectionList;


        #endregion

        #region Constructors

        public LBSpecialty() { }

        public LBSpecialty(string cName, string eName, string sName, string useCode, string standCode, string deveCode, string shortcode, string pinYinZiTou, string comment, bool isUse, int dispOrder, long labID, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
        {
            this._cName = cName;
            this._eName = eName;
            this._sName = sName;
            this._useCode = useCode;
            this._standCode = standCode;
            this._deveCode = deveCode;
            this._shortcode = shortcode;
            this._pinYinZiTou = pinYinZiTou;
            this._comment = comment;
            this._isUse = isUse;
            this._dispOrder = dispOrder;
            this._labID = labID;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "CName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string CName
        {
            get { return _cName; }
            set { _cName = value; }
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
        [DataDesc(CName = "", ShortCode = "UseCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string UseCode
        {
            get { return _useCode; }
            set { _useCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "StandCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string StandCode
        {
            get { return _standCode; }
            set { _standCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DeveCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string DeveCode
        {
            get { return _deveCode; }
            set { _deveCode = value; }
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

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LBSectionList", Desc = "")]
        public virtual IList<LBSection> LBSectionList
        {
            get
            {
                if (_lBSectionList == null)
                {
                    _lBSectionList = new List<LBSection>();
                }
                return _lBSectionList;
            }
            set { _lBSectionList = value; }
        }


        #endregion
    }
    #endregion
}