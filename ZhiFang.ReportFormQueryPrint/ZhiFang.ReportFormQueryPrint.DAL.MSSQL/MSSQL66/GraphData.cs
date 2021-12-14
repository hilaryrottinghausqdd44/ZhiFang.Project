using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.DBUtility;
using System.Data;


namespace ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66
{
    /// <summary>
    /// 数据访问类:GraphData
    /// </summary>
    public partial class GraphData : IDGraphData
    {
        public GraphData()
        { }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("SectionNo", "GraphData");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(DateTime ReceiveDate, int SectionNo, int TestTypeNo, string SampleNo, string GraphName, int GraphNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from GraphData");
            strSql.Append(" where ReceiveDate=@ReceiveDate and SectionNo=@SectionNo and TestTypeNo=@TestTypeNo and SampleNo=@SampleNo and GraphName=@GraphName and GraphNo=@GraphNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@ReceiveDate", SqlDbType.DateTime),
					new SqlParameter("@SectionNo", SqlDbType.Int,4),
					new SqlParameter("@TestTypeNo", SqlDbType.Int,4),
					new SqlParameter("@SampleNo", SqlDbType.VarChar,20),
					new SqlParameter("@GraphName", SqlDbType.VarChar,50),
					new SqlParameter("@GraphNo", SqlDbType.Int,4)			};
            parameters[0].Value = ReceiveDate;
            parameters[1].Value = SectionNo;
            parameters[2].Value = TestTypeNo;
            parameters[3].Value = SampleNo;
            parameters[4].Value = GraphName;
            parameters[5].Value = GraphNo;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.GraphData model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into GraphData(");
            strSql.Append("ReceiveDate,SectionNo,TestTypeNo,SampleNo,GraphName,GraphNo,EquipNo,PointType,ShowPoint,MColor,SColor,ShowAxis,ShowLable,MinX,MaxX,MinY,MaxY,ShowTitle,STitle,GraphValue,GraphMemo,GraphF1,GraphF2,ChartTop,ChartHeight,ChartLeft,ChartWidth,Graphjpg,IsFile,GraphFileName,GraphFileTime,isFileToServer)");
            strSql.Append(" values (");
            strSql.Append("@ReceiveDate,@SectionNo,@TestTypeNo,@SampleNo,@GraphName,@GraphNo,@EquipNo,@PointType,@ShowPoint,@MColor,@SColor,@ShowAxis,@ShowLable,@MinX,@MaxX,@MinY,@MaxY,@ShowTitle,@STitle,@GraphValue,@GraphMemo,@GraphF1,@GraphF2,@ChartTop,@ChartHeight,@ChartLeft,@ChartWidth,@Graphjpg,@IsFile,@GraphFileName,@GraphFileTime,@isFileToServer)");
            SqlParameter[] parameters = {
					new SqlParameter("@ReceiveDate", SqlDbType.DateTime),
					new SqlParameter("@SectionNo", SqlDbType.Int,4),
					new SqlParameter("@TestTypeNo", SqlDbType.Int,4),
					new SqlParameter("@SampleNo", SqlDbType.VarChar,20),
					new SqlParameter("@GraphName", SqlDbType.VarChar,50),
					new SqlParameter("@GraphNo", SqlDbType.Int,4),
					new SqlParameter("@EquipNo", SqlDbType.Int,4),
					new SqlParameter("@PointType", SqlDbType.Int,4),
					new SqlParameter("@ShowPoint", SqlDbType.Int,4),
					new SqlParameter("@MColor", SqlDbType.Int,4),
					new SqlParameter("@SColor", SqlDbType.VarChar,10),
					new SqlParameter("@ShowAxis", SqlDbType.Int,4),
					new SqlParameter("@ShowLable", SqlDbType.Int,4),
					new SqlParameter("@MinX", SqlDbType.Float,8),
					new SqlParameter("@MaxX", SqlDbType.Float,8),
					new SqlParameter("@MinY", SqlDbType.Float,8),
					new SqlParameter("@MaxY", SqlDbType.Float,8),
					new SqlParameter("@ShowTitle", SqlDbType.Int,4),
					new SqlParameter("@STitle", SqlDbType.VarChar,20),
					new SqlParameter("@GraphValue", SqlDbType.Text),
					new SqlParameter("@GraphMemo", SqlDbType.VarChar,200),
					new SqlParameter("@GraphF1", SqlDbType.VarChar,20),
					new SqlParameter("@GraphF2", SqlDbType.VarChar,20),
					new SqlParameter("@ChartTop", SqlDbType.Int,4),
					new SqlParameter("@ChartHeight", SqlDbType.Int,4),
					new SqlParameter("@ChartLeft", SqlDbType.Int,4),
					new SqlParameter("@ChartWidth", SqlDbType.Int,4),
					new SqlParameter("@Graphjpg", SqlDbType.Image),
					new SqlParameter("@IsFile", SqlDbType.Int,4),
					new SqlParameter("@GraphFileName", SqlDbType.VarChar,200),
					new SqlParameter("@GraphFileTime", SqlDbType.DateTime),
					new SqlParameter("@isFileToServer", SqlDbType.Int,4)};
            parameters[0].Value = model.ReceiveDate;
            parameters[1].Value = model.SectionNo;
            parameters[2].Value = model.TestTypeNo;
            parameters[3].Value = model.SampleNo;
            parameters[4].Value = model.GraphName;
            parameters[5].Value = model.GraphNo;
            parameters[6].Value = model.EquipNo;
            parameters[7].Value = model.PointType;
            parameters[8].Value = model.ShowPoint;
            parameters[9].Value = model.MColor;
            parameters[10].Value = model.SColor;
            parameters[11].Value = model.ShowAxis;
            parameters[12].Value = model.ShowLable;
            parameters[13].Value = model.MinX;
            parameters[14].Value = model.MaxX;
            parameters[15].Value = model.MinY;
            parameters[16].Value = model.MaxY;
            parameters[17].Value = model.ShowTitle;
            parameters[18].Value = model.STitle;
            parameters[19].Value = model.GraphValue;
            parameters[20].Value = model.GraphMemo;
            parameters[21].Value = model.GraphF1;
            parameters[22].Value = model.GraphF2;
            parameters[23].Value = model.ChartTop;
            parameters[24].Value = model.ChartHeight;
            parameters[25].Value = model.ChartLeft;
            parameters[26].Value = model.ChartWidth;
            parameters[27].Value = model.Graphjpg;
            parameters[28].Value = model.IsFile;
            parameters[29].Value = model.GraphFileName;
            parameters[30].Value = model.GraphFileTime;
            parameters[31].Value = model.isFileToServer;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.GraphData model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update GraphData set ");
            strSql.Append("EquipNo=@EquipNo,");
            strSql.Append("PointType=@PointType,");
            strSql.Append("ShowPoint=@ShowPoint,");
            strSql.Append("MColor=@MColor,");
            strSql.Append("SColor=@SColor,");
            strSql.Append("ShowAxis=@ShowAxis,");
            strSql.Append("ShowLable=@ShowLable,");
            strSql.Append("MinX=@MinX,");
            strSql.Append("MaxX=@MaxX,");
            strSql.Append("MinY=@MinY,");
            strSql.Append("MaxY=@MaxY,");
            strSql.Append("ShowTitle=@ShowTitle,");
            strSql.Append("STitle=@STitle,");
            strSql.Append("GraphValue=@GraphValue,");
            strSql.Append("GraphMemo=@GraphMemo,");
            strSql.Append("GraphF1=@GraphF1,");
            strSql.Append("GraphF2=@GraphF2,");
            strSql.Append("ChartTop=@ChartTop,");
            strSql.Append("ChartHeight=@ChartHeight,");
            strSql.Append("ChartLeft=@ChartLeft,");
            strSql.Append("ChartWidth=@ChartWidth,");
            strSql.Append("Graphjpg=@Graphjpg,");
            strSql.Append("IsFile=@IsFile,");
            strSql.Append("GraphFileName=@GraphFileName,");
            strSql.Append("GraphFileTime=@GraphFileTime,");
            strSql.Append("isFileToServer=@isFileToServer");
            strSql.Append(" where ReceiveDate=@ReceiveDate and SectionNo=@SectionNo and TestTypeNo=@TestTypeNo and SampleNo=@SampleNo and GraphName=@GraphName and GraphNo=@GraphNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@EquipNo", SqlDbType.Int,4),
					new SqlParameter("@PointType", SqlDbType.Int,4),
					new SqlParameter("@ShowPoint", SqlDbType.Int,4),
					new SqlParameter("@MColor", SqlDbType.Int,4),
					new SqlParameter("@SColor", SqlDbType.VarChar,10),
					new SqlParameter("@ShowAxis", SqlDbType.Int,4),
					new SqlParameter("@ShowLable", SqlDbType.Int,4),
					new SqlParameter("@MinX", SqlDbType.Float,8),
					new SqlParameter("@MaxX", SqlDbType.Float,8),
					new SqlParameter("@MinY", SqlDbType.Float,8),
					new SqlParameter("@MaxY", SqlDbType.Float,8),
					new SqlParameter("@ShowTitle", SqlDbType.Int,4),
					new SqlParameter("@STitle", SqlDbType.VarChar,20),
					new SqlParameter("@GraphValue", SqlDbType.Text),
					new SqlParameter("@GraphMemo", SqlDbType.VarChar,200),
					new SqlParameter("@GraphF1", SqlDbType.VarChar,20),
					new SqlParameter("@GraphF2", SqlDbType.VarChar,20),
					new SqlParameter("@ChartTop", SqlDbType.Int,4),
					new SqlParameter("@ChartHeight", SqlDbType.Int,4),
					new SqlParameter("@ChartLeft", SqlDbType.Int,4),
					new SqlParameter("@ChartWidth", SqlDbType.Int,4),
					new SqlParameter("@Graphjpg", SqlDbType.Image),
					new SqlParameter("@IsFile", SqlDbType.Int,4),
					new SqlParameter("@GraphFileName", SqlDbType.VarChar,200),
					new SqlParameter("@GraphFileTime", SqlDbType.DateTime),
					new SqlParameter("@isFileToServer", SqlDbType.Int,4),
					new SqlParameter("@ReceiveDate", SqlDbType.DateTime),
					new SqlParameter("@SectionNo", SqlDbType.Int,4),
					new SqlParameter("@TestTypeNo", SqlDbType.Int,4),
					new SqlParameter("@SampleNo", SqlDbType.VarChar,20),
					new SqlParameter("@GraphName", SqlDbType.VarChar,50),
					new SqlParameter("@GraphNo", SqlDbType.Int,4)};
            parameters[0].Value = model.EquipNo;
            parameters[1].Value = model.PointType;
            parameters[2].Value = model.ShowPoint;
            parameters[3].Value = model.MColor;
            parameters[4].Value = model.SColor;
            parameters[5].Value = model.ShowAxis;
            parameters[6].Value = model.ShowLable;
            parameters[7].Value = model.MinX;
            parameters[8].Value = model.MaxX;
            parameters[9].Value = model.MinY;
            parameters[10].Value = model.MaxY;
            parameters[11].Value = model.ShowTitle;
            parameters[12].Value = model.STitle;
            parameters[13].Value = model.GraphValue;
            parameters[14].Value = model.GraphMemo;
            parameters[15].Value = model.GraphF1;
            parameters[16].Value = model.GraphF2;
            parameters[17].Value = model.ChartTop;
            parameters[18].Value = model.ChartHeight;
            parameters[19].Value = model.ChartLeft;
            parameters[20].Value = model.ChartWidth;
            parameters[21].Value = model.Graphjpg;
            parameters[22].Value = model.IsFile;
            parameters[23].Value = model.GraphFileName;
            parameters[24].Value = model.GraphFileTime;
            parameters[25].Value = model.isFileToServer;
            parameters[26].Value = model.ReceiveDate;
            parameters[27].Value = model.SectionNo;
            parameters[28].Value = model.TestTypeNo;
            parameters[29].Value = model.SampleNo;
            parameters[30].Value = model.GraphName;
            parameters[31].Value = model.GraphNo;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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
        /// 删除一条数据
        /// </summary>
        public bool Delete(DateTime ReceiveDate, int SectionNo, int TestTypeNo, string SampleNo, string GraphName, int GraphNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from GraphData ");
            strSql.Append(" where ReceiveDate=@ReceiveDate and SectionNo=@SectionNo and TestTypeNo=@TestTypeNo and SampleNo=@SampleNo and GraphName=@GraphName and GraphNo=@GraphNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@ReceiveDate", SqlDbType.DateTime),
					new SqlParameter("@SectionNo", SqlDbType.Int,4),
					new SqlParameter("@TestTypeNo", SqlDbType.Int,4),
					new SqlParameter("@SampleNo", SqlDbType.VarChar,20),
					new SqlParameter("@GraphName", SqlDbType.VarChar,50),
					new SqlParameter("@GraphNo", SqlDbType.Int,4)			};
            parameters[0].Value = ReceiveDate;
            parameters[1].Value = SectionNo;
            parameters[2].Value = TestTypeNo;
            parameters[3].Value = SampleNo;
            parameters[4].Value = GraphName;
            parameters[5].Value = GraphNo;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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
        /// 得到一个对象实体
        /// </summary>
        public Model.GraphData GetModel(DateTime ReceiveDate, int SectionNo, int TestTypeNo, string SampleNo, string GraphName, int GraphNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ReceiveDate,SectionNo,TestTypeNo,SampleNo,GraphName,GraphNo,EquipNo,PointType,ShowPoint,MColor,SColor,ShowAxis,ShowLable,MinX,MaxX,MinY,MaxY,ShowTitle,STitle,GraphValue,GraphMemo,GraphF1,GraphF2,ChartTop,ChartHeight,ChartLeft,ChartWidth,Graphjpg,IsFile,GraphFileName,GraphFileTime,isFileToServer from GraphData ");
            strSql.Append(" where ReceiveDate=@ReceiveDate and SectionNo=@SectionNo and TestTypeNo=@TestTypeNo and SampleNo=@SampleNo and GraphName=@GraphName and GraphNo=@GraphNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@ReceiveDate", SqlDbType.DateTime),
					new SqlParameter("@SectionNo", SqlDbType.Int,4),
					new SqlParameter("@TestTypeNo", SqlDbType.Int,4),
					new SqlParameter("@SampleNo", SqlDbType.VarChar,20),
					new SqlParameter("@GraphName", SqlDbType.VarChar,50),
					new SqlParameter("@GraphNo", SqlDbType.Int,4)			};
            parameters[0].Value = ReceiveDate;
            parameters[1].Value = SectionNo;
            parameters[2].Value = TestTypeNo;
            parameters[3].Value = SampleNo;
            parameters[4].Value = GraphName;
            parameters[5].Value = GraphNo;

            Model.GraphData model = new Model.GraphData();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ReceiveDate"] != null && ds.Tables[0].Rows[0]["ReceiveDate"].ToString() != "")
                {
                    model.ReceiveDate = DateTime.Parse(ds.Tables[0].Rows[0]["ReceiveDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SectionNo"] != null && ds.Tables[0].Rows[0]["SectionNo"].ToString() != "")
                {
                    model.SectionNo = int.Parse(ds.Tables[0].Rows[0]["SectionNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TestTypeNo"] != null && ds.Tables[0].Rows[0]["TestTypeNo"].ToString() != "")
                {
                    model.TestTypeNo = int.Parse(ds.Tables[0].Rows[0]["TestTypeNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SampleNo"] != null && ds.Tables[0].Rows[0]["SampleNo"].ToString() != "")
                {
                    model.SampleNo = ds.Tables[0].Rows[0]["SampleNo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GraphName"] != null && ds.Tables[0].Rows[0]["GraphName"].ToString() != "")
                {
                    model.GraphName = ds.Tables[0].Rows[0]["GraphName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GraphNo"] != null && ds.Tables[0].Rows[0]["GraphNo"].ToString() != "")
                {
                    model.GraphNo = int.Parse(ds.Tables[0].Rows[0]["GraphNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EquipNo"] != null && ds.Tables[0].Rows[0]["EquipNo"].ToString() != "")
                {
                    model.EquipNo = int.Parse(ds.Tables[0].Rows[0]["EquipNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PointType"] != null && ds.Tables[0].Rows[0]["PointType"].ToString() != "")
                {
                    model.PointType = int.Parse(ds.Tables[0].Rows[0]["PointType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ShowPoint"] != null && ds.Tables[0].Rows[0]["ShowPoint"].ToString() != "")
                {
                    model.ShowPoint = int.Parse(ds.Tables[0].Rows[0]["ShowPoint"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MColor"] != null && ds.Tables[0].Rows[0]["MColor"].ToString() != "")
                {
                    model.MColor = int.Parse(ds.Tables[0].Rows[0]["MColor"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SColor"] != null && ds.Tables[0].Rows[0]["SColor"].ToString() != "")
                {
                    model.SColor = ds.Tables[0].Rows[0]["SColor"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ShowAxis"] != null && ds.Tables[0].Rows[0]["ShowAxis"].ToString() != "")
                {
                    model.ShowAxis = int.Parse(ds.Tables[0].Rows[0]["ShowAxis"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ShowLable"] != null && ds.Tables[0].Rows[0]["ShowLable"].ToString() != "")
                {
                    model.ShowLable = int.Parse(ds.Tables[0].Rows[0]["ShowLable"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MinX"] != null && ds.Tables[0].Rows[0]["MinX"].ToString() != "")
                {
                    model.MinX = decimal.Parse(ds.Tables[0].Rows[0]["MinX"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MaxX"] != null && ds.Tables[0].Rows[0]["MaxX"].ToString() != "")
                {
                    model.MaxX = decimal.Parse(ds.Tables[0].Rows[0]["MaxX"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MinY"] != null && ds.Tables[0].Rows[0]["MinY"].ToString() != "")
                {
                    model.MinY = decimal.Parse(ds.Tables[0].Rows[0]["MinY"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MaxY"] != null && ds.Tables[0].Rows[0]["MaxY"].ToString() != "")
                {
                    model.MaxY = decimal.Parse(ds.Tables[0].Rows[0]["MaxY"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ShowTitle"] != null && ds.Tables[0].Rows[0]["ShowTitle"].ToString() != "")
                {
                    model.ShowTitle = int.Parse(ds.Tables[0].Rows[0]["ShowTitle"].ToString());
                }
                if (ds.Tables[0].Rows[0]["STitle"] != null && ds.Tables[0].Rows[0]["STitle"].ToString() != "")
                {
                    model.STitle = ds.Tables[0].Rows[0]["STitle"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GraphValue"] != null && ds.Tables[0].Rows[0]["GraphValue"].ToString() != "")
                {
                    model.GraphValue = ds.Tables[0].Rows[0]["GraphValue"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GraphMemo"] != null && ds.Tables[0].Rows[0]["GraphMemo"].ToString() != "")
                {
                    model.GraphMemo = ds.Tables[0].Rows[0]["GraphMemo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GraphF1"] != null && ds.Tables[0].Rows[0]["GraphF1"].ToString() != "")
                {
                    model.GraphF1 = ds.Tables[0].Rows[0]["GraphF1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GraphF2"] != null && ds.Tables[0].Rows[0]["GraphF2"].ToString() != "")
                {
                    model.GraphF2 = ds.Tables[0].Rows[0]["GraphF2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ChartTop"] != null && ds.Tables[0].Rows[0]["ChartTop"].ToString() != "")
                {
                    model.ChartTop = int.Parse(ds.Tables[0].Rows[0]["ChartTop"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ChartHeight"] != null && ds.Tables[0].Rows[0]["ChartHeight"].ToString() != "")
                {
                    model.ChartHeight = int.Parse(ds.Tables[0].Rows[0]["ChartHeight"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ChartLeft"] != null && ds.Tables[0].Rows[0]["ChartLeft"].ToString() != "")
                {
                    model.ChartLeft = int.Parse(ds.Tables[0].Rows[0]["ChartLeft"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ChartWidth"] != null && ds.Tables[0].Rows[0]["ChartWidth"].ToString() != "")
                {
                    model.ChartWidth = int.Parse(ds.Tables[0].Rows[0]["ChartWidth"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Graphjpg"] != null && ds.Tables[0].Rows[0]["Graphjpg"].ToString() != "")
                {
                    model.Graphjpg = (byte[])ds.Tables[0].Rows[0]["Graphjpg"];
                }
                if (ds.Tables[0].Rows[0]["IsFile"] != null && ds.Tables[0].Rows[0]["IsFile"].ToString() != "")
                {
                    model.IsFile = int.Parse(ds.Tables[0].Rows[0]["IsFile"].ToString());
                }
                if (ds.Tables[0].Rows[0]["GraphFileName"] != null && ds.Tables[0].Rows[0]["GraphFileName"].ToString() != "")
                {
                    model.GraphFileName = ds.Tables[0].Rows[0]["GraphFileName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["GraphFileTime"] != null && ds.Tables[0].Rows[0]["GraphFileTime"].ToString() != "")
                {
                    model.GraphFileTime = DateTime.Parse(ds.Tables[0].Rows[0]["GraphFileTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["isFileToServer"] != null && ds.Tables[0].Rows[0]["isFileToServer"].ToString() != "")
                {
                    model.isFileToServer = int.Parse(ds.Tables[0].Rows[0]["isFileToServer"].ToString());
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ReceiveDate,SectionNo,TestTypeNo,SampleNo,GraphName,GraphNo,EquipNo,PointType,ShowPoint,MColor,SColor,ShowAxis,ShowLable,MinX,MaxX,MinY,MaxY,ShowTitle,STitle,GraphValue,GraphMemo,GraphF1,GraphF2,ChartTop,ChartHeight,ChartLeft,ChartWidth,Graphjpg,IsFile,GraphFileName,GraphFileTime,isFileToServer ");
            strSql.Append(" FROM GraphData ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" ReceiveDate,SectionNo,TestTypeNo,SampleNo,GraphName,GraphNo,EquipNo,PointType,ShowPoint,MColor,SColor,ShowAxis,ShowLable,MinX,MaxX,MinY,MaxY,ShowTitle,STitle,GraphValue,GraphMemo,GraphF1,GraphF2,ChartTop,ChartHeight,ChartLeft,ChartWidth,Graphjpg,IsFile,GraphFileName,GraphFileTime,isFileToServer ");
            strSql.Append(" FROM GraphData ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM GraphData ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.GraphNo desc");
            }
            strSql.Append(")AS Row, T.*  from GraphData T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "GraphData";
            parameters[1].Value = "GraphNo";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  Method

        #region IDataBase<GraphData> 成员


        int IDataBase<Model.GraphData>.Add(Model.GraphData t)
        {
            throw new NotImplementedException();
        }

        int IDataBase<Model.GraphData>.Update(Model.GraphData model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update GraphData set ");
            strSql.Append("EquipNo=@EquipNo,");
            strSql.Append("PointType=@PointType,");
            strSql.Append("ShowPoint=@ShowPoint,");
            strSql.Append("MColor=@MColor,");
            strSql.Append("SColor=@SColor,");
            strSql.Append("ShowAxis=@ShowAxis,");
            strSql.Append("ShowLable=@ShowLable,");
            strSql.Append("MinX=@MinX,");
            strSql.Append("MaxX=@MaxX,");
            strSql.Append("MinY=@MinY,");
            strSql.Append("MaxY=@MaxY,");
            strSql.Append("ShowTitle=@ShowTitle,");
            strSql.Append("STitle=@STitle,");
            strSql.Append("GraphValue=@GraphValue,");
            strSql.Append("GraphMemo=@GraphMemo,");
            strSql.Append("GraphF1=@GraphF1,");
            strSql.Append("GraphF2=@GraphF2,");
            strSql.Append("ChartTop=@ChartTop,");
            strSql.Append("ChartHeight=@ChartHeight,");
            strSql.Append("ChartLeft=@ChartLeft,");
            strSql.Append("ChartWidth=@ChartWidth,");
            strSql.Append("Graphjpg=@Graphjpg,");
            strSql.Append("IsFile=@IsFile,");
            strSql.Append("GraphFileName=@GraphFileName,");
            strSql.Append("GraphFileTime=@GraphFileTime,");
            strSql.Append("isFileToServer=@isFileToServer");
            strSql.Append(" where ReceiveDate=@ReceiveDate and SectionNo=@SectionNo and TestTypeNo=@TestTypeNo and SampleNo=@SampleNo and GraphName=@GraphName and GraphNo=@GraphNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@EquipNo", SqlDbType.Int,4),
					new SqlParameter("@PointType", SqlDbType.Int,4),
					new SqlParameter("@ShowPoint", SqlDbType.Int,4),
					new SqlParameter("@MColor", SqlDbType.Int,4),
					new SqlParameter("@SColor", SqlDbType.VarChar,10),
					new SqlParameter("@ShowAxis", SqlDbType.Int,4),
					new SqlParameter("@ShowLable", SqlDbType.Int,4),
					new SqlParameter("@MinX", SqlDbType.Float,8),
					new SqlParameter("@MaxX", SqlDbType.Float,8),
					new SqlParameter("@MinY", SqlDbType.Float,8),
					new SqlParameter("@MaxY", SqlDbType.Float,8),
					new SqlParameter("@ShowTitle", SqlDbType.Int,4),
					new SqlParameter("@STitle", SqlDbType.VarChar,20),
					new SqlParameter("@GraphValue", SqlDbType.Text),
					new SqlParameter("@GraphMemo", SqlDbType.VarChar,200),
					new SqlParameter("@GraphF1", SqlDbType.VarChar,20),
					new SqlParameter("@GraphF2", SqlDbType.VarChar,20),
					new SqlParameter("@ChartTop", SqlDbType.Int,4),
					new SqlParameter("@ChartHeight", SqlDbType.Int,4),
					new SqlParameter("@ChartLeft", SqlDbType.Int,4),
					new SqlParameter("@ChartWidth", SqlDbType.Int,4),
					new SqlParameter("@Graphjpg", SqlDbType.Image),
					new SqlParameter("@IsFile", SqlDbType.Int,4),
					new SqlParameter("@GraphFileName", SqlDbType.VarChar,200),
					new SqlParameter("@GraphFileTime", SqlDbType.DateTime),
					new SqlParameter("@isFileToServer", SqlDbType.Int,4),
					new SqlParameter("@ReceiveDate", SqlDbType.DateTime),
					new SqlParameter("@SectionNo", SqlDbType.Int,4),
					new SqlParameter("@TestTypeNo", SqlDbType.Int,4),
					new SqlParameter("@SampleNo", SqlDbType.VarChar,20),
					new SqlParameter("@GraphName", SqlDbType.VarChar,50),
					new SqlParameter("@GraphNo", SqlDbType.Int,4)};
            parameters[0].Value = model.EquipNo;
            parameters[1].Value = model.PointType;
            parameters[2].Value = model.ShowPoint;
            parameters[3].Value = model.MColor;
            parameters[4].Value = model.SColor;
            parameters[5].Value = model.ShowAxis;
            parameters[6].Value = model.ShowLable;
            parameters[7].Value = model.MinX;
            parameters[8].Value = model.MaxX;
            parameters[9].Value = model.MinY;
            parameters[10].Value = model.MaxY;
            parameters[11].Value = model.ShowTitle;
            parameters[12].Value = model.STitle;
            parameters[13].Value = model.GraphValue;
            parameters[14].Value = model.GraphMemo;
            parameters[15].Value = model.GraphF1;
            parameters[16].Value = model.GraphF2;
            parameters[17].Value = model.ChartTop;
            parameters[18].Value = model.ChartHeight;
            parameters[19].Value = model.ChartLeft;
            parameters[20].Value = model.ChartWidth;
            parameters[21].Value = model.Graphjpg;
            parameters[22].Value = model.IsFile;
            parameters[23].Value = model.GraphFileName;
            parameters[24].Value = model.GraphFileTime;
            parameters[25].Value = model.isFileToServer;
            parameters[26].Value = model.ReceiveDate;
            parameters[27].Value = model.SectionNo;
            parameters[28].Value = model.TestTypeNo;
            parameters[29].Value = model.SampleNo;
            parameters[30].Value = model.GraphName;
            parameters[31].Value = model.GraphNo;

            return  DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
          
        }

        public DataSet GetList(Model.GraphData model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Convert(varchar(10),ReceiveDate,21) as ReceiveDate,SectionNo,TestTypeNo,SampleNo,GraphName,GraphNo,EquipNo,PointType,ShowPoint,MColor,SColor,ShowAxis,ShowLable,MinX,MaxX,MinY,MaxY,ShowTitle,STitle,GraphValue,GraphMemo,GraphF1,GraphF2,ChartTop,ChartHeight,ChartLeft,ChartWidth,Graphjpg ");
            strSql.Append(" FROM GraphData where 1=1 ");

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
            if (model.GraphValue != null)
            {
                strSql.Append(" and GraphValue='" + model.GraphValue + "'");
            }
            if (model.GraphMemo != null)
            {
                strSql.Append(" and GraphMemo='" + model.GraphMemo + "'");
            }
            if (model.Graphjpg != null)
            {
                strSql.Append(" and Graphjpg=" + model.Graphjpg + "");
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion
    }
}

