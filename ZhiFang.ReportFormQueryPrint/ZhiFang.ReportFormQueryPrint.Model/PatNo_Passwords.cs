using System;
namespace ZhiFang.ReportFormQueryPrint.Model
{
	/// <summary>
	/// PatNo_Passwords:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class PatNo_Passwords
	{
		public PatNo_Passwords()
		{}
		#region Model
		private int _id;
		private string _patno;
		private string _passwords;
		private DateTime? _addtime= DateTime.Now;
        private DateTime? _updatetime;
		/// <summary>
		/// 
		/// </summary>
		public int Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PatNo
		{
			set{ _patno=value;}
			get{return _patno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Passwords
		{
			set{ _passwords=value;}
			get{return _passwords;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? AddTime
		{
			set{ _addtime=value;}
			get{return _addtime;}
		}
		/// <summary>
		/// 
		/// </summary>
        public DateTime? UpdateTime
		{
            set { _updatetime = value; }
            get { return _updatetime; }
		}
		#endregion Model

	}
}

