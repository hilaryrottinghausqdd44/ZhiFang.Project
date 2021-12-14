using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.OA;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.OA
{
    public interface IDAHSingleLicenceDao : IDBaseDao<AHSingleLicence, long>
    {
        /// <summary>
        /// 获取单站点需要特批的数据
        /// </summary>
        /// <param name="where"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        EntityList<AHSingleLicence> SearchSpecialApprovalAHSingleLicenceByHQL(string where, int page, int limit, string sort);

    }
}