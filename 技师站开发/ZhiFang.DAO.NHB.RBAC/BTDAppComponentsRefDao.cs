using ZhiFang.Entity.RBAC;
using ZhiFang.IDAO.RBAC;

namespace ZhiFang.DAO.NHB.RBAC
{
    public class BTDAppComponentsRefDao : Base.BaseDaoNHBService<BTDAppComponentsRef, long>, IDBTDAppComponentsRefDao
    {
        public override void Flush()
        {
            try
            {
                this.HibernateTemplate.Delete(" From BTDAppComponentsRef btdacr where btdacr.BTDAppComponents=null  or btdacr.RefAppComID=null ");
            }
            catch
            {

            }
        }
    }
}