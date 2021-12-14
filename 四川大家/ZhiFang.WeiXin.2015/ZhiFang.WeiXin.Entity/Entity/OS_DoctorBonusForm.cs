using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;
using ZhiFang.WeiXin.Entity.ViewObject.Request;

namespace ZhiFang.WeiXin.Entity
{
	#region OSDoctorBonusForm

	/// <summary>
	/// OSDoctorBonusForm object for NHibernate mapped table 'OS_DoctorBonusForm'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "医生奖金结算单", ClassCName = "OSDoctorBonusForm", ShortCode = "OSDoctorBonusForm", Desc = "医生奖金结算单")]
	public class OSDoctorBonusForm : BusinessBase
    {
		#region Member Variables
		
        protected long? _doctorCount;
        protected long? _orderFormCount;
        protected double? _amount;
        protected double? _orderFormAmount;
        protected string _bonusFormCode;
        protected string _bonusFormRound;
        protected string _bonusApplyManName;
        protected long? _bonusApplyManID;
        protected DateTime? _bonusApplytTime;
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
        protected string _memo;
        protected int _dispOrder;
        protected bool _isUse;
        protected DateTime? _dataUpdateTime;
        protected long? _status;


        #endregion

        #region Constructors



        #endregion

        #region Public Properties
        
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医生数量", ShortCode = "DoctorCount", Desc = "医生数量", ContextType = SysDic.All, Length = 8)]
        public virtual long? DoctorCount
		{
			get { return _doctorCount; }
			set { _doctorCount = value; }
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
        [DataDesc(CName = "结算金额", ShortCode = "Amount", Desc = "结算金额", ContextType = SysDic.All, Length = 8)]
        public virtual double? Amount
		{
			get { return _amount; }
			set { _amount = value; }
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
        [DataDesc(CName = "结算单号", ShortCode = "BonusFormCode", Desc = "结算单号", ContextType = SysDic.All, Length = 30)]
        public virtual string BonusFormCode
		{
			get { return _bonusFormCode; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for BonusFormCode", value, value.ToString());
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
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for BonusFormRound", value, value.ToString());
				_bonusFormRound = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "结算处理人", ShortCode = "BonusApplyManName", Desc = "结算处理人", ContextType = SysDic.All, Length = 50)]
        public virtual string BonusApplyManName
		{
			get { return _bonusApplyManName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for BonusApplyManName", value, value.ToString());
				_bonusApplyManName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "结算处理人ID", ShortCode = "BonusApplyManID", Desc = "结算处理人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? BonusApplyManID
		{
			get { return _bonusApplyManID; }
			set { _bonusApplyManID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "结算处理开始时间", ShortCode = "BonusApplytTime", Desc = "结算处理开始时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? BonusApplytTime
		{
			get { return _bonusApplytTime; }
			set { _bonusApplytTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "结算处理人", ShortCode = "BonusOneReviewManName", Desc = "结算处理人", ContextType = SysDic.All, Length = 50)]
        public virtual string BonusOneReviewManName
		{
			get { return _bonusOneReviewManName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for BonusOneReviewManName", value, value.ToString());
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
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for BonusTwoReviewManName", value, value.ToString());
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
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for BonusThreeReviewManName", value, value.ToString());
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

		
		#endregion
	}
	#endregion
}