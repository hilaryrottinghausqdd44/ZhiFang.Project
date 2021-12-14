using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Digitlab8  
{
	 	//B_Lab_Equipment
		
	public partial class B_Lab_Equipment: IDLab_Equipment	{	
		DBUtility.IDBConnection idb;
        public B_Lab_Equipment(string dbsourceconn)
        {
            idb = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
		public B_Lab_Equipment()
		{
            idb = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.LisDB());
        }
		D_LogInfo d_log = new D_LogInfo();
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ZhiFang.Model.Lab_Equipment model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into B_Lab_Equipment(");			
            strSql.Append("LabCode,LabEquipNo,CName,ShortName,ShortCode,SectionNo,Computer,ComProgram,ComPort,BaudRate,Parity,DataBits,StopBits,Visible,DoubleDir,LicenceKey,LicenceType,LicenceDate,SQH,SNo,SickType,UseImmPlate,ImmCalc,CommPara,ReagentPara,StandCode,ZFStandCode,UseFlag");
			strSql.Append(") values (");
            strSql.Append("@LabCode,@LabEquipNo,@CName,@ShortName,@ShortCode,@SectionNo,@Computer,@ComProgram,@ComPort,@BaudRate,@Parity,@DataBits,@StopBits,@Visible,@DoubleDir,@LicenceKey,@LicenceType,@LicenceDate,@SQH,@SNo,@SickType,@UseImmPlate,@ImmCalc,@CommPara,@ReagentPara,@StandCode,@ZFStandCode,@UseFlag");            
            strSql.Append(") ");            
            
			SqlParameter[] parameters = {
			            new SqlParameter("@LabCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@LabEquipNo", SqlDbType.Int,4) ,            
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
			            
            parameters[0].Value = model.LabCode;                        
            parameters[1].Value = model.LabEquipNo;                        
            parameters[2].Value = model.CName;                        
            parameters[3].Value = model.ShortName;                        
            parameters[4].Value = model.ShortCode;                        
            parameters[5].Value = model.SectionNo;                        
            parameters[6].Value = model.Computer;                        
            parameters[7].Value = model.ComProgram;                        
            parameters[8].Value = model.ComPort;                        
            parameters[9].Value = model.BaudRate;                        
            parameters[10].Value = model.Parity;                        
            parameters[11].Value = model.DataBits;                        
            parameters[12].Value = model.StopBits;                        
            parameters[13].Value = model.Visible;                        
            parameters[14].Value = model.DoubleDir;                        
            parameters[15].Value = model.LicenceKey;                        
            parameters[16].Value = model.LicenceType;                        
            parameters[17].Value = model.LicenceDate;                        
            parameters[18].Value = model.SQH;                        
            parameters[19].Value = model.SNo;                        
            parameters[20].Value = model.SickType;                        
            parameters[21].Value = model.UseImmPlate;                        
            parameters[22].Value = model.ImmCalc;                        
            parameters[23].Value = model.CommPara;                        
            parameters[24].Value = model.ReagentPara;                        
            parameters[25].Value = model.StandCode;                        
            parameters[26].Value = model.ZFStandCode;                        
            parameters[27].Value = model.UseFlag;                  
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
		public int Update(ZhiFang.Model.Lab_Equipment model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update B_Lab_Equipment set ");
			                                                
            strSql.Append(" LabCode = @LabCode , ");                                    
            strSql.Append(" LabEquipNo = @LabEquipNo , ");                                    
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
			strSql.Append(" where LabCode=@LabCode and LabEquipNo=@LabEquipNo  ");
						
			SqlParameter[] parameters = {
			            	
                           
            new SqlParameter("@LabCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@LabEquipNo", SqlDbType.Int,4) ,            	
                           
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
            			    
				
                
			   
			if(model.LabCode!=null)
			{
            	parameters[0].Value = model.LabCode;            	
            }
            	
                
			   
			if(model.LabEquipNo!=null)
			{
            	parameters[1].Value = model.LabEquipNo;            	
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
            	
                
			   
			if(model.SectionNo!=null)
			{
            	parameters[5].Value = model.SectionNo;            	
            }
            	
                
			   
			if(model.Computer!=null)
			{
            	parameters[6].Value = model.Computer;            	
            }
            	
                
			   
			if(model.ComProgram!=null)
			{
            	parameters[7].Value = model.ComProgram;            	
            }
            	
                
			   
			if(model.ComPort!=null)
			{
            	parameters[8].Value = model.ComPort;            	
            }
            	
                
			   
			if(model.BaudRate!=null)
			{
            	parameters[9].Value = model.BaudRate;            	
            }
            	
                
			   
			if(model.Parity!=null)
			{
            	parameters[10].Value = model.Parity;            	
            }
            	
                
			   
			if(model.DataBits!=null)
			{
            	parameters[11].Value = model.DataBits;            	
            }
            	
                
			   
			if(model.StopBits!=null)
			{
            	parameters[12].Value = model.StopBits;            	
            }
            	
                
			   
			if(model.Visible!=null)
			{
            	parameters[13].Value = model.Visible;            	
            }
            	
                
			   
			if(model.DoubleDir!=null)
			{
            	parameters[14].Value = model.DoubleDir;            	
            }
            	
                
			   
			if(model.LicenceKey!=null)
			{
            	parameters[15].Value = model.LicenceKey;            	
            }
            	
                
			   
			if(model.LicenceType!=null)
			{
            	parameters[16].Value = model.LicenceType;            	
            }
            	
                
			   
			if(model.LicenceDate!=null)
			{
            	parameters[17].Value = model.LicenceDate;            	
            }
            	
                
			   
			if(model.SQH!=null)
			{
            	parameters[18].Value = model.SQH;            	
            }
            	
                
			   
			if(model.SNo!=null)
			{
            	parameters[19].Value = model.SNo;            	
            }
            	
                
			   
			if(model.SickType!=null)
			{
            	parameters[20].Value = model.SickType;            	
            }
            	
                
			   
			if(model.UseImmPlate!=null)
			{
            	parameters[21].Value = model.UseImmPlate;            	
            }
            	
                
			   
			if(model.ImmCalc!=null)
			{
            	parameters[22].Value = model.ImmCalc;            	
            }
            	
                
			   
			if(model.CommPara!=null)
			{
            	parameters[23].Value = model.CommPara;            	
            }
            	
                
			   
			if(model.ReagentPara!=null)
			{
            	parameters[24].Value = model.ReagentPara;            	
            }
            	
                
				
                
				
                
			   
			if(model.StandCode!=null)
			{
            	parameters[25].Value = model.StandCode;            	
            }
            	
                
			   
			if(model.ZFStandCode!=null)
			{
            	parameters[26].Value = model.ZFStandCode;            	
            }
            	
                
			   
			if(model.UseFlag!=null)
			{
            	parameters[27].Value = model.UseFlag;            	
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
		public int Delete(string LabCode,int LabEquipNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from B_Lab_Equipment ");
			strSql.Append(" where LabCode=@LabCode and LabEquipNo=@LabEquipNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@LabCode", SqlDbType.VarChar,50),
					new SqlParameter("@LabEquipNo", SqlDbType.Int,4)};
			parameters[0].Value = LabCode;
			parameters[1].Value = LabEquipNo;


			return idb.ExecuteNonQuery(strSql.ToString(),parameters);
			
		}
		
				/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteList(string EquipIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from B_Lab_Equipment ");
			strSql.Append(" where ID in ("+EquipIDlist + ")  ");
			return idb.ExecuteNonQuery(strSql.ToString());
			
		}
				
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZhiFang.Model.Lab_Equipment GetModel(string LabCode,int LabEquipNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select EquipID, LabCode, LabEquipNo, CName, ShortName, ShortCode, SectionNo, Computer, ComProgram, ComPort, BaudRate, Parity, DataBits, StopBits, Visible, DoubleDir, LicenceKey, LicenceType, LicenceDate, SQH, SNo, SickType, UseImmPlate, ImmCalc, CommPara, ReagentPara, DTimeStampe, AddTime, StandCode, ZFStandCode, UseFlag  ");			
			strSql.Append("  from B_Lab_Equipment ");
			strSql.Append(" where LabCode=@LabCode and LabEquipNo=@LabEquipNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@LabCode", SqlDbType.VarChar,50),
					new SqlParameter("@LabEquipNo", SqlDbType.Int,4)};
			parameters[0].Value = LabCode;
			parameters[1].Value = LabEquipNo;

			
			ZhiFang.Model.Lab_Equipment model=new ZhiFang.Model.Lab_Equipment();
			DataSet ds=idb.ExecuteDataSet(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																if(ds.Tables[0].Rows[0]["EquipID"].ToString()!="")
				{
					model.EquipID=int.Parse(ds.Tables[0].Rows[0]["EquipID"].ToString());
				}
																																								model.LabCode= ds.Tables[0].Rows[0]["LabCode"].ToString();
																																if(ds.Tables[0].Rows[0]["LabEquipNo"].ToString()!="")
				{
					model.LabEquipNo=int.Parse(ds.Tables[0].Rows[0]["LabEquipNo"].ToString());
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
			strSql.Append(" FROM B_Lab_Equipment ");
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
			strSql.Append(" FROM B_Lab_Equipment ");
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
		public DataSet GetList(ZhiFang.Model.Lab_Equipment model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM B_Lab_Equipment where 1=1 ");
			                                                                
                        if(model.LabCode !=null)
                        {
                        strSql.Append(" and LabCode='"+model.LabCode+"' ");
                        }
                                                    
             
            if(model.LabEquipNo !=0)
                        {
                        strSql.Append(" and LabEquipNo="+model.LabEquipNo+" ");
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
        public DataSet GetListByLike(ZhiFang.Model.Lab_Equipment model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *,'('+convert(varchar(100),LabEquipNo)+')'+CName as LabEquipNoAndName ");
            strSql.Append(" FROM B_Lab_Equipment where 1=1 ");
            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }

            if (model.CName != null)
            {
                strSql.Append(" and ( CName like '%" + model.CName + "%' ");
            }

            if (model.LabEquipNo != 0)
            {
                strSql.Append(" or LabEquipNo like '%" + model.LabEquipNo + "%' ");
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
            strSql.Append("select count(*) FROM B_Lab_Equipment ");
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
        public int GetTotalCount(ZhiFang.Model.Lab_Equipment model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_Lab_Equipment where 1=1 ");
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
        public DataSet GetListByPage(ZhiFang.Model.Lab_Equipment model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top " + nowPageSize + " * from B_Lab_Equipment where 1=1  ");
                                                
                                                                        
                  
                        if(model.LabCode !=null)
                        {
                        strSql.Append(" and LabCode='"+model.LabCode+"' ");
                        }
                                                                        if(model.LabEquipNo !=0)
            {
            	 strSql.Append(" and LabEquipNo="+model.LabEquipNo+" ");
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
                                                            strSql.Append("and EquipID not in ");
            strSql.Append("(select top " + (nowPageSize * nowPageNum) + " EquipID from B_Lab_Equipment where 1=1  ");
            strSql.Append(" order by EquipID) order by EquipID  ");
            return idb.ExecuteDataSet(strSql.ToString());
        }
        
        public bool Exists(string LabCode,int LabEquipNo)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from B_Lab_Equipment ");
			strSql.Append(" where LabCode=@LabCode and LabEquipNo=@LabEquipNo ");
						SqlParameter[] parameters = {
					new SqlParameter("@LabCode", SqlDbType.VarChar,50),
					new SqlParameter("@LabEquipNo", SqlDbType.Int,4)};
			parameters[0].Value = LabCode;
			parameters[1].Value = LabEquipNo;


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
            return idb.GetMaxID("LabCode,LabEquipNo","B_Lab_Equipment");
        }

        public DataSet GetList(int Top, ZhiFang.Model.Lab_Equipment model, string filedOrder)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" * ");
			strSql.Append(" FROM B_Lab_Equipment ");		
			
			                                          
            if(model.LabCode !=null)
            {
                        
            strSql.Append(" and LabCode='"+model.LabCode+"' ");
                        }
                                          
            if(model.LabEquipNo !=null)
            {
                        strSql.Append(" and LabEquipNo="+model.LabEquipNo+" ");
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


        #region IDataBase<Lab_Equipment> 成员
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
                        if (this.Exists(ds.Tables[0].Rows[i]["LabCode"].ToString().Trim(),int.Parse(ds.Tables[0].Rows[i]["LabEquipNo"].ToString().Trim())))
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
                strSql.Append("insert into B_Lab_Equipment (");
                strSql.Append("LabCode,LabEquipNo,CName,ShortName,ShortCode,SectionNo,Computer,ComProgram,ComPort,BaudRate,Parity,DataBits,StopBits,Visible,DoubleDir,LicenceKey,LicenceType,LicenceDate,SQH,SNo,SickType,UseImmPlate,ImmCalc,CommPara,ReagentPara,AddTime,StandCode,ZFStandCode,UseFlag");
                strSql.Append(") values (");
                strSql.Append("'" + dr["LabCode"].ToString().Trim() + "','" + dr["LabEquipNo"].ToString().Trim() + "','" + dr["CName"].ToString().Trim() + "','" + dr["ShortName"].ToString().Trim() + "','" + dr["ShortCode"].ToString().Trim() + "','" + dr["SectionNo"].ToString().Trim() + "','" + dr["Computer"].ToString().Trim() + "','" + dr["ComProgram"].ToString().Trim() + "','" + dr["ComPort"].ToString().Trim() + "','" + dr["BaudRate"].ToString().Trim() + "','" + dr["Parity"].ToString().Trim() + "','" + dr["DataBits"].ToString().Trim() + "','" + dr["StopBits"].ToString().Trim() + "','" + dr["Visible"].ToString().Trim() + "','" + dr["DoubleDir"].ToString().Trim() + "','" + dr["LicenceKey"].ToString().Trim() + "','" + dr["LicenceType"].ToString().Trim() + "','" + dr["LicenceDate"].ToString().Trim() + "','" + dr["SQH"].ToString().Trim() + "','" + dr["SNo"].ToString().Trim() + "','" + dr["SickType"].ToString().Trim() + "','" + dr["UseImmPlate"].ToString().Trim() + "','" + dr["ImmCalc"].ToString().Trim() + "','" + dr["CommPara"].ToString().Trim() + "','" + dr["ReagentPara"].ToString().Trim() + "','" + DateTime.Parse(dr["AddTime"].ToString().Trim()) + "','" + dr["StandCode"].ToString().Trim() + "','" + dr["ZFStandCode"].ToString().Trim() + "','" + dr["UseFlag"].ToString().Trim() + "'");
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
                strSql.Append("update B_Lab_Equipment set ");

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
                strSql.Append(" where LabCode='" + dr["LabCode"].ToString().Trim() + "' and LabEquipNo='" + dr["LabEquipNo"].ToString().Trim() + "' ");

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

