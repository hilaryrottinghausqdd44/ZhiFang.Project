using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Digitlab8  
{
	 	//U_Dic_PUser
		
	public partial class U_Dic_PUser : IDPUser,IDBatchCopy,IDGetListByTimeStampe
	{	
		DBUtility.IDBConnection idb;
        public U_Dic_PUser(string dbsourceconn)
        {
            idb = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
		public U_Dic_PUser()
		{
            idb = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.LisDB());
        }
		D_LogInfo d_log = new D_LogInfo();
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ZhiFang.Model.PUser model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into U_Dic_PUser(");			
            strSql.Append("UserNo,CName,Password,ShortCode,Gender,Birthday,Role,Resume,Visible,DispOrder,HisOrderCode,userimage,usertype,DeptNo,SectorTypeNo,UserImeName,IsManager,PassWordS,StandCode,ZFStandCode,UseFlag");
			strSql.Append(") values (");
            strSql.Append("@UserNo,@CName,@Password,@ShortCode,@Gender,@Birthday,@Role,@Resume,@Visible,@DispOrder,@HisOrderCode,@userimage,@usertype,@DeptNo,@SectorTypeNo,@UserImeName,@IsManager,@PassWordS,@StandCode,@ZFStandCode,@UseFlag");            
            strSql.Append(") ");            
            
			SqlParameter[] parameters = {
			            new SqlParameter("@UserNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@CName", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@Password", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@ShortCode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@Gender", SqlDbType.Int,4) ,            
                        new SqlParameter("@Birthday", SqlDbType.DateTime) ,            
                        new SqlParameter("@Role", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Resume", SqlDbType.VarChar,250) ,            
                        new SqlParameter("@Visible", SqlDbType.Int,4) ,            
                        new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            
                        new SqlParameter("@HisOrderCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@userimage", SqlDbType.Image) ,            
                        new SqlParameter("@usertype", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@DeptNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@SectorTypeNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@UserImeName", SqlDbType.VarChar,80) ,            
                        new SqlParameter("@IsManager", SqlDbType.Int,4) ,            
                        new SqlParameter("@PassWordS", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ZFStandCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@UseFlag", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.UserNo;                        
            parameters[1].Value = model.CName;                        
            parameters[2].Value = model.Password;                        
            parameters[3].Value = model.ShortCode;                        
            parameters[4].Value = model.Gender;                        
            parameters[5].Value = model.Birthday;                        
            parameters[6].Value = model.Role;                        
            parameters[7].Value = model.Resume;                        
            parameters[8].Value = model.Visible;                        
            parameters[9].Value = model.DispOrder;                        
            parameters[10].Value = model.HisOrderCode;                        
            parameters[11].Value = model.userimage;                        
            parameters[12].Value = model.usertype;                        
            parameters[13].Value = model.DeptNo;                        
            parameters[14].Value = model.SectorTypeNo;                        
            parameters[15].Value = model.UserImeName;                        
            parameters[16].Value = model.IsManager;                        
            parameters[17].Value = model.PassWordS;                        
            parameters[18].Value = model.StandCode;                        
            parameters[19].Value = model.ZFStandCode;                        
            parameters[20].Value = model.UseFlag;                  
			if (idb.ExecuteNonQuery(strSql.ToString(),parameters) > 0)
            {
                return d_log.OperateLog("PUser", "", "", DateTime.Now, 1);
            }
            else
                return -1;
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ZhiFang.Model.PUser model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update U_Dic_PUser set ");
			                                                
            strSql.Append(" UserNo = @UserNo , ");                                    
            strSql.Append(" CName = @CName , ");                                    
            strSql.Append(" Password = @Password , ");                                    
            strSql.Append(" ShortCode = @ShortCode , ");                                    
            strSql.Append(" Gender = @Gender , ");                                    
            strSql.Append(" Birthday = @Birthday , ");                                    
            strSql.Append(" Role = @Role , ");                                    
            strSql.Append(" Resume = @Resume , ");                                    
            strSql.Append(" Visible = @Visible , ");                                    
            strSql.Append(" DispOrder = @DispOrder , ");                                    
            strSql.Append(" HisOrderCode = @HisOrderCode , ");                                    
            strSql.Append(" userimage = @userimage , ");                                    
            strSql.Append(" usertype = @usertype , ");                                    
            strSql.Append(" DeptNo = @DeptNo , ");                                    
            strSql.Append(" SectorTypeNo = @SectorTypeNo , ");                                    
            strSql.Append(" UserImeName = @UserImeName , ");                                    
            strSql.Append(" IsManager = @IsManager , ");                                    
            strSql.Append(" PassWordS = @PassWordS , ");                                                                                    
            strSql.Append(" StandCode = @StandCode , ");                                    
            strSql.Append(" ZFStandCode = @ZFStandCode , ");                                    
            strSql.Append(" UseFlag = @UseFlag  ");            			
			strSql.Append(" where UserNo=@UserNo  ");
						
			SqlParameter[] parameters = {
			            	
                           
            new SqlParameter("@UserNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@CName", SqlDbType.VarChar,10) ,            	
                           
            new SqlParameter("@Password", SqlDbType.VarChar,10) ,            	
                           
            new SqlParameter("@ShortCode", SqlDbType.VarChar,10) ,            	
                           
            new SqlParameter("@Gender", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@Birthday", SqlDbType.DateTime) ,            	
                           
            new SqlParameter("@Role", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@Resume", SqlDbType.VarChar,250) ,            	
                           
            new SqlParameter("@Visible", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@HisOrderCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@userimage", SqlDbType.Image) ,            	
                           
            new SqlParameter("@usertype", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@DeptNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@SectorTypeNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@UserImeName", SqlDbType.VarChar,80) ,            	
                           
            new SqlParameter("@IsManager", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@PassWordS", SqlDbType.VarChar,20) ,            	
                        	
                        	
                           
            new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@ZFStandCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@UseFlag", SqlDbType.Int,4)             	
              
            };
            			    
				
                
			   
			if(model.UserNo!=null)
			{
            	parameters[0].Value = model.UserNo;            	
            }
            	
                
			   
			if(model.CName!=null)
			{
            	parameters[1].Value = model.CName;            	
            }
            	
                
			   
			if(model.Password!=null)
			{
            	parameters[2].Value = model.Password;            	
            }
            	
                
			   
			if(model.ShortCode!=null)
			{
            	parameters[3].Value = model.ShortCode;            	
            }
            	
                
			   
			if(model.Gender!=null)
			{
            	parameters[4].Value = model.Gender;            	
            }
            	
                
			   
			if(model.Birthday!=null)
			{
            	parameters[5].Value = model.Birthday;            	
            }
            	
                
			   
			if(model.Role!=null)
			{
            	parameters[6].Value = model.Role;            	
            }
            	
                
			   
			if(model.Resume!=null)
			{
            	parameters[7].Value = model.Resume;            	
            }
            	
                
			   
			if(model.Visible!=null)
			{
            	parameters[8].Value = model.Visible;            	
            }
            	
                
			   
			if(model.DispOrder!=null)
			{
            	parameters[9].Value = model.DispOrder;            	
            }
            	
                
			   
			if(model.HisOrderCode!=null)
			{
            	parameters[10].Value = model.HisOrderCode;            	
            }
            	
                
			   
			if(model.userimage!=null)
			{
            	parameters[11].Value = model.userimage;            	
            }
            	
                
			   
			if(model.usertype!=null)
			{
            	parameters[12].Value = model.usertype;            	
            }
            	
                
			   
			if(model.DeptNo!=null)
			{
            	parameters[13].Value = model.DeptNo;            	
            }
            	
                
			   
			if(model.SectorTypeNo!=null)
			{
            	parameters[14].Value = model.SectorTypeNo;            	
            }
            	
                
			   
			if(model.UserImeName!=null)
			{
            	parameters[15].Value = model.UserImeName;            	
            }
            	
                
			   
			if(model.IsManager!=null)
			{
            	parameters[16].Value = model.IsManager;            	
            }
            	
                
			   
			if(model.PassWordS!=null)
			{
            	parameters[17].Value = model.PassWordS;            	
            }
            	
                
				
                
				
                
			   
			if(model.StandCode!=null)
			{
            	parameters[18].Value = model.StandCode;            	
            }
            	
                
			   
			if(model.ZFStandCode!=null)
			{
            	parameters[19].Value = model.ZFStandCode;            	
            }
            	
                
			   
			if(model.UseFlag!=null)
			{
            	parameters[20].Value = model.UseFlag;            	
            }
            	
                        
			if (idb.ExecuteNonQuery(strSql.ToString(),parameters) > 0)
            {
                return d_log.OperateLog("PUser", "", "", DateTime.Now, 1);
            }
            else
                return -1;
		}
		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int UserNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from U_Dic_PUser ");
			strSql.Append(" where UserNo=@UserNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@UserNo", SqlDbType.Int,4)};
			parameters[0].Value = UserNo;


			return idb.ExecuteNonQuery(strSql.ToString(),parameters);
		
		}
		
				/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteList(string UserIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from U_Dic_PUser ");
			strSql.Append(" where ID in ("+UserIDlist + ")  ");
			return idb.ExecuteNonQuery(strSql.ToString());
			
		}
				
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZhiFang.Model.PUser GetModel(int UserNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select UserID, UserNo, CName, Password, ShortCode, Gender, Birthday, Role, Resume, Visible, DispOrder, HisOrderCode, userimage, usertype, DeptNo, SectorTypeNo, UserImeName, IsManager, PassWordS, DTimeStampe, AddTime, StandCode, ZFStandCode, UseFlag  ");			
			strSql.Append("  from U_Dic_PUser ");
			strSql.Append(" where UserNo=@UserNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@UserNo", SqlDbType.Int,4)};
			parameters[0].Value = UserNo;

			
			ZhiFang.Model.PUser model=new ZhiFang.Model.PUser();
			DataSet ds=idb.ExecuteDataSet(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																if(ds.Tables[0].Rows[0]["UserID"].ToString()!="")
				{
					model.UserID=int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
				}
																																				if(ds.Tables[0].Rows[0]["UserNo"].ToString()!="")
				{
					model.UserNo=int.Parse(ds.Tables[0].Rows[0]["UserNo"].ToString());
				}
																																								model.CName= ds.Tables[0].Rows[0]["CName"].ToString();
																																				model.Password= ds.Tables[0].Rows[0]["Password"].ToString();
																																				model.ShortCode= ds.Tables[0].Rows[0]["ShortCode"].ToString();
																																if(ds.Tables[0].Rows[0]["Gender"].ToString()!="")
				{
					model.Gender=int.Parse(ds.Tables[0].Rows[0]["Gender"].ToString());
				}
																																				if(ds.Tables[0].Rows[0]["Birthday"].ToString()!="")
				{
					model.Birthday=DateTime.Parse(ds.Tables[0].Rows[0]["Birthday"].ToString());
				}
																																								model.Role= ds.Tables[0].Rows[0]["Role"].ToString();
																																				model.Resume= ds.Tables[0].Rows[0]["Resume"].ToString();
																																if(ds.Tables[0].Rows[0]["Visible"].ToString()!="")
				{
					model.Visible=int.Parse(ds.Tables[0].Rows[0]["Visible"].ToString());
				}
																																				if(ds.Tables[0].Rows[0]["DispOrder"].ToString()!="")
				{
					model.DispOrder=int.Parse(ds.Tables[0].Rows[0]["DispOrder"].ToString());
				}
																																								model.HisOrderCode= ds.Tables[0].Rows[0]["HisOrderCode"].ToString();
																																								if(ds.Tables[0].Rows[0]["userimage"].ToString()!="")
				{
					model.userimage= (byte[])ds.Tables[0].Rows[0]["userimage"];
				}
																																model.usertype= ds.Tables[0].Rows[0]["usertype"].ToString();
																																if(ds.Tables[0].Rows[0]["DeptNo"].ToString()!="")
				{
					model.DeptNo=int.Parse(ds.Tables[0].Rows[0]["DeptNo"].ToString());
				}
																																				if(ds.Tables[0].Rows[0]["SectorTypeNo"].ToString()!="")
				{
					model.SectorTypeNo=int.Parse(ds.Tables[0].Rows[0]["SectorTypeNo"].ToString());
				}
																																								model.UserImeName= ds.Tables[0].Rows[0]["UserImeName"].ToString();
																																if(ds.Tables[0].Rows[0]["IsManager"].ToString()!="")
				{
					model.IsManager=int.Parse(ds.Tables[0].Rows[0]["IsManager"].ToString());
				}
																																								model.PassWordS= ds.Tables[0].Rows[0]["PassWordS"].ToString();
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
			strSql.Append(" FROM U_Dic_PUser ");
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
			strSql.Append(" FROM U_Dic_PUser ");
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
		public DataSet GetList(ZhiFang.Model.PUser model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM U_Dic_PUser where 1=1 ");
			                                                                
             
            if(model.UserNo !=0)
                        {
                        strSql.Append(" and UserNo="+model.UserNo+" ");
                        }
                                                    
                        if(model.CName !=null)
                        {
                        strSql.Append(" and CName='"+model.CName+"' ");
                        }
                                                    
                        if(model.Password !=null)
                        {
                        strSql.Append(" and Password='"+model.Password+"' ");
                        }
                                                    
                        if(model.ShortCode !=null)
                        {
                        strSql.Append(" and ShortCode='"+model.ShortCode+"' ");
                        }
                                                    
                        if(model.Gender !=null)
                        {
                        strSql.Append(" and Gender="+model.Gender+" ");
                        }
                                                    
                        if(model.Birthday !=null)
                        {
                        strSql.Append(" and Birthday='"+model.Birthday+"' ");
                        }
                                                    
                        if(model.Role !=null)
                        {
                        strSql.Append(" and Role='"+model.Role+"' ");
                        }
                                                    
                        if(model.Resume !=null)
                        {
                        strSql.Append(" and Resume='"+model.Resume+"' ");
                        }
                                                                
                        if(model.DispOrder !=null)
                        {
                        strSql.Append(" and DispOrder="+model.DispOrder+" ");
                        }
                                                    
                        if(model.HisOrderCode !=null)
                        {
                        strSql.Append(" and HisOrderCode='"+model.HisOrderCode+"' ");
                        }
                                                    
                        if(model.userimage !=null)
                        {
                        strSql.Append(" and userimage='"+model.userimage+"' ");
                        }
                                                    
                        if(model.usertype !=null)
                        {
                        strSql.Append(" and usertype='"+model.usertype+"' ");
                        }
                                                    
                        if(model.DeptNo !=null)
                        {
                        strSql.Append(" and DeptNo="+model.DeptNo+" ");
                        }
                                                    
                        if(model.SectorTypeNo !=null)
                        {
                        strSql.Append(" and SectorTypeNo="+model.SectorTypeNo+" ");
                        }
                                                    
                        if(model.UserImeName !=null)
                        {
                        strSql.Append(" and UserImeName='"+model.UserImeName+"' ");
                        }
                                                    
                        if(model.IsManager !=null)
                        {
                        strSql.Append(" and IsManager="+model.IsManager+" ");
                        }
                                                    
                        if(model.PassWordS !=null)
                        {
                        strSql.Append(" and PassWordS='"+model.PassWordS+"' ");
                        }
                                                    
                        if(model.DTimeStampe !=null)
                        {
                        strSql.Append(" and DTimeStampe='"+model.DTimeStampe+"' ");
                        }
                                                                
                        if(model.StandCode !=null)
                        {
                        strSql.Append(" and StandCode='"+model.StandCode+"' ");
                        }
                                                    
                        if(model.ZFStandCode !=null)
                        {
                        strSql.Append(" and ZFStandCode='"+model.ZFStandCode+"' ");
                        }
                                                return idb.ExecuteDataSet(strSql.ToString());
		}
		
		/// <summary>
		/// 获取总记录
		/// </summary>
		public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM U_Dic_PUser ");
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
        public int GetTotalCount(ZhiFang.Model.PUser model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM U_Dic_PUser where 1=1 ");
           	                                          
            if(model.UserNo !=null)
            {
                        strSql.Append(" and UserNo="+model.UserNo+" ");
                        }
                                          
            if(model.CName !=null)
            {
                        strSql.Append(" and CName='"+model.CName+"' ");
                        }
                                          
            if(model.Password !=null)
            {
                        strSql.Append(" and Password='"+model.Password+"' ");
                        }
                                          
            if(model.ShortCode !=null)
            {
                        strSql.Append(" and ShortCode='"+model.ShortCode+"' ");
                        }
                                          
            if(model.Gender !=null)
            {
                        strSql.Append(" and Gender="+model.Gender+" ");
                        }
                                          
            if(model.Birthday !=null)
            {
                        strSql.Append(" and Birthday='"+model.Birthday+"' ");
                        }
                                          
            if(model.Role !=null)
            {
                        strSql.Append(" and Role='"+model.Role+"' ");
                        }
                                          
            if(model.Resume !=null)
            {
                        strSql.Append(" and Resume='"+model.Resume+"' ");
                        }
                                          
            if(model.Visible !=null)
            {
                        strSql.Append(" and Visible="+model.Visible+" ");
                        }
                                          
            if(model.DispOrder !=null)
            {
                        strSql.Append(" and DispOrder="+model.DispOrder+" ");
                        }
                                          
            if(model.HisOrderCode !=null)
            {
                        strSql.Append(" and HisOrderCode='"+model.HisOrderCode+"' ");
                        }
                                          
            if(model.userimage !=null)
            {
                        strSql.Append(" and userimage='"+model.userimage+"' ");
                        }
                                          
            if(model.usertype !=null)
            {
                        strSql.Append(" and usertype='"+model.usertype+"' ");
                        }
                                          
            if(model.DeptNo !=null)
            {
                        strSql.Append(" and DeptNo="+model.DeptNo+" ");
                        }
                                          
            if(model.SectorTypeNo !=null)
            {
                        strSql.Append(" and SectorTypeNo="+model.SectorTypeNo+" ");
                        }
                                          
            if(model.UserImeName !=null)
            {
                        strSql.Append(" and UserImeName='"+model.UserImeName+"' ");
                        }
                                          
            if(model.IsManager !=null)
            {
                        strSql.Append(" and IsManager="+model.IsManager+" ");
                        }
                                          
            if(model.PassWordS !=null)
            {
                        strSql.Append(" and PassWordS='"+model.PassWordS+"' ");
                        }
                                          
            if(model.DTimeStampe !=null)
            {
                        strSql.Append(" and DTimeStampe='"+model.DTimeStampe+"' ");
                        }
                                          
            if(model.AddTime !=null)
            {
                        strSql.Append(" and AddTime='"+model.AddTime+"' ");
                        }
                                          
            if(model.StandCode !=null)
            {
                        strSql.Append(" and StandCode='"+model.StandCode+"' ");
                        }
                                          
            if(model.ZFStandCode !=null)
            {
                        strSql.Append(" and ZFStandCode='"+model.ZFStandCode+"' ");
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
        
        /// <summary>
        /// 利用标识列分页
        /// </summary>
        /// <param name="model">model=null时为中心字典表分页；model!=null时为中心--实验室 对照关系分页</param>
        /// <param name="nowPageNum">第几页</param>
        /// <param name="nowPageSize">每页多少行</param>
        /// <returns>DataSet</returns>
        public DataSet GetListByPage(ZhiFang.Model.PUser model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            if (model != null && model.LabCode != null)
            {
                strSql.Append(" select top " + nowPageSize + "  * from U_Dic_PUser left join B_PUserControl on U_Dic_PUser.UserNo=B_PUserControl.UserNo ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and B_PUserControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append("where UserID not in ( ");
                strSql.Append("select top " + (nowPageSize * nowPageNum) + " UserID from  U_Dic_PUser left join B_PUserControl on U_Dic_PUser.UserNo=B_PUserControl.UserNo ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and B_PUserControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append("order by U_Dic_PUser.UserID ) order by U_Dic_PUser.UserID ");
                return idb.ExecuteDataSet(strSql.ToString());
            }
            else
            {
                strSql.Append("select top " + nowPageSize + "  * from U_Dic_PUser where UserID not in  ");
                strSql.Append("(select top " + (nowPageSize * nowPageNum) + " UserID from U_Dic_PUser order by UserID) order by UserID  ");
                return idb.ExecuteDataSet(strSql.ToString());
            }
        }
        
        public bool Exists(int UserNo)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from U_Dic_PUser ");
			strSql.Append(" where UserNo ='"+UserNo+"'");
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
		
		public bool CopyToLab(List<string> lst)
		{
			System.Collections.ArrayList arrySql = new System.Collections.ArrayList();
			string LabTableName="U_Dic_PUser";
			LabTableName="B_Lab_"+LabTableName.Substring(LabTableName.IndexOf("_") + 1, LabTableName.Length - (LabTableName.IndexOf("_") + 1));
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSqlControl = new StringBuilder();
            string TableKey="UserNo";
            string TableKeySub=TableKey;
            if(TableKey.ToLower().Contains("no"))
            {
            	TableKeySub=TableKey.Substring(0,TableKey.ToLower().IndexOf("no"));
            }
            try
            {
	            for (int i = 0; i < lst.Count; i++)
	            {
	                	strSql.Append("insert into "+LabTableName+"( LabCode,");			
	            		strSql.Append(" LabUserNo , CName , Password , ShortCode , Gender , Birthday , Role , Resume , Visible , DispOrder , HisOrderCode , userimage , usertype , DeptNo , SectorTypeNo , UserImeName , IsManager , PassWordS , StandCode , ZFStandCode , UseFlag ");
						strSql.Append(") select '" + lst[i].Trim() + "' as LabCode, ");
	            		strSql.Append("UserNo,CName,Password,ShortCode,Gender,Birthday,Role,Resume,Visible,DispOrder,HisOrderCode,userimage,usertype,DeptNo,SectorTypeNo,UserImeName,IsManager,PassWordS,StandCode,ZFStandCode,UseFlag");            
	            		strSql.Append(" from U_Dic_PUser ");    
	            		
	            		strSqlControl.Append("insert into B_PUserControl ( ");
	            		strSqlControl.Append(" "+TableKeySub+"ControlNo,"+TableKey+",ControlLabNo,Control"+TableKey+",UseFlag ");
	            		strSqlControl.Append(")  select ");
	            		strSqlControl.Append("  '" + lst[i].Trim() + "'+'_'+convert(varchar(50)," + TableKey + ")+'_'+convert(varchar(50), " + TableKey + ") as "+TableKeySub+"ControlNo,"+TableKey+",'" + lst[i].Trim() + "' as ControlLabNo,"+TableKey+",UseFlag ");
	            		strSqlControl.Append(" from U_Dic_PUser ");  
	            		
	            		arrySql.Add(strSql.ToString());
	            		arrySql.Add(strSqlControl.ToString());	    
	            		
	            		strSql = new StringBuilder();
             			strSqlControl = new StringBuilder();
             				             		
	             }
	             
	             idb.BatchUpdateWithTransaction(arrySql);
                 d_log.OperateLog("PUser", "", "", DateTime.Now, 1);
	             return true;
            }
            catch
            {
            	return false;
            }
           
		}
		
		public int GetMaxId()
        {
            return idb.GetMaxID("UserNo","U_Dic_PUser");
        }

        public DataSet GetList(int Top, ZhiFang.Model.PUser model, string filedOrder)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" * ");
			strSql.Append(" FROM U_Dic_PUser ");		
			
			                                          
            if(model.UserNo !=null)
            {
                        strSql.Append(" and UserNo="+model.UserNo+" ");
                        }
                                          
            if(model.CName !=null)
            {
                        
            strSql.Append(" and CName='"+model.CName+"' ");
                        }
                                          
            if(model.Password !=null)
            {
                        
            strSql.Append(" and Password='"+model.Password+"' ");
                        }
                                          
            if(model.ShortCode !=null)
            {
                        
            strSql.Append(" and ShortCode='"+model.ShortCode+"' ");
                        }
                                          
            if(model.Gender !=null)
            {
                        strSql.Append(" and Gender="+model.Gender+" ");
                        }
                                          
            if(model.Birthday !=null)
            {
                        
            strSql.Append(" and Birthday='"+model.Birthday+"' ");
                        }
                                          
            if(model.Role !=null)
            {
                        
            strSql.Append(" and Role='"+model.Role+"' ");
                        }
                                          
            if(model.Resume !=null)
            {
                        
            strSql.Append(" and Resume='"+model.Resume+"' ");
                        }
                                          
            if(model.Visible !=null)
            {
                        strSql.Append(" and Visible="+model.Visible+" ");
                        }
                                          
            if(model.DispOrder !=null)
            {
                        strSql.Append(" and DispOrder="+model.DispOrder+" ");
                        }
                                          
            if(model.HisOrderCode !=null)
            {
                        
            strSql.Append(" and HisOrderCode='"+model.HisOrderCode+"' ");
                        }
                                          
            if(model.userimage !=null)
            {
                        
            strSql.Append(" and userimage='"+model.userimage+"' ");
                        }
                                          
            if(model.usertype !=null)
            {
                        
            strSql.Append(" and usertype='"+model.usertype+"' ");
                        }
                                          
            if(model.DeptNo !=null)
            {
                        strSql.Append(" and DeptNo="+model.DeptNo+" ");
                        }
                                          
            if(model.SectorTypeNo !=null)
            {
                        strSql.Append(" and SectorTypeNo="+model.SectorTypeNo+" ");
                        }
                                          
            if(model.UserImeName !=null)
            {
                        
            strSql.Append(" and UserImeName='"+model.UserImeName+"' ");
                        }
                                          
            if(model.IsManager !=null)
            {
                        strSql.Append(" and IsManager="+model.IsManager+" ");
                        }
                                          
            if(model.PassWordS !=null)
            {
                        
            strSql.Append(" and PassWordS='"+model.PassWordS+"' ");
                        }
                                          
            if(model.DTimeStampe !=null)
            {
                        
            strSql.Append(" and DTimeStampe='"+model.DTimeStampe+"' ");
                        }
                                          
            if(model.AddTime !=null)
            {
                        
            strSql.Append(" and AddTime='"+model.AddTime+"' ");
                        }
                                          
            if(model.StandCode !=null)
            {
                        
            strSql.Append(" and StandCode='"+model.StandCode+"' ");
                        }
                                          
            if(model.ZFStandCode !=null)
            {
                        
            strSql.Append(" and ZFStandCode='"+model.ZFStandCode+"' ");
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

        #region IDGetListByTimeStampe 成员

        public DataSet GetListByTimeStampe(string LabCode, int dTimeStampe)
        {
            DataSet dsAll = new DataSet();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *,'" + LabCode + "' as LabCode,UserNo as LabUserNo from U_Dic_PUser where 1=1 ");
            if (dTimeStampe != -999999)
            {
                strSql.Append(" and convert(int,DTimeStampe) > '" + dTimeStampe + "' ");
            }
            DataTable dtServer = idb.ExecuteDataSet(strSql.ToString()).Tables[0];
            dtServer.TableName = "ServerDatas";

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("select *,LabUserNo as UserNo from U_Dic_Lab_PUser where 1=1 ");
            if (LabCode.Trim() != "")
            {
                strSql2.Append(" and LabCode= '" + LabCode + "' ");
            }
            if (dTimeStampe != -999999)
            {
                strSql2.Append(" and convert(int,DTimeStampe) > '" + dTimeStampe + "' ");
            }
            DataTable dtLab = idb.ExecuteDataSet(strSql2.ToString()).Tables[0];
            dtLab.TableName = "LabDatas";

            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("select * from U_Dic_PUserControl where 1=1 ");
            if (LabCode.Trim() != "")
            {
                strSql3.Append(" and ControlLabNo= '" + LabCode + "' ");
            }
            if (dTimeStampe != -999999)
            {
                strSql3.Append(" and convert(int,DTimeStampe) > '" + dTimeStampe + "' ");
            }
            DataTable dtControl = idb.ExecuteDataSet(strSql3.ToString()).Tables[0];
            dtControl.TableName = "ControlDatas";
            
            dsAll.Tables.Add(dtServer.Copy());
            dsAll.Tables.Add(dtLab.Copy());
            dsAll.Tables.Add(dtControl.Copy());
            return dsAll;
        }

        #endregion

        #region IDPUser 成员

        public bool Exists(int UserNo, string ShortCode)
        {
            throw new NotImplementedException();
        }

        public int Delete(int UserNo, string ShortCode)
        {
            throw new NotImplementedException();
        }

        public Model.PUser GetModel(int UserNo, string ShortCode)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDataBase<PUser> 成员
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
                        if (this.Exists(int.Parse(ds.Tables[0].Rows[i]["UserNo"].ToString().Trim())))
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
                strSql.Append("insert into U_Dic_PUserControl (");
                strSql.Append("UserNo,CName,Password,ShortCode,Gender,Birthday,Role,Resume,Visible,DispOrder,HisOrderCode,userimage,usertype,DeptNo,SectorTypeNo,UserImeName,IsManager,PassWordS,AddTime,StandCode,ZFStandCode,UseFlag");
                strSql.Append(") values (");
                strSql.Append("'" + dr["UserNo"].ToString().Trim() + "','" + dr["CName"].ToString().Trim() + "','" + dr["Password"].ToString().Trim() + "','" + dr["ShortCode"].ToString().Trim() + "','" + dr["Gender"].ToString().Trim() + "','" + dr["Birthday"].ToString().Trim() + "','" + dr["Role"].ToString().Trim() + "','" + dr["Resume"].ToString().Trim() + "','" + dr["Visible"].ToString().Trim() + "','" + dr["DispOrder"].ToString().Trim() + "','" + dr["HisOrderCode"].ToString().Trim() + "','" + dr["userimage"].ToString().Trim() + "','" + dr["usertype"].ToString().Trim() + "','" + dr["DeptNo"].ToString().Trim() + "','" + dr["SectorTypeNo"].ToString().Trim() + "','" + dr["UserImeName"].ToString().Trim() + "','" + dr["IsManager"].ToString().Trim() + "','" + dr["PassWordS"].ToString().Trim() + "','" + DateTime.Parse(dr["AddTime"].ToString().Trim()) + "','" + dr["StandCode"].ToString().Trim() + "','" + dr["ZFStandCode"].ToString().Trim() + "','" + dr["UseFlag"].ToString().Trim() + "'");
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
                strSql.Append("update U_Dic_PUserControl set ");

                strSql.Append(" CName = '" + dr["CName"].ToString().Trim() + "' , ");
                strSql.Append(" Password = '" + dr["Password"].ToString().Trim() + "' , ");
                strSql.Append(" ShortCode = '" + dr["ShortCode"].ToString().Trim() + "' , ");
                strSql.Append(" Gender = '" + dr["Gender"].ToString().Trim() + "' , ");
                strSql.Append(" Birthday = '" + dr["Birthday"].ToString().Trim() + "' , ");
                strSql.Append(" Role = '" + dr["Role"].ToString().Trim() + "' , ");
                strSql.Append(" Resume = '" + dr["Resume"].ToString().Trim() + "' , ");
                strSql.Append(" Visible = '" + dr["Visible"].ToString().Trim() + "' , ");
                strSql.Append(" DispOrder = '" + dr["DispOrder"].ToString().Trim() + "' , ");
                strSql.Append(" HisOrderCode = '" + dr["HisOrderCode"].ToString().Trim() + "' , ");
                strSql.Append(" userimage = '" + dr["userimage"].ToString().Trim() + "' , ");
                strSql.Append(" usertype = '" + dr["usertype"].ToString().Trim() + "' , ");
                strSql.Append(" DeptNo = '" + dr["DeptNo"].ToString().Trim() + "' , ");
                strSql.Append(" SectorTypeNo = '" + dr["SectorTypeNo"].ToString().Trim() + "' , ");
                strSql.Append(" UserImeName = '" + dr["UserImeName"].ToString().Trim() + "' , ");
                strSql.Append(" IsManager = '" + dr["IsManager"].ToString().Trim() + "' , ");
                strSql.Append(" PassWordS = '" + dr["PassWordS"].ToString().Trim() + "' , ");
                strSql.Append(" StandCode = '" + dr["StandCode"].ToString().Trim() + "' , ");
                strSql.Append(" ZFStandCode = '" + dr["ZFStandCode"].ToString().Trim() + "' , ");
                strSql.Append(" UseFlag = '" + dr["UseFlag"].ToString().Trim() + "'  ");
                strSql.Append(" where UserNo='" + dr["UserNo"].ToString().Trim() + "' ");

                return idb.ExecuteNonQuery(strSql.ToString());
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region IDBatchCopy 成员


        public int DeleteByDataRow(DataRow dr)
        {
            throw new NotImplementedException();
        }

        #endregion


        public bool IsExist(string labCodeNo)
        {
            throw new NotImplementedException();
        }

        public bool DeleteByLabCode(string LabCodeNo)
        {
            throw new NotImplementedException();
        }
    }
}

