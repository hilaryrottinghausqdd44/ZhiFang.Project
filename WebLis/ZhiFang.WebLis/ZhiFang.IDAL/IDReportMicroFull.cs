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
	/// 接口层ReportMicroFull
	/// </summary>
    public interface IDReportMicroFull : IDataBase<ZhiFang.Model.ReportMicroFull>
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
        Model.ReportMicroFull GetModel(string ReportFormID, string ReportItemID);


        /// <summary>
        /// 微生物项目表单信息
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        DataSet GetReportMicroGroupList(string FormNo);

        int DeleteByWhere(string Strwhere);
        DataSet GetList(string strWhere);
        int BackUpReportMicroFullByWhere(string Strwhere);
		#endregion  成员方法
	} 
}
