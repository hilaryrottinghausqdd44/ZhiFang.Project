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
	public class MEPTSampleItemDao : BaseDaoNHB<MEPTSampleItem, long>, IDMEPTSampleItemDao
	{
        public IList<MEPTSampleItem> SearchMEPTSampleItemByGMGroupID(long meptSampleFormID, long gmGroupID)
        {
            Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>();
            
            dic.Add("MEPTSampleItem", new List<ICriterion>() { Restrictions.Eq("SampleFrom.Id", meptSampleFormID) });
            if (gmGroupID > 0)
            {
                dic.Add("ItemAllItem", null);
                dic.Add("GMGroupItemList", null);
                dic.Add("GMGroup", new List<ICriterion>() { Restrictions.Eq("Id", gmGroupID) });
            }
            DaoNHBCriteriaAction<List<MEPTSampleItem>, MEPTSampleItem> action = new DaoNHBCriteriaAction<List<MEPTSampleItem>, MEPTSampleItem>(dic);

            IList<MEPTSampleItem> list = base.HibernateTemplate.Execute<List<MEPTSampleItem>>(action);
            return list;
        }
	} 
}