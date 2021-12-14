using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace ZhiFang.ReportFormQueryPrint.Model
{
    //HisDisp
    public class HisDisp
    {

        /// <summary>
        /// ReceiveDate
        /// </summary>		
        private DateTime _receivestartdate;
        public DateTime ReceiveStartDate
        {
            get { return _receivestartdate; }
            set { _receivestartdate = value; }
        }
        /// <summary>
        /// ReceiveDate
        /// </summary>		
        private DateTime _receiveenddate;
        public DateTime ReceiveEndDate
        {
            get { return _receiveenddate; }
            set { _receiveenddate = value; }
        }
        /// <summary>
        /// SectionNo
        /// </summary>		
        private int _sectionno;
        public int SectionNo
        {
            get { return _sectionno; }
            set { _sectionno = value; }
        }
        /// <summary>
        /// TestTypeNo
        /// </summary>		
        private int _testtypeno;
        public int TestTypeNo
        {
            get { return _testtypeno; }
            set { _testtypeno = value; }
        }
        /// <summary>
        /// SampleNo
        /// </summary>		
        private string _sampleno;
        public string SampleNo
        {
            get { return _sampleno; }
            set { _sampleno = value; }
        }
        /// <summary>
        /// ItemNo
        /// </summary>		
        private string _itemno;
        public string ItemNo
        {
            get { return _itemno; }
            set { _itemno = value; }
        }
        /// <summary>
        /// ReportValue
        /// </summary>		
        private decimal _reportvalue;
        public decimal ReportValue
        {
            get { return _reportvalue; }
            set { _reportvalue = value; }
        }
        /// <summary>
        /// PatNo
        /// </summary>		
        private string _patno;
        public string PatNo
        {
            get { return _patno; }
            set { _patno = value; }
        }
        /// <summary>
        /// SampleTypeNo
        /// </summary>		
        private string _sampletypeno;
        public string SampleTypeNo
        {
            get { return _sampletypeno; }
            set { _sampletypeno = value; }
        }

    }
}
