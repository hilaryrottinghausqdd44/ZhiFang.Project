using System;
namespace ZhiFang.Model
{
	/// <summary>
	/// 实体类ReportMicro 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class ReportMicro
	{
		public ReportMicro()
		{}
		#region Model
		private int _resultno;
		private int _itemno;
		private int? _descno;
		private int? _microno;
		private string _microdesc;
		private int? _antino;
		private string _suscept;
		private decimal? _susquan;
		private string _susdesc;
		private string _refrange;
		private DateTime? _itemdate;
		private DateTime? _itemtime;
		private string _itemdesc;
		private int? _equipno;
		private int? _modified;
		private int? _ismatch;
		private int? _checktype;
		private int? _isreceive;
        private string _formno;
		private string _microcountdesc;
		private int? _mresulttype;
        private DateTime? _receivedate;
        private int _sectionno;
        private int _testtypeno;
        private string _sampleno;
		/// <summary>
		/// 
		/// </summary>
		public int ResultNo
		{
			set{ _resultno=value;}
			get{return _resultno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int ItemNo
		{
			set{ _itemno=value;}
			get{return _itemno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? DescNo
		{
			set{ _descno=value;}
			get{return _descno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? MicroNo
		{
			set{ _microno=value;}
			get{return _microno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MicroDesc
		{
			set{ _microdesc=value;}
			get{return _microdesc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? AntiNo
		{
			set{ _antino=value;}
			get{return _antino;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Suscept
		{
			set{ _suscept=value;}
			get{return _suscept;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? SusQuan
		{
			set{ _susquan=value;}
			get{return _susquan;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SusDesc
		{
			set{ _susdesc=value;}
			get{return _susdesc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RefRange
		{
			set{ _refrange=value;}
			get{return _refrange;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ItemDate
		{
			set{ _itemdate=value;}
			get{return _itemdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ItemTime
		{
			set{ _itemtime=value;}
			get{return _itemtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ItemDesc
		{
			set{ _itemdesc=value;}
			get{return _itemdesc;}
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
		public int? Modified
		{
			set{ _modified=value;}
			get{return _modified;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsMatch
		{
			set{ _ismatch=value;}
			get{return _ismatch;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CheckType
		{
			set{ _checktype=value;}
			get{return _checktype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? isReceive
		{
			set{ _isreceive=value;}
			get{return _isreceive;}
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
		/// 
		/// </summary>
		public string microcountdesc
		{
			set{ _microcountdesc=value;}
			get{return _microcountdesc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? mresulttype
		{
			set{ _mresulttype=value;}
			get{return _mresulttype;}
		}
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ReceiveDate
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
		#endregion Model

	}
}

