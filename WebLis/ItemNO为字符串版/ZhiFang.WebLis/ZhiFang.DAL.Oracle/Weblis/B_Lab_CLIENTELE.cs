using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.Oracle.weblis  
{
	 	//B_Lab_CLIENTELE
		
	public partial class B_Lab_CLIENTELE: BaseDALLisDB, IDLab_CLIENTELE	{	
        public B_Lab_CLIENTELE(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
		public B_Lab_CLIENTELE()
		{
        }
		D_LogInfo d_log = new D_LogInfo();
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ZhiFang.Model.Lab_CLIENTELE model)
		{
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.LabCode != null)
            {
                strSql1.Append("LabCode,");
                strSql2.Append("'" + model.LabCode + "',");
            }
            if (model.LabClIENTNO != null)
            {
                strSql1.Append("LabClIENTNO,");
                strSql2.Append("" + model.LabClIENTNO + ",");
            }
            if (model.CNAME != null)
            {
                strSql1.Append("CNAME,");
                strSql2.Append("'" + model.CNAME + "',");
            }
            if (model.ENAME != null)
            {
                strSql1.Append("ENAME,");
                strSql2.Append("'" + model.ENAME + "',");
            }
            if (model.SHORTCODE != null)
            {
                strSql1.Append("SHORTCODE,");
                strSql2.Append("'" + model.SHORTCODE + "',");
            }
            if (model.ISUSE != null)
            {
                strSql1.Append("ISUSE,");
                strSql2.Append("" + model.ISUSE + ",");
            }
            if (model.LINKMAN != null)
            {
                strSql1.Append("LINKMAN,");
                strSql2.Append("'" + model.LINKMAN + "',");
            }
            if (model.PHONENUM1 != null)
            {
                strSql1.Append("PHONENUM1,");
                strSql2.Append("'" + model.PHONENUM1 + "',");
            }
            if (model.ADDRESS != null)
            {
                strSql1.Append("ADDRESS,");
                strSql2.Append("'" + model.ADDRESS + "',");
            }
            if (model.MAILNO != null)
            {
                strSql1.Append("MAILNO,");
                strSql2.Append("'" + model.MAILNO + "',");
            }
            if (model.EMAIL != null)
            {
                strSql1.Append("EMAIL,");
                strSql2.Append("'" + model.EMAIL + "',");
            }
            if (model.PRINCIPAL != null)
            {
                strSql1.Append("PRINCIPAL,");
                strSql2.Append("'" + model.PRINCIPAL + "',");
            }
            if (model.PHONENUM2 != null)
            {
                strSql1.Append("PHONENUM2,");
                strSql2.Append("'" + model.PHONENUM2 + "',");
            }
            if (model.CLIENTTYPE != null)
            {
                strSql1.Append("CLIENTTYPE,");
                strSql2.Append("" + model.CLIENTTYPE + ",");
            }
            if (model.BmanNo != null)
            {
                strSql1.Append("BmanNo,");
                strSql2.Append("" + model.BmanNo + ",");
            }
            if (model.Romark != null)
            {
                strSql1.Append("Romark,");
                strSql2.Append("'" + model.Romark + "',");
            }
            if (model.TitleType != null)
            {
                strSql1.Append("TitleType,");
                strSql2.Append("" + model.TitleType + ",");
            }
            if (model.UploadType != null)
            {
                strSql1.Append("UploadType,");
                strSql2.Append("" + model.UploadType + ",");
            }
            if (model.PrintType != null)
            {
                strSql1.Append("PrintType,");
                strSql2.Append("" + model.PrintType + ",");
            }
            if (model.InputDataType != null)
            {
                strSql1.Append("InputDataType,");
                strSql2.Append("" + model.InputDataType + ",");
            }
            if (model.ReportPageType != null)
            {
                strSql1.Append("ReportPageType,");
                strSql2.Append("" + model.ReportPageType + ",");
            }
            if (model.ClientArea != null)
            {
                strSql1.Append("ClientArea,");
                strSql2.Append("'" + model.ClientArea + "',");
            }
            if (model.ClientStyle != null)
            {
                strSql1.Append("ClientStyle,");
                strSql2.Append("'" + model.ClientStyle + "',");
            }
            if (model.RelationName != null)
            {
                strSql1.Append("RelationName,");
                strSql2.Append("'" + model.RelationName + "',");
            }
            if (model.WebLisSourceOrgID != null)
            {
                strSql1.Append("WebLisSourceOrgID,");
                strSql2.Append("'" + model.WebLisSourceOrgID + "',");
            }
            if (model.GroupName != null)
            {
                strSql1.Append("GroupName,");
                strSql2.Append("'" + model.GroupName + "',");
            }
                strSql1.Append("DTimeStampe,");
                strSql2.Append("sysdate+ '1.1234',");
            
            if (model.AddTime != null)
            {
                strSql1.Append("AddTime,");
                strSql2.Append("to_date('" + model.AddTime.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");
            }
            if (model.StandCode != null)
            {
                strSql1.Append("StandCode,");
                strSql2.Append("'" + model.StandCode + "',");
            }
            if (model.ZFStandCode != null)
            {
                strSql1.Append("ZFStandCode,");
                strSql2.Append("'" + model.ZFStandCode + "',");
            }
            if (model.UseFlag != null)
            {
                strSql1.Append("UseFlag,");
                strSql2.Append("" + model.UseFlag + ",");
            }
            strSql.Append("insert into B_Lab_CLIENTELE(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
        
            if (DbHelperSQL.ExecuteNonQuery(strSql.ToString()) > 0)
            {
                return d_log.OperateLog("CLIENTELE", "", "", DateTime.Now, 1);
            }
            else
                return -1;


            //StringBuilder strSql=new StringBuilder();
            //strSql.Append("insert into B_Lab_CLIENTELE(");			
            //strSql.Append("LabCode,LabClIENTNO,CNAME,ENAME,SHORTCODE,ISUSE,LINKMAN,PHONENUM1,ADDRESS,MAILNO,EMAIL,PRINCIPAL,PHONENUM2,CLIENTTYPE,BmanNo,Romark,TitleType,UploadType,PrintType,InputDataType,Reportpagetype,ClientArea,ClientStyle,RelationName,WebLisSourceOrgID,GroupName,StandCode,ZFStandCode,UseFlag");
            //strSql.Append(") values (");
            //strSql.Append("@LabCode,@LabClIENTNO,@CNAME,@ENAME,@SHORTCODE,@ISUSE,@LINKMAN,@PHONENUM1,@ADDRESS,@MAILNO,@EMAIL,@PRINCIPAL,@PHONENUM2,@CLIENTTYPE,@BmanNo,@Romark,@TitleType,@UploadType,@PrintType,@InputDataType,@Reportpagetype,@ClientArea,@ClientStyle,@RelationName,@WebLisSourceOrgID,@GroupName,@StandCode,@ZFStandCode,@UseFlag");            
            //strSql.Append(") ");            
            
            //SqlParameter[] parameters = {
            //            new SqlParameter("@LabCode", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@LabClIENTNO", SqlDbType.Int,4) ,            
            //            new SqlParameter("@CNAME", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@ENAME", SqlDbType.VarChar,40) ,            
            //            new SqlParameter("@SHORTCODE", SqlDbType.VarChar,40) ,            
            //            new SqlParameter("@ISUSE", SqlDbType.Int,4) ,            
            //            new SqlParameter("@LINKMAN", SqlDbType.VarChar,40) ,            
            //            new SqlParameter("@PHONENUM1", SqlDbType.VarChar,40) ,            
            //            new SqlParameter("@ADDRESS", SqlDbType.VarChar,40) ,            
            //            new SqlParameter("@MAILNO", SqlDbType.VarChar,40) ,            
            //            new SqlParameter("@EMAIL", SqlDbType.VarChar,40) ,            
            //            new SqlParameter("@PRINCIPAL", SqlDbType.VarChar,40) ,            
            //            new SqlParameter("@PHONENUM2", SqlDbType.VarChar,40) ,            
            //            new SqlParameter("@CLIENTTYPE", SqlDbType.Int,4) ,            
            //            new SqlParameter("@BmanNo", SqlDbType.Int,4) ,            
            //            new SqlParameter("@Romark", SqlDbType.VarChar,200) ,            
            //            new SqlParameter("@TitleType", SqlDbType.Int,4) ,            
            //            new SqlParameter("@UploadType", SqlDbType.Int,4) ,            
            //            new SqlParameter("@PrintType", SqlDbType.Int,4) ,            
            //            new SqlParameter("@InputDataType", SqlDbType.Int,4) ,            
            //            new SqlParameter("@Reportpagetype", SqlDbType.Int,4) ,            
            //            new SqlParameter("@ClientArea", SqlDbType.VarChar,40) ,            
            //            new SqlParameter("@ClientStyle", SqlDbType.VarChar,40) ,            
            //            new SqlParameter("@RelationName", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@WebLisSourceOrgID", SqlDbType.VarChar,10) ,            
            //            new SqlParameter("@GroupName", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@ZFStandCode", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@UseFlag", SqlDbType.Int,4)             
              
            //};
			            
            //parameters[0].Value = model.LabCode;                        
            //parameters[1].Value = model.LabClIENTNO;                        
            //parameters[2].Value = model.CNAME;                        
            //parameters[3].Value = model.ENAME;                        
            //parameters[4].Value = model.SHORTCODE;                        
            //parameters[5].Value = model.ISUSE;                        
            //parameters[6].Value = model.LINKMAN;                        
            //parameters[7].Value = model.PHONENUM1;                        
            //parameters[8].Value = model.ADDRESS;                        
            //parameters[9].Value = model.MAILNO;                        
            //parameters[10].Value = model.EMAIL;                        
            //parameters[11].Value = model.PRINCIPAL;                        
            //parameters[12].Value = model.PHONENUM2;                        
            //parameters[13].Value = model.CLIENTTYPE;                        
            //parameters[14].Value = model.BmanNo;                        
            //parameters[15].Value = model.Romark;                        
            //parameters[16].Value = model.TitleType;                        
            //parameters[17].Value = model.UploadType;                        
            //parameters[18].Value = model.PrintType;                        
            //parameters[19].Value = model.InputDataType;                        
            //parameters[20].Value = model.ReportPageType;                        
            //parameters[21].Value = model.ClientArea;                        
            //parameters[22].Value = model.ClientStyle;                        
            //parameters[23].Value = model.RelationName;                        
            //parameters[24].Value = model.WebLisSourceOrgID;                        
            //parameters[25].Value = model.GroupName;                        
            //parameters[26].Value = model.StandCode;                        
            //parameters[27].Value = model.ZFStandCode;                        
            //parameters[28].Value = model.UseFlag;                  
            //if (DbHelperSQL.ExecuteNonQuery(strSql.ToString(),parameters) > 0)
            //{
            //    return d_log.OperateLog("CLIENTELE", "", "", DateTime.Now, 1);
            //}
            //else
            //    return -1;
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ZhiFang.Model.Lab_CLIENTELE model)
		{

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update B_Lab_CLIENTELE set ");
            if (model.CNAME != null)
            {
                strSql.Append("CNAME='" + model.CNAME + "',");
            }
            else
            {
                strSql.Append("CNAME= null ,");
            }
            if (model.ENAME != null)
            {
                strSql.Append("ENAME='" + model.ENAME + "',");
            }
            else
            {
                strSql.Append("ENAME= null ,");
            }
            if (model.SHORTCODE != null)
            {
                strSql.Append("SHORTCODE='" + model.SHORTCODE + "',");
            }
            else
            {
                strSql.Append("SHORTCODE= null ,");
            }
            if (model.ISUSE != null)
            {
                strSql.Append("ISUSE=" + model.ISUSE + ",");
            }
            else
            {
                strSql.Append("ISUSE= null ,");
            }
            if (model.LINKMAN != null)
            {
                strSql.Append("LINKMAN='" + model.LINKMAN + "',");
            }
            else
            {
                strSql.Append("LINKMAN= null ,");
            }
            if (model.PHONENUM1 != null)
            {
                strSql.Append("PHONENUM1='" + model.PHONENUM1 + "',");
            }
            else
            {
                strSql.Append("PHONENUM1= null ,");
            }
            if (model.ADDRESS != null)
            {
                strSql.Append("ADDRESS='" + model.ADDRESS + "',");
            }
            else
            {
                strSql.Append("ADDRESS= null ,");
            }
            if (model.MAILNO != null)
            {
                strSql.Append("MAILNO='" + model.MAILNO + "',");
            }
            else
            {
                strSql.Append("MAILNO= null ,");
            }
            if (model.EMAIL != null)
            {
                strSql.Append("EMAIL='" + model.EMAIL + "',");
            }
            else
            {
                strSql.Append("EMAIL= null ,");
            }
            if (model.PRINCIPAL != null)
            {
                strSql.Append("PRINCIPAL='" + model.PRINCIPAL + "',");
            }
            else
            {
                strSql.Append("PRINCIPAL= null ,");
            }
            if (model.PHONENUM2 != null)
            {
                strSql.Append("PHONENUM2='" + model.PHONENUM2 + "',");
            }
            else
            {
                strSql.Append("PHONENUM2= null ,");
            }
            if (model.CLIENTTYPE != null)
            {
                strSql.Append("CLIENTTYPE=" + model.CLIENTTYPE + ",");
            }
            else
            {
                strSql.Append("CLIENTTYPE= null ,");
            }
            if (model.BmanNo != null)
            {
                strSql.Append("BmanNo=" + model.BmanNo + ",");
            }
            else
            {
                strSql.Append("BmanNo= null ,");
            }
            if (model.Romark != null)
            {
                strSql.Append("Romark='" + model.Romark + "',");
            }
            else
            {
                strSql.Append("Romark= null ,");
            }
            if (model.TitleType != null)
            {
                strSql.Append("TitleType=" + model.TitleType + ",");
            }
            else
            {
                strSql.Append("TitleType= null ,");
            }
            if (model.UploadType != null)
            {
                strSql.Append("UploadType=" + model.UploadType + ",");
            }
            else
            {
                strSql.Append("UploadType= null ,");
            }
            if (model.PrintType != null)
            {
                strSql.Append("PrintType=" + model.PrintType + ",");
            }
            else
            {
                strSql.Append("PrintType= null ,");
            }
            if (model.InputDataType != null)
            {
                strSql.Append("InputDataType=" + model.InputDataType + ",");
            }
            else
            {
                strSql.Append("InputDataType= null ,");
            }
            if (model.ReportPageType != null)
            {
                strSql.Append("ReportPageType=" + model.ReportPageType + ",");
            }
            else
            {
                strSql.Append("Reportpagetype= null ,");
            }
            if (model.ClientArea != null)
            {
                strSql.Append("ClientArea='" + model.ClientArea + "',");
            }
            else
            {
                strSql.Append("ClientArea= null ,");
            }
            if (model.ClientStyle != null)
            {
                strSql.Append("ClientStyle='" + model.ClientStyle + "',");
            }
            else
            {
                strSql.Append("ClientStyle= null ,");
            }
            if (model.RelationName != null)
            {
                strSql.Append("RelationName='" + model.RelationName + "',");
            }
            else
            {
                strSql.Append("RelationName= null ,");
            }
            if (model.WebLisSourceOrgID != null)
            {
                strSql.Append("WebLisSourceOrgID='" + model.WebLisSourceOrgID + "',");
            }
            else
            {
                strSql.Append("WebLisSourceOrgID= null ,");
            }
            if (model.GroupName != null)
            {
                strSql.Append("GroupName='" + model.GroupName + "',");
            }
            else
            {
                strSql.Append("GroupName= null ,");
            }
            if (model.AddTime != null)
            {
                strSql.Append("AddTime = to_date('" + model.AddTime.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");
            }
            if (model.StandCode != null)
            {
                strSql.Append("StandCode='" + model.StandCode + "',");
            }
            else
            {
                strSql.Append("StandCode= null ,");
            }
            if (model.ZFStandCode != null)
            {
                strSql.Append("ZFStandCode='" + model.ZFStandCode + "',");
            }
            else
            {
                strSql.Append("ZFStandCode= null ,");
            }
            if (model.UseFlag != null)
            {
                strSql.Append("UseFlag=" + model.UseFlag + ",");
            }
            else
            {
                strSql.Append("UseFlag= null ,");
            }
            strSql.Append("DTimeStampe = sysdate+ '1.1234',");
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where ClIENTID=" + model.ClIENTID + "");
            if (DbHelperSQL.ExecuteNonQuery(strSql.ToString()) > 0)
            {
                return d_log.OperateLog("CLIENTELE", "", "", DateTime.Now, 1);
            }
            else
                return -1;

            //StringBuilder strSql=new StringBuilder();
            //strSql.Append("update B_Lab_CLIENTELE set ");
			                                                
            //strSql.Append(" LabCode = @LabCode , ");                                    
            //strSql.Append(" LabClIENTNO = @LabClIENTNO , ");                                    
            //strSql.Append(" CNAME = @CNAME , ");                                    
            //strSql.Append(" ENAME = @ENAME , ");                                    
            //strSql.Append(" SHORTCODE = @SHORTCODE , ");                                    
            //strSql.Append(" ISUSE = @ISUSE , ");                                    
            //strSql.Append(" LINKMAN = @LINKMAN , ");                                    
            //strSql.Append(" PHONENUM1 = @PHONENUM1 , ");                                    
            //strSql.Append(" ADDRESS = @ADDRESS , ");                                    
            //strSql.Append(" MAILNO = @MAILNO , ");                                    
            //strSql.Append(" EMAIL = @EMAIL , ");                                    
            //strSql.Append(" PRINCIPAL = @PRINCIPAL , ");                                    
            //strSql.Append(" PHONENUM2 = @PHONENUM2 , ");                                    
            //strSql.Append(" CLIENTTYPE = @CLIENTTYPE , ");                                    
            //strSql.Append(" BmanNo = @BmanNo , ");                                    
            //strSql.Append(" Romark = @Romark , ");                                    
            //strSql.Append(" TitleType = @TitleType , ");                                    
            //strSql.Append(" UploadType = @UploadType , ");                                    
            //strSql.Append(" PrintType = @PrintType , ");                                    
            //strSql.Append(" InputDataType = @InputDataType , ");                                    
            //strSql.Append(" Reportpagetype = @Reportpagetype , ");                                    
            //strSql.Append(" ClientArea = @ClientArea , ");                                    
            //strSql.Append(" ClientStyle = @ClientStyle , ");                                    
            //strSql.Append(" RelationName = @RelationName , ");                                    
            //strSql.Append(" WebLisSourceOrgID = @WebLisSourceOrgID , ");                                    
            //strSql.Append(" GroupName = @GroupName , ");                                                                                    
            //strSql.Append(" StandCode = @StandCode , ");                                    
            //strSql.Append(" ZFStandCode = @ZFStandCode , ");                                    
            //strSql.Append(" UseFlag = @UseFlag  ");            			
            //strSql.Append(" where LabCode=@LabCode and LabClIENTNO=@LabClIENTNO  ");
						
            //SqlParameter[] parameters = {
			            	
                           
            //new SqlParameter("@LabCode", SqlDbType.VarChar,50) ,            	
                           
            //new SqlParameter("@LabClIENTNO", SqlDbType.Int,4) ,            	
                           
            //new SqlParameter("@CNAME", SqlDbType.VarChar,50) ,            	
                           
            //new SqlParameter("@ENAME", SqlDbType.VarChar,40) ,            	
                           
            //new SqlParameter("@SHORTCODE", SqlDbType.VarChar,40) ,            	
                           
            //new SqlParameter("@ISUSE", SqlDbType.Int,4) ,            	
                           
            //new SqlParameter("@LINKMAN", SqlDbType.VarChar,40) ,            	
                           
            //new SqlParameter("@PHONENUM1", SqlDbType.VarChar,40) ,            	
                           
            //new SqlParameter("@ADDRESS", SqlDbType.VarChar,40) ,            	
                           
            //new SqlParameter("@MAILNO", SqlDbType.VarChar,40) ,            	
                           
            //new SqlParameter("@EMAIL", SqlDbType.VarChar,40) ,            	
                           
            //new SqlParameter("@PRINCIPAL", SqlDbType.VarChar,40) ,            	
                           
            //new SqlParameter("@PHONENUM2", SqlDbType.VarChar,40) ,            	
                           
            //new SqlParameter("@CLIENTTYPE", SqlDbType.Int,4) ,            	
                           
            //new SqlParameter("@BmanNo", SqlDbType.Int,4) ,            	
                           
            //new SqlParameter("@Romark", SqlDbType.VarChar,200) ,            	
                           
            //new SqlParameter("@TitleType", SqlDbType.Int,4) ,            	
                           
            //new SqlParameter("@UploadType", SqlDbType.Int,4) ,            	
                           
            //new SqlParameter("@PrintType", SqlDbType.Int,4) ,            	
                           
            //new SqlParameter("@InputDataType", SqlDbType.Int,4) ,            	
                           
            //new SqlParameter("@Reportpagetype", SqlDbType.Int,4) ,            	
                           
            //new SqlParameter("@ClientArea", SqlDbType.VarChar,40) ,            	
                           
            //new SqlParameter("@ClientStyle", SqlDbType.VarChar,40) ,            	
                           
            //new SqlParameter("@RelationName", SqlDbType.VarChar,50) ,            	
                           
            //new SqlParameter("@WebLisSourceOrgID", SqlDbType.VarChar,10) ,            	
                           
            //new SqlParameter("@GroupName", SqlDbType.VarChar,50) ,            	
                        	
                        	
                           
            //new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,            	
                           
            //new SqlParameter("@ZFStandCode", SqlDbType.VarChar,50) ,            	
                           
            //new SqlParameter("@UseFlag", SqlDbType.Int,4)             	
              
            //};
            			    
				
                
			   
            //if(model.LabCode!=null)
            //{
            //    parameters[0].Value = model.LabCode;            	
            //}
            	
                
			   
            //if(model.LabClIENTNO!=null)
            //{
            //    parameters[1].Value = model.LabClIENTNO;            	
            //}
            	
                
			   
            //if(model.CNAME!=null)
            //{
            //    parameters[2].Value = model.CNAME;            	
            //}
            	
                
			   
            //if(model.ENAME!=null)
            //{
            //    parameters[3].Value = model.ENAME;            	
            //}
            	
                
			   
            //if(model.SHORTCODE!=null)
            //{
            //    parameters[4].Value = model.SHORTCODE;            	
            //}
            	
                
			   
            //if(model.ISUSE!=null)
            //{
            //    parameters[5].Value = model.ISUSE;            	
            //}
            	
                
			   
            //if(model.LINKMAN!=null)
            //{
            //    parameters[6].Value = model.LINKMAN;            	
            //}
            	
                
			   
            //if(model.PHONENUM1!=null)
            //{
            //    parameters[7].Value = model.PHONENUM1;            	
            //}
            	
                
			   
            //if(model.ADDRESS!=null)
            //{
            //    parameters[8].Value = model.ADDRESS;            	
            //}
            	
                
			   
            //if(model.MAILNO!=null)
            //{
            //    parameters[9].Value = model.MAILNO;            	
            //}
            	
                
			   
            //if(model.EMAIL!=null)
            //{
            //    parameters[10].Value = model.EMAIL;            	
            //}
            	
                
			   
            //if(model.PRINCIPAL!=null)
            //{
            //    parameters[11].Value = model.PRINCIPAL;            	
            //}
            	
                
			   
            //if(model.PHONENUM2!=null)
            //{
            //    parameters[12].Value = model.PHONENUM2;            	
            //}
            	
                
			   
            //if(model.CLIENTTYPE!=null)
            //{
            //    parameters[13].Value = model.CLIENTTYPE;            	
            //}
            	
                
			   
            //if(model.BmanNo!=null)
            //{
            //    parameters[14].Value = model.BmanNo;            	
            //}
            	
                
			   
            //if(model.Romark!=null)
            //{
            //    parameters[15].Value = model.Romark;            	
            //}
            	
                
			   
            //if(model.TitleType!=null)
            //{
            //    parameters[16].Value = model.TitleType;            	
            //}
            	
                
			   
            //if(model.UploadType!=null)
            //{
            //    parameters[17].Value = model.UploadType;            	
            //}
            	
                
			   
            //if(model.PrintType!=null)
            //{
            //    parameters[18].Value = model.PrintType;            	
            //}
            	
                
			   
            //if(model.InputDataType!=null)
            //{
            //    parameters[19].Value = model.InputDataType;            	
            //}
            	
                
			   
            //if(model.ReportPageType!=null)
            //{
            //    parameters[20].Value = model.ReportPageType;            	
            //}
            	
                
			   
            //if(model.ClientArea!=null)
            //{
            //    parameters[21].Value = model.ClientArea;            	
            //}
            	
                
			   
            //if(model.ClientStyle!=null)
            //{
            //    parameters[22].Value = model.ClientStyle;            	
            //}
            	
                
			   
            //if(model.RelationName!=null)
            //{
            //    parameters[23].Value = model.RelationName;            	
            //}
            	
                
			   
            //if(model.WebLisSourceOrgID!=null)
            //{
            //    parameters[24].Value = model.WebLisSourceOrgID;            	
            //}
            	
                
			   
            //if(model.GroupName!=null)
            //{
            //    parameters[25].Value = model.GroupName;            	
            //}
            	
                
				
                
				
                
			   
            //if(model.StandCode!=null)
            //{
            //    parameters[26].Value = model.StandCode;            	
            //}
            	
                
			   
            //if(model.ZFStandCode!=null)
            //{
            //    parameters[27].Value = model.ZFStandCode;            	
            //}
            	
                
			   
            //if(model.UseFlag!=null)
            //{
            //    parameters[28].Value = model.UseFlag;            	
            //}
            	
                        
            //if (DbHelperSQL.ExecuteNonQuery(strSql.ToString(),parameters) > 0)
            //{
            //    return d_log.OperateLog("CLIENTELE", "", "", DateTime.Now, 1);
            //}
            //else
            //    return -1;
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


			return DbHelperSQL.ExecuteNonQuery(strSql.ToString(),parameters);
			
		}
		
				/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteList(string ClIENTIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from B_Lab_CLIENTELE ");
			strSql.Append(" where ID in ("+ClIENTIDlist + ")  ");
			return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
			
		}
				
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZhiFang.Model.Lab_CLIENTELE GetModel(string LabCode,int LabClIENTNO)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ClIENTID, LabCode, LabClIENTNO, CNAME, ENAME, SHORTCODE, ISUSE, LINKMAN, PHONENUM1, ADDRESS, MAILNO, EMAIL, PRINCIPAL, PHONENUM2, CLIENTTYPE, BmanNo, Romark, TitleType, UploadType, PrintType, InputDataType, Reportpagetype, ClientArea, ClientStyle, RelationName, WebLisSourceOrgID, GroupName, AddTime, StandCode, ZFStandCode, UseFlag  ");			
			strSql.Append("  from B_Lab_CLIENTELE ");
			strSql.Append(" where LabCode=@LabCode and LabClIENTNO=@LabClIENTNO ");
						SqlParameter[] parameters = {
					new SqlParameter("@LabCode", SqlDbType.VarChar,50),
					new SqlParameter("@LabClIENTNO", SqlDbType.Int,4)};
			parameters[0].Value = LabCode;
			parameters[1].Value = LabClIENTNO;

			
			ZhiFang.Model.Lab_CLIENTELE model=new ZhiFang.Model.Lab_CLIENTELE();
			DataSet ds=DbHelperSQL.ExecuteDataSet(strSql.ToString(),parameters);
			
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
																																				
				if(ds.Tables[0].Rows[0]["Reportpagetype"].ToString()!="")
				{
					model.ReportPageType=int.Parse(ds.Tables[0].Rows[0]["Reportpagetype"].ToString());
				}
																																								
				model.ClientArea= ds.Tables[0].Rows[0]["ClientArea"].ToString();
																																				
				model.ClientStyle= ds.Tables[0].Rows[0]["ClientStyle"].ToString();
																																				
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
			return DbHelperSQL.ExecuteDataSet(strSql.ToString());
		}
		
		
		/// <summary>
		/// 根据实体获取DataSet
		/// </summary>
		public DataSet GetList(ZhiFang.Model.Lab_CLIENTELE model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM B_Lab_CLIENTELE where 1=1 ");
			                                                                
                        if(model.LabCode !=null)
                        {
                        strSql.Append(" and LabCode='"+model.LabCode+"' ");
                        }
                                                    
             
            if(model.LabClIENTNO !=0)
                        {
                        strSql.Append(" and LabClIENTNO="+model.LabClIENTNO+" ");
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
                                                    
                        if(model.ISUSE !=null)
                        {
                        strSql.Append(" and ISUSE="+model.ISUSE+" ");
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
                        strSql.Append(" and CLIENTTYPE="+model.CLIENTTYPE+" ");
                        }
                                                    
                        if(model.BmanNo !=null)
                        {
                        strSql.Append(" and BmanNo="+model.BmanNo+" ");
                        }
                                                    
                        if(model.Romark !=null)
                        {
                        strSql.Append(" and Romark='"+model.Romark+"' ");
                        }
                                                    
                        if(model.TitleType !=null)
                        {
                        strSql.Append(" and TitleType="+model.TitleType+" ");
                        }
                                                    
                        if(model.UploadType !=null)
                        {
                        strSql.Append(" and UploadType="+model.UploadType+" ");
                        }
                                                    
                        if(model.PrintType !=null)
                        {
                        strSql.Append(" and PrintType="+model.PrintType+" ");
                        }
                                                    
                        if(model.InputDataType !=null)
                        {
                        strSql.Append(" and InputDataType="+model.InputDataType+" ");
                        }
                                                    
                        if(model.ReportPageType !=null)
                        {
                        strSql.Append(" and Reportpagetype="+model.ReportPageType+" ");
                        }
                                                    
                        if(model.ClientArea !=null)
                        {
                        strSql.Append(" and ClientArea='"+model.ClientArea+"' ");
                        }
                                                    
                        if(model.ClientStyle !=null)
                        {
                        strSql.Append(" and ClientStyle='"+model.ClientStyle+"' ");
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
                        Common.Log.Log.Info("B_Lab_CLIENTELE,GetList:" + strSql.ToString());
                                                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
		}
		public DataSet GetListByLike(ZhiFang.Model.Lab_CLIENTELE model)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select  ClIENTIDAndName.*,concat(concat('(',CONCAT(ClIENTID,')')),cname) as ClIENTIDAndName  ");
			strSql.Append(" FROM B_Lab_CLIENTELE where 1=1 ");
			if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }
			                                                                  
            if(model.LabClIENTNO !=null)
            {
                        strSql.Append(" or LabClIENTNO like '%"+model.LabClIENTNO+"%' ");
                        }
                                          
            if(model.CNAME !=null)
            {
                        strSql.Append(" or CNAME like '%"+model.CNAME+"%' ");
                        }
                                          
            if(model.ENAME !=null)
            {
                        strSql.Append(" or ENAME like '%"+model.ENAME+"%' ");
                        }
                                          
            if(model.SHORTCODE !=null)
            {
                        strSql.Append(" or SHORTCODE like '%"+model.SHORTCODE+"%' ");
                        }
                                          
            if(model.ISUSE !=null)
            {
                        strSql.Append(" or ISUSE like '%"+model.ISUSE+"%' ");
                        }
                                          
            if(model.LINKMAN !=null)
            {
                        strSql.Append(" or LINKMAN like '%"+model.LINKMAN+"%' ");
                        }
                                          
            if(model.PHONENUM1 !=null)
            {
                        strSql.Append(" or PHONENUM1 like '%"+model.PHONENUM1+"%' ");
                        }
                                          
            if(model.ADDRESS !=null)
            {
                        strSql.Append(" or ADDRESS like '%"+model.ADDRESS+"%' ");
                        }
                                          
            if(model.MAILNO !=null)
            {
                        strSql.Append(" or MAILNO like '%"+model.MAILNO+"%' ");
                        }
                                          
            if(model.EMAIL !=null)
            {
                        strSql.Append(" or EMAIL like '%"+model.EMAIL+"%' ");
                        }
                                          
            if(model.PRINCIPAL !=null)
            {
                        strSql.Append(" or PRINCIPAL like '%"+model.PRINCIPAL+"%' ");
                        }
                                          
            if(model.PHONENUM2 !=null)
            {
                        strSql.Append(" or PHONENUM2 like '%"+model.PHONENUM2+"%' ");
                        }
                                          
            if(model.CLIENTTYPE !=null)
            {
                        strSql.Append(" or CLIENTTYPE like '%"+model.CLIENTTYPE+"%' ");
                        }
                                          
            if(model.BmanNo !=null)
            {
                        strSql.Append(" or BmanNo like '%"+model.BmanNo+"%' ");
                        }
                                          
            if(model.Romark !=null)
            {
                        strSql.Append(" or Romark like '%"+model.Romark+"%' ");
                        }
                                          
            if(model.TitleType !=null)
            {
                        strSql.Append(" or TitleType like '%"+model.TitleType+"%' ");
                        }
                                          
            if(model.UploadType !=null)
            {
                        strSql.Append(" or UploadType like '%"+model.UploadType+"%' ");
                        }
                                          
            if(model.PrintType !=null)
            {
                        strSql.Append(" or PrintType like '%"+model.PrintType+"%' ");
                        }
                                          
            if(model.InputDataType !=null)
            {
                        strSql.Append(" or InputDataType like '%"+model.InputDataType+"%' ");
                        }
                                          
            if(model.ReportPageType !=null)
            {
                        strSql.Append(" or Reportpagetype like '%"+model.ReportPageType+"%' ");
                        }
                                          
            if(model.ClientArea !=null)
            {
                        strSql.Append(" or ClientArea like '%"+model.ClientArea+"%' ");
                        }
                                          
            if(model.ClientStyle !=null)
            {
                        strSql.Append(" or ClientStyle like '%"+model.ClientStyle+"%' ");
                        }
                                          
            if(model.RelationName !=null)
            {
                        strSql.Append(" or RelationName like '%"+model.RelationName+"%' ");
                        }
                                          
            if(model.WebLisSourceOrgID !=null)
            {
                        strSql.Append(" or WebLisSourceOrgID like '%"+model.WebLisSourceOrgID+"%' ");
                        }
                                          
            if(model.GroupName !=null)
            {
                        strSql.Append(" or GroupName like '%"+model.GroupName+"%' ");
                        }
                                                                  
            if(model.AddTime !=null)
            {
                        strSql.Append(" or AddTime like '%"+model.AddTime+"%' ");
                        }
                                          
            if(model.StandCode !=null)
            {
                        strSql.Append(" or StandCode like '%"+model.StandCode+"%' ");
                        }
                                          
            if(model.ZFStandCode !=null)
            {
                        strSql.Append(" or ZFStandCode like '%"+model.ZFStandCode+"%' ");
                        }
                                          
            if(model.UseFlag !=null)
            {
                        strSql.Append(" or UseFlag like '%"+model.UseFlag+"%' ");
                        }
                                    return DbHelperSQL.ExecuteDataSet(strSql.ToString());
		}
		
		/// <summary>
		/// 获取总记录
		/// </summary>
		public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_Lab_CLIENTELE ");
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
        public int GetTotalCount(ZhiFang.Model.Lab_CLIENTELE model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_Lab_CLIENTELE where 1=1 ");
           	                                                  
                        
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
                                        
                        
            if(model.ISUSE !=null)
            {            
            	strSql.Append(" and ISUSE='"+model.ISUSE+"' ");
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
                                        
                        
            if(model.UploadType !=null)
            {            
            	strSql.Append(" and UploadType='"+model.UploadType+"' ");
            }
                                        
                        
            if(model.PrintType !=null)
            {            
            	strSql.Append(" and PrintType='"+model.PrintType+"' ");
            }
                                        
                        
            if(model.InputDataType !=null)
            {            
            	strSql.Append(" and InputDataType='"+model.InputDataType+"' ");
            }
                                        
                        
            if(model.ReportPageType !=null)
            {            
            	strSql.Append(" and Reportpagetype='"+model.ReportPageType+"' ");
            }
                                        
                        
            if(model.ClientArea !=null)
            {            
            	strSql.Append(" and ClientArea='"+model.ClientArea+"' ");
            }
                                        
                        
            if(model.ClientStyle !=null)
            {            
            	strSql.Append(" and ClientStyle='"+model.ClientStyle+"' ");
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
        public DataSet GetListByPage(ZhiFang.Model.Lab_CLIENTELE model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  * from B_Lab_CLIENTELE where 1=1  ");
                                                                              
                        if(model.LabCode !=null)
                        {
                        strSql.Append(" and LabCode='"+model.LabCode+"' ");
                        }
                                                      
             
            if(model.LabClIENTNO !=0)
                        {
                        strSql.Append(" and LabClIENTNO="+model.LabClIENTNO+" ");
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
                                                      
                        if(model.ISUSE !=null)
                        {
                        strSql.Append(" and ISUSE="+model.ISUSE+" ");
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
                        strSql.Append(" and CLIENTTYPE="+model.CLIENTTYPE+" ");
                        }
                                                      
                        if(model.BmanNo !=null)
                        {
                        strSql.Append(" and BmanNo="+model.BmanNo+" ");
                        }
                                                      
                        if(model.Romark !=null)
                        {
                        strSql.Append(" and Romark='"+model.Romark+"' ");
                        }
                                                      
                        if(model.TitleType !=null)
                        {
                        strSql.Append(" and TitleType="+model.TitleType+" ");
                        }
                                                      
                        if(model.UploadType !=null)
                        {
                        strSql.Append(" and UploadType="+model.UploadType+" ");
                        }
                                                      
                        if(model.PrintType !=null)
                        {
                        strSql.Append(" and PrintType="+model.PrintType+" ");
                        }
                                                      
                        if(model.InputDataType !=null)
                        {
                        strSql.Append(" and InputDataType="+model.InputDataType+" ");
                        }
                                                      
                        if(model.ReportPageType !=null)
                        {
                        strSql.Append(" and Reportpagetype="+model.ReportPageType+" ");
                        }
                                                      
                        if(model.ClientArea !=null)
                        {
                        strSql.Append(" and ClientArea='"+model.ClientArea+"' ");
                        }
                                                      
                        if(model.ClientStyle !=null)
                        {
                        strSql.Append(" and ClientStyle='"+model.ClientStyle+"' ");
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
                        strSql.Append("and  ROWNUM <= '" + nowPageSize + "' and ClIENTID not in ");
                        strSql.Append("(select  ClIENTID from B_Lab_CLIENTELE where 1=1 and ROWNUM <= '" + (nowPageSize * nowPageNum) + "' ");
                                                                              
                        if(model.LabCode !=null)
                        {
                        strSql.Append(" and LabCode='"+model.LabCode+"' ");
                        }
                                                      
             
            if(model.LabClIENTNO !=0)
                        {
                        strSql.Append(" and LabClIENTNO="+model.LabClIENTNO+" ");
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
                                                      
                        if(model.ISUSE !=null)
                        {
                        strSql.Append(" and ISUSE="+model.ISUSE+" ");
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
                        strSql.Append(" and CLIENTTYPE="+model.CLIENTTYPE+" ");
                        }
                                                      
                        if(model.BmanNo !=null)
                        {
                        strSql.Append(" and BmanNo="+model.BmanNo+" ");
                        }
                                                      
                        if(model.Romark !=null)
                        {
                        strSql.Append(" and Romark='"+model.Romark+"' ");
                        }
                                                      
                        if(model.TitleType !=null)
                        {
                        strSql.Append(" and TitleType="+model.TitleType+" ");
                        }
                                                      
                        if(model.UploadType !=null)
                        {
                        strSql.Append(" and UploadType="+model.UploadType+" ");
                        }
                                                      
                        if(model.PrintType !=null)
                        {
                        strSql.Append(" and PrintType="+model.PrintType+" ");
                        }
                                                      
                        if(model.InputDataType !=null)
                        {
                        strSql.Append(" and InputDataType="+model.InputDataType+" ");
                        }
                                                      
                        if(model.ReportPageType !=null)
                        {
                        strSql.Append(" and Reportpagetype="+model.ReportPageType+" ");
                        }
                                                      
                        if(model.ClientArea !=null)
                        {
                        strSql.Append(" and ClientArea='"+model.ClientArea+"' ");
                        }
                                                      
                        if(model.ClientStyle !=null)
                        {
                        strSql.Append(" and ClientStyle='"+model.ClientStyle+"' ");
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
                                                strSql.Append(" ) order by ClIENTID  ");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
        
        public bool Exists(string LabCode,int LabClIENTNO)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from B_Lab_CLIENTELE ");
			strSql.Append(" where LabCode='"+LabCode+"' and LabClIENTNO="+LabClIENTNO+" ");
            //            SqlParameter[] parameters = {
            //        new SqlParameter("@LabCode", SqlDbType.VarChar,50),
            //        new SqlParameter("@LabClIENTNO", SqlDbType.Int,4)};
            //parameters[0].Value = LabCode;
            //parameters[1].Value = LabClIENTNO;


			DataSet ds=DbHelperSQL.ExecuteDataSet(strSql.ToString());
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
                string strCount = DbHelperSQL.ExecuteScalar(strSql.ToString());
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
            return DbHelperSQL.GetMaxID("LabCode,LabClIENTNO","B_Lab_CLIENTELE");
        }

        public DataSet GetList(int Top, ZhiFang.Model.Lab_CLIENTELE model, string filedOrder)
        {
            StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			
			strSql.Append(" * ");
			strSql.Append(" FROM B_Lab_CLIENTELE ");

            strSql.Append(" where 1=1 ");                                                       
                        
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
                                            
                        
            if(model.ISUSE !=null)
            {            
            	strSql.Append(" and ISUSE='"+model.ISUSE+"' ");
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
                                            
                        
            if(model.UploadType !=null)
            {            
            	strSql.Append(" and UploadType='"+model.UploadType+"' ");
            }
                                            
                        
            if(model.PrintType !=null)
            {            
            	strSql.Append(" and PrintType='"+model.PrintType+"' ");
            }
                                            
                        
            if(model.InputDataType !=null)
            {            
            	strSql.Append(" and InputDataType='"+model.InputDataType+"' ");
            }
                                            
                        
            if(model.ReportPageType !=null)
            {            
            	strSql.Append(" and Reportpagetype='"+model.ReportPageType+"' ");
            }
                                            
                        
            if(model.ClientArea !=null)
            {            
            	strSql.Append(" and ClientArea='"+model.ClientArea+"' ");
            }
                                            
                        
            if(model.ClientStyle !=null)
            {            
            	strSql.Append(" and ClientStyle='"+model.ClientStyle+"' ");
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
            strSql.Append(" and ROWNUM <= '" + Top + "'");                          
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
                        if (this.Exists(ds.Tables[0].Rows[i]["LabCode"].ToString().Trim(), int.Parse(ds.Tables[0].Rows[i]["LabClIENTNO"].ToString().Trim())))
                        {
                            System.Threading.Thread.Sleep(ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ThreadDicSynchInterval"));
                            count += this.UpdateByDataRow(dr);
                        }
                        else
                        {
                            System.Threading.Thread.Sleep(ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ThreadDicSynchInterval"));
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
				strSql.Append("insert into B_Lab_CLIENTELE (");			
            	strSql.Append("LabCode,LabClIENTNO,CNAME,ENAME,SHORTCODE,ISUSE,LINKMAN,PHONENUM1,ADDRESS,MAILNO,EMAIL,PRINCIPAL,PHONENUM2,CLIENTTYPE,BmanNo,Romark,TitleType,UploadType,PrintType,InputDataType,Reportpagetype,ClientArea,ClientStyle,RelationName,WebLisSourceOrgID,GroupName,StandCode,ZFStandCode,UseFlag");
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
            	             	            	
            	if(dr.Table.Columns["Reportpagetype"]!=null && dr.Table.Columns["Reportpagetype"].ToString().Trim()!="")
            	{
            		strSql.Append(" '"+dr["Reportpagetype"].ToString().Trim()+"', ");
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
                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch(Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.B_Lab_CLIENTELE.AddByDataRow 同步数据时异常：", ex);
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
			      			       
			    			    
			    if( dr.Table.Columns["Reportpagetype"]!=null && dr.Table.Columns["Reportpagetype"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" Reportpagetype = '"+dr["Reportpagetype"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["ClientArea"]!=null && dr.Table.Columns["ClientArea"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" ClientArea = '"+dr["ClientArea"].ToString().Trim()+"' , ");
			    }
			      			       
			    			    
			    if( dr.Table.Columns["ClientStyle"]!=null && dr.Table.Columns["ClientStyle"].ToString().Trim()!="" )
			    {
			    	strSql.Append(" ClientStyle = '"+dr["ClientStyle"].ToString().Trim()+"' , ");
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
				strSql.Append(" where LabCode='" + dr["LabCode"].ToString().Trim() + "' and LabClIENTNO='"+dr["LabClIENTNO"].ToString().Trim()+"' ");
						
                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch(Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.B_Lab_CLIENTELE .UpdateByDataRow同步数据时异常：", ex);
                return 0;
            }
        }
		
   
	}
}

