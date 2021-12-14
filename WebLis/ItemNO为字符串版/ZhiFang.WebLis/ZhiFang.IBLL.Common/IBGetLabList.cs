using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IBLL.Common
{
    public interface IBGetLabList
    {
        /// <summary>
        /// 获取实验室列表
        /// </summary>
        /// <param name="LabFrom">输入实验室来源</param>
        DataSet GetLabLst_Dept(ZhiFang.Model.Department model, out string LabFrom);
        DataSet GetLabLst_Client(ZhiFang.Model.CLIENTELE model, out string LabFrom);
        DataSet GetLabLst_RBAC(string str, out string LabFrom);
		/// <summary>
		/// DataTable转换为List
		/// </summary>
		/// <param name="dt">DataTable</param>
		/// <returns>List</returns>
		List<ZhiFang.Model.CLIENTELE> DataTableToList_Client(DataTable dt);
    }
}
