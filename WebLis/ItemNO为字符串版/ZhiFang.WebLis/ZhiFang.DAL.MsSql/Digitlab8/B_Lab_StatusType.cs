using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Digitlab8  
{
	 	//B_Lab_StatusType
		
	public partial class B_Lab_StatusType: IDLab_StatusType	{	
		DBUtility.IDBConnection idb;
        public B_Lab_StatusType(string dbsourceconn)
        {
            idb = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
		public B_Lab_StatusType()
		{
            idb = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.LisDB());
        }
		D_LogInfo d_log = new D_LogInfo();
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ZhiFang.Model.Lab_StatusType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into B_Lab_StatusType(");			
            strSql.Append("LabCode,LabStatusNo,CName,StatusDesc,StatusColor,StandCode,ZFStandCode,UseFlag");
			strSql.Append(") values (");
            strSql.Append("@LabCode,@LabStatusNo,@CName,@StatusDesc,@StatusColor,@StandCode,@ZFStandCode,@UseFlag");            
            strSql.Append(") ");            
            
			SqlParameter[] parameters = {
			            new SqlParameter("@LabCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@LabStatusNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@CName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@StatusDesc", SqlDbType.VarChar,250) ,            
                        new SqlParameter("@StatusColor", SqlDbType.VarChar,15) ,            
                        new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ZFStandCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@UseFlag", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.LabCode;                        
            parameters[1].Value = model.LabStatusNo;                        
            parameters[2].Value = model.CName;                        
            parameters[3].Value = model.StatusDesc;                        
            parameters[4].Value = model.StatusColor;                        
            parameters[5].Value = model.StandCode;                        
            parameters[6].Value = model.ZFStandCode;                        
            parameters[7].Value = model.UseFlag;                  
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
		public int Update(ZhiFang.Model.Lab_StatusType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update B_Lab_StatusType set ");
			                                                
            strSql.Append(" LabCode = @LabCode , ");                                    
            strSql.Append(" LabStatusNo = @LabStatusNo , ");                                    
            strSql.Append(" CName = @CName , ");                                    
            strSql.Append(" StatusDesc = @StatusDesc , ");                                    
            strSql.Append(" StatusColor = @StatusColor , ");                                                                                    
            strSql.Append(" StandCode = @StandCode , ");                                    
            strSql.Append(" ZFStandCode = @ZFStandCode , ");                                    
            strSql.Append(" UseFlag = @UseFlag  ");            			
			strSql.Append(" where LabCode=@LabCode and LabStatusNo=@LabStatusNo  ");
						
			SqlParameter[] parameters = {
			            	
                           
            new SqlParameter("@LabCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@LabStatusNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@CName", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@StatusDesc", SqlDbType.VarChar,250) ,            	
                           
            new SqlParameter("@StatusColor", SqlDbType.VarChar,15) ,            	
                        	
                        	
                           
            new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@ZFStandCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@UseFlag", SqlDbType.Int,4)             	
              
            };
            			    
				
                
			   
			if(model.LabCode!=null)
			{
            	parameters[0].Value = model.LabCode;            	
            }
            	
                
			   
			if(model.LabStatusNo!=null)
			{
            	parameters[1].Value = model.LabStatusNo;            	
            }
            	
                
			   
			if(model.CName!=null)
			{
            	parameters[2].Value = model.CName;            	
            }
            	
                
			   
			if(model.StatusDesc!=null)
			{
            	parameters[3].Value = model.StatusDesc;            	
            }
            	
                
			   
			if(model.StatusColor!=null)
			{
            	parameters[4].Value = model.StatusColor;            	
            }
            	
                
				
                
				
                
			   
			if(model.StandCode!=null)
			{
            	parameters[5].Value = model.StandCode;            	
            }
            	
                
			   
			if(model.ZFStandCode!=null)
			{
            	parameters[6].Value = model.ZFStandCode;            	
            }
            	
                
			   
			if(model.UseFlag!=null)
			{
            	parameters[7].Value = model.UseFlag;            	
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
		public int Delete(string LabCode,int LabStatusNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from B_Lab_StatusType ");
			strSql.Append(" where LabCode=@LabCode and LabStatusNo=@LabStatusNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@LabCode", SqlDbType.VarChar,50),
					new SqlParameter("@LabStatusNo", SqlDbType.Int,4)};
			parameters[0].Value = LabCode;
			parameters[1].Value = LabStatusNo;


			return idb.ExecuteNonQuery(strSql.ToString(),parameters);
			
		}
		
				/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteList(string StatusIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from B_Lab_StatusType ");
			strSql.Append(" where ID in ("+StatusIDlist + ")  ");
			return idb.ExecuteNonQuery(strSql.ToString());
			
		}
				
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZhiFang.Model.Lab_StatusType GetModel(string LabCode,int LabStatusNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select StatusID, LabCode, LabStatusNo, CName, StatusDesc, StatusColor, DTimeStampe, AddTime, StandCode, ZFStandCode, UseFlag  ");			
			strSql.Append("  from B_Lab_StatusType ");
			strSql.Append(" where LabCode=@LabCode and LabStatusNo=@LabStatusNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@LabCode", SqlDbType.VarChar,50),
					new SqlParameter("@LabStatusNo", SqlDbType.Int,4)};
			parameters[0].Value = LabCode;
			parameters[1].Value = LabStatusNo;

			
			ZhiFang.Model.Lab_StatusType model=new ZhiFang.Model.Lab_StatusType();
			DataSet ds=idb.ExecuteDataSet(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																if(ds.Tables[0].Rows[0]["StatusID"].ToString()!="")
				{
					model.StatusID=int.Parse(ds.Tables[0].Rows[0]["StatusID"].ToString());
				}
																																								model.LabCode= ds.Tables[0].Rows[0]["LabCode"].ToString();
																																if(ds.Tables[0].Rows[0]["LabStatusNo"].ToString()!="")
				{
					model.LabStatusNo=int.Parse(ds.Tables[0].Rows[0]["LabStatusNo"].ToString());
				}
																																								model.CName= ds.Tables[0].Rows[0]["CName"].ToString();
																																				model.StatusDesc= ds.Tables[0].Rows[0]["StatusDesc"].ToString();
																																				model.StatusColor= ds.Tables[0].Rows[0]["StatusColor"].ToString();
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
			strSql.Append(" FROM B_Lab_StatusType ");
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
			strSql.Append(" FROM B_Lab_StatusType ");
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
		public DataSet GetList(ZhiFang.Model.Lab_StatusType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM B_Lab_StatusType where 1=1 ");
			                                                                
                        if(model.LabCode !=null)
                        {
                        strSql.Append(" and LabCode='"+model.LabCode+"' ");
                        }
                                                    
             
            if(model.LabStatusNo !=0)
                        {
                        strSql.Append(" and LabStatusNo="+model.LabStatusNo+" ");
                        }
                                                    
                        if(model.CName !=null)
                        {
                        strSql.Append(" and CName='"+model.CName+"' ");
                        }
                                                    
                        if(model.StatusDesc !=null)
                        {
                        strSql.Append(" and StatusDesc='"+model.StatusDesc+"' ");
                        }
                                                    
                        if(model.StatusColor !=null)
                        {
                        strSql.Append(" and StatusColor='"+model.StatusColor+"' ");
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
        public DataSet GetListByLike(ZhiFang.Model.Lab_StatusType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ,'('+convert(varchar(100),LabStatusNo)+')'+CName as LabStatusNoAndName ");
            strSql.Append(" FROM B_Lab_StatusType where 1=1 ");
            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }
            if (model.CName != null)
            {
                strSql.Append(" and ( CName like '%" + model.CName + "%' ");
            }
            if (model.LabStatusNo != 0)
            {
                strSql.Append(" or LabStatusNo like '%" + model.LabStatusNo + "%' ");
            }

            if (strSql.ToString().IndexOf("like") >= 0)
                strSql.Append(" ) ");
            return idb.ExecuteDataSet(strSql.ToString());
        }
		
		/// <summary>
		/// 获取总记录
		/// </summary>
		public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_Lab_StatusType ");
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
        public int GetTotalCount(ZhiFang.Model.Lab_StatusType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_Lab_StatusType where 1=1 ");
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
        public DataSet GetListByPage(ZhiFang.Model.Lab_StatusType model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top " + nowPageSize + " * from B_Lab_StatusType where 1=1  ");
                                                
                                                                        
                  
                        if(model.LabCode !=null)
                        {
                        strSql.Append(" and LabCode='"+model.LabCode+"' ");
                        }
                                                                        if(model.LabStatusNo !=0)
            {
            	 strSql.Append(" and LabStatusNo="+model.LabStatusNo+" ");
            }
                                                            
                  
                        if(model.CName !=null)
                        {
                        strSql.Append(" and CName='"+model.CName+"' ");
                        }
                                                                        
                  
                        if(model.StatusDesc !=null)
                        {
                        strSql.Append(" and StatusDesc='"+model.StatusDesc+"' ");
                        }
                                                                        
                  
                        if(model.StatusColor !=null)
                        {
                        strSql.Append(" and StatusColor='"+model.StatusColor+"' ");
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
                                                            strSql.Append("and StatusID not in ");
            strSql.Append("(select top " + (nowPageSize * nowPageNum) + " StatusID from B_Lab_StatusType where 1=1  ");
            strSql.Append(" order by StatusID) order by StatusID  ");
            return idb.ExecuteDataSet(strSql.ToString());
        }
        
        public bool Exists(string LabCode,int LabStatusNo)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from B_Lab_StatusType ");
			strSql.Append(" where LabCode=@LabCode and LabStatusNo=@LabStatusNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@LabCode", SqlDbType.VarChar,50),
					new SqlParameter("@LabStatusNo", SqlDbType.Int,4)};
			parameters[0].Value = LabCode;
			parameters[1].Value = LabStatusNo;


			DataSet ds=idb.ExecuteDataSet(strSql.ToString(),parameters);
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0 && ds.Tables[0].Rows[0][0].ToString().Trim()!="0" )
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
            return idb.GetMaxID("LabCode,LabStatusNo","B_Lab_StatusType");
        }

        public DataSet GetList(int Top, ZhiFang.Model.Lab_StatusType model, string filedOrder)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" * ");
			strSql.Append(" FROM B_Lab_StatusType ");		
			
			                                          
            if(model.LabCode !=null)
            {
                        
            strSql.Append(" and LabCode='"+model.LabCode+"' ");
                        }
                                          
            if(model.LabStatusNo !=null)
            {
                        strSql.Append(" and LabStatusNo="+model.LabStatusNo+" ");
                        }
                                          
            if(model.CName !=null)
            {
                        
            strSql.Append(" and CName='"+model.CName+"' ");
                        }
                                          
            if(model.StatusDesc !=null)
            {
                        
            strSql.Append(" and StatusDesc='"+model.StatusDesc+"' ");
                        }
                                          
            if(model.StatusColor !=null)
            {
                        
            strSql.Append(" and StatusColor='"+model.StatusColor+"' ");
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



        #region IDataBase<Lab_StatusType> 成员
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
                        if (this.Exists(ds.Tables[0].Rows[i]["LabCode"].ToString().Trim(),int.Parse(ds.Tables[0].Rows[i]["LabStatusNo"].ToString().Trim())))
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
                strSql.Append("insert into B_Lab_StatusType (");
                strSql.Append("LabCode,LabStatusNo,CName,StatusDesc,StatusColor,AddTime,StandCode,ZFStandCode,UseFlag");
                strSql.Append(") values (");
                strSql.Append("'" + dr["LabCode"].ToString().Trim() + "','" + dr["LabStatusNo"].ToString().Trim() + "','" + dr["CName"].ToString().Trim() + "','" + dr["StatusDesc"].ToString().Trim() + "','" + dr["StatusColor"].ToString().Trim() + "','" + DateTime.Parse(dr["AddTime"].ToString().Trim()) + "','" + dr["StandCode"].ToString().Trim() + "','" + dr["ZFStandCode"].ToString().Trim() + "','" + dr["UseFlag"].ToString().Trim() + "'");
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
                strSql.Append("update B_Lab_StatusType set ");

                strSql.Append(" CName = '" + dr["CName"].ToString().Trim() + "' , ");
                strSql.Append(" StatusDesc = '" + dr["StatusDesc"].ToString().Trim() + "' , ");
                strSql.Append(" StatusColor = '" + dr["StatusColor"].ToString().Trim() + "' , ");
                strSql.Append(" StandCode = '" + dr["StandCode"].ToString().Trim() + "' , ");
                strSql.Append(" ZFStandCode = '" + dr["ZFStandCode"].ToString().Trim() + "' , ");
                strSql.Append(" UseFlag = '" + dr["UseFlag"].ToString().Trim() + "'  ");
                strSql.Append(" where LabCode='" + dr["LabCode"].ToString().Trim() + "' and LabStatusNo='" + dr["LabStatusNo"].ToString().Trim() + "' ");

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

