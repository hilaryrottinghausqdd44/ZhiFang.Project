using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.WeiXin.Entity
{
	#region BusinessLogicClientControl

	/// <summary>
	/// BusinessLogicClientControl object for NHibernate mapped table 'BusinessLogicClientControl'.
	/// </summary>
	[DataContract]
	[DataDesc(CName = "", ClassCName = "BusinessLogicClientControl", ShortCode = "BusinessLogicClientControl", Desc = "")]
	public class BusinessLogicClientControl : BaseEntity
	{
		#region Member Variables
		protected string _Account;
		protected CLIENTELE _CLIENTELE;
		protected DateTime? _addTime;
		protected int _Flag;


		#endregion

		#region Constructors



		#endregion

		#region Public Properties


		[DataMember]
		[DataDesc(CName = "", ShortCode = "Account", Desc = "", ContextType = SysDic.All, Length = 300)]
		public virtual string Account
		{
			get { return _Account; }
			set
			{
				_Account = value;
			}
		}
		[DataMember]
		[DataDesc(CName = "", ShortCode = "ClientNo", Desc = "", ContextType = SysDic.All, Length = 300)]
		public virtual CLIENTELE CLIENTELE
		{
			get { return _CLIENTELE; }
			set
			{
				_CLIENTELE = value;
			}
		}
		[DataMember]
		[DataDesc(CName = "", ShortCode = "Flag", Desc = "", ContextType = SysDic.All, Length = 300)]
		public virtual int Flag
		{
			get { return _Flag; }
			set
			{
				_Flag = value;
			}
		}
		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "AddTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? AddTime
		{
			get { return _addTime; }
			set { _addTime = value; }
		}


		#endregion
	}
	#endregion
}