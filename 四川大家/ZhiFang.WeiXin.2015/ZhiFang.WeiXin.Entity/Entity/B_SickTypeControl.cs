using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
	#region BSickTypeControl

	/// <summary>
	/// BSickTypeControl object for NHibernate mapped table 'B_SickTypeControl'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "BSickTypeControl", ShortCode = "BSickTypeControl", Desc = "")]
	public class BSickTypeControl : BaseEntity
	{
		#region Member Variables
		
        protected string _sickTypeControlNo; 
        protected int _sickTypeNo;
        protected string _controlLabNo;
        protected int _controlSickTypeNo;
       // protected byte[] _dTimeStampe;
       // protected DateTime? _addTime;
        protected int _useFlag;
		

		#endregion

		#region Constructors

		public BSickTypeControl() { }

		public BSickTypeControl(string sickTypeControlNo, int sickTypeNo, string controlLabNo, int controlSickTypeNo, byte[] dTimeStampe, DateTime addTime, int useFlag )
		{
			this._sickTypeControlNo = sickTypeControlNo;
			this._sickTypeNo = sickTypeNo;
			this._controlLabNo = controlLabNo;
			this._controlSickTypeNo = controlSickTypeNo;
			//this._dTimeStampe = dTimeStampe;
			this._dataAddTime = addTime;
			this._useFlag = useFlag;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "SickTypeControlNo", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual string SickTypeControlNo
		{
			get { return _sickTypeControlNo; }
			set { _sickTypeControlNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SickTypeNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int SickTypeNo
		{
			get { return _sickTypeNo; }
			set { _sickTypeNo = value; }
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
        [DataDesc(CName = "", ShortCode = "ControlSickTypeNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ControlSickTypeNo
		{
			get { return _controlSickTypeNo; }
			set { _controlSickTypeNo = value; }
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