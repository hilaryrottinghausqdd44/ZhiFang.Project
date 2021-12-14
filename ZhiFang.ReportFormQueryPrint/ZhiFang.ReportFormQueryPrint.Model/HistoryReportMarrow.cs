using System;
namespace ZhiFang.ReportFormQueryPrint.Model
{
    /// <summary>
    /// 实体类HistoryReportMarrow 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
	public class HistoryReportMarrow
    {
		public HistoryReportMarrow()
		{}
		#region Model
        private long _ReportPublicationID;
        private DateTime? _receivedate;
        private int? _sectionno;
        private int? _testtypeno;
        private string _sampleno;
		private int _paritemno;
		private int _itemno;
		private int? _bloodnum;
		private decimal? _bloodpercent;
		private int? _marrownum;
		private decimal? _marrowpercent;
		private string _blooddesc;
		private string _marrowdesc;
		private int? _statusno;
		private string _refrange;
		private int? _equipno;
		private int? _iscale;
		private int? _modified;
		private DateTime? _itemdate;
		private DateTime? _itemtime;
		private int? _ismatch;
		private string _resultstatus;
        private string _formno;
		private string _itemname;
        private DateTime? _DataAddTime;
        private DateTime? _DataUpdateTime;
        private DateTime? _DataMigrationTime;

        /// <summary>
        /// 报告发布ID
        /// </summary>
        public long ReportPublicationID
        {
            set { _ReportPublicationID = value; }
            get { return _ReportPublicationID; }
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
        public int? SectionNo
        {
            set { _sectionno = value; }
            get { return _sectionno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? TestTypeNo
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
		public int ParItemNo
		{
			set{ _paritemno=value;}
			get{return _paritemno;}
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
		public int? BloodNum
		{
			set{ _bloodnum=value;}
			get{return _bloodnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? BloodPercent
		{
			set{ _bloodpercent=value;}
			get{return _bloodpercent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? MarrowNum
		{
			set{ _marrownum=value;}
			get{return _marrownum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? MarrowPercent
		{
			set{ _marrowpercent=value;}
			get{return _marrowpercent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BloodDesc
		{
			set{ _blooddesc=value;}
			get{return _blooddesc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MarrowDesc
		{
			set{ _marrowdesc=value;}
			get{return _marrowdesc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? StatusNo
		{
			set{ _statusno=value;}
			get{return _statusno;}
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
		public int? EquipNo
		{
			set{ _equipno=value;}
			get{return _equipno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsCale
		{
			set{ _iscale=value;}
			get{return _iscale;}
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
		public int? IsMatch
		{
			set{ _ismatch=value;}
			get{return _ismatch;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ResultStatus
		{
			set{ _resultstatus=value;}
			get{return _resultstatus;}
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
		public string ItemName
		{
			set{ _itemname=value;}
			get{return _itemname;}
		}
        public DateTime? DataAddTime
        {
            set { _DataAddTime = value; }
            get { return _DataAddTime; }
        }
        public DateTime? DataUpdateTime
        {
            set { _DataUpdateTime = value; }
            get { return _DataUpdateTime; }
        }
        public DateTime? DataMigrationTime
        {
            set { _DataMigrationTime = value; }
            get { return _DataMigrationTime; }
        }
		#endregion Model

	}
}

