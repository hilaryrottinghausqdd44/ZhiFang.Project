using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IDAO;

namespace ZhiFang.Digitlab.DAO.NHB
{	
	public class EPModuleDao : BaseDaoNHB<EPModule, long>, IDEPModuleDao
	{
        #region IDEPModuleDao 成员

        public IList<EPModule> SearchEPModuleByEquipID(long longEquipID)
        {
            string strHQL = "from EPModule epmodule where epmodule.EPBEquip.Id=:EquipID";
            return this.HibernateTemplate.FindByNamedParam<EPModule>(strHQL, "EquipID", longEquipID);
        }

        #endregion
    } 
}