using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;

namespace ZhiFang.IBLL.Base
{
    public interface IBGenericManager<T>
    {
        /// <summary>
        /// 获取或设置实体
        /// </summary>
        T Entity { get; set; }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <returns></returns>
        bool Add();
        bool Save();
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <returns></returns>
        bool Edit();

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <returns></returns>
        bool Remove();

        /// <summary>
        /// 根据ID删除数据
        /// </summary>
        /// <param name="longID">记录ID</param>
        /// <returns></returns>
        bool Remove(long longID);

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="strParas">需要更新的字符数组</param>
        /// <returns></returns>
        int DeleteByHql(string strHqlWhere);

        /// <summary>
        /// 查找实体列表
        /// </summary>
        /// <returns></returns>
        IList<T> Search();
        /// <summary>
        /// 根据条件查询实体列表
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        IList<T> SearchListByHQL(string strHqlWhere);
        /// <summary>
        /// 根据条件查询实体列表
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        ZhiFang.Entity.Base.EntityList<T> SearchListByHQL(string strHqlWhere, int page, int count);

        /// <summary>
        /// 根据条件查询实体列表
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        EntityList<T> SearchListByHQL(string strHqlWhere, string Order, int page, int count);

        /// <summary>
        /// 根据ID获取实体
        /// </summary>
        /// <param name="longID"></param>
        /// <returns></returns>
        T Get(long longID);

        /// <summary>
        /// 获取所有记录条数
        /// </summary>
        /// <returns></returns>
        int GetTotalCount();



        /// <summary>
        /// 获取所有数据实体列表
        /// </summary>
        /// <returns></returns>
        IList<T> LoadAll();



        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="strParas">需要更新的字符数组</param>
        /// <returns></returns>
        bool Update(string[] strParas);



        #region

        ///// <summary>
        ///// 根据实体获取记录条数
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //int GetTotalCount(T entity);

        /// <summary>
        /// 根据ID加载实体
        /// </summary>
        /// <param name="longID"></param>
        /// <returns></returns>
        //T Load(long longID);

        #endregion
    }
    public interface IBGenericManager<T, T1>
    {
        /// <summary>
        /// 获取或设置实体
        /// </summary>
        T Entity { get; set; }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <returns></returns>
        bool Add();

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <returns></returns>
        bool Edit();

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <returns></returns>
        bool Remove();

        /// <summary>
        /// 根据ID删除数据
        /// </summary>
        /// <param name="longID">记录ID</param>
        /// <returns></returns>
        bool Remove(T1 longID);

        /// <summary>
        /// 查找实体列表
        /// </summary>
        /// <returns></returns>
        IList<T> Search();

        /// <summary>
        /// 根据条件查询实体列表
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        EntityList<T> SearchListByHQL(string strHqlWhere, int page, int count);

        /// <summary>
        /// 根据条件查询实体列表
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        EntityList<T> SearchListByHQL(string strHqlWhere, string Order, int page, int count);

        /// <summary>
        /// 根据ID获取实体
        /// </summary>
        /// <param name="longID"></param>
        /// <returns></returns>
        T Get(T1 longID);

        /// <summary>
        /// 获取所有记录条数
        /// </summary>
        /// <returns></returns>
        int GetTotalCount();

        /// <summary>
        /// 获取所有数据实体列表
        /// </summary>
        /// <returns></returns>
        IList<T> LoadAll();

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="strParas">需要更新的字符数组</param>
        /// <returns></returns>
        bool Update(string[] strParas);

        #region
        ///// <summary>
        ///// 根据实体获取记录条数
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //int GetTotalCount(T entity);
        /// <summary>
        /// 根据ID加载实体
        /// </summary>
        /// <param name="longID"></param>
        /// <returns></returns>
        //T Load(T1 longID);
        #endregion
    }
}

