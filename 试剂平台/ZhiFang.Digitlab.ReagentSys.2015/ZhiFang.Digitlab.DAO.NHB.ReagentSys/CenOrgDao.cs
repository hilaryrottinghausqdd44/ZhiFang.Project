using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IDAO;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Digitlab.IDAO.ReagentSys;

namespace ZhiFang.Digitlab.DAO.NHB.ReagentSys
{	
	public class CenOrgDao : BaseDaoNHB<CenOrg, long>, IDCenOrgDao
	{
        public int GetMaxOrgNoDao(long orgTypeID, int minOrgNo)
        {
            object tempObject = this.Session.CreateQuery("select max(cenorg.OrgNo) as OrgNo from CenOrg cenorg " +
                " left join  cenorg.CenOrgType ct where 1=1 and ct.Id=" + orgTypeID.ToString()).UniqueResult();
            if (tempObject != null)
            {
                int maxOrgNo = (int)tempObject;
                maxOrgNo = maxOrgNo < minOrgNo ? minOrgNo : ++maxOrgNo;
                if (minOrgNo > 0)
                    return maxOrgNo;
                else
                    return minOrgNo;
            }
            else
                return minOrgNo;
        }
	} 
}