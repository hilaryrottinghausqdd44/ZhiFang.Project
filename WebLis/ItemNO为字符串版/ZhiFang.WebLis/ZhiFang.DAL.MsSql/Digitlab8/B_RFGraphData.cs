using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Digitlab8  
{
	 	//B_RFGraphData
		
	public partial class B_RFGraphData : IDRFGraphData,IDBatchCopy,IDGetListByTimeStampe
	{	
		DBUtility.IDBConnection idb;
        public B_RFGraphData(string dbsourceconn)
        {
            idb = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
		public B_RFGraphData()
		{
            idb = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.LisDB());
        }
		D_LogInfo d_log = new D_LogInfo();
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ZhiFang.Model.RFGraphData model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into B_RFGraphData(");			
            strSql.Append("GraphName,GraphNo,EquipNo,PointType,ShowPoint,MColor,SColor,ShowAxis,ShowLable,MinX,MaxX,MinY,MaxY,ShowTitle,STitle,GraphValue,GraphMemo,GraphF1,GraphF2,ChartTop,ChartHeight,ChartLeft,ChartWidth,Graphjpg,FormNo,UnionKey,StandCode,ZFStandCode,UseFlag");
			strSql.Append(") values (");
            strSql.Append("@GraphName,@GraphNo,@EquipNo,@PointType,@ShowPoint,@MColor,@SColor,@ShowAxis,@ShowLable,@MinX,@MaxX,@MinY,@MaxY,@ShowTitle,@STitle,@GraphValue,@GraphMemo,@GraphF1,@GraphF2,@ChartTop,@ChartHeight,@ChartLeft,@ChartWidth,@Graphjpg,@FormNo,@UnionKey,@StandCode,@ZFStandCode,@UseFlag");            
            strSql.Append(") ");            
            
			SqlParameter[] parameters = {
			            new SqlParameter("@GraphName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@GraphNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@EquipNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@PointType", SqlDbType.Int,4) ,            
                        new SqlParameter("@ShowPoint", SqlDbType.Int,4) ,            
                        new SqlParameter("@MColor", SqlDbType.Int,4) ,            
                        new SqlParameter("@SColor", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@ShowAxis", SqlDbType.Int,4) ,            
                        new SqlParameter("@ShowLable", SqlDbType.Int,4) ,            
                        new SqlParameter("@MinX", SqlDbType.Float,8) ,            
                        new SqlParameter("@MaxX", SqlDbType.Float,8) ,            
                        new SqlParameter("@MinY", SqlDbType.Float,8) ,            
                        new SqlParameter("@MaxY", SqlDbType.Float,8) ,            
                        new SqlParameter("@ShowTitle", SqlDbType.Int,4) ,            
                        new SqlParameter("@STitle", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@GraphValue", SqlDbType.Text) ,            
                        new SqlParameter("@GraphMemo", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@GraphF1", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@GraphF2", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ChartTop", SqlDbType.Int,4) ,            
                        new SqlParameter("@ChartHeight", SqlDbType.Int,4) ,            
                        new SqlParameter("@ChartLeft", SqlDbType.Int,4) ,            
                        new SqlParameter("@ChartWidth", SqlDbType.Int,4) ,            
                        new SqlParameter("@Graphjpg", SqlDbType.Image) ,            
                        new SqlParameter("@FormNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@UnionKey", SqlDbType.Int,4) ,            
                        new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ZFStandCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@UseFlag", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.GraphName;                        
            parameters[1].Value = model.GraphNo;                        
            parameters[2].Value = model.EquipNo;                        
            parameters[3].Value = model.PointType;                        
            parameters[4].Value = model.ShowPoint;                        
            parameters[5].Value = model.MColor;                        
            parameters[6].Value = model.SColor;                        
            parameters[7].Value = model.ShowAxis;                        
            parameters[8].Value = model.ShowLable;                        
            parameters[9].Value = model.MinX;                        
            parameters[10].Value = model.MaxX;                        
            parameters[11].Value = model.MinY;                        
            parameters[12].Value = model.MaxY;                        
            parameters[13].Value = model.ShowTitle;                        
            parameters[14].Value = model.STitle;                        
            parameters[15].Value = model.GraphValue;                        
            parameters[16].Value = model.GraphMemo;                        
            parameters[17].Value = model.GraphF1;                        
            parameters[18].Value = model.GraphF2;                        
            parameters[19].Value = model.ChartTop;                        
            parameters[20].Value = model.ChartHeight;                        
            parameters[21].Value = model.ChartLeft;                        
            parameters[22].Value = model.ChartWidth;                        
            parameters[23].Value = model.Graphjpg;                        
            parameters[24].Value = model.FormNo;                        
            parameters[25].Value = model.UnionKey;                        
            parameters[26].Value = model.StandCode;                        
            parameters[27].Value = model.ZFStandCode;                        
            parameters[28].Value = model.UseFlag;                  
			if (idb.ExecuteNonQuery(strSql.ToString(),parameters) > 0)
            {
                return d_log.OperateLog("RFGraphData", "", "", DateTime.Now, 1);
            }
            else
                return -1;
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ZhiFang.Model.RFGraphData model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update B_RFGraphData set ");
			                                                
            strSql.Append(" GraphName = @GraphName , ");                                    
            strSql.Append(" GraphNo = @GraphNo , ");                                    
            strSql.Append(" EquipNo = @EquipNo , ");                                    
            strSql.Append(" PointType = @PointType , ");                                    
            strSql.Append(" ShowPoint = @ShowPoint , ");                                    
            strSql.Append(" MColor = @MColor , ");                                    
            strSql.Append(" SColor = @SColor , ");                                    
            strSql.Append(" ShowAxis = @ShowAxis , ");                                    
            strSql.Append(" ShowLable = @ShowLable , ");                                    
            strSql.Append(" MinX = @MinX , ");                                    
            strSql.Append(" MaxX = @MaxX , ");                                    
            strSql.Append(" MinY = @MinY , ");                                    
            strSql.Append(" MaxY = @MaxY , ");                                    
            strSql.Append(" ShowTitle = @ShowTitle , ");                                    
            strSql.Append(" STitle = @STitle , ");                                    
            strSql.Append(" GraphValue = @GraphValue , ");                                    
            strSql.Append(" GraphMemo = @GraphMemo , ");                                    
            strSql.Append(" GraphF1 = @GraphF1 , ");                                    
            strSql.Append(" GraphF2 = @GraphF2 , ");                                    
            strSql.Append(" ChartTop = @ChartTop , ");                                    
            strSql.Append(" ChartHeight = @ChartHeight , ");                                    
            strSql.Append(" ChartLeft = @ChartLeft , ");                                    
            strSql.Append(" ChartWidth = @ChartWidth , ");                                    
            strSql.Append(" Graphjpg = @Graphjpg , ");                                    
            strSql.Append(" FormNo = @FormNo , ");                                    
            strSql.Append(" UnionKey = @UnionKey , ");                                                                                    
            strSql.Append(" StandCode = @StandCode , ");                                    
            strSql.Append(" ZFStandCode = @ZFStandCode , ");                                    
            strSql.Append(" UseFlag = @UseFlag  ");            			
			strSql.Append(" where GraphName=@GraphName and GraphNo=@GraphNo and FormNo=@FormNo  ");
						
			SqlParameter[] parameters = {
			            	
                           
            new SqlParameter("@GraphName", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@GraphNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@EquipNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@PointType", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@ShowPoint", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@MColor", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@SColor", SqlDbType.VarChar,10) ,            	
                           
            new SqlParameter("@ShowAxis", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@ShowLable", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@MinX", SqlDbType.Float,8) ,            	
                           
            new SqlParameter("@MaxX", SqlDbType.Float,8) ,            	
                           
            new SqlParameter("@MinY", SqlDbType.Float,8) ,            	
                           
            new SqlParameter("@MaxY", SqlDbType.Float,8) ,            	
                           
            new SqlParameter("@ShowTitle", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@STitle", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@GraphValue", SqlDbType.Text) ,            	
                           
            new SqlParameter("@GraphMemo", SqlDbType.VarChar,200) ,            	
                           
            new SqlParameter("@GraphF1", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@GraphF2", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@ChartTop", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@ChartHeight", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@ChartLeft", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@ChartWidth", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@Graphjpg", SqlDbType.Image) ,            	
                           
            new SqlParameter("@FormNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@UnionKey", SqlDbType.Int,4) ,            	
                        	
                        	
                           
            new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@ZFStandCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@UseFlag", SqlDbType.Int,4)             	
              
            };
            			    
				
                
			   
			if(model.GraphName!=null)
			{
            	parameters[0].Value = model.GraphName;            	
            }
            	
                
			   
			if(model.GraphNo!=null)
			{
            	parameters[1].Value = model.GraphNo;            	
            }
            	
                
			   
			if(model.EquipNo!=null)
			{
            	parameters[2].Value = model.EquipNo;            	
            }
            	
                
			   
			if(model.PointType!=null)
			{
            	parameters[3].Value = model.PointType;            	
            }
            	
                
			   
			if(model.ShowPoint!=null)
			{
            	parameters[4].Value = model.ShowPoint;            	
            }
            	
                
			   
			if(model.MColor!=null)
			{
            	parameters[5].Value = model.MColor;            	
            }
            	
                
			   
			if(model.SColor!=null)
			{
            	parameters[6].Value = model.SColor;            	
            }
            	
                
			   
			if(model.ShowAxis!=null)
			{
            	parameters[7].Value = model.ShowAxis;            	
            }
            	
                
			   
			if(model.ShowLable!=null)
			{
            	parameters[8].Value = model.ShowLable;            	
            }
            	
                
			   
			if(model.MinX!=null)
			{
            	parameters[9].Value = model.MinX;            	
            }
            	
                
			   
			if(model.MaxX!=null)
			{
            	parameters[10].Value = model.MaxX;            	
            }
            	
                
			   
			if(model.MinY!=null)
			{
            	parameters[11].Value = model.MinY;            	
            }
            	
                
			   
			if(model.MaxY!=null)
			{
            	parameters[12].Value = model.MaxY;            	
            }
            	
                
			   
			if(model.ShowTitle!=null)
			{
            	parameters[13].Value = model.ShowTitle;            	
            }
            	
                
			   
			if(model.STitle!=null)
			{
            	parameters[14].Value = model.STitle;            	
            }
            	
                
			   
			if(model.GraphValue!=null)
			{
            	parameters[15].Value = model.GraphValue;            	
            }
            	
                
			   
			if(model.GraphMemo!=null)
			{
            	parameters[16].Value = model.GraphMemo;            	
            }
            	
                
			   
			if(model.GraphF1!=null)
			{
            	parameters[17].Value = model.GraphF1;            	
            }
            	
                
			   
			if(model.GraphF2!=null)
			{
            	parameters[18].Value = model.GraphF2;            	
            }
            	
                
			   
			if(model.ChartTop!=null)
			{
            	parameters[19].Value = model.ChartTop;            	
            }
            	
                
			   
			if(model.ChartHeight!=null)
			{
            	parameters[20].Value = model.ChartHeight;            	
            }
            	
                
			   
			if(model.ChartLeft!=null)
			{
            	parameters[21].Value = model.ChartLeft;            	
            }
            	
                
			   
			if(model.ChartWidth!=null)
			{
            	parameters[22].Value = model.ChartWidth;            	
            }
            	
                
			   
			if(model.Graphjpg!=null)
			{
            	parameters[23].Value = model.Graphjpg;            	
            }
            	
                
			   
			if(model.FormNo!=null)
			{
            	parameters[24].Value = model.FormNo;            	
            }
            	
                
			   
			if(model.UnionKey!=null)
			{
            	parameters[25].Value = model.UnionKey;            	
            }
            	
                
				
                
				
                
			   
			if(model.StandCode!=null)
			{
            	parameters[26].Value = model.StandCode;            	
            }
            	
                
			   
			if(model.ZFStandCode!=null)
			{
            	parameters[27].Value = model.ZFStandCode;            	
            }
            	
                
			   
			if(model.UseFlag!=null)
			{
            	parameters[28].Value = model.UseFlag;            	
            }
            	
                        
			if (idb.ExecuteNonQuery(strSql.ToString(),parameters) > 0)
            {
                return d_log.OperateLog("RFGraphData", "", "", DateTime.Now, 1);
            }
            else
                return -1;
		}
		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
        public int Delete(string GraphName, int GraphNo, string FormNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from B_RFGraphData ");
			strSql.Append(" where GraphName=@GraphName and GraphNo=@GraphNo and FormNo=@FormNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@GraphName", SqlDbType.VarChar,50),
					new SqlParameter("@GraphNo", SqlDbType.Int,4),
					new SqlParameter("@FormNo", SqlDbType.VarChar,50)};
			parameters[0].Value = GraphName;
			parameters[1].Value = GraphNo;
			parameters[2].Value = FormNo;


			return idb.ExecuteNonQuery(strSql.ToString(),parameters);
		
		}
		
				/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteList(string GraphIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from B_RFGraphData ");
			strSql.Append(" where ID in ("+GraphIDlist + ")  ");
			return idb.ExecuteNonQuery(strSql.ToString());
			
		}
				
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public ZhiFang.Model.RFGraphData GetModel(string GraphName, int GraphNo, string FormNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select GraphID, GraphName, GraphNo, EquipNo, PointType, ShowPoint, MColor, SColor, ShowAxis, ShowLable, MinX, MaxX, MinY, MaxY, ShowTitle, STitle, GraphValue, GraphMemo, GraphF1, GraphF2, ChartTop, ChartHeight, ChartLeft, ChartWidth, Graphjpg, FormNo, UnionKey, DTimeStampe, AddTime, StandCode, ZFStandCode, UseFlag  ");
            strSql.Append("  from B_RFGraphData ");
            strSql.Append(" where GraphName=@GraphName and GraphNo=@GraphNo and FormNo=@FormNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@GraphName", SqlDbType.VarChar,50),
					new SqlParameter("@GraphNo", SqlDbType.Int,4),
					new SqlParameter("@FormNo", SqlDbType.VarChar,50)};
            parameters[0].Value = GraphName;
            parameters[1].Value = GraphNo;
            parameters[2].Value = FormNo;


            ZhiFang.Model.RFGraphData model = new ZhiFang.Model.RFGraphData();
            DataSet ds = idb.ExecuteDataSet(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["GraphID"].ToString() != "")
                {
                    model.GraphID = int.Parse(ds.Tables[0].Rows[0]["GraphID"].ToString());
                }
                model.GraphName = ds.Tables[0].Rows[0]["GraphName"].ToString();
                if (ds.Tables[0].Rows[0]["GraphNo"].ToString() != "")
                {
                    model.GraphNo = int.Parse(ds.Tables[0].Rows[0]["GraphNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EquipNo"].ToString() != "")
                {
                    model.EquipNo = int.Parse(ds.Tables[0].Rows[0]["EquipNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PointType"].ToString() != "")
                {
                    model.PointType = int.Parse(ds.Tables[0].Rows[0]["PointType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ShowPoint"].ToString() != "")
                {
                    model.ShowPoint = int.Parse(ds.Tables[0].Rows[0]["ShowPoint"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MColor"].ToString() != "")
                {
                    model.MColor = int.Parse(ds.Tables[0].Rows[0]["MColor"].ToString());
                }
                model.SColor = ds.Tables[0].Rows[0]["SColor"].ToString();
                if (ds.Tables[0].Rows[0]["ShowAxis"].ToString() != "")
                {
                    model.ShowAxis = int.Parse(ds.Tables[0].Rows[0]["ShowAxis"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ShowLable"].ToString() != "")
                {
                    model.ShowLable = int.Parse(ds.Tables[0].Rows[0]["ShowLable"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MinX"].ToString() != "")
                {
                    model.MinX = decimal.Parse(ds.Tables[0].Rows[0]["MinX"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MaxX"].ToString() != "")
                {
                    model.MaxX = decimal.Parse(ds.Tables[0].Rows[0]["MaxX"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MinY"].ToString() != "")
                {
                    model.MinY = decimal.Parse(ds.Tables[0].Rows[0]["MinY"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MaxY"].ToString() != "")
                {
                    model.MaxY = decimal.Parse(ds.Tables[0].Rows[0]["MaxY"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ShowTitle"].ToString() != "")
                {
                    model.ShowTitle = int.Parse(ds.Tables[0].Rows[0]["ShowTitle"].ToString());
                }
                model.STitle = ds.Tables[0].Rows[0]["STitle"].ToString();
                model.GraphValue = ds.Tables[0].Rows[0]["GraphValue"].ToString();
                model.GraphMemo = ds.Tables[0].Rows[0]["GraphMemo"].ToString();
                model.GraphF1 = ds.Tables[0].Rows[0]["GraphF1"].ToString();
                model.GraphF2 = ds.Tables[0].Rows[0]["GraphF2"].ToString();
                if (ds.Tables[0].Rows[0]["ChartTop"].ToString() != "")
                {
                    model.ChartTop = int.Parse(ds.Tables[0].Rows[0]["ChartTop"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ChartHeight"].ToString() != "")
                {
                    model.ChartHeight = int.Parse(ds.Tables[0].Rows[0]["ChartHeight"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ChartLeft"].ToString() != "")
                {
                    model.ChartLeft = int.Parse(ds.Tables[0].Rows[0]["ChartLeft"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ChartWidth"].ToString() != "")
                {
                    model.ChartWidth = int.Parse(ds.Tables[0].Rows[0]["ChartWidth"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Graphjpg"].ToString() != "")
                {
                    model.Graphjpg = (byte[])ds.Tables[0].Rows[0]["Graphjpg"];
                }
                model.FormNo = ds.Tables[0].Rows[0]["FormNo"].ToString();
                if (ds.Tables[0].Rows[0]["UnionKey"].ToString() != "")
                {
                    model.UnionKey = int.Parse(ds.Tables[0].Rows[0]["UnionKey"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AddTime"].ToString() != "")
                {
                    model.AddTime = DateTime.Parse(ds.Tables[0].Rows[0]["AddTime"].ToString());
                }
                model.StandCode = ds.Tables[0].Rows[0]["StandCode"].ToString();
                model.ZFStandCode = ds.Tables[0].Rows[0]["ZFStandCode"].ToString();
                if (ds.Tables[0].Rows[0]["UseFlag"].ToString() != "")
                {
                    model.UseFlag = int.Parse(ds.Tables[0].Rows[0]["UseFlag"].ToString());
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
			strSql.Append("select * ");
			strSql.Append(" FROM B_RFGraphData ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return idb.ExecuteDataSet(strSql.ToString());
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
			strSql.Append(" * ");
			strSql.Append(" FROM B_RFGraphData ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return idb.ExecuteDataSet(strSql.ToString());
		}
		
		/// <summary>
		/// 根据实体获取DataSet
		/// </summary>
		public DataSet GetList(ZhiFang.Model.RFGraphData model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM B_RFGraphData where 1=1 ");
			                                                                
                        if(model.GraphName !=null)
                        {
                        strSql.Append(" and GraphName='"+model.GraphName+"' ");
                        }
                                                    
             
            if(model.GraphNo !=0)
                        {
                        strSql.Append(" and GraphNo="+model.GraphNo+" ");
                        }
                                                    
                        if(model.EquipNo !=null)
                        {
                        strSql.Append(" and EquipNo="+model.EquipNo+" ");
                        }
                                                    
                        if(model.PointType !=null)
                        {
                        strSql.Append(" and PointType="+model.PointType+" ");
                        }
                                                    
                        if(model.ShowPoint !=null)
                        {
                        strSql.Append(" and ShowPoint="+model.ShowPoint+" ");
                        }
                                                    
                        if(model.MColor !=null)
                        {
                        strSql.Append(" and MColor="+model.MColor+" ");
                        }
                                                    
                        if(model.SColor !=null)
                        {
                        strSql.Append(" and SColor='"+model.SColor+"' ");
                        }
                                                    
                        if(model.ShowAxis !=null)
                        {
                        strSql.Append(" and ShowAxis="+model.ShowAxis+" ");
                        }
                                                    
                        if(model.ShowLable !=null)
                        {
                        strSql.Append(" and ShowLable="+model.ShowLable+" ");
                        }
                                                    
                        if(model.MinX !=null)
                        {
                        strSql.Append(" and MinX="+model.MinX+" ");
                        }
                                                    
                        if(model.MaxX !=null)
                        {
                        strSql.Append(" and MaxX="+model.MaxX+" ");
                        }
                                                    
                        if(model.MinY !=null)
                        {
                        strSql.Append(" and MinY="+model.MinY+" ");
                        }
                                                    
                        if(model.MaxY !=null)
                        {
                        strSql.Append(" and MaxY="+model.MaxY+" ");
                        }
                                                    
                        if(model.ShowTitle !=null)
                        {
                        strSql.Append(" and ShowTitle="+model.ShowTitle+" ");
                        }
                                                    
                        if(model.STitle !=null)
                        {
                        strSql.Append(" and STitle='"+model.STitle+"' ");
                        }
                                                    
                        if(model.GraphValue !=null)
                        {
                        strSql.Append(" and GraphValue='"+model.GraphValue+"' ");
                        }
                                                    
                        if(model.GraphMemo !=null)
                        {
                        strSql.Append(" and GraphMemo='"+model.GraphMemo+"' ");
                        }
                                                    
                        if(model.GraphF1 !=null)
                        {
                        strSql.Append(" and GraphF1='"+model.GraphF1+"' ");
                        }
                                                    
                        if(model.GraphF2 !=null)
                        {
                        strSql.Append(" and GraphF2='"+model.GraphF2+"' ");
                        }
                                                    
                        if(model.ChartTop !=null)
                        {
                        strSql.Append(" and ChartTop="+model.ChartTop+" ");
                        }
                                                    
                        if(model.ChartHeight !=null)
                        {
                        strSql.Append(" and ChartHeight="+model.ChartHeight+" ");
                        }
                                                    
                        if(model.ChartLeft !=null)
                        {
                        strSql.Append(" and ChartLeft="+model.ChartLeft+" ");
                        }
                                                    
                        if(model.ChartWidth !=null)
                        {
                        strSql.Append(" and ChartWidth="+model.ChartWidth+" ");
                        }
                                                    
                        if(model.Graphjpg !=null)
                        {
                        strSql.Append(" and Graphjpg='"+model.Graphjpg+"' ");
                        }
                                                    
             
            if(model.FormNo !=null)
                        {
                        strSql.Append(" and FormNo="+model.FormNo+" ");
                        }
                                                    
                        if(model.UnionKey !=null)
                        {
                        strSql.Append(" and UnionKey="+model.UnionKey+" ");
                        }
                                                    
                        if(model.DTimeStampe !=null)
                        {
                        strSql.Append(" and DTimeStampe='"+model.DTimeStampe+"' ");
                        }
                                                                
                        if(model.StandCode !=null)
                        {
                        strSql.Append(" and StandCode='"+model.StandCode+"' ");
                        }
                                                    
                        if(model.ZFStandCode !=null)
                        {
                        strSql.Append(" and ZFStandCode='"+model.ZFStandCode+"' ");
                        }
                                                return idb.ExecuteDataSet(strSql.ToString());
		}
		
		/// <summary>
		/// 获取总记录
		/// </summary>
		public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_RFGraphData ");
            string strCount = idb.ExecuteScalar(strSql.ToString());
            if (strCount != null && strCount.Trim() != "")
            {
                return Convert.ToInt32(strCount.Trim());
            }
            else
            {
                return 0;
            }
        }
        public int GetTotalCount(ZhiFang.Model.RFGraphData model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_RFGraphData where 1=1 ");
           	                                          
            if(model.GraphName !=null)
            {
                        strSql.Append(" and GraphName='"+model.GraphName+"' ");
                        }
                                          
            if(model.GraphNo !=null)
            {
                        strSql.Append(" and GraphNo="+model.GraphNo+" ");
                        }
                                          
            if(model.EquipNo !=null)
            {
                        strSql.Append(" and EquipNo="+model.EquipNo+" ");
                        }
                                          
            if(model.PointType !=null)
            {
                        strSql.Append(" and PointType="+model.PointType+" ");
                        }
                                          
            if(model.ShowPoint !=null)
            {
                        strSql.Append(" and ShowPoint="+model.ShowPoint+" ");
                        }
                                          
            if(model.MColor !=null)
            {
                        strSql.Append(" and MColor="+model.MColor+" ");
                        }
                                          
            if(model.SColor !=null)
            {
                        strSql.Append(" and SColor='"+model.SColor+"' ");
                        }
                                          
            if(model.ShowAxis !=null)
            {
                        strSql.Append(" and ShowAxis="+model.ShowAxis+" ");
                        }
                                          
            if(model.ShowLable !=null)
            {
                        strSql.Append(" and ShowLable="+model.ShowLable+" ");
                        }
                                          
            if(model.MinX !=null)
            {
                        strSql.Append(" and MinX="+model.MinX+" ");
                        }
                                          
            if(model.MaxX !=null)
            {
                        strSql.Append(" and MaxX="+model.MaxX+" ");
                        }
                                          
            if(model.MinY !=null)
            {
                        strSql.Append(" and MinY="+model.MinY+" ");
                        }
                                          
            if(model.MaxY !=null)
            {
                        strSql.Append(" and MaxY="+model.MaxY+" ");
                        }
                                          
            if(model.ShowTitle !=null)
            {
                        strSql.Append(" and ShowTitle="+model.ShowTitle+" ");
                        }
                                          
            if(model.STitle !=null)
            {
                        strSql.Append(" and STitle='"+model.STitle+"' ");
                        }
                                          
            if(model.GraphValue !=null)
            {
                        strSql.Append(" and GraphValue='"+model.GraphValue+"' ");
                        }
                                          
            if(model.GraphMemo !=null)
            {
                        strSql.Append(" and GraphMemo='"+model.GraphMemo+"' ");
                        }
                                          
            if(model.GraphF1 !=null)
            {
                        strSql.Append(" and GraphF1='"+model.GraphF1+"' ");
                        }
                                          
            if(model.GraphF2 !=null)
            {
                        strSql.Append(" and GraphF2='"+model.GraphF2+"' ");
                        }
                                          
            if(model.ChartTop !=null)
            {
                        strSql.Append(" and ChartTop="+model.ChartTop+" ");
                        }
                                          
            if(model.ChartHeight !=null)
            {
                        strSql.Append(" and ChartHeight="+model.ChartHeight+" ");
                        }
                                          
            if(model.ChartLeft !=null)
            {
                        strSql.Append(" and ChartLeft="+model.ChartLeft+" ");
                        }
                                          
            if(model.ChartWidth !=null)
            {
                        strSql.Append(" and ChartWidth="+model.ChartWidth+" ");
                        }
                                          
            if(model.Graphjpg !=null)
            {
                        strSql.Append(" and Graphjpg='"+model.Graphjpg+"' ");
                        }
                                          
            if(model.FormNo !=null)
            {
                        strSql.Append(" and FormNo="+model.FormNo+" ");
                        }
                                          
            if(model.UnionKey !=null)
            {
                        strSql.Append(" and UnionKey="+model.UnionKey+" ");
                        }
                                          
            if(model.DTimeStampe !=null)
            {
                        strSql.Append(" and DTimeStampe='"+model.DTimeStampe+"' ");
                        }
                                          
            if(model.AddTime !=null)
            {
                        strSql.Append(" and AddTime='"+model.AddTime+"' ");
                        }
                                          
            if(model.StandCode !=null)
            {
                        strSql.Append(" and StandCode='"+model.StandCode+"' ");
                        }
                                          
            if(model.ZFStandCode !=null)
            {
                        strSql.Append(" and ZFStandCode='"+model.ZFStandCode+"' ");
                        }
                                          
            if(model.UseFlag !=null)
            {
                        strSql.Append(" and UseFlag="+model.UseFlag+" ");
                        }
                                    string strCount = idb.ExecuteScalar(strSql.ToString());
            if (strCount != null && strCount.Trim() != "")
            {
                return Convert.ToInt32(strCount.Trim());
            }
            else
            {
                return 0;
            }
        }
        
        /// <summary>
        /// 利用标识列分页
        /// </summary>
        /// <param name="model">model=null时为中心字典表分页；model!=null时为中心--实验室 对照关系分页</param>
        /// <param name="nowPageNum">第几页</param>
        /// <param name="nowPageSize">每页多少行</param>
        /// <returns>DataSet</returns>
        public DataSet GetListByPage(ZhiFang.Model.RFGraphData model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            if (model != null && model.LabCode != null)
            {
                strSql.Append(" select top " + nowPageSize + "  * from B_RFGraphData left join B_RFGraphDataControl on B_RFGraphData.GraphName,GraphNo,FormNo=B_RFGraphDataControl.GraphName,GraphNo,FormNo ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and B_RFGraphDataControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append("where GraphID not in ( ");
                strSql.Append("select top " + (nowPageSize * nowPageNum) + " GraphID from  B_RFGraphData left join B_RFGraphDataControl on B_RFGraphData.GraphName,GraphNo,FormNo=B_RFGraphDataControl.GraphName,GraphNo,FormNo ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and B_RFGraphDataControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append("order by B_RFGraphData.GraphID ) order by B_RFGraphData.GraphID ");
                return idb.ExecuteDataSet(strSql.ToString());
            }
            else
            {
                strSql.Append("select top " + nowPageSize + "  * from B_RFGraphData where GraphID not in  ");
                strSql.Append("(select top " + (nowPageSize * nowPageNum) + " GraphID from B_RFGraphData order by GraphID) order by GraphID  ");
                return idb.ExecuteDataSet(strSql.ToString());
            }
        }

        public bool Exists(string GraphName, int GraphNo, string FormNo)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from B_RFGraphData ");
            strSql.Append(" where GraphName='" + GraphName + "' and GraphNo='" + GraphNo + "' and FormNo ='" + FormNo + "'");
			string strCount = idb.ExecuteScalar(strSql.ToString());
            if (strCount != null && strCount.Trim() != "" && strCount.Trim()!="0")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
		
		public bool CopyToLab(List<string> lst)
		{
			System.Collections.ArrayList arrySql = new System.Collections.ArrayList();
			string LabTableName="B_RFGraphData";
			LabTableName="B_Lab_"+LabTableName.Substring(LabTableName.IndexOf("_") + 1, LabTableName.Length - (LabTableName.IndexOf("_") + 1));
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSqlControl = new StringBuilder();
            string TableKey="GraphName,GraphNo,FormNo";
            string TableKeySub=TableKey;
            if(TableKey.ToLower().Contains("no"))
            {
            	TableKeySub=TableKey.Substring(0,TableKey.ToLower().IndexOf("no"));
            }
            try
            {
	            for (int i = 0; i < lst.Count; i++)
	            {
	                	strSql.Append("insert into "+LabTableName+"( LabCode,");			
	            		strSql.Append(" LabGraphName , LabGraphNo , EquipNo , PointType , ShowPoint , MColor , SColor , ShowAxis , ShowLable , MinX , MaxX , MinY , MaxY , ShowTitle , STitle , GraphValue , GraphMemo , GraphF1 , GraphF2 , ChartTop , ChartHeight , ChartLeft , ChartWidth , Graphjpg , LabFormNo , UnionKey , StandCode , ZFStandCode , UseFlag ");
						strSql.Append(") select '" + lst[i].Trim() + "' as LabCode, ");
	            		strSql.Append("GraphName,GraphNo,EquipNo,PointType,ShowPoint,MColor,SColor,ShowAxis,ShowLable,MinX,MaxX,MinY,MaxY,ShowTitle,STitle,GraphValue,GraphMemo,GraphF1,GraphF2,ChartTop,ChartHeight,ChartLeft,ChartWidth,Graphjpg,FormNo,UnionKey,StandCode,ZFStandCode,UseFlag");            
	            		strSql.Append(" from B_RFGraphData ");    
	            		
	            		strSqlControl.Append("insert into B_RFGraphDataControl ( ");
	            		strSqlControl.Append(" "+TableKeySub+"ControlNo,"+TableKey+",ControlLabNo,Control"+TableKey+",UseFlag ");
	            		strSqlControl.Append(")  select ");
	            		strSqlControl.Append("  '" + lst[i].Trim() + "'+'_'+convert(varchar(50)," + TableKey + ")+'_'+convert(varchar(50), " + TableKey + ") as "+TableKeySub+"ControlNo,"+TableKey+",'" + lst[i].Trim() + "' as ControlLabNo,"+TableKey+",UseFlag ");
	            		strSqlControl.Append(" from B_RFGraphData ");  
	            		
	            		arrySql.Add(strSql.ToString());
	            		arrySql.Add(strSqlControl.ToString());	    
	            		
	            		strSql = new StringBuilder();
             			strSqlControl = new StringBuilder();
             			
	             }

                idb.BatchUpdateWithTransaction(arrySql);
                d_log.OperateLog("RFGraphData", "", "", DateTime.Now, 1);
	             return true;
            }
            catch
            {
            	return false;
            }
           
		}
		
		public int GetMaxId()
        {
            return idb.GetMaxID("GraphName,GraphNo,FormNo","B_RFGraphData");
        }

        public DataSet GetList(int Top, ZhiFang.Model.RFGraphData model, string filedOrder)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" * ");
			strSql.Append(" FROM B_RFGraphData ");		
			
			                                          
            if(model.GraphName !=null)
            {
                        
            strSql.Append(" and GraphName='"+model.GraphName+"' ");
                        }
                                          
            if(model.GraphNo !=null)
            {
                        strSql.Append(" and GraphNo="+model.GraphNo+" ");
                        }
                                          
            if(model.EquipNo !=null)
            {
                        strSql.Append(" and EquipNo="+model.EquipNo+" ");
                        }
                                          
            if(model.PointType !=null)
            {
                        strSql.Append(" and PointType="+model.PointType+" ");
                        }
                                          
            if(model.ShowPoint !=null)
            {
                        strSql.Append(" and ShowPoint="+model.ShowPoint+" ");
                        }
                                          
            if(model.MColor !=null)
            {
                        strSql.Append(" and MColor="+model.MColor+" ");
                        }
                                          
            if(model.SColor !=null)
            {
                        
            strSql.Append(" and SColor='"+model.SColor+"' ");
                        }
                                          
            if(model.ShowAxis !=null)
            {
                        strSql.Append(" and ShowAxis="+model.ShowAxis+" ");
                        }
                                          
            if(model.ShowLable !=null)
            {
                        strSql.Append(" and ShowLable="+model.ShowLable+" ");
                        }
                                          
            if(model.MinX !=null)
            {
                        strSql.Append(" and MinX="+model.MinX+" ");
                        }
                                          
            if(model.MaxX !=null)
            {
                        strSql.Append(" and MaxX="+model.MaxX+" ");
                        }
                                          
            if(model.MinY !=null)
            {
                        strSql.Append(" and MinY="+model.MinY+" ");
                        }
                                          
            if(model.MaxY !=null)
            {
                        strSql.Append(" and MaxY="+model.MaxY+" ");
                        }
                                          
            if(model.ShowTitle !=null)
            {
                        strSql.Append(" and ShowTitle="+model.ShowTitle+" ");
                        }
                                          
            if(model.STitle !=null)
            {
                        
            strSql.Append(" and STitle='"+model.STitle+"' ");
                        }
                                          
            if(model.GraphValue !=null)
            {
                        
            strSql.Append(" and GraphValue='"+model.GraphValue+"' ");
                        }
                                          
            if(model.GraphMemo !=null)
            {
                        
            strSql.Append(" and GraphMemo='"+model.GraphMemo+"' ");
                        }
                                          
            if(model.GraphF1 !=null)
            {
                        
            strSql.Append(" and GraphF1='"+model.GraphF1+"' ");
                        }
                                          
            if(model.GraphF2 !=null)
            {
                        
            strSql.Append(" and GraphF2='"+model.GraphF2+"' ");
                        }
                                          
            if(model.ChartTop !=null)
            {
                        strSql.Append(" and ChartTop="+model.ChartTop+" ");
                        }
                                          
            if(model.ChartHeight !=null)
            {
                        strSql.Append(" and ChartHeight="+model.ChartHeight+" ");
                        }
                                          
            if(model.ChartLeft !=null)
            {
                        strSql.Append(" and ChartLeft="+model.ChartLeft+" ");
                        }
                                          
            if(model.ChartWidth !=null)
            {
                        strSql.Append(" and ChartWidth="+model.ChartWidth+" ");
                        }
                                          
            if(model.Graphjpg !=null)
            {
                        
            strSql.Append(" and Graphjpg='"+model.Graphjpg+"' ");
                        }
                                          
            if(model.FormNo !=null)
            {
                        strSql.Append(" and FormNo="+model.FormNo+" ");
                        }
                                          
            if(model.UnionKey !=null)
            {
                        strSql.Append(" and UnionKey="+model.UnionKey+" ");
                        }
                                          
            if(model.DTimeStampe !=null)
            {
                        
            strSql.Append(" and DTimeStampe='"+model.DTimeStampe+"' ");
                        }
                                          
            if(model.AddTime !=null)
            {
                        
            strSql.Append(" and AddTime='"+model.AddTime+"' ");
                        }
                                          
            if(model.StandCode !=null)
            {
                        
            strSql.Append(" and StandCode='"+model.StandCode+"' ");
                        }
                                          
            if(model.ZFStandCode !=null)
            {
                        
            strSql.Append(" and ZFStandCode='"+model.ZFStandCode+"' ");
                        }
                                          
            if(model.UseFlag !=null)
            {
                        strSql.Append(" and UseFlag="+model.UseFlag+" ");
                        }
                                    
			strSql.Append(" order by " + filedOrder);
			return idb.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetAllList()
        {
            return GetList("");
        }

        #region IDGetListByTimeStampe 成员

        public DataSet GetListByTimeStampe(string LabCode, int dTimeStampe)
        {
            DataSet dsAll = new DataSet();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from B_RFGraphData where 1=1 ");
            if (dTimeStampe != -999999)
            {
                strSql.Append(" and convert(int,DTimeStampe) > '" + dTimeStampe + "' ");
            }
            DataTable dtServer = idb.ExecuteDataSet(strSql.ToString()).Tables[0];
            dtServer.TableName = "ServerDatas";

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("select * from B_Lab_RFGraphData where 1=1 ");
            if (LabCode.Trim() != "")
            {
                strSql2.Append(" and LabCode= '" + LabCode + "' ");
            }
            if (dTimeStampe != -999999)
            {
                strSql2.Append(" and convert(int,DTimeStampe) > '" + dTimeStampe + "' ");
            }
            DataTable dtLab = idb.ExecuteDataSet(strSql2.ToString()).Tables[0];
            dtLab.TableName = "LabDatas";

            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("select * from B_RFGraphDataControl where 1=1 ");
            if (LabCode.Trim() != "")
            {
                strSql3.Append(" and ControlLabNo= '" + LabCode + "' ");
            }
            if (dTimeStampe != -999999)
            {
                strSql3.Append(" and convert(int,DTimeStampe) > '" + dTimeStampe + "' ");
            }
            DataTable dtControl = idb.ExecuteDataSet(strSql3.ToString()).Tables[0];
            dtControl.TableName = "ControlDatas";

            dsAll.Tables.Add(dtServer.Copy());
            dsAll.Tables.Add(dtLab.Copy());
            dsAll.Tables.Add(dtControl.Copy());
            return dsAll;
        }

        #endregion



        #region IDataBase<RFGraphData> 成员
        public int AddUpdateByDataSet(DataSet ds)
        {
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    int count = 0;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = ds.Tables[0].Rows[i];
                        if (this.Exists(ds.Tables[0].Rows[i]["GraphName"].ToString().Trim(), int.Parse(ds.Tables[0].Rows[i]["GraphNo"].ToString().Trim()), ds.Tables[0].Rows[i]["FormNo"].ToString().Trim()))
                        {
                            count += this.UpdateByDataRow(dr);
                        }
                        else
                            count += this.AddByDataRow(dr);
                    }
                    if (count == ds.Tables[0].Rows.Count)
                        return 1;
                    else
                        return 0;
                }
                catch
                {
                    return 0;
                }
            }
            else
                return 1;
        }
        public int AddByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into B_RFGraphData (");
                strSql.Append("GraphName,GraphNo,EquipNo,PointType,ShowPoint,MColor,SColor,ShowAxis,ShowLable,MinX,MaxX,MinY,MaxY,ShowTitle,STitle,GraphValue,GraphMemo,GraphF1,GraphF2,ChartTop,ChartHeight,ChartLeft,ChartWidth,Graphjpg,FormNo,UnionKey,AddTime,StandCode,ZFStandCode,UseFlag");
                strSql.Append(") values (");
                strSql.Append("'" + dr["GraphName"].ToString().Trim() + "','" + dr["GraphNo"].ToString().Trim() + "','" + dr["EquipNo"].ToString().Trim() + "','" + dr["PointType"].ToString().Trim() + "','" + dr["ShowPoint"].ToString().Trim() + "','" + dr["MColor"].ToString().Trim() + "','" + dr["SColor"].ToString().Trim() + "','" + dr["ShowAxis"].ToString().Trim() + "','" + dr["ShowLable"].ToString().Trim() + "','" + dr["MinX"].ToString().Trim() + "','" + dr["MaxX"].ToString().Trim() + "','" + dr["MinY"].ToString().Trim() + "','" + dr["MaxY"].ToString().Trim() + "','" + dr["ShowTitle"].ToString().Trim() + "','" + dr["STitle"].ToString().Trim() + "','" + dr["GraphValue"].ToString().Trim() + "','" + dr["GraphMemo"].ToString().Trim() + "','" + dr["GraphF1"].ToString().Trim() + "','" + dr["GraphF2"].ToString().Trim() + "','" + dr["ChartTop"].ToString().Trim() + "','" + dr["ChartHeight"].ToString().Trim() + "','" + dr["ChartLeft"].ToString().Trim() + "','" + dr["ChartWidth"].ToString().Trim() + "','" + dr["Graphjpg"].ToString().Trim() + "','" + dr["FormNo"].ToString().Trim() + "','" + dr["UnionKey"].ToString().Trim() + "','" + DateTime.Parse(dr["AddTime"].ToString().Trim()) + "','" + dr["StandCode"].ToString().Trim() + "','" + dr["ZFStandCode"].ToString().Trim() + "','" + dr["UseFlag"].ToString().Trim() + "'");
                strSql.Append(") ");
                return idb.ExecuteNonQuery(strSql.ToString());
            }
            catch
            {
                return 0;
            }
        }
        public int UpdateByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update B_RFGraphData set ");

                strSql.Append(" EquipNo = '" + dr["EquipNo"].ToString().Trim() + "' , ");
                strSql.Append(" PointType = '" + dr["PointType"].ToString().Trim() + "' , ");
                strSql.Append(" ShowPoint = '" + dr["ShowPoint"].ToString().Trim() + "' , ");
                strSql.Append(" MColor = '" + dr["MColor"].ToString().Trim() + "' , ");
                strSql.Append(" SColor = '" + dr["SColor"].ToString().Trim() + "' , ");
                strSql.Append(" ShowAxis = '" + dr["ShowAxis"].ToString().Trim() + "' , ");
                strSql.Append(" ShowLable = '" + dr["ShowLable"].ToString().Trim() + "' , ");
                strSql.Append(" MinX = '" + dr["MinX"].ToString().Trim() + "' , ");
                strSql.Append(" MaxX = '" + dr["MaxX"].ToString().Trim() + "' , ");
                strSql.Append(" MinY = '" + dr["MinY"].ToString().Trim() + "' , ");
                strSql.Append(" MaxY = '" + dr["MaxY"].ToString().Trim() + "' , ");
                strSql.Append(" ShowTitle = '" + dr["ShowTitle"].ToString().Trim() + "' , ");
                strSql.Append(" STitle = '" + dr["STitle"].ToString().Trim() + "' , ");
                strSql.Append(" GraphValue = '" + dr["GraphValue"].ToString().Trim() + "' , ");
                strSql.Append(" GraphMemo = '" + dr["GraphMemo"].ToString().Trim() + "' , ");
                strSql.Append(" GraphF1 = '" + dr["GraphF1"].ToString().Trim() + "' , ");
                strSql.Append(" GraphF2 = '" + dr["GraphF2"].ToString().Trim() + "' , ");
                strSql.Append(" ChartTop = '" + dr["ChartTop"].ToString().Trim() + "' , ");
                strSql.Append(" ChartHeight = '" + dr["ChartHeight"].ToString().Trim() + "' , ");
                strSql.Append(" ChartLeft = '" + dr["ChartLeft"].ToString().Trim() + "' , ");
                strSql.Append(" ChartWidth = '" + dr["ChartWidth"].ToString().Trim() + "' , ");
                strSql.Append(" Graphjpg = '" + dr["Graphjpg"].ToString().Trim() + "' , ");
                strSql.Append(" UnionKey = '" + dr["UnionKey"].ToString().Trim() + "' , ");
                strSql.Append(" StandCode = '" + dr["StandCode"].ToString().Trim() + "' , ");
                strSql.Append(" ZFStandCode = '" + dr["ZFStandCode"].ToString().Trim() + "' , ");
                strSql.Append(" UseFlag = '" + dr["UseFlag"].ToString().Trim() + "'  ");
                strSql.Append(" where GraphName='" + dr["GraphName"].ToString().Trim() + "' and GraphName,GraphNo,FormNo='" + dr["GraphNo"].ToString().Trim() + "' and FormNo='" + dr["FormNo"].ToString().Trim() + "' ");

                return idb.ExecuteNonQuery(strSql.ToString());
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region IDBatchCopy 成员


        public int DeleteByDataRow(DataRow dr)
        {
            throw new NotImplementedException();
        }

        #endregion


        public bool IsExist(string labCodeNo)
        {
            throw new NotImplementedException();
        }

        public bool DeleteByLabCode(string LabCodeNo)
        {
            throw new NotImplementedException();
        }
    }
}

