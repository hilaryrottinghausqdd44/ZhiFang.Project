using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.Entity.ProjectProgressMonitorManage.ViewObject.Response;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ProjectProgressMonitorManage
{
    /// <summary>
    ///
    /// </summary>
    public interface IBPWorkDayLog : IBGenericManager<PWorkDayLog>
    {
        bool AddPWorkDayLogByWeiXin(List<string> AttachmentUrlList);
        IList<WorkLogVO> SearchPWorkDayLogBySendTypeAndWorkLogType(string sd, string ed, int page, int limit, string sendtype, string worklogtype, string sort, long empid, long ownempid);
        IList<WorkLogVO> SearchTaskWorkDayLogTaskId(string sd, string ed, int page, int limit, string sort, long empid, long ownempid, long taskid);
        bool UpdatePWorkDayLogByField(PWorkDayLog entity, string[] tempArray);
        WorkLogVO ST_UDTO_SearchWorkDayLogByIdAndWorkLogType(long id, string worklogtype);
        int WorkDayAddLikeCountLogByIdAndWorkLogType(long id, string worklogtype);
        IList<WorkLogVO> SearchPWorkDayLogByDeptIdOrEmpIdAndWorkLogType(string sd, string ed, int page, int limit, long deptid, string worklogtype, string sort, long empid, long ownempid,bool isincludesubdept);
        IList<WorkLogVO> SearchPWorkDayLogByEmpId(string monthday, long empid, bool isincludesubdept);
    }
}