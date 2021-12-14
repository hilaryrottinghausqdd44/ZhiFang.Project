using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Digitlab8  
{
	 	//B_StatusTypeControl
		
	public partial class B_StatusTypeControl: IDStatusTypeControl	{	
		DBUtility.IDBConnection idb;
        public B_StatusTypeControl(string dbsourceconn)
        {
            idb = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
		public B_StatusTypeControl()
		{
            idb = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.LisDB());
        }
		D_LogInfo d_log = new D_LogInfo();
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ZhiFang.Model.StatusTypeControl model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into B_StatusTypeControl(");			
            strSql.Append("StatusControlNo,StatusNo,ControlLabNo,ControlStatusNo,UseFlag");
			strSql.Append(") values (");
            strSql.Append("@StatusControlNo,@StatusNo,@ControlLabNo,@ControlStatusNo,@UseFlag");            
            strSql.Append(") ");            
            
			SqlParameter[] parameters = {
			            new SqlParameter("@StatusControlNo", SqlDbType.Char,50) ,            
                        new SqlParameter("@StatusNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@ControlLabNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ControlStatusNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@UseFlag", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.StatusControlNo;                        
            parameters[1].Value = model.StatusNo;                        
            parameters[2].Value = model.ControlLabNo;                        
            parameters[3].Value = model.ControlStatusNo;                        
            parameters[4].Value = model.UseFlag;                  
			if (idb.ExecuteNonQuery(strSql.ToString(),parameters) > 0)
            {
                return d_log.OperateLog("StatusType", "", "", DateTime.Now, 1);
            }
            else
                return -1;
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ZhiFang.Model.StatusTypeControl model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update B_StatusTypeControl set ");
			                                                
            strSql.Append(" StatusControlNo = @StatusControlNo , ");                                    
            strSql.Append(" StatusNo = @StatusNo , ");                                    
            strSql.Append(" ControlLabNo = @ControlLabNo , ");                                    
            strSql.Append(" ControlStatusNo = @ControlStatusNo , ");                                                                                    
            strSql.Append(" UseFlag = @UseFlag  ");            			
			strSql.Append(" where StatusControlNo=@StatusControlNo  ");
						
			SqlParameter[] parameters = {
			            	
                           
            new SqlParameter("@StatusControlNo", SqlDbType.Char,50) ,            	
                           
            new SqlParameter("@StatusNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@ControlLabNo", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@ControlStatusNo", SqlDbType.Int,4) ,            	
                        	
                        	
                           
            new SqlParameter("@UseFlag", SqlDbType.Int,4)             	
              
            };
            			    
				
                
			   
			if(model.StatusControlNo!=null)
			{
            	parameters[0].Value = model.StatusControlNo;            	
            }
            	
                
			   
			if(model.StatusNo!=null)
			{
            	parameters[1].Value = model.StatusNo;            	
            }
            	
                
			   
			if(model.ControlLabNo!=null)
			{
            	parameters[2].Value = model.ControlLabNo;            	
            }
            	
                
			   
			if(model.ControlStatusNo!=null)
			{
            	parameters[3].Value = model.ControlStatusNo;            	
            }
            	
                
				
                
				
                
			   
			if(model.UseFlag!=null)
			{
            	parameters[4].Value = model.UseFlag;            	
            }
            	
                        
			if (idb.ExecuteNonQuery(strSql.ToString(),parameters) > 0)
            {
                return d_log.OperateLog("StatusType", "", "", DateTime.Now, 1);
            }
            else
                return -1;
		}
		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(string StatusControlNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from B_StatusTypeControl ");
			strSql.Append(" where StatusControlNo=@StatusControlNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@StatusControlNo", SqlDbType.Char,50)};
			parameters[0].Value = StatusControlNo;


			return idb.ExecuteNonQuery(strSql.ToString(),parameters);
			
		}
		
				/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteList(string Idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from B_StatusTypeControl ");
			strSql.Append(" where ID in ("+Idlist + ")  ");
			return idb.ExecuteNonQuery(strSql.ToString());
			
		}
				
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZhiFang.Model.StatusTypeControl GetModel(string StatusControlNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Id, StatusControlNo, StatusNo, ControlLabNo, ControlStatusNo, DTimeStampe, AddTime, UseFlag  ");			
			strSql.Append("  from B_StatusTypeControl ");
			strSql.Append(" where StatusControlNo=@StatusControlNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@StatusControlNo", SqlDbType.Char,50)};
			parameters[0].Value = StatusControlNo;

			
			ZhiFang.Model.StatusTypeControl model=new ZhiFang.Model.StatusTypeControl();
			DataSet ds=idb.ExecuteDataSet(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["Id"].ToString()!="")
				{
					model.Id=int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
				}
																																				model.StatusControlNo= ds.Tables[0].Rows[0]["StatusControlNo"].ToString();
																												if(ds.Tables[0].Rows[0]["StatusNo"].ToString()!="")
				{
					model.StatusNo=int.Parse(ds.Tables[0].Rows[0]["StatusNo"].ToString());
				}
																																				model.ControlLabNo= ds.Tables[0].Rows[0]["ControlLabNo"].ToString();
																												if(ds.Tables[0].Rows[0]["ControlStatusNo"].ToString()!="")
				{
					model.ControlStatusNo=int.Parse(ds.Tables[0].Rows[0]["ControlStatusNo"].ToString());
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
			strSql.Append(" FROM B_StatusTypeControl ");
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
			strSql.Append(" FROM B_StatusTypeControl ");
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
		public DataSet GetList(ZhiFang.Model.StatusTypeControl model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM B_StatusTypeControl where 1=1 ");
			                                          
            if(model.StatusControlNo !=null)
            {
                        strSql.Append(" and StatusControlNo='"+model.StatusControlNo+"' ");
                        }
                                          
            if(model.StatusNo !=null)
            {
                        strSql.Append(" and StatusNo="+model.StatusNo+" ");
                        }
                                          
            if(model.ControlLabNo !=null)
            {
                        strSql.Append(" and ControlLabNo='"+model.ControlLabNo+"' ");
                        }
                                          
            if(model.ControlStatusNo !=null)
            {
                        strSql.Append(" and ControlStatusNo="+model.ControlStatusNo+" ");
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
                                    return idb.ExecuteDataSet(strSql.ToString());
		}
		
		/// <summary>
		/// 获取总记录
		/// </summary>
		public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_StatusTypeControl ");
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
        public int GetTotalCount(ZhiFang.Model.StatusTypeControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_StatusTypeControl where 1=1 ");
           	                                          
            if(model.StatusControlNo !=null)
            {
                        strSql.Append(" and StatusControlNo='"+model.StatusControlNo+"' ");
                        }
                                          
            if(model.StatusNo !=null)
            {
                        strSql.Append(" and StatusNo="+model.StatusNo+" ");
                        }
                                          
            if(model.ControlLabNo !=null)
            {
                        strSql.Append(" and ControlLabNo='"+model.ControlLabNo+"' ");
                        }
                                          
            if(model.ControlStatusNo !=null)
            {
                        strSql.Append(" and ControlStatusNo="+model.ControlStatusNo+" ");
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
        
        
        public bool Exists(string StatusControlNo)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from B_StatusTypeControl ");
			strSql.Append(" where StatusControlNo ='"+StatusControlNo+"'");
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
		
		public int GetMaxId()
        {
            return idb.GetMaxID("StatusControlNo","B_StatusTypeControl");
        }

        public DataSet GetList(int Top, ZhiFang.Model.StatusTypeControl model, string filedOrder)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" * ");
			strSql.Append(" FROM B_StatusTypeControl ");		
			
			                                          
            if(model.StatusControlNo !=null)
            {
                        
            strSql.Append(" and StatusControlNo='"+model.StatusControlNo+"' ");
                        }
                                          
            if(model.StatusNo !=null)
            {
                        strSql.Append(" and StatusNo="+model.StatusNo+" ");
                        }
                                          
            if(model.ControlLabNo !=null)
            {
                        
            strSql.Append(" and ControlLabNo='"+model.ControlLabNo+"' ");
                        }
                                          
            if(model.ControlStatusNo !=null)
            {
                        strSql.Append(" and ControlStatusNo="+model.ControlStatusNo+" ");
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



        #region IDataBase<StatusTypeControl> 成员
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
                        if (this.Exists(ds.Tables[0].Rows[i]["StatusControlNo"].ToString().Trim()))
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
                strSql.Append("insert into B_StatusTypeControl (");
                strSql.Append("StatusControlNo,StatusNo,ControlLabNo,ControlStatusNo,AddTime,UseFlag");
                strSql.Append(") values (");
                strSql.Append("'" + dr["StatusControlNo"].ToString().Trim() + "','" + dr["StatusNo"].ToString().Trim() + "','" + dr["ControlLabNo"].ToString().Trim() + "','" + dr["ControlStatusNo"].ToString().Trim() + "','" + DateTime.Parse(dr["AddTime"].ToString().Trim()) + "','" + dr["UseFlag"].ToString().Trim() + "'");
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
                strSql.Append("update B_StatusTypeControl set ");

                strSql.Append(" StatusNo = '" + dr["StatusNo"].ToString().Trim() + "' , ");
                strSql.Append(" ControlLabNo = '" + dr["ControlLabNo"].ToString().Trim() + "' , ");
                strSql.Append(" ControlStatusNo = '" + dr["ControlStatusNo"].ToString().Trim() + "' , ");
                strSql.Append(" UseFlag = '" + dr["UseFlag"].ToString().Trim() + "'  ");
                strSql.Append(" where StatusControlNo='" + dr["StatusControlNo"].ToString().Trim() + "' ");

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

