using System;
namespace ZhiFang.ReportFormQueryPrint.Model
{
	/// <summary>
	/// 实体类PGroup 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class PGroup
	{
		public PGroup()
		{}
		#region Model
		private long _sectionno;
		private int? _supergroupno;
		private string _cname;
		private string _shortname;
		private string _shortcode;
		private string _sectiondesc;
		private int? _sectiontype;
		private int _visible;
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
        private string _clientNo;
        private string _filepath;
		/// <summary>
		/// 
		/// </summary>


        public string ClientNo
        {
            get { return _clientNo; }
            set { _clientNo = value; }
        }
        public long SectionNo
		{
			set{ _sectionno=value;}
			get{return _sectionno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? SuperGroupNo
		{
			set{ _supergroupno=value;}
			get{return _supergroupno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CName
		{
			set{ _cname=value;}
			get{return _cname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ShortName
		{
			set{ _shortname=value;}
			get{return _shortname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ShortCode
		{
			set{ _shortcode=value;}
			get{return _shortcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SectionDesc
		{
			set{ _sectiondesc=value;}
			get{return _sectiondesc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? SectionType
		{
			set{ _sectiontype=value;}
			get{return _sectiontype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Visible
		{
			set{ _visible=value;}
			get{return _visible;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? DispOrder
		{
			set{ _disporder=value;}
			get{return _disporder;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? onlinetime
		{
			set{ _onlinetime=value;}
			get{return _onlinetime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? KeyDispOrder
		{
			set{ _keydisporder=value;}
			get{return _keydisporder;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? dummygroup
		{
			set{ _dummygroup=value;}
			get{return _dummygroup;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? uniontype
		{
			set{ _uniontype=value;}
			get{return _uniontype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? SectorTypeNo
		{
			set{ _sectortypeno=value;}
			get{return _sectortypeno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? SampleRule
		{
			set{ _samplerule=value;}
			get{return _samplerule;}
		}
        /// <summary>
        /// 
        /// </summary>
        public int? SampleNoType
        {
            set { _samplenotype = value; }
            get { return _samplenotype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsSendGroup
        {
            set { _issendgroup = value; }
            get { return _issendgroup; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ReportSection
        {
            set { _reportsection = value; }
            get { return _reportsection; }
        }
        
		#endregion Model

	}
}

