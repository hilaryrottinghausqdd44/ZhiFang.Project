using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ZhiFang.DAO.SQL.WebAssist.GKBarcode;
using ZhiFang.Entity.WebAssist.GKBarcode;
using ZhiFang.IDAO.NHB.WebAssist;

namespace ZhiFang.DAO.SQL.WebAssist
{
	/// <summary>
	/// 数据访问类:LastBarcodeS
	/// </summary>
	public partial class LastBarcodeS_SQL : ILastBarcodeS_SQL
	{
		public LastBarcodeS_SQL()
		{}
		#region  Method
		//查询字段
		private string FieldStr = " LastBarcodeS,Date,Time,Department ";
		public IList<LastBarcodeS> GetListByHQL(string strSqlWhere)
		{
			IList<LastBarcodeS> tempList = new List<LastBarcodeS>();
			DataSet ds = GetList(strSqlWhere);
			if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow row in ds.Tables[0].Rows)
				{
					tempList.Add(DataRowToModel(row));
				}
			}
			else
			{
				return tempList;
			}
			return tempList;
		}
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LastBarcodeS GetModel()
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1  ");
			strSql.Append(this.FieldStr);
			strSql.Append(" from LastBarcodeS ");
			strSql.Append(" where " );
			LastBarcodeS model=new LastBarcodeS();
			DataSet ds=DbHelperSQL.QuerySql(DbHelperSQL.ConnectionString, strSql.ToString());
			if(ds.Tables[0].Rows.Count>0)
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
		public LastBarcodeS DataRowToModel(DataRow row)
		{
			LastBarcodeS model=new LastBarcodeS();
			if (row != null)
			{
				if(row["LastBarcodeS"]!=null && row["LastBarcodeS"].ToString()!="")
				{
					model.LastBarcodeSId=int.Parse(row["LastBarcodeS"].ToString());
				}
				if(row["Date"]!=null && row["Date"].ToString()!="")
				{
					model.Date=DateTime.Parse(row["Date"].ToString());
				}
				if(row["Time"]!=null && row["Time"].ToString()!="")
				{
					model.Time=DateTime.Parse(row["Time"].ToString());
				}
				if(row["Department"]!=null)
				{
					model.Department=row["Department"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			strSql.Append(this.FieldStr);
			strSql.Append(" FROM LastBarcodeS ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.QuerySql(DbHelperSQL.ConnectionString, strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" LastBarcodeS,Date,Time,Department ");
			strSql.Append(" FROM LastBarcodeS ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.QuerySql(DbHelperSQL.ConnectionString, strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM LastBarcodeS ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperSQL.GetSingle(DbHelperSQL.ConnectionString, strSql.ToString());
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.SerialNo desc");
			}
			strSql.Append(")AS Row, T.*  from LastBarcodeS T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.QuerySql(DbHelperSQL.ConnectionString, strSql.ToString());
		}

		/*
		*/

		#endregion  Method
		#region  MethodEx

		#endregion  MethodEx
	}
}

