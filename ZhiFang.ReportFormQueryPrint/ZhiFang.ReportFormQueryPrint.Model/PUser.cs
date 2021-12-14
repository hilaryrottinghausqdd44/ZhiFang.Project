using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZhiFang.ReportFormQueryPrint.Model
{
	/// <summary>
	/// 实体类PUser 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class PUser
	{
		public PUser()
		{}
		#region Model
		private long  _userno;
		private string _cname;
		private string _password;
		private string _shortcode;
		private long? _gender;
		private DateTime? _birthday;
		private string _role;
		private string _resume;
		private int? _visible;
		private int? _disporder;
		private string _hisordercode;
		private byte[] _userimage;
		private string _usertype;
		private int? _deptno;
		private long? _sectortypeno;
		private string _userimename;
		private int? _ismanager;
		private string _passwords;
		private DateTime? _tstamp;
        private string _filepath;
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public long UserNo
		{
			set{ _userno=value;}
			get{return _userno;}
		}
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string CName
		{
			set{ _cname=value;}
			get{return _cname;}
		}
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Password
		{
			set{ _password=value;}
			get{return _password;}
		}
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string ShortCode
		{
			set{ _shortcode=value;}
			get{return _shortcode;}
		}
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public long? Gender
		{
			set{ _gender=value;}
			get{return _gender;}
		}
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public DateTime? Birthday
		{
			set{ _birthday=value;}
			get{return _birthday;}
		}
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Role
		{
			set{ _role=value;}
			get{return _role;}
		}
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Resume
		{
			set{ _resume=value;}
			get{return _resume;}
		}
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int? Visible
		{
			set{ _visible=value;}
			get{return _visible;}
		}
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int? DispOrder
		{
			set{ _disporder=value;}
			get{return _disporder;}
		}
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string HisOrderCode
		{
			set{ _hisordercode=value;}
			get{return _hisordercode;}
		}
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public byte[] userimage
		{
			set{ _userimage=value;}
			get{return _userimage;}
		}
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string usertype
		{
			set{ _usertype=value;}
			get{return _usertype;}
		}
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int? DeptNo
		{
			set{ _deptno=value;}
			get{return _deptno;}
		}
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public long? SectorTypeNo
		{
			set{ _sectortypeno=value;}
			get{return _sectortypeno;}
		}
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string UserImeName
		{
			set{ _userimename=value;}
			get{return _userimename;}
		}
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int? IsManager
		{
			set{ _ismanager=value;}
			get{return _ismanager;}
		}
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string PassWordS
		{
			set{ _passwords=value;}
			get{return _passwords;}
		}
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public DateTime? tstamp
		{
			set{ _tstamp=value;}
			get{return _tstamp;}
		}
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string FilePath
        {
            set { _filepath = value; }
            get { return _filepath; }
        }
		#endregion Model

	}
}

