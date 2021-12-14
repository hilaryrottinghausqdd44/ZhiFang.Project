using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
    #region PContractFollow

    /// <summary>
    /// PContract object for NHibernate mapped table 'P_Contract'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "项目跟踪", ClassCName = "PContractFollow", ShortCode = "PContractFollow", Desc = "")]
    public class PContractFollow : BaseEntity
    {
        #region Member Variables

        protected long? _pClientID;
        protected string _pClientName;
        protected long? _payOrgID;
        protected string _payOrg;
        protected string _contractNumber;
        protected string _name;
        protected string _sName;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected int _dispOrder;
        protected string _comment;
        protected bool _isUse;
        protected double _amount;
        protected double _payedMoney;
        protected double _leftMoney;
        protected string _rcvedRecord;
        protected double _rcvedRatio;
        protected string _invoiceMemoHTML;
        protected string _invoiceMemo;
        protected string _attention;
        protected string _specialDemand;
        protected string _newLinkEquipCost;
        protected DateTime? _implStartTime;
        protected DateTime? _implFinishTime;
        protected string _contract;
        protected string _attachModule;
        protected double _feeInCarrying;
        protected double _profit;
        protected double _feeBefore;
        protected string _content;
        protected bool _isChange;
        protected string _changeDesc;
        protected DateTime? _labFinishTime;
        protected string _remainingProblems;
        protected string _hasHIS;
        protected double _customCost;
        protected long? _pManagerID;
        protected string _pManager;
        protected string _hISFirm;
        protected string _purchaseDescHTML;
        protected string _purchaseDesc;
        protected string _implFirm;
        protected DateTime? _paidServiceStartTime;
        protected double _regularLinkEquip;
        protected string _partyAName;
        protected double _software;
        protected double _hardware;
        protected string _signedAnnualService;
        protected double _annualServiceCharge;
        protected double _businessExpenses;
        protected string _emphases;
        protected DateTime? _systemStartRunTime;
        protected double _purchaseFee;
        protected DateTime? _acceptDate;
        protected string _linkEquipInfoListHTML;
        protected string _linkEquipInfoList;
        protected string _hISDemand;
        protected string _acceptanceDescNo;
        protected string _noteBfImplHTML;
        protected string _noteBfImpl;
        protected DateTime? _needStartDate;
        protected DateTime? _needEndDate;
        protected double _implDays;
        protected double _middleFee;
        protected string _contractServiceCharge;
        protected string _projectLinkMan;
        protected string _linkMan;
        protected string _linkPhoneNo;
        protected DateTime? _installationTime;
        protected DateTime? _agreedPayTime;
        protected DateTime? _realityPayTime;
        protected string _isInvoices;
        protected DateTime? _licenseExpirationTime;
        protected DateTime? _licenseDeferredTime;
        protected string _totalDelay;
        protected string _newLinkEquipName;
        protected string _serviceYear;
        protected string _freeServiceDate;
        protected double _otherPaidExpenses;
        protected long? _provinceID;
        protected string _provinceName;
        protected long? _deptID;
        protected string _deptName;
        protected long? _compnameID;
        protected string _compname;
        protected long? _principalID;
        protected string _principal;
        protected string _contractStatus;
        protected long? _applyManID;
        protected string _applyMan;
        protected DateTime? _applyDate;
        protected long? _reviewManID;
        protected string _reviewMan;
        protected DateTime? _reviewDate;
        protected string _reviewInfo;
        protected long? _TreviewManID;
        protected string _TreviewMan;
        protected DateTime? _TreviewDate;
        protected string _TreviewInfo;
        protected long? _signManID;
        protected string _signMan;
        protected DateTime? _signDate;
        protected string _allHtmlInfo;
        protected double _invoiceMoney;
        protected int _EquipOneWayCount;
        protected int _EquipTwoWayCount;
        protected string _receiveName;
        protected string _receiveAddress;
        protected string _receivePhoneNumbers;
        protected string _freightName;
        protected string _freightOddNumbers;
        protected long? _contentID;

        protected long? _contrastId;
        protected string _contrastCName;
        protected long? _checkId;
        protected string _checkCName;
        protected long? _PContractAttrID;
        protected string _PContractAttrName;
        protected DateTime? _ServerContractStartDateTime;
        protected DateTime? _ServerContractEndDateTime;
        protected double _OriginalMoneyTotal;
        protected double _ServerChargeRatio;
        protected DateTime? _PlanSignDateTime;
        #endregion

        #region Constructors

        public PContractFollow() { }

        public PContractFollow(long labID, long pClientID, string pClientName, long payOrgID, string payOrg, string contractNumber, string name, string sName, string shortcode, string pinYinZiTou, int dispOrder, string comment, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp, double amount, double payedMoney, double leftMoney, string rcvedRecord, double rcvedRatio, string invoiceMemoHTML, string invoiceMemo, string attention, string specialDemand, string newLinkEquipCost, DateTime implStartTime, DateTime implFinishTime, string contract, string attachModule, double feeInCarrying, double profit, double feeBefore, string content, bool isChange, string changeDesc, DateTime labFinishTime, string remainingProblems, string hasHIS, double customCost, long pManagerID, string pManager, string hISFirm, string purchaseDescHTML, string purchaseDesc, string implFirm, DateTime paidServiceStartTime, double regularLinkEquip, string partyAName, double software, double hardware, string signedAnnualService, double annualServiceCharge, double businessExpenses, string emphases, DateTime systemStartRunTime, double purchaseFee, DateTime acceptDate, string linkEquipInfoListHTML, string linkEquipInfoList, string hISDemand, string acceptanceDescNo, string noteBfImplHTML, string noteBfImpl, DateTime needStartDate, DateTime needEndDate, double implDays, double middleFee, string contractServiceCharge, string projectLinkMan, string linkMan, string linkPhoneNo, DateTime installationTime, DateTime agreedPayTime, DateTime realityPayTime, string isInvoices, DateTime licenseExpirationTime, DateTime licenseDeferredTime, string totalDelay, string newLinkEquipName, string serviceYear, string freeServiceDate, double otherPaidExpenses, long provinceID, string provinceName, long deptID, string deptName, long compnameID, string compname, long principalID, string principal, string contractStatus, long applyManID, string applyMan, DateTime applyDate, long reviewManID, string reviewMan, DateTime reviewDate, string reviewInfo, long signManID, string signMan, DateTime signDate, string allHtmlInfo, string receiveName, string receiveAddress, string receivePhoneNumbers, string freightName, string freightOddNumbers, long? contentID, long? contrastId, string contrastCName, long? checkId, string checkCName)
        {
            this._labID = labID;
            this._pClientID = pClientID;
            this._pClientName = pClientName;
            this._payOrgID = payOrgID;
            this._payOrg = payOrg;
            this._contractNumber = contractNumber;
            this._name = name;
            this._sName = sName;
            this._shortcode = shortcode;
            this._pinYinZiTou = pinYinZiTou;
            this._dispOrder = dispOrder;
            this._comment = comment;
            this._isUse = isUse;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
            this._amount = amount;
            this._payedMoney = payedMoney;
            this._leftMoney = leftMoney;
            this._rcvedRecord = rcvedRecord;
            this._rcvedRatio = rcvedRatio;
            this._invoiceMemoHTML = invoiceMemoHTML;
            this._invoiceMemo = invoiceMemo;
            this._attention = attention;
            this._specialDemand = specialDemand;
            this._newLinkEquipCost = newLinkEquipCost;
            this._implStartTime = implStartTime;
            this._implFinishTime = implFinishTime;
            this._contract = contract;
            this._attachModule = attachModule;
            this._feeInCarrying = feeInCarrying;
            this._profit = profit;
            this._feeBefore = feeBefore;
            this._content = content;
            this._isChange = isChange;
            this._changeDesc = changeDesc;
            this._labFinishTime = labFinishTime;
            this._remainingProblems = remainingProblems;
            this._hasHIS = hasHIS;
            this._customCost = customCost;
            this._pManagerID = pManagerID;
            this._pManager = pManager;
            this._hISFirm = hISFirm;
            this._purchaseDescHTML = purchaseDescHTML;
            this._purchaseDesc = purchaseDesc;
            this._implFirm = implFirm;
            this._paidServiceStartTime = paidServiceStartTime;
            this._regularLinkEquip = regularLinkEquip;
            this._partyAName = partyAName;
            this._software = software;
            this._hardware = hardware;
            this._signedAnnualService = signedAnnualService;
            this._annualServiceCharge = annualServiceCharge;
            this._businessExpenses = businessExpenses;
            this._emphases = emphases;
            this._systemStartRunTime = systemStartRunTime;
            this._purchaseFee = purchaseFee;
            this._acceptDate = acceptDate;
            this._linkEquipInfoListHTML = linkEquipInfoListHTML;
            this._linkEquipInfoList = linkEquipInfoList;
            this._hISDemand = hISDemand;
            this._acceptanceDescNo = acceptanceDescNo;
            this._noteBfImplHTML = noteBfImplHTML;
            this._noteBfImpl = noteBfImpl;
            this._needStartDate = needStartDate;
            this._needEndDate = needEndDate;
            this._implDays = implDays;
            this._middleFee = middleFee;
            this._contractServiceCharge = contractServiceCharge;
            this._projectLinkMan = projectLinkMan;
            this._linkMan = linkMan;
            this._linkPhoneNo = linkPhoneNo;
            this._installationTime = installationTime;
            this._agreedPayTime = agreedPayTime;
            this._realityPayTime = realityPayTime;
            this._isInvoices = isInvoices;
            this._licenseExpirationTime = licenseExpirationTime;
            this._licenseDeferredTime = licenseDeferredTime;
            this._totalDelay = totalDelay;
            this._newLinkEquipName = newLinkEquipName;
            this._serviceYear = serviceYear;
            this._freeServiceDate = freeServiceDate;
            this._otherPaidExpenses = otherPaidExpenses;
            this._provinceID = provinceID;
            this._provinceName = provinceName;
            this._deptID = deptID;
            this._deptName = deptName;
            this._compnameID = compnameID;
            this._compname = compname;
            this._principalID = principalID;
            this._principal = principal;
            this._contractStatus = contractStatus;
            this._applyManID = applyManID;
            this._applyMan = applyMan;
            this._applyDate = applyDate;
            this._reviewManID = reviewManID;
            this._reviewMan = reviewMan;
            this._reviewDate = reviewDate;
            this._reviewInfo = reviewInfo;
            this._signManID = signManID;
            this._signMan = signMan;
            this._signDate = signDate;
            this._allHtmlInfo = allHtmlInfo;
            this._receiveName = receiveName;
            this._receiveAddress = receiveAddress;
            this._receivePhoneNumbers = receivePhoneNumbers;
            this._freightName = freightName;
            this._freightOddNumbers = freightOddNumbers;
            this._contentID = contentID;
            this._contrastId = contrastId;
            this._contrastCName = contrastCName;
            this._checkId = checkId;
            this._checkCName = checkCName;
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
        [DataDesc(CName = "用户名称", ShortCode = "PClientName", Desc = "用户名称", ContextType = SysDic.All, Length = 200)]
        public virtual string PClientName
        {
            get { return _pClientName; }
            set
            {
                if (value != null && value.Length > 200)
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
        [DataDesc(CName = "付款单位", ShortCode = "PayOrg", Desc = "付款单位", ContextType = SysDic.All, Length = 200)]
        public virtual string PayOrg
        {
            get { return _payOrg; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for PayOrg", value, value.ToString());
                _payOrg = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "合同编号", ShortCode = "ContractNumber", Desc = "合同编号", ContextType = SysDic.All, Length = 200)]
        public virtual string ContractNumber
        {
            get { return _contractNumber; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ContractNumber", value, value.ToString());
                _contractNumber = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "Name", Desc = "名称", ContextType = SysDic.All, Length = 100)]
        public virtual string Name
        {
            get { return _name; }
            set
            {
                if (value != null && value.Length > 100)
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
                if (value != null && value.Length > 40)
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
                if (value != null && value.Length > 20)
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
                if (value != null && value.Length > 50)
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
        [DataDesc(CName = "合同总额", ShortCode = "Amount", Desc = "合同总额", ContextType = SysDic.All, Length = 8)]
        public virtual double Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "已收金额", ShortCode = "PayedMoney", Desc = "已收金额", ContextType = SysDic.All, Length = 8)]
        public virtual double PayedMoney
        {
            get { return _payedMoney; }
            set { _payedMoney = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "剩余款项", ShortCode = "LeftMoney", Desc = "剩余款项", ContextType = SysDic.All, Length = 8)]
        public virtual double LeftMoney
        {
            get { return _leftMoney; }
            set { _leftMoney = value; }
        }

        [DataMember]
        [DataDesc(CName = "收付款记录", ShortCode = "RcvedRecord", Desc = "收付款记录", ContextType = SysDic.All, Length = 200)]
        public virtual string RcvedRecord
        {
            get { return _rcvedRecord; }
            set
            {
                _rcvedRecord = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "收款比例", ShortCode = "RcvedRatio", Desc = "收款比例", ContextType = SysDic.All, Length = 8)]
        public virtual double RcvedRatio
        {
            get { return _rcvedRatio; }
            set { _rcvedRatio = value; }
        }

        [DataMember]
        [DataDesc(CName = "发票备注HTML", ShortCode = "InvoiceMemoHTML", Desc = "发票备注HTML", ContextType = SysDic.All, Length = -1)]
        public virtual string InvoiceMemoHTML
        {
            get { return _invoiceMemoHTML; }
            set
            {
                _invoiceMemoHTML = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "发票备注", ShortCode = "InvoiceMemo", Desc = "发票备注", ContextType = SysDic.All, Length = 200)]
        public virtual string InvoiceMemo
        {
            get { return _invoiceMemo; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for InvoiceMemo", value, value.ToString());
                _invoiceMemo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "关注程度", ShortCode = "Attention", Desc = "关注程度", ContextType = SysDic.All, Length = 200)]
        public virtual string Attention
        {
            get { return _attention; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for Attention", value, value.ToString());
                _attention = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "特殊功能", ShortCode = "SpecialDemand", Desc = "特殊功能", ContextType = SysDic.All, Length = 200)]
        public virtual string SpecialDemand
        {
            get { return _specialDemand; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for SpecialDemand", value, value.ToString());
                _specialDemand = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "合同新联机费用", ShortCode = "NewLinkEquipCost", Desc = "合同新联机费用", ContextType = SysDic.All, Length = 200)]
        public virtual string NewLinkEquipCost
        {
            get { return _newLinkEquipCost; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for NewLinkEquipCost", value, value.ToString());
                _newLinkEquipCost = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "实施起始时间", ShortCode = "ImplStartTime", Desc = "实施起始时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ImplStartTime
        {
            get { return _implStartTime; }
            set { _implStartTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "实施结束时间", ShortCode = "ImplFinishTime", Desc = "实施结束时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ImplFinishTime
        {
            get { return _implFinishTime; }
            set { _implFinishTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "合同文本", ShortCode = "Contract", Desc = "合同文本", ContextType = SysDic.All, Length = 200)]
        public virtual string Contract
        {
            get { return _contract; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for Contract", value, value.ToString());
                _contract = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "附加模块", ShortCode = "AttachModule", Desc = "附加模块", ContextType = SysDic.All, Length = 200)]
        public virtual string AttachModule
        {
            get { return _attachModule; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for AttachModule", value, value.ToString());
                _attachModule = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "实施费用", ShortCode = "FeeInCarrying", Desc = "实施费用", ContextType = SysDic.All, Length = 8)]
        public virtual double FeeInCarrying
        {
            get { return _feeInCarrying; }
            set { _feeInCarrying = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "项目毛利", ShortCode = "Profit", Desc = "项目毛利", ContextType = SysDic.All, Length = 8)]
        public virtual double Profit
        {
            get { return _profit; }
            set { _profit = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "售前费用", ShortCode = "FeeBefore", Desc = "售前费用", ContextType = SysDic.All, Length = 8)]
        public virtual double FeeBefore
        {
            get { return _feeBefore; }
            set { _feeBefore = value; }
        }

        [DataMember]
        [DataDesc(CName = "项目类别", ShortCode = "Content", Desc = "项目类别", ContextType = SysDic.All, Length = 200)]
        public virtual string Content
        {
            get { return _content; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for Content", value, value.ToString());
                _content = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "合同有无变更", ShortCode = "IsChange", Desc = "合同有无变更", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsChange
        {
            get { return _isChange; }
            set { _isChange = value; }
        }

        [DataMember]
        [DataDesc(CName = "变更说明", ShortCode = "ChangeDesc", Desc = "变更说明", ContextType = SysDic.All, Length = 200)]
        public virtual string ChangeDesc
        {
            get { return _changeDesc; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ChangeDesc", value, value.ToString());
                _changeDesc = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "科内完成时间", ShortCode = "LabFinishTime", Desc = "科内完成时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? LabFinishTime
        {
            get { return _labFinishTime; }
            set { _labFinishTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "遗留问题", ShortCode = "RemainingProblems", Desc = "遗留问题", ContextType = SysDic.All, Length = 200)]
        public virtual string RemainingProblems
        {
            get { return _remainingProblems; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for RemainingProblems", value, value.ToString());
                _remainingProblems = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "是否有HIS接口", ShortCode = "HasHIS", Desc = "是否有HIS接口", ContextType = SysDic.All, Length = 200)]
        public virtual string HasHIS
        {
            get { return _hasHIS; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for HasHIS", value, value.ToString());
                _hasHIS = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "定制费用", ShortCode = "CustomCost", Desc = "定制费用", ContextType = SysDic.All, Length = 8)]
        public virtual double CustomCost
        {
            get { return _customCost; }
            set { _customCost = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "实施负责人ID", ShortCode = "PManagerID", Desc = "实施负责人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? PManagerID
        {
            get { return _pManagerID; }
            set { _pManagerID = value; }
        }

        [DataMember]
        [DataDesc(CName = "实施负责人", ShortCode = "PManager", Desc = "实施负责人", ContextType = SysDic.All, Length = 200)]
        public virtual string PManager
        {
            get { return _pManager; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for PManager", value, value.ToString());
                _pManager = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "HIS厂商", ShortCode = "HISFirm", Desc = "HIS厂商", ContextType = SysDic.All, Length = 200)]
        public virtual string HISFirm
        {
            get { return _hISFirm; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for HISFirm", value, value.ToString());
                _hISFirm = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "采购说明HTML", ShortCode = "PurchaseDescHTML", Desc = "采购说明HTML", ContextType = SysDic.All, Length = -1)]
        public virtual string PurchaseDescHTML
        {
            get { return _purchaseDescHTML; }
            set
            {
                _purchaseDescHTML = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "采购说明", ShortCode = "PurchaseDesc", Desc = "采购说明", ContextType = SysDic.All, Length = 200)]
        public virtual string PurchaseDesc
        {
            get { return _purchaseDesc; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for PurchaseDesc", value, value.ToString());
                _purchaseDesc = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "实施方", ShortCode = "ImplFirm", Desc = "实施方", ContextType = SysDic.All, Length = 200)]
        public virtual string ImplFirm
        {
            get { return _implFirm; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ImplFirm", value, value.ToString());
                _implFirm = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "有偿服务应开始时间", ShortCode = "PaidServiceStartTime", Desc = "有偿服务应开始时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? PaidServiceStartTime
        {
            get { return _paidServiceStartTime; }
            set { _paidServiceStartTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "规定连机数量", ShortCode = "RegularLinkEquip", Desc = "规定连机数量", ContextType = SysDic.All, Length = 8)]
        public virtual double RegularLinkEquip
        {
            get { return _regularLinkEquip; }
            set { _regularLinkEquip = value; }
        }

        [DataMember]
        [DataDesc(CName = "甲方名称", ShortCode = "PartyAName", Desc = "甲方名称", ContextType = SysDic.All, Length = 200)]
        public virtual string PartyAName
        {
            get { return _partyAName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for PartyAName", value, value.ToString());
                _partyAName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "软件", ShortCode = "Software", Desc = "软件", ContextType = SysDic.All, Length = 8)]
        public virtual double Software
        {
            get { return _software; }
            set { _software = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "硬件", ShortCode = "Hardware", Desc = "硬件", ContextType = SysDic.All, Length = 8)]
        public virtual double Hardware
        {
            get { return _hardware; }
            set { _hardware = value; }
        }

        [DataMember]
        [DataDesc(CName = "服务签署年数", ShortCode = "SignedAnnualService", Desc = "服务签署年数", ContextType = SysDic.All, Length = 200)]
        public virtual string SignedAnnualService
        {
            get { return _signedAnnualService; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for SignedAnnualService", value, value.ToString());
                _signedAnnualService = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "每年服务费用", ShortCode = "AnnualServiceCharge", Desc = "每年服务费用", ContextType = SysDic.All, Length = 8)]
        public virtual double AnnualServiceCharge
        {
            get { return _annualServiceCharge; }
            set { _annualServiceCharge = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "商务费用", ShortCode = "BusinessExpenses", Desc = "商务费用", ContextType = SysDic.All, Length = 8)]
        public virtual double BusinessExpenses
        {
            get { return _businessExpenses; }
            set { _businessExpenses = value; }
        }

        [DataMember]
        [DataDesc(CName = "重点", ShortCode = "Emphases", Desc = "重点", ContextType = SysDic.All, Length = 200)]
        public virtual string Emphases
        {
            get { return _emphases; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for Emphases", value, value.ToString());
                _emphases = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "系统投入运行时间", ShortCode = "SystemStartRunTime", Desc = "系统投入运行时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? SystemStartRunTime
        {
            get { return _systemStartRunTime; }
            set { _systemStartRunTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "采购费用", ShortCode = "PurchaseFee", Desc = "采购费用", ContextType = SysDic.All, Length = 8)]
        public virtual double PurchaseFee
        {
            get { return _purchaseFee; }
            set { _purchaseFee = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "验收时间", ShortCode = "AcceptDate", Desc = "验收时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? AcceptDate
        {
            get { return _acceptDate; }
            set { _acceptDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "仪器连机清单HTML", ShortCode = "LinkEquipInfoListHTML", Desc = "仪器连机清单HTML", ContextType = SysDic.All, Length = -1)]
        public virtual string LinkEquipInfoListHTML
        {
            get { return _linkEquipInfoListHTML; }
            set
            {
                _linkEquipInfoListHTML = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "仪器连机清单", ShortCode = "LinkEquipInfoList", Desc = "仪器连机清单", ContextType = SysDic.All, Length = 200)]
        public virtual string LinkEquipInfoList
        {
            get { return _linkEquipInfoList; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for LinkEquipInfoList", value, value.ToString());
                _linkEquipInfoList = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "HIS接口要求", ShortCode = "HISDemand", Desc = "HIS接口要求", ContextType = SysDic.All, Length = 200)]
        public virtual string HISDemand
        {
            get { return _hISDemand; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for HISDemand", value, value.ToString());
                _hISDemand = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "验收说明(编号)", ShortCode = "AcceptanceDescNo", Desc = "验收说明(编号)", ContextType = SysDic.All, Length = 200)]
        public virtual string AcceptanceDescNo
        {
            get { return _acceptanceDescNo; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for AcceptanceDescNo", value, value.ToString());
                _acceptanceDescNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "工程注意事项HTML", ShortCode = "NoteBfImplHTML", Desc = "工程注意事项HTML", ContextType = SysDic.All, Length = -1)]
        public virtual string NoteBfImplHTML
        {
            get { return _noteBfImplHTML; }
            set
            {
                _noteBfImplHTML = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "工程注意事项", ShortCode = "NoteBfImpl", Desc = "工程注意事项", ContextType = SysDic.All, Length = 200)]
        public virtual string NoteBfImpl
        {
            get { return _noteBfImpl; }
            set
            {
                _noteBfImpl = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "要求实施时间", ShortCode = "NeedStartDate", Desc = "要求实施时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? NeedStartDate
        {
            get { return _needStartDate; }
            set { _needStartDate = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "要求完成时间", ShortCode = "NeedEndDate", Desc = "要求完成时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? NeedEndDate
        {
            get { return _needEndDate; }
            set { _needEndDate = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "实际实施天数", ShortCode = "ImplDays", Desc = "实际实施天数", ContextType = SysDic.All, Length = 8)]
        public virtual double ImplDays
        {
            get { return _implDays; }
            set { _implDays = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "其它费用", ShortCode = "MiddleFee", Desc = "其它费用", ContextType = SysDic.All, Length = 8)]
        public virtual double MiddleFee
        {
            get { return _middleFee; }
            set { _middleFee = value; }
        }

        [DataMember]
        [DataDesc(CName = "合同服务费用", ShortCode = "ContractServiceCharge", Desc = "合同服务费用", ContextType = SysDic.All, Length = 200)]
        public virtual string ContractServiceCharge
        {
            get { return _contractServiceCharge; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ContractServiceCharge", value, value.ToString());
                _contractServiceCharge = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "项目联系人", ShortCode = "ProjectLinkMan", Desc = "项目联系人", ContextType = SysDic.All, Length = 200)]
        public virtual string ProjectLinkMan
        {
            get { return _projectLinkMan; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ProjectLinkMan", value, value.ToString());
                _projectLinkMan = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "联系人", ShortCode = "LinkMan", Desc = "联系人", ContextType = SysDic.All, Length = 200)]
        public virtual string LinkMan
        {
            get { return _linkMan; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for LinkMan", value, value.ToString());
                _linkMan = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "联系电话", ShortCode = "LinkPhoneNo", Desc = "联系电话", ContextType = SysDic.All, Length = 200)]
        public virtual string LinkPhoneNo
        {
            get { return _linkPhoneNo; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for LinkPhoneNo", value, value.ToString());
                _linkPhoneNo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "安装时间", ShortCode = "InstallationTime", Desc = "安装时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? InstallationTime
        {
            get { return _installationTime; }
            set { _installationTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "约定回款时间", ShortCode = "AgreedPayTime", Desc = "约定回款时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? AgreedPayTime
        {
            get { return _agreedPayTime; }
            set { _agreedPayTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "实际回款时间", ShortCode = "RealityPayTime", Desc = "实际回款时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? RealityPayTime
        {
            get { return _realityPayTime; }
            set { _realityPayTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否开具发票", ShortCode = "IsInvoices", Desc = "是否开具发票", ContextType = SysDic.All, Length = 200)]
        public virtual string IsInvoices
        {
            get { return _isInvoices; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for IsInvoices", value, value.ToString());
                _isInvoices = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "授权到期时间", ShortCode = "LicenseExpirationTime", Desc = "授权到期时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? LicenseExpirationTime
        {
            get { return _licenseExpirationTime; }
            set { _licenseExpirationTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "授权延期至", ShortCode = "LicenseDeferredTime", Desc = "授权延期至", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? LicenseDeferredTime
        {
            get { return _licenseDeferredTime; }
            set { _licenseDeferredTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "累积延期", ShortCode = "TotalDelay", Desc = "累积延期", ContextType = SysDic.All, Length = 200)]
        public virtual string TotalDelay
        {
            get { return _totalDelay; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for TotalDelay", value, value.ToString());
                _totalDelay = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "新连机仪器名称", ShortCode = "NewLinkEquipName", Desc = "新连机仪器名称", ContextType = SysDic.All, Length = 200)]
        public virtual string NewLinkEquipName
        {
            get { return _newLinkEquipName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for NewLinkEquipName", value, value.ToString());
                _newLinkEquipName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "服务年份", ShortCode = "ServiceYear", Desc = "服务年份", ContextType = SysDic.All, Length = 56)]
        public virtual string ServiceYear
        {
            get { return _serviceYear; }
            set
            {
                if (value != null && value.Length > 56)
                    throw new ArgumentOutOfRangeException("Invalid value for ServiceYear", value, value.ToString());
                _serviceYear = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "免费服务期", ShortCode = "FreeServiceDate", Desc = "免费服务期", ContextType = SysDic.All, Length = 200)]
        public virtual string FreeServiceDate
        {
            get { return _freeServiceDate; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for FreeServiceDate", value, value.ToString());
                _freeServiceDate = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "其它费用已付", ShortCode = "OtherPaidExpenses", Desc = "其它费用已付", ContextType = SysDic.All, Length = 8)]
        public virtual double OtherPaidExpenses
        {
            get { return _otherPaidExpenses; }
            set { _otherPaidExpenses = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "省份ID", ShortCode = "ProvinceID", Desc = "省份ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ProvinceID
        {
            get { return _provinceID; }
            set { _provinceID = value; }
        }

        [DataMember]
        [DataDesc(CName = "省份", ShortCode = "ProvinceName", Desc = "省份", ContextType = SysDic.All, Length = 200)]
        public virtual string ProvinceName
        {
            get { return _provinceName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ProvinceName", value, value.ToString());
                _provinceName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DeptID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? DeptID
        {
            get { return _deptID; }
            set { _deptID = value; }
        }

        [DataMember]
        [DataDesc(CName = "所属部门", ShortCode = "DeptName", Desc = "所属部门", ContextType = SysDic.All, Length = 200)]
        public virtual string DeptName
        {
            get { return _deptName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for DeptName", value, value.ToString());
                _deptName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CompnameID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? CompnameID
        {
            get { return _compnameID; }
            set { _compnameID = value; }
        }

        [DataMember]
        [DataDesc(CName = "本公司名称", ShortCode = "Compname", Desc = "本公司名称", ContextType = SysDic.All, Length = 100)]
        public virtual string Compname
        {
            get { return _compname; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Compname", value, value.ToString());
                _compname = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "销售负责人ID", ShortCode = "PrincipalID", Desc = "销售负责人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? PrincipalID
        {
            get { return _principalID; }
            set { _principalID = value; }
        }

        [DataMember]
        [DataDesc(CName = "销售负责人", ShortCode = "Principal", Desc = "销售负责人", ContextType = SysDic.All, Length = 200)]
        public virtual string Principal
        {
            get { return _principal; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for Principal", value, value.ToString());
                _principal = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "合同状态", ShortCode = "ContractStatus", Desc = "合同状态", ContextType = SysDic.All, Length = 200)]
        public virtual string ContractStatus
        {
            get { return _contractStatus; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ContractStatus", value, value.ToString());
                _contractStatus = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ApplyManID", Desc = "", ContextType = SysDic.All, Length = 8)]
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
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ApplyMan", value, value.ToString());
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
        [DataDesc(CName = "商务评审人ID", ShortCode = "ReviewManID", Desc = "商务评审人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReviewManID
        {
            get { return _reviewManID; }
            set { _reviewManID = value; }
        }

        [DataMember]
        [DataDesc(CName = "商务评审人", ShortCode = "ReviewMan", Desc = "商务评审人", ContextType = SysDic.All, Length = 200)]
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
        [DataDesc(CName = "商务评审时间", ShortCode = "ReviewDate", Desc = "商务评审时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ReviewDate
        {
            get { return _reviewDate; }
            set { _reviewDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "商务评审人意见", ShortCode = "ReviewInfo", Desc = "商务评审人意见", ContextType = SysDic.All, Length = 500)]
        public virtual string ReviewInfo
        {
            get { return _reviewInfo; }
            set
            {
                _reviewInfo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "技术评审人ID", ShortCode = "TechReviewManID", Desc = "技术评审人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? TechReviewManID
        {
            get { return _TreviewManID; }
            set { _TreviewManID = value; }
        }

        [DataMember]
        [DataDesc(CName = "技术评审人", ShortCode = "TechReviewMan", Desc = "技术评审人", ContextType = SysDic.All, Length = 200)]
        public virtual string TechReviewMan
        {
            get { return _TreviewMan; }
            set
            {
                _TreviewMan = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "技术评审时间", ShortCode = "TechReviewDate", Desc = "技术评审时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? TechReviewDate
        {
            get { return _TreviewDate; }
            set { _TreviewDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "技术评审人意见", ShortCode = "TechReviewInfo", Desc = "技术评审人意见", ContextType = SysDic.All, Length = 500)]
        public virtual string TechReviewInfo
        {
            get { return _TreviewInfo; }
            set
            {
                _TreviewInfo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "签署人ID", ShortCode = "SignManID", Desc = "签署人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? SignManID
        {
            get { return _signManID; }
            set { _signManID = value; }
        }

        [DataMember]
        [DataDesc(CName = "签署人", ShortCode = "SignMan", Desc = "签署人", ContextType = SysDic.All, Length = 200)]
        public virtual string SignMan
        {
            get { return _signMan; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for SignMan", value, value.ToString());
                _signMan = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "签署日期", ShortCode = "SignDate", Desc = "签署日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? SignDate
        {
            get { return _signDate; }
            set { _signDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "附加信息", ShortCode = "AllHtmlInfo", Desc = "附加信息", ContextType = SysDic.All, Length = -1)]
        public virtual string AllHtmlInfo
        {
            get { return _allHtmlInfo; }
            set
            {
                _allHtmlInfo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "已开发票金额", ShortCode = "InvoiceMoney", Desc = "已开发票金额", ContextType = SysDic.All, Length = 8)]
        public virtual double InvoiceMoney
        {
            get { return _invoiceMoney; }
            set { _invoiceMoney = value; }
        }

        [DataMember]
        [DataDesc(CName = "单项仪器个数", ShortCode = "EquipOneWayCount", Desc = "单项仪器个数", ContextType = SysDic.All, Length = 8)]
        public virtual int EquipOneWayCount
        {
            get { return _EquipOneWayCount; }
            set { _EquipOneWayCount = value; }
        }
        [DataMember]
        [DataDesc(CName = "双向仪器个数", ShortCode = "EquipTwoWayCount", Desc = "双向仪器个数", ContextType = SysDic.All, Length = 8)]
        public virtual int EquipTwoWayCount
        {
            get { return _EquipTwoWayCount; }
            set { _EquipTwoWayCount = value; }
        }


        [DataMember]
        [DataDesc(CName = "收货人姓名", ShortCode = "ReceiveName", Desc = "收货人姓名", ContextType = SysDic.All, Length = 100)]
        public virtual string ReceiveName
        {
            get { return _receiveName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for ReceiveName", value, value.ToString());
                _receiveName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "收货人地址", ShortCode = "ReceiveAddress", Desc = "收货人地址", ContextType = SysDic.All, Length = 200)]
        public virtual string ReceiveAddress
        {
            get { return _receiveAddress; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ReceiveAddress", value, value.ToString());
                _receiveAddress = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "收货人电话", ShortCode = "ReceivePhoneNumbers", Desc = "收货人电话", ContextType = SysDic.All, Length = 20)]
        public virtual string ReceivePhoneNumbers
        {
            get { return _receivePhoneNumbers; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for ReceivePhoneNumbers", value, value.ToString());
                _receivePhoneNumbers = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "货运公司名称", ShortCode = "FreightName", Desc = "货运公司名称", ContextType = SysDic.All, Length = 200)]
        public virtual string FreightName
        {
            get { return _freightName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for FreightName", value, value.ToString());
                _freightName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "货运单号", ShortCode = "FreightOddNumbers", Desc = "货运单号", ContextType = SysDic.All, Length = 200)]
        public virtual string FreightOddNumbers
        {
            get { return _freightOddNumbers; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for FreightOddNumbers", value, value.ToString());
                _freightOddNumbers = value;
            }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "项目类别ID", ShortCode = "ContentID", Desc = "项目类别ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ContentID
        {
            get { return _contentID; }
            set { _contentID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "对比人Id", ShortCode = "ContrastId", Desc = "对比人Id", ContextType = SysDic.All, Length = 8)]
        public virtual long? ContrastId
        {
            get { return _contrastId; }
            set { _contrastId = value; }
        }

        [DataMember]
        [DataDesc(CName = "对比人名称", ShortCode = "ContrastCName", Desc = "对比人名称", ContextType = SysDic.All, Length = 60)]
        public virtual string ContrastCName
        {
            get { return _contrastCName; }
            set
            {
                if (value != null && value.Length > 60)
                    throw new ArgumentOutOfRangeException("Invalid value for ContrastCName", value, value.ToString());
                _contrastCName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审核人Id", ShortCode = "CheckId", Desc = "审核人Id", ContextType = SysDic.All, Length = 8)]
        public virtual long? CheckId
        {
            get { return _checkId; }
            set { _checkId = value; }
        }

        [DataMember]
        [DataDesc(CName = "审核人名称", ShortCode = "CheckCName", Desc = "审核人名称", ContextType = SysDic.All, Length = 60)]
        public virtual string CheckCName
        {
            get { return _checkCName; }
            set
            {
                if (value != null && value.Length > 60)
                    throw new ArgumentOutOfRangeException("Invalid value for CheckCName", value, value.ToString());
                _checkCName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "合同性质Id", ShortCode = "PContractAttrID", Desc = "合同性质Id", ContextType = SysDic.All, Length = 8)]
        public virtual long? PContractAttrID
        {
            get { return _PContractAttrID; }
            set { _PContractAttrID = value; }
        }

        [DataMember]
        [DataDesc(CName = "合同性质名称", ShortCode = "PContractAttrName", Desc = "合同性质名称", ContextType = SysDic.All, Length = 200)]
        public virtual string PContractAttrName
        {
            get { return _PContractAttrName; }
            set
            {
                _PContractAttrName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "服务合同开始时间", ShortCode = "ServerContractStartDateTime", Desc = "服务合同开始时间", ContextType = SysDic.All, Length = 200)]
        public virtual DateTime? ServerContractStartDateTime
        {
            get { return _ServerContractStartDateTime; }
            set
            {
                _ServerContractStartDateTime = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "到期时间", ShortCode = "ServerContractEndDateTime", Desc = "到期时间", ContextType = SysDic.All, Length = 200)]
        public virtual DateTime? ServerContractEndDateTime
        {
            get { return _ServerContractEndDateTime; }
            set
            {
                _ServerContractEndDateTime = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "原始合同总额", ShortCode = "OriginalMoneyTotal", Desc = "原始合同总额", ContextType = SysDic.All, Length = 200)]
        public virtual double OriginalMoneyTotal
        {
            get { return _OriginalMoneyTotal; }
            set
            {
                _OriginalMoneyTotal = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "服务收费比例", ShortCode = "ServerChargeRatio", Desc = "服务收费比例", ContextType = SysDic.All, Length = 200)]
        public virtual double ServerChargeRatio
        {
            get { return _ServerChargeRatio; }
            set
            {
                _ServerChargeRatio = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "计划签署时间", ShortCode = "PlanSignDateTime", Desc = "计划签署时间", ContextType = SysDic.All, Length = 200)]
        public virtual DateTime? PlanSignDateTime
        {
            get { return _PlanSignDateTime; }
            set
            {
                _PlanSignDateTime = value;
            }
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