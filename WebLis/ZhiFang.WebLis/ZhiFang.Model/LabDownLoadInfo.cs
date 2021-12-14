using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace ZhiFang.Model
{
    //LabDownLoadInfo		
    [Serializable]
    public class LabDownLoadInfo
    {
        public LabDownLoadInfo()
        { }

        /// <summary>
        /// Id
        /// </summary>
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// TableName
        /// </summary>
        private string _tablename;
        public string TableName
        {
            get { return _tablename; }
            set { _tablename = value; }
        }

        /// <summary>
        /// ServerTime
        /// </summary>

        private int? _servertime;
        public int? ServerTime
        {
            get { return _servertime; }
            set { _servertime = value; }
        }

        /// <summary>
        /// LocalTime
        /// </summary>

        private DateTime? _localtime;
        public DateTime? LocalTime
        {
            get { return _localtime; }
            set { _localtime = value; }
        }

        /// <summary>
        /// Status
        /// </summary>

        private int? _status;
        public int? Status
        {
            get { return _status; }
            set { _status = value; }
        }

        /// <summary>
        /// DownLoadCount
        /// </summary>

        private int? _downloadcount;
        public int? DownLoadCount
        {
            get { return _downloadcount; }
            set { _downloadcount = value; }
        }

        /// <summary>
        /// MsgRemark
        /// </summary>
        private string _msgremark;
        public string MsgRemark
        {
            get { return _msgremark; }
            set { _msgremark = value; }
        }

        /// <summary>
        /// DTimeStampe
        /// </summary>

        private DateTime? _dtimestampe;
        public DateTime? DTimeStampe
        {
            get { return _dtimestampe; }
            set { _dtimestampe = value; }
        }

        /// <summary>
        /// AddTime
        /// </summary>

        private DateTime? _addtime = DateTime.Now;
        public DateTime? AddTime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }

        /// <summary>
        /// UseFlag
        /// </summary>

        private int? _useflag = 1;
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