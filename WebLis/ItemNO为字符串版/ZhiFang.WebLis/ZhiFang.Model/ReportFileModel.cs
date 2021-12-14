using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Model
{
    //ReportFileInfo
    public class ReportFileInfo
    {
        /// <summary>
        /// UniqueID
        /// </summary>		
        private long? _uniqueid;
        public long? UniqueID
        {
            get { return _uniqueid; }
            set { _uniqueid = value; }
        }  
        /// <summary>
        /// ID
        /// </summary>		
        private long? g_id;
        public long? G_ID
        {
            get { return g_id; }
            set { g_id = value; }
        }        
        /// <summary>
        /// PAT_ID
        /// </summary>		
        private string _pat_id;
        public string PAT_ID
        {
            get { return _pat_id; }
            set { _pat_id = value; }
        }

        /// <summary>
        /// Card_ID
        /// </summary>
        private string _card_id;
        public string Card_ID
        {
            get { return _card_id; }
            set { _card_id = value; }
        }
        /// <summary>
        /// Clinictype
        /// </summary>
        private string _clinictype;

        public string ClinicType
        {
            get { return _clinictype; }
            set { _clinictype = value; }
        }
        /// <summary>
        /// Name
        /// </summary>		
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Age
        /// </summary>
        private string _age;

        public string Age
        {
            get { return _age; }
            set { _age = value; }
        }

        /// <summary>
        /// Sex
        /// </summary>		
        private string _sex;
        public string Sex
        {
            get { return _sex; }
            set { _sex = value; }
        }
        /// <summary>
        /// MobilePhone
        /// </summary>		
        private string _mobilephone;
        public string MobilePhone
        {
            get { return _mobilephone; }
            set { _mobilephone = value; }
        }
        /// <summary>
        /// Report_Time
        /// </summary>		
        private string _report_time;
        public string Report_Time
        {
            get { return _report_time; }
            set { _report_time = value; }
        }
         //<summary>
         //Medical_Institution_ID
         //</summary>		
        private string _medical_institution_id;
        public string Medical_Institution_ID
        {
            get { return _medical_institution_id; }
            set { _medical_institution_id = value; }
        }
        /// <summary>
        /// Medical_Institution_Code
        /// </summary>		
        private string _medical_institution_code;
        public string Medical_Institution_Code
        {
            get { return _medical_institution_code; }
            set { _medical_institution_code = value; }
        }
        /// <summary>
        /// ProjectCode
        /// </summary>		
        private string _projectcode;
        public string ProjectCode
        {
            get { return _projectcode; }
            set { _projectcode = value; }
        }
        
        /// <summary>
        /// PageNo
        /// </summary>		
        private string _pageno;
        public string PageNo
        {
            get { return _pageno; }
            set { _pageno = value; }
        }

        /// <summary>
        /// File_Name
        /// </summary>		
        private string _file_name;
        public string File_Name
        {
            get { return _file_name; }
            set { _file_name = value; }
        }
        /// <summary>
        /// File_Url
        /// </summary>		
        private string _file_url;
        public string File_Url
        {
            get { return _file_url; }
            set { _file_url = value; }
        }
        /// <summary>
        /// DataTimeStamp
        /// </summary>		
        private DateTime? _datatimestamp;
        public DateTime? DataTimeStamp
        {
            get { return _datatimestamp; }
            set { _datatimestamp = value; }
        }

        /// <summary>
        /// AddDataTime
        /// </summary>		
        private DateTime _adddatatime;
        public DateTime AddDataTime
        {
            get { return _adddatatime; }
            set { _adddatatime = value; }
        }
        /// <summary>
        /// OperaTion
        /// </summary>		
        private string _operation;
        public string OperaTion
        {
            get { return _operation; }
            set { _operation = value; }
        }
        /// <summary>
        /// ChangeStatus
        /// </summary>		
        private int? _changestatus;
        public int? ChangeStatus
        {
            get { return _changestatus; }
            set { _changestatus = value; }
        }
    }
}