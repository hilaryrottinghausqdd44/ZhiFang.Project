using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Digitlab8  
{
	 	//B_Equipment
		
	public partial class B_Equipment : IDEquipment,IDBatchCopy,IDGetListByTimeStampe
	{	
		DBUtility.IDBConnection idb;
        public B_Equipment(string dbsourceconn)
        {
            idb = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
		public B_Equipment()
		{
            idb = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.LisDB());
        }
		D_LogInfo d_log = new D_LogInfo();
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ZhiFang.Model.Equipment model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into B_Equipment(");			
            strSql.Append("EquipNo,CName,ShortName,ShortCode,SectionNo,Computer,ComProgram,ComPort,BaudRate,Parity,DataBits,StopBits,Visible,DoubleDir,LicenceKey,LicenceType,LicenceDate,SQH,SNo,SickType,UseImmPlate,ImmCalc,CommPara,ReagentPara,StandCode,ZFStandCode,UseFlag");
			strSql.Append(") values (");
            strSql.Append("@EquipNo,@CName,@ShortName,@ShortCode,@SectionNo,@Computer,@ComProgram,@ComPort,@BaudRate,@Parity,@DataBits,@StopBits,@Visible,@DoubleDir,@LicenceKey,@LicenceType,@LicenceDate,@SQH,@SNo,@SickType,@UseImmPlate,@ImmCalc,@CommPara,@ReagentPara,@StandCode,@ZFStandCode,@UseFlag");            
            strSql.Append(") ");            
            
			SqlParameter[] parameters = {
			            new SqlParameter("@EquipNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@CName", SqlDbType.VarChar,40) ,            
                        new SqlParameter("@ShortName", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@ShortCode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@SectionNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@Computer", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ComProgram", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ComPort", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@BaudRate", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@Parity", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@DataBits", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@StopBits", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@Visible", SqlDbType.Int,4) ,            
                        new SqlParameter("@DoubleDir", SqlDbType.Int,4) ,            
                        new SqlParameter("@LicenceKey", SqlDbType.VarChar,30) ,            
                        new SqlParameter("@LicenceType", SqlDbType.VarChar,25) ,            
                        new SqlParameter("@LicenceDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@SQH", SqlDbType.VarChar,4) ,            
                        new SqlParameter("@SNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@SickType", SqlDbType.Int,4) ,            
                        new SqlParameter("@UseImmPlate", SqlDbType.Int,4) ,            
                        new SqlParameter("@ImmCalc", SqlDbType.Int,4) ,            
                        new SqlParameter("@CommPara", SqlDbType.VarChar,800) ,            
                        new SqlParameter("@ReagentPara", SqlDbType.VarChar,800) ,            
                        new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ZFStandCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@UseFlag", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.EquipNo;                        
            parameters[1].Value = model.CName;                        
            parameters[2].Value = model.ShortName;                        
            parameters[3].Value = model.ShortCode;                        
            parameters[4].Value = model.SectionNo;                        
            parameters[5].Value = model.Computer;                        
            parameters[6].Value = model.ComProgram;                        
            parameters[7].Value = model.ComPort;                        
            parameters[8].Value = model.BaudRate;                        
            parameters[9].Value = model.Parity;                        
            parameters[10].Value = model.DataBits;                        
            parameters[11].Value = model.StopBits;                        
            parameters[12].Value = model.Visible;                        
            parameters[13].Value = model.DoubleDir;                        
            parameters[14].Value = model.LicenceKey;                        
            parameters[15].Value = model.LicenceType;                        
            parameters[16].Value = model.LicenceDate;                        
            parameters[17].Value = model.SQH;                        
            parameters[18].Value = model.SNo;                        
            parameters[19].Value = model.SickType;                        
            parameters[20].Value = model.UseImmPlate;                        
            parameters[21].Value = model.ImmCalc;                        
            parameters[22].Value = model.CommPara;                        
            parameters[23].Value = model.ReagentPara;                        
            parameters[24].Value = model.StandCode;                        
            parameters[25].Value = model.ZFStandCode;                        
            parameters[26].Value = model.UseFlag;                  
			if (idb.ExecuteNonQuery(strSql.ToString(),parameters) > 0)
            {
                return d_log.OperateLog("Equipment", "", "", DateTime.Now, 1);
            }
            else
                return -1;
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ZhiFang.Model.Equipment model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update B_Equipment set ");
			                                                
            strSql.Append(" EquipNo = @EquipNo , ");                                    
            strSql.Append(" CName = @CName , ");                                    
            strSql.Append(" ShortName = @ShortName , ");                                    
            strSql.Append(" ShortCode = @ShortCode , ");                                    
            strSql.Append(" SectionNo = @SectionNo , ");                                    
            strSql.Append(" Computer = @Computer , ");                                    
            strSql.Append(" ComProgram = @ComProgram , ");                                    
            strSql.Append(" ComPort = @ComPort , ");                                    
            strSql.Append(" BaudRate = @BaudRate , ");                                    
            strSql.Append(" Parity = @Parity , ");                                    
            strSql.Append(" DataBits = @DataBits , ");                                    
            strSql.Append(" StopBits = @StopBits , ");                                    
            strSql.Append(" Visible = @Visible , ");                                    
            strSql.Append(" DoubleDir = @DoubleDir , ");                                    
            strSql.Append(" LicenceKey = @LicenceKey , ");                                    
            strSql.Append(" LicenceType = @LicenceType , ");                                    
            strSql.Append(" LicenceDate = @LicenceDate , ");                                    
            strSql.Append(" SQH = @SQH , ");                                    
            strSql.Append(" SNo = @SNo , ");                                    
            strSql.Append(" SickType = @SickType , ");                                    
            strSql.Append(" UseImmPlate = @UseImmPlate , ");                                    
            strSql.Append(" ImmCalc = @ImmCalc , ");                                    
            strSql.Append(" CommPara = @CommPara , ");                                    
            strSql.Append(" ReagentPara = @ReagentPara , ");                                                                                    
            strSql.Append(" StandCode = @StandCode , ");                                    
            strSql.Append(" ZFStandCode = @ZFStandCode , ");                                    
            strSql.Append(" UseFlag = @UseFlag  ");            			
			strSql.Append(" where EquipNo=@EquipNo  ");
						
			SqlParameter[] parameters = {
			            	
                           
            new SqlParameter("@EquipNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@CName", SqlDbType.VarChar,40) ,            	
                           
            new SqlParameter("@ShortName", SqlDbType.VarChar,10) ,            	
                           
            new SqlParameter("@ShortCode", SqlDbType.VarChar,10) ,            	
                           
            new SqlParameter("@SectionNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@Computer", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@ComProgram", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@ComPort", SqlDbType.VarChar,10) ,            	
                           
            new SqlParameter("@BaudRate", SqlDbType.VarChar,10) ,            	
                           
            new SqlParameter("@Parity", SqlDbType.VarChar,10) ,            	
                           
            new SqlParameter("@DataBits", SqlDbType.VarChar,10) ,            	
                           
            new SqlParameter("@StopBits", SqlDbType.VarChar,10) ,            	
                           
            new SqlParameter("@Visible", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@DoubleDir", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@LicenceKey", SqlDbType.VarChar,30) ,            	
                           
            new SqlParameter("@LicenceType", SqlDbType.VarChar,25) ,            	
                           
            new SqlParameter("@LicenceDate", SqlDbType.DateTime) ,            	
                           
            new SqlParameter("@SQH", SqlDbType.VarChar,4) ,            	
                           
            new SqlParameter("@SNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@SickType", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@UseImmPlate", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@ImmCalc", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@CommPara", SqlDbType.VarChar,800) ,            	
                           
            new SqlParameter("@ReagentPara", SqlDbType.VarChar,800) ,            	
                        	
                        	
                           
            new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@ZFStandCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@UseFlag", SqlDbType.Int,4)             	
              
            };
            			    
				
                
			   
			if(model.EquipNo!=null)
			{
            	parameters[0].Value = model.EquipNo;            	
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
            	
                
			   
			if(model.SectionNo!=null)
			{
            	parameters[4].Value = model.SectionNo;            	
            }
            	
                
			   
			if(model.Computer!=null)
			{
            	parameters[5].Value = model.Computer;            	
            }
            	
                
			   
			if(model.ComProgram!=null)
			{
            	parameters[6].Value = model.ComProgram;            	
            }
            	
                
			   
			if(model.ComPort!=null)
			{
            	parameters[7].Value = model.ComPort;            	
            }
            	
                
			   
			if(model.BaudRate!=null)
			{
            	parameters[8].Value = model.BaudRate;            	
            }
            	
                
			   
			if(model.Parity!=null)
			{
            	parameters[9].Value = model.Parity;            	
            }
            	
                
			   
			if(model.DataBits!=null)
			{
            	parameters[10].Value = model.DataBits;            	
            }
            	
                
			   
			if(model.StopBits!=null)
			{
            	parameters[11].Value = model.StopBits;            	
            }
            	
                
			   
			if(model.Visible!=null)
			{
            	parameters[12].Value = model.Visible;            	
            }
            	
                
			   
			if(model.DoubleDir!=null)
			{
            	parameters[13].Value = model.DoubleDir;            	
            }
            	
                
			   
			if(model.LicenceKey!=null)
			{
            	parameters[14].Value = model.LicenceKey;            	
            }
            	
                
			   
			if(model.LicenceType!=null)
			{
            	parameters[15].Value = model.LicenceType;            	
            }
            	
                
			   
			if(model.LicenceDate!=null)
			{
            	parameters[16].Value = model.LicenceDate;            	
            }
            	
                
			   
			if(model.SQH!=null)
			{
            	parameters[17].Value = model.SQH;            	
            }
            	
                
			   
			if(model.SNo!=null)
			{
            	parameters[18].Value = model.SNo;            	
            }
            	
                
			   
			if(model.SickType!=null)
			{
            	parameters[19].Value = model.SickType;            	
            }
            	
                
			   
			if(model.UseImmPlate!=null)
			{
            	parameters[20].Value = model.UseImmPlate;            	
            }
            	
                
			   
			if(model.ImmCalc!=null)
			{
            	parameters[21].Value = model.ImmCalc;            	
            }
            	
                
			   
			if(model.CommPara!=null)
			{
            	parameters[22].Value = model.CommPara;            	
            }
            	
                
			   
			if(model.ReagentPara!=null)
			{
            	parameters[23].Value = model.ReagentPara;            	
            }
            	
                
				
                
				
                
			   
			if(model.StandCode!=null)
			{
            	parameters[24].Value = model.StandCode;            	
            }
            	
                
			   
			if(model.ZFStandCode!=null)
			{
            	parameters[25].Value = model.ZFStandCode;            	
            }
            	
                
			   
			if(model.UseFlag!=null)
			{
            	parameters[26].Value = model.UseFlag;            	
            }
            	
                        
			if (idb.ExecuteNonQuery(strSql.ToString(),parameters) > 0)
            {
                return d_log.OperateLog("Equipment", "", "", DateTime.Now, 1);
            }
            else
                return -1;
		}
		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int EquipNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from B_Equipment ");
			strSql.Append(" where EquipNo=@EquipNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@EquipNo", SqlDbType.Int,4)};
			parameters[0].Value = EquipNo;


			return idb.ExecuteNonQuery(strSql.ToString(),parameters);
		
		}
		
				/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteList(string EquipIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from B_Equipment ");
			strSql.Append(" where ID in ("+EquipIDlist + ")  ");
			return idb.ExecuteNonQuery(strSql.ToString());
			
		}
				
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZhiFang.Model.Equipment GetModel(int EquipNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select EquipID, EquipNo, CName, ShortName, ShortCode, SectionNo, Computer, ComProgram, ComPort, BaudRate, Parity, DataBits, StopBits, Visible, DoubleDir, LicenceKey, LicenceType, LicenceDate, SQH, SNo, SickType, UseImmPlate, ImmCalc, CommPara, ReagentPara, DTimeStampe, AddTime, StandCode, ZFStandCode, UseFlag  ");			
			strSql.Append("  from B_Equipment ");
			strSql.Append(" where EquipNo=@EquipNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@EquipNo", SqlDbType.Int,4)};
			parameters[0].Value = EquipNo;

			
			ZhiFang.Model.Equipment model=new ZhiFang.Model.Equipment();
			DataSet ds=idb.ExecuteDataSet(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																if(ds.Tables[0].Rows[0]["EquipID"].ToString()!="")
				{
					model.EquipID=int.Parse(ds.Tables[0].Rows[0]["EquipID"].ToString());
				}
																																				if(ds.Tables[0].Rows[0]["EquipNo"].ToString()!="")
				{
					model.EquipNo=int.Parse(ds.Tables[0].Rows[0]["EquipNo"].ToString());
				}
																																								model.CName= ds.Tables[0].Rows[0]["CName"].ToString();
																																				model.ShortName= ds.Tables[0].Rows[0]["ShortName"].ToString();
																																				model.ShortCode= ds.Tables[0].Rows[0]["ShortCode"].ToString();
																																if(ds.Tables[0].Rows[0]["SectionNo"].ToString()!="")
				{
					model.SectionNo=int.Parse(ds.Tables[0].Rows[0]["SectionNo"].ToString());
				}
																																								model.Computer= ds.Tables[0].Rows[0]["Computer"].ToString();
																																				model.ComProgram= ds.Tables[0].Rows[0]["ComProgram"].ToString();
																																				model.ComPort= ds.Tables[0].Rows[0]["ComPort"].ToString();
																																				model.BaudRate= ds.Tables[0].Rows[0]["BaudRate"].ToString();
																																				model.Parity= ds.Tables[0].Rows[0]["Parity"].ToString();
																																				model.DataBits= ds.Tables[0].Rows[0]["DataBits"].ToString();
																																				model.StopBits= ds.Tables[0].Rows[0]["StopBits"].ToString();
																																if(ds.Tables[0].Rows[0]["Visible"].ToString()!="")
				{
					model.Visible=int.Parse(ds.Tables[0].Rows[0]["Visible"].ToString());
				}
																																				if(ds.Tables[0].Rows[0]["DoubleDir"].ToString()!="")
				{
					model.DoubleDir=int.Parse(ds.Tables[0].Rows[0]["DoubleDir"].ToString());
				}
																																								model.LicenceKey= ds.Tables[0].Rows[0]["LicenceKey"].ToString();
																																				model.LicenceType= ds.Tables[0].Rows[0]["LicenceType"].ToString();
																																if(ds.Tables[0].Rows[0]["LicenceDate"].ToString()!="")
				{
					model.LicenceDate=DateTime.Parse(ds.Tables[0].Rows[0]["LicenceDate"].ToString());
				}
																																								model.SQH= ds.Tables[0].Rows[0]["SQH"].ToString();
																																if(ds.Tables[0].Rows[0]["SNo"].ToString()!="")
				{
					model.SNo=int.Parse(ds.Tables[0].Rows[0]["SNo"].ToString());
				}
																																				if(ds.Tables[0].Rows[0]["SickType"].ToString()!="")
				{
					model.SickType=int.Parse(ds.Tables[0].Rows[0]["SickType"].ToString());
				}
																																				if(ds.Tables[0].Rows[0]["UseImmPlate"].ToString()!="")
				{
					model.UseImmPlate=int.Parse(ds.Tables[0].Rows[0]["UseImmPlate"].ToString());
				}
																																				if(ds.Tables[0].Rows[0]["ImmCalc"].ToString()!="")
				{
					model.ImmCalc=int.Parse(ds.Tables[0].Rows[0]["ImmCalc"].ToString());
				}
																																								model.CommPara= ds.Tables[0].Rows[0]["CommPara"].ToString();
																																				model.ReagentPara= ds.Tables[0].Rows[0]["ReagentPara"].ToString();
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
			strSql.Append(" FROM B_Equipment ");
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
			strSql.Append(" FROM B_Equipment ");
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
		public DataSet GetList(ZhiFang.Model.Equipment model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM B_Equipment where 1=1 ");
			                                                                
             
            if(model.EquipNo !=0)
                        {
                        strSql.Append(" and EquipNo="+model.EquipNo+" ");
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
                                                    
                        if(model.SectionNo !=null)
                        {
                        strSql.Append(" and SectionNo="+model.SectionNo+" ");
                        }
                                                    
                        if(model.Computer !=null)
                        {
                        strSql.Append(" and Computer='"+model.Computer+"' ");
                        }
                                                    
                        if(model.ComProgram !=null)
                        {
                        strSql.Append(" and ComProgram='"+model.ComProgram+"' ");
                        }
                                                    
                        if(model.ComPort !=null)
                        {
                        strSql.Append(" and ComPort='"+model.ComPort+"' ");
                        }
                                                    
                        if(model.BaudRate !=null)
                        {
                        strSql.Append(" and BaudRate='"+model.BaudRate+"' ");
                        }
                                                    
                        if(model.Parity !=null)
                        {
                        strSql.Append(" and Parity='"+model.Parity+"' ");
                        }
                                                    
                        if(model.DataBits !=null)
                        {
                        strSql.Append(" and DataBits='"+model.DataBits+"' ");
                        }
                                                    
                        if(model.StopBits !=null)
                        {
                        strSql.Append(" and StopBits='"+model.StopBits+"' ");
                        }
                                                                
                        if(model.DoubleDir !=null)
                        {
                        strSql.Append(" and DoubleDir="+model.DoubleDir+" ");
                        }
                                                    
                        if(model.LicenceKey !=null)
                        {
                        strSql.Append(" and LicenceKey='"+model.LicenceKey+"' ");
                        }
                                                    
                        if(model.LicenceType !=null)
                        {
                        strSql.Append(" and LicenceType='"+model.LicenceType+"' ");
                        }
                                                    
                        if(model.LicenceDate !=null)
                        {
                        strSql.Append(" and LicenceDate='"+model.LicenceDate+"' ");
                        }
                                                    
                        if(model.SQH !=null)
                        {
                        strSql.Append(" and SQH='"+model.SQH+"' ");
                        }
                                                    
                        if(model.SNo !=null)
                        {
                        strSql.Append(" and SNo="+model.SNo+" ");
                        }
                                                    
                        if(model.SickType !=null)
                        {
                        strSql.Append(" and SickType="+model.SickType+" ");
                        }
                                                    
                        if(model.UseImmPlate !=null)
                        {
                        strSql.Append(" and UseImmPlate="+model.UseImmPlate+" ");
                        }
                                                    
                        if(model.ImmCalc !=null)
                        {
                        strSql.Append(" and ImmCalc="+model.ImmCalc+" ");
                        }
                                                    
                        if(model.CommPara !=null)
                        {
                        strSql.Append(" and CommPara='"+model.CommPara+"' ");
                        }
                                                    
                        if(model.ReagentPara !=null)
                        {
                        strSql.Append(" and ReagentPara='"+model.ReagentPara+"' ");
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
            strSql.Append("select count(*) FROM B_Equipment ");
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
        public int GetTotalCount(ZhiFang.Model.Equipment model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_Equipment where 1=1 ");
           	                                          
            if(model.EquipNo !=null)
            {
                        strSql.Append(" and EquipNo="+model.EquipNo+" ");
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
                                          
            if(model.SectionNo !=null)
            {
                        strSql.Append(" and SectionNo="+model.SectionNo+" ");
                        }
                                          
            if(model.Computer !=null)
            {
                        strSql.Append(" and Computer='"+model.Computer+"' ");
                        }
                                          
            if(model.ComProgram !=null)
            {
                        strSql.Append(" and ComProgram='"+model.ComProgram+"' ");
                        }
                                          
            if(model.ComPort !=null)
            {
                        strSql.Append(" and ComPort='"+model.ComPort+"' ");
                        }
                                          
            if(model.BaudRate !=null)
            {
                        strSql.Append(" and BaudRate='"+model.BaudRate+"' ");
                        }
                                          
            if(model.Parity !=null)
            {
                        strSql.Append(" and Parity='"+model.Parity+"' ");
                        }
                                          
            if(model.DataBits !=null)
            {
                        strSql.Append(" and DataBits='"+model.DataBits+"' ");
                        }
                                          
            if(model.StopBits !=null)
            {
                        strSql.Append(" and StopBits='"+model.StopBits+"' ");
                        }
                                          
            if(model.Visible !=null)
            {
                        strSql.Append(" and Visible="+model.Visible+" ");
                        }
                                          
            if(model.DoubleDir !=null)
            {
                        strSql.Append(" and DoubleDir="+model.DoubleDir+" ");
                        }
                                          
            if(model.LicenceKey !=null)
            {
                        strSql.Append(" and LicenceKey='"+model.LicenceKey+"' ");
                        }
                                          
            if(model.LicenceType !=null)
            {
                        strSql.Append(" and LicenceType='"+model.LicenceType+"' ");
                        }
                                          
            if(model.LicenceDate !=null)
            {
                        strSql.Append(" and LicenceDate='"+model.LicenceDate+"' ");
                        }
                                          
            if(model.SQH !=null)
            {
                        strSql.Append(" and SQH='"+model.SQH+"' ");
                        }
                                          
            if(model.SNo !=null)
            {
                        strSql.Append(" and SNo="+model.SNo+" ");
                        }
                                          
            if(model.SickType !=null)
            {
                        strSql.Append(" and SickType="+model.SickType+" ");
                        }
                                          
            if(model.UseImmPlate !=null)
            {
                        strSql.Append(" and UseImmPlate="+model.UseImmPlate+" ");
                        }
                                          
            if(model.ImmCalc !=null)
            {
                        strSql.Append(" and ImmCalc="+model.ImmCalc+" ");
                        }
                                          
            if(model.CommPara !=null)
            {
                        strSql.Append(" and CommPara='"+model.CommPara+"' ");
                        }
                                          
            if(model.ReagentPara !=null)
            {
                        strSql.Append(" and ReagentPara='"+model.ReagentPara+"' ");
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
        public DataSet GetListByPage(ZhiFang.Model.Equipment model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            if (model != null && model.LabCode != null)
            {
                strSql.Append(" select top " + nowPageSize + "  * from B_Equipment left join B_EquipmentControl on B_Equipment.EquipNo=B_EquipmentControl.EquipNo ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and B_EquipmentControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append("where EquipID not in ( ");
                strSql.Append("select top " + (nowPageSize * nowPageNum) + " EquipID from  B_Equipment left join B_EquipmentControl on B_Equipment.EquipNo=B_EquipmentControl.EquipNo ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and B_EquipmentControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append("order by B_Equipment.EquipID ) order by B_Equipment.EquipID ");
                return idb.ExecuteDataSet(strSql.ToString());
            }
            else
            {
                strSql.Append("select top " + nowPageSize + "  * from B_Equipment where EquipID not in  ");
                strSql.Append("(select top " + (nowPageSize * nowPageNum) + " EquipID from B_Equipment order by EquipID) order by EquipID  ");
                return idb.ExecuteDataSet(strSql.ToString());
            }
        }
        
        public bool Exists(int EquipNo)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from B_Equipment ");
			strSql.Append(" where EquipNo ='"+EquipNo+"'");
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
			string LabTableName="B_Equipment";
			LabTableName="B_Lab_"+LabTableName.Substring(LabTableName.IndexOf("_") + 1, LabTableName.Length - (LabTableName.IndexOf("_") + 1));
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSqlControl = new StringBuilder();
            string TableKey="EquipNo";
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
	            		strSql.Append(" LabEquipNo , CName , ShortName , ShortCode , SectionNo , Computer , ComProgram , ComPort , BaudRate , Parity , DataBits , StopBits , Visible , DoubleDir , LicenceKey , LicenceType , LicenceDate , SQH , SNo , SickType , UseImmPlate , ImmCalc , CommPara , ReagentPara , StandCode , ZFStandCode , UseFlag ");
						strSql.Append(") select '" + lst[i].Trim() + "' as LabCode, ");
	            		strSql.Append("EquipNo,CName,ShortName,ShortCode,SectionNo,Computer,ComProgram,ComPort,BaudRate,Parity,DataBits,StopBits,Visible,DoubleDir,LicenceKey,LicenceType,LicenceDate,SQH,SNo,SickType,UseImmPlate,ImmCalc,CommPara,ReagentPara,StandCode,ZFStandCode,UseFlag");            
	            		strSql.Append(" from B_Equipment ");    
	            		
	            		strSqlControl.Append("insert into B_EquipmentControl ( ");
	            		strSqlControl.Append(" "+TableKeySub+"ControlNo,"+TableKey+",ControlLabNo,Control"+TableKey+",UseFlag ");
	            		strSqlControl.Append(")  select ");
	            		strSqlControl.Append("  '" + lst[i].Trim() + "'+'_'+convert(varchar(50)," + TableKey + ")+'_'+convert(varchar(50), " + TableKey + ") as "+TableKeySub+"ControlNo,"+TableKey+",'" + lst[i].Trim() + "' as ControlLabNo,"+TableKey+",UseFlag ");
	            		strSqlControl.Append(" from B_Equipment ");  
	            		
	            		arrySql.Add(strSql.ToString());
	            		arrySql.Add(strSqlControl.ToString());	    
	            		
	            		strSql = new StringBuilder();
             			strSqlControl = new StringBuilder();
             			
	             }

                idb.BatchUpdateWithTransaction(arrySql);
                d_log.OperateLog("Equipment", "", "", DateTime.Now, 1);        
	             return true;
            }
            catch
            {
            	return false;
            }
           
		}
		
		public int GetMaxId()
        {
            return idb.GetMaxID("EquipNo","B_Equipment");
        }

        public DataSet GetList(int Top, ZhiFang.Model.Equipment model, string filedOrder)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" * ");
			strSql.Append(" FROM B_Equipment ");		
			
			                                          
            if(model.EquipNo !=null)
            {
                        strSql.Append(" and EquipNo="+model.EquipNo+" ");
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
                                          
            if(model.SectionNo !=null)
            {
                        strSql.Append(" and SectionNo="+model.SectionNo+" ");
                        }
                                          
            if(model.Computer !=null)
            {
                        
            strSql.Append(" and Computer='"+model.Computer+"' ");
                        }
                                          
            if(model.ComProgram !=null)
            {
                        
            strSql.Append(" and ComProgram='"+model.ComProgram+"' ");
                        }
                                          
            if(model.ComPort !=null)
            {
                        
            strSql.Append(" and ComPort='"+model.ComPort+"' ");
                        }
                                          
            if(model.BaudRate !=null)
            {
                        
            strSql.Append(" and BaudRate='"+model.BaudRate+"' ");
                        }
                                          
            if(model.Parity !=null)
            {
                        
            strSql.Append(" and Parity='"+model.Parity+"' ");
                        }
                                          
            if(model.DataBits !=null)
            {
                        
            strSql.Append(" and DataBits='"+model.DataBits+"' ");
                        }
                                          
            if(model.StopBits !=null)
            {
                        
            strSql.Append(" and StopBits='"+model.StopBits+"' ");
                        }
                                          
            if(model.Visible !=null)
            {
                        strSql.Append(" and Visible="+model.Visible+" ");
                        }
                                          
            if(model.DoubleDir !=null)
            {
                        strSql.Append(" and DoubleDir="+model.DoubleDir+" ");
                        }
                                          
            if(model.LicenceKey !=null)
            {
                        
            strSql.Append(" and LicenceKey='"+model.LicenceKey+"' ");
                        }
                                          
            if(model.LicenceType !=null)
            {
                        
            strSql.Append(" and LicenceType='"+model.LicenceType+"' ");
                        }
                                          
            if(model.LicenceDate !=null)
            {
                        
            strSql.Append(" and LicenceDate='"+model.LicenceDate+"' ");
                        }
                                          
            if(model.SQH !=null)
            {
                        
            strSql.Append(" and SQH='"+model.SQH+"' ");
                        }
                                          
            if(model.SNo !=null)
            {
                        strSql.Append(" and SNo="+model.SNo+" ");
                        }
                                          
            if(model.SickType !=null)
            {
                        strSql.Append(" and SickType="+model.SickType+" ");
                        }
                                          
            if(model.UseImmPlate !=null)
            {
                        strSql.Append(" and UseImmPlate="+model.UseImmPlate+" ");
                        }
                                          
            if(model.ImmCalc !=null)
            {
                        strSql.Append(" and ImmCalc="+model.ImmCalc+" ");
                        }
                                          
            if(model.CommPara !=null)
            {
                        
            strSql.Append(" and CommPara='"+model.CommPara+"' ");
                        }
                                          
            if(model.ReagentPara !=null)
            {
                        
            strSql.Append(" and ReagentPara='"+model.ReagentPara+"' ");
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
            strSql.Append("select *,'" + LabCode + "' as LabCode,EquipNo as LabEquipNo from B_Equipment where 1=1 ");
            if (dTimeStampe != -999999)
            {
                strSql.Append(" and convert(int,DTimeStampe) > '" + dTimeStampe + "' ");
            }
            DataTable dtServer = idb.ExecuteDataSet(strSql.ToString()).Tables[0];
            dtServer.TableName = "ServerDatas";

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("select *,LabEquipNo as EquipNo from B_Lab_Equipment where 1=1 ");
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
            strSql3.Append("select * from B_EquipmentControl where 1=1 ");
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


        #region IDataBase<Equipment> 成员
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
                        if (this.Exists(int.Parse(ds.Tables[0].Rows[i]["EquipNo"].ToString().Trim())))
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
                strSql.Append("insert into B_Equipment (");
                strSql.Append("EquipNo,CName,ShortName,ShortCode,SectionNo,Computer,ComProgram,ComPort,BaudRate,Parity,DataBits,StopBits,Visible,DoubleDir,LicenceKey,LicenceType,LicenceDate,SQH,SNo,SickType,UseImmPlate,ImmCalc,CommPara,ReagentPara,AddTime,StandCode,ZFStandCode,UseFlag");
                strSql.Append(") values (");
                strSql.Append("'" + dr["EquipNo"].ToString().Trim() + "','" + dr["CName"].ToString().Trim() + "','" + dr["ShortName"].ToString().Trim() + "','" + dr["ShortCode"].ToString().Trim() + "','" + dr["SectionNo"].ToString().Trim() + "','" + dr["Computer"].ToString().Trim() + "','" + dr["ComProgram"].ToString().Trim() + "','" + dr["ComPort"].ToString().Trim() + "','" + dr["BaudRate"].ToString().Trim() + "','" + dr["Parity"].ToString().Trim() + "','" + dr["DataBits"].ToString().Trim() + "','" + dr["StopBits"].ToString().Trim() + "','" + dr["Visible"].ToString().Trim() + "','" + dr["DoubleDir"].ToString().Trim() + "','" + dr["LicenceKey"].ToString().Trim() + "','" + dr["LicenceType"].ToString().Trim() + "','" + dr["LicenceDate"].ToString().Trim() + "','" + dr["SQH"].ToString().Trim() + "','" + dr["SNo"].ToString().Trim() + "','" + dr["SickType"].ToString().Trim() + "','" + dr["UseImmPlate"].ToString().Trim() + "','" + dr["ImmCalc"].ToString().Trim() + "','" + dr["CommPara"].ToString().Trim() + "','" + dr["ReagentPara"].ToString().Trim() + "','" + DateTime.Parse(dr["AddTime"].ToString().Trim()) + "','" + dr["StandCode"].ToString().Trim() + "','" + dr["ZFStandCode"].ToString().Trim() + "','" + dr["UseFlag"].ToString().Trim() + "'");
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
                strSql.Append("update B_Equipment set ");

                strSql.Append(" CName = '" + dr["CName"].ToString().Trim() + "' , ");
                strSql.Append(" ShortName = '" + dr["ShortName"].ToString().Trim() + "' , ");
                strSql.Append(" ShortCode = '" + dr["ShortCode"].ToString().Trim() + "' , ");
                strSql.Append(" SectionNo = '" + dr["SectionNo"].ToString().Trim() + "' , ");
                strSql.Append(" Computer = '" + dr["Computer"].ToString().Trim() + "' , ");
                strSql.Append(" ComProgram = '" + dr["ComProgram"].ToString().Trim() + "' , ");
                strSql.Append(" ComPort = '" + dr["ComPort"].ToString().Trim() + "' , ");
                strSql.Append(" BaudRate = '" + dr["BaudRate"].ToString().Trim() + "' , ");
                strSql.Append(" Parity = '" + dr["Parity"].ToString().Trim() + "' , ");
                strSql.Append(" DataBits = '" + dr["DataBits"].ToString().Trim() + "' , ");
                strSql.Append(" StopBits = '" + dr["StopBits"].ToString().Trim() + "' , ");
                strSql.Append(" Visible = '" + dr["Visible"].ToString().Trim() + "' , ");
                strSql.Append(" DoubleDir = '" + dr["DoubleDir"].ToString().Trim() + "' , ");
                strSql.Append(" LicenceKey = '" + dr["LicenceKey"].ToString().Trim() + "' , ");
                strSql.Append(" LicenceType = '" + dr["LicenceType"].ToString().Trim() + "' , ");
                strSql.Append(" LicenceDate = '" + dr["LicenceDate"].ToString().Trim() + "' , ");
                strSql.Append(" SQH = '" + dr["SQH"].ToString().Trim() + "' , ");
                strSql.Append(" SNo = '" + dr["SNo"].ToString().Trim() + "' , ");
                strSql.Append(" SickType = '" + dr["SickType"].ToString().Trim() + "' , ");
                strSql.Append(" UseImmPlate = '" + dr["UseImmPlate"].ToString().Trim() + "' , ");
                strSql.Append(" ImmCalc = '" + dr["ImmCalc"].ToString().Trim() + "' , ");
                strSql.Append(" CommPara = '" + dr["CommPara"].ToString().Trim() + "' , ");
                strSql.Append(" ReagentPara = '" + dr["ReagentPara"].ToString().Trim() + "' , ");
                strSql.Append(" StandCode = '" + dr["StandCode"].ToString().Trim() + "' , ");
                strSql.Append(" ZFStandCode = '" + dr["ZFStandCode"].ToString().Trim() + "' , ");
                strSql.Append(" UseFlag = '" + dr["UseFlag"].ToString().Trim() + "'  ");
                strSql.Append(" where EquipNo='" + dr["EquipNo"].ToString().Trim() + "' ");

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

