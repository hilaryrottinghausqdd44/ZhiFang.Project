using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.DBUtility;
using ZhiFang.ReportFormQueryPrint.Model;
using System.Collections.Generic;

namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.ReportCenter
{
	/// <summary>
	/// 数据访问类:ReportItemFull
	/// </summary>
	public class ReportItem:IDReportItem
	{
		public ReportItem()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ItemNo", "ReportItemFull"); 
		}
        

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM ReportItemQueryDataSource ");
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
			strSql.Append(" FROM ReportItemQueryDataSource ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}		

		#endregion  Method
		#region  MethodEx

		#endregion  MethodEx

        public bool Exists(int ItemNo, string FormNo)
        {
            throw new NotImplementedException();
        }

        public int Delete(int ItemNo, string FormNo)
        {
            throw new NotImplementedException();
        }

        public ZhiFang.ReportFormQueryPrint.Model.ReportItem GetModel(int ItemNo, string FormNo)
        {
            throw new NotImplementedException();
        }

        public DataTable GetReportItemList(string FormNo)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * ");
                strSql.Append(" FROM ReportItemQueryDataSource ");
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

        public DataTable GetReportItemFullList(string FormNo)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * ");
                strSql.Append(" FROM ReportItemQueryDataSource ");
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


        public int Add(ZhiFang.ReportFormQueryPrint.Model.ReportItem t)
        {
            throw new NotImplementedException();
        }

        public int Update(ZhiFang.ReportFormQueryPrint.Model.ReportItem t)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(ZhiFang.ReportFormQueryPrint.Model.ReportItem t)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 多项目历史对比
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        public DataTable GetReportItemCNameList(string FormNo)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select ItemCname,ItemValue,ItemUnit ");
                strSql.Append(" FROM ReportItemQueryDataSource ");
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

        public DataSet getTestItemItemDescByitem(string ItemNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select cname,itemDesc  ");
            strSql.Append(" FROM TestItem ");
            if (ItemNo.Trim() != "")
            {
                strSql.Append(" where ItemNo = " + ItemNo);
            }
            ZhiFang.Common.Log.Log.Debug(strSql.ToString() + "@" + DbHelperSQL.connectionString);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetReportItemFullByReportFormId(string reportFormId)
        {
            if (reportFormId == null || reportFormId.Equals(""))
            {
                ZhiFang.Common.Log.Log.Debug("GetReportItemFullByReportFormId.ReportFormID为空请检查传入参数！");
                return null;
            }

            StringBuilder sql = new StringBuilder();
            sql.Append("select * from ReportItemFull where ReportPublicationID = '" + reportFormId + "'");
            ZhiFang.Common.Log.Log.Debug("GetReportItemFullByReportFormId.ReportFormFull.SQL:" + sql.ToString());
            return DbHelperSQL.Query(sql.ToString());
        }

        public int UpdateReportItemFull(ReportItemFull t)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE ReportItemFull SET ");

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
            if (t.OrderNo != null && !t.OrderNo.Equals(""))
            {
                builder.Append(",OrderNo=" + "'" + t.OrderNo + "'");
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
            if (t.ReportValue != null && !t.ReportValue.Equals(""))
            {
                builder.Append(",ReportValue=" + "'" + t.ReportValue + "'");
            }
            if (t.ReportDesc != null && !t.ReportDesc.Equals(""))
            {
                builder.Append(",ReportDesc=" + "'" + t.ReportDesc + "'");
            }
            if (t.ItemValue != null && !t.ItemValue.Equals(""))
            {
                builder.Append(",ItemValue=" + "'" + t.ItemValue + "'");
            }
            if (t.ItemUnit != null && !t.ItemUnit.Equals(""))
            {
                builder.Append(",ItemUnit=" + "'" + t.ItemUnit + "'");
            }
            if (t.ResultStatus != null && !t.ResultStatus.Equals(""))
            {
                builder.Append(",ResultStatus=" + "'" + t.ResultStatus + "'");
            }
            if (t.RefRange != null && !t.RefRange.Equals(""))
            {
                builder.Append(",RefRange=" + "'" + t.RefRange + "'");
            }
            if (t.ItemDesc != null && !t.ItemDesc.Equals(""))
            {
                builder.Append(",ItemDesc=" + "'" + t.ItemDesc + "'");
            }
           
            if (t.ZDY1 != null && !t.ZDY1.Equals(""))
            {
                builder.Append(",ZDY1=" + "'" + t.ZDY1 + "'");
            }
            if (t.ZDY2 != null && !t.ZDY2.Equals(""))
            {
                builder.Append(",ZDY2=" + "'" + t.ZDY2 + "'");
            }
            if (t.ZDY3 != null && !t.ZDY3.Equals(""))
            {
                builder.Append(",ZDY3=" + "'" + t.ZDY3 + "'");
            }
            if (t.ZDY4 != null && !t.ZDY4.Equals(""))
            {
                builder.Append(",ZDY4=" + "'" + t.ZDY4 + "'");
            }
            if (t.ZDY5 != null && !t.ZDY5.Equals(""))
            {
                builder.Append(",ZDY5=" + "'" + t.ZDY5 + "'");
            }           
            builder.Append(" WHERE ReportPublicationID =" + t.ReportPublicationID +" and ItemNo = "+t.ItemNo);
            ZhiFang.Common.Log.Log.Debug("UpdateReportItemFull:" + builder.ToString());
            return DbHelperSQL.ExecuteSql(builder.ToString());
        }

        public DataSet GetReportItemList_DataSet(List<string> FormNo)
        {
            throw new NotImplementedException();
        }

        public DataTable GetReportItemFullListAndSort(string FormNo, List<string> sortFields)
        {

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * ");
                strSql.Append(" FROM ReportItemQueryDataSource ");
                strSql.Append(" where ReportFormID='" + FormNo + "' ");
                if (sortFields != null && sortFields.Count > 0)
                {
                    int size = sortFields.Count;
                    //获取第一个元素
                    string sortFieldsFirst = sortFields[0];
                    //将 字段名,排序方式 格式的string分开
                    string[] sortField = sortFieldsFirst.Split(new char[] { ',' });
                    //判断如果第一组字段格式错误，则不排序
                    if (sortField != null && sortField.Length > 1)
                    {
                        strSql.Append(" order by " + sortField[0] + " " + sortField[1]);
                        //如果需要多个字段排序，继续拼接
                        if (size > 1)
                        {
                            for (int i = 1; i < size; i++)
                            {
                                string[] sortFieldElse = sortFields[i].Split(new char[] { ',' });
                                if (sortFieldElse.Length > 1)
                                {
                                    strSql.Append("," + sortFieldElse[0] + " " + sortFieldElse[1]);
                                }
                                else
                                {
                                    int j = i + 1;
                                    ZhiFang.Common.Log.Log.Debug("第" + j + "组排序字段格式错误，请按提示格式拼接");
                                }
                            }
                        }
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug("排序字段格式错误，请按提示格式拼接");
                    }

                }
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
                ZhiFang.Common.Log.Log.Debug("GetReportItemFullListAndSort:" + ex.ToString());
                return new DataTable();
            }
        }

        public DataTable GetProcReportItemQueryDataSource(string FormNo, List<string> sortFields)
        {
            try
            {
                StringBuilder sortFieldsSql = new StringBuilder();
                if (sortFields != null && sortFields.Count > 0)
                {
                    int size = sortFields.Count;
                    //获取第一个元素
                    string sortFieldsFirst = sortFields[0];
                    //将 字段名,排序方式 格式的string分开
                    string[] sortField = sortFieldsFirst.Split(new char[] { ',' });
                    //判断如果第一组字段格式错误，则不排序
                    if (sortField != null && sortField.Length > 1)
                    {
                        sortFieldsSql.Append(sortField[0] + " " + sortField[1]);
                        //如果需要多个字段排序，继续拼接
                        if (size > 1)
                        {
                            for (int i = 1; i < size; i++)
                            {
                                string[] sortFieldElse = sortFields[i].Split(new char[] { ',' });
                                if (sortFieldElse.Length > 1)
                                {
                                    sortFieldsSql.Append("," + sortFieldElse[0] + " " + sortFieldElse[1]);
                                }
                                else
                                {
                                    int j = i + 1;
                                    ZhiFang.Common.Log.Log.Debug("第" + j + "组排序字段格式错误，请按提示格式拼接");
                                }
                            }
                        }
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug("排序字段格式错误，请按提示格式拼接");
                    }

                }
                SqlParameter sqlParameter = new SqlParameter("@ReportFormID", SqlDbType.VarChar, 50);
                SqlParameter sqlParameter1 = new SqlParameter("@SortFields", SqlDbType.VarChar, 1000);
                sqlParameter.Value = FormNo;
                sqlParameter1.Value = sortFieldsSql.ToString();
                DataSet dataSet = DbHelperSQL.RunProcedure("Proc_ReportItemQueryDataSource",new SqlParameter[] { sqlParameter, sqlParameter1}, "ReportItemFull");
                if (dataSet.Tables.Count > 0)
                {
                    dataSet.Tables[0].Columns.Add("DISPLAYID", typeof(string));
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        dataSet.Tables[0].Rows[i]["DISPLAYID"] = (i + 1).ToString();
                    }
                    for (int i = 0; i < dataSet.Tables[0].Columns.Count; i++)
                    {
                        dataSet.Tables[0].Columns[i].ColumnName = dataSet.Tables[0].Columns[i].ColumnName.ToUpper();
                    }

                    return dataSet.Tables[0];
                }
                else 
                {
                    return new DataTable();
                }
                
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("GetProcReportItemQueryDataSource:" + ex.ToString());
                return new DataTable();
            }
        }

        public int GetReportItemFullListWhereCount(string FormNo, string where)
        {
            int count = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ItemCname,ItemValue,ItemUnit ");
            strSql.Append(" FROM ReportItemQueryDataSource ");
            strSql.Append(" where ReportFormID='" + FormNo + "' ");
            if (!String.IsNullOrWhiteSpace(where))
            {
                strSql.Append(" and " + where);
            }
            ZhiFang.Common.Log.Log.Debug(strSql.ToString());
            var counto = DbHelperSQL.GetSingle(strSql.ToString());
            if (counto != null)
            {
                count = int.Parse(counto.ToString());
            }
            return count;
        }

        public DataSet GetReportItemListSort_DataSet(List<string> FormNo, string sortFields)
        {
            throw new NotImplementedException();
        }
    }
}

