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
	/// 数据访问类:ReportMicroFull
	/// </summary>
	public class ReportMicro:IDReportMicro
	{
		public ReportMicro()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("OrderNo", "ReportMicroFull"); 
		}


		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string ReportFormID,int OrderNo)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from ReportMicroFull");
			strSql.Append(" where ReportFormID='"+ReportFormID+"' and OrderNo="+OrderNo+" ");
			return DbHelperSQL.Exists(strSql.ToString());
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string ReportFormID,int OrderNo)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ReportMicroFull ");
			strSql.Append(" where ReportFormID='"+ReportFormID+"' and OrderNo="+OrderNo+" " );
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
			strSql.Append(" FROM ReportMicroFull ");
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
			strSql.Append(" FROM ReportMicroFull ");
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
			strSql.Append("select count(1) FROM ReportMicroFull ");
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
				strSql.Append("order by T.OrderNo desc");
			}
			strSql.Append(")AS Row, T.*  from ReportMicroFull T ");
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

        public bool Exists(int ResultNo, int ItemNo, string FormNo)
        {
            throw new NotImplementedException();
        }

        public int Delete(int ResultNo, int ItemNo, string FormNo)
        {
            throw new NotImplementedException();
        }

        public ZhiFang.ReportFormQueryPrint.Model.ReportMicro GetModel(int ResultNo, int ItemNo, string FormNo)
        {
            throw new NotImplementedException();
        }

        public DataTable GetReportMicroList(string FormNo)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * ");
                strSql.Append(" FROM ReportMicroQueryDataSource ");
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
                ZhiFang.Common.Log.Log.Debug("GetReportMicroList:" + ex.ToString());
                return new DataTable();
            }
        }

        public DataTable GetReportMicroList(string FormNo, string ItemNo)
        {
            throw new NotImplementedException();
        }

        public DataTable GetReportMicroList(string FormNo, string ItemNo, string MicroNo)
        {
            throw new NotImplementedException();
        }

        public DataTable GetReportMicroGroupList(string FormNo)
        {
            try
            {
                #region 执行存储过程
                if (FormNo != null && FormNo.Trim()!="")
                {

                    SqlParameter sp = new SqlParameter("@ReportFormID", SqlDbType.VarChar, 50);
                    sp.Value = FormNo;
                    DataSet ds = DbHelperSQL.RunProcedure("GetReportMicroGroupFullList", new SqlParameter[] { sp }, "ReportMicFull");
                    ZhiFang.Common.Log.Log.Debug(ds.GetXml() + "@" + DbHelperSQL.connectionString);
                    if (ds.Tables.Count > 0)
                    {
                        ds.Tables[0].Columns.Add("DISPLAYID", typeof(string));
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            ds.Tables[0].Rows[i]["DISPLAYID"] = (i + 1).ToString();
                        }
                        for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                        {
                            ds.Tables[0].Columns[i].ColumnName = ds.Tables[0].Columns[i].ColumnName.ToUpper();
                        }
                        //Common.TransDataToXML.TransformDTIntoXML(ds.Tables[0], "d://ReportForm.xml");
                        return ds.Tables[0];
                    }
                    else
                    {
                        return new DataTable();
                    }
                }
                else
                {
                    return new DataTable();
                }
                #endregion
            }
            catch
            {
                return new DataTable();
            }
        }


        public int Add(ZhiFang.ReportFormQueryPrint.Model.ReportMicro t)
        {
            throw new NotImplementedException();
        }

        public int Update(ZhiFang.ReportFormQueryPrint.Model.ReportMicro t)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(ZhiFang.ReportFormQueryPrint.Model.ReportMicro t)
        {
            throw new NotImplementedException();
        }

        internal DataTable GetReportMicroFullList(string FormNo)
        {
            throw new NotImplementedException();
        }

        public DataTable GetReportMicroGroupListForSTestType(string FormNo)
        {
            try
            {
                #region 执行存储过程
                if (FormNo != null && FormNo.Trim() != "")
                {

                    SqlParameter sp = new SqlParameter("@ReportFormID", SqlDbType.VarChar, 50);
                    sp.Value = FormNo;
                    DataSet ds = DbHelperSQL.RunProcedure("GetReportMicroGroupFullListForSTestType", new SqlParameter[] { sp }, "ReportMicFull");
                    ZhiFang.Common.Log.Log.Debug(ds.GetXml() + "@" + DbHelperSQL.connectionString);
                    if (ds.Tables.Count > 0)
                    {
                        ds.Tables[0].Columns.Add("DISPLAYID", typeof(string));
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            ds.Tables[0].Rows[i]["DISPLAYID"] = (i + 1).ToString();
                        }
                        for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                        {
                            ds.Tables[0].Columns[i].ColumnName = ds.Tables[0].Columns[i].ColumnName.ToUpper();
                        }
                        //Common.TransDataToXML.TransformDTIntoXML(ds.Tables[0], "d://ReportForm.xml");
                        return ds.Tables[0];
                    }
                    else
                    {
                        return new DataTable();
                    }
                }
                else
                {
                    return new DataTable();
                }
                #endregion
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataSet GetReportMicroFullByReportFormId(string reportformid)
        {
            if (reportformid == null || reportformid.Equals(""))
            {
                ZhiFang.Common.Log.Log.Debug("GetReportMicroFullByReportFormId.ReportFormID为空请检查传入参数！");
                return null;
            }

            StringBuilder sql = new StringBuilder();
            sql.Append("select * from ReportMicroFull where ReportPublicationID = '" + reportformid + "'");
            ZhiFang.Common.Log.Log.Debug("GetReportMicroFullByReportFormId.ReportFormFull.SQL:" + sql.ToString());
            return DbHelperSQL.Query(sql.ToString());
        }

        public int UpdateReportMicroFull(ReportMicroFull t)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE ReportMicroFull SET ");

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
            if (t.ItemNo != null && !t.ItemNo.Equals(""))
            {
                builder.Append(",ItemNo=" + "'" + t.ItemNo + "'");
            }
            if (t.ItemCname != null && !t.ItemCname.Equals(""))
            {
                builder.Append(",ItemCname=" + "'" + t.ItemCname + "'");
            }
            if (t.ItemEname != null && !t.ItemEname.Equals(""))
            {
                builder.Append(",ItemEname=" + "'" + t.ItemEname + "'");
            }
            if (t.ReportValue != null && !t.ReportValue.Equals(""))
            {
                builder.Append(",ReportValue=" + "'" + t.ReportValue + "'");
            }
            if (t.DescNo != null && !t.DescNo.Equals(""))
            {
                builder.Append(",DescNo=" + "'" + t.DescNo + "'");
            }
            if (t.DescName != null && !t.DescName.Equals(""))
            {
                builder.Append(",DescName=" + "'" + t.DescName + "'");
            }
            if (t.MicroStepName != null && !t.MicroStepName.Equals(""))
            {
                builder.Append(",MicroStepName=" + "'" + t.MicroStepName + "'");
            }
            if (t.MicroName != null && !t.MicroName.Equals(""))
            {
                builder.Append(",MicroName=" + "'" + t.MicroName + "'");
            }
            if (t.MicroEame != null && !t.MicroEame.Equals(""))
            {
                builder.Append(",MicroEame=" + "'" + t.MicroEame + "'");
            }
            if (t.MicroDesc != null && !t.MicroDesc.Equals(""))
            {
                builder.Append(",MicroDesc=" + "'" + t.MicroDesc + "'");
            }

            if (t.MicroResultDesc != null && !t.MicroResultDesc.Equals(""))
            {
                builder.Append(",MicroResultDesc=" + "'" + t.MicroResultDesc + "'");
            }
            if (t.ItemDesc != null && !t.ItemDesc.Equals(""))
            {
                builder.Append(",ItemDesc=" + "'" + t.ItemDesc + "'");
            }
            if (t.AntiName != null && !t.AntiName.Equals(""))
            {
                builder.Append(",AntiName=" + "'" + t.AntiName + "'");
            }
            if (t.AntiEName != null && !t.AntiEName.Equals(""))
            {
                builder.Append(",AntiEName=" + "'" + t.AntiEName + "'");
            }
            if (t.Suscept != null && !t.Suscept.Equals(""))
            {
                builder.Append(",Suscept=" + "'" + t.Suscept + "'");
            }

            if (t.SusQuan != null && !t.SusQuan.Equals(""))
            {
                builder.Append(",SusQuan=" + "'" + t.SusQuan + "'");
            }

            if (t.SusDesc != null && !t.SusDesc.Equals(""))
            {
                builder.Append(",SusDesc=" + "'" + t.SusDesc + "'");
            }

            if (t.AntiUnit != null && !t.AntiUnit.Equals(""))
            {
                builder.Append(",AntiUnit=" + "'" + t.AntiUnit + "'");
            }

            if (t.RefRange != null && !t.RefRange.Equals(""))
            {
                builder.Append(",RefRange=" + "'" + t.RefRange + "'");
            }

            if (t.ResultState != null && !t.ResultState.Equals(""))
            {
                builder.Append(",ResultState=" + "'" + t.ResultState + "'");
            }

            if (t.EquipName != null && !t.EquipName.Equals(""))
            {
                builder.Append(",EquipName=" + "'" + t.EquipName + "'");
            }

            if (t.CheckType != null && !t.CheckType.Equals(""))
            {
                builder.Append(",CheckType=" + "'" + t.CheckType + "'");
            }
            if (t.PYJDF1 != null && !t.PYJDF1.Equals(""))
            {
                builder.Append(",PYJDF1=" + "'" + t.PYJDF1 + "'");
            }
            if (t.PYJDF2 != null && !t.PYJDF2.Equals(""))
            {
                builder.Append(",PYJDF2=" + "'" + t.PYJDF2 + "'");
            }
            if (t.PYJDF3 != null && !t.PYJDF3.Equals(""))
            {
                builder.Append(",PYJDF3=" + "'" + t.PYJDF3 + "'");
            }
            if (t.PYJDF4 != null && !t.PYJDF4.Equals(""))
            {
                builder.Append(",PYJDF4=" + "'" + t.PYJDF4 + "'");
            }
            if (t.PYJDF5 != null && !t.PYJDF5.Equals(""))
            {
                builder.Append(",PYJDF5=" + "'" + t.PYJDF5 + "'");
            }
            if (t.PYJDF6 != null && !t.PYJDF6.Equals(""))
            {
                builder.Append(",PYJDF6=" + "'" + t.PYJDF6 + "'");
            }
            if (t.PYJDF7 != null && !t.PYJDF7.Equals(""))
            {
                builder.Append(",PYJDF7=" + "'" + t.PYJDF7 + "'");
            }
            if (t.PYJDF8 != null && !t.PYJDF8.Equals(""))
            {
                builder.Append(",PYJDF8=" + "'" + t.PYJDF8 + "'");
            }
            if (t.PYJDF9 != null && !t.PYJDF9.Equals(""))
            {
                builder.Append(",PYJDF9=" + "'" + t.PYJDF9 + "'");
            }
            if (t.PYJDF10 != null && !t.PYJDF10.Equals(""))
            {
                builder.Append(",PYJDF10=" + "'" + t.PYJDF10 + "'");
            }
            if (t.PYJDF11 != null && !t.PYJDF11.Equals(""))
            {
                builder.Append(",PYJDF11=" + "'" + t.PYJDF11 + "'");
            }
            if (t.PYJDF12 != null && !t.PYJDF12.Equals(""))
            {
                builder.Append(",PYJDF12=" + "'" + t.PYJDF12 + "'");
            }
            if (t.PYJDF13 != null && !t.PYJDF13.Equals(""))
            {
                builder.Append(",PYJDF13=" + "'" + t.PYJDF13 + "'");
            }
            if (t.PYJDF14 != null && !t.PYJDF14.Equals(""))
            {
                builder.Append(",PYJDF14=" + "'" + t.PYJDF14 + "'");
            }
            if (t.PYJDF15 != null && !t.PYJDF15.Equals(""))
            {
                builder.Append(",PYJDF15=" + "'" + t.PYJDF15 + "'");
            }
            if (t.PYJDF16 != null && !t.PYJDF16.Equals(""))
            {
                builder.Append(",PYJDF16=" + "'" + t.PYJDF16 + "'");
            }
            if (t.PYJDF17 != null && !t.PYJDF17.Equals(""))
            {
                builder.Append(",PYJDF17=" + "'" + t.PYJDF17 + "'");
            }
            if (t.PYJDF18 != null && !t.PYJDF18.Equals(""))
            {
                builder.Append(",PYJDF18=" + "'" + t.PYJDF18 + "'");
            }
            if (t.PYJDF19 != null && !t.PYJDF19.Equals(""))
            {
                builder.Append(",PYJDF19=" + "'" + t.PYJDF19 + "'");
            }
            if (t.PYJDF20 != null && !t.PYJDF20.Equals(""))
            {
                builder.Append(",PYJDF20=" + "'" + t.PYJDF20 + "'");
            }
            if (t.TPF1 != null && !t.TPF1.Equals(""))
            {
                builder.Append(",TPF1=" + "'" + t.TPF1 + "'");
            }
            if (t.TPF2 != null && !t.TPF2.Equals(""))
            {
                builder.Append(",TPF2=" + "'" + t.TPF2 + "'");
            }
            if (t.TPF3 != null && !t.TPF3.Equals(""))
            {
                builder.Append(",TPF3=" + "'" + t.TPF3 + "'");
            }
            if (t.TPF4 != null && !t.TPF4.Equals(""))
            {
                builder.Append(",TPF4=" + "'" + t.TPF4 + "'");
            }
            if (t.TPF5 != null && !t.TPF5.Equals(""))
            {
                builder.Append(",TPF5=" + "'" + t.TPF5 + "'");
            }
            if (t.TPF6 != null && !t.TPF6.Equals(""))
            {
                builder.Append(",TPF6=" + "'" + t.TPF6 + "'");
            }
            if (t.TPF7 != null && !t.TPF7.Equals(""))
            {
                builder.Append(",TPF7=" + "'" + t.TPF7 + "'");
            }
            if (t.TPF8 != null && !t.TPF8.Equals(""))
            {
                builder.Append(",TPF8=" + "'" + t.TPF8 + "'");
            }
            if (t.TPF9 != null && !t.TPF9.Equals(""))
            {
                builder.Append(",TPF9=" + "'" + t.TPF9 + "'");
            }
            if (t.TPF10 != null && !t.TPF10.Equals(""))
            {
                builder.Append(",TPF10=" + "'" + t.TPF10 + "'");
            }
            if (t.TestComment != null && !t.TestComment.Equals(""))
            {
                builder.Append(",TestComment=" + "'" + t.TestComment + "'");
            }

            builder.Append(" WHERE ReportPublicationID =" + t.ReportPublicationID + " and OrderNo = " + t.OrderNo);
            ZhiFang.Common.Log.Log.Debug("UpdateReportItemFull:" + builder.ToString());
            return DbHelperSQL.ExecuteSql(builder.ToString());
        }
    }
}

