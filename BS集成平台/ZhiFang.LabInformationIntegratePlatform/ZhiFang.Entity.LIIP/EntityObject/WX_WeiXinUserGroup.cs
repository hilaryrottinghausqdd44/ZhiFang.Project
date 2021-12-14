using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LIIP
{
	#region WXWeiXinUserGroup

	/// <summary>
	/// WXWeiXinUserGroup object for NHibernate mapped table 'WX_WeiXinUserGroup'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "WXWeiXinUserGroup", ShortCode = "WXWeiXinUserGroup", Desc = "")]
	public class WXWeiXinUserGroup : BaseEntityService
	{
		#region Member Variables
		
        protected string _groupName;
        protected string _comment;
        protected long _operaterId;
        protected string _operaterName;
        protected int _count;
        protected DateTime? _addTime;
		

		#endregion

		#region Constructors

		public WXWeiXinUserGroup() { }

		public WXWeiXinUserGroup( long labID, string groupName, string comment, long operaterId, string operaterName, int count, DateTime addTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._groupName = groupName;
			this._comment = comment;
			this._operaterId = operaterId;
			this._operaterName = operaterName;
			this._count = count;
			this._addTime = addTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "GroupName", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string GroupName
		{
			get { return _groupName; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for GroupName", value, value.ToString());
				_groupName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Comment", Desc = "", ContextType = SysDic.All, Length = 300)]
        public virtual string Comment
		{
			get { return _comment; }
			set
			{
				if ( value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for Comment", value, value.ToString());
				_comment = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OperaterId", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long OperaterId
		{
			get { return _operaterId; }
			set { _operaterId = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OperaterName", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string OperaterName
		{
			get { return _operaterName; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for OperaterName", value, value.ToString());
				_operaterName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Count", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Count
		{
			get { return _count; }
			set { _count = value; }
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