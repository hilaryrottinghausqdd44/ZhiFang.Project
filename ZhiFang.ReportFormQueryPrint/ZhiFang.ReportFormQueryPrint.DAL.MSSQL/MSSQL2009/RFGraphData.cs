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
	/// 数据访问类RFGraphData。
	/// </summary>
    public class RFGraphData : IDRFGraphData
	{
		public RFGraphData()
		{}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("GraphNo", "RFGraphData"); 
		}


		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string GraphName,int GraphNo,string FormNo)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from RFGraphData");
			strSql.Append(" where GraphName='"+GraphName+"' and GraphNo="+GraphNo+" and FormNo="+FormNo+" ");
			return DbHelperSQL.Exists(strSql.ToString());
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public int Add(Model.RFGraphData model)
		{
			StringBuilder strSql=new StringBuilder();
			StringBuilder strSql1=new StringBuilder();
			StringBuilder strSql2=new StringBuilder();
			if (model.GraphName != null)
			{
				strSql1.Append("GraphName,");
				strSql2.Append("'"+model.GraphName+"',");
			}
			if (model.GraphNo != null)
			{
				strSql1.Append("GraphNo,");
				strSql2.Append(""+model.GraphNo+",");
			}
			if (model.EquipNo != null)
			{
				strSql1.Append("EquipNo,");
				strSql2.Append(""+model.EquipNo+",");
			}
			if (model.PointType != null)
			{
				strSql1.Append("PointType,");
				strSql2.Append(""+model.PointType+",");
			}
			if (model.ShowPoint != null)
			{
				strSql1.Append("ShowPoint,");
				strSql2.Append(""+model.ShowPoint+",");
			}
			if (model.MColor != null)
			{
				strSql1.Append("MColor,");
				strSql2.Append(""+model.MColor+",");
			}
			if (model.SColor != null)
			{
				strSql1.Append("SColor,");
				strSql2.Append("'"+model.SColor+"',");
			}
			if (model.ShowAxis != null)
			{
				strSql1.Append("ShowAxis,");
				strSql2.Append(""+model.ShowAxis+",");
			}
			if (model.ShowLable != null)
			{
				strSql1.Append("ShowLable,");
				strSql2.Append(""+model.ShowLable+",");
			}
			if (model.MinX != null)
			{
				strSql1.Append("MinX,");
				strSql2.Append(""+model.MinX+",");
			}
			if (model.MaxX != null)
			{
				strSql1.Append("MaxX,");
				strSql2.Append(""+model.MaxX+",");
			}
			if (model.MinY != null)
			{
				strSql1.Append("MinY,");
				strSql2.Append(""+model.MinY+",");
			}
			if (model.MaxY != null)
			{
				strSql1.Append("MaxY,");
				strSql2.Append(""+model.MaxY+",");
			}
			if (model.ShowTitle != null)
			{
				strSql1.Append("ShowTitle,");
				strSql2.Append(""+model.ShowTitle+",");
			}
			if (model.STitle != null)
			{
				strSql1.Append("STitle,");
				strSql2.Append("'"+model.STitle+"',");
			}
			if (model.GraphValue != null)
			{
				strSql1.Append("GraphValue,");
				strSql2.Append("'"+model.GraphValue+"',");
			}
			if (model.GraphMemo != null)
			{
				strSql1.Append("GraphMemo,");
				strSql2.Append("'"+model.GraphMemo+"',");
			}
			if (model.GraphF1 != null)
			{
				strSql1.Append("GraphF1,");
				strSql2.Append("'"+model.GraphF1+"',");
			}
			if (model.GraphF2 != null)
			{
				strSql1.Append("GraphF2,");
				strSql2.Append("'"+model.GraphF2+"',");
			}
			if (model.ChartTop != null)
			{
				strSql1.Append("ChartTop,");
				strSql2.Append(""+model.ChartTop+",");
			}
			if (model.ChartHeight != null)
			{
				strSql1.Append("ChartHeight,");
				strSql2.Append(""+model.ChartHeight+",");
			}
			if (model.ChartLeft != null)
			{
				strSql1.Append("ChartLeft,");
				strSql2.Append(""+model.ChartLeft+",");
			}
			if (model.ChartWidth != null)
			{
				strSql1.Append("ChartWidth,");
				strSql2.Append(""+model.ChartWidth+",");
			}
			if (model.Graphjpg != null)
			{
				strSql1.Append("Graphjpg,");
				strSql2.Append(""+model.Graphjpg+",");
			}
			if (model.FormNo != null)
			{
				strSql1.Append("FormNo,");
				strSql2.Append(""+model.FormNo+",");
			}
			strSql.Append("insert into RFGraphData(");
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
        public int Update(Model.RFGraphData model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update RFGraphData set ");
			if (model.EquipNo != null)
			{
				strSql.Append("EquipNo="+model.EquipNo+",");
			}
			if (model.PointType != null)
			{
				strSql.Append("PointType="+model.PointType+",");
			}
			if (model.ShowPoint != null)
			{
				strSql.Append("ShowPoint="+model.ShowPoint+",");
			}
			if (model.MColor != null)
			{
				strSql.Append("MColor="+model.MColor+",");
			}
			if (model.SColor != null)
			{
				strSql.Append("SColor='"+model.SColor+"',");
			}
			if (model.ShowAxis != null)
			{
				strSql.Append("ShowAxis="+model.ShowAxis+",");
			}
			if (model.ShowLable != null)
			{
				strSql.Append("ShowLable="+model.ShowLable+",");
			}
			if (model.MinX != null)
			{
				strSql.Append("MinX="+model.MinX+",");
			}
			if (model.MaxX != null)
			{
				strSql.Append("MaxX="+model.MaxX+",");
			}
			if (model.MinY != null)
			{
				strSql.Append("MinY="+model.MinY+",");
			}
			if (model.MaxY != null)
			{
				strSql.Append("MaxY="+model.MaxY+",");
			}
			if (model.ShowTitle != null)
			{
				strSql.Append("ShowTitle="+model.ShowTitle+",");
			}
			if (model.STitle != null)
			{
				strSql.Append("STitle='"+model.STitle+"',");
			}
			if (model.GraphValue != null)
			{
				strSql.Append("GraphValue='"+model.GraphValue+"',");
			}
			if (model.GraphMemo != null)
			{
				strSql.Append("GraphMemo='"+model.GraphMemo+"',");
			}
			if (model.GraphF1 != null)
			{
				strSql.Append("GraphF1='"+model.GraphF1+"',");
			}
			if (model.GraphF2 != null)
			{
				strSql.Append("GraphF2='"+model.GraphF2+"',");
			}
			if (model.ChartTop != null)
			{
				strSql.Append("ChartTop="+model.ChartTop+",");
			}
			if (model.ChartHeight != null)
			{
				strSql.Append("ChartHeight="+model.ChartHeight+",");
			}
			if (model.ChartLeft != null)
			{
				strSql.Append("ChartLeft="+model.ChartLeft+",");
			}
			if (model.ChartWidth != null)
			{
				strSql.Append("ChartWidth="+model.ChartWidth+",");
			}
			if (model.Graphjpg != null)
			{
				strSql.Append("Graphjpg="+model.Graphjpg+",");
			}
			int n = strSql.ToString().LastIndexOf(",");
			strSql.Remove(n, 1);
			strSql.Append(" where GraphName='"+ model.GraphName+"' and GraphNo="+ model.GraphNo+" and FormNo="+ model.FormNo+" ");
            return DbHelperSQL.ExecuteSql(strSql.ToString());
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
        public int Delete(string GraphName, int GraphNo, string FormNo)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from RFGraphData ");
			strSql.Append(" where GraphName='"+GraphName+"' and GraphNo="+GraphNo+" and FormNo="+FormNo+" " );
            return DbHelperSQL.ExecuteSql(strSql.ToString());
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public Model.RFGraphData GetModel(string GraphName, int GraphNo, string FormNo)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1  ");
			strSql.Append(" GraphName,GraphNo,EquipNo,PointType,ShowPoint,MColor,SColor,ShowAxis,ShowLable,MinX,MaxX,MinY,MaxY,ShowTitle,STitle,GraphValue,GraphMemo,GraphF1,GraphF2,ChartTop,ChartHeight,ChartLeft,ChartWidth,Graphjpg,FormNo ");
			strSql.Append(" from RFGraphData ");
			strSql.Append(" where GraphName='"+GraphName+"' and GraphNo="+GraphNo+" and FormNo="+FormNo+" " );
			Model.RFGraphData model=new Model.RFGraphData();
			DataSet ds=DbHelperSQL.Query(strSql.ToString());
			if(ds.Tables[0].Rows.Count>0)
			{
				model.GraphName=ds.Tables[0].Rows[0]["GraphName"].ToString();
				if(ds.Tables[0].Rows[0]["GraphNo"].ToString()!="")
				{
					model.GraphNo=int.Parse(ds.Tables[0].Rows[0]["GraphNo"].ToString());
				}
				if(ds.Tables[0].Rows[0]["EquipNo"].ToString()!="")
				{
					model.EquipNo=int.Parse(ds.Tables[0].Rows[0]["EquipNo"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PointType"].ToString()!="")
				{
					model.PointType=int.Parse(ds.Tables[0].Rows[0]["PointType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ShowPoint"].ToString()!="")
				{
					model.ShowPoint=int.Parse(ds.Tables[0].Rows[0]["ShowPoint"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MColor"].ToString()!="")
				{
					model.MColor=int.Parse(ds.Tables[0].Rows[0]["MColor"].ToString());
				}
				model.SColor=ds.Tables[0].Rows[0]["SColor"].ToString();
				if(ds.Tables[0].Rows[0]["ShowAxis"].ToString()!="")
				{
					model.ShowAxis=int.Parse(ds.Tables[0].Rows[0]["ShowAxis"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ShowLable"].ToString()!="")
				{
					model.ShowLable=int.Parse(ds.Tables[0].Rows[0]["ShowLable"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MinX"].ToString()!="")
				{
					model.MinX=decimal.Parse(ds.Tables[0].Rows[0]["MinX"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MaxX"].ToString()!="")
				{
					model.MaxX=decimal.Parse(ds.Tables[0].Rows[0]["MaxX"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MinY"].ToString()!="")
				{
					model.MinY=decimal.Parse(ds.Tables[0].Rows[0]["MinY"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MaxY"].ToString()!="")
				{
					model.MaxY=decimal.Parse(ds.Tables[0].Rows[0]["MaxY"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ShowTitle"].ToString()!="")
				{
					model.ShowTitle=int.Parse(ds.Tables[0].Rows[0]["ShowTitle"].ToString());
				}
				model.STitle=ds.Tables[0].Rows[0]["STitle"].ToString();
				model.GraphValue=ds.Tables[0].Rows[0]["GraphValue"].ToString();
				model.GraphMemo=ds.Tables[0].Rows[0]["GraphMemo"].ToString();
				model.GraphF1=ds.Tables[0].Rows[0]["GraphF1"].ToString();
				model.GraphF2=ds.Tables[0].Rows[0]["GraphF2"].ToString();
				if(ds.Tables[0].Rows[0]["ChartTop"].ToString()!="")
				{
					model.ChartTop=int.Parse(ds.Tables[0].Rows[0]["ChartTop"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ChartHeight"].ToString()!="")
				{
					model.ChartHeight=int.Parse(ds.Tables[0].Rows[0]["ChartHeight"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ChartLeft"].ToString()!="")
				{
					model.ChartLeft=int.Parse(ds.Tables[0].Rows[0]["ChartLeft"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ChartWidth"].ToString()!="")
				{
					model.ChartWidth=int.Parse(ds.Tables[0].Rows[0]["ChartWidth"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Graphjpg"].ToString()!="")
				{
					model.Graphjpg=(byte[])ds.Tables[0].Rows[0]["Graphjpg"];
				}
				if(ds.Tables[0].Rows[0]["FormNo"].ToString()!="")
				{
					model.FormNo=ds.Tables[0].Rows[0]["FormNo"].ToString();
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
			strSql.Append("select GraphName,GraphNo,EquipNo,PointType,ShowPoint,MColor,SColor,ShowAxis,ShowLable,MinX,MaxX,MinY,MaxY,ShowTitle,STitle,GraphValue,GraphMemo,GraphF1,GraphF2,ChartTop,ChartHeight,ChartLeft,ChartWidth,Graphjpg,FormNo ");
			strSql.Append(" FROM RFGraphData ");
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
			strSql.Append(" GraphName,GraphNo,EquipNo,PointType,ShowPoint,MColor,SColor,ShowAxis,ShowLable,MinX,MaxX,MinY,MaxY,ShowTitle,STitle,GraphValue,GraphMemo,GraphF1,GraphF2,ChartTop,ChartHeight,ChartLeft,ChartWidth,Graphjpg,FormNo ");
			strSql.Append(" FROM RFGraphData ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		*/
        public DataSet GetList(Model.RFGraphData model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select GraphName,GraphNo,EquipNo,PointType,ShowPoint,MColor,SColor,ShowAxis,ShowLable,MinX,MaxX,MinY,MaxY,ShowTitle,STitle,GraphValue,GraphMemo,GraphF1,GraphF2,ChartTop,ChartHeight,ChartLeft,ChartWidth,Graphjpg,FormNo ");
            strSql.Append(" FROM RFGraphData where 1=1 ");
            if (model.EquipNo != null)
            {
                strSql.Append(" and EquipNo=" + model.EquipNo + "");
            }
            if (model.PointType != null)
            {
                strSql.Append(" and PointType=" + model.PointType + "");
            }
            if (model.ShowPoint != null)
            {
                strSql.Append(" and ShowPoint=" + model.ShowPoint + "");
            }
            if (model.MColor != null)
            {
                strSql.Append(" and MColor=" + model.MColor + "");
            }
            if (model.SColor != null)
            {
                strSql.Append(" and SColor='" + model.SColor + "'");
            }
            if (model.ShowAxis != null)
            {
                strSql.Append(" and ShowAxis=" + model.ShowAxis + "");
            }
            if (model.ShowLable != null)
            {
                strSql.Append(" and ShowLable=" + model.ShowLable + "");
            }
            if (model.MinX != null)
            {
                strSql.Append(" and MinX=" + model.MinX + "");
            }
            if (model.MaxX != null)
            {
                strSql.Append(" and MaxX=" + model.MaxX + "");
            }
            if (model.MinY != null)
            {
                strSql.Append(" and MinY=" + model.MinY + "");
            }
            if (model.MaxY != null)
            {
                strSql.Append(" and MaxY=" + model.MaxY + "");
            }
            if (model.ShowTitle != null)
            {
                strSql.Append(" and ShowTitle=" + model.ShowTitle + "");
            }
            if (model.STitle != null)
            {
                strSql.Append(" and STitle='" + model.STitle + "'");
            }
            if (model.GraphValue != null)
            {
                strSql.Append(" and GraphValue='" + model.GraphValue + "'");
            }
            if (model.GraphMemo != null)
            {
                strSql.Append(" and GraphMemo='" + model.GraphMemo + "'");
            }
            if (model.GraphF1 != null)
            {
                strSql.Append(" and GraphF1='" + model.GraphF1 + "'");
            }
            if (model.GraphF2 != null)
            {
                strSql.Append(" and GraphF2='" + model.GraphF2 + "'");
            }
            if (model.ChartTop != null)
            {
                strSql.Append(" and ChartTop=" + model.ChartTop + "");
            }
            if (model.ChartHeight != null)
            {
                strSql.Append(" and ChartHeight=" + model.ChartHeight + "");
            }
            if (model.ChartLeft != null)
            {
                strSql.Append(" and ChartLeft=" + model.ChartLeft + "");
            }
            if (model.ChartWidth != null)
            {
                strSql.Append(" and ChartWidth=" + model.ChartWidth + "");
            }
            if (model.Graphjpg != null)
            {
                strSql.Append(" and Graphjpg=" + model.Graphjpg + "");
            }
            if (model.GraphName != null)
            {
                strSql.Append(" and GraphName=" + model.GraphName + "");
            }
            if (model.GraphNo != null)
            {
                strSql.Append(" and GraphNo=" + model.GraphNo + "");
            }
            if (model.FormNo != null)
            {
                strSql.Append(" and FormNo=" + model.FormNo + "");
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion




        public DataSet GetListByReportFormId(string ReportFormId)
        {
            return this.GetList(" FormNo ='" + ReportFormId + "' ");
        }


        public DataSet GetListByReportPublicationID(string ReportPublicationID)
        {
            throw new NotImplementedException();
        }
    }
}

