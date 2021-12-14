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
    [DataDesc(CName = "", ClassCName = "PUser", ShortCode = "PUser", Desc = "")]
    public class PUser : BaseEntityServiceByInt
    {
        #region Member Variables

        protected string _cName;
        protected string _password;
        protected string _shortCode;
        protected int? _gender;
        protected DateTime? _birthday;
        protected string _role;
        protected string _resume;
        protected int _visible;
        protected int _dispOrder;
        protected string _hisOrder;
        protected string _hisOrderCode;
        protected byte[] _userimage;
        protected string _usertype;
        protected int? _deptNo;
        protected int? _sectorTypeNo;
        protected string _userImeName;
        protected int? _isManager;
        protected string _passWordS;
        protected string _cAUserName;
        protected string _cAContainerName;
        protected string _cAUserID;
        protected string _cAkeysn;
        protected string _code1;
        protected string _code2;
        protected string _code3;
        protected string _code4;
        protected string _code5;
        protected DateTime? _pWDateTime;
        protected DateTime? _loginDateTime;
        protected int? _cAUserAuthorised;
        protected string _userDataRights;
        protected string _imgsignature;
        protected string _email;
        protected string _otherCode;
        protected string _code6;
        protected string _code7;
        protected string _code8;
        protected string _code9;
        protected string _code10;

        protected Doctor _doctor;
        #endregion

        #region Constructors

        public PUser() { }

        public PUser(string cName, string password, string shortCode, int gender, DateTime birthday, string role, string resume, int visible, int dispOrder, string hisOrder, string hisOrderCode, byte[] userimage, string usertype, int deptNo, int sectorTypeNo, string userImeName, int isManager, string passWordS, string cAUserName, string cAContainerName, string cAUserID, string cAkeysn, string code1, string code2, string code3, string code4, string code5, DateTime pWDateTime, DateTime loginDateTime, int cAUserAuthorised, string userDataRights, string imgsignature, string email, string otherCode, string code6, string code7, string code8, string code9, string code10, byte[] dataTimeStamp)
        {
            this._cName = cName;
            this._password = password;
            this._shortCode = shortCode;
            this._gender = gender;
            this._birthday = birthday;
            this._role = role;
            this._resume = resume;
            this._visible = visible;
            this._dispOrder = dispOrder;
            this._hisOrder = hisOrder;
            this._hisOrderCode = hisOrderCode;
            this._userimage = userimage;
            this._usertype = usertype;
            this._deptNo = deptNo;
            this._sectorTypeNo = sectorTypeNo;
            this._userImeName = userImeName;
            this._isManager = isManager;
            this._passWordS = passWordS;
            this._cAUserName = cAUserName;
            this._cAContainerName = cAContainerName;
            this._cAUserID = cAUserID;
            this._cAkeysn = cAkeysn;
            this._code1 = code1;
            this._code2 = code2;
            this._code3 = code3;
            this._code4 = code4;
            this._code5 = code5;
            this._pWDateTime = pWDateTime;
            this._loginDateTime = loginDateTime;
            this._cAUserAuthorised = cAUserAuthorised;
            this._userDataRights = userDataRights;
            this._imgsignature = imgsignature;
            this._email = email;
            this._otherCode = otherCode;
            this._code6 = code6;
            this._code7 = code7;
            this._code8 = code8;
            this._code9 = code9;
            this._code10 = code10;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "主键ID", ShortCode = "Id", Desc = "主键ID", ContextType = SysDic.Number)]
        public override int Id
        {
            get
            {
                if (_id <= 0 && _id != -99 && _id != -1 && _id != -2)
                    _id = ZhiFang.Common.Public.GUIDHelp.GetGUIDInt();
                return _id;
            }
            set { _id = value; }
        }

        [DataMember]
        [DataDesc(CName = "所属医生信息", ShortCode = "Doctor", Desc = "所属医生信息")]
        public virtual Doctor Doctor
        {
            get { return _doctor; }
            set { _doctor = value; }
        }

        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "CName", Desc = "名称", ContextType = SysDic.All, Length = 50)]
        public virtual string CName
        {
            get { return _cName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
                _cName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "Password", ShortCode = "Password", Desc = "Password", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "ShortCode", ShortCode = "ShortCode", Desc = "ShortCode", ContextType = SysDic.All, Length = 20)]
        public virtual string ShortCode
        {
            get { return _shortCode; }
            set
            {
                _shortCode = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "Gender", ShortCode = "Gender", Desc = "Gender", ContextType = SysDic.All, Length = 4)]
        public virtual int? Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "Birthday", ShortCode = "Birthday", Desc = "Birthday", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? Birthday
        {
            get { return _birthday; }
            set { _birthday = value; }
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
        [DataDesc(CName = "Resume", ShortCode = "Resume", Desc = "Resume", ContextType = SysDic.All, Length = 250)]
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
        [DataDesc(CName = "Visible", ShortCode = "Visible", Desc = "Visible", ContextType = SysDic.All, Length = 4)]
        public virtual int Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        [DataMember]
        [DataDesc(CName = "DispOrder", ShortCode = "DispOrder", Desc = "DispOrder", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "HisOrder", ShortCode = "HisOrder", Desc = "HisOrder", ContextType = SysDic.All, Length = 4)]
        public virtual string HisOrder
        {
            get { return _hisOrder; }
            set
            {
                if (value != null && value.Length > 4)
                    throw new ArgumentOutOfRangeException("Invalid value for HisOrder", value, value.ToString());
                _hisOrder = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "HisOrderCode", ShortCode = "HisOrderCode", Desc = "HisOrderCode", ContextType = SysDic.All, Length = 50)]
        public virtual string HisOrderCode
        {
            get { return _hisOrderCode; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for HisOrderCode", value, value.ToString());
                _hisOrderCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "Userimage", ShortCode = "Userimage", Desc = "Userimage", ContextType = SysDic.All, Length = 16)]
        public virtual byte[] Userimage
        {
            get { return _userimage; }
            set { _userimage = value; }
        }

        [DataMember]
        [DataDesc(CName = "Usertype", ShortCode = "Usertype", Desc = "Usertype", ContextType = SysDic.All, Length = 20)]
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "DeptNo", ShortCode = "DeptNo", Desc = "DeptNo", ContextType = SysDic.All, Length = 4)]
        public virtual int? DeptNo
        {
            get { return _deptNo; }
            set { _deptNo = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "SectorTypeNo", ShortCode = "SectorTypeNo", Desc = "SectorTypeNo", ContextType = SysDic.All, Length = 4)]
        public virtual int? SectorTypeNo
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "IsManager", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int? IsManager
        {
            get { return _isManager; }
            set { _isManager = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PassWordS", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string PassWordS
        {
            get { return _passWordS; }
            set
            {
                if (value != null && value.Length > 20)
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
        [DataDesc(CName = "Code1", ShortCode = "Code1", Desc = "Code1", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "Code2", ShortCode = "Code2", Desc = "Code2", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "Code3", ShortCode = "Code3", Desc = "Code3", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "Code4", ShortCode = "Code4", Desc = "Code4", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "Code5", ShortCode = "Code5", Desc = "Code5", ContextType = SysDic.All, Length = 50)]
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "PWDateTime", ShortCode = "PWDateTime", Desc = "PWDateTime", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? PWDateTime
        {
            get { return _pWDateTime; }
            set { _pWDateTime = value; }
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "CAUserAuthorised", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int? CAUserAuthorised
        {
            get { return _cAUserAuthorised; }
            set { _cAUserAuthorised = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UserDataRights", Desc = "", ContextType = SysDic.All, Length = 2000)]
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
        [DataDesc(CName = "", ShortCode = "Imgsignature", Desc = "", ContextType = SysDic.All, Length = 16)]
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
        [DataDesc(CName = "", ShortCode = "Email", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string Email
        {
            get { return _email; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for Email", value, value.ToString());
                _email = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "OtherCode", Desc = "", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "", ShortCode = "Code6", Desc = "", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "", ShortCode = "Code7", Desc = "", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "", ShortCode = "Code8", Desc = "", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "", ShortCode = "Code9", Desc = "", ContextType = SysDic.All, Length = 50)]
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
        [DataDesc(CName = "", ShortCode = "Code10", Desc = "", ContextType = SysDic.All, Length = 50)]
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