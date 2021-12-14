using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IBLL.Report
{
    public interface IBLLBase<T>
    {
        #region  成员方法
        /// <summary>
        /// 得到最大ID
        /// </summary>
        int GetMaxId();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(T t);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        int Update(T t);

        /// <summary>
        /// 获得数据列表
        /// </summary>
        //DataSet GetList(string strWhere);
        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(T t);
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        //DataSet GetList(int Top, string strWhere, string filedOrder);
        /// <summary>
        /// 获得数据列表
        /// </summary>
       // List<T> GetModelList(string strWhere);
        /// <summary>
        /// 获得数据列表
        /// </summary>
        List<T> GetModelList(T t);
        /// <summary>
        /// 获得数据列表
        /// </summary>
        List<T> DataTableToList(DataTable dt);

        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetAllList();

        #endregion  成员方法
    }
}
