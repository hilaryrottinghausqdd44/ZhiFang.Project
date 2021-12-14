using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.ReportFormQueryPrint.Model
{
    #region ReportItemFull

   
    public class ReportMarrowFull
    {
        #region Member Variables

        private long _ReportPublicationID;
        private long _itemno;
        protected string _reportFormID;
        protected DateTime? _receiveDate;
        protected long _sectionNo;
        protected long _testTypeNo;
        protected string _sampleNo;
        protected int _orderNo;
        protected long _parItemNo;
        protected string _paritemName;
        protected string _itemCname;
        protected string _itemEname;
        protected int _bloodNum;
        protected double _bloodPercent;
        protected string _bloodDesc;
        protected int _marrowNum;
        protected double _marrowPercent;
        protected string _marrowDesc;
        protected string _refRange;
        protected long _equipNo;
        protected string _equipName;
        protected string _resultStatus;
        protected string _diagMethod;
        protected DateTime? _dataUpdateTime;
        protected DateTime? _dataMigrationTime;


        #endregion

        #region Public Properties

        public long ReportPublicationID
        {
            set { _ReportPublicationID = value; }
            get { return _ReportPublicationID; }
        }

        public long ItemNo
        {
            set { _itemno = value; }
            get { return _itemno; }
        }
        public virtual string ReportFormID
        {
            get { return _reportFormID; }
            set
            {
                _reportFormID = value;
            }
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

        public virtual int BloodNum
        {
            get { return _bloodNum; }
            set { _bloodNum = value; }
        }

        public virtual double BloodPercent
        {
            get { return _bloodPercent; }
            set { _bloodPercent = value; }
        }

        public virtual string BloodDesc
        {
            get { return _bloodDesc; }
            set
            {
                _bloodDesc = value;
            }
        }

        public virtual int MarrowNum
        {
            get { return _marrowNum; }
            set { _marrowNum = value; }
        }

        public virtual double MarrowPercent
        {
            get { return _marrowPercent; }
            set { _marrowPercent = value; }
        }

        public virtual string MarrowDesc
        {
            get { return _marrowDesc; }
            set
            {
                _marrowDesc = value;
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

        public virtual string ResultStatus
        {
            get { return _resultStatus; }
            set
            {
                _resultStatus = value;
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


        #endregion
    }
    #endregion
}