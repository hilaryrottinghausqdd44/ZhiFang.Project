using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using System.Runtime.Serialization;
namespace ZhiFang.Model
{
    //GenderType		
    [DataContract]
    public class GenderType
    {
        public GenderType()
        { }

        /// <summary>
        /// GenderID
        /// </summary>
        private int _genderid;
        [DataMember] 
        public int GenderID
        {
            get { return _genderid; }
            set { _genderid = value; }
        }

        /// <summary>
        /// GenderNo
        /// </summary>
        private int _genderno;
        [DataMember] 
        public int GenderNo
        {
            get { return _genderno; }
            set { _genderno = value; }
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
        /// Visible
        /// </summary>

        private int? _visible = 1;
        [DataMember] 
        public int? Visible
        {
            get { return _visible; }
            set { _visible = value; }
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

        private string _orderfield = "GenderNo";//排序
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

        [DataMember]
        public GenderTypeControl GenderTypeControl { get; set; }

    }

}