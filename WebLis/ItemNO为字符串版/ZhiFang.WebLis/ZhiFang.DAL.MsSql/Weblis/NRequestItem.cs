using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
using ZhiFang.DAL.MsSql;
namespace ZhiFang.DAL.MsSql.Weblis
{
    //equestItem		
    public partial class NRequestItem : BaseDALLisDB, IDNRequestItem
    {
        public NRequestItem(string dbsourceconn)
        {
            DbHelperSQL = ZhiFang.DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public NRequestItem()
        {
            DbHelperSQL = ZhiFang.DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.WebLisDB());
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.NRequestItem model)
        {
            
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into NRequestItem(");
            strSql.Append("HISCharge,ItemCharge,SampleTypeNo,zdy1,zdy2,SerialNo,zdy3,zdy4,zdy5,DeleteFlag,ItemSource,OldSerialNo,CountNodesItemSource,ReportFlag,PartFlag,WebLisOrgID,WebLisSourceOrgID,WebLisSourceOrgName,ClientNo,ClientName,NRequestFormNo,BarCodeFormNo,FormNo,TollItemNo,ParItemNo,IsCheckFee,ReceiveFlag,CombiItemNo,SampleType,CheckType,CheckTypeName,price ");
            strSql.Append(") values (");
            strSql.Append("@HISCharge,@ItemCharge,@SampleTypeNo,@zdy1,@zdy2,@SerialNo,@zdy3,@zdy4,@zdy5,@DeleteFlag,@ItemSource,@OldSerialNo,@CountNodesItemSource,@ReportFlag,@PartFlag,@WebLisOrgID,@WebLisSourceOrgID,@WebLisSourceOrgName,@ClientNo,@ClientName,@NRequestFormNo,@BarCodeFormNo,@FormNo,@TollItemNo,@ParItemNo,@IsCheckFee,@ReceiveFlag,@CombiItemNo,@SampleType,@CheckType,@CheckTypeName,@price");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@HISCharge", SqlDbType.Money,8) ,            
                        new SqlParameter("@ItemCharge", SqlDbType.Money,8) ,            
                        new SqlParameter("@SampleTypeNo", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@zdy1", SqlDbType.VarChar,80) ,            
                        new SqlParameter("@zdy2", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@SerialNo", SqlDbType.NChar,30) ,            
                        new SqlParameter("@zdy3", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@zdy4", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@zdy5", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@DeleteFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@ItemSource", SqlDbType.Int,4) ,            
                        new SqlParameter("@OldSerialNo", SqlDbType.VarChar,60) ,            
                        new SqlParameter("@CountNodesItemSource", SqlDbType.Char,1) ,            
                        new SqlParameter("@ReportFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@PartFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@WebLisOrgID", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@WebLisSourceOrgID", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@WebLisSourceOrgName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ClientNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ClientName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@NRequestFormNo", SqlDbType.BigInt,8) ,            
                        new SqlParameter("@BarCodeFormNo", SqlDbType.BigInt,8) ,            
                        new SqlParameter("@FormNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@TollItemNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@ParItemNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@IsCheckFee", SqlDbType.Int,4) ,            
                        new SqlParameter("@ReceiveFlag", SqlDbType.Int,4),
                        new SqlParameter("@CombiItemNo", SqlDbType.VarChar,50),  
               new SqlParameter("@SampleType", SqlDbType.VarChar,10),  
                new SqlParameter("@CheckType", SqlDbType.VarChar,20),  
                 new SqlParameter("@CheckTypeName", SqlDbType.VarChar,40),
                 new SqlParameter("@price",SqlDbType.Decimal)
               
            };

            parameters[0].Value = model.HISCharge;
            parameters[1].Value = model.ItemCharge;
            parameters[2].Value = model.SampleTypeNo;
            parameters[3].Value = model.zdy1;
            parameters[4].Value = model.zdy2;
            parameters[5].Value = model.SerialNo;
            parameters[6].Value = model.zdy3;
            parameters[7].Value = model.zdy4;
            parameters[8].Value = model.zdy5;
            parameters[9].Value = model.DeleteFlag;
            parameters[10].Value = model.ItemSource;
            parameters[11].Value = model.OldSerialNo;
            parameters[12].Value = model.CountNodesItemSource;
            parameters[13].Value = model.ReportFlag;
            parameters[14].Value = model.PartFlag;
            parameters[15].Value = model.WebLisOrgID;
            parameters[16].Value = model.WebLisSourceOrgID;
            parameters[17].Value = model.WebLisSourceOrgName;
            parameters[18].Value = model.ClientNo;
            parameters[19].Value = model.ClientName;
            parameters[20].Value = model.NRequestFormNo;
            parameters[21].Value = model.BarCodeFormNo;
            parameters[22].Value = model.FormNo;
            parameters[23].Value = model.TollItemNo;
            string ParItemNo= (model.ParItemNo == "" || model.ParItemNo == null) ? "0" : model.ParItemNo;
            parameters[24].Value = ParItemNo;
            parameters[25].Value = model.IsCheckFee;
            parameters[26].Value = model.ReceiveFlag;
            parameters[27].Value = model.CombiItemNo;

            parameters[28].Value = model.SampleType;
            parameters[29].Value = model.CheckType;
            parameters[30].Value = model.CheckTypeName;
            parameters[31].Value = model.Price;
           
            Common.Log.Log.Info(strSql.ToString());
            StringBuilder builder = new StringBuilder();
            builder.Append("插入NRequestItem的数据：");
            for (int i = 0; i < parameters.Length; i++)
            {
               if(parameters[i].Value!=null && parameters[i].Value.ToString()!="")
                builder.Append(parameters[i].ParameterName+"="+ parameters[i].Value + ",");
            }
            
            Common.Log.Log.Info(builder.ToString());
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 十堰太和 申请单录入 组套项目不需要对照 ganwh add 2015-10-27
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add_TaiHe(ZhiFang.Model.NRequestItem model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into NRequestItem(");
            strSql.Append("HISCharge,ItemCharge,SampleTypeNo,zdy1,zdy2,SerialNo,zdy3,zdy4,zdy5,DeleteFlag,ItemSource,OldSerialNo,CountNodesItemSource,ReportFlag,PartFlag,WebLisOrgID,WebLisSourceOrgID,WebLisSourceOrgName,ClientNo,ClientName,NRequestFormNo,BarCodeFormNo,FormNo,TollItemNo,ParItemNo,IsCheckFee,ReceiveFlag,CombiItemNo,SampleType,CheckType,CheckTypeName,price,LabCombiItemNo,LabParItemNo ");
            strSql.Append(") values (");
            strSql.Append("@HISCharge,@ItemCharge,@SampleTypeNo,@zdy1,@zdy2,@SerialNo,@zdy3,@zdy4,@zdy5,@DeleteFlag,@ItemSource,@OldSerialNo,@CountNodesItemSource,@ReportFlag,@PartFlag,@WebLisOrgID,@WebLisSourceOrgID,@WebLisSourceOrgName,@ClientNo,@ClientName,@NRequestFormNo,@BarCodeFormNo,@FormNo,@TollItemNo,@ParItemNo,@IsCheckFee,@ReceiveFlag,@CombiItemNo,@SampleType,@CheckType,@CheckTypeName,@price,@LabCombiItemNo,@LabParItemNo");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@HISCharge", SqlDbType.Money,8) ,            
                        new SqlParameter("@ItemCharge", SqlDbType.Money,8) ,            
                        new SqlParameter("@SampleTypeNo", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@zdy1", SqlDbType.VarChar,80) ,            
                        new SqlParameter("@zdy2", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@SerialNo", SqlDbType.NChar,30) ,            
                        new SqlParameter("@zdy3", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@zdy4", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@zdy5", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@DeleteFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@ItemSource", SqlDbType.Int,4) ,            
                        new SqlParameter("@OldSerialNo", SqlDbType.VarChar,60) ,            
                        new SqlParameter("@CountNodesItemSource", SqlDbType.Char,1) ,            
                        new SqlParameter("@ReportFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@PartFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@WebLisOrgID", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@WebLisSourceOrgID", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@WebLisSourceOrgName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ClientNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ClientName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@NRequestFormNo", SqlDbType.BigInt,8) ,            
                        new SqlParameter("@BarCodeFormNo", SqlDbType.BigInt,8) ,            
                        new SqlParameter("@FormNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@TollItemNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@ParItemNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@IsCheckFee", SqlDbType.Int,4) ,            
                        new SqlParameter("@ReceiveFlag", SqlDbType.Int,4),
                        new SqlParameter("@CombiItemNo", SqlDbType.VarChar,50),  
               new SqlParameter("@SampleType", SqlDbType.VarChar,10),  
                new SqlParameter("@CheckType", SqlDbType.VarChar,20),  
                 new SqlParameter("@CheckTypeName", SqlDbType.VarChar,40),
                 new SqlParameter("@price",SqlDbType.Decimal),
                  new SqlParameter("@LabCombiItemNo",SqlDbType.Int,10),
                   new SqlParameter("@LabParItemNo",SqlDbType.VarChar,50)
            };

            parameters[0].Value = model.HISCharge;
            parameters[1].Value = model.ItemCharge;
            parameters[2].Value = model.SampleTypeNo;
            parameters[3].Value = model.zdy1;
            parameters[4].Value = model.zdy2;
            parameters[5].Value = model.SerialNo;
            parameters[6].Value = model.zdy3;
            parameters[7].Value = model.zdy4;
            parameters[8].Value = model.zdy5;
            parameters[9].Value = model.DeleteFlag;
            parameters[10].Value = model.ItemSource;
            parameters[11].Value = model.OldSerialNo;
            parameters[12].Value = model.CountNodesItemSource;
            parameters[13].Value = model.ReportFlag;
            parameters[14].Value = model.PartFlag;
            parameters[15].Value = model.WebLisOrgID;
            parameters[16].Value = model.WebLisSourceOrgID;
            parameters[17].Value = model.WebLisSourceOrgName;
            parameters[18].Value = model.ClientNo;
            parameters[19].Value = model.ClientName;
            parameters[20].Value = model.NRequestFormNo;
            parameters[21].Value = model.BarCodeFormNo;
            parameters[22].Value = model.FormNo;
            parameters[23].Value = model.TollItemNo;
            parameters[24].Value = (model.ParItemNo == "" || model.ParItemNo == null) ? "0" : model.ParItemNo;
            parameters[25].Value = model.IsCheckFee;
            parameters[26].Value = model.ReceiveFlag;
            parameters[27].Value = model.CombiItemNo;

            parameters[28].Value = model.SampleType;
            parameters[29].Value = model.CheckType;
            parameters[30].Value = model.CheckTypeName;
            parameters[31].Value = model.Price;
            parameters[32].Value = model.LabCombiItemNo;
            parameters[33].Value = model.LabParItemNo;

            Common.Log.Log.Info(strSql.ToString());
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.NRequestItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update NRequestItem set ");
            strSql.Append(" HISCharge = @HISCharge , ");
            strSql.Append(" ItemCharge = @ItemCharge , ");
            strSql.Append(" SampleTypeNo = @SampleTypeNo , ");
            strSql.Append(" zdy1 = @zdy1 , ");
            strSql.Append(" zdy2 = @zdy2 , ");
            strSql.Append(" SerialNo = @SerialNo , ");
            strSql.Append(" zdy3 = @zdy3 , ");
            strSql.Append(" zdy4 = @zdy4 , ");
            strSql.Append(" zdy5 = @zdy5 , ");
            strSql.Append(" DeleteFlag = @DeleteFlag , ");
            strSql.Append(" ItemSource = @ItemSource , ");
            strSql.Append(" OldSerialNo = @OldSerialNo , ");
            strSql.Append(" CountNodesItemSource = @CountNodesItemSource , ");
            strSql.Append(" ReportFlag = @ReportFlag , ");
            strSql.Append(" PartFlag = @PartFlag , ");
            strSql.Append(" WebLisOrgID = @WebLisOrgID , ");
            strSql.Append(" WebLisSourceOrgID = @WebLisSourceOrgID , ");
            strSql.Append(" WebLisSourceOrgName = @WebLisSourceOrgName , ");
            strSql.Append(" ClientNo = @ClientNo , ");
            strSql.Append(" ClientName = @ClientName , ");
            strSql.Append(" NRequestFormNo = @NRequestFormNo , ");
            strSql.Append(" BarCodeFormNo = @BarCodeFormNo , ");
            strSql.Append(" FormNo = @FormNo , ");
            strSql.Append(" TollItemNo = @TollItemNo , ");
            strSql.Append(" ParItemNo = @ParItemNo , ");
            strSql.Append(" IsCheckFee = @IsCheckFee , ");
            strSql.Append(" ReceiveFlag = @ReceiveFlag,  ");
            strSql.Append(" CombiItemNo = @CombiItemNo,  ");
            strSql.Append(" SampleType = @SampleType,  ");
            strSql.Append(" CheckType = @CheckType,  ");
            strSql.Append(" CheckTypeName = @CheckTypeName , ");
            strSql.Append(" price = @price  ");
            strSql.Append(" where NRequestItemNo=@NRequestItemNo ");

            SqlParameter[] parameters = {
			            new SqlParameter("@NRequestItemNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@HISCharge", SqlDbType.Money,8) ,            
                        new SqlParameter("@ItemCharge", SqlDbType.Money,8) ,            
                        new SqlParameter("@SampleTypeNo", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@zdy1", SqlDbType.VarChar,80) ,            
                        new SqlParameter("@zdy2", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@SerialNo", SqlDbType.NChar,30) ,            
                        new SqlParameter("@zdy3", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@zdy4", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@zdy5", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@DeleteFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@ItemSource", SqlDbType.Int,4) ,            
                        new SqlParameter("@OldSerialNo", SqlDbType.VarChar,60) ,            
                        new SqlParameter("@CountNodesItemSource", SqlDbType.Char,1) ,            
                        new SqlParameter("@ReportFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@PartFlag", SqlDbType.Int,4) , 
                        new SqlParameter("@WebLisOrgID", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@WebLisSourceOrgID", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@WebLisSourceOrgName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ClientNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ClientName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@NRequestFormNo", SqlDbType.BigInt,8) ,            
                        new SqlParameter("@BarCodeFormNo", SqlDbType.BigInt,8) ,            
                        new SqlParameter("@FormNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@TollItemNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@ParItemNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@IsCheckFee", SqlDbType.Int,4) ,            
                        new SqlParameter("@ReceiveFlag", SqlDbType.Int,4),         
                        new SqlParameter("@CombiItemNo", SqlDbType.VarChar,50),
                        new SqlParameter("@SampleType", SqlDbType.VarChar,10),  
                new SqlParameter("@CheckType", SqlDbType.VarChar,20),  
                new SqlParameter("@CheckTypeName", SqlDbType.VarChar,40),
                 new SqlParameter("@price", SqlDbType.Decimal)
                
              
            };
            if (model.NRequestItemNo != null)
            {
                parameters[0].Value = model.NRequestItemNo;
            }
            if (model.HISCharge != null)
            {
                parameters[1].Value = model.HISCharge;
            }
            if (model.ItemCharge != null)
            {
                parameters[2].Value = model.ItemCharge;
            }
            if (model.SampleTypeNo != null)
            {
                parameters[3].Value = model.SampleTypeNo;
            }
            if (model.zdy1 != null)
            {
                parameters[4].Value = model.zdy1;
            }
            if (model.zdy2 != null)
            {
                parameters[5].Value = model.zdy2;
            }
            if (model.SerialNo != null)
            {
                parameters[6].Value = model.SerialNo;
            }
            if (model.zdy3 != null)
            {
                parameters[7].Value = model.zdy3;
            }
            if (model.zdy4 != null)
            {
                parameters[8].Value = model.zdy4;
            }
            if (model.zdy5 != null)
            {
                parameters[9].Value = model.zdy5;
            }
            if (model.DeleteFlag != null)
            {
                parameters[10].Value = model.DeleteFlag;
            }
            if (model.ItemSource != null)
            {
                parameters[11].Value = model.ItemSource;
            }
            if (model.OldSerialNo != null)
            {
                parameters[12].Value = model.OldSerialNo;
            }
            if (model.CountNodesItemSource != null)
            {
                parameters[13].Value = model.CountNodesItemSource;
            }
            if (model.ReportFlag != null)
            {
                parameters[14].Value = model.ReportFlag;
            }
            if (model.PartFlag != null)
            {
                parameters[15].Value = model.PartFlag;
            }
            if (model.WebLisOrgID != null)
            {
                parameters[16].Value = model.WebLisOrgID;
            }
            if (model.WebLisSourceOrgID != null)
            {
                parameters[17].Value = model.WebLisSourceOrgID;
            }
            if (model.WebLisSourceOrgName != null)
            {
                parameters[18].Value = model.WebLisSourceOrgName;
            }
            if (model.ClientNo != null)
            {
                parameters[19].Value = model.ClientNo;
            }
            if (model.ClientName != null)
            {
                parameters[20].Value = model.ClientName;
            }
            if (model.NRequestFormNo != null)
            {
                parameters[21].Value = model.NRequestFormNo;
            }
            if (model.BarCodeFormNo != null)
            {
                parameters[22].Value = model.BarCodeFormNo;
            }
            if (model.FormNo != null)
            {
                parameters[23].Value = model.FormNo;
            }
            if (model.TollItemNo != null)
            {
                parameters[24].Value = model.TollItemNo;
            }
            if (model.ParItemNo != null)
            {
                parameters[25].Value = model.ParItemNo;
            }
            if (model.IsCheckFee != null)
            {
                parameters[26].Value = model.IsCheckFee;
            }
            if (model.ReceiveFlag != null)
            {
                parameters[27].Value = model.ReceiveFlag;
            }
            if (model.CombiItemNo != null)
            {
                parameters[28].Value = model.CombiItemNo;
            }
            if (model.SampleType != null)
            {
                parameters[29].Value = model.SampleType;
            }
            if (model.CheckType != null)
            {
                parameters[30].Value = model.CheckType;
            }
            if (model.CheckTypeName != null)
            {
                parameters[31].Value = model.CheckTypeName;
            }
            if (model.Price != null)
                parameters[32].Value = model.Price;
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 十堰太和 申请单录入 组套项目不需要对照 ganwh add 2015-10-27
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update_TaiHe(ZhiFang.Model.NRequestItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update NRequestItem set ");
            strSql.Append(" HISCharge = @HISCharge , ");
            strSql.Append(" ItemCharge = @ItemCharge , ");
            strSql.Append(" SampleTypeNo = @SampleTypeNo , ");
            strSql.Append(" zdy1 = @zdy1 , ");
            strSql.Append(" zdy2 = @zdy2 , ");
            strSql.Append(" SerialNo = @SerialNo , ");
            strSql.Append(" zdy3 = @zdy3 , ");
            strSql.Append(" zdy4 = @zdy4 , ");
            strSql.Append(" zdy5 = @zdy5 , ");
            strSql.Append(" DeleteFlag = @DeleteFlag , ");
            strSql.Append(" ItemSource = @ItemSource , ");
            strSql.Append(" OldSerialNo = @OldSerialNo , ");
            strSql.Append(" CountNodesItemSource = @CountNodesItemSource , ");
            strSql.Append(" ReportFlag = @ReportFlag , ");
            strSql.Append(" PartFlag = @PartFlag , ");
            strSql.Append(" WebLisOrgID = @WebLisOrgID , ");
            strSql.Append(" WebLisSourceOrgID = @WebLisSourceOrgID , ");
            strSql.Append(" WebLisSourceOrgName = @WebLisSourceOrgName , ");
            strSql.Append(" ClientNo = @ClientNo , ");
            strSql.Append(" ClientName = @ClientName , ");
            strSql.Append(" NRequestFormNo = @NRequestFormNo , ");
            strSql.Append(" BarCodeFormNo = @BarCodeFormNo , ");
            strSql.Append(" FormNo = @FormNo , ");
            strSql.Append(" TollItemNo = @TollItemNo , ");
            strSql.Append(" ParItemNo = @ParItemNo , ");
            strSql.Append(" IsCheckFee = @IsCheckFee , ");
            strSql.Append(" ReceiveFlag = @ReceiveFlag,  ");
            strSql.Append(" CombiItemNo = @CombiItemNo,  ");
            strSql.Append(" SampleType = @SampleType,  ");
            strSql.Append(" CheckType = @CheckType,  ");
            strSql.Append(" CheckTypeName = @CheckTypeName , ");
            strSql.Append(" price = @price,  ");
            strSql.Append(" LabCombiItemNo = @LabCombiItemNo, ");
            strSql.Append(" LabParItemNo = @LabParItemNo  ");
            strSql.Append(" where NRequestItemNo=@NRequestItemNo ");

            SqlParameter[] parameters = {
			            new SqlParameter("@NRequestItemNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@HISCharge", SqlDbType.Money,8) ,            
                        new SqlParameter("@ItemCharge", SqlDbType.Money,8) ,            
                        new SqlParameter("@SampleTypeNo", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@zdy1", SqlDbType.VarChar,80) ,            
                        new SqlParameter("@zdy2", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@SerialNo", SqlDbType.NChar,30) ,            
                        new SqlParameter("@zdy3", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@zdy4", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@zdy5", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@DeleteFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@ItemSource", SqlDbType.Int,4) ,            
                        new SqlParameter("@OldSerialNo", SqlDbType.VarChar,60) ,            
                        new SqlParameter("@CountNodesItemSource", SqlDbType.Char,1) ,            
                        new SqlParameter("@ReportFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@PartFlag", SqlDbType.Int,4) , 
                        new SqlParameter("@WebLisOrgID", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@WebLisSourceOrgID", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@WebLisSourceOrgName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ClientNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ClientName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@NRequestFormNo", SqlDbType.BigInt,8) ,            
                        new SqlParameter("@BarCodeFormNo", SqlDbType.BigInt,8) ,            
                        new SqlParameter("@FormNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@TollItemNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@ParItemNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@IsCheckFee", SqlDbType.Int,4) ,            
                        new SqlParameter("@ReceiveFlag", SqlDbType.Int,4),         
                        new SqlParameter("@CombiItemNo", SqlDbType.VarChar,50),
                        new SqlParameter("@SampleType", SqlDbType.VarChar,10),  
                new SqlParameter("@CheckType", SqlDbType.VarChar,20),  
                new SqlParameter("@CheckTypeName", SqlDbType.VarChar,40),
                 new SqlParameter("@price", SqlDbType.Decimal),
                  new SqlParameter("@LabCombiItemNo",SqlDbType.VarChar,50),
                   new SqlParameter("@LabParItemNo", SqlDbType.VarChar,50)

            };
            if (model.NRequestItemNo != null)
            {
                parameters[0].Value = model.NRequestItemNo;
            }
            if (model.HISCharge != null)
            {
                parameters[1].Value = model.HISCharge;
            }
            if (model.ItemCharge != null)
            {
                parameters[2].Value = model.ItemCharge;
            }
            if (model.SampleTypeNo != null)
            {
                parameters[3].Value = model.SampleTypeNo;
            }
            if (model.zdy1 != null)
            {
                parameters[4].Value = model.zdy1;
            }
            if (model.zdy2 != null)
            {
                parameters[5].Value = model.zdy2;
            }
            if (model.SerialNo != null)
            {
                parameters[6].Value = model.SerialNo;
            }
            if (model.zdy3 != null)
            {
                parameters[7].Value = model.zdy3;
            }
            if (model.zdy4 != null)
            {
                parameters[8].Value = model.zdy4;
            }
            if (model.zdy5 != null)
            {
                parameters[9].Value = model.zdy5;
            }
            if (model.DeleteFlag != null)
            {
                parameters[10].Value = model.DeleteFlag;
            }
            if (model.ItemSource != null)
            {
                parameters[11].Value = model.ItemSource;
            }
            if (model.OldSerialNo != null)
            {
                parameters[12].Value = model.OldSerialNo;
            }
            if (model.CountNodesItemSource != null)
            {
                parameters[13].Value = model.CountNodesItemSource;
            }
            if (model.ReportFlag != null)
            {
                parameters[14].Value = model.ReportFlag;
            }
            if (model.PartFlag != null)
            {
                parameters[15].Value = model.PartFlag;
            }
            if (model.WebLisOrgID != null)
            {
                parameters[16].Value = model.WebLisOrgID;
            }
            if (model.WebLisSourceOrgID != null)
            {
                parameters[17].Value = model.WebLisSourceOrgID;
            }
            if (model.WebLisSourceOrgName != null)
            {
                parameters[18].Value = model.WebLisSourceOrgName;
            }
            if (model.ClientNo != null)
            {
                parameters[19].Value = model.ClientNo;
            }
            if (model.ClientName != null)
            {
                parameters[20].Value = model.ClientName;
            }
            if (model.NRequestFormNo != null)
            {
                parameters[21].Value = model.NRequestFormNo;
            }
            if (model.BarCodeFormNo != null)
            {
                parameters[22].Value = model.BarCodeFormNo;
            }
            if (model.FormNo != null)
            {
                parameters[23].Value = model.FormNo;
            }
            if (model.TollItemNo != null)
            {
                parameters[24].Value = model.TollItemNo;
            }
            if (model.ParItemNo != null)
            {
                parameters[25].Value = model.ParItemNo;
            }
            if (model.IsCheckFee != null)
            {
                parameters[26].Value = model.IsCheckFee;
            }
            if (model.ReceiveFlag != null)
            {
                parameters[27].Value = model.ReceiveFlag;
            }
            if (model.CombiItemNo != null)
            {
                parameters[28].Value = model.CombiItemNo;
            }
            if (model.SampleType != null)
            {
                parameters[29].Value = model.SampleType;
            }
            if (model.CheckType != null)
            {
                parameters[30].Value = model.CheckType;
            }
            if (model.CheckTypeName != null)
            {
                parameters[31].Value = model.CheckTypeName;
            }
            if (model.Price != null)
                parameters[32].Value = model.Price;

            if (model.LabCombiItemNo != null)
            {
                parameters[33].Value = model.LabCombiItemNo;
            }
            if (model.LabParItemNo != null)
            {
                parameters[34].Value = model.LabParItemNo;
            }
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int NRequestItemNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from NRequestItem ");
            strSql.Append(" where NRequestItemNo=@NRequestItemNo");
            SqlParameter[] parameters = {
					new SqlParameter("@NRequestItemNo", SqlDbType.Int,4)
			};
            parameters[0].Value = NRequestItemNo;
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string NRequestItemNolist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from NRequestItem ");
            strSql.Append(" where NRequestItemNo in (" + NRequestItemNolist + ")  ");
            int rows = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.NRequestItem GetModel(int NRequestItemNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select NRequestItemNo, HISCharge, ItemCharge, SampleTypeNo, zdy1, zdy2, SerialNo, zdy3, zdy4, zdy5, DeleteFlag, ItemSource, OldSerialNo, CountNodesItemSource, ReportFlag, PartFlag, WebLisOrgID, WebLisSourceOrgID, WebLisSourceOrgName, ClientNo, ClientName, NRequestFormNo, BarCodeFormNo, FormNo, TollItemNo, ParItemNo, IsCheckFee, ReceiveFlag  ");
            strSql.Append("  from NRequestItem ");
            strSql.Append(" where NRequestItemNo=@NRequestItemNo");
            SqlParameter[] parameters = {
					new SqlParameter("@NRequestItemNo", SqlDbType.Int,4)
			};
            parameters[0].Value = NRequestItemNo;


            ZhiFang.Model.NRequestItem model = new ZhiFang.Model.NRequestItem();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["NRequestItemNo"].ToString() != "")
                {
                    model.NRequestItemNo = int.Parse(ds.Tables[0].Rows[0]["NRequestItemNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HISCharge"].ToString() != "")
                {
                    model.HISCharge = decimal.Parse(ds.Tables[0].Rows[0]["HISCharge"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ItemCharge"].ToString() != "")
                {
                    model.ItemCharge = decimal.Parse(ds.Tables[0].Rows[0]["ItemCharge"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SampleTypeNo"].ToString() != "")
                {
                    model.SampleTypeNo = int.Parse(ds.Tables[0].Rows[0]["SampleTypeNo"].ToString());
                }
                model.zdy1 = ds.Tables[0].Rows[0]["zdy1"].ToString();
                model.zdy2 = ds.Tables[0].Rows[0]["zdy2"].ToString();
                model.SerialNo = ds.Tables[0].Rows[0]["SerialNo"].ToString();
                model.zdy3 = ds.Tables[0].Rows[0]["zdy3"].ToString();
                model.zdy4 = ds.Tables[0].Rows[0]["zdy4"].ToString();
                model.zdy5 = ds.Tables[0].Rows[0]["zdy5"].ToString();
                if (ds.Tables[0].Rows[0]["DeleteFlag"].ToString() != "")
                {
                    model.DeleteFlag = int.Parse(ds.Tables[0].Rows[0]["DeleteFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ItemSource"].ToString() != "")
                {
                    model.ItemSource = int.Parse(ds.Tables[0].Rows[0]["ItemSource"].ToString());
                }
                model.OldSerialNo = ds.Tables[0].Rows[0]["OldSerialNo"].ToString();
                model.CountNodesItemSource = ds.Tables[0].Rows[0]["CountNodesItemSource"].ToString();
                if (ds.Tables[0].Rows[0]["ReportFlag"].ToString() != "")
                {
                    model.ReportFlag = int.Parse(ds.Tables[0].Rows[0]["ReportFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PartFlag"].ToString() != "")
                {
                    model.PartFlag = int.Parse(ds.Tables[0].Rows[0]["PartFlag"].ToString());
                }
                model.WebLisOrgID = ds.Tables[0].Rows[0]["WebLisOrgID"].ToString();
                model.WebLisSourceOrgID = ds.Tables[0].Rows[0]["WebLisSourceOrgID"].ToString();
                model.WebLisSourceOrgName = ds.Tables[0].Rows[0]["WebLisSourceOrgName"].ToString();
                model.ClientNo = ds.Tables[0].Rows[0]["ClientNo"].ToString();
                model.ClientName = ds.Tables[0].Rows[0]["ClientName"].ToString();
                if (ds.Tables[0].Rows[0]["NRequestFormNo"].ToString() != "")
                {
                    model.NRequestFormNo = long.Parse(ds.Tables[0].Rows[0]["NRequestFormNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BarCodeFormNo"].ToString() != "")
                {
                    model.BarCodeFormNo = long.Parse(ds.Tables[0].Rows[0]["BarCodeFormNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FormNo"].ToString() != "")
                {
                    model.FormNo = int.Parse(ds.Tables[0].Rows[0]["FormNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TollItemNo"].ToString() != "")
                {
                    model.TollItemNo = int.Parse(ds.Tables[0].Rows[0]["TollItemNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ParItemNo"].ToString() != "")
                {
                    //model.ParItemNo = int.Parse(ds.Tables[0].Rows[0]["ParItemNo"].ToString());
                    model.ParItemNo = ds.Tables[0].Rows[0]["ParItemNo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["IsCheckFee"].ToString() != "")
                {
                    model.IsCheckFee = int.Parse(ds.Tables[0].Rows[0]["IsCheckFee"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ReceiveFlag"].ToString() != "")
                {
                    model.ReceiveFlag = int.Parse(ds.Tables[0].Rows[0]["ReceiveFlag"].ToString());
                }
                model.SampleType = ds.Tables[0].Rows[0]["SampleType"].ToString();
                model.CheckType = ds.Tables[0].Rows[0]["CheckType"].ToString();
                model.CheckTypeName = ds.Tables[0].Rows[0]["CheckTypeName"].ToString();

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
            strSql.Append("SELECT     dbo.TestItem.CName, dbo.TestItem.EName, dbo.TestItem.ShortName, dbo.TestItem.ShortCode, dbo.TestItem.IsDoctorItem, dbo.TestItem.IschargeItem, "
                      + "dbo.TestItem.isCombiItem, dbo.TestItem.IsProfile, dbo.TestItem.ItemNo, dbo.NRequestItem.* ");
            strSql.Append(" FROM dbo.TestItem RIGHT OUTER JOIN  dbo.NRequestItem ON dbo.TestItem.ItemNo = dbo.NRequestItem.ParItemNo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表 PKI定制 ganwh 2016-01-05 add
        /// </summary>
        public DataSet GetList_PKI(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * from NRequestItem ");
           
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 and " + strWhere);
            }
            ZhiFang.Common.Log.Log.Info("DAL.MsSql.Weblis.NRequestItem->GetList_PKI 查询NRequestItem：" + strSql.ToString());
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 根据实体获取DataSet   注意此处为四川大家（组套）定制CombiItemNo组套
        /// </summary>
        public DataSet GetList(ZhiFang.Model.NRequestItem model)
        {
            StringBuilder strSql = new StringBuilder();
            string CombiItem = ZhiFang.Common.Public.ConfigHelper.GetConfigString("CombiItem");
            if (CombiItem == "组套定制")
            {
                strSql.Append("select   dbo.TestItem.CName, dbo.TestItem.EName, dbo.TestItem.ShortName, dbo.TestItem.ShortCode, dbo.TestItem.IsDoctorItem, dbo.TestItem.IschargeItem, "
                          + "dbo.TestItem.isCombiItem, dbo.TestItem.IsProfile, dbo.TestItem.ItemNo , dbo.NRequestItem.* ");
                strSql.Append(" FROM  dbo.TestItem RIGHT OUTER JOIN  dbo.NRequestItem ON dbo.TestItem.ItemNo = dbo.NRequestItem.CombiItemNo where 1=1 ");
            }
            else
            {
                strSql.Append("select   dbo.TestItem.CName, dbo.TestItem.EName, dbo.TestItem.ShortName, dbo.TestItem.ShortCode, dbo.TestItem.IsDoctorItem, dbo.TestItem.IschargeItem, "
                             + "dbo.TestItem.isCombiItem, dbo.TestItem.IsProfile, dbo.TestItem.ItemNo , dbo.NRequestItem.* ");
                strSql.Append(" FROM  dbo.TestItem RIGHT OUTER JOIN  dbo.NRequestItem ON dbo.TestItem.ItemNo = dbo.NRequestItem.ParItemNo where 1=1 ");
            }

            if (model.HISCharge != null)
            {
                strSql.Append(" and HISCharge=" + model.HISCharge + " ");
            }

            if (model.ItemCharge != null)
            {
                strSql.Append(" and ItemCharge=" + model.ItemCharge + " ");
            }

            if (model.SampleTypeNo != null)
            {
                int result = 0;
                if (int.TryParse(model.SampleTypeNo.ToString(), out result))
                {
                    strSql.Append(" and SampleTypeNo=" + model.SampleTypeNo + " ");
                }
                else
                {
                    strSql.Append(" and SampleTypeNo='" + model.SampleTypeNo + "' ");
                }
                
            }

            if (model.zdy1 != null)
            {
                strSql.Append(" and zdy1='" + model.zdy1 + "' ");
            }

            if (model.zdy2 != null)
            {
                strSql.Append(" and zdy2='" + model.zdy2 + "' ");
            }

            if (model.SerialNo != null)
            {
                strSql.Append(" and SerialNo='" + model.SerialNo + "' ");
            }

            if (model.zdy3 != null)
            {
                strSql.Append(" and zdy3='" + model.zdy3 + "' ");
            }

            if (model.zdy4 != null)
            {
                strSql.Append(" and zdy4='" + model.zdy4 + "' ");
            }

            if (model.zdy5 != null)
            {
                strSql.Append(" and zdy5='" + model.zdy5 + "' ");
            }

            if (model.DeleteFlag != null)
            {
                strSql.Append(" and DeleteFlag=" + model.DeleteFlag + " ");
            }

            if (model.ItemSource != null)
            {
                strSql.Append(" and ItemSource=" + model.ItemSource + " ");
            }

            if (model.OldSerialNo != null)
            {
                strSql.Append(" and OldSerialNo='" + model.OldSerialNo + "' ");
            }

            if (model.CountNodesItemSource != null)
            {
                strSql.Append(" and CountNodesItemSource='" + model.CountNodesItemSource + "' ");
            }

            if (model.ReportFlag != null)
            {
                strSql.Append(" and ReportFlag=" + model.ReportFlag + " ");
            }

            if (model.PartFlag != null)
            {
                strSql.Append(" and PartFlag=" + model.PartFlag + " ");
            }

            if (model.WebLisOrgID != null)
            {
                strSql.Append(" and WebLisOrgID='" + model.WebLisOrgID + "' ");
            }

            if (model.WebLisSourceOrgID != null)
            {
                strSql.Append(" and WebLisSourceOrgID='" + model.WebLisSourceOrgID + "' ");
            }

            if (model.WebLisSourceOrgName != null)
            {
                strSql.Append(" and WebLisSourceOrgName='" + model.WebLisSourceOrgName + "' ");
            }

            if (model.ClientNo != null)
            {
                strSql.Append(" and ClientNo='" + model.ClientNo + "' ");
            }

            if (model.ClientName != null)
            {
                strSql.Append(" and ClientName='" + model.ClientName + "' ");
            }
            if (model.CombiItemNo != null)
            {
                strSql.Append(" and CombiItemNo='" + model.CombiItemNo + "' ");
            }
            if (model.NRequestFormNo != null)
            {
                strSql.Append(" and NRequestFormNo='" + model.NRequestFormNo + "' ");
            }

            if (model.BarCodeFormNo != null)
            {
                strSql.Append(" and BarCodeFormNo=" + model.BarCodeFormNo + " ");
            }

            if (model.FormNo != null)
            {
                strSql.Append(" and FormNo=" + model.FormNo + " ");
            }

            if (model.TollItemNo != null)
            {
                strSql.Append(" and TollItemNo=" + model.TollItemNo + " ");
            }

            if (model.ParItemNo != null)
            {
                int result = 0;
                if (int.TryParse(model.ParItemNo, out result))
                {
                    strSql.Append(" and ParItemNo=" + model.ParItemNo + " ");
                }
                else {
                    strSql.Append(" and ParItemNo='" + model.ParItemNo + "' ");
                }
               
            }

            if (model.IsCheckFee != null)
            {
                strSql.Append(" and IsCheckFee=" + model.IsCheckFee + " ");
            }

            if (model.ReceiveFlag != null)
            {
                strSql.Append(" and ReceiveFlag=" + model.ReceiveFlag + " ");
            }
            if (model.SampleType != null)
            {
                strSql.Append(" and SampleType=" + model.SampleType + " ");
            }
            if (model.CheckType != null)
            {
                strSql.Append(" and CheckType=" + model.CheckType + " ");
            }
            if (model.CheckTypeName != null)
            {
                strSql.Append(" and CheckTypeName=" + model.CheckTypeName + " ");
            }
            strSql.Append(" order by CombiItemNo,ParItemNo ");
            Common.Log.Log.Info(strSql.ToString());
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 十堰太和 组套不需要对照 ganwh add 2015-10-28
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DataSet GetList_TaiHe(ZhiFang.Model.NRequestItem model)
        {
            StringBuilder strSql = new StringBuilder();
            string CombiItem = ZhiFang.Common.Public.ConfigHelper.GetConfigString("CombiItem");
            strSql.Append("select dbo.NRequestItem.* FROM dbo.NRequestItem  where 1=1 ");

            if (model.HISCharge != null)
            {
                strSql.Append(" and HISCharge=" + model.HISCharge + " ");
            }

            if (model.ItemCharge != null)
            {
                strSql.Append(" and ItemCharge=" + model.ItemCharge + " ");
            }

            if (model.SampleTypeNo != null)
            {
                int result = 0;
                if (int.TryParse(model.SampleTypeNo.ToString(), out result))
                {
                    strSql.Append(" and SampleTypeNo=" + model.SampleTypeNo + " ");
                }
                else
                {
                    strSql.Append(" and SampleTypeNo='" + model.SampleTypeNo + "' ");
                }

            }

            if (model.zdy1 != null)
            {
                strSql.Append(" and zdy1='" + model.zdy1 + "' ");
            }

            if (model.zdy2 != null)
            {
                strSql.Append(" and zdy2='" + model.zdy2 + "' ");
            }

            if (model.SerialNo != null)
            {
                strSql.Append(" and SerialNo='" + model.SerialNo + "' ");
            }

            if (model.zdy3 != null)
            {
                strSql.Append(" and zdy3='" + model.zdy3 + "' ");
            }

            if (model.zdy4 != null)
            {
                strSql.Append(" and zdy4='" + model.zdy4 + "' ");
            }

            if (model.zdy5 != null)
            {
                strSql.Append(" and zdy5='" + model.zdy5 + "' ");
            }

            if (model.DeleteFlag != null)
            {
                strSql.Append(" and DeleteFlag=" + model.DeleteFlag + " ");
            }

            if (model.ItemSource != null)
            {
                strSql.Append(" and ItemSource=" + model.ItemSource + " ");
            }

            if (model.OldSerialNo != null)
            {
                strSql.Append(" and OldSerialNo='" + model.OldSerialNo + "' ");
            }

            if (model.CountNodesItemSource != null)
            {
                strSql.Append(" and CountNodesItemSource='" + model.CountNodesItemSource + "' ");
            }

            if (model.ReportFlag != null)
            {
                strSql.Append(" and ReportFlag=" + model.ReportFlag + " ");
            }

            if (model.PartFlag != null)
            {
                strSql.Append(" and PartFlag=" + model.PartFlag + " ");
            }

            if (model.WebLisOrgID != null)
            {
                strSql.Append(" and WebLisOrgID='" + model.WebLisOrgID + "' ");
            }

            if (model.WebLisSourceOrgID != null)
            {
                strSql.Append(" and WebLisSourceOrgID='" + model.WebLisSourceOrgID + "' ");
            }

            if (model.WebLisSourceOrgName != null)
            {
                strSql.Append(" and WebLisSourceOrgName='" + model.WebLisSourceOrgName + "' ");
            }

            if (model.ClientNo != null)
            {
                strSql.Append(" and ClientNo='" + model.ClientNo + "' ");
            }

            if (model.ClientName != null)
            {
                strSql.Append(" and ClientName='" + model.ClientName + "' ");
            }
            if (model.CombiItemNo != null)
            {
                strSql.Append(" and CombiItemNo='" + model.CombiItemNo + "' ");
            }
            if (model.NRequestFormNo != null)
            {
                strSql.Append(" and NRequestFormNo='" + model.NRequestFormNo + "' ");
            }

            if (model.BarCodeFormNo != null)
            {
                strSql.Append(" and BarCodeFormNo=" + model.BarCodeFormNo + " ");
            }

            if (model.FormNo != null)
            {
                strSql.Append(" and FormNo=" + model.FormNo + " ");
            }

            if (model.TollItemNo != null)
            {
                strSql.Append(" and TollItemNo=" + model.TollItemNo + " ");
            }

            if (model.ParItemNo != null)
            {
                int result = 0;
                if (int.TryParse(model.ParItemNo, out result))
                {
                    strSql.Append(" and ParItemNo=" + model.ParItemNo + " ");
                }
                else
                {
                    strSql.Append(" and ParItemNo='" + model.ParItemNo + "' ");
                }

            }

            if (model.IsCheckFee != null)
            {
                strSql.Append(" and IsCheckFee=" + model.IsCheckFee + " ");
            }

            if (model.ReceiveFlag != null)
            {
                strSql.Append(" and ReceiveFlag=" + model.ReceiveFlag + " ");
            }
            if (model.SampleType != null)
            {
                strSql.Append(" and SampleType=" + model.SampleType + " ");
            }
            if (model.CheckType != null)
            {
                strSql.Append(" and CheckType=" + model.CheckType + " ");
            }
            if (model.CheckTypeName != null)
            {
                strSql.Append(" and CheckTypeName=" + model.CheckTypeName + " ");
            }
            strSql.Append(" order by LabCombiItemNo,LabParItemNo ");
            Common.Log.Log.Info(strSql.ToString());
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }


        /// <summary>
        /// 获取总记录
        /// </summary>
        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM NRequestItem ");
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
        public int GetTotalCount(ZhiFang.Model.NRequestItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM NRequestItem where 1=1 ");

            if (model.HISCharge != null)
            {
                strSql.Append(" and HISCharge=" + model.HISCharge + " ");
            }

            if (model.ItemCharge != null)
            {
                strSql.Append(" and ItemCharge=" + model.ItemCharge + " ");
            }

            if (model.SampleTypeNo != null)
            {
                strSql.Append(" and SampleTypeNo=" + model.SampleTypeNo + " ");
            }

            if (model.zdy1 != null)
            {
                strSql.Append(" and zdy1='" + model.zdy1 + "' ");
            }

            if (model.zdy2 != null)
            {
                strSql.Append(" and zdy2='" + model.zdy2 + "' ");
            }

            if (model.SerialNo != null)
            {
                strSql.Append(" and SerialNo='" + model.SerialNo + "' ");
            }

            if (model.zdy3 != null)
            {
                strSql.Append(" and zdy3='" + model.zdy3 + "' ");
            }

            if (model.zdy4 != null)
            {
                strSql.Append(" and zdy4='" + model.zdy4 + "' ");
            }

            if (model.zdy5 != null)
            {
                strSql.Append(" and zdy5='" + model.zdy5 + "' ");
            }

            if (model.DeleteFlag != null)
            {
                strSql.Append(" and DeleteFlag=" + model.DeleteFlag + " ");
            }

            if (model.ItemSource != null)
            {
                strSql.Append(" and ItemSource=" + model.ItemSource + " ");
            }

            if (model.OldSerialNo != null)
            {
                strSql.Append(" and OldSerialNo='" + model.OldSerialNo + "' ");
            }

            if (model.CountNodesItemSource != null)
            {
                strSql.Append(" and CountNodesItemSource='" + model.CountNodesItemSource + "' ");
            }

            if (model.ReportFlag != null)
            {
                strSql.Append(" and ReportFlag=" + model.ReportFlag + " ");
            }

            if (model.PartFlag != null)
            {
                strSql.Append(" and PartFlag=" + model.PartFlag + " ");
            }

            if (model.WebLisOrgID != null)
            {
                strSql.Append(" and WebLisOrgID='" + model.WebLisOrgID + "' ");
            }

            if (model.WebLisSourceOrgID != null)
            {
                strSql.Append(" and WebLisSourceOrgID='" + model.WebLisSourceOrgID + "' ");
            }

            if (model.WebLisSourceOrgName != null)
            {
                strSql.Append(" and WebLisSourceOrgName='" + model.WebLisSourceOrgName + "' ");
            }

            if (model.ClientNo != null)
            {
                strSql.Append(" and ClientNo='" + model.ClientNo + "' ");
            }

            if (model.ClientName != null)
            {
                strSql.Append(" and ClientName='" + model.ClientName + "' ");
            }

            if (model.NRequestFormNo != null)
            {
                strSql.Append(" and NRequestFormNo='" + model.NRequestFormNo + "' ");
            }

            if (model.BarCodeFormNo != null)
            {
                strSql.Append(" and BarCodeFormNo=" + model.BarCodeFormNo + " ");
            }

            if (model.FormNo != null)
            {
                strSql.Append(" and FormNo=" + model.FormNo + " ");
            }

            if (model.TollItemNo != null)
            {
                strSql.Append(" and TollItemNo=" + model.TollItemNo + " ");
            }

            if (model.ParItemNo != null)
            {
                strSql.Append(" and ParItemNo=" + model.ParItemNo + " ");
            }

            if (model.IsCheckFee != null)
            {
                strSql.Append(" and IsCheckFee=" + model.IsCheckFee + " ");
            }

            if (model.ReceiveFlag != null)
            {
                strSql.Append(" and ReceiveFlag=" + model.ReceiveFlag + " ");
            }
            if (model.SampleType != null)
            {
                strSql.Append(" and SampleType=" + model.SampleType + " ");
            }
            if (model.CheckType != null)
            {
                strSql.Append(" and CheckType=" + model.CheckType + " ");
            }
            if (model.CheckTypeName != null)
            {
                strSql.Append(" and CheckTypeName=" + model.CheckTypeName + " ");
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
        public DataSet GetListByPage(ZhiFang.Model.NRequestItem model, int nowPageNum, int nowPageSize)
        {
            throw new NotImplementedException();
        }

        public bool Exists(int NRequestItemNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from NRequestItem ");
            strSql.Append(" where NRequestItemNo ='" + NRequestItemNo + "'");
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

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("NRequestItemNo", "NRequestItem");
        }

        public DataSet GetList(int Top, ZhiFang.Model.NRequestItem model, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" SELECT     dbo.TestItem.CName, dbo.TestItem.EName, dbo.TestItem.ShortName, dbo.TestItem.ShortCode, dbo.TestItem.IsDoctorItem, dbo.TestItem.IschargeItem, "
                      + "dbo.TestItem.isCombiItem, dbo.TestItem.IsProfile, dbo.TestItem.ItemNo , dbo.NRequestItem.* ");
            strSql.Append(" FROM dbo.TestItem RIGHT OUTER JOIN  dbo.NRequestItem ON dbo.TestItem.ItemNo = dbo.NRequestItem.ParItemNo ");


            if (model.HISCharge != null)
            {
                strSql.Append(" and HISCharge=" + model.HISCharge + " ");
            }

            if (model.ItemCharge != null)
            {
                strSql.Append(" and ItemCharge=" + model.ItemCharge + " ");
            }

            if (model.SampleTypeNo != null)
            {
                strSql.Append(" and SampleTypeNo=" + model.SampleTypeNo + " ");
            }

            if (model.zdy1 != null)
            {

                strSql.Append(" and zdy1='" + model.zdy1 + "' ");
            }

            if (model.zdy2 != null)
            {

                strSql.Append(" and zdy2='" + model.zdy2 + "' ");
            }

            if (model.SerialNo != null)
            {

                strSql.Append(" and SerialNo='" + model.SerialNo + "' ");
            }

            if (model.zdy3 != null)
            {

                strSql.Append(" and zdy3='" + model.zdy3 + "' ");
            }

            if (model.zdy4 != null)
            {

                strSql.Append(" and zdy4='" + model.zdy4 + "' ");
            }

            if (model.zdy5 != null)
            {

                strSql.Append(" and zdy5='" + model.zdy5 + "' ");
            }

            if (model.DeleteFlag != null)
            {
                strSql.Append(" and DeleteFlag=" + model.DeleteFlag + " ");
            }

            if (model.ItemSource != null)
            {
                strSql.Append(" and ItemSource=" + model.ItemSource + " ");
            }

            if (model.OldSerialNo != null)
            {

                strSql.Append(" and OldSerialNo='" + model.OldSerialNo + "' ");
            }

            if (model.CountNodesItemSource != null)
            {

                strSql.Append(" and CountNodesItemSource='" + model.CountNodesItemSource + "' ");
            }

            if (model.ReportFlag != null)
            {
                strSql.Append(" and ReportFlag=" + model.ReportFlag + " ");
            }

            if (model.PartFlag != null)
            {
                strSql.Append(" and PartFlag=" + model.PartFlag + " ");
            }

            if (model.WebLisOrgID != null)
            {

                strSql.Append(" and WebLisOrgID='" + model.WebLisOrgID + "' ");
            }

            if (model.WebLisSourceOrgID != null)
            {

                strSql.Append(" and WebLisSourceOrgID='" + model.WebLisSourceOrgID + "' ");
            }

            if (model.WebLisSourceOrgName != null)
            {

                strSql.Append(" and WebLisSourceOrgName='" + model.WebLisSourceOrgName + "' ");
            }

            if (model.ClientNo != null)
            {

                strSql.Append(" and ClientNo='" + model.ClientNo + "' ");
            }

            if (model.ClientName != null)
            {

                strSql.Append(" and ClientName='" + model.ClientName + "' ");
            }

            if (model.NRequestFormNo != null)
            {

                strSql.Append(" and NRequestFormNo='" + model.NRequestFormNo + "' ");
            }

            if (model.BarCodeFormNo != null)
            {
                strSql.Append(" and BarCodeFormNo=" + model.BarCodeFormNo + " ");
            }

            if (model.FormNo != null)
            {
                strSql.Append(" and FormNo=" + model.FormNo + " ");
            }

            if (model.TollItemNo != null)
            {
                strSql.Append(" and TollItemNo=" + model.TollItemNo + " ");
            }

            if (model.ParItemNo != null)
            {
                strSql.Append(" and ParItemNo=" + model.ParItemNo + " ");
            }

            if (model.IsCheckFee != null)
            {
                strSql.Append(" and IsCheckFee=" + model.IsCheckFee + " ");
            }

            if (model.ReceiveFlag != null)
            {
                strSql.Append(" and ReceiveFlag=" + model.ReceiveFlag + " ");
            }
            if (model.SampleType != null)
            {
                strSql.Append(" and SampleType=" + model.SampleType + " ");
            }
            if (model.CheckType != null)
            {
                strSql.Append(" and CheckType=" + model.CheckType + " ");
            }
            if (model.CheckTypeName != null)
            {
                strSql.Append(" and CheckTypeName=" + model.CheckTypeName + " ");
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetAllList()
        {
            return GetList("");
        }

        #region IDNRequestItem 成员

        public bool DeleteList_ByNRequestFormNo(long NRequestItemNolist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from NRequestItem ");
            strSql.Append(" where NRequestFormNo=@NRequestFormNo");
            SqlParameter[] parameters = {
					new SqlParameter("@NRequestFormNo", SqlDbType.BigInt,8)
			};
            parameters[0].Value = NRequestItemNolist;
            if (DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region IDataBase<NRequestItem> 成员

        public int AddUpdateByDataSet(DataSet ds)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDNRequestItem 成员


        public DataSet GetList_ClientNo(Model.NRequestItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT     dbo.B_TestItemControl.ControlLabNo, dbo.B_TestItemControl.ItemNo AS Expr1, dbo.B_Lab_TestItem.*, dbo.NRequestItem.* FROM        dbo.B_Lab_TestItem INNER JOIN                      dbo.B_TestItemControl ON dbo.B_Lab_TestItem.LabCode = dbo.B_TestItemControl.ControlLabNo AND                       dbo.B_Lab_TestItem.ItemNo = dbo.B_TestItemControl.ControlItemNo INNER JOIN                      dbo.NRequestItem ON dbo.B_TestItemControl.ControlLabNo = dbo.NRequestItem.WebLisSourceOrgID AND                       dbo.B_TestItemControl.ItemNo = dbo.NRequestItem.ParItemNo WHERE     (1 = 1)");

            if (model.HISCharge != null)
            {
                strSql.Append(" and  dbo.NRequestItem.HISCharge=" + model.HISCharge + " ");
            }

            if (model.ItemCharge != null)
            {
                strSql.Append(" and  dbo.NRequestItem.ItemCharge=" + model.ItemCharge + " ");
            }

            if (model.SampleTypeNo != null)
            {
                strSql.Append(" and  dbo.NRequestItem.SampleTypeNo=" + model.SampleTypeNo + " ");
            }

            if (model.zdy1 != null)
            {
                strSql.Append(" and  dbo.NRequestItem.zdy1='" + model.zdy1 + "' ");
            }

            if (model.zdy2 != null)
            {
                strSql.Append(" and  dbo.NRequestItem.zdy2='" + model.zdy2 + "' ");
            }

            if (model.SerialNo != null)
            {
                strSql.Append(" and  dbo.NRequestItem.SerialNo='" + model.SerialNo + "' ");
            }

            if (model.zdy3 != null)
            {
                strSql.Append(" and  dbo.NRequestItem.zdy3='" + model.zdy3 + "' ");
            }

            if (model.zdy4 != null)
            {
                strSql.Append(" and  dbo.NRequestItem.zdy4='" + model.zdy4 + "' ");
            }

            if (model.zdy5 != null)
            {
                strSql.Append(" and  dbo.NRequestItem.zdy5='" + model.zdy5 + "' ");
            }

            if (model.DeleteFlag != null)
            {
                strSql.Append(" and  dbo.NRequestItem.DeleteFlag=" + model.DeleteFlag + " ");
            }

            if (model.ItemSource != null)
            {
                strSql.Append(" and  dbo.NRequestItem.ItemSource=" + model.ItemSource + " ");
            }

            if (model.OldSerialNo != null)
            {
                strSql.Append(" and  dbo.NRequestItem.OldSerialNo='" + model.OldSerialNo + "' ");
            }

            if (model.CountNodesItemSource != null)
            {
                strSql.Append(" and  dbo.NRequestItem.CountNodesItemSource='" + model.CountNodesItemSource + "' ");
            }

            if (model.ReportFlag != null)
            {
                strSql.Append(" and  dbo.NRequestItem.ReportFlag=" + model.ReportFlag + " ");
            }

            if (model.PartFlag != null)
            {
                strSql.Append(" and  dbo.NRequestItem.PartFlag=" + model.PartFlag + " ");
            }

            if (model.WebLisOrgID != null)
            {
                strSql.Append(" and  dbo.NRequestItem.WebLisOrgID='" + model.WebLisOrgID + "' ");
            }

            if (model.WebLisSourceOrgID != null)
            {
                strSql.Append(" and  dbo.NRequestItem.WebLisSourceOrgID='" + model.WebLisSourceOrgID + "' ");
            }

            if (model.WebLisSourceOrgName != null)
            {
                strSql.Append(" and  dbo.NRequestItem.WebLisSourceOrgName='" + model.WebLisSourceOrgName + "' ");
            }

            if (model.ClientNo != null)
            {
                strSql.Append(" and  dbo.NRequestItem.ClientNo='" + model.ClientNo + "' ");
            }

            if (model.ClientName != null)
            {
                strSql.Append(" and  dbo.NRequestItem.ClientName='" + model.ClientName + "' ");
            }

            if (model.NRequestFormNo != null)
            {
                strSql.Append(" and  dbo.NRequestItem.NRequestFormNo='" + model.NRequestFormNo + "' ");
            }

            if (model.BarCodeFormNo != null)
            {
                strSql.Append(" and  dbo.NRequestItem.BarCodeFormNo=" + model.BarCodeFormNo + " ");
            }

            if (model.FormNo != null)
            {
                strSql.Append(" and  dbo.NRequestItem.FormNo=" + model.FormNo + " ");
            }

            if (model.TollItemNo != null)
            {
                strSql.Append(" and  dbo.NRequestItem.TollItemNo=" + model.TollItemNo + " ");
            }

            if (model.ParItemNo != null)
            {
                strSql.Append(" and  dbo.NRequestItem.ParItemNo=" + model.ParItemNo + " ");
            }

            if (model.IsCheckFee != null)
            {
                strSql.Append(" and  dbo.NRequestItem.IsCheckFee=" + model.IsCheckFee + " ");
            }

            if (model.ReceiveFlag != null)
            {
                strSql.Append(" and  dbo.NRequestItem.ReceiveFlag=" + model.ReceiveFlag + " ");
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        #endregion


        public int UpdateByBarcodeFormNo(Model.NRequestItem nRequestItem)
        {
            string strSql = "update  NRequestItem set WebLisOrgID='" + nRequestItem.WebLisOrgID + "' where BarCodeFormNo=" + nRequestItem.BarCodeFormNo;
            return DbHelperSQL.ExecuteNonQuery(strSql);
        }

        public int UpdateByList(List<string> listStrColumnNi, List<string> listStrDataNi)
        {
            throw new NotImplementedException();
        }

        public int AddByList(List<string> listStrColumnNi, List<string> listStrDataNi)
        {
            string strSql = "insert into NrequestItem (";
            for (int i = 0; i < listStrColumnNi.Count; i++)
            {
                strSql += listStrColumnNi[i].ToString() + ",";
            }
            strSql = strSql.Remove(strSql.Length - 1);
            strSql += ") values( ";
            for (int h = 0; h < listStrDataNi.Count; h++)
            {
                strSql += "'" + listStrDataNi[h].ToString() + "',";
            }
            strSql = strSql.Remove(strSql.Length - 1);
            strSql += ")";
            return DbHelperSQL.ExecuteNonQuery(strSql);
        }

        public DataSet GetNrequestItemByNrequestNo(long nrequestNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT   dbo.NRequestItem.NRequestItemNo, dbo.NRequestItem.HISCharge, dbo.NRequestItem.ItemCharge, dbo.NRequestItem.zdy1, dbo.NRequestItem.zdy2,");
            strSql.Append("dbo.NRequestItem.SerialNo, dbo.NRequestItem.zdy3, dbo.NRequestItem.zdy4, dbo.NRequestItem.zdy5, dbo.NRequestItem.DeleteFlag, ");
            strSql.Append("dbo.NRequestItem.ItemSource, dbo.NRequestItem.OldSerialNo, dbo.NRequestItem.CountNodesItemSource, dbo.NRequestItem.ReportFlag, ");
            strSql.Append("dbo.NRequestItem.PartFlag, dbo.NRequestItem.WebLisOrgID, dbo.NRequestItem.WebLisSourceOrgID, dbo.NRequestItem.WebLisSourceOrgName,");
            strSql.Append("dbo.NRequestItem.ClientNo, dbo.NRequestItem.ClientName, dbo.NRequestItem.NRequestFormNo, dbo.NRequestItem.BarCodeFormNo, dbo.NRequestItem.FormNo, ");
            strSql.Append("dbo.NRequestItem.TollItemNo, dbo.NRequestItem.IsCheckFee, dbo.NRequestItem.ReceiveFlag, dbo.NRequestItem.CombiItemNo, ");
            strSql.Append("dbo.TestItem.CName AS ParItemName, TestItem_1.CName AS CombiItemName, dbo.BarCodeForm.BarCode, dbo.BarCodeForm.SampleTypeNo, dbo.NRequestItem.ParItemNo");
            strSql.Append(" FROM  dbo.NRequestItem INNER JOIN");
            strSql.Append(" dbo.BarCodeForm ON dbo.NRequestItem.BarCodeFormNo = dbo.BarCodeForm.BarCodeFormNo INNER JOIN");
            strSql.Append(" dbo.TestItem ON dbo.NRequestItem.ParItemNo = dbo.TestItem.ItemNo INNER JOIN dbo.TestItem AS TestItem_1 ON dbo.NRequestItem.CombiItemNo = TestItem_1.ItemNo");

            if (nrequestNo != null)
            {
                strSql.Append(" where  nrequestitem.nrequestformno=" + nrequestNo);
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());

        }

        /// <summary>
        /// 十堰太和 获取申请单组套项目 ganwh add 2015-10-28
        /// </summary>
        /// <param name="nrequestNo"></param>
        /// <returns></returns>
        public DataSet GetCombiItemByNrequestFormNo(long nrequestNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.LabCombiItemNo,SUM(a.price) SumPrice,a.ClientNo,b.CName ");
            strSql.Append("from( select LabCombiItemNo,price,ClientNo  from NRequestItem where NRequestFormNo='"+nrequestNo+"') a  ");
            strSql.Append("join B_Lab_TestItem b on a.ClientNo=b.LabCode and a.LabCombiItemNo=b.ItemNo group by LabCombiItemNo,ClientNo,CName; ");
         
            ZhiFang.Common.Log.Log.Info("获取申请单组套项目:"+strSql.ToString());
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 根据条码表ID号,获取项目
        /// </summary>
        /// <param name="BarCodeFormNo"></param>
        /// <returns></returns>
        public DataSet GetNrequestItemByBarCodeFormNo(string BarCodeFormNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select TestItem.CName,ff.NRequestItemNo ,BarCodeForm.SampleTypeName,TestItem.CheckMethodName as CheckMethodName,ff.price,TestItem.Unit from ");
            strSql.Append(" (select * from NRequestItem  where BarCodeFormNo ='" + BarCodeFormNo + "') ff ");
            strSql.Append(" inner join BarCodeForm on BarCodeForm.BarCodeFormNo=ff.BarCodeFormNo ");
            strSql.Append(" inner join TestItem ");
            strSql.Append(" on TestItem.ItemNo= ff.ParItemNo ");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
        //把拒收记录复制到拒收表 ganwh add 2015-10-7
        public int Add(string strSql)
        {
            return DbHelperSQL.ExecuteNonQuery(strSql);
        }

        //查询被拒收申请单的项目信息 ganwh add 2015-10-7
        public DataSet GetRefuseList(string strSql)
        {
            return DbHelperSQL.ExecuteDataSet(strSql);
        }
    }
}

