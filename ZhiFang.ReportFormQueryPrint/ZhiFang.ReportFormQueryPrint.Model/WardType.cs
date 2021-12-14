using System;
using System.Runtime.Serialization;

namespace ZhiFang.ReportFormQueryPrint.Model
{
    #region WardType
    
    public class WardType 
    {
        #region Member Variables

        protected long _wardNo;
        protected string _cName;
        protected string _shortName;
        protected string _shortCode;
        protected int _visible;
        protected int _dispOrder;
        protected string _hisOrderCode;
        protected string _code1;
        protected string _code2;
        protected string _code3;
        protected string _code4;
        protected string _code5;
        protected string _urgentState;
        protected string _patState;
        protected string _code6;
        protected string _code7;
        protected string _code8;


        #endregion

        #region Constructors

        public WardType() { }

        public WardType(string cName, string shortName, string shortCode, int visible, int dispOrder, string hisOrderCode, string code1, string code2, string code3, string code4, string code5, string urgentState, string patState, string code6, string code7, string code8)
        {
            this._cName = cName;
            this._shortName = shortName;
            this._shortCode = shortCode;
            this._visible = visible;
            this._dispOrder = dispOrder;
            this._hisOrderCode = hisOrderCode;
            this._code1 = code1;
            this._code2 = code2;
            this._code3 = code3;
            this._code4 = code4;
            this._code5 = code5;
            this._urgentState = urgentState;
            this._patState = patState;
            this._code6 = code6;
            this._code7 = code7;
            this._code8 = code8;
        }

        #endregion

        #region Public Properties


        [DataMember]
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
        public virtual long WardNo
        {
            get { return _wardNo; }
            set { _wardNo = value;}
        }

        [DataMember]
        public virtual string ShortName
        {
            get { return _shortName; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for ShortName", value, value.ToString());
                _shortName = value;
            }
        }

        [DataMember]
        public virtual string ShortCode
        {
            get { return _shortCode; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for ShortCode", value, value.ToString());
                _shortCode = value;
            }
        }

        [DataMember]
        public virtual int Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        [DataMember]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
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


        #endregion
    }
    #endregion
}
