using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.DBUtility;
using System.Data;
using System.IO;

namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.History
{
	/// <summary>
	/// 数据访问类RFGraphData。
	/// </summary>
	public class RFGraphData
	{
		public RFGraphData()
		{}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("SectionNo", "RFGraphData"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(DateTime ReceiveDate,int SectionNo,int TestTypeNo,string SampleNo,string GraphName,int GraphNo)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from RFGraphData");
			strSql.Append(" where ReceiveDate='"+ReceiveDate+"' and SectionNo="+SectionNo+" and TestTypeNo="+TestTypeNo+" and SampleNo='"+SampleNo+"' and GraphName='"+GraphName+"' and GraphNo="+GraphNo+" ");
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
			if (model.ReceiveDate != null)
			{
				strSql1.Append("ReceiveDate,");
				strSql2.Append("'"+model.ReceiveDate+"',");
			}
			if (model.SectionNo != null)
			{
				strSql1.Append("SectionNo,");
				strSql2.Append(""+model.SectionNo+",");
			}
			if (model.TestTypeNo != null)
			{
				strSql1.Append("TestTypeNo,");
				strSql2.Append(""+model.TestTypeNo+",");
			}
			if (model.SampleNo != null)
			{
				strSql1.Append("SampleNo,");
				strSql2.Append("'"+model.SampleNo+"',");
			}
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
			strSql.Append(" where ReceiveDate='"+ model.ReceiveDate+"' and SectionNo="+ model.SectionNo+" and TestTypeNo="+ model.TestTypeNo+" and SampleNo='"+ model.SampleNo+"' and GraphName='"+ model.GraphName+"' and GraphNo="+ model.GraphNo+" ");
			return DbHelperSQL.ExecuteSql(strSql.ToString());
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
        public int Delete(DateTime ReceiveDate, int SectionNo, int TestTypeNo, string SampleNo, string GraphName, int GraphNo)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from RFGraphData ");
			strSql.Append(" where ReceiveDate='"+ReceiveDate+"' and SectionNo="+SectionNo+" and TestTypeNo="+TestTypeNo+" and SampleNo='"+SampleNo+"' and GraphName='"+GraphName+"' and GraphNo="+GraphNo+" " );
			return DbHelperSQL.ExecuteSql(strSql.ToString());
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Model.RFGraphData GetModel(DateTime ReceiveDate,int SectionNo,int TestTypeNo,string SampleNo,string GraphName,int GraphNo)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1  ");
			strSql.Append(" Convert(varchar(10),ReceiveDate,21) as ReceiveDate,SectionNo,TestTypeNo,SampleNo,GraphName,GraphNo,EquipNo,PointType,ShowPoint,MColor,SColor,ShowAxis,ShowLable,MinX,MaxX,MinY,MaxY,ShowTitle,STitle,GraphValue,GraphMemo,GraphF1,GraphF2,ChartTop,ChartHeight,ChartLeft,ChartWidth,Graphjpg ");
			strSql.Append(" from RFGraphData ");
			strSql.Append(" where ReceiveDate='"+ReceiveDate+"' and SectionNo="+SectionNo+" and TestTypeNo="+TestTypeNo+" and SampleNo='"+SampleNo+"' and GraphName='"+GraphName+"' and GraphNo="+GraphNo+" " );
			Model.RFGraphData model=new Model.RFGraphData();
			DataSet ds=DbHelperSQL.Query(strSql.ToString());
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ReceiveDate"].ToString()!="")
				{
					model.ReceiveDate=DateTime.Parse(ds.Tables[0].Rows[0]["ReceiveDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SectionNo"].ToString()!="")
				{
					model.SectionNo=int.Parse(ds.Tables[0].Rows[0]["SectionNo"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TestTypeNo"].ToString()!="")
				{
					model.TestTypeNo=int.Parse(ds.Tables[0].Rows[0]["TestTypeNo"].ToString());
				}
				model.SampleNo=ds.Tables[0].Rows[0]["SampleNo"].ToString();
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
			strSql.Append("select Convert(varchar(10),ReceiveDate,21) as ReceiveDate,SectionNo,TestTypeNo,SampleNo,GraphName,GraphNo,EquipNo,PointType,ShowPoint,MColor,SColor,ShowAxis,ShowLable,MinX,MaxX,MinY,MaxY,ShowTitle,STitle,GraphValue,GraphMemo,GraphF1,GraphF2,ChartTop,ChartHeight,ChartLeft,ChartWidth,Graphjpg ");
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
			strSql.Append(" Convert(varchar(10),ReceiveDate,21) as ReceiveDate,SectionNo,TestTypeNo,SampleNo,GraphName,GraphNo,EquipNo,PointType,ShowPoint,MColor,SColor,ShowAxis,ShowLable,MinX,MaxX,MinY,MaxY,ShowTitle,STitle,GraphValue,GraphMemo,GraphF1,GraphF2,ChartTop,ChartHeight,ChartLeft,ChartWidth,Graphjpg ");
			strSql.Append(" FROM RFGraphData ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

        public bool Exists(string GraphName, int GraphNo, string FormNo)
        {
            string[] p = FormNo.Split(';');
            if (p.Length >= 4)
            {
                try
                {
                    return this.Exists(Convert.ToDateTime(p[0]), Convert.ToInt32(p[1]), Convert.ToInt32(p[2]), p[3], GraphName, GraphNo);
                    //strSql.Append(" where ReceiveDate='" + p[0] + "' and SectionNo=" + p[1] + " and TestTypeNo=" + p[2] + " and SampleNo='" + p[3] + "' ");
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
                //strSql.Append(" where 1=2 ");
            }
        }

        public int Delete(string GraphName, int GraphNo, string FormNo)
        {
            string[] p = FormNo.Split(';');
            if (p.Length >= 4)
            {
                try
                {
                    return this.Delete(Convert.ToDateTime(p[0]), Convert.ToInt32(p[1]), Convert.ToInt32(p[2]), p[3], GraphName, GraphNo);
                    //strSql.Append(" where ReceiveDate='" + p[0] + "' and SectionNo=" + p[1] + " and TestTypeNo=" + p[2] + " and SampleNo='" + p[3] + "' ");
                }
                catch
                {
                    return 0;
                }
            }
            else
            {
                return 0;
                //strSql.Append(" where 1=2 ");
            }
        }

        public Model.RFGraphData GetModel(string GraphName, int GraphNo, string FormNo)
        {
            string[] p = FormNo.Split(';');
            if (p.Length >= 4)
            {
                try
                {
                    return this.GetModel(Convert.ToDateTime(p[0]), Convert.ToInt32(p[1]), Convert.ToInt32(p[2]), p[3], GraphName, GraphNo);
                    //strSql.Append(" where ReceiveDate='" + p[0] + "' and SectionNo=" + p[1] + " and TestTypeNo=" + p[2] + " and SampleNo='" + p[3] + "' ");
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return null;
                //strSql.Append(" where 1=2 ");
            }
        }

        public DataSet GetList(Model.RFGraphData model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Convert(varchar(10),ReceiveDate,21) as ReceiveDate,SectionNo,TestTypeNo,SampleNo,GraphName,GraphNo,EquipNo,PointType,ShowPoint,MColor,SColor,ShowAxis,ShowLable,MinX,MaxX,MinY,MaxY,ShowTitle,STitle,GraphValue,GraphMemo,GraphF1,GraphF2,ChartTop,ChartHeight,ChartLeft,ChartWidth,Graphjpg ");
            strSql.Append(" FROM RFGraphData where 1=1 ");

            if (model.ReceiveDate != null)
            {
                strSql.Append(" and ReceiveDate='" + model.ReceiveDate + "'");
            }
            if (model.SectionNo != null)
            {
                strSql.Append(" and SectionNo=" + model.SectionNo + "");
            }
            if (model.TestTypeNo != null)
            {
                strSql.Append(" and TestTypeNo=" + model.TestTypeNo + "");
            }
            if (model.SampleNo != null)
            {
                strSql.Append(" and SampleNo='" + model.SampleNo + "'");
            }
            if (model.GraphName != null)
            {
                strSql.Append(" and GraphName='" + model.GraphName + "'");
            }
            if (model.EquipNo != null)
            {
                strSql.Append(" and EquipNo=" + model.EquipNo + "");
            } 
            
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
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion


        public DataSet GetListByReportFormId(string ReportFormId)
        {
            string[] p = ReportFormId.Split(';');
            if (p.Length >= 4)
            {
                try
                {
                    return this.GetList(" ReceiveDate='" + p[0] + "' and SectionNo=" + p[1] + " and TestTypeNo=" + p[2] + " and SampleNo='" + p[3] + "' ");
                }
                catch (Exception e)
                {
                    ZhiFang.Common.Log.Log.Error("MSSQL.History.GetListByReportFormId,异常：" + e.ToString());
                    return null;
                }
            }
            else
            {
                ZhiFang.Common.Log.Log.Error("MSSQL.History.GetListByReportFormId,参数错误ReportFormId：" + ReportFormId);
                return null;
            }
        }


        public DataSet GetListByReportPublicationID(string ReportPublicationID)
        {
            string[] p = ReportPublicationID.Split(';');
            if (p.Length >= 4)
            {
                try
                {
                    DataSet ds= this.GetList(" ReceiveDate='" + p[0] + "' and SectionNo=" + p[1] + " and TestTypeNo=" + p[2] + " and SampleNo='" + p[3] + "' ");
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ds.Tables[0].Columns.Add("FilePath");
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            byte[] imagebytes = null;
                            if (ds.Tables[0].Rows[i]["Graphjpg"] != DBNull.Value)
                            {
                                imagebytes = (byte[])ds.Tables[0].Rows[i]["Graphjpg"];
                                if (ds.Tables[0].Rows[i]["pointtype"] != null && ds.Tables[0].Rows[i]["pointtype"].ToString().Trim() == "8")
                                {
                                    string path = System.AppDomain.CurrentDomain.BaseDirectory + ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("ReportFormImagesURL") + "RFGraphData\\" + Convert.ToDateTime(ds.Tables[0].Rows[i]["ReceiveDate"].ToString()).ToString("yyyy-MM-dd") + "\\";
                                    string filename = ReportPublicationID + "_" + ds.Tables[0].Rows[i]["ReceiveDate"].ToString() + "_" + ds.Tables[0].Rows[i]["GraphName"].ToString() + "_" + ds.Tables[0].Rows[i]["GraphNo"].ToString() + "_" + ds.Tables[0].Rows[i]["PointType"].ToString() + ".pdf";
                                    //if(!File.Exists(path+ filename))原有是否覆盖
                                    if (ZhiFang.ReportFormQueryPrint.Common.FilesHelper.CreatDirFile(path, filename, imagebytes))
                                    {
                                        ds.Tables[0].Rows[i]["FilePath"] = ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("ReportFormImagesURL") + "RFGraphData\\" + Convert.ToDateTime(ds.Tables[0].Rows[i]["ReceiveDate"].ToString()).ToString("yyyy-MM-dd") + "\\" + filename;
                                    }
                                    else
                                    {
                                        ZhiFang.Common.Log.Log.Debug(path + "" + filename + "保存失败！");
                                    }
                                }
                                else
                                {

                                    string path = System.AppDomain.CurrentDomain.BaseDirectory + ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("ReportFormImagesURL") + "RFGraphData\\" + Convert.ToDateTime(ds.Tables[0].Rows[i]["ReceiveDate"].ToString()).ToString("yyyy-MM-dd") + "\\";
                                    string filename = ReportPublicationID + "_" + ds.Tables[0].Rows[i]["ReceiveDate"].ToString() + "_" + ds.Tables[0].Rows[i]["GraphName"].ToString() + "_" + ds.Tables[0].Rows[i]["GraphNo"].ToString() + "_" + ds.Tables[0].Rows[i]["PointType"].ToString() + ".jpg";
                                    //if(!File.Exists(path+ filename))原有是否覆盖
                                    if (ZhiFang.ReportFormQueryPrint.Common.FilesHelper.CreatDirFile(path, filename, imagebytes))
                                    {
                                        ds.Tables[0].Rows[i]["FilePath"] = path + filename;
                                    }
                                }
                            }
                        }
                        for (int i = ds.Tables[0].Rows.Count - 1; i >= 0; i--)
                        {
                            if (ds.Tables[0].Rows[i]["Graphjpg"] == DBNull.Value)
                            {
                                ds.Tables[0].Rows.RemoveAt(i);
                            }
                        }
                        return ds;
                    }
                    return null;
                }
                catch(Exception e)
                {
                    ZhiFang.Common.Log.Log.Error("MSSQL.History.GetListByReportPublicationID,异常：" + e.ToString());
                    return null;
                }
            }
            else
            {
                ZhiFang.Common.Log.Log.Error("MSSQL.History.GetListByReportPublicationID,参数错误ReportPublicationID：" + ReportPublicationID);
                return null;
            }
        }
    }
}

