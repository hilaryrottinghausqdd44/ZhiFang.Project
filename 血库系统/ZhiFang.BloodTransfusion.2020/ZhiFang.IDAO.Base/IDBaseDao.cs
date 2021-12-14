using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.IDAO.Base
{
    public interface IDBaseDao<T, T1>
    {
        /// <summary>
        /// 获取总记录数
        /// </summary>
        /// <returns>记录数</returns>
        int GetTotalCount();

        /// <summary>
        /// 根据实体获取List
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>IList&lt;T&gt;</returns>
        IList<T> GetObjects(T entity);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>T</returns>
        T Get(T1 id);

        /// <summary>
        /// HQL语句查询
        /// </summary>
        /// <param name="hql">HQL查询字符串，必须带where条件</param>
        /// <returns>IList&lt;T&gt;</returns>
        IList<T> Find<T>(string hql);

        /// <summary>
        /// 插入实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>bool</returns>
        bool Save(T entity);

        /// <summary>
        /// 新增数据，返回主键
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>主键</returns>
        object SaveByEntity(T entity);

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="voList">实体列表</param>
        /// <returns>bool</returns>
        bool BatchSaveVO(T voList);

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        bool Update(T entity);

        /// <summary>
        /// 根据string类型数组更新
        /// </summary>
        /// <param name="strParas">数组</param>
        /// <returns>bool</returns>
        bool Update(string[] strParas);
        /// <summary>
        /// 保存实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        bool SaveOrUpdate(T entity);

        /// <summary>
        /// 根据实体删除
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>bool</returns>
        bool Delete(T entity);

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>bool</returns>
        bool Delete(T1 id);

        bool DeleteByHQL(T1 id);

        void Flush();

        void Evict(T entity);

        /// <summary>
        /// 获取全部集合
        /// </summary>
        /// <returns>IList&lt;T&gt;</returns>
        IList<T> LoadAll();

        /// <summary>
        /// 根据HQL语句获取集合列表
        /// </summary>
        /// <param name="strHQL">HQL条件</param>
        /// <param name="start">页数</param>
        /// <param name="count">每页显示记录数</param>
        /// <returns>EntityList&lt;T&gt;</returns>
        ZhiFang.Entity.Base.EntityList<T> GetListByHQL(string strHqlWhere, int start, int count);

        /// <summary>
        /// 根据HQL语句获取集合列表
        /// </summary>
        /// <param name="strHQL">HQL条件</param>
        /// <param name="Order">排序条件</param>
        /// <param name="start">页数</param>
        /// <param name="count">每页显示记录数</param>
        /// <returns>EntityList&lt;T&gt;</returns>
        ZhiFang.Entity.Base.EntityList<T> GetListByHQL(string strHqlWhere, string Order, int start, int count);

        /// <summary>
        /// 根据HQL语句获取记录数
        /// </summary>
        /// <param name="strHQL">HQL条件</param>
        /// <returns>int</returns>
        int GetListCountByHQL(string strHqlWhere);

        /// 根据HQL语句获取合计结果数
        /// </summary>
        /// <param name="strHQL">HQL条件</param>
        /// <returns>int</returns>
        object GetTotalByHQL(string strHqlWhere, string field);

        /// <summary>
        /// 执行HQL语句
        /// </summary>
        /// <param name="hql"></param>
        /// <returns></returns>
        int UpdateByHql(string hql);
        /// <summary>
        /// 执行HQL语句(删除)
        /// </summary>
        /// <param name="hql"></param>
        /// <returns></returns>
        int DeleteByHql(string hql);

        #region

        /// <summary>
        /// 根据实体获取总记录数
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>记录数</returns>
        //int GetTotalCount(T entity);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        //T Load(T1 id);
        #endregion

        IList<T> GetListByHQL(string strHqlWhere);

    }
}
