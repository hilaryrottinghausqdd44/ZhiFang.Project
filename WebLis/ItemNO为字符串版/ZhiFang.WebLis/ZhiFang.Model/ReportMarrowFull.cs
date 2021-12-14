using System;
namespace ZhiFang.Model
{
	/// <summary>
	/// ReportMarrowFull:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ReportMarrowFull
	{
		public ReportMarrowFull()
		{}
		#region Model
		private string _reportformid;
		private int? _reportitemid;
		private int? _paritemno;
		private int? _itemno;
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
		private int? _formno;
		/// <summary>
		/// 
		/// </summary>
		public string ReportFormID
		{
			set{ _reportformid=value;}
			get{return _reportformid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ReportItemID
		{
			set{ _reportitemid=value;}
			get{return _reportitemid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ParItemNo
		{
			set{ _paritemno=value;}
			get{return _paritemno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ItemNo
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
		public int? FormNo
		{
			set{ _formno=value;}
			get{return _formno;}
		}
		#endregion Model

	}
}

