using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
	#region PReceive

	/// <summary>
	/// PReceive object for NHibernate mapped table 'P_Receive'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "商务收款", ClassCName = "PReceive", ShortCode = "PReceive", Desc = "商务收款")]
	public class PReceive : BaseEntity
	{
        #region Member Variables

        protected PFinanceReceive _pFinanceReceive;
        protected PReceivePlan _pReceivePlan;
        protected long? _pContractID;
        protected string _pContractName;
        protected long? _pClientID;
        protected string _pClientName;
        protected long? _payOrgID;
        protected string _payOrgName;
        protected long? _compnameID;
        protected string _componeName;
        protected double _receiveAmount;
        protected DateTime? _receiveDate;
        protected long? _receiveManID;
        protected string _receiveManName;
        protected string _receiveMemo;
        protected long? _inputerID;
        protected string _inputerName;
        protected string _editMemoBusiness;
        protected string _businessMemo;
        protected string _yearMonth;
        protected int _dispOrder;
        protected string _comment;
        protected bool _isUse;
        protected DateTime? _ContractSignDateTime;

        #endregion

        #region Constructors

        public PReceive() { }

		public PReceive( long labID, long pReceivePlanID, long pFReceivenID, long pContractID, string pContractName, long pClientID, string pClientName, long payOrgID, string payOrgName, long compnameID, string componeName, double receiveAmount, DateTime receiveDate, long receiveManID, string receiveManName, string receiveMemo, long inputerID, string inputerName, string editMemoBusiness, string businessMemo, string yearMonth, int dispOrder, string comment, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._pContractID = pContractID;
			this._pContractName = pContractName;
			this._pClientID = pClientID;
			this._pClientName = pClientName;
			this._payOrgID = payOrgID;
			this._payOrgName = payOrgName;
			this._compnameID = compnameID;
			this._componeName = componeName;
			this._receiveAmount = receiveAmount;
			this._receiveDate = receiveDate;
			this._receiveManID = receiveManID;
			this._receiveManName = receiveManName;
			this._receiveMemo = receiveMemo;
			this._inputerID = inputerID;
			this._inputerName = inputerName;
			this._editMemoBusiness = editMemoBusiness;
			this._businessMemo = businessMemo;
			this._yearMonth = yearMonth;
			this._dispOrder = dispOrder;
			this._comment = comment;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "商务收款ID", ShortCode = "PReceivePlanID", Desc = "商务收款ID", ContextType = SysDic.All, Length = 8)]
        public virtual PReceivePlan PReceivePlan
        {
			get { return _pReceivePlan; }
			set { _pReceivePlan = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "财务收款ID", ShortCode = "PFReceivenID", Desc = "财务收款ID", ContextType = SysDic.All, Length = 8)]
        public virtual PFinanceReceive PFinanceReceive
        {
			get { return _pFinanceReceive; }
			set { _pFinanceReceive = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "合同ID", ShortCode = "PContractID", Desc = "合同ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? PContractID
		{
			get { return _pContractID; }
			set { _pContractID = value; }
		}

        [DataMember]
        [DataDesc(CName = "合同名称", ShortCode = "PContractName", Desc = "合同名称", ContextType = SysDic.All, Length = 200)]
        public virtual string PContractName
		{
			get { return _pContractName; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for PContractName", value, value.ToString());
				_pContractName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "客户ID", ShortCode = "PClientID", Desc = "客户ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? PClientID
		{
			get { return _pClientID; }
			set { _pClientID = value; }
		}

        [DataMember]
        [DataDesc(CName = "客户名称", ShortCode = "PClientName", Desc = "客户名称", ContextType = SysDic.All, Length = 200)]
        public virtual string PClientName
		{
			get { return _pClientName; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for PClientName", value, value.ToString());
				_pClientName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "付款单位ID", ShortCode = "PayOrgID", Desc = "付款单位ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? PayOrgID
		{
			get { return _payOrgID; }
			set { _payOrgID = value; }
		}

        [DataMember]
        [DataDesc(CName = "付款单位名称", ShortCode = "PayOrgName", Desc = "付款单位名称", ContextType = SysDic.All, Length = 200)]
        public virtual string PayOrgName
		{
			get { return _payOrgName; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for PayOrgName", value, value.ToString());
				_payOrgName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "执行公司ID", ShortCode = "CompnameID", Desc = "执行公司ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? CompnameID
		{
			get { return _compnameID; }
			set { _compnameID = value; }
		}

        [DataMember]
        [DataDesc(CName = "本公司名称", ShortCode = "ComponeName", Desc = "本公司名称", ContextType = SysDic.All, Length = 200)]
        public virtual string ComponeName
		{
			get { return _componeName; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ComponeName", value, value.ToString());
				_componeName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "收款金额", ShortCode = "ReceiveAmount", Desc = "收款金额", ContextType = SysDic.All, Length = 8)]
        public virtual double ReceiveAmount
		{
			get { return _receiveAmount; }
			set { _receiveAmount = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "收款日期", ShortCode = "ReceiveDate", Desc = "收款日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ReceiveDate
		{
			get { return _receiveDate; }
			set { _receiveDate = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "收款负责人ID", ShortCode = "ReceiveManID", Desc = "收款负责人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReceiveManID
		{
			get { return _receiveManID; }
			set { _receiveManID = value; }
		}

        [DataMember]
        [DataDesc(CName = "收款负责人", ShortCode = "ReceiveManName", Desc = "收款负责人", ContextType = SysDic.All, Length = 200)]
        public virtual string ReceiveManName
		{
			get { return _receiveManName; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ReceiveManName", value, value.ToString());
				_receiveManName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "收款说明", ShortCode = "ReceiveMemo", Desc = "收款说明", ContextType = SysDic.All, Length = 200)]
        public virtual string ReceiveMemo
		{
			get { return _receiveMemo; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ReceiveMemo", value, value.ToString());
				_receiveMemo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "录入人ID", ShortCode = "InputerID", Desc = "录入人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? InputerID
		{
			get { return _inputerID; }
			set { _inputerID = value; }
		}

        [DataMember]
        [DataDesc(CName = "录入人姓名", ShortCode = "InputerName", Desc = "录入人姓名", ContextType = SysDic.All, Length = 200)]
        public virtual string InputerName
		{
			get { return _inputerName; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for InputerName", value, value.ToString());
				_inputerName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "修改说明（业务）", ShortCode = "EditMemoBusiness", Desc = "修改说明（业务）", ContextType = SysDic.All, Length = 200)]
        public virtual string EditMemoBusiness
		{
			get { return _editMemoBusiness; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for EditMemoBusiness", value, value.ToString());
				_editMemoBusiness = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "商务备注", ShortCode = "BusinessMemo", Desc = "商务备注", ContextType = SysDic.All, Length = 200)]
        public virtual string BusinessMemo
		{
			get { return _businessMemo; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for BusinessMemo", value, value.ToString());
				_businessMemo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "月份", ShortCode = "YearMonth", Desc = "月份", ContextType = SysDic.All, Length = 200)]
        public virtual string YearMonth
		{
			get { return _yearMonth; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for YearMonth", value, value.ToString());
				_yearMonth = value;
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
        [DataDesc(CName = "备注", ShortCode = "Comment", Desc = "备注", ContextType = SysDic.All, Length = -1)]
        public virtual string Comment
		{
			get { return _comment; }
			set
			{
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
        [DataDesc(CName = "合同签署时间", ShortCode = "ContractSignDateTime", Desc = "合同签署时间", ContextType = SysDic.All, Length = 1)]
        public virtual DateTime? ContractSignDateTime
        {
            get { return _ContractSignDateTime; }
            set { _ContractSignDateTime = value; }
        }

        #endregion

        #region
        /// <summary>
        /// 对此业务实体操作时的描述
        /// </summary>
        [DataMember]
        public virtual string OperationMemo { get; set; }
        
        #endregion
    }
    #endregion
}