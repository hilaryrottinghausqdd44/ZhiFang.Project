using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Model
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
		private int _userno;
		private string _cname;
		private string _password;
		private string _shortcode;
		private int? _gender;
		private DateTime? _birthday;
		private string _role;
		private string _resume;
		private int? _visible;
		private int? _disporder;
		private string _hisordercode;
		private byte[] _userimage;
		private string _usertype;
		private int? _deptno;
		private int? _sectortypeno;
		private string _userimename;
		private int? _ismanager;
		private string _passwords;
		private DateTime? _tstamp;

        private int _userid;
        private DateTime? _dtimestampe;
        private DateTime? _addtime;
        private string _standcode;
        private string _zfstandcode;
        private int? _useflag;

        /// <summary>
        /// UserID
        /// </summary>
        public int UserID
        {
            get { return _userid; }
            set { _userid = value; }
        }

        /// <summary>
        /// UserNo
        /// </summary>
        public int UserNo
        {
            get { return _userno; }
            set { _userno = value; }
        }

        /// <summary>
        /// CName
        /// </summary>
        public string CName
        {
            get { return _cname; }
            set { _cname = value; }
        }

        /// <summary>
        /// Password
        /// </summary>
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        /// <summary>
        /// ShortCode
        /// </summary>
        public string ShortCode
        {
            get { return _shortcode; }
            set { _shortcode = value; }
        }

        /// <summary>
        /// Gender
        /// </summary>
        public int? Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        /// <summary>
        /// Birthday
        /// </summary>
        public DateTime? Birthday
        {
            get { return _birthday; }
            set { _birthday = value; }
        }

        /// <summary>
        /// Role
        /// </summary>
        public string Role
        {
            get { return _role; }
            set { _role = value; }
        }

        /// <summary>
        /// Resume
        /// </summary>
        public string Resume
        {
            get { return _resume; }
            set { _resume = value; }
        }

        /// <summary>
        /// Visible
        /// </summary>
        public int? Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        /// <summary>
        /// DispOrder
        /// </summary>
        public int? DispOrder
        {
            get { return _disporder; }
            set { _disporder = value; }
        }

        /// <summary>
        /// HisOrderCode
        /// </summary>
        public string HisOrderCode
        {
            get { return _hisordercode; }
            set { _hisordercode = value; }
        }

        /// <summary>
        /// userimage
        /// </summary>
        public byte[] userimage
        {
            get { return _userimage; }
            set { _userimage = value; }
        }

        /// <summary>
        /// usertype
        /// </summary>
        public string usertype
        {
            get { return _usertype; }
            set { _usertype = value; }
        }

        /// <summary>
        /// DeptNo
        /// </summary>
        public int? DeptNo
        {
            get { return _deptno; }
            set { _deptno = value; }
        }

        /// <summary>
        /// SectorTypeNo
        /// </summary>
        public int? SectorTypeNo
        {
            get { return _sectortypeno; }
            set { _sectortypeno = value; }
        }

        /// <summary>
        /// UserImeName
        /// </summary>
        public string UserImeName
        {
            get { return _userimename; }
            set { _userimename = value; }
        }

        /// <summary>
        /// IsManager
        /// </summary>
        public int? IsManager
        {
            get { return _ismanager; }
            set { _ismanager = value; }
        }

        /// <summary>
        /// PassWordS
        /// </summary>
        public string PassWordS
        {
            get { return _passwords; }
            set { _passwords = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? tstamp
        {
            set { _tstamp = value; }
            get { return _tstamp; }
        }

        /// <summary>
        /// DTimeStampe
        /// </summary>
        public DateTime? DTimeStampe
        {
            get { return _dtimestampe; }
            set { _dtimestampe = value; }
        }

        /// <summary>
        /// AddTime
        /// </summary>
        public DateTime? AddTime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }

        /// <summary>
        /// StandCode
        /// </summary>
        public string StandCode
        {
            get { return _standcode; }
            set { _standcode = value; }
        }

        /// <summary>
        /// ZFStandCode
        /// </summary>
        public string ZFStandCode
        {
            get { return _zfstandcode; }
            set { _zfstandcode = value; }
        }

        /// <summary>
        /// UseFlag
        /// </summary>
        public int? UseFlag
        {
            get { return _useflag; }
            set { _useflag = value; }
        }

        /// <summary>
        /// 中心--实验室  对照关系使用
        /// </summary>
        private string _labcode;
        public string LabCode
        {
            get { return _labcode; }
            set { _labcode = value; }
        }
		#endregion Model

	}
}

