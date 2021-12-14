using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
    #region BloodBPreForm

    /// <summary>
    /// BloodBPreForm object for NHibernate mapped table 'Blood_BPreForm'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BloodBPreForm", ShortCode = "BloodBPreForm", Desc = "")]
    public class BloodBPreForm : BaseEntityServiceByString
    {
        #region Member Variables

        //protected string _bReqFormID;
        protected DateTime _operTime;
        protected int _operatorID;
        protected DateTime? _printTime;
        protected int _printCount;
        protected string _memo;
        protected string _zX1;
        protected string _zX2;
        protected string _zX3;
        protected int _bPreCheckFlag;
        protected string _checkID;
        protected DateTime? _checkTime;
        //protected string _bReqItemID;
        protected string _bloodOrder;
        protected string _barCode;
        protected int _isVisible;
        protected int _isCharge;
        protected int _outFlag;
        protected string _firstABOTest;
        protected string _firstABOWay;
        protected string _bPreAntiBody;
        protected int _overdueID;
        protected string _firstRhTest;
        protected string _yqCode;
        protected string _barCodeMemo;
        protected int _isCompleted;
        protected int _bPreSendID;
        protected DateTime? _bPreSendTime;
        protected int _dispOrder;
        protected bool _visible;

        protected BloodBReqItem _bloodBReqItem;
        protected BloodBReqForm _bloodBReqForm;
        #endregion

        #region Constructors

        public BloodBPreForm() { }

        public BloodBPreForm(BloodBReqItem bloodBReqItem, DateTime operTime, int operatorID, DateTime printTime, int printCount, string memo, string zX1, string zX2, string zX3, int bPreCheckFlag, string checkID, DateTime checkTime, string bReqItemID, string bloodOrder, string barCode, int isVisible, int isCharge, int outFlag, string firstABOTest, string firstABOWay, string bPreAntiBody, int overdueID, string firstRhTest, string yqCode, string barCodeMemo, int isCompleted, int bPreSendID, DateTime bPreSendTime, long labID, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp, bool visible,
            BloodBReqForm bloodBReqForm)
        {
            this._bloodBReqForm = bloodBReqForm;
            this._bloodBReqItem = bloodBReqItem;

            //this._bReqFormID = bReqFormID;
            this._operTime = operTime;
            this._operatorID = operatorID;
            this._printTime = printTime;
            this._printCount = printCount;
            this._memo = memo;
            this._zX1 = zX1;
            this._zX2 = zX2;
            this._zX3 = zX3;
            this._bPreCheckFlag = bPreCheckFlag;
            this._checkID = checkID;
            this._checkTime = checkTime;
            //this._bReqItemID = bReqItemID;
            this._bloodOrder = bloodOrder;
            this._barCode = barCode;
            this._isVisible = isVisible;
            this._isCharge = isCharge;
            this._outFlag = outFlag;
            this._firstABOTest = firstABOTest;
            this._firstABOWay = firstABOWay;
            this._bPreAntiBody = bPreAntiBody;
            this._overdueID = overdueID;
            this._firstRhTest = firstRhTest;
            this._yqCode = yqCode;
            this._barCodeMemo = barCodeMemo;
            this._isCompleted = isCompleted;
            this._bPreSendID = bPreSendID;
            this._bPreSendTime = bPreSendTime;
            this._labID = labID;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
            this._visible = visible;
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
        [DataDesc(CName = "BloodBReqForm", ShortCode = "BloodBReqForm", Desc = "BloodBReqForm")]
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OperTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime OperTime
        {
            get { return _operTime; }
            set { _operTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OperatorID", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int OperatorID
        {
            get { return _operatorID; }
            set { _operatorID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "PrintTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? PrintTime
        {
            get { return _printTime; }
            set { _printTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PrintCount", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int PrintCount
        {
            get { return _printCount; }
            set { _printCount = value; }
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
        [DataDesc(CName = "", ShortCode = "BPreCheckFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int BPreCheckFlag
        {
            get { return _bPreCheckFlag; }
            set { _bPreCheckFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CheckID", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string CheckID
        {
            get { return _checkID; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for CheckID", value, value.ToString());
                _checkID = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CheckTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? CheckTime
        {
            get { return _checkTime; }
            set { _checkTime = value; }
        }

        //      [DataMember]
        //      [DataDesc(CName = "", ShortCode = "BReqItemID", Desc = "", ContextType = SysDic.All, Length = 50)]
        //      public virtual string BReqItemID
        //{
        //	get { return _bReqItemID; }
        //	set
        //	{
        //		if ( value != null && value.Length > 50)
        //			throw new ArgumentOutOfRangeException("Invalid value for BReqItemID", value, value.ToString());
        //		_bReqItemID = value;
        //	}
        //}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BloodOrder", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string BloodOrder
        {
            get { return _bloodOrder; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for BloodOrder", value, value.ToString());
                _bloodOrder = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BarCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string BarCode
        {
            get { return _barCode; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for BarCode", value, value.ToString());
                _barCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsVisible", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsVisible
        {
            get { return _isVisible; }
            set { _isVisible = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsCharge", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsCharge
        {
            get { return _isCharge; }
            set { _isCharge = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OutFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int OutFlag
        {
            get { return _outFlag; }
            set { _outFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FirstABOTest", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string FirstABOTest
        {
            get { return _firstABOTest; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for FirstABOTest", value, value.ToString());
                _firstABOTest = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FirstABOWay", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string FirstABOWay
        {
            get { return _firstABOWay; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for FirstABOWay", value, value.ToString());
                _firstABOWay = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BPreAntiBody", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BPreAntiBody
        {
            get { return _bPreAntiBody; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for BPreAntiBody", value, value.ToString());
                _bPreAntiBody = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OverdueID", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int OverdueID
        {
            get { return _overdueID; }
            set { _overdueID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "FirstRhTest", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string FirstRhTest
        {
            get { return _firstRhTest; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for FirstRhTest", value, value.ToString());
                _firstRhTest = value;
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
        [DataDesc(CName = "", ShortCode = "BarCodeMemo", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string BarCodeMemo
        {
            get { return _barCodeMemo; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for BarCodeMemo", value, value.ToString());
                _barCodeMemo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsCompleted", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsCompleted
        {
            get { return _isCompleted; }
            set { _isCompleted = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BPreSendID", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int BPreSendID
        {
            get { return _bPreSendID; }
            set { _bPreSendID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BPreSendTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? BPreSendTime
        {
            get { return _bPreSendTime; }
            set { _bPreSendTime = value; }
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