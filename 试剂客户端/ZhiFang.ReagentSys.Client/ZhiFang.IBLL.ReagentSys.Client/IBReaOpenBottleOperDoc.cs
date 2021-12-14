

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaOpenBottleOperDoc : IBGenericManager<ReaOpenBottleOperDoc>
    {
        /// <summary>
        /// 获取出库货品开瓶管理登记信息
        /// </summary>
        /// <param name="outDtlId"></param>
        /// <param name="usrId"></param>
        /// <param name="usrCName"></param>
        /// <returns></returns>
        ReaOpenBottleOperDoc GetOBottleOperDocByOutDtlId(long outDtlId, long usrId, string usrCName);
        /// <summary>
        /// 新增保存
        /// </summary>
        /// <param name="outDtl"></param>
        /// <param name="outDtlId"></param>
        /// <param name="usrId"></param>
        /// <param name="usrCName"></param>
        /// <returns></returns>
        ReaOpenBottleOperDoc AddReaOpenBottleOperDoc(ReaBmsOutDtl outDtl, long outDtlId, long usrId, string usrCName);
    }
}