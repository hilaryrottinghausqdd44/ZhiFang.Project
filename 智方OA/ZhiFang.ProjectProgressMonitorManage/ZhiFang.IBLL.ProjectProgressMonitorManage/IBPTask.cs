using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.Entity.ProjectProgressMonitorManage.ViewObject.Request;
using ZhiFang.Entity.ProjectProgressMonitorManage.ViewObject.Response;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ProjectProgressMonitorManage
{
	/// <summary>
	///
	/// </summary>
	public  interface IBPTask : IBGenericManager<PTask>
	{
        bool Test(SysWeiXinTemplate.PushWeiXinMessage func, string openid);
        /// <summary>
        /// 根据查询对象Task_Search查询实体列表
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        EntityList<PTask> SearchListByEntity(Task_Search entity, string sort, int Page, int Limit);
        /// <summary>
        /// 根据查询对象Task_Search查询实体列表
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="empid"></param>
        /// <param name="sort"></param>
        /// <param name="Page"></param>
        /// <param name="Limit"></param>
        /// <returns></returns>
        EntityList<PTask> SearchListByEntity(Task_Search entity, long empid, string sort, int Page, int Limit);
        /// <summary>
        /// 根据查询对象Task_Search查询实体列表
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        EntityList<PTask> SearchListByEntity(Task_Search entity, long empid, int Page, int Limit);
        bool PTaskAdd(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction);
        bool PTaskStatusUpdate(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, PTask entity, string[] tempArray, long EmpID, string EmpName,out string ErrorInfo);
    }
}