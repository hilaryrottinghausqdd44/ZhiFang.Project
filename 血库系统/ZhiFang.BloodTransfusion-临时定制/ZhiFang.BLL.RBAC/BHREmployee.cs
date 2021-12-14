using System;
using System.Collections.Generic;
using System.Linq;
using ZhiFang.IDAO.RBAC;
using ZhiFang.Entity.RBAC;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using System.Data;
using ZhiFang.BloodTransfusion.Common;

namespace ZhiFang.BLL.RBAC
{	
	public class BHREmployee : BaseBLL<HREmployee>, ZhiFang.IBLL.RBAC.IBHREmployee
    {
        #region IBHREmployee 成员
        ZhiFang.IBLL.RBAC.IBHRDept IBHRDept { get; set; }
        ZhiFang.IDAO.RBAC.IDHRDeptDao IDHRDeptDao { get; set; }
        ZhiFang.IBLL.RBAC.IBRBACUser IBRBACUser { get; set; }
        ZhiFang.IBLL.RBAC.IBBSex IBBSex { get; set; }
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
        public BaseResultDataValue CheckHREmployeeExcelFormat(string excelFilePath, string serverPath)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string xmlHREmployee = serverPath + "\\BaseTableXML\\HREmployee.xml";
            if (System.IO.File.Exists(xmlHREmployee))
            {
                IList<string> dicColumn = ExcelDataCommon.GetRequiredFieldByXml(xmlHREmployee);
                Dictionary<string, Type> dicType = ExcelDataCommon.GetFieldTypeByXml<HREmployee>(xmlHREmployee);
                baseResultDataValue.success = MyNPOIHelper.CheckExcelFile(excelFilePath, dicColumn, dicType);
                if (!baseResultDataValue.success)
                {
                    baseResultDataValue.ErrorInfo = "Error001";
                    baseResultDataValue.ResultDataValue = System.IO.Path.GetFileName(excelFilePath);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "人员导入配置信息不存在！";
                ZhiFang.Common.Log.Log.Info("人员导入配置信息不存在！");
            }
            return baseResultDataValue;
        }//

        public BaseResultDataValue AddHREmployeeByExcel(string deptID, string excelFilePath, string serverPath)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            DataTable dt = MyNPOIHelper.ImportExceltoDataTable(excelFilePath);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataColumn dataColumn in dt.Columns)
                {
                    dataColumn.ColumnName = dataColumn.ColumnName.Trim();
                }
                string xmlHREmployee = serverPath + "\\BaseTableXML\\HREmployee.xml";
                if (System.IO.File.Exists(xmlHREmployee))
                {
                    DataSet dataSet = new DataSet();
                    dataSet.Tables.Add(dt);

                    HRDept dept = null;
                    if (!string.IsNullOrWhiteSpace(deptID) && Int64.Parse(deptID) > 0)
                        dept = IBHRDept.Get(Int64.Parse(deptID));
                    IList<string> listPrimaryKey = new List<string>();
                    Dictionary<string, string> dicDefaultValue = new Dictionary<string, string>();
                    Dictionary<string, string> dicColumn = ExcelDataCommon.GetColumnNameByDataSet(dataSet, xmlHREmployee, listPrimaryKey, dicDefaultValue);
                    if (listPrimaryKey.Count > 0)
                    {
                        if (dept != null)
                            baseResultDataValue = _AddHREmployeeDataTable(dt, dept, dicColumn, listPrimaryKey, dicDefaultValue);
                        else
                            baseResultDataValue = _AddHREmployeeDataTable(dt, dicColumn, listPrimaryKey, dicDefaultValue);
                        baseResultDataValue.success = MyNPOIHelper.GetInputExcelFileState(excelFilePath, dt);
                        if (baseResultDataValue.success)
                        {
                            baseResultDataValue.ErrorInfo = baseResultDataValue.ResultDataValue;
                            baseResultDataValue.ResultDataValue = System.IO.Path.GetFileName(excelFilePath);
                        }
                    }
                    else
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "人员导入对照表没有设置唯一键！";
                    }
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "人员导入配置信息不存在！";
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "人员数据信息为空！";
            }
            if (!baseResultDataValue.success)
                ZhiFang.Common.Log.Log.Info(baseResultDataValue.ErrorInfo);
            return baseResultDataValue;
        }//

        public BaseResultDataValue _AddHREmployeeDataTable(DataTable dataTable, HRDept dept, Dictionary<string, string> dicColumn, IList<string> listPrimaryKey, Dictionary<string, string> dicDefaultValue)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string prodColumn = "-9999";
            Dictionary<string, HRDept> dicHRDept = new Dictionary<string, HRDept>();
            dicHRDept.Add(prodColumn, dept);
            baseResultDataValue = _AddHREmployeeData(dataTable, prodColumn, dicHRDept, dicColumn, listPrimaryKey, dicDefaultValue);
            return baseResultDataValue;
        }

        public BaseResultDataValue _AddHREmployeeDataTable(DataTable dataTable, Dictionary<string, string> dicColumn, IList<string> listPrimaryKey, Dictionary<string, string> dicDefaultValue)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            string deptColumn = "";
            if (dicColumn.Values.Contains("DeptNo"))
                deptColumn = dicColumn.FirstOrDefault(q => q.Value == "DeptNo").Key;
            if (string.IsNullOrWhiteSpace(deptColumn))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "人员配置文件没有配置人员部门编码信息！";
                ZhiFang.Common.Log.Log.Info(baseResultDataValue.ErrorInfo);
                return baseResultDataValue;
            }
            Dictionary<string, long> dicMain = new Dictionary<string, long>();
            Dictionary<string, HRDept> dicHRDept = new Dictionary<string, HRDept>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                dataRow["ExcelRowInputFlag"] = 0;
                dataRow["ExcelRowInputInfo"] = "导入成功";
                string deptNo = "";
                HRDept dept = null;
                if (dataRow[deptColumn] != null)
                {
                    deptNo = dataRow[deptColumn].ToString();
                    if (dicHRDept.Keys.Contains(deptNo))
                        continue;
                    IList<HRDept> listHRDept = IBHRDept.SearchListByHQL(" hrdept.UseCode=\'" + deptNo + "\'");
                    if (listHRDept != null && listHRDept.Count == 1)
                    {
                        dept = listHRDept[0];
                        dicHRDept.Add(deptNo, dept);
                    }
                    else if (listHRDept.Count > 1)
                    {
                        baseResultDataValue.ErrorInfo = "导入失败：根据部门编码【" + deptNo + "】找到多个部门信息！";
                        dataRow["ExcelRowInputFlag"] = -1;
                        dataRow["ExcelRowInputInfo"] = baseResultDataValue.ErrorInfo;
                        ZhiFang.Common.Log.Log.Info(baseResultDataValue.ErrorInfo);
                        continue;
                    }
                    else
                    {
                        baseResultDataValue.ErrorInfo = "导入失败：根据部门编码【" + deptNo + "】找不到对应的部门信息！";
                        dataRow["ExcelRowInputFlag"] = -1;
                        dataRow["ExcelRowInputInfo"] = baseResultDataValue.ErrorInfo;
                        ZhiFang.Common.Log.Log.Info(baseResultDataValue.ErrorInfo);
                        continue;
                    }
                }
                if (string.IsNullOrWhiteSpace(deptNo))
                {
                    baseResultDataValue.ErrorInfo = "导入失败：Excel中【" + deptColumn + "】列存在为空的信息，请补充完整！";
                    dataRow["ExcelRowInputFlag"] = -2;
                    dataRow["ExcelRowInputInfo"] = baseResultDataValue.ErrorInfo;
                    ZhiFang.Common.Log.Log.Info(baseResultDataValue.ErrorInfo);
                    continue;
                }
            }
            dicColumn.Remove(deptColumn);
            baseResultDataValue = _AddHREmployeeData(dataTable, deptColumn, dicHRDept, dicColumn, listPrimaryKey, dicDefaultValue);
            return baseResultDataValue;
        }
        public BaseResultDataValue _AddHREmployeeData(DataTable dataTable, string deptColumn, Dictionary<string, HRDept> dicHRDept, Dictionary<string, string> dicColumn, IList<string> listPrimaryKey, Dictionary<string, string> dicDefaultValue)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            int isExistCount = 0;
            int isErrorCount = 0;
            int isSuccCount = 0;
            Dictionary<string, long> dicMain = new Dictionary<string, long>();
            string UserAccountName = "";
            string SexName = "";
            IList<BSex> listSex = IBBSex.LoadAll();
            foreach (KeyValuePair<string, string> keyValuePair in dicColumn)
            {
                if (keyValuePair.Value == "UserAccount")
                {
                    UserAccountName = keyValuePair.Key;
                    break;
                }
            }
            foreach (KeyValuePair<string, string> keyValuePair in dicColumn)
            {
                if (keyValuePair.Value == "SexName")
                {
                    SexName = keyValuePair.Key;
                    break;
                }
            }
            foreach (DataRow dataRow in dataTable.Rows)
            {
                dataRow["ExcelRowInputFlag"] = 0;
                dataRow["ExcelRowInputInfo"] = "导入成功";
                HRDept dept = null;
                if (dicHRDept.Count == 1)
                    dept = dicHRDept.Values.First();
                else
                {
                    string deptNo = "";
                    if (dataRow[deptColumn] != null)
                        deptNo = dataRow[deptColumn].ToString();
                    if (dicHRDept.Keys.Contains(deptNo))
                        dept = dicHRDept[deptNo];
                    else
                        continue;
                }

                string keyValue = "";
                string keyHQL = "";

                foreach (string strKey in listPrimaryKey)
                {
                    if (!string.IsNullOrEmpty(dataRow[strKey].ToString()))
                    {
                        keyValue += "_" + dataRow[strKey].ToString().Trim();
                        if (string.IsNullOrEmpty(keyHQL))
                            keyHQL += " hremployee." + dicColumn[strKey] + "=\'" + dataRow[strKey].ToString().Trim() + "\'";
                        else
                            keyHQL += " and " + " hremployee." + dicColumn[strKey] + "=\'" + dataRow[strKey].ToString().Trim() + "\'";
                    }
                }
                if ((!string.IsNullOrEmpty(keyValue)) && (!string.IsNullOrEmpty(keyHQL)))
                {
                    if (dicMain.ContainsKey(keyValue))
                    {//导入的信息中存在主键列相同的记录
                        isSuccCount++;
                        continue;
                    }

                     keyHQL += " and " + " hremployee.HRDept.Id=" + dept.Id.ToString();

                    IList<HREmployee> listHREmployee = null;
                    dicMain.Add(keyValue, 0);
                    listHREmployee = this.SearchListByHQL(keyHQL);
                    if (listHREmployee != null && listHREmployee.Count > 0)
                    {
                        dicMain[keyValue] = listHREmployee[0].Id;
                        isExistCount++;
                        dataRow["ExcelRowInputFlag"] = 1;
                        dataRow["ExcelRowInputInfo"] = "未导入：该人员信息已经存在";
                        ZhiFang.Common.Log.Log.Info(string.Format("未导入：人员信息已经存在！人员名称为：{0} 编码为：{1}", listHREmployee[0].CName, listHREmployee[0].Shortcode));
                    }
                    else
                    {
                        HREmployee emp = ExcelDataCommon.AddExcelDataToDataBase<HREmployee>(dataRow, dicColumn, dicDefaultValue);
                        if (emp != null)
                        {
                            if (!string.IsNullOrEmpty(SexName) && dataRow.Table.Columns.Contains(SexName) && (!string.IsNullOrEmpty(dataRow[SexName].ToString())))
                            {
                                emp.BSex = IBBSex.GetSexByName(listSex, dataRow[SexName].ToString());
                            }
                            long labid = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.LabID));

                            emp.HRDept = dept;
                            emp.DataAddTime = DateTime.Now;
                            emp.DataUpdateTime = DateTime.Now;
                            //emp.PinYinZiTou = PinYinConverter.Get(emp.CName);
                            emp.IsUse = true;
                            emp.IsEnabled = 1;
                            emp.LabID = labid;
                            this.Entity = emp;
                            if (this.Add())
                            {
                                dicMain[keyValue] = emp.Id;
                                isSuccCount++;
                                //if (dataTable.Columns.Contains(UserAccountName))
                                //{

                                //    string userAccount = dataRow[UserAccountName] == null ? emp.PinYinZiTou : dataRow[UserAccountName].ToString();
                                //    IBRBACUser.AddRBACUser(emp, userAccount, "123456");
                                //}
                            }
                            else
                            {
                                isErrorCount++;
                                dataRow["ExcelRowInputFlag"] = -2;
                                dataRow["ExcelRowInputInfo"] = "导入失败：人员信息保存失败";
                                ZhiFang.Common.Log.Log.Info("导入失败：人员信息保存失败！");
                            }
                        }
                    }
                }
            }
            baseResultDataValue.ResultDataValue = string.Format("共需导入人员信息{0}条，其中：导入成功{1}条，导入失败{3}条，未导入{2}条！", dataTable.Rows.Count, isSuccCount, isExistCount, isErrorCount);
            return baseResultDataValue;
        }
        #endregion

        public BaseResultData AddHREmployeeSyncByInterface(string syncField, string syncFieldValue, HRDept dept,  Dictionary<string, object> dicFieldAndValue, bool isAddAccount)
        {
            BaseResultData baseresultdata = new BaseResultData();
            EntityList<HREmployee> listHREmployee = this.SearchListByHQL(syncField + "=\'" + syncFieldValue + "\'", 0, 0);
            bool isEdit = (listHREmployee != null && listHREmployee.count > 0);
            HREmployee emp = null;
            if (isEdit)
                emp = listHREmployee.list[0];
            else
                emp = new HREmployee();
            HRDept hrDept = null; 
            foreach (KeyValuePair<string, object> kv in dicFieldAndValue)
            {
                try
                {
                    if (kv.Key.ToUpper() == "MATCHDEPTCODE")
                    {
                        IList<HRDept> listHRDept = IBHRDept.SearchListByHQL(" hrdept.MatchCode=\'" + kv.Value + "\'");
                        if (listHRDept != null && listHRDept.Count > 0)
                        {
                            hrDept = listHRDept[0];
                        }
                    }
                    else
                    {
                        System.Reflection.PropertyInfo propertyInfo = emp.GetType().GetProperty(kv.Key);
                        if (propertyInfo != null && kv.Value != null)
                            propertyInfo.SetValue(emp, ExcelDataCommon.DataConvert(propertyInfo, kv.Value), null);
                    }
                }
                catch (Exception ex)
                {
                    ZhiFang.Common.Log.Log.Info("人员实体属性赋值失败：" + kv.Key + "---" + kv.Value.ToString() + "。 Error：" + ex.Message);
                }
            }
            emp.NameF = "";
            emp.NameL = "";
            if (hrDept != null)
                emp.HRDept = hrDept;
            if (emp.HRDept == null)
                emp.HRDept = dept;
            emp.IsUse = true;
            emp.IsEnabled = 1;
            this.Entity = emp;
            if (isEdit)
            {
                emp.DataUpdateTime = DateTime.Now;
                baseresultdata.success = this.Edit();
            }
            else
            {
                emp.DataAddTime = DateTime.Now;
                emp.DataUpdateTime = DateTime.Now;
                baseresultdata.success = this.Add();
                if (baseresultdata.success && isAddAccount)
                {
                    IBRBACUser.AddRBACUser(emp, emp.UseCode, emp.UseCode);
                }
            }

            return baseresultdata;
        }
    } 
}