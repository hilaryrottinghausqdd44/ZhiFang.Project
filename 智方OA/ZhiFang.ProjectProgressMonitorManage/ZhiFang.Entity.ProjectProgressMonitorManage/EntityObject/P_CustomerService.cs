using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
    #region PCustomerService

    /// <summary>
    /// PCustomerService object for NHibernate mapped table 'P_CustomerService'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "服务单管理", ClassCName = "PCustomerService", ShortCode = "PCustomerService", Desc = "服务单管理")]
    public class PCustomerService : BaseEntity
    {
        #region Member Variables

        protected string _customerServiceNo;
        protected long? _clientID;
        protected string _clientName;
        protected long? _componeID;
        protected string _componeName;
        protected long? _customerServiceTypeID;
        protected string _customerServiceTypeName;
        protected long? _provinceID;
        protected string _provinceName;
        protected string _customerServiceMemo;
        protected string _problemMemo;
        protected string _analysisTreatmentMethods;
        protected long _status;
        protected long? _phenomenonTypeID;
        protected string _phenomenonTypeName;
        protected long? _reasonTypeID;
        protected string _reasonTypeName;
        protected long? _measuresTypeID;
        protected string _measuresTypeName;
        protected long? _programFuncID;
        protected string _programFuncName;
        protected long? _serviceModeID;
        protected string _serviceModeName;
        protected long? _serviceAcceptanceManID;
        protected string _serviceAcceptanceMan;
        protected DateTime? _serviceAcceptanceDate;
        protected long? _serviceReturnManID;
        protected string _serviceReturnMan;
        protected DateTime? _serviceReturnDate;
        protected string _serviceReturnToMan;
        protected string _serviceReturnToManPhone;
        protected string _serviceReturnMemo;
        protected DateTime? _serviceFinishDate;
        protected long? _satisfactionID;
        protected string _satisfactionName;
        protected long? _completionStatusID;
        protected string _completionStatusName;
        protected int _dispOrder;
        protected string _comment;
        protected bool _isUse;
        protected long _serviceOperationCompleteManID;
        protected string _serviceOperationCompleteMan;
        protected string _serviceOperationCompleteMemo;
        protected bool _isProxy;
        protected string _requestMan;
        protected string _requestManPhone;
        protected long _serviceRegisterManID;
        protected string _serviceRegisterMan;
        protected DateTime? _serviceRegisterDate;
        protected DateTime? _serviceOperationDate;
        private long? _deptID;
        private string _deptName;
        private string _statusCName;

        #endregion

        #region Constructors

        public PCustomerService() { }

        public PCustomerService(long labID, string customerServiceNo, long clientID, string clientName, long componeID, string componeName, long customerServiceTypeID, string customerServiceTypeName, long provinceID, string provinceName, string customerServiceMemo, string problemMemo, string analysisTreatmentMethods, long status, long phenomenonTypeID, string phenomenonTypeName, long reasonTypeID, string reasonTypeName, long measuresTypeID, string measuresTypeName, long programFuncID, string programFuncName, long serviceModeID, string serviceModeName, long serviceAcceptanceManID, string serviceAcceptanceMan, DateTime serviceAcceptanceDate, long serviceReturnManID, string serviceReturnMan, DateTime serviceReturnDate, string serviceReturnToMan, string serviceReturnToManPhone, string serviceReturnMemo, DateTime serviceFinishDate, long satisfactionID, string satisfactionName, long completionStatusID, string completionStatusName, int dispOrder, string comment, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp, long serviceOperationCompleteManID, string serviceOperationCompleteMan, string serviceOperationCompleteMemo, bool isProxy, string requestMan, string requestManPhone, long serviceRegisterManID, string serviceRegisterMan, DateTime serviceRegisterDate, DateTime serviceOperationDate, string statusCName)
        {
            this._labID = labID;
            this._customerServiceNo = customerServiceNo;
            this._clientID = clientID;
            this._clientName = clientName;
            this._componeID = componeID;
            this._componeName = componeName;
            this._customerServiceTypeID = customerServiceTypeID;
            this._customerServiceTypeName = customerServiceTypeName;
            this._provinceID = provinceID;
            this._provinceName = provinceName;
            this._customerServiceMemo = customerServiceMemo;
            this._problemMemo = problemMemo;
            this._analysisTreatmentMethods = analysisTreatmentMethods;
            this._status = status;
            this._phenomenonTypeID = phenomenonTypeID;
            this._phenomenonTypeName = phenomenonTypeName;
            this._reasonTypeID = reasonTypeID;
            this._reasonTypeName = reasonTypeName;
            this._measuresTypeID = measuresTypeID;
            this._measuresTypeName = measuresTypeName;
            this._programFuncID = programFuncID;
            this._programFuncName = programFuncName;
            this._serviceModeID = serviceModeID;
            this._serviceModeName = serviceModeName;
            this._serviceAcceptanceManID = serviceAcceptanceManID;
            this._serviceAcceptanceMan = serviceAcceptanceMan;
            this._serviceAcceptanceDate = serviceAcceptanceDate;
            this._serviceReturnManID = serviceReturnManID;
            this._serviceReturnMan = serviceReturnMan;
            this._serviceReturnDate = serviceReturnDate;
            this._serviceReturnToMan = serviceReturnToMan;
            this._serviceReturnToManPhone = serviceReturnToManPhone;
            this._serviceReturnMemo = serviceReturnMemo;
            this._serviceFinishDate = serviceFinishDate;
            this._satisfactionID = satisfactionID;
            this._satisfactionName = satisfactionName;
            this._completionStatusID = completionStatusID;
            this._completionStatusName = completionStatusName;
            this._dispOrder = dispOrder;
            this._comment = comment;
            this._isUse = isUse;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
            this._serviceOperationCompleteManID = serviceOperationCompleteManID;
            this._serviceOperationCompleteMan = serviceOperationCompleteMan;
            this._serviceOperationCompleteMemo = serviceOperationCompleteMemo;
            this._isProxy = isProxy;
            this._requestMan = requestMan;
            this._requestManPhone = requestManPhone;
            this._serviceRegisterManID = serviceRegisterManID;
            this._serviceRegisterMan = serviceRegisterMan;
            this._serviceRegisterDate = serviceRegisterDate;
            this._serviceOperationDate = serviceOperationDate;
            this._statusCName = statusCName;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "服务单号", ShortCode = "CustomerServiceNo", Desc = "服务单号", ContextType = SysDic.All, Length = 200)]
        public virtual string CustomerServiceNo
        {
            get { return _customerServiceNo; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for CustomerServiceNo", value, value.ToString());
                _customerServiceNo = value;
            }
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
        [DataDesc(CName = "客户名称", ShortCode = "ClientName", Desc = "客户名称", ContextType = SysDic.All, Length = 200)]
        public virtual string ClientName
        {
            get { return _clientName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ClientName", value, value.ToString());
                _clientName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "本公司ID", ShortCode = "ComponeID", Desc = "本公司ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ComponeID
        {
            get { return _componeID; }
            set { _componeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "本公司名称", ShortCode = "ComponeName", Desc = "本公司名称", ContextType = SysDic.All, Length = 200)]
        public virtual string ComponeName
        {
            get { return _componeName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ComponeName", value, value.ToString());
                _componeName = value;
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
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for DeptName", value, value.ToString());
                _deptName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "服务单类型ID", ShortCode = "CustomerServiceTypeID", Desc = "服务单类型ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? CustomerServiceTypeID
        {
            get { return _customerServiceTypeID; }
            set { _customerServiceTypeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "服务单类型名称", ShortCode = "CustomerServiceTypeName", Desc = "服务单类型名称", ContextType = SysDic.All, Length = 200)]
        public virtual string CustomerServiceTypeName
        {
            get { return _customerServiceTypeName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for CustomerServiceTypeName", value, value.ToString());
                _customerServiceTypeName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "所属省份ID", ShortCode = "ProvinceID", Desc = "所属省份ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ProvinceID
        {
            get { return _provinceID; }
            set { _provinceID = value; }
        }

        [DataMember]
        [DataDesc(CName = "所属省份名称", ShortCode = "ProvinceName", Desc = "所属省份名称", ContextType = SysDic.All, Length = 200)]
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
        [DataDesc(CName = "服务单说明", ShortCode = "CustomerServiceMemo", Desc = "服务单说明", ContextType = SysDic.All, Length = 200)]
        public virtual string CustomerServiceMemo
        {
            get { return _customerServiceMemo; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for CustomerServiceMemo", value, value.ToString());
                _customerServiceMemo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "服务单问题描述", ShortCode = "ProblemMemo", Desc = "服务单问题描述", ContextType = SysDic.All, Length = -1)]
        public virtual string ProblemMemo
        {
            get { return _problemMemo; }
            set
            {
                _problemMemo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "分析及处理办法", ShortCode = "AnalysisTreatmentMethods", Desc = "分析及处理办法", ContextType = SysDic.All, Length = -1)]
        public virtual string AnalysisTreatmentMethods
        {
            get { return _analysisTreatmentMethods; }
            set
            {
                _analysisTreatmentMethods = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "服务单状态", ShortCode = "Status", Desc = "服务单状态", ContextType = SysDic.All, Length = 8)]
        public virtual long Status
        {
            get { return _status; }
            set { _status = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "现象分类ID", ShortCode = "PhenomenonTypeID", Desc = "现象分类ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? PhenomenonTypeID
        {
            get { return _phenomenonTypeID; }
            set { _phenomenonTypeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "现象分类名称", ShortCode = "PhenomenonTypeName", Desc = "现象分类名称", ContextType = SysDic.All, Length = 200)]
        public virtual string PhenomenonTypeName
        {
            get { return _phenomenonTypeName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for PhenomenonTypeName", value, value.ToString());
                _phenomenonTypeName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "原因分类ID", ShortCode = "ReasonTypeID", Desc = "原因分类ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReasonTypeID
        {
            get { return _reasonTypeID; }
            set { _reasonTypeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "原因分类名称", ShortCode = "ReasonTypeName", Desc = "原因分类名称", ContextType = SysDic.All, Length = 200)]
        public virtual string ReasonTypeName
        {
            get { return _reasonTypeName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ReasonTypeName", value, value.ToString());
                _reasonTypeName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "措施分类ID", ShortCode = "MeasuresTypeID", Desc = "措施分类ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? MeasuresTypeID
        {
            get { return _measuresTypeID; }
            set { _measuresTypeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "措施分类名称", ShortCode = "MeasuresTypeName", Desc = "措施分类名称", ContextType = SysDic.All, Length = 200)]
        public virtual string MeasuresTypeName
        {
            get { return _measuresTypeName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for MeasuresTypeName", value, value.ToString());
                _measuresTypeName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "相关功能ID", ShortCode = "ProgramFuncID", Desc = "相关功能ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ProgramFuncID
        {
            get { return _programFuncID; }
            set { _programFuncID = value; }
        }

        [DataMember]
        [DataDesc(CName = "相关功能名称", ShortCode = "ProgramFuncName", Desc = "相关功能名称", ContextType = SysDic.All, Length = 200)]
        public virtual string ProgramFuncName
        {
            get { return _programFuncName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ProgramFuncName", value, value.ToString());
                _programFuncName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "服务方式ID", ShortCode = "ServiceModeID", Desc = "服务方式ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ServiceModeID
        {
            get { return _serviceModeID; }
            set { _serviceModeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "服务方式名称", ShortCode = "ServiceModeName", Desc = "服务方式名称", ContextType = SysDic.All, Length = 200)]
        public virtual string ServiceModeName
        {
            get { return _serviceModeName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ServiceModeName", value, value.ToString());
                _serviceModeName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "服务受理人ID", ShortCode = "ServiceAcceptanceManID", Desc = "服务受理人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ServiceAcceptanceManID
        {
            get { return _serviceAcceptanceManID; }
            set { _serviceAcceptanceManID = value; }
        }

        [DataMember]
        [DataDesc(CName = "服务受理人", ShortCode = "ServiceAcceptanceMan", Desc = "服务受理人", ContextType = SysDic.All, Length = 200)]
        public virtual string ServiceAcceptanceMan
        {
            get { return _serviceAcceptanceMan; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ServiceAcceptanceMan", value, value.ToString());
                _serviceAcceptanceMan = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "服务受理时间", ShortCode = "ServiceAcceptanceDate", Desc = "服务受理时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ServiceAcceptanceDate
        {
            get { return _serviceAcceptanceDate; }
            set { _serviceAcceptanceDate = value; }
        }

        [DataMember]
        public virtual string ServiceAcceptanceDateString
        {
            get
            {
                if (this.ServiceAcceptanceDate.HasValue)
                {
                    return this.ServiceAcceptanceDate.Value.ToString("yyyy-MM-dd");
                }
                return "";
            }
            set { }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "服务回访人ID", ShortCode = "ServiceReturnManID", Desc = "服务回访人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ServiceReturnManID
        {
            get { return _serviceReturnManID; }
            set { _serviceReturnManID = value; }
        }

        [DataMember]
        [DataDesc(CName = "服务回访人", ShortCode = "ServiceReturnMan", Desc = "服务回访人", ContextType = SysDic.All, Length = 200)]
        public virtual string ServiceReturnMan
        {
            get { return _serviceReturnMan; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ServiceReturnMan", value, value.ToString());
                _serviceReturnMan = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "服务回访时间", ShortCode = "ServiceReturnDate", Desc = "服务回访时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ServiceReturnDate
        {
            get { return _serviceReturnDate; }
            set { _serviceReturnDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "服务被回访人", ShortCode = "ServiceReturnToMan", Desc = "服务被回访人", ContextType = SysDic.All, Length = 200)]
        public virtual string ServiceReturnToMan
        {
            get { return _serviceReturnToMan; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ServiceReturnToMan", value, value.ToString());
                _serviceReturnToMan = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "服务被回访人联系电话", ShortCode = "ServiceReturnToManPhone", Desc = "服务被回访人联系电话", ContextType = SysDic.All, Length = 200)]
        public virtual string ServiceReturnToManPhone
        {
            get { return _serviceReturnToManPhone; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ServiceReturnToManPhone", value, value.ToString());
                _serviceReturnToManPhone = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "服务回访备注", ShortCode = "ServiceReturnMemo", Desc = "服务回访备注", ContextType = SysDic.All, Length = -1)]
        public virtual string ServiceReturnMemo
        {
            get { return _serviceReturnMemo; }
            set
            {
                _serviceReturnMemo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "服务完成时间", ShortCode = "ServiceFinishDate", Desc = "服务完成时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ServiceFinishDate
        {
            get { return _serviceFinishDate; }
            set { _serviceFinishDate = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "用户满意度ID", ShortCode = "SatisfactionID", Desc = "用户满意度ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? SatisfactionID
        {
            get { return _satisfactionID; }
            set { _satisfactionID = value; }
        }

        [DataMember]
        [DataDesc(CName = "用户满意度名称", ShortCode = "SatisfactionName", Desc = "用户满意度名称", ContextType = SysDic.All, Length = 200)]
        public virtual string SatisfactionName
        {
            get { return _satisfactionName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for SatisfactionName", value, value.ToString());
                _satisfactionName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "完成情况ID", ShortCode = "CompletionStatusID", Desc = "完成情况ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? CompletionStatusID
        {
            get { return _completionStatusID; }
            set { _completionStatusID = value; }
        }

        [DataMember]
        [DataDesc(CName = "完成情况名称", ShortCode = "CompletionStatusName", Desc = "完成情况名称", ContextType = SysDic.All, Length = 200)]
        public virtual string CompletionStatusName
        {
            get { return _completionStatusName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for CompletionStatusName", value, value.ToString());
                _completionStatusName = value;
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
        [DataDesc(CName = "服务最终处理人ID", ShortCode = "ServiceOperationCompleteManID", Desc = "服务最终处理人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long ServiceOperationCompleteManID
        {
            get { return _serviceOperationCompleteManID; }
            set { _serviceOperationCompleteManID = value; }
        }

        [DataMember]
        [DataDesc(CName = "服务最终处理人", ShortCode = "ServiceOperationCompleteMan", Desc = "服务最终处理人", ContextType = SysDic.All, Length = 200)]
        public virtual string ServiceOperationCompleteMan
        {
            get { return _serviceOperationCompleteMan; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for ServiceOperationCompleteMan", value, value.ToString());
                _serviceOperationCompleteMan = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "服务最终处理说明", ShortCode = "ServiceOperationCompleteMemo", Desc = "服务最终处理说明", ContextType = SysDic.All, Length = -1)]
        public virtual string ServiceOperationCompleteMemo
        {
            get { return _serviceOperationCompleteMemo; }
            set
            {
                _serviceOperationCompleteMemo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "是否是代理请求", ShortCode = "IsProxy", Desc = "是否是代理请求", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsProxy
        {
            get { return _isProxy; }
            set { _isProxy = value; }
        }

        [DataMember]
        [DataDesc(CName = "请求人", ShortCode = "RequestMan", Desc = "请求人", ContextType = SysDic.All, Length = 20)]
        public virtual string RequestMan
        {
            get { return _requestMan; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for RequestMan", value, value.ToString());
                _requestMan = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "请求人联系电话", ShortCode = "RequestManPhone", Desc = "请求人联系电话", ContextType = SysDic.All, Length = 20)]
        public virtual string RequestManPhone
        {
            get { return _requestManPhone; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for RequestManPhone", value, value.ToString());
                _requestManPhone = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "服务登记人ID", ShortCode = "ServiceRegisterManID", Desc = "服务登记人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long ServiceRegisterManID
        {
            get { return _serviceRegisterManID; }
            set { _serviceRegisterManID = value; }
        }

        [DataMember]
        [DataDesc(CName = "服务登记人", ShortCode = "ServiceRegisterMan", Desc = "服务登记人", ContextType = SysDic.All, Length = 200)]
        public virtual string ServiceRegisterMan
        {
            get { return _serviceRegisterMan; }
            set
            {
                _serviceRegisterMan = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "服务登记时间", ShortCode = "ServiceRegisterDate", Desc = "服务登记时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ServiceRegisterDate
        {
            get { return _serviceRegisterDate; }
            set { _serviceRegisterDate = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "服务处理时间", ShortCode = "ServiceOperationDate", Desc = "服务处理时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ServiceOperationDate
        {
            get { return _serviceOperationDate; }
            set { _serviceOperationDate = value; }
        }
        [DataMember]
        [DataDesc(CName = "状态名称", ShortCode = "StatusCName", Desc = "状态名称", ContextType = SysDic.All, Length = 50)]
        public virtual string StatusCName
        {
            get { return _statusCName; }
            set
            {
                _componeName = value;
            }
        }
        #endregion
    }
    #endregion
}