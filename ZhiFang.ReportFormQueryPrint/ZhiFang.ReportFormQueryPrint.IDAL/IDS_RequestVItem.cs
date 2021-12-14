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
    public interface IDS_RequestVItem : IDataBase<Model.S_RequestVItem>
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int ItemNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(int ItemNo);
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Model.TestItem GetModel(int ItemNo);
        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetListLike(Model.TestItem model);

        DataSet GetList(string strWhere, string fields);

        DataSet GetListByReportPublicationID(string ReportPublicationID);
        #endregion  成员方法
    }
}
