using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.RBAC;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.RBAC;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LIIP;

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
                if (s.Trim().IndexOf("ParentID=") >= 0 )
                {
                    newpid = long.Parse(s.Trim().Split('=')[1].Trim());
                }
            }
            RBACModule rbacmodule=DBDao.Get(moduleid);
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

            //try
            //{
            //    IList<long> listParent = new List<long>();
            //    IList<RBACModule> list = SearchModuleByHREmpID(longHREmpID);
            //    foreach (RBACModule tmp in list)
            //    {
            //        GetParentNodeTree(tmp, null);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    result.success = false;
            //    result.ErrorInfo = ex.Message;
            //}
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
                tree.ComponentsListJson = entity.ComponentsListJson;
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
                tree.ComponentsListJson = entity.ComponentsListJson;
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
                    tree.Para = m.Para!=null?m.Para.ToString():"";
                    tree.ComponentsListJson = m.ComponentsListJson;
                    tree.expanded = true;

                    IList<RBACModule> tmplist = allList.Where(p => p.ParentID == m.Id).OrderBy(p => p.DispOrder).ToList();
                    if (tmplist.Count > 0)     //还有子节点
                    {
                        tree.Tree = GetLeafTrees(tmplist, longHREmpID, empList, allList);
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
            if (this.SearchListByHQL(" rbacmodule.ParentID="+longModuleID,-1,-1).count>0 )
            {
                throw new Exception("模块为非叶子节点不能删除！");
            }
            else
            {
                flag=((IDRBACModuleDao)base.DBDao).Delete(longModuleID);
            }
            return flag;
        }
        /// <summary>
        /// 根据where条件查询模块树节点
        /// </summary>
        /// <param name="where"></param>
        /// <param name="IsListTree"></param>
        /// <returns></returns>
        public BaseResultTree<RBACModule> SearchRBACModuleToTree(string where, bool IsListTree) {
           
            EntityList<RBACModule> tmplist = this.SearchListByHQL(where, " DispOrder ", -1, -1);
            //EntityList<RBACModule> tmplist = new EntityList<RBACModule>();
            //tmplist.count = 1;
            //RBACModule a = new RBACModule() { Id = 1, CName = "测试" };
            //RBACModule aa = new RBACModule() { Id = 2,ParentID=1, CName = "测试" };
            //BTDAppComponents b=new BTDAppComponents(){Id=11,CName="应用"};
            //aa.BTDAppComponents=b;

            //BaseResultTree<Entity.RBACModule> ListTreeRoot = new BaseResultTree<RBACModule>();
            //ListTreeRoot.Tree = new List<tree<RBACModule>>();
            //tree<Entity.RBACModule> tree = new Entity.tree<Entity.RBACModule>();
            //tree.text = tmplist.list[i].CName;
            //tree.url = tmplist.list[i].Url;
            //tree.icon = tmplist.list[i].PicFile;
            //tree.tid = tmplist.list[i].Id.ToString();
            //tree.pid = RBACModuleID.ToString();



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
                    tree.Para = tmplist.list[i].Para;
                    tree.ComponentsListJson = tmplist.list[i].ComponentsListJson;
                    if (IsListTree)
                    {
                        tmplist.list[i].RBACModuleOperList = null;
                        tmplist.list[i].RBACRoleModuleList = null;
                        tmplist.list[i].RBACEmpOptionsList = null;
                        //tmplist.list[i].BTDAppComponents = null;
                        tree.value = tmplist.list[i];
                    }
                    tree.Tree = GetRBACModuleLeafTrees<RBACModule>(tmplist.list[i].Id, IsListTree);
                    if (tree.Tree.Length > 0)
                    {
                        tree.leaf = false;
                    }
                    else
                    {
                        tree.leaf = true;
                    }
                    tree.expanded = true;
                    ListTreeRoot.Tree.Add(tree);
                }
            }
            return ListTreeRoot;
        }

        public BaseResultTree<RBACModule> SearchRBACModuleToTree(long RBACModuleID,bool IsListTree)
        {
            if (RBACModuleID < 0)
            {
                RBACModuleID=0;
            }
            EntityList<RBACModule> tmplist = this.SearchListByHQL(" rbacmodule.ParentID= " + RBACModuleID, " DispOrder ", -1, -1);
            //EntityList<RBACModule> tmplist = new EntityList<RBACModule>();
            //tmplist.count = 1;
            //RBACModule a = new RBACModule() { Id = 1, CName = "测试" };
            //RBACModule aa = new RBACModule() { Id = 2,ParentID=1, CName = "测试" };
            //BTDAppComponents b=new BTDAppComponents(){Id=11,CName="应用"};
            //aa.BTDAppComponents=b;

            //BaseResultTree<Entity.RBACModule> ListTreeRoot = new BaseResultTree<RBACModule>();
            //ListTreeRoot.Tree = new List<tree<RBACModule>>();
            //tree<Entity.RBACModule> tree = new Entity.tree<Entity.RBACModule>();
            //tree.text = tmplist.list[i].CName;
            //tree.url = tmplist.list[i].Url;
            //tree.icon = tmplist.list[i].PicFile;
            //tree.tid = tmplist.list[i].Id.ToString();
            //tree.pid = RBACModuleID.ToString();



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
                    tree.ComponentsListJson = tmplist.list[i].ComponentsListJson;
                    if (IsListTree)
                    {
                        tmplist.list[i].RBACModuleOperList = null;
                        tmplist.list[i].RBACRoleModuleList = null;
                        tmplist.list[i].RBACEmpOptionsList = null;
                        //tmplist.list[i].BTDAppComponents = null;
                        tree.value = tmplist.list[i];
                    }
                    tree.Tree = GetRBACModuleLeafTrees<RBACModule>(tmplist.list[i].Id, IsListTree);
                    if (tree.Tree.Length > 0)
                    {
                        tree.leaf = false;
                    }
                    else
                    {
                        tree.leaf = true;
                    }
                    tree.expanded = true;
                    ListTreeRoot.Tree.Add(tree);
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
                        tree.ComponentsListJson = tmplist.list[i].ComponentsListJson;
                        if (IsListTree)
                        {
                            tmplist.list[i].RBACModuleOperList = null;
                            tmplist.list[i].RBACRoleModuleList = null;
                            tmplist.list[i].RBACEmpOptionsList = null;
                            //tmplist.list[i].BTDAppComponents = null;
                            tree.value = tmplist.list[i];
                        }                        
                        tree.Tree = GetRBACModuleLeafTrees<RBACModule>(tmplist.list[i].Id, IsListTree);
                        if (tree.Tree.Length > 0)
                        {
                            tree.leaf = false;
                        }
                        else
                        {
                            tree.leaf = true;
                        }
                        listTree.Add(tree);
                    }
                }
            }
            catch (Exception ex)
            { }
            return listTree.ToArray();
        }

        public RBACModule SearchModuleByUseCode(string UseCode)
        {
            return ((IDRBACModuleDao)base.DBDao).SearchModuleByUseCode(UseCode);
        }

        public bool DeleteModuleByUseCode(string UseCode)
        {
            RBACModule rbacm=((IDRBACModuleDao)base.DBDao).SearchModuleByUseCode(UseCode);
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

        public bool BModuleGetAndAdd(long bModuleId, string bModule, ref long id)
        {
            if (bModuleId==0)
            {
                var bModules =  DBDao.GetListByHQL("StandCode='"+ ModuleType.统计系统.Value.Code + "'");
                if (bModules.Count <= 0)
                {
                    bModuleId = (long)DBDao.SaveByEntity(new RBACModule()
                    {
                        ParentID=0,
                        ModuleType = 3,
                        PicFile = "package.PNG",
                        CName = "统计系统",
                        StandCode = ModuleType.统计系统.Value.Code,
                        IsUse = true
                    });
                }
                else
                {
                    bModuleId = bModules.First().Id;
                }
            }
            if (bModuleId > 0)
            {
                RBACModule rBACModule = ZhiFang.Common.Public.JsonHelp.DeserializeObject<RBACModule>(bModule);
                rBACModule.ParentID = bModuleId;
                id = (long)DBDao.SaveByEntity(rBACModule);
            }
            else
            {
                throw new Exception("BModuleGetAndAdd: 新增父模块失败!");
            }
            return id>0;
        }

        public BaseResultTree SearchModuleTreeTwoStageByHREmpID(long longHREmpID, long ModuleID)
        {
            BaseResultTree result = new BaseResultTree();
            result.success = true;
            List<tree> listTree = new List<tree>();
            try
            {
                if (ModuleID != 0)
                {
                    IList<RBACModule> empList = SearchModuleByHREmpID(longHREmpID);
                    IList<RBACModule> allList = this.SearchListByHQL(" 1=1 and IsUse=true", " DispOrder ", -1, -1).list;
                    IList<RBACModule> tmplist = this.SearchListByHQL(" rbacmodule.ParentID="+ ModuleID + " and IsUse=true ", " DispOrder ", -1, -1).list;
                    listTree = GetLeafTreeOneStage(tmplist, longHREmpID, empList, allList);
                }
                else {
                    IList<RBACModule> empList = SearchModuleByHREmpID(longHREmpID);
                    IList<RBACModule> allList = this.SearchListByHQL(" 1=1 and IsUse=true", " DispOrder ", -1, -1).list;
                    IList<RBACModule> tmplist = this.SearchListByHQL(" rbacmodule.ParentID=0 and IsUse=true ", " DispOrder ", -1, -1).list;
                    listTree = GetLeafTreeOneStage(tmplist, longHREmpID, empList, allList);
                }
               
            }
            catch (Exception ex)
            {
                result.success = false;
                result.ErrorInfo = ex.Message;
            }
            result.Tree = listTree;
            return result;
        }

        public BaseResultTree SearchModuleTreeTwoStageByHREmpID(long longHREmpID, string ModuleCode)
        {
            BaseResultTree result = new BaseResultTree();
            result.success = true;
            List<tree> listTree = new List<tree>();
            try
            {
                if (String.IsNullOrEmpty(ModuleCode))
                {
                    return SearchModuleTreeTwoStageByHREmpID(longHREmpID, 0);
                }
                else
                {
                    var tmpmodulelist = DBDao.GetListByHQL($" StandCode='{ModuleCode}' ");
                    if (tmpmodulelist != null && tmpmodulelist.Count > 0)
                        return SearchModuleTreeTwoStageByHREmpID(longHREmpID, tmpmodulelist.First().Id);
                    else
                        return SearchModuleTreeTwoStageByHREmpID(longHREmpID, 0);
                }
            }
            catch (Exception ex)
            {
                result.success = false;
                result.ErrorInfo = ex.Message;
            }
            result.Tree = listTree;
            return result;
        }
        public List<tree> GetLeafTreeOneStage(IList<RBACModule> list, long longHREmpID, IList<RBACModule> empList, IList<RBACModule> allList) {
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
                    tree.Para = m.Para != null ? m.Para.ToString() : "";
                    tree.ComponentsListJson = m.ComponentsListJson;
                    tree.expanded = true;
                    IList<RBACModule> tmplist = allList.Where(p => p.ParentID == m.Id).OrderBy(p => p.DispOrder).ToList();
                    if (tmplist.Count > 0)     //还有子节点
                    {
                        List<RBACModule>  rBACModules = GetLeafTreeTwoStage(tmplist, longHREmpID, empList, allList);
                        List<tree> stree = new List<tree>();
                        foreach (var item in rBACModules)
                        {
                            tree tree1 = new tree();
                            tree1.text = item.CName;
                            tree1.url = item.Url;
                            tree1.icon = item.PicFile;
                            tree1.tid = item.Id.ToString();
                            tree1.pid = m.Id.ToString();
                            tree1.Para = item.Para != null ? item.Para.ToString() : "";
                            tree1.ComponentsListJson = item.ComponentsListJson;
                            tree1.expanded = true;
                            stree.Add(tree1);
                        }
                        tree.Tree = stree;
                        listTree.Add(tree);
                    }
                    else //无子节点
                    {
                        tree.leaf = true;
                        listTree.Add(tree);
                    }
                }
            }
            return listTree;
        }

        public List<RBACModule> GetLeafTreeTwoStage(IList<RBACModule> list, long longHREmpID, IList<RBACModule> empList, IList<RBACModule> allList)
        {
            if (list.Count == 0)
            {
                return null;
            }
            List<RBACModule> rBACModules = new List<RBACModule>();
            foreach (RBACModule m in list)
            {
                if (empList.Contains(m))
                {
                    IList<RBACModule> tmplist = allList.Where(p => p.ParentID == m.Id).OrderBy(p => p.DispOrder).ToList();
                    if (tmplist.Count > 0)     //还有子节点
                    {
                        rBACModules = GetLeafTreeTwoStage(tmplist, longHREmpID, empList, allList);
                    }
                    else                                  //无子节点
                    { 
                        rBACModules.Add(m);
                    }
                }
            }
            return rBACModules;
        }

        public List<Ttree<RBACModule>> SearchRBACModule_IncludePModule(string where, bool IsListTree)
        {
            EntityList<RBACModule> tmplist = this.SearchListByHQL(where, " DispOrder ", -1, -1);

            List<Ttree<RBACModule>> ListTreeRoot = new List<Ttree<RBACModule>>();

            if ((tmplist != null) && (tmplist.list != null) && (tmplist.list.Count > 0))
            {
                for (int i = 0; i < tmplist.count; i++)
                {
                    Ttree<RBACModule> tree = new Ttree<RBACModule>();
                    tree.text = tmplist.list[i].CName;
                    tree.url = tmplist.list[i].Url;
                    tree.icon = tmplist.list[i].PicFile;
                    tree.tid = tmplist.list[i].Id.ToString();
                    tree.Para = tmplist.list[i].Para;
                    tree.ComponentsListJson = tmplist.list[i].ComponentsListJson;
                    if (tmplist.list[i].ParentID.HasValue)
                    {
                        tree.pid = tmplist.list[i].ParentID.Value.ToString();
                    }
                    if (IsListTree)
                    {
                        tmplist.list[i].RBACModuleOperList = null;
                        tmplist.list[i].RBACRoleModuleList = null;
                        tmplist.list[i].RBACEmpOptionsList = null;
                        //tmplist.list[i].BTDAppComponents = null;
                        tree.value = tmplist.list[i];
                    }
                    //判断父ID是否在树上,如果是则直接加入到父节点下的Tree里,不是则重新查找


                    //返回一个模块的父级树.如果有重复父级树,则把模块添加到相关树下
                    List<Ttree<RBACModule>> tmpttree = GetRBACModule_IncludePModule<RBACModule>(tree,ListTreeRoot,IsListTree);
                }
            }
            return ListTreeRoot;
        }

        private List<Ttree<RBACModule>> GetRBACModule_IncludePModule<RBACModule>(Ttree<RBACModule> SubTree, List<Ttree<RBACModule>> RootTreeList, bool isListTree)
        {
            if (RootTreeList == null)
            {
                RootTreeList = new List<Ttree<RBACModule>>();
            }

            if (ModuleTreeContains(SubTree, RootTreeList))
            {
                return RootTreeList;
            }

            if (string.IsNullOrEmpty(SubTree.pid) || SubTree.pid.Trim() == "0")
            {
                RootTreeList.Add(SubTree);
                return RootTreeList;
            }
            var pmodule = DBDao.Get(long.Parse(SubTree.pid.Trim()));
            if (pmodule == null)
            {
                throw new Exception($"GetRBACModule_IncludePModule,异常:父模块ID:{SubTree.pid},未能找到相关数据!");
            }
            if (ModuleTreeContainsAndAddSubTree(SubTree, RootTreeList))
            {
                return RootTreeList;
            }
            else
            {
                Ttree<RBACModule> ptree = new Ttree<RBACModule>();
                ptree.text = pmodule.CName;
                ptree.url = pmodule.Url;
                ptree.icon = pmodule.PicFile;
                ptree.tid = pmodule.Id.ToString();
                ptree.Para = pmodule.Para;
                ptree.ComponentsListJson = pmodule.ComponentsListJson;
                if (pmodule.ParentID.HasValue)
                {
                    ptree.pid = pmodule.ParentID.Value.ToString();
                }
                if (ptree.Tree == null)
                {
                    ptree.Tree = new List<Ttree<RBACModule>>();
                }
                ptree.Tree.Add(SubTree);
                List<tree<RBACModule>> treelist = new List<tree<RBACModule>>();
                return GetRBACModule_IncludePModule<RBACModule>(ptree, RootTreeList, isListTree);
            }           
        }

        private bool ModuleTreeContains<RBACModule>(Ttree<RBACModule> SubTree, List<Ttree<RBACModule>> RootTreeList)
        {
            if (RootTreeList != null)
            {
                for (int i = 0; i < RootTreeList.Count; i++)
                {
                    if (RootTreeList[i].Tree != null && RootTreeList[i].Tree.Count > 0)
                    {
                        if (ModuleTreeContains<RBACModule>(SubTree, RootTreeList[i].Tree.ToList()))
                        {
                            return true;
                        }
                    }
                    if (RootTreeList[i].tid == SubTree.pid)
                    {
                        RootTreeList[i].Tree.Add(SubTree);
                        return true;
                    }
                }
            }
            return false;
        }

        private bool ModuleTreeContainsAndAddSubTree<RBACModule>(Ttree<RBACModule> SubTree, List<Ttree<RBACModule>> RootTreeList)
        {
            if (RootTreeList != null)
            {
                if (string.IsNullOrEmpty(SubTree.pid) || SubTree.pid.Trim() == "0")
                {
                    RootTreeList.Add(SubTree);
                    return true;
                }
                for (int i = 0; i < RootTreeList.Count; i++)
                {
                    if (RootTreeList[i].Tree != null && RootTreeList[i].Tree.Count > 0)
                    {
                        if (ModuleTreeContains<RBACModule>(SubTree, RootTreeList[i].Tree.ToList()))
                        {
                            return true;
                        }
                    }
                    if (RootTreeList[i].tid == SubTree.pid)
                    {
                        RootTreeList[i].Tree.Add(SubTree);
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion

    }
}