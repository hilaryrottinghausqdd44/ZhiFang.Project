using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZhiFang.ReportFormQueryPrint.DBUtility;
namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.ReportCenter
{
	/// <summary>
	/// 数据访问类:ReportResultsFull
	/// </summary>
	public class ReportResultsFull
	{
		public ReportResultsFull()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ParItemNo", "ReportResultsFull"); 
		}


		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string ReportFormID,int ParItemNo,int ItemNo)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from ReportResultsFull");
			strSql.Append(" where ReportFormID='"+ReportFormID+"' and ParItemNo="+ParItemNo+" and ItemNo="+ItemNo+" ");
			return DbHelperSQL.Exists(strSql.ToString());
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string ReportFormID,int ParItemNo,int ItemNo)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ReportResultsFull ");
			strSql.Append(" where ReportFormID='"+ReportFormID+"' and ParItemNo="+ParItemNo+" and ItemNo="+ItemNo+" " );
			int rowsAffected=DbHelperSQL.ExecuteSql(strSql.ToString());
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
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ReportFormID,ReceiveDate,SectionNo,TestTypeNo,SampleNo,OrderNo,ParItemNo,ItemNo,ValueTypeNo,ItemCname,ItemEname,ReportValue,ReportDesc,ReportText,ReportImage,ResultStatus,RefRange,CItemNo,CItemName,isFileToServer,GraphFileTime,GraphFileName ");
			strSql.Append(" FROM ReportResultsFull ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
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
			strSql.Append(" ReportFormID,ReceiveDate,SectionNo,TestTypeNo,SampleNo,OrderNo,ParItemNo,ItemNo,ValueTypeNo,ItemCname,ItemEname,ReportValue,ReportDesc,ReportText,ReportImage,ResultStatus,RefRange,CItemNo,CItemName,isFileToServer,GraphFileTime,GraphFileName ");
			strSql.Append(" FROM ReportResultsFull ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM ReportResultsFull ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.ItemNo desc");
			}
			strSql.Append(")AS Row, T.*  from ReportResultsFull T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		*/

		#endregion  Method
		#region  MethodEx

		#endregion  MethodEx
	}
}

