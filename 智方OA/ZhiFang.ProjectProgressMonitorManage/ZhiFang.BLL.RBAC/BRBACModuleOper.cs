using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.RBAC;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.RBAC;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;

namespace ZhiFang.BLL.RBAC
{
    public class BRBACModuleOper : BaseBLL<RBACModuleOper>, IBRBACModuleOper
    {
        public IBRBACRoleRight IBRBACRoleRight { get; set; }
        #region IBRBACModuleOper 成员

        public IList<RBACModuleOper> SearchModuleOperIDByModuleID(long longModuleID)
        {
            return ((IDRBACModuleOperDao)base.DBDao).SearchModuleOperIDByModuleID(longModuleID);
        }

        public IList<RBACModuleOper> SearchModuleOperByRoleID(long longRoleID)
        {
            return ((IDRBACModuleOperDao)base.DBDao).SearchModuleOperByRoleID(longRoleID);
        }

        public IList<RBACModuleOper> SearchModuleOperByHREmpID(long longHREmpID)
        {
            return ((IDRBACModuleOperDao)base.DBDao).SearchModuleOperByHREmpID(longHREmpID);
        }

        public IList<RBACModuleOper> SearchModuleOperByUserCode(string strUserCode)
        {
            return ((IDRBACModuleOperDao)base.DBDao).SearchModuleOperByUserCode(strUserCode);
        }

        /// <summary>
        /// 模块操作权限判定服务
        /// </summary>
        /// <param name="longHREmpID"></param>
        /// <param name="longModuleOperID"></param>
        /// <returns></returns>
        public bool JudgeModuleOperByHREmpIDAndModuleOperID(long longHREmpID, long longModuleOperID)
        {
            IList<RBACModuleOper> moduleOper = ((IDRBACModuleOperDao)base.DBDao).SearchModuleOperByHREmpID(longHREmpID);
            moduleOper = moduleOper.Where(p => p.Id == longModuleOperID).ToList();
            return (moduleOper.Count > 0) ? true : false;
        }

        /// <summary>
        /// 根据Session中的人员ID返回新增的模块列表
        /// </summary>
        /// <returns></returns>
        public IList<RBACModuleOper> RBAC_BA_GetNewModuleListBySessionHREmpID(long longHREmpID)
        {

            return null;
        }

        public bool DeleteByRBACModuleId(long RBACModuleId)
        {
            IList<RBACModuleOper> rbacmolist = ((IDRBACModuleOperDao)base.DBDao).SearchModuleOperIDByModuleID(RBACModuleId);
            if (rbacmolist != null && rbacmolist.Count > 0)
            {
                List<long> rbacmoidlist = new List<long>();
                foreach (var rbacmo in rbacmolist)
                {
                    rbacmoidlist.Add(rbacmo.Id);
                }

                if (!IBRBACRoleRight.DeleteByRBACModuleOperIDList(rbacmoidlist))
                {
                    return false;
                    throw new Exception("删除模块操作权限失败！");
                }
            }
            DBDao.DeleteByHql(" From RBACModuleOper rbacmoduleoper where rbacmoduleoper.RBACModule.Id=" + RBACModuleId);
            SYSDataRowRoleCacheBase.IsRefreshModuleOperCache = true;
            return true;
        }

        #endregion

        #region 系统缓存模块服务信息
        public IList<SYSCacheModuleOper> GetModuleOperCacheList()
        {
            IList<SYSCacheModuleOper> tempList = new List<SYSCacheModuleOper>();
            if (SYSDataRowRoleCacheBase.IsRefreshModuleOperCache == true)
            {
                IList<RBACModuleOper> list = this.SearchListByHQL("IsUse=1");
                //double objSize = 0;
                foreach (RBACModuleOper item in list)
                {
                    SYSCacheModuleOper model = new SYSCacheModuleOper();
                    model.ModuleId = item.RBACModule.Id;
                    model.ModuleOperId = item.Id;
                    model.RowFilterBaseCName = item.RowFilterBaseCName;
                    model.ServiceURLEName = item.ServiceURLEName;
                    //if (item.RBACRoleRightList != null && item.RBACRoleRightList.Count > 0)
                    //{
                    //    foreach (RBACRoleRight roleRight in item.RBACRoleRightList)
                    //    {
                    //        SYSCacheRoleRight cacheRoleRight = new SYSCacheRoleRight();
                    //        cacheRoleRight.RightID = roleRight.Id;
                    //        if (roleRight.RBACRole != null)
                    //            cacheRoleRight.RoleID = roleRight.RBACRole.Id;
                    //        if (roleRight.RBACModuleOper != null)
                    //            cacheRoleRight.ModuleOperId = roleRight.RBACModuleOper.Id;
                    //        if (roleRight.RBACRowFilter != null)
                    //            cacheRoleRight.RowFilterID = roleRight.RBACRowFilter.Id;
                    //        model.SYSCacheRoleRightList.Add(cacheRoleRight);
                    //    }
                    //}
                    //objSize += System.Runtime.InteropServices.Marshal.SizeOf(model);
                    tempList.Add(model);
                }
                SYSDataRowRoleCacheBase.IsRefreshModuleOperCache = false;
            }
            return tempList;
        }

        public BaseResultBool CopyRBACModuleOperOfModule(long moduleId, string copyModuleOpeIdStr)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (string.IsNullOrEmpty(copyModuleOpeIdStr))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "选择复制的模块服务为空!";
                return tempBaseResultBool;
            }
            copyModuleOpeIdStr = copyModuleOpeIdStr.TrimEnd(',');
            IList<RBACModuleOper> list = this.SearchListByHQL("Id in (" + copyModuleOpeIdStr + ")");
            string errorInfo = "";
            if (list.Count > 0)
            {
                RBACModule module = new RBACModule();
                module.Id = moduleId;
                byte[] arrDataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
                module.DataTimeStamp = arrDataTimeStamp;
                foreach (var model in list)
                {
                    RBACModuleOper copyEntity = new RBACModuleOper();
                    copyEntity.RBACModule = module;
                    copyEntity.IsUse = true;
                    copyEntity.CName = model.CName;
                    copyEntity.Comment = model.Comment;
                    copyEntity.RowFilterBase = model.RowFilterBase;
                    copyEntity.RowFilterBaseCName = model.RowFilterBaseCName;
                    copyEntity.ServiceURLEName = model.ServiceURLEName;
                    copyEntity.UseCode = model.UseCode;
                    copyEntity.UseRowFilter = model.UseRowFilter;
                    this.Entity = copyEntity;
                    bool result = this.Add();
                    if (result == false)
                        errorInfo += "模块服务名称为:" + copyEntity.CName + "复制新增失败;";
                }
            }
            if (!string.IsNullOrEmpty(errorInfo))
            {
                ZhiFang.Common.Log.Log.Error("CopyRBACModuleOperOfModule错误:" + errorInfo);
                tempBaseResultBool.ErrorInfo = errorInfo;
                tempBaseResultBool.success = false;
            }
            return tempBaseResultBool;
        }
        #endregion
    }
}