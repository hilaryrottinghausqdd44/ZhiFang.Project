using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region BHostType

    /// <summary>
    /// BHostType object for NHibernate mapped table 'B_HostType'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "站点类型表", ClassCName = "BHostType", ShortCode = "BHostType", Desc = "站点类型表")]
    public class BHostType : BaseEntity
    {
        #region Member Variables

        protected string _cName;
        protected string _sName;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected string _hostTypeDesc;
        protected int _dispOrder;
        protected bool _isUse;
        protected DateTime? _dataUpdateTime;
        //protected IList<BHostTypePara> _bHostTypeParaList; 


        #endregion

        #region Constructors

        public BHostType() { }

        public BHostType(long labID, string cName, string sName, string shortcode, string pinYinZiTou, string hostTypeDesc, int dispOrder, bool isUse, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._cName = cName;
            this._sName = sName;
            this._shortcode = shortcode;
            this._pinYinZiTou = pinYinZiTou;
            this._hostTypeDesc = hostTypeDesc;
            this._dispOrder = dispOrder;
            this._isUse = isUse;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "CName", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string CName
        {
            get { return _cName; }
            set { _cName = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SName", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string SName
        {
            get { return _sName; }
            set { _sName = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Shortcode", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string Shortcode
        {
            get { return _shortcode; }
            set { _shortcode = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PinYinZiTou", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string PinYinZiTou
        {
            get { return _pinYinZiTou; }
            set { _pinYinZiTou = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HostTypeDesc", Desc = "", ContextType = SysDic.All, Length = 2000)]
        public virtual string HostTypeDesc
        {
            get { return _hostTypeDesc; }
            set { _hostTypeDesc = value; }
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

        //      [DataMember]
        //      [DataDesc(CName = "站点类型和参数关系", ShortCode = "BHostTypeParaList", Desc = "站点类型和参数关系")]
        //public virtual IList<BHostTypePara> BHostTypeParaList
        //{
        //	get
        //	{
        //		if (_bHostTypeParaList==null)
        //		{
        //			_bHostTypeParaList = new List<BHostTypePara>();
        //		}
        //		return _bHostTypeParaList;
        //	}
        //	set { _bHostTypeParaList = value; }
        //}


        #endregion
    }
    #endregion
}