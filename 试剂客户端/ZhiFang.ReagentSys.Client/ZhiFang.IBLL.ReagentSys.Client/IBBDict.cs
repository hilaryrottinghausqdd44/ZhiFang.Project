using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public  interface IBBDict : IBGenericManager<BDict>
	{
        /// <summary>
        /// 接口同步保存品牌（厂商）
        /// </summary>
        /// <returns></returns>
        BaseResultData SaveProdOrgByInterface(string matchCode, Dictionary<string, object> dicFieldAndValue, ref BDict bDict);

    }
}
