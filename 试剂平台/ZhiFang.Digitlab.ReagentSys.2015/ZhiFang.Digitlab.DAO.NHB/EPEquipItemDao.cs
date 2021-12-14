using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IDAO;

namespace ZhiFang.Digitlab.DAO.NHB
{	
	public class EPEquipItemDao : BaseDaoNHB<EPEquipItem, long>, IDEPEquipItemDao
	{
        #region IDEPEquipItemDao 成员

        /// <summary>
        /// 根据项目结果类型获取仪器项目列表
        /// </summary>
        /// <param name="intValueType">结果类型</param>
        /// <returns></returns>
        public IList<EPEquipItem> SearchEPEquipItemByItemValueType(int intValueType)
        {
            string strHQL = "select epequipitem from EPEquipItem epequipitem where 1=1";
            if (intValueType > 0)
            {
                strHQL += " and epequipitem.ItemAllItem.ValueType=" + intValueType;
            }
            var l = this.HibernateTemplate.Find<EPEquipItem>(strHQL);
            return l;
        }

        /// <summary>
        /// 根据专业ID和项目结果类型获取仪器项目列表
        /// </summary>
        /// <param name="SpecialtyID">专业ID</param>
        /// <param name="intValueType">结果类型</param>
        /// <returns></returns>
        public IList<EPEquipItem> SearchEPEquipItemBySpecialtyIDAndItemValueType(long longSpecialtyID, int intValueType)
        {
            string strHQL = "select epequipitem from EPEquipItem epequipitem where epequipitem.ItemAllItem.BSpecialty.Id=" + longSpecialtyID;
            if (intValueType > 0)
            {
                strHQL += " and epequipitem.ItemAllItem.ValueType=" + intValueType;
            }
            var l = this.HibernateTemplate.Find<EPEquipItem>(strHQL);
            return l;
        }

        #endregion
        /// <summary>
        /// 根据实体删除
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>bool</returns>
        public override bool Delete(EPEquipItem entity)
        {
            bool result = true;
            ZhiFang.Common.Log.Log.Warn("执行删除仪器项目开始:仪器项目名称为:" + entity.ItemAllItem.CName + ",操作人为:" + ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName) + ",操作人ID为:" + ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
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
            EPEquipItem entity = this.Get(id);
            ZhiFang.Common.Log.Log.Warn("执行删除仪器项目开始:仪器项目名称为:" + entity.ItemAllItem.CName + ",操作人为:" + ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName) + ",操作人ID为:" + ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
            int result = this.HibernateTemplate.Delete(" From EPEquipItem epequipitem where epequipitem.Id=" + id);
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