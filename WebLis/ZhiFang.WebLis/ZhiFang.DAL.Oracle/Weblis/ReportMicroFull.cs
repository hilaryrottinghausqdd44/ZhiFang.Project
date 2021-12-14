using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using ZhiFang.DBUtility;
using System.Data;

namespace ZhiFang.DAL.Oracle.weblis
{
	/// <summary>
	/// 数据访问类:ReportMicroFull
	/// </summary>
    public partial class ReportMicroFull : BaseDALLisDB, IDReportMicroFull
	{
        public ReportMicroFull(string dbsourceconn)
       {
           DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
       }
        public ReportMicroFull()
       {
           DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
       }
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ReportItemID", "ReportMicroFull"); 
		}


		/// <summary>
		/// 是否存在该记录
		/// </summary>
        public bool Exists(string ReportFormID, string ReportItemID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from ReportMicroFull");
			strSql.Append(" where ReportFormID='"+ReportFormID+"' and ReportItemID="+ReportItemID+" ");
			return DbHelperSQL.Exists(strSql.ToString());
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ZhiFang.Model.ReportMicroFull model)
		{
			StringBuilder strSql=new StringBuilder();
			StringBuilder strSql1=new StringBuilder();
			StringBuilder strSql2=new StringBuilder();
			if (model.ReportFormID != null)
			{
				strSql1.Append("ReportFormID,");
				strSql2.Append("'"+model.ReportFormID+"',");
			}
			if (model.ReportItemID != null)
			{
				strSql1.Append("ReportItemID,");
				strSql2.Append(""+model.ReportItemID+",");
			}
			if (model.ResultNo != null)
			{
				strSql1.Append("ResultNo,");
				strSql2.Append(""+model.ResultNo+",");
			}
			if (model.ItemNo != null)
			{
				strSql1.Append("ItemNo,");
				strSql2.Append(""+model.ItemNo+",");
			}
			if (model.ItemName != null)
			{
				strSql1.Append("ItemName,");
				strSql2.Append("'"+model.ItemName+"',");
			}
			if (model.DescNo != null)
			{
				strSql1.Append("DescNo,");
				strSql2.Append(""+model.DescNo+",");
			}
			if (model.DescName != null)
			{
				strSql1.Append("DescName,");
				strSql2.Append("'"+model.DescName+"',");
			}
			if (model.MicroNo != null)
			{
				strSql1.Append("MicroNo,");
				strSql2.Append(""+model.MicroNo+",");
			}
			if (model.MicroDesc != null)
			{
				strSql1.Append("MicroDesc,");
				strSql2.Append("'"+model.MicroDesc+"',");
			}
			if (model.MicroName != null)
			{
				strSql1.Append("MicroName,");
				strSql2.Append("'"+model.MicroName+"',");
			}
			if (model.AntiNo != null)
			{
				strSql1.Append("AntiNo,");
				strSql2.Append(""+model.AntiNo+",");
			}
			if (model.AntiName != null)
			{
				strSql1.Append("AntiName,");
				strSql2.Append("'"+model.AntiName+"',");
			}
			if (model.Suscept != null)
			{
				strSql1.Append("Suscept,");
				strSql2.Append("'"+model.Suscept+"',");
			}
			if (model.SusQuan != null)
			{
				strSql1.Append("SusQuan,");
				strSql2.Append(""+model.SusQuan+",");
			}
			if (model.RefRange != null)
			{
				strSql1.Append("RefRange,");
				strSql2.Append("'"+model.RefRange+"',");
			}
			if (model.SusDesc != null)
			{
				strSql1.Append("SusDesc,");
				strSql2.Append("'"+model.SusDesc+"',");
			}
			if (model.AntiUnit != null)
			{
				strSql1.Append("AntiUnit,");
				strSql2.Append("'"+model.AntiUnit+"',");
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
			if (model.ItemDesc != null)
			{
				strSql1.Append("ItemDesc,");
				strSql2.Append("'"+model.ItemDesc+"',");
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
			if (model.IsMatch != null)
			{
				strSql1.Append("IsMatch,");
				strSql2.Append(""+model.IsMatch+",");
			}
			if (model.CheckType != null)
			{
				strSql1.Append("CheckType,");
				strSql2.Append(""+model.CheckType+",");
			}
			if (model.SerialNo != null)
			{
				strSql1.Append("SerialNo,");
				strSql2.Append("'"+model.SerialNo+"',");
			}
			if (model.FormNo != null)
			{
				strSql1.Append("FormNo,");
				strSql2.Append(""+model.FormNo+",");
			}
			strSql.Append("insert into ReportMicroFull(");
			strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
			strSql.Append(")");
			strSql.Append(" values (");
			strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
			strSql.Append(")");
			return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ZhiFang.Model.ReportMicroFull model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update ReportMicroFull set ");
            if (model.ResultNo != null)
            {
                strSql.Append(" and ResultNo=" + model.ResultNo + " ");
            }
            if (model.ItemNo != null)
            {
                strSql.Append(" and ItemNo=" + model.ItemNo + " ");
            }
            if (model.ItemName != null)
            {
                strSql.Append(" and ItemName='" + model.ItemName + "' ");
            }
            if (model.DescNo != null)
            {
                strSql.Append(" and DescNo=" + model.DescNo + " ");
            }
            if (model.DescName != null)
            {
                strSql.Append(" and DescName='" + model.DescName + "' ");
            }
            if (model.MicroNo != null)
            {
                strSql.Append(" and MicroNo=" + model.MicroNo + " ");
            }
            if (model.MicroDesc != null)
            {
                strSql.Append(" and MicroDesc='" + model.MicroDesc + "' ");
            }
            if (model.MicroName != null)
            {
                strSql.Append(" and MicroName='" + model.MicroName + "' ");
            }
            if (model.AntiNo != null)
            {
                strSql.Append(" and AntiNo=" + model.AntiNo + " ");
            }
            if (model.AntiName != null)
            {
                strSql.Append(" and AntiName='" + model.AntiName + "' ");
            }
            if (model.Suscept != null)
            {
                strSql.Append(" and Suscept='" + model.Suscept + "' ");
            }
            if (model.SusQuan != null)
            {
                strSql.Append(" and SusQuan=" + model.SusQuan + " ");
            }
            if (model.RefRange != null)
            {
                strSql.Append(" and RefRange='" + model.RefRange + "' ");
            }
            if (model.SusDesc != null)
            {
                strSql.Append(" and SusDesc='" + model.SusDesc + "' ");
            }
            if (model.AntiUnit != null)
            {
                strSql.Append(" and AntiUnit='" + model.AntiUnit + "' ");
            }
            if (model.ItemDate != null)
            {
                strSql.Append(" and ItemDate='" + model.ItemDate + "' ");
            }
            if (model.ItemTime != null)
            {
                strSql.Append(" and ItemTime='" + model.ItemTime + "' ");
            }
            if (model.ItemDesc != null)
            {
                strSql.Append(" and ItemDesc='" + model.ItemDesc + "' ");
            }
            if (model.EquipNo != null)
            {
                strSql.Append(" and EquipNo=" + model.EquipNo + " ");
            }
            if (model.Modified != null)
            {
                strSql.Append(" and Modified=" + model.Modified + " ");
            }
            if (model.IsMatch != null)
            {
                strSql.Append(" and IsMatch=" + model.IsMatch + " ");
            }
            if (model.CheckType != null)
            {
                strSql.Append(" and CheckType=" + model.CheckType + " ");
            }
            if (model.SerialNo != null)
            {
                strSql.Append(" and SerialNo='" + model.SerialNo + "' ");
            }
            if (model.FormNo != null)
            {
                strSql.Append(" and FormNo=" + model.FormNo + " ");
            }
			int n = strSql.ToString().LastIndexOf(",");
			strSql.Remove(n, 1);
			strSql.Append(" where ReportFormID='"+ model.ReportFormID+"' and ReportItemID="+ model.ReportItemID+" ");
			int rowsAffected=DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            return rowsAffected;
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
        public int Delete(string ReportFormID, string ReportItemID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ReportMicroFull ");
			strSql.Append(" where ReportFormID='"+ReportFormID+"' and ReportItemID="+ReportItemID+" " );
			int rowsAffected=DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            return rowsAffected;
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public ZhiFang.Model.ReportMicroFull GetModel(string ReportFormID, string ReportItemID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select   ");
            strSql.Append(" * ");
			strSql.Append(" from ReportMicroFull ");
            strSql.Append(" where ROWNUM <='1' and ReportFormID='" + ReportFormID + "' and ReportItemID=" + ReportItemID + " ");
			Model.ReportMicroFull model=new Model.ReportMicroFull();
			DataSet ds=DbHelperSQL.ExecuteDataSet(strSql.ToString());
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ReportFormID"]!=null && ds.Tables[0].Rows[0]["ReportFormID"].ToString()!="")
				{
					model.ReportFormID=ds.Tables[0].Rows[0]["ReportFormID"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ReportItemID"]!=null && ds.Tables[0].Rows[0]["ReportItemID"].ToString()!="")
				{
					model.ReportItemID=int.Parse(ds.Tables[0].Rows[0]["ReportItemID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ResultNo"]!=null && ds.Tables[0].Rows[0]["ResultNo"].ToString()!="")
				{
					model.ResultNo=int.Parse(ds.Tables[0].Rows[0]["ResultNo"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ItemNo"]!=null && ds.Tables[0].Rows[0]["ItemNo"].ToString()!="")
				{
					model.ItemNo=int.Parse(ds.Tables[0].Rows[0]["ItemNo"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ItemName"]!=null && ds.Tables[0].Rows[0]["ItemName"].ToString()!="")
				{
					model.ItemName=ds.Tables[0].Rows[0]["ItemName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["DescNo"]!=null && ds.Tables[0].Rows[0]["DescNo"].ToString()!="")
				{
					model.DescNo=int.Parse(ds.Tables[0].Rows[0]["DescNo"].ToString());
				}
				if(ds.Tables[0].Rows[0]["DescName"]!=null && ds.Tables[0].Rows[0]["DescName"].ToString()!="")
				{
					model.DescName=ds.Tables[0].Rows[0]["DescName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["MicroNo"]!=null && ds.Tables[0].Rows[0]["MicroNo"].ToString()!="")
				{
					model.MicroNo=int.Parse(ds.Tables[0].Rows[0]["MicroNo"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MicroDesc"]!=null && ds.Tables[0].Rows[0]["MicroDesc"].ToString()!="")
				{
					model.MicroDesc=ds.Tables[0].Rows[0]["MicroDesc"].ToString();
				}
				if(ds.Tables[0].Rows[0]["MicroName"]!=null && ds.Tables[0].Rows[0]["MicroName"].ToString()!="")
				{
					model.MicroName=ds.Tables[0].Rows[0]["MicroName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["AntiNo"]!=null && ds.Tables[0].Rows[0]["AntiNo"].ToString()!="")
				{
					model.AntiNo=int.Parse(ds.Tables[0].Rows[0]["AntiNo"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AntiName"]!=null && ds.Tables[0].Rows[0]["AntiName"].ToString()!="")
				{
					model.AntiName=ds.Tables[0].Rows[0]["AntiName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Suscept"]!=null && ds.Tables[0].Rows[0]["Suscept"].ToString()!="")
				{
					model.Suscept=ds.Tables[0].Rows[0]["Suscept"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SusQuan"]!=null && ds.Tables[0].Rows[0]["SusQuan"].ToString()!="")
				{
					model.SusQuan=decimal.Parse(ds.Tables[0].Rows[0]["SusQuan"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RefRange"]!=null && ds.Tables[0].Rows[0]["RefRange"].ToString()!="")
				{
					model.RefRange=ds.Tables[0].Rows[0]["RefRange"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SusDesc"]!=null && ds.Tables[0].Rows[0]["SusDesc"].ToString()!="")
				{
					model.SusDesc=ds.Tables[0].Rows[0]["SusDesc"].ToString();
				}
				if(ds.Tables[0].Rows[0]["AntiUnit"]!=null && ds.Tables[0].Rows[0]["AntiUnit"].ToString()!="")
				{
					model.AntiUnit=ds.Tables[0].Rows[0]["AntiUnit"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ItemDate"]!=null && ds.Tables[0].Rows[0]["ItemDate"].ToString()!="")
				{
					model.ItemDate=DateTime.Parse(ds.Tables[0].Rows[0]["ItemDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ItemTime"]!=null && ds.Tables[0].Rows[0]["ItemTime"].ToString()!="")
				{
					model.ItemTime=DateTime.Parse(ds.Tables[0].Rows[0]["ItemTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ItemDesc"]!=null && ds.Tables[0].Rows[0]["ItemDesc"].ToString()!="")
				{
					model.ItemDesc=ds.Tables[0].Rows[0]["ItemDesc"].ToString();
				}
				if(ds.Tables[0].Rows[0]["EquipNo"]!=null && ds.Tables[0].Rows[0]["EquipNo"].ToString()!="")
				{
					model.EquipNo=int.Parse(ds.Tables[0].Rows[0]["EquipNo"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Modified"]!=null && ds.Tables[0].Rows[0]["Modified"].ToString()!="")
				{
					model.Modified=int.Parse(ds.Tables[0].Rows[0]["Modified"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsMatch"]!=null && ds.Tables[0].Rows[0]["IsMatch"].ToString()!="")
				{
					model.IsMatch=int.Parse(ds.Tables[0].Rows[0]["IsMatch"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CheckType"]!=null && ds.Tables[0].Rows[0]["CheckType"].ToString()!="")
				{
					model.CheckType=int.Parse(ds.Tables[0].Rows[0]["CheckType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SerialNo"]!=null && ds.Tables[0].Rows[0]["SerialNo"].ToString()!="")
				{
					model.SerialNo=ds.Tables[0].Rows[0]["SerialNo"].ToString();
				}
				if(ds.Tables[0].Rows[0]["FormNo"]!=null && ds.Tables[0].Rows[0]["FormNo"].ToString()!="")
				{
					model.FormNo=int.Parse(ds.Tables[0].Rows[0]["FormNo"].ToString());
				}
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
            strSql.Append("select * ");
			strSql.Append(" FROM ReportMicroFull ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.ExecuteDataSet(strSql.ToString());
		}

        public DataSet GetColumns()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select COLUMN_NAME from all_tab_columns where table_name='REPORTMICROFULL'");
            DataSet dsc = DbHelperSQL.ExecuteDataSet(strSql.ToString());
            if (dsc != null && dsc.Tables.Count > 0 && dsc.Tables[0].Rows.Count > 0)
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                for (int i = 0; i < dsc.Tables[0].Rows.Count; i++)
                {
                    dt.Columns.Add(dsc.Tables[0].Rows[i]["COLUMN_NAME"].ToString());
                }
                ds.Tables.Add(dt);
                return ds;
            }
            return new DataSet();
        }

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			
            strSql.Append(" * ");
			strSql.Append(" FROM ReportMicroFull ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
                strSql.Append(" and ROWNUM <= '" + Top + "'");
            }
            else
            {
                strSql.Append(" where ROWNUM <= '" + Top + "'");
            }
            if (filedOrder == null)
            { }
            else
            {
                strSql.Append(" order by " + filedOrder);
            }
			return DbHelperSQL.ExecuteDataSet(strSql.ToString());
		}

		/*
		*/

        public DataSet GetList(ZhiFang.Model.ReportMicroFull model)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * ");
                strSql.Append(" FROM ReportMicroFull where 1=1 ");
                if (model.ResultNo != null)
                {
                    strSql.Append(" and ResultNo=" + model.ResultNo + " ");
                }
                if (model.ItemNo != null)
                {
                    strSql.Append(" and ItemNo=" + model.ItemNo + " ");
                }
                if (model.ItemName != null)
                {
                    strSql.Append(" and ItemName='" + model.ItemName + "' ");
                }
                if (model.DescNo != null)
                {
                    strSql.Append(" and DescNo=" + model.DescNo + " ");
                }
                if (model.DescName != null)
                {
                    strSql.Append(" and DescName='" + model.DescName + "' ");
                }
                if (model.MicroNo != null)
                {
                    strSql.Append(" and MicroNo=" + model.MicroNo + " ");
                }
                if (model.MicroDesc != null)
                {
                    strSql.Append(" and MicroDesc='" + model.MicroDesc + "' ");
                }
                if (model.MicroName != null)
                {
                    strSql.Append(" and MicroName='" + model.MicroName + "' ");
                }
                if (model.AntiNo != null)
                {
                    strSql.Append(" and AntiNo=" + model.AntiNo + " ");
                }
                if (model.AntiName != null)
                {
                    strSql.Append(" and AntiName='" + model.AntiName + "' ");
                }
                if (model.Suscept != null)
                {
                    strSql.Append(" and Suscept='" + model.Suscept + "' ");
                }
                if (model.SusQuan != null)
                {
                    strSql.Append(" and SusQuan=" + model.SusQuan + " ");
                }
                if (model.RefRange != null)
                {
                    strSql.Append(" and RefRange='" + model.RefRange + "' ");
                }
                if (model.SusDesc != null)
                {
                    strSql.Append(" and SusDesc='" + model.SusDesc + "' ");
                }
                if (model.AntiUnit != null)
                {
                    strSql.Append(" and AntiUnit='" + model.AntiUnit + "' ");
                }
                if (model.ItemDate != null)
                {
                    strSql.Append(" and ItemDate='" + model.ItemDate + "' ");
                }
                if (model.ItemTime != null)
                {
                    strSql.Append(" and ItemTime='" + model.ItemTime + "' ");
                }
                if (model.ItemDesc != null)
                {
                    strSql.Append(" and ItemDesc='" + model.ItemDesc + "' ");
                }
                if (model.EquipNo != null)
                {
                    strSql.Append(" and EquipNo=" + model.EquipNo + " ");
                }
                if (model.Modified != null)
                {
                    strSql.Append(" and Modified=" + model.Modified + " ");
                }
                if (model.IsMatch != null)
                {
                    strSql.Append(" and IsMatch=" + model.IsMatch + " ");
                }
                if (model.CheckType != null)
                {
                    strSql.Append(" and CheckType=" + model.CheckType + " ");
                }
                if (model.SerialNo != null)
                {
                    strSql.Append(" and SerialNo='" + model.SerialNo + "' ");
                }
                if (model.FormNo != null)
                {
                    strSql.Append(" and FormNo=" + model.FormNo + " ");
                }
                if (model.ReportFormID != null)
                {
                    strSql.Append(" and  ReportFormID='" + model.ReportFormID + "' ");
                }
                Common.Log.Log.Info("报告项目信息：" + strSql.ToString());
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
            catch (Exception ex)
            {
                Common.Log.Log.Debug("异常信息：" + ex.ToString());
                return null;
            }
        }

        #endregion

        #region IDataBase<ReportMicroFull> 成员


        public DataSet GetList(int Top, Model.ReportMicroFull model, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top "+Top.ToString()+" * ");
            strSql.Append(" FROM ReportMicroFull where 1=1 ");
            if (model.ResultNo != null)
            {
                strSql.Append(" and ResultNo=" + model.ResultNo + " ");
            }
            if (model.ItemNo != null)
            {
                strSql.Append(" and ItemNo=" + model.ItemNo + " ");
            }
            if (model.ItemName != null)
            {
                strSql.Append(" and ItemName='" + model.ItemName + "' ");
            }
            if (model.DescNo != null)
            {
                strSql.Append(" and DescNo=" + model.DescNo + " ");
            }
            if (model.DescName != null)
            {
                strSql.Append(" and DescName='" + model.DescName + "' ");
            }
            if (model.MicroNo != null)
            {
                strSql.Append(" and MicroNo=" + model.MicroNo + " ");
            }
            if (model.MicroDesc != null)
            {
                strSql.Append(" and MicroDesc='" + model.MicroDesc + "' ");
            }
            if (model.MicroName != null)
            {
                strSql.Append(" and MicroName='" + model.MicroName + "' ");
            }
            if (model.AntiNo != null)
            {
                strSql.Append(" and AntiNo=" + model.AntiNo + " ");
            }
            if (model.AntiName != null)
            {
                strSql.Append(" and AntiName='" + model.AntiName + "' ");
            }
            if (model.Suscept != null)
            {
                strSql.Append(" and Suscept='" + model.Suscept + "' ");
            }
            if (model.SusQuan != null)
            {
                strSql.Append(" and SusQuan=" + model.SusQuan + " ");
            }
            if (model.RefRange != null)
            {
                strSql.Append(" and RefRange='" + model.RefRange + "' ");
            }
            if (model.SusDesc != null)
            {
                strSql.Append(" and SusDesc='" + model.SusDesc + "' ");
            }
            if (model.AntiUnit != null)
            {
                strSql.Append(" and AntiUnit='" + model.AntiUnit + "' ");
            }
            if (model.ItemDate != null)
            {
                strSql.Append(" and ItemDate='" + model.ItemDate + "' ");
            }
            if (model.ItemTime != null)
            {
                strSql.Append(" and ItemTime='" + model.ItemTime + "' ");
            }
            if (model.ItemDesc != null)
            {
                strSql.Append(" and ItemDesc='" + model.ItemDesc + "' ");
            }
            if (model.EquipNo != null)
            {
                strSql.Append(" and EquipNo=" + model.EquipNo + " ");
            }
            if (model.Modified != null)
            {
                strSql.Append(" and Modified=" + model.Modified + " ");
            }
            if (model.IsMatch != null)
            {
                strSql.Append(" and IsMatch=" + model.IsMatch + " ");
            }
            if (model.CheckType != null)
            {
                strSql.Append(" and CheckType=" + model.CheckType + " ");
            }
            if (model.SerialNo != null)
            {
                strSql.Append(" and SerialNo='" + model.SerialNo + "' ");
            }
            if (model.FormNo != null)
            {
                strSql.Append(" and FormNo=" + model.FormNo + " ");
            }
            if (model.ReportFormID != null)
            {
                strSql.Append(" and  ReportFormID='" + model.ReportFormID + "' ");
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetAllList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ReportMicroFull ");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        #endregion

        #region IDataBase<ReportMicroFull> 成员

        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM ReportMicroFull");
            return Convert.ToInt32(DbHelperSQL.ExecuteScalar(strSql.ToString()));
        }

        public int GetTotalCount(Model.ReportMicroFull model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM ReportMicroFull where 1=1 ");
            if (model.ResultNo != null)
            {
                strSql.Append(" and ResultNo=" + model.ResultNo + " ");
            }
            if (model.ItemNo != null)
            {
                strSql.Append(" and ItemNo=" + model.ItemNo + " ");
            }
            if (model.ItemName != null)
            {
                strSql.Append(" and ItemName='" + model.ItemName + "' ");
            }
            if (model.DescNo != null)
            {
                strSql.Append(" and DescNo=" + model.DescNo + " ");
            }
            if (model.DescName != null)
            {
                strSql.Append(" and DescName='" + model.DescName + "' ");
            }
            if (model.MicroNo != null)
            {
                strSql.Append(" and MicroNo=" + model.MicroNo + " ");
            }
            if (model.MicroDesc != null)
            {
                strSql.Append(" and MicroDesc='" + model.MicroDesc + "' ");
            }
            if (model.MicroName != null)
            {
                strSql.Append(" and MicroName='" + model.MicroName + "' ");
            }
            if (model.AntiNo != null)
            {
                strSql.Append(" and AntiNo=" + model.AntiNo + " ");
            }
            if (model.AntiName != null)
            {
                strSql.Append(" and AntiName='" + model.AntiName + "' ");
            }
            if (model.Suscept != null)
            {
                strSql.Append(" and Suscept='" + model.Suscept + "' ");
            }
            if (model.SusQuan != null)
            {
                strSql.Append(" and SusQuan=" + model.SusQuan + " ");
            }
            if (model.RefRange != null)
            {
                strSql.Append(" and RefRange='" + model.RefRange + "' ");
            }
            if (model.SusDesc != null)
            {
                strSql.Append(" and SusDesc='" + model.SusDesc + "' ");
            }
            if (model.AntiUnit != null)
            {
                strSql.Append(" and AntiUnit='" + model.AntiUnit + "' ");
            }
            if (model.ItemDate != null)
            {
                strSql.Append(" and ItemDate='" + model.ItemDate + "' ");
            }
            if (model.ItemTime != null)
            {
                strSql.Append(" and ItemTime='" + model.ItemTime + "' ");
            }
            if (model.ItemDesc != null)
            {
                strSql.Append(" and ItemDesc='" + model.ItemDesc + "' ");
            }
            if (model.EquipNo != null)
            {
                strSql.Append(" and EquipNo=" + model.EquipNo + " ");
            }
            if (model.Modified != null)
            {
                strSql.Append(" and Modified=" + model.Modified + " ");
            }
            if (model.IsMatch != null)
            {
                strSql.Append(" and IsMatch=" + model.IsMatch + " ");
            }
            if (model.CheckType != null)
            {
                strSql.Append(" and CheckType=" + model.CheckType + " ");
            }
            if (model.SerialNo != null)
            {
                strSql.Append(" and SerialNo='" + model.SerialNo + "' ");
            }
            if (model.FormNo != null)
            {
                strSql.Append(" and FormNo=" + model.FormNo + " ");
            }
            if (model.ReportFormID != null)
            {
                strSql.Append(" and  ReportFormID='" + model.ReportFormID + "' ");
            }
            return Convert.ToInt32(DbHelperSQL.ExecuteScalar(strSql.ToString()));
        }

        #endregion

        #region IDataBase<ReportMicroFull> 成员

        public int AddUpdateByDataSet(DataSet ds)
        {
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    int count = 0;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = ds.Tables[0].Rows[i];
                        if (this.Exists(ds.Tables[0].Rows[i]["ReportFormID"].ToString().Trim(), ds.Tables[0].Rows[i]["ReportItemID"].ToString().Trim()))
                        {
                            count += this.UpdateByDataRow(dr);
                        }
                        else
                            count += this.AddByDataRow(dr);
                    }
                    if (count == ds.Tables[0].Rows.Count)
                        return 1;
                    else
                        return 0;
                }
                catch
                {
                    return 0;
                }
            }
            else
                return 1;
        }
        public int AddByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into ReportMicroFull (");
                strSql.Append("ReportFormID,ReportItemID,ResultNo,ItemNo,ItemName,DescNo,DescName,MicroNo,MicroDesc,MicroName,AntiNo,AntiName,Suscept,SusQuan,RefRange,SusDesc,AntiUnit,ItemDate,ItemTime,ItemDesc,EquipNo,Modified,IsMatch,CheckType,SerialNo,FormNo");
                strSql.Append(") values (");
                strSql.Append("'" + dr["ReportFormID"].ToString().Trim() + "','" + dr["ReportItemID"].ToString().Trim() + "','" + dr["ResultNo"].ToString().Trim() + "','" + dr["ItemNo"].ToString().Trim() + "','" + dr["ItemName"].ToString().Trim() + "','" + dr["DescNo"].ToString().Trim() + "','" + dr["DescName"].ToString().Trim() + "','" + dr["MicroNo"].ToString().Trim() + "','" + dr["MicroDesc"].ToString().Trim() + "','" + dr["MicroName"].ToString().Trim() + "','" + dr["AntiNo"].ToString().Trim() + "','" + dr["AntiName"].ToString().Trim() + "','" + dr["Suscept"].ToString().Trim() + "','" + dr["SusQuan"].ToString().Trim() + "','" + dr["RefRange"].ToString().Trim() + "','" + dr["SusDesc"].ToString().Trim() + "','" + dr["AntiUnit"].ToString().Trim() + "','" + dr["ItemDate"].ToString().Trim() + "','" + dr["ItemTime"].ToString().Trim() + "','" + dr["ItemDesc"].ToString().Trim() + "','" + dr["EquipNo"].ToString().Trim() + "','" + dr["Modified"].ToString().Trim() + "','" + dr["IsMatch"].ToString().Trim() + "','" + dr["CheckType"].ToString().Trim() + "','" + dr["SerialNo"].ToString().Trim() + "','" + dr["FormNo"].ToString().Trim() + "'");
                strSql.Append(") ");
                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch
            {
                return 0;
            }
        }
        public int UpdateByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update ReportMicroFull set ");

                strSql.Append(" ReportFormID = '" + dr["ReportFormID"].ToString().Trim() + "' , ");
                strSql.Append(" ReportItemID = '" + dr["ReportItemID"].ToString().Trim() + "' , ");
                strSql.Append(" ResultNo = '" + dr["ResultNo"].ToString().Trim() + "' , ");
                strSql.Append(" ItemNo = '" + dr["ItemNo"].ToString().Trim() + "' , ");
                strSql.Append(" ItemName = '" + dr["ItemName"].ToString().Trim() + "' , ");
                strSql.Append(" DescNo = '" + dr["DescNo"].ToString().Trim() + "' , ");
                strSql.Append(" DescName = '" + dr["DescName"].ToString().Trim() + "' , ");
                strSql.Append(" MicroNo = '" + dr["MicroNo"].ToString().Trim() + "' , ");
                strSql.Append(" MicroDesc = '" + dr["MicroDesc"].ToString().Trim() + "' , ");
                strSql.Append(" MicroName = '" + dr["MicroName"].ToString().Trim() + "' , ");
                strSql.Append(" AntiNo = '" + dr["AntiNo"].ToString().Trim() + "' , ");
                strSql.Append(" AntiName = '" + dr["AntiName"].ToString().Trim() + "' , ");
                strSql.Append(" Suscept = '" + dr["Suscept"].ToString().Trim() + "' , ");
                strSql.Append(" SusQuan = '" + dr["SusQuan"].ToString().Trim() + "' , ");
                strSql.Append(" RefRange = '" + dr["RefRange"].ToString().Trim() + "' , ");
                strSql.Append(" SusDesc = '" + dr["SusDesc"].ToString().Trim() + "' , ");
                strSql.Append(" AntiUnit = '" + dr["AntiUnit"].ToString().Trim() + "' , ");
                strSql.Append(" ItemDate = '" + dr["ItemDate"].ToString().Trim() + "' , ");
                strSql.Append(" ItemTime = '" + dr["ItemTime"].ToString().Trim() + "' , ");
                strSql.Append(" ItemDesc = '" + dr["ItemDesc"].ToString().Trim() + "' , ");
                strSql.Append(" EquipNo = '" + dr["EquipNo"].ToString().Trim() + "' , ");
                strSql.Append(" Modified = '" + dr["Modified"].ToString().Trim() + "' , ");
                strSql.Append(" IsMatch = '" + dr["IsMatch"].ToString().Trim() + "' , ");
                strSql.Append(" CheckType = '" + dr["CheckType"].ToString().Trim() + "' , ");
                strSql.Append(" SerialNo = '" + dr["SerialNo"].ToString().Trim() + "' , ");
                strSql.Append(" FormNo = '" + dr["FormNo"].ToString().Trim() + "'  ");
                strSql.Append(" where ='" + dr[""].ToString().Trim() + "' ");

                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch
            {
                return 0;
            }
        }
        

        #endregion




        public DataSet GetReportMicroGroupList(string FormNo)
        {
            throw new NotImplementedException();
        }


        public int DeleteByWhere(string Strwhere)
        {
            string str = "delete from ReportMicroFull where " + Strwhere;
            return DbHelperSQL.ExecuteNonQuery(str);
           // throw new NotImplementedException();
        }


        public int BackUpReportMicroFullByWhere(string Strwhere)
        {
            throw new NotImplementedException();
        }
    }
}

