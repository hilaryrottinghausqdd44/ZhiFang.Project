using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
    #region BloodBPreItem

    /// <summary>
    /// BloodBPreItem object for NHibernate mapped table 'Blood_BPreItem'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BloodBPreItem", ShortCode = "BloodBPreItem", Desc = "")]
    public class BloodBPreItem : BaseEntityServiceByString
    {
        #region Member Variables

        protected string _bloodinPlaceID;
        protected string _bBagCode;
        protected string _pCode;
        protected string _bBagExCode;
        protected DateTime? _invalidDate;
        protected DateTime? _collectDate;
        protected double _bCount;
        protected string _b3Code;
        protected int _getFlag;
        protected int _getUserID;
        protected DateTime? _getTime;
        protected string _memo;
        protected string _zx1;
        protected string _zx2;
        protected string _zx3;
        protected DateTime? _bPreDate;
        protected string _bINo;
        protected decimal _price;
        protected string _mainside;
        protected string _secondSide;
        protected int _outflag;
        protected string _getBloodId;
        protected int _isaboCharge;
        protected int _msChargeFlag;
        protected int _ssChargeFlag;
        protected string _bCNo;
        //protected string _bReqFormID;
        protected int _bPreItemCheckFlag;
        protected DateTime? _bPreItemCheckTime;
        protected string _bPreItemCheckID;
        protected string _bloodinID;
        protected string _bPreUserID;
        protected string _sendId;
        protected DateTime? _sendTime;
        protected string _bPreSame;
        protected int _msWay1;
        protected int _msWay2;
        protected int _msWay3;
        protected string _msresult1;
        protected string _msresult2;
        protected string _msresult3;
        protected int _ssWay1;
        protected int _ssWay2;
        protected int _ssWay3;
        protected string _ssresult1;
        protected string _ssresult2;
        protected string _ssresult3;
        protected string _bframeNO;
        protected string _gbloodscan;

        protected string _notProcess;
        protected string _isLock;
        protected int _bPremhyyFlag;
        protected int _ismulbpreitem;
        protected string _zlxmbm;
        protected string _getOperID;
        protected DateTime? _getOperTime;
        protected string _bloodoC;
        protected string _oKNo;
        protected string _bagChargeFlag;
        protected string _hl7Result;
        protected string _hl7Id;
        protected string _hl7Time;
        protected string _bagNurseHint;
        protected string _qxpzBZNo;
        protected string _globulin;
        protected int _dispOrder;
        protected bool _visible;
        protected BloodBPreForm _bloodBPreForm;
        protected BloodBReqForm _bloodBReqForm;

        protected BloodBReqItem _bloodBReqItem;
        protected Bloodstyle _bloodstyle;
        protected BloodBUnit _bloodBUnit;
        protected BloodABO _bloodABO;
        protected BloodBInItem _bloodBInItem;

        #endregion

        #region Constructors

        public BloodBPreItem() { }

        public BloodBPreItem(BloodBReqItem bloodBReqItem, BloodBInItem bloodBInItem, BloodBUnit bloodBUnit, Bloodstyle bloodstyle, BloodABO bloodABO, string bloodinPlaceID, string bBagCode, string pCode, string bBagExCode, DateTime invalidDate, DateTime collectDate, double bCount, string b3Code, int getFlag, int getUserID, DateTime getTime, string memo, string zx1, string zx2, string zx3, DateTime bPreDate, string bINo, decimal price, string mainside, string secondSide, int outflag, string getBloodId, int isaboCharge, int msChargeFlag, int ssChargeFlag, string bCNo, int bPreItemCheckFlag, DateTime bPreItemCheckTime, string bPreItemCheckID, string bloodinID, string bPreUserID, string sendId, DateTime sendTime, string bPreSame, int msWay1, int msWay2, int msWay3, string msresult1, string msresult2, string msresult3, int ssWay1, int ssWay2, int ssWay3, string ssresult1, string ssresult2, string ssresult3, string bframeNO, string gbloodscan, string notProcess, string isLock, int bPremhyyFlag, int ismulbpreitem, string zlxmbm, string getOperID, DateTime getOperTime, string bloodoC, string oKNo, string bagChargeFlag, string hl7Result, string hl7Id, string hl7Time, string bagNurseHint, string qxpzBZNo, string globulin, long labID, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp, bool visible, BloodBReqForm bloodBReqForm, BloodBPreForm bloodBPreForm)
        {
            this._bloodBReqForm = bloodBReqForm;

            this._bloodBReqItem = bloodBReqItem;
            this._bloodBInItem = bloodBInItem;
            this._bloodBUnit = bloodBUnit;
            this._bloodstyle = bloodstyle;
            this._bloodABO = bloodABO;

            this._bloodinPlaceID = bloodinPlaceID;
            this._bBagCode = bBagCode;
            this._pCode = pCode;
            this._bBagExCode = bBagExCode;
            this._invalidDate = invalidDate;
            this._collectDate = collectDate;
            this._bCount = bCount;
            this._b3Code = b3Code;
            this._getFlag = getFlag;
            this._getUserID = getUserID;
            this._getTime = getTime;
            this._memo = memo;
            this._zx1 = zx1;
            this._zx2 = zx2;
            this._zx3 = zx3;
            this._bPreDate = bPreDate;
            this._bINo = bINo;
            this._price = price;
            this._mainside = mainside;
            this._secondSide = secondSide;
            this._outflag = outflag;
            this._getBloodId = getBloodId;
            this._isaboCharge = isaboCharge;
            this._msChargeFlag = msChargeFlag;
            this._ssChargeFlag = ssChargeFlag;
            this._bCNo = bCNo;
            //this._bReqFormID = bReqFormID;
            this._bPreItemCheckFlag = bPreItemCheckFlag;
            this._bPreItemCheckTime = bPreItemCheckTime;
            this._bPreItemCheckID = bPreItemCheckID;
            this._bloodinID = bloodinID;
            this._bPreUserID = bPreUserID;
            this._sendId = sendId;
            this._sendTime = sendTime;
            this._bPreSame = bPreSame;
            this._msWay1 = msWay1;
            this._msWay2 = msWay2;
            this._msWay3 = msWay3;
            this._msresult1 = msresult1;
            this._msresult2 = msresult2;
            this._msresult3 = msresult3;
            this._ssWay1 = ssWay1;
            this._ssWay2 = ssWay2;
            this._ssWay3 = ssWay3;
            this._ssresult1 = ssresult1;
            this._ssresult2 = ssresult2;
            this._ssresult3 = ssresult3;
            this._bframeNO = bframeNO;
            this._gbloodscan = gbloodscan;
            this._notProcess = notProcess;
            this._isLock = isLock;
            this._bPremhyyFlag = bPremhyyFlag;
            this._ismulbpreitem = ismulbpreitem;
            this._zlxmbm = zlxmbm;
            this._getOperID = getOperID;
            this._getOperTime = getOperTime;
            this._bloodoC = bloodoC;
            this._oKNo = oKNo;
            this._bagChargeFlag = bagChargeFlag;
            this._hl7Result = hl7Result;
            this._hl7Id = hl7Id;
            this._hl7Time = hl7Time;
            this._bagNurseHint = bagNurseHint;
            this._qxpzBZNo = qxpzBZNo;
            this._globulin = globulin;
            this._labID = labID;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
            this._visible = visible;
            this._bloodBPreForm = bloodBPreForm;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BloodBPreForm", Desc = "")]
        public virtual BloodBPreForm BloodBPreForm
        {
            get { return _bloodBPreForm; }
            set { _bloodBPreForm = value; }
        }
        [DataMember]
        [DataDesc(CName = "BloodBReqItem", ShortCode = "BloodBReqItem", Desc = "BloodBReqItem")]
        public virtual BloodBReqItem BloodBReqItem
        {
            get { return _bloodBReqItem; }
            set { _bloodBReqItem = value; }
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
        [DataDesc(CName = "BloodBInItem", ShortCode = "BloodBInItem", Desc = "BloodBInItem")]
        public virtual BloodBInItem BloodBInItem
        {
            get { return _bloodBInItem; }
            set { _bloodBInItem = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BloodinPlaceID", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BloodinPlaceID
        {
            get { return _bloodinPlaceID; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for BloodinPlaceID", value, value.ToString());
                _bloodinPlaceID = value;
            }
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
        [DataDesc(CName = "", ShortCode = "PCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string PCode
        {
            get { return _pCode; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for PCode", value, value.ToString());
                _pCode = value;
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
        [DataDesc(CName = "", ShortCode = "BCount", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double BCount
        {
            get { return _bCount; }
            set { _bCount = value; }
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
        [DataDesc(CName = "", ShortCode = "GetFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int GetFlag
        {
            get { return _getFlag; }
            set { _getFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "GetUserID", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int GetUserID
        {
            get { return _getUserID; }
            set { _getUserID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "GetTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? GetTime
        {
            get { return _getTime; }
            set { _getTime = value; }
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
        [DataDesc(CName = "", ShortCode = "Zx1", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Zx1
        {
            get { return _zx1; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Zx1", value, value.ToString());
                _zx1 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Zx2", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Zx2
        {
            get { return _zx2; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Zx2", value, value.ToString());
                _zx2 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Zx3", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Zx3
        {
            get { return _zx3; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Zx3", value, value.ToString());
                _zx3 = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BPreDate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? BPreDate
        {
            get { return _bPreDate; }
            set { _bPreDate = value; }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Price", Desc = "", ContextType = SysDic.All, Length = 9)]
        public virtual decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Mainside", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Mainside
        {
            get { return _mainside; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Mainside", value, value.ToString());
                _mainside = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SecondSide", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string SecondSide
        {
            get { return _secondSide; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for SecondSide", value, value.ToString());
                _secondSide = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Outflag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Outflag
        {
            get { return _outflag; }
            set { _outflag = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "GetBloodId", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string GetBloodId
        {
            get { return _getBloodId; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for GetBloodId", value, value.ToString());
                _getBloodId = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsaboCharge", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsaboCharge
        {
            get { return _isaboCharge; }
            set { _isaboCharge = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "MsChargeFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int MsChargeFlag
        {
            get { return _msChargeFlag; }
            set { _msChargeFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SsChargeFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int SsChargeFlag
        {
            get { return _ssChargeFlag; }
            set { _ssChargeFlag = value; }
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
        [DataDesc(CName = "", ShortCode = "BloodBReqForm", Desc = "")]
        public virtual BloodBReqForm BloodBReqForm
        {
            get { return _bloodBReqForm; }
            set { _bloodBReqForm = value; }
        }

        //      [DataMember]
        //      [DataDesc(CName = "", ShortCode = "BReqFormID", Desc = "", ContextType = SysDic.All, Length = 40)]
        //      public virtual string BReqFormID
        //{
        //	get { return _bReqFormID; }
        //	set
        //	{
        //		if ( value != null && value.Length > 40)
        //			throw new ArgumentOutOfRangeException("Invalid value for BReqFormID", value, value.ToString());
        //		_bReqFormID = value;
        //	}
        //}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BPreItemCheckFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int BPreItemCheckFlag
        {
            get { return _bPreItemCheckFlag; }
            set { _bPreItemCheckFlag = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BPreItemCheckTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? BPreItemCheckTime
        {
            get { return _bPreItemCheckTime; }
            set { _bPreItemCheckTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BPreItemCheckID", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BPreItemCheckID
        {
            get { return _bPreItemCheckID; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for BPreItemCheckID", value, value.ToString());
                _bPreItemCheckID = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BloodinID", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BloodinID
        {
            get { return _bloodinID; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for BloodinID", value, value.ToString());
                _bloodinID = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BPreUserID", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BPreUserID
        {
            get { return _bPreUserID; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for BPreUserID", value, value.ToString());
                _bPreUserID = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SendId", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string SendId
        {
            get { return _sendId; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for SendId", value, value.ToString());
                _sendId = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "SendTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? SendTime
        {
            get { return _sendTime; }
            set { _sendTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BPreSame", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string BPreSame
        {
            get { return _bPreSame; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for BPreSame", value, value.ToString());
                _bPreSame = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "MsWay1", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int MsWay1
        {
            get { return _msWay1; }
            set { _msWay1 = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "MsWay2", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int MsWay2
        {
            get { return _msWay2; }
            set { _msWay2 = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "MsWay3", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int MsWay3
        {
            get { return _msWay3; }
            set { _msWay3 = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Msresult1", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Msresult1
        {
            get { return _msresult1; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Msresult1", value, value.ToString());
                _msresult1 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Msresult2", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Msresult2
        {
            get { return _msresult2; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Msresult2", value, value.ToString());
                _msresult2 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Msresult3", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Msresult3
        {
            get { return _msresult3; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Msresult3", value, value.ToString());
                _msresult3 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SsWay1", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int SsWay1
        {
            get { return _ssWay1; }
            set { _ssWay1 = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SsWay2", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int SsWay2
        {
            get { return _ssWay2; }
            set { _ssWay2 = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SsWay3", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int SsWay3
        {
            get { return _ssWay3; }
            set { _ssWay3 = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Ssresult1", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Ssresult1
        {
            get { return _ssresult1; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Ssresult1", value, value.ToString());
                _ssresult1 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Ssresult2", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Ssresult2
        {
            get { return _ssresult2; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Ssresult2", value, value.ToString());
                _ssresult2 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Ssresult3", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Ssresult3
        {
            get { return _ssresult3; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Ssresult3", value, value.ToString());
                _ssresult3 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BframeNO", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BframeNO
        {
            get { return _bframeNO; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for BframeNO", value, value.ToString());
                _bframeNO = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Gbloodscan", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Gbloodscan
        {
            get { return _gbloodscan; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Gbloodscan", value, value.ToString());
                _gbloodscan = value;
            }
        }


        [DataMember]
        [DataDesc(CName = "", ShortCode = "NotProcess", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string NotProcess
        {
            get { return _notProcess; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for NotProcess", value, value.ToString());
                _notProcess = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsLock", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string IsLock
        {
            get { return _isLock; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for IsLock", value, value.ToString());
                _isLock = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BPremhyyFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int BPremhyyFlag
        {
            get { return _bPremhyyFlag; }
            set { _bPremhyyFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Ismulbpreitem", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Ismulbpreitem
        {
            get { return _ismulbpreitem; }
            set { _ismulbpreitem = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Zlxmbm", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Zlxmbm
        {
            get { return _zlxmbm; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Zlxmbm", value, value.ToString());
                _zlxmbm = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "GetOperID", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string GetOperID
        {
            get { return _getOperID; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for GetOperID", value, value.ToString());
                _getOperID = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "GetOperTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? GetOperTime
        {
            get { return _getOperTime; }
            set { _getOperTime = value; }
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
        [DataDesc(CName = "", ShortCode = "OKNo", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string OKNo
        {
            get { return _oKNo; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for OKNo", value, value.ToString());
                _oKNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BagChargeFlag", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string BagChargeFlag
        {
            get { return _bagChargeFlag; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for BagChargeFlag", value, value.ToString());
                _bagChargeFlag = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Hl7Result", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Hl7Result
        {
            get { return _hl7Result; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Hl7Result", value, value.ToString());
                _hl7Result = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Hl7Id", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Hl7Id
        {
            get { return _hl7Id; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Hl7Id", value, value.ToString());
                _hl7Id = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Hl7Time", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Hl7Time
        {
            get { return _hl7Time; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Hl7Time", value, value.ToString());
                _hl7Time = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BagNurseHint", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string BagNurseHint
        {
            get { return _bagNurseHint; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for BagNurseHint", value, value.ToString());
                _bagNurseHint = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "QxpzBZNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string QxpzBZNo
        {
            get { return _qxpzBZNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for QxpzBZNo", value, value.ToString());
                _qxpzBZNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Globulin", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Globulin
        {
            get { return _globulin; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Globulin", value, value.ToString());
                _globulin = value;
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