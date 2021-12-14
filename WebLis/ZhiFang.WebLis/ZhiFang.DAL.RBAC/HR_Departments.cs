using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
namespace ZhiFang.DAL.RBAC
{
    //HR_Departments
    public class HR_Departments : BaseDALLisDB
    {

        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from HR_Departments");
            strSql.Append(" where ID=" + ID + " ");
            return DbHelperSQL.Exists(strSql.ToString());
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.RBAC.Entity.HR_Departments model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.SN != null)
            {
                strSql1.Append("SN,");
                strSql2.Append("'" + model.SN + "',");
            }
            if (model.CName != null)
            {
                strSql1.Append("CName,");
                strSql2.Append("'" + model.CName + "',");
            }
            if (model.EName != null)
            {
                strSql1.Append("EName,");
                strSql2.Append("'" + model.EName + "',");
            }
            if (model.SName != null)
            {
                strSql1.Append("SName,");
                strSql2.Append("'" + model.SName + "',");
            }
            if (model.Descr != null)
            {
                strSql1.Append("Descr,");
                strSql2.Append("'" + model.Descr + "',");
            }
            if (model.Tel != null)
            {
                strSql1.Append("Tel,");
                strSql2.Append("'" + model.Tel + "',");
            }
            if (model.Fax != null)
            {
                strSql1.Append("Fax,");
                strSql2.Append("'" + model.Fax + "',");
            }
            if (model.Zip != null)
            {
                strSql1.Append("Zip,");
                strSql2.Append("'" + model.Zip + "',");
            }
            if (model.Address != null)
            {
                strSql1.Append("Address,");
                strSql2.Append("'" + model.Address + "',");
            }
            if (model.Contact != null)
            {
                strSql1.Append("Contact,");
                strSql2.Append("'" + model.Contact + "',");
            }
            if (model.DeptDesktopID != null)
            {
                strSql1.Append("DeptDesktopID,");
                strSql2.Append("" + model.DeptDesktopID + ",");
            }
            if (model.DeptDesktopName != null)
            {
                strSql1.Append("DeptDesktopName,");
                strSql2.Append("'" + model.DeptDesktopName + "',");
            }
            if (model.ParentOrg != null)
            {
                strSql1.Append("ParentOrg,");
                strSql2.Append("'" + model.ParentOrg + "',");
            }
            if (model.orgType != null)
            {
                strSql1.Append("orgType,");
                strSql2.Append("'" + model.orgType + "',");
            }
            if (model.OrgCode != null)
            {
                strSql1.Append("OrgCode,");
                strSql2.Append("'" + model.OrgCode + "',");
            }
            if (model.RelationName != null)
            {
                strSql1.Append("RelationName,");
                strSql2.Append("'" + model.RelationName + "',");
            }
            strSql.Append("insert into HR_Departments(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            strSql.Append(";select @@IDENTITY");
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.RBAC.Entity.HR_Departments model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update HR_Departments set ");
            if (model.SN != null)
            {
                strSql.Append("SN='" + model.SN + "',");
            }
            if (model.CName != null)
            {
                strSql.Append("CName='" + model.CName + "',");
            }
            if (model.EName != null)
            {
                strSql.Append("EName='" + model.EName + "',");
            }
            else
            {
                strSql.Append("EName= null ,");
            }
            if (model.SName != null)
            {
                strSql.Append("SName='" + model.SName + "',");
            }
            else
            {
                strSql.Append("SName= null ,");
            }
            if (model.Descr != null)
            {
                strSql.Append("Descr='" + model.Descr + "',");
            }
            else
            {
                strSql.Append("Descr= null ,");
            }
            if (model.Tel != null)
            {
                strSql.Append("Tel='" + model.Tel + "',");
            }
            else
            {
                strSql.Append("Tel= null ,");
            }
            if (model.Fax != null)
            {
                strSql.Append("Fax='" + model.Fax + "',");
            }
            else
            {
                strSql.Append("Fax= null ,");
            }
            if (model.Zip != null)
            {
                strSql.Append("Zip='" + model.Zip + "',");
            }
            else
            {
                strSql.Append("Zip= null ,");
            }
            if (model.Address != null)
            {
                strSql.Append("Address='" + model.Address + "',");
            }
            else
            {
                strSql.Append("Address= null ,");
            }
            if (model.Contact != null)
            {
                strSql.Append("Contact='" + model.Contact + "',");
            }
            else
            {
                strSql.Append("Contact= null ,");
            }
            if (model.DeptDesktopID != null)
            {
                strSql.Append("DeptDesktopID=" + model.DeptDesktopID + ",");
            }
            else
            {
                strSql.Append("DeptDesktopID= null ,");
            }
            if (model.DeptDesktopName != null)
            {
                strSql.Append("DeptDesktopName='" + model.DeptDesktopName + "',");
            }
            else
            {
                strSql.Append("DeptDesktopName= null ,");
            }
            if (model.ParentOrg != null)
            {
                strSql.Append("ParentOrg='" + model.ParentOrg + "',");
            }
            else
            {
                strSql.Append("ParentOrg= null ,");
            }
            if (model.orgType != null)
            {
                strSql.Append("orgType='" + model.orgType + "',");
            }
            if (model.OrgCode != null)
            {
                strSql.Append("OrgCode='" + model.OrgCode + "',");
            }
            else
            {
                strSql.Append("OrgCode= null ,");
            }
            if (model.RelationName != null)
            {
                strSql.Append("RelationName='" + model.RelationName + "',");
            }
            else
            {
                strSql.Append("RelationName= null ,");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where ID=" + model.ID + "");
            int rowsAffected = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            return rowsAffected;
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from HR_Departments ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)
            };
            parameters[0].Value = ID;


            int rows = DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from HR_Departments ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
            int rows = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.RBAC.Entity.HR_Departments GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(" ID,SN,CName,EName,SName,Descr,Tel,Fax,Zip,Address,Contact,DeptDesktopID,DeptDesktopName,ParentOrg,orgType,OrgCode,RelationName ");
            strSql.Append(" from HR_Departments ");
            strSql.Append(" where ID=" + ID + "");
            ZhiFang.Model.RBAC.Entity.HR_Departments model = new ZhiFang.Model.RBAC.Entity.HR_Departments();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZhiFang.Model.RBAC.Entity.HR_Departments DataRowToModel(DataRow row)
        {
            ZhiFang.Model.RBAC.Entity.HR_Departments model = new ZhiFang.Model.RBAC.Entity.HR_Departments();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["SN"] != null)
                {
                    model.SN = row["SN"].ToString();
                }
                if (row["CName"] != null)
                {
                    model.CName = row["CName"].ToString();
                }
                if (row["EName"] != null)
                {
                    model.EName = row["EName"].ToString();
                }
                if (row["SName"] != null)
                {
                    model.SName = row["SName"].ToString();
                }
                if (row["Descr"] != null)
                {
                    model.Descr = row["Descr"].ToString();
                }
                if (row["Tel"] != null)
                {
                    model.Tel = row["Tel"].ToString();
                }
                if (row["Fax"] != null)
                {
                    model.Fax = row["Fax"].ToString();
                }
                if (row["Zip"] != null)
                {
                    model.Zip = row["Zip"].ToString();
                }
                if (row["Address"] != null)
                {
                    model.Address = row["Address"].ToString();
                }
                if (row["Contact"] != null)
                {
                    model.Contact = row["Contact"].ToString();
                }
                if (row["DeptDesktopID"] != null && row["DeptDesktopID"].ToString() != "")
                {
                    model.DeptDesktopID = int.Parse(row["DeptDesktopID"].ToString());
                }
                if (row["DeptDesktopName"] != null)
                {
                    model.DeptDesktopName = row["DeptDesktopName"].ToString();
                }
                if (row["ParentOrg"] != null)
                {
                    model.ParentOrg = row["ParentOrg"].ToString();
                }
                if (row["orgType"] != null)
                {
                    model.orgType = row["orgType"].ToString();
                }
                if (row["OrgCode"] != null)
                {
                    model.OrgCode = row["OrgCode"].ToString();
                }
                if (row["RelationName"] != null)
                {
                    model.RelationName = row["RelationName"].ToString();
                }
            }
            return model;
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM HR_Departments ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM HR_Departments ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM HR_Departments ");
            if (strWhere!=null && strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int page, int rows)
        {
            if (page <= 0)
                page = 1;
            if (rows <= 0)
                rows = 1;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (orderby!=null && orderby.Trim()!="")
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from HR_Departments T ");
            if (strWhere != null && strWhere.Trim() != "")
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", (page-1)*rows, page * rows);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
    }
}

