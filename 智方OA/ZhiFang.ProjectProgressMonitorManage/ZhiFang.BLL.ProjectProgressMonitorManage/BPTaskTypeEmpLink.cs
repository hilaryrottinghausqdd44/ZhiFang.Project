using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.ProjectProgressMonitorManage;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;

namespace ZhiFang.BLL.ProjectProgressMonitorManage
{
    /// <summary>
    ///
    /// </summary>
    public class BPTaskTypeEmpLink : BaseBLL<PTaskTypeEmpLink>, ZhiFang.IBLL.ProjectProgressMonitorManage.IBPTaskTypeEmpLink
    {
        IBLL.ProjectProgressMonitorManage.IBBDictTree IBBDictTree { get; set; }
        /// <summary>
        /// 根据员工ID返回树列表
        /// </summary>
        /// <param name="longHREmpID"></param>
        /// <param name="pidStr">字典树的一级父节点(根节点)</param>
        /// <param name="where"></param>
        /// <returns></returns>
        public BaseResultTree SearchPTaskTypeEmpLinkToTree(long longHREmpID, string pidStr, string where)
        {
            BaseResultTree result = new BaseResultTree();
            result.success = true;
            if (String.IsNullOrEmpty(pidStr)& String.IsNullOrEmpty(where))
            {
                return result;
            }
            List<tree> listTree = new List<tree>();
            if (string.IsNullOrEmpty(where))
            {
                where = " EmpID=" + longHREmpID;
            }
            else
            {
                where += " and EmpID=" + longHREmpID;
            }
            try
            {
                IList<PTaskTypeEmpLink> empList = this.SearchListByHQL(where);
                //ZhiFang.Common.Log.Log.Debug("EmpID为:" + longHREmpID + "的权限字典树节点数据为:" + empList.Count);
                if (empList == null || empList.Count == 0) return result;

                StringBuilder strb = new StringBuilder();
                foreach (var item in empList)
                {
                    strb.Append(item.TaskTypeID);
                    strb.Append(",");
                }
                string idStr = strb.ToString().TrimEnd(',');
                IList<BDictTree> allTreeList = IBBDictTree.SearchBDictTreeAndChildTreeByHQL(-1, -1, " IsUse=true and Id in (" + idStr + ")", "", true).list;
                //ZhiFang.Common.Log.Log.Debug("ParentID为:" + idStr + "的所有子孙节点数据为:" + allTreeList.Count);
                IList<BDictTree> pidList = IBBDictTree.SearchListByHQL(" IsUse=true and ParentID in (" + pidStr + ")");
                string[] pidArr = pidStr.Split(',');
                listTree = GetLeafTrees(pidList, allTreeList, pidArr);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.ErrorInfo = ex.Message;
            }
            result.Tree = listTree;
            return result;
        }
        public List<tree> GetLeafTrees(IList<BDictTree> pidList, IList<BDictTree> allTreeList, string[] pidArr)
        {
            if (pidList.Count == 0)
            {
                return null;
            }
            List<tree> listTree = new List<tree>();
            foreach (BDictTree curTree in pidList)
            {
                tree tree = new tree();
                tree.text = curTree.CName;
                tree.tid = curTree.Id.ToString();
                tree.pid = curTree.ParentID.ToString();
                tree.expanded = false;
                IList<BDictTree> tmplist = allTreeList.Where(p => p.ParentID == curTree.Id).OrderBy(p => p.DispOrder).ToList();
                if (tmplist.Count > 0)
                {
                    tree.Tree = GetLeafTrees(tmplist, allTreeList, pidArr);
                    if (tree.Tree != null && tree.Tree.Count > 0)
                    {
                        tree.leaf = false;
                    }
                    else
                    {
                        tree.leaf = true;
                    }
                    tree.iconCls = tree.leaf ? "tree-child-16" : "tree-parent-16";
                    listTree.Add(tree);
                }
                else
                {
                    //只有tree的父节点不为根节点时才添加该子节点
                    if (!pidArr.Contains(tree.pid))
                    {
                        tree.leaf = true;
                        tree.iconCls = "tree-child-16";
                        listTree.Add(tree);
                    }
                }
            }
            return listTree;
        }
        public List<tree> GetLeafTrees2(IList<BDictTree> pidList, long longHREmpID, IList<PTaskTypeEmpLink> empList, IList<BDictTree> allTreeList)
        {
            if (pidList.Count == 0)
            {
                return null;
            }
            List<tree> listTree = new List<tree>();
            foreach (BDictTree curTree in pidList)
            {
                var tempCurList = empList.Where(p => p.TaskTypeID == curTree.Id).ToList();
                if (tempCurList.Count > 0)
                {
                    tree tree = new tree();
                    tree.text = curTree.CName;
                    tree.tid = curTree.Id.ToString();
                    tree.pid = curTree.ParentID.ToString();
                    tree.expanded = false;
                    IList<BDictTree> tmplist = allTreeList.Where(p => p.ParentID == curTree.Id).OrderBy(p => p.DispOrder).ToList();
                    if (tmplist.Count > 0)
                    {
                        tree.Tree = GetLeafTrees2(tmplist, longHREmpID, empList, allTreeList);
                        if (tree.Tree != null && tree.Tree.Count > 0)
                        {
                            tree.leaf = false;
                        }
                        else
                        {
                            tree.leaf = true;
                        }
                        tree.iconCls = tree.leaf ? "tree-child-16" : "tree-parent-16";
                        listTree.Add(tree);
                    }
                    else
                    {
                        tree.leaf = true;
                        tree.iconCls = "tree-child-16";
                        listTree.Add(tree);
                    }
                }
            }
            return listTree;
        }
    }
}