using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
    #region ReaBmsCenSaleDocConfirm

    /// <summary>
    /// BmsCenSaleDocConfirm object for NHibernate mapped table 'BmsCenSaleDocConfirm'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "供货验收单表", ClassCName = "ReaBmsCenSaleDocConfirm", ShortCode = "ReaBmsCenSaleDocConfirm", Desc = "供货验收单表")]
    public class ReaBmsCenSaleDocConfirm : BaseEntity
    {
        #region Member Variables

        protected string _saleDocConfirmNo;
        protected string _saleDocNo;
        protected string _OrderDocNo;
        protected string _labName;
        protected string _companyName;
        protected int _status;
        protected string _statusName;
        protected long? _accepterID;
        protected string _accepterName;
        protected DateTime? _acceptTime;
        protected long? _secAccepterID;
        protected string _secAccepterName;
        protected DateTime? _secAcceptTime;
        protected string _acceptMemo;
        protected bool _isAcceptError;
        protected int _printTimes;
        protected string _memo;
        protected int _dispOrder;
        protected int _deleteFlag;
        protected DateTime? _dataUpdateTime;
        //protected IList<ReaBmsCenSaleDtlConfirm> _reaBmsCenSaleDtlConfirmList;
        protected long? _reaCompID;
        protected string _reaCompName;
        protected long? _sourceType;
        protected string _invoiceNo;

        protected long? _saleDocID;
        protected long? _labcID;
        protected string _labcName;
        protected long? _compID;
        protected string _reaCompanyName;
        protected string _reaServerCompCode;
        protected long? _orderDocID;
        protected long? _orgID;
        protected string _orderDocNo;
        protected string _reaServerLabcCode;
        protected string _reaCompCode;
        //protected string _reaLabcCode;
        protected double? _totalPrice;
        protected string _otherDocNo;
        protected string _zX1;
        protected string _zX2;
        protected string _zX3;

        protected string _transportNo;
        #endregion

        #region Constructors

        public ReaBmsCenSaleDocConfirm() { }

        public ReaBmsCenSaleDocConfirm(string saleDocConfirmNo, string saleDocNo, string labName, string companyName, int status, string statusName, long accepterID, string accepterName, DateTime acceptTime, long secAccepterID, string secAccepterName, DateTime secAcceptTime, string acceptMemo, bool isAcceptError, int printTimes, string memo, int dispOrder, int deleteFlag, DateTime dataUpdateTime, DateTime dataAddTime)
        {
            this._saleDocConfirmNo = saleDocConfirmNo;
            this._saleDocNo = saleDocNo;
            this._labName = labName;
            this._companyName = companyName;
            this._status = status;
            this._statusName = statusName;
            this._accepterID = accepterID;
            this._accepterName = accepterName;
            this._acceptTime = acceptTime;
            this._secAccepterID = secAccepterID;
            this._secAccepterName = secAccepterName;
            this._secAcceptTime = secAcceptTime;
            this._acceptMemo = acceptMemo;
            this._isAcceptError = isAcceptError;
            this._printTimes = printTimes;
            this._memo = memo;
            this._dispOrder = dispOrder;
            this._deleteFlag = deleteFlag;
            this._dataUpdateTime = dataUpdateTime;
            this._dataAddTime = dataAddTime;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "TotalPrice", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double? TotalPrice
        {
            get { return _totalPrice; }
            set { _totalPrice = value; }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReaServerLabcCode", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string ReaServerLabcCode
        {
            get { return _reaServerLabcCode; }
            set
            {
                _reaServerLabcCode = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReaCompCode", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string ReaCompCode
        {
            get { return _reaCompCode; }
            set
            {
                _reaCompCode = value;
            }
        }
        //[DataMember]
        //[DataDesc(CName = "", ShortCode = "ReaLabcCode", Desc = "", ContextType = SysDic.All, Length = 200)]
        //public virtual string ReaLabcCode
        //{
        //    get { return _reaLabcCode; }
        //    set
        //    {
        //        _reaLabcCode = value;
        //    }
        //}
        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReaCompanyName", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string ReaCompanyName
        {
            get { return _reaCompanyName; }
            set
            {
                _reaCompanyName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "LabcID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? LabcID
        {
            get { return _labcID; }
            set { _labcID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LabcName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string LabcName
        {
            get { return _labcName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for LabcName", value, value.ToString());
                _labcName = value;
            }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CompID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? CompID
        {
            get { return _compID; }
            set { _compID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CompanyName", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string CompanyName
        {
            get { return _companyName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for CompanyName", value, value.ToString());
                _companyName = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReaServerCompCode", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string ReaServerCompCode
        {
            get { return _reaServerCompCode; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ReaServerCompCode", value, value.ToString());
                _reaServerCompCode = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OrderDocID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? OrderDocID
        {
            get { return _orderDocID; }
            set { _orderDocID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OrgID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? OrgID
        {
            get { return _orgID; }
            set { _orgID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OrderDocNo", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string OrderDocNo
        {
            get { return _orderDocNo; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for OrderDocNo", value, value.ToString());
                _orderDocNo = value;
            }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "供货总单ID", ShortCode = "SaleDocID", Desc = "供货总单ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? SaleDocID
        {
            get { return _saleDocID; }
            set { _saleDocID = value; }
        }

        [DataMember]
        [DataDesc(CName = "供货验收单号", ShortCode = "SaleDocConfirmNo", Desc = "供货验收单号", ContextType = SysDic.All, Length = 50)]
        public virtual string SaleDocConfirmNo
        {
            get { return _saleDocConfirmNo; }
            set { _saleDocConfirmNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "供货单号", ShortCode = "SaleDocNo", Desc = "供货单号", ContextType = SysDic.All, Length = 50)]
        public virtual string SaleDocNo
        {
            get { return _saleDocNo; }
            set { _saleDocNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "实验室名称", ShortCode = "LabName", Desc = "实验室名称", ContextType = SysDic.All, Length = 100)]
        public virtual string LabName
        {
            get { return _labName; }
            set { _labName = value; }
        }

        [DataMember]
        [DataDesc(CName = "单据状态", ShortCode = "Status", Desc = "单据状态", ContextType = SysDic.All, Length = 4)]
        public virtual int Status
        {
            get { return _status; }
            set { _status = value; }
        }

        [DataMember]
        [DataDesc(CName = "单据状态描述", ShortCode = "StatusName", Desc = "单据状态描述", ContextType = SysDic.All, Length = 100)]
        public virtual string StatusName
        {
            get { return _statusName; }
            set { _statusName = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "主验收人ID", ShortCode = "AccepterID", Desc = "主验收人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? AccepterID
        {
            get { return _accepterID; }
            set { _accepterID = value; }
        }

        [DataMember]
        [DataDesc(CName = "主验收人", ShortCode = "AccepterName", Desc = "主验收人", ContextType = SysDic.All, Length = 100)]
        public virtual string AccepterName
        {
            get { return _accepterName; }
            set { _accepterName = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "验收时间", ShortCode = "AcceptTime", Desc = "验收时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? AcceptTime
        {
            get { return _acceptTime; }
            set { _acceptTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "次验收人ID", ShortCode = "SecAccepterID", Desc = "次验收人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? SecAccepterID
        {
            get { return _secAccepterID; }
            set { _secAccepterID = value; }
        }

        [DataMember]
        [DataDesc(CName = "次验收人", ShortCode = "SecAccepterName", Desc = "次验收人", ContextType = SysDic.All, Length = 100)]
        public virtual string SecAccepterName
        {
            get { return _secAccepterName; }
            set { _secAccepterName = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "次验收时间", ShortCode = "SecAcceptTime", Desc = "次验收时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? SecAcceptTime
        {
            get { return _secAcceptTime; }
            set { _secAcceptTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "验收备注", ShortCode = "AcceptMemo", Desc = "验收备注", ContextType = SysDic.All, Length = 1000)]
        public virtual string AcceptMemo
        {
            get { return _acceptMemo; }
            set { _acceptMemo = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否验收异常", ShortCode = "IsAcceptError", Desc = "是否验收异常", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsAcceptError
        {
            get { return _isAcceptError; }
            set { _isAcceptError = value; }
        }

        [DataMember]
        [DataDesc(CName = "打印次数", ShortCode = "PrintTimes", Desc = "打印次数", ContextType = SysDic.All, Length = 4)]
        public virtual int PrintTimes
        {
            get { return _printTimes; }
            set { _printTimes = value; }
        }

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = 1000)]
        public virtual string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "删除标志", ShortCode = "DeleteFlag", Desc = "删除标志", ContextType = SysDic.All, Length = 4)]
        public virtual int DeleteFlag
        {
            get { return _deleteFlag; }
            set { _deleteFlag = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        //[DataMember]
        //[DataDesc(CName = "供货验收明细单表", ShortCode = "ReaBmsCenSaleDtlConfirmList", Desc = "供货验收明细单表")]
        //public virtual IList<ReaBmsCenSaleDtlConfirm> ReaBmsCenSaleDtlConfirmList
        //{
        //    get
        //    {
        //        if (_reaBmsCenSaleDtlConfirmList == null)
        //        {
        //            _reaBmsCenSaleDtlConfirmList = new List<ReaBmsCenSaleDtlConfirm>();
        //        }
        //        return _reaBmsCenSaleDtlConfirmList;
        //    }
        //    set { _reaBmsCenSaleDtlConfirmList = value; }
        //}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "本地供应商ID", ShortCode = "ReaCompID", Desc = "本地供应商ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReaCompID
        {
            get { return _reaCompID; }
            set { _reaCompID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据来源类型", ShortCode = "SourceType", Desc = "数据来源类型", ContextType = SysDic.All, Length = 8)]
        public virtual long? SourceType
        {
            get { return _sourceType; }
            set { _sourceType = value; }
        }

        [DataMember]
        [DataDesc(CName = "发票号", ShortCode = "InvoiceNo", Desc = "发票号", ContextType = SysDic.All, Length = 100)]
        public virtual string InvoiceNo
        {
            get { return _invoiceNo; }
            set { _invoiceNo = value; }
        }
        [DataMember]
        [DataDesc(CName = "第三方单号", ShortCode = "OtherDocNo", Desc = "第三方单号", ContextType = SysDic.All, Length = 50)]
        public virtual string OtherDocNo
        {
            get { return _otherDocNo; }
            set
            {
                _otherDocNo = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZX1", Desc = "", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "", ShortCode = "ZX2", Desc = "", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "", ShortCode = "ZX3", Desc = "", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "货运单号", ShortCode = "TransportNo", Desc = "货运单号", ContextType = SysDic.All, Length = 300)]
        public virtual string TransportNo
        {
            get { return _transportNo; }
            set { _transportNo = value; }
        }
        #endregion
    }
    #endregion
}