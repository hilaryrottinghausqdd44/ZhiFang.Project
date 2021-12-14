using System;
using System.Collections.Generic;
using ZhiFang.Entity.SA;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.NHB.SA
{
    public interface IDCenOrgDao : IDBaseDao<CenOrg, long>
    {
        /// <summary>
        /// 获取CenOrg时,默认不添加按LabID的过滤条件
        /// </summary>
        IList<CenOrg> GetCenOrgOfNoLabIDList(string hqlWhere);
    }
}
