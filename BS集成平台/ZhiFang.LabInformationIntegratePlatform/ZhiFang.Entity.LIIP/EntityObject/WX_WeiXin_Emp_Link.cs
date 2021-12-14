using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LIIP
{
	#region WXWeiXinEmpLink

	/// <summary>
	/// WXWeiXinEmpLink object for NHibernate mapped table 'WX_WeiXin_Emp_Link'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "WXWeiXinEmpLink", ShortCode = "WXWeiXinEmpLink", Desc = "")]
	public class WXWeiXinEmpLink : BaseEntityService
	{
		#region Member Variables
		
        protected long? _weiXinAccountID;
        protected long? _empID;
        protected string _empName;
		

		#endregion

		#region Constructors

		public WXWeiXinEmpLink() { }

		public WXWeiXinEmpLink( long labID, long weiXinAccountID, long empID, string empName, DateTime dataAddTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._weiXinAccountID = weiXinAccountID;
			this._empID = empID;
			this._empName = empName;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "WeiXinAccountID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? WeiXinAccountID
		{
			get { return _weiXinAccountID; }
			set { _weiXinAccountID = value; }
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
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for EmpName", value, value.ToString());
				_empName = value;
			}
		}

		
		#endregion
	}
	#endregion
}