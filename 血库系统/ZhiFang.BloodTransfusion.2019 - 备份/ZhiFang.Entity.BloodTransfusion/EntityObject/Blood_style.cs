using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
    #region Bloodstyle

    /// <summary>
    /// Bloodstyle object for NHibernate mapped table 'blood_style'.
    /// BloodNo为主键
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "Bloodstyle", ShortCode = "Bloodstyle", Desc = "")]
    public class Bloodstyle : BaseEntityServiceByInt
    {
        #region Member Variables

        protected int? _bloodNo;

        protected string _cName;
        protected string _isMakeBlood;
        protected string _hisDispCode;
        protected string _bisDispCode;
        protected string _storeDays;
        protected string _shortCode;
        protected string _shortName;
        protected int _dispOrder;
        protected string _visible;
        //protected string _bloodclass;
        protected string _warnDays;
        protected string _warnUnit;
        protected string _storeUnit;
        protected string _regAllow;
        protected int _storeCondNo;
        protected double? _bloodScale;
        protected int _ahead;
        protected string _aheadUnit;
        protected string _hemolysisTime;
        protected string _hemolysisUnit;
        protected string _isprocess;

        protected string _sName;
        protected string _pinYinZiTou;

        protected BloodClass _bloodClass;
        protected BloodUnit _bloodUnit;
        #endregion

        #region Constructors

        public Bloodstyle() { }

        public Bloodstyle(string bloodName, string isMakeBlood, string hisDispCode, string bisDispCode, string storeDays, string shortCode, string shortName, int dispOrder, string visible, string warnDays, string warnUnit, string storeUnit, string regAllow, int storeCondNo, double bloodScale, int ahead, string aheadUnit, string hemolysisTime, string hemolysisUnit,string isprocess, long labID, DateTime dataAddTime, byte[] dataTimeStamp)
        {
            this._cName = bloodName;
            this._isMakeBlood = isMakeBlood;
            this._hisDispCode = hisDispCode;
            this._bisDispCode = bisDispCode;
            this._storeDays = storeDays;
            this._shortCode = shortCode;
            this._shortName = shortName;
            this._dispOrder = dispOrder;
            this._visible = visible;
            //this._bloodclass = bloodclass;
            this._warnDays = warnDays;
            this._warnUnit = warnUnit;
            this._storeUnit = storeUnit;
            this._regAllow = regAllow;
            //this._bloodUnitNo = bloodUnitNo;
            this._storeCondNo = storeCondNo;
            this._bloodScale = bloodScale;
            this._ahead = ahead;
            this._aheadUnit = aheadUnit;
            this._hemolysisTime = hemolysisTime;
            this._hemolysisUnit = hemolysisUnit;
            this._isprocess = isprocess;
            this._labID = labID;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties
        [DataMember]
        [DataDesc(CName = "BloodClass", ShortCode = "BloodClass", Desc = "BloodClass")]
        public virtual BloodClass BloodClass
        {
            get { return _bloodClass; }
            set { _bloodClass = value; }
        }
        [DataMember]
        [DataDesc(CName = "BloodUnit", ShortCode = "BloodUnit", Desc = "BloodUnit")]
        public virtual BloodUnit BloodUnit
        {
            get { return _bloodUnit; }
            set { _bloodUnit = value; }
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
        //[DataMember]
        //[DataDesc(CName = "BloodNo", ShortCode = "BloodNo", Desc = "主键", ContextType = SysDic.All)]
        //public virtual int? BloodNo
        //{
        //    get
        //    {
        //        if (!_bloodNo.HasValue || _bloodNo < 0)
        //            _bloodNo = ZhiFang.Common.Public.GUIDHelp.GetGUIDInt();
        //        return _bloodNo;
        //    }
        //    set { _bloodNo = value; }
        //}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CName", Desc = "", ContextType = SysDic.All, Length = 30)]
        public virtual string CName
        {
            get { return _cName; }
            set
            {
                if (value != null && value.Length > 30)
                    throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
                _cName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsMakeBlood", Desc = "", ContextType = SysDic.All, Length = 2)]
        public virtual string IsMakeBlood
        {
            get { return _isMakeBlood; }
            set
            {
                if (value != null && value.Length > 2)
                    throw new ArgumentOutOfRangeException("Invalid value for IsMakeBlood", value, value.ToString());
                _isMakeBlood = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HisDispCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string HisDispCode
        {
            get { return _hisDispCode; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for HisDispCode", value, value.ToString());
                _hisDispCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BisDispCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string BisDispCode
        {
            get { return _bisDispCode; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for BisDispCode", value, value.ToString());
                _bisDispCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "StoreDays", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string StoreDays
        {
            get { return _storeDays; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for StoreDays", value, value.ToString());
                _storeDays = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ShortCode", Desc = "", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "", ShortCode = "ShortName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ShortName
        {
            get { return _shortName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ShortName", value, value.ToString());
                _shortName = value;
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
        [DataDesc(CName = "", ShortCode = "Visible", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Visible
        {
            get { return _visible; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Visible", value, value.ToString());
                _visible = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "WarnDays", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string WarnDays
        {
            get { return _warnDays; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for WarnDays", value, value.ToString());
                _warnDays = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "WarnUnit", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string WarnUnit
        {
            get { return _warnUnit; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for WarnUnit", value, value.ToString());
                _warnUnit = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "StoreUnit", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string StoreUnit
        {
            get { return _storeUnit; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for StoreUnit", value, value.ToString());
                _storeUnit = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "RegAllow", Desc = "", ContextType = SysDic.All, Length = 2)]
        public virtual string RegAllow
        {
            get { return _regAllow; }
            set
            {
                if (value != null && value.Length > 2)
                    throw new ArgumentOutOfRangeException("Invalid value for RegAllow", value, value.ToString());
                _regAllow = value;
            }
        }


        [DataMember]
        [DataDesc(CName = "", ShortCode = "StoreCondNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int StoreCondNo
        {
            get { return _storeCondNo; }
            set { _storeCondNo = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BloodScale", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double? BloodScale
        {
            get { return _bloodScale; }
            set { _bloodScale = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Ahead", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Ahead
        {
            get { return _ahead; }
            set { _ahead = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AheadUnit", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string AheadUnit
        {
            get { return _aheadUnit; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for AheadUnit", value, value.ToString());
                _aheadUnit = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HemolysisTime", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string HemolysisTime
        {
            get { return _hemolysisTime; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for HemolysisTime", value, value.ToString());
                _hemolysisTime = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HemolysisUnit", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string HemolysisUnit
        {
            get { return _hemolysisUnit; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for HemolysisUnit", value, value.ToString());
                _hemolysisUnit = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Isprocess", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string Isprocess
        {
            get { return _isprocess; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for Isprocess", value, value.ToString());
                _isprocess = value;
            }
        }


        #endregion

        #region 自定义属性
        protected string _bReqCount;
        protected string _bloodUnitCName;
        protected string _bloodClassCName;

        [DataMember]
        [DataDesc(CName = "血制品申请量", ShortCode = "BReqCount", Desc = "血制品申请量", ContextType = SysDic.All, Length = 20)]
        public virtual string BReqCount
        {
            get { return _bReqCount; }
            set
            {
                _bReqCount = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "血制品分类名称", ShortCode = "BloodClassCName", Desc = "血制品分类名称", ContextType = SysDic.All, Length = 50)]
        public virtual string BloodClassCName
        {
            get { return _bloodClassCName; }
            set
            {
                _bloodClassCName = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "血制品单位名称", ShortCode = "BloodUnitCName", Desc = "血制品单位名称", ContextType = SysDic.All, Length = 50)]
        public virtual string BloodUnitCName
        {
            get { return _bloodUnitCName; }
            set
            {
                _bloodUnitCName = value;
            }
        }

        private void SetBloodstyle(Bloodstyle bloodstyle)
        {
            this._id = bloodstyle.Id;
            this._cName = bloodstyle.CName;
            this._isMakeBlood = bloodstyle.IsMakeBlood;
            this._hisDispCode = bloodstyle.HisDispCode;
            this._bisDispCode = bloodstyle.HisDispCode;
            this._storeDays = bloodstyle.StoreDays;
            this._shortCode = bloodstyle.ShortCode;
            this._shortName = bloodstyle.ShortName;
            this._dispOrder = bloodstyle.DispOrder;
            this._visible = bloodstyle.Visible;
            //this._bloodclass = bloodstyle.Bloodclass;
            this._warnDays = bloodstyle.WarnDays;
            this._warnUnit = bloodstyle.WarnUnit;
            this._storeUnit = bloodstyle.StoreUnit;
            this._regAllow = bloodstyle.RegAllow;
            //this._bloodUnitNo = bloodstyle.BloodUnitNo;
            this._storeCondNo = bloodstyle.StoreCondNo;
            this._bloodScale = bloodstyle.BloodScale;
            this._ahead = bloodstyle.Ahead;
            this._aheadUnit = bloodstyle.AheadUnit;
            this._hemolysisTime = bloodstyle.HemolysisTime;
            this._hemolysisUnit = bloodstyle.HemolysisUnit;
            this._isprocess = bloodstyle.Isprocess;
            this._labID = bloodstyle.LabID;
            this._dataAddTime = bloodstyle.DataAddTime;
            this._dataTimeStamp = bloodstyle.DataTimeStamp;
        }
        public Bloodstyle(Bloodstyle bloodstyle, BloodClass bloodclass, BloodUnit bloodunit)
        {
            SetBloodstyle(bloodstyle);

            this._bloodClass = bloodclass;
            this._bloodUnit = bloodunit;

            this._bloodClassCName = bloodclass.CName;
            this._bloodUnitCName = bloodunit.CName;
        }

        #endregion

    }
    #endregion
}