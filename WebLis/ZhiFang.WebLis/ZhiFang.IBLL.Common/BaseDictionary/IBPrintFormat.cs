using System;
using System.Data;
using System.Collections.Generic;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
    /// <summary>
    /// 接口层IBPrintFormat	
    /// </summary>
    public interface IBPrintFormat : IBBase<ZhiFang.Model.PrintFormat>, IBDataPage<ZhiFang.Model.PrintFormat>
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string PrintFormatNo);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(string PrintFormatNo);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.PrintFormat GetModel(string PrintFormatNo);

        /// <summary>
        /// DataTable转换为List
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns>List</returns>
        List<ZhiFang.Model.PrintFormat> DataTableToList(DataTable dt);
        #endregion  成员方法

        DataSet GetList(int p, Model.PrintFormat c);
       
    }
}