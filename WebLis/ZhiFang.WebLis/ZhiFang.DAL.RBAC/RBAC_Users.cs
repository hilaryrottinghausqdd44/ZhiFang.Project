using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
namespace ZhiFang.DAL.RBAC
{
    //RBAC_Users
    public class RBAC_Users : BaseDALLisDB
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.RBAC.Entity.RBAC_Users model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.Account != null)
            {
                strSql1.Append("Account,");
                strSql2.Append("'" + model.Account + "',");
            }
            if (model.Password != null)
            {
                strSql1.Append("Password,");
                strSql2.Append("'" + model.Password + "',");
            }
            if (model.EmpID != null)
            {
                strSql1.Append("EmpID,");
                strSql2.Append("" + model.EmpID + ",");
            }
            if (model.UserDesc != null)
            {
                strSql1.Append("UserDesc,");
                strSql2.Append("'" + model.UserDesc + "',");
            }
            if (model.EnMPwd != null)
            {
                strSql1.Append("EnMPwd,");
                strSql2.Append("" + (model.EnMPwd ? 1 : 0) + ",");
            }
            if (model.PwdExprd != null)
            {
                strSql1.Append("PwdExprd,");
                strSql2.Append("" + (model.PwdExprd ? 1 : 0) + ",");
            }
            if (model.AccExprd != null)
            {
                strSql1.Append("AccExprd,");
                strSql2.Append("" + (model.AccExprd ? 1 : 0) + ",");
            }
            if (model.AccLock != null)
            {
                strSql1.Append("AccLock,");
                strSql2.Append("" + (model.AccLock ? 1 : 0) + ",");
            }
            if (model.LockedPeriod != null)
            {
                strSql1.Append("LockedPeriod,");
                strSql2.Append("" + model.LockedPeriod + ",");
            }
            if (model.AuUnlock != null)
            {
                strSql1.Append("AuUnlock,");
                strSql2.Append("" + model.AuUnlock + ",");
            }
            if (model.AccLockDt != null)
            {
                strSql1.Append("AccLockDt,");
                strSql2.Append("'" + model.AccLockDt.Value.ToString("yyyy-MM-dd HH:mm:ss") + "',");
            }
            if (model.LoginTm != null)
            {
                strSql1.Append("LoginTm,");
                strSql2.Append("'" + model.LoginTm.Value.ToString("yyyy-MM-dd HH:mm:ss") + "',");
            }
            if (model.AccExpTm != null)
            {
                strSql1.Append("AccExpTm,");
                strSql2.Append("'" + model.AccExpTm.Value.ToString("yyyy-MM-dd HH:mm:ss") + "',");
            }
            if (model.AccCreateTime != null)
            {
                strSql1.Append("AccCreateTime,");
                strSql2.Append("'" + model.AccCreateTime.Value.ToString("yyyy-MM-dd HH:mm:ss") + "',");
            }
            strSql.Append("insert into RBAC_Users(");
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
        public bool Update(ZhiFang.Model.RBAC.Entity.RBAC_Users model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update RBAC_Users set ");
            if (model.Password != null)
            {
                strSql.Append("Password='" + model.Password + "',");
            }
            else
            {
                strSql.Append("Password= null ,");
            }
            if (model.UserDesc != null)
            {
                strSql.Append("UserDesc='" + model.UserDesc + "',");
            }
            else
            {
                strSql.Append("UserDesc= null ,");
            }
            if (model.EnMPwd != null)
            {
                strSql.Append("EnMPwd=" + (model.EnMPwd ? 1 : 0) + ",");
            }
            if (model.PwdExprd != null)
            {
                strSql.Append("PwdExprd=" + (model.PwdExprd ? 1 : 0) + ",");
            }
            if (model.AccExprd != null)
            {
                strSql.Append("AccExprd=" + (model.AccExprd ? 1 : 0) + ",");
            }
            else
            {
                strSql.Append("AccExprd= null ,");
            }
            if (model.AccLock != null)
            {
                strSql.Append("AccLock=" + (model.AccLock ? 1 : 0) + ",");
            }
            if (model.LockedPeriod != null)
            {
                strSql.Append("LockedPeriod=" + model.LockedPeriod + ",");
            }
            else
            {
                strSql.Append("LockedPeriod= null ,");
            }
            if (model.AuUnlock != null)
            {
                strSql.Append("AuUnlock=" + model.AuUnlock + ",");
            }
            else
            {
                strSql.Append("AuUnlock= null ,");
            }
            if (model.AccLockDt != null)
            {
                strSql.Append("AccLockDt='" + model.AccLockDt.Value.ToString("yyyy-MM-dd HH:mm:ss") + "',");
            }
            else
            {
                strSql.Append("AccLockDt= null ,");
            }
            if (model.LoginTm != null)
            {
                strSql.Append("LoginTm='" + model.LoginTm.Value.ToString("yyyy-MM-dd HH:mm:ss") + "',");
            }
            else
            {
                strSql.Append("LoginTm= null ,");
            }
            if (model.AccExpTm != null)
            {
                strSql.Append("AccExpTm='" + model.AccExpTm.Value.ToString("yyyy-MM-dd HH:mm:ss") + "',");
            }
            else
            {
                strSql.Append("AccExpTm= null ,");
            }
            if (model.AccCreateTime != null)
            {
                strSql.Append("AccCreateTime='" + model.AccCreateTime.Value.ToString("yyyy-MM-dd HH:mm:ss") + "',");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where ID=" + model.ID + "");
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

        public int SetEmpPWDByEmpID(int empID, string password)
        {
            int rowsAffected = DbHelperSQL.ExecuteNonQuery(" update RBAC_Users set Password='"+ password + "' where EmpID="+empID);
            return rowsAffected;
        }

        public int SetEmpAccountByEmpID(int empID, string Account)
        {
            int rowsAffected = DbHelperSQL.ExecuteNonQuery(" update RBAC_Users set Account='" + Account + "' where EmpID=" + empID);
            return rowsAffected;
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from RBAC_Users ");
            strSql.Append(" where ID="+ ID);

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
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteListByEmpId(string EmpId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from RBAC_Users ");
            strSql.Append(" where EmpId in (" + EmpId + ")  ");
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
        public Model.RBAC.Entity.RBAC_Users GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID, Account, Password, EmpID, UserDesc, EnMPwd, PwdExprd, AccExprd, AccLock, LockedPeriod, AuUnlock, AccLockDt, LoginTm, AccExpTm, AccCreateTime  ");
            strSql.Append("  from RBAC_Users ");
            strSql.Append(" where ID="+ ID);


            Model.RBAC.Entity.RBAC_Users model = new Model.RBAC.Entity.RBAC_Users();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                model.Account = ds.Tables[0].Rows[0]["Account"].ToString();
                model.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                if (ds.Tables[0].Rows[0]["EmpID"].ToString() != "")
                {
                    model.EmpID = int.Parse(ds.Tables[0].Rows[0]["EmpID"].ToString());
                }
                model.UserDesc = ds.Tables[0].Rows[0]["UserDesc"].ToString();
                if (ds.Tables[0].Rows[0]["EnMPwd"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["EnMPwd"].ToString() == "1") || (ds.Tables[0].Rows[0]["EnMPwd"].ToString().ToLower() == "true"))
                    {
                        model.EnMPwd = true;
                    }
                    else
                    {
                        model.EnMPwd = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["PwdExprd"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["PwdExprd"].ToString() == "1") || (ds.Tables[0].Rows[0]["PwdExprd"].ToString().ToLower() == "true"))
                    {
                        model.PwdExprd = true;
                    }
                    else
                    {
                        model.PwdExprd = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["AccExprd"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["AccExprd"].ToString() == "1") || (ds.Tables[0].Rows[0]["AccExprd"].ToString().ToLower() == "true"))
                    {
                        model.AccExprd = true;
                    }
                    else
                    {
                        model.AccExprd = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["AccLock"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["AccLock"].ToString() == "1") || (ds.Tables[0].Rows[0]["AccLock"].ToString().ToLower() == "true"))
                    {
                        model.AccLock = true;
                    }
                    else
                    {
                        model.AccLock = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["LockedPeriod"].ToString() != "")
                {
                    model.LockedPeriod = int.Parse(ds.Tables[0].Rows[0]["LockedPeriod"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AuUnlock"].ToString() != "")
                {
                    model.AuUnlock = int.Parse(ds.Tables[0].Rows[0]["AuUnlock"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AccLockDt"].ToString() != "")
                {
                    model.AccLockDt = DateTime.Parse(ds.Tables[0].Rows[0]["AccLockDt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LoginTm"].ToString() != "")
                {
                    model.LoginTm = DateTime.Parse(ds.Tables[0].Rows[0]["LoginTm"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AccExpTm"].ToString() != "")
                {
                    model.AccExpTm = DateTime.Parse(ds.Tables[0].Rows[0]["AccExpTm"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AccCreateTime"].ToString() != "")
                {
                    model.AccCreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["AccCreateTime"].ToString());
                }

                return model;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM RBAC_Users ");
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
            strSql.Append(" FROM RBAC_Users ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }


    }
}

