using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.OA;
using ZhiFang.IDAO.OA;

namespace ZhiFang.DAO.NHB.OA
{	
	public class AHServerLicenceDao : BaseDaoNHB<AHServerLicence, long>, IDAHServerLicenceDao
	{
        // <summary>
        /// 获取服务器授权需要特批的数据
        /// </summary>
        /// <param name="where"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public EntityList<AHServerLicence> SearchSpecialApprovalAHServerLicenceByHQL(string where, int page, int limit, string sort)
        {
            EntityList<AHServerLicence> list = new EntityList<AHServerLicence>();
            string hqlTemp = "";
            string strHQL = "from AHServerLicence ahserverlicence ";
            StringBuilder strlistHQL = new StringBuilder();
            strlistHQL.Append(strHQL);
            if (String.IsNullOrEmpty(hqlTemp))
            {
                hqlTemp = where;
            }
            else if (!String.IsNullOrEmpty(where) && !String.IsNullOrEmpty(hqlTemp))
            {
                hqlTemp = hqlTemp + " and " + where;
            }

            if (!string.IsNullOrEmpty(hqlTemp))
            {
                strlistHQL.Append(" where " + hqlTemp);
            }
            if (sort != null && sort.Trim().Length > 0)
            {
                strlistHQL.Append(" order by " + sort);
            }

            StringBuilder strCountHQL = new StringBuilder();
            strCountHQL.Append("select count(distinct ahserverlicence.Id) " + strHQL);
            if (!string.IsNullOrEmpty(where))
            {
                strCountHQL.Append(" where " + hqlTemp);
            }
            DaoNHBSearchByHqlAction<List<AHServerLicence>, AHServerLicence> action = new DaoNHBSearchByHqlAction<List<AHServerLicence>, AHServerLicence>(strlistHQL.ToString(), page, limit);
            DaoNHBGetCountByHqlAction<int> actionCount = new DaoNHBGetCountByHqlAction<int>(strCountHQL.ToString());

            list.list = this.HibernateTemplate.Execute<List<AHServerLicence>>(action);
            list.count = this.HibernateTemplate.Execute<int>(actionCount);
            return list;
        }

        public EntityList<AHServerLicence> SearchListByDocAndDtlHQL_LicenceInfo(string where, string dtlWhere, int page, int limit, string sort)
        {
            EntityList<AHServerLicence> entityList = new EntityList<AHServerLicence>();
            entityList.list=new List<AHServerLicence>();

            StringBuilder listHql = new StringBuilder();
            string strHQL = " select DISTINCT ahserverlicence from AHServerLicence ahserverlicence,AHServerEquipLicence ahserverequiplicence where ahserverequiplicence.ServerLicenceID=ahserverlicence.Id ";
            listHql.Append(strHQL);
            if (!string.IsNullOrEmpty(where))
            {
                listHql.Append(" and " + where);
            }
            if (!string.IsNullOrEmpty(dtlWhere))
            {
                listHql.Append(" and " + dtlWhere);
            }
            if (!string.IsNullOrEmpty(sort))
                listHql.Append(" order by " + sort);

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

            DaoNHBSearchByHqlAction<List<AHServerLicence>, AHServerLicence> action = new DaoNHBSearchByHqlAction<List<AHServerLicence>, AHServerLicence>(listHql.ToString(), page, limit);
            var list2 = this.HibernateTemplate.Execute<List<AHServerLicence>>(action);
            if (list2 != null)
            {
                entityList.list = list2;
            }
            if (entityList.list.Count<=0) {
                entityList.count = 0;
                return entityList;
            }

            StringBuilder strCountHQL = new StringBuilder();
            strCountHQL.Append("select count(distinct ahserverlicence.Id) from AHServerLicence ahserverlicence,AHServerEquipLicence ahserverequiplicence where ahserverequiplicence.ServerLicenceID=ahserverlicence.Id ");
            if (!string.IsNullOrEmpty(where))
            {
                strCountHQL.Append(" and " + where);
            }
            if (!string.IsNullOrEmpty(dtlWhere))
            {
                strCountHQL.Append(" and " + dtlWhere);
            }
            DaoNHBGetCountByHqlAction<int> actionCount = new DaoNHBGetCountByHqlAction<int>(strCountHQL.ToString());
            entityList.count = this.HibernateTemplate.Execute<int>(actionCount);

            return entityList;
        }
    } 
}