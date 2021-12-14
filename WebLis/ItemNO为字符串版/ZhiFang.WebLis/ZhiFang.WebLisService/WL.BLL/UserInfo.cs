using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBUtility;
using System.Collections;
using System.Data;

using ECDS.Common;

namespace ZhiFang.WebLisService.WL.BLL
{
    public class UserInfo
    {



        #region 员工基本信息（从HR_Employees表取）


        /// <summary>
        /// 根据用户登录ID，取用户的信息，保存到哈希表
        /// 必须只满足一条记录才返回
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Hashtable GetUserInfoHashtable(string userName)
        {
            Hashtable returnValue = new Hashtable();
            //取用户信息
            DataSet ds = getUserInfo(userName);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count == 1)
                {
                    returnValue = DataConn.GetRowDataFromDataTable(ds.Tables[0], 0);
                }
            }
            return returnValue;
        }

        /// <summary>
        /// 根据用户登录ID取用户帐号信息
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public DataSet getUserInfo(string userName)
        {
            //用户表
            string tableName = "RBAC_Users";
            string sqlModal = "SELECT * FROM [{0}] WHERE [{0}].[Account]='{1}'";
            string sql = string.Format(sqlModal, tableName, userName.ToString());
            return DataConn.CreateDB().ExecDS(sql);
        }

        /// <summary>
        /// 根据员工编号，取员工的姓名
        /// 如果员工编号为0,则返回系统管理员
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public string getEmployeeNameFromEmployeeID(int employeeID)
        {
            //是系统管理员
            if (employeeID == 0)
            {
                return "系统管理员";
            }
            string employeeName = "";
            Hashtable hashEmployeeInfo = getEmployeeInfoHashtable(employeeID);
            if (hashEmployeeInfo.Count > 0)
            {
                //取到员工姓名
                employeeName = hashEmployeeInfo["NameL"].ToString() + hashEmployeeInfo["NameF"].ToString();
            }
            return employeeName;
        }

        /// <summary>
        /// 根据员工编号取员工基本信息
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public Hashtable getEmployeeInfoHashtable(int employeeID)
        {
            Hashtable returnValue = new Hashtable();
            //员工表
            DataSet ds = getEmployeeInfo(employeeID);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count == 1)
                {
                    returnValue = DataConn.GetRowDataFromDataTable(ds.Tables[0], 0);
                }
            }
            return returnValue;
        }


        /// <summary>
        /// 根据员工编号取员工基本信息
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public DataSet getEmployeeInfo(int employeeID)
        {
            //员工表
            string tableName = "HR_Employees";
            string sqlModal = "SELECT * FROM [{0}] WHERE [{0}].[ID]={1}";
            string sql = string.Format(sqlModal, tableName, employeeID.ToString());
            return DataConn.CreateDB().ExecDS(sql);
        }


        #endregion



        #region 用户登录信息（从RBAC_Users表取）


        /// <summary>
        /// 根据用户ID获取用户登录的账户名称
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string getUserNameFromUserID(string userID)
        {
            if (userID == "")
                return "";
            string userName = "";
            //用户表
            string tableName = "RBAC_Users";
            string sqlModal = "SELECT [Account] FROM [{0}] WHERE [{0}].[ID]={1}";
            string sql = string.Format(sqlModal, tableName, userID);
            DataSet ds = DataConn.CreateDB().ExecDS(sql);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count == 1)
                {
                    userName = ds.Tables[0].Rows[0][0].ToString();
                }
            }
            return userName;
        }



        /// <summary>
        /// 根据用户登录ID，取用户的信息，保存到哈希表
        /// 必须只满足一条记录才返回
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Hashtable getUserInfoHashtable(string userName)
        {
            Hashtable returnValue = new Hashtable();
            //取用户信息
            DataSet ds = getUserInfo(userName);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count == 1)
                {
                    returnValue = clsCommon.GetData.getRowDataFromDataTable(ds.Tables[0], 0);
                }
            }
            return returnValue;
        }



        /// <summary>
        /// 根据用户登录ID，取其对应的员工号
        /// 如果用户不存在，返回-1
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public int getUserEmployeeID(string userName)
        {
            int employeeID = -1;
            //用户信息
            Hashtable hashUser = getUserInfoHashtable(userName);
            if (hashUser["EmpID"] != null)
            {
                string empID = hashUser["EmpID"].ToString();
                employeeID = int.Parse(empID);
            }
            return employeeID;
        }




        /// <summary>
        /// 根据用户登录ID，用户密码。判断是否一致；Password
        /// 如果一致，则返回对应的员工号，否则返回-1
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public int getUserEmployeeID(string userName, string passWord)
        {
            int employeeID = -1;
            //用户信息
            Hashtable hashUser = getUserInfoHashtable(userName);
            if (hashUser["EmpID"] != null)
            {
                if (passWord == hashUser["Password"].ToString())//用户名和密码一致
                {
                    string empID = hashUser["EmpID"].ToString();
                    employeeID = int.Parse(empID);
                }
            }
            return employeeID;
        }





        #endregion



        #region 部门基本信息（从HR_Departments表取）


        /// <summary>
        /// 取所有的部门基本信息
        /// </summary>
        /// <returns></returns>
        public DataSet getDepartmentInfoALL()
        {
            //员工表
            string tableName = "HR_Departments";
            string sqlModal = "SELECT * FROM [{0}]";
            string sql = string.Format(sqlModal, tableName);
            return DataConn.CreateDB().ExecDS(sql);
            ;
        }


        /// <summary>
        /// 取所有的部门基本信息，返回数据格式：部门编号、部门名称
        /// 其值都是字符串
        /// </summary>
        /// <returns></returns>
        public Hashtable getDepartmentIDDepartmentNameALL()
        {
            Hashtable returnValue = new Hashtable();
            //取所有的部门信息
            DataSet ds = getDepartmentInfoALL();
            if (ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Hashtable hashRow = clsCommon.GetData.getRowDataFromDataTable(ds.Tables[0], i);
                    if (hashRow["ID"] != null)
                    {
                        string deptID = hashRow["ID"].ToString();
                        if (hashRow["CName"] != null)
                        {
                            string deptName = hashRow["CName"].ToString();
                            if (returnValue[deptID] == null)
                                returnValue.Add(deptID, deptName);
                        }
                    }
                }
            }
            return returnValue;
        }


        /// <summary>
        /// 根据部门编号，取部门基本信息
        /// </summary>
        /// <param name="deptID">部门编号</param>
        /// <returns></returns>
        public DataSet getDepartmentInfo(int deptID)
        {
            //员工表
            string tableName = "HR_Departments";
            string sqlModal = "SELECT * FROM [{0}] WHERE [{0}].[ID]={1}";
            string sql = string.Format(sqlModal, tableName, deptID.ToString());
            return DataConn.CreateDB().ExecDS(sql);

        }




        /// <summary>
        /// 根据部门编号，取部门基本信息
        /// 返回哈希表
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public Hashtable getDepartmentInfoHashtable(int deptID)
        {
            Hashtable returnValue = new Hashtable();
            //员工表
            DataSet ds = getDepartmentInfo(deptID);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count == 1)
                {
                    returnValue = clsCommon.GetData.getRowDataFromDataTable(ds.Tables[0], 0);
                }
            }
            return returnValue;
        }



        /// <summary>
        /// 根据部门编号，取部门名称
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public string getDepartmentNameFromDepartmentID(string deptID)
        {
            string returnValue = "";
            //取所有的部门编号、部门名称
            Hashtable hashDept = getDepartmentIDDepartmentNameALL();
            if (hashDept[deptID] != null)
                returnValue = hashDept[deptID].ToString();
            return returnValue;
        }



        #endregion


        #region 员工权限基本信息（从RBAC_EmplRoles表取）

        /// <summary>
        /// 从RBAC_EmplRoles表，取某个员工的权限分配信息（如所在部门等）
        /// 返回多条记录
        /// </summary>
        /// <param name="employeeID">员工编号</param>
        /// <returns></returns>
        public DataSet getEmployeeRole(int employeeID)
        {
            string tableName = "RBAC_EmplRoles";
            string sqlModal = "SELECT * FROM [{0}] WHERE [{0}].[EmplID]={1}";
            string sql = string.Format(sqlModal, tableName, employeeID);
            return DataConn.CreateDB().ExecDS(sql);
        }



        /// <summary>
        /// 获取员工所在的部门，返回哈希表：部门编号、部门名称
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public Hashtable getEmployeeDepartmentIDDepartmentNameHashtable(int employeeID)
        {
            //取员工权限信息
            DataSet ds = getEmployeeRole(employeeID);
            //部门
            Hashtable hashDept = new Hashtable();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                if (!Convert.IsDBNull(dr["DeptID"]))
                {
                    string deptID = dr["DeptID"].ToString();
                    string deptName = getDepartmentNameFromDepartmentID(deptID);
                    if (hashDept[deptID] == null)
                        hashDept.Add(deptID, deptName);
                }
            }
            return hashDept;
        }


        /// <summary>
        /// 获取员工所在的部门，返回字符串（部门与部门之间用逗号分隔）
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public string getEmployeeDepartmentName(int employeeID)
        {
            string returnValue = "";
            //部门编号、部门名称
            Hashtable hashDept = getEmployeeDepartmentIDDepartmentNameHashtable(employeeID);
            System.Collections.IDictionaryEnumerator myEnumerator = hashDept.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                string deptName = myEnumerator.Value.ToString();
                if (deptName != "")
                {
                    if (returnValue != "")
                        returnValue += ",";
                    returnValue += deptName;
                }
            }
            return returnValue;
        }



        /// <summary>
        /// 获取员工所在的部门，返回字符串（部门与部门之间用逗号分隔）
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public string getEmployeeDepartmentID(int employeeID)
        {
            string returnValue = "";
            //部门编号、部门名称
            Hashtable hashDept = getEmployeeDepartmentIDDepartmentNameHashtable(employeeID);
            System.Collections.IDictionaryEnumerator myEnumerator = hashDept.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                string deptID = myEnumerator.Key.ToString();
                if (deptID != "")
                {
                    if (returnValue != "")
                        returnValue += ",";
                    returnValue += deptID;
                }
            }
            return returnValue;
        }



        /// <summary>
        /// 获取员工所在的部门和职位
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public Hashtable getEmployeeDepartmentPosition(int employeeID)
        {
            //取员工的权限信息
            DataSet ds = getEmployeeRole(employeeID);
            //部门职位
            Hashtable hashDeptPosiID = new Hashtable();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                if (!Convert.IsDBNull(dr["DeptID"]))
                {
                    string deptID = dr["DeptID"].ToString();

                    if (!Convert.IsDBNull(dr["PositionID"]))
                        deptID += "," + dr["PositionID"];
                    if (hashDeptPosiID[deptID] == null)
                        hashDeptPosiID.Add(deptID, deptID);
                }
            }
            return hashDeptPosiID;
        }



        /// <summary>
        /// 获取员工所在的部门
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public Hashtable getEmployeeDepartment(int employeeID)
        {
            //取员工的权限信息
            DataSet ds = getEmployeeRole(employeeID);
            //部门职位
            Hashtable hashDeptID = new Hashtable();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                if (!Convert.IsDBNull(dr["DeptID"]))
                {
                    string deptID = dr["DeptID"].ToString();
                    if (hashDeptID[deptID] == null)
                        hashDeptID.Add(deptID, deptID);
                }
            }
            return hashDeptID;
        }



        /// <summary>
        /// 获取员工的职位
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public Hashtable getEmployeePosition(int employeeID)
        {
            //取员工的权限信息
            DataSet ds = getEmployeeRole(employeeID);
            //部门职位
            Hashtable hashPosiID = new Hashtable();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (!Convert.IsDBNull(dr["PositionID"]))
                {
                    string posiID = dr["PositionID"].ToString();
                    if (hashPosiID[posiID] == null)
                        hashPosiID.Add(posiID, posiID);
                }
            }
            return hashPosiID;
        }





        /// <summary>
        /// 获取员工的职位
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public Hashtable getEmployeeDuty(int employeeID)
        {
            //取员工的权限信息
            DataSet ds = getEmployeeRole(employeeID);
            //部门职位
            Hashtable hashPostID = new Hashtable();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (!Convert.IsDBNull(dr["PostID"]))
                {
                    string postID = dr["PostID"].ToString();
                    if (hashPostID[postID] == null)
                        hashPostID.Add(postID, postID);
                }
            }
            return hashPostID;
        }


        #endregion



        /// <summary>
        /// 根据员工登录帐号编号，取员工的姓名
        /// 可以供默认值函数：“登录者姓名()” 调用
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public string getEmployeeNameFromUserName(string userName)
        {
            int employeeID = getUserEmployeeID(userName);
            return getEmployeeNameFromEmployeeID(employeeID);
        }



        /// <summary>
        /// 根据员工登录帐号编号，取员工的所在的部门名称
        /// 可以供默认值函数：“登录者部门()” 调用
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public string getEmployeeDepartmentNameFromUserName(string userName)
        {
            int employeeID = getUserEmployeeID(userName);
            return getEmployeeDepartmentName(employeeID);
        }






    }
}
