using System.Collections.Generic;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.SA;
using ZhiFang.IDAO.NHB.SA;

namespace ZhiFang.DAO.NHB.SA
{
    public class CenOrgDao : BaseDaoNHBService<CenOrg, long>, IDCenOrgDao
    {
        /// <summary>
        /// 获取CenOrg时,默认不添加按LabID的过滤条件
        /// </summary>
        public IList<CenOrg> GetCenOrgOfNoLabIDList(string hqlWhere)
        {
            string strHQL = "select cenorg from CenOrg cenorg where 1=1 ";
            if (hqlWhere != null && hqlWhere.Length > 0)
            {
                strHQL += "and " + hqlWhere;
            }
            IList<CenOrg> cenOrgList = this.HibernateTemplate.Find<CenOrg>(strHQL);
            return cenOrgList;
        }
    }
}