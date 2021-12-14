using System;
using System.Data;
using System.Collections.Generic;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
    /// <summary>
    /// 接口层IBLab_Department	
    /// </summary>
    public interface IBLab_Department : IBBase<ZhiFang.Model.Lab_Department>, IBDataPage<ZhiFang.Model.Lab_Department>
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string LabCode, int LabDeptNo);
        bool Exists(System.Collections.Hashtable ht);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(string LabCode, int LabDeptNo);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.Lab_Department GetModel(string LabCode, int LabDeptNo);
        /// <summary>
        /// DataTable转换为List
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns>List</returns>
        List<ZhiFang.Model.Lab_Department > DataTableToList(DataTable dt);	
        DataSet GetListByLike(ZhiFang.Model.Lab_Department model);

        /// <summary>
        /// 数据同步时增加数据
        /// </summary>
        int AddByDataRow(DataRow dr);
        /// <summary>
        /// 数据同步时修改数据
        /// </summary>
        int UpdateByDataRow(DataRow dr);
        #endregion  成员方法
    }
}