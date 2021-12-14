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
	/// 数据访问类District。
	/// </summary>
	public class District:IDDistrict
	{
		public District()
		{}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("DistrictNo", "District"); 
		}


		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int DistrictNo)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from District");
			strSql.Append(" where DistrictNo="+DistrictNo+" ");
			return DbHelperSQL.Exists(strSql.ToString());
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public int Add(Model.District model)
		{
			StringBuilder strSql=new StringBuilder();
			StringBuilder strSql1=new StringBuilder();
			StringBuilder strSql2=new StringBuilder();
			if (model.DistrictNo != null)
			{
				strSql1.Append("DistrictNo,");
				strSql2.Append(""+model.DistrictNo+",");
			}
			if (model.CName != null)
			{
				strSql1.Append("CName,");
				strSql2.Append("'"+model.CName+"',");
			}
			if (model.ShortName != null)
			{
				strSql1.Append("ShortName,");
				strSql2.Append("'"+model.ShortName+"',");
			}
			if (model.ShortCode != null)
			{
				strSql1.Append("ShortCode,");
				strSql2.Append("'"+model.ShortCode+"',");
			}
			if (model.Visible != null)
			{
				strSql1.Append("Visible,");
				strSql2.Append(""+model.Visible+",");
			}
			if (model.DispOrder != null)
			{
				strSql1.Append("DispOrder,");
				strSql2.Append(""+model.DispOrder+",");
			}
			if (model.HisOrderCode != null)
			{
				strSql1.Append("HisOrderCode,");
				strSql2.Append("'"+model.HisOrderCode+"',");
			}
			strSql.Append("insert into District(");
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
        public int Update(Model.District model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update District set ");
			if (model.CName != null)
			{
				strSql.Append("CName='"+model.CName+"',");
			}
			if (model.ShortName != null)
			{
				strSql.Append("ShortName='"+model.ShortName+"',");
			}
			if (model.ShortCode != null)
			{
				strSql.Append("ShortCode='"+model.ShortCode+"',");
			}
			if (model.DispOrder != null)
			{
				strSql.Append("DispOrder="+model.DispOrder+",");
			}
			if (model.HisOrderCode != null)
			{
				strSql.Append("HisOrderCode='"+model.HisOrderCode+"',");
			}
			int n = strSql.ToString().LastIndexOf(",");
			strSql.Remove(n, 1);
			strSql.Append(" where DistrictNo="+ model.DistrictNo+" and Visible="+ model.Visible+" ");
			return DbHelperSQL.ExecuteSql(strSql.ToString());
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
        public int Delete(int DistrictNo)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from District ");
			strSql.Append(" where DistrictNo="+DistrictNo+" " );
			return DbHelperSQL.ExecuteSql(strSql.ToString());
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Model.District GetModel(int DistrictNo)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1  ");
			strSql.Append(" DistrictNo,CName,ShortName,ShortCode,Visible,DispOrder,HisOrderCode ");
			strSql.Append(" from District ");
			strSql.Append(" where DistrictNo="+DistrictNo+" " );
			Model.District model=new Model.District();
			DataSet ds=DbHelperSQL.Query(strSql.ToString());
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["DistrictNo"].ToString()!="")
				{
					model.DistrictNo=int.Parse(ds.Tables[0].Rows[0]["DistrictNo"].ToString());
				}
				model.CName=ds.Tables[0].Rows[0]["CName"].ToString();
				model.ShortName=ds.Tables[0].Rows[0]["ShortName"].ToString();
				model.ShortCode=ds.Tables[0].Rows[0]["ShortCode"].ToString();
				if(ds.Tables[0].Rows[0]["Visible"].ToString()!="")
				{
					model.Visible=int.Parse(ds.Tables[0].Rows[0]["Visible"].ToString());
				}
				if(ds.Tables[0].Rows[0]["DispOrder"].ToString()!="")
				{
					model.DispOrder=int.Parse(ds.Tables[0].Rows[0]["DispOrder"].ToString());
				}
				model.HisOrderCode=ds.Tables[0].Rows[0]["HisOrderCode"].ToString();
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
			strSql.Append("select DistrictNo,CName,ShortName,ShortCode,Visible,DispOrder,HisOrderCode ");
			strSql.Append(" FROM District ");
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
			strSql.Append(" DistrictNo,CName,ShortName,ShortCode,Visible,DispOrder,HisOrderCode ");
			strSql.Append(" FROM District ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		*/

        public DataSet GetList(Model.District model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DistrictNo,CName,ShortName,ShortCode,Visible,DispOrder,HisOrderCode  ");
            strSql.Append(" FROM District where 1=1");
            if (model.CName != null)
            {
                strSql.Append(" and CName='" + model.CName + "'");
            }
            if (model.ShortName != null)
            {
                strSql.Append(" and ShortName='" + model.ShortName + "'");
            }
            if (model.ShortCode != null)
            {
                strSql.Append(" and ShortCode='" + model.ShortCode + "'");
            }
            if (model.Visible != null)
            {
                strSql.Append(" and Visible=" + model.Visible + "");
            }
            if (model.DispOrder != null)
            {
                strSql.Append(" and DispOrder=" + model.DispOrder + "");
            }
            if (model.HisOrderCode != null)
            {
                strSql.Append(" and HisOrderCode='" + model.HisOrderCode + "'");
            }
            if (model.DistrictNo != null)
            {
                strSql.Append(" and DistrictNo='" + model.DistrictNo + "'");
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion
    }
}

