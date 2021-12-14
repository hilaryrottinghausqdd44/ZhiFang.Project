using System;
using System.Collections.Generic;
using ZhiFang.IDAO.RBAC;
using ZhiFang.Entity.RBAC;
using ZhiFang.Entity.Base;
using ZhiFang.BLL.Base;
using ZhiFang.IBLL.RBAC;
using System.Linq;
using System.Data;

namespace ZhiFang.BLL.RBAC
{
    public class BHRDept : BaseBLL<ZhiFang.Entity.RBAC.HRDept>, IBHRDept
    {
        #region IBHRDept 成员
        const string OrgImg16 = "orgImg16";
        const string OrgsImg16 = "orgsImg16";
        const string UserImg16 = "userImg16";
        const string UsersImg16 = "usersImg16";

        public override bool Add()
        {
            List<HRDept> deptlist = new List<HRDept>();
            IList<HRDept> deptilist = DBDao.GetListByHQL(" IsUse=1 ");
            if (deptilist != null && deptilist.Count > 0)
                deptlist = deptilist.ToList();

            if (Entity != null && Entity.ParentID > 0)
            {
                var deptp = DBDao.Get(Entity.ParentID);
                if (deptp == null)
                {
                    throw new Exception("部门树父节点为空!请检查数据!ParentID=" + Entity.ParentID);
                }
                List<HRDept> deptplist = GetParentList(Entity.ParentID);
                deptplist.Add(deptp);

                if (deptplist.Count(a => a.ParentID == Entity.Id) > 0)
                {
                    throw new Exception("部门树节点循环引用!请检查数据!ParentID=" + Entity.Id);
                }
            }

            return DBDao.Save(Entity);
        }
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
                EntityList<HRDept> tempEntityList = this.SearchListByHQL(tempWhereStr, " DispOrder ", -1, -1);

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
                EntityList<HRDept> tempEntityList = this.SearchListByHQL(" hrdept.ParentID=" + ParentID.ToString(), " DispOrder ", -1, -1);
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
            for (int i = 0; i < treeList.Count; i++)
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
                throw ex;
            }
            return deptList;
        }
        public List<HRDept> GetParentList(long DeptID)
        {
            List<HRDept> deptList = new List<HRDept>();
            try
            {
                HRDept tmpdept = this.Get(DeptID);
                if ((tmpdept != null) && (tmpdept.ParentID > 0))
                {
                    var tmpparentdept = DBDao.Get(tmpdept.ParentID);
                    if (tmpparentdept != null)
                    {
                        List<HRDept> tempList = GetParentList(tmpparentdept.Id);
                        if (tempList != null && tempList.Count > 0)
                            deptList = deptList.Union(tempList).ToList();
                        if (!deptList.Contains(tmpparentdept))
                            deptList.Add(tmpparentdept);
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
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

        /// <summary>
        /// 获取到某一部门的所有下级部门Id信息
        /// </summary>
        /// <param name="id">当前部门信息</param>
        /// <returns></returns>
        public IList<long> GetSubDeptIdListByDeptId(long id)
        {
            return ((IDHRDeptDao)base.DBDao).GetSubDeptIdListByDeptId(id);
        }

        /// <summary>
        /// 获取到某一部门的所有父级部门Id(ParentID=0结束)信息
        /// </summary>
        /// <param name="id">当前部门信息</param>
        /// <returns></returns>
        public IList<long> GetParentDeptIdListByDeptId(long id)
        {
            return ((IDHRDeptDao)base.DBDao).GetParentDeptIdListByDeptId(id);
        }

        public BaseResultDataValue TransformDeptByExcel(DataTable tmpdt)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            #region 验证数据格式
            if (tmpdt == null || tmpdt.Rows.Count <= 0)
                return null;

            List<string> ColList = new List<string>() { "部门编码", "部门名称", "父级部门编码", "父级部门名称", "英文名称", "简称", "地址", "电话", "传真", "显示次序", "备注" };
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
                if (tmpdt.Rows[i]["部门名称"] == DBNull.Value || tmpdt.Rows[i]["部门名称"] == null)
                {
                    errrowlist.Add(i.ToString());
                    continue;
                }

                if (tmpdt.Rows[i]["部门编码"] == DBNull.Value || tmpdt.Rows[i]["部门编码"] == null)
                {
                    errrowlist.Add(i.ToString());
                    continue;
                }
            }
            if (errrowlist.Count > 0)
                return new BaseResultDataValue() { ErrorInfo = $"导入数据文件格式数据记录不符合要求,行数{string.Join(",", errrowlist)}!", success = false };
            #endregion
            List<HRDept> tmpdeptlistall = new List<HRDept>();
            List<HRDept> tmpdeptlist = new List<HRDept>();
            for (int i = 0; i < tmpdt.Rows.Count; i++)
            {
                HRDept tmp = new HRDept();
                tmp.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                tmp.CName = tmpdt.Rows[i]["部门名称"].ToString();
                tmp.StandCode = tmpdt.Rows[i]["部门编码"].ToString();
                //tmp.ParentName = tmpdt.Rows[i]["父级部门名称"].ToString();
                if (tmpdt.Rows[i]["父级部门编码"] != null)
                    tmp.ParentStandCode = tmpdt.Rows[i]["父级部门编码"].ToString();

                if (tmpdt.Rows[i]["英文名称"] != null)
                    tmp.EName = tmpdt.Rows[i]["英文名称"].ToString();

                if (tmpdt.Rows[i]["简称"] != null)
                    tmp.SName = tmpdt.Rows[i]["简称"].ToString();

                if (tmpdt.Rows[i]["地址"] != null)
                    tmp.Address = tmpdt.Rows[i]["地址"].ToString();

                if (tmpdt.Rows[i]["电话"] != null)
                    tmp.Tel = tmpdt.Rows[i]["电话"].ToString();

                if (tmpdt.Rows[i]["传真"] != null)
                    tmp.Fax = tmpdt.Rows[i]["传真"].ToString();

                if (tmpdt.Rows[i]["显示次序"] != null && int.TryParse(tmpdt.Rows[i]["显示次序"].ToString(), out int aaa))
                    tmp.DispOrder = aaa;

                if (tmpdt.Rows[i]["备注"] != null)
                    tmp.Comment = tmpdt.Rows[i]["备注"].ToString();

                tmpdeptlistall.Add(tmp);
            }
            List<HRDept> deptlist = new List<HRDept>();
            IList<HRDept> deptilist = DBDao.GetListByHQL(" IsUse=1 ");
            if (deptilist != null && deptilist.Count > 0)
                deptlist = deptilist.ToList();

            foreach (var dept in tmpdeptlistall)
            {
                if (deptlist.Count(a => a.StandCode == dept.StandCode) > 0)
                {
                    dept.ErrorInfo = $"系统内已存在编码重复的部门!部门编码{dept.StandCode}";
                    tmpdeptlist.Add(dept);
                    continue;
                }
                if (tmpdeptlistall.Count(a => a.StandCode == dept.StandCode) > 1)
                {
                    dept.ErrorInfo = $"上传数据中存在编码重复的部门!部门编码{dept.StandCode}";
                    tmpdeptlist.Add(dept);
                    continue;
                }
                if (!string.IsNullOrEmpty(dept.ParentStandCode))
                {

                    if (deptlist.Count(a => a.StandCode == dept.ParentStandCode) > 0)
                    {
                        dept.ParentID = deptlist.Where(a => a.StandCode == dept.ParentStandCode).First().Id;
                        dept.ParentName = deptlist.Where(a => a.StandCode == dept.ParentStandCode).First().CName;
                    }
                    else
                    {
                        if (tmpdeptlistall.Count(a => a.StandCode == dept.ParentStandCode) > 0)
                        {
                            dept.ParentID = tmpdeptlistall.Where(a => a.StandCode == dept.ParentStandCode).First().Id;
                            dept.ParentName = tmpdeptlistall.Where(a => a.StandCode == dept.ParentStandCode).First().CName;
                        }
                        else
                        {
                            dept.ErrorInfo = $"未能找到父部门!编码{dept.ParentStandCode}";
                        }
                    }
                }
                tmpdeptlist.Add(dept);
            }

            brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(tmpdeptlist);
            brdv.success = true;
            return brdv;
        }
    }
}
