using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
	#region BLabSampleType

	/// <summary>
	/// BLabSampleType object for NHibernate mapped table 'B_Lab_SampleType'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BLabSampleType", ShortCode = "BLabSampleType", Desc = "")]
	public class BLabSampleType : BaseEntity
	{
		#region Member Variables
		
        protected int _labSampleTypeNo;
        protected string _labCode;
        protected string _cName;
        protected string _shortCode;
        protected int _visible;
        protected int _dispOrder;
        protected string _hisOrderCode;
        //protected byte[] _dTimeStampe;
       // protected DateTime? _addTime;
        protected string _standCode;
        protected string _zFStandCode;
        protected int _useFlag;
        protected string _code1;
        protected string _code2;
        protected string _code3;
		

		#endregion

		#region Constructors

		public BLabSampleType() { }

		public BLabSampleType( int labSampleTypeNo, string labCode,string cName, string shortCode, int visible, int dispOrder, string hisOrderCode, byte[] dTimeStampe, DateTime addTime, string standCode, string zFStandCode, int useFlag, string code1, string code2, string code3 )
		{
            this._labCode = labCode;
            this._labSampleTypeNo = labSampleTypeNo;
			this._cName = cName;
			this._shortCode = shortCode;
			this._visible = visible;
			this._dispOrder = dispOrder;
			this._hisOrderCode = hisOrderCode;
			//this._dTimeStampe = dTimeStampe;
			//this._addTime = addTime;
			this._standCode = standCode;
			this._zFStandCode = zFStandCode;
			this._useFlag = useFlag;
			this._code1 = code1;
			this._code2 = code2;
			this._code3 = code3;
		}

        #endregion

        #region Public Properties

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "labCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string LabCode
        {
            get { return _labCode; }
            set { _labCode = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "LabSampleTypeNo", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual int LabSampleTypeNo
        {
			get { return _labSampleTypeNo; }
			set { _labSampleTypeNo = value; }
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
				if ( value != null && value.Length > 20)
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

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Code1", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Code1
		{
			get { return _code1; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Code1", value, value.ToString());
				_code1 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Code2", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Code2
		{
			get { return _code2; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Code2", value, value.ToString());
				_code2 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Code3", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Code3
		{
			get { return _code3; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Code3", value, value.ToString());
				_code3 = value;
			}
		}

		
		#endregion
	}
	#endregion
}