using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.IDAO.ProjectProgressMonitorManage;

namespace ZhiFang.DAO.NHB.ProjectProgressMonitorManage
{
    public class PClientDao : BaseDaoNHB<PClient, long>, IDPClientDao
    {
        public long GetNextMaxClientNo()
        {
            long clientNo = 0;
            string hql = "select max(pclient.ClientNo) as ClientNo from PClient pclient";
            DaoNHBGetMaxByHqlAction<long> actionClientNo = new DaoNHBGetMaxByHqlAction<long>(hql.ToString());
            clientNo = this.HibernateTemplate.Execute<long>(actionClientNo);
            if (clientNo > 0) clientNo = clientNo + 1;
            return clientNo;
        }
        public long GetNextMaxLicenceCode()
        {
            long licencecode = 0;
            string hql = "select max(pclient.LicenceCode) as LicenceCode from PClient pclient";
            DaoNHBGetMaxByHqlAction<long> actionClientNo = new DaoNHBGetMaxByHqlAction<long>(hql.ToString());
            licencecode = this.HibernateTemplate.Execute<long>(actionClientNo);
            if (licencecode > 0) licencecode = licencecode + 1;
            return licencecode;
        }

    }
}