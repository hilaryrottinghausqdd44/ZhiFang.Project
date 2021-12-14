using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.IDAO.RBAC;

namespace ZhiFang.DAO.NHB.RBAC
{
    public class PUserDao : BaseDaoNHBServiceByInt<PUser, int>, IDPUserDao
    {
        public int GetMaxId()
        {
            string id = "";
            string strHQL = string.Format(" select Max(Id) from PUser puser where 1=1");
            DaoNHBGetStringMaxByHqlAction<string> action = new DaoNHBGetStringMaxByHqlAction<string>(strHQL);
            id = this.HibernateTemplate.Execute<string>(action);
            int id2 = -1;
            if (!string.IsNullOrEmpty(id))
                int.TryParse(id, out id2);
            return id2;
        }
    }
}