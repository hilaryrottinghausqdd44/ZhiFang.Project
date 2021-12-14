using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.IDAO.ProjectProgressMonitorManage;

namespace ZhiFang.DAO.NHB.ProjectProgressMonitorManage
{
    public class SCInteractionDao : BaseDaoNHB<SCInteraction, long>, IDSCInteractionDao
    {
        /// <summary>
        /// 更新某一交流话题的回复个数累加1并更新最后回复时间
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateReplyCountAndLastReplyDateTimeOfId(long? id)
        {
            bool result = true;
            if (id.HasValue)
            {
                string hql = "update SCInteraction scinteraction set scinteraction.ReplyCount=(scinteraction.ReplyCount+1),scinteraction.LastReplyDateTime='"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +"' where scinteraction.Id=" + id.Value;
                int counts = this.UpdateByHql(hql);
                if (counts > 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }
            return result;
        }

    }
}