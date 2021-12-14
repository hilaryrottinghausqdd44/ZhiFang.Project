using System;
using System.Data;
namespace ZhiFang.IDAL
{
    /// <summary>
    /// 接口层IDDepartmentControl	
    /// </summary>
    public interface IDDepartmentControl : IDataBase<ZhiFang.Model.DepartmentControl>
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string DepartmentControlNo);
        bool Exists(System.Collections.Hashtable ht);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(string DepartmentControlNo);


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.DepartmentControl GetModel(string DepartmentControlNo);

        /// <summary>
        /// 数据同步时增加数据
        /// </summary>
        int AddByDataRow(DataRow dr);
        /// <summary>
        /// 数据同步时修改数据
        /// </summary>
        int UpdateByDataRow(DataRow dr);

        #endregion  成员方法

        #region 字典对照
        DataSet GetListByPage(ZhiFang.Model.DepartmentControl model, int nowPageNum, int nowPageSize);

        DataSet B_lab_GetListByPage(ZhiFang.Model.DepartmentControl model, int nowPageNum, int nowPageSize);
        #endregion
    }
}