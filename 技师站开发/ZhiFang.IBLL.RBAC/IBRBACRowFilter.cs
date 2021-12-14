using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.IBLL.RBAC
{
    public interface IBRBACRowFilter : ZhiFang.IBLL.Base.IBGenericManager<RBACRowFilter>
    {
        /// <summary>
        /// 依行过滤条件ID及模块操作ID新增行过滤条件
        /// </summary>
        /// <param name="moduleOperId"></param>
        /// <param name="addRoleIdStr"></param>
        /// <param name="isDefaultCondition"></param>
        /// <param name="editRoleRightIdStr"></param>
        /// <returns></returns>
        BaseResultDataValue RBACRowFilterAndRBACRoleRightAddByModuleOperId(long moduleOperId, string addRoleIdStr, bool isDefaultCondition, string editRoleRightIdStr);
        /// <summary>
        /// 依行过滤条件ID及模块操作ID更新行过滤条件
        /// </summary>
        /// <param name="tempArray"></param>
        /// <param name="moduleOperId"></param>
        /// <param name="addRoleIdStr"></param>
        /// <param name="isDefaultCondition"></param>
        /// <param name="editRoleRightIdStr"></param>
        /// <returns></returns>
        BaseResultBool UpdateRBACRowFilterAndRBACRoleRightByModuleOperId(string[] tempArray, long moduleOperId, string addRoleIdStr, bool isDefaultCondition, string editRoleRightIdStr);
        /// <summary>
        /// 依行过滤条件ID及模块操作ID,删除行数据条件操作
        /// 1.,如果行数据条件为模块操作的默认条件,先清空更新待删除的行数据条件的模块操作的行数据条件Id值
        /// 2.更新清空待删除的行数据条件相关的角色权限记录的行数据条件的Id值
        /// 3.物理删除该行数据条件信息
        /// </summary>
        /// <param name="id">行数据条件的id</param>
        /// <param name="moduleOperId">模块操作的Id</param>
        /// <param name="isDefaultCondition">行数据条件是否是模块操作的默认条件</param>
        /// <returns></returns>
        BaseResultBool DeleteRBACRoleRightByModuleOperId(long id, long moduleOperId);
        /// <summary>
        /// 获取缓存行数据条件信息
        /// </summary>
        /// <returns></returns>
        IList<SYSCacheRowFilter> GetRowFilterCacheList();

        /// <summary>
        /// 依预置条件ID新增行数据过滤条件时同时处行数据条件的理角色权限
        /// </summary>
        /// <param name="preconditionsId"></param>
        /// <param name="addRoleIdStr"></param>
        /// <param name="editRoleRightIdStr"></param>
        /// <returns></returns>
        BaseResultDataValue AddRBACRowFilterAndRBACRoleRightByPreconditionsId(long preconditionsId, string addRoleIdStr, string editRoleRightIdStr, string moduleOperId);
        BaseResultBool UpdateRBACRowFilterAndRBACRoleRightByPreconditionsId(string[] tempArray, long preconditionsId, string addRoleIdStr, string editRoleRightIdStr, string moduleOperId);
        /// <summary>
        /// 依行过滤条件ID及预置条件ID,删除行数据条件操作
        /// </summary>
        /// <param name="id"></param>
        /// <param name="preconditionsId"></param>
        /// <returns></returns>
        BaseResultBool DeleteRBACRowFilterAndRBACRoleRightByPreconditionsId(long id, long preconditionsId);
        /// <summary>
        /// 将某一预置条件下选择的行过滤条件复制新增到指定的预置条件项
        /// </summary>
        /// <param name="preconditionsIdStr"></param>
        /// <param name="rowfilterIdStr"></param>
        /// <returns></returns>
        BaseResultBool CopyRBACRowFilterOfPreconditionsIdStr(string preconditionsIdStr, string rowfilterIdStr);
    }
}