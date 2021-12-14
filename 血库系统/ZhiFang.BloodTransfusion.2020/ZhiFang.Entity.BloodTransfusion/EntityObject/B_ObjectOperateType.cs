using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
	#region BSampleOperateType

	/// <summary>
	/// BSampleOperateType object for NHibernate mapped table 'B_SampleOperateType'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "样本操作类型", ClassCName = "BSampleOperateType", ShortCode = "BSampleOperateType", Desc = "样本操作类型")]
	public class BObjectOperateType : BaseEntity
	{
		#region Member Variables
		
        protected int _operateLevel;
        protected string _name;
        protected string _sName;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected string _comment;
        protected bool _isUse;
		protected IList<BObjectOperate> _operateTypeBSampleOperateList;

		#endregion

		#region Constructors

		public BObjectOperateType() { }

		public BObjectOperateType( long labID, int operateLevel, string name, string sName, string shortcode, string pinYinZiTou, string comment, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._operateLevel = operateLevel;
			this._name = name;
			this._sName = sName;
			this._shortcode = shortcode;
			this._pinYinZiTou = pinYinZiTou;
			this._comment = comment;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "操作级别", ShortCode = "OperateLevel", Desc = "操作级别", ContextType = SysDic.All, Length = 4)]
        public virtual int OperateLevel
		{
			get { return _operateLevel; }
			set { _operateLevel = value; }
		}

        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "Name", Desc = "名称", ContextType = SysDic.All, Length = 40)]
        public virtual string Name
		{
			get { return _name; }
			set
			{	
				_name = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 40)]
        public virtual string SName
		{
			get { return _sName; }
			set
			{
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
				_shortcode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "汉字拼音字头", ShortCode = "PinYinZiTou", Desc = "汉字拼音字头", ContextType = SysDic.All, Length = 50)]
        public virtual string PinYinZiTou
		{
			get { return _pinYinZiTou; }
			set
			{
				_pinYinZiTou = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "说明", ShortCode = "Comment", Desc = "说明", ContextType = SysDic.All, Length = 16)]
        public virtual string Comment
		{
			get { return _comment; }
			set
			{
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
        [DataDesc(CName = "对象操作", ShortCode = "OperateTypeBObjectOperateList", Desc = "对象操作")]
		public virtual IList<BObjectOperate> OperateTypeBObjectOperateList
        {
			get
			{
				if (_operateTypeBSampleOperateList==null)
				{
					_operateTypeBSampleOperateList = new List<BObjectOperate>();
				}
				return _operateTypeBSampleOperateList;
			}
			set { _operateTypeBSampleOperateList = value; }
		}

        
		#endregion
	}
	#endregion
}