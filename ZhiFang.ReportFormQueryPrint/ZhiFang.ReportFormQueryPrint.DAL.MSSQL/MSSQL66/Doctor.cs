using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.DBUtility;
using System.Data;
namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66
{
	/// <summary>
	/// 数据访问类Doctor。
	/// </summary>
    public class Doctor : IDDoctor
	{
        public Doctor()
        { }
        #region  成员方法

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("DoctorNo", "Doctor");
        }


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int DoctorNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Doctor");
            strSql.Append(" where DoctorNo=" + DoctorNo + " ");
            return DbHelperSQL.Exists(strSql.ToString());
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.Doctor model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.DoctorNo != null)
            {
                strSql1.Append("DoctorNo,");
                strSql2.Append("" + model.DoctorNo + ",");
            }
            if (model.CName != null)
            {
                strSql1.Append("CName,");
                strSql2.Append("'" + model.CName + "',");
            }
            if (model.ShortCode != null)
            {
                strSql1.Append("ShortCode,");
                strSql2.Append("'" + model.ShortCode + "',");
            }
            if (model.HisOrderCode != null)
            {
                strSql1.Append("HisOrderCode,");
                strSql2.Append("'" + model.HisOrderCode + "',");
            }
            if (model.Visible != null)
            {
                strSql1.Append("Visible,");
                strSql2.Append("" + model.Visible + ",");
            }
            if (model.doctorPhoneCode != null)
            {
                strSql1.Append("doctorPhoneCode,");
                strSql2.Append("'" + model.doctorPhoneCode + "',");
            }
            strSql.Append("insert into Doctor(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Model.Doctor model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Doctor set ");
            if (model.ShortCode != null)
            {
                strSql.Append("ShortCode='" + model.ShortCode + "',");
            }
            if (model.CName != null)
            {
                strSql.Append("CName='" + model.CName + "',");
            }
            if (model.HisOrderCode != null)
            {
                strSql.Append("HisOrderCode='" + model.HisOrderCode + "',");
            }
            if (model.Visible != null)
            {
                strSql.Append("Visible=" + model.Visible + ",");
            }
            if (model.doctorPhoneCode != null)
            {
                strSql.Append("doctorPhoneCode='" + model.doctorPhoneCode + "',");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where DoctorNo=" + model.DoctorNo + " ");
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int DoctorNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Doctor ");
            strSql.Append(" where DoctorNo=" + DoctorNo + " ");
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Doctor GetModel(int DoctorNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(" DoctorNo,CName,ShortCode,HisOrderCode,Visible,doctorPhoneCode ");
            strSql.Append(" from Doctor ");
            strSql.Append(" where DoctorNo=" + DoctorNo + " ");
            Model.Doctor model = new Model.Doctor();
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["DoctorNo"].ToString() != "")
                {
                    model.DoctorNo = int.Parse(ds.Tables[0].Rows[0]["DoctorNo"].ToString());
                }
                model.CName = ds.Tables[0].Rows[0]["CName"].ToString();
                model.ShortCode = ds.Tables[0].Rows[0]["ShortCode"].ToString();
                model.HisOrderCode = ds.Tables[0].Rows[0]["HisOrderCode"].ToString();
                if (ds.Tables[0].Rows[0]["Visible"].ToString() != "")
                {
                    model.Visible = int.Parse(ds.Tables[0].Rows[0]["Visible"].ToString());
                }
                model.doctorPhoneCode = ds.Tables[0].Rows[0]["doctorPhoneCode"].ToString();
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
            strSql.Append("select DoctorNo,CName,ShortCode,HisOrderCode,Visible,doctorPhoneCode ");
            strSql.Append(" FROM Doctor ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
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
            strSql.Append(" DoctorNo,CName,ShortCode,HisOrderCode,Visible,doctorPhoneCode ");
            strSql.Append(" FROM Doctor ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
        */


        public DataSet GetList(Model.Doctor model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DoctorNo,CName,ShortCode,HisOrderCode,Visible,doctorPhoneCode ");
            strSql.Append(" FROM Doctor where 1=1 ");
            if (model.ShortCode != null)
            {
                strSql.Append(" and ShortCode='" + model.ShortCode + "'");
            }
            if (model.CName != null)
            {
                strSql.Append(" and CName='" + model.CName + "'");
            }
            if (model.HisOrderCode != null)
            {
                strSql.Append(" and HisOrderCode='" + model.HisOrderCode + "'");
            }
            if (model.Visible != null)
            {
                strSql.Append(" and Visible=" + model.Visible + "");
            }
            if (model.doctorPhoneCode != null)
            {
                strSql.Append(" and doctorPhoneCode='" + model.doctorPhoneCode + "'");
            }
            if (model.DoctorNo != null)
            {
                strSql.Append(" and DoctorNo=" + model.DoctorNo + "");
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion
    }
}

