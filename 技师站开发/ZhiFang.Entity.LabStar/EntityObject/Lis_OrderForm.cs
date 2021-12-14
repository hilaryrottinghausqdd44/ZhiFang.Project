using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LisOrderForm

    /// <summary>
    /// LisOrderForm object for NHibernate mapped table 'Lis_OrderForm'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "医嘱单", ClassCName = "LisOrderForm", ShortCode = "LisOrderForm", Desc = "医嘱单")]
    public class LisOrderForm : BaseEntity
    {
        #region Member Variables

        protected DateTime? _partitionDate;
        protected string _orderFormNo;
        protected long? _orderTypeID;
        protected DateTime? _orderTime;
        protected DateTime? _orderExecTime;
        protected int _orderExecFlag;
        protected long? _hospitalID;
        protected long? _execDeptID;
        protected long? _destinationID;
        protected int _isUrgent;
        protected long? _clientID;
        protected int _isByHand;
        protected int _isAffirm;
        protected string _formMemo;
        protected long? _chargeID;
        protected string _chargeOrderName;
        protected double? _charge;
        protected int _isCheckFee;
        protected double? _balance;
        protected string _operHost;
        protected long? _operatorID;
        protected string _operatorName;
        protected long? _checkerID;
        protected string _checkerName;
        protected string _parItemCName;
        protected string _photoURL;
        protected string _requestSource;
        protected int _printTimes;
        protected string _hisHospitalNo;
        protected string _hisDeptNo;
        protected string _hisDeptName;
        protected string _hisDoctorNo;
        protected string _hisDoctor;
        protected string _hisDoctorPhoneCode;
        protected DateTime? _dataUpdateTime;
        protected long? _patID;
        /*protected LisPatient _lisPatient;*/


        #endregion

        #region Constructors

        public LisOrderForm() { }

        public LisOrderForm(DateTime partitionDate, string orderFormNo, long orderTypeID, DateTime orderTime, DateTime orderExecTime, int orderExecFlag, long hospitalID, long execDeptID, long destinationID, int isUrgent, long clientID, int isByHand, int isAffirm, string formMemo, long chargeID, string chargeOrderName, double charge, int isCheckFee, double balance, string operHost, long operatorID, string operatorName, long checkerID, string checkerName, string parItemCName, string photoURL, string requestSource, int printTimes, string hisHospitalNo, string hisDeptNo, string hisDeptName, string hisDoctorNo, string hisDoctor, string hisDoctorPhoneCode, DateTime dataAddTime, DateTime dataUpdateTime, long labID, byte[] dataTimeStamp, long PatID)
        {
            this._partitionDate = partitionDate;
            this._orderFormNo = orderFormNo;
            this._orderTypeID = orderTypeID;
            this._orderTime = orderTime;
            this._orderExecTime = orderExecTime;
            this._orderExecFlag = orderExecFlag;
            this._hospitalID = hospitalID;
            this._execDeptID = execDeptID;
            this._destinationID = destinationID;
            this._isUrgent = isUrgent;
            this._clientID = clientID;
            this._isByHand = isByHand;
            this._isAffirm = isAffirm;
            this._formMemo = formMemo;
            this._chargeID = chargeID;
            this._chargeOrderName = chargeOrderName;
            this._charge = charge;
            this._isCheckFee = isCheckFee;
            this._balance = balance;
            this._operHost = operHost;
            this._operatorID = operatorID;
            this._operatorName = operatorName;
            this._checkerID = checkerID;
            this._checkerName = checkerName;
            this._parItemCName = parItemCName;
            this._photoURL = photoURL;
            this._requestSource = requestSource;
            this._printTimes = printTimes;
            this._hisHospitalNo = hisHospitalNo;
            this._hisDeptNo = hisDeptNo;
            this._hisDeptName = hisDeptName;
            this._hisDoctorNo = hisDoctorNo;
            this._hisDoctor = hisDoctor;
            this._hisDoctorPhoneCode = hisDoctorPhoneCode;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._labID = labID;
            this._dataTimeStamp = dataTimeStamp;
            this._patID = PatID;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "分区日期", ShortCode = "PartitionDate", Desc = "分区日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? PartitionDate
        {
            get { return _partitionDate; }
            set { _partitionDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "医嘱单号", ShortCode = "OrderFormNo", Desc = "医嘱单号", ContextType = SysDic.All, Length = 100)]
        public virtual string OrderFormNo
        {
            get { return _orderFormNo; }
            set { _orderFormNo = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医嘱类型ID", ShortCode = "OrderTypeID", Desc = "医嘱类型ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? OrderTypeID
        {
            get { return _orderTypeID; }
            set { _orderTypeID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "开单日期", ShortCode = "OrderTime", Desc = "开单日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? OrderTime
        {
            get { return _orderTime; }
            set { _orderTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医嘱执行时间", ShortCode = "OrderExecTime", Desc = "医嘱执行时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? OrderExecTime
        {
            get { return _orderExecTime; }
            set { _orderExecTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "医嘱执行标志", ShortCode = "OrderExecFlag", Desc = "医嘱执行标志", ContextType = SysDic.All, Length = 4)]
        public virtual int OrderExecFlag
        {
            get { return _orderExecFlag; }
            set { _orderExecFlag = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "院区ID", ShortCode = "HospitalID", Desc = "院区ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? HospitalID
        {
            get { return _hospitalID; }
            set { _hospitalID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "检验执行科室ID", ShortCode = "ExecDeptID", Desc = "检验执行科室ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ExecDeptID
        {
            get { return _execDeptID; }
            set { _execDeptID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "送检目的地ID", ShortCode = "DestinationID", Desc = "送检目的地ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? DestinationID
        {
            get { return _destinationID; }
            set { _destinationID = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否急查", ShortCode = "IsUrgent", Desc = "是否急查", ContextType = SysDic.All, Length = 4)]
        public virtual int IsUrgent
        {
            get { return _isUrgent; }
            set { _isUrgent = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "客户ID", ShortCode = "ClientID", Desc = "客户ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ClientID
        {
            get { return _clientID; }
            set { _clientID = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否手工录入", ShortCode = "IsByHand", Desc = "是否手工录入", ContextType = SysDic.All, Length = 4)]
        public virtual int IsByHand
        {
            get { return _isByHand; }
            set { _isByHand = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否已经审核", ShortCode = "IsAffirm", Desc = "是否已经审核", ContextType = SysDic.All, Length = 4)]
        public virtual int IsAffirm
        {
            get { return _isAffirm; }
            set { _isAffirm = value; }
        }

        [DataMember]
        [DataDesc(CName = "医嘱备注", ShortCode = "FormMemo", Desc = "医嘱备注", ContextType = SysDic.All, Length = 500)]
        public virtual string FormMemo
        {
            get { return _formMemo; }
            set { _formMemo = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "收费类型ID", ShortCode = "ChargeID", Desc = "收费类型ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ChargeID
        {
            get { return _chargeID; }
            set { _chargeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "收费类型名称", ShortCode = "ChargeOrderName", Desc = "收费类型名称", ContextType = SysDic.All, Length = 100)]
        public virtual string ChargeOrderName
        {
            get { return _chargeOrderName; }
            set { _chargeOrderName = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "费用金额", ShortCode = "Charge", Desc = "费用金额", ContextType = SysDic.All, Length = 8)]
        public virtual double? Charge
        {
            get { return _charge; }
            set { _charge = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否已经计费", ShortCode = "IsCheckFee", Desc = "是否已经计费", ContextType = SysDic.All, Length = 4)]
        public virtual int IsCheckFee
        {
            get { return _isCheckFee; }
            set { _isCheckFee = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "余额", ShortCode = "Balance", Desc = "余额", ContextType = SysDic.All, Length = 8)]
        public virtual double? Balance
        {
            get { return _balance; }
            set { _balance = value; }
        }

        [DataMember]
        [DataDesc(CName = "开单站点", ShortCode = "OperHost", Desc = "开单站点", ContextType = SysDic.All, Length = 200)]
        public virtual string OperHost
        {
            get { return _operHost; }
            set { _operHost = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "开单者ID", ShortCode = "OperatorID", Desc = "开单者ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? OperatorID
        {
            get { return _operatorID; }
            set { _operatorID = value; }
        }

        [DataMember]
        [DataDesc(CName = "开单者", ShortCode = "OperatorName", Desc = "开单者", ContextType = SysDic.All, Length = 200)]
        public virtual string OperatorName
        {
            get { return _operatorName; }
            set { _operatorName = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "审核者ID", ShortCode = "CheckerID", Desc = "审核者ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? CheckerID
        {
            get { return _checkerID; }
            set { _checkerID = value; }
        }

        [DataMember]
        [DataDesc(CName = "审核者", ShortCode = "CheckerName", Desc = "审核者", ContextType = SysDic.All, Length = 200)]
        public virtual string CheckerName
        {
            get { return _checkerName; }
            set { _checkerName = value; }
        }

        [DataMember]
        [DataDesc(CName = "项目名称", ShortCode = "ParItemCName", Desc = "项目名称", ContextType = SysDic.All, Length = 2000)]
        public virtual string ParItemCName
        {
            get { return _parItemCName; }
            set { _parItemCName = value; }
        }

        [DataMember]
        [DataDesc(CName = "图片路径", ShortCode = "PhotoURL", Desc = "图片路径", ContextType = SysDic.All, Length = 200)]
        public virtual string PhotoURL
        {
            get { return _photoURL; }
            set { _photoURL = value; }
        }

        [DataMember]
        [DataDesc(CName = "医嘱来源", ShortCode = "RequestSource", Desc = "医嘱来源", ContextType = SysDic.All, Length = 200)]
        public virtual string RequestSource
        {
            get { return _requestSource; }
            set { _requestSource = value; }
        }

        [DataMember]
        [DataDesc(CName = "打印次数", ShortCode = "PrintTimes", Desc = "打印次数", ContextType = SysDic.All, Length = 4)]
        public virtual int PrintTimes
        {
            get { return _printTimes; }
            set { _printTimes = value; }
        }

        [DataMember]
        [DataDesc(CName = "His院区编号", ShortCode = "HisHospitalNo", Desc = "His院区编号", ContextType = SysDic.All, Length = 200)]
        public virtual string HisHospitalNo
        {
            get { return _hisHospitalNo; }
            set { _hisHospitalNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "His科室编号", ShortCode = "HisDeptNo", Desc = "His科室编号", ContextType = SysDic.All, Length = 200)]
        public virtual string HisDeptNo
        {
            get { return _hisDeptNo; }
            set { _hisDeptNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "His科室", ShortCode = "HisDeptName", Desc = "His科室", ContextType = SysDic.All, Length = 200)]
        public virtual string HisDeptName
        {
            get { return _hisDeptName; }
            set { _hisDeptName = value; }
        }

        [DataMember]
        [DataDesc(CName = "His医生编号", ShortCode = "HisDoctorNo", Desc = "His医生编号", ContextType = SysDic.All, Length = 200)]
        public virtual string HisDoctorNo
        {
            get { return _hisDoctorNo; }
            set { _hisDoctorNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "His医生", ShortCode = "HisDoctor", Desc = "His医生", ContextType = SysDic.All, Length = 200)]
        public virtual string HisDoctor
        {
            get { return _hisDoctor; }
            set { _hisDoctor = value; }
        }

        [DataMember]
        [DataDesc(CName = "His医生电话", ShortCode = "HisDoctorPhoneCode", Desc = "His医生电话", ContextType = SysDic.All, Length = 200)]
        public virtual string HisDoctorPhoneCode
        {
            get { return _hisDoctorPhoneCode; }
            set { _hisDoctorPhoneCode = value; }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "患者就诊信息ID", ShortCode = "PatID", Desc = "患者就诊信息ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? PatID
        {
            get { return _patID; }
            set { _patID = value; }
        }

        /*[DataMember]
        [DataDesc(CName = "患者就诊信息", ShortCode = "LisPatient", Desc = "患者就诊信息")]
        public virtual LisPatient LisPatient
        {
            get { return _lisPatient; }
            set { _lisPatient = value; }
        }*/


        #endregion
    }
    #endregion
}