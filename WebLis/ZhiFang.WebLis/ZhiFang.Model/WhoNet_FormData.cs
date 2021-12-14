using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace ZhiFang.Model
{
    //WhoNet_FormData
    public class WhoNet_FormData
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
        /// FormID
        /// </summary>		
        private long? _formid;
        public long? FormID
        {
            get { return _formid; }
            set { _formid = value; }
        }
        /// <summary>
        /// country_a
        /// </summary>		
        private string _country_a;
        public string country_a
        {
            get { return _country_a; }
            set { _country_a = value; }
        }
        /// <summary>
        /// laboratory
        /// </summary>		
        private string _laboratory;
        public string laboratory
        {
            get { return _laboratory; }
            set { _laboratory = value; }
        }
        /// <summary>
        /// patient_id
        /// </summary>		
        private string _patient_id;
        public string patient_id
        {
            get { return _patient_id; }
            set { _patient_id = value; }
        }
        /// <summary>
        /// last_name
        /// </summary>		
        private string _last_name;
        public string last_name
        {
            get { return _last_name; }
            set { _last_name = value; }
        }
        /// <summary>
        /// first_name
        /// </summary>		
        private string _first_name;
        public string first_name
        {
            get { return _first_name; }
            set { _first_name = value; }
        }
        /// <summary>
        /// sex
        /// </summary>		
        private string _sex;
        public string sex
        {
            get { return _sex; }
            set { _sex = value; }
        }
        /// <summary>
        /// age
        /// </summary>		
        private string _age;
        public string age
        {
            get { return _age; }
            set { _age = value; }
        }
        /// <summary>
        /// pat_type
        /// </summary>		
        private string _pat_type;
        public string pat_type
        {
            get { return _pat_type; }
            set { _pat_type = value; }
        }
        /// <summary>
        /// ward
        /// </summary>		
        private string _ward;
        public string ward
        {
            get { return _ward; }
            set { _ward = value; }
        }
        /// <summary>
        /// department
        /// </summary>		
        private string _department;
        public string department
        {
            get { return _department; }
            set { _department = value; }
        }
        /// <summary>
        /// ward_type
        /// </summary>		
        private string _ward_type;
        public string ward_type
        {
            get { return _ward_type; }
            set { _ward_type = value; }
        }
        /// <summary>
        /// date_brith
        /// </summary>		
        private DateTime? _date_brith;
        public DateTime? date_brith
        {
            get { return _date_brith; }
            set { _date_brith = value; }
        }
        /// <summary>
        /// institut
        /// </summary>		
        private string _institut;
        public string institut
        {
            get { return _institut; }
            set { _institut = value; }
        }
        /// <summary>
        /// SPEC_NUM
        /// </summary>		
        private string _spec_num;
        public string SPEC_NUM
        {
            get { return _spec_num; }
            set { _spec_num = value; }
        }
        /// <summary>
        /// SPEC_DATE
        /// </summary>		
        private DateTime? _spec_date;
        public DateTime? SPEC_DATE
        {
            get { return _spec_date; }
            set { _spec_date = value; }
        }
        /// <summary>
        /// SPEC_TYPE
        /// </summary>		
        private string _spec_type;
        public string SPEC_TYPE
        {
            get { return _spec_type; }
            set { _spec_type = value; }
        }
        /// <summary>
        /// SPEC_CODE
        /// </summary>		
        private string _spec_code;
        public string SPEC_CODE
        {
            get { return _spec_code; }
            set { _spec_code = value; }
        }
        /// <summary>
        /// SPEC_REAS
        /// </summary>		
        private string _spec_reas;
        public string SPEC_REAS
        {
            get { return _spec_reas; }
            set { _spec_reas = value; }
        }
        /// <summary>
        /// DATE_ADMIS
        /// </summary>		
        private DateTime? _date_admis;
        public DateTime? DATE_ADMIS
        {
            get { return _date_admis; }
            set { _date_admis = value; }
        }
        /// <summary>
        /// DATE_DISCH
        /// </summary>		
        private DateTime? _date_disch = DateTime.Now;
        public DateTime? DATE_DISCH
        {
            get { return _date_disch; }
            set { _date_disch = value; }
        }
        /// <summary>
        /// DATE_OPER
        /// </summary>		
        private DateTime? _date_oper = DateTime.Now;
        public DateTime? DATE_OPER
        {
            get { return _date_oper; }
            set { _date_oper = value; }
        }
        /// <summary>
        /// DATE_WARD
        /// </summary>		
        private DateTime? _date_ward = DateTime.Now;
        public DateTime? DATE_WARD
        {
            get { return _date_ward; }
            set { _date_ward = value; }
        }
        /// <summary>
        /// DIAGNOSIS
        /// </summary>		
        private string _diagnosis;
        public string DIAGNOSIS
        {
            get { return _diagnosis; }
            set { _diagnosis = value; }
        }
        /// <summary>
        /// DATE_INFEC
        /// </summary>		
        private DateTime? _date_infec = DateTime.Now;
        public DateTime? DATE_INFEC
        {
            get { return _date_infec; }
            set { _date_infec = value; }
        }
        /// <summary>
        /// SITEINFECT
        /// </summary>		
        private string _siteinfect;
        public string SITEINFECT
        {
            get { return _siteinfect; }
            set { _siteinfect = value; }
        }
        /// <summary>
        /// OPERATION
        /// </summary>		
        private string _operation;
        public string OPERATION
        {
            get { return _operation; }
            set { _operation = value; }
        }
        /// <summary>
        /// ORDER_MD
        /// </summary>		
        private string _order_md;
        public string ORDER_MD
        {
            get { return _order_md; }
            set { _order_md = value; }
        }
        /// <summary>
        /// CLNOUTCOME
        /// </summary>		
        private string _clnoutcome;
        public string CLNOUTCOME
        {
            get { return _clnoutcome; }
            set { _clnoutcome = value; }
        }
        /// <summary>
        /// PHYSICIAN
        /// </summary>		
        private string _physician;
        public string PHYSICIAN
        {
            get { return _physician; }
            set { _physician = value; }
        }
        /// <summary>
        /// PRIOR_ABX
        /// </summary>		
        private string _prior_abx;
        public string PRIOR_ABX
        {
            get { return _prior_abx; }
            set { _prior_abx = value; }
        }
        /// <summary>
        /// RESP_TO_TX
        /// </summary>		
        private string _resp_to_tx;
        public string RESP_TO_TX
        {
            get { return _resp_to_tx; }
            set { _resp_to_tx = value; }
        }
        /// <summary>
        /// SURGEON
        /// </summary>		
        private string _surgeon;
        public string SURGEON
        {
            get { return _surgeon; }
            set { _surgeon = value; }
        }
        /// <summary>
        /// STORAGELOC
        /// </summary>		
        private string _storageloc;
        public string STORAGELOC
        {
            get { return _storageloc; }
            set { _storageloc = value; }
        }
        /// <summary>
        /// STORAGENUM
        /// </summary>		
        private string _storagenum;
        public string STORAGENUM
        {
            get { return _storagenum; }
            set { _storagenum = value; }
        }
        /// <summary>
        /// RESID_TYPE
        /// </summary>		
        private string _resid_type;
        public string RESID_TYPE
        {
            get { return _resid_type; }
            set { _resid_type = value; }
        }
        /// <summary>
        /// OCCUPATION
        /// </summary>		
        private string _occupation;
        public string OCCUPATION
        {
            get { return _occupation; }
            set { _occupation = value; }
        }
        /// <summary>
        /// ETHNIC
        /// </summary>		
        private string _ethnic;
        public string ETHNIC
        {
            get { return _ethnic; }
            set { _ethnic = value; }
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

    }
}

