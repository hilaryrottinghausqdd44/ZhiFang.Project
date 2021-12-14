using System;
using System.Collections;
using System.Runtime.Serialization;
using Newtonsoft.Json;using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.RBAC
{
	#region HREmpIdentity

	/// <summary>
	/// HREmpIdentity object for NHibernate mapped table 'HR_EmpIdentity'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "员工身份", ClassCName = "", ShortCode = "YGSF", Desc = "员工身份")]
    public class HREmpIdentity : BaseEntity
	{
		#region Member Variables
		
		
		protected long _tSysID;
		protected bool _isUse;
		protected int _dispOrder;
		protected DateTime? _dataUpdateTime;
		protected HREmployee _hREmployee;
        protected STypeDetail _sTypeDetail;

		#endregion

		#region Constructors

		public HREmpIdentity() { }

		public HREmpIdentity( long labID, long tSysID, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, HREmployee hREmployee )
		{
			this._labID = labID;
			this._tSysID = tSysID;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._hREmployee = hREmployee;
		}

		#endregion

		#region Public Properties

        [DataMember]
        [DataDesc(CName = "员工身份类型ID", ShortCode = "YGSFLXID", Desc = "员工身份类型ID", ContextType = SysDic.Number, Length = 8)]
		public virtual long TSysID
		{
			get { return _tSysID; }
			set { _tSysID = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "SFSY", Desc = "是否使用", ContextType = SysDic.All)]
		public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "XSCX", Desc = "显示次序", ContextType = SysDic.Number, Length = 4)]
		public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "SJGXSJ", Desc = "数据更新时间", ContextType = SysDic.DateTime)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        
        [DataMember]
        [DataDesc(CName = "员工", ShortCode = "YG", Desc = "员工")]
		public virtual HREmployee HREmployee
		{
			get { return _hREmployee; }
			set { _hREmployee = value; }
		}

        [DataMember]
        [DataDesc(CName = "系统类型明细表", ShortCode = "STypeDetail", Desc = "系统类型明细表")]
        public virtual STypeDetail STypeDetail
        {
            get { return _sTypeDetail; }
            set { _sTypeDetail = value; }
        }
		#endregion
	}
	#endregion
}