using System.Collections.Generic;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IDAO.NHB.BloodTransfusion;

namespace ZhiFang.DAO.NHB.BloodTransfusion
{
    public class BloodBReqItemDao : BaseDaoNHBService<BloodBReqItem, long>, IDBloodBReqItemDao
    {
        public IList<BloodBReqItem> SearchBloodBReqItemListByJoinHql(string where, string docHql, string bloodstyleHql, string sort, int page, int limit)
        {
            IList<BloodBReqItem> entityList = new List<BloodBReqItem>();
            StringBuilder sqlHql = new StringBuilder();

            sqlHql.Append(" select ");
            if (!string.IsNullOrEmpty(docHql) && !string.IsNullOrEmpty(bloodstyleHql))
            {
                sqlHql.Append(" new BloodBReqItem(bloodbreqform,bloodbreqitem, bloodclass,bloodstyle,bloodunit) from BloodBReqForm bloodbreqform, BloodBReqItem bloodbreqitem, Bloodstyle bloodstyle, BloodClass bloodclass, BloodUnit bloodunit where bloodbreqitem.BReqFormID=bloodbreqform.Id and bloodbreqitem.BloodNo =bloodstyle.Id and bloodbreqitem.BCNo=bloodclass.Id and bloodbreqitem.BloodUnitNo=bloodunit.Id ");
            }
            else
            {
                sqlHql.Append(" new BloodBReqItem(bloodbreqitem,bloodclass,bloodstyle,bloodunit) from BloodBReqItem bloodbreqitem, Bloodstyle bloodstyle, BloodClass bloodclass, BloodUnit bloodunit where bloodbreqitem.BloodNo =bloodstyle.Id and bloodbreqitem.BCNo=bloodclass.Id and bloodbreqitem.BloodUnitNo=bloodunit.Id ");
            }
            if (!string.IsNullOrEmpty(docHql))
            {
                sqlHql.Append(" and " + docHql);
            }
            if (!string.IsNullOrEmpty(bloodstyleHql))
            {
                sqlHql.Append(" and " + bloodstyleHql);
            }
            if (!string.IsNullOrEmpty(where))
            {
                sqlHql.Append(" and " + where);
            }
            if (!string.IsNullOrEmpty(sort))
                sqlHql.Append(" order by " + sort);
            int? start1 = null;
            int? count1 = null;
            if (page > 0)
            {
                start1 = page;
            }
            if (limit > 0)
            {
                count1 = limit;
            }
            GetDataRowRoleHQLString();//"BloodBReqItem"
            string hql = sqlHql.Append(" and " + DataRowRoleHQLString).ToString();
            hql = FilterMacroCommand(hql);//宏命令过滤
            DaoNHBSearchByHqlAction<List<BloodBReqItem>, BloodBReqItem> action = new DaoNHBSearchByHqlAction<List<BloodBReqItem>, BloodBReqItem>(hql, start1, count1);
            var list = this.HibernateTemplate.Execute<List<BloodBReqItem>>(action);

            if (list != null)
            {
                entityList = list;
            }

            return entityList;
        }
        public EntityList<BloodBReqItem> SearchBloodBReqItemEntityListByJoinHql(string where, string docHql, string bloodstyleHql, string sort, int page, int limit)
        {
            EntityList<BloodBReqItem> entityList = new EntityList<BloodBReqItem>();
            entityList.list = new List<BloodBReqItem>();

            var list = SearchBloodBReqItemListByJoinHql(where, docHql, bloodstyleHql, sort, page, limit);
            if (list != null)
            {
                entityList.list = list;
            }
            else
            {
                return entityList;
            }

            StringBuilder sqlHql = new StringBuilder();
            sqlHql.Append(" select count(DISTINCT bloodbreqitem.Id) ");
            if (!string.IsNullOrEmpty(docHql) && !string.IsNullOrEmpty(bloodstyleHql))
            {
                sqlHql.Append(" from BloodBReqForm bloodbreqform, BloodBReqItem bloodbreqitem, Bloodstyle bloodstyle, BloodClass bloodclass, BloodUnit bloodunit where bloodbreqitem.BReqFormID=bloodbreqform.Id and bloodbreqitem.BloodNo =bloodstyle.Id and bloodbreqitem.BCNo=bloodclass.Id and bloodbreqitem.BloodUnitNo=bloodunit.Id ");
            }
            else
            {
                sqlHql.Append(" from BloodBReqItem bloodbreqitem, Bloodstyle bloodstyle, BloodClass bloodclass, BloodUnit bloodunit where bloodbreqitem.BloodNo =bloodstyle.Id and bloodbreqitem.BCNo=bloodclass.Id and bloodbreqitem.BloodUnitNo=bloodunit.Id ");
            }
            if (!string.IsNullOrEmpty(docHql))
            {
                sqlHql.Append(" and " + docHql);
            }
            if (!string.IsNullOrEmpty(bloodstyleHql))
            {
                sqlHql.Append(" and " + bloodstyleHql);
            }
            if (!string.IsNullOrEmpty(where))
            {
                sqlHql.Append(" and " + where);
            }
            GetDataRowRoleHQLString();//"BloodBReqItem"
            string hql = sqlHql.Append(" and " + DataRowRoleHQLString).ToString();
            hql = FilterMacroCommand(hql);//宏命令过滤

            DaoNHBGetCountByHqlAction<BloodBReqItem> actionCount = new DaoNHBGetCountByHqlAction<BloodBReqItem>(hql);
            entityList.count = this.HibernateTemplate.Execute<int>(actionCount);
            return entityList;
        }
    }
}