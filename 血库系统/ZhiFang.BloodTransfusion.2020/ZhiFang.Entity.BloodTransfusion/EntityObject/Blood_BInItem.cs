using System;
using System.Collections.Generic;
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
    [DataDesc(CName = "入库明细", ClassCName = "BloodBInItem", ShortCode = "BloodBInItem", Desc = "入库明细")]
	public class BloodBInItem : BaseEntity
	{
		#region Member Variables
		
        protected string _bInItemNo;
        protected string _inTypeNo;
        protected string _bframeNo;
        protected string _bINo;
        protected string _bBagCode;
        protected string _pCode;
        protected string _aBORHCode;
        protected string _invalidCode;
        protected string _collectCode;
        protected string _allCode;
        protected string _bBagExCode;
        protected DateTime? _invalidDate;
        protected DateTime? _collectDate;
        protected double _bCount;
        protected double _price;
        protected int _checkFlag;
        protected DateTime? _bInDate;
        protected int _isScanCheck;
        protected string _b3Code;
        protected int _isRepeat;
        protected long? _sUserID;
        protected string _sUserName;
        protected string _bCCode;
        protected string _bankBloodName;
        protected string _yQCode;
        protected string _bloodoC;
        protected string _memo;
        protected bool _visible;
        protected int _dispOrder;
		protected BloodABO _bloodABO;
		protected BDict _bloodBank;
		protected BloodBInForm _bloodBInForm;
		protected BloodClass _bloodClass;
		protected BDict _bloodIceBox;
		protected BDict _bloodStoreCond;
		protected BloodStyle _bloodStyle;
		protected BloodUnit _bloodUnit;

		#endregion

		#region Constructors

		public BloodBInItem() { }

		public BloodBInItem( long labID, string bInItemNo, string inTypeNo, string bframeNo, string bINo, string bBagCode, string pCode, string aBORHCode, string invalidCode, string collectCode, string allCode, string bBagExCode, DateTime invalidDate, DateTime collectDate, double bCount, double price, int checkFlag, DateTime bInDate, int isScanCheck, string b3Code, int isRepeat, long sUserID, string sUserName, string bCCode, string bankBloodName, string yQCode, string bloodoC, string memo, bool visible, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp, BloodABO bloodABO, BDict bBankNo, BloodBInForm bloodBInForm, BloodClass bloodClass, BDict iceboxNo, BDict storeCondNo, BloodStyle bloodStyle, BloodUnit bloodUnit )
		{
			this._labID = labID;
			this._bInItemNo = bInItemNo;
			this._inTypeNo = inTypeNo;
			this._bframeNo = bframeNo;
			this._bINo = bINo;
			this._bBagCode = bBagCode;
			this._pCode = pCode;
			this._aBORHCode = aBORHCode;
			this._invalidCode = invalidCode;
			this._collectCode = collectCode;
			this._allCode = allCode;
			this._bBagExCode = bBagExCode;
			this._invalidDate = invalidDate;
			this._collectDate = collectDate;
			this._bCount = bCount;
			this._price = price;
			this._checkFlag = checkFlag;
			this._bInDate = bInDate;
			this._isScanCheck = isScanCheck;
			this._b3Code = b3Code;
			this._isRepeat = isRepeat;
			this._sUserID = sUserID;
			this._sUserName = sUserName;
			this._bCCode = bCCode;
			this._bankBloodName = bankBloodName;
			this._yQCode = yQCode;
			this._bloodoC = bloodoC;
			this._memo = memo;
			this._visible = visible;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bloodABO = bloodABO;
			this._bloodBank = bBankNo;
			this._bloodBInForm = bloodBInForm;
			this._bloodClass = bloodClass;
			this._bloodIceBox = iceboxNo;
			this._bloodStoreCond = storeCondNo;
			this._bloodStyle = bloodStyle;
			this._bloodUnit = bloodUnit;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "入库明细单号", ShortCode = "BInItemNo", Desc = "入库明细单号", ContextType = SysDic.All, Length = 20)]
        public virtual string BInItemNo
		{
			get { return _bInItemNo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for BInItemNo", value, value.ToString());
				_bInItemNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "入库类型编码，存放血站的入库类型", ShortCode = "InTypeNo", Desc = "入库类型编码，存放血站的入库类型", ContextType = SysDic.All, Length = 20)]
        public virtual string InTypeNo
		{
			get { return _inTypeNo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for InTypeNo", value, value.ToString());
				_inTypeNo = value;
			}
		}

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
        [DataDesc(CName = "冰箱篮号", ShortCode = "BINo", Desc = "冰箱篮号", ContextType = SysDic.All, Length = 20)]
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
        [DataDesc(CName = "血型条码", ShortCode = "ABORHCode", Desc = "血型条码", ContextType = SysDic.All, Length = 50)]
        public virtual string ABORHCode
		{
			get { return _aBORHCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ABORHCode", value, value.ToString());
				_aBORHCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "有效日期条码", ShortCode = "InvalidCode", Desc = "有效日期条码", ContextType = SysDic.All, Length = 50)]
        public virtual string InvalidCode
		{
			get { return _invalidCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for InvalidCode", value, value.ToString());
				_invalidCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "采样日期条码", ShortCode = "CollectCode", Desc = "采样日期条码", ContextType = SysDic.All, Length = 50)]
        public virtual string CollectCode
		{
			get { return _collectCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for CollectCode", value, value.ToString());
				_collectCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "包含所有血液信息 条码", ShortCode = "AllCode", Desc = "包含所有血液信息 条码", ContextType = SysDic.All, Length = 200)]
        public virtual string AllCode
		{
			get { return _allCode; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for AllCode", value, value.ToString());
				_allCode = value;
			}
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
        [DataDesc(CName = "有效日期", ShortCode = "InvalidDate", Desc = "有效日期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? InvalidDate
		{
			get { return _invalidDate; }
			set { _invalidDate = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "采集日期", ShortCode = "CollectDate", Desc = "采集日期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? CollectDate
		{
			get { return _collectDate; }
			set { _collectDate = value; }
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
        [DataDesc(CName = "价格", ShortCode = "Price", Desc = "价格", ContextType = SysDic.All, Length = 8)]
        public virtual double Price
		{
			get { return _price; }
			set { _price = value; }
		}

        [DataMember]
        [DataDesc(CName = "审核标志", ShortCode = "CheckFlag", Desc = "审核标志", ContextType = SysDic.All, Length = 4)]
        public virtual int CheckFlag
		{
			get { return _checkFlag; }
			set { _checkFlag = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "入库时间", ShortCode = "BInDate", Desc = "入库时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? BInDate
		{
			get { return _bInDate; }
			set { _bInDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否扫描", ShortCode = "IsScanCheck", Desc = "是否扫描", ContextType = SysDic.All, Length = 4)]
        public virtual int IsScanCheck
		{
			get { return _isScanCheck; }
			set { _isScanCheck = value; }
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
        [DataDesc(CName = "重复标识", ShortCode = "IsRepeat", Desc = "重复标识", ContextType = SysDic.All, Length = 4)]
        public virtual int IsRepeat
		{
			get { return _isRepeat; }
			set { _isRepeat = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "入库登记者ID", ShortCode = "SUserID", Desc = "入库登记者ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? SUserID
		{
			get { return _sUserID; }
			set { _sUserID = value; }
		}

        [DataMember]
        [DataDesc(CName = "入库登记者", ShortCode = "SUserName", Desc = "入库登记者", ContextType = SysDic.All, Length = 50)]
        public virtual string SUserName
		{
			get { return _sUserName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SUserName", value, value.ToString());
				_sUserName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "BCCode", ShortCode = "BCCode", Desc = "BCCode", ContextType = SysDic.All, Length = 20)]
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
        [DataDesc(CName = "血站血液名称", ShortCode = "BankBloodName", Desc = "血站血液名称", ContextType = SysDic.All, Length = 50)]
        public virtual string BankBloodName
		{
			get { return _bankBloodName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for BankBloodName", value, value.ToString());
				_bankBloodName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "院区编号", ShortCode = "YQCode", Desc = "院区编号", ContextType = SysDic.All, Length = 20)]
        public virtual string YQCode
		{
			get { return _yQCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for YQCode", value, value.ToString());
				_yQCode = value;
			}
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
        [DataDesc(CName = "血站信息", ShortCode = "BloodBank", Desc = "血站信息")]
		public virtual BDict BloodBank
		{
			get { return _bloodBank; }
			set { _bloodBank = value; }
		}

        [DataMember]
        [DataDesc(CName = "入库主单", ShortCode = "BloodBInForm", Desc = "入库主单")]
		public virtual BloodBInForm BloodBInForm
		{
			get { return _bloodBInForm; }
			set { _bloodBInForm = value; }
		}

        [DataMember]
        [DataDesc(CName = "血袋分类", ShortCode = "BloodClass", Desc = "血袋分类")]
		public virtual BloodClass BloodClass
		{
			get { return _bloodClass; }
			set { _bloodClass = value; }
		}

		[DataMember]
		[DataDesc(CName = "冰箱", ShortCode = "BloodIceBox", Desc = "冰箱")]
		public virtual BDict BloodIceBox
		{
			get { return _bloodIceBox; }
			set { _bloodIceBox = value; }
		}

		[DataMember]
		[DataDesc(CName = "贮存温度", ShortCode = "BloodStoreCond", Desc = "贮存温度")]
		public virtual BDict BloodStoreCond
		{
			get { return _bloodStoreCond; }
			set { _bloodStoreCond = value; }
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
		#endregion
	}
	#endregion
}