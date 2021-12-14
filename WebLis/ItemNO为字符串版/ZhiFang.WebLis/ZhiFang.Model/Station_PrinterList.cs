using System;
namespace ZhiFang.Model
{
	/// <summary>
	/// 实体类Station_PrinterList 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Station_PrinterList
	{
		public Station_PrinterList()
		{}
		#region Model
		private int _id;
		private string _stationname;
		private string _printername;
		private string _pagesize;
		private DateTime? _addtime;
		private int? _sort;
		/// <summary>
		/// 
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string StationName
		{
			set{ _stationname=value;}
			get{return _stationname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PrinterName
		{
			set{ _printername=value;}
			get{return _printername;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PageSize
		{
			set{ _pagesize=value;}
			get{return _pagesize;}
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
		public int? Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		#endregion Model

	}
}

