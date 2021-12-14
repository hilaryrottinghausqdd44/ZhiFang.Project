using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
namespace ZhiFang.DAL.RBAC
{
	 	//RBAC_Operates
		public class RBAC_Operates
	{
   		     
		public bool Exists(string SN,int Type)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from RBAC_Operates");
			strSql.Append(" where ");
			                                                                   strSql.Append(" SN = @SN and  ");
                                                                   strSql.Append(" Type = @Type  ");
                            			SqlParameter[] parameters = {
					new SqlParameter("@SN", SqlDbType.NVarChar,50),
					new SqlParameter("@Type", SqlDbType.Int,4)			};
			parameters[0].Value = SN;
			parameters[1].Value = Type;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.RBAC_Operates model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into RBAC_Operates(");			
            strSql.Append("SN,CName,EName,SName,OperateColor,Type,Descr");
			strSql.Append(") values (");
            strSql.Append("@SN,@CName,@EName,@SName,@OperateColor,@Type,@Descr");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@SN", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@CName", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@EName", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@SName", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@OperateColor", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@Type", SqlDbType.Int,4) ,            
                        new SqlParameter("@Descr", SqlDbType.NVarChar,50)             
              
            };
			            
            parameters[0].Value = model.SN;                        
            parameters[1].Value = model.CName;                        
            parameters[2].Value = model.EName;                        
            parameters[3].Value = model.SName;                        
            parameters[4].Value = model.OperateColor;                        
            parameters[5].Value = model.Type;                        
            parameters[6].Value = model.Descr;                        
			   
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
		public bool Update(Maticsoft.Model.RBAC_Operates model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update RBAC_Operates set ");
			                                                
            strSql.Append(" SN = @SN , ");                                    
            strSql.Append(" CName = @CName , ");                                    
            strSql.Append(" EName = @EName , ");                                    
            strSql.Append(" SName = @SName , ");                                    
            strSql.Append(" OperateColor = @OperateColor , ");                                    
            strSql.Append(" Type = @Type , ");                                    
            strSql.Append(" Descr = @Descr  ");            			
			strSql.Append(" where ID=@ID ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4) ,            
                        new SqlParameter("@SN", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@CName", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@EName", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@SName", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@OperateColor", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@Type", SqlDbType.Int,4) ,            
                        new SqlParameter("@Descr", SqlDbType.NVarChar,50)             
              
            };
						            
            parameters[0].Value = model.ID;                        
            parameters[1].Value = model.SN;                        
            parameters[2].Value = model.CName;                        
            parameters[3].Value = model.EName;                        
            parameters[4].Value = model.SName;                        
            parameters[5].Value = model.OperateColor;                        
            parameters[6].Value = model.Type;                        
            parameters[7].Value = model.Descr;                        
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
		public bool Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from RBAC_Operates ");
			strSql.Append(" where ID=@ID");
						SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;


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
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from RBAC_Operates ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
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
		public Maticsoft.Model.RBAC_Operates GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID, SN, CName, EName, SName, OperateColor, Type, Descr  ");			
			strSql.Append("  from RBAC_Operates ");
			strSql.Append(" where ID=@ID");
						SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			
			Maticsoft.Model.RBAC_Operates model=new Maticsoft.Model.RBAC_Operates();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
																																				model.SN= ds.Tables[0].Rows[0]["SN"].ToString();
																																model.CName= ds.Tables[0].Rows[0]["CName"].ToString();
																																model.EName= ds.Tables[0].Rows[0]["EName"].ToString();
																																model.SName= ds.Tables[0].Rows[0]["SName"].ToString();
																																model.OperateColor= ds.Tables[0].Rows[0]["OperateColor"].ToString();
																												if(ds.Tables[0].Rows[0]["Type"].ToString()!="")
				{
					model.Type=int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
				}
																																				model.Descr= ds.Tables[0].Rows[0]["Descr"].ToString();
																										
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
			strSql.Append(" FROM RBAC_Operates ");
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
			strSql.Append(" FROM RBAC_Operates ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

   
	}
}

