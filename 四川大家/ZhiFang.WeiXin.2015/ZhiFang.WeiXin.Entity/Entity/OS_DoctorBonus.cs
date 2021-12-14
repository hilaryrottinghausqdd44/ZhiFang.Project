using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;
using ZhiFang.WeiXin.Entity.ViewObject.Request;

namespace ZhiFang.WeiXin.Entity
{
	#region OSDoctorBonus

	/// <summary>
	/// OSDoctorBonus object for NHibernate mapped table 'OS_DoctorBonus'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "医生奖金结算记录", ClassCName = "OSDoctorBonus", ShortCode = "OSDoctorBonus", Desc = "医生奖金结算记录")]
	public class OSDoctorBonus : OSDoctorBonusBase
    {
		#region Member Variables
		
        protected long? _oSDoctorBonusFormID;
        protected string _bonusCode;
        protected string _bonusFormCode;
        protected string _bonusFormRound;
        protected long? _doctorAccountID;
        protected long? _weiXinUserID;
        protected string _doctorName;
        protected double? _amount;
        protected long? _orderFormCount;
        protected double? _orderFormAmount;
        protected double? _percent;
        protected string _memo;
        protected int _dispOrder;
        protected bool _isUse;
        protected DateTime? _dataUpdateTime;
        protected string _bonusOneReviewManName;
        protected long? _bonusOneReviewManID;
        protected DateTime? _bonusOneReviewStartTime;
        protected DateTime? _bonusOneReviewFinishTime;
        protected string _bonusTwoReviewManName;
        protected long? _bonusTwoReviewManID;
        protected DateTime? _bonusTwoReviewStartTime;
        protected DateTime? _bonusTwoReviewFinishTime;
        protected string _bonusThreeReviewManName;
        protected long? _bonusThreeReviewManID;
        protected DateTime? _bonusThreeReviewStartTime;
        protected DateTime? _bonusThreeReviewFinishTime;
        protected long? _paymentMethod;
        protected long? _bankID;
        protected string _bankAccount;
        protected string _bankTransFormCode;
        protected long? _status;
        protected string _BankAddress;
        protected string _mobileCode;
        protected string _iDNumber;
        protected string _ReturnCode;
        protected string _ResultCode;
        protected string _ReturnMsg;
        protected string _ErrCode;
        protected string _ErrCodeDes;
        protected string _ErrorMemo;
        protected string _PaymentNo;
        protected string _PaymentTime;

        #endregion

        #region Constructors
        public OSDoctorBonus() { }

		
		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OSDoctorBonusFormID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? OSDoctorBonusFormID
		{
			get { return _oSDoctorBonusFormID; }
			set { _oSDoctorBonusFormID = value; }
		}

        [DataMember]
        [DataDesc(CName = "结算记录单号", ShortCode = "BonusCode", Desc = "结算记录单号", ContextType = SysDic.All, Length = 30)]
        public virtual string BonusCode
		{
			get { return _bonusCode; }
			set
			{
				_bonusCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "结算单号", ShortCode = "BonusFormCode", Desc = "结算单号", ContextType = SysDic.All, Length = 30)]
        public virtual string BonusFormCode
		{
			get { return _bonusFormCode; }
			set
			{
				_bonusFormCode = value;
			}
		}
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "状态", ShortCode = "Status", Desc = "状态", ContextType = SysDic.All, Length = 8)]
        public virtual long? Status
        {
            get { return _status; }
            set { _status = value; }
        }
        [DataMember]
        [DataDesc(CName = "结算周期", ShortCode = "BonusFormRound", Desc = "结算周期", ContextType = SysDic.All, Length = 30)]
        public virtual string BonusFormRound
		{
			get { return _bonusFormRound; }
			set
			{
				_bonusFormRound = value;
			}
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
        [DataDesc(CName = "医生微信ID", ShortCode = "WeiXinUserID", Desc = "医生微信ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? WeiXinUserID
		{
			get { return _weiXinUserID; }
			set { _weiXinUserID = value; }
		}

        [DataMember]
        [DataDesc(CName = "医生姓名", ShortCode = "DoctorName", Desc = "医生姓名", ContextType = SysDic.All, Length = 20)]
        public virtual string DoctorName
		{
			get { return _doctorName; }
			set
			{			
				_doctorName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "奖金金额", ShortCode = "Amount", Desc = "奖金金额", ContextType = SysDic.All, Length = 8)]
        public virtual double? Amount
		{
			get { return _amount; }
			set { _amount = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "开单数量", ShortCode = "OrderFormCount", Desc = "开单数量", ContextType = SysDic.All, Length = 8)]
        public virtual long? OrderFormCount
		{
			get { return _orderFormCount; }
			set { _orderFormCount = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "开单金额", ShortCode = "OrderFormAmount", Desc = "开单金额", ContextType = SysDic.All, Length = 8)]
        public virtual double? OrderFormAmount
		{
			get { return _orderFormAmount; }
			set { _orderFormAmount = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "计算比率", ShortCode = "Percent", Desc = "计算比率", ContextType = SysDic.All, Length = 8)]
        public virtual double? Percent
		{
			get { return _percent; }
			set { _percent = value; }
		}

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = 500)]
        public virtual string Memo
		{
			get { return _memo; }
			set
			{
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
        [DataDesc(CName = "结算处理人", ShortCode = "BonusOneReviewManName", Desc = "结算处理人", ContextType = SysDic.All, Length = 50)]
        public virtual string BonusOneReviewManName
		{
			get { return _bonusOneReviewManName; }
			set
			{
				_bonusOneReviewManName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "结算处理人ID", ShortCode = "BonusOneReviewManID", Desc = "结算处理人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? BonusOneReviewManID
		{
			get { return _bonusOneReviewManID; }
			set { _bonusOneReviewManID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "结算处理开始时间", ShortCode = "BonusOneReviewStartTime", Desc = "结算处理开始时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? BonusOneReviewStartTime
		{
			get { return _bonusOneReviewStartTime; }
			set { _bonusOneReviewStartTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "结算处理完成时间", ShortCode = "BonusOneReviewFinishTime", Desc = "结算处理完成时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? BonusOneReviewFinishTime
		{
			get { return _bonusOneReviewFinishTime; }
			set { _bonusOneReviewFinishTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "结算审批人", ShortCode = "BonusTwoReviewManName", Desc = "结算审批人", ContextType = SysDic.All, Length = 50)]
        public virtual string BonusTwoReviewManName
		{
			get { return _bonusTwoReviewManName; }
			set
			{
				_bonusTwoReviewManName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "退款审批人ID", ShortCode = "BonusTwoReviewManID", Desc = "退款审批人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? BonusTwoReviewManID
		{
			get { return _bonusTwoReviewManID; }
			set { _bonusTwoReviewManID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "BonusTwoReviewStartTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? BonusTwoReviewStartTime
		{
			get { return _bonusTwoReviewStartTime; }
			set { _bonusTwoReviewStartTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "结算审批完成时间", ShortCode = "BonusTwoReviewFinishTime", Desc = "结算审批完成时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? BonusTwoReviewFinishTime
		{
			get { return _bonusTwoReviewFinishTime; }
			set { _bonusTwoReviewFinishTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "结算发放人", ShortCode = "BonusThreeReviewManName", Desc = "结算发放人", ContextType = SysDic.All, Length = 50)]
        public virtual string BonusThreeReviewManName
		{
			get { return _bonusThreeReviewManName; }
			set
			{
				_bonusThreeReviewManName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "结算发放人ID", ShortCode = "BonusThreeReviewManID", Desc = "结算发放人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? BonusThreeReviewManID
		{
			get { return _bonusThreeReviewManID; }
			set { _bonusThreeReviewManID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "结算发放开始时间", ShortCode = "BonusThreeReviewStartTime", Desc = "结算发放开始时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? BonusThreeReviewStartTime
		{
			get { return _bonusThreeReviewStartTime; }
			set { _bonusThreeReviewStartTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "结算发放完成时间", ShortCode = "BonusThreeReviewFinishTime", Desc = "结算发放完成时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? BonusThreeReviewFinishTime
		{
			get { return _bonusThreeReviewFinishTime; }
			set { _bonusThreeReviewFinishTime = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "1、微信（企业付款：后期实现）", ShortCode = "PaymentMethod", Desc = "1、微信（企业付款：后期实现）", ContextType = SysDic.All, Length = 8)]
        public virtual long? PaymentMethod
		{
			get { return _paymentMethod; }
			set { _paymentMethod = value; }
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
				_bankTransFormCode = value;
			}
		}
        [DataMember]
        [DataDesc(CName = "开户行地址", ShortCode = "BankAddress", Desc = "开户行地址", ContextType = SysDic.All, Length = 50)]
        public virtual string BankAddress
        {
            get { return _BankAddress; }
            set
            {
                _BankAddress = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "MobileCode", Desc = "", ContextType = SysDic.All, Length = 11)]
        public virtual string MobileCode
        {
            get { return _mobileCode; }
            set
            {
                if (value != null && value.Length > 11)
                    throw new ArgumentOutOfRangeException("Invalid value for MobileCode", value, value.ToString());
                _mobileCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IDNumber", Desc = "", ContextType = SysDic.All, Length = 18)]
        public virtual string IDNumber
        {
            get { return _iDNumber; }
            set
            {
                if (value != null && value.Length > 18)
                    throw new ArgumentOutOfRangeException("Invalid value for IDNumber", value, value.ToString());
                _iDNumber = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "微信返回状态码", ShortCode = "ReturnCode", Desc = "微信返回状态码", ContextType = SysDic.All, Length = 50)]
        public virtual string ReturnCode
        {
            get { return _ReturnCode; }
            set
            {
                _ReturnCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "微信业务结果", ShortCode = "ResultCode", Desc = "微信业务结果", ContextType = SysDic.All, Length = 50)]
        public virtual string ResultCode
        {
            get { return _ResultCode; }
            set
            {
                _ResultCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "微信返回信息", ShortCode = "ReturnMsg", Desc = "微信返回信息", ContextType = SysDic.All, Length = 50)]
        public virtual string ReturnMsg
        {
            get { return _ReturnMsg; }
            set
            {
                _ReturnMsg = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "微信错误代码", ShortCode = "ErrCode", Desc = "微信错误代码", ContextType = SysDic.All, Length = 50)]
        public virtual string ErrCode
        {
            get { return _ErrCode; }
            set
            {
                _ErrCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "微信错误代码描述", ShortCode = "ErrCodeDes", Desc = "微信错误代码描述", ContextType = SysDic.All, Length = 50)]
        public virtual string ErrCodeDes
        {
            get { return _ErrCodeDes; }
            set
            {
                _ErrCodeDes = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "微信错误描述", ShortCode = "ErrorMemo", Desc = "微信错误描述", ContextType = SysDic.All, Length = 50)]
        public virtual string ErrorMemo
        {
            get { return _ErrorMemo; }
            set
            {
                _ErrorMemo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "微信企业付款订单号", ShortCode = "PaymentNo", Desc = "微信企业付款订单号", ContextType = SysDic.All, Length = 50)]
        public virtual string PaymentNo
        {
            get { return _PaymentNo; }
            set
            {
                _PaymentNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "微信支付成功时间", ShortCode = "BankAddress", Desc = "微信支付成功时间", ContextType = SysDic.All, Length = 50)]
        public virtual string PaymentTime
        {
            get { return _PaymentTime; }
            set
            {
                _PaymentTime = value;
            }
        }

        #endregion
    }
	#endregion
}