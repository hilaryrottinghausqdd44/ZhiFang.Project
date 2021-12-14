using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Digitlab8  
{
	 	//B_StatusType
		
	public partial class B_StatusType : IDStatusType,IDBatchCopy,IDGetListByTimeStampe
	{	
		DBUtility.IDBConnection idb;
        public B_StatusType(string dbsourceconn)
        {
            idb = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
		public B_StatusType()
		{
            idb = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.LisDB());
        }
		D_LogInfo d_log = new D_LogInfo();
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ZhiFang.Model.StatusType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into B_StatusType(");			
            strSql.Append("StatusNo,CName,StatusDesc,StatusColor,StandCode,ZFStandCode,UseFlag");
			strSql.Append(") values (");
            strSql.Append("@StatusNo,@CName,@StatusDesc,@StatusColor,@StandCode,@ZFStandCode,@UseFlag");            
            strSql.Append(") ");            
            
			SqlParameter[] parameters = {
			            new SqlParameter("@StatusNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@CName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@StatusDesc", SqlDbType.VarChar,250) ,            
                        new SqlParameter("@StatusColor", SqlDbType.VarChar,15) ,            
                        new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ZFStandCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@UseFlag", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.StatusNo;                        
            parameters[1].Value = model.CName;                        
            parameters[2].Value = model.StatusDesc;                        
            parameters[3].Value = model.StatusColor;                        
            parameters[4].Value = model.StandCode;                        
            parameters[5].Value = model.ZFStandCode;                        
            parameters[6].Value = model.UseFlag;                  
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
		public int Update(ZhiFang.Model.StatusType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update B_StatusType set ");
			                                                
            strSql.Append(" StatusNo = @StatusNo , ");                                    
            strSql.Append(" CName = @CName , ");                                    
            strSql.Append(" StatusDesc = @StatusDesc , ");                                    
            strSql.Append(" StatusColor = @StatusColor , ");                                                                                    
            strSql.Append(" StandCode = @StandCode , ");                                    
            strSql.Append(" ZFStandCode = @ZFStandCode , ");                                    
            strSql.Append(" UseFlag = @UseFlag  ");            			
			strSql.Append(" where StatusNo=@StatusNo  ");
						
			SqlParameter[] parameters = {
			            	
                           
            new SqlParameter("@StatusNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@CName", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@StatusDesc", SqlDbType.VarChar,250) ,            	
                           
            new SqlParameter("@StatusColor", SqlDbType.VarChar,15) ,            	
                        	
                        	
                           
            new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@ZFStandCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@UseFlag", SqlDbType.Int,4)             	
              
            };
            			    
				
                
			   
			if(model.StatusNo!=null)
			{
            	parameters[0].Value = model.StatusNo;            	
            }
            	
                
			   
			if(model.CName!=null)
			{
            	parameters[1].Value = model.CName;            	
            }
            	
                
			   
			if(model.StatusDesc!=null)
			{
            	parameters[2].Value = model.StatusDesc;            	
            }
            	
                
			   
			if(model.StatusColor!=null)
			{
            	parameters[3].Value = model.StatusColor;            	
            }
            	
                
				
                
				
                
			   
			if(model.StandCode!=null)
			{
            	parameters[4].Value = model.StandCode;            	
            }
            	
                
			   
			if(model.ZFStandCode!=null)
			{
            	parameters[5].Value = model.ZFStandCode;            	
            }
            	
                
			   
			if(model.UseFlag!=null)
			{
            	parameters[6].Value = model.UseFlag;            	
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
		public int Delete(int StatusNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from B_StatusType ");
			strSql.Append(" where StatusNo=@StatusNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@StatusNo", SqlDbType.Int,4)};
			parameters[0].Value = StatusNo;


			return idb.ExecuteNonQuery(strSql.ToString(),parameters);
		
		}
		
				/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteList(string StatusIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from B_StatusType ");
			strSql.Append(" where ID in ("+StatusIDlist + ")  ");
			return idb.ExecuteNonQuery(strSql.ToString());
			
		}
				
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZhiFang.Model.StatusType GetModel(int StatusNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select StatusID, StatusNo, CName, StatusDesc, StatusColor, DTimeStampe, AddTime, StandCode, ZFStandCode, UseFlag  ");			
			strSql.Append("  from B_StatusType ");
			strSql.Append(" where StatusNo=@StatusNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@StatusNo", SqlDbType.Int,4)};
			parameters[0].Value = StatusNo;

			
			ZhiFang.Model.StatusType model=new ZhiFang.Model.StatusType();
			DataSet ds=idb.ExecuteDataSet(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																if(ds.Tables[0].Rows[0]["StatusID"].ToString()!="")
				{
					model.StatusID=int.Parse(ds.Tables[0].Rows[0]["StatusID"].ToString());
				}
																																				if(ds.Tables[0].Rows[0]["StatusNo"].ToString()!="")
				{
					model.StatusNo=int.Parse(ds.Tables[0].Rows[0]["StatusNo"].ToString());
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
			strSql.Append(" FROM B_StatusType ");
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
			strSql.Append(" FROM B_StatusType ");
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
		public DataSet GetList(ZhiFang.Model.StatusType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM B_StatusType where 1=1 ");
			                                                                
             
            if(model.StatusNo !=0)
                        {
                        strSql.Append(" and StatusNo="+model.StatusNo+" ");
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
		
		/// <summary>
		/// 获取总记录
		/// </summary>
		public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_StatusType ");
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
        public int GetTotalCount(ZhiFang.Model.StatusType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_StatusType where 1=1 ");
           	                                          
            if(model.StatusNo !=null)
            {
                        strSql.Append(" and StatusNo="+model.StatusNo+" ");
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
        public DataSet GetListByPage(ZhiFang.Model.StatusType model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            if (model != null && model.LabCode != null)
            {
                strSql.Append(" select top " + nowPageSize + "  * from B_StatusType left join B_StatusTypeControl on B_StatusType.StatusNo=B_StatusTypeControl.StatusNo ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and B_StatusTypeControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append("where StatusID not in ( ");
                strSql.Append("select top " + (nowPageSize * nowPageNum) + " StatusID from  B_StatusType left join B_StatusTypeControl on B_StatusType.StatusNo=B_StatusTypeControl.StatusNo ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and B_StatusTypeControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append("order by B_StatusType.StatusID ) order by B_StatusType.StatusID ");
                return idb.ExecuteDataSet(strSql.ToString());
            }
            else
            {
                strSql.Append("select top " + nowPageSize + "  * from B_StatusType where StatusID not in  ");
                strSql.Append("(select top " + (nowPageSize * nowPageNum) + " StatusID from B_StatusType order by StatusID) order by StatusID  ");
                return idb.ExecuteDataSet(strSql.ToString());
            }
        }
        
        public bool Exists(int StatusNo)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from B_StatusType ");
			strSql.Append(" where StatusNo ='"+StatusNo+"'");
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
			string LabTableName="B_StatusType";
			LabTableName="B_Lab_"+LabTableName.Substring(LabTableName.IndexOf("_") + 1, LabTableName.Length - (LabTableName.IndexOf("_") + 1));
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSqlControl = new StringBuilder();
            string TableKey="StatusNo";
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
	            		strSql.Append(" LabStatusNo , CName , StatusDesc , StatusColor , StandCode , ZFStandCode , UseFlag ");
						strSql.Append(") select '" + lst[i].Trim() + "' as LabCode, ");
	            		strSql.Append("StatusNo,CName,StatusDesc,StatusColor,StandCode,ZFStandCode,UseFlag");            
	            		strSql.Append(" from B_StatusType ");    
	            		
	            		strSqlControl.Append("insert into B_StatusTypeControl ( ");
	            		strSqlControl.Append(" "+TableKeySub+"ControlNo,"+TableKey+",ControlLabNo,Control"+TableKey+",UseFlag ");
	            		strSqlControl.Append(")  select ");
	            		strSqlControl.Append("  '" + lst[i].Trim() + "'+'_'+convert(varchar(50)," + TableKey + ")+'_'+convert(varchar(50), " + TableKey + ") as "+TableKeySub+"ControlNo,"+TableKey+",'" + lst[i].Trim() + "' as ControlLabNo,"+TableKey+",UseFlag ");
	            		strSqlControl.Append(" from B_StatusType ");  
	            		
	            		arrySql.Add(strSql.ToString());
	            		arrySql.Add(strSqlControl.ToString());	    
	            		
	            		strSql = new StringBuilder();
             			strSqlControl = new StringBuilder();
             			
	             }
	             
	             idb.BatchUpdateWithTransaction(arrySql);
                 d_log.OperateLog("StatusType", "", "", DateTime.Now, 1);
	             return true;
            }
            catch
            {
            	return false;
            }
           
		}
		
		public int GetMaxId()
        {
            return idb.GetMaxID("StatusNo","B_StatusType");
        }

        public DataSet GetList(int Top, ZhiFang.Model.StatusType model, string filedOrder)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" * ");
			strSql.Append(" FROM B_StatusType ");		
			
			                                          
            if(model.StatusNo !=null)
            {
                        strSql.Append(" and StatusNo="+model.StatusNo+" ");
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

        #region IDGetListByTimeStampe 成员

        public DataSet GetListByTimeStampe(string LabCode, int dTimeStampe)
        {
            DataSet dsAll = new DataSet();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *,'" + LabCode + "' as LabCode,StatusNo as LabStatusNo from B_StatusType where 1=1 ");
            if (dTimeStampe != -999999)
            {
                strSql.Append(" and convert(int,DTimeStampe) > '" + dTimeStampe + "' ");
            }
            DataTable dtServer = idb.ExecuteDataSet(strSql.ToString()).Tables[0];
            dtServer.TableName = "ServerDatas";

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("select *,LabStatusNo as StatusNo from B_Lab_StatusType where 1=1 ");
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
            strSql3.Append("select * from B_StatusTypeControl where 1=1 ");
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


        #region IDataBase<StatusType> 成员
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
                        if (this.Exists(int.Parse(ds.Tables[0].Rows[i]["StatusNo"].ToString().Trim())))
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
                strSql.Append("insert into B_StatusType (");
                strSql.Append("StatusNo,CName,StatusDesc,StatusColor,AddTime,StandCode,ZFStandCode,UseFlag");
                strSql.Append(") values (");
                strSql.Append("'" + dr["StatusNo"].ToString().Trim() + "','" + dr["CName"].ToString().Trim() + "','" + dr["StatusDesc"].ToString().Trim() + "','" + dr["StatusColor"].ToString().Trim() + "','" + DateTime.Parse(dr["AddTime"].ToString().Trim()) + "','" + dr["StandCode"].ToString().Trim() + "','" + dr["ZFStandCode"].ToString().Trim() + "','" + dr["UseFlag"].ToString().Trim() + "'");
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
                strSql.Append("update B_StatusType set ");

                strSql.Append(" CName = '" + dr["CName"].ToString().Trim() + "' , ");
                strSql.Append(" StatusDesc = '" + dr["StatusDesc"].ToString().Trim() + "' , ");
                strSql.Append(" StatusColor = '" + dr["StatusColor"].ToString().Trim() + "' , ");
                strSql.Append(" StandCode = '" + dr["StandCode"].ToString().Trim() + "' , ");
                strSql.Append(" ZFStandCode = '" + dr["ZFStandCode"].ToString().Trim() + "' , ");
                strSql.Append(" UseFlag = '" + dr["UseFlag"].ToString().Trim() + "'  ");
                strSql.Append(" where StatusNo='" + dr["StatusNo"].ToString().Trim() + "' ");

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

