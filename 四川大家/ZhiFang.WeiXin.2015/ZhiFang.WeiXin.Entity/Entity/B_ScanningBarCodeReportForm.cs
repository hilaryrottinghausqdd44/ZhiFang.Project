using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
	#region BScanningBarCodeReportForm

	/// <summary>
	/// BScanningBarCodeReportForm object for NHibernate mapped table 'B_ScanningBarCodeReportForm'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "扫一扫(条码)报告索引表", ClassCName = "BScanningBarCodeReportForm", ShortCode = "BScanningBarCodeReportForm", Desc = "扫一扫(条码)报告索引表")]
	public class BScanningBarCodeReportForm : BaseEntity
	{
		#region Member Variables
		
        protected string _hospitalCode;
        protected long? _weiXinUserID;
        protected long? _reportFormIndexID;
        protected string _barCode;
        protected string _WeiXinAccount;
        protected string _name;
        protected int _age;
        protected string _ageUnit;
        protected string _sex;
        protected DateTime? _reportFormTime;
        protected bool _readedFlag;
        protected DateTime? _dataUpdateTime;
		

		#endregion

		#region Constructors

		public BScanningBarCodeReportForm() { }

		public BScanningBarCodeReportForm( string hospitalCode, long weiXinUserID, long reportFormIndexID, string barCode, string name, int age, string ageUnit, string sex, DateTime reportFormTime, bool readedFlag, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp )
		{
			this._hospitalCode = hospitalCode;
			this._weiXinUserID = weiXinUserID;
			this._reportFormIndexID = reportFormIndexID;
			this._barCode = barCode;
			this._name = name;
			this._age = age;
			this._ageUnit = ageUnit;
			this._sex = sex;
			this._reportFormTime = reportFormTime;
			this._readedFlag = readedFlag;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "HospitalCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string HospitalCode
		{
			get { return _hospitalCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for HospitalCode", value, value.ToString());
				_hospitalCode = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "微信关注用户ID", ShortCode = "WeiXinUserID", Desc = "微信关注用户ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? WeiXinUserID
		{
			get { return _weiXinUserID; }
			set { _weiXinUserID = value; }
		}

        //[DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        //[DataDesc(CName = "报告单ID", ShortCode = "ReportFormIndexID", Desc = "报告单ID", ContextType = SysDic.All, Length = 8)]
        //public virtual long? ReportFormIndexID
        //{
        //    get { return _reportFormIndexID; }
        //    set { _reportFormIndexID = value; }
        //}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "BarCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string BarCode
		{
			get { return _barCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for BarCode", value, value.ToString());
				_barCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "微信关注用户OpenID", ShortCode = "WeiXinAccount", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string WeiXinAccount
        {
            get { return _WeiXinAccount; }
            set
            {
                _WeiXinAccount = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Name", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Name
		{
			get { return _name; }
			set
			{
				if ( value != null && value.Length > 20)
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
				if ( value != null && value.Length > 20)
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
				if ( value != null && value.Length > 20)
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

		
		#endregion
	}
	#endregion
}