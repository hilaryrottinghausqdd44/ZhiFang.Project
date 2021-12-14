using System;
using System.Collections;
using System.Runtime.Serialization;
using Newtonsoft.Json;using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.RBAC
{
	#region HRDeptIdentity

	/// <summary>
	/// HRDeptIdentity object for NHibernate mapped table 'HR_DeptIdentity'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "部门身份", ClassCName = "", ShortCode = "BMSF", Desc = "部门身份")]
    public class HRDeptIdentity : BaseEntity
	{
		#region Member Variables
		
		
		protected int _idenTypeID;
		protected bool _isUse;
		protected int _dispOrder;
		protected DateTime? _dataUpdateTime;
		protected HRDept _hRDept;
        //protected STypeDetail _idenType;
        private string _tSysCode;
        private string _tSysName;
        private long _SystemID;
        private string _SystemCode;
        private string _SystemName;
        #endregion

        #region Constructors

        public HRDeptIdentity() { }

		public HRDeptIdentity( long labID, int idenTypeID, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, HRDept hRDept )
		{
			this._labID = labID;
			this._idenTypeID = idenTypeID;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._hRDept = hRDept;
		}

		#endregion

		#region Public Properties

        [DataMember]
        [DataDesc(CName = "身份类型ID", ShortCode = "SFLXID", Desc = "身份类型ID", ContextType = SysDic.Number, Length = 4)]
		public virtual int IdenTypeID
		{
			get { return _idenTypeID; }
			set { _idenTypeID = value; }
		}
        [DataMember]
        [DataDesc(CName = "身份类型编码", ShortCode = "YGSFLXID", Desc = "身份类型编码", ContextType = SysDic.Number, Length = 8)]
        public virtual string TSysCode
        {
            get { return _tSysCode; }
            set { _tSysCode = value; }
        }
        [DataMember]
        [DataDesc(CName = "身份类型名称", ShortCode = "YGSFLXID", Desc = "身份类型名称", ContextType = SysDic.Number, Length = 8)]
        public virtual string TSysName
        {
            get { return _tSysName; }
            set { _tSysName = value; }
        }
        [DataMember]
        [DataDesc(CName = "身份类型所属系统ID", ShortCode = "YGSFLXID", Desc = "身份类型所属系统ID", ContextType = SysDic.Number, Length = 8)]
        public virtual long SystemID
        {
            get { return _SystemID; }
            set { _SystemID = value; }
        }
        [DataMember]
        [DataDesc(CName = "身份类型所属系统编码", ShortCode = "YGSFLXID", Desc = "身份类型所属系统编码", ContextType = SysDic.Number, Length = 8)]
        public virtual string SystemCode
        {
            get { return _SystemCode; }
            set { _SystemCode = value; }
        }
        [DataMember]
        [DataDesc(CName = "身份类型所属系统名称", ShortCode = "YGSFLXID", Desc = "身份类型所属系统名称", ContextType = SysDic.Number, Length = 8)]
        public virtual string SystemName
        {
            get { return _SystemName; }
            set { _SystemName = value; }
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
        [DataDesc(CName = "部门", ShortCode = "BM", Desc = "部门")]
		public virtual HRDept HRDept
		{
			get { return _hRDept; }
			set { _hRDept = value; }
		}

        //[DataMember]
        //[DataDesc(CName = "系统类型明细表", ShortCode = "IdenType", Desc = "系统类型明细表")]
        //public virtual STypeDetail IdenType
        //{
        //    get { return _idenType; }
        //    set { _idenType = value; }
        //}

		#endregion
	}
	#endregion
}