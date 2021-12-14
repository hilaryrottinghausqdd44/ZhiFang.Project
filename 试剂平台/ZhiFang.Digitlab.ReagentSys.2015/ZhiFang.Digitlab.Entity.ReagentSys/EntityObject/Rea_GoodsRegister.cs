using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity.ReagentSys
{
	#region ReaGoodsRegister

	/// <summary>
	/// ReaGoodsRegister object for NHibernate mapped table 'Rea_GoodsRegister'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "货品注册证表", ClassCName = "ReaGoodsRegister", ShortCode = "ReaGoodsRegister", Desc = "货品注册证表")]
	public class ReaGoodsRegister : BaseEntity
	{
        #region Member Variables

        protected long? _FactoryID;
        protected string _factoryName;
        protected long? _CompID;
        protected string _CompanyName;
        protected string _CompNo;
        protected string _goodsNo;
        protected string _cName;
        protected string _eName;
        protected string _shortCode;
        protected string _goodsLotNo;
        protected string _registerNo;
        protected DateTime? _registerDate;
        protected DateTime? _registerInvalidDate;
        protected string _registerFilePath;
        protected int _dispOrder;
        protected int _visible;
        protected string _zX1;
        protected string _zX2;
        protected string _zX3;
        protected long? _empID;
        protected string _empName;
        protected DateTime? _dataUpdateTime;
		protected ReaGoods _reaGoods;

		#endregion

		#region Constructors

		public ReaGoodsRegister() { }

		public ReaGoodsRegister( long labID, string factoryName, string goodsNo, string cName, string eName, string shortCode, string goodsLotNo, string registerNo, DateTime registerDate, DateTime registerInvalidDate, string registerFilePath, int dispOrder, int visible, string zX1, string zX2, string zX3, long empID, string empName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, ReaGoods reaGoods )
		{
			this._labID = labID;
			this._factoryName = factoryName;
			this._goodsNo = goodsNo;
			this._cName = cName;
			this._eName = eName;
			this._shortCode = shortCode;
			this._goodsLotNo = goodsLotNo;
			this._registerNo = registerNo;
			this._registerDate = registerDate;
			this._registerInvalidDate = registerInvalidDate;
			this._registerFilePath = registerFilePath;
			this._dispOrder = dispOrder;
			this._visible = visible;
			this._zX1 = zX1;
			this._zX2 = zX2;
			this._zX3 = zX3;
			this._empID = empID;
			this._empName = empName;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._reaGoods = reaGoods;
		}

        #endregion

        #region Public Properties

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "厂家ID", ShortCode = "FactoryID", Desc = "厂家ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? FactoryID
        {
            get { return _FactoryID; }
            set { _FactoryID = value; }
        }

        [DataMember]
        [DataDesc(CName = "厂家名称", ShortCode = "FactoryName", Desc = "厂家名称", ContextType = SysDic.All, Length = 200)]
        public virtual string FactoryName
		{
			get { return _factoryName; }
			set { _factoryName = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "供应商ID", ShortCode = "CompID", Desc = "供应商ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? CompID
        {
            get { return _CompID; }
            set { _CompID = value; }
        }

        [DataMember]
        [DataDesc(CName = "供应商名称", ShortCode = "CompanyName", Desc = "供应商名称", ContextType = SysDic.All, Length = 200)]
        public virtual string CompanyName
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
        }

        [DataMember]
        [DataDesc(CName = "供应商编号", ShortCode = "CompNo", Desc = "供应商编号", ContextType = SysDic.All, Length = 200)]
        public virtual string CompNo
        {
            get { return _CompNo; }
            set { _CompNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "产品编号", ShortCode = "GoodsNo", Desc = "产品编号", ContextType = SysDic.All, Length = 50)]
        public virtual string GoodsNo
		{
			get { return _goodsNo; }
			set { _goodsNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "产品中文名", ShortCode = "CName", Desc = "产品中文名", ContextType = SysDic.All, Length = 200)]
        public virtual string CName
		{
			get { return _cName; }
			set { _cName = value; }
		}

        [DataMember]
        [DataDesc(CName = "产品英文名", ShortCode = "EName", Desc = "产品英文名", ContextType = SysDic.All, Length = 200)]
        public virtual string EName
		{
			get { return _eName; }
			set { _eName = value; }
		}

        [DataMember]
        [DataDesc(CName = "产品代码", ShortCode = "ShortCode", Desc = "产品代码", ContextType = SysDic.All, Length = 100)]
        public virtual string ShortCode
		{
			get { return _shortCode; }
			set { _shortCode = value; }
		}

        [DataMember]
        [DataDesc(CName = "产品批号", ShortCode = "GoodsLotNo", Desc = "产品批号", ContextType = SysDic.All, Length = 200)]
        public virtual string GoodsLotNo
		{
			get { return _goodsLotNo; }
			set { _goodsLotNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "注册证编号", ShortCode = "RegisterNo", Desc = "注册证编号", ContextType = SysDic.All, Length = 200)]
        public virtual string RegisterNo
		{
			get { return _registerNo; }
			set { _registerNo = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "注册日期", ShortCode = "RegisterDate", Desc = "注册日期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? RegisterDate
		{
			get { return _registerDate; }
			set { _registerDate = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "注册证有效期", ShortCode = "RegisterInvalidDate", Desc = "注册证有效期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? RegisterInvalidDate
		{
			get { return _registerInvalidDate; }
			set { _registerInvalidDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "注册文件路径", ShortCode = "RegisterFilePath", Desc = "注册文件路径", ContextType = SysDic.All, Length = 500)]
        public virtual string RegisterFilePath
		{
			get { return _registerFilePath; }
			set { _registerFilePath = value; }
		}

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "Visible", Desc = "是否使用", ContextType = SysDic.All, Length = 4)]
        public virtual int Visible
		{
			get { return _visible; }
			set { _visible = value; }
		}

        [DataMember]
        [DataDesc(CName = "专项1", ShortCode = "ZX1", Desc = "专项1", ContextType = SysDic.All, Length = 100)]
        public virtual string ZX1
		{
			get { return _zX1; }
			set { _zX1 = value; }
		}

        [DataMember]
        [DataDesc(CName = "专项2", ShortCode = "ZX2", Desc = "专项2", ContextType = SysDic.All, Length = 100)]
        public virtual string ZX2
		{
			get { return _zX2; }
			set { _zX2 = value; }
		}

        [DataMember]
        [DataDesc(CName = "专项3", ShortCode = "ZX3", Desc = "专项3", ContextType = SysDic.All, Length = 100)]
        public virtual string ZX3
		{
			get { return _zX3; }
			set { _zX3 = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "创建人ID", ShortCode = "EmpID", Desc = "创建人ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? EmpID
		{
			get { return _empID; }
			set { _empID = value; }
		}

        [DataMember]
        [DataDesc(CName = "创建人", ShortCode = "EmpName", Desc = "创建人", ContextType = SysDic.All, Length = 50)]
        public virtual string EmpName
		{
			get { return _empName; }
			set { _empName = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "平台货品表", ShortCode = "ReaGoods", Desc = "平台货品表")]
		public virtual ReaGoods ReaGoods
		{
			get { return _reaGoods; }
			set { _reaGoods = value; }
		}

        
		#endregion
	}
	#endregion
}