using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
    #region BloodBOutItem

    /// <summary>
    /// BloodBOutItem object for NHibernate mapped table 'Blood_BOutItem'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BloodBOutItem", ShortCode = "BloodBOutItem", Desc = "")]
    public class BloodBOutItem : BaseEntityServiceByString
    {
        #region Member Variables
        protected string _bBagCode;
        protected string _pcode;
        protected string _bBagExCode;
        protected string _b3Code;
        protected DateTime? _invalidDate;
        protected DateTime? _collectDate;
        protected double _bOutCount;
        protected int _checkFlag;
        protected string _bINo;
        protected string _memo;
        protected string _zX1;
        protected string _zX2;
        protected string _zX3;
        protected DateTime? _bODate;
        //protected string _bloodNo;
        protected int _outFlag;
        protected decimal _price;
        protected int _isCharge;
        //protected int _bloodABONo;
        protected string _bPreFormID;
        //protected string _bloodUnitNo;
        protected string _binItemID;
        protected string _bCNo;
        protected string _getBloodId;
        protected int _isBloodAdverse;
        protected DateTime? _bloodInfusionStartTime;
        protected DateTime? _bloodInfusionEndTime;
        protected string _bloodAdverseInputNo;
        protected string _bloodAdverseInputName;
        protected DateTime? _bloodAdverseInputTime;
        protected int _toHisFlag;
        protected string _bagChargeFlag1;
        protected string _bagChargeFlag2;
        protected string _bagChargeFlag3;
        protected string _bagChargeFlag4;
        protected string _bagChargeFlag5;
        protected string _bloodoC;
        protected string _oKNo;
        protected string _bPreItemId;
        protected string _usePlaceId;
        protected string _zlxmbm;
        protected string _orderFlag;
        protected string _bloodAdverseCheckNo;
        protected string _bloodAdverseCheckName;
        protected string _bloodAdverseCheckTime;
        protected string _iFlag1;
        protected string _iFlag2;
        protected string _iFlag3;
        protected string _iFlag4;
        protected string _iFlag5;
        protected string _iFlag6;
        protected string _iFlag7;
        protected string _iFlag8;
        protected string _iFlag9;
        protected string _iFlag10;
        protected string _outputno;
        protected string _bloodAdverseDemo;
        protected string _bnrNo;
        protected string _scanID;
        protected string _scanTime;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected bool _visible;
        protected int _confirmCompletion;
        protected int _handoverCompletion;
        protected int _courseCompletion;
        protected int _recoverCompletion;

        protected BloodBReqItem _bloodBReqItem;
        protected BloodBOutForm _bloodBOutForm;
        protected BloodUsePlace _bloodUsePlace;
        protected Bloodstyle _bloodstyle;
        protected BloodABO _bloodABO;
        protected BloodBUnit _bloodBUnit;
        protected BloodBReqFormVO _bloodBReqFormVO;

        #endregion

        #region Constructors

        public BloodBOutItem() { }

        public BloodBOutItem(BloodBReqItem bloodBReqItem, BloodBOutForm bloodBOutForm, Bloodstyle bloodstyle, BloodABO bloodABO, BloodUsePlace bloodUsePlace, string bBagCode, string pcode, string bBagExCode, DateTime invalidDate, DateTime collectDate, double bOutCount, int checkFlag, string bINo, string memo, string zX1, string zX2, string zX3, DateTime bODate, int outFlag, decimal price, int isCharge, string bPreFormID, BloodBUnit bloodBUnit, string binItemID, string bCNo, string getBloodId, int isBloodAdverse, DateTime bloodInfusionStartTime, DateTime bloodInfusionEndTime, string bloodAdverseInputNo, string bloodAdverseInputName, DateTime bloodAdverseInputTime, int toHisFlag, string bagChargeFlag1, string bagChargeFlag2, string bagChargeFlag3, string bagChargeFlag4, string bagChargeFlag5, string bloodoC, string oKNo, string bPreItemId, string usePlaceId, string zlxmbm, string orderFlag, string bloodAdverseCheckNo, string bloodAdverseCheckName, string bloodAdverseCheckTime, string iFlag1, string iFlag2, string iFlag3, string iFlag4, string iFlag5, string iFlag6, string iFlag7, string iFlag8, string iFlag9, string iFlag10, string outputno, string bloodAdverseDemo, string bnrNo, string scanID, string scanTime, long labID, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, bool visible, int confirmCompletion, int handoverCompletion, int courseCompletion, int recoverCompletion)
        {
            this._bloodBReqItem = bloodBReqItem;
            this._bloodBOutForm = bloodBOutForm;
            this._bloodABO = bloodABO;
            this._bloodUsePlace = bloodUsePlace;
            this._bBagCode = bBagCode;
            this._pcode = pcode;
            this._bBagExCode = bBagExCode;
            this._invalidDate = invalidDate;
            this._collectDate = collectDate;
            this._bOutCount = bOutCount;
            this._checkFlag = checkFlag;
            this._bINo = bINo;
            this._memo = memo;
            this._zX1 = zX1;
            this._zX2 = zX2;
            this._zX3 = zX3;
            this._bODate = bODate;
            this._bloodstyle = bloodstyle;
            this._outFlag = outFlag;
            this._price = price;
            this._isCharge = isCharge;
            this._bPreFormID = bPreFormID;
            this._bloodBUnit = bloodBUnit;
            this._binItemID = binItemID;
            this._bCNo = bCNo;
            this._getBloodId = getBloodId;
            this._isBloodAdverse = isBloodAdverse;
            this._bloodInfusionStartTime = bloodInfusionStartTime;
            this._bloodInfusionEndTime = bloodInfusionEndTime;
            this._bloodAdverseInputNo = bloodAdverseInputNo;
            this._bloodAdverseInputName = bloodAdverseInputName;
            this._bloodAdverseInputTime = bloodAdverseInputTime;
            this._toHisFlag = toHisFlag;
            this._bagChargeFlag1 = bagChargeFlag1;
            this._bagChargeFlag2 = bagChargeFlag2;
            this._bagChargeFlag3 = bagChargeFlag3;
            this._bagChargeFlag4 = bagChargeFlag4;
            this._bagChargeFlag5 = bagChargeFlag5;
            this._bloodoC = bloodoC;
            this._oKNo = oKNo;
            this._bPreItemId = bPreItemId;
            this._usePlaceId = usePlaceId;
            this._zlxmbm = zlxmbm;
            this._orderFlag = orderFlag;
            this._bloodAdverseCheckNo = bloodAdverseCheckNo;
            this._bloodAdverseCheckName = bloodAdverseCheckName;
            this._bloodAdverseCheckTime = bloodAdverseCheckTime;
            this._iFlag1 = iFlag1;
            this._iFlag2 = iFlag2;
            this._iFlag3 = iFlag3;
            this._iFlag4 = iFlag4;
            this._iFlag5 = iFlag5;
            this._iFlag6 = iFlag6;
            this._iFlag7 = iFlag7;
            this._iFlag8 = iFlag8;
            this._iFlag9 = iFlag9;
            this._iFlag10 = iFlag10;
            this._outputno = outputno;
            this._bloodAdverseDemo = bloodAdverseDemo;
            this._bnrNo = bnrNo;
            this._scanID = scanID;
            this._scanTime = scanTime;
            this._labID = labID;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._visible = visible;
            this._confirmCompletion = confirmCompletion;
            this._handoverCompletion = handoverCompletion;
            this._courseCompletion = courseCompletion;
            this._recoverCompletion = recoverCompletion;
        }

        #endregion

        #region Public Properties
        [DataMember]
        [DataDesc(CName = "BloodBReqItem", ShortCode = "BloodBReqItem", Desc = "BloodBReqItem")]
        public virtual BloodBReqItem BloodBReqItem
        {
            get { return _bloodBReqItem; }
            set { _bloodBReqItem = value; }
        }
        [DataMember]
        [DataDesc(CName = "BloodBReqFormVO", ShortCode = "BloodBReqFormVO", Desc = "BloodBReqFormVO")]
        public virtual BloodBReqFormVO BloodBReqFormVO
        {
            get { return _bloodBReqFormVO; }
            set { _bloodBReqFormVO = value; }
        }

        [DataMember]
        [DataDesc(CName = "BloodBOutForm", ShortCode = "BloodBOutForm", Desc = "BloodBOutForm")]
        public virtual BloodBOutForm BloodBOutForm
        {
            get { return _bloodBOutForm; }
            set { _bloodBOutForm = value; }
        }
        [DataMember]
        [DataDesc(CName = "BloodUsePlace", ShortCode = "BloodUsePlace", Desc = "BloodUsePlace")]
        public virtual BloodUsePlace BloodUsePlace
        {
            get { return _bloodUsePlace; }
            set { _bloodUsePlace = value; }
        }
        [DataMember]
        [DataDesc(CName = "Bloodstyle", ShortCode = "Bloodstyle", Desc = "Bloodstyle")]
        public virtual Bloodstyle Bloodstyle
        {
            get { return _bloodstyle; }
            set { _bloodstyle = value; }
        }
        [DataMember]
        [DataDesc(CName = "BloodABO", ShortCode = "BloodABO", Desc = "BloodABO")]
        public virtual BloodABO BloodABO
        {
            get { return _bloodABO; }
            set { _bloodABO = value; }
        }
        [DataMember]
        [DataDesc(CName = "BloodBUnit", ShortCode = "BloodBUnit", Desc = "BloodBUnit")]
        public virtual BloodBUnit BloodBUnit
        {
            get { return _bloodBUnit; }
            set { _bloodBUnit = value; }
        }

        //      [DataMember]
        //      [DataDesc(CName = "BOutFormID", ShortCode = "BOutFormID", Desc = "BOutFormID", ContextType = SysDic.All, Length = 20)]
        //      public virtual string BOutFormID
        //      {
        //	get { return _bOutFormID; }
        //	set
        //	{
        //		if ( value != null && value.Length > 20)
        //			throw new ArgumentOutOfRangeException("Invalid value for BOutFormID", value, value.ToString());
        //              _bOutFormID = value;
        //	}
        //}

        //[DataMember]
        //[DataDesc(CName = "", ShortCode = "BReqItemID", Desc = "", ContextType = SysDic.All, Length = 20)]
        //public virtual string BReqItemID
        //{
        //    get { return _bReqItemID; }
        //    set
        //    {
        //        if (value != null && value.Length > 20)
        //            throw new ArgumentOutOfRangeException("Invalid value for BReqItemID", value, value.ToString());
        //        _bReqItemID = value;
        //    }
        //}

        //[DataMember]
        //[DataDesc(CName = "", ShortCode = "BloodinPlaceID", Desc = "", ContextType = SysDic.All, Length = 20)]
        //public virtual string BloodinPlaceID
        //{
        //    get { return _bloodinPlaceID; }
        //    set
        //    {
        //        if (value != null && value.Length > 20)
        //            throw new ArgumentOutOfRangeException("Invalid value for BloodinPlaceID", value, value.ToString());
        //        _bloodinPlaceID = value;
        //    }
        //}

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
        [DataDesc(CName = "B3Code", ShortCode = "B3Code", Desc = "B3Code", ContextType = SysDic.All, Length = 50)]
        public virtual string B3Code
        {
            get { return _b3Code; }
            set
            {
                _b3Code = value;
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
        [DataDesc(CName = "", ShortCode = "BOutCount", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double BOutCount
        {
            get { return _bOutCount; }
            set { _bOutCount = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CheckFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int CheckFlag
        {
            get { return _checkFlag; }
            set { _checkFlag = value; }
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
        [DataDesc(CName = "", ShortCode = "BODate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? BODate
        {
            get { return _bODate; }
            set { _bODate = value; }
        }

        //[DataMember]
        //[DataDesc(CName = "", ShortCode = "BloodNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        //public virtual string BloodNo
        //{
        //    get { return _bloodNo; }
        //    set
        //    {
        //        if (value != null && value.Length > 20)
        //            throw new ArgumentOutOfRangeException("Invalid value for BloodNo", value, value.ToString());
        //        _bloodNo = value;
        //    }
        //}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OutFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int OutFlag
        {
            get { return _outFlag; }
            set { _outFlag = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Price", Desc = "", ContextType = SysDic.All, Length = 9)]
        public virtual decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsCharge", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsCharge
        {
            get { return _isCharge; }
            set { _isCharge = value; }
        }

        //[DataMember]
        //[DataDesc(CName = "", ShortCode = "BloodABONo", Desc = "", ContextType = SysDic.All, Length = 4)]
        //public virtual int BloodABONo
        //{
        //    get { return _bloodABONo; }
        //    set { _bloodABONo = value; }
        //}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BPreFormID", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string BPreFormID
        {
            get { return _bPreFormID; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for BPreFormID", value, value.ToString());
                _bPreFormID = value;
            }
        }

        //[DataMember]
        //[DataDesc(CName = "", ShortCode = "BloodUnitNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        //public virtual string BloodUnitNo
        //{
        //    get { return _bloodUnitNo; }
        //    set
        //    {
        //        if (value != null && value.Length > 20)
        //            throw new ArgumentOutOfRangeException("Invalid value for BloodUnitNo", value, value.ToString());
        //        _bloodUnitNo = value;
        //    }
        //}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BinItemID", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string BinItemID
        {
            get { return _binItemID; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for BinItemID", value, value.ToString());
                _binItemID = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BCNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BCNo
        {
            get { return _bCNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for BCNo", value, value.ToString());
                _bCNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "GetBloodId", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string GetBloodId
        {
            get { return _getBloodId; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for GetBloodId", value, value.ToString());
                _getBloodId = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsBloodAdverse", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsBloodAdverse
        {
            get { return _isBloodAdverse; }
            set { _isBloodAdverse = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BloodInfusionStartTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? BloodInfusionStartTime
        {
            get { return _bloodInfusionStartTime; }
            set { _bloodInfusionStartTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BloodInfusionEndTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? BloodInfusionEndTime
        {
            get { return _bloodInfusionEndTime; }
            set { _bloodInfusionEndTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BloodAdverseInputNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BloodAdverseInputNo
        {
            get { return _bloodAdverseInputNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for BloodAdverseInputNo", value, value.ToString());
                _bloodAdverseInputNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BloodAdverseInputName", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BloodAdverseInputName
        {
            get { return _bloodAdverseInputName; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for BloodAdverseInputName", value, value.ToString());
                _bloodAdverseInputName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BloodAdverseInputTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? BloodAdverseInputTime
        {
            get { return _bloodAdverseInputTime; }
            set { _bloodAdverseInputTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ToHisFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ToHisFlag
        {
            get { return _toHisFlag; }
            set { _toHisFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BagChargeFlag1", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string BagChargeFlag1
        {
            get { return _bagChargeFlag1; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for BagChargeFlag1", value, value.ToString());
                _bagChargeFlag1 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BagChargeFlag2", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string BagChargeFlag2
        {
            get { return _bagChargeFlag2; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for BagChargeFlag2", value, value.ToString());
                _bagChargeFlag2 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BagChargeFlag3", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string BagChargeFlag3
        {
            get { return _bagChargeFlag3; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for BagChargeFlag3", value, value.ToString());
                _bagChargeFlag3 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BagChargeFlag4", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string BagChargeFlag4
        {
            get { return _bagChargeFlag4; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for BagChargeFlag4", value, value.ToString());
                _bagChargeFlag4 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BagChargeFlag5", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string BagChargeFlag5
        {
            get { return _bagChargeFlag5; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for BagChargeFlag5", value, value.ToString());
                _bagChargeFlag5 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BloodoC", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BloodoC
        {
            get { return _bloodoC; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for BloodoC", value, value.ToString());
                _bloodoC = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OKNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string OKNo
        {
            get { return _oKNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for OKNo", value, value.ToString());
                _oKNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BPreItemId", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BPreItemId
        {
            get { return _bPreItemId; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for BPreItemId", value, value.ToString());
                _bPreItemId = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UsePlaceId", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string UsePlaceId
        {
            get { return _usePlaceId; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for UsePlaceId", value, value.ToString());
                _usePlaceId = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Zlxmbm", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Zlxmbm
        {
            get { return _zlxmbm; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Zlxmbm", value, value.ToString());
                _zlxmbm = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OrderFlag", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string OrderFlag
        {
            get { return _orderFlag; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for OrderFlag", value, value.ToString());
                _orderFlag = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BloodAdverseCheckNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BloodAdverseCheckNo
        {
            get { return _bloodAdverseCheckNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for BloodAdverseCheckNo", value, value.ToString());
                _bloodAdverseCheckNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BloodAdverseCheckName", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BloodAdverseCheckName
        {
            get { return _bloodAdverseCheckName; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for BloodAdverseCheckName", value, value.ToString());
                _bloodAdverseCheckName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BloodAdverseCheckTime", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BloodAdverseCheckTime
        {
            get { return _bloodAdverseCheckTime; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for BloodAdverseCheckTime", value, value.ToString());
                _bloodAdverseCheckTime = value;
            }
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
        [DataDesc(CName = "", ShortCode = "IFlag6", Desc = "", ContextType = SysDic.All, Length = 5)]
        public virtual string IFlag6
        {
            get { return _iFlag6; }
            set
            {
                if (value != null && value.Length > 5)
                    throw new ArgumentOutOfRangeException("Invalid value for IFlag6", value, value.ToString());
                _iFlag6 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IFlag7", Desc = "", ContextType = SysDic.All, Length = 5)]
        public virtual string IFlag7
        {
            get { return _iFlag7; }
            set
            {
                if (value != null && value.Length > 5)
                    throw new ArgumentOutOfRangeException("Invalid value for IFlag7", value, value.ToString());
                _iFlag7 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IFlag8", Desc = "", ContextType = SysDic.All, Length = 5)]
        public virtual string IFlag8
        {
            get { return _iFlag8; }
            set
            {
                if (value != null && value.Length > 5)
                    throw new ArgumentOutOfRangeException("Invalid value for IFlag8", value, value.ToString());
                _iFlag8 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IFlag9", Desc = "", ContextType = SysDic.All, Length = 5)]
        public virtual string IFlag9
        {
            get { return _iFlag9; }
            set
            {
                if (value != null && value.Length > 5)
                    throw new ArgumentOutOfRangeException("Invalid value for IFlag9", value, value.ToString());
                _iFlag9 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IFlag10", Desc = "", ContextType = SysDic.All, Length = 5)]
        public virtual string IFlag10
        {
            get { return _iFlag10; }
            set
            {
                if (value != null && value.Length > 5)
                    throw new ArgumentOutOfRangeException("Invalid value for IFlag10", value, value.ToString());
                _iFlag10 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Outputno", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string Outputno
        {
            get { return _outputno; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Outputno", value, value.ToString());
                _outputno = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BloodAdverseDemo", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string BloodAdverseDemo
        {
            get { return _bloodAdverseDemo; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for BloodAdverseDemo", value, value.ToString());
                _bloodAdverseDemo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BnrNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BnrNo
        {
            get { return _bnrNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for BnrNo", value, value.ToString());
                _bnrNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ScanID", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string ScanID
        {
            get { return _scanID; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for ScanID", value, value.ToString());
                _scanID = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ScanTime", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string ScanTime
        {
            get { return _scanTime; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for ScanTime", value, value.ToString());
                _scanTime = value;
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Visible", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ConfirmCompletion", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ConfirmCompletion
        {
            get { return _confirmCompletion; }
            set { _confirmCompletion = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HandoverCompletion", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int HandoverCompletion
        {
            get { return _handoverCompletion; }
            set { _handoverCompletion = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CourseCompletion", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int CourseCompletion
        {
            get { return _courseCompletion; }
            set { _courseCompletion = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "RecoverCompletion", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int RecoverCompletion
        {
            get { return _recoverCompletion; }
            set { _recoverCompletion = value; }
        }


        #endregion

        #region 自定义
        /// <summary>
        /// 输血过程记录登记使用
        /// </summary>

        protected BloodBReqForm _bloodBReqForm;
        [DataMember]
        [DataDesc(CName = "BloodBReqForm", ShortCode = "BloodBReqForm", Desc = "发血血袋的申请主单信息")]
        public virtual BloodBReqForm BloodBReqForm
        {
            get { return _bloodBReqForm; }
            set { _bloodBReqForm = value; }
        }
        /// <summary>
        /// 输血过程记录登记使用
        /// </summary>

        protected BloodTransForm _bloodTransForm;
        [DataMember]
        [DataDesc(CName = "BloodTransForm", ShortCode = "BloodTransForm", Desc = "发血血袋的输血记录主单信息")]
        public virtual BloodTransForm BloodTransForm
        {
            get { return _bloodTransForm; }
            set { _bloodTransForm = value; }
        }

        #region 血袋接收使用

        [DataMember]
        [DataDesc(CName = "血袋外观信息", ShortCode = "BloodAppearance", Desc = "血袋外观信息")]
        public virtual BloodBagOperationDtl BloodAppearance { get; set; }

        [DataMember]
        [DataDesc(CName = "血袋完整性", ShortCode = "BloodIntegrity", Desc = "血袋完整性")]
        public virtual BloodBagOperationDtl BloodIntegrity { get; set; }

        #endregion

        private BloodBReqFormVO setBloodBReqFormVO(BloodBReqForm bloodbreqform)
        {
            BloodBReqFormVO bloodBReqFormVO = new BloodBReqFormVO();
            bloodBReqFormVO.AdmID = bloodbreqform.AdmID;
            bloodBReqFormVO.PatNo = bloodbreqform.PatNo;
            bloodBReqFormVO.CName = bloodbreqform.CName;
            bloodBReqFormVO.Sex = bloodbreqform.Sex;
            bloodBReqFormVO.AgeALL = bloodbreqform.AgeALL;
            bloodBReqFormVO.Bed = bloodbreqform.Bed;
            bloodBReqFormVO.DeptNo = bloodbreqform.DeptNo;
            return bloodBReqFormVO;
        }
        //BloodBReqItem bloodbreqitem, 
        public BloodBOutItem(BloodBOutItem bloodboutitem, BloodBOutForm bloodboutform, BloodBReqForm bloodbreqform, Bloodstyle bloodstyle, BloodABO bloodabo, BloodBUnit bloodbunit)
        {
            this._bloodBReqFormVO = setBloodBReqFormVO(bloodbreqform);
            this._bloodBReqForm = bloodbreqform;
            this._bloodBReqItem = bloodboutitem.BloodBReqItem;
            this._bloodBOutForm = bloodboutform;
            this._bloodstyle = bloodstyle;
            this._bloodABO = bloodabo;
            this._bloodBUnit = bloodbunit;
            this._bloodUsePlace = bloodboutitem.BloodUsePlace;

            this._id = bloodboutitem.Id;
            this._bBagCode = bloodboutitem.BBagCode;
            this._pcode = bloodboutitem.Pcode;
            this._b3Code = bloodboutitem.B3Code;
            
            this._bBagExCode = bloodboutitem.BBagExCode;
            this._invalidDate = bloodboutitem.InvalidDate;
            this._collectDate = bloodboutitem.CollectDate;
            this._bOutCount = bloodboutitem.BOutCount;
            this._checkFlag = bloodboutitem.CheckFlag;
            this._bINo = bloodboutitem.BINo;
            this._memo = bloodboutitem.Memo;
            this._zX1 = bloodboutitem.ZX1;
            this._zX2 = bloodboutitem.ZX2;
            this._zX3 = bloodboutitem.ZX3;
            this._bODate = bloodboutitem.BODate;
            this._outFlag = bloodboutitem.OutFlag;
            this._price = bloodboutitem.Price;
            this._isCharge = bloodboutitem.IsCharge;
            this._bPreFormID = bloodboutitem.BPreFormID;
            this._binItemID = bloodboutitem.BinItemID;
            this._bCNo = bloodboutitem.BCNo;
            this._getBloodId = bloodboutitem.GetBloodId;
            this._isBloodAdverse = bloodboutitem.IsBloodAdverse;
            this._bloodInfusionStartTime = bloodboutitem.BloodInfusionStartTime;
            this._bloodInfusionEndTime = bloodboutitem.BloodInfusionEndTime;
            this._bloodAdverseInputNo = bloodboutitem.BloodAdverseInputNo;
            this._bloodAdverseInputName = bloodboutitem.BloodAdverseInputName;
            this._bloodAdverseInputTime = bloodboutitem.BloodAdverseInputTime;
            this._toHisFlag = bloodboutitem.ToHisFlag;
            this._bagChargeFlag1 = bloodboutitem.BagChargeFlag1;
            this._bagChargeFlag2 = bloodboutitem.BagChargeFlag2;
            this._bagChargeFlag3 = bloodboutitem.BagChargeFlag3;
            this._bagChargeFlag4 = bloodboutitem.BagChargeFlag4;
            this._bagChargeFlag5 = bloodboutitem.BagChargeFlag5;
            this._bloodoC = bloodboutitem.BloodoC;
            this._oKNo = bloodboutitem.OKNo;
            this._bPreItemId = bloodboutitem.BPreItemId;
            this._usePlaceId = bloodboutitem.UsePlaceId;
            this._zlxmbm = bloodboutitem.Zlxmbm;
            this._orderFlag = bloodboutitem.OrderFlag;
            this._bloodAdverseCheckNo = bloodboutitem.BloodAdverseCheckNo;
            this._bloodAdverseCheckName = bloodboutitem.BloodAdverseCheckName;
            this._bloodAdverseCheckTime = bloodboutitem.BloodAdverseCheckTime;
            this._outputno = bloodboutitem.Outputno;
            this._bloodAdverseDemo = bloodboutitem.BloodAdverseDemo;
            this._bnrNo = bloodboutitem.BnrNo;
            this._scanID = bloodboutitem.ScanID;
            this._scanTime = bloodboutitem.ScanTime;
            this._labID = bloodboutitem.LabID;
            this._dispOrder = bloodboutitem.DispOrder;
            this._dataAddTime = bloodboutitem.DataAddTime;
            this._dataUpdateTime = bloodboutitem.DataUpdateTime;
            this._dataTimeStamp = bloodboutitem.DataTimeStamp;
            this._visible = bloodboutitem.Visible;
            this._confirmCompletion = bloodboutitem.ConfirmCompletion;
            this._handoverCompletion = bloodboutitem.HandoverCompletion;
            this._courseCompletion = bloodboutitem.CourseCompletion;
            this._recoverCompletion = bloodboutitem.RecoverCompletion;
        }
        #endregion
    }
    #endregion
}