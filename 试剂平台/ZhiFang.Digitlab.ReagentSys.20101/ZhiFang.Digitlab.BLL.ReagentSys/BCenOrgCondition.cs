
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.IDAO;
using ZhiFang.Digitlab.Entity.ReagentSys;

namespace ZhiFang.Digitlab.BLL.ReagentSys
{
	/// <summary>
	///
	/// </summary>
	public  class BCenOrgCondition : BaseBLL<CenOrgCondition>, ZhiFang.Digitlab.IBLL.ReagentSys.IBCenOrgCondition
	{
        /// <summary>
        /// 验证上下级关系
        /// </summary>
        /// <param name="upperID"></param>
        /// <param name="lowerID"></param>
        /// <returns></returns>
        public bool ValidateUpperAndLowerLevel(long upperID, long lowerID)
        {
            bool flag = false;
            IList<CenOrgCondition> listCenOrgCondition = this.SearchListByHQL(" cenorgcondition.cenorg2.Id=" + lowerID.ToString());
            if (listCenOrgCondition != null && listCenOrgCondition.Count > 0)
            {
                IList<CenOrgCondition> list = listCenOrgCondition.Where(p => p.cenorg1.Id == upperID).ToList();
                flag = (list != null && list.Count > 0);
            }
            return flag;
        }
   
	}
}