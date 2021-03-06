using System;
using System.Data;
namespace ZhiFang.IDAL
{
    /// <summary>
    /// 接口层SearchReCheckLog
    /// </summary>
    public interface IDSearchReCheckLog
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(long Id);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool Add(ZhiFang.Model.SearchReCheckLog model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(ZhiFang.Model.SearchReCheckLog model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(long Id);
        bool DeleteList(string Idlist);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.SearchReCheckLog GetModel(long Id);
        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(string strWhere);
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        DataSet GetList(int Top, string strWhere, string filedOrder);
        int GetRecordCount(string strWhere);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        /// <summary>
        /// 根据分页获得数据列表
        /// </summary>
        //DataSet GetList(int PageSize,int PageIndex,string strWhere);
        #endregion  成员方法
    }
}
