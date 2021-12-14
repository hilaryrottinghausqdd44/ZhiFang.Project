using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ZhiFang.IDAO.LIIP.QMS;

namespace ZhiFang.DAO.MSSQL.QMS
{
     public class HRDeptEmpDao : IDHRDeptEmp
    {
        public SqlServerHelper DbHelperSQL = new SqlServerHelper("DBConnection_QMS");
        public DataSet GetDS(string strSql)
        {
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
        public DataSet GetAllList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from HR_DeptEmp");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetList(string wherestr)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from HR_DeptEmp");
            string sql = (wherestr == null || wherestr.Trim() == "" ? " 1=1 " : " " + wherestr + " ");
            strSql.Append(" where "+ sql);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public int GetTotalCount(string wherestr)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM HR_DeptEmp");
            if (wherestr != null && wherestr.Trim() != "")
            {
                strSql.Append(" where " + wherestr);
            }
            return Convert.ToInt32(DbHelperSQL.ExecuteScalar(strSql.ToString()));
        }

        public List<ZhiFang.Entity.RBAC.HRDeptEmp> GetAllObjectList()
        {
            DataSet ds = new DataSet();
            ds = GetAllList();
            IList<ZhiFang.Entity.RBAC.HRDeptEmp> deptlist = new List<Entity.RBAC.HRDeptEmp>();
            if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
            {
                return new List<Entity.RBAC.HRDeptEmp>();
            }
            deptlist = DataSetToEntityList<Entity.RBAC.HRDeptEmp>(ds, 0);            
            return deptlist.ToList<ZhiFang.Entity.RBAC.HRDeptEmp>();
        }

        /// <summary>
        /// DataSet转换为实体列表
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="p_DataSet">DataSet</param>
        /// <param name="p_TableIndex">待转换数据表索引</param>
        /// <returns>实体类列表</returns>
        public static IList<HRDeptEmp> DataSetToEntityList<HRDeptEmp>(DataSet p_DataSet, int p_TableIndex)
        {
            if (p_DataSet == null || p_DataSet.Tables.Count < 0)
                return default(IList<HRDeptEmp>);
            if (p_TableIndex > p_DataSet.Tables.Count - 1)
                return default(IList<HRDeptEmp>);
            if (p_TableIndex < 0)
                p_TableIndex = 0;
            if (p_DataSet.Tables[p_TableIndex].Rows.Count <= 0)
                return default(IList<HRDeptEmp>);

            DataTable p_Data = p_DataSet.Tables[p_TableIndex];
            // 返回值初始化
            IList<HRDeptEmp> result = new List<HRDeptEmp>();
            for (int j = 0; j < p_Data.Rows.Count; j++)
            {
                HRDeptEmp _t = (HRDeptEmp)Activator.CreateInstance(typeof(HRDeptEmp));
                PropertyInfo[] propertys = _t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    if (pi.Name == "Id" && p_Data.Rows[j]["DeptEmpID"] != DBNull.Value)
                    {
                        pi.SetValue(_t, p_Data.Rows[j]["DeptEmpID"], null);
                    }
                    else if (p_Data.Columns.IndexOf(pi.Name.ToUpper()) != -1 && p_Data.Rows[j][pi.Name.ToUpper()] != DBNull.Value)
                    {
                        pi.SetValue(_t, p_Data.Rows[j][pi.Name.ToUpper()], null);
                    } 
                    else if (pi.Name == "HRDept"  && p_Data.Rows[j]["DeptID"] != DBNull.Value) {
                        var hRDept= new Entity.RBAC.HRDept();
                        hRDept.Id = long.Parse(p_Data.Rows[j]["DeptID"].ToString());
                        pi.SetValue(_t, hRDept, null);
                    }
                    else if (pi.Name == "HREmployee"  && p_Data.Rows[j]["EmpID"] != DBNull.Value)
                    {
                        var hREmployee = new Entity.RBAC.HREmployee();
                        hREmployee.Id = long.Parse(p_Data.Rows[j]["EmpID"].ToString());
                        pi.SetValue(_t, hREmployee, null);
                    }
                    else
                    {
                        pi.SetValue(_t, null, null);
                    }
                }                
                result.Add(_t);
            } 
            return result;
        }   
    }
}
