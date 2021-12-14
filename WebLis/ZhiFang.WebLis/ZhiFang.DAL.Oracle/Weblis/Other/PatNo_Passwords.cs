using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using ZhiFang.DBUtility;
using System.Data;

namespace ZhiFang.DAL.Oracle.weblis
{
	/// <summary>
	/// 数据访问类:PatNo_Passwords
	/// </summary>
    public partial class PatNo_Passwords : BaseDALLisDB, IDPatNo_Passwords
	{
         public PatNo_Passwords(string dbsourceconn)
       {
           DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
       }
         public PatNo_Passwords()
       {
           DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
       }
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Id", "PatNo_Passwords"); 
		}


		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from PatNo_Passwords");
			strSql.Append(" where Id="+Id+" ");
			return DbHelperSQL.Exists(strSql.ToString());
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ZhiFang.Model.PatNo_Passwords model)
		{
			StringBuilder strSql=new StringBuilder();
			StringBuilder strSql1=new StringBuilder();
			StringBuilder strSql2=new StringBuilder();
			if (model.PatNo != null)
			{
				strSql1.Append("PatNo,");
				strSql2.Append("'"+model.PatNo+"',");
			}
			if (model.Passwords != null)
			{
				strSql1.Append("Passwords,");
				strSql2.Append("'"+model.Passwords+"',");
			}
			if (model.AddTime != null)
			{
				strSql1.Append("AddTime,");
                strSql2.Append(" to_date('" + model.AddTime.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");
			}
			if (model.UpdateTime != null)
			{
				strSql1.Append("UpdateTime,");
				strSql2.Append("'"+model.UpdateTime+"',");
			}
			strSql.Append("insert into PatNo_Passwords(");
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
		public int Update(ZhiFang.Model.PatNo_Passwords model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update PatNo_Passwords set ");
			if (model.PatNo != null)
			{
				strSql.Append("PatNo='"+model.PatNo+"',");
			}
			if (model.Passwords != null)
			{
				strSql.Append("Passwords='"+model.Passwords+"',");
			}
			if (model.AddTime != null)
			{
                strSql.Append("AddTime=to_date('" + model.AddTime.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");
			}
			if (model.UpdateTime != null)
			{
				strSql.Append("UpdateTime='"+model.UpdateTime+"',");
			}
			int n = strSql.ToString().LastIndexOf(",");
			strSql.Remove(n, 1);
            strSql.Append(" where PatNo='" + model.PatNo + "'");
			int rowsAffected=DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            return rowsAffected;
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from PatNo_Passwords ");
			strSql.Append(" where Id="+Id+"" );
			int rowsAffected=DbHelperSQL.ExecuteNonQuery(strSql.ToString());
			if (rowsAffected > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string Idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from PatNo_Passwords ");
			strSql.Append(" where Id in ("+Idlist + ")  ");
			int rows=DbHelperSQL.ExecuteNonQuery(strSql.ToString());
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
		public ZhiFang.Model.PatNo_Passwords GetModel(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			strSql.Append(" Id,PatNo,Passwords,AddTime,UpdateTime ");
			strSql.Append(" from PatNo_Passwords ");
            strSql.Append(" where ROWNUM <='1' and Id=" + Id + "");
			Model.PatNo_Passwords model=new Model.PatNo_Passwords();
			DataSet ds=DbHelperSQL.ExecuteDataSet(strSql.ToString());
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Id"]!=null && ds.Tables[0].Rows[0]["Id"].ToString()!="")
				{
					model.Id=int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PatNo"]!=null && ds.Tables[0].Rows[0]["PatNo"].ToString()!="")
				{
					model.PatNo=ds.Tables[0].Rows[0]["PatNo"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Passwords"]!=null && ds.Tables[0].Rows[0]["Passwords"].ToString()!="")
				{
					model.Passwords=ds.Tables[0].Rows[0]["Passwords"].ToString();
				}
				if(ds.Tables[0].Rows[0]["AddTime"]!=null && ds.Tables[0].Rows[0]["AddTime"].ToString()!="")
				{
					model.AddTime=DateTime.Parse(ds.Tables[0].Rows[0]["AddTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UpdateTime"]!=null && ds.Tables[0].Rows[0]["UpdateTime"].ToString()!="")
				{
					model.UpdateTime=DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Id,PatNo,Passwords,AddTime,UpdateTime ");
			strSql.Append(" FROM PatNo_Passwords ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.ExecuteDataSet(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			strSql.Append(" Id,PatNo,Passwords,AddTime,UpdateTime ");
			strSql.Append(" FROM PatNo_Passwords ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
                strSql.Append(" and ROWNUM <= '" + Top + "'");
            }
            else
            {
                strSql.Append(" where ROWNUM <= '" + Top + "'");
            }
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.ExecuteDataSet(strSql.ToString());
		}

		/*
		*/

		#endregion  Method

        #region IDataBase<PatNo_Passwords> 成员
        public DataSet GetList(ZhiFang.Model.PatNo_Passwords model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" * ");
            strSql.Append(" FROM PatNo_Passwords where 1=1 ");
            if (model.Id != null && model.Id>0)
            {
                strSql.Append(" and Id='" + model.Id + "' ");
            }
            if (model.PatNo != null)
            {
                strSql.Append(" and PatNo='" + model.PatNo + "' ");
            }
            if (model.Passwords != null)
            {
                strSql.Append(" and Passwords='" + model.Passwords + "'");
            }
            if (model.AddTime != null)
            {
                strSql.Append(" and AddTime=to_date('" + model.AddTime.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");
            }
            if (model.UpdateTime != null)
            {
                strSql.Append(" and UpdateTime='" + model.UpdateTime + "'");
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetList(int Top, Model.PatNo_Passwords model, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" * ");
            strSql.Append(" FROM PatNo_Passwords where 1=1 ");
            if (model.Id != null && model.Id > 0)
            {
                strSql.Append(" and Id='" + model.Id + "' ");
            }
            if (model.PatNo != null)
            {
                strSql.Append(" and PatNo='" + model.PatNo + "' ");
            }
            if (model.Passwords != null)
            {
                strSql.Append(" and Passwords='" + model.Passwords + "'");
            }
            if (model.AddTime != null)
            {
                strSql.Append(" and AddTime=to_date('" + model.AddTime.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");
            }
            if (model.UpdateTime != null)
            {
                strSql.Append(" and UpdateTime='" + model.UpdateTime + "'");
            }
            
                strSql.Append(" and ROWNUM <= '" + Top + "'");
           
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetAllList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,PatNo,Passwords,AddTime,UpdateTime ");
            strSql.Append(" FROM PatNo_Passwords ");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        #endregion

        #region IDataBase<PatNo_Passwords> 成员
        
        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM PatNo_Passwords ");
            string strCount = DbHelperSQL.ExecuteScalar(strSql.ToString());
            if (strCount != null && strCount.Trim() != "")
            {
                return Convert.ToInt32(strCount.Trim());
            }
            else
            {
                return 0;
            }
        }

        public int GetTotalCount(Model.PatNo_Passwords model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM PatNo_Passwords where 1=1 ");
            if (model.Id != null && model.Id > 0)
            {
                strSql.Append(" and Id='" + model.Id + "' ");
            }
            if (model.PatNo != null)
            {
                strSql.Append(" and PatNo='" + model.PatNo + "' ");
            }
            if (model.Passwords != null)
            {
                strSql.Append(" and Passwords='" + model.Passwords + "'");
            }
            if (model.AddTime != null)
            {
                strSql.Append(" and AddTime=to_date('" + model.AddTime.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");
            }
            if (model.UpdateTime != null)
            {
                strSql.Append(" and UpdateTime='" + model.UpdateTime + "'");
            }
            string strCount = DbHelperSQL.ExecuteScalar(strSql.ToString());
            if (strCount != null && strCount.Trim() != "")
            {
                return Convert.ToInt32(strCount.Trim());
            }
            else
            {
                return 0;
            }
        }

        #endregion

        #region IDataBase<PatNo_Passwords> 成员


        public int AddUpdateByDataSet(DataSet ds)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

