using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.ReportFormQueryPrint.Model
{
    #region ReportDrugGene

    /// <summary>
    /// ReportDrugGeneFull object for NHibernate mapped table 'ReportDrugGeneFull'.
    /// </summary>

    public class ReportDrugGene
	{
		#region Member Variables
		
        protected string _reportFormID;
        protected DateTime? _receiveDate;
        protected long _sectionNo;
        protected long _testTypeNo;
        protected string _sampleNo;
        protected int _orderNo;
        protected string _testDrugName;
        protected string _itemCname;
        protected string _itemname;
        protected double _reportValue;
        protected string _itemValue;
        protected string _resultPhraseDesc;
        protected DateTime? _dataUpdateTime;
        protected DateTime? _dataMigrationTime;
        protected string _defaultReagent;
        protected int _secretgrade;
		

		#endregion

		#region Public Properties

        public virtual string ReportFormID
		{
			get { return _reportFormID; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ReportFormID", value, value.ToString());
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
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for SampleNo", value, value.ToString());
				_sampleNo = value;
			}
		}

        public virtual int OrderNo
		{
			get { return _orderNo; }
			set { _orderNo = value; }
		}

        
        public virtual string TestDrugName
		{
			get { return _testDrugName; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for TestDrugName", value, value.ToString());
				_testDrugName = value;
			}
		}

       
        public virtual string ItemCname
		{
			get { return _itemCname; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ItemCname", value, value.ToString());
				_itemCname = value;
			}
		}

       
        public virtual string Itemname
		{
			get { return _itemname; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for Itemname", value, value.ToString());
				_itemname = value;
			}
		}

        
        public virtual double ReportValue
		{
			get { return _reportValue; }
			set { _reportValue = value; }
		}

        
        public virtual string ItemValue
		{
			get { return _itemValue; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for ItemValue", value, value.ToString());
				_itemValue = value;
			}
		}

        
        public virtual string ResultPhraseDesc
		{
			get { return _resultPhraseDesc; }
			set
			{
				if ( value != null && value.Length > 400)
					throw new ArgumentOutOfRangeException("Invalid value for ResultPhraseDesc", value, value.ToString());
				_resultPhraseDesc = value;
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
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for DefaultReagent", value, value.ToString());
				_defaultReagent = value;
			}
		}

        
        public virtual int Secretgrade
		{
			get { return _secretgrade; }
			set { _secretgrade = value; }
		}

		
		#endregion
	}
	#endregion
}