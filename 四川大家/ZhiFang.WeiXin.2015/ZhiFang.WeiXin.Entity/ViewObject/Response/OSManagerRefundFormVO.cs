using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity.ViewObject.Response
{
    [DataContract]
    [DataDesc(CName = "退费单VO", ClassCName = "OSManagerRefundFormVO", ShortCode = "OSManagerRefundFormVO", Desc = "退费单VO")]
    public class RFVO : BaseEntity
    {
        public RFVO()
        {
            RFOVOList = new List<RFOVO>();
        }
        #region Member Variables

        protected string _mRefundFormCode;
        protected long? _uOFID;
        protected string _uOFCode;
        protected long? _oSUserConsumerFormID;
        protected string _oSUserConsumerFormCode;
        protected string _doctorOpenID;
        protected string _doctorName;
        protected long? _userAccountID;
        protected long? _userWeiXinUserID;
        protected string _userName;
        protected string _userOpenID;
        protected long _status;
        protected string _memo;
        protected int _dispOrder;
        protected bool _isUse;
        protected DateTime? _dataUpdateTime;
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


        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "退费单编号", ShortCode = "MRefundFormCode", Desc = "退费单编号", ContextType = SysDic.All, Length = 30)]
        public virtual string MRFCode
        {
            get { return _mRefundFormCode; }
            set
            {
                if (value != null && value.Length > 30)
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
        [DataDesc(CName = "", ShortCode = "OSUserConsumerFormID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? UCFID
        {
            get { return _oSUserConsumerFormID; }
            set { _oSUserConsumerFormID = value; }
        }

        [DataMember]
        [DataDesc(CName = "消费单编号", ShortCode = "OSUserConsumerFormCode", Desc = "消费单编号", ContextType = SysDic.All, Length = 30)]
        public virtual string UCFCode
        {
            get { return _oSUserConsumerFormCode; }
            set
            {
                if (value != null && value.Length > 30)
                    throw new ArgumentOutOfRangeException("Invalid value for OSUserConsumerFormCode", value, value.ToString());
                _oSUserConsumerFormCode = value;
            }
        }       

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DoctorOpenID", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string DOpenID
        {
            get { return _doctorOpenID; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for DoctorOpenID", value, value.ToString());
                _doctorOpenID = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "医生姓名", ShortCode = "DoctorName", Desc = "医生姓名", ContextType = SysDic.All, Length = 20)]
        public virtual string DN
        {
            get { return _doctorName; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for DoctorName", value, value.ToString());
                _doctorName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "用户账户信息ID", ShortCode = "UserAccountID", Desc = "用户账户信息ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? UAID
        {
            get { return _userAccountID; }
            set { _userAccountID = value; }
        }

        [DataMember]
        [DataDesc(CName = "用户姓名", ShortCode = "UserName", Desc = "用户姓名", ContextType = SysDic.All, Length = 20)]
        public virtual string UN
        {
            get { return _userName; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for UserName", value, value.ToString());
                _userName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "用户OpenID", ShortCode = "UserOpenID", Desc = "用户OpenID", ContextType = SysDic.All, Length = 50)]
        public virtual string UOpenID
        {
            get { return _userOpenID; }
            set
            {
                if (value != null && value.Length > 50)
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
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = 500)]
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
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IU
        {
            get { return _isUse; }
            set { _isUse = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DUT
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }       

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "实际金额", ShortCode = "Price", Desc = "实际金额", ContextType = SysDic.All, Length = 8)]
        public virtual double? Pe
        {
            get { return _price; }
            set { _price = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "退费金额", ShortCode = "RefundPrice", Desc = "退费金额", ContextType = SysDic.All, Length = 8)]
        public virtual double? RP
        {
            get { return _refundPrice; }
            set { _refundPrice = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "缴费时间", ShortCode = "PayTime", Desc = "缴费时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? PT
        {
            get { return _payTime; }
            set { _payTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "退费申请人", ShortCode = "RefundApplyManName", Desc = "退费申请人", ContextType = SysDic.All, Length = 50)]
        public virtual string RAManName
        {
            get { return _refundApplyManName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for RefundApplyManName", value, value.ToString());
                _refundApplyManName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "退费申请人ID", ShortCode = "RefundApplyManID", Desc = "退费申请人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? RAManID
        {
            get { return _refundApplyManID; }
            set { _refundApplyManID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "退费申请时间", ShortCode = "RefundApplyTime", Desc = "退费申请时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? RAT
        {
            get { return _refundApplyTime; }
            set { _refundApplyTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "退款处理人", ShortCode = "RefundOneReviewManName", Desc = "退款处理人", ContextType = SysDic.All, Length = 50)]
        public virtual string ROneReManName
        {
            get { return _refundOneReviewManName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for RefundOneReviewManName", value, value.ToString());
                _refundOneReviewManName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "退款处理人ID", ShortCode = "RefundOneReviewManID", Desc = "退款处理人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ROneReManID
        {
            get { return _refundOneReviewManID; }
            set { _refundOneReviewManID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "退款处理开始时间", ShortCode = "RefundOneReviewStartTime", Desc = "退款处理开始时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ROneReST
        {
            get { return _refundOneReviewStartTime; }
            set { _refundOneReviewStartTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "退款处理完成时间", ShortCode = "RefundOneReviewFinishTime", Desc = "退款处理完成时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ROneReFT
        {
            get { return _refundOneReviewFinishTime; }
            set { _refundOneReviewFinishTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "退款审批人", ShortCode = "RefundTwoReviewManName", Desc = "退款审批人", ContextType = SysDic.All, Length = 50)]
        public virtual string RTwoReManName
        {
            get { return _refundTwoReviewManName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for RefundTwoReviewManName", value, value.ToString());
                _refundTwoReviewManName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "退款审批人ID", ShortCode = "RefundTwoReviewManID", Desc = "退款审批人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? RTwoReManID
        {
            get { return _refundTwoReviewManID; }
            set { _refundTwoReviewManID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "退款审批开始时间", ShortCode = "RefundTwoReviewStartTime", Desc = "退款审批开始时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? RTwoReST
        {
            get { return _refundTwoReviewStartTime; }
            set { _refundTwoReviewStartTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "退款审批时间", ShortCode = "RefundTwoReviewFinishTime", Desc = "退款审批时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? RTwoReFT
        {
            get { return _refundTwoReviewFinishTime; }
            set { _refundTwoReviewFinishTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "退款发放人", ShortCode = "RefundThreeReviewManName", Desc = "退款发放人", ContextType = SysDic.All, Length = 50)]
        public virtual string RThreeReManName
        {
            get { return _refundThreeReviewManName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for RefundThreeReviewManName", value, value.ToString());
                _refundThreeReviewManName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "退款发放人ID", ShortCode = "RefundThreeReviewManID", Desc = "退款发放人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? RThreeReManID
        {
            get { return _refundThreeReviewManID; }
            set { _refundThreeReviewManID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "退款发放开始时间", ShortCode = "RefundThreeReviewStartTime", Desc = "退款发放开始时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? RThreeReST
        {
            get { return _refundThreeReviewStartTime; }
            set { _refundThreeReviewStartTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "退款发放完成时间", ShortCode = "RefundThreeReviewFinishTime", Desc = "退款发放完成时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? RThreeReFT
        {
            get { return _refundThreeReviewFinishTime; }
            set { _refundThreeReviewFinishTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "退费原因", ShortCode = "RefundReason", Desc = "退费原因", ContextType = SysDic.All, Length = 900)]
        public virtual string RR
        {
            get { return _refundReason; }
            set
            {
                if (value != null && value.Length > 900)
                    throw new ArgumentOutOfRangeException("Invalid value for RefundReason", value, value.ToString());
                _refundReason = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "退款处理说明", ShortCode = "RefundOneReviewReason", Desc = "退款处理说明", ContextType = SysDic.All, Length = 900)]
        public virtual string ROneReR
        {
            get { return _refundOneReviewReason; }
            set
            {
                if (value != null && value.Length > 900)
                    throw new ArgumentOutOfRangeException("Invalid value for RefundOneReviewReason", value, value.ToString());
                _refundOneReviewReason = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "退款审批说明", ShortCode = "RefundTwoReviewReason", Desc = "退款审批说明", ContextType = SysDic.All, Length = 900)]
        public virtual string RTwoReR
        {
            get { return _refundTwoReviewReason; }
            set
            {
                if (value != null && value.Length > 900)
                    throw new ArgumentOutOfRangeException("Invalid value for RefundTwoReviewReason", value, value.ToString());
                _refundTwoReviewReason = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "退款发放说明", ShortCode = "RefundThreeReviewReason", Desc = "退款发放说明", ContextType = SysDic.All, Length = 900)]
        public virtual string RThreeReR
        {
            get { return _refundThreeReviewReason; }
            set
            {
                if (value != null && value.Length > 900)
                    throw new ArgumentOutOfRangeException("Invalid value for RefundThreeReviewReason", value, value.ToString());
                _refundThreeReviewReason = value;
            }
        }       

        [DataMember]
        [DataDesc(CName = "微信退款单号", ShortCode = "RefundId", Desc = "微信退款单号", ContextType = SysDic.All, Length = 50)]
        public virtual string RId
        {
            get { return _refundId; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for RefundId", value, value.ToString());
                _refundId = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "1、微信退回", ShortCode = "RefundType", Desc = "1、微信退回", ContextType = SysDic.All, Length = 8)]
        public virtual long? RType
        {
            get { return _refundType; }
            set { _refundType = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "银行种类", ShortCode = "BankID", Desc = "银行种类", ContextType = SysDic.All, Length = 8)]
        public virtual long? BID
        {
            get { return _bankID; }
            set { _bankID = value; }
        }

        [DataMember]
        [DataDesc(CName = "银行帐号", ShortCode = "BankAccount", Desc = "银行帐号", ContextType = SysDic.All, Length = 50)]
        public virtual string BA
        {
            get { return _bankAccount; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for BankAccount", value, value.ToString());
                _bankAccount = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "银行转账单号", ShortCode = "BankTransFormCode", Desc = "银行转账单号", ContextType = SysDic.All, Length = 50)]
        public virtual string BTFCode
        {
            get { return _bankTransFormCode; }
            set
            {
                _bankTransFormCode = value;
            }
        }


        #endregion

        [DataMember]
        public List<RFOVO> RFOVOList { get; set; }
    }

    [DataContract]
    [DataDesc(CName = "退费单操作VO", ClassCName = "OSManagerRefundFormOperationVO", ShortCode = "OSManagerRefundFormOperationVO", Desc = "退费单操作VO")]
    public class RFOVO : BaseEntity
    {
        #region Member Variables

        protected string _typeName;
        protected string _memo;
        protected int _dispOrder;
        protected bool _isUse;
        protected string _creatorName;
        protected DateTime? _dataUpdateTime;

        #endregion

        #region Public Properties
        [DataMember]
        [DataDesc(CName = "操作类型名", ShortCode = "TypeName", Desc = "操作类型名", ContextType = SysDic.All, Length = 20)]
        public virtual string TN
        {
            get { return _typeName; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for TypeName", value, value.ToString());
                _typeName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = 500)]
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
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IU
        {
            get { return _isUse; }
            set { _isUse = value; }
        }

        [DataMember]
        [DataDesc(CName = "创建者姓名", ShortCode = "CreatorName", Desc = "创建者姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string CN
        {
            get { return _creatorName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for CreatorName", value, value.ToString());
                _creatorName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据修改时间", ShortCode = "DataUpdateTime", Desc = "数据修改时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DUT
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }


        #endregion
    }
}
