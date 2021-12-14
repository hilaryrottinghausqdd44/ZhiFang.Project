using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region SCBagOperation

	/// <summary>
	/// SCBagOperation object for NHibernate mapped table 'SC_BagOperation'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "血袋操作记录表 入库、复检、配血、发血、领用、接收、输注、回收、销毁、退库、回退血站", ClassCName = "SCBagOperation", ShortCode = "SCBagOperation", Desc = "血袋操作记录表 入库、复检、配血、发血、领用、接收、输注、回收、销毁、退库、回退血站")]
	public class SCBagOperation : BaseEntity
	{
		#region Member Variables
		
        protected long? _typeId;
        protected string _businessModuleCode;
        protected long? _bobjectID;
        protected long? _bobjectDtlID;
        protected int _dispOrder;
        protected bool _visible;
        protected long? _operatorID;
        protected string _operatorName;
        protected string _memo;
		protected BloodQtyDtl _bloodQtyDtl;
		protected BloodStyle _bloodStyle;

		#endregion

		#region Constructors

		public SCBagOperation() { }

		public SCBagOperation( long labID, long typeId, string businessModuleCode, long bobjectID, long bobjectDtlID, int dispOrder, bool visible, long operatorID, string operatorName, string memo, DateTime dataAddTime, byte[] dataTimeStamp, BloodQtyDtl bloodQtyDtl, BloodStyle bloodStyle )
		{
			this._labID = labID;
			this._typeId = typeId;
			this._businessModuleCode = businessModuleCode;
			this._bobjectID = bobjectID;
			this._bobjectDtlID = bobjectDtlID;
			this._dispOrder = dispOrder;
			this._visible = visible;
			this._operatorID = operatorID;
			this._operatorName = operatorName;
			this._memo = memo;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bloodQtyDtl = bloodQtyDtl;
			this._bloodStyle = bloodStyle;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "操作类型Id", ShortCode = "TypeId", Desc = "操作类型Id", ContextType = SysDic.All, Length = 8)]
		public virtual long? TypeId
		{
			get { return _typeId; }
			set { _typeId = value; }
		}

        [DataMember]
        [DataDesc(CName = "业务模块代码", ShortCode = "BusinessModuleCode", Desc = "业务模块代码", ContextType = SysDic.All, Length = 20)]
        public virtual string BusinessModuleCode
		{
			get { return _businessModuleCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for BusinessModuleCode", value, value.ToString());
				_businessModuleCode = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "业务主单对象ID", ShortCode = "BobjectID", Desc = "业务主单对象ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? BobjectID
		{
			get { return _bobjectID; }
			set { _bobjectID = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "业务明细对象ID", ShortCode = "BobjectDtlID", Desc = "业务明细对象ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? BobjectDtlID
		{
			get { return _bobjectDtlID; }
			set { _bobjectDtlID = value; }
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
        [DataDesc(CName = "操作者", ShortCode = "OperatorID", Desc = "操作者", ContextType = SysDic.All, Length = 8)]
		public virtual long? OperatorID
		{
			get { return _operatorID; }
			set { _operatorID = value; }
		}

        [DataMember]
        [DataDesc(CName = "操作人姓名", ShortCode = "OperatorName", Desc = "操作人姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string OperatorName
		{
			get { return _operatorName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for OperatorName", value, value.ToString());
				_operatorName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All)]
        public virtual string Memo
		{
			get { return _memo; }
			set
			{
				_memo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "库存表", ShortCode = "BloodQtyDtl", Desc = "库存表")]
		public virtual BloodQtyDtl BloodQtyDtl
		{
			get { return _bloodQtyDtl; }
			set { _bloodQtyDtl = value; }
		}

        [DataMember]
        [DataDesc(CName = "血制品字典", ShortCode = "BloodStyle", Desc = "血制品字典")]
		public virtual BloodStyle BloodStyle
		{
			get { return _bloodStyle; }
			set { _bloodStyle = value; }
		}

        
		#endregion
	}
	#endregion
}