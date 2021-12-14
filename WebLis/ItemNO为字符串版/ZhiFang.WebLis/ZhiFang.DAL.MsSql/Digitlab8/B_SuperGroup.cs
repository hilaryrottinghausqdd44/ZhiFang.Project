using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Digitlab8  
{
	 	//B_SuperGroup
		
	public partial class B_SuperGroup : IDSuperGroup,IDBatchCopy,IDGetListByTimeStampe
	{	
		DBUtility.IDBConnection idb;
        public B_SuperGroup(string dbsourceconn)
        {
            idb = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
		public B_SuperGroup()
		{
            idb = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.LisDB());
        }
		D_LogInfo d_log = new D_LogInfo();
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ZhiFang.Model.SuperGroup model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into B_SuperGroup(");			
            strSql.Append("SuperGroupNo,CName,ShortName,ShortCode,Visible,DispOrder,StandCode,ZFStandCode,UseFlag");
			strSql.Append(") values (");
            strSql.Append("@SuperGroupNo,@CName,@ShortName,@ShortCode,@Visible,@DispOrder,@StandCode,@ZFStandCode,@UseFlag");            
            strSql.Append(") ");            
            
			SqlParameter[] parameters = {
			            new SqlParameter("@SuperGroupNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@CName", SqlDbType.VarChar,40) ,            
                        new SqlParameter("@ShortName", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@ShortCode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@Visible", SqlDbType.Int,4) ,            
                        new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            
                        new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ZFStandCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@UseFlag", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.SuperGroupNo;                        
            parameters[1].Value = model.CName;                        
            parameters[2].Value = model.ShortName;                        
            parameters[3].Value = model.ShortCode;                        
            parameters[4].Value = model.Visible;                        
            parameters[5].Value = model.DispOrder;                        
            parameters[6].Value = model.StandCode;                        
            parameters[7].Value = model.ZFStandCode;                        
            parameters[8].Value = model.UseFlag;                  
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
		public int Update(ZhiFang.Model.SuperGroup model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update B_SuperGroup set ");
			                                                
            strSql.Append(" SuperGroupNo = @SuperGroupNo , ");                                    
            strSql.Append(" CName = @CName , ");                                    
            strSql.Append(" ShortName = @ShortName , ");                                    
            strSql.Append(" ShortCode = @ShortCode , ");                                    
            strSql.Append(" Visible = @Visible , ");                                    
            strSql.Append(" DispOrder = @DispOrder , ");                                                                                    
            strSql.Append(" StandCode = @StandCode , ");                                    
            strSql.Append(" ZFStandCode = @ZFStandCode , ");                                    
            strSql.Append(" UseFlag = @UseFlag  ");            			
			strSql.Append(" where SuperGroupNo=@SuperGroupNo  ");
						
			SqlParameter[] parameters = {
			            	
                           
            new SqlParameter("@SuperGroupNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@CName", SqlDbType.VarChar,40) ,            	
                           
            new SqlParameter("@ShortName", SqlDbType.VarChar,10) ,            	
                           
            new SqlParameter("@ShortCode", SqlDbType.VarChar,10) ,            	
                           
            new SqlParameter("@Visible", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            	
                        	
                        	
                           
            new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@ZFStandCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@UseFlag", SqlDbType.Int,4)             	
              
            };
            			    
				
                
			   
			if(model.SuperGroupNo!=null)
			{
            	parameters[0].Value = model.SuperGroupNo;            	
            }
            	
                
			   
			if(model.CName!=null)
			{
            	parameters[1].Value = model.CName;            	
            }
            	
                
			   
			if(model.ShortName!=null)
			{
            	parameters[2].Value = model.ShortName;            	
            }
            	
                
			   
			if(model.ShortCode!=null)
			{
            	parameters[3].Value = model.ShortCode;            	
            }
            	
                
			   
			if(model.Visible!=null)
			{
            	parameters[4].Value = model.Visible;            	
            }
            	
                
			   
			if(model.DispOrder!=null)
			{
            	parameters[5].Value = model.DispOrder;            	
            }
            	
                
				
                
				
                
			   
			if(model.StandCode!=null)
			{
            	parameters[6].Value = model.StandCode;            	
            }
            	
                
			   
			if(model.ZFStandCode!=null)
			{
            	parameters[7].Value = model.ZFStandCode;            	
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
		public int Delete(int SuperGroupNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from B_SuperGroup ");
			strSql.Append(" where SuperGroupNo=@SuperGroupNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@SuperGroupNo", SqlDbType.Int,4)};
			parameters[0].Value = SuperGroupNo;


			return idb.ExecuteNonQuery(strSql.ToString(),parameters);
		
		}
		
				/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteList(string SuperGroupIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from B_SuperGroup ");
			strSql.Append(" where ID in ("+SuperGroupIDlist + ")  ");
			return idb.ExecuteNonQuery(strSql.ToString());
			
		}
				
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZhiFang.Model.SuperGroup GetModel(int SuperGroupNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select SuperGroupID, SuperGroupNo, CName, ShortName, ShortCode, Visible, DispOrder, DTimeStampe, AddTime, StandCode, ZFStandCode, UseFlag  ");			
			strSql.Append("  from B_SuperGroup ");
			strSql.Append(" where SuperGroupNo=@SuperGroupNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@SuperGroupNo", SqlDbType.Int,4)};
			parameters[0].Value = SuperGroupNo;

			
			ZhiFang.Model.SuperGroup model=new ZhiFang.Model.SuperGroup();
			DataSet ds=idb.ExecuteDataSet(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																if(ds.Tables[0].Rows[0]["SuperGroupID"].ToString()!="")
				{
					model.SuperGroupID=int.Parse(ds.Tables[0].Rows[0]["SuperGroupID"].ToString());
				}
																																				if(ds.Tables[0].Rows[0]["SuperGroupNo"].ToString()!="")
				{
					model.SuperGroupNo=int.Parse(ds.Tables[0].Rows[0]["SuperGroupNo"].ToString());
				}
																																								model.CName= ds.Tables[0].Rows[0]["CName"].ToString();
																																				model.ShortName= ds.Tables[0].Rows[0]["ShortName"].ToString();
																																				model.ShortCode= ds.Tables[0].Rows[0]["ShortCode"].ToString();
																																if(ds.Tables[0].Rows[0]["Visible"].ToString()!="")
				{
					model.Visible=int.Parse(ds.Tables[0].Rows[0]["Visible"].ToString());
				}
																																				if(ds.Tables[0].Rows[0]["DispOrder"].ToString()!="")
				{
					model.DispOrder=int.Parse(ds.Tables[0].Rows[0]["DispOrder"].ToString());
				}
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
			strSql.Append(" FROM B_SuperGroup ");
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
			strSql.Append(" FROM B_SuperGroup ");
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
		public DataSet GetList(ZhiFang.Model.SuperGroup model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM B_SuperGroup where 1=1 ");
			                                                                
             
            if(model.SuperGroupNo !=0)
                        {
                        strSql.Append(" and SuperGroupNo="+model.SuperGroupNo+" ");
                        }
                                                    
                        if(model.CName !=null)
                        {
                        strSql.Append(" and CName='"+model.CName+"' ");
                        }
                                                    
                        if(model.ShortName !=null)
                        {
                        strSql.Append(" and ShortName='"+model.ShortName+"' ");
                        }
                                                    
                        if(model.ShortCode !=null)
                        {
                        strSql.Append(" and ShortCode='"+model.ShortCode+"' ");
                        }
                                                                
                        if(model.DispOrder !=null)
                        {
                        strSql.Append(" and DispOrder="+model.DispOrder+" ");
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
            strSql.Append("select count(*) FROM B_SuperGroup ");
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
        public int GetTotalCount(ZhiFang.Model.SuperGroup model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            strSql.Append("select count(*) FROM B_SuperGroup where 1=1 ");
            if (model != null)
            {
                if (model.SuperGroupNo != -1)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or SuperGroupNo like '%" + model.SuperGroupNo + "%' ");
                    else
                        strWhere.Append(" and ( SuperGroupNo like '%" + model.SuperGroupNo + "%' ");
                }
                if (model.CName != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or CName like '%" + model.CName + "%' ");
                    else
                        strWhere.Append(" and ( CName like '%" + model.CName + "%' ");
                }
                if (model.ShortName != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or ShortName like '%" + model.ShortName + "%' ");
                    else
                        strWhere.Append(" and ( ShortName like '%" + model.ShortName + "%' ");
                }
                if (model.ShortCode != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or ShortCode like '%" + model.ShortCode + "%' ");
                    else
                        strWhere.Append(" and ( ShortCode like '%" + model.ShortCode + "%' ");
                }
                if (strWhere.Length > 0)
                    strWhere.Append(" ) ");
            }
            strSql.Append(strWhere.ToString());
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
        public DataSet GetListByPage(ZhiFang.Model.SuperGroup model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            if (model != null)
            {
                if (model.SuperGroupNo != -1)
                {
                    strWhere.Append(" and ( B_SuperGroup.SuperGroupNo like '%" + model.SuperGroupNo + "%'  ");
                }
                if (model.CName != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or B_SuperGroup.CName like '%" + model.CName + "%'  ");
                    else
                        strWhere.Append(" and ( B_SuperGroup.CName like '%" + model.CName + "%'  ");
                }
                if (model.ShortName != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or B_SuperGroup.ShortName like '%" + model.ShortName + "%'  ");
                    else
                        strWhere.Append(" and ( B_SuperGroup.ShortName like '%" + model.ShortName + "%'  ");
                }
                if (model.ShortCode != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or B_SuperGroup.ShortCode like '%" + model.ShortCode + "%'  ");
                    else
                        strWhere.Append(" and ( B_SuperGroup.ShortCode like '%" + model.ShortCode + "%'  ");
                }
                if (strWhere.Length > 0)
                    strWhere.Append(" ) ");
            }
            if (model != null && model.LabCode != null)
            {
                strSql.Append(" select top " + nowPageSize + "  * from B_SuperGroup left join B_SuperGroupControl on B_SuperGroup.SuperGroupNo=B_SuperGroupControl.SuperGroupNo ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and B_SuperGroupControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append("where SuperGroupID not in ( ");
                strSql.Append("select top " + (nowPageSize * nowPageNum) + " SuperGroupID from  B_SuperGroup left join B_SuperGroupControl on B_SuperGroup.SuperGroupNo=B_SuperGroupControl.SuperGroupNo ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and B_SuperGroupControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append(" " + strWhere.ToString() + " order by B_SuperGroup.SuperGroupID ) " + strWhere.ToString() + " order by B_SuperGroup.SuperGroupID ");
                //ZhiFang.Common.Log.Log.Info("B_SuperGroup------>" + strSql.ToString());
                return idb.ExecuteDataSet(strSql.ToString());
            }
            else
            {
                strSql.Append("select top " + nowPageSize + "  * from B_SuperGroup where SuperGroupID not in  ");
                strSql.Append("(select top " + (nowPageSize * nowPageNum) + " SuperGroupID from B_SuperGroup where 1=1 " + strWhere.ToString() + " order by " + model.OrderField + ") " + strWhere.ToString() + " order by " + model.OrderField + "  ");
                //ZhiFang.Common.Log.Log.Info("B_SuperGroup------>" + strSql.ToString());
                return idb.ExecuteDataSet(strSql.ToString());
            }
        }
        
        public bool Exists(int SuperGroupNo)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from B_SuperGroup ");
			strSql.Append(" where SuperGroupNo ='"+SuperGroupNo+"'");
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
            strSql.Append("select count(1) from B_SuperGroup where 1=1 ");
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

		public bool CopyToLab(List<string> lst)
		{
			System.Collections.ArrayList arrySql = new System.Collections.ArrayList();
			string LabTableName="B_SuperGroup";
			LabTableName="B_Lab_"+LabTableName.Substring(LabTableName.IndexOf("_") + 1, LabTableName.Length - (LabTableName.IndexOf("_") + 1));
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSqlControl = new StringBuilder();
            string TableKey="SuperGroupNo";
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
	            		strSql.Append(" LabSuperGroupNo , CName , ShortName , ShortCode , Visible , DispOrder , StandCode , ZFStandCode , UseFlag ");
						strSql.Append(") select '" + lst[i].Trim() + "' as LabCode, ");
	            		strSql.Append("SuperGroupNo,CName,ShortName,ShortCode,Visible,DispOrder,StandCode,ZFStandCode,UseFlag");            
	            		strSql.Append(" from B_SuperGroup ");    
	            		
	            		strSqlControl.Append("insert into B_SuperGroupControl ( ");
	            		strSqlControl.Append(" "+TableKeySub+"ControlNo,"+TableKey+",ControlLabNo,Control"+TableKey+",UseFlag ");
	            		strSqlControl.Append(")  select ");
	            		strSqlControl.Append("  '" + lst[i].Trim() + "'+'_'+convert(varchar(50)," + TableKey + ")+'_'+convert(varchar(50), " + TableKey + ") as "+TableKeySub+"ControlNo,"+TableKey+",'" + lst[i].Trim() + "' as ControlLabNo,"+TableKey+",UseFlag ");
	            		strSqlControl.Append(" from B_SuperGroup ");  
	            		
	            		arrySql.Add(strSql.ToString());
	            		arrySql.Add(strSqlControl.ToString());	    
	            		
	            		strSql = new StringBuilder();
             			strSqlControl = new StringBuilder();
             			
	             		
	             }
	             
	             idb.BatchUpdateWithTransaction(arrySql);
                 d_log.OperateLog("SuperGroup", "", "", DateTime.Now, 1);
	             return true;
            }
            catch
            {
            	return false;
            }
           
		}
		
		public int GetMaxId()
        {
            return idb.GetMaxID("SuperGroupNo","B_SuperGroup");
        }

        public DataSet GetList(int Top, ZhiFang.Model.SuperGroup model, string filedOrder)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" * ");
			strSql.Append(" FROM B_SuperGroup ");		
			
			                                          
            if(model.SuperGroupNo !=null)
            {
                        strSql.Append(" and SuperGroupNo="+model.SuperGroupNo+" ");
                        }
                                          
            if(model.CName !=null)
            {
                        
            strSql.Append(" and CName='"+model.CName+"' ");
                        }
                                          
            if(model.ShortName !=null)
            {
                        
            strSql.Append(" and ShortName='"+model.ShortName+"' ");
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
            strSql.Append("select *,'" + LabCode + "' as LabCode,SuperGroupNo as LabSuperGroupNo from B_SuperGroup where 1=1 ");
            if (dTimeStampe != -999999)
            {
                strSql.Append(" and convert(int,DTimeStampe) > '" + dTimeStampe + "' ");
            }
            DataTable dtServer = idb.ExecuteDataSet(strSql.ToString()).Tables[0];
            dtServer.TableName = "ServerDatas";

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("select *,LabSuperGroupNo as SuperGroupNo from B_Lab_SuperGroup where 1=1 ");
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
            strSql3.Append("select * from B_SuperGroupControl where 1=1 ");
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


        #region IDataBase<SuperGroup> 成员
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
                        if (this.Exists(int.Parse(ds.Tables[0].Rows[i]["SuperGroupNo"].ToString().Trim())))
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
                strSql.Append("insert into B_SuperGroup (");
                strSql.Append("SuperGroupNo,CName,ShortName,ShortCode,Visible,DispOrder,ParentNo,AddTime,StandCode,ZFStandCode,UseFlag");
                strSql.Append(") values (");
                strSql.Append("'" + dr["SuperGroupNo"].ToString().Trim() + "','" + dr["CName"].ToString().Trim() + "','" + dr["ShortName"].ToString().Trim() + "','" + dr["ShortCode"].ToString().Trim() + "','" + dr["Visible"].ToString().Trim() + "','" + dr["DispOrder"].ToString().Trim() + "','" + dr["ParentNo"].ToString().Trim() + "','" + DateTime.Parse(dr["AddTime"].ToString().Trim()) + "','" + dr["StandCode"].ToString().Trim() + "','" + dr["ZFStandCode"].ToString().Trim() + "','" + dr["UseFlag"].ToString().Trim() + "'");
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
                strSql.Append("update B_SuperGroup set ");

                strSql.Append(" CName = '" + dr["CName"].ToString().Trim() + "' , ");
                strSql.Append(" ShortName = '" + dr["ShortName"].ToString().Trim() + "' , ");
                strSql.Append(" ShortCode = '" + dr["ShortCode"].ToString().Trim() + "' , ");
                strSql.Append(" Visible = '" + dr["Visible"].ToString().Trim() + "' , ");
                strSql.Append(" DispOrder = '" + dr["DispOrder"].ToString().Trim() + "' , ");
                strSql.Append(" ParentNo = '" + dr["ParentNo"].ToString().Trim() + "' , ");
                strSql.Append(" StandCode = '" + dr["StandCode"].ToString().Trim() + "' , ");
                strSql.Append(" ZFStandCode = '" + dr["ZFStandCode"].ToString().Trim() + "' , ");
                strSql.Append(" UseFlag = '" + dr["UseFlag"].ToString().Trim() + "'  ");
                strSql.Append(" where SuperGroupNo='" + dr["SuperGroupNo"].ToString().Trim() + "' ");

                return idb.ExecuteNonQuery(strSql.ToString());
            }
            catch
            {
                return 0;
            }
        }
        public int DeleteByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                if (dr.Table.Columns["SuperGroupNo"] != null && dr.Table.Columns["SuperGroupNo"].ToString().Trim() != "")
                {
                    strSql.Append("delete from B_SuperGroup ");
                    strSql.Append(" where SuperGroupNo='" + dr["SuperGroupNo"].ToString().Trim() + "' ");
                    return idb.ExecuteNonQuery(strSql.ToString());
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.digitlab8.SuperGroup.DeleteByDataRow同步数据时异常：", ex);
                return 0;
            }
        }
        
        #endregion

        #region IDSuperGroup 成员


        public DataSet GetParentSuperGroupNolist()
        {
            DataSet dsAll = new DataSet();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from B_SuperGroup where 1=1 ");
            strSql.Append(" and ParentNo is null ");
            return idb.ExecuteDataSet(strSql.ToString());
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

