using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.Model;
using System.Data;

namespace ZhiFang.ReportFormQueryPrint.IDAL
{
	/// <summary>
	/// 接口层ICLIENTELE 的摘要说明。
	/// </summary>
    public interface IDCLIENTELE : IDataBase<Model.CLIENTELE>
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int ClIENTNO);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(int ClIENTNO);
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Model.CLIENTELE GetModel(int ClIENTNO);

        DataSet GetList(string where,string fields);
		#endregion  成员方法
	}
}
