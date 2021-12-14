using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IDAO;

namespace ZhiFang.Digitlab.DAO.NHB
{	
	public class MEPTOrderItemDao : BaseDaoNHB<MEPTOrderItem, long>, IDMEPTOrderItemDao
	{
        public override void Flush()
        {
            try
            {
                this.HibernateTemplate.Delete(" From MEPTOrderItem meptoi where meptoi.MEPTOrderForm=null ");
            }
            catch (Exception ex)
            {

            }
        }
	} 
}