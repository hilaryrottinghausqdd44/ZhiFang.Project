using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IDAO;
using NHibernate;
using NHibernate.Criterion;

namespace ZhiFang.Digitlab.DAO.NHB
{
    public class ItemAllItemDao : BaseDaoNHB<ItemAllItem, long>, IDItemAllItemDao
    {
        #region IDItemAllItemDao 成员

        public IList<ItemAllItem> SearchItemAllItemByEquipID(long longEquipID)
        {
            Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>();
            dic.Add("ItemAllItem", null);
            dic.Add("EPEquipItemList", null);
            dic.Add("EPBEquip", new List<ICriterion>() { Restrictions.Eq("Id", longEquipID) });

            DaoNHBCriteriaAction<List<ItemAllItem>, ItemAllItem> action = new DaoNHBCriteriaAction<List<ItemAllItem>, ItemAllItem>(dic);

            List<ItemAllItem> l = base.HibernateTemplate.Execute<List<ItemAllItem>>(action);
            return l;
        }

        public IList<ItemAllItem> SearchItemAllItemByEquipIDAndValueType(long longEquipID, int intValueType)
        {
            string strHQL = "select itemallitem from ItemAllItem itemallitem join itemallitem.EPEquipItemList epi where epi.EPBEquip.Id=" + longEquipID;
            if (intValueType > 0)
            {
                strHQL += " and itemallitem.ValueType=" + intValueType;
            }
            var l = this.HibernateTemplate.Find<ItemAllItem>(strHQL);
            return l;
        }

        public IList<ItemAllItem> MEPT_UDTO_SearchSpecialtyAndSampleTypeItemList(long SpecialtyId, long SampleTypeId)
        {
            var list = this.HibernateTemplate.Find<ItemAllItem>(" select iai from ItemAllItem iai join iai.BSpecialtyItemList bsi join iai.MEPTSampleTypeItemList mstil where mstil.BSampleType.Id=" + SampleTypeId + " and bsi.BSpecialty.Id=" + SpecialtyId + " and  iai.ItemType=" + ((long)ItemType.DoctorItem) + "  ");
            return list;

        }

        #endregion

        /// <summary>
        /// 根据实体删除
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>bool</returns>
        public override bool Delete(ItemAllItem entity)
        {
            bool result = true;
            ZhiFang.Common.Log.Log.Warn("执行删除小组项目开始:检验项目名称为:" + entity.CName + ",操作人为:" + ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName) + ",操作人ID为:" + ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
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
            ItemAllItem entity = this.Get(id);
            ZhiFang.Common.Log.Log.Warn("执行删除小组项目开始:检验项目名称为:" + entity.CName + ",操作人为:" + ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName) + ",操作人ID为:" + ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
            int result = this.HibernateTemplate.Delete(" From ItemAllItem itemallitem where itemallitem.Id=" + id);
            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public IList<ItemAllItem> SearchListByGroupId(long GroupId)
        {

            string strHQL = "select iai from ItemAllItem iai inner join iai.GMGroupItemList  gmgroupitemlist where gmgroupitemlist.GMGroup.Id = " + GroupId;

            //select distinct c from Customer c inner join c.Orders o
            var aaa =this.HibernateTemplate.Find<object>(strHQL);
            var pl = this.HibernateTemplate.Find<ItemAllItem>(strHQL);
            return pl;
        }
    }
}