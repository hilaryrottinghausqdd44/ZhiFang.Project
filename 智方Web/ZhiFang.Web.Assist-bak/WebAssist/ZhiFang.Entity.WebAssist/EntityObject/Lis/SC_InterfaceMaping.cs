using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.WebAssist
{
	#region SCInterfaceMaping

	/// <summary>
	/// BloodInterfaceMaping object for NHibernate mapped table 'Blood_Interface_Maping'.
	/// </summary>
	[DataContract]
	[DataDesc(CName = "接口映射(对照)关系表", ClassCName = "SCInterfaceMaping", ShortCode = "SCInterfaceMaping", Desc = "接口映射(对照)关系表")]
	public class SCInterfaceMaping : BaseEntity
	{
		#region Member Variables

		protected long _bobjectID;
		protected string _mapingCode;
		protected bool _isUse;
		protected int _dispOrder;
		protected BDict _bobjectType;

		#endregion

		#region Constructors

		public SCInterfaceMaping() { }

		public SCInterfaceMaping(long labID, long bobjectID, string mapingCode, bool isUse, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp, BDict bobjectType)
		{
			this._labID = labID;
			this._bobjectID = bobjectID;
			this._mapingCode = mapingCode;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bobjectType = bobjectType;
		}

		#endregion

		#region Public Properties


		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "业务对象ID", ShortCode = "BobjectID", Desc = "业务对象ID", ContextType = SysDic.All, Length = 8)]
		public virtual long BobjectID
		{
			get { return _bobjectID; }
			set { _bobjectID = value; }
		}

		[DataMember]
		[DataDesc(CName = "对照码", ShortCode = "MapingCode", Desc = "对照码", ContextType = SysDic.All, Length = 60)]
		public virtual string MapingCode
		{
			get { return _mapingCode; }
			set
			{
				if (value != null && value.Length > 60)
					throw new ArgumentOutOfRangeException("Invalid value for MapingCode", value, value.ToString());
				_mapingCode = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
		public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

		[DataMember]
		[DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
		public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

		[DataMember]
		[DataDesc(CName = "字典信息", ShortCode = "BobjectType", Desc = "字典信息")]
		public virtual BDict BobjectType
		{
			get { return _bobjectType; }
			set { _bobjectType = value; }
		}


		#endregion
	}
	#endregion
}