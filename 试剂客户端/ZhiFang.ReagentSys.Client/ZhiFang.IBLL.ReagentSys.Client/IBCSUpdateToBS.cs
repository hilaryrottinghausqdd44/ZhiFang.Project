using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
{
    public interface IBCSUpdateToBS : IBGenericManager<SServiceClient>
    {
        BaseResultDataValue DeleteCSUpdateToBSQtyDtlInfo(long labId, string entity, long empID, string empName);
        /// <summary>
        /// CS试剂客户端升级到BS试剂客户端分步处理
        /// </summary>
        /// <param name="labId"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        BaseResultDataValue AddCSUpdateToBSByStep(long labId, string entity, long empID, string empName);

        BaseResultDataValue GetCSUpdateToBSInfo();
    }
}
