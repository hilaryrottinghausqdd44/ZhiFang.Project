using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.RBAC
{
	#region Department

	/// <summary>
	/// Department object for NHibernate mapped table 'Department'.
	/// </summary>
	[DataContract]
	[DataDesc(CName = "部门(病区/科室)信息表", ClassCName = "Department", ShortCode = "Department", Desc = "部门(病区/科室)信息表")]
	public class Department : BaseEntity
	{
		#region Member Variables

		protected long _parentID;
		protected string _cName;
		protected string _shortName;
		protected string _shortCode;
		protected string _deptPhoneCode;
		protected string _urgentState;
		protected string _patState;
		protected string _code1;
		protected string _code2;
		protected string _code3;
		protected string _code4;
		protected string _code5;
		protected string _code6;
		protected string _code7;
		protected string _code8;
		protected string _code9;
		protected string _code10;
		protected bool _visible;
		protected int _dispOrder;


		#endregion

		#region Constructors

		public Department() { }

		public Department(long labID, long parentID, string cName, string shortName, string shortCode, string deptPhoneCode, string urgentState, string patState, string code1, string code2, string code3, string code4, string code5, string code6, string code7, string code8, string code9, string code10, bool visible, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp)
		{
			this._labID = labID;
			this._parentID = parentID;
			this._cName = cName;
			this._shortName = shortName;
			this._shortCode = shortCode;
			this._deptPhoneCode = deptPhoneCode;
			this._urgentState = urgentState;
			this._patState = patState;
			this._code1 = code1;
			this._code2 = code2;
			this._code3 = code3;
			this._code4 = code4;
			this._code5 = code5;
			this._code6 = code6;
			this._code7 = code7;
			this._code8 = code8;
			this._code9 = code9;
			this._code10 = code10;
			this._visible = visible;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "上级编码", ShortCode = "ParentID", Desc = "上级编码", ContextType = SysDic.All, Length = 8)]
		public virtual long ParentID
		{
			get { return _parentID; }
			set { _parentID = value; }
		}

		[DataMember]
		[DataDesc(CName = "名称", ShortCode = "CName", Desc = "名称", ContextType = SysDic.All, Length = 40)]
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
		[DataDesc(CName = "简称", ShortCode = "ShortName", Desc = "简称", ContextType = SysDic.All, Length = 20)]
		public virtual string ShortName
		{
			get { return _shortName; }
			set
			{
				if (value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ShortName", value, value.ToString());
				_shortName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "快捷码", ShortCode = "ShortCode", Desc = "快捷码", ContextType = SysDic.All, Length = 20)]
		public virtual string ShortCode
		{
			get { return _shortCode; }
			set
			{
				if (value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ShortCode", value, value.ToString());
				_shortCode = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "电话", ShortCode = "DeptPhoneCode", Desc = "电话", ContextType = SysDic.All, Length = 50)]
		public virtual string DeptPhoneCode
		{
			get { return _deptPhoneCode; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for DeptPhoneCode", value, value.ToString());
				_deptPhoneCode = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "UrgentState", ShortCode = "UrgentState", Desc = "UrgentState", ContextType = SysDic.All, Length = 50)]
		public virtual string UrgentState
		{
			get { return _urgentState; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for UrgentState", value, value.ToString());
				_urgentState = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "PatState", ShortCode = "PatState", Desc = "PatState", ContextType = SysDic.All, Length = 50)]
		public virtual string PatState
		{
			get { return _patState; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for PatState", value, value.ToString());
				_patState = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "code_1", ShortCode = "Code1", Desc = "code_1", ContextType = SysDic.All, Length = 50)]
		public virtual string Code1
		{
			get { return _code1; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Code1", value, value.ToString());
				_code1 = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "code_2", ShortCode = "Code2", Desc = "code_2", ContextType = SysDic.All, Length = 50)]
		public virtual string Code2
		{
			get { return _code2; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Code2", value, value.ToString());
				_code2 = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "code_3", ShortCode = "Code3", Desc = "code_3", ContextType = SysDic.All, Length = 50)]
		public virtual string Code3
		{
			get { return _code3; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Code3", value, value.ToString());
				_code3 = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "code_4", ShortCode = "Code4", Desc = "code_4", ContextType = SysDic.All, Length = 50)]
		public virtual string Code4
		{
			get { return _code4; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Code4", value, value.ToString());
				_code4 = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "code_5", ShortCode = "Code5", Desc = "code_5", ContextType = SysDic.All, Length = 50)]
		public virtual string Code5
		{
			get { return _code5; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Code5", value, value.ToString());
				_code5 = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "code_6", ShortCode = "Code6", Desc = "code_6", ContextType = SysDic.All, Length = 50)]
		public virtual string Code6
		{
			get { return _code6; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Code6", value, value.ToString());
				_code6 = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "code_7", ShortCode = "Code7", Desc = "code_7", ContextType = SysDic.All, Length = 50)]
		public virtual string Code7
		{
			get { return _code7; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Code7", value, value.ToString());
				_code7 = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "code_8", ShortCode = "Code8", Desc = "code_8", ContextType = SysDic.All, Length = 50)]
		public virtual string Code8
		{
			get { return _code8; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Code8", value, value.ToString());
				_code8 = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "code_9", ShortCode = "Code9", Desc = "code_9", ContextType = SysDic.All, Length = 50)]
		public virtual string Code9
		{
			get { return _code9; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Code9", value, value.ToString());
				_code9 = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "code_10", ShortCode = "Code10", Desc = "code_10", ContextType = SysDic.All, Length = 50)]
		public virtual string Code10
		{
			get { return _code10; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Code10", value, value.ToString());
				_code10 = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "是否使用", ShortCode = "Visible", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
		public virtual bool Visible
		{
			get { return _visible; }
			set { _visible = value; }
		}

		[DataMember]
		[DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
		public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}


		#endregion
	}
	#endregion
}