using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Model;
using System.Data;
namespace ZhiFang.IDAL
{
	/// <summary>
	/// 接口层IPrintFormat 的摘要说明。
	/// </summary>
	public interface IDPrintFormat:IDAL.IDataBase<ZhiFang.Model.PrintFormat>
	{
		#region  成员方法	
	   
		/// <summary>
		/// 是否存在该记录
		/// </summary>
        bool Exists(string Id);
		/// <summary>
		/// 删除一条数据
		/// </summary>
        int Delete(string Id);
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        Model.PrintFormat GetModel(string Id);
		#endregion  成员方法

        DataSet GetListByPage(PrintFormat model, int nowPageNum, int nowPageSize);
    }
}
