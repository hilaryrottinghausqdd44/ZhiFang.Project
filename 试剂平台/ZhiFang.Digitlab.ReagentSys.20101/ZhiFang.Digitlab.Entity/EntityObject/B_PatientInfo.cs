using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region BPatientInfo

	/// <summary>
	/// BPatientInfo object for NHibernate mapped table 'B_PatientInfo'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "病人信息", ClassCName = "BPatientInfo", ShortCode = "BPatientInfo", Desc = "病人信息")]
	public class BPatientInfo : BaseEntity
	{
		#region Member Variables
		
        protected string _patientID;
        protected string _patNo;
        protected string _cName;
        protected DateTime? _birthday;
        protected string _telephone;
        protected string _address;
        protected string _zipCode;
        protected int _certificateNo;
        protected string _certificateNumber;
        protected DateTime? _dataUpdateTime;
		protected BNationality _bNationality;
		protected BSex _bSex;
		protected IList<MEPTOrderForm> _mEPTOrderFormList;

		#endregion

		#region Constructors

		public BPatientInfo() { }

		public BPatientInfo( long labID, string patientID, string patNo, string cName, DateTime birthday, string telephone, string address, string zipCode, int certificateNo, string certificateNumber, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BNationality bNationality, BSex bSex )
		{
			this._labID = labID;
			this._patientID = patientID;
			this._patNo = patNo;
			this._cName = cName;
			this._birthday = birthday;
			this._telephone = telephone;
			this._address = address;
			this._zipCode = zipCode;
			this._certificateNo = certificateNo;
			this._certificateNumber = certificateNumber;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bNationality = bNationality;
			this._bSex = bSex;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "病人ID", ShortCode = "PatientID", Desc = "病人ID", ContextType = SysDic.All, Length = 20)]
        public virtual string PatientID
		{
			get { return _patientID; }
			set
			{
				if ( value != null && value.Length > 20)
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
				if ( value != null && value.Length > 20)
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
				if ( value != null && value.Length > 40)
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
        [DataDesc(CName = "电话", ShortCode = "Telephone", Desc = "电话", ContextType = SysDic.All, Length = 30)]
        public virtual string Telephone
		{
			get { return _telephone; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for Telephone", value, value.ToString());
				_telephone = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "地址", ShortCode = "Address", Desc = "地址", ContextType = SysDic.All, Length = 200)]
        public virtual string Address
		{
			get { return _address; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for Address", value, value.ToString());
				_address = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "邮编", ShortCode = "ZipCode", Desc = "邮编", ContextType = SysDic.All, Length = 10)]
        public virtual string ZipCode
		{
			get { return _zipCode; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for ZipCode", value, value.ToString());
				_zipCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "证件类型", ShortCode = "CertificateNo", Desc = "证件类型", ContextType = SysDic.All, Length = 4)]
        public virtual int CertificateNo
		{
			get { return _certificateNo; }
			set { _certificateNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "证件编号", ShortCode = "CertificateNumber", Desc = "证件编号", ContextType = SysDic.All, Length = 50)]
        public virtual string CertificateNumber
		{
			get { return _certificateNumber; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for CertificateNumber", value, value.ToString());
				_certificateNumber = value;
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
        [DataDesc(CName = "民族", ShortCode = "BNationality", Desc = "民族")]
		public virtual BNationality BNationality
		{
			get { return _bNationality; }
			set { _bNationality = value; }
		}

        [DataMember]
        [DataDesc(CName = "性别", ShortCode = "BSex", Desc = "性别")]
		public virtual BSex BSex
		{
			get { return _bSex; }
			set { _bSex = value; }
		}

        [DataMember]
        [DataDesc(CName = "医嘱单", ShortCode = "MEPTOrderFormList", Desc = "医嘱单")]
		public virtual IList<MEPTOrderForm> MEPTOrderFormList
		{
			get
			{
				if (_mEPTOrderFormList==null)
				{
					_mEPTOrderFormList = new List<MEPTOrderForm>();
				}
				return _mEPTOrderFormList;
			}
			set { _mEPTOrderFormList = value; }
		}

        
		#endregion
	}
	#endregion
}