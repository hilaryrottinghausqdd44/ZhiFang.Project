using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.WebAssist
{
	#region StatusType

	/// <summary>
	/// StatusType object for NHibernate mapped table 'StatusType'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "StatusType", ShortCode = "StatusType", Desc = "")]
	public class StatusType : BaseEntity
	{
		#region Member Variables
		
        protected string _cName;
        protected string _statusDesc;
        protected string _statusColor;

		#endregion

		#region Constructors

		public StatusType() { }

		public StatusType( string cName, string statusDesc, string statusColor )
		{
			this._cName = cName;
			this._statusDesc = statusDesc;
			this._statusColor = statusColor;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "CName", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string CName
		{
			get { return _cName; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
				_cName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "StatusDesc", Desc = "", ContextType = SysDic.All, Length = 250)]
        public virtual string StatusDesc
		{
			get { return _statusDesc; }
			set
			{
				if ( value != null && value.Length > 250)
					throw new ArgumentOutOfRangeException("Invalid value for StatusDesc", value, value.ToString());
				_statusDesc = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "StatusColor", Desc = "", ContextType = SysDic.All, Length = 15)]
        public virtual string StatusColor
		{
			get { return _statusColor; }
			set
			{
				if ( value != null && value.Length > 15)
					throw new ArgumentOutOfRangeException("Invalid value for StatusColor", value, value.ToString());
				_statusColor = value;
			}
		}

        
		#endregion
	}
	#endregion
}