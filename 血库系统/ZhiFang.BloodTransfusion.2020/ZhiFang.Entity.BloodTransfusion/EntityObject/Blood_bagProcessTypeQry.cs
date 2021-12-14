using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BloodBagProcessTypeQry

	/// <summary>
	/// BloodBagProcessTypeQry object for NHibernate mapped table 'Blood_BagProcessTypeQry'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "血制品加工类型关系", ClassCName = "BloodBagProcessTypeQry", ShortCode = "BloodBagProcessTypeQry", Desc = "血制品加工类型关系")]
	public class BloodBagProcessTypeQry : BaseEntity
	{
		#region Member Variables
		
        //protected string _cName;
        //protected string _sName;
        //protected string _shortCode;
        //protected string _pinYinZiTou;
        protected bool _isUse;
        protected int _dispOrder;
		protected BloodBagProcessType _bloodBagProcessType;
		protected BloodStyle _bloodStyle;

		#endregion

		#region Constructors

		public BloodBagProcessTypeQry() { }

		public BloodBagProcessTypeQry( long labID,  bool isUse, int dispOrder, DateTime dataAddTime, byte[] dataTimeStamp, BloodBagProcessType bloodBagProcessType, BloodStyle bloodStyle )
		{
			this._labID = labID;
			//this._cName = cName;
			//this._sName = sName;
			//this._shortCode = shortCode;
			//this._pinYinZiTou = pinYinZiTou;
			this._isUse = isUse;
			this._dispOrder = dispOrder;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bloodBagProcessType = bloodBagProcessType;
			this._bloodStyle = bloodStyle;
		}

		#endregion

		#region Public Properties



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
        [DataDesc(CName = "加工类型表", ShortCode = "BloodBagProcessType", Desc = "加工类型表")]
		public virtual BloodBagProcessType BloodBagProcessType
		{
			get { return _bloodBagProcessType; }
			set { _bloodBagProcessType = value; }
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