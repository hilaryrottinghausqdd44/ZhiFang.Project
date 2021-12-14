using System;
using System.Collections.Generic;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using System.Linq;

namespace ZhiFang.DAO.NHB.ReagentSys.Client
{
    public class ReaCenOrgDao : BaseDaoNHB<ReaCenOrg, long>, IDReaCenOrgDao
    {
        /// <summary>
        /// 获取机构编号最大值(不能添加LabId作过滤条件)
        /// </summary>
        /// <returns></returns>
        public int GetMaxOrgNo()
        {
            try
            {
                DaoNHBGetMaxByHqlAction<ReaCenOrg> action = new DaoNHBGetMaxByHqlAction<ReaCenOrg>("select max(reacenorg.OrgNo) as OrgNo  from ReaCenOrg reacenorg where 1=1 ");
                long orgNo = this.HibernateTemplate.Execute(action);
                int maxOrgNo = (int)orgNo;
                maxOrgNo = maxOrgNo + 1;
                return maxOrgNo;
            }
            catch (Exception ee)
            {
                string strHQL = "select reacenorg from ReaCenOrg reacenorg where 1=1 ";
                IList<ReaCenOrg> tempList = this.HibernateTemplate.Find<ReaCenOrg>(strHQL);
                int orgNo = tempList.Max(p => p.OrgNo.Value);
                return (orgNo + 1);
            }
        }
    }
}