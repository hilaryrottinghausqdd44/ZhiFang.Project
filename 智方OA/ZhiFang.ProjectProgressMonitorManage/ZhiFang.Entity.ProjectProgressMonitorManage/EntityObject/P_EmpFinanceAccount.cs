using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ProjectProgressMonitorManage.ViewObject.Request;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
	#region PEmpFinanceAccount

	/// <summary>
	/// PEmpFinanceAccount object for NHibernate mapped table 'P_EmpFinanceAccount'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "员工财务账户", ClassCName = "PEmpFinanceAccount", ShortCode = "PEmpFinanceAccount", Desc = "员工财务账户")]
	public class PEmpFinanceAccount : PEmpFinanceAccount_Business
    {
		#region Member Variables
		
        protected long? _empID;
        protected string _name;
        protected string _sName;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected int _dispOrder;
        protected string _comment;
        protected bool _isUse;
        protected double _unRepaymentAmount;
        protected double _loanUpperAmount;
        protected double _oneceLoanUpperAmount;
        protected double _loanAmount;
        protected double _repaymentAmount;
		

		#endregion

		#region Constructors

		public PEmpFinanceAccount() { }

		public PEmpFinanceAccount( long labID, long empID, string name, string sName, string shortcode, string pinYinZiTou, int dispOrder, string comment, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp, double unRepaymentAmount, double loanUpperAmount, double oneceLoanUpperAmount, double loanAmount, double repaymentAmount )
		{
			this._labID = labID;
			this._empID = empID;
			this._name = name;
			this._sName = sName;
			this._shortcode = shortcode;
			this._pinYinZiTou = pinYinZiTou;
			this._dispOrder = dispOrder;
			this._comment = comment;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._unRepaymentAmount = unRepaymentAmount;
			this._loanUpperAmount = loanUpperAmount;
			this._oneceLoanUpperAmount = oneceLoanUpperAmount;
			this._loanAmount = loanAmount;
			this._repaymentAmount = repaymentAmount;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "员工ID", ShortCode = "EmpID", Desc = "员工ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? EmpID
		{
			get { return _empID; }
			set { _empID = value; }
		}

        [DataMember]
        [DataDesc(CName = "员工名称", ShortCode = "Name", Desc = "员工名称", ContextType = SysDic.All, Length = 40)]
        public virtual string Name
		{
			get { return _name; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for Name", value, value.ToString());
				_name = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 40)]
        public virtual string SName
		{
			get { return _sName; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for SName", value, value.ToString());
				_sName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "快捷码", ShortCode = "Shortcode", Desc = "快捷码", ContextType = SysDic.All, Length = 20)]
        public virtual string Shortcode
		{
			get { return _shortcode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Shortcode", value, value.ToString());
				_shortcode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "汉字拼音字头", ShortCode = "PinYinZiTou", Desc = "汉字拼音字头", ContextType = SysDic.All, Length = 50)]
        public virtual string PinYinZiTou
		{
			get { return _pinYinZiTou; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for PinYinZiTou", value, value.ToString());
				_pinYinZiTou = value;
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
        [DataDesc(CName = "备注", ShortCode = "Comment", Desc = "备注", ContextType = SysDic.All, Length = 300)]
        public virtual string Comment
		{
			get { return _comment; }
			set
			{
				if ( value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for Comment", value, value.ToString());
				_comment = value;
			}
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
        [DataDesc(CName = "待还额度", ShortCode = "UnRepaymentAmount", Desc = "待还额度", ContextType = SysDic.All, Length = 8)]
        public virtual double UnRepaymentAmount
		{
			get { return _unRepaymentAmount; }
			set { _unRepaymentAmount = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "借款上限", ShortCode = "LoanUpperAmount", Desc = "借款上限", ContextType = SysDic.All, Length = 8)]
        public virtual double LoanUpperAmount
		{
			get { return _loanUpperAmount; }
			set { _loanUpperAmount = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "单笔借款上限", ShortCode = "OneceLoanUpperAmount", Desc = "单笔借款上限", ContextType = SysDic.All, Length = 8)]
        public virtual double OneceLoanUpperAmount
		{
			get { return _oneceLoanUpperAmount; }
			set { _oneceLoanUpperAmount = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "借款总额", ShortCode = "LoanAmount", Desc = "借款总额", ContextType = SysDic.All, Length = 8)]
        public virtual double LoanAmount
		{
			get { return _loanAmount; }
			set { _loanAmount = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "还款总额", ShortCode = "RepaymentAmount", Desc = "还款总额", ContextType = SysDic.All, Length = 8)]
        public virtual double RepaymentAmount
		{
			get { return _repaymentAmount; }
			set { _repaymentAmount = value; }
		}

		
		#endregion
	}
	#endregion
}