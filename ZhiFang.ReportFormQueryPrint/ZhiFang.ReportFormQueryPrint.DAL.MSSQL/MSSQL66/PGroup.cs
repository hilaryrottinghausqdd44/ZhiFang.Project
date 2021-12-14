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
	/// 数据访问类PGroup。
	/// </summary>
    public class PGroup : IDPGroup
	{
		public PGroup()
		{}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("SectionNo", "PGroup"); 
		}


		/// <summary>
		/// 是否存在该记录
		/// </summary>
        public bool Exists(int SectionNo, int Visible)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from PGroup");
            strSql.Append(" where SectionNo=" + SectionNo + " and Visible=" + Visible + " ");
			return DbHelperSQL.Exists(strSql.ToString());
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public int Add(Model.PGroup model)
		{
			StringBuilder strSql=new StringBuilder();
			StringBuilder strSql1=new StringBuilder();
			StringBuilder strSql2=new StringBuilder();
			if (model.SectionNo != null)
			{
				strSql1.Append("SectionNo,");
				strSql2.Append(""+model.SectionNo+",");
			}
			if (model.SuperGroupNo != null)
			{
				strSql1.Append("SuperGroupNo,");
				strSql2.Append(""+model.SuperGroupNo+",");
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
			if (model.SectionDesc != null)
			{
				strSql1.Append("SectionDesc,");
				strSql2.Append("'"+model.SectionDesc+"',");
			}
			if (model.SectionType != null)
			{
				strSql1.Append("SectionType,");
				strSql2.Append(""+model.SectionType+",");
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
			if (model.onlinetime != null)
			{
				strSql1.Append("onlinetime,");
				strSql2.Append(""+model.onlinetime+",");
			}
			if (model.KeyDispOrder != null)
			{
				strSql1.Append("KeyDispOrder,");
				strSql2.Append(""+model.KeyDispOrder+",");
			}
			if (model.dummygroup != null)
			{
				strSql1.Append("dummygroup,");
				strSql2.Append(""+model.dummygroup+",");
			}
			if (model.uniontype != null)
			{
				strSql1.Append("uniontype,");
				strSql2.Append(""+model.uniontype+",");
			}
			if (model.SectorTypeNo != null)
			{
				strSql1.Append("SectorTypeNo,");
				strSql2.Append(""+model.SectorTypeNo+",");
			}
			if (model.SampleNoType != null)
			{
				strSql1.Append("SampleNoType,");
				strSql2.Append(""+model.SampleNoType+",");
			}
			if (model.IsSendGroup != null)
			{
				strSql1.Append("IsSendGroup,");
				strSql2.Append(""+model.IsSendGroup+",");
			}
			if (model.ReportSection != null)
			{
				strSql1.Append("ReportSection,");
				strSql2.Append(""+model.ReportSection+",");
			}
			strSql.Append("insert into PGroup(");
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
        public int Update(Model.PGroup model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update PGroup set ");
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
			if (model.SectionDesc != null)
			{
				strSql.Append("SectionDesc='"+model.SectionDesc+"',");
			}
			if (model.SectionType != null)
			{
				strSql.Append("SectionType="+model.SectionType+",");
			}
			if (model.Visible != null)
			{
				strSql.Append("Visible="+model.Visible+",");
			}
			if (model.DispOrder != null)
			{
				strSql.Append("DispOrder="+model.DispOrder+",");
			}
			if (model.onlinetime != null)
			{
				strSql.Append("onlinetime="+model.onlinetime+",");
			}
			if (model.KeyDispOrder != null)
			{
				strSql.Append("KeyDispOrder="+model.KeyDispOrder+",");
			}
			if (model.dummygroup != null)
			{
				strSql.Append("dummygroup="+model.dummygroup+",");
			}
			if (model.uniontype != null)
			{
				strSql.Append("uniontype="+model.uniontype+",");
			}
			if (model.SectorTypeNo != null)
			{
				strSql.Append("SectorTypeNo="+model.SectorTypeNo+",");
			}
			if (model.SampleNoType != null)
			{
				strSql.Append("SampleNoType="+model.SampleNoType+",");
			}
			if (model.IsSendGroup != null)
			{
				strSql.Append("IsSendGroup="+model.IsSendGroup+",");
			}
			if (model.ReportSection != null)
			{
				strSql.Append("ReportSection="+model.ReportSection+",");
			}
			int n = strSql.ToString().LastIndexOf(",");
			strSql.Remove(n, 1);
			strSql.Append(" where SectionNo="+ model.SectionNo+" ");
			return DbHelperSQL.ExecuteSql(strSql.ToString());
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
        public int Delete(int SectionNo, int Visible)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from PGroup ");
            strSql.Append(" where SectionNo=" + SectionNo + "  and Visible=" + Visible + " ");
			return DbHelperSQL.ExecuteSql(strSql.ToString());
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public Model.PGroup GetModel(int SectionNo, int Visible)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1  ");
			strSql.Append(" SectionNo,SuperGroupNo,CName,ShortName,ShortCode,SectionDesc,SectionType,Visible,DispOrder,onlinetime,KeyDispOrder,dummygroup,uniontype,SectorTypeNo,SampleNoType,IsSendGroup,ReportSection ");
			strSql.Append(" from PGroup ");
            strSql.Append(" where SectionNo=" + SectionNo + " and Visible=" + Visible + " ");
			Model.PGroup model=new Model.PGroup();
			DataSet ds=DbHelperSQL.Query(strSql.ToString());
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["SectionNo"].ToString()!="")
				{
					model.SectionNo=int.Parse(ds.Tables[0].Rows[0]["SectionNo"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SuperGroupNo"].ToString()!="")
				{
					model.SuperGroupNo=int.Parse(ds.Tables[0].Rows[0]["SuperGroupNo"].ToString());
				}
				model.CName=ds.Tables[0].Rows[0]["CName"].ToString();
				model.ShortName=ds.Tables[0].Rows[0]["ShortName"].ToString();
				model.ShortCode=ds.Tables[0].Rows[0]["ShortCode"].ToString();
				model.SectionDesc=ds.Tables[0].Rows[0]["SectionDesc"].ToString();
				if(ds.Tables[0].Rows[0]["SectionType"].ToString()!="")
				{
					model.SectionType=int.Parse(ds.Tables[0].Rows[0]["SectionType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Visible"].ToString()!="")
				{
					model.Visible=int.Parse(ds.Tables[0].Rows[0]["Visible"].ToString());
				}
				if(ds.Tables[0].Rows[0]["DispOrder"].ToString()!="")
				{
					model.DispOrder=int.Parse(ds.Tables[0].Rows[0]["DispOrder"].ToString());
				}
				if(ds.Tables[0].Rows[0]["onlinetime"].ToString()!="")
				{
					model.onlinetime=int.Parse(ds.Tables[0].Rows[0]["onlinetime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["KeyDispOrder"].ToString()!="")
				{
					model.KeyDispOrder=int.Parse(ds.Tables[0].Rows[0]["KeyDispOrder"].ToString());
				}
				if(ds.Tables[0].Rows[0]["dummygroup"].ToString()!="")
				{
					model.dummygroup=int.Parse(ds.Tables[0].Rows[0]["dummygroup"].ToString());
				}
				if(ds.Tables[0].Rows[0]["uniontype"].ToString()!="")
				{
					model.uniontype=int.Parse(ds.Tables[0].Rows[0]["uniontype"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SectorTypeNo"].ToString()!="")
				{
					model.SectorTypeNo=int.Parse(ds.Tables[0].Rows[0]["SectorTypeNo"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SampleNoType"].ToString()!="")
				{
					model.SampleNoType=int.Parse(ds.Tables[0].Rows[0]["SampleNoType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsSendGroup"].ToString()!="")
				{
					model.IsSendGroup=int.Parse(ds.Tables[0].Rows[0]["IsSendGroup"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ReportSection"].ToString()!="")
				{
					model.ReportSection=int.Parse(ds.Tables[0].Rows[0]["ReportSection"].ToString());
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
			strSql.Append("select SectionNo,SuperGroupNo,CName,ShortName,ShortCode,SectionDesc,SectionType,Visible,DispOrder,onlinetime,KeyDispOrder,dummygroup,uniontype,SectorTypeNo,SampleNoType,IsSendGroup,ReportSection ");
            strSql.Append(" FROM PGroup where 1=1");
            /**if(strWhere !=null && strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}*/
            if (strWhere != null && strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
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
			strSql.Append(" SectionNo,SuperGroupNo,CName,ShortName,ShortCode,SectionDesc,SectionType,Visible,DispOrder,onlinetime,KeyDispOrder,dummygroup,uniontype,SectorTypeNo,SampleNoType,IsSendGroup,ReportSection ");
			strSql.Append(" FROM PGroup ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

        public DataSet GetList(Model.PGroup model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SectionNo,SuperGroupNo,CName,ShortName,ShortCode,SectionDesc,SectionType,Visible,DispOrder,onlinetime,KeyDispOrder,dummygroup,uniontype,SectorTypeNo,SampleNoType,IsSendGroup,ReportSection ");
            strSql.Append(" FROM PGroup ");
            if (model.SuperGroupNo != null)
            {
                strSql.Append(" and SuperGroupNo=" + model.SuperGroupNo + "");
            }
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
            if (model.SectionDesc != null)
            {
                strSql.Append(" and SectionDesc='" + model.SectionDesc + "'");
            }
            if (model.SectionType != null)
            {
                strSql.Append(" and SectionType=" + model.SectionType + "");
            }
            if (model.DispOrder != null)
            {
                strSql.Append(" and DispOrder=" + model.DispOrder + "");
            }
            if (model.onlinetime != null)
            {
                strSql.Append(" and onlinetime=" + model.onlinetime + "");
            }
            if (model.KeyDispOrder != null)
            {
                strSql.Append(" and KeyDispOrder=" + model.KeyDispOrder + "");
            }
            if (model.dummygroup != null)
            {
                strSql.Append(" and dummygroup=" + model.dummygroup + "");
            }
            if (model.uniontype != null)
            {
                strSql.Append(" and uniontype=" + model.uniontype + "");
            }
            if (model.SectorTypeNo != null)
            {
                strSql.Append(" and SectorTypeNo=" + model.SectorTypeNo + "");
            }
            if (model.SectionNo != null)
            {
                strSql.Append(" and SectionNo=" + model.SectionNo + "");
            }
            if (model.Visible != null)
            {
                strSql.Append(" and Visible=" + model.Visible + "");
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion

        #region IDPGroup 成员


        public Model.PGroup GetModel(string ClientNo, int SectionNo, int Visible)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(" SectionNo,SuperGroupNo,CName,SectionType,Visible");
            strSql.Append(" from PGroup ");
            strSql.Append(" where SectionNo=" + SectionNo + " and Visible=" + Visible + " ");
            Model.PGroup model = new Model.PGroup();
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["SectionNo"].ToString() != "")
                {
                    model.SectionNo = int.Parse(ds.Tables[0].Rows[0]["SectionNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SuperGroupNo"].ToString() != "")
                {
                    model.SuperGroupNo = int.Parse(ds.Tables[0].Rows[0]["SuperGroupNo"].ToString());
                }
                model.CName = ds.Tables[0].Rows[0]["CName"].ToString();
                if (ds.Tables[0].Rows[0]["SectionType"].ToString() != "")
                {
                    model.SectionType = int.Parse(ds.Tables[0].Rows[0]["SectionType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Visible"].ToString() != "")
                {
                    model.Visible = int.Parse(ds.Tables[0].Rows[0]["Visible"].ToString());
                }
                if (ClientNo != null && ClientNo != "")
                {
                    model.ClientNo = ClientNo;
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}

