using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IDAO;

namespace ZhiFang.Digitlab.DAO.NHB
{	
	public class BTDAppComponentsDao : BaseDaoNHB<BTDAppComponents, long>, IDBTDAppComponentsDao
	{
        public override bool Update(BTDAppComponents entity)
        {
            //DeleteByHql("From BTDAppComponentsRef a where a.BTDAppComponents.Id = " + entity.Id);
            this.HibernateTemplate.Update(entity);
            return true;
        }

	} 
}