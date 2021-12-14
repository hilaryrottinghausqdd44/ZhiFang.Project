using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodClassUnitLink

	/// <summary>
	/// BloodClassUnitLink object for NHibernate mapped table 'Blood_ClassUnitLink'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "血制品分类的单位换算关系表", ClassCName = "BloodClassUnitLink", ShortCode = "BloodClassUnitLink", Desc = "血制品分类的单位换算关系表")]
	public class BloodClassUnitLink : BaseEntity
	{
		#region Member Variables
		
        protected double _bloodScale;
        protected bool _isCalcUnit;
        protected bool _isUse;
        protected int _dispOrder;
        protected string _memo;
		protected BloodClass _bloodClass;
		protected BloodUnit _bloodUnit;

		#endregion

		#region Constructors

		public BloodClassUnitLink() { }

		public BloodClassUnitLink( long labID, double bloodScale, bool isCalcUnit, bool isUse, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp, string memo, BloodClass bloodClass, BloodUnit bloodUnit )
		{
			this._labID = labID;
			this._bloodScale = bloodScale;
			this._isCalcUnit = isCalcUnit;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._memo = memo;
			this._bloodClass = bloodClass;
			this._bloodUnit = bloodUnit;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "换算比例", ShortCode = "BloodScale", Desc = "换算比例", ContextType = SysDic.All, Length = 8)]
        public virtual double BloodScale
		{
			get { return _bloodScale; }
			set { _bloodScale = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否计算单位(基本单位、显示单位)", ShortCode = "IsCalcUnit", Desc = "是否计算单位(基本单位、显示单位)", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsCalcUnit
		{
			get { return _isCalcUnit; }
			set { _isCalcUnit = value; }
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
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = 16)]
        public virtual string Memo
		{
			get { return _memo; }
			set
			{
				if ( value != null && value.Length > 16)
					throw new ArgumentOutOfRangeException("Invalid value for Memo", value, value.ToString());
				_memo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "血袋分类", ShortCode = "BloodClass", Desc = "血袋分类")]
		public virtual BloodClass BloodClass
		{
			get { return _bloodClass; }
			set { _bloodClass = value; }
		}

        [DataMember]
        [DataDesc(CName = "血制品单位", ShortCode = "BloodUnit", Desc = "血制品单位")]
		public virtual BloodUnit BloodUnit
		{
			get { return _bloodUnit; }
			set { _bloodUnit = value; }
		}

        
		#endregion
	}
	#endregion
}