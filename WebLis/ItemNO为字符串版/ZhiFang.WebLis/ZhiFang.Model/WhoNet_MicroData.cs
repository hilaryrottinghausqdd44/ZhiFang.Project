using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace ZhiFang.Model
{
    //WhoNet_MicroData
    public class WhoNet_MicroData
    {

        /// <summary>
        /// LabID
        /// </summary>		
        private long? _labid;
        public long? LabID
        {
            get { return _labid; }
            set { _labid = value; }
        }
        /// <summary>
        /// MicroID
        /// </summary>		
        private long? _microid;
        public long? MicroID
        {
            get { return _microid; }
            set { _microid = value; }
        }
        /// <summary>
        /// FomID
        /// </summary>		
        private long? _fomid;
        public long? FomID
        {
            get { return _fomid; }
            set { _fomid = value; }
        }
        /// <summary>
        /// date_data
        /// </summary>		
        private DateTime? _date_data = DateTime.Now;
        public DateTime? date_data
        {
            get { return _date_data; }
            set { _date_data = value; }
        }
        /// <summary>
        /// organism
        /// </summary>		
        private string _organism;
        public string organism
        {
            get { return _organism; }
            set { _organism = value; }
        }
        /// <summary>
        /// org_type
        /// </summary>		
        private string _org_type;
        public string org_type
        {
            get { return _org_type; }
            set { _org_type = value; }
        }
        /// <summary>
        /// origin
        /// </summary>		
        private string _origin;
        public string origin
        {
            get { return _origin; }
            set { _origin = value; }
        }
        /// <summary>
        /// ESBL
        /// </summary>		
        private string _esbl;
        public string ESBL
        {
            get { return _esbl; }
            set { _esbl = value; }
        }
        /// <summary>
        /// comment
        /// </summary>		
        private string _comment;
        public string comment
        {
            get { return _comment; }
            set { _comment = value; }
        }
        /// <summary>
        /// DataAddTime
        /// </summary>		
        private DateTime? _dataaddtime = DateTime.Now;
        public DateTime? DataAddTime
        {
            get { return _dataaddtime; }
            set { _dataaddtime = value; }
        }
        /// <summary>
        /// DataUpDateTime
        /// </summary>		
        private DateTime? _dataupDateTime = DateTime.Now;
        public DateTime? DataUpDateTime
        {
            get { return _dataupDateTime; }
            set { _dataupDateTime = value; }
        }
        /// <summary>
        /// DataTimeStamp
        /// </summary>		
        private DateTime? _datatimestamp = DateTime.Now;
        public DateTime? DataTimeStamp
        {
            get { return _datatimestamp; }
            set { _datatimestamp = value; }
        }
        /// <summary>
        /// beta_lact
        /// </summary>		
        private string _beta_lact;
        public string beta_lact
        {
            get { return _beta_lact; }
            set { _beta_lact = value; }
        }
      

    }
}

