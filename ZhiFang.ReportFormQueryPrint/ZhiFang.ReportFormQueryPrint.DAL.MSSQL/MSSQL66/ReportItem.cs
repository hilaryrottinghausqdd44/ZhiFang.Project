using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.DBUtility;
using System.Data;
using System.Data.SqlClient;
using ZhiFang.ReportFormQueryPrint.Model;

namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66
{
	/// <summary>
	/// 数据访问类ReportItem。
	/// </summary>
    public class ReportItem : IDReportItem
	{
		public ReportItem()
		{}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return -1; 
		}


		/// <summary>
		/// 是否存在该记录
		/// </summary>
        public bool Exists(int ItemNo, string FormNo)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from ReportItem");
            string[] p = FormNo.Split(';');
            if (p.Length >= 4)
            {
                strSql.Append(" where ReceiveDate='" + p[0] + "' and SectionNo=" + p[1] + " and TestTypeNo=" + p[2] + " and SampleNo='" + p[3] + "' and ItemNo=" + ItemNo + " ");
            }
            else
            {
                strSql.Append(" where 1=2 ");
            }
            ZhiFang.Common.Log.Log.Debug(strSql.ToString() + "@" + DbHelperSQL.connectionString);
            //strSql.Append(" where ReceiveDate='"+ReceiveDate+"' and SectionNo="+SectionNo+" and TestTypeNo="+TestTypeNo+" and SampleNo='"+SampleNo+"' and ItemNo="+ItemNo+" ");
			return DbHelperSQL.Exists(strSql.ToString());
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Model.ReportItem model)
		{
			StringBuilder strSql=new StringBuilder();
			StringBuilder strSql1=new StringBuilder();
			StringBuilder strSql2=new StringBuilder();
			if (model.ReceiveDate != null)
			{
				strSql1.Append("ReceiveDate,");
				strSql2.Append("'"+model.ReceiveDate+"',");
			}
			if (model.SectionNo != null)
			{
				strSql1.Append("SectionNo,");
				strSql2.Append(""+model.SectionNo+",");
			}
			if (model.TestTypeNo != null)
			{
				strSql1.Append("TestTypeNo,");
				strSql2.Append(""+model.TestTypeNo+",");
			}
			if (model.SampleNo != null)
			{
				strSql1.Append("SampleNo,");
				strSql2.Append("'"+model.SampleNo+"',");
			}
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
			if (model.OriginalValue != null)
			{
				strSql1.Append("OriginalValue,");
				strSql2.Append(""+model.OriginalValue+",");
			}
			if (model.ReportValue != null)
			{
				strSql1.Append("ReportValue,");
				strSql2.Append(""+model.ReportValue+",");
			}
			if (model.OriginalDesc != null)
			{
				strSql1.Append("OriginalDesc,");
				strSql2.Append("'"+model.OriginalDesc+"',");
			}
			if (model.ReportDesc != null)
			{
				strSql1.Append("ReportDesc,");
				strSql2.Append("'"+model.ReportDesc+"',");
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
			if (model.HisValue != null)
			{
				strSql1.Append("HisValue,");
				strSql2.Append("'"+model.HisValue+"',");
			}
			if (model.HisComp != null)
			{
				strSql1.Append("HisComp,");
				strSql2.Append("'"+model.HisComp+"',");
			}
			if (model.isReceive != null)
			{
				strSql1.Append("isReceive,");
				strSql2.Append(""+model.isReceive+",");
			}
			if (model.CountNodesItemSource != null)
			{
				strSql1.Append("CountNodesItemSource,");
				strSql2.Append("'"+model.CountNodesItemSource+"',");
			}
			if (model.Unit != null)
			{
				strSql1.Append("Unit,");
				strSql2.Append("'"+model.Unit+"',");
			}
			if (model.PlateNo != null)
			{
				strSql1.Append("PlateNo,");
				strSql2.Append(""+model.PlateNo+",");
			}
			if (model.PositionNo != null)
			{
				strSql1.Append("PositionNo,");
				strSql2.Append(""+model.PositionNo+",");
			}
			if (model.EquipCommMemo != null)
			{
				strSql1.Append("EquipCommMemo,");
				strSql2.Append("'"+model.EquipCommMemo+"',");
			}
			strSql.Append("insert into ReportItem(");
			strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
			strSql.Append(")");
			strSql.Append(" values (");
			strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
			strSql.Append(")");
            ZhiFang.Common.Log.Log.Debug(strSql.ToString() + "@" + DbHelperSQL.connectionString);
			return DbHelperSQL.ExecuteSql(strSql.ToString());
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public int Update(Model.ReportItem model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update ReportItem set ");
			if (model.ParItemNo != null)
			{
				strSql.Append("ParItemNo="+model.ParItemNo+",");
			}
			if (model.OriginalValue != null)
			{
				strSql.Append("OriginalValue="+model.OriginalValue+",");
			}
			if (model.ReportValue != null)
			{
				strSql.Append("ReportValue="+model.ReportValue+",");
			}
			if (model.OriginalDesc != null)
			{
				strSql.Append("OriginalDesc='"+model.OriginalDesc+"',");
			}
			if (model.ReportDesc != null)
			{
				strSql.Append("ReportDesc='"+model.ReportDesc+"',");
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
			if (model.HisValue != null)
			{
				strSql.Append("HisValue='"+model.HisValue+"',");
			}
			if (model.HisComp != null)
			{
				strSql.Append("HisComp='"+model.HisComp+"',");
			}
			if (model.isReceive != null)
			{
				strSql.Append("isReceive="+model.isReceive+",");
			}
			if (model.CountNodesItemSource != null)
			{
				strSql.Append("CountNodesItemSource='"+model.CountNodesItemSource+"',");
			}
			if (model.Unit != null)
			{
				strSql.Append("Unit='"+model.Unit+"',");
			}
			if (model.PlateNo != null)
			{
				strSql.Append("PlateNo="+model.PlateNo+",");
			}
			if (model.PositionNo != null)
			{
				strSql.Append("PositionNo="+model.PositionNo+",");
			}
			if (model.EquipCommMemo != null)
			{
				strSql.Append("EquipCommMemo='"+model.EquipCommMemo+"',");
			}
			int n = strSql.ToString().LastIndexOf(",");
			strSql.Remove(n, 1);
            string[] p = model.FormNo.Split(';');
            if (p.Length >= 4)
            {
                strSql.Append(" where ReceiveDate='" + p[0] + "' and SectionNo=" + p[1] + " and TestTypeNo=" + p[2] + " and SampleNo='" + p[3] + "' and ItemNo=" + model.ItemNo + " ");
            }
            else
            {
                strSql.Append(" where 1=2 ");
            }
            ZhiFang.Common.Log.Log.Debug(strSql.ToString() + "@" + DbHelperSQL.connectionString);
			//strSql.Append(" where ReceiveDate='"+ model.ReceiveDate+"' and SectionNo="+ model.SectionNo+" and TestTypeNo="+ model.TestTypeNo+" and SampleNo='"+ model.SampleNo+"' and ItemNo="+ model.ItemNo+" ");
			return DbHelperSQL.ExecuteSql(strSql.ToString());
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
        public int Delete(int ItemNo, string FormNo)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ReportItem ");
            string[] p = FormNo.Split(';');
            if (p.Length >= 4)
            {
                strSql.Append(" where ReceiveDate='" + p[0] + "' and SectionNo=" + p[1] + " and TestTypeNo=" + p[2] + " and SampleNo='" + p[3] + "' and ItemNo=" + ItemNo + " ");
            }
            else
            {
                strSql.Append(" where 1=2 ");
            }
            ZhiFang.Common.Log.Log.Debug(strSql.ToString() + "@" + DbHelperSQL.connectionString);
            //strSql.Append(" where ReceiveDate='"+ReceiveDate+"' and SectionNo="+SectionNo+" and TestTypeNo="+TestTypeNo+" and SampleNo='"+SampleNo+"' and ItemNo="+ItemNo+" " );
			return DbHelperSQL.ExecuteSql(strSql.ToString());
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public Model.ReportItem GetModel(int ItemNo, string FormNo)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1  ");
			strSql.Append(" Convert(varchar(10),ReceiveDate,21) as ReceiveDate,SectionNo,TestTypeNo,SampleNo,ParItemNo,ItemNo,OriginalValue,ReportValue,OriginalDesc,ReportDesc,StatusNo,RefRange,EquipNo,Modified,ItemDate,ItemTime,IsMatch,ResultStatus,HisValue,HisComp,isReceive,CountNodesItemSource,Unit,PlateNo,PositionNo,EquipCommMemo ");
			strSql.Append(" from ReportItem ");
            string[] p = FormNo.Split(';');
            if (p.Length >= 4)
            {
                strSql.Append(" where ReceiveDate='" + p[0] + "' and SectionNo=" + p[1] + " and TestTypeNo=" + p[2] + " and SampleNo='" + p[3] + "' and ItemNo=" + ItemNo + " ");
            }
            else
            {
                strSql.Append(" where 1=2 ");
            }
            ZhiFang.Common.Log.Log.Debug(strSql.ToString() + "@" + DbHelperSQL.connectionString);
            //strSql.Append(" where ReceiveDate='"+ReceiveDate+"' and SectionNo="+SectionNo+" and TestTypeNo="+TestTypeNo+" and SampleNo='"+SampleNo+"' and ItemNo="+ItemNo+" " );
			Model.ReportItem model=new Model.ReportItem();
			DataSet ds=DbHelperSQL.Query(strSql.ToString());
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ReceiveDate"].ToString()!="")
				{
					model.ReceiveDate=DateTime.Parse(ds.Tables[0].Rows[0]["ReceiveDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SectionNo"].ToString()!="")
				{
					model.SectionNo=int.Parse(ds.Tables[0].Rows[0]["SectionNo"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TestTypeNo"].ToString()!="")
				{
					model.TestTypeNo=int.Parse(ds.Tables[0].Rows[0]["TestTypeNo"].ToString());
				}
				model.SampleNo=ds.Tables[0].Rows[0]["SampleNo"].ToString();
				if(ds.Tables[0].Rows[0]["ParItemNo"].ToString()!="")
				{
					model.ParItemNo=int.Parse(ds.Tables[0].Rows[0]["ParItemNo"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ItemNo"].ToString()!="")
				{
					model.ItemNo=int.Parse(ds.Tables[0].Rows[0]["ItemNo"].ToString());
				}
				if(ds.Tables[0].Rows[0]["OriginalValue"].ToString()!="")
				{
					model.OriginalValue=decimal.Parse(ds.Tables[0].Rows[0]["OriginalValue"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ReportValue"].ToString()!="")
				{
					model.ReportValue=decimal.Parse(ds.Tables[0].Rows[0]["ReportValue"].ToString());
				}
				model.OriginalDesc=ds.Tables[0].Rows[0]["OriginalDesc"].ToString();
				model.ReportDesc=ds.Tables[0].Rows[0]["ReportDesc"].ToString();
				if(ds.Tables[0].Rows[0]["StatusNo"].ToString()!="")
				{
					model.StatusNo=int.Parse(ds.Tables[0].Rows[0]["StatusNo"].ToString());
				}
				model.RefRange=ds.Tables[0].Rows[0]["RefRange"].ToString();
				if(ds.Tables[0].Rows[0]["EquipNo"].ToString()!="")
				{
					model.EquipNo=int.Parse(ds.Tables[0].Rows[0]["EquipNo"].ToString());
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
				model.HisValue=ds.Tables[0].Rows[0]["HisValue"].ToString();
				model.HisComp=ds.Tables[0].Rows[0]["HisComp"].ToString();
				if(ds.Tables[0].Rows[0]["isReceive"].ToString()!="")
				{
					model.isReceive=int.Parse(ds.Tables[0].Rows[0]["isReceive"].ToString());
				}
				model.CountNodesItemSource=ds.Tables[0].Rows[0]["CountNodesItemSource"].ToString();
				model.Unit=ds.Tables[0].Rows[0]["Unit"].ToString();
				if(ds.Tables[0].Rows[0]["PlateNo"].ToString()!="")
				{
					model.PlateNo=int.Parse(ds.Tables[0].Rows[0]["PlateNo"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PositionNo"].ToString()!="")
				{
					model.PositionNo=int.Parse(ds.Tables[0].Rows[0]["PositionNo"].ToString());
				}
				model.EquipCommMemo=ds.Tables[0].Rows[0]["EquipCommMemo"].ToString();
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
			strSql.Append("select Convert(varchar(10),ReceiveDate,21) as ReceiveDate,SectionNo,TestTypeNo,SampleNo,ParItemNo,ItemNo,OriginalValue,ReportValue,OriginalDesc,ReportDesc,StatusNo,RefRange,EquipNo,Modified,ItemDate,ItemTime,IsMatch,ResultStatus,HisValue,HisComp,isReceive,CountNodesItemSource,Unit,PlateNo,PositionNo,EquipCommMemo ");
			strSql.Append(" FROM ReportItem ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            ZhiFang.Common.Log.Log.Debug(strSql.ToString() + "@" + DbHelperSQL.connectionString);
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
			strSql.Append(" Convert(varchar(10),ReceiveDate,21) as ReceiveDate,SectionNo,TestTypeNo,SampleNo,ParItemNo,ItemNo,OriginalValue,ReportValue,OriginalDesc,ReportDesc,StatusNo,RefRange,EquipNo,Modified,ItemDate,ItemTime,IsMatch,ResultStatus,HisValue,HisComp,isReceive,CountNodesItemSource,Unit,PlateNo,PositionNo,EquipCommMemo ");
			strSql.Append(" FROM ReportItem ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
            ZhiFang.Common.Log.Log.Debug(strSql.ToString() + "@" + DbHelperSQL.connectionString);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		*/

        public DataSet GetList(Model.ReportItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Convert(varchar(10),ReceiveDate,21) as ReceiveDate,SectionNo,TestTypeNo,SampleNo,ParItemNo,ItemNo,OriginalValue,ReportValue,OriginalDesc,ReportDesc,StatusNo,RefRange,EquipNo,Modified,ItemDate,ItemTime,IsMatch,ResultStatus,HisValue,HisComp,isReceive,CountNodesItemSource,Unit,PlateNo,PositionNo,EquipCommMemo ");
            strSql.Append(" FROM ReportItem where 1=1 ");
            if (model.ParItemNo != null)
            {
                strSql.Append(" and ParItemNo=" + model.ParItemNo + "");
            }
            if (model.OriginalValue != null)
            {
                strSql.Append(" and OriginalValue=" + model.OriginalValue + "");
            }
            if (model.ReportValue != null)
            {
                strSql.Append(" and ReportValue=" + model.ReportValue + "");
            }
            if (model.OriginalDesc != null)
            {
                strSql.Append(" and OriginalDesc='" + model.OriginalDesc + "'");
            }
            if (model.ReportDesc != null)
            {
                strSql.Append(" and ReportDesc='" + model.ReportDesc + "'");
            }
            if (model.StatusNo != null)
            {
                strSql.Append(" and StatusNo=" + model.StatusNo + "");
            }
            if (model.RefRange != null)
            {
                strSql.Append(" and RefRange='" + model.RefRange + "'");
            }
            if (model.EquipNo != null)
            {
                strSql.Append(" and EquipNo=" + model.EquipNo + "");
            }
            if (model.Modified != null)
            {
                strSql.Append(" and Modified=" + model.Modified + "");
            }
            if (model.ItemDate != null)
            {
                strSql.Append(" and ItemDate='" + model.ItemDate + "'");
            }
            if (model.ItemTime != null)
            {
                strSql.Append(" and ItemTime='" + model.ItemTime + "'");
            }
            if (model.IsMatch != null)
            {
                strSql.Append(" and IsMatch=" + model.IsMatch + "");
            }
            if (model.ResultStatus != null)
            {
                strSql.Append(" and ResultStatus='" + model.ResultStatus + "'");
            }
            if (model.HisValue != null)
            {
                strSql.Append(" and HisValue='" + model.HisValue + "'");
            }
            if (model.HisComp != null)
            {
                strSql.Append(" and HisComp='" + model.HisComp + "'");
            }
            if (model.isReceive != null)
            {
                strSql.Append(" and isReceive=" + model.isReceive + "");
            }
            if (model.CountNodesItemSource != null)
            {
                strSql.Append(" and CountNodesItemSource='" + model.CountNodesItemSource + "'");
            }
            if (model.Unit != null)
            {
                strSql.Append(" and Unit='" + model.Unit + "'");
            }
            if (model.PlateNo != null)
            {
                strSql.Append(" and PlateNo=" + model.PlateNo + "");
            }
            if (model.PositionNo != null)
            {
                strSql.Append(" and PositionNo=" + model.PositionNo + "");
            }
            if (model.EquipCommMemo != null)
            {
                strSql.Append(" and EquipCommMemo='" + model.EquipCommMemo + "'");
            }
            string[] p = model.FormNo.Split(';');
            if (p.Length >= 4)
            {
                strSql.Append(" and ReceiveDate='" + p[0] + "' and SectionNo=" + p[1] + " and TestTypeNo=" + p[2] + " and SampleNo='" + p[3] + "'  ");
            }
            else
            {
                strSql.Append(" and 1=2 ");
            }
            if (model.ItemNo != null)
            {
                strSql.Append(" and ItemNo=" + model.ItemNo + " ");
            }
            ZhiFang.Common.Log.Log.Debug(strSql.ToString() + "@" + DbHelperSQL.connectionString);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataTable GetReportItemList(string FormNo)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from ReportItemDataSource ");
                string[] p = FormNo.Split(';');
                if (p.Length >= 4)
                {
                    strSql.Append(" where ReceiveDate='" + p[0] + "' and SectionNo=" + p[1] + " and TestTypeNo=" + p[2] + " and SampleNo='" + p[3] + "'  ");
                }
                else
                {
                    strSql.Append(" where 1=2 ");
                }

                strSql.Append(" ORDER BY ti.disporder");
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
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetReportItemFullList(string FormNo)
        {
            try
            {
                #region 执行拼接脚本
                /*
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT     dbo.ReportForm.FormNo, TestItem_2.CName AS TestItemName, TestItem_2.StandardCode AS TestItemSName, dbo.ReportForm.ReceiveDate, ");
                strSql.Append("dbo.ReportForm.SectionNo, dbo.ReportForm.TestTypeNo, dbo.ReportForm.SampleNo, dbo.ReportItem.ParItemNo, dbo.ReportItem.ItemNo,  ");
                strSql.Append("dbo.ReportItem.OriginalValue, dbo.ReportItem.ReportValue, dbo.ReportItem.OriginalDesc, dbo.ReportItem.ReportDesc, dbo.ReportItem.StatusNo,  ");
                strSql.Append("dbo.ReportItem.EquipNo, dbo.ReportItem.Modified, dbo.ReportItem.RefRange, dbo.ReportItem.ItemDate, dbo.ReportItem.ItemTime,  ");
                strSql.Append("dbo.ReportItem.IsMatch, dbo.ReportItem.ResultStatus, CONVERT(varchar(10), dbo.ReportItem.ItemDate, 120) + ' ' + CONVERT(varchar(8),  ");
                strSql.Append("dbo.ReportItem.ItemTime, 114) AS TestItemDateTime, ISNULL(dbo.ReportItem.ReportDesc, '') + ISNULL(CONVERT(VARCHAR(50),  ");
                strSql.Append("dbo.ReportItem.ReportValue), '') AS ReportValueAll, TestItem_1.CName AS ParItemName, TestItem_1.ShortName AS ParItemSName,  ");
                strSql.Append("TestItem_2.DispOrder, TestItem_2.DispOrder AS ItemOrder, TestItem_2.Unit, dbo.ReportForm.SerialNo, dbo.ReportForm.zdy1,  ");
                strSql.Append("dbo.ReportForm.zdy2 AS OldSerialNlo, dbo.ReportForm.zdy3, dbo.ReportForm.zdy5, dbo.ReportForm.zdy4, TestItem_2.OrderNo AS HisOrderNo,  ");
                strSql.Append("dbo.ReportForm.Technician, dbo.ReportForm.Checker, CONVERT(varchar(10), dbo.ReportForm.CheckDate, 120) + ' ' + CONVERT(varchar(8),  ");
                strSql.Append("dbo.ReportForm.CheckTime, 114) AS checkdatetime, dbo.ReportForm.OldSerialNo AS zdy2 ");
                strSql.Append("FROM         dbo.ReportItem INNER JOIN ");
                strSql.Append("dbo.ReportForm ON dbo.ReportItem.FormNo = dbo.ReportForm.FormNo LEFT OUTER JOIN ");
                strSql.Append("dbo.TestItem AS TestItem_1 ON dbo.ReportItem.ParItemNo = TestItem_1.ItemNo LEFT OUTER JOIN ");
                strSql.Append("dbo.TestItem AS TestItem_2 ON dbo.ReportItem.ItemNo = TestItem_2.ItemNo ");
                strSql.Append("  where ReportItem.FormNo=" + FormNo + "  ");
                DataSet ds = DbHelperSQL.Query(strSql.ToString());
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                    {
                        ds.Tables[0].Columns[i].ColumnName = ds.Tables[0].Columns[i].ColumnName.ToUpper();
                    }
                    //Common.TransDataToXML.TransformDTIntoXML(ds.Tables[0], "d://ReportItem.xml");
                    return ds.Tables[0];
                }
                else
                {
                    return new DataTable();
                }
                */
                #endregion
                #region 执行存储过程
                //string[] p = FormNo.Split(';');
                //if (p.Length >= 4)
                //{

                //    SqlParameter sp = new SqlParameter("@ReceiveDate", SqlDbType.VarChar, 10);
                //    SqlParameter sp1 = new SqlParameter("@SectionNo", SqlDbType.VarChar, 50);
                //    SqlParameter sp2 = new SqlParameter("@TestTypeNo", SqlDbType.VarChar, 50);
                //    SqlParameter sp3 = new SqlParameter("@SampleNo", SqlDbType.VarChar, 50);
                //    sp.Value = p[0];
                //    sp1.Value = p[1];
                //    sp2.Value = p[2];
                //    sp3.Value = p[3];
                //    DataSet ds = DbHelperSQL.RunProcedure("GetReportItemFullList", new SqlParameter[] { sp,sp1,sp2,sp3 }, "ReportItemFull");
                //    if (ds.Tables.Count > 0)
                //    {
                //        ds.Tables[0].Columns.Add("DISPLAYID", typeof(string));
                //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //        {
                //            ds.Tables[0].Rows[i]["DISPLAYID"] = (i + 1).ToString();
                //        }
                //        for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                //        {
                //            ds.Tables[0].Columns[i].ColumnName = ds.Tables[0].Columns[i].ColumnName.ToUpper();
                //        }
                //        //Common.TransDataToXML.TransformDTIntoXML(ds.Tables[0], "d://ReportForm.xml");
                //        return ds.Tables[0];
                //    }
                //    else
                //    {
                //        return new DataTable();
                //    }
                //}
                //else
                //{
                //    return new DataTable();
                //}
                #endregion

                string[] p = FormNo.Split(';');
                
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * ");
                strSql.Append(" FROM ReportItemQueryDataSource ");
                if (p.Length >= 4)
                {
                    strSql.Append(" where ReceiveDate='" + p[0] + "' and SectionNo="+p[1]+ " and TestTypeNo="+p[2]+ " and SampleNo='" + p[3]+ "'");
                }
                //strSql.Append(" order by rowid ");
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
            catch(Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("GetReportItemFullList:" + ex.ToString());
                return new DataTable();
            }
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
            throw new NotImplementedException();
        }

        public int UpdateReportItemFull(ReportItemFull t)
        {
            throw new NotImplementedException();
        }

        public DataSet GetReportItemList_DataSet(List<string> FormNo)
        {
            
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * ");
                strSql.Append(" FROM ReportItemQueryDataSource ");
                strSql.Append(" where 1=2 ");
                foreach (var a in FormNo) {
                    string[] p = a.Split(';');
                    if (p.Length >= 4)
                    {
                        strSql.Append(" or (ReceiveDate='" + p[0] + "' and SectionNo=" + p[1] + " and TestTypeNo=" + p[2] + " and SampleNo='" + p[3] + "')");
                    }
                }
                ZhiFang.Common.Log.Log.Debug("GetReportItemList_DataSet.sql:"+ strSql.ToString());
                DataSet ds = DbHelperSQL.Query(strSql.ToString());
                return ds;
        }

        public DataTable GetReportItemFullListAndSort(string FormNo, List<string> sortFields)
        {
            try
            {
                string[] p = FormNo.Split(';');

                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * ");
                strSql.Append(" FROM ReportItemQueryDataSource ");
                if (p.Length >= 4)
                {
                    strSql.Append(" where ReceiveDate='" + p[0] + "' and SectionNo=" + p[1] + " and TestTypeNo=" + p[2] + " and SampleNo='" + p[3] + "'");
                }
                if (sortFields != null && sortFields.Count > 0)
                {
                    int size = sortFields.Count;
                    //获取第一个元素
                    string sortFieldsFirst = sortFields[0];
                    //将 字段名,排序方式 格式的string分开
                    string[] sortField = sortFieldsFirst.Split(new char[] { ',' });
                    //判断如果第一组字段格式错误，则不排序
                    if (sortField!=null && sortField.Length > 1)
                    {
                        strSql.Append(" order by " + sortField[0] + " " + sortField[1]);
                        //如果需要多个字段排序，继续拼接
                        if (size > 1)
                        {
                            for (int i = 1; i < size; i++)
                            {
                                string[] sortFieldElse = sortFields[i].Split(new char[] { ',' });
                                if (sortFieldElse.Length>1)
                                {
                                    strSql.Append("," + sortFieldElse[0] + " " + sortFieldElse[1]);
                                }
                                else
                                {
                                    int j = i + 1;
                                    ZhiFang.Common.Log.Log.Debug("第"+j+"组排序字段格式错误，请按提示格式拼接");
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
                string[] p = FormNo.Split(';');
                if (p.Length >= 4)
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
                    SqlParameter sp = new SqlParameter("@ReceiveDate", SqlDbType.VarChar, 20);
                    SqlParameter sp1 = new SqlParameter("@SectionNo", SqlDbType.VarChar, 50);
                    SqlParameter sp2 = new SqlParameter("@TestTypeNo", SqlDbType.VarChar, 50);
                    SqlParameter sp3 = new SqlParameter("@SampleNo", SqlDbType.VarChar, 50);
                    SqlParameter sqlParameter1 = new SqlParameter("@SortFields", SqlDbType.VarChar, 1000);
                    sp.Value = p[0];
                    sp1.Value = p[1];
                    sp2.Value = p[2];
                    sp3.Value = p[3];
                    sqlParameter1.Value = sortFieldsSql.ToString();
                    DataSet dataSet = DbHelperSQL.RunProcedure("Proc_ReportItemQueryDataSource", new SqlParameter[] { sp, sp1, sp2, sp3, sqlParameter1 }, "ReportItem");
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
            string[] p = FormNo.Split(';');

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) ");
            strSql.Append(" FROM ReportItemQueryDataSource ");
            if (p.Length >= 4)
            {
                strSql.Append(" where ReceiveDate='" + p[0] + "' and SectionNo=" + p[1] + " and TestTypeNo=" + p[2] + " and SampleNo='" + p[3] + "'");
            }
            if (!String.IsNullOrWhiteSpace(where))
            {
                strSql.Append(" and "+where);
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ReportItemQueryDataSource ");
            strSql.Append(" where 1=2 ");
            foreach (var a in FormNo)
            {
                string[] p = a.Split(';');
                if (p.Length >= 4)
                {
                    strSql.Append(" or (ReceiveDate='" + p[0] + "' and SectionNo=" + p[1] + " and TestTypeNo=" + p[2] + " and SampleNo='" + p[3] + "')");
                }
            }
            if (!string.IsNullOrEmpty(sortFields))
            {
                strSql.Append(" order by "+ sortFields);
            }
            ZhiFang.Common.Log.Log.Debug("GetReportItemList_DataSet.sql:" + strSql.ToString());
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            return ds;
        }

        #endregion
    }
}

