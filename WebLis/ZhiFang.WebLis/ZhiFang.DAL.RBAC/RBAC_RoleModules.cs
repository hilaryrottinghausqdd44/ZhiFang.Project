using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
namespace ZhiFang.DAL.RBAC
{
	 	//RBAC_RoleModules
		public class RBAC_RoleModules
	{
   		     
		public bool Exists(int ModuleID,int EmplID,int DeptID,int PositionID,int PostID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from RBAC_RoleModules");
			strSql.Append(" where ");
			                                       strSql.Append(" ModuleID = @ModuleID and  ");
                                                                   strSql.Append(" EmplID = @EmplID and  ");
                                                                   strSql.Append(" DeptID = @DeptID and  ");
                                                                   strSql.Append(" PositionID = @PositionID and  ");
                                                                   strSql.Append(" PostID = @PostID  ");
                            			SqlParameter[] parameters = {
					new SqlParameter("@ModuleID", SqlDbType.Int,4),
					new SqlParameter("@EmplID", SqlDbType.Int,4),
					new SqlParameter("@DeptID", SqlDbType.Int,4),
					new SqlParameter("@PositionID", SqlDbType.Int,4),
					new SqlParameter("@PostID", SqlDbType.Int,4)			};
			parameters[0].Value = ModuleID;
			parameters[1].Value = EmplID;
			parameters[2].Value = DeptID;
			parameters[3].Value = PositionID;
			parameters[4].Value = PostID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(Maticsoft.Model.RBAC_RoleModules model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into RBAC_RoleModules(");			
            strSql.Append("SN,ModuleID,EmplID,DeptID,PositionID,PostID,AccAbility,OpAbility,Validity");
			strSql.Append(") values (");
            strSql.Append("@SN,@ModuleID,@EmplID,@DeptID,@PositionID,@PostID,@AccAbility,@OpAbility,@Validity");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@SN", SqlDbType.Int,4) ,            
                        new SqlParameter("@ModuleID", SqlDbType.Int,4) ,            
                        new SqlParameter("@EmplID", SqlDbType.Int,4) ,            
                        new SqlParameter("@DeptID", SqlDbType.Int,4) ,            
                        new SqlParameter("@PositionID", SqlDbType.Int,4) ,            
                        new SqlParameter("@PostID", SqlDbType.Int,4) ,            
                        new SqlParameter("@AccAbility", SqlDbType.SmallInt,2) ,            
                        new SqlParameter("@OpAbility", SqlDbType.SmallInt,2) ,            
                        new SqlParameter("@Validity", SqlDbType.Char,2048)             
              
            };
			            
            parameters[0].Value = model.SN;                        
            parameters[1].Value = model.ModuleID;                        
            parameters[2].Value = model.EmplID;                        
            parameters[3].Value = model.DeptID;                        
            parameters[4].Value = model.PositionID;                        
            parameters[5].Value = model.PostID;                        
            parameters[6].Value = model.AccAbility;                        
            parameters[7].Value = model.OpAbility;                        
            parameters[8].Value = model.Validity;                        
			            DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.RBAC_RoleModules model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update RBAC_RoleModules set ");
			                        
            strSql.Append(" SN = @SN , ");                                    
            strSql.Append(" ModuleID = @ModuleID , ");                                    
            strSql.Append(" EmplID = @EmplID , ");                                    
            strSql.Append(" DeptID = @DeptID , ");                                    
            strSql.Append(" PositionID = @PositionID , ");                                    
            strSql.Append(" PostID = @PostID , ");                                    
            strSql.Append(" AccAbility = @AccAbility , ");                                    
            strSql.Append(" OpAbility = @OpAbility , ");                                    
            strSql.Append(" Validity = @Validity  ");            			
			strSql.Append(" where ModuleID=@ModuleID and EmplID=@EmplID and DeptID=@DeptID and PositionID=@PositionID and PostID=@PostID  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@SN", SqlDbType.Int,4) ,            
                        new SqlParameter("@ModuleID", SqlDbType.Int,4) ,            
                        new SqlParameter("@EmplID", SqlDbType.Int,4) ,            
                        new SqlParameter("@DeptID", SqlDbType.Int,4) ,            
                        new SqlParameter("@PositionID", SqlDbType.Int,4) ,            
                        new SqlParameter("@PostID", SqlDbType.Int,4) ,            
                        new SqlParameter("@AccAbility", SqlDbType.SmallInt,2) ,            
                        new SqlParameter("@OpAbility", SqlDbType.SmallInt,2) ,            
                        new SqlParameter("@Validity", SqlDbType.Char,2048)             
              
            };
						            
            parameters[0].Value = model.SN;                        
            parameters[1].Value = model.ModuleID;                        
            parameters[2].Value = model.EmplID;                        
            parameters[3].Value = model.DeptID;                        
            parameters[4].Value = model.PositionID;                        
            parameters[5].Value = model.PostID;                        
            parameters[6].Value = model.AccAbility;                        
            parameters[7].Value = model.OpAbility;                        
            parameters[8].Value = model.Validity;                        
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
		public bool Delete(int ModuleID,int EmplID,int DeptID,int PositionID,int PostID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from RBAC_RoleModules ");
			strSql.Append(" where ModuleID=@ModuleID and EmplID=@EmplID and DeptID=@DeptID and PositionID=@PositionID and PostID=@PostID ");
						SqlParameter[] parameters = {
					new SqlParameter("@ModuleID", SqlDbType.Int,4),
					new SqlParameter("@EmplID", SqlDbType.Int,4),
					new SqlParameter("@DeptID", SqlDbType.Int,4),
					new SqlParameter("@PositionID", SqlDbType.Int,4),
					new SqlParameter("@PostID", SqlDbType.Int,4)			};
			parameters[0].Value = ModuleID;
			parameters[1].Value = EmplID;
			parameters[2].Value = DeptID;
			parameters[3].Value = PositionID;
			parameters[4].Value = PostID;


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
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.RBAC_RoleModules GetModel(int ModuleID,int EmplID,int DeptID,int PositionID,int PostID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select SN, ModuleID, EmplID, DeptID, PositionID, PostID, AccAbility, OpAbility, Validity  ");			
			strSql.Append("  from RBAC_RoleModules ");
			strSql.Append(" where ModuleID=@ModuleID and EmplID=@EmplID and DeptID=@DeptID and PositionID=@PositionID and PostID=@PostID ");
						SqlParameter[] parameters = {
					new SqlParameter("@ModuleID", SqlDbType.Int,4),
					new SqlParameter("@EmplID", SqlDbType.Int,4),
					new SqlParameter("@DeptID", SqlDbType.Int,4),
					new SqlParameter("@PositionID", SqlDbType.Int,4),
					new SqlParameter("@PostID", SqlDbType.Int,4)			};
			parameters[0].Value = ModuleID;
			parameters[1].Value = EmplID;
			parameters[2].Value = DeptID;
			parameters[3].Value = PositionID;
			parameters[4].Value = PostID;

			
			Maticsoft.Model.RBAC_RoleModules model=new Maticsoft.Model.RBAC_RoleModules();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["SN"].ToString()!="")
				{
					model.SN=int.Parse(ds.Tables[0].Rows[0]["SN"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["ModuleID"].ToString()!="")
				{
					model.ModuleID=int.Parse(ds.Tables[0].Rows[0]["ModuleID"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["EmplID"].ToString()!="")
				{
					model.EmplID=int.Parse(ds.Tables[0].Rows[0]["EmplID"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["DeptID"].ToString()!="")
				{
					model.DeptID=int.Parse(ds.Tables[0].Rows[0]["DeptID"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["PositionID"].ToString()!="")
				{
					model.PositionID=int.Parse(ds.Tables[0].Rows[0]["PositionID"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["PostID"].ToString()!="")
				{
					model.PostID=int.Parse(ds.Tables[0].Rows[0]["PostID"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["AccAbility"].ToString()!="")
				{
					model.AccAbility=int.Parse(ds.Tables[0].Rows[0]["AccAbility"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["OpAbility"].ToString()!="")
				{
					model.OpAbility=int.Parse(ds.Tables[0].Rows[0]["OpAbility"].ToString());
				}
																																				model.Validity= ds.Tables[0].Rows[0]["Validity"].ToString();
																										
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
			strSql.Append(" FROM RBAC_RoleModules ");
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
			strSql.Append(" FROM RBAC_RoleModules ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

   
	}
}

