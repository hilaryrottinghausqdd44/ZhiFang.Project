using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Model
{
    public class OSUserOrderForm
    {
        public OSUserOrderForm()
        { }
        #region Model
        private long _areaid;
        private long _hospitalid;
        private long _uofid;
        private string _uofcode;
        private long? _dofid;
        private long? _doctoraccountid;
        private long? _os_userconsumerformid;
        private string _os_userconsumerformcode;
        private long? _weixinuserid;
        private string _doctoropenid;
        private string _doctorname;
        private long? _useraccountid;
        private long? _userweixinuserid;
        private string _username;
        private string _useropenid;
        private long? _status;
        private string _paycode;
        private string _memo;
        private int? _disporder;
        private bool _isuse;
        private DateTime? _dataaddtime;
        private DateTime? _dataupdatetime;
        private DateTime? _datatimestamp;
        private decimal? _marketprice;
        private decimal? _greatmasterprice;
        private decimal? _discountprice;
        private decimal? _discount;
        private decimal? _price;
        private decimal? _adviceprice;
        private decimal? _refundprice;
        private DateTime? _paytime;
        private DateTime? _cancelapplytime;
        private DateTime? _cancelfinishedtime;
        private DateTime? _consumerstarttime;
        private DateTime? _consumerfinishedtime;
        private DateTime? _refundapplytime;
        private string _refundonereviewmanname;
        private long? _refundonereviewmanid;
        private DateTime? _refundonereviewstarttime;
        private DateTime? _refundonereviewfinishtime;
        private string _refundtworeviewmanname;
        private long? _refundtworeviewmanid;
        private DateTime? _refundtworeviewstarttime;
        private DateTime? _refundtworeviewfinishtime;
        private string _refundthreereviewmanname;
        private long? _refundthreereviewmanid;
        private DateTime? _refundthreereviewstarttime;
        private DateTime? _refundthreereviewfinishtime;
        private string _refundreason;
        private string _refundonereviewreason;
        private string _refundtworeviewreason;
        private string _refundthreereviewreason;
        private bool _isprepay;
        private string _prepayid;
        private DateTime? _prepaytime;
        private string _prepayreturncode;
        private string _prepayreturnmsg;
        private string _prepayerrcode;
        private string _prepayerrname;
        private string _transactionid;
        private string _column_59;
        private string _column_60;
        private string _column_61;
        private string _column_62;
        /// <summary>
        /// 区域ID
        /// </summary>
        public long AreaID
        {
            set { _areaid = value; }
            get { return _areaid; }
        }
        /// <summary>
        /// 医院ID
        /// </summary>
        public long HospitalID
        {
            set { _hospitalid = value; }
            get { return _hospitalid; }
        }
        /// <summary>
        /// 用户订单ID
        /// </summary>
        public long UOFID
        {
            set { _uofid = value; }
            get { return _uofid; }
        }
        /// <summary>
        /// 用户订单编号
        /// </summary>
        public string UOFCode
        {
            set { _uofcode = value; }
            get { return _uofcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long? DOFID
        {
            set { _dofid = value; }
            get { return _dofid; }
        }
        /// <summary>
        /// 医生账户信息ID
        /// </summary>
        public long? DoctorAccountID
        {
            set { _doctoraccountid = value; }
            get { return _doctoraccountid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long? OS_UserConsumerFormID
        {
            set { _os_userconsumerformid = value; }
            get { return _os_userconsumerformid; }
        }
        /// <summary>
        /// 消费单编号
        /// </summary>
        public string OS_UserConsumerFormCode
        {
            set { _os_userconsumerformcode = value; }
            get { return _os_userconsumerformcode; }
        }
        /// <summary>
        /// 医生微信ID
        /// </summary>
        public long? WeiXinUserID
        {
            set { _weixinuserid = value; }
            get { return _weixinuserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DoctorOpenID
        {
            set { _doctoropenid = value; }
            get { return _doctoropenid; }
        }
        /// <summary>
        /// 医生姓名
        /// </summary>
        public string DoctorName
        {
            set { _doctorname = value; }
            get { return _doctorname; }
        }
        /// <summary>
        /// 用户账户信息ID
        /// </summary>
        public long? UserAccountID
        {
            set { _useraccountid = value; }
            get { return _useraccountid; }
        }
        /// <summary>
        /// 用户微信ID
        /// </summary>
        public long? UserWeiXinUserID
        {
            set { _userweixinuserid = value; }
            get { return _userweixinuserid; }
        }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 用户OpenID
        /// </summary>
        public string UserOpenID
        {
            set { _useropenid = value; }
            get { return _useropenid; }
        }
        /// <summary>
        /// 订单状态
        /// </summary>
        public long? Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 消费码
        /// </summary>
        public string PayCode
        {
            set { _paycode = value; }
            get { return _paycode; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo
        {
            set { _memo = value; }
            get { return _memo; }
        }
        /// <summary>
        /// 显示次序
        /// </summary>
        public int? DispOrder
        {
            set { _disporder = value; }
            get { return _disporder; }
        }
        /// <summary>
        /// 是否使用
        /// </summary>
        public bool IsUse
        {
            set { _isuse = value; }
            get { return _isuse; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? DataAddTime
        {
            set { _dataaddtime = value; }
            get { return _dataaddtime; }
        }
        /// <summary>
        /// 数据更新时间
        /// </summary>
        public DateTime? DataUpdateTime
        {
            set { _dataupdatetime = value; }
            get { return _dataupdatetime; }
        }
        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime? DataTimeStamp
        {
            set { _datatimestamp = value; }
            get { return _datatimestamp; }
        }
        /// <summary>
        /// 市场价格
        /// </summary>
        public decimal? MarketPrice
        {
            set { _marketprice = value; }
            get { return _marketprice; }
        }
        /// <summary>
        /// 大家价格
        /// </summary>
        public decimal? GreatMasterPrice
        {
            set { _greatmasterprice = value; }
            get { return _greatmasterprice; }
        }
        /// <summary>
        /// 折扣价格
        /// </summary>
        public decimal? DiscountPrice
        {
            set { _discountprice = value; }
            get { return _discountprice; }
        }
        /// <summary>
        /// 折扣率
        /// </summary>
        public decimal? Discount
        {
            set { _discount = value; }
            get { return _discount; }
        }
        /// <summary>
        /// 实际金额
        /// </summary>
        public decimal? Price
        {
            set { _price = value; }
            get { return _price; }
        }
        /// <summary>
        /// 咨询费
        /// </summary>
        public decimal? AdvicePrice
        {
            set { _adviceprice = value; }
            get { return _adviceprice; }
        }
        /// <summary>
        /// 退费金额
        /// </summary>
        public decimal? RefundPrice
        {
            set { _refundprice = value; }
            get { return _refundprice; }
        }
        /// <summary>
        /// 缴费时间
        /// </summary>
        public DateTime? PayTime
        {
            set { _paytime = value; }
            get { return _paytime; }
        }
        /// <summary>
        /// 取消申请时间
        /// </summary>
        public DateTime? CancelApplyTime
        {
            set { _cancelapplytime = value; }
            get { return _cancelapplytime; }
        }
        /// <summary>
        /// 取消完成时间
        /// </summary>
        public DateTime? CancelFinishedTime
        {
            set { _cancelfinishedtime = value; }
            get { return _cancelfinishedtime; }
        }
        /// <summary>
        /// 消费开始时间
        /// </summary>
        public DateTime? ConsumerStartTime
        {
            set { _consumerstarttime = value; }
            get { return _consumerstarttime; }
        }
        /// <summary>
        /// 消费完成时间
        /// </summary>
        public DateTime? ConsumerFinishedTime
        {
            set { _consumerfinishedtime = value; }
            get { return _consumerfinishedtime; }
        }
        /// <summary>
        /// 退费申请时间
        /// </summary>
        public DateTime? RefundApplyTime
        {
            set { _refundapplytime = value; }
            get { return _refundapplytime; }
        }
        /// <summary>
        /// 退款处理人
        /// </summary>
        public string RefundOneReviewManName
        {
            set { _refundonereviewmanname = value; }
            get { return _refundonereviewmanname; }
        }
        /// <summary>
        /// 退款处理人ID
        /// </summary>
        public long? RefundOneReviewManID
        {
            set { _refundonereviewmanid = value; }
            get { return _refundonereviewmanid; }
        }
        /// <summary>
        /// 退款处理开始时间
        /// </summary>
        public DateTime? RefundOneReviewStartTime
        {
            set { _refundonereviewstarttime = value; }
            get { return _refundonereviewstarttime; }
        }
        /// <summary>
        /// 退款处理完成时间
        /// </summary>
        public DateTime? RefundOneReviewFinishTime
        {
            set { _refundonereviewfinishtime = value; }
            get { return _refundonereviewfinishtime; }
        }
        /// <summary>
        /// 退款审批人
        /// </summary>
        public string RefundTwoReviewManName
        {
            set { _refundtworeviewmanname = value; }
            get { return _refundtworeviewmanname; }
        }
        /// <summary>
        /// 退款审批人ID
        /// </summary>
        public long? RefundTwoReviewManID
        {
            set { _refundtworeviewmanid = value; }
            get { return _refundtworeviewmanid; }
        }
        /// <summary>
        /// 退款审批开始时间
        /// </summary>
        public DateTime? RefundTwoReviewStartTime
        {
            set { _refundtworeviewstarttime = value; }
            get { return _refundtworeviewstarttime; }
        }
        /// <summary>
        /// 退款审批时间
        /// </summary>
        public DateTime? RefundTwoReviewFinishTime
        {
            set { _refundtworeviewfinishtime = value; }
            get { return _refundtworeviewfinishtime; }
        }
        /// <summary>
        /// 退款发放人
        /// </summary>
        public string RefundThreeReviewManName
        {
            set { _refundthreereviewmanname = value; }
            get { return _refundthreereviewmanname; }
        }
        /// <summary>
        /// 退款发放人ID
        /// </summary>
        public long? RefundThreeReviewManID
        {
            set { _refundthreereviewmanid = value; }
            get { return _refundthreereviewmanid; }
        }
        /// <summary>
        /// 退款发放开始时间
        /// </summary>
        public DateTime? RefundThreeReviewStartTime
        {
            set { _refundthreereviewstarttime = value; }
            get { return _refundthreereviewstarttime; }
        }
        /// <summary>
        /// 退款发放完成时间
        /// </summary>
        public DateTime? RefundThreeReviewFinishTime
        {
            set { _refundthreereviewfinishtime = value; }
            get { return _refundthreereviewfinishtime; }
        }
        /// <summary>
        /// 退费原因
        /// </summary>
        public string RefundReason
        {
            set { _refundreason = value; }
            get { return _refundreason; }
        }
        /// <summary>
        /// 退款处理说明
        /// </summary>
        public string RefundOneReviewReason
        {
            set { _refundonereviewreason = value; }
            get { return _refundonereviewreason; }
        }
        /// <summary>
        /// 退款审批说明
        /// </summary>
        public string RefundTwoReviewReason
        {
            set { _refundtworeviewreason = value; }
            get { return _refundtworeviewreason; }
        }
        /// <summary>
        /// 退款发放说明
        /// </summary>
        public string RefundThreeReviewReason
        {
            set { _refundthreereviewreason = value; }
            get { return _refundthreereviewreason; }
        }
        /// <summary>
        /// 是否已预下单
        /// </summary>
        public bool IsPrePay
        {
            set { _isprepay = value; }
            get { return _isprepay; }
        }
        /// <summary>
        /// WeiXin统一下单编码
        /// </summary>
        public string PrePayId
        {
            set { _prepayid = value; }
            get { return _prepayid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? PrePayTime
        {
            set { _prepaytime = value; }
            get { return _prepaytime; }
        }
        /// <summary>
        /// 预下单通信结果
        /// </summary>
        public string PrePayReturnCode
        {
            set { _prepayreturncode = value; }
            get { return _prepayreturncode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PrePayReturnMsg
        {
            set { _prepayreturnmsg = value; }
            get { return _prepayreturnmsg; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PrePayErrCode
        {
            set { _prepayerrcode = value; }
            get { return _prepayerrcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PrePayErrName
        {
            set { _prepayerrname = value; }
            get { return _prepayerrname; }
        }
        /// <summary>
        /// 微信订单号
        /// </summary>
        public string TransactionId
        {
            set { _transactionid = value; }
            get { return _transactionid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Column_59
        {
            set { _column_59 = value; }
            get { return _column_59; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Column_60
        {
            set { _column_60 = value; }
            get { return _column_60; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Column_61
        {
            set { _column_61 = value; }
            get { return _column_61; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Column_62
        {
            set { _column_62 = value; }
            get { return _column_62; }
        }
        #endregion Model
    }
}
