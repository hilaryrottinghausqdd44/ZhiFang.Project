using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity.ReagentSys
{
	#region CenOrgCondition

	/// <summary>
	/// CenOrgCondition object for NHibernate mapped table 'CenOrgCondition'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "机构关系表", ClassCName = "CenOrgCondition", ShortCode = "CenOrgCondition", Desc = "")]
	public class CenOrgCondition : BaseEntity
	{
		#region Member Variables
		
        protected string _memo;
		protected CenOrg _1;
		protected CenOrg _2;
        private string _ConfirmToken;
        private string _ConfirmUserNo;
        private string _ConfirmUserName;
        private string _ConfirmUserPassWord;
        private string _ConfirmUrl;
        private string _CustomerCode;
        private string _CustomerAccount;
        protected string _orgAlias1;
        protected string _orgAlias2;

		#endregion

		#region Constructors

		public CenOrgCondition() { }

		public CenOrgCondition( string memo, CenOrg cenorg1, CenOrg cenorg2 )
		{
			this._memo = memo;
			this._1 = cenorg1;
			this._2 = cenorg2;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "说明", ShortCode = "Memo", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string Memo
		{
			get { return _memo; }
			set
			{
				_memo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "机构1", ShortCode = "CenOrg1", Desc = "机构1-上级机构")]
		public virtual CenOrg cenorg1
		{
			get { return _1; }
			set { _1 = value; }
		}

        [DataMember]
        [DataDesc(CName = "机构2", ShortCode = "CenOrg2", Desc = "机构2-下级机构")]
        public virtual CenOrg cenorg2
		{
			get { return _2; }
			set { _2 = value; }
		}

        [DataMember]
        [DataDesc(CName = "机构1别名", ShortCode = "OrgAlias1", Desc = "机构1别名", ContextType = SysDic.All, Length = 100)]
        public virtual string OrgAlias1
        {
            get { return _orgAlias1; }
            set
            {
                _orgAlias1 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "机构2别名", ShortCode = "OrgAlias2", Desc = "机构2别名", ContextType = SysDic.All, Length = 100)]
        public virtual string OrgAlias2
        {
            get { return _orgAlias2; }
            set
            {
                _orgAlias2 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "上传确认key", ShortCode = "ConfirmToken", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ConfirmToken 
        {
            get { return _ConfirmToken; }
            set
            {
                _ConfirmToken = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "上传确认用户编码", ShortCode = "ConfirmUserNo", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ConfirmUserNo 
        {
            get { return _ConfirmUserNo; }
            set
            {
                _ConfirmUserNo = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "上传确认用户名称", ShortCode = "ConfirmUserName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ConfirmUserName  
        {
            get { return _ConfirmUserName; }
            set
            {
                _ConfirmUserName = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "上传确认用户密码", ShortCode = "ConfirmUserPassWord", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ConfirmUserPassWord  
        {
            get { return _ConfirmUserPassWord; }
            set
            {
                _ConfirmUserPassWord = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "上传的URL", ShortCode = "ConfirmUrl", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ConfirmUrl  
        {
            get { return _ConfirmUrl; }
            set
            {
                _ConfirmUrl = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "实验室在供应商系统中的编码", ShortCode = "CustomerCode", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string CustomerCode  
        {
            get { return _CustomerCode; }
            set
            {
                _CustomerCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "实验室在供应商系统中的账户", ShortCode = "CustomerAccount", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string CustomerAccount  
        {
            get { return _CustomerAccount; }
            set
            {
                _CustomerAccount = value;
            }
        }        
		#endregion
	}
	#endregion
}
