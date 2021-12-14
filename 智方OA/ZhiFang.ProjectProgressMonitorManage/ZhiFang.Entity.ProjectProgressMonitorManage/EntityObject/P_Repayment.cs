using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ProjectProgressMonitorManage.ViewObject.Request;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
	#region PRepayment

	/// <summary>
	/// PRepayment object for NHibernate mapped table 'P_Repayment'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "还款记录", ClassCName = "PRepayment", ShortCode = "PRepayment", Desc = "还款记录")]
	public class PRepayment : BusinessBase
    {
		#region Member Variables
		
        protected string _pRepaymentNo;
        protected long? _pRepaymentTypeID;
        protected string _pRepaymentlTypeName;
        protected double _pRepaymentAmount;
        protected long? _pRepaymentContentTypeID;
        protected string _pRepaymentContentTypeName;
        protected string _pRepaymentMemo;
        protected long? _deptID;
        protected string _deptName;
        protected long _status;
        protected long? _applyManID;
        protected string _applyMan;
        protected DateTime? _applyDate;
        protected long? _reviewManID;
        protected string _reviewMan;
        protected DateTime? _reviewDate;
        protected string _reviewInfo;
        protected int _dispOrder;
        protected string _comment;
        protected bool _isUse;
		

		#endregion

		#region Constructors

		public PRepayment() { }

		public PRepayment( long labID, string pRepaymentNo, long pRepaymentTypeID, string pRepaymentlTypeName, double pRepaymentAmount, long pRepaymentContentTypeID, string pRepaymentContentTypeName, string pRepaymentMemo, long deptID, string deptName, long status, long applyManID, string applyMan, DateTime applyDate, long reviewManID, string reviewMan, DateTime reviewDate, string reviewInfo, int dispOrder, string comment, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._pRepaymentNo = pRepaymentNo;
			this._pRepaymentTypeID = pRepaymentTypeID;
			this._pRepaymentlTypeName = pRepaymentlTypeName;
			this._pRepaymentAmount = pRepaymentAmount;
			this._pRepaymentContentTypeID = pRepaymentContentTypeID;
			this._pRepaymentContentTypeName = pRepaymentContentTypeName;
			this._pRepaymentMemo = pRepaymentMemo;
			this._deptID = deptID;
			this._deptName = deptName;
			this._status = status;
			this._applyManID = applyManID;
			this._applyMan = applyMan;
			this._applyDate = applyDate;
			this._reviewManID = reviewManID;
			this._reviewMan = reviewMan;
			this._reviewDate = reviewDate;
			this._reviewInfo = reviewInfo;
			this._dispOrder = dispOrder;
			this._comment = comment;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "还款记录单号", ShortCode = "PRepaymentNo", Desc = "还款记录单号", ContextType = SysDic.All, Length = 200)]
        public virtual string PRepaymentNo
		{
			get { return _pRepaymentNo; }
			set
			{
				_pRepaymentNo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "还款记录类型ID", ShortCode = "PRepaymentTypeID", Desc = "还款记录类型ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? PRepaymentTypeID
		{
			get { return _pRepaymentTypeID; }
			set { _pRepaymentTypeID = value; }
		}

        [DataMember]
        [DataDesc(CName = "还款记录类型名称", ShortCode = "PRepaymentlTypeName", Desc = "还款记录类型名称", ContextType = SysDic.All, Length = 200)]
        public virtual string PRepaymentlTypeName
		{
			get { return _pRepaymentlTypeName; }
			set
			{
				_pRepaymentlTypeName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "还款记录金额", ShortCode = "PRepaymentAmount", Desc = "还款记录金额", ContextType = SysDic.All, Length = 8)]
        public virtual double PRepaymentAmount
		{
			get { return _pRepaymentAmount; }
			set { _pRepaymentAmount = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "还款记录内容类型ID", ShortCode = "PRepaymentContentTypeID", Desc = "还款记录内容类型ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? PRepaymentContentTypeID
		{
			get { return _pRepaymentContentTypeID; }
			set { _pRepaymentContentTypeID = value; }
		}

        [DataMember]
        [DataDesc(CName = "还款记录内容类型名称", ShortCode = "PRepaymentContentTypeName", Desc = "还款记录内容类型名称", ContextType = SysDic.All, Length = 200)]
        public virtual string PRepaymentContentTypeName
		{
			get { return _pRepaymentContentTypeName; }
			set
			{
                _pRepaymentContentTypeName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "还款记录说明", ShortCode = "PRepaymentMemo", Desc = "还款记录说明", ContextType = SysDic.All, Length = 200)]
        public virtual string PRepaymentMemo
		{
			get { return _pRepaymentMemo; }
			set
			{
				_pRepaymentMemo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "所属部门ID", ShortCode = "DeptID", Desc = "所属部门ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? DeptID
		{
			get { return _deptID; }
			set { _deptID = value; }
		}

        [DataMember]
        [DataDesc(CName = "所属部门名称", ShortCode = "DeptName", Desc = "所属部门名称", ContextType = SysDic.All, Length = 200)]
        public virtual string DeptName
		{
			get { return _deptName; }
			set
			{
				_deptName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "还款记录状态", ShortCode = "Status", Desc = "还款记录状态", ContextType = SysDic.All, Length = 8)]
        public virtual long Status
		{
			get { return _status; }
			set { _status = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "申请人ID", ShortCode = "ApplyManID", Desc = "申请人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ApplyManID
		{
			get { return _applyManID; }
			set { _applyManID = value; }
		}

        [DataMember]
        [DataDesc(CName = "申请人", ShortCode = "ApplyMan", Desc = "申请人", ContextType = SysDic.All, Length = 200)]
        public virtual string ApplyMan
		{
			get { return _applyMan; }
			set
			{
				_applyMan = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "申请时间", ShortCode = "ApplyDate", Desc = "申请时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ApplyDate
		{
			get { return _applyDate; }
			set { _applyDate = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "1审核人ID", ShortCode = "ReviewManID", Desc = "1审核人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReviewManID
		{
			get { return _reviewManID; }
			set { _reviewManID = value; }
		}

        [DataMember]
        [DataDesc(CName = "1审核人", ShortCode = "ReviewMan", Desc = "1审核人", ContextType = SysDic.All, Length = 200)]
        public virtual string ReviewMan
		{
			get { return _reviewMan; }
			set
			{
				_reviewMan = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "1审核时间", ShortCode = "ReviewDate", Desc = "1审核时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ReviewDate
		{
			get { return _reviewDate; }
			set { _reviewDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "1审核人意见", ShortCode = "ReviewInfo", Desc = "1审核人意见", ContextType = SysDic.All, Length = 500)]
        public virtual string ReviewInfo
		{
			get { return _reviewInfo; }
			set
			{
				_reviewInfo = value;
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

		
		#endregion
	}
	#endregion
}