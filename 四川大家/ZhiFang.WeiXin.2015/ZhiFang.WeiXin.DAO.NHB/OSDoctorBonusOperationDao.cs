using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.WeiXin.Entity;
using ZhiFang.WeiXin.IDAO;

namespace  ZhiFang.WeiXin.DAO.NHB
{	
	public class OSDoctorBonusOperationDao : BaseDaoNHB<OSDoctorBonusOperation, long>, IDOSDoctorBonusOperationDao
	{
        public int DeleteByBobjectID(long bobjectID)
        {
            int result = 0;
            result = this.DeleteByHql("FROM OSDoctorBonusOperation  where BobjectID=" + bobjectID);
            return result;
        }
    } 
}