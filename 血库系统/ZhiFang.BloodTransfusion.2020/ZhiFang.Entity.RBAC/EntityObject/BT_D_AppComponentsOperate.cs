using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.RBAC
{
	#region BTDAppComponentsOperate

	/// <summary>
	/// BTDAppComponentsOperate object for NHibernate mapped table 'BT_D_AppComponentsOperate'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "程序操作包括按钮及非按钮操作（如下拉列表等）", ClassCName = "BTDAppComponentsOperate", ShortCode = "BTDAppComponentsOperate", Desc = "程序操作包括按钮及非按钮操作（如下拉列表等）")]
	public class BTDAppComponentsOperate : BaseEntityService
    {
		#region Member Variables
		
        protected string _appComOperateKeyWord;
        protected string _appComOperateName;
        protected string _RowFilterBase;
        protected string _creator;
        protected string _modifier;
        protected DateTime? _dataUpdateTime;
		protected BTDAppComponents _bTDAppComponents;
		protected IList<RBACModuleOper> _rBACModuleOperList; 

		#endregion

		#region Constructors

		public BTDAppComponentsOperate() { }

		public BTDAppComponentsOperate( long labID, string appComOperateKeyWord, string appComOperateName, string creator, string modifier, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, BTDAppComponents bTDAppComponents )
		{
			this._labID = labID;
			this._appComOperateKeyWord = appComOperateKeyWord;
			this._appComOperateName = appComOperateName;
			this._creator = creator;
			this._modifier = modifier;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._bTDAppComponents = bTDAppComponents;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "操作关键字", ShortCode = "AppComOperateKeyWord", Desc = "操作关键字", ContextType = SysDic.All, Length = 50)]
        public virtual string AppComOperateKeyWord
		{
			get { return _appComOperateKeyWord; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for AppComOperateKeyWord", value, value.ToString());
				_appComOperateKeyWord = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "操作名称", ShortCode = "AppComOperateName", Desc = "操作名称", ContextType = SysDic.All, Length = 50)]
        public virtual string AppComOperateName
		{
			get { return _appComOperateName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for AppComOperateName", value, value.ToString());
				_appComOperateName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "创建者", ShortCode = "Creator", Desc = "创建者", ContextType = SysDic.All, Length = 50)]
        public virtual string Creator
		{
			get { return _creator; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Creator", value, value.ToString());
				_creator = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "修改者", ShortCode = "Modifier", Desc = "修改者", ContextType = SysDic.All, Length = 50)]
        public virtual string Modifier
		{
			get { return _modifier; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Modifier", value, value.ToString());
				_modifier = value;
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
        [DataDesc(CName = "应用组件", ShortCode = "BTDAppComponents", Desc = "应用组件")]
		public virtual BTDAppComponents BTDAppComponents
		{
			get { return _bTDAppComponents; }
			set { _bTDAppComponents = value; }
		}

        [DataMember]
        [DataDesc(CName = "模块操作", ShortCode = "RBACModuleOperList", Desc = "模块操作")]
		public virtual IList<RBACModuleOper> RBACModuleOperList
		{
			get
			{
				if (_rBACModuleOperList==null)
				{
					_rBACModuleOperList = new List<RBACModuleOper>();
				}
				return _rBACModuleOperList;
			}
			set { _rBACModuleOperList = value; }
		}

        [DataMember]
        [DataDesc(CName = "行过滤依据对象", ShortCode = "RowFilterBase", Desc = "行过滤依据对象", ContextType = SysDic.All, Length = 50)]
        public virtual string RowFilterBase
        {
            get { return _RowFilterBase; }
            set
            {
                _RowFilterBase = value;
            }
        }
		#endregion
	}
	#endregion
}