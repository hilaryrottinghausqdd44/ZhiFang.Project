using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.RBAC
{
    #region Doctor

    /// <summary>
    /// Doctor object for NHibernate mapped table 'Doctor'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "Doctor", ShortCode = "Doctor", Desc = "")]
    public class Doctor : BaseEntityServiceByInt
    {
        #region Member Variables

        protected string _cName;
        protected string _shortCode;
        protected string _hisOrderCode;
        protected int _visible;
        protected string _doctorPhoneCode;
        protected string _code1;
        protected string _code2;
        protected string _code3;
        protected string _code4;
        protected string _code5;
        protected long _gradeNo;
        protected string _adminName;

        protected int _dispOrder;
        #endregion

        #region Constructors

        public Doctor() { }

        public Doctor(string cName, string shortCode, string hisOrderCode, int visible, string doctorPhoneCode, string code1, string code2, string code3, string code4, string code5, long gradeNo, string adminName, long labID, byte[] dataTimeStamp, int dispOrder)
        {
            this._cName = cName;
            this._shortCode = shortCode;
            this._hisOrderCode = hisOrderCode;
            this._visible = visible;
            this._dispOrder = dispOrder;
            this._doctorPhoneCode = doctorPhoneCode;
            this._code1 = code1;
            this._code2 = code2;
            this._code3 = code3;
            this._code4 = code4;
            this._code5 = code5;
            this._gradeNo = gradeNo;
            this._adminName = adminName;
            this._labID = labID;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties
        [DataMember]
        [DataDesc(CName = "DispOrder", ShortCode = "DispOrder", Desc = "DispOrder", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }
        [DataMember]
        [DataDesc(CName = "CName", ShortCode = "CName", Desc = "CName", ContextType = SysDic.All, Length = 40)]
        public virtual string CName
        {
            get { return _cName; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
                _cName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "ShortCode", ShortCode = "ShortCode", Desc = "ShortCode", ContextType = SysDic.All, Length = 40)]
        public virtual string ShortCode
        {
            get { return _shortCode; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for ShortCode", value, value.ToString());
                _shortCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "HisOrderCode", ShortCode = "HisOrderCode", Desc = "HisOrderCode", ContextType = SysDic.All, Length = 20)]
        public virtual string HisOrderCode
        {
            get { return _hisOrderCode; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for HisOrderCode", value, value.ToString());
                _hisOrderCode = value;
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
        [DataDesc(CName = "DoctorPhoneCode", ShortCode = "DoctorPhoneCode", Desc = "DoctorPhoneCode", ContextType = SysDic.All, Length = 20)]
        public virtual string DoctorPhoneCode
        {
            get { return _doctorPhoneCode; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for DoctorPhoneCode", value, value.ToString());
                _doctorPhoneCode = value;
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
        [DataDesc(CName = "GradeNo", ShortCode = "GradeNo", Desc = "GradeNo", ContextType = SysDic.All)]
        public virtual long GradeNo
        {
            get { return _gradeNo; }
            set { _gradeNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "AdminName", ShortCode = "AdminName", Desc = "AdminName", ContextType = SysDic.All, Length = 20)]
        public virtual string AdminName
        {
            get { return _adminName; }
            set
            {
                _adminName = value;
            }
        }


        #endregion
    }
    #endregion
}