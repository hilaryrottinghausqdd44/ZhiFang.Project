using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Digitlab8  
{
	 	//B_EquipmentControl
		
	public partial class B_EquipmentControl: IDEquipmentControl	{	
		DBUtility.IDBConnection idb;
        public B_EquipmentControl(string dbsourceconn)
        {
            idb = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
		public B_EquipmentControl()
		{
            idb = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.LisDB());
        }
		D_LogInfo d_log = new D_LogInfo();
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ZhiFang.Model.EquipmentControl model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into B_EquipmentControl(");			
            strSql.Append("EquipControlNo,EquipNo,ControlLabNo,ControlEquipNo,UseFlag");
			strSql.Append(") values (");
            strSql.Append("@EquipControlNo,@EquipNo,@ControlLabNo,@ControlEquipNo,@UseFlag");            
            strSql.Append(") ");            
            
			SqlParameter[] parameters = {
			            new SqlParameter("@EquipControlNo", SqlDbType.Char,50) ,            
                        new SqlParameter("@EquipNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@ControlLabNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ControlEquipNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@UseFlag", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.EquipControlNo;                        
            parameters[1].Value = model.EquipNo;                        
            parameters[2].Value = model.ControlLabNo;                        
            parameters[3].Value = model.ControlEquipNo;                        
            parameters[4].Value = model.UseFlag;                  
			if (idb.ExecuteNonQuery(strSql.ToString(),parameters) > 0)
            {
                return d_log.OperateLog("Equipment", "", "", DateTime.Now, 1);
            }
            else
                return -1;
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ZhiFang.Model.EquipmentControl model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update B_EquipmentControl set ");
			                                                
            strSql.Append(" EquipControlNo = @EquipControlNo , ");                                    
            strSql.Append(" EquipNo = @EquipNo , ");                                    
            strSql.Append(" ControlLabNo = @ControlLabNo , ");                                    
            strSql.Append(" ControlEquipNo = @ControlEquipNo , ");                                                                                    
            strSql.Append(" UseFlag = @UseFlag  ");            			
			strSql.Append(" where EquipControlNo=@EquipControlNo  ");
						
			SqlParameter[] parameters = {
			            	
                           
            new SqlParameter("@EquipControlNo", SqlDbType.Char,50) ,            	
                           
            new SqlParameter("@EquipNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@ControlLabNo", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@ControlEquipNo", SqlDbType.Int,4) ,            	
                        	
                        	
                           
            new SqlParameter("@UseFlag", SqlDbType.Int,4)             	
              
            };
            			    
				
                
			   
			if(model.EquipControlNo!=null)
			{
            	parameters[0].Value = model.EquipControlNo;            	
            }
            	
                
			   
			if(model.EquipNo!=null)
			{
            	parameters[1].Value = model.EquipNo;            	
            }
            	
                
			   
			if(model.ControlLabNo!=null)
			{
            	parameters[2].Value = model.ControlLabNo;            	
            }
            	
                
			   
			if(model.ControlEquipNo!=null)
			{
            	parameters[3].Value = model.ControlEquipNo;            	
            }
            	
                
				
                
				
                
			   
			if(model.UseFlag!=null)
			{
            	parameters[4].Value = model.UseFlag;            	
            }
            	
                        
			if (idb.ExecuteNonQuery(strSql.ToString(),parameters) > 0)
            {
                return d_log.OperateLog("Equipment", "", "", DateTime.Now, 1);
            }
            else
                return -1;
		}
		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(string EquipControlNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from B_EquipmentControl ");
			strSql.Append(" where EquipControlNo=@EquipControlNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@EquipControlNo", SqlDbType.Char,50)};
			parameters[0].Value = EquipControlNo;


			return idb.ExecuteNonQuery(strSql.ToString(),parameters);
			
		}
		
				/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteList(string Idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from B_EquipmentControl ");
			strSql.Append(" where ID in ("+Idlist + ")  ");
			return idb.ExecuteNonQuery(strSql.ToString());
			
		}
				
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZhiFang.Model.EquipmentControl GetModel(string EquipControlNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Id, EquipControlNo, EquipNo, ControlLabNo, ControlEquipNo, DTimeStampe, AddTime, UseFlag  ");			
			strSql.Append("  from B_EquipmentControl ");
			strSql.Append(" where EquipControlNo=@EquipControlNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@EquipControlNo", SqlDbType.Char,50)};
			parameters[0].Value = EquipControlNo;

			
			ZhiFang.Model.EquipmentControl model=new ZhiFang.Model.EquipmentControl();
			DataSet ds=idb.ExecuteDataSet(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["Id"].ToString()!="")
				{
					model.Id=int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
				}
																																				model.EquipControlNo= ds.Tables[0].Rows[0]["EquipControlNo"].ToString();
																												if(ds.Tables[0].Rows[0]["EquipNo"].ToString()!="")
				{
					model.EquipNo=int.Parse(ds.Tables[0].Rows[0]["EquipNo"].ToString());
				}
																																				model.ControlLabNo= ds.Tables[0].Rows[0]["ControlLabNo"].ToString();
																												if(ds.Tables[0].Rows[0]["ControlEquipNo"].ToString()!="")
				{
					model.ControlEquipNo=int.Parse(ds.Tables[0].Rows[0]["ControlEquipNo"].ToString());
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
			strSql.Append(" FROM B_EquipmentControl ");
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
			strSql.Append(" FROM B_EquipmentControl ");
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
		public DataSet GetList(ZhiFang.Model.EquipmentControl model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM B_EquipmentControl where 1=1 ");
			                                          
            if(model.EquipControlNo !=null)
            {
                        strSql.Append(" and EquipControlNo='"+model.EquipControlNo+"' ");
                        }
                                          
            if(model.EquipNo !=null)
            {
                        strSql.Append(" and EquipNo="+model.EquipNo+" ");
                        }
                                          
            if(model.ControlLabNo !=null)
            {
                        strSql.Append(" and ControlLabNo='"+model.ControlLabNo+"' ");
                        }
                                          
            if(model.ControlEquipNo !=null)
            {
                        strSql.Append(" and ControlEquipNo="+model.ControlEquipNo+" ");
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
            strSql.Append("select count(*) FROM B_EquipmentControl ");
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
        public int GetTotalCount(ZhiFang.Model.EquipmentControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_EquipmentControl where 1=1 ");
           	                                          
            if(model.EquipControlNo !=null)
            {
                        strSql.Append(" and EquipControlNo='"+model.EquipControlNo+"' ");
                        }
                                          
            if(model.EquipNo !=null)
            {
                        strSql.Append(" and EquipNo="+model.EquipNo+" ");
                        }
                                          
            if(model.ControlLabNo !=null)
            {
                        strSql.Append(" and ControlLabNo='"+model.ControlLabNo+"' ");
                        }
                                          
            if(model.ControlEquipNo !=null)
            {
                        strSql.Append(" and ControlEquipNo="+model.ControlEquipNo+" ");
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
        
        
        public bool Exists(string EquipControlNo)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from B_EquipmentControl ");
			strSql.Append(" where EquipControlNo ='"+EquipControlNo+"'");
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
            return idb.GetMaxID("EquipControlNo","B_EquipmentControl");
        }

        public DataSet GetList(int Top, ZhiFang.Model.EquipmentControl model, string filedOrder)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" * ");
			strSql.Append(" FROM B_EquipmentControl ");		
			
			                                          
            if(model.EquipControlNo !=null)
            {
                        
            strSql.Append(" and EquipControlNo='"+model.EquipControlNo+"' ");
                        }
                                          
            if(model.EquipNo !=null)
            {
                        strSql.Append(" and EquipNo="+model.EquipNo+" ");
                        }
                                          
            if(model.ControlLabNo !=null)
            {
                        
            strSql.Append(" and ControlLabNo='"+model.ControlLabNo+"' ");
                        }
                                          
            if(model.ControlEquipNo !=null)
            {
                        strSql.Append(" and ControlEquipNo="+model.ControlEquipNo+" ");
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



        #region IDataBase<EquipmentControl> 成员
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
                        if (this.Exists(ds.Tables[0].Rows[i]["EquipControlNo"].ToString().Trim()))
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
                strSql.Append("insert into B_EquipmentControl (");
                strSql.Append("EquipControlNo,EquipNo,ControlLabNo,ControlEquipNo,AddTime,UseFlag");
                strSql.Append(") values (");
                strSql.Append("'" + dr["EquipControlNo"].ToString().Trim() + "','" + dr["EquipNo"].ToString().Trim() + "','" + dr["ControlLabNo"].ToString().Trim() + "','" + dr["ControlEquipNo"].ToString().Trim() + "','" + DateTime.Parse(dr["AddTime"].ToString().Trim()) + "','" + dr["UseFlag"].ToString().Trim() + "'");
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
                strSql.Append("update B_EquipmentControl set ");

                strSql.Append(" EquipNo = '" + dr["EquipNo"].ToString().Trim() + "' , ");
                strSql.Append(" ControlLabNo = '" + dr["ControlLabNo"].ToString().Trim() + "' , ");
                strSql.Append(" ControlEquipNo = '" + dr["ControlEquipNo"].ToString().Trim() + "' , ");
                strSql.Append(" UseFlag = '" + dr["UseFlag"].ToString().Trim() + "'  ");
                strSql.Append(" where EquipControlNo='" + dr["EquipControlNo"].ToString().Trim() + "' ");

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

