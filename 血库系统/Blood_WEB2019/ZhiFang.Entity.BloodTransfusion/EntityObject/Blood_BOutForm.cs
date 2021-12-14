using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
    #region BloodBOutForm

    /// <summary>
    /// BloodBOutForm object for NHibernate mapped table 'Blood_BOutForm'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BloodBOutForm", ShortCode = "BloodBOutForm", Desc = "")]
    public class BloodBOutForm : BaseEntityServiceByString
    {
        #region Member Variables

        //protected string _bReqFormID;
        //protected string _bPreFormID;
        protected DateTime _operTime;
        protected string _operatorID;
        protected string _checkID;
        protected DateTime? _checkTime;
        protected string _outType;
        protected int _checkFlag;
        protected DateTime? _printTime;
        protected int _printCount;
        protected string _memo;
        protected string _zX1;
        protected string _zX2;
        protected string _zX3;
        protected string _bReqItemID;
        protected string _bloodOrder;
        protected string _patID;
        protected string _yqCode;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected bool _visible;
        protected int _confirmCompletion;
        protected int _handoverCompletion;
        protected int _courseCompletion;
        protected int _recoverCompletion;

        protected string _endBloodOperName;
        protected long? _endBloodOperId;
        protected DateTime? _endBloodOperTime;
        protected string _endBloodReason;
        protected BDict _bDEndBReason;
        
        protected BloodBReqForm _bloodBReqForm;
        protected BloodBPreForm _bloodBPreForm;
        #endregion

        #region Constructors

        public BloodBOutForm() { }
        public BloodBOutForm(DateTime operTime, string operatorID, string checkID, DateTime checkTime, string outType, int checkFlag, DateTime printTime, int printCount, string memo, string zX1, string zX2, string zX3, string bReqItemID, string bloodOrder, string patID, string yqCode, long labID, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, bool visible, int confirmCompletion, int handoverCompletion, int courseCompletion, int recoverCompletion, BloodBReqForm bloodBReqForm, BloodBPreForm bloodBPreForm)
        {
            //this._bReqFormID = bReqFormID;
            this._bloodBReqForm = bloodBReqForm;
            this._bloodBPreForm = bloodBPreForm;
            //this._bPreFormID = bPreFormID;
            this._operTime = operTime;
            this._operatorID = operatorID;
            this._checkID = checkID;
            this._checkTime = checkTime;
            this._outType = outType;
            this._checkFlag = checkFlag;
            this._printTime = printTime;
            this._printCount = printCount;
            this._memo = memo;
            this._zX1 = zX1;
            this._zX2 = zX2;
            this._zX3 = zX3;
            this._bReqItemID = bReqItemID;
            this._bloodOrder = bloodOrder;
            this._patID = patID;
            this._yqCode = yqCode;
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
        [DataDesc(CName = "ÖÕÖ¹ÊäÑªÔ­Òò×Öµä", ShortCode = "BDEndBReason", Desc = "ÖÕÖ¹ÊäÑªÔ­Òò×Öµä")]
        public virtual BDict BDEndBReason
        {
            get { return _bDEndBReason; }
            set { _bDEndBReason = value; }
        }
        [DataMember]
        [DataDesc(CName = "BloodBPreForm", ShortCode = "BloodBPreForm", Desc = "BloodBPreForm")]
        public virtual BloodBPreForm BloodBPreForm
        {
            get { return _bloodBPreForm; }
            set { _bloodBPreForm = value; }
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

        //[DataMember]
        //[DataDesc(CName = "", ShortCode = "BPreFormID", Desc = "", ContextType = SysDic.All, Length = 40)]
        //public virtual string BPreFormID
        //{
        //    get { return _bPreFormID; }
        //    set
        //    {
        //        if (value != null && value.Length > 40)
        //            throw new ArgumentOutOfRangeException("Invalid value for BPreFormID", value, value.ToString());
        //        _bPreFormID = value;
        //    }
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
        [DataDesc(CName = "", ShortCode = "OperatorID", Desc = "", ContextType = SysDic.All, Length = 520)]
        public virtual string OperatorID
        {
            get { return _operatorID; }
            set
            {
                if (value != null && value.Length > 520)
                    throw new ArgumentOutOfRangeException("Invalid value for OperatorID", value, value.ToString());
                _operatorID = value;
            }
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

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OutType", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string OutType
        {
            get { return _outType; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for OutType", value, value.ToString());
                _outType = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CheckFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int CheckFlag
        {
            get { return _checkFlag; }
            set { _checkFlag = value; }
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
        [DataDesc(CName = "", ShortCode = "BReqItemID", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string BReqItemID
        {
            get { return _bReqItemID; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for BReqItemID", value, value.ToString());
                _bReqItemID = value;
            }
        }

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
        [DataDesc(CName = "", ShortCode = "PatID", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string PatID
        {
            get { return _patID; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for PatID", value, value.ToString());
                _patID = value;
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

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "ÖÕÖ¹ÊäÑª²Ù×÷ÈËId", ShortCode = "EndBloodOperId", Desc = "ÖÕÖ¹ÊäÑª²Ù×÷ÈËId", ContextType = SysDic.All, Length = 8)]
        public virtual long? EndBloodOperId
        {
            get { return _endBloodOperId; }
            set { _endBloodOperId = value; }
        }
        [DataMember]
        [DataDesc(CName = "ÖÕÖ¹ÊäÑª²Ù×÷ÈË", ShortCode = "EndBloodOperName", Desc = "ÖÕÖ¹ÊäÑª²Ù×÷ÈË", ContextType = SysDic.All, Length = 50)]
        public virtual string EndBloodOperName
        {
            get { return _endBloodOperName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for EndBloodOperName", value, value.ToString());
                _endBloodOperName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "ÖÕÖ¹ÊäÑª²Ù×÷Ê±¼ä", ShortCode = "EndBloodOperTime", Desc = "ÖÕÖ¹ÊäÑª²Ù×÷Ê±¼ä", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? EndBloodOperTime
        {
            get { return _endBloodOperTime; }
            set { _endBloodOperTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "ÖÕÖ¹ÊäÑªÔ­Òò", ShortCode = "EndBloodReason", Desc = "ÖÕÖ¹ÊäÑªÔ­Òò", ContextType = SysDic.All, Length = 500)]
        public virtual string EndBloodReason
        {
            get { return _endBloodReason; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for EndBloodReason", value, value.ToString());
                _endBloodReason = value;
            }
        }

        #endregion

        #region ×Ô¶¨Òå

        protected double _dtlTotal;
        protected string _operator;

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "DtlTotal", ShortCode = "DtlTotal", Desc = "·¢Ñªµ¥µÄÑªÒº×Ü´üÊý", ContextType = SysDic.All)]
        public virtual double DtlTotal
        {
            get { return _dtlTotal; }
            set { _dtlTotal = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Operator", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Operator
        {
            get { return _operator; }
            set
            {
                _operator = value;
            }
        }
        public BloodBOutForm(BloodBOutForm bloodboutform, BloodBReqForm bloodbreqform)
        {
            this._bloodBReqForm = bloodbreqform;
            this._bloodBPreForm = bloodboutform.BloodBPreForm;

            this.Id = bloodboutform.Id;
            this._operTime = bloodboutform.OperTime;
            this._operatorID = bloodboutform.OperatorID;
            this._checkID = bloodboutform.CheckID;
            this._checkTime = bloodboutform.CheckTime;
            this._outType = bloodboutform.OutType;
            this._checkFlag = bloodboutform.CheckFlag;
            this._printTime = bloodboutform.PrintTime;
            this._printCount = bloodboutform.PrintCount;
            this._memo = bloodboutform.Memo;
            this._zX1 = bloodboutform.ZX1;
            this._zX2 = bloodboutform.ZX2;
            this._zX3 = bloodboutform.ZX3;
            this._bReqItemID = bloodboutform.BReqItemID;
            this._bloodOrder = bloodboutform.BloodOrder;
            this._patID = bloodboutform.PatID;
            this._yqCode = bloodboutform.YqCode;
            this._labID = bloodboutform.LabID;
            this._dispOrder = bloodboutform.DispOrder;
            this._dataAddTime = bloodboutform.DataAddTime;
            this._dataUpdateTime = bloodboutform.DataUpdateTime;
            this._dataTimeStamp = bloodboutform.DataTimeStamp;
            this._visible = bloodboutform.Visible;
            this._confirmCompletion = bloodboutform.ConfirmCompletion;
            this._handoverCompletion = bloodboutform.HandoverCompletion;
            this._courseCompletion = bloodboutform.CourseCompletion;
            this._recoverCompletion = bloodboutform.RecoverCompletion;

            this._endBloodOperId = bloodboutform.EndBloodOperId;
            this._endBloodOperTime = bloodboutform.EndBloodOperTime;
            this._endBloodOperName = bloodboutform.EndBloodOperName;
            this._endBloodReason = bloodboutform.EndBloodReason;
            this._bDEndBReason = bloodboutform.BDEndBReason;
        }
        #endregion
    }
    #endregion
}