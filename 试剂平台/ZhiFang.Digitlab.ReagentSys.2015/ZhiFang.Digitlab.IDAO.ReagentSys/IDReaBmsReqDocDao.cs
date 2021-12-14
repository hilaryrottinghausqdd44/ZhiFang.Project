using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity.ReagentSys;

namespace ZhiFang.Digitlab.IDAO.ReagentSys
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