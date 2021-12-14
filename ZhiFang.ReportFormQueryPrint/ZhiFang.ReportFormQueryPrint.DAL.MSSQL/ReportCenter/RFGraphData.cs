using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZhiFang.ReportFormQueryPrint.DBUtility;
using ZhiFang.ReportFormQueryPrint.IDAL;
namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.ReportCenter
{
	/// <summary>
	/// 数据访问类:RFGraphData
	/// </summary>
	public class RFGraphData:IDRFGraphData
	{
		public RFGraphData()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("GraphNo", "RFGraphData"); 
		}


		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string ReportFormID,string GraphName,int GraphNo)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from RFGraphData");
			strSql.Append(" where ReportFormID='"+ReportFormID+"' and GraphName='"+GraphName+"' and GraphNo="+GraphNo+" ");
			return DbHelperSQL.Exists(strSql.ToString());
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string ReportFormID,string GraphName,int GraphNo)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from RFGraphData ");
			strSql.Append(" where ReportFormID='"+ReportFormID+"' and GraphName='"+GraphName+"' and GraphNo="+GraphNo+" " );
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
            strSql.Append("select * ");
			strSql.Append(" FROM RFGraphData ");
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
            strSql.Append(" ReportFormID,ReceiveDate,SectionNo,TestTypeNo,SampleNo,GraphName,GraphNo,EquipNo,PointType,ShowPoint,MColor,SColor,ShowAxis,ShowLable,MinX,MaxX,MinY,MaxY,ShowTitle,STitle,GraphValue,GraphMemo,GraphF1,GraphF2,ChartTop,ChartHeight,ChartLeft,ChartWidth,Graphjpg,IsFile,GraphFileName,GraphFileTime,isFileToServer,FilePath ");
			strSql.Append(" FROM RFGraphData ");
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
			strSql.Append("select count(1) FROM RFGraphData ");
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
				strSql.Append("order by T.GraphNo desc");
			}
			strSql.Append(")AS Row, T.*  from RFGraphData T ");
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

        public bool Exists(string GraphName, int GraphNo, string FormNo)
        {
            throw new NotImplementedException();
        }

        public int Delete(string GraphName, int GraphNo, string FormNo)
        {
            throw new NotImplementedException();
        }

        public Model.RFGraphData GetModel(string GraphName, int GraphNo, string FormNo)
        {
            throw new NotImplementedException();
        }


        public int Add(Model.RFGraphData t)
        {
            throw new NotImplementedException();
        }

        public int Update(Model.RFGraphData t)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(Model.RFGraphData t)
        {
            throw new NotImplementedException();
        }


        public DataSet GetListByReportFormId(string ReportFormId)
        {
            return this.GetList(" ReportFormID ='" + ReportFormId + "' ");
        }


        public DataSet GetListByReportPublicationID(string ReportPublicationID)
        {
            return this.GetList(" ReportPublicationID  ='" + ReportPublicationID + "' ");
        }

        public DataSet GetListByReportPublicationIDAndPointType(string ReportPublicationID, string PointType)
        {
			string param = " ReportPublicationID  ='" + ReportPublicationID + "' ";
			if (!string.IsNullOrEmpty(PointType.Trim()))
			{
				param += " and PointType not in (" + PointType + ")";
			}
			return this.GetList(param);
		}
    }
}

