//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.1026
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
//     
//     说明：当前文件用于调用HIS接口核收
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZhiFang.Digitlab.Entity.LisInterface
{
    using System.Runtime.Serialization;
    using System;


    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "RetrieveObject", Namespace = "http://schemas.datacontract.org/2004/07/ZhiFang.Digitlab.Model")]
    [System.SerializableAttribute()]
    public partial class RetrieveObject : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged
    {

        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string QueryColumnNameField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string QueryColumnValueField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SickTypeIDField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string WhereClauseField;

        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string QueryColumnName
        {
            get
            {
                return this.QueryColumnNameField;
            }
            set
            {
                if ((object.ReferenceEquals(this.QueryColumnNameField, value) != true))
                {
                    this.QueryColumnNameField = value;
                    this.RaisePropertyChanged("QueryColumnName");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string QueryColumnValue
        {
            get
            {
                return this.QueryColumnValueField;
            }
            set
            {
                if ((object.ReferenceEquals(this.QueryColumnValueField, value) != true))
                {
                    this.QueryColumnValueField = value;
                    this.RaisePropertyChanged("QueryColumnValue");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SickTypeID
        {
            get
            {
                return this.SickTypeIDField;
            }
            set
            {
                if ((object.ReferenceEquals(this.SickTypeIDField, value) != true))
                {
                    this.SickTypeIDField = value;
                    this.RaisePropertyChanged("SickTypeID");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string WhereClause
        {
            get
            {
                return this.WhereClauseField;
            }
            set
            {
                if ((object.ReferenceEquals(this.WhereClauseField, value) != true))
                {
                    this.WhereClauseField = value;
                    this.RaisePropertyChanged("WhereClause");
                }
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "BaseResult", Namespace = "http://schemas.datacontract.org/2004/07/ZhiFang.Digitlab.Model")]
    [System.SerializableAttribute()]
    public partial class BaseResult : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged
    {

        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ErrorInfoField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool successField;

        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ErrorInfo
        {
            get
            {
                return this.ErrorInfoField;
            }
            set
            {
                if ((object.ReferenceEquals(this.ErrorInfoField, value) != true))
                {
                    this.ErrorInfoField = value;
                    this.RaisePropertyChanged("ErrorInfo");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool success
        {
            get
            {
                return this.successField;
            }
            set
            {
                if ((this.successField.Equals(value) != true))
                {
                    this.successField = value;
                    this.RaisePropertyChanged("success");
                }
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "EnumFunction", Namespace = "http://schemas.datacontract.org/2004/07/ZhiFang.Common.Enums")]
    public enum EnumFunction : int
    {

        [System.Runtime.Serialization.EnumMemberAttribute()]
        AddFees = 1,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        DecreaseFees = 2,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        RemoveFees = 3,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        RefuseSample = 4,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ResetFlag = 5,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ReceiveFunction = 6,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        SendAfter = 7,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        CountNoses = 8,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ReceiveFlag = 9,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        CancelFlag = 10,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        SendAfterCancel = 11,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        NursePrintSetFlag = 12,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        NursePrintSetFlagCancel = 13,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        NurseReceiptSample = 14,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        RejectSample = 15,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ReceiptSample = 16,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        NurseCollectSampleSetFlag = 17,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        NurseSendSampleSetFlag = 18,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        NurseReceiveSampleSetFlag = 19,
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "SearchObject", Namespace = "http://schemas.datacontract.org/2004/07/ZhiFang.Digitlab.Model")]
    [System.SerializableAttribute()]
    public partial class SearchObject : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged
    {

        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long GroupSampleFormIDField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PatNoField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long SickTypeIDField;

        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public long GroupSampleFormID
        {
            get
            {
                return this.GroupSampleFormIDField;
            }
            set
            {
                if ((this.GroupSampleFormIDField.Equals(value) != true))
                {
                    this.GroupSampleFormIDField = value;
                    this.RaisePropertyChanged("GroupSampleFormID");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PatNo
        {
            get
            {
                return this.PatNoField;
            }
            set
            {
                if ((object.ReferenceEquals(this.PatNoField, value) != true))
                {
                    this.PatNoField = value;
                    this.RaisePropertyChanged("PatNo");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public long SickTypeID
        {
            get
            {
                return this.SickTypeIDField;
            }
            set
            {
                if ((this.SickTypeIDField.Equals(value) != true))
                {
                    this.SickTypeIDField = value;
                    this.RaisePropertyChanged("SickTypeID");
                }
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace = "", ConfigurationName = "LisInterface.LIS")]
    public interface LIS
    {

        [System.ServiceModel.OperationContractAttribute(Action = "urn:LIS/ReceivePatientInfo", ReplyAction = "urn:LIS/ReceivePatientInfoResponse")]
        ZhiFang.Digitlab.Entity.LisInterface.BaseResult ReceivePatientInfo(ZhiFang.Digitlab.Entity.LisInterface.RetrieveObject obj);

        [System.ServiceModel.OperationContractAttribute(Action = "urn:LIS/RetrieveFromLabStation", ReplyAction = "urn:LIS/RetrieveFromLabStationResponse")]
        ZhiFang.Digitlab.Entity.LisInterface.BaseResult RetrieveFromLabStation(ZhiFang.Digitlab.Entity.LisInterface.RetrieveObject obj);

        [System.ServiceModel.OperationContractAttribute(Action = "urn:LIS/RetrieveFromNurseStation", ReplyAction = "urn:LIS/RetrieveFromNurseStationResponse")]
        ZhiFang.Digitlab.Entity.LisInterface.BaseResult RetrieveFromNurseStation(ZhiFang.Digitlab.Entity.LisInterface.RetrieveObject obj);

        [System.ServiceModel.OperationContractAttribute(Action = "urn:LIS/RetrieveFromMeptStation", ReplyAction = "urn:LIS/RetrieveFromMeptStationResponse")]
        ZhiFang.Digitlab.Entity.LisInterface.BaseResult RetrieveFromMeptStation(ZhiFang.Digitlab.Entity.LisInterface.RetrieveObject obj);

        [System.ServiceModel.OperationContractAttribute(Action = "urn:LIS/SetFlagByName", ReplyAction = "urn:LIS/SetFlagByNameResponse")]
        ZhiFang.Digitlab.Entity.LisInterface.BaseResult SetFlagByName(string funcName, string strMsg);

        [System.ServiceModel.OperationContractAttribute(Action = "urn:LIS/SetFlagByEnum", ReplyAction = "urn:LIS/SetFlagByEnumResponse")]
        ZhiFang.Digitlab.Entity.LisInterface.BaseResult SetFlagByEnum(ZhiFang.Digitlab.Entity.LisInterface.EnumFunction enumFunc, string strMsg);

        [System.ServiceModel.OperationContractAttribute(Action = "urn:LIS/SendAfter", ReplyAction = "urn:LIS/SendAfterResponse")]
        ZhiFang.Digitlab.Entity.LisInterface.BaseResult SendAfter(ZhiFang.Digitlab.Entity.LisInterface.SearchObject obj);

        [System.ServiceModel.OperationContractAttribute(Action = "urn:LIS/QueryInterface", ReplyAction = "urn:LIS/QueryInterfaceResponse")]
        bool QueryInterface(ZhiFang.Digitlab.Entity.LisInterface.SearchObject obj);
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface LISChannel : ZhiFang.Digitlab.Entity.LisInterface.LIS, System.ServiceModel.IClientChannel
    {
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class LISClient : System.ServiceModel.ClientBase<ZhiFang.Digitlab.Entity.LisInterface.LIS>, ZhiFang.Digitlab.Entity.LisInterface.LIS
    {

        public LISClient()
        {
        }

        public LISClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public LISClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public LISClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public LISClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        public ZhiFang.Digitlab.Entity.LisInterface.BaseResult ReceivePatientInfo(ZhiFang.Digitlab.Entity.LisInterface.RetrieveObject obj)
        {
            return base.Channel.ReceivePatientInfo(obj);
        }

        public ZhiFang.Digitlab.Entity.LisInterface.BaseResult RetrieveFromLabStation(ZhiFang.Digitlab.Entity.LisInterface.RetrieveObject obj)
        {
            return base.Channel.RetrieveFromLabStation(obj);
        }

        public ZhiFang.Digitlab.Entity.LisInterface.BaseResult RetrieveFromNurseStation(ZhiFang.Digitlab.Entity.LisInterface.RetrieveObject obj)
        {
            return base.Channel.RetrieveFromNurseStation(obj);
        }

        public ZhiFang.Digitlab.Entity.LisInterface.BaseResult RetrieveFromMeptStation(ZhiFang.Digitlab.Entity.LisInterface.RetrieveObject obj)
        {
            return base.Channel.RetrieveFromMeptStation(obj);
        }

        public ZhiFang.Digitlab.Entity.LisInterface.BaseResult SetFlagByName(string funcName, string strMsg)
        {
            return base.Channel.SetFlagByName(funcName, strMsg);
        }

        public ZhiFang.Digitlab.Entity.LisInterface.BaseResult SetFlagByEnum(ZhiFang.Digitlab.Entity.LisInterface.EnumFunction enumFunc, string strMsg)
        {
            return base.Channel.SetFlagByEnum(enumFunc, strMsg);
        }

        public ZhiFang.Digitlab.Entity.LisInterface.BaseResult SendAfter(ZhiFang.Digitlab.Entity.LisInterface.SearchObject obj)
        {
            return base.Channel.SendAfter(obj);
        }

        public bool QueryInterface(ZhiFang.Digitlab.Entity.LisInterface.SearchObject obj)
        {
            return base.Channel.QueryInterface(obj);
        }
    }
}