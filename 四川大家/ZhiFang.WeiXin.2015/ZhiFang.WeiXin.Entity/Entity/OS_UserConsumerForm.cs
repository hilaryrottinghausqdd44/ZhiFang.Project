using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
	#region OSUserConsumerForm

	/// <summary>
	/// OSUserConsumerForm object for NHibernate mapped table 'OS_UserConsumerForm'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "用户消费单", ClassCName = "OSUserConsumerForm", ShortCode = "OSUserConsumerForm", Desc = "用户消费单")]
	public class OSUserConsumerForm : BaseEntity
	{
		#region Member Variables
		
        protected long? _areaID;
        protected long? _hospitalID;
        protected string _oSUserConsumerFormCode;
        protected long? _nRQFID;
        protected long? _dOFID;
        protected long? _doctorAccountID;
        protected long? _weiXinUserID;
        protected string _doctorOpenID;
        protected string _doctorName;
        protected double? _marketPrice;
        protected double? _greatMasterPrice;
        protected double? _discountPrice;
        protected double? _discount;
        protected double? _price;
        protected double? _advicePrice;
        protected long? _userAccountID;
        protected long? _userWeiXinUserID;
        protected long? _oSDoctorBonusID;
        protected string _userName;
        protected string _userOpenID;
        protected long? _status;
        protected string _payCode;
        protected long? _orgID;
        protected string _weblisSourceOrgID;
        protected string _weblisSourceOrgName;
        protected long? _empID;
        protected string _empName;
        protected long? _ConsumerAreaID;
        protected string _memo;
        protected int _dispOrder;
        protected bool _isUse;
        protected DateTime? _dataUpdateTime;
        protected long? _TypeID;
        protected string _TypeName;
        protected bool _CollectionFlag;
        protected double _CollectionPrice;


        #endregion

        #region Constructors

        public OSUserConsumerForm() { }

		public OSUserConsumerForm( long? areaID, long? hospitalID, string oSUserConsumerFormCode, long? nRQFID, long? dOFID, long? doctorAccountID, long? weiXinUserID, string doctorOpenID, string doctorName, byte[] dataTimeStamp, double? marketPrice, double? greatMasterPrice, double? discountPrice, double? discount, double? price, double? advicePrice, long? userAccountID, long? userWeiXinUserID, long? oSDoctorBonusID, string userName, string userOpenID, long? status, string payCode, long? orgID, string weblisSourceOrgID, string weblisSourceOrgName, long? empID, string empName, string memo, int dispOrder, bool isUse, DateTime dataAddTime, DateTime dataUpdateTime )
		{
			this._areaID = areaID;
			this._hospitalID = hospitalID;
			this._oSUserConsumerFormCode = oSUserConsumerFormCode;
			this._nRQFID = nRQFID;
			this._dOFID = dOFID;
			this._doctorAccountID = doctorAccountID;
			this._weiXinUserID = weiXinUserID;
			this._doctorOpenID = doctorOpenID;
			this._doctorName = doctorName;
			this._dataTimeStamp = dataTimeStamp;
			this._marketPrice = marketPrice;
			this._greatMasterPrice = greatMasterPrice;
			this._discountPrice = discountPrice;
			this._discount = discount;
			this._price = price;
			this._advicePrice = advicePrice;
			this._userAccountID = userAccountID;
			this._userWeiXinUserID = userWeiXinUserID;
			this._oSDoctorBonusID = oSDoctorBonusID;
			this._userName = userName;
			this._userOpenID = userOpenID;
			this._status = status;
			this._payCode = payCode;
			this._orgID = orgID;
			this._weblisSourceOrgID = weblisSourceOrgID;
			this._weblisSourceOrgName = weblisSourceOrgName;
			this._empID = empID;
			this._empName = empName;
			this._memo = memo;
			this._dispOrder = dispOrder;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "区域ID", ShortCode = "AreaID", Desc = "区域ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? AreaID
		{
			get { return _areaID; }
			set { _areaID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医院ID", ShortCode = "HospitalID", Desc = "医院ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? HospitalID
		{
			get { return _hospitalID; }
			set { _hospitalID = value; }
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
        [DataDesc(CName = "申请单ID", ShortCode = "NRQFID", Desc = "申请单ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? NRQFID
		{
			get { return _nRQFID; }
			set { _nRQFID = value; }
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
        [DataDesc(CName = "市场价格", ShortCode = "MarketPrice", Desc = "市场价格", ContextType = SysDic.All, Length = 8)]
        public virtual double? MarketPrice
		{
			get { return _marketPrice; }
			set { _marketPrice = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "大家价格", ShortCode = "GreatMasterPrice", Desc = "大家价格", ContextType = SysDic.All, Length = 8)]
        public virtual double? GreatMasterPrice
		{
			get { return _greatMasterPrice; }
			set { _greatMasterPrice = value; }
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
        [DataDesc(CName = "咨询费", ShortCode = "AdvicePrice", Desc = "咨询费", ContextType = SysDic.All, Length = 8)]
        public virtual double? AdvicePrice
		{
			get { return _advicePrice; }
			set { _advicePrice = value; }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医生奖金结算记录ID", ShortCode = "OSDoctorBonusID", Desc = "医生奖金结算记录ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? OSDoctorBonusID
		{
			get { return _oSDoctorBonusID; }
			set { _oSDoctorBonusID = value; }
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
        public virtual long? Status
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
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for WeblisOrgID", value, value.ToString());
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
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for WeblisOrgName", value, value.ToString());
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
        #endregion
    }
	#endregion
}