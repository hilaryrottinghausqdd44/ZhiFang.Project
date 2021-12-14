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
	public class QCMatDao : BaseDaoNHB<QCMat, long>, IDQCMatDao
	{
        #region IDQCMatDao 成员

        public IList<QCMat> SearchQCMatByEquipID(long longEquipID)
        {
            Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>();
            dic.Add("QCMat", null);
            dic.Add("EPBEquip", new List<ICriterion>() { Restrictions.Eq("Id", longEquipID) });

            DaoNHBCriteriaAction<List<QCMat>, QCMat> action = new DaoNHBCriteriaAction<List<QCMat>, QCMat>(dic);

            List<QCMat> l = base.HibernateTemplate.Execute<List<QCMat>>(action);
            return l;
            //ICriteria criteria = base.HibernateTemplate.SessionFactory.GetCurrentSession().CreateCriteria(typeof(QCMat))
            //    .CreateCriteria("EPBEquip").Add(Restrictions.Eq("EquipID", longEquipID));

            //return criteria.List<QCMat>();
        }

        public IList<QCMat> SearchQCMatCustomColumnByEquipID(long longEquipID)
        {
            IList<QCMat> tempQCMat = new List<QCMat>();
            string strHQL = "select distinct qcmat.Id,qcmat.CName,qcmat.ConcLevel,qcmat.DispOrder,qcmat.SName from QCMat qcmat join qcmat.QCItemList where qcmat.EPBEquip.Id=" + longEquipID;
            strHQL += " order by qcmat.DispOrder,qcmat.SName ";
            var l = this.HibernateTemplate.Find<object>(strHQL).ToArray();
            if (l != null && l.Count() > 0)
            {
                foreach (var tmp in l)
                {
                    if (((object[])(tmp))[0] != null)
                    {
                        QCMat entity = new QCMat();
                        entity.Id = long.Parse(((object[])(tmp))[0].ToString());
                        entity.CName = ((object[])(tmp))[1] == null ? "" : ((object[])(tmp))[1].ToString();
                        entity.ConcLevel = ((object[])(tmp))[2] == null ? "" : ((object[])(tmp))[2].ToString();
                        tempQCMat.Add(entity);
                    }
                }
            }
            return tempQCMat;
        }

        public IList<QCMat> SearchQCMatCustomColumnByEquipModuleID(long longEquipID, long longModuleID)
        {
            IList<QCMat> tempQCMat = new List<QCMat>();
            string strHQL = "select distinct qcmat.Id,qcmat.CName,qcmat.ConcLevel,qcmat.DispOrder,qcmat.SName from QCMat qcmat join qcmat.QCItemList where qcmat.EPBEquip.Id=" + longEquipID + " and qcmat.EquipModule='" + longModuleID.ToString() + "' ";
            strHQL += " order by qcmat.DispOrder,qcmat.SName ";
            var l = this.HibernateTemplate.Find<object>(strHQL).ToArray();
            if (l != null && l.Count() > 0)
            {
                foreach (var tmp in l)
                {
                    if (((object[])(tmp))[0] != null)
                    {
                        QCMat entity = new QCMat();
                        entity.Id = long.Parse(((object[])(tmp))[0].ToString());
                        entity.CName = ((object[])(tmp))[1] == null ? "" : ((object[])(tmp))[1].ToString();
                        entity.ConcLevel = ((object[])(tmp))[2] == null ? "" : ((object[])(tmp))[2].ToString();
                        tempQCMat.Add(entity);
                    }
                }
            }
            return tempQCMat;
        }

        public IList<QCMat> SearchQCMatByEquipModuleID(long longEquipID, long longModuleID)
        {
            //string strHql = "select qcmat from QCMat qcmat join qcmat.EPBEquip epbequip where epbequip.Id=" + longEquipID + " and qcmat.EquipModule=" + longModuleID.ToString() + "";
            string strHql = "select qcmat from QCMat qcmat where qcmat.EPBEquip.Id=" + longEquipID + " and qcmat.EquipModule='" + longModuleID.ToString() + "' ";
            return this.HibernateTemplate.Find<QCMat>(strHql).ToList();
        }

        public IList<QCMat> SearchQCMatByItemID(long longItemID)
        {
            Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>();
            dic.Add("QCMat", null);
            dic.Add("QCItemList", null);
            dic.Add("ItemAllItem", new List<ICriterion>() { Restrictions.Eq("Id", longItemID) });

            DaoNHBCriteriaAction<List<QCMat>, QCMat> action = new DaoNHBCriteriaAction<List<QCMat>, QCMat>(dic);

            List<QCMat> l = base.HibernateTemplate.Execute<List<QCMat>>(action);
            return l;
            //ICriteria criteria = base.HibernateTemplate.SessionFactory.GetCurrentSession().CreateCriteria(typeof(QCMat))
            //    .CreateCriteria("QCItemList").CreateCriteria("ItemAllItem").Add(Restrictions.Eq("ItemID", longItemID));

            //return criteria.List<QCMat>();
        }

        /// <summary>
        /// 根据检验项目ID获取质控物列表
        /// </summary>
        /// <param name="longItemID">检验项目ID</param>
        /// <param name="longEquipID">仪器ID</param>
        /// <returns>IList&lt;QCMat&gt;</returns>
        public IList<QCMat> SearchQCMatByItemIDAndEquipID(long longItemID, long longEquipID)
        {
            string strHQL = "select qcmat from QCMat qcmat join qcmat.QCItemList qcitemlist where qcmat.EPBEquip.Id= " + longEquipID + " and qcitemlist.ItemAllItem.Id= " + longItemID;
            return this.HibernateTemplate.Find<QCMat>(strHQL);
            //Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>();
            //dic.Add("QCMat", null);
            //dic.Add("EPBEquip", new List<ICriterion>() { Restrictions.Eq("Id", longItemID) });
            //dic.Add("QCItemList", null);
            //dic.Add("ItemAllItem", new List<ICriterion>() { Restrictions.Eq("Id", longItemID) });

            //DaoNHBCriteriaAction<List<QCMat>, QCMat> action = new DaoNHBCriteriaAction<List<QCMat>, QCMat>(dic);

            //List<QCMat> l = base.HibernateTemplate.Execute<List<QCMat>>(action);
            //return l;
        }

        public EntityList<QCMat> SearchQCMatListByHQL(string strHqlWhere, string Order, int start, int count)
        {
            GetDataRowRoleHQLString();

            Entity.EntityList<QCMat> entityList = new Entity.EntityList<QCMat>();

            string strHQL = "select qcmat from QCMat qcmat left outer join qcmat.EPBEquip epbequip where 1=1 ";
            if (strHqlWhere != null && strHqlWhere.Length > 0)
            {
                strHQL += "and " + strHqlWhere;
            }
            strHQL += " and " + DataRowRoleHQLString;
            strHQL = FilterMacroCommand(strHQL);//宏命令过滤
            if (Order != null && Order.Trim().Length > 0)
            {
                string strOrder = Order;
                string strTempOrder = "";
                if (strOrder.IndexOf(",") > 0)
                {
                    string[] arr = strOrder.Split(',');
                    foreach (string s in arr)
                    {
                        if (s.Split('.').Length - 1 > 1)
                        {
                            strTempOrder += s.Split('.')[1].ToString().ToLower() + "." + s.Split('.')[2].ToString() + ",";
                        }
                        else
                            strTempOrder += s + ",";
                    }
                }
                else
                {
                    if (strOrder.Split('.').Length - 1 > 1)
                    {
                        strTempOrder += strOrder.Split('.')[1].ToString().ToLower() + "." + strOrder.Split('.')[2].ToString() + ",";
                    }
                    else
                        strTempOrder += strOrder + ",";
                }
                strTempOrder = strTempOrder.Substring(0, strTempOrder.LastIndexOf(","));

                strHQL += " order by " + strTempOrder;
            }
            int? start1 = null;
            int? count1 = null;
            if (start > 0)
            {
                start1 = start;
            }
            if (count > 0)
            {
                count1 = count;
            }
            DaoNHBSearchByHqlAction<List<QCMat>, QCMat> action = new DaoNHBSearchByHqlAction<List<QCMat>, QCMat>(strHQL, start1, count1);

            entityList.list = this.HibernateTemplate.Execute<List<QCMat>>(action);
            entityList.count = this.GetListCountByHQL(strHqlWhere);
            return entityList;
        }

        /// <summary>
        /// 根据质控项目ID列表获取质控物列表
        /// </summary>
        /// <param name="qcItemIDList">质控项目ID列表</param>
        /// <returns>IList&lt;QCMat&gt;</returns>
        public IList<QCMat> SearchQCMatByQCItemIDList(IList<string> qcItemIDList)
        {
            string strWhere = "";
            if (qcItemIDList != null && qcItemIDList.Count > 0)
            {
                foreach (var tmp in qcItemIDList)
                {
                    strWhere += " qcitemlist.Id=" + tmp + " or ";
                }
            }
            strWhere = " (" + strWhere + " 1=2) ";
            string strHQL = "select qcmat from QCMat qcmat join qcmat.QCItemList qcitemlist where 1=1";
            if (strWhere.Length > 0)
            {
                strHQL += " and " + strWhere;
            }
            strHQL += " order by qcmat.DispOrder";
            var l = this.HibernateTemplate.Find<QCMat>(strHQL);
            return l;
        }

        #endregion
    } 
}