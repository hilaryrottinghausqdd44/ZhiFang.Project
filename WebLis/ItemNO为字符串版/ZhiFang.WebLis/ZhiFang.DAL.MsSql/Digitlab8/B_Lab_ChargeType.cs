using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Digitlab8  
{
	 	//B_Lab_ChargeType
		
	public partial class B_Lab_ChargeType: IDLab_ChargeType	{	
		DBUtility.IDBConnection idb;
        public B_Lab_ChargeType(string dbsourceconn)
        {
            idb = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
		public B_Lab_ChargeType()
		{
            idb = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.LisDB());
        }
		D_LogInfo d_log = new D_LogInfo();
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ZhiFang.Model.Lab_ChargeType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into B_Lab_ChargeType(");			
            strSql.Append("LabCode,LabChargeNo,CName,ChargeTypeDesc,Discount,Append,ShortCode,Visible,DispOrder,HisOrderCode,StandCode,ZFStandCode,UseFlag");
			strSql.Append(") values (");
            strSql.Append("@LabCode,@LabChargeNo,@CName,@ChargeTypeDesc,@Discount,@Append,@ShortCode,@Visible,@DispOrder,@HisOrderCode,@StandCode,@ZFStandCode,@UseFlag");            
            strSql.Append(") ");            
            
			SqlParameter[] parameters = {
			            new SqlParameter("@LabCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@LabChargeNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@CName", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@ChargeTypeDesc", SqlDbType.VarChar,250) ,            
                        new SqlParameter("@Discount", SqlDbType.Float,8) ,            
                        new SqlParameter("@Append", SqlDbType.Float,8) ,            
                        new SqlParameter("@ShortCode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@Visible", SqlDbType.Int,4) ,            
                        new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            
                        new SqlParameter("@HisOrderCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ZFStandCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@UseFlag", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.LabCode;                        
            parameters[1].Value = model.LabChargeNo;                        
            parameters[2].Value = model.CName;                        
            parameters[3].Value = model.ChargeTypeDesc;                        
            parameters[4].Value = model.Discount;                        
            parameters[5].Value = model.Append;                        
            parameters[6].Value = model.ShortCode;                        
            parameters[7].Value = model.Visible;                        
            parameters[8].Value = model.DispOrder;                        
            parameters[9].Value = model.HisOrderCode;                        
            parameters[10].Value = model.StandCode;                        
            parameters[11].Value = model.ZFStandCode;                        
            parameters[12].Value = model.UseFlag;                  
			if (idb.ExecuteNonQuery(strSql.ToString(),parameters) > 0)
            {
                return d_log.OperateLog("ChargeType", "", "", DateTime.Now, 1);
            }
            else
                return -1;
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ZhiFang.Model.Lab_ChargeType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update B_Lab_ChargeType set ");
			                                                
            strSql.Append(" LabCode = @LabCode , ");                                    
            strSql.Append(" LabChargeNo = @LabChargeNo , ");                                    
            strSql.Append(" CName = @CName , ");                                    
            strSql.Append(" ChargeTypeDesc = @ChargeTypeDesc , ");                                    
            strSql.Append(" Discount = @Discount , ");                                    
            strSql.Append(" Append = @Append , ");                                    
            strSql.Append(" ShortCode = @ShortCode , ");                                    
            strSql.Append(" Visible = @Visible , ");                                    
            strSql.Append(" DispOrder = @DispOrder , ");                                    
            strSql.Append(" HisOrderCode = @HisOrderCode , ");                                                                                    
            strSql.Append(" StandCode = @StandCode , ");                                    
            strSql.Append(" ZFStandCode = @ZFStandCode , ");                                    
            strSql.Append(" UseFlag = @UseFlag  ");            			
			strSql.Append(" where LabCode=@LabCode and LabChargeNo=@LabChargeNo  ");
						
			SqlParameter[] parameters = {
			            	
                           
            new SqlParameter("@LabCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@LabChargeNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@CName", SqlDbType.VarChar,10) ,            	
                           
            new SqlParameter("@ChargeTypeDesc", SqlDbType.VarChar,250) ,            	
                           
            new SqlParameter("@Discount", SqlDbType.Float,8) ,            	
                           
            new SqlParameter("@Append", SqlDbType.Float,8) ,            	
                           
            new SqlParameter("@ShortCode", SqlDbType.VarChar,10) ,            	
                           
            new SqlParameter("@Visible", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@HisOrderCode", SqlDbType.VarChar,20) ,            	
                        	
                        	
                           
            new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@ZFStandCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@UseFlag", SqlDbType.Int,4)             	
              
            };
            			    
				
                
			   
			if(model.LabCode!=null)
			{
            	parameters[0].Value = model.LabCode;            	
            }
            	
                
			   
			if(model.LabChargeNo!=null)
			{
            	parameters[1].Value = model.LabChargeNo;            	
            }
            	
                
			   
			if(model.CName!=null)
			{
            	parameters[2].Value = model.CName;            	
            }
            	
                
			   
			if(model.ChargeTypeDesc!=null)
			{
            	parameters[3].Value = model.ChargeTypeDesc;            	
            }
            	
                
			   
			if(model.Discount!=null)
			{
            	parameters[4].Value = model.Discount;            	
            }
            	
                
			   
			if(model.Append!=null)
			{
            	parameters[5].Value = model.Append;            	
            }
            	
                
			   
			if(model.ShortCode!=null)
			{
            	parameters[6].Value = model.ShortCode;            	
            }
            	
                
			   
			if(model.Visible!=null)
			{
            	parameters[7].Value = model.Visible;            	
            }
            	
                
			   
			if(model.DispOrder!=null)
			{
            	parameters[8].Value = model.DispOrder;            	
            }
            	
                
			   
			if(model.HisOrderCode!=null)
			{
            	parameters[9].Value = model.HisOrderCode;            	
            }
            	
                
				
                
				
                
			   
			if(model.StandCode!=null)
			{
            	parameters[10].Value = model.StandCode;            	
            }
            	
                
			   
			if(model.ZFStandCode!=null)
			{
            	parameters[11].Value = model.ZFStandCode;            	
            }
            	
                
			   
			if(model.UseFlag!=null)
			{
            	parameters[12].Value = model.UseFlag;            	
            }
            	
                        
			if (idb.ExecuteNonQuery(strSql.ToString(),parameters) > 0)
            {
                return d_log.OperateLog("ChargeType", "", "", DateTime.Now, 1);
            }
            else
                return -1;
		}
		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(string LabCode,int LabChargeNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from B_Lab_ChargeType ");
			strSql.Append(" where LabCode=@LabCode and LabChargeNo=@LabChargeNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@LabCode", SqlDbType.VarChar,50),
					new SqlParameter("@LabChargeNo", SqlDbType.Int,4)};
			parameters[0].Value = LabCode;
			parameters[1].Value = LabChargeNo;


			return idb.ExecuteNonQuery(strSql.ToString(),parameters);
			
		}
		
				/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteList(string ChargeIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from B_Lab_ChargeType ");
			strSql.Append(" where ID in ("+ChargeIDlist + ")  ");
			return idb.ExecuteNonQuery(strSql.ToString());
			
		}
				
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZhiFang.Model.Lab_ChargeType GetModel(string LabCode,int LabChargeNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ChargeID, LabCode, LabChargeNo, CName, ChargeTypeDesc, Discount, Append, ShortCode, Visible, DispOrder, HisOrderCode, DTimeStampe, AddTime, StandCode, ZFStandCode, UseFlag  ");			
			strSql.Append("  from B_Lab_ChargeType ");
			strSql.Append(" where LabCode=@LabCode and LabChargeNo=@LabChargeNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@LabCode", SqlDbType.VarChar,50),
					new SqlParameter("@LabChargeNo", SqlDbType.Int,4)};
			parameters[0].Value = LabCode;
			parameters[1].Value = LabChargeNo;

			
			ZhiFang.Model.Lab_ChargeType model=new ZhiFang.Model.Lab_ChargeType();
			DataSet ds=idb.ExecuteDataSet(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																if(ds.Tables[0].Rows[0]["ChargeID"].ToString()!="")
				{
					model.ChargeID=int.Parse(ds.Tables[0].Rows[0]["ChargeID"].ToString());
				}
																																								model.LabCode= ds.Tables[0].Rows[0]["LabCode"].ToString();
																																if(ds.Tables[0].Rows[0]["LabChargeNo"].ToString()!="")
				{
					model.LabChargeNo=int.Parse(ds.Tables[0].Rows[0]["LabChargeNo"].ToString());
				}
																																								model.CName= ds.Tables[0].Rows[0]["CName"].ToString();
																																				model.ChargeTypeDesc= ds.Tables[0].Rows[0]["ChargeTypeDesc"].ToString();
																																if(ds.Tables[0].Rows[0]["Discount"].ToString()!="")
				{
					model.Discount=decimal.Parse(ds.Tables[0].Rows[0]["Discount"].ToString());
				}
																																				if(ds.Tables[0].Rows[0]["Append"].ToString()!="")
				{
					model.Append=decimal.Parse(ds.Tables[0].Rows[0]["Append"].ToString());
				}
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
			strSql.Append(" FROM B_Lab_ChargeType ");
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
			strSql.Append(" FROM B_Lab_ChargeType ");
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
		public DataSet GetList(ZhiFang.Model.Lab_ChargeType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM B_Lab_ChargeType where 1=1 ");
			                                                                
                        if(model.LabCode !=null)
                        {
                        strSql.Append(" and LabCode='"+model.LabCode+"' ");
                        }
                                                    
             
            if(model.LabChargeNo !=0)
                        {
                        strSql.Append(" and LabChargeNo="+model.LabChargeNo+" ");
                        }
                                                    
                        if(model.CName !=null)
                        {
                        strSql.Append(" and CName='"+model.CName+"' ");
                        }
                                                    
                        if(model.ChargeTypeDesc !=null)
                        {
                        strSql.Append(" and ChargeTypeDesc='"+model.ChargeTypeDesc+"' ");
                        }
                                                    
                        if(model.Discount !=null)
                        {
                        strSql.Append(" and Discount="+model.Discount+" ");
                        }
                                                    
                        if(model.Append !=null)
                        {
                        strSql.Append(" and Append="+model.Append+" ");
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
        public DataSet GetListByLike(ZhiFang.Model.Lab_ChargeType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *,'('+convert(varchar(100),LabChargeNo)+')'+CName as LabChargeNoAndName ");
            strSql.Append(" FROM B_Lab_ChargeType where 1=1 ");
            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }

            if (model.CName != null)
            {
                strSql.Append(" and ( CName like '%" + model.CName + "%' ");
            }
            if (model.LabChargeNo != 0)
            {
                strSql.Append(" or LabChargeNo like '%" + model.LabChargeNo + "%' ");
            }

            if (model.ChargeTypeDesc != null)
            {
                strSql.Append(" or ChargeTypeDesc like '%" + model.ChargeTypeDesc + "%' ");
            }

            if (model.Discount != null)
            {
                strSql.Append(" or Discount like '%" + model.Discount + "%' ");
            }

            if (model.Append != null)
            {
                strSql.Append(" or Append like '%" + model.Append + "%' ");
            }

            if (model.ShortCode != null)
            {
                strSql.Append(" or ShortCode like '%" + model.ShortCode + "%' ");
            }

            if (model.DispOrder != null)
            {
                strSql.Append(" or DispOrder like '%" + model.DispOrder + "%' ");
            }

            if (model.HisOrderCode != null)
            {
                strSql.Append(" or HisOrderCode like '%" + model.HisOrderCode + "%' ");
            }

            if (model.StandCode != null)
            {
                strSql.Append(" or StandCode like '%" + model.StandCode + "%' ");
            }

            if (model.ZFStandCode != null)
            {
                strSql.Append(" or ZFStandCode like '%" + model.ZFStandCode + "%' ");
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
            strSql.Append("select count(*) FROM B_Lab_ChargeType ");
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
        public int GetTotalCount(ZhiFang.Model.Lab_ChargeType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_Lab_ChargeType where 1=1 ");
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
        public DataSet GetListByPage(ZhiFang.Model.Lab_ChargeType model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top " + nowPageSize + " * from B_Lab_ChargeType where 1=1  ");
                                                
                                                                        
                  
                        if(model.LabCode !=null)
                        {
                        strSql.Append(" and LabCode='"+model.LabCode+"' ");
                        }
                                                                        if(model.LabChargeNo !=0)
            {
            	 strSql.Append(" and LabChargeNo="+model.LabChargeNo+" ");
            }
                                                            
                  
                        if(model.CName !=null)
                        {
                        strSql.Append(" and CName='"+model.CName+"' ");
                        }
                                                                        
                  
                        if(model.ChargeTypeDesc !=null)
                        {
                        strSql.Append(" and ChargeTypeDesc='"+model.ChargeTypeDesc+"' ");
                        }
                                                                        
                  
                        if(model.Discount !=null)
                        {
                        strSql.Append(" and Discount="+model.Discount+" ");
                        }
                                                                        
                  
                        if(model.Append !=null)
                        {
                        strSql.Append(" and Append="+model.Append+" ");
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
                                                            strSql.Append("and ChargeID not in ");
            strSql.Append("(select top " + (nowPageSize * nowPageNum) + " ChargeID from B_Lab_ChargeType where 1=1  ");
            strSql.Append(" order by ChargeID) order by ChargeID  ");
            return idb.ExecuteDataSet(strSql.ToString());
        }
        
        public bool Exists(string LabCode,int LabChargeNo)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from B_Lab_ChargeType ");
			strSql.Append(" where LabCode=@LabCode and LabChargeNo=@LabChargeNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@LabCode", SqlDbType.VarChar,50),
					new SqlParameter("@LabChargeNo", SqlDbType.Int,4)};
			parameters[0].Value = LabCode;
			parameters[1].Value = LabChargeNo;


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
		
		public int GetMaxId()
        {
            return idb.GetMaxID("LabCode,LabChargeNo","B_Lab_ChargeType");
        }

        public DataSet GetList(int Top, ZhiFang.Model.Lab_ChargeType model, string filedOrder)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" * ");
			strSql.Append(" FROM B_Lab_ChargeType ");		
			
			                                          
            if(model.LabCode !=null)
            {
                        
            strSql.Append(" and LabCode='"+model.LabCode+"' ");
                        }
                                          
            if(model.LabChargeNo !=null)
            {
                        strSql.Append(" and LabChargeNo="+model.LabChargeNo+" ");
                        }
                                          
            if(model.CName !=null)
            {
                        
            strSql.Append(" and CName='"+model.CName+"' ");
                        }
                                          
            if(model.ChargeTypeDesc !=null)
            {
                        
            strSql.Append(" and ChargeTypeDesc='"+model.ChargeTypeDesc+"' ");
                        }
                                          
            if(model.Discount !=null)
            {
                        strSql.Append(" and Discount="+model.Discount+" ");
                        }
                                          
            if(model.Append !=null)
            {
                        strSql.Append(" and Append="+model.Append+" ");
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



        #region IDataBase<Lab_ChargeType> 成员
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
                        if (this.Exists(ds.Tables[0].Rows[i]["LabCode"].ToString().Trim(),int.Parse(ds.Tables[0].Rows[i]["LabChargeNo"].ToString().Trim())))
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
                strSql.Append("insert into B_Lab_ChargeType (");
                strSql.Append("LabCode,LabChargeNo,CName,ChargeTypeDesc,Discount,Append,ShortCode,Visible,DispOrder,HisOrderCode,AddTime,StandCode,ZFStandCode,UseFlag");
                strSql.Append(") values (");
                strSql.Append("'" + dr["LabCode"].ToString().Trim() + "','" + dr["LabChargeNo"].ToString().Trim() + "','" + dr["CName"].ToString().Trim() + "','" + dr["ChargeTypeDesc"].ToString().Trim() + "','" + dr["Discount"].ToString().Trim() + "','" + dr["Append"].ToString().Trim() + "','" + dr["ShortCode"].ToString().Trim() + "','" + dr["Visible"].ToString().Trim() + "','" + dr["DispOrder"].ToString().Trim() + "','" + dr["HisOrderCode"].ToString().Trim() + "','" + DateTime.Parse(dr["AddTime"].ToString().Trim()) + "','" + dr["StandCode"].ToString().Trim() + "','" + dr["ZFStandCode"].ToString().Trim() + "','" + dr["UseFlag"].ToString().Trim() + "'");
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
                strSql.Append("update B_Lab_ChargeType set ");

                strSql.Append(" CName = '" + dr["CName"].ToString().Trim() + "' , ");
                strSql.Append(" ChargeTypeDesc = '" + dr["ChargeTypeDesc"].ToString().Trim() + "' , ");
                strSql.Append(" Discount = '" + dr["Discount"].ToString().Trim() + "' , ");
                strSql.Append(" Append = '" + dr["Append"].ToString().Trim() + "' , ");
                strSql.Append(" ShortCode = '" + dr["ShortCode"].ToString().Trim() + "' , ");
                strSql.Append(" Visible = '" + dr["Visible"].ToString().Trim() + "' , ");
                strSql.Append(" DispOrder = '" + dr["DispOrder"].ToString().Trim() + "' , ");
                strSql.Append(" HisOrderCode = '" + dr["HisOrderCode"].ToString().Trim() + "' , ");
                strSql.Append(" StandCode = '" + dr["StandCode"].ToString().Trim() + "' , ");
                strSql.Append(" ZFStandCode = '" + dr["ZFStandCode"].ToString().Trim() + "' , ");
                strSql.Append(" UseFlag = '" + dr["UseFlag"].ToString().Trim() + "'  ");
                strSql.Append(" where LabCode='" + dr["LabCode"].ToString().Trim() + "' and LabChargeNo='" + dr["LabChargeNo"].ToString().Trim() + "' ");

                return idb.ExecuteNonQuery(strSql.ToString());
            }
            catch
            {
                return 0;
            }
        }
        #endregion
    }
}

