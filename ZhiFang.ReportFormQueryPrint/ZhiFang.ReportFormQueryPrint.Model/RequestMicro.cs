using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.ReportFormQueryPrint.Model
{
    #region RequestMicro

    /// <summary>
    /// RequestMicro object for NHibernate mapped table 'RequestMicro'.
    /// </summary>
    [DataContract]
    public class RequestMicro
    {
        #region Member Variables

        protected int _itemNo;
        protected int _descNo;
        protected int _microNo;
        protected string _microDesc;
        protected int _antiNo;
        protected string _suscept;
        protected double _susQuan;
        protected string _susDesc;
        protected string _refRange;
        protected DateTime? _itemDate;
        protected DateTime? _itemTime;
        protected string _itemDesc;
        protected int _equipNo;
        protected int _modified;
        protected int _isMatch;
        protected int _checkType;
        protected int _isReceive;
        protected string _serialNoParent;
        protected string _zdy1;
        protected string _zdy2;
        protected string _zdy3;
        protected string _zdy4;
        protected string _zdy5;
        protected string _antiDesc;
        protected int _iEResult;
        protected string _antiGroupType;
        protected string _expertDesc;
        protected int _resultState;
        protected string _microResultDesc;


        #endregion

        #region Public Properties


        [DataMember]
        public virtual int ItemNo
        {
            get { return _itemNo; }
            set { _itemNo = value; }
        }

        [DataMember]
        public virtual int DescNo
        {
            get { return _descNo; }
            set { _descNo = value; }
        }

        [DataMember]
        public virtual int MicroNo
        {
            get { return _microNo; }
            set { _microNo = value; }
        }

        [DataMember]
        public virtual string MicroDesc
        {
            get { return _microDesc; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for MicroDesc", value, value.ToString());
                _microDesc = value;
            }
        }

        [DataMember]
        public virtual int AntiNo
        {
            get { return _antiNo; }
            set { _antiNo = value; }
        }

        [DataMember]
        public virtual string Suscept
        {
            get { return _suscept; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Suscept", value, value.ToString());
                _suscept = value;
            }
        }

        [DataMember]
        public virtual double SusQuan
        {
            get { return _susQuan; }
            set { _susQuan = value; }
        }

        [DataMember]
        public virtual string SusDesc
        {
            get { return _susDesc; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for SusDesc", value, value.ToString());
                _susDesc = value;
            }
        }

        [DataMember]
        public virtual string RefRange
        {
            get { return _refRange; }
            set
            {
                if (value != null && value.Length > 30)
                    throw new ArgumentOutOfRangeException("Invalid value for RefRange", value, value.ToString());
                _refRange = value;
            }
        }

        [DataMember]
        public virtual DateTime? ItemDate
        {
            get { return _itemDate; }
            set { _itemDate = value; }
        }

        [DataMember]
        public virtual DateTime? ItemTime
        {
            get { return _itemTime; }
            set { _itemTime = value; }
        }

        [DataMember]
        public virtual string ItemDesc
        {
            get { return _itemDesc; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for ItemDesc", value, value.ToString());
                _itemDesc = value;
            }
        }

        [DataMember]
        public virtual int EquipNo
        {
            get { return _equipNo; }
            set { _equipNo = value; }
        }

        [DataMember]
        public virtual int Modified
        {
            get { return _modified; }
            set { _modified = value; }
        }

        [DataMember]
        public virtual int IsMatch
        {
            get { return _isMatch; }
            set { _isMatch = value; }
        }

        [DataMember]
        public virtual int CheckType
        {
            get { return _checkType; }
            set { _checkType = value; }
        }

        [DataMember]
        public virtual int IsReceive
        {
            get { return _isReceive; }
            set { _isReceive = value; }
        }

        [DataMember]
        public virtual string SerialNoParent
        {
            get { return _serialNoParent; }
            set
            {
                if (value != null && value.Length > 60)
                    throw new ArgumentOutOfRangeException("Invalid value for SerialNoParent", value, value.ToString());
                _serialNoParent = value;
            }
        }

        [DataMember]
        public virtual string Zdy1
        {
            get { return _zdy1; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Zdy1", value, value.ToString());
                _zdy1 = value;
            }
        }

        [DataMember]
        public virtual string Zdy2
        {
            get { return _zdy2; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Zdy2", value, value.ToString());
                _zdy2 = value;
            }
        }

        [DataMember]
        public virtual string Zdy3
        {
            get { return _zdy3; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Zdy3", value, value.ToString());
                _zdy3 = value;
            }
        }

        [DataMember]
        public virtual string Zdy4
        {
            get { return _zdy4; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Zdy4", value, value.ToString());
                _zdy4 = value;
            }
        }

        [DataMember]
        public virtual string Zdy5
        {
            get { return _zdy5; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Zdy5", value, value.ToString());
                _zdy5 = value;
            }
        }

        [DataMember]
        public virtual string AntiDesc
        {
            get { return _antiDesc; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for AntiDesc", value, value.ToString());
                _antiDesc = value;
            }
        }

        [DataMember]
        public virtual int IEResult
        {
            get { return _iEResult; }
            set { _iEResult = value; }
        }

        [DataMember]
        public virtual string AntiGroupType
        {
            get { return _antiGroupType; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for AntiGroupType", value, value.ToString());
                _antiGroupType = value;
            }
        }

        [DataMember]
        public virtual string ExpertDesc
        {
            get { return _expertDesc; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for ExpertDesc", value, value.ToString());
                _expertDesc = value;
            }
        }

        [DataMember]
        public virtual int ResultState
        {
            get { return _resultState; }
            set { _resultState = value; }
        }

        [DataMember]
        public virtual string MicroResultDesc
        {
            get { return _microResultDesc; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for MicroResultDesc", value, value.ToString());
                _microResultDesc = value;
            }
        }


        #endregion
    }
    #endregion
}