using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Digitlab8  
{
	 	//B_FolkTypeControl
		
	public partial class B_FolkTypeControl: IDFolkTypeControl	{	
		DBUtility.IDBConnection idb;
        public B_FolkTypeControl(string dbsourceconn)
        {
            idb = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
		public B_FolkTypeControl()
		{
            idb = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.LisDB());
        }
		D_LogInfo d_log = new D_LogInfo();
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ZhiFang.Model.FolkTypeControl model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into B_FolkTypeControl(");			
            strSql.Append("FolkControlNo,FolkNo,ControlLabNo,ControlFolkNo,UseFlag");
			strSql.Append(") values (");
            strSql.Append("@FolkControlNo,@FolkNo,@ControlLabNo,@ControlFolkNo,@UseFlag");            
            strSql.Append(") ");            
            
			SqlParameter[] parameters = {
			            new SqlParameter("@FolkControlNo", SqlDbType.Char,50) ,            
                        new SqlParameter("@FolkNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@ControlLabNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ControlFolkNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@UseFlag", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.FolkControlNo;                        
            parameters[1].Value = model.FolkNo;                        
            parameters[2].Value = model.ControlLabNo;                        
            parameters[3].Value = model.ControlFolkNo;                        
            parameters[4].Value = model.UseFlag;                  
			if (idb.ExecuteNonQuery(strSql.ToString(),parameters) > 0)
            {
                return d_log.OperateLog("FolkType", "", "", DateTime.Now, 1);
            }
            else
                return -1;
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ZhiFang.Model.FolkTypeControl model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update B_FolkTypeControl set ");
			                                                
            strSql.Append(" FolkControlNo = @FolkControlNo , ");                                    
            strSql.Append(" FolkNo = @FolkNo , ");                                    
            strSql.Append(" ControlLabNo = @ControlLabNo , ");                                    
            strSql.Append(" ControlFolkNo = @ControlFolkNo , ");                                                                                    
            strSql.Append(" UseFlag = @UseFlag  ");            			
			strSql.Append(" where FolkControlNo=@FolkControlNo  ");
						
			SqlParameter[] parameters = {
			            	
                           
            new SqlParameter("@FolkControlNo", SqlDbType.Char,50) ,            	
                           
            new SqlParameter("@FolkNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@ControlLabNo", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@ControlFolkNo", SqlDbType.Int,4) ,            	
                        	
                        	
                           
            new SqlParameter("@UseFlag", SqlDbType.Int,4)             	
              
            };
            			    
				
                
			   
			if(model.FolkControlNo!=null)
			{
            	parameters[0].Value = model.FolkControlNo;            	
            }
            	
                
			   
			if(model.FolkNo!=null)
			{
            	parameters[1].Value = model.FolkNo;            	
            }
            	
                
			   
			if(model.ControlLabNo!=null)
			{
            	parameters[2].Value = model.ControlLabNo;            	
            }
            	
                
			   
			if(model.ControlFolkNo!=null)
			{
            	parameters[3].Value = model.ControlFolkNo;            	
            }
            	
                
				
                
				
                
			   
			if(model.UseFlag!=null)
			{
            	parameters[4].Value = model.UseFlag;            	
            }
            	
                        
			if (idb.ExecuteNonQuery(strSql.ToString(),parameters) > 0)
            {
                return d_log.OperateLog("FolkType", "", "", DateTime.Now, 1);
            }
            else
                return -1;
		}
		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(string FolkControlNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from B_FolkTypeControl ");
			strSql.Append(" where FolkControlNo=@FolkControlNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@FolkControlNo", SqlDbType.Char,50)};
			parameters[0].Value = FolkControlNo;


			return idb.ExecuteNonQuery(strSql.ToString(),parameters);
			
		}
		
				/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteList(string Idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from B_FolkTypeControl ");
			strSql.Append(" where ID in ("+Idlist + ")  ");
			return idb.ExecuteNonQuery(strSql.ToString());
			
		}
				
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZhiFang.Model.FolkTypeControl GetModel(string FolkControlNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Id, FolkControlNo, FolkNo, ControlLabNo, ControlFolkNo, DTimeStampe, AddTime, UseFlag  ");			
			strSql.Append("  from B_FolkTypeControl ");
			strSql.Append(" where FolkControlNo=@FolkControlNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@FolkControlNo", SqlDbType.Char,50)};
			parameters[0].Value = FolkControlNo;

			
			ZhiFang.Model.FolkTypeControl model=new ZhiFang.Model.FolkTypeControl();
			DataSet ds=idb.ExecuteDataSet(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["Id"].ToString()!="")
				{
					model.Id=int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
				}
																																				model.FolkControlNo= ds.Tables[0].Rows[0]["FolkControlNo"].ToString();
																												if(ds.Tables[0].Rows[0]["FolkNo"].ToString()!="")
				{
					model.FolkNo=int.Parse(ds.Tables[0].Rows[0]["FolkNo"].ToString());
				}
																																				model.ControlLabNo= ds.Tables[0].Rows[0]["ControlLabNo"].ToString();
																												if(ds.Tables[0].Rows[0]["ControlFolkNo"].ToString()!="")
				{
					model.ControlFolkNo=int.Parse(ds.Tables[0].Rows[0]["ControlFolkNo"].ToString());
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
			strSql.Append(" FROM B_FolkTypeControl ");
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
			strSql.Append(" FROM B_FolkTypeControl ");
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
		public DataSet GetList(ZhiFang.Model.FolkTypeControl model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM B_FolkTypeControl where 1=1 ");
			                                          
            if(model.FolkControlNo !=null)
            {
                        strSql.Append(" and FolkControlNo='"+model.FolkControlNo+"' ");
                        }
                                          
            if(model.FolkNo !=null)
            {
                        strSql.Append(" and FolkNo="+model.FolkNo+" ");
                        }
                                          
            if(model.ControlLabNo !=null)
            {
                        strSql.Append(" and ControlLabNo='"+model.ControlLabNo+"' ");
                        }
                                          
            if(model.ControlFolkNo !=null)
            {
                        strSql.Append(" and ControlFolkNo="+model.ControlFolkNo+" ");
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
            strSql.Append("select count(*) FROM B_FolkTypeControl ");
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
        public int GetTotalCount(ZhiFang.Model.FolkTypeControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_FolkTypeControl where 1=1 ");
           	                                          
            if(model.FolkControlNo !=null)
            {
                        strSql.Append(" and FolkControlNo='"+model.FolkControlNo+"' ");
                        }
                                          
            if(model.FolkNo !=null)
            {
                        strSql.Append(" and FolkNo="+model.FolkNo+" ");
                        }
                                          
            if(model.ControlLabNo !=null)
            {
                        strSql.Append(" and ControlLabNo='"+model.ControlLabNo+"' ");
                        }
                                          
            if(model.ControlFolkNo !=null)
            {
                        strSql.Append(" and ControlFolkNo="+model.ControlFolkNo+" ");
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
        
        
        public bool Exists(string FolkControlNo)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from B_FolkTypeControl ");
			strSql.Append(" where FolkControlNo ='"+FolkControlNo+"'");
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
            return idb.GetMaxID("FolkControlNo","B_FolkTypeControl");
        }

        public DataSet GetList(int Top, ZhiFang.Model.FolkTypeControl model, string filedOrder)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" * ");
			strSql.Append(" FROM B_FolkTypeControl ");		
			
			                                          
            if(model.FolkControlNo !=null)
            {
                        
            strSql.Append(" and FolkControlNo='"+model.FolkControlNo+"' ");
                        }
                                          
            if(model.FolkNo !=null)
            {
                        strSql.Append(" and FolkNo="+model.FolkNo+" ");
                        }
                                          
            if(model.ControlLabNo !=null)
            {
                        
            strSql.Append(" and ControlLabNo='"+model.ControlLabNo+"' ");
                        }
                                          
            if(model.ControlFolkNo !=null)
            {
                        strSql.Append(" and ControlFolkNo="+model.ControlFolkNo+" ");
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



        #region IDataBase<FolkTypeControl> 成员
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
                        if (this.Exists(ds.Tables[0].Rows[i]["FolkControlNo"].ToString().Trim()))
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
                strSql.Append("insert into B_FolkTypeControl (");
                strSql.Append("FolkControlNo,FolkNo,ControlLabNo,ControlFolkNo,AddTime,UseFlag");
                strSql.Append(") values (");
                strSql.Append("'" + dr["FolkControlNo"].ToString().Trim() + "','" + dr["FolkNo"].ToString().Trim() + "','" + dr["ControlLabNo"].ToString().Trim() + "','" + dr["ControlFolkNo"].ToString().Trim() + "','" + DateTime.Parse(dr["AddTime"].ToString().Trim()) + "','" + dr["UseFlag"].ToString().Trim() + "'");
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
                strSql.Append("update B_FolkTypeControl set ");

                strSql.Append(" FolkNo = '" + dr["FolkNo"].ToString().Trim() + "' , ");
                strSql.Append(" ControlLabNo = '" + dr["ControlLabNo"].ToString().Trim() + "' , ");
                strSql.Append(" ControlFolkNo = '" + dr["ControlFolkNo"].ToString().Trim() + "' , ");
                strSql.Append(" UseFlag = '" + dr["UseFlag"].ToString().Trim() + "'  ");
                strSql.Append(" where FolkControlNo='" + dr["FolkControlNo"].ToString().Trim() + "' ");

                return idb.ExecuteNonQuery(strSql.ToString());
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region IDFolkTypeControl 成员


        public bool CheckIncludeLabCode(List<string> l, string LabCode)
        {
            throw new NotImplementedException();
        }

        public bool CheckIncludeCenterCode(List<string> l, string LabCode)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDFolkTypeControl 成员


        public DataSet GetLabCodeNo(string LabCode, List<string> CenterNoList)
        {
            throw new NotImplementedException();
        }

        public DataSet GetCenterNo(string LabCode, List<string> LabPrimaryNoList)
        {
            throw new NotImplementedException();
        }

        #endregion


        public DataSet GetListByPage(Model.FolkTypeControl model, int nowPageNum, int nowPageSize)
        {
            throw new NotImplementedException();
        }

        public DataSet B_lab_GetListByPage(Model.FolkTypeControl model, int nowPageNum, int nowPageSize)
        {
            throw new NotImplementedException();
        }
    }
}

