using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity.ReagentSys
{
	#region ReaPlace

	/// <summary>
	/// ReaPlace object for NHibernate mapped table 'Rea_Place'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "货位表", ClassCName = "ReaPlace", ShortCode = "ReaPlace", Desc = "货位表")]
	public class ReaPlace : BaseEntity
	{
		#region Member Variables
		
        protected string _cName;
        protected string _shortCode;
        protected string _memo;
        protected string _zX1;
        protected string _zX2;
        protected string _zX3;
        protected int _dispOrder;
        protected bool _visible;
        protected long? _createrID;
        protected string _createrName;
        protected DateTime? _dataUpdateTime;
		protected ReaStorage _reaStorage;

		#endregion

		#region Constructors

		public ReaPlace() { }

		public ReaPlace( long labID, string cName, string shortCode, string memo, string zX1, string zX2, string zX3, int dispOrder, bool visible, long createrID, string createrName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, ReaStorage reaStorage )
		{
			this._labID = labID;
			this._cName = cName;
			this._shortCode = shortCode;
			this._memo = memo;
			this._zX1 = zX1;
			this._zX2 = zX2;
			this._zX3 = zX3;
			this._dispOrder = dispOrder;
			this._visible = visible;
			this._createrID = createrID;
			this._createrName = createrName;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._reaStorage = reaStorage;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "库房名称", ShortCode = "CName", Desc = "库房名称", ContextType = SysDic.All, Length = 20)]
        public virtual string CName
		{
			get { return _cName; }
			set { _cName = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "代码", ShortCode = "ShortCode", Desc = "代码", ContextType = SysDic.All, Length = 8)]
		public virtual string ShortCode
		{
			get { return _shortCode; }
			set { _shortCode = value; }
		}

        [DataMember]
        [DataDesc(CName = "描述", ShortCode = "Memo", Desc = "描述", ContextType = SysDic.All, Length = -1)]
        public virtual string Memo
		{
			get { return _memo; }
			set { _memo = value; }
		}

        [DataMember]
        [DataDesc(CName = "专项1", ShortCode = "ZX1", Desc = "专项1", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX1
		{
			get { return _zX1; }
			set { _zX1 = value; }
		}

        [DataMember]
        [DataDesc(CName = "专项2", ShortCode = "ZX2", Desc = "专项2", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX2
		{
			get { return _zX2; }
			set { _zX2 = value; }
		}

        [DataMember]
        [DataDesc(CName = "专项3", ShortCode = "ZX3", Desc = "专项3", ContextType = SysDic.All, Length = 50)]
        public virtual string ZX3
		{
			get { return _zX3; }
			set { _zX3 = value; }
		}

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "Visible", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
		{
			get { return _visible; }
			set { _visible = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "创建者ID", ShortCode = "CreaterID", Desc = "创建者ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? CreaterID
		{
			get { return _createrID; }
			set { _createrID = value; }
		}

        [DataMember]
        [DataDesc(CName = "创建者姓名", ShortCode = "CreaterName", Desc = "创建者姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string CreaterName
		{
			get { return _createrName; }
			set { _createrName = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "存储库房表", ShortCode = "ReaStorage", Desc = "存储库房表")]
		public virtual ReaStorage ReaStorage
		{
			get { return _reaStorage; }
			set { _reaStorage = value; }
		}
        
		#endregion
	}
	#endregion
}