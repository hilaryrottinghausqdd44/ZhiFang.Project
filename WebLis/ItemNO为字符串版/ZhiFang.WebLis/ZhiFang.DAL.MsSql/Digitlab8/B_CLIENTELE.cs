using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.MsSql.Digitlab8
{
    //B_CLIENTELE

    public partial class B_CLIENTELE : IDCLIENTELE, IDBatchCopy, IDGetListByTimeStampe
    {
        DBUtility.IDBConnection idb;
        public B_CLIENTELE(string dbsourceconn)
        {
            idb = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public B_CLIENTELE()
        {
            idb = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.LisDB());
        }
        D_LogInfo d_log = new D_LogInfo();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.CLIENTELE model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into B_CLIENTELE(");
            strSql.Append("ClIENTNO,CNAME,ENAME,SHORTCODE,ISUSE,LINKMAN,PHONENUM1,ADDRESS,MAILNO,EMAIL,PRINCIPAL,PHONENUM2,CLIENTTYPE,BmanNo,Romark,TitleType,UploadType,PrintType,InputDataType,ReportPageType,ClientArea,ClientStyle,FaxNo,AutoFax,ClientReportTitle,IsPrintItem,CZDY1,CZDY2,CZDY3,CZDY4,CZDY5,CZDY6,LinkManPosition,LinkMan1,LinkManPosition1,ClientCode,CWAccountDate,NFClientType,RelationName,WebLisSourceOrgID,GroupName,StandCode,ZFStandCode,UseFlag");
            strSql.Append(") values (");
            strSql.Append("@ClIENTNO,@CNAME,@ENAME,@SHORTCODE,@ISUSE,@LINKMAN,@PHONENUM1,@ADDRESS,@MAILNO,@EMAIL,@PRINCIPAL,@PHONENUM2,@CLIENTTYPE,@BmanNo,@Romark,@TitleType,@UploadType,@PrintType,@InputDataType,@ReportPageType,@ClientArea,@ClientStyle,@FaxNo,@AutoFax,@ClientReportTitle,@IsPrintItem,@CZDY1,@CZDY2,@CZDY3,@CZDY4,@CZDY5,@CZDY6,@LinkManPosition,@LinkMan1,@LinkManPosition1,@ClientCode,@CWAccountDate,@NFClientType,@RelationName,@WebLisSourceOrgID,@GroupName,@StandCode,@ZFStandCode,@UseFlag");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ClIENTNO", SqlDbType.BigInt) ,            
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

            parameters[0].Value = model.ClIENTNO;
            parameters[1].Value = model.CNAME;
            parameters[2].Value = model.ENAME;
            parameters[3].Value = model.SHORTCODE;
            parameters[4].Value = model.ISUSE;
            parameters[5].Value = model.LINKMAN;
            parameters[6].Value = model.PHONENUM1;
            parameters[7].Value = model.ADDRESS;
            parameters[8].Value = model.MAILNO;
            parameters[9].Value = model.EMAIL;
            parameters[10].Value = model.PRINCIPAL;
            parameters[11].Value = model.PHONENUM2;
            parameters[12].Value = model.CLIENTTYPE;
            parameters[13].Value = model.bmanno;
            parameters[14].Value = model.romark;
            parameters[15].Value = model.titletype;
            parameters[16].Value = model.uploadtype;
            parameters[17].Value = model.printtype;
            parameters[18].Value = model.InputDataType;
            parameters[19].Value = model.reportpagetype;
            parameters[20].Value = model.clientarea;
            parameters[21].Value = model.clientstyle;
            parameters[22].Value = model.FaxNo;
            parameters[23].Value = model.AutoFax;
            parameters[24].Value = model.ClientReportTitle;
            parameters[25].Value = model.IsPrintItem;
            parameters[26].Value = model.CZDY1;
            parameters[27].Value = model.CZDY2;
            parameters[28].Value = model.CZDY3;
            parameters[29].Value = model.CZDY4;
            parameters[30].Value = model.CZDY5;
            parameters[31].Value = model.CZDY6;
            parameters[32].Value = model.LinkManPosition;
            parameters[33].Value = model.LinkMan1;
            parameters[34].Value = model.LinkManPosition1;
            parameters[35].Value = model.clientcode;
            parameters[36].Value = model.CWAccountDate;
            parameters[37].Value = model.NFClientType;
            parameters[38].Value = model.RelationName;
            parameters[39].Value = model.WebLisSourceOrgId;
            parameters[40].Value = model.GroupName;
            parameters[41].Value = model.StandCode;
            parameters[42].Value = model.ZFStandCode;
            parameters[43].Value = model.UseFlag;
            if (idb.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
            {
                return d_log.OperateLog("CLIENTELE", "", "", DateTime.Now, 1);
            }
            else
                return -1;
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.CLIENTELE model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update B_CLIENTELE set ");

            strSql.Append(" ClIENTNO = @ClIENTNO , ");
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
            strSql.Append(" where ClIENTNO=@ClIENTNO  ");

            SqlParameter[] parameters = {
			            	
                           
            new SqlParameter("@ClIENTNO", SqlDbType.BigInt,4) ,            	
                           
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




            if (model.ClIENTNO != null)
            {
                parameters[0].Value = model.ClIENTNO;
            }



            if (model.CNAME != null)
            {
                parameters[1].Value = model.CNAME;
            }



            if (model.ENAME != null)
            {
                parameters[2].Value = model.ENAME;
            }



            if (model.SHORTCODE != null)
            {
                parameters[3].Value = model.SHORTCODE;
            }



            if (model.ISUSE != null)
            {
                parameters[4].Value = model.ISUSE;
            }



            if (model.LINKMAN != null)
            {
                parameters[5].Value = model.LINKMAN;
            }



            if (model.PHONENUM1 != null)
            {
                parameters[6].Value = model.PHONENUM1;
            }



            if (model.ADDRESS != null)
            {
                parameters[7].Value = model.ADDRESS;
            }



            if (model.MAILNO != null)
            {
                parameters[8].Value = model.MAILNO;
            }



            if (model.EMAIL != null)
            {
                parameters[9].Value = model.EMAIL;
            }



            if (model.PRINCIPAL != null)
            {
                parameters[10].Value = model.PRINCIPAL;
            }



            if (model.PHONENUM2 != null)
            {
                parameters[11].Value = model.PHONENUM2;
            }



            if (model.CLIENTTYPE != null)
            {
                parameters[12].Value = model.CLIENTTYPE;
            }



            if (model.bmanno != null)
            {
                parameters[13].Value = model.bmanno;
            }



            if (model.romark != null)
            {
                parameters[14].Value = model.romark;
            }



            if (model.titletype != null)
            {
                parameters[15].Value = model.titletype;
            }



            if (model.uploadtype != null)
            {
                parameters[16].Value = model.uploadtype;
            }



            if (model.printtype != null)
            {
                parameters[17].Value = model.printtype;
            }



            if (model.InputDataType != null)
            {
                parameters[18].Value = model.InputDataType;
            }



            if (model.reportpagetype != null)
            {
                parameters[19].Value = model.reportpagetype;
            }



            if (model.clientarea != null)
            {
                parameters[20].Value = model.clientarea;
            }



            if (model.clientstyle != null)
            {
                parameters[21].Value = model.clientstyle;
            }



            if (model.FaxNo != null)
            {
                parameters[22].Value = model.FaxNo;
            }



            if (model.AutoFax != null)
            {
                parameters[23].Value = model.AutoFax;
            }



            if (model.ClientReportTitle != null)
            {
                parameters[24].Value = model.ClientReportTitle;
            }



            if (model.IsPrintItem != null)
            {
                parameters[25].Value = model.IsPrintItem;
            }



            if (model.CZDY1 != null)
            {
                parameters[26].Value = model.CZDY1;
            }



            if (model.CZDY2 != null)
            {
                parameters[27].Value = model.CZDY2;
            }



            if (model.CZDY3 != null)
            {
                parameters[28].Value = model.CZDY3;
            }



            if (model.CZDY4 != null)
            {
                parameters[29].Value = model.CZDY4;
            }



            if (model.CZDY5 != null)
            {
                parameters[30].Value = model.CZDY5;
            }



            if (model.CZDY6 != null)
            {
                parameters[31].Value = model.CZDY6;
            }



            if (model.LinkManPosition != null)
            {
                parameters[32].Value = model.LinkManPosition;
            }



            if (model.LinkMan1 != null)
            {
                parameters[33].Value = model.LinkMan1;
            }



            if (model.LinkManPosition1 != null)
            {
                parameters[34].Value = model.LinkManPosition1;
            }



            if (model.clientcode != null)
            {
                parameters[35].Value = model.clientcode;
            }



            if (model.CWAccountDate != null)
            {
                parameters[36].Value = model.CWAccountDate;
            }



            if (model.NFClientType != null)
            {
                parameters[37].Value = model.NFClientType;
            }



            if (model.RelationName != null)
            {
                parameters[38].Value = model.RelationName;
            }



            if (model.WebLisSourceOrgId != null)
            {
                parameters[39].Value = model.WebLisSourceOrgId;
            }



            if (model.GroupName != null)
            {
                parameters[40].Value = model.GroupName;
            }







            if (model.StandCode != null)
            {
                parameters[41].Value = model.StandCode;
            }



            if (model.ZFStandCode != null)
            {
                parameters[42].Value = model.ZFStandCode;
            }



            if (model.UseFlag != null)
            {
                parameters[43].Value = model.UseFlag;
            }


            if (idb.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
            {
                return d_log.OperateLog("CLIENTELE", "", "", DateTime.Now, 1);
            }
            else
                return -1;
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(long ClIENTNO)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from B_CLIENTELE ");
            strSql.Append(" where ClIENTNO=@ClIENTNO ");
            SqlParameter[] parameters = {
					new SqlParameter("@ClIENTNO", SqlDbType.BigInt)};
            parameters[0].Value = ClIENTNO;


            return idb.ExecuteNonQuery(strSql.ToString(), parameters);

        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int DeleteList(string ClIENTIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from B_CLIENTELE ");
            strSql.Append(" where ID in (" + ClIENTIDlist + ")  ");
            return idb.ExecuteNonQuery(strSql.ToString());

        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.CLIENTELE GetModel(long ClIENTNO)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ClIENTID, ClIENTNO, CNAME, ENAME, SHORTCODE, ISUSE, LINKMAN, PHONENUM1, ADDRESS, MAILNO, EMAIL, PRINCIPAL, PHONENUM2, CLIENTTYPE, BmanNo, Romark, TitleType, UploadType, PrintType, InputDataType, ReportPageType, ClientArea, ClientStyle, FaxNo, AutoFax, ClientReportTitle, IsPrintItem, CZDY1, CZDY2, CZDY3, CZDY4, CZDY5, CZDY6, LinkManPosition, LinkMan1, LinkManPosition1, ClientCode, CWAccountDate, NFClientType, RelationName, WebLisSourceOrgID, GroupName, AddTime, StandCode, ZFStandCode, UseFlag  ");
            strSql.Append("  from B_CLIENTELE ");
            strSql.Append(" where ClIENTNO=@ClIENTNO ");
            SqlParameter[] parameters = {
					new SqlParameter("@ClIENTNO", SqlDbType.BigInt)};
            parameters[0].Value = ClIENTNO;


            ZhiFang.Model.CLIENTELE model = new ZhiFang.Model.CLIENTELE();
            DataSet ds = idb.ExecuteDataSet(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {

                if (ds.Tables[0].Rows[0]["ClIENTID"].ToString() != "")
                {
                    model.CLIENTID = int.Parse(ds.Tables[0].Rows[0]["ClIENTID"].ToString());
                }

                if (ds.Tables[0].Rows[0]["ClIENTNO"].ToString() != "")
                {
                    model.ClIENTNO = ds.Tables[0].Rows[0]["ClIENTNO"].ToString();
                }

                model.CNAME = ds.Tables[0].Rows[0]["CNAME"].ToString();

                model.ENAME = ds.Tables[0].Rows[0]["ENAME"].ToString();

                model.SHORTCODE = ds.Tables[0].Rows[0]["SHORTCODE"].ToString();

                if (ds.Tables[0].Rows[0]["ISUSE"].ToString() != "")
                {
                    model.ISUSE = int.Parse(ds.Tables[0].Rows[0]["ISUSE"].ToString());
                }

                model.LINKMAN = ds.Tables[0].Rows[0]["LINKMAN"].ToString();

                model.PHONENUM1 = ds.Tables[0].Rows[0]["PHONENUM1"].ToString();

                model.ADDRESS = ds.Tables[0].Rows[0]["ADDRESS"].ToString();

                model.MAILNO = ds.Tables[0].Rows[0]["MAILNO"].ToString();

                model.EMAIL = ds.Tables[0].Rows[0]["EMAIL"].ToString();

                model.PRINCIPAL = ds.Tables[0].Rows[0]["PRINCIPAL"].ToString();

                model.PHONENUM2 = ds.Tables[0].Rows[0]["PHONENUM2"].ToString();

                if (ds.Tables[0].Rows[0]["CLIENTTYPE"].ToString() != "")
                {
                    model.CLIENTTYPE = int.Parse(ds.Tables[0].Rows[0]["CLIENTTYPE"].ToString());
                }

                if (ds.Tables[0].Rows[0]["BmanNo"].ToString() != "")
                {
                    model.bmanno = int.Parse(ds.Tables[0].Rows[0]["BmanNo"].ToString());
                }

                model.romark = ds.Tables[0].Rows[0]["Romark"].ToString();

                if (ds.Tables[0].Rows[0]["TitleType"].ToString() != "")
                {
                    model.titletype = int.Parse(ds.Tables[0].Rows[0]["TitleType"].ToString());
                }

                if (ds.Tables[0].Rows[0]["UploadType"].ToString() != "")
                {
                    model.uploadtype = int.Parse(ds.Tables[0].Rows[0]["UploadType"].ToString());
                }

                if (ds.Tables[0].Rows[0]["PrintType"].ToString() != "")
                {
                    model.printtype = int.Parse(ds.Tables[0].Rows[0]["PrintType"].ToString());
                }

                if (ds.Tables[0].Rows[0]["InputDataType"].ToString() != "")
                {
                    model.InputDataType = int.Parse(ds.Tables[0].Rows[0]["InputDataType"].ToString());
                }

                if (ds.Tables[0].Rows[0]["ReportPageType"].ToString() != "")
                {
                    model.reportpagetype = int.Parse(ds.Tables[0].Rows[0]["ReportPageType"].ToString());
                }

                model.clientarea = ds.Tables[0].Rows[0]["ClientArea"].ToString();

                model.clientstyle = ds.Tables[0].Rows[0]["ClientStyle"].ToString();

                model.FaxNo = ds.Tables[0].Rows[0]["FaxNo"].ToString();

                if (ds.Tables[0].Rows[0]["AutoFax"].ToString() != "")
                {
                    model.AutoFax = int.Parse(ds.Tables[0].Rows[0]["AutoFax"].ToString());
                }

                model.ClientReportTitle = ds.Tables[0].Rows[0]["ClientReportTitle"].ToString();

                if (ds.Tables[0].Rows[0]["IsPrintItem"].ToString() != "")
                {
                    model.IsPrintItem = int.Parse(ds.Tables[0].Rows[0]["IsPrintItem"].ToString());
                }

                model.CZDY1 = ds.Tables[0].Rows[0]["CZDY1"].ToString();

                model.CZDY2 = ds.Tables[0].Rows[0]["CZDY2"].ToString();

                model.CZDY3 = ds.Tables[0].Rows[0]["CZDY3"].ToString();

                model.CZDY4 = ds.Tables[0].Rows[0]["CZDY4"].ToString();

                model.CZDY5 = ds.Tables[0].Rows[0]["CZDY5"].ToString();

                model.CZDY6 = ds.Tables[0].Rows[0]["CZDY6"].ToString();

                model.LinkManPosition = ds.Tables[0].Rows[0]["LinkManPosition"].ToString();

                model.LinkMan1 = ds.Tables[0].Rows[0]["LinkMan1"].ToString();

                model.LinkManPosition1 = ds.Tables[0].Rows[0]["LinkManPosition1"].ToString();

                model.clientcode = ds.Tables[0].Rows[0]["ClientCode"].ToString();

                model.CWAccountDate = ds.Tables[0].Rows[0]["CWAccountDate"].ToString();

                model.NFClientType = ds.Tables[0].Rows[0]["NFClientType"].ToString();

                model.RelationName = ds.Tables[0].Rows[0]["RelationName"].ToString();

                model.WebLisSourceOrgId = ds.Tables[0].Rows[0]["WebLisSourceOrgID"].ToString();

                model.GroupName = ds.Tables[0].Rows[0]["GroupName"].ToString();

                if (ds.Tables[0].Rows[0]["AddTime"].ToString() != "")
                {
                    model.AddTime = DateTime.Parse(ds.Tables[0].Rows[0]["AddTime"].ToString());
                }

                model.StandCode = ds.Tables[0].Rows[0]["StandCode"].ToString();

                model.ZFStandCode = ds.Tables[0].Rows[0]["ZFStandCode"].ToString();

                if (ds.Tables[0].Rows[0]["UseFlag"].ToString() != "")
                {
                    model.UseFlag = int.Parse(ds.Tables[0].Rows[0]["UseFlag"].ToString());
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *,ClIENTNO as LabNo ");
            strSql.Append(" FROM B_CLIENTELE ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return idb.ExecuteDataSet(strSql.ToString());
        }


        /// <summary>
        /// 根据实体获取DataSet
        /// </summary>
        public DataSet GetList(ZhiFang.Model.CLIENTELE model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM B_CLIENTELE where 1=1 ");



            if (model.ClIENTNO != "" && model.ClIENTNO != null)
            {

                strSql.Append(" and ClIENTNO=" + model.ClIENTNO + " ");
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

            if (model.ISUSE != null)
            {

                strSql.Append(" and ISUSE=" + model.ISUSE + " ");
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

            if (model.bmanno != null)
            {

                strSql.Append(" and BmanNo=" + model.bmanno + " ");
            }

            if (model.romark != null)
            {

                strSql.Append(" and Romark='" + model.romark + "' ");
            }

            if (model.titletype != null)
            {

                strSql.Append(" and TitleType=" + model.titletype + " ");
            }

            if (model.uploadtype != null)
            {

                strSql.Append(" and UploadType=" + model.uploadtype + " ");
            }

            if (model.printtype != null)
            {

                strSql.Append(" and PrintType=" + model.printtype + " ");
            }

            if (model.InputDataType != null)
            {

                strSql.Append(" and InputDataType=" + model.InputDataType + " ");
            }

            if (model.reportpagetype != null)
            {

                strSql.Append(" and ReportPageType=" + model.reportpagetype + " ");
            }

            if (model.clientarea != null)
            {

                strSql.Append(" and ClientArea='" + model.clientarea + "' ");
            }

            if (model.clientstyle != null)
            {

                strSql.Append(" and ClientStyle='" + model.clientstyle + "' ");
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

            if (model.IsPrintItem != null)
            {

                strSql.Append(" and IsPrintItem=" + model.IsPrintItem + " ");
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

            if (model.clientcode != null)
            {

                strSql.Append(" and ClientCode='" + model.clientcode + "' ");
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

            if (model.WebLisSourceOrgId != null)
            {

                strSql.Append(" and WebLisSourceOrgID='" + model.WebLisSourceOrgId + "' ");
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

        public DataSet GetListLike(Model.CLIENTELE model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *,ClIENTNO as LabNo,convert(varchar(100),ClIENTNO)+'_'+CNAME as LabNo_Value,CNAME+'('+convert(varchar(100),ClIENTNO)+')' as LabNoAndName_Text ");
            strSql.Append(" FROM B_CLIENTELE  ");

            if (model.CNAME != null)
            {
                strSql.Append(" where 1=2 ");
                strSql.Append(" or CNAME like '%" + model.CNAME + "%' ");
            }

            if (model.ClIENTNO != "" && model.ClIENTNO != null)
            {
                if (strSql.ToString().IndexOf("where 1=2") < 0)
                {
                    strSql.Append(" where 1=2 ");
                }
                strSql.Append(" or ClIENTNO like '%" + model.ClIENTNO + "%' ");
            }
            
            if (model.SHORTCODE != null)
            {
                if (strSql.ToString().IndexOf("where 1=2") < 0)
                {
                    strSql.Append(" where 1=2 ");
                }
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
            strSql.Append("select count(*) FROM B_CLIENTELE ");
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
        public int GetTotalCount(ZhiFang.Model.CLIENTELE model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            strSql.Append("select count(*) FROM B_CLIENTELE where 1=1 ");
            if (model != null)
            {
                if (model.ClIENTNO != "" && model.ClIENTNO != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or ClIENTNO like '%" + model.ClIENTNO + "%' ");
                    else
                        strWhere.Append(" and ( ClIENTNO like '%" + model.ClIENTNO + "%' ");
                }
                if (model.CNAME != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or CNAME like '%" + model.CNAME + "%' ");
                    else
                        strWhere.Append(" and ( CNAME like '%" + model.CNAME + "%' ");
                }
                if (model.ENAME != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or ENAME like '%" + model.ENAME + "%' ");
                    else
                        strWhere.Append(" and ( ENAME like '%" + model.ENAME + "%' ");
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
        public DataSet GetListByPage(ZhiFang.Model.CLIENTELE model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            if (model != null)
            {
                if (model.ClIENTNO != "" && model.ClIENTNO != null)
                {
                    strWhere.Append(" and ( B_CLIENTELE.ClIENTNO like '%" + model.ClIENTNO + "%'  ");
                }
                if (model.CNAME != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or B_CLIENTELE.CNAME like '%" + model.CNAME + "%'  ");
                    else
                        strWhere.Append(" and ( B_CLIENTELE.CNAME like '%" + model.CNAME + "%'  ");
                }
                if (model.ENAME != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or B_CLIENTELE.ENAME like '%" + model.ENAME + "%'  ");
                    else
                        strWhere.Append(" and ( B_CLIENTELE.ENAME like '%" + model.ENAME + "%'  ");
                }
                if (model.SHORTCODE != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or B_CLIENTELE.SHORTCODE like '%" + model.SHORTCODE + "%'  ");
                    else
                        strWhere.Append(" and ( B_CLIENTELE.SHORTCODE like '%" + model.SHORTCODE + "%'  ");
                }
                if (strWhere.Length > 0)
                    strWhere.Append(" ) ");
            }
            if (model != null && model.LabCode != null)
            {
                strSql.Append(" select top " + nowPageSize + "  * from B_CLIENTELE left join B_CLIENTELEControl on B_CLIENTELE.ClIENTNO=B_CLIENTELEControl.ClIENTNO ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and B_CLIENTELEControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append("where ClIENTID not in ( ");
                strSql.Append("select top " + (nowPageSize * nowPageNum) + " ClIENTID from  B_CLIENTELE left join B_CLIENTELEControl on B_CLIENTELE.ClIENTNO=B_CLIENTELEControl.ClIENTNO ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and B_CLIENTELEControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append(" " + strWhere.ToString() + " order by B_CLIENTELE.ClIENTID ) " + strWhere.ToString() + " order by B_CLIENTELE.ClIENTID ");
                return idb.ExecuteDataSet(strSql.ToString());
            }
            else
            {
                strSql.Append("select top " + nowPageSize + "  * from B_CLIENTELE where ClIENTID not in  ");
                strSql.Append("(select top " + (nowPageSize * nowPageNum) + " ClIENTID from B_CLIENTELE where 1=1 " + strWhere.ToString() + " order by " + model.OrderField + ") " + strWhere.ToString() + " order by " + model.OrderField + "  ");
                return idb.ExecuteDataSet(strSql.ToString());
            }
        }

        public bool Exists(long ClIENTNO)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from B_CLIENTELE ");
            strSql.Append(" where ClIENTNO ='" + ClIENTNO + "'");
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

        public bool Exists(System.Collections.Hashtable ht)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from B_CLIENTELE where 1=1 ");
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
            string LabTableName = "B_CLIENTELE";
            LabTableName = "B_Lab_" + LabTableName.Substring(LabTableName.IndexOf("_") + 1, LabTableName.Length - (LabTableName.IndexOf("_") + 1));
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSqlControl = new StringBuilder();
            string TableKey = "ClIENTNO";
            string TableKeySub = TableKey;
            if (TableKey.ToLower().Contains("no"))
            {
                TableKeySub = TableKey.Substring(0, TableKey.ToLower().IndexOf("no"));
            }
            try
            {
                for (int i = 0; i < lst.Count; i++)
                {
                    strSql.Append("insert into " + LabTableName + "( LabCode,");
                    strSql.Append(" LabClIENTNO , CNAME , ENAME , SHORTCODE , ISUSE , LINKMAN , PHONENUM1 , ADDRESS , MAILNO , EMAIL , PRINCIPAL , PHONENUM2 , CLIENTTYPE , BmanNo , Romark , TitleType , UploadType , PrintType , InputDataType , ReportPageType , ClientArea , ClientStyle , FaxNo , AutoFax , ClientReportTitle , IsPrintItem , CZDY1 , CZDY2 , CZDY3 , CZDY4 , CZDY5 , CZDY6 , LinkManPosition , LinkMan1 , LinkManPosition1 , ClientCode , CWAccountDate , NFClientType , RelationName , WebLisSourceOrgID , GroupName , StandCode , ZFStandCode , UseFlag ");
                    strSql.Append(") select '" + lst[i].Trim() + "' as LabCode, ");
                    strSql.Append("ClIENTNO,CNAME,ENAME,SHORTCODE,ISUSE,LINKMAN,PHONENUM1,ADDRESS,MAILNO,EMAIL,PRINCIPAL,PHONENUM2,CLIENTTYPE,BmanNo,Romark,TitleType,UploadType,PrintType,InputDataType,ReportPageType,ClientArea,ClientStyle,FaxNo,AutoFax,ClientReportTitle,IsPrintItem,CZDY1,CZDY2,CZDY3,CZDY4,CZDY5,CZDY6,LinkManPosition,LinkMan1,LinkManPosition1,ClientCode,CWAccountDate,NFClientType,RelationName,WebLisSourceOrgID,GroupName,StandCode,ZFStandCode,UseFlag");
                    strSql.Append(" from B_CLIENTELE ");

                    strSqlControl.Append("insert into B_CLIENTELEControl ( ");
                    strSqlControl.Append(" " + TableKeySub + "ControlNo," + TableKey + ",ControlLabNo,Control" + TableKey + ",UseFlag ");
                    strSqlControl.Append(")  select ");
                    strSqlControl.Append("  '" + lst[i].Trim() + "'+'_'+convert(varchar(50)," + TableKey + ")+'_'+convert(varchar(50), " + TableKey + ") as " + TableKeySub + "ControlNo," + TableKey + ",'" + lst[i].Trim() + "' as ControlLabNo," + TableKey + ",UseFlag ");
                    strSqlControl.Append(" from B_CLIENTELE ");

                    arrySql.Add(strSql.ToString());
                    arrySql.Add(strSqlControl.ToString());

                    strSql = new StringBuilder();
                    strSqlControl = new StringBuilder();

                }

                idb.BatchUpdateWithTransaction(arrySql);

                d_log.OperateLog("CLIENTELE", "", "", DateTime.Now, 1);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public int GetMaxId()
        {
            return idb.GetMaxID("ClIENTNO", "B_CLIENTELE");
        }

        public DataSet GetList(int Top, ZhiFang.Model.CLIENTELE model, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM B_CLIENTELE ");



            if (model.ClIENTNO != "" && model.ClIENTNO != null)
            {
                strSql.Append(" and ClIENTNO='" + model.ClIENTNO + "' ");
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


            if (model.ISUSE != null)
            {
                strSql.Append(" and ISUSE='" + model.ISUSE + "' ");
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
                strSql.Append(" and CLIENTTYPE='" + model.CLIENTTYPE + "' ");
            }


            if (model.bmanno != null)
            {
                strSql.Append(" and BmanNo='" + model.bmanno + "' ");
            }


            if (model.romark != null)
            {
                strSql.Append(" and Romark='" + model.romark + "' ");
            }


            if (model.titletype != null)
            {
                strSql.Append(" and TitleType='" + model.titletype + "' ");
            }


            if (model.uploadtype != null)
            {
                strSql.Append(" and UploadType='" + model.uploadtype + "' ");
            }


            if (model.printtype != null)
            {
                strSql.Append(" and PrintType='" + model.printtype + "' ");
            }


            if (model.InputDataType != null)
            {
                strSql.Append(" and InputDataType='" + model.InputDataType + "' ");
            }


            if (model.reportpagetype != null)
            {
                strSql.Append(" and ReportPageType='" + model.reportpagetype + "' ");
            }


            if (model.clientarea != null)
            {
                strSql.Append(" and ClientArea='" + model.clientarea + "' ");
            }


            if (model.clientstyle != null)
            {
                strSql.Append(" and ClientStyle='" + model.clientstyle + "' ");
            }


            if (model.FaxNo != null)
            {
                strSql.Append(" and FaxNo='" + model.FaxNo + "' ");
            }


            if (model.AutoFax != null)
            {
                strSql.Append(" and AutoFax='" + model.AutoFax + "' ");
            }


            if (model.ClientReportTitle != null)
            {
                strSql.Append(" and ClientReportTitle='" + model.ClientReportTitle + "' ");
            }


            if (model.IsPrintItem != null)
            {
                strSql.Append(" and IsPrintItem='" + model.IsPrintItem + "' ");
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


            if (model.clientcode != null)
            {
                strSql.Append(" and ClientCode='" + model.clientcode + "' ");
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


            if (model.WebLisSourceOrgId != null)
            {
                strSql.Append(" and WebLisSourceOrgID='" + model.WebLisSourceOrgId + "' ");
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
            strSql.Append("select *,'" + LabCode + "' as LabCode,ClIENTNO as LabClIENTNO from B_CLIENTELE where 1=1 ");
            if (dTimeStampe != -999999)
            {
                strSql.Append(" and convert(int,DTimeStampe) > '" + dTimeStampe + "' ");
            }
            DataTable dtServer = idb.ExecuteDataSet(strSql.ToString()).Tables[0];
            dtServer.TableName = "ServerDatas";

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("select *,LabClIENTNO as ClIENTNO from B_Lab_CLIENTELE where 1=1 ");
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
            strSql3.Append("select * from B_CLIENTELEControl where 1=1 ");
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
                        if (this.Exists(int.Parse(ds.Tables[0].Rows[i]["ClIENTNO"].ToString().Trim())))
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
                strSql.Append("insert into B_CLIENTELE (");
                strSql.Append("ClIENTNO,CNAME,ENAME,SHORTCODE,ISUSE,LINKMAN,PHONENUM1,ADDRESS,MAILNO,EMAIL,PRINCIPAL,PHONENUM2,CLIENTTYPE,BmanNo,Romark,TitleType,UploadType,PrintType,InputDataType,ReportPageType,ClientArea,ClientStyle,FaxNo,AutoFax,ClientReportTitle,IsPrintItem,CZDY1,CZDY2,CZDY3,CZDY4,CZDY5,CZDY6,LinkManPosition,LinkMan1,LinkManPosition1,ClientCode,CWAccountDate,NFClientType,RelationName,WebLisSourceOrgID,GroupName,StandCode,ZFStandCode,UseFlag");
                strSql.Append(") values (");

                if (dr.Table.Columns["ClIENTNO"] != null && dr.Table.Columns["ClIENTNO"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["ClIENTNO"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["CNAME"] != null && dr.Table.Columns["CNAME"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["CNAME"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["ENAME"] != null && dr.Table.Columns["ENAME"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["ENAME"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["SHORTCODE"] != null && dr.Table.Columns["SHORTCODE"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["SHORTCODE"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["ISUSE"] != null && dr.Table.Columns["ISUSE"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["ISUSE"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["LINKMAN"] != null && dr.Table.Columns["LINKMAN"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["LINKMAN"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["PHONENUM1"] != null && dr.Table.Columns["PHONENUM1"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["PHONENUM1"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["ADDRESS"] != null && dr.Table.Columns["ADDRESS"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["ADDRESS"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["MAILNO"] != null && dr.Table.Columns["MAILNO"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["MAILNO"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["EMAIL"] != null && dr.Table.Columns["EMAIL"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["EMAIL"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["PRINCIPAL"] != null && dr.Table.Columns["PRINCIPAL"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["PRINCIPAL"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["PHONENUM2"] != null && dr.Table.Columns["PHONENUM2"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["PHONENUM2"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["CLIENTTYPE"] != null && dr.Table.Columns["CLIENTTYPE"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["CLIENTTYPE"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["BmanNo"] != null && dr.Table.Columns["BmanNo"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["BmanNo"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["Romark"] != null && dr.Table.Columns["Romark"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["Romark"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["TitleType"] != null && dr.Table.Columns["TitleType"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["TitleType"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["UploadType"] != null && dr.Table.Columns["UploadType"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["UploadType"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["PrintType"] != null && dr.Table.Columns["PrintType"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["PrintType"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["InputDataType"] != null && dr.Table.Columns["InputDataType"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["InputDataType"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["ReportPageType"] != null && dr.Table.Columns["ReportPageType"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["ReportPageType"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["ClientArea"] != null && dr.Table.Columns["ClientArea"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["ClientArea"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["ClientStyle"] != null && dr.Table.Columns["ClientStyle"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["ClientStyle"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["FaxNo"] != null && dr.Table.Columns["FaxNo"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["FaxNo"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["AutoFax"] != null && dr.Table.Columns["AutoFax"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["AutoFax"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["ClientReportTitle"] != null && dr.Table.Columns["ClientReportTitle"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["ClientReportTitle"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["IsPrintItem"] != null && dr.Table.Columns["IsPrintItem"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["IsPrintItem"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["CZDY1"] != null && dr.Table.Columns["CZDY1"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["CZDY1"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["CZDY2"] != null && dr.Table.Columns["CZDY2"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["CZDY2"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["CZDY3"] != null && dr.Table.Columns["CZDY3"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["CZDY3"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["CZDY4"] != null && dr.Table.Columns["CZDY4"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["CZDY4"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["CZDY5"] != null && dr.Table.Columns["CZDY5"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["CZDY5"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["CZDY6"] != null && dr.Table.Columns["CZDY6"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["CZDY6"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["LinkManPosition"] != null && dr.Table.Columns["LinkManPosition"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["LinkManPosition"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["LinkMan1"] != null && dr.Table.Columns["LinkMan1"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["LinkMan1"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["LinkManPosition1"] != null && dr.Table.Columns["LinkManPosition1"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["LinkManPosition1"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["ClientCode"] != null && dr.Table.Columns["ClientCode"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["ClientCode"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["CWAccountDate"] != null && dr.Table.Columns["CWAccountDate"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["CWAccountDate"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["NFClientType"] != null && dr.Table.Columns["NFClientType"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["NFClientType"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["RelationName"] != null && dr.Table.Columns["RelationName"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["RelationName"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["WebLisSourceOrgID"] != null && dr.Table.Columns["WebLisSourceOrgID"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["WebLisSourceOrgID"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["GroupName"] != null && dr.Table.Columns["GroupName"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["GroupName"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["StandCode"] != null && dr.Table.Columns["StandCode"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["StandCode"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["ZFStandCode"] != null && dr.Table.Columns["ZFStandCode"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["ZFStandCode"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["UseFlag"] != null && dr.Table.Columns["UseFlag"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["UseFlag"].ToString().Trim() + "', ");
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
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Digitlab8.B_CLIENTELE.AddByDataRow 同步数据时异常：", ex);
                return 0;
            }
        }
        public int UpdateByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update B_CLIENTELE set ");


                if (dr.Table.Columns["CNAME"] != null && dr.Table.Columns["CNAME"].ToString().Trim() != "")
                {
                    strSql.Append(" CNAME = '" + dr["CNAME"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["ENAME"] != null && dr.Table.Columns["ENAME"].ToString().Trim() != "")
                {
                    strSql.Append(" ENAME = '" + dr["ENAME"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["SHORTCODE"] != null && dr.Table.Columns["SHORTCODE"].ToString().Trim() != "")
                {
                    strSql.Append(" SHORTCODE = '" + dr["SHORTCODE"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["ISUSE"] != null && dr.Table.Columns["ISUSE"].ToString().Trim() != "")
                {
                    strSql.Append(" ISUSE = '" + dr["ISUSE"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["LINKMAN"] != null && dr.Table.Columns["LINKMAN"].ToString().Trim() != "")
                {
                    strSql.Append(" LINKMAN = '" + dr["LINKMAN"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["PHONENUM1"] != null && dr.Table.Columns["PHONENUM1"].ToString().Trim() != "")
                {
                    strSql.Append(" PHONENUM1 = '" + dr["PHONENUM1"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["ADDRESS"] != null && dr.Table.Columns["ADDRESS"].ToString().Trim() != "")
                {
                    strSql.Append(" ADDRESS = '" + dr["ADDRESS"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["MAILNO"] != null && dr.Table.Columns["MAILNO"].ToString().Trim() != "")
                {
                    strSql.Append(" MAILNO = '" + dr["MAILNO"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["EMAIL"] != null && dr.Table.Columns["EMAIL"].ToString().Trim() != "")
                {
                    strSql.Append(" EMAIL = '" + dr["EMAIL"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["PRINCIPAL"] != null && dr.Table.Columns["PRINCIPAL"].ToString().Trim() != "")
                {
                    strSql.Append(" PRINCIPAL = '" + dr["PRINCIPAL"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["PHONENUM2"] != null && dr.Table.Columns["PHONENUM2"].ToString().Trim() != "")
                {
                    strSql.Append(" PHONENUM2 = '" + dr["PHONENUM2"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["CLIENTTYPE"] != null && dr.Table.Columns["CLIENTTYPE"].ToString().Trim() != "")
                {
                    strSql.Append(" CLIENTTYPE = '" + dr["CLIENTTYPE"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["BmanNo"] != null && dr.Table.Columns["BmanNo"].ToString().Trim() != "")
                {
                    strSql.Append(" BmanNo = '" + dr["BmanNo"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["Romark"] != null && dr.Table.Columns["Romark"].ToString().Trim() != "")
                {
                    strSql.Append(" Romark = '" + dr["Romark"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["TitleType"] != null && dr.Table.Columns["TitleType"].ToString().Trim() != "")
                {
                    strSql.Append(" TitleType = '" + dr["TitleType"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["UploadType"] != null && dr.Table.Columns["UploadType"].ToString().Trim() != "")
                {
                    strSql.Append(" UploadType = '" + dr["UploadType"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["PrintType"] != null && dr.Table.Columns["PrintType"].ToString().Trim() != "")
                {
                    strSql.Append(" PrintType = '" + dr["PrintType"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["InputDataType"] != null && dr.Table.Columns["InputDataType"].ToString().Trim() != "")
                {
                    strSql.Append(" InputDataType = '" + dr["InputDataType"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["ReportPageType"] != null && dr.Table.Columns["ReportPageType"].ToString().Trim() != "")
                {
                    strSql.Append(" ReportPageType = '" + dr["ReportPageType"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["ClientArea"] != null && dr.Table.Columns["ClientArea"].ToString().Trim() != "")
                {
                    strSql.Append(" ClientArea = '" + dr["ClientArea"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["ClientStyle"] != null && dr.Table.Columns["ClientStyle"].ToString().Trim() != "")
                {
                    strSql.Append(" ClientStyle = '" + dr["ClientStyle"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["FaxNo"] != null && dr.Table.Columns["FaxNo"].ToString().Trim() != "")
                {
                    strSql.Append(" FaxNo = '" + dr["FaxNo"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["AutoFax"] != null && dr.Table.Columns["AutoFax"].ToString().Trim() != "")
                {
                    strSql.Append(" AutoFax = '" + dr["AutoFax"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["ClientReportTitle"] != null && dr.Table.Columns["ClientReportTitle"].ToString().Trim() != "")
                {
                    strSql.Append(" ClientReportTitle = '" + dr["ClientReportTitle"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["IsPrintItem"] != null && dr.Table.Columns["IsPrintItem"].ToString().Trim() != "")
                {
                    strSql.Append(" IsPrintItem = '" + dr["IsPrintItem"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["CZDY1"] != null && dr.Table.Columns["CZDY1"].ToString().Trim() != "")
                {
                    strSql.Append(" CZDY1 = '" + dr["CZDY1"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["CZDY2"] != null && dr.Table.Columns["CZDY2"].ToString().Trim() != "")
                {
                    strSql.Append(" CZDY2 = '" + dr["CZDY2"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["CZDY3"] != null && dr.Table.Columns["CZDY3"].ToString().Trim() != "")
                {
                    strSql.Append(" CZDY3 = '" + dr["CZDY3"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["CZDY4"] != null && dr.Table.Columns["CZDY4"].ToString().Trim() != "")
                {
                    strSql.Append(" CZDY4 = '" + dr["CZDY4"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["CZDY5"] != null && dr.Table.Columns["CZDY5"].ToString().Trim() != "")
                {
                    strSql.Append(" CZDY5 = '" + dr["CZDY5"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["CZDY6"] != null && dr.Table.Columns["CZDY6"].ToString().Trim() != "")
                {
                    strSql.Append(" CZDY6 = '" + dr["CZDY6"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["LinkManPosition"] != null && dr.Table.Columns["LinkManPosition"].ToString().Trim() != "")
                {
                    strSql.Append(" LinkManPosition = '" + dr["LinkManPosition"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["LinkMan1"] != null && dr.Table.Columns["LinkMan1"].ToString().Trim() != "")
                {
                    strSql.Append(" LinkMan1 = '" + dr["LinkMan1"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["LinkManPosition1"] != null && dr.Table.Columns["LinkManPosition1"].ToString().Trim() != "")
                {
                    strSql.Append(" LinkManPosition1 = '" + dr["LinkManPosition1"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["ClientCode"] != null && dr.Table.Columns["ClientCode"].ToString().Trim() != "")
                {
                    strSql.Append(" ClientCode = '" + dr["ClientCode"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["CWAccountDate"] != null && dr.Table.Columns["CWAccountDate"].ToString().Trim() != "")
                {
                    strSql.Append(" CWAccountDate = '" + dr["CWAccountDate"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["NFClientType"] != null && dr.Table.Columns["NFClientType"].ToString().Trim() != "")
                {
                    strSql.Append(" NFClientType = '" + dr["NFClientType"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["RelationName"] != null && dr.Table.Columns["RelationName"].ToString().Trim() != "")
                {
                    strSql.Append(" RelationName = '" + dr["RelationName"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["WebLisSourceOrgID"] != null && dr.Table.Columns["WebLisSourceOrgID"].ToString().Trim() != "")
                {
                    strSql.Append(" WebLisSourceOrgID = '" + dr["WebLisSourceOrgID"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["GroupName"] != null && dr.Table.Columns["GroupName"].ToString().Trim() != "")
                {
                    strSql.Append(" GroupName = '" + dr["GroupName"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["StandCode"] != null && dr.Table.Columns["StandCode"].ToString().Trim() != "")
                {
                    strSql.Append(" StandCode = '" + dr["StandCode"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["ZFStandCode"] != null && dr.Table.Columns["ZFStandCode"].ToString().Trim() != "")
                {
                    strSql.Append(" ZFStandCode = '" + dr["ZFStandCode"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["UseFlag"] != null && dr.Table.Columns["UseFlag"].ToString().Trim() != "")
                {
                    strSql.Append(" UseFlag = '" + dr["UseFlag"].ToString().Trim() + "' , ");
                }


                int n = strSql.ToString().LastIndexOf(",");
                strSql.Remove(n, 1);
                strSql.Append(" where ClIENTNO='" + dr["ClIENTNO"].ToString().Trim() + "' ");

                return idb.ExecuteNonQuery(strSql.ToString());
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Digitlab8.B_CLIENTELE .UpdateByDataRow同步数据时异常：", ex);
                return 0;
            }
        }
        public int DeleteByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                if (dr.Table.Columns["ClIENTNO"] != null && dr.Table.Columns["ClIENTNO"].ToString().Trim() != "")
                {
                    strSql.Append("delete from B_CLIENTELE ");
                    strSql.Append(" where ClIENTNO='" + dr["ClIENTNO"].ToString().Trim() + "' ");
                    return idb.ExecuteNonQuery(strSql.ToString());
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.digitlab8.CLIENTELE.DeleteByDataRow同步数据时异常：", ex);
                return 0;
            }
        }


        public DataSet GetClientNo(string CLIENTIDlist, string CName)
        {
            throw new NotImplementedException();
        }


        public bool IsExist(string labCodeNo)
        {
            throw new NotImplementedException();
        }

        public bool DeleteByLabCode(string LabCodeNo)
        {
            throw new NotImplementedException();
        }


        public int Add(List<Model.CLIENTELE> modelList)
        {
            throw new NotImplementedException();
        }


        public int Update(List<Model.CLIENTELE> modelList)
        {
            throw new NotImplementedException();
        }
    }
}

