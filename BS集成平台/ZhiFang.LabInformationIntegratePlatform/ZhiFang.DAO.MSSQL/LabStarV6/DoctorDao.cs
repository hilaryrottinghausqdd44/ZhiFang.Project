using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhiFang.Entity.RBAC;
using ZhiFang.IDAO.LIIP;

namespace ZhiFang.DAO.MSSQL.LabStarV6
{
     public class DoctorDao: IDEmpClone
    {
        public SqlServerHelper DbHelperSQL = new SqlServerHelper("DBConnection_LabStarV6");
        public DataSet GetDS(string strSql)
        {
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
        public DataSet GetAllList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Doctor");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetList(string wherestr)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Doctor");
            string sql = (wherestr == null || wherestr.Trim() == "" ? " 1=1 " : " " + wherestr + " ");
            strSql.Append(" where "+ sql);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public int GetTotalCount(string wherestr)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM Doctor");
            return Convert.ToInt32(DbHelperSQL.ExecuteScalar(strSql.ToString()));
        }

        public List<HREmployee> GetAllEmpList()
        {
            DataSet ds = new DataSet();
            ds = GetAllList();
            List<ZhiFang.Entity.RBAC.HREmployee> emplist = new List<Entity.RBAC.HREmployee>();
            if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
            {
                return new List<Entity.RBAC.HREmployee>();
            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                HREmployee hremp = TransObject(ds.Tables[0].Rows[i]);
                if (hremp != null)
                    emplist.Add(hremp);
            }
            return emplist;
        }

        public Entity.RBAC.HREmployee TransObject(DataRow dr)
        {
            if (dr == null)
            {
                return null;
            }
            Entity.RBAC.HREmployee emp = new Entity.RBAC.HREmployee();
            if (dr.Table.Columns.Contains("DoctorNo") && dr["DoctorNo"] != null && dr["DoctorNo"].ToString().Trim() != "")
            {
                string tmpid = dr["DoctorNo"].ToString().Trim();
                List<string> a = new List<string>() { "0", "00", "000", "0000" };
                tmpid = (tmpid.Length <= 4)?a[4 - tmpid.Length] + tmpid: tmpid;
                emp.Id = long.Parse(ZhiFang.Entity.LIIP.ZFSystemRole.智方_系统角色_医生.Key + tmpid);
                //LIS编码
                emp.StandCode = Entity.LIIP.ZFSystemRole.智方_系统角色_医生.Key + "_" + dr["DoctorNo"].ToString().Trim();
            }

            if (dr.Table.Columns.Contains("CName") && dr["CName"] != null && dr["CName"].ToString().Trim() != "")
            {
                emp.CName = dr["CName"].ToString().Trim();
                emp.NameF= dr["CName"].ToString().Trim();
                emp.NameL = "";
            }

            if (dr.Table.Columns.Contains("ShortCode") && dr["ShortCode"] != null && dr["ShortCode"].ToString().Trim() != "")
            {
                emp.Shortcode = dr["ShortCode"].ToString().Trim();
            }

            if (dr.Table.Columns.Contains("Visible") && dr["Visible"] != null && dr["Visible"].ToString().Trim() != "")
            {
                emp.IsUse = dr["Visible"].ToString().Trim()=="1";
            }

            if (dr.Table.Columns.Contains("DisOrder") && dr["DisOrder"] != null && dr["DisOrder"].ToString().Trim() != "")
            {
                emp.DispOrder = int.Parse(dr["DisOrder"].ToString().Trim());
            }

            if (dr.Table.Columns.Contains("Code_1") && dr["Code_1"] != null && dr["Code_1"].ToString().Trim() != "")
            {
                //HIS编码
                emp.DeveCode = dr["Code_1"].ToString().Trim();
            }

            if (dr.Table.Columns.Contains("doctorphonecode") && dr["doctorphonecode"] != null && dr["doctorphonecode"].ToString().Trim() != "")
            {
                emp.Tel = dr["doctorphonecode"].ToString().Trim();
            }
            return emp;
        }

        public bool Add(Entity.RBAC.HREmployee entity)
        {
            int count = GetTotalCount("") + 1;
            entity.Id = count;

            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            strSql1.Append("DoctorNo,");
            strSql2.Append("" + entity.Id + ",");
            if (entity.CName != null)
            {
                strSql1.Append("CName,");
                if (entity.CName.Length > 5)
                {
                    strSql2.Append("'" + entity.CName.Substring(0, 5) + "',");
                }
                else 
                {
                    strSql2.Append("'" + entity.CName + "',");
                }    
            }
            if (entity.Shortcode != null)
            {
                strSql1.Append("ShortCode,");
                if (entity.Shortcode.Length > 10)
                {
                    strSql2.Append("'" + entity.Shortcode.Substring(0, 9) + "',");
                }
                else
                {
                    strSql2.Append("'" + entity.Shortcode + "',");
                }
            }
            if (entity.IsUse != null)
            {
                strSql1.Append("Visible,");
                int aa = 0;
                if (entity.IsUse.Value)
                {
                    aa = 1;
                }
                strSql2.Append("" + aa + ",");
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
            if (entity.DeveCode != null)
            {
                strSql1.Append("code_2,");
                strSql2.Append("'" + entity.DeveCode + "',");
            }
            if (entity.DeveCode != null)
            {
                strSql1.Append("code_3,");
                strSql2.Append("'" + entity.DeveCode + "',");
            }
            if (entity.DeveCode != null)
            {
                strSql1.Append("doctorphonecode,");
                strSql2.Append("'" + entity.Tel + "',");
            }
            strSql.Append("insert into Doctor(");
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
