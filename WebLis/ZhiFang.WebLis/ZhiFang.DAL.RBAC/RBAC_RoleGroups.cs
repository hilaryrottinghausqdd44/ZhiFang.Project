using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
namespace ZhiFang.DAL.RBAC
{
	 	//RBAC_RoleGroups
		public class RBAC_RoleGroups
	{
   		     
		public bool Exists(string RoleGroupNo,string RoleGroupType)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from RBAC_RoleGroups");
			strSql.Append(" where ");
			                                                                   strSql.Append(" RoleGroupNo = @RoleGroupNo and  ");
                                                                   strSql.Append(" RoleGroupType = @RoleGroupType  ");
                            			SqlParameter[] parameters = {
					new SqlParameter("@RoleGroupNo", SqlDbType.VarChar,50),
					new SqlParameter("@RoleGroupType", SqlDbType.VarChar,50)			};
			parameters[0].Value = RoleGroupNo;
			parameters[1].Value = RoleGroupType;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.RBAC_RoleGroups model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into RBAC_RoleGroups(");			
            strSql.Append("RoleGroupOrder,RoleGroupNo,RoleGroupName,RoleGroupEnabled,RoleGroupDesc,RoleGroupType");
			strSql.Append(") values (");
            strSql.Append("@RoleGroupOrder,@RoleGroupNo,@RoleGroupName,@RoleGroupEnabled,@RoleGroupDesc,@RoleGroupType");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@RoleGroupOrder", SqlDbType.Int,4) ,            
                        new SqlParameter("@RoleGroupNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@RoleGroupName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@RoleGroupEnabled", SqlDbType.Int,4) ,            
                        new SqlParameter("@RoleGroupDesc", SqlDbType.VarChar,250) ,            
                        new SqlParameter("@RoleGroupType", SqlDbType.VarChar,50)             
              
            };
			            
            parameters[0].Value = model.RoleGroupOrder;                        
            parameters[1].Value = model.RoleGroupNo;                        
            parameters[2].Value = model.RoleGroupName;                        
            parameters[3].Value = model.RoleGroupEnabled;                        
            parameters[4].Value = model.RoleGroupDesc;                        
            parameters[5].Value = model.RoleGroupType;                        
			   
			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);			
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
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.RBAC_RoleGroups model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update RBAC_RoleGroups set ");
			                                                
            strSql.Append(" RoleGroupOrder = @RoleGroupOrder , ");                                    
            strSql.Append(" RoleGroupNo = @RoleGroupNo , ");                                    
            strSql.Append(" RoleGroupName = @RoleGroupName , ");                                    
            strSql.Append(" RoleGroupEnabled = @RoleGroupEnabled , ");                                    
            strSql.Append(" RoleGroupDesc = @RoleGroupDesc , ");                                    
            strSql.Append(" RoleGroupType = @RoleGroupType  ");            			
			strSql.Append(" where RoleGroupID=@RoleGroupID ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@RoleGroupID", SqlDbType.Int,4) ,            
                        new SqlParameter("@RoleGroupOrder", SqlDbType.Int,4) ,            
                        new SqlParameter("@RoleGroupNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@RoleGroupName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@RoleGroupEnabled", SqlDbType.Int,4) ,            
                        new SqlParameter("@RoleGroupDesc", SqlDbType.VarChar,250) ,            
                        new SqlParameter("@RoleGroupType", SqlDbType.VarChar,50)             
              
            };
						            
            parameters[0].Value = model.RoleGroupID;                        
            parameters[1].Value = model.RoleGroupOrder;                        
            parameters[2].Value = model.RoleGroupNo;                        
            parameters[3].Value = model.RoleGroupName;                        
            parameters[4].Value = model.RoleGroupEnabled;                        
            parameters[5].Value = model.RoleGroupDesc;                        
            parameters[6].Value = model.RoleGroupType;                        
            int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool Delete(int RoleGroupID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from RBAC_RoleGroups ");
			strSql.Append(" where RoleGroupID=@RoleGroupID");
						SqlParameter[] parameters = {
					new SqlParameter("@RoleGroupID", SqlDbType.Int,4)
			};
			parameters[0].Value = RoleGroupID;


			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string RoleGroupIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from RBAC_RoleGroups ");
			strSql.Append(" where ID in ("+RoleGroupIDlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
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
		public Maticsoft.Model.RBAC_RoleGroups GetModel(int RoleGroupID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select RoleGroupID, RoleGroupOrder, RoleGroupNo, RoleGroupName, RoleGroupEnabled, RoleGroupDesc, RoleGroupType  ");			
			strSql.Append("  from RBAC_RoleGroups ");
			strSql.Append(" where RoleGroupID=@RoleGroupID");
						SqlParameter[] parameters = {
					new SqlParameter("@RoleGroupID", SqlDbType.Int,4)
			};
			parameters[0].Value = RoleGroupID;

			
			Maticsoft.Model.RBAC_RoleGroups model=new Maticsoft.Model.RBAC_RoleGroups();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["RoleGroupID"].ToString()!="")
				{
					model.RoleGroupID=int.Parse(ds.Tables[0].Rows[0]["RoleGroupID"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["RoleGroupOrder"].ToString()!="")
				{
					model.RoleGroupOrder=int.Parse(ds.Tables[0].Rows[0]["RoleGroupOrder"].ToString());
				}
																																				model.RoleGroupNo= ds.Tables[0].Rows[0]["RoleGroupNo"].ToString();
																																model.RoleGroupName= ds.Tables[0].Rows[0]["RoleGroupName"].ToString();
																												if(ds.Tables[0].Rows[0]["RoleGroupEnabled"].ToString()!="")
				{
					model.RoleGroupEnabled=int.Parse(ds.Tables[0].Rows[0]["RoleGroupEnabled"].ToString());
				}
																																				model.RoleGroupDesc= ds.Tables[0].Rows[0]["RoleGroupDesc"].ToString();
																																model.RoleGroupType= ds.Tables[0].Rows[0]["RoleGroupType"].ToString();
																										
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
			strSql.Append(" FROM RBAC_RoleGroups ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
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
			strSql.Append(" FROM RBAC_RoleGroups ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

   
	}
}

