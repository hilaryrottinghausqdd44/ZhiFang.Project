using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEPTSamplingGroupPrint

	/// <summary>
	/// MEPTSamplingGroupPrint object for NHibernate mapped table 'MEPT_SamplingGroupPrint'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "采样组打印", ClassCName = "MEPTSamplingGroupPrint", ShortCode = "MEPTSamplingGroupPrint", Desc = "采样组打印")]
    public class MEPTSamplingGroupPrint : BaseEntity
	{
		#region Member Variables
		
        
        protected string _cName;
        protected string _sName;
        protected string _printerChannelCode1;
        protected string _printerChannelCode2;
        protected int _printNum;
        protected int _isPasteTube;
        protected DateTime? _dataUpdateTime;
        protected BNodeTable _bNodeTable;
		protected MEPTBarCodePrintType _mEPTBarCodePrintType;
		protected IList<MEPTSamplingGroup> _mEPTSamplingGroupList;

		#endregion

		#region Constructors

		public MEPTSamplingGroupPrint() { }

		public MEPTSamplingGroupPrint( long labID, string cName, string sName, string printerChannelCode1, string printerChannelCode2, int printNum, int isPasteTube, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BNodeTable mEPTBNodeTable, MEPTBarCodePrintType mEPTBarCodePrintType )
		{
			this._labID = labID;
			this._cName = cName;
			this._sName = sName;
			this._printerChannelCode1 = printerChannelCode1;
			this._printerChannelCode2 = printerChannelCode2;
			this._printNum = printNum;
			this._isPasteTube = isPasteTube;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bNodeTable = mEPTBNodeTable;
			this._mEPTBarCodePrintType = mEPTBarCodePrintType;
		}

		#endregion

		#region Public Properties



        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "CName", Desc = "名称", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 50)]
        public virtual string SName
		{
			get { return _sName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SName", value, value.ToString());
				_sName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "打包机通道号1", ShortCode = "PrinterChannelCode1", Desc = "打包机通道号1", ContextType = SysDic.All, Length = 50)]
        public virtual string PrinterChannelCode1
		{
			get { return _printerChannelCode1; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for PrinterChannelCode1", value, value.ToString());
				_printerChannelCode1 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "打包机通道号2", ShortCode = "PrinterChannelCode2", Desc = "打包机通道号2", ContextType = SysDic.All, Length = 50)]
        public virtual string PrinterChannelCode2
		{
			get { return _printerChannelCode2; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for PrinterChannelCode2", value, value.ToString());
				_printerChannelCode2 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "条码打印份数", ShortCode = "PrintNum", Desc = "条码打印份数", ContextType = SysDic.All, Length = 4)]
        public virtual int PrintNum
		{
			get { return _printNum; }
			set { _printNum = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否贴管", ShortCode = "IsPasteTube", Desc = "是否贴管", ContextType = SysDic.All, Length = 4)]
        public virtual int IsPasteTube
		{
			get { return _isPasteTube; }
			set { _isPasteTube = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        

        [DataMember]
        [DataDesc(CName = "站点表", ShortCode = "BNodeTable", Desc = "站点表", ContextType = SysDic.All)]
        public virtual BNodeTable BNodeTable
		{
			get { return _bNodeTable; }
			set { _bNodeTable = value; }
		}

        [DataMember]
        [DataDesc(CName = "条码打印类型", ShortCode = "MEPTBarCodePrintType", Desc = "条码打印类型", ContextType = SysDic.All)]
		public virtual MEPTBarCodePrintType MEPTBarCodePrintType
		{
			get { return _mEPTBarCodePrintType; }
			set { _mEPTBarCodePrintType = value; }
		}

        [DataMember]
        [DataDesc(CName = "采样组设置", ShortCode = "MEPTSamplingGroupList", Desc = "采样组设置", ContextType = SysDic.All)]
		public virtual IList<MEPTSamplingGroup> MEPTSamplingGroupList
		{
			get
			{
				if (_mEPTSamplingGroupList==null)
				{
					_mEPTSamplingGroupList = new List<MEPTSamplingGroup>();
				}
				return _mEPTSamplingGroupList;
			}
			set { _mEPTSamplingGroupList = value; }
		}

		#endregion
	}
	#endregion
}