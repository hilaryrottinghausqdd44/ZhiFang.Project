using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.DBUtility;
using System.Data;

namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL2009
{
	/// <summary>
	/// ���ݷ�����Station_PrinterList��
	/// </summary>
	public class Station_PrinterList:IDStation_PrinterList
	{
		public Station_PrinterList()
		{}
		#region  ��Ա����
        /// <summary>
        /// �õ����ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("Id", "Station_PrinterList");
        }
		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Station_PrinterList");
			strSql.Append(" where id="+id+" ");
			return DbHelperSQL.Exists(strSql.ToString());
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public int Add(Model.Station_PrinterList model)
		{
			StringBuilder strSql=new StringBuilder();
			StringBuilder strSql1=new StringBuilder();
			StringBuilder strSql2=new StringBuilder();
			if (model.StationName != null)
			{
				strSql1.Append("StationName,");
				strSql2.Append("'"+model.StationName+"',");
			}
			if (model.PrinterName != null)
			{
				strSql1.Append("PrinterName,");
				strSql2.Append("'"+model.PrinterName+"',");
			}
			if (model.PageSize != null)
			{
				strSql1.Append("PageSize,");
				strSql2.Append("'"+model.PageSize+"',");
			}
			if (model.AddTime != null)
			{
				strSql1.Append("AddTime,");
				strSql2.Append("'"+model.AddTime+"',");
			}
			if (model.Sort != null)
			{
				strSql1.Append("Sort,");
				strSql2.Append(""+model.Sort+",");
			}
			strSql.Append("insert into Station_PrinterList(");
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
		/// ����һ������
		/// </summary>
		public int Update(Model.Station_PrinterList model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Station_PrinterList set ");
			if (model.StationName != null)
			{
				strSql.Append("StationName='"+model.StationName+"',");
			}
			if (model.PrinterName != null)
			{
				strSql.Append("PrinterName='"+model.PrinterName+"',");
			}
			if (model.PageSize != null)
			{
				strSql.Append("PageSize='"+model.PageSize+"',");
			}
			if (model.AddTime != null)
			{
				strSql.Append("AddTime='"+model.AddTime+"',");
			}
			if (model.Sort != null)
			{
				strSql.Append("Sort="+model.Sort+",");
			}
			int n = strSql.ToString().LastIndexOf(",");
			strSql.Remove(n, 1);
			strSql.Append(" where id="+ model.id+" ");
			return DbHelperSQL.ExecuteSql(strSql.ToString());
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public int Delete(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Station_PrinterList ");
			strSql.Append(" where id="+id+" " );
			return DbHelperSQL.ExecuteSql(strSql.ToString());
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public Model.Station_PrinterList GetModel(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1  ");
			strSql.Append(" id,StationName,PrinterName,PageSize,AddTime,Sort ");
			strSql.Append(" from Station_PrinterList ");
			strSql.Append(" where id="+id+" " );
			Model.Station_PrinterList model=new Model.Station_PrinterList();
			DataSet ds=DbHelperSQL.Query(strSql.ToString());
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["id"].ToString()!="")
				{
					model.id=int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
				}
				model.StationName=ds.Tables[0].Rows[0]["StationName"].ToString();
				model.PrinterName=ds.Tables[0].Rows[0]["PrinterName"].ToString();
				model.PageSize=ds.Tables[0].Rows[0]["PageSize"].ToString();
				if(ds.Tables[0].Rows[0]["AddTime"].ToString()!="")
				{
					model.AddTime=DateTime.Parse(ds.Tables[0].Rows[0]["AddTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Sort"].ToString()!="")
				{
					model.Sort=int.Parse(ds.Tables[0].Rows[0]["Sort"].ToString());
				}
				return model;
			}
			else
			{
				return null;
			}
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,StationName,PrinterName,PageSize,AddTime,Sort ");
			strSql.Append(" FROM Station_PrinterList ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// ���ǰ��������
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" id,StationName,PrinterName,PageSize,AddTime,Sort ");
			strSql.Append(" FROM Station_PrinterList ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}
        public DataSet GetList(Model.Station_PrinterList model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,StationName,PrinterName,PageSize,AddTime,Sort ");
            strSql.Append(" FROM Station_PrinterList where 1=1  ");
            if (model.StationName != null)
            {
                strSql.Append(" and StationName='" + model.StationName + "'");
            }
            if (model.PrinterName != null)
            {
                strSql.Append(" and PrinterName='" + model.PrinterName + "'");
            }
            if (model.PageSize != null)
            {
                strSql.Append(" and PageSize='" + model.PageSize + "'");
            }
            if (model.AddTime != null)
            {
                strSql.Append(" and AddTime='" + model.AddTime + "'");
            }
            if (model.Sort != null)
            {
                strSql.Append(" and Sort=" + model.Sort + "");
            }
            if (model.id != null)
            {
                strSql.Append(" and id=" + model.id + "");
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion
    }
}

