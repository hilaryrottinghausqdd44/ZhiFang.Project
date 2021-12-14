using System;
using System.Collections.Generic;
using ZhiFang.IDAO.RBAC;
using ZhiFang.Entity.RBAC;
using ZhiFang.Entity.Base;
using ZhiFang.BLL.Base;
using ZhiFang.IBLL.RBAC;
using System.Linq;

namespace ZhiFang.BLL.RBAC
{
    public class BHRDept : BaseBLL<ZhiFang.Entity.RBAC.HRDept>, IBHRDept
    {
        #region IBHRDept 成员
        const string OrgImg16 = "orgImg16";
        const string OrgsImg16 = "orgsImg16";
        const string UserImg16 = "userImg16";
        const string UsersImg16 = "usersImg16";

        ZhiFang.IBLL.RBAC.IBHREmployee IBHREmployee { get; set; }
        IDHREmployeeDao HREmployeeDBDao { get; set; }
        public IList<HRDept> SearchHRDeptByHREmpID(long longHREmpID)
        {
            return ((IDHRDeptDao)base.DBDao).SearchHRDeptByHREmpID(longHREmpID);
        }

        public IList<HRDept> SearchHRDeptByHRDeptIdentity(long longHRDeptIdentity)
        {
            return ((IDHRDeptDao)base.DBDao).SearchHRDeptByHRDeptIdentity(longHRDeptIdentity);
        }

        #endregion

        public BaseResultTree SearchHRDeptTree(long longHRDeptID)
        {
            //longHRDeptID = 2;
            BaseResultTree tempBaseResultTree = new BaseResultTree(); 
            List<tree> tempListTree = new List<tree>();  
            try
            {
                string tempWhereStr = "";//为空查整个部门表
                if (longHRDeptID > 0)
                    tempWhereStr = " hrdept.Id=" + longHRDeptID.ToString();
                else
                    tempWhereStr = " hrdept.ParentID=" + longHRDeptID.ToString();
                EntityList<HRDept> tempEntityList = this.SearchListByHQL(tempWhereStr, " DispOrder ", - 1, -1);

                if ((tempEntityList != null) && (tempEntityList.list != null) && (tempEntityList.list.Count > 0))
                {
                    foreach (HRDept tempHRDept in tempEntityList.list)
                    {
                        tree tempTree = new tree();
                        tempTree.text = tempHRDept.CName;
                        tempTree.tid = tempHRDept.Id.ToString();
                        tempTree.pid = longHRDeptID.ToString();
                        //tempTree.objectType = tempHRDept.GetType().Name;
                        tempTree.objectType = "HRDept";
                        tempTree.expanded = true;
                        tempTree.Tree = GetChildTree(tempHRDept.Id);
                        tempTree.leaf = (tempTree.Tree.Count <= 0);
                        tempTree.iconCls = tempTree.leaf ? "orgImg16" : "orgsImg16";
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

        public List<tree> GetChildTree(long ParentID)
        {
            List<tree> tempListTree = new List<tree>();
            try
            {
                EntityList<HRDept> tempEntityList = this.SearchListByHQL(" hrdept.ParentID=" + ParentID.ToString(), -1, -1);
                if ((tempEntityList != null) && (tempEntityList.list != null) && (tempEntityList.list.Count > 0))
                {
                    foreach (HRDept tempHRDept in tempEntityList.list)
                    {
                        tree tempTree = new tree();
                        tempTree.text = tempHRDept.CName;
                        tempTree.tid = tempHRDept.Id.ToString();
                        tempTree.pid = ParentID.ToString();
                        tempTree.objectType = "HRDept";
                        tempTree.Tree = GetChildTree(tempHRDept.Id);
                        tempTree.leaf = (tempTree.Tree.Count <= 0);
                        tempTree.iconCls = tempTree.leaf ? "orgImg16" : "orgsImg16";
                        tempListTree.Add(tempTree);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return tempListTree;
        }

        public BaseResultTree<HRDept> SearchHRDeptListTree(long longHRDeptID)
        {
            //longHRDeptID = 2;
            BaseResultTree<HRDept> tempBaseResultTree = new BaseResultTree<HRDept>();
            EntityList<HRDept> tempEntityList = new EntityList<HRDept>();
            try
            {
                string tempWhereStr = "";
                if (longHRDeptID > 0)
                    tempWhereStr = " hrdept.Id=" + longHRDeptID.ToString();
                else
                    tempWhereStr = " hrdept.ParentID=" + longHRDeptID.ToString();
                List<tree<HRDept>> tempListTree = new List<tree<HRDept>>();
                ;
                tempEntityList = this.SearchListByHQL(tempWhereStr, " DispOrder ", -1, -1);
                if ((tempEntityList != null) && (tempEntityList.list != null) && (tempEntityList.list.Count > 0))
                {
                    foreach (HRDept tempHRDept in tempEntityList.list)
                    {
                        tree<HRDept> tempTree = new tree<HRDept>();
                        tempTree.text = tempHRDept.CName;
                        tempTree.tid = tempHRDept.Id.ToString();
                        tempTree.pid = longHRDeptID.ToString();
                        tempTree.objectType = "HRDept";
                        tempTree.value = tempHRDept;
                        tempTree.expanded = true;
                        tempTree.Tree = GetChildTreeList(tempHRDept.Id);
                        tempTree.leaf = (tempTree.Tree.Length <= 0);
                        tempTree.iconCls = tempTree.leaf ? "orgImg16" : "orgsImg16";
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

        public tree<HRDept>[] GetChildTreeList(long ParentID)
        {
            List<tree<HRDept>> tempListTree = new List<tree<HRDept>>();
            try
            {
                EntityList<HRDept> tempEntityList = this.SearchListByHQL(" hrdept.ParentID=" + ParentID.ToString(), " DispOrder ", - 1, -1);
                if ((tempEntityList != null) && (tempEntityList.list != null) && (tempEntityList.list.Count > 0))
                {
                    foreach (HRDept tempHRDept in tempEntityList.list)
                    {
                        tree<HRDept> tempTree = new tree<HRDept>();
                        tempTree.text = tempHRDept.CName;
                        tempTree.tid = tempHRDept.Id.ToString();
                        tempTree.pid = ParentID.ToString();
                        tempTree.objectType = "HRDept";
                        tempTree.value = tempHRDept;
                        tempTree.Tree = GetChildTreeList(tempHRDept.Id);
                        tempTree.leaf = (tempTree.Tree.Length <= 0);
                        tempTree.iconCls = tempTree.leaf ? "orgImg16" : "orgsImg16";
                        tempListTree.Add(tempTree);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return tempListTree.ToArray();
        }

        public BaseResultTree GetHRDeptEmployeeFrameTree(long longHRDeptID)
        {
            //longHRDeptID = 2;
            BaseResultTree tempBaseResultTree = new BaseResultTree();
            IList<HREmployee> tempHREmployeeList = new List<HREmployee>();
            tempBaseResultTree = SearchHRDeptTree(longHRDeptID);
            GetHRDeptEmployeeFrameChildTree(tempBaseResultTree.Tree);
            return tempBaseResultTree;
        }

        public void GetHRDeptEmployeeFrameChildTree(IList<tree> treeList)
        {
            HRDept tempHRDept = new HRDept();  
            for (int i = 0; i < treeList.Count;i++ )
            {
                GetHRDeptEmployeeFrameChildTree(treeList[i].Tree);
                tempHRDept = this.Get(Int64.Parse(treeList[i].tid));
                if (tempHRDept != null)
                {
                    foreach (HREmployee tempHREmployee in tempHRDept.HREmployeeList)
                    {
                        tree tempHREmployeeTree = new tree();
                        tempHREmployeeTree.text = tempHREmployee.CName;
                        tempHREmployeeTree.tid = tempHREmployee.Id.ToString();
                        tempHREmployeeTree.pid = treeList[i].tid;
                        tempHREmployeeTree.objectType = "HREmployee";
                        tempHREmployeeTree.leaf = true;
                        tempHREmployeeTree.iconCls = tempHREmployeeTree.leaf ? "userImg16" : "usersImg16";
                        if (treeList[i].Tree == null)
                            treeList[i].Tree = new List<tree>();
                        treeList[i].Tree.Add(tempHREmployeeTree);
                        treeList[i].leaf = false;
                        treeList[i].iconCls = treeList[i].leaf ? "orgImg16" : "orgsImg16"; 
                    }
                }
            }
        }

        public IList<HRDept> SearchHRDeptListByHRDeptId(long deptId)
        {
            IList<HRDept> deptList = new List<HRDept>();
            try
            {
                string tempWhereStr = "";//为空查整个部门表
                if (deptId > 0)
                    tempWhereStr = " hrdept.Id=" + deptId.ToString();
                else
                    tempWhereStr = " hrdept.ParentID=" + deptId.ToString();
                EntityList<HRDept> tempEntityList = this.SearchListByHQL(tempWhereStr, -1, -1);

                if ((tempEntityList != null) && (tempEntityList.list != null) && (tempEntityList.list.Count > 0))
                {
                    foreach (HRDept tempHRDept in tempEntityList.list)
                    {
                        IList<HRDept> tempList = GetChildList(tempHRDept.Id);
                        deptList = deptList.Union(tempList).ToList();
                        if (!deptList.Contains(tempHRDept))
                            deptList.Add(tempHRDept);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            if (deptList.Count > 0)
                deptList = deptList.OrderBy(p => p.ParentID).OrderBy(p => p.DispOrder).ToList();
            return deptList;
        }
        public IList<HRDept> GetChildList(long ParentID)
        {
            List<HRDept> deptList = new List<HRDept>();
            try
            {
                EntityList<HRDept> tempEntityList = this.SearchListByHQL(" hrdept.ParentID=" + ParentID.ToString(), -1, -1);
                if ((tempEntityList != null) && (tempEntityList.list != null) && (tempEntityList.list.Count > 0))
                {
                    foreach (HRDept tempHRDept in tempEntityList.list)
                    {
                        IList<HRDept> tempList = GetChildList(tempHRDept.Id);
                        deptList = deptList.Union(tempList).ToList();
                        if (!deptList.Contains(tempHRDept))
                            deptList.Add(tempHRDept);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return deptList;
        }
        public string SearchHRDeptIdListByHRDeptId(long deptId)
        {
            string listID = "";
            IList<HRDept> deptList = new List<HRDept>();
            try
            {
                string tempWhereStr = "";//为空查整个部门表
                if (deptId > 0)
                    tempWhereStr = " hrdept.Id=" + deptId.ToString();
                else
                    tempWhereStr = " hrdept.ParentID=" + deptId.ToString();
                EntityList<HRDept> tempEntityList = this.SearchListByHQL(tempWhereStr, -1, -1);

                if ((tempEntityList != null) && (tempEntityList.list != null) && (tempEntityList.list.Count > 0))
                {
                    foreach (HRDept tempHRDept in tempEntityList.list)
                    {
                        IList<HRDept> tempList = GetChildList(tempHRDept.Id);
                        deptList = deptList.Union(tempList).ToList();
                        if (!deptList.Contains(tempHRDept))
                            deptList.Add(tempHRDept);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            if (deptList.Count > 0)
            {
                foreach (HRDept dept in deptList)
                {
                    if (string.IsNullOrEmpty(listID))
                        listID = dept.Id.ToString();
                    else
                        listID += "," + dept.Id.ToString();
                }
            }
            return listID;
        }
    }
}
