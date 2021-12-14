using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Model
{
    public class OSUserOrderItem
    {
        public OSUserOrderItem()
        { }
        #region Model
        private long _areaid;
        private long _hospitalid;
        private long _uoiid;
        private long? _uofid;
        private long? _os_userconsumerformid;
        private long? _itemid;
        private long? _recommendationitemproductid;
        private long? _doiid;
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
        private long? _status;
        private string _paycode;
        private DateTime? _cancelapplytime;
        private DateTime? _cancelfinishedtime;
        private DateTime? _consumerstarttime;
        private DateTime? _consumerfinishedtime;
        private string _itemno;
        protected string _ItemCName;
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
        /// 用户订单项目ID
        /// </summary>
        public long UOIID
        {
            set { _uoiid = value; }
            get { return _uoiid; }
        }
        /// <summary>
        /// 用户订单ID
        /// </summary>
        public long? UOFID
        {
            set { _uofid = value; }
            get { return _uofid; }
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
        /// 项目ID
        /// </summary>
        public long? ItemID
        {
            set { _itemid = value; }
            get { return _itemid; }
        }
        /// <summary>
        /// 特推项目产品ID
        /// </summary>
        public long? RecommendationItemProductID
        {
            set { _recommendationitemproductid = value; }
            get { return _recommendationitemproductid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long? DOIID
        {
            set { _doiid = value; }
            get { return _doiid; }
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
        /// 
        /// </summary>
        public string ItemNo
        {
            set { _itemno = value; }
            get { return _itemno; }
        }
        public virtual string ItemCName
        {
            get { return _ItemCName; }
            set { _ItemCName = value; }
        }
        #endregion Model

    }
}
