using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IDAL
{
    public interface IDataBase<T>
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
        DataSet GetList(T t);        
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        DataSet GetList(int Top, T t, string filedOrder);
        /// <summary>
        /// 获得所有数据列表
        /// </summary>
        DataSet GetAllList();
        /// <summary>
        /// 根据DataSet增加或修改数据，同步字典时使用
        /// </summary>
        int AddUpdateByDataSet(DataSet ds);
        /// <summary>
        /// 获取总记录数
        /// </summary>
        int GetTotalCount();
        int GetTotalCount(T t);
        #endregion  成员方法
    }
}
