using System.Collections.Generic;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IDAO.NHB.BloodTransfusion;

namespace ZhiFang.DAO.NHB.BloodTransfusion
{
    public class BloodstyleDao : BaseDaoNHBServiceByInt<Bloodstyle, int>, IDBloodstyleDao
    {
        public IList<Bloodstyle> SearchBloodstyleListByJoinHql(string where, string bloodclassHql, string sort, int page, int limit)
        {
            IList<Bloodstyle> entityList = new List<Bloodstyle>();
            StringBuilder sqlHql = new StringBuilder();
            //sqlHql.Append(" select new Bloodstyle(bloodstyle,bloodclass,bloodunit) from Bloodstyle bloodstyle,BloodClass bloodclass,BloodUnit bloodunit where bloodstyle.BCNo=bloodclass.Id and bloodstyle.BloodUnitNo=bloodunit.Id  ");
            sqlHql.Append(" select new Bloodstyle(bloodstyle,bloodclass,bloodunit) from Bloodstyle bloodstyle");
            sqlHql.Append(" left join fetch bloodstyle.BloodClass bloodclass ");
            sqlHql.Append(" left join fetch bloodstyle.BloodUnit bloodunit ");

            if (!string.IsNullOrEmpty(bloodclassHql))
            {
                sqlHql.Append(" and " + bloodclassHql);
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
            GetDataRowRoleHQLString();//"Bloodstyle"
            string hql = sqlHql.Append(" and " + DataRowRoleHQLString).ToString();
            hql = FilterMacroCommand(hql);//宏命令过滤

            DaoNHBSearchByHqlAction<List<Bloodstyle>, Bloodstyle> action = new DaoNHBSearchByHqlAction<List<Bloodstyle>, Bloodstyle>(hql, start1, count1);
            var list = this.HibernateTemplate.Execute<List<Bloodstyle>>(action);

            if (list != null)
            {
                entityList = list;
            }

            return entityList;
        }
        public EntityList<Bloodstyle> SearchBloodstyleEntityListByJoinHql(string where, string bloodclassHql, string sort, int page, int limit)
        {
            EntityList<Bloodstyle> entityList = new EntityList<Bloodstyle>();
            entityList.list = new List<Bloodstyle>();

            var list = SearchBloodstyleListByJoinHql(where, bloodclassHql, sort, page, limit);
            if (list != null)
            {
                entityList.list = list;
            }
            else
            {
                return entityList;
            }

            StringBuilder sqlHql = new StringBuilder();
            //sqlHql.Append(" select count(DISTINCT bloodstyle.Id) from Bloodstyle bloodstyle,BloodClass bloodclass,BloodUnit bloodunit where bloodstyle.BCNo=bloodclass.Id and bloodstyle.BloodUnitNo=bloodunit.Id ");

            sqlHql.Append(" select count(DISTINCT bloodstyle.Id) from Bloodstyle bloodstyle");
            sqlHql.Append(" left join bloodstyle.BloodClass bloodclass ");
            sqlHql.Append(" left join bloodstyle.BloodUnit bloodunit ");

            if (!string.IsNullOrEmpty(bloodclassHql))
            {
                sqlHql.Append(" and " + bloodclassHql);
            }
            if (!string.IsNullOrEmpty(where))
            {
                sqlHql.Append(" and " + where);
            }
            GetDataRowRoleHQLString();//"Bloodstyle"
            string hql = sqlHql.Append(" and " + DataRowRoleHQLString).ToString();
            hql = FilterMacroCommand(hql);//宏命令过滤

            DaoNHBGetCountByHqlAction<Bloodstyle> actionCount = new DaoNHBGetCountByHqlAction<Bloodstyle>(hql);
            entityList.count = this.HibernateTemplate.Execute<int>(actionCount);
            return entityList;
        }
    }
}