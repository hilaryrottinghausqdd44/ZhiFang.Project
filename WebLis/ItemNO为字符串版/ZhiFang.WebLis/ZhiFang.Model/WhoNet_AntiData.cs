using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace ZhiFang.Model
{
    //WhoNet_AntiData
    public class WhoNet_AntiData
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
        /// AntiID
        /// </summary>		
        private long? _antiid;
        public long? AntiID
        {
            get { return _antiid; }
            set { _antiid = value; }
        }
        /// <summary>
        /// AntiName
        /// </summary>		
        private string _antiname;
        public string AntiName
        {
            get { return _antiname; }
            set { _antiname = value; }
        }
        /// <summary>
        /// TestMethod
        /// </summary>		
        private string _testmethod;
        public string TestMethod
        {
            get { return _testmethod; }
            set { _testmethod = value; }
        }
        /// <summary>
        /// RefRange
        /// </summary>		
        private string _refrange;
        public string RefRange
        {
            get { return _refrange; }
            set { _refrange = value; }
        }
        /// <summary>
        /// Suscept
        /// </summary>		
        private string _suscept;
        public string Suscept
        {
            get { return _suscept; }
            set { _suscept = value; }
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
        /// DataUpdateTime
        /// </summary>		
        private DateTime? _dataupdatetime = DateTime.Now;
        public DateTime? DataUpdateTime
        {
            get { return _dataupdatetime; }
            set { _dataupdatetime = value; }
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
        /// MicroID
        /// </summary>
        private long? _MicroID;
        public long? MicroID
        {
            get { return _MicroID; }
            set { _MicroID = value; }
        }

    }
}

