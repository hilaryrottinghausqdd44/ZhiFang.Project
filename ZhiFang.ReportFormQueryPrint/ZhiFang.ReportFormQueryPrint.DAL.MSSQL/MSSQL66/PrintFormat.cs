using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.DBUtility;
using System.Data;
namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66
{
	/// <summary>
	/// 数据访问类PrintFormat。
	/// </summary>
	public class PrintFormat:IDPrintFormat
	{
		public PrintFormat()
		{}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Id", "PrintFormat"); 
		}


		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from PrintFormat");
			strSql.Append(" where Id="+Id+" ");
			return DbHelperSQL.Exists(strSql.ToString());
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Model.PrintFormat model)
		{
			StringBuilder strSql=new StringBuilder();
			StringBuilder strSql1=new StringBuilder();
			StringBuilder strSql2=new StringBuilder();
			if (model.PrintFormatName != null)
			{
				strSql1.Append("PrintFormatName,");
				strSql2.Append("'"+model.PrintFormatName+"',");
			}
			if (model.PintFormatAddress != null)
			{
				strSql1.Append("PintFormatAddress,");
				strSql2.Append("'"+model.PintFormatAddress+"',");
			}
			if (model.PintFormatFileName != null)
			{
				strSql1.Append("PintFormatFileName,");
				strSql2.Append("'"+model.PintFormatFileName+"',");
			}
			if (model.ItemParaLineNum != null)
			{
				strSql1.Append("ItemParaLineNum,");
				strSql2.Append(""+model.ItemParaLineNum+",");
			}
			if (model.PaperSize != null)
			{
				strSql1.Append("PaperSize,");
				strSql2.Append("'"+model.PaperSize+"',");
			}
			if (model.PrintFormatDesc != null)
			{
				strSql1.Append("PrintFormatDesc,");
				strSql2.Append("'"+model.PrintFormatDesc+"',");
			}
			if (model.BatchPrint != null)
			{
				strSql1.Append("BatchPrint,");
				strSql2.Append(""+model.BatchPrint+",");
			}
            if (model.ImageFlag != null)
            {
                strSql1.Append("ImageFlag,");
                strSql2.Append("" + model.ImageFlag + ",");
            }
            if (model.AntiFlag != null)
            {
                strSql1.Append("AntiFlag,");
                strSql2.Append("" + model.AntiFlag + ",");
            }
			strSql.Append("insert into PrintFormat(");
			strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
			strSql.Append(")");
			strSql.Append(" values (");
			strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
			strSql.Append(")");
			strSql.Append(";select @@IDENTITY");
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			if (obj == null)
			{
				return 1;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public int Update(Model.PrintFormat model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update PrintFormat set ");
			if (model.PrintFormatName != null)
			{
				strSql.Append("PrintFormatName='"+model.PrintFormatName+"',");
			}
			if (model.PintFormatAddress != null)
			{
				strSql.Append("PintFormatAddress='"+model.PintFormatAddress+"',");
			}
			if (model.PintFormatFileName != null)
			{
				strSql.Append("PintFormatFileName='"+model.PintFormatFileName+"',");
			}
			if (model.ItemParaLineNum != null)
			{
				strSql.Append("ItemParaLineNum="+model.ItemParaLineNum+",");
			}
			if (model.PaperSize != null)
			{
				strSql.Append("PaperSize='"+model.PaperSize+"',");
			}
			if (model.PrintFormatDesc != null)
			{
				strSql.Append("PrintFormatDesc='"+model.PrintFormatDesc+"',");
			}
			if (model.BatchPrint != null)
			{
				strSql.Append("BatchPrint="+model.BatchPrint+",");
			}
            if (model.ImageFlag != null)
            {
                strSql.Append("ImageFlag=" + model.ImageFlag + ",");
            }
            if (model.AntiFlag != null)
            {
                strSql.Append("AntiFlag=" + model.AntiFlag + ",");
            }
			int n = strSql.ToString().LastIndexOf(",");
			strSql.Remove(n, 1);
			strSql.Append(" where Id="+ model.Id+" ");
			return DbHelperSQL.ExecuteSql(strSql.ToString());
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
        public int Delete(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from PrintFormat ");
			strSql.Append(" where Id="+Id+" " );
			return DbHelperSQL.ExecuteSql(strSql.ToString());
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Model.PrintFormat GetModel(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1  ");
            strSql.Append(" Id,PrintFormatName,PintFormatAddress,PintFormatFileName,ItemParaLineNum,PaperSize,PrintFormatDesc,BatchPrint,ImageFlag,AntiFlag ");
			strSql.Append(" from PrintFormat ");
			strSql.Append(" where Id="+Id+" " );
			Model.PrintFormat model=new Model.PrintFormat();
			DataSet ds=DbHelperSQL.Query(strSql.ToString());
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Id"].ToString()!="")
				{
					model.Id=int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
				}
				model.PrintFormatName=ds.Tables[0].Rows[0]["PrintFormatName"].ToString();
				model.PintFormatAddress=ds.Tables[0].Rows[0]["PintFormatAddress"].ToString();
				model.PintFormatFileName=ds.Tables[0].Rows[0]["PintFormatFileName"].ToString();
				if(ds.Tables[0].Rows[0]["ItemParaLineNum"].ToString()!="")
				{
					model.ItemParaLineNum=int.Parse(ds.Tables[0].Rows[0]["ItemParaLineNum"].ToString());
				}
				model.PaperSize=ds.Tables[0].Rows[0]["PaperSize"].ToString();
				model.PrintFormatDesc=ds.Tables[0].Rows[0]["PrintFormatDesc"].ToString();
				if(ds.Tables[0].Rows[0]["BatchPrint"].ToString()!="")
				{
					model.BatchPrint=int.Parse(ds.Tables[0].Rows[0]["BatchPrint"].ToString());
				}
                if (ds.Tables[0].Rows[0]["ImageFlag"].ToString() != "")
                {
                    model.ImageFlag = int.Parse(ds.Tables[0].Rows[0]["ImageFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AntiFlag"].ToString() != "")
                {
                    model.AntiFlag = int.Parse(ds.Tables[0].Rows[0]["AntiFlag"].ToString());
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
			strSql.Append("select Id,PrintFormatName,PintFormatAddress,PintFormatFileName,ItemParaLineNum,PaperSize,PrintFormatDesc,BatchPrint,ImageFlag,AntiFlag ");
			strSql.Append(" FROM PrintFormat ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(Model.PrintFormat model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,PrintFormatName,PintFormatAddress,PintFormatFileName,ItemParaLineNum,PaperSize,PrintFormatDesc,BatchPrint,ImageFlag,AntiFlag ");
            strSql.Append(" FROM PrintFormat where 1=1 ");
            if (model.PrintFormatName != null)
            {
                strSql.Append(" and PrintFormatName='" + model.PrintFormatName + "'");
            }
            if (model.PintFormatAddress != null)
            {
                strSql.Append(" and PintFormatAddress='" + model.PintFormatAddress + "'");
            }
            if (model.PintFormatFileName != null)
            {
                strSql.Append(" and PintFormatFileName='" + model.PintFormatFileName + "'");
            }
            if (model.ItemParaLineNum != null)
            {
                strSql.Append(" and ItemParaLineNum=" + model.ItemParaLineNum + "");
            }
            if (model.PaperSize != null)
            {
                strSql.Append(" and PaperSize='" + model.PaperSize + "'");
            }
            if (model.PrintFormatDesc != null)
            {
                strSql.Append(" and PrintFormatDesc='" + model.PrintFormatDesc + "'");
            }
            if (model.BatchPrint != null)
            {
                strSql.Append(" and BatchPrint=" + model.BatchPrint + "");
            }
            if (model.ImageFlag != null)
            {
                strSql.Append(" and ImageFlag=" + model.ImageFlag + "");
            }
            if (model.AntiFlag != null)
            {
                strSql.Append(" and AntiFlag=" + model.AntiFlag + "");
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
            strSql.Append(" Id,PrintFormatName,PintFormatAddress,PintFormatFileName,ItemParaLineNum,PaperSize,PrintFormatDesc,BatchPrint,ImageFlag,AntiFlag ");
			strSql.Append(" FROM PrintFormat ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		*/

		#endregion  成员方法

      
    }
}

