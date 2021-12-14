using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
using ZhiFang.Model;

namespace ZhiFang.DAL.Oracle.weblis
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
        public int Add(ZhiFang.Model.NRequestForm model)
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
                strSql2.Append("to_date('" + model.OperDate.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");
            }
            if (model.OperTime != null)
            {
                strSql1.Append("OperTime,");
                strSql2.Append("to_date('" + model.OperTime.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");
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
                strSql2.Append("to_date('" + model.FlagDateDelete.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");
            }
            if (model.NurseFlag != null)
            {
                strSql1.Append("NurseFlag,");
                strSql2.Append("" + model.NurseFlag + ",");
            }
            if (model.IsByHand != null)
            {
                strSql1.Append("IsByHand,");
                strSql2.Append("'" + (model.IsByHand) + "',");
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
                strSql2.Append("to_date('" + model.CollectDate.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");
            }
            if (model.CollectTime != null)
            {
                strSql1.Append("CollectTime,");
                strSql2.Append("to_date('" + model.CollectTime.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");
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
            if (model.PersonID != null)
            {
                strSql1.Append("PersonID,");
                strSql2.Append("'" + model.PersonID + "',");
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
            if (model.BarCode != null)
            {
                strSql1.Append("BarCode,");
                strSql2.Append("'" + model.BarCode + "',");
            }
            if (model.CombiItemName != null)
            {
                strSql1.Append("CombiItemName,");
                strSql2.Append("'" + model.CombiItemName + "',");
            }
            if (model.PrintTimes != null)
            {
                strSql1.Append("PrintTimes,");
                strSql2.Append("'" + model.PrintTimes + "',");
            }
            if (model.Price != null)
            {
                strSql1.Append("Price,");
                strSql2.Append("'" + model.Price + "',");
            }

            //if (model.BarCodeNo != null)
            //{
            //    strSql1.Append("BarCodeNo,");
            //    strSql2.Append("'" + model.BarCodeNo + "',");
            //}
            //if (model.WeblisFlag != null)
            //{
            //    strSql1.Append("WeblisFlag,");
            //    strSql2.Append("'" + model.WeblisFlag + "',");
            //}
            //if (model.FlagDateUpload != null)
            //{
            //    strSql1.Append("FlagDateUpload,");
            //    strSql2.Append("'" + model.FlagDateUpload + "',");
            //}
            //if (model.UploadFlag != null)
            //{
            //    strSql1.Append("UploadFlag,");
            //    strSql2.Append("" + model.UploadFlag + ",");
            //}
            //if (model.LoseInfo != null)
            //{
            //    strSql1.Append("LoseInfo,");
            //    strSql2.Append("'" + model.LoseInfo + "',");
            //}
            //if (model.AgeUnit != null)
            //{
            //    strSql1.Append("AgeUnit,");
            //    strSql2.Append("'" + model.AgeUnit + "',");
            //}
            //if (model.LABNREQUESTFORMNO != null)
            //{
            //    strSql1.Append("LABNREQUESTFORMNO,");
            //    strSql2.Append("'" + model.LABNREQUESTFORMNO + "',");
            //}
            //if (model.NFAgeUnitName != null)
            //{
            //    strSql1.Append("NFAgeUnitName,");
            //    strSql2.Append("'" + model.NFAgeUnitName + "',");
            //}
            //if (model.NFGenderName != null)
            //{
            //    strSql1.Append("NFGenderName,");
            //    strSql2.Append("'" + model.NFGenderName + "',");
            //}
            //if (model.NFDeptName != null)
            //{
            //    strSql1.Append("NFDeptName,");
            //    strSql2.Append("'" + model.NFDeptName + "',");
            //}
            //if (model.NFDistrictName != null)
            //{
            //    strSql1.Append("NFDistrictName,");
            //    strSql2.Append("'" + model.NFDistrictName + "',");
            //}
            //if (model.NFWardName != null)
            //{
            //    strSql1.Append("NFWardName,");
            //    strSql2.Append("'" + model.NFWardName + "',");
            //}
            //if (model.NFFolkName != null)
            //{
            //    strSql1.Append("NFFolkName,");
            //    strSql2.Append("'" + model.NFFolkName + "',");
            //}
            //if (model.NFSickTypeName != null)
            //{
            //    strSql1.Append("NFSickTypeName,");
            //    strSql2.Append("'" + model.NFSickTypeName + "',");
            //}
            //if (model.NFDoctorName != null)
            //{
            //    strSql1.Append("NFDoctorName,");
            //    strSql2.Append("'" + model.NFDoctorName + "',");
            //}
            //if (model.NFClientName != null)
            //{
            //    strSql1.Append("NFClientName,");
            //    strSql2.Append("'" + model.NFClientName + "',");
            //}
            //if (model.NFClientArea != null)
            //{
            //    strSql1.Append("NFClientArea,");
            //    strSql2.Append("'" + model.NFClientArea + "',");
            //}
            //if (model.NFClientStyle != null)
            //{
            //    strSql1.Append("NFClientStyle,");
            //    strSql2.Append("'" + model.NFClientStyle + "',");
            //}
            //if (model.NFClientType != null)
            //{
            //    strSql1.Append("NFClientType,");
            //    strSql2.Append("'" + model.NFClientType + "',");
            //}
            //if (model.NFbusinessname != null)
            //{
            //    strSql1.Append("NFbusinessname,");
            //    strSql2.Append("'" + model.NFbusinessname + "',");
            //}
            //if (model.NFtesttypename != null)
            //{
            //    strSql1.Append("NFtesttypename,");
            //    strSql2.Append("'" + model.NFtesttypename + "',");
            //}
            //if (model.NFsampletypename != null)
            //{
            //    strSql1.Append("NFsampletypename,");
            //    strSql2.Append("'" + model.NFsampletypename + "',");
            //}
            //if (model.LABCENTERNO != null)
            //{
            //    strSql1.Append("LABCENTERNO,");
            //    strSql2.Append("'" + model.LABCENTERNO + "',");
            //}
            //if (model.LABCENTERNAME != null)
            //{
            //    strSql1.Append("LABCENTERNAME,");
            //    strSql2.Append("'" + model.LABCENTERNAME + "',");
            //}
            //if (model.LABCLIENTNO != null)
            //{
            //    strSql1.Append("LABCLIENTNO,");
            //    strSql2.Append("'" + model.LABCLIENTNO + "',");
            //}
            //if (model.LABCLIENTNAME != null)
            //{
            //    strSql1.Append("LABCLIENTNAME,");
            //    strSql2.Append("'" + model.LABCLIENTNAME + "',");
            //}
            //if (model.LABDONO != null)
            //{
            //    strSql1.Append("LABDONO,");
            //    strSql2.Append("'" + model.LABDONO + "',");
            //}
            //if (model.LABDONAME != null)
            //{
            //    strSql1.Append("LABDONAME,");
            //    strSql2.Append("'" + model.LABDONAME + "',");
            //}
            //if (model.LABUPLOADDATE != null)
            //{
            //    strSql1.Append("LABUPLOADDATE,");
            //    strSql2.Append("'" + model.LABUPLOADDATE + "',");
            //}
            //if (model.AreaSendFlag != null)
            //{
            //    strSql1.Append("AreaSendFlag,");
            //    strSql2.Append("" + model.AreaSendFlag + ",");
            //}
            //if (model.AreaSendTime != null)
            //{
            //    strSql1.Append("AreaSendTime,");
            //    strSql2.Append("'" + model.AreaSendTime + "',");
            //}
            strSql.Append("insert into NRequestForm(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            return idb.ExecuteNonQuery(strSql.ToString());

            //try
            //{
            //    StringBuilder strSql = new StringBuilder();
            //    strSql.Append("insert into NRequestForm(");
            //    strSql.Append("ClientNo,TestTypeName,CName,DoctorName,CollecterName,SampleTypeName,SerialNo,ReceiveFlag,StatusNo,SampleTypeNo,PatNo,ClientName,GenderNo,Birthday,Age,AgeUnitNo,FolkNo,DistrictNo,WardNo,Bed,DeptNo,Doctor,AgeUnitName,DiagNo,Diag,ChargeNo,Charge,Chargeflag,CountNodesFormSource,IsCheckFee,Operator,OperDate,OperTime,GenderName,FormMemo,RequestSource,SickOrder,jztype,zdy1,zdy2,zdy3,zdy4,zdy5,FlagDateDelete,DeptName,NurseFlag,IsByHand,TestTypeNo,ExecDeptNo,CollectDate,CollectTime,Collecter,LABCENTER,NRequestFormNo,CheckNo,DistrictName,CheckName,WebLisSourceOrgID,WebLisSourceOrgName,WardName,FolkName,ClinicTypeName");
            //    strSql.Append(") values (");
            //    strSql.Append("@ClientNo,@TestTypeName,@CName,@DoctorName,@CollecterName,@SampleTypeName,@SerialNo,@ReceiveFlag,@StatusNo,@SampleTypeNo,@PatNo,@ClientName,@GenderNo,@Birthday,@Age,@AgeUnitNo,@FolkNo,@DistrictNo,@WardNo,@Bed,@DeptNo,@Doctor,@AgeUnitName,@DiagNo,@Diag,@ChargeNo,@Charge,@Chargeflag,@CountNodesFormSource,@IsCheckFee,@Operator,@OperDate,@OperTime,@GenderName,@FormMemo,@RequestSource,@SickOrder,@jztype,@zdy1,@zdy2,@zdy3,@zdy4,@zdy5,@FlagDateDelete,@DeptName,@NurseFlag,@IsByHand,@TestTypeNo,@ExecDeptNo,@CollectDate,@CollectTime,@Collecter,@LABCENTER,@NRequestFormNo,@CheckNo,@DistrictName,@CheckName,@WebLisSourceOrgID,@WebLisSourceOrgName,@WardName,@FolkName,@ClinicTypeName");
            //    strSql.Append(") ");

            //    SqlParameter[] parameters = {
            //            new SqlParameter("@ClientNo", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@TestTypeName", SqlDbType.VarChar,10) ,            
            //            new SqlParameter("@CName", SqlDbType.VarChar,30) ,            
            //            new SqlParameter("@DoctorName", SqlDbType.VarChar,10) ,            
            //            new SqlParameter("@CollecterName", SqlDbType.VarChar,10) ,            
            //            new SqlParameter("@SampleTypeName", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@SerialNo", SqlDbType.VarChar,30) ,            
            //            new SqlParameter("@ReceiveFlag", SqlDbType.Int,4) ,            
            //            new SqlParameter("@StatusNo", SqlDbType.Int,4) ,            
            //            new SqlParameter("@SampleTypeNo", SqlDbType.Int,4) ,            
            //            new SqlParameter("@PatNo", SqlDbType.VarChar,20) ,            
            //            new SqlParameter("@ClientName", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@GenderNo", SqlDbType.Int,4) ,            
            //            new SqlParameter("@Birthday", SqlDbType.DateTime) ,            
            //            new SqlParameter("@Age", SqlDbType.Float,8) ,            
            //            new SqlParameter("@AgeUnitNo", SqlDbType.Int,4) ,            
            //            new SqlParameter("@FolkNo", SqlDbType.Int,4) ,            
            //            new SqlParameter("@DistrictNo", SqlDbType.Int,4) ,            
            //            new SqlParameter("@WardNo", SqlDbType.Int,4) ,            
            //            new SqlParameter("@Bed", SqlDbType.VarChar,10) ,            
            //            new SqlParameter("@DeptNo", SqlDbType.Int,4) ,            
            //            new SqlParameter("@Doctor", SqlDbType.Int,4) ,            
            //            new SqlParameter("@AgeUnitName", SqlDbType.VarChar,10) ,            
            //            new SqlParameter("@DiagNo", SqlDbType.Int,4) ,            
            //            new SqlParameter("@Diag", SqlDbType.VarChar,100) ,            
            //            new SqlParameter("@ChargeNo", SqlDbType.Int,4) ,            
            //            new SqlParameter("@Charge", SqlDbType.Money,8) ,            
            //            new SqlParameter("@Chargeflag", SqlDbType.VarChar,10) ,            
            //            new SqlParameter("@CountNodesFormSource", SqlDbType.Char,1) ,            
            //            new SqlParameter("@IsCheckFee", SqlDbType.Int,4) ,            
            //            new SqlParameter("@Operator", SqlDbType.VarChar,20) ,            
            //            new SqlParameter("@OperDate", SqlDbType.DateTime) ,            
            //            new SqlParameter("@OperTime", SqlDbType.DateTime) ,            
            //            new SqlParameter("@GenderName", SqlDbType.VarChar,10) ,            
            //            new SqlParameter("@FormMemo", SqlDbType.VarChar,40) ,            
            //            new SqlParameter("@RequestSource", SqlDbType.VarChar,20) ,            
            //            new SqlParameter("@SickOrder", SqlDbType.VarChar,20) ,            
            //            new SqlParameter("@jztype", SqlDbType.Int,4) ,            
            //            new SqlParameter("@zdy1", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@zdy2", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@zdy3", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@zdy4", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@zdy5", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@FlagDateDelete", SqlDbType.DateTime) ,            
            //            new SqlParameter("@DeptName", SqlDbType.VarChar,40) ,            
            //            new SqlParameter("@NurseFlag", SqlDbType.Int,4) ,            
            //            new SqlParameter("@IsByHand", SqlDbType.Bit,1) ,            
            //            new SqlParameter("@TestTypeNo", SqlDbType.Int,4) ,            
            //            new SqlParameter("@ExecDeptNo", SqlDbType.Int,4) ,            
            //            new SqlParameter("@CollectDate", SqlDbType.DateTime) ,            
            //            new SqlParameter("@CollectTime", SqlDbType.DateTime) ,            
            //            new SqlParameter("@Collecter", SqlDbType.VarChar,10) ,            
            //            new SqlParameter("@LABCENTER", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@NRequestFormNo", SqlDbType.BigInt,8) ,            
            //            new SqlParameter("@CheckNo", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@DistrictName", SqlDbType.VarChar,20) ,            
            //            new SqlParameter("@CheckName", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@WebLisSourceOrgID", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@WebLisSourceOrgName", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@WardName", SqlDbType.VarChar,40) ,            
            //            new SqlParameter("@FolkName", SqlDbType.VarChar,20) ,            
            //            new SqlParameter("@ClinicTypeName", SqlDbType.VarChar,20),

            //};

            //    parameters[0].Value = model.ClientNo;
            //    parameters[1].Value = model.TestTypeName;
            //    parameters[2].Value = model.CName;
            //    parameters[3].Value = model.DoctorName;
            //    parameters[4].Value = model.CollecterName;
            //    parameters[5].Value = model.SampleTypeName;
            //    parameters[6].Value = model.SerialNo;
            //    parameters[7].Value = model.ReceiveFlag;
            //    parameters[8].Value = model.StatusNo;
            //    parameters[9].Value = model.SampleTypeNo;
            //    parameters[10].Value = model.PatNo;
            //    parameters[11].Value = model.ClientName;
            //    parameters[12].Value = model.GenderNo;
            //    parameters[13].Value = model.Birthday;
            //    parameters[14].Value = model.Age;
            //    parameters[15].Value = model.AgeUnitNo;
            //    parameters[16].Value = model.FolkNo;
            //    parameters[17].Value = model.DistrictNo;
            //    parameters[18].Value = model.WardNo;
            //    parameters[19].Value = model.Bed;
            //    parameters[20].Value = model.DeptNo;
            //    parameters[21].Value = model.Doctor;
            //    parameters[22].Value = model.AgeUnitName;
            //    parameters[23].Value = model.DiagNo;
            //    parameters[24].Value = model.Diag;
            //    parameters[25].Value = model.ChargeNo;
            //    parameters[26].Value = model.Charge;
            //    parameters[27].Value = model.Chargeflag;
            //    parameters[28].Value = model.CountNodesFormSource;
            //    parameters[29].Value = model.IsCheckFee;
            //    parameters[30].Value = model.Operator;
            //    parameters[31].Value = model.OperDate;
            //    parameters[32].Value = model.OperTime;
            //    parameters[33].Value = model.GenderName;
            //    parameters[34].Value = model.FormMemo;
            //    parameters[35].Value = model.RequestSource;
            //    parameters[36].Value = model.SickOrder;
            //    parameters[37].Value = model.jztype;
            //    parameters[38].Value = model.zdy1;
            //    parameters[39].Value = model.zdy2;
            //    parameters[40].Value = model.zdy3;
            //    parameters[41].Value = model.zdy4;
            //    parameters[42].Value = model.zdy5;
            //    parameters[43].Value = model.FlagDateDelete;
            //    parameters[44].Value = model.DeptName;
            //    parameters[45].Value = model.NurseFlag;
            //    parameters[46].Value = model.IsByHand;
            //    parameters[47].Value = model.TestTypeNo;
            //    parameters[48].Value = model.ExecDeptNo;
            //    parameters[49].Value = model.CollectDate;
            //    parameters[50].Value = model.CollectTime;
            //    parameters[51].Value = model.Collecter;
            //    parameters[52].Value = model.LABCENTER;
            //    parameters[53].Value = model.NRequestFormNo;
            //    parameters[54].Value = model.CheckNo;
            //    parameters[55].Value = model.DistrictName;
            //    parameters[56].Value = model.CheckName;
            //    parameters[57].Value = model.WebLisSourceOrgID;
            //    parameters[58].Value = model.WebLisSourceOrgName;
            //    parameters[59].Value = model.WardName;
            //    parameters[60].Value = model.FolkName;
            //    parameters[61].Value = model.ClinicTypeName;
            //    Common.Log.Log.Info(strSql.ToString());

            //    return idb.ExecuteNonQuery(strSql.ToString(), parameters);
            //}
            //catch (Exception e)
            //{
            //    Common.Log.Log.Debug("错误信息：" + e.ToString() + "字符串连接:" + idb.ConnectionString);
            //    return 0;
            //}
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.NRequestForm model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update NRequestForm set ");
            if (model.Weblisflag != null)
            {
                strSql.Append("Weblisflag='" + model.Weblisflag + "',");
            }
            if (model.ClientNo != null)
            {
                strSql.Append("ClientNo='" + model.ClientNo + "',");
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
            if (model.DistrictName != null)
            {
                strSql.Append("DistrictName='" + model.DistrictName + "',");
            }
            if (model.WardName != null)
            {
                strSql.Append("WardName='" + model.WardName + "',");
            }
            if (model.FolkName != null)
            {
                strSql.Append("FolkName='" + model.FolkName + "',");
            }
            if (model.ClinicTypeName != null)
            {
                strSql.Append("ClinicTypeName='" + model.ClinicTypeName + "',");
            }
            if (model.TestTypeName != null)
            {
                strSql.Append("TestTypeName='" + model.TestTypeName + "',");
            }
            if (model.CName != null)
            {
                strSql.Append("CName='" + model.CName + "',");
            }
            if (model.DoctorName != null)
            {
                strSql.Append("DoctorName='" + model.DoctorName + "',");
            }
            if (model.CollecterName != null)
            {
                strSql.Append("CollecterName='" + model.CollecterName + "',");
            }
            if (model.SampleTypeName != null)
            {
                strSql.Append("SampleTypeName='" + model.SampleTypeName + "',");
            }
            if (model.SerialNo != null)
            {
                strSql.Append("SerialNo='" + model.SerialNo + "',");
            }
            if (model.ReceiveFlag != null)
            {
                strSql.Append("ReceiveFlag=" + model.ReceiveFlag + ",");
            }
            if (model.StatusNo != null)
            {
                strSql.Append("StatusNo=" + model.StatusNo + ",");
            }
            if (model.SampleTypeNo != null)
            {
                strSql.Append("SampleTypeNo=" + model.SampleTypeNo + ",");
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
            if (model.Birthday != null)
            {
                strSql.Append("Birthday='" + model.Birthday + "',");
            }
            if (model.Age != null)
            {
                strSql.Append("Age=" + model.Age + ",");
            }
            if (model.AgeUnitNo != null)
            {
                strSql.Append("AgeUnitNo=" + model.AgeUnitNo + ",");
            }
            if (model.FolkNo != null)
            {
                strSql.Append("FolkNo=" + model.FolkNo + ",");
            }
            if (model.DistrictNo != null)
            {
                strSql.Append("DistrictNo=" + model.DistrictNo + ",");
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
            if (model.DeptNo != null)
            {
                strSql.Append("DeptNo=" + model.DeptNo + ",");
            }
            if (model.Doctor != null)
            {
                strSql.Append("Doctor=" + model.Doctor + ",");
            }
            if (model.DiagNo != null)
            {
                strSql.Append("DiagNo=" + model.DiagNo + ",");
            }
            if (model.Diag != null)
            {
                strSql.Append("Diag='" + model.Diag + "',");
            }
            if (model.ChargeNo != null)
            {
                strSql.Append("ChargeNo=" + model.ChargeNo + ",");
            }
            if (model.Charge != null)
            {
                strSql.Append("Charge=" + model.Charge + ",");
            }
            if (model.Chargeflag != null)
            {
                strSql.Append("Chargeflag='" + model.Chargeflag + "',");
            }
            if (model.CountNodesFormSource != null)
            {
                strSql.Append("CountNodesFormSource='" + model.CountNodesFormSource + "',");
            }
            if (model.IsCheckFee != null)
            {
                strSql.Append("IsCheckFee=" + model.IsCheckFee + ",");
            }
            if (model.Operator != null)
            {
                strSql.Append("Operator='" + model.Operator + "',");
            }
            if (model.OperDate != null)
            {
                strSql.Append("OperDate=to_date('" + model.OperDate.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");
            }
            if (model.OperTime != null)
            {
                strSql.Append("OperTime=to_date('" + model.OperTime.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");
            }
            if (model.FormMemo != null)
            {
                strSql.Append("FormMemo='" + model.FormMemo + "',");
            }
            if (model.RequestSource != null)
            {
                strSql.Append("RequestSource='" + model.RequestSource + "',");
            }
            if (model.SickOrder != null)
            {
                strSql.Append("SickOrder='" + model.SickOrder + "',");
            }
            if (model.jztype != null)
            {
                strSql.Append("jztype=" + model.jztype + ",");
            }
            if (model.zdy1 != null)
            {
                strSql.Append("zdy1='" + model.zdy1 + "',");
            }
            if (model.zdy2 != null)
            {
                strSql.Append("zdy2='" + model.zdy2 + "',");
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
            if (model.zdy5 != null)
            {
                strSql.Append("zdy5='" + model.zdy5 + "',");
            }
            if (model.FlagDateDelete != null)
            {
                strSql.Append("FlagDateDelete='" + model.FlagDateDelete + "',");
            }
            if (model.NurseFlag != null)
            {
                strSql.Append("NurseFlag=" + model.NurseFlag + ",");
            }
            if (model.IsByHand != null)
            {
                strSql.Append("IsByHand=" + (model.IsByHand) + ",");
            }
            if (model.TestTypeNo != null)
            {
                strSql.Append("TestTypeNo=" + model.TestTypeNo + ",");
            }
            if (model.ExecDeptNo != null)
            {
                strSql.Append("ExecDeptNo=" + model.ExecDeptNo + ",");
            }
            if (model.CollectDate != null)
            {
                strSql.Append("CollectDate=to_date('" + model.CollectDate.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");
            }
            if (model.CollectTime != null)
            {
                strSql.Append("CollectTime=to_date('" + model.CollectTime.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");
            }
            if (model.Collecter != null)
            {
                strSql.Append("Collecter='" + model.Collecter + "',");
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
            if (model.CheckName != null)
            {
                strSql.Append("CheckName='" + model.CheckName + "',");
            }
            if (model.WebLisSourceOrgID != null)
            {
                strSql.Append("WebLisSourceOrgID='" + model.WebLisSourceOrgID + "',");
            }
            if (model.WebLisSourceOrgName != null)
            {
                strSql.Append("WebLisSourceOrgName='" + model.WebLisSourceOrgName + "',");
            }
            if (model.OldSerialNo != null)
            {
                strSql.Append("OldSerialNo='" + model.OldSerialNo + "',");
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



            //if (model.BarCodeNo != null)
            //{
            //    strSql.Append("BarCodeNo='" + model.BarCodeNo + "',");
            //}
            //else
            //{
            //    strSql.Append("BarCodeNo= null ,");
            //}
            //if (model.WeblisFlag != null)
            //{
            //    strSql.Append("WeblisFlag='" + model.WeblisFlag + "',");
            //}
            //else
            //{
            //    strSql.Append("WeblisFlag= null ,");
            //}
            //if (model.FlagDateUpload != null)
            //{
            //    strSql.Append("FlagDateUpload='" + model.FlagDateUpload + "',");
            //}
            //else
            //{
            //    strSql.Append("FlagDateUpload= null ,");
            //}
            //if (model.UploadFlag != null)
            //{
            //    strSql.Append("UploadFlag=" + model.UploadFlag + ",");
            //}
            //else
            //{
            //    strSql.Append("UploadFlag= null ,");
            //}
            //if (model.LoseInfo != null)
            //{
            //    strSql.Append("LoseInfo='" + model.LoseInfo + "',");
            //}
            //else
            //{
            //    strSql.Append("LoseInfo= null ,");
            //}
            //if (model.AgeUnit != null)
            //{
            //    strSql.Append("AgeUnit='" + model.AgeUnit + "',");
            //}
            //else
            //{
            //    strSql.Append("AgeUnit= null ,");
            //}
            //if (model.LABNREQUESTFORMNO != null)
            //{
            //    strSql.Append("LABNREQUESTFORMNO='" + model.LABNREQUESTFORMNO + "',");
            //}
            //else
            //{
            //    strSql.Append("LABNREQUESTFORMNO= null ,");
            //}
            //if (model.NFAgeUnitName != null)
            //{
            //    strSql.Append("NFAgeUnitName='" + model.NFAgeUnitName + "',");
            //}
            //else
            //{
            //    strSql.Append("NFAgeUnitName= null ,");
            //}
            //if (model.NFGenderName != null)
            //{
            //    strSql.Append("NFGenderName='" + model.NFGenderName + "',");
            //}
            //else
            //{
            //    strSql.Append("NFGenderName= null ,");
            //}
            //if (model.NFDeptName != null)
            //{
            //    strSql.Append("NFDeptName='" + model.NFDeptName + "',");
            //}
            //else
            //{
            //    strSql.Append("NFDeptName= null ,");
            //}
            //if (model.NFDistrictName != null)
            //{
            //    strSql.Append("NFDistrictName='" + model.NFDistrictName + "',");
            //}
            //else
            //{
            //    strSql.Append("NFDistrictName= null ,");
            //}
            //if (model.NFWardName != null)
            //{
            //    strSql.Append("NFWardName='" + model.NFWardName + "',");
            //}
            //else
            //{
            //    strSql.Append("NFWardName= null ,");
            //}
            //if (model.NFFolkName != null)
            //{
            //    strSql.Append("NFFolkName='" + model.NFFolkName + "',");
            //}
            //else
            //{
            //    strSql.Append("NFFolkName= null ,");
            //}
            //if (model.NFSickTypeName != null)
            //{
            //    strSql.Append("NFSickTypeName='" + model.NFSickTypeName + "',");
            //}
            //else
            //{
            //    strSql.Append("NFSickTypeName= null ,");
            //}
            //if (model.NFDoctorName != null)
            //{
            //    strSql.Append("NFDoctorName='" + model.NFDoctorName + "',");
            //}
            //else
            //{
            //    strSql.Append("NFDoctorName= null ,");
            //}
            //if (model.NFClientName != null)
            //{
            //    strSql.Append("NFClientName='" + model.NFClientName + "',");
            //}
            //else
            //{
            //    strSql.Append("NFClientName= null ,");
            //}
            //if (model.NFClientArea != null)
            //{
            //    strSql.Append("NFClientArea='" + model.NFClientArea + "',");
            //}
            //else
            //{
            //    strSql.Append("NFClientArea= null ,");
            //}
            //if (model.NFClientStyle != null)
            //{
            //    strSql.Append("NFClientStyle='" + model.NFClientStyle + "',");
            //}
            //else
            //{
            //    strSql.Append("NFClientStyle= null ,");
            //}
            //if (model.NFClientType != null)
            //{
            //    strSql.Append("NFClientType='" + model.NFClientType + "',");
            //}
            //else
            //{
            //    strSql.Append("NFClientType= null ,");
            //}
            //if (model.NFbusinessname != null)
            //{
            //    strSql.Append("NFbusinessname='" + model.NFbusinessname + "',");
            //}
            //else
            //{
            //    strSql.Append("NFbusinessname= null ,");
            //}
            //if (model.NFtesttypename != null)
            //{
            //    strSql.Append("NFtesttypename='" + model.NFtesttypename + "',");
            //}
            //else
            //{
            //    strSql.Append("NFtesttypename= null ,");
            //}
            //if (model.NFsampletypename != null)
            //{
            //    strSql.Append("NFsampletypename='" + model.NFsampletypename + "',");
            //}
            //else
            //{
            //    strSql.Append("NFsampletypename= null ,");
            //}
            //if (model.LABCENTERNO != null)
            //{
            //    strSql.Append("LABCENTERNO='" + model.LABCENTERNO + "',");
            //}
            //else
            //{
            //    strSql.Append("LABCENTERNO= null ,");
            //}
            //if (model.LABCENTERNAME != null)
            //{
            //    strSql.Append("LABCENTERNAME='" + model.LABCENTERNAME + "',");
            //}
            //else
            //{
            //    strSql.Append("LABCENTERNAME= null ,");
            //}
            //if (model.LABCLIENTNO != null)
            //{
            //    strSql.Append("LABCLIENTNO='" + model.LABCLIENTNO + "',");
            //}
            //else
            //{
            //    strSql.Append("LABCLIENTNO= null ,");
            //}
            //if (model.LABCLIENTNAME != null)
            //{
            //    strSql.Append("LABCLIENTNAME='" + model.LABCLIENTNAME + "',");
            //}
            //else
            //{
            //    strSql.Append("LABCLIENTNAME= null ,");
            //}
            //if (model.LABDONO != null)
            //{
            //    strSql.Append("LABDONO='" + model.LABDONO + "',");
            //}
            //else
            //{
            //    strSql.Append("LABDONO= null ,");
            //}
            //if (model.LABDONAME != null)
            //{
            //    strSql.Append("LABDONAME='" + model.LABDONAME + "',");
            //}
            //else
            //{
            //    strSql.Append("LABDONAME= null ,");
            //}
            //if (model.LABUPLOADDATE != null)
            //{
            //    strSql.Append("LABUPLOADDATE='" + model.LABUPLOADDATE + "',");
            //}
            //else
            //{
            //    strSql.Append("LABUPLOADDATE= null ,");
            //}
            //if (model.AreaSendFlag != null)
            //{
            //    strSql.Append("AreaSendFlag=" + model.AreaSendFlag + ",");
            //}
            //else
            //{
            //    strSql.Append("AreaSendFlag= null ,");
            //}
            //if (model.AreaSendTime != null)
            //{
            //    strSql.Append("AreaSendTime='" + model.AreaSendTime + "',");
            //}
            //else
            //{
            //    strSql.Append("AreaSendTime= null ,");
            //}
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where NRequestFormNo=" + model.NRequestFormNo + " ");

            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("update NRequestForm set ");

            //strSql.Append(" ClientNo = @ClientNo , ");
            //strSql.Append(" TestTypeName = @TestTypeName , ");
            //strSql.Append(" CName = @CName , ");
            //strSql.Append(" DoctorName = @DoctorName , ");
            //strSql.Append(" CollecterName = @CollecterName , ");
            //strSql.Append(" SampleTypeName = @SampleTypeName , ");
            //strSql.Append(" SerialNo = @SerialNo , ");
            //strSql.Append(" ReceiveFlag = @ReceiveFlag , ");
            //strSql.Append(" StatusNo = @StatusNo , ");
            //strSql.Append(" SampleTypeNo = @SampleTypeNo , ");
            //strSql.Append(" PatNo = @PatNo , ");
            //strSql.Append(" ClientName = @ClientName , ");
            //strSql.Append(" GenderNo = @GenderNo , ");
            //strSql.Append(" Birthday = @Birthday , ");
            //strSql.Append(" Age = @Age , ");
            //strSql.Append(" AgeUnitNo = @AgeUnitNo , ");
            //strSql.Append(" FolkNo = @FolkNo , ");
            //strSql.Append(" DistrictNo = @DistrictNo , ");
            //strSql.Append(" WardNo = @WardNo , ");
            //strSql.Append(" Bed = @Bed , ");
            //strSql.Append(" DeptNo = @DeptNo , ");
            //strSql.Append(" Doctor = @Doctor , ");
            //strSql.Append(" AgeUnitName = @AgeUnitName , ");
            //strSql.Append(" DiagNo = @DiagNo , ");
            //strSql.Append(" Diag = @Diag , ");
            //strSql.Append(" ChargeNo = @ChargeNo , ");
            //strSql.Append(" Charge = @Charge , ");
            //strSql.Append(" Chargeflag = @Chargeflag , ");
            //strSql.Append(" CountNodesFormSource = @CountNodesFormSource , ");
            //strSql.Append(" IsCheckFee = @IsCheckFee , ");
            //strSql.Append(" Operator = @Operator , ");
            //strSql.Append(" OperDate = @OperDate , ");
            //strSql.Append(" OperTime = @OperTime , ");
            //strSql.Append(" GenderName = @GenderName , ");
            //strSql.Append(" FormMemo = @FormMemo , ");
            //strSql.Append(" RequestSource = @RequestSource , ");
            //strSql.Append(" SickOrder = @SickOrder , ");
            //strSql.Append(" jztype = @jztype , ");
            //strSql.Append(" zdy1 = @zdy1 , ");
            //strSql.Append(" zdy2 = @zdy2 , ");
            //strSql.Append(" zdy3 = @zdy3 , ");
            //strSql.Append(" zdy4 = @zdy4 , ");
            //strSql.Append(" zdy5 = @zdy5 , ");
            //strSql.Append(" FlagDateDelete = @FlagDateDelete , ");
            //strSql.Append(" DeptName = @DeptName , ");
            //strSql.Append(" NurseFlag = @NurseFlag , ");
            //strSql.Append(" IsByHand = @IsByHand , ");
            //strSql.Append(" TestTypeNo = @TestTypeNo , ");
            //strSql.Append(" ExecDeptNo = @ExecDeptNo , ");
            //strSql.Append(" CollectDate = @CollectDate , ");
            //strSql.Append(" CollectTime = @CollectTime , ");
            //strSql.Append(" Collecter = @Collecter , ");
            //strSql.Append(" LABCENTER = @LABCENTER , ");
            //strSql.Append(" NRequestFormNo = @NRequestFormNo , ");
            //strSql.Append(" CheckNo = @CheckNo , ");
            //strSql.Append(" DistrictName = @DistrictName , ");
            //strSql.Append(" CheckName = @CheckName , ");
            //strSql.Append(" WebLisSourceOrgID = @WebLisSourceOrgID , ");
            //strSql.Append(" WebLisSourceOrgName = @WebLisSourceOrgName , ");
            //strSql.Append(" WardName = @WardName , ");
            //strSql.Append(" FolkName = @FolkName , ");
            //strSql.Append(" ClinicTypeName = @ClinicTypeName  ");
            //strSql.Append(" where NRequestFormNo=@NRequestFormNo  ");

            //SqlParameter[] parameters = {

            //new SqlParameter("@ClientNo", SqlDbType.VarChar,50) ,            	

            //new SqlParameter("@TestTypeName", SqlDbType.VarChar,10) ,            	

            //new SqlParameter("@CName", SqlDbType.VarChar,30) ,            	

            //new SqlParameter("@DoctorName", SqlDbType.VarChar,10) ,            	

            //new SqlParameter("@CollecterName", SqlDbType.VarChar,10) ,            	

            //new SqlParameter("@SampleTypeName", SqlDbType.VarChar,50) ,            	

            //new SqlParameter("@SerialNo", SqlDbType.VarChar,30) ,            	

            //new SqlParameter("@ReceiveFlag", SqlDbType.Int,4) ,            	

            //new SqlParameter("@StatusNo", SqlDbType.Int,4) ,            	

            //new SqlParameter("@SampleTypeNo", SqlDbType.Int,4) ,            	

            //new SqlParameter("@PatNo", SqlDbType.VarChar,20) ,            	

            //new SqlParameter("@ClientName", SqlDbType.VarChar,50) ,            	

            //new SqlParameter("@GenderNo", SqlDbType.Int,4) ,            	

            //new SqlParameter("@Birthday", SqlDbType.DateTime) ,            	

            //new SqlParameter("@Age", SqlDbType.Float,8) ,            	

            //new SqlParameter("@AgeUnitNo", SqlDbType.Int,4) ,            	

            //new SqlParameter("@FolkNo", SqlDbType.Int,4) ,            	

            //new SqlParameter("@DistrictNo", SqlDbType.Int,4) ,            	

            //new SqlParameter("@WardNo", SqlDbType.Int,4) ,            	

            //new SqlParameter("@Bed", SqlDbType.VarChar,10) ,            	

            //new SqlParameter("@DeptNo", SqlDbType.Int,4) ,            	

            //new SqlParameter("@Doctor", SqlDbType.Int,4) ,            	

            //new SqlParameter("@AgeUnitName", SqlDbType.VarChar,10) ,            	

            //new SqlParameter("@DiagNo", SqlDbType.Int,4) ,            	

            //new SqlParameter("@Diag", SqlDbType.VarChar,100) ,            	

            //new SqlParameter("@ChargeNo", SqlDbType.Int,4) ,            	

            //new SqlParameter("@Charge", SqlDbType.Money,8) ,            	

            //new SqlParameter("@Chargeflag", SqlDbType.VarChar,10) ,            	

            //new SqlParameter("@CountNodesFormSource", SqlDbType.Char,1) ,            	

            //new SqlParameter("@IsCheckFee", SqlDbType.Int,4) ,            	

            //new SqlParameter("@Operator", SqlDbType.VarChar,20) ,            	

            //new SqlParameter("@OperDate", SqlDbType.DateTime) ,            	

            //new SqlParameter("@OperTime", SqlDbType.DateTime) ,            	

            //new SqlParameter("@GenderName", SqlDbType.VarChar,10) ,            	

            //new SqlParameter("@FormMemo", SqlDbType.VarChar,40) ,            	

            //new SqlParameter("@RequestSource", SqlDbType.VarChar,20) ,            	

            //new SqlParameter("@SickOrder", SqlDbType.VarChar,20) ,            	

            //new SqlParameter("@jztype", SqlDbType.Int,4) ,            	

            //new SqlParameter("@zdy1", SqlDbType.VarChar,50) ,            	

            //new SqlParameter("@zdy2", SqlDbType.VarChar,50) ,            	

            //new SqlParameter("@zdy3", SqlDbType.VarChar,50) ,            	

            //new SqlParameter("@zdy4", SqlDbType.VarChar,50) ,            	

            //new SqlParameter("@zdy5", SqlDbType.VarChar,50) ,            	

            //new SqlParameter("@FlagDateDelete", SqlDbType.DateTime) ,            	

            //new SqlParameter("@DeptName", SqlDbType.VarChar,40) ,            	

            //new SqlParameter("@NurseFlag", SqlDbType.Int,4) ,            	

            //new SqlParameter("@IsByHand", SqlDbType.Bit,1) ,            	

            //new SqlParameter("@TestTypeNo", SqlDbType.Int,4) ,            	

            //new SqlParameter("@ExecDeptNo", SqlDbType.Int,4) ,            	

            //new SqlParameter("@CollectDate", SqlDbType.DateTime) ,            	

            //new SqlParameter("@CollectTime", SqlDbType.DateTime) ,            	

            //new SqlParameter("@Collecter", SqlDbType.VarChar,10) ,            	

            //new SqlParameter("@LABCENTER", SqlDbType.VarChar,50) ,            	

            //new SqlParameter("@NRequestFormNo", SqlDbType.BigInt,8) ,            	

            //new SqlParameter("@CheckNo", SqlDbType.VarChar,50) ,            	

            //new SqlParameter("@DistrictName", SqlDbType.VarChar,20) ,            	

            //new SqlParameter("@CheckName", SqlDbType.VarChar,50) ,            	

            //new SqlParameter("@WebLisSourceOrgID", SqlDbType.VarChar,50) ,            	

            //new SqlParameter("@WebLisSourceOrgName", SqlDbType.VarChar,50) ,            	

            //new SqlParameter("@WardName", SqlDbType.VarChar,40) ,            	

            //new SqlParameter("@FolkName", SqlDbType.VarChar,20) ,            	

            //new SqlParameter("@ClinicTypeName", SqlDbType.VarChar,20)             	

            //};


            //if (model.ClientNo != null)
            //{
            //    parameters[0].Value = model.ClientNo;
            //}



            //if (model.TestTypeName != null)
            //{
            //    parameters[1].Value = model.TestTypeName;
            //}



            //if (model.CName != null)
            //{
            //    parameters[2].Value = model.CName;
            //}



            //if (model.DoctorName != null)
            //{
            //    parameters[3].Value = model.DoctorName;
            //}



            //if (model.CollecterName != null)
            //{
            //    parameters[4].Value = model.CollecterName;
            //}



            //if (model.SampleTypeName != null)
            //{
            //    parameters[5].Value = model.SampleTypeName;
            //}



            //if (model.SerialNo != null)
            //{
            //    parameters[6].Value = model.SerialNo;
            //}



            //if (model.ReceiveFlag != null)
            //{
            //    parameters[7].Value = model.ReceiveFlag;
            //}



            //if (model.StatusNo != null)
            //{
            //    parameters[8].Value = model.StatusNo;
            //}



            //if (model.SampleTypeNo != null)
            //{
            //    parameters[9].Value = model.SampleTypeNo;
            //}



            //if (model.PatNo != null)
            //{
            //    parameters[10].Value = model.PatNo;
            //}



            //if (model.ClientName != null)
            //{
            //    parameters[11].Value = model.ClientName;
            //}



            //if (model.GenderNo != null)
            //{
            //    parameters[12].Value = model.GenderNo;
            //}



            //if (model.Birthday != null)
            //{
            //    parameters[13].Value = model.Birthday;
            //}



            //if (model.Age != null)
            //{
            //    parameters[14].Value = model.Age;
            //}



            //if (model.AgeUnitNo != null)
            //{
            //    parameters[15].Value = model.AgeUnitNo;
            //}



            //if (model.FolkNo != null)
            //{
            //    parameters[16].Value = model.FolkNo;
            //}



            //if (model.DistrictNo != null)
            //{
            //    parameters[17].Value = model.DistrictNo;
            //}



            //if (model.WardNo != null)
            //{
            //    parameters[18].Value = model.WardNo;
            //}



            //if (model.Bed != null)
            //{
            //    parameters[19].Value = model.Bed;
            //}



            //if (model.DeptNo != null)
            //{
            //    parameters[20].Value = model.DeptNo;
            //}



            //if (model.Doctor != null)
            //{
            //    parameters[21].Value = model.Doctor;
            //}



            //if (model.AgeUnitName != null)
            //{
            //    parameters[22].Value = model.AgeUnitName;
            //}



            //if (model.DiagNo != null)
            //{
            //    parameters[23].Value = model.DiagNo;
            //}



            //if (model.Diag != null)
            //{
            //    parameters[24].Value = model.Diag;
            //}



            //if (model.ChargeNo != null)
            //{
            //    parameters[25].Value = model.ChargeNo;
            //}



            //if (model.Charge != null)
            //{
            //    parameters[26].Value = model.Charge;
            //}



            //if (model.Chargeflag != null)
            //{
            //    parameters[27].Value = model.Chargeflag;
            //}



            //if (model.CountNodesFormSource != null)
            //{
            //    parameters[28].Value = model.CountNodesFormSource;
            //}



            //if (model.IsCheckFee != null)
            //{
            //    parameters[29].Value = model.IsCheckFee;
            //}



            //if (model.Operator != null)
            //{
            //    parameters[30].Value = model.Operator;
            //}



            //if (model.OperDate != null)
            //{
            //    parameters[31].Value = model.OperDate;
            //}



            //if (model.OperTime != null)
            //{
            //    parameters[32].Value = model.OperTime;
            //}



            //if (model.GenderName != null)
            //{
            //    parameters[33].Value = model.GenderName;
            //}



            //if (model.FormMemo != null)
            //{
            //    parameters[34].Value = model.FormMemo;
            //}



            //if (model.RequestSource != null)
            //{
            //    parameters[35].Value = model.RequestSource;
            //}



            //if (model.SickOrder != null)
            //{
            //    parameters[36].Value = model.SickOrder;
            //}



            //if (model.jztype != null)
            //{
            //    parameters[37].Value = model.jztype;
            //}



            //if (model.zdy1 != null)
            //{
            //    parameters[38].Value = model.zdy1;
            //}



            //if (model.zdy2 != null)
            //{
            //    parameters[39].Value = model.zdy2;
            //}



            //if (model.zdy3 != null)
            //{
            //    parameters[40].Value = model.zdy3;
            //}



            //if (model.zdy4 != null)
            //{
            //    parameters[41].Value = model.zdy4;
            //}



            //if (model.zdy5 != null)
            //{
            //    parameters[42].Value = model.zdy5;
            //}



            //if (model.FlagDateDelete != null)
            //{
            //    parameters[43].Value = model.FlagDateDelete;
            //}



            //if (model.DeptName != null)
            //{
            //    parameters[44].Value = model.DeptName;
            //}



            //if (model.NurseFlag != null)
            //{
            //    parameters[45].Value = model.NurseFlag;
            //}



            //if (model.IsByHand != null)
            //{
            //    parameters[46].Value = model.IsByHand;
            //}



            //if (model.TestTypeNo != null)
            //{
            //    parameters[47].Value = model.TestTypeNo;
            //}



            //if (model.ExecDeptNo != null)
            //{
            //    parameters[48].Value = model.ExecDeptNo;
            //}



            //if (model.CollectDate != null)
            //{
            //    parameters[49].Value = model.CollectDate;
            //}



            //if (model.CollectTime != null)
            //{
            //    parameters[50].Value = model.CollectTime;
            //}



            //if (model.Collecter != null)
            //{
            //    parameters[51].Value = model.Collecter;
            //}



            //if (model.LABCENTER != null)
            //{
            //    parameters[52].Value = model.LABCENTER;
            //}



            //if (model.NRequestFormNo != null)
            //{
            //    parameters[53].Value = model.NRequestFormNo;
            //}



            //if (model.CheckNo != null)
            //{
            //    parameters[54].Value = model.CheckNo;
            //}



            //if (model.DistrictName != null)
            //{
            //    parameters[55].Value = model.DistrictName;
            //}



            //if (model.CheckName != null)
            //{
            //    parameters[56].Value = model.CheckName;
            //}



            //if (model.WebLisSourceOrgID != null)
            //{
            //    parameters[57].Value = model.WebLisSourceOrgID;
            //}



            //if (model.WebLisSourceOrgName != null)
            //{
            //    parameters[58].Value = model.WebLisSourceOrgName;
            //}



            //if (model.WardName != null)
            //{
            //    parameters[59].Value = model.WardName;
            //}



            //if (model.FolkName != null)
            //{
            //    parameters[60].Value = model.FolkName;
            //}



            //if (model.ClinicTypeName != null)
            //{
            //    parameters[61].Value = model.ClinicTypeName;
            //}


            return idb.ExecuteNonQuery(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(long NRequestFormNo)
        {

            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("delete from NRequestForm ");
            //strSql.Append(" where NRequestFormNo=@NRequestFormNo ");
            //SqlParameter[] parameters = {
            //        new SqlParameter("@NRequestFormNo", SqlDbType.BigInt,8)			};
            //parameters[0].Value = NRequestFormNo;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from NRequestForm ");
            strSql.Append(" where NRequestFormNo=" + NRequestFormNo + " ");

            return idb.ExecuteNonQuery(strSql.ToString());
        }

        public Model.NRequestForm GetModelBySerialNo(string SerialNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select weblisflag,ClientNo, TestTypeName, CName, DoctorName, CollecterName, SampleTypeName, SerialNo, ReceiveFlag, StatusNo, SampleTypeNo, PatNo, ClientName, GenderNo, Birthday, Age, AgeUnitNo, FolkNo, DistrictNo, WardNo, Bed, DeptNo, Doctor, AgeUnitName, DiagNo, Diag, ChargeNo, Charge, Chargeflag, CountNodesFormSource, IsCheckFee, Operator, OperDate, OperTime, GenderName, FormMemo, RequestSource, SickOrder, jztype, zdy1, zdy2, zdy3, zdy4, zdy5, FlagDateDelete, DeptName, NurseFlag, IsByHand, TestTypeNo, ExecDeptNo, CollectDate, CollectTime, Collecter, LABCENTER, NRequestFormNo, CheckNo, DistrictName, CheckName, WebLisSourceOrgID, WebLisSourceOrgName, WardName, FolkName, ClinicTypeName  ");
            strSql.Append("  from NRequestForm ");
            strSql.Append(" where SerialNo='" + SerialNo + "' ");



            ZhiFang.Model.NRequestForm model = new ZhiFang.Model.NRequestForm();
            DataSet ds = idb.ExecuteDataSet(strSql.ToString());

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

            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("select ClientNo, TestTypeName, CName, DoctorName, CollecterName, SampleTypeName, SerialNo, ReceiveFlag, StatusNo, SampleTypeNo, PatNo, ClientName, GenderNo, Birthday, Age, AgeUnitNo, FolkNo, DistrictNo, WardNo, Bed, DeptNo, Doctor, AgeUnitName, DiagNo, Diag, ChargeNo, Charge, Chargeflag, CountNodesFormSource, IsCheckFee, Operator, OperDate, OperTime, GenderName, FormMemo, RequestSource, SickOrder, jztype, zdy1, zdy2, zdy3, zdy4, zdy5, FlagDateDelete, DeptName, NurseFlag, IsByHand, TestTypeNo, ExecDeptNo, CollectDate, CollectTime, Collecter, LABCENTER, NRequestFormNo, CheckNo, DistrictName, CheckName, WebLisSourceOrgID, WebLisSourceOrgName, WardName, FolkName, ClinicTypeName  ");
            //strSql.Append("  from NRequestForm ");
            //strSql.Append(" where NRequestFormNo=@NRequestFormNo ");
            //SqlParameter[] parameters = {
            //        new SqlParameter("@NRequestFormNo", SqlDbType.BigInt,8)			};
            //parameters[0].Value = NRequestFormNo;
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" select *  ");
            strSql.Append(" from NRequestForm ");
            strSql.Append(" where 1=1 and ROWNUM <= '1' and NRequestFormNo=" + NRequestFormNo + " ");

            ZhiFang.Model.NRequestForm model = new ZhiFang.Model.NRequestForm();
            DataSet ds = idb.ExecuteDataSet(strSql.ToString());

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
                //model.PersonID = ds.Tables[0].Rows[0]["PersonID"].ToString();
                model.SampleType = ds.Tables[0].Rows[0]["SampleType"].ToString();
                model.TelNo = ds.Tables[0].Rows[0]["TelNo"].ToString();
                model.WebLisOrgID = ds.Tables[0].Rows[0]["WebLisOrgID"].ToString();
                model.WebLisOrgName = ds.Tables[0].Rows[0]["WebLisOrgName"].ToString();
                model.STATUSName = ds.Tables[0].Rows[0]["STATUSName"].ToString();
                model.jztypeName = ds.Tables[0].Rows[0]["jztypeName"].ToString();
                model.BarCode = ds.Tables[0].Rows[0]["barcode"].ToString();
                model.CombiItemName = ds.Tables[0].Rows[0]["CombiItemName"].ToString();
                if (ds.Tables[0].Rows[0]["printTimes"].ToString() != "")
                {
                    model.PrintTimes = int.Parse(ds.Tables[0].Rows[0]["printTimes"].ToString());
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

            strSql.Append(" * ");
            strSql.Append(" FROM NRequestForm ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
                strSql.Append(" and ROWNUM <= '" + Top + "'");
            }
            else
            {
                strSql.Append(" where ROWNUM <= '" + Top + "'");
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
                strSql.Append(" and OperDate=to_date('" + model.OperDate.ToString() + "','YYYY-MM-DD HH24:MI:SS') ");
            }

            if (model.OperTime != null)
            {
                strSql.Append(" and OperTime=to_date('" + model.OperDate.ToString() + "','YYYY-MM-DD HH24:MI:SS') ");
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
                strSql.Append(" and FlagDateDelete=to_date('" + model.FlagDateDelete.ToString() + "','YYYY-MM-DD HH24:MI:SS') ");
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
                strSql.Append(" and CollectDate=to_date('" + model.CollectDate.ToString() + "','YYYY-MM-DD HH24:MI:SS') ");
            }

            if (model.CollectTime != null)
            {
                strSql.Append(" and CollectTime=to_date('" + model.CollectTime.ToString() + "','YYYY-MM-DD HH24:MI:SS') ");
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM GenderType RIGHT OUTER JOIN " +
                        "NRequestItem INNER JOIN " +
                        "NRequestForm ON " +
                        "NRequestItem.NRequestFormNo = NRequestForm.NRequestFormNo LEFT OUTER JOIN " +
                        "PatDiagInfo ON NRequestForm.DiagNo = PatDiagInfo.DiagNo LEFT OUTER JOIN " +
                        "SampleType ON NRequestForm.SampleTypeNo = SampleType.SampleTypeNo ON  " +
                        "GenderType.GenderNo = NRequestForm.GenderNo LEFT OUTER JOIN " +
                        "BarCodeForm ON NRequestItem.BarCodeFormNo = BarCodeForm.BarCodeFormNo  where 1=1 ");
            if (model.ClientNo != null)
            {
                strSql.Append(" and NRequestForm.ClientNo='" + model.ClientNo + "' ");
            }

            if (model.TestTypeName != null)
            {
                strSql.Append(" and NRequestForm.TestTypeName='" + model.TestTypeName + "' ");
            }

            if (model.CName != null)
            {
                strSql.Append(" and NRequestForm.CName='" + model.CName + "' ");
            }

            if (model.DoctorName != null)
            {
                strSql.Append(" and NRequestForm.DoctorName='" + model.DoctorName + "' ");
            }

            if (model.CollecterName != null)
            {
                strSql.Append(" and NRequestForm.CollecterName='" + model.CollecterName + "' ");
            }

            if (model.SampleTypeName != null)
            {
                strSql.Append(" and NRequestForm.SampleTypeName='" + model.SampleTypeName + "' ");
            }

            if (model.SerialNo != null)
            {
                strSql.Append(" and NRequestForm.SerialNo='" + model.SerialNo + "' ");
            }

            if (model.ReceiveFlag != null)
            {
                strSql.Append(" and NRequestForm.ReceiveFlag=" + model.ReceiveFlag + " ");
            }

            if (model.StatusNo != null)
            {
                strSql.Append(" and NRequestForm.StatusNo=" + model.StatusNo + " ");
            }

            if (model.SampleTypeNo != null)
            {
                strSql.Append(" and NRequestForm.SampleTypeNo=" + model.SampleTypeNo + " ");
            }

            if (model.PatNo != null)
            {
                strSql.Append(" and NRequestForm.PatNo='" + model.PatNo + "' ");
            }

            if (model.ClientName != null)
            {
                strSql.Append(" and NRequestForm.ClientName='" + model.ClientName + "' ");
            }

            if (model.GenderNo != null)
            {
                strSql.Append(" and NRequestForm.GenderNo=" + model.GenderNo + " ");
            }

            if (model.Birthday != null)
            {
                strSql.Append(" and NRequestForm.Birthday='" + model.Birthday + "' ");
            }

            if (model.Age != null)
            {
                strSql.Append(" and NRequestForm.Age=" + model.Age + " ");
            }

            if (model.AgeUnitNo != null)
            {
                strSql.Append(" and NRequestForm.AgeUnitNo=" + model.AgeUnitNo + " ");
            }

            if (model.FolkNo != null)
            {
                strSql.Append(" and NRequestForm.FolkNo=" + model.FolkNo + " ");
            }

            if (model.DistrictNo != null)
            {
                strSql.Append(" and NRequestForm.DistrictNo=" + model.DistrictNo + " ");
            }

            if (model.WardNo != null)
            {
                strSql.Append(" and NRequestForm.WardNo=" + model.WardNo + " ");
            }

            if (model.Bed != null)
            {
                strSql.Append(" and NRequestForm.Bed='" + model.Bed + "' ");
            }

            if (model.DeptNo != null)
            {
                strSql.Append(" and NRequestForm.DeptNo=" + model.DeptNo + " ");
            }

            if (model.Doctor != null)
            {
                strSql.Append(" and NRequestForm.Doctor=" + model.Doctor + " ");
            }

            if (model.AgeUnitName != null)
            {
                strSql.Append(" and NRequestForm.AgeUnitName='" + model.AgeUnitName + "' ");
            }

            if (model.DiagNo != null)
            {
                strSql.Append(" and NRequestForm.DiagNo=" + model.DiagNo + " ");
            }

            if (model.Diag != null)
            {
                strSql.Append(" and NRequestForm.Diag='" + model.Diag + "' ");
            }

            if (model.ChargeNo != null)
            {
                strSql.Append(" and NRequestForm.ChargeNo=" + model.ChargeNo + " ");
            }

            if (model.Charge != null)
            {
                strSql.Append(" and NRequestForm.Charge=" + model.Charge + " ");
            }

            if (model.Chargeflag != null)
            {
                strSql.Append(" and NRequestForm.Chargeflag='" + model.Chargeflag + "' ");
            }

            if (model.CountNodesFormSource != null)
            {
                strSql.Append(" and NRequestForm.CountNodesFormSource='" + model.CountNodesFormSource + "' ");
            }

            if (model.IsCheckFee != null)
            {
                strSql.Append(" and NRequestForm.IsCheckFee=" + model.IsCheckFee + " ");
            }

            if (model.Operator != null)
            {
                strSql.Append(" and NRequestForm.Operator='" + model.Operator + "' ");
            }

            if (model.OperDate != null)
            {
                strSql.Append(" and OperDate=to_date('" + model.OperDate.ToString() + "','YYYY-MM-DD HH24:MI:SS') ");
            }

            if (model.OperTime != null)
            {
                strSql.Append(" and OperTime=to_date('" + model.OperTime.ToString() + "','YYYY-MM-DD HH24:MI:SS') ");
            }

            if (model.GenderName != null)
            {
                strSql.Append(" and NRequestForm.GenderName='" + model.GenderName + "' ");
            }

            if (model.FormMemo != null)
            {
                strSql.Append(" and NRequestForm.FormMemo='" + model.FormMemo + "' ");
            }

            if (model.RequestSource != null)
            {
                strSql.Append(" and NRequestForm.RequestSource='" + model.RequestSource + "' ");
            }

            if (model.SickOrder != null)
            {
                strSql.Append(" and NRequestForm.SickOrder='" + model.SickOrder + "' ");
            }

            if (model.jztype != null)
            {
                strSql.Append(" and NRequestForm.jztype=" + model.jztype + " ");
            }

            if (model.zdy1 != null)
            {
                strSql.Append(" and NRequestForm.zdy1='" + model.zdy1 + "' ");
            }

            if (model.zdy2 != null)
            {
                strSql.Append(" and NRequestForm.zdy2='" + model.zdy2 + "' ");
            }

            if (model.zdy3 != null)
            {
                strSql.Append(" and NRequestForm.zdy3='" + model.zdy3 + "' ");
            }

            if (model.zdy4 != null)
            {
                strSql.Append(" and NRequestForm.zdy4='" + model.zdy4 + "' ");
            }

            if (model.zdy5 != null)
            {
                strSql.Append(" and NRequestForm.zdy5='" + model.zdy5 + "' ");
            }

            if (model.FlagDateDelete != null)
            {
                strSql.Append(" and NRequestForm.FlagDateDelete=to_date('" + model.FlagDateDelete.ToString() + "','YYYY-MM-DD HH24:MI:SS') ");
            }

            if (model.DeptName != null)
            {
                strSql.Append(" and NRequestForm.DeptName='" + model.DeptName + "' ");
            }

            if (model.NurseFlag != null)
            {
                strSql.Append(" and NRequestForm.NurseFlag=" + model.NurseFlag + " ");
            }

            if (model.IsByHand != null)
            {
                strSql.Append(" and NRequestForm.IsByHand='" + model.IsByHand + "' ");
            }

            if (model.TestTypeNo != null)
            {
                strSql.Append(" and NRequestForm.TestTypeNo=" + model.TestTypeNo + " ");
            }

            if (model.ExecDeptNo != null)
            {
                strSql.Append(" and NRequestForm.ExecDeptNo=" + model.ExecDeptNo + " ");
            }

            if (model.CollectDate != null)
            {
                strSql.Append(" and NRequestForm.CollectDate=to_date('" + model.CollectDate.ToString() + "','YYYY-MM-DD HH24:MI:SS') ");
            }

            if (model.CollectTime != null)
            {
                strSql.Append(" and NRequestForm.CollectTime=to_date('" + model.CollectTime.ToString() + "','YYYY-MM-DD HH24:MI:SS') ");
            }

            if (model.Collecter != null)
            {
                strSql.Append(" and NRequestForm.Collecter='" + model.Collecter + "' ");
            }

            if (model.LABCENTER != null)
            {
                strSql.Append(" and NRequestForm.LABCENTER='" + model.LABCENTER + "' ");
            }

            if (model.NRequestFormNo != null)
            {
                strSql.Append(" and NRequestForm.NRequestFormNo='" + model.NRequestFormNo + "' ");
            }

            if (model.CheckNo != null)
            {
                strSql.Append(" and NRequestForm.CheckNo='" + model.CheckNo + "' ");
            }

            if (model.DistrictName != null)
            {
                strSql.Append(" and NRequestForm.DistrictName='" + model.DistrictName + "' ");
            }

            if (model.CheckName != null)
            {
                strSql.Append(" and NRequestForm.CheckName='" + model.CheckName + "' ");
            }

            if (model.WebLisSourceOrgID != null)
            {
                strSql.Append(" and NRequestForm.WebLisSourceOrgID='" + model.WebLisSourceOrgID + "' ");
            }

            if (model.WebLisSourceOrgName != null)
            {
                strSql.Append(" and NRequestForm.WebLisSourceOrgName='" + model.WebLisSourceOrgName + "' ");
            }

            if (model.WardName != null)
            {
                strSql.Append(" and NRequestForm.WardName='" + model.WardName + "' ");
            }

            if (model.FolkName != null)
            {
                strSql.Append(" and NRequestForm.FolkName='" + model.FolkName + "' ");
            }

            if (model.ClinicTypeName != null)
            {
                strSql.Append(" and NRequestForm.ClinicTypeName='" + model.ClinicTypeName + "' ");
            }
            if (model.OperDateStart != null && model.OperDateStart != "")
            {
                strSql.Append(" and NRequestForm.OperDate >= to_date('" + model.OperDate.ToString() + "','YYYY-MM-DD HH24:MI:SS') ");
            }
            if (model.OperDateEnd != null && model.OperDateEnd != "")
            {
                strSql.Append(" and NRequestForm.OperDate <= to_date('" + model.OperDate.ToString() + "','YYYY-MM-DD HH24:MI:SS') ");
            }

            if (model.CollectDateStart != "" && model.CollectDateStart != null)
            {
                strSql.Append(" and BarCodeForm.CollectDate >= to_date('" + model.CollectDateStart.ToString() + "','YYYY-MM-DD HH24:MI:SS') ");
            }
            if (model.CollectDateEnd != "" && model.CollectDateEnd != null)
            {
                strSql.Append(" and BarCodeForm.CollectDate <= to_date('" + model.CollectDateEnd.ToString() + "','YYYY-MM-DD HH24:MI:SS') ");
            }
            if (model.IsOnlyNoBar == true)
            {
                strSql.Append(" and (BarCodeForm.PrintCount=0 or BarCodeForm.PrintCount is null)  ");
            }
            else
            {
                strSql.Append(" and (BarCodeForm.PrintCount>=0  or BarCodeForm.PrintCount is null)  ");
            }
            if (model.ClientList != null && model.ClientList.Trim() != "")
            {
                strSql.Append(" and NRequestForm.WebLisSourceOrgID in  (" + model.ClientList + ")  ");
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
        public DataSet GetListByPage(ZhiFang.Model.NRequestForm model, int nowPageNum, int nowPageSize)
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
                strwhere.Append(" and NRequestForm.PatNo='" + model.PatNo + "' ");
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
                strwhere.Append(" and NRequestForm.OperDate = to_date('" + model.OperDate.ToString() + "','YYYY-MM-DD HH24:MI:SS') ");

            }

            if (model.OperTime != null)
            {
                strwhere.Append(" and NRequestForm.OperTime = to_date('" + model.OperTime.ToString() + "','YYYY-MM-DD HH24:MI:SS') ");
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
                strwhere.Append(" and NRequestForm.FlagDateDelete = to_date('" + model.FlagDateDelete.ToString() + "','YYYY-MM-DD HH24:MI:SS') ");

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
                strwhere.Append(" and NRequestForm.CollectDate = to_date('" + model.CollectDate.ToString() + "','YYYY-MM-DD HH24:MI:SS') ");
            }

            if (model.CollectTime != null)
            {
                strwhere.Append(" and NRequestForm.CollectTime = to_date('" + model.CollectTime.ToString() + "','YYYY-MM-DD HH24:MI:SS') ");
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
                strwhere.Append(" and to_char(NRequestForm.OperDate,'YYYY-MM-DD') >= '" + model.OperDateStart + "'  ");
            }
            if (model.OperDateEnd != null && model.OperDateEnd != "")
            {
                strwhere.Append(" and to_char(NRequestForm.OperDate,'YYYY-MM-DD HH24:MI:SS') <= '" + model.OperDateEnd + "' ");
            }

            if (model.CollectDateStart != null)
            {
                strwhere.Append(" and BarCodeForm.CollectDate = to_date('" + model.CollectDateStart.ToString() + "','YYYY-MM-DD HH24:MI:SS') ");

            }
            if (model.CollectDateEnd != null)
            {
                strwhere.Append(" and BarCodeForm.CollectDate = to_date('" + model.CollectDateEnd.ToString() + "','YYYY-MM-DD HH24:MI:SS') ");

            }
            if (model.IsOnlyNoBar == true)
            {
                strwhere.Append(" and (BarCodeForm.PrintCount=0 or BarCodeForm.PrintCount is null)  ");
            }
            else
            {
                strwhere.Append(" and (BarCodeForm.PrintCount>=0  or BarCodeForm.PrintCount is null)  ");
            }
            if (model.ClientList != null && model.ClientList.Trim() != "")
            {
                strwhere.Append(" and NRequestForm.ClientNo  in  (" + model.ClientList + ")  ");
            }
            #endregion
            StringBuilder strSql = new StringBuilder();
            if (model != null)
            {
                strSql.Append(" SELECT distinct  NRequestForm.NRequestFormNo,  BarCodeForm.BarCode as BarCode, BarCodeForm.BarCodeFormNo as BarCodeFormNo,BarCodeForm.PrintCount as PrintCount, NRequestForm.CName as CName,NRequestForm.DoctorName as DoctorName, NRequestForm.WebLisSourceOrgID,NRequestForm.WebLisSourceOrgName as WebLisSourceOrgName,NRequestForm.ClientNo,NRequestForm.ClientName as ClientName,NRequestForm.AgeUnitName as AgeUnitName, NRequestForm.Diag as Diag,GenderType.CName AS Sex, " +
                        "NRequestForm.Age as Age, SampleType.CName AS SampleName,concat(concat(to_char(BarCodeForm.inceptdate,'YYYY-MM-DD'),' '),to_char(BarCodeForm.incepttime,'HH24:MI:SS')) as incepttime,concat(concat(to_char(BarCodeForm.CollectDate,'YYYY-MM-DD'),' '),to_char(BarCodeForm.CollectTime,'HH24:MI:SS')) as CollectTime," +
                        "to_char(NRequestForm.OperTime,'YYYY-MM-DD HH24:MI:SS') as OperTime,PatDiagInfo.DiagDesc as DiagDesc,NRequestForm.zdy5  " +
                        "FROM GenderType RIGHT OUTER JOIN " +
                        "NRequestItem INNER JOIN " +
                        "NRequestForm ON " +
                        "NRequestItem.NRequestFormNo = NRequestForm.NRequestFormNo LEFT OUTER JOIN " +
                        "PatDiagInfo ON NRequestForm.DiagNo = PatDiagInfo.DiagNo LEFT OUTER JOIN " +
                        "SampleType ON NRequestForm.SampleTypeNo = SampleType.SampleTypeNo ON  " +
                        "GenderType.GenderNo = NRequestForm.GenderNo LEFT OUTER JOIN " +
                        "BarCodeForm ON NRequestItem.BarCodeFormNo = BarCodeForm.BarCodeFormNo ");

                strSql.Append(" where  ROWNUM <= '" + nowPageSize + "' and NRequestItem.NRequestFormNo not in (  ");
                strSql.Append("select  NRequestForm.NRequestFormNo from  GenderType RIGHT OUTER JOIN " +
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

                strSql.Append(" and ROWNUM <= '0'  ) ");
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
                strSql.Append(" SELECT distinct  NRequestForm.NRequestFormNo,BarCodeForm.BarCode as BarCode, BarCodeForm.BarCodeFormNo as BarCodeFormNo,BarCodeForm.PrintCount as PrintCount, NRequestForm.CName as CName,NRequestForm.DoctorName as DoctorName, NRequestForm.WebLisSourceOrgID,NRequestForm.WebLisSourceOrgName as WebLisSourceOrgName,NRequestForm.ClientNo,NRequestForm.ClientName as ClientName,NRequestForm.AgeUnitName as AgeUnitName, NRequestForm.Diag as Diag,GenderType.CName AS Sex, " +
                        "NRequestForm.Age as Age, SampleType.CName AS SampleName, concat(concat(to_char(BarCodeForm.CollectDate,'YYYY-MM-DD'),' '),to_char(BarCodeForm.CollectTime,'HH24:MI:SS')) as CollectTime, " +
                        "to_char(NRequestForm.OperTime,'YYYY-MM-DD HH24:MI:SS') as OperTime,PatDiagInfo.DiagDesc as DiagDesc,NRequestForm.zdy5  " +
                        "FROM GenderType RIGHT OUTER JOIN " +
                        "NRequestItem INNER JOIN " +
                        "NRequestForm ON " +
                        "NRequestItem.NRequestFormNo = NRequestForm.NRequestFormNo LEFT OUTER JOIN " +
                        "PatDiagInfo ON NRequestForm.DiagNo = PatDiagInfo.DiagNo LEFT OUTER JOIN " +
                        "SampleType ON NRequestForm.SampleTypeNo = SampleType.SampleTypeNo ON  " +
                        "GenderType.GenderNo = NRequestForm.GenderNo LEFT OUTER JOIN " +
                        "BarCodeForm ON NRequestItem.BarCodeFormNo = BarCodeForm.BarCodeFormNo ");

                strSql.Append(" where  ROWNUM <= '" + nowPageSize + "' and NRequestFormNo not in (  ");
                strSql.Append("select NRequestForm.NRequestFormNo from  GenderType RIGHT OUTER JOIN " +
                        "NRequestItem INNER JOIN " +
                        "NRequestForm ON " +
                        "NRequestItem.NRequestFormNo = NRequestForm.NRequestFormNo LEFT OUTER JOIN " +
                        "PatDiagInfo ON NRequestForm.DiagNo = PatDiagInfo.DiagNo LEFT OUTER JOIN " +
                        "SampleType ON NRequestForm.SampleTypeNo = SampleType.SampleTypeNo ON  " +
                        "GenderType.GenderNo = NRequestForm.GenderNo LEFT OUTER JOIN " +
                        "BarCodeForm ON NRequestItem.BarCodeFormNo = BarCodeForm.BarCodeFormNo   ");

                strSql.Append("where 1=1 and ROWNUM <= '" + (nowPageSize * nowPageNum) + "' ) order by NRequestForm.NRequestFormNo  ");
                ZhiFang.Common.Log.Log.Info(strSql.ToString());
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
        }
        public DataSet GetListBy(ZhiFang.Model.NRequestForm model)
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
                strwhere.Append(" and NRequestForm.PatNo='" + model.PatNo + "' ");
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
                strSql.Append(" SELECT distinct NRequestForm.NRequestFormNo,  BarCodeForm.BarCode as BarCode, BarCodeForm.BarCodeFormNo as BarCodeFormNo,BarCodeForm.PrintCount as PrintCount, NRequestForm.CName as CName,NRequestForm.DoctorName as DoctorName, NRequestForm.WebLisSourceOrgID,NRequestForm.WebLisSourceOrgName as WebLisSourceOrgName,NRequestForm.ClientNo,NRequestForm.ClientName as ClientName,NRequestForm.AgeUnitName as AgeUnitName, NRequestForm.Diag as Diag,GenderType.CName AS Sex, " +
                        "NRequestForm.Age as Age, SampleType.CName AS SampleName, concat(concat(BarCodeForm.inceptdate,' '),BarCodeForm.incepttime) as incepttime,concat(concat(BarCodeForm.CollectDate,' '),BarCodeForm.CollectTime) as CollectTime," +
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
                strSql.Append("select NRequestForm.NRequestFormNo from  GenderType RIGHT OUTER JOIN " +
                        "NRequestItem INNER JOIN " +
                        "NRequestForm ON " +
                        "NRequestItem.NRequestFormNo = NRequestForm.NRequestFormNo LEFT OUTER JOIN " +
                        "PatDiagInfo ON NRequestForm.DiagNo = PatDiagInfo.DiagNo LEFT OUTER JOIN " +
                        "SampleType ON NRequestForm.SampleTypeNo = SampleType.SampleTypeNo ON  " +
                        "GenderType.GenderNo = NRequestForm.GenderNo LEFT OUTER JOIN " +
                        "BarCodeForm ON NRequestItem.BarCodeFormNo = BarCodeForm.BarCodeFormNo where 1=1 and ROWNUM <='0'");
                if (strwhere.ToString().Trim() == "")
                {
                    strSql.Append(" 1=2");
                }
                else
                {
                    strSql.Append(" 1=1 " + strwhere.ToString() + "   ");
                }

                strSql.Append(" ) ");
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
                        "NRequestForm.Age as Age, SampleType.CName AS SampleName, ConCat(Concat(BarCodeForm.CollectDate,' '),BarCodeForm.CollectTime) as CollectTime, " +
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

                strSql.Append(") order by NRequestForm.NRequestFormNo  ");
                ZhiFang.Common.Log.Log.Info(strSql.ToString());
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
        }

        /// <summary>
        /// 海妇婴保健院(定制)在NRequestForm表里面新增字段:TestAim（诊断类型） ganwh add 2016-1-11
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
                    strSqlControl.Append("  concat(concat(concat(concat(" + lst[i].Trim() + ",'_')," + TableKey + "),'_')," + TableKey + ") as " + TableKeySub + "ControlNo," + TableKey + ",'" + lst[i].Trim() + "' as ControlLabNo," + TableKey + ",UseFlag ");
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

            strSql.Append(" * ");
            strSql.Append(" FROM NRequestForm ");
            strSql.Append(" where 1=1 ");

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
            strSql.Append(" and ROWNUM <= '" + Top + "'");
            strSql.Append(" order by " + filedOrder);
            return idb.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetAllList()
        {
            return GetList("");
        }

        #region IDNRequestForm 成员

        public DataSet GetListByBarCodeNo(string BarCodeNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT (SELECT CName FROM TestItem  TestItem WHERE (ItemNo = NRequestItem.ParItemNo))  TestItemCName,BarCodeForm.BarCode, NRequestItem.ParItemNo, BarCodeForm.CollecterID, BarCodeForm.CollectDate AS Expr1, BarCodeForm.CollectTime AS Expr2, BarCodeForm.Collecter AS Expr3, (SELECT CName  FROM TestItem  TestItem_2 WHERE      (ItemNo = NRequestItem.CombiItemNo)) AS CombiItemName, NRequestForm.* FROM         NRequestForm INNER JOIN                      NRequestItem ON NRequestItem.NRequestFormNo = NRequestForm.NRequestFormNo INNER JOIN TestItem  TestItem_1 ON NRequestItem.ParItemNo = TestItem_1.ItemNo RIGHT OUTER JOIN  BarCodeForm ON NRequestItem.BarCodeFormNo = BarCodeForm.BarCodeFormNo");
            if (BarCodeNo.Trim() != "")
            {
                strSql.Append(" where BarCodeForm.BarCode='" + BarCodeNo + "'");
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
        public DataSet GetListByModel(ZhiFang.Model.NRequestForm NRequestForm, ZhiFang.Model.BarCodeForm BarCodeForm)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT (SELECT CName FROM TestItem  TestItem WHERE (ItemNo = NRequestItem.ParItemNo))  TestItemCName,BarCodeForm.BarCode, NRequestItem.ParItemNo, BarCodeForm.CollecterID, (SELECT CName  FROM TestItem  TestItem_2 WHERE      (ItemNo = NRequestItem.CombiItemNo))  CombiItemName, NRequestForm.* FROM         NRequestForm INNER JOIN                      NRequestItem ON NRequestItem.NRequestFormNo = NRequestForm.NRequestFormNo INNER JOIN TestItem  TestItem_1 ON NRequestItem.ParItemNo = TestItem_1.ItemNo RIGHT OUTER JOIN  BarCodeForm ON NRequestItem.BarCodeFormNo = BarCodeForm.BarCodeFormNo where 1=1");
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
                strSql.Append(" and NRequestForm.PatNo='" + NRequestForm.PatNo + "'");

            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        #endregion

        #region IDataBase<NRequestForm> 成员


        public int AddUpdateByDataSet(DataSet ds)
        {
            throw new NotImplementedException();
        }

        public int Delete(string SerialNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from NRequestForm ");
            strSql.Append(" where SerialNo='" + SerialNo + "' ");
            //SqlParameter[] parameters = {
            //        new SqlParameter("@SerialNo", SqlDbType.VarChar,50)			};
            //parameters[0].Value = SerialNo;
            return idb.ExecuteNonQuery(strSql.ToString());
        }

        #endregion

        /// <summary>
        /// 太和医院操作人员工作量统计 ganw add 2015-10-29
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
            #region where
            strSql.Append(" select top " + page + " * from  ");
            strSql.Append("(select  ROW_NUMBER() over(order by a.OperDate desc) rownumber, left(convert(varchar,a.OperDate,21)," + dateType + ") OperDate ,a.ClientName ,a.ClientNo,a.Collecter Operator, COUNT(c.barcode) BarcodeNum ,sum(b.price) SumMoney");
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
            strSql.Append(" group by a.OperDate,a.ClientName, a.ClientNo,c.BarCode, a.Collecter, left(convert(varchar,a.OperDate,21)," + dateType + ")) subTable ");
            strSql.Append("  where subTable.rownumber>" + rows * (page - 1));
            #endregion

            ZhiFang.Common.Log.Log.Info(strSql.ToString());
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
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

        #region IDNRequestForm 成员


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
                strwhere.Append(" and NRequestForm.PatNo='" + model.PatNo + "' ");
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
                strwhere.Append(" and NRequestForm.OperDate >=to_date('" + model.OperDateStart + "','YYYY-MM-DD HH24:MI:SS')  ");
            }
            if (model.OperDateEnd != null && model.OperDateEnd != "")
            {
                strwhere.Append(" and NRequestForm.OperDate <=to_date('" + model.OperDateEnd + "','YYYY-MM-DD HH24:MI:SS') ");
            }

            if (model.CollectDateStart != null)
            {
                strwhere.Append(" and NRequestForm.CollectDate >=to_date('" + model.CollectDateStart + "','YYYY-MM-DD HH24:MI:SS')  ");
            }
            if (model.CollectDateEnd != null)
            {
                strwhere.Append(" and NRequestForm.CollectDate <=to_date('" + model.CollectDateEnd + "','YYYY-MM-DD HH24:MI:SS')  ");
            }
            if (model.ClientList != null && model.ClientList.Trim() != "")
            {
                strwhere.Append(" and NRequestForm.ClientNo  in  (" + model.ClientList + ")  ");
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
        //ganwh add 统计查询 2015-6-16
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
                strSql.Append(" select count(*) FROM StatisticsNequestForm where 1=1  ");
                if (strwhere.ToString().Trim() != "")
                {
                    strSql.Append("   " + strwhere.ToString() + "   ");
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
                strwhere.Append(" and NRequestForm.PatNo='" + model.PatNo + "' ");
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
                strwhere.Append(" and NRequestForm.OperDate >=to_date('" + model.OperDateStart + "','YYYY-MM-DD HH24:MI:SS')  ");
            }
            if (model.OperDateEnd != null && model.OperDateEnd != "")
            {
                strwhere.Append(" and NRequestForm.OperDate <=to_date('" + model.OperDateEnd + "','YYYY-MM-DD HH24:MI:SS') ");
            }

            if (model.CollectDateStart != null)
            {
                strwhere.Append(" and NRequestForm.CollectDate >=to_date('" + model.CollectDateStart + "','YYYY-MM-DD HH24:MI:SS')  ");
            }
            if (model.CollectDateEnd != null)
            {
                strwhere.Append(" and NRequestForm.CollectDate <=to_date('" + model.CollectDateEnd + "','YYYY-MM-DD HH24:MI:SS')  ");
            }
            if (model.ClientList != null && model.ClientList.Trim() != "")
            {
                strwhere.Append(" and NRequestForm.ClientNo  in  (" + model.ClientList + ")  ");
            }
            #endregion
            StringBuilder strSql = new StringBuilder();
            if (model != null)
            {
                strSql.Append(" select * from ( SELECT  NRequestForm.NRequestFormNo, " +
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
                        "SampleType.CName AS SampleName," +
                        "to_char(NRequestForm.operdate,'yyyy-mm-dd') || to_char(NRequestForm.opertime,' hh24:mi:ss')as incepttime," +
                        "to_char(NRequestForm.CollectDate,'yyyy-mm-dd') || to_char(NRequestForm.CollectTime,' hh24:mi:ss')as CollectTime," +
                        "NRequestForm.OperDate as OperDate," +
                        "NRequestForm.OperTime as OperTime," +
                        "PatDiagInfo.DiagDesc as DiagDesc," +
                        "NRequestForm.zdy5 " +
                        "FROM GenderType RIGHT OUTER JOIN " +
                        "NRequestForm LEFT OUTER JOIN " +
                        "PatDiagInfo ON NRequestForm.DiagNo = PatDiagInfo.DiagNo LEFT OUTER JOIN " +
                        "SampleType ON NRequestForm.SampleTypeNo = SampleType.SampleTypeNo ON GenderType.GenderNo = NRequestForm.GenderNo");

                strSql.Append(" where  ROWNUM <='" + nowPageSize + "' and NRequestForm.NRequestFormNo not in (  ");
                strSql.Append("select NRequestForm.NRequestFormNo FROM GenderType RIGHT OUTER JOIN " +
                        "NRequestForm LEFT OUTER JOIN " +
                        "PatDiagInfo ON NRequestForm.DiagNo = PatDiagInfo.DiagNo LEFT OUTER JOIN " +
                        "SampleType ON NRequestForm.SampleTypeNo = SampleType.SampleTypeNo ON GenderType.GenderNo = NRequestForm.GenderNo where ");
                if (strwhere.ToString().Trim() == "")
                {
                    strSql.Append(" 1=2 and ROWNUM <='" + (nowPageSize * nowPageNum) + "' ");
                }
                else
                {
                    strSql.Append(" 1=1 and ROWNUM <='" + (nowPageSize * nowPageNum) + "' " + strwhere.ToString() + "   ");
                }

                strSql.Append("  ) ");
                if (strwhere.ToString().Trim() == "")
                {
                    strSql.Append(" and 1=2");
                }
                else
                {
                    strSql.Append("   " + strwhere.ToString() + "   ");
                }
                strSql.Append("  ) aa order by aa.OperDate desc,aa.OperTime desc ");
                ZhiFang.Common.Log.Log.Info(strSql.ToString());
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
            else
            {
                strSql.Append("  select * from ( SELECT distinct   NRequestForm.NRequestFormNo, " +
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
                        "to_char(NRequestForm.operdate,'yyyy-mm-dd') || to_char(NRequestForm.opertime,' hh24:mi:ss')as incepttime," +
                        "to_char(NRequestForm.CollectDate,'yyyy-mm-dd') || to_char(NRequestForm.CollectTime,' hh24:mi:ss')as CollectTime," +
                        "NRequestForm.OperDate as OperDate," +
                        "NRequestForm.OperTime as OperTime," +
                        "PatDiagInfo.DiagDesc as DiagDesc," +
                        "NRequestForm.zdy5 " +
                        "FROM  GenderType RIGHT OUTER JOIN " +
                        "NRequestForm LEFT OUTER JOIN " +
                        "PatDiagInfo ON NRequestForm.DiagNo = PatDiagInfo.DiagNo LEFT OUTER JOIN " +
                        "SampleType ON NRequestForm.SampleTypeNo = SampleType.SampleTypeNo ON GenderType.GenderNo = NRequestForm.GenderNo ");

                strSql.Append(" where ROWNUM <='" + nowPageSize + "' and NRequestForm.NRequestFormNo not in (  ");
                strSql.Append("select NRequestForm.NRequestFormNo from  GenderType RIGHT OUTER JOIN " +
                        "NRequestForm LEFT OUTER JOIN " +
                        "PatDiagInfo ON NRequestForm.DiagNo = PatDiagInfo.DiagNo LEFT OUTER JOIN " +
                        "SampleType ON NRequestForm.SampleTypeNo = SampleType.SampleTypeNo ON GenderType.GenderNo = NRequestForm.GenderNo where  ROWNUM <='" + (nowPageSize * nowPageNum) + "' ");

                strSql.Append("  )) aa order by aa.OperDate desc,aa.OperTime desc ");
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
                    "StatisticsNequestForm.ClientName)");
            strSql.Append(" where StatisticsNequestForm.NRequestFormNo not in (  ");
            strSql.Append("select top " + (nowPageSize * nowPageNum) + " StatisticsNequestForm.NRequestFormNo FROM StatisticsNequestForm ）");
            if (strwhere.ToString().Trim() == "")
            {
                strSql.Append(" 1=2");
            }
            else
            {
                strSql.Append(" 1=1 " + strwhere.ToString() + "   ");
            }

            strSql.Append("order by StatisticsNequestForm.OperDate desc ) ");
            ZhiFang.Common.Log.Log.Info(strSql.ToString());
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetNRequstFormListByPage_CombiItemNo(Model.NRequestForm nrf_m, int startPage, int pageSize)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region IDNRequestForm 成员


        public DataTable GetBarCodeByNRequestFormNo(string p)
        {
            if (p.Trim() != "")
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" SELECT     BarCodeForm.BarCode,BarCodeForm.BarCodeFormNo,BarCodeForm.color,BarCodeForm.ItemName,BarCodeForm.ItemNo FROM         NRequestForm INNER JOIN   NRequestItem ON NRequestForm.NRequestFormNo = NRequestItem.NRequestFormNo INNER JOIN       BarCodeForm ON NRequestItem.BarCodeFormNo = BarCodeForm.BarCodeFormNo          where NRequestForm.NRequestFormNo=" + p + "             group by BarCodeForm.BarCode,BarCodeForm.BarCodeFormNo,BarCodeForm.color,BarCodeForm.ItemName,BarCodeForm.ItemNo");
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

        #endregion

        #region IDNRequestForm 成员


        public int CheckNReportFormWeblisFlag(long NRequestFormNo)
        {
            StringBuilder strwhere = new StringBuilder();
            strwhere.Append(" SELECT count(*) FROM BarCodeForm INNER JOIN NRequestItem ON BarCodeForm.BarCodeFormNo = NRequestItem.BarCodeFormNo INNER JOIN NRequestForm ON NRequestItem.NRequestFormNo = NRequestForm.NRequestFormNo ");
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

        #endregion

        public DataTable GetBarCodeAndCNameByNRequestFormNo(string p)
        {
            if (p.Trim() != "")
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" SELECT     BarCodeForm.BarCode,BarCodeForm.BarCodeFormNo,NRequestForm.CName,NRequestForm.CollectDate, BarCodeForm.ReceiveDate,NRequestForm.OperDate FROM  ");
                strSql.Append(" NRequestForm INNER JOIN   NRequestItem ON NRequestForm.NRequestFormNo = NRequestItem.NRequestFormNo INNER JOIN       BarCodeForm ");
                strSql.Append(" ON NRequestItem.BarCodeFormNo = BarCodeForm.BarCodeFormNo ");
                strSql.Append(" where NRequestForm.NRequestFormNo=" + p + "            group by BarCodeForm.BarCode,BarCodeForm.BarCodeFormNo,NRequestForm.CName,NRequestForm.CollectDate, BarCodeForm.ReceiveDate,NRequestForm.OperDate");
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
            throw new NotImplementedException();
        }


        public DataSet GetStaticRecOrgSamplePrice(Model.StaticRecOrgSamplePrice model, int rows, int page)
        {
            throw new NotImplementedException();
        }
        public DataSet GetStaticRecOrgSamplePrice(Model.StaticRecOrgSamplePrice model)
        {
            throw new NotImplementedException();

        }

        public DataSet GetStaticPersonTestItemPriceList(int page, int rows, Model.StaticPersonTestItemPrice model)
        {
            throw new NotImplementedException();
        }


        public DataSet GetStaticPersonTestItemPriceList(Model.StaticPersonTestItemPrice model)
        {
            throw new NotImplementedException();
        }
        //把拒收记录复制到拒收表 ganwh add 2015-10-7
        public int Add(string strSql)
        {
            return DbHelperSQL.ExecuteNonQuery(strSql);
        }

        //查询被拒收申请单的信息 ganwh add 2015-10-7
        public DataSet GetRefuseList(string strSql)
        {
            return DbHelperSQL.ExecuteDataSet(strSql);
        }


        public DataSet GetBarcodePrice(Model.StaticRecOrgSamplePrice model, int rows, int page)
        {
            throw new NotImplementedException();
        }

        public DataSet GetBarcodePrice(Model.StaticRecOrgSamplePrice model)
        {
            throw new NotImplementedException();
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

                strSql.Append(" group by NRequestForm.ZDY10,NRequestForm.NRequestFormNo,NRequestForm.OldSerialNo,NRequestForm.CName, NRequestForm.DoctorName,NRequestForm.DeptName,NRequestForm.WebLisSourceOrgID, NRequestForm.WebLisSourceOrgName,NRequestForm.ClientNo,NRequestForm.jztype, NRequestForm.jztypeName,NRequestForm.PatNo,NRequestForm.ClientName, NRequestForm.AgeUnitName,NRequestForm.Diag,GenderType.CName,NRequestForm.Age, SampleType.CName,Convert(varchar(10),NRequestForm.operdate,21)+' '+Convert(varchar(10), NRequestForm.opertime,8),Convert(varchar(10),NRequestForm.CollectDate,21)+' '+Convert(varchar(10), NRequestForm.CollectTime,8),NRequestForm.OperDate,NRequestForm.OperTime, PatDiagInfo.DiagDesc,NRequestForm.zdy5 ) aa");

                strSql.Append(" order by aa.OperDate desc,aa.OperTime desc");
                //ZhiFang.Common.Log.Log.Info(strSql.ToString());
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
                #endregion
            }
            else
            {
                strSql.Append("  select * from ( SELECT distinct top " + nowPageSize + "  NRequestForm.ZDY10,NRequestForm.NRequestFormNo,NRequestForm.OldSerialNo," +
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
            if (model != null && strwhere.ToString().Trim() != "")
            {
                #region 有查询条件的处理
                if (strwhere.ToString().Trim() != "")
                {
                    strSql.Append(" select COUNT(*) from (SELECT NRequestForm.ZDY10,NRequestForm.NRequestFormNo,NRequestForm.OldSerialNo,NRequestForm.CName as CName, NRequestForm.DoctorName as DoctorName,NRequestForm.DeptName as DeptName,NRequestForm.WebLisSourceOrgID, NRequestForm.WebLisSourceOrgName as WebLisSourceOrgName,NRequestForm.ClientNo,NRequestForm.jztype, NRequestForm.jztypeName,NRequestForm.PatNo,NRequestForm.ClientName as ClientName, NRequestForm.AgeUnitName as AgeUnitName,NRequestForm.Diag as Diag,GenderType.CName AS Sex,NRequestForm.Age as Age, SampleType.CName AS SampleName,Convert(varchar(10),NRequestForm.operdate,21)+' '+Convert(varchar(10), NRequestForm.opertime,8)as incepttime,Convert(varchar(10),NRequestForm.CollectDate,21)+' '+Convert(varchar(10), NRequestForm.CollectTime,8)as CollectTime,NRequestForm.OperDate as OperDate,NRequestForm.OperTime as OperTime, PatDiagInfo.DiagDesc as DiagDesc,NRequestForm.zdy5 FROM dbo.GenderType RIGHT OUTER JOIN dbo.NRequestForm LEFT OUTER JOIN dbo.PatDiagInfo ON dbo.NRequestForm.DiagNo = dbo.PatDiagInfo.DiagNo LEFT OUTER JOIN dbo.SampleType ON dbo.NRequestForm.SampleTypeNo = dbo.SampleType.SampleTypeNo ON dbo.GenderType.GenderNo = dbo.NRequestForm.GenderNo LEFT JOIN dbo.NRequestItem ON NRequestForm.NRequestFormNo = dbo.NRequestItem.NRequestFormNo LEFT JOIN dbo.BarCodeForm ON dbo.NRequestItem.BarCodeFormNo = dbo.BarCodeForm.BarCodeFormNo where ");

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
            //添加按明细条码查询
            if (model.BarCode != null && model.BarCode.Trim() != "")
            {
                strwhere.Append(" and BarCodeForm.BarCode='" + model.BarCode + "'  ");
            }
            //消费码
            if (model.ZDY10 != null)
            {
                strwhere.Append(" and NRequestForm.ZDY10='" + model.ZDY10 + "' ");
            }
            #endregion
            return strwhere;
        }

        public DataSet GetNRequstFormList_SampleSendNo(Model.NRequestForm nrf_m, int startPage, int pageSize)
        {
            throw new NotImplementedException();
        }

        public int SendBarCodeFromByBarCodeList(string barCodeList, string userId, string employeeName)
        {
            throw new NotImplementedException();
        }

        public int DeliveryBarCodeFromByBarCodeList(string barCodeList, string userId, string employeeName, bool flag, string reason)
        {
            throw new NotImplementedException();
        }

        public int ReceiveBarCodeFromByBarCodeList(string barCodeList, string userId, string employeeName, bool flag, string reason, string weblisorgid, string weblisorgname)
        {
            throw new NotImplementedException();
        }

        public DataSet GetListByBarCodeList(List<string> BarCodeList)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

