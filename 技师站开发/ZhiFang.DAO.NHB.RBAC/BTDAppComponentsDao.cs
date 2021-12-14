using ZhiFang.Entity.RBAC;
using ZhiFang.IDAO.RBAC;

namespace ZhiFang.DAO.NHB.RBAC
{
    public class BTDAppComponentsDao : Base.BaseDaoNHBService<BTDAppComponents, long>, IDBTDAppComponentsDao
    {
        public override bool Update(BTDAppComponents entity)
        {
            //DeleteByHql("From BTDAppComponentsRef a where a.BTDAppComponents.Id = " + entity.Id);
            this.HibernateTemplate.Update(entity);
            return true;
        }

    }
}