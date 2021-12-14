using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity.ReagentSys
{
	#region TestEquipLab

	/// <summary>
	/// TestEquipLab object for NHibernate mapped table 'TestEquipLab'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "实验室检验仪器表", ClassCName = "TestEquipLab", ShortCode = "TestEquipLab", Desc = "")]
	public class TestEquipLab : BaseEntity
	{
		#region Member Variables
		
        protected string _cName;
        protected string _eName;
        protected string _shortCode;
        protected string _memo;
        protected int _visible;
        protected int _dispOrder;
        protected string _lisCode;
        protected DateTime? _dataUpdateTime;
		protected CenOrg _comp;
		protected CenOrg _lab;
		protected CenOrg _prod;
		protected TestEquipType _testEquipType;
		protected TestEquipProd _testEquipProd;

		#endregion

		#region Constructors

		public TestEquipLab() { }

		public TestEquipLab( string cName, string eName, string shortCode, string memo, int visible, int dispOrder, string lisCode, DateTime? dataUpdateTime, CenOrg comp, CenOrg lab, CenOrg prod, TestEquipType testEquipType, TestEquipProd testEquipProd )
		{
			this._cName = cName;
			this._eName = eName;
			this._shortCode = shortCode;
			this._memo = memo;
			this._visible = visible;
			this._dispOrder = dispOrder;
			this._lisCode = lisCode;
			this._dataUpdateTime = dataUpdateTime;
			this._comp = comp;
			this._lab = lab;
			this._prod = prod;
			this._testEquipType = testEquipType;
			this._testEquipProd = testEquipProd;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "中文名称", ShortCode = "CName", Desc = "中文名称", ContextType = SysDic.All, Length = 300)]
        public virtual string CName
		{
			get { return _cName; }
			set { _cName = value; }
		}

        [DataMember]
        [DataDesc(CName = "英文名称", ShortCode = "EName", Desc = "英文名称", ContextType = SysDic.All, Length = 200)]
        public virtual string EName
		{
			get { return _eName; }
			set { _eName = value; }
		}

        [DataMember]
        [DataDesc(CName = "代码", ShortCode = "ShortCode", Desc = "代码", ContextType = SysDic.All, Length = 100)]
        public virtual string ShortCode
		{
			get { return _shortCode; }
			set { _shortCode = value; }
		}

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = 500)]
        public virtual string Memo
		{
			get { return _memo; }
			set { _memo = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "Visible", Desc = "是否使用", ContextType = SysDic.All, Length = 4)]
        public virtual int Visible
		{
			get { return _visible; }
			set { _visible = value; }
		}

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "LIS仪器编号", ShortCode = "LisCode", Desc = "LIS仪器编号", ContextType = SysDic.All, Length = 100)]
        public virtual string LisCode
		{
			get { return _lisCode; }
			set { _lisCode = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "修改时间", ShortCode = "DataUpdateTime", Desc = "修改时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Comp", Desc = "")]
		public virtual CenOrg Comp
		{
			get { return _comp; }
			set { _comp = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Lab", Desc = "")]
		public virtual CenOrg Lab
		{
			get { return _lab; }
			set { _lab = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ProdOrg", Desc = "")]
		public virtual CenOrg ProdOrg
		{
			get { return _prod; }
			set { _prod = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TestEquipType", Desc = "")]
		public virtual TestEquipType TestEquipType
		{
			get { return _testEquipType; }
			set { _testEquipType = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TestEquipProd", Desc = "")]
		public virtual TestEquipProd TestEquipProd
		{
			get { return _testEquipProd; }
			set { _testEquipProd = value; }
		}

        
		#endregion
	}
	#endregion
}