using System;
using System.Data;
using ZhiFang.IBLL.Common;
using System.Collections.Generic;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
    /// <summary>
    /// 接口层IBCLIENTELE	
    /// </summary>
    public interface IBCLIENTELE : IBBase<ZhiFang.Model.CLIENTELE>, IBDataPage<ZhiFang.Model.CLIENTELE>
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(long ClIENTNO);
        DataSet GetClientNo(string CLIENTIDlist, string CName);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(long ClIENTNO);
        /// <summary>
        /// DataTable转换为List
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns>List</returns>
        List<ZhiFang.Model.CLIENTELE> DataTableToList(DataTable dt);
        int DeleteList(string CLIENTIDlist);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.CLIENTELE GetModel(long ClIENTNO);

        #endregion  成员方法

        DataSet GetList(int p, Model.CLIENTELE c);
        DataSet GetList(ZhiFang.Model.CLIENTELE model);
        int Add(List<ZhiFang.Model.CLIENTELE> modelList);
        int Update(List<ZhiFang.Model.CLIENTELE> modelList);
    }
}