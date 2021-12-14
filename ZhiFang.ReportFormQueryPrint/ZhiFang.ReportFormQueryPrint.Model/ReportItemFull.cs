using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.ReportFormQueryPrint.Model
{
    #region ReportItemFull

   
    public class ReportItemFull
    {
        #region Member Variables

        private long _ReportPublicationID;
        protected string _reportFormID;
        protected DateTime? _receiveDate;
        protected long _sectionNo;
        protected long _testTypeNo;
        protected string _sampleNo;
        protected int _orderNo;
        protected long _parItemNo;
        protected string _paritemName;
        protected long _ItemNo;
        protected string _itemCname;
        protected string _itemEname;
        protected double _reportValue;
        protected string _reportDesc;
        protected string _itemValue;
        protected string _itemUnit;
        protected string _resultStatus;
        protected string _refRange;
        protected long _equipNo;
        protected string _equipName;
        protected string _diagMethod;
        protected int _prec;
        protected string _standardCode;
        protected string _itemDesc;
        protected int _secretGrade;
        protected int _visible;
        protected string _zDY1;
        protected string _zDY2;
        protected string _zDY3;
        protected string _zDY4;
        protected string _zDY5;
        protected DateTime? _dataUpdateTime;
        protected DateTime? _dataMigrationTime;
        protected string _defaultReagent;
        protected string _reportValue2;
        protected string _reportValue3;
        protected byte[] _itemResultInfo;
        protected int _pItemOrder;
        protected string _printItemDesc;


        #endregion

        #region Public Properties

        public long ReportPublicationID
        {
            set { _ReportPublicationID = value; }
            get { return _ReportPublicationID; }
        }
        public virtual string ReportFormID
        {
            get { return _reportFormID; }
            set
            {
                
                _reportFormID = value;
            }
        }
        public long ItemNo
        {
            set { _ItemNo = value; }
            get { return _ItemNo; }
        }

        public virtual DateTime? ReceiveDate
        {
            get { return _receiveDate; }
            set { _receiveDate = value; }
        }

        
        public virtual long SectionNo
        {
            get { return _sectionNo; }
            set { _sectionNo = value; }
        }

        public virtual long TestTypeNo
        {
            get { return _testTypeNo; }
            set { _testTypeNo = value; }
        }

       
        public virtual string SampleNo
        {
            get { return _sampleNo; }
            set
            {
               
                _sampleNo = value;
            }
        }

       
        public virtual int OrderNo
        {
            get { return _orderNo; }
            set { _orderNo = value; }
        }

      
        public virtual long ParItemNo
        {
            get { return _parItemNo; }
            set { _parItemNo = value; }
        }

        
        public virtual string ParitemName
        {
            get { return _paritemName; }
            set
            {
                
                _paritemName = value;
            }
        }

       
        public virtual string ItemCname
        {
            get { return _itemCname; }
            set
            {
                _itemCname = value;
            }
        }

       
        public virtual string ItemEname
        {
            get { return _itemEname; }
            set
            {
              
                _itemEname = value;
            }
        }

        
        public virtual double ReportValue
        {
            get { return _reportValue; }
            set { _reportValue = value; }
        }

        
        public virtual string ReportDesc
        {
            get { return _reportDesc; }
            set
            {
               
                _reportDesc = value;
            }
        }

       
        public virtual string ItemValue
        {
            get { return _itemValue; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for ItemValue", value, value.ToString());
                _itemValue = value;
            }
        }

        
        public virtual string ItemUnit
        {
            get { return _itemUnit; }
            set
            {
                
                _itemUnit = value;
            }
        }

        public virtual string ResultStatus
        {
            get { return _resultStatus; }
            set
            {
               
                _resultStatus = value;
            }
        }

        public virtual string RefRange
        {
            get { return _refRange; }
            set
            {
              
                _refRange = value;
            }
        }

        public virtual long EquipNo
        {
            get { return _equipNo; }
            set { _equipNo = value; }
        }

        
        public virtual string EquipName
        {
            get { return _equipName; }
            set
            {
               
                _equipName = value;
            }
        }

        public virtual string DiagMethod
        {
            get { return _diagMethod; }
            set
            {
                _diagMethod = value;
            }
        }

        public virtual int Prec
        {
            get { return _prec; }
            set { _prec = value; }
        }

        public virtual string StandardCode
        {
            get { return _standardCode; }
            set
            {
                _standardCode = value;
            }
        }

        public virtual string ItemDesc
        {
            get { return _itemDesc; }
            set
            {
                _itemDesc = value;
            }
        }

        public virtual int SecretGrade
        {
            get { return _secretGrade; }
            set { _secretGrade = value; }
        }

        public virtual int Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        public virtual string ZDY1
        {
            get { return _zDY1; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for ZDY1", value, value.ToString());
                _zDY1 = value;
            }
        }

        public virtual string ZDY2
        {
            get { return _zDY2; }
            set
            {
                _zDY2 = value;
            }
        }
        public virtual string ZDY3
        {
            get { return _zDY3; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for ZDY3", value, value.ToString());
                _zDY3 = value;
            }
        }

        public virtual string ZDY4
        {
            get { return _zDY4; }
            set
            {
                _zDY4 = value;
            }
        }

        public virtual string ZDY5
        {
            get { return _zDY5; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for ZDY5", value, value.ToString());
                _zDY5 = value;
            }
        }

        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        public virtual DateTime? DataMigrationTime
        {
            get { return _dataMigrationTime; }
            set { _dataMigrationTime = value; }
        }

        public virtual string DefaultReagent
        {
            get { return _defaultReagent; }
            set
            {
                _defaultReagent = value;
            }
        }

        public virtual string ReportValue2
        {
            get { return _reportValue2; }
            set
            {
                _reportValue2 = value;
            }
        }

        public virtual string ReportValue3
        {
            get { return _reportValue3; }
            set
            {
                _reportValue3 = value;
            }
        }

        public virtual byte[] ItemResultInfo
        {
            get { return _itemResultInfo; }
            set { _itemResultInfo = value; }
        }

        public virtual int PItemOrder
        {
            get { return _pItemOrder; }
            set { _pItemOrder = value; }
        }

        public virtual string PrintItemDesc
        {
            get { return _printItemDesc; }
            set
            {
                _printItemDesc = value;
            }
        }


        #endregion
    }
    #endregion
}