using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region BImmunePlateTemplateDetailInfo

	/// <summary>
	/// BImmunePlateTemplateDetailInfo object for NHibernate mapped table 'B_ImmunePlateTemplateDetailInfo'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "用于存储酶免板布局模板的详细布局信息。记录每一个孔位的检验项目、样本类型等信息", ClassCName = "BImmunePlateTemplateDetailInfo", ShortCode = "BImmunePlateTemplateDetailInfo", Desc = "用于存储酶免板布局模板的详细布局信息。记录每一个孔位的检验项目、样本类型等信息")]
	public class BImmunePlateTemplateDetailInfo : BaseEntity
	{
		#region Member Variables
		
        protected int _positionNo;
        protected string _dispPositionNo;
        protected int _sampleType;
        protected string _gSampleNo;
        protected int _bZPSampleOrder;
        protected int _sameBZPSampleOrder;
        protected DateTime? _dataUpdateTime;
		protected BImmunePlateTemplate _bImmunePlateTemplate;
		protected ItemAllItem _itemAllItem;

		#endregion

		#region Constructors

		public BImmunePlateTemplateDetailInfo() { }

		public BImmunePlateTemplateDetailInfo( long labID, int positionNo, string dispPositionNo, int sampleType, string gSampleNo, int bZPSampleOrder, int sameBZPSampleOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BImmunePlateTemplate bImmunePlateTemplate, ItemAllItem itemAllItem )
		{
			this._labID = labID;
			this._positionNo = positionNo;
			this._dispPositionNo = dispPositionNo;
			this._sampleType = sampleType;
			this._gSampleNo = gSampleNo;
			this._bZPSampleOrder = bZPSampleOrder;
			this._sameBZPSampleOrder = sameBZPSampleOrder;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bImmunePlateTemplate = bImmunePlateTemplate;
			this._itemAllItem = itemAllItem;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "酶免板孔位ID，其值为1-96", ShortCode = "PositionNo", Desc = "酶免板孔位ID，其值为1-96", ContextType = SysDic.All, Length = 4)]
        public virtual int PositionNo
		{
			get { return _positionNo; }
			set { _positionNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "酶免板孔位显示名称", ShortCode = "DispPositionNo", Desc = "酶免板孔位显示名称", ContextType = SysDic.All, Length = 10)]
        public virtual string DispPositionNo
		{
			get { return _dispPositionNo; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for DispPositionNo", value, value.ToString());
				_dispPositionNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "每个孔所检标本的类型：", ShortCode = "SampleType", Desc = "每个孔所检标本的类型：", ContextType = SysDic.All, Length = 4)]
        public virtual int SampleType
		{
			get { return _sampleType; }
			set { _sampleType = value; }
		}

        [DataMember]
        [DataDesc(CName = "检测标本的样本号：", ShortCode = "GSampleNo", Desc = "检测标本的样本号：", ContextType = SysDic.All, Length = 20)]
        public virtual string GSampleNo
		{
			get { return _gSampleNo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for GSampleNo", value, value.ToString());
				_gSampleNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "标准品标本次序", ShortCode = "BZPSampleOrder", Desc = "标准品标本次序", ContextType = SysDic.All, Length = 4)]
        public virtual int BZPSampleOrder
		{
			get { return _bZPSampleOrder; }
			set { _bZPSampleOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "同型标准品标本次序", ShortCode = "SameBZPSampleOrder", Desc = "同型标准品标本次序", ContextType = SysDic.All, Length = 4)]
        public virtual int SameBZPSampleOrder
		{
			get { return _sameBZPSampleOrder; }
			set { _sameBZPSampleOrder = value; }
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
        [DataDesc(CName = "用于存储酶免板布局模板信息。为了方便用户操作，提高用户工作效率，系统提供“酶免板布局模板定制、读取”功能，使用户可以根据需要将某一类板布局存为模板，以便以后遇到相同的检验需求时，直接读取模板中的布局信息，减免了用户对此类信息设置的繁复操作。", ShortCode = "BImmunePlateTemplate", Desc = "用于存储酶免板布局模板信息。为了方便用户操作，提高用户工作效率，系统提供“酶免板布局模板定制、读取”功能，使用户可以根据需要将某一类板布局存为模板，以便以后遇到相同的检验需求时，直接读取模板中的布局信息，减免了用户对此类信息设置的繁复操作。")]
		public virtual BImmunePlateTemplate BImmunePlateTemplate
		{
			get { return _bImmunePlateTemplate; }
			set { _bImmunePlateTemplate = value; }
		}

        [DataMember]
        [DataDesc(CName = "所有项目", ShortCode = "ItemAllItem", Desc = "所有项目")]
		public virtual ItemAllItem ItemAllItem
		{
			get { return _itemAllItem; }
			set { _itemAllItem = value; }
		}

        
		#endregion
	}
	#endregion
}