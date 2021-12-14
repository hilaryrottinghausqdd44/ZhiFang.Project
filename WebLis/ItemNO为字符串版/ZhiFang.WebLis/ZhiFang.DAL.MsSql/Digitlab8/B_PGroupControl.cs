using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Digitlab8  
{
	 	//B_PGroupControl
		
	public partial class B_PGroupControl: IDPGroupControl	{	
		DBUtility.IDBConnection idb;
        public B_PGroupControl(string dbsourceconn)
        {
            idb = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
		public B_PGroupControl()
		{
            idb = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.LisDB());
        }
		D_LogInfo d_log = new D_LogInfo();
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ZhiFang.Model.PGroupControl model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into B_PGroupControl(");			
            strSql.Append("SectionControlNo,SectionNo,ControlLabNo,ControlSectionNo,UseFlag");
			strSql.Append(") values (");
            strSql.Append("@SectionControlNo,@SectionNo,@ControlLabNo,@ControlSectionNo,@UseFlag");            
            strSql.Append(") ");            
            
			SqlParameter[] parameters = {
			            new SqlParameter("@SectionControlNo", SqlDbType.Char,50) ,            
                        new SqlParameter("@SectionNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@ControlLabNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ControlSectionNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@UseFlag", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.SectionControlNo;                        
            parameters[1].Value = model.SectionNo;                        
            parameters[2].Value = model.ControlLabNo;                        
            parameters[3].Value = model.ControlSectionNo;                        
            parameters[4].Value = model.UseFlag;                  
			if (idb.ExecuteNonQuery(strSql.ToString(),parameters) > 0)
            {
                return d_log.OperateLog("PGroup", "", "", DateTime.Now, 1);
            }
            else
                return -1;
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ZhiFang.Model.PGroupControl model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update B_PGroupControl set ");
			                                                
            strSql.Append(" SectionControlNo = @SectionControlNo , ");                                    
            strSql.Append(" SectionNo = @SectionNo , ");                                    
            strSql.Append(" ControlLabNo = @ControlLabNo , ");                                    
            strSql.Append(" ControlSectionNo = @ControlSectionNo , ");                                                                                    
            strSql.Append(" UseFlag = @UseFlag  ");            			
			strSql.Append(" where SectionControlNo=@SectionControlNo  ");
						
			SqlParameter[] parameters = {
			            	
                           
            new SqlParameter("@SectionControlNo", SqlDbType.Char,50) ,            	
                           
            new SqlParameter("@SectionNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@ControlLabNo", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@ControlSectionNo", SqlDbType.Int,4) ,            	
                        	
                        	
                           
            new SqlParameter("@UseFlag", SqlDbType.Int,4)             	
              
            };
            			    
				
                
			   
			if(model.SectionControlNo!=null)
			{
            	parameters[0].Value = model.SectionControlNo;            	
            }
            	
                
			   
			if(model.SectionNo!=null)
			{
            	parameters[1].Value = model.SectionNo;            	
            }
            	
                
			   
			if(model.ControlLabNo!=null)
			{
            	parameters[2].Value = model.ControlLabNo;            	
            }
            	
                
			   
			if(model.ControlSectionNo!=null)
			{
            	parameters[3].Value = model.ControlSectionNo;            	
            }
            	
                
				
                
				
                
			   
			if(model.UseFlag!=null)
			{
            	parameters[4].Value = model.UseFlag;            	
            }
            	
                        
			if (idb.ExecuteNonQuery(strSql.ToString(),parameters) > 0)
            {
                return d_log.OperateLog("PGroup", "", "", DateTime.Now, 1);
            }
            else
                return -1;
		}
		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(string SectionControlNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from B_PGroupControl ");
			strSql.Append(" where SectionControlNo=@SectionControlNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@SectionControlNo", SqlDbType.Char,50)};
			parameters[0].Value = SectionControlNo;


			return idb.ExecuteNonQuery(strSql.ToString(),parameters);
			
		}
		
				/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteList(string Idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from B_PGroupControl ");
			strSql.Append(" where ID in ("+Idlist + ")  ");
			return idb.ExecuteNonQuery(strSql.ToString());
			
		}
				
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZhiFang.Model.PGroupControl GetModel(string SectionControlNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Id, SectionControlNo, SectionNo, ControlLabNo, ControlSectionNo, DTimeStampe, AddTime, UseFlag  ");			
			strSql.Append("  from B_PGroupControl ");
			strSql.Append(" where SectionControlNo=@SectionControlNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@SectionControlNo", SqlDbType.Char,50)};
			parameters[0].Value = SectionControlNo;

			
			ZhiFang.Model.PGroupControl model=new ZhiFang.Model.PGroupControl();
			DataSet ds=idb.ExecuteDataSet(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["Id"].ToString()!="")
				{
					model.Id=int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
				}
																																				model.SectionControlNo= ds.Tables[0].Rows[0]["SectionControlNo"].ToString();
																												if(ds.Tables[0].Rows[0]["SectionNo"].ToString()!="")
				{
					model.SectionNo=int.Parse(ds.Tables[0].Rows[0]["SectionNo"].ToString());
				}
																																				model.ControlLabNo= ds.Tables[0].Rows[0]["ControlLabNo"].ToString();
																												if(ds.Tables[0].Rows[0]["ControlSectionNo"].ToString()!="")
				{
					model.ControlSectionNo=int.Parse(ds.Tables[0].Rows[0]["ControlSectionNo"].ToString());
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
			strSql.Append(" FROM B_PGroupControl ");
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
			strSql.Append(" FROM B_PGroupControl ");
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
		public DataSet GetList(ZhiFang.Model.PGroupControl model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM B_PGroupControl where 1=1 ");
			                                          
            if(model.SectionControlNo !=null)
            {
                        strSql.Append(" and SectionControlNo='"+model.SectionControlNo+"' ");
                        }
                                          
            if(model.SectionNo !=null)
            {
                        strSql.Append(" and SectionNo="+model.SectionNo+" ");
                        }
                                          
            if(model.ControlLabNo !=null)
            {
                        strSql.Append(" and ControlLabNo='"+model.ControlLabNo+"' ");
                        }
                                          
            if(model.ControlSectionNo !=null)
            {
                        strSql.Append(" and ControlSectionNo="+model.ControlSectionNo+" ");
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
            strSql.Append("select count(*) FROM B_PGroupControl ");
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
        public int GetTotalCount(ZhiFang.Model.PGroupControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_PGroupControl where 1=1 ");
           	                                          
            if(model.SectionControlNo !=null)
            {
                        strSql.Append(" and SectionControlNo='"+model.SectionControlNo+"' ");
                        }
                                          
            if(model.SectionNo !=null)
            {
                        strSql.Append(" and SectionNo="+model.SectionNo+" ");
                        }
                                          
            if(model.ControlLabNo !=null)
            {
                        strSql.Append(" and ControlLabNo='"+model.ControlLabNo+"' ");
                        }
                                          
            if(model.ControlSectionNo !=null)
            {
                        strSql.Append(" and ControlSectionNo="+model.ControlSectionNo+" ");
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
        
        
        public bool Exists(string SectionControlNo)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from B_PGroupControl ");
			strSql.Append(" where SectionControlNo ='"+SectionControlNo+"'");
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
            return idb.GetMaxID("SectionControlNo","B_PGroupControl");
        }

        public DataSet GetList(int Top, ZhiFang.Model.PGroupControl model, string filedOrder)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" * ");
			strSql.Append(" FROM B_PGroupControl ");		
			
			                                          
            if(model.SectionControlNo !=null)
            {
                        
            strSql.Append(" and SectionControlNo='"+model.SectionControlNo+"' ");
                        }
                                          
            if(model.SectionNo !=null)
            {
                        strSql.Append(" and SectionNo="+model.SectionNo+" ");
                        }
                                          
            if(model.ControlLabNo !=null)
            {
                        
            strSql.Append(" and ControlLabNo='"+model.ControlLabNo+"' ");
                        }
                                          
            if(model.ControlSectionNo !=null)
            {
                        strSql.Append(" and ControlSectionNo="+model.ControlSectionNo+" ");
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



        #region IDataBase<PGroupControl> 成员
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
                        if (this.Exists(ds.Tables[0].Rows[i]["SectionControlNo"].ToString().Trim()))
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
                strSql.Append("insert into B_PGroupControl (");
                strSql.Append("SectionControlNo,SectionNo,ControlLabNo,ControlSectionNo,AddTime,UseFlag");
                strSql.Append(") values (");
                strSql.Append("'" + dr["SectionControlNo"].ToString().Trim() + "','" + dr["SectionNo"].ToString().Trim() + "','" + dr["ControlLabNo"].ToString().Trim() + "','" + dr["ControlSectionNo"].ToString().Trim() + "','" + DateTime.Parse(dr["AddTime"].ToString().Trim()) + "','" + dr["UseFlag"].ToString().Trim() + "'");
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
                strSql.Append("update B_PGroupControl set ");

                strSql.Append(" SectionNo = '" + dr["SectionNo"].ToString().Trim() + "' , ");
                strSql.Append(" ControlLabNo = '" + dr["ControlLabNo"].ToString().Trim() + "' , ");
                strSql.Append(" ControlSectionNo = '" + dr["ControlSectionNo"].ToString().Trim() + "' , ");
                strSql.Append(" UseFlag = '" + dr["UseFlag"].ToString().Trim() + "'  ");
                strSql.Append(" where SectionControlNo='" + dr["SectionControlNo"].ToString().Trim() + "' ");

                return idb.ExecuteNonQuery(strSql.ToString());
            }
            catch
            {
                return 0;
            }
        }
        #endregion


        public DataSet GetListByPage(Model.PGroupControl model, int nowPageNum, int nowPageSize)
        {
            throw new NotImplementedException();
        }

        public DataSet B_lab_GetListByPage(Model.PGroupControl model, int nowPageNum, int nowPageSize)
        {
            throw new NotImplementedException();
        }
    }
}

