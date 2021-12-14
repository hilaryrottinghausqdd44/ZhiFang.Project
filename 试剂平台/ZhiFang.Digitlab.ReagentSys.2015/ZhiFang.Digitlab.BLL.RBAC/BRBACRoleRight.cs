using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.IDAO;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IBLL.RBAC;
using Spring.Context;
using Spring.Context.Support;


namespace ZhiFang.Digitlab.BLL.RBAC
{	
	public class BRBACRoleRight : ZhiFang.Digitlab.BLL.BaseBLL<ZhiFang.Digitlab.Entity.RBACRoleRight>, IBRBACRoleRight
    {
        public IBRBACRole IBRBACRole { set; get; }

        public IBRBACModuleOper IBRBACModuleOper { set; get; }

        public IBRBACRowFilter IBRBACRowFilter { get; set; }

        public IList<RBACRoleRight> GetRBACRoleRightByRBACUserCodeAndModuleOperID(string strUserCode, long longModuleOperID)
        {
            return ((IDRBACRoleRightDao)base.DBDao).GetRBACRoleRightByRBACUserCodeAndModuleOperID(strUserCode,longModuleOperID);      
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

            bool r =false;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="longModuleOperID"></param>
        /// <returns></returns>
        public BaseResultTree SearchRBACRowFilterTreeByModuleOperID(long longModuleOperID)
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
                    if (rbacRowFilter != null)
                        defaultRBACRowFilterID = rbacRowFilter.Id;
                    foreach (RBACRoleRight tempRBACRoleRight in tempRBACRoleRightList)
                    {
                        if (tempRBACRoleRight.RBACRowFilter != null && (!tempRBACRowFilterID.Contains(tempRBACRoleRight.RBACRowFilter.Id.ToString())))
                        {
                            tempRBACRowFilterID.Add(tempRBACRoleRight.RBACRowFilter.Id.ToString());
                            tree tempTree = new tree();
                            if (defaultRBACRowFilterID > 0 && defaultRBACRowFilterID == tempRBACRoleRight.RBACRowFilter.Id)
                            {
                                tempTree.text = "(默认)" + tempRBACRoleRight.RBACRowFilter.CName;
                                isdefaultRBACRowFilter = true;
                            }
                            else
                                tempTree.text = tempRBACRoleRight.RBACRowFilter.CName;
                            tempTree.tid = tempRBACRoleRight.RBACRowFilter.Id.ToString();
                            tempTree.pid = "0";
                            tempTree.objectType = "RBACRowFilter";
                            tempTree.expanded = true;
                            tempTree.Tree = GetChildTree(tempRBACRoleRight.RBACRowFilter.Id, tempRBACRoleRightList);
                            tempTree.leaf = (tempTree.Tree.Count <= 0);
                            //tempTree.iconCls = tempTree.leaf ? "orgImg16" : "orgsImg16";
                            tempTree.iconCls = "orgsImg16";
                            tempListTree.Add(tempTree);
                        }
                    }
                    if (rbacRowFilter != null && (!isdefaultRBACRowFilter))
                    {
                        tree tempTree = new tree();
                        tempTree.text = "(默认)" + rbacRowFilter.CName;
                        tempTree.tid = rbacRowFilter.Id.ToString();
                        tempTree.pid = "0";
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

        public List<tree> GetChildTree(long ParentID, IList<RBACRoleRight> rbacRoleRightList)
        {
            List<tree> tempListTree = new List<tree>();
            try
            {
                IList<RBACRoleRight> tempEntityList = rbacRoleRightList.Where(p => (p.RBACRowFilter != null && p.RBACRowFilter.Id == ParentID)).ToList();
                if (tempEntityList != null && tempEntityList.Count > 0)
                {
                    foreach (RBACRoleRight tempRBACRoleRight in tempEntityList)
                    {
                        if (tempRBACRoleRight.RBACRole != null)
                        {
                            tree tempTree = new tree();
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
        /// 复制角色权限
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
    } 
}