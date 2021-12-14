using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.RBAC
{
    #region PUser

    /// <summary>
    /// PUser object for NHibernate mapped table 'PUser'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "用户帐号信息表", ClassCName = "PUser", ShortCode = "PUser", Desc = "用户帐号信息表")]
    public class PUser : BaseEntity
    {
        #region Member Variables

        protected string _cName;
        protected string _shortCode;
        protected string _password;
        protected int _gender;
        protected DateTime? _birthday;
        protected long? _deptNo;
        protected long? _gradeNo;
        protected string _role;
        protected string _resume;
        protected byte[] _userimage;
        protected string _usertype;
        protected int _sectorTypeNo;
        protected string _userImeName;
        protected int _isManager;
        protected string _passWordS;
        protected string _cAUserName;
        protected string _cAContainerName;
        protected string _cAUserID;
        protected string _cAkeysn;
        protected string _email;
        protected DateTime? _loginDateTime;
        protected int _cAUserAuthorised;
        protected DateTime? _pWDateTime;
        protected string _userDataRights;
        protected string _imgsignature;
        protected string _otherCode;
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
        protected int _dispOrder;
        protected bool _visible;


        #endregion

        #region Constructors

        public PUser() { }

        public PUser(long labID, string cName, string shortCode, string password, int gender, DateTime birthday, long deptNo, long gradeNo, string role, string resume, byte[] userimage, string usertype, int sectorTypeNo, string userImeName, int isManager, string passWordS, string cAUserName, string cAContainerName, string cAUserID, string cAkeysn, string email, DateTime loginDateTime, int cAUserAuthorised, DateTime pWDateTime, string userDataRights, string imgsignature, string otherCode, string code1, string code2, string code3, string code4, string code5, string code6, string code7, string code8, string code9, string code10, int dispOrder, bool visible, DateTime dataAddTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._cName = cName;
            this._shortCode = shortCode;
            this._password = password;
            this._gender = gender;
            this._birthday = birthday;
            this._deptNo = deptNo;
            this._gradeNo = gradeNo;
            this._role = role;
            this._resume = resume;
            this._userimage = userimage;
            this._usertype = usertype;
            this._sectorTypeNo = sectorTypeNo;
            this._userImeName = userImeName;
            this._isManager = isManager;
            this._passWordS = passWordS;
            this._cAUserName = cAUserName;
            this._cAContainerName = cAContainerName;
            this._cAUserID = cAUserID;
            this._cAkeysn = cAkeysn;
            this._email = email;
            this._loginDateTime = loginDateTime;
            this._cAUserAuthorised = cAUserAuthorised;
            this._pWDateTime = pWDateTime;
            this._userDataRights = userDataRights;
            this._imgsignature = imgsignature;
            this._otherCode = otherCode;
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
            this._dispOrder = dispOrder;
            this._visible = visible;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "主键ID", ShortCode = "Id", Desc = "主键ID", ContextType = SysDic.Number)]
        public override long Id
        {
            get
            {
                if (_id <= -3 && _id != -99)
                    _id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                return _id;
            }
            set { _id = value; }
        }
        [DataMember]
        [DataDesc(CName = "中文名称", ShortCode = "CName", Desc = "中文名称", ContextType = SysDic.All, Length = 20)]
        public virtual string CName
        {
            get { return _cName; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
                _cName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "帐号", ShortCode = "ShortCode", Desc = "帐号", ContextType = SysDic.All, Length = 20)]
        public virtual string ShortCode
        {
            get { return _shortCode; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for ShortCode", value, value.ToString());
                _shortCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "密码", ShortCode = "Password", Desc = "密码", ContextType = SysDic.All, Length = 50)]
        public virtual string Password
        {
            get { return _password; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Password", value, value.ToString());
                _password = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "Gender", ShortCode = "Gender", Desc = "Gender", ContextType = SysDic.All, Length = 4)]
        public virtual int Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "出生日期", ShortCode = "Birthday", Desc = "出生日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? Birthday
        {
            get { return _birthday; }
            set { _birthday = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DeptNo", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? DeptNo
        {
            get { return _deptNo; }
            set { _deptNo = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医生等级", ShortCode = "GradeNo", Desc = "医生等级", ContextType = SysDic.All, Length = 8)]
        public virtual long? GradeNo
        {
            get { return _gradeNo; }
            set { _gradeNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "Role", ShortCode = "Role", Desc = "Role", ContextType = SysDic.All, Length = 20)]
        public virtual string Role
        {
            get { return _role; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Role", value, value.ToString());
                _role = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Resume", Desc = "", ContextType = SysDic.All, Length = 250)]
        public virtual string Resume
        {
            get { return _resume; }
            set
            {
                if (value != null && value.Length > 250)
                    throw new ArgumentOutOfRangeException("Invalid value for Resume", value, value.ToString());
                _resume = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Userimage", Desc = "", ContextType = SysDic.All, Length = 16)]
        public virtual byte[] Userimage
        {
            get { return _userimage; }
            set { _userimage = value; }
        }

        [DataMember]
        [DataDesc(CName = "身份类型", ShortCode = "Usertype", Desc = "身份类型", ContextType = SysDic.All, Length = 20)]
        public virtual string Usertype
        {
            get { return _usertype; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Usertype", value, value.ToString());
                _usertype = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "SectorTypeNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int SectorTypeNo
        {
            get { return _sectorTypeNo; }
            set { _sectorTypeNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UserImeName", Desc = "", ContextType = SysDic.All, Length = 80)]
        public virtual string UserImeName
        {
            get { return _userImeName; }
            set
            {
                if (value != null && value.Length > 80)
                    throw new ArgumentOutOfRangeException("Invalid value for UserImeName", value, value.ToString());
                _userImeName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsManager", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsManager
        {
            get { return _isManager; }
            set { _isManager = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PassWordS", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string PassWordS
        {
            get { return _passWordS; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for PassWordS", value, value.ToString());
                _passWordS = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CAUserName", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string CAUserName
        {
            get { return _cAUserName; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for CAUserName", value, value.ToString());
                _cAUserName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CAContainerName", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string CAContainerName
        {
            get { return _cAContainerName; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for CAContainerName", value, value.ToString());
                _cAContainerName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CAUserID", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string CAUserID
        {
            get { return _cAUserID; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for CAUserID", value, value.ToString());
                _cAUserID = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CAkeysn", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string CAkeysn
        {
            get { return _cAkeysn; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for CAkeysn", value, value.ToString());
                _cAkeysn = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Email", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Email
        {
            get { return _email; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Email", value, value.ToString());
                _email = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "LoginDateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? LoginDateTime
        {
            get { return _loginDateTime; }
            set { _loginDateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CAUserAuthorised", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int CAUserAuthorised
        {
            get { return _cAUserAuthorised; }
            set { _cAUserAuthorised = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "PWDateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? PWDateTime
        {
            get { return _pWDateTime; }
            set { _pWDateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "UserDataRights", ShortCode = "UserDataRights", Desc = "UserDataRights", ContextType = SysDic.All, Length = 2000)]
        public virtual string UserDataRights
        {
            get { return _userDataRights; }
            set
            {
                if (value != null && value.Length > 2000)
                    throw new ArgumentOutOfRangeException("Invalid value for UserDataRights", value, value.ToString());
                _userDataRights = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "Imgsignature", ShortCode = "Imgsignature", Desc = "Imgsignature", ContextType = SysDic.All, Length = 16)]
        public virtual string Imgsignature
        {
            get { return _imgsignature; }
            set
            {
                if (value != null && value.Length > 16)
                    throw new ArgumentOutOfRangeException("Invalid value for Imgsignature", value, value.ToString());
                _imgsignature = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "OtherCode", ShortCode = "OtherCode", Desc = "OtherCode", ContextType = SysDic.All, Length = 50)]
        public virtual string OtherCode
        {
            get { return _otherCode; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for OtherCode", value, value.ToString());
                _otherCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "code_1", ShortCode = "Code1", Desc = "code_1", ContextType = SysDic.All, Length = 50)]
        public virtual string Code1
        {
            get { return _code1; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Code1", value, value.ToString());
                _code1 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "code_2", ShortCode = "Code2", Desc = "code_2", ContextType = SysDic.All, Length = 50)]
        public virtual string Code2
        {
            get { return _code2; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Code2", value, value.ToString());
                _code2 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "code_3", ShortCode = "Code3", Desc = "code_3", ContextType = SysDic.All, Length = 50)]
        public virtual string Code3
        {
            get { return _code3; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Code3", value, value.ToString());
                _code3 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "code_4", ShortCode = "Code4", Desc = "code_4", ContextType = SysDic.All, Length = 50)]
        public virtual string Code4
        {
            get { return _code4; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Code4", value, value.ToString());
                _code4 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "code_5", ShortCode = "Code5", Desc = "code_5", ContextType = SysDic.All, Length = 50)]
        public virtual string Code5
        {
            get { return _code5; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Code5", value, value.ToString());
                _code5 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "code_6", ShortCode = "Code6", Desc = "code_6", ContextType = SysDic.All, Length = 50)]
        public virtual string Code6
        {
            get { return _code6; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Code6", value, value.ToString());
                _code6 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "code_7", ShortCode = "Code7", Desc = "code_7", ContextType = SysDic.All, Length = 50)]
        public virtual string Code7
        {
            get { return _code7; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Code7", value, value.ToString());
                _code7 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "code_8", ShortCode = "Code8", Desc = "code_8", ContextType = SysDic.All, Length = 50)]
        public virtual string Code8
        {
            get { return _code8; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Code8", value, value.ToString());
                _code8 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "code_9", ShortCode = "Code9", Desc = "code_9", ContextType = SysDic.All, Length = 50)]
        public virtual string Code9
        {
            get { return _code9; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Code9", value, value.ToString());
                _code9 = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "code_10", ShortCode = "Code10", Desc = "code_10", ContextType = SysDic.All, Length = 50)]
        public virtual string Code10
        {
            get { return _code10; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Code10", value, value.ToString());
                _code10 = value;
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
        [DataDesc(CName = "", ShortCode = "Visible", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }


        #endregion

        #region 自定义属性

        protected Department _ward;
        protected Department _department;

        [DataMember]
        [DataDesc(CName = "所属病区", ShortCode = "Ward", Desc = "所属病区")]
        public virtual Department Ward
        {
            get { return _ward; }
            set { _ward = value; }
        }
        [DataMember]
        [DataDesc(CName = "所属部门", ShortCode = "Department", Desc = "所属部门")]
        public virtual Department Department
        {
            get { return _department; }
            set { _department = value; }
        }

        #endregion
    }
    #endregion
}