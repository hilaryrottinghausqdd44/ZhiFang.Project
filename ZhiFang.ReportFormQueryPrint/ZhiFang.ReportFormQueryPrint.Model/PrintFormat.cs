using System;
namespace ZhiFang.ReportFormQueryPrint.Model
{
	/// <summary>
	/// 实体类PrintFormat 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class PrintFormat
	{
		public PrintFormat()
		{}
		#region Model
		private int _id;
		private string _printformatname;
		private string _pintformataddress;
		private string _pintformatfilename;
		private int? _itemparalinenum;
		private string _papersize;
		private string _printformatdesc;
		private int? _batchprint;
        private int? _imageflag;
        private int? _antiflag;
		/// <summary>
		/// Id
		/// </summary>
		public int Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 模板名
		/// </summary>
		public string PrintFormatName
		{
			set{ _printformatname=value;}
			get{return _printformatname;}
		}
		/// <summary>
		/// 模板存放地址
		/// </summary>
		public string PintFormatAddress
		{
			set{ _pintformataddress=value;}
			get{return _pintformataddress;}
		}
		/// <summary>
		/// 模板文件名
		/// </summary>
		public string PintFormatFileName
		{
			set{ _pintformatfilename=value;}
			get{return _pintformatfilename;}
		}
		/// <summary>
		/// 项目个数参数
		/// </summary>
		public int? ItemParaLineNum
		{
			set{ _itemparalinenum=value;}
			get{return _itemparalinenum;}
		}
		/// <summary>
		/// 纸张大小
		/// </summary>
		public string PaperSize
		{
			set{ _papersize=value;}
			get{return _papersize;}
		}
		/// <summary>
		/// 描述
		/// </summary>
		public string PrintFormatDesc
		{
			set{ _printformatdesc=value;}
			get{return _printformatdesc;}
		}
		/// <summary>
		/// 套打
		/// </summary>
		public int? BatchPrint
		{
			set{ _batchprint=value;}
			get{return _batchprint;}
		}
        /// <summary>
        /// 带图标志
        /// </summary>
        public int? ImageFlag
        {
            set { _imageflag = value; }
            get { return _imageflag; }
        }
        /// <summary>
        /// 带抗生素标志
        /// </summary>
        public int? AntiFlag
        {
            set { _antiflag = value; }
            get { return _antiflag; }
        }
		#endregion Model

	}
}

