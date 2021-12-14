using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
	#region OSManagerRefundForm

	/// <summary>
	/// OSManagerRefundForm object for NHibernate mapped table 'OS_ManagerRefundForm'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "退费申请单：退费申请由商家内部特定角色员工发起，退费价格是同客户协商确定。", ClassCName = "OSManagerRefundForm", ShortCode = "OSManagerRefundForm", Desc = "退费申请单：退费申请由商家内部特定角色员工发起，退费价格是同客户协商确定。")]
	public class OSManagerRefundForm : BaseEntity
	{
		#region Member Variables
		
        protected string _mRefundFormCode;
        protected long? _uOFID;
        protected string _uOFCode;
        protected long? _dOFID;
        protected long? _doctorAccountID;
        protected long? _oSUserConsumerFormID;
        protected string _oSUserConsumerFormCode;
        protected long? _weiXinUserID;
        protected string _doctorOpenID;
        protected string _doctorName;
        protected long? _userAccountID;
        protected long? _userWeiXinUserID;
        protected string _userName;
        protected string _userOpenID;
        protected long _status;
        protected string _payCode;
        protected string _memo;
        protected int _dispOrder;
        protected bool _isUse;
        protected DateTime? _dataUpdateTime;
        protected double? _discountPrice;
        protected double? _discount;
        protected double? _price;
        protected double? _refundPrice;
        protected DateTime? _payTime;
        protected string _refundApplyManName;
        protected long? _refundApplyManID;
        protected DateTime? _refundApplyTime;
        protected string _refundOneReviewManName;
        protected long? _refundOneReviewManID;
        protected DateTime? _refundOneReviewStartTime;
        protected DateTime? _refundOneReviewFinishTime;
        protected string _refundTwoReviewManName;
        protected long? _refundTwoReviewManID;
        protected DateTime? _refundTwoReviewStartTime;
        protected DateTime? _refundTwoReviewFinishTime;
        protected string _refundThreeReviewManName;
        protected long? _refundThreeReviewManID;
        protected DateTime? _refundThreeReviewStartTime;
        protected DateTime? _refundThreeReviewFinishTime;
        protected string _refundReason;
        protected string _refundOneReviewReason;
        protected string _refundTwoReviewReason;
        protected string _refundThreeReviewReason;
        protected string _transactionId;
        protected string _refundId;
        protected long? _refundType;
        protected long? _bankID;
        protected string _bankAccount;
        protected string _bankTransFormCode;
        protected bool _CollectionFlag;
        protected double _CollectionPrice;


        #endregion

        #region Constructors

        public OSManagerRefundForm() { }

		public OSManagerRefundForm( string mRefundFormCode, long? uOFID, string uOFCode, long? dOFID, long? doctorAccountID, long? oSUserConsumerFormID, string oSUserConsumerFormCode, long? weiXinUserID, string doctorOpenID, string doctorName, long? userAccountID, long? userWeiXinUserID, string userName, string userOpenID, long status, string payCode, string memo, int dispOrder, bool isUse, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, double? discountPrice, double? discount, double? price, double? refundPrice, DateTime payTime, string refundApplyManName, long? refundApplyManID, DateTime refundApplyTime, string refundOneReviewManName, long? refundOneReviewManID, DateTime refundOneReviewStartTime, DateTime refundOneReviewFinishTime, string refundTwoReviewManName, long? refundTwoReviewManID, DateTime refundTwoReviewStartTime, DateTime refundTwoReviewFinishTime, string refundThreeReviewManName, long? refundThreeReviewManID, DateTime refundThreeReviewStartTime, DateTime refundThreeReviewFinishTime, string refundReason, string refundOneReviewReason, string refundTwoReviewReason, string refundThreeReviewReason, string transactionId, string refundId, long? refundType, long? bankID, string bankAccount, string bankTransFormCode )
		{
			this._mRefundFormCode = mRefundFormCode;
			this._uOFID = uOFID;
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
			this._discountPrice = discountPrice;
			this._discount = discount;
			this._price = price;
			this._refundPrice = refundPrice;
			this._payTime = payTime;
			this._refundApplyManName = refundApplyManName;
			this._refundApplyManID = refundApplyManID;
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
			this._transactionId = transactionId;
			this._refundId = refundId;
			this._refundType = refundType;
			this._bankID = bankID;
			this._bankAccount = bankAccount;
			this._bankTransFormCode = bankTransFormCode;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "退费单编号", ShortCode = "MRefundFormCode", Desc = "退费单编号", ContextType = SysDic.All, Length = 30)]
        public virtual string MRefundFormCode
		{
			get { return _mRefundFormCode; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for MRefundFormCode", value, value.ToString());
				_mRefundFormCode = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "用户订单ID", ShortCode = "UOFID", Desc = "用户订单ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? UOFID
		{
			get { return _uOFID; }
			set { _uOFID = value; }
		}

        [DataMember]
        [DataDesc(CName = "用户订单编号", ShortCode = "UOFCode", Desc = "用户订单编号", ContextType = SysDic.All, Length = 30)]
        public virtual string UOFCode
		{
			get { return _uOFCode; }
			set
			{
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
        public virtual long? UserAccountID
		{
			get { return _userAccountID; }
			set { _userAccountID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "用户微信ID", ShortCode = "UserWeiXinUserID", Desc = "用户微信ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? UserWeiXinUserID
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
        [DataDesc(CName = "状态", ShortCode = "Status", Desc = "状态", ContextType = SysDic.All, Length = 8)]
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
        [DataDesc(CName = "折扣价格", ShortCode = "DiscountPrice", Desc = "折扣价格", ContextType = SysDic.All, Length = 8)]
        public virtual double? DiscountPrice
		{
			get { return _discountPrice; }
			set { _discountPrice = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "折扣率", ShortCode = "Discount", Desc = "折扣率", ContextType = SysDic.All, Length = 8)]
        public virtual double? Discount
		{
			get { return _discount; }
			set { _discount = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "实际金额", ShortCode = "Price", Desc = "实际金额", ContextType = SysDic.All, Length = 8)]
        public virtual double? Price
		{
			get { return _price; }
			set { _price = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "退费金额", ShortCode = "RefundPrice", Desc = "退费金额", ContextType = SysDic.All, Length = 8)]
        public virtual double? RefundPrice
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
        [DataDesc(CName = "退费申请人", ShortCode = "RefundApplyManName", Desc = "退费申请人", ContextType = SysDic.All, Length = 50)]
        public virtual string RefundApplyManName
		{
			get { return _refundApplyManName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for RefundApplyManName", value, value.ToString());
				_refundApplyManName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "退费申请人ID", ShortCode = "RefundApplyManID", Desc = "退费申请人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? RefundApplyManID
		{
			get { return _refundApplyManID; }
			set { _refundApplyManID = value; }
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
        public virtual long? RefundOneReviewManID
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
        public virtual long? RefundTwoReviewManID
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
        public virtual long? RefundThreeReviewManID
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
        [DataDesc(CName = "微信退款单号", ShortCode = "RefundId", Desc = "微信退款单号", ContextType = SysDic.All, Length = 50)]
        public virtual string RefundId
		{
			get { return _refundId; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for RefundId", value, value.ToString());
				_refundId = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "1、微信退回", ShortCode = "RefundType", Desc = "1、微信退回", ContextType = SysDic.All, Length = 8)]
        public virtual long? RefundType
		{
			get { return _refundType; }
			set { _refundType = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "银行种类", ShortCode = "BankID", Desc = "银行种类", ContextType = SysDic.All, Length = 8)]
        public virtual long? BankID
		{
			get { return _bankID; }
			set { _bankID = value; }
		}

        [DataMember]
        [DataDesc(CName = "银行帐号", ShortCode = "BankAccount", Desc = "银行帐号", ContextType = SysDic.All, Length = 50)]
        public virtual string BankAccount
		{
			get { return _bankAccount; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for BankAccount", value, value.ToString());
				_bankAccount = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "银行转账单号", ShortCode = "BankTransFormCode", Desc = "银行转账单号", ContextType = SysDic.All, Length = 50)]
        public virtual string BankTransFormCode
		{
			get { return _bankTransFormCode; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for BankTransFormCode", value, value.ToString());
				_bankTransFormCode = value;
			}
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
        #endregion
    }
	#endregion
}