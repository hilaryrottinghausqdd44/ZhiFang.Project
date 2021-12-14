using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.Oracle.weblis
{
	 	//TestType
		
	public partial class TestType : BaseDALLisDB,IDTestType,IDBatchCopy
	{	
        public TestType(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
		public TestType()
		{
            DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
        }
		D_LogInfo d_log = new D_LogInfo();
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ZhiFang.Model.TestType model)
		{
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.TestTypeNo != null)
            {
                strSql1.Append("TestTypeNo,");
                strSql2.Append("" + model.TestTypeNo + ",");
            }
            if (model.CName != null)
            {
                strSql1.Append("CName,");
                strSql2.Append("'" + model.CName + "',");
            }
            if (model.TestTypeDesc != null)
            {
                strSql1.Append("TestTypeDesc,");
                strSql2.Append("'" + model.TestTypeDesc + "',");
            }
            if (model.ShortCode != null)
            {
                strSql1.Append("ShortCode,");
                strSql2.Append("'" + model.ShortCode + "',");
            }
            if (model.Visible != null)
            {
                strSql1.Append("Visible,");
                strSql2.Append("" + model.Visible + ",");
            }
            if (model.DispOrder != null)
            {
                strSql1.Append("DispOrder,");
                strSql2.Append("" + model.DispOrder + ",");
            }
            if (model.HisOrderCode != null)
            {
                strSql1.Append("HisOrderCode,");
                strSql2.Append("'" + model.HisOrderCode + "',");
            }
            strSql.Append("insert into TestType(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            if (DbHelperSQL.ExecuteNonQuery(strSql.ToString()) > 0)
            {
                return d_log.OperateLog("TestType", "", "", DateTime.Now, 1);
            }
            else
                return -1;

            //StringBuilder strSql=new StringBuilder();
            //strSql.Append("insert into TestType(");			
            //strSql.Append("TestTypeNo,CName,TestTypeDesc,ShortCode,Visible,DispOrder,HisOrderCode");
            //strSql.Append(") values (");
            //strSql.Append("@TestTypeNo,@CName,@TestTypeDesc,@ShortCode,@Visible,@DispOrder,@HisOrderCode");            
            //strSql.Append(") ");            
            
            //SqlParameter[] parameters = {
            //            new SqlParameter("@TestTypeNo", SqlDbType.Int,4) ,            
            //            new SqlParameter("@CName", SqlDbType.VarChar,20) ,            
            //            new SqlParameter("@TestTypeDesc", SqlDbType.VarChar,250) ,            
            //            new SqlParameter("@ShortCode", SqlDbType.VarChar,10) ,            
            //            new SqlParameter("@Visible", SqlDbType.Int,4) ,            
            //            new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            
            //            new SqlParameter("@HisOrderCode", SqlDbType.VarChar,20)             
              
            //};
			            
            //parameters[0].Value = model.TestTypeNo;                        
            //parameters[1].Value = model.CName;                        
            //parameters[2].Value = model.TestTypeDesc;                        
            //parameters[3].Value = model.ShortCode;                        
            //parameters[4].Value = model.Visible;                        
            //parameters[5].Value = model.DispOrder;                        
            //parameters[6].Value = model.HisOrderCode;                  		
            //if (DbHelperSQL.ExecuteNonQuery(strSql.ToString(),parameters) > 0)
            //{
            //    return d_log.OperateLog("TestType", "", "", DateTime.Now, 1);
            //}
            //else
            //    return -1;
		}		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ZhiFang.Model.TestType model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TestType set ");
            if (model.CName != null)
            {
                strSql.Append("CName='" + model.CName + "',");
            }
            else
            {
                strSql.Append("CName= null ,");
            }
            if (model.TestTypeDesc != null)
            {
                strSql.Append("TestTypeDesc='" + model.TestTypeDesc + "',");
            }
            else
            {
                strSql.Append("TestTypeDesc= null ,");
            }
            if (model.ShortCode != null)
            {
                strSql.Append("ShortCode='" + model.ShortCode + "',");
            }
            else
            {
                strSql.Append("ShortCode= null ,");
            }
            if (model.Visible != null)
            {
                strSql.Append("Visible=" + model.Visible + ",");
            }
            else
            {
                strSql.Append("Visible= null ,");
            }
            if (model.DispOrder != null)
            {
                strSql.Append("DispOrder=" + model.DispOrder + ",");
            }
            else
            {
                strSql.Append("DispOrder= null ,");
            }
            if (model.HisOrderCode != null)
            {
                strSql.Append("HisOrderCode='" + model.HisOrderCode + "',");
            }
            else
            {
                strSql.Append("HisOrderCode= null ,");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where TestTypeNo=" + model.TestTypeNo + " ");

            if (DbHelperSQL.ExecuteNonQuery(strSql.ToString()) > 0)
            {
                return d_log.OperateLog("TestType", "", "", DateTime.Now, 1);
            }
            else
                return -1;

            //StringBuilder strSql=new StringBuilder();
            //strSql.Append("update TestType set ");
			                        
            //strSql.Append(" TestTypeNo = @TestTypeNo , ");                                    
            //strSql.Append(" CName = @CName , ");                                    
            //strSql.Append(" TestTypeDesc = @TestTypeDesc , ");                                    
            //strSql.Append(" ShortCode = @ShortCode , ");                                    
            //strSql.Append(" Visible = @Visible , ");                                    
            //strSql.Append(" DispOrder = @DispOrder , ");                                    
            //strSql.Append(" HisOrderCode = @HisOrderCode  ");            			
            //strSql.Append(" where TestTypeNo=@TestTypeNo  ");
						
            //SqlParameter[] parameters = {
			               
            //new SqlParameter("@TestTypeNo", SqlDbType.Int,4) ,            	
                           
            //new SqlParameter("@CName", SqlDbType.VarChar,20) ,            	
                           
            //new SqlParameter("@TestTypeDesc", SqlDbType.VarChar,250) ,            	
                           
            //new SqlParameter("@ShortCode", SqlDbType.VarChar,10) ,            	
                           
            //new SqlParameter("@Visible", SqlDbType.Int,4) ,            	
                           
            //new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            	
                           
            //new SqlParameter("@HisOrderCode", SqlDbType.VarChar,20)             	
              
            //};
            			    
			   
            //if(model.TestTypeNo!=null)
            //{
            //    parameters[0].Value = model.TestTypeNo;            	
            //}
            	
                
			   
            //if(model.CName!=null)
            //{
            //    parameters[1].Value = model.CName;            	
            //}
            	
                
			   
            //if(model.TestTypeDesc!=null)
            //{
            //    parameters[2].Value = model.TestTypeDesc;            	
            //}
            	
                
			   
            //if(model.ShortCode!=null)
            //{
            //    parameters[3].Value = model.ShortCode;            	
            //}
            	
                
			   
            //if(model.Visible!=null)
            //{
            //    parameters[4].Value = model.Visible;            	
            //}
            	
                
			   
            //if(model.DispOrder!=null)
            //{
            //    parameters[5].Value = model.DispOrder;            	
            //}
            	
                
			   
            //if(model.HisOrderCode!=null)
            //{
            //    parameters[6].Value = model.HisOrderCode;            	
            //}
            	
                       
            //if (DbHelperSQL.ExecuteNonQuery(strSql.ToString(),parameters) > 0)
            //{
            //   return d_log.OperateLog("TestType", "", "", DateTime.Now, 1);
            //}
            //else
            //    return -1;
		}		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int TestTypeNo)
		{
			
            //StringBuilder strSql=new StringBuilder();
            //strSql.Append("delete from TestType ");
            //strSql.Append(" where TestTypeNo=@TestTypeNo ");
            //            SqlParameter[] parameters = {
            //        new SqlParameter("@TestTypeNo", SqlDbType.Int,4)};
            //parameters[0].Value = TestTypeNo;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TestType ");
            strSql.Append(" where TestTypeNo=" + TestTypeNo + " ");

			return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
		
		}
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from TestType ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
			return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
			
		}		
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZhiFang.Model.TestType GetModel(int TestTypeNo)
		{
			
            //StringBuilder strSql=new StringBuilder();
            //strSql.Append("select TestTypeNo, CName, TestTypeDesc, ShortCode, Visible, DispOrder, HisOrderCode  ");			
            //strSql.Append("  from TestType ");
            //strSql.Append(" where TestTypeNo=@TestTypeNo ");
            //            SqlParameter[] parameters = {
            //        new SqlParameter("@TestTypeNo", SqlDbType.Int,4)};
            //parameters[0].Value = TestTypeNo;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" TestTypeNo,CName,TestTypeDesc,ShortCode,Visible,DispOrder,HisOrderCode ");
            strSql.Append(" from TestType ");
            strSql.Append(" where TestTypeNo=" + TestTypeNo + " ");
			
			ZhiFang.Model.TestType model=new ZhiFang.Model.TestType();
			DataSet ds=DbHelperSQL.ExecuteDataSet(strSql.ToString());
			
			if(ds.Tables[0].Rows.Count>0)
			{
																if(ds.Tables[0].Rows[0]["TestTypeNo"].ToString()!="")
				{
					model.TestTypeNo=int.Parse(ds.Tables[0].Rows[0]["TestTypeNo"].ToString());
				}
																																								model.CName= ds.Tables[0].Rows[0]["CName"].ToString();
																																				model.TestTypeDesc= ds.Tables[0].Rows[0]["TestTypeDesc"].ToString();
																																				model.ShortCode= ds.Tables[0].Rows[0]["ShortCode"].ToString();
																																if(ds.Tables[0].Rows[0]["Visible"].ToString()!="")
				{
					model.Visible=int.Parse(ds.Tables[0].Rows[0]["Visible"].ToString());
				}
																																				if(ds.Tables[0].Rows[0]["DispOrder"].ToString()!="")
				{
					model.DispOrder=int.Parse(ds.Tables[0].Rows[0]["DispOrder"].ToString());
				}
																																								model.HisOrderCode= ds.Tables[0].Rows[0]["HisOrderCode"].ToString();
																										
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
			strSql.Append(" FROM TestType ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.ExecuteDataSet(strSql.ToString());
		}
		
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			strSql.Append(" * ");
			strSql.Append(" FROM TestType ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
                strSql.Append(" and ROWNUM <= '" + Top + "'");
            }
            else
            {
                strSql.Append(" where ROWNUM <= '" + Top + "'");
            }
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.ExecuteDataSet(strSql.ToString());
		}
		
		/// <summary>
		/// 根据实体获取DataSet
		/// </summary>
		public DataSet GetList(ZhiFang.Model.TestType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM TestType where 1=1 ");
			                            
             
            if(model.TestTypeNo !=0)
                        {
                        strSql.Append(" and TestTypeNo="+model.TestTypeNo+" ");
                        }
                                                    
                        if(model.CName !=null)
                        {
                        strSql.Append(" and CName='"+model.CName+"' ");
                        }
                                                    
                        if(model.TestTypeDesc !=null)
                        {
                        strSql.Append(" and TestTypeDesc='"+model.TestTypeDesc+"' ");
                        }
                                                    
                        if(model.ShortCode !=null)
                        {
                        strSql.Append(" and ShortCode='"+model.ShortCode+"' ");
                        }
                                                                
                        if(model.DispOrder !=null)
                        {
                        strSql.Append(" and DispOrder="+model.DispOrder+" ");
                        }
                                                    
                        if(model.HisOrderCode !=null)
                        {
                        strSql.Append(" and HisOrderCode='"+model.HisOrderCode+"' ");
                        }
                                    return DbHelperSQL.ExecuteDataSet(strSql.ToString());
		}
		
		/// <summary>
		/// 获取总记录
		/// </summary>
		public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM TestType ");
            string strCount = DbHelperSQL.ExecuteScalar(strSql.ToString());
            if (strCount != null && strCount.Trim() != "")
            {
                return Convert.ToInt32(strCount.Trim());
            }
            else
            {
                return 0;
            }
        }
        public int GetTotalCount(ZhiFang.Model.TestType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM TestType where 1=1 ");
           	                  
            if(model.TestTypeNo !=null)
            {
                        strSql.Append(" and TestTypeNo="+model.TestTypeNo+" ");
                        }
                                          
            if(model.CName !=null)
            {
                        strSql.Append(" and CName='"+model.CName+"' ");
                        }
                                          
            if(model.TestTypeDesc !=null)
            {
                        strSql.Append(" and TestTypeDesc='"+model.TestTypeDesc+"' ");
                        }
                                          
            if(model.ShortCode !=null)
            {
                        strSql.Append(" and ShortCode='"+model.ShortCode+"' ");
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
                                    string strCount = DbHelperSQL.ExecuteScalar(strSql.ToString());
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
        public DataSet GetListByPage(ZhiFang.Model.TestType model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            if (model != null && model.LabCode != null)
            {
                strSql.Append(" select  * from TestType left join TestTypeControl on TestType.TestTypeNo=TestTypeControl.TestTypeNo ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and TestTypeControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append("where  ROWNUM <= '" + nowPageSize + "' and TestTypeNo not in ( ");
                strSql.Append("select  TestTypeNo from  TestType left join TestTypeControl on TestType.TestTypeNo=TestTypeControl.TestTypeNo where 1=1 ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and TestTypeControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append(" and ROWNUM <= '" + (nowPageSize * nowPageNum) + "' ) order by TestType.TestTypeNo ");
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
            else
            {
                strSql.Append("select  * from TestType where  ROWNUM <= '" + nowPageSize + "' and TestTypeNo not in  ");
                strSql.Append("(select TestTypeNo from TestType where 1=1 and ROWNUM <= '" + (nowPageSize * nowPageNum) + "' ) order by TestTypeNo  ");
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
        }
        
        public bool Exists(int TestTypeNo)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from TestType ");
			strSql.Append(" where TestTypeNo ='"+TestTypeNo+"'");
			string strCount = DbHelperSQL.ExecuteScalar(strSql.ToString());
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
			string LabTableName="TestType";
			LabTableName="B_Lab_"+LabTableName.Substring(LabTableName.IndexOf("_") + 1, LabTableName.Length - (LabTableName.IndexOf("_") + 1));
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSqlControl = new StringBuilder();
            string TableKey="TestTypeNo";
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
	            		strSql.Append(" LabTestTypeNo , CName , TestTypeDesc , ShortCode , Visible , DispOrder , HisOrderCode ");
						strSql.Append(") select '" + lst[i].Trim() + "' as LabCode, ");
	            		strSql.Append("TestTypeNo,CName,TestTypeDesc,ShortCode,Visible,DispOrder,HisOrderCode");            
	            		strSql.Append(" from TestType ");    
	            		
	            		strSqlControl.Append("insert into TestTypeControl ( ");
	            		strSqlControl.Append(" "+TableKeySub+"ControlNo,"+TableKey+",ControlLabNo,Control"+TableKey+",UseFlag ");
	            		strSqlControl.Append(")  select ");
                        strSqlControl.Append("  concat(concat(concat(concat(" + lst[i].Trim() + ",'_')," + TableKey + "),'_')," + TableKey + ") as " + TableKeySub + "ControlNo," + TableKey + ",'" + lst[i].Trim() + "' as ControlLabNo," + TableKey + ",UseFlag ");
	            		strSqlControl.Append(" from TestType ");  
	            		
	            		arrySql.Add(strSql.ToString());
	            		arrySql.Add(strSqlControl.ToString());	    
	            		
	            		strSql = new StringBuilder();
             			strSqlControl = new StringBuilder();
	             }
	             
	             DbHelperSQL.BatchUpdateWithTransaction(arrySql);
                 d_log.OperateLog("TestType", "", "", DateTime.Now, 1);   
	             return true;
            }
            catch
            {
            	return false;
            }
           
		}
		
		public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("TestTypeNo","TestType");
        }

        public DataSet GetList(int Top, ZhiFang.Model.TestType model, string filedOrder)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			
			strSql.Append(" * ");
			strSql.Append(" FROM TestType ");
            strSql.Append(" where 1=1 ");
			                  
            if(model.TestTypeNo !=null)
            {
                        strSql.Append(" and TestTypeNo="+model.TestTypeNo+" ");
                        }
                                          
            if(model.CName !=null)
            {
                        
            strSql.Append(" and CName='"+model.CName+"' ");
                        }
                                          
            if(model.TestTypeDesc !=null)
            {
                        
            strSql.Append(" and TestTypeDesc='"+model.TestTypeDesc+"' ");
                        }
                                          
            if(model.ShortCode !=null)
            {
                        
            strSql.Append(" and ShortCode='"+model.ShortCode+"' ");
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
            strSql.Append(" and ROWNUM <= '" + Top + "'");                        
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetAllList()
        {
            return GetList("");
        }
        
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
			            if (this.Exists(int.Parse(ds.Tables[0].Rows[i]["TestTypeNo"].ToString().Trim())))
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
                strSql.Append("insert into TestType (");
                strSql.Append("TestTypeNo,CName,TestTypeDesc,ShortCode,Visible,DispOrder,HisOrderCode");
                strSql.Append(") values (");
                if (dr.Table.Columns["TestTypeNo"] != null && dr.Table.Columns["TestTypeNo"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["TestTypeNo"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["CName"] != null && dr.Table.Columns["CName"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["CName"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["TestTypeDesc"] != null && dr.Table.Columns["TestTypeDesc"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["TestTypeDesc"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["ShortCode"] != null && dr.Table.Columns["ShortCode"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["ShortCode"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["Visible"] != null && dr.Table.Columns["Visible"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["Visible"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["DispOrder"] != null && dr.Table.Columns["DispOrder"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["DispOrder"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["HisOrderCode"] != null && dr.Table.Columns["HisOrderCode"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["HisOrderCode"].ToString().Trim() + "' ");
                }
                else
                {
                    strSql.Append(" null ");
                }
                strSql.Append(") ");
                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch(Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("同步数据时错误：", ex);
                return 0;
            }
        }
        public int UpdateByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update TestType set ");


                if (dr.Table.Columns["CName"] != null && dr.Table.Columns["CName"].ToString().Trim() != "")
                {
                    strSql.Append(" CName = '" + dr["CName"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["TestTypeDesc"] != null && dr.Table.Columns["TestTypeDesc"].ToString().Trim() != "")
                {
                    strSql.Append(" TestTypeDesc = '" + dr["TestTypeDesc"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["ShortCode"] != null && dr.Table.Columns["ShortCode"].ToString().Trim() != "")
                {
                    strSql.Append(" ShortCode = '" + dr["ShortCode"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["Visible"] != null && dr.Table.Columns["Visible"].ToString().Trim() != "")
                {
                    strSql.Append(" Visible = '" + dr["Visible"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["DispOrder"] != null && dr.Table.Columns["DispOrder"].ToString().Trim() != "")
                {
                    strSql.Append(" DispOrder = '" + dr["DispOrder"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["HisOrderCode"] != null && dr.Table.Columns["HisOrderCode"].ToString().Trim() != "")
                {
                    strSql.Append(" HisOrderCode = '" + dr["HisOrderCode"].ToString().Trim() + "'  ");
                }

                strSql.Append(" where TestTypeNo='" + dr["TestTypeNo"].ToString().Trim() + "' ");

                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch(Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("同步数据时错误：", ex);
                return 0;
            }
        }

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

