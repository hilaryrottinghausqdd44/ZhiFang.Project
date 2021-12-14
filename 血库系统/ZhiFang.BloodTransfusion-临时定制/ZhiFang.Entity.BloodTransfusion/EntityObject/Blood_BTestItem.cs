using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
    #region BloodBTestItem

    /// <summary>
    /// BloodBTestItem object for NHibernate mapped table 'Blood_BTestItem'.
    /// BTestItemNo为主键
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BloodBTestItem", ShortCode = "BloodBTestItem", Desc = "")]
    public class BloodBTestItem : BaseEntityServiceByInt
    {
        #region Member Variables

        //protected int? _bTestItemNo;
        protected string _cName;
        protected string _iSgroup;
        protected string _bUnitNo;
        protected string _liscode1;
        protected string _liscode2;
        protected string _liscode3;
        protected string _eName;
        protected int _visible;
        protected int _dispOrder;
        protected string _shortcode;
        protected string _hisOrderCode;
        protected string _reference;
        protected bool _isResultItem;
        protected bool _isPreTrransfusionEvaluationItem;

        protected string _sName;
        protected string _pinYinZiTou;
        #endregion

        #region Constructors

        public BloodBTestItem() { }

        public BloodBTestItem(string cName, string iSgroup, string bUnitNo, string liscode1, string liscode2, string liscode3, string eName, int visible, int dispOrder, string shortcode, string hisOrderCode, string reference, long labID, DateTime dataAddTime, byte[] dataTimeStamp,bool isPreTrransfusionEvaluationItem)
        {
            this._cName = cName;
            this._iSgroup = iSgroup;
            this._bUnitNo = bUnitNo;
            this._liscode1 = liscode1;
            this._liscode2 = liscode2;
            this._liscode3 = liscode3;
            this._eName = eName;
            this._visible = visible;
            this._dispOrder = dispOrder;
            this._shortcode = shortcode;
            this._hisOrderCode = hisOrderCode;
            this._reference = reference;
            this._labID = labID;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
            this._isPreTrransfusionEvaluationItem = isPreTrransfusionEvaluationItem;
        }

        #endregion

        #region Public Properties

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
        [DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 50)]
        public virtual string SName
        {
            get { return _sName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for SName", value, value.ToString());
                _sName = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "是否为输血前评估项", ShortCode = "IsPreTrransfusionEvaluationItem", Desc = "是否为输血前评估项", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsPreTrransfusionEvaluationItem
        {
            get { return _isPreTrransfusionEvaluationItem; }
            set { _isPreTrransfusionEvaluationItem = value; }
        }
        [DataMember]
        [DataDesc(CName = "是否为录入结果项", ShortCode = "IsResultItem", Desc = "是否为录入结果项", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsResultItem
        {
            get { return _isResultItem; }
            set { _isResultItem = value; }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "CName", Desc = "", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "", ShortCode = "ISgroup", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string ISgroup
        {
            get { return _iSgroup; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for ISgroup", value, value.ToString());
                _iSgroup = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BUnitNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BUnitNo
        {
            get { return _bUnitNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for BUnitNo", value, value.ToString());
                _bUnitNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Liscode1", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Liscode1
        {
            get { return _liscode1; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Liscode1", value, value.ToString());
                _liscode1 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Liscode2", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Liscode2
        {
            get { return _liscode2; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Liscode2", value, value.ToString());
                _liscode2 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Liscode3", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Liscode3
        {
            get { return _liscode3; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Liscode3", value, value.ToString());
                _liscode3 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EName", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string EName
        {
            get { return _eName; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for EName", value, value.ToString());
                _eName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Visible", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Shortcode", Desc = "", ContextType = SysDic.All, Length = 20)]
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
        [DataDesc(CName = "", ShortCode = "HisOrderCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string HisOrderCode
        {
            get { return _hisOrderCode; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for HisOrderCode", value, value.ToString());
                _hisOrderCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Reference", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string Reference
        {
            get { return _reference; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for Reference", value, value.ToString());
                _reference = value;
            }
        }


        #endregion

        #region 自定义属性
        protected BloodBUnit _bloodBUnit;
        [DataMember]
        [DataDesc(CName = "单位", ShortCode = "BloodBUnit", Desc = "单位")]
        public virtual BloodBUnit BloodBUnit
        {
            get { return _bloodBUnit; }
            set { _bloodBUnit = value; }
        }
        #endregion

    }
    #endregion
}