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
    [DataDesc(CName = "配血主单", ClassCName = "BloodBPreForm", ShortCode = "BloodBPreForm", Desc = "配血主单")]
	public class BloodBPreForm : BaseEntity
	{
		#region Member Variables
		
        protected string _yqCode;
        protected string _bPreFormNo;
        protected int _bPreCheckFlag;
        protected string _barCode;
        protected int _isCharge;
        protected int _outFlag;
        protected long? _operatorID;
        protected string _operator;
        protected DateTime? _operTime;
        protected long? _checkID;
        protected string _checker;
        protected DateTime? _checkTime;
        protected string _bloodOrder;
        protected string _bPreAntiBody;
        protected string _firstABOWay;
        protected string _firstABOTest;
        protected long? _overdueID;
        protected string _overdueCName;
        protected string _barCodeMemo;
        protected string _firstRhTest;
        protected int _isCompleted;
        protected DateTime? _bPreSendTime;
        protected long? _bPreSendID;
        protected string _bPreSendCName;
        protected DateTime? _printTime;
        protected int _printCount;
        protected string _memo;
        protected bool _visible;
        protected int _dispOrder;
		protected BloodBReqForm _bloodBReqForm;
		protected BloodBReqItem _bloodBReqItem;
		protected BloodPatinfo _bloodPatinfo;
		protected BloodQtyDtl _bloodQtyDtl;

		#endregion

		#region Constructors

		public BloodBPreForm() { }

		public BloodBPreForm( long labID, string yqCode, string bPreFormNo, int bPreCheckFlag, string barCode, int isCharge, int outFlag, long operatorID, string operator1, DateTime operTime, long checkID, string checker, DateTime checkTime, string bloodOrder, string bPreAntiBody, string firstABOWay, string firstABOTest, long overdueID, string overdueCName, string barCodeMemo, string firstRhTest, int isCompleted, DateTime bPreSendTime, long bPreSendID, string bPreSendCName, DateTime printTime, int printCount, string memo, bool visible, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp, BloodBReqForm bloodBReqForm, BloodBReqItem bloodBReqItem, BloodPatinfo bloodPatinfo, BloodQtyDtl bloodQtyDtl )
		{
			this._labID = labID;
			this._yqCode = yqCode;
			this._bPreFormNo = bPreFormNo;
			this._bPreCheckFlag = bPreCheckFlag;
			this._barCode = barCode;
			this._isCharge = isCharge;
			this._outFlag = outFlag;
			this._operatorID = operatorID;
			this._operator = operator1;
			this._operTime = operTime;
			this._checkID = checkID;
			this._checker = checker;
			this._checkTime = checkTime;
			this._bloodOrder = bloodOrder;
			this._bPreAntiBody = bPreAntiBody;
			this._firstABOWay = firstABOWay;
			this._firstABOTest = firstABOTest;
			this._overdueID = overdueID;
			this._overdueCName = overdueCName;
			this._barCodeMemo = barCodeMemo;
			this._firstRhTest = firstRhTest;
			this._isCompleted = isCompleted;
			this._bPreSendTime = bPreSendTime;
			this._bPreSendID = bPreSendID;
			this._bPreSendCName = bPreSendCName;
			this._printTime = printTime;
			this._printCount = printCount;
			this._memo = memo;
			this._visible = visible;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bloodBReqForm = bloodBReqForm;
			this._bloodBReqItem = bloodBReqItem;
			this._bloodPatinfo = bloodPatinfo;
			this._bloodQtyDtl = bloodQtyDtl;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "院区编号", ShortCode = "YqCode", Desc = "院区编号", ContextType = SysDic.All, Length = 20)]
        public virtual string YqCode
		{
			get { return _yqCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for YqCode", value, value.ToString());
				_yqCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "配血单号", ShortCode = "BPreFormNo", Desc = "配血单号", ContextType = SysDic.All, Length = 20)]
        public virtual string BPreFormNo
		{
			get { return _bPreFormNo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for BPreFormNo", value, value.ToString());
				_bPreFormNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "标志", ShortCode = "BPreCheckFlag", Desc = "标志", ContextType = SysDic.All, Length = 4)]
        public virtual int BPreCheckFlag
		{
			get { return _bPreCheckFlag; }
			set { _bPreCheckFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "标本号", ShortCode = "BarCode", Desc = "标本号", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "是否收费", ShortCode = "IsCharge", Desc = "是否收费", ContextType = SysDic.All, Length = 4)]
        public virtual int IsCharge
		{
			get { return _isCharge; }
			set { _isCharge = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否完全出库", ShortCode = "OutFlag", Desc = "是否完全出库", ContextType = SysDic.All, Length = 4)]
        public virtual int OutFlag
		{
			get { return _outFlag; }
			set { _outFlag = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "配血者ID", ShortCode = "OperatorID", Desc = "配血者ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? OperatorID
		{
			get { return _operatorID; }
			set { _operatorID = value; }
		}

        [DataMember]
        [DataDesc(CName = "配血者", ShortCode = "Operator", Desc = "配血者", ContextType = SysDic.All, Length = 50)]
        public virtual string Operator
		{
			get { return _operator; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Operator", value, value.ToString());
				_operator = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "配血时间", ShortCode = "OperTime", Desc = "配血时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? OperTime
		{
			get { return _operTime; }
			set { _operTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审定者ID", ShortCode = "CheckID", Desc = "审定者ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? CheckID
		{
			get { return _checkID; }
			set { _checkID = value; }
		}

        [DataMember]
        [DataDesc(CName = "审定者", ShortCode = "Checker", Desc = "审定者", ContextType = SysDic.All, Length = 50)]
        public virtual string Checker
		{
			get { return _checker; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Checker", value, value.ToString());
				_checker = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审定时间", ShortCode = "CheckTime", Desc = "审定时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? CheckTime
		{
			get { return _checkTime; }
			set { _checkTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "子号", ShortCode = "BloodOrder", Desc = "子号", ContextType = SysDic.All, Length = 10)]
        public virtual string BloodOrder
		{
			get { return _bloodOrder; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for BloodOrder", value, value.ToString());
				_bloodOrder = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "抗体筛选结果", ShortCode = "BPreAntiBody", Desc = "抗体筛选结果", ContextType = SysDic.All, Length = 20)]
        public virtual string BPreAntiBody
		{
			get { return _bPreAntiBody; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for BPreAntiBody", value, value.ToString());
				_bPreAntiBody = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "初检ABO血型检验方法", ShortCode = "FirstABOWay", Desc = "初检ABO血型检验方法", ContextType = SysDic.All, Length = 20)]
        public virtual string FirstABOWay
		{
			get { return _firstABOWay; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for FirstABOWay", value, value.ToString());
				_firstABOWay = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "初检ABO血型", ShortCode = "FirstABOTest", Desc = "初检ABO血型", ContextType = SysDic.All, Length = 20)]
        public virtual string FirstABOTest
		{
			get { return _firstABOTest; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for FirstABOTest", value, value.ToString());
				_firstABOTest = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "配血条码备注操作者Id", ShortCode = "OverdueID", Desc = "配血条码备注操作者Id", ContextType = SysDic.All, Length = 8)]
		public virtual long? OverdueID
		{
			get { return _overdueID; }
			set { _overdueID = value; }
		}

        [DataMember]
        [DataDesc(CName = "配血条码备注操作者", ShortCode = "OverdueCName", Desc = "配血条码备注操作者", ContextType = SysDic.All, Length = 50)]
        public virtual string OverdueCName
		{
			get { return _overdueCName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for OverdueCName", value, value.ToString());
				_overdueCName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "配血标本备注", ShortCode = "BarCodeMemo", Desc = "配血标本备注", ContextType = SysDic.All, Length = 50)]
        public virtual string BarCodeMemo
		{
			get { return _barCodeMemo; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for BarCodeMemo", value, value.ToString());
				_barCodeMemo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "初检RH血型", ShortCode = "FirstRhTest", Desc = "初检RH血型", ContextType = SysDic.All, Length = 20)]
        public virtual string FirstRhTest
		{
			get { return _firstRhTest; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for FirstRhTest", value, value.ToString());
				_firstRhTest = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "是否完成", ShortCode = "IsCompleted", Desc = "是否完成", ContextType = SysDic.All, Length = 4)]
        public virtual int IsCompleted
		{
			get { return _isCompleted; }
			set { _isCompleted = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "扫描时间", ShortCode = "BPreSendTime", Desc = "扫描时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? BPreSendTime
		{
			get { return _bPreSendTime; }
			set { _bPreSendTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "扫描者ID", ShortCode = "BPreSendID", Desc = "扫描者ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? BPreSendID
		{
			get { return _bPreSendID; }
			set { _bPreSendID = value; }
		}

        [DataMember]
        [DataDesc(CName = "扫描者", ShortCode = "BPreSendCName", Desc = "扫描者", ContextType = SysDic.All, Length = 50)]
        public virtual string BPreSendCName
		{
			get { return _bPreSendCName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for BPreSendCName", value, value.ToString());
				_bPreSendCName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "打印时间", ShortCode = "PrintTime", Desc = "打印时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? PrintTime
		{
			get { return _printTime; }
			set { _printTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "打印次数", ShortCode = "PrintCount", Desc = "打印次数", ContextType = SysDic.All, Length = 4)]
        public virtual int PrintCount
		{
			get { return _printCount; }
			set { _printCount = value; }
		}

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = 500)]
        public virtual string Memo
		{
			get { return _memo; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for Memo", value, value.ToString());
				_memo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "Visible", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
		{
			get { return _visible; }
			set { _visible = value; }
		}

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "用血申请主单表", ShortCode = "BloodBReqForm", Desc = "用血申请主单表")]
		public virtual BloodBReqForm BloodBReqForm
		{
			get { return _bloodBReqForm; }
			set { _bloodBReqForm = value; }
		}

        [DataMember]
        [DataDesc(CName = "用血申请明细信息表", ShortCode = "BloodBReqItem", Desc = "用血申请明细信息表")]
		public virtual BloodBReqItem BloodBReqItem
		{
			get { return _bloodBReqItem; }
			set { _bloodBReqItem = value; }
		}

        [DataMember]
        [DataDesc(CName = "病人就诊记录信息表", ShortCode = "BloodPatinfo", Desc = "病人就诊记录信息表")]
		public virtual BloodPatinfo BloodPatinfo
		{
			get { return _bloodPatinfo; }
			set { _bloodPatinfo = value; }
		}

        [DataMember]
        [DataDesc(CName = "库存表", ShortCode = "BloodQtyDtl", Desc = "库存表")]
		public virtual BloodQtyDtl BloodQtyDtl
		{
			get { return _bloodQtyDtl; }
			set { _bloodQtyDtl = value; }
		}

        
		#endregion
	}
	#endregion
}