using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Digitlab8  
{
	 	//B_CLIENTELEControl
		
	public partial class B_CLIENTELEControl: IDCLIENTELEControl	{	
		DBUtility.IDBConnection idb;
        public B_CLIENTELEControl(string dbsourceconn)
        {
            idb = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
		public B_CLIENTELEControl()
		{
            idb = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.LisDB());
        }
		D_LogInfo d_log = new D_LogInfo();
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ZhiFang.Model.CLIENTELEControl model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into B_CLIENTELEControl(");			
            strSql.Append("ClIENTControlNo,ClIENTNO,ControlLabNo,ControlClIENTNO,UseFlag");
			strSql.Append(") values (");
            strSql.Append("@ClIENTControlNo,@ClIENTNO,@ControlLabNo,@ControlClIENTNO,@UseFlag");            
            strSql.Append(") ");            
            
			SqlParameter[] parameters = {
			            new SqlParameter("@ClIENTControlNo", SqlDbType.Char,50) ,            
                        new SqlParameter("@ClIENTNO", SqlDbType.Int,4) ,            
                        new SqlParameter("@ControlLabNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ControlClIENTNO", SqlDbType.Int,4) ,            
                        new SqlParameter("@UseFlag", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.ClIENTControlNo;                        
            parameters[1].Value = model.ClIENTNO;                        
            parameters[2].Value = model.ControlLabNo;                        
            parameters[3].Value = model.ControlClIENTNO;                        
            parameters[4].Value = model.UseFlag;                  
			if (idb.ExecuteNonQuery(strSql.ToString(),parameters) > 0)
            {
                return d_log.OperateLog("CLIENTELE", "", "", DateTime.Now, 1);
            }
            else
                return -1;
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ZhiFang.Model.CLIENTELEControl model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update B_CLIENTELEControl set ");
			                                                
            strSql.Append(" ClIENTControlNo = @ClIENTControlNo , ");                                    
            strSql.Append(" ClIENTNO = @ClIENTNO , ");                                    
            strSql.Append(" ControlLabNo = @ControlLabNo , ");                                    
            strSql.Append(" ControlClIENTNO = @ControlClIENTNO , ");                                                                                    
            strSql.Append(" UseFlag = @UseFlag  ");            			
			strSql.Append(" where ClIENTControlNo=@ClIENTControlNo  ");
						
			SqlParameter[] parameters = {
			            	
                           
            new SqlParameter("@ClIENTControlNo", SqlDbType.Char,50) ,            	
                           
            new SqlParameter("@ClIENTNO", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@ControlLabNo", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@ControlClIENTNO", SqlDbType.Int,4) ,            	
                        	
                        	
                           
            new SqlParameter("@UseFlag", SqlDbType.Int,4)             	
              
            };
            			    
				
                
			   
			if(model.ClIENTControlNo!=null)
			{
            	parameters[0].Value = model.ClIENTControlNo;            	
            }
            	
                
			   
			if(model.ClIENTNO!=null)
			{
            	parameters[1].Value = model.ClIENTNO;            	
            }
            	
                
			   
			if(model.ControlLabNo!=null)
			{
            	parameters[2].Value = model.ControlLabNo;            	
            }
            	
                
			   
			if(model.ControlClIENTNO!=null)
			{
            	parameters[3].Value = model.ControlClIENTNO;            	
            }
            	
                
				
                
				
                
			   
			if(model.UseFlag!=null)
			{
            	parameters[4].Value = model.UseFlag;            	
            }
            	
                        
			if (idb.ExecuteNonQuery(strSql.ToString(),parameters) > 0)
            {
                return d_log.OperateLog("CLIENTELE", "", "", DateTime.Now, 1);
            }
            else
                return -1;
		}
		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(string ClIENTControlNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from B_CLIENTELEControl ");
			strSql.Append(" where ClIENTControlNo=@ClIENTControlNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@ClIENTControlNo", SqlDbType.Char,50)};
			parameters[0].Value = ClIENTControlNo;


			return idb.ExecuteNonQuery(strSql.ToString(),parameters);
			
		}
		
				/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteList(string Idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from B_CLIENTELEControl ");
			strSql.Append(" where ID in ("+Idlist + ")  ");
			return idb.ExecuteNonQuery(strSql.ToString());
			
		}
				
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZhiFang.Model.CLIENTELEControl GetModel(string ClIENTControlNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Id, ClIENTControlNo, ClIENTNO, ControlLabNo, ControlClIENTNO, DTimeStampe, AddTime, UseFlag  ");			
			strSql.Append("  from B_CLIENTELEControl ");
			strSql.Append(" where ClIENTControlNo=@ClIENTControlNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@ClIENTControlNo", SqlDbType.Char,50)};
			parameters[0].Value = ClIENTControlNo;

			
			ZhiFang.Model.CLIENTELEControl model=new ZhiFang.Model.CLIENTELEControl();
			DataSet ds=idb.ExecuteDataSet(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["Id"].ToString()!="")
				{
					model.Id=int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
				}
																																				model.ClIENTControlNo= ds.Tables[0].Rows[0]["ClIENTControlNo"].ToString();
																												if(ds.Tables[0].Rows[0]["ClIENTNO"].ToString()!="")
				{
					model.ClIENTNO=int.Parse(ds.Tables[0].Rows[0]["ClIENTNO"].ToString());
				}
																																				model.ControlLabNo= ds.Tables[0].Rows[0]["ControlLabNo"].ToString();
																												if(ds.Tables[0].Rows[0]["ControlClIENTNO"].ToString()!="")
				{
					model.ControlClIENTNO=int.Parse(ds.Tables[0].Rows[0]["ControlClIENTNO"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["DTimeStampe"].ToString()!="")
				{
					model.DTimeStampe=DateTime.Parse(ds.Tables[0].Rows[0]["DTimeStampe"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["AddTime"].ToString()!="")
				{
					model.AddTime=DateTime.Parse(ds.Tables[0].Rows[0]["AddTime"].ToString());
				}
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
			strSql.Append(" FROM B_CLIENTELEControl ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return idb.ExecuteDataSet(strSql.ToString());
		}
		
		
		
		/// <summary>
		/// 根据实体获取DataSet
		/// </summary>
        public DataSet GetList(ZhiFang.Model.CLIENTELEControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM B_CLIENTELEControl where 1=1 ");

            if (model.ClIENTControlNo != null)
            {
                strSql.Append(" and ClIENTControlNo='" + model.ClIENTControlNo + "' ");
            }

            if (model.ClIENTNO != 0)
            {
                strSql.Append(" and ClIENTNO=" + model.ClIENTNO + " ");
            }

            if (model.ControlLabNo != null)
            {
                strSql.Append(" and ControlLabNo='" + model.ControlLabNo + "' ");
            }

            if (model.ControlClIENTNO != 0)
            {
                strSql.Append(" and ControlClIENTNO=" + model.ControlClIENTNO + " ");
            }

            return idb.ExecuteDataSet(strSql.ToString());
        }
		
		/// <summary>
		/// 获取总记录
		/// </summary>
		public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_CLIENTELEControl ");
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
        public int GetTotalCount(ZhiFang.Model.CLIENTELEControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_CLIENTELEControl where 1=1 ");
           	                                          
            if(model.ClIENTControlNo !=null)
            {
                        strSql.Append(" and ClIENTControlNo='"+model.ClIENTControlNo+"' ");
                        }
                                          
            if(model.ClIENTNO !=null)
            {
                        strSql.Append(" and ClIENTNO="+model.ClIENTNO+" ");
                        }
                                          
            if(model.ControlLabNo !=null)
            {
                        strSql.Append(" and ControlLabNo='"+model.ControlLabNo+"' ");
                        }
                                          
            if(model.ControlClIENTNO !=null)
            {
                        strSql.Append(" and ControlClIENTNO="+model.ControlClIENTNO+" ");
                        }
                                          
            if(model.DTimeStampe !=null)
            {
                        strSql.Append(" and DTimeStampe='"+model.DTimeStampe+"' ");
                        }
                                          
            if(model.AddTime !=null)
            {
                        strSql.Append(" and AddTime='"+model.AddTime+"' ");
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
        
        
        public bool Exists(string ClIENTControlNo)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from B_CLIENTELEControl ");
			strSql.Append(" where ClIENTControlNo ='"+ClIENTControlNo+"'");
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

        public bool Exists(System.Collections.Hashtable ht)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from B_CLIENTELEControl where 1=1 ");
            if (ht.Count > 0)
            {
                foreach (System.Collections.DictionaryEntry item in ht)
                {
                    strSql.Append(" and " + item.Key.ToString().Trim() + "='" + item.Value + "' ");
                }
                string strCount = idb.ExecuteScalar(strSql.ToString());
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
            return idb.GetMaxID("ClIENTControlNo","B_CLIENTELEControl");
        }

        public DataSet GetList(int Top, ZhiFang.Model.CLIENTELEControl model, string filedOrder)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" * ");
			strSql.Append(" FROM B_CLIENTELEControl ");		
			
			                                          
            if(model.ClIENTControlNo !=null)
            {
                        
            strSql.Append(" and ClIENTControlNo='"+model.ClIENTControlNo+"' ");
                        }
                                          
            if(model.ClIENTNO !=null)
            {
                        strSql.Append(" and ClIENTNO="+model.ClIENTNO+" ");
                        }
                                          
            if(model.ControlLabNo !=null)
            {
                        
            strSql.Append(" and ControlLabNo='"+model.ControlLabNo+"' ");
                        }
                                          
            if(model.ControlClIENTNO !=null)
            {
                        strSql.Append(" and ControlClIENTNO="+model.ControlClIENTNO+" ");
                        }
                                          
            if(model.DTimeStampe !=null)
            {
                        
            strSql.Append(" and DTimeStampe='"+model.DTimeStampe+"' ");
                        }
                                          
            if(model.AddTime !=null)
            {
                        
            strSql.Append(" and AddTime='"+model.AddTime+"' ");
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
			            if (this.Exists(ds.Tables[0].Rows[i]["ClIENTControlNo"].ToString().Trim()))
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
				strSql.Append("insert into B_CLIENTELEControl (");			
            	strSql.Append("ClIENTControlNo,ClIENTNO,ControlLabNo,ControlClIENTNO,UseFlag");
				strSql.Append(") values (");			
            	            	            	
            	if(dr.Table.Columns["ClIENTControlNo"]!=null && dr.Table.Columns["ClIENTControlNo"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["ClIENTControlNo"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["ClIENTNO"]!=null && dr.Table.Columns["ClIENTNO"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["ClIENTNO"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["ControlLabNo"]!=null && dr.Table.Columns["ControlLabNo"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["ControlLabNo"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["ControlClIENTNO"]!=null && dr.Table.Columns["ControlClIENTNO"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["ControlClIENTNO"].ToString().Trim()+"', ");
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
                return idb.ExecuteNonQuery(strSql.ToString());
            }
            catch(Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Digitlab8.B_CLIENTELEControl.AddByDataRow 同步数据时异常：", ex);
                return 0;
            }          
        }
        public int UpdateByDataRow(DataRow dr)
        {
            try
            {
               StringBuilder strSql=new StringBuilder();
			   strSql.Append("update B_CLIENTELEControl set ");
			   			    			    			       
			    			    
			    if( dr.Table.Columns["ClIENTNO"]!=null && dr.Table.Columns["ClIENTNO"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" ClIENTNO = '"+dr["ClIENTNO"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["ControlLabNo"]!=null && dr.Table.Columns["ControlLabNo"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" ControlLabNo = '"+dr["ControlLabNo"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["ControlClIENTNO"]!=null && dr.Table.Columns["ControlClIENTNO"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" ControlClIENTNO = '"+dr["ControlClIENTNO"].ToString().Trim()+"' , ");
			    }
			      			    			    			       
			    			    
			    if( dr.Table.Columns["UseFlag"]!=null && dr.Table.Columns["UseFlag"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" UseFlag = '"+dr["UseFlag"].ToString().Trim()+"' , ");
			    }
			      		
			    
			    int n = strSql.ToString().LastIndexOf(",");
				strSql.Remove(n, 1);
				strSql.Append(" where ClIENTControlNo='"+dr["ClIENTControlNo"].ToString().Trim()+"' ");
						
                return idb.ExecuteNonQuery(strSql.ToString());
            }
            catch(Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Digitlab8.B_CLIENTELEControl .UpdateByDataRow同步数据时异常：", ex);
                return 0;
            }
        }
		
   
	}
}

