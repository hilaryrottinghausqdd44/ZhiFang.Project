using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity.ReagentSys
{
	#region ReaBmsReqDoc

	/// <summary>
	/// ReaBmsReqDoc object for NHibernate mapped table 'Rea_BmsReqDoc'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "申请总单表", ClassCName = "ReaBmsReqDoc", ShortCode = "ReaBmsReqDoc", Desc = "申请总单表")]
    public class ReaBmsReqDoc : BaseEntity
    {
        #region Member Variables

        protected string _reqDocNo;
        protected long? _deptID;
        protected int _urgentFlag;
        protected int _status;
        protected DateTime? _operDate;
        protected int _printTimes;
        protected string _zX1;
        protected string _zX2;
        protected string _zX3;
        protected int _dispOrder;
        protected string _memo;
        protected bool _visible;
        protected long? _applyID;
        protected string _applyName;
        protected DateTime? _applyTime;
        protected long? _reviewManID;
        protected string _reviewManName;
        protected DateTime? _reviewTime;
        protected DateTime? _dataUpdateTime;
        protected string _deptName;
        protected IList<ReaBmsReqDtl> _reaBmsReqDtlList;
        protected string _ReviewMemo;


        #endregion

        #region Constructors

        public ReaBmsReqDoc() { }

        public ReaBmsReqDoc(long labID, string reqDocNo, long deptID, int urgentFlag, int status, DateTime operDate, int printTimes, string zX1, string zX2, string zX3, int dispOrder, string memo, bool visible, long applyID, string applyName, DateTime applyTime, long reviewManID, string reviewManName, DateTime reviewTime, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, string deptName)
        {
            this._labID = labID;
            this._reqDocNo = reqDocNo;
            this._deptID = deptID;
            this._urgentFlag = urgentFlag;
            this._status = status;
            this._operDate = operDate;
            this._printTimes = printTimes;
            this._zX1 = zX1;
            this._zX2 = zX2;
            this._zX3 = zX3;
            this._dispOrder = dispOrder;
            this._memo = memo;
            this._visible = visible;
            this._applyID = applyID;
            this._applyName = applyName;
            this._applyTime = applyTime;
            this._reviewManID = reviewManID;
            this._reviewManName = reviewManName;
            this._reviewTime = reviewTime;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._deptName = deptName;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "申请总单号", ShortCode = "ReqDocNo", Desc = "申请总单号", ContextType = SysDic.All, Length = 20)]
        public virtual string ReqDocNo
        {
            get { return _reqDocNo; }
            set
            {
                _reqDocNo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "部门ID", ShortCode = "DeptID", Desc = "部门ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? DeptID
        {
            get { return _deptID; }
            set { _deptID = value; }
        }

        [DataMember]
        [DataDesc(CName = "紧急标志", ShortCode = "UrgentFlag", Desc = "紧急标志", ContextType = SysDic.All, Length = 4)]
        public virtual int UrgentFlag
        {
            get { return _urgentFlag; }
            set { _urgentFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "单据状态", ShortCode = "Status", Desc = "单据状态", ContextType = SysDic.All, Length = 4)]
        public virtual int Status
        {
            get { return _status; }
            set { _status = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "操作日期", ShortCode = "OperDate", Desc = "操作日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? OperDate
        {
            get { return _operDate; }
            set { _operDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "打印次数", ShortCode = "PrintTimes", Desc = "打印次数", ContextType = SysDic.All, Length = 4)]
        public virtual int PrintTimes
        {
            get { return _printTimes; }
            set { _printTimes = value; }
        }

        [DataMember]
        [DataDesc(CName = "专项1", ShortCode = "ZX1", Desc = "专项1", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX1
        {
            get { return _zX1; }
            set
            {
                _zX1 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "专项2", ShortCode = "ZX2", Desc = "专项2", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX2
        {
            get { return _zX2; }
            set
            {
                _zX2 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "专项3", ShortCode = "ZX3", Desc = "专项3", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX3
        {
            get { return _zX3; }
            set
            {
                _zX3 = value;
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
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = -1)]
        public virtual string Memo
        {
            get { return _memo; }
            set
            {
                _memo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "Visible", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
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
                _applyName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "申请时间", ShortCode = "ApplyTime", Desc = "申请时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ApplyTime
        {
            get { return _applyTime; }
            set { _applyTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ReviewManID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReviewManID
        {
            get { return _reviewManID; }
            set { _reviewManID = value; }
        }

        [DataMember]
        [DataDesc(CName = "审核人ID", ShortCode = "ReviewManName", Desc = "审核人ID", ContextType = SysDic.All, Length = 50)]
        public virtual string ReviewManName
        {
            get { return _reviewManName; }
            set
            {
                _reviewManName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审核人名称", ShortCode = "ReviewTime", Desc = "审核人名称", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ReviewTime
        {
            get { return _reviewTime; }
            set { _reviewTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审核时间", ShortCode = "DataUpdateTime", Desc = "审核时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "部门名称", ShortCode = "DeptName", Desc = "部门名称", ContextType = SysDic.All, Length = 20)]
        public virtual string DeptName
        {
            get { return _deptName; }
            set
            {
                _deptName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "申请明细表", ShortCode = "ReaBmsReqDtlList", Desc = "申请明细表")]
        public virtual IList<ReaBmsReqDtl> ReaBmsReqDtlList
        {
            get
            {
                if (_reaBmsReqDtlList == null)
                {
                    _reaBmsReqDtlList = new List<ReaBmsReqDtl>();
                }
                return _reaBmsReqDtlList;
            }
            set { _reaBmsReqDtlList = value; }
        }

        [DataMember]
        [DataDesc(CName = "审核意见", ShortCode = "ReviewMemo", Desc = "审核意见")]
        public virtual string ReviewMemo
        {
            get
            {
                return _ReviewMemo;
            }
            set { _ReviewMemo = value; }
        }

        #endregion
    }
    #endregion
}