using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace ZhiFang.IBLL.Report
{
	/// <summary>
	/// 接口层ReportMicroFull
	/// </summary>
    public interface IBReportMicroFull : IBLLBase<Model.ReportMicroFull>
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
        Model.ReportMicroFull GetModel(string ReportFormID, string ReportItemID);

        DataSet GetColumns();
        /// <summary>
        /// 得到一个对象实体，从缓存中。
        /// </summary>
        Model.ReportMicroFull GetModelByCache(string ReportFormID, string ReportItemID);
        bool CheckReportMicroCenter(DataSet dsReportMicroFull, string DestiOrgID, out string ReturnDescription);
        bool CheckReportMicroLab(DataSet dsReportMicroFull, string DestiOrgID, out string ReturnDescription);

        DataSet GetReportMicroGroupList(string FormNo);

        int DeleteByWhere(string Strwhere);
        DataSet GetList(string strWhere);
        int BackUpReportMicroFullByWhere(string Strwhere);
        #endregion  成员方法
	} 
}
