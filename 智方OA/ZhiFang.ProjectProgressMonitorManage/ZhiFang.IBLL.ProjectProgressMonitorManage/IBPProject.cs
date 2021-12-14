

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
    public interface IBPProject : IBGenericManager<PProject>
    {
        /// <summary>
        /// 项目信息保存前的计算处理
        /// </summary>
        void CalcProcessing();

        bool AddProject(PProject entity);

        BaseResultDataValue AddCopyProjectByID(long projectID, long typeID, bool isStandard);

        BaseResultDataValue AddStandardTask(long projectID);

        BaseResultDataValue AddProjectTaskByProjectID(long fromProjectID, long toProjectID, bool isStandard);

    }
}