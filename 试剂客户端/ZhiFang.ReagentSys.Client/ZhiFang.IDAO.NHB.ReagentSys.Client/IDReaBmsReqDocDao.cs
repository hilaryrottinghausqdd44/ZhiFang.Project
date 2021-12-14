using ZhiFang.IDAO.Base;
using ZhiFang.Entity.ReagentSys.Client;

namespace ZhiFang.IDAO.NHB.ReagentSys.Client
{
    public interface IDReaBmsReqDocDao : IDBaseDao<ReaBmsReqDoc, long>
    {
        /// <summary>
        /// 批量更新申请单的状态
        /// </summary>
        /// <param name="idStr"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        bool UpdateStatusByIdStr(string idStr, int status);

    }
}