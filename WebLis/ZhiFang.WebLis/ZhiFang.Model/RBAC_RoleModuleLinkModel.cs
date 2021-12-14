using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Model
{
    public class RBAC_RoleModuleLinkModel
	{
		public RBAC_RoleModuleLinkModel()
		{ }
		#region Model
		private long _id;
		private long? _moduleid;
		private long? _roleid;
		private string _modulesn;
		private bool _isuse;
		private DateTime? _dataaddtime;
		private DateTime? _datatimestamp;
		/// <summary>
		/// 
		/// </summary>
		public long Id
		{
			set { _id = value; }
			get { return _id; }
		}
		/// <summary>
		/// 
		/// </summary>
		public long? ModuleId
		{
			set { _moduleid = value; }
			get { return _moduleid; }
		}
		/// <summary>
		/// 
		/// </summary>
		public long? RoleId
		{
			set { _roleid = value; }
			get { return _roleid; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string ModuleSN
		{
			set { _modulesn = value; }
			get { return _modulesn; }
		}
		/// <summary>
		/// 是否使用
		/// </summary>
		public bool IsUse
		{
			set { _isuse = value; }
			get { return _isuse; }
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime? DataAddTime
		{
			set { _dataaddtime = value; }
			get { return _dataaddtime; }
		}
		/// <summary>
		/// 时间戳
		/// </summary>
		public DateTime? DataTimeStamp
		{
			set { _datatimestamp = value; }
			get { return _datatimestamp; }
		}
		#endregion Model
	}
}
