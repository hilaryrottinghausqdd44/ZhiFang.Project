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
	public class ItemRefeRangeDao : BaseDaoNHB<ItemRefeRange, long>, IDItemRefeRangeDao
	{
        public IList<ItemRefeRange> SearchItemRefeRangeByItemID(long longItemID, string OrderField, bool isASC)
        {
            Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>();
            dic.Add("ItemRefeRange", null);
            dic.Add("ItemAllItem", new List<ICriterion>() { Restrictions.Eq("Id", longItemID) });       //, new Order(OrderField, isASC)
            DaoNHBCriteriaAction<List<ItemRefeRange>, ItemRefeRange> action = new DaoNHBCriteriaAction<List<ItemRefeRange>, ItemRefeRange>(dic);
            List<ItemRefeRange> list = base.HibernateTemplate.Execute<List<ItemRefeRange>>(action);
            return list;
        }

        public IList<ItemRefeRange> SearchItemRefeRangeByItemID(long longItemID, string strOrder)
        {
            string strHQL = "from ItemRefeRange itemreferange " +
                            "left join itemreferange.ItemAllItem itemallitem " +
                            "where itemallitem.Id="+longItemID.ToString();

            if (strOrder != null && strOrder.Trim().Length > 0)
                strHQL += " order by " + strOrder;

            strHQL = "select itemreferange " + strHQL;

            DaoNHBSearchByHqlAction<List<ItemRefeRange>, ItemRefeRange> action = new DaoNHBSearchByHqlAction<List<ItemRefeRange>, ItemRefeRange>(strHQL, 0, 0);

            IList<ItemRefeRange> list = this.HibernateTemplate.Execute<List<ItemRefeRange>>(action).ToList();

            return list;
        }

        public IList<ItemRefeRange> SearchItemRefeRangeByItemID(string strItemIDList, string strOrder)
        {
            IList<ItemRefeRange> list = new List<ItemRefeRange>();
            if (!string.IsNullOrEmpty(strItemIDList.Trim()))
            {
                string strHQL = "from ItemRefeRange itemreferange " +
                                "left join itemreferange.ItemAllItem itemallitem " +
                                "where itemallitem.Id in (" + strItemIDList + ")";

                if (strOrder != null && strOrder.Trim().Length > 0)
                    strHQL += " order by " + strOrder;

                strHQL = "select itemreferange " + strHQL;

                DaoNHBSearchByHqlAction<List<ItemRefeRange>, ItemRefeRange> action = new DaoNHBSearchByHqlAction<List<ItemRefeRange>, ItemRefeRange>(strHQL, 0, 0);

                list = this.HibernateTemplate.Execute<List<ItemRefeRange>>(action).ToList();
            }

            return list;
        }
	} 
}