using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region TestEquip

	/// <summary>
	/// TestEquip object for NHibernate mapped table 'TestEquip'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "TestEquip", ShortCode = "TestEquip", Desc = "")]
	public class TestEquip : BaseEntityService
    {
		#region Member Variables
		
        protected long _deptID;
        protected string _cName;
        protected string _eName;
        protected string _shortCode;
        protected string _memo;
        protected int _visible;
        protected int _dispOrder;
        protected string _lisCode;
        protected DateTime _dataUpdateTime;
		

		#endregion

		#region Constructors

		public TestEquip() { }

		public TestEquip( long labID, long deptID, string cName, string eName, string shortCode, string memo, int visible, int dispOrder, string lisCode, DateTime dataUpdateTime )
		{
			this._labID = labID;
			this._deptID = deptID;
			this._cName = cName;
			this._eName = eName;
			this._shortCode = shortCode;
			this._memo = memo;
			this._visible = visible;
			this._dispOrder = dispOrder;
			this._lisCode = lisCode;
			this._dataUpdateTime = dataUpdateTime;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DeptID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long DeptID
		{
			get { return _deptID; }
			set { _deptID = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CName", Desc = "", ContextType = SysDic.All, Length = 300)]
        public virtual string CName
		{
			get { return _cName; }
			set
			{
				if ( value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
				_cName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "EName", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string EName
		{
			get { return _eName; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for EName", value, value.ToString());
				_eName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ShortCode", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ShortCode
		{
			get { return _shortCode; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for ShortCode", value, value.ToString());
				_shortCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Memo", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string Memo
		{
			get { return _memo; }
			set
			{
				if ( value != null && value.Length > 500)
					throw new ArgumentOutOfRangeException("Invalid value for Memo", value, value.ToString());
				_memo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Visible", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Visible
		{
			get { return _visible; }
			set { _visible = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LisCode", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string LisCode
		{
			get { return _lisCode; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for LisCode", value, value.ToString());
				_lisCode = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

		
		#endregion
	}
	#endregion
}