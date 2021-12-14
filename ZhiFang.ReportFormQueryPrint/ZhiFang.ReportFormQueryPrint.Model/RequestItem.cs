using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.ReportFormQueryPrint.Model
{
    #region RequestItem

    /// <summary>
    /// RequestItem object for NHibernate mapped table 'RequestItem'.
    /// </summary>
    [DataContract]
    public class RequestItem 
    {
        #region Member Variables

        protected int _parItemNo;
        protected double _originalValue;
        protected double _reportValue;
        protected string _originalDesc;
        protected string _reportDesc;
        protected int _statusNo;
        protected string _refRange;
        protected int _equipNo;
        protected int _modified;
        protected DateTime? _itemDate;
        protected DateTime? _itemTime;
        protected int _isMatch;
        protected string _resultStatus;
        protected string _hisValue;
        protected string _hisComp;
        protected int _isReceive;
        protected string _serialNoParent;
        protected string _zdy1;
        protected string _zdy2;
        protected string _zdy3;
        protected string _zdy4;
        protected string _zdy5;
        protected string _countNodesItemSource;
        protected string _unit;
        protected int _plateNo;
        protected int _positionNo;
        protected string _equipCommMemo;
        protected int _equipCommSend;
        protected string _eValueState;
        protected string _eModule;
        protected string _ePosition;
        protected string _eSend;
        protected int _isRedo;
        protected int _isDel;
        protected string _hisReceiveDate;
        protected int _fromRedoNo;
        protected int _iReportstatus;
        protected string _itemTestMemo;
        protected string _itemDealWith;
        protected int _iEResult;
        protected string _mergeno;
        protected int _oldParItemNo;
        protected string _eErrorInfo;
        protected double _dilutionMultiple;
        private string _formno;


        #endregion

        #region Public Properties


        [DataMember]
        public virtual int ParItemNo
        {
            get { return _parItemNo; }
            set { _parItemNo = value; }
        }
        public string FormNo
        {
            set { _formno = value; }
            get { return _formno; }
        }
        [DataMember]
        public virtual double OriginalValue
        {
            get { return _originalValue; }
            set { _originalValue = value; }
        }

        [DataMember]
        public virtual double ReportValue
        {
            get { return _reportValue; }
            set { _reportValue = value; }
        }

        [DataMember]
        public virtual string OriginalDesc
        {
            get { return _originalDesc; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for OriginalDesc", value, value.ToString());
                _originalDesc = value;
            }
        }

        [DataMember]
        public virtual string ReportDesc
        {
            get { return _reportDesc; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for ReportDesc", value, value.ToString());
                _reportDesc = value;
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
                if (value != null && value.Length > 400)
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

        [DataMember]
        public virtual string HisValue
        {
            get { return _hisValue; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for HisValue", value, value.ToString());
                _hisValue = value;
            }
        }

        [DataMember]
        public virtual string HisComp
        {
            get { return _hisComp; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for HisComp", value, value.ToString());
                _hisComp = value;
            }
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
                if (value != null && value.Length > 60)
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
                if (value != null && value.Length > 60)
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
                if (value != null && value.Length > 60)
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
                if (value != null && value.Length > 60)
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
                if (value != null && value.Length > 60)
                    throw new ArgumentOutOfRangeException("Invalid value for Zdy5", value, value.ToString());
                _zdy5 = value;
            }
        }

        [DataMember]
        public virtual string CountNodesItemSource
        {
            get { return _countNodesItemSource; }
            set
            {
                if (value != null && value.Length > 1)
                    throw new ArgumentOutOfRangeException("Invalid value for CountNodesItemSource", value, value.ToString());
                _countNodesItemSource = value;
            }
        }

        [DataMember]
        public virtual string Unit
        {
            get { return _unit; }
            set
            {
                if (value != null && value.Length > 10)
                    throw new ArgumentOutOfRangeException("Invalid value for Unit", value, value.ToString());
                _unit = value;
            }
        }

        [DataMember]
        public virtual int PlateNo
        {
            get { return _plateNo; }
            set { _plateNo = value; }
        }

        [DataMember]
        public virtual int PositionNo
        {
            get { return _positionNo; }
            set { _positionNo = value; }
        }

        [DataMember]
        public virtual string EquipCommMemo
        {
            get { return _equipCommMemo; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for EquipCommMemo", value, value.ToString());
                _equipCommMemo = value;
            }
        }

        [DataMember]
        public virtual int EquipCommSend
        {
            get { return _equipCommSend; }
            set { _equipCommSend = value; }
        }

        [DataMember]
        public virtual string EValueState
        {
            get { return _eValueState; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for EValueState", value, value.ToString());
                _eValueState = value;
            }
        }

        [DataMember]
        public virtual string EModule
        {
            get { return _eModule; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for EModule", value, value.ToString());
                _eModule = value;
            }
        }

        [DataMember]
        public virtual string EPosition
        {
            get { return _ePosition; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for EPosition", value, value.ToString());
                _ePosition = value;
            }
        }

        [DataMember]
        public virtual string ESend
        {
            get { return _eSend; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for ESend", value, value.ToString());
                _eSend = value;
            }
        }

        [DataMember]
        public virtual int IsRedo
        {
            get { return _isRedo; }
            set { _isRedo = value; }
        }

        [DataMember]
        public virtual int IsDel
        {
            get { return _isDel; }
            set { _isDel = value; }
        }

        [DataMember]
        public virtual string HisReceiveDate
        {
            get { return _hisReceiveDate; }
            set
            {
                if (value != null && value.Length > 40)
                    throw new ArgumentOutOfRangeException("Invalid value for HisReceiveDate", value, value.ToString());
                _hisReceiveDate = value;
            }
        }

        [DataMember]
        public virtual int FromRedoNo
        {
            get { return _fromRedoNo; }
            set { _fromRedoNo = value; }
        }

        [DataMember]
        public virtual int IReportstatus
        {
            get { return _iReportstatus; }
            set { _iReportstatus = value; }
        }

        [DataMember]
        public virtual string ItemTestMemo
        {
            get { return _itemTestMemo; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ItemTestMemo", value, value.ToString());
                _itemTestMemo = value;
            }
        }

        [DataMember]
        public virtual string ItemDealWith
        {
            get { return _itemDealWith; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ItemDealWith", value, value.ToString());
                _itemDealWith = value;
            }
        }

        [DataMember]
        public virtual int IEResult
        {
            get { return _iEResult; }
            set { _iEResult = value; }
        }

        [DataMember]
        public virtual string Mergeno
        {
            get { return _mergeno; }
            set
            {
                if (value != null && value.Length > 30)
                    throw new ArgumentOutOfRangeException("Invalid value for Mergeno", value, value.ToString());
                _mergeno = value;
            }
        }

        [DataMember]
        public virtual int OldParItemNo
        {
            get { return _oldParItemNo; }
            set { _oldParItemNo = value; }
        }

        [DataMember]
        public virtual string EErrorInfo
        {
            get { return _eErrorInfo; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for EErrorInfo", value, value.ToString());
                _eErrorInfo = value;
            }
        }

        [DataMember]
        public virtual double DilutionMultiple
        {
            get { return _dilutionMultiple; }
            set { _dilutionMultiple = value; }
        }


        #endregion
    }
    #endregion
}