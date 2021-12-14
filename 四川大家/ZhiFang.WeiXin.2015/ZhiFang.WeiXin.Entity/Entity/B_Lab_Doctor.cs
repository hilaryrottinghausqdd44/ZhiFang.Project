using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
	#region BLabDoctor

	/// <summary>
	/// BLabDoctor object for NHibernate mapped table 'B_Lab_Doctor'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BLabDoctor", ShortCode = "BLabDoctor", Desc = "")]
	public class BLabDoctor : BaseEntity
	{
        #region Member Variables

        protected string _labCode;
        protected long _labDoctorNo;
        protected string _cName;
        protected string _shortCode;
        protected string _hisOrderCode;
        protected int _visible;
        //protected byte[] _dTimeStampe;
        //protected DateTime? _addTime;
        protected string _standCode;
        protected string _zFStandCode;
        protected int _useFlag;
        //protected int _dispOrder;

        #endregion

        #region Constructors

        public BLabDoctor() { }

		public BLabDoctor(string labCode, long labDoctorNo, string cName, string shortCode, string hisOrderCode, int visible, byte[] dTimeStampe, DateTime addTime, string standCode, string zFStandCode, int useFlag,int dispOrder)
		{
            this._labCode = labCode;
            this._labDoctorNo = labDoctorNo;
			this._cName = cName;
			this._shortCode = shortCode;
			this._hisOrderCode = hisOrderCode;
			this._visible = visible;
			//this._dTimeStampe = dTimeStampe;
			//this._addTime = addTime;
			this._standCode = standCode;
			this._zFStandCode = zFStandCode;
			this._useFlag = useFlag;
            //this._dispOrder = dispOrder;

        }

        #endregion

        #region Public Properties

        [DataMember]
        [DataDesc(CName = "", ShortCode = "LabCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string LabCode
        {
            get { return _labCode; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
                _labCode = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "LabDoctorNo", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long LabDoctorNo
        {
			get { return _labDoctorNo; }
			set { _labDoctorNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string CName
		{
			get { return _cName; }
			set
			{
				if ( value != null && value.Length > 50)
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
        [DataDesc(CName = "", ShortCode = "HisOrderCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string HisOrderCode
		{
			get { return _hisOrderCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for HisOrderCode", value, value.ToString());
				_hisOrderCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Visible", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Visible
		{
			get { return _visible; }
			set { _visible = value; }
		}

        //[DataMember]
        //[DataDesc(CName = "", ShortCode = "DTimeStampe", Desc = "", ContextType = SysDic.All, Length = 8)]
  //      public virtual byte[] DTimeStampe
		//{
		//	get { return _dTimeStampe; }
		//	set { _dTimeStampe = value; }
		//}

  //      [DataMember]
  //      [JsonConverter(typeof(JsonConvertClass))]
  //      [DataDesc(CName = "", ShortCode = "AddTime", Desc = "", ContextType = SysDic.All, Length = 8)]
  //      public virtual DateTime? AddTime
		//{
		//	get { return _addTime; }
		//	set { _addTime = value; }
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

        /*[DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }*/
        #endregion
    }
	#endregion
}