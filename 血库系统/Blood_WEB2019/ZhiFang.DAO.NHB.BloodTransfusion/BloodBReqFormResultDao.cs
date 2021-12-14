using System.Collections.Generic;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IDAO.NHB.BloodTransfusion;

namespace ZhiFang.DAO.NHB.BloodTransfusion
{	
	public class BloodBReqFormResultDao : BaseDaoNHBService<BloodBReqFormResult, long>, IDBloodBReqFormResultDao
	{
        public IList<BloodBReqFormResult> SearchBloodBReqFormResultListByJoinHql(string where, string bloodbtestitemHql, string sort, int page, int limit)
        {
            IList<BloodBReqFormResult> entityList = new List<BloodBReqFormResult>();
            StringBuilder sqlHql = new StringBuilder();

            sqlHql.Append(" select new BloodBReqFormResult(bloodbreqformresult,bloodbtestitem) from BloodBReqFormResult bloodbreqformresult,BloodBTestItem bloodbtestitem  where bloodbreqformresult.BTestItemNo=bloodbtestitem.Id ");

            if (!string.IsNullOrEmpty(bloodbtestitemHql))
            {
                sqlHql.Append(" and " + bloodbtestitemHql);
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
            GetDataRowRoleHQLString();//"BloodBReqFormResult"
            string hql = sqlHql.Append(" and " + DataRowRoleHQLString).ToString();
            hql = FilterMacroCommand(hql);//宏命令过滤

            DaoNHBSearchByHqlAction<List<BloodBReqFormResult>, BloodBReqFormResult> action = new DaoNHBSearchByHqlAction<List<BloodBReqFormResult>, BloodBReqFormResult>(hql, start1, count1);
            var list = this.HibernateTemplate.Execute<List<BloodBReqFormResult>>(action);

            if (list != null)
            {
                entityList = list;
            }

            return entityList;
        }
        public EntityList<BloodBReqFormResult> SearchBloodBReqFormResultEntityListByJoinHql(string where, string bloodbtestitemHql, string sort, int page, int limit)
        {
            EntityList<BloodBReqFormResult> entityList = new EntityList<BloodBReqFormResult>();
            entityList.list = new List<BloodBReqFormResult>();

            var list = SearchBloodBReqFormResultListByJoinHql(where, bloodbtestitemHql, sort, page, limit);
            if (list != null)
            {
                entityList.list = list;
            }
            else
            {
                return entityList;
            }

            StringBuilder sqlHql = new StringBuilder();
            sqlHql.Append(" select count(DISTINCT bloodbreqformresult.Id) from BloodBReqFormResult bloodbreqformresult,BloodBTestItem bloodbtestitem  where bloodbreqformresult.BTestItemNo=bloodbtestitem.Id");

            if (!string.IsNullOrEmpty(bloodbtestitemHql))
            {
                sqlHql.Append(" and " + bloodbtestitemHql);
            }
            if (!string.IsNullOrEmpty(where))
            {
                sqlHql.Append(" and " + where);
            }
            GetDataRowRoleHQLString();//"BloodBReqFormResult"
            string hql = sqlHql.Append(" and " + DataRowRoleHQLString).ToString();
            hql = FilterMacroCommand(hql);//宏命令过滤

            DaoNHBGetCountByHqlAction<BloodBReqFormResult> actionCount = new DaoNHBGetCountByHqlAction<BloodBReqFormResult>(hql);
            entityList.count = this.HibernateTemplate.Execute<int>(actionCount);
            return entityList;
        }
    } 
}