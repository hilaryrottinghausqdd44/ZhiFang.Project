using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
using ZhiFang.Model;
using System.Linq;

namespace ZhiFang.DAL.MsSql.Weblis
{
    //equestForm		
    public partial class NRequestForm : BaseDALLisDB, IDNRequestForm
    {
        DBUtility.IDBConnection idb;
        public NRequestForm(string dbsourceconn)
        {
            idb = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public NRequestForm()
        {
            Common.Log.Log.Info(ZhiFang.Common.Dictionary.DBSource.LisDB());
            idb = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.LisDB());
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        //public int Add(ZhiFang.Model.NRequestForm model)
        //{
        //    try
        //    {
        //        StringBuilder strSql = new StringBuilder();
        //        strSql.Append("insert into NRequestForm(");
        //        strSql.Append("ClientNo,TestTypeName,CName,DoctorName,CollecterName,SampleTypeName,SerialNo,ReceiveFlag,StatusNo,SampleTypeNo,PatNo,ClientName,GenderNo,Birthday,Age,AgeUnitNo,FolkNo,DistrictNo,WardNo,Bed,DeptNo,Doctor,AgeUnitName,DiagNo,Diag,ChargeNo,Charge,Chargeflag,CountNodesFormSource,IsCheckFee,Operator,OperDate,OperTime,GenderName,FormMemo,RequestSource,SickOrder,jztype,zdy1,zdy2,zdy3,zdy4,zdy5,FlagDateDelete,DeptName,NurseFlag,IsByHand,TestTypeNo,ExecDeptNo,CollectDate,CollectTime,Collecter,LABCENTER,NRequestFormNo,CheckNo,DistrictName,CheckName,WebLisSourceOrgID,WebLisSourceOrgName,WardName,FolkName,ClinicTypeName,PersonID,SampleType,TelNo,WebLisOrgID,WebLisOrgName,STATUSName,jztypeName,barcode,CombiItemName,printTimes,price,zdy6,zdy7,zdy8,zdy9,zdy10");
        //        strSql.Append(") values (");
        //        strSql.Append("@ClientNo,@TestTypeName,@CName,@DoctorName,@CollecterName,@SampleTypeName,@SerialNo,@ReceiveFlag,@StatusNo,@SampleTypeNo,@PatNo,@ClientName,@GenderNo,@Birthday,@Age,@AgeUnitNo,@FolkNo,@DistrictNo,@WardNo,@Bed,@DeptNo,@Doctor,@AgeUnitName,@DiagNo,@Diag,@ChargeNo,@Charge,@Chargeflag,@CountNodesFormSource,@IsCheckFee,@Operator,@OperDate,@OperTime,@GenderName,@FormMemo,@RequestSource,@SickOrder,@jztype,@zdy1,@zdy2,@zdy3,@zdy4,@zdy5,@FlagDateDelete,@DeptName,@NurseFlag,@IsByHand,@TestTypeNo,@ExecDeptNo,@CollectDate,@CollectTime,@Collecter,@LABCENTER,@NRequestFormNo,@CheckNo,@DistrictName,@CheckName,@WebLisSourceOrgID,@WebLisSourceOrgName,@WardName,@FolkName,@ClinicTypeName,@PersonID,@SampleType,@TelNo,@WebLisOrgID,@WebLisOrgName,@STATUSName,@jztypeName,@barcode,@CombiItemName,@printTimes,@price,@zdy6,@zdy7,@zdy8,@zdy9,@zdy10");
        //        strSql.Append(") ");

        //        SqlParameter[] parameters = {
        //                new SqlParameter("@ClientNo", SqlDbType.VarChar,50) ,
        //                new SqlParameter("@TestTypeName", SqlDbType.VarChar,10) ,
        //                new SqlParameter("@CName", SqlDbType.VarChar,30) ,
        //                new SqlParameter("@DoctorName", SqlDbType.VarChar,10) ,
        //                new SqlParameter("@CollecterName", SqlDbType.VarChar,10) ,
        //                new SqlParameter("@SampleTypeName", SqlDbType.VarChar,50) ,
        //                new SqlParameter("@SerialNo", SqlDbType.VarChar,30) ,
        //                new SqlParameter("@ReceiveFlag", SqlDbType.Int,4) ,
        //                new SqlParameter("@StatusNo", SqlDbType.Int,4) ,
        //                new SqlParameter("@SampleTypeNo", SqlDbType.Int,4) ,
        //                new SqlParameter("@PatNo", SqlDbType.VarChar,20) ,
        //                new SqlParameter("@ClientName", SqlDbType.VarChar,50) ,
        //                new SqlParameter("@GenderNo", SqlDbType.Int,4) ,
        //                new SqlParameter("@Birthday", SqlDbType.DateTime) ,
        //                new SqlParameter("@Age", SqlDbType.Float,8) ,
        //                new SqlParameter("@AgeUnitNo", SqlDbType.Int,4) ,
        //                new SqlParameter("@FolkNo", SqlDbType.Int,4) ,
        //                new SqlParameter("@DistrictNo", SqlDbType.Int,4) ,
        //                new SqlParameter("@WardNo", SqlDbType.Int,4) ,
        //                new SqlParameter("@Bed", SqlDbType.VarChar,10) ,
        //                new SqlParameter("@DeptNo", SqlDbType.Int,4) ,
        //                new SqlParameter("@Doctor", SqlDbType.Int,4) ,
        //                new SqlParameter("@AgeUnitName", SqlDbType.VarChar,10) ,
        //                new SqlParameter("@DiagNo", SqlDbType.Int,4) ,
        //                new SqlParameter("@Diag", SqlDbType.VarChar,100) ,
        //                new SqlParameter("@ChargeNo", SqlDbType.Int,4) ,
        //                new SqlParameter("@Charge", SqlDbType.Money,8) ,
        //                new SqlParameter("@Chargeflag", SqlDbType.VarChar,10) ,
        //                new SqlParameter("@CountNodesFormSource", SqlDbType.Char,1) ,
        //                new SqlParameter("@IsCheckFee", SqlDbType.Int,4) ,
        //                new SqlParameter("@Operator", SqlDbType.VarChar,20) ,
        //                new SqlParameter("@OperDate", SqlDbType.DateTime) ,
        //                new SqlParameter("@OperTime", SqlDbType.DateTime) ,
        //                new SqlParameter("@GenderName", SqlDbType.VarChar,10) ,
        //                new SqlParameter("@FormMemo", SqlDbType.VarChar,40) ,
        //                new SqlParameter("@RequestSource", SqlDbType.VarChar,20) ,
        //                new SqlParameter("@SickOrder", SqlDbType.VarChar,20) ,
        //                new SqlParameter("@jztype", SqlDbType.Int,4) ,
        //                new SqlParameter("@zdy1", SqlDbType.VarChar,50) ,
        //                new SqlParameter("@zdy2", SqlDbType.VarChar,50) ,
        //                new SqlParameter("@zdy3", SqlDbType.VarChar,50) ,
        //                new SqlParameter("@zdy4", SqlDbType.VarChar,50) ,
        //                new SqlParameter("@zdy5", SqlDbType.VarChar,50) ,
        //                new SqlParameter("@FlagDateDelete", SqlDbType.DateTime) ,
        //                new SqlParameter("@DeptName", SqlDbType.VarChar,40) ,
        //                new SqlParameter("@NurseFlag", SqlDbType.Int,4) ,
        //                new SqlParameter("@IsByHand", SqlDbType.Bit,1) ,
        //                new SqlParameter("@TestTypeNo", SqlDbType.Int,4) ,
        //                new SqlParameter("@ExecDeptNo", SqlDbType.Int,4) ,
        //                new SqlParameter("@CollectDate", SqlDbType.DateTime) ,
        //                new SqlParameter("@CollectTime", SqlDbType.DateTime) ,
        //                new SqlParameter("@Collecter", SqlDbType.VarChar,10) ,
        //                new SqlParameter("@LABCENTER", SqlDbType.VarChar,50) ,
        //                new SqlParameter("@NRequestFormNo", SqlDbType.BigInt) ,
        //                new SqlParameter("@CheckNo", SqlDbType.VarChar,50) ,
        //                new SqlParameter("@DistrictName", SqlDbType.VarChar,20) ,
        //                new SqlParameter("@CheckName", SqlDbType.VarChar,50) ,
        //                new SqlParameter("@WebLisSourceOrgID", SqlDbType.VarChar,50) ,
        //                new SqlParameter("@WebLisSourceOrgName", SqlDbType.VarChar,50) ,
        //                new SqlParameter("@WardName", SqlDbType.VarChar,40) ,
        //                new SqlParameter("@FolkName", SqlDbType.VarChar,20) ,
        //                new SqlParameter("@ClinicTypeName", SqlDbType.VarChar,20),
        //                new SqlParameter("@PersonID", SqlDbType.VarChar,20),
        //                new SqlParameter("@SampleType", SqlDbType.VarChar,10),
        //                new SqlParameter("@TelNo", SqlDbType.VarChar,40),
        //                new SqlParameter("@WebLisOrgID", SqlDbType.VarChar,50),
        //                new SqlParameter("@WebLisOrgName", SqlDbType.VarChar,150),
        //                new SqlParameter("@STATUSName", SqlDbType.VarChar,50),
        //                new SqlParameter("@jztypeName", SqlDbType.VarChar,40),
        //                 new SqlParameter("@barcode", SqlDbType.VarChar,200),
        //                  new SqlParameter("@CombiItemName", SqlDbType.VarChar,200),
        //                  new SqlParameter("@printTimes", SqlDbType.Int,4),
        //                  new SqlParameter("@price", SqlDbType.Decimal),
        //                  new SqlParameter("@zdy6",SqlDbType.VarChar,60),
        //                  new SqlParameter("@zdy7",SqlDbType.VarChar,60),
        //                  new SqlParameter("@zdy8",SqlDbType.VarChar,60),
        //                  new SqlParameter("@zdy9",SqlDbType.VarChar,60),
        //                  new SqlParameter("@zdy10",SqlDbType.VarChar,60)

        //    };

        //        parameters[0].Value = model.ClientNo;
        //        parameters[1].Value = model.TestTypeName;
        //        parameters[2].Value = model.CName;
        //        parameters[3].Value = model.DoctorName;
        //        parameters[4].Value = model.CollecterName;
        //        parameters[5].Value = model.SampleTypeName;
        //        parameters[6].Value = model.SerialNo;
        //        parameters[7].Value = model.ReceiveFlag;
        //        parameters[8].Value = model.StatusNo;
        //        parameters[9].Value = model.SampleTypeNo;
        //        parameters[10].Value = model.PatNo;
        //        parameters[11].Value = model.ClientName;
        //        parameters[12].Value = model.GenderNo;
        //        parameters[13].Value = model.Birthday;
        //        parameters[14].Value = model.Age;
        //        parameters[15].Value = model.AgeUnitNo;
        //        parameters[16].Value = model.FolkNo;
        //        parameters[17].Value = model.DistrictNo;
        //        parameters[18].Value = model.WardNo;
        //        parameters[19].Value = model.Bed;
        //        parameters[20].Value = model.DeptNo;
        //        parameters[21].Value = model.Doctor;
        //        parameters[22].Value = model.AgeUnitName;
        //        parameters[23].Value = model.DiagNo;
        //        parameters[24].Value = model.Diag;
        //        parameters[25].Value = model.ChargeNo;
        //        parameters[26].Value = model.Charge;
        //        parameters[27].Value = model.Chargeflag;
        //        parameters[28].Value = model.CountNodesFormSource;
        //        parameters[29].Value = model.IsCheckFee;
        //        parameters[30].Value = model.Operator;
        //        parameters[31].Value = model.OperDate;
        //        parameters[32].Value = model.OperTime;
        //        parameters[33].Value = model.GenderName;
        //        parameters[34].Value = model.FormMemo;
        //        parameters[35].Value = model.RequestSource;
        //        parameters[36].Value = model.SickOrder;
        //        parameters[37].Value = model.jztype;
        //        parameters[38].Value = model.zdy1;
        //        parameters[39].Value = model.zdy2;
        //        parameters[40].Value = model.zdy3;
        //        parameters[41].Value = model.zdy4;
        //        parameters[42].Value = model.zdy5;
        //        parameters[43].Value = model.FlagDateDelete;
        //        parameters[44].Value = model.DeptName;
        //        parameters[45].Value = model.NurseFlag;
        //        parameters[46].Value = model.IsByHand;
        //        parameters[47].Value = model.TestTypeNo;
        //        parameters[48].Value = model.ExecDeptNo;
        //        parameters[49].Value = model.CollectDate;
        //        parameters[50].Value = model.CollectTime;
        //        parameters[51].Value = model.Collecter;
        //        parameters[52].Value = model.LABCENTER;
        //        parameters[53].Value = model.NRequestFormNo;
        //        parameters[54].Value = model.CheckNo;
        //        parameters[55].Value = model.DistrictName;
        //        parameters[56].Value = model.CheckName;
        //        parameters[57].Value = model.WebLisSourceOrgID;
        //        parameters[58].Value = model.WebLisSourceOrgName;
        //        parameters[59].Value = model.WardName;
        //        parameters[60].Value = model.FolkName;
        //        parameters[61].Value = model.ClinicTypeName;
        //        parameters[62].Value = model.PersonID;
        //        parameters[63].Value = model.SampleType;
        //        parameters[64].Value = model.TelNo;
        //        parameters[65].Value = model.WebLisOrgID;
        //        parameters[66].Value = model.WebLisOrgName;
        //        parameters[67].Value = model.STATUSName;
        //        parameters[68].Value = model.jztypeName;
        //        parameters[69].Value = model.BarCode;
        //        parameters[70].Value = model.CombiItemName;
        //        parameters[71].Value = model.PrintTimes;
        //        parameters[72].Value = model.Price;
        //        parameters[73].Value = model.ZDY6;
        //        parameters[74].Value = model.ZDY7;
        //        parameters[75].Value = model.ZDY8;
        //        parameters[76].Value = model.ZDY9;
        //        parameters[77].Value = model.ZDY10;
        //        Common.Log.Log.Info(strSql.ToString());
        //        Common.Log.Log.Debug("ZhiFang.DAL.MsSql.Weblis.NRequestForm.Add.NRequestFormNo：" + model.NRequestFormNo);
        //        return idb.ExecuteNonQuery(strSql.ToString(), parameters);
        //    }
        //    catch (Exception e)
        //    {
        //        Common.Log.Log.Debug("错误信息：" + e.ToString() + "字符串连接:" + idb.ConnectionString);
        //        return 0;
        //    }
        //}

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.NRequestForm model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
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
            if (model.AgeUnitName != null)
            {
                strSql1.Append("AgeUnitName,");
                strSql2.Append("'" + model.AgeUnitName + "',");
            }
            if (model.GenderName != null)
            {
                strSql1.Append("GenderName,");
                strSql2.Append("'" + model.GenderName + "',");
            }
            if (model.DeptName != null)
            {
                strSql1.Append("DeptName,");
                strSql2.Append("'" + model.DeptName + "',");
            }
            if (model.DistrictName != null)
            {
                strSql1.Append("DistrictName,");
                strSql2.Append("'" + model.DistrictName + "',");
            }
            if (model.WardName != null)
            {
                strSql1.Append("WardName,");
                strSql2.Append("'" + model.WardName + "',");
            }
            if (model.FolkName != null)
            {
                strSql1.Append("FolkName,");
                strSql2.Append("'" + model.FolkName + "',");
            }
            if (model.ClinicTypeName != null)
            {
                strSql1.Append("ClinicTypeName,");
                strSql2.Append("'" + model.ClinicTypeName + "',");
            }
            if (model.TestTypeName != null)
            {
                strSql1.Append("TestTypeName,");
                strSql2.Append("'" + model.TestTypeName + "',");
            }
            if (model.CName != null)
            {
                strSql1.Append("CName,");
                strSql2.Append("'" + model.CName + "',");
            }
            if (model.DoctorName != null)
            {
                strSql1.Append("DoctorName,");
                strSql2.Append("'" + model.DoctorName + "',");
            }
            if (model.CollecterName != null)
            {
                strSql1.Append("CollecterName,");
                strSql2.Append("'" + model.CollecterName + "',");
            }
            if (model.SampleTypeName != null)
            {
                strSql1.Append("SampleTypeName,");
                strSql2.Append("'" + model.SampleTypeName + "',");
            }
            if (model.SerialNo != null)
            {
                strSql1.Append("SerialNo,");
                strSql2.Append("'" + model.SerialNo + "',");
            }
            if (model.ReceiveFlag != null)
            {
                strSql1.Append("ReceiveFlag,");
                strSql2.Append("" + model.ReceiveFlag + ",");
            }
            if (model.StatusNo != null)
            {
                strSql1.Append("StatusNo,");
                strSql2.Append("" + model.StatusNo + ",");
            }
            if (model.SampleTypeNo != null)
            {
                strSql1.Append("SampleTypeNo,");
                strSql2.Append("" + model.SampleTypeNo + ",");
            }
            if (model.PatNo != null)
            {
                strSql1.Append("PatNo,");
                strSql2.Append("'" + model.PatNo + "',");
            }
            if (model.GenderNo != null)
            {
                strSql1.Append("GenderNo,");
                strSql2.Append("" + model.GenderNo + ",");
            }
            if (model.Birthday != null)
            {
                strSql1.Append("Birthday,");
                strSql2.Append("'" + model.Birthday + "',");
            }
            if (model.Age != null)
            {
                strSql1.Append("Age,");
                strSql2.Append("" + model.Age + ",");
            }
            if (model.AgeUnitNo != null)
            {
                strSql1.Append("AgeUnitNo,");
                strSql2.Append("" + model.AgeUnitNo + ",");
            }
            if (model.FolkNo != null)
            {
                strSql1.Append("FolkNo,");
                strSql2.Append("" + model.FolkNo + ",");
            }
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
            if (model.Bed != null)
            {
                strSql1.Append("Bed,");
                strSql2.Append("'" + model.Bed + "',");
            }
            if (model.DeptNo != null)
            {
                strSql1.Append("DeptNo,");
                strSql2.Append("" + model.DeptNo + ",");
            }
            if (model.Doctor != null)
            {
                strSql1.Append("Doctor,");
                strSql2.Append("" + model.Doctor + ",");
            }
            if (model.DiagNo != null)
            {
                strSql1.Append("DiagNo,");
                strSql2.Append("" + model.DiagNo + ",");
            }
            if (model.Diag != null)
            {
                strSql1.Append("Diag,");
                strSql2.Append("'" + model.Diag + "',");
            }
            if (model.ChargeNo != null)
            {
                strSql1.Append("ChargeNo,");
                strSql2.Append("" + model.ChargeNo + ",");
            }
            if (model.Charge != null)
            {
                strSql1.Append("Charge,");
                strSql2.Append("" + model.Charge + ",");
            }
            if (model.Chargeflag != null)
            {
                strSql1.Append("Chargeflag,");
                strSql2.Append("'" + model.Chargeflag + "',");
            }
            if (model.CountNodesFormSource != null)
            {
                strSql1.Append("CountNodesFormSource,");
                strSql2.Append("'" + model.CountNodesFormSource + "',");
            }
            if (model.IsCheckFee != null)
            {
                strSql1.Append("IsCheckFee,");
                strSql2.Append("" + model.IsCheckFee + ",");
            }
            if (model.Operator != null)
            {
                strSql1.Append("Operator,");
                strSql2.Append("'" + model.Operator + "',");
            }
            if (model.OperDate != null)
            {
                strSql1.Append("OperDate,");
                strSql2.Append("'" + model.OperDate + "',");
            }
            if (model.OperTime != null)
            {
                strSql1.Append("OperTime,");
                strSql2.Append("'" + model.OperTime + "',");
            }
            if (model.FormMemo != null)
            {
                strSql1.Append("FormMemo,");
                strSql2.Append("'" + model.FormMemo + "',");
            }
            if (model.RequestSource != null)
            {
                strSql1.Append("RequestSource,");
                strSql2.Append("'" + model.RequestSource + "',");
            }
            if (model.SickOrder != null)
            {
                strSql1.Append("SickOrder,");
                strSql2.Append("'" + model.SickOrder + "',");
            }
            if (model.jztype != null)
            {
                strSql1.Append("jztype,");
                strSql2.Append("" + model.jztype + ",");
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
            if (model.FlagDateDelete != null)
            {
                strSql1.Append("FlagDateDelete,");
                strSql2.Append("'" + model.FlagDateDelete + "',");
            }
            if (model.NurseFlag != null)
            {
                strSql1.Append("NurseFlag,");
                strSql2.Append("" + model.NurseFlag + ",");
            }
            if (model.TestTypeNo != null)
            {
                strSql1.Append("TestTypeNo,");
                strSql2.Append("" + model.TestTypeNo + ",");
            }
            if (model.ExecDeptNo != null)
            {
                strSql1.Append("ExecDeptNo,");
                strSql2.Append("" + model.ExecDeptNo + ",");
            }
            if (model.CollectDate != null)
            {
                strSql1.Append("CollectDate,");
                strSql2.Append("'" + model.CollectDate + "',");
            }
            if (model.CollectTime != null)
            {
                strSql1.Append("CollectTime,");
                strSql2.Append("'" + model.CollectTime + "',");
            }
            if (model.Collecter != null)
            {
                strSql1.Append("Collecter,");
                strSql2.Append("'" + model.Collecter + "',");
            }
            if (model.LABCENTER != null)
            {
                strSql1.Append("LABCENTER,");
                strSql2.Append("'" + model.LABCENTER + "',");
            }
            if (model.NRequestFormNo != null)
            {
                strSql1.Append("NRequestFormNo,");
                strSql2.Append("" + model.NRequestFormNo + ",");
            }
            if (model.CheckNo != null)
            {
                strSql1.Append("CheckNo,");
                strSql2.Append("'" + model.CheckNo + "',");
            }
            if (model.CheckName != null)
            {
                strSql1.Append("CheckName,");
                strSql2.Append("'" + model.CheckName + "',");
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
            if (model.OldSerialNo != null)
            {
                strSql1.Append("OldSerialNo,");
                strSql2.Append("'" + model.OldSerialNo + "',");
            }
            if (model.Weblisflag != null)
            {
                strSql1.Append("WeblisFlag,");
                strSql2.Append("'" + model.Weblisflag + "',");
            }
            if (!string.IsNullOrEmpty(model.Nationality))
            {
                strSql1.Append("Nationality,");
                strSql2.Append("'" + model.Nationality + "',");
            }
            if (!string.IsNullOrEmpty(model.PassportNo))
            {
                strSql1.Append("PassportNo,");
                strSql2.Append("'" + model.PassportNo + "',");
            }
            if (model.SMITypeId.HasValue)
            {
                strSql1.Append("SMITypeId,");
                strSql2.Append(" " + model.SMITypeId + ",");
            }

            if (!string.IsNullOrEmpty(model.SMITypeName))
            {
                strSql1.Append("SMITypeName,");
                strSql2.Append("'" + model.SMITypeName + "',");
            }

            if (!string.IsNullOrEmpty(model.NCPTestTypeNo))
            {
                strSql1.Append("NCPTestTypeNo,");
                strSql2.Append("'" + model.NCPTestTypeNo + "',");
            }

            if (!string.IsNullOrEmpty(model.IDCardAddress))
            {
                strSql1.Append("IDCardAddress,");
                strSql2.Append("'" + model.IDCardAddress + "',");
            }

            if (model.CollectTypeId.HasValue)
            {
                strSql1.Append("CollectTypeId,");
                strSql2.Append("" + model.CollectTypeId + ",");
            }

            if (!string.IsNullOrEmpty(model.CollectTypeName))
            {
                strSql1.Append("CollectTypeName,");
                strSql2.Append("'" + model.CollectTypeName + "',");
            }

            if (model.FlagDateUpload != null)
            {
                strSql1.Append("FlagDateUpload,");
                strSql2.Append("'" + model.FlagDateUpload + "',");
            }
            if (model.PersonID != null)
            {
                strSql1.Append("PersonID,");
                strSql2.Append("'" + model.PersonID + "',");
            }
            if (model.SampleType != null)
            {
                strSql1.Append("SampleType,");
                strSql2.Append("'" + model.SampleType + "',");
            }
            if (model.TelNo != null)
            {
                strSql1.Append("TelNo,");
                strSql2.Append("'" + model.TelNo + "',");
            }
            if (model.WebLisOrgID != null)
            {
                strSql1.Append("WebLisOrgID,");
                strSql2.Append("'" + model.WebLisOrgID + "',");
            }
            if (model.WebLisOrgName != null)
            {
                strSql1.Append("WebLisOrgName,");
                strSql2.Append("'" + model.WebLisOrgName + "',");
            }
            if (model.STATUSName != null)
            {
                strSql1.Append("STATUSName,");
                strSql2.Append("'" + model.STATUSName + "',");
            }
            if (model.jztypeName != null)
            {
                strSql1.Append("jztypeName,");
                strSql2.Append("'" + model.jztypeName + "',");
            }
            if (model.PrintTimes != null)
            {
                strSql1.Append("printTimes,");
                strSql2.Append("" + model.PrintTimes + ",");
            }
            if (model.BarCode != null)
            {
                strSql1.Append("barcode,");
                strSql2.Append("'" + model.BarCode + "',");
            }
            if (model.CombiItemName != null)
            {
                strSql1.Append("CombiItemName,");
                strSql2.Append("'" + model.CombiItemName + "',");
            }
            if (model.Price != null)
            {
                strSql1.Append("price,");
                strSql2.Append("" + model.Price + ",");
            }
            if (model.UrgentState != null)
            {
                strSql1.Append("UrgentState,");
                strSql2.Append("'" + model.UrgentState + "',");
            }
            if (model.ZDY9 != null)
            {
                strSql1.Append("Zdy9,");
                strSql2.Append("'" + model.ZDY9 + "',");
            }
            if (model.ZDY6 != null)
            {
                strSql1.Append("ZDY6,");
                strSql2.Append("'" + model.ZDY6 + "',");
            }
            if (model.ZDY7 != null)
            {
                strSql1.Append("ZDY7,");
                strSql2.Append("'" + model.ZDY7 + "',");
            }
            if (model.ZDY8 != null)
            {
                strSql1.Append("ZDY8,");
                strSql2.Append("'" + model.ZDY8 + "',");
            }
            if (model.ZDY10 != null)
            {
                strSql1.Append("ZDY10,");
                strSql2.Append("'" + model.ZDY10 + "',");
            }
            if (model.ZDY11 != null)
            {
                strSql1.Append("zdy11,");
                strSql2.Append("'" + model.ZDY11 + "',");
            }
            if (model.ZDY12 != null)
            {
                strSql1.Append("zdy12,");
                strSql2.Append("'" + model.ZDY12 + "',");
            }
            if (model.ZDY13 != null)
            {
                strSql1.Append("zdy13,");
                strSql2.Append("'" + model.ZDY13 + "',");
            }
            if (model.ZDY14 != null)
            {
                strSql1.Append("zdy14,");
                strSql2.Append("'" + model.ZDY14 + "',");
            }
            if (model.ZDY15 != null)
            {
                strSql1.Append("zdy15,");
                strSql2.Append("'" + model.ZDY15 + "',");
            }
            if (model.ZDY16 != null)
            {
                strSql1.Append("zdy16,");
                strSql2.Append("'" + model.ZDY16 + "',");
            }
            if (model.ZDY17 != null)
            {
                strSql1.Append("zdy17,");
                strSql2.Append("'" + model.ZDY17 + "',");
            }
            if (model.ZDY18 != null)
            {
                strSql1.Append("zdy18,");
                strSql2.Append("'" + model.ZDY18 + "',");
            }
            if (model.ZDY19 != null)
            {
                strSql1.Append("zdy19,");
                strSql2.Append("'" + model.ZDY19 + "',");
            }
            if (model.ZDY20 != null)
            {
                strSql1.Append("zdy20,");
                strSql2.Append("'" + model.ZDY20 + "',");
            }
            if (model.ReceiveDate != null)
            {
                strSql1.Append("ReceiveDate,");
                strSql2.Append("'" + model.ReceiveDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "',");
            }
            strSql.Append("insert into NRequestForm(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            Common.Log.Log.Info(strSql.ToString());
            Common.Log.Log.Debug("ZhiFang.DAL.MsSql.Weblis.NRequestForm.Add.NRequestFormNo：" + model.NRequestFormNo);
            int rows = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            return rows;
        }

        /// <summary>
        /// 海妇婴保健院(定制)在NRequestForm表里面新增字段:TestAim（诊断类型）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add_PKI(ZhiFang.Model.NRequestForm model)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into NRequestForm(");
                strSql.Append("ClientNo,TestTypeName,CName,DoctorName,CollecterName,SampleTypeName,SerialNo,ReceiveFlag,StatusNo,SampleTypeNo,PatNo,ClientName,GenderNo,Birthday,Age,AgeUnitNo,FolkNo,DistrictNo,WardNo,Bed,DeptNo,Doctor,AgeUnitName,DiagNo,Diag,ChargeNo,Charge,Chargeflag,CountNodesFormSource,IsCheckFee,Operator,OperDate,OperTime,GenderName,FormMemo,RequestSource,SickOrder,jztype,zdy1,zdy2,zdy3,zdy4,zdy5,FlagDateDelete,DeptName,NurseFlag,IsByHand,TestTypeNo,ExecDeptNo,CollectDate,CollectTime,Collecter,LABCENTER,NRequestFormNo,CheckNo,DistrictName,CheckName,WebLisSourceOrgID,WebLisSourceOrgName,WardName,FolkName,ClinicTypeName,PersonID,SampleType,TelNo,WebLisOrgID,WebLisOrgName,STATUSName,jztypeName,barcode,CombiItemName,printTimes,price,TestAim");
                strSql.Append(") values (");
                strSql.Append("@ClientNo,@TestTypeName,@CName,@DoctorName,@CollecterName,@SampleTypeName,@SerialNo,@ReceiveFlag,@StatusNo,@SampleTypeNo,@PatNo,@ClientName,@GenderNo,@Birthday,@Age,@AgeUnitNo,@FolkNo,@DistrictNo,@WardNo,@Bed,@DeptNo,@Doctor,@AgeUnitName,@DiagNo,@Diag,@ChargeNo,@Charge,@Chargeflag,@CountNodesFormSource,@IsCheckFee,@Operator,@OperDate,@OperTime,@GenderName,@FormMemo,@RequestSource,@SickOrder,@jztype,@zdy1,@zdy2,@zdy3,@zdy4,@zdy5,@FlagDateDelete,@DeptName,@NurseFlag,@IsByHand,@TestTypeNo,@ExecDeptNo,@CollectDate,@CollectTime,@Collecter,@LABCENTER,@NRequestFormNo,@CheckNo,@DistrictName,@CheckName,@WebLisSourceOrgID,@WebLisSourceOrgName,@WardName,@FolkName,@ClinicTypeName,@PersonID,@SampleType,@TelNo,@WebLisOrgID,@WebLisOrgName,@STATUSName,@jztypeName,@barcode,@CombiItemName,@printTimes,@price,@TestAim");
                strSql.Append(") ");

                SqlParameter[] parameters = {
                        new SqlParameter("@ClientNo", SqlDbType.VarChar,50) ,
                        new SqlParameter("@TestTypeName", SqlDbType.VarChar,10) ,
                        new SqlParameter("@CName", SqlDbType.VarChar,30) ,
                        new SqlParameter("@DoctorName", SqlDbType.VarChar,10) ,
                        new SqlParameter("@CollecterName", SqlDbType.VarChar,10) ,
                        new SqlParameter("@SampleTypeName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@SerialNo", SqlDbType.VarChar,30) ,
                        new SqlParameter("@ReceiveFlag", SqlDbType.Int,4) ,
                        new SqlParameter("@StatusNo", SqlDbType.Int,4) ,
                        new SqlParameter("@SampleTypeNo", SqlDbType.Int,4) ,
                        new SqlParameter("@PatNo", SqlDbType.VarChar,20) ,
                        new SqlParameter("@ClientName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@GenderNo", SqlDbType.Int,4) ,
                        new SqlParameter("@Birthday", SqlDbType.DateTime) ,
                        new SqlParameter("@Age", SqlDbType.Float,8) ,
                        new SqlParameter("@AgeUnitNo", SqlDbType.Int,4) ,
                        new SqlParameter("@FolkNo", SqlDbType.Int,4) ,
                        new SqlParameter("@DistrictNo", SqlDbType.Int,4) ,
                        new SqlParameter("@WardNo", SqlDbType.Int,4) ,
                        new SqlParameter("@Bed", SqlDbType.VarChar,10) ,
                        new SqlParameter("@DeptNo", SqlDbType.Int,4) ,
                        new SqlParameter("@Doctor", SqlDbType.Int,4) ,
                        new SqlParameter("@AgeUnitName", SqlDbType.VarChar,10) ,
                        new SqlParameter("@DiagNo", SqlDbType.Int,4) ,
                        new SqlParameter("@Diag", SqlDbType.VarChar,100) ,
                        new SqlParameter("@ChargeNo", SqlDbType.Int,4) ,
                        new SqlParameter("@Charge", SqlDbType.Money,8) ,
                        new SqlParameter("@Chargeflag", SqlDbType.VarChar,10) ,
                        new SqlParameter("@CountNodesFormSource", SqlDbType.Char,1) ,
                        new SqlParameter("@IsCheckFee", SqlDbType.Int,4) ,
                        new SqlParameter("@Operator", SqlDbType.VarChar,20) ,
                        new SqlParameter("@OperDate", SqlDbType.DateTime) ,
                        new SqlParameter("@OperTime", SqlDbType.DateTime) ,
                        new SqlParameter("@GenderName", SqlDbType.VarChar,10) ,
                        new SqlParameter("@FormMemo", SqlDbType.VarChar,40) ,
                        new SqlParameter("@RequestSource", SqlDbType.VarChar,20) ,
                        new SqlParameter("@SickOrder", SqlDbType.VarChar,20) ,
                        new SqlParameter("@jztype", SqlDbType.Int,4) ,
                        new SqlParameter("@zdy1", SqlDbType.VarChar,50) ,
                        new SqlParameter("@zdy2", SqlDbType.VarChar,50) ,
                        new SqlParameter("@zdy3", SqlDbType.VarChar,50) ,
                        new SqlParameter("@zdy4", SqlDbType.VarChar,50) ,
                        new SqlParameter("@zdy5", SqlDbType.VarChar,50) ,
                        new SqlParameter("@FlagDateDelete", SqlDbType.DateTime) ,
                        new SqlParameter("@DeptName", SqlDbType.VarChar,40) ,
                        new SqlParameter("@NurseFlag", SqlDbType.Int,4) ,
                        new SqlParameter("@IsByHand", SqlDbType.Bit,1) ,
                        new SqlParameter("@TestTypeNo", SqlDbType.Int,4) ,
                        new SqlParameter("@ExecDeptNo", SqlDbType.Int,4) ,
                        new SqlParameter("@CollectDate", SqlDbType.DateTime) ,
                        new SqlParameter("@CollectTime", SqlDbType.DateTime) ,
                        new SqlParameter("@Collecter", SqlDbType.VarChar,10) ,
                        new SqlParameter("@LABCENTER", SqlDbType.VarChar,50) ,
                        new SqlParameter("@NRequestFormNo", SqlDbType.BigInt,8) ,
                        new SqlParameter("@CheckNo", SqlDbType.VarChar,50) ,
                        new SqlParameter("@DistrictName", SqlDbType.VarChar,20) ,
                        new SqlParameter("@CheckName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@WebLisSourceOrgID", SqlDbType.VarChar,50) ,
                        new SqlParameter("@WebLisSourceOrgName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@WardName", SqlDbType.VarChar,40) ,
                        new SqlParameter("@FolkName", SqlDbType.VarChar,20) ,
                        new SqlParameter("@ClinicTypeName", SqlDbType.VarChar,20),
                        new SqlParameter("@PersonID", SqlDbType.VarChar,20),
                        new SqlParameter("@SampleType", SqlDbType.VarChar,10),
                        new SqlParameter("@TelNo", SqlDbType.VarChar,40),
                        new SqlParameter("@WebLisOrgID", SqlDbType.VarChar,50),
                        new SqlParameter("@WebLisOrgName", SqlDbType.VarChar,150),
                        new SqlParameter("@STATUSName", SqlDbType.VarChar,50),
                        new SqlParameter("@jztypeName", SqlDbType.VarChar,40),
                        new SqlParameter("@barcode", SqlDbType.VarChar,200),
                        new SqlParameter("@CombiItemName", SqlDbType.VarChar,200),
                        new SqlParameter("@printTimes", SqlDbType.Int,4),
                        new SqlParameter("@price", SqlDbType.Decimal),
                        new SqlParameter("@TestAim", SqlDbType.VarChar,50),

            };

                parameters[0].Value = model.ClientNo;
                parameters[1].Value = model.TestTypeName;
                parameters[2].Value = model.CName;
                parameters[3].Value = model.DoctorName;
                parameters[4].Value = model.CollecterName;
                parameters[5].Value = model.SampleTypeName;
                parameters[6].Value = model.SerialNo;
                parameters[7].Value = model.ReceiveFlag;
                parameters[8].Value = model.StatusNo;
                parameters[9].Value = model.SampleTypeNo;
                parameters[10].Value = model.PatNo;
                parameters[11].Value = model.ClientName;
                parameters[12].Value = model.GenderNo;
                parameters[13].Value = model.Birthday;
                parameters[14].Value = model.Age;
                parameters[15].Value = model.AgeUnitNo;
                parameters[16].Value = model.FolkNo;
                parameters[17].Value = model.DistrictNo;
                parameters[18].Value = model.WardNo;
                parameters[19].Value = model.Bed;
                parameters[20].Value = model.DeptNo;
                parameters[21].Value = model.Doctor;
                parameters[22].Value = model.AgeUnitName;
                parameters[23].Value = model.DiagNo;
                parameters[24].Value = model.Diag;
                parameters[25].Value = model.ChargeNo;
                parameters[26].Value = model.Charge;
                parameters[27].Value = model.Chargeflag;
                parameters[28].Value = model.CountNodesFormSource;
                parameters[29].Value = model.IsCheckFee;
                parameters[30].Value = model.Operator;
                parameters[31].Value = model.OperDate;
                parameters[32].Value = model.OperTime;
                parameters[33].Value = model.GenderName;
                parameters[34].Value = model.FormMemo;
                parameters[35].Value = model.RequestSource;
                parameters[36].Value = model.SickOrder;
                parameters[37].Value = model.jztype;
                parameters[38].Value = model.zdy1;
                parameters[39].Value = model.zdy2;
                parameters[40].Value = model.zdy3;
                parameters[41].Value = model.zdy4;
                parameters[42].Value = model.zdy5;
                parameters[43].Value = model.FlagDateDelete;
                parameters[44].Value = model.DeptName;
                parameters[45].Value = model.NurseFlag;
                parameters[46].Value = model.IsByHand;
                parameters[47].Value = model.TestTypeNo;
                parameters[48].Value = model.ExecDeptNo;
                parameters[49].Value = model.CollectDate;
                parameters[50].Value = model.CollectTime;
                parameters[51].Value = model.Collecter;
                parameters[52].Value = model.LABCENTER;
                parameters[53].Value = model.NRequestFormNo;
                parameters[54].Value = model.CheckNo;
                parameters[55].Value = model.DistrictName;
                parameters[56].Value = model.CheckName;
                parameters[57].Value = model.WebLisSourceOrgID;
                parameters[58].Value = model.WebLisSourceOrgName;
                parameters[59].Value = model.WardName;
                parameters[60].Value = model.FolkName;
                parameters[61].Value = model.ClinicTypeName;
                parameters[62].Value = model.PersonID;
                parameters[63].Value = model.SampleType;
                parameters[64].Value = model.TelNo;
                parameters[65].Value = model.WebLisOrgID;
                parameters[66].Value = model.WebLisOrgName;
                parameters[67].Value = model.STATUSName;
                parameters[68].Value = model.jztypeName;
                parameters[69].Value = model.BarCode;
                parameters[70].Value = model.CombiItemName;
                parameters[71].Value = model.PrintTimes;
                parameters[72].Value = model.Price;
                parameters[73].Value = model.TestAim;
                Common.Log.Log.Info(@"ZhiFang.DAL.MsSql\Weblis\NRequestForm.cs->Add_PKI:新增Sql语句:" + strSql.ToString());
                return idb.ExecuteNonQuery(strSql.ToString(), parameters);
            }
            catch (Exception e)
            {
                Common.Log.Log.Debug(@"ZhiFang.DAL.MsSql\Weblis\NRequestForm.cs->Add_PKI错误信息：" + e.ToString() + "字符串连接:" + idb.ConnectionString);
                return 0;
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.NRequestForm model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update NRequestForm set ");
            if (model.ClientNo != null)
            {
                strSql.Append("ClientNo='" + model.ClientNo + "',");
            }
            if (model.TestAim != null)
            {
                strSql.Append("TestAim='" + model.TestAim + "',");
            }
            if (model.ClientName != null)
            {
                strSql.Append("ClientName='" + model.ClientName + "',");
            }
            else
            {
                strSql.Append("ClientName= null ,");
            }
            if (model.AgeUnitName != null)
            {
                strSql.Append("AgeUnitName='" + model.AgeUnitName + "',");
            }
            else
            {
                strSql.Append("AgeUnitName= null ,");
            }
            if (model.GenderName != null)
            {
                strSql.Append("GenderName='" + model.GenderName + "',");
            }
            else
            {
                strSql.Append("GenderName= null ,");
            }
            if (model.DeptName != null)
            {
                strSql.Append("DeptName='" + model.DeptName + "',");
            }
            else
            {
                strSql.Append("DeptName= null ,");
            }
            if (model.DistrictName != null)
            {
                strSql.Append("DistrictName='" + model.DistrictName + "',");
            }
            else
            {
                strSql.Append("DistrictName= null ,");
            }
            if (model.WardName != null)
            {
                strSql.Append("WardName='" + model.WardName + "',");
            }
            else
            {
                strSql.Append("WardName= null ,");
            }
            if (model.FolkName != null)
            {
                strSql.Append("FolkName='" + model.FolkName + "',");
            }
            else
            {
                strSql.Append("FolkName= null ,");
            }
            if (model.ClinicTypeName != null)
            {
                strSql.Append("ClinicTypeName='" + model.ClinicTypeName + "',");
            }
            else
            {
                strSql.Append("ClinicTypeName= null ,");
            }
            if (model.TestTypeName != null)
            {
                strSql.Append("TestTypeName='" + model.TestTypeName + "',");
            }
            else
            {
                strSql.Append("TestTypeName= null ,");
            }
            if (model.CName != null)
            {
                strSql.Append("CName='" + model.CName + "',");
            }
            else
            {
                strSql.Append("CName= null ,");
            }
            if (model.DoctorName != null)
            {
                strSql.Append("DoctorName='" + model.DoctorName + "',");
            }
            else
            {
                strSql.Append("DoctorName= null ,");
            }
            if (model.CollecterName != null)
            {
                strSql.Append("CollecterName='" + model.CollecterName + "',");
            }
            else
            {
                strSql.Append("CollecterName= null ,");
            }
            if (model.SampleTypeName != null)
            {
                strSql.Append("SampleTypeName='" + model.SampleTypeName + "',");
            }
            else
            {
                strSql.Append("SampleTypeName= null ,");
            }
            if (model.SerialNo != null)
            {
                strSql.Append("SerialNo='" + model.SerialNo + "',");
            }
            if (model.ReceiveFlag != null)
            {
                strSql.Append("ReceiveFlag=" + model.ReceiveFlag + ",");
            }
            else
            {
                strSql.Append("ReceiveFlag= null ,");
            }
            if (model.StatusNo != null)
            {
                strSql.Append("StatusNo=" + model.StatusNo + ",");
            }
            else
            {
                strSql.Append("StatusNo= null ,");
            }
            if (model.SampleTypeNo != null)
            {
                strSql.Append("SampleTypeNo=" + model.SampleTypeNo + ",");
            }
            else
            {
                strSql.Append("SampleTypeNo= null ,");
            }
            if (model.PatNo != null)
            {
                strSql.Append("PatNo='" + model.PatNo + "',");
            }
            else
            {
                strSql.Append("PatNo= null ,");
            }
            if (model.GenderNo != null)
            {
                strSql.Append("GenderNo=" + model.GenderNo + ",");
            }
            else
            {
                strSql.Append("GenderNo= null ,");
            }
            if (model.Birthday != null)
            {
                strSql.Append("Birthday='" + model.Birthday + "',");
            }
            else
            {
                strSql.Append("Birthday= null ,");
            }
            if (model.Age != null)
            {
                strSql.Append("Age=" + model.Age + ",");
            }
            else
            {
                strSql.Append("Age= null ,");
            }
            if (model.AgeUnitNo != null)
            {
                strSql.Append("AgeUnitNo=" + model.AgeUnitNo + ",");
            }
            else
            {
                strSql.Append("AgeUnitNo= null ,");
            }
            if (model.FolkNo != null)
            {
                strSql.Append("FolkNo=" + model.FolkNo + ",");
            }
            else
            {
                strSql.Append("FolkNo= null ,");
            }
            if (model.DistrictNo != null)
            {
                strSql.Append("DistrictNo=" + model.DistrictNo + ",");
            }
            else
            {
                strSql.Append("DistrictNo= null ,");
            }
            if (model.WardNo != null)
            {
                strSql.Append("WardNo=" + model.WardNo + ",");
            }
            else
            {
                strSql.Append("WardNo= null ,");
            }
            if (model.Bed != null)
            {
                strSql.Append("Bed='" + model.Bed + "',");
            }
            else
            {
                strSql.Append("Bed= null ,");
            }
            if (model.DeptNo != null)
            {
                strSql.Append("DeptNo=" + model.DeptNo + ",");
            }
            else
            {
                strSql.Append("DeptNo= null ,");
            }
            if (model.Doctor != null)
            {
                strSql.Append("Doctor=" + model.Doctor + ",");
            }
            else
            {
                strSql.Append("Doctor= null ,");
            }
            if (model.DiagNo != null)
            {
                strSql.Append("DiagNo=" + model.DiagNo + ",");
            }
            else
            {
                strSql.Append("DiagNo= null ,");
            }
            if (model.Diag != null)
            {
                strSql.Append("Diag='" + model.Diag + "',");
            }
            else
            {
                strSql.Append("Diag= null ,");
            }
            if (model.ChargeNo != null)
            {
                strSql.Append("ChargeNo=" + model.ChargeNo + ",");
            }
            else
            {
                strSql.Append("ChargeNo= null ,");
            }
            if (model.Charge != null)
            {
                strSql.Append("Charge=" + model.Charge + ",");
            }
            else
            {
                strSql.Append("Charge= null ,");
            }
            if (model.Chargeflag != null)
            {
                strSql.Append("Chargeflag='" + model.Chargeflag + "',");
            }
            else
            {
                strSql.Append("Chargeflag= null ,");
            }
            if (model.CountNodesFormSource != null)
            {
                strSql.Append("CountNodesFormSource='" + model.CountNodesFormSource + "',");
            }
            else
            {
                strSql.Append("CountNodesFormSource= null ,");
            }
            if (model.IsCheckFee != null)
            {
                strSql.Append("IsCheckFee=" + model.IsCheckFee + ",");
            }
            else
            {
                strSql.Append("IsCheckFee= null ,");
            }
            if (model.Operator != null)
            {
                strSql.Append("Operator='" + model.Operator + "',");
            }
            else
            {
                strSql.Append("Operator= null ,");
            }
            if (model.OperDate != null)
            {
                strSql.Append("OperDate='" + model.OperDate + "',");
            }
            else
            {
                strSql.Append("OperDate= null ,");
            }
            if (model.OperTime != null)
            {
                strSql.Append("OperTime='" + model.OperTime + "',");
            }
            else
            {
                strSql.Append("OperTime= null ,");
            }
            if (model.FormMemo != null)
            {
                strSql.Append("FormMemo='" + model.FormMemo + "',");
            }
            else
            {
                strSql.Append("FormMemo= null ,");
            }
            if (model.RequestSource != null)
            {
                strSql.Append("RequestSource='" + model.RequestSource + "',");
            }
            else
            {
                strSql.Append("RequestSource= null ,");
            }
            if (model.SickOrder != null)
            {
                strSql.Append("SickOrder='" + model.SickOrder + "',");
            }
            else
            {
                strSql.Append("SickOrder= null ,");
            }
            if (model.jztype != null)
            {
                strSql.Append("jztype=" + model.jztype + ",");
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
            if (model.FlagDateDelete != null)
            {
                strSql.Append("FlagDateDelete='" + model.FlagDateDelete + "',");
            }
            else
            {
                strSql.Append("FlagDateDelete= null ,");
            }
            if (model.NurseFlag != null)
            {
                strSql.Append("NurseFlag=" + model.NurseFlag + ",");
            }
            else
            {
                strSql.Append("NurseFlag= null ,");
            }
            if (model.TestTypeNo != null)
            {
                strSql.Append("TestTypeNo=" + model.TestTypeNo + ",");
            }
            else
            {
                strSql.Append("TestTypeNo= null ,");
            }
            if (model.ExecDeptNo != null)
            {
                strSql.Append("ExecDeptNo=" + model.ExecDeptNo + ",");
            }
            else
            {
                strSql.Append("ExecDeptNo= null ,");
            }
            if (model.CollectDate != null)
            {
                strSql.Append("CollectDate='" + model.CollectDate + "',");
            }
            else
            {
                strSql.Append("CollectDate= null ,");
            }
            if (model.CollectTime != null)
            {
                strSql.Append("CollectTime='" + model.CollectTime + "',");
            }
            else
            {
                strSql.Append("CollectTime= null ,");
            }
            if (model.Collecter != null)
            {
                strSql.Append("Collecter='" + model.Collecter + "',");
            }
            else
            {
                strSql.Append("Collecter= null ,");
            }
            if (model.LABCENTER != null)
            {
                strSql.Append("LABCENTER='" + model.LABCENTER + "',");
            }
            else
            {
                strSql.Append("LABCENTER= null ,");
            }
            if (model.CheckNo != null)
            {
                strSql.Append("CheckNo='" + model.CheckNo + "',");
            }
            else
            {
                strSql.Append("CheckNo= null ,");
            }
            if (model.CheckName != null)
            {
                strSql.Append("CheckName='" + model.CheckName + "',");
            }
            else
            {
                strSql.Append("CheckName= null ,");
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
            if (model.OldSerialNo != null)
            {
                strSql.Append("OldSerialNo='" + model.OldSerialNo + "',");
            }
            else
            {
                strSql.Append("OldSerialNo= null ,");
            }

            if (model.PersonID != null)
            {
                strSql.Append("PersonID='" + model.PersonID + "',");
            }
            else
            {
                strSql.Append("PersonID= null ,");
            }
            if (model.SampleType != null)
            {
                strSql.Append("SampleType='" + model.SampleType + "',");
            }
            else
            {
                strSql.Append("SampleType= null ,");
            }
            if (model.TelNo != null)
            {
                strSql.Append("TelNo='" + model.TelNo + "',");
            }
            else
            {
                strSql.Append("TelNo= null ,");
            }
            if (model.WebLisOrgID != null)
            {
                strSql.Append("WebLisOrgID='" + model.WebLisOrgID + "',");
            }
            else
            {
                strSql.Append("WebLisOrgID= null ,");
            }
            if (model.WebLisOrgName != null)
            {
                strSql.Append("WebLisOrgName='" + model.WebLisOrgName + "',");
            }
            else
            {
                strSql.Append("WebLisOrgName= null ,");
            }
            if (model.STATUSName != null)
            {
                strSql.Append("STATUSName='" + model.STATUSName + "',");
            }
            else
            {
                strSql.Append("STATUSName= null ,");
            }
            if (model.jztypeName != null)
            {
                strSql.Append("jztypeName='" + model.jztypeName + "',");
            }
            else
            {
                strSql.Append("jztypeName= null ,");
            }
            //BarCode
            if (model.BarCode != null)
            {
                strSql.Append("barcode='" + model.BarCode + "',");
            }
            else
            {
                strSql.Append("barcode= null ,");
            }

            if (!string.IsNullOrEmpty(model.Nationality))
            {
                strSql.Append("Nationality='" + model.Nationality + "',");
            }

            if (!string.IsNullOrEmpty(model.PassportNo))
            {
                strSql.Append("PassportNo='" + model.PassportNo + "',");
            }

            if (model.SMITypeId.HasValue)
            {
                strSql.Append("SMITypeId=" + model.SMITypeId + ",");
            }

            if (!string.IsNullOrEmpty(model.SMITypeName))
            {
                strSql.Append("SMITypeName='" + model.SMITypeName + "',");
            }

            if (!string.IsNullOrEmpty(model.NCPTestTypeNo))
            {
                strSql.Append("NCPTestTypeNo='" + model.NCPTestTypeNo + "',");
            }

             if (!string.IsNullOrEmpty(model.IDCardAddress))
            {
                strSql.Append("IDCardAddress='" + model.IDCardAddress + "',");
            }
            if (model.CollectTypeId.HasValue)
            {
                strSql.Append("CollectTypeId=" + model.CollectTypeId + ",");
            }
            if (!string.IsNullOrEmpty(model.CollectTypeName))
            {
                strSql.Append("CollectTypeName='" + model.CollectTypeName + "',");
            }

            //CombiItemName
            if (model.CombiItemName != null)
            {
                strSql.Append("CombiItemName='" + model.CombiItemName + "',");
            }
            else
            {
                strSql.Append("CombiItemName= null ,");
            }
            if (model.PrintTimes != null)
                strSql.Append("printTimes='" + model.PrintTimes + "',");
            else
                strSql.Append("printTimes= null ,");

            if (model.Price != null)
            {
                strSql.Append("price='" + model.Price + "',");
            }
            else
                strSql.Append("price= null ,");

            if (model.ZDY6 != null)
            {
                strSql.Append("zdy6='" + model.ZDY6 + "',");
            }
            else
            {
                strSql.Append("zdy6= null ,");
            }
            if (model.ZDY7 != null)
            {
                strSql.Append("zdy7='" + model.ZDY7 + "',");
            }
            else
            {
                strSql.Append("zdy7= null ,");
            }
            if (model.ZDY8 != null)
            {
                strSql.Append("zdy8='" + model.ZDY8 + "',");
            }
            else
            {
                strSql.Append("zdy8= null ,");
            }

            if (model.ZDY9 != null)
            {
                strSql.Append("Zdy9='" + model.ZDY9 + "',");
            }
            else
            {
                strSql.Append("Zdy9= null ,");
            }
            if (model.ZDY10 != null)
            {
                strSql.Append("zdy10='" + model.ZDY10 + "',");
            }
            else
            {
                strSql.Append("zdy10= null ,");
            }
            if (model.ZDY11 != null)
            {
                strSql.Append("zdy11='" + model.ZDY11 + "',");
            }
            if (model.ZDY12 != null)
            {
                strSql.Append("zdy12='" + model.ZDY12 + "',");
            }
            if (model.ZDY13 != null)
            {
                strSql.Append("zdy13='" + model.ZDY13 + "',");
            }
            if (model.ZDY14 != null)
            {
                strSql.Append("zdy14='" + model.ZDY14 + "',");
            }
            if (model.ZDY15 != null)
            {
                strSql.Append("zdy15='" + model.ZDY15 + "',");
            }
            if (model.ZDY16 != null)
            {
                strSql.Append("zdy16='" + model.ZDY16 + "',");
            }
            if (model.ZDY17 != null)
            {
                strSql.Append("zdy17='" + model.ZDY17 + "',");
            }
            if (model.ZDY18 != null)
            {
                strSql.Append("zdy18='" + model.ZDY18 + "',");
            }
            if (model.ZDY19 != null)
            {
                strSql.Append("zdy19='" + model.ZDY19 + "',");
            }
            if (model.ZDY20 != null)
            {
                strSql.Append("zdy20='" + model.ZDY20 + "',");
            }
            if (model.ReceiveDate != null)
            {
                strSql.Append("ReceiveDate='" + model.ReceiveDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "',");
            }

            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where NRequestFormNo= '" + model.NRequestFormNo + "'");
            ZhiFang.Common.Log.Log.Info("更新NRequestFormNo:" + strSql);
            int rowsAffected = DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            return rowsAffected;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(long NRequestFormNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from NRequestForm ");
            strSql.Append(" where NRequestFormNo=@NRequestFormNo ");
            SqlParameter[] parameters = {
                    new SqlParameter("@NRequestFormNo", SqlDbType.BigInt,8)         };
            parameters[0].Value = NRequestFormNo;


            return idb.ExecuteNonQuery(strSql.ToString(), parameters);
        }

        public Model.NRequestForm GetModelBySerialNo(string SerialNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select PersonID,SampleType, TelNo,WebLisOrgID,WebLisOrgName,STATUSName, weblisflag,ClientNo, TestTypeName, CName, DoctorName, CollecterName, SampleTypeName, SerialNo, ReceiveFlag, StatusNo, SampleTypeNo, PatNo, ClientName, GenderNo, Birthday, Age, AgeUnitNo, FolkNo, DistrictNo, WardNo, Bed, DeptNo, Doctor, AgeUnitName, DiagNo, Diag, ChargeNo, Charge, Chargeflag, CountNodesFormSource, IsCheckFee, Operator, OperDate, OperTime, GenderName, FormMemo, RequestSource, SickOrder, jztype, zdy1, zdy2, zdy3, zdy4, zdy5, FlagDateDelete, DeptName, NurseFlag, IsByHand, TestTypeNo, ExecDeptNo, CollectDate, CollectTime, Collecter, LABCENTER, NRequestFormNo, CheckNo, DistrictName, CheckName, WebLisSourceOrgID, WebLisSourceOrgName, WardName, FolkName, ClinicTypeName  ");
            strSql.Append("  from NRequestForm ");
            strSql.Append(" where SerialNo=@SerialNo ");
            SqlParameter[] parameters = {
                    new SqlParameter("@SerialNo", SqlDbType.VarChar)            };
            parameters[0].Value = SerialNo;


            ZhiFang.Model.NRequestForm model = new ZhiFang.Model.NRequestForm();
            DataSet ds = idb.ExecuteDataSet(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.Weblisflag = ds.Tables[0].Rows[0]["weblisflag"].ToString();
                model.ClientNo = ds.Tables[0].Rows[0]["ClientNo"].ToString();
                model.TestTypeName = ds.Tables[0].Rows[0]["TestTypeName"].ToString();
                model.CName = ds.Tables[0].Rows[0]["CName"].ToString();
                model.DoctorName = ds.Tables[0].Rows[0]["DoctorName"].ToString();
                model.CollecterName = ds.Tables[0].Rows[0]["CollecterName"].ToString();
                model.SampleTypeName = ds.Tables[0].Rows[0]["SampleTypeName"].ToString();
                model.SerialNo = ds.Tables[0].Rows[0]["SerialNo"].ToString();
                if (ds.Tables[0].Rows[0]["ReceiveFlag"].ToString() != "")
                {
                    model.ReceiveFlag = int.Parse(ds.Tables[0].Rows[0]["ReceiveFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["StatusNo"].ToString() != "")
                {
                    model.StatusNo = int.Parse(ds.Tables[0].Rows[0]["StatusNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SampleTypeNo"].ToString() != "")
                {
                    model.SampleTypeNo = int.Parse(ds.Tables[0].Rows[0]["SampleTypeNo"].ToString());
                }
                model.PatNo = ds.Tables[0].Rows[0]["PatNo"].ToString();
                model.ClientName = ds.Tables[0].Rows[0]["ClientName"].ToString();
                if (ds.Tables[0].Rows[0]["GenderNo"].ToString() != "")
                {
                    model.GenderNo = int.Parse(ds.Tables[0].Rows[0]["GenderNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Birthday"].ToString() != "")
                {
                    model.Birthday = DateTime.Parse(ds.Tables[0].Rows[0]["Birthday"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Age"].ToString() != "")
                {
                    model.Age = decimal.Parse(ds.Tables[0].Rows[0]["Age"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AgeUnitNo"].ToString() != "")
                {
                    model.AgeUnitNo = int.Parse(ds.Tables[0].Rows[0]["AgeUnitNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FolkNo"].ToString() != "")
                {
                    model.FolkNo = int.Parse(ds.Tables[0].Rows[0]["FolkNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DistrictNo"].ToString() != "")
                {
                    model.DistrictNo = int.Parse(ds.Tables[0].Rows[0]["DistrictNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WardNo"].ToString() != "")
                {
                    model.WardNo = int.Parse(ds.Tables[0].Rows[0]["WardNo"].ToString());
                }
                model.Bed = ds.Tables[0].Rows[0]["Bed"].ToString();
                if (ds.Tables[0].Rows[0]["DeptNo"].ToString() != "")
                {
                    model.DeptNo = int.Parse(ds.Tables[0].Rows[0]["DeptNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Doctor"].ToString() != "")
                {
                    model.Doctor = int.Parse(ds.Tables[0].Rows[0]["Doctor"].ToString());
                }
                model.AgeUnitName = ds.Tables[0].Rows[0]["AgeUnitName"].ToString();
                if (ds.Tables[0].Rows[0]["DiagNo"].ToString() != "")
                {
                    model.DiagNo = int.Parse(ds.Tables[0].Rows[0]["DiagNo"].ToString());
                }
                model.Diag = ds.Tables[0].Rows[0]["Diag"].ToString();
                if (ds.Tables[0].Rows[0]["ChargeNo"].ToString() != "")
                {
                    model.ChargeNo = int.Parse(ds.Tables[0].Rows[0]["ChargeNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Charge"].ToString() != "")
                {
                    model.Charge = decimal.Parse(ds.Tables[0].Rows[0]["Charge"].ToString());
                }
                model.Chargeflag = ds.Tables[0].Rows[0]["Chargeflag"].ToString();
                model.CountNodesFormSource = ds.Tables[0].Rows[0]["CountNodesFormSource"].ToString();
                if (ds.Tables[0].Rows[0]["IsCheckFee"].ToString() != "")
                {
                    model.IsCheckFee = int.Parse(ds.Tables[0].Rows[0]["IsCheckFee"].ToString());
                }
                model.Operator = ds.Tables[0].Rows[0]["Operator"].ToString();
                if (ds.Tables[0].Rows[0]["OperDate"].ToString() != "")
                {
                    model.OperDate = DateTime.Parse(ds.Tables[0].Rows[0]["OperDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OperTime"].ToString() != "")
                {
                    model.OperTime = DateTime.Parse(ds.Tables[0].Rows[0]["OperTime"].ToString());
                }
                model.GenderName = ds.Tables[0].Rows[0]["GenderName"].ToString();
                model.FormMemo = ds.Tables[0].Rows[0]["FormMemo"].ToString();
                model.RequestSource = ds.Tables[0].Rows[0]["RequestSource"].ToString();
                model.SickOrder = ds.Tables[0].Rows[0]["SickOrder"].ToString();
                if (ds.Tables[0].Rows[0]["jztype"].ToString() != "")
                {
                    model.jztype = int.Parse(ds.Tables[0].Rows[0]["jztype"].ToString());
                }
                model.zdy1 = ds.Tables[0].Rows[0]["zdy1"].ToString();
                model.zdy2 = ds.Tables[0].Rows[0]["zdy2"].ToString();
                model.zdy3 = ds.Tables[0].Rows[0]["zdy3"].ToString();
                model.zdy4 = ds.Tables[0].Rows[0]["zdy4"].ToString();
                model.zdy5 = ds.Tables[0].Rows[0]["zdy5"].ToString();
                if (ds.Tables[0].Rows[0]["FlagDateDelete"].ToString() != "")
                {
                    model.FlagDateDelete = DateTime.Parse(ds.Tables[0].Rows[0]["FlagDateDelete"].ToString());
                }
                model.DeptName = ds.Tables[0].Rows[0]["DeptName"].ToString();
                if (ds.Tables[0].Rows[0]["NurseFlag"].ToString() != "")
                {
                    model.NurseFlag = int.Parse(ds.Tables[0].Rows[0]["NurseFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsByHand"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsByHand"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsByHand"].ToString().ToLower() == "true"))
                    {
                        model.IsByHand = true;
                    }
                    else
                    {
                        model.IsByHand = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["TestTypeNo"].ToString() != "")
                {
                    model.TestTypeNo = int.Parse(ds.Tables[0].Rows[0]["TestTypeNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ExecDeptNo"].ToString() != "")
                {
                    model.ExecDeptNo = int.Parse(ds.Tables[0].Rows[0]["ExecDeptNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CollectDate"].ToString() != "")
                {
                    model.CollectDate = DateTime.Parse(ds.Tables[0].Rows[0]["CollectDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CollectTime"].ToString() != "")
                {
                    model.CollectTime = DateTime.Parse(ds.Tables[0].Rows[0]["CollectTime"].ToString());
                }
                model.Collecter = ds.Tables[0].Rows[0]["Collecter"].ToString();
                model.LABCENTER = ds.Tables[0].Rows[0]["LABCENTER"].ToString();
                if (ds.Tables[0].Rows[0]["NRequestFormNo"].ToString() != "")
                {
                    model.NRequestFormNo = long.Parse(ds.Tables[0].Rows[0]["NRequestFormNo"].ToString());
                }
                model.CheckNo = ds.Tables[0].Rows[0]["CheckNo"].ToString();
                model.DistrictName = ds.Tables[0].Rows[0]["DistrictName"].ToString();
                model.CheckName = ds.Tables[0].Rows[0]["CheckName"].ToString();
                model.WebLisSourceOrgID = ds.Tables[0].Rows[0]["WebLisSourceOrgID"].ToString();
                model.WebLisSourceOrgName = ds.Tables[0].Rows[0]["WebLisSourceOrgName"].ToString();
                model.WardName = ds.Tables[0].Rows[0]["WardName"].ToString();
                model.FolkName = ds.Tables[0].Rows[0]["FolkName"].ToString();
                model.ClinicTypeName = ds.Tables[0].Rows[0]["ClinicTypeName"].ToString();
                model.PersonID = ds.Tables[0].Rows[0]["PersonID"].ToString();
                model.SampleType = ds.Tables[0].Rows[0]["SampleType"].ToString();
                model.TelNo = ds.Tables[0].Rows[0]["TelNo"].ToString();
                model.WebLisOrgID = ds.Tables[0].Rows[0]["WebLisOrgID"].ToString();
                model.WebLisOrgName = ds.Tables[0].Rows[0]["WebLisOrgName"].ToString();
                model.STATUSName = ds.Tables[0].Rows[0]["STATUSName"].ToString();
                model.jztypeName = ds.Tables[0].Rows[0]["jztypeName"].ToString();

                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.NRequestForm GetModel(long NRequestFormNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append("  from NRequestForm ");
            strSql.Append(" where NRequestFormNo=@NRequestFormNo ");
            SqlParameter[] parameters = {
                    new SqlParameter("@NRequestFormNo", SqlDbType.BigInt,8)         };
            parameters[0].Value = NRequestFormNo;


            ZhiFang.Model.NRequestForm model = new ZhiFang.Model.NRequestForm();
            DataSet ds = idb.ExecuteDataSet(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.ClientNo = ds.Tables[0].Rows[0]["ClientNo"].ToString();
                model.TestTypeName = ds.Tables[0].Rows[0]["TestTypeName"].ToString();
                model.CName = ds.Tables[0].Rows[0]["CName"].ToString();
                model.DoctorName = ds.Tables[0].Rows[0]["DoctorName"].ToString();
                model.CollecterName = ds.Tables[0].Rows[0]["CollecterName"].ToString();
                model.SampleTypeName = ds.Tables[0].Rows[0]["SampleTypeName"].ToString();
                model.SerialNo = ds.Tables[0].Rows[0]["SerialNo"].ToString();
                if (ds.Tables[0].Rows[0]["ReceiveFlag"].ToString() != "")
                {
                    model.ReceiveFlag = int.Parse(ds.Tables[0].Rows[0]["ReceiveFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["StatusNo"].ToString() != "")
                {
                    model.StatusNo = int.Parse(ds.Tables[0].Rows[0]["StatusNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SampleTypeNo"].ToString() != "")
                {
                    model.SampleTypeNo = int.Parse(ds.Tables[0].Rows[0]["SampleTypeNo"].ToString());
                }
                model.PatNo = ds.Tables[0].Rows[0]["PatNo"].ToString();
                model.ClientName = ds.Tables[0].Rows[0]["ClientName"].ToString();
                if (ds.Tables[0].Rows[0]["GenderNo"].ToString() != "")
                {
                    model.GenderNo = int.Parse(ds.Tables[0].Rows[0]["GenderNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Birthday"].ToString() != "")
                {
                    model.Birthday = DateTime.Parse(ds.Tables[0].Rows[0]["Birthday"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Age"].ToString() != "")
                {
                    model.Age = decimal.Parse(ds.Tables[0].Rows[0]["Age"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AgeUnitNo"].ToString() != "")
                {
                    model.AgeUnitNo = int.Parse(ds.Tables[0].Rows[0]["AgeUnitNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FolkNo"].ToString() != "")
                {
                    model.FolkNo = int.Parse(ds.Tables[0].Rows[0]["FolkNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DistrictNo"].ToString() != "")
                {
                    model.DistrictNo = int.Parse(ds.Tables[0].Rows[0]["DistrictNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WardNo"].ToString() != "")
                {
                    model.WardNo = int.Parse(ds.Tables[0].Rows[0]["WardNo"].ToString());
                }
                model.Bed = ds.Tables[0].Rows[0]["Bed"].ToString();
                if (ds.Tables[0].Rows[0]["DeptNo"].ToString() != "")
                {
                    model.DeptNo = int.Parse(ds.Tables[0].Rows[0]["DeptNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Doctor"].ToString() != "")
                {
                    model.Doctor = int.Parse(ds.Tables[0].Rows[0]["Doctor"].ToString());
                }
                model.AgeUnitName = ds.Tables[0].Rows[0]["AgeUnitName"].ToString();
                if (ds.Tables[0].Rows[0]["DiagNo"].ToString() != "")
                {
                    model.DiagNo = int.Parse(ds.Tables[0].Rows[0]["DiagNo"].ToString());
                }
                model.Diag = ds.Tables[0].Rows[0]["Diag"].ToString();
                if (ds.Tables[0].Rows[0]["ChargeNo"].ToString() != "")
                {
                    model.ChargeNo = int.Parse(ds.Tables[0].Rows[0]["ChargeNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Charge"].ToString() != "")
                {
                    model.Charge = decimal.Parse(ds.Tables[0].Rows[0]["Charge"].ToString());
                }
                model.Chargeflag = ds.Tables[0].Rows[0]["Chargeflag"].ToString();
                model.CountNodesFormSource = ds.Tables[0].Rows[0]["CountNodesFormSource"].ToString();
                if (ds.Tables[0].Rows[0]["IsCheckFee"].ToString() != "")
                {
                    model.IsCheckFee = int.Parse(ds.Tables[0].Rows[0]["IsCheckFee"].ToString());
                }
                model.Operator = ds.Tables[0].Rows[0]["Operator"].ToString();
                if (ds.Tables[0].Rows[0]["OperDate"].ToString() != "")
                {
                    model.OperDate = DateTime.Parse(ds.Tables[0].Rows[0]["OperDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OperTime"].ToString() != "")
                {
                    model.OperTime = DateTime.Parse(ds.Tables[0].Rows[0]["OperTime"].ToString());
                }
                model.GenderName = ds.Tables[0].Rows[0]["GenderName"].ToString();
                model.FormMemo = ds.Tables[0].Rows[0]["FormMemo"].ToString();
                model.RequestSource = ds.Tables[0].Rows[0]["RequestSource"].ToString();
                model.SickOrder = ds.Tables[0].Rows[0]["SickOrder"].ToString();
                if (ds.Tables[0].Rows[0]["jztype"].ToString() != "")
                {
                    model.jztype = int.Parse(ds.Tables[0].Rows[0]["jztype"].ToString());
                }
                model.zdy1 = ds.Tables[0].Rows[0]["zdy1"].ToString();
                model.zdy2 = ds.Tables[0].Rows[0]["zdy2"].ToString();
                model.zdy3 = ds.Tables[0].Rows[0]["zdy3"].ToString();
                model.zdy4 = ds.Tables[0].Rows[0]["zdy4"].ToString();
                model.zdy5 = ds.Tables[0].Rows[0]["zdy5"].ToString();
                if (ds.Tables[0].Columns.Contains("zdy6") && ds.Tables[0].Rows[0]["zdy6"] != null && ds.Tables[0].Rows[0]["zdy6"].ToString() != "")
                {
                    model.ZDY6 = ds.Tables[0].Rows[0]["zdy6"].ToString();
                }
                if (ds.Tables[0].Columns.Contains("zdy7") && ds.Tables[0].Rows[0]["zdy7"] != null && ds.Tables[0].Rows[0]["zdy7"].ToString() != "")
                {
                    model.ZDY7 = ds.Tables[0].Rows[0]["zdy7"].ToString();
                }
                if (ds.Tables[0].Columns.Contains("zdy8") && ds.Tables[0].Rows[0]["zdy8"] != null && ds.Tables[0].Rows[0]["zdy8"].ToString() != "")
                {
                    model.ZDY8 = ds.Tables[0].Rows[0]["zdy8"].ToString();
                }
                if (ds.Tables[0].Columns.Contains("zdy9") && ds.Tables[0].Rows[0]["zdy9"] != null && ds.Tables[0].Rows[0]["zdy9"].ToString() != "")
                {
                    model.ZDY9 = ds.Tables[0].Rows[0]["zdy9"].ToString();
                }
                if (ds.Tables[0].Columns.Contains("zdy10") && ds.Tables[0].Rows[0]["zdy10"] != null && ds.Tables[0].Rows[0]["zdy10"].ToString() != "")
                {
                    model.ZDY10 = ds.Tables[0].Rows[0]["zdy10"].ToString();
                }





                if (ds.Tables[0].Columns.Contains("Nationality") && ds.Tables[0].Rows[0]["Nationality"] != null && ds.Tables[0].Rows[0]["Nationality"].ToString() != "")
                {
                    model.Nationality = ds.Tables[0].Rows[0]["Nationality"].ToString();
                }

                if (ds.Tables[0].Columns.Contains("PassportNo") && ds.Tables[0].Rows[0]["PassportNo"] != null && ds.Tables[0].Rows[0]["PassportNo"].ToString() != "")
                {
                    model.PassportNo = ds.Tables[0].Rows[0]["PassportNo"].ToString();
                }

                if (ds.Tables[0].Columns.Contains("SMITypeId") && ds.Tables[0].Rows[0]["SMITypeId"] != null && ds.Tables[0].Rows[0]["SMITypeId"].ToString() != "")
                {
                    model.SMITypeId =long.Parse(ds.Tables[0].Rows[0]["SMITypeId"].ToString());
                }

                if (ds.Tables[0].Columns.Contains("SMITypeName") && ds.Tables[0].Rows[0]["SMITypeName"] != null && ds.Tables[0].Rows[0]["SMITypeName"].ToString() != "")
                {
                    model.SMITypeName = ds.Tables[0].Rows[0]["SMITypeName"].ToString();
                }

                if (ds.Tables[0].Columns.Contains("NCPTestTypeNo") && ds.Tables[0].Rows[0]["NCPTestTypeNo"] != null && ds.Tables[0].Rows[0]["NCPTestTypeNo"].ToString() != "")
                {
                    model.NCPTestTypeNo = ds.Tables[0].Rows[0]["NCPTestTypeNo"].ToString();
                }
                if (ds.Tables[0].Columns.Contains("IDCardAddress") && ds.Tables[0].Rows[0]["IDCardAddress"] != null && ds.Tables[0].Rows[0]["IDCardAddress"].ToString() != "")
                {
                    model.IDCardAddress = ds.Tables[0].Rows[0]["IDCardAddress"].ToString();
                }

                if (ds.Tables[0].Columns.Contains("CollectTypeId") && ds.Tables[0].Rows[0]["CollectTypeId"] != null && ds.Tables[0].Rows[0]["CollectTypeId"].ToString() != "")
                {
                    model.CollectTypeId = long.Parse(ds.Tables[0].Rows[0]["CollectTypeId"].ToString());
                }

                if (ds.Tables[0].Columns.Contains("CollectTypeName") && ds.Tables[0].Rows[0]["CollectTypeName"] != null && ds.Tables[0].Rows[0]["CollectTypeName"].ToString() != "")
                {
                    model.CollectTypeName = ds.Tables[0].Rows[0]["CollectTypeName"].ToString();
                }



                if (ds.Tables[0].Rows[0]["PersonID"] != null && ds.Tables[0].Rows[0]["PersonID"].ToString() != "")
                {
                    model.PersonID = ds.Tables[0].Rows[0]["PersonID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FlagDateDelete"].ToString() != "")
                {
                    model.FlagDateDelete = DateTime.Parse(ds.Tables[0].Rows[0]["FlagDateDelete"].ToString());
                }
                model.DeptName = ds.Tables[0].Rows[0]["DeptName"].ToString();
                if (ds.Tables[0].Rows[0]["NurseFlag"].ToString() != "")
                {
                    model.NurseFlag = int.Parse(ds.Tables[0].Rows[0]["NurseFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsByHand"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsByHand"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsByHand"].ToString().ToLower() == "true"))
                    {
                        model.IsByHand = true;
                    }
                    else
                    {
                        model.IsByHand = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["TestTypeNo"].ToString() != "")
                {
                    model.TestTypeNo = int.Parse(ds.Tables[0].Rows[0]["TestTypeNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ExecDeptNo"].ToString() != "")
                {
                    model.ExecDeptNo = int.Parse(ds.Tables[0].Rows[0]["ExecDeptNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CollectDate"].ToString() != "")
                {
                    model.CollectDate = DateTime.Parse(ds.Tables[0].Rows[0]["CollectDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CollectTime"].ToString() != "")
                {
                    model.CollectTime = DateTime.Parse(ds.Tables[0].Rows[0]["CollectTime"].ToString());
                }
                model.Collecter = ds.Tables[0].Rows[0]["Collecter"].ToString();
                model.LABCENTER = ds.Tables[0].Rows[0]["LABCENTER"].ToString();
                if (ds.Tables[0].Rows[0]["NRequestFormNo"].ToString() != "")
                {
                    model.NRequestFormNo = long.Parse(ds.Tables[0].Rows[0]["NRequestFormNo"].ToString());
                }
                model.CheckNo = ds.Tables[0].Rows[0]["CheckNo"].ToString();
                model.DistrictName = ds.Tables[0].Rows[0]["DistrictName"].ToString();
                model.CheckName = ds.Tables[0].Rows[0]["CheckName"].ToString();
                model.WebLisSourceOrgID = ds.Tables[0].Rows[0]["WebLisSourceOrgID"].ToString();
                model.WebLisSourceOrgName = ds.Tables[0].Rows[0]["WebLisSourceOrgName"].ToString();
                model.WardName = ds.Tables[0].Rows[0]["WardName"].ToString();
                model.FolkName = ds.Tables[0].Rows[0]["FolkName"].ToString();
                model.ClinicTypeName = ds.Tables[0].Rows[0]["ClinicTypeName"].ToString();
                model.SampleType = ds.Tables[0].Rows[0]["SampleType"].ToString();
                model.TelNo = ds.Tables[0].Rows[0]["TelNo"].ToString();
                model.WebLisOrgID = ds.Tables[0].Rows[0]["WebLisOrgID"].ToString();
                model.WebLisOrgName = ds.Tables[0].Rows[0]["WebLisOrgName"].ToString();
                model.STATUSName = ds.Tables[0].Rows[0]["STATUSName"].ToString();
                model.jztypeName = ds.Tables[0].Rows[0]["jztypeName"].ToString();
                model.BarCode = ds.Tables[0].Rows[0]["barcode"].ToString();
                model.CombiItemName = ds.Tables[0].Rows[0]["CombiItemName"].ToString();
                if (ds.Tables[0].Rows[0]["printTimes"] != null && ds.Tables[0].Rows[0]["printTimes"].ToString() != "")
                {
                    model.PrintTimes = int.Parse(ds.Tables[0].Rows[0]["printTimes"].ToString());
                }
                if (ds.Tables[0].Columns.Contains("ReceiveDate") && ds.Tables[0].Rows[0]["ReceiveDate"] != null && ds.Tables[0].Rows[0]["ReceiveDate"].ToString() != "")
                {
                    model.ReceiveDate = DateTime.Parse(ds.Tables[0].Rows[0]["ReceiveDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["price"].ToString() != "")
                    model.Price = decimal.Parse(ds.Tables[0].Rows[0]["price"].ToString());
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
            strSql.Append(" FROM NRequestForm ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return idb.ExecuteDataSet(strSql.ToString());
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
            strSql.Append(" FROM NRequestForm ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return idb.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 根据实体获取DataSet
        /// </summary>
        public DataSet GetList(ZhiFang.Model.NRequestForm model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM NRequestForm where 1=1 ");

            if (model.ClientNo != null)
            {
                strSql.Append(" and ClientNo='" + model.ClientNo + "' ");
            }

            if (model.TestTypeName != null)
            {
                strSql.Append(" and TestTypeName='" + model.TestTypeName + "' ");
            }

            if (model.CName != null)
            {
                strSql.Append(" and CName='" + model.CName + "' ");
            }

            if (model.DoctorName != null)
            {
                strSql.Append(" and DoctorName='" + model.DoctorName + "' ");
            }

            if (model.CollecterName != null)
            {
                strSql.Append(" and CollecterName='" + model.CollecterName + "' ");
            }

            if (model.SampleTypeName != null)
            {
                strSql.Append(" and SampleTypeName='" + model.SampleTypeName + "' ");
            }

            if (model.SerialNo != null)
            {
                strSql.Append(" and SerialNo='" + model.SerialNo + "' ");
            }

            if (model.ReceiveFlag != null)
            {
                strSql.Append(" and ReceiveFlag=" + model.ReceiveFlag + " ");
            }

            if (model.StatusNo != null)
            {
                strSql.Append(" and StatusNo=" + model.StatusNo + " ");
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

            if (model.PatNo != null)
            {
                //strSql.Append(" and PatNo='" + model.PatNo + "' ");
                strSql.Append(" and PatNo like '%" + model.PatNo + "%'");
            }

            if (model.ClientName != null)
            {
                strSql.Append(" and ClientName='" + model.ClientName + "' ");
            }

            if (model.GenderNo != null)
            {
                strSql.Append(" and GenderNo=" + model.GenderNo + " ");
            }

            if (model.Birthday != null)
            {
                strSql.Append(" and Birthday='" + model.Birthday + "' ");
            }

            if (model.Age != null)
            {
                strSql.Append(" and Age=" + model.Age + " ");
            }

            if (model.AgeUnitNo != null)
            {
                strSql.Append(" and AgeUnitNo=" + model.AgeUnitNo + " ");
            }

            if (model.FolkNo != null)
            {
                strSql.Append(" and FolkNo=" + model.FolkNo + " ");
            }

            if (model.DistrictNo != null)
            {
                strSql.Append(" and DistrictNo=" + model.DistrictNo + " ");
            }

            if (model.WardNo != null)
            {
                strSql.Append(" and WardNo=" + model.WardNo + " ");
            }

            if (model.Bed != null)
            {
                strSql.Append(" and Bed='" + model.Bed + "' ");
            }

            if (model.DeptNo != null)
            {
                strSql.Append(" and DeptNo=" + model.DeptNo + " ");
            }

            if (model.Doctor != null)
            {
                strSql.Append(" and Doctor=" + model.Doctor + " ");
            }

            if (model.AgeUnitName != null)
            {
                strSql.Append(" and AgeUnitName='" + model.AgeUnitName + "' ");
            }

            if (model.DiagNo != null)
            {
                strSql.Append(" and DiagNo=" + model.DiagNo + " ");
            }

            if (model.Diag != null)
            {
                strSql.Append(" and Diag='" + model.Diag + "' ");
            }

            if (model.ChargeNo != null)
            {
                strSql.Append(" and ChargeNo=" + model.ChargeNo + " ");
            }

            if (model.Charge != null)
            {
                strSql.Append(" and Charge=" + model.Charge + " ");
            }

            if (model.Chargeflag != null)
            {
                strSql.Append(" and Chargeflag='" + model.Chargeflag + "' ");
            }

            if (model.CountNodesFormSource != null)
            {
                strSql.Append(" and CountNodesFormSource='" + model.CountNodesFormSource + "' ");
            }

            if (model.IsCheckFee != null)
            {
                strSql.Append(" and IsCheckFee=" + model.IsCheckFee + " ");
            }

            if (model.Operator != null)
            {
                strSql.Append(" and Operator='" + model.Operator + "' ");
            }

            if (model.OperDate != null)
            {
                strSql.Append(" and OperDate='" + model.OperDate + "' ");
            }

            if (model.OperTime != null)
            {
                strSql.Append(" and OperTime='" + model.OperTime + "' ");
            }

            if (model.GenderName != null)
            {
                strSql.Append(" and GenderName='" + model.GenderName + "' ");
            }

            if (model.FormMemo != null)
            {
                strSql.Append(" and FormMemo='" + model.FormMemo + "' ");
            }

            if (model.RequestSource != null)
            {
                strSql.Append(" and RequestSource='" + model.RequestSource + "' ");
            }

            if (model.SickOrder != null)
            {
                strSql.Append(" and SickOrder='" + model.SickOrder + "' ");
            }

            if (model.jztype != null)
            {
                strSql.Append(" and jztype=" + model.jztype + " ");
            }

            if (model.zdy1 != null)
            {
                strSql.Append(" and zdy1='" + model.zdy1 + "' ");
            }

            if (model.zdy2 != null)
            {
                strSql.Append(" and zdy2='" + model.zdy2 + "' ");
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

            if (model.FlagDateDelete != null)
            {
                strSql.Append(" and FlagDateDelete='" + model.FlagDateDelete + "' ");
            }

            if (model.DeptName != null)
            {
                strSql.Append(" and DeptName='" + model.DeptName + "' ");
            }

            if (model.NurseFlag != null)
            {
                strSql.Append(" and NurseFlag=" + model.NurseFlag + " ");
            }

            if (model.IsByHand != null)
            {
                strSql.Append(" and IsByHand='" + model.IsByHand + "' ");
            }

            if (model.TestTypeNo != null)
            {
                strSql.Append(" and TestTypeNo=" + model.TestTypeNo + " ");
            }

            if (model.ExecDeptNo != null)
            {
                strSql.Append(" and ExecDeptNo=" + model.ExecDeptNo + " ");
            }

            if (model.CollectDate != null)
            {
                strSql.Append(" and CollectDate='" + model.CollectDate + "' ");
            }

            if (model.CollectTime != null)
            {
                strSql.Append(" and CollectTime='" + model.CollectTime + "' ");
            }

            if (model.Collecter != null)
            {
                strSql.Append(" and Collecter='" + model.Collecter + "' ");
            }

            if (model.LABCENTER != null)
            {
                strSql.Append(" and LABCENTER='" + model.LABCENTER + "' ");
            }

            if (model.NRequestFormNo != null)
            {
                strSql.Append(" and NRequestFormNo='" + model.NRequestFormNo + "' ");
            }

            if (model.CheckNo != null)
            {
                strSql.Append(" and CheckNo='" + model.CheckNo + "' ");
            }

            if (model.DistrictName != null)
            {
                strSql.Append(" and DistrictName='" + model.DistrictName + "' ");
            }

            if (model.CheckName != null)
            {
                strSql.Append(" and CheckName='" + model.CheckName + "' ");
            }

            if (model.WebLisSourceOrgID != null)
            {
                strSql.Append(" and WebLisSourceOrgID='" + model.WebLisSourceOrgID + "' ");
            }

            if (model.WebLisSourceOrgName != null)
            {
                strSql.Append(" and WebLisSourceOrgName='" + model.WebLisSourceOrgName + "' ");
            }

            if (model.WardName != null)
            {
                strSql.Append(" and WardName='" + model.WardName + "' ");
            }

            if (model.FolkName != null)
            {
                strSql.Append(" and FolkName='" + model.FolkName + "' ");
            }

            if (model.ClinicTypeName != null)
            {
                strSql.Append(" and ClinicTypeName='" + model.ClinicTypeName + "' ");
            }
            if (model.PersonID != null)
            {
                strSql.Append(" and PersonID='" + model.PersonID + "' ");
            }
            if (model.SampleType != null)
            {
                strSql.Append(" and SampleType='" + model.SampleType + "' ");
            }
            if (model.TelNo != null)
            {
                strSql.Append(" and TelNo='" + model.TelNo + "' ");
            }
            if (model.WebLisOrgID != null)
            {
                strSql.Append(" and WebLisOrgID='" + model.WebLisOrgID + "' ");
            }
            if (model.WebLisOrgName != null)
            {
                strSql.Append(" and WebLisOrgName='" + model.WebLisOrgName + "' ");
            }
            if (model.STATUSName != null)
            {
                strSql.Append(" and STATUSName='" + model.STATUSName + "' ");
            }
            if (model.jztypeName != null)
            {
                strSql.Append(" and jztypeName='" + model.jztypeName + "' ");
            }
            ZhiFang.Common.Log.Log.Info(strSql.ToString());
            return idb.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 获取总记录
        /// </summary>
        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM NRequestForm ");
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

        public int GetTotalCount(ZhiFang.Model.NRequestForm model)
        {
            return this.GetNRequstFormListTotalCount(model);
        }

        /// <summary>
        /// 利用标识列分页
        /// </summary>
        /// <param name="model">model=null时为中心字典表分页；model!=null时为中心--实验室 对照关系分页</param>
        /// <param name="nowPageNum">第几页</param>
        /// <param name="nowPageSize">每页多少行</param>
        /// <returns>DataSet</returns>
        public DataSet GetListByPage(ZhiFang.Model.NRequestForm model, int nowPageNum, int nowPageSize)
        {
            return this.GetNRequstFormListByPage(model, nowPageNum, nowPageSize);
        }
        public DataSet GetListBy(ZhiFang.Model.NRequestForm model)
        {
            StringBuilder strwhere = new StringBuilder();
            #region where
            if (model.ClientNo != null)
            {
                strwhere.Append(" and NRequestForm.ClientNo='" + model.ClientNo + "' ");
            }

            if (model.ClientNoList != null && model.ClientNoList.Count > 0)
            {
                strwhere.Append(" and NRequestForm.ClientNo in ('" + string.Join("','", model.ClientNoList) + "') ");
            }

            if (model.TestTypeName != null)
            {
                strwhere.Append(" and NRequestForm.TestTypeName='" + model.TestTypeName + "' ");
            }

            if (model.CName != null && model.CName != "")
            {
                strwhere.Append(" and NRequestForm.CName='" + model.CName + "' ");
            }

            if (model.DoctorName != null && model.DoctorName != "")
            {
                strwhere.Append(" and NRequestForm.DoctorName='" + model.DoctorName + "' ");
            }

            if (model.CollecterName != null)
            {
                strwhere.Append(" and NRequestForm.CollecterName='" + model.CollecterName + "' ");
            }

            if (model.SampleTypeName != null)
            {
                strwhere.Append(" and NRequestForm.SampleTypeName='" + model.SampleTypeName + "' ");
            }

            if (model.SerialNo != null)
            {
                strwhere.Append(" and NRequestForm.SerialNo='" + model.SerialNo + "' ");
            }

            if (model.ReceiveFlag != null)
            {
                strwhere.Append(" and NRequestForm.ReceiveFlag=" + model.ReceiveFlag + " ");
            }

            if (model.StatusNo != null)
            {
                strwhere.Append(" and NRequestForm.StatusNo=" + model.StatusNo + " ");
            }

            if (model.SampleTypeNo != null)
            {
                strwhere.Append(" and NRequestForm.SampleTypeNo=" + model.SampleTypeNo + " ");
            }

            if (model.PatNo != null && model.PatNo != "")
            {
                strwhere.Append(" and NRequestForm.PatNo like '%" + model.PatNo + "%' ");
            }

            if (model.ClientName != null)
            {
                strwhere.Append(" and NRequestForm.ClientName='" + model.ClientName + "' ");
            }

            if (model.GenderNo != null)
            {
                strwhere.Append(" and NRequestForm.GenderNo=" + model.GenderNo + " ");
            }

            if (model.Birthday != null)
            {
                strwhere.Append(" and NRequestForm.Birthday='" + model.Birthday + "' ");
            }

            if (model.Age != null)
            {
                strwhere.Append(" and NRequestForm.Age=" + model.Age + " ");
            }

            if (model.AgeUnitNo != null)
            {
                strwhere.Append(" and NRequestForm.AgeUnitNo=" + model.AgeUnitNo + " ");
            }

            if (model.FolkNo != null)
            {
                strwhere.Append(" and NRequestForm.FolkNo=" + model.FolkNo + " ");
            }

            if (model.DistrictNo != null)
            {
                strwhere.Append(" and NRequestForm.DistrictNo=" + model.DistrictNo + " ");
            }

            if (model.WardNo != null)
            {
                strwhere.Append(" and NRequestForm.WardNo=" + model.WardNo + " ");
            }

            if (model.Bed != null)
            {
                strwhere.Append(" and NRequestForm.Bed='" + model.Bed + "' ");
            }

            if (model.DeptNo != null)
            {
                strwhere.Append(" and NRequestForm.DeptNo=" + model.DeptNo + " ");
            }

            if (model.Doctor != null)
            {
                strwhere.Append(" and NRequestForm.Doctor=" + model.Doctor + " ");
            }

            if (model.AgeUnitName != null)
            {
                strwhere.Append(" and NRequestForm.AgeUnitName='" + model.AgeUnitName + "' ");
            }

            if (model.DiagNo != null)
            {
                strwhere.Append(" and NRequestForm.DiagNo=" + model.DiagNo + " ");
            }

            if (model.Diag != null)
            {
                strwhere.Append(" and NRequestForm.Diag='" + model.Diag + "' ");
            }

            if (model.ChargeNo != null)
            {
                strwhere.Append(" and NRequestForm.ChargeNo=" + model.ChargeNo + " ");
            }

            if (model.Charge != null)
            {
                strwhere.Append(" and NRequestForm.Charge=" + model.Charge + " ");
            }

            if (model.Chargeflag != null)
            {
                strwhere.Append(" and NRequestForm.Chargeflag='" + model.Chargeflag + "' ");
            }

            if (model.CountNodesFormSource != null)
            {
                strwhere.Append(" and NRequestForm.CountNodesFormSource='" + model.CountNodesFormSource + "' ");
            }

            if (model.IsCheckFee != null)
            {
                strwhere.Append(" and NRequestForm.IsCheckFee=" + model.IsCheckFee + " ");
            }

            if (model.Operator != null)
            {
                strwhere.Append(" and NRequestForm.Operator='" + model.Operator + "' ");
            }

            if (model.OperDate != null)
            {
                strwhere.Append(" and NRequestForm.OperDate='" + model.OperDate + "' ");
            }

            if (model.OperTime != null)
            {
                strwhere.Append(" and NRequestForm.OperTime='" + model.OperTime + "' ");
            }

            if (model.GenderName != null)
            {
                strwhere.Append(" and NRequestForm.GenderName='" + model.GenderName + "' ");
            }

            if (model.FormMemo != null)
            {
                strwhere.Append(" and NRequestForm.FormMemo='" + model.FormMemo + "' ");
            }

            if (model.RequestSource != null)
            {
                strwhere.Append(" and NRequestForm.RequestSource='" + model.RequestSource + "' ");
            }

            if (model.SickOrder != null)
            {
                strwhere.Append(" and NRequestForm.SickOrder='" + model.SickOrder + "' ");
            }

            if (model.jztype != null)
            {
                strwhere.Append(" and NRequestForm.jztype=" + model.jztype + " ");
            }

            if (model.zdy1 != null)
            {
                strwhere.Append(" and NRequestForm.zdy1='" + model.zdy1 + "' ");
            }

            if (model.zdy2 != null)
            {
                strwhere.Append(" and NRequestForm.zdy2='" + model.zdy2 + "' ");
            }

            if (model.zdy3 != null)
            {
                strwhere.Append(" and NRequestForm.zdy3='" + model.zdy3 + "' ");
            }

            if (model.zdy4 != null)
            {
                strwhere.Append(" and NRequestForm.zdy4='" + model.zdy4 + "' ");
            }

            if (model.zdy5 != null)
            {
                strwhere.Append(" and NRequestForm.zdy5='" + model.zdy5 + "' ");
            }

            if (model.FlagDateDelete != null)
            {
                strwhere.Append(" and NRequestForm.FlagDateDelete='" + model.FlagDateDelete + "' ");
            }

            if (model.DeptName != null)
            {
                strwhere.Append(" and NRequestForm.DeptName='" + model.DeptName + "' ");
            }

            if (model.NurseFlag != null)
            {
                strwhere.Append(" and NRequestForm.NurseFlag=" + model.NurseFlag + " ");
            }

            if (model.IsByHand != null)
            {
                strwhere.Append(" and NRequestForm.IsByHand='" + model.IsByHand + "' ");
            }

            if (model.TestTypeNo != null)
            {
                strwhere.Append(" and NRequestForm.TestTypeNo=" + model.TestTypeNo + " ");
            }

            if (model.ExecDeptNo != null)
            {
                strwhere.Append(" and NRequestForm.ExecDeptNo=" + model.ExecDeptNo + " ");
            }

            if (model.CollectDate != null)
            {
                strwhere.Append(" and NRequestForm.CollectDate='" + model.CollectDate + "' ");
            }

            if (model.CollectTime != null)
            {
                strwhere.Append(" and NRequestForm.CollectTime='" + model.CollectTime + "' ");
            }

            if (model.Collecter != null)
            {
                strwhere.Append(" and NRequestForm.Collecter='" + model.Collecter + "' ");
            }

            if (model.LABCENTER != null)
            {
                strwhere.Append(" and NRequestForm.LABCENTER='" + model.LABCENTER + "' ");
            }

            if (model.NRequestFormNo != null)
            {
                strwhere.Append(" and NRequestForm.NRequestFormNo='" + model.NRequestFormNo + "' ");
            }

            if (model.CheckNo != null)
            {
                strwhere.Append(" and NRequestForm.CheckNo='" + model.CheckNo + "' ");
            }

            if (model.DistrictName != null)
            {
                strwhere.Append(" and NRequestForm.DistrictName='" + model.DistrictName + "' ");
            }

            if (model.CheckName != null)
            {
                strwhere.Append(" and NRequestForm.CheckName='" + model.CheckName + "' ");
            }

            if (model.WebLisSourceOrgID != null)
            {
                strwhere.Append(" and NRequestForm.WebLisSourceOrgID='" + model.WebLisSourceOrgID + "' ");
            }

            if (model.WebLisSourceOrgName != null)
            {
                strwhere.Append(" and NRequestForm.WebLisSourceOrgName='" + model.WebLisSourceOrgName + "' ");
            }

            if (model.WardName != null)
            {
                strwhere.Append(" and NRequestForm.WardName='" + model.WardName + "' ");
            }

            if (model.FolkName != null)
            {
                strwhere.Append(" and NRequestForm.FolkName='" + model.FolkName + "' ");
            }

            if (model.ClinicTypeName != null)
            {
                strwhere.Append(" and NRequestForm.ClinicTypeName='" + model.ClinicTypeName + "' ");
            }
            if (model.OperDateStart != null && model.OperDateStart != "")
            {
                strwhere.Append(" and NRequestForm.OperDate >='" + model.OperDateStart + "'  ");
            }
            if (model.OperDateEnd != null && model.OperDateEnd != "")
            {
                strwhere.Append(" and NRequestForm.OperDate <='" + model.OperDateEnd + "' ");
            }

            if (model.CollectDateStart != null)
            {
                strwhere.Append(" and BarCodeForm.CollectDate >='" + model.CollectDateStart + "'  ");
            }
            if (model.CollectDateEnd != null)
            {
                strwhere.Append(" and BarCodeForm.CollectDate <='" + model.CollectDateEnd + "'  ");
            }
            if (model.ClientList != null && model.ClientList.Trim() != "")
            {
                strwhere.Append(" and NRequestForm.ClientNo  in  (" + model.ClientList + ")  ");
            }
            #endregion
            StringBuilder strSql = new StringBuilder();
            if (model != null)
            {
                strSql.Append(" SELECT distinct NRequestForm.NRequestFormNo,  BarCodeForm.BarCode as BarCode, BarCodeForm.BarCodeFormNo as BarCodeFormNo,BarCodeForm.PrintCount as PrintCount, NRequestForm.CName as CName,NRequestForm.DoctorName as DoctorName, BarCodeForm.WebLisSourceOrgID,BarCodeForm.WebLisSourceOrgName as WebLisSourceOrgName,NRequestForm.ClientNo,NRequestForm.ClientName as ClientName,NRequestForm.AgeUnitName as AgeUnitName, NRequestForm.Diag as Diag,GenderType.CName AS Sex, " +
                        "NRequestForm.Age as Age, SampleType.CName AS SampleName, Convert(varchar(10),BarCodeForm.inceptdate,21)+' '+Convert(varchar(10),BarCodeForm.incepttime,8)as incepttime,Convert(varchar(10),BarCodeForm.CollectDate,21)+' '+Convert(varchar(10),BarCodeForm.CollectTime,8)as CollectTime," +
                        "NRequestForm.OperDate as OperDate,NRequestForm.OperTime as OperTime,PatDiagInfo.DiagDesc as DiagDesc " +
                        "FROM GenderType RIGHT OUTER JOIN " +
                        "NRequestItem INNER JOIN " +
                        "NRequestForm ON " +
                        "NRequestItem.NRequestFormNo = NRequestForm.NRequestFormNo LEFT OUTER JOIN " +
                        "PatDiagInfo ON NRequestForm.DiagNo = PatDiagInfo.DiagNo LEFT OUTER JOIN " +
                        "SampleType ON NRequestForm.SampleTypeNo = SampleType.SampleTypeNo ON  " +
                        "GenderType.GenderNo = NRequestForm.GenderNo LEFT OUTER JOIN " +
                        "BarCodeForm ON NRequestItem.BarCodeFormNo = BarCodeForm.BarCodeFormNo ");

                strSql.Append(" where NRequestItem.NRequestFormNo not in (  ");
                strSql.Append("select top 0 NRequestForm.NRequestFormNo from  GenderType RIGHT OUTER JOIN " +
                        "NRequestItem INNER JOIN " +
                        "NRequestForm ON " +
                        "NRequestItem.NRequestFormNo = NRequestForm.NRequestFormNo LEFT OUTER JOIN " +
                        "PatDiagInfo ON NRequestForm.DiagNo = PatDiagInfo.DiagNo LEFT OUTER JOIN " +
                        "SampleType ON NRequestForm.SampleTypeNo = SampleType.SampleTypeNo ON  " +
                        "GenderType.GenderNo = NRequestForm.GenderNo LEFT OUTER JOIN " +
                        "BarCodeForm ON NRequestItem.BarCodeFormNo = BarCodeForm.BarCodeFormNo where ");
                if (strwhere.ToString().Trim() == "")
                {
                    strSql.Append(" 1=2");
                }
                else
                {
                    strSql.Append(" 1=1 " + strwhere.ToString() + "   ");
                }

                strSql.Append("order by NRequestForm.NRequestFormNo ) ");
                if (strwhere.ToString().Trim() == "")
                {
                    strSql.Append(" and 1=2");
                }
                else
                {
                    strSql.Append("   " + strwhere.ToString() + "   ");
                }
                strSql.Append(" order by NRequestForm.NRequestFormNo  ");
                ZhiFang.Common.Log.Log.Info(strSql.ToString());
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
            else
            {
                strSql.Append(" SELECT distinct NRequestForm.NRequestFormNo,BarCodeForm.BarCode as BarCode, BarCodeForm.BarCodeFormNo as BarCodeFormNo,BarCodeForm.PrintCount as PrintCount, NRequestForm.CName as CName,NRequestForm.DoctorName as DoctorName, NRequestForm.WebLisSourceOrgID,NRequestForm.WebLisSourceOrgName as WebLisSourceOrgName,NRequestForm.ClientNo,NRequestForm.ClientName as ClientName,NRequestForm.AgeUnitName as AgeUnitName, NRequestForm.Diag as Diag,GenderType.CName AS Sex, " +
                        "NRequestForm.Age as Age, SampleType.CName AS SampleName, Convert(varchar(10),BarCodeForm.CollectDate,21)+' '+Convert(varchar(10),BarCodeForm.CollectTime,8)as CollectTime, " +
                        "NRequestForm.OperDate as OperDate,NRequestForm.OperTime as OperTime,PatDiagInfo.DiagDesc as DiagDesc " +
                        "FROM GenderType RIGHT OUTER JOIN " +
                        "NRequestItem INNER JOIN " +
                        "NRequestForm ON " +
                        "NRequestItem.NRequestFormNo = NRequestForm.NRequestFormNo LEFT OUTER JOIN " +
                        "PatDiagInfo ON NRequestForm.DiagNo = PatDiagInfo.DiagNo LEFT OUTER JOIN " +
                        "SampleType ON NRequestForm.SampleTypeNo = SampleType.SampleTypeNo ON  " +
                        "GenderType.GenderNo = NRequestForm.GenderNo LEFT OUTER JOIN " +
                        "BarCodeForm ON NRequestItem.BarCodeFormNo = BarCodeForm.BarCodeFormNo ");

                strSql.Append(" where NRequestFormNo not in (  ");
                strSql.Append("select * NRequestForm.NRequestFormNo from  GenderType RIGHT OUTER JOIN " +
                        "NRequestItem INNER JOIN " +
                        "NRequestForm ON " +
                        "NRequestItem.NRequestFormNo = NRequestForm.NRequestFormNo LEFT OUTER JOIN " +
                        "PatDiagInfo ON NRequestForm.DiagNo = PatDiagInfo.DiagNo LEFT OUTER JOIN " +
                        "SampleType ON NRequestForm.SampleTypeNo = SampleType.SampleTypeNo ON  " +
                        "GenderType.GenderNo = NRequestForm.GenderNo LEFT OUTER JOIN " +
                        "BarCodeForm ON NRequestItem.BarCodeFormNo = BarCodeForm.BarCodeFormNo   ");

                strSql.Append("order by NRequestForm.NRequestFormNo ) order by NRequestForm.NRequestFormNo  ");
                ZhiFang.Common.Log.Log.Info(strSql.ToString());
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
        }


        public bool Exists(long NRequestFormNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from NRequestForm ");
            strSql.Append(" where NRequestFormNo ='" + NRequestFormNo + "'");
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

        public bool CopyToLab(List<string> lst)
        {
            System.Collections.ArrayList arrySql = new System.Collections.ArrayList();
            string LabTableName = "NRequestForm";
            LabTableName = "B_Lab_" + LabTableName.Substring(LabTableName.IndexOf("_") + 1, LabTableName.Length - (LabTableName.IndexOf("_") + 1));
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSqlControl = new StringBuilder();
            string TableKey = "NRequestFormNo";
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
                    strSql.Append(" ClientNo , TestTypeName , CName , DoctorName , CollecterName , SampleTypeName , SerialNo , ReceiveFlag , StatusNo , SampleTypeNo , PatNo , ClientName , GenderNo , Birthday , Age , AgeUnitNo , FolkNo , DistrictNo , WardNo , Bed , DeptNo , Doctor , AgeUnitName , DiagNo , Diag , ChargeNo , Charge , Chargeflag , CountNodesFormSource , IsCheckFee , Operator , OperDate , OperTime , GenderName , FormMemo , RequestSource , SickOrder , jztype , zdy1 , zdy2 , zdy3 , zdy4 , zdy5 , FlagDateDelete , DeptName , NurseFlag , IsByHand , TestTypeNo , ExecDeptNo , CollectDate , CollectTime , Collecter , LABCENTER , LabNRequestFormNo , CheckNo , DistrictName , CheckName , WebLisSourceOrgID , WebLisSourceOrgName , WardName , FolkName , ClinicTypeName ");
                    strSql.Append(") select '" + lst[i].Trim() + "' as LabCode, ");
                    strSql.Append("ClientNo,TestTypeName,CName,DoctorName,CollecterName,SampleTypeName,SerialNo,ReceiveFlag,StatusNo,SampleTypeNo,PatNo,ClientName,GenderNo,Birthday,Age,AgeUnitNo,FolkNo,DistrictNo,WardNo,Bed,DeptNo,Doctor,AgeUnitName,DiagNo,Diag,ChargeNo,Charge,Chargeflag,CountNodesFormSource,IsCheckFee,Operator,OperDate,OperTime,GenderName,FormMemo,RequestSource,SickOrder,jztype,zdy1,zdy2,zdy3,zdy4,zdy5,FlagDateDelete,DeptName,NurseFlag,IsByHand,TestTypeNo,ExecDeptNo,CollectDate,CollectTime,Collecter,LABCENTER,NRequestFormNo,CheckNo,DistrictName,CheckName,WebLisSourceOrgID,WebLisSourceOrgName,WardName,FolkName,ClinicTypeName");
                    strSql.Append(" from NRequestForm ");

                    strSqlControl.Append("insert into NRequestFormControl ( ");
                    strSqlControl.Append(" " + TableKeySub + "ControlNo," + TableKey + ",ControlLabNo,Control" + TableKey + ",UseFlag ");
                    strSqlControl.Append(")  select ");
                    strSqlControl.Append("  '" + lst[i].Trim() + "'+'_'+convert(varchar(50)," + TableKey + ")+'_'+convert(varchar(50), " + TableKey + ") as " + TableKeySub + "ControlNo," + TableKey + ",'" + lst[i].Trim() + "' as ControlLabNo," + TableKey + ",UseFlag ");
                    strSqlControl.Append(" from NRequestForm ");

                    arrySql.Add(strSql.ToString());
                    arrySql.Add(strSqlControl.ToString());

                    strSql = new StringBuilder();
                    strSqlControl = new StringBuilder();

                    //d_log.OperateLog("NRequestFormControl", "", "", DateTime.Now, 1);
                    //d_log.OperateLog(LabTableName, "", "", DateTime.Now, 1);
                    //d_log.OperateLog("equestForm", "", "", DateTime.Now, 1);
                }

                idb.BatchUpdateWithTransaction(arrySql);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public int GetMaxId()
        {
            return idb.GetMaxID("NRequestFormNo", "NRequestForm");
        }

        public DataSet GetList(int Top, ZhiFang.Model.NRequestForm model, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM NRequestForm ");


            if (model.ClientNo != null)
            {

                strSql.Append(" and ClientNo='" + model.ClientNo + "' ");
            }

            if (model.TestTypeName != null)
            {

                strSql.Append(" and TestTypeName='" + model.TestTypeName + "' ");
            }

            if (model.CName != null)
            {

                strSql.Append(" and CName='" + model.CName + "' ");
            }

            if (model.DoctorName != null)
            {

                strSql.Append(" and DoctorName='" + model.DoctorName + "' ");
            }

            if (model.CollecterName != null)
            {

                strSql.Append(" and CollecterName='" + model.CollecterName + "' ");
            }

            if (model.SampleTypeName != null)
            {

                strSql.Append(" and SampleTypeName='" + model.SampleTypeName + "' ");
            }

            if (model.SerialNo != null)
            {

                strSql.Append(" and SerialNo='" + model.SerialNo + "' ");
            }

            if (model.ReceiveFlag != null)
            {
                strSql.Append(" and ReceiveFlag=" + model.ReceiveFlag + " ");
            }

            if (model.StatusNo != null)
            {
                strSql.Append(" and StatusNo=" + model.StatusNo + " ");
            }

            if (model.SampleTypeNo != null)
            {
                strSql.Append(" and SampleTypeNo=" + model.SampleTypeNo + " ");
            }

            if (model.PatNo != null)
            {

                strSql.Append(" and PatNo='" + model.PatNo + "' ");
            }

            if (model.ClientName != null)
            {

                strSql.Append(" and ClientName='" + model.ClientName + "' ");
            }

            if (model.GenderNo != null)
            {
                strSql.Append(" and GenderNo=" + model.GenderNo + " ");
            }

            if (model.Birthday != null)
            {

                strSql.Append(" and Birthday='" + model.Birthday + "' ");
            }

            if (model.Age != null)
            {
                strSql.Append(" and Age=" + model.Age + " ");
            }

            if (model.AgeUnitNo != null)
            {
                strSql.Append(" and AgeUnitNo=" + model.AgeUnitNo + " ");
            }

            if (model.FolkNo != null)
            {
                strSql.Append(" and FolkNo=" + model.FolkNo + " ");
            }

            if (model.DistrictNo != null)
            {
                strSql.Append(" and DistrictNo=" + model.DistrictNo + " ");
            }

            if (model.WardNo != null)
            {
                strSql.Append(" and WardNo=" + model.WardNo + " ");
            }

            if (model.Bed != null)
            {

                strSql.Append(" and Bed='" + model.Bed + "' ");
            }

            if (model.DeptNo != null)
            {
                strSql.Append(" and DeptNo=" + model.DeptNo + " ");
            }

            if (model.Doctor != null)
            {
                strSql.Append(" and Doctor=" + model.Doctor + " ");
            }

            if (model.AgeUnitName != null)
            {

                strSql.Append(" and AgeUnitName='" + model.AgeUnitName + "' ");
            }

            if (model.DiagNo != null)
            {
                strSql.Append(" and DiagNo=" + model.DiagNo + " ");
            }

            if (model.Diag != null)
            {

                strSql.Append(" and Diag='" + model.Diag + "' ");
            }

            if (model.ChargeNo != null)
            {
                strSql.Append(" and ChargeNo=" + model.ChargeNo + " ");
            }

            if (model.Charge != null)
            {
                strSql.Append(" and Charge=" + model.Charge + " ");
            }

            if (model.Chargeflag != null)
            {

                strSql.Append(" and Chargeflag='" + model.Chargeflag + "' ");
            }

            if (model.CountNodesFormSource != null)
            {

                strSql.Append(" and CountNodesFormSource='" + model.CountNodesFormSource + "' ");
            }

            if (model.IsCheckFee != null)
            {
                strSql.Append(" and IsCheckFee=" + model.IsCheckFee + " ");
            }

            if (model.Operator != null)
            {

                strSql.Append(" and Operator='" + model.Operator + "' ");
            }

            if (model.OperDate != null)
            {

                strSql.Append(" and OperDate='" + model.OperDate + "' ");
            }

            if (model.OperTime != null)
            {

                strSql.Append(" and OperTime='" + model.OperTime + "' ");
            }

            if (model.GenderName != null)
            {

                strSql.Append(" and GenderName='" + model.GenderName + "' ");
            }

            if (model.FormMemo != null)
            {

                strSql.Append(" and FormMemo='" + model.FormMemo + "' ");
            }

            if (model.RequestSource != null)
            {

                strSql.Append(" and RequestSource='" + model.RequestSource + "' ");
            }

            if (model.SickOrder != null)
            {

                strSql.Append(" and SickOrder='" + model.SickOrder + "' ");
            }

            if (model.jztype != null)
            {
                strSql.Append(" and jztype=" + model.jztype + " ");
            }

            if (model.zdy1 != null)
            {

                strSql.Append(" and zdy1='" + model.zdy1 + "' ");
            }

            if (model.zdy2 != null)
            {

                strSql.Append(" and zdy2='" + model.zdy2 + "' ");
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

            if (model.FlagDateDelete != null)
            {

                strSql.Append(" and FlagDateDelete='" + model.FlagDateDelete + "' ");
            }

            if (model.DeptName != null)
            {

                strSql.Append(" and DeptName='" + model.DeptName + "' ");
            }

            if (model.NurseFlag != null)
            {
                strSql.Append(" and NurseFlag=" + model.NurseFlag + " ");
            }

            if (model.IsByHand != null)
            {

                strSql.Append(" and IsByHand='" + model.IsByHand + "' ");
            }

            if (model.TestTypeNo != null)
            {
                strSql.Append(" and TestTypeNo=" + model.TestTypeNo + " ");
            }

            if (model.ExecDeptNo != null)
            {
                strSql.Append(" and ExecDeptNo=" + model.ExecDeptNo + " ");
            }

            if (model.CollectDate != null)
            {

                strSql.Append(" and CollectDate='" + model.CollectDate + "' ");
            }

            if (model.CollectTime != null)
            {

                strSql.Append(" and CollectTime='" + model.CollectTime + "' ");
            }

            if (model.Collecter != null)
            {

                strSql.Append(" and Collecter='" + model.Collecter + "' ");
            }

            if (model.LABCENTER != null)
            {

                strSql.Append(" and LABCENTER='" + model.LABCENTER + "' ");
            }

            if (model.NRequestFormNo != null)
            {

                strSql.Append(" and NRequestFormNo='" + model.NRequestFormNo + "' ");
            }

            if (model.CheckNo != null)
            {

                strSql.Append(" and CheckNo='" + model.CheckNo + "' ");
            }

            if (model.DistrictName != null)
            {

                strSql.Append(" and DistrictName='" + model.DistrictName + "' ");
            }

            if (model.CheckName != null)
            {

                strSql.Append(" and CheckName='" + model.CheckName + "' ");
            }

            if (model.WebLisSourceOrgID != null)
            {

                strSql.Append(" and WebLisSourceOrgID='" + model.WebLisSourceOrgID + "' ");
            }

            if (model.WebLisSourceOrgName != null)
            {

                strSql.Append(" and WebLisSourceOrgName='" + model.WebLisSourceOrgName + "' ");
            }

            if (model.WardName != null)
            {

                strSql.Append(" and WardName='" + model.WardName + "' ");
            }

            if (model.FolkName != null)
            {

                strSql.Append(" and FolkName='" + model.FolkName + "' ");
            }

            if (model.ClinicTypeName != null)
            {

                strSql.Append(" and ClinicTypeName='" + model.ClinicTypeName + "' ");
            }
            if (model.PersonID != null)
            {
                strSql.Append(" and PersonID='" + model.PersonID + "' ");
            }
            if (model.SampleType != null)
            {
                strSql.Append(" and SampleType='" + model.SampleType + "' ");
            }
            if (model.TelNo != null)
            {
                strSql.Append(" and TelNo='" + model.TelNo + "' ");
            }
            if (model.WebLisOrgID != null)
            {
                strSql.Append(" and WebLisOrgID='" + model.WebLisOrgID + "' ");
            }
            if (model.WebLisOrgName != null)
            {
                strSql.Append(" and WebLisOrgName='" + model.WebLisOrgName + "' ");
            }
            if (model.STATUSName != null)
            {
                strSql.Append(" and STATUSName='" + model.STATUSName + "' ");
            }
            if (model.jztypeName != null)
            {
                strSql.Append(" and jztypeName='" + model.jztypeName + "' ");
            }
            strSql.Append(" order by " + filedOrder);
            return idb.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetAllList()
        {
            return GetList("");
        }

        public DataSet GetListByBarCodeNo(string BarCodeNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT (SELECT CName FROM dbo.TestItem AS TestItem WHERE (ItemNo = dbo.NRequestItem.ParItemNo)) AS TestItemCName,NRequestItem.CombiItemNo,BarCodeForm.BarCode, dbo.NRequestItem.ParItemNo, dbo.BarCodeForm.CollecterID, dbo.BarCodeForm.CollectDate AS Expr1, dbo.BarCodeForm.CollectTime AS Expr2, dbo.BarCodeForm.Collecter AS Expr3, (SELECT CName  FROM dbo.TestItem AS TestItem_2 WHERE      (ItemNo = dbo.NRequestItem.CombiItemNo)) AS CombiItemName, dbo.NRequestForm.* FROM         dbo.NRequestForm INNER JOIN                      dbo.NRequestItem ON dbo.NRequestItem.NRequestFormNo = dbo.NRequestForm.NRequestFormNo INNER JOIN dbo.TestItem AS TestItem_1 ON dbo.NRequestItem.ParItemNo = TestItem_1.ItemNo RIGHT OUTER JOIN  dbo.BarCodeForm ON dbo.NRequestItem.BarCodeFormNo = dbo.BarCodeForm.BarCodeFormNo");
            if (BarCodeNo.Trim() != "")
            {
                strSql.Append(" where BarCodeForm.BarCode='" + BarCodeNo + "'");
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
        public DataSet GetListByBarCodeList(List<string> BarCodeList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT (SELECT CName FROM dbo.TestItem AS TestItem WHERE (ItemNo = dbo.NRequestItem.ParItemNo)) AS TestItemCName,NRequestItem.CombiItemNo,BarCodeForm.BarCode, dbo.NRequestItem.ParItemNo, dbo.BarCodeForm.CollecterID, dbo.BarCodeForm.CollectDate AS Expr1, dbo.BarCodeForm.CollectTime AS Expr2, dbo.BarCodeForm.Collecter AS Expr3, (SELECT CName  FROM dbo.TestItem AS TestItem_2 WHERE      (ItemNo = dbo.NRequestItem.CombiItemNo)) AS CombiItemName, dbo.NRequestForm.* FROM         dbo.NRequestForm INNER JOIN                      dbo.NRequestItem ON dbo.NRequestItem.NRequestFormNo = dbo.NRequestForm.NRequestFormNo INNER JOIN dbo.TestItem AS TestItem_1 ON dbo.NRequestItem.ParItemNo = TestItem_1.ItemNo RIGHT OUTER JOIN  dbo.BarCodeForm ON dbo.NRequestItem.BarCodeFormNo = dbo.BarCodeForm.BarCodeFormNo");
            if (BarCodeList!=null && BarCodeList.Count()>0)
            {
                strSql.Append(" where BarCodeForm.BarCode in ('" + string.Join("','", BarCodeList) + "') ");
            }
            else
            {
                return null;
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
        public DataSet GetListByModel(ZhiFang.Model.NRequestForm NRequestForm, ZhiFang.Model.BarCodeForm BarCodeForm)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT (SELECT CName FROM dbo.TestItem AS TestItem WHERE (ItemNo = dbo.NRequestItem.ParItemNo)) AS TestItemCName,BarCodeForm.BarCode, dbo.NRequestItem.ParItemNo, dbo.BarCodeForm.CollecterID, (SELECT CName  FROM dbo.TestItem AS TestItem_2 WHERE      (ItemNo = dbo.NRequestItem.CombiItemNo)) AS CombiItemName, dbo.NRequestForm.* FROM         dbo.NRequestForm INNER JOIN                      dbo.NRequestItem ON dbo.NRequestItem.NRequestFormNo = dbo.NRequestForm.NRequestFormNo INNER JOIN dbo.TestItem AS TestItem_1 ON dbo.NRequestItem.ParItemNo = TestItem_1.ItemNo RIGHT OUTER JOIN  dbo.BarCodeForm ON dbo.NRequestItem.BarCodeFormNo = dbo.BarCodeForm.BarCodeFormNo where 1=1");
            if (BarCodeForm.BarCode != "" && BarCodeForm.BarCode != null)
            {
                strSql.Append(" and BarCodeForm.BarCode='" + BarCodeForm.BarCode + "'");
            }
            if (NRequestForm.WebLisSourceOrgID != "" && NRequestForm.WebLisSourceOrgID != null)
            {
                strSql.Append(" and NRequestForm.WebLisSourceOrgID='" + NRequestForm.WebLisSourceOrgID + "'");
            }
            if (NRequestForm.ClientNo != "" && NRequestForm.ClientNo != null)
            {
                strSql.Append(" and NRequestForm.ClientNo='" + NRequestForm.ClientNo + "'");
            }
            if (NRequestForm.CName != "" && NRequestForm.CName != null)
            {
                strSql.Append(" and NRequestForm.CName='" + NRequestForm.CName + "'");

            }
            if (NRequestForm.PatNo != "" && NRequestForm.PatNo != null)
            {
                strSql.Append(" and NRequestForm.PatNo like '%" + NRequestForm.PatNo + "%'");

            }
            Common.Log.Log.Info("样本流转：" + strSql);
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }


        public int AddUpdateByDataSet(DataSet ds)
        {
            throw new NotImplementedException();
        }

        public int Delete(string SerialNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from NRequestForm ");
            strSql.Append(" where SerialNo=@SerialNo ");
            SqlParameter[] parameters = {
                    new SqlParameter("@SerialNo", SqlDbType.VarChar,50)         };
            parameters[0].Value = SerialNo;
            return idb.ExecuteNonQuery(strSql.ToString(), parameters);
        }

        public int UpdateByList(List<string> listStrColumnNf, List<string> listStrDataNf)
        {
            throw new NotImplementedException();
        }

        public int AddByList(List<string> listStrColumnNf, List<string> listStrDataNf)
        {
            string strSql = "insert into NrequestForm (";
            for (int i = 0; i < listStrColumnNf.Count; i++)
            {
                strSql += listStrColumnNf[i].ToString() + ",";
            }
            strSql = strSql.Remove(strSql.Length - 1);
            strSql += ") values( ";
            for (int h = 0; h < listStrDataNf.Count; h++)
            {
                strSql += "'" + listStrDataNf[h].ToString() + "',";
            }
            strSql = strSql.Remove(strSql.Length - 1);
            strSql += ")";
            return DbHelperSQL.ExecuteNonQuery(strSql);
        }

        public int GetNRequstFormListTotalCount(Model.NRequestForm model)
        {
            StringBuilder strwhere = new StringBuilder();
            #region where
            if (model.ClientNo != null)
            {
                strwhere.Append(" and NRequestForm.ClientNo='" + model.ClientNo + "' ");
            }

            if (model.TestTypeName != null)
            {
                strwhere.Append(" and NRequestForm.TestTypeName='" + model.TestTypeName + "' ");
            }

            if (model.CName != null)
            {
                strwhere.Append(" and NRequestForm.CName='" + model.CName + "' ");
            }

            if (model.DoctorName != null)
            {
                strwhere.Append(" and NRequestForm.DoctorName='" + model.DoctorName + "' ");
            }

            if (model.CollecterName != null)
            {
                strwhere.Append(" and NRequestForm.CollecterName='" + model.CollecterName + "' ");
            }

            if (model.SampleTypeName != null)
            {
                strwhere.Append(" and NRequestForm.SampleTypeName='" + model.SampleTypeName + "' ");
            }

            if (model.SerialNo != null)
            {
                strwhere.Append(" and NRequestForm.SerialNo='" + model.SerialNo + "' ");
            }

            if (model.ReceiveFlag != null)
            {
                strwhere.Append(" and NRequestForm.ReceiveFlag=" + model.ReceiveFlag + " ");
            }

            if (model.StatusNo != null)
            {
                strwhere.Append(" and NRequestForm.StatusNo=" + model.StatusNo + " ");
            }

            if (model.SampleTypeNo != null)
            {
                strwhere.Append(" and NRequestForm.SampleTypeNo=" + model.SampleTypeNo + " ");
            }

            if (model.PatNo != null)
            {
                strwhere.Append(" and NRequestForm.PatNo like '%" + model.PatNo + "%' ");
            }

            if (model.ClientName != null)
            {
                strwhere.Append(" and NRequestForm.ClientName='" + model.ClientName + "' ");
            }

            if (model.GenderNo != null)
            {
                strwhere.Append(" and NRequestForm.GenderNo=" + model.GenderNo + " ");
            }

            if (model.Birthday != null)
            {
                strwhere.Append(" and NRequestForm.Birthday='" + model.Birthday + "' ");
            }

            if (model.Age != null)
            {
                strwhere.Append(" and NRequestForm.Age=" + model.Age + " ");
            }

            if (model.AgeUnitNo != null)
            {
                strwhere.Append(" and NRequestForm.AgeUnitNo=" + model.AgeUnitNo + " ");
            }

            if (model.FolkNo != null)
            {
                strwhere.Append(" and NRequestForm.FolkNo=" + model.FolkNo + " ");
            }

            if (model.DistrictNo != null)
            {
                strwhere.Append(" and NRequestForm.DistrictNo=" + model.DistrictNo + " ");
            }

            if (model.WardNo != null)
            {
                strwhere.Append(" and NRequestForm.WardNo=" + model.WardNo + " ");
            }

            if (model.Bed != null)
            {
                strwhere.Append(" and NRequestForm.Bed='" + model.Bed + "' ");
            }

            if (model.DeptNo != null)
            {
                strwhere.Append(" and NRequestForm.DeptNo=" + model.DeptNo + " ");
            }

            if (model.Doctor != null)
            {
                strwhere.Append(" and NRequestForm.Doctor=" + model.Doctor + " ");
            }

            if (model.AgeUnitName != null)
            {
                strwhere.Append(" and NRequestForm.AgeUnitName='" + model.AgeUnitName + "' ");
            }

            if (model.DiagNo != null)
            {
                strwhere.Append(" and NRequestForm.DiagNo=" + model.DiagNo + " ");
            }

            if (model.Diag != null)
            {
                strwhere.Append(" and NRequestForm.Diag='" + model.Diag + "' ");
            }

            if (model.ChargeNo != null)
            {
                strwhere.Append(" and NRequestForm.ChargeNo=" + model.ChargeNo + " ");
            }

            if (model.Charge != null)
            {
                strwhere.Append(" and NRequestForm.Charge=" + model.Charge + " ");
            }

            if (model.Chargeflag != null)
            {
                strwhere.Append(" and NRequestForm.Chargeflag='" + model.Chargeflag + "' ");
            }

            if (model.CountNodesFormSource != null)
            {
                strwhere.Append(" and NRequestForm.CountNodesFormSource='" + model.CountNodesFormSource + "' ");
            }

            if (model.IsCheckFee != null)
            {
                strwhere.Append(" and NRequestForm.IsCheckFee=" + model.IsCheckFee + " ");
            }

            if (model.Operator != null)
            {
                strwhere.Append(" and NRequestForm.Operator='" + model.Operator + "' ");
            }

            if (model.OperDate != null)
            {
                strwhere.Append(" and NRequestForm.OperDate='" + model.OperDate + "' ");
            }

            if (model.OperTime != null)
            {
                strwhere.Append(" and NRequestForm.OperTime='" + model.OperTime + "' ");
            }

            if (model.GenderName != null)
            {
                strwhere.Append(" and NRequestForm.GenderName='" + model.GenderName + "' ");
            }

            if (model.FormMemo != null)
            {
                strwhere.Append(" and NRequestForm.FormMemo='" + model.FormMemo + "' ");
            }

            if (model.RequestSource != null)
            {
                strwhere.Append(" and NRequestForm.RequestSource='" + model.RequestSource + "' ");
            }

            if (model.SickOrder != null)
            {
                strwhere.Append(" and NRequestForm.SickOrder='" + model.SickOrder + "' ");
            }

            if (model.jztype != null)
            {
                strwhere.Append(" and NRequestForm.jztype=" + model.jztype + " ");
            }

            if (model.zdy1 != null)
            {
                strwhere.Append(" and NRequestForm.zdy1='" + model.zdy1 + "' ");
            }

            if (model.zdy2 != null)
            {
                strwhere.Append(" and NRequestForm.zdy2='" + model.zdy2 + "' ");
            }

            if (model.zdy3 != null)
            {
                strwhere.Append(" and NRequestForm.zdy3='" + model.zdy3 + "' ");
            }

            if (model.zdy4 != null)
            {
                strwhere.Append(" and NRequestForm.zdy4='" + model.zdy4 + "' ");
            }

            if (model.zdy5 != null)
            {
                strwhere.Append(" and NRequestForm.zdy5='" + model.zdy5 + "' ");
            }

            if (model.FlagDateDelete != null)
            {
                strwhere.Append(" and NRequestForm.FlagDateDelete='" + model.FlagDateDelete + "' ");
            }

            if (model.DeptName != null)
            {
                strwhere.Append(" and NRequestForm.DeptName='" + model.DeptName + "' ");
            }

            if (model.NurseFlag != null)
            {
                strwhere.Append(" and NRequestForm.NurseFlag=" + model.NurseFlag + " ");
            }

            if (model.IsByHand != null)
            {
                strwhere.Append(" and NRequestForm.IsByHand='" + model.IsByHand + "' ");
            }

            if (model.TestTypeNo != null)
            {
                strwhere.Append(" and NRequestForm.TestTypeNo=" + model.TestTypeNo + " ");
            }

            if (model.ExecDeptNo != null)
            {
                strwhere.Append(" and NRequestForm.ExecDeptNo=" + model.ExecDeptNo + " ");
            }

            if (model.CollectDate != null)
            {
                strwhere.Append(" and NRequestForm.CollectDate='" + model.CollectDate + "' ");
            }

            if (model.CollectTime != null)
            {
                strwhere.Append(" and NRequestForm.CollectTime='" + model.CollectTime + "' ");
            }

            if (model.Collecter != null)
            {
                strwhere.Append(" and NRequestForm.Collecter='" + model.Collecter + "' ");
            }

            if (model.LABCENTER != null)
            {
                strwhere.Append(" and NRequestForm.LABCENTER='" + model.LABCENTER + "' ");
            }

            if (model.NRequestFormNo != null)
            {
                strwhere.Append(" and NRequestForm.NRequestFormNo='" + model.NRequestFormNo + "' ");
            }

            if (model.CheckNo != null)
            {
                strwhere.Append(" and NRequestForm.CheckNo='" + model.CheckNo + "' ");
            }

            if (model.DistrictName != null)
            {
                strwhere.Append(" and NRequestForm.DistrictName='" + model.DistrictName + "' ");
            }

            if (model.CheckName != null)
            {
                strwhere.Append(" and NRequestForm.CheckName='" + model.CheckName + "' ");
            }

            if (model.WebLisSourceOrgID != null)
            {
                strwhere.Append(" and NRequestForm.WebLisSourceOrgID='" + model.WebLisSourceOrgID + "' ");
            }

            if (model.WebLisSourceOrgName != null)
            {
                strwhere.Append(" and NRequestForm.WebLisSourceOrgName='" + model.WebLisSourceOrgName + "' ");
            }

            if (model.WardName != null)
            {
                strwhere.Append(" and NRequestForm.WardName='" + model.WardName + "' ");
            }

            if (model.FolkName != null)
            {
                strwhere.Append(" and NRequestForm.FolkName='" + model.FolkName + "' ");
            }

            if (model.ClinicTypeName != null)
            {
                strwhere.Append(" and NRequestForm.ClinicTypeName='" + model.ClinicTypeName + "' ");
            }
            if (model.OperDateStart != null && model.OperDateStart != "")
            {
                strwhere.Append(" and NRequestForm.OperDate >='" + model.OperDateStart + "'  ");
            }
            if (model.OperDateEnd != null && model.OperDateEnd != "")
            {
                strwhere.Append(" and NRequestForm.OperDate <='" + model.OperDateEnd + "' ");
            }

            if (model.CollectDateStart != null)
            {
                strwhere.Append(" and NRequestForm.CollectDate >='" + model.CollectDateStart + "'  ");
            }
            if (model.CollectDateEnd != null)
            {
                strwhere.Append(" and NRequestForm.CollectDate <='" + model.CollectDateEnd + "'  ");
            }
            if (model.ClientList != null && model.ClientList.Trim() != "")
            {
                strwhere.Append(" and NRequestForm.ClientNo  in  (" + model.ClientList + ")  ");
            }
            if (model.DoctorNameList != null && model.DoctorNameList.Trim() != "")
            {
                strwhere.Append(" and NRequestForm.DoctorName  in  ('" + string.Join("','", model.DoctorNameList.Split(',')) + "')  ");
            }

            if (model.SickTypeList != null && model.SickTypeList.Trim() != "")
            {
                strwhere.Append(" and NRequestForm.jztype  in  ('" + string.Join("','", model.SickTypeList.Split(',')) + "')  ");
            }

            if (model.WeblisflagList != null && model.WeblisflagList.Trim() != "")
            {
                if (model.WeblisflagList.Split(',').ToList().Contains("0"))
                    strwhere.Append(" and (NRequestForm.Weblisflag  in  ('" + string.Join("','", model.WeblisflagList.Split(',')) + "') or NRequestForm.Weblisflag is null ) ");
                else
                    strwhere.Append(" and NRequestForm.Weblisflag  in  ('" + string.Join("','", model.WeblisflagList.Split(',')) + "') ");
            }
            #endregion
            StringBuilder strSql = new StringBuilder();
            if (model != null)
            {
                strSql.Append(" select count(*) FROM NRequestForm where 1=1  ");
                if (strwhere.ToString().Trim() != "")
                {
                    strSql.Append("   " + strwhere.ToString() + "   ");
                }
            }
            else
            {
                strSql.Append("select count(*) FROM NRequestForm where 1=1  ");
            }
            ZhiFang.Common.Log.Log.Info(strSql.ToString());
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
        public int GetNRequstFormListTotalCount2(Model.NRequestForm model)
        {
            StringBuilder strwhere = new StringBuilder();
            #region where
            if (model.ClientNo != null)
            {
                strwhere.Append(" and StatisticsNequestForm.ClientNo='" + model.ClientNo + "' ");
            }

            if (model.CName != null)
            {
                strwhere.Append(" and StatisticsNequestForm.CName like '%" + model.CName + "%' ");
            }

            if (model.BarCode != null)
            {
                strwhere.Append(" and StatisticsNequestForm.BarCode like '%" + model.BarCode + "%' ");
            }

            if (model.Weblisflag != null && int.Parse(model.Weblisflag) > 0)
            {
                strwhere.Append(" and StatisticsNequestForm.Weblisflag='" + model.Weblisflag + "' ");
            }

            if (model.DoctorName != null)
            {
                strwhere.Append(" and StatisticsNequestForm.DoctorName like '%" + model.DoctorName + "%' ");
            }

            if (model.PatNo != null)
            {
                strwhere.Append(" and StatisticsNequestForm.PatNo like '%" + model.PatNo + "%' ");
            }

            if (model.ClientName != null)
            {
                strwhere.Append(" and StatisticsNequestForm.ClientName='" + model.ClientName + "' ");
            }

            if (model.NRequestFormNo != null)
            {
                strwhere.Append(" and StatisticsNequestForm.NRequestFormNo='" + model.NRequestFormNo + "' ");
            }

            if (model.OperDateStart != null && model.OperDateStart != "")
            {
                strwhere.Append(" and StatisticsNequestForm.OperDate >='" + model.OperDateStart + "'  ");
            }
            if (model.OperDateEnd != null && model.OperDateEnd != "")
            {
                strwhere.Append(" and StatisticsNequestForm.OperDate <='" + model.OperDateEnd + "' ");
            }

            if (model.CollectDateStart != null)
            {
                strwhere.Append(" and StatisticsNequestForm.CollectDate >='" + model.CollectDateStart + "'  ");
            }
            if (model.CollectDateEnd != null)
            {
                strwhere.Append(" and StatisticsNequestForm.CollectDate <='" + model.CollectDateEnd + "'  ");
            }
            if (model.ClientList != null && model.ClientList.Trim() != "")
            {
                strwhere.Append(" and StatisticsNequestForm.ClientNo  in  (" + model.ClientList + ")  ");
            }
            #endregion
            StringBuilder strSql = new StringBuilder();
            if (model != null)
            {
                strSql.Append("select count(*) FROM StatisticsNequestForm where 1=1  ");
                if (strwhere.ToString().Trim() != "")
                {
                    strSql.Append("   " + strwhere.ToString() + " ");
                }
            }
            else
            {
                strSql.Append("select count(*) FROM StatisticsNequestForm where 1=1  ");
            }
            ZhiFang.Common.Log.Log.Info(strSql.ToString());
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

        public DataSet GetNRequstFormListByPage(Model.NRequestForm model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strwhere = new StringBuilder();
            #region where
            if (model.ClientNo != null)
            {
                strwhere.Append(" and NRequestForm.ClientNo='" + model.ClientNo + "' ");
            }

            if (model.TestTypeName != null)
            {
                strwhere.Append(" and NRequestForm.TestTypeName='" + model.TestTypeName + "' ");
            }

            if (model.CName != null)
            {
                strwhere.Append(" and NRequestForm.CName='" + model.CName + "' ");
            }

            if (model.DoctorName != null)
            {
                strwhere.Append(" and NRequestForm.DoctorName='" + model.DoctorName + "' ");
            }

            if (model.CollecterName != null)
            {
                strwhere.Append(" and NRequestForm.CollecterName='" + model.CollecterName + "' ");
            }

            if (model.SampleTypeName != null)
            {
                strwhere.Append(" and NRequestForm.SampleTypeName='" + model.SampleTypeName + "' ");
            }

            if (model.SerialNo != null)
            {
                strwhere.Append(" and NRequestForm.SerialNo='" + model.SerialNo + "' ");
            }

            if (model.ReceiveFlag != null)
            {
                strwhere.Append(" and NRequestForm.ReceiveFlag=" + model.ReceiveFlag + " ");
            }

            if (model.StatusNo != null)
            {
                strwhere.Append(" and NRequestForm.StatusNo=" + model.StatusNo + " ");
            }

            if (model.SampleTypeNo != null)
            {
                strwhere.Append(" and NRequestForm.SampleTypeNo=" + model.SampleTypeNo + " ");
            }

            if (model.PatNo != null)
            {
                strwhere.Append(" and NRequestForm.PatNo like '%" + model.PatNo + "%' ");
            }

            if (model.ClientName != null)
            {
                strwhere.Append(" and NRequestForm.ClientName='" + model.ClientName + "' ");
            }

            if (model.GenderNo != null)
            {
                strwhere.Append(" and NRequestForm.GenderNo=" + model.GenderNo + " ");
            }

            if (model.Birthday != null)
            {
                strwhere.Append(" and NRequestForm.Birthday='" + model.Birthday + "' ");
            }

            if (model.Age != null)
            {
                strwhere.Append(" and NRequestForm.Age=" + model.Age + " ");
            }

            if (model.AgeUnitNo != null)
            {
                strwhere.Append(" and NRequestForm.AgeUnitNo=" + model.AgeUnitNo + " ");
            }

            if (model.FolkNo != null)
            {
                strwhere.Append(" and NRequestForm.FolkNo=" + model.FolkNo + " ");
            }

            if (model.DistrictNo != null)
            {
                strwhere.Append(" and NRequestForm.DistrictNo=" + model.DistrictNo + " ");
            }

            if (model.WardNo != null)
            {
                strwhere.Append(" and NRequestForm.WardNo=" + model.WardNo + " ");
            }

            if (model.Bed != null)
            {
                strwhere.Append(" and NRequestForm.Bed='" + model.Bed + "' ");
            }

            if (model.DeptNo != null)
            {
                strwhere.Append(" and NRequestForm.DeptNo=" + model.DeptNo + " ");
            }

            if (model.Doctor != null)
            {
                strwhere.Append(" and NRequestForm.Doctor=" + model.Doctor + " ");
            }

            if (model.AgeUnitName != null)
            {
                strwhere.Append(" and NRequestForm.AgeUnitName='" + model.AgeUnitName + "' ");
            }

            if (model.DiagNo != null)
            {
                strwhere.Append(" and NRequestForm.DiagNo=" + model.DiagNo + " ");
            }

            if (model.Diag != null)
            {
                strwhere.Append(" and NRequestForm.Diag='" + model.Diag + "' ");
            }

            if (model.ChargeNo != null)
            {
                strwhere.Append(" and NRequestForm.ChargeNo=" + model.ChargeNo + " ");
            }

            if (model.Charge != null)
            {
                strwhere.Append(" and NRequestForm.Charge=" + model.Charge + " ");
            }

            if (model.Chargeflag != null)
            {
                strwhere.Append(" and NRequestForm.Chargeflag='" + model.Chargeflag + "' ");
            }

            if (model.CountNodesFormSource != null)
            {
                strwhere.Append(" and NRequestForm.CountNodesFormSource='" + model.CountNodesFormSource + "' ");
            }

            if (model.IsCheckFee != null)
            {
                strwhere.Append(" and NRequestForm.IsCheckFee=" + model.IsCheckFee + " ");
            }

            if (model.Operator != null)
            {
                strwhere.Append(" and NRequestForm.Operator='" + model.Operator + "' ");
            }

            if (model.OperDate != null)
            {
                strwhere.Append(" and NRequestForm.OperDate='" + model.OperDate + "' ");
            }

            if (model.OperTime != null)
            {
                strwhere.Append(" and NRequestForm.OperTime='" + model.OperTime + "' ");
            }

            if (model.GenderName != null)
            {
                strwhere.Append(" and NRequestForm.GenderName='" + model.GenderName + "' ");
            }

            if (model.FormMemo != null)
            {
                strwhere.Append(" and NRequestForm.FormMemo='" + model.FormMemo + "' ");
            }

            if (model.RequestSource != null)
            {
                strwhere.Append(" and NRequestForm.RequestSource='" + model.RequestSource + "' ");
            }

            if (model.SickOrder != null)
            {
                strwhere.Append(" and NRequestForm.SickOrder='" + model.SickOrder + "' ");
            }

            if (model.jztype != null)
            {
                strwhere.Append(" and NRequestForm.jztype=" + model.jztype + " ");
            }

            if (model.zdy1 != null)
            {
                strwhere.Append(" and NRequestForm.zdy1='" + model.zdy1 + "' ");
            }

            if (model.zdy2 != null)
            {
                strwhere.Append(" and NRequestForm.zdy2='" + model.zdy2 + "' ");
            }

            if (model.zdy3 != null)
            {
                strwhere.Append(" and NRequestForm.zdy3='" + model.zdy3 + "' ");
            }

            if (model.zdy4 != null)
            {
                strwhere.Append(" and NRequestForm.zdy4='" + model.zdy4 + "' ");
            }

            if (model.zdy5 != null)
            {
                strwhere.Append(" and NRequestForm.zdy5='" + model.zdy5 + "' ");
            }

            if (model.FlagDateDelete != null)
            {
                strwhere.Append(" and NRequestForm.FlagDateDelete='" + model.FlagDateDelete + "' ");
            }

            if (model.DeptName != null)
            {
                strwhere.Append(" and NRequestForm.DeptName='" + model.DeptName + "' ");
            }

            if (model.NurseFlag != null)
            {
                strwhere.Append(" and NRequestForm.NurseFlag=" + model.NurseFlag + " ");
            }

            if (model.IsByHand != null)
            {
                strwhere.Append(" and NRequestForm.IsByHand='" + model.IsByHand + "' ");
            }

            if (model.TestTypeNo != null)
            {
                strwhere.Append(" and NRequestForm.TestTypeNo=" + model.TestTypeNo + " ");
            }

            if (model.ExecDeptNo != null)
            {
                strwhere.Append(" and NRequestForm.ExecDeptNo=" + model.ExecDeptNo + " ");
            }

            if (model.CollectDate != null)
            {
                strwhere.Append(" and NRequestForm.CollectDate='" + model.CollectDate + "' ");
            }

            if (model.CollectTime != null)
            {
                strwhere.Append(" and NRequestForm.CollectTime='" + model.CollectTime + "' ");
            }

            if (model.Collecter != null)
            {
                strwhere.Append(" and NRequestForm.Collecter='" + model.Collecter + "' ");
            }

            if (model.LABCENTER != null)
            {
                strwhere.Append(" and NRequestForm.LABCENTER='" + model.LABCENTER + "' ");
            }

            if (model.NRequestFormNo != null)
            {
                strwhere.Append(" and NRequestForm.NRequestFormNo='" + model.NRequestFormNo + "' ");
            }

            if (model.CheckNo != null)
            {
                strwhere.Append(" and NRequestForm.CheckNo='" + model.CheckNo + "' ");
            }

            if (model.DistrictName != null)
            {
                strwhere.Append(" and NRequestForm.DistrictName='" + model.DistrictName + "' ");
            }

            if (model.CheckName != null)
            {
                strwhere.Append(" and NRequestForm.CheckName='" + model.CheckName + "' ");
            }

            if (model.WebLisSourceOrgID != null)
            {
                strwhere.Append(" and NRequestForm.WebLisSourceOrgID='" + model.WebLisSourceOrgID + "' ");
            }

            if (model.WebLisSourceOrgName != null)
            {
                strwhere.Append(" and NRequestForm.WebLisSourceOrgName='" + model.WebLisSourceOrgName + "' ");
            }

            if (model.WardName != null)
            {
                strwhere.Append(" and NRequestForm.WardName='" + model.WardName + "' ");
            }

            if (model.FolkName != null)
            {
                strwhere.Append(" and NRequestForm.FolkName='" + model.FolkName + "' ");
            }

            if (model.ClinicTypeName != null)
            {
                strwhere.Append(" and NRequestForm.ClinicTypeName='" + model.ClinicTypeName + "' ");
            }
            if (model.OperDateStart != null && model.OperDateStart != "")
            {
                strwhere.Append(" and NRequestForm.OperDate >='" + model.OperDateStart + "'  ");
            }
            if (model.OperDateEnd != null && model.OperDateEnd != "")
            {
                strwhere.Append(" and NRequestForm.OperDate <='" + model.OperDateEnd + "' ");
            }

            if (model.CollectDateStart != null)
            {
                strwhere.Append(" and NRequestForm.CollectDate >='" + model.CollectDateStart + "'  ");
            }
            if (model.CollectDateEnd != null)
            {
                strwhere.Append(" and NRequestForm.CollectDate <='" + model.CollectDateEnd + "'  ");
            }
            if (model.ClientList != null && model.ClientList.Trim() != "")
            {
                strwhere.Append(" and NRequestForm.ClientNo  in  (" + model.ClientList + ")  ");
            }

            if (model.DoctorNameList != null && model.DoctorNameList.Trim() != "")
            {
                strwhere.Append(" and NRequestForm.DoctorName  in  ('" + string.Join("','", model.DoctorNameList.Split(',')) + "')  ");
            }

            if (model.SickTypeList != null && model.SickTypeList.Trim() != "")
            {
                strwhere.Append(" and NRequestForm.jztype  in  ('" + string.Join("','", model.SickTypeList.Split(',')) + "')  ");
            }
            if (model.WeblisflagList != null && model.WeblisflagList.Trim() != "")
            {
                if (model.WeblisflagList.Split(',').ToList().Contains("0"))
                    strwhere.Append(" and (NRequestForm.Weblisflag  in  ('" + string.Join("','", model.WeblisflagList.Split(',')) + "') or NRequestForm.Weblisflag is null ) ");
                else
                    strwhere.Append(" and NRequestForm.Weblisflag  in  ('" + string.Join("','", model.WeblisflagList.Split(',')) + "') ");
            }
            if (model.PersonID != null && model.PersonID.Trim() != "")
            {
                strwhere.Append(" and NRequestForm.PersonID  =  '" + model.PersonID + "'  ");
            }
            if (model.BarCode != null && model.BarCode.Trim() != "")
            {
                strwhere.Append(" and NRequestForm.BarCode =  '" + model.BarCode + "'   ");
            }
            if (model.NRFNOlist != null && model.NRFNOlist.Count>0)
            {
                strwhere.Append(" and NRequestForm.NRequestFormNo in(" + string.Join(",", model.NRFNOlist) + ")");
            }
            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("NRequestFormListOtherSQL") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("NRequestFormListOtherSQL").Trim() != "")
            {
                strwhere.Append(" and " + Common.Public.ConfigHelper.GetConfigString("NRequestFormListOtherSQL"));
            }
            #endregion
            StringBuilder strSql = new StringBuilder();
            if (model != null)
            {
                strSql.Append(" select * from ( SELECT top " + nowPageSize + " NRequestForm.NRequestFormNo, " +
                        "NRequestForm.CName as CName," +
                        "NRequestForm.DoctorName as DoctorName," +
                        "NRequestForm.DeptName as DeptName," +
                        "NRequestForm.WebLisSourceOrgID," +
                        "NRequestForm.WebLisSourceOrgName as WebLisSourceOrgName," +
                        "NRequestForm.ClientNo," +
                        "NRequestForm.jztype," +
                        "NRequestForm.jztypeName," +
                        "NRequestForm.TestTypeNo," +
                        "NRequestForm.TestTypeName," +
                        "NRequestForm.PatNo," +
                        "NRequestForm.ClientName as ClientName," +
                        "NRequestForm.AgeUnitName as AgeUnitName," +
                        "NRequestForm.Diag as Diag,GenderType.CName AS Sex," +
                        "NRequestForm.Age as Age, " +
                        "NRequestForm.LABUPLOADDATE as LABUPLOADDATE, " +
                        "NRequestForm.PersonID as PersonID, " +
                          "NRequestForm.WebLisFlag as WebLisFlag, " +
                            "NRequestForm.RejectionReason as RejectionReason, " +
                        "SampleType.CName AS SampleName," +
                        "Convert(varchar(10),NRequestForm.operdate,21)+' '+Convert(varchar(10),NRequestForm.opertime,8)as incepttime," +
                        "Convert(varchar(10),NRequestForm.CollectDate,21)+' '+Convert(varchar(10),NRequestForm.CollectTime,8)as CollectTime," +
                        "Convert(varchar(10),NRequestForm.OperDate,21)+' '+Convert(varchar(10),NRequestForm.OperDate,8)as  OperDate," +
                        "Convert(varchar(10),NRequestForm.OperTime,21)+' '+Convert(varchar(10),NRequestForm.OperTime,8)as  OperTime," +
                        "PatDiagInfo.DiagDesc as DiagDesc," +
                        "NRequestForm.zdy5 " +
                        "FROM dbo.GenderType RIGHT OUTER JOIN " +
                        "dbo.NRequestForm LEFT OUTER JOIN " +
                        "dbo.PatDiagInfo ON dbo.NRequestForm.DiagNo = dbo.PatDiagInfo.DiagNo LEFT OUTER JOIN " +
                        "dbo.SampleType ON dbo.NRequestForm.SampleTypeNo = dbo.SampleType.SampleTypeNo ON dbo.GenderType.GenderNo = dbo.NRequestForm.GenderNo");

                strSql.Append(" where NRequestForm.NRequestFormNo not in (  ");
                strSql.Append("select top " + (nowPageSize * nowPageNum) + " NRequestForm.NRequestFormNo FROM dbo.GenderType RIGHT OUTER JOIN " +
                        "dbo.NRequestForm LEFT OUTER JOIN " +
                        "dbo.PatDiagInfo ON dbo.NRequestForm.DiagNo = dbo.PatDiagInfo.DiagNo LEFT OUTER JOIN " +
                        "dbo.SampleType ON dbo.NRequestForm.SampleTypeNo = dbo.SampleType.SampleTypeNo ON dbo.GenderType.GenderNo = dbo.NRequestForm.GenderNo where ");
                if (strwhere.ToString().Trim() == "")
                {
                    strSql.Append(" 1=2");
                }
                else
                {
                    strSql.Append(" 1=1 " + strwhere.ToString() + "   ");
                }

                strSql.Append("order by NRequestForm.OperDate desc,NRequestForm.OperTime desc ) ");
                if (strwhere.ToString().Trim() == "")
                {
                    strSql.Append(" and 1=2");
                }
                else
                {
                    strSql.Append("   " + strwhere.ToString() + "   ");
                }
                strSql.Append(" order by NRequestForm.OperDate desc,NRequestForm.OperTime desc ) aa order by aa.OperDate asc,aa.OperTime asc ");
                ZhiFang.Common.Log.Log.Info(strSql.ToString());
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
            else
            {
                strSql.Append("  select * from ( SELECT distinct top " + nowPageSize + "  NRequestForm.NRequestFormNo, " +
                        "NRequestForm.CName as CName," +
                        "NRequestForm.DoctorName as DoctorName," +
                        "NRequestForm.WebLisSourceOrgID," +
                        "NRequestForm.WebLisSourceOrgName as WebLisSourceOrgName," +
                        "NRequestForm.ClientNo," +
                        "NRequestForm.ClientName as ClientName," +
                        "NRequestForm.AgeUnitName as AgeUnitName," +
                        "NRequestForm.Diag as Diag,GenderType.CName AS Sex," +
                        "NRequestForm.Age as Age, " +
                        "SampleType.CName AS SampleName," +
                         "NRequestForm.jztype," +
                        "NRequestForm.jztypeName," +
                        "NRequestForm.TestTypeNo," +
                        "NRequestForm.TestTypeName," +
                        "Convert(varchar(10),NRequestForm.operdate,21)+' '+Convert(varchar(10),NRequestForm.opertime,8)as incepttime," +
                        "Convert(varchar(10),NRequestForm.CollectDate,21)+' '+Convert(varchar(10),NRequestForm.CollectTime,8)as CollectTime," +
                        "NRequestForm.OperDate as OperDate," +
                        "NRequestForm.OperTime as OperTime," +
                        "NRequestForm.LABUPLOADDATE as LABUPLOADDATE, " +
                        "PatDiagInfo.DiagDesc as DiagDesc," +
                        "NRequestForm.zdy5 " +
                        "FROM  dbo.GenderType RIGHT OUTER JOIN " +
                        "dbo.NRequestForm LEFT OUTER JOIN " +
                        "dbo.PatDiagInfo ON dbo.NRequestForm.DiagNo = dbo.PatDiagInfo.DiagNo LEFT OUTER JOIN " +
                        "dbo.SampleType ON dbo.NRequestForm.SampleTypeNo = dbo.SampleType.SampleTypeNo ON dbo.GenderType.GenderNo = dbo.NRequestForm.GenderNo ");

                strSql.Append(" where NRequestForm.NRequestFormNo not in (  ");
                strSql.Append("select  top " + (nowPageSize * nowPageNum) + " NRequestForm.NRequestFormNo from  dbo.GenderType RIGHT OUTER JOIN " +
                        "dbo.NRequestForm LEFT OUTER JOIN " +
                        "dbo.PatDiagInfo ON dbo.NRequestForm.DiagNo = dbo.PatDiagInfo.DiagNo LEFT OUTER JOIN " +
                        "dbo.SampleType ON dbo.NRequestForm.SampleTypeNo = dbo.SampleType.SampleTypeNo ON dbo.GenderType.GenderNo = dbo.NRequestForm.GenderNo ");

                strSql.Append("order by NRequestForm.OperDate desc,NRequestForm.OperTime desc ) order by NRequestForm.OperDate desc,NRequestForm.OperTime desc  ) aa order by aa.OperDate asc,aa.OperTime asc ");
                ZhiFang.Common.Log.Log.Info(strSql.ToString());
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
        }

        public DataSet GetNRequstFormListByPage2(Model.NRequestForm model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strwhere = new StringBuilder();
            #region where
            if (model.ClientNo != null)
            {
                strwhere.Append(" and StatisticsNequestForm.ClientNo='" + model.ClientNo + "' ");
            }

            if (model.CName != null)
            {
                strwhere.Append(" and StatisticsNequestForm.CName like '%" + model.CName + "%' ");
            }

            if (model.BarCode != null)
            {
                strwhere.Append(" and StatisticsNequestForm.BarCode like '%" + model.BarCode + "%' ");
            }

            if (model.Weblisflag != null && int.Parse(model.Weblisflag) > 0)
            {
                strwhere.Append(" and StatisticsNequestForm.Weblisflag='" + model.Weblisflag + "' ");
            }

            if (model.DoctorName != null)
            {
                strwhere.Append(" and StatisticsNequestForm.DoctorName like '%" + model.DoctorName + "%' ");
            }

            if (model.PatNo != null)
            {
                strwhere.Append(" and StatisticsNequestForm.PatNo like '%" + model.PatNo + "%' ");
            }

            if (model.ClientName != null)
            {
                strwhere.Append(" and StatisticsNequestForm.ClientName='" + model.ClientName + "' ");
            }

            if (model.NRequestFormNo != null)
            {
                strwhere.Append(" and StatisticsNequestForm.NRequestFormNo='" + model.NRequestFormNo + "' ");
            }

            if (model.OperDateStart != null && model.OperDateStart != "")
            {
                strwhere.Append(" and StatisticsNequestForm.OperDate >='" + model.OperDateStart + "'  ");
            }
            if (model.OperDateEnd != null && model.OperDateEnd != "")
            {
                strwhere.Append(" and StatisticsNequestForm.OperDate <='" + model.OperDateEnd + "' ");
            }

            if (model.CollectDateStart != null)
            {
                strwhere.Append(" and StatisticsNequestForm.CollectDate >='" + model.CollectDateStart + "'  ");
            }
            if (model.CollectDateEnd != null)
            {
                strwhere.Append(" and StatisticsNequestForm.CollectDate <='" + model.CollectDateEnd + "'  ");
            }
            if (model.ClientList != null && model.ClientList.Trim() != "")
            {
                strwhere.Append(" and StatisticsNequestForm.ClientNo  in  (" + model.ClientList + ")  ");
            }
            #endregion
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" select * from ( SELECT top " + nowPageSize + " StatisticsNequestForm.NRequestFormNo, " +
                    "StatisticsNequestForm.BarCode," +
                    "StatisticsNequestForm.CName," +
                    "StatisticsNequestForm.Sex," +
                    "StatisticsNequestForm.Age," +
                    "StatisticsNequestForm.SampleTypeName," +
                    "StatisticsNequestForm.ItemName," +
                    "StatisticsNequestForm.DoctorName," +
                    "StatisticsNequestForm.OperDate," +
                    "StatisticsNequestForm.CollectDate," +
                    "StatisticsNequestForm.ClientNo," +
                    "StatisticsNequestForm.PatNo," +
                    "StatisticsNequestForm.WebLisFlag," +
                    "StatisticsNequestForm.ClientName from StatisticsNequestForm ");
            strSql.Append(" where StatisticsNequestForm.NRequestFormNo not in (  ");
            strSql.Append("select top " + (nowPageSize * nowPageNum) + " StatisticsNequestForm.NRequestFormNo FROM StatisticsNequestForm ");
            if (strwhere.ToString().Trim() == "")
            {
                strSql.Append("where 1=2");
            }
            else
            {
                strSql.Append("where 1=1 " + strwhere.ToString() + "   ");
            }
            strSql.Append("order by StatisticsNequestForm.OperDate desc ) ");
            if (strwhere.ToString().Trim() == "")
            {
                strSql.Append(" and 1=2");
            }
            else
            {
                strSql.Append("   " + strwhere.ToString() + "   ");
            }
            strSql.Append(" order by StatisticsNequestForm.OperDate desc ) aa order by aa.OperDate desc ");
            ZhiFang.Common.Log.Log.Info(strSql.ToString());
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetNRequstFormListByPage_CombiItemNo(Model.NRequestForm model, int startPage, int pageSize)
        {
            StringBuilder strwhere = new StringBuilder();
            #region where
            if (model.ClientNo != null)
            {
                strwhere.Append(" and NRequestForm.ClientNo='" + model.ClientNo + "' ");
            }

            if (model.TestTypeName != null)
            {
                strwhere.Append(" and NRequestForm.TestTypeName='" + model.TestTypeName + "' ");
            }

            if (model.CName != null)
            {
                strwhere.Append(" and NRequestForm.CName='" + model.CName + "' ");
            }

            if (model.DoctorName != null)
            {
                strwhere.Append(" and NRequestForm.DoctorName='" + model.DoctorName + "' ");
            }

            if (model.CollecterName != null)
            {
                strwhere.Append(" and NRequestForm.CollecterName='" + model.CollecterName + "' ");
            }

            if (model.SampleTypeName != null)
            {
                strwhere.Append(" and NRequestForm.SampleTypeName='" + model.SampleTypeName + "' ");
            }

            if (model.SerialNo != null)
            {
                strwhere.Append(" and NRequestForm.SerialNo='" + model.SerialNo + "' ");
            }

            if (model.ReceiveFlag != null)
            {
                strwhere.Append(" and NRequestForm.ReceiveFlag=" + model.ReceiveFlag + " ");
            }

            if (model.StatusNo != null)
            {
                strwhere.Append(" and NRequestForm.StatusNo=" + model.StatusNo + " ");
            }

            if (model.SampleTypeNo != null)
            {
                strwhere.Append(" and NRequestForm.SampleTypeNo=" + model.SampleTypeNo + " ");
            }

            if (model.PatNo != null)
            {
                strwhere.Append(" and NRequestForm.PatNo like '%" + model.PatNo + "%' ");
            }

            if (model.ClientName != null)
            {
                strwhere.Append(" and NRequestForm.ClientName='" + model.ClientName + "' ");
            }

            if (model.GenderNo != null)
            {
                strwhere.Append(" and NRequestForm.GenderNo=" + model.GenderNo + " ");
            }

            if (model.Birthday != null)
            {
                strwhere.Append(" and NRequestForm.Birthday='" + model.Birthday + "' ");
            }

            if (model.Age != null)
            {
                strwhere.Append(" and NRequestForm.Age=" + model.Age + " ");
            }

            if (model.AgeUnitNo != null)
            {
                strwhere.Append(" and NRequestForm.AgeUnitNo=" + model.AgeUnitNo + " ");
            }

            if (model.FolkNo != null)
            {
                strwhere.Append(" and NRequestForm.FolkNo=" + model.FolkNo + " ");
            }

            if (model.DistrictNo != null)
            {
                strwhere.Append(" and NRequestForm.DistrictNo=" + model.DistrictNo + " ");
            }

            if (model.WardNo != null)
            {
                strwhere.Append(" and NRequestForm.WardNo=" + model.WardNo + " ");
            }

            if (model.Bed != null)
            {
                strwhere.Append(" and NRequestForm.Bed='" + model.Bed + "' ");
            }

            if (model.DeptNo != null)
            {
                strwhere.Append(" and NRequestForm.DeptNo=" + model.DeptNo + " ");
            }

            if (model.Doctor != null)
            {
                strwhere.Append(" and NRequestForm.Doctor=" + model.Doctor + " ");
            }

            if (model.AgeUnitName != null)
            {
                strwhere.Append(" and NRequestForm.AgeUnitName='" + model.AgeUnitName + "' ");
            }

            if (model.DiagNo != null)
            {
                strwhere.Append(" and NRequestForm.DiagNo=" + model.DiagNo + " ");
            }

            if (model.Diag != null)
            {
                strwhere.Append(" and NRequestForm.Diag='" + model.Diag + "' ");
            }

            if (model.ChargeNo != null)
            {
                strwhere.Append(" and NRequestForm.ChargeNo=" + model.ChargeNo + " ");
            }

            if (model.Charge != null)
            {
                strwhere.Append(" and NRequestForm.Charge=" + model.Charge + " ");
            }

            if (model.Chargeflag != null)
            {
                strwhere.Append(" and NRequestForm.Chargeflag='" + model.Chargeflag + "' ");
            }

            if (model.CountNodesFormSource != null)
            {
                strwhere.Append(" and NRequestForm.CountNodesFormSource='" + model.CountNodesFormSource + "' ");
            }

            if (model.IsCheckFee != null)
            {
                strwhere.Append(" and NRequestForm.IsCheckFee=" + model.IsCheckFee + " ");
            }

            if (model.Operator != null)
            {
                strwhere.Append(" and NRequestForm.Operator='" + model.Operator + "' ");
            }

            if (model.OperDate != null)
            {
                strwhere.Append(" and NRequestForm.OperDate='" + model.OperDate + "' ");
            }

            if (model.OperTime != null)
            {
                strwhere.Append(" and NRequestForm.OperTime='" + model.OperTime + "' ");
            }

            if (model.GenderName != null)
            {
                strwhere.Append(" and NRequestForm.GenderName='" + model.GenderName + "' ");
            }

            if (model.FormMemo != null)
            {
                strwhere.Append(" and NRequestForm.FormMemo='" + model.FormMemo + "' ");
            }

            if (model.RequestSource != null)
            {
                strwhere.Append(" and NRequestForm.RequestSource='" + model.RequestSource + "' ");
            }

            if (model.SickOrder != null)
            {
                strwhere.Append(" and NRequestForm.SickOrder='" + model.SickOrder + "' ");
            }

            if (model.jztype != null)
            {
                strwhere.Append(" and NRequestForm.jztype=" + model.jztype + " ");
            }

            if (model.zdy1 != null)
            {
                strwhere.Append(" and NRequestForm.zdy1='" + model.zdy1 + "' ");
            }

            if (model.zdy2 != null)
            {
                strwhere.Append(" and NRequestForm.zdy2='" + model.zdy2 + "' ");
            }

            if (model.zdy3 != null)
            {
                strwhere.Append(" and NRequestForm.zdy3='" + model.zdy3 + "' ");
            }

            if (model.zdy4 != null)
            {
                strwhere.Append(" and NRequestForm.zdy4='" + model.zdy4 + "' ");
            }

            if (model.zdy5 != null)
            {
                strwhere.Append(" and NRequestForm.zdy5='" + model.zdy5 + "' ");
            }

            if (model.FlagDateDelete != null)
            {
                strwhere.Append(" and NRequestForm.FlagDateDelete='" + model.FlagDateDelete + "' ");
            }

            if (model.DeptName != null)
            {
                strwhere.Append(" and NRequestForm.DeptName='" + model.DeptName + "' ");
            }

            if (model.NurseFlag != null)
            {
                strwhere.Append(" and NRequestForm.NurseFlag=" + model.NurseFlag + " ");
            }

            if (model.IsByHand != null)
            {
                strwhere.Append(" and NRequestForm.IsByHand='" + model.IsByHand + "' ");
            }

            if (model.TestTypeNo != null)
            {
                strwhere.Append(" and NRequestForm.TestTypeNo=" + model.TestTypeNo + " ");
            }

            if (model.ExecDeptNo != null)
            {
                strwhere.Append(" and NRequestForm.ExecDeptNo=" + model.ExecDeptNo + " ");
            }

            if (model.CollectDate != null)
            {
                strwhere.Append(" and NRequestForm.CollectDate='" + model.CollectDate + "' ");
            }

            if (model.CollectTime != null)
            {
                strwhere.Append(" and NRequestForm.CollectTime='" + model.CollectTime + "' ");
            }

            if (model.Collecter != null)
            {
                strwhere.Append(" and NRequestForm.Collecter='" + model.Collecter + "' ");
            }

            if (model.LABCENTER != null)
            {
                strwhere.Append(" and NRequestForm.LABCENTER='" + model.LABCENTER + "' ");
            }

            if (model.NRequestFormNo != null)
            {
                strwhere.Append(" and NRequestForm.NRequestFormNo='" + model.NRequestFormNo + "' ");
            }

            if (model.CheckNo != null)
            {
                strwhere.Append(" and NRequestForm.CheckNo='" + model.CheckNo + "' ");
            }

            if (model.DistrictName != null)
            {
                strwhere.Append(" and NRequestForm.DistrictName='" + model.DistrictName + "' ");
            }

            if (model.CheckName != null)
            {
                strwhere.Append(" and NRequestForm.CheckName='" + model.CheckName + "' ");
            }

            if (model.WebLisSourceOrgID != null)
            {
                strwhere.Append(" and NRequestForm.WebLisSourceOrgID='" + model.WebLisSourceOrgID + "' ");
            }

            if (model.WebLisSourceOrgName != null)
            {
                strwhere.Append(" and NRequestForm.WebLisSourceOrgName='" + model.WebLisSourceOrgName + "' ");
            }

            if (model.WardName != null)
            {
                strwhere.Append(" and NRequestForm.WardName='" + model.WardName + "' ");
            }

            if (model.FolkName != null)
            {
                strwhere.Append(" and NRequestForm.FolkName='" + model.FolkName + "' ");
            }

            if (model.ClinicTypeName != null)
            {
                strwhere.Append(" and NRequestForm.ClinicTypeName='" + model.ClinicTypeName + "' ");
            }
            if (model.OperDateStart != null && model.OperDateStart != "")
            {
                strwhere.Append(" and NRequestForm.OperDate >='" + model.OperDateStart + "'  ");
            }
            if (model.OperDateEnd != null && model.OperDateEnd != "")
            {
                strwhere.Append(" and NRequestForm.OperDate <='" + model.OperDateEnd + "' ");
            }

            if (model.CollectDateStart != null)
            {
                strwhere.Append(" and NRequestForm.CollectDate >='" + model.CollectDateStart + "'  ");
            }
            if (model.CollectDateEnd != null)
            {
                strwhere.Append(" and NRequestForm.CollectDate <='" + model.CollectDateEnd + "'  ");
            }
            if (model.ClientList != null && model.ClientList.Trim() != "")
            {
                strwhere.Append(" and NRequestForm.ClientNo  in  (" + model.ClientList + ")  ");
            }

            if (model.DoctorNameList != null && model.DoctorNameList.Trim() != "")
            {
                strwhere.Append(" and NRequestForm.DoctorName  in  ('" + string.Join("','", model.DoctorNameList.Split(',')) + "')  ");
            }

            if (model.SickTypeList != null && model.SickTypeList.Trim() != "")
            {
                strwhere.Append(" and NRequestForm.jztype  in  ('" + string.Join("','", model.SickTypeList.Split(',')) + "')  ");
            }
            if (model.WeblisflagList != null && model.WeblisflagList.Trim() != "")
            {
                if (model.WeblisflagList.Split(',').ToList().Contains("0"))
                    strwhere.Append(" and (NRequestForm.Weblisflag  in  ('" + string.Join("','", model.WeblisflagList.Split(',')) + "') or NRequestForm.Weblisflag is null ) ");
                else
                    strwhere.Append(" and NRequestForm.Weblisflag  in  ('" + string.Join("','", model.WeblisflagList.Split(',')) + "') ");
            }
            if (model.PersonID != null && model.PersonID.Trim() != "")
            {
                strwhere.Append(" and NRequestForm.PersonID  =  '" + model.PersonID + "'  ");
            }
            if (model.BarCode != null && model.BarCode.Trim() != "")
            {
                strwhere.Append(" and NRequestForm.BarCode =  '" + model.BarCode + "'   ");
            }
            if (model.CombiItemNo != null && model.CombiItemNo.Trim() != "")
            {
                strwhere.Append(" and (select COUNT(*) FROM NRequestItem where 1=1      and CombiItemNo='" + model.CombiItemNo + "'  and NRequestForm.NRequestFormNo =NRequestFormNo )>0");
            }
            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("NRequestFormListOtherSQL") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("NRequestFormListOtherSQL").Trim() != "")
            {
                strwhere.Append(" and " + Common.Public.ConfigHelper.GetConfigString("NRequestFormListOtherSQL"));
            }

            #endregion
            StringBuilder strSql = new StringBuilder();
            if (model != null)
            {
                string fields = " NRequestForm.NRequestFormNo, " +
                        "NRequestForm.CName as CName," +
                        "NRequestForm.DoctorName as DoctorName," +
                        "NRequestForm.DeptName as DeptName," +
                        "NRequestForm.WebLisSourceOrgID," +
                        "NRequestForm.WebLisSourceOrgName as WebLisSourceOrgName," +
                        "NRequestForm.ClientNo," +
                        "NRequestForm.jztype," +
                        "NRequestForm.jztypeName," +
                        "NRequestForm.PatNo," +
                        "NRequestForm.ClientName as ClientName," +
                        "NRequestForm.AgeUnitName as AgeUnitName," +
                        "NRequestForm.Diag as Diag,GenderType.CName AS Sex," +
                        "NRequestForm.Age as Age, " +
                        "NRequestForm.WebLisFlag as WebLisFlag, " +
                        "NRequestForm.LABUPLOADDATE as LABUPLOADDATE, " +
                        "SampleType.CName AS SampleName," +
                        "Convert(varchar(10),NRequestForm.operdate,21)+' '+Convert(varchar(10),NRequestForm.opertime,8)as incepttime," +
                        "Convert(varchar(10),NRequestForm.CollectDate,21)+' '+Convert(varchar(10),NRequestForm.CollectTime,8)as CollectTime," +
                        "Convert(varchar(10),NRequestForm.OperDate,21)+' '+Convert(varchar(10),NRequestForm.OperDate,8)as  OperDate," +
                        "Convert(varchar(10),NRequestForm.OperTime,21)+' '+Convert(varchar(10),NRequestForm.OperTime,8)as  OperTime," +
                        "PatDiagInfo.DiagDesc as DiagDesc," +
                        "NRequestForm.PersonID ," +
                        "NRequestForm.Charge ," +
                        "NRequestForm.TestTypeName ," +
                         "Convert(varchar(10),NRequestForm.Receivedate,21) as  Receivedate," +
                        "NRequestForm.zdy1 ," +
                        "NRequestForm.zdy2 ," +
                        "NRequestForm.zdy3 ," +
                        "NRequestForm.zdy4 ," +
                        "NRequestForm.zdy5 ," +
                        "NRequestForm.zdy6 ," +
                        "NRequestForm.zdy7 ," +
                        "NRequestForm.zdy8 ," +
                        "NRequestForm.zdy9 ," +
                        "NRequestForm.zdy10 ";
                string from = "FROM dbo.GenderType RIGHT OUTER JOIN " +
                        "dbo.NRequestForm LEFT OUTER JOIN " +
                        "dbo.PatDiagInfo ON dbo.NRequestForm.DiagNo = dbo.PatDiagInfo.DiagNo LEFT OUTER JOIN " +
                        "dbo.SampleType ON dbo.NRequestForm.SampleTypeNo = dbo.SampleType.SampleTypeNo ON dbo.GenderType.GenderNo = dbo.NRequestForm.GenderNo ";

                string tmpwhere = (strwhere.ToString().Trim() == "") ? " 1=2 " : " 1=1 " + strwhere.ToString() + " ";

                string NRequestFormNolist = " select top " + (pageSize * startPage) + " NRequestForm.NRequestFormNo FROM dbo.GenderType RIGHT OUTER JOIN  dbo.NRequestForm LEFT OUTER JOIN  dbo.PatDiagInfo ON dbo.NRequestForm.DiagNo = dbo.PatDiagInfo.DiagNo LEFT OUTER JOIN  dbo.SampleType ON dbo.NRequestForm.SampleTypeNo = dbo.SampleType.SampleTypeNo ON dbo.GenderType.GenderNo = dbo.NRequestForm.GenderNo where " + tmpwhere + " order by NRequestForm.OperDate desc, NRequestForm.OperTime desc ";

                string where = " where " + tmpwhere + " and NRequestForm.NRequestFormNo not in (" + NRequestFormNolist + ")  order by NRequestForm.OperDate desc,NRequestForm.OperTime desc";


                strSql.Append(" select * from ( SELECT top " + pageSize + fields + from + where + " ) aa order by aa.OperDate asc,aa.OperTime asc ");

                ZhiFang.Common.Log.Log.Info(strSql.ToString());
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
            else
            {
                strSql.Append("  select * from ( SELECT distinct top " + pageSize + "  NRequestForm.NRequestFormNo, " +
                        "NRequestForm.CName as CName," +
                        "NRequestForm.DoctorName as DoctorName," +
                        "NRequestForm.WebLisSourceOrgID," +
                        "NRequestForm.WebLisSourceOrgName as WebLisSourceOrgName," +
                        "NRequestForm.ClientNo," +
                        "NRequestForm.ClientName as ClientName," +
                        "NRequestForm.AgeUnitName as AgeUnitName," +
                        "NRequestForm.Diag as Diag,GenderType.CName AS Sex," +
                        "NRequestForm.Age as Age, " +
                        "SampleType.CName AS SampleName," +
                        "Convert(varchar(10),NRequestForm.operdate,21)+' '+Convert(varchar(10),NRequestForm.opertime,8)as incepttime," +
                        "Convert(varchar(10),NRequestForm.CollectDate,21)+' '+Convert(varchar(10),NRequestForm.CollectTime,8)as CollectTime," +
                        "NRequestForm.OperDate as OperDate," +
                        "NRequestForm.OperTime as OperTime," +
                        "NRequestForm.LABUPLOADDATE as LABUPLOADDATE, " +
                        "PatDiagInfo.DiagDesc as DiagDesc," +
                        "NRequestForm.zdy1 ," +
                        "NRequestForm.zdy2 ," +
                        "NRequestForm.zdy3 ," +
                        "NRequestForm.zdy4 ," +
                        "NRequestForm.zdy5 ," +
                        "NRequestForm.zdy6 ," +
                        "NRequestForm.zdy7 ," +
                        "NRequestForm.zdy8 ," +
                        "NRequestForm.zdy9 ," +
                        "NRequestForm.zdy10 " +
                        "FROM  dbo.GenderType RIGHT OUTER JOIN " +
                        "dbo.NRequestForm LEFT OUTER JOIN " +
                        "dbo.PatDiagInfo ON dbo.NRequestForm.DiagNo = dbo.PatDiagInfo.DiagNo LEFT OUTER JOIN " +
                        "dbo.SampleType ON dbo.NRequestForm.SampleTypeNo = dbo.SampleType.SampleTypeNo ON dbo.GenderType.GenderNo = dbo.NRequestForm.GenderNo ");

                strSql.Append(" where NRequestForm.NRequestFormNo not in (  ");
                strSql.Append("select  top " + (pageSize * startPage) + " NRequestForm.NRequestFormNo from  dbo.GenderType RIGHT OUTER JOIN " +
                        "dbo.NRequestForm LEFT OUTER JOIN " +
                        "dbo.PatDiagInfo ON dbo.NRequestForm.DiagNo = dbo.PatDiagInfo.DiagNo LEFT OUTER JOIN " +
                        "dbo.SampleType ON dbo.NRequestForm.SampleTypeNo = dbo.SampleType.SampleTypeNo ON dbo.GenderType.GenderNo = dbo.NRequestForm.GenderNo ");

                strSql.Append("order by NRequestForm.OperDate desc,NRequestForm.OperTime desc ) order by NRequestForm.OperDate desc,NRequestForm.OperTime desc  ) aa order by aa.OperDate asc,aa.OperTime asc ");
                ZhiFang.Common.Log.Log.Info(strSql.ToString());
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
        }

        public DataSet GetNRequstFormList_SampleSendNo(Model.NRequestForm model, int startPage, int pageSize)
        {
            StringBuilder strwhere = new StringBuilder();
            #region where
            if (model.ClientNo != null)
            {
                strwhere.Append(" and NRequestForm.ClientNo='" + model.ClientNo + "' ");
            }

            if (model.TestTypeName != null)
            {
                strwhere.Append(" and NRequestForm.TestTypeName='" + model.TestTypeName + "' ");
            }

            if (model.CName != null)
            {
                strwhere.Append(" and NRequestForm.CName='" + model.CName + "' ");
            }

            if (model.DoctorName != null)
            {
                strwhere.Append(" and NRequestForm.DoctorName='" + model.DoctorName + "' ");
            }

            if (model.CollecterName != null)
            {
                strwhere.Append(" and NRequestForm.CollecterName='" + model.CollecterName + "' ");
            }

            if (model.SampleTypeName != null)
            {
                strwhere.Append(" and NRequestForm.SampleTypeName='" + model.SampleTypeName + "' ");
            }

            if (model.SerialNo != null)
            {
                strwhere.Append(" and NRequestForm.SerialNo='" + model.SerialNo + "' ");
            }

            if (model.ReceiveFlag != null)
            {
                strwhere.Append(" and NRequestForm.ReceiveFlag=" + model.ReceiveFlag + " ");
            }

            if (model.StatusNo != null)
            {
                strwhere.Append(" and NRequestForm.StatusNo=" + model.StatusNo + " ");
            }

            if (model.SampleTypeNo != null)
            {
                strwhere.Append(" and NRequestForm.SampleTypeNo=" + model.SampleTypeNo + " ");
            }

            if (model.PatNo != null)
            {
                strwhere.Append(" and NRequestForm.PatNo like '%" + model.PatNo + "%' ");
            }

            if (model.ClientName != null)
            {
                strwhere.Append(" and NRequestForm.ClientName='" + model.ClientName + "' ");
            }

            if (model.GenderNo != null)
            {
                strwhere.Append(" and NRequestForm.GenderNo=" + model.GenderNo + " ");
            }

            if (model.Birthday != null)
            {
                strwhere.Append(" and NRequestForm.Birthday='" + model.Birthday + "' ");
            }

            if (model.Age != null)
            {
                strwhere.Append(" and NRequestForm.Age=" + model.Age + " ");
            }

            if (model.AgeUnitNo != null)
            {
                strwhere.Append(" and NRequestForm.AgeUnitNo=" + model.AgeUnitNo + " ");
            }

            if (model.FolkNo != null)
            {
                strwhere.Append(" and NRequestForm.FolkNo=" + model.FolkNo + " ");
            }

            if (model.DistrictNo != null)
            {
                strwhere.Append(" and NRequestForm.DistrictNo=" + model.DistrictNo + " ");
            }

            if (model.WardNo != null)
            {
                strwhere.Append(" and NRequestForm.WardNo=" + model.WardNo + " ");
            }

            if (model.Bed != null)
            {
                strwhere.Append(" and NRequestForm.Bed='" + model.Bed + "' ");
            }

            if (model.DeptNo != null)
            {
                strwhere.Append(" and NRequestForm.DeptNo=" + model.DeptNo + " ");
            }

            if (model.Doctor != null)
            {
                strwhere.Append(" and NRequestForm.Doctor=" + model.Doctor + " ");
            }

            if (model.AgeUnitName != null)
            {
                strwhere.Append(" and NRequestForm.AgeUnitName='" + model.AgeUnitName + "' ");
            }

            if (model.DiagNo != null)
            {
                strwhere.Append(" and NRequestForm.DiagNo=" + model.DiagNo + " ");
            }

            if (model.Diag != null)
            {
                strwhere.Append(" and NRequestForm.Diag='" + model.Diag + "' ");
            }

            if (model.ChargeNo != null)
            {
                strwhere.Append(" and NRequestForm.ChargeNo=" + model.ChargeNo + " ");
            }

            if (model.Charge != null)
            {
                strwhere.Append(" and NRequestForm.Charge=" + model.Charge + " ");
            }

            if (model.Chargeflag != null)
            {
                strwhere.Append(" and NRequestForm.Chargeflag='" + model.Chargeflag + "' ");
            }

            if (model.CountNodesFormSource != null)
            {
                strwhere.Append(" and NRequestForm.CountNodesFormSource='" + model.CountNodesFormSource + "' ");
            }

            if (model.IsCheckFee != null)
            {
                strwhere.Append(" and NRequestForm.IsCheckFee=" + model.IsCheckFee + " ");
            }

            if (model.Operator != null)
            {
                strwhere.Append(" and NRequestForm.Operator='" + model.Operator + "' ");
            }

            if (model.OperDate != null)
            {
                strwhere.Append(" and NRequestForm.OperDate='" + model.OperDate + "' ");
            }

            if (model.OperTime != null)
            {
                strwhere.Append(" and NRequestForm.OperTime='" + model.OperTime + "' ");
            }

            if (model.GenderName != null)
            {
                strwhere.Append(" and NRequestForm.GenderName='" + model.GenderName + "' ");
            }

            if (model.FormMemo != null)
            {
                strwhere.Append(" and NRequestForm.FormMemo='" + model.FormMemo + "' ");
            }

            if (model.RequestSource != null)
            {
                strwhere.Append(" and NRequestForm.RequestSource='" + model.RequestSource + "' ");
            }

            if (model.SickOrder != null)
            {
                strwhere.Append(" and NRequestForm.SickOrder='" + model.SickOrder + "' ");
            }

            if (model.jztype != null)
            {
                strwhere.Append(" and NRequestForm.jztype=" + model.jztype + " ");
            }

            if (model.zdy1 != null)
            {
                strwhere.Append(" and NRequestForm.zdy1='" + model.zdy1 + "' ");
            }

            if (model.zdy2 != null)
            {
                strwhere.Append(" and NRequestForm.zdy2='" + model.zdy2 + "' ");
            }

            if (model.zdy3 != null)
            {
                strwhere.Append(" and NRequestForm.zdy3='" + model.zdy3 + "' ");
            }

            if (model.zdy4 != null)
            {
                strwhere.Append(" and NRequestForm.zdy4='" + model.zdy4 + "' ");
            }

            if (model.zdy5 != null)
            {
                strwhere.Append(" and NRequestForm.zdy5='" + model.zdy5 + "' ");
            }

            if (model.FlagDateDelete != null)
            {
                strwhere.Append(" and NRequestForm.FlagDateDelete='" + model.FlagDateDelete + "' ");
            }

            if (model.DeptName != null)
            {
                strwhere.Append(" and NRequestForm.DeptName='" + model.DeptName + "' ");
            }

            if (model.NurseFlag != null)
            {
                strwhere.Append(" and NRequestForm.NurseFlag=" + model.NurseFlag + " ");
            }

            if (model.IsByHand != null)
            {
                strwhere.Append(" and NRequestForm.IsByHand='" + model.IsByHand + "' ");
            }

            if (model.TestTypeNo != null)
            {
                strwhere.Append(" and NRequestForm.TestTypeNo=" + model.TestTypeNo + " ");
            }

            if (model.ExecDeptNo != null)
            {
                strwhere.Append(" and NRequestForm.ExecDeptNo=" + model.ExecDeptNo + " ");
            }

            if (model.CollectDate != null)
            {
                strwhere.Append(" and NRequestForm.CollectDate='" + model.CollectDate + "' ");
            }

            if (model.CollectTime != null)
            {
                strwhere.Append(" and NRequestForm.CollectTime='" + model.CollectTime + "' ");
            }

            if (model.Collecter != null)
            {
                strwhere.Append(" and NRequestForm.Collecter='" + model.Collecter + "' ");
            }

            if (model.LABCENTER != null)
            {
                strwhere.Append(" and NRequestForm.LABCENTER='" + model.LABCENTER + "' ");
            }

            if (model.NRequestFormNo != null)
            {
                strwhere.Append(" and NRequestForm.NRequestFormNo='" + model.NRequestFormNo + "' ");
            }

            if (model.CheckNo != null)
            {
                strwhere.Append(" and NRequestForm.CheckNo='" + model.CheckNo + "' ");
            }

            if (model.DistrictName != null)
            {
                strwhere.Append(" and NRequestForm.DistrictName='" + model.DistrictName + "' ");
            }

            if (model.CheckName != null)
            {
                strwhere.Append(" and NRequestForm.CheckName='" + model.CheckName + "' ");
            }

            if (model.WebLisSourceOrgID != null)
            {
                strwhere.Append(" and NRequestForm.WebLisSourceOrgID='" + model.WebLisSourceOrgID + "' ");
            }

            if (model.WebLisSourceOrgName != null)
            {
                strwhere.Append(" and NRequestForm.WebLisSourceOrgName='" + model.WebLisSourceOrgName + "' ");
            }

            if (model.WardName != null)
            {
                strwhere.Append(" and NRequestForm.WardName='" + model.WardName + "' ");
            }

            if (model.FolkName != null)
            {
                strwhere.Append(" and NRequestForm.FolkName='" + model.FolkName + "' ");
            }

            if (model.ClinicTypeName != null)
            {
                strwhere.Append(" and NRequestForm.ClinicTypeName='" + model.ClinicTypeName + "' ");
            }
            if (model.OperDateStart != null && model.OperDateStart != "")
            {
                strwhere.Append(" and NRequestForm.OperDate >='" + model.OperDateStart + "'  ");
            }
            if (model.OperDateEnd != null && model.OperDateEnd != "")
            {
                strwhere.Append(" and NRequestForm.OperDate <='" + model.OperDateEnd + "' ");
            }

            if (model.CollectDateStart != null)
            {
                strwhere.Append(" and NRequestForm.CollectDate >='" + model.CollectDateStart + "'  ");
            }
            if (model.CollectDateEnd != null)
            {
                strwhere.Append(" and NRequestForm.CollectDate <='" + model.CollectDateEnd + "'  ");
            }
            if (model.ClientList != null && model.ClientList.Trim() != "")
            {
                strwhere.Append(" and NRequestForm.ClientNo  in  (" + model.ClientList + ")  ");
            }

            if (model.DoctorNameList != null && model.DoctorNameList.Trim() != "")
            {
                strwhere.Append(" and NRequestForm.DoctorName  in  ('" + string.Join("','", model.DoctorNameList.Split(',')) + "')  ");
            }

            if (model.SickTypeList != null && model.SickTypeList.Trim() != "")
            {
                strwhere.Append(" and NRequestForm.jztype  in  ('" + string.Join("','", model.SickTypeList.Split(',')) + "')  ");
            }

            if (model.WeblisflagList != null && model.WeblisflagList.Trim() != "")
            {
                if (model.WeblisflagList.Split(',').ToList().Contains("0"))
                    strwhere.Append(" and (NRequestForm.Weblisflag  in  ('" + string.Join("','", model.WeblisflagList.Split(',')) + "') or NRequestForm.Weblisflag is null ) ");
                else
                    strwhere.Append(" and NRequestForm.Weblisflag  in  ('" + string.Join("','", model.WeblisflagList.Split(',')) + "') ");
            }

            if (model.PersonID != null && model.PersonID.Trim() != "")
            {
                strwhere.Append(" and NRequestForm.PersonID  =  '" + model.PersonID + "'  ");
            }
            if (model.BarCode != null && model.BarCode.Trim() != "")
            {
                strwhere.Append(" and NRequestForm.BarCode =  '" + model.BarCode + "'   ");
            }
            if (model.CombiItemNo != null && model.CombiItemNo.Trim() != "")
            {
                strwhere.Append(" and (select COUNT(*) FROM NRequestItem where 1=1      and CombiItemNo='" + model.CombiItemNo + "'  and NRequestForm.NRequestFormNo =NRequestFormNo )>0");
            }
            if (model.SampleSendNo != null && model.SampleSendNo.Trim() != "")
            {
                strwhere.Append(" and  (select COUNT(*) FROM dbo.BarCodeForm INNER JOIN dbo.NRequestItem ON dbo.BarCodeForm.BarCodeFormNo = dbo.NRequestItem.BarCodeFormNo where SampleSendNo = '" + model.SampleSendNo.Trim() + "'  and NRequestForm.NRequestFormNo = NRequestFormNo) > 0 ");
            }
            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("NRequestFormListOtherSQL") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("NRequestFormListOtherSQL").Trim() != "")
            {
                strwhere.Append(" and " + Common.Public.ConfigHelper.GetConfigString("NRequestFormListOtherSQL"));
            }

            #endregion
            StringBuilder strSql = new StringBuilder();
            if (model != null)
            {
                string fields = " NRequestForm.NRequestFormNo, " +
                        "NRequestForm.CName as CName," +
                        "NRequestForm.DoctorName as DoctorName," +
                        "NRequestForm.DeptName as DeptName," +
                        "NRequestForm.WebLisSourceOrgID," +
                        "NRequestForm.WebLisSourceOrgName as WebLisSourceOrgName," +
                        "NRequestForm.ClientNo," +
                        "NRequestForm.jztype," +
                        "NRequestForm.jztypeName," +
                        "NRequestForm.PatNo," +
                        "NRequestForm.ClientName as ClientName," +
                        "NRequestForm.AgeUnitName as AgeUnitName," +
                        "NRequestForm.Diag as Diag,GenderType.CName AS Sex," +
                        "NRequestForm.Age as Age, " +
                        "NRequestForm.WebLisFlag as WebLisFlag, " +
                        "NRequestForm.LABUPLOADDATE as LABUPLOADDATE, " +
                        "SampleType.CName AS SampleName," +
                        "Convert(varchar(10),NRequestForm.operdate,21)+' '+Convert(varchar(10),NRequestForm.opertime,8)as incepttime," +
                        "Convert(varchar(10),NRequestForm.CollectDate,21)+' '+Convert(varchar(10),NRequestForm.CollectTime,8)as CollectTime," +
                        "Convert(varchar(10),NRequestForm.OperDate,21)+' '+Convert(varchar(10),NRequestForm.OperDate,8)as  OperDate," +
                        "Convert(varchar(10),NRequestForm.OperTime,21)+' '+Convert(varchar(10),NRequestForm.OperTime,8)as  OperTime," +
                        "PatDiagInfo.DiagDesc as DiagDesc," +
                        "NRequestForm.PersonID ," +
                        "NRequestForm.Charge ," +
                        "NRequestForm.TestTypeName ," +
                         "Convert(varchar(10),NRequestForm.Receivedate,21) as  Receivedate," +
                        "NRequestForm.zdy1 ," +
                        "NRequestForm.zdy2 ," +
                        "NRequestForm.zdy3 ," +
                        "NRequestForm.zdy4 ," +
                        "NRequestForm.zdy5 ," +
                        "NRequestForm.zdy6 ," +
                        "NRequestForm.zdy7 ," +
                        "NRequestForm.zdy8 ," +
                        "NRequestForm.zdy9 ," +
                        "NRequestForm.zdy10 ";
                string from = "FROM dbo.GenderType RIGHT OUTER JOIN " +
                        "dbo.NRequestForm LEFT OUTER JOIN " +
                        "dbo.PatDiagInfo ON dbo.NRequestForm.DiagNo = dbo.PatDiagInfo.DiagNo LEFT OUTER JOIN " +
                        "dbo.SampleType ON dbo.NRequestForm.SampleTypeNo = dbo.SampleType.SampleTypeNo ON dbo.GenderType.GenderNo = dbo.NRequestForm.GenderNo ";

                string tmpwhere = (strwhere.ToString().Trim() == "") ? " 1=2 " : " 1=1 " + strwhere.ToString() + " ";

                string NRequestFormNolist = " select top " + (pageSize * startPage) + " NRequestForm.NRequestFormNo FROM dbo.GenderType RIGHT OUTER JOIN  dbo.NRequestForm LEFT OUTER JOIN  dbo.PatDiagInfo ON dbo.NRequestForm.DiagNo = dbo.PatDiagInfo.DiagNo LEFT OUTER JOIN  dbo.SampleType ON dbo.NRequestForm.SampleTypeNo = dbo.SampleType.SampleTypeNo ON dbo.GenderType.GenderNo = dbo.NRequestForm.GenderNo where " + tmpwhere + " order by NRequestForm.OperDate desc, NRequestForm.OperTime desc ";

                string where = " where " + tmpwhere + " and NRequestForm.NRequestFormNo not in (" + NRequestFormNolist + ")  order by NRequestForm.OperDate desc,NRequestForm.OperTime desc";


                strSql.Append(" select * from ( SELECT top " + pageSize + fields + from + where + " ) aa order by aa.OperDate asc,aa.OperTime asc ");

                ZhiFang.Common.Log.Log.Info(strSql.ToString());
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
            else
            {
                strSql.Append("  select * from ( SELECT distinct top " + pageSize + "  NRequestForm.NRequestFormNo, " +
                        "NRequestForm.CName as CName," +
                        "NRequestForm.DoctorName as DoctorName," +
                        "NRequestForm.WebLisSourceOrgID," +
                        "NRequestForm.WebLisSourceOrgName as WebLisSourceOrgName," +
                        "NRequestForm.ClientNo," +
                        "NRequestForm.ClientName as ClientName," +
                        "NRequestForm.AgeUnitName as AgeUnitName," +
                        "NRequestForm.Diag as Diag,GenderType.CName AS Sex," +
                        "NRequestForm.Age as Age, " +
                        "SampleType.CName AS SampleName," +
                        "Convert(varchar(10),NRequestForm.operdate,21)+' '+Convert(varchar(10),NRequestForm.opertime,8)as incepttime," +
                        "Convert(varchar(10),NRequestForm.CollectDate,21)+' '+Convert(varchar(10),NRequestForm.CollectTime,8)as CollectTime," +
                        "NRequestForm.OperDate as OperDate," +
                        "NRequestForm.OperTime as OperTime," +
                        "NRequestForm.LABUPLOADDATE as LABUPLOADDATE, " +
                        "PatDiagInfo.DiagDesc as DiagDesc," +
                        "NRequestForm.zdy1 ," +
                        "NRequestForm.zdy2 ," +
                        "NRequestForm.zdy3 ," +
                        "NRequestForm.zdy4 ," +
                        "NRequestForm.zdy5 ," +
                        "NRequestForm.zdy6 ," +
                        "NRequestForm.zdy7 ," +
                        "NRequestForm.zdy8 ," +
                        "NRequestForm.zdy9 ," +
                        "NRequestForm.zdy10 " +
                        "FROM  dbo.GenderType RIGHT OUTER JOIN " +
                        "dbo.NRequestForm LEFT OUTER JOIN " +
                        "dbo.PatDiagInfo ON dbo.NRequestForm.DiagNo = dbo.PatDiagInfo.DiagNo LEFT OUTER JOIN " +
                        "dbo.SampleType ON dbo.NRequestForm.SampleTypeNo = dbo.SampleType.SampleTypeNo ON dbo.GenderType.GenderNo = dbo.NRequestForm.GenderNo ");

                strSql.Append(" where NRequestForm.NRequestFormNo not in (  ");
                strSql.Append("select  top " + (pageSize * startPage) + " NRequestForm.NRequestFormNo from  dbo.GenderType RIGHT OUTER JOIN " +
                        "dbo.NRequestForm LEFT OUTER JOIN " +
                        "dbo.PatDiagInfo ON dbo.NRequestForm.DiagNo = dbo.PatDiagInfo.DiagNo LEFT OUTER JOIN " +
                        "dbo.SampleType ON dbo.NRequestForm.SampleTypeNo = dbo.SampleType.SampleTypeNo ON dbo.GenderType.GenderNo = dbo.NRequestForm.GenderNo ");

                strSql.Append("order by NRequestForm.OperDate desc,NRequestForm.OperTime desc ) order by NRequestForm.OperDate desc,NRequestForm.OperTime desc  ) aa order by aa.OperDate asc,aa.OperTime asc ");
                ZhiFang.Common.Log.Log.Info(strSql.ToString());
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
        }

        public DataTable GetBarCodeByNRequestFormNo(string p)
        {
            if (p.Trim() != "")
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" SELECT     dbo.BarCodeForm.BarCode,dbo.BarCodeForm.BarCodeFormNo,dbo.BarCodeForm.color,dbo.BarCodeForm.ItemName,dbo.BarCodeForm.ItemNo,dbo.BarCodeForm.WebLisFlag FROM         dbo.NRequestForm INNER JOIN   dbo.NRequestItem ON NRequestForm.NRequestFormNo = dbo.NRequestItem.NRequestFormNo INNER JOIN       dbo.BarCodeForm ON dbo.NRequestItem.BarCodeFormNo = dbo.BarCodeForm.BarCodeFormNo          where NRequestForm.NRequestFormNo=" + p + "             group by dbo.BarCodeForm.BarCode,dbo.BarCodeForm.BarCodeFormNo,dbo.BarCodeForm.color,dbo.BarCodeForm.ItemName,dbo.BarCodeForm.ItemNo,dbo.BarCodeForm.WebLisFlag");
                DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());
                if (ds != null && ds.Tables.Count > 0)
                {
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public DataTable GetBarCodeByNRequestFormNo(string p, string barCode)
        {
            if (p.Trim() != "")
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" SELECT     dbo.BarCodeForm.BarCode,dbo.BarCodeForm.BarCodeFormNo,dbo.BarCodeForm.color,dbo.BarCodeForm.SampleTypeName,dbo.BarCodeForm.ItemName,dbo.BarCodeForm.ItemNo FROM         dbo.NRequestForm INNER JOIN   dbo.NRequestItem ON NRequestForm.NRequestFormNo = dbo.NRequestItem.NRequestFormNo INNER JOIN       dbo.BarCodeForm ON dbo.NRequestItem.BarCodeFormNo = dbo.BarCodeForm.BarCodeFormNo          where NRequestForm.NRequestFormNo='" + p + "' and dbo.BarCodeForm.BarCode='" + barCode + "'           group by dbo.BarCodeForm.BarCode,dbo.BarCodeForm.BarCodeFormNo,dbo.BarCodeForm.color,dbo.BarCodeForm.ItemName,dbo.BarCodeForm.SampleTypeName,dbo.BarCodeForm.ItemNo");
                DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());
                if (ds != null && ds.Tables.Count > 0)
                {
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public DataTable GetBarCodeAndCNameByNRequestFormNo(string p)
        {
            if (p.Trim() != "")
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" SELECT     dbo.BarCodeForm.BarCode,dbo.BarCodeForm.BarCodeFormNo,NRequestForm.CName,NRequestForm.CollectDate, BarCodeForm.ReceiveDate,NRequestForm.OperDate FROM  ");
                strSql.Append(" dbo.NRequestForm INNER JOIN   dbo.NRequestItem ON NRequestForm.NRequestFormNo = dbo.NRequestItem.NRequestFormNo INNER JOIN       dbo.BarCodeForm ");
                strSql.Append(" ON dbo.NRequestItem.BarCodeFormNo = dbo.BarCodeForm.BarCodeFormNo ");
                strSql.Append(" where NRequestForm.NRequestFormNo=" + p + "            group by dbo.BarCodeForm.BarCode,dbo.BarCodeForm.BarCodeFormNo,NRequestForm.CName,NRequestForm.CollectDate, BarCodeForm.ReceiveDate,NRequestForm.OperDate");
                DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());
                if (ds != null && ds.Tables.Count > 0)
                {
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public int CheckNReportFormWeblisFlag(long NRequestFormNo)
        {
            StringBuilder strwhere = new StringBuilder();
            strwhere.Append(" SELECT count(*) FROM dbo.BarCodeForm INNER JOIN dbo.NRequestItem ON dbo.BarCodeForm.BarCodeFormNo = dbo.NRequestItem.BarCodeFormNo INNER JOIN dbo.NRequestForm ON dbo.NRequestItem.NRequestFormNo = dbo.NRequestForm.NRequestFormNo ");
            if (NRequestFormNo != 0)
            {

                strwhere.Append("   where NRequestForm.NRequestFormno=" + NRequestFormNo + " and BarCodeForm.weblisflag=5  ");
            }
            else
            {
                return 0;
            }
            ZhiFang.Common.Log.Log.Info(strwhere.ToString());
            string strCount = idb.ExecuteScalar(strwhere.ToString());
            if (strCount != null && strCount.Trim() != "")
            {
                return Convert.ToInt32(strCount.Trim());
            }
            else
            {
                return 0;
            }
        }

        #region  项目报表查询
        public DataSet GetStaticRecOrgSamplePrice(Model.StaticRecOrgSamplePrice model, int rows, int page)
        {
            StringBuilder strwhere = new StringBuilder();
            #region where
            if (model.OperDateBegin != null)
            {
                strwhere.Append(" a.OperDate>='" + model.OperDateBegin + "' ");
            }
            if (model.OperDateEnd != null)
            {
                strwhere.Append(" and a.OperDate<='" + model.OperDateEnd + " 23:59' ");
            }
            if (model.ClientNo != null)
            {
                strwhere.Append(" and a.ClientNo='" + model.ClientNo + "' ");
            }
            if (model.CName != null)
            {
                strwhere.Append(" and d.CName='" + model.CName + "' ");
            }
            #endregion

            StringBuilder strSql = new StringBuilder();

            if (model != null)
            {

                strSql.Append(" select top " + rows + " * from (select  ROW_NUMBER() over(order by a.OperDate desc) rownumber,a.ClientName, d.CName, b.ParItemNo,COUNT(b.ParItemNo) 'SampleNum',b.price,COUNT(b.ParItemNo)*b.price 'ItemTotalPrice' from NRequestForm a join NRequestItem b on a.NRequestFormNo=b.NRequestFormNo join BarCodeForm c on b.BarCodeFormNo=c.BarCodeFormNo join B_Lab_TestItem d on d.ItemNo=b.LabParItemNo and d.LabCode=a.ClientNo where 1=1 and " + strwhere.ToString() + "and c.WebLisFlag=5 and b.ParItemNo>0 group by a.OperDate,a.ClientName,d.CName, b.ParItemNo,b.price) subTable where subTable.rownumber>" + rows * (page - 1));

            }

            ZhiFang.Common.Log.Log.Info(strSql.ToString());
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetStaticRecOrgSamplePrice(Model.StaticRecOrgSamplePrice model)
        {
            StringBuilder strwhere = new StringBuilder();
            #region where
            if (model.OperDateBegin != null)
            {
                strwhere.Append("a.OperDate>='" + model.OperDateBegin + "' ");
            }
            if (model.OperDateEnd != null)
            {
                strwhere.Append(" and a.OperDate<='" + model.OperDateEnd + " 23:59' ");
            }
            if (model.ClientNo != null && model.ClientNo != "")
            {
                strwhere.Append(" and a.ClientNo='" + model.ClientNo + "' ");
            }
            if (model.CName != null && model.CName != "")
            {
                strwhere.Append(" and d.CName='" + model.CName + "' ");
            }
            #endregion

            StringBuilder strSql = new StringBuilder();

            if (model != null)
            {
                //strSql.Append("select a.ClientName, d.CName, a.ParItemNo,COUNT(a.ParItemNo) 'SampleNum',a.price 'Price',COUNT(a.ParItemNo)*a.price 'ItemTotalPrice'");

                //strSql.Append("from NRequestForm b join NRequestItem a on a.NRequestFormNo=b.NRequestFormNo join BarCodeForm c on a.BarCodeFormNo=c.BarCodeFormNo join B_Lab_TestItem d on d.ItemNo=a.LabParItemNo and d.LabCode=a.ClientNo ");
                //if (strwhere.ToString().Trim() == "")
                //{
                //    strSql.Append(" where 1=2");
                //}
                //else
                //{
                //    strSql.Append(" where c.WebLisFlag=5 and a.ParItemNo>0 " + strwhere.ToString());
                //    strSql.Append("group by a.ClientName,d.CName, a.ParItemNo,a.price");
                //}
                strSql.Append("select  ROW_NUMBER() over(order by a.OperDate desc) rownumber,a.ClientName, d.CName, b.ParItemNo,COUNT(b.ParItemNo) 'SampleNum',b.price,COUNT(b.ParItemNo)*b.price 'ItemTotalPrice' from NRequestForm a join NRequestItem b on a.NRequestFormNo=b.NRequestFormNo join BarCodeForm c on b.BarCodeFormNo=c.BarCodeFormNo join B_Lab_TestItem d on d.ItemNo=b.LabParItemNo and d.LabCode=a.ClientNo where 1=1 and " + strwhere.ToString() + " and c.WebLisFlag=5 and b.ParItemNo>0 group by a.OperDate,a.ClientName,d.CName, b.ParItemNo,b.price");

            }
            ZhiFang.Common.Log.Log.Info(strSql.ToString());
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());

        }

        #endregion

        #region 个人检验情况统计
        public DataSet GetStaticPersonTestItemPriceList(int page, int rows, ZhiFang.Model.StaticPersonTestItemPrice model)
        {
            StringBuilder strwhere = new StringBuilder();
            #region where
            if (model.BCName != null)
            {
                strwhere.Append(" and b.CName='" + model.BCName + "' ");
            }
            if (model.BarCode != null)
            {
                strwhere.Append(" and b.BarCode='" + model.BarCode + "' ");
            }
            if (model.ClientName != null)
            {
                strwhere.Append(" and b.ClientNo='" + model.ClientName + "' ");
            }
            if (model.DCName != null)
            {
                strwhere.Append(" and d.CName='" + model.DCName + "' ");
            }
            if (model.PatNo != null)
            {
                strwhere.Append(" and b.PatNo='" + model.PatNo + "' ");
            }
            #endregion
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top " + rows + " b.OperDate ,b.Age ,b.AgeUnit ,b.GenderName ,b.ClientName ,b.CName as BCName ,b.PatNo ,c.barcode ,a.ParItemNo ,d.CName as DCName ,a.price " +
                          "from NRequestItem a left join NRequestForm b on a.NRequestFormNo=b.NRequestFormNo left join BarCodeForm c on c.BarCodeFormNo=a.BarCodeFormNo left join  B_Lab_TestItem d on d.ItemNo=a.ParItemNo " +
                          "where c.weblisFlag=5 and d.isCombiItem is null and OperDate>='" + model.OperdateBegin + "' and OperDate<='" + model.OperdateEnd + "' " + strwhere + " and a.NRequestItemNo not in(select top " + (page - 1) * rows +
                          " a.NRequestItemNo from NRequestItem a left join NRequestForm b on a.NRequestFormNo=b.NRequestFormNo left join BarCodeForm c on c.BarCodeFormNo=a.BarCodeFormNo left join  B_Lab_TestItem d on d.ItemNo=a.ParItemNo " +
                          "where c.weblisFlag=5 and d.isCombiItem is null and a.ParItemNo >0 and OperDate>='" + model.OperdateBegin + "' and OperDate<='" + model.OperdateEnd + "' " + strwhere +
                          " group by b.GenderName,b.Age,b.AgeUnit,b.PatNo,b.CName, c.barcode,b.SerialNo, a.ParItemNo,d.CName, b.OperDate, b.ClientName, a.NRequestItemNo, a.Price order by b.OperDate desc, b.CName" +
                          ") group by  b.GenderName,b.Age,b.AgeUnit,b.PatNo,b.CName, c.barcode,b.SerialNo, a.ParItemNo,d.CName, b.OperDate, b.ClientName, a.NRequestItemNo, a.Price order by b.OperDate desc, b.CName");




            //ZhiFang.Common.Log.Log.Info(strSql.ToString());
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetStaticPersonTestItemPriceList(ZhiFang.Model.StaticPersonTestItemPrice model)
        {
            StringBuilder strwhere = new StringBuilder();
            #region where
            if (model.BCName != null)
            {
                strwhere.Append(" and b.CName='" + model.BCName + "' ");
            }
            if (model.BarCode != null)
            {
                strwhere.Append(" and b.BarCode='" + model.BarCode + "' ");
            }
            if (model.ClientName != null)
            {
                strwhere.Append(" and b.ClientNo='" + model.ClientName + "' ");
            }
            if (model.DCName != null)
            {
                strwhere.Append(" and d.CName='" + model.DCName + "' ");
            }
            if (model.PatNo != null)
            {
                strwhere.Append(" and b.PatNo='" + model.PatNo + "' ");
            }
            #endregion
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select b.OperDate ,b.Age ,b.AgeUnit ,b.GenderName ,b.ClientName ,b.CName as BCName ,b.PatNo ,c.barcode ,a.ParItemNo ,d.CName as DCName ,a.price " +
                          "from NRequestItem a left join NRequestForm b on a.NRequestFormNo=b.NRequestFormNo left join BarCodeForm c on c.BarCodeFormNo=a.BarCodeFormNo left join  B_Lab_TestItem d on d.ItemNo=a.ParItemNo " +
                          "where c.weblisFlag=5 and a.ParItemNo >0  and OperDate>='" + model.OperdateBegin + "' and OperDate<='" + model.OperdateEnd + "' " + strwhere +
                          " group by  b.GenderName,b.Age,b.AgeUnit,b.PatNo,b.CName, c.barcode,b.SerialNo, a.ParItemNo,d.CName, b.OperDate, b.ClientName, a.NRequestItemNo, a.Price order by b.OperDate desc, b.CName");
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
        #endregion

        /// <summary>
        /// 太和医院操作人员工作量统计
        /// </summary>
        /// <param name="model"></param>
        /// <param name="rows"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public DataSet GetOpertorWorkCount(Model.StaticRecOrgSamplePrice model, int rows, int page)
        {
            StringBuilder strSql = new StringBuilder();
            int dateType = 10;
            if (model.DateType == "month")
            {
                dateType = 7;
            }
            if (model.DateType == "year")
            {
                dateType = 4;
            }
            #region 查询全部
            if (rows == 0 && page == 0)
            {

                strSql.Append("select  ROW_NUMBER() over(order by left(convert(varchar,a.OperDate,21)," + dateType + ") desc) rownumber, left(convert(varchar,a.OperDate,21)," + dateType + ") OperDate ,a.ClientName ,a.ClientNo,a.Collecter Operator, COUNT(distinct c.barcode) BarcodeNum ,sum(b.price) SumMoney");
                strSql.Append(" from  NRequestForm a  join NRequestItem b on a.NRequestFormNo=b.NRequestFormNo join BarCodeForm c on c.BarCodeFormNo=b.BarCodeFormNo  where  1=1 ");

                if (model.OperDateBegin != null)
                {
                    strSql.Append(" and a.OperDate>='" + model.OperDateBegin + "' ");
                }
                if (model.OperDateEnd != null)
                {
                    strSql.Append(" and a.OperDate<='" + model.OperDateEnd + "' ");
                }
                if (model.ClientNo != null)
                {
                    strSql.Append(" and a.ClientNo='" + model.ClientNo + "' ");
                }
                if (model.Operator != null)
                {
                    strSql.Append(" and a.Collecter='" + model.Operator + "' ");
                }

                strSql.Append(" and c.WebLisFlag=5 and b.ParItemNo>0  ");
                strSql.Append(" group by a.ClientName, a.ClientNo, a.Collecter, left(convert(varchar,a.OperDate,21)," + dateType + ')');

            }

            #endregion

            #region 分页查询
            else
            {
                strSql.Append(" select top " + rows + " * from  ");
                strSql.Append("(select  ROW_NUMBER() over(order by left(convert(varchar,a.OperDate,21)," + dateType + ") desc) rownumber, left(convert(varchar,a.OperDate,21)," + dateType + ") OperDate ,a.ClientName ,a.ClientNo,a.Collecter Operator, COUNT(distinct c.barcode) BarcodeNum ,sum(b.price) SumMoney");
                strSql.Append(" from  NRequestForm a  join NRequestItem b on a.NRequestFormNo=b.NRequestFormNo join BarCodeForm c on c.BarCodeFormNo=b.BarCodeFormNo  where  1=1 ");

                if (model.OperDateBegin != null)
                {
                    strSql.Append(" and a.OperDate>='" + model.OperDateBegin + "' ");
                }
                if (model.OperDateEnd != null)
                {
                    strSql.Append(" and a.OperDate<='" + model.OperDateEnd + "' ");
                }
                if (model.ClientNo != null)
                {
                    strSql.Append(" and a.ClientNo='" + model.ClientNo + "' ");
                }
                if (model.Operator != null)
                {
                    strSql.Append(" and a.Collecter='" + model.Operator + "' ");
                }

                strSql.Append(" and c.WebLisFlag=5 and b.ParItemNo>0  ");
                strSql.Append(" group by a.ClientName, a.ClientNo, a.Collecter, left(convert(varchar,a.OperDate,21)," + dateType + ")) subTable ");
                strSql.Append("  where subTable.rownumber>" + rows * (page - 1));
            }

            #endregion

            ZhiFang.Common.Log.Log.Info(strSql.ToString());
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public int Add(string strSql)
        {
            return DbHelperSQL.ExecuteNonQuery(strSql);
        }

        public DataSet GetRefuseList(string strSql)
        {
            return DbHelperSQL.ExecuteDataSet(strSql);
        }

        DataSet IDNRequestForm.GetBarcodePrice(Model.StaticRecOrgSamplePrice model, int rows, int page)
        {
            StringBuilder strwhere = new StringBuilder();
            #region where
            if (model.OperDateBegin != null)
            {
                strwhere.Append(" and a.OperDate>='" + model.OperDateBegin + "' ");
            }
            if (model.OperDateEnd != null)
            {
                strwhere.Append(" and a.OperDate<='" + model.OperDateEnd + "' ");
            }
            if (model.ClientNo != null)
            {
                strwhere.Append(" and a.ClientNo='" + model.ClientNo + "' ");
            }
            int dateType = 10;
            if (model.DateType == "month")
            {
                dateType = 7;
            }
            if (model.DateType == "year")
            {
                dateType = 4;
            }

            #endregion

            StringBuilder strSql = new StringBuilder();

            if (model != null && strwhere.ToString().Trim() != "")
            {
                strSql.Append(" select top " + rows + " * from (select  ROW_NUMBER() over(order by left(convert(varchar(" + dateType + "),a.OperDate,21),10) desc) rownumber,left(convert(varchar(" + dateType + "),a.OperDate,21),10) OperDate ,a.ClientName ,COUNT(distinct c.barcode) Barcode ,sum(b.price) Price from  NRequestForm a  join NRequestItem b on a.NRequestFormNo=b.NRequestFormNo join BarCodeForm c on c.BarCodeFormNo=b.BarCodeFormNo where  1=1 " + strwhere + " and c.WebLisFlag=5 and b.ParItemNo>0 group by a.ClientName,  left(convert(varchar(" + dateType + "),a.OperDate,21),10)) subTable where subTable.rownumber>" + rows * (page - 1));
                //strSql.Append("(select  ROW_NUMBER() over(order by a.OperDate desc) rownumber, left(convert(varchar(" + dateType + "),a.OperDate,21),10) OperDate , a.ClientName ,COUNT(c.barcode) Barcode ,sum(b.price) Price  ");
                //strSql.Append("from NRequestForm a  join NRequestItem b on a.NRequestFormNo=b.NRequestFormNo join BarCodeForm c on c.BarCodeFormNo=b.BarCodeFormNo ");
                //strSql.Append("where  1=1 " + strwhere + " and c.WebLisFlag=5 and b.ParItemNo>0group by a.OperDate,a.ClientName, c.BarCode,  left(convert(varchar(" + dateType + "),a.OperDate,21),10)) ");
                //strSql.Append("subTable where subTable.rownumber>"+rows*(page-1));

                //strSql.Append("select top " + rows + " * from");
                //strSql.Append("(select  ROW_NUMBER() over(order by left(convert(varchar(" + dateType + "),a.OperDate,21),10) desc) rownumber,left(convert(varchar(" + dateType + "),a.OperDate,21),10) OperDate ,a.ClientName ,COUNT(distinct c.barcode) Barcode ,sum(b.price) Price");
                //strSql.Append("from  NRequestForm a  join NRequestItem b on a.NRequestFormNo=b.NRequestFormNo join BarCodeForm c on c.BarCodeFormNo=b.BarCodeFormNo");
                //strSql.Append("where  1=1 " + strwhere + " and c.WebLisFlag=5 and b.ParItemNo>0 group by a.ClientName,  left(convert(varchar(" + dateType + "),a.OperDate,21),10))");
                //strSql.Append("subTable where subTable.rownumber>0" + rows * (page - 1));

            }

            ZhiFang.Common.Log.Log.Info(strSql.ToString());
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        DataSet IDNRequestForm.GetBarcodePrice(Model.StaticRecOrgSamplePrice model)
        {
            StringBuilder strwhere = new StringBuilder();
            #region where
            if (model.OperDateBegin != null)
            {
                strwhere.Append(" and a.OperDate>='" + model.OperDateBegin + "' ");
            }
            if (model.OperDateEnd != null)
            {
                strwhere.Append(" and a.OperDate<='" + model.OperDateEnd + "' ");
            }
            if (model.ClientNo != null && model.ClientNo != "")
            {
                strwhere.Append(" and a.ClientNo='" + model.ClientNo + "' ");
            }
            int dateType = 10;
            if (model.DateType == "month")
            {
                dateType = 7;
            }
            if (model.DateType == "year")
            {
                dateType = 4;
            }

            #endregion

            StringBuilder strSql = new StringBuilder();

            if (model != null && strwhere.ToString().Trim() != "")
            {
                //strSql.Append("select  ROW_NUMBER() over(order by a.OperDate desc) rownumber, left(convert(varchar(" + dateType + "),a.OperDate,21),10) OperDate , a.ClientName ,COUNT(c.barcode) Barcode ,sum(b.price) Price  ");
                //strSql.Append("from NRequestForm a  join NRequestItem b on a.NRequestFormNo=b.NRequestFormNo join BarCodeForm c on c.BarCodeFormNo=b.BarCodeFormNo ");
                //strSql.Append("where  1=1 " + strwhere + " and c.WebLisFlag=5 and b.ParItemNo>0group by a.OperDate,a.ClientName, c.BarCode,  left(convert(varchar(" + dateType + "),a.OperDate,21),10)");
                strSql.Append("select  ROW_NUMBER() over(order by left(convert(varchar(" + dateType + "),a.OperDate,21),10) desc) rownumber,left(convert(varchar(" + dateType + "),a.OperDate,21),10) OperDate ,a.ClientName ,COUNT(distinct c.barcode) Barcode ,sum(b.price) Price from  NRequestForm a  join NRequestItem b on a.NRequestFormNo=b.NRequestFormNo join BarCodeForm c on c.BarCodeFormNo=b.BarCodeFormNo where  1=1 " + strwhere + " and c.WebLisFlag=5 and b.ParItemNo>0 group by a.ClientName,  left(convert(varchar(" + dateType + "),a.OperDate,21),10)");

            }

            ZhiFang.Common.Log.Log.Info(strSql.ToString());
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
        #region 消费采样模块
        /// <summary>
        /// 获取查看本采血站点所录入的申请单信息
        /// 查询条件：项目条码、消费码、姓名。排序时间倒序
        /// </summary>
        /// <param name="model"></param>
        /// <param name="nowPageNum"></param>
        /// <param name="nowPageSize"></param>
        /// <returns></returns>
        public DataSet GetNRequstFormListByDetailsAndPage(Model.NRequestForm model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strwhere = GetGetNRequstFormListByDetailsWhere(model);
            StringBuilder strSql = new StringBuilder();
            if (model != null)
            {
                #region 有查询条件的处理
                strSql.Append(" select * from ( SELECT top " + nowPageSize + " NRequestForm.ZDY10,NRequestForm.NRequestFormNo,NRequestForm.OldSerialNo,NRequestForm.CName as CName, NRequestForm.DoctorName as DoctorName,NRequestForm.DeptName as DeptName, NRequestForm.WebLisSourceOrgID, NRequestForm.WebLisSourceOrgName as WebLisSourceOrgName, NRequestForm.ClientNo, NRequestForm.jztype, NRequestForm.jztypeName, NRequestForm.PatNo, NRequestForm.ClientName as ClientName, NRequestForm.AgeUnitName as AgeUnitName, NRequestForm.Diag as Diag, GenderType.CName AS Sex, NRequestForm.Age as Age, SampleType.CName AS SampleName, Convert(varchar(10), NRequestForm.operdate, 21) + ' ' + Convert(varchar(10), NRequestForm.opertime, 8) as incepttime, Convert(varchar(10), NRequestForm.CollectDate, 21) + ' ' + Convert(varchar(10), NRequestForm.CollectTime, 8) as CollectTime, NRequestForm.OperDate as OperDate, NRequestForm.OperTime as OperTime, PatDiagInfo.DiagDesc as DiagDesc, NRequestForm.zdy5 FROM dbo.GenderType RIGHT OUTER JOIN dbo.NRequestForm LEFT OUTER JOIN dbo.PatDiagInfo ON dbo.NRequestForm.DiagNo = dbo.PatDiagInfo.DiagNo LEFT OUTER JOIN dbo.SampleType ON dbo.NRequestForm.SampleTypeNo = dbo.SampleType.SampleTypeNo ON dbo.GenderType.GenderNo = dbo.NRequestForm.GenderNo LEFT JOIN dbo.NRequestItem ON NRequestForm.NRequestFormNo = dbo.NRequestItem.NRequestFormNo LEFT JOIN dbo.BarCodeForm ON dbo.NRequestItem.BarCodeFormNo = dbo.BarCodeForm.BarCodeFormNo where");


                if (strwhere.ToString().Trim() == "")
                {
                    strSql.Append(" 1=2");
                }
                else
                {
                    strSql.Append(" 1=1 " + strwhere.ToString() + "   ");
                }

                //strSql.Append(" and NRequestForm.NRequestFormNo not in (select top 0 NRequestForm.NRequestFormNo FROM dbo.GenderType RIGHT OUTER JOIN dbo.NRequestForm LEFT OUTER JOIN dbo.PatDiagInfo ON dbo.NRequestForm.DiagNo = dbo.PatDiagInfo.DiagNo LEFT OUTER JOIN dbo.SampleType ON dbo.NRequestForm.SampleTypeNo = dbo.SampleType.SampleTypeNo ON dbo.GenderType.GenderNo = dbo.NRequestForm.GenderNo ) ");

                strSql.Append(" group by NRequestForm.ZDY10,NRequestForm.NRequestFormNo,NRequestForm.OldSerialNo, NRequestForm.CName, NRequestForm.DoctorName,NRequestForm.DeptName,NRequestForm.WebLisSourceOrgID, NRequestForm.WebLisSourceOrgName,NRequestForm.ClientNo,NRequestForm.jztype, NRequestForm.jztypeName,NRequestForm.PatNo,NRequestForm.ClientName, NRequestForm.AgeUnitName,NRequestForm.Diag,GenderType.CName,NRequestForm.Age, SampleType.CName,Convert(varchar(10),NRequestForm.operdate,21)+' '+Convert(varchar(10), NRequestForm.opertime,8),Convert(varchar(10),NRequestForm.CollectDate,21)+' '+Convert(varchar(10), NRequestForm.CollectTime,8),NRequestForm.OperDate,NRequestForm.OperTime, PatDiagInfo.DiagDesc,NRequestForm.zdy5 ) aa");

                strSql.Append(" order by aa.OperDate desc,aa.OperTime desc");
                //ZhiFang.Common.Log.Log.Info(strSql.ToString());
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
                #endregion
            }
            else
            {
                strSql.Append("  select * from ( SELECT distinct top " + nowPageSize + "  NRequestForm.ZDY10,NRequestForm.NRequestFormNo, " +
                    "NRequestForm.OldSerialNo," +
                        "NRequestForm.CName as CName," +
                        "NRequestForm.DoctorName as DoctorName," +
                        "NRequestForm.WebLisSourceOrgID," +
                        "NRequestForm.WebLisSourceOrgName as WebLisSourceOrgName," +
                        "NRequestForm.ClientNo," +
                        "NRequestForm.ClientName as ClientName," +
                        "NRequestForm.AgeUnitName as AgeUnitName," +
                        "NRequestForm.Diag as Diag,GenderType.CName AS Sex," +
                        "NRequestForm.Age as Age, " +
                        "SampleType.CName AS SampleName," +
                        "Convert(varchar(10),NRequestForm.operdate,21)+' '+Convert(varchar(10),NRequestForm.opertime,8)as incepttime," +
                        "Convert(varchar(10),NRequestForm.CollectDate,21)+' '+Convert(varchar(10),NRequestForm.CollectTime,8)as CollectTime," +
                        "NRequestForm.OperDate as OperDate," +
                        "NRequestForm.OperTime as OperTime," +
                        "PatDiagInfo.DiagDesc as DiagDesc," +
                        "NRequestForm.zdy5 " +
                        "FROM  dbo.GenderType RIGHT OUTER JOIN " +
                        "dbo.NRequestForm LEFT OUTER JOIN " +
                        "dbo.PatDiagInfo ON dbo.NRequestForm.DiagNo = dbo.PatDiagInfo.DiagNo LEFT OUTER JOIN " +
                        "dbo.SampleType ON dbo.NRequestForm.SampleTypeNo = dbo.SampleType.SampleTypeNo ON dbo.GenderType.GenderNo = dbo.NRequestForm.GenderNo ");

                strSql.Append(" where NRequestForm.NRequestFormNo not in (  ");
                strSql.Append("select  top " + (nowPageSize * nowPageNum) + " NRequestForm.NRequestFormNo from  dbo.GenderType RIGHT OUTER JOIN " +
                        "dbo.NRequestForm LEFT OUTER JOIN " +
                        "dbo.PatDiagInfo ON dbo.NRequestForm.DiagNo = dbo.PatDiagInfo.DiagNo LEFT OUTER JOIN " +
                        "dbo.SampleType ON dbo.NRequestForm.SampleTypeNo = dbo.SampleType.SampleTypeNo ON dbo.GenderType.GenderNo = dbo.NRequestForm.GenderNo ");

                strSql.Append("order by NRequestForm.OperDate desc,NRequestForm.OperTime desc ) order by NRequestForm.OperDate desc,NRequestForm.OperTime desc  ) aa order by aa.OperDate desc,aa.OperTime desc ");

                //ZhiFang.Common.Log.Log.Info(strSql.ToString());
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
        }
        /// <summary>
        /// 获取查看本采血站点所录入的申请单总记录数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int GetNRequstFormListByDetailsTotalCount(Model.NRequestForm model)
        {
            StringBuilder strwhere = GetGetNRequstFormListByDetailsWhere(model);

            StringBuilder strSql = new StringBuilder();
            if (model != null)
            {
                #region 有查询条件的处理
                if (strwhere.ToString().Trim() != "")
                {
                    strSql.Append(" select COUNT(*) from (SELECT NRequestForm.ZDY10,NRequestForm.NRequestFormNo,NRequestForm.OldSerialNo,NRequestForm.CName as CName, NRequestForm.DoctorName as DoctorName,NRequestForm.DeptName as DeptName,NRequestForm.WebLisSourceOrgID, NRequestForm.WebLisSourceOrgName as WebLisSourceOrgName,NRequestForm.ClientNo,NRequestForm.jztype, NRequestForm.jztypeName,NRequestForm.PatNo,NRequestForm.ClientName as ClientName, NRequestForm.AgeUnitName as AgeUnitName,NRequestForm.Diag as Diag,GenderType.CName AS Sex,NRequestForm.Age as Age, SampleType.CName AS SampleName,Convert(varchar(10),NRequestForm.operdate,21)+' '+Convert(varchar(10), NRequestForm.opertime,8)as incepttime,Convert(varchar(10),NRequestForm.CollectDate,21)+' '+Convert(varchar(10), NRequestForm.CollectTime,8)as CollectTime,NRequestForm.OperDate as OperDate,NRequestForm.OperTime as OperTime, PatDiagInfo.DiagDesc as DiagDesc,NRequestForm.zdy5 FROM dbo.GenderType RIGHT OUTER JOIN dbo.NRequestForm LEFT OUTER JOIN dbo.PatDiagInfo ON dbo.NRequestForm.DiagNo = dbo.PatDiagInfo.DiagNo LEFT OUTER JOIN dbo.SampleType ON dbo.NRequestForm.SampleTypeNo = dbo.SampleType.SampleTypeNo ON dbo.GenderType.GenderNo = dbo.NRequestForm.GenderNo ");

                    //项目明细
                    //strSql.Append(" LEFT JOIN dbo.NRequestItem ON NRequestForm.NRequestFormNo = dbo.NRequestItem.NRequestFormNo LEFT JOIN dbo.BarCodeForm ON dbo.NRequestItem.BarCodeFormNo = dbo.BarCodeForm.BarCodeFormNo  ");

                    strSql.Append(" where ");
                    if (strwhere.ToString().Trim() == "")
                    {
                        strSql.Append(" 1=2");
                    }
                    else
                    {
                        strSql.Append(" 1=1 " + strwhere.ToString() + "   ");
                    }

                    //strSql.Append(" and NRequestForm.NRequestFormNo not in (select top 0 NRequestForm.NRequestFormNo FROM dbo.GenderType RIGHT OUTER JOIN dbo.NRequestForm LEFT OUTER JOIN dbo.PatDiagInfo ON dbo.NRequestForm.DiagNo = dbo.PatDiagInfo.DiagNo LEFT OUTER JOIN dbo.SampleType ON dbo.NRequestForm.SampleTypeNo = dbo.SampleType.SampleTypeNo ON dbo.GenderType.GenderNo = dbo.NRequestForm.GenderNo )");

                    strSql.Append(" group by NRequestForm.ZDY10,NRequestForm.NRequestFormNo,NRequestForm.OldSerialNo,NRequestForm.CName, NRequestForm.DoctorName,NRequestForm.DeptName,NRequestForm.WebLisSourceOrgID, NRequestForm.WebLisSourceOrgName,NRequestForm.ClientNo,NRequestForm.jztype, NRequestForm.jztypeName,NRequestForm.PatNo,NRequestForm.ClientName, NRequestForm.AgeUnitName,NRequestForm.Diag,GenderType.CName,NRequestForm.Age, SampleType.CName,Convert(varchar(10),NRequestForm.operdate,21)+' '+Convert(varchar(10), NRequestForm.opertime,8),Convert(varchar(10),NRequestForm.CollectDate,21)+' '+Convert(varchar(10), NRequestForm.CollectTime,8),NRequestForm.OperDate,NRequestForm.OperTime, PatDiagInfo.DiagDesc,NRequestForm.zdy5 ) aa");
                    //ZhiFang.Common.Log.Log.Info(strSql.ToString()); 
                }
                else
                {
                    return 0;
                }
                #endregion
            }
            else
            {
                strSql.Append("select count(*) FROM NRequestForm where 1=1  ");
            }
            //ZhiFang.Common.Log.Log.Info(strSql.ToString());
            string strCount = null;
            if (strSql.ToString() != "")
                strCount = idb.ExecuteScalar(strSql.ToString());
            if (strCount != null && strCount.Trim() != "")
            {
                return Convert.ToInt32(strCount.Trim());
            }
            else
            {
                return 0;
            }
        }

        private StringBuilder GetGetNRequstFormListByDetailsWhere(Model.NRequestForm model)
        {
            StringBuilder strwhere = new StringBuilder();
            #region where
            if (model.ClientNo != null)
            {
                strwhere.Append(" and NRequestForm.ClientNo='" + model.ClientNo + "' ");
            }

            if (model.TestTypeName != null)
            {
                strwhere.Append(" and NRequestForm.TestTypeName='" + model.TestTypeName + "' ");
            }

            if (model.CName != null)
            {
                strwhere.Append(" and NRequestForm.CName='" + model.CName + "' ");
            }

            if (model.DoctorName != null)
            {
                strwhere.Append(" and NRequestForm.DoctorName='" + model.DoctorName + "' ");
            }

            if (model.CollecterName != null)
            {
                strwhere.Append(" and NRequestForm.CollecterName='" + model.CollecterName + "' ");
            }

            if (model.SampleTypeName != null)
            {
                strwhere.Append(" and NRequestForm.SampleTypeName='" + model.SampleTypeName + "' ");
            }

            if (model.SerialNo != null)
            {
                strwhere.Append(" and NRequestForm.SerialNo='" + model.SerialNo + "' ");
            }

            if (model.ReceiveFlag != null)
            {
                strwhere.Append(" and NRequestForm.ReceiveFlag=" + model.ReceiveFlag + " ");
            }

            if (model.StatusNo != null)
            {
                strwhere.Append(" and NRequestForm.StatusNo=" + model.StatusNo + " ");
            }

            if (model.SampleTypeNo != null)
            {
                strwhere.Append(" and NRequestForm.SampleTypeNo=" + model.SampleTypeNo + " ");
            }

            if (model.PatNo != null)
            {
                strwhere.Append(" and NRequestForm.PatNo like '%" + model.PatNo + "%' ");
            }

            if (model.ClientName != null)
            {
                strwhere.Append(" and NRequestForm.ClientName='" + model.ClientName + "' ");
            }

            if (model.GenderNo != null)
            {
                strwhere.Append(" and NRequestForm.GenderNo=" + model.GenderNo + " ");
            }

            if (model.Birthday != null)
            {
                strwhere.Append(" and NRequestForm.Birthday='" + model.Birthday + "' ");
            }

            if (model.Age != null)
            {
                strwhere.Append(" and NRequestForm.Age=" + model.Age + " ");
            }

            if (model.AgeUnitNo != null)
            {
                strwhere.Append(" and NRequestForm.AgeUnitNo=" + model.AgeUnitNo + " ");
            }

            if (model.FolkNo != null)
            {
                strwhere.Append(" and NRequestForm.FolkNo=" + model.FolkNo + " ");
            }

            if (model.DistrictNo != null)
            {
                strwhere.Append(" and NRequestForm.DistrictNo=" + model.DistrictNo + " ");
            }

            if (model.WardNo != null)
            {
                strwhere.Append(" and NRequestForm.WardNo=" + model.WardNo + " ");
            }

            if (model.Bed != null)
            {
                strwhere.Append(" and NRequestForm.Bed='" + model.Bed + "' ");
            }

            if (model.DeptNo != null)
            {
                strwhere.Append(" and NRequestForm.DeptNo=" + model.DeptNo + " ");
            }

            if (model.Doctor != null)
            {
                strwhere.Append(" and NRequestForm.Doctor=" + model.Doctor + " ");
            }

            if (model.AgeUnitName != null)
            {
                strwhere.Append(" and NRequestForm.AgeUnitName='" + model.AgeUnitName + "' ");
            }

            if (model.DiagNo != null)
            {
                strwhere.Append(" and NRequestForm.DiagNo=" + model.DiagNo + " ");
            }

            if (model.Diag != null)
            {
                strwhere.Append(" and NRequestForm.Diag='" + model.Diag + "' ");
            }

            if (model.ChargeNo != null)
            {
                strwhere.Append(" and NRequestForm.ChargeNo=" + model.ChargeNo + " ");
            }

            if (model.Charge != null)
            {
                strwhere.Append(" and NRequestForm.Charge=" + model.Charge + " ");
            }

            if (model.Chargeflag != null)
            {
                strwhere.Append(" and NRequestForm.Chargeflag='" + model.Chargeflag + "' ");
            }

            if (model.CountNodesFormSource != null)
            {
                strwhere.Append(" and NRequestForm.CountNodesFormSource='" + model.CountNodesFormSource + "' ");
            }

            if (model.IsCheckFee != null)
            {
                strwhere.Append(" and NRequestForm.IsCheckFee=" + model.IsCheckFee + " ");
            }

            if (model.Operator != null)
            {
                strwhere.Append(" and NRequestForm.Operator='" + model.Operator + "' ");
            }

            if (model.OperDate != null)
            {
                strwhere.Append(" and NRequestForm.OperDate='" + model.OperDate + "' ");
            }

            if (model.OperTime != null)
            {
                strwhere.Append(" and NRequestForm.OperTime='" + model.OperTime + "' ");
            }

            if (model.GenderName != null)
            {
                strwhere.Append(" and NRequestForm.GenderName='" + model.GenderName + "' ");
            }

            if (model.FormMemo != null)
            {
                strwhere.Append(" and NRequestForm.FormMemo='" + model.FormMemo + "' ");
            }

            if (model.RequestSource != null)
            {
                strwhere.Append(" and NRequestForm.RequestSource='" + model.RequestSource + "' ");
            }

            if (model.SickOrder != null)
            {
                strwhere.Append(" and NRequestForm.SickOrder='" + model.SickOrder + "' ");
            }

            if (model.jztype != null)
            {
                strwhere.Append(" and NRequestForm.jztype=" + model.jztype + " ");
            }

            if (model.zdy1 != null)
            {
                strwhere.Append(" and NRequestForm.zdy1='" + model.zdy1 + "' ");
            }

            if (model.zdy2 != null)
            {
                strwhere.Append(" and NRequestForm.zdy2='" + model.zdy2 + "' ");
            }

            if (model.zdy3 != null)
            {
                strwhere.Append(" and NRequestForm.zdy3='" + model.zdy3 + "' ");
            }

            if (model.zdy4 != null)
            {
                strwhere.Append(" and NRequestForm.zdy4='" + model.zdy4 + "' ");
            }

            if (model.zdy5 != null)
            {
                strwhere.Append(" and NRequestForm.zdy5='" + model.zdy5 + "' ");
            }

            if (model.FlagDateDelete != null)
            {
                strwhere.Append(" and NRequestForm.FlagDateDelete='" + model.FlagDateDelete + "' ");
            }

            if (model.DeptName != null)
            {
                strwhere.Append(" and NRequestForm.DeptName='" + model.DeptName + "' ");
            }

            if (model.NurseFlag != null)
            {
                strwhere.Append(" and NRequestForm.NurseFlag=" + model.NurseFlag + " ");
            }

            if (model.IsByHand != null)
            {
                strwhere.Append(" and NRequestForm.IsByHand='" + model.IsByHand + "' ");
            }

            if (model.TestTypeNo != null)
            {
                strwhere.Append(" and NRequestForm.TestTypeNo=" + model.TestTypeNo + " ");
            }

            if (model.ExecDeptNo != null)
            {
                strwhere.Append(" and NRequestForm.ExecDeptNo=" + model.ExecDeptNo + " ");
            }

            if (model.CollectDate != null)
            {
                strwhere.Append(" and NRequestForm.CollectDate='" + model.CollectDate + "' ");
            }

            if (model.CollectTime != null)
            {
                strwhere.Append(" and NRequestForm.CollectTime='" + model.CollectTime + "' ");
            }

            if (model.Collecter != null)
            {
                strwhere.Append(" and NRequestForm.Collecter='" + model.Collecter + "' ");
            }

            if (model.LABCENTER != null)
            {
                strwhere.Append(" and NRequestForm.LABCENTER='" + model.LABCENTER + "' ");
            }

            if (model.NRequestFormNo != null)
            {
                strwhere.Append(" and NRequestForm.NRequestFormNo='" + model.NRequestFormNo + "' ");
            }

            if (model.CheckNo != null)
            {
                strwhere.Append(" and NRequestForm.CheckNo='" + model.CheckNo + "' ");
            }

            if (model.DistrictName != null)
            {
                strwhere.Append(" and NRequestForm.DistrictName='" + model.DistrictName + "' ");
            }

            if (model.CheckName != null)
            {
                strwhere.Append(" and NRequestForm.CheckName='" + model.CheckName + "' ");
            }

            if (model.WebLisSourceOrgID != null)
            {
                strwhere.Append(" and NRequestForm.WebLisSourceOrgID='" + model.WebLisSourceOrgID + "' ");
            }

            if (model.WebLisSourceOrgName != null)
            {
                strwhere.Append(" and NRequestForm.WebLisSourceOrgName='" + model.WebLisSourceOrgName + "' ");
            }

            if (model.WardName != null)
            {
                strwhere.Append(" and NRequestForm.WardName='" + model.WardName + "' ");
            }

            if (model.FolkName != null)
            {
                strwhere.Append(" and NRequestForm.FolkName='" + model.FolkName + "' ");
            }

            if (model.ClinicTypeName != null)
            {
                strwhere.Append(" and NRequestForm.ClinicTypeName='" + model.ClinicTypeName + "' ");
            }
            if (model.OperDateStart != null && model.OperDateStart != "")
            {
                strwhere.Append(" and NRequestForm.OperDate >='" + model.OperDateStart + "'  ");
            }
            if (model.OperDateEnd != null && model.OperDateEnd != "")
            {
                strwhere.Append(" and NRequestForm.OperDate <='" + model.OperDateEnd + "' ");
            }

            if (model.CollectDateStart != null)
            {
                strwhere.Append(" and NRequestForm.CollectDate >='" + model.CollectDateStart + "'  ");
            }
            if (model.CollectDateEnd != null)
            {
                strwhere.Append(" and NRequestForm.CollectDate <='" + model.CollectDateEnd + "'  ");
            }
            if (model.ClientList != null && model.ClientList.Trim() != "")
            {
                strwhere.Append(" and NRequestForm.ClientNo  in  (" + model.ClientList + ")  ");
            }

            ////添加按明细条码查询
            //if (model.BarCode != null && model.BarCode.Trim() != "")
            //{
            //    strwhere.Append(" and BarCodeForm.BarCode='" + model.BarCode + "'  ");
            //}

            if (model.OldSerialNo != null)
            {
                strwhere.Append(" and NRequestForm.OldSerialNo='" + model.OldSerialNo + "' ");
            }
            //消费码
            if (model.ZDY10 != null)
            {
                strwhere.Append(" and NRequestForm.ZDY10='" + model.ZDY10 + "' ");
            }
            //存项目明细的条码
            if (model.BarCode != null)
            {
                strwhere.Append(" and NRequestForm.BarCode like '%" + model.BarCode + "%' ");
            }
            #endregion
            return strwhere;
        }
        #endregion

        public int SendBarCodeFromByBarCodeList(string barCodeList, string userId, string employeeName)
        {
            StringBuilder strwhere = new StringBuilder();
            strwhere.Append(" update nrequestform set weblisflag=1, SenderName='" + employeeName + "' , SenderID=" + userId + " , SendDateTime ='" + DateTime.Now.ToString("yyyy-MM-dd") + "' ");
            strwhere.Append(" where nrequestformno in (select NRequestFormNo from       dbo.BarCodeForm INNER JOIN                   dbo.NRequestItem ON dbo.BarCodeForm.BarCodeFormNo = dbo.NRequestItem.BarCodeFormNo where  BarCodeForm.BarCode in ('" +barCodeList.Replace(",","','") + "')) and (weblisflag=0 or weblisflag is null ) ");
            ZhiFang.Common.Log.Log.Info("SendBarCodeFromByBarCodeList.nrequestform.sql:" + strwhere.ToString());
            string strCount = idb.ExecuteScalar(strwhere.ToString());
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
            strwhere.Append(" update nrequestform set weblisflag=2, DeliveryerName='" + employeeName + "' , DeliveryerID=" + userId + " , DeliveryDateTime ='" + DateTime.Now.ToString("yyyy-MM-dd") + "' ");
            strwhere.Append(" where nrequestformno in (select NRequestFormNo from       dbo.BarCodeForm INNER JOIN                   dbo.NRequestItem ON dbo.BarCodeForm.BarCodeFormNo = dbo.NRequestItem.BarCodeFormNo where  BarCodeForm.BarCode in ('" + barCodeList.Replace(",", "','") + "')) and (weblisflag=1 ) ");
            ZhiFang.Common.Log.Log.Info("DeliveryBarCodeFromByBarCodeList.nrequestform.sql:" + strwhere.ToString());
            string strCount = idb.ExecuteScalar(strwhere.ToString());
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
            string weblisflag = flag ?"5":"6";
            string strreason = "";
            if (!string.IsNullOrWhiteSpace(reason))
            {
                strreason = reason;
            }
            strwhere.Append(" update nrequestform set weblisflag="+ weblisflag + ", ReceipientName='" + employeeName + "' , ReceipientID=" + userId + " , ReceiveDateTime ='" + DateTime.Now.ToString("yyyy-MM-dd") + "' ,RejectionReason='"+ strreason + "' ,WebLisOrgID='" + weblisorgid + "'  ,WebLisOrgName='" + weblisorgname + "'");
            strwhere.Append(" where nrequestformno in (select NRequestFormNo from       dbo.BarCodeForm INNER JOIN                   dbo.NRequestItem ON dbo.BarCodeForm.BarCodeFormNo = dbo.NRequestItem.BarCodeFormNo where  BarCodeForm.BarCode in ('" + barCodeList.Replace(",", "','") + "')) and (weblisflag=2 ) ");
            ZhiFang.Common.Log.Log.Info("DeliveryBarCodeFromByBarCodeList.nrequestform.sql:" + strwhere.ToString());
            string strCount = idb.ExecuteScalar(strwhere.ToString());
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

