using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LIIP
{
	#region BHospitalEmpLink

	/// <summary>
	/// BHospitalEmpLink object for NHibernate mapped table 'B_HospitalEmpLink'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BHospitalEmpLink", ShortCode = "BHospitalEmpLink", Desc = "")]
	public class BHospitalEmpLink : BaseEntityService//BaseEntity
	{
		#region Member Variables
		
        protected long? _hospitalID;
        protected string _hospitalName;
        protected long? _empID;
        protected string _empName;
        protected long _linkTypeID;
        protected string _linkTypeName;
        protected bool _isUse;
        protected DateTime? _dataUpdateTime;
        protected string _HospitalCode;

        #endregion

        #region Constructors

        public BHospitalEmpLink() { }

		public BHospitalEmpLink( long labID, long hospitalID, string hospitalName, long empID, string empName, long linkTypeID, string linkTypeName, bool isUse, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._hospitalID = hospitalID;
			this._hospitalName = hospitalName;
			this._empID = empID;
			this._empName = empName;
			this._linkTypeID = linkTypeID;
			this._linkTypeName = linkTypeName;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "HospitalID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? HospitalID
		{
			get { return _hospitalID; }
			set { _hospitalID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HospitalName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string HospitalName
		{
			get { return _hospitalName; }
			set
			{
				_hospitalName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HospitalCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string HospitalCode
        {
            get { return _HospitalCode; }
            set
            {
                _HospitalCode = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "EmpID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? EmpID
		{
			get { return _empID; }
			set { _empID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EmpName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string EmpName
		{
			get { return _empName; }
			set
			{
				_empName = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "LinkTypeID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long LinkTypeID
		{
			get { return _linkTypeID; }
			set { _linkTypeID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LinkTypeName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string LinkTypeName
		{
			get { return _linkTypeName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for LinkTypeName", value, value.ToString());
				_linkTypeName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsUse", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

		
		#endregion
	}
	#endregion
}