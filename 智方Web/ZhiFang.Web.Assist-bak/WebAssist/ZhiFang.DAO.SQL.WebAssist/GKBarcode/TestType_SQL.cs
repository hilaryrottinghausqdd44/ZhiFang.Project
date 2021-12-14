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
	/// 数据访问类:TestType
	/// </summary>
	public partial class TestType_SQL : ITestType_SQL
	{
		public TestType_SQL()
		{}
		#region  Method
		//查询字段
		private string FieldStr = " TestTypeID,TestTypeName,TestParIndex,information1,information2,information3,information4,Item1_Index,Item2_Index,Item3_Index,Item4_Index,ReprtTemp,Samples_P_RP,Infors_P_Sample,QCStandar,JugeRepC1Text,JugeRepC1Left,JugeRepC2Text,JugeRepC2Left,JugeRepC3Text,JugeRepC3Left";
		public IList<TestType> GetListByHQL(string strSqlWhere)
		{
			IList<TestType> tempList = new List<TestType>();
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
		public ZhiFang.Entity.WebAssist.GKBarcode.TestType GetModel()
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1  ");
			strSql.Append(this.FieldStr);
			strSql.Append(" from TestType ");
			strSql.Append(" where " );
			TestType model=new TestType();
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
		public ZhiFang.Entity.WebAssist.GKBarcode.TestType DataRowToModel(DataRow row)
		{
			ZhiFang.Entity.WebAssist.GKBarcode.TestType model=new ZhiFang.Entity.WebAssist.GKBarcode.TestType();
			if (row != null)
			{
				if(row["TestTypeID"]!=null && row["TestTypeID"].ToString()!="")
				{
					model.TestTypeID=int.Parse(row["TestTypeID"].ToString());
				}
				if(row["TestTypeName"]!=null)
				{
					model.TestTypeName=row["TestTypeName"].ToString();
				}
				if(row["TestParIndex"]!=null && row["TestParIndex"].ToString()!="")
				{
					model.TestParIndex=int.Parse(row["TestParIndex"].ToString());
				}
				if(row["information1"]!=null)
				{
					model.information1=row["information1"].ToString();
				}
				if(row["information2"]!=null)
				{
					model.information2=row["information2"].ToString();
				}
				if(row["information3"]!=null)
				{
					model.information3=row["information3"].ToString();
				}
				if(row["information4"]!=null)
				{
					model.information4=row["information4"].ToString();
				}
				if(row["Item1_Index"]!=null && row["Item1_Index"].ToString()!="")
				{
					model.Item1_Index=int.Parse(row["Item1_Index"].ToString());
				}
				if(row["Item2_Index"]!=null && row["Item2_Index"].ToString()!="")
				{
					model.Item2_Index=int.Parse(row["Item2_Index"].ToString());
				}
				if(row["Item3_Index"]!=null)
				{
					model.Item3_Index=row["Item3_Index"].ToString();
				}
				if(row["Item4_Index"]!=null && row["Item4_Index"].ToString()!="")
				{
					model.Item4_Index=int.Parse(row["Item4_Index"].ToString());
				}
				if(row["ReprtTemp"]!=null)
				{
					model.ReprtTemp=row["ReprtTemp"].ToString();
				}
				if(row["Samples_P_RP"]!=null && row["Samples_P_RP"].ToString()!="")
				{
					model.Samples_P_RP=int.Parse(row["Samples_P_RP"].ToString());
				}
				if(row["Infors_P_Sample"]!=null && row["Infors_P_Sample"].ToString()!="")
				{
					model.Infors_P_Sample=int.Parse(row["Infors_P_Sample"].ToString());
				}
				if(row["QCStandar"]!=null)
				{
					model.QCStandar=row["QCStandar"].ToString();
				}
				if(row["JugeRepC1Text"]!=null)
				{
					model.JugeRepC1Text=row["JugeRepC1Text"].ToString();
				}
				if(row["JugeRepC1Left"]!=null)
				{
					model.JugeRepC1Left=row["JugeRepC1Left"].ToString();
				}
				if(row["JugeRepC2Text"]!=null)
				{
					model.JugeRepC2Text=row["JugeRepC2Text"].ToString();
				}
				if(row["JugeRepC2Left"]!=null)
				{
					model.JugeRepC2Left=row["JugeRepC2Left"].ToString();
				}
				if(row["JugeRepC3Text"]!=null)
				{
					model.JugeRepC3Text=row["JugeRepC3Text"].ToString();
				}
				if(row["JugeRepC3Left"]!=null)
				{
					model.JugeRepC3Left=row["JugeRepC3Left"].ToString();
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
			strSql.Append(" FROM TestType ");
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
			strSql.Append(this.FieldStr);
			strSql.Append(" FROM TestType ");
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
			strSql.Append("select count(1) FROM TestType ");
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
			strSql.Append(")AS Row, T.*  from TestType T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.QuerySql(DbHelperSQL.ConnectionString, strSql.ToString());
		}

        #endregion  Method
     
    }
}

