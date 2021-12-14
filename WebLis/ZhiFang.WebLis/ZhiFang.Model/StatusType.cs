using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace ZhiFang.Model
{
    //StatusType		
    [Serializable]
    public class StatusType
    {
        public StatusType()
        { }
        private int _statusid;
        private int _statusno;
        private string _cname;
        private string _statusdesc;
        private string _statuscolor;
        private DateTime? _dtimestampe;
        private DateTime? _addtime;
        private string _standcode;
        private string _zfstandcode;
        private int? _useflag;

        /// <summary>
        /// StatusID
        /// </summary>
        public int StatusID
        {
            get { return _statusid; }
            set { _statusid = value; }
        }

        /// <summary>
        /// StatusNo
        /// </summary>
        public int StatusNo
        {
            get { return _statusno; }
            set { _statusno = value; }
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
        /// StatusDesc
        /// </summary>
        public string StatusDesc
        {
            get { return _statusdesc; }
            set { _statusdesc = value; }
        }

        /// <summary>
        /// StatusColor
        /// </summary>
        public string StatusColor
        {
            get { return _statuscolor; }
            set { _statuscolor = value; }
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