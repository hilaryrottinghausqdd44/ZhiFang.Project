using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LIIP
{
    #region SCMsg

    /// <summary>
    /// SCMsg object for NHibernate mapped table 'SC_Msg'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "公共消息表", ClassCName = "SCMsg", ShortCode = "SCMsg", Desc = "公共消息表")]
    public class SCMsg : BaseEntityService
    {
        #region Member Variables

        protected string _msgContent;
        protected long _msgTypeID;
        protected string _msgTypeName;
        protected string _msgTypeCode;
        protected long? _systemID;
        protected string _systemCName;
        protected string _systemCode;
        protected short _msgLevel;
        protected string _sendNodeName;
        protected string _sendIPAddress;
        protected long _senderID;
        protected string _senderName;
        protected string _recNodeName;
        protected string _recIPAddress;
        protected long _recSectionID;
        protected string _recSectionName;
        protected long _recLabID;
        protected string _recLabName;
        protected long _recDeptID;
        protected string _recDeptName;
        protected long _receiverID;
        protected string _receiverName;
        protected long _recSickTypeID;
        protected string _recSickTypeName;
        protected long _recDistrictID;
        protected string _recDistrictName;
        protected long _recDoctorID;
        protected string _recDoctorName;
        protected DateTime? _requireReplyTime;
        protected long _unRecSectorTypeID;
        protected string _unRecSectorTypeName;
        protected long _readFlag;
        protected DateTime? _confirmDateTime;
        protected string _confirmMemo;
        protected long _confirmFlag;
        protected DateTime? _requireConfirmTime;
        protected long _confirmNotifyDoctorID;
        protected string _confirmIPAddress;
        protected long _confirmerID;
        protected string _confirmerName;
        protected string _recDeptPhoneCode;
        protected long _warningUploaderFlag;
        protected long _warningUploaderID;
        protected string _warningUploaderName;
        protected long _warningUpLoadNotifyNurseID;
        protected string _warningUpLoadNotifyNurseName;
        protected DateTime? _warningUpLoadDateTime;
        protected long _loginReadUserID;
        protected string _loginReadUserName;
        protected DateTime? _loginReadDateTime;
        protected long _sendToMsgCentre;
        protected long _handlingFlag;
        protected string _handlingNodeName;
        protected DateTime? _handlingDateTime;
        protected DateTime? _requireHandleTime;
        protected long _handleFlag;
        protected DateTime? _firstHandleDateTime;
        protected long _timeOutCallFlag;
        protected long _timeOutCallUserID;
        protected string _timeOutCallUserName;
        protected long _timeOutCallRecUserID;
        protected string _timeOutCallRecUserName;
        protected DateTime? _timeOutCallDateTime;
        protected long _sendToWebService;
        protected long _sendToHisFlag;
        protected long _sendLinkMsg;
        protected string _createToCheckTakeTime;
        protected long _sendToPhone;
        protected string _memo;
        protected int _dispOrder;
        protected long _rebackerID;
        protected string _rebackerName;
        protected DateTime? _rebackTime;
        protected string _rebackMemo;
        protected long _rebackFalg;
        protected bool _isUse;
        protected long _creatorID;
        protected string _creatorName;
        protected DateTime? _dataUpdateTime;
        private long? _SendSectionID;
        private string _SendSectionName;
        private string _RecDeptCode;
        private string _RecDeptCodeHIS;
        private string _RecDoctorCodeHIS;
        private string _RecDoctorCode;
        private string _WarningUpLoadMemo;
        private string _WarningUpLoadNotifyNurseCode;
        private string _WarningUpLoadNotifyNurseCodeHIS;
        private string _ConfirmerCode;
        private string _ConfirmerCodeHIS;
        private string _ConfirmNotifyDoctorCodeHIS;
        private string _ConfirmNotifyDoctorCode;
        private string _ConfirmNotifyDoctorName;
        private string _SenderAccount;
        private long _ConfirmDeptID;
        private string _ConfirmDeptName;
        private string _ConfirmDeptCode;
        private string _ConfirmDeptCodeHIS;
        private string _TestItemNoList;
        private string _TestItemNameList;

        #endregion

        #region Constructors

        public SCMsg() { }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "消息内容", ShortCode = "MsgContent", Desc = "消息内容", ContextType = SysDic.All, Length = 5000)]
        public virtual string MsgContent
        {
            get { return _msgContent; }
            set
            {
                if (value != null && value.Length > 5000)
                    throw new ArgumentOutOfRangeException("Invalid value for MsgContent", value, value.ToString());
                _msgContent = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "消息类型ID", ShortCode = "MsgTypeID", Desc = "消息类型ID", ContextType = SysDic.All, Length = 8)]
        public virtual long MsgTypeID
        {
            get { return _msgTypeID; }
            set { _msgTypeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "消息类型名称", ShortCode = "MsgTypeName", Desc = "消息类型名称", ContextType = SysDic.All, Length = 100)]
        public virtual string MsgTypeName
        {
            get { return _msgTypeName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for MsgTypeName", value, value.ToString());
                _msgTypeName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "消息类型代码", ShortCode = "MsgTypeCode", Desc = "消息类型代码", ContextType = SysDic.All, Length = 50)]
        public virtual string MsgTypeCode
        {
            get { return _msgTypeCode; }
            set { _msgTypeCode = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "所属系统ID", ShortCode = "SystemID", Desc = "所属系统ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? SystemID
        {
            get { return _systemID; }
            set { _systemID = value; }
        }

        [DataMember]
        [DataDesc(CName = "所属系统名称", ShortCode = "SystemCName", Desc = "所属系统名称", ContextType = SysDic.All, Length = 100)]
        public virtual string SystemCName
        {
            get { return _systemCName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for SystemCName", value, value.ToString());
                _systemCName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "所属系统代码", ShortCode = "SystemCode", Desc = "所属系统代码", ContextType = SysDic.All, Length = 50)]
        public virtual string SystemCode
        {
            get { return _systemCode; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for SystemCode", value, value.ToString());
                _systemCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "消息级别", ShortCode = "MsgLevel", Desc = "消息级别", ContextType = SysDic.All, Length = 2)]
        public virtual short MsgLevel
        {
            get { return _msgLevel; }
            set { _msgLevel = value; }
        }

        [DataMember]
        [DataDesc(CName = "发送站点名称", ShortCode = "SendNodeName", Desc = "发送站点名称", ContextType = SysDic.All, Length = 100)]
        public virtual string SendNodeName
        {
            get { return _sendNodeName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for SendNodeName", value, value.ToString());
                _sendNodeName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "发送IP地址", ShortCode = "SendIPAddress", Desc = "发送IP地址", ContextType = SysDic.All, Length = 20)]
        public virtual string SendIPAddress
        {
            get { return _sendIPAddress; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for SendIPAddress", value, value.ToString());
                _sendIPAddress = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "消息发送者ID", ShortCode = "SenderID", Desc = "消息发送者ID", ContextType = SysDic.All, Length = 8)]
        public virtual long SenderID
        {
            get { return _senderID; }
            set { _senderID = value; }
        }

        [DataMember]
        [DataDesc(CName = "消息发送者姓名", ShortCode = "SenderName", Desc = "消息发送者姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string SenderName
        {
            get { return _senderName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for SenderName", value, value.ToString());
                _senderName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "消息发送小组ID", ShortCode = "SendSectionID", Desc = "消息发送小组ID", ContextType = SysDic.All, Length = 50)]
        public virtual long? SendSectionID
        {
            get { return _SendSectionID; }
            set
            {
                _SendSectionID = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "发送小组名称", ShortCode = "SendSectionName", Desc = "发送小组名称", ContextType = SysDic.All, Length = 50)]
        public virtual string SendSectionName
        {
            get { return _SendSectionName; }
            set
            {
                _SendSectionName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "接收站点名称", ShortCode = "RecNodeName", Desc = "接收站点名称", ContextType = SysDic.All, Length = 100)]
        public virtual string RecNodeName
        {
            get { return _recNodeName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for RecNodeName", value, value.ToString());
                _recNodeName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "接收IP地址", ShortCode = "RecIPAddress", Desc = "接收IP地址", ContextType = SysDic.All, Length = 20)]
        public virtual string RecIPAddress
        {
            get { return _recIPAddress; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for RecIPAddress", value, value.ToString());
                _recIPAddress = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "接收小组ID", ShortCode = "RecSectionID", Desc = "接收小组ID", ContextType = SysDic.All, Length = 8)]
        public virtual long RecSectionID
        {
            get { return _recSectionID; }
            set { _recSectionID = value; }
        }

        [DataMember]
        [DataDesc(CName = "接收小组姓名", ShortCode = "RecSectionName", Desc = "接收小组姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string RecSectionName
        {
            get { return _recSectionName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for RecSectionName", value, value.ToString());
                _recSectionName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "接收机构ID", ShortCode = "RecLabID", Desc = "接收机构ID", ContextType = SysDic.All, Length = 8)]
        public virtual long RecLabID
        {
            get { return _recLabID; }
            set { _recLabID = value; }
        }

        [DataMember]
        [DataDesc(CName = "接收机构名称", ShortCode = "RecLabName", Desc = "接收机构名称", ContextType = SysDic.All, Length = 100)]
        public virtual string RecLabName
        {
            get { return _recLabName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for RecLabName", value, value.ToString());
                _recLabName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "接收科室ID", ShortCode = "RecDeptID", Desc = "接收科室ID", ContextType = SysDic.All, Length = 8)]
        public virtual long RecDeptID
        {
            get { return _recDeptID; }
            set { _recDeptID = value; }
        }

        [DataMember]
        [DataDesc(CName = "接收科室名称", ShortCode = "RecDeptName", Desc = "接收科室名称", ContextType = SysDic.All, Length = 100)]
        public virtual string RecDeptName
        {
            get { return _recDeptName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for RecDeptName", value, value.ToString());
                _recDeptName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "接收科室编码", ShortCode = "RecDeptCode", Desc = "接收科室编码", ContextType = SysDic.All, Length = 100)]
        public virtual string RecDeptCode
        {
            get { return _RecDeptCode; }
            set
            {
                _RecDeptCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "接收科室编码HIS", ShortCode = "RecDeptCodeHIS", Desc = "接收科室编码HIS", ContextType = SysDic.All, Length = 100)]
        public virtual string RecDeptCodeHIS
        {
            get { return _RecDeptCodeHIS; }
            set
            {
                _RecDeptCodeHIS = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "接收者ID", ShortCode = "ReceiverID", Desc = "接收者ID", ContextType = SysDic.All, Length = 8)]
        public virtual long ReceiverID
        {
            get { return _receiverID; }
            set { _receiverID = value; }
        }

        [DataMember]
        [DataDesc(CName = "接收者姓名", ShortCode = "ReceiverName", Desc = "接收者姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string ReceiverName
        {
            get { return _receiverName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ReceiverName", value, value.ToString());
                _receiverName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "消息接收就诊类型编号", ShortCode = "RecSickTypeID", Desc = "消息接收就诊类型编号", ContextType = SysDic.All, Length = 8)]
        public virtual long RecSickTypeID
        {
            get { return _recSickTypeID; }
            set { _recSickTypeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "消息接收就诊类型名称", ShortCode = "RecSickTypeName", Desc = "消息接收就诊类型名称", ContextType = SysDic.All, Length = 100)]
        public virtual string RecSickTypeName
        {
            get { return _recSickTypeName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for RecSickTypeName", value, value.ToString());
                _recSickTypeName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "消息接收病区ID", ShortCode = "RecDistrictID", Desc = "消息接收病区ID", ContextType = SysDic.All, Length = 8)]
        public virtual long RecDistrictID
        {
            get { return _recDistrictID; }
            set { _recDistrictID = value; }
        }

        [DataMember]
        [DataDesc(CName = "消息接收病区名称", ShortCode = "RecDistrictName", Desc = "消息接收病区名称", ContextType = SysDic.All, Length = 100)]
        public virtual string RecDistrictName
        {
            get { return _recDistrictName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for RecDistrictName", value, value.ToString());
                _recDistrictName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "接收消息的医生ID", ShortCode = "RecDoctorID", Desc = "接收消息的医生ID", ContextType = SysDic.All, Length = 8)]
        public virtual long RecDoctorID
        {
            get { return _recDoctorID; }
            set { _recDoctorID = value; }
        }

        [DataMember]
        [DataDesc(CName = "接收消息的医生姓名", ShortCode = "RecDoctorName", Desc = "接收消息的医生姓名", ContextType = SysDic.All, Length = 100)]
        public virtual string RecDoctorName
        {
            get { return _recDoctorName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for RecDoctorName", value, value.ToString());
                _recDoctorName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "接收消息的医生编码HIS", ShortCode = "RecDoctorCodeHIS", Desc = "接收消息的医生编码HIS", ContextType = SysDic.All, Length = 100)]
        public virtual string RecDoctorCodeHIS
        {
            get { return _RecDoctorCodeHIS; }
            set
            {
                _RecDoctorCodeHIS = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "接收消息的医生编码", ShortCode = "RecDoctorCode", Desc = "接收消息的医生编码", ContextType = SysDic.All, Length = 100)]
        public virtual string RecDoctorCode
        {
            get { return _RecDoctorCode; }
            set
            {
                _RecDoctorCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "要求回复时间", ShortCode = "RequireReplyTime", Desc = "要求回复时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? RequireReplyTime
        {
            get { return _requireReplyTime; }
            set { _requireReplyTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "拒收消息接收实验类型ID", ShortCode = "UnRecSectorTypeID", Desc = "拒收消息接收实验类型ID", ContextType = SysDic.All, Length = 8)]
        public virtual long UnRecSectorTypeID
        {
            get { return _unRecSectorTypeID; }
            set { _unRecSectorTypeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "拒收消息接收实验类型名称", ShortCode = "UnRecSectorTypeName", Desc = "拒收消息接收实验类型名称", ContextType = SysDic.All, Length = 100)]
        public virtual string UnRecSectorTypeName
        {
            get { return _unRecSectorTypeName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for UnRecSectorTypeName", value, value.ToString());
                _unRecSectorTypeName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "已读标志，0未查阅，1已查阅。  ", ShortCode = "ReadFlag", Desc = "已读标志，0未查阅，1已查阅。  ", ContextType = SysDic.All, Length = 8)]
        public virtual long ReadFlag
        {
            get { return _readFlag; }
            set { _readFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "消息确认时间", ShortCode = "ConfirmDateTime", Desc = "消息确认时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ConfirmDateTime
        {
            get { return _confirmDateTime; }
            set { _confirmDateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "消息确认备注", ShortCode = "ConfirmMemo", Desc = "消息确认备注", ContextType = SysDic.All, Length = 5000)]
        public virtual string ConfirmMemo
        {
            get { return _confirmMemo; }
            set
            {
                if (value != null && value.Length > 5000)
                    throw new ArgumentOutOfRangeException("Invalid value for ConfirmMemo", value, value.ToString());
                _confirmMemo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "消息确认标志", ShortCode = "ConfirmFlag", Desc = "消息确认标志", ContextType = SysDic.All, Length = 8)]
        public virtual long ConfirmFlag
        {
            get { return _confirmFlag; }
            set { _confirmFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "要求确认时间", ShortCode = "RequireConfirmTime", Desc = "要求确认时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? RequireConfirmTime
        {
            get { return _requireConfirmTime; }
            set { _requireConfirmTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "确认时通知医生ID", ShortCode = "ConfirmNotifyDoctorID", Desc = "确认时通知医生ID", ContextType = SysDic.All, Length = 8)]
        public virtual long ConfirmNotifyDoctorID
        {
            get { return _confirmNotifyDoctorID; }
            set { _confirmNotifyDoctorID = value; }
        }

        [DataMember]
        [DataDesc(CName = "消息确认IP地址", ShortCode = "ConfirmIPAddress", Desc = "消息确认IP地址", ContextType = SysDic.All, Length = 20)]
        public virtual string ConfirmIPAddress
        {
            get { return _confirmIPAddress; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for ConfirmIPAddress", value, value.ToString());
                _confirmIPAddress = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "确认人ID", ShortCode = "ConfirmerID", Desc = "确认人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long ConfirmerID
        {
            get { return _confirmerID; }
            set { _confirmerID = value; }
        }

        [DataMember]
        [DataDesc(CName = "确认人姓名", ShortCode = "ConfirmerName", Desc = "确认人姓名", ContextType = SysDic.All, Length = 100)]
        public virtual string ConfirmerName
        {
            get { return _confirmerName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for ConfirmerName", value, value.ToString());
                _confirmerName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "接收科室电话", ShortCode = "RecDeptPhoneCode", Desc = "接收科室电话", ContextType = SysDic.All, Length = 50)]
        public virtual string RecDeptPhoneCode
        {
            get { return _recDeptPhoneCode; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for RecDeptPhoneCode", value, value.ToString());
                _recDeptPhoneCode = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "警告上报标志", ShortCode = "WarningUploaderFlag", Desc = "警告上报标志", ContextType = SysDic.All, Length = 8)]
        public virtual long WarningUploaderFlag
        {
            get { return _warningUploaderFlag; }
            set { _warningUploaderFlag = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "警告上报者ID", ShortCode = "WarningUploaderID", Desc = "警告上报者ID", ContextType = SysDic.All, Length = 8)]
        public virtual long WarningUploaderID
        {
            get { return _warningUploaderID; }
            set { _warningUploaderID = value; }
        }

        [DataMember]
        [DataDesc(CName = "警告上报者姓名", ShortCode = "WarningUploaderName", Desc = "警告上报者姓名", ContextType = SysDic.All, Length = 100)]
        public virtual string WarningUploaderName
        {
            get { return _warningUploaderName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for WarningUploaderName", value, value.ToString());
                _warningUploaderName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "警告上报时通知护士ID", ShortCode = "WarningUpLoadNotifyNurseID", Desc = "警告上报时通知护士ID", ContextType = SysDic.All, Length = 8)]
        public virtual long WarningUpLoadNotifyNurseID
        {
            get { return _warningUpLoadNotifyNurseID; }
            set { _warningUpLoadNotifyNurseID = value; }
        }

        [DataMember]
        [DataDesc(CName = "警告上报时通知护士编号", ShortCode = "WarningUpLoadNotifyNurseName", Desc = "警告上报时通知护士编号", ContextType = SysDic.All, Length = 100)]
        public virtual string WarningUpLoadNotifyNurseName
        {
            get { return _warningUpLoadNotifyNurseName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for WarningUpLoadNotifyNurseName", value, value.ToString());
                _warningUpLoadNotifyNurseName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "警告上报时间", ShortCode = "WarningUpLoadDateTime", Desc = "警告上报时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? WarningUpLoadDateTime
        {
            get { return _warningUpLoadDateTime; }
            set { _warningUpLoadDateTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "警告上报备注", ShortCode = "WarningUpLoadMemo", Desc = "警告上报备注", ContextType = SysDic.All, Length = 8)]
        public virtual string WarningUpLoadMemo
        {
            get { return _WarningUpLoadMemo; }
            set { _WarningUpLoadMemo = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "查阅人ID", ShortCode = "LoginReadUserID", Desc = "查阅人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long LoginReadUserID
        {
            get { return _loginReadUserID; }
            set { _loginReadUserID = value; }
        }

        [DataMember]
        [DataDesc(CName = "查阅人姓名", ShortCode = "LoginReadUserName", Desc = "查阅人姓名", ContextType = SysDic.All, Length = 100)]
        public virtual string LoginReadUserName
        {
            get { return _loginReadUserName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for LoginReadUserName", value, value.ToString());
                _loginReadUserName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "查阅时间", ShortCode = "LoginReadDateTime", Desc = "查阅时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? LoginReadDateTime
        {
            get { return _loginReadDateTime; }
            set { _loginReadDateTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "SendToMsgCentre", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long SendToMsgCentre
        {
            get { return _sendToMsgCentre; }
            set { _sendToMsgCentre = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "处理中标志", ShortCode = "HandlingFlag", Desc = "处理中标志", ContextType = SysDic.All, Length = 8)]
        public virtual long HandlingFlag
        {
            get { return _handlingFlag; }
            set { _handlingFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "处理中站点名称", ShortCode = "HandlingNodeName", Desc = "处理中站点名称", ContextType = SysDic.All, Length = 200)]
        public virtual string HandlingNodeName
        {
            get { return _handlingNodeName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for HandlingNodeName", value, value.ToString());
                _handlingNodeName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "处理时间", ShortCode = "HandlingDateTime", Desc = "处理时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? HandlingDateTime
        {
            get { return _handlingDateTime; }
            set { _handlingDateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "要求处理时间", ShortCode = "RequireHandleTime", Desc = "要求处理时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? RequireHandleTime
        {
            get { return _requireHandleTime; }
            set { _requireHandleTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "处理标志", ShortCode = "HandleFlag", Desc = "处理标志", ContextType = SysDic.All, Length = 8)]
        public virtual long HandleFlag
        {
            get { return _handleFlag; }
            set { _handleFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "第一次处理消息时间", ShortCode = "FirstHandleDateTime", Desc = "第一次处理消息时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? FirstHandleDateTime
        {
            get { return _firstHandleDateTime; }
            set { _firstHandleDateTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "超时未处理消息时提示标志", ShortCode = "TimeOutCallFlag", Desc = "超时未处理消息时提示标志", ContextType = SysDic.All, Length = 8)]
        public virtual long TimeOutCallFlag
        {
            get { return _timeOutCallFlag; }
            set { _timeOutCallFlag = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "超时未处理消息时提示人ID", ShortCode = "TimeOutCallUserID", Desc = "超时未处理消息时提示人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long TimeOutCallUserID
        {
            get { return _timeOutCallUserID; }
            set { _timeOutCallUserID = value; }
        }

        [DataMember]
        [DataDesc(CName = "超时未处理消息时提示人名称", ShortCode = "TimeOutCallUserName", Desc = "超时未处理消息时提示人名称", ContextType = SysDic.All, Length = 100)]
        public virtual string TimeOutCallUserName
        {
            get { return _timeOutCallUserName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for TimeOutCallUserName", value, value.ToString());
                _timeOutCallUserName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "超时未处理消息时提示接收人ID", ShortCode = "TimeOutCallRecUserID", Desc = "超时未处理消息时提示接收人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long TimeOutCallRecUserID
        {
            get { return _timeOutCallRecUserID; }
            set { _timeOutCallRecUserID = value; }
        }

        [DataMember]
        [DataDesc(CName = "超时未处理消息时提示接收人姓名", ShortCode = "TimeOutCallRecUserName", Desc = "超时未处理消息时提示接收人姓名", ContextType = SysDic.All, Length = 100)]
        public virtual string TimeOutCallRecUserName
        {
            get { return _timeOutCallRecUserName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for TimeOutCallRecUserName", value, value.ToString());
                _timeOutCallRecUserName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "超时未处理消息时提示时间", ShortCode = "TimeOutCallDateTime", Desc = "超时未处理消息时提示时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? TimeOutCallDateTime
        {
            get { return _timeOutCallDateTime; }
            set { _timeOutCallDateTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "SendToWebService", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long SendToWebService
        {
            get { return _sendToWebService; }
            set { _sendToWebService = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "发送到His数据库标志   0未发送   1已发送   ", ShortCode = "SendToHisFlag", Desc = "发送到His数据库标志   0未发送   1已发送   ", ContextType = SysDic.All, Length = 8)]
        public virtual long SendToHisFlag
        {
            get { return _sendToHisFlag; }
            set { _sendToHisFlag = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "发送关联危机值标志", ShortCode = "SendLinkMsg", Desc = "发送关联危机值标志", ContextType = SysDic.All, Length = 8)]
        public virtual long SendLinkMsg
        {
            get { return _sendLinkMsg; }
            set { _sendLinkMsg = value; }
        }

        [DataMember]
        [DataDesc(CName = "产生结果到审核时间", ShortCode = "CreateToCheckTakeTime", Desc = "产生结果到审核时间", ContextType = SysDic.All, Length = 100)]
        public virtual string CreateToCheckTakeTime
        {
            get { return _createToCheckTakeTime; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for CreateToCheckTakeTime", value, value.ToString());
                _createToCheckTakeTime = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "发送到手机标志", ShortCode = "SendToPhone", Desc = "发送到手机标志", ContextType = SysDic.All, Length = 8)]
        public virtual long SendToPhone
        {
            get { return _sendToPhone; }
            set { _sendToPhone = value; }
        }

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = 500)]
        public virtual string Memo
        {
            get { return _memo; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for Memo", value, value.ToString());
                _memo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "撤回人ID", ShortCode = "RebackerID", Desc = "撤回人ID", ContextType = SysDic.All, Length = 8)]
        public virtual long RebackerID
        {
            get { return _rebackerID; }
            set { _rebackerID = value; }
        }

        [DataMember]
        [DataDesc(CName = "撤回人名称", ShortCode = "RebackerName", Desc = "撤回人名称", ContextType = SysDic.All, Length = 20)]
        public virtual string RebackerName
        {
            get { return _rebackerName; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for RebackerName", value, value.ToString());
                _rebackerName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "撤回时间", ShortCode = "RebackTime", Desc = "撤回时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? RebackTime
        {
            get { return _rebackTime; }
            set { _rebackTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "撤回原因", ShortCode = "RebackMemo", Desc = "撤回原因", ContextType = SysDic.All, Length = 500)]
        public virtual string RebackMemo
        {
            get { return _rebackMemo; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for RebackMemo", value, value.ToString());
                _rebackMemo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "撤回标志", ShortCode = "RebackFalg", Desc = "撤回标志", ContextType = SysDic.All, Length = 8)]
        public virtual long RebackFalg
        {
            get { return _rebackFalg; }
            set { _rebackFalg = value; }
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
        [DataDesc(CName = "创建者", ShortCode = "CreatorID", Desc = "创建者", ContextType = SysDic.All, Length = 8)]
        public virtual long CreatorID
        {
            get { return _creatorID; }
            set { _creatorID = value; }
        }

        [DataMember]
        [DataDesc(CName = "创建者姓名", ShortCode = "CreatorName", Desc = "创建者姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string CreatorName
        {
            get { return _creatorName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for CreatorName", value, value.ToString());
                _creatorName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "数据修改时间", ShortCode = "DataUpdateTime", Desc = "数据修改时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "警告上报时通知护士编码", ShortCode = "WarningUpLoadNotifyNurseCode", Desc = "警告上报时通知护士编码", ContextType = SysDic.All, Length = 8)]
        public virtual string WarningUpLoadNotifyNurseCode
        {
            get { return _WarningUpLoadNotifyNurseCode; }
            set { _WarningUpLoadNotifyNurseCode = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "警告上报时通知护士编码HIS", ShortCode = "WarningUpLoadNotifyNurseCodeHIS", Desc = "警告上报时通知护士编码HIS", ContextType = SysDic.All, Length = 8)]
        public virtual string WarningUpLoadNotifyNurseCodeHIS
        {
            get { return _WarningUpLoadNotifyNurseCodeHIS; }
            set { _WarningUpLoadNotifyNurseCodeHIS = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "确认人编码", ShortCode = "ConfirmerCode", Desc = "确认人编码", ContextType = SysDic.All, Length = 8)]
        public virtual string ConfirmerCode
        {
            get { return _ConfirmerCode; }
            set { _ConfirmerCode = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "确认人编码HIS", ShortCode = "ConfirmerCodeHIS", Desc = "确认人编码HIS", ContextType = SysDic.All, Length = 8)]
        public virtual string ConfirmerCodeHIS
        {
            get { return _ConfirmerCodeHIS; }
            set { _ConfirmerCodeHIS = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "确认时通知医生编码HIS", ShortCode = "ConfirmNotifyDoctorCodeHIS", Desc = "确认时通知医生编码HIS", ContextType = SysDic.All, Length = 8)]
        public virtual string ConfirmNotifyDoctorCodeHIS
        {
            get { return _ConfirmNotifyDoctorCodeHIS; }
            set { _ConfirmNotifyDoctorCodeHIS = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "确认时通知医生编码", ShortCode = "ConfirmNotifyDoctorCode", Desc = "确认时通知医生编码", ContextType = SysDic.All, Length = 8)]
        public virtual string ConfirmNotifyDoctorCode
        {
            get { return _ConfirmNotifyDoctorCode; }
            set { _ConfirmNotifyDoctorCode = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "确认时通知医生姓名", ShortCode = "ConfirmNotifyDoctorName", Desc = "确认时通知医生姓名", ContextType = SysDic.All, Length = 8)]
        public virtual string ConfirmNotifyDoctorName
        {
            get { return _ConfirmNotifyDoctorName; }
            set { _ConfirmNotifyDoctorName = value; }
        }

        [DataMember]
        [DataDesc(CName = "发送者帐号", ShortCode = "SenderAccount", Desc = "发送者帐号", ContextType = SysDic.All, Length = 8)]
        public virtual string SenderAccount
        {
            get { return _SenderAccount; }
            set { _SenderAccount = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "确认科室ID", ShortCode = "ConfirmDeptID", Desc = "确认科室ID", ContextType = SysDic.All, Length = 8)]
        public virtual long ConfirmDeptID
        {
            get { return _ConfirmDeptID; }
            set { _ConfirmDeptID = value; }
        }

        [DataMember]
        [DataDesc(CName = "确认科室名称", ShortCode = "ConfirmDeptName", Desc = "确认科室名称", ContextType = SysDic.All, Length = 100)]
        public virtual string ConfirmDeptName
        {
            get { return _ConfirmDeptName; }
            set
            {
                _ConfirmDeptName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "确认科室编码", ShortCode = "ConfirmDeptCode", Desc = "确认科室编码", ContextType = SysDic.All, Length = 100)]
        public virtual string ConfirmDeptCode
        {
            get { return _ConfirmDeptCode; }
            set
            {
                _ConfirmDeptCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "确认科室编码HIS", ShortCode = "ConfirmDeptCodeHIS", Desc = "确认科室编码HIS", ContextType = SysDic.All, Length = 100)]
        public virtual string ConfirmDeptCodeHIS
        {
            get { return _ConfirmDeptCodeHIS; }
            set
            {
                _ConfirmDeptCodeHIS = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "检验项目编码", ShortCode = "TestItemNoList", Desc = "检验项目编码", ContextType = SysDic.All, Length = 100)]
        public virtual string TestItemNoList
        {
            get { return _TestItemNoList; }
            set
            {
                _TestItemNoList = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "检验项目名称", ShortCode = "TestItemNameList", Desc = "检验项目名称", ContextType = SysDic.All, Length = 100)]
        public virtual string TestItemNameList
        {
            get { return _TestItemNameList; }
            set
            {
                _TestItemNameList = value;
            }
        }

        #endregion
    }
    #endregion
}