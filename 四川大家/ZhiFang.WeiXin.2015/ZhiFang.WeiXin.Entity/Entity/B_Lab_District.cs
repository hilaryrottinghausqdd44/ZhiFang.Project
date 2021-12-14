using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
	#region BLabDistrict

	/// <summary>
	/// BLabDistrict object for NHibernate mapped table 'B_Lab_District'.
	/// </summary>
	[DataContract]
	[DataDesc(CName = "", ClassCName = "BLabDistrict", ShortCode = "BLabDistrict", Desc = "")]
	public class BLabDistrict : BaseEntity
	{
		#region Member Variables

		protected int _districtID;
		protected string _cName;
		protected string _shortName;
		protected string _labCode;
		protected string _labDistrictNo;
		protected string _shortCode;
		protected int _visible;
		protected int _dispOrder;
		protected string _hisOrderCode;
		protected byte[] _dTimeStampe;
		protected DateTime? _addTime;
		protected string _standCode;
		protected string _zFStandCode;
		protected int _useFlag;


		#endregion

		#region Constructors

		public BLabDistrict() { }

		public BLabDistrict(int districtID, string cName, string shortName, string shortCode, int visible, int dispOrder, string hisOrderCode, byte[] dTimeStampe, DateTime addTime, string standCode, string zFStandCode, int useFlag)
		{
			this._districtID = districtID;
			this._cName = cName;
			this._shortName = shortName;
			this._shortCode = shortCode;
			this._visible = visible;
			this._dispOrder = dispOrder;
			this._hisOrderCode = hisOrderCode;
			this._dTimeStampe = dTimeStampe;
			this._addTime = addTime;
			this._standCode = standCode;
			this._zFStandCode = zFStandCode;
			this._useFlag = useFlag;
		}

		#endregion

		#region Public Properties


		[DataMember]
		[DataDesc(CName = "", ShortCode = "DistrictID", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int DistrictID
		{
			get { return _districtID; }
			set { _districtID = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "CName", Desc = "", ContextType = SysDic.All, Length = 20)]
		public virtual string CName
		{
			get { return _cName; }
			set
			{
				if (value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
				_cName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "ShortName", Desc = "", ContextType = SysDic.All, Length = 10)]
		public virtual string ShortName
		{
			get { return _shortName; }
			set
			{
				if (value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for ShortName", value, value.ToString());
				_shortName = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "ShortCode", Desc = "", ContextType = SysDic.All, Length = 10)]
		public virtual string ShortCode
		{
			get { return _shortCode; }
			set
			{
				if (value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for ShortCode", value, value.ToString());
				_shortCode = value;
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
		[DataDesc(CName = "", ShortCode = "HisOrderCode", Desc = "", ContextType = SysDic.All, Length = 20)]
		public virtual string HisOrderCode
		{
			get { return _hisOrderCode; }
			set
			{
				if (value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for HisOrderCode", value, value.ToString());
				_hisOrderCode = value;
			}
		}


		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "", ShortCode = "AddTime", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? AddTime
		{
			get { return _addTime; }
			set { _addTime = value; }
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "StandCode", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string StandCode
		{
			get { return _standCode; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for StandCode", value, value.ToString());
				_standCode = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "ZFStandCode", Desc = "", ContextType = SysDic.All, Length = 50)]
		public virtual string ZFStandCode
		{
			get { return _zFStandCode; }
			set
			{
				if (value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ZFStandCode", value, value.ToString());
				_zFStandCode = value;
			}
		}

		[DataMember]
		[DataDesc(CName = "", ShortCode = "UseFlag", Desc = "", ContextType = SysDic.All, Length = 4)]
		public virtual int UseFlag
		{
			get { return _useFlag; }
			set { _useFlag = value; }
		}
		[DataMember]
		public virtual string LabCode { get => _labCode; set => _labCode = value; }
		[DataMember]
		public virtual string LabDistrictNo { get => _labDistrictNo; set => _labDistrictNo = value; }


		#endregion
	}
	#endregion
}