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
    public class AHServerProgramLicenceDao : BaseDaoNHB<AHServerProgramLicence, long>, IDAHServerProgramLicenceDao
    {
        public int DeleteByStrId(string strId)
        {
            int result = 0;
            if (!String.IsNullOrEmpty(strId))
                result = this.DeleteByHql("FROM AHServerProgramLicence  where Id in(" + strId+")");
            return result;
        }
        /// <summary>
        /// 获取上一次的授权程序申请信息
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="Order"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public EntityList<AHServerProgramLicence> SearchPreListByHQL(string strHqlWhere, string order, int page, int count)
        {
            EntityList<AHServerProgramLicence> list = new EntityList<AHServerProgramLicence>();

            string strHQL = "from AHServerProgramLicence ahserverprogramlicence,AHServerLicence ahserverlicence ";
            string hqlTemp = "";
            StringBuilder strlistHQL = new StringBuilder();
            if (String.IsNullOrEmpty(hqlTemp))
            {
                hqlTemp = strHqlWhere;
            }
            else if (!String.IsNullOrEmpty(strHqlWhere) && !String.IsNullOrEmpty(hqlTemp))
            {
                hqlTemp = hqlTemp + " and " + strHqlWhere;
            }
            //获取信息
            strlistHQL.Append("select distinct ahserverprogramlicence " + strHQL);
            if (!string.IsNullOrEmpty(hqlTemp))
            {
                strlistHQL.Append(" where " + hqlTemp);
            }

            //获取信息总数
            StringBuilder strCountHQL = new StringBuilder();
            strCountHQL.Append("select count(distinct ahserverprogramlicence.Id) " + strHQL);
            if (!string.IsNullOrEmpty(hqlTemp))
            {
                strCountHQL.Append(" where " + hqlTemp);
            }

            DaoNHBSearchByHqlAction<List<AHServerProgramLicence>, AHServerProgramLicence> action = new DaoNHBSearchByHqlAction<List<AHServerProgramLicence>, AHServerProgramLicence>(strlistHQL.ToString(), page, count);
            DaoNHBGetCountByHqlAction<int> actionCount = new DaoNHBGetCountByHqlAction<int>(strCountHQL.ToString());

            list.list = this.HibernateTemplate.Execute<List<AHServerProgramLicence>>(action);
            list.count = this.HibernateTemplate.Execute<int>(actionCount);
            return list;
        }
    }
}