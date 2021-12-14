using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.DBUtility;
using System.Data;
namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL2009
{
   public   class PrintHtml:IDPrintHtml 
    {
       public PrintHtml()
       { }
        #region IDPrintHtml 成员
     
       /// <summary>
       /// 是否存在该条记录
       /// </summary>
       /// <param name="Formno"></param>
       /// <returns></returns>
       public bool Exists(string Formno)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from PrintHtml");
            strSql.Append(" where FormNo='" + Formno  + "' ");
            return DbHelperSQL.Exists(strSql.ToString());
        }
       /// <summary>
       /// 删除一条记录
       /// </summary>
       /// <param name="Formno"></param>
       /// <returns></returns>
       public int Delete(string Formno)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from PrintHtml ");
            strSql.Append(" where FormNo='" + Formno  + "' ");
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }
       /// <summary>
       /// 得到对象实体
       /// </summary>
       /// <param name="Formno"></param>
       /// <returns></returns>
       public Model.PrintHtml GetModel(string Formno)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(" FormNo,SectionNo,ClietNo,PrintFormatNo,Url,Date ,BUrl ");
            strSql.Append(" from PrintHtml ");
            strSql.Append(" where FormNo='" + Formno  + "' ");
            Model.PrintHtml model = new Model.PrintHtml();
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["FormNo"].ToString() != "")
                {
                    model.Formno  = ds.Tables[0].Rows[0]["FormNo"].ToString();
                }
                model.Sectionno  =int.Parse ( ds.Tables[0].Rows[0]["SectionNo"].ToString());
                model.Clientno  =int.Parse ( ds.Tables[0].Rows[0]["ClientNo"].ToString());
                model.Printfomatno  = int.Parse (ds.Tables[0].Rows[0]["PrintFormatNo"].ToString());
                model.Url  = ds.Tables[0].Rows[0]["Url"].ToString();
                model.Date  = DateTime .Parse ( ds.Tables[0].Rows[0]["Date"].ToString());
                model.Burl = ds.Tables[0].Rows[0]["BUrl"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region IDataBase<PrintHtml> 成员

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("FormNo", "PrintHtml");
        }

        public int Add(Model.PrintHtml model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model .Formno .ToString ()!="")
            {
                strSql1.Append("FormNo,");
                strSql2.Append("'" + model.Formno+ "',");
            }
            if (model.Printfomatno .ToString()  != "")
            {
                strSql1.Append("PrintFormatNo,");
                strSql2.Append("" + model.Printfomatno  + ",");
            }
            if (model.Sectionno .ToString () != "")
            {
                strSql1.Append("SectionNo,");
                strSql2.Append("" + model.Sectionno  + ",");
            }
            if (model.Clientno .ToString() != "")
            {
                strSql1.Append("ClientNo,");
                strSql2.Append("" + model.Clientno  + ",");
            }
            if (model.Date!=null &&model .Date .ToString ()!="")
            {
                strSql1.Append("Date,");
                strSql2.Append("'" + model.Date  + "',");
            }
            if (model.Url  != "")
            {
                strSql1.Append("Url,");
                strSql2.Append("'" + model.Url  + "',");
            }
            if (model.Burl  != "")
            {
                strSql1.Append("BUrl,");
                strSql2.Append("'" + model.Burl  + "',");
            }
            strSql.Append("insert into PrintHtml(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        public int Update(Model.PrintHtml model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update PrintHtml set ");
            if (model.Formno.ToString ()  != "")
            {
                strSql.Append("FormNo='" + model.Formno  + "',");
            }
            if (model.Printfomatno.ToString ()  != "")
            {
                strSql.Append("PrintFormatNo=" + model.Printfomatno  + ",");
            }
            if (model.Sectionno.ToString ()  != "")
            {
                strSql.Append("SectionNo=" + model.Sectionno  + ",");
            }
            if (model.Clientno.ToString ()  !="")
            {
                strSql.Append("ClientNo=" + model.Clientno  + ",");
            }
            if (model.Url  != null)
            {
                strSql.Append("Url='" + model.Url  + "',");
            }
            if (model.Date  != null)
            {
                strSql.Append("Date='" + model.Date  + "',");
            }
            if (model.Burl  != null)
            {
                strSql.Append("BUrl='" + model.Burl  + "',");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where FormNo='" + model.Formno  + "' ");
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct  substring(FormNo,0,charindex('_',FormNo)) FormNo    ");
            strSql.Append(" FROM PrintHtml ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// by spf 2010-08-27
        /// </summary>
        /// <param name="formno"></param>
        /// <returns></returns>
        public DataSet GetHtmlPrintInfo(string formno)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select FormNo,BUrl ,Date   ");
            strSql.Append(" FROM PrintHtml ");
            strSql.Append(" where substring(FormNo,0,charindex('_',FormNo))="+formno +"  " ); 
            return DbHelperSQL.Query(strSql.ToString());
        }
        public DataSet GetList(Model.PrintHtml model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select FormNo,PrintFormatNo,SectionNo,ClientNo,Url,Date  ");
            strSql.Append(" FROM PrintHtml where 1=1");
            if (model.Formno .ToString()  != "")
            {
                strSql.Append(" and FormNo='" + model.Formno  + "'");
            }
            if (model.Printfomatno .ToString () !="")
            {
                strSql.Append(" and PrintFormatNo=" + model.Printfomatno  + "");
            }
            if (model.Sectionno .ToString ()!="")
            {
                strSql.Append(" and SectionNo=" + model.Sectionno  + "");
            }
            if (model.Clientno .ToString () !="")
            {
                strSql.Append(" and ClientNo=" + model.Clientno + "");
            }
            if (model .Url !=""||model .Url !=null )
            {
                strSql.Append(" and Url='" + model.Url  + "'");
            }
            if (model.Date .ToString ()!=""||model .Date !=null )
            {
                strSql.Append(" and Date='" + model.Date  + "'");
            }
            if (model.Burl  != "" || model.Burl  != null)
            {
                strSql.Append(" and BUrl='" + model.Burl  + "'");
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {

              StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" FormNo,PrintFormatNo,SectionNo,ClienNo,Url,Date,BUrl ");
            strSql.Append(" FROM FormNo");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion
    }
}
