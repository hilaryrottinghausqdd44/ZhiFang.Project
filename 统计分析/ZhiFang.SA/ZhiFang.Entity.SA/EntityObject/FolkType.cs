using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.SA
{
	#region FolkType

	/// <summary>
	/// FolkType object for NHibernate mapped table 'FolkType'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "FolkType", ShortCode = "FolkType", Desc = "")]
	public class FolkType : BaseEntity
	{
		#region Member Variables
		
        protected string _cName;
        protected string _shortCode;
        protected int _visible;
        protected int _dispOrder;
        protected string _hisOrderCode;
        protected string _code1;
        protected string _code2;
        protected string _code3;
        protected string _code4;
        protected string _code5;
        protected string _code6;
        protected string _code7;
        protected string _code8;
        protected string _code9;
        protected string _code10;
		

		#endregion

		#region Constructors

		public FolkType() { }

		public FolkType( string cName, string shortCode, int visible, int dispOrder, string hisOrderCode, string code1, string code2, string code3, string code4, string code5, string code6, string code7, string code8, string code9, string code10 )
		{
			this._cName = cName;
			this._shortCode = shortCode;
			this._visible = visible;
			this._dispOrder = dispOrder;
			this._hisOrderCode = hisOrderCode;
			this._code1 = code1;
			this._code2 = code2;
			this._code3 = code3;
			this._code4 = code4;
			this._code5 = code5;
			this._code6 = code6;
			this._code7 = code7;
			this._code8 = code8;
			this._code9 = code9;
			this._code10 = code10;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "CName", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string CName
		{
			get { return _cName; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
				_cName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ShortCode", Desc = "", ContextType = SysDic.All, Length = 10)]
        public virtual string ShortCode
		{
			get { return _shortCode; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for ShortCode", value, value.ToString());
				_shortCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Visible", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Visible
		{
			get { return _visible; }
			set { _visible = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "HisOrderCode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string HisOrderCode
		{
			get { return _hisOrderCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for HisOrderCode", value, value.ToString());
				_hisOrderCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "????????????", ShortCode = "Code1", Desc = "????????????", ContextType = SysDic.All, Length = 50)]
        public virtual string Code1
		{
			get { return _code1; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Code1", value, value.ToString());
				_code1 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "????????????", ShortCode = "Code2", Desc = "????????????", ContextType = SysDic.All, Length = 50)]
        public virtual string Code2
		{
			get { return _code2; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Code2", value, value.ToString());
				_code2 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "????????????", ShortCode = "Code3", Desc = "????????????", ContextType = SysDic.All, Length = 50)]
        public virtual string Code3
		{
			get { return _code3; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Code3", value, value.ToString());
				_code3 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "???????????????", ShortCode = "Code4", Desc = "???????????????", ContextType = SysDic.All, Length = 50)]
        public virtual string Code4
		{
			get { return _code4; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Code4", value, value.ToString());
				_code4 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "???????????????", ShortCode = "Code5", Desc = "???????????????", ContextType = SysDic.All, Length = 50)]
        public virtual string Code5
		{
			get { return _code5; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Code5", value, value.ToString());
				_code5 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Code6", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Code6
		{
			get { return _code6; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Code6", value, value.ToString());
				_code6 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Code7", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Code7
		{
			get { return _code7; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Code7", value, value.ToString());
				_code7 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Code8", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Code8
		{
			get { return _code8; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Code8", value, value.ToString());
				_code8 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Code9", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Code9
		{
			get { return _code9; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Code9", value, value.ToString());
				_code9 = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Code10", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Code10
		{
			get { return _code10; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Code10", value, value.ToString());
				_code10 = value;
			}
		}

		
		#endregion
	}
	#endregion
}