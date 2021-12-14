using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Model;
using System.Data;
using ZhiFang.IDAL;

namespace ZhiFang.IDAL
{
	/// <summary>
	/// 接口层ReportItemFull
	/// </summary>
    public interface IDReportItemFull : IDataBase<ZhiFang.Model.ReportItemFull>
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
        bool Exists(string ReportFormID, string ReportItemID);
		/// <summary>
		/// 删除一条数据
		/// </summary>
        int Delete(string ReportFormID, string ReportItemID);

        DataSet GetColumns();
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        Model.ReportItemFull GetModel(string ReportFormID, string ReportItemID);

        DataSet GetList(string strWhere);
		#endregion  成员方法
        int DeleteByWhere(string where);

        int BackUpReportItemFullByWhere(string Strwhere);
	} 
}
