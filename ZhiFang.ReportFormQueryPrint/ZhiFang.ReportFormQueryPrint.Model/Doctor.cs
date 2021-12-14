using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.ReportFormQueryPrint.Model
{
    [Serializable]
    public class Doctor
    {
        public Doctor()
        { }
        #region Model
        private long _doctorno;
        private string _cname;
        private string _shortcode;
        private string _hisordercode;
        private int? _visible;
        private string _DoctorPhoneCode;
        /// <summary>
        /// 
        /// </summary>
        public long DoctorNo
        {
            set { _doctorno = value; }
            get { return _doctorno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CName
        {
            set { _cname = value; }
            get { return _cname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShortCode
        {
            set { _shortcode = value; }
            get { return _shortcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HisOrderCode
        {
            set { _hisordercode = value; }
            get { return _hisordercode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Visible
        {
            set { _visible = value; }
            get { return _visible; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string doctorPhoneCode
        {
            set { _DoctorPhoneCode = value; }
            get { return _DoctorPhoneCode; }
        }
        #endregion Model

    }
}
