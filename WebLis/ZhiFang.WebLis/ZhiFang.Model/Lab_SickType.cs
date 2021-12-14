using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using System.Runtime.Serialization;
namespace ZhiFang.Model
{
    //Lab_SickType		
    [DataContract]
    public class Lab_SickType
    {
        public Lab_SickType()
        { }

        /// <summary>
        /// SickTypeID
        /// </summary>
        private int _sicktypeid;
        [DataMember] 
        public int SickTypeID
        {
            get { return _sicktypeid; }
            set { _sicktypeid = value; }
        }
        //是否对照
        private string _controlstatus;
        [DataMember]
        public string ControlStatus
        {
            get { return _controlstatus; }
            set { _controlstatus = value; }
        }
        /// <summary>
        /// LabCode
        /// </summary>
        private string _labcode;
        [DataMember] 
        public string LabCode
        {
            get { return _labcode; }
            set { _labcode = value; }
        }

        /// <summary>
        /// LabSickTypeNo
        /// </summary>
        private int _labsicktypeno;
        [DataMember] 
        public int LabSickTypeNo
        {
            get { return _labsicktypeno; }
            set { _labsicktypeno = value; }
        }

        /// <summary>
        /// CName
        /// </summary>
        private string _cname;
        [DataMember] 
        public string CName
        {
            get { return _cname; }
            set { _cname = value; }
        }

        /// <summary>
        /// ShortCode
        /// </summary>
        private string _shortcode;
        [DataMember] 
        public string ShortCode
        {
            get { return _shortcode; }
            set { _shortcode = value; }
        }

        /// <summary>
        /// DispOrder
        /// </summary>

        private int? _disporder;
        [DataMember] 
        public int? DispOrder
        {
            get { return _disporder; }
            set { _disporder = value; }
        }

        /// <summary>
        /// HisOrderCode
        /// </summary>
        private string _hisordercode;
        [DataMember] 
        public string HisOrderCode
        {
            get { return _hisordercode; }
            set { _hisordercode = value; }
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
        /// StandCode
        /// </summary>
        private string _standcode;
        [DataMember] 
        public string StandCode
        {
            get { return _standcode; }
            set { _standcode = value; }
        }

        /// <summary>
        /// ZFStandCode
        /// </summary>
        private string _zfstandcode;
        [DataMember] 
        public string ZFStandCode
        {
            get { return _zfstandcode; }
            set { _zfstandcode = value; }
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

        private string _orderfield = "SickTypeID";//排序
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


    }
}