using System.Collections.Generic;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IDAO.NHB.BloodTransfusion;

namespace ZhiFang.DAO.NHB.BloodTransfusion
{	
	public class BloodBReqItemResultDao : BaseDaoNHBService<BloodBReqItemResult, long>, IDBloodBReqItemResultDao
	{
        public IList<BloodBReqItemResult> SearchBloodBReqItemResultListByJoinHql(string where, string bloodbtestitemHql, string sort, int page, int limit)
        {
            IList<BloodBReqItemResult> entityList = new List<BloodBReqItemResult>();
            StringBuilder sqlHql = new StringBuilder();

            sqlHql.Append(" select new BloodBReqItemResult(bloodbreqitemresult,bloodbtestitem) from BloodBReqItemResult bloodbreqitemresult,BloodBTestItem bloodbtestitem  where bloodbreqitemresult.BTestItemNo=bloodbtestitem.Id ");

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
            GetDataRowRoleHQLString();//"BloodBReqItemResult"
            string hql = sqlHql.Append(" and " + DataRowRoleHQLString).ToString();
            hql = FilterMacroCommand(hql);//宏命令过滤

            DaoNHBSearchByHqlAction<List<BloodBReqItemResult>, BloodBReqItemResult> action = new DaoNHBSearchByHqlAction<List<BloodBReqItemResult>, BloodBReqItemResult>(hql, start1, count1);
            var list = this.HibernateTemplate.Execute<List<BloodBReqItemResult>>(action);

            if (list != null)
            {
                entityList = list;
            }

            return entityList;
        }
        public EntityList<BloodBReqItemResult> SearchBloodBReqItemResultEntityListByJoinHql(string where, string bloodbtestitemHql, string sort, int page, int limit)
        {
            EntityList<BloodBReqItemResult> entityList = new EntityList<BloodBReqItemResult>();
            entityList.list = new List<BloodBReqItemResult>();

            var list = SearchBloodBReqItemResultListByJoinHql(where, bloodbtestitemHql, sort, page, limit);
            if (list != null)
            {
                entityList.list = list;
            }
            else
            {
                return entityList;
            }

            StringBuilder sqlHql = new StringBuilder();
            sqlHql.Append(" select count(DISTINCT bloodbreqitemresult.Id) from BloodBReqItemResult bloodbreqitemresult,BloodBTestItem bloodbtestitem  where bloodbreqitemresult.BTestItemNo=bloodbtestitem.Id");

            if (!string.IsNullOrEmpty(bloodbtestitemHql))
            {
                sqlHql.Append(" and " + bloodbtestitemHql);
            }
            if (!string.IsNullOrEmpty(where))
            {
                sqlHql.Append(" and " + where);
            }
            GetDataRowRoleHQLString();//"BloodBReqItemResult"
            string hql = sqlHql.Append(" and " + DataRowRoleHQLString).ToString();
            hql = FilterMacroCommand(hql);//宏命令过滤

            DaoNHBGetCountByHqlAction<BloodBReqItemResult> actionCount = new DaoNHBGetCountByHqlAction<BloodBReqItemResult>(hql);
            entityList.count = this.HibernateTemplate.Execute<int>(actionCount);
            return entityList;
        }
    } 
}