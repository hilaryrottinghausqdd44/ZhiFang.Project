using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
    #region BloodBReqItem

    /// <summary>
    /// BloodBReqItem object for NHibernate mapped table 'Blood_BReqItem'.
    /// BReqFormID及BloodNo作为联合主键
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BloodBReqItem", ShortCode = "BloodBReqItem", Desc = "")]
    public class BloodBReqItem : BaseEntityService
    {
        #region Member Variables
        //protected long? _bReqItemID;
        protected string _bReqFormID;
        protected int? _bloodNo;
        protected double? _bReqCount;
        protected double? _bPreCount;
        protected double? _bOutCount;
        protected int? _bloodUnitNo;
        protected int? _bloodOrder;
        protected int? _bPresOutFlag;
        protected string _memo;
        protected string _zX1;
        protected string _zX2;
        protected string _zX3;
        protected int? _auditflag;
        protected string _barcode;
        protected string _b3Code;
        protected string _mainSignID;
        protected DateTime? _mainSigndate;
        protected string _deptSignID;
        protected DateTime? _deptSigndate;
        protected int? _bReqTypeItemID;
        protected DateTime? _reqTimeItem;
        protected string _bCCode;
        protected string _bCNo;
        protected int? _bPreCheckFlag;
        protected double? _getCount;
        protected string _zlxmbm;
        protected int? _dispOrder;
        protected bool _visible;

        #endregion

        #region Constructors

        public BloodBReqItem() { }
        public BloodBReqItem(string bReqFormID, int bloodNo, double bReqCount, double bPreCount, double bOutCount, int bloodUnitNo, int bloodOrder, int bPresOutFlag, string memo, string zX1, string zX2, string zX3, int auditflag, string barcode, string b3Code, string mainSignID, DateTime mainSigndate, string deptSignID, DateTime deptSigndate, int bReqTypeItemID, DateTime reqTimeItem, string bCCode, string bCNo, int bPreCheckFlag, double getCount, string zlxmbm, long labID, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp, bool visible)
        {
            
            this._bReqFormID = bReqFormID;
            this._bloodNo = bloodNo;
            this._bReqCount = bReqCount;
            this._bPreCount = bPreCount;
            this._bOutCount = bOutCount;
            this._bloodUnitNo = bloodUnitNo;
            this._bloodOrder = bloodOrder;
            this._bPresOutFlag = bPresOutFlag;
            this._memo = memo;
            this._zX1 = zX1;
            this._zX2 = zX2;
            this._zX3 = zX3;
            this._auditflag = auditflag;
            this._barcode = barcode;
            this._b3Code = b3Code;
            this._mainSignID = mainSignID;
            this._mainSigndate = mainSigndate;
            this._deptSignID = deptSignID;
            this._deptSigndate = deptSigndate;
            this._bReqTypeItemID = bReqTypeItemID;
            this._reqTimeItem = reqTimeItem;
            this._bCCode = bCCode;
            this._bCNo = bCNo;
            this._bPreCheckFlag = bPreCheckFlag;
            this._getCount = getCount;
            this._zlxmbm = zlxmbm;
            this._labID = labID;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
            this._visible = visible;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [DataDesc(CName = "BReqFormID(联合主键1)", ShortCode = "BReqFormID", Desc = "BReqFormID(联合主键1)", ContextType = SysDic.All, Length = 40)]
        public virtual string BReqFormID
        {
            get { return _bReqFormID; }
            set { _bReqFormID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "联合主键2", ShortCode = "BloodOrder", Desc = "联合主键2", ContextType = SysDic.All, Length = 4)]
        public virtual int? BloodOrder
        {
            get { return _bloodOrder; }
            set { _bloodOrder = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "BloodNo", ShortCode = "BloodNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int? BloodNo
        {
            get { return _bloodNo; }
            set { _bloodNo = value; }
        }

        //[DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        //[DataDesc(CName = "自增", ShortCode = "BReqItemID", Desc = "自增", ContextType = SysDic.All, Length = 8)]
        //public virtual long? BReqItemID
        //{
        //    get { return _bReqItemID; }
        //    set { _bReqItemID = value; }
        //}


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BReqCount", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double? BReqCount
        {
            get { return _bReqCount; }
            set { _bReqCount = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BPreCount", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double? BPreCount
        {
            get { return _bPreCount; }
            set { _bPreCount = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BOutCount", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double? BOutCount
        {
            get { return _bOutCount; }
            set { _bOutCount = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BloodUnitNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int? BloodUnitNo
        {
            get { return _bloodUnitNo; }
            set { _bloodUnitNo = value; }
        }


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BPresOutFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int? BPresOutFlag
        {
            get { return _bPresOutFlag; }
            set { _bPresOutFlag = value; }
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
        [DataDesc(CName = "", ShortCode = "Auditflag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int? Auditflag
        {
            get { return _auditflag; }
            set { _auditflag = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Barcode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Barcode
        {
            get { return _barcode; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Barcode", value, value.ToString());
                _barcode = value;
            }
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
        [DataDesc(CName = "", ShortCode = "MainSignID", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string MainSignID
        {
            get { return _mainSignID; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for MainSignID", value, value.ToString());
                _mainSignID = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "MainSigndate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? MainSigndate
        {
            get { return _mainSigndate; }
            set { _mainSigndate = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DeptSignID", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string DeptSignID
        {
            get { return _deptSignID; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for DeptSignID", value, value.ToString());
                _deptSignID = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DeptSigndate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DeptSigndate
        {
            get { return _deptSigndate; }
            set { _deptSigndate = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BReqTypeItemID", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int? BReqTypeItemID
        {
            get { return _bReqTypeItemID; }
            set { _bReqTypeItemID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ReqTimeItem", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ReqTimeItem
        {
            get { return _reqTimeItem; }
            set { _reqTimeItem = value; }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BPreCheckFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int? BPreCheckFlag
        {
            get { return _bPreCheckFlag; }
            set { _bPreCheckFlag = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "GetCount", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double? GetCount
        {
            get { return _getCount; }
            set { _getCount = value; }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int? DispOrder
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

        #region 自定义属性

        protected BloodBReqForm _bloodBReqForm;
        protected Bloodstyle _bloodstyle;
        protected BloodClass _bloodClass;
        protected BloodUnit _bloodUnit;

        protected string _bloodclassCName;
        protected string _bloodCName;
        protected string _bloodUnitCName;

        [DataMember]
        [DataDesc(CName = "BloodBReqForm", ShortCode = "BloodBReqForm", Desc = "BloodBReqForm")]
        public virtual BloodBReqForm BloodBReqForm
        {
            get { return _bloodBReqForm; }
            set { _bloodBReqForm = value; }
        }
        [DataMember]
        [DataDesc(CName = "Bloodstyle", ShortCode = "Bloodstyle", Desc = "Bloodstyle")]
        public virtual Bloodstyle Bloodstyle
        {
            get { return _bloodstyle; }
            set { _bloodstyle = value; }
        }
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
        [DataDesc(CName = "血制品类型名称", ShortCode = "BloodclassCName", Desc = "血制品类型名称", ContextType = SysDic.All, Length = 50)]
        public virtual string BloodclassCName
        {
            get { return _bloodclassCName; }
            set
            {
                _bloodclassCName = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "血制品名称", ShortCode = "BloodCName", Desc = "血制品名称", ContextType = SysDic.All, Length = 50)]
        public virtual string BloodCName
        {
            get { return _bloodCName; }
            set
            {
                _bloodCName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "血制品单位", ShortCode = "BloodUnitCName", Desc = "血制品单位", ContextType = SysDic.All, Length = 50)]
        public virtual string BloodUnitCName
        {
            get { return _bloodUnitCName; }
            set
            {
                _bloodUnitCName = value;
            }
        }
        public BloodBReqItem(BloodBReqForm bloodbreqform, BloodBReqItem bloodbreqitem, BloodClass bloodclass, Bloodstyle bloodstyle, BloodUnit bloodunit)
        {
            _setBloodBReqItem(bloodbreqitem);

            this._bloodBReqForm = bloodbreqform;
            this._bloodstyle = bloodstyle;
            this._bloodClass = bloodclass;
            this._bloodUnit = bloodunit;

            this._bloodclassCName = bloodclass.CName;
            this._bloodCName = bloodstyle.CName;
            this._bloodUnitCName = bloodunit.CName;
        }
        public BloodBReqItem(BloodBReqItem bloodbreqitem, BloodClass bloodclass, Bloodstyle bloodstyle, BloodUnit bloodunit)
        {
            _setBloodBReqItem(bloodbreqitem);

            this._bloodstyle = bloodstyle;
            this._bloodClass = bloodclass;
            this._bloodUnit = bloodunit;

            this._bloodclassCName = bloodclass.CName;
            this._bloodCName = bloodstyle.CName;
            this._bloodUnitCName = bloodunit.CName;
        }
        private void _setBloodBReqItem(BloodBReqItem bloodbreqitem)
        {
            this._id = bloodbreqitem.Id;
            this._bReqFormID = bloodbreqitem.BReqFormID;
            this._bloodNo = bloodbreqitem.BloodNo;
            this._bReqCount = bloodbreqitem.BReqCount;
            this._bPreCount = bloodbreqitem.BPreCount;
            this._bOutCount = bloodbreqitem.BOutCount;
            this._bloodUnitNo = bloodbreqitem.BloodUnitNo;
            this._bloodOrder = bloodbreqitem.BloodOrder;
            this._bPresOutFlag = bloodbreqitem.BPresOutFlag;
            this._memo = bloodbreqitem.Memo;
            this._zX1 = bloodbreqitem.ZX1;
            this._zX2 = bloodbreqitem.ZX2;
            this._zX3 = bloodbreqitem.ZX3;
            this._auditflag = bloodbreqitem.Auditflag;
            this._barcode = bloodbreqitem.Barcode;
            this._b3Code = bloodbreqitem.B3Code;
            this._mainSignID = bloodbreqitem.MainSignID;
            this._mainSigndate = bloodbreqitem.MainSigndate;
            this._deptSignID = bloodbreqitem.DeptSignID;
            this._deptSigndate = bloodbreqitem.DeptSigndate;
            this._bReqTypeItemID = bloodbreqitem.BReqTypeItemID;
            this._reqTimeItem = bloodbreqitem.ReqTimeItem;
            this._bCCode = bloodbreqitem.BCCode;
            this._bCNo = bloodbreqitem.BCNo;
            this._bPreCheckFlag = bloodbreqitem.BPreCheckFlag;
            this._getCount = bloodbreqitem.GetCount;
            this._zlxmbm = bloodbreqitem.Zlxmbm;
            this._labID = bloodbreqitem.LabID;
            this._dispOrder = bloodbreqitem.DispOrder;
            this._dataAddTime = bloodbreqitem.DataAddTime;
            this._dataTimeStamp = bloodbreqitem.DataTimeStamp;
            this._visible = bloodbreqitem.Visible;
        }

        #endregion
    }
    #endregion
}