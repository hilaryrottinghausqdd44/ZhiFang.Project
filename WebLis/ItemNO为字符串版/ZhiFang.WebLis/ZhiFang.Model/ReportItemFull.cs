using System;
namespace ZhiFang.Model
{
	/// <summary>
	/// ReportItemFull:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ReportItemFull
	{
		public ReportItemFull()
		{}
		#region Model
		private string _reportformid;
		private int? _reportitemid;
		private string _testitemname;
		private string _testitemsname;
		private DateTime? _receivedate;
		private string _sectionno;
		private string _testtypeno;
		private string _sampleno;
		private string _paritemno;
		private string _itemno;
		private string _originalvalue;
		private string _reportvalue;
		private string _originaldesc;
		private string _reportdesc;
		private string _statusno;
		private string _equipno;
		private string _modified;
		private string _refrange;
		private DateTime? _itemdate;
		private DateTime? _itemtime;
		private string _ismatch;
		private string _resultstatus;
		private DateTime? _testitemdatetime;
		private string _reportvalueall;
		private string _paritemname;
		private string _paritemsname;
		private string _disporder;
		private string _itemorder;
		private string _unit;
		private string _serialno;
		private string _zdy1;
		private string _zdy2;
		private string _zdy3;
		private string _zdy4;
		private string _zdy5;
		private string _hisorderno;
		private int? _formno;
		/// <summary>
		/// 申请序号
		/// </summary>
		public string ReportFormID
		{
			set{ _reportformid=value;}
			get{return _reportformid;}
		}
		/// <summary>
		/// 项目结果序号
		/// </summary>
		public int? ReportItemID
		{
			set{ _reportitemid=value;}
			get{return _reportitemid;}
		}
		/// <summary>
		/// 项目名称
		/// </summary>
		public string TESTITEMNAME
		{
			set{ _testitemname=value;}
			get{return _testitemname;}
		}
		/// <summary>
		/// 项目缩写
		/// </summary>
		public string TESTITEMSNAME
		{
			set{ _testitemsname=value;}
			get{return _testitemsname;}
		}
		/// <summary>
		/// 核收日期
		/// </summary>
		public DateTime? RECEIVEDATE
		{
			set{ _receivedate=value;}
			get{return _receivedate;}
		}
		/// <summary>
		/// 小组编号
		/// </summary>
		public string SECTIONNO
		{
			set{ _sectionno=value;}
			get{return _sectionno;}
		}
		/// <summary>
		/// 检测类型编号
		/// </summary>
		public string TESTTYPENO
		{
			set{ _testtypeno=value;}
			get{return _testtypeno;}
		}
		/// <summary>
		/// 样本号
		/// </summary>
		public string SAMPLENO
		{
			set{ _sampleno=value;}
			get{return _sampleno;}
		}
		/// <summary>
		/// 申请项目编号
		/// </summary>
		public string PARITEMNO
		{
			set{ _paritemno=value;}
			get{return _paritemno;}
		}
		/// <summary>
		/// 项目编号
		/// </summary>
		public string ITEMNO
		{
			set{ _itemno=value;}
			get{return _itemno;}
		}
		/// <summary>
		/// 原始结果
		/// </summary>
		public string ORIGINALVALUE
		{
			set{ _originalvalue=value;}
			get{return _originalvalue;}
		}
		/// <summary>
		/// 报告结果
		/// </summary>
		public string REPORTVALUE
		{
			set{ _reportvalue=value;}
			get{return _reportvalue;}
		}
		/// <summary>
		/// 描述
		/// </summary>
		public string ORIGINALDESC
		{
			set{ _originaldesc=value;}
			get{return _originaldesc;}
		}
		/// <summary>
		/// 报告描述
		/// </summary>
		public string REPORTDESC
		{
			set{ _reportdesc=value;}
			get{return _reportdesc;}
		}
		/// <summary>
		/// 状态
		/// </summary>
		public string STATUSNO
		{
			set{ _statusno=value;}
			get{return _statusno;}
		}
		/// <summary>
		/// 仪器号
		/// </summary>
		public string EQUIPNO
		{
			set{ _equipno=value;}
			get{return _equipno;}
		}
		/// <summary>
		/// 是否修改
		/// </summary>
		public string MODIFIED
		{
			set{ _modified=value;}
			get{return _modified;}
		}
		/// <summary>
		/// 参考值范围
		/// </summary>
		public string REFRANGE
		{
			set{ _refrange=value;}
			get{return _refrange;}
		}
		/// <summary>
		/// 测定日期
		/// </summary>
		public DateTime? ITEMDATE
		{
			set{ _itemdate=value;}
			get{return _itemdate;}
		}
		/// <summary>
		/// 测定时间
		/// </summary>
		public DateTime? ITEMTIME
		{
			set{ _itemtime=value;}
			get{return _itemtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ISMATCH
		{
			set{ _ismatch=value;}
			get{return _ismatch;}
		}
		/// <summary>
		/// 结果描述
		/// </summary>
		public string RESULTSTATUS
		{
			set{ _resultstatus=value;}
			get{return _resultstatus;}
		}
		/// <summary>
		/// 检测日期
		/// </summary>
		public DateTime? TESTITEMDATETIME
		{
			set{ _testitemdatetime=value;}
			get{return _testitemdatetime;}
		}
		/// <summary>
		/// 结果
		/// </summary>
		public string REPORTVALUEALL
		{
			set{ _reportvalueall=value;}
			get{return _reportvalueall;}
		}
		/// <summary>
		/// 申请项目
		/// </summary>
		public string PARITEMNAME
		{
			set{ _paritemname=value;}
			get{return _paritemname;}
		}
		/// <summary>
		/// 申请项目缩写
		/// </summary>
		public string PARITEMSNAME
		{
			set{ _paritemsname=value;}
			get{return _paritemsname;}
		}
		/// <summary>
		/// 排序
		/// </summary>
		public string DISPORDER
		{
			set{ _disporder=value;}
			get{return _disporder;}
		}
		/// <summary>
		/// 项目序号
		/// </summary>
		public string ITEMORDER
		{
			set{ _itemorder=value;}
			get{return _itemorder;}
		}
		/// <summary>
		/// 单位
		/// </summary>
		public string UNIT
		{
			set{ _unit=value;}
			get{return _unit;}
		}
		/// <summary>
		/// 申请单号
		/// </summary>
		public string SERIALNO
		{
			set{ _serialno=value;}
			get{return _serialno;}
		}
		/// <summary>
		/// 自定义1
		/// </summary>
		public string ZDY1
		{
			set{ _zdy1=value;}
			get{return _zdy1;}
		}
		/// <summary>
		/// 自定义2
		/// </summary>
		public string ZDY2
		{
			set{ _zdy2=value;}
			get{return _zdy2;}
		}
		/// <summary>
		/// 自定义3
		/// </summary>
		public string ZDY3
		{
			set{ _zdy3=value;}
			get{return _zdy3;}
		}
		/// <summary>
		/// 自定义4
		/// </summary>
		public string ZDY4
		{
			set{ _zdy4=value;}
			get{return _zdy4;}
		}
		/// <summary>
		/// 自定义5
		/// </summary>
		public string ZDY5
		{
			set{ _zdy5=value;}
			get{return _zdy5;}
		}
        public string ZDY10 { get; set; }
        public int? PRINTTIMES { get; set; }
       
		/// <summary>
		/// HIS项目编码
		/// </summary>
		public string HISORDERNO
		{
			set{ _hisorderno=value;}
			get{return _hisorderno;}
		}
		/// <summary>
		/// 申请单临时编号
		/// </summary>
		public int? FORMNO
		{
			set{ _formno=value;}
			get{return _formno;}
		}
		#endregion Model

	}
}

