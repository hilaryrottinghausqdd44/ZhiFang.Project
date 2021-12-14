
using System;

namespace ZhiFang.Entity.WebAssist.GKBarcode
{
	/// <summary>
	/// TestType:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class TestType
	{
		public TestType()
		{}
		#region Model
		private int _testtypeid;
		private string _testtypename;
		private int? _testparindex;
		private string _information1;
		private string _information2;
		private string _information3;
		private string _information4;
		private int? _item1_index;
		private int? _item2_index;
		private string _item3_index;
		private int? _item4_index;
		private string _reprttemp;
		private int? _samples_p_rp;
		private int? _infors_p_sample;
		private string _qcstandar;
		private string _jugerepc1text;
		private string _jugerepc1left;
		private string _jugerepc2text;
		private string _jugerepc2left;
		private string _jugerepc3text;
		private string _jugerepc3left;
		/// <summary>
		/// 
		/// </summary>
		public int TestTypeID
		{
			set{ _testtypeid=value;}
			get{return _testtypeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TestTypeName
		{
			set{ _testtypename=value;}
			get{return _testtypename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? TestParIndex
		{
			set{ _testparindex=value;}
			get{return _testparindex;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string information1
		{
			set{ _information1=value;}
			get{return _information1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string information2
		{
			set{ _information2=value;}
			get{return _information2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string information3
		{
			set{ _information3=value;}
			get{return _information3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string information4
		{
			set{ _information4=value;}
			get{return _information4;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Item1_Index
		{
			set{ _item1_index=value;}
			get{return _item1_index;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Item2_Index
		{
			set{ _item2_index=value;}
			get{return _item2_index;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Item3_Index
		{
			set{ _item3_index=value;}
			get{return _item3_index;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Item4_Index
		{
			set{ _item4_index=value;}
			get{return _item4_index;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReprtTemp
		{
			set{ _reprttemp=value;}
			get{return _reprttemp;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Samples_P_RP
		{
			set{ _samples_p_rp=value;}
			get{return _samples_p_rp;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Infors_P_Sample
		{
			set{ _infors_p_sample=value;}
			get{return _infors_p_sample;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string QCStandar
		{
			set{ _qcstandar=value;}
			get{return _qcstandar;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string JugeRepC1Text
		{
			set{ _jugerepc1text=value;}
			get{return _jugerepc1text;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string JugeRepC1Left
		{
			set{ _jugerepc1left=value;}
			get{return _jugerepc1left;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string JugeRepC2Text
		{
			set{ _jugerepc2text=value;}
			get{return _jugerepc2text;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string JugeRepC2Left
		{
			set{ _jugerepc2left=value;}
			get{return _jugerepc2left;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string JugeRepC3Text
		{
			set{ _jugerepc3text=value;}
			get{return _jugerepc3text;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string JugeRepC3Left
		{
			set{ _jugerepc3left=value;}
			get{return _jugerepc3left;}
		}
		#endregion Model

	}
}

