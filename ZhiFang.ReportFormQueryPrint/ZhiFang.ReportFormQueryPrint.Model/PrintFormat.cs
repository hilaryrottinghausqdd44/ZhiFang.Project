using System;
namespace ZhiFang.ReportFormQueryPrint.Model
{
	/// <summary>
	/// ʵ����PrintFormat ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
		/// ģ����
		/// </summary>
		public string PrintFormatName
		{
			set{ _printformatname=value;}
			get{return _printformatname;}
		}
		/// <summary>
		/// ģ���ŵ�ַ
		/// </summary>
		public string PintFormatAddress
		{
			set{ _pintformataddress=value;}
			get{return _pintformataddress;}
		}
		/// <summary>
		/// ģ���ļ���
		/// </summary>
		public string PintFormatFileName
		{
			set{ _pintformatfilename=value;}
			get{return _pintformatfilename;}
		}
		/// <summary>
		/// ��Ŀ��������
		/// </summary>
		public int? ItemParaLineNum
		{
			set{ _itemparalinenum=value;}
			get{return _itemparalinenum;}
		}
		/// <summary>
		/// ֽ�Ŵ�С
		/// </summary>
		public string PaperSize
		{
			set{ _papersize=value;}
			get{return _papersize;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string PrintFormatDesc
		{
			set{ _printformatdesc=value;}
			get{return _printformatdesc;}
		}
		/// <summary>
		/// �״�
		/// </summary>
		public int? BatchPrint
		{
			set{ _batchprint=value;}
			get{return _batchprint;}
		}
        /// <summary>
        /// ��ͼ��־
        /// </summary>
        public int? ImageFlag
        {
            set { _imageflag = value; }
            get { return _imageflag; }
        }
        /// <summary>
        /// �������ر�־
        /// </summary>
        public int? AntiFlag
        {
            set { _antiflag = value; }
            get { return _antiflag; }
        }
		#endregion Model

	}
}

