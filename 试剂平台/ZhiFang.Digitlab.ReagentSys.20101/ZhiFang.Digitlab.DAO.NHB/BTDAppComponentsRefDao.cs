using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IDAO;

namespace ZhiFang.Digitlab.DAO.NHB
{
    public class BTDAppComponentsRefDao : BaseDaoNHB<BTDAppComponentsRef, long>, IDBTDAppComponentsRefDao
    {
        public override void Flush()
        {
            try
            {
                this.HibernateTemplate.Delete(" From BTDAppComponentsRef btdacr where btdacr.BTDAppComponents=null  or btdacr.RefAppComID=null ");
            }
            catch (Exception ex)
            { 
            
            }
        }
    }
}