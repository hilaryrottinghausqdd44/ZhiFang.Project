using ZhiFang.BLL.Base;
using ZhiFang.Entity.WebAssist;
using ZhiFang.IBLL.WebAssist;
using ZhiFang.Entity.Base;
using System.Collections.Generic;

using ZhiFang.IBLL.RBAC;

namespace ZhiFang.BLL.WebAssist
{
    /// <summary>
    ///
    /// </summary>
    public class BCenOrg : BaseBLL<CenOrg>, IBCenOrg
    {

        private int GetMaxOrgNo()
        {
            IList<int> list = this.DBDao.Find<int>("select max(cenorg.OrgNo) as OrgNo  from CenOrg cenorg where 1=1 ");
            if (list != null && list.Count > 0)
            {
                int maxOrgNo = list[0];
                maxOrgNo = maxOrgNo < 100002 ? 100002 : ++maxOrgNo;
                //if (maxOrgNo < 100001)
                //    maxOrgNo = 100001;
                //else
                //    maxOrgNo++;
                return maxOrgNo;
            }
            else
                return 0;
        }

    }
}