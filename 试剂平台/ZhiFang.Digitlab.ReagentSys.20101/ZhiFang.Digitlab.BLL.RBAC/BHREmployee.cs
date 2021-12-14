using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using ZhiFang.Digitlab.IDAO;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.BLL.RBAC
{	
	public class BHREmployee : ZhiFang.Digitlab.BLL.BaseBLL<HREmployee>, ZhiFang.Digitlab.IBLL.RBAC.IBHREmployee
    {
        #region IBHREmployee 成员
        ZhiFang.Digitlab.IBLL.RBAC.IBHRDept IBHRDept { get; set; }

        public IList<HREmployee> SearchHREmployeeByRoleID(long longRoleID)
        {
            return ((IDAO.IDHREmployeeDao)base.DBDao).SearchHREmployeeByRoleID(longRoleID);
        }

        public IList<HREmployee> SearchHREmployeeByHRDeptID(long longHRDeptID)
        {
            return ((IDAO.IDHREmployeeDao)base.DBDao).SearchHREmployeeByHRDeptID(longHRDeptID);
        }

        /// <summary>
        /// 查询指定部门和角色下的员工列表
        /// </summary>
        /// <param name="longHRDeptID">部门ID</param>
        /// <param name="longRBACRoleID">角色ID</param>
        /// <returns></returns>
        public IList<HREmployee> SearchHREmployeeByHRDeptIDAndRBACRoleID(long longHRDeptID, long longRBACRoleID)
        {
            IList<HREmployee> tempList = new List<HREmployee>();
            tempList = SearchHREmployeeByRoleID(longRBACRoleID);
            if (tempList != null && tempList.Count > 0)
                tempList = tempList.Where(p => p.HRDept.Id == longHRDeptID).ToList();
            return tempList;
        }

        /// <summary>
        /// 查询指定部门的员工列表，并过滤拥有指定角色的员工
        /// </summary>
        /// <param name="longHRDeptID">部门ID</param>
        /// <param name="longRBACRoleID">角色ID</param>
        /// <returns></returns>
        public IList<HREmployee> SearchHREmployeeNoRBACRoleIDByHRDeptID(long longHRDeptID, long longRBACRoleID)
        {
            IList<HREmployee> tempList = new List<HREmployee>();
            tempList = SearchHREmployeeByHRDeptID("id=" + longHRDeptID.ToString(), -1, -1, "hremployee.HRDept.DispOrder ASC", 0);
            if (tempList != null && tempList.Count > 0)
            {                            
                IList<HREmployee> tempHREmployeeList = new List<HREmployee>();
                foreach (HREmployee tempHREmployee in tempList)
                {
                    if (tempHREmployee.RBACEmpRoleList != null && tempHREmployee.RBACEmpRoleList.Count > 0)
                    {
                        IList<RBACEmpRoles> RBACEmpRoleList = tempHREmployee.RBACEmpRoleList.Where(t => t.RBACRole.Id == longRBACRoleID).ToList();
                        if (RBACEmpRoleList != null && RBACEmpRoleList.Count > 0)
                            tempHREmployeeList.Add(tempHREmployee);
                    }
                }
                foreach (HREmployee tempHREmployee in tempHREmployeeList)
                {
                    tempList.Remove(tempHREmployee);
                }
            }
            return tempList;
        }

        string GetPropertySQLByTree(List<tree> treeList)
        {
            string strWhereSQL = "";

            foreach (tree tempTree in treeList)
            {
                strWhereSQL = strWhereSQL + " or hremployee.HRDept.Id=" + tempTree.tid.ToString();
                if (tempTree.Tree.Count > 0)
                    strWhereSQL = strWhereSQL + GetPropertySQLByTree(tempTree.Tree);
            }
            return strWhereSQL;
        }

        /// <summary>
        /// 查询部门的直属员工列表(包含子部门)
        /// </summary>
        /// <param name="where"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="sort"></param>
        /// <param name="flagRole"> flagRole为null或０，查找所有员工；为１查找已分配角色的员工；；为２查找未分配角色的员工</param>
        /// <returns></returns>
        public IList<HREmployee> SearchHREmployeeByHRDeptID(string where, int page, int limit, string sort, int flagRole)
        {
            IList<HREmployee> tempList = new List<HREmployee>();
            if (where != null && where.Length>0)
            {
                string[] tempHQLList = where.Split('^');
                if (tempHQLList.Length > 0)
                {  
                    long tempHRDeptID = 0; 
                    string tempOtherHQL = "";
                    string strWhereSQL = "";
                    string[] tempIDHQL = tempHQLList[0].Split('=');
                    tempHRDeptID = Int64.Parse(tempIDHQL[1]);
                    if (tempHQLList.Length > 1 && (!string.IsNullOrEmpty(tempHQLList[1])))
                      tempOtherHQL = " and "+tempHQLList[1];
                    BaseResultTree tempBaseResultTree = IBHRDept.SearchHRDeptTree(tempHRDeptID);
                    strWhereSQL = GetPropertySQLByTree(tempBaseResultTree.Tree);
                    if (!string.IsNullOrEmpty(strWhereSQL))
                    {
                        strWhereSQL = "(" + strWhereSQL.Remove(0, 3) + ")" + tempOtherHQL;
                        tempList = this.SearchListByHQL(strWhereSQL, sort, page, limit).list;
                    }
                } 
            }
            if (tempList != null && tempList.Count > 0)
            { 
                if (flagRole==0)
                    return tempList;
                else if (flagRole == 1)
                {
                    tempList = tempList.Where(p => (p.RBACEmpRoleList != null && p.RBACEmpRoleList.Count > 0)).ToList();
                }
                else if (flagRole == 2)
                {
                    tempList = tempList.Where(p => (p.RBACEmpRoleList == null || p.RBACEmpRoleList.Count == 0)).ToList();
                }
            }
            return tempList;
        }

        public IList<HREmployee> SearchHREmployeeByHRPositionID(long longHRPositionID)
        {
            return ((IDAO.IDHREmployeeDao)base.DBDao).SearchHREmployeeByHRPositionID(longHRPositionID);
        }

        public IList<HREmployee> SearchHREmployeeByHRDeptIdentityID(long longHRDeptIdentityID)
        {
            return ((IDAO.IDHREmployeeDao)base.DBDao).SearchHREmployeeByHRDeptIdentityID(longHRDeptIdentityID);
        }

        public IList<HREmployee> SearchHREmployeeByHREmpIdentityID(long longHREmpIdentityID)
        {
            return ((IDAO.IDHREmployeeDao)base.DBDao).SearchHREmployeeByHREmpIdentityID(longHREmpIdentityID);
        }

        public IList<HREmployee> SearchHREmployeeByUserAccount(string strUserAccount)
        {
            return ((IDAO.IDHREmployeeDao)base.DBDao).SearchHREmployeeByUserAccount(strUserAccount);
        }

        public IList<HREmployee> SearchHREmployeeByUserCode(string strUserCode)
        {
            return ((IDAO.IDHREmployeeDao)base.DBDao).SearchHREmployeeByUserCode(strUserCode);
        }

        #endregion

        #region IBHREmployee 成员


        public IList<RBACModule> RBAC_UDTO_SearchModuleByHREmpIDRole(long id, int page, int limit)
        {
            return ((IDAO.IDHREmployeeDao)base.DBDao).SearchModuleByHREmpIDRole(id,page,limit);
        }

        #endregion
    } 
}