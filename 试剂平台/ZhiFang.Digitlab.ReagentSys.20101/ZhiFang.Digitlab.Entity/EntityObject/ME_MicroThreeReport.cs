using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEMicroThreeReport

	/// <summary>
	/// MEMicroThreeReport object for NHibernate mapped table 'ME_MicroThreeReport'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "微生物三级报告主表", ClassCName = "MEMicroThreeReport", ShortCode = "MEMicroThreeReport", Desc = "微生物三级报告主表")]
	public class MEMicroThreeReport : BaseEntity
	{
		#region Member Variables
		
        protected string _barCode;
        protected string _cName;
        protected string _patNo;
        protected string _deptName;
        protected string _gSampleNo;
        protected string _sampleTypeName;
        protected int _itemType;
        protected string _memo;
        protected bool _isReport;
        protected string _reportRecipients;
        protected string _empName;
        protected DateTime? _dataUpdateTime;
		protected BMedicalDepartment _bMedicalDepartment;
		protected HREmployee _hREmployee;
		protected MEGroupSampleForm _mEGroupSampleForm;
		protected BSampleType _bSampleType;
		protected IList<MEMicroThreeReportDetail> _mEMicroThreeReportDetailList; 

		#endregion

		#region Constructors

		public MEMicroThreeReport() { }

		public MEMicroThreeReport( long labID, string barCode, string cName, string patNo, string deptName, string gSampleNo, string sampleTypeName, int itemType, string memo, bool isReport, string reportRecipients, DateTime dataAddTime, string empName, DateTime dataUpdateTime, byte[] dataTimeStamp, BMedicalDepartment bMedicalDepartment, HREmployee hREmployee, MEGroupSampleForm mEGroupSampleForm, BSampleType bSampleType )
		{
			this._labID = labID;
			this._barCode = barCode;
			this._cName = cName;
			this._patNo = patNo;
			this._deptName = deptName;
			this._gSampleNo = gSampleNo;
			this._sampleTypeName = sampleTypeName;
			this._itemType = itemType;
			this._memo = memo;
			this._isReport = isReport;
			this._reportRecipients = reportRecipients;
			this._dataAddTime = dataAddTime;
			this._empName = empName;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bMedicalDepartment = bMedicalDepartment;
			this._hREmployee = hREmployee;
			this._mEGroupSampleForm = mEGroupSampleForm;
			this._bSampleType = bSampleType;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "条码号", ShortCode = "BarCode", Desc = "条码号", ContextType = SysDic.All, Length = 30)]
        public virtual string BarCode
		{
			get { return _barCode; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for BarCode", value, value.ToString());
				_barCode = value;
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
        [DataDesc(CName = "", ShortCode = "DeptName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string DeptName
		{
			get { return _deptName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for DeptName", value, value.ToString());
				_deptName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "小组检测编号", ShortCode = "GSampleNo", Desc = "小组检测编号", ContextType = SysDic.All, Length = 20)]
        public virtual string GSampleNo
		{
			get { return _gSampleNo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for GSampleNo", value, value.ToString());
				_gSampleNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "SampleTypeName", Desc = "名称", ContextType = SysDic.All, Length = 40)]
        public virtual string SampleTypeName
		{
			get { return _sampleTypeName; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for SampleTypeName", value, value.ToString());
				_sampleTypeName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "记录项类型", ShortCode = "ItemType", Desc = "记录项类型", ContextType = SysDic.All, Length = 4)]
        public virtual int ItemType
		{
			get { return _itemType; }
			set { _itemType = value; }
		}

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = 500)]
        public virtual string Memo
		{
			get { return _memo; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for Memo", value, value.ToString());
				_memo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "是否报告", ShortCode = "IsReport", Desc = "是否报告", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsReport
		{
			get { return _isReport; }
			set { _isReport = value; }
		}

        [DataMember]
        [DataDesc(CName = "报告收件人", ShortCode = "ReportRecipients", Desc = "报告收件人", ContextType = SysDic.All, Length = 50)]
        public virtual string ReportRecipients
		{
			get { return _reportRecipients; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ReportRecipients", value, value.ToString());
				_reportRecipients = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EmpName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string EmpName
		{
			get { return _empName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for EmpName", value, value.ToString());
				_empName = value;
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
        [DataDesc(CName = "就诊科室", ShortCode = "BMedicalDepartment", Desc = "就诊科室")]
		public virtual BMedicalDepartment BMedicalDepartment
		{
			get { return _bMedicalDepartment; }
			set { _bMedicalDepartment = value; }
		}

        [DataMember]
        [DataDesc(CName = "员工", ShortCode = "HREmployee", Desc = "员工")]
		public virtual HREmployee HREmployee
		{
			get { return _hREmployee; }
			set { _hREmployee = value; }
		}

        [DataMember]
        [DataDesc(CName = "小组样本单", ShortCode = "MEGroupSampleForm", Desc = "小组样本单")]
		public virtual MEGroupSampleForm MEGroupSampleForm
		{
			get { return _mEGroupSampleForm; }
			set { _mEGroupSampleForm = value; }
		}

        [DataMember]
        [DataDesc(CName = "样本类型", ShortCode = "BSampleType", Desc = "样本类型")]
		public virtual BSampleType BSampleType
		{
			get { return _bSampleType; }
			set { _bSampleType = value; }
		}

        [DataMember]
        [DataDesc(CName = "微生物三级报告细表", ShortCode = "MEMicroThreeReportDetailList", Desc = "微生物三级报告细表")]
		public virtual IList<MEMicroThreeReportDetail> MEMicroThreeReportDetailList
		{
			get
			{
				if (_mEMicroThreeReportDetailList==null)
				{
					_mEMicroThreeReportDetailList = new List<MEMicroThreeReportDetail>();
				}
				return _mEMicroThreeReportDetailList;
			}
			set { _mEMicroThreeReportDetailList = value; }
		}

        
		#endregion
	}
	#endregion
}