using System;
using System.Data;
namespace ZhiFang.IDAL
{
    /// <summary>
    /// 接口层WhoNet_AntiData
    /// </summary>
    public interface IWhoNet_AntiData : IDataBase<ZhiFang.Model.WhoNet_AntiData>
    {
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(ZhiFang.Model.WhoNet_AntiData model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        int Update(ZhiFang.Model.WhoNet_AntiData model);
        /// <summary>
        /// 删除数据
        /// </summary>
        int Delete(long AntiID);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.WhoNet_AntiData GetModel(long AntiID);
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