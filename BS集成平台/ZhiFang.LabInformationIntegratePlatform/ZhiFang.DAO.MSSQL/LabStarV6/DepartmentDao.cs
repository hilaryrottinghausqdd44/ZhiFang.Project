using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhiFang.IDAO.LIIP;

namespace ZhiFang.DAO.MSSQL.LabStarV6
{
     public class DepartmentDao: IDepartmentClone
    {
        public SqlServerHelper DbHelperSQL = new SqlServerHelper("DBConnection_LabStarV6");
        public DataSet GetDS(string strSql)
        {
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
        public DataSet GetAllList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Department");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetList(string wherestr)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Department");
            string sql = (wherestr == null || wherestr.Trim() == "" ? " 1=1 " : " " + wherestr + " ");
            strSql.Append(" where "+ sql);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public int GetTotalCount(string wherestr)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM Department");
            if (wherestr != null && wherestr.Trim() != "")
            {
                strSql.Append(" where " + wherestr);
            }
            return Convert.ToInt32(DbHelperSQL.ExecuteScalar(strSql.ToString()));
        }

        public List<ZhiFang.Entity.RBAC.HRDept> GetAllObjectList()
        {
            DataSet ds = new DataSet();
            ds = GetAllList();
            List<ZhiFang.Entity.RBAC.HRDept> deptlist = new List<Entity.RBAC.HRDept>();
            if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
            {
                return new List<Entity.RBAC.HRDept>();
            }
            for (int i=0;i<ds.Tables[0].Rows.Count;i++)
            {
                Entity.RBAC.HRDept hrdept = TransObject(ds.Tables[0].Rows[i]);
                if (hrdept != null)
                    deptlist.Add(hrdept);
            }
            return deptlist;
        }

        public Entity.RBAC.HRDept TransObject(DataRow dr)
        {
            if (dr == null)
            {
                return null;
            }
            Entity.RBAC.HRDept dept = new Entity.RBAC.HRDept();
            if (dr.Table.Columns.Contains("DeptNo") && dr["DeptNo"] != null && dr["DeptNo"].ToString().Trim() != "")
            {
                dept.Id = long.Parse(dr["DeptNo"].ToString().Trim());
                //LIS
                dept.StandCode = dr["DeptNo"].ToString().Trim();
            }

            if (dr.Table.Columns.Contains("CName") && dr["CName"] != null && dr["CName"].ToString().Trim() != "")
            {
                dept.CName = dr["CName"].ToString().Trim();
            }

			//if (dr.Table.Columns.Contains("ShortName") && dr["ShortName"] != null && dr["ShortName"].ToString().Trim() != "")
			//{
			//    dept.Shortcode = dr["ShortName"].ToString().Trim();
			//}

			if (dr.Table.Columns.Contains("ShortName") && dr["ShortName"] != null && dr["ShortName"].ToString().Trim() != "")
			{
				dept.SName = dr["ShortName"].ToString().Trim();
			}

			if (dr.Table.Columns.Contains("ShortCode") && dr["ShortCode"] != null && dr["ShortCode"].ToString().Trim() != "")
            {
                dept.Shortcode = dr["ShortCode"].ToString().Trim();
            }

            if (dr.Table.Columns.Contains("Visible") && dr["Visible"] != null && dr["Visible"].ToString().Trim() != "")
            {
                dept.IsUse = dr["Visible"].ToString().Trim()=="1";
            }

            if (dr.Table.Columns.Contains("DisOrder") && dr["DisOrder"] != null && dr["DisOrder"].ToString().Trim() != "")
            {
                dept.DispOrder = int.Parse(dr["DisOrder"].ToString().Trim());
            }

            if (dr.Table.Columns.Contains("Code_1") && dr["Code_1"] != null && dr["Code_1"].ToString().Trim() != "")
            {
                //HIS
                dept.DeveCode = dr["Code_1"].ToString().Trim();
            }
            return dept;
        }

        public bool Add(Entity.RBAC.HRDept entity)
        {
            int count = GetTotalCount("")+1;
			entity.Id = count;

			StringBuilder strSql = new StringBuilder();
			StringBuilder strSql1 = new StringBuilder();
			StringBuilder strSql2 = new StringBuilder();			
			strSql1.Append("DeptNo,");
			strSql2.Append("" + entity.Id + ",");
			if (entity.CName != null)
			{
				strSql1.Append("CName,");
				strSql2.Append("'" + entity.CName + "',");
			}
			if (entity.SName != null)
			{
				strSql1.Append("ShortName,");
				strSql2.Append("'" + entity.SName + "',");
			}
			if (entity.Shortcode != null)
			{
				strSql1.Append("ShortCode,");
				strSql2.Append("'" + entity.Shortcode + "',");
			}
			if (entity.IsUse != null)
			{
				strSql1.Append("Visible,");
                int aa = 0;
                if (entity.IsUse) {
                    aa = 1;
                }                
                strSql2.Append("" + aa + ",");
			}
			if (entity.DispOrder != null)
			{
				strSql1.Append("DispOrder,");
				strSql2.Append("" + entity.DispOrder + ",");
			}
			if (entity.DeveCode != null)
			{
				strSql1.Append("HisOrderCode,");
				strSql2.Append("'" + entity.DeveCode + "',");
			}
			if (entity.DeveCode != null)
			{
				strSql1.Append("code_1,");
				strSql2.Append("'" + entity.DeveCode + "',");
			}
			strSql.Append("insert into Department(");
			strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
			strSql.Append(")");
			strSql.Append(" values (");
			strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
			strSql.Append(")");
			int rows = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
			return (rows > 0) ? true : false;
		}
    }
}
