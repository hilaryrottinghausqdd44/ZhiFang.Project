using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity.ReagentSys
{
	#region CenOrgType

	/// <summary>
	/// CenOrgType object for NHibernate mapped table 'CenOrgType'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "机构类型表", ClassCName = "CenOrgType", ShortCode = "CenOrgType", Desc = "")]
	public class CenOrgType : BaseEntity
	{
		#region Member Variables
		
        protected string _cName;
        protected string _eName;
        protected string _shortCode;
        protected string _memo;
        protected int _dispOrder;
        protected int _visible;

		#endregion

		#region Constructors

		public CenOrgType() { }

		public CenOrgType( string cName, string eName, string shortCode, string memo, int dispOrder, int visible )
		{
			this._cName = cName;
			this._eName = eName;
			this._shortCode = shortCode;
			this._memo = memo;
			this._dispOrder = dispOrder;
			this._visible = visible;
		}

		#endregion

		#region Public Properties


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
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = 4)]
        public virtual string Memo
		{
			get { return _memo; }
			set { _memo = value; }
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
        
		#endregion
	}
	#endregion
}