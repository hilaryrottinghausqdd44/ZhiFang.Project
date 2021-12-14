using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IDAO;

namespace ZhiFang.Digitlab.DAO.NHB
{
    public class GMGroupDao : BaseDaoNHB<GMGroup, long>, IDGMGroupDao
    {
        /// <summary>
        /// 根据实体删除
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>bool</returns>
        public override bool Delete(GMGroup entity)
        {
            bool result = true;
            ZhiFang.Common.Log.Log.Warn("执行删除检验小组开始:检验小组名称为:" + entity.Name + ",操作人为:" + ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName) + ",操作人ID为:" + ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
            try
            {
                this.HibernateTemplate.Delete(entity);
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>bool</returns>
        public override bool Delete(long id)
        {
            GMGroup entity = this.Get(id);
            ZhiFang.Common.Log.Log.Warn("执行删除检验小组开始:检验小组名称为::" + entity.Name + ",操作人为:" + ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName) + ",操作人ID为:" + ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
            int result = this.HibernateTemplate.Delete(" From GMGroup gmmroup where gmmroup.Id=" + id);
            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}