using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.IDAO;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.BLL.RBAC
{	
	public class BRBACRole : ZhiFang.Digitlab.BLL.BaseBLL<ZhiFang.Digitlab.Entity.RBACRole>, ZhiFang.Digitlab.IBLL.RBAC.IBRBACRole
    {

        #region IBRBACRole 成员

        public IList<RBACRole> SearchRoleByHREmpID(long longHREmpID)
        {
            return ((IDAO.IDRBACRoleDao)base.DBDao).SearchRoleByHREmpID(longHREmpID);
        }

        public IList<RBACRole> SearchRoleByUserCode(string strUserCode)
        {
            return ((IDAO.IDRBACRoleDao)base.DBDao).SearchRoleByUserCode(strUserCode);
        }

        public IList<RBACRole> SearchRoleByModuleID(long longModuleID)
        {
            return ((IDAO.IDRBACRoleDao)base.DBDao).SearchRoleByModuleID(longModuleID);
        }

        public string SearchRoleModuleOperByModuleID(long longModuleID)
        {
            string tempRoleList = "";
            IList<RBACRole> tempRBACRoleList = ((IDAO.IDRBACRoleDao)base.DBDao).SearchRoleByModuleID(longModuleID);
            if (tempRBACRoleList != null && tempRBACRoleList.Count > 0)
            {
                
                foreach(RBACRole tempRBACRole in tempRBACRoleList)
                {
                    string strRoleOperate = "{roleId:\"" + tempRBACRole.Id.ToString() + "\"";  
                    string strOperate = ""; 
                    if (tempRBACRole.RBACRoleRightList != null && tempRBACRole.RBACRoleRightList.Count > 0)
                    { 
                        foreach(RBACRoleRight tempRBACRoleRight in tempRBACRole.RBACRoleRightList)
                        {
                            if (tempRBACRoleRight.RBACModuleOper != null)
                                strOperate += "," + "{rightID:\"" + tempRBACRoleRight.Id.ToString() + "\", moduleOperID:\"" + tempRBACRoleRight.RBACModuleOper.Id.ToString() + "\"}";
                        }
                        if (strOperate.Length > 0)
                            strOperate = "operateList:[" + strOperate.Remove(0, 1)+"]";
                        else
                            strOperate = "operateList:[]";
                    }
                    tempRoleList += ","+strRoleOperate + "," + strOperate + "}";
                }
                tempRoleList = "[" + tempRoleList.Remove(0, 1) + "]";
            }
            return tempRoleList;
        }

        public IList<RBACRole> SearchRoleByModuleOperID(long longModuleOperID)
        {
            return ((IDAO.IDRBACRoleDao)base.DBDao).SearchRoleByModuleOperID(longModuleOperID);
        }
        public BaseResultTree SearchRBACRoleTree(long longRBACRoleID)
        {
            //longRBACRoleID = 2;
            BaseResultTree tempBaseResultTree = new BaseResultTree();
            List<tree> tempListTree = new List<tree>();
            try
            {
                string tempWhereStr = "";//为空查整个部门表
                if (longRBACRoleID > 0)
                    tempWhereStr = " rbacrole.Id=" + longRBACRoleID.ToString();
                else
                    tempWhereStr = " (rbacrole.ParentID=" + longRBACRoleID.ToString() + " or rbacrole.ParentID is Null)";
                EntityList<RBACRole> tempEntityList = this.SearchListByHQL(tempWhereStr, -1, -1);

                if ((tempEntityList != null) && (tempEntityList.list.Count > 0))
                {
                    foreach (RBACRole tempRBACRole in tempEntityList.list)
                    {
                        tree tempTree = new tree();
                        tempTree.text = tempRBACRole.CName;
                        tempTree.tid = tempRBACRole.Id.ToString();
                        tempTree.pid = longRBACRoleID.ToString();
                        tempTree.expanded = true;
                        tempTree.Tree = GetChildTree(tempRBACRole.Id);
                        tempTree.leaf = (tempTree.Tree.Count <= 0);
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
            EntityList<RBACRole> tempEntityList = new EntityList<RBACRole>();
            List<tree> tempListTree = new List<tree>();
            try
            {
                tempEntityList = this.SearchListByHQL(" rbacrole.ParentID=" + ParentID.ToString(), -1, -1);
                if (tempEntityList.list.Count > 0)
                {
                    foreach (RBACRole tempRBACRole in tempEntityList.list)
                    {
                        tree tempTree = new tree();
                        tempTree.text = tempRBACRole.CName;
                        tempTree.tid = tempRBACRole.Id.ToString();
                        tempTree.pid = ParentID.ToString();
                        tempTree.Tree = GetChildTree(tempRBACRole.Id);
                        tempTree.leaf = (tempTree.Tree.Count <= 0);
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
        
        public BaseResultTree<RBACRole> SearchRBACRoleListTree(long longRBACRoleID)
        {
            //longRBACRoleID = 2;
            BaseResultTree<RBACRole> tempBaseResultTree = new BaseResultTree<RBACRole>();
            EntityList<RBACRole> tempEntityList = new EntityList<RBACRole>();
            try
            {
                string tempWhereStr = "";
                if (longRBACRoleID > 0)
                    tempWhereStr = " rbacrole.Id=" + longRBACRoleID.ToString();
                else
                    tempWhereStr = " rbacrole.ParentID=" + longRBACRoleID.ToString();
                List<tree<RBACRole>> tempListTree = new List<tree<RBACRole>>();
                ;
                tempEntityList = this.SearchListByHQL(tempWhereStr, -1, -1);
                if ((tempEntityList != null) && (tempEntityList.list != null) && (tempEntityList.list.Count > 0))
                {
                    foreach (RBACRole tempRBACRole in tempEntityList.list)
                    {
                        tree<RBACRole> tempTree = new tree<RBACRole>();
                        tempTree.text = tempRBACRole.CName;
                        tempTree.tid = tempRBACRole.Id.ToString();
                        tempTree.pid = longRBACRoleID.ToString();
                        tempTree.value = tempRBACRole;
                        tempTree.expanded = true;
                        tempTree.Tree = GetChildTreeList(tempRBACRole.Id);
                        tempTree.leaf = (tempTree.Tree.Length <= 0);
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

        public tree<RBACRole>[] GetChildTreeList(long ParentID)
        {
            List<tree<RBACRole>> tempListTree = new List<tree<RBACRole>>();
            try
            {
                EntityList<RBACRole> tempEntityList = this.SearchListByHQL(" rbacrole.ParentID=" + ParentID.ToString(), -1, -1);
                if ((tempEntityList != null) && (tempEntityList.list != null) && (tempEntityList.list.Count > 0))
                {
                    foreach (RBACRole tempRBACRole in tempEntityList.list)
                    {
                        tree<RBACRole> tempTree = new tree<RBACRole>();
                        tempTree.text = tempRBACRole.CName;
                        tempTree.tid = tempRBACRole.Id.ToString();
                        tempTree.pid = ParentID.ToString();
                        tempTree.value = tempRBACRole;
                        tempTree.Tree = GetChildTreeList(tempRBACRole.Id);
                        tempTree.leaf = (tempTree.Tree.Length <= 0);
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
        //public BaseResultTree<RBACRole> SearchRBACRoleListTree(long longRBACRoleID)
        //{
        //    longRBACRoleID = 2;
        //    BaseResultTree<RBACRole> tempBaseResultTree = new BaseResultTree<RBACRole>();
        //    try
        //    {
        //        string tempWhereStr = "";
        //        List<tree<RBACRole>> tempListTree = new List<tree<RBACRole>>();
        //        EntityList<RBACRole> tempEntityList = new EntityList<RBACRole>();
        //        tempEntityList = this.SearchListByHQL(tempWhereStr, 1, 1000);
        //        IList<RBACRole> IList = tempEntityList.list;
        //        List<RBACRole> tempList = null;
        //        if (longRBACRoleID > 0)
        //            tempList = IList.Where(a => a.Id == longRBACRoleID).ToList<RBACRole>();
        //        else
        //            tempList = IList.Where(a => a.ParentID == longRBACRoleID).ToList<RBACRole>();
        //        foreach (RBACRole tempRBACRole in tempList)
        //        {
        //            tree<RBACRole> tempTree = new tree<RBACRole>();
        //            tempTree.text = tempRBACRole.CName;
        //            tempTree.tid = tempRBACRole.Id.ToString();
        //            tempTree.value = tempRBACRole;
        //            tempTree.expanded = true;
        //            tempTree.Tree = GetLeafTreeList(tempRBACRole.Id, IList);
        //            tempTree.leaf = (tempTree.Tree.Length <= 0);
        //            tempListTree.Add(tempTree);
        //        }
        //        tempBaseResultTree.Tree = tempListTree;
        //        tempBaseResultTree.success = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        tempBaseResultTree.success = false;
        //        tempBaseResultTree.ErrorInfo = ex.Message;
        //    }
        //    return tempBaseResultTree;
        //}

        //public tree<RBACRole>[] GetLeafTreeList(long ParentID, IList<RBACRole> allList)
        //{
        //    List<tree<RBACRole>> tempListTree = new List<tree<RBACRole>>();
        //    try
        //    {
        //        IList<RBACRole> list = allList.Where(a => a.ParentID == ParentID).ToList<RBACRole>();
        //        if (list.Count > 0)
        //        {
        //            foreach (RBACRole tempRBACRole in list)
        //            {
        //                tree<RBACRole> tempTree = new tree<RBACRole>();
        //                tempTree.text = tempRBACRole.CName;
        //                tempTree.tid = tempRBACRole.Id.ToString();
        //                tempTree.value = tempRBACRole;
        //                tempTree.Tree = GetLeafTreeList(tempRBACRole.Id, allList);
        //                tempTree.leaf = (tempTree.Tree.Length <= 0);
        //                tempListTree.Add(tempTree);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    return tempListTree.ToArray();
        //}

        #endregion
    } 
}