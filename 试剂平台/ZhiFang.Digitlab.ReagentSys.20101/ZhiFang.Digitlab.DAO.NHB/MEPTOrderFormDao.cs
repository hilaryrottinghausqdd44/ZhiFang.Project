using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IDAO;

namespace ZhiFang.Digitlab.DAO.NHB
{	
	public class MEPTOrderFormDao : BaseDaoNHB<MEPTOrderForm, long>, IDMEPTOrderFormDao
	{
        public override bool Delete(long id)
        {
            int result = 0;
            result = this.HibernateTemplate.Delete(" From MEPTOrderItem moi where moi.MEPTOrderForm.Id = " + id);
            this.HibernateTemplate.Delete(Get(id));
            if (result > 0)
            {
                return true;
            }
            else
            {
                return true;
            }
        }
	} 
}