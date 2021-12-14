using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Model
{
    /// <summary>
    /// SearchReCheckLog:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class SearchReCheckLog
    {
        public SearchReCheckLog()
        { }
        #region Model
        private long _id;
        private string _batch_num;
        private string _upload_time;
        private string _local_id;
        private string _resultdesc;
        private string _resultcode;
        private string _serialnum_id;
        private string _uniqueid;
        private string _pid;
        private string _business_relation_id;
        private string _business_active_type;
        private string _business_active_des;
        private string _business_id;
        private string _basic_active_type;
        private string _basic_active_des;
        private string _basic_active_id;
        private string _organization_code;
        private string _organization_name;
        private string _domain_code;
        private string _domain_name;
        private string _ver;
        private string _verdes;
        private string _region_iden;
        private string _data_security;
        private string _record_iden;
        private string _create_date;
        private string _update_date;
        private string _datagenerate_date;
        private string _sys_code;
        private string _sys_name;
        private string _org_code;
        private string _org_name;
        private string _task_type;
        private string _person_name;
        private string _cert_type;
        private string _cert_name;
        private string _cert_number;
        private string _person_tel;
        private string _task_time;
        private string _task_desc;
        private string _doctor_id;
        private string _doctor_name;
        private string _bus_result_code;
        private string _bus_result_desc;
        private bool _uploadflag;
        private DateTime? _adddatetime = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        public long Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BATCH_NUM
        {
            set { _batch_num = value; }
            get { return _batch_num; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UPLOAD_TIME
        {
            set { _upload_time = value; }
            get { return _upload_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LOCAL_ID
        {
            set { _local_id = value; }
            get { return _local_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RESULTDESC
        {
            set { _resultdesc = value; }
            get { return _resultdesc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RESULTCODE
        {
            set { _resultcode = value; }
            get { return _resultcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SERIALNUM_ID
        {
            set { _serialnum_id = value; }
            get { return _serialnum_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UNIQUEID
        {
            set { _uniqueid = value; }
            get { return _uniqueid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PID
        {
            set { _pid = value; }
            get { return _pid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BUSINESS_RELATION_ID
        {
            set { _business_relation_id = value; }
            get { return _business_relation_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BUSINESS_ACTIVE_TYPE
        {
            set { _business_active_type = value; }
            get { return _business_active_type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BUSINESS_ACTIVE_DES
        {
            set { _business_active_des = value; }
            get { return _business_active_des; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BUSINESS_ID
        {
            set { _business_id = value; }
            get { return _business_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BASIC_ACTIVE_TYPE
        {
            set { _basic_active_type = value; }
            get { return _basic_active_type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BASIC_ACTIVE_DES
        {
            set { _basic_active_des = value; }
            get { return _basic_active_des; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BASIC_ACTIVE_ID
        {
            set { _basic_active_id = value; }
            get { return _basic_active_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ORGANIZATION_CODE
        {
            set { _organization_code = value; }
            get { return _organization_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ORGANIZATION_NAME
        {
            set { _organization_name = value; }
            get { return _organization_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DOMAIN_CODE
        {
            set { _domain_code = value; }
            get { return _domain_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DOMAIN_NAME
        {
            set { _domain_name = value; }
            get { return _domain_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string VER
        {
            set { _ver = value; }
            get { return _ver; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string VERDES
        {
            set { _verdes = value; }
            get { return _verdes; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string REGION_IDEN
        {
            set { _region_iden = value; }
            get { return _region_iden; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DATA_SECURITY
        {
            set { _data_security = value; }
            get { return _data_security; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RECORD_IDEN
        {
            set { _record_iden = value; }
            get { return _record_iden; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CREATE_DATE
        {
            set { _create_date = value; }
            get { return _create_date; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UPDATE_DATE
        {
            set { _update_date = value; }
            get { return _update_date; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DATAGENERATE_DATE
        {
            set { _datagenerate_date = value; }
            get { return _datagenerate_date; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SYS_CODE
        {
            set { _sys_code = value; }
            get { return _sys_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SYS_NAME
        {
            set { _sys_name = value; }
            get { return _sys_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ORG_CODE
        {
            set { _org_code = value; }
            get { return _org_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ORG_NAME
        {
            set { _org_name = value; }
            get { return _org_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TASK_TYPE
        {
            set { _task_type = value; }
            get { return _task_type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PERSON_NAME
        {
            set { _person_name = value; }
            get { return _person_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CERT_TYPE
        {
            set { _cert_type = value; }
            get { return _cert_type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CERT_NAME
        {
            set { _cert_name = value; }
            get { return _cert_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CERT_NUMBER
        {
            set { _cert_number = value; }
            get { return _cert_number; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PERSON_TEL
        {
            set { _person_tel = value; }
            get { return _person_tel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TASK_TIME
        {
            set { _task_time = value; }
            get { return _task_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TASK_DESC
        {
            set { _task_desc = value; }
            get { return _task_desc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DOCTOR_ID
        {
            set { _doctor_id = value; }
            get { return _doctor_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DOCTOR_NAME
        {
            set { _doctor_name = value; }
            get { return _doctor_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BUS_RESULT_CODE
        {
            set { _bus_result_code = value; }
            get { return _bus_result_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BUS_RESULT_DESC
        {
            set { _bus_result_desc = value; }
            get { return _bus_result_desc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool UpLoadFlag
        {
            set { _uploadflag = value; }
            get { return _uploadflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? AddDateTime
        {
            set { _adddatetime = value; }
            get { return _adddatetime; }
        }
        #endregion Model

    }
}
