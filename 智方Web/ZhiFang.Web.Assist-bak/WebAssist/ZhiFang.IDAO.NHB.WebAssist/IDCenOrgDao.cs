using System;
using System.Collections.Generic;
using ZhiFang.Entity.WebAssist;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.NHB.WebAssist
{
    public interface IDCenOrgDao : IDBaseDao<CenOrg, long>
    {
        /// <summary>
        /// 获取CenOrg时,默认不添加按LabID的过滤条件
        /// </summary>
        IList<CenOrg> GetCenOrgOfNoLabIDList(string hqlWhere);
    }
}
