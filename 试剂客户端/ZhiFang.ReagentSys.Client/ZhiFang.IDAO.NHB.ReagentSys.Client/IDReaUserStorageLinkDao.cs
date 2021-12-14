using ZhiFang.IDAO.Base;
using ZhiFang.Entity.ReagentSys.Client;
using System.Collections.Generic;

namespace ZhiFang.IDAO.NHB.ReagentSys.Client
{
    public interface IDReaUserStorageLinkDao : IDBaseDao<ReaUserStorageLink, long>
    {
        /// <summary>
        /// 依传入的条件获取库房人员权限关系的所属库房ID集合信息
        /// </summary>
        /// <param name="hqlWhere"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        IList<long> SearchStorageIDListByHQL(string hqlWhere, string sort, int page, int limit);

    }
}