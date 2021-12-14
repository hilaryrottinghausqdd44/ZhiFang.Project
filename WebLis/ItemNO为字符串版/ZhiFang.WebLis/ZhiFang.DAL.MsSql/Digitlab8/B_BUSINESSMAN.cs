using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Digitlab8  
{
	 	//B_BUSINESSMAN
		
	public partial class B_BUSINESSMAN : IDBUSINESSMAN,IDBatchCopy, IDGetListByTimeStampe
	{	
		DBUtility.IDBConnection idb;
        public B_BUSINESSMAN(string dbsourceconn)
        {
            idb = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
		public B_BUSINESSMAN()
		{
            idb = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.LisDB());
        }
		D_LogInfo d_log = new D_LogInfo();
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ZhiFang.Model.BUSINESSMAN model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into B_BUSINESSMAN(");			
            strSql.Append("CNAME,BMANNO,SHORTCODE,ISUSE,IDCODE,ADDRESS,PHONENUM,ROMARK,DispOrder,StandCode,ZFStandCode,UseFlag");
			strSql.Append(") values (");
            strSql.Append("@CNAME,@BMANNO,@SHORTCODE,@ISUSE,@IDCODE,@ADDRESS,@PHONENUM,@ROMARK,@DispOrder,@StandCode,@ZFStandCode,@UseFlag");            
            strSql.Append(") ");            
            
			SqlParameter[] parameters = {
			            new SqlParameter("@CNAME", SqlDbType.VarChar,40) ,            
                        new SqlParameter("@BMANNO", SqlDbType.Int,4) ,            
                        new SqlParameter("@SHORTCODE", SqlDbType.VarChar,40) ,            
                        new SqlParameter("@ISUSE", SqlDbType.Int,4) ,            
                        new SqlParameter("@IDCODE", SqlDbType.VarChar,40) ,            
                        new SqlParameter("@ADDRESS", SqlDbType.VarChar,80) ,            
                        new SqlParameter("@PHONENUM", SqlDbType.VarChar,40) ,            
                        new SqlParameter("@ROMARK", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            
                        new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ZFStandCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@UseFlag", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.CNAME;                        
            parameters[1].Value = model.BMANNO;                        
            parameters[2].Value = model.SHORTCODE;                        
            parameters[3].Value = model.ISUSE;                        
            parameters[4].Value = model.IDCODE;                        
            parameters[5].Value = model.ADDRESS;                        
            parameters[6].Value = model.PHONENUM;                        
            parameters[7].Value = model.ROMARK;                        
            parameters[8].Value = model.DispOrder;                        
            parameters[9].Value = model.StandCode;                        
            parameters[10].Value = model.ZFStandCode;                        
            parameters[11].Value = model.UseFlag;                  
			if (idb.ExecuteNonQuery(strSql.ToString(),parameters) > 0)
            {
                return d_log.OperateLog("BUSINESSMAN", "", "", DateTime.Now, 1);
            }
            else
                return -1;
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ZhiFang.Model.BUSINESSMAN model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update B_BUSINESSMAN set ");
			                                                
            strSql.Append(" CNAME = @CNAME , ");                                    
            strSql.Append(" BMANNO = @BMANNO , ");                                    
            strSql.Append(" SHORTCODE = @SHORTCODE , ");                                    
            strSql.Append(" ISUSE = @ISUSE , ");                                    
            strSql.Append(" IDCODE = @IDCODE , ");                                    
            strSql.Append(" ADDRESS = @ADDRESS , ");                                    
            strSql.Append(" PHONENUM = @PHONENUM , ");                                    
            strSql.Append(" ROMARK = @ROMARK , ");                                    
            strSql.Append(" DispOrder = @DispOrder , ");                                                                                    
            strSql.Append(" StandCode = @StandCode , ");                                    
            strSql.Append(" ZFStandCode = @ZFStandCode , ");                                    
            strSql.Append(" UseFlag = @UseFlag  ");            			
			strSql.Append(" where BMANNO=@BMANNO  ");
						
			SqlParameter[] parameters = {
			            	
                           
            new SqlParameter("@CNAME", SqlDbType.VarChar,40) ,            	
                           
            new SqlParameter("@BMANNO", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@SHORTCODE", SqlDbType.VarChar,40) ,            	
                           
            new SqlParameter("@ISUSE", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@IDCODE", SqlDbType.VarChar,40) ,            	
                           
            new SqlParameter("@ADDRESS", SqlDbType.VarChar,80) ,            	
                           
            new SqlParameter("@PHONENUM", SqlDbType.VarChar,40) ,            	
                           
            new SqlParameter("@ROMARK", SqlDbType.VarChar,200) ,            	
                           
            new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            	
                        	
                        	
                           
            new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@ZFStandCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@UseFlag", SqlDbType.Int,4)             	
              
            };
            			    
				
                
			   
			if(model.CNAME!=null)
			{
            	parameters[0].Value = model.CNAME;            	
            }
            	
                
			   
			if(model.BMANNO!=null)
			{
            	parameters[1].Value = model.BMANNO;            	
            }
            	
                
			   
			if(model.SHORTCODE!=null)
			{
            	parameters[2].Value = model.SHORTCODE;            	
            }
            	
                
			   
			if(model.ISUSE!=null)
			{
            	parameters[3].Value = model.ISUSE;            	
            }
            	
                
			   
			if(model.IDCODE!=null)
			{
            	parameters[4].Value = model.IDCODE;            	
            }
            	
                
			   
			if(model.ADDRESS!=null)
			{
            	parameters[5].Value = model.ADDRESS;            	
            }
            	
                
			   
			if(model.PHONENUM!=null)
			{
            	parameters[6].Value = model.PHONENUM;            	
            }
            	
                
			   
			if(model.ROMARK!=null)
			{
            	parameters[7].Value = model.ROMARK;            	
            }
            	
                
			   
			if(model.DispOrder!=null)
			{
            	parameters[8].Value = model.DispOrder;            	
            }
            	
                
				
                
				
                
			   
			if(model.StandCode!=null)
			{
            	parameters[9].Value = model.StandCode;            	
            }
            	
                
			   
			if(model.ZFStandCode!=null)
			{
            	parameters[10].Value = model.ZFStandCode;            	
            }
            	
                
			   
			if(model.UseFlag!=null)
			{
            	parameters[11].Value = model.UseFlag;            	
            }
            	
                        
			if (idb.ExecuteNonQuery(strSql.ToString(),parameters) > 0)
            {
                return d_log.OperateLog("BUSINESSMAN", "", "", DateTime.Now, 1);
            }
            else
                return -1;
		}
		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int BMANNO)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from B_BUSINESSMAN ");
			strSql.Append(" where BMANNO=@BMANNO ");
						SqlParameter[] parameters = {
					new SqlParameter("@BMANNO", SqlDbType.Int,4)};
			parameters[0].Value = BMANNO;


			return idb.ExecuteNonQuery(strSql.ToString(),parameters);
		
		}
		
				/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteList(string BNANIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from B_BUSINESSMAN ");
			strSql.Append(" where ID in ("+BNANIDlist + ")  ");
			return idb.ExecuteNonQuery(strSql.ToString());
			
		}
				
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZhiFang.Model.BUSINESSMAN GetModel(int BMANNO)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select BNANID, CNAME, BMANNO, SHORTCODE, ISUSE, IDCODE, ADDRESS, PHONENUM, ROMARK, DispOrder, AddTime, StandCode, ZFStandCode, UseFlag  ");			
			strSql.Append("  from B_BUSINESSMAN ");
			strSql.Append(" where BMANNO=@BMANNO ");
						SqlParameter[] parameters = {
					new SqlParameter("@BMANNO", SqlDbType.Int,4)};
			parameters[0].Value = BMANNO;

			
			ZhiFang.Model.BUSINESSMAN model=new ZhiFang.Model.BUSINESSMAN();
			DataSet ds=idb.ExecuteDataSet(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																
				if(ds.Tables[0].Rows[0]["BNANID"].ToString()!="")
				{
					model.BNANID=int.Parse(ds.Tables[0].Rows[0]["BNANID"].ToString());
				}
																																								
				model.CNAME= ds.Tables[0].Rows[0]["CNAME"].ToString();
																																
				if(ds.Tables[0].Rows[0]["BMANNO"].ToString()!="")
				{
					model.BMANNO=int.Parse(ds.Tables[0].Rows[0]["BMANNO"].ToString());
				}
																																								
				model.SHORTCODE= ds.Tables[0].Rows[0]["SHORTCODE"].ToString();
																																
				if(ds.Tables[0].Rows[0]["ISUSE"].ToString()!="")
				{
					model.ISUSE=int.Parse(ds.Tables[0].Rows[0]["ISUSE"].ToString());
				}
																																								
				model.IDCODE= ds.Tables[0].Rows[0]["IDCODE"].ToString();
																																				
				model.ADDRESS= ds.Tables[0].Rows[0]["ADDRESS"].ToString();
																																				
				model.PHONENUM= ds.Tables[0].Rows[0]["PHONENUM"].ToString();
																																				
				model.ROMARK= ds.Tables[0].Rows[0]["ROMARK"].ToString();
																																
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
			strSql.Append(" FROM B_BUSINESSMAN ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return idb.ExecuteDataSet(strSql.ToString());
		}
		
		
		/// <summary>
		/// 根据实体获取DataSet
		/// </summary>
		public DataSet GetList(ZhiFang.Model.BUSINESSMAN model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM B_BUSINESSMAN where 1=1 ");
			                                                                
                        if(model.CNAME !=null)
                        {
                        
            strSql.Append(" and CNAME='"+model.CNAME+"' ");
                        }
                                                    
             
            
            if(model.BMANNO !=0)
                        {
                        
            strSql.Append(" and BMANNO="+model.BMANNO+" ");
                        }
                                                    
                        if(model.SHORTCODE !=null)
                        {
                        
            strSql.Append(" and SHORTCODE='"+model.SHORTCODE+"' ");
                        }
                                                    
                        if(model.ISUSE !=null)
                        {
                        
            strSql.Append(" and ISUSE="+model.ISUSE+" ");
                        }
                                                    
                        if(model.IDCODE !=null)
                        {
                        
            strSql.Append(" and IDCODE='"+model.IDCODE+"' ");
                        }
                                                    
                        if(model.ADDRESS !=null)
                        {
                        
            strSql.Append(" and ADDRESS='"+model.ADDRESS+"' ");
                        }
                                                    
                        if(model.PHONENUM !=null)
                        {
                        
            strSql.Append(" and PHONENUM='"+model.PHONENUM+"' ");
                        }
                                                    
                        if(model.ROMARK !=null)
                        {
                        
            strSql.Append(" and ROMARK='"+model.ROMARK+"' ");
                        }
                                                    
                        if(model.DispOrder !=null)
                        {
                        
            strSql.Append(" and DispOrder="+model.DispOrder+" ");
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

        public DataSet GetListLike(Model.BUSINESSMAN model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *,BMANNO as LabNo,convert(varchar(100),BMANNO)+'_'+CNAME as LabNo_Value,CNAME+'('+convert(varchar(100),BMANNO)+')' as LabNoAndName_Text ");
            strSql.Append(" FROM B_Department  ");

            if (model.CNAME != null)
            {
                strSql.Append(" where 1=2 ");
                strSql.Append(" or CNAME like '%" + model.CNAME + "%' ");
            }

            if (model.BMANNO != 0)
            {
                strSql.Append(" or BMANNO like '%" + model.BMANNO + "%' ");
            }
            
            if (model.SHORTCODE != null)
            {
                strSql.Append(" or SHORTCODE like '%" + model.SHORTCODE + "%' ");
            }

            return idb.ExecuteDataSet(strSql.ToString());
        }

		/// <summary>
		/// 获取总记录
		/// </summary>
		public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_BUSINESSMAN ");
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
        public int GetTotalCount(ZhiFang.Model.BUSINESSMAN model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            strSql.Append("select count(*) FROM B_BUSINESSMAN where 1=1 ");
            if (model != null)
            {
                if (model.CNAME != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or CNAME like '%" + model.CNAME + "%' ");
                    else
                        strWhere.Append(" and ( CNAME like '%" + model.CNAME + "%' ");
                }
                if (model.BMANNO != 0)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or BMANNO like '%" + model.BMANNO + "%' ");
                    else
                        strWhere.Append(" and ( BMANNO like '%" + model.BMANNO + "%' ");
                }
                if (model.SHORTCODE != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or SHORTCODE like '%" + model.SHORTCODE + "%' ");
                    else
                        strWhere.Append(" and ( SHORTCODE like '%" + model.SHORTCODE + "%' ");
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
        public DataSet GetListByPage(ZhiFang.Model.BUSINESSMAN model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            if (model != null)
            {
                if (model.BMANNO != 0)
                {
                    strWhere.Append(" and ( B_BUSINESSMAN.BMANNO like '%" + model.BMANNO + "%'  ");
                }
                if (model.CNAME != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or B_BUSINESSMAN.CNAME like '%" + model.CNAME + "%'  ");
                    else
                        strWhere.Append(" and ( B_BUSINESSMAN.CNAME like '%" + model.CNAME + "%'  ");
                }
                if (model.SHORTCODE != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or B_BUSINESSMAN.SHORTCODE like '%" + model.SHORTCODE + "%'  ");
                    else
                        strWhere.Append(" and ( B_BUSINESSMAN.SHORTCODE like '%" + model.SHORTCODE + "%'  ");
                }
                if (strWhere.Length > 0)
                    strWhere.Append(" ) ");
            }
            if (model != null && model.LabCode != null)
            {
                strSql.Append(" select top " + nowPageSize + "  * from B_BUSINESSMAN left join B_BUSINESSMANControl on B_BUSINESSMAN.BMANNO=B_BUSINESSMANControl.BMANNO ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and B_BUSINESSMANControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append("where BNANID not in ( ");
                strSql.Append("select top " + (nowPageSize * nowPageNum) + " BNANID from  B_BUSINESSMAN left join B_BUSINESSMANControl on B_BUSINESSMAN.BMANNO=B_BUSINESSMANControl.BMANNO ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and B_BUSINESSMANControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append(" " + strWhere.ToString() + " order by B_BUSINESSMAN.BNANID ) " + strWhere.ToString() + " order by B_BUSINESSMAN.BNANID ");
                return idb.ExecuteDataSet(strSql.ToString());
            }
            else
            {
                strSql.Append("select top " + nowPageSize + "  * from B_BUSINESSMAN where BNANID not in  ");
                strSql.Append("(select top " + (nowPageSize * nowPageNum) + " BNANID from B_BUSINESSMAN where 1=1 " + strWhere.ToString() + " order by " + model.OrderField + ") " + strWhere.ToString() + " order by " + model.OrderField + "  ");
                return idb.ExecuteDataSet(strSql.ToString());
            }
        }
        
        public bool Exists(int BMANNO)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from B_BUSINESSMAN ");
			strSql.Append(" where BMANNO ='"+BMANNO+"'");
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
            strSql.Append("select count(1) from B_BUSINESSMAN where 1=1 ");
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
			string LabTableName="B_BUSINESSMAN";
			LabTableName="B_Lab_"+LabTableName.Substring(LabTableName.IndexOf("_") + 1, LabTableName.Length - (LabTableName.IndexOf("_") + 1));
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSqlControl = new StringBuilder();
            string TableKey="BMANNO";
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
	            		strSql.Append(" CNAME , LabBMANNO , SHORTCODE , ISUSE , IDCODE , ADDRESS , PHONENUM , ROMARK , DispOrder , StandCode , ZFStandCode , UseFlag ");
						strSql.Append(") select '" + lst[i].Trim() + "' as LabCode, ");
	            		strSql.Append("CNAME,BMANNO,SHORTCODE,ISUSE,IDCODE,ADDRESS,PHONENUM,ROMARK,DispOrder,StandCode,ZFStandCode,UseFlag");            
	            		strSql.Append(" from B_BUSINESSMAN ");    
	            		
	            		strSqlControl.Append("insert into B_BUSINESSMANControl ( ");
	            		strSqlControl.Append(" "+TableKeySub+"ControlNo,"+TableKey+",ControlLabNo,Control"+TableKey+",UseFlag ");
	            		strSqlControl.Append(")  select ");
	            		strSqlControl.Append("  '" + lst[i].Trim() + "'+'_'+convert(varchar(50)," + TableKey + ")+'_'+convert(varchar(50), " + TableKey + ") as "+TableKeySub+"ControlNo,"+TableKey+",'" + lst[i].Trim() + "' as ControlLabNo,"+TableKey+",UseFlag ");
	            		strSqlControl.Append(" from B_BUSINESSMAN ");  
	            		
	            		arrySql.Add(strSql.ToString());
	            		arrySql.Add(strSqlControl.ToString());	    
	            		
	            		strSql = new StringBuilder();
             			strSqlControl = new StringBuilder();
             			
	             }

                idb.BatchUpdateWithTransaction(arrySql);
                d_log.OperateLog("BUSINESSMAN", "", "", DateTime.Now, 1);
	             return true;
            }
            catch
            {
            	return false;
            }
           
		}
		
		public int GetMaxId()
        {
            return idb.GetMaxID("BMANNO","B_BUSINESSMAN");
        }

        public DataSet GetList(int Top, ZhiFang.Model.BUSINESSMAN model, string filedOrder)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" * ");
			strSql.Append(" FROM B_BUSINESSMAN ");		
			
			                                                       
                        
            if(model.CNAME !=null)
            {            
            	strSql.Append(" and CNAME='"+model.CNAME+"' ");
            }
                                            
                        
            if(model.BMANNO !=0)
            {            
            	strSql.Append(" and BMANNO='"+model.BMANNO+"' ");
            }
                                            
                        
            if(model.SHORTCODE !=null)
            {            
            	strSql.Append(" and SHORTCODE='"+model.SHORTCODE+"' ");
            }
                                            
                        
            if(model.ISUSE !=null)
            {            
            	strSql.Append(" and ISUSE='"+model.ISUSE+"' ");
            }
                                            
                        
            if(model.IDCODE !=null)
            {            
            	strSql.Append(" and IDCODE='"+model.IDCODE+"' ");
            }
                                            
                        
            if(model.ADDRESS !=null)
            {            
            	strSql.Append(" and ADDRESS='"+model.ADDRESS+"' ");
            }
                                            
                        
            if(model.PHONENUM !=null)
            {            
            	strSql.Append(" and PHONENUM='"+model.PHONENUM+"' ");
            }
                                            
                        
            if(model.ROMARK !=null)
            {            
            	strSql.Append(" and ROMARK='"+model.ROMARK+"' ");
            }
                                            
                        
            if(model.DispOrder !=null)
            {            
            	strSql.Append(" and DispOrder='"+model.DispOrder+"' ");
            }
                                                                    
                        
            if(model.StandCode !=null)
            {            
            	strSql.Append(" and StandCode='"+model.StandCode+"' ");
            }
                                            
                        
            if(model.ZFStandCode !=null)
            {            
            	strSql.Append(" and ZFStandCode='"+model.ZFStandCode+"' ");
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
            strSql.Append("select *,'"+LabCode+"' as LabCode,BMANNO as LabBMANNO from B_BUSINESSMAN where 1=1 ");
            if (dTimeStampe != -999999)
            {
                strSql.Append(" and convert(int,DTimeStampe) > '" + dTimeStampe + "' ");
            }
            DataTable dtServer = idb.ExecuteDataSet(strSql.ToString()).Tables[0];
            dtServer.TableName = "ServerDatas";

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("select *,LabBMANNO as BMANNO from B_Lab_BUSINESSMAN where 1=1 ");
            if (LabCode.Trim() != "")
            {
                strSql2.Append(" and LabCode= '" + LabCode.Trim() + "' ");
            }
            if (dTimeStampe != -999999)
            {
                strSql2.Append(" and convert(int,DTimeStampe) > '" + dTimeStampe + "' ");
            }
            DataTable dtLab = idb.ExecuteDataSet(strSql2.ToString()).Tables[0];
            dtLab.TableName = "LabDatas";

            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("select * from B_BUSINESSMANControl where 1=1 ");
            if (LabCode.Trim() != "")
            {
                strSql3.Append(" and ControlLabNo= '" + LabCode.Trim() + "' ");
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
			                                            
                        if (this.Exists(int.Parse(ds.Tables[0].Rows[i]["BMANNO"].ToString().Trim())))
                                                	
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
				strSql.Append("insert into B_BUSINESSMAN (");			
            	strSql.Append("CNAME,BMANNO,SHORTCODE,ISUSE,IDCODE,ADDRESS,PHONENUM,ROMARK,DispOrder,StandCode,ZFStandCode,UseFlag");
				strSql.Append(") values (");			
            	            	            	
            	if(dr.Table.Columns["CNAME"]!=null && dr.Table.Columns["CNAME"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["CNAME"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["BMANNO"]!=null && dr.Table.Columns["BMANNO"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["BMANNO"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["SHORTCODE"]!=null && dr.Table.Columns["SHORTCODE"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["SHORTCODE"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["ISUSE"]!=null && dr.Table.Columns["ISUSE"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["ISUSE"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["IDCODE"]!=null && dr.Table.Columns["IDCODE"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["IDCODE"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["ADDRESS"]!=null && dr.Table.Columns["ADDRESS"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["ADDRESS"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["PHONENUM"]!=null && dr.Table.Columns["PHONENUM"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["PHONENUM"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["ROMARK"]!=null && dr.Table.Columns["ROMARK"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["ROMARK"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["DispOrder"]!=null && dr.Table.Columns["DispOrder"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["DispOrder"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["StandCode"]!=null && dr.Table.Columns["StandCode"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["StandCode"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["ZFStandCode"]!=null && dr.Table.Columns["ZFStandCode"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["ZFStandCode"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["UseFlag"]!=null && dr.Table.Columns["UseFlag"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["UseFlag"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	
            	int n = strSql.ToString().LastIndexOf(",");
				strSql.Remove(n, 1);
            	strSql.Append(") ");  
                return idb.ExecuteNonQuery(strSql.ToString());
            }
            catch(Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Digitlab8.B_BUSINESSMAN.AddByDataRow 同步数据时异常：", ex);
                return 0;
            }          
        }
        public int UpdateByDataRow(DataRow dr)
        {
            try
            {
               StringBuilder strSql=new StringBuilder();
			   strSql.Append("update B_BUSINESSMAN set ");
			   			    			       
			    			    
			    if( dr.Table.Columns["CNAME"]!=null && dr.Table.Columns["CNAME"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" CNAME = '"+dr["CNAME"].ToString().Trim()+"' , ");
			    }
			      			    			       
			    			    
			    if( dr.Table.Columns["SHORTCODE"]!=null && dr.Table.Columns["SHORTCODE"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" SHORTCODE = '"+dr["SHORTCODE"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["ISUSE"]!=null && dr.Table.Columns["ISUSE"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" ISUSE = '"+dr["ISUSE"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["IDCODE"]!=null && dr.Table.Columns["IDCODE"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" IDCODE = '"+dr["IDCODE"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["ADDRESS"]!=null && dr.Table.Columns["ADDRESS"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" ADDRESS = '"+dr["ADDRESS"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["PHONENUM"]!=null && dr.Table.Columns["PHONENUM"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" PHONENUM = '"+dr["PHONENUM"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["ROMARK"]!=null && dr.Table.Columns["ROMARK"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" ROMARK = '"+dr["ROMARK"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["DispOrder"]!=null && dr.Table.Columns["DispOrder"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" DispOrder = '"+dr["DispOrder"].ToString().Trim()+"' , ");
			    }
			      			    			    			       
			    			    
			    if( dr.Table.Columns["StandCode"]!=null && dr.Table.Columns["StandCode"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" StandCode = '"+dr["StandCode"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["ZFStandCode"]!=null && dr.Table.Columns["ZFStandCode"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" ZFStandCode = '"+dr["ZFStandCode"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["UseFlag"]!=null && dr.Table.Columns["UseFlag"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" UseFlag = '"+dr["UseFlag"].ToString().Trim()+"' , ");
			    }
			      		
			    
			    int n = strSql.ToString().LastIndexOf(",");
				strSql.Remove(n, 1);
				strSql.Append(" where BMANNO='"+dr["BMANNO"].ToString().Trim()+"' ");
						
                return idb.ExecuteNonQuery(strSql.ToString());
            }
            catch(Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Digitlab8.B_BUSINESSMAN .UpdateByDataRow同步数据时异常：", ex);
                return 0;
            }
        }
        public int DeleteByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                if (dr.Table.Columns["BMANNO"] != null && dr.Table.Columns["BMANNO"].ToString().Trim() != "")
                {
                    strSql.Append("delete from B_BUSINESSMAN ");
                    strSql.Append(" where BMANNO='" + dr["BMANNO"].ToString().Trim() + "' ");
                    return idb.ExecuteNonQuery(strSql.ToString());
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Digitlab8.BUSINESSMAN.DeleteByDataRow同步数据时异常：", ex);
                return 0;
            }
        }




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

