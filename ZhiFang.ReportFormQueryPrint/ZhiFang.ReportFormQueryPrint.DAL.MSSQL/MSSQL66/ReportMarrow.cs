using System;
using System.Data;
using System.Text;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.DBUtility;
using System.Data.SqlClient;
using ZhiFang.ReportFormQueryPrint.Model;

namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66
{
	/// <summary>
	/// 数据访问类ReportMarrow。
	/// </summary>
    public class ReportMarrow : IDReportMarrow
	{
		public ReportMarrow()
		{}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("FormNo", "ReportMarrow"); 
		}


		/// <summary>
		/// 是否存在该记录
		/// </summary>
        public bool Exists(string FormNo, string ItemNo)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from ReportMarrow");
			strSql.Append(" where FormNo="+FormNo+" and ItemNo="+ItemNo+" ");
			return DbHelperSQL.Exists(strSql.ToString());
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Model.ReportMarrow model)
		{
			StringBuilder strSql=new StringBuilder();
			StringBuilder strSql1=new StringBuilder();
			StringBuilder strSql2=new StringBuilder();
			if (model.ParItemNo != null)
			{
				strSql1.Append("ParItemNo,");
				strSql2.Append(""+model.ParItemNo+",");
			}
			if (model.ItemNo != null)
			{
				strSql1.Append("ItemNo,");
				strSql2.Append(""+model.ItemNo+",");
			}
			if (model.BloodNum != null)
			{
				strSql1.Append("BloodNum,");
				strSql2.Append(""+model.BloodNum+",");
			}
			if (model.BloodPercent != null)
			{
				strSql1.Append("BloodPercent,");
				strSql2.Append(""+model.BloodPercent+",");
			}
			if (model.MarrowNum != null)
			{
				strSql1.Append("MarrowNum,");
				strSql2.Append(""+model.MarrowNum+",");
			}
			if (model.MarrowPercent != null)
			{
				strSql1.Append("MarrowPercent,");
				strSql2.Append(""+model.MarrowPercent+",");
			}
			if (model.BloodDesc != null)
			{
				strSql1.Append("BloodDesc,");
				strSql2.Append("'"+model.BloodDesc+"',");
			}
			if (model.MarrowDesc != null)
			{
				strSql1.Append("MarrowDesc,");
				strSql2.Append("'"+model.MarrowDesc+"',");
			}
			if (model.StatusNo != null)
			{
				strSql1.Append("StatusNo,");
				strSql2.Append(""+model.StatusNo+",");
			}
			if (model.RefRange != null)
			{
				strSql1.Append("RefRange,");
				strSql2.Append("'"+model.RefRange+"',");
			}
			if (model.EquipNo != null)
			{
				strSql1.Append("EquipNo,");
				strSql2.Append(""+model.EquipNo+",");
			}
			if (model.IsCale != null)
			{
				strSql1.Append("IsCale,");
				strSql2.Append(""+model.IsCale+",");
			}
			if (model.Modified != null)
			{
				strSql1.Append("Modified,");
				strSql2.Append(""+model.Modified+",");
			}
			if (model.ItemDate != null)
			{
				strSql1.Append("ItemDate,");
				strSql2.Append("'"+model.ItemDate+"',");
			}
			if (model.ItemTime != null)
			{
				strSql1.Append("ItemTime,");
				strSql2.Append("'"+model.ItemTime+"',");
			}
			if (model.IsMatch != null)
			{
				strSql1.Append("IsMatch,");
				strSql2.Append(""+model.IsMatch+",");
			}
			if (model.ResultStatus != null)
			{
				strSql1.Append("ResultStatus,");
				strSql2.Append("'"+model.ResultStatus+"',");
			}
			if (model.FormNo != null)
			{
				strSql1.Append("FormNo,");
				strSql2.Append(""+model.FormNo+",");
			}
			if (model.ItemName != null)
			{
				strSql1.Append("ItemName,");
				strSql2.Append("'"+model.ItemName+"',");
			}
			strSql.Append("insert into ReportMarrow(");
			strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
			strSql.Append(")");
			strSql.Append(" values (");
			strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
			strSql.Append(")");
			return DbHelperSQL.ExecuteSql(strSql.ToString());
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public int Update(Model.ReportMarrow model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update ReportMarrow set ");
			if (model.ParItemNo != null)
			{
				strSql.Append("ParItemNo="+model.ParItemNo+",");
			}
			if (model.BloodNum != null)
			{
				strSql.Append("BloodNum="+model.BloodNum+",");
			}
			if (model.BloodPercent != null)
			{
				strSql.Append("BloodPercent="+model.BloodPercent+",");
			}
			if (model.MarrowNum != null)
			{
				strSql.Append("MarrowNum="+model.MarrowNum+",");
			}
			if (model.MarrowPercent != null)
			{
				strSql.Append("MarrowPercent="+model.MarrowPercent+",");
			}
			if (model.BloodDesc != null)
			{
				strSql.Append("BloodDesc='"+model.BloodDesc+"',");
			}
			if (model.MarrowDesc != null)
			{
				strSql.Append("MarrowDesc='"+model.MarrowDesc+"',");
			}
			if (model.StatusNo != null)
			{
				strSql.Append("StatusNo="+model.StatusNo+",");
			}
			if (model.RefRange != null)
			{
				strSql.Append("RefRange='"+model.RefRange+"',");
			}
			if (model.EquipNo != null)
			{
				strSql.Append("EquipNo="+model.EquipNo+",");
			}
			if (model.IsCale != null)
			{
				strSql.Append("IsCale="+model.IsCale+",");
			}
			if (model.Modified != null)
			{
				strSql.Append("Modified="+model.Modified+",");
			}
			if (model.ItemDate != null)
			{
				strSql.Append("ItemDate='"+model.ItemDate+"',");
			}
			if (model.ItemTime != null)
			{
				strSql.Append("ItemTime='"+model.ItemTime+"',");
			}
			if (model.IsMatch != null)
			{
				strSql.Append("IsMatch="+model.IsMatch+",");
			}
			if (model.ResultStatus != null)
			{
				strSql.Append("ResultStatus='"+model.ResultStatus+"',");
			}
			if (model.ItemName != null)
			{
				strSql.Append("ItemName='"+model.ItemName+"',");
			}
			int n = strSql.ToString().LastIndexOf(",");
			strSql.Remove(n, 1);
			strSql.Append(" where FormNo="+ model.FormNo+" and ItemNo="+ model.ItemNo+" ");
			return DbHelperSQL.ExecuteSql(strSql.ToString());
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
        public int Delete(string FormNo, string ItemNo)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ReportMarrow ");
			strSql.Append(" where FormNo="+FormNo+" and ItemNo="+ItemNo+" " );
			return DbHelperSQL.ExecuteSql(strSql.ToString());
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public Model.ReportMarrow GetModel(string FormNo, string ItemNo)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1  ");
			strSql.Append(" ParItemNo,ItemNo,BloodNum,BloodPercent,MarrowNum,MarrowPercent,BloodDesc,MarrowDesc,StatusNo,RefRange,EquipNo,IsCale,Modified,ItemDate,ItemTime,IsMatch,ResultStatus,FormNo,ItemName ");
			strSql.Append(" from ReportMarrow ");
			strSql.Append(" where FormNo="+FormNo+" and ItemNo="+ItemNo+" " );
			Model.ReportMarrow model=new Model.ReportMarrow();
			DataSet ds=DbHelperSQL.Query(strSql.ToString());
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ParItemNo"].ToString()!="")
				{
					model.ParItemNo=int.Parse(ds.Tables[0].Rows[0]["ParItemNo"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ItemNo"].ToString()!="")
				{
					model.ItemNo=int.Parse(ds.Tables[0].Rows[0]["ItemNo"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BloodNum"].ToString()!="")
				{
					model.BloodNum=int.Parse(ds.Tables[0].Rows[0]["BloodNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BloodPercent"].ToString()!="")
				{
					model.BloodPercent=decimal.Parse(ds.Tables[0].Rows[0]["BloodPercent"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MarrowNum"].ToString()!="")
				{
					model.MarrowNum=int.Parse(ds.Tables[0].Rows[0]["MarrowNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MarrowPercent"].ToString()!="")
				{
					model.MarrowPercent=decimal.Parse(ds.Tables[0].Rows[0]["MarrowPercent"].ToString());
				}
				model.BloodDesc=ds.Tables[0].Rows[0]["BloodDesc"].ToString();
				model.MarrowDesc=ds.Tables[0].Rows[0]["MarrowDesc"].ToString();
				if(ds.Tables[0].Rows[0]["StatusNo"].ToString()!="")
				{
					model.StatusNo=int.Parse(ds.Tables[0].Rows[0]["StatusNo"].ToString());
				}
				model.RefRange=ds.Tables[0].Rows[0]["RefRange"].ToString();
				if(ds.Tables[0].Rows[0]["EquipNo"].ToString()!="")
				{
					model.EquipNo=int.Parse(ds.Tables[0].Rows[0]["EquipNo"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsCale"].ToString()!="")
				{
					model.IsCale=int.Parse(ds.Tables[0].Rows[0]["IsCale"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Modified"].ToString()!="")
				{
					model.Modified=int.Parse(ds.Tables[0].Rows[0]["Modified"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ItemDate"].ToString()!="")
				{
					model.ItemDate=DateTime.Parse(ds.Tables[0].Rows[0]["ItemDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ItemTime"].ToString()!="")
				{
					model.ItemTime=DateTime.Parse(ds.Tables[0].Rows[0]["ItemTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsMatch"].ToString()!="")
				{
					model.IsMatch=int.Parse(ds.Tables[0].Rows[0]["IsMatch"].ToString());
				}
				model.ResultStatus=ds.Tables[0].Rows[0]["ResultStatus"].ToString();
				if(ds.Tables[0].Rows[0]["FormNo"].ToString()!="")
				{
					model.FormNo=ds.Tables[0].Rows[0]["FormNo"].ToString();
				}
				model.ItemName=ds.Tables[0].Rows[0]["ItemName"].ToString();
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
			strSql.Append("select ParItemNo,ItemNo,BloodNum,BloodPercent,MarrowNum,MarrowPercent,BloodDesc,MarrowDesc,StatusNo,RefRange,EquipNo,IsCale,Modified,ItemDate,ItemTime,IsMatch,ResultStatus,FormNo,ItemName ");
			strSql.Append(" FROM ReportMarrow ");
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
			strSql.Append(" ParItemNo,ItemNo,BloodNum,BloodPercent,MarrowNum,MarrowPercent,BloodDesc,MarrowDesc,StatusNo,RefRange,EquipNo,IsCale,Modified,ItemDate,ItemTime,IsMatch,ResultStatus,FormNo,ItemName ");
			strSql.Append(" FROM ReportMarrow ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		*/

        /// <summary>
        /// 根据FormNo返回ReportForm包含的ReportMarrow列表
        /// </summary>
        /// <param name="FormNo">FormNo</param>
        /// <returns></returns>
        public DataTable GetReportMarrowList(string FormNo)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * ");
                strSql.Append(" FROM reportmarrowitemdatasource ");
                if (FormNo.Trim() != "")
                {
                    strSql.Append(" where FormNo=" + FormNo);
                }
                ZhiFang.Common.Log.Log.Debug(strSql.ToString());
                return DbHelperSQL.Query(strSql.ToString()).Tables[0];
            }
            catch(Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.ToString());
                return new DataTable();
            }
        }

		#endregion  成员方法

        #region IDataBase<ReportMarrow> 成员

        public DataSet GetList(Model.ReportMarrow model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from  ReportMarrowQueryDataSource where 1=1  ");
            if (model != null)
            {
                if (model.ParItemNo != null)
                {
                    strSql.Append(" and ParItemNo=" + model.ParItemNo + " ");
                }
                if (model.BloodNum != null)
                {
                    strSql.Append(" and BloodNum=" + model.BloodNum + " ");
                }
                if (model.BloodPercent != null)
                {
                    strSql.Append(" and BloodPercent=" + model.BloodPercent + " ");
                }
                if (model.MarrowNum != null)
                {
                    strSql.Append(" and MarrowNum=" + model.MarrowNum + " ");
                }
                if (model.MarrowPercent != null)
                {
                    strSql.Append(" and MarrowPercent=" + model.MarrowPercent + " ");
                }
                if (model.BloodDesc != null)
                {
                    strSql.Append(" and BloodDesc='" + model.BloodDesc + "' ");
                }
                if (model.MarrowDesc != null)
                {
                    strSql.Append(" and MarrowDesc='" + model.MarrowDesc + "' ");
                }
                if (model.StatusNo != null)
                {
                    strSql.Append(" and StatusNo=" + model.StatusNo + " ");
                }
                if (model.RefRange != null)
                {
                    strSql.Append(" and RefRange='" + model.RefRange + "' ");
                }
                if (model.EquipNo != null)
                {
                    strSql.Append(" and EquipNo=" + model.EquipNo + " ");
                }
                if (model.IsCale != null)
                {
                    strSql.Append(" and IsCale=" + model.IsCale + " ");
                }
                if (model.Modified != null)
                {
                    strSql.Append(" and Modified=" + model.Modified + " ");
                }
                if (model.ItemDate != null)
                {
                    strSql.Append(" and ItemDate='" + model.ItemDate + "' ");
                }
                if (model.ItemTime != null)
                {
                    strSql.Append(" and ItemTime='" + model.ItemTime + "' ");
                }
                if (model.IsMatch != null)
                {
                    strSql.Append(" and IsMatch=" + model.IsMatch + " ");
                }
                if (model.ResultStatus != null)
                {
                    strSql.Append(" and ResultStatus='" + model.ResultStatus + "' ");
                }
                if (model.ItemName != null)
                {
                    strSql.Append(" and ItemName='" + model.ItemName + "' ");
                }
                if (model.FormNo != null)
                {
                    strSql.Append(" and FormNo='" + model.FormNo + "' ");

                }
                if (model.ItemNo != null)
                {
                    strSql.Append(" and ItemNo='" + model.ItemNo + "' ");
                }
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion

        #region IDReportMarrow 成员


        public DataTable GetReportMarrowItemList(string FormNo)
        {
            #region 执行存储过程
            //SqlParameter sp = new SqlParameter("@FormNo", SqlDbType.VarChar, 50);
            //sp.Value = FormNo;
            //DataSet ds = DbHelperSQL.RunProcedure("GetReportMarrowItemFullList", new SqlParameter[] { sp }, "ReportMarrowItemFull");
            //if (ds.Tables.Count > 0)
            //{
            //    ds.Tables[0].Columns.Add("DISPLAYID", typeof(string));
            //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //    {
            //        ds.Tables[0].Rows[i]["DISPLAYID"] = (i + 1).ToString();
            //    }
            //    for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            //    {
            //        ds.Tables[0].Columns[i].ColumnName = ds.Tables[0].Columns[i].ColumnName.ToUpper();
            //    }
            //    //Common.TransDataToXML.TransformDTIntoXML(ds.Tables[0], "d://ReportForm.xml");
            //    return ds.Tables[0];
            //}
            //else
            //{
            //    return new DataTable();
            //}
            #endregion

            if (FormNo == null || FormNo == "") return new DataTable();

            try
            {
                #region 执行视图
                ZhiFang.Common.Log.Log.Debug("MSSSQL6.6.GetReportMarrowItemList:" + FormNo);
                string[] p = FormNo.Split(';');
                string receivedate = p[0];
                string sectionno = p[1];
                string testtypeno = p[2];
                string sampleno = p[3];
                var sql = "select * from ReportMarrowQueryDataSource where ReceiveDate='" + receivedate + "' and SectionNo=" + sectionno + " and TestTypeNo=" + testtypeno + " and SampleNo='" + sampleno + "' ";
                DataSet ds = DbHelperSQL.Query(sql);
                ZhiFang.Common.Log.Log.Debug("MSSSQL6.6.GetReportMarrowItemList:sql=" + sql);
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                    {
                        ds.Tables[0].Columns[i].ColumnName = ds.Tables[0].Columns[i].ColumnName.ToUpper();
                    }
                    return ds.Tables[0];
                }
                else
                {
                    return new DataTable();
                }
                #endregion
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("MSSSQL6.6.GetReportMarrowItemList:" + e);
                return new DataTable();
            }
        }


        public DataSet GetReportMarrowFullList(string FormNo)
        {
            #region 执行存储过程
            //SqlParameter sp = new SqlParameter("@FormNo", SqlDbType.VarChar, 50);
            //sp.Value = FormNo;
            //DataSet ds = DbHelperSQL.RunProcedure("GetReportMarrowFullList", new SqlParameter[] { sp }, "ReportMarrowFull");
            //if (ds.Tables.Count > 0)
            //{
            //    ds.Tables[0].Columns.Add("DISPLAYID", typeof(string));
            //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //    {
            //        ds.Tables[0].Rows[i]["DISPLAYID"] = (i + 1).ToString();
            //    }
            //    for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            //    {
            //        ds.Tables[0].Columns[i].ColumnName = ds.Tables[0].Columns[i].ColumnName.ToUpper();
            //    }
            //    //Common.TransDataToXML.TransformDTIntoXML(ds.Tables[0], "d://ReportForm.xml");
            //    return ds;
            //}
            //else
            //{
            //    return new DataSet();
            //}
            #endregion

            if (FormNo == null || FormNo == "") return new DataSet();

            try
            {
                #region 执行视图
                ZhiFang.Common.Log.Log.Debug("MSSSQL6.6.GetReportMarrowFullList:" + FormNo);
                string[] p = FormNo.Split(';');
                string receivedate = p[0];
                string sectionno = p[1];
                string testtypeno = p[2];
                string sampleno = p[3];
                var sql = "select * from ReportMarrowQueryDataSource where ReceiveDate='" + receivedate + "' and SectionNo=" + sectionno + " and TestTypeNo=" + testtypeno + " and SampleNo='" + sampleno + "' ";
                DataSet ds = DbHelperSQL.Query(sql);
                ZhiFang.Common.Log.Log.Debug("MSSSQL6.6.GetReportMarrowFullList:sql=" + sql);
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                    {
                        ds.Tables[0].Columns[i].ColumnName = ds.Tables[0].Columns[i].ColumnName.ToUpper();
                    }
                    return ds;
                }
                else
                {
                    return new DataSet();
                }
                #endregion
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("MSSSQL6.6.GetReportMarrowFullList:" + e);
                return new DataSet();
            }
        }

        public DataSet GetReportMarrowFullByReportFormID(string reportformid)
        {
            throw new NotImplementedException();
        }

        public int UpdateReportMarrowFull(ReportMarrowFull model)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

