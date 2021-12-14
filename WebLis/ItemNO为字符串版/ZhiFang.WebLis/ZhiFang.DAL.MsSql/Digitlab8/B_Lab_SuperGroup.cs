using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Digitlab8  
{
	 	//B_Lab_SuperGroup
		
	public partial class B_Lab_SuperGroup: IDLab_SuperGroup	{	
		DBUtility.IDBConnection idb;
        public B_Lab_SuperGroup(string dbsourceconn)
        {
            idb = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
		public B_Lab_SuperGroup()
		{
            idb = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.LisDB());
        }
		D_LogInfo d_log = new D_LogInfo();
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ZhiFang.Model.Lab_SuperGroup model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into B_Lab_SuperGroup(");			
            strSql.Append("LabCode,LabSuperGroupNo,CName,ShortName,ShortCode,Visible,DispOrder,ParentNo,StandCode,ZFStandCode,UseFlag");
			strSql.Append(") values (");
            strSql.Append("@LabCode,@LabSuperGroupNo,@CName,@ShortName,@ShortCode,@Visible,@DispOrder,@ParentNo,@StandCode,@ZFStandCode,@UseFlag");            
            strSql.Append(") ");            
            
			SqlParameter[] parameters = {
			            new SqlParameter("@LabCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@LabSuperGroupNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@CName", SqlDbType.VarChar,40) ,            
                        new SqlParameter("@ShortName", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@ShortCode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@Visible", SqlDbType.Int,4) ,            
                        new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            
                        new SqlParameter("@ParentNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ZFStandCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@UseFlag", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.LabCode;                        
            parameters[1].Value = model.LabSuperGroupNo;                        
            parameters[2].Value = model.CName;                        
            parameters[3].Value = model.ShortName;                        
            parameters[4].Value = model.ShortCode;                        
            parameters[5].Value = model.Visible;                        
            parameters[6].Value = model.DispOrder;                        
            parameters[7].Value = model.ParentNo;                        
            parameters[8].Value = model.StandCode;                        
            parameters[9].Value = model.ZFStandCode;                        
            parameters[10].Value = model.UseFlag;                  
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
		public int Update(ZhiFang.Model.Lab_SuperGroup model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update B_Lab_SuperGroup set ");
			                                                
            strSql.Append(" LabCode = @LabCode , ");                                    
            strSql.Append(" LabSuperGroupNo = @LabSuperGroupNo , ");                                    
            strSql.Append(" CName = @CName , ");                                    
            strSql.Append(" ShortName = @ShortName , ");                                    
            strSql.Append(" ShortCode = @ShortCode , ");                                    
            strSql.Append(" Visible = @Visible , ");                                    
            strSql.Append(" DispOrder = @DispOrder , ");                                    
            strSql.Append(" ParentNo = @ParentNo , ");                                                                                    
            strSql.Append(" StandCode = @StandCode , ");                                    
            strSql.Append(" ZFStandCode = @ZFStandCode , ");                                    
            strSql.Append(" UseFlag = @UseFlag  ");            			
			strSql.Append(" where LabCode=@LabCode and LabSuperGroupNo=@LabSuperGroupNo  ");
						
			SqlParameter[] parameters = {
			            	
                           
            new SqlParameter("@LabCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@LabSuperGroupNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@CName", SqlDbType.VarChar,40) ,            	
                           
            new SqlParameter("@ShortName", SqlDbType.VarChar,10) ,            	
                           
            new SqlParameter("@ShortCode", SqlDbType.VarChar,10) ,            	
                           
            new SqlParameter("@Visible", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@ParentNo", SqlDbType.Int,4) ,            	
                        	
                        	
                           
            new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@ZFStandCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@UseFlag", SqlDbType.Int,4)             	
              
            };
            			    
				
                
			   
			if(model.LabCode!=null)
			{
            	parameters[0].Value = model.LabCode;            	
            }
            	
                
			   
			if(model.LabSuperGroupNo!=null)
			{
            	parameters[1].Value = model.LabSuperGroupNo;            	
            }
            	
                
			   
			if(model.CName!=null)
			{
            	parameters[2].Value = model.CName;            	
            }
            	
                
			   
			if(model.ShortName!=null)
			{
            	parameters[3].Value = model.ShortName;            	
            }
            	
                
			   
			if(model.ShortCode!=null)
			{
            	parameters[4].Value = model.ShortCode;            	
            }
            	
                
			   
			if(model.Visible!=null)
			{
            	parameters[5].Value = model.Visible;            	
            }
            	
                
			   
			if(model.DispOrder!=null)
			{
            	parameters[6].Value = model.DispOrder;            	
            }
            	
                
			   
			if(model.ParentNo!=null)
			{
            	parameters[7].Value = model.ParentNo;            	
            }
            	
                
				
                
				
                
			   
			if(model.StandCode!=null)
			{
            	parameters[8].Value = model.StandCode;            	
            }
            	
                
			   
			if(model.ZFStandCode!=null)
			{
            	parameters[9].Value = model.ZFStandCode;            	
            }
            	
                
			   
			if(model.UseFlag!=null)
			{
            	parameters[10].Value = model.UseFlag;            	
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
		public int Delete(string LabCode,int LabSuperGroupNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from B_Lab_SuperGroup ");
			strSql.Append(" where LabCode=@LabCode and LabSuperGroupNo=@LabSuperGroupNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@LabCode", SqlDbType.VarChar,50),
					new SqlParameter("@LabSuperGroupNo", SqlDbType.Int,4)};
			parameters[0].Value = LabCode;
			parameters[1].Value = LabSuperGroupNo;


			return idb.ExecuteNonQuery(strSql.ToString(),parameters);
			
		}
		
				/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteList(string SuperGroupIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from B_Lab_SuperGroup ");
			strSql.Append(" where ID in ("+SuperGroupIDlist + ")  ");
			return idb.ExecuteNonQuery(strSql.ToString());
			
		}
				
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZhiFang.Model.Lab_SuperGroup GetModel(string LabCode,int LabSuperGroupNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select SuperGroupID, LabCode, LabSuperGroupNo, CName, ShortName, ShortCode, Visible, DispOrder, ParentNo, DTimeStampe, AddTime, StandCode, ZFStandCode, UseFlag  ");			
			strSql.Append("  from B_Lab_SuperGroup ");
			strSql.Append(" where LabCode=@LabCode and LabSuperGroupNo=@LabSuperGroupNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@LabCode", SqlDbType.VarChar,50),
					new SqlParameter("@LabSuperGroupNo", SqlDbType.Int,4)};
			parameters[0].Value = LabCode;
			parameters[1].Value = LabSuperGroupNo;

			
			ZhiFang.Model.Lab_SuperGroup model=new ZhiFang.Model.Lab_SuperGroup();
			DataSet ds=idb.ExecuteDataSet(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																if(ds.Tables[0].Rows[0]["SuperGroupID"].ToString()!="")
				{
					model.SuperGroupID=int.Parse(ds.Tables[0].Rows[0]["SuperGroupID"].ToString());
				}
																																								model.LabCode= ds.Tables[0].Rows[0]["LabCode"].ToString();
																																if(ds.Tables[0].Rows[0]["LabSuperGroupNo"].ToString()!="")
				{
					model.LabSuperGroupNo=int.Parse(ds.Tables[0].Rows[0]["LabSuperGroupNo"].ToString());
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
																																				if(ds.Tables[0].Rows[0]["ParentNo"].ToString()!="")
				{
					model.ParentNo=int.Parse(ds.Tables[0].Rows[0]["ParentNo"].ToString());
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
			strSql.Append(" FROM B_Lab_SuperGroup ");
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
			strSql.Append(" FROM B_Lab_SuperGroup ");
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
        public DataSet GetList(ZhiFang.Model.Lab_SuperGroup model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM B_Lab_SuperGroup where 1=1 ");

            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }
            if (model.LabSuperGroupNo != -1)
            {
                strSql.Append(" and LabSuperGroupNo=" + model.LabSuperGroupNo + " ");
            }

            if (model.CName != null)
            {
                strSql.Append(" and CName='" + model.CName + "' ");
            }

            if (model.ShortName != null)
            {
                strSql.Append(" and ShortName='" + model.ShortName + "' ");
            }

            if (model.ShortCode != null)
            {
                strSql.Append(" and ShortCode='" + model.ShortCode + "' ");
            }

            return idb.ExecuteDataSet(strSql.ToString());
        }
        public DataSet GetListByLike(ZhiFang.Model.Lab_SuperGroup model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ,'('+convert(varchar(100),LabSuperGroupNo)+')'+CName as LabSuperGroupNoAndName ");
            strSql.Append(" FROM B_Lab_SuperGroup where 1=1 ");
            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }
            if (model.CName != null)
            {
                strSql.Append(" and ( CName like '%" + model.CName + "%' ");
            }
            if (model.LabSuperGroupNo != 0)
            {
                strSql.Append(" or LabSuperGroupNo like '%" + model.LabSuperGroupNo + "%' ");
            }
            if (model.ShortName != null)
            {
                strSql.Append(" or ShortName like '%" + model.ShortName + "%' ");
            }

            if (model.ShortCode != null)
            {
                strSql.Append(" or ShortCode like '%" + model.ShortCode + "%' ");
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
            strSql.Append("select count(*) FROM B_Lab_SuperGroup ");
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
        public int GetTotalCount(ZhiFang.Model.Lab_SuperGroup model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            strSql.Append("select count(*) FROM B_Lab_SuperGroup where 1=1 ");
            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode = '" + model.LabCode + "'");
            }
            if (model != null)
            {
                if (model.LabSuperGroupNo != -1)
                {
                    strWhere.Append(" and ( LabSuperGroupNo like '%" + model.LabSuperGroupNo + "%' ");
                }
                if (model.CName != null)
                {
                    if (strWhere.Length == 0)
                        strWhere.Append(" and ( CName like '%" + model.CName + "%' ");
                    else
                        strWhere.Append(" or CName like '%" + model.CName + "%' ");
                }
                if (model.ShortName != null)
                {
                    if (strWhere.Length == 0)
                        strWhere.Append(" and ( ShortName like '%" + model.ShortName + "%' ");
                    else
                        strWhere.Append(" or ShortName like '%" + model.ShortName + "%' ");
                }
                if (model.ShortCode != null)
                {
                    if (strWhere.Length == 0)
                        strWhere.Append(" and ( ShortCode like '%" + model.ShortCode + "%' ");
                    else
                        strWhere.Append(" or ShortCode like '%" + model.ShortCode + "%' ");
                }
                if (strWhere.Length != 0)
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
        public DataSet GetListByPage(ZhiFang.Model.Lab_SuperGroup model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            if (model.LabSuperGroupNo != -1)
            {
                strWhere.Append(" and ( LabSuperGroupNo like '%" + model.LabSuperGroupNo + "%' ");
            }
            if (model.CName != null)
            {
                if (strWhere.Length == 0)
                    strWhere.Append(" and ( CName like '%" + model.CName + "%' ");
                else
                    strWhere.Append(" or CName like '%" + model.CName + "%' ");
            }
            if (model.ShortName != null)
            {
                if (strWhere.Length == 0)
                    strWhere.Append(" and ( ShortName like '%" + model.ShortName + "%' ");
                else
                    strWhere.Append(" or ShortName like '%" + model.ShortName + "%' ");
            }
            if (model.ShortCode != null)
            {
                if (strWhere.Length == 0)
                    strWhere.Append(" and ( ShortCode like '%" + model.ShortCode + "%' ");
                else
                    strWhere.Append(" or ShortCode like '%" + model.ShortCode + "%' ");
            }
            if (strWhere.Length != 0)
                strWhere.Append(" ) ");

            strSql.Append("select top " + nowPageSize + " * from B_Lab_SuperGroup where 1=1  ");
            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }
            strSql.Append(" " + strWhere.ToString() + " and SuperGroupID not in ");
            strSql.Append("(select top " + (nowPageSize * nowPageNum) + " SuperGroupID from B_Lab_SuperGroup where 1=1  ");
            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }
            strSql.Append(" " + strWhere.ToString() + " order by SuperGroupID) order by SuperGroupID  ");
            return idb.ExecuteDataSet(strSql.ToString());
        }
        
        public bool Exists(string LabCode,int LabSuperGroupNo)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from B_Lab_SuperGroup ");
			strSql.Append(" where LabCode=@LabCode and LabSuperGroupNo=@LabSuperGroupNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@LabCode", SqlDbType.VarChar,50),
					new SqlParameter("@LabSuperGroupNo", SqlDbType.Int,4)};
			parameters[0].Value = LabCode;
			parameters[1].Value = LabSuperGroupNo;


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

        public bool Exists(System.Collections.Hashtable ht)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from B_Lab_SuperGroup where 1=1 ");
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
            return idb.GetMaxID("LabCode,LabSuperGroupNo","B_Lab_SuperGroup");
        }

        public DataSet GetList(int Top, ZhiFang.Model.Lab_SuperGroup model, string filedOrder)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" * ");
			strSql.Append(" FROM B_Lab_SuperGroup ");		
			
			                                          
            if(model.LabCode !=null)
            {
                        
            strSql.Append(" and LabCode='"+model.LabCode+"' ");
                        }
                                          
            if(model.LabSuperGroupNo !=null)
            {
                        strSql.Append(" and LabSuperGroupNo="+model.LabSuperGroupNo+" ");
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
                                          
            if(model.ParentNo !=null)
            {
                        strSql.Append(" and ParentNo="+model.ParentNo+" ");
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



        #region IDataBase<Lab_SuperGroup> 成员
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
                        if (this.Exists(ds.Tables[0].Rows[i]["LabCode"].ToString().Trim(),int.Parse(ds.Tables[0].Rows[i]["LabSuperGroupNo"].ToString().Trim())))
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
                strSql.Append("insert into B_Lab_SuperGroup (");
                strSql.Append("LabCode,LabSuperGroupNo,CName,ShortName,ShortCode,Visible,DispOrder,ParentNo,AddTime,StandCode,ZFStandCode,UseFlag");
                strSql.Append(") values (");
                strSql.Append("'" + dr["LabCode"].ToString().Trim() + "','" + dr["LabSuperGroupNo"].ToString().Trim() + "','" + dr["CName"].ToString().Trim() + "','" + dr["ShortName"].ToString().Trim() + "','" + dr["ShortCode"].ToString().Trim() + "','" + dr["Visible"].ToString().Trim() + "','" + dr["DispOrder"].ToString().Trim() + "','" + dr["ParentNo"].ToString().Trim() + "','" + DateTime.Parse(dr["AddTime"].ToString().Trim()) + "','" + dr["StandCode"].ToString().Trim() + "','" + dr["ZFStandCode"].ToString().Trim() + "','" + dr["UseFlag"].ToString().Trim() + "'");
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
                strSql.Append("update B_Lab_SuperGroup set ");

                strSql.Append(" CName = '" + dr["CName"].ToString().Trim() + "' , ");
                strSql.Append(" ShortName = '" + dr["ShortName"].ToString().Trim() + "' , ");
                strSql.Append(" ShortCode = '" + dr["ShortCode"].ToString().Trim() + "' , ");
                strSql.Append(" Visible = '" + dr["Visible"].ToString().Trim() + "' , ");
                strSql.Append(" DispOrder = '" + dr["DispOrder"].ToString().Trim() + "' , ");
                strSql.Append(" ParentNo = '" + dr["ParentNo"].ToString().Trim() + "' , ");
                strSql.Append(" StandCode = '" + dr["StandCode"].ToString().Trim() + "' , ");
                strSql.Append(" ZFStandCode = '" + dr["ZFStandCode"].ToString().Trim() + "' , ");
                strSql.Append(" UseFlag = '" + dr["UseFlag"].ToString().Trim() + "'  ");
                strSql.Append(" where LabCode='" + dr["LabCode"].ToString().Trim() + "' and LabSuperGroupNo='" + dr["LabSuperGroupNo"].ToString().Trim() + "' ");

                return idb.ExecuteNonQuery(strSql.ToString());
            }
            catch
            {
                return 0;
            }
        }
        #endregion


        public DataSet GetLabCodeNo(string LabCode, List<string> LabCname)
        {
            throw new NotImplementedException();
        }


        public DataSet GetParentSuperGroupNolist()
        {
            throw new NotImplementedException();
        }
    }
}

