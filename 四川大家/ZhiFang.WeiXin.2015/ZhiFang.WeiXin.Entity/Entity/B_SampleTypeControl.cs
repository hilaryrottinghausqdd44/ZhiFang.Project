using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
	#region BSampleTypeControl

	/// <summary>
	/// BSampleTypeControl object for NHibernate mapped table 'B_SampleTypeControl'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BSampleTypeControl", ShortCode = "BSampleTypeControl", Desc = "")]
	public class BSampleTypeControl : BaseEntity
	{
		#region Member Variables
		
        protected string _sampleTypeControlNo;
        protected int _sampleTypeNo;
        protected string _controlLabNo;
        protected int _controlSampleTypeNo;
        //protected byte[] _dTimeStampe;
        //protected DateTime? _addTime;
        protected int _useFlag;
		

		#endregion

		#region Constructors

		public BSampleTypeControl() { }

		public BSampleTypeControl( string sampleTypeControlNo, int sampleTypeNo, string controlLabNo, int controlSampleTypeNo, byte[] dTimeStampe, DateTime addTime, int useFlag )
		{
			this._sampleTypeControlNo = sampleTypeControlNo;
			this._sampleTypeNo = sampleTypeNo;
			this._controlLabNo = controlLabNo;
			this._controlSampleTypeNo = controlSampleTypeNo;
			//this._dTimeStampe = dTimeStampe;
			//this._addTime = addTime;
			this._useFlag = useFlag;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "SampleTypeControlNo", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual string SampleTypeControlNo
        {
			get { return _sampleTypeControlNo; }
			set { _sampleTypeControlNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SampleTypeNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int SampleTypeNo
		{
			get { return _sampleTypeNo; }
			set { _sampleTypeNo = value; }
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
        [DataDesc(CName = "", ShortCode = "ControlSampleTypeNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ControlSampleTypeNo
		{
			get { return _controlSampleTypeNo; }
			set { _controlSampleTypeNo = value; }
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