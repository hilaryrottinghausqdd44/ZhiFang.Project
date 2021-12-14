using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.Model;
using System.Data;

namespace ZhiFang.ReportFormQueryPrint.IDAL
{
	/// <summary>
	/// 接口层ITestItem 的摘要说明。
	/// </summary>
	public interface IDStation_PrinterList: IDataBase<Model.Station_PrinterList>
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int id);		
		/// <summary>
		/// 删除一条数据
		/// </summary>
        int Delete(int id);
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Model.Station_PrinterList GetModel(int id);		
		#endregion  成员方法
	}
}
