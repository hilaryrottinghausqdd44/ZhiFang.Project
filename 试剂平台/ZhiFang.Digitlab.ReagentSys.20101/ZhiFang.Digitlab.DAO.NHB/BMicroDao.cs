using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IDAO;

namespace ZhiFang.Digitlab.DAO.NHB
{
    public class BMicroDao : BaseDaoNHB<BMicro, long>, IDBMicroDao
    {
        /// <summary>
        /// 根据实体删除
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>bool</returns>
        public override bool Delete(BMicro entity)
        {
            bool result = true;
            ZhiFang.Common.Log.Log.Warn("执行删除细菌开始:细菌名称为:" + entity.CName + ",操作人为:" + ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName) + ",操作人ID为:" + ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
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
            BMicro entity = this.Get(id);
            ZhiFang.Common.Log.Log.Warn("执行删除细菌开始:细菌名称为:" + entity.CName + ",操作人为:" + ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName) + ",操作人ID为:" + ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
            int result = this.HibernateTemplate.Delete(" From BMicro bmicro where bmicro.Id=" + id);
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