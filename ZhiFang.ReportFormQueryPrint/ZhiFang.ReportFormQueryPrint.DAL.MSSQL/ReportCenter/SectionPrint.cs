using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.DBUtility;
using System.Collections.Generic;
using ZhiFang.ReportFormQueryPrint.Model;
using ZhiFang.ReportFormQueryPrint.Common;

namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.ReportCenter
{
	/// <summary>
	/// 数据访问类:SectionPrint
	/// </summary>
	public class SectionPrint:IDSectionPrint
	{
		public SectionPrint()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("SPID", "SectionPrint"); 
		}


		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int SPID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from SectionPrint");
			strSql.Append(" where SPID="+SPID+" ");
			return DbHelperSQL.Exists(strSql.ToString());
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int SPID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SectionPrint ");
			strSql.Append(" where SPID="+SPID+" " );
            ZhiFang.Common.Log.Log.Debug("SectionPrint.delete sql:" + strSql.ToString());
            int rowsAffected=DbHelperSQL.ExecuteSql(strSql.ToString());
			if (rowsAffected > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string SPIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SectionPrint ");
			strSql.Append(" where SPID in ("+SPIDlist + ")  ");
            ZhiFang.Common.Log.Log.Debug("SectionPrint.DeleteList sql:" + strSql.ToString());
            int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			if (rows > 0)
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
			strSql.Append("select SPID,SectionNo,UseDefPrint,PrintFormat,PrintProgram,DefPrinter,PNested,PPreview,FormatPara,TestItemNo,ItemCountMax,ItemCountMin,PrintOrder,PrintFile,nodename,microattribute,sicktypeno,DataTimeStamp ");
			strSql.Append(" FROM SectionPrint ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            ZhiFang.Common.Log.Log.Debug(strSql.ToString());
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
			strSql.Append(" SPID,SectionNo,UseDefPrint,PrintFormat,PrintProgram,DefPrinter,PNested,PPreview,FormatPara,TestItemNo,ItemCountMax,ItemCountMin,PrintOrder,PrintFile,nodename,microattribute,sicktypeno,DataTimeStamp ");
			strSql.Append(" FROM SectionPrint ");
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
			strSql.Append("select count(1) FROM SectionPrint ");
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
				strSql.Append("order by T.SPID desc");
			}
			strSql.Append(")AS Row, T.*  from SectionPrint T ");
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

        public int Add(Model.SectionPrint t)
        {
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            StringBuilder builder3 = new StringBuilder();
            long sectionNo = t.SectionNo;
            builder2.Append("SectionNo,");
            builder3.Append(t.SectionNo + ",");
            builder2.Append("DataAddTime,");
            builder3.Append("'" + DateTime.Now + "',");
            builder2.Append("SPID,");
            builder3.Append(GUIDHelp.GetGUIDInt() + ",");
            if (t.PrintFormat != null)
            {
                builder2.Append("PrintFormat,");
                builder3.Append("'" + t.PrintFormat + "',");
            } 
            if (t.PrintProgram != null)
            {
                builder2.Append("PrintProgram,");
                builder3.Append("'" + t.PrintProgram + "',");
            }
            if (t.DefPrinter != null)
            {
                builder2.Append("DefPrinter,");
                builder3.Append("'" + t.DefPrinter + "',");
            }
            if (t.TestItemNo.HasValue)
            {
                builder2.Append("TestItemNo,");
                builder3.Append(t.TestItemNo + ",");
            }
            if (t.ItemCountMin.HasValue)
            {
                builder2.Append("ItemCountMin,");
                builder3.Append(t.ItemCountMin + ",");
            }
            if (t.ItemCountMax != null)
            {
                builder2.Append("ItemCountMax,");
                builder3.Append("'" + t.ItemCountMax + "',");
            }
            if (t.sicktypeno.HasValue)
            {
                builder2.Append("sicktypeno,");
                builder3.Append(t.sicktypeno + ",");
            }
            if (t.PrintOrder.HasValue)
            {
                builder2.Append("PrintOrder,");
                builder3.Append(t.PrintOrder + ",");
            }
            if (t.microattribute != null)
            {
                builder2.Append("microattribute,");
                builder3.Append("'" + t.microattribute + "',");
            }
            builder2.Append("UseDefPrint,");
            builder3.Append( 1 + ",");
            builder.Append("insert into SectionPrint(");
            builder.Append(builder2.ToString().Remove(builder2.Length - 1));
            builder.Append(")");
            builder.Append(" values (");
            builder.Append(builder3.ToString().Remove(builder3.Length - 1));
            builder.Append(")");
            //builder.Append(";select @@IDENTITY");
            ZhiFang.Common.Log.Log.Debug("SectionPrint.add sql:"+builder.ToString());
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 1;
            }
            return Convert.ToInt32(single);
        }

         int IDSectionPrint.Delete(int SPID)
        {
            int i = 0;
            string sql = "delete from SectionPrint where SPID=" + SPID;
            ZhiFang.Common.Log.Log.Debug(sql);
            i = DbHelperSQL.ExecuteSql(sql);
            return i;
        }

        public int Delete(int SectionNo, int SPID)
        {
            throw new NotImplementedException();
        }

        int IDSectionPrint.DeleteList(string SPIDlist)
        {
            throw new NotImplementedException();
        }

        public bool Exists(int SectionNo, int SPID)
        {
            throw new NotImplementedException();
        }

        public Model.SectionPrint GetModel(int SPID)
        {
            throw new NotImplementedException();
        }

        public int Update(Model.SectionPrint t)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE SectionPrint SET ");
            builder.Append("DataUpdateTime=" + "'" + DateTime.Now + "'");
            if (t.SectionNo != null)
            {
                builder.Append(",SectionNo=" + "'" + t.SectionNo + "'");
            }
            if (t.PrintFormat != null && !t.PrintFormat.Equals(""))
            {
                builder.Append(",PrintFormat=" + "'" + t.PrintFormat + "'");
            }
            if (t.PrintProgram != null && !t.PrintProgram.Equals(""))
            {
                builder.Append(",PrintProgram=" + "'" + t.PrintProgram + "'");
            }
            if (t.DefPrinter != null && !t.DefPrinter.Equals(""))
            {
                builder.Append(",DefPrinter=" + "'" + t.DefPrinter + "'");
            }
            if (t.TestItemNo != null && !t.TestItemNo.Equals(""))
            {
                builder.Append(",TestItemNo=" + "'" + t.TestItemNo + "'");
            }
            if (t.ItemCountMax != null)
            {
                builder.Append(",ItemCountMin=" + t.ItemCountMin );
            }
            else
            {
                builder.Append(",ItemCountMax=null");
            }
            if (t.ItemCountMin != null)
            {
                builder.Append(",ItemCountMax=" + t.ItemCountMax );
            }
            else
            {
                builder.Append(",ItemCountMin=null");
            }
            if (t.sicktypeno != null && !t.sicktypeno.Equals(""))
            {
                builder.Append(",sicktypeno=" + "'" + t.sicktypeno + "'");
            }
            if (t.PrintOrder != null && !t.PrintOrder.Equals(""))
            {
                builder.Append(",PrintOrder=" + "'" + t.PrintOrder + "'");
            }
            if (t.microattribute != null && !t.microattribute.Equals(""))
            {
                builder.Append(",microattribute=" + "'" + t.microattribute + "'");
            }
            if (t.IsRFGraphdataPDf)
            {
                builder.Append(",IsRFGraphdataPDf=" + 1 );
            }
            else
            {
                builder.Append(",IsRFGraphdataPDf=" + 0 );
            }
            builder.Append(",UseDefPrint="  + 1 );         
            builder.Append(" WHERE SPID=" + t.SPID);
            ZhiFang.Common.Log.Log.Debug("SectionPrint.Update sql:"+builder.ToString());
            return DbHelperSQL.ExecuteSql(builder.ToString());
        }
        
        public DataSet GetList(Model.SectionPrint model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * ");
            builder.Append(" FROM SectionPrint where 1=1 ");
            if (model.UseDefPrint.HasValue)
            {
                builder.Append(" and UseDefPrint=" + model.UseDefPrint + " ");
            }
            long sectionNo = model.SectionNo;
            builder.Append(" and SectionNo=" + model.SectionNo + " ");
            if (model.PrintFormat != null)
            {
                builder.Append(" and PrintFormat='" + model.PrintFormat + "' ");
            }
            if (model.PrintProgram != null)
            {
                builder.Append(" and PrintProgram='" + model.PrintProgram + "' ");
            }
            if (model.DefPrinter != null)
            {
                builder.Append(" and DefPrinter='" + model.DefPrinter + "' ");
            }
            if (model.PNested.HasValue)
            {
                builder.Append(" and PNested=" + model.PNested + " ");
            }
            if (model.PPreview.HasValue)
            {
                builder.Append(" and PPreview=" + model.PPreview + " ");
            }
            if (model.FormatPara != null)
            {
                builder.Append(" and FormatPara='" + model.FormatPara + "' ");
            }
            if (model.TestItemNo.HasValue)
            {
                builder.Append(" and TestItemNo=" + model.TestItemNo + " ");
            }
            if (model.ItemCountMax.HasValue)
            {
                builder.Append(" and ItemCountMax=" + model.ItemCountMax + " ");
            }
            if (model.ItemCountMin.HasValue)
            {
                builder.Append(" and ItemCountMin=" + model.ItemCountMin + " ");
            }
            if (model.PrintOrder.HasValue)
            {
                builder.Append(" and PrintOrder=" + model.PrintOrder + " ");
            }
            if (model.PrintFile != null)
            {
                builder.Append(" and PrintFile=" + model.PrintFile + " ");
            }
            if (model.clientno.HasValue)
            {
                builder.Append(" and SPID=" + model.SPID + " ");
            }
            if (model.SampleTypeNo != 0 ) {

                builder.Append(" and SampleTypeNo=" + model.SampleTypeNo + " ");
            }
            if (model.LabID>0)
            {
                builder.Append(" and LabID=" + model.LabID + " ");
            }
            builder.Append(" order by  PrintOrder asc, SPID desc ");
            ZhiFang.Common.Log.Log.Debug("SectionPrint.GetList.sql:"+ builder.ToString());
            return DbHelperSQL.Query(builder.ToString());
        }

        public List<Model.SectionPrint> GetModelList(Model.SectionPrint sectionprint)
        {
            List<Model.SectionPrint> sectionprintlist = new List<Model.SectionPrint>();
            DataSet ds = GetList(sectionprint);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Model.SectionPrint model = new Model.SectionPrint();
                    if (ds.Tables[0].Rows[i]["LabID"].ToString() != "")
                    {
                        model.LabID = long.Parse(ds.Tables[0].Rows[i]["LabID"].ToString());
                    }
                    model.FormatPara = ds.Tables[0].Rows[i]["FormatPara"].ToString();
                    if (ds.Tables[0].Rows[i]["TestItemNo"].ToString() != "")
                    {
                        model.TestItemNo = int.Parse(ds.Tables[0].Rows[i]["TestItemNo"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["ItemCountMax"].ToString() != "")
                    {
                        model.ItemCountMax = int.Parse(ds.Tables[0].Rows[i]["ItemCountMax"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["ItemCountMin"].ToString() != "")
                    {
                        model.ItemCountMin = int.Parse(ds.Tables[0].Rows[i]["ItemCountMin"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["PrintOrder"].ToString() != "")
                    {
                        model.PrintOrder = int.Parse(ds.Tables[0].Rows[i]["PrintOrder"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["PrintFile"].ToString() != "")
                    {
                        model.PrintFile = (byte[])ds.Tables[0].Rows[i]["PrintFile"];
                    }
                    model.nodename = ds.Tables[0].Rows[i]["nodename"].ToString();
                    model.microattribute = ds.Tables[0].Rows[i]["microattribute"].ToString();
                    if (ds.Tables[0].Rows[i]["sicktypeno"].ToString() != "")
                    {
                        model.sicktypeno = int.Parse(ds.Tables[0].Rows[i]["sicktypeno"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["DataAddTime"].ToString() != "")
                    {
                        model.DataAddTime = DateTime.Parse(ds.Tables[0].Rows[i]["DataAddTime"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["SPID"].ToString() != "")
                    {
                        model.SPID = long.Parse(ds.Tables[0].Rows[i]["SPID"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["DataUpdateTime"].ToString() != "")
                    {
                        model.DataUpdateTime = DateTime.Parse(ds.Tables[0].Rows[i]["DataUpdateTime"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["DataMigrationTime"].ToString() != "")
                    {
                        model.DataMigrationTime = DateTime.Parse(ds.Tables[0].Rows[i]["DataMigrationTime"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["DataTimeStamp"].ToString() != "")
                    {
                        //model.DataTimeStamp = DateTime.Parse(ds.Tables[0].Rows[i]["DataTimeStamp"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["SectionNo"].ToString() != "")
                    {
                        model.SectionNo = long.Parse(ds.Tables[0].Rows[i]["SectionNo"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["UseDefPrint"].ToString() != "")
                    {
                        model.UseDefPrint = int.Parse(ds.Tables[0].Rows[i]["UseDefPrint"].ToString());
                    }
                    model.PrintFormat = ds.Tables[0].Rows[i]["PrintFormat"].ToString();
                    model.PrintProgram = ds.Tables[0].Rows[i]["PrintProgram"].ToString();
                    model.DefPrinter = ds.Tables[0].Rows[i]["DefPrinter"].ToString();
                    if (ds.Tables[0].Rows[i]["PNested"].ToString() != "")
                    {
                        model.PNested = int.Parse(ds.Tables[0].Rows[i]["PNested"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["PPreview"].ToString() != "")
                    {
                        model.PPreview = int.Parse(ds.Tables[0].Rows[i]["PPreview"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["clientno"].ToString() != "" && ds.Tables[0].Rows[i]["clientno"].ToString() != "0" && ds.Tables[0].Rows[i]["clientno"].ToString() != null)
                    {
                        model.clientno = int.Parse(ds.Tables[0].Rows[i]["clientno"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["SampleTypeNo"].ToString() != "" && ds.Tables[0].Rows[i]["SampleTypeNo"].ToString() != "0" && ds.Tables[0].Rows[i]["SampleTypeNo"].ToString() != null)
                    {
                        model.SampleTypeNo = int.Parse(ds.Tables[0].Rows[i]["SampleTypeNo"].ToString());
                    }
                    if (ds.Tables[0].Columns.Contains("IsRFGraphdataPDf") && ds.Tables[0].Rows[i]["IsRFGraphdataPDf"].ToString() != null && ds.Tables[0].Rows[i]["IsRFGraphdataPDf"].ToString() != "")
                    {
                        model.IsRFGraphdataPDf = bool.Parse(ds.Tables[0].Rows[i]["IsRFGraphdataPDf"].ToString());
                    }
                    sectionprintlist.Add(model);
                }
                return sectionprintlist;
            }
            else
            {
                return null;
            }
        }
        /** 连表查询小组名 */
        public DataSet GetSectionPgroupList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.SPID as SPID , "+
                          " a.SectionNo as SectionNo," +
                          "a.UseDefPrint as UseDefPrint," +
                          "a.PrintFormat as PrintFormat," +
                          "a.PrintProgram as PrintProgram," +
                          "a.DefPrinter as DefPrinter," +
                          "a.PNested as PNested," +
                          "a.PPreview as PPreview," +
                          "a.FormatPara as FormatPara," +
                          "a.TestItemNo as TestItemNo," +
                          "a.ItemCountMax as ItemCountMax," +
                          "a.ItemCountMin as ItemCountMin," +
                          "a.PrintOrder as PrintOrder," +
                          "a.PrintFile as PrintFile," +
                          "a.nodename as nodename," +
                          "a.microattribute as microattribute," +
                          "a.sicktypeno as sicktypeno," +
                          "a.DataTimeStamp as DataTimeStamp," +
                          "a.IsRFGraphdataPDf as IsRFGraphdataPDf," +
                          "b.CName as CName");
            strSql.Append(" FROM SectionPrint a left join PGroup b on a.SectionNo = b.SectionNo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            ZhiFang.Common.Log.Log.Debug(strSql.ToString());
            DataSet aa = DbHelperSQL.Query(strSql.ToString());
            return aa;
        }

        public Model.SectionPrint GetSectionPrintStrPageNameBySectionNo(string SectionNo)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1  ");
            builder.Append(" * ");
            builder.Append(" from SectionPrint ");
            builder.Append(" where SectionNo=" + SectionNo);
            builder.Append(" order by printorder");
            Model.SectionPrint print = new Model.SectionPrint();
            DataSet set = DbHelperSQL.Query(builder.ToString());
            if (set.Tables[0].Rows.Count > 0)
            {
                if (set.Tables[0].Rows[0]["SPID"].ToString() != "")
                {
                    print.SPID = long.Parse(set.Tables[0].Rows[0]["SPID"].ToString());
                }
                if (set.Tables[0].Rows[0]["SectionNo"].ToString() != "")
                {
                    print.SectionNo = int.Parse(set.Tables[0].Rows[0]["SectionNo"].ToString());
                }
                if (set.Tables[0].Rows[0]["UseDefPrint"].ToString() != "")
                {
                    print.UseDefPrint = new int?(int.Parse(set.Tables[0].Rows[0]["UseDefPrint"].ToString()));
                }
                print.PrintFormat = set.Tables[0].Rows[0]["PrintFormat"].ToString();
                print.PrintProgram = set.Tables[0].Rows[0]["PrintProgram"].ToString();
                print.DefPrinter = set.Tables[0].Rows[0]["DefPrinter"].ToString();
                if (set.Tables[0].Rows[0]["PNested"].ToString() != "")
                {
                    print.PNested = new int?(int.Parse(set.Tables[0].Rows[0]["PNested"].ToString()));
                }
                if (set.Tables[0].Rows[0]["PPreview"].ToString() != "")
                {
                    print.PPreview = new int?(int.Parse(set.Tables[0].Rows[0]["PPreview"].ToString()));
                }
                print.FormatPara = set.Tables[0].Rows[0]["FormatPara"].ToString();
                if (set.Tables[0].Rows[0]["TestItemNo"].ToString() != "")
                {
                    print.TestItemNo = new int?(int.Parse(set.Tables[0].Rows[0]["TestItemNo"].ToString()));
                }
                if (set.Tables[0].Rows[0]["ItemCountMax"].ToString() != "")
                {
                    print.ItemCountMax = new int?(int.Parse(set.Tables[0].Rows[0]["ItemCountMax"].ToString()));
                }
                if (set.Tables[0].Rows[0]["ItemCountMin"].ToString() != "")
                {
                    print.ItemCountMin = new int?(int.Parse(set.Tables[0].Rows[0]["ItemCountMin"].ToString()));
                }
                if (set.Tables[0].Rows[0]["PrintOrder"].ToString() != "")
                {
                    print.PrintOrder = new int?(int.Parse(set.Tables[0].Rows[0]["PrintOrder"].ToString()));
                }
                if (set.Tables[0].Rows[0]["PrintFile"].ToString() != "")
                {
                    print.PrintFile = (byte[])set.Tables[0].Rows[0]["PrintFile"];
                }
                return print;
            }
            return null;
        }
    }
}

