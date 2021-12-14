using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEPTSamplingGroup

	/// <summary>
	/// MEPTSamplingGroup object for NHibernate mapped table 'MEPT_SamplingGroup'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "采样组设置", ClassCName = "MEPTSamplingGroup", ShortCode = "MEPTSamplingGroup", Desc = "采样组设置")]
    public class MEPTSamplingGroup : BaseEntity
	{
		#region Member Variables
		
        
        protected string _cName;
        protected string _eName;
        protected string _sName;
        protected string _shortcode;
        protected int _dispOrder;
        protected string _comment;
        protected bool _isUse;
        protected string _destination;
        protected DateTime? _dataUpdateTime;
        protected MEPTSamplingGroupPrint _mEPTSamplingGroupPrint;
		protected BSpecialty _bSpecialty;
		protected MEPTSamplingTube _mEPTSamplingTube;
		protected BSampleType _bSampleType;
		protected IList<MEPTItemSplit> _mEPTItemSplitList;
		protected IList<MEPTSamplingItem> _mEPTSamplingItemList;

		#endregion

		#region Constructors

		public MEPTSamplingGroup() { }

		public MEPTSamplingGroup( long labID, string cName, string eName, string sName, string shortcode, int dispOrder, string comment, bool isUse, string destination, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, MEPTSamplingGroupPrint mEPTSamplingGroupPrint, BSpecialty mEPTBSpecialty, MEPTSamplingTube mEPTSamplingTube, BSampleType mEBSampleType )
		{
			this._labID = labID;
			this._cName = cName;
			this._eName = eName;
			this._sName = sName;
			this._shortcode = shortcode;
			this._dispOrder = dispOrder;
			this._comment = comment;
			this._isUse = isUse;
			this._destination = destination;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._mEPTSamplingGroupPrint = mEPTSamplingGroupPrint;
			this._bSpecialty = mEPTBSpecialty;
			this._mEPTSamplingTube = mEPTSamplingTube;
			this._bSampleType = mEBSampleType;
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
        [DataDesc(CName = "英文名称", ShortCode = "EName", Desc = "英文名称", ContextType = SysDic.All, Length = 50)]
        public virtual string EName
		{
			get { return _eName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for EName", value, value.ToString());
				_eName = value;
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
        [DataDesc(CName = "快捷码", ShortCode = "Shortcode", Desc = "快捷码", ContextType = SysDic.All, Length = 20)]
        public virtual string Shortcode
		{
			get { return _shortcode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Shortcode", value, value.ToString());
				_shortcode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "说明", ShortCode = "Comment", Desc = "说明", ContextType = SysDic.All, Length = 16)]
        public virtual string Comment
		{
			get { return _comment; }
			set
			{
				if ( value != null && value.Length > 8000)
					throw new ArgumentOutOfRangeException("Invalid value for Comment", value, value.ToString());
				_comment = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
		}

        [DataMember]
        [DataDesc(CName = "送检目的地", ShortCode = "Destination", Desc = "送检目的地", ContextType = SysDic.All, Length = 50)]
        public virtual string Destination
		{
			get { return _destination; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Destination", value, value.ToString());
				_destination = value;
			}
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
        [DataDesc(CName = "采样组打印", ShortCode = "MEPTSamplingGroupPrint", Desc = "采样组打印", ContextType = SysDic.All)]
		public virtual MEPTSamplingGroupPrint MEPTSamplingGroupPrint
		{
			get { return _mEPTSamplingGroupPrint; }
			set { _mEPTSamplingGroupPrint = value; }
		}

        [DataMember]
        [DataDesc(CName = "专业表", ShortCode = "BSpecialty", Desc = "专业表", ContextType = SysDic.All)]
        public virtual BSpecialty BSpecialty
		{
			get { return _bSpecialty; }
			set { _bSpecialty = value; }
		}

        [DataMember]
        [DataDesc(CName = "采样管设置", ShortCode = "MEPTSamplingTube", Desc = "采样管设置", ContextType = SysDic.All)]
		public virtual MEPTSamplingTube MEPTSamplingTube
		{
			get { return _mEPTSamplingTube; }
			set { _mEPTSamplingTube = value; }
		}

        [DataMember]
        [DataDesc(CName = "样本类型", ShortCode = "BSampleType", Desc = "样本类型", ContextType = SysDic.All)]
        public virtual BSampleType BSampleType
		{
			get { return _bSampleType; }
			set { _bSampleType = value; }
		}

        [DataMember]
        [DataDesc(CName = "按项目拆分", ShortCode = "MEPTItemSplitList", Desc = "按项目拆分", ContextType = SysDic.All)]
		public virtual IList<MEPTItemSplit> MEPTItemSplitList
		{
			get
			{
				if (_mEPTItemSplitList==null)
				{
					_mEPTItemSplitList = new List<MEPTItemSplit>();
				}
				return _mEPTItemSplitList;
			}
			set { _mEPTItemSplitList = value; }
		}

        [DataMember]
        [DataDesc(CName = "采样项目维护", ShortCode = "MEPTSamplingItemList", Desc = "采样项目维护", ContextType = SysDic.All)]
		public virtual IList<MEPTSamplingItem> MEPTSamplingItemList
		{
			get
			{
				if (_mEPTSamplingItemList==null)
				{
					_mEPTSamplingItemList = new List<MEPTSamplingItem>();
				}
				return _mEPTSamplingItemList;
			}
			set { _mEPTSamplingItemList = value; }
		}

		#endregion
	}
	#endregion
}