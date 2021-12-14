using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
    #region ReaBmsOutDoc

    /// <summary>
    /// ReaBmsOutDoc object for NHibernate mapped table 'Rea_BmsOutDoc'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "出库总单表", ClassCName = "ReaBmsOutDoc", ShortCode = "ReaBmsOutDoc", Desc = "出库总单表")]
    public class ReaBmsOutDoc : BaseEntity
    {
        #region Member Variables

        protected long? _deptID;
        protected long _outType;
        protected int _status;
        protected DateTime? _operDate;
        protected int _printTimes;
        protected string _zX1;
        protected string _zX2;
        protected string _zX3;
        protected int _dispOrder;
        protected string _memo;
        protected bool? _visible;
        protected long? _createrID;
        protected string _createrName;
        protected DateTime _dataUpdateTime;
        protected string _deptName;
        protected string _outTypeName;
        protected string _statusName;
        protected long? _operateOutDocID;
        protected string _operateOutDocNo;
        protected string _outDocNo;
        protected double _totalPrice;
        protected long? _takerID;
        protected string _takerName;
        protected long? _checkDocID;
        protected long? _storageID;
        protected string _storageName;
        protected long? _outBoundID;
        protected string _outBoundName;
        protected long? _confirmId;
        protected string _confirmName;
        protected DateTime? _confirmTime;
        protected string _confirmMemo;

        protected bool _isHasCheck;
        protected long? _checkID;
        protected string _checkName;
        protected DateTime? _checkTime;
        protected string _checkMemo;
        protected bool _isHasApproval;
        protected long? _approvalId;
        protected string _approvalCName;
        protected DateTime? _approvalTime;
        protected string _approvalMemo;

        protected int _iOFlag;
        protected string _iOMemo;
<<<<<<< .mine
        protected bool _isNeedBOpen;
||||||| .r2673
=======

        protected string _labcName;
        protected string _reaServerLabcCode;
        protected long? _labOutDocID;
        protected int? _isThirdFlag;
        protected long? _SupplierConfirmID;
        protected string _SupplierConfirmName;
        protected DateTime? _SupplierConfirmTime;
        protected string _SupplierConfirmMemo;
>>>>>>> .r2783
        #endregion

        #region Constructors

        public ReaBmsOutDoc() { }

        public ReaBmsOutDoc(long labID, long deptID, long outType, int status, DateTime operDate, int printTimes, string zX1, string zX2, string zX3, int dispOrder, string memo, bool visible, long createrID, string createrName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, string deptName, string outTypeName, string statusName, long operateOutDocID, string operateOutDocNo, string outDocNo, double totalPrice, long takerID, string takerName, long checkID, string checkName, long checkDocID, string checkMemo, DateTime checkTime, long storageID, string storageName, long outBoundID, string outBoundName, bool isHasCheck, bool isHasApproval, long approvalId, string financeCheckName, DateTime approvalTime, string approvalMemo, int iOFlag, string iOMemo)
        {
            this._labID = labID;
            this._deptID = deptID;
            this._outType = outType;
            this._status = status;
            this._operDate = operDate;
            this._printTimes = printTimes;
            this._zX1 = zX1;
            this._zX2 = zX2;
            this._zX3 = zX3;
            this._dispOrder = dispOrder;
            this._memo = memo;
            this._visible = visible;
            this._createrID = createrID;
            this._createrName = createrName;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._deptName = deptName;
            this._outTypeName = outTypeName;
            this._statusName = statusName;
            this._operateOutDocID = operateOutDocID;
            this._operateOutDocNo = operateOutDocNo;
            this._outDocNo = outDocNo;
            this._totalPrice = totalPrice;
            this._takerID = takerID;
            this._takerName = takerName;
            this._checkID = checkID;
            this._checkName = checkName;
            this._checkDocID = checkDocID;
            this._checkMemo = checkMemo;
            this._checkTime = checkTime;
            this._storageID = storageID;
            this._storageName = storageName;
            this._outBoundID = outBoundID;
            this._outBoundName = outBoundName;
            this._isHasCheck = isHasCheck;
            this._isHasApproval = isHasApproval;
            this._approvalId = approvalId;
            this._approvalCName = financeCheckName;
            this._approvalTime = approvalTime;
            this._approvalMemo = approvalMemo;
            this._iOFlag = iOFlag;
            this._iOMemo = iOMemo;
        }

        #endregion

        #region Public Properties
        [DataMember]
        [DataDesc(CName = "是否需要开瓶管理", ShortCode = "IsNeedBOpen", Desc = "是否需要开瓶管理", ContextType = SysDic.All, Length = 4)]
        public virtual bool IsNeedBOpen
        {
            get { return _isNeedBOpen; }
            set { _isNeedBOpen = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "出库人ID", ShortCode = "OutBoundID", Desc = "出库人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? OutBoundID
        {
            get { return _outBoundID; }
            set { _outBoundID = value; }
        }

        [DataMember]
        [DataDesc(CName = "出库人姓名", ShortCode = "OutBoundName", Desc = "出库人姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string OutBoundName
        {
            get { return _outBoundName; }
            set
            {
                _outBoundName = value;
            }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "库房ID", ShortCode = "StorageID", Desc = "库房ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? StorageID
        {
            get { return _storageID; }
            set { _storageID = value; }
        }

        [DataMember]
        [DataDesc(CName = "库房名称", ShortCode = "StorageName", Desc = "库房库名称", ContextType = SysDic.All, Length = 50)]
        public virtual string StorageName
        {
            get { return _storageName; }
            set
            {
                _storageName = value;
            }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "使用部门ID", ShortCode = "DeptID", Desc = "使用部门ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? DeptID
        {
            get { return _deptID; }
            set { _deptID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "出库类型", ShortCode = "OutType", Desc = "出库类型", ContextType = SysDic.All, Length = 8)]
        public virtual long OutType
        {
            get { return _outType; }
            set { _outType = value; }
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
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ZX1", value, value.ToString());
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
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ZX2", value, value.ToString());
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
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ZX3", value, value.ToString());
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "是否使用", ShortCode = "Visible", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool? Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "创建者ID", ShortCode = "CreaterID", Desc = "创建者ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? CreaterID
        {
            get { return _createrID; }
            set { _createrID = value; }
        }

        [DataMember]
        [DataDesc(CName = "创建者姓名", ShortCode = "CreaterName", Desc = "创建者姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string CreaterName
        {
            get { return _createrName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for CreaterName", value, value.ToString());
                _createrName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "更新时间", ShortCode = "DataUpdateTime", Desc = "更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DeptName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string DeptName
        {
            get { return _deptName; }
            set
            {
                _deptName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OutTypeName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string OutTypeName
        {
            get { return _outTypeName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for OutTypeName", value, value.ToString());
                _outTypeName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "StatusName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string StatusName
        {
            get { return _statusName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for StatusName", value, value.ToString());
                _statusName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OperateOutDocID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? OperateOutDocID
        {
            get { return _operateOutDocID; }
            set { _operateOutDocID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OperateOutDocNo", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string OperateOutDocNo
        {
            get { return _operateOutDocNo; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for OperateOutDocNo", value, value.ToString());
                _operateOutDocNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OutDocNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string OutDocNo
        {
            get { return _outDocNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for OutDocNo", value, value.ToString());
                _outDocNo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "TotalPrice", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double TotalPrice
        {
            get { return _totalPrice; }
            set { _totalPrice = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "盘库单ID", ShortCode = "CheckDocID", Desc = "盘库单ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? CheckDocID
        {
            get { return _checkDocID; }
            set { _checkDocID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "TakerID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? TakerID
        {
            get { return _takerID; }
            set { _takerID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TakerName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string TakerName
        {
            get { return _takerName; }
            set
            {
                _takerName = value;
            }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "出库确认人Id", ShortCode = "ConfirmId", Desc = "出库确认人Id", ContextType = SysDic.All, Length = 8)]
        public virtual long? ConfirmId
        {
            get { return _confirmId; }
            set { _confirmId = value; }
        }

        [DataMember]
        [DataDesc(CName = "出库确认人", ShortCode = "ConfirmName", Desc = "出库确认人", ContextType = SysDic.All, Length = 50)]
        public virtual string ConfirmName
        {
            get { return _confirmName; }
            set
            {
                _confirmName = value;
            }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "出库确认时间", ShortCode = "ConfirmTime", Desc = "出库确认时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ConfirmTime
        {
            get { return _confirmTime; }
            set { _confirmTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "出库确认意见", ShortCode = "ConfirmMemo", Desc = "出库确认意见", ContextType = SysDic.All, Length = 500)]
        public virtual string ConfirmMemo
        {
            get { return _confirmMemo; }
            set { _confirmMemo = value; }
        }
        [DataMember]
        [DataDesc(CName = "是否需要审核", ShortCode = "IsHasCheck", Desc = "是否需要审核", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsHasCheck
        {
            get { return _isHasCheck; }
            set { _isHasCheck = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审核人Id", ShortCode = "CheckID", Desc = "审核人Id", ContextType = SysDic.All, Length = 8)]
        public virtual long? CheckID
        {
            get { return _checkID; }
            set { _checkID = value; }
        }

        [DataMember]
        [DataDesc(CName = "审核人", ShortCode = "CheckName", Desc = "审核人", ContextType = SysDic.All, Length = 50)]
        public virtual string CheckName
        {
            get { return _checkName; }
            set
            {
                _checkName = value;
            }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审核时间", ShortCode = "CheckTime", Desc = "审核时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? CheckTime
        {
            get { return _checkTime; }
            set { _checkTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审核意见", ShortCode = "CheckMemo", Desc = "审核意见", ContextType = SysDic.All, Length = 500)]
        public virtual string CheckMemo
        {
            get { return _checkMemo; }
            set { _checkMemo = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否需要审批", ShortCode = "IsHasApproval", Desc = "是否需要审批", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsHasApproval
        {
            get { return _isHasApproval; }
            set { _isHasApproval = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审批人Id", ShortCode = "ApprovalId", Desc = "审批人Id", ContextType = SysDic.All, Length = 8)]
        public virtual long? ApprovalId
        {
            get { return _approvalId; }
            set { _approvalId = value; }
        }

        [DataMember]
        [DataDesc(CName = "审批人名称", ShortCode = "ApprovalCName", Desc = "审批人名称", ContextType = SysDic.All, Length = 50)]
        public virtual string ApprovalCName
        {
            get { return _approvalCName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ApprovalCName", value, value.ToString());
                _approvalCName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "财审批时间", ShortCode = "ApprovalTime", Desc = "审批时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ApprovalTime
        {
            get { return _approvalTime; }
            set { _approvalTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "审批备注", ShortCode = "ApprovalMemo", Desc = "审批备注", ContextType = SysDic.All, Length = 500)]
        public virtual string ApprovalMemo
        {
            get { return _approvalMemo; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for ApprovalMemo", value, value.ToString());
                _approvalMemo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "接口数据标志", ShortCode = "IOFlag", Desc = "接口数据标志", ContextType = SysDic.All, Length = 4)]
        public virtual int IOFlag
        {
            get { return _iOFlag; }
            set { _iOFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "接口结果备注", ShortCode = "IOMemo", Desc = "接口结果备注", ContextType = SysDic.All, Length = Int32.MaxValue)]
        public virtual string IOMemo
        {
            get { return _iOMemo; }
            set
            {
                _iOMemo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "实验室名称", ShortCode = "LabcName", Desc = "实验室名称", ContextType = SysDic.All, Length = 100)]
        public virtual string LabcName
        {
            get { return _labcName; }
            set
            {
                _labcName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "实验室平台编码", ShortCode = "ReaServerLabcCode", Desc = "实验室平台编码", ContextType = SysDic.All, Length = 100)]
        public virtual string ReaServerLabcCode
        {
            get { return _reaServerLabcCode; }
            set
            {
                _reaServerLabcCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "实验室出库总单ID", ShortCode = "LabOutDocID", Desc = "实验室出库总单ID", ContextType = SysDic.All, Length = 4)]
        public virtual long? LabOutDocID
        {
            get { return _labOutDocID; }
            set
            {
                _labOutDocID = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "数据写入第三方系统标志", ShortCode = "IsThirdFlag", Desc = "数据写入第三方系统标志", ContextType = SysDic.All, Length = 4)]
        public virtual int? IsThirdFlag
        {
            get { return _isThirdFlag; }
            set { _isThirdFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "供应商确认人编号", ShortCode = "SupplierConfirmID", Desc = "供应商确认人编号", ContextType = SysDic.All, Length = 4)]
        public virtual long? SupplierConfirmID
        {
            get { return _SupplierConfirmID; }
            set
            {
                _SupplierConfirmID = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "供应商确认人姓名", ShortCode = "SupplierConfirmName", Desc = "供应商确认人姓名", ContextType = SysDic.All, Length = 100)]
        public virtual string SupplierConfirmName
        {
            get { return _SupplierConfirmName; }
            set
            {
                _SupplierConfirmName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "供应商确认时间", ShortCode = "SupplierConfirmTime", Desc = "供应商确认时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? SupplierConfirmTime
        {
            get { return _SupplierConfirmTime; }
            set { _SupplierConfirmTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "供应商确认备注", ShortCode = "SupplierConfirmMemo", Desc = "供应商确认备注", ContextType = SysDic.All, Length = 500)]
        public virtual string SupplierConfirmMemo
        {
            get { return _SupplierConfirmMemo; }
            set
            {
                _SupplierConfirmMemo = value;
            }
        }
        #endregion

        #region 自定义属性及统计

        [DataMember]
        [DataDesc(CName = "机构", ShortCode = "ReaCenOrg", Desc = "机构")]
        public virtual ReaCenOrg ReaCenOrg { get; set; }

        public ReaBmsOutDoc(ReaBmsOutDoc reaBmsOutDoc, ReaCenOrg reaCenOrg)
        {
            this.ReaCenOrg = reaCenOrg;
            this._id = reaBmsOutDoc.Id;
            this._labID = reaBmsOutDoc.LabID;
            this._deptID = reaBmsOutDoc.DeptID;
            this._outType = reaBmsOutDoc.OutType;
            this._status = reaBmsOutDoc.Status;
            this._operDate = reaBmsOutDoc.OperDate;
            this._printTimes = reaBmsOutDoc.PrintTimes;
            this._zX1 = reaBmsOutDoc.ZX1;
            this._zX2 = reaBmsOutDoc.ZX2;
            this._zX3 = reaBmsOutDoc.ZX3;
            this._dispOrder = reaBmsOutDoc.DispOrder;
            this._memo = reaBmsOutDoc.Memo;
            this._visible = reaBmsOutDoc.Visible;
            this._createrID = reaBmsOutDoc.CreaterID;
            this._createrName = reaBmsOutDoc.CreaterName;
            this._dataAddTime = reaBmsOutDoc.DataAddTime;
            this._dataUpdateTime = reaBmsOutDoc.DataUpdateTime;
            this._dataTimeStamp = reaBmsOutDoc.DataTimeStamp;
            this._deptName = reaBmsOutDoc.DeptName;
            this._outTypeName = reaBmsOutDoc.OutTypeName;
            this._statusName = reaBmsOutDoc.StatusName;
            this._operateOutDocID = reaBmsOutDoc.OperateOutDocID;
            this._operateOutDocNo = reaBmsOutDoc.OperateOutDocNo;
            this._outDocNo = reaBmsOutDoc.OutDocNo;
            this._totalPrice = reaBmsOutDoc.TotalPrice;
            this._takerID = reaBmsOutDoc.TakerID;
            this._takerName = reaBmsOutDoc.TakerName;
            this._checkID = reaBmsOutDoc.CheckID;
            this._checkName = reaBmsOutDoc.CheckName;
            this._checkDocID = reaBmsOutDoc.CheckDocID;
            this._checkMemo = reaBmsOutDoc.CheckMemo;
            this._checkTime = reaBmsOutDoc.CheckTime;
            this._storageID = reaBmsOutDoc.StorageID;
            this._storageName = reaBmsOutDoc.StorageName;
            this._outBoundID = reaBmsOutDoc.OutBoundID;
            this._outBoundName = reaBmsOutDoc.OutBoundName;
            this._isHasCheck = reaBmsOutDoc.IsHasCheck;
            this._isHasApproval = reaBmsOutDoc.IsHasApproval;
            this._approvalId = reaBmsOutDoc.ApprovalId;
            this._approvalCName = reaBmsOutDoc.ApprovalCName;
            this._approvalTime = reaBmsOutDoc.ApprovalTime;
            this._approvalMemo = reaBmsOutDoc.ApprovalMemo;
            this._iOFlag = reaBmsOutDoc.IOFlag;
            this._iOMemo = reaBmsOutDoc.IOMemo;
            this._labcName = reaBmsOutDoc.LabcName;
            this._reaServerLabcCode = reaBmsOutDoc.ReaServerLabcCode;
            this._labOutDocID = reaBmsOutDoc.LabOutDocID;
            this.IsThirdFlag = reaBmsOutDoc.IsThirdFlag;
        }

        #endregion
    }
    #endregion
}