using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
	#region BDoctorControl

	/// <summary>
	/// BDoctorControl object for NHibernate mapped table 'B_DoctorControl'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BDoctorControl", ShortCode = "BDoctorControl", Desc = "")]
	public class BDoctorControl : BaseEntity
	{
		#region Member Variables
		
        protected string _doctorControlNo;
        protected long _doctorNo;
        protected string _controlLabNo;
        protected long _controlDoctorNo;
    //    protected byte[] _dTimeStampe;
        //protected DateTime? _addTime;
        protected int _useFlag;
		

		#endregion

		#region Constructors

		public BDoctorControl() { }

		public BDoctorControl( string doctorControlNo, long doctorNo, string controlLabNo, long controlDoctorNo, byte[] dTimeStampe, DateTime addTime, int useFlag )
		{
			this._doctorControlNo = doctorControlNo;
			this._doctorNo = doctorNo;
			this._controlLabNo = controlLabNo;
			this._controlDoctorNo = controlDoctorNo;
			//this._dTimeStampe = dTimeStampe;
			//this._addTime = addTime;
			this._useFlag = useFlag;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DoctorControlNo", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual string DoctorControlNo
        {
			get { return _doctorControlNo; }
			set { _doctorControlNo = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DoctorNo", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long DoctorNo
		{
			get { return _doctorNo; }
			set { _doctorNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ControlLabNo", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string ControlLabNo
		{
			get { return _controlLabNo; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ControlLabNo", value, value.ToString());
				_controlLabNo = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "ControlDoctorNo", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long ControlDoctorNo
		{
			get { return _controlDoctorNo; }
			set { _controlDoctorNo = value; }
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