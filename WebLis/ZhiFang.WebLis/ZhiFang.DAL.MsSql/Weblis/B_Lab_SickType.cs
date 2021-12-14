using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Weblis  
{
	//B_Lab_SickType		
	public partial class B_Lab_SickType: BaseDALLisDB, IDLab_SickType	{	
        public B_Lab_SickType(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
		public B_Lab_SickType()
		{
        }
		D_LogInfo d_log = new D_LogInfo();
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ZhiFang.Model.Lab_SickType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into B_Lab_SickType(");			
            strSql.Append("LabCode,LabSickTypeNo,CName,ShortCode,DispOrder,HisOrderCode,StandCode,ZFStandCode,UseFlag");
			strSql.Append(") values (");
            strSql.Append("@LabCode,@LabSickTypeNo,@CName,@ShortCode,@DispOrder,@HisOrderCode,@StandCode,@ZFStandCode,@UseFlag");            
            strSql.Append(") ");            
            
			SqlParameter[] parameters = {
			            new SqlParameter("@LabCode", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@LabSickTypeNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@CName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ShortCode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            
                        new SqlParameter("@HisOrderCode", SqlDbType.VarChar,21) ,            
                        new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ZFStandCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@UseFlag", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.LabCode;                        
            parameters[1].Value = model.LabSickTypeNo;                        
            parameters[2].Value = model.CName;                        
            parameters[3].Value = model.ShortCode;                        
            parameters[4].Value = model.DispOrder;                        
            parameters[5].Value = model.HisOrderCode;                        
            parameters[6].Value = model.StandCode;                        
            parameters[7].Value = model.ZFStandCode;                        
            parameters[8].Value = model.UseFlag;                  
			if (DbHelperSQL.ExecuteNonQuery(strSql.ToString(),parameters) > 0)
            {
                return d_log.OperateLog("SickType", "", "", DateTime.Now, 1);
            }
            else
                return -1;
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ZhiFang.Model.Lab_SickType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update B_Lab_SickType set ");
			                                                
            strSql.Append(" LabCode = @LabCode , ");                                    
            strSql.Append(" LabSickTypeNo = @LabSickTypeNo , ");                                    
            strSql.Append(" CName = @CName , ");                                    
            strSql.Append(" ShortCode = @ShortCode , ");                                    
            strSql.Append(" DispOrder = @DispOrder , ");                                    
            strSql.Append(" HisOrderCode = @HisOrderCode , ");                                                                                    
            strSql.Append(" StandCode = @StandCode , ");                                    
            strSql.Append(" ZFStandCode = @ZFStandCode , ");                                    
            strSql.Append(" UseFlag = @UseFlag  ");            			
			strSql.Append(" where LabCode=@LabCode and LabSickTypeNo=@LabSickTypeNo  ");
						
			SqlParameter[] parameters = {
			            	
                           
            new SqlParameter("@LabCode", SqlDbType.VarChar,100) ,            	
                           
            new SqlParameter("@LabSickTypeNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@CName", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@ShortCode", SqlDbType.VarChar,10) ,            	
                           
            new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@HisOrderCode", SqlDbType.VarChar,21) ,            	
                        	
                        	
                           
            new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@ZFStandCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@UseFlag", SqlDbType.Int,4)             	
              
            };
            			    
				
                
			   
			if(model.LabCode!=null)
			{
            	parameters[0].Value = model.LabCode;            	
            }
            	
                
			   
			if(model.LabSickTypeNo!=null)
			{
            	parameters[1].Value = model.LabSickTypeNo;            	
            }
            	
                
			   
			if(model.CName!=null)
			{
            	parameters[2].Value = model.CName;            	
            }
            	
                
			   
			if(model.ShortCode!=null)
			{
            	parameters[3].Value = model.ShortCode;            	
            }
            	
                
			   
			if(model.DispOrder!=null)
			{
            	parameters[4].Value = model.DispOrder;            	
            }
            	
                
			   
			if(model.HisOrderCode!=null)
			{
            	parameters[5].Value = model.HisOrderCode;            	
            }
            	
                
				
                
				
                
			   
			if(model.StandCode!=null)
			{
            	parameters[6].Value = model.StandCode;            	
            }
            	
                
			   
			if(model.ZFStandCode!=null)
			{
            	parameters[7].Value = model.ZFStandCode;            	
            }
            	
                
			   
			if(model.UseFlag!=null)
			{
            	parameters[8].Value = model.UseFlag;            	
            }
            	
                        
			if (DbHelperSQL.ExecuteNonQuery(strSql.ToString(),parameters) > 0)
            {
                return d_log.OperateLog("SickType", "", "", DateTime.Now, 1);
            }
            else
                return -1;
		}
		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(string LabCode,int LabSickTypeNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from B_Lab_SickType ");
			strSql.Append(" where LabCode=@LabCode and LabSickTypeNo=@LabSickTypeNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@LabCode", SqlDbType.VarChar,100),
					new SqlParameter("@LabSickTypeNo", SqlDbType.Int,4)};
			parameters[0].Value = LabCode;
			parameters[1].Value = LabSickTypeNo;


			return DbHelperSQL.ExecuteNonQuery(strSql.ToString(),parameters);
			
		}
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteList(string SickTypeIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from B_Lab_SickType ");
			strSql.Append(" where ID in ("+SickTypeIDlist + ")  ");
			return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
			
		}
				
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZhiFang.Model.Lab_SickType GetModel(string LabCode,int LabSickTypeNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select SickTypeID, LabCode, LabSickTypeNo, CName, ShortCode, DispOrder, HisOrderCode, AddTime, StandCode, ZFStandCode, UseFlag  ");			
			strSql.Append("  from B_Lab_SickType ");
			strSql.Append(" where LabCode=@LabCode and LabSickTypeNo=@LabSickTypeNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@LabCode", SqlDbType.VarChar,100),
					new SqlParameter("@LabSickTypeNo", SqlDbType.Int,4)};
			parameters[0].Value = LabCode;
			parameters[1].Value = LabSickTypeNo;

			
			ZhiFang.Model.Lab_SickType model=new ZhiFang.Model.Lab_SickType();
			DataSet ds=DbHelperSQL.ExecuteDataSet(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																
				if(ds.Tables[0].Rows[0]["SickTypeID"].ToString()!="")
				{
					model.SickTypeID=int.Parse(ds.Tables[0].Rows[0]["SickTypeID"].ToString());
				}
																																								
				model.LabCode= ds.Tables[0].Rows[0]["LabCode"].ToString();
																																
				if(ds.Tables[0].Rows[0]["LabSickTypeNo"].ToString()!="")
				{
					model.LabSickTypeNo=int.Parse(ds.Tables[0].Rows[0]["LabSickTypeNo"].ToString());
				}
																																								
				model.CName= ds.Tables[0].Rows[0]["CName"].ToString();
																																				
				model.ShortCode= ds.Tables[0].Rows[0]["ShortCode"].ToString();
																																
				if(ds.Tables[0].Rows[0]["DispOrder"].ToString()!="")
				{
					model.DispOrder=int.Parse(ds.Tables[0].Rows[0]["DispOrder"].ToString());
				}
																																								
				model.HisOrderCode= ds.Tables[0].Rows[0]["HisOrderCode"].ToString();
																																				
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
			strSql.Append(" FROM B_Lab_SickType ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.ExecuteDataSet(strSql.ToString());
		}
		
		
		/// <summary>
		/// 根据实体获取DataSet
		/// </summary>
		public DataSet GetList(ZhiFang.Model.Lab_SickType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM B_Lab_SickType where 1=1 ");
			                                                                
                        if(model.LabCode !=null)
                        {
                        strSql.Append(" and LabCode='"+model.LabCode+"' ");
                        }
                                                    
             
            if(model.LabSickTypeNo !=0)
                        {
                        strSql.Append(" and LabSickTypeNo="+model.LabSickTypeNo+" ");
                        }
                                                    
                        if(model.CName !=null)
                        {
                        strSql.Append(" and CName='"+model.CName+"' ");
                        }
                                                    
                        if(model.ShortCode !=null)
                        {
                        strSql.Append(" and ShortCode='"+model.ShortCode+"' ");
                        }
                                                    
                        if(model.DispOrder !=null)
                        {
                        strSql.Append(" and DispOrder="+model.DispOrder+" ");
                        }
                                                    
                        if(model.HisOrderCode !=null)
                        {
                        strSql.Append(" and HisOrderCode='"+model.HisOrderCode+"' ");
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
        public DataSet GetListByLike(ZhiFang.Model.Lab_SickType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  *,'('+convert(varchar(100),LabSickTypeNo)+')'+CName as LabSickTypeNoAndName  ");
            strSql.Append(" FROM B_Lab_SickType where 1=1 ");
            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }

            string strLike = "";
            if (model.SearchLikeKey != null)
            {
                strLike = " and (LabSickTypeNo like '%" + model.SearchLikeKey + "%' or CName like '%" + model.SearchLikeKey + "%' or ShortCode like '%" + model.SearchLikeKey + "%') ";
            }
            strSql.Append(strLike);

            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
		
		/// <summary>
		/// 获取总记录
		/// </summary>
		public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_Lab_SickType ");
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
        public int GetTotalCount(ZhiFang.Model.Lab_SickType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_Lab_SickType where 1=1 ");
           	if (model.LabCode != null)
            {
                strSql.Append(" and LabCode = '" + model.LabCode + "'");
            }
           	string strLike = "";
            if (model != null && model.SearchLikeKey != null)
            {
                strLike = " and (LabSickTypeNo like '%" + model.SearchLikeKey + "%' or CName like '%" + model.SearchLikeKey + "%' or ShortCode like '%" + model.SearchLikeKey + "%') ";
            }
            strSql.Append(strLike);
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
        public DataSet GetListByPage(ZhiFang.Model.Lab_SickType model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            string strLike = "";
            if (model.SearchLikeKey != null)
            {
                strLike = " and (LabSickTypeNo like '%" + model.SearchLikeKey + "%' or CName like '%" + model.SearchLikeKey + "%' or ShortCode like '%" + model.SearchLikeKey + "%') ";
            }
            strSql.Append("select top " + nowPageSize + " * from B_Lab_SickType where 1=1  ");
            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }
            strSql.Append(" " + strLike + " and SickTypeID not in ");
            strSql.Append("(select top " + (nowPageSize * nowPageNum) + " SickTypeID from B_Lab_SickType where 1=1  ");
            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }
            strSql.Append(" " + strLike + " order by " + model.OrderField + " desc ) order by " + model.OrderField + " desc ");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
        
        public bool Exists(string LabCode,int LabSickTypeNo)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from B_Lab_SickType ");
			strSql.Append(" where LabCode=@LabCode and LabSickTypeNo=@LabSickTypeNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@LabCode", SqlDbType.VarChar,100),
					new SqlParameter("@LabSickTypeNo", SqlDbType.Int,4)};
			parameters[0].Value = LabCode;
			parameters[1].Value = LabSickTypeNo;


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
            strSql.Append("select count(1) from B_Lab_SickType where 1=1 ");
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
            return DbHelperSQL.GetMaxID("LabCode,LabSickTypeNo","B_Lab_SickType");
        }

        public DataSet GetList(int Top, ZhiFang.Model.Lab_SickType model, string filedOrder)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" * ");
			strSql.Append(" FROM B_Lab_SickType ");		
			
			                                                       
                        
            if(model.LabCode !=null)
            {            
            	strSql.Append(" and LabCode='"+model.LabCode+"' ");
            }
                                            
                        
            if(model.LabSickTypeNo !=0)
            {            
            	strSql.Append(" and LabSickTypeNo='"+model.LabSickTypeNo+"' ");
            }
                                            
                        
            if(model.CName !=null)
            {            
            	strSql.Append(" and CName='"+model.CName+"' ");
            }
                                            
                        
            if(model.ShortCode !=null)
            {            
            	strSql.Append(" and ShortCode='"+model.ShortCode+"' ");
            }
                                            
                        
            if(model.DispOrder !=null)
            {            
            	strSql.Append(" and DispOrder='"+model.DispOrder+"' ");
            }
                                            
                        
            if(model.HisOrderCode !=null)
            {            
            	strSql.Append(" and HisOrderCode='"+model.HisOrderCode+"' ");
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
			            if (this.Exists(ds.Tables[0].Rows[i]["LabCode"].ToString().Trim(), int.Parse(ds.Tables[0].Rows[i]["LabSickTypeNo"].ToString().Trim())))			            
			            {
                            count += this.UpdateByDataRow(dr);
                        }
                        else
                        {
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
				strSql.Append("insert into B_Lab_SickType (");			
            	strSql.Append("LabCode,LabSickTypeNo,CName,ShortCode,DispOrder,HisOrderCode,StandCode,ZFStandCode,UseFlag");
				strSql.Append(") values (");			
            	            	            	
            	if(dr.Table.Columns["LabCode"]!=null && dr.Table.Columns["LabCode"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["LabCode"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["LabSickTypeNo"]!=null && dr.Table.Columns["LabSickTypeNo"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["LabSickTypeNo"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["CName"]!=null && dr.Table.Columns["CName"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["CName"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["ShortCode"]!=null && dr.Table.Columns["ShortCode"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["ShortCode"].ToString().Trim()+"', ");
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
            	             	            	
            	if(dr.Table.Columns["HisOrderCode"]!=null && dr.Table.Columns["HisOrderCode"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["HisOrderCode"].ToString().Trim()+"', ");
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
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Weblis.B_Lab_SickType.AddByDataRow 同步数据时异常：", ex);
                return 0;
            }          
        }
        public int UpdateByDataRow(DataRow dr)
        {
            try
            {
               StringBuilder strSql=new StringBuilder();
			   strSql.Append("update B_Lab_SickType set ");
			   			    			    			    			       
			    			    
			    if( dr.Table.Columns["CName"]!=null && dr.Table.Columns["CName"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" CName = '"+dr["CName"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["ShortCode"]!=null && dr.Table.Columns["ShortCode"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" ShortCode = '"+dr["ShortCode"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["DispOrder"]!=null && dr.Table.Columns["DispOrder"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" DispOrder = '"+dr["DispOrder"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["HisOrderCode"]!=null && dr.Table.Columns["HisOrderCode"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" HisOrderCode = '"+dr["HisOrderCode"].ToString().Trim()+"' , ");
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
				strSql.Append(" where LabCode='" + dr["LabCode"].ToString().Trim() + "' and  SickTypeID='"+ dr["SickTypeID"].ToString().Trim() +"' andand LabSickTypeNo='"+ dr["LabSickTypeNo"].ToString().Trim() +"'  ");
						
                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch(Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Weblis.B_Lab_SickType .UpdateByDataRow同步数据时异常：", ex);
                return 0;
            }
        }



        /// <summary>
        /// 根据就诊类型的名称查找对应的实验室端就诊类型的编码
        /// </summary>
        /// <param name="LabCode"></param>
        /// <param name="LabCnameList">实验室对应的名称</param>
        /// <returns>实验室端的编码</returns>
        public DataSet GetLabCodeNo(string LabCode, List<string> LabCnameList)
        {
            DataSet ds = new DataSet();
            try
            {
                string listNames = "";
                for (int i = 0; i < LabCnameList.Count; i++)
                {
                    if (listNames.Trim() == "")
                        listNames = "'" + LabCnameList[i].Trim() + "'";
                    else
                        listNames += "," + "'" + LabCnameList[i].Trim() + "'";
                }
                string strSql = "select LabSickTypeNo from B_Lab_SampleType where LabCode='" + LabCode.Trim() + "' and CName in (" + listNames + ")";
                ZhiFang.Common.Log.Log.Info("ZhiFang.DAL.MsSql.Weblis.LabSickTypeNo.GetLabCodeNo:" + strSql);
                ds = DbHelperSQL.ExecuteDataSet(strSql);
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Weblis.LabSickTypeNo.GetLabCodeNo异常->", ex);
            }
            return ds;
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
            strSql.Append(" * ");
            strSql.Append(" FROM B_Lab_SickType ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            if (filedOrder.Trim() != "")
                strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
    }

}

