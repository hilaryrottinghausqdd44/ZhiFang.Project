using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace ZhiFang.IBLL.Report
{
	/// <summary>
	/// 接口层ReportMarrowFull
	/// </summary>
    public interface IBReportMarrowFull : IBLLBase<Model.ReportMarrowFull>
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
        Model.ReportMarrowFull GetModel(string ReportFormID, string ReportItemID);
        int DeleteByWhere(string Strwhere);

        DataSet GetList(string strWhere);
        /// <summary>
        /// 得到一个对象实体，从缓存中。
        /// </summary>
        Model.ReportMarrowFull GetModelByCache(string ReportFormID, string ReportItemID);
        bool CheckReportMarrowCenter(DataSet dsReportMarrowFull, string DestiOrgID, out string ReturnDescription);
        bool CheckReportMarrowLab(DataSet dsReportMarrowFull, string DestiOrgID, out string ReturnDescription);

        int BackUpReportMarrowFullByWhere(string Strwhere);
        #endregion  成员方法
	} 
}
