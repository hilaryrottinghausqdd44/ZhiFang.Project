using System;
using System.Data;
using ZhiFang.IBLL.Common;
using System.Collections.Generic;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
    /// <summary>
    /// 接口层IBBUSINESSMAN	
    /// </summary>
    public interface IBBUSINESSMAN : IBBase<ZhiFang.Model.BUSINESSMAN>, IBDataPage<ZhiFang.Model.BUSINESSMAN>
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int BMANNO);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(int BMANNO);



        int DeleteList(string BNANIDlist);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.BUSINESSMAN GetModel(int BMANNO);


        /// <summary>
        /// DataTable转换为List
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns>List</returns>
        List<ZhiFang.Model.BUSINESSMAN> DataTableToList(DataTable dt);
        #endregion  成员方法
    }
}