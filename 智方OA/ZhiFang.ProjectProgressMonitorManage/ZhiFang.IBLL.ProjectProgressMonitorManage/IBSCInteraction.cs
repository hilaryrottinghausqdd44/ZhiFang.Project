

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ProjectProgressMonitorManage
{
	/// <summary>
	///
	/// </summary>
	public  interface IBSCInteraction : IBGenericManager<SCInteraction>
	{
        /// <summary>
        /// 扩展交流内容的新增服务(支持新增话题或新增交流内容)
        /// </summary>
        /// <returns></returns>
        BaseResultDataValue AddSCInteractionExtend();
        /// <summary>
        /// 依某一业务对象ID获取该业务对象ID下的所有交流内容信息
        /// </summary>
        /// <param name="bobjectID"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        ZhiFang.Entity.Base.EntityList<SCInteraction> SearchAllSCInteractionByBobjectID(string bobjectID, int page, int count);
        /// <summary>
        /// 依某一业务对象ID获取该业务对象ID下的所有交流内容信息
        /// </summary>
        /// <param name="bobjectID"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        ZhiFang.Entity.Base.EntityList<SCInteraction> SearchAllSCInteractionByBobjectID(string bobjectID, string sort, int page, int count);
    }
}