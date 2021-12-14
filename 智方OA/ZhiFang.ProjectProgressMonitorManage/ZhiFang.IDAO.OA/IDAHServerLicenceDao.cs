using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.OA;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.OA
{
    public interface IDAHServerLicenceDao : IDBaseDao<AHServerLicence, long>
    {
        /// <summary>
        /// 获取服务器授权需要特批的数据
        /// </summary>
        /// <param name="where"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        EntityList<AHServerLicence> SearchSpecialApprovalAHServerLicenceByHQL(string where, int page, int limit, string sort);

        /// <summary>
        /// 联合查询仪器明细获取服务器授权信息
        /// </summary>
        /// <param name="where">申请主单查询条件</param>
        /// <param name="dtlWhere">仪器明细查询条件</param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        EntityList<AHServerLicence> SearchListByDocAndDtlHQL_LicenceInfo(string where, string dtlWhere, int page, int limit, string sort);
    }
}