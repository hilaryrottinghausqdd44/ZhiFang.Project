using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IBLL.Common
{
    public interface IBDataPage<T>
    {
        /// <summary>
        /// 利用标识列分页
        /// </summary>
        /// <param name="nowPageNum">第几页</param>
        /// <param name="nowPageSize">每页多少行</param>
        /// <returns>DataSet</returns>
        DataSet GetListByPage(T t, int nowPageNum, int nowPageSize);
    }
}
