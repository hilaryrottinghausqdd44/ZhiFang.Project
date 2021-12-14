using System;
using System.Data;
namespace ZhiFang.ReportFormQueryPrint.IDAL
{
	/// <summary>
	/// 接口层PatNo_Passwords
	/// </summary>
    public interface IDPatNo_Passwords : IDataBase<Model.PatNo_Passwords>
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int Id);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int Id);
		Model.PatNo_Passwords GetModel(int Id);
		#endregion  成员方法
	} 
}
