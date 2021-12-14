using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Digitlab8  
{
	 	//B_Lab_CLIENTELE
		
	public partial class B_Lab_CLIENTELE: IDLab_CLIENTELE	{	
		DBUtility.IDBConnection idb;
        public B_Lab_CLIENTELE(string dbsourceconn)
        {
            idb = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
		public B_Lab_CLIENTELE()
		{
            idb = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.LisDB());
        }
		D_LogInfo d_log = new D_LogInfo();
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ZhiFang.Model.Lab_CLIENTELE model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into B_Lab_CLIENTELE(");			
            strSql.Append("LabCode,LabClIENTNO,CNAME,ENAME,SHORTCODE,ISUSE,LINKMAN,PHONENUM1,ADDRESS,MAILNO,EMAIL,PRINCIPAL,PHONENUM2,CLIENTTYPE,BmanNo,Romark,TitleType,UploadType,PrintType,InputDataType,ReportPageType,ClientArea,ClientStyle,FaxNo,AutoFax,ClientReportTitle,IsPrintItem,CZDY1,CZDY2,CZDY3,CZDY4,CZDY5,CZDY6,LinkManPosition,LinkMan1,LinkManPosition1,ClientCode,CWAccountDate,NFClientType,RelationName,WebLisSourceOrgID,GroupName,StandCode,ZFStandCode,UseFlag");
			strSql.Append(") values (");
            strSql.Append("@LabCode,@LabClIENTNO,@CNAME,@ENAME,@SHORTCODE,@ISUSE,@LINKMAN,@PHONENUM1,@ADDRESS,@MAILNO,@EMAIL,@PRINCIPAL,@PHONENUM2,@CLIENTTYPE,@BmanNo,@Romark,@TitleType,@UploadType,@PrintType,@InputDataType,@ReportPageType,@ClientArea,@ClientStyle,@FaxNo,@AutoFax,@ClientReportTitle,@IsPrintItem,@CZDY1,@CZDY2,@CZDY3,@CZDY4,@CZDY5,@CZDY6,@LinkManPosition,@LinkMan1,@LinkManPosition1,@ClientCode,@CWAccountDate,@NFClientType,@RelationName,@WebLisSourceOrgID,@GroupName,@StandCode,@ZFStandCode,@UseFlag");            
            strSql.Append(") ");            
            
			SqlParameter[] parameters = {
			            new SqlParameter("@LabCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@LabClIENTNO", SqlDbType.Int,4) ,            
                        new SqlParameter("@CNAME", SqlDbType.VarChar,80) ,            
                        new SqlParameter("@ENAME", SqlDbType.VarChar,40) ,            
                        new SqlParameter("@SHORTCODE", SqlDbType.VarChar,40) ,            
                        new SqlParameter("@ISUSE", SqlDbType.Int,4) ,            
                        new SqlParameter("@LINKMAN", SqlDbType.VarChar,40) ,            
                        new SqlParameter("@PHONENUM1", SqlDbType.VarChar,40) ,            
                        new SqlParameter("@ADDRESS", SqlDbType.VarChar,40) ,            
                        new SqlParameter("@MAILNO", SqlDbType.VarChar,40) ,            
                        new SqlParameter("@EMAIL", SqlDbType.VarChar,40) ,            
                        new SqlParameter("@PRINCIPAL", SqlDbType.VarChar,40) ,            
                        new SqlParameter("@PHONENUM2", SqlDbType.VarChar,40) ,            
                        new SqlParameter("@CLIENTTYPE", SqlDbType.Int,4) ,            
                        new SqlParameter("@BmanNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@Romark", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@TitleType", SqlDbType.Int,4) ,            
                        new SqlParameter("@UploadType", SqlDbType.Int,4) ,            
                        new SqlParameter("@PrintType", SqlDbType.Int,4) ,            
                        new SqlParameter("@InputDataType", SqlDbType.Int,4) ,            
                        new SqlParameter("@ReportPageType", SqlDbType.Int,4) ,            
                        new SqlParameter("@ClientArea", SqlDbType.VarChar,40) ,            
                        new SqlParameter("@ClientStyle", SqlDbType.VarChar,40) ,            
                        new SqlParameter("@FaxNo", SqlDbType.VarChar,40) ,            
                        new SqlParameter("@AutoFax", SqlDbType.Int,4) ,            
                        new SqlParameter("@ClientReportTitle", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@IsPrintItem", SqlDbType.Int,4) ,            
                        new SqlParameter("@CZDY1", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@CZDY2", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@CZDY3", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@CZDY4", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@CZDY5", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@CZDY6", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@LinkManPosition", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@LinkMan1", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@LinkManPosition1", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ClientCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@CWAccountDate", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@NFClientType", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@RelationName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@WebLisSourceOrgID", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@GroupName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ZFStandCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@UseFlag", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.LabCode;                        
            parameters[1].Value = model.LabClIENTNO;                        
            parameters[2].Value = model.CNAME;                        
            parameters[3].Value = model.ENAME;                        
            parameters[4].Value = model.SHORTCODE;                        
            parameters[5].Value = model.ISUSE;                        
            parameters[6].Value = model.LINKMAN;                        
            parameters[7].Value = model.PHONENUM1;                        
            parameters[8].Value = model.ADDRESS;                        
            parameters[9].Value = model.MAILNO;                        
            parameters[10].Value = model.EMAIL;                        
            parameters[11].Value = model.PRINCIPAL;                        
            parameters[12].Value = model.PHONENUM2;                        
            parameters[13].Value = model.CLIENTTYPE;                        
            parameters[14].Value = model.BmanNo;                        
            parameters[15].Value = model.Romark;                        
            parameters[16].Value = model.TitleType;                        
            parameters[17].Value = model.UploadType;                        
            parameters[18].Value = model.PrintType;                        
            parameters[19].Value = model.InputDataType;                        
            parameters[20].Value = model.ReportPageType;                        
            parameters[21].Value = model.ClientArea;                        
            parameters[22].Value = model.ClientStyle;                        
            parameters[23].Value = model.FaxNo;                        
            parameters[24].Value = model.AutoFax;                        
            parameters[25].Value = model.ClientReportTitle;                        
            parameters[26].Value = model.IsPrintItem;                        
            parameters[27].Value = model.CZDY1;                        
            parameters[28].Value = model.CZDY2;                        
            parameters[29].Value = model.CZDY3;                        
            parameters[30].Value = model.CZDY4;                        
            parameters[31].Value = model.CZDY5;                        
            parameters[32].Value = model.CZDY6;                        
            parameters[33].Value = model.LinkManPosition;                        
            parameters[34].Value = model.LinkMan1;                        
            parameters[35].Value = model.LinkManPosition1;                        
            parameters[36].Value = model.ClientCode;                        
            parameters[37].Value = model.CWAccountDate;                        
            parameters[38].Value = model.NFClientType;                        
            parameters[39].Value = model.RelationName;                        
            parameters[40].Value = model.WebLisSourceOrgID;                        
            parameters[41].Value = model.GroupName;                        
            parameters[42].Value = model.StandCode;                        
            parameters[43].Value = model.ZFStandCode;                        
            parameters[44].Value = model.UseFlag;                  
			if (idb.ExecuteNonQuery(strSql.ToString(),parameters) > 0)
            {
                return d_log.OperateLog("CLIENTELE", "", "", DateTime.Now, 1);
            }
            else
                return -1;
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ZhiFang.Model.Lab_CLIENTELE model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update B_Lab_CLIENTELE set ");
			                                                
            strSql.Append(" LabCode = @LabCode , ");                                    
            strSql.Append(" LabClIENTNO = @LabClIENTNO , ");                                    
            strSql.Append(" CNAME = @CNAME , ");                                    
            strSql.Append(" ENAME = @ENAME , ");                                    
            strSql.Append(" SHORTCODE = @SHORTCODE , ");                                    
            strSql.Append(" ISUSE = @ISUSE , ");                                    
            strSql.Append(" LINKMAN = @LINKMAN , ");                                    
            strSql.Append(" PHONENUM1 = @PHONENUM1 , ");                                    
            strSql.Append(" ADDRESS = @ADDRESS , ");                                    
            strSql.Append(" MAILNO = @MAILNO , ");                                    
            strSql.Append(" EMAIL = @EMAIL , ");                                    
            strSql.Append(" PRINCIPAL = @PRINCIPAL , ");                                    
            strSql.Append(" PHONENUM2 = @PHONENUM2 , ");                                    
            strSql.Append(" CLIENTTYPE = @CLIENTTYPE , ");                                    
            strSql.Append(" BmanNo = @BmanNo , ");                                    
            strSql.Append(" Romark = @Romark , ");                                    
            strSql.Append(" TitleType = @TitleType , ");                                    
            strSql.Append(" UploadType = @UploadType , ");                                    
            strSql.Append(" PrintType = @PrintType , ");                                    
            strSql.Append(" InputDataType = @InputDataType , ");                                    
            strSql.Append(" ReportPageType = @ReportPageType , ");                                    
            strSql.Append(" ClientArea = @ClientArea , ");                                    
            strSql.Append(" ClientStyle = @ClientStyle , ");                                    
            strSql.Append(" FaxNo = @FaxNo , ");                                    
            strSql.Append(" AutoFax = @AutoFax , ");                                    
            strSql.Append(" ClientReportTitle = @ClientReportTitle , ");                                    
            strSql.Append(" IsPrintItem = @IsPrintItem , ");                                    
            strSql.Append(" CZDY1 = @CZDY1 , ");                                    
            strSql.Append(" CZDY2 = @CZDY2 , ");                                    
            strSql.Append(" CZDY3 = @CZDY3 , ");                                    
            strSql.Append(" CZDY4 = @CZDY4 , ");                                    
            strSql.Append(" CZDY5 = @CZDY5 , ");                                    
            strSql.Append(" CZDY6 = @CZDY6 , ");                                    
            strSql.Append(" LinkManPosition = @LinkManPosition , ");                                    
            strSql.Append(" LinkMan1 = @LinkMan1 , ");                                    
            strSql.Append(" LinkManPosition1 = @LinkManPosition1 , ");                                    
            strSql.Append(" ClientCode = @ClientCode , ");                                    
            strSql.Append(" CWAccountDate = @CWAccountDate , ");                                    
            strSql.Append(" NFClientType = @NFClientType , ");                                    
            strSql.Append(" RelationName = @RelationName , ");                                    
            strSql.Append(" WebLisSourceOrgID = @WebLisSourceOrgID , ");                                    
            strSql.Append(" GroupName = @GroupName , ");                                                                                    
            strSql.Append(" StandCode = @StandCode , ");                                    
            strSql.Append(" ZFStandCode = @ZFStandCode , ");                                    
            strSql.Append(" UseFlag = @UseFlag  ");            			
			strSql.Append(" where LabCode=@LabCode and LabClIENTNO=@LabClIENTNO  ");
						
			SqlParameter[] parameters = {
			            	
                           
            new SqlParameter("@LabCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@LabClIENTNO", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@CNAME", SqlDbType.VarChar,80) ,            	
                           
            new SqlParameter("@ENAME", SqlDbType.VarChar,40) ,            	
                           
            new SqlParameter("@SHORTCODE", SqlDbType.VarChar,40) ,            	
                           
            new SqlParameter("@ISUSE", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@LINKMAN", SqlDbType.VarChar,40) ,            	
                           
            new SqlParameter("@PHONENUM1", SqlDbType.VarChar,40) ,            	
                           
            new SqlParameter("@ADDRESS", SqlDbType.VarChar,40) ,            	
                           
            new SqlParameter("@MAILNO", SqlDbType.VarChar,40) ,            	
                           
            new SqlParameter("@EMAIL", SqlDbType.VarChar,40) ,            	
                           
            new SqlParameter("@PRINCIPAL", SqlDbType.VarChar,40) ,            	
                           
            new SqlParameter("@PHONENUM2", SqlDbType.VarChar,40) ,            	
                           
            new SqlParameter("@CLIENTTYPE", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@BmanNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@Romark", SqlDbType.VarChar,100) ,            	
                           
            new SqlParameter("@TitleType", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@UploadType", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@PrintType", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@InputDataType", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@ReportPageType", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@ClientArea", SqlDbType.VarChar,40) ,            	
                           
            new SqlParameter("@ClientStyle", SqlDbType.VarChar,40) ,            	
                           
            new SqlParameter("@FaxNo", SqlDbType.VarChar,40) ,            	
                           
            new SqlParameter("@AutoFax", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@ClientReportTitle", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@IsPrintItem", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@CZDY1", SqlDbType.VarChar,100) ,            	
                           
            new SqlParameter("@CZDY2", SqlDbType.VarChar,100) ,            	
                           
            new SqlParameter("@CZDY3", SqlDbType.VarChar,100) ,            	
                           
            new SqlParameter("@CZDY4", SqlDbType.VarChar,100) ,            	
                           
            new SqlParameter("@CZDY5", SqlDbType.VarChar,100) ,            	
                           
            new SqlParameter("@CZDY6", SqlDbType.VarChar,100) ,            	
                           
            new SqlParameter("@LinkManPosition", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@LinkMan1", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@LinkManPosition1", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@ClientCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@CWAccountDate", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@NFClientType", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@RelationName", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@WebLisSourceOrgID", SqlDbType.VarChar,10) ,            	
                           
            new SqlParameter("@GroupName", SqlDbType.VarChar,50) ,            	
                        	
                        	
                           
            new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@ZFStandCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@UseFlag", SqlDbType.Int,4)             	
              
            };
            			    
				
                
			   
			if(model.LabCode!=null)
			{
            	parameters[0].Value = model.LabCode;            	
            }
            	
                
			   
			if(model.LabClIENTNO!=null)
			{
            	parameters[1].Value = model.LabClIENTNO;            	
            }
            	
                
			   
			if(model.CNAME!=null)
			{
            	parameters[2].Value = model.CNAME;            	
            }
            	
                
			   
			if(model.ENAME!=null)
			{
            	parameters[3].Value = model.ENAME;            	
            }
            	
                
			   
			if(model.SHORTCODE!=null)
			{
            	parameters[4].Value = model.SHORTCODE;            	
            }
            	
                
			   
			if(model.ISUSE!=null)
			{
            	parameters[5].Value = model.ISUSE;            	
            }
            	
                
			   
			if(model.LINKMAN!=null)
			{
            	parameters[6].Value = model.LINKMAN;            	
            }
            	
                
			   
			if(model.PHONENUM1!=null)
			{
            	parameters[7].Value = model.PHONENUM1;            	
            }
            	
                
			   
			if(model.ADDRESS!=null)
			{
            	parameters[8].Value = model.ADDRESS;            	
            }
            	
                
			   
			if(model.MAILNO!=null)
			{
            	parameters[9].Value = model.MAILNO;            	
            }
            	
                
			   
			if(model.EMAIL!=null)
			{
            	parameters[10].Value = model.EMAIL;            	
            }
            	
                
			   
			if(model.PRINCIPAL!=null)
			{
            	parameters[11].Value = model.PRINCIPAL;            	
            }
            	
                
			   
			if(model.PHONENUM2!=null)
			{
            	parameters[12].Value = model.PHONENUM2;            	
            }
            	
                
			   
			if(model.CLIENTTYPE!=null)
			{
            	parameters[13].Value = model.CLIENTTYPE;            	
            }
            	
                
			   
			if(model.BmanNo!=null)
			{
            	parameters[14].Value = model.BmanNo;            	
            }
            	
                
			   
			if(model.Romark!=null)
			{
            	parameters[15].Value = model.Romark;            	
            }
            	
                
			   
			if(model.TitleType!=null)
			{
            	parameters[16].Value = model.TitleType;            	
            }
            	
                
			   
			if(model.UploadType!=null)
			{
            	parameters[17].Value = model.UploadType;            	
            }
            	
                
			   
			if(model.PrintType!=null)
			{
            	parameters[18].Value = model.PrintType;            	
            }
            	
                
			   
			if(model.InputDataType!=null)
			{
            	parameters[19].Value = model.InputDataType;            	
            }
            	
                
			   
			if(model.ReportPageType!=null)
			{
            	parameters[20].Value = model.ReportPageType;            	
            }
            	
                
			   
			if(model.ClientArea!=null)
			{
            	parameters[21].Value = model.ClientArea;            	
            }
            	
                
			   
			if(model.ClientStyle!=null)
			{
            	parameters[22].Value = model.ClientStyle;            	
            }
            	
                
			   
			if(model.FaxNo!=null)
			{
            	parameters[23].Value = model.FaxNo;            	
            }
            	
                
			   
			if(model.AutoFax!=null)
			{
            	parameters[24].Value = model.AutoFax;            	
            }
            	
                
			   
			if(model.ClientReportTitle!=null)
			{
            	parameters[25].Value = model.ClientReportTitle;            	
            }
            	
                
			   
			if(model.IsPrintItem!=null)
			{
            	parameters[26].Value = model.IsPrintItem;            	
            }
            	
                
			   
			if(model.CZDY1!=null)
			{
            	parameters[27].Value = model.CZDY1;            	
            }
            	
                
			   
			if(model.CZDY2!=null)
			{
            	parameters[28].Value = model.CZDY2;            	
            }
            	
                
			   
			if(model.CZDY3!=null)
			{
            	parameters[29].Value = model.CZDY3;            	
            }
            	
                
			   
			if(model.CZDY4!=null)
			{
            	parameters[30].Value = model.CZDY4;            	
            }
            	
                
			   
			if(model.CZDY5!=null)
			{
            	parameters[31].Value = model.CZDY5;            	
            }
            	
                
			   
			if(model.CZDY6!=null)
			{
            	parameters[32].Value = model.CZDY6;            	
            }
            	
                
			   
			if(model.LinkManPosition!=null)
			{
            	parameters[33].Value = model.LinkManPosition;            	
            }
            	
                
			   
			if(model.LinkMan1!=null)
			{
            	parameters[34].Value = model.LinkMan1;            	
            }
            	
                
			   
			if(model.LinkManPosition1!=null)
			{
            	parameters[35].Value = model.LinkManPosition1;            	
            }
            	
                
			   
			if(model.ClientCode!=null)
			{
            	parameters[36].Value = model.ClientCode;            	
            }
            	
                
			   
			if(model.CWAccountDate!=null)
			{
            	parameters[37].Value = model.CWAccountDate;            	
            }
            	
                
			   
			if(model.NFClientType!=null)
			{
            	parameters[38].Value = model.NFClientType;            	
            }
            	
                
			   
			if(model.RelationName!=null)
			{
            	parameters[39].Value = model.RelationName;            	
            }
            	
                
			   
			if(model.WebLisSourceOrgID!=null)
			{
            	parameters[40].Value = model.WebLisSourceOrgID;            	
            }
            	
                
			   
			if(model.GroupName!=null)
			{
            	parameters[41].Value = model.GroupName;            	
            }
            	
                
				
                
				
                
			   
			if(model.StandCode!=null)
			{
            	parameters[42].Value = model.StandCode;            	
            }
            	
                
			   
			if(model.ZFStandCode!=null)
			{
            	parameters[43].Value = model.ZFStandCode;            	
            }
            	
                
			   
			if(model.UseFlag!=null)
			{
            	parameters[44].Value = model.UseFlag;            	
            }
            	
                        
			if (idb.ExecuteNonQuery(strSql.ToString(),parameters) > 0)
            {
                return d_log.OperateLog("CLIENTELE", "", "", DateTime.Now, 1);
            }
            else
                return -1;
		}
		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(string LabCode,int LabClIENTNO)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from B_Lab_CLIENTELE ");
			strSql.Append(" where LabCode=@LabCode and LabClIENTNO=@LabClIENTNO ");
						SqlParameter[] parameters = {
					new SqlParameter("@LabCode", SqlDbType.VarChar,50),
					new SqlParameter("@LabClIENTNO", SqlDbType.Int,4)};
			parameters[0].Value = LabCode;
			parameters[1].Value = LabClIENTNO;


			return idb.ExecuteNonQuery(strSql.ToString(),parameters);
			
		}
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteList(string ClIENTIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from B_Lab_CLIENTELE ");
			strSql.Append(" where ID in ("+ClIENTIDlist + ")  ");
			return idb.ExecuteNonQuery(strSql.ToString());
			
		}
				
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZhiFang.Model.Lab_CLIENTELE GetModel(string LabCode,int LabClIENTNO)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ClIENTID, LabCode, LabClIENTNO, CNAME, ENAME, SHORTCODE, ISUSE, LINKMAN, PHONENUM1, ADDRESS, MAILNO, EMAIL, PRINCIPAL, PHONENUM2, CLIENTTYPE, BmanNo, Romark, TitleType, UploadType, PrintType, InputDataType, ReportPageType, ClientArea, ClientStyle, FaxNo, AutoFax, ClientReportTitle, IsPrintItem, CZDY1, CZDY2, CZDY3, CZDY4, CZDY5, CZDY6, LinkManPosition, LinkMan1, LinkManPosition1, ClientCode, CWAccountDate, NFClientType, RelationName, WebLisSourceOrgID, GroupName, AddTime, StandCode, ZFStandCode, UseFlag  ");			
			strSql.Append("  from B_Lab_CLIENTELE ");
			strSql.Append(" where LabCode=@LabCode and LabClIENTNO=@LabClIENTNO ");
						SqlParameter[] parameters = {
					new SqlParameter("@LabCode", SqlDbType.VarChar,50),
					new SqlParameter("@LabClIENTNO", SqlDbType.Int,4)};
			parameters[0].Value = LabCode;
			parameters[1].Value = LabClIENTNO;

			
			ZhiFang.Model.Lab_CLIENTELE model=new ZhiFang.Model.Lab_CLIENTELE();
			DataSet ds=idb.ExecuteDataSet(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																
				if(ds.Tables[0].Rows[0]["ClIENTID"].ToString()!="")
				{
					model.ClIENTID=int.Parse(ds.Tables[0].Rows[0]["ClIENTID"].ToString());
				}
																																								
				model.LabCode= ds.Tables[0].Rows[0]["LabCode"].ToString();
																																
				if(ds.Tables[0].Rows[0]["LabClIENTNO"].ToString()!="")
				{
					model.LabClIENTNO=int.Parse(ds.Tables[0].Rows[0]["LabClIENTNO"].ToString());
				}
																																								
				model.CNAME= ds.Tables[0].Rows[0]["CNAME"].ToString();
																																				
				model.ENAME= ds.Tables[0].Rows[0]["ENAME"].ToString();
																																				
				model.SHORTCODE= ds.Tables[0].Rows[0]["SHORTCODE"].ToString();
																																
				if(ds.Tables[0].Rows[0]["ISUSE"].ToString()!="")
				{
					model.ISUSE=int.Parse(ds.Tables[0].Rows[0]["ISUSE"].ToString());
				}
																																								
				model.LINKMAN= ds.Tables[0].Rows[0]["LINKMAN"].ToString();
																																				
				model.PHONENUM1= ds.Tables[0].Rows[0]["PHONENUM1"].ToString();
																																				
				model.ADDRESS= ds.Tables[0].Rows[0]["ADDRESS"].ToString();
																																				
				model.MAILNO= ds.Tables[0].Rows[0]["MAILNO"].ToString();
																																				
				model.EMAIL= ds.Tables[0].Rows[0]["EMAIL"].ToString();
																																				
				model.PRINCIPAL= ds.Tables[0].Rows[0]["PRINCIPAL"].ToString();
																																				
				model.PHONENUM2= ds.Tables[0].Rows[0]["PHONENUM2"].ToString();
																																
				if(ds.Tables[0].Rows[0]["CLIENTTYPE"].ToString()!="")
				{
					model.CLIENTTYPE=int.Parse(ds.Tables[0].Rows[0]["CLIENTTYPE"].ToString());
				}
																																				
				if(ds.Tables[0].Rows[0]["BmanNo"].ToString()!="")
				{
					model.BmanNo=int.Parse(ds.Tables[0].Rows[0]["BmanNo"].ToString());
				}
																																								
				model.Romark= ds.Tables[0].Rows[0]["Romark"].ToString();
																																
				if(ds.Tables[0].Rows[0]["TitleType"].ToString()!="")
				{
					model.TitleType=int.Parse(ds.Tables[0].Rows[0]["TitleType"].ToString());
				}
																																				
				if(ds.Tables[0].Rows[0]["UploadType"].ToString()!="")
				{
					model.UploadType=int.Parse(ds.Tables[0].Rows[0]["UploadType"].ToString());
				}
																																				
				if(ds.Tables[0].Rows[0]["PrintType"].ToString()!="")
				{
					model.PrintType=int.Parse(ds.Tables[0].Rows[0]["PrintType"].ToString());
				}
																																				
				if(ds.Tables[0].Rows[0]["InputDataType"].ToString()!="")
				{
					model.InputDataType=int.Parse(ds.Tables[0].Rows[0]["InputDataType"].ToString());
				}
																																				
				if(ds.Tables[0].Rows[0]["ReportPageType"].ToString()!="")
				{
					model.ReportPageType=int.Parse(ds.Tables[0].Rows[0]["ReportPageType"].ToString());
				}
																																								
				model.ClientArea= ds.Tables[0].Rows[0]["ClientArea"].ToString();
																																				
				model.ClientStyle= ds.Tables[0].Rows[0]["ClientStyle"].ToString();
																																				
				model.FaxNo= ds.Tables[0].Rows[0]["FaxNo"].ToString();
																																
				if(ds.Tables[0].Rows[0]["AutoFax"].ToString()!="")
				{
					model.AutoFax=int.Parse(ds.Tables[0].Rows[0]["AutoFax"].ToString());
				}
																																								
				model.ClientReportTitle= ds.Tables[0].Rows[0]["ClientReportTitle"].ToString();
																																
				if(ds.Tables[0].Rows[0]["IsPrintItem"].ToString()!="")
				{
					model.IsPrintItem=int.Parse(ds.Tables[0].Rows[0]["IsPrintItem"].ToString());
				}
																																								
				model.CZDY1= ds.Tables[0].Rows[0]["CZDY1"].ToString();
																																				
				model.CZDY2= ds.Tables[0].Rows[0]["CZDY2"].ToString();
																																				
				model.CZDY3= ds.Tables[0].Rows[0]["CZDY3"].ToString();
																																				
				model.CZDY4= ds.Tables[0].Rows[0]["CZDY4"].ToString();
																																				
				model.CZDY5= ds.Tables[0].Rows[0]["CZDY5"].ToString();
																																				
				model.CZDY6= ds.Tables[0].Rows[0]["CZDY6"].ToString();
																																				
				model.LinkManPosition= ds.Tables[0].Rows[0]["LinkManPosition"].ToString();
																																				
				model.LinkMan1= ds.Tables[0].Rows[0]["LinkMan1"].ToString();
																																				
				model.LinkManPosition1= ds.Tables[0].Rows[0]["LinkManPosition1"].ToString();
																																				
				model.ClientCode= ds.Tables[0].Rows[0]["ClientCode"].ToString();
																																				
				model.CWAccountDate= ds.Tables[0].Rows[0]["CWAccountDate"].ToString();
																																				
				model.NFClientType= ds.Tables[0].Rows[0]["NFClientType"].ToString();
																																				
				model.RelationName= ds.Tables[0].Rows[0]["RelationName"].ToString();
																																				
				model.WebLisSourceOrgID= ds.Tables[0].Rows[0]["WebLisSourceOrgID"].ToString();
																																				
				model.GroupName= ds.Tables[0].Rows[0]["GroupName"].ToString();
																																				
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
			strSql.Append(" FROM B_Lab_CLIENTELE ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return idb.ExecuteDataSet(strSql.ToString());
		}
		
		
		/// <summary>
		/// 根据实体获取DataSet
		/// </summary>
        public DataSet GetList(ZhiFang.Model.Lab_CLIENTELE model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM B_Lab_CLIENTELE where 1=1 ");

            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }


            if (model.LabClIENTNO != 0)
            {
                strSql.Append(" and LabClIENTNO=" + model.LabClIENTNO + " ");
            }

            if (model.CNAME != null)
            {
                strSql.Append(" and CNAME='" + model.CNAME + "' ");
            }

            if (model.ENAME != null)
            {
                strSql.Append(" and ENAME='" + model.ENAME + "' ");
            }

            if (model.SHORTCODE != null)
            {
                strSql.Append(" and SHORTCODE='" + model.SHORTCODE + "' ");
            }

            if (model.LINKMAN != null)
            {
                strSql.Append(" and LINKMAN='" + model.LINKMAN + "' ");
            }

            if (model.PHONENUM1 != null)
            {
                strSql.Append(" and PHONENUM1='" + model.PHONENUM1 + "' ");
            }

            if (model.ADDRESS != null)
            {
                strSql.Append(" and ADDRESS='" + model.ADDRESS + "' ");
            }

            if (model.MAILNO != null)
            {
                strSql.Append(" and MAILNO='" + model.MAILNO + "' ");
            }

            if (model.EMAIL != null)
            {
                strSql.Append(" and EMAIL='" + model.EMAIL + "' ");
            }

            if (model.PRINCIPAL != null)
            {
                strSql.Append(" and PRINCIPAL='" + model.PRINCIPAL + "' ");
            }

            if (model.PHONENUM2 != null)
            {
                strSql.Append(" and PHONENUM2='" + model.PHONENUM2 + "' ");
            }

            if (model.CLIENTTYPE != null)
            {
                strSql.Append(" and CLIENTTYPE=" + model.CLIENTTYPE + " ");
            }

            if (model.BmanNo != null)
            {
                strSql.Append(" and BmanNo=" + model.BmanNo + " ");
            }

            if (model.Romark != null)
            {
                strSql.Append(" and Romark='" + model.Romark + "' ");
            }

            if (model.TitleType != null)
            {
                strSql.Append(" and TitleType=" + model.TitleType + " ");
            }

            if (model.PrintType != null)
            {
                strSql.Append(" and PrintType=" + model.PrintType + " ");
            }

            if (model.ClientArea != null)
            {
                strSql.Append(" and ClientArea='" + model.ClientArea + "' ");
            }

            if (model.ClientStyle != null)
            {
                strSql.Append(" and ClientStyle='" + model.ClientStyle + "' ");
            }

            if (model.FaxNo != null)
            {
                strSql.Append(" and FaxNo='" + model.FaxNo + "' ");
            }

            if (model.AutoFax != null)
            {
                strSql.Append(" and AutoFax=" + model.AutoFax + " ");
            }

            if (model.ClientReportTitle != null)
            {
                strSql.Append(" and ClientReportTitle='" + model.ClientReportTitle + "' ");
            }

            if (model.CZDY1 != null)
            {
                strSql.Append(" and CZDY1='" + model.CZDY1 + "' ");
            }

            if (model.CZDY2 != null)
            {
                strSql.Append(" and CZDY2='" + model.CZDY2 + "' ");
            }

            if (model.CZDY3 != null)
            {
                strSql.Append(" and CZDY3='" + model.CZDY3 + "' ");
            }

            if (model.CZDY4 != null)
            {
                strSql.Append(" and CZDY4='" + model.CZDY4 + "' ");
            }

            if (model.CZDY5 != null)
            {
                strSql.Append(" and CZDY5='" + model.CZDY5 + "' ");
            }

            if (model.CZDY6 != null)
            {
                strSql.Append(" and CZDY6='" + model.CZDY6 + "' ");
            }

            if (model.LinkManPosition != null)
            {
                strSql.Append(" and LinkManPosition='" + model.LinkManPosition + "' ");
            }

            if (model.LinkMan1 != null)
            {
                strSql.Append(" and LinkMan1='" + model.LinkMan1 + "' ");
            }

            if (model.LinkManPosition1 != null)
            {
                strSql.Append(" and LinkManPosition1='" + model.LinkManPosition1 + "' ");
            }

            if (model.ClientCode != null)
            {
                strSql.Append(" and ClientCode='" + model.ClientCode + "' ");
            }

            if (model.CWAccountDate != null)
            {
                strSql.Append(" and CWAccountDate='" + model.CWAccountDate + "' ");
            }

            if (model.NFClientType != null)
            {
                strSql.Append(" and NFClientType='" + model.NFClientType + "' ");
            }

            if (model.RelationName != null)
            {
                strSql.Append(" and RelationName='" + model.RelationName + "' ");
            }

            if (model.WebLisSourceOrgID != null)
            {
                strSql.Append(" and WebLisSourceOrgID='" + model.WebLisSourceOrgID + "' ");
            }

            if (model.GroupName != null)
            {
                strSql.Append(" and GroupName='" + model.GroupName + "' ");
            }

            if (model.StandCode != null)
            {
                strSql.Append(" and StandCode='" + model.StandCode + "' ");
            }

            if (model.ZFStandCode != null)
            {
                strSql.Append(" and ZFStandCode='" + model.ZFStandCode + "' ");
            }
            return idb.ExecuteDataSet(strSql.ToString());
        }
        public DataSet GetListByLike(ZhiFang.Model.Lab_CLIENTELE model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *,'('+convert(varchar(100),LabClIENTNO)+')'+CNAME as LabClIENTNOAndName ");
            strSql.Append(" FROM B_Lab_CLIENTELE where 1=1 ");
            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }

            if (model.LabClIENTNO != 0)
            {
                strSql.Append(" or LabClIENTNO like '%" + model.LabClIENTNO + "%' ");
            }

            if (model.CNAME != null)
            {
                strSql.Append(" or CNAME like '%" + model.CNAME + "%' ");
            }

            if (model.ENAME != null)
            {
                strSql.Append(" or ENAME like '%" + model.ENAME + "%' ");
            }

            if (model.SHORTCODE != null)
            {
                strSql.Append(" or SHORTCODE like '%" + model.SHORTCODE + "%' ");
            }

            if (model.LINKMAN != null)
            {
                strSql.Append(" or LINKMAN like '%" + model.LINKMAN + "%' ");
            }

            if (model.PHONENUM1 != null)
            {
                strSql.Append(" or PHONENUM1 like '%" + model.PHONENUM1 + "%' ");
            }

            if (model.ADDRESS != null)
            {
                strSql.Append(" or ADDRESS like '%" + model.ADDRESS + "%' ");
            }

            if (model.MAILNO != null)
            {
                strSql.Append(" or MAILNO like '%" + model.MAILNO + "%' ");
            }

            if (model.EMAIL != null)
            {
                strSql.Append(" or EMAIL like '%" + model.EMAIL + "%' ");
            }

            if (model.PRINCIPAL != null)
            {
                strSql.Append(" or PRINCIPAL like '%" + model.PRINCIPAL + "%' ");
            }

            if (model.PHONENUM2 != null)
            {
                strSql.Append(" or PHONENUM2 like '%" + model.PHONENUM2 + "%' ");
            }

            if (model.CLIENTTYPE != null)
            {
                strSql.Append(" or CLIENTTYPE like '%" + model.CLIENTTYPE + "%' ");
            }

            if (model.BmanNo != null)
            {
                strSql.Append(" or BmanNo like '%" + model.BmanNo + "%' ");
            }

            if (model.Romark != null)
            {
                strSql.Append(" or Romark like '%" + model.Romark + "%' ");
            }

            if (model.TitleType != null)
            {
                strSql.Append(" or TitleType like '%" + model.TitleType + "%' ");
            }

            if (model.UploadType != null)
            {
                strSql.Append(" or UploadType like '%" + model.UploadType + "%' ");
            }

            if (model.PrintType != null)
            {
                strSql.Append(" or PrintType like '%" + model.PrintType + "%' ");
            }

            if (model.ClientArea != null)
            {
                strSql.Append(" or ClientArea like '%" + model.ClientArea + "%' ");
            }

            if (model.ClientStyle != null)
            {
                strSql.Append(" or ClientStyle like '%" + model.ClientStyle + "%' ");
            }

            if (model.FaxNo != null)
            {
                strSql.Append(" or FaxNo like '%" + model.FaxNo + "%' ");
            }

            if (model.AutoFax != null)
            {
                strSql.Append(" or AutoFax like '%" + model.AutoFax + "%' ");
            }

            if (model.ClientReportTitle != null)
            {
                strSql.Append(" or ClientReportTitle like '%" + model.ClientReportTitle + "%' ");
            }

            if (model.CZDY1 != null)
            {
                strSql.Append(" or CZDY1 like '%" + model.CZDY1 + "%' ");
            }

            if (model.CZDY2 != null)
            {
                strSql.Append(" or CZDY2 like '%" + model.CZDY2 + "%' ");
            }

            if (model.CZDY3 != null)
            {
                strSql.Append(" or CZDY3 like '%" + model.CZDY3 + "%' ");
            }

            if (model.CZDY4 != null)
            {
                strSql.Append(" or CZDY4 like '%" + model.CZDY4 + "%' ");
            }

            if (model.CZDY5 != null)
            {
                strSql.Append(" or CZDY5 like '%" + model.CZDY5 + "%' ");
            }

            if (model.CZDY6 != null)
            {
                strSql.Append(" or CZDY6 like '%" + model.CZDY6 + "%' ");
            }

            if (model.LinkManPosition != null)
            {
                strSql.Append(" or LinkManPosition like '%" + model.LinkManPosition + "%' ");
            }

            if (model.LinkMan1 != null)
            {
                strSql.Append(" or LinkMan1 like '%" + model.LinkMan1 + "%' ");
            }

            if (model.LinkManPosition1 != null)
            {
                strSql.Append(" or LinkManPosition1 like '%" + model.LinkManPosition1 + "%' ");
            }

            if (model.ClientCode != null)
            {
                strSql.Append(" or ClientCode like '%" + model.ClientCode + "%' ");
            }

            if (model.CWAccountDate != null)
            {
                strSql.Append(" or CWAccountDate like '%" + model.CWAccountDate + "%' ");
            }

            if (model.NFClientType != null)
            {
                strSql.Append(" or NFClientType like '%" + model.NFClientType + "%' ");
            }

            if (model.RelationName != null)
            {
                strSql.Append(" or RelationName like '%" + model.RelationName + "%' ");
            }

            if (model.WebLisSourceOrgID != null)
            {
                strSql.Append(" or WebLisSourceOrgID like '%" + model.WebLisSourceOrgID + "%' ");
            }

            if (model.GroupName != null)
            {
                strSql.Append(" or GroupName like '%" + model.GroupName + "%' ");
            }

            return idb.ExecuteDataSet(strSql.ToString());
        }
		
		/// <summary>
		/// 获取总记录
		/// </summary>
		public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_Lab_CLIENTELE ");
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
        public int GetTotalCount(ZhiFang.Model.Lab_CLIENTELE model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            strSql.Append("select count(*) FROM B_Lab_CLIENTELE where 1=1 ");
            if(model.LabCode !=null)
            {            
            	strSql.Append(" and LabCode='"+model.LabCode+"' ");
            } 
            if (model != null)
            {
                if (model.LabClIENTNO != -1)
                {
                    strWhere.Append(" and ( LabClIENTNO like '%" + model.LabClIENTNO + "%' ");
                }
                if (model.CNAME != null)
                {
                    if (strWhere.Length == 0)
                        strWhere.Append(" and ( CNAME like '%" + model.CNAME + "%' ");
                    else
                        strWhere.Append(" or CNAME like '%" + model.CNAME + "%' ");
                }
                if (model.SHORTCODE != null)
                {
                    if (strWhere.Length == 0)
                        strWhere.Append(" and ( SHORTCODE like '%" + model.SHORTCODE + "%' ");
                    else
                        strWhere.Append(" or SHORTCODE like '%" + model.SHORTCODE + "%' ");
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
        public DataSet GetListByPage(ZhiFang.Model.Lab_CLIENTELE model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            if (model.LabClIENTNO != -1)
            {
                strWhere.Append(" and ( LabClIENTNO like '%" + model.LabClIENTNO + "%' ");
            }
            if (model.CNAME != null)
            {
                if (strWhere.Length == 0)
                    strWhere.Append(" and ( CNAME like '%" + model.CNAME + "%' ");
                else
                    strWhere.Append(" or CNAME like '%" + model.CNAME + "%' ");
            }
            if (model.ENAME != null)
            {
                if (strWhere.Length == 0)
                    strWhere.Append(" and ( ENAME like '%" + model.ENAME + "%' ");
                else
                    strWhere.Append(" or ENAME like '%" + model.ENAME + "%' ");
            }
            if (model.SHORTCODE != null)
            {
                if (strWhere.Length == 0)
                    strWhere.Append(" and ( SHORTCODE like '%" + model.SHORTCODE + "%' ");
                else
                    strWhere.Append(" or SHORTCODE like '%" + model.SHORTCODE + "%' ");
            }
            if (strWhere.Length != 0)
                strWhere.Append(" ) ");

            strSql.Append("select top " + nowPageSize + " * from B_Lab_CLIENTELE where 1=1  ");

            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }
            strSql.Append(" " + strWhere.ToString() + " and ClIENTID not in ");
            strSql.Append("(select top " + (nowPageSize * nowPageNum) + " ClIENTID from B_Lab_CLIENTELE where 1=1  ");
            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }
            strSql.Append(" " + strWhere.ToString() + "  order by ClIENTID) order by ClIENTID  ");
            return idb.ExecuteDataSet(strSql.ToString());
        }
        
        public bool Exists(string LabCode,int LabClIENTNO)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from B_Lab_CLIENTELE ");
			strSql.Append(" where LabCode=@LabCode and LabClIENTNO=@LabClIENTNO ");
						SqlParameter[] parameters = {
					new SqlParameter("@LabCode", SqlDbType.VarChar,50),
					new SqlParameter("@LabClIENTNO", SqlDbType.Int,4)};
			parameters[0].Value = LabCode;
			parameters[1].Value = LabClIENTNO;


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
            strSql.Append("select count(1) from B_Lab_CLIENTELE where 1=1 ");
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
            return idb.GetMaxID("LabCode,LabClIENTNO","B_Lab_CLIENTELE");
        }

        public DataSet GetList(int Top, ZhiFang.Model.Lab_CLIENTELE model, string filedOrder)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" * ");
			strSql.Append(" FROM B_Lab_CLIENTELE ");		
			
			                                                       
                        
            if(model.LabCode !=null)
            {            
            	strSql.Append(" and LabCode='"+model.LabCode+"' ");
            }
                                            
                        
            if(model.LabClIENTNO !=0)
            {            
            	strSql.Append(" and LabClIENTNO='"+model.LabClIENTNO+"' ");
            }
                                            
                        
            if(model.CNAME !=null)
            {            
            	strSql.Append(" and CNAME='"+model.CNAME+"' ");
            }
                                            
                        
            if(model.ENAME !=null)
            {            
            	strSql.Append(" and ENAME='"+model.ENAME+"' ");
            }
                                            
                        
            if(model.SHORTCODE !=null)
            {            
            	strSql.Append(" and SHORTCODE='"+model.SHORTCODE+"' ");
            }
                                            
                        
            if(model.LINKMAN !=null)
            {            
            	strSql.Append(" and LINKMAN='"+model.LINKMAN+"' ");
            }
                                            
                        
            if(model.PHONENUM1 !=null)
            {            
            	strSql.Append(" and PHONENUM1='"+model.PHONENUM1+"' ");
            }
                                            
                        
            if(model.ADDRESS !=null)
            {            
            	strSql.Append(" and ADDRESS='"+model.ADDRESS+"' ");
            }
                                            
                        
            if(model.MAILNO !=null)
            {            
            	strSql.Append(" and MAILNO='"+model.MAILNO+"' ");
            }
                                            
                        
            if(model.EMAIL !=null)
            {            
            	strSql.Append(" and EMAIL='"+model.EMAIL+"' ");
            }
                                            
                        
            if(model.PRINCIPAL !=null)
            {            
            	strSql.Append(" and PRINCIPAL='"+model.PRINCIPAL+"' ");
            }
                                            
                        
            if(model.PHONENUM2 !=null)
            {            
            	strSql.Append(" and PHONENUM2='"+model.PHONENUM2+"' ");
            }
                                            
                        
            if(model.CLIENTTYPE !=null)
            {            
            	strSql.Append(" and CLIENTTYPE='"+model.CLIENTTYPE+"' ");
            }
                                            
                        
            if(model.BmanNo !=null)
            {            
            	strSql.Append(" and BmanNo='"+model.BmanNo+"' ");
            }
                                            
                        
            if(model.Romark !=null)
            {            
            	strSql.Append(" and Romark='"+model.Romark+"' ");
            }
                                            
                        
            if(model.TitleType !=null)
            {            
            	strSql.Append(" and TitleType='"+model.TitleType+"' ");
            }
                                            
                        
            if(model.PrintType !=null)
            {            
            	strSql.Append(" and PrintType='"+model.PrintType+"' ");
            }
                                            
                        
            if(model.ClientArea !=null)
            {            
            	strSql.Append(" and ClientArea='"+model.ClientArea+"' ");
            }
                                            
                        
            if(model.ClientStyle !=null)
            {            
            	strSql.Append(" and ClientStyle='"+model.ClientStyle+"' ");
            }
                                            
                        
            if(model.FaxNo !=null)
            {            
            	strSql.Append(" and FaxNo='"+model.FaxNo+"' ");
            }
                                            
                        
            if(model.AutoFax !=null)
            {            
            	strSql.Append(" and AutoFax='"+model.AutoFax+"' ");
            }
                                            
                        
            if(model.ClientReportTitle !=null)
            {            
            	strSql.Append(" and ClientReportTitle='"+model.ClientReportTitle+"' ");
            }
                                            
                        
            if(model.CZDY1 !=null)
            {            
            	strSql.Append(" and CZDY1='"+model.CZDY1+"' ");
            }
                                            
                        
            if(model.CZDY2 !=null)
            {            
            	strSql.Append(" and CZDY2='"+model.CZDY2+"' ");
            }
                                            
                        
            if(model.CZDY3 !=null)
            {            
            	strSql.Append(" and CZDY3='"+model.CZDY3+"' ");
            }
                                            
                        
            if(model.CZDY4 !=null)
            {            
            	strSql.Append(" and CZDY4='"+model.CZDY4+"' ");
            }
                                            
                        
            if(model.CZDY5 !=null)
            {            
            	strSql.Append(" and CZDY5='"+model.CZDY5+"' ");
            }
                                            
                        
            if(model.CZDY6 !=null)
            {            
            	strSql.Append(" and CZDY6='"+model.CZDY6+"' ");
            }
                                            
                        
            if(model.LinkManPosition !=null)
            {            
            	strSql.Append(" and LinkManPosition='"+model.LinkManPosition+"' ");
            }
                                            
                        
            if(model.LinkMan1 !=null)
            {            
            	strSql.Append(" and LinkMan1='"+model.LinkMan1+"' ");
            }
                                            
                        
            if(model.LinkManPosition1 !=null)
            {            
            	strSql.Append(" and LinkManPosition1='"+model.LinkManPosition1+"' ");
            }
                                            
                        
            if(model.ClientCode !=null)
            {            
            	strSql.Append(" and ClientCode='"+model.ClientCode+"' ");
            }
                                            
                        
            if(model.CWAccountDate !=null)
            {            
            	strSql.Append(" and CWAccountDate='"+model.CWAccountDate+"' ");
            }
                                            
                        
            if(model.NFClientType !=null)
            {            
            	strSql.Append(" and NFClientType='"+model.NFClientType+"' ");
            }
                                            
                        
            if(model.RelationName !=null)
            {            
            	strSql.Append(" and RelationName='"+model.RelationName+"' ");
            }
                                            
                        
            if(model.WebLisSourceOrgID !=null)
            {            
            	strSql.Append(" and WebLisSourceOrgID='"+model.WebLisSourceOrgID+"' ");
            }
                                            
                        
            if(model.GroupName !=null)
            {            
            	strSql.Append(" and GroupName='"+model.GroupName+"' ");
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
			            if (this.Exists(ds.Tables[0].Rows[i]["LabCode"].ToString().Trim(),int.Parse(ds.Tables[0].Rows[i]["LabClIENTNO"].ToString().Trim())))
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
				strSql.Append("insert into B_Lab_CLIENTELE (");			
            	strSql.Append("LabCode,LabClIENTNO,CNAME,ENAME,SHORTCODE,ISUSE,LINKMAN,PHONENUM1,ADDRESS,MAILNO,EMAIL,PRINCIPAL,PHONENUM2,CLIENTTYPE,BmanNo,Romark,TitleType,UploadType,PrintType,InputDataType,ReportPageType,ClientArea,ClientStyle,FaxNo,AutoFax,ClientReportTitle,IsPrintItem,CZDY1,CZDY2,CZDY3,CZDY4,CZDY5,CZDY6,LinkManPosition,LinkMan1,LinkManPosition1,ClientCode,CWAccountDate,NFClientType,RelationName,WebLisSourceOrgID,GroupName,StandCode,ZFStandCode,UseFlag");
				strSql.Append(") values (");			
            	            	            	
            	if(dr.Table.Columns["LabCode"]!=null && dr.Table.Columns["LabCode"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["LabCode"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["LabClIENTNO"]!=null && dr.Table.Columns["LabClIENTNO"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["LabClIENTNO"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["CNAME"]!=null && dr.Table.Columns["CNAME"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["CNAME"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["ENAME"]!=null && dr.Table.Columns["ENAME"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["ENAME"].ToString().Trim()+"', ");
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
            	             	            	
            	if(dr.Table.Columns["LINKMAN"]!=null && dr.Table.Columns["LINKMAN"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["LINKMAN"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["PHONENUM1"]!=null && dr.Table.Columns["PHONENUM1"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["PHONENUM1"].ToString().Trim()+"', ");
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
            	             	            	
            	if(dr.Table.Columns["MAILNO"]!=null && dr.Table.Columns["MAILNO"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["MAILNO"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["EMAIL"]!=null && dr.Table.Columns["EMAIL"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["EMAIL"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["PRINCIPAL"]!=null && dr.Table.Columns["PRINCIPAL"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["PRINCIPAL"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["PHONENUM2"]!=null && dr.Table.Columns["PHONENUM2"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["PHONENUM2"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["CLIENTTYPE"]!=null && dr.Table.Columns["CLIENTTYPE"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["CLIENTTYPE"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["BmanNo"]!=null && dr.Table.Columns["BmanNo"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["BmanNo"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["Romark"]!=null && dr.Table.Columns["Romark"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["Romark"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["TitleType"]!=null && dr.Table.Columns["TitleType"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["TitleType"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["UploadType"]!=null && dr.Table.Columns["UploadType"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["UploadType"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["PrintType"]!=null && dr.Table.Columns["PrintType"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["PrintType"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["InputDataType"]!=null && dr.Table.Columns["InputDataType"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["InputDataType"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["ReportPageType"]!=null && dr.Table.Columns["ReportPageType"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["ReportPageType"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["ClientArea"]!=null && dr.Table.Columns["ClientArea"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["ClientArea"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["ClientStyle"]!=null && dr.Table.Columns["ClientStyle"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["ClientStyle"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["FaxNo"]!=null && dr.Table.Columns["FaxNo"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["FaxNo"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["AutoFax"]!=null && dr.Table.Columns["AutoFax"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["AutoFax"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["ClientReportTitle"]!=null && dr.Table.Columns["ClientReportTitle"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["ClientReportTitle"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["IsPrintItem"]!=null && dr.Table.Columns["IsPrintItem"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["IsPrintItem"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["CZDY1"]!=null && dr.Table.Columns["CZDY1"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["CZDY1"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["CZDY2"]!=null && dr.Table.Columns["CZDY2"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["CZDY2"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["CZDY3"]!=null && dr.Table.Columns["CZDY3"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["CZDY3"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["CZDY4"]!=null && dr.Table.Columns["CZDY4"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["CZDY4"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["CZDY5"]!=null && dr.Table.Columns["CZDY5"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["CZDY5"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["CZDY6"]!=null && dr.Table.Columns["CZDY6"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["CZDY6"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["LinkManPosition"]!=null && dr.Table.Columns["LinkManPosition"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["LinkManPosition"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["LinkMan1"]!=null && dr.Table.Columns["LinkMan1"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["LinkMan1"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["LinkManPosition1"]!=null && dr.Table.Columns["LinkManPosition1"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["LinkManPosition1"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["ClientCode"]!=null && dr.Table.Columns["ClientCode"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["ClientCode"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["CWAccountDate"]!=null && dr.Table.Columns["CWAccountDate"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["CWAccountDate"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["NFClientType"]!=null && dr.Table.Columns["NFClientType"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["NFClientType"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["RelationName"]!=null && dr.Table.Columns["RelationName"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["RelationName"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["WebLisSourceOrgID"]!=null && dr.Table.Columns["WebLisSourceOrgID"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["WebLisSourceOrgID"].ToString().Trim()+"', ");
            	}
            	else
            	{
            		strSql.Append(" null, ");
            	}
            	             	            	
            	if(dr.Table.Columns["GroupName"]!=null && dr.Table.Columns["GroupName"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["GroupName"].ToString().Trim()+"', ");
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
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Digitlab8.B_Lab_CLIENTELE.AddByDataRow 同步数据时异常：", ex);
                return 0;
            }          
        }
        public int UpdateByDataRow(DataRow dr)
        {
            try
            {
               StringBuilder strSql=new StringBuilder();
			   strSql.Append("update B_Lab_CLIENTELE set ");
			   			    			    			    			       
			    			    
			    if( dr.Table.Columns["CNAME"]!=null && dr.Table.Columns["CNAME"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" CNAME = '"+dr["CNAME"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["ENAME"]!=null && dr.Table.Columns["ENAME"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" ENAME = '"+dr["ENAME"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["SHORTCODE"]!=null && dr.Table.Columns["SHORTCODE"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" SHORTCODE = '"+dr["SHORTCODE"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["ISUSE"]!=null && dr.Table.Columns["ISUSE"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" ISUSE = '"+dr["ISUSE"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["LINKMAN"]!=null && dr.Table.Columns["LINKMAN"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" LINKMAN = '"+dr["LINKMAN"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["PHONENUM1"]!=null && dr.Table.Columns["PHONENUM1"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" PHONENUM1 = '"+dr["PHONENUM1"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["ADDRESS"]!=null && dr.Table.Columns["ADDRESS"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" ADDRESS = '"+dr["ADDRESS"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["MAILNO"]!=null && dr.Table.Columns["MAILNO"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" MAILNO = '"+dr["MAILNO"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["EMAIL"]!=null && dr.Table.Columns["EMAIL"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" EMAIL = '"+dr["EMAIL"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["PRINCIPAL"]!=null && dr.Table.Columns["PRINCIPAL"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" PRINCIPAL = '"+dr["PRINCIPAL"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["PHONENUM2"]!=null && dr.Table.Columns["PHONENUM2"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" PHONENUM2 = '"+dr["PHONENUM2"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["CLIENTTYPE"]!=null && dr.Table.Columns["CLIENTTYPE"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" CLIENTTYPE = '"+dr["CLIENTTYPE"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["BmanNo"]!=null && dr.Table.Columns["BmanNo"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" BmanNo = '"+dr["BmanNo"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["Romark"]!=null && dr.Table.Columns["Romark"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" Romark = '"+dr["Romark"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["TitleType"]!=null && dr.Table.Columns["TitleType"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" TitleType = '"+dr["TitleType"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["UploadType"]!=null && dr.Table.Columns["UploadType"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" UploadType = '"+dr["UploadType"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["PrintType"]!=null && dr.Table.Columns["PrintType"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" PrintType = '"+dr["PrintType"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["InputDataType"]!=null && dr.Table.Columns["InputDataType"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" InputDataType = '"+dr["InputDataType"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["ReportPageType"]!=null && dr.Table.Columns["ReportPageType"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" ReportPageType = '"+dr["ReportPageType"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["ClientArea"]!=null && dr.Table.Columns["ClientArea"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" ClientArea = '"+dr["ClientArea"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["ClientStyle"]!=null && dr.Table.Columns["ClientStyle"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" ClientStyle = '"+dr["ClientStyle"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["FaxNo"]!=null && dr.Table.Columns["FaxNo"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" FaxNo = '"+dr["FaxNo"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["AutoFax"]!=null && dr.Table.Columns["AutoFax"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" AutoFax = '"+dr["AutoFax"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["ClientReportTitle"]!=null && dr.Table.Columns["ClientReportTitle"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" ClientReportTitle = '"+dr["ClientReportTitle"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["IsPrintItem"]!=null && dr.Table.Columns["IsPrintItem"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" IsPrintItem = '"+dr["IsPrintItem"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["CZDY1"]!=null && dr.Table.Columns["CZDY1"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" CZDY1 = '"+dr["CZDY1"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["CZDY2"]!=null && dr.Table.Columns["CZDY2"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" CZDY2 = '"+dr["CZDY2"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["CZDY3"]!=null && dr.Table.Columns["CZDY3"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" CZDY3 = '"+dr["CZDY3"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["CZDY4"]!=null && dr.Table.Columns["CZDY4"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" CZDY4 = '"+dr["CZDY4"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["CZDY5"]!=null && dr.Table.Columns["CZDY5"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" CZDY5 = '"+dr["CZDY5"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["CZDY6"]!=null && dr.Table.Columns["CZDY6"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" CZDY6 = '"+dr["CZDY6"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["LinkManPosition"]!=null && dr.Table.Columns["LinkManPosition"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" LinkManPosition = '"+dr["LinkManPosition"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["LinkMan1"]!=null && dr.Table.Columns["LinkMan1"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" LinkMan1 = '"+dr["LinkMan1"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["LinkManPosition1"]!=null && dr.Table.Columns["LinkManPosition1"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" LinkManPosition1 = '"+dr["LinkManPosition1"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["ClientCode"]!=null && dr.Table.Columns["ClientCode"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" ClientCode = '"+dr["ClientCode"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["CWAccountDate"]!=null && dr.Table.Columns["CWAccountDate"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" CWAccountDate = '"+dr["CWAccountDate"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["NFClientType"]!=null && dr.Table.Columns["NFClientType"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" NFClientType = '"+dr["NFClientType"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["RelationName"]!=null && dr.Table.Columns["RelationName"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" RelationName = '"+dr["RelationName"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["WebLisSourceOrgID"]!=null && dr.Table.Columns["WebLisSourceOrgID"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" WebLisSourceOrgID = '"+dr["WebLisSourceOrgID"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["GroupName"]!=null && dr.Table.Columns["GroupName"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" GroupName = '"+dr["GroupName"].ToString().Trim()+"' , ");
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
                strSql.Append(" where LabCode='" + dr["LabCode"].ToString().Trim() + "' and  LabClIENTNO='" + dr["LabClIENTNO"].ToString().Trim() + "' ");
						
                return idb.ExecuteNonQuery(strSql.ToString());
            }
            catch(Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Digitlab8.B_Lab_CLIENTELE .UpdateByDataRow同步数据时异常：", ex);
                return 0;
            }
        }
		
   
	}
}

