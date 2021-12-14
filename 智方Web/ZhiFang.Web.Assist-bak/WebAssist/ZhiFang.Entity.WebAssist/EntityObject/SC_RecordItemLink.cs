using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.WebAssist
{
	#region SCRecordItemLink

	/// <summary>
	/// SCRecordItemLink object for NHibernate mapped table 'SC_RecordItemLink'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "记录项类型与记录项字典关系", ClassCName = "SCRecordItemLink", ShortCode = "SCRecordItemLink", Desc = "记录项类型与记录项字典关系")]
	public class SCRecordItemLink : BaseEntity
	{
		#region Member Variables
		protected string _testItemCode;
		protected int _dispOrder;
        protected bool _isUse;
		protected bool _isBillVisible;
		protected SCRecordType _sCRecordType;
		protected SCRecordTypeItem _sCRecordTypeItem;

		#endregion

		#region Constructors

		public SCRecordItemLink() { }

		public SCRecordItemLink( long labID, int dispOrder, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp, SCRecordType sCRecordType, SCRecordTypeItem sCRecordTypeItem )
		{
			this._labID = labID;
			this._dispOrder = dispOrder;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
			this._sCRecordType = sCRecordType;
			this._sCRecordTypeItem = sCRecordTypeItem;
		}

		#endregion

		#region Public Properties
		[DataMember]
		[DataDesc(CName = "IsBillVisible", ShortCode = "IsBillVisible", Desc = "IsBillVisible", ContextType = SysDic.All, Length = 1)]
		public virtual bool IsBillVisible
		{
			get { return _isBillVisible; }
			set { _isBillVisible = value; }
		}

		[DataMember]
		[DataDesc(CName = "TestItemCode", ShortCode = "TestItemCode", Desc = "TestItemCode", ContextType = SysDic.All, Length = 60)]
		public virtual string TestItemCode
		{
			get { return _testItemCode; }
			set { _testItemCode = value; }
		}

		[DataMember]
        [DataDesc(CName = "记录项类型Id", ShortCode = "DispOrder", Desc = "记录项类型Id", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsUse", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SCRecordType", Desc = "")]
		public virtual SCRecordType SCRecordType
		{
			get { return _sCRecordType; }
			set { _sCRecordType = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SCRecordTypeItem", Desc = "")]
		public virtual SCRecordTypeItem SCRecordTypeItem
		{
			get { return _sCRecordTypeItem; }
			set { _sCRecordTypeItem = value; }
		}


		#endregion

		#region 自定义属性

		[DataMember]
		[DataDesc(CName = "TestItemCName", ShortCode = "TestItemCName", Desc = "TestItemCName", ContextType = SysDic.All, Length = 120)]
		public virtual string TestItemCName { get; set; }

		#endregion
	}
	#endregion
}