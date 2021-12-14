using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.IDAO;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IBLL.RBAC;
using ZhiFang.Digitlab.IBLL.Business;

namespace ZhiFang.Digitlab.BLL.RBAC
{	
	public class BRBACModule : ZhiFang.Digitlab.BLL.BaseBLL<ZhiFang.Digitlab.Entity.RBACModule>, ZhiFang.Digitlab.IBLL.RBAC.IBRBACModule
    {
        public IDRBACRoleModuleDao IDRBACRoleModuleDao { get; set; }
        public IBRBACModuleOper IBRBACModuleOper { get; set; }
        public IBGMGroupEquip IBGMGroupEquip { get; set; }
        //public IBRBACModuleOper IBRBACModuleOper { get; set; }
        #region IBRBACModule 成员

        public IList<RBACModule> SearchModuleByModuleOperID(long longModuleOperID)
        {
            return ((IDAO.IDRBACModuleDao)base.DBDao).SearchModuleByModuleOperID(longModuleOperID);
        }

        public IList<RBACModule> SearchModuleByRoleID(long longRoleID)
        {
            return ((IDAO.IDRBACModuleDao)base.DBDao).SearchModuleByRoleID(longRoleID);
        }

        public IList<RBACModule> SearchModuleByHREmpID(long longHREmpID)
        {
            return ((IDAO.IDRBACModuleDao)base.DBDao).SearchModuleByHREmpID(longHREmpID);
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
            UpdateOldRole(rbacmodule);
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
                RBACModule oprbacmodule = DBDao.Get(pid.Value);
                IList<RBACModule> subrbacmodulelist = DBDao.GetListByHQL(" rbacmodule.ParentID=" + pid.ToString() + " and rbacmodule.Id <>" + rbacmodule.Id.ToString(), 0, 0).list;
                //IList<RBACRoleModule> rbacrolemodulelist = rbacmodule.RBACRoleModuleList;
                IList<RBACRoleModule> rbacrolemodulelist = IDRBACRoleModuleDao.GetListByHQL(" rbacrolemodule.RBACModule.Id=" + rbacmodule.Id, 0, 0).list;
                foreach (var r in rbacrolemodulelist)
                {
                    bool flag = false;
                    foreach (var s in subrbacmodulelist)
                    {
                        int count = IDRBACRoleModuleDao.GetListCountByHQL(" rbacrolemodule.RBACRole.Id =" + r.RBACRole.Id + " and rbacrolemodule.RBACModule.Id =" + s.Id);
                        flag = count > 0;
                        break;
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
                            break;
                        }
                        UpdateNewRole(rbacrolemodulelist, newprbacmodule.ParentID.Value);
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
                IList<RBACModule> allList = this.SearchListByHQL(" 1=1", " DispOrder,DataAddTime ", -1, -1).list;
                IList<RBACModule> tmplist = this.SearchListByHQL(" rbacmodule.ParentID=0 ", " DispOrder,DataAddTime ", -1, -1).list;
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
                IList<RBACModule> empList = ((IDAO.IDRBACModuleDao)base.DBDao).SearchModuleByRoleID(longRBACRoleID);
                IList<RBACModule> allList = this.SearchListByHQL(" 1=1", " DispOrder,DataAddTime ", -1, -1).list;
                IList<RBACModule> tmplist = this.SearchListByHQL(" rbacmodule.ParentID=0 ", " DispOrder,DataAddTime ", -1, -1).list;
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

                    //IList<RBACModule> tmplist = allList.Where(p => p.ParentID == m.Id).OrderBy(p => p.DispOrder).OrderBy(p => p.DataAddTime).ToList();
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
            return ((IDAO.IDRBACModuleDao)base.DBDao).SearchModuleByUserCode(strUserCode);
        }

        public IList<RBACModule> SearchModuleByHREmpIDAndModuleID(long longHREmpID, long longModuleID)
        {
            return ((IDAO.IDRBACModuleDao)base.DBDao).SearchModuleByHREmpIDAndModuleID(longHREmpID, longModuleID);
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
                flag=((IDAO.IDRBACModuleDao)base.DBDao).Delete(longModuleID);
            }
            return flag;
        }

        public BaseResultTree<Entity.RBACModule> SearchRBACModuleToTree(long RBACModuleID,bool IsListTree)
        {
            if (RBACModuleID < 0)
            {
                RBACModuleID=0;
            }
            EntityList<RBACModule> tmplist = this.SearchListByHQL(" rbacmodule.ParentID= " + RBACModuleID, " DispOrder,DataAddTime ", -1, -1);
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



            BaseResultTree<Entity.RBACModule> ListTreeRoot = new BaseResultTree<RBACModule>();
            ListTreeRoot.Tree = new List<tree<RBACModule>>();

            if ((tmplist != null) && (tmplist.list != null) && (tmplist.list.Count > 0))
            {
                for (int i = 0; i < tmplist.count; i++)
                {
                    tree<Entity.RBACModule> tree = new Entity.tree<Entity.RBACModule>();
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
                    tree.Tree = GetRBACModuleLeafTrees<Entity.RBACModule>(tmplist.list[i].Id, IsListTree);
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
                EntityList<RBACModule> tmplist = this.SearchListByHQL(" rbacmodule.ParentID= " + ParentID, " DispOrder,DataAddTime ", -1, -1);
                if ((tmplist != null) && (tmplist.list != null) && (tmplist.list.Count > 0))
                {
                    for (int i = 0; i < tmplist.count; i++)
                    {
                        tree<RBACModule> tree = new Entity.tree<RBACModule>();
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
            return ((IDAO.IDRBACModuleDao)base.DBDao).SearchModuleByUseCode(UseCode);
        }

        public bool DeleteModuleByUseCode(string UseCode)
        {
            RBACModule rbacm=((IDAO.IDRBACModuleDao)base.DBDao).SearchModuleByUseCode(UseCode);
            if (rbacm != null)
            {
                if (!IBRBACModuleOper.DeleteByRBACModuleId(rbacm.Id))
                {
                    return false;
                    throw new Exception("删除检验小组模块操作失败！"); 
                }
                if (!IDRBACRoleModuleDao.DeleteByRBACModuleId(rbacm.Id))
                {
                    return false;
                    throw new Exception("删除检验小组模块权限失败！");
                }
                if (!DBDao.Delete(rbacm.Id))
                {
                    return false;
                    throw new Exception("删除检验小组模块失败！");
                }
            }
            return true;
        }

        public object SearchTestGroupEquipRBACModuleToTree(string RBACModuleUseCode, bool IsListTree)
        {
            string hql = "( rbacmodule.ModuleType=2 or rbacmodule.ModuleType=3 )";
            EntityList<RBACModule> tmplist = this.SearchListByHQL(hql, " DispOrder,DataAddTime ", -1, -1);
            BaseResultTree ListTreeRoot = new BaseResultTree();
            ListTreeRoot.Tree = new List<tree>();            

            if ((tmplist != null) && (tmplist.list != null) && (tmplist.list.Count > 0))
            {
                var TestGrouplist = tmplist.list.Where(rbacm => (rbacm.ModuleType == 2)).AsEnumerable();
                var Equiplist = tmplist.list.Where(rbacm => (rbacm.ModuleType == 3));

                for (int i = 0; i < TestGrouplist.Count(); i++)
                {
                    tree tree = new Entity.tree();
                    tree.text = TestGrouplist.ElementAt(i).CName;
                    tree.url = TestGrouplist.ElementAt(i).Url;
                    tree.icon = TestGrouplist.ElementAt(i).PicFile;
                    tree.tid = TestGrouplist.ElementAt(i).Id.ToString();
                    tree.pid = TestGrouplist.ElementAt(i).ParentID.ToString();
                    tree.objectType = "string";
                    tree.value = TestGrouplist.ElementAt(i).Para;
                    tree.Para = TestGrouplist.ElementAt(i).Para;
                    if (IsListTree)
                    {
                        TestGrouplist.ElementAt(i).RBACModuleOperList = null;
                        TestGrouplist.ElementAt(i).RBACRoleModuleList = null;
                        TestGrouplist.ElementAt(i).RBACEmpOptionsList = null;
                        //tmplist.list[i].BTDAppComponents = null;
                        tree.value = TestGrouplist.ElementAt(i);
                        tree.Para = TestGrouplist.ElementAt(i);
                    }

                    tree.Tree = GetTestGroupEquipRBACModuleLeafTrees<Entity.RBACModule>(TestGrouplist.ElementAt(i).Id, TestGrouplist.ElementAt(i).UseCode, Equiplist, IsListTree);
                    //if (tree.Tree.Length > 0)
                    //{
                    //    tree.leaf = false;
                    //}
                    //else
                    //{
                    //    tree.leaf = true;
                    //}
                    tree.expanded = true;
                    ListTreeRoot.Tree.Add(tree);
                }
            }
            return ListTreeRoot;
        }

        public List<tree> GetTestGroupEquipRBACModuleLeafTrees<T>(long ParentID, string TestGroupEquipUseCode, IEnumerable<RBACModule> EquipModulelist, bool IsListTree)
        {           
            long GroupID = 0;
            try
            {
                GroupID = Convert.ToInt64(TestGroupEquipUseCode.Split('_')[1].Trim());
            }
            catch (Exception e)
            {
                throw new Exception("初始化小组ID时发生错误！"+e.ToString());
            }
            List<tree> listTree = new List<tree>();
            try
            {
                //查询小组仪器
                EntityList<GMGroupEquip> groupequiplist = IBGMGroupEquip.SearchListByHQL(" gmgroupequip.GMGroup.Id= " + GroupID, " DispOrder,DataAddTime ", -1, -1);
                if ((groupequiplist != null) && (groupequiplist.list != null) && (groupequiplist.list.Count > 0))
                {
                    for (int i = 0; i < groupequiplist.count; i++)
                    {
                        var BRBACModule = EquipModulelist.Where(equip => (equip.UseCode.Split('_')[1] == groupequiplist.list[i].EPBEquip.Id.ToString()));

                        if (BRBACModule.Count() > 0)
                        {
                            tree tree = new Entity.tree();
                            tree.text = BRBACModule.ElementAt(0).CName;
                            tree.url = BRBACModule.ElementAt(0).Url;
                            tree.icon = BRBACModule.ElementAt(0).PicFile;
                            tree.tid = BRBACModule.ElementAt(0).Id.ToString();
                            tree.pid = ParentID.ToString();
                            tree.value = BRBACModule.ElementAt(0).Para;
                            tree.Para = BRBACModule.ElementAt(0).Para;
                            if (IsListTree)
                            {
                                BRBACModule.ElementAt(0).RBACModuleOperList = null;
                                BRBACModule.ElementAt(0).RBACRoleModuleList = null;
                                BRBACModule.ElementAt(0).RBACEmpOptionsList = null;
                                //tmplist.list[i].BTDAppComponents = null;
                                tree.value = BRBACModule.ElementAt(0);
                                tree.Para = BRBACModule.ElementAt(0);
                            }                           
                            listTree.Add(tree);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("查询小组仪器时发生错误！" + ex.ToString());
            }
            return listTree;
        }

        public object SearchTestGroupOrEquipRBACModuleToTree(string RBACModuleUseCode, int ModuleType, bool IsListTree)
        {
            string hql = "( rbacmodule.ModuleType=2 )";
            if (ModuleType == 2 || ModuleType == 3)
            {
                if (ModuleType == 2)
                {
                    hql = "( rbacmodule.ModuleType=2 )";
                }

                if (ModuleType == 3)
                {
                    hql = "( rbacmodule.ModuleType=3 )";
                }
            }
            else
            {
                ZhiFang.Common.Log.Log.Error("SearchTestGroupOrEquipRBACModuleToTree:ModuleType不符合规范！ModuleType:" + ModuleType);
                return null;
            }
            EntityList<RBACModule> tmplist = this.SearchListByHQL(hql, " DispOrder,DataAddTime ", -1, -1);
            BaseResultTree ListTreeRoot = new BaseResultTree();
            ListTreeRoot.Tree = new List<tree>();

            if ((tmplist != null) && (tmplist.list != null) && (tmplist.list.Count > 0))
            {
                var TestGrouplist = tmplist.list.Where(rbacm => (rbacm.ModuleType == ModuleType)).AsEnumerable();

                for (int i = 0; i < TestGrouplist.Count(); i++)
                {
                    tree tree = new Entity.tree();
                    tree.text = TestGrouplist.ElementAt(i).CName;
                    tree.url = TestGrouplist.ElementAt(i).Url;
                    tree.icon = TestGrouplist.ElementAt(i).PicFile;
                    tree.tid = TestGrouplist.ElementAt(i).Id.ToString();
                    tree.pid = TestGrouplist.ElementAt(i).ParentID.ToString();
                    tree.objectType = "string";
                    tree.value = TestGrouplist.ElementAt(i).Para;
                    tree.Para = TestGrouplist.ElementAt(i).Para;
                    tree.expanded = true;
                    ListTreeRoot.Tree.Add(tree);
                }
            }
            return ListTreeRoot;
        }

        public object SearchTestGroupEquipModuleTreeByHREmpID(long longHREmpID)
        {
            BaseResultTree result = new BaseResultTree();
            result.success = true;
            List<tree> listTree = new List<tree>();
            try
            {
                IList<RBACModule> empList = SearchModuleByHREmpID(longHREmpID);
                BaseResultTree ListTreeRoot = new BaseResultTree();
                ListTreeRoot.Tree = new List<tree>();

                if ((empList != null) && (empList.Count() > 0))
                {
                    var grouplist = empList.Where(module => (module.ModuleType == 2));
                    var equiplist = empList.Where(module => (module.ModuleType == 3));

                    // var TestGrouplist = tmplist.list.Where(rbacm => (rbacm.ModuleType == 2)).AsEnumerable();
                    //var Equiplist = tmplist.list.Where(rbacm => (rbacm.ModuleType == 3));

                    for (int i = 0; i < grouplist.Count(); i++)
                    {
                        tree tree = new Entity.tree();
                        tree.text = grouplist.ElementAt(i).CName;
                        tree.url = grouplist.ElementAt(i).Url;
                        tree.icon = grouplist.ElementAt(i).PicFile;
                        tree.tid = grouplist.ElementAt(i).Id.ToString();
                        tree.pid = grouplist.ElementAt(i).ParentID.ToString();
                        tree.objectType = "string";
                        tree.value = grouplist.ElementAt(i).Para;
                        tree.Para = grouplist.ElementAt(i).Para;
                        tree.Tree = GetTestGroupEquipRBACModuleLeafTrees<Entity.RBACModule>(grouplist.ElementAt(i).Id, grouplist.ElementAt(i).UseCode, equiplist, false);
                        tree.expanded = true;
                        ListTreeRoot.Tree.Add(tree);
                    }
                }
                return ListTreeRoot;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.ErrorInfo = ex.Message;
            }
            result.Tree = listTree;
            return result;
        }

        public object SearchTestGroupOrEquipModuleTreeByHREmpID(long longHREmpID, int ModuleType)
        {
            BaseResultTree result = new BaseResultTree();
            result.success = true;
            List<tree> listTree = new List<tree>();
            try
            {
                IList<RBACModule> empList = SearchModuleByHREmpID(longHREmpID);
                BaseResultTree ListTreeRoot = new BaseResultTree();
                ListTreeRoot.Tree = new List<tree>();

                if ((empList != null) && (empList.Count() > 0))
                {
                    var grouplist = empList.Where(module => (module.ModuleType == ModuleType));

                    // var TestGrouplist = tmplist.list.Where(rbacm => (rbacm.ModuleType == 2)).AsEnumerable();
                    //var Equiplist = tmplist.list.Where(rbacm => (rbacm.ModuleType == 3));

                    for (int i = 0; i < grouplist.Count(); i++)
                    {
                        tree tree = new Entity.tree();
                        tree.text = grouplist.ElementAt(i).CName;
                        tree.url = grouplist.ElementAt(i).Url;
                        tree.icon = grouplist.ElementAt(i).PicFile;
                        tree.tid = grouplist.ElementAt(i).Id.ToString();
                        tree.pid = grouplist.ElementAt(i).ParentID.ToString();
                        tree.objectType = "string";
                        tree.value = grouplist.ElementAt(i).Para;
                        tree.Para = grouplist.ElementAt(i).Para;
                        tree.expanded = true;
                        ListTreeRoot.Tree.Add(tree);
                    }
                }
                return ListTreeRoot;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.ErrorInfo = ex.Message;
            }
            result.Tree = listTree;
            return result;
        }

        public object SearchTestGroupModuleByHREmpID(long longHREmpID)
        {
            BaseResultTree result = new BaseResultTree();
            result.success = true;
            List<tree> listTree = new List<tree>();
            try
            {
                IList<RBACModule> empList = SearchModuleByHREmpID(longHREmpID);
                BaseResultTree ListTreeRoot = new BaseResultTree();
                ListTreeRoot.Tree = new List<tree>();

                if ((empList != null) && (empList.Count() > 0))
                {
                    var grouplist = empList.Where(module => (module.ModuleType == 2));

                    // var TestGrouplist = tmplist.list.Where(rbacm => (rbacm.ModuleType == 2)).AsEnumerable();
                    //var Equiplist = tmplist.list.Where(rbacm => (rbacm.ModuleType == 3));

                    for (int i = 0; i < grouplist.Count(); i++)
                    {
                        tree tree = new Entity.tree();
                        tree.text = grouplist.ElementAt(i).CName;
                        tree.url = grouplist.ElementAt(i).Url;
                        tree.icon = grouplist.ElementAt(i).PicFile;
                        tree.tid = grouplist.ElementAt(i).Id.ToString();
                        tree.pid = grouplist.ElementAt(i).ParentID.ToString();
                        tree.objectType = "string";
                        tree.value = grouplist.ElementAt(i).Para;
                        tree.Para = grouplist.ElementAt(i).Para;
                        tree.expanded = true;
                        ListTreeRoot.Tree.Add(tree);
                    }
                }
                return ListTreeRoot;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.ErrorInfo = ex.Message;
            }
            result.Tree = listTree;
            return result;
        }

        #endregion


        
    } 
}