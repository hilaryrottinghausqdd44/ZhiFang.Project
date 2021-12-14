using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
namespace ZhiFang.DAL.RBAC
{
	 	//RBAC_Modules
		public class RBAC_Modules
	{
   		     
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from RBAC_Modules");
			strSql.Append(" where ");
			                                       strSql.Append(" ID = @ID  ");
                            			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.RBAC_Modules model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into RBAC_Modules(");			
            strSql.Append("SN,CName,EName,SName,Type,Image,URL,Para,Descr,ButtonsTheme,Owner,CreateDate,ModuleCode");
			strSql.Append(") values (");
            strSql.Append("@SN,@CName,@EName,@SName,@Type,@Image,@URL,@Para,@Descr,@ButtonsTheme,@Owner,@CreateDate,@ModuleCode");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@SN", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@CName", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@EName", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@SName", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@Type", SqlDbType.Int,4) ,            
                        new SqlParameter("@Image", SqlDbType.NVarChar,250) ,            
                        new SqlParameter("@URL", SqlDbType.NVarChar,150) ,            
                        new SqlParameter("@Para", SqlDbType.NVarChar,500) ,            
                        new SqlParameter("@Descr", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@ButtonsTheme", SqlDbType.NVarChar,40) ,            
                        new SqlParameter("@Owner", SqlDbType.Int,4) ,            
                        new SqlParameter("@CreateDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@ModuleCode", SqlDbType.VarChar,30)             
              
            };
			            
            parameters[0].Value = model.SN;                        
            parameters[1].Value = model.CName;                        
            parameters[2].Value = model.EName;                        
            parameters[3].Value = model.SName;                        
            parameters[4].Value = model.Type;                        
            parameters[5].Value = model.Image;                        
            parameters[6].Value = model.URL;                        
            parameters[7].Value = model.Para;                        
            parameters[8].Value = model.Descr;                        
            parameters[9].Value = model.ButtonsTheme;                        
            parameters[10].Value = model.Owner;                        
            parameters[11].Value = model.CreateDate;                        
            parameters[12].Value = model.ModuleCode;                        
			   
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
		public bool Update(Maticsoft.Model.RBAC_Modules model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update RBAC_Modules set ");
			                                                
            strSql.Append(" SN = @SN , ");                                    
            strSql.Append(" CName = @CName , ");                                    
            strSql.Append(" EName = @EName , ");                                    
            strSql.Append(" SName = @SName , ");                                    
            strSql.Append(" Type = @Type , ");                                    
            strSql.Append(" Image = @Image , ");                                    
            strSql.Append(" URL = @URL , ");                                    
            strSql.Append(" Para = @Para , ");                                    
            strSql.Append(" Descr = @Descr , ");                                    
            strSql.Append(" ButtonsTheme = @ButtonsTheme , ");                                    
            strSql.Append(" Owner = @Owner , ");                                    
            strSql.Append(" CreateDate = @CreateDate , ");                                    
            strSql.Append(" ModuleCode = @ModuleCode  ");            			
			strSql.Append(" where ID=@ID ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4) ,            
                        new SqlParameter("@SN", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@CName", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@EName", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@SName", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@Type", SqlDbType.Int,4) ,            
                        new SqlParameter("@Image", SqlDbType.NVarChar,250) ,            
                        new SqlParameter("@URL", SqlDbType.NVarChar,150) ,            
                        new SqlParameter("@Para", SqlDbType.NVarChar,500) ,            
                        new SqlParameter("@Descr", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@ButtonsTheme", SqlDbType.NVarChar,40) ,            
                        new SqlParameter("@Owner", SqlDbType.Int,4) ,            
                        new SqlParameter("@CreateDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@ModuleCode", SqlDbType.VarChar,30)             
              
            };
						            
            parameters[0].Value = model.ID;                        
            parameters[1].Value = model.SN;                        
            parameters[2].Value = model.CName;                        
            parameters[3].Value = model.EName;                        
            parameters[4].Value = model.SName;                        
            parameters[5].Value = model.Type;                        
            parameters[6].Value = model.Image;                        
            parameters[7].Value = model.URL;                        
            parameters[8].Value = model.Para;                        
            parameters[9].Value = model.Descr;                        
            parameters[10].Value = model.ButtonsTheme;                        
            parameters[11].Value = model.Owner;                        
            parameters[12].Value = model.CreateDate;                        
            parameters[13].Value = model.ModuleCode;                        
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
			strSql.Append("delete from RBAC_Modules ");
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
			strSql.Append("delete from RBAC_Modules ");
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
		public Maticsoft.Model.RBAC_Modules GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID, SN, CName, EName, SName, Type, Image, URL, Para, Descr, ButtonsTheme, Owner, CreateDate, ModuleCode  ");			
			strSql.Append("  from RBAC_Modules ");
			strSql.Append(" where ID=@ID");
						SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			
			Maticsoft.Model.RBAC_Modules model=new Maticsoft.Model.RBAC_Modules();
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
																												if(ds.Tables[0].Rows[0]["Type"].ToString()!="")
				{
					model.Type=int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
				}
																																				model.Image= ds.Tables[0].Rows[0]["Image"].ToString();
																																model.URL= ds.Tables[0].Rows[0]["URL"].ToString();
																																model.Para= ds.Tables[0].Rows[0]["Para"].ToString();
																																model.Descr= ds.Tables[0].Rows[0]["Descr"].ToString();
																																model.ButtonsTheme= ds.Tables[0].Rows[0]["ButtonsTheme"].ToString();
																												if(ds.Tables[0].Rows[0]["Owner"].ToString()!="")
				{
					model.Owner=int.Parse(ds.Tables[0].Rows[0]["Owner"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["CreateDate"].ToString()!="")
				{
					model.CreateDate=DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
				}
																																				model.ModuleCode= ds.Tables[0].Rows[0]["ModuleCode"].ToString();
																										
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
			strSql.Append(" FROM RBAC_Modules ");
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
			strSql.Append(" FROM RBAC_Modules ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

   
	}
}

