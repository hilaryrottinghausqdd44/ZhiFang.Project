using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace ZhiFang.Model
{
    //Diagnosis		
    [Serializable]
    public class Diagnosis
    {
        public Diagnosis()
        { }
        private int _diagid;
        private int _diagno;
        private string _cname;
        private string _diagdesc;
        private string _shortcode;
        private int? _visible;
        private int? _disporder;
        private string _hisordercode;
        private DateTime? _dtimestampe;
        private DateTime? _addtime;
        private string _standcode;
        private string _zfstandcode;
        private int? _useflag;

        /// <summary>
        /// DiagID
        /// </summary>
        public int DiagID
        {
            get { return _diagid; }
            set { _diagid = value; }
        }

        /// <summary>
        /// DiagNo
        /// </summary>
        public int DiagNo
        {
            get { return _diagno; }
            set { _diagno = value; }
        }

        /// <summary>
        /// CName
        /// </summary>
        public string CName
        {
            get { return _cname; }
            set { _cname = value; }
        }

        /// <summary>
        /// DiagDesc
        /// </summary>
        public string DiagDesc
        {
            get { return _diagdesc; }
            set { _diagdesc = value; }
        }

        /// <summary>
        /// ShortCode
        /// </summary>
        public string ShortCode
        {
            get { return _shortcode; }
            set { _shortcode = value; }
        }

        /// <summary>
        /// Visible
        /// </summary>
        public int? Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        /// <summary>
        /// DispOrder
        /// </summary>
        public int? DispOrder
        {
            get { return _disporder; }
            set { _disporder = value; }
        }

        /// <summary>
        /// HisOrderCode
        /// </summary>
        public string HisOrderCode
        {
            get { return _hisordercode; }
            set { _hisordercode = value; }
        }

        /// <summary>
        /// DTimeStampe
        /// </summary>
        public DateTime? DTimeStampe
        {
            get { return _dtimestampe; }
            set { _dtimestampe = value; }
        }

        /// <summary>
        /// AddTime
        /// </summary>
        public DateTime? AddTime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }

        /// <summary>
        /// StandCode
        /// </summary>
        public string StandCode
        {
            get { return _standcode; }
            set { _standcode = value; }
        }

        /// <summary>
        /// ZFStandCode
        /// </summary>
        public string ZFStandCode
        {
            get { return _zfstandcode; }
            set { _zfstandcode = value; }
        }

        /// <summary>
        /// UseFlag
        /// </summary>
        public int? UseFlag
        {
            get { return _useflag; }
            set { _useflag = value; }
        }

        /// <summary>
        /// 中心--实验室  对照关系使用
        /// </summary>
        private string _labcode;
        public string LabCode
        {
            get { return _labcode; }
            set { _labcode = value; }
        }

    }
}