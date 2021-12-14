using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.NHB.ReagentSys.Client
{
    public interface IDCenOrgDao : IDBaseDao<CenOrg, long>
    {
        /// <summary>
        /// 获取CenOrg时,默认不添加按LabID的过滤条件
        /// </summary>
        IList<CenOrg> GetCenOrgOfNoLabIDList(string hqlWhere);
    }
}
