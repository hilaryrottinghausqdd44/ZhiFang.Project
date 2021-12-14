using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
    #region BloodBInItem

    /// <summary>
    /// BloodBInItem object for NHibernate mapped table 'Blood_BInItem'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BloodBInItem", ShortCode = "BloodBInItem", Desc = "")]
    public class BloodBInItem : BaseEntityServiceByString
    {
        #region Member Variables

        //protected string _bInFormID;
        protected string _bBagCode;
        protected string _pcode;
        protected string _aBORHCode;
        protected string _invalidCode;
        protected string _collectCode;
        protected string _allCode;
        protected string _bBagExCode;
        // protected int _bloodABONo;
        protected DateTime? _invalidDate;
        protected DateTime? _collectDate;
        //protected int _bloodUnitNo;
        protected double _bcount;
        protected double _price;
        protected int _checkFlag;
        protected string _memo;
        protected string _zX1;
        protected string _zX2;
        protected string _zX3;
        protected DateTime? _bInDate;
        protected string _bINo;
        protected int _bloodNo;
        protected string _iceboxNo;
        protected string _bframeNo;
        protected int _storeCondNo;
        protected string _bBankNo;
        protected int _isScanCheck;
        protected int _bReaNo;
        protected string _b3Code;
        protected int _isRepeat;
        protected string _sUserID;
        protected string _inTypeNo;
        protected string _bCCode;
        //protected string _bCNo;
        protected string _bankBloodName;
        protected string _yqCode;
        protected string _bloodoC;
        protected int _oKNo;
        protected string _iFlag1;
        protected string _iFlag2;
        protected string _iFlag3;
        protected string _iFlag4;
        protected string _iFlag5;
        protected int _dispOrder;
        protected bool _visible;

        protected BloodBInForm _bloodBInForm;
        protected BloodClass _bloodClass;
        protected BloodBUnit _bloodBUnit;
        protected Bloodstyle _bloodstyle;
        protected BloodABO _bloodABO;

        #endregion

        #region Constructors

        public BloodBInItem() { }

        public BloodBInItem(BloodBInForm bloodBInForm, BloodClass bloodClass, BloodBUnit bloodBUnit,Bloodstyle bloodstyle, BloodABO bloodABO, string bBagCode, string pcode, string aBORHCode, string invalidCode, string collectCode, string allCode, string bBagExCode, DateTime invalidDate, DateTime collectDate,  double bcount, double price, int checkFlag, string memo, string zX1, string zX2, string zX3, DateTime bInDate, string bINo, int bloodNo, string iceboxNo, string bframeNo, int storeCondNo, string bBankNo, int isScanCheck, int bReaNo, string b3Code, int isRepeat, string sUserID, string inTypeNo, string bCCode, string bankBloodName, string yqCode, string bloodoC, int oKNo, string iFlag1, string iFlag2, string iFlag3, string iFlag4, string iFlag5, long labID, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp, bool visible)
        {
            this._bloodBInForm = bloodBInForm;
            this._bloodClass = bloodClass;
            this._bloodBUnit = bloodBUnit;
            this._bloodstyle = bloodstyle;
            this._bloodABO = bloodABO;

            this._bBagCode = bBagCode;
            this._pcode = pcode;
            this._aBORHCode = aBORHCode;
            this._invalidCode = invalidCode;
            this._collectCode = collectCode;
            this._allCode = allCode;
            this._bBagExCode = bBagExCode;
            this._bloodstyle = bloodstyle;
            this._invalidDate = invalidDate;
            this._collectDate = collectDate;
            //this._bloodUnitNo = bloodUnitNo;
            this._bcount = bcount;
            this._price = price;
            this._checkFlag = checkFlag;
            this._memo = memo;
            this._zX1 = zX1;
            this._zX2 = zX2;
            this._zX3 = zX3;
            this._bInDate = bInDate;
            this._bINo = bINo;
            this._bloodNo = bloodNo;
            this._iceboxNo = iceboxNo;
            this._bframeNo = bframeNo;
            this._storeCondNo = storeCondNo;
            this._bBankNo = bBankNo;
            this._isScanCheck = isScanCheck;
            this._bReaNo = bReaNo;
            this._b3Code = b3Code;
            this._isRepeat = isRepeat;
            this._sUserID = sUserID;
            this._inTypeNo = inTypeNo;
            this._bCCode = bCCode;
            this._bankBloodName = bankBloodName;
            this._yqCode = yqCode;
            this._bloodoC = bloodoC;
            this._oKNo = oKNo;
            this._iFlag1 = iFlag1;
            this._iFlag2 = iFlag2;
            this._iFlag3 = iFlag3;
            this._iFlag4 = iFlag4;
            this._iFlag5 = iFlag5;
            this._labID = labID;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
            this._visible = visible;
        }

        #endregion

        #region Public Properties
        [DataMember]
        [DataDesc(CName = "BloodBInForm", ShortCode = "BloodBInForm", Desc = "BloodBInForm")]
        public virtual BloodBInForm BloodBInForm
        {
            get { return _bloodBInForm; }
            set { _bloodBInForm = value; }
        }

        [DataMember]
        [DataDesc(CName = "Bloodstyle", ShortCode = "Bloodstyle", Desc = "Bloodstyle")]
        public virtual Bloodstyle Bloodstyle
        {
            get { return _bloodstyle; }
            set { _bloodstyle = value; }
        }
        [DataMember]
        [DataDesc(CName = "BloodBUnit", ShortCode = "BloodBUnit", Desc = "BloodBUnit")]
        public virtual BloodBUnit BloodBUnit
        {
            get { return _bloodBUnit; }
            set { _bloodBUnit = value; }
        }
        [DataMember]
        [DataDesc(CName = "BloodABO", ShortCode = "BloodABO", Desc = "BloodABO")]
        public virtual BloodABO BloodABO
        {
            get { return _bloodABO; }
            set { _bloodABO = value; }
        }

        [DataMember]
        [DataDesc(CName = "BloodClass", ShortCode = "BloodClass", Desc = "BloodClass")]
        public virtual BloodClass BloodClass
        {
            get { return _bloodClass; }
            set { _bloodClass = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BBagCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string BBagCode
        {
            get { return _bBagCode; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for BBagCode", value, value.ToString());
                _bBagCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Pcode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Pcode
        {
            get { return _pcode; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Pcode", value, value.ToString());
                _pcode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ABORHCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ABORHCode
        {
            get { return _aBORHCode; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ABORHCode", value, value.ToString());
                _aBORHCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "InvalidCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string InvalidCode
        {
            get { return _invalidCode; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for InvalidCode", value, value.ToString());
                _invalidCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CollectCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string CollectCode
        {
            get { return _collectCode; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for CollectCode", value, value.ToString());
                _collectCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AllCode", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string AllCode
        {
            get { return _allCode; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for AllCode", value, value.ToString());
                _allCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BBagExCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BBagExCode
        {
            get { return _bBagExCode; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for BBagExCode", value, value.ToString());
                _bBagExCode = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "InvalidDate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? InvalidDate
        {
            get { return _invalidDate; }
            set { _invalidDate = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CollectDate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? CollectDate
        {
            get { return _collectDate; }
            set { _collectDate = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Bcount", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double Bcount
        {
            get { return _bcount; }
            set { _bcount = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Price", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double Price
        {
            get { return _price; }
            set { _price = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CheckFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int CheckFlag
        {
            get { return _checkFlag; }
            set { _checkFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Memo", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string Memo
        {
            get { return _memo; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for Memo", value, value.ToString());
                _memo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZX1", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX1
        {
            get { return _zX1; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ZX1", value, value.ToString());
                _zX1 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZX2", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX2
        {
            get { return _zX2; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ZX2", value, value.ToString());
                _zX2 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZX3", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX3
        {
            get { return _zX3; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ZX3", value, value.ToString());
                _zX3 = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BInDate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? BInDate
        {
            get { return _bInDate; }
            set { _bInDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BINo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BINo
        {
            get { return _bINo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for BINo", value, value.ToString());
                _bINo = value;
            }
        }

        //      [DataMember]
        //      [DataDesc(CName = "", ShortCode = "BloodNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        //      public virtual int BloodNo
        //{
        //	get { return _bloodNo; }
        //	set { _bloodNo = value; }
        //}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IceboxNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string IceboxNo
        {
            get { return _iceboxNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for IceboxNo", value, value.ToString());
                _iceboxNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BframeNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BframeNo
        {
            get { return _bframeNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for BframeNo", value, value.ToString());
                _bframeNo = value;
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
        [DataDesc(CName = "", ShortCode = "BBankNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BBankNo
        {
            get { return _bBankNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for BBankNo", value, value.ToString());
                _bBankNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsScanCheck", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsScanCheck
        {
            get { return _isScanCheck; }
            set { _isScanCheck = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BReaNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int BReaNo
        {
            get { return _bReaNo; }
            set { _bReaNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "B3Code", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string B3Code
        {
            get { return _b3Code; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for B3Code", value, value.ToString());
                _b3Code = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsRepeat", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsRepeat
        {
            get { return _isRepeat; }
            set { _isRepeat = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SUserID", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string SUserID
        {
            get { return _sUserID; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for SUserID", value, value.ToString());
                _sUserID = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "InTypeNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string InTypeNo
        {
            get { return _inTypeNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for InTypeNo", value, value.ToString());
                _inTypeNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BCCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BCCode
        {
            get { return _bCCode; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for BCCode", value, value.ToString());
                _bCCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BankBloodName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string BankBloodName
        {
            get { return _bankBloodName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for BankBloodName", value, value.ToString());
                _bankBloodName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "YqCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string YqCode
        {
            get { return _yqCode; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for YqCode", value, value.ToString());
                _yqCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BloodoC", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string BloodoC
        {
            get { return _bloodoC; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for BloodoC", value, value.ToString());
                _bloodoC = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OKNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int OKNo
        {
            get { return _oKNo; }
            set { _oKNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IFlag1", Desc = "", ContextType = SysDic.All, Length = 5)]
        public virtual string IFlag1
        {
            get { return _iFlag1; }
            set
            {
                if (value != null && value.Length > 5)
                    throw new ArgumentOutOfRangeException("Invalid value for IFlag1", value, value.ToString());
                _iFlag1 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IFlag2", Desc = "", ContextType = SysDic.All, Length = 5)]
        public virtual string IFlag2
        {
            get { return _iFlag2; }
            set
            {
                if (value != null && value.Length > 5)
                    throw new ArgumentOutOfRangeException("Invalid value for IFlag2", value, value.ToString());
                _iFlag2 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IFlag3", Desc = "", ContextType = SysDic.All, Length = 5)]
        public virtual string IFlag3
        {
            get { return _iFlag3; }
            set
            {
                if (value != null && value.Length > 5)
                    throw new ArgumentOutOfRangeException("Invalid value for IFlag3", value, value.ToString());
                _iFlag3 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IFlag4", Desc = "", ContextType = SysDic.All, Length = 5)]
        public virtual string IFlag4
        {
            get { return _iFlag4; }
            set
            {
                if (value != null && value.Length > 5)
                    throw new ArgumentOutOfRangeException("Invalid value for IFlag4", value, value.ToString());
                _iFlag4 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IFlag5", Desc = "", ContextType = SysDic.All, Length = 5)]
        public virtual string IFlag5
        {
            get { return _iFlag5; }
            set
            {
                if (value != null && value.Length > 5)
                    throw new ArgumentOutOfRangeException("Invalid value for IFlag5", value, value.ToString());
                _iFlag5 = value;
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
        [DataDesc(CName = "", ShortCode = "Visible", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }


        #endregion
    }
    #endregion
}