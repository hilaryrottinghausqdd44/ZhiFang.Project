using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Weblis  
{
	 	//B_BUSINESSMANControl
		
	public partial class B_BUSINESSMANControl: BaseDALLisDB, IDBUSINESSMANControl	{	

        public B_BUSINESSMANControl(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
		public B_BUSINESSMANControl()
		{
        }
		D_LogInfo d_log = new D_LogInfo();
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ZhiFang.Model.BUSINESSMANControl model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into B_BUSINESSMANControl(");			
            strSql.Append("BMANControlNo,BMANNO,ControlLabNo,ControlBMANNO,UseFlag");
			strSql.Append(") values (");
            strSql.Append("@BMANControlNo,@BMANNO,@ControlLabNo,@ControlBMANNO,@UseFlag");            
            strSql.Append(") ");            
            
			SqlParameter[] parameters = {
			            new SqlParameter("@BMANControlNo", SqlDbType.Char,50) ,            
                        new SqlParameter("@BMANNO", SqlDbType.Int,4) ,            
                        new SqlParameter("@ControlLabNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ControlBMANNO", SqlDbType.Int,4) ,            
                        new SqlParameter("@UseFlag", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.BMANControlNo;                        
            parameters[1].Value = model.BMANNO;                        
            parameters[2].Value = model.ControlLabNo;                        
            parameters[3].Value = model.ControlBMANNO;                        
            parameters[4].Value = model.UseFlag;                  
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
		public int Update(ZhiFang.Model.BUSINESSMANControl model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update B_BUSINESSMANControl set ");
			                                                
            strSql.Append(" BMANControlNo = @BMANControlNo , ");                                    
            strSql.Append(" BMANNO = @BMANNO , ");                                    
            strSql.Append(" ControlLabNo = @ControlLabNo , ");                                    
            strSql.Append(" ControlBMANNO = @ControlBMANNO , ");                                                                                    
            strSql.Append(" UseFlag = @UseFlag  ");            			
			strSql.Append(" where BMANControlNo=@BMANControlNo  ");
						
			SqlParameter[] parameters = {
			            	
                           
            new SqlParameter("@BMANControlNo", SqlDbType.Char,50) ,            	
                           
            new SqlParameter("@BMANNO", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@ControlLabNo", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@ControlBMANNO", SqlDbType.Int,4) ,            	
                        	
                        	
                           
            new SqlParameter("@UseFlag", SqlDbType.Int,4)             	
              
            };
            			    
				
                
			   
			if(model.BMANControlNo!=null)
			{
            	parameters[0].Value = model.BMANControlNo;            	
            }
            	
                
			   
			if(model.BMANNO!=null)
			{
            	parameters[1].Value = model.BMANNO;            	
            }
            	
                
			   
			if(model.ControlLabNo!=null)
			{
            	parameters[2].Value = model.ControlLabNo;            	
            }
            	
                
			   
			if(model.ControlBMANNO!=null)
			{
            	parameters[3].Value = model.ControlBMANNO;            	
            }
            	
                
				
                
				
                
			   
			if(model.UseFlag!=null)
			{
            	parameters[4].Value = model.UseFlag;            	
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
		public int Delete(string BMANControlNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from B_BUSINESSMANControl ");
			strSql.Append(" where BMANControlNo=@BMANControlNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@BMANControlNo", SqlDbType.Char,50)};
			parameters[0].Value = BMANControlNo;

            ZhiFang.Common.Log.Log.Info("strSql.ToString()");
			return DbHelperSQL.ExecuteNonQuery(strSql.ToString(),parameters);
			
		}
		
				/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteList(string Idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from B_BUSINESSMANControl ");
			strSql.Append(" where ID in ("+Idlist + ")  ");
			return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
			
		}
				
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZhiFang.Model.BUSINESSMANControl GetModel(string BMANControlNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Id, BMANControlNo, BMANNO, ControlLabNo, ControlBMANNO, DTimeStampe, AddTime, UseFlag  ");			
			strSql.Append("  from B_BUSINESSMANControl ");
			strSql.Append(" where BMANControlNo=@BMANControlNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@BMANControlNo", SqlDbType.Char,50)};
			parameters[0].Value = BMANControlNo;

			
			ZhiFang.Model.BUSINESSMANControl model=new ZhiFang.Model.BUSINESSMANControl();
			DataSet ds=DbHelperSQL.ExecuteDataSet(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["Id"].ToString()!="")
				{
					model.Id=int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
				}
																																				model.BMANControlNo= ds.Tables[0].Rows[0]["BMANControlNo"].ToString();
																												if(ds.Tables[0].Rows[0]["BMANNO"].ToString()!="")
				{
					model.BMANNO=int.Parse(ds.Tables[0].Rows[0]["BMANNO"].ToString());
				}
																																				model.ControlLabNo= ds.Tables[0].Rows[0]["ControlLabNo"].ToString();
																												if(ds.Tables[0].Rows[0]["ControlBMANNO"].ToString()!="")
				{
					model.ControlBMANNO=int.Parse(ds.Tables[0].Rows[0]["ControlBMANNO"].ToString());
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
			strSql.Append(" FROM B_BUSINESSMANControl ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.ExecuteDataSet(strSql.ToString());
		}
		
		
		
		/// <summary>
		/// 根据实体获取DataSet
		/// </summary>
        public DataSet GetList(ZhiFang.Model.BUSINESSMANControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM B_BUSINESSMANControl where 1=1 ");

            if (model.BMANControlNo != null)
            {
                strSql.Append(" and BMANControlNo='" + model.BMANControlNo + "' ");
            }

            if (model.BMANNO != 0)
            {
                strSql.Append(" and BMANNO=" + model.BMANNO + " ");
            }

            if (model.ControlLabNo != null)
            {
                strSql.Append(" and ControlLabNo='" + model.ControlLabNo + "' ");
            }

            if (model.ControlBMANNO != 0)
            {
                strSql.Append(" and ControlBMANNO=" + model.ControlBMANNO + " ");
            }

            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
		
		/// <summary>
		/// 获取总记录
		/// </summary>
		public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_BUSINESSMANControl ");
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
        public int GetTotalCount(ZhiFang.Model.BUSINESSMANControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_BUSINESSMANControl where 1=1 ");
           	                                          
            if(model.BMANControlNo !=null)
            {
                        strSql.Append(" and BMANControlNo='"+model.BMANControlNo+"' ");
                        }
                                          
            if(model.BMANNO !=null)
            {
                        strSql.Append(" and BMANNO="+model.BMANNO+" ");
                        }
                                          
            if(model.ControlLabNo !=null)
            {
                        strSql.Append(" and ControlLabNo='"+model.ControlLabNo+"' ");
                        }
                                          
            if(model.ControlBMANNO !=null)
            {
                        strSql.Append(" and ControlBMANNO="+model.ControlBMANNO+" ");
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
        
        
        public bool Exists(string BMANControlNo)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from B_BUSINESSMANControl ");
			strSql.Append(" where BMANControlNo ='"+BMANControlNo+"'");
			string strCount = DbHelperSQL.ExecuteScalar(strSql.ToString());
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
            strSql.Append("select count(1) from B_BUSINESSMANControl where 1=1 ");
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
            return DbHelperSQL.GetMaxID("BMANControlNo","B_BUSINESSMANControl");
        }

        public DataSet GetList(int Top, ZhiFang.Model.BUSINESSMANControl model, string filedOrder)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" * ");
			strSql.Append(" FROM B_BUSINESSMANControl ");		
			
			                                          
            if(model.BMANControlNo !=null)
            {
                        
            strSql.Append(" and BMANControlNo='"+model.BMANControlNo+"' ");
                        }
                                          
            if(model.BMANNO !=null)
            {
                        strSql.Append(" and BMANNO="+model.BMANNO+" ");
                        }
                                          
            if(model.ControlLabNo !=null)
            {
                        
            strSql.Append(" and ControlLabNo='"+model.ControlLabNo+"' ");
                        }
                                          
            if(model.ControlBMANNO !=null)
            {
                        strSql.Append(" and ControlBMANNO="+model.ControlBMANNO+" ");
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
                        if (this.Exists(ds.Tables[0].Rows[i]["BMANControlNo"].ToString().Trim()))
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
				strSql.Append("insert into B_BUSINESSMANControl (");			
            	strSql.Append("BMANControlNo,BMANNO,ControlLabNo,ControlBMANNO,UseFlag");
				strSql.Append(") values (");			
            	            	            	
            	if(dr.Table.Columns["BMANControlNo"]!=null && dr.Table.Columns["BMANControlNo"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["BMANControlNo"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["BMANNO"]!=null && dr.Table.Columns["BMANNO"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["BMANNO"].ToString().Trim()+"', ");
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
            	             	            	
            	if(dr.Table.Columns["ControlBMANNO"]!=null && dr.Table.Columns["ControlBMANNO"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["ControlBMANNO"].ToString().Trim()+"', ");
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
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Weblis.B_BUSINESSMANControl.AddByDataRow 同步数据时异常：", ex);
                return 0;
            }          
        }
        public int UpdateByDataRow(DataRow dr)
        {
            try
            {
               StringBuilder strSql=new StringBuilder();
			   strSql.Append("update B_BUSINESSMANControl set ");
			   			    			    			       
			    			    
			    if( dr.Table.Columns["BMANNO"]!=null && dr.Table.Columns["BMANNO"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" BMANNO = '"+dr["BMANNO"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["ControlLabNo"]!=null && dr.Table.Columns["ControlLabNo"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" ControlLabNo = '"+dr["ControlLabNo"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["ControlBMANNO"]!=null && dr.Table.Columns["ControlBMANNO"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" ControlBMANNO = '"+dr["ControlBMANNO"].ToString().Trim()+"' , ");
			    }
			      			    			    			       
			    			    
			    if( dr.Table.Columns["UseFlag"]!=null && dr.Table.Columns["UseFlag"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" UseFlag = '"+dr["UseFlag"].ToString().Trim()+"' , ");
			    }
			      		
			    
			    int n = strSql.ToString().LastIndexOf(",");
				strSql.Remove(n, 1);
				strSql.Append(" where BMANControlNo='"+dr["BMANControlNo"].ToString().Trim()+"' ");
						
                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch(Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Weblis.B_BUSINESSMANControl .UpdateByDataRow同步数据时异常：", ex);
                return 0;
            }
        }
		
   
	}
}

