using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Weblis  
{
	 	//B_Lab_BUSINESSMAN
		
	public partial class B_Lab_BUSINESSMAN: BaseDALLisDB, IDLab_BUSINESSMAN	{	
	
        public B_Lab_BUSINESSMAN(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
		public B_Lab_BUSINESSMAN()
		{
        }
		D_LogInfo d_log = new D_LogInfo();
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ZhiFang.Model.Lab_BUSINESSMAN model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into B_Lab_BUSINESSMAN(");			
            strSql.Append("CNAME,LabCode,LabBMANNO,SHORTCODE,ISUSE,IDCODE,ADDRESS,PHONENUM,ROMARK,DispOrder,StandCode,ZFStandCode,UseFlag");
			strSql.Append(") values (");
            strSql.Append("@CNAME,@LabCode,@LabBMANNO,@SHORTCODE,@ISUSE,@IDCODE,@ADDRESS,@PHONENUM,@ROMARK,@DispOrder,@StandCode,@ZFStandCode,@UseFlag");            
            strSql.Append(") ");            
            
			SqlParameter[] parameters = {
			            new SqlParameter("@CNAME", SqlDbType.VarChar,40) ,            
                        new SqlParameter("@LabCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@LabBMANNO", SqlDbType.Int,4) ,            
                        new SqlParameter("@SHORTCODE", SqlDbType.VarChar,40) ,            
                        new SqlParameter("@ISUSE", SqlDbType.Int,4) ,            
                        new SqlParameter("@IDCODE", SqlDbType.VarChar,40) ,            
                        new SqlParameter("@ADDRESS", SqlDbType.VarChar,80) ,            
                        new SqlParameter("@PHONENUM", SqlDbType.VarChar,40) ,            
                        new SqlParameter("@ROMARK", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            
                        new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ZFStandCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@UseFlag", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.CNAME;                        
            parameters[1].Value = model.LabCode;                        
            parameters[2].Value = model.LabBMANNO;                        
            parameters[3].Value = model.SHORTCODE;                        
            parameters[4].Value = model.ISUSE;                        
            parameters[5].Value = model.IDCODE;                        
            parameters[6].Value = model.ADDRESS;                        
            parameters[7].Value = model.PHONENUM;                        
            parameters[8].Value = model.ROMARK;                        
            parameters[9].Value = model.DispOrder;                        
            parameters[10].Value = model.StandCode;                        
            parameters[11].Value = model.ZFStandCode;                        
            parameters[12].Value = model.UseFlag;                  
			if (DbHelperSQL.ExecuteNonQuery(strSql.ToString(),parameters) > 0)
            {
                return d_log.OperateLog("BUSINESSMAN", "", "", DateTime.Now, 1);
            }
            else
                return -1;
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ZhiFang.Model.Lab_BUSINESSMAN model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update B_Lab_BUSINESSMAN set ");
			                                                
            strSql.Append(" CNAME = @CNAME , ");                                    
            strSql.Append(" LabCode = @LabCode , ");                                    
            strSql.Append(" LabBMANNO = @LabBMANNO , ");                                    
            strSql.Append(" SHORTCODE = @SHORTCODE , ");                                    
            strSql.Append(" ISUSE = @ISUSE , ");                                    
            strSql.Append(" IDCODE = @IDCODE , ");                                    
            strSql.Append(" ADDRESS = @ADDRESS , ");                                    
            strSql.Append(" PHONENUM = @PHONENUM , ");                                    
            strSql.Append(" ROMARK = @ROMARK , ");                                    
            strSql.Append(" DispOrder = @DispOrder , ");                                                                                    
            strSql.Append(" StandCode = @StandCode , ");                                    
            strSql.Append(" ZFStandCode = @ZFStandCode , ");                                    
            strSql.Append(" UseFlag = @UseFlag  ");            			
			strSql.Append(" where LabCode=@LabCode and LabBMANNO=@LabBMANNO  ");
						
			SqlParameter[] parameters = {
			            	
                           
            new SqlParameter("@CNAME", SqlDbType.VarChar,40) ,            	
                           
            new SqlParameter("@LabCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@LabBMANNO", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@SHORTCODE", SqlDbType.VarChar,40) ,            	
                           
            new SqlParameter("@ISUSE", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@IDCODE", SqlDbType.VarChar,40) ,            	
                           
            new SqlParameter("@ADDRESS", SqlDbType.VarChar,80) ,            	
                           
            new SqlParameter("@PHONENUM", SqlDbType.VarChar,40) ,            	
                           
            new SqlParameter("@ROMARK", SqlDbType.VarChar,200) ,            	
                           
            new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            	
                        	
                        	
                           
            new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@ZFStandCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@UseFlag", SqlDbType.Int,4)             	
              
            };
            			    
				
                
			   
			if(model.CNAME!=null)
			{
            	parameters[0].Value = model.CNAME;            	
            }
            	
                
			   
			if(model.LabCode!=null)
			{
            	parameters[1].Value = model.LabCode;            	
            }
            	
                
			   
			if(model.LabBMANNO!=null)
			{
            	parameters[2].Value = model.LabBMANNO;            	
            }
            	
                
			   
			if(model.SHORTCODE!=null)
			{
            	parameters[3].Value = model.SHORTCODE;            	
            }
            	
                
			   
			if(model.ISUSE!=null)
			{
            	parameters[4].Value = model.ISUSE;            	
            }
            	
                
			   
			if(model.IDCODE!=null)
			{
            	parameters[5].Value = model.IDCODE;            	
            }
            	
                
			   
			if(model.ADDRESS!=null)
			{
            	parameters[6].Value = model.ADDRESS;            	
            }
            	
                
			   
			if(model.PHONENUM!=null)
			{
            	parameters[7].Value = model.PHONENUM;            	
            }
            	
                
			   
			if(model.ROMARK!=null)
			{
            	parameters[8].Value = model.ROMARK;            	
            }
            	
                
			   
			if(model.DispOrder!=null)
			{
            	parameters[9].Value = model.DispOrder;            	
            }
            	
                
				
                
				
                
			   
			if(model.StandCode!=null)
			{
            	parameters[10].Value = model.StandCode;            	
            }
            	
                
			   
			if(model.ZFStandCode!=null)
			{
            	parameters[11].Value = model.ZFStandCode;            	
            }
            	
                
			   
			if(model.UseFlag!=null)
			{
            	parameters[12].Value = model.UseFlag;            	
            }
            	
                        
			if (DbHelperSQL.ExecuteNonQuery(strSql.ToString(),parameters) > 0)
            {
                return d_log.OperateLog("BUSINESSMAN", "", "", DateTime.Now, 1);
            }
            else
                return -1;
		}
		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(string LabCode,int LabBMANNO)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from B_Lab_BUSINESSMAN ");
			strSql.Append(" where LabCode=@LabCode and LabBMANNO=@LabBMANNO ");
						SqlParameter[] parameters = {
					new SqlParameter("@LabCode", SqlDbType.VarChar,50),
					new SqlParameter("@LabBMANNO", SqlDbType.Int,4)};
			parameters[0].Value = LabCode;
			parameters[1].Value = LabBMANNO;


			return DbHelperSQL.ExecuteNonQuery(strSql.ToString(),parameters);
			
		}
		
				/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteList(string BNANIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from B_Lab_BUSINESSMAN ");
			strSql.Append(" where ID in ("+BNANIDlist + ")  ");
			return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
			
		}
				
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZhiFang.Model.Lab_BUSINESSMAN GetModel(string LabCode,int LabBMANNO)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select BNANID, CNAME, LabCode, LabBMANNO, SHORTCODE, ISUSE, IDCODE, ADDRESS, PHONENUM, ROMARK, DispOrder, AddTime, StandCode, ZFStandCode, UseFlag  ");			
			strSql.Append("  from B_Lab_BUSINESSMAN ");
			strSql.Append(" where LabCode=@LabCode and LabBMANNO=@LabBMANNO ");
						SqlParameter[] parameters = {
					new SqlParameter("@LabCode", SqlDbType.VarChar,50),
					new SqlParameter("@LabBMANNO", SqlDbType.Int,4)};
			parameters[0].Value = LabCode;
			parameters[1].Value = LabBMANNO;

			
			ZhiFang.Model.Lab_BUSINESSMAN model=new ZhiFang.Model.Lab_BUSINESSMAN();
			DataSet ds=DbHelperSQL.ExecuteDataSet(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																
				if(ds.Tables[0].Rows[0]["BNANID"].ToString()!="")
				{
					model.BNANID=int.Parse(ds.Tables[0].Rows[0]["BNANID"].ToString());
				}
																																								
				model.CNAME= ds.Tables[0].Rows[0]["CNAME"].ToString();
																																				
				model.LabCode= ds.Tables[0].Rows[0]["LabCode"].ToString();
																																
				if(ds.Tables[0].Rows[0]["LabBMANNO"].ToString()!="")
				{
					model.LabBMANNO=int.Parse(ds.Tables[0].Rows[0]["LabBMANNO"].ToString());
				}
																																								
				model.SHORTCODE= ds.Tables[0].Rows[0]["SHORTCODE"].ToString();
																																
				if(ds.Tables[0].Rows[0]["ISUSE"].ToString()!="")
				{
					model.ISUSE=int.Parse(ds.Tables[0].Rows[0]["ISUSE"].ToString());
				}
																																								
				model.IDCODE= ds.Tables[0].Rows[0]["IDCODE"].ToString();
																																				
				model.ADDRESS= ds.Tables[0].Rows[0]["ADDRESS"].ToString();
																																				
				model.PHONENUM= ds.Tables[0].Rows[0]["PHONENUM"].ToString();
																																				
				model.ROMARK= ds.Tables[0].Rows[0]["ROMARK"].ToString();
																																
				if(ds.Tables[0].Rows[0]["DispOrder"].ToString()!="")
				{
					model.DispOrder=int.Parse(ds.Tables[0].Rows[0]["DispOrder"].ToString());
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
			strSql.Append(" FROM B_Lab_BUSINESSMAN ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.ExecuteDataSet(strSql.ToString());
		}
		
		
		/// <summary>
		/// 根据实体获取DataSet
		/// </summary>
		public DataSet GetList(ZhiFang.Model.Lab_BUSINESSMAN model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM B_Lab_BUSINESSMAN where 1=1 ");
			                                                                
                        if(model.CNAME !=null)
                        {
                        strSql.Append(" and CNAME='"+model.CNAME+"' ");
                        }
                                                    
                        if(model.LabCode !=null)
                        {
                        strSql.Append(" and LabCode='"+model.LabCode+"' ");
                        }
                                                    
             
            if(model.LabBMANNO !=0)
                        {
                        strSql.Append(" and LabBMANNO="+model.LabBMANNO+" ");
                        }
                                                    
                        if(model.SHORTCODE !=null)
                        {
                        strSql.Append(" and SHORTCODE='"+model.SHORTCODE+"' ");
                        }
                                                    
                        if(model.ISUSE !=null)
                        {
                        strSql.Append(" and ISUSE="+model.ISUSE+" ");
                        }
                                                    
                        if(model.IDCODE !=null)
                        {
                        strSql.Append(" and IDCODE='"+model.IDCODE+"' ");
                        }
                                                    
                        if(model.ADDRESS !=null)
                        {
                        strSql.Append(" and ADDRESS='"+model.ADDRESS+"' ");
                        }
                                                    
                        if(model.PHONENUM !=null)
                        {
                        strSql.Append(" and PHONENUM='"+model.PHONENUM+"' ");
                        }
                                                    
                        if(model.ROMARK !=null)
                        {
                        strSql.Append(" and ROMARK='"+model.ROMARK+"' ");
                        }
                                                    
                        if(model.DispOrder !=null)
                        {
                        strSql.Append(" and DispOrder="+model.DispOrder+" ");
                        }
                                                                            
                        if(model.StandCode !=null)
                        {
                        strSql.Append(" and StandCode='"+model.StandCode+"' ");
                        }
                                                    
                        if(model.ZFStandCode !=null)
                        {
                        strSql.Append(" and ZFStandCode='"+model.ZFStandCode+"' ");
                        }
                                                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
		}
		public DataSet GetListByLike(ZhiFang.Model.Lab_BUSINESSMAN model)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select  *,'('+convert(varchar(100),LabBMANNO)+')'+CNAME as LabBMANNOAndName ");
			strSql.Append(" FROM B_Lab_BUSINESSMAN where 1=1 ");
			if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }
			                                          
            if(model.CNAME !=null)
            {
                        strSql.Append(" or CNAME like '%"+model.CNAME+"%' ");
                        }
                                                                  
            if(model.LabBMANNO !=0)
            {
                        strSql.Append(" or LabBMANNO like '%"+model.LabBMANNO+"%' ");
                        }
                                          
            if(model.SHORTCODE !=null)
            {
                        strSql.Append(" or SHORTCODE like '%"+model.SHORTCODE+"%' ");
                        }
                                          
            if(model.ISUSE !=null)
            {
                        strSql.Append(" or ISUSE like '%"+model.ISUSE+"%' ");
                        }
                                          
            if(model.IDCODE !=null)
            {
                        strSql.Append(" or IDCODE like '%"+model.IDCODE+"%' ");
                        }
                                          
            if(model.ADDRESS !=null)
            {
                        strSql.Append(" or ADDRESS like '%"+model.ADDRESS+"%' ");
                        }
                                          
            if(model.PHONENUM !=null)
            {
                        strSql.Append(" or PHONENUM like '%"+model.PHONENUM+"%' ");
                        }
                                          
            if(model.ROMARK !=null)
            {
                        strSql.Append(" or ROMARK like '%"+model.ROMARK+"%' ");
                        }
                                          
            if(model.DispOrder !=null)
            {
                        strSql.Append(" or DispOrder like '%"+model.DispOrder+"%' ");
                        }
                                                                  
            if(model.AddTime !=null)
            {
                        strSql.Append(" or AddTime like '%"+model.AddTime+"%' ");
                        }
                                          
            if(model.StandCode !=null)
            {
                        strSql.Append(" or StandCode like '%"+model.StandCode+"%' ");
                        }
                                          
            if(model.ZFStandCode !=null)
            {
                        strSql.Append(" or ZFStandCode like '%"+model.ZFStandCode+"%' ");
                        }
                                          
            if(model.UseFlag !=null)
            {
                        strSql.Append(" or UseFlag like '%"+model.UseFlag+"%' ");
                        }
                                    return DbHelperSQL.ExecuteDataSet(strSql.ToString());
		}
		
		/// <summary>
		/// 获取总记录
		/// </summary>
		public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_Lab_BUSINESSMAN ");
            string strCount = DbHelperSQL.ExecuteScalar(strSql.ToString());
            if (strCount != null && strCount.Trim() != "")
            {
                return Convert.ToInt32(strCount.Trim());
            }
            else
            {
                return 0;
            }
        }
        public int GetTotalCount(ZhiFang.Model.Lab_BUSINESSMAN model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_Lab_BUSINESSMAN where 1=1 ");
           	                                                  
                        
            if(model.CNAME !=null)
            {            
            	strSql.Append(" and CNAME='"+model.CNAME+"' ");
            }
                                        
                        
            if(model.LabCode !=null)
            {            
            	strSql.Append(" and LabCode='"+model.LabCode+"' ");
            }
                                        
                        
            if(model.LabBMANNO !=0)
            {            
            	strSql.Append(" and LabBMANNO='"+model.LabBMANNO+"' ");
            }
                                        
                        
            if(model.SHORTCODE !=null)
            {            
            	strSql.Append(" and SHORTCODE='"+model.SHORTCODE+"' ");
            }
                                        
                        
            if(model.ISUSE !=null)
            {            
            	strSql.Append(" and ISUSE='"+model.ISUSE+"' ");
            }
                                        
                        
            if(model.IDCODE !=null)
            {            
            	strSql.Append(" and IDCODE='"+model.IDCODE+"' ");
            }
                                        
                        
            if(model.ADDRESS !=null)
            {            
            	strSql.Append(" and ADDRESS='"+model.ADDRESS+"' ");
            }
                                        
                        
            if(model.PHONENUM !=null)
            {            
            	strSql.Append(" and PHONENUM='"+model.PHONENUM+"' ");
            }
                                        
                        
            if(model.ROMARK !=null)
            {            
            	strSql.Append(" and ROMARK='"+model.ROMARK+"' ");
            }
                                        
                        
            if(model.DispOrder !=null)
            {            
            	strSql.Append(" and DispOrder='"+model.DispOrder+"' ");
            }
                                                                
                        
            if(model.StandCode !=null)
            {            
            	strSql.Append(" and StandCode='"+model.StandCode+"' ");
            }
                                        
                        
            if(model.ZFStandCode !=null)
            {            
            	strSql.Append(" and ZFStandCode='"+model.ZFStandCode+"' ");
            }
                                       
            string strCount = DbHelperSQL.ExecuteScalar(strSql.ToString());
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
        public DataSet GetListByPage(ZhiFang.Model.Lab_BUSINESSMAN model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top " + nowPageSize + " * from B_Lab_BUSINESSMAN where 1=1  ");
                                                                              
                        if(model.CNAME !=null)
                        {
                        strSql.Append(" and CNAME='"+model.CNAME+"' ");
                        }
                                                      
                        if(model.LabCode !=null)
                        {
                        strSql.Append(" and LabCode='"+model.LabCode+"' ");
                        }
                                                      
             
            if(model.LabBMANNO !=0)
                        {
                        strSql.Append(" and LabBMANNO="+model.LabBMANNO+" ");
                        }
                                                      
                        if(model.SHORTCODE !=null)
                        {
                        strSql.Append(" and SHORTCODE='"+model.SHORTCODE+"' ");
                        }
                                                      
                        if(model.ISUSE !=null)
                        {
                        strSql.Append(" and ISUSE="+model.ISUSE+" ");
                        }
                                                      
                        if(model.IDCODE !=null)
                        {
                        strSql.Append(" and IDCODE='"+model.IDCODE+"' ");
                        }
                                                      
                        if(model.ADDRESS !=null)
                        {
                        strSql.Append(" and ADDRESS='"+model.ADDRESS+"' ");
                        }
                                                      
                        if(model.PHONENUM !=null)
                        {
                        strSql.Append(" and PHONENUM='"+model.PHONENUM+"' ");
                        }
                                                      
                        if(model.ROMARK !=null)
                        {
                        strSql.Append(" and ROMARK='"+model.ROMARK+"' ");
                        }
                                                      
                        if(model.DispOrder !=null)
                        {
                        strSql.Append(" and DispOrder="+model.DispOrder+" ");
                        }
                                                                              
                        if(model.StandCode !=null)
                        {
                        strSql.Append(" and StandCode='"+model.StandCode+"' ");
                        }
                                                      
                        if(model.ZFStandCode !=null)
                        {
                        strSql.Append(" and ZFStandCode='"+model.ZFStandCode+"' ");
                        }
                                                strSql.Append("and BNANID not in ");
            strSql.Append("(select top " + (nowPageSize * nowPageNum ) + " BNANID from B_Lab_BUSINESSMAN where 1=1  ");
                                                                              
                        if(model.CNAME !=null)
                        {
                        strSql.Append(" and CNAME='"+model.CNAME+"' ");
                        }
                                                      
                        if(model.LabCode !=null)
                        {
                        strSql.Append(" and LabCode='"+model.LabCode+"' ");
                        }
                                                      
             
            if(model.LabBMANNO !=0)
                        {
                        strSql.Append(" and LabBMANNO="+model.LabBMANNO+" ");
                        }
                                                      
                        if(model.SHORTCODE !=null)
                        {
                        strSql.Append(" and SHORTCODE='"+model.SHORTCODE+"' ");
                        }
                                                      
                        if(model.ISUSE !=null)
                        {
                        strSql.Append(" and ISUSE="+model.ISUSE+" ");
                        }
                                                      
                        if(model.IDCODE !=null)
                        {
                        strSql.Append(" and IDCODE='"+model.IDCODE+"' ");
                        }
                                                      
                        if(model.ADDRESS !=null)
                        {
                        strSql.Append(" and ADDRESS='"+model.ADDRESS+"' ");
                        }
                                                      
                        if(model.PHONENUM !=null)
                        {
                        strSql.Append(" and PHONENUM='"+model.PHONENUM+"' ");
                        }
                                                      
                        if(model.ROMARK !=null)
                        {
                        strSql.Append(" and ROMARK='"+model.ROMARK+"' ");
                        }
                                                      
                        if(model.DispOrder !=null)
                        {
                        strSql.Append(" and DispOrder="+model.DispOrder+" ");
                        }
                                                                              
                        if(model.StandCode !=null)
                        {
                        strSql.Append(" and StandCode='"+model.StandCode+"' ");
                        }
                                                      
                        if(model.ZFStandCode !=null)
                        {
                        strSql.Append(" and ZFStandCode='"+model.ZFStandCode+"' ");
                        }
                                                strSql.Append(" order by BNANID desc ) order by BNANID desc ");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
        
        public bool Exists(string LabCode,int LabBMANNO)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from B_Lab_BUSINESSMAN ");
			strSql.Append(" where LabCode=@LabCode and LabBMANNO=@LabBMANNO ");
						SqlParameter[] parameters = {
					new SqlParameter("@LabCode", SqlDbType.VarChar,50),
					new SqlParameter("@LabBMANNO", SqlDbType.Int,4)};
			parameters[0].Value = LabCode;
			parameters[1].Value = LabBMANNO;


			DataSet ds=DbHelperSQL.ExecuteDataSet(strSql.ToString(),parameters);
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0 && ds.Tables[0].Rows[0][0].ToString().Trim()!="0" )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Exists(System.Collections.Hashtable ht)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from B_Lab_BUSINESSMAN where 1=1 ");
            if (ht.Count > 0)
            {
                foreach (System.Collections.DictionaryEntry item in ht)
                {
                    strSql.Append(" and " + item.Key.ToString().Trim() + "='" + item.Value + "' ");
                }
                string strCount = DbHelperSQL.ExecuteScalar(strSql.ToString());
                if (strCount != null && strCount.Trim() != "" && strCount.Trim() != "0")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

		public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("LabCode,LabBMANNO","B_Lab_BUSINESSMAN");
        }

        public DataSet GetList(int Top, ZhiFang.Model.Lab_BUSINESSMAN model, string filedOrder)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" * ");
			strSql.Append(" FROM B_Lab_BUSINESSMAN ");		
			
			                                                       
                        
            if(model.CNAME !=null)
            {            
            	strSql.Append(" and CNAME='"+model.CNAME+"' ");
            }
                                            
                        
            if(model.LabCode !=null)
            {            
            	strSql.Append(" and LabCode='"+model.LabCode+"' ");
            }
                                            
                        
            if(model.LabBMANNO !=0)
            {            
            	strSql.Append(" and LabBMANNO='"+model.LabBMANNO+"' ");
            }
                                            
                        
            if(model.SHORTCODE !=null)
            {            
            	strSql.Append(" and SHORTCODE='"+model.SHORTCODE+"' ");
            }
                                            
                        
            if(model.ISUSE !=null)
            {            
            	strSql.Append(" and ISUSE='"+model.ISUSE+"' ");
            }
                                            
                        
            if(model.IDCODE !=null)
            {            
            	strSql.Append(" and IDCODE='"+model.IDCODE+"' ");
            }
                                            
                        
            if(model.ADDRESS !=null)
            {            
            	strSql.Append(" and ADDRESS='"+model.ADDRESS+"' ");
            }
                                            
                        
            if(model.PHONENUM !=null)
            {            
            	strSql.Append(" and PHONENUM='"+model.PHONENUM+"' ");
            }
                                            
                        
            if(model.ROMARK !=null)
            {            
            	strSql.Append(" and ROMARK='"+model.ROMARK+"' ");
            }
                                            
                        
            if(model.DispOrder !=null)
            {            
            	strSql.Append(" and DispOrder='"+model.DispOrder+"' ");
            }
                                                                    
                        
            if(model.StandCode !=null)
            {            
            	strSql.Append(" and StandCode='"+model.StandCode+"' ");
            }
                                            
                        
            if(model.ZFStandCode !=null)
            {            
            	strSql.Append(" and ZFStandCode='"+model.ZFStandCode+"' ");
            }
                                      
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetAllList()
        {
            return GetList("");
        }
        
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
                        if (this.Exists(ds.Tables[0].Rows[i]["LabCode"].ToString().Trim(), int.Parse(ds.Tables[0].Rows[i]["LabBMANNO"].ToString().Trim())))
                        {
                            System.Threading.Thread.Sleep(ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ThreadDicSynchInterval"));
                            count += this.UpdateByDataRow(dr);
                        }
                        else
                        {
                            System.Threading.Thread.Sleep(ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ThreadDicSynchInterval"));
                            count += this.AddByDataRow(dr);
                        }
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
				strSql.Append("insert into B_Lab_BUSINESSMAN (");			
            	strSql.Append("CNAME,LabCode,LabBMANNO,SHORTCODE,ISUSE,IDCODE,ADDRESS,PHONENUM,ROMARK,DispOrder,StandCode,ZFStandCode,UseFlag");
				strSql.Append(") values (");			
            	            	            	
            	if(dr.Table.Columns["CNAME"]!=null && dr.Table.Columns["CNAME"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["CNAME"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["LabCode"]!=null && dr.Table.Columns["LabCode"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["LabCode"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["LabBMANNO"]!=null && dr.Table.Columns["LabBMANNO"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["LabBMANNO"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["SHORTCODE"]!=null && dr.Table.Columns["SHORTCODE"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["SHORTCODE"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["ISUSE"]!=null && dr.Table.Columns["ISUSE"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["ISUSE"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["IDCODE"]!=null && dr.Table.Columns["IDCODE"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["IDCODE"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["ADDRESS"]!=null && dr.Table.Columns["ADDRESS"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["ADDRESS"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["PHONENUM"]!=null && dr.Table.Columns["PHONENUM"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["PHONENUM"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["ROMARK"]!=null && dr.Table.Columns["ROMARK"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["ROMARK"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["DispOrder"]!=null && dr.Table.Columns["DispOrder"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["DispOrder"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["StandCode"]!=null && dr.Table.Columns["StandCode"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["StandCode"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["ZFStandCode"]!=null && dr.Table.Columns["ZFStandCode"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["ZFStandCode"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["UseFlag"]!=null && dr.Table.Columns["UseFlag"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["UseFlag"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	
            	int n = strSql.ToString().LastIndexOf(",");
				strSql.Remove(n, 1);
            	strSql.Append(") ");  
                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch(Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Weblis.B_Lab_BUSINESSMAN.AddByDataRow 同步数据时异常：", ex);
                return 0;
            }          
        }
        public int UpdateByDataRow(DataRow dr)
        {
            try
            {
               StringBuilder strSql=new StringBuilder();
			   strSql.Append("update B_Lab_BUSINESSMAN set ");
			   			    			       
			    			    
			    if( dr.Table.Columns["CNAME"]!=null && dr.Table.Columns["CNAME"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" CNAME = '"+dr["CNAME"].ToString().Trim()+"' , ");
			    }
			      			    			    			       
			    			    
			    if( dr.Table.Columns["SHORTCODE"]!=null && dr.Table.Columns["SHORTCODE"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" SHORTCODE = '"+dr["SHORTCODE"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["ISUSE"]!=null && dr.Table.Columns["ISUSE"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" ISUSE = '"+dr["ISUSE"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["IDCODE"]!=null && dr.Table.Columns["IDCODE"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" IDCODE = '"+dr["IDCODE"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["ADDRESS"]!=null && dr.Table.Columns["ADDRESS"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" ADDRESS = '"+dr["ADDRESS"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["PHONENUM"]!=null && dr.Table.Columns["PHONENUM"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" PHONENUM = '"+dr["PHONENUM"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["ROMARK"]!=null && dr.Table.Columns["ROMARK"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" ROMARK = '"+dr["ROMARK"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["DispOrder"]!=null && dr.Table.Columns["DispOrder"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" DispOrder = '"+dr["DispOrder"].ToString().Trim()+"' , ");
			    }
			      			    			    			       
			    			    
			    if( dr.Table.Columns["StandCode"]!=null && dr.Table.Columns["StandCode"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" StandCode = '"+dr["StandCode"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["ZFStandCode"]!=null && dr.Table.Columns["ZFStandCode"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" ZFStandCode = '"+dr["ZFStandCode"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["UseFlag"]!=null && dr.Table.Columns["UseFlag"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" UseFlag = '"+dr["UseFlag"].ToString().Trim()+"' , ");
			    }
			      		
			    
			    int n = strSql.ToString().LastIndexOf(",");
				strSql.Remove(n, 1);
				strSql.Append(" where LabCode='" + dr["LabCode"].ToString().Trim() + "' and LabBMANNO='"+dr["LabBMANNO"].ToString().Trim()+"' ");
						
                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch(Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Weblis.B_Lab_BUSINESSMAN .UpdateByDataRow同步数据时异常：", ex);
                return 0;
            }
        }
		
   
	}
}

