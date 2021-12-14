using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
namespace ZhiFang.Model
{
    //SampleTypeControl		
    [DataContract]
    public class SampleTypeControl
    {
        public SampleTypeControl()
        { }

        /// <summary>
        /// Id
        /// </summary>
        private int _id;
        [DataMember]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// SampleTypeControlNo
        /// </summary>
        private string _sampletypecontrolno;
        [DataMember]
        public string SampleTypeControlNo
        {
            get { return _sampletypecontrolno; }
            set { _sampletypecontrolno = value; }
        }

        /// <summary>
        /// SampleTypeNo
        /// </summary>
        private int? _sampletypeno;
        [DataMember]
        public int? SampleTypeNo
        {
            get { return _sampletypeno; }
            set { _sampletypeno = value; }
        }

        /// <summary>
        /// ControlLabNo
        /// </summary>
        private string _controllabno;
        [DataMember]
        public string ControlLabNo
        {
            get { return _controllabno; }
            set { _controllabno = value; }
        }

        /// <summary>
        /// ControlSampleTypeNo
        /// </summary>
        private string _controlsampletypeno;
        [DataMember]
        public string ControlSampleTypeNo
        {
            get { return _controlsampletypeno; }
            set { _controlsampletypeno = value; }
        }

        /// <summary>
        /// DTimeStampe
        /// </summary>
        private DateTime? _dtimestampe;
        [DataMember]
        public DateTime? DTimeStampe
        {
            get { return _dtimestampe; }
            set { _dtimestampe = value; }
        }

        /// <summary>
        /// AddTime
        /// </summary>
        private DateTime? _addtime = DateTime.Now;
        [DataMember]
        public DateTime? AddTime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }

        /// <summary>
        /// UseFlag
        /// </summary>
        private int? _useflag = 1;
        [DataMember]
        public int? UseFlag
        {
            get { return _useflag; }
            set { _useflag = value; }
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
        /// <summary>
        /// 对照状态
        /// </summary>
        private string _controlstate;
        [DataMember]
        public string ControlState
        {
            get { return _controlstate; }
            set { _controlstate = value; }
        }
        /// <summary>
        /// 模糊查询
        /// </summary>
        private string _searchlikekey;
        [DataMember]
        public string SearchLikeKey
        {
            get { return _searchlikekey; }
            set { _searchlikekey = value; }
        }

        #region
        private string _centercname;
        [DataMember]
        public string CenterCName
        {
            get { return _centercname; }
            set { _centercname = value; }
        }
        private string _labsampletypeno;
        [DataMember]
        public string LabSampleTypeNo
        {
            get { return _labsampletypeno; }
            set { _labsampletypeno = value; }
        }

        private string _cname;
        [DataMember]
        public string CName
        {
            get { return _cname; }
            set { _cname = value; }
        }
        private string _shortcode;
        [DataMember]
        public string ShortCode
        {
            get { return _shortcode; }
            set { _shortcode = value; }
        }

        #endregion
    }
}