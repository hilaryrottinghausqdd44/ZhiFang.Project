using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IBLL.Common
{
    public interface IBBase<T>
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(T model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        int Update(T model);
        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(T model);        
        /// <summary>
        /// 获取总记录数
        /// </summary>
        int GetTotalCount();
        int GetTotalCount(T model);
        /// <summary>
        /// 获得所有数据列表
        /// </summary>
        DataSet GetAllList();
        
    }
}
