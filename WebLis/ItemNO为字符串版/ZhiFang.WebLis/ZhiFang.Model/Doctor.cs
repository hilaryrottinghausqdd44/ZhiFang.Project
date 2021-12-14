using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ZhiFang.Model
{
    [DataContract]
    public class Doctor
    {
        public Doctor()
        { }
        #region Model
        private int _doctorno;
        private string _cname;
        private string _shortcode;
        private string _hisordercode;
        private int? _visible;
        private string _DoctorPhoneCode;

        private int _doctorid;
        private DateTime? _dtimestampe;
        private DateTime? _addtime;
        private string _standcode;
        private string _zfstandcode;
        private int? _useflag;
        /// <summary>
        /// DoctorID
        /// </summary>
		[DataMember]
        public int DoctorID
        {
            get { return _doctorid; }
            set { _doctorid = value; }
        }

        /// <summary>
        /// DoctorNo
        /// </summary>
		[DataMember]
        public int DoctorNo
        {
            get { return _doctorno; }
            set { _doctorno = value; }
        }

        /// <summary>
        /// CName
        /// </summary>
		[DataMember]
        public string CName
        {
            get { return _cname; }
            set { _cname = value; }
        }

        /// <summary>
        /// ShortCode
        /// </summary>
		[DataMember]
        public string ShortCode
        {
            get { return _shortcode; }
            set { _shortcode = value; }
        }

        /// <summary>
        /// HisOrderCode
        /// </summary>
		[DataMember]
        public string HisOrderCode
        {
            get { return _hisordercode; }
            set { _hisordercode = value; }
        }

        /// <summary>
        /// Visible
        /// </summary>
		[DataMember]
        public int? Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        /// <summary>
        /// DTimeStampe
        /// </summary>
		[DataMember]
        public DateTime? DTimeStampe
        {
            get { return _dtimestampe; }
            set { _dtimestampe = value; }
        }

        /// <summary>
        /// AddTime
        /// </summary>
		[DataMember]
        public DateTime? AddTime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }

        /// <summary>
        /// StandCode
        /// </summary>
		[DataMember]
        public string StandCode
        {
            get { return _standcode; }
            set { _standcode = value; }
        }

        /// <summary>
        /// ZFStandCode
        /// </summary>
		[DataMember]
        public string ZFStandCode
        {
            get { return _zfstandcode; }
            set { _zfstandcode = value; }
        }

        /// <summary>
        /// UseFlag
        /// </summary>
		[DataMember]
        public int? UseFlag
        {
            get { return _useflag; }
            set { _useflag = value; }
        }

        /// <summary>
        /// 
        /// </summary>
		[DataMember]
        public string doctorPhoneCode
        {
            set { _DoctorPhoneCode = value; }
            get { return _DoctorPhoneCode; }
        }

        /// <summary>
        /// 中心--实验室  对照关系使用
        /// </summary>
        private string _labcode;
		[DataMember]
        public string LabCode
        {
            get { return _labcode; }
            set { _labcode = value; }
        }

		[DataMember]
        public string DoctorLikeKey { get; set; }

        private string _orderfield = "DoctorNo";//排序
		[DataMember]
        public string OrderField
        {
            get { return _orderfield; }
            set { _orderfield = value; }
        }

        private string _searchlikekey;//模糊查询字段
		[DataMember]
        public string SearchLikeKey
        {
            get { return _searchlikekey; }
            set { _searchlikekey = value; }
        }


        /// <summary>
        /// DoctorControl/////////
        /// </summary>
        [DataMember]
        public DoctorControl DoctorControl { get; set; }
        #endregion Model

    }
}
