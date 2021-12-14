using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;

namespace ZhiFang.DAL.MsSql.Weblis
{
    /// <summary>
    /// 数据访问类:BarCodeForm
    /// </summary>
    public partial class BarCodeForm : BaseDALLisDB, IDBarCodeForm
    {
        public BarCodeForm(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public BarCodeForm()
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.BarCodeForm model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into BarCodeForm(");
            strSql.Append("BarCodeFormNo,Collecter,CollecterID,CollectDate,CollectTime,refuseUser,refuseopinion,refusereason,refuseTime,signflag,incepter,BarCode,inceptTime,inceptDate,ReceiveMan,ReceiveDate,ReceiveTime,PrintInfo,PrintCount,Dr2Flag,FlagDateDelete,DispenseFlag,SamplingGroupNo,SerialScanTime,BarCodeSource,DeleteFlag,SendOffFlag,SendOffMan,EMSMan,SendOffDate,ReportSignMan,ReportSignDate,RefuseIncepter,IsPrep,RefuseIncepterMemo,ReportFlag,SendOffMemo,SampleTypeNo,SampleSendNo,WebLisFlag,WebLisOpTime,WebLiser,WebLisDescript,WebLisOrgID,isSpiltItem,WebLisIsReply,WebLisReplyDate,WebLisSourceOrgId,WebLisUploadTime,WebLisUploadStatus,WebLisUploadTestStatus,WebLisUploader,WebLisUploadDes,WebLisSourceOrgName,ClientNo,IsAffirm,ClientName,ReceiveFlag,SampleCap,ClientHost,WebLisOrgName,color,ItemName,ItemNo,SampleTypeName");
            strSql.Append(") values (");
            strSql.Append("@BarCodeFormNo,@Collecter,@CollecterID,@CollectDate,@CollectTime,@refuseUser,@refuseopinion,@refusereason,@refuseTime,@signflag,@incepter,@BarCode,@inceptTime,@inceptDate,@ReceiveMan,@ReceiveDate,@ReceiveTime,@PrintInfo,@PrintCount,@Dr2Flag,@FlagDateDelete,@DispenseFlag,@SamplingGroupNo,@SerialScanTime,@BarCodeSource,@DeleteFlag,@SendOffFlag,@SendOffMan,@EMSMan,@SendOffDate,@ReportSignMan,@ReportSignDate,@RefuseIncepter,@IsPrep,@RefuseIncepterMemo,@ReportFlag,@SendOffMemo,@SampleTypeNo,@SampleSendNo,@WebLisFlag,@WebLisOpTime,@WebLiser,@WebLisDescript,@WebLisOrgID,@isSpiltItem,@WebLisIsReply,@WebLisReplyDate,@WebLisSourceOrgId,@WebLisUploadTime,@WebLisUploadStatus,@WebLisUploadTestStatus,@WebLisUploader,@WebLisUploadDes,@WebLisSourceOrgName,@ClientNo,@IsAffirm,@ClientName,@ReceiveFlag,@SampleCap,@ClientHost,@WebLisOrgName,@color,@ItemName,@ItemNo,@SampleTypeName");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@BarCodeFormNo", SqlDbType.BigInt,8) ,            
                        new SqlParameter("@Collecter", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CollecterID", SqlDbType.Int,4) ,            
                        new SqlParameter("@CollectDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@CollectTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@refuseUser", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@refuseopinion", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@refusereason", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@refuseTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@signflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@incepter", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@BarCode", SqlDbType.VarChar,30) ,            
                        new SqlParameter("@inceptTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@inceptDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@ReceiveMan", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ReceiveDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@ReceiveTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@PrintInfo", SqlDbType.VarChar,600) ,            
                        new SqlParameter("@PrintCount", SqlDbType.Int,4) ,            
                        new SqlParameter("@Dr2Flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FlagDateDelete", SqlDbType.DateTime) ,            
                        new SqlParameter("@DispenseFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@SamplingGroupNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@SerialScanTime", SqlDbType.VarChar,30) ,            
                        new SqlParameter("@BarCodeSource", SqlDbType.Int,4) ,            
                        new SqlParameter("@DeleteFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@SendOffFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@SendOffMan", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@EMSMan", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@SendOffDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@ReportSignMan", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ReportSignDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@RefuseIncepter", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@IsPrep", SqlDbType.Int,4) ,            
                        new SqlParameter("@RefuseIncepterMemo", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@ReportFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@SendOffMemo", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@SampleTypeNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@SampleSendNo", SqlDbType.VarChar,30) ,            
                        new SqlParameter("@WebLisFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@WebLisOpTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@WebLiser", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@WebLisDescript", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@WebLisOrgID", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@isSpiltItem", SqlDbType.Int,4) ,            
                        new SqlParameter("@WebLisIsReply", SqlDbType.Int,4) ,            
                        new SqlParameter("@WebLisReplyDate", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@WebLisSourceOrgId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@WebLisUploadTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@WebLisUploadStatus", SqlDbType.Int,4) ,            
                        new SqlParameter("@WebLisUploadTestStatus", SqlDbType.Int,4) ,            
                        new SqlParameter("@WebLisUploader", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@WebLisUploadDes", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@WebLisSourceOrgName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ClientNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@IsAffirm", SqlDbType.Int,4) ,            
                        new SqlParameter("@ClientName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ReceiveFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@SampleCap", SqlDbType.Float,8) ,            
                        new SqlParameter("@ClientHost", SqlDbType.VarChar,60),
                        new SqlParameter("@WebLisOrgName", SqlDbType.VarChar,150),
                          new SqlParameter("@color", SqlDbType.VarChar,20),
                           new SqlParameter("@ItemName", SqlDbType.VarChar,200),
                           new SqlParameter("@ItemNo", SqlDbType.VarChar,200),
                           new SqlParameter("@SampleTypeName",SqlDbType.VarChar,200)

            };

            parameters[0].Value = model.BarCodeFormNo;
            parameters[1].Value = model.Collecter;
            parameters[2].Value = model.CollecterID;
            parameters[3].Value = model.CollectDate;
            parameters[4].Value = model.CollectTime;
            parameters[5].Value = model.refuseUser;
            parameters[6].Value = model.refuseopinion;
            parameters[7].Value = model.refusereason;
            parameters[8].Value = model.refuseTime;
            parameters[9].Value = model.signflag;
            parameters[10].Value = model.incepter;
            parameters[11].Value = model.BarCode;
            parameters[12].Value = model.inceptTime;
            parameters[13].Value = model.inceptDate;
            parameters[14].Value = model.ReceiveMan;
            parameters[15].Value = model.ReceiveDate;
            parameters[16].Value = model.ReceiveTime;
            parameters[17].Value = model.PrintInfo;
            parameters[18].Value = model.PrintCount;
            parameters[19].Value = model.Dr2Flag;
            parameters[20].Value = model.FlagDateDelete;
            parameters[21].Value = model.DispenseFlag;
            parameters[22].Value = model.SamplingGroupNo;
            parameters[23].Value = model.SerialScanTime;
            parameters[24].Value = model.BarCodeSource;
            parameters[25].Value = model.DeleteFlag;
            parameters[26].Value = model.SendOffFlag;
            parameters[27].Value = model.SendOffMan;
            parameters[28].Value = model.EMSMan;
            parameters[29].Value = model.SendOffDate;
            parameters[30].Value = model.ReportSignMan;
            parameters[31].Value = model.ReportSignDate;
            parameters[32].Value = model.RefuseIncepter;
            parameters[33].Value = model.IsPrep;
            parameters[34].Value = model.RefuseIncepterMemo;
            parameters[35].Value = model.ReportFlag;
            parameters[36].Value = model.SendOffMemo;
            parameters[37].Value = model.SampleTypeNo;
            parameters[38].Value = model.SampleSendNo;
            parameters[39].Value = model.WebLisFlag;
            parameters[40].Value = model.WebLisOpTime;
            parameters[41].Value = model.WebLiser;
            parameters[42].Value = model.WebLisDescript;
            parameters[43].Value = model.WebLisOrgID;
            parameters[44].Value = model.isSpiltItem;
            parameters[45].Value = model.WebLisIsReply;
            parameters[46].Value = model.WebLisReplyDate;
            parameters[47].Value = model.WebLisSourceOrgId;
            parameters[48].Value = model.WebLisUploadTime;
            parameters[49].Value = model.WebLisUploadStatus;
            parameters[50].Value = model.WebLisUploadTestStatus;
            parameters[51].Value = model.WebLisUploader;
            parameters[52].Value = model.WebLisUploadDes;
            parameters[53].Value = model.WebLisSourceOrgName;
            parameters[54].Value = model.ClientNo;
            parameters[55].Value = model.IsAffirm;
            parameters[56].Value = model.ClientName;
            parameters[57].Value = model.ReceiveFlag;
            parameters[58].Value = model.SampleCap;
            parameters[59].Value = model.ClientHost;
            parameters[60].Value = model.WebLisOrgName;
            parameters[61].Value = model.Color;
            parameters[62].Value = model.ItemName;
            parameters[63].Value = model.ItemNo;
            parameters[64].Value = model.SampleTypeName;
            Common.Log.Log.Info(strSql.ToString());
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 十堰太和 申请单录入 组套项目不需要对照
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add_TaiHe(ZhiFang.Model.BarCodeForm model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into BarCodeForm(");
            strSql.Append("BarCodeFormNo,Collecter,CollecterID,CollectDate,CollectTime,refuseUser,refuseopinion,refusereason,refuseTime,signflag,incepter,BarCode,inceptTime,inceptDate,ReceiveMan,ReceiveDate,ReceiveTime,PrintInfo,PrintCount,Dr2Flag,FlagDateDelete,DispenseFlag,SamplingGroupNo,SerialScanTime,BarCodeSource,DeleteFlag,SendOffFlag,SendOffMan,EMSMan,SendOffDate,ReportSignMan,ReportSignDate,RefuseIncepter,IsPrep,RefuseIncepterMemo,ReportFlag,SendOffMemo,SampleTypeNo,SampleSendNo,WebLisFlag,WebLisOpTime,WebLiser,WebLisDescript,WebLisOrgID,isSpiltItem,WebLisIsReply,WebLisReplyDate,WebLisSourceOrgId,WebLisUploadTime,WebLisUploadStatus,WebLisUploadTestStatus,WebLisUploader,WebLisUploadDes,WebLisSourceOrgName,ClientNo,IsAffirm,ClientName,ReceiveFlag,SampleCap,ClientHost,WebLisOrgName,color,ItemName,ItemNo,SampleTypeName,LabItemName,LabItemNo");
            strSql.Append(") values (");
            strSql.Append("@BarCodeFormNo,@Collecter,@CollecterID,@CollectDate,@CollectTime,@refuseUser,@refuseopinion,@refusereason,@refuseTime,@signflag,@incepter,@BarCode,@inceptTime,@inceptDate,@ReceiveMan,@ReceiveDate,@ReceiveTime,@PrintInfo,@PrintCount,@Dr2Flag,@FlagDateDelete,@DispenseFlag,@SamplingGroupNo,@SerialScanTime,@BarCodeSource,@DeleteFlag,@SendOffFlag,@SendOffMan,@EMSMan,@SendOffDate,@ReportSignMan,@ReportSignDate,@RefuseIncepter,@IsPrep,@RefuseIncepterMemo,@ReportFlag,@SendOffMemo,@SampleTypeNo,@SampleSendNo,@WebLisFlag,@WebLisOpTime,@WebLiser,@WebLisDescript,@WebLisOrgID,@isSpiltItem,@WebLisIsReply,@WebLisReplyDate,@WebLisSourceOrgId,@WebLisUploadTime,@WebLisUploadStatus,@WebLisUploadTestStatus,@WebLisUploader,@WebLisUploadDes,@WebLisSourceOrgName,@ClientNo,@IsAffirm,@ClientName,@ReceiveFlag,@SampleCap,@ClientHost,@WebLisOrgName,@color,@ItemName,@ItemNo,@SampleTypeName,@LabItemName,@LabItemNo");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@BarCodeFormNo", SqlDbType.BigInt,8) ,            
                        new SqlParameter("@Collecter", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CollecterID", SqlDbType.Int,4) ,            
                        new SqlParameter("@CollectDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@CollectTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@refuseUser", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@refuseopinion", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@refusereason", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@refuseTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@signflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@incepter", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@BarCode", SqlDbType.VarChar,30) ,            
                        new SqlParameter("@inceptTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@inceptDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@ReceiveMan", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ReceiveDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@ReceiveTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@PrintInfo", SqlDbType.VarChar,600) ,            
                        new SqlParameter("@PrintCount", SqlDbType.Int,4) ,            
                        new SqlParameter("@Dr2Flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FlagDateDelete", SqlDbType.DateTime) ,            
                        new SqlParameter("@DispenseFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@SamplingGroupNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@SerialScanTime", SqlDbType.VarChar,30) ,            
                        new SqlParameter("@BarCodeSource", SqlDbType.Int,4) ,            
                        new SqlParameter("@DeleteFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@SendOffFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@SendOffMan", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@EMSMan", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@SendOffDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@ReportSignMan", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ReportSignDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@RefuseIncepter", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@IsPrep", SqlDbType.Int,4) ,            
                        new SqlParameter("@RefuseIncepterMemo", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@ReportFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@SendOffMemo", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@SampleTypeNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@SampleSendNo", SqlDbType.VarChar,30) ,            
                        new SqlParameter("@WebLisFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@WebLisOpTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@WebLiser", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@WebLisDescript", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@WebLisOrgID", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@isSpiltItem", SqlDbType.Int,4) ,            
                        new SqlParameter("@WebLisIsReply", SqlDbType.Int,4) ,            
                        new SqlParameter("@WebLisReplyDate", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@WebLisSourceOrgId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@WebLisUploadTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@WebLisUploadStatus", SqlDbType.Int,4) ,            
                        new SqlParameter("@WebLisUploadTestStatus", SqlDbType.Int,4) ,            
                        new SqlParameter("@WebLisUploader", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@WebLisUploadDes", SqlDbType.VarChar,500) ,            
                        new SqlParameter("@WebLisSourceOrgName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ClientNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@IsAffirm", SqlDbType.Int,4) ,            
                        new SqlParameter("@ClientName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ReceiveFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@SampleCap", SqlDbType.Float,8) ,            
                        new SqlParameter("@ClientHost", SqlDbType.VarChar,60),
                        new SqlParameter("@WebLisOrgName", SqlDbType.VarChar,150),
                          new SqlParameter("@color", SqlDbType.VarChar,20),
                           new SqlParameter("@ItemName", SqlDbType.VarChar,200),
                           new SqlParameter("@ItemNo", SqlDbType.VarChar,200),
                           new SqlParameter("@SampleTypeName",SqlDbType.VarChar,200),
                           new SqlParameter("@LabItemName",SqlDbType.VarChar,200),
                           new SqlParameter("@LabItemNo",SqlDbType.VarChar,200)
              
            };

            parameters[0].Value = model.BarCodeFormNo;
            parameters[1].Value = model.Collecter;
            parameters[2].Value = model.CollecterID;
            parameters[3].Value = model.CollectDate;
            parameters[4].Value = model.CollectTime;
            parameters[5].Value = model.refuseUser;
            parameters[6].Value = model.refuseopinion;
            parameters[7].Value = model.refusereason;
            parameters[8].Value = model.refuseTime;
            parameters[9].Value = model.signflag;
            parameters[10].Value = model.incepter;
            parameters[11].Value = model.BarCode;
            parameters[12].Value = model.inceptTime;
            parameters[13].Value = model.inceptDate;
            parameters[14].Value = model.ReceiveMan;
            parameters[15].Value = model.ReceiveDate;
            parameters[16].Value = model.ReceiveTime;
            parameters[17].Value = model.PrintInfo;
            parameters[18].Value = model.PrintCount;
            parameters[19].Value = model.Dr2Flag;
            parameters[20].Value = model.FlagDateDelete;
            parameters[21].Value = model.DispenseFlag;
            parameters[22].Value = model.SamplingGroupNo;
            parameters[23].Value = model.SerialScanTime;
            parameters[24].Value = model.BarCodeSource;
            parameters[25].Value = model.DeleteFlag;
            parameters[26].Value = model.SendOffFlag;
            parameters[27].Value = model.SendOffMan;
            parameters[28].Value = model.EMSMan;
            parameters[29].Value = model.SendOffDate;
            parameters[30].Value = model.ReportSignMan;
            parameters[31].Value = model.ReportSignDate;
            parameters[32].Value = model.RefuseIncepter;
            parameters[33].Value = model.IsPrep;
            parameters[34].Value = model.RefuseIncepterMemo;
            parameters[35].Value = model.ReportFlag;
            parameters[36].Value = model.SendOffMemo;
            parameters[37].Value = model.SampleTypeNo;
            parameters[38].Value = model.SampleSendNo;
            parameters[39].Value = model.WebLisFlag;
            parameters[40].Value = model.WebLisOpTime;
            parameters[41].Value = model.WebLiser;
            parameters[42].Value = model.WebLisDescript;
            parameters[43].Value = model.WebLisOrgID;
            parameters[44].Value = model.isSpiltItem;
            parameters[45].Value = model.WebLisIsReply;
            parameters[46].Value = model.WebLisReplyDate;
            parameters[47].Value = model.WebLisSourceOrgId;
            parameters[48].Value = model.WebLisUploadTime;
            parameters[49].Value = model.WebLisUploadStatus;
            parameters[50].Value = model.WebLisUploadTestStatus;
            parameters[51].Value = model.WebLisUploader;
            parameters[52].Value = model.WebLisUploadDes;
            parameters[53].Value = model.WebLisSourceOrgName;
            parameters[54].Value = model.ClientNo;
            parameters[55].Value = model.IsAffirm;
            parameters[56].Value = model.ClientName;
            parameters[57].Value = model.ReceiveFlag;
            parameters[58].Value = model.SampleCap;
            parameters[59].Value = model.ClientHost;
            parameters[60].Value = model.WebLisOrgName;
            parameters[61].Value = model.Color;
            parameters[62].Value = model.ItemName;
            parameters[63].Value = model.ItemNo;
            parameters[64].Value = model.SampleTypeName;
            parameters[65].Value = model.LabItemName;
            parameters[66].Value = model.LabItemNo;

            Common.Log.Log.Info(strSql.ToString());
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.BarCodeForm model)
        {
            StringBuilder strSql = new StringBuilder();
            try
            {
                strSql.Append("update BarCodeForm set ");

                strSql.Append(" BarCodeFormNo = @BarCodeFormNo , ");
                strSql.Append(" Collecter = @Collecter , ");
                strSql.Append(" CollecterID = @CollecterID , ");
                strSql.Append(" CollectDate = @CollectDate , ");
                strSql.Append(" CollectTime = @CollectTime , ");
                strSql.Append(" refuseUser = @refuseUser , ");
                strSql.Append(" refuseopinion = @refuseopinion , ");
                strSql.Append(" refusereason = @refusereason , ");
                strSql.Append(" refuseTime = @refuseTime , ");
                strSql.Append(" signflag = @signflag , ");
                strSql.Append(" incepter = @incepter , ");
                strSql.Append(" BarCode = @BarCode , ");
                strSql.Append(" inceptTime = @inceptTime , ");
                strSql.Append(" inceptDate = @inceptDate , ");
                strSql.Append(" ReceiveMan = @ReceiveMan , ");
                strSql.Append(" ReceiveDate = @ReceiveDate , ");
                strSql.Append(" ReceiveTime = @ReceiveTime , ");
                strSql.Append(" PrintInfo = @PrintInfo , ");
                strSql.Append(" PrintCount = @PrintCount , ");
                strSql.Append(" Dr2Flag = @Dr2Flag , ");
                strSql.Append(" FlagDateDelete = @FlagDateDelete , ");
                strSql.Append(" DispenseFlag = @DispenseFlag , ");
                strSql.Append(" SamplingGroupNo = @SamplingGroupNo , ");
                strSql.Append(" SerialScanTime = @SerialScanTime , ");
                strSql.Append(" BarCodeSource = @BarCodeSource , ");
                strSql.Append(" DeleteFlag = @DeleteFlag , ");
                strSql.Append(" SendOffFlag = @SendOffFlag , ");
                strSql.Append(" SendOffMan = @SendOffMan , ");
                strSql.Append(" EMSMan = @EMSMan , ");
                strSql.Append(" SendOffDate = @SendOffDate , ");
                strSql.Append(" ReportSignMan = @ReportSignMan , ");
                strSql.Append(" ReportSignDate = @ReportSignDate , ");
                strSql.Append(" RefuseIncepter = @RefuseIncepter , ");
                strSql.Append(" IsPrep = @IsPrep , ");
                strSql.Append(" RefuseIncepterMemo = @RefuseIncepterMemo , ");
                strSql.Append(" ReportFlag = @ReportFlag , ");
                strSql.Append(" SendOffMemo = @SendOffMemo , ");
                strSql.Append(" SampleTypeNo = @SampleTypeNo , ");
                strSql.Append(" SampleSendNo = @SampleSendNo , ");
                strSql.Append(" WebLisFlag = @WebLisFlag , ");
                strSql.Append(" WebLisOpTime = @WebLisOpTime , ");
                strSql.Append(" WebLiser = @WebLiser , ");
                strSql.Append(" WebLisDescript = @WebLisDescript , ");
                strSql.Append(" WebLisOrgID = @WebLisOrgID , ");
                strSql.Append(" isSpiltItem = @isSpiltItem , ");
                strSql.Append(" WebLisIsReply = @WebLisIsReply , ");
                strSql.Append(" WebLisReplyDate = @WebLisReplyDate , ");
                strSql.Append(" WebLisSourceOrgId = @WebLisSourceOrgId , ");
                strSql.Append(" WebLisUploadTime = @WebLisUploadTime , ");
                strSql.Append(" WebLisUploadStatus = @WebLisUploadStatus , ");
                strSql.Append(" WebLisUploadTestStatus = @WebLisUploadTestStatus , ");
                strSql.Append(" WebLisUploader = @WebLisUploader , ");
                strSql.Append(" WebLisUploadDes = @WebLisUploadDes , ");
                strSql.Append(" WebLisSourceOrgName = @WebLisSourceOrgName , ");
                strSql.Append(" ClientNo = @ClientNo , ");
                strSql.Append(" IsAffirm = @IsAffirm , ");
                strSql.Append(" ClientName = @ClientName , ");
                strSql.Append(" ReceiveFlag = @ReceiveFlag , ");
                strSql.Append(" SampleCap = @SampleCap , ");
                strSql.Append(" ClientHost = @ClientHost,  ");
                strSql.Append(" WebLisOrgName = @WebLisOrgName, ");
                strSql.Append(" color = @color, ");
                strSql.Append(" ItemName = @ItemName, ");
                strSql.Append(" ItemNo = @ItemNo, ");
                strSql.Append(" SampleTypeName=@SampleTypeName ");
                strSql.Append(" where BarCodeFormNo=@BarCodeFormNo  ");

                SqlParameter[] parameters = {
			               
            new SqlParameter("@BarCodeFormNo", SqlDbType.BigInt,8) ,            	
                           
            new SqlParameter("@Collecter", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@CollecterID", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@CollectDate", SqlDbType.DateTime) ,            	
                           
            new SqlParameter("@CollectTime", SqlDbType.DateTime) ,            	
                           
            new SqlParameter("@refuseUser", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@refuseopinion", SqlDbType.VarChar,100) ,            	
                           
            new SqlParameter("@refusereason", SqlDbType.VarChar,100) ,            	
                           
            new SqlParameter("@refuseTime", SqlDbType.DateTime) ,            	
                           
            new SqlParameter("@signflag", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@incepter", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@BarCode", SqlDbType.VarChar,30) ,            	
                           
            new SqlParameter("@inceptTime", SqlDbType.DateTime) ,            	
                           
            new SqlParameter("@inceptDate", SqlDbType.DateTime) ,            	
                           
            new SqlParameter("@ReceiveMan", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@ReceiveDate", SqlDbType.DateTime) ,            	
                           
            new SqlParameter("@ReceiveTime", SqlDbType.DateTime) ,            	
                           
            new SqlParameter("@PrintInfo", SqlDbType.VarChar,600) ,            	
                           
            new SqlParameter("@PrintCount", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@Dr2Flag", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@FlagDateDelete", SqlDbType.DateTime) ,            	
                           
            new SqlParameter("@DispenseFlag", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@SamplingGroupNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@SerialScanTime", SqlDbType.VarChar,30) ,            	
                           
            new SqlParameter("@BarCodeSource", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@DeleteFlag", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@SendOffFlag", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@SendOffMan", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@EMSMan", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@SendOffDate", SqlDbType.DateTime) ,            	
                           
            new SqlParameter("@ReportSignMan", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@ReportSignDate", SqlDbType.DateTime) ,            	
                           
            new SqlParameter("@RefuseIncepter", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@IsPrep", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@RefuseIncepterMemo", SqlDbType.VarChar,200) ,            	
                           
            new SqlParameter("@ReportFlag", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@SendOffMemo", SqlDbType.VarChar,200) ,            	
                           
            new SqlParameter("@SampleTypeNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@SampleSendNo", SqlDbType.VarChar,30) ,            	
                           
            new SqlParameter("@WebLisFlag", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@WebLisOpTime", SqlDbType.DateTime) ,            	
                           
            new SqlParameter("@WebLiser", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@WebLisDescript", SqlDbType.VarChar,500) ,            	
                           
            new SqlParameter("@WebLisOrgID", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@isSpiltItem", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@WebLisIsReply", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@WebLisReplyDate", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@WebLisSourceOrgId", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@WebLisUploadTime", SqlDbType.DateTime) ,            	
                           
            new SqlParameter("@WebLisUploadStatus", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@WebLisUploadTestStatus", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@WebLisUploader", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@WebLisUploadDes", SqlDbType.VarChar,500) ,            	
                           
            new SqlParameter("@WebLisSourceOrgName", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@ClientNo", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@IsAffirm", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@ClientName", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@ReceiveFlag", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@SampleCap", SqlDbType.Float,8) ,            	
                           
            new SqlParameter("@ClientHost", SqlDbType.VarChar,60),             	
              
            new SqlParameter("@WebLisOrgName", SqlDbType.VarChar,150),
            new SqlParameter("@color", SqlDbType.VarChar,20),
            new SqlParameter("@ItemName", SqlDbType.VarChar,200),
            new SqlParameter("@ItemNo", SqlDbType.VarChar,200),
            new SqlParameter("@SampleTypeName", SqlDbType.VarChar,200)

            };


                if (model.BarCodeFormNo != null)
                {
                    parameters[0].Value = model.BarCodeFormNo;
                }



                if (model.Collecter != null)
                {
                    parameters[1].Value = model.Collecter;
                }



                if (model.CollecterID != null)
                {
                    parameters[2].Value = model.CollecterID;
                }



                if (model.CollectDate != null)
                {
                    parameters[3].Value = model.CollectDate;
                }



                if (model.CollectTime != null)
                {
                    parameters[4].Value = model.CollectTime;
                }



                if (model.refuseUser != null)
                {
                    parameters[5].Value = model.refuseUser;
                }



                if (model.refuseopinion != null)
                {
                    parameters[6].Value = model.refuseopinion;
                }



                if (model.refusereason != null)
                {
                    parameters[7].Value = model.refusereason;
                }



                if (model.refuseTime != null)
                {
                    parameters[8].Value = model.refuseTime;
                }



                if (model.signflag != null)
                {
                    parameters[9].Value = model.signflag;
                }



                if (model.incepter != null)
                {
                    parameters[10].Value = model.incepter;
                }



                if (model.BarCode != null)
                {
                    parameters[11].Value = model.BarCode;
                }



                if (model.inceptTime != null)
                {
                    parameters[12].Value = model.inceptTime;
                }



                if (model.inceptDate != null)
                {
                    parameters[13].Value = model.inceptDate;
                }



                if (model.ReceiveMan != null)
                {
                    parameters[14].Value = model.ReceiveMan;
                }



                if (model.ReceiveDate != null)
                {
                    parameters[15].Value = model.ReceiveDate;
                }



                if (model.ReceiveTime != null)
                {
                    parameters[16].Value = model.ReceiveTime;
                }



                if (model.PrintInfo != null)
                {
                    parameters[17].Value = model.PrintInfo;
                }



                if (model.PrintCount != null)
                {
                    parameters[18].Value = model.PrintCount;
                }



                if (model.Dr2Flag != null)
                {
                    parameters[19].Value = model.Dr2Flag;
                }



                if (model.FlagDateDelete != null)
                {
                    parameters[20].Value = model.FlagDateDelete;
                }



                if (model.DispenseFlag != null)
                {
                    parameters[21].Value = model.DispenseFlag;
                }



                if (model.SamplingGroupNo != null)
                {
                    parameters[22].Value = model.SamplingGroupNo;
                }



                if (model.SerialScanTime != null)
                {
                    parameters[23].Value = model.SerialScanTime;
                }



                if (model.BarCodeSource != null)
                {
                    parameters[24].Value = model.BarCodeSource;
                }



                if (model.DeleteFlag != null)
                {
                    parameters[25].Value = model.DeleteFlag;
                }



                if (model.SendOffFlag != null)
                {
                    parameters[26].Value = model.SendOffFlag;
                }



                if (model.SendOffMan != null)
                {
                    parameters[27].Value = model.SendOffMan;
                }



                if (model.EMSMan != null)
                {
                    parameters[28].Value = model.EMSMan;
                }



                if (model.SendOffDate != null)
                {
                    parameters[29].Value = model.SendOffDate;
                }



                if (model.ReportSignMan != null)
                {
                    parameters[30].Value = model.ReportSignMan;
                }



                if (model.ReportSignDate != null)
                {
                    parameters[31].Value = model.ReportSignDate;
                }



                if (model.RefuseIncepter != null)
                {
                    parameters[32].Value = model.RefuseIncepter;
                }



                if (model.IsPrep != null)
                {
                    parameters[33].Value = model.IsPrep;
                }



                if (model.RefuseIncepterMemo != null)
                {
                    parameters[34].Value = model.RefuseIncepterMemo;
                }



                if (model.ReportFlag != null)
                {
                    parameters[35].Value = model.ReportFlag;
                }



                if (model.SendOffMemo != null)
                {
                    parameters[36].Value = model.SendOffMemo;
                }



                if (model.SampleTypeNo != null)
                {
                    parameters[37].Value = model.SampleTypeNo;
                }



                if (model.SampleSendNo != null)
                {
                    parameters[38].Value = model.SampleSendNo;
                }



                if (model.WebLisFlag != null)
                {
                    parameters[39].Value = model.WebLisFlag;
                }



                if (model.WebLisOpTime != null)
                {
                    parameters[40].Value = model.WebLisOpTime;
                }



                if (model.WebLiser != null)
                {
                    parameters[41].Value = model.WebLiser;
                }



                if (model.WebLisDescript != null)
                {
                    parameters[42].Value = model.WebLisDescript;
                }



                if (model.WebLisOrgID != null)
                {
                    parameters[43].Value = model.WebLisOrgID;
                }



                if (model.isSpiltItem != null)
                {
                    parameters[44].Value = model.isSpiltItem;
                }



                if (model.WebLisIsReply != null)
                {
                    parameters[45].Value = model.WebLisIsReply;
                }



                if (model.WebLisReplyDate != null)
                {
                    parameters[46].Value = model.WebLisReplyDate;
                }



                if (model.WebLisSourceOrgId != null)
                {
                    parameters[47].Value = model.WebLisSourceOrgId;
                }



                if (model.WebLisUploadTime != null)
                {
                    parameters[48].Value = model.WebLisUploadTime;
                }



                if (model.WebLisUploadStatus != null)
                {
                    parameters[49].Value = model.WebLisUploadStatus;
                }



                if (model.WebLisUploadTestStatus != null)
                {
                    parameters[50].Value = model.WebLisUploadTestStatus;
                }



                if (model.WebLisUploader != null)
                {
                    parameters[51].Value = model.WebLisUploader;
                }



                if (model.WebLisUploadDes != null)
                {
                    parameters[52].Value = model.WebLisUploadDes;
                }



                if (model.WebLisSourceOrgName != null)
                {
                    parameters[53].Value = model.WebLisSourceOrgName;
                }



                if (model.ClientNo != null)
                {
                    parameters[54].Value = model.ClientNo;
                }



                if (model.IsAffirm != null)
                {
                    parameters[55].Value = model.IsAffirm;
                }



                if (model.ClientName != null)
                {
                    parameters[56].Value = model.ClientName;
                }



                if (model.ReceiveFlag != null)
                {
                    parameters[57].Value = model.ReceiveFlag;
                }



                if (model.SampleCap != null)
                {
                    parameters[58].Value = model.SampleCap;
                }



                if (model.ClientHost != null)
                {
                    parameters[59].Value = model.ClientHost;
                }
                if (model.WebLisOrgName != null)
                {
                    parameters[60].Value = model.WebLisOrgName;
                }
                if (model.Color != null)
                    parameters[61].Value = model.Color;
                if (model.ItemName != null)
                    parameters[62].Value = model.ItemName;
                if (model.ItemNo != null)
                    parameters[63].Value = model.ItemNo;
                if (model.SampleTypeName != null)
                    parameters[64].Value = model.SampleTypeName;
                //Common.Log.Log.Info(strSql.ToString());
                return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Info(e.ToString());
                return 0;

            }
        }

        /// <summary>
        /// 十堰太和 申请单录入 组套项目不需要对照 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update_TaiHe(ZhiFang.Model.BarCodeForm model)
        {
            StringBuilder strSql = new StringBuilder();
            try
            {
                strSql.Append("update BarCodeForm set ");

                strSql.Append(" BarCodeFormNo = @BarCodeFormNo , ");
                strSql.Append(" Collecter = @Collecter , ");
                strSql.Append(" CollecterID = @CollecterID , ");
                strSql.Append(" CollectDate = @CollectDate , ");
                strSql.Append(" CollectTime = @CollectTime , ");
                strSql.Append(" refuseUser = @refuseUser , ");
                strSql.Append(" refuseopinion = @refuseopinion , ");
                strSql.Append(" refusereason = @refusereason , ");
                strSql.Append(" refuseTime = @refuseTime , ");
                strSql.Append(" signflag = @signflag , ");
                strSql.Append(" incepter = @incepter , ");
                strSql.Append(" BarCode = @BarCode , ");
                strSql.Append(" inceptTime = @inceptTime , ");
                strSql.Append(" inceptDate = @inceptDate , ");
                strSql.Append(" ReceiveMan = @ReceiveMan , ");
                strSql.Append(" ReceiveDate = @ReceiveDate , ");
                strSql.Append(" ReceiveTime = @ReceiveTime , ");
                strSql.Append(" PrintInfo = @PrintInfo , ");
                strSql.Append(" PrintCount = @PrintCount , ");
                strSql.Append(" Dr2Flag = @Dr2Flag , ");
                strSql.Append(" FlagDateDelete = @FlagDateDelete , ");
                strSql.Append(" DispenseFlag = @DispenseFlag , ");
                strSql.Append(" SamplingGroupNo = @SamplingGroupNo , ");
                strSql.Append(" SerialScanTime = @SerialScanTime , ");
                strSql.Append(" BarCodeSource = @BarCodeSource , ");
                strSql.Append(" DeleteFlag = @DeleteFlag , ");
                strSql.Append(" SendOffFlag = @SendOffFlag , ");
                strSql.Append(" SendOffMan = @SendOffMan , ");
                strSql.Append(" EMSMan = @EMSMan , ");
                strSql.Append(" SendOffDate = @SendOffDate , ");
                strSql.Append(" ReportSignMan = @ReportSignMan , ");
                strSql.Append(" ReportSignDate = @ReportSignDate , ");
                strSql.Append(" RefuseIncepter = @RefuseIncepter , ");
                strSql.Append(" IsPrep = @IsPrep , ");
                strSql.Append(" RefuseIncepterMemo = @RefuseIncepterMemo , ");
                strSql.Append(" ReportFlag = @ReportFlag , ");
                strSql.Append(" SendOffMemo = @SendOffMemo , ");
                strSql.Append(" SampleTypeNo = @SampleTypeNo , ");
                strSql.Append(" SampleSendNo = @SampleSendNo , ");
                strSql.Append(" WebLisFlag = @WebLisFlag , ");
                strSql.Append(" WebLisOpTime = @WebLisOpTime , ");
                strSql.Append(" WebLiser = @WebLiser , ");
                strSql.Append(" WebLisDescript = @WebLisDescript , ");
                strSql.Append(" WebLisOrgID = @WebLisOrgID , ");
                strSql.Append(" isSpiltItem = @isSpiltItem , ");
                strSql.Append(" WebLisIsReply = @WebLisIsReply , ");
                strSql.Append(" WebLisReplyDate = @WebLisReplyDate , ");
                strSql.Append(" WebLisSourceOrgId = @WebLisSourceOrgId , ");
                strSql.Append(" WebLisUploadTime = @WebLisUploadTime , ");
                strSql.Append(" WebLisUploadStatus = @WebLisUploadStatus , ");
                strSql.Append(" WebLisUploadTestStatus = @WebLisUploadTestStatus , ");
                strSql.Append(" WebLisUploader = @WebLisUploader , ");
                strSql.Append(" WebLisUploadDes = @WebLisUploadDes , ");
                strSql.Append(" WebLisSourceOrgName = @WebLisSourceOrgName , ");
                strSql.Append(" ClientNo = @ClientNo , ");
                strSql.Append(" IsAffirm = @IsAffirm , ");
                strSql.Append(" ClientName = @ClientName , ");
                strSql.Append(" ReceiveFlag = @ReceiveFlag , ");
                strSql.Append(" SampleCap = @SampleCap , ");
                strSql.Append(" ClientHost = @ClientHost,  ");
                strSql.Append(" WebLisOrgName = @WebLisOrgName, ");
                strSql.Append(" color = @color, ");
                strSql.Append(" ItemName = @ItemName, ");
                strSql.Append(" ItemNo = @ItemNo, ");
                strSql.Append(" SampleTypeName=@SampleTypeName, ");
                strSql.Append(" LabItemName = @LabItemName, ");
                strSql.Append(" LabItemNo = @LabItemNo ");
                strSql.Append(" where BarCodeFormNo=@BarCodeFormNo  ");


                SqlParameter[] parameters = {
			               
            new SqlParameter("@BarCodeFormNo", SqlDbType.BigInt,8) ,            	
                           
            new SqlParameter("@Collecter", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@CollecterID", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@CollectDate", SqlDbType.DateTime) ,            	
                           
            new SqlParameter("@CollectTime", SqlDbType.DateTime) ,            	
                           
            new SqlParameter("@refuseUser", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@refuseopinion", SqlDbType.VarChar,100) ,            	
                           
            new SqlParameter("@refusereason", SqlDbType.VarChar,100) ,            	
                           
            new SqlParameter("@refuseTime", SqlDbType.DateTime) ,            	
                           
            new SqlParameter("@signflag", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@incepter", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@BarCode", SqlDbType.VarChar,30) ,            	
                           
            new SqlParameter("@inceptTime", SqlDbType.DateTime) ,            	
                           
            new SqlParameter("@inceptDate", SqlDbType.DateTime) ,            	
                           
            new SqlParameter("@ReceiveMan", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@ReceiveDate", SqlDbType.DateTime) ,            	
                           
            new SqlParameter("@ReceiveTime", SqlDbType.DateTime) ,            	
                           
            new SqlParameter("@PrintInfo", SqlDbType.VarChar,600) ,            	
                           
            new SqlParameter("@PrintCount", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@Dr2Flag", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@FlagDateDelete", SqlDbType.DateTime) ,            	
                           
            new SqlParameter("@DispenseFlag", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@SamplingGroupNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@SerialScanTime", SqlDbType.VarChar,30) ,            	
                           
            new SqlParameter("@BarCodeSource", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@DeleteFlag", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@SendOffFlag", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@SendOffMan", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@EMSMan", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@SendOffDate", SqlDbType.DateTime) ,            	
                           
            new SqlParameter("@ReportSignMan", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@ReportSignDate", SqlDbType.DateTime) ,            	
                           
            new SqlParameter("@RefuseIncepter", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@IsPrep", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@RefuseIncepterMemo", SqlDbType.VarChar,200) ,            	
                           
            new SqlParameter("@ReportFlag", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@SendOffMemo", SqlDbType.VarChar,200) ,            	
                           
            new SqlParameter("@SampleTypeNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@SampleSendNo", SqlDbType.VarChar,30) ,            	
                           
            new SqlParameter("@WebLisFlag", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@WebLisOpTime", SqlDbType.DateTime) ,            	
                           
            new SqlParameter("@WebLiser", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@WebLisDescript", SqlDbType.VarChar,500) ,            	
                           
            new SqlParameter("@WebLisOrgID", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@isSpiltItem", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@WebLisIsReply", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@WebLisReplyDate", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@WebLisSourceOrgId", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@WebLisUploadTime", SqlDbType.DateTime) ,            	
                           
            new SqlParameter("@WebLisUploadStatus", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@WebLisUploadTestStatus", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@WebLisUploader", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@WebLisUploadDes", SqlDbType.VarChar,500) ,            	
                           
            new SqlParameter("@WebLisSourceOrgName", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@ClientNo", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@IsAffirm", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@ClientName", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@ReceiveFlag", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@SampleCap", SqlDbType.Float,8) ,            	
                           
            new SqlParameter("@ClientHost", SqlDbType.VarChar,60),             	
              
            new SqlParameter("@WebLisOrgName", SqlDbType.VarChar,150),
            new SqlParameter("@color", SqlDbType.VarChar,20),
            new SqlParameter("@ItemName", SqlDbType.VarChar,200),
            new SqlParameter("@ItemNo", SqlDbType.VarChar,200),
            new SqlParameter("@SampleTypeName", SqlDbType.VarChar,200),
              new SqlParameter("@LabItemName", SqlDbType.VarChar,200),
            new SqlParameter("@LabItemNo", SqlDbType.VarChar,200)

            };

                if (model.BarCodeFormNo != null)
                {
                    parameters[0].Value = model.BarCodeFormNo;
                }

                if (model.Collecter != null)
                {
                    parameters[1].Value = model.Collecter;
                }

                if (model.CollecterID != null)
                {
                    parameters[2].Value = model.CollecterID;
                }

                if (model.CollectDate != null)
                {
                    parameters[3].Value = model.CollectDate;
                }

                if (model.CollectTime != null)
                {
                    parameters[4].Value = model.CollectTime;
                }

                if (model.refuseUser != null)
                {
                    parameters[5].Value = model.refuseUser;
                }

                if (model.refuseopinion != null)
                {
                    parameters[6].Value = model.refuseopinion;
                }

                if (model.refusereason != null)
                {
                    parameters[7].Value = model.refusereason;
                }

                if (model.refuseTime != null)
                {
                    parameters[8].Value = model.refuseTime;
                }

                if (model.signflag != null)
                {
                    parameters[9].Value = model.signflag;
                }

                if (model.incepter != null)
                {
                    parameters[10].Value = model.incepter;
                }

                if (model.BarCode != null)
                {
                    parameters[11].Value = model.BarCode;
                }

                if (model.inceptTime != null)
                {
                    parameters[12].Value = model.inceptTime;
                }

                if (model.inceptDate != null)
                {
                    parameters[13].Value = model.inceptDate;
                }

                if (model.ReceiveMan != null)
                {
                    parameters[14].Value = model.ReceiveMan;
                }

                if (model.ReceiveDate != null)
                {
                    parameters[15].Value = model.ReceiveDate;
                }

                if (model.ReceiveTime != null)
                {
                    parameters[16].Value = model.ReceiveTime;
                }

                if (model.PrintInfo != null)
                {
                    parameters[17].Value = model.PrintInfo;
                }

                if (model.PrintCount != null)
                {
                    parameters[18].Value = model.PrintCount;
                }

                if (model.Dr2Flag != null)
                {
                    parameters[19].Value = model.Dr2Flag;
                }

                if (model.FlagDateDelete != null)
                {
                    parameters[20].Value = model.FlagDateDelete;
                }

                if (model.DispenseFlag != null)
                {
                    parameters[21].Value = model.DispenseFlag;
                }

                if (model.SamplingGroupNo != null)
                {
                    parameters[22].Value = model.SamplingGroupNo;
                }

                if (model.SerialScanTime != null)
                {
                    parameters[23].Value = model.SerialScanTime;
                }

                if (model.BarCodeSource != null)
                {
                    parameters[24].Value = model.BarCodeSource;
                }

                if (model.DeleteFlag != null)
                {
                    parameters[25].Value = model.DeleteFlag;
                }

                if (model.SendOffFlag != null)
                {
                    parameters[26].Value = model.SendOffFlag;
                }

                if (model.SendOffMan != null)
                {
                    parameters[27].Value = model.SendOffMan;
                }

                if (model.EMSMan != null)
                {
                    parameters[28].Value = model.EMSMan;
                }

                if (model.SendOffDate != null)
                {
                    parameters[29].Value = model.SendOffDate;
                }

                if (model.ReportSignMan != null)
                {
                    parameters[30].Value = model.ReportSignMan;
                }

                if (model.ReportSignDate != null)
                {
                    parameters[31].Value = model.ReportSignDate;
                }

                if (model.RefuseIncepter != null)
                {
                    parameters[32].Value = model.RefuseIncepter;
                }

                if (model.IsPrep != null)
                {
                    parameters[33].Value = model.IsPrep;
                }

                if (model.RefuseIncepterMemo != null)
                {
                    parameters[34].Value = model.RefuseIncepterMemo;
                }

                if (model.ReportFlag != null)
                {
                    parameters[35].Value = model.ReportFlag;
                }

                if (model.SendOffMemo != null)
                {
                    parameters[36].Value = model.SendOffMemo;
                }

                if (model.SampleTypeNo != null)
                {
                    parameters[37].Value = model.SampleTypeNo;
                }

                if (model.SampleSendNo != null)
                {
                    parameters[38].Value = model.SampleSendNo;
                }

                if (model.WebLisFlag != null)
                {
                    parameters[39].Value = model.WebLisFlag;
                }

                if (model.WebLisOpTime != null)
                {
                    parameters[40].Value = model.WebLisOpTime;
                }

                if (model.WebLiser != null)
                {
                    parameters[41].Value = model.WebLiser;
                }

                if (model.WebLisDescript != null)
                {
                    parameters[42].Value = model.WebLisDescript;
                }

                if (model.WebLisOrgID != null)
                {
                    parameters[43].Value = model.WebLisOrgID;
                }

                if (model.isSpiltItem != null)
                {
                    parameters[44].Value = model.isSpiltItem;
                }

                if (model.WebLisIsReply != null)
                {
                    parameters[45].Value = model.WebLisIsReply;
                }

                if (model.WebLisReplyDate != null)
                {
                    parameters[46].Value = model.WebLisReplyDate;
                }

                if (model.WebLisSourceOrgId != null)
                {
                    parameters[47].Value = model.WebLisSourceOrgId;
                }

                if (model.WebLisUploadTime != null)
                {
                    parameters[48].Value = model.WebLisUploadTime;
                }

                if (model.WebLisUploadStatus != null)
                {
                    parameters[49].Value = model.WebLisUploadStatus;
                }

                if (model.WebLisUploadTestStatus != null)
                {
                    parameters[50].Value = model.WebLisUploadTestStatus;
                }

                if (model.WebLisUploader != null)
                {
                    parameters[51].Value = model.WebLisUploader;
                }

                if (model.WebLisUploadDes != null)
                {
                    parameters[52].Value = model.WebLisUploadDes;
                }

                if (model.WebLisSourceOrgName != null)
                {
                    parameters[53].Value = model.WebLisSourceOrgName;
                }

                if (model.ClientNo != null)
                {
                    parameters[54].Value = model.ClientNo;
                }

                if (model.IsAffirm != null)
                {
                    parameters[55].Value = model.IsAffirm;
                }

                if (model.ClientName != null)
                {
                    parameters[56].Value = model.ClientName;
                }

                if (model.ReceiveFlag != null)
                {
                    parameters[57].Value = model.ReceiveFlag;
                }

                if (model.SampleCap != null)
                {
                    parameters[58].Value = model.SampleCap;
                }

                if (model.ClientHost != null)
                {
                    parameters[59].Value = model.ClientHost;
                }
                if (model.WebLisOrgName != null)
                {
                    parameters[60].Value = model.WebLisOrgName;
                }
                if (model.Color != null)
                    parameters[61].Value = model.Color;
                if (model.ItemName != null)
                    parameters[62].Value = model.ItemName;
                if (model.ItemNo != null)
                    parameters[63].Value = model.ItemNo;
                if (model.SampleTypeName != null)
                    parameters[64].Value = model.SampleTypeName;
                if (model.LabItemName != null)
                    parameters[65].Value = model.LabItemName;
                if (model.LabItemNo != null)
                    parameters[66].Value = model.LabItemNo;
                return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Info(e.ToString());
                return 0;

            }
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(long BarCodeFormNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from BarCodeForm ");
            strSql.Append(" where BarCodeFormNo=@BarCodeFormNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@BarCodeFormNo", SqlDbType.BigInt,8)			};
            parameters[0].Value = BarCodeFormNo;


            return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.BarCodeForm GetModel(long BarCodeFormNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select BarCodeFormNo, Collecter, CollecterID, CollectDate, CollectTime, refuseUser, refuseopinion, refusereason, refuseTime, signflag, incepter, BarCode, inceptTime, inceptDate, ReceiveMan, ReceiveDate, ReceiveTime, PrintInfo, PrintCount, Dr2Flag, FlagDateDelete, DispenseFlag, SamplingGroupNo, SerialScanTime, BarCodeSource, DeleteFlag, SendOffFlag, SendOffMan, EMSMan, SendOffDate, ReportSignMan, ReportSignDate, RefuseIncepter, IsPrep, RefuseIncepterMemo, ReportFlag, SendOffMemo, SampleTypeNo, SampleSendNo, WebLisFlag, WebLisOpTime, WebLiser, WebLisDescript, WebLisOrgID, isSpiltItem, WebLisIsReply, WebLisReplyDate, WebLisSourceOrgId, WebLisUploadTime, WebLisUploadStatus, WebLisUploadTestStatus, WebLisUploader, WebLisUploadDes, WebLisSourceOrgName, ClientNo, IsAffirm, ClientName, ReceiveFlag, SampleCap,color,ItemName,ItemNo, ClientHost,SampleTypeName  ");
            strSql.Append("  from BarCodeForm ");
            strSql.Append(" where BarCodeFormNo=@BarCodeFormNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@BarCodeFormNo", SqlDbType.BigInt,8)			};
            parameters[0].Value = BarCodeFormNo;


            ZhiFang.Model.BarCodeForm model = new ZhiFang.Model.BarCodeForm();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["BarCodeFormNo"].ToString() != "")
                {
                    model.BarCodeFormNo = Int64.Parse(ds.Tables[0].Rows[0]["BarCodeFormNo"].ToString());
                }
                model.Collecter = ds.Tables[0].Rows[0]["Collecter"].ToString();
                if (ds.Tables[0].Rows[0]["CollecterID"].ToString() != "")
                {
                    model.CollecterID = int.Parse(ds.Tables[0].Rows[0]["CollecterID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CollectDate"].ToString() != "")
                {
                    model.CollectDate = DateTime.Parse(ds.Tables[0].Rows[0]["CollectDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CollectTime"].ToString() != "")
                {
                    model.CollectTime = DateTime.Parse(ds.Tables[0].Rows[0]["CollectTime"].ToString());
                }
                model.refuseUser = ds.Tables[0].Rows[0]["refuseUser"].ToString();
                model.refuseopinion = ds.Tables[0].Rows[0]["refuseopinion"].ToString();
                model.refusereason = ds.Tables[0].Rows[0]["refusereason"].ToString();
                if (ds.Tables[0].Rows[0]["refuseTime"].ToString() != "")
                {
                    model.refuseTime = DateTime.Parse(ds.Tables[0].Rows[0]["refuseTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["signflag"].ToString() != "")
                {
                    model.signflag = int.Parse(ds.Tables[0].Rows[0]["signflag"].ToString());
                }
                model.incepter = ds.Tables[0].Rows[0]["incepter"].ToString();
                model.BarCode = ds.Tables[0].Rows[0]["BarCode"].ToString();
                if (ds.Tables[0].Rows[0]["inceptTime"].ToString() != "")
                {
                    model.inceptTime = DateTime.Parse(ds.Tables[0].Rows[0]["inceptTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["inceptDate"].ToString() != "")
                {
                    model.inceptDate = DateTime.Parse(ds.Tables[0].Rows[0]["inceptDate"].ToString());
                }
                model.ReceiveMan = ds.Tables[0].Rows[0]["ReceiveMan"].ToString();
                if (ds.Tables[0].Rows[0]["ReceiveDate"].ToString() != "")
                {
                    model.ReceiveDate = DateTime.Parse(ds.Tables[0].Rows[0]["ReceiveDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ReceiveTime"].ToString() != "")
                {
                    model.ReceiveTime = DateTime.Parse(ds.Tables[0].Rows[0]["ReceiveTime"].ToString());
                }
                model.PrintInfo = ds.Tables[0].Rows[0]["PrintInfo"].ToString();
                if (ds.Tables[0].Rows[0]["PrintCount"].ToString() != "")
                {
                    model.PrintCount = int.Parse(ds.Tables[0].Rows[0]["PrintCount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Dr2Flag"].ToString() != "")
                {
                    model.Dr2Flag = int.Parse(ds.Tables[0].Rows[0]["Dr2Flag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FlagDateDelete"].ToString() != "")
                {
                    model.FlagDateDelete = DateTime.Parse(ds.Tables[0].Rows[0]["FlagDateDelete"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DispenseFlag"].ToString() != "")
                {
                    model.DispenseFlag = int.Parse(ds.Tables[0].Rows[0]["DispenseFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SamplingGroupNo"].ToString() != "")
                {
                    model.SamplingGroupNo = int.Parse(ds.Tables[0].Rows[0]["SamplingGroupNo"].ToString());
                }
                model.SerialScanTime = ds.Tables[0].Rows[0]["SerialScanTime"].ToString();
                if (ds.Tables[0].Rows[0]["BarCodeSource"].ToString() != "")
                {
                    model.BarCodeSource = int.Parse(ds.Tables[0].Rows[0]["BarCodeSource"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DeleteFlag"].ToString() != "")
                {
                    model.DeleteFlag = int.Parse(ds.Tables[0].Rows[0]["DeleteFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SendOffFlag"].ToString() != "")
                {
                    model.SendOffFlag = int.Parse(ds.Tables[0].Rows[0]["SendOffFlag"].ToString());
                }
                model.SendOffMan = ds.Tables[0].Rows[0]["SendOffMan"].ToString();
                model.EMSMan = ds.Tables[0].Rows[0]["EMSMan"].ToString();
                if (ds.Tables[0].Rows[0]["SendOffDate"].ToString() != "")
                {
                    model.SendOffDate = DateTime.Parse(ds.Tables[0].Rows[0]["SendOffDate"].ToString());
                }
                model.ReportSignMan = ds.Tables[0].Rows[0]["ReportSignMan"].ToString();
                if (ds.Tables[0].Rows[0]["ReportSignDate"].ToString() != "")
                {
                    model.ReportSignDate = DateTime.Parse(ds.Tables[0].Rows[0]["ReportSignDate"].ToString());
                }
                model.RefuseIncepter = ds.Tables[0].Rows[0]["RefuseIncepter"].ToString();
                if (ds.Tables[0].Rows[0]["IsPrep"].ToString() != "")
                {
                    model.IsPrep = int.Parse(ds.Tables[0].Rows[0]["IsPrep"].ToString());
                }
                model.RefuseIncepterMemo = ds.Tables[0].Rows[0]["RefuseIncepterMemo"].ToString();
                if (ds.Tables[0].Rows[0]["ReportFlag"].ToString() != "")
                {
                    model.ReportFlag = int.Parse(ds.Tables[0].Rows[0]["ReportFlag"].ToString());
                }
                model.SendOffMemo = ds.Tables[0].Rows[0]["SendOffMemo"].ToString();
                if (ds.Tables[0].Rows[0]["SampleTypeNo"].ToString() != "")
                {
                    model.SampleTypeNo = int.Parse(ds.Tables[0].Rows[0]["SampleTypeNo"].ToString());
                }
                model.SampleSendNo = ds.Tables[0].Rows[0]["SampleSendNo"].ToString();
                if (ds.Tables[0].Rows[0]["WebLisFlag"].ToString() != "")
                {
                    model.WebLisFlag = int.Parse(ds.Tables[0].Rows[0]["WebLisFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WebLisOpTime"].ToString() != "")
                {
                    model.WebLisOpTime = DateTime.Parse(ds.Tables[0].Rows[0]["WebLisOpTime"].ToString());
                }
                model.WebLiser = ds.Tables[0].Rows[0]["WebLiser"].ToString();
                model.WebLisDescript = ds.Tables[0].Rows[0]["WebLisDescript"].ToString();
                model.WebLisOrgID = ds.Tables[0].Rows[0]["WebLisOrgID"].ToString();
                if (ds.Tables[0].Rows[0]["isSpiltItem"].ToString() != "")
                {
                    model.isSpiltItem = int.Parse(ds.Tables[0].Rows[0]["isSpiltItem"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WebLisIsReply"].ToString() != "")
                {
                    model.WebLisIsReply = int.Parse(ds.Tables[0].Rows[0]["WebLisIsReply"].ToString());
                }
                model.WebLisReplyDate = ds.Tables[0].Rows[0]["WebLisReplyDate"].ToString();
                model.WebLisSourceOrgId = ds.Tables[0].Rows[0]["WebLisSourceOrgId"].ToString();
                if (ds.Tables[0].Rows[0]["WebLisUploadTime"].ToString() != "")
                {
                    model.WebLisUploadTime = DateTime.Parse(ds.Tables[0].Rows[0]["WebLisUploadTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WebLisUploadStatus"].ToString() != "")
                {
                    model.WebLisUploadStatus = int.Parse(ds.Tables[0].Rows[0]["WebLisUploadStatus"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WebLisUploadTestStatus"].ToString() != "")
                {
                    model.WebLisUploadTestStatus = int.Parse(ds.Tables[0].Rows[0]["WebLisUploadTestStatus"].ToString());
                }
                model.WebLisUploader = ds.Tables[0].Rows[0]["WebLisUploader"].ToString();
                model.WebLisUploadDes = ds.Tables[0].Rows[0]["WebLisUploadDes"].ToString();
                model.WebLisSourceOrgName = ds.Tables[0].Rows[0]["WebLisSourceOrgName"].ToString();
                model.ClientNo = ds.Tables[0].Rows[0]["ClientNo"].ToString();
                if (ds.Tables[0].Rows[0]["IsAffirm"].ToString() != "")
                {
                    model.IsAffirm = int.Parse(ds.Tables[0].Rows[0]["IsAffirm"].ToString());
                }
                model.ClientName = ds.Tables[0].Rows[0]["ClientName"].ToString();
                if (ds.Tables[0].Rows[0]["ReceiveFlag"].ToString() != "")
                {
                    model.ReceiveFlag = int.Parse(ds.Tables[0].Rows[0]["ReceiveFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SampleCap"].ToString() != "")
                {
                    model.SampleCap = decimal.Parse(ds.Tables[0].Rows[0]["SampleCap"].ToString());
                }
                if (ds.Tables[0].Rows[0]["color"].ToString() != "")
                {
                    model.Color = ds.Tables[0].Rows[0]["color"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ItemName"].ToString() != "")
                {
                    model.ItemName = ds.Tables[0].Rows[0]["ItemName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ItemNo"].ToString() != "")
                {
                    model.ItemNo = ds.Tables[0].Rows[0]["ItemNo"].ToString();
                }
                model.ClientHost = ds.Tables[0].Rows[0]["ClientHost"].ToString();
                if (ds.Tables[0].Rows[0]["SampleTypeName"].ToString() != "")
                    model.SampleTypeName = ds.Tables[0].Rows[0]["SampleTypeName"].ToString();
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
            strSql.Append("select * ");
            strSql.Append(" FROM BarCodeForm ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetWeblisOrgName(ZhiFang.Model.BarCodeForm model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct WebLisSourceOrgName,WebLisSourceOrgId,COUNT(*) as BarCodeCount from BarCodeForm where ");
            if (model.WebLisSourceOrgId != null && model.WebLisSourceOrgId != "")
            {
                strSql.Append(" WebLisSourceOrgId=" + model.WebLisSourceOrgId + "and");
            }
            if (model.CollectDateStart != null)
            {
                strSql.Append(" BarCodeForm.CollectDate >='" + model.CollectDateStart + "'  ");
            }
            if (model.CollectDateEnd != null)
            {
                strSql.Append(" and BarCodeForm.CollectDate <='" + model.CollectDateEnd + "'  ");
            }
            strSql.Append("Group by WebLisSourceOrgName,WebLisSourceOrgId");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
        public DataSet GetAllList(ZhiFang.Model.BarCodeForm model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct SENDITEMNAME, BarCode,Cname,GenderName,Age,DoctorName,NRequestForm.CollectTime,NRequestForm.OperTime,DistrictNo from BarCodeForm join NRequestForm on BarCodeForm.BarCode=NRequestForm.SerialNo join NRequestItem on NRequestItem.BarCodeFormNo=BarCodeForm.BarCodeFormNo");
            if (model.WebLisSourceOrgId != null && model.WebLisSourceOrgId != "")
            {
                strSql.Append(" where BarCodeForm.WebLisSourceOrgId=" + model.WebLisSourceOrgId);
            }
            Common.Log.Log.Info(strSql.ToString());
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());

        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM BarCodeForm ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 根据实体获取DataSet
        /// </summary>
        public DataSet GetList(ZhiFang.Model.BarCodeForm model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM BarCodeForm where 1=1 ");


            if (model.BarCodeFormNo != null && model.BarCodeFormNo.Value != 0)
            {
                strSql.Append(" and BarCodeFormNo=" + model.BarCodeFormNo + " ");
            }

            if (model.Collecter != null)
            {
                strSql.Append(" and Collecter='" + model.Collecter + "' ");
            }

            if (model.CollecterID != null)
            {
                strSql.Append(" and CollecterID=" + model.CollecterID + " ");
            }

            if (model.CollectDate != null)
            {
                strSql.Append(" and CollectDate='" + model.CollectDate + "' ");
            }

            if (model.CollectTime != null)
            {
                strSql.Append(" and CollectTime='" + model.CollectTime + "' ");
            }

            if (model.refuseUser != null)
            {
                strSql.Append(" and refuseUser='" + model.refuseUser + "' ");
            }

            if (model.refuseopinion != null)
            {
                strSql.Append(" and refuseopinion='" + model.refuseopinion + "' ");
            }

            if (model.refusereason != null)
            {
                strSql.Append(" and refusereason='" + model.refusereason + "' ");
            }

            if (model.refuseTime != null)
            {
                strSql.Append(" and refuseTime='" + model.refuseTime + "' ");
            }

            if (model.signflag != null)
            {
                strSql.Append(" and signflag=" + model.signflag + " ");
            }

            if (model.incepter != null)
            {
                strSql.Append(" and incepter='" + model.incepter + "' ");
            }

            if (model.BarCode != null && model.BarCode!="")
            {
                strSql.Append(" and BarCode='" + model.BarCode + "' ");
            }

            if (model.inceptTime != null)
            {
                strSql.Append(" and inceptTime='" + model.inceptTime + "' ");
            }

            if (model.inceptDate != null)
            {
                strSql.Append(" and inceptDate='" + model.inceptDate + "' ");
            }

            if (model.ReceiveMan != null)
            {
                strSql.Append(" and ReceiveMan='" + model.ReceiveMan + "' ");
            }

            if (model.ReceiveDate != null)
            {
                strSql.Append(" and ReceiveDate='" + model.ReceiveDate + "' ");
            }

            if (model.ReceiveTime != null)
            {
                strSql.Append(" and ReceiveTime='" + model.ReceiveTime + "' ");
            }

            if (model.PrintInfo != null)
            {
                strSql.Append(" and PrintInfo='" + model.PrintInfo + "' ");
            }

            if (model.PrintCount != null)
            {
                strSql.Append(" and PrintCount=" + model.PrintCount + " ");
            }

            if (model.Dr2Flag != null)
            {
                strSql.Append(" and Dr2Flag=" + model.Dr2Flag + " ");
            }

            if (model.FlagDateDelete != null)
            {
                strSql.Append(" and FlagDateDelete='" + model.FlagDateDelete + "' ");
            }

            if (model.DispenseFlag != null)
            {
                strSql.Append(" and DispenseFlag=" + model.DispenseFlag + " ");
            }

            if (model.SamplingGroupNo != null)
            {
                strSql.Append(" and SamplingGroupNo=" + model.SamplingGroupNo + " ");
            }

            if (model.SerialScanTime != null)
            {
                strSql.Append(" and SerialScanTime='" + model.SerialScanTime + "' ");
            }

            if (model.BarCodeSource != null)
            {
                strSql.Append(" and BarCodeSource=" + model.BarCodeSource + " ");
            }

            if (model.DeleteFlag != null)
            {
                strSql.Append(" and DeleteFlag=" + model.DeleteFlag + " ");
            }

            if (model.SendOffFlag != null)
            {
                strSql.Append(" and SendOffFlag=" + model.SendOffFlag + " ");
            }

            if (model.SendOffMan != null)
            {
                strSql.Append(" and SendOffMan='" + model.SendOffMan + "' ");
            }

            if (model.EMSMan != null)
            {
                strSql.Append(" and EMSMan='" + model.EMSMan + "' ");
            }

            if (model.SendOffDate != null)
            {
                strSql.Append(" and SendOffDate='" + model.SendOffDate + "' ");
            }

            if (model.ReportSignMan != null)
            {
                strSql.Append(" and ReportSignMan='" + model.ReportSignMan + "' ");
            }

            if (model.ReportSignDate != null)
            {
                strSql.Append(" and ReportSignDate='" + model.ReportSignDate + "' ");
            }

            if (model.RefuseIncepter != null)
            {
                strSql.Append(" and RefuseIncepter='" + model.RefuseIncepter + "' ");
            }

            if (model.IsPrep != null)
            {
                strSql.Append(" and IsPrep=" + model.IsPrep + " ");
            }

            if (model.RefuseIncepterMemo != null)
            {
                strSql.Append(" and RefuseIncepterMemo='" + model.RefuseIncepterMemo + "' ");
            }

            if (model.ReportFlag != null)
            {
                strSql.Append(" and ReportFlag=" + model.ReportFlag + " ");
            }

            if (model.SendOffMemo != null)
            {
                strSql.Append(" and SendOffMemo='" + model.SendOffMemo + "' ");
            }

            if (model.SampleTypeNo != null)
            {
                strSql.Append(" and SampleTypeNo=" + model.SampleTypeNo + " ");
            }

            if (model.SampleSendNo != null)
            {
                strSql.Append(" and SampleSendNo='" + model.SampleSendNo + "' ");
            }

            if (model.WebLisFlag != null)
            {
                strSql.Append(" and WebLisFlag=" + model.WebLisFlag + " ");
            }

            if (model.WebLisOpTime != null)
            {
                strSql.Append(" and WebLisOpTime='" + model.WebLisOpTime + "' ");
            }

            if (model.WebLiser != null)
            {
                strSql.Append(" and WebLiser='" + model.WebLiser + "' ");
            }

            if (model.WebLisDescript != null)
            {
                strSql.Append(" and WebLisDescript='" + model.WebLisDescript + "' ");
            }

            if (model.WebLisOrgID != null && model.WebLisOrgID!="")
            {
                strSql.Append(" and WebLisOrgID='" + model.WebLisOrgID + "' ");
            }

            if (model.isSpiltItem != null)
            {
                strSql.Append(" and isSpiltItem=" + model.isSpiltItem + " ");
            }

            if (model.WebLisIsReply != null)
            {
                strSql.Append(" and WebLisIsReply=" + model.WebLisIsReply + " ");
            }

            if (model.WebLisReplyDate != null)
            {
                strSql.Append(" and WebLisReplyDate='" + model.WebLisReplyDate + "' ");
            }

            if (model.WebLisSourceOrgId != null && model.WebLisSourceOrgId!="")
            {
                strSql.Append(" and WebLisSourceOrgId='" + model.WebLisSourceOrgId + "' ");
            }

            if (model.WebLisUploadTime != null)
            {
                strSql.Append(" and WebLisUploadTime='" + model.WebLisUploadTime + "' ");
            }

            if (model.WebLisUploadStatus != null)
            {
                strSql.Append(" and WebLisUploadStatus=" + model.WebLisUploadStatus + " ");
            }

            if (model.WebLisUploadTestStatus != null)
            {
                strSql.Append(" and WebLisUploadTestStatus=" + model.WebLisUploadTestStatus + " ");
            }

            if (model.WebLisUploader != null)
            {
                strSql.Append(" and WebLisUploader='" + model.WebLisUploader + "' ");
            }

            if (model.WebLisUploadDes != null)
            {
                strSql.Append(" and WebLisUploadDes='" + model.WebLisUploadDes + "' ");
            }

            if (model.WebLisSourceOrgName != null)
            {
                strSql.Append(" and WebLisSourceOrgName='" + model.WebLisSourceOrgName + "' ");
            }

            if (model.ClientNo != null && model.ClientNo!="")
            {
                strSql.Append(" and ClientNo='" + model.ClientNo + "' ");
            }

            if (model.IsAffirm != null)
            {
                strSql.Append(" and IsAffirm=" + model.IsAffirm + " ");
            }

            if (model.ClientName != null)
            {
                strSql.Append(" and ClientName='" + model.ClientName + "' ");
            }

            if (model.ReceiveFlag != null)
            {
                strSql.Append(" and ReceiveFlag=" + model.ReceiveFlag + " ");
            }

            if (model.SampleCap != null)
            {
                strSql.Append(" and SampleCap=" + model.SampleCap + " ");
            }

            if (model.ClientHost != null)
            {
                strSql.Append(" and ClientHost='" + model.ClientHost + "' ");
            }
            if (model.WebLisOrgName != null)
            {
                strSql.Append(" and WebLisOrgName='" + model.WebLisOrgName + "' ");
            }
             if (model.ClientNoList != null&& model.ClientNoList.Count>0)
            {
                strSql.Append(" and WebLisSourceOrgId in ('" + string.Join("','",model.ClientNoList) + "') ");
            }
            if (model.SampleTypeName != null)
                strSql.Append(" and SampleTypeName='" + model.SampleTypeName + "' ");
            ZhiFang.Common.Log.Log.Info("查询条码表信息SQL:" + strSql.ToString());
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 获取总记录
        /// </summary>
        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM BarCodeForm ");
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
        public int GetTotalCount(ZhiFang.Model.BarCodeForm model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM BarCodeForm where 1=1 ");

            if (model.BarCodeFormNo != null)
            {
                strSql.Append(" and BarCodeFormNo=" + model.BarCodeFormNo + " ");
            }

            if (model.Collecter != null)
            {
                strSql.Append(" and Collecter='" + model.Collecter + "' ");
            }

            if (model.CollecterID != null)
            {
                strSql.Append(" and CollecterID=" + model.CollecterID + " ");
            }

            if (model.CollectDate != null)
            {
                strSql.Append(" and CollectDate='" + model.CollectDate + "' ");
            }

            if (model.CollectTime != null)
            {
                strSql.Append(" and CollectTime='" + model.CollectTime + "' ");
            }

            if (model.refuseUser != null)
            {
                strSql.Append(" and refuseUser='" + model.refuseUser + "' ");
            }

            if (model.refuseopinion != null)
            {
                strSql.Append(" and refuseopinion='" + model.refuseopinion + "' ");
            }

            if (model.refusereason != null)
            {
                strSql.Append(" and refusereason='" + model.refusereason + "' ");
            }

            if (model.refuseTime != null)
            {
                strSql.Append(" and refuseTime='" + model.refuseTime + "' ");
            }

            if (model.signflag != null)
            {
                strSql.Append(" and signflag=" + model.signflag + " ");
            }

            if (model.incepter != null)
            {
                strSql.Append(" and incepter='" + model.incepter + "' ");
            }

            if (model.BarCode != null)
            {
                strSql.Append(" and BarCode='" + model.BarCode + "' ");
            }

            if (model.inceptTime != null)
            {
                strSql.Append(" and inceptTime='" + model.inceptTime + "' ");
            }

            if (model.inceptDate != null)
            {
                strSql.Append(" and inceptDate='" + model.inceptDate + "' ");
            }

            if (model.ReceiveMan != null)
            {
                strSql.Append(" and ReceiveMan='" + model.ReceiveMan + "' ");
            }

            if (model.ReceiveDate != null)
            {
                strSql.Append(" and ReceiveDate='" + model.ReceiveDate + "' ");
            }

            if (model.ReceiveTime != null)
            {
                strSql.Append(" and ReceiveTime='" + model.ReceiveTime + "' ");
            }

            if (model.PrintInfo != null)
            {
                strSql.Append(" and PrintInfo='" + model.PrintInfo + "' ");
            }

            if (model.PrintCount != null)
            {
                strSql.Append(" and PrintCount=" + model.PrintCount + " ");
            }

            if (model.Dr2Flag != null)
            {
                strSql.Append(" and Dr2Flag=" + model.Dr2Flag + " ");
            }

            if (model.FlagDateDelete != null)
            {
                strSql.Append(" and FlagDateDelete='" + model.FlagDateDelete + "' ");
            }

            if (model.DispenseFlag != null)
            {
                strSql.Append(" and DispenseFlag=" + model.DispenseFlag + " ");
            }

            if (model.SamplingGroupNo != null)
            {
                strSql.Append(" and SamplingGroupNo=" + model.SamplingGroupNo + " ");
            }

            if (model.SerialScanTime != null)
            {
                strSql.Append(" and SerialScanTime='" + model.SerialScanTime + "' ");
            }

            if (model.BarCodeSource != null)
            {
                strSql.Append(" and BarCodeSource=" + model.BarCodeSource + " ");
            }

            if (model.DeleteFlag != null)
            {
                strSql.Append(" and DeleteFlag=" + model.DeleteFlag + " ");
            }

            if (model.SendOffFlag != null)
            {
                strSql.Append(" and SendOffFlag=" + model.SendOffFlag + " ");
            }

            if (model.SendOffMan != null)
            {
                strSql.Append(" and SendOffMan='" + model.SendOffMan + "' ");
            }

            if (model.EMSMan != null)
            {
                strSql.Append(" and EMSMan='" + model.EMSMan + "' ");
            }

            if (model.SendOffDate != null)
            {
                strSql.Append(" and SendOffDate='" + model.SendOffDate + "' ");
            }

            if (model.ReportSignMan != null)
            {
                strSql.Append(" and ReportSignMan='" + model.ReportSignMan + "' ");
            }

            if (model.ReportSignDate != null)
            {
                strSql.Append(" and ReportSignDate='" + model.ReportSignDate + "' ");
            }

            if (model.RefuseIncepter != null)
            {
                strSql.Append(" and RefuseIncepter='" + model.RefuseIncepter + "' ");
            }

            if (model.IsPrep != null)
            {
                strSql.Append(" and IsPrep=" + model.IsPrep + " ");
            }

            if (model.RefuseIncepterMemo != null)
            {
                strSql.Append(" and RefuseIncepterMemo='" + model.RefuseIncepterMemo + "' ");
            }

            if (model.ReportFlag != null)
            {
                strSql.Append(" and ReportFlag=" + model.ReportFlag + " ");
            }

            if (model.SendOffMemo != null)
            {
                strSql.Append(" and SendOffMemo='" + model.SendOffMemo + "' ");
            }

            if (model.SampleTypeNo != null)
            {
                strSql.Append(" and SampleTypeNo=" + model.SampleTypeNo + " ");
            }

            if (model.SampleSendNo != null)
            {
                strSql.Append(" and SampleSendNo='" + model.SampleSendNo + "' ");
            }

            if (model.WebLisFlag != null)
            {
                strSql.Append(" and WebLisFlag=" + model.WebLisFlag + " ");
            }

            if (model.WebLisOpTime != null)
            {
                strSql.Append(" and WebLisOpTime='" + model.WebLisOpTime + "' ");
            }

            if (model.WebLiser != null)
            {
                strSql.Append(" and WebLiser='" + model.WebLiser + "' ");
            }

            if (model.WebLisDescript != null)
            {
                strSql.Append(" and WebLisDescript='" + model.WebLisDescript + "' ");
            }

            if (model.WebLisOrgID != null)
            {
                strSql.Append(" and WebLisOrgID='" + model.WebLisOrgID + "' ");
            }

            if (model.isSpiltItem != null)
            {
                strSql.Append(" and isSpiltItem=" + model.isSpiltItem + " ");
            }

            if (model.WebLisIsReply != null)
            {
                strSql.Append(" and WebLisIsReply=" + model.WebLisIsReply + " ");
            }

            if (model.WebLisReplyDate != null)
            {
                strSql.Append(" and WebLisReplyDate='" + model.WebLisReplyDate + "' ");
            }

            if (model.WebLisSourceOrgId != null)
            {
                strSql.Append(" and WebLisSourceOrgId='" + model.WebLisSourceOrgId + "' ");
            }

            if (model.WebLisUploadTime != null)
            {
                strSql.Append(" and WebLisUploadTime='" + model.WebLisUploadTime + "' ");
            }

            if (model.WebLisUploadStatus != null)
            {
                strSql.Append(" and WebLisUploadStatus=" + model.WebLisUploadStatus + " ");
            }

            if (model.WebLisUploadTestStatus != null)
            {
                strSql.Append(" and WebLisUploadTestStatus=" + model.WebLisUploadTestStatus + " ");
            }

            if (model.WebLisUploader != null)
            {
                strSql.Append(" and WebLisUploader='" + model.WebLisUploader + "' ");
            }

            if (model.WebLisUploadDes != null)
            {
                strSql.Append(" and WebLisUploadDes='" + model.WebLisUploadDes + "' ");
            }

            if (model.WebLisSourceOrgName != null)
            {
                strSql.Append(" and WebLisSourceOrgName='" + model.WebLisSourceOrgName + "' ");
            }

            if (model.ClientNo != null)
            {
                strSql.Append(" and ClientNo='" + model.ClientNo + "' ");
            }

            if (model.IsAffirm != null)
            {
                strSql.Append(" and IsAffirm=" + model.IsAffirm + " ");
            }

            if (model.ClientName != null)
            {
                strSql.Append(" and ClientName='" + model.ClientName + "' ");
            }

            if (model.ReceiveFlag != null)
            {
                strSql.Append(" and ReceiveFlag=" + model.ReceiveFlag + " ");
            }

            if (model.SampleCap != null)
            {
                strSql.Append(" and SampleCap=" + model.SampleCap + " ");
            }

            if (model.ClientHost != null)
            {
                strSql.Append(" and ClientHost='" + model.ClientHost + "' ");
            }
            if (model.WebLisOrgName != null)
            {
                strSql.Append(" and WebLisOrgName='" + model.WebLisOrgName + "' ");
            }
            Common.Log.Log.Info(strSql.ToString());
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
        public DataSet GetListByPage(ZhiFang.Model.BarCodeForm model, int nowPageNum, int nowPageSize)
        {
            throw new NotImplementedException();
        }

        public bool Exists(long BarCodeFormNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from BarCodeForm ");
            strSql.Append(" where BarCodeFormNo ='" + BarCodeFormNo + "'");
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

        public bool CopyToLab(List<string> lst)
        {
            System.Collections.ArrayList arrySql = new System.Collections.ArrayList();
            string LabTableName = "BarCodeForm";
            LabTableName = "B_Lab_" + LabTableName.Substring(LabTableName.IndexOf("_") + 1, LabTableName.Length - (LabTableName.IndexOf("_") + 1));
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSqlControl = new StringBuilder();
            string TableKey = "BarCodeFormNo";
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
                    strSql.Append(" LabBarCodeFormNo , Collecter , CollecterID , CollectDate , CollectTime , refuseUser , refuseopinion , refusereason , refuseTime , signflag , incepter , BarCode , inceptTime , inceptDate , ReceiveMan , ReceiveDate , ReceiveTime , PrintInfo , PrintCount , Dr2Flag , FlagDateDelete , DispenseFlag , SamplingGroupNo , SerialScanTime , BarCodeSource , DeleteFlag , SendOffFlag , SendOffMan , EMSMan , SendOffDate , ReportSignMan , ReportSignDate , RefuseIncepter , IsPrep , RefuseIncepterMemo , ReportFlag , SendOffMemo , SampleTypeNo , SampleSendNo , WebLisFlag , WebLisOpTime , WebLiser , WebLisDescript , WebLisOrgID , isSpiltItem , WebLisIsReply , WebLisReplyDate , WebLisSourceOrgId , WebLisUploadTime , WebLisUploadStatus , WebLisUploadTestStatus , WebLisUploader , WebLisUploadDes , WebLisSourceOrgName , ClientNo , IsAffirm , ClientName , ReceiveFlag , SampleCap , ClientHost ");
                    strSql.Append(") select '" + lst[i].Trim() + "' as LabCode, ");
                    strSql.Append("BarCodeFormNo,Collecter,CollecterID,CollectDate,CollectTime,refuseUser,refuseopinion,refusereason,refuseTime,signflag,incepter,BarCode,inceptTime,inceptDate,ReceiveMan,ReceiveDate,ReceiveTime,PrintInfo,PrintCount,Dr2Flag,FlagDateDelete,DispenseFlag,SamplingGroupNo,SerialScanTime,BarCodeSource,DeleteFlag,SendOffFlag,SendOffMan,EMSMan,SendOffDate,ReportSignMan,ReportSignDate,RefuseIncepter,IsPrep,RefuseIncepterMemo,ReportFlag,SendOffMemo,SampleTypeNo,SampleSendNo,WebLisFlag,WebLisOpTime,WebLiser,WebLisDescript,WebLisOrgID,isSpiltItem,WebLisIsReply,WebLisReplyDate,WebLisSourceOrgId,WebLisUploadTime,WebLisUploadStatus,WebLisUploadTestStatus,WebLisUploader,WebLisUploadDes,WebLisSourceOrgName,ClientNo,IsAffirm,ClientName,ReceiveFlag,SampleCap,ClientHost");
                    strSql.Append(" from BarCodeForm ");

                    strSqlControl.Append("insert into BarCodeFormControl ( ");
                    strSqlControl.Append(" " + TableKeySub + "ControlNo," + TableKey + ",ControlLabNo,Control" + TableKey + ",UseFlag ");
                    strSqlControl.Append(")  select ");
                    strSqlControl.Append("  '" + lst[i].Trim() + "'+'_'+convert(varchar(50)," + TableKey + ")+'_'+convert(varchar(50), " + TableKey + ") as " + TableKeySub + "ControlNo," + TableKey + ",'" + lst[i].Trim() + "' as ControlLabNo," + TableKey + ",UseFlag ");
                    strSqlControl.Append(" from BarCodeForm ");

                    arrySql.Add(strSql.ToString());
                    arrySql.Add(strSqlControl.ToString());

                    strSql = new StringBuilder();
                    strSqlControl = new StringBuilder();

                    //d_log.OperateLog("BarCodeFormControl", "", "", DateTime.Now, 1);
                    //d_log.OperateLog(LabTableName, "", "", DateTime.Now, 1);
                    //d_log.OperateLog("rCodeForm", "", "", DateTime.Now, 1);
                }

                DbHelperSQL.BatchUpdateWithTransaction(arrySql);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("BarCodeFormNo", "BarCodeForm");
        }

        public DataSet GetList(int Top, ZhiFang.Model.BarCodeForm model, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM BarCodeForm ");


            if (model.BarCodeFormNo != null)
            {
                strSql.Append(" and BarCodeFormNo=" + model.BarCodeFormNo + " ");
            }

            if (model.Collecter != null)
            {

                strSql.Append(" and Collecter='" + model.Collecter + "' ");
            }

            if (model.CollecterID != null)
            {
                strSql.Append(" and CollecterID=" + model.CollecterID + " ");
            }

            if (model.CollectDate != null)
            {

                strSql.Append(" and CollectDate='" + model.CollectDate + "' ");
            }

            if (model.CollectTime != null)
            {

                strSql.Append(" and CollectTime='" + model.CollectTime + "' ");
            }

            if (model.refuseUser != null)
            {

                strSql.Append(" and refuseUser='" + model.refuseUser + "' ");
            }

            if (model.refuseopinion != null)
            {

                strSql.Append(" and refuseopinion='" + model.refuseopinion + "' ");
            }

            if (model.refusereason != null)
            {

                strSql.Append(" and refusereason='" + model.refusereason + "' ");
            }

            if (model.refuseTime != null)
            {

                strSql.Append(" and refuseTime='" + model.refuseTime + "' ");
            }

            if (model.signflag != null)
            {
                strSql.Append(" and signflag=" + model.signflag + " ");
            }

            if (model.incepter != null)
            {

                strSql.Append(" and incepter='" + model.incepter + "' ");
            }

            if (model.BarCode != null)
            {

                strSql.Append(" and BarCode='" + model.BarCode + "' ");
            }

            if (model.inceptTime != null)
            {

                strSql.Append(" and inceptTime='" + model.inceptTime + "' ");
            }

            if (model.inceptDate != null)
            {

                strSql.Append(" and inceptDate='" + model.inceptDate + "' ");
            }

            if (model.ReceiveMan != null)
            {

                strSql.Append(" and ReceiveMan='" + model.ReceiveMan + "' ");
            }

            if (model.ReceiveDate != null)
            {

                strSql.Append(" and ReceiveDate='" + model.ReceiveDate + "' ");
            }

            if (model.ReceiveTime != null)
            {

                strSql.Append(" and ReceiveTime='" + model.ReceiveTime + "' ");
            }

            if (model.PrintInfo != null)
            {

                strSql.Append(" and PrintInfo='" + model.PrintInfo + "' ");
            }

            if (model.PrintCount != null)
            {
                strSql.Append(" and PrintCount=" + model.PrintCount + " ");
            }

            if (model.Dr2Flag != null)
            {
                strSql.Append(" and Dr2Flag=" + model.Dr2Flag + " ");
            }

            if (model.FlagDateDelete != null)
            {

                strSql.Append(" and FlagDateDelete='" + model.FlagDateDelete + "' ");
            }

            if (model.DispenseFlag != null)
            {
                strSql.Append(" and DispenseFlag=" + model.DispenseFlag + " ");
            }

            if (model.SamplingGroupNo != null)
            {
                strSql.Append(" and SamplingGroupNo=" + model.SamplingGroupNo + " ");
            }

            if (model.SerialScanTime != null)
            {

                strSql.Append(" and SerialScanTime='" + model.SerialScanTime + "' ");
            }

            if (model.BarCodeSource != null)
            {
                strSql.Append(" and BarCodeSource=" + model.BarCodeSource + " ");
            }

            if (model.DeleteFlag != null)
            {
                strSql.Append(" and DeleteFlag=" + model.DeleteFlag + " ");
            }

            if (model.SendOffFlag != null)
            {
                strSql.Append(" and SendOffFlag=" + model.SendOffFlag + " ");
            }

            if (model.SendOffMan != null)
            {

                strSql.Append(" and SendOffMan='" + model.SendOffMan + "' ");
            }

            if (model.EMSMan != null)
            {

                strSql.Append(" and EMSMan='" + model.EMSMan + "' ");
            }

            if (model.SendOffDate != null)
            {

                strSql.Append(" and SendOffDate='" + model.SendOffDate + "' ");
            }

            if (model.ReportSignMan != null)
            {

                strSql.Append(" and ReportSignMan='" + model.ReportSignMan + "' ");
            }

            if (model.ReportSignDate != null)
            {

                strSql.Append(" and ReportSignDate='" + model.ReportSignDate + "' ");
            }

            if (model.RefuseIncepter != null)
            {

                strSql.Append(" and RefuseIncepter='" + model.RefuseIncepter + "' ");
            }

            if (model.IsPrep != null)
            {
                strSql.Append(" and IsPrep=" + model.IsPrep + " ");
            }

            if (model.RefuseIncepterMemo != null)
            {

                strSql.Append(" and RefuseIncepterMemo='" + model.RefuseIncepterMemo + "' ");
            }

            if (model.ReportFlag != null)
            {
                strSql.Append(" and ReportFlag=" + model.ReportFlag + " ");
            }

            if (model.SendOffMemo != null)
            {

                strSql.Append(" and SendOffMemo='" + model.SendOffMemo + "' ");
            }

            if (model.SampleTypeNo != null)
            {
                strSql.Append(" and SampleTypeNo=" + model.SampleTypeNo + " ");
            }

            if (model.SampleSendNo != null)
            {

                strSql.Append(" and SampleSendNo='" + model.SampleSendNo + "' ");
            }

            if (model.WebLisFlag != null)
            {
                strSql.Append(" and WebLisFlag=" + model.WebLisFlag + " ");
            }

            if (model.WebLisOpTime != null)
            {

                strSql.Append(" and WebLisOpTime='" + model.WebLisOpTime + "' ");
            }

            if (model.WebLiser != null)
            {

                strSql.Append(" and WebLiser='" + model.WebLiser + "' ");
            }

            if (model.WebLisDescript != null)
            {

                strSql.Append(" and WebLisDescript='" + model.WebLisDescript + "' ");
            }

            if (model.WebLisOrgID != null)
            {

                strSql.Append(" and WebLisOrgID='" + model.WebLisOrgID + "' ");
            }

            if (model.isSpiltItem != null)
            {
                strSql.Append(" and isSpiltItem=" + model.isSpiltItem + " ");
            }

            if (model.WebLisIsReply != null)
            {
                strSql.Append(" and WebLisIsReply=" + model.WebLisIsReply + " ");
            }

            if (model.WebLisReplyDate != null)
            {

                strSql.Append(" and WebLisReplyDate='" + model.WebLisReplyDate + "' ");
            }

            if (model.WebLisSourceOrgId != null)
            {

                strSql.Append(" and WebLisSourceOrgId='" + model.WebLisSourceOrgId + "' ");
            }

            if (model.WebLisUploadTime != null)
            {

                strSql.Append(" and WebLisUploadTime='" + model.WebLisUploadTime + "' ");
            }

            if (model.WebLisUploadStatus != null)
            {
                strSql.Append(" and WebLisUploadStatus=" + model.WebLisUploadStatus + " ");
            }

            if (model.WebLisUploadTestStatus != null)
            {
                strSql.Append(" and WebLisUploadTestStatus=" + model.WebLisUploadTestStatus + " ");
            }

            if (model.WebLisUploader != null)
            {

                strSql.Append(" and WebLisUploader='" + model.WebLisUploader + "' ");
            }

            if (model.WebLisUploadDes != null)
            {

                strSql.Append(" and WebLisUploadDes='" + model.WebLisUploadDes + "' ");
            }

            if (model.WebLisSourceOrgName != null)
            {

                strSql.Append(" and WebLisSourceOrgName='" + model.WebLisSourceOrgName + "' ");
            }

            if (model.ClientNo != null)
            {

                strSql.Append(" and ClientNo='" + model.ClientNo + "' ");
            }

            if (model.IsAffirm != null)
            {
                strSql.Append(" and IsAffirm=" + model.IsAffirm + " ");
            }

            if (model.ClientName != null)
            {

                strSql.Append(" and ClientName='" + model.ClientName + "' ");
            }

            if (model.ReceiveFlag != null)
            {
                strSql.Append(" and ReceiveFlag=" + model.ReceiveFlag + " ");
            }

            if (model.SampleCap != null)
            {
                strSql.Append(" and SampleCap=" + model.SampleCap + " ");
            }

            if (model.ClientHost != null)
            {

                strSql.Append(" and ClientHost='" + model.ClientHost + "' ");
            }
            if (model.WebLisOrgName != null)
            {

                strSql.Append(" and WebLisOrgName='" + model.WebLisOrgName + "' ");
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetAllList()
        {
            return GetList("");
        }
        public int AddUpdateByDataSet(DataSet ds)
        {
            throw new NotImplementedException();
        }

        public string GetNewBarCode(string ClientNo)
        {
            string NewBarCode = ClientNo.Trim();

            if (NewBarCode == "")
            {
                return "";
            }
            string tmpcode = DbHelperSQL.GetSingle("SELECT TOP 1 ClientNo FROM CLIENTELE WHERE (CNAME LIKE '" + NewBarCode + "%') ").ToString();
            //条码补位
            //string sql = "SELECT TOP 1 BarCode FROM BarCodeForm WHERE ( BarCode like '" + tmpcode + "%')  and len(barcode)=10 ORDER BY BarCode desc";
            Common.Log.Log.Info("tmpcode原始值:" + tmpcode);
            if (tmpcode.Length == 1)
            {
                tmpcode = "100" + tmpcode;
                Common.Log.Log.Info("tmpcode.Length =1:" + tmpcode);
            }
            else if (tmpcode.Length == 2)
            {
                tmpcode = "10" + tmpcode;
                Common.Log.Log.Info("tmpcode.Length =2:" + tmpcode);
            }
            else
            {
                tmpcode = "1" + tmpcode;
                Common.Log.Log.Info("tmpcode.else:" + tmpcode);
            }

            string sql = "SELECT TOP 1 BarCode FROM BarCodeForm WHERE ( BarCode like '" + tmpcode + "%') ORDER BY BarCode desc";
            Common.Log.Log.Debug("查询语句:" + sql.ToString());
            DataSet dSet = DbHelperSQL.ExecuteDataSet(sql);

            if (dSet.Tables.Count > 0 && dSet.Tables[0].Rows.Count > 0)
            {
                //string BarCodeStr1 = "11021111000005";
                string BarCodeStr = dSet.Tables[0].Rows[0][0].ToString().Substring(4);
                Common.Log.Log.Info("BarCodeStr(Substring()):" + BarCodeStr);
                try
                {
                    int tempBarCode = Convert.ToInt32(BarCodeStr);
                    Common.Log.Log.Info("tempBarCode(Convert()):" + tempBarCode);
                    tempBarCode++;
                    Common.Log.Log.Info("PadLeft:" + tmpcode + tempBarCode.ToString().PadLeft(6, '0'));
                    return tmpcode + tempBarCode.ToString().PadLeft(6, '0');
                }
                catch (Exception ex)
                {
                    Common.Log.Log.Debug("错误信息:" + ex.ToString());
                    return "条码生成错误！";
                }

            }
            else
            {
                //生成新条码号
                tmpcode = tmpcode + "000001";
                return tmpcode;
            }
        }


        public int UpdateByBarCode(Model.BarCodeForm model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BarCodeForm set ");
            strSql.Append(" Collecter = @Collecter , ");
            strSql.Append(" CollecterID = @CollecterID , ");
            strSql.Append(" CollectDate = @CollectDate , ");
            strSql.Append(" CollectTime = @CollectTime , ");
            strSql.Append(" refuseUser = @refuseUser , ");
            strSql.Append(" refuseopinion = @refuseopinion , ");
            strSql.Append(" refusereason = @refusereason , ");
            strSql.Append(" refuseTime = @refuseTime , ");
            strSql.Append(" signflag = @signflag , ");
            strSql.Append(" incepter = @incepter , ");
            strSql.Append(" BarCode = @BarCode , ");
            strSql.Append(" inceptTime = @inceptTime , ");
            strSql.Append(" inceptDate = @inceptDate , ");
            strSql.Append(" ReceiveMan = @ReceiveMan , ");
            strSql.Append(" ReceiveDate = @ReceiveDate , ");
            strSql.Append(" ReceiveTime = @ReceiveTime , ");
            strSql.Append(" PrintInfo = @PrintInfo , ");
            strSql.Append(" PrintCount = @PrintCount , ");
            strSql.Append(" Dr2Flag = @Dr2Flag , ");
            strSql.Append(" FlagDateDelete = @FlagDateDelete , ");
            strSql.Append(" DispenseFlag = @DispenseFlag , ");
            strSql.Append(" SamplingGroupNo = @SamplingGroupNo , ");
            strSql.Append(" SerialScanTime = @SerialScanTime , ");
            strSql.Append(" BarCodeSource = @BarCodeSource , ");
            strSql.Append(" DeleteFlag = @DeleteFlag , ");
            strSql.Append(" SendOffFlag = @SendOffFlag , ");
            strSql.Append(" SendOffMan = @SendOffMan , ");
            strSql.Append(" EMSMan = @EMSMan , ");
            strSql.Append(" SendOffDate = @SendOffDate , ");
            strSql.Append(" ReportSignMan = @ReportSignMan , ");
            strSql.Append(" ReportSignDate = @ReportSignDate , ");
            strSql.Append(" RefuseIncepter = @RefuseIncepter , ");
            strSql.Append(" IsPrep = @IsPrep , ");
            strSql.Append(" RefuseIncepterMemo = @RefuseIncepterMemo , ");
            strSql.Append(" ReportFlag = @ReportFlag , ");
            strSql.Append(" SendOffMemo = @SendOffMemo , ");
            strSql.Append(" SampleTypeNo = @SampleTypeNo , ");
            strSql.Append(" SampleSendNo = @SampleSendNo , ");
            strSql.Append(" WebLisFlag = @WebLisFlag , ");
            strSql.Append(" WebLisOpTime = @WebLisOpTime , ");
            strSql.Append(" WebLiser = @WebLiser , ");
            strSql.Append(" WebLisDescript = @WebLisDescript , ");
            strSql.Append(" WebLisOrgID = @WebLisOrgID , ");
            strSql.Append(" isSpiltItem = @isSpiltItem , ");
            strSql.Append(" WebLisIsReply = @WebLisIsReply , ");
            strSql.Append(" WebLisReplyDate = @WebLisReplyDate , ");
            strSql.Append(" WebLisSourceOrgId = @WebLisSourceOrgId , ");
            strSql.Append(" WebLisUploadTime = @WebLisUploadTime , ");
            strSql.Append(" WebLisUploadStatus = @WebLisUploadStatus , ");
            strSql.Append(" WebLisUploadTestStatus = @WebLisUploadTestStatus , ");
            strSql.Append(" WebLisUploader = @WebLisUploader , ");
            strSql.Append(" WebLisUploadDes = @WebLisUploadDes , ");
            strSql.Append(" WebLisSourceOrgName = @WebLisSourceOrgName , ");
            strSql.Append(" ClientNo = @ClientNo , ");
            strSql.Append(" IsAffirm = @IsAffirm , ");
            strSql.Append(" ClientName = @ClientName , ");
            strSql.Append(" ReceiveFlag = @ReceiveFlag , ");
            strSql.Append(" SampleCap = @SampleCap , ");
            strSql.Append(" ClientHost = @ClientHost,  ");
            strSql.Append(" WebLisOrgName = @WebLisOrgName  ");
            strSql.Append(" where BarCode=@BarCode  ");

            SqlParameter[] parameters = {			               
                           
            new SqlParameter("@Collecter", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@CollecterID", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@CollectDate", SqlDbType.DateTime) ,            	
                           
            new SqlParameter("@CollectTime", SqlDbType.DateTime) ,            	
                           
            new SqlParameter("@refuseUser", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@refuseopinion", SqlDbType.VarChar,100) ,            	
                           
            new SqlParameter("@refusereason", SqlDbType.VarChar,100) ,            	
                           
            new SqlParameter("@refuseTime", SqlDbType.DateTime) ,            	
                           
            new SqlParameter("@signflag", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@incepter", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@BarCode", SqlDbType.VarChar,30) ,            	
                           
            new SqlParameter("@inceptTime", SqlDbType.DateTime) ,            	
                           
            new SqlParameter("@inceptDate", SqlDbType.DateTime) ,            	
                           
            new SqlParameter("@ReceiveMan", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@ReceiveDate", SqlDbType.DateTime) ,            	
                           
            new SqlParameter("@ReceiveTime", SqlDbType.DateTime) ,            	
                           
            new SqlParameter("@PrintInfo", SqlDbType.VarChar,600) ,            	
                           
            new SqlParameter("@PrintCount", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@Dr2Flag", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@FlagDateDelete", SqlDbType.DateTime) ,            	
                           
            new SqlParameter("@DispenseFlag", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@SamplingGroupNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@SerialScanTime", SqlDbType.VarChar,30) ,            	
                           
            new SqlParameter("@BarCodeSource", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@DeleteFlag", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@SendOffFlag", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@SendOffMan", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@EMSMan", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@SendOffDate", SqlDbType.DateTime) ,            	
                           
            new SqlParameter("@ReportSignMan", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@ReportSignDate", SqlDbType.DateTime) ,            	
                           
            new SqlParameter("@RefuseIncepter", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@IsPrep", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@RefuseIncepterMemo", SqlDbType.VarChar,200) ,            	
                           
            new SqlParameter("@ReportFlag", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@SendOffMemo", SqlDbType.VarChar,200) ,            	
                           
            new SqlParameter("@SampleTypeNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@SampleSendNo", SqlDbType.VarChar,30) ,            	
                           
            new SqlParameter("@WebLisFlag", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@WebLisOpTime", SqlDbType.DateTime) ,            	
                           
            new SqlParameter("@WebLiser", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@WebLisDescript", SqlDbType.VarChar,500) ,            	
                           
            new SqlParameter("@WebLisOrgID", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@isSpiltItem", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@WebLisIsReply", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@WebLisReplyDate", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@WebLisSourceOrgId", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@WebLisUploadTime", SqlDbType.DateTime) ,            	
                           
            new SqlParameter("@WebLisUploadStatus", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@WebLisUploadTestStatus", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@WebLisUploader", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@WebLisUploadDes", SqlDbType.VarChar,500) ,            	
                           
            new SqlParameter("@WebLisSourceOrgName", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@ClientNo", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@IsAffirm", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@ClientName", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@ReceiveFlag", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@SampleCap", SqlDbType.Float,8) ,            	
                           
            new SqlParameter("@ClientHost", SqlDbType.VarChar,60) ,            	
            
            new SqlParameter("@WebLisOrgName", SqlDbType.VarChar,150) 

            };
            if (model.Collecter != null)
            {
                parameters[0].Value = model.Collecter;
            }

            if (model.CollecterID != null)
            {
                parameters[1].Value = model.CollecterID;
            }

            if (model.CollectDate != null)
            {
                parameters[2].Value = model.CollectDate;
            }

            if (model.CollectTime != null)
            {
                parameters[3].Value = model.CollectTime;
            }

            if (model.refuseUser != null)
            {
                parameters[4].Value = model.refuseUser;
            }

            if (model.refuseopinion != null)
            {
                parameters[5].Value = model.refuseopinion;
            }

            if (model.refusereason != null)
            {
                parameters[6].Value = model.refusereason;
            }

            if (model.refuseTime != null)
            {
                parameters[7].Value = model.refuseTime;
            }

            if (model.signflag != null)
            {
                parameters[8].Value = model.signflag;
            }

            if (model.incepter != null)
            {
                parameters[9].Value = model.incepter;
            }

            if (model.BarCode != null)
            {
                parameters[10].Value = model.BarCode;
            }

            if (model.inceptTime != null)
            {
                parameters[11].Value = model.inceptTime;
            }

            if (model.inceptDate != null)
            {
                parameters[12].Value = model.inceptDate;
            }

            if (model.ReceiveMan != null)
            {
                parameters[13].Value = model.ReceiveMan;
            }

            if (model.ReceiveDate != null)
            {
                parameters[14].Value = model.ReceiveDate;
            }

            if (model.ReceiveTime != null)
            {
                parameters[15].Value = model.ReceiveTime;
            }

            if (model.PrintInfo != null)
            {
                parameters[16].Value = model.PrintInfo;
            }

            if (model.PrintCount != null)
            {
                parameters[17].Value = model.PrintCount;
            }

            if (model.Dr2Flag != null)
            {
                parameters[18].Value = model.Dr2Flag;
            }

            if (model.FlagDateDelete != null)
            {
                parameters[19].Value = model.FlagDateDelete;
            }

            if (model.DispenseFlag != null)
            {
                parameters[20].Value = model.DispenseFlag;
            }

            if (model.SamplingGroupNo != null)
            {
                parameters[21].Value = model.SamplingGroupNo;
            }

            if (model.SerialScanTime != null)
            {
                parameters[22].Value = model.SerialScanTime;
            }

            if (model.BarCodeSource != null)
            {
                parameters[23].Value = model.BarCodeSource;
            }

            if (model.DeleteFlag != null)
            {
                parameters[24].Value = model.DeleteFlag;
            }

            if (model.SendOffFlag != null)
            {
                parameters[25].Value = model.SendOffFlag;
            }

            if (model.SendOffMan != null)
            {
                parameters[26].Value = model.SendOffMan;
            }

            if (model.EMSMan != null)
            {
                parameters[27].Value = model.EMSMan;
            }

            if (model.SendOffDate != null)
            {
                parameters[28].Value = model.SendOffDate;
            }

            if (model.ReportSignMan != null)
            {
                parameters[29].Value = model.ReportSignMan;
            }

            if (model.ReportSignDate != null)
            {
                parameters[30].Value = model.ReportSignDate;
            }

            if (model.RefuseIncepter != null)
            {
                parameters[31].Value = model.RefuseIncepter;
            }

            if (model.IsPrep != null)
            {
                parameters[32].Value = model.IsPrep;
            }

            if (model.RefuseIncepterMemo != null)
            {
                parameters[33].Value = model.RefuseIncepterMemo;
            }

            if (model.ReportFlag != null)
            {
                parameters[34].Value = model.ReportFlag;
            }

            if (model.SendOffMemo != null)
            {
                parameters[35].Value = model.SendOffMemo;
            }

            if (model.SampleTypeNo != null)
            {
                parameters[36].Value = model.SampleTypeNo;
            }

            if (model.SampleSendNo != null)
            {
                parameters[37].Value = model.SampleSendNo;
            }

            if (model.WebLisFlag != null)
            {
                parameters[38].Value = model.WebLisFlag;
            }

            if (model.WebLisOpTime != null)
            {
                parameters[39].Value = model.WebLisOpTime;
            }

            if (model.WebLiser != null)
            {
                parameters[40].Value = model.WebLiser;
            }

            if (model.WebLisDescript != null)
            {
                parameters[41].Value = model.WebLisDescript;
            }

            if (model.WebLisOrgID != null)
            {
                parameters[42].Value = model.WebLisOrgID;
            }

            if (model.isSpiltItem != null)
            {
                parameters[43].Value = model.isSpiltItem;
            }

            if (model.WebLisIsReply != null)
            {
                parameters[44].Value = model.WebLisIsReply;
            }

            if (model.WebLisReplyDate != null)
            {
                parameters[45].Value = model.WebLisReplyDate;
            }

            if (model.WebLisSourceOrgId != null)
            {
                parameters[46].Value = model.WebLisSourceOrgId;
            }

            if (model.WebLisUploadTime != null)
            {
                parameters[47].Value = model.WebLisUploadTime;
            }

            if (model.WebLisUploadStatus != null)
            {
                parameters[48].Value = model.WebLisUploadStatus;
            }

            if (model.WebLisUploadTestStatus != null)
            {
                parameters[49].Value = model.WebLisUploadTestStatus;
            }

            if (model.WebLisUploader != null)
            {
                parameters[50].Value = model.WebLisUploader;
            }

            if (model.WebLisUploadDes != null)
            {
                parameters[51].Value = model.WebLisUploadDes;
            }

            if (model.WebLisSourceOrgName != null)
            {
                parameters[52].Value = model.WebLisSourceOrgName;
            }

            if (model.ClientNo != null)
            {
                parameters[53].Value = model.ClientNo;
            }

            if (model.IsAffirm != null)
            {
                parameters[54].Value = model.IsAffirm;
            }

            if (model.ClientName != null)
            {
                parameters[55].Value = model.ClientName;
            }

            if (model.ReceiveFlag != null)
            {
                parameters[56].Value = model.ReceiveFlag;
            }

            if (model.SampleCap != null)
            {
                parameters[57].Value = model.SampleCap;
            }

            if (model.ClientHost != null)
            {
                parameters[58].Value = model.ClientHost;
            }
            if (model.WebLisOrgName != null)
            {
                parameters[59].Value = model.WebLisOrgName;
            }
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);
        }


        public int UpdatePrintFlag(Model.BarCodeForm model)
        {
            string strSql = "update  BarCodeForm set PrintCount='" + model.PrintCount + "' where BarCode='" + model.BarCode + "'";
            Common.Log.Log.Info("修改打印标记:" + strSql.ToString());
            return DbHelperSQL.ExecuteNonQuery(strSql);
        }
        public int UpdateByBarCodeFormNo(Model.BarCodeForm barCode)
        {
            string strSql = "update  BarCodeForm set WebLisOrgID='" + barCode.WebLisOrgID + "' where BarCodeFormNo=" + barCode.BarCodeFormNo;
            return DbHelperSQL.ExecuteNonQuery(strSql);
        }

        public int UpdateByOrderNo(Model.BarCodeForm barCode)
        {
            string strSql = "update  BarCodeForm set OrderNo=null where OrderNo='" + barCode.OrderNo + "'";
            return DbHelperSQL.ExecuteNonQuery(strSql);
        }
        public DataSet GetBarCodeView(string BarCode)
        {
            string strSql = "Select*from BarCodePrintView where BarCode= '" + BarCode + "'";
            return DbHelperSQL.ExecuteDataSet(strSql);
        }

        public int UpdateOrderNoByBarCodeFormNo(string BarCodeFormNo, string OrderNo)
        {
            string strSql = " update  BarCodeForm set OrderNo='" + OrderNo + "' where BarCodeFormNo='" + BarCodeFormNo + "'";
            return DbHelperSQL.ExecuteNonQuery(strSql);
        }

        public int UpdateWebLisFlagByBarCode(string WebLisFlag, string BarCode, string WebLisOrgID)
        {
            string strSql = " update  BarCodeForm set WebLisFlag=" + WebLisFlag + ",WebLisOrgID='" + WebLisOrgID + "' where BarCode='" + BarCode + "'";
            return DbHelperSQL.ExecuteNonQuery(strSql);
        }

        public int UpdateByList(List<string> lisStrColumn, List<string> lisStrData)
        {
            throw new NotImplementedException();
        }


        public int AddByList(List<string> lisStrColumn, List<string> lisStrData)
        {

            string strSql = "insert into BarCodeForm (";
            for (int i = 0; i < lisStrColumn.Count; i++)
            {
                strSql += lisStrColumn[i].ToString() + ",";
            }
            strSql = strSql.Remove(strSql.Length - 1);
            strSql += ") values( ";
            for (int h = 0; h < lisStrData.Count; h++)
            {
                strSql += "'" + lisStrData[h].ToString() + "',";
            }
            strSql = strSql.Remove(strSql.Length - 1);
            strSql += ")";
            return DbHelperSQL.ExecuteNonQuery(strSql);
        }


        public DataSet GetListByNRequestFormNo(long NRequestFormNo)
        {
            DataSet ds = DbHelperSQL.ExecuteDataSet("SELECT    dbo.BarCodeForm.BarCode,dbo.BarCodeForm.BarCodeFormNo,dbo.BarCodeForm.weblisflag FROM dbo.BarCodeForm INNER JOIN dbo.NRequestItem ON dbo.BarCodeForm.BarCodeFormNo = dbo.NRequestItem.BarCodeFormNo where NRequestFormNo=" + NRequestFormNo.ToString());
            return ds;
        }


        public int DeleteList_ByNRequestFormNo(long NRequestFormNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from BarCodeForm ");
            strSql.Append(" where BarCodeForm.BarCodeFormNo in( SELECT dbo.BarCodeForm.BarCodeFormNo FROM dbo.BarCodeForm INNER JOIN dbo.NRequestItem ON dbo.BarCodeForm.BarCodeFormNo = dbo.NRequestItem.BarCodeFormNo where NRequestItem.NRequestFormNo=" + NRequestFormNo + " )");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
        }


        /// <summary>
        /// 根据订单号获取条码信息
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public DataSet GetBarCodeByOrderNo(string OrderNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.CName, c.BarCode,c.ReceiveDate,c.CollectDate,a.OperDate,c.BarCodeFormNo, ");
            strSql.Append(" a.NRequestFormNo from NRequestForm a ");
            strSql.Append(" join NRequestItem b on a.NRequestFormNo=b.NRequestFormNo ");
            strSql.Append(" join BarCodeForm c on b.BarCodeFormNo=c.BarCodeFormNo where c.OrderNo= ");
            strSql.Append(" '"+OrderNo+"' ");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 这个条码是否被其它订单使用
        /// </summary>
        /// <param name="BarCode"></param>
        /// <returns></returns>
        public bool OtherOrderUser(string BarCode)
        {
            bool f = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT OrderNo FROM BarCodeForm where BarCode='" + BarCode + "'");
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["OrderNo"] != null && ds.Tables[0].Rows[0]["OrderNo"].ToString() != "")
                {
                    f = true;
                }
                else
                    f = false;
            }

            return f;
        }

        /// <summary>
        ///把记录复制到拒收表
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public int Add(string strSql)
        {
            return DbHelperSQL.ExecuteNonQuery(strSql);
        }

        //查询被拒收申请单的条码信息 ganwh add 2015-10-7
        public DataSet GetRefuseList(string strSql)
        {
            return DbHelperSQL.ExecuteDataSet(strSql);
        }

        public int SendBarCodeFromByBarCodeList(string barCodeList, string userId, string employeeName)
        {
            StringBuilder strwhere = new StringBuilder();
            strwhere.Append(" update barcodeform set SenderName='" + employeeName + "' , SenderID=" + userId + " , SendDateTime ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ,weblisflag=1");
            strwhere.Append(" where BarCode in ('" + barCodeList.Replace(",", "','") + "') and (weblisflag=0 or weblisflag is null ) ");
            ZhiFang.Common.Log.Log.Info("SendBarCodeFromByBarCodeList.barcodeform.sql:" + strwhere.ToString());
            string strCount = DbHelperSQL.ExecuteScalar(strwhere.ToString());
            if (strCount != null && strCount.Trim() != "")
            {
                return Convert.ToInt32(strCount.Trim());
            }
            else
            {
                return 0;
            }
        }

        public int DeliveryBarCodeFromByBarCodeList(string barCodeList, string userId, string employeeName, bool flag, string reason)
        {
            StringBuilder strwhere = new StringBuilder();
            strwhere.Append(" update barcodeform  set weblisflag=2, DeliveryerName='" + employeeName + "' , DeliveryerID=" + userId + " , DeliveryDateTime ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            strwhere.Append(" where BarCode in ('" + barCodeList.Replace(",", "','") + "')  and (weblisflag=1 ) ");
            ZhiFang.Common.Log.Log.Info("DeliveryBarCodeFromByBarCodeList.barcodeform.sql:" + strwhere.ToString());
            string strCount = DbHelperSQL.ExecuteScalar(strwhere.ToString());
            if (strCount != null && strCount.Trim() != "")
            {
                return Convert.ToInt32(strCount.Trim());
            }
            else
            {
                return 0;
            }
        }

        public int ReceiveBarCodeFromByBarCodeList(string barCodeList, string userId, string employeeName, bool flag, string reason, string weblisorgid, string weblisorgname)
        {
            StringBuilder strwhere = new StringBuilder();
            string weblisflag = flag ? "5" : "6";
            string strreason = "";
            if (!string.IsNullOrWhiteSpace(reason))
            {
                strreason = reason;
            }
            strwhere.Append(" update barcodeform  set weblisflag=" + weblisflag + ", ReceipientName='" + employeeName + "' , ReceipientID=" + userId + " , ReceiveDateTime ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ,RejectionReason='" + strreason + "'  ,WebLisOrgID='" + weblisorgid + "'  ,WebLisOrgName='" + weblisorgname + "' ");
            strwhere.Append(" where BarCode in ('" + barCodeList.Replace(",", "','") + "') and (weblisflag=2 ) ");
            ZhiFang.Common.Log.Log.Info("ReceiveBarCodeFromByBarCodeList.barcodeform.sql:" + strwhere.ToString());
            string strCount = DbHelperSQL.ExecuteScalar(strwhere.ToString());
            if (strCount != null && strCount.Trim() != "")
            {
                return Convert.ToInt32(strCount.Trim());
            }
            else
            {
                return 0;
            }
        }
    }
}

