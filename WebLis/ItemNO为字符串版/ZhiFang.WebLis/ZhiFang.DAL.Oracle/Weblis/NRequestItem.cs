using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;

namespace ZhiFang.DAL.Oracle.weblis
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
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.ItemSource != null)
            {
                strSql1.Append("ItemSource,");
                strSql2.Append("" + model.ItemSource + ",");
            }
            if (model.NRequestFormNo != null)
            {
                strSql1.Append("NRequestFormNo,");
                strSql2.Append("" + model.NRequestFormNo + ",");
            }
            if (model.BarCodeFormNo != null)
            {
                strSql1.Append("BarCodeFormNo,");
                strSql2.Append("" + model.BarCodeFormNo + ",");
            }
            if (model.FormNo != null)
            {
                strSql1.Append("FormNo,");
                strSql2.Append("" + model.FormNo + ",");
            }
            if (model.TollItemNo != null)
            {
                strSql1.Append("TollItemNo,");
                strSql2.Append("" + model.TollItemNo + ",");
            }
            if (model.ParItemNo != null)
            {
                strSql1.Append("ParItemNo,");
                strSql2.Append("" + model.ParItemNo + ",");
            }
            if (model.IsCheckFee != null)
            {
                strSql1.Append("IsCheckFee,");
                strSql2.Append("" + model.IsCheckFee + ",");
            }
            if (model.ReceiveFlag != null)
            {
                strSql1.Append("ReceiveFlag,");
                strSql2.Append("" + model.ReceiveFlag + ",");
            }
            if (model.HISCharge != null)
            {
                strSql1.Append("HISCharge,");
                strSql2.Append("" + model.HISCharge + ",");
            }
            if (model.ItemCharge != null)
            {
                strSql1.Append("ItemCharge,");
                strSql2.Append("" + model.ItemCharge + ",");
            }
            if (model.SampleTypeNo != null)
            {
                strSql1.Append("SampleTypeNo,");
                strSql2.Append("" + model.SampleTypeNo + ",");
            }
            if (model.zdy1 != null)
            {
                strSql1.Append("zdy1,");
                strSql2.Append("'" + model.zdy1 + "',");
            }
            if (model.zdy2 != null)
            {
                strSql1.Append("zdy2,");
                strSql2.Append("'" + model.zdy2 + "',");
            }
            if (model.SerialNo != null)
            {
                strSql1.Append("SerialNo,");
                strSql2.Append("'" + model.SerialNo + "',");
            }
            if (model.zdy3 != null)
            {
                strSql1.Append("zdy3,");
                strSql2.Append("'" + model.zdy3 + "',");
            }
            if (model.zdy4 != null)
            {
                strSql1.Append("zdy4,");
                strSql2.Append("'" + model.zdy4 + "',");
            }
            if (model.zdy5 != null)
            {
                strSql1.Append("zdy5,");
                strSql2.Append("'" + model.zdy5 + "',");
            }
            if (model.DeleteFlag != null)
            {
                strSql1.Append("DeleteFlag,");
                strSql2.Append("" + model.DeleteFlag + ",");
            }
            if (model.OldSerialNo != null)
            {
                strSql1.Append("OldSerialNo,");
                strSql2.Append("'" + model.OldSerialNo + "',");
            }
            if (model.CountNodesItemSource != null)
            {
                strSql1.Append("CountNodesItemSource,");
                strSql2.Append("'" + model.CountNodesItemSource + "',");
            }
            if (model.ReportFlag != null)
            {
                strSql1.Append("ReportFlag,");
                strSql2.Append("" + model.ReportFlag + ",");
            }
            if (model.PartFlag != null)
            {
                strSql1.Append("PartFlag,");
                strSql2.Append("" + model.PartFlag + ",");
            }
            if (model.WebLisOrgID != null)
            {
                strSql1.Append("WebLisOrgID,");
                strSql2.Append("'" + model.WebLisOrgID + "',");
            }
            if (model.WebLisSourceOrgID != null)
            {
                strSql1.Append("WebLisSourceOrgID,");
                strSql2.Append("'" + model.WebLisSourceOrgID + "',");
            }
            if (model.WebLisSourceOrgName != null)
            {
                strSql1.Append("WebLisSourceOrgName,");
                strSql2.Append("'" + model.WebLisSourceOrgName + "',");
            }
            if (model.ClientNo != null)
            {
                strSql1.Append("ClientNo,");
                strSql2.Append("'" + model.ClientNo + "',");
            }
            if (model.ClientName != null)
            {
                strSql1.Append("ClientName,");
                strSql2.Append("'" + model.ClientName + "',");
            }
            //if (model.FlagDateDelete != null)
            //{
            //    strSql1.Append("FlagDateDelete,");
            //    strSql2.Append("'" + model.FlagDateDelete + "',");
            //}
            //if (model.CollectDate != null)
            //{
            //    strSql1.Append("CollectDate,");
            //    strSql2.Append("'" + model.CollectDate + "',");
            //}
            //if (model.CollectTime != null)
            //{
            //    strSql1.Append("CollectTime,");
            //    strSql2.Append("'" + model.CollectTime + "',");
            //}
            //if (model.Collecter != null)
            //{
            //    strSql1.Append("Collecter,");
            //    strSql2.Append("'" + model.Collecter + "',");
            //}
            //if (model.WebLisFlag != null)
            //{
            //    strSql1.Append("WebLisFlag,");
            //    strSql2.Append("'" + model.WebLisFlag + "',");
            //}
            //if (model.ParItemName != null)
            //{
            //    strSql1.Append("ParItemName,");
            //    strSql2.Append("'" + model.ParItemName + "',");
            //}
            //if (model.ParItemCode != null)
            //{
            //    strSql1.Append("ParItemCode,");
            //    strSql2.Append("'" + model.ParItemCode + "',");
            //}
            //if (model.LABNREQUESTFORMNO != null)
            //{
            //    strSql1.Append("LABNREQUESTFORMNO,");
            //    strSql2.Append("'" + model.LABNREQUESTFORMNO + "',");
            //}
            //if (model.ComboId != null)
            //{
            //    strSql1.Append("ComboId,");
            //    strSql2.Append("" + model.ComboId + ",");
            //}
            if (model.CombiItemNo != null)
            {
                strSql1.Append("CombiItemNo,");
                strSql2.Append("" + model.CombiItemNo + ",");
            }

            if (model.SampleType != null)
            {
                strSql1.Append("SampleType,");
                strSql2.Append("'" + model.SampleType + "',"); 
            }
            if (model.CheckType != null)
            {
                strSql1.Append("CheckType,");
                strSql2.Append("'" + model.CheckType + "',");
            }
            if (model.CheckTypeName != null)
            {
                strSql1.Append("CheckTypeName,");
                strSql2.Append("'" + model.CheckTypeName + "',");
            }
            if (model.Price != null)
            {
                strSql1.Append("Price,");
                strSql2.Append("" + model.Price + ",");
            }



            strSql.Append("insert into NRequestItem(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");

            //StringBuilder strSql=new StringBuilder();
            //strSql.Append("insert into NRequestItem(");			
            //strSql.Append("HISCharge,ItemCharge,SampleTypeNo,zdy1,zdy2,SerialNo,zdy3,zdy4,zdy5,DeleteFlag,ItemSource,OldSerialNo,CountNodesItemSource,ReportFlag,PartFlag,WebLisOrgID,WebLisSourceOrgID,WebLisSourceOrgName,ClientNo,ClientName,NRequestFormNo,BarCodeFormNo,FormNo,TollItemNo,ParItemNo,IsCheckFee,ReceiveFlag");
            //strSql.Append(") values (");
            //strSql.Append("@HISCharge,@ItemCharge,@SampleTypeNo,@zdy1,@zdy2,@SerialNo,@zdy3,@zdy4,@zdy5,@DeleteFlag,@ItemSource,@OldSerialNo,@CountNodesItemSource,@ReportFlag,@PartFlag,@WebLisOrgID,@WebLisSourceOrgID,@WebLisSourceOrgName,@ClientNo,@ClientName,@NRequestFormNo,@BarCodeFormNo,@FormNo,@TollItemNo,@ParItemNo,@IsCheckFee,@ReceiveFlag");            
            //strSql.Append(") ");            

            //SqlParameter[] parameters = {
            //            new SqlParameter("@HISCharge", SqlDbType.Money,8) ,            
            //            new SqlParameter("@ItemCharge", SqlDbType.Money,8) ,            
            //            new SqlParameter("@SampleTypeNo", SqlDbType.Int,4) ,            
            //            new SqlParameter("@zdy1", SqlDbType.VarChar,80) ,            
            //            new SqlParameter("@zdy2", SqlDbType.VarChar,20) ,            
            //            new SqlParameter("@SerialNo", SqlDbType.NChar,30) ,            
            //            new SqlParameter("@zdy3", SqlDbType.VarChar,20) ,            
            //            new SqlParameter("@zdy4", SqlDbType.VarChar,20) ,            
            //            new SqlParameter("@zdy5", SqlDbType.VarChar,20) ,            
            //            new SqlParameter("@DeleteFlag", SqlDbType.Int,4) ,            
            //            new SqlParameter("@ItemSource", SqlDbType.Int,4) ,            
            //            new SqlParameter("@OldSerialNo", SqlDbType.VarChar,60) ,            
            //            new SqlParameter("@CountNodesItemSource", SqlDbType.Char,1) ,            
            //            new SqlParameter("@ReportFlag", SqlDbType.Int,4) ,            
            //            new SqlParameter("@PartFlag", SqlDbType.Int,4) ,            
            //            new SqlParameter("@WebLisOrgID", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@WebLisSourceOrgID", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@WebLisSourceOrgName", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@ClientNo", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@ClientName", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@NRequestFormNo", SqlDbType.BigInt,8) ,            
            //            new SqlParameter("@BarCodeFormNo", SqlDbType.BigInt,8) ,            
            //            new SqlParameter("@FormNo", SqlDbType.Int,4) ,            
            //            new SqlParameter("@TollItemNo", SqlDbType.Int,4) ,            
            //            new SqlParameter("@ParItemNo", SqlDbType.Int,4) ,            
            //            new SqlParameter("@IsCheckFee", SqlDbType.Int,4) ,            
            //            new SqlParameter("@ReceiveFlag", SqlDbType.Int,4)             

            //};

            //parameters[0].Value = model.HISCharge;                        
            //parameters[1].Value = model.ItemCharge;                        
            //parameters[2].Value = model.SampleTypeNo;                        
            //parameters[3].Value = model.zdy1;                        
            //parameters[4].Value = model.zdy2;                        
            //parameters[5].Value = model.SerialNo;                        
            //parameters[6].Value = model.zdy3;                        
            //parameters[7].Value = model.zdy4;                        
            //parameters[8].Value = model.zdy5;                        
            //parameters[9].Value = model.DeleteFlag;                        
            //parameters[10].Value = model.ItemSource;                        
            //parameters[11].Value = model.OldSerialNo;                        
            //parameters[12].Value = model.CountNodesItemSource;                        
            //parameters[13].Value = model.ReportFlag;                        
            //parameters[14].Value = model.PartFlag;                        
            //parameters[15].Value = model.WebLisOrgID;                        
            //parameters[16].Value = model.WebLisSourceOrgID;                        
            //parameters[17].Value = model.WebLisSourceOrgName;                        
            //parameters[18].Value = model.ClientNo;                        
            //parameters[19].Value = model.ClientName;                        
            //parameters[20].Value = model.NRequestFormNo;                        
            //parameters[21].Value = model.BarCodeFormNo;                        
            //parameters[22].Value = model.FormNo;                        
            //parameters[23].Value = model.TollItemNo;                        
            //parameters[24].Value = model.ParItemNo;                        
            //parameters[25].Value = model.IsCheckFee;                        
            //parameters[26].Value = model.ReceiveFlag;
            Common.Log.Log.Info(strSql.ToString());
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());

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
                        new SqlParameter("@CombiItemNo", SqlDbType.Int,4),  
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

            if (model.ItemSource != null)
            {
                strSql.Append("ItemSource=" + model.ItemSource + ",");
            }
            else
            {
                strSql.Append("ItemSource= null ,");
            }
            if (model.NRequestFormNo != null)
            {
                strSql.Append("NRequestFormNo=" + model.NRequestFormNo + ",");
            }
            if (model.BarCodeFormNo != null)
            {
                strSql.Append("BarCodeFormNo=" + model.BarCodeFormNo + ",");
            }
            else
            {
                strSql.Append("BarCodeFormNo= null ,");
            }
            if (model.FormNo != null)
            {
                strSql.Append("FormNo=" + model.FormNo + ",");
            }
            else
            {
                strSql.Append("FormNo= null ,");
            }
            if (model.TollItemNo != null)
            {
                strSql.Append("TollItemNo=" + model.TollItemNo + ",");
            }
            else
            {
                strSql.Append("TollItemNo= null ,");
            }
            if (model.ParItemNo != null)
            {
                strSql.Append("ParItemNo=" + model.ParItemNo + ",");
            }
            if (model.IsCheckFee != null)
            {
                strSql.Append("IsCheckFee=" + model.IsCheckFee + ",");
            }
            else
            {
                strSql.Append("IsCheckFee= null ,");
            }
            if (model.ReceiveFlag != null)
            {
                strSql.Append("ReceiveFlag=" + model.ReceiveFlag + ",");
            }
            else
            {
                strSql.Append("ReceiveFlag= null ,");
            }
            if (model.HISCharge != null)
            {
                strSql.Append("HISCharge=" + model.HISCharge + ",");
            }
            else
            {
                strSql.Append("HISCharge= null ,");
            }
            if (model.ItemCharge != null)
            {
                strSql.Append("ItemCharge=" + model.ItemCharge + ",");
            }
            else
            {
                strSql.Append("ItemCharge= null ,");
            }
            if (model.SampleTypeNo != null)
            {
                strSql.Append("SampleTypeNo=" + model.SampleTypeNo + ",");
            }
            else
            {
                strSql.Append("SampleTypeNo= null ,");
            }
            if (model.zdy1 != null)
            {
                strSql.Append("zdy1='" + model.zdy1 + "',");
            }
            else
            {
                strSql.Append("zdy1= null ,");
            }
            if (model.zdy2 != null)
            {
                strSql.Append("zdy2='" + model.zdy2 + "',");
            }
            else
            {
                strSql.Append("zdy2= null ,");
            }
            if (model.SerialNo != null)
            {
                strSql.Append("SerialNo='" + model.SerialNo + "',");
            }
            else
            {
                strSql.Append("SerialNo= null ,");
            }
            if (model.zdy3 != null)
            {
                strSql.Append("zdy3='" + model.zdy3 + "',");
            }
            else
            {
                strSql.Append("zdy3= null ,");
            }
            if (model.zdy4 != null)
            {
                strSql.Append("zdy4='" + model.zdy4 + "',");
            }
            else
            {
                strSql.Append("zdy4= null ,");
            }
            if (model.zdy5 != null)
            {
                strSql.Append("zdy5='" + model.zdy5 + "',");
            }
            else
            {
                strSql.Append("zdy5= null ,");
            }
            if (model.DeleteFlag != null)
            {
                strSql.Append("DeleteFlag=" + model.DeleteFlag + ",");
            }
            else
            {
                strSql.Append("DeleteFlag= null ,");
            }
            if (model.OldSerialNo != null)
            {
                strSql.Append("OldSerialNo='" + model.OldSerialNo + "',");
            }
            else
            {
                strSql.Append("OldSerialNo= null ,");
            }
            if (model.CountNodesItemSource != null)
            {
                strSql.Append("CountNodesItemSource='" + model.CountNodesItemSource + "',");
            }
            else
            {
                strSql.Append("CountNodesItemSource= null ,");
            }
            if (model.ReportFlag != null)
            {
                strSql.Append("ReportFlag=" + model.ReportFlag + ",");
            }
            else
            {
                strSql.Append("ReportFlag= null ,");
            }
            if (model.PartFlag != null)
            {
                strSql.Append("PartFlag=" + model.PartFlag + ",");
            }
            else
            {
                strSql.Append("PartFlag= null ,");
            }
            if (model.WebLisOrgID != null)
            {
                strSql.Append("WebLisOrgID='" + model.WebLisOrgID + "',");
            }
            if (model.WebLisSourceOrgID != null)
            {
                strSql.Append("WebLisSourceOrgID='" + model.WebLisSourceOrgID + "',");
            }
            else
            {
                strSql.Append("WebLisSourceOrgID= null ,");
            }
            if (model.WebLisSourceOrgName != null)
            {
                strSql.Append("WebLisSourceOrgName='" + model.WebLisSourceOrgName + "',");
            }
            else
            {
                strSql.Append("WebLisSourceOrgName= null ,");
            }
            if (model.ClientNo != null)
            {
                strSql.Append("ClientNo='" + model.ClientNo + "',");
            }
            else
            {
                strSql.Append("ClientNo= null ,");
            }
            if (model.ClientName != null)
            {
                strSql.Append("ClientName='" + model.ClientName + "',");
            }
            else
            {
                strSql.Append("ClientName= null ,");
            }
            //if (model.FlagDateDelete != null)
            //{
            //    strSql.Append("FlagDateDelete='" + model.FlagDateDelete + "',");
            //}
            //else
            //{
            //    strSql.Append("FlagDateDelete= null ,");
            //}
            //if (model.CollectDate != null)
            //{
            //    strSql.Append("CollectDate='" + model.CollectDate + "',");
            //}
            //else
            //{
            //    strSql.Append("CollectDate= null ,");
            //}
            //if (model.CollectTime != null)
            //{
            //    strSql.Append("CollectTime='" + model.CollectTime + "',");
            //}
            //else
            //{
            //    strSql.Append("CollectTime= null ,");
            //}
            //if (model.Collecter != null)
            //{
            //    strSql.Append("Collecter='" + model.Collecter + "',");
            //}
            //else
            //{
            //    strSql.Append("Collecter= null ,");
            //}
            //if (model.WebLisFlag != null)
            //{
            //    strSql.Append("WebLisFlag='" + model.WebLisFlag + "',");
            //}
            //else
            //{
            //    strSql.Append("WebLisFlag= null ,");
            //}
            //if (model.ParItemName != null)
            //{
            //    strSql.Append("ParItemName='" + model.ParItemName + "',");
            //}
            //else
            //{
            //    strSql.Append("ParItemName= null ,");
            //}
            //if (model.ParItemCode != null)
            //{
            //    strSql.Append("ParItemCode='" + model.ParItemCode + "',");
            //}
            //else
            //{
            //    strSql.Append("ParItemCode= null ,");
            //}
            //if (model.LABNREQUESTFORMNO != null)
            //{
            //    strSql.Append("LABNREQUESTFORMNO='" + model.LABNREQUESTFORMNO + "',");
            //}
            //else
            //{
            //    strSql.Append("LABNREQUESTFORMNO= null ,");
            //}
            //if (model.ComboId != null)
            //{
            //    strSql.Append("ComboId=" + model.ComboId + ",");
            //}
            //else
            //{
            //    strSql.Append("ComboId= null ,");
            //}
            if (model.CombiItemNo != null)
            {
                strSql.Append("CombiItemNo=" + model.CombiItemNo + ",");
            }
            else
            {
                strSql.Append("CombiItemNo= null ,");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where NRequestItemNo=" + model.NRequestItemNo + "");

            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());

            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("update NRequestItem set ");
            //strSql.Append(" HISCharge = @HISCharge , ");
            //strSql.Append(" ItemCharge = @ItemCharge , ");
            //strSql.Append(" SampleTypeNo = @SampleTypeNo , ");
            //strSql.Append(" zdy1 = @zdy1 , ");
            //strSql.Append(" zdy2 = @zdy2 , ");
            //strSql.Append(" SerialNo = @SerialNo , ");
            //strSql.Append(" zdy3 = @zdy3 , ");
            //strSql.Append(" zdy4 = @zdy4 , ");
            //strSql.Append(" zdy5 = @zdy5 , ");
            //strSql.Append(" DeleteFlag = @DeleteFlag , ");
            //strSql.Append(" ItemSource = @ItemSource , ");
            //strSql.Append(" OldSerialNo = @OldSerialNo , ");
            //strSql.Append(" CountNodesItemSource = @CountNodesItemSource , ");
            //strSql.Append(" ReportFlag = @ReportFlag , ");
            //strSql.Append(" PartFlag = @PartFlag , ");
            //strSql.Append(" WebLisOrgID = @WebLisOrgID , ");
            //strSql.Append(" WebLisSourceOrgID = @WebLisSourceOrgID , ");
            //strSql.Append(" WebLisSourceOrgName = @WebLisSourceOrgName , ");
            //strSql.Append(" ClientNo = @ClientNo , ");
            //strSql.Append(" ClientName = @ClientName , ");
            //strSql.Append(" NRequestFormNo = @NRequestFormNo , ");
            //strSql.Append(" BarCodeFormNo = @BarCodeFormNo , ");
            //strSql.Append(" FormNo = @FormNo , ");
            //strSql.Append(" TollItemNo = @TollItemNo , ");
            //strSql.Append(" ParItemNo = @ParItemNo , ");
            //strSql.Append(" IsCheckFee = @IsCheckFee , ");
            //strSql.Append(" ReceiveFlag = @ReceiveFlag  ");
            //strSql.Append(" where NRequestItemNo=@NRequestItemNo ");

            //SqlParameter[] parameters = {
            //            new SqlParameter("@NRequestItemNo", SqlDbType.Int,4) ,            
            //            new SqlParameter("@HISCharge", SqlDbType.Money,8) ,            
            //            new SqlParameter("@ItemCharge", SqlDbType.Money,8) ,            
            //            new SqlParameter("@SampleTypeNo", SqlDbType.Int,4) ,            
            //            new SqlParameter("@zdy1", SqlDbType.VarChar,80) ,            
            //            new SqlParameter("@zdy2", SqlDbType.VarChar,20) ,            
            //            new SqlParameter("@SerialNo", SqlDbType.NChar,30) ,            
            //            new SqlParameter("@zdy3", SqlDbType.VarChar,20) ,            
            //            new SqlParameter("@zdy4", SqlDbType.VarChar,20) ,            
            //            new SqlParameter("@zdy5", SqlDbType.VarChar,20) ,            
            //            new SqlParameter("@DeleteFlag", SqlDbType.Int,4) ,            
            //            new SqlParameter("@ItemSource", SqlDbType.Int,4) ,            
            //            new SqlParameter("@OldSerialNo", SqlDbType.VarChar,60) ,            
            //            new SqlParameter("@CountNodesItemSource", SqlDbType.Char,1) ,            
            //            new SqlParameter("@ReportFlag", SqlDbType.Int,4) ,            
            //            new SqlParameter("@PartFlag", SqlDbType.Int,4) ,            
            //            new SqlParameter("@WebLisOrgID", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@WebLisSourceOrgID", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@WebLisSourceOrgName", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@ClientNo", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@ClientName", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@NRequestFormNo", SqlDbType.BigInt,8) ,            
            //            new SqlParameter("@BarCodeFormNo", SqlDbType.BigInt,8) ,            
            //            new SqlParameter("@FormNo", SqlDbType.Int,4) ,            
            //            new SqlParameter("@TollItemNo", SqlDbType.Int,4) ,            
            //            new SqlParameter("@ParItemNo", SqlDbType.Int,4) ,            
            //            new SqlParameter("@IsCheckFee", SqlDbType.Int,4) ,            
            //            new SqlParameter("@ReceiveFlag", SqlDbType.Int,4)             

            //};
            //if (model.NRequestItemNo != null)
            //{
            //    parameters[0].Value = model.NRequestItemNo;
            //}
            //if (model.HISCharge != null)
            //{
            //    parameters[1].Value = model.HISCharge;
            //}
            //if (model.ItemCharge != null)
            //{
            //    parameters[2].Value = model.ItemCharge;
            //}
            //if (model.SampleTypeNo != null)
            //{
            //    parameters[3].Value = model.SampleTypeNo;
            //}
            //if (model.zdy1 != null)
            //{
            //    parameters[4].Value = model.zdy1;
            //}
            //if (model.zdy2 != null)
            //{
            //    parameters[5].Value = model.zdy2;
            //}
            //if (model.SerialNo != null)
            //{
            //    parameters[6].Value = model.SerialNo;
            //}
            //if (model.zdy3 != null)
            //{
            //    parameters[7].Value = model.zdy3;
            //}
            //if (model.zdy4 != null)
            //{
            //    parameters[8].Value = model.zdy4;
            //}
            //if (model.zdy5 != null)
            //{
            //    parameters[9].Value = model.zdy5;
            //}
            //if (model.DeleteFlag != null)
            //{
            //    parameters[10].Value = model.DeleteFlag;
            //}
            //if (model.ItemSource != null)
            //{
            //    parameters[11].Value = model.ItemSource;
            //}
            //if (model.OldSerialNo != null)
            //{
            //    parameters[12].Value = model.OldSerialNo;
            //}
            //if (model.CountNodesItemSource != null)
            //{
            //    parameters[13].Value = model.CountNodesItemSource;
            //}
            //if (model.ReportFlag != null)
            //{
            //    parameters[14].Value = model.ReportFlag;
            //}
            //if (model.PartFlag != null)
            //{
            //    parameters[15].Value = model.PartFlag;
            //}
            //if (model.WebLisOrgID != null)
            //{
            //    parameters[16].Value = model.WebLisOrgID;
            //}
            //if (model.WebLisSourceOrgID != null)
            //{
            //    parameters[17].Value = model.WebLisSourceOrgID;
            //}
            //if (model.WebLisSourceOrgName != null)
            //{
            //    parameters[18].Value = model.WebLisSourceOrgName;
            //}
            //if (model.ClientNo != null)
            //{
            //    parameters[19].Value = model.ClientNo;
            //}
            //if (model.ClientName != null)
            //{
            //    parameters[20].Value = model.ClientName;
            //}
            //if (model.NRequestFormNo != null)
            //{
            //    parameters[21].Value = model.NRequestFormNo;
            //}
            //if (model.BarCodeFormNo != null)
            //{
            //    parameters[22].Value = model.BarCodeFormNo;
            //}
            //if (model.FormNo != null)
            //{
            //    parameters[23].Value = model.FormNo;
            //}
            //if (model.TollItemNo != null)
            //{
            //    parameters[24].Value = model.TollItemNo;
            //}
            //if (model.ParItemNo != null)
            //{
            //    parameters[25].Value = model.ParItemNo;
            //}
            //if (model.IsCheckFee != null)
            //{
            //    parameters[26].Value = model.IsCheckFee;
            //}
            //if (model.ReceiveFlag != null)
            //{
            //    parameters[27].Value = model.ReceiveFlag;
            //}
            //return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);
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
            strSql.Append(" price = @LabCombiItemNo, ");
            strSql.Append(" price = @LabParItemNo  ");
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
                        new SqlParameter("@CombiItemNo", SqlDbType.Int,4),
                        new SqlParameter("@SampleType", SqlDbType.VarChar,10),  
                new SqlParameter("@CheckType", SqlDbType.VarChar,20),  
                new SqlParameter("@CheckTypeName", SqlDbType.VarChar,40),
                 new SqlParameter("@price", SqlDbType.Decimal),
                  new SqlParameter("@LabCombiItemNo", SqlDbType.Decimal),
                   new SqlParameter("@LabParItemNo", SqlDbType.Decimal)

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
        /// 删除一条数据
        /// </summary>
        public int Delete(int NRequestItemNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from NRequestItem ");
            strSql.Append(" where NRequestItemNo=" + NRequestItemNo + "");
            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("delete from NRequestItem ");
            //strSql.Append(" where NRequestItemNo=@NRequestItemNo");
            //SqlParameter[] parameters = {
            //        new SqlParameter("@NRequestItemNo", SqlDbType.Int,4)
            //};
            //parameters[0].Value = NRequestItemNo;
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
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

            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("select NRequestItemNo, HISCharge, ItemCharge, SampleTypeNo, zdy1, zdy2, SerialNo, zdy3, zdy4, zdy5, DeleteFlag, ItemSource, OldSerialNo, CountNodesItemSource, ReportFlag, PartFlag, WebLisOrgID, WebLisSourceOrgID, WebLisSourceOrgName, ClientNo, ClientName, NRequestFormNo, BarCodeFormNo, FormNo, TollItemNo, ParItemNo, IsCheckFee, ReceiveFlag  ");
            //strSql.Append("  from NRequestItem ");
            //strSql.Append(" where NRequestItemNo=@NRequestItemNo");
            //SqlParameter[] parameters = {
            //        new SqlParameter("@NRequestItemNo", SqlDbType.Int,4)
            //};
            //parameters[0].Value = NRequestItemNo;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" select NRequestItemNo, HISCharge, ItemCharge, SampleTypeNo, zdy1, zdy2, SerialNo, zdy3, zdy4, zdy5, DeleteFlag, ItemSource, OldSerialNo, CountNodesItemSource, ReportFlag, PartFlag, WebLisOrgID, WebLisSourceOrgID, WebLisSourceOrgName, ClientNo, ClientName, NRequestFormNo, BarCodeFormNo, FormNo, TollItemNo, ParItemNo, IsCheckFee, ReceiveFlag");
            strSql.Append(" from NRequestItem ");
            strSql.Append(" where NRequestItemNo=" + NRequestItemNo + "");

            ZhiFang.Model.NRequestItem model = new ZhiFang.Model.NRequestItem();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());

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
            strSql.Append("SELECT     TestItem.CName, TestItem.EName, TestItem.ShortName, TestItem.ShortCode, TestItem.IsDoctorItem, TestItem.IschargeItem, "
                      + "TestItem.isCombiItem, TestItem.IsProfile, TestItem.ItemNo, NRequestItem.* ");
            strSql.Append(" FROM TestItem RIGHT OUTER JOIN  NRequestItem ON TestItem.ItemNo = NRequestItem.ParItemNo ");
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
        /// 根据实体获取DataSet
        /// </summary>
        public DataSet GetList(ZhiFang.Model.NRequestItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   TestItem.CName, TestItem.EName, TestItem.ShortName, TestItem.ShortCode, TestItem.IsDoctorItem, TestItem.IschargeItem, "
                      + "TestItem.isCombiItem, TestItem.IsProfile, TestItem.ItemNo , NRequestItem.* ");
            strSql.Append(" FROM  TestItem RIGHT OUTER JOIN  NRequestItem ON TestItem.ItemNo = NRequestItem.ParItemNo where 1=1 ");

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

            strSql.Append(" SELECT     TestItem.CName, TestItem.EName, TestItem.ShortName, TestItem.ShortCode, TestItem.IsDoctorItem, TestItem.IschargeItem, "
                      + "TestItem.isCombiItem, TestItem.IsProfile, TestItem.ItemNo , NRequestItem.* ");
            strSql.Append(" FROM TestItem RIGHT OUTER JOIN  NRequestItem ON TestItem.ItemNo = NRequestItem.ParItemNo ");


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

            strSql.Append(" and ROWNUM <= '" + Top + "'");

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
            strSql.Append(" where NRequestFormNo=" + NRequestItemNolist + "");
            //strSql.Append(" where NRequestFormNo=@NRequestFormNo");
            //SqlParameter[] parameters = {
            //        new SqlParameter("@NRequestFormNo", SqlDbType.BigInt,8)
            //};
            //parameters[0].Value = NRequestItemNolist;
            if (DbHelperSQL.ExecuteNonQuery(strSql.ToString()) > 0)
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
            strSql.Append("SELECT     B_TestItemControl.ControlLabNo, B_TestItemControl.ItemNo AS Expr1, B_Lab_TestItem.*, NRequestItem.* FROM        B_Lab_TestItem INNER JOIN                      B_TestItemControl ON B_Lab_TestItem.LabCode = B_TestItemControl.ControlLabNo AND                       B_Lab_TestItem.ItemNo = B_TestItemControl.ControlItemNo INNER JOIN                      NRequestItem ON B_TestItemControl.ControlLabNo = NRequestItem.WebLisSourceOrgID AND                       B_TestItemControl.ItemNo = NRequestItem.ParItemNo WHERE     (1 = 1)");

            if (model.HISCharge != null)
            {
                strSql.Append(" and  NRequestItem.HISCharge=" + model.HISCharge + " ");
            }

            if (model.ItemCharge != null)
            {
                strSql.Append(" and  NRequestItem.ItemCharge=" + model.ItemCharge + " ");
            }

            if (model.SampleTypeNo != null)
            {
                strSql.Append(" and  NRequestItem.SampleTypeNo=" + model.SampleTypeNo + " ");
            }

            if (model.zdy1 != null)
            {
                strSql.Append(" and  NRequestItem.zdy1='" + model.zdy1 + "' ");
            }

            if (model.zdy2 != null)
            {
                strSql.Append(" and  NRequestItem.zdy2='" + model.zdy2 + "' ");
            }

            if (model.SerialNo != null)
            {
                strSql.Append(" and  NRequestItem.SerialNo='" + model.SerialNo + "' ");
            }

            if (model.zdy3 != null)
            {
                strSql.Append(" and  NRequestItem.zdy3='" + model.zdy3 + "' ");
            }

            if (model.zdy4 != null)
            {
                strSql.Append(" and  NRequestItem.zdy4='" + model.zdy4 + "' ");
            }

            if (model.zdy5 != null)
            {
                strSql.Append(" and  NRequestItem.zdy5='" + model.zdy5 + "' ");
            }

            if (model.DeleteFlag != null)
            {
                strSql.Append(" and  NRequestItem.DeleteFlag=" + model.DeleteFlag + " ");
            }

            if (model.ItemSource != null)
            {
                strSql.Append(" and  NRequestItem.ItemSource=" + model.ItemSource + " ");
            }

            if (model.OldSerialNo != null)
            {
                strSql.Append(" and  NRequestItem.OldSerialNo='" + model.OldSerialNo + "' ");
            }

            if (model.CountNodesItemSource != null)
            {
                strSql.Append(" and  NRequestItem.CountNodesItemSource='" + model.CountNodesItemSource + "' ");
            }

            if (model.ReportFlag != null)
            {
                strSql.Append(" and  NRequestItem.ReportFlag=" + model.ReportFlag + " ");
            }

            if (model.PartFlag != null)
            {
                strSql.Append(" and  NRequestItem.PartFlag=" + model.PartFlag + " ");
            }

            if (model.WebLisOrgID != null)
            {
                strSql.Append(" and  NRequestItem.WebLisOrgID='" + model.WebLisOrgID + "' ");
            }

            if (model.WebLisSourceOrgID != null)
            {
                strSql.Append(" and  NRequestItem.WebLisSourceOrgID='" + model.WebLisSourceOrgID + "' ");
            }

            if (model.WebLisSourceOrgName != null)
            {
                strSql.Append(" and  NRequestItem.WebLisSourceOrgName='" + model.WebLisSourceOrgName + "' ");
            }

            if (model.ClientNo != null)
            {
                strSql.Append(" and  NRequestItem.ClientNo='" + model.ClientNo + "' ");
            }

            if (model.ClientName != null)
            {
                strSql.Append(" and  NRequestItem.ClientName='" + model.ClientName + "' ");
            }

            if (model.NRequestFormNo != null)
            {
                strSql.Append(" and  NRequestItem.NRequestFormNo='" + model.NRequestFormNo + "' ");
            }

            if (model.BarCodeFormNo != null)
            {
                strSql.Append(" and  NRequestItem.BarCodeFormNo=" + model.BarCodeFormNo + " ");
            }

            if (model.FormNo != null)
            {
                strSql.Append(" and  NRequestItem.FormNo=" + model.FormNo + " ");
            }

            if (model.TollItemNo != null)
            {
                strSql.Append(" and  NRequestItem.TollItemNo=" + model.TollItemNo + " ");
            }

            if (model.ParItemNo != null)
            {
                strSql.Append(" and  NRequestItem.ParItemNo=" + model.ParItemNo + " ");
            }

            if (model.IsCheckFee != null)
            {
                strSql.Append(" and  NRequestItem.IsCheckFee=" + model.IsCheckFee + " ");
            }

            if (model.ReceiveFlag != null)
            {
                strSql.Append(" and  NRequestItem.ReceiveFlag=" + model.ReceiveFlag + " ");
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        #endregion


        public int UpdateByBarcodeFormNo(Model.NRequestItem nRequestItem)
        {
            string strSql = "update  NRequestItem set WebLisOrgID='" + nRequestItem.WebLisOrgID + "' where BarCodeFormNo=" + nRequestItem.BarCodeFormNo;
            return DbHelperSQL.ExecuteNonQuery(strSql);
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
            strSql.Append("from( select LabCombiItemNo,price,ClientNo  from NRequestItem where NRequestFormNo='" + nrequestNo + "') a  ");
            strSql.Append("join B_Lab_TestItem b on a.ClientNo=b.LabCode and a.LabCombiItemNo=b.ItemNo group by LabCombiItemNo,ClientNo,CName; ");

            ZhiFang.Common.Log.Log.Info("获取申请单组套项目:" + strSql.ToString());
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
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
            strSql.Append("SELECT   NRequestItem.NRequestItemNo, NRequestItem.HISCharge, NRequestItem.ItemCharge, NRequestItem.zdy1, NRequestItem.zdy2,");
            strSql.Append("NRequestItem.SerialNo, NRequestItem.zdy3, NRequestItem.zdy4, NRequestItem.zdy5, NRequestItem.DeleteFlag, ");
            strSql.Append("NRequestItem.ItemSource, NRequestItem.OldSerialNo, NRequestItem.CountNodesItemSource, NRequestItem.ReportFlag, ");
            strSql.Append("NRequestItem.PartFlag, NRequestItem.WebLisOrgID, NRequestItem.WebLisSourceOrgID, NRequestItem.WebLisSourceOrgName,");
            strSql.Append("NRequestItem.ClientNo, NRequestItem.ClientName, NRequestItem.NRequestFormNo, NRequestItem.BarCodeFormNo, NRequestItem.FormNo, ");
            strSql.Append("NRequestItem.TollItemNo, NRequestItem.IsCheckFee, NRequestItem.ReceiveFlag, NRequestItem.CombiItemNo, ");
            strSql.Append("TestItem.CName AS ParItemName, TestItem_1.CName AS CombiItemName, BarCodeForm.BarCode, BarCodeForm.SampleTypeNo, NRequestItem.ParItemNo");
            strSql.Append(" FROM  NRequestItem INNER JOIN");
            strSql.Append(" BarCodeForm ON NRequestItem.BarCodeFormNo = BarCodeForm.BarCodeFormNo INNER JOIN");
            strSql.Append(" TestItem ON NRequestItem.ParItemNo = TestItem.ItemNo INNER JOIN TestItem TestItem_1 ON NRequestItem.CombiItemNo = TestItem_1.ItemNo");

            if (nrequestNo != null)
            {
                strSql.Append(" where  nrequestitem.nrequestformno=" + nrequestNo);
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }


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

