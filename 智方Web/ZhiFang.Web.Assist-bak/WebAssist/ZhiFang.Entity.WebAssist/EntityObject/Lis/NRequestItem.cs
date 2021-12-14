using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.WebAssist
{
	#region NRequestItem

	/// <summary>
	/// NRequestItem object for NHibernate mapped table 'NRequestItem'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "NRequestItem", ShortCode = "NRequestItem", Desc = "")]
	public class NRequestItem : BaseEntityServiceByString
	{
		#region Member Variables
		
        protected string _orderNo;
        protected string _sampleTypeNo;
        protected int _receiveFlag;
        protected string _barCode;
        protected int _itemNo;
        protected DateTime? _flagDateDelete;
        protected DateTime _itemDate;
        protected string _zdy1;
        protected string _zdy2;
        protected string _zdy3;
        protected string _zdy4;
        protected string _zdy5;
        protected int _samplingGroupNo;
        protected string _oldSerialNo;
        protected string _uniteName;
        protected string _uniteItemNo;
        protected int _isCheckFee;
        protected string _countNodesItemSource;
        protected double _charge;
        protected int _stateFlag;
        protected int _itemDispenseFlag;
        protected string _pItemCName;
        protected int _pItemNo;
        protected int _isNurseDo;
        protected int _prepaymentFlag;
        protected double _balance;
        protected string _serialNoParent;
        protected int _chargeFlag;
        protected string _sampleno;
        protected string _mergeno;
        protected string _oldParItemno;
        protected DateTime? _receiveDate;
        protected int _sectionNo;
        protected int _testTypeNo;
        protected string _nRequestItemNo;
        protected string _oldNRequestItemNo;
        protected int _checkFlag;
        protected DateTime? _checkTime;
        protected string _reportDateMemo;
        protected DateTime? _formFlagDateDelete;
        protected string _reportDateGroup;
        protected string _groupItemList;
        protected int _itemGroupNo;
        protected int _iAutoUnion;
        protected string _autoUnionSNo;
        protected long? _nItemID;
        protected int _dispOrder;
        protected int _iNeedUnionCount;
        protected int _nPItemNo;
        protected int _number;
        protected string _cancelReason;

		#endregion

		#region Constructors

		public NRequestItem() { }

		public NRequestItem( string orderNo, string sampleTypeNo, int receiveFlag, string barCode, int itemNo, DateTime flagDateDelete, DateTime itemDate, string zdy1, string zdy2, string zdy3, string zdy4, string zdy5, int samplingGroupNo, string oldSerialNo, string uniteName, string uniteItemNo, int isCheckFee, string countNodesItemSource, double charge, int stateFlag, int itemDispenseFlag, string pItemCName, int pItemNo, int isNurseDo, int prepaymentFlag, double balance, string serialNoParent, int chargeFlag, string sampleno, string mergeno, string oldParItemno, DateTime receiveDate, int sectionNo, int testTypeNo, string nRequestItemNo, string oldNRequestItemNo, int checkFlag, DateTime checkTime, string reportDateMemo, DateTime formFlagDateDelete, string reportDateGroup, string groupItemList, int itemGroupNo, int iAutoUnion, string autoUnionSNo, long nItemID, int dispOrder, int iNeedUnionCount, int nPItemNo, int number, string cancelReason )
		{
			this._orderNo = orderNo;
			this._sampleTypeNo = sampleTypeNo;
			this._receiveFlag = receiveFlag;
			this._barCode = barCode;
			this._itemNo = itemNo;
			this._flagDateDelete = flagDateDelete;
			this._itemDate = itemDate;
			this._zdy1 = zdy1;
			this._zdy2 = zdy2;
			this._zdy3 = zdy3;
			this._zdy4 = zdy4;
			this._zdy5 = zdy5;
			this._samplingGroupNo = samplingGroupNo;
			this._oldSerialNo = oldSerialNo;
			this._uniteName = uniteName;
			this._uniteItemNo = uniteItemNo;
			this._isCheckFee = isCheckFee;
			this._countNodesItemSource = countNodesItemSource;
			this._charge = charge;
			this._stateFlag = stateFlag;
			this._itemDispenseFlag = itemDispenseFlag;
			this._pItemCName = pItemCName;
			this._pItemNo = pItemNo;
			this._isNurseDo = isNurseDo;
			this._prepaymentFlag = prepaymentFlag;
			this._balance = balance;
			this._serialNoParent = serialNoParent;
			this._chargeFlag = chargeFlag;
			this._sampleno = sampleno;
			this._mergeno = mergeno;
			this._oldParItemno = oldParItemno;
			this._receiveDate = receiveDate;
			this._sectionNo = sectionNo;
			this._testTypeNo = testTypeNo;
			this._nRequestItemNo = nRequestItemNo;
			this._oldNRequestItemNo = oldNRequestItemNo;
			this._checkFlag = checkFlag;
			this._checkTime = checkTime;
			this._reportDateMemo = reportDateMemo;
			this._formFlagDateDelete = formFlagDateDelete;
			this._reportDateGroup = reportDateGroup;
			this._groupItemList = groupItemList;
			this._itemGroupNo = itemGroupNo;
			this._iAutoUnion = iAutoUnion;
			this._autoUnionSNo = autoUnionSNo;
			this._nItemID = nItemID;
			this._dispOrder = dispOrder;
			this._iNeedUnionCount = iNeedUnionCount;
			this._nPItemNo = nPItemNo;
			this._number = number;
			this._cancelReason = cancelReason;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "OrderNo", Desc = "", ContextType = SysDic.All, Length = 60)]
        public virtual string OrderNo
		{
			get { return _orderNo; }
			set
			{
				if ( value != null && value.Length > 60)
					throw new ArgumentOutOfRangeException("Invalid value for OrderNo", value, value.ToString());
				_orderNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SampleTypeNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string SampleTypeNo
		{
			get { return _sampleTypeNo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for SampleTypeNo", value, value.ToString());
				_sampleTypeNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReceiveFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ReceiveFlag
		{
			get { return _receiveFlag; }
			set { _receiveFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BarCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string BarCode
		{
			get { return _barCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for BarCode", value, value.ToString());
				_barCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ItemNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ItemNo
		{
			get { return _itemNo; }
			set { _itemNo = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "FlagDateDelete", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? FlagDateDelete
		{
			get { return _flagDateDelete; }
			set { _flagDateDelete = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ItemDate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime ItemDate
		{
			get { return _itemDate; }
			set { _itemDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Zdy1", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string Zdy1
		{
			get { return _zdy1; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for Zdy1", value, value.ToString());
				_zdy1 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Zdy2", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string Zdy2
		{
			get { return _zdy2; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for Zdy2", value, value.ToString());
				_zdy2 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Zdy3", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string Zdy3
		{
			get { return _zdy3; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for Zdy3", value, value.ToString());
				_zdy3 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Zdy4", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string Zdy4
		{
			get { return _zdy4; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for Zdy4", value, value.ToString());
				_zdy4 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Zdy5", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string Zdy5
		{
			get { return _zdy5; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for Zdy5", value, value.ToString());
				_zdy5 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SamplingGroupNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int SamplingGroupNo
		{
			get { return _samplingGroupNo; }
			set { _samplingGroupNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OldSerialNo", Desc = "", ContextType = SysDic.All, Length = 60)]
        public virtual string OldSerialNo
		{
			get { return _oldSerialNo; }
			set
			{
				if ( value != null && value.Length > 60)
					throw new ArgumentOutOfRangeException("Invalid value for OldSerialNo", value, value.ToString());
				_oldSerialNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UniteName", Desc = "", ContextType = SysDic.All, Length = 60)]
        public virtual string UniteName
		{
			get { return _uniteName; }
			set
			{
				if ( value != null && value.Length > 60)
					throw new ArgumentOutOfRangeException("Invalid value for UniteName", value, value.ToString());
				_uniteName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UniteItemNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string UniteItemNo
		{
			get { return _uniteItemNo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for UniteItemNo", value, value.ToString());
				_uniteItemNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsCheckFee", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsCheckFee
		{
			get { return _isCheckFee; }
			set { _isCheckFee = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CountNodesItemSource", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual string CountNodesItemSource
		{
			get { return _countNodesItemSource; }
			set
			{
				if ( value != null && value.Length > 1)
					throw new ArgumentOutOfRangeException("Invalid value for CountNodesItemSource", value, value.ToString());
				_countNodesItemSource = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Charge", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double Charge
		{
			get { return _charge; }
			set { _charge = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "StateFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int StateFlag
		{
			get { return _stateFlag; }
			set { _stateFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ItemDispenseFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ItemDispenseFlag
		{
			get { return _itemDispenseFlag; }
			set { _itemDispenseFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PItemCName", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string PItemCName
		{
			get { return _pItemCName; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for PItemCName", value, value.ToString());
				_pItemCName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PItemNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int PItemNo
		{
			get { return _pItemNo; }
			set { _pItemNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsNurseDo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsNurseDo
		{
			get { return _isNurseDo; }
			set { _isNurseDo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PrepaymentFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int PrepaymentFlag
		{
			get { return _prepaymentFlag; }
			set { _prepaymentFlag = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Balance", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double Balance
		{
			get { return _balance; }
			set { _balance = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SerialNoParent", Desc = "", ContextType = SysDic.All, Length = 60)]
        public virtual string SerialNoParent
		{
			get { return _serialNoParent; }
			set
			{
				if ( value != null && value.Length > 60)
					throw new ArgumentOutOfRangeException("Invalid value for SerialNoParent", value, value.ToString());
				_serialNoParent = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ChargeFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ChargeFlag
		{
			get { return _chargeFlag; }
			set { _chargeFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Sampleno", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Sampleno
		{
			get { return _sampleno; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Sampleno", value, value.ToString());
				_sampleno = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Mergeno", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Mergeno
		{
			get { return _mergeno; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Mergeno", value, value.ToString());
				_mergeno = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OldParItemno", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string OldParItemno
		{
			get { return _oldParItemno; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for OldParItemno", value, value.ToString());
				_oldParItemno = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ReceiveDate", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ReceiveDate
		{
			get { return _receiveDate; }
			set { _receiveDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SectionNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int SectionNo
		{
			get { return _sectionNo; }
			set { _sectionNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TestTypeNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int TestTypeNo
		{
			get { return _testTypeNo; }
			set { _testTypeNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "NRequestItemNo", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string NRequestItemNo
		{
			get { return _nRequestItemNo; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for NRequestItemNo", value, value.ToString());
				_nRequestItemNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OldNRequestItemNo", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string OldNRequestItemNo
		{
			get { return _oldNRequestItemNo; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for OldNRequestItemNo", value, value.ToString());
				_oldNRequestItemNo = value;
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
        [DataDesc(CName = "", ShortCode = "CheckTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? CheckTime
		{
			get { return _checkTime; }
			set { _checkTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReportDateMemo", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string ReportDateMemo
		{
			get { return _reportDateMemo; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for ReportDateMemo", value, value.ToString());
				_reportDateMemo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "FormFlagDateDelete", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? FormFlagDateDelete
		{
			get { return _formFlagDateDelete; }
			set { _formFlagDateDelete = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReportDateGroup", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ReportDateGroup
		{
			get { return _reportDateGroup; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ReportDateGroup", value, value.ToString());
				_reportDateGroup = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "GroupItemList", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string GroupItemList
		{
			get { return _groupItemList; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for GroupItemList", value, value.ToString());
				_groupItemList = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ItemGroupNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ItemGroupNo
		{
			get { return _itemGroupNo; }
			set { _itemGroupNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IAutoUnion", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IAutoUnion
		{
			get { return _iAutoUnion; }
			set { _iAutoUnion = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AutoUnionSNo", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string AutoUnionSNo
		{
			get { return _autoUnionSNo; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for AutoUnionSNo", value, value.ToString());
				_autoUnionSNo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "NItemID", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? NItemID
		{
			get { return _nItemID; }
			set { _nItemID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "INeedUnionCount", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int INeedUnionCount
		{
			get { return _iNeedUnionCount; }
			set { _iNeedUnionCount = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "NPItemNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int NPItemNo
		{
			get { return _nPItemNo; }
			set { _nPItemNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Number", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Number
		{
			get { return _number; }
			set { _number = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CancelReason", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string CancelReason
		{
			get { return _cancelReason; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for CancelReason", value, value.ToString());
				_cancelReason = value;
			}
		}

        
		#endregion
	}
	#endregion
}