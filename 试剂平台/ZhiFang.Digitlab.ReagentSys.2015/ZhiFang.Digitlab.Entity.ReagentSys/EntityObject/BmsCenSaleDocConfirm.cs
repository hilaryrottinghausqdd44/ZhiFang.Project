using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity.ReagentSys
{
    #region BmsCenSaleDocConfirm

    /// <summary>
    /// BmsCenSaleDocConfirm object for NHibernate mapped table 'BmsCenSaleDocConfirm'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "供货验收单表", ClassCName = "BmsCenSaleDocConfirm", ShortCode = "BmsCenSaleDocConfirm", Desc = "供货验收单表")]
    public class BmsCenSaleDocConfirm : BaseEntity
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
        protected BmsCenSaleDoc _bmsCenSaleDoc;
        protected BmsCenOrderDoc _BmsCenOrderDoc;
        protected CenOrg _comp;
        protected CenOrg _lab;
        protected IList<BmsCenSaleDtlConfirm> _bmsCenSaleDtlConfirmList;
        protected long? _reaCompID;
        protected string _reaCompName;
        protected long? _SourceType;
        protected string _InvoiceNo;

        #endregion

        #region Constructors

        public BmsCenSaleDocConfirm() { }

        public BmsCenSaleDocConfirm(string saleDocConfirmNo, string saleDocNo, string labName, string companyName, int status, string statusName, long accepterID, string accepterName, DateTime acceptTime, long secAccepterID, string secAccepterName, DateTime secAcceptTime, string acceptMemo, bool isAcceptError, int printTimes, string memo, int dispOrder, int deleteFlag, DateTime dataUpdateTime, DateTime dataAddTime, BmsCenSaleDoc bmsCenSaleDoc, CenOrg comp, CenOrg lab)
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
            this._bmsCenSaleDoc = bmsCenSaleDoc;
            this._comp = comp;
            this._lab = lab;
        }

        #endregion

        #region Public Properties


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
        [DataDesc(CName = "定单表编号", ShortCode = "OrderDocNo", Desc = "定单表编号")]
        public virtual string OrderDocNo
        {
            get { return _OrderDocNo; }
            set { _OrderDocNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "实验室名称", ShortCode = "LabName", Desc = "实验室名称", ContextType = SysDic.All, Length = 100)]
        public virtual string LabName
        {
            get { return _labName; }
            set { _labName = value; }
        }

        [DataMember]
        [DataDesc(CName = "供应商名称", ShortCode = "CompanyName", Desc = "供应商名称", ContextType = SysDic.All, Length = 200)]
        public virtual string CompanyName
        {
            get { return _companyName; }
            set { _companyName = value; }
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

        [DataMember]
        [DataDesc(CName = "平台供货总单表", ShortCode = "BmsCenSaleDoc", Desc = "平台供货总单表")]
        public virtual BmsCenSaleDoc BmsCenSaleDoc
        {
            get { return _bmsCenSaleDoc; }
            set { _bmsCenSaleDoc = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Comp", Desc = "")]
        public virtual CenOrg Comp
        {
            get { return _comp; }
            set { _comp = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Lab", Desc = "")]
        public virtual CenOrg Lab
        {
            get { return _lab; }
            set { _lab = value; }
        }

        [DataMember]
        [DataDesc(CName = "定单表", ShortCode = "BmsCenOrderDoc", Desc = "定单表")]
        public virtual BmsCenOrderDoc BmsCenOrderDoc
        {
            get { return _BmsCenOrderDoc; }
            set { _BmsCenOrderDoc = value; }
        }

       

        [DataMember]
        [DataDesc(CName = "供货验收明细单表", ShortCode = "BmsCenSaleDtlConfirmList", Desc = "供货验收明细单表")]
        public virtual IList<BmsCenSaleDtlConfirm> BmsCenSaleDtlConfirmList
        {
            get
            {
                if (_bmsCenSaleDtlConfirmList == null)
                {
                    _bmsCenSaleDtlConfirmList = new List<BmsCenSaleDtlConfirm>();
                }
                return _bmsCenSaleDtlConfirmList;
            }
            set { _bmsCenSaleDtlConfirmList = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "本地供应商ID", ShortCode = "ReaCompID", Desc = "本地供应商ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReaCompID
        {
            get { return _reaCompID; }
            set { _reaCompID = value; }
        }

        [DataMember]
        [DataDesc(CName = "供应商名称", ShortCode = "ReaCompName", Desc = "供应商名称", ContextType = SysDic.All, Length = 200)]
        public virtual string ReaCompName
        {
            get { return _reaCompName; }
            set
            {
                _reaCompName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据来源类型", ShortCode = "SourceType", Desc = "数据来源类型", ContextType = SysDic.All, Length = 8)]
        public virtual long? SourceType
        {
            get { return _SourceType; }
            set { _SourceType = value; }
        }

        [DataMember]
        [DataDesc(CName = "发票号", ShortCode = "InvoiceNo", Desc = "发票号", ContextType = SysDic.All, Length = 8)]
        public virtual string InvoiceNo
        {
            get { return _InvoiceNo; }
            set { _InvoiceNo = value; }
        }

        #endregion
    }
    #endregion
}