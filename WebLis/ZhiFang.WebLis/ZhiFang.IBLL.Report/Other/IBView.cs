using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IBLL.Report.Other
{
    public interface IBView
    {
        /// <summary>
        /// 查询指定视图中所有数据
        /// </summary>
        /// <param name="Top">条数</param>
        /// <param name="ViewName">视图名称</param>
        /// <param name="strWhere">条件</param>
        /// <param name="StrOrder">排序</param>
        /// <returns>DataSet</returns>
        DataSet GetViewData(int Top,string ViewName,string strWhere,string StrOrder);
        DataSet GetViewData_Revise(int Top, string ViewName, string strWhere, string StrOrder);
        DataSet GetReportValue(string[] p);
    }
}
