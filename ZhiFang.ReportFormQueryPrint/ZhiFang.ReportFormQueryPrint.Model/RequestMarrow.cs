using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.ReportFormQueryPrint.Model
{
    #region RequestMarrow

    /// <summary>
    /// RequestMarrow object for NHibernate mapped table 'RequestMarrow'.
    /// </summary>
    [DataContract]
    public class RequestMarrow 
    {
        #region Member Variables

        protected int _parItemNo;
        protected int _bloodNum;
        protected double _bloodPercent;
        protected int _marrowNum;
        protected double _marrowPercent;
        protected string _bloodDesc;
        protected string _marrowDesc;
        protected int _statusNo;
        protected string _refRange;
        protected int _equipNo;
        protected int _isCale;
        protected int _modified;
        protected DateTime? _itemDate;
        protected DateTime? _itemTime;
        protected int _isMatch;
        protected string _resultStatus;


        #endregion

        #region Public Properties


        [DataMember]
        public virtual int ParItemNo
        {
            get { return _parItemNo; }
            set { _parItemNo = value; }
        }

        [DataMember]
        public virtual int BloodNum
        {
            get { return _bloodNum; }
            set { _bloodNum = value; }
        }

        [DataMember]
        public virtual double BloodPercent
        {
            get { return _bloodPercent; }
            set { _bloodPercent = value; }
        }

        [DataMember]
        public virtual int MarrowNum
        {
            get { return _marrowNum; }
            set { _marrowNum = value; }
        }

        [DataMember]
        public virtual double MarrowPercent
        {
            get { return _marrowPercent; }
            set { _marrowPercent = value; }
        }

        [DataMember]
        public virtual string BloodDesc
        {
            get { return _bloodDesc; }
            set
            {
                if (value != null && value.Length > 250)
                    throw new ArgumentOutOfRangeException("Invalid value for BloodDesc", value, value.ToString());
                _bloodDesc = value;
            }
        }

        [DataMember]
        public virtual string MarrowDesc
        {
            get { return _marrowDesc; }
            set
            {
                if (value != null && value.Length > 250)
                    throw new ArgumentOutOfRangeException("Invalid value for MarrowDesc", value, value.ToString());
                _marrowDesc = value;
            }
        }

        [DataMember]
        public virtual int StatusNo
        {
            get { return _statusNo; }
            set { _statusNo = value; }
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
        public virtual int EquipNo
        {
            get { return _equipNo; }
            set { _equipNo = value; }
        }

        [DataMember]
        public virtual int IsCale
        {
            get { return _isCale; }
            set { _isCale = value; }
        }

        [DataMember]
        public virtual int Modified
        {
            get { return _modified; }
            set { _modified = value; }
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
        public virtual int IsMatch
        {
            get { return _isMatch; }
            set { _isMatch = value; }
        }

        [DataMember]
        public virtual string ResultStatus
        {
            get { return _resultStatus; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for ResultStatus", value, value.ToString());
                _resultStatus = value;
            }
        }


        #endregion
    }
    #endregion
}