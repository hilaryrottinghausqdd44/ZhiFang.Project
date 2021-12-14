using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodQtyDtl

	/// <summary>
	/// BloodQtyDtl object for NHibernate mapped table 'Blood_QtyDtl'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "库存表", ClassCName = "BloodQtyDtl", ShortCode = "BloodQtyDtl", Desc = "库存表")]
	public class BloodQtyDtl : BaseEntity
	{
		#region Member Variables
		
        protected string _bframeNo;
        protected string _bINo;
        protected string _bBagCode;
        protected string _pCode;
        protected string _b3Code;
        protected double _bCount;
        protected DateTime _invalidDate;
        protected DateTime? _collectDate;
        protected double _price;
        protected double _bTotalCount;
        protected double _calcTotalCount;
        protected string _bBagExCode;
        protected DateTime? _bIWarnDate;
        protected DateTime? _bIDate;
        protected int _useFlag;
        protected int _hflag;
        protected long? _reviewBloodAboNo;
        protected long? _reviewId;
        protected long? _reviewCName;
        protected DateTime? _reviewTime;
        protected int _invFlag;
        protected string _bCCode;
        protected string _yqCode;
        protected DateTime? _warndate;
        protected string _bloodoC;
        protected int _ismulflag;
        protected bool _preflag;
        protected bool _bagCheckFlag;
        protected string _memo;
        protected bool _visible;
        protected int _dispOrder;
		protected BloodABO _bloodABO;
		protected BloodBInItem _bloodBInItem;
		protected BloodClass _bloodClass;
		protected BloodStyle _bloodStyle;
		protected BloodUnit _bloodUnit;
		protected BDict _bBankNo;
		protected BDict _iceboxNo;
		protected BDict _storeCondNo;

		#endregion

		#region Constructors

		public BloodQtyDtl() { }

		public BloodQtyDtl( long labID, string bframeNo, string bINo, string bBagCode, string pCode, string b3Code, double bCount, DateTime invalidDate, DateTime collectDate, double price, double bTotalCount, double calcTotalCount, string bBagExCode, DateTime bIWarnDate, DateTime bIDate, int useFlag, int hflag, long reviewBloodAboNo, long reviewId, long reviewCName, DateTime reviewTime, int invFlag, string bCCode, string yqCode, DateTime warndate, string bloodoC, int ismulflag, bool preflag, bool bagCheckFlag, string memo, bool visible, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp, BloodABO blood, BloodBInItem bloodBInItem, BloodClass bloodClass, BloodStyle bloodStyle, BloodUnit bloodUnit, BDict bBankNo, BDict iceboxNo, BDict storeCondNo )
		{
			this._labID = labID;
			this._bframeNo = bframeNo;
			this._bINo = bINo;
			this._bBagCode = bBagCode;
			this._pCode = pCode;
			this._b3Code = b3Code;
			this._bCount = bCount;
			this._invalidDate = invalidDate;
			this._collectDate = collectDate;
			this._price = price;
			this._bTotalCount = bTotalCount;
			this._calcTotalCount = calcTotalCount;
			this._bBagExCode = bBagExCode;
			this._bIWarnDate = bIWarnDate;
			this._bIDate = bIDate;
			this._useFlag = useFlag;
			this._hflag = hflag;
			this._reviewBloodAboNo = reviewBloodAboNo;
			this._reviewId = reviewId;
			this._reviewCName = reviewCName;
			this._reviewTime = reviewTime;
			this._invFlag = invFlag;
			this._bCCode = bCCode;
			this._yqCode = yqCode;
			this._warndate = warndate;
			this._bloodoC = bloodoC;
			this._ismulflag = ismulflag;
			this._preflag = preflag;
			this._bagCheckFlag = bagCheckFlag;
			this._memo = memo;
			this._visible = visible;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bloodABO = blood;
			this._bloodBInItem = bloodBInItem;
			this._bloodClass = bloodClass;
			this._bloodStyle = bloodStyle;
			this._bloodUnit = bloodUnit;
			this._bBankNo = bBankNo;
			this._iceboxNo = iceboxNo;
			this._storeCondNo = storeCondNo;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "架号", ShortCode = "BframeNo", Desc = "架号", ContextType = SysDic.All, Length = 20)]
        public virtual string BframeNo
		{
			get { return _bframeNo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for BframeNo", value, value.ToString());
				_bframeNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "位号", ShortCode = "BINo", Desc = "位号", ContextType = SysDic.All, Length = 20)]
        public virtual string BINo
		{
			get { return _bINo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for BINo", value, value.ToString());
				_bINo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "血袋码", ShortCode = "BBagCode", Desc = "血袋码", ContextType = SysDic.All, Length = 50)]
        public virtual string BBagCode
		{
			get { return _bBagCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for BBagCode", value, value.ToString());
				_bBagCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "产品码", ShortCode = "PCode", Desc = "产品码", ContextType = SysDic.All, Length = 50)]
        public virtual string PCode
		{
			get { return _pCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for PCode", value, value.ToString());
				_pCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "唯一码", ShortCode = "B3Code", Desc = "唯一码", ContextType = SysDic.All, Length = 50)]
        public virtual string B3Code
		{
			get { return _b3Code; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for B3Code", value, value.ToString());
				_b3Code = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "规格", ShortCode = "BCount", Desc = "规格", ContextType = SysDic.All, Length = 8)]
        public virtual double BCount
		{
			get { return _bCount; }
			set { _bCount = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "失效时间", ShortCode = "InvalidDate", Desc = "失效时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime InvalidDate
		{
			get { return _invalidDate; }
			set { _invalidDate = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "采集时间", ShortCode = "CollectDate", Desc = "采集时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? CollectDate
		{
			get { return _collectDate; }
			set { _collectDate = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "价格", ShortCode = "Price", Desc = "价格", ContextType = SysDic.All, Length = 8)]
        public virtual double Price
		{
			get { return _price; }
			set { _price = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "库存量", ShortCode = "BTotalCount", Desc = "库存量", ContextType = SysDic.All, Length = 8)]
        public virtual double BTotalCount
		{
			get { return _bTotalCount; }
			set { _bTotalCount = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "计算库存量,按血制品分类单位换算关系计算", ShortCode = "CalcTotalCount", Desc = "计算库存量,按血制品分类单位换算关系计算", ContextType = SysDic.All, Length = 8)]
        public virtual double CalcTotalCount
		{
			get { return _calcTotalCount; }
			set { _calcTotalCount = value; }
		}

        [DataMember]
        [DataDesc(CName = "血袋信息附加码", ShortCode = "BBagExCode", Desc = "血袋信息附加码", ContextType = SysDic.All, Length = 20)]
        public virtual string BBagExCode
		{
			get { return _bBagExCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for BBagExCode", value, value.ToString());
				_bBagExCode = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "预警时间", ShortCode = "BIWarnDate", Desc = "预警时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? BIWarnDate
		{
			get { return _bIWarnDate; }
			set { _bIWarnDate = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "入库时间", ShortCode = "BIDate", Desc = "入库时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? BIDate
		{
			get { return _bIDate; }
			set { _bIDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "使用状态", ShortCode = "UseFlag", Desc = "使用状态", ContextType = SysDic.All, Length = 4)]
        public virtual int UseFlag
		{
			get { return _useFlag; }
			set { _useFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "Hflag", ShortCode = "Hflag", Desc = "Hflag", ContextType = SysDic.All, Length = 4)]
        public virtual int Hflag
		{
			get { return _hflag; }
			set { _hflag = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "复核血型编号", ShortCode = "ReviewBloodAboNo", Desc = "复核血型编号", ContextType = SysDic.All, Length = 8)]
		public virtual long? ReviewBloodAboNo
		{
			get { return _reviewBloodAboNo; }
			set { _reviewBloodAboNo = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "复核者ID", ShortCode = "ReviewId", Desc = "复核者ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? ReviewId
		{
			get { return _reviewId; }
			set { _reviewId = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "复核者ID", ShortCode = "ReviewCName", Desc = "复核者ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? ReviewCName
		{
			get { return _reviewCName; }
			set { _reviewCName = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "复核时间", ShortCode = "ReviewTime", Desc = "复核时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ReviewTime
		{
			get { return _reviewTime; }
			set { _reviewTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "InvFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int InvFlag
		{
			get { return _invFlag; }
			set { _invFlag = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BCCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BCCode
		{
			get { return _bCCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for BCCode", value, value.ToString());
				_bCCode = value;
			}
		}

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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "预警时间", ShortCode = "Warndate", Desc = "预警时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? Warndate
		{
			get { return _warndate; }
			set { _warndate = value; }
		}

        [DataMember]
        [DataDesc(CName = "入库温度", ShortCode = "BloodoC", Desc = "入库温度", ContextType = SysDic.All, Length = 10)]
        public virtual string BloodoC
		{
			get { return _bloodoC; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for BloodoC", value, value.ToString());
				_bloodoC = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "一血多配标志", ShortCode = "Ismulflag", Desc = "一血多配标志", ContextType = SysDic.All, Length = 4)]
        public virtual int Ismulflag
		{
			get { return _ismulflag; }
			set { _ismulflag = value; }
		}

        [DataMember]
        [DataDesc(CName = "配血标志", ShortCode = "Preflag", Desc = "配血标志", ContextType = SysDic.All, Length = 1)]
        public virtual bool Preflag
		{
			get { return _preflag; }
			set { _preflag = value; }
		}

        [DataMember]
        [DataDesc(CName = "血袋复检标志", ShortCode = "BagCheckFlag", Desc = "血袋复检标志", ContextType = SysDic.All, Length = 1)]
        public virtual bool BagCheckFlag
		{
			get { return _bagCheckFlag; }
			set { _bagCheckFlag = value; }
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
        [DataDesc(CName = "血型表", ShortCode = "BloodABO", Desc = "血型表")]
		public virtual BloodABO BloodABO
		{
			get { return _bloodABO; }
			set { _bloodABO = value; }
		}

        [DataMember]
        [DataDesc(CName = "入库明细", ShortCode = "BloodBInItem", Desc = "入库明细")]
		public virtual BloodBInItem BloodBInItem
		{
			get { return _bloodBInItem; }
			set { _bloodBInItem = value; }
		}

        [DataMember]
        [DataDesc(CName = "血袋分类", ShortCode = "BloodClass", Desc = "血袋分类")]
		public virtual BloodClass BloodClass
		{
			get { return _bloodClass; }
			set { _bloodClass = value; }
		}

        [DataMember]
        [DataDesc(CName = "血制品字典", ShortCode = "BloodStyle", Desc = "血制品字典")]
		public virtual BloodStyle BloodStyle
		{
			get { return _bloodStyle; }
			set { _bloodStyle = value; }
		}

        [DataMember]
        [DataDesc(CName = "血制品单位", ShortCode = "BloodUnit", Desc = "血制品单位")]
		public virtual BloodUnit BloodUnit
		{
			get { return _bloodUnit; }
			set { _bloodUnit = value; }
		}

        [DataMember]
        [DataDesc(CName = "字典信息", ShortCode = "BBankNo", Desc = "字典信息")]
		public virtual BDict BBank
		{
			get { return _bBankNo; }
			set { _bBankNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "字典信息", ShortCode = "IceboxNo", Desc = "字典信息")]
		public virtual BDict Icebox
		{
			get { return _iceboxNo; }
			set { _iceboxNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "字典信息", ShortCode = "StoreCondNo", Desc = "字典信息")]
		public virtual BDict StoreCond
		{
			get { return _storeCondNo; }
			set { _storeCondNo = value; }
		}

		#endregion
	}
	#endregion
}