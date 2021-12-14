using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhiFang.Common.Public;
using ZhiFang.Entity.RBAC;
using ZhiFang.IDAO.LIIP;

namespace ZhiFang.DAO.MSSQL.LabStarV6
{
     public class NPUserDao: IDEmpClone
    {
        public SqlServerHelper DbHelperSQL = new SqlServerHelper("DBConnection_LabStarV6");
        public DataSet GetDS(string strSql)
        {
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
        public DataSet GetAllList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from NPUser");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetList(string wherestr)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from NPUser");
            string sql = (wherestr == null || wherestr.Trim() == "" ? " 1=1 " : " " + wherestr + " ");
            strSql.Append(" where "+ sql);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public int GetTotalCount(string wherestr)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM NPUser");
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
            if (dr.Table.Columns.Contains("NPUserNo") && dr["NPUserNo"] != null && dr["NPUserNo"].ToString().Trim() != "")
            {
                if (long.Parse(dr["NPUserNo"].ToString().Trim()) <= 0)
                    return null;
                string tmpid = dr["NPUserNo"].ToString().Trim();
                List<string> a = new List<string>() { "0", "00", "000", "0000" };
                tmpid = (tmpid.Length <= 4) ? a[4 - tmpid.Length] + tmpid : tmpid;
                emp.Id = long.Parse(ZhiFang.Entity.LIIP.ZFSystemRole.智方_系统角色_物流护工.Key + tmpid);
                //开发商编码
                emp.StandCode = ZhiFang.Entity.LIIP.ZFSystemRole.智方_系统角色_物流护工.Key + "_" + dr["NPUserNo"].ToString().Trim();
            }

            if (dr.Table.Columns.Contains("CName") && dr["CName"] != null && dr["CName"].ToString().Trim() != "")
            {
                emp.CName = dr["CName"].ToString().Trim();
                emp.NameF = dr["CName"].ToString().Trim();
                emp.NameL = "";
            }

            if (dr.Table.Columns.Contains("ShortCode") && dr["ShortCode"] != null && dr["ShortCode"].ToString().Trim() != "")
            {
                emp.Shortcode = dr["ShortCode"].ToString().Trim();
            }

            if (dr.Table.Columns.Contains("Role") && dr["Role"] != null && dr["Role"].ToString().Trim() != "")
            {
                emp.Comment = dr["Role"].ToString().Trim();
            }
            else
            {
                if (dr.Table.Columns.Contains("usertype") && dr["usertype"] != null && dr["usertype"].ToString().Trim() != "")
                {
                    emp.Comment = dr["usertype"].ToString().Trim();
                }
            }

            
            if (dr.Table.Columns.Contains("Visible") && dr["Visible"] != null && dr["Visible"].ToString().Trim() != "")
            {
                emp.IsUse = dr["Visible"].ToString().Trim() == "1";
                emp.IsEnabled = int.Parse(dr["Visible"].ToString().Trim());
            }

            if (dr.Table.Columns.Contains("DisOrder") && dr["DisOrder"] != null && dr["DisOrder"].ToString().Trim() != "")
            {
                emp.DispOrder = int.Parse(dr["DisOrder"].ToString().Trim());
            }

            if (dr.Table.Columns.Contains("HISOrderCode") && dr["HISOrderCode"] != null && dr["HISOrderCode"].ToString().Trim() != "")
            {
                //HIS编码,一般没有HIS编码
                emp.DeveCode = dr["HISOrderCode"].ToString().Trim();
            }

            emp.RBACUserList = new List<RBACUser>();

            RBACUser tmpuser = new RBACUser();
            tmpuser.Account = emp.Shortcode;
            tmpuser.CName = emp.CName;
            tmpuser.UseCode = emp.UseCode;
            tmpuser.PWD = SecurityHelp.MD5Encrypt(LIIP.Common.PUserPWDHelp.UnCovertPassWord(dr["Password"].ToString().Trim()), SecurityHelp.PWDMD5Key);
            tmpuser.DispOrder = emp.DispOrder;
            tmpuser.IsUse = emp.IsUse;
            tmpuser.Shortcode = emp.Shortcode;
            tmpuser.StandCode = emp.StandCode;
            tmpuser.AccLock = false;
            tmpuser.HREmployee = emp;
            emp.RBACUserList.Add(tmpuser);

            return emp;
        }

        public bool Add(HREmployee entity)
        {
            int count = GetTotalCount("") + 1;
            entity.Id = count;

            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (entity.Id != null)
            {
                strSql1.Append("NPUserNo,");
                strSql2.Append("" + entity.Id + ",");
            }
            if (entity.CName != null)
            {
                strSql1.Append("CName,");
                strSql2.Append("'" + entity.CName + "',");
            }
            if (entity.RBACUserList[0].Shortcode != null && entity.RBACUserList[0].Shortcode != "")
            {
                strSql1.Append("Shortcode,");
                strSql2.Append("'" + entity.RBACUserList[0].Shortcode + "',");
            }
            if (entity.Comment != null)
            {
                strSql1.Append("Role,");
                strSql2.Append("'" + entity.Comment + "',");
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
            if (entity.DispOrder != null)
            {
                strSql1.Append("DispOrder,");
                strSql2.Append("'" + entity.DispOrder + "',");
            }
            strSql.Append("insert into NPuser(");
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
