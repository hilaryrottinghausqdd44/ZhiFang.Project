using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEMicroRetainedBacteria

	/// <summary>
	/// MEMicroRetainedBacteria object for NHibernate mapped table 'ME_MicroRetainedBacteria'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "微生物留菌记录", ClassCName = "MEMicroRetainedBacteria", ShortCode = "MEMicroRetainedBacteria", Desc = "微生物留菌记录")]
	public class MEMicroRetainedBacteria : BaseEntity
	{
		#region Member Variables
		
        protected string _barCode;
        protected string _cName;
        protected string _patNo;
        protected long? _deptID;
        protected string _deptName;
        protected string _gSampleNo;
        protected long? _sampleTypeID;
        protected string _sampleTypeName;
        protected int _position;
        protected string _empName;
        protected DateTime? _dataUpdateTime;
		protected BMicro _bMicro;
		protected HREmployee _hREmployee;
		protected MEGroupSampleForm _mEGroupSampleForm;
		protected MEMicroAppraisalValue _mEMicroAppraisalValue;
		protected MEMicroRetainedBacteriaBox _mEMicroRetainedBacteriaBox;
		protected IList<MEMicroAppraisalValue> _mEMicroAppraisalValueList; 

		#endregion

		#region Constructors

		public MEMicroRetainedBacteria() { }

		public MEMicroRetainedBacteria( long labID, string barCode, string cName, string patNo, long deptID, string deptName, string gSampleNo, long sampleTypeID, string sampleTypeName, int position, DateTime dataAddTime, string empName, DateTime dataUpdateTime, byte[] dataTimeStamp, BMicro bMicro, HREmployee hREmployee, MEGroupSampleForm mEGroupSampleForm, MEMicroAppraisalValue mEMicroAppraisalValue, MEMicroRetainedBacteriaBox mEMicroRetainedBacteriaBox )
		{
			this._labID = labID;
			this._barCode = barCode;
			this._cName = cName;
			this._patNo = patNo;
			this._deptID = deptID;
			this._deptName = deptName;
			this._gSampleNo = gSampleNo;
			this._sampleTypeID = sampleTypeID;
			this._sampleTypeName = sampleTypeName;
			this._position = position;
			this._dataAddTime = dataAddTime;
			this._empName = empName;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bMicro = bMicro;
			this._hREmployee = hREmployee;
			this._mEGroupSampleForm = mEGroupSampleForm;
			this._mEMicroAppraisalValue = mEMicroAppraisalValue;
			this._mEMicroRetainedBacteriaBox = mEMicroRetainedBacteriaBox;
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "就诊科室ID", ShortCode = "DeptID", Desc = "就诊科室ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? DeptID
		{
			get { return _deptID; }
			set { _deptID = value; }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "样本类型ID", ShortCode = "SampleTypeID", Desc = "样本类型ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? SampleTypeID
		{
			get { return _sampleTypeID; }
			set { _sampleTypeID = value; }
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
        [DataDesc(CName = "留菌位置号", ShortCode = "Position", Desc = "留菌位置号", ContextType = SysDic.All, Length = 4)]
        public virtual int Position
		{
			get { return _position; }
			set { _position = value; }
		}

        [DataMember]
        [DataDesc(CName = "记录人姓名", ShortCode = "EmpName", Desc = "记录人姓名", ContextType = SysDic.All, Length = 60)]
        public virtual string EmpName
		{
			get { return _empName; }
			set
			{
				if ( value != null && value.Length > 60)
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
        [DataDesc(CName = "微生物", ShortCode = "BMicro", Desc = "微生物")]
		public virtual BMicro BMicro
		{
			get { return _bMicro; }
			set { _bMicro = value; }
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
        [DataDesc(CName = "微生物鉴定结果", ShortCode = "MEMicroAppraisalValue", Desc = "微生物鉴定结果")]
		public virtual MEMicroAppraisalValue MEMicroAppraisalValue
		{
			get { return _mEMicroAppraisalValue; }
			set { _mEMicroAppraisalValue = value; }
		}

        [DataMember]
        [DataDesc(CName = "微生物留菌盒记录", ShortCode = "MEMicroRetainedBacteriaBox", Desc = "微生物留菌盒记录")]
		public virtual MEMicroRetainedBacteriaBox MEMicroRetainedBacteriaBox
		{
			get { return _mEMicroRetainedBacteriaBox; }
			set { _mEMicroRetainedBacteriaBox = value; }
		}

        [DataMember]
        [DataDesc(CName = "微生物鉴定结果", ShortCode = "MEMicroAppraisalValueList", Desc = "微生物鉴定结果")]
		public virtual IList<MEMicroAppraisalValue> MEMicroAppraisalValueList
		{
			get
			{
				if (_mEMicroAppraisalValueList==null)
				{
					_mEMicroAppraisalValueList = new List<MEMicroAppraisalValue>();
				}
				return _mEMicroAppraisalValueList;
			}
			set { _mEMicroAppraisalValueList = value; }
		}

        
		#endregion
	}
	#endregion
}