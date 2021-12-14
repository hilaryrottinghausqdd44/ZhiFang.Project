

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaTestEquipLab : IBGenericManager<ReaTestEquipLab>
    {
        bool EditVerification();
        /// <summary>
        /// 试剂客户端同步LIS的检验仪器信息
        /// </summary>
        /// <returns></returns>
        BaseResultBool SaveSyncLisTestEquipLabInfo(long labid);
    }
}