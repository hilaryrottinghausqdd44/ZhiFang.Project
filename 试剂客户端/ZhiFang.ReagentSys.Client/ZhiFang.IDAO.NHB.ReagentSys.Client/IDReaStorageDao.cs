using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.NHB.ReagentSys.Client
{
    public interface IDReaStorageDao : IDBaseDao<ReaStorage, long>
    {
        IList<ReaStorage> SearchReaStorageListByStorageAndLinHQL(string storageHql, string linkHql, string operType, string sort, int page, int limit);
        /// <summary>
        /// 根据员工权限获取库房信息
        /// </summary>
        /// <param name="storageHql"></param>
        /// <param name="linkHql"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        EntityList<ReaStorage> SearchEntityListByStorageAndLinHQL(string storageHql, string linkHql, string operType, string sort, int page, int count);
    }
}