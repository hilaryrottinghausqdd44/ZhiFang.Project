using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.Model.RBAC.Entity;
namespace ZhiFang.DAL.RBAC
{
    //RBAC_EmplRoles
    public class RBAC_EmplRoles : BaseDALLisDB
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.RBAC.Entity.RBAC_EmplRoles model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.SN != null)
            {
                strSql1.Append("SN,");
                strSql2.Append("" + model.SN + ",");
            }
            if (model.EmplID != null)
            {
                strSql1.Append("EmplID,");
                strSql2.Append("" + model.EmplID + ",");
            }
            if (model.DeptID != null)
            {
                strSql1.Append("DeptID,");
                strSql2.Append("" + model.DeptID + ",");
            }
            if (model.PositionID != null)
            {
                strSql1.Append("PositionID,");
                strSql2.Append("" + model.PositionID + ",");
            }
            if (model.PostID != null)
            {
                strSql1.Append("PostID,");
                strSql2.Append("" + model.PostID + ",");
            }
            if (model.Sort != null)
            {
                strSql1.Append("Sort,");
                strSql2.Append("" + model.Sort + ",");
            }
            if (model.Validity != null)
            {
                strSql1.Append("Validity,");
                strSql2.Append("'" + model.Validity + "',");
            }
            strSql.Append("insert into RBAC_EmplRoles(");
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
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from RBAC_EmplRoles ");
            strSql.Append(" where ID=" + ID + "");
            int rowsAffected = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="EmplID">人员ID</param>
        /// <param name="flag">0部门1角色</param>
        /// <returns></returns>
		public bool DeleteByEmpID(int EmplID,int flag)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from RBAC_EmplRoles ");
            strSql.Append(" where EmplID= "+ EmplID);
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
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM RBAC_EmplRoles ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM RBAC_EmplRoles ");
            if (strWhere.Trim() != "")
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
    }
}

