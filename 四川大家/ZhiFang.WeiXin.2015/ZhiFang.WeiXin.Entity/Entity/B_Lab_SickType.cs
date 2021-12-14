using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
	#region BLabSickType

	/// <summary>
	/// BLabSickType object for NHibernate mapped table 'B_Lab_SickType'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BLabSickType", ShortCode = "BLabSickType", Desc = "")]
	public class BLabSickType : BaseEntity
	{
        #region Member Variables

        protected string _labCode; 
        protected int _labSickTypeNo;
        protected string _cName;
        protected string _shortCode;
        protected int _dispOrder;
        protected string _hisOrderCode;
        //protected byte[] _dTimeStampe;
        //protected DateTime? _addTime;
        protected string _standCode;
        protected string _zFStandCode;
        protected int _useFlag;
		

		#endregion

		#region Constructors

		public BLabSickType() { }

		public BLabSickType( string labCode, int labSickTypeNo, string cName, string shortCode, int dispOrder, string hisOrderCode, byte[] dTimeStampe, DateTime addTime, string standCode, string zFStandCode, int useFlag )
		{
            this._labCode= labCode; 
			this._labSickTypeNo = labSickTypeNo;
			this._cName = cName;
			this._shortCode = shortCode;
			this._dispOrder = dispOrder;
			this._hisOrderCode = hisOrderCode;
			//this._dTimeStampe = dTimeStampe;
			this._dataAddTime = addTime;
			this._standCode = standCode;
			this._zFStandCode = zFStandCode;
			this._useFlag = useFlag;
		}

        #endregion

        #region Public Properties

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ShortCode", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string LabCode
        {
            get { return _labCode; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for LabCode", value, value.ToString());
                _labCode = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "LabSickTypeNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int LabSickTypeNo
        {
			get { return _labSickTypeNo; }
			set { _labSickTypeNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CName", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string CName
		{
			get { return _cName; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
				_cName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ShortCode", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string ShortCode
		{
			get { return _shortCode; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for ShortCode", value, value.ToString());
				_shortCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HisOrderCode", Desc = "", ContextType = SysDic.All, Length = 21)]
        public virtual string HisOrderCode
		{
			get { return _hisOrderCode; }
			set
			{
				if ( value != null && value.Length > 21)
					throw new ArgumentOutOfRangeException("Invalid value for HisOrderCode", value, value.ToString());
				_hisOrderCode = value;
			}
		}

  //      [DataMember]
  //      [DataDesc(CName = "", ShortCode = "DTimeStampe", Desc = "", ContextType = SysDic.All, Length = 8)]
  //      public virtual byte[] DTimeStampe
		//{
		//	get { return _dTimeStampe; }
		//	set { _dTimeStampe = value; }
		//}

  //      [DataMember]
  //      [JsonConverter(typeof(JsonConvertClass))]
  //      [DataDesc(CName = "", ShortCode = "AddTime", Desc = "", ContextType = SysDic.All, Length = 8)]
  //      public virtual DateTime? DataAddTime
  //      {
		//	get { return _dataAddTime; }
		//	set { _dataAddTime = value; }
		//}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "StandCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string StandCode
		{
			get { return _standCode; }
			set
			{
				if ( value != null && value.Length > 50)
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
				if ( value != null && value.Length > 50)
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

		
		#endregion
	}
	#endregion
}