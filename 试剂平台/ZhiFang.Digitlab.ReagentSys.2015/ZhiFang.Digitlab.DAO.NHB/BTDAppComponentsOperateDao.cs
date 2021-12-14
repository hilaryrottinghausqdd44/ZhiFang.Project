using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IDAO;

namespace ZhiFang.Digitlab.DAO.NHB
{
    public class BTDAppComponentsOperateDao : BaseDaoNHB<BTDAppComponentsOperate, long>, IDBTDAppComponentsOperateDao
    {
        public override void Flush()
        {
            try
            {
                IList<BTDAppComponentsOperate> ilistappco = this.HibernateTemplate.Find<BTDAppComponentsOperate>(" From BTDAppComponentsOperate btdaco where btdaco.BTDAppComponents=null ");
                foreach (var a in ilistappco)
                {
                    IList<RBACModuleOper> ilistrbacmoduleoper = this.HibernateTemplate.Find<RBACModuleOper>(" select rbacmoduleoper from RBACModuleOper rbacmoduleoper where rbacmoduleoper.BTDAppComponentsOperate.Id=" + a.Id);
                    if (ilistrbacmoduleoper == null || ilistrbacmoduleoper.Count<=0)
                    {
                        base.Delete(a);
                    }
                }
                //this.HibernateTemplate.q.Delete(" From BTDAppComponentsOperate btdaco where btdaco.BTDAppComponents=null ");
                //and btdaco.RBACModuleOperList=null  ");
            }
            catch (Exception ex)
            {

            }
        }
    }
}
