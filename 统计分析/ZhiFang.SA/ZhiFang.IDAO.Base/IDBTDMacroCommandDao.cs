using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.IDAO.Base
{
	public interface IDBTDMacroCommandDao 
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
        Dictionary<string, BTDMacroCommand> GetObjects();

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>T</returns>
        BTDMacroCommand Get(string key);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        BTDMacroCommand Load(string key);

        /// <summary>
        /// 插入实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>bool</returns>
        bool Save(string key, BTDMacroCommand value);

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        bool Update(string key, BTDMacroCommand value);

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>bool</returns>
        bool Delete(string key);
        /// <summary>
        /// 宏过滤
        /// </summary>
        /// <param name="hql"></param>
        /// <returns></returns>
        string FilterMacroCommand(string hql);
	} 
}