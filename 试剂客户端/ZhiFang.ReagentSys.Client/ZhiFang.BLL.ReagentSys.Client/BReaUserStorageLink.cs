
using ZhiFang.BLL.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.Base;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;

namespace ZhiFang.BLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public class BReaUserStorageLink : BaseBLL<ReaUserStorageLink>, ZhiFang.IBLL.ReagentSys.Client.IBReaUserStorageLink
    {
        /// <summary>
        /// 依传入的条件获取库房人员权限关系的所属库房ID集合信息
        /// </summary>
        /// <param name="hqlWhere"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public IList<long> SearchStorageIDListByHQL(string hqlWhere, string sort, int page, int limit)
        {
            IList<long> storageIDList = new List<long>();
            storageIDList = ((IDReaUserStorageLinkDao)base.DBDao).SearchStorageIDListByHQL(hqlWhere, sort, page, limit);
            return storageIDList;
        }
    }
}