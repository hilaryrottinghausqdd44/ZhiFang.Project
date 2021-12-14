using System;
namespace ZhiFang.Model
{
	/// <summary>
	/// 实体类RFGraphData 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class RFGraphData
	{
		public RFGraphData()
		{}
		#region Model
        private DateTime _receivedate;
        private int _sectionno;
        private int _testtypeno;
        private string _sampleno;
		private string _graphname;
		private int? _graphno;
		private int? _equipno;
		private int? _pointtype;
		private int? _showpoint;
		private int? _mcolor;
		private string _scolor;
		private int? _showaxis;
		private int? _showlable;
		private decimal? _minx;
		private decimal? _maxx;
		private decimal? _miny;
		private decimal? _maxy;
		private int? _showtitle;
		private string _stitle;
		private string _graphvalue;
		private string _graphmemo;
		private string _graphf1;
		private string _graphf2;
		private int? _charttop;
		private int? _chartheight;
		private int? _chartleft;
		private int? _chartwidth;
		private byte[] _graphjpg;
        private string _formno;



        private int _graphid;
        private int? _unionkey;
        private DateTime? _dtimestampe;
        private DateTime? _addtime;
        private string _standcode;
        private string _zfstandcode;
        private int? _useflag;
        /// <summary>
        /// 
        /// </summary>
        public DateTime ReceiveDate
        {
            set { _receivedate = value; }
            get { return _receivedate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int SectionNo
        {
            set { _sectionno = value; }
            get { return _sectionno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int TestTypeNo
        {
            set { _testtypeno = value; }
            get { return _testtypeno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SampleNo
        {
            set { _sampleno = value; }
            get { return _sampleno; }
        }

		/// <summary>
		/// 
		/// </summary>
		public string GraphName
		{
			set{ _graphname=value;}
			get{return _graphname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? GraphNo
		{
			set{ _graphno=value;}
			get{return _graphno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? EquipNo
		{
			set{ _equipno=value;}
			get{return _equipno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? PointType
		{
			set{ _pointtype=value;}
			get{return _pointtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ShowPoint
		{
			set{ _showpoint=value;}
			get{return _showpoint;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? MColor
		{
			set{ _mcolor=value;}
			get{return _mcolor;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SColor
		{
			set{ _scolor=value;}
			get{return _scolor;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ShowAxis
		{
			set{ _showaxis=value;}
			get{return _showaxis;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ShowLable
		{
			set{ _showlable=value;}
			get{return _showlable;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? MinX
		{
			set{ _minx=value;}
			get{return _minx;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? MaxX
		{
			set{ _maxx=value;}
			get{return _maxx;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? MinY
		{
			set{ _miny=value;}
			get{return _miny;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? MaxY
		{
			set{ _maxy=value;}
			get{return _maxy;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ShowTitle
		{
			set{ _showtitle=value;}
			get{return _showtitle;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string STitle
		{
			set{ _stitle=value;}
			get{return _stitle;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string GraphValue
		{
			set{ _graphvalue=value;}
			get{return _graphvalue;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string GraphMemo
		{
			set{ _graphmemo=value;}
			get{return _graphmemo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string GraphF1
		{
			set{ _graphf1=value;}
			get{return _graphf1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string GraphF2
		{
			set{ _graphf2=value;}
			get{return _graphf2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ChartTop
		{
			set{ _charttop=value;}
			get{return _charttop;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ChartHeight
		{
			set{ _chartheight=value;}
			get{return _chartheight;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ChartLeft
		{
			set{ _chartleft=value;}
			get{return _chartleft;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ChartWidth
		{
			set{ _chartwidth=value;}
			get{return _chartwidth;}
		}
		/// <summary>
		/// 
		/// </summary>
		public byte[] Graphjpg
		{
			set{ _graphjpg=value;}
			get{return _graphjpg;}
		}
		/// <summary>
		/// 
		/// </summary>
        public string FormNo
		{
			set{ _formno=value;}
			get{return _formno;}
		}
        /// <summary>
        /// GraphID
        /// </summary>
        public int GraphID
        {
            get { return _graphid; }
            set { _graphid = value; }
        }

        /// <summary>
        /// UnionKey
        /// </summary>
        public int? UnionKey
        {
            get { return _unionkey; }
            set { _unionkey = value; }
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
		#endregion Model

	}
}

