using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.DBUtility;
namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.ReportCenter
{
	/// <summary>
	/// 数据访问类:ReportItemFull
	/// </summary>
	public class ReportDrugGene : IDReportDrugGene
    {
		public ReportDrugGene()
		{}

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ItemNo", "ReportDrugGeneFull"); 
		}
        

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM ReportDrugGeneQueryDataSource ");
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
			strSql.Append(" FROM ReportDrugGeneQueryDataSource ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}		

		

        public bool Exists(int ItemNo, string FormNo)
        {
            throw new NotImplementedException();
        }

        public int Delete(int ItemNo, string FormNo)
        {
            throw new NotImplementedException();
        }

        public ZhiFang.ReportFormQueryPrint.Model.ReportDrugGene GetModel(int ItemNo, string FormNo)
        {
            throw new NotImplementedException();
        }

        public DataTable GetReportItemList(string FormNo)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * ");
                strSql.Append(" FROM ReportDrugGeneQueryDataSource ");
                strSql.Append(" where ReportPublicationID='" + FormNo + "' ");
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
                ZhiFang.Common.Log.Log.Debug("ReportDrugGeneFull:" + ex.ToString());
                return new DataTable();
            }
        }

        public DataTable GetReportItemFullList(string FormNo)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * ");
                strSql.Append(" FROM ReportDrugGeneQueryDataSource ");
                strSql.Append(" where ReportPublicationID='" + FormNo + "' ");
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
                ZhiFang.Common.Log.Log.Debug("ReportDrugGeneFull:" + ex.ToString());
                return new DataTable();
            }
        }


        public int Add(ZhiFang.ReportFormQueryPrint.Model.ReportDrugGene entity)
        {
            throw new NotImplementedException();
        }

        public int Update(ZhiFang.ReportFormQueryPrint.Model.ReportDrugGene entity)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(ZhiFang.ReportFormQueryPrint.Model.ReportDrugGene entity)
        {
            throw new NotImplementedException();
        }
    }
}

