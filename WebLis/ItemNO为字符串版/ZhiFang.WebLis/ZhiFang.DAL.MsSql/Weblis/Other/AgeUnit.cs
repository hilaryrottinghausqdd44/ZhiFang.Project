using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Weblis
{
	 	//AgeUnit
		
	public partial class AgeUnit : BaseDALLisDB,IDAgeUnit,IDBatchCopy
	{	
        public AgeUnit(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
		public AgeUnit()
		{
            DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
        }
		D_LogInfo d_log = new D_LogInfo();
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ZhiFang.Model.AgeUnit model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into AgeUnit(");			
            strSql.Append("AgeUnitNo,CName,ShortCode,Visible,DispOrder,HisOrderCode");
			strSql.Append(") values (");
            strSql.Append("@AgeUnitNo,@CName,@ShortCode,@Visible,@DispOrder,@HisOrderCode");            
            strSql.Append(") ");            
            
			SqlParameter[] parameters = {
			            new SqlParameter("@AgeUnitNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@CName", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@ShortCode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@Visible", SqlDbType.Int,4) ,            
                        new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            
                        new SqlParameter("@HisOrderCode", SqlDbType.VarChar,20)             
              
            };
			            
            parameters[0].Value = model.AgeUnitNo;                        
            parameters[1].Value = model.CName;                        
            parameters[2].Value = model.ShortCode;                        
            parameters[3].Value = model.Visible;                        
            parameters[4].Value = model.DispOrder;                        
            parameters[5].Value = model.HisOrderCode;                  		
			if (DbHelperSQL.ExecuteNonQuery(strSql.ToString(),parameters) > 0)
            {
            	return d_log.OperateLog("AgeUnit", "", "", DateTime.Now, 1);
            }
            else
                return -1;
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ZhiFang.Model.AgeUnit model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update AgeUnit set ");
			                        
            strSql.Append(" AgeUnitNo = @AgeUnitNo , ");                                    
            strSql.Append(" CName = @CName , ");                                    
            strSql.Append(" ShortCode = @ShortCode , ");                                    
            strSql.Append(" Visible = @Visible , ");                                    
            strSql.Append(" DispOrder = @DispOrder , ");                                    
            strSql.Append(" HisOrderCode = @HisOrderCode  ");            			
			strSql.Append(" where AgeUnitNo=@AgeUnitNo  ");
						
			SqlParameter[] parameters = {
			               
            new SqlParameter("@AgeUnitNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@CName", SqlDbType.VarChar,10) ,            	
                           
            new SqlParameter("@ShortCode", SqlDbType.VarChar,10) ,            	
                           
            new SqlParameter("@Visible", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@HisOrderCode", SqlDbType.VarChar,20)             	
              
            };
            			    
			   
			if(model.AgeUnitNo!=null)
			{
            	parameters[0].Value = model.AgeUnitNo;            	
            }
            	
                
			   
			if(model.CName!=null)
			{
            	parameters[1].Value = model.CName;            	
            }
            	
                
			   
			if(model.ShortCode!=null)
			{
            	parameters[2].Value = model.ShortCode;            	
            }
            	
                
			   
			if(model.Visible!=null)
			{
            	parameters[3].Value = model.Visible;            	
            }
            	
                
			   
			if(model.DispOrder!=null)
			{
            	parameters[4].Value = model.DispOrder;            	
            }
            	
                
			   
			if(model.HisOrderCode!=null)
			{
            	parameters[5].Value = model.HisOrderCode;            	
            }
            	
                       
			if (DbHelperSQL.ExecuteNonQuery(strSql.ToString(),parameters) > 0)
            {
               return d_log.OperateLog("AgeUnit", "", "", DateTime.Now, 1);
            }
            else
                return -1;
		}
		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int AgeUnitNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from AgeUnit ");
			strSql.Append(" where AgeUnitNo=@AgeUnitNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@AgeUnitNo", SqlDbType.Int,4)};
			parameters[0].Value = AgeUnitNo;


			return DbHelperSQL.ExecuteNonQuery(strSql.ToString(),parameters);
		
		}
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from AgeUnit ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
			return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
			
		}
		
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZhiFang.Model.AgeUnit GetModel(int AgeUnitNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select AgeUnitNo, CName, ShortCode, Visible, DispOrder, HisOrderCode  ");			
			strSql.Append("  from AgeUnit ");
			strSql.Append(" where AgeUnitNo=@AgeUnitNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@AgeUnitNo", SqlDbType.Int,4)};
			parameters[0].Value = AgeUnitNo;

			
			ZhiFang.Model.AgeUnit model=new ZhiFang.Model.AgeUnit();
			DataSet ds=DbHelperSQL.ExecuteDataSet(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																if(ds.Tables[0].Rows[0]["AgeUnitNo"].ToString()!="")
				{
					model.AgeUnitNo=int.Parse(ds.Tables[0].Rows[0]["AgeUnitNo"].ToString());
				}
																																								model.CName= ds.Tables[0].Rows[0]["CName"].ToString();
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
			strSql.Append(" FROM AgeUnit ");
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
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" * ");
			strSql.Append(" FROM AgeUnit ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.ExecuteDataSet(strSql.ToString());
		}
		
		/// <summary>
		/// 根据实体获取DataSet
		/// </summary>
		public DataSet GetList(ZhiFang.Model.AgeUnit model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM AgeUnit where 1=1 ");
			                            
             
            if(model.AgeUnitNo !=0)
                        {
                        strSql.Append(" and AgeUnitNo="+model.AgeUnitNo+" ");
                        }
                                                    
                        if(model.CName !=null)
                        {
                        strSql.Append(" and CName='"+model.CName+"' ");
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
            strSql.Append("select count(*) FROM AgeUnit ");
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
        public int GetTotalCount(ZhiFang.Model.AgeUnit model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM AgeUnit where 1=1 ");
           	                  
            if(model.AgeUnitNo !=null)
            {
                        strSql.Append(" and AgeUnitNo="+model.AgeUnitNo+" ");
                        }
                                          
            if(model.CName !=null)
            {
                        strSql.Append(" and CName='"+model.CName+"' ");
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
        public DataSet GetListByPage(ZhiFang.Model.AgeUnit model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            if (model != null && model.LabCode != null)
            {
                strSql.Append(" select top " + nowPageSize + "  * from AgeUnit left join AgeUnitControl on AgeUnit.AgeUnitNo=AgeUnitControl.AgeUnitNo ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and AgeUnitControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append("where AgeUnitNo not in ( ");
                strSql.Append("select top " + (nowPageSize * nowPageNum ) + " AgeUnitNo from  AgeUnit left join AgeUnitControl on AgeUnit.AgeUnitNo=AgeUnitControl.AgeUnitNo ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and AgeUnitControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append("order by AgeUnit.AgeUnitNo ) order by AgeUnit.AgeUnitNo ");
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
            else
            {
                strSql.Append("select top " + nowPageSize + "  * from AgeUnit where AgeUnitNo not in  ");
                strSql.Append("(select top " + (nowPageSize * nowPageNum ) + " AgeUnitNo from AgeUnit order by AgeUnitNo) order by AgeUnitNo  ");
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
        }
        
        public bool Exists(int AgeUnitNo)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from AgeUnit ");
			strSql.Append(" where AgeUnitNo ='"+AgeUnitNo+"'");
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
			string LabTableName="AgeUnit";
			LabTableName="B_Lab_"+LabTableName.Substring(LabTableName.IndexOf("_") + 1, LabTableName.Length - (LabTableName.IndexOf("_") + 1));
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSqlControl = new StringBuilder();
            string TableKey="AgeUnitNo";
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
	            		strSql.Append(" LabAgeUnitNo , CName , ShortCode , Visible , DispOrder , HisOrderCode ");
						strSql.Append(") select '" + lst[i].Trim() + "' as LabCode, ");
	            		strSql.Append("AgeUnitNo,CName,ShortCode,Visible,DispOrder,HisOrderCode");            
	            		strSql.Append(" from AgeUnit ");    
	            		
	            		strSqlControl.Append("insert into AgeUnitControl ( ");
	            		strSqlControl.Append(" "+TableKeySub+"ControlNo,"+TableKey+",ControlLabNo,Control"+TableKey+",UseFlag ");
	            		strSqlControl.Append(")  select ");
	            		strSqlControl.Append("  '" + lst[i].Trim() + "'+'_'+convert(varchar(50)," + TableKey + ")+'_'+convert(varchar(50), " + TableKey + ") as "+TableKeySub+"ControlNo,"+TableKey+",'" + lst[i].Trim() + "' as ControlLabNo,"+TableKey+",UseFlag ");
	            		strSqlControl.Append(" from AgeUnit ");  
	            		
	            		arrySql.Add(strSql.ToString());
	            		arrySql.Add(strSqlControl.ToString());	    
	            		
	            		strSql = new StringBuilder();
             			strSqlControl = new StringBuilder();
             			
	             }

                DbHelperSQL.BatchUpdateWithTransaction(arrySql);
                d_log.OperateLog("AgeUnit", "", "", DateTime.Now, 1);	             
	             return true;
            }
            catch
            {
            	return false;
            }
           
		}
		
		public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("AgeUnitNo","AgeUnit");
        }

        public DataSet GetList(int Top, ZhiFang.Model.AgeUnit model, string filedOrder)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" * ");
			strSql.Append(" FROM AgeUnit ");		
			
			                  
            if(model.AgeUnitNo !=null)
            {
                        strSql.Append(" and AgeUnitNo="+model.AgeUnitNo+" ");
                        }
                                          
            if(model.CName !=null)
            {
                        
            strSql.Append(" and CName='"+model.CName+"' ");
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
			            if (this.Exists(int.Parse(ds.Tables[0].Rows[i]["AgeUnitNo"].ToString().Trim())))
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
                strSql.Append("insert into AgeUnit (");
                strSql.Append("AgeUnitNo,CName,ShortCode,Visible,DispOrder,HisOrderCode");
                strSql.Append(") values (");
                if (dr.Table.Columns["AgeUnitNo"] != null && dr.Table.Columns["AgeUnitNo"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["AgeUnitNo"].ToString().Trim() + "', ");
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
                strSql.Append("update AgeUnit set ");


                if (dr.Table.Columns["CName"] != null && dr.Table.Columns["CName"].ToString().Trim() != "")
                {
                    strSql.Append(" CName = '" + dr["CName"].ToString().Trim() + "' , ");
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

                strSql.Append(" where AgeUnitNo='" + dr["AgeUnitNo"].ToString().Trim() + "' ");

                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch
            {
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

