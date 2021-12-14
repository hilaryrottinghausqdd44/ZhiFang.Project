using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.DBUtility;
using System.Data;
using System.Data.SqlClient;

namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL2009
{
	/// <summary>
	/// 数据访问类ReportMicro。
	/// </summary>
	public class ReportMicro:IDReportMicro
	{
		public ReportMicro()
		{}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ResultNo", "ReportMicro"); 
		}


		/// <summary>
		/// 是否存在该记录
		/// </summary>
        public bool Exists(int ResultNo, int ItemNo, string FormNo)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from ReportMicro");
			strSql.Append(" where ResultNo="+ResultNo+" and ItemNo="+ItemNo+" and FormNo="+FormNo+" ");
			return DbHelperSQL.Exists(strSql.ToString());
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Model.ReportMicro model)
		{
			StringBuilder strSql=new StringBuilder();
			StringBuilder strSql1=new StringBuilder();
			StringBuilder strSql2=new StringBuilder();
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
			if (model.DescNo != null)
			{
				strSql1.Append("DescNo,");
				strSql2.Append(""+model.DescNo+",");
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
			if (model.AntiNo != null)
			{
				strSql1.Append("AntiNo,");
				strSql2.Append(""+model.AntiNo+",");
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
			if (model.SusDesc != null)
			{
				strSql1.Append("SusDesc,");
				strSql2.Append("'"+model.SusDesc+"',");
			}
			if (model.RefRange != null)
			{
				strSql1.Append("RefRange,");
				strSql2.Append("'"+model.RefRange+"',");
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
			if (model.isReceive != null)
			{
				strSql1.Append("isReceive,");
				strSql2.Append(""+model.isReceive+",");
			}
			if (model.FormNo != null)
			{
				strSql1.Append("FormNo,");
				strSql2.Append(""+model.FormNo+",");
			}
			if (model.microcountdesc != null)
			{
				strSql1.Append("microcountdesc,");
				strSql2.Append("'"+model.microcountdesc+"',");
			}
			if (model.mresulttype != null)
			{
				strSql1.Append("mresulttype,");
				strSql2.Append(""+model.mresulttype+",");
			}
			strSql.Append("insert into ReportMicro(");
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
		public int Update(Model.ReportMicro model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update ReportMicro set ");
			if (model.DescNo != null)
			{
				strSql.Append("DescNo="+model.DescNo+",");
			}
			if (model.MicroNo != null)
			{
				strSql.Append("MicroNo="+model.MicroNo+",");
			}
			if (model.MicroDesc != null)
			{
				strSql.Append("MicroDesc='"+model.MicroDesc+"',");
			}
			if (model.AntiNo != null)
			{
				strSql.Append("AntiNo="+model.AntiNo+",");
			}
			if (model.Suscept != null)
			{
				strSql.Append("Suscept='"+model.Suscept+"',");
			}
			if (model.SusQuan != null)
			{
				strSql.Append("SusQuan="+model.SusQuan+",");
			}
			if (model.SusDesc != null)
			{
				strSql.Append("SusDesc='"+model.SusDesc+"',");
			}
			if (model.RefRange != null)
			{
				strSql.Append("RefRange='"+model.RefRange+"',");
			}
			if (model.ItemDate != null)
			{
				strSql.Append("ItemDate='"+model.ItemDate+"',");
			}
			if (model.ItemTime != null)
			{
				strSql.Append("ItemTime='"+model.ItemTime+"',");
			}
			if (model.ItemDesc != null)
			{
				strSql.Append("ItemDesc='"+model.ItemDesc+"',");
			}
			if (model.EquipNo != null)
			{
				strSql.Append("EquipNo="+model.EquipNo+",");
			}
			if (model.Modified != null)
			{
				strSql.Append("Modified="+model.Modified+",");
			}
			if (model.IsMatch != null)
			{
				strSql.Append("IsMatch="+model.IsMatch+",");
			}
			if (model.CheckType != null)
			{
				strSql.Append("CheckType="+model.CheckType+",");
			}
			if (model.isReceive != null)
			{
				strSql.Append("isReceive="+model.isReceive+",");
			}
			if (model.microcountdesc != null)
			{
				strSql.Append("microcountdesc='"+model.microcountdesc+"',");
			}
			if (model.mresulttype != null)
			{
				strSql.Append("mresulttype="+model.mresulttype+",");
			}
			int n = strSql.ToString().LastIndexOf(",");
			strSql.Remove(n, 1);
			strSql.Append(" where ResultNo="+ model.ResultNo+" and ItemNo="+ model.ItemNo+" and FormNo="+ model.FormNo+" ");
            return DbHelperSQL.ExecuteSql(strSql.ToString());
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
        public int Delete(int ResultNo, int ItemNo, string FormNo)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ReportMicro ");
			strSql.Append(" where ResultNo="+ResultNo+" and ItemNo="+ItemNo+" and FormNo="+FormNo+" " );
            return DbHelperSQL.ExecuteSql(strSql.ToString());
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public Model.ReportMicro GetModel(int ResultNo, int ItemNo, string FormNo)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1  ");
			strSql.Append(" ResultNo,ItemNo,DescNo,MicroNo,MicroDesc,AntiNo,Suscept,SusQuan,SusDesc,RefRange,ItemDate,ItemTime,ItemDesc,EquipNo,Modified,IsMatch,CheckType,isReceive,FormNo,microcountdesc,mresulttype ");
			strSql.Append(" from ReportMicro ");
			strSql.Append(" where ResultNo="+ResultNo+" and ItemNo="+ItemNo+" and FormNo="+FormNo+" " );
			Model.ReportMicro model=new Model.ReportMicro();
			DataSet ds=DbHelperSQL.Query(strSql.ToString());
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ResultNo"].ToString()!="")
				{
					model.ResultNo=int.Parse(ds.Tables[0].Rows[0]["ResultNo"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ItemNo"].ToString()!="")
				{
					model.ItemNo=int.Parse(ds.Tables[0].Rows[0]["ItemNo"].ToString());
				}
				if(ds.Tables[0].Rows[0]["DescNo"].ToString()!="")
				{
					model.DescNo=int.Parse(ds.Tables[0].Rows[0]["DescNo"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MicroNo"].ToString()!="")
				{
					model.MicroNo=int.Parse(ds.Tables[0].Rows[0]["MicroNo"].ToString());
				}
				model.MicroDesc=ds.Tables[0].Rows[0]["MicroDesc"].ToString();
				if(ds.Tables[0].Rows[0]["AntiNo"].ToString()!="")
				{
					model.AntiNo=int.Parse(ds.Tables[0].Rows[0]["AntiNo"].ToString());
				}
				model.Suscept=ds.Tables[0].Rows[0]["Suscept"].ToString();
				if(ds.Tables[0].Rows[0]["SusQuan"].ToString()!="")
				{
					model.SusQuan=decimal.Parse(ds.Tables[0].Rows[0]["SusQuan"].ToString());
				}
				model.SusDesc=ds.Tables[0].Rows[0]["SusDesc"].ToString();
				model.RefRange=ds.Tables[0].Rows[0]["RefRange"].ToString();
				if(ds.Tables[0].Rows[0]["ItemDate"].ToString()!="")
				{
					model.ItemDate=DateTime.Parse(ds.Tables[0].Rows[0]["ItemDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ItemTime"].ToString()!="")
				{
					model.ItemTime=DateTime.Parse(ds.Tables[0].Rows[0]["ItemTime"].ToString());
				}
				model.ItemDesc=ds.Tables[0].Rows[0]["ItemDesc"].ToString();
				if(ds.Tables[0].Rows[0]["EquipNo"].ToString()!="")
				{
					model.EquipNo=int.Parse(ds.Tables[0].Rows[0]["EquipNo"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Modified"].ToString()!="")
				{
					model.Modified=int.Parse(ds.Tables[0].Rows[0]["Modified"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsMatch"].ToString()!="")
				{
					model.IsMatch=int.Parse(ds.Tables[0].Rows[0]["IsMatch"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CheckType"].ToString()!="")
				{
					model.CheckType=int.Parse(ds.Tables[0].Rows[0]["CheckType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["isReceive"].ToString()!="")
				{
					model.isReceive=int.Parse(ds.Tables[0].Rows[0]["isReceive"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FormNo"].ToString()!="")
				{
					model.FormNo=ds.Tables[0].Rows[0]["FormNo"].ToString();
				}
				model.microcountdesc=ds.Tables[0].Rows[0]["microcountdesc"].ToString();
				if(ds.Tables[0].Rows[0]["mresulttype"].ToString()!="")
				{
					model.mresulttype=int.Parse(ds.Tables[0].Rows[0]["mresulttype"].ToString());
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
			strSql.Append("select ResultNo,ItemNo,DescNo,MicroNo,MicroDesc,AntiNo,Suscept,SusQuan,SusDesc,RefRange,ItemDate,ItemTime,ItemDesc,EquipNo,Modified,IsMatch,CheckType,isReceive,FormNo,microcountdesc,mresulttype ");
			strSql.Append(" FROM ReportMicro ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(Model.ReportMicro model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ResultNo,ItemNo,DescNo,MicroNo,MicroDesc,AntiNo,Suscept,SusQuan,SusDesc,RefRange,ItemDate,ItemTime,ItemDesc,EquipNo,Modified,IsMatch,CheckType,isReceive,FormNo,microcountdesc,mresulttype ");
            strSql.Append(" FROM ReportMicro where 1=1 ");
            if (model.DescNo != null)
            {
                strSql.Append(" and DescNo=" + model.DescNo + "");
            }
            if (model.MicroNo != null)
            {
                strSql.Append(" and MicroNo=" + model.MicroNo + "");
            }
            if (model.MicroDesc != null)
            {
                strSql.Append(" and MicroDesc='" + model.MicroDesc + "'");
            }
            if (model.AntiNo != null)
            {
                strSql.Append(" and AntiNo=" + model.AntiNo + "");
            }
            if (model.Suscept != null)
            {
                strSql.Append(" and Suscept='" + model.Suscept + "'");
            }
            if (model.SusQuan != null)
            {
                strSql.Append(" and SusQuan=" + model.SusQuan + "");
            }
            if (model.SusDesc != null)
            {
                strSql.Append(" and SusDesc='" + model.SusDesc + "'");
            }
            if (model.RefRange != null)
            {
                strSql.Append(" and RefRange='" + model.RefRange + "'");
            }
            if (model.ItemDate != null)
            {
                strSql.Append(" and ItemDate='" + model.ItemDate + "'");
            }
            if (model.ItemTime != null)
            {
                strSql.Append(" and ItemTime='" + model.ItemTime + "'");
            }
            if (model.ItemDesc != null)
            {
                strSql.Append(" and ItemDesc='" + model.ItemDesc + "'");
            }
            if (model.EquipNo != null)
            {
                strSql.Append(" and EquipNo=" + model.EquipNo + "");
            }
            if (model.Modified != null)
            {
                strSql.Append(" and Modified=" + model.Modified + "");
            }
            if (model.IsMatch != null)
            {
                strSql.Append(" and IsMatch=" + model.IsMatch + "");
            }
            if (model.CheckType != null)
            {
                strSql.Append(" and CheckType=" + model.CheckType + "");
            }
            if (model.isReceive != null)
            {
                strSql.Append(" and isReceive=" + model.isReceive + "");
            }
            if (model.microcountdesc != null)
            {
                strSql.Append(" and microcountdesc='" + model.microcountdesc + "'");
            }
            if (model.mresulttype != null)
            {
                strSql.Append(" and mresulttype=" + model.mresulttype + "");
            }
            if (model.ResultNo != null)
            {
                strSql.Append(" and ResultNo=" + model.ResultNo + "");
            }
            if (model.ItemNo != null)
            {
                strSql.Append(" and ItemNo=" + model.ItemNo + "");
            }
            if (model.FormNo != null)
            {
                strSql.Append(" and FormNo=" + model.FormNo + "");
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
			strSql.Append(" ResultNo,ItemNo,DescNo,MicroNo,MicroDesc,AntiNo,Suscept,SusQuan,SusDesc,RefRange,ItemDate,ItemTime,ItemDesc,EquipNo,Modified,IsMatch,CheckType,isReceive,FormNo,microcountdesc,mresulttype ");
			strSql.Append(" FROM ReportMicro ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

        public DataTable GetReportMicroList(string FormNo)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select MI.*,TI.cname itemcname,TI.ename itemename from (select DISTINCT b.ItemNo,b.formno,b.itemdesc  from  reportmicro b where b.formno in ('" + FormNo + "') ) MI  left join TestItem TI on(TI.itemNo=MI.itemNo)  WHERE MI.formno='" + FormNo + "'");
                DataSet ds = DbHelperSQL.Query(strSql.ToString());
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
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetReportMicroList(string FormNo, string ItemNo)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select MN.* ,MI.cname Microcname,MI.ename Microename,mp.cname descname  from (select DISTINCT b.formno,b.ItemNo,b.MicroNo,b.descno,b.microdesc,b.microcountdesc  from reportmicro b where b.formno in ('" + FormNo + "') ) MN  left join  MicroItem MI on(MI.MicroNo=MN.MicroNo)  left join microphrase mp on(MP.descNo=MN.descno)  where MN.formno='" + FormNo + "' and MN.Itemno='" + ItemNo + "'");
                DataSet ds = DbHelperSQL.Query(strSql.ToString());
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
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetReportMicroList(string FormNo, string ItemNo, string MicroNo)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select MA.* ,AI.cname Anticname,AI.ename Antiename  from ( select * from reportmicro b where (b.formno  in ('" + FormNo + "') and not(b.antino is null))) MA  left join AntiBiotic AI on(AI.AntiNo=MA.AntiNo)  where MA.formno='" + FormNo + "' and MA.Itemno='" + ItemNo + "' and MA.Microno='" + MicroNo + "'");
                DataSet ds = DbHelperSQL.Query(strSql.ToString());
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
            }
            catch
            {
                return new DataTable();
            }
        }
        /// <summary>
        /// 根据FormNo数组返回ReportForm列表
        /// </summary>
        /// <param name="FormNo">FormNo数组</param>
        /// <returns></returns>
        public DataTable GetReportMicroFullList(string FormNo)
        {
            try
            {                
                #region 执行存储过程
                SqlParameter sp = new SqlParameter("@FormNo", SqlDbType.VarChar, 50);
                sp.Value = FormNo;
                DataSet ds = DbHelperSQL.RunProcedure("GetReportMicroFullList", new SqlParameter[] { sp }, "ReportMicroFull");
                if (ds.Tables.Count > 0)
                {
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
                #endregion
            }
            catch
            {
                return new DataTable();
            }
        }
        #endregion  成员方法




        public DataTable GetReportMicroGroupList(string FormNo)
        {
            try
            {
                #region 执行存储过程
                if (FormNo != null && FormNo.Trim() != "")
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

        public DataTable GetReportMicroGroupListForSTestType(string FormNo)
        {
            throw new NotImplementedException();
        }
    }
}

