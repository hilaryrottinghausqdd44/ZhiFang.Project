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
	/// 数据访问类CLIENTELE。
	/// </summary>
    public class CLIENTELE : IDCLIENTELE
	{
		public CLIENTELE()
		{}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ClIENTNO", "CLIENTELE"); 
		}


		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ClIENTNO)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from CLIENTELE");
			strSql.Append(" where ClIENTNO="+ClIENTNO+" ");
			return DbHelperSQL.Exists(strSql.ToString());
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public int Add(Model.CLIENTELE model)
		{
			StringBuilder strSql=new StringBuilder();
			StringBuilder strSql1=new StringBuilder();
			StringBuilder strSql2=new StringBuilder();
			if (model.ClIENTNO != null)
			{
				strSql1.Append("ClIENTNO,");
				strSql2.Append(""+model.ClIENTNO+",");
			}
			if (model.CNAME != null)
			{
				strSql1.Append("CNAME,");
				strSql2.Append("'"+model.CNAME+"',");
			}
			if (model.ENAME != null)
			{
				strSql1.Append("ENAME,");
				strSql2.Append("'"+model.ENAME+"',");
			}
			if (model.SHORTCODE != null)
			{
				strSql1.Append("SHORTCODE,");
				strSql2.Append("'"+model.SHORTCODE+"',");
			}
			if (model.ISUSE != null)
			{
				strSql1.Append("ISUSE,");
				strSql2.Append(""+model.ISUSE+",");
			}
			if (model.LINKMAN != null)
			{
				strSql1.Append("LINKMAN,");
				strSql2.Append("'"+model.LINKMAN+"',");
			}
			if (model.PHONENUM1 != null)
			{
				strSql1.Append("PHONENUM1,");
				strSql2.Append("'"+model.PHONENUM1+"',");
			}
			if (model.ADDRESS != null)
			{
				strSql1.Append("ADDRESS,");
				strSql2.Append("'"+model.ADDRESS+"',");
			}
			if (model.MAILNO != null)
			{
				strSql1.Append("MAILNO,");
				strSql2.Append("'"+model.MAILNO+"',");
			}
			if (model.EMAIL != null)
			{
				strSql1.Append("EMAIL,");
				strSql2.Append("'"+model.EMAIL+"',");
			}
			if (model.PRINCIPAL != null)
			{
				strSql1.Append("PRINCIPAL,");
				strSql2.Append("'"+model.PRINCIPAL+"',");
			}
			if (model.PHONENUM2 != null)
			{
				strSql1.Append("PHONENUM2,");
				strSql2.Append("'"+model.PHONENUM2+"',");
			}
			if (model.CLIENTTYPE != null)
			{
				strSql1.Append("CLIENTTYPE,");
				strSql2.Append(""+model.CLIENTTYPE+",");
			}
			if (model.bmanno != null)
			{
				strSql1.Append("bmanno,");
				strSql2.Append(""+model.bmanno+",");
			}
			if (model.romark != null)
			{
				strSql1.Append("romark,");
				strSql2.Append("'"+model.romark+"',");
			}
			if (model.titletype != null)
			{
				strSql1.Append("titletype,");
				strSql2.Append(""+model.titletype+",");
			}
			strSql.Append("insert into CLIENTELE(");
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
        public int Update(Model.CLIENTELE model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update CLIENTELE set ");
			if (model.CNAME != null)
			{
				strSql.Append("CNAME='"+model.CNAME+"',");
			}
			if (model.ENAME != null)
			{
				strSql.Append("ENAME='"+model.ENAME+"',");
			}
			if (model.SHORTCODE != null)
			{
				strSql.Append("SHORTCODE='"+model.SHORTCODE+"',");
			}
			if (model.ISUSE != null)
			{
				strSql.Append("ISUSE="+model.ISUSE+",");
			}
			if (model.LINKMAN != null)
			{
				strSql.Append("LINKMAN='"+model.LINKMAN+"',");
			}
			if (model.PHONENUM1 != null)
			{
				strSql.Append("PHONENUM1='"+model.PHONENUM1+"',");
			}
			if (model.ADDRESS != null)
			{
				strSql.Append("ADDRESS='"+model.ADDRESS+"',");
			}
			if (model.MAILNO != null)
			{
				strSql.Append("MAILNO='"+model.MAILNO+"',");
			}
			if (model.EMAIL != null)
			{
				strSql.Append("EMAIL='"+model.EMAIL+"',");
			}
			if (model.PRINCIPAL != null)
			{
				strSql.Append("PRINCIPAL='"+model.PRINCIPAL+"',");
			}
			if (model.PHONENUM2 != null)
			{
				strSql.Append("PHONENUM2='"+model.PHONENUM2+"',");
			}
			if (model.CLIENTTYPE != null)
			{
				strSql.Append("CLIENTTYPE="+model.CLIENTTYPE+",");
			}
			if (model.bmanno != null)
			{
				strSql.Append("bmanno="+model.bmanno+",");
			}
			if (model.romark != null)
			{
				strSql.Append("romark='"+model.romark+"',");
			}
			if (model.titletype != null)
			{
				strSql.Append("titletype="+model.titletype+",");
			}
			int n = strSql.ToString().LastIndexOf(",");
			strSql.Remove(n, 1);
			strSql.Append(" where ClIENTNO="+ model.ClIENTNO+" ");
            return DbHelperSQL.ExecuteSql(strSql.ToString());
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
        public int Delete(int ClIENTNO)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from CLIENTELE ");
			strSql.Append(" where ClIENTNO="+ClIENTNO+" " );
            return DbHelperSQL.ExecuteSql(strSql.ToString());
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Model.CLIENTELE GetModel(int ClIENTNO)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1  ");
			strSql.Append(" ClIENTNO,CNAME,ENAME,SHORTCODE,ISUSE,LINKMAN,PHONENUM1,ADDRESS,MAILNO,EMAIL,PRINCIPAL,PHONENUM2,CLIENTTYPE,bmanno,romark,titletype ");
			strSql.Append(" from CLIENTELE ");
			strSql.Append(" where ClIENTNO="+ClIENTNO+" " );
			Model.CLIENTELE model=new Model.CLIENTELE();
			DataSet ds=DbHelperSQL.Query(strSql.ToString());
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ClIENTNO"].ToString()!="")
				{
					model.ClIENTNO=int.Parse(ds.Tables[0].Rows[0]["ClIENTNO"].ToString());
				}
				model.CNAME=ds.Tables[0].Rows[0]["CNAME"].ToString();
				model.ENAME=ds.Tables[0].Rows[0]["ENAME"].ToString();
				model.SHORTCODE=ds.Tables[0].Rows[0]["SHORTCODE"].ToString();
				if(ds.Tables[0].Rows[0]["ISUSE"].ToString()!="")
				{
					model.ISUSE=int.Parse(ds.Tables[0].Rows[0]["ISUSE"].ToString());
				}
				model.LINKMAN=ds.Tables[0].Rows[0]["LINKMAN"].ToString();
				model.PHONENUM1=ds.Tables[0].Rows[0]["PHONENUM1"].ToString();
				model.ADDRESS=ds.Tables[0].Rows[0]["ADDRESS"].ToString();
				model.MAILNO=ds.Tables[0].Rows[0]["MAILNO"].ToString();
				model.EMAIL=ds.Tables[0].Rows[0]["EMAIL"].ToString();
				model.PRINCIPAL=ds.Tables[0].Rows[0]["PRINCIPAL"].ToString();
				model.PHONENUM2=ds.Tables[0].Rows[0]["PHONENUM2"].ToString();
				if(ds.Tables[0].Rows[0]["CLIENTTYPE"].ToString()!="")
				{
					model.CLIENTTYPE=int.Parse(ds.Tables[0].Rows[0]["CLIENTTYPE"].ToString());
				}
				if(ds.Tables[0].Rows[0]["bmanno"].ToString()!="")
				{
					model.bmanno=int.Parse(ds.Tables[0].Rows[0]["bmanno"].ToString());
				}
				model.romark=ds.Tables[0].Rows[0]["romark"].ToString();
				if(ds.Tables[0].Rows[0]["titletype"].ToString()!="")
				{
					model.titletype=int.Parse(ds.Tables[0].Rows[0]["titletype"].ToString());
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
			strSql.Append("select ClIENTNO,CNAME,ENAME,SHORTCODE,ISUSE,LINKMAN,PHONENUM1,ADDRESS,MAILNO,EMAIL,PRINCIPAL,PHONENUM2,CLIENTTYPE,bmanno,romark,titletype ");
			strSql.Append(" FROM CLIENTELE ");
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
			strSql.Append(" ClIENTNO,CNAME,ENAME,SHORTCODE,ISUSE,LINKMAN,PHONENUM1,ADDRESS,MAILNO,EMAIL,PRINCIPAL,PHONENUM2,CLIENTTYPE,bmanno,romark,titletype ");
			strSql.Append(" FROM CLIENTELE ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		*/


        public DataSet GetList(Model.CLIENTELE model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ClIENTNO,CNAME,ENAME,SHORTCODE,ISUSE,LINKMAN,PHONENUM1,ADDRESS,MAILNO,EMAIL,PRINCIPAL,PHONENUM2,CLIENTTYPE,bmanno,romark,titletype,uploadtype,tstamp,printtype,InputDataType,reportpagetype,clientarea,clientstyle,FaxNo,AutoFax,ClientReportTitle,IsPrintItem,CZDY1,CZDY2,CZDY3,CZDY4,CZDY5,CZDY6 ");
            strSql.Append(" FROM CLIENTELE where 1=1 ");
            if (model.CNAME != null)
            {
                strSql.Append(" and CNAME='" + model.CNAME + "'");
            }
            if (model.ENAME != null)
            {
                strSql.Append(" and ENAME='" + model.ENAME + "'");
            }
            if (model.SHORTCODE != null)
            {
                strSql.Append(" and SHORTCODE='" + model.SHORTCODE + "'");
            }
            if (model.ISUSE != null)
            {
                strSql.Append(" and ISUSE=" + model.ISUSE + "");
            }
            if (model.LINKMAN != null)
            {
                strSql.Append(" and LINKMAN='" + model.LINKMAN + "'");
            }
            if (model.PHONENUM1 != null)
            {
                strSql.Append(" and PHONENUM1='" + model.PHONENUM1 + "'");
            }
            if (model.ADDRESS != null)
            {
                strSql.Append(" and ADDRESS='" + model.ADDRESS + "'");
            }
            if (model.MAILNO != null)
            {
                strSql.Append(" and MAILNO='" + model.MAILNO + "'");
            }
            if (model.EMAIL != null)
            {
                strSql.Append(" and EMAIL='" + model.EMAIL + "'");
            }
            if (model.PRINCIPAL != null)
            {
                strSql.Append(" and PRINCIPAL='" + model.PRINCIPAL + "'");
            }
            if (model.PHONENUM2 != null)
            {
                strSql.Append(" and PHONENUM2='" + model.PHONENUM2 + "'");
            }
            if (model.bmanno != null)
            {
                strSql.Append(" and bmanno=" + model.bmanno + "");
            }
            if (model.romark != null)
            {
                strSql.Append(" and romark='" + model.romark + "'");
            }
            if (model.titletype != null)
            {
                strSql.Append(" and titletype=" + model.titletype + "");
            }
            if (model.uploadtype != null)
            {
                strSql.Append(" and uploadtype=" + model.uploadtype + "");
            }
            if (model.printtype != null)
            {
                strSql.Append(" and printtype=" + model.printtype + "");
            }
            if (model.InputDataType != null)
            {
                strSql.Append(" and InputDataType=" + model.InputDataType + "");
            }
            if (model.reportpagetype != null)
            {
                strSql.Append(" and reportpagetype=" + model.reportpagetype + "");
            }
            if (model.clientarea != null)
            {
                strSql.Append(" and clientarea='" + model.clientarea + "'");
            }
            if (model.clientstyle != null)
            {
                strSql.Append(" and clientstyle='" + model.clientstyle + "'");
            }
            if (model.FaxNo != null)
            {
                strSql.Append(" and FaxNo='" + model.FaxNo + "'");
            }
            if (model.AutoFax != null)
            {
                strSql.Append(" and AutoFax=" + model.AutoFax + "");
            }
            if (model.ClientReportTitle != null)
            {
                strSql.Append(" and ClientReportTitle='" + model.ClientReportTitle + "'");
            }
            if (model.IsPrintItem != null)
            {
                strSql.Append(" and IsPrintItem=" + model.IsPrintItem + "");
            }
            if (model.CZDY1 != null)
            {
                strSql.Append(" and CZDY1='" + model.CZDY1 + "'");
            }
            if (model.CZDY2 != null)
            {
                strSql.Append(" and CZDY2='" + model.CZDY2 + "'");
            }
            if (model.CZDY3 != null)
            {
                strSql.Append(" and CZDY3='" + model.CZDY3 + "'");
            }
            if (model.CZDY4 != null)
            {
                strSql.Append(" and CZDY4='" + model.CZDY4 + "'");
            }
            if (model.CZDY5 != null)
            {
                strSql.Append(" and CZDY5='" + model.CZDY5 + "'");
            }
            if (model.CZDY6 != null)
            {
                strSql.Append(" and CZDY6='" + model.CZDY6 + "'");
            }
            if (model.ClIENTNO != null)
            {
                strSql.Append(" and ClIENTNO='" + model.ClIENTNO + "'");
            }
            if (model.CLIENTTYPE != null)
            {
                strSql.Append(" and CLIENTTYPE='" + model.CLIENTTYPE + "'");
            }
            strSql.Append(" order by ShortCode ");
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetList(string where, string fields)
        {
            StringBuilder strSql = new StringBuilder();
            if (fields == "" || fields == null){
                fields = " ClIENTNO,CNAME ";
            }
            strSql.Append("select ");
            strSql.Append(fields);
            strSql.Append(" FROM CLIENTELE ");
            if (where.Trim() != "")
            {
                strSql.Append(" where " + where);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion
    }
}

