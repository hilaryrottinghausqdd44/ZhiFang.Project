using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.Model;
using System.Data;

namespace ZhiFang.ReportFormQueryPrint.IDAL
{
    public interface IDDepartment : IDataBase<Model.Department>
    {
         Model.Department GetModel(string where);

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int DeptNo);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(int DeptNo);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Model.Department GetModel(int DeptNo);
        /// <summary>
		/// 获得前几行数据
		/// </summary>
        DataSet GetList(string Where, int page, int limit);
        
    }
}
