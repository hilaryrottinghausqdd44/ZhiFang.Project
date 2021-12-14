using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
	#region BWeiXinEmpLink

	/// <summary>
	/// BWeiXinEmpLink object for NHibernate mapped table 'B_WeiXin_Emp_Link'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "微信账户绑定员工表", ClassCName = "BWeiXinEmpLink", ShortCode = "BWeiXinEmpLink", Desc = "微信账户绑定员工表")]
	public class BWeiXinEmpLink : BaseEntity
	{
		#region Member Variables
		
        protected long? _empID;
		protected BWeiXinAccount _bWeiXinAccount;
        protected string _EmpName;

        #endregion

        #region Constructors

        public BWeiXinEmpLink() { }

		public BWeiXinEmpLink( long labID, long empID, DateTime dataAddTime, byte[] dataTimeStamp, BWeiXinAccount bWeiXinAccount )
		{
			this._labID = labID;
			this._empID = empID;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bWeiXinAccount = bWeiXinAccount;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "员工ID", ShortCode = "EmpID", Desc = "员工ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? EmpID
		{
			get { return _empID; }
			set { _empID = value; }
		}

        [DataMember]
        [DataDesc(CName = "微信账户", ShortCode = "BWeiXinAccount", Desc = "微信账户")]
		public virtual BWeiXinAccount BWeiXinAccount
		{
			get { return _bWeiXinAccount; }
			set { _bWeiXinAccount = value; }
		}


        [DataMember]
        [DataDesc(CName = "员工名称", ShortCode = "EmpName", Desc = "员工名称")]
        public virtual string EmpName
        {
            get { return _EmpName; }
            set { _EmpName = value; }
        }


        #endregion
    }
	#endregion
}