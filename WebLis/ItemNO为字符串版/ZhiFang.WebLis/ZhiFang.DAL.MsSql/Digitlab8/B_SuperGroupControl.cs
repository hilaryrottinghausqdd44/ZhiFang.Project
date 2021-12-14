using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Digitlab8  
{
	 	//B_SuperGroupControl
		
	public partial class B_SuperGroupControl: IDSuperGroupControl	{	
		DBUtility.IDBConnection idb;
        public B_SuperGroupControl(string dbsourceconn)
        {
            idb = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
		public B_SuperGroupControl()
		{
            idb = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.LisDB());
        }
		D_LogInfo d_log = new D_LogInfo();
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ZhiFang.Model.SuperGroupControl model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into B_SuperGroupControl(");			
            strSql.Append("SuperGroupControlNo,SuperGroupNo,ControlLabNo,ControlSuperGroupNo,UseFlag");
			strSql.Append(") values (");
            strSql.Append("@SuperGroupControlNo,@SuperGroupNo,@ControlLabNo,@ControlSuperGroupNo,@UseFlag");            
            strSql.Append(") ");            
            
			SqlParameter[] parameters = {
			            new SqlParameter("@SuperGroupControlNo", SqlDbType.Char,50) ,            
                        new SqlParameter("@SuperGroupNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@ControlLabNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ControlSuperGroupNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@UseFlag", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.SuperGroupControlNo;                        
            parameters[1].Value = model.SuperGroupNo;                        
            parameters[2].Value = model.ControlLabNo;                        
            parameters[3].Value = model.ControlSuperGroupNo;                        
            parameters[4].Value = model.UseFlag;                  
			if (idb.ExecuteNonQuery(strSql.ToString(),parameters) > 0)
            {
                return d_log.OperateLog("SuperGroup", "", "", DateTime.Now, 1);
            }
            else
                return -1;
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ZhiFang.Model.SuperGroupControl model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update B_SuperGroupControl set ");
			                                                
            strSql.Append(" SuperGroupControlNo = @SuperGroupControlNo , ");                                    
            strSql.Append(" SuperGroupNo = @SuperGroupNo , ");                                    
            strSql.Append(" ControlLabNo = @ControlLabNo , ");                                    
            strSql.Append(" ControlSuperGroupNo = @ControlSuperGroupNo , ");                                                                                    
            strSql.Append(" UseFlag = @UseFlag  ");            			
			strSql.Append(" where SuperGroupControlNo=@SuperGroupControlNo  ");
						
			SqlParameter[] parameters = {
			            	
                           
            new SqlParameter("@SuperGroupControlNo", SqlDbType.Char,50) ,            	
                           
            new SqlParameter("@SuperGroupNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@ControlLabNo", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@ControlSuperGroupNo", SqlDbType.Int,4) ,            	
                        	
                        	
                           
            new SqlParameter("@UseFlag", SqlDbType.Int,4)             	
              
            };
            			    
				
                
			   
			if(model.SuperGroupControlNo!=null)
			{
            	parameters[0].Value = model.SuperGroupControlNo;            	
            }
            	
                
			   
			if(model.SuperGroupNo!=null)
			{
            	parameters[1].Value = model.SuperGroupNo;            	
            }
            	
                
			   
			if(model.ControlLabNo!=null)
			{
            	parameters[2].Value = model.ControlLabNo;            	
            }
            	
                
			   
			if(model.ControlSuperGroupNo!=null)
			{
            	parameters[3].Value = model.ControlSuperGroupNo;            	
            }
            	
                
				
                
				
                
			   
			if(model.UseFlag!=null)
			{
            	parameters[4].Value = model.UseFlag;            	
            }
            	
                        
			if (idb.ExecuteNonQuery(strSql.ToString(),parameters) > 0)
            {
                return d_log.OperateLog("SuperGroup", "", "", DateTime.Now, 1);
            }
            else
                return -1;
		}
		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(string SuperGroupControlNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from B_SuperGroupControl ");
			strSql.Append(" where SuperGroupControlNo=@SuperGroupControlNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@SuperGroupControlNo", SqlDbType.Char,50)};
			parameters[0].Value = SuperGroupControlNo;


			return idb.ExecuteNonQuery(strSql.ToString(),parameters);
			
		}
		
				/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteList(string Idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from B_SuperGroupControl ");
			strSql.Append(" where ID in ("+Idlist + ")  ");
			return idb.ExecuteNonQuery(strSql.ToString());
			
		}
				
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZhiFang.Model.SuperGroupControl GetModel(string SuperGroupControlNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Id, SuperGroupControlNo, SuperGroupNo, ControlLabNo, ControlSuperGroupNo, DTimeStampe, AddTime, UseFlag  ");			
			strSql.Append("  from B_SuperGroupControl ");
			strSql.Append(" where SuperGroupControlNo=@SuperGroupControlNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@SuperGroupControlNo", SqlDbType.Char,50)};
			parameters[0].Value = SuperGroupControlNo;

			
			ZhiFang.Model.SuperGroupControl model=new ZhiFang.Model.SuperGroupControl();
			DataSet ds=idb.ExecuteDataSet(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["Id"].ToString()!="")
				{
					model.Id=int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
				}
																																				model.SuperGroupControlNo= ds.Tables[0].Rows[0]["SuperGroupControlNo"].ToString();
																												if(ds.Tables[0].Rows[0]["SuperGroupNo"].ToString()!="")
				{
					model.SuperGroupNo=int.Parse(ds.Tables[0].Rows[0]["SuperGroupNo"].ToString());
				}
																																				model.ControlLabNo= ds.Tables[0].Rows[0]["ControlLabNo"].ToString();
																												if(ds.Tables[0].Rows[0]["ControlSuperGroupNo"].ToString()!="")
				{
					model.ControlSuperGroupNo=int.Parse(ds.Tables[0].Rows[0]["ControlSuperGroupNo"].ToString());
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
			strSql.Append(" FROM B_SuperGroupControl ");
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
			strSql.Append(" FROM B_SuperGroupControl ");
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
        public DataSet GetList(ZhiFang.Model.SuperGroupControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM B_SuperGroupControl where 1=1 ");

            if (model.SuperGroupControlNo != null)
            {
                strSql.Append(" and SuperGroupControlNo='" + model.SuperGroupControlNo + "' ");
            }

            if (model.SuperGroupNo != -1)
            {
                strSql.Append(" and SuperGroupNo=" + model.SuperGroupNo + " ");
            }

            if (model.ControlLabNo != null)
            {
                strSql.Append(" and ControlLabNo='" + model.ControlLabNo + "' ");
            }

            if (model.ControlSuperGroupNo != null)
            {
                strSql.Append(" and ControlSuperGroupNo=" + model.ControlSuperGroupNo + " ");
            }

            
            return idb.ExecuteDataSet(strSql.ToString());
        }
		
		/// <summary>
		/// 获取总记录
		/// </summary>
		public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_SuperGroupControl ");
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
        public int GetTotalCount(ZhiFang.Model.SuperGroupControl model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_SuperGroupControl where 1=1 ");
           	                                          
            if(model.SuperGroupControlNo !=null)
            {
                        strSql.Append(" and SuperGroupControlNo='"+model.SuperGroupControlNo+"' ");
                        }
                                          
            if(model.SuperGroupNo !=null)
            {
                        strSql.Append(" and SuperGroupNo="+model.SuperGroupNo+" ");
                        }
                                          
            if(model.ControlLabNo !=null)
            {
                        strSql.Append(" and ControlLabNo='"+model.ControlLabNo+"' ");
                        }
                                          
            if(model.ControlSuperGroupNo !=null)
            {
                        strSql.Append(" and ControlSuperGroupNo="+model.ControlSuperGroupNo+" ");
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
        
        
        public bool Exists(string SuperGroupControlNo)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from B_SuperGroupControl ");
			strSql.Append(" where SuperGroupControlNo ='"+SuperGroupControlNo+"'");
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

        public bool Exists(System.Collections.Hashtable ht)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from B_SuperGroupControl where 1=1 ");
            if (ht.Count > 0)
            {
                foreach (System.Collections.DictionaryEntry item in ht)
                {
                    strSql.Append(" and " + item.Key.ToString().Trim() + "='" + item.Value + "' ");
                }
                string strCount = idb.ExecuteScalar(strSql.ToString());
            if (strCount != null && strCount.Trim() != "" && strCount.Trim() != "0")
            {
                return true;
            }
            else
            {
                return false;
            }
            }
            else
            {
                return false;
            }
        }

		public int GetMaxId()
        {
            return idb.GetMaxID("SuperGroupControlNo","B_SuperGroupControl");
        }

        public DataSet GetList(int Top, ZhiFang.Model.SuperGroupControl model, string filedOrder)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" * ");
			strSql.Append(" FROM B_SuperGroupControl ");		
			
			                                          
            if(model.SuperGroupControlNo !=null)
            {
                        
            strSql.Append(" and SuperGroupControlNo='"+model.SuperGroupControlNo+"' ");
                        }
                                          
            if(model.SuperGroupNo !=null)
            {
                        strSql.Append(" and SuperGroupNo="+model.SuperGroupNo+" ");
                        }
                                          
            if(model.ControlLabNo !=null)
            {
                        
            strSql.Append(" and ControlLabNo='"+model.ControlLabNo+"' ");
                        }
                                          
            if(model.ControlSuperGroupNo !=null)
            {
                        strSql.Append(" and ControlSuperGroupNo="+model.ControlSuperGroupNo+" ");
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



        #region IDataBase<SuperGroupControl> 成员
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
                        if (this.Exists(ds.Tables[0].Rows[i]["SuperGroupControlNo"].ToString().Trim()))
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
                strSql.Append("insert into B_SuperGroupControl (");
                strSql.Append("SuperGroupControlNo,SuperGroupNo,ControlLabNo,ControlSuperGroupNo,AddTime,UseFlag");
                strSql.Append(") values (");
                strSql.Append("'" + dr["SuperGroupControlNo"].ToString().Trim() + "','" + dr["SuperGroupNo"].ToString().Trim() + "','" + dr["ControlLabNo"].ToString().Trim() + "','" + dr["ControlSuperGroupNo"].ToString().Trim() + "','" + DateTime.Parse(dr["AddTime"].ToString().Trim()) + "','" + dr["UseFlag"].ToString().Trim() + "'");
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
                strSql.Append("update B_SuperGroupControl set ");

                strSql.Append(" SuperGroupNo = '" + dr["SuperGroupNo"].ToString().Trim() + "' , ");
                strSql.Append(" ControlLabNo = '" + dr["ControlLabNo"].ToString().Trim() + "' , ");
                strSql.Append(" ControlSuperGroupNo = '" + dr["ControlSuperGroupNo"].ToString().Trim() + "' , ");
                strSql.Append(" UseFlag = '" + dr["UseFlag"].ToString().Trim() + "'  ");
                strSql.Append(" where SuperGroupControlNo='" + dr["SuperGroupControlNo"].ToString().Trim() + "' ");

                return idb.ExecuteNonQuery(strSql.ToString());
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region IDSuperGroupControl 成员


        public bool CheckIncludeLabCode(List<string> l, string LabCode)
        {
            throw new NotImplementedException();
        }

        public bool CheckIncludeCenterCode(List<string> l, string LabCode)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDSuperGroupControl 成员


        public DataSet GetLabCodeNo(string LabCode, List<string> CenterNoList)
        {
            throw new NotImplementedException();
        }

        public DataSet GetCenterNo(string LabCode, List<string> LabPrimaryNoList)
        {
            throw new NotImplementedException();
        }

        #endregion


        public DataSet GetListByPage(Model.SuperGroupControl model, int nowPageNum, int nowPageSize)
        {
            throw new NotImplementedException();
        }

        public DataSet B_lab_GetListByPage(Model.SuperGroupControl model, int nowPageNum, int nowPageSize)
        {
            throw new NotImplementedException();
        }
    }
}

