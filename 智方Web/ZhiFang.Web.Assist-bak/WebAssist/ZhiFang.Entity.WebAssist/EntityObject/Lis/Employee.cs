using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.WebAssist
{
	#region Employee

	/// <summary>
	/// Employee object for NHibernate mapped table 'Employee'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "Employee", ShortCode = "Employee", Desc = "")]
	public class Employee : BaseEntityService
    {
		#region Member Variables
		
        protected string _cName;
        protected string _shortCode;
        protected string _employeeCode;
        protected int _gender;
        protected DateTime? _brithday;
        protected int _deptNo;
        protected int _isVisible;
        protected int _dispOrder;
		

		#endregion

		#region Constructors

		public Employee() { }

		public Employee( string cName, string shortCode, string employeeCode, int gender, DateTime brithday, int deptNo, int isVisible, int dispOrder )
		{
			this._cName = cName;
			this._shortCode = shortCode;
			this._employeeCode = employeeCode;
			this._gender = gender;
			this._brithday = brithday;
			this._deptNo = deptNo;
			this._isVisible = isVisible;
			this._dispOrder = dispOrder;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "CName", Desc = "", ContextType = SysDic.All, Length = 30)]
        public virtual string CName
		{
			get { return _cName; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
				_cName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ShortCode", Desc = "", ContextType = SysDic.All, Length = 30)]
        public virtual string ShortCode
		{
			get { return _shortCode; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for ShortCode", value, value.ToString());
				_shortCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EmployeeCode", Desc = "", ContextType = SysDic.All, Length = 30)]
        public virtual string EmployeeCode
		{
			get { return _employeeCode; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for EmployeeCode", value, value.ToString());
				_employeeCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Gender", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Gender
		{
			get { return _gender; }
			set { _gender = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "Brithday", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? Brithday
		{
			get { return _brithday; }
			set { _brithday = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DeptNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DeptNo
		{
			get { return _deptNo; }
			set { _deptNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsVisible", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsVisible
		{
			get { return _isVisible; }
			set { _isVisible = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

		
		#endregion
	}
	#endregion
}