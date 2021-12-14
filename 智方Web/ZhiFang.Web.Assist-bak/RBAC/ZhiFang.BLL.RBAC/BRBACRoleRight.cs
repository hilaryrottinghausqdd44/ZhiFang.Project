using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.RBAC;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.RBAC;
using Spring.Context;
using Spring.Context.Support;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC.ViewObject.Response;

namespace ZhiFang.BLL.RBAC
{
    public class BRBACRoleRight : BaseBLL<ZhiFang.Entity.RBAC.RBACRoleRight>, IBRBACRoleRight
    {
        public IBRBACRole IBRBACRole { set; get; }

        public IBRBACModuleOper IBRBACModuleOper { set; get; }

        public IBRBACRowFilter IBRBACRowFilter { get; set; }

        public IBRBACRoleModule IBRBACRoleModule { get; set; }

        public IList<RBACRoleRight> GetRBACRoleRightByRBACUserCodeAndModuleOperID(string strUserCode, long longModuleOperID)
        {
            return ((IDRBACRoleRightDao)base.DBDao).GetRBACRoleRightByRBACUserCodeAndModuleOperID(strUserCode, longModuleOperID);
        }

        /// <summary>
        /// 模块操作权限信息获取服务
        /// </summary>
        /// <param name="longHREmpID"></param>
        /// <param name="longModuleOperID"></param>
        /// <returns></returns>
        public IList<RBACRoleRight> RBAC_BA_GetRBACRoleRightByHREmpIDAndModuleOperID(long longHREmpID, long longModuleOperID)
        {
            IList<RBACRoleRight> roleRightList = new List<RBACRoleRight>();
            IList<RBACRole> roleList = IBRBACRole.SearchRoleByHREmpID(longHREmpID);
            foreach (var tmp in roleList)
            {
                IList<RBACRoleRight> tmpList = ((IDRBACRoleRightDao)base.DBDao).SearchRBACRoleRightByRoleIDAndModuleOperID(tmp.Id, longModuleOperID);
                roleRightList = roleRightList.Concat(tmpList).ToList();
            }
            return roleRightList;
        }

        /// <summary>
        /// 模块操作权限信息获取服务
        /// </summary>
        /// <param name="longHREmpID"></param>
        /// <param name="longModuleOperID"></param>
        /// <returns></returns>
        public bool JudgeRBACRoleRightByHREmpIDAndModuleOperID(long longHREmpID, long longModuleOperID)
        {

            IApplicationContext context = ContextRegistry.GetContext();
            object bslog = context.GetObject("DataRowRoleHQL");
            DataRowRoleHQL D = (DataRowRoleHQL)bslog;

            bool r = false;
            IList<RBACRole> roleList = IBRBACRole.SearchRoleByHREmpID(longHREmpID);

            foreach (var tmp in roleList)
            {
                IList<RBACRoleRight> tmpList = ((IDRBACRoleRightDao)base.DBDao).SearchRBACRoleRightByRoleIDAndModuleOperID(tmp.Id, longModuleOperID);
                if (tmpList.Count > 0)
                {
                    if (tmpList[0].RBACRowFilter != null && tmpList[0].RBACRowFilter.IsUse)
                    {
                        if (D.Hql == null || D.Hql.Trim() == "")
                        {
                            D.Hql = tmpList[0].RBACRowFilter.RowFilterCondition;
                        }
                        else
                        {
                            D.Hql += " or " + tmpList[0].RBACRowFilter.RowFilterCondition;
                        }
                    }
                    r = true;
                }
            }
            if (!r)
            {
                return r;
            }
            if (D.Hql.Trim() == "" || D.Hql == null)
            {
                RBACModuleOper rbacmo = IBRBACModuleOper.Get(longModuleOperID);
                if (rbacmo.UseRowFilter)
                {
                    if (rbacmo != null && rbacmo.RBACRowFilter != null)
                    {
                        D.Hql = rbacmo.RBACRowFilter.RowFilterCondition;
                    }
                    else
                    {
                        D.Hql = "1=2 ";
                    }
                }
                else
                {
                    D.Hql = "1=1";
                }
            }
            return r;
        }

        #region 模块操作行数据过滤条件
        /// <summary>
        /// 模块操作的行数据条件角色树
        /// </summary>
        /// <param name="longModuleOperID"></param>
        /// <returns></returns>
        public BaseResultTree SearchRBACRowFilterTreeByModuleOperID(long longModuleOperID, bool isPreconditions)
        {
            BaseResultTree tempBaseResultTree = new BaseResultTree();
            List<tree> tempListTree = new List<tree>();
            try
            {
                var tempEntity = this.SearchListByHQL(" rbacroleright.RBACModuleOper.Id=" + longModuleOperID.ToString(), -1, -1);
                if (tempEntity != null && tempEntity.count > 0)
                {
                    IList<RBACRoleRight> tempRBACRoleRightList = tempEntity.list;
                    IList<string> tempRBACRowFilterID = new List<string>();
                    long defaultRBACRowFilterID = 0;
                    bool isdefaultRBACRowFilter = false;
                    RBACRowFilter rbacRowFilter = tempRBACRoleRightList[0].RBACModuleOper.RBACRowFilter;
                    if (rbacRowFilter != null && rbacRowFilter.IsPreconditions == isPreconditions)
                        defaultRBACRowFilterID = rbacRowFilter.Id;
                    foreach (RBACRoleRight tempRoleRight in tempRBACRoleRightList)
                    {
                        if (tempRoleRight.RBACRowFilter != null && tempRoleRight.RBACRowFilter.IsPreconditions == isPreconditions & (!tempRBACRowFilterID.Contains(tempRoleRight.RBACRowFilter.Id.ToString())))
                        {
                            tempRBACRowFilterID.Add(tempRoleRight.RBACRowFilter.Id.ToString());
                            tree tempTree = new tree();
                            if (defaultRBACRowFilterID > 0 && defaultRBACRowFilterID == tempRoleRight.RBACRowFilter.Id)
                            {
                                tempTree.text = "(默认)" + tempRoleRight.RBACRowFilter.CName;
                                isdefaultRBACRowFilter = true;
                            }
                            else
                                tempTree.text = tempRoleRight.RBACRowFilter.CName;
                            tempTree.Para = tempRoleRight.Id;
                            //预置条件的实体编码及实体显示名称还原用
                            if (!String.IsNullOrEmpty(tempRoleRight.RBACRowFilter.EntityCode))
                                tempTree.value = tempRoleRight.RBACRowFilter.EntityCode + "|" + tempRoleRight.RBACRowFilter.EntityCName;
                            tempTree.tid = tempRoleRight.RBACRowFilter.Id.ToString();
                            tempTree.pid = "0";
                            tempTree.objectType = "RBACRowFilter";
                            tempTree.expanded = true;
                            tempTree.Tree = GetChildTree(tempRoleRight.RBACRowFilter.Id, tempRBACRoleRightList, isPreconditions);
                            tempTree.leaf = (tempTree.Tree.Count <= 0);
                            //tempTree.iconCls = tempTree.leaf ? "orgImg16" : "orgsImg16";
                            tempTree.iconCls = "orgsImg16";
                            tempListTree.Add(tempTree);
                        }
                    }
                    if (rbacRowFilter != null && rbacRowFilter.IsPreconditions == isPreconditions && (!isdefaultRBACRowFilter))
                    {
                        tree tempTree = new tree();
                        tempTree.text = "(默认)" + rbacRowFilter.CName;
                        tempTree.tid = rbacRowFilter.Id.ToString();
                        tempTree.pid = "0";
                        tempTree.Para = "";
                        //预置条件的实体编码及实体显示名称还原用
                        if (!String.IsNullOrEmpty(rbacRowFilter.EntityCode))
                            tempTree.value = rbacRowFilter.EntityCode + "|" + rbacRowFilter.EntityCName;
                        tempTree.objectType = "RBACRowFilter";
                        //tempTree.expanded = true;
                        tempTree.leaf = true;
                        //tempTree.iconCls = tempTree.leaf ? "orgImg16" : "orgsImg16";
                        tempTree.iconCls = "orgsImg16";
                        tempListTree.Add(tempTree);
                    }
                }
                tempBaseResultTree.Tree = tempListTree;
                tempBaseResultTree.success = true;
            }
            catch (Exception ex)
            {
                tempBaseResultTree.success = false;
                tempBaseResultTree.ErrorInfo = ex.Message;
            }
            return tempBaseResultTree;
        }

        public List<tree> GetChildTree(long ParentID, IList<RBACRoleRight> rbacRoleRightList, bool isPreconditions)
        {
            List<tree> tempListTree = new List<tree>();
            try
            {
                IList<RBACRoleRight> tempEntityList = rbacRoleRightList.Where(p => (p.RBACRowFilter != null && p.RBACRowFilter.IsPreconditions == isPreconditions && p.RBACRowFilter.Id == ParentID)).ToList();
                if (tempEntityList != null && tempEntityList.Count > 0)
                {
                    foreach (RBACRoleRight tempRBACRoleRight in tempEntityList)
                    {
                        if (tempRBACRoleRight.RBACRole != null)
                        {
                            tree tempTree = new tree();
                            //角色权限ID值给Para,方便前台通过获取Para值删除角色权限信息
                            tempTree.Para = tempRBACRoleRight.Id;
                            //if (!String.IsNullOrEmpty(tempRBACRoleRight.RBACRowFilter.EntityCode))
                            tempTree.value = "";// tempRBACRoleRight.RBACRowFilter.EntityCode + "|" + tempRBACRoleRight.RBACRowFilter.EntityCName;
                            tempTree.text = tempRBACRoleRight.RBACRole.CName;
                            tempTree.tid = tempRBACRoleRight.RBACRole.Id.ToString();
                            tempTree.pid = ParentID.ToString();
                            tempTree.objectType = "RBACRole";
                            tempTree.leaf = true;
                            //tempTree.iconCls = tempTree.leaf ? "orgImg16" : "orgsImg16";
                            tempTree.iconCls = "orgImg16";
                            tempListTree.Add(tempTree);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return tempListTree;
        }

        /// <summary>
        /// 复制模块操作的角色权限
        /// </summary>
        /// <param name="sourceModuleOperID">源模块操作ID</param>
        /// <param name="targetModuleOperID">目标模块操作ID</param>
        /// <returns>BaseResultDataValue</returns>
        public BaseResultDataValue AddRBACRoleRightByModuleOperID(long sourceModuleOperID, long targetModuleOperID)
        {
            BaseResultDataValue tmpBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tmpBaseResultDataValue.success = true;
                tmpBaseResultDataValue.ResultDataValue = "";

                var tmpEntityList = this.SearchListByHQL(" rbacroleright.RBACModuleOper.Id=" + targetModuleOperID, -1, -1);
                if (tmpEntityList != null && tmpEntityList.count > 0)
                {
                    //根据目标模块操作ID删除角色权限
                    foreach (var tmp in tmpEntityList.list)
                    {
                        this.Entity = tmp;
                        this.Remove();
                    }
                }
                var tmpEntityCopyList = this.SearchListByHQL(" rbacroleright.RBACModuleOper.Id=" + sourceModuleOperID, -1, -1);
                if (tmpEntityCopyList != null && tmpEntityCopyList.count > 0)
                {
                    //提取行过滤ID
                    IList<long> tmpInsertRowFilterIDList = new List<long>();
                    IList<long> tmpRowFilterIDList = tmpEntityCopyList.list.Where(p => p.RBACRowFilter != null).Select(p => p.RBACRowFilter.Id).Distinct().ToList();
                    Dictionary<long, long> dic = new Dictionary<long, long>();
                    if (tmpRowFilterIDList != null && tmpRowFilterIDList.Count > 0)
                    {
                        foreach (var id in tmpRowFilterIDList)
                        {
                            dic.Add(id, ZhiFang.Common.Public.GUIDHelp.GetGUIDLong());
                        }
                    }

                    //获取目标模块操作对象
                    var tmpRBACModuleOper = IBRBACModuleOper.Get(targetModuleOperID);
                    //将源模块操作ID下角色权限复制到目标模块操作ID下
                    foreach (var tmp in tmpEntityCopyList.list)
                    {
                        //角色权限的行过滤实体重新复制一份，生成新的ID保存到数据库中
                        RBACRowFilter tmpRBACRowFilter = null;
                        if (dic != null && dic.Count > 0 && tmp.RBACRowFilter != null)
                        {
                            var value = dic[tmp.RBACRowFilter.Id];

                            tmpRBACRowFilter = new RBACRowFilter();
                            tmpRBACRowFilter.Id = value;
                            tmpRBACRowFilter.CName = tmp.RBACRowFilter.CName;
                            tmpRBACRowFilter.EName = tmp.RBACRowFilter.EName;
                            tmpRBACRowFilter.SName = tmp.RBACRowFilter.SName;
                            tmpRBACRowFilter.RowFilterCondition = tmp.RBACRowFilter.RowFilterCondition;
                            tmpRBACRowFilter.StandCode = tmp.RBACRowFilter.StandCode;
                            tmpRBACRowFilter.DeveCode = tmp.RBACRowFilter.DeveCode;
                            tmpRBACRowFilter.PinYinZiTou = tmp.RBACRowFilter.PinYinZiTou;
                            tmpRBACRowFilter.Shortcode = tmp.RBACRowFilter.Shortcode;
                            tmpRBACRowFilter.Comment = tmp.RBACRowFilter.Comment;
                            tmpRBACRowFilter.IsUse = tmp.RBACRowFilter.IsUse;
                            tmpRBACRowFilter.DispOrder = tmp.RBACRowFilter.DispOrder;
                            tmpRBACRowFilter.RowFilterConstruction = tmp.RBACRowFilter.RowFilterConstruction;
                            tmpRBACRowFilter.DataUpdateTime = tmp.RBACRowFilter.DataUpdateTime;
                            tmpRBACRowFilter.DataTimeStamp = tmp.DataTimeStamp;

                            if (tmpInsertRowFilterIDList.IndexOf(value) < 0)
                            {
                                IBRBACRowFilter.Entity = tmpRBACRowFilter;
                                IBRBACRowFilter.Add();
                                tmpInsertRowFilterIDList.Add(value);
                            }
                        }

                        //复制角色权限
                        RBACRoleRight rbacRoleRight = new RBACRoleRight();
                        rbacRoleRight.RBACModuleOper = tmpRBACModuleOper;
                        rbacRoleRight.RBACRole = tmp.RBACRole;
                        rbacRoleRight.RBACRowFilter = tmpRBACRowFilter;
                        rbacRoleRight.IsUse = tmp.IsUse;
                        rbacRoleRight.DispOrder = tmp.DispOrder;
                        rbacRoleRight.Comment = tmp.Comment;

                        this.Entity = rbacRoleRight;
                        this.Add();
                    }
                }
            }
            catch (Exception ex)
            {
                tmpBaseResultDataValue.success = false;
                tmpBaseResultDataValue.ResultDataValue = "错误信息：" + ex.Message;
            }
            return tmpBaseResultDataValue;
        }

        public bool DeleteByRBACModuleOperIDList(List<long> RBACModuleOperIDList)
        {
            string molist = "";
            foreach (var id in RBACModuleOperIDList)
            {
                molist += id + ",";
            }
            if (molist != "")
            {
                molist = molist.Substring(0, molist.LastIndexOf(','));
            }
            DBDao.DeleteByHql(" From RBACRoleRight rbacroleright where rbacroleright.RBACModuleOper.Id in (" + molist + ")");
            return true;
        }
        /// <summary>
        /// 清空相关的角色权限的行数据条件Id值
        /// </summary>
        /// <param name="rowFilterId">行数据条件Id值</param>
        /// <param name="moduleOperId">模块操作Id值</param>
        /// <returns></returns>
        public bool UpdateRBACRoleRightOfClearRowFilterIdByModuleOperId(long rowFilterId, long moduleOperId)
        {
            string hql = "";
            hql = "update RBACRoleRight rbacroleright set RBACRowFilter.Id=null where rbacroleright.RBACRowFilter.Id=" + rowFilterId + " and rbacroleright.RBACModuleOper.Id=" + moduleOperId;
            ZhiFang.Common.Log.Log.Debug("清空相关的角色权限的行数据条件Id值的执行语句--" + hql);
            if (!String.IsNullOrEmpty(hql))
                DBDao.UpdateByHql(hql);
            return true;
        }
        /// <summary>
        /// 删除行数据过滤条件的所有角色访问权限(模块操作)
        /// </summary>
        /// <param name="rowFilterId"></param>
        /// <param name="moduleOperId"></param>
        /// <returns></returns>
        public bool DeleteRBACRoleRightByModuleOperId(long rowFilterId, long moduleOperId)
        {
            //rbacroleright.RBACRole.Id is not null and
            DBDao.DeleteByHql(" From RBACRoleRight rbacroleright where rbacroleright.RBACRowFilter.Id=" + rowFilterId + " and rbacroleright.RBACModuleOper.Id=" + moduleOperId);
            return true;
        }
        #endregion

        #region 获取行数据过滤条件待选角色信息
        /// <summary>
        /// 找出模块操作下已经分配好的角色ID
        /// 预置条件分配用过的角色可以继续在模块服务的行数据条件里选择
        /// </summary>
        /// <param name="moduleOperId"></param>
        /// <returns></returns>
        private string GetRBACRoleRightIdStrByModuleOperID(long moduleOperId, string rowFilterId)
        {
            StringBuilder strb = new StringBuilder();
            IList<RBACRoleRight> tempList = new List<RBACRoleRight>();
            string hql = " rbacroleright.RBACRole.Id is not null and rbacroleright.RBACPreconditions.Id is null and rbacroleright.RBACModuleOper.Id=" + moduleOperId;
            if (!string.IsNullOrEmpty(rowFilterId))
                hql = hql + " and rbacroleright.RBACRowFilter.Id=" + rowFilterId;
            else
                hql = hql + " and rbacroleright.RBACRowFilter.Id is not null ";
            tempList = this.SearchListByHQL(hql);

            foreach (var model in tempList)
            {
                strb.Append(model.RBACRole.Id + ",");
            }
            return strb.ToString().TrimEnd(',');
        }
        /// <summary>
        /// 获取模块操作的行数据条件配置时的待选模块角色信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="sort"></param>
        /// <param name="moduleId"></param>
        /// <param name="moduleOperId"></param>
        /// <returns></returns>
        public EntityList<RBACRoleVO> SearchRBACRoleRightByModuleIdAndModuleOperID(int page, int limit, string sort, long moduleId, long moduleOperId, string where)
        {
            EntityList<RBACRoleVO> tempEntityList = new EntityList<RBACRoleVO>();
            tempEntityList.list = new List<RBACRoleVO>();
            string rowFilterId = "";
            string noInRoleIdStr = GetRBACRoleRightIdStrByModuleOperID(moduleOperId, rowFilterId);
            EntityList<RBACRoleModule> tempList = new EntityList<RBACRoleModule>();
            string hqlWhere = "rbacrolemodule.RBACModule.Id=" + moduleId;
            if (!String.IsNullOrEmpty(noInRoleIdStr))
                hqlWhere += " and rbacrolemodule.RBACRole.Id not in(" + noInRoleIdStr + ")";
            if (!String.IsNullOrEmpty(where))
                hqlWhere = where + " and " + hqlWhere;
            tempList = IBRBACRoleModule.SearchListByHQL(hqlWhere, sort, page, limit);
            tempEntityList.count = tempList.count;
            if (tempList.list != null)
            {
                foreach (var model in tempList.list)
                {
                    RBACRoleVO vo = new RBACRoleVO();
                    vo.Id = model.RBACRole.Id;
                    vo.CName = model.RBACRole.CName;
                    tempEntityList.list.Add(vo);
                }
            }
            return tempEntityList;
        }
        #endregion


        #region 预置条件
        public bool UpdateRBACRoleRightOfClearRowFilterIdByPreconditionsId(long rowFilterId, long preconditionsId)
        {
            string hql = "";
            hql = "update RBACRoleRight rbacroleright set RBACRowFilter.Id=null,RBACPreconditions.Id=null where rbacroleright.RBACRowFilter.Id=" + rowFilterId + " and rbacroleright.RBACPreconditions.Id=" + preconditionsId;
            ZhiFang.Common.Log.Log.Debug("清空相关的角色权限的行数据条件Id值的执行语句--" + hql);
            if (!String.IsNullOrEmpty(hql))
                DBDao.UpdateByHql(hql);
            return true;
        }
        /// <summary>
        /// 删除行数据过滤条件的所有角色访问权限(预置条件)
        /// </summary>
        /// <param name="rowFilterId"></param>
        /// <param name="moduleOperId"></param>
        /// <returns></returns>
        public bool DeleteRBACRoleRightByPreconditionsId(long rowFilterId, long preconditionsId)
        {
            //rbacroleright.RBACRole.Id is not null and 
            DBDao.DeleteByHql(" From RBACRoleRight rbacroleright where rbacroleright.RBACRowFilter.Id=" + rowFilterId + " and rbacroleright.RBACPreconditions.Id=" + preconditionsId);
            return true;
        }
        /// <summary>
        /// 预置条件的行数据过滤条件角色树
        /// </summary>
        /// <param name="preconditionsId"></param>
        /// <returns></returns>
        public BaseResultTree SearchRBACRowFilterTreeByPreconditionsId(long preconditionsId)
        {
            BaseResultTree tempBaseResultTree = new BaseResultTree();
            List<tree> tempListTree = new List<tree>();
            try
            {
                var tempEntity = this.SearchListByHQL(" rbacroleright.RBACPreconditions.Id=" + preconditionsId.ToString(), -1, -1);
                if (tempEntity != null && tempEntity.count > 0)
                {
                    IList<RBACRoleRight> tempRBACRoleRightList = tempEntity.list;
                    IList<string> tempRBACRowFilterID = new List<string>();
                    foreach (RBACRoleRight tempRBACRoleRight in tempRBACRoleRightList)
                    {
                        if (tempRBACRoleRight.RBACRowFilter != null && (!tempRBACRowFilterID.Contains(tempRBACRoleRight.RBACRowFilter.Id.ToString())))
                        {
                            tempRBACRowFilterID.Add(tempRBACRoleRight.RBACRowFilter.Id.ToString());
                            tree tempTree = new tree();
                            //角色权限ID值给Para,方便前台通过获取Para值删除角色权限信息
                            tempTree.Para = tempRBACRoleRight.Id;
                            tempTree.text = tempRBACRoleRight.RBACRowFilter.CName;
                            tempTree.tid = tempRBACRoleRight.RBACRowFilter.Id.ToString();
                            tempTree.pid = "0";
                            tempTree.objectType = "RBACRowFilter";
                            tempTree.expanded = true;
                            tempTree.Tree = GetChildTree(tempRBACRoleRight.RBACRowFilter.Id, tempRBACRoleRightList, true);
                            tempTree.leaf = (tempTree.Tree.Count <= 0);
                            //tempTree.iconCls = tempTree.leaf ? "orgImg16" : "orgsImg16";
                            tempTree.iconCls = "orgsImg16";
                            tempListTree.Add(tempTree);
                        }
                    }
                }
                tempBaseResultTree.Tree = tempListTree;
                tempBaseResultTree.success = true;
            }
            catch (Exception ex)
            {
                tempBaseResultTree.success = false;
                tempBaseResultTree.ErrorInfo = ex.Message;
            }
            return tempBaseResultTree;
        }
        /// <summary>
        /// 找出预置条件下已经分配好的角色ID
        /// </summary>
        /// <param name="preconditionsId"></param>
        /// <param name="rowFilterId"></param>
        /// <returns></returns>
        private string GetRBACRoleRightIdStrByPreconditionsId(long preconditionsId, string rowFilterId)
        {
            StringBuilder strb = new StringBuilder();
            IList<RBACRoleRight> tempList = new List<RBACRoleRight>();
            string hql = "rbacroleright.RBACRole.Id is not null" + " and rbacroleright.RBACPreconditions.Id = " + preconditionsId;
            if (!string.IsNullOrEmpty(rowFilterId))
                hql = hql + " and rbacroleright.RBACRowFilter.Id=" + rowFilterId;
            else
                hql = hql + " and rbacroleright.RBACRowFilter.Id is not null ";
            tempList = this.SearchListByHQL(hql);
            foreach (var model in tempList)
            {
                if (model.RBACRole != null)
                {
                    if (!strb.ToString().Contains(model.RBACRole.Id.ToString()))
                        strb.Append(model.RBACRole.Id + ",");
                }
            }
            return strb.ToString().TrimEnd(',');
        }
        /// <summary>
        /// 获取预置条件配置时的待选模块角色信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="sort"></param>
        /// <param name="moduleId"></param>
        /// <param name="preconditionsId"></param>
        /// <returns></returns>
        public EntityList<RBACRoleVO> SearchRBACRoleRightByModuleIdAndPreconditionsId(int page, int limit, string sort, long moduleId, long preconditionsId, string where, string rowFilterId)
        {
            EntityList<RBACRoleVO> tempEntityList = new EntityList<RBACRoleVO>();
            tempEntityList.list = new List<RBACRoleVO>();
            string noInRoleIdStr = GetRBACRoleRightIdStrByPreconditionsId(preconditionsId, rowFilterId);
            EntityList<RBACRoleModule> tempList = new EntityList<RBACRoleModule>();
            string hqlWhere = "rbacrolemodule.RBACModule.Id=" + moduleId;
            if (!String.IsNullOrEmpty(noInRoleIdStr))
                hqlWhere += " and rbacrolemodule.RBACRole.Id not in(" + noInRoleIdStr + ")";
            if (!String.IsNullOrEmpty(where))
                hqlWhere = where + " and " + hqlWhere;
            tempList = IBRBACRoleModule.SearchListByHQL(hqlWhere, sort, page, limit);
            tempEntityList.count = tempList.count;
            if (tempList.list != null)
            {
                foreach (var model in tempList.list)
                {
                    RBACRoleVO vo = new RBACRoleVO();
                    vo.Id = model.RBACRole.Id;
                    vo.CName = model.RBACRole.CName;
                    tempEntityList.list.Add(vo);
                }
            }
            return tempEntityList;
        }
        #endregion
    }
}