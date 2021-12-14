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
    public class AHSingleLicenceDao : BaseDaoNHB<AHSingleLicence, long>, IDAHSingleLicenceDao
    {
        /// <summary>
        /// 获取单站点授权需要特批的数据
        /// </summary>
        /// <param name="where"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public EntityList<AHSingleLicence> SearchSpecialApprovalAHSingleLicenceByHQL(string where, int page, int limit, string sort)
        {
            EntityList<AHSingleLicence> list = new EntityList<AHSingleLicence>();
            string hqlTemp = "";
            string strHQL = "from AHSingleLicence ahsinglelicence ";
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
            strCountHQL.Append("select count(distinct ahsinglelicence.Id) " + strHQL);
            if (!string.IsNullOrEmpty(where))
            {
                strCountHQL.Append(" where " + hqlTemp);
            }
            
            DaoNHBSearchByHqlAction<List<AHSingleLicence>, AHSingleLicence> action = new DaoNHBSearchByHqlAction<List<AHSingleLicence>, AHSingleLicence>(strlistHQL.ToString(), page, limit);
            DaoNHBGetCountByHqlAction<int> actionCount = new DaoNHBGetCountByHqlAction<int>(strCountHQL.ToString());

            list.list = this.HibernateTemplate.Execute<List<AHSingleLicence>>(action);
            list.count = this.HibernateTemplate.Execute<int>(actionCount);
            return list;
        }
    }
}