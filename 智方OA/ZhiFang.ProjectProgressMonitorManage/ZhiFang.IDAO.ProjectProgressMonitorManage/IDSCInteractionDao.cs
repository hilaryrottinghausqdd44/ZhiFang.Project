using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.ProjectProgressMonitorManage
{
	public interface IDSCInteractionDao : IDBaseDao<SCInteraction, long>
	{
        /// <summary>
        /// 更新某一交流话题的回复个数累加1并更新最后回复时间
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool UpdateReplyCountAndLastReplyDateTimeOfId(long? id);

    } 
}