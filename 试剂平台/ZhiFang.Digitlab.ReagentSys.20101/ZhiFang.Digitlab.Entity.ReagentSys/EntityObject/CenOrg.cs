using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity.ReagentSys
{
	#region CenOrg

	/// <summary>
	/// CenOrg object for NHibernate mapped table 'CenOrg'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "机构信息表", ClassCName = "CenOrg", ShortCode = "CenOrg", Desc = "")]
	public class CenOrg : BaseEntity
	{
		#region Member Variables
		
        protected int _orgNo;
        protected string _cName;
        protected string _eName;
        protected string _serverIP;
        protected string _serverPort;
        protected string _shortCode;
        protected string _address;
        protected string _contact;
        protected string _tel;
        protected string _fox;
        protected string _email;
        protected string _webAddress;
        protected string _memo;
        protected int _dispOrder;
        protected int _visible;
        protected string _zX1;
        protected string _zX2;
        protected string _zX3;
        protected string _tel1;
        protected string _hotTel;
        protected string _hotTel1;
        protected DateTime? _dataUpdateTime;
		protected CenOrgType _cenOrgType;

		#endregion

		#region Constructors

		public CenOrg() { }

		public CenOrg( int orgNo, string cName, string eName, string serverIP, string serverPort, string shortCode, string address, string contact, string tel, string fox, string email, string webAddress, string memo, int dispOrder, int visible, string zX1, string zX2, string zX3, CenOrgType cenOrgType )
		{
			this._orgNo = orgNo;
			this._cName = cName;
			this._eName = eName;
			this._serverIP = serverIP;
			this._serverPort = serverPort;
			this._shortCode = shortCode;
			this._address = address;
			this._contact = contact;
			this._tel = tel;
			this._fox = fox;
			this._email = email;
			this._webAddress = webAddress;
			this._memo = memo;
			this._dispOrder = dispOrder;
			this._visible = visible;
			this._zX1 = zX1;
			this._zX2 = zX2;
			this._zX3 = zX3;
			this._cenOrgType = cenOrgType;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "机构编号", ShortCode = "OrgNo", Desc = "机构编号", ContextType = SysDic.All, Length = 4)]
        public virtual int OrgNo
		{
			get { return _orgNo; }
			set
			{
				_orgNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "中文名", ShortCode = "CName", Desc = "中文名", ContextType = SysDic.All, Length = 100)]
        public virtual string CName
		{
			get { return _cName; }
			set
			{
				_cName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "英文名", ShortCode = "EName", Desc = "英文名", ContextType = SysDic.All, Length = 100)]
        public virtual string EName
		{
			get { return _eName; }
			set
			{
				_eName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "服务器IP", ShortCode = "ServerIP", Desc = "服务器IP", ContextType = SysDic.All, Length = 100)]
        public virtual string ServerIP
		{
			get { return _serverIP; }
			set
			{
				_serverIP = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "服务器端口", ShortCode = "ServerPort", Desc = "服务器端口", ContextType = SysDic.All, Length = 10)]
        public virtual string ServerPort
		{
			get { return _serverPort; }
			set
			{
				_serverPort = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "代码", ShortCode = "ShortCode", Desc = "代码", ContextType = SysDic.All, Length = 50)]
        public virtual string ShortCode
		{
			get { return _shortCode; }
			set
			{
				_shortCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "机构地址", ShortCode = "Address", Desc = "机构地址", ContextType = SysDic.All, Length = 100)]
        public virtual string Address
		{
			get { return _address; }
			set
			{
				_address = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "联系人", ShortCode = "Contact", Desc = "联系人", ContextType = SysDic.All, Length = 100)]
        public virtual string Contact
		{
			get { return _contact; }
			set
			{
				_contact = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "电话", ShortCode = "Tel", Desc = "电话", ContextType = SysDic.All, Length = 50)]
        public virtual string Tel
		{
			get { return _tel; }
			set
			{
				_tel = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "传真", ShortCode = "Fox", Desc = "传真", ContextType = SysDic.All, Length = 50)]
        public virtual string Fox
		{
			get { return _fox; }
			set
			{
				_fox = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "邮箱", ShortCode = "Email", Desc = "邮箱", ContextType = SysDic.All, Length = 50)]
        public virtual string Email
		{
			get { return _email; }
			set
			{
				_email = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "网址", ShortCode = "WebAddress", Desc = "网址", ContextType = SysDic.All, Length = 100)]
        public virtual string WebAddress
		{
			get { return _webAddress; }
			set
			{
				_webAddress = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = 100)]
        public virtual string Memo
		{
			get { return _memo; }
			set
			{
				_memo = value;
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
        [DataDesc(CName = "是否使用", ShortCode = "Visible", Desc = "是否使用", ContextType = SysDic.All, Length = 4)]
        public virtual int Visible
		{
			get { return _visible; }
			set { _visible = value; }
		}

        [DataMember]
        [DataDesc(CName = "专项1", ShortCode = "ZX1", Desc = "专项1", ContextType = SysDic.All, Length = 100)]
        public virtual string ZX1
		{
			get { return _zX1; }
			set
			{
				_zX1 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "专项2", ShortCode = "ZX2", Desc = "专项2", ContextType = SysDic.All, Length = 100)]
        public virtual string ZX2
		{
			get { return _zX2; }
			set
			{
				_zX2 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "专项3", ShortCode = "ZX3", Desc = "专项3", ContextType = SysDic.All, Length = 100)]
        public virtual string ZX3
		{
			get { return _zX3; }
			set
			{
				_zX3 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "电话1", ShortCode = "Tel1", Desc = "电话1", ContextType = SysDic.All, Length = 500)]
        public virtual string Tel1
        {
            get { return _tel1; }
            set { _tel1 = value; }
        }

        [DataMember]
        [DataDesc(CName = "热线电话", ShortCode = "HotTel", Desc = "热线电话", ContextType = SysDic.All, Length = 500)]
        public virtual string HotTel
        {
            get { return _hotTel; }
            set { _hotTel = value; }
        }

        [DataMember]
        [DataDesc(CName = "热线电话1", ShortCode = "HotTel1", Desc = "热线电话1", ContextType = SysDic.All, Length = 500)]
        public virtual string HotTel1
        {
            get { return _hotTel1; }
            set { _hotTel1 = value; }
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
        [DataDesc(CName = "", ShortCode = "CenOrgType", Desc = "")]
		public virtual CenOrgType CenOrgType
		{
			get { return _cenOrgType; }
			set { _cenOrgType = value; }
		}        
        
		#endregion
	}
	#endregion
}