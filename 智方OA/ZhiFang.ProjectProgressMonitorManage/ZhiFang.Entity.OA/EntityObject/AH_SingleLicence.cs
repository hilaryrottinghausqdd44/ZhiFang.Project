using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.OA.ViewObject.Request;

namespace ZhiFang.Entity.OA
{
	#region AHSingleLicence

	/// <summary>
	/// AHSingleLicence object for NHibernate mapped table 'AH_SingleLicence'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "单站点授权授权流程Status：1暂存、2申请、3授权中、4商务授权通过、5商务授权退回、6特批授权中、7特批授权通过、8特批授权退回、9授权完成", ClassCName = "AHSingleLicence", ShortCode = "AHSingleLicence", Desc = "单站点授权授权流程Status：1暂存、2申请、3授权中、4商务授权通过、5商务授权退回、6特批授权中、7特批授权通过、8特批授权退回、9授权完成")]
	public class AHSingleLicence : AHLicence
    {
		#region Member Variables
		
        protected long? _pClientID;
        protected string _pClientName;
        protected long? _pContractID;
        protected long? _programID;
        protected string _programName;
        protected long? _equipID;
        protected string _equipName;
        protected long? _licenceTypeId;
        protected string _sQH;
        protected string _licenceKey;
        protected string _macAddress;
        protected DateTime? _startDate;
        protected DateTime? _endDate;
        protected long _status;
        protected long? _applyID;
        protected string _applyName;
        protected DateTime? _applyDataTime;
        protected string _applyInfo;
        protected long? _oneAuditID;
        protected string _oneAuditName;
        protected DateTime? _oneAuditDataTime;
        protected string _oneAuditInfo;
        protected long? _twoAuditID;
        protected string _twoAuditName;
        protected DateTime? _twoAuditDataTime;
        protected string _twoAuditInfo;
        protected DateTime? _genDateTime;
        protected string _comment;
        protected bool _isUse;
        protected bool _isCharLicenceByMAC;
        protected DateTime? _plannReceiptDate;

        #endregion

        #region Constructors

        public AHSingleLicence() { }

		public AHSingleLicence( long labID, long? pClientID, string pClientName, long? pContractID, long? programID, string programName, long? equipID, string equipName, long? licenceTypeId, string sQH, string licenceKey, string macAddress, DateTime startDate, DateTime endDate, long status, long? applyID, string applyName, DateTime applyDataTime, string applyInfo, long oneAuditID, string oneAuditName, DateTime oneAuditDataTime, string oneAuditInfo, long? twoAuditID, string twoAuditName, DateTime twoAuditDataTime, string twoAuditInfo, DateTime genDateTime, string comment, bool isUse, DateTime dataAddTime, bool isCharLicenceByMAC, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._pClientID = pClientID;
			this._pClientName = pClientName;
			this._pContractID = pContractID;
			this._programID = programID;
			this._programName = programName;
			this._equipID = equipID;
			this._equipName = equipName;
			this._licenceTypeId = licenceTypeId;
			this._sQH = sQH;
			this._licenceKey = licenceKey;
			this._macAddress = macAddress;
			this._startDate = startDate;
			this._endDate = endDate;
			this._status = status;
			this._applyID = applyID;
			this._applyName = applyName;
			this._applyDataTime = applyDataTime;
			this._applyInfo = applyInfo;
			this._oneAuditID = oneAuditID;
			this._oneAuditName = oneAuditName;
			this._oneAuditDataTime = oneAuditDataTime;
			this._oneAuditInfo = oneAuditInfo;
			this._twoAuditID = twoAuditID;
			this._twoAuditName = twoAuditName;
			this._twoAuditDataTime = twoAuditDataTime;
			this._twoAuditInfo = twoAuditInfo;
			this._genDateTime = genDateTime;
			this._comment = comment;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
            this._isCharLicenceByMAC =isCharLicenceByMAC;
            this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "客户ID", ShortCode = "PClientID", Desc = "客户ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? PClientID
		{
			get { return _pClientID; }
			set { _pClientID = value; }
		}

        [DataMember]
        [DataDesc(CName = "客户名称", ShortCode = "PClientName", Desc = "客户名称", ContextType = SysDic.All, Length = 100)]
        public virtual string PClientName
		{
			get { return _pClientName; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for PClientName", value, value.ToString());
				_pClientName = value;
			}
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "程序主键ID", ShortCode = "ProgramID", Desc = "程序主键ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ProgramID
		{
			get { return _programID; }
			set { _programID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ProgramName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ProgramName
		{
			get { return _programName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ProgramName", value, value.ToString());
				_programName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "仪器ID", ShortCode = "EquipID", Desc = "仪器ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? EquipID
		{
			get { return _equipID; }
			set { _equipID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EquipName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string EquipName
		{
			get { return _equipName; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for EquipName", value, value.ToString());
				_equipName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "LicenceTypeId", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? LicenceTypeId
		{
			get { return _licenceTypeId; }
			set { _licenceTypeId = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SQH", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string SQH
		{
			get { return _sQH; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for SQH", value, value.ToString());
				_sQH = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LicenceKey", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string LicenceKey
		{
			get { return _licenceKey; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for LicenceKey", value, value.ToString());
				_licenceKey = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "MacAddress", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string MacAddress
		{
			get { return _macAddress; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for MacAddress", value, value.ToString());
				_macAddress = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "StartDate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? StartDate
		{
			get { return _startDate; }
			set { _startDate = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "EndDate", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? EndDate
		{
			get { return _endDate; }
			set { _endDate = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Status", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long Status
		{
			get { return _status; }
			set { _status = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "申请人ID", ShortCode = "ApplyID", Desc = "申请人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ApplyID
		{
			get { return _applyID; }
			set { _applyID = value; }
		}

        [DataMember]
        [DataDesc(CName = "申请人名称", ShortCode = "ApplyName", Desc = "申请人名称", ContextType = SysDic.All, Length = 50)]
        public virtual string ApplyName
		{
			get { return _applyName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ApplyName", value, value.ToString());
				_applyName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ApplyDataTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ApplyDataTime
		{
			get { return _applyDataTime; }
			set { _applyDataTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "申请备注", ShortCode = "ApplyInfo", Desc = "申请备注", ContextType = SysDic.All, Length = 300)]
        public virtual string ApplyInfo
		{
			get { return _applyInfo; }
			set
			{
				if ( value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for ApplyInfo", value, value.ToString());
				_applyInfo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "一审人ID", ShortCode = "OneAuditID", Desc = "一审人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? OneAuditID
		{
			get { return _oneAuditID; }
			set { _oneAuditID = value; }
		}

        [DataMember]
        [DataDesc(CName = "一审人名称", ShortCode = "OneAuditName", Desc = "一审人名称", ContextType = SysDic.All, Length = 50)]
        public virtual string OneAuditName
		{
			get { return _oneAuditName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for OneAuditName", value, value.ToString());
				_oneAuditName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "一审时间", ShortCode = "OneAuditDataTime", Desc = "一审时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? OneAuditDataTime
		{
			get { return _oneAuditDataTime; }
			set { _oneAuditDataTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "一审意见", ShortCode = "OneAuditInfo", Desc = "一审意见", ContextType = SysDic.All, Length = 300)]
        public virtual string OneAuditInfo
		{
			get { return _oneAuditInfo; }
			set
			{
				if ( value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for OneAuditInfo", value, value.ToString());
				_oneAuditInfo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "特殊审批人ID", ShortCode = "TwoAuditID", Desc = "特殊审批人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? TwoAuditID
		{
			get { return _twoAuditID; }
			set { _twoAuditID = value; }
		}

        [DataMember]
        [DataDesc(CName = "特殊审批人名称", ShortCode = "TwoAuditName", Desc = "特殊审批人名称", ContextType = SysDic.All, Length = 50)]
        public virtual string TwoAuditName
		{
			get { return _twoAuditName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for TwoAuditName", value, value.ToString());
				_twoAuditName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "特殊审批时间", ShortCode = "TwoAuditDataTime", Desc = "特殊审批时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? TwoAuditDataTime
		{
			get { return _twoAuditDataTime; }
			set { _twoAuditDataTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "特殊审批意见", ShortCode = "TwoAuditInfo", Desc = "特殊审批意见", ContextType = SysDic.All, Length = 300)]
        public virtual string TwoAuditInfo
		{
			get { return _twoAuditInfo; }
			set
			{
				if ( value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for TwoAuditInfo", value, value.ToString());
				_twoAuditInfo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "授权时间", ShortCode = "GenDateTime", Desc = "授权时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? GenDateTime
		{
			get { return _genDateTime; }
			set { _genDateTime = value; }
		}
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "计划收款时间", ShortCode = "PlannReceiptDate", Desc = "计划收款时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? PlannReceiptDate
        {
            get { return _plannReceiptDate; }
            set { _plannReceiptDate = value; }
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
        [DataDesc(CName = "是否是字符串授权码", ShortCode = "IsCharLicenceByMAC", Desc = "是否是字符串授权码", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsCharLicenceByMAC
        {
            get { return _isCharLicenceByMAC; }
            set { _isCharLicenceByMAC = value; }
        }
        [DataMember]
        public virtual string LicenceClientName { get; set; }
        [DataMember]
        public virtual string LicenceCode { get; set; }

        #endregion
    }
	#endregion
}