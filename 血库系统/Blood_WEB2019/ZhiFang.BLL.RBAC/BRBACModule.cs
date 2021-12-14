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
    public class BRBACModule : BaseBLL<ZhiFang.Entity.RBAC.RBACModule>, ZhiFang.IBLL.RBAC.IBRBACModule
    {
        public IDRBACRoleModuleDao IDRBACRoleModuleDao { get; set; }
        public IBRBACModuleOper IBRBACModuleOper { get; set; }

        //public IBRBACModuleOper IBRBACModuleOper { get; set; }
        #region IBRBACModule 成员

        public IList<RBACModule> SearchModuleByModuleOperID(long longModuleOperID)
        {
            return ((IDRBACModuleDao)base.DBDao).SearchModuleByModuleOperID(longModuleOperID);
        }

        public IList<RBACModule> SearchModuleByRoleID(long longRoleID)
        {
            return ((IDRBACModuleDao)base.DBDao).SearchModuleByRoleID(longRoleID);
        }

        public IList<RBACModule> SearchModuleByHREmpID(long longHREmpID)
        {
            return ((IDRBACModuleDao)base.DBDao).SearchModuleByHREmpID(longHREmpID);
        }

        public override bool Update(string[] strParas)
        {
            long moduleid = 0;
            long newpid = 0;
            foreach (string s in strParas)
            {
                if (s.Trim().IndexOf("Id=") >= 0 && s.Trim().IndexOf(".Id=") < 0)
                {
                    if (s.Trim().Split('=')[0].Trim() == "Id")
                        moduleid = long.Parse(s.Trim().Split('=')[1].Trim());
                }
                if (s.Trim().IndexOf("ParentID=") >= 0)
                {
                    newpid = long.Parse(s.Trim().Split('=')[1].Trim());
                }
            }
            RBACModule rbacmodule = DBDao.Get(moduleid);
            #region 更新老权限
            if (newpid != rbacmodule.ParentID)
            {
                UpdateOldRole(rbacmodule);
            }
            #endregion
            #region 更新新权限
            //IList<RBACRoleModule> rbacrolemodulelist = rbacmodule.RBACRoleModuleList;
            IList<RBACRoleModule> rbacrolemodulelist = IDRBACRoleModuleDao.GetListByHQL(" rbacrolemodule.RBACModule.Id=" + rbacmodule.Id, 0, 0).list;
            UpdateNewRole(rbacrolemodulelist, newpid);
            #endregion
            return base.Update(strParas);
        }

        public bool UpdateSingleFields(string[] strParas)
        {
            return base.Update(strParas);
        }

        private void UpdateOldRole(RBACModule rbacmodule)
        {
            //IList<RBACRoleModule> rbacmoduleList = rbacmodule.RBACRoleModuleList;
            long? pid = rbacmodule.ParentID;
            if (pid != null && pid.Value > 0)
            {
                RBACModule oprbacmodule = DBDao.Get(pid.Value);//父节点实体
                IList<RBACModule> subrbacmodulelist = DBDao.GetListByHQL(" rbacmodule.ParentID=" + pid.ToString() + " and rbacmodule.Id <>" + rbacmodule.Id.ToString(), 0, 0).list;//取父节点的所有子节点（不包括自己）：兄弟节点。
                //IList<RBACRoleModule> rbacrolemodulelist = rbacmodule.RBACRoleModuleList;
                IList<RBACRoleModule> rbacrolemodulelist = IDRBACRoleModuleDao.GetListByHQL(" rbacrolemodule.RBACModule.Id=" + rbacmodule.Id, 0, 0).list;//当前模块的角色模块列表
                foreach (var r in rbacrolemodulelist)
                {
                    bool flag = false;
                    foreach (var s in subrbacmodulelist)
                    {
                        int count = IDRBACRoleModuleDao.GetListCountByHQL(" rbacrolemodule.RBACRole.Id =" + r.RBACRole.Id + " and rbacrolemodule.RBACModule.Id =" + s.Id);//判断有当前模块权限的角色有没有兄弟模块的权限。
                        flag = count > 0;
                        if (flag)
                        {
                            break;
                        }
                    }
                    if (!flag)
                    {
                        UpdateOldRole(oprbacmodule);
                        IDRBACRoleModuleDao.DeleteByHql(" from RBACRoleModule rbacrolemodule where rbacrolemodule.RBACRole.Id =" + r.RBACRole.Id + " and rbacrolemodule.RBACModule.Id =" + pid);
                    }
                }
            }
        }

        private void UpdateNewRole(IList<RBACRoleModule> rbacrolemodulelist, long newpid)
        {
            if (newpid > 0)
            {
                RBACModule newprbacmodule = DBDao.Get(newpid);
                foreach (var r in rbacrolemodulelist)
                {
                    bool flag = false;
                    int count = IDRBACRoleModuleDao.GetListCountByHQL(" rbacrolemodule.RBACRole.Id =" + r.RBACRole.Id + " and rbacrolemodule.RBACModule.Id =" + newpid);
                    flag = count > 0;
                    //break;
                    if (!flag)
                    {
                        IDRBACRoleModuleDao.Save(new RBACRoleModule() { RBACRole = r.RBACRole, RBACModule = newprbacmodule });
                        if (newprbacmodule.ParentID.Value == 0)
                        {
                            continue;
                        }
                        else
                        {
                            UpdateNewRole(rbacrolemodulelist, newprbacmodule.ParentID.Value);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 根据员工ID返回树列表
        /// </summary>
        /// <param name="longHREmpID"></param>
        /// <returns></returns>
        public BaseResultTree SearchModuleTreeByHREmpID(long longHREmpID)
        {
            BaseResultTree result = new BaseResultTree();
            result.success = true;
            List<tree> listTree = new List<tree>();

            try
            {
                IList<RBACModule> empList = SearchModuleByHREmpID(longHREmpID);

                IList<RBACModule> allList = this.SearchListByHQL(" 1=1 and IsUse=true", " DispOrder ", -1, -1).list;
                IList<RBACModule> tmplist = this.SearchListByHQL(" rbacmodule.ParentID=0 and IsUse=true ", " DispOrder ", -1, -1).list;
                listTree = GetLeafTrees(tmplist, longHREmpID, empList, allList);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.ErrorInfo = ex.Message;
            }

            result.Tree = listTree;
            return result;
        }

        public BaseResultTree SearchModuleTreeByRBACRoleID(long longRBACRoleID)
        {
            BaseResultTree result = new BaseResultTree();
            result.success = true;
            List<tree> listTree = new List<tree>();

            try
            {
                IList<RBACModule> empList = ((IDRBACModuleDao)base.DBDao).SearchModuleByRoleID(longRBACRoleID);
                IList<RBACModule> allList = this.SearchListByHQL(" 1=1", " DispOrder ", -1, -1).list;
                IList<RBACModule> tmplist = this.SearchListByHQL(" rbacmodule.ParentID=0 ", " DispOrder ", -1, -1).list;
                listTree = GetLeafTrees(tmplist, 0, empList, allList);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.ErrorInfo = ex.Message;
            }
            result.Tree = listTree;
            return result;
        }

        public List<tree> GetParentNodeTree(RBACModule entity, List<tree> subTree)
        {
            List<tree> listTree = new List<tree>();
            if (entity.ParentID != 0)
            {
                tree tree = new tree();
                tree.text = entity.CName;
                tree.url = entity.Url;
                tree.icon = entity.PicFile;
                tree.tid = entity.Id.ToString();
                tree.pid = entity.ParentID.ToString();
                tree.Tree = listTree;
                tree.Tree = subTree;

                List<tree> newList = new List<tree>();
                newList.Add(tree);
                listTree = GetParentNodeTree(this.Get(Convert.ToInt64(entity.ParentID)), newList);
            }
            else
            {
                tree tree = new tree();
                tree.text = entity.CName;
                tree.url = entity.Url;
                tree.icon = entity.PicFile;
                tree.tid = entity.Id.ToString();
                tree.pid = entity.ParentID.ToString();
                tree.Tree = subTree;
                listTree.Add(tree);
            }

            return listTree;
        }

        public List<tree> GetLeafTrees(IList<RBACModule> list, long longHREmpID, IList<RBACModule> empList, IList<RBACModule> allList)
        {
            if (list.Count == 0)
            {
                return null;
            }
            List<tree> listTree = new List<tree>();

            foreach (RBACModule m in list)
            {
                if (empList.Contains(m))
                {
                    tree tree = new tree();
                    tree.text = m.CName;
                    tree.url = m.Url;
                    tree.icon = m.PicFile;
                    tree.tid = m.Id.ToString();
                    tree.pid = m.ParentID.ToString();
                    tree.expanded = true;

                    IList<RBACModule> tmplist = allList.Where(p => p.ParentID == m.Id).OrderBy(p => p.DispOrder).ToList();
                    if (tmplist.Count > 0)     //还有子节点
                    {
                        var treeList = GetLeafTrees(tmplist, longHREmpID, empList, allList);
                        tree.Tree = treeList;
                        if (treeList.Count > 0)
                        {
                            tree.leaf = false;
                        }
                        else
                        {
                            tree.leaf = true;
                        }
                        //if (tree.Tree.Count != 0)
                        //{

                        listTree.Add(tree);

                        //}
                    }
                    else                                  //无子节点
                    {
                        //if (empList.Contains(m))
                        //{
                        tree.leaf = true;
                        listTree.Add(tree);
                        //}
                    }
                }
            }
            return listTree;
        }

        public IList<RBACModule> SearchModuleByUserCode(string strUserCode)
        {
            return ((IDRBACModuleDao)base.DBDao).SearchModuleByUserCode(strUserCode);
        }

        public IList<RBACModule> SearchModuleByHREmpIDAndModuleID(long longHREmpID, long longModuleID)
        {
            return ((IDRBACModuleDao)base.DBDao).SearchModuleByHREmpIDAndModuleID(longHREmpID, longModuleID);
        }

        public override bool Remove(long longModuleID)
        {
            bool flag = false;
            if (this.SearchListByHQL(" rbacmodule.ParentID=" + longModuleID, -1, -1).count > 0)
            {
                throw new Exception("模块为非叶子节点不能删除！");
            }
            else
            {
                flag = ((IDRBACModuleDao)base.DBDao).Delete(longModuleID);
            }
            return flag;
        }

        public BaseResultTree<RBACModule> SearchRBACModuleToTree(long RBACModuleID, bool IsListTree)
        {
            if (RBACModuleID < 0)
            {
                RBACModuleID = 0;
            }
            EntityList<RBACModule> tmplist = this.SearchListByHQL(" rbacmodule.ParentID= " + RBACModuleID, " DispOrder ", -1, -1);


            BaseResultTree<RBACModule> ListTreeRoot = new BaseResultTree<RBACModule>();
            ListTreeRoot.Tree = new List<tree<RBACModule>>();

            if ((tmplist != null) && (tmplist.list != null) && (tmplist.list.Count > 0))
            {
                for (int i = 0; i < tmplist.count; i++)
                {
                    tree<RBACModule> tree = new tree<RBACModule>();
                    tree.text = tmplist.list[i].CName;
                    tree.url = tmplist.list[i].Url;
                    tree.icon = tmplist.list[i].PicFile;
                    tree.tid = tmplist.list[i].Id.ToString();
                    tree.pid = RBACModuleID.ToString();
                    tree.Para = tmplist.list[i].Para;
                    if (IsListTree)
                    {
                        tmplist.list[i].RBACModuleOperList = null;
                        tmplist.list[i].RBACRoleModuleList = null;
                        tmplist.list[i].RBACEmpOptionsList = null;
                        //tmplist.list[i].BTDAppComponents = null;
                        tree.value = tmplist.list[i];
                    }
                    try
                    {
                        var treeList = GetRBACModuleLeafTrees<RBACModule>(tmplist.list[i].Id, IsListTree);
                        tree.Tree = treeList;
                        if (treeList.Count() > 0)
                        {
                            tree.leaf = false;
                        }
                        else
                        {
                            tree.leaf = true;
                        }

                        //if (treeList.Length > 0)
                        //{
                        //    tree.leaf = false;
                        //    tree.Tree = treeList;
                        //}
                        //else
                        //{
                        //    tree.leaf = true;
                        //    tree.Tree = new tree<RBACModule>[] { };
                        //}
                        tree.expanded = true;
                        ListTreeRoot.Tree.Add(tree);
                    }
                    catch (Exception ex)
                    {
                        ZhiFang.Common.Log.Log.Error("SearchRBACModuleToTree.Error:" + ex.StackTrace);
                        throw ex;
                    }

                }
            }
            return ListTreeRoot;
        }

        public tree<RBACModule>[] GetRBACModuleLeafTrees<T>(long ParentID, bool IsListTree)
        {
            List<tree<RBACModule>> listTree = new List<tree<RBACModule>>();
            try
            {
                EntityList<RBACModule> tmplist = this.SearchListByHQL(" rbacmodule.ParentID= " + ParentID, " DispOrder ", -1, -1);
                if ((tmplist != null) && (tmplist.list != null) && (tmplist.list.Count > 0))
                {
                    for (int i = 0; i < tmplist.count; i++)
                    {
                        tree<RBACModule> tree = new tree<RBACModule>();
                        tree.text = tmplist.list[i].CName;
                        tree.url = tmplist.list[i].Url;
                        tree.icon = tmplist.list[i].PicFile;
                        tree.tid = tmplist.list[i].Id.ToString();
                        tree.pid = ParentID.ToString();
                        tree.Para = tmplist.list[i].Para;
                        if (IsListTree)
                        {
                            tmplist.list[i].RBACModuleOperList = null;
                            tmplist.list[i].RBACRoleModuleList = null;
                            tmplist.list[i].RBACEmpOptionsList = null;
                            //tmplist.list[i].BTDAppComponents = null;
                            tree.value = tmplist.list[i];
                        }
                        var treeList = GetRBACModuleLeafTrees<RBACModule>(tmplist.list[i].Id, IsListTree);
                        tree.Tree = treeList;
                        if (treeList.Length > 0)
                        {
                            tree.leaf = false;
                        }
                        else
                        {
                            tree.leaf = true;
                            //tree.Tree = new tree<RBACModule>[] { };
                        }
                        listTree.Add(tree);
                    }
                }
                //return listTree.ToArray();
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("GetRBACModuleLeafTrees.Error:" + ex.StackTrace);
                throw ex;
            }
            if (listTree.Count > 0)
            {
                return listTree.ToArray();
            }
            else
            {
                tree<RBACModule>[] arr = new tree<RBACModule>[] { };
                return arr;
            }
        }

        public RBACModule SearchModuleByUseCode(string UseCode)
        {
            return ((IDRBACModuleDao)base.DBDao).SearchModuleByUseCode(UseCode);
        }

        public bool DeleteModuleByUseCode(string UseCode)
        {
            RBACModule rbacm = ((IDRBACModuleDao)base.DBDao).SearchModuleByUseCode(UseCode);
            if (rbacm != null)
            {
                if (!IBRBACModuleOper.DeleteByRBACModuleId(rbacm.Id))
                {
                    return false;
                    throw new Exception("删除模块操作失败！");
                }
                if (!IDRBACRoleModuleDao.DeleteByRBACModuleId(rbacm.Id))
                {
                    return false;
                    throw new Exception("删除角色模块权限失败！");
                }
                if (!DBDao.Delete(rbacm.Id))
                {
                    return false;
                    throw new Exception("删除模块失败！");
                }
            }
            return true;
        }

        #endregion

    }
}