using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.DBUtility;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.Model;

namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.ReportCenter
{
    public class Department : IDDepartment
    {
        public int Add(Model.Department t)
        {
            throw new NotImplementedException();
        }

        public int Delete(int DeptNo)
        {
            throw new NotImplementedException();
        }

        public bool Exists(int DeptNo)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(Model.Department t)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            
            if (strWhere != null && strWhere.Trim() != "")
            {
                strWhere = " where 1=1 and " + strWhere;
            }
            strSql.Append("SELECT * FROM Department " + strWhere);
            ZhiFang.Common.Log.Log.Debug(strSql.ToString());
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(string Where, int page, int limit)
        {
            StringBuilder strSql = new StringBuilder();
            if (page < 1)
            {
                page = 1;
            }
            if (limit < 1)
            {
                limit = 1;
            }
            if (Where != null && Where.Trim() != "")
            {
                Where = " where 1=1 and " + Where;
            }
            strSql.Append("SELECT * FROM(  SELECT TOP " + limit + " * FROM  (    SELECT TOP " + page * limit + " * FROM Department " + Where + "  ORDER BY DeptNo    ) as a  ORDER BY DeptNo desc) as b ORDER BY DeptNo ASC ");
            return DbHelperSQL.Query(strSql.ToString());
        }

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Department GetModel(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(" DeptNo,CName,ShortName,ShortCode,Visible,DispOrder,HisOrderCode ");
            strSql.Append(" from Department ");
            strSql.Append(" where " + where);
            Model.Department model = new Model.Department();
            ZhiFang.Common.Log.Log.Debug(strSql.ToString());
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["DeptNo"].ToString() != "")
                {
                    model.DeptNo = int.Parse(ds.Tables[0].Rows[0]["DeptNo"].ToString());
                }
                model.CName = ds.Tables[0].Rows[0]["CName"].ToString();
                model.ShortName = ds.Tables[0].Rows[0]["ShortName"].ToString();
                model.ShortCode = ds.Tables[0].Rows[0]["ShortCode"].ToString();
                if (ds.Tables[0].Rows[0]["Visible"].ToString() != "")
                {
                    model.Visible = int.Parse(ds.Tables[0].Rows[0]["Visible"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DispOrder"].ToString() != "")
                {
                    model.DispOrder = int.Parse(ds.Tables[0].Rows[0]["DispOrder"].ToString());
                }
                model.HisOrderCode = ds.Tables[0].Rows[0]["HisOrderCode"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }

        public Model.Department GetModel(int DeptNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(" DeptNo,CName,ShortName,ShortCode,Visible,DispOrder ");
            strSql.Append(" from Department ");
            strSql.Append(" where DeptNo=" + DeptNo + " ");
            Model.Department model = new Model.Department();
            ZhiFang.Common.Log.Log.Debug(strSql.ToString());
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["DeptNo"].ToString() != "")
                {
                    model.DeptNo = long.Parse(ds.Tables[0].Rows[0]["DeptNo"].ToString());
                }
                model.CName = ds.Tables[0].Rows[0]["CName"].ToString();
                model.ShortName = ds.Tables[0].Rows[0]["ShortName"].ToString();
                model.ShortCode = ds.Tables[0].Rows[0]["ShortCode"].ToString();
                if (ds.Tables[0].Rows[0]["Visible"].ToString() != "")
                {
                    model.Visible = int.Parse(ds.Tables[0].Rows[0]["Visible"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DispOrder"].ToString() != "")
                {
                    model.DispOrder = int.Parse(ds.Tables[0].Rows[0]["DispOrder"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        public int Update(Model.Department t)
        {
            throw new NotImplementedException();
        }
    }
}
