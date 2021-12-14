using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Model
{
    public class OSUserConsumerForm
    {
        public OSUserConsumerForm()
        { }
        #region Model
        private long _areaid;
        private long _hospitalid;
        private long _os_userconsumerformid;
        private string _os_userconsumerformcode;
        private long? _nrqfid;
        private long? _dofid;
        private long? _doctoraccountid;
        private long? _weixinuserid;
        private string _doctoropenid;
        private string _doctorname;
        private DateTime? _datatimestamp;
        private decimal? _marketprice;
        private decimal? _greatmasterprice;
        private decimal? _discountprice;
        private decimal? _discount;
        private decimal? _price;
        private decimal? _adviceprice;
        private long? _useraccountid;
        private long? _userweixinuserid;
        private long? _os_doctorbonusid;
        private string _username;
        private string _useropenid;
        private long? _status;
        private string _paycode;
        private long? _orgid;
        private string _weblisSourceorgid;
        private string _weblisSourceorgname;
        private long? _empid;
        private string _empname;
        private string _memo;
        private int? _disporder;
        private bool _isuse;
        private DateTime? _dataaddtime;
        private DateTime? _dataupdatetime;
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
        public long OS_UserConsumerFormID
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
        /// 申请单ID
        /// </summary>
        public long? NRQFID
        {
            set { _nrqfid = value; }
            get { return _nrqfid; }
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
        /// 医生奖金结算记录ID
        /// </summary>
        public long? OS_DoctorBonusID
        {
            set { _os_doctorbonusid = value; }
            get { return _os_doctorbonusid; }
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
        /// 采血站点ID
        /// </summary>
        public long? OrgID
        {
            set { _orgid = value; }
            get { return _orgid; }
        }
        /// <summary>
        /// 采血站点组织机构代码
        /// </summary>
        public string WeblisSourceOrgID
        {
            set { _weblisSourceorgid = value; }
            get { return _weblisSourceorgid; }
        }
        /// <summary>
        /// 采血站点名称
        /// </summary>
        public string WeblisSourceOrgName
        {
            set { _weblisSourceorgname = value; }
            get { return _weblisSourceorgname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long? EmpID
        {
            set { _empid = value; }
            get { return _empid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EmpAccount
        {
            set { _empname = value; }
            get { return _empname; }
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

        public string ConsumerAreaID { get; set; }
        #endregion Model
    }
}
