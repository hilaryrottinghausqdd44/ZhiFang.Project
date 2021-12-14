using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ProjectProgressMonitorManage
{
    /// <summary>
    ///
    /// </summary>
    public  interface IBPReceivePlan : IBGenericManager<PReceivePlan>
	{
        BaseResultDataValue AddPReceivePlanList(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, List<PReceivePlan> prplist);
        BaseResultDataValue ChangeApplyPReceivePlan(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, List<PReceivePlan> prplist, long PPReceivePlanID);
        BaseResultDataValue ChangeSubmitPReceivePlan(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, long PPReceivePlanID);
        BaseResultDataValue UnChangeSubmitPReceivePlan(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, long PPReceivePlanID);
        bool DelPReceivePlan(long longPReceivePlanID);
        BaseResultTree<PReceivePlan> SearchListTreeByHQL(string where);
        BaseResultTree<PReceivePlan> AdvSearchListTreeByHQL(string where);
        BaseResultDataValue AdvSearchTotalListTreeByHQL(string where, string fields);
    }
}