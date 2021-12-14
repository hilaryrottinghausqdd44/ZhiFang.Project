
using System;

namespace ZhiFang.Entity.WebAssist.GKBarcode
{
	/// <summary>
	/// GKBarRed:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class GKBarRed
	{
		public GKBarRed()
		{}
		#region Model
		private string _sel;
		private DateTime? _recdate;
		private DateTime? _rectime;
		private int? _depid;
		private int? _testtpyeid;
		private int? _collecterid;
		private string _information1;
		private string _information2;
		private string _information3;
		private string _information4;
		private string _barcode;
		private string _printinfor;
		private string _recievedinfor;
		private string _apply;
		private string _applydate;
		private string _applytime;
		private DateTime? _collectdate;
		private DateTime? _collecttime;
		private int? _chroniclerid;
		private string _testresult;
		private string _judge;
		private string _judge_operator;
		private DateTime? _judge_date;
		private string _technician;
		private DateTime? _testdate;
		private string _fee;
		private string _archived;
		private string _monitortype;
		private int _serialno;

		private bool _isSync;

		/// <summary>
		/// 是否已经同步导入新系统
		/// </summary>
		public bool IsSync
		{
			set { _isSync = value; }
			get { return _isSync; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string Sel
		{
			set{ _sel=value;}
			get{return _sel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? RecDate
		{
			set{ _recdate=value;}
			get{return _recdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? RecTime
		{
			set{ _rectime=value;}
			get{return _rectime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? DepID
		{
			set{ _depid=value;}
			get{return _depid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? TestTpyeID
		{
			set{ _testtpyeid=value;}
			get{return _testtpyeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CollecterID
		{
			set{ _collecterid=value;}
			get{return _collecterid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Information1
		{
			set{ _information1=value;}
			get{return _information1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Information2
		{
			set{ _information2=value;}
			get{return _information2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Information3
		{
			set{ _information3=value;}
			get{return _information3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Information4
		{
			set{ _information4=value;}
			get{return _information4;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BarCode
		{
			set{ _barcode=value;}
			get{return _barcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PrintInfor
		{
			set{ _printinfor=value;}
			get{return _printinfor;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RecievedInfor
		{
			set{ _recievedinfor=value;}
			get{return _recievedinfor;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Apply
		{
			set{ _apply=value;}
			get{return _apply;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ApplyDate
		{
			set{ _applydate=value;}
			get{return _applydate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ApplyTime
		{
			set{ _applytime=value;}
			get{return _applytime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CollectDate
		{
			set{ _collectdate=value;}
			get{return _collectdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CollectTime
		{
			set{ _collecttime=value;}
			get{return _collecttime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ChroniclerID
		{
			set{ _chroniclerid=value;}
			get{return _chroniclerid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TestResult
		{
			set{ _testresult=value;}
			get{return _testresult;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Judge
		{
			set{ _judge=value;}
			get{return _judge;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Judge_Operator
		{
			set{ _judge_operator=value;}
			get{return _judge_operator;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Judge_Date
		{
			set{ _judge_date=value;}
			get{return _judge_date;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Technician
		{
			set{ _technician=value;}
			get{return _technician;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? TestDate
		{
			set{ _testdate=value;}
			get{return _testdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Fee
		{
			set{ _fee=value;}
			get{return _fee;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Archived
		{
			set{ _archived=value;}
			get{return _archived;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MonitorType
		{
			set{ _monitortype=value;}
			get{return _monitortype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int SerialNo
		{
			set{ _serialno=value;}
			get{return _serialno;}
		}
		#endregion Model

	}
}

