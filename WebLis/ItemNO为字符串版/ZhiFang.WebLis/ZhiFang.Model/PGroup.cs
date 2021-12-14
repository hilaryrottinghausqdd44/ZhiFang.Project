using System;
using System.Runtime.Serialization;
namespace ZhiFang.Model
{
	/// <summary>
	/// 实体类PGroup 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[DataContract]
	public class PGroup
	{
		public PGroup()
		{}
		#region Model
		private int? _sectionno;
		private int? _supergroupno;
		private string _cname;
		private string _shortname;
		private string _shortcode;
		private string _sectiondesc;
		private int? _sectiontype;
		private int? _visible;
		private int? _disporder;
		private int? _onlinetime;
		private int? _keydisporder;
		private int? _dummygroup;
		private int? _uniontype;
		private int? _sectortypeno;
		private int? _samplerule;
        private int? _samplenotype;
        private int? _issendgroup;
        private int? _reportsection;

        private int _sectionid; 
        private DateTime? _dtimestampe;
        private DateTime? _addtime;
        private string _standcode;
        private string _zfstandcode;
        private int? _useflag;
        /// <summary>
        /// SectionID
        /// </summary>
        [DataMember] 
        public int SectionID
        {
            get { return _sectionid; }
            set { _sectionid = value; }
        }

		private string _orderfield = "SectionNo";//排序
		[DataMember] 
        public string OrderField
		{
			get { return _orderfield; }
			set { _orderfield = value; }
		}
        /// <summary>
        /// SectionNo
        /// </summary>
        [DataMember] 
        public int? SectionNo
        {
            get { return _sectionno; }
            set { _sectionno = value; }
        }

        /// <summary>
        /// SuperGroupNo
        /// </summary>
        [DataMember] 
        public int? SuperGroupNo
        {
            get { return _supergroupno; }
            set { _supergroupno = value; }
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
        /// ShortName
        /// </summary>
        [DataMember] 
        public string ShortName
        {
            get { return _shortname; }
            set { _shortname = value; }
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
        /// SectionDesc
        /// </summary>
        [DataMember] 
        public string SectionDesc
        {
            get { return _sectiondesc; }
            set { _sectiondesc = value; }
        }

        /// <summary>
        /// SectionType
        /// </summary>
        [DataMember] 
        public int? SectionType
        {
            get { return _sectiontype; }
            set { _sectiontype = value; }
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
        /// OnlineTime
        /// </summary>
        [DataMember] 
        public int? OnlineTime
        {
            get { return _onlinetime; }
            set { _onlinetime = value; }
        }

        /// <summary>
        /// KeyDispOrder
        /// </summary>
        [DataMember] 
        public int? KeyDispOrder
        {
            get { return _keydisporder; }
            set { _keydisporder = value; }
        }

        /// <summary>
        /// DummyGroup
        /// </summary>
        [DataMember] 
        public int? DummyGroup
        {
            get { return _dummygroup; }
            set { _dummygroup = value; }
        }

        /// <summary>
        /// UnionType
        /// </summary>
        [DataMember] 
        public int? UnionType
        {
            get { return _uniontype; }
            set { _uniontype = value; }
        }

        /// <summary>
        /// SectorTypeNo
        /// </summary>
        [DataMember] 
        public int? SectorTypeNo
        {
            get { return _sectortypeno; }
            set { _sectortypeno = value; }
        }

        /// <summary>
        /// SampleRule
        /// </summary>
        [DataMember] 
        public int? SampleRule
        {
            get { return _samplerule; }
            set { _samplerule = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember] 
        public int? SampleNoType
        {
            set { _samplenotype = value; }
            get { return _samplenotype; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember] 
        public int? IsSendGroup
        {
            set { _issendgroup = value; }
            get { return _issendgroup; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember] 
        public int? ReportSection
        {
            set { _reportsection = value; }
            get { return _reportsection; }
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



		private string _searchlikekey;//模糊查询字段
		[DataMember] 
        public string SearchLikeKey
		{
			get { return _searchlikekey; }
			set { _searchlikekey = value; }
		}



        /// <summary>
        /// PGroupControl/////////
        /// </summary>
        [DataMember]
        public PGroupControl PGroupControl { get; set; }
		#endregion Model

	}
}

