using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
	#region OSUserOrderForm

	/// <summary>
	/// OSUserOrderForm object for NHibernate mapped table 'OS_UserOrderForm'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "用户订单", ClassCName = "OSUserOrderForm", ShortCode = "OSUserOrderForm", Desc = "用户订单")]
	public class OSUserOrderForm : BaseEntity
	{
		#region Member Variables
		
        protected long _areaID;
        protected long _hospitalID;
        protected string _uOFCode;
        protected long? _dOFID;
        protected long? _doctorAccountID;
        protected long? _oSUserConsumerFormID;
        protected string _oSUserConsumerFormCode;
        protected long? _weiXinUserID;
        protected string _doctorOpenID;
        protected string _doctorName;
        protected long _userAccountID;
        protected long _userWeiXinUserID;
        protected string _userName;
        protected string _userOpenID;
        protected long _status;
        protected string _payCode;
        protected string _memo;
        protected int _dispOrder;
        protected bool _isUse;
        protected DateTime? _dataUpdateTime;
        protected double _marketPrice;
        protected double _greatMasterPrice;
        protected double _discountPrice;
        protected double _discount;
        protected double _price;
        protected double _advicePrice;
        protected double _refundPrice;
        protected DateTime? _payTime;
        protected DateTime? _cancelApplyTime;
        protected DateTime? _cancelFinishedTime;
        protected DateTime? _consumerStartTime;
        protected DateTime? _consumerFinishedTime;
        protected DateTime? _refundApplyTime;
        protected string _refundOneReviewManName;
        protected long _refundOneReviewManID;
        protected DateTime? _refundOneReviewStartTime;
        protected DateTime? _refundOneReviewFinishTime;
        protected string _refundTwoReviewManName;
        protected long _refundTwoReviewManID;
        protected DateTime? _refundTwoReviewStartTime;
        protected DateTime? _refundTwoReviewFinishTime;
        protected string _refundThreeReviewManName;
        protected long _refundThreeReviewManID;
        protected DateTime? _refundThreeReviewStartTime;
        protected DateTime? _refundThreeReviewFinishTime;
        protected string _refundReason;
        protected string _refundOneReviewReason;
        protected string _refundTwoReviewReason;
        protected string _refundThreeReviewReason;
        protected bool _isPrePay;
        protected string _prePayId;
        protected DateTime? _prePayTime;
        protected string _prePayReturnCode;
        protected string _prePayReturnMsg;
        protected string _prePayErrCode;
        protected string _prePayErrName;
        protected string _transactionId;
        protected string _column59;
        protected string _column60;
        protected string _column61;
        protected string _column62;
        protected long? _orgID;
        protected string _weblisSourceOrgID;
        protected string _weblisSourceOrgName;
        protected long? _empID;
        protected string _empName;
        protected long? _ConsumerAreaID;
        protected long? _TypeID;
        protected string _TypeName;
        protected bool _CollectionFlag;
        protected double _CollectionPrice;
        protected string _DoctMobileCode;
        protected string _UserMobileCode;

        #endregion

        #region Constructors

        public OSUserOrderForm() { }

		public OSUserOrderForm( long areaID, long hospitalID, string uOFCode, long dOFID, long doctorAccountID, long oSUserConsumerFormID, string oSUserConsumerFormCode, long weiXinUserID, string doctorOpenID, string doctorName, long userAccountID, long userWeiXinUserID, string userName, string userOpenID, long status, string payCode, string memo, int dispOrder, bool isUse, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, double marketPrice, double greatMasterPrice, double discountPrice, double discount, double price, double advicePrice, double refundPrice, DateTime payTime, DateTime cancelApplyTime, DateTime cancelFinishedTime, DateTime consumerStartTime, DateTime consumerFinishedTime, DateTime refundApplyTime, string refundOneReviewManName, long refundOneReviewManID, DateTime refundOneReviewStartTime, DateTime refundOneReviewFinishTime, string refundTwoReviewManName, long refundTwoReviewManID, DateTime refundTwoReviewStartTime, DateTime refundTwoReviewFinishTime, string refundThreeReviewManName, long refundThreeReviewManID, DateTime refundThreeReviewStartTime, DateTime refundThreeReviewFinishTime, string refundReason, string refundOneReviewReason, string refundTwoReviewReason, string refundThreeReviewReason, bool isPrePay, string prePayId, DateTime prePayTime, string prePayReturnCode, string prePayReturnMsg, string prePayErrCode, string prePayErrName, string transactionId, string column59, string column60, string column61, string column62 )
		{
			this._areaID = areaID;
			this._hospitalID = hospitalID;
			this._uOFCode = uOFCode;
			this._dOFID = dOFID;
			this._doctorAccountID = doctorAccountID;
			this._oSUserConsumerFormID = oSUserConsumerFormID;
			this._oSUserConsumerFormCode = oSUserConsumerFormCode;
			this._weiXinUserID = weiXinUserID;
			this._doctorOpenID = doctorOpenID;
			this._doctorName = doctorName;
			this._userAccountID = userAccountID;
			this._userWeiXinUserID = userWeiXinUserID;
			this._userName = userName;
			this._userOpenID = userOpenID;
			this._status = status;
			this._payCode = payCode;
			this._memo = memo;
			this._dispOrder = dispOrder;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._marketPrice = marketPrice;
			this._greatMasterPrice = greatMasterPrice;
			this._discountPrice = discountPrice;
			this._discount = discount;
			this._price = price;
			this._advicePrice = advicePrice;
			this._refundPrice = refundPrice;
			this._payTime = payTime;
			this._cancelApplyTime = cancelApplyTime;
			this._cancelFinishedTime = cancelFinishedTime;
			this._consumerStartTime = consumerStartTime;
			this._consumerFinishedTime = consumerFinishedTime;
			this._refundApplyTime = refundApplyTime;
			this._refundOneReviewManName = refundOneReviewManName;
			this._refundOneReviewManID = refundOneReviewManID;
			this._refundOneReviewStartTime = refundOneReviewStartTime;
			this._refundOneReviewFinishTime = refundOneReviewFinishTime;
			this._refundTwoReviewManName = refundTwoReviewManName;
			this._refundTwoReviewManID = refundTwoReviewManID;
			this._refundTwoReviewStartTime = refundTwoReviewStartTime;
			this._refundTwoReviewFinishTime = refundTwoReviewFinishTime;
			this._refundThreeReviewManName = refundThreeReviewManName;
			this._refundThreeReviewManID = refundThreeReviewManID;
			this._refundThreeReviewStartTime = refundThreeReviewStartTime;
			this._refundThreeReviewFinishTime = refundThreeReviewFinishTime;
			this._refundReason = refundReason;
			this._refundOneReviewReason = refundOneReviewReason;
			this._refundTwoReviewReason = refundTwoReviewReason;
			this._refundThreeReviewReason = refundThreeReviewReason;
			this._isPrePay = isPrePay;
			this._prePayId = prePayId;
			this._prePayTime = prePayTime;
			this._prePayReturnCode = prePayReturnCode;
			this._prePayReturnMsg = prePayReturnMsg;
			this._prePayErrCode = prePayErrCode;
			this._prePayErrName = prePayErrName;
			this._transactionId = transactionId;
			this._column59 = column59;
			this._column60 = column60;
			this._column61 = column61;
			this._column62 = column62;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "区域ID", ShortCode = "AreaID", Desc = "区域ID", ContextType = SysDic.All, Length = 8)]
        public virtual long AreaID
		{
			get { return _areaID; }
			set { _areaID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医院ID", ShortCode = "HospitalID", Desc = "医院ID", ContextType = SysDic.All, Length = 8)]
        public virtual long HospitalID
		{
			get { return _hospitalID; }
			set { _hospitalID = value; }
		}

        [DataMember]
        [DataDesc(CName = "用户订单编号", ShortCode = "UOFCode", Desc = "用户订单编号", ContextType = SysDic.All, Length = 30)]
        public virtual string UOFCode
		{
			get { return _uOFCode; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for UOFCode", value, value.ToString());
				_uOFCode = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DOFID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? DOFID
		{
			get { return _dOFID; }
			set { _dOFID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医生账户信息ID", ShortCode = "DoctorAccountID", Desc = "医生账户信息ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? DoctorAccountID
		{
			get { return _doctorAccountID; }
			set { _doctorAccountID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OSUserConsumerFormID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? OSUserConsumerFormID
		{
			get { return _oSUserConsumerFormID; }
			set { _oSUserConsumerFormID = value; }
		}

        [DataMember]
        [DataDesc(CName = "消费单编号", ShortCode = "OSUserConsumerFormCode", Desc = "消费单编号", ContextType = SysDic.All, Length = 30)]
        public virtual string OSUserConsumerFormCode
		{
			get { return _oSUserConsumerFormCode; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for OSUserConsumerFormCode", value, value.ToString());
				_oSUserConsumerFormCode = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医生微信ID", ShortCode = "WeiXinUserID", Desc = "医生微信ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? WeiXinUserID
		{
			get { return _weiXinUserID; }
			set { _weiXinUserID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DoctorOpenID", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string DoctorOpenID
		{
			get { return _doctorOpenID; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for DoctorOpenID", value, value.ToString());
				_doctorOpenID = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "医生姓名", ShortCode = "DoctorName", Desc = "医生姓名", ContextType = SysDic.All, Length = 20)]
        public virtual string DoctorName
		{
			get { return _doctorName; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for DoctorName", value, value.ToString());
				_doctorName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "用户账户信息ID", ShortCode = "UserAccountID", Desc = "用户账户信息ID", ContextType = SysDic.All, Length = 8)]
        public virtual long UserAccountID
		{
			get { return _userAccountID; }
			set { _userAccountID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "用户微信ID", ShortCode = "UserWeiXinUserID", Desc = "用户微信ID", ContextType = SysDic.All, Length = 8)]
        public virtual long UserWeiXinUserID
		{
			get { return _userWeiXinUserID; }
			set { _userWeiXinUserID = value; }
		}

        [DataMember]
        [DataDesc(CName = "用户姓名", ShortCode = "UserName", Desc = "用户姓名", ContextType = SysDic.All, Length = 20)]
        public virtual string UserName
		{
			get { return _userName; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for UserName", value, value.ToString());
				_userName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "用户OpenID", ShortCode = "UserOpenID", Desc = "用户OpenID", ContextType = SysDic.All, Length = 50)]
        public virtual string UserOpenID
		{
			get { return _userOpenID; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for UserOpenID", value, value.ToString());
				_userOpenID = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "订单状态", ShortCode = "Status", Desc = "订单状态", ContextType = SysDic.All, Length = 8)]
        public virtual long Status
		{
			get { return _status; }
			set { _status = value; }
		}

        [DataMember]
        [DataDesc(CName = "消费码", ShortCode = "PayCode", Desc = "消费码", ContextType = SysDic.All, Length = 50)]
        public virtual string PayCode
		{
			get { return _payCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for PayCode", value, value.ToString());
				_payCode = value;
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
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "市场价格", ShortCode = "MarketPrice", Desc = "市场价格", ContextType = SysDic.All, Length = 8)]
        public virtual double MarketPrice
		{
			get { return _marketPrice; }
			set { _marketPrice = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "大家价格", ShortCode = "GreatMasterPrice", Desc = "大家价格", ContextType = SysDic.All, Length = 8)]
        public virtual double GreatMasterPrice
		{
			get { return _greatMasterPrice; }
			set { _greatMasterPrice = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "折扣价格", ShortCode = "DiscountPrice", Desc = "折扣价格", ContextType = SysDic.All, Length = 8)]
        public virtual double DiscountPrice
		{
			get { return _discountPrice; }
			set { _discountPrice = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "折扣率", ShortCode = "Discount", Desc = "折扣率", ContextType = SysDic.All, Length = 8)]
        public virtual double Discount
		{
			get { return _discount; }
			set { _discount = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "实际金额", ShortCode = "Price", Desc = "实际金额", ContextType = SysDic.All, Length = 8)]
        public virtual double Price
		{
			get { return _price; }
			set { _price = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "咨询费", ShortCode = "AdvicePrice", Desc = "咨询费", ContextType = SysDic.All, Length = 8)]
        public virtual double AdvicePrice
		{
			get { return _advicePrice; }
			set { _advicePrice = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "退费金额", ShortCode = "RefundPrice", Desc = "退费金额", ContextType = SysDic.All, Length = 8)]
        public virtual double RefundPrice
		{
			get { return _refundPrice; }
			set { _refundPrice = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "缴费时间", ShortCode = "PayTime", Desc = "缴费时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? PayTime
		{
			get { return _payTime; }
			set { _payTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "取消申请时间", ShortCode = "CancelApplyTime", Desc = "取消申请时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? CancelApplyTime
		{
			get { return _cancelApplyTime; }
			set { _cancelApplyTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "取消完成时间", ShortCode = "CancelFinishedTime", Desc = "取消完成时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? CancelFinishedTime
		{
			get { return _cancelFinishedTime; }
			set { _cancelFinishedTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "消费开始时间", ShortCode = "ConsumerStartTime", Desc = "消费开始时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ConsumerStartTime
		{
			get { return _consumerStartTime; }
			set { _consumerStartTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "消费完成时间", ShortCode = "ConsumerFinishedTime", Desc = "消费完成时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ConsumerFinishedTime
		{
			get { return _consumerFinishedTime; }
			set { _consumerFinishedTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "退费申请时间", ShortCode = "RefundApplyTime", Desc = "退费申请时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? RefundApplyTime
		{
			get { return _refundApplyTime; }
			set { _refundApplyTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "退款处理人", ShortCode = "RefundOneReviewManName", Desc = "退款处理人", ContextType = SysDic.All, Length = 50)]
        public virtual string RefundOneReviewManName
		{
			get { return _refundOneReviewManName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for RefundOneReviewManName", value, value.ToString());
				_refundOneReviewManName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "退款处理人ID", ShortCode = "RefundOneReviewManID", Desc = "退款处理人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long RefundOneReviewManID
		{
			get { return _refundOneReviewManID; }
			set { _refundOneReviewManID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "退款处理开始时间", ShortCode = "RefundOneReviewStartTime", Desc = "退款处理开始时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? RefundOneReviewStartTime
		{
			get { return _refundOneReviewStartTime; }
			set { _refundOneReviewStartTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "退款处理完成时间", ShortCode = "RefundOneReviewFinishTime", Desc = "退款处理完成时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? RefundOneReviewFinishTime
		{
			get { return _refundOneReviewFinishTime; }
			set { _refundOneReviewFinishTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "退款审批人", ShortCode = "RefundTwoReviewManName", Desc = "退款审批人", ContextType = SysDic.All, Length = 50)]
        public virtual string RefundTwoReviewManName
		{
			get { return _refundTwoReviewManName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for RefundTwoReviewManName", value, value.ToString());
				_refundTwoReviewManName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "退款审批人ID", ShortCode = "RefundTwoReviewManID", Desc = "退款审批人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long RefundTwoReviewManID
		{
			get { return _refundTwoReviewManID; }
			set { _refundTwoReviewManID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "退款审批开始时间", ShortCode = "RefundTwoReviewStartTime", Desc = "退款审批开始时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? RefundTwoReviewStartTime
		{
			get { return _refundTwoReviewStartTime; }
			set { _refundTwoReviewStartTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "退款审批时间", ShortCode = "RefundTwoReviewFinishTime", Desc = "退款审批时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? RefundTwoReviewFinishTime
		{
			get { return _refundTwoReviewFinishTime; }
			set { _refundTwoReviewFinishTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "退款发放人", ShortCode = "RefundThreeReviewManName", Desc = "退款发放人", ContextType = SysDic.All, Length = 50)]
        public virtual string RefundThreeReviewManName
		{
			get { return _refundThreeReviewManName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for RefundThreeReviewManName", value, value.ToString());
				_refundThreeReviewManName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "退款发放人ID", ShortCode = "RefundThreeReviewManID", Desc = "退款发放人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long RefundThreeReviewManID
		{
			get { return _refundThreeReviewManID; }
			set { _refundThreeReviewManID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "退款发放开始时间", ShortCode = "RefundThreeReviewStartTime", Desc = "退款发放开始时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? RefundThreeReviewStartTime
		{
			get { return _refundThreeReviewStartTime; }
			set { _refundThreeReviewStartTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "退款发放完成时间", ShortCode = "RefundThreeReviewFinishTime", Desc = "退款发放完成时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? RefundThreeReviewFinishTime
		{
			get { return _refundThreeReviewFinishTime; }
			set { _refundThreeReviewFinishTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "退费原因", ShortCode = "RefundReason", Desc = "退费原因", ContextType = SysDic.All, Length = 900)]
        public virtual string RefundReason
		{
			get { return _refundReason; }
			set
			{
				if ( value != null && value.Length > 900)
					throw new ArgumentOutOfRangeException("Invalid value for RefundReason", value, value.ToString());
				_refundReason = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "退款处理说明", ShortCode = "RefundOneReviewReason", Desc = "退款处理说明", ContextType = SysDic.All, Length = 900)]
        public virtual string RefundOneReviewReason
		{
			get { return _refundOneReviewReason; }
			set
			{
				if ( value != null && value.Length > 900)
					throw new ArgumentOutOfRangeException("Invalid value for RefundOneReviewReason", value, value.ToString());
				_refundOneReviewReason = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "退款审批说明", ShortCode = "RefundTwoReviewReason", Desc = "退款审批说明", ContextType = SysDic.All, Length = 900)]
        public virtual string RefundTwoReviewReason
		{
			get { return _refundTwoReviewReason; }
			set
			{
				if ( value != null && value.Length > 900)
					throw new ArgumentOutOfRangeException("Invalid value for RefundTwoReviewReason", value, value.ToString());
				_refundTwoReviewReason = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "退款发放说明", ShortCode = "RefundThreeReviewReason", Desc = "退款发放说明", ContextType = SysDic.All, Length = 900)]
        public virtual string RefundThreeReviewReason
		{
			get { return _refundThreeReviewReason; }
			set
			{
				if ( value != null && value.Length > 900)
					throw new ArgumentOutOfRangeException("Invalid value for RefundThreeReviewReason", value, value.ToString());
				_refundThreeReviewReason = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "是否已预下单", ShortCode = "IsPrePay", Desc = "是否已预下单", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsPrePay
		{
			get { return _isPrePay; }
			set { _isPrePay = value; }
		}

        [DataMember]
        [DataDesc(CName = "WeiXin统一下单编码", ShortCode = "PrePayId", Desc = "WeiXin统一下单编码", ContextType = SysDic.All, Length = 50)]
        public virtual string PrePayId
		{
			get { return _prePayId; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for PrePayId", value, value.ToString());
				_prePayId = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "PrePayTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? PrePayTime
		{
			get { return _prePayTime; }
			set { _prePayTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "预下单通信结果", ShortCode = "PrePayReturnCode", Desc = "预下单通信结果", ContextType = SysDic.All, Length = 50)]
        public virtual string PrePayReturnCode
		{
			get { return _prePayReturnCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for PrePayReturnCode", value, value.ToString());
				_prePayReturnCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PrePayReturnMsg", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string PrePayReturnMsg
		{
			get { return _prePayReturnMsg; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for PrePayReturnMsg", value, value.ToString());
				_prePayReturnMsg = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PrePayErrCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string PrePayErrCode
		{
			get { return _prePayErrCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for PrePayErrCode", value, value.ToString());
				_prePayErrCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PrePayErrName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string PrePayErrName
		{
			get { return _prePayErrName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for PrePayErrName", value, value.ToString());
				_prePayErrName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "微信订单号", ShortCode = "TransactionId", Desc = "微信订单号", ContextType = SysDic.All, Length = 50)]
        public virtual string TransactionId
		{
			get { return _transactionId; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for TransactionId", value, value.ToString());
				_transactionId = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Column59", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string Column59
		{
			get { return _column59; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for Column59", value, value.ToString());
				_column59 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Column60", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string Column60
		{
			get { return _column60; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for Column60", value, value.ToString());
				_column60 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Column61", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string Column61
		{
			get { return _column61; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for Column61", value, value.ToString());
				_column61 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Column62", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string Column62
		{
			get { return _column62; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for Column62", value, value.ToString());
				_column62 = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "采血站点ID", ShortCode = "OrgID", Desc = "采血站点ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? OrgID
        {
            get { return _orgID; }
            set { _orgID = value; }
        }

        [DataMember]
        [DataDesc(CName = "采血站点组织机构代码", ShortCode = "WeblisOrgID", Desc = "采血站点组织机构代码", ContextType = SysDic.All, Length = 50)]
        public virtual string WeblisSourceOrgID
        {
            get { return _weblisSourceOrgID; }
            set
            {
              
                _weblisSourceOrgID = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "采血站点名称", ShortCode = "WeblisOrgName", Desc = "采血站点名称", ContextType = SysDic.All, Length = 50)]
        public virtual string WeblisSourceOrgName
        {
            get { return _weblisSourceOrgName; }
            set
            {
                _weblisSourceOrgName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "采血人员ID", ShortCode = "EmpID", Desc = "采血人员ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? EmpID
        {
            get { return _empID; }
            set { _empID = value; }
        }

        [DataMember]
        [DataDesc(CName = "采血人员账户", ShortCode = "EmpAccount", Desc = "采血人员账户", ContextType = SysDic.All, Length = 50)]
        public virtual string EmpAccount
        {
            get { return _empName; }
            set
            {
                _empName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "采血区域ID", ShortCode = "ConsumerAreaID", Desc = "采血区域ID", ContextType = SysDic.All, Length = 50)]
        public virtual long? ConsumerAreaID
        {
            get { return _ConsumerAreaID; }
            set
            {
                _ConsumerAreaID = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "类型ID", ShortCode = "TypeID", Desc = "类型ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? TypeID
        {
            get { return _TypeID; }
            set { _TypeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "类型名称", ShortCode = "TypeName", Desc = "类型名称", ContextType = SysDic.All, Length = 8)]
        public virtual string TypeName
        {
            get { return _TypeName; }
            set { _TypeName = value; }
        }

        [DataMember]
        [DataDesc(CName = "采样费用标记", ShortCode = "CollectionFlag", Desc = "采样费用标记", ContextType = SysDic.All, Length = 500)]
        public virtual bool CollectionFlag
        {
            get { return _CollectionFlag; }
            set
            {
                _CollectionFlag = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "采样费用金额", ShortCode = "CollectionPrice", Desc = "采样费用金额", ContextType = SysDic.All, Length = 500)]
        public virtual double CollectionPrice
        {
            get { return _CollectionPrice; }
            set
            {
                _CollectionPrice = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "医生电话", ShortCode = "DoctMobileCode", Desc = "医生电话", ContextType = SysDic.All, Length = 500)]
        public virtual string DoctMobileCode
        {
            get { return _DoctMobileCode; }
            set
            {
                _DoctMobileCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "用户电话", ShortCode = "UserMobileCode", Desc = "采样费用金额", ContextType = SysDic.All, Length = 500)]
        public virtual string UserMobileCode
        {
            get { return _UserMobileCode; }
            set
            {
                _UserMobileCode = value;
            }
        }
        #endregion
    }
	#endregion
}