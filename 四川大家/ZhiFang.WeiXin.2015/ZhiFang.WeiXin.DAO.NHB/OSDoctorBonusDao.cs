using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.WeiXin.Entity;
using ZhiFang.WeiXin.IDAO;

namespace  ZhiFang.WeiXin.DAO.NHB
{	
	public class OSDoctorBonusDao : BaseDaoNHB<OSDoctorBonus, long>, IDOSDoctorBonusDao
	{
        public int DeleteByOSDoctorBonusFormID(long longOSDoctorBonusFormID)
        {
            int result = 0;
            result = this.DeleteByHql("FROM OSDoctorBonus  where OSDoctorBonusFormID=" + longOSDoctorBonusFormID);
            return result;
        }
    } 
}