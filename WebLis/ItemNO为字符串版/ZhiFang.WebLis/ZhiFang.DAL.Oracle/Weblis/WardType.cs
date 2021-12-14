using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.Oracle.weblis
{
	 	//WardType
		
	public partial class WardType : BaseDALLisDB,IDWardType,IDBatchCopy, IDGetListByTimeStampe
	{	
         public WardType(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
         public WardType()
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
        }
		D_LogInfo d_log = new D_LogInfo();
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ZhiFang.Model.WardType model)
		{
            //StringBuilder strSql=new StringBuilder();
            //strSql.Append("insert into WardType(");			
            //strSql.Append("DistrictNo,WardNo,CName,ShortName,ShortCode,Visible,DispOrder,HisOrderCode");
            //strSql.Append(") values (");
            //strSql.Append("@DistrictNo,@WardNo,@CName,@ShortName,@ShortCode,@Visible,@DispOrder,@HisOrderCode");            
            //strSql.Append(") ");            
            
            //SqlParameter[] parameters = {
            //            new SqlParameter("@DistrictNo", SqlDbType.Int,4) ,            
            //            new SqlParameter("@WardNo", SqlDbType.Int,4) ,            
            //            new SqlParameter("@CName", SqlDbType.VarChar,40) ,            
            //            new SqlParameter("@ShortName", SqlDbType.VarChar,10) ,            
            //            new SqlParameter("@ShortCode", SqlDbType.VarChar,10) ,            
            //            new SqlParameter("@Visible", SqlDbType.Int,4) ,            
            //            new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            
            //            new SqlParameter("@HisOrderCode", SqlDbType.VarChar,20)             
              
            //};
			            
            //parameters[0].Value = model.DistrictNo;                        
            //parameters[1].Value = model.WardNo;                        
            //parameters[2].Value = model.CName;                        
            //parameters[3].Value = model.ShortName;                        
            //parameters[4].Value = model.ShortCode;                        
            //parameters[5].Value = model.Visible;                        
            //parameters[6].Value = model.DispOrder;                        
            //parameters[7].Value = model.HisOrderCode;

            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.DistrictNo != null)
            {
                strSql1.Append("DistrictNo,");
                strSql2.Append("" + model.DistrictNo + ",");
            }
            if (model.WardNo != null)
            {
                strSql1.Append("WardNo,");
                strSql2.Append("" + model.WardNo + ",");
            }
            if (model.CName != null)
            {
                strSql1.Append("CName,");
                strSql2.Append("'" + model.CName + "',");
            }
            if (model.ShortName != null)
            {
                strSql1.Append("ShortName,");
                strSql2.Append("'" + model.ShortName + "',");
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
            strSql.Append("insert into WardType(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");

            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            //if (DbHelperSQL.ExecuteNonQuery(strSql.ToString(),parameters) > 0)
            //{
            //    return d_log.OperateLog("WardType", "", "", DateTime.Now, 1);
            //}
            //else
            //    return -1;
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ZhiFang.Model.WardType model)
		{
            //StringBuilder strSql=new StringBuilder();
            //strSql.Append("update WardType set ");

            //strSql.Append(" DistrictNo = @DistrictNo , ");                                 
            //strSql.Append(" CName = @CName , ");                                    
            //strSql.Append(" ShortName = @ShortName , ");                                    
            //strSql.Append(" ShortCode = @ShortCode , ");                                    
            //strSql.Append(" Visible = @Visible , ");                                    
            //strSql.Append(" DispOrder = @DispOrder , ");                                    
            //strSql.Append(" HisOrderCode = @HisOrderCode  ");            			
            //strSql.Append(" where DistrictNo=@OldDistrictNo and WardNo=@WardNo  ");
						
            //SqlParameter[] parameters = {			               
            //new SqlParameter("@DistrictNo", SqlDbType.Int,4) ,
            //new SqlParameter("@WardNo", SqlDbType.Int,4) ,
            //new SqlParameter("@CName", SqlDbType.VarChar,40) ,
            //new SqlParameter("@ShortName", SqlDbType.VarChar,10) ,
            //new SqlParameter("@ShortCode", SqlDbType.VarChar,10) , 
            //new SqlParameter("@Visible", SqlDbType.Int,4) , 
            //new SqlParameter("@DispOrder", SqlDbType.Int,4) , 
            //new SqlParameter("@HisOrderCode", SqlDbType.VarChar,20) ,
            //new SqlParameter("@OldDistrictNo", SqlDbType.Int,4) 
              
            //};
            
            //if(model.DistrictNo!=null)
            //{
            //    parameters[0].Value = model.DistrictNo;            	
            //}
           
            //if(model.WardNo!=null)
            //{
            //    parameters[1].Value = model.WardNo;            	
            //}
             
            //if(model.CName!=null)
            //{
            //    parameters[2].Value = model.CName;            	
            //}
            
            //if(model.ShortName!=null)
            //{
            //    parameters[3].Value = model.ShortName;            	
            //}
            
            //if(model.ShortCode!=null)
            //{
            //    parameters[4].Value = model.ShortCode;            	
            //}
            
            //if(model.Visible!=null)
            //{
            //    parameters[5].Value = model.Visible;            	
            //}
             
            //if(model.DispOrder!=null)
            //{
            //    parameters[6].Value = model.DispOrder;            	
            //}
            
            //if(model.HisOrderCode!=null)
            //{
            //    parameters[7].Value = model.HisOrderCode;            	
            //}

            //if (model.OldDistrictNo != null)
            //{
            //    parameters[8].Value = model.OldDistrictNo;
            //}

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update WardType set ");
            if (model.CName != null)
            {
                strSql.Append("CName='" + model.CName + "',");
            }
            else
            {
                strSql.Append("CName= null ,");
            }
            if (model.ShortName != null)
            {
                strSql.Append("ShortName='" + model.ShortName + "',");
            }
            else
            {
                strSql.Append("ShortName= null ,");
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
            strSql.Append(" where DistrictNo=" + model.DistrictNo + " and WardNo=" + model.WardNo + " ");

            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());         
            //if (DbHelperSQL.ExecuteNonQuery(strSql.ToString(),parameters) > 0)
            //{
            //   return d_log.OperateLog("WardType", "", "", DateTime.Now, 1);
            //}
            //else
            //    return -1;
		}
		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int DistrictNo,int WardNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from WardType ");
            strSql.Append(" where 1=1 and ROWNUM <= '1' and DistrictNo=" + DistrictNo + " and WardNo=" + WardNo + " ");
            //strSql.Append(" where DistrictNo=@DistrictNo and WardNo=@WardNo ");
            //            SqlParameter[] parameters = {
            //        new SqlParameter("@DistrictNo", SqlDbType.Int,4),
            //        new SqlParameter("@WardNo", SqlDbType.Int,4)};
            //parameters[0].Value = DistrictNo;
            //parameters[1].Value = WardNo;


			return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
		
		}
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from WardType ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
			return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
			
		}
		
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZhiFang.Model.WardType GetModel(int DistrictNo,int WardNo)
		{
			
            //StringBuilder strSql=new StringBuilder();
            //strSql.Append("select DistrictNo, WardNo, CName, ShortName, ShortCode, Visible, DispOrder, HisOrderCode  ");			
            //strSql.Append("  from WardType ");
            //strSql.Append(" where DistrictNo=@DistrictNo and WardNo=@WardNo ");
            //            SqlParameter[] parameters = {
            //        new SqlParameter("@DistrictNo", SqlDbType.Int,4),
            //        new SqlParameter("@WardNo", SqlDbType.Int,4)};
            //parameters[0].Value = DistrictNo;
            //parameters[1].Value = WardNo;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" DistrictNo,WardNo,CName,ShortName,ShortCode,Visible,DispOrder,HisOrderCode ");
            strSql.Append(" from WardType ");
            strSql.Append(" where 1=1 and ROWNUM <= '1' and DistrictNo=" + DistrictNo + " and WardNo=" + WardNo + " ");
			ZhiFang.Model.WardType model=new ZhiFang.Model.WardType();
			DataSet ds=DbHelperSQL.ExecuteDataSet(strSql.ToString());
			
			if(ds.Tables[0].Rows.Count>0)
			{
																
				if(ds.Tables[0].Rows[0]["DistrictNo"].ToString()!="")
				{
					model.DistrictNo=int.Parse(ds.Tables[0].Rows[0]["DistrictNo"].ToString());
				}
																																				
				if(ds.Tables[0].Rows[0]["WardNo"].ToString()!="")
				{
					model.WardNo=int.Parse(ds.Tables[0].Rows[0]["WardNo"].ToString());
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
			strSql.Append(" FROM WardType ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.ExecuteDataSet(strSql.ToString());
		}
		
		
		/// <summary>
		/// 根据实体获取DataSet
		/// </summary>
		public DataSet GetList(ZhiFang.Model.WardType model)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select T1.WardNo,T1.DistrictNo,T2.Cname as 'DCname', T1.CName as 'WCname',T1.ShortName,T1.ShortCode,T1.Visible,T1.DispOrder,T1.HisOrderCode ");
			strSql.Append(" FROM WardType T1 inner join District T2 on T1.DistrictNo=T2.DistrictNo ");
			                            
             
            if(model.DistrictNo !=0)
                        {
                        strSql.Append(" and T1.DistrictNo="+model.DistrictNo+" ");
                        }
                                                    
             
            if(model.WardNo !=0)
                        {
                        strSql.Append(" and T1.WardNo="+model.WardNo+" ");
                        }
                                                    
                        if(model.CName !=null)
                        {
                        strSql.Append(" and T1.CName='"+model.CName+"' ");
                        }
                        if (model.CName != null)
                        {
                            strSql.Append(" and T2.CName='" + model.CName + "' ");
                        }
                                                    
                        if(model.ShortName !=null)
                        {
                        strSql.Append(" and T1.ShortName='"+model.ShortName+"' ");
                        }
                                                    
                        if(model.ShortCode !=null)
                        {
                        strSql.Append(" and T1.ShortCode='"+model.ShortCode+"' ");
                        }
                                                                
                        if(model.DispOrder !=null)
                        {
                        strSql.Append(" and T1.DispOrder="+model.DispOrder+" ");
                        }
                                                    
                        if(model.HisOrderCode !=null)
                        {
                        strSql.Append(" and T1.HisOrderCode='"+model.HisOrderCode+"' ");
                        }
                                    return DbHelperSQL.ExecuteDataSet(strSql.ToString());
		}
		public DataSet GetListLike(ZhiFang.Model.WardType  model)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select WardType.*,DistrictNo,WardNo as LabNo,concat(concat(WardNo,'_'),CNAME) as LabNo_Value,concat(concat(concat(concat(CName,'('),DistrictNo),WardNo),')') as LabNoAndName_Text ");
			strSql.Append(" FROM WardType where 1=1 ");
			if (model.CName != null)
            {
                strSql.Append(" where 1=2 ");
                strSql.Append(" or CName like '%" + model.CName + "%' ");
            }

            if (model.DistrictNo != 0)
            {                
                strSql.Append(" or DistrictNo like '%" + model.DistrictNo + "%' ");
            }      

            if (model.WardNo != 0)
            {                
                strSql.Append(" or WardNo like '%" + model.WardNo + "%' ");
            } 

            if (model.ShortName != null)
            {
                strSql.Append(" or ShortName like '%" + model.ShortName + "%' ");
            }

            if (model.ShortCode != null)
            {
                strSql.Append(" or ShortCode like '%" + model.ShortCode + "%' ");
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
		}
		
		/// <summary>
		/// 获取总记录
		/// </summary>
		public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM WardType ");
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
        public int GetTotalCount(ZhiFang.Model.WardType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM WardType where 1=1 ");
           	                         
                        
            if(model.DistrictNo !=0)
            {            
            	strSql.Append(" and DistrictNo='"+model.DistrictNo+"' ");
            }
                                        
                        
            if(model.WardNo !=0)
            {            
            	strSql.Append(" and WardNo='"+model.WardNo+"' ");
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
            	strSql.Append(" and DispOrder='"+model.DispOrder+"' ");
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
        public DataSet GetListByPage(ZhiFang.Model.WardType model, int nowPageNum, int nowPageSize)
        {
            string strLike = "";
            StringBuilder strSql = new StringBuilder();
           // strSql.Append(" select  * from WardType left join WardTypeControl on WardType.WardTypeNo=WardTypeControl.WardTypeNo ");
            if (model != null && model.LabCode != null)
            {
                //if (model.SearchLikeKey != null)
                //{
                //    strLike = " and (WardType.DistrictNo like '%" + model.SearchLikeKey + "%'or WardType.WardNo like '%" + model.SearchLikeKey + "%' or WardType.CName like '%" + model.SearchLikeKey + "%' or WardType.ShortCode like '%" + model.SearchLikeKey + "%') ";
                //}
                strSql.Append(" select  * from WardType left join WardTypeControl on WardType.DistrictNo,WardNo=WardTypeControl.DistrictNo,WardNo ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and WardTypeControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append("where  ROWNUM <= '" + nowPageSize + "' and DistrictNo,WardNo not in ( ");
                strSql.Append("select DistrictNo,WardNo from  WardType left join WardTypeControl on WardType.DistrictNo,WardNo=WardTypeControl.DistrictNo,WardNo ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and WardTypeControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append(" where 1=1 and ROWNUM <= '" + (nowPageSize * nowPageNum) + "' " + strLike + " ) " + strLike + " order by WardType." + model.OrderField + " ");
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }

            else
            {
                //if (model.SearchLikeKey != null)
                //{
                //    strLike = " and (DistrictNo like '%" + model.SearchLikeKey + "%' or WardNo like '%" + model.SearchLikeKey + "%' or CName like '%" + model.SearchLikeKey + "%' or ShortCode like '%" + model.SearchLikeKey + "%' or ShortName like '%" + model.SearchLikeKey + "%') ";
                //}
                strSql.Append("select T1.WardNo,T1.DistrictNo,T2.Cname as 'DCname', T1.CName as 'WCname',T1.ShortName,T1.ShortCode,T1.Visible,T1.DispOrder,T1.HisOrderCode from WardType T1 inner join District T2 on T1.DistrictNo=T2.DistrictNo where  ROWNUM <= '" + nowPageSize + "' and cast(T2.DistrictNo as varchar)+'-'+cast(WardNo as varchar) not in ");
                strSql.Append("(select cast(T2.DistrictNo as varchar)+'-'+cast(WardNo as varchar) from WardType T1 inner join District T2 on T1.DistrictNo=T2.DistrictNo where 1=1 and ROWNUM <= '" + (nowPageSize * nowPageNum) + "' )" + " order by " + model.OrderField + " ");

                #region
                /*strSql.Append("select top " + nowPageSize + "  * from WardType where  DistrictNo+'_'+WardNo not in  ");
                strSql.Append("(select top " + (nowPageSize * nowPageNum) + "  DistrictNo+'_'+WardNo from WardType where 1=1 " + strLike + " order by " + model.OrderField + ") " + strLike + " order by " + model.OrderField + "  ");
                 */
                #endregion
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
            #region
            /*
            StringBuilder strSql = new StringBuilder();
            if (model != null && model.LabCode != null)
            {
                strSql.Append(" select top " + nowPageSize + "  * from WardType left join WardTypeControl on WardType.DistrictNo,WardNo=WardTypeControl.DistrictNo,WardNo ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and WardTypeControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append("where DistrictNo,WardNo not in ( ");
                strSql.Append("select top " + (nowPageSize * nowPageNum ) + " DistrictNo,WardNo from  WardType left join WardTypeControl on WardType.DistrictNo,WardNo=WardTypeControl.DistrictNo,WardNo ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and WardTypeControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append("order by WardType.DistrictNo,WardNo ) order by WardType.DistrictNo,WardNo ");
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
            else
            {
                string likesql = "";
                if (model.SearchLikeKey != null && model.SearchLikeKey.Trim() != "")
                {
                    likesql = " and  (  CNAME like '%" + model.SearchLikeKey + "%'  or ShortName like '%" + model.SearchLikeKey + "%'  or SHORTCODE like '%" + model.SearchLikeKey + "%') ";
                }
                strSql.Append("select top " + nowPageSize + "  * from WardType where DistrictNo+'_'+WardNo not in  ");
                strSql.Append("(select top " + (nowPageSize * nowPageNum) + " DistrictNo+'_'+WardNo from WardType  where 1=1 " + likesql + "  order by DistrictNo,WardNo)");
                strSql.Append(likesql);
                strSql.Append("order by DistrictNo,WardNo  ");
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
             */
            #endregion
        }
        
        public bool Exists(int DistrictNo,int WardNo)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from WardType ");
            strSql.Append(" where DistrictNo ='" + DistrictNo + "' and WardNo ='" +  WardNo + "'");
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
			string LabTableName="WardType";
			LabTableName="B_Lab_"+LabTableName.Substring(LabTableName.IndexOf("_") + 1, LabTableName.Length - (LabTableName.IndexOf("_") + 1));
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSqlControl = new StringBuilder();
            string TableKey="DistrictNo,WardNo";
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
	            		strSql.Append(" LabDistrictNo , LabWardNo , CName , ShortName , ShortCode , Visible , DispOrder , HisOrderCode ");
						strSql.Append(") select '" + lst[i].Trim() + "' as LabCode, ");
	            		strSql.Append("DistrictNo,WardNo,CName,ShortName,ShortCode,Visible,DispOrder,HisOrderCode");            
	            		strSql.Append(" from WardType ");    
	            		
	            		strSqlControl.Append("insert into WardTypeControl ( ");
	            		strSqlControl.Append(" "+TableKeySub+"ControlNo,"+TableKey+",ControlLabNo,Control"+TableKey+",UseFlag ");
	            		strSqlControl.Append(")  select ");
                        strSqlControl.Append("  concat(concat(concat(concat(" + lst[i].Trim() + ",'_')," + TableKey + "),'_')," + TableKey + ") as " + TableKeySub + "ControlNo," + TableKey + ",'" + lst[i].Trim() + "' as ControlLabNo," + TableKey + ",UseFlag ");
	            		strSqlControl.Append(" from WardType ");  
	            		
	            		arrySql.Add(strSql.ToString());
	            		arrySql.Add(strSqlControl.ToString());	    
	            		
	            		strSql = new StringBuilder();
             			strSqlControl = new StringBuilder();           			
	             		
	             }
	             
	             DbHelperSQL.BatchUpdateWithTransaction(arrySql);
                 //d_log.OperateLog("WardType", "", "", DateTime.Now, 1); 
	             return true;
            }
            catch
            {
            	return false;
            }
           
		}
		
		public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("DistrictNo,WardNo","WardType");
        }

        public DataSet GetList(int Top, ZhiFang.Model.WardType model, string filedOrder)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			
			strSql.Append(" * ");
			strSql.Append(" FROM WardType ");
            strSql.Append(" where 1=1 ");
			                              
                        
            if(model.DistrictNo !=0)
            {            
            	strSql.Append(" and DistrictNo='"+model.DistrictNo+"' ");
            }
                                            
                        
            if(model.WardNo !=0)
            {            
            	strSql.Append(" and WardNo='"+model.WardNo+"' ");
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
            	strSql.Append(" and DispOrder='"+model.DispOrder+"' ");
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
        
        #region IDGetListByTimeStampe 成员

        public DataSet GetListByTimeStampe(string LabCode, int dTimeStampe)
        {
            DataSet dsAll = new DataSet();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select WardType.*,'" + LabCode + "' as LabCode,DistrictNo,WardNo as LabDistrictNo,WardNo from WardType where 1=1 ");
            if (dTimeStampe != -999999)
            {
                strSql.Append(" and TO_CHAR(DTimeStampe,'YYYY-MM-DD:HH24:MI:SS') > '" + dTimeStampe + "' ");
            }
            DataTable dtServer = DbHelperSQL.ExecuteDataSet(strSql.ToString()).Tables[0];
            dtServer.TableName = "ServerDatas";

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("select B_Lab_WardType.*,LabDistrictNo,WardNo as DistrictNo,WardNo from B_Lab_WardType where 1=1 ");
            if (LabCode.Trim() != "")
            {
                strSql2.Append(" and LabCode= '" + LabCode.Trim() + "' ");
            }
            if (dTimeStampe != -999999)
            {
                strSql2.Append(" and TO_CHAR(DTimeStampe,'YYYY-MM-DD:HH24:MI:SS') > '" + dTimeStampe + "' ");
            }
            DataTable dtLab = DbHelperSQL.ExecuteDataSet(strSql2.ToString()).Tables[0];
            dtLab.TableName = "LabDatas";

            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("select * from B_WardTypeControl where 1=1 ");
            if (LabCode.Trim() != "")
            {
                strSql3.Append(" and ControlLabNo= '" + LabCode.Trim() + "' ");
            }
            if (dTimeStampe != -999999)
            {
                strSql3.Append(" and TO_CHAR(DTimeStampe,'YYYY-MM-DD:HH24:MI:SS') > '" + dTimeStampe + "' ");
            }
            DataTable dtControl = DbHelperSQL.ExecuteDataSet(strSql3.ToString()).Tables[0];
            dtControl.TableName = "ControlDatas";

            dsAll.Tables.Add(dtServer.Copy());
            dsAll.Tables.Add(dtLab.Copy());
            dsAll.Tables.Add(dtControl.Copy());
            return dsAll;
        }

        #endregion
        
        #region IDBatchCopy 成员

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

                        if (this.Exists(int.Parse(ds.Tables[0].Rows[i]["DistrictNo"].ToString().Trim()), int.Parse(ds.Tables[0].Rows[i]["WardNo"].ToString().Trim())))
                        {
                            count += this.UpdateByDataRow(dr);
                        }
                        else
                        {
                            count += this.AddByDataRow(dr);
                        }
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
                strSql.Append("insert into WardType (");
                strSql.Append("DistrictNo,WardNo,CName,ShortName,ShortCode,Visible,DispOrder,HisOrderCode");
                strSql.Append(") values (");

                if (dr.Table.Columns["DistrictNo"] != null && dr.Table.Columns["DistrictNo"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["DistrictNo"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["WardNo"] != null && dr.Table.Columns["WardNo"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["WardNo"].ToString().Trim() + "', ");
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

                if (dr.Table.Columns["ShortName"] != null && dr.Table.Columns["ShortName"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["ShortName"].ToString().Trim() + "', ");
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
                    strSql.Append(" '" + dr["HisOrderCode"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                int n = strSql.ToString().LastIndexOf(",");
                strSql.Remove(n, 1);
                strSql.Append(") ");
                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.WardType.AddByDataRow 同步数据时异常：", ex);
                return 0;
            }
        }
        public int UpdateByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update WardType set ");


                if (dr.Table.Columns["CName"] != null && dr.Table.Columns["CName"].ToString().Trim() != "")
                {
                    strSql.Append(" CName = '" + dr["CName"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["ShortName"] != null && dr.Table.Columns["ShortName"].ToString().Trim() != "")
                {
                    strSql.Append(" ShortName = '" + dr["ShortName"].ToString().Trim() + "' , ");
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
                    strSql.Append(" HisOrderCode = '" + dr["HisOrderCode"].ToString().Trim() + "' , ");
                }


                int n = strSql.ToString().LastIndexOf(",");
                strSql.Remove(n, 1);
                strSql.Append(" where DistrictNo='" + dr["DistrictNo"].ToString().Trim() + "' and WardNo='" + dr["WardNo"].ToString().Trim() + "' ");

                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.WardType .UpdateByDataRow同步数据时异常：", ex);
                return 0;
            }
        }
        public int DeleteByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                if (dr.Table.Columns["DistrictNo"] != null && dr.Table.Columns["DistrictNo"].ToString().Trim() != "" && dr.Table.Columns["WardNo"] != null && dr.Table.Columns["WardNo"].ToString().Trim() != "")
                {
                    strSql.Append("delete from WardType ");
                    strSql.Append(" where DistrictNo='" + dr["DistrictNo"].ToString().Trim() + "' and  WardNo='" + dr["WardNo"].ToString().Trim() + "' ");
                    return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.WardType .DeleteByDataRow同步数据时异常：", ex);
                return 0;
            }
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

