using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace ZhiFang.IBLL.Report
{
	/// <summary>
	/// 接口层ReportItemFull
	/// </summary>
    public interface IBReportItemFull : IBLLBase<Model.ReportItemFull>
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

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Model.ReportItemFull GetModel(string ReportFormID, string ReportItemID);
        DataSet GetList(string strWhere);
        int DeleteByWhere(string where);
        DataSet GetColumns();
        /// <summary>
        /// 得到一个对象实体，从缓存中。
        /// </summary>
        Model.ReportItemFull GetModelByCache(string ReportFormID, string ReportItemID);
        bool CheckReportItemCenter(DataSet dsReportItemFull, string DestiOrgID, out string ReturnDescription);
        bool CheckReportItemCenter_yinzhou(DataSet dsReportItemFull, string DestiOrgID, out string ReturnDescription);
        bool CheckReportItemLab(DataSet dsReportItemFull, string DestiOrgID, out string ReturnDescription);
        int BackUpReportItemFullByWhere(string Strwhere);
        #endregion  成员方法

	} 
}
