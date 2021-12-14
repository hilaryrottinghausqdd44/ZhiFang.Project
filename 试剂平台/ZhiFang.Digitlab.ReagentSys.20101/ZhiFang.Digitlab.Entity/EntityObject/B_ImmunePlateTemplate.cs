using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region BImmunePlateTemplate

	/// <summary>
	/// BImmunePlateTemplate object for NHibernate mapped table 'B_ImmunePlateTemplate'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "用于存储酶免板布局模板信息。为了方便用户操作，提高用户工作效率，系统提供“酶免板布局模板定制、读取”功能，使用户可以根据需要将某一类板布局存为模板，以便以后遇到相同的检验需求时，直接读取模板中的布局信息，减免了用户对此类信息设置的繁复操作。", ClassCName = "BImmunePlateTemplate", ShortCode = "BImmunePlateTemplate", Desc = "用于存储酶免板布局模板信息。为了方便用户操作，提高用户工作效率，系统提供“酶免板布局模板定制、读取”功能，使用户可以根据需要将某一类板布局存为模板，以便以后遇到相同的检验需求时，直接读取模板中的布局信息，减免了用户对此类信息设置的繁复操作。")]
	public class BImmunePlateTemplate : BaseEntity
	{
		#region Member Variables
		
        protected string _templateName;
        protected DateTime? _dataUpdateTime;
		protected EPBEquip _ePBEquip;
		protected IList<BImmunePlateTemplateDetailInfo> _bImmunePlateTemplateDetailInfoList; 

		#endregion

		#region Constructors

		public BImmunePlateTemplate() { }

		public BImmunePlateTemplate( long labID, string templateName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, EPBEquip ePBEquip )
		{
			this._labID = labID;
			this._templateName = templateName;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._ePBEquip = ePBEquip;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "模板名称", ShortCode = "TemplateName", Desc = "模板名称", ContextType = SysDic.All, Length = 40)]
        public virtual string TemplateName
		{
			get { return _templateName; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for TemplateName", value, value.ToString());
				_templateName = value;
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
        [DataDesc(CName = "仪器表", ShortCode = "EPBEquip", Desc = "仪器表")]
		public virtual EPBEquip EPBEquip
		{
			get { return _ePBEquip; }
			set { _ePBEquip = value; }
		}

        [DataMember]
        [DataDesc(CName = "用于存储酶免板布局模板的详细布局信息。记录每一个孔位的检验项目、样本类型等信息", ShortCode = "BImmunePlateTemplateDetailInfoList", Desc = "用于存储酶免板布局模板的详细布局信息。记录每一个孔位的检验项目、样本类型等信息")]
		public virtual IList<BImmunePlateTemplateDetailInfo> BImmunePlateTemplateDetailInfoList
		{
			get
			{
				if (_bImmunePlateTemplateDetailInfoList==null)
				{
					_bImmunePlateTemplateDetailInfoList = new List<BImmunePlateTemplateDetailInfo>();
				}
				return _bImmunePlateTemplateDetailInfoList;
			}
			set { _bImmunePlateTemplateDetailInfoList = value; }
		}

        
		#endregion
	}
	#endregion
}