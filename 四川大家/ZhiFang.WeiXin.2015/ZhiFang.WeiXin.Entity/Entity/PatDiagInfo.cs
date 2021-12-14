using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
	#region PatDiagInfo

	/// <summary>
	/// PatDiagInfo object for NHibernate mapped table 'PatDiagInfo'.
	/// </summary>
	[DataContract]
	[DataDesc(CName = "", ClassCName = "PatDiagInfo", ShortCode = "PatDiagInfo", Desc = "")]
	public class PatDiagInfo : BaseEntity
	{
		#region Member Variables

		protected int _diagNo;
		protected string _diagDesc;


		#endregion

		#region Constructors

		public PatDiagInfo() { }

		public PatDiagInfo(int diagNo, string diagDesc)
		{
			this._diagNo = diagNo;
			this._diagDesc = diagDesc;
		}

		#endregion

		#region Public Properties


		[DataMember]
		[DataDesc(CName = "", ShortCode = "DiagNo", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int DiagNo
		{
			get { return _diagNo; }
			set { _diagNo = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "DiagDesc", Desc = "", ContextType = SysDic.All, Length = 250)]
		public virtual string DiagDesc
		{
			get { return _diagDesc; }
			set
			{
				if (value != null && value.Length > 250)
					throw new ArgumentOutOfRangeException("Invalid value for DiagDesc", value, value.ToString());
				_diagDesc = value;
			}
		}


		#endregion
	}
	#endregion
}