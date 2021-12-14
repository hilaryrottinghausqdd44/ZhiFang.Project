using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.RBAC;
using ZhiFang.IDAO.RBAC;

namespace ZhiFang.BLL.RBAC
{
    public class BRBACRowFilter : BaseBLL<RBACRowFilter>, ZhiFang.IBLL.RBAC.IBRBACRowFilter
    {
        public IBRBACModuleOper IBRBACModuleOper { set; get; }
        public IBRBACRoleRight IBRBACRoleRight { get; set; }
        public IBRBACPreconditions IBRBACPreconditions { set; get; }

        #region 模块服务的行数据条件处理
        public BaseResultDataValue RBACRowFilterAndRBACRoleRightAddByModuleOperId(long moduleOperId, string addRoleIdStr, bool isDefaultCondition, string editRoleRightIdStr)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (this.Entity == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "Entity为空,不能保存!";
                return brdv;
            }
            brdv.success = this.Add();
            if (brdv.success)
            {
                brdv = AddRoleRightByModuleOperId(moduleOperId);
                if (isDefaultCondition == true)
                    brdv = SetIsDefaultRBACRowFilter(moduleOperId, isDefaultCondition);
                if (!String.IsNullOrEmpty(addRoleIdStr))
                    brdv = AddRoleRightByModuleOperId(moduleOperId, addRoleIdStr);
                if (!String.IsNullOrEmpty(editRoleRightIdStr))
                    brdv = UpdateRoleRight(editRoleRightIdStr, "update");
            }
            if (brdv.success == true)
                SYSDataRowRoleCacheBase.IsRefreshRowFilterCache = true;
            return brdv;
        }
        public BaseResultBool UpdateRBACRowFilterAndRBACRoleRightByModuleOperId(string[] tempArray, long moduleOperId, string addRoleIdStr, bool isDefaultCondition, string editRoleRightIdStr)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (this.Entity == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "Entity为空,不能保存!";
                return tempBaseResultBool;
            }
            brdv.success = Update(tempArray);// ((IDRBACRowFilterDao)base.DBDao).
            if (brdv.success)
            {
                brdv = SetIsDefaultRBACRowFilter(moduleOperId, isDefaultCondition);
                if (!String.IsNullOrEmpty(addRoleIdStr))
                    brdv = AddRoleRightByModuleOperId(moduleOperId, addRoleIdStr);
                if (!String.IsNullOrEmpty(editRoleRightIdStr))
                    brdv = UpdateRoleRight(editRoleRightIdStr, "update");
            }
            tempBaseResultBool.success = brdv.success;
            tempBaseResultBool.ErrorInfo = brdv.ErrorInfo;
            if (brdv.success == true)
                SYSDataRowRoleCacheBase.IsRefreshRowFilterCache = true;
            return tempBaseResultBool;
        }
        /// <summary>
        /// 模块服务--物理删除该行数据条件信息及相关的角色权限信息
        /// </summary>
        /// <param name="id">行数据条件的id</param>
        /// <param name="moduleOperId">模块操作的Id</param>
        /// <returns></returns>
        public BaseResultBool DeleteRBACRoleRightByModuleOperId(long id, long moduleOperId)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            this.Entity = this.Get(id);
            if (this.Entity == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "id为" + id + "的信息为空,不能删除!";
                return tempBaseResultBool;
            }

            bool isDefaultCondition = false;
            brdv = SetIsDefaultRBACRowFilter(moduleOperId, isDefaultCondition);

            if (brdv.success)
            {
                brdv.success = IBRBACRoleRight.DeleteRBACRoleRightByModuleOperId(this.Entity.Id, moduleOperId);

            }
            if (brdv.success)
            {
                brdv.success = ((IDRBACRowFilterDao)base.DBDao).Delete(this.Entity);
            }
            tempBaseResultBool.success = brdv.success;
            tempBaseResultBool.ErrorInfo = brdv.ErrorInfo;
            if (brdv.success == true)
                SYSDataRowRoleCacheBase.IsRefreshRowFilterCache = true;
            return tempBaseResultBool;
        }
        /// <summary>
        /// 更新模块操作
        /// 如果行数据过滤条件为模块操作的默认行数据条件,更新模块操作的行数据过滤条件的关系
        /// </summary>
        /// <param name="moduleOperId"></param>
        /// <param name="isDefaultCondition">是否模块操作的默认行数据过滤条件</param>
        /// <returns></returns>
        private BaseResultDataValue SetIsDefaultRBACRowFilter(long moduleOperId, bool isDefaultCondition)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (this.Entity == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "Entity为空,不能保存!";
                return brdv;
            }
            List<string> list = new List<string>();
            list.Add("Id=" + moduleOperId);
            if (isDefaultCondition == true)
                list.Add("RBACRowFilter.Id=" + this.Entity.Id);
            else
                list.Add("RBACRowFilter.Id=null");
            brdv.success = IBRBACModuleOper.Update(list.ToArray());
            return brdv;
        }
        /// <summary>
        /// 新增角色权限的数据过滤条件空行的关系(只有模块操作ID和行过滤条件ID,角色ID为空)
        /// </summary>
        /// <param name="moduleOperId">模块操作Id</param>
        /// <returns></returns>
        private BaseResultDataValue AddRoleRightByModuleOperId(long moduleOperId)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (this.Entity == null)
            {
                brdv.success = true;
                return brdv;
            }
            byte[] arrDataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
            RBACModuleOper moduleOper = new RBACModuleOper();
            moduleOper.Id = moduleOperId;
            moduleOper.DataTimeStamp = arrDataTimeStamp;

            RBACRoleRight roleRight = new RBACRoleRight();
            roleRight.RBACModuleOper = moduleOper;
            roleRight.RBACRowFilter = this.Entity;
            //ZhiFang.Common.Log.Log.Debug("新增角色权限的数据过滤条件空行的关系--模块操作Id:" + moduleOperId + ",行过滤条件Id:" + this.Entity.Id);
            IBRBACRoleRight.Entity = roleRight;
            brdv.success = IBRBACRoleRight.Add();
            return brdv;
        }
        /// <summary>
        /// 依模块操作ID,新增行过滤条件的角色权限关系
        /// </summary>
        /// <param name="moduleOperId">模块操作Id</param>
        /// <param name="roleIdStr">角色ID字符串</param>
        /// <returns></returns>
        private BaseResultDataValue AddRoleRightByModuleOperId(long moduleOperId, string roleIdStr)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (String.IsNullOrEmpty(roleIdStr))
            {
                brdv.success = true;
                return brdv;
            }
            if (this.Entity == null)
            {
                brdv.success = true;
                return brdv;
            }
            string[] idStr = roleIdStr.Trim().Split(',');
            byte[] arrDataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
            if (this.Entity.DataTimeStamp == null || this.Entity.DataTimeStamp.Length == 0) this.Entity.DataTimeStamp = arrDataTimeStamp;
            RBACModuleOper moduleOper = new RBACModuleOper();
            moduleOper.Id = moduleOperId;
            moduleOper.DataTimeStamp = arrDataTimeStamp;

            foreach (string id in idStr)
            {
                if (!String.IsNullOrEmpty(id.Trim()))
                {
                    RBACRoleRight roleRight = new RBACRoleRight();
                    roleRight.RBACModuleOper = moduleOper;
                    roleRight.RBACRowFilter = this.Entity;

                    RBACRole role = new RBACRole();
                    role.Id = long.Parse(id.Trim());
                    role.DataTimeStamp = arrDataTimeStamp;
                    roleRight.RBACRole = role;
                    ZhiFang.Common.Log.Log.Debug("新增行过滤条件的角色权限关系--模块操作Id:" + moduleOperId + ",行过滤条件Id:" + this.Entity.Id + ",角色Id:" + id);
                    IBRBACRoleRight.Entity = roleRight;
                    brdv.success = IBRBACRoleRight.Add();
                }
            }
            return brdv;
        }
        #endregion

        /// <summary>
        /// 行过滤条件在该模块操作下,选择角色已经存在角色权限中,需要更新角色权限的行过滤条件关系
        /// </summary>
        /// <param name="roleRightIdStr">角色权限ID字符串,如123,1231,31222,2123131</param>
        ///<param name="type">update:更新行数据条件id到角色权限;clear:清空角色权限的行数据条件的id值</param>
        /// <returns></returns>
        private BaseResultDataValue UpdateRoleRight(string roleRightIdStr, string type)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (String.IsNullOrEmpty(roleRightIdStr))
            {
                brdv.success = true;
                return brdv;
            }
            if (String.IsNullOrEmpty(type))
            {
                brdv.success = true;
                return brdv;
            }
            if (this.Entity == null)
            {
                brdv.success = true;
                return brdv;
            }
            string[] idStr = roleRightIdStr.Trim().Split(',');
            foreach (string id in idStr)
            {
                if (!String.IsNullOrEmpty(id.Trim()))
                {
                    List<string> list = new List<string>();
                    list.Add("Id=" + id.Trim());
                    switch (type)
                    {
                        case "clear":
                            list.Add("RBACRowFilter.Id=null");
                            break;
                        default:
                            list.Add("RBACRowFilter.Id=" + this.Entity.Id);
                            break;
                    }
                    brdv.success = IBRBACRoleRight.Update(list.ToArray());
                    ZhiFang.Common.Log.Log.Debug("更新角色权限的行过滤条件关系--" + "行过滤条件Id:" + this.Entity.Id + ",角色Id:" + id);
                }
            }
            return brdv;
        }
        #region 系统缓存行数据条件信息
        public IList<SYSCacheRowFilter> GetRowFilterCacheList()
        {
            IList<SYSCacheRowFilter> tempList = new List<SYSCacheRowFilter>();

            if (SYSDataRowRoleCacheBase.IsRefreshRowFilterCache == true)
            {
                IList<RBACRowFilter> list = this.SearchListByHQL("IsUse=1");
                //double objSize = 0;
                foreach (RBACRowFilter item in list)
                {
                    SYSCacheRowFilter model = new SYSCacheRowFilter();
                    model.ModuleOperId = item.Id;
                    model.Id = item.Id;
                    model.EntityCode = item.EntityCode;
                    model.RowFilterCondition = item.RowFilterCondition;
                    tempList.Add(model);
                }
                SYSDataRowRoleCacheBase.IsRefreshRowFilterCache = false;
            }
            return tempList;
        }
        #endregion
        #region 预置条件的行数据条件处理
        public BaseResultDataValue AddRBACRowFilterAndRBACRoleRightByPreconditionsId(long preconditionsId, string addRoleIdStr, string editRoleRightIdStr, string moduleOperId)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (this.Entity == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "Entity为空,不能保存!";
                return brdv;
            }
            brdv.success = this.Add();
            if (brdv.success)
            {
                //添加默认空行
                brdv = AddRoleRightByPreconditionsId(preconditionsId, moduleOperId);

                if (!String.IsNullOrEmpty(addRoleIdStr))
                    brdv = AddRoleRightByPreconditionsId(preconditionsId, addRoleIdStr, moduleOperId);
                if (!String.IsNullOrEmpty(editRoleRightIdStr))
                    brdv = UpdateRoleRight(editRoleRightIdStr, "update");
            }
            if (brdv.success == true)
                SYSDataRowRoleCacheBase.IsRefreshRowFilterCache = true;
            return brdv;
        }
        /// <summary>
        /// 新增角色权限的数据过滤条件空行的关系(只有预置条件ID,行过滤条件ID,角色ID为空)
        /// </summary>
        /// <param name="moduleOperId">模块操作Id</param>
        /// <returns></returns>
        private BaseResultDataValue AddRoleRightByPreconditionsId(long preconditionsId, string moduleOperId)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (this.Entity == null)
            {
                brdv.success = true;
                return brdv;
            }
            byte[] arrDataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
            if (this.Entity.DataTimeStamp == null || this.Entity.DataTimeStamp.Length == 0) this.Entity.DataTimeStamp = arrDataTimeStamp;
            RBACPreconditions module = new RBACPreconditions();
            module.Id = preconditionsId;
            module.DataTimeStamp = arrDataTimeStamp;

            RBACModuleOper moduleOper = null;
            if (!string.IsNullOrEmpty(moduleOperId))
            {
                moduleOper = new RBACModuleOper();
                moduleOper.Id = long.Parse(moduleOperId);
                moduleOper.DataTimeStamp = arrDataTimeStamp;
            }

            RBACRoleRight roleRight = new RBACRoleRight();
            roleRight.RBACPreconditions = module;
            roleRight.RBACModuleOper = moduleOper;
            roleRight.RBACRowFilter = this.Entity;
            //ZhiFang.Common.Log.Log.Debug("新增角色权限的数据过滤条件空行的关系--预置条件Id:" + preconditionsId + ",行过滤条件Id:" + this.Entity.Id);
            IBRBACRoleRight.Entity = roleRight;
            brdv.success = IBRBACRoleRight.Add();
            return brdv;
        }
        private BaseResultDataValue AddRoleRightByPreconditionsId(long preconditionsId, string roleIdStr, string moduleOperId)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (String.IsNullOrEmpty(roleIdStr))
            {
                brdv.success = true;
                return brdv;
            }
            if (this.Entity == null)
            {
                brdv.success = true;
                return brdv;
            }
            string[] idStr = roleIdStr.Trim().Split(',');
            byte[] arrDataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
            if (this.Entity.DataTimeStamp == null || this.Entity.DataTimeStamp.Length == 0) this.Entity.DataTimeStamp = arrDataTimeStamp;
            RBACPreconditions precondition = new RBACPreconditions();
            precondition.Id = preconditionsId;
            precondition.DataTimeStamp = arrDataTimeStamp;
            RBACModuleOper moduleOper = null;
            if (!string.IsNullOrEmpty(moduleOperId))
            {
                moduleOper = new RBACModuleOper();
                moduleOper.Id = long.Parse(moduleOperId);
                moduleOper.DataTimeStamp = arrDataTimeStamp;
            }

            foreach (string id in idStr)
            {
                if (!String.IsNullOrEmpty(id.Trim()))
                {
                    RBACRoleRight roleRight = new RBACRoleRight();
                    roleRight.RBACPreconditions = precondition;
                    roleRight.RBACRowFilter = this.Entity;
                    roleRight.RBACModuleOper = moduleOper;
                    RBACRole role = new RBACRole();
                    role.Id = long.Parse(id.Trim());
                    role.DataTimeStamp = arrDataTimeStamp;
                    roleRight.RBACRole = role;
                    //ZhiFang.Common.Log.Log.Debug("新增行过滤条件的角色权限关系--预置条件Id:" + preconditionsId + ",行过滤条件Id:" + this.Entity.Id + ",角色Id:" + id);
                    IBRBACRoleRight.Entity = roleRight;
                    brdv.success = IBRBACRoleRight.Add();
                }
            }
            return brdv;
        }
        public BaseResultBool UpdateRBACRowFilterAndRBACRoleRightByPreconditionsId(string[] tempArray, long preconditionsId, string addRoleIdStr, string editRoleRightIdStr, string moduleOperId)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (this.Entity == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "Entity为空,不能保存!";
                return tempBaseResultBool;
            }
            brdv.success = this.Update(tempArray);//((IDRBACRowFilterDao)base.DBDao)
            if (brdv.success)
            {
                if (!String.IsNullOrEmpty(addRoleIdStr))
                    brdv = AddRoleRightByPreconditionsId(preconditionsId, addRoleIdStr, moduleOperId);
                if (!String.IsNullOrEmpty(editRoleRightIdStr))
                    brdv = UpdateRoleRight(editRoleRightIdStr, "update");
            }
            tempBaseResultBool.success = brdv.success;
            tempBaseResultBool.ErrorInfo = brdv.ErrorInfo;
            if (tempBaseResultBool.success == true)
                SYSDataRowRoleCacheBase.IsRefreshRowFilterCache = true;
            return tempBaseResultBool;
        }
        public BaseResultBool DeleteRBACRowFilterAndRBACRoleRightByPreconditionsId(long id, long preconditionsId)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            this.Entity = this.Get(id);
            if (this.Entity == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "id为" + id + "的信息为空,不能删除!";
                return tempBaseResultBool;
            }

            if (brdv.success)
            {
                brdv.success = IBRBACRoleRight.DeleteRBACRoleRightByPreconditionsId(this.Entity.Id, preconditionsId);
            }
            if (brdv.success)
            {
                brdv.success = ((IDRBACRowFilterDao)base.DBDao).Delete(this.Entity);
            }
            tempBaseResultBool.success = brdv.success;
            tempBaseResultBool.ErrorInfo = brdv.ErrorInfo;
            if (tempBaseResultBool.success == true)
                SYSDataRowRoleCacheBase.IsRefreshRowFilterCache = true;
            return tempBaseResultBool;
        }
        #endregion

        /// <summary>
        /// 将某一预置条件下选择的行过滤条件复制新增到指定的预置条件项
        /// </summary>
        /// <param name="preconditionsIdStr"></param>
        /// <param name="rowfilterIdStr"></param>
        /// <returns></returns>
        public BaseResultBool CopyRBACRowFilterOfPreconditionsIdStr(string preconditionsIdStr, string rowfilterIdStr)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (string.IsNullOrEmpty(preconditionsIdStr))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "选择的预置条件项为空!";
                return tempBaseResultBool;
            }
            else if (string.IsNullOrEmpty(rowfilterIdStr))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "选择行过滤条件值为空!";
                return tempBaseResultBool;
            }

            preconditionsIdStr = preconditionsIdStr.TrimEnd(',');
            rowfilterIdStr = rowfilterIdStr.TrimEnd(',');
            IList<RBACRowFilter> list = this.SearchListByHQL(" IsPreconditions=1 and Id in (" + rowfilterIdStr + ")");
            string errorInfo = "";
            if (list.Count > 0)
            {
                string[] copyIdArr = preconditionsIdStr.Split(',');
                IList<RBACPreconditions> tempList = IBRBACPreconditions.SearchListByHQL("Id in (" + preconditionsIdStr + ")");
                foreach (var precondition in tempList)
                {
                    foreach (var entity in list)
                    {
                        entity.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                        entity.IsPreconditions = true;
                        entity.RBACPreconditions = precondition;
                        this.Entity = entity;
                        if (this.Add() == false)
                            errorInfo += "行过滤条件名称为:" + entity.CName + "复制新增失败;";
                        else
                        {
                            //给预置条件新增的行过滤条件添加默认的权限
                            AddRoleRightByPreconditionsId(precondition.Id, precondition.RBACModuleOper.Id.ToString());
                        }
                    }
                }
            }
            if (!string.IsNullOrEmpty(errorInfo))
            {
                ZhiFang.Common.Log.Log.Error("CopyRBACRowFilterOfPreconditionsIdStr错误:" + errorInfo);
                tempBaseResultBool.ErrorInfo = errorInfo;
                tempBaseResultBool.success = false;
            }
            return tempBaseResultBool;
        }
    }
}