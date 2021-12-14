namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66
{


    using Model;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using ZhiFang.ReportFormQueryPrint.IDAL;
    using ZhiFang.ReportFormQueryPrint.DBUtility;
    using System.Collections.Generic;
    using Common;

    public class SectionPrint : IDSectionPrint, IDataBase<Model.SectionPrint>
    {
        public int Add(Model.SectionPrint model)
        {
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            StringBuilder builder3 = new StringBuilder();
            long sectionNo = model.SectionNo;
            builder2.Append("SectionNo,");
            builder3.Append(model.SectionNo + ",");
            //builder2.Append("SPID,");
            //builder3.Append(GUIDHelp.GetGUIDInt()+",");
            if (model.PrintFormat != null)
            {
                builder2.Append("PrintFormat,");
                builder3.Append("'" + model.PrintFormat + "',");
            }
            if (model.PrintProgram != null)
            {
                builder2.Append("PrintProgram,");
                builder3.Append("'" + model.PrintProgram + "',");
            }
            if (model.DefPrinter != null)
            {
                builder2.Append("DefPrinter,");
                builder3.Append("'" + model.DefPrinter + "',");
            }
            if (model.TestItemNo.HasValue)
            {
                builder2.Append("TestItemNo,");
                builder3.Append(model.TestItemNo + ",");
            }
            if (model.ItemCountMin.HasValue)
            {
                builder2.Append("ItemCountMin,");
                builder3.Append(model.ItemCountMin + ",");
            }
            if (model.ItemCountMax != null)
            {
                builder2.Append("ItemCountMax,");
                builder3.Append("'" + model.ItemCountMax + "',");
            }
            if (model.sicktypeno.HasValue)
            {
                builder2.Append("sicktypeno,");
                builder3.Append(model.sicktypeno + ",");
            }
            if (model.PrintOrder.HasValue)
            {
                builder2.Append("PrintOrder,");
                builder3.Append(model.PrintOrder + ",");
            }
            if (model.microattribute != null)
            {
                builder2.Append("microattribute,");
                builder3.Append("'"+model.microattribute + "',");
            }
            builder2.Append("UseDefPrint,");
            builder3.Append(1 + ",");
            builder.Append("insert into SectionPrint(");
            builder.Append(builder2.ToString().Remove(builder2.Length - 1));
            builder.Append(")");
            builder.Append(" values (");
            builder.Append(builder3.ToString().Remove(builder3.Length - 1));
            builder.Append(")");
            builder.Append(";select @@IDENTITY");
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
            ZhiFang.Common.Log.Log.Debug("SectionPrint.IDSectionPrint.delete sql:" + sql);
            i = DbHelperSQL.ExecuteSql(sql);
            return i;
        }

        public int Delete(int SPID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SectionPrint ");
            builder.Append(" where SPID=" + SPID);
            ZhiFang.Common.Log.Log.Debug("SectionPrint.delete sql:" + builder.ToString());
            return DbHelperSQL.ExecuteSql(builder.ToString());
        }

        public int Delete(int SectionNo, int SPID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SectionPrint ");
            builder.Append(" where SectionNo=@SectionNo and SPID=@SPID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SectionNo", SqlDbType.Int, 4), new SqlParameter("@SPID", SqlDbType.Int, 4) };
            cmdParms[0].Value = SectionNo;
            cmdParms[1].Value = SPID;
            ZhiFang.Common.Log.Log.Debug("SectionPrint.delete sql:" + builder.ToString());
            return DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms);
        }

        public int DeleteList(string SPIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SectionPrint ");
            builder.Append(" where SPID in (" + SPIDlist + ")  ");
            ZhiFang.Common.Log.Log.Debug("SectionPrint.delete sql:" + builder.ToString());
            return DbHelperSQL.ExecuteSql(builder.ToString());
        }

        public bool Exists(int SectionNo, int SPID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SectionPrint");
            builder.Append(string.Concat(new object[] { " where SectionNo=", SectionNo, " and SPID=", SPID, " " }));
            return DbHelperSQL.Exists(builder.ToString());
        }

        public DataSet GetList(Model.SectionPrint model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select SPID,SectionNo,UseDefPrint,PrintFormat,PrintProgram,DefPrinter,PNested,PPreview,FormatPara,TestItemNo,ItemCountMax,ItemCountMin,PrintOrder,PrintFile,null as pageformat,clientno,Microattribute,IsRFGraphdataPDf,clientno,SampleTypeNo ");
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

            ZhiFang.Common.Log.Log.Debug("SectionPrint.GetList.sql:"+ builder.ToString());
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select SPID,SectionNo,UseDefPrint,PrintFormat,PrintProgram,DefPrinter,PNested,PPreview,FormatPara,TestItemNo,ItemCountMax,ItemCountMin,PrintOrder,PrintFile,null as pageformat,null as clientno  ");
            builder.Append(" FROM SectionPrint ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" SPID,SectionNo,UseDefPrint,PrintFormat,PrintProgram,DefPrinter,PNested,PPreview,FormatPara,TestItemNo,ItemCountMax,ItemCountMin,PrintOrder,PrintFile ,null as pageformat,null as clientno ");
            builder.Append(" FROM SectionPrint ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("SectionNo", "SectionPrint");
        }

        public Model.SectionPrint GetModel(int SPID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1  ");
            builder.Append(" SPID,SectionNo,UseDefPrint,PrintFormat,PrintProgram,DefPrinter,PNested,PPreview,FormatPara,TestItemNo,ItemCountMax,ItemCountMin,PrintOrder,PrintFile,null as pageformat,null as clientno ");
            builder.Append(" from SectionPrint ");
            builder.Append(" where SPID=" + SPID);
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
                    print.PrintFile = (byte[]) set.Tables[0].Rows[0]["PrintFile"];
                }
                return print;
            }
            return null;
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
                    //if (ds.Tables[0].Rows[i]["LabID"].ToString() != "")
                    //{
                    //    model.LabID = long.Parse(ds.Tables[0].Rows[i]["LabID"].ToString());
                    //}
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
                    //model.nodename = ds.Tables[0].Rows[i]["nodename"].ToString();
                    model.microattribute = ds.Tables[0].Rows[i]["microattribute"].ToString();
                    //if (ds.Tables[0].Rows[i]["sicktypeno"].ToString() != "")
                    //{
                    //    model.sicktypeno = int.Parse(ds.Tables[0].Rows[i]["sicktypeno"].ToString());
                    //}
                    //if (ds.Tables[0].Rows[i]["DataAddTime"].ToString() != "")
                    //{
                    //    model.DataAddTime = DateTime.Parse(ds.Tables[0].Rows[i]["DataAddTime"].ToString());
                    //}
                    if (ds.Tables[0].Rows[i]["SPID"].ToString() != "")
                    {
                        model.SPID = long.Parse(ds.Tables[0].Rows[i]["SPID"].ToString());
                    }
                    //if (ds.Tables[0].Rows[i]["DataUpdateTime"].ToString() != "")
                    //{
                    //    model.DataUpdateTime = DateTime.Parse(ds.Tables[0].Rows[i]["DataUpdateTime"].ToString());
                    //}
                    //if (ds.Tables[0].Rows[i]["DataMigrationTime"].ToString() != "")
                    //{
                    //    model.DataMigrationTime = DateTime.Parse(ds.Tables[0].Rows[i]["DataMigrationTime"].ToString());
                    //}
                    //if (ds.Tables[0].Rows[i]["DataTimeStamp"].ToString() != "")
                    //{
                    //    //model.DataTimeStamp = DateTime.Parse(ds.Tables[0].Rows[i]["DataTimeStamp"].ToString());
                    //}
                    if (ds.Tables[0].Rows[i]["SectionNo"].ToString() != "")
                    {
                        model.SectionNo = long.Parse(ds.Tables[0].Rows[i]["SectionNo"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["UseDefPrint"].ToString() != "")
                    {
                        model.UseDefPrint = int.Parse(ds.Tables[0].Rows[i]["UseDefPrint"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["clientno"].ToString() != "" && ds.Tables[0].Rows[i]["clientno"].ToString() != "0" && ds.Tables[0].Rows[i]["clientno"].ToString() != null)
                    {
                        model.clientno = int.Parse(ds.Tables[0].Rows[i]["clientno"].ToString());
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
                    if (ds.Tables[0].Rows[i]["SampleTypeNo"].ToString() != "" && ds.Tables[0].Rows[i]["SampleTypeNo"].ToString() != "0")
                    {
                        model.SampleTypeNo = int.Parse(ds.Tables[0].Rows[i]["SampleTypeNo"].ToString());
                    }
                    if (ds.Tables[0].Columns.Contains("IsRFGraphdataPDf") && ds.Tables[0].Rows[i]["IsRFGraphdataPDf"].ToString() != null && ds.Tables[0].Rows[i]["IsRFGraphdataPDf"].ToString() != "") {
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
            strSql.Append("select a.SPID,a.SectionNo,a.UseDefPrint,a.PrintFormat,a.PrintProgram,a.DefPrinter,a.PNested,a.PPreview,a.FormatPara,a.TestItemNo,a.ItemCountMax,a.ItemCountMin,a.PrintOrder,a.PrintFile,a.nodename,a.microattribute,a.sicktypeno,b.CName,a.IsRFGraphdataPDf   ");
            strSql.Append(" FROM SectionPrint a left join PGroup b on a.SectionNo = b.SectionNo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            ZhiFang.Common.Log.Log.Debug(strSql.ToString());
            return DbHelperSQL.Query(strSql.ToString());
        }

        public int Update(Model.SectionPrint model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SectionPrint set ");
            if (model.SectionNo != null)
            {
                builder.Append("SectionNo=" + "'" + model.SectionNo + "',");
            }
            if (model.UseDefPrint.HasValue)
            {
                builder.Append("UseDefPrint=" + model.UseDefPrint + ",");
            }
            if (model.PrintFormat != null)
            {
                builder.Append("PrintFormat='" + model.PrintFormat + "',");
            }
            if (model.PrintProgram != null)
            {
                builder.Append("PrintProgram='" + model.PrintProgram + "',");
            }
            if (model.DefPrinter != null)
            {
                builder.Append("DefPrinter='" + model.DefPrinter + "',");
            }
            if (model.PNested.HasValue)
            {
                builder.Append("PNested=" + model.PNested + ",");
            }
            if (model.PPreview.HasValue)
            {
                builder.Append("PPreview=" + model.PPreview + ",");
            }
            if (model.FormatPara != null)
            {
                builder.Append("FormatPara='" + model.FormatPara + "',");
            }
            if (model.TestItemNo.HasValue)
            {
                builder.Append("TestItemNo=" + model.TestItemNo + ",");
            }
            if (model.ItemCountMax != null)
            {
                builder.Append("ItemCountMax=" + model.ItemCountMax + ",");
            }
            else {
                builder.Append("ItemCountMax=null,");
            }
            if (model.ItemCountMin != null)
            {
                builder.Append("ItemCountMin=" + model.ItemCountMin + ",");
            }
            else 
            {
                builder.Append("ItemCountMin=null,");
            }           
            if (model.sicktypeno.HasValue)
            {
                builder.Append("sicktypeno=" + model.sicktypeno + ",");
            }
            if (model.PrintOrder.HasValue)
            {
                builder.Append("PrintOrder=" + model.PrintOrder + ",");
            }
            if (model.PrintFile != null)
            {
                builder.Append("PrintFile=" + model.PrintFile + ",");
            }
            if (model.IsRFGraphdataPDf)
            {
                builder.Append("IsRFGraphdataPDf=" + 1 + ",");
            }
            else
            {
                builder.Append("IsRFGraphdataPDf=" + 0 + ",");
            }
            
            int startIndex = builder.ToString().LastIndexOf(",");
            builder.Remove(startIndex, 1);
            builder.Append(" where SPID=" + model.SPID);
            ZhiFang.Common.Log.Log.Debug("UpdateSectionPrint.SQL:" + builder.ToString());
            return DbHelperSQL.ExecuteSql(builder.ToString());
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

