using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.BloodTransfusion;
using ZhiFang.IBLL.RBAC;
using ZhiFang.IDAO.NHB.BloodTransfusion;
using ZhiFang.IDAO.RBAC;

namespace ZhiFang.BLL.RBAC
{
    /// <summary>
    ///
    /// </summary>
    public class BDepartment : BaseBLL<Department, int>, IBDepartment
    {
        IBSCOperation IBSCOperation { get; set; }
        IDBloodBReqFormDao IDBloodBReqFormDao { get; set; }

        const string OrgImg16 = "orgImg16";
        const string OrgsImg16 = "orgsImg16";
        const string UserImg16 = "userImg16";
        const string UsersImg16 = "usersImg16";

        #region 同步处理
        public IList<JObject> GetSyncDeptList()
        {
            IList<JObject> objList = new List<JObject>();
            IList<Department> allList = this.LoadAll();
            foreach (var entity in allList)
            {
                if (entity.Id < 0) continue;

                //HRDept dept = _getDepartment(entity);
                JObject jdept = new JObject();
                //JObject jdept = JsonHelper.GetPropertyInfo<Department>(dept);
                jdept.Add("LabID", entity.LabID.ToString());
                jdept.Add("Id", entity.Id.ToString());
                jdept.Add("ParentID", 0);
                jdept.Add("CName", entity.CName);
                jdept.Add("ShortCode", entity.ShortCode);
                jdept.Add("DispOrder", entity.DispOrder);
                if (!string.IsNullOrEmpty(entity.Code1))
                    jdept.Add("StandCode", entity.Code1);
                if (!string.IsNullOrEmpty(entity.Code2))
                    jdept.Add("SName", entity.Code2);
                if (!string.IsNullOrEmpty(entity.Code3))
                    jdept.Add("Comment", entity.Code3);
                jdept.Add("IsUse", entity.Visible.ToString());

                JObject jentity = new JObject();
                jentity.Add("entity", jdept);
                objList.Add(jentity);
            }
            return objList;
        }
        private HRDept _getDepartment(Department department)
        {
            HRDept dept = new HRDept();
            dept.Id = department.Id;
            dept.CName = department.CName;
            dept.Shortcode = department.ShortCode;
            dept.IsUse = department.Visible == 1 ? true : false;
            dept.DispOrder = department.DispOrder;
            dept.Code1 = department.Code1;
            dept.Code2 = department.Code2;
            dept.Code3 = department.Code3;
            dept.Code4 = department.Code4;
            dept.Code5 = department.Code5;

            return dept;
        }
        #endregion

        #region IBDepartment 成员
        public BaseResultTree<Department> SearchDepartmentListTree(int longDepartmentID)
        {
            //longDepartmentID = 2;
            BaseResultTree<Department> tempBaseResultTree = new BaseResultTree<Department>();
            EntityList<Department> tempEntityList = new EntityList<Department>();
            try
            {
                string tempWhereStr = "";
                if (longDepartmentID > 0)
                    tempWhereStr = " department.Id=" + longDepartmentID.ToString();
                else
                    tempWhereStr = " department.ParentID=" + longDepartmentID.ToString();
                List<tree<Department>> tempListTree = new List<tree<Department>>();
                ;
                tempEntityList = this.SearchListByHQL(tempWhereStr, " DispOrder ", -1, -1);
                if ((tempEntityList != null) && (tempEntityList.list != null) && (tempEntityList.list.Count > 0))
                {
                    foreach (Department tempDepartment in tempEntityList.list)
                    {
                        tree<Department> tempTree = new tree<Department>();
                        tempTree.text = tempDepartment.CName;
                        tempTree.tid = tempDepartment.Id.ToString();
                        tempTree.pid = longDepartmentID.ToString();
                        tempTree.objectType = "Department";
                        tempTree.value = tempDepartment;
                        tempTree.expanded = true;
                        var treeList = GetChildTreeList(tempDepartment.Id);
                        tempTree.Tree = treeList;
                        if (treeList.Length > 0)
                        {
                            tempTree.leaf = false;
                        }
                        else
                        {
                            //tempTree.Tree = new tree<Department>[] { };
                            tempTree.leaf = true;
                        }
                        //tempTree.leaf = (tempTree.Tree.Length <= 0);
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

        public tree<Department>[] GetChildTreeList(int ParentID)
        {
            List<tree<Department>> tempListTree = new List<tree<Department>>();
            try
            {
                EntityList<Department> tempEntityList = this.SearchListByHQL(" department.ParentID=" + ParentID.ToString(), " DispOrder ", -1, -1);
                if ((tempEntityList != null) && (tempEntityList.list != null) && (tempEntityList.list.Count > 0))
                {
                    foreach (Department tempDepartment in tempEntityList.list)
                    {
                        tree<Department> tempTree = new tree<Department>();
                        tempTree.text = tempDepartment.CName;
                        tempTree.tid = tempDepartment.Id.ToString();
                        tempTree.pid = ParentID.ToString();
                        tempTree.objectType = "Department";
                        tempTree.value = tempDepartment;
                        //tempTree.Tree = GetChildTreeList(tempDepartment.Id);
                        var treeList = GetChildTreeList(tempDepartment.Id);
                        tempTree.Tree = treeList;
                        if (treeList.Length > 0)
                        {
                            tempTree.leaf = false;
                        }
                        else
                        {
                            tempTree.leaf = true;
                            //tempTree.Tree = new tree<Department>[] { };
                        }
                        //tempTree.leaf = (tempTree.Tree.Length <= 0);
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

        //ZhiFang.IBLL.RBAC.IBHREmployee IBHREmployee { get; set; }
        //IDHREmployeeDao HREmployeeDBDao { get; set; }
        //public IList<Department> SearchDepartmentByHREmpID(long longHREmpID)
        //{
        //    return ((IDDepartmentDao)base.DBDao).SearchDepartmentByHREmpID(longHREmpID);
        //}

        //public IList<Department> SearchDepartmentByDepartmentIdentity(long longDepartmentIdentity)
        //{
        //    return ((IDDepartmentDao)base.DBDao).SearchDepartmentByDepartmentIdentity(longDepartmentIdentity);
        //}
        public BaseResultTree SearchDepartmentTree(int longDepartmentID)
        {
            //longDepartmentID = 2;
            BaseResultTree tempBaseResultTree = new BaseResultTree();
            List<tree> tempListTree = new List<tree>();
            try
            {
                string tempWhereStr = "";//为空查整个部门表
                if (longDepartmentID > 0)
                    tempWhereStr = " department.Id=" + longDepartmentID.ToString();
                else
                    tempWhereStr = " department.ParentID=" + longDepartmentID.ToString();
                EntityList<Department> tempEntityList = this.SearchListByHQL(tempWhereStr, " DispOrder ", -1, -1);

                if ((tempEntityList != null) && (tempEntityList.list != null) && (tempEntityList.list.Count > 0))
                {
                    foreach (Department tempDepartment in tempEntityList.list)
                    {
                        tree tempTree = new tree();
                        tempTree.text = tempDepartment.CName;
                        tempTree.tid = tempDepartment.Id.ToString();
                        tempTree.pid = longDepartmentID.ToString();
                        //tempTree.objectType = tempDepartment.GetType().Name;
                        tempTree.objectType = "Department";
                        tempTree.expanded = true;
                        //tempTree.Tree = GetChildTree(tempDepartment.Id);
                        //tempTree.leaf = (tempTree.Tree.Count <= 0);
                        var treeList = GetChildTree(tempDepartment.Id);
                        if (treeList.Count > 0)
                        {
                            tempTree.leaf = false;
                            tempTree.Tree = treeList;
                        }
                        else
                        {
                            tempTree.leaf = true;
                            tempTree.Tree = new List<tree>();
                        }
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

        public List<tree> GetChildTree(int ParentID)
        {
            List<tree> tempListTree = new List<tree>();
            try
            {
                EntityList<Department> tempEntityList = this.SearchListByHQL(" department.ParentID=" + ParentID.ToString(), -1, -1);
                if ((tempEntityList != null) && (tempEntityList.list != null) && (tempEntityList.list.Count > 0))
                {
                    foreach (Department tempDepartment in tempEntityList.list)
                    {
                        tree tempTree = new tree();
                        tempTree.text = tempDepartment.CName;
                        tempTree.tid = tempDepartment.Id.ToString();
                        tempTree.pid = ParentID.ToString();
                        tempTree.objectType = "Department";
                        //tempTree.Tree = GetChildTree(tempDepartment.Id);
                        var treeList = GetChildTree(tempDepartment.Id);
                        if (treeList.Count > 0)
                        {
                            tempTree.leaf = false;
                            tempTree.Tree = treeList;
                        }
                        else
                        {
                            tempTree.leaf = true;
                            tempTree.Tree = new List<tree>();
                        }
                        //tempTree.leaf = (tempTree.Tree.Count <= 0);
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

        public BaseResultTree GetDepartmentEmployeeFrameTree(int longDepartmentID)
        {
            //longDepartmentID = 2;
            BaseResultTree tempBaseResultTree = new BaseResultTree();
            //IList<HREmployee> tempHREmployeeList = new List<HREmployee>();
            tempBaseResultTree = SearchDepartmentTree(longDepartmentID);
            GetDepartmentEmployeeFrameChildTree(tempBaseResultTree.Tree);
            return tempBaseResultTree;
        }

        public void GetDepartmentEmployeeFrameChildTree(IList<tree> treeList)
        {
            Department tempDepartment = new Department();
            for (int i = 0; i < treeList.Count; i++)
            {
                GetDepartmentEmployeeFrameChildTree(treeList[i].Tree);
                tempDepartment = this.Get(int.Parse(treeList[i].tid));
                if (tempDepartment != null)
                {
                    //foreach (HREmployee tempHREmployee in tempDepartment.HREmployeeList)
                    //{
                    //    tree tempHREmployeeTree = new tree();
                    //    tempHREmployeeTree.text = tempHREmployee.CName;
                    //    tempHREmployeeTree.tid = tempHREmployee.Id.ToString();
                    //    tempHREmployeeTree.pid = treeList[i].tid;
                    //    tempHREmployeeTree.objectType = "HREmployee";
                    //    tempHREmployeeTree.leaf = true;
                    //    tempHREmployeeTree.iconCls = tempHREmployeeTree.leaf ? "userImg16" : "usersImg16";
                    //    if (treeList[i].Tree == null)
                    //        treeList[i].Tree = new List<tree>();
                    //    treeList[i].Tree.Add(tempHREmployeeTree);
                    //    treeList[i].leaf = false;
                    //    treeList[i].iconCls = treeList[i].leaf ? "orgImg16" : "orgsImg16";
                    //}
                }
            }
        }
        public string SearchDepIdStrByDeptId(int deptId)
        {
            string listID = "";
            IList<Department> deptList = new List<Department>();
            try
            {
                string tempWhereStr = "";//为空查整个部门表
                if (deptId > 0)
                    tempWhereStr = " department.Id=" + deptId.ToString();
                else
                    tempWhereStr = " department.ParentID=" + deptId.ToString();
                EntityList<Department> tempEntityList = this.SearchListByHQL(tempWhereStr, -1, -1);

                if ((tempEntityList != null) && (tempEntityList.list != null) && (tempEntityList.list.Count > 0))
                {
                    foreach (Department tempDepartment in tempEntityList.list)
                    {
                        IList<Department> tempList = GetChildList(tempDepartment.Id);
                        deptList = deptList.Union(tempList).ToList();
                        if (!deptList.Contains(tempDepartment))
                            deptList.Add(tempDepartment);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            if (deptList.Count > 0)
            {
                foreach (Department dept in deptList)
                {
                    if (string.IsNullOrEmpty(listID))
                        listID = dept.Id.ToString();
                    else
                        listID += "," + dept.Id.ToString();
                }
            }
            return listID;
        }
        public IList<Department> GetChildList(int ParentID)
        {
            List<Department> deptList = new List<Department>();
            try
            {
                EntityList<Department> tempEntityList = this.SearchListByHQL(" department.ParentID=" + ParentID.ToString(), -1, -1);
                if ((tempEntityList != null) && (tempEntityList.list != null) && (tempEntityList.list.Count > 0))
                {
                    foreach (Department tempHRDept in tempEntityList.list)
                    {
                        IList<Department> tempList = GetChildList(tempHRDept.Id);
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
        public IList<Department> SearchDepartmentListByDepartmentId(int deptId)
        {
            IList<Department> deptList = new List<Department>();
            try
            {
                string tempWhereStr = "";//为空查整个部门表
                if (deptId > 0)
                    tempWhereStr = " hrdept.Id=" + deptId.ToString();
                else
                    tempWhereStr = " department.ParentID=" + deptId.ToString();
                EntityList<Department> tempEntityList = this.SearchListByHQL(tempWhereStr, -1, -1);

                if ((tempEntityList != null) && (tempEntityList.list != null) && (tempEntityList.list.Count > 0))
                {
                    foreach (Department tempHRDept in tempEntityList.list)
                    {
                        IList<Department> tempList = GetChildList(tempHRDept.Id);
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

        #endregion

        #region 修改信息记录
        public void AddSCOperation(Department serverEntity, string[] arrFields, long empID, string empName)
        {
            StringBuilder strbMemo = new StringBuilder();
            EditGetUpdateMemo<Department>(serverEntity, this.Entity, this.Entity.GetType(), arrFields, ref strbMemo);
            if (strbMemo.Length > 0)
            {
                SCOperation sco = new SCOperation();
                sco.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                sco.LabID = serverEntity.LabID;
                sco.BobjectID = this.Entity.Id;
                sco.CreatorID = empID;
                if (empName != null && empName.Trim() != "")
                    sco.CreatorName = empName;
                sco.BusinessModuleCode = "Department";
                strbMemo.Insert(0, "本次修改记录:" + System.Environment.NewLine);
                //ZhiFang.Common.Log.Log.Debug("修改人:" + empName + "," + strbMemo.ToString());
                sco.Memo = strbMemo.ToString();
                sco.IsUse = true;
                sco.Type = long.Parse(UpdateOperationType.科室修改记录.Key);
                sco.TypeName = UpdateOperationType.GetStatusDic()[sco.Type.ToString()].Name;
                IBSCOperation.Entity = sco;
                IBSCOperation.Add();
            }
        }
        private void EditGetUpdateMemo<T>(T serverEntity, T updateEntity, Type type, string[] arrFields, ref StringBuilder strbMemo)
        {
            //为空判断
            if (serverEntity == null && updateEntity == null)
                return;
            else if (serverEntity == null || updateEntity == null)
                return;

            Type t = type;
            System.Reflection.PropertyInfo[] props = t.GetProperties();
            foreach (var po in props)
            {
                if (po.Name == "Id" || po.Name == "LabID" || po.Name == "DataAddTime" || po.Name == "DataUpdateTime" || po.Name == "DataTimeStamp")
                    continue;

                if (arrFields.Contains(po.Name) == true && IsCanCompare(po.PropertyType))
                {
                    object serverValue = po.GetValue(serverEntity, null);
                    object updateValue = po.GetValue(updateEntity, null);
                    if (serverValue == null)
                        serverValue = "";
                    if (updateValue == null)
                        updateValue = "";
                    if (!serverValue.ToString().Equals(updateValue.ToString()))
                    {
                        string cname = po.Name;
                        foreach (var pattribute in po.GetCustomAttributes(false))
                        {
                            if (pattribute.ToString() == "ZhiFang.Entity.Base.DataDescAttribute")
                            {
                                cname = ((DataDescAttribute)pattribute).CName;
                                break;
                            }
                        }
                        if (string.IsNullOrEmpty(serverValue.ToString()))
                            serverValue = "空";
                        if (string.IsNullOrEmpty(updateValue.ToString()))
                            updateValue = "空";
                        strbMemo.Append("【" + cname + "】由原来:" + serverValue.ToString() + ",修改为:" + updateValue.ToString() + ";" + System.Environment.NewLine);
                    }
                }
            }
        }
        /// <summary>
        /// 该类型是否可直接进行值的比较
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private bool IsCanCompare(Type t)
        {
            if (t.IsValueType)
            {
                return true;
            }
            else
            {
                //String是特殊的引用类型，它可以直接进行值的比较
                if (t.FullName == typeof(String).FullName)
                {
                    return true;
                }
                return false;
            }
        }
        #endregion

    }
}