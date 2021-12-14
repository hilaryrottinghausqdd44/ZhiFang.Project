using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.Model;
using System.Data;
namespace ZhiFang.ReportFormQueryPrint.IDAL
{
	/// <summary>
	/// 接口层IPrintFormat 的摘要说明。
	/// </summary>
	public interface IDPrintFormat:IDAL.IDataBase<Model.PrintFormat>
	{
		#region  成员方法	
	   
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int Id);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(int Id);
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Model.PrintFormat GetModel(int Id);
		#endregion  成员方法
	}
}
