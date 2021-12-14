using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IDAO;
using ZhiFang.Common.Public;
using NHibernate;
using NHibernate.Criterion;

namespace ZhiFang.Digitlab.DAO.NHB
{	
	public class MEPTSampleFormDao : BaseDaoNHB<MEPTSampleForm, long>, IDMEPTSampleFormDao
	{
        #region IDMEPTSampleFormDao 成员

        public EntityList<MEPTSampleForm> SearchMEPTSampleFormByHQL(string strHqlWhere, string strOrder, int start, int count)
        {
            EntityList<MEPTSampleForm> list = new EntityList<MEPTSampleForm>();

            string strHQL = "from MEPTSampleForm meptsampleform " +
                            "join meptsampleform.MEPTOrderForm meptorderform, " +
                            "BSampleOperate bsampleoperate join bsampleoperate.OperateType bsampleoperatetype, " +
                            "BSampleStatus bsamplestatus join bsamplestatus.BSampleStatusType bsamplestatustype " +
                            "where meptsampleform.Id = bsampleoperate.OperateObjectID and meptsampleform.Id = bsamplestatus.OperateObjectID ";
                            
            if (strHqlWhere != null && strHqlWhere.Length > 0)
            {
                strHQL += "and " + strHqlWhere;
            }
            string hqlCount = strHQL; 
            if (strOrder != null && strOrder.Trim().Length > 0)
            {
                strHQL += " order by " + strOrder;
            }

            //获取样本单信息
            string strTempHQL = "select distinct meptsampleform " + strHQL;
            //获取样本单总数
            string strCount = "select count(distinct meptsampleform.Id) " + hqlCount;

            DaoNHBSearchByHqlAction<List<MEPTSampleForm>, MEPTSampleForm> action = new DaoNHBSearchByHqlAction<List<MEPTSampleForm>, MEPTSampleForm>(strTempHQL, start, count);
            DaoNHBGetCountByHqlAction<int> actionCount = new DaoNHBGetCountByHqlAction<int>(strCount);
           
            list.list = this.HibernateTemplate.Execute<List<MEPTSampleForm>>(action);
            list.count = this.HibernateTemplate.Execute<int>(actionCount);    
            return list;
        }

        public IList<MEPTSampleForm> SearchMEPTSampleFormByGMGroupID(string startDate ,string endDate, long gmGroupID, string sort)
        {
            Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>(); 

            //dic.Add("MEPTSampleForm", new List<ICriterion>() { Restrictions.Between("BarCodeOpTime", DateTime.Parse(startDate), DateTime.Parse(endDate)) });
            dic.Add("MEPTSampleForm", new List<ICriterion>() { Restrictions.Ge("DataAddTime", DateTime.Parse(startDate)), Restrictions.Le("DataAddTime", DateTime.Parse(endDate)) });
            if (gmGroupID > 0)
            {
                dic.Add("MEPTSampleItemList", null);
                dic.Add("ItemAllItem", null);
                dic.Add("GMGroupItemList", null);
                dic.Add("GMGroup", new List<ICriterion>() { Restrictions.Eq("Id", gmGroupID) });
            }

            DaoNHBCriteriaAction<List<MEPTSampleForm>, MEPTSampleForm> action = new DaoNHBCriteriaAction<List<MEPTSampleForm>, MEPTSampleForm>(dic);
            IList<MEPTSampleForm> list = base.HibernateTemplate.Execute<List<MEPTSampleForm>>(action).Distinct().ToList();

            return list;
        }

        public EntityList<MEPTSampleForm> SearchMEPTSampleFormByGMGroupID(string startDate ,string endDate, long gmGroupID, int start, int count, string order)
        {
            EntityList<MEPTSampleForm> list = new EntityList<MEPTSampleForm>();
            startDate = StringPlus.StrConvertDataBaseStr(startDate,false);
            endDate = StringPlus.StrConvertDataBaseStr(endDate, false);
            string strHQL = "from MEPTSampleForm meptsampleform " +
                            " left join meptsampleform.BSampleStatus bsamplestatus " +
                            " left join bsamplestatus.BSampleStatusType bsamplestatustype "+
                            " left join meptsampleform.MEPTSampleItemList meptsampleitem " +
                            " left join meptsampleitem.ItemAllItem itemallitem " +
                            " left join itemallitem.GMGroupItemList gmgroupitem " +
                            " left join gmgroupitem.GMGroup gmgroup "+ 
                            " where meptsampleform.DataAddTime>=" + startDate +
                            " and meptsampleform.DataAddTime<=" + endDate +
                            " and gmgroup.Id=" + gmGroupID.ToString() +
                            " and (bsamplestatustype.Level is null or bsamplestatustype.Level<130)";                
            //获取样本单信息
            string strTempHQL = "select distinct meptsampleform " + strHQL;
            if (order != null && order.Trim().Length > 0)
            {
                strTempHQL += " order by " + order;
            }
            //获取样本单总数
            string strCount = "select count(distinct meptsampleform.Id) " + strHQL;

            DaoNHBSearchByHqlAction<List<MEPTSampleForm>, MEPTSampleForm> action = new DaoNHBSearchByHqlAction<List<MEPTSampleForm>, MEPTSampleForm>(strTempHQL, start, count);
            DaoNHBGetCountByHqlAction<int> actionCount = new DaoNHBGetCountByHqlAction<int>(strCount);
           
            list.list = this.HibernateTemplate.Execute<List<MEPTSampleForm>>(action);
            list.count = this.HibernateTemplate.Execute<int>(actionCount);    
            return list;
        }
        public override bool Delete(long id)
        {
            int result = 0;
            result = this.HibernateTemplate.Delete(" From MEPTSampleItem meptsampleitem where meptsampleitem.SampleFrom.Id = " + id);
            result = this.HibernateTemplate.Delete(" From MEPTSampleForm meptsampleform where meptsampleform.Id = " + id);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return true;
            }

        }
        #endregion
    } 
}