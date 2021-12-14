using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
    #region MEPTOrderForm

    /// <summary>
    /// MEPTOrderForm object for NHibernate mapped table 'MEPT_OrderForm'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "医嘱单", ClassCName = "MEPTOrderForm", ShortCode = "MEPTOrderForm", Desc = "医嘱单")]
    public class MEPTOrderForm : BaseEntity
    {
        #region Member Variables

        protected string _serialNo;
        protected string _oldSerialNo;
        protected string _patCardNo;
        protected string _inpatientNo;
        protected string _patientID;
        protected string _patNo;
        protected string _cName;
        protected DateTime? _birthday;
        protected double _age;
        protected string _bed;
        protected string _doctor;
        protected string _diag;
        protected int _isFree;
        protected decimal _charge;
        protected string _formMemo;
        protected int _isByHand;
        protected string _collectPart;
        protected string _testAim;
        protected string _zDY1;
        protected string _zDY2;
        protected string _zDY3;
        protected string _zDY4;
        protected string _zDY5;
        protected DateTime? _dataUpdateTime;
        protected bool _chargeFlag;
        protected string _zDY6;
        protected string _zDY7;
        protected string _zDY8;
        protected string _zDY9;
        protected string _zDY10;
        protected BMedicalDepartment _exec;
        protected BDiseaseArea _bDiseaseArea;
        protected BDiseaseRoom _bDiseaseRoom;
        protected BDoctor _bDoctor;
        protected BMedicalDepartment _bMedicalDepartment;
        protected MEPTBCharge _mEPTBCharge;
        protected BLaboratory _client;
        protected BDiag _bDiag;
        protected BLaboratory _bLaboratory;
        protected BPatientInfo _bPatientInfo;
        protected MEPTBSampleSource _mEPTBSampleSource;
        protected BSampleType _bSampleType;
        protected BSickType _bSickType;
        protected BSpecialty _bSpecialty;
        protected BTestType _bTestType;
        protected BSampleStatus _bSampleStatus;
        protected BAgeUnit _bAgeUnit;
        protected IList<MEChargeInfo> _mEChargeInfoList;
        protected IList<MEGroupSampleForm> _mEGroupSampleFormList;
        protected IList<MEPTOrderItem> _mEPTOrderItemList;
        protected IList<MEPTSampleForm> _mEPTSampleFormList;

        #endregion

        #region Constructors

        public MEPTOrderForm() { }

        public MEPTOrderForm(string serialNo, string oldSerialNo, string patCardNo, string inpatientNo, string patientID, string patNo, string cName, DateTime birthday, double age, string bed, string doctor, string diag, int isFree, decimal charge, string formMemo, int isByHand, string collectPart, string testAim, string zDY1, string zDY2, string zDY3, string zDY4, string zDY5, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, bool chargeFlag, string zDY6, string zDY7, string zDY8, string zDY9, string zDY10, BMedicalDepartment exec, BDiseaseArea bDiseaseArea, BDiseaseRoom bDiseaseRoom, BDoctor bDoctor, BMedicalDepartment bMedicalDepartment, MEPTBCharge mEPTBCharge, BLaboratory client, BDiag bDiag, BLaboratory bLaboratory, BPatientInfo bPatientInfo, MEPTBSampleSource mEPTBSampleSource, BSampleType bSampleType, BSickType bSickType, BSpecialty bSpecialty, BTestType bTestType, BSampleStatus bSampleStatus, BAgeUnit bAgeUnit)
        {
            this._serialNo = serialNo;
            this._oldSerialNo = oldSerialNo;
            this._patCardNo = patCardNo;
            this._inpatientNo = inpatientNo;
            this._patientID = patientID;
            this._patNo = patNo;
            this._cName = cName;
            this._birthday = birthday;
            this._age = age;
            this._bed = bed;
            this._doctor = doctor;
            this._diag = diag;
            this._isFree = isFree;
            this._charge = charge;
            this._formMemo = formMemo;
            this._isByHand = isByHand;
            this._collectPart = collectPart;
            this._testAim = testAim;
            this._zDY1 = zDY1;
            this._zDY2 = zDY2;
            this._zDY3 = zDY3;
            this._zDY4 = zDY4;
            this._zDY5 = zDY5;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._chargeFlag = chargeFlag;
            this._zDY6 = zDY6;
            this._zDY7 = zDY7;
            this._zDY8 = zDY8;
            this._zDY9 = zDY9;
            this._zDY10 = zDY10;
            this._exec = exec;
            this._bDiseaseArea = bDiseaseArea;
            this._bDiseaseRoom = bDiseaseRoom;
            this._bDoctor = bDoctor;
            this._bMedicalDepartment = bMedicalDepartment;
            this._mEPTBCharge = mEPTBCharge;
            this._client = client;
            this._bDiag = bDiag;
            this._bLaboratory = bLaboratory;
            this._bPatientInfo = bPatientInfo;
            this._mEPTBSampleSource = mEPTBSampleSource;
            this._bSampleType = bSampleType;
            this._bSickType = bSickType;
            this._bSpecialty = bSpecialty;
            this._bTestType = bTestType;
            this._bSampleStatus = bSampleStatus;
            this._bAgeUnit = bAgeUnit;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "申请单号", ShortCode = "SerialNo", Desc = "申请单号", ContextType = SysDic.All, Length = 30)]
        public virtual string SerialNo
        {
            get { return _serialNo; }
            set
            {
                if (value != null && value.Length > 30)
                    throw new ArgumentOutOfRangeException("Invalid value for SerialNo", value, value.ToString());
                _serialNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "原始申请单号", ShortCode = "OldSerialNo", Desc = "原始申请单号", ContextType = SysDic.All, Length = 60)]
        public virtual string OldSerialNo
        {
            get { return _oldSerialNo; }
            set
            {
                if (value != null && value.Length > 60)
                    throw new ArgumentOutOfRangeException("Invalid value for OldSerialNo", value, value.ToString());
                _oldSerialNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "就诊卡号", ShortCode = "PatCardNo", Desc = "就诊卡号", ContextType = SysDic.All, Length = 20)]
        public virtual string PatCardNo
        {
            get { return _patCardNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for PatCardNo", value, value.ToString());
                _patCardNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "住院号", ShortCode = "InpatientNo", Desc = "住院号", ContextType = SysDic.All, Length = 20)]
        public virtual string InpatientNo
        {
            get { return _inpatientNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for InpatientNo", value, value.ToString());
                _inpatientNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "病人ID", ShortCode = "PatientID", Desc = "病人ID", ContextType = SysDic.All, Length = 20)]
        public virtual string PatientID
        {
            get { return _patientID; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for PatientID", value, value.ToString());
                _patientID = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "病历号", ShortCode = "PatNo", Desc = "病历号", ContextType = SysDic.All, Length = 20)]
        public virtual string PatNo
        {
            get { return _patNo; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for PatNo", value, value.ToString());
                _patNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "姓名", ShortCode = "CName", Desc = "姓名", ContextType = SysDic.All, Length = 40)]
        public virtual string CName
        {
            get { return _cName; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
                _cName = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "生日", ShortCode = "Birthday", Desc = "生日", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? Birthday
        {
            get { return _birthday; }
            set { _birthday = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "年龄", ShortCode = "Age", Desc = "年龄", ContextType = SysDic.All, Length = 8)]
        public virtual double Age
        {
            get { return _age; }
            set { _age = value; }
        }

        [DataMember]
        [DataDesc(CName = "病床", ShortCode = "Bed", Desc = "病床", ContextType = SysDic.All, Length = 10)]
        public virtual string Bed
        {
            get { return _bed; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for Bed", value, value.ToString());
                _bed = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "医生", ShortCode = "Doctor", Desc = "医生", ContextType = SysDic.All, Length = 50)]
        public virtual string Doctor
        {
            get { return _doctor; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Doctor", value, value.ToString());
                _doctor = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "临床诊断", ShortCode = "Diag", Desc = "临床诊断", ContextType = SysDic.All, Length = 100)]
        public virtual string Diag
        {
            get { return _diag; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Diag", value, value.ToString());
                _diag = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "是否免费", ShortCode = "IsFree", Desc = "是否免费", ContextType = SysDic.All, Length = 4)]
        public virtual int IsFree
        {
            get { return _isFree; }
            set { _isFree = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "费用", ShortCode = "Charge", Desc = "费用", ContextType = SysDic.All, Length = 8)]
        public virtual decimal Charge
        {
            get { return _charge; }
            set { _charge = value; }
        }

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "FormMemo", Desc = "备注", ContextType = SysDic.All, Length = 16)]
        public virtual string FormMemo
        {
            get { return _formMemo; }
            set
            {
                if (value != null && value.Length > 16)
                    throw new ArgumentOutOfRangeException("Invalid value for FormMemo", value, value.ToString());
                _formMemo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "获取申请单方式", ShortCode = "IsByHand", Desc = "获取申请单方式", ContextType = SysDic.All, Length = 4)]
        public virtual int IsByHand
        {
            get { return _isByHand; }
            set { _isByHand = value; }
        }

        [DataMember]
        [DataDesc(CName = "采样部位", ShortCode = "CollectPart", Desc = "采样部位", ContextType = SysDic.All, Length = 200)]
        public virtual string CollectPart
        {
            get { return _collectPart; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for CollectPart", value, value.ToString());
                _collectPart = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "送检目的", ShortCode = "TestAim", Desc = "送检目的", ContextType = SysDic.All, Length = 80)]
        public virtual string TestAim
        {
            get { return _testAim; }
            set
            {
                if (value != null && value.Length > 80)
                    throw new ArgumentOutOfRangeException("Invalid value for TestAim", value, value.ToString());
                _testAim = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "自定义1", ShortCode = "ZDY1", Desc = "自定义1", ContextType = SysDic.All, Length = 50)]
        public virtual string ZDY1
        {
            get { return _zDY1; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ZDY1", value, value.ToString());
                _zDY1 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "自定义2", ShortCode = "ZDY2", Desc = "自定义2", ContextType = SysDic.All, Length = 50)]
        public virtual string ZDY2
        {
            get { return _zDY2; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ZDY2", value, value.ToString());
                _zDY2 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "自定义3", ShortCode = "ZDY3", Desc = "自定义3", ContextType = SysDic.All, Length = 50)]
        public virtual string ZDY3
        {
            get { return _zDY3; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ZDY3", value, value.ToString());
                _zDY3 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "自定义4", ShortCode = "ZDY4", Desc = "自定义4", ContextType = SysDic.All, Length = 50)]
        public virtual string ZDY4
        {
            get { return _zDY4; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ZDY4", value, value.ToString());
                _zDY4 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "自定义5", ShortCode = "ZDY5", Desc = "自定义5", ContextType = SysDic.All, Length = 50)]
        public virtual string ZDY5
        {
            get { return _zDY5; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ZDY5", value, value.ToString());
                _zDY5 = value;
            }
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
        [DataDesc(CName = "计费标志", ShortCode = "ChargeFlag", Desc = "计费标志", ContextType = SysDic.All, Length = 1)]
        public virtual bool ChargeFlag
        {
            get { return _chargeFlag; }
            set { _chargeFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY6", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZDY6
        {
            get { return _zDY6; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ZDY6", value, value.ToString());
                _zDY6 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY7", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZDY7
        {
            get { return _zDY7; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ZDY7", value, value.ToString());
                _zDY7 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY8", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZDY8
        {
            get { return _zDY8; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ZDY8", value, value.ToString());
                _zDY8 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY9", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZDY9
        {
            get { return _zDY9; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ZDY9", value, value.ToString());
                _zDY9 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ZDY10", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ZDY10
        {
            get { return _zDY10; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ZDY10", value, value.ToString());
                _zDY10 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "执行科室", ShortCode = "Exec", Desc = "执行科室")]
        public virtual BMedicalDepartment Exec
        {
            get { return _exec; }
            set { _exec = value; }
        }

        [DataMember]
        [DataDesc(CName = "病区", ShortCode = "BDiseaseArea", Desc = "病区")]
        public virtual BDiseaseArea BDiseaseArea
        {
            get { return _bDiseaseArea; }
            set { _bDiseaseArea = value; }
        }

        [DataMember]
        [DataDesc(CName = "病房", ShortCode = "BDiseaseRoom", Desc = "病房")]
        public virtual BDiseaseRoom BDiseaseRoom
        {
            get { return _bDiseaseRoom; }
            set { _bDiseaseRoom = value; }
        }

        [DataMember]
        [DataDesc(CName = "医生", ShortCode = "BDoctor", Desc = "医生")]
        public virtual BDoctor BDoctor
        {
            get { return _bDoctor; }
            set { _bDoctor = value; }
        }

        [DataMember]
        [DataDesc(CName = "就诊科室", ShortCode = "BMedicalDepartment", Desc = "就诊科室")]
        public virtual BMedicalDepartment BMedicalDepartment
        {
            get { return _bMedicalDepartment; }
            set { _bMedicalDepartment = value; }
        }

        [DataMember]
        [DataDesc(CName = "费用表", ShortCode = "MEPTBCharge", Desc = "费用表")]
        public virtual MEPTBCharge MEPTBCharge
        {
            get { return _mEPTBCharge; }
            set { _mEPTBCharge = value; }
        }

        [DataMember]
        [DataDesc(CName = "实验室字典表", ShortCode = "Client", Desc = "实验室字典表")]
        public virtual BLaboratory Client
        {
            get { return _client; }
            set { _client = value; }
        }

        [DataMember]
        [DataDesc(CName = "临床诊断", ShortCode = "BDiag", Desc = "临床诊断")]
        public virtual BDiag BDiag
        {
            get { return _bDiag; }
            set { _bDiag = value; }
        }

        [DataMember]
        [DataDesc(CName = "实验室字典表", ShortCode = "BLaboratory", Desc = "实验室字典表")]
        public virtual BLaboratory BLaboratory
        {
            get { return _bLaboratory; }
            set { _bLaboratory = value; }
        }

        [DataMember]
        [DataDesc(CName = "病人信息", ShortCode = "BPatientInfo", Desc = "病人信息")]
        public virtual BPatientInfo BPatientInfo
        {
            get { return _bPatientInfo; }
            set { _bPatientInfo = value; }
        }

        [DataMember]
        [DataDesc(CName = "样本来源", ShortCode = "MEPTBSampleSource", Desc = "样本来源")]
        public virtual MEPTBSampleSource MEPTBSampleSource
        {
            get { return _mEPTBSampleSource; }
            set { _mEPTBSampleSource = value; }
        }

        [DataMember]
        [DataDesc(CName = "样本类型", ShortCode = "BSampleType", Desc = "样本类型")]
        public virtual BSampleType BSampleType
        {
            get { return _bSampleType; }
            set { _bSampleType = value; }
        }

        [DataMember]
        [DataDesc(CName = "就诊类型", ShortCode = "BSickType", Desc = "就诊类型")]
        public virtual BSickType BSickType
        {
            get { return _bSickType; }
            set { _bSickType = value; }
        }

        [DataMember]
        [DataDesc(CName = "专业表", ShortCode = "BSpecialty", Desc = "专业表")]
        public virtual BSpecialty BSpecialty
        {
            get { return _bSpecialty; }
            set { _bSpecialty = value; }
        }

        [DataMember]
        [DataDesc(CName = "检验类型", ShortCode = "BTestType", Desc = "检验类型")]
        public virtual BTestType BTestType
        {
            get { return _bTestType; }
            set { _bTestType = value; }
        }

        [DataMember]
        [DataDesc(CName = "样本状态", ShortCode = "BSampleStatus", Desc = "样本状态")]
        public virtual BSampleStatus BSampleStatus
        {
            get { return _bSampleStatus; }
            set { _bSampleStatus = value; }
        }

        [DataMember]
        [DataDesc(CName = "年龄单位", ShortCode = "BAgeUnit", Desc = "年龄单位")]
        public virtual BAgeUnit BAgeUnit
        {
            get { return _bAgeUnit; }
            set { _bAgeUnit = value; }
        }

        [DataMember]
        [DataDesc(CName = "计费操作表", ShortCode = "MEChargeInfoList", Desc = "计费操作表")]
        public virtual IList<MEChargeInfo> MEChargeInfoList
        {
            get
            {
                if (_mEChargeInfoList == null)
                {
                    _mEChargeInfoList = new List<MEChargeInfo>();
                }
                return _mEChargeInfoList;
            }
            set { _mEChargeInfoList = value; }
        }

        [DataMember]
        [DataDesc(CName = "小组样本单", ShortCode = "MEGroupSampleFormList", Desc = "小组样本单")]
        public virtual IList<MEGroupSampleForm> MEGroupSampleFormList
        {
            get
            {
                if (_mEGroupSampleFormList == null)
                {
                    _mEGroupSampleFormList = new List<MEGroupSampleForm>();
                }
                return _mEGroupSampleFormList;
            }
            set { _mEGroupSampleFormList = value; }
        }

        [DataMember]
        [DataDesc(CName = "医嘱单项目", ShortCode = "MEPTOrderItemList", Desc = "医嘱单项目")]
        public virtual IList<MEPTOrderItem> MEPTOrderItemList
        {
            get
            {
                if (_mEPTOrderItemList == null)
                {
                    _mEPTOrderItemList = new List<MEPTOrderItem>();
                }
                return _mEPTOrderItemList;
            }
            set { _mEPTOrderItemList = value; }
        }

        [DataMember]
        [DataDesc(CName = "样本单", ShortCode = "MEPTSampleFormList", Desc = "样本单")]
        public virtual IList<MEPTSampleForm> MEPTSampleFormList
        {
            get
            {
                if (_mEPTSampleFormList == null)
                {
                    _mEPTSampleFormList = new List<MEPTSampleForm>();
                }
                return _mEPTSampleFormList;
            }
            set { _mEPTSampleFormList = value; }
        }


        #endregion
    }
    #endregion
}