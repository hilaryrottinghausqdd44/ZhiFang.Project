using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.DBUtility;
using ZhiFang.ReportFormQueryPrint.Model;

namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.ReportCenter
{
	/// <summary>
	/// 数据访问类:ReportMarrowFull
	/// </summary>
	public class ReportMarrow:IDReportMarrow
	{
		public ReportMarrow()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ItemNo", "ReportMarrowFull"); 
		}


		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string ReportFormID,int ItemNo)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from ReportMarrowFull");
			strSql.Append(" where ReportFormID='"+ReportFormID+"' and ItemNo="+ItemNo+" ");
			return DbHelperSQL.Exists(strSql.ToString());
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string ReportFormID,int ItemNo)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ReportMarrowFull ");
			strSql.Append(" where ReportFormID='"+ReportFormID+"' and ItemNo="+ItemNo+" " );
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
			strSql.Append(" FROM ReportMarrowFull ");
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
			strSql.Append(" * ");
			strSql.Append(" FROM ReportMarrowFull ");
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
			strSql.Append("select count(1) FROM ReportMarrowFull ");
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
			strSql.Append(")AS Row, T.*  from ReportMarrowFull T ");
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

        public bool Exists(string ItemNo, string FormNo)
        {
            throw new NotImplementedException();
        }

        public int Delete(string ItemNo, string FormNo)
        {
            throw new NotImplementedException();
        }

        public ZhiFang.ReportFormQueryPrint.Model.ReportMarrow GetModel(string ItemNo, string FormNo)
        {
            throw new NotImplementedException();
        }

        public DataTable GetReportMarrowItemList(string FormNo)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * ");
                strSql.Append(" FROM ReportMarrowQueryDataSource ");
                strSql.Append(" where ReportFormID='" + FormNo + "' ");
                DataSet ds = DbHelperSQL.Query(strSql.ToString());
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                    {
                        ds.Tables[0].Columns[i].ColumnName = ds.Tables[0].Columns[i].ColumnName.ToUpper();
                    }
                    ZhiFang.Common.Log.Log.Debug(strSql.ToString() + "@" + DbHelperSQL.connectionString);
                    //Common.TransDataToXML.TransformDTIntoXML(ds.Tables[0], "d://ReportItem.xml");
                    return ds.Tables[0];
                }
                else
                {
                    return new DataTable();
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("GetReportItemFullList:" + ex.ToString());
                return new DataTable();
            }
        }

        public DataSet GetReportMarrowFullList(string p)
        {
            throw new NotImplementedException();
        }


        public int Add(ZhiFang.ReportFormQueryPrint.Model.ReportMarrow t)
        {
            throw new NotImplementedException();
        }

        public int Update(ZhiFang.ReportFormQueryPrint.Model.ReportMarrow t)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(ZhiFang.ReportFormQueryPrint.Model.ReportMarrow t)
        {
            throw new NotImplementedException();
        }

        internal DataTable GetReportMarrowList(string FormNo)
        {
            throw new NotImplementedException();
        }

        public DataSet GetReportMarrowFullByReportFormID(string reportformid)
        {
            if (reportformid == null || reportformid.Equals(""))
            {
                ZhiFang.Common.Log.Log.Debug("GetReportMarrowFullByReportFormID.ReportFormID为空请检查传入参数！");
                return null;
            }

            StringBuilder sql = new StringBuilder();
            sql.Append("select * from ReportMarrowFull where ReportPublicationID = '" + reportformid + "'");
            ZhiFang.Common.Log.Log.Debug("GetReportMarrowFullByReportFormID.ReportFormFull.SQL:" + sql.ToString());
            return DbHelperSQL.Query(sql.ToString());
        }

        public int UpdateReportMarrowFull(ReportMarrowFull t)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE ReportMarrowFull SET ");

            builder.Append("DataUpdateTime=" + "'" + DateTime.Now + "'");

            if (t.SectionNo != null && !t.SectionNo.Equals(""))
            {
                builder.Append(",SectionNo=" + "'" + t.SectionNo + "'");
            }
            if (t.TestTypeNo != null && !t.TestTypeNo.Equals(""))
            {
                builder.Append(",TestTypeNo=" + "'" + t.TestTypeNo + "'");
            }
            if (t.SampleNo != null && !t.SampleNo.Equals(""))
            {
                builder.Append(",SampleNo=" + "'" + t.SampleNo + "'");
            }
            if (t.ParItemNo != null && !t.ParItemNo.Equals(""))
            {
                builder.Append(",ParItemNo=" + "'" + t.ParItemNo + "'");
            }
            if (t.ParitemName != null && !t.ParitemName.Equals(""))
            {
                builder.Append(",ParitemName=" + "'" + t.ParitemName + "'");
            }
            if (t.ItemCname != null && !t.ItemCname.Equals(""))
            {
                builder.Append(",ItemCname=" + "'" + t.ItemCname + "'");
            }
            if (t.ItemEname != null && !t.ItemEname.Equals(""))
            {
                builder.Append(",ItemEname=" + "'" + t.ItemEname + "'");
            }
            if (t.BloodDesc != null && !t.BloodDesc.Equals(""))
            {
                builder.Append(",BloodDesc=" + "'" + t.BloodDesc + "'");
            }
            if (t.MarrowNum != null && !t.MarrowNum.Equals(""))
            {
                builder.Append(",MarrowNum=" + "'" + t.MarrowNum + "'");
            }
            if (t.MarrowPercent != null && !t.MarrowPercent.Equals(""))
            {
                builder.Append(",MarrowPercent=" + "'" + t.MarrowPercent + "'");
            }
            if (t.MarrowDesc != null && !t.MarrowDesc.Equals(""))
            {
                builder.Append(",MarrowDesc=" + "'" + t.MarrowDesc + "'");
            }
            if (t.RefRange != null && !t.RefRange.Equals(""))
            {
                builder.Append(",RefRange=" + "'" + t.RefRange + "'");
            }
            if (t.EquipName != null && !t.EquipName.Equals(""))
            {
                builder.Append(",EquipName=" + "'" + t.EquipName + "'");
            }
            if (t.ResultStatus != null && !t.ResultStatus.Equals(""))
            {
                builder.Append(",ResultStatus=" + "'" + t.ResultStatus + "'");
            }

            if (t.DiagMethod != null && !t.DiagMethod.Equals(""))
            {
                builder.Append(",DiagMethod=" + "'" + t.DiagMethod + "'");
            }            
            builder.Append(" WHERE ReportPublicationID =" + t.ReportPublicationID + " and ItemNo = " + t.ItemNo);
            ZhiFang.Common.Log.Log.Debug("UpdateReportItemFull:" + builder.ToString());
            return DbHelperSQL.ExecuteSql(builder.ToString());
        }
    }
}

