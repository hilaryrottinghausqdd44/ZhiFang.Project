using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.RBAC
{
    #region Department

    /// <summary>
    /// Department object for NHibernate mapped table 'Department'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "Department", ShortCode = "Department", Desc = "")]
    public class Department : BaseEntityServiceByInt
    {
        #region Member Variables

        protected string _cName;
        protected string _shortName;
        protected string _shortCode;
        protected int _visible;
        protected int _dispOrder;
        protected string _hisOrderCode;
        protected string _urgentState;
        protected string _patState;
        protected string _code1;
        protected string _code2;
        protected string _code3;
        protected string _code4;
        protected string _code5;
        protected string _deptPhoneCode;
        protected int _parentID;


        #endregion

        #region Constructors

        public Department() { }

        public Department(string cName, string shortName, string shortCode, int visible, int dispOrder, string hisOrderCode, string urgentState, string patState, string code1, string code2, string code3, string code4, string code5, string deptPhoneCode, long labID, byte[] dataTimeStamp, int parentID, DateTime dataAddTime)
        {
            this._cName = cName;
            this._shortName = shortName;
            this._shortCode = shortCode;
            this._visible = visible;
            this._dispOrder = dispOrder;
            this._hisOrderCode = hisOrderCode;
            this._urgentState = urgentState;
            this._patState = patState;
            this._code1 = code1;
            this._code2 = code2;
            this._code3 = code3;
            this._code4 = code4;
            this._code5 = code5;
            this._deptPhoneCode = deptPhoneCode;
            this._labID = labID;
            this._dataTimeStamp = dataTimeStamp;
            this._parentID = parentID;
            this._dataAddTime = dataAddTime;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "CName", ShortCode = "CName", Desc = "CName", ContextType = SysDic.All, Length = 50)]
        public virtual string CName
        {
            get { return _cName; }
            set
            {
                _cName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "ShortName", ShortCode = "ShortName", Desc = "ShortName", ContextType = SysDic.All, Length = 50)]
        public virtual string ShortName
        {
            get { return _shortName; }
            set
            {
                _shortName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "ShortCode", ShortCode = "ShortCode", Desc = "ShortCode", ContextType = SysDic.All, Length = 50)]
        public virtual string ShortCode
        {
            get { return _shortCode; }
            set
            {
                _shortCode = value;
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
        [DataDesc(CName = "UrgentState", ShortCode = "UrgentState", Desc = "UrgentState", ContextType = SysDic.All, Length = 50)]
        public virtual string UrgentState
        {
            get { return _urgentState; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for UrgentState", value, value.ToString());
                _urgentState = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "PatState", ShortCode = "PatState", Desc = "PatState", ContextType = SysDic.All, Length = 50)]
        public virtual string PatState
        {
            get { return _patState; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for PatState", value, value.ToString());
                _patState = value;
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
        [DataDesc(CName = "DeptPhoneCode", ShortCode = "DeptPhoneCode", Desc = "DeptPhoneCode", ContextType = SysDic.All, Length = 50)]
        public virtual string DeptPhoneCode
        {
            get { return _deptPhoneCode; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for DeptPhoneCode", value, value.ToString());
                _deptPhoneCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "ParentID", ShortCode = "ParentID", Desc = "ParentID", ContextType = SysDic.All, Length = 4)]
        public virtual int ParentID
        {
            get { return _parentID; }
            set { _parentID = value; }
        }


        #endregion
    }
    #endregion
}