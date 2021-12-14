using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ZhiFang.DAL.RBAC
{
    public class RBAC_RoleModuleLinkDAO: BaseDALLisDB
	{
		//public RBAC_RoleModuleLinkDAO(string dbsourceconn)
		//{
		//	DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
		//}
		//public RBAC_RoleModuleLinkDAO()
		//{
		//	DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
		//}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long Id)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from RBAC_RoleModuleLink");
			strSql.Append(" where Id=" + Id + " ");
			return DbHelperSQL.Exists(strSql.ToString());
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Model.RBAC_RoleModuleLinkModel model)
		{
			StringBuilder strSql = new StringBuilder();
			StringBuilder strSql1 = new StringBuilder();
			StringBuilder strSql2 = new StringBuilder();
			if (model.Id != null)
			{
				strSql1.Append("Id,");
				strSql2.Append("" + model.Id + ",");
			}
			if (model.ModuleId != null)
			{
				strSql1.Append("ModuleId,");
				strSql2.Append("" + model.ModuleId + ",");
			}
			if (model.RoleId != null)
			{
				strSql1.Append("RoleId,");
				strSql2.Append("" + model.RoleId + ",");
			}
			if (model.ModuleSN != null)
			{
				strSql1.Append("ModuleSN,");
				strSql2.Append("'" + model.ModuleSN + "',");
			}
			if (model.IsUse != null)
			{
				strSql1.Append("IsUse,");
				strSql2.Append("" + (model.IsUse ? 1 : 0) + ",");
			}
			if (model.DataAddTime != null)
			{
				strSql1.Append("DataAddTime,");
				strSql2.Append("'" + model.DataAddTime + "',");
			}
			if (model.DataTimeStamp != null)
			{
				strSql1.Append("DataTimeStamp,");
				strSql2.Append("" + model.DataTimeStamp + ",");
			}
			strSql.Append("insert into RBAC_RoleModuleLink(");
			strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
			strSql.Append(")");
			strSql.Append(" values (");
			strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
			strSql.Append(")");
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
		/// 更新一条数据
		/// </summary>
		public bool Update(Model.RBAC_RoleModuleLinkModel model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update RBAC_RoleModuleLink set ");
			if (model.ModuleId != null)
			{
				strSql.Append("ModuleId=" + model.ModuleId + ",");
			}
			else
			{
				strSql.Append("ModuleId= null ,");
			}
			if (model.RoleId != null)
			{
				strSql.Append("RoleId=" + model.RoleId + ",");
			}
			else
			{
				strSql.Append("RoleId= null ,");
			}
			if (model.ModuleSN != null)
			{
				strSql.Append("ModuleSN='" + model.ModuleSN + "',");
			}
			else
			{
				strSql.Append("ModuleSN= null ,");
			}
			if (model.IsUse != null)
			{
				strSql.Append("IsUse=" + (model.IsUse ? 1 : 0) + ",");
			}
			else
			{
				strSql.Append("IsUse= null ,");
			}
			if (model.DataAddTime != null)
			{
				strSql.Append("DataAddTime='" + model.DataAddTime + "',");
			}
			else
			{
				strSql.Append("DataAddTime= null ,");
			}
			int n = strSql.ToString().LastIndexOf(",");
			strSql.Remove(n, 1);
			strSql.Append(" where Id=" + model.Id + " ");
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
		public bool Delete(long Id)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from RBAC_RoleModuleLink ");
			strSql.Append(" where Id=" + Id + " ");
			int rowsAffected = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
			if (rowsAffected > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}       /// <summary>
				/// 批量删除数据
				/// </summary>
		public bool DeleteList(string Idlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from RBAC_RoleModuleLink ");
			strSql.Append(" where Id in (" + Idlist + ")  ");
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

		public bool DeleteListwhere(string where)
		{
			if (string.IsNullOrEmpty(where))
			{
				return false;
			}
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from RBAC_RoleModuleLink ");
			strSql.Append(" where "+ where);
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
		public Model.RBAC_RoleModuleLinkModel GetModel(long Id)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1  ");
			strSql.Append(" Id,ModuleId,RoleId,ModuleSN,IsUse,DataAddTime,DataTimeStamp ");
			strSql.Append(" from RBAC_RoleModuleLink ");
			strSql.Append(" where Id=" + Id + " ");
			Model.RBAC_RoleModuleLinkModel model = new Model.RBAC_RoleModuleLinkModel();
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
		public Model.RBAC_RoleModuleLinkModel DataRowToModel(DataRow row)
		{
			Model.RBAC_RoleModuleLinkModel model = new Model.RBAC_RoleModuleLinkModel();
			if (row != null)
			{
				if (row["Id"] != null && row["Id"].ToString() != "")
				{
					model.Id = long.Parse(row["Id"].ToString());
				}
				if (row["ModuleId"] != null && row["ModuleId"].ToString() != "")
				{
					model.ModuleId = long.Parse(row["ModuleId"].ToString());
				}
				if (row["RoleId"] != null && row["RoleId"].ToString() != "")
				{
					model.RoleId = long.Parse(row["RoleId"].ToString());
				}
				if (row["ModuleSN"] != null)
				{
					model.ModuleSN = row["ModuleSN"].ToString();
				}
				if (row["IsUse"] != null && row["IsUse"].ToString() != "")
				{
					if ((row["IsUse"].ToString() == "1") || (row["IsUse"].ToString().ToLower() == "true"))
					{
						model.IsUse = true;
					}
					else
					{
						model.IsUse = false;
					}
				}
				if (row["DataAddTime"] != null && row["DataAddTime"].ToString() != "")
				{
					model.DataAddTime = DateTime.Parse(row["DataAddTime"].ToString());
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
			strSql.Append("select Id,ModuleId,RoleId,ModuleSN,IsUse,DataAddTime,DataTimeStamp ");
			strSql.Append(" FROM RBAC_RoleModuleLink ");
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
			strSql.Append(" Id,ModuleId,RoleId,ModuleSN,IsUse,DataAddTime,DataTimeStamp ");
			strSql.Append(" FROM RBAC_RoleModuleLink ");
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
			strSql.Append("select count(1) FROM RBAC_RoleModuleLink ");
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
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby);
			}
			else
			{
				strSql.Append("order by T.Id desc");
			}
			strSql.Append(")AS Row, T.*  from RBAC_RoleModuleLink T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.ExecuteDataSet(strSql.ToString());
		}
	}
}
