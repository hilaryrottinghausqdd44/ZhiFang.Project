using System;
namespace ZhiFang.ReportFormQueryPrint.Model
{
	/// <summary>
	/// ʵ����SampleType ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class SampleType
	{
		public SampleType()
		{}
		#region Model
		private long _sampletypeno;
		private string _cname;
		private string _shortcode;
		private int _visible;
		private int? _disporder;
		private string _hisordercode;
		/// <summary>
		/// 
		/// </summary>
		public long SampleTypeNo
		{
			set{ _sampletypeno=value;}
			get{return _sampletypeno;}
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
		public string ShortCode
		{
			set{ _shortcode=value;}
			get{return _shortcode;}
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
		public string HisOrderCode
		{
			set{ _hisordercode=value;}
			get{return _hisordercode;}
		}
		#endregion Model

	}
}

