using System;
using System.Data;
namespace ZhiFang.IDAL
{
    /// <summary>
    /// 接口层WhoNet_FormData
    /// </summary>
    public interface IWhoNet_FormData : IDataBase<ZhiFang.Model.WhoNet_FormData>
    {
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(ZhiFang.Model.WhoNet_FormData model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        int Update(ZhiFang.Model.WhoNet_FormData model);
        /// <summary>
        /// 删除数据
        /// </summary>
        int Delete();

        DataSet JoinCount(string LABORATORY, DateTime? SPEC_DATE, string SPEC_TYPE, string ORGANISM);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.WhoNet_FormData GetModel();
        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(string strWhere);
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        DataSet GetList(int Top, string strWhere, string filedOrder);
        /// <summary>
        /// 根据分页获得数据列表
        /// </summary>
        //DataSet GetList(int PageSize,int PageIndex,string strWhere);
        #endregion  成员方法
    }
}