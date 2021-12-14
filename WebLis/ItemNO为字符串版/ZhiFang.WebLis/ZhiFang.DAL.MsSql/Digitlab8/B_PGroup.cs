using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Digitlab8  
{
	 	//B_PGroup
		
	public partial class B_PGroup : IDPGroup,IDBatchCopy,IDGetListByTimeStampe
	{	
		DBUtility.IDBConnection idb;
        public B_PGroup(string dbsourceconn)
        {
            idb = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
		public B_PGroup()
		{
            idb = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.LisDB());
        }
		D_LogInfo d_log = new D_LogInfo();
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ZhiFang.Model.PGroup model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into B_PGroup(");			
            strSql.Append("SectionNo,SuperGroupNo,CName,ShortName,ShortCode,SectionDesc,SectionType,Visible,DispOrder,OnlineTime,KeyDispOrder,DummyGroup,UnionType,SectorTypeNo,SampleRule,StandCode,ZFStandCode,UseFlag");
			strSql.Append(") values (");
            strSql.Append("@SectionNo,@SuperGroupNo,@CName,@ShortName,@ShortCode,@SectionDesc,@SectionType,@Visible,@DispOrder,@OnlineTime,@KeyDispOrder,@DummyGroup,@UnionType,@SectorTypeNo,@SampleRule,@StandCode,@ZFStandCode,@UseFlag");            
            strSql.Append(") ");            
            
			SqlParameter[] parameters = {
			            new SqlParameter("@SectionNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@SuperGroupNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@CName", SqlDbType.VarChar,40) ,            
                        new SqlParameter("@ShortName", SqlDbType.VarChar,40) ,            
                        new SqlParameter("@ShortCode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@SectionDesc", SqlDbType.VarChar,250) ,            
                        new SqlParameter("@SectionType", SqlDbType.Int,4) ,            
                        new SqlParameter("@Visible", SqlDbType.Int,4) ,            
                        new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            
                        new SqlParameter("@OnlineTime", SqlDbType.Int,4) ,            
                        new SqlParameter("@KeyDispOrder", SqlDbType.Int,4) ,            
                        new SqlParameter("@DummyGroup", SqlDbType.Int,4) ,            
                        new SqlParameter("@UnionType", SqlDbType.Int,4) ,            
                        new SqlParameter("@SectorTypeNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@SampleRule", SqlDbType.Int,4) ,            
                        new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ZFStandCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@UseFlag", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.SectionNo;                        
            parameters[1].Value = model.SuperGroupNo;                        
            parameters[2].Value = model.CName;                        
            parameters[3].Value = model.ShortName;                        
            parameters[4].Value = model.ShortCode;                        
            parameters[5].Value = model.SectionDesc;                        
            parameters[6].Value = model.SectionType;                        
            parameters[7].Value = model.Visible;                        
            parameters[8].Value = model.DispOrder;                        
            parameters[9].Value = model.OnlineTime;                        
            parameters[10].Value = model.KeyDispOrder;                        
            parameters[11].Value = model.DummyGroup;                        
            parameters[12].Value = model.UnionType;                        
            parameters[13].Value = model.SectorTypeNo;                        
            parameters[14].Value = model.SampleRule;                        
            parameters[15].Value = model.StandCode;                        
            parameters[16].Value = model.ZFStandCode;                        
            parameters[17].Value = model.UseFlag;                  
			if (idb.ExecuteNonQuery(strSql.ToString(),parameters) > 0)
            {
                return d_log.OperateLog("PGroup", "", "", DateTime.Now, 1);
            }
            else
                return -1;
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ZhiFang.Model.PGroup model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update B_PGroup set ");
			                                                
            strSql.Append(" SectionNo = @SectionNo , ");                                    
            strSql.Append(" SuperGroupNo = @SuperGroupNo , ");                                    
            strSql.Append(" CName = @CName , ");                                    
            strSql.Append(" ShortName = @ShortName , ");                                    
            strSql.Append(" ShortCode = @ShortCode , ");                                    
            strSql.Append(" SectionDesc = @SectionDesc , ");                                    
            strSql.Append(" SectionType = @SectionType , ");                                    
            strSql.Append(" Visible = @Visible , ");                                    
            strSql.Append(" DispOrder = @DispOrder , ");                                    
            strSql.Append(" OnlineTime = @OnlineTime , ");                                    
            strSql.Append(" KeyDispOrder = @KeyDispOrder , ");                                    
            strSql.Append(" DummyGroup = @DummyGroup , ");                                    
            strSql.Append(" UnionType = @UnionType , ");                                    
            strSql.Append(" SectorTypeNo = @SectorTypeNo , ");                                    
            strSql.Append(" SampleRule = @SampleRule , ");                                                                                    
            strSql.Append(" StandCode = @StandCode , ");                                    
            strSql.Append(" ZFStandCode = @ZFStandCode , ");                                    
            strSql.Append(" UseFlag = @UseFlag  ");            			
			strSql.Append(" where SectionNo=@SectionNo  ");
						
			SqlParameter[] parameters = {
			            	
                           
            new SqlParameter("@SectionNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@SuperGroupNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@CName", SqlDbType.VarChar,40) ,            	
                           
            new SqlParameter("@ShortName", SqlDbType.VarChar,40) ,            	
                           
            new SqlParameter("@ShortCode", SqlDbType.VarChar,10) ,            	
                           
            new SqlParameter("@SectionDesc", SqlDbType.VarChar,250) ,            	
                           
            new SqlParameter("@SectionType", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@Visible", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@OnlineTime", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@KeyDispOrder", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@DummyGroup", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@UnionType", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@SectorTypeNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@SampleRule", SqlDbType.Int,4) ,            	
                        	
                        	
                           
            new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@ZFStandCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@UseFlag", SqlDbType.Int,4)             	
              
            };
            			    
				
                
			   
			if(model.SectionNo!=null)
			{
            	parameters[0].Value = model.SectionNo;            	
            }
            	
                
			   
			if(model.SuperGroupNo!=null)
			{
            	parameters[1].Value = model.SuperGroupNo;            	
            }
            	
                
			   
			if(model.CName!=null)
			{
            	parameters[2].Value = model.CName;            	
            }
            	
                
			   
			if(model.ShortName!=null)
			{
            	parameters[3].Value = model.ShortName;            	
            }
            	
                
			   
			if(model.ShortCode!=null)
			{
            	parameters[4].Value = model.ShortCode;            	
            }
            	
                
			   
			if(model.SectionDesc!=null)
			{
            	parameters[5].Value = model.SectionDesc;            	
            }
            	
                
			   
			if(model.SectionType!=null)
			{
            	parameters[6].Value = model.SectionType;            	
            }
            	
                
			   
			if(model.Visible!=null)
			{
            	parameters[7].Value = model.Visible;            	
            }
            	
                
			   
			if(model.DispOrder!=null)
			{
            	parameters[8].Value = model.DispOrder;            	
            }
            	
                
			   
			if(model.OnlineTime!=null)
			{
            	parameters[9].Value = model.OnlineTime;            	
            }
            	
                
			   
			if(model.KeyDispOrder!=null)
			{
            	parameters[10].Value = model.KeyDispOrder;            	
            }
            	
                
			   
			if(model.DummyGroup!=null)
			{
            	parameters[11].Value = model.DummyGroup;            	
            }
            	
                
			   
			if(model.UnionType!=null)
			{
            	parameters[12].Value = model.UnionType;            	
            }
            	
                
			   
			if(model.SectorTypeNo!=null)
			{
            	parameters[13].Value = model.SectorTypeNo;            	
            }
            	
                
			   
			if(model.SampleRule!=null)
			{
            	parameters[14].Value = model.SampleRule;            	
            }
            	
                
				
                
				
                
			   
			if(model.StandCode!=null)
			{
            	parameters[15].Value = model.StandCode;            	
            }
            	
                
			   
			if(model.ZFStandCode!=null)
			{
            	parameters[16].Value = model.ZFStandCode;            	
            }
            	
                
			   
			if(model.UseFlag!=null)
			{
            	parameters[17].Value = model.UseFlag;            	
            }
            	
                        
			if (idb.ExecuteNonQuery(strSql.ToString(),parameters) > 0)
            {
                return d_log.OperateLog("PGroup", "", "", DateTime.Now, 1);
            }
            else
                return -1;
		}
		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int SectionNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from B_PGroup ");
			strSql.Append(" where SectionNo=@SectionNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@SectionNo", SqlDbType.Int,4)};
			parameters[0].Value = SectionNo;


			return idb.ExecuteNonQuery(strSql.ToString(),parameters);
		
		}
		
				/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteList(string SectionIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from B_PGroup ");
			strSql.Append(" where ID in ("+SectionIDlist + ")  ");
			return idb.ExecuteNonQuery(strSql.ToString());
			
		}
				
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZhiFang.Model.PGroup GetModel(int SectionNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select SectionID, SectionNo, SuperGroupNo, CName, ShortName, ShortCode, SectionDesc, SectionType, Visible, DispOrder, OnlineTime, KeyDispOrder, DummyGroup, UnionType, SectorTypeNo, SampleRule, DTimeStampe, AddTime, StandCode, ZFStandCode, UseFlag  ");			
			strSql.Append("  from B_PGroup ");
			strSql.Append(" where SectionNo=@SectionNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@SectionNo", SqlDbType.Int,4)};
			parameters[0].Value = SectionNo;

			
			ZhiFang.Model.PGroup model=new ZhiFang.Model.PGroup();
			DataSet ds=idb.ExecuteDataSet(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																if(ds.Tables[0].Rows[0]["SectionID"].ToString()!="")
				{
					model.SectionID=int.Parse(ds.Tables[0].Rows[0]["SectionID"].ToString());
				}
																																				if(ds.Tables[0].Rows[0]["SectionNo"].ToString()!="")
				{
					model.SectionNo=int.Parse(ds.Tables[0].Rows[0]["SectionNo"].ToString());
				}
																																				if(ds.Tables[0].Rows[0]["SuperGroupNo"].ToString()!="")
				{
					model.SuperGroupNo=int.Parse(ds.Tables[0].Rows[0]["SuperGroupNo"].ToString());
				}
																																								model.CName= ds.Tables[0].Rows[0]["CName"].ToString();
																																				model.ShortName= ds.Tables[0].Rows[0]["ShortName"].ToString();
																																				model.ShortCode= ds.Tables[0].Rows[0]["ShortCode"].ToString();
																																				model.SectionDesc= ds.Tables[0].Rows[0]["SectionDesc"].ToString();
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
																																				if(ds.Tables[0].Rows[0]["OnlineTime"].ToString()!="")
				{
					model.OnlineTime=int.Parse(ds.Tables[0].Rows[0]["OnlineTime"].ToString());
				}
																																				if(ds.Tables[0].Rows[0]["KeyDispOrder"].ToString()!="")
				{
					model.KeyDispOrder=int.Parse(ds.Tables[0].Rows[0]["KeyDispOrder"].ToString());
				}
																																				if(ds.Tables[0].Rows[0]["DummyGroup"].ToString()!="")
				{
					model.DummyGroup=int.Parse(ds.Tables[0].Rows[0]["DummyGroup"].ToString());
				}
																																				if(ds.Tables[0].Rows[0]["UnionType"].ToString()!="")
				{
					model.UnionType=int.Parse(ds.Tables[0].Rows[0]["UnionType"].ToString());
				}
																																				if(ds.Tables[0].Rows[0]["SectorTypeNo"].ToString()!="")
				{
					model.SectorTypeNo=int.Parse(ds.Tables[0].Rows[0]["SectorTypeNo"].ToString());
				}
																																				if(ds.Tables[0].Rows[0]["SampleRule"].ToString()!="")
				{
					model.SampleRule=int.Parse(ds.Tables[0].Rows[0]["SampleRule"].ToString());
				}
																																								if(ds.Tables[0].Rows[0]["AddTime"].ToString()!="")
				{
					model.AddTime=DateTime.Parse(ds.Tables[0].Rows[0]["AddTime"].ToString());
				}
																																								model.StandCode= ds.Tables[0].Rows[0]["StandCode"].ToString();
																																				model.ZFStandCode= ds.Tables[0].Rows[0]["ZFStandCode"].ToString();
																																if(ds.Tables[0].Rows[0]["UseFlag"].ToString()!="")
				{
					model.UseFlag=int.Parse(ds.Tables[0].Rows[0]["UseFlag"].ToString());
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
			strSql.Append(" FROM B_PGroup ");
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
			strSql.Append(" FROM B_PGroup ");
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
		public DataSet GetList(ZhiFang.Model.PGroup model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM B_PGroup where 1=1 ");
			                                                                
             
            if(model.SectionNo !=0)
                        {
                        strSql.Append(" and SectionNo="+model.SectionNo+" ");
                        }
                                                    
                        if(model.SuperGroupNo !=null)
                        {
                        strSql.Append(" and SuperGroupNo="+model.SuperGroupNo+" ");
                        }
                                                    
                        if(model.CName !=null)
                        {
                        strSql.Append(" and CName='"+model.CName+"' ");
                        }
                                                    
                        if(model.ShortName !=null)
                        {
                        strSql.Append(" and ShortName='"+model.ShortName+"' ");
                        }
                                                    
                        if(model.ShortCode !=null)
                        {
                        strSql.Append(" and ShortCode='"+model.ShortCode+"' ");
                        }
                                                    
                        if(model.SectionDesc !=null)
                        {
                        strSql.Append(" and SectionDesc='"+model.SectionDesc+"' ");
                        }
                                                    
                        if(model.SectionType !=null)
                        {
                        strSql.Append(" and SectionType="+model.SectionType+" ");
                        }
                                                                
                        if(model.DispOrder !=null)
                        {
                        strSql.Append(" and DispOrder="+model.DispOrder+" ");
                        }
                                                    
                        if(model.OnlineTime !=null)
                        {
                        strSql.Append(" and OnlineTime="+model.OnlineTime+" ");
                        }
                                                    
                        if(model.KeyDispOrder !=null)
                        {
                        strSql.Append(" and KeyDispOrder="+model.KeyDispOrder+" ");
                        }
                                                    
                        if(model.DummyGroup !=null)
                        {
                        strSql.Append(" and DummyGroup="+model.DummyGroup+" ");
                        }
                                                    
                        if(model.UnionType !=null)
                        {
                        strSql.Append(" and UnionType="+model.UnionType+" ");
                        }
                                                    
                        if(model.SectorTypeNo !=null)
                        {
                        strSql.Append(" and SectorTypeNo="+model.SectorTypeNo+" ");
                        }
                                                    
                        if(model.SampleRule !=null)
                        {
                        strSql.Append(" and SampleRule="+model.SampleRule+" ");
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
            strSql.Append("select count(*) FROM B_PGroup ");
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
        public int GetTotalCount(ZhiFang.Model.PGroup model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_PGroup where 1=1 ");
           	                                          
            if(model.SectionNo !=null)
            {
                        strSql.Append(" and SectionNo="+model.SectionNo+" ");
                        }
                                          
            if(model.SuperGroupNo !=null)
            {
                        strSql.Append(" and SuperGroupNo="+model.SuperGroupNo+" ");
                        }
                                          
            if(model.CName !=null)
            {
                        strSql.Append(" and CName='"+model.CName+"' ");
                        }
                                          
            if(model.ShortName !=null)
            {
                        strSql.Append(" and ShortName='"+model.ShortName+"' ");
                        }
                                          
            if(model.ShortCode !=null)
            {
                        strSql.Append(" and ShortCode='"+model.ShortCode+"' ");
                        }
                                          
            if(model.SectionDesc !=null)
            {
                        strSql.Append(" and SectionDesc='"+model.SectionDesc+"' ");
                        }
                                          
            if(model.SectionType !=null)
            {
                        strSql.Append(" and SectionType="+model.SectionType+" ");
                        }
                                          
            if(model.Visible !=null)
            {
                        strSql.Append(" and Visible="+model.Visible+" ");
                        }
                                          
            if(model.DispOrder !=null)
            {
                        strSql.Append(" and DispOrder="+model.DispOrder+" ");
                        }
                                          
            if(model.OnlineTime !=null)
            {
                        strSql.Append(" and OnlineTime="+model.OnlineTime+" ");
                        }
                                          
            if(model.KeyDispOrder !=null)
            {
                        strSql.Append(" and KeyDispOrder="+model.KeyDispOrder+" ");
                        }
                                          
            if(model.DummyGroup !=null)
            {
                        strSql.Append(" and DummyGroup="+model.DummyGroup+" ");
                        }
                                          
            if(model.UnionType !=null)
            {
                        strSql.Append(" and UnionType="+model.UnionType+" ");
                        }
                                          
            if(model.SectorTypeNo !=null)
            {
                        strSql.Append(" and SectorTypeNo="+model.SectorTypeNo+" ");
                        }
                                          
            if(model.SampleRule !=null)
            {
                        strSql.Append(" and SampleRule="+model.SampleRule+" ");
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
        public DataSet GetListByPage(ZhiFang.Model.PGroup model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            if (model != null && model.LabCode != null)
            {
                strSql.Append(" select top " + nowPageSize + "  * from B_PGroup left join B_PGroupControl on B_PGroup.SectionNo=B_PGroupControl.SectionNo ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and B_PGroupControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append("where SectionID not in ( ");
                strSql.Append("select top " + (nowPageSize * nowPageNum) + " SectionID from  B_PGroup left join B_PGroupControl on B_PGroup.SectionNo=B_PGroupControl.SectionNo ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and B_PGroupControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append("order by B_PGroup.SectionID ) order by B_PGroup.SectionID ");
                return idb.ExecuteDataSet(strSql.ToString());
            }
            else
            {
                strSql.Append("select top " + nowPageSize + "  * from B_PGroup where SectionID not in  ");
                strSql.Append("(select top " + (nowPageSize * nowPageNum) + " SectionID from B_PGroup order by SectionID) order by SectionID  ");
                return idb.ExecuteDataSet(strSql.ToString());
            }
        }
        
        public bool Exists(int SectionNo)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from B_PGroup ");
			strSql.Append(" where SectionNo ='"+SectionNo+"'");
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
			string LabTableName="B_PGroup";
			LabTableName="B_Lab_"+LabTableName.Substring(LabTableName.IndexOf("_") + 1, LabTableName.Length - (LabTableName.IndexOf("_") + 1));
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSqlControl = new StringBuilder();
            string TableKey="SectionNo";
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
	            		strSql.Append(" LabSectionNo , SuperGroupNo , CName , ShortName , ShortCode , SectionDesc , SectionType , Visible , DispOrder , OnlineTime , KeyDispOrder , DummyGroup , UnionType , SectorTypeNo , SampleRule , StandCode , ZFStandCode , UseFlag ");
						strSql.Append(") select '" + lst[i].Trim() + "' as LabCode, ");
	            		strSql.Append("SectionNo,SuperGroupNo,CName,ShortName,ShortCode,SectionDesc,SectionType,Visible,DispOrder,OnlineTime,KeyDispOrder,DummyGroup,UnionType,SectorTypeNo,SampleRule,StandCode,ZFStandCode,UseFlag");            
	            		strSql.Append(" from B_PGroup ");    
	            		
	            		strSqlControl.Append("insert into B_PGroupControl ( ");
	            		strSqlControl.Append(" "+TableKeySub+"ControlNo,"+TableKey+",ControlLabNo,Control"+TableKey+",UseFlag ");
	            		strSqlControl.Append(")  select ");
	            		strSqlControl.Append("  '" + lst[i].Trim() + "'+'_'+convert(varchar(50)," + TableKey + ")+'_'+convert(varchar(50), " + TableKey + ") as "+TableKeySub+"ControlNo,"+TableKey+",'" + lst[i].Trim() + "' as ControlLabNo,"+TableKey+",UseFlag ");
	            		strSqlControl.Append(" from B_PGroup ");  
	            		
	            		arrySql.Add(strSql.ToString());
	            		arrySql.Add(strSqlControl.ToString());	    
	            		
	            		strSql = new StringBuilder();
             			strSqlControl = new StringBuilder();
             			
	             }

                idb.BatchUpdateWithTransaction(arrySql);
                d_log.OperateLog("PGroup", "", "", DateTime.Now, 1);
	             return true;
            }
            catch
            {
            	return false;
            }
           
		}
		
		public int GetMaxId()
        {
            return idb.GetMaxID("SectionNo","B_PGroup");
        }

        public DataSet GetList(int Top, ZhiFang.Model.PGroup model, string filedOrder)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" * ");
			strSql.Append(" FROM B_PGroup ");		
			
			                                          
            if(model.SectionNo !=null)
            {
                        strSql.Append(" and SectionNo="+model.SectionNo+" ");
                        }
                                          
            if(model.SuperGroupNo !=null)
            {
                        strSql.Append(" and SuperGroupNo="+model.SuperGroupNo+" ");
                        }
                                          
            if(model.CName !=null)
            {
                        
            strSql.Append(" and CName='"+model.CName+"' ");
                        }
                                          
            if(model.ShortName !=null)
            {
                        
            strSql.Append(" and ShortName='"+model.ShortName+"' ");
                        }
                                          
            if(model.ShortCode !=null)
            {
                        
            strSql.Append(" and ShortCode='"+model.ShortCode+"' ");
                        }
                                          
            if(model.SectionDesc !=null)
            {
                        
            strSql.Append(" and SectionDesc='"+model.SectionDesc+"' ");
                        }
                                          
            if(model.SectionType !=null)
            {
                        strSql.Append(" and SectionType="+model.SectionType+" ");
                        }
                                          
            if(model.Visible !=null)
            {
                        strSql.Append(" and Visible="+model.Visible+" ");
                        }
                                          
            if(model.DispOrder !=null)
            {
                        strSql.Append(" and DispOrder="+model.DispOrder+" ");
                        }
                                          
            if(model.OnlineTime !=null)
            {
                        strSql.Append(" and OnlineTime="+model.OnlineTime+" ");
                        }
                                          
            if(model.KeyDispOrder !=null)
            {
                        strSql.Append(" and KeyDispOrder="+model.KeyDispOrder+" ");
                        }
                                          
            if(model.DummyGroup !=null)
            {
                        strSql.Append(" and DummyGroup="+model.DummyGroup+" ");
                        }
                                          
            if(model.UnionType !=null)
            {
                        strSql.Append(" and UnionType="+model.UnionType+" ");
                        }
                                          
            if(model.SectorTypeNo !=null)
            {
                        strSql.Append(" and SectorTypeNo="+model.SectorTypeNo+" ");
                        }
                                          
            if(model.SampleRule !=null)
            {
                        strSql.Append(" and SampleRule="+model.SampleRule+" ");
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
            strSql.Append("select *,'" + LabCode + "' as LabCode,SectionNo as LabSectionNo from B_PGroup where 1=1 ");
            if (dTimeStampe != -999999)
            {
                strSql.Append(" and convert(int,DTimeStampe) > '" + dTimeStampe + "' ");
            }
            DataTable dtServer = idb.ExecuteDataSet(strSql.ToString()).Tables[0];
            dtServer.TableName = "ServerDatas";

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("select *,LabSectionNo as SectionNo from B_Lab_PGroup where 1=1 ");
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
            strSql3.Append("select * from B_PGroupControl where 1=1 ");
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

        #region IDPGroup 成员

        public bool Exists(int SectionNo, int Visible)
        {
            throw new NotImplementedException();
        }

        public int Delete(int SectionNo, int Visible)
        {
            throw new NotImplementedException();
        }

        public Model.PGroup GetModel(int SectionNo, int Visible)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDataBase<PGroup> 成员
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
                        if (this.Exists(int.Parse(ds.Tables[0].Rows[i]["SectionNo"].ToString().Trim())))
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
                strSql.Append("insert into B_PGroup (");
                strSql.Append("SectionNo,SuperGroupNo,CName,ShortName,ShortCode,SectionDesc,SectionType,Visible,DispOrder,OnlineTime,KeyDispOrder,DummyGroup,UnionType,SectorTypeNo,SampleRule,AddTime,StandCode,ZFStandCode,UseFlag");
                strSql.Append(") values (");
                strSql.Append("'" + dr["SectionNo"].ToString().Trim() + "','" + dr["SuperGroupNo"].ToString().Trim() + "','" + dr["CName"].ToString().Trim() + "','" + dr["ShortName"].ToString().Trim() + "','" + dr["ShortCode"].ToString().Trim() + "','" + dr["SectionDesc"].ToString().Trim() + "','" + dr["SectionType"].ToString().Trim() + "','" + dr["Visible"].ToString().Trim() + "','" + dr["DispOrder"].ToString().Trim() + "','" + dr["OnlineTime"].ToString().Trim() + "','" + dr["KeyDispOrder"].ToString().Trim() + "','" + dr["DummyGroup"].ToString().Trim() + "','" + dr["UnionType"].ToString().Trim() + "','" + dr["SectorTypeNo"].ToString().Trim() + "','" + dr["SampleRule"].ToString().Trim() + "','" + DateTime.Parse(dr["AddTime"].ToString().Trim()) + "','" + dr["StandCode"].ToString().Trim() + "','" + dr["ZFStandCode"].ToString().Trim() + "','" + dr["UseFlag"].ToString().Trim() + "'");
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
                strSql.Append("update B_PGroup set ");

                strSql.Append(" SuperGroupNo = '" + dr["SuperGroupNo"].ToString().Trim() + "' , ");
                strSql.Append(" CName = '" + dr["CName"].ToString().Trim() + "' , ");
                strSql.Append(" ShortName = '" + dr["ShortName"].ToString().Trim() + "' , ");
                strSql.Append(" ShortCode = '" + dr["ShortCode"].ToString().Trim() + "' , ");
                strSql.Append(" SectionDesc = '" + dr["SectionDesc"].ToString().Trim() + "' , ");
                strSql.Append(" SectionType = '" + dr["SectionType"].ToString().Trim() + "' , ");
                strSql.Append(" Visible = '" + dr["Visible"].ToString().Trim() + "' , ");
                strSql.Append(" DispOrder = '" + dr["DispOrder"].ToString().Trim() + "' , ");
                strSql.Append(" OnlineTime = '" + dr["OnlineTime"].ToString().Trim() + "' , ");
                strSql.Append(" KeyDispOrder = '" + dr["KeyDispOrder"].ToString().Trim() + "' , ");
                strSql.Append(" DummyGroup = '" + dr["DummyGroup"].ToString().Trim() + "' , ");
                strSql.Append(" UnionType = '" + dr["UnionType"].ToString().Trim() + "' , ");
                strSql.Append(" SectorTypeNo = '" + dr["SectorTypeNo"].ToString().Trim() + "' , ");
                strSql.Append(" SampleRule = '" + dr["SampleRule"].ToString().Trim() + "' , ");
                strSql.Append(" StandCode = '" + dr["StandCode"].ToString().Trim() + "' , ");
                strSql.Append(" ZFStandCode = '" + dr["ZFStandCode"].ToString().Trim() + "' , ");
                strSql.Append(" UseFlag = '" + dr["UseFlag"].ToString().Trim() + "'  ");
                strSql.Append(" where SectionNo='" + dr["SectionNo"].ToString().Trim() + "' ");

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


        public int Add(List<Model.PGroup> modelList)
        {
            throw new NotImplementedException();
        }


        public int Update(List<Model.PGroup> modelList)
        {
            throw new NotImplementedException();
        }
    }
}

