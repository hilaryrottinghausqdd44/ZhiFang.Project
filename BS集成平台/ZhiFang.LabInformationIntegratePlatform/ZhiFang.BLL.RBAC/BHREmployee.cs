using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using ZhiFang.IDAO.RBAC;
using ZhiFang.Entity.RBAC;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using System.Data;

namespace ZhiFang.BLL.RBAC
{	
	public class BHREmployee : BaseBLL<HREmployee>, ZhiFang.IBLL.RBAC.IBHREmployee
    {
        #region IBHREmployee 成员
        ZhiFang.IBLL.RBAC.IBHRDept IBHRDept { get; set; }
        ZhiFang.IDAO.RBAC.IDHRDeptDao IDHRDeptDao { get; set; }
        IDBSexDao IDBSexDao { get; set; }

        public IList<HREmployee> SearchHREmployeeByRoleID(long longRoleID)
        {
            return ((IDHREmployeeDao)base.DBDao).SearchHREmployeeByRoleID(longRoleID);
        }

        public IList<HREmployee> SearchHREmployeeByHRDeptID(long longHRDeptID)
        {
            return ((IDHREmployeeDao)base.DBDao).SearchHREmployeeByHRDeptID(longHRDeptID);
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

        /// <summary>
        /// 查询部门的直属员工列表(包含子部门)
        /// </summary>
        /// <param name="where"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="sort"></param>
        /// <param name="flagRole"> flagRole为null或０，查找所有员工；为１查找已分配角色的员工；；为２查找未分配角色的员工</param>
        /// <returns></returns>
        public EntityList<HREmployee> SearchHREmployeeByHRDeptID(string where, int page, int limit, string sort)
        {
            EntityList<HREmployee> tempList = new EntityList<HREmployee>();
            if (where != null && where.Length > 0)
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
                        tempOtherHQL = " and " + tempHQLList[1];
                    BaseResultTree tempBaseResultTree = IBHRDept.SearchHRDeptTree(tempHRDeptID);
                    strWhereSQL = GetPropertySQLByTree(tempBaseResultTree.Tree);
                    if (!string.IsNullOrEmpty(strWhereSQL))
                    {
                        strWhereSQL = "(" + strWhereSQL.Remove(0, 3) + ")" + tempOtherHQL;
                        tempList = this.SearchListByHQL(strWhereSQL, sort, page, limit);
                    }
                }
            }            
            return tempList;
        }

        public IList<HREmployee> SearchHREmployeeByHRPositionID(long longHRPositionID)
        {
            return ((IDHREmployeeDao)base.DBDao).SearchHREmployeeByHRPositionID(longHRPositionID);
        }

        public IList<HREmployee> SearchHREmployeeByHRDeptIdentityID(long longHRDeptIdentityID)
        {
            return ((IDHREmployeeDao)base.DBDao).SearchHREmployeeByHRDeptIdentityID(longHRDeptIdentityID);
        }

        public IList<HREmployee> SearchHREmployeeByHREmpIdentityID(long longHREmpIdentityID)
        {
            return ((IDHREmployeeDao)base.DBDao).SearchHREmployeeByHREmpIdentityID(longHREmpIdentityID);
        }

        public IList<HREmployee> SearchHREmployeeByUserAccount(string strUserAccount)
        {
            return ((IDHREmployeeDao)base.DBDao).SearchHREmployeeByUserAccount(strUserAccount);
        }

        public IList<HREmployee> SearchHREmployeeByUserCode(string strUserCode)
        {
            return ((IDHREmployeeDao)base.DBDao).SearchHREmployeeByUserCode(strUserCode);
        }
        
        public IList<RBACModule> RBAC_UDTO_SearchModuleByHREmpIDRole(long id, int page, int limit)
        {
            return ((IDHREmployeeDao)base.DBDao).SearchModuleByHREmpIDRole(id,page,limit);
        }

        public IList<HREmployee> SearchHREmployeeByHRDeptIDIncludeSubHRDept(long hRDeptID, int page, int limit, string sort)
        {
            List<long> deptidlist = new List<long>();
            List<long> tmpdeptidlist = IDHRDeptDao.GetSubDeptIdListByDeptId(hRDeptID);
            if (tmpdeptidlist != null && tmpdeptidlist.Count > 0)
            {
                deptidlist = tmpdeptidlist;
            }
            deptidlist.Add(hRDeptID);
            IList<ZhiFang.Entity.RBAC.HREmployee> allemplist = DBDao.GetListByHQL(" IsUse=true and HRDept.Id in (" + string.Join(",", deptidlist.ToArray()) + ") ");
            return allemplist;
        }

        public EntityList<HREmployee> SearchHREmployeeByManagerEmpId(long EmpId, string where, int page, int limit, string sort)
        {
            List<long> hrdeptidlist = new List<long>();
            EntityList<HREmployee> entityemplist = new EntityList<HREmployee>();
            long tmpid = EmpId;
            if (tmpid <= 0)
            {
                return null;
            }
            IList<ZhiFang.Entity.RBAC.HRDept> hrdeptlist = IDHRDeptDao.GetListByHQL(" ManagerID=" + EmpId);
            if (hrdeptlist.Count > 0)
            {
                foreach (Entity.RBAC.HRDept dept in hrdeptlist)
                {
                    hrdeptidlist.Add(dept.Id);
                    List<long> tmphrdeptlist = IDHRDeptDao.GetSubDeptIdListByDeptId(dept.Id);
                    foreach (long id in tmphrdeptlist)
                    {
                        hrdeptidlist.Add(id);
                    }
                }
                entityemplist = DBDao.GetListByHQL(where + " and IsUse=true and HRDept.Id in (" + string.Join(",", hrdeptidlist.ToArray()) + ") ", sort, page, limit);
            }
            return entityemplist;
        }



        #endregion

        public IList<HREmployee> GetHREmployeeAllList()
        {
            return DBDao.LoadAll();
        }

        public IList<HREmployee> GetHREmployeeListByDeptIdList(string idlist)
        {
            return DBDao.GetListByHQL(" HRDept.Id in (" + idlist + ")").ToList();
        }

        public bool TUpdate(HREmployee entity)
        {
            HREmployee emp = DBDao.Get(entity.Id);
            if (emp == null)
            {
                return false;
                //brdv.ErrorInfo = "员工Id值无效！";
                //brdv.ResultDataValue = "{id:''}";
                //brdv.success = false;
                //return WebOperationContext.Current.CreateTextResponse(Common.Public.JsonSerializer.JsonDotNetSerializer(brdv), "text/plain", Encoding.UTF8);
            }
            emp.SignatureImage = entity.SignatureImage;
            return DBDao.Update(emp);
        }

        public BaseResultDataValue TransformEmpByExcel(DataTable tmpdt)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            #region 验证数据格式
            if (tmpdt == null || tmpdt.Rows.Count <= 0)
                return null;

            List<string> ColList = new List<string>() { "人员编码","人员名称","部门编码", "部门名称", "性别", "出生日期", "英文名称", "电话",  "显示次序", "备注" };
            if (tmpdt.Columns.Count != ColList.Count)
                return new BaseResultDataValue() { ErrorInfo = "导入数据文件格式数据列不符合要求!", success = false };
            List<string> errcollist = new List<string>();
            ColList.ForEach(a =>
            {
                if (!tmpdt.Columns.Contains(a))
                    errcollist.Add(a);
            });
            if (errcollist.Count > 0)
                return new BaseResultDataValue() { ErrorInfo = $"导入数据文件格式数据列不符合要求,缺少列{string.Join(",", errcollist)}!", success = false };

            List<string> errrowlist = new List<string>();
            for (int i = 0; i < tmpdt.Rows.Count; i++)
            {
                if (tmpdt.Rows[i]["人员名称"] == DBNull.Value || tmpdt.Rows[i]["人员名称"] == null)
                {
                    errrowlist.Add("行:"+i.ToString()+",人员名称为空!");
                    continue;
                }

                if (tmpdt.Rows[i]["人员编码"] == DBNull.Value || tmpdt.Rows[i]["人员编码"] == null)
                {
                    errrowlist.Add("行:" + i.ToString() + ",人员编码为空!");
                    continue;
                }
                if (tmpdt.Rows[i]["部门名称"] == DBNull.Value || tmpdt.Rows[i]["部门名称"] == null)
                {
                    errrowlist.Add("行:" + i.ToString() + ",部门名称为空!");
                    continue;
                }

                if (tmpdt.Rows[i]["部门编码"] == DBNull.Value || tmpdt.Rows[i]["部门编码"] == null)
                {
                    errrowlist.Add("行:" + i.ToString() + ",部门编码为空!");
                    continue;
                }

                if (tmpdt.Rows[i]["性别"] == DBNull.Value || tmpdt.Rows[i]["性别"] == null || !new List<string>() { "男","女","男转女","女转男","未知"}.Contains( tmpdt.Rows[i]["性别"].ToString().Trim()))
                {
                    errrowlist.Add("行:" + i.ToString() + ",性别内容为空或格式错误!");
                    continue;
                }
                if (tmpdt.Rows[i]["出生日期"] == DBNull.Value || tmpdt.Rows[i]["出生日期"] == null|| !DateTime.TryParse(tmpdt.Rows[i]["出生日期"].ToString(),out DateTime tmpdate))
                {
                    errrowlist.Add("行:" + i.ToString() + ",出生日期内容为空或格式错误!");
                    continue;
                }
            }
            if (errrowlist.Count > 0)
                return new BaseResultDataValue() { ErrorInfo = $"导入数据文件格式数据记录不符合要求,数据:{string.Join(",", errrowlist)}!", success = false };
            #endregion
            List<HREmployee> tmpemplistall = new List<HREmployee>();
            List<HREmployee> tmpemplist = new List<HREmployee>();
            for (int i = 0; i < tmpdt.Rows.Count; i++)
            {
                HREmployee tmp = new HREmployee();
                tmp.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                tmp.CName = tmpdt.Rows[i]["人员名称"].ToString();
                tmp.NameL= tmp.CName;
                tmp.NameF  = "";
                tmp.StandCode = tmpdt.Rows[i]["人员编码"].ToString();

                tmp.HRDept = new HRDept() { StandCode= tmpdt.Rows[i]["部门编码"].ToString() };
                tmp.BSex = new BSex() { Name = tmpdt.Rows[i]["性别"].ToString() };
                tmp.Birthday = DateTime.Parse(tmpdt.Rows[i]["出生日期"].ToString());
                tmp.EName = (tmpdt.Rows[i]["英文名称"] !=null)?tmpdt.Rows[i]["英文名称"].ToString().Trim():"";              

               
                if (tmpdt.Rows[i]["电话"] != null)
                    tmp.Tel = tmpdt.Rows[i]["电话"].ToString();

                if (tmpdt.Rows[i]["显示次序"] != null && int.TryParse(tmpdt.Rows[i]["显示次序"].ToString(), out int aaa))
                    tmp.DispOrder = aaa;

                if (tmpdt.Rows[i]["备注"] != null)
                    tmp.Comment = tmpdt.Rows[i]["备注"].ToString();

                tmpemplistall.Add(tmp);
            }
            List<HREmployee> emplist = new List<HREmployee>();
            IList<HREmployee> empilist = DBDao.GetListByHQL(" IsUse=1 ");
            if (empilist != null && empilist.Count > 0)
                emplist = emplist.ToList();

            IList<BSex> BSexlist = IDBSexDao.GetListByHQL(" IsUse=1 ");

            List<HRDept> deptlist = new List<HRDept>();
            IList<HRDept> deptilist = IDHRDeptDao.GetListByHQL(" IsUse=1 ");
            if (deptilist != null && deptilist.Count > 0)
                deptlist = deptilist.ToList();

            foreach (var emp in tmpemplistall)
            {
                if (emplist.Count(a => a.StandCode == emp.StandCode) > 0)
                {
                    emp.ErrorInfo = $"系统内已存在编码重复的员工!员工编码{emp.StandCode}";
                    tmpemplist.Add(emp);
                    continue;
                }
                if (tmpemplistall.Count(a => a.StandCode == emp.StandCode) > 1)
                {
                    emp.ErrorInfo = $"上传数据中存在编码重复的员工!员工编码{emp.StandCode}";
                    tmpemplist.Add(emp);
                    continue;
                }
                if (emp.HRDept!=null && !string.IsNullOrEmpty(emp.HRDept.StandCode))
                {

                    if (deptlist.Count(a => a.StandCode == emp.HRDept.StandCode) > 0)
                    {
                        emp.HRDept = deptlist.Where(a => a.StandCode == emp.HRDept.StandCode).First();
                        emp.HRDept.HRDeptEmpList = null;
                        emp.HRDept.HRDeptIdentityList = null;
                        emp.HRDept.HREmployeeList = null;
                    }
                    else
                    {
                        emp.ErrorInfo = $"未能找到所属部门!编码{emp.HRDept.StandCode}";
                    }
                }
                if (emp.BSex != null && !string.IsNullOrEmpty(emp.BSex.Name))
                {

                    if (BSexlist.Count(a => a.Name == emp.BSex.Name.Trim()) > 0)
                    {
                        emp.BSex = BSexlist.Where(a => a.Name == emp.BSex.Name.Trim()).First();
                    }
                    else
                    {
                        emp.ErrorInfo = $"未能找到性别字典数据!编码{emp.BSex.Name}";
                    }
                }
                emp.HRDeptEmpList = null;
                emp.HREmpIdentityList = null;
                tmpemplist.Add(emp);
            }

            brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(tmpemplist);
            brdv.success = true;
            return brdv;
        }
    } 
}