using System;
using System.Runtime.Serialization;
namespace ZhiFang.Model
{
	/// <summary>
	/// 实体类SampleType 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[DataContract]
	public class SampleType
	{
		public SampleType()
		{}
		#region Model
		private int _sampletypeno;
		private string _cname;
		private string _shortcode;
		private int?_visible;
		private int? _disporder;
		private string _hisordercode;

        private int _sampletypeid;
        private DateTime? _dtimestampe;
        private DateTime? _addtime;
        private string _standcode;
        private string _zfstandcode;
        private int? _useflag;
        /// <summary>
        /// SampleTypeID
        /// </summary>
        [DataMember] 
        public int SampleTypeID
        {
            get { return _sampletypeid; }
            set { _sampletypeid = value; }
        }

        /// <summary>
        /// SampleTypeNo
        /// </summary>
        [DataMember] 
        public int SampleTypeNo
        {
            get { return _sampletypeno; }
            set { _sampletypeno = value; }
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
        /// Visible
        /// </summary>
        [DataMember] 
        public int? Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        /// <summary>
        /// DispOrder
        /// </summary>
        [DataMember] 
        public int? DispOrder
        {
            get { return _disporder; }
            set { _disporder = value; }
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
        public string SampleTypeLikeKey { get; set; }

        /// <summary>
        /// code_1
        /// </summary>
        private string _code_1;
        [DataMember] 
        public string code_1
        {
            get { return _code_1; }
            set { _code_1 = value; }
        }

        /// <summary>
        /// code_2
        /// </summary>
        private string _code_2;
        [DataMember] 
        public string code_2
        {
            get { return _code_2; }
            set { _code_2 = value; }
        }

        /// <summary>
        /// code_3
        /// </summary>
        private string _code_3;
        [DataMember] 
        public string code_3
        {
            get { return _code_3; }
            set { _code_3 = value; }
        }

        private string _orderfield = "SampleTypeNo";//排序
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
		#endregion Model

        [DataMember]
        public SampleTypeControl SampleTypeControl { get; set; }
	}
}

