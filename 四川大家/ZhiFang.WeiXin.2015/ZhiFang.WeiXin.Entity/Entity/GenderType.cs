using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
	#region GenderType

	/// <summary>
	/// GenderType object for NHibernate mapped table 'GenderType'.
	/// </summary>
	[DataContract]
	[DataDesc(CName = "", ClassCName = "GenderType", ShortCode = "GenderType", Desc = "")]
	public class GenderType : BaseEntity
	{
		#region Member Variables

		protected string _cName;
		protected string _shortCode;
		protected int _visible;
		protected int _dispOrder;
		protected string _hisOrderCode;


		#endregion

		#region Constructors

		public GenderType() { }

		public GenderType(string cName, string shortCode, int visible, int dispOrder, string hisOrderCode)
		{
			this._cName = cName;
			this._shortCode = shortCode;
			this._visible = visible;
			this._dispOrder = dispOrder;
			this._hisOrderCode = hisOrderCode;
		}

		#endregion

		#region Public Properties


		[DataMember]
		[DataDesc(CName = "", ShortCode = "CName", Desc = "", ContextType = SysDic.All, Length = 10)]
		public virtual string CName
		{
			get { return _cName; }
			set
			{
				if (value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
				_cName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "ShortCode", Desc = "", ContextType = SysDic.All, Length = 10)]
		public virtual string ShortCode
		{
			get { return _shortCode; }
			set
			{
				if (value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for ShortCode", value, value.ToString());
				_shortCode = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "Visible", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int Visible
		{
			get { return _visible; }
			set { _visible = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "HisOrderCode", Desc = "", ContextType = SysDic.All, Length = 20)]
		public virtual string HisOrderCode
		{
			get { return _hisOrderCode; }
			set
			{
				if (value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for HisOrderCode", value, value.ToString());
				_hisOrderCode = value;
			}
		}



		#endregion
	}
	#endregion
}