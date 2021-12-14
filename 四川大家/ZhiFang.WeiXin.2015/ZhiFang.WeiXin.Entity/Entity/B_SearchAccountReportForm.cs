using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
    #region BSearchAccountReportForm

    /// <summary>
    /// BSearchAccountReportForm object for NHibernate mapped table 'B_SearchAccountReportForm'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "查询账户报告索引表", ClassCName = "BSearchAccountReportForm", ShortCode = "BSearchAccountReportForm", Desc = "查询账户报告索引表")]
    public class BSearchAccountReportForm : BaseEntity
    {
        #region Member Variables

        protected long? _accountHospitalSearchContextID;
        protected long? _reportFormIndexID;
        protected string _hospitalCode;
        protected bool _readedFlag;
        protected DateTime? _dataUpdateTime;
        protected string _name;
        protected int _age;
        protected string _ageUnit;
        protected string _sex;
        protected DateTime? _reportFormTime;
        protected long? _SBarCodeRFID;
        protected string _Barcode;
        protected string _MobileCode;
        protected string _IDNumber;
        protected string _MediCare;
        protected string _PatNo;
        protected string _VisNo;
        protected string _TakeNo;
        protected string _HospitalName;
        protected DateTime? _COLLECTDATE;
        protected DateTime? _COLLECTTIME;
        protected string _PItemName;
        protected string _ReportFormType;
        protected int _ExportCount;
        protected List<Entity.RFReportItemFull> itemlist;
        protected List<Entity.RFReportMicroFull> microlist;
        protected List<Entity.RFReportMarrowFull> marrowlist;




        #endregion

        #region Constructors

        public BSearchAccountReportForm() { }

        public BSearchAccountReportForm(long accountHospitalSearchContextID, long reportFormIndexID, string hospitalCode, bool readedFlag, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, string name, int age, string ageUnit, string sex, DateTime reportFormTime)
        {
            this._accountHospitalSearchContextID = accountHospitalSearchContextID;
            this._reportFormIndexID = reportFormIndexID;
            this._hospitalCode = hospitalCode;
            this._readedFlag = readedFlag;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._name = name;
            this._age = age;
            this._ageUnit = ageUnit;
            this._sex = sex;
            this._reportFormTime = reportFormTime;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "AccountHospitalSearchContextID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? AccountHospitalSearchContextID
        {
            get { return _accountHospitalSearchContextID; }
            set { _accountHospitalSearchContextID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ReportFormIndexID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? ReportFormIndexID
        {
            get { return _reportFormIndexID; }
            set { _reportFormIndexID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HospitalCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string HospitalCode
        {
            get { return _hospitalCode; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for HospitalCode", value, value.ToString());
                _hospitalCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReadedFlag", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool ReadedFlag
        {
            get { return _readedFlag; }
            set { _readedFlag = value; }
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
        [DataDesc(CName = "", ShortCode = "Name", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Name
        {
            get { return _name; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Name", value, value.ToString());
                _name = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Age", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Age
        {
            get { return _age; }
            set { _age = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "AgeUnit", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string AgeUnit
        {
            get { return _ageUnit; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for AgeUnit", value, value.ToString());
                _ageUnit = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Sex", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Sex
        {
            get { return _sex; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Sex", value, value.ToString());
                _sex = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ReportFormTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? ReportFormTime
        {
            get { return _reportFormTime; }
            set { _reportFormTime = value; }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "扫一扫表索引", ShortCode = "SBarCodeRFID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? SBarCodeRFID
        {
            get { return _SBarCodeRFID; }
            set { _SBarCodeRFID = value; }
        }
        [DataMember]
        [DataDesc(CName = "条码", ShortCode = "Barcode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Barcode
        {
            get { return _Barcode; }
            set { _Barcode = value; }
        }
        [DataMember]
        [DataDesc(CName = "手机号", ShortCode = "MobileCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string MobileCode
        {
            get { return _MobileCode; }
            set { _MobileCode = value; }
        }
        [DataMember]
        [DataDesc(CName = "身份证", ShortCode = "IDNumber", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string IDNumber
        {
            get { return _IDNumber; }
            set { _IDNumber = value; }
        }
        [DataMember]
        [DataDesc(CName = "医保卡号", ShortCode = "MediCare", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string MediCare
        {
            get { return _MediCare; }
            set { _MediCare = value; }
        }
        [DataMember]
        [DataDesc(CName = "病历号", ShortCode = "PatNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string PatNo
        {
            get { return _PatNo; }
            set { _PatNo = value; }
        }
        [DataMember]
        [DataDesc(CName = "就诊卡号", ShortCode = "VisNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string VisNo
        {
            get { return _VisNo; }
            set { _VisNo = value; }
        }
        [DataMember]
        [DataDesc(CName = "取单号", ShortCode = "TakeNo", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string TakeNo
        {
            get { return _TakeNo; }
            set { _TakeNo = value; }
        }
        [DataMember]
        [DataDesc(CName = "医院名称", ShortCode = "HospitalName", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string HospitalName
        {
            get { return _HospitalName; }
            set { _HospitalName = value; }
        }

        [DataMember]
        [DataDesc(CName = "采样日期", ShortCode = "COLLECTDATE", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual DateTime? COLLECTDATE
        {
            get { return _COLLECTDATE; }
            set { _COLLECTDATE = value; }
        }
        [DataMember]
        [DataDesc(CName = "采样时间", ShortCode = "COLLECTTIME", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual DateTime? COLLECTTIME
        {
            get { return _COLLECTTIME; }
            set { _COLLECTTIME = value; }
        }
        [DataMember]
        [DataDesc(CName = "医嘱项目", ShortCode = "PItemName", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string PItemName
        {
            get { return _PItemName; }
            set { _PItemName = value; }
        }
        [DataMember]
        [DataDesc(CName = "报告类型", ShortCode = "ReportFormType", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string ReportFormType
        {
            get { return _ReportFormType; }
            set { _ReportFormType = value; }
        }

        [DataMember]
        [DataDesc(CName = "普通项目列表", ShortCode = "ItemList", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual List<Entity.RFReportItemFull> ItemList
        {
            get { return itemlist; }
            set { itemlist = value; }
        }
        [DataMember]
        [DataDesc(CName = "微生物项目列表", ShortCode = "MicroList", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual List<Entity.RFReportMicroFull> MicroList
        {
            get { return microlist; }
            set { microlist = value; }
        }
        [DataMember]
        [DataDesc(CName = "骨髓项目列表", ShortCode = "MarrowList", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual List<Entity.RFReportMarrowFull> MarrowList
        {
            get { return marrowlist; }
            set { marrowlist = value; }
        }

        [DataMember]
        [DataDesc(CName = "浏览次数", ShortCode = "ExportCount", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual int ExportCount
        {
            get { return _ExportCount; }
            set { _ExportCount = value; }
        }
        #endregion
    }
    #endregion
}