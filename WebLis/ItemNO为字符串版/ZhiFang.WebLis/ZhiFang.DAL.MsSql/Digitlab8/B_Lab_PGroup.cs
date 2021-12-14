using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Digitlab8  
{
	 	//B_Lab_PGroup
		
	public partial class B_Lab_PGroup: IDLab_PGroup	{	
		DBUtility.IDBConnection idb;
        public B_Lab_PGroup(string dbsourceconn)
        {
            idb = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
		public B_Lab_PGroup()
		{
            idb = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.LisDB());
        }
		D_LogInfo d_log = new D_LogInfo();
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ZhiFang.Model.Lab_PGroup model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into B_Lab_PGroup(");			
            strSql.Append("LabCode,LabSectionNo,SuperGroupNo,CName,ShortName,ShortCode,SectionDesc,SectionType,Visible,DispOrder,OnlineTime,KeyDispOrder,DummyGroup,UnionType,SectorTypeNo,SampleRule,StandCode,ZFStandCode,UseFlag");
			strSql.Append(") values (");
            strSql.Append("@LabCode,@LabSectionNo,@SuperGroupNo,@CName,@ShortName,@ShortCode,@SectionDesc,@SectionType,@Visible,@DispOrder,@OnlineTime,@KeyDispOrder,@DummyGroup,@UnionType,@SectorTypeNo,@SampleRule,@StandCode,@ZFStandCode,@UseFlag");            
            strSql.Append(") ");            
            
			SqlParameter[] parameters = {
			            new SqlParameter("@LabCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@LabSectionNo", SqlDbType.Int,4) ,            
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
			            
            parameters[0].Value = model.LabCode;                        
            parameters[1].Value = model.LabSectionNo;                        
            parameters[2].Value = model.SuperGroupNo;                        
            parameters[3].Value = model.CName;                        
            parameters[4].Value = model.ShortName;                        
            parameters[5].Value = model.ShortCode;                        
            parameters[6].Value = model.SectionDesc;                        
            parameters[7].Value = model.SectionType;                        
            parameters[8].Value = model.Visible;                        
            parameters[9].Value = model.DispOrder;                        
            parameters[10].Value = model.OnlineTime;                        
            parameters[11].Value = model.KeyDispOrder;                        
            parameters[12].Value = model.DummyGroup;                        
            parameters[13].Value = model.UnionType;                        
            parameters[14].Value = model.SectorTypeNo;                        
            parameters[15].Value = model.SampleRule;                        
            parameters[16].Value = model.StandCode;                        
            parameters[17].Value = model.ZFStandCode;                        
            parameters[18].Value = model.UseFlag;                  
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
		public int Update(ZhiFang.Model.Lab_PGroup model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update B_Lab_PGroup set ");
			                                                
            strSql.Append(" LabCode = @LabCode , ");                                    
            strSql.Append(" LabSectionNo = @LabSectionNo , ");                                    
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
			strSql.Append(" where LabCode=@LabCode and LabSectionNo=@LabSectionNo  ");
						
			SqlParameter[] parameters = {
			            	
                           
            new SqlParameter("@LabCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@LabSectionNo", SqlDbType.Int,4) ,            	
                           
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
            			    
				
                
			   
			if(model.LabCode!=null)
			{
            	parameters[0].Value = model.LabCode;            	
            }
            	
                
			   
			if(model.LabSectionNo!=null)
			{
            	parameters[1].Value = model.LabSectionNo;            	
            }
            	
                
			   
			if(model.SuperGroupNo!=null)
			{
            	parameters[2].Value = model.SuperGroupNo;            	
            }
            	
                
			   
			if(model.CName!=null)
			{
            	parameters[3].Value = model.CName;            	
            }
            	
                
			   
			if(model.ShortName!=null)
			{
            	parameters[4].Value = model.ShortName;            	
            }
            	
                
			   
			if(model.ShortCode!=null)
			{
            	parameters[5].Value = model.ShortCode;            	
            }
            	
                
			   
			if(model.SectionDesc!=null)
			{
            	parameters[6].Value = model.SectionDesc;            	
            }
            	
                
			   
			if(model.SectionType!=null)
			{
            	parameters[7].Value = model.SectionType;            	
            }
            	
                
			   
			if(model.Visible!=null)
			{
            	parameters[8].Value = model.Visible;            	
            }
            	
                
			   
			if(model.DispOrder!=null)
			{
            	parameters[9].Value = model.DispOrder;            	
            }
            	
                
			   
			if(model.OnlineTime!=null)
			{
            	parameters[10].Value = model.OnlineTime;            	
            }
            	
                
			   
			if(model.KeyDispOrder!=null)
			{
            	parameters[11].Value = model.KeyDispOrder;            	
            }
            	
                
			   
			if(model.DummyGroup!=null)
			{
            	parameters[12].Value = model.DummyGroup;            	
            }
            	
                
			   
			if(model.UnionType!=null)
			{
            	parameters[13].Value = model.UnionType;            	
            }
            	
                
			   
			if(model.SectorTypeNo!=null)
			{
            	parameters[14].Value = model.SectorTypeNo;            	
            }
            	
                
			   
			if(model.SampleRule!=null)
			{
            	parameters[15].Value = model.SampleRule;            	
            }
            	
                
				
                
				
                
			   
			if(model.StandCode!=null)
			{
            	parameters[16].Value = model.StandCode;            	
            }
            	
                
			   
			if(model.ZFStandCode!=null)
			{
            	parameters[17].Value = model.ZFStandCode;            	
            }
            	
                
			   
			if(model.UseFlag!=null)
			{
            	parameters[18].Value = model.UseFlag;            	
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
		public int Delete(string LabCode,int LabSectionNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from B_Lab_PGroup ");
			strSql.Append(" where LabCode=@LabCode and LabSectionNo=@LabSectionNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@LabCode", SqlDbType.VarChar,50),
					new SqlParameter("@LabSectionNo", SqlDbType.Int,4)};
			parameters[0].Value = LabCode;
			parameters[1].Value = LabSectionNo;


			return idb.ExecuteNonQuery(strSql.ToString(),parameters);
			
		}
		
				/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteList(string SectionIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from B_Lab_PGroup ");
			strSql.Append(" where ID in ("+SectionIDlist + ")  ");
			return idb.ExecuteNonQuery(strSql.ToString());
			
		}
				
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZhiFang.Model.Lab_PGroup GetModel(string LabCode,int LabSectionNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select SectionID, LabCode, LabSectionNo, SuperGroupNo, CName, ShortName, ShortCode, SectionDesc, SectionType, Visible, DispOrder, OnlineTime, KeyDispOrder, DummyGroup, UnionType, SectorTypeNo, SampleRule, DTimeStampe, AddTime, StandCode, ZFStandCode, UseFlag  ");			
			strSql.Append("  from B_Lab_PGroup ");
			strSql.Append(" where LabCode=@LabCode and LabSectionNo=@LabSectionNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@LabCode", SqlDbType.VarChar,50),
					new SqlParameter("@LabSectionNo", SqlDbType.Int,4)};
			parameters[0].Value = LabCode;
			parameters[1].Value = LabSectionNo;

			
			ZhiFang.Model.Lab_PGroup model=new ZhiFang.Model.Lab_PGroup();
			DataSet ds=idb.ExecuteDataSet(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																if(ds.Tables[0].Rows[0]["SectionID"].ToString()!="")
				{
					model.SectionID=int.Parse(ds.Tables[0].Rows[0]["SectionID"].ToString());
				}
																																								model.LabCode= ds.Tables[0].Rows[0]["LabCode"].ToString();
																																if(ds.Tables[0].Rows[0]["LabSectionNo"].ToString()!="")
				{
					model.LabSectionNo=int.Parse(ds.Tables[0].Rows[0]["LabSectionNo"].ToString());
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
			strSql.Append(" FROM B_Lab_PGroup ");
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
			strSql.Append(" FROM B_Lab_PGroup ");
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
		public DataSet GetList(ZhiFang.Model.Lab_PGroup model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM B_Lab_PGroup where 1=1 ");
			                                                                
                        if(model.LabCode !=null)
                        {
                        strSql.Append(" and LabCode='"+model.LabCode+"' ");
                        }
                                                    
             
            if(model.LabSectionNo !=0)
                        {
                        strSql.Append(" and LabSectionNo="+model.LabSectionNo+" ");
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
        public DataSet GetListByLike(ZhiFang.Model.Lab_PGroup model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *,'('+convert(varchar(100),LabSectionNo)+')'+CName as LabSectionNoAndName ");
            strSql.Append(" FROM B_Lab_PGroup where 1=1 ");
            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }

            if (model.CName != null)
            {
                strSql.Append(" and ( CName like '%" + model.CName + "%' ");
            }

            if (model.LabSectionNo != 0)
            {
                strSql.Append(" or LabSectionNo like '%" + model.LabSectionNo + "%' ");
            }

            if (model.ShortName != null)
            {
                strSql.Append(" or ShortName like '%" + model.ShortName + "%' ");
            }

            if (model.ShortCode != null)
            {
                strSql.Append(" or ShortCode like '%" + model.ShortCode + "%' ");
            }

            if (strSql.ToString().IndexOf("like") >= 0)
                strSql.Append(" ) ");
            return idb.ExecuteDataSet(strSql.ToString());
        }
		
		/// <summary>
		/// 获取总记录
		/// </summary>
		public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_Lab_PGroup ");
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
        public int GetTotalCount(ZhiFang.Model.Lab_PGroup model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_Lab_PGroup where 1=1 ");
           	                                                                                                                                                                                          
            if(model.DummyGroup !=null)
            {
                        strSql.Append(" and DummyGroup="+model.DummyGroup+" ");
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
        public DataSet GetListByPage(ZhiFang.Model.Lab_PGroup model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top " + nowPageSize + " * from B_Lab_PGroup where 1=1  ");
                                                
                                                                        
                  
                        if(model.LabCode !=null)
                        {
                        strSql.Append(" and LabCode='"+model.LabCode+"' ");
                        }
                                                                        if(model.LabSectionNo !=0)
            {
            	 strSql.Append(" and LabSectionNo="+model.LabSectionNo+" ");
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
                                                                        
                  
                        if(model.DummyGroup !=0)
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
                                                            strSql.Append("and SectionID not in ");
            strSql.Append("(select top " + (nowPageSize * nowPageNum) + " SectionID from B_Lab_PGroup where 1=1  ");
            strSql.Append(" order by SectionID) order by SectionID  ");
            return idb.ExecuteDataSet(strSql.ToString());
        }
        
        public bool Exists(string LabCode,int LabSectionNo)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from B_Lab_PGroup ");
			strSql.Append(" where LabCode=@LabCode and LabSectionNo=@LabSectionNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@LabCode", SqlDbType.VarChar,50),
					new SqlParameter("@LabSectionNo", SqlDbType.Int,4)};
			parameters[0].Value = LabCode;
			parameters[1].Value = LabSectionNo;


			DataSet ds=idb.ExecuteDataSet(strSql.ToString(),parameters);
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0 && ds.Tables[0].Rows[0][0].ToString().Trim()!="0" )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
		
		public int GetMaxId()
        {
            return idb.GetMaxID("LabCode,LabSectionNo","B_Lab_PGroup");
        }

        public DataSet GetList(int Top, ZhiFang.Model.Lab_PGroup model, string filedOrder)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" * ");
			strSql.Append(" FROM B_Lab_PGroup ");		
			
			                                          
            if(model.LabCode !=null)
            {
                        
            strSql.Append(" and LabCode='"+model.LabCode+"' ");
                        }
                                          
            if(model.LabSectionNo !=null)
            {
                        strSql.Append(" and LabSectionNo="+model.LabSectionNo+" ");
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



        #region IDataBase<Lab_PGroup> 成员
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
                        if (this.Exists(ds.Tables[0].Rows[i]["LabCode"].ToString().Trim(),int.Parse(ds.Tables[0].Rows[i]["LabSectionNo"].ToString().Trim())))
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
                strSql.Append("insert into B_Lab_PGroup (");
                strSql.Append("LabCode,LabSectionNo,SuperGroupNo,CName,ShortName,ShortCode,SectionDesc,SectionType,Visible,DispOrder,OnlineTime,KeyDispOrder,DummyGroup,UnionType,SectorTypeNo,SampleRule,AddTime,StandCode,ZFStandCode,UseFlag");
                strSql.Append(") values (");
                strSql.Append("'" + dr["LabCode"].ToString().Trim() + "','" + dr["LabSectionNo"].ToString().Trim() + "','" + dr["SuperGroupNo"].ToString().Trim() + "','" + dr["CName"].ToString().Trim() + "','" + dr["ShortName"].ToString().Trim() + "','" + dr["ShortCode"].ToString().Trim() + "','" + dr["SectionDesc"].ToString().Trim() + "','" + dr["SectionType"].ToString().Trim() + "','" + dr["Visible"].ToString().Trim() + "','" + dr["DispOrder"].ToString().Trim() + "','" + dr["OnlineTime"].ToString().Trim() + "','" + dr["KeyDispOrder"].ToString().Trim() + "','" + dr["DummyGroup"].ToString().Trim() + "','" + dr["UnionType"].ToString().Trim() + "','" + dr["SectorTypeNo"].ToString().Trim() + "','" + dr["SampleRule"].ToString().Trim() + "','" + DateTime.Parse(dr["AddTime"].ToString().Trim()) + "','" + dr["StandCode"].ToString().Trim() + "','" + dr["ZFStandCode"].ToString().Trim() + "','" + dr["UseFlag"].ToString().Trim() + "'");
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
                strSql.Append("update B_Lab_PGroup set ");

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
                strSql.Append(" where LabCode='" + dr["LabCode"].ToString().Trim() + "' and LabSectionNo='" + dr["LabSectionNo"].ToString().Trim() + "' ");

                return idb.ExecuteNonQuery(strSql.ToString());
            }
            catch
            {
                return 0;
            }
        }
        #endregion
    }
}

