using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Model
{
    public partial class OSUserConsumerItem
    {
        public OSUserConsumerItem()
        { }
        #region Model
        private long _areaid;
        private long _hospitalid;
        private long _os_userconsumeritemid;
        private long? _os_userconsumerformid;
        private long? _recommendationitemproductid;
        private long? _uoiid;
        private long? _itemid;
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
        /// 
        /// </summary>
        public long OS_UserConsumerItemID
        {
            set { _os_userconsumeritemid = value; }
            get { return _os_userconsumeritemid; }
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
        /// 特推项目产品ID
        /// </summary>
        public long? RecommendationItemProductID
        {
            set { _recommendationitemproductid = value; }
            get { return _recommendationitemproductid; }
        }
        /// <summary>
        /// 用户订单项目ID
        /// </summary>
        public long? UOIID
        {
            set { _uoiid = value; }
            get { return _uoiid; }
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
