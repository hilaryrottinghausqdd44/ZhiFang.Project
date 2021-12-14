using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
namespace ZhiFang.DAL.Oracle.weblis
{
    //TestItem

    public partial class TestItem : BaseDALLisDB, IDTestItem, IDBatchCopy, IDGetListByTimeStampe
    {
        public TestItem(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public TestItem()
        {
        }
        //D_LogInfo d_log = new D_LogInfo();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.TestItem model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.ItemNo != null)
            {
                strSql1.Append("ItemNo,");
                strSql2.Append("" + model.ItemNo + ",");
            }
            if (model.CName != null)
            {
                strSql1.Append("CName,");
                strSql2.Append("'" + model.CName + "',");
            }
            if (model.EName != null)
            {
                strSql1.Append("EName,");
                strSql2.Append("'" + model.EName + "',");
            }
            if (model.ShortName != null)
            {
                strSql1.Append("ShortName,");
                strSql2.Append("'" + model.ShortName + "',");
            }
            if (model.ShortCode != null)
            {
                strSql1.Append("ShortCode,");
                strSql2.Append("'" + model.ShortCode + "',");
            }
            if (model.DiagMethod != null)
            {
                strSql1.Append("DiagMethod,");
                strSql2.Append("'" + model.DiagMethod + "',");
            }
            if (model.Unit != null)
            {
                strSql1.Append("Unit,");
                strSql2.Append("'" + model.Unit + "',");
            }
            if (model.IsCalc != null)
            {
                strSql1.Append("IsCalc,");
                strSql2.Append("" + model.IsCalc + ",");
            }
            if (model.Visible != null)
            {
                strSql1.Append("Visible,");
                strSql2.Append("" + model.Visible + ",");
            }
            if (model.DispOrder != null)
            {
                strSql1.Append("DispOrder,");
                strSql2.Append("" + model.DispOrder + ",");
            }
            if (model.Prec != null)
            {
                strSql1.Append("Prec,");
                strSql2.Append("" + model.Prec + ",");
            }
            if (model.IsProfile != null)
            {
                strSql1.Append("IsProfile,");
                strSql2.Append("" + model.IsProfile + ",");
            }
            if (model.OrderNo != null)
            {
                strSql1.Append("OrderNo,");
                strSql2.Append("'" + model.OrderNo + "',");
            }
            if (model.StandardCode != null)
            {
                strSql1.Append("StandardCode,");
                strSql2.Append("'" + model.StandardCode + "',");
            }
            if (model.ItemDesc != null)
            {
                strSql1.Append("ItemDesc,");
                strSql2.Append("'" + model.ItemDesc + "',");
            }
            if (model.FWorkLoad != null)
            {
                strSql1.Append("FWorkLoad,");
                strSql2.Append("" + model.FWorkLoad + ",");
            }
            if (model.Secretgrade != null)
            {
                strSql1.Append("Secretgrade,");
                strSql2.Append("" + model.Secretgrade + ",");
            }
            if (model.Cuegrade != null)
            {
                strSql1.Append("Cuegrade,");
                strSql2.Append("" + model.Cuegrade + ",");
            }
            if (model.IsDoctorItem != null)
            {
                strSql1.Append("IsDoctorItem,");
                strSql2.Append("" + model.IsDoctorItem + ",");
            }
            if (model.IschargeItem != null)
            {
                strSql1.Append("IschargeItem,");
                strSql2.Append("" + model.IschargeItem + ",");
            }
            if (model.Price != null)
            {
                strSql1.Append("price,");
                strSql2.Append("" + model.Price + ",");
            }
            if (model.HisDispOrder != null)
            {
                strSql1.Append("HisDispOrder,");
                strSql2.Append("" + model.HisDispOrder + ",");
            }
            if (model.SuperGroupNo != null)
            {
                strSql1.Append("SuperGroupNo,");
                strSql2.Append("" + model.SuperGroupNo + ",");
            }
            strSql1.Append("tstamp,");
            strSql2.Append("Systimestamp,");

            //if (model.UpLoadTimeL != null)
            //{
            //    strSql1.Append("UpLoadTimeL,");
            //    strSql2.Append("" + model.UpLoadTimeL + ",");
            //}
            //if (model.IntervalInvalid != null)
            //{
            //    strSql1.Append("IntervalInvalid,");
            //    strSql2.Append("" + model.IntervalInvalid + ",");
            //}
            //if (model.IntervalCheck != null)
            //{
            //    strSql1.Append("IntervalCheck,");
            //    strSql2.Append("" + model.IntervalCheck + ",");
            //}
            //if (model.IntervalInceptReceive != null)
            //{
            //    strSql1.Append("IntervalInceptReceive,");
            //    strSql2.Append("" + model.IntervalInceptReceive + ",");
            //}
            //if (model.IntervalCheckSend != null)
            //{
            //    strSql1.Append("IntervalCheckSend,");
            //    strSql2.Append("" + model.IntervalCheckSend + ",");
            //}
            if (model.SpecialType != null)
            {
                strSql1.Append("SpecialType,");
                strSql2.Append("'" + model.SpecialType + "',");
            }
            if (model.SpecialSection != null)
            {
                strSql1.Append("SpecialSection,");
                strSql2.Append("'" + model.SpecialSection + "',");
            }
            //if (model.lowprice != null)
            //{
            //    strSql1.Append("lowprice,");
            //    strSql2.Append("'" + model.lowprice + "',");
            //}
            if (model.SpecTypeNo != null)
            {
                strSql1.Append("SpecTypeNo,");
                strSql2.Append("" + model.SpecTypeNo + ",");
            }
            //if (model.isCombiItem != null)
            //{
            //    strSql1.Append("isCombiItem,");
            //    strSql2.Append("" + model.isCombiItem + ",");
            //}
            if (model.Color != null)
            {
                strSql1.Append("Color,");
                strSql2.Append("'" + model.Color + "',");
            }
            //if (model.isHistoryitem != null)
            //{
            //    strSql1.Append("isHistoryitem,");
            //    strSql2.Append("" + model.isHistoryitem + ",");
            //}
            strSql.Append("insert into TestItem(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");

            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());

            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("insert into TestItem(");
            //strSql.Append("ItemNo,CName,EName,ShortName,ShortCode,DiagMethod,Unit,IsCalc,Visible,DispOrder,Prec,IsProfile,OrderNo,StandardCode,ItemDesc,FWorkLoad,Secretgrade,Cuegrade,IsDoctorItem,IschargeItem,price,isCombiItem,Color,SuperGroupNo");
            //strSql.Append(") values (");
            //strSql.Append("@ItemNo,@CName,@EName,@ShortName,@ShortCode,@DiagMethod,@Unit,@IsCalc,@Visible,@DispOrder,@Prec,@IsProfile,@OrderNo,@StandardCode,@ItemDesc,@FWorkLoad,@Secretgrade,@Cuegrade,@IsDoctorItem,@IschargeItem,@price,@isCombiItem,@Color,@SuperGroupNo");
            //strSql.Append(") ");

            //SqlParameter[] parameters = {
            //            new SqlParameter("@ItemNo", SqlDbType.Int,4) , 

            //            new SqlParameter("@CName", SqlDbType.VarChar,255) ,            
            //            new SqlParameter("@EName", SqlDbType.VarChar,40) ,            
            //            new SqlParameter("@ShortName", SqlDbType.VarChar,40) ,            
            //            new SqlParameter("@ShortCode", SqlDbType.VarChar,40) ,            
            //            new SqlParameter("@DiagMethod", SqlDbType.VarChar,40) ,            
            //            new SqlParameter("@Unit", SqlDbType.VarChar,40) ,            
            //            new SqlParameter("@IsCalc", SqlDbType.Int,4) ,            
            //            new SqlParameter("@Visible", SqlDbType.Int,4) ,            
            //            new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            
            //            new SqlParameter("@Prec", SqlDbType.Int,4) ,            
            //            new SqlParameter("@IsProfile", SqlDbType.Int,4) ,            
            //            new SqlParameter("@OrderNo", SqlDbType.VarChar,20) ,            
            //            new SqlParameter("@StandardCode", SqlDbType.VarChar,20) ,            
            //            new SqlParameter("@ItemDesc", SqlDbType.VarChar,1024) ,            
            //            new SqlParameter("@FWorkLoad", SqlDbType.Float,8) ,            
            //            new SqlParameter("@Secretgrade", SqlDbType.Int,4) ,            
            //            new SqlParameter("@Cuegrade", SqlDbType.Int,4) ,            
            //            new SqlParameter("@IsDoctorItem", SqlDbType.Int,4) ,            
            //            new SqlParameter("@IschargeItem", SqlDbType.Int,4) ,            
            //            new SqlParameter("@price", SqlDbType.Float,8) ,            
            //            new SqlParameter("@isCombiItem", SqlDbType.Int,4) ,            
            //            new SqlParameter("@Color", SqlDbType.VarChar,50),
            //            new SqlParameter("@SuperGroupNo",SqlDbType.Int,4),

            //};

            //parameters[0].Value = model.ItemNo;
            //parameters[1].Value = model.CName;
            //parameters[2].Value = model.EName;
            //parameters[3].Value = model.ShortName;
            //parameters[4].Value = model.ShortCode;
            //parameters[5].Value = model.DiagMethod;
            //parameters[6].Value = model.Unit;
            //parameters[7].Value = model.IsCalc;
            //parameters[8].Value = model.Visible;
            //parameters[9].Value = model.DispOrder;
            //parameters[10].Value = model.Prec;
            //parameters[11].Value = model.IsProfile;
            //parameters[12].Value = model.OrderNo;
            //parameters[13].Value = model.StandardCode;
            //parameters[14].Value = model.ItemDesc;
            //parameters[15].Value = model.FWorkLoad;
            //parameters[16].Value = model.Secretgrade;
            //parameters[17].Value = model.Cuegrade;
            //parameters[18].Value = model.IsDoctorItem;
            //parameters[19].Value = model.IschargeItem;
            //parameters[20].Value = model.price;
            //parameters[21].Value = model.IsCombiItem;
            //parameters[22].Value = model.Color;
            //parameters[23].Value = model.SuperGroupNo;

            //return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.TestItem model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TestItem set ");
            if (model.CName != null)
            {
                strSql.Append("CName='" + model.CName + "',");
            }
            else
            {
                strSql.Append("CName= null ,");
            }
            if (model.EName != null)
            {
                strSql.Append("EName='" + model.EName + "',");
            }
            else
            {
                strSql.Append("EName= null ,");
            }
            if (model.ShortName != null)
            {
                strSql.Append("ShortName='" + model.ShortName + "',");
            }
            else
            {
                strSql.Append("ShortName= null ,");
            }
            if (model.ShortCode != null)
            {
                strSql.Append("ShortCode='" + model.ShortCode + "',");
            }
            else
            {
                strSql.Append("ShortCode= null ,");
            }
            if (model.DiagMethod != null)
            {
                strSql.Append("DiagMethod='" + model.DiagMethod + "',");
            }
            else
            {
                strSql.Append("DiagMethod= null ,");
            }
            if (model.Unit != null)
            {
                strSql.Append("Unit='" + model.Unit + "',");
            }
            else
            {
                strSql.Append("Unit= null ,");
            }
            if (model.IsCalc != null)
            {
                strSql.Append("IsCalc=" + model.IsCalc + ",");
            }
            else
            {
                strSql.Append("IsCalc= null ,");
            }
            if (model.Visible != null)
            {
                strSql.Append("Visible=" + model.Visible + ",");
            }
            else
            {
                strSql.Append("Visible= null ,");
            }
            if (model.DispOrder != null)
            {
                strSql.Append("DispOrder=" + model.DispOrder + ",");
            }
            else
            {
                strSql.Append("DispOrder= null ,");
            }
            if (model.Prec != null)
            {
                strSql.Append("Prec=" + model.Prec + ",");
            }
            else
            {
                strSql.Append("Prec= null ,");
            }
            if (model.IsProfile != null)
            {
                strSql.Append("IsProfile=" + model.IsProfile + ",");
            }
            if (model.OrderNo != null)
            {
                strSql.Append("OrderNo='" + model.OrderNo + "',");
            }
            else
            {
                strSql.Append("OrderNo= null ,");
            }
            if (model.StandardCode != null)
            {
                strSql.Append("StandardCode='" + model.StandardCode + "',");
            }
            else
            {
                strSql.Append("StandardCode= null ,");
            }
            if (model.ItemDesc != null)
            {
                strSql.Append("ItemDesc='" + model.ItemDesc + "',");
            }
            else
            {
                strSql.Append("ItemDesc= null ,");
            }
            if (model.FWorkLoad != null)
            {
                strSql.Append("FWorkLoad=" + model.FWorkLoad + ",");
            }
            else
            {
                strSql.Append("FWorkLoad= null ,");
            }
            if (model.Secretgrade != null)
            {
                strSql.Append("Secretgrade=" + model.Secretgrade + ",");
            }
            else
            {
                strSql.Append("Secretgrade= null ,");
            }
            if (model.Cuegrade != null)
            {
                strSql.Append("Cuegrade=" + model.Cuegrade + ",");
            }
            else
            {
                strSql.Append("Cuegrade= null ,");
            }
            if (model.IsDoctorItem != null)
            {
                strSql.Append("IsDoctorItem=" + model.IsDoctorItem + ",");
            }
            else
            {
                strSql.Append("IsDoctorItem= null ,");
            }
            if (model.IschargeItem != null)
            {
                strSql.Append("IschargeItem=" + model.IschargeItem + ",");
            }
            else
            {
                strSql.Append("IschargeItem= null ,");
            }
            if (model.Price != null)
            {
                strSql.Append("price=" + model.Price + ",");
            }
            else
            {
                strSql.Append("price= null ,");
            }
            if (model.HisDispOrder != null)
            {
                strSql.Append("HisDispOrder=" + model.HisDispOrder + ",");
            }
            else
            {
                strSql.Append("HisDispOrder= null ,");
            }
            if (model.SuperGroupNo != null)
            {
                strSql.Append("SuperGroupNo=" + model.SuperGroupNo + ",");
            }
            else
            {
                strSql.Append("SuperGroupNo= null ,");
            }
            //if (model.UpLoadTimeL != null)
            //{
            //    strSql.Append("UpLoadTimeL=" + model.UpLoadTimeL + ",");
            //}
            //else
            //{
            //    strSql.Append("UpLoadTimeL= null ,");
            //}
            //if (model.IntervalInvalid != null)
            //{
            //    strSql.Append("IntervalInvalid=" + model.IntervalInvalid + ",");
            //}
            //else
            //{
            //    strSql.Append("IntervalInvalid= null ,");
            //}
            //if (model.IntervalCheck != null)
            //{
            //    strSql.Append("IntervalCheck=" + model.IntervalCheck + ",");
            //}
            //else
            //{
            //    strSql.Append("IntervalCheck= null ,");
            //}
            //if (model.IntervalInceptReceive != null)
            //{
            //    strSql.Append("IntervalInceptReceive=" + model.IntervalInceptReceive + ",");
            //}
            //else
            //{
            //    strSql.Append("IntervalInceptReceive= null ,");
            //}
            //if (model.IntervalCheckSend != null)
            //{
            //    strSql.Append("IntervalCheckSend=" + model.IntervalCheckSend + ",");
            //}
            //else
            //{
            //    strSql.Append("IntervalCheckSend= null ,");
            //}
            if (model.SpecialType != null)
            {
                strSql.Append("SpecialType='" + model.SpecialType + "',");
            }
            else
            {
                strSql.Append("SpecialType= null ,");
            }
            if (model.SpecialSection != null)
            {
                strSql.Append("SpecialSection='" + model.SpecialSection + "',");
            }
            else
            {
                strSql.Append("SpecialSection= null ,");
            }
            //if (model.lowprice != null)
            //{
            //    strSql.Append("lowprice='" + model.lowprice + "',");
            //}
            //else
            //{
            //    strSql.Append("lowprice= null ,");
            //}
            if (model.SpecTypeNo != null)
            {
                strSql.Append("SpecTypeNo=" + model.SpecTypeNo + ",");
            }
            else
            {
                strSql.Append("SpecTypeNo= null ,");
            }
            //if (model.isCombiItem != null)
            //{
            //    strSql.Append("isCombiItem=" + model.isCombiItem + ",");
            //}
            //else
            //{
            //    strSql.Append("isCombiItem= null ,");
            //}
            if (model.Color != null)
            {
                strSql.Append("Color='" + model.Color + "',");
            }
            else
            {
                strSql.Append("Color= null ,");
            }
            //if (model.isHistoryitem != null)
            //{
            //    strSql.Append("isHistoryitem=" + model.isHistoryitem + ",");
            //}
            //else
            //{
            //    strSql.Append("isHistoryitem= null ,");
            //}
            strSql.Append("tstamp= Systimestamp,");

            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where ItemNo=" + model.ItemNo + " ");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());


            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("update TestItem set ");

            //strSql.Append(" ItemNo = @ItemNo , ");
            //strSql.Append(" CName = @CName , ");
            //strSql.Append(" EName = @EName , ");
            //strSql.Append(" ShortName = @ShortName , ");
            //strSql.Append(" ShortCode = @ShortCode , ");
            //strSql.Append(" DiagMethod = @DiagMethod , ");
            //strSql.Append(" Unit = @Unit , ");
            //strSql.Append(" IsCalc = @IsCalc , ");
            //strSql.Append(" Visible = @Visible , ");
            //strSql.Append(" DispOrder = @DispOrder , ");
            //strSql.Append(" Prec = @Prec , ");
            //strSql.Append(" IsProfile = @IsProfile , ");
            //strSql.Append(" OrderNo = @OrderNo , ");
            //strSql.Append(" StandardCode = @StandardCode , ");
            //strSql.Append(" ItemDesc = @ItemDesc , ");
            //strSql.Append(" FWorkLoad = @FWorkLoad , ");
            //strSql.Append(" Secretgrade = @Secretgrade , ");
            //strSql.Append(" Cuegrade = @Cuegrade , ");
            //strSql.Append(" IsDoctorItem = @IsDoctorItem , ");
            //strSql.Append(" IschargeItem = @IschargeItem , ");
            //strSql.Append(" price = @price , ");
            //strSql.Append(" isCombiItem = @isCombiItem , ");
            //strSql.Append(" Color = @Color,  ");
            //strSql.Append("SuperGroupNo=@SuperGroupNo ");
            //strSql.Append(" where ItemNo=@ItemNo  ");

            //SqlParameter[] parameters = {

            //new SqlParameter("@ItemNo", SqlDbType.Int,4) ,            	

            //new SqlParameter("@CName", SqlDbType.VarChar,255) ,            	

            //new SqlParameter("@EName", SqlDbType.VarChar,40) ,            	

            //new SqlParameter("@ShortName", SqlDbType.VarChar,40) ,            	

            //new SqlParameter("@ShortCode", SqlDbType.VarChar,40) ,            	

            //new SqlParameter("@DiagMethod", SqlDbType.VarChar,40) ,            	

            //new SqlParameter("@Unit", SqlDbType.VarChar,40) ,            	

            //new SqlParameter("@IsCalc", SqlDbType.Int,4) ,            	

            //new SqlParameter("@Visible", SqlDbType.Int,4) ,            	

            //new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            	

            //new SqlParameter("@Prec", SqlDbType.Int,4) ,            	

            //new SqlParameter("@IsProfile", SqlDbType.Int,4) ,            	

            //new SqlParameter("@OrderNo", SqlDbType.VarChar,20) ,            	

            //new SqlParameter("@StandardCode", SqlDbType.VarChar,20) ,            	

            //new SqlParameter("@ItemDesc", SqlDbType.VarChar,1024) ,            	

            //new SqlParameter("@FWorkLoad", SqlDbType.Float,8) ,            	

            //new SqlParameter("@Secretgrade", SqlDbType.Int,4) ,            	

            //new SqlParameter("@Cuegrade", SqlDbType.Int,4) ,            	

            //new SqlParameter("@IsDoctorItem", SqlDbType.Int,4) ,            	

            //new SqlParameter("@IschargeItem", SqlDbType.Int,4) ,            	

            //new SqlParameter("@price", SqlDbType.Float,8) ,            	

            //new SqlParameter("@isCombiItem", SqlDbType.Int,4) ,            	

            //new SqlParameter("@Color", SqlDbType.VarChar,50) ,           	
            //new SqlParameter("@SuperGroupNo",SqlDbType.Int,4)
            //};


            //if (model.ItemNo != null)
            //{
            //    parameters[0].Value = model.ItemNo;
            //}



            //if (model.CName != null)
            //{
            //    parameters[1].Value = model.CName;
            //}



            //if (model.EName != null)
            //{
            //    parameters[2].Value = model.EName;
            //}



            //if (model.ShortName != null)
            //{
            //    parameters[3].Value = model.ShortName;
            //}



            //if (model.ShortCode != null)
            //{
            //    parameters[4].Value = model.ShortCode;
            //}



            //if (model.DiagMethod != null)
            //{
            //    parameters[5].Value = model.DiagMethod;
            //}



            //if (model.Unit != null)
            //{
            //    parameters[6].Value = model.Unit;
            //}



            //if (model.IsCalc != null)
            //{
            //    parameters[7].Value = model.IsCalc;
            //}



            //if (model.Visible != null)
            //{
            //    parameters[8].Value = model.Visible;
            //}



            //if (model.DispOrder != null)
            //{
            //    parameters[9].Value = model.DispOrder;
            //}



            //if (model.Prec != null)
            //{
            //    parameters[10].Value = model.Prec;
            //}



            //if (model.IsProfile != null)
            //{
            //    parameters[11].Value = model.IsProfile;
            //}



            //if (model.OrderNo != null)
            //{
            //    parameters[12].Value = model.OrderNo;
            //}



            //if (model.StandardCode != null)
            //{
            //    parameters[13].Value = model.StandardCode;
            //}



            //if (model.ItemDesc != null)
            //{
            //    parameters[14].Value = model.ItemDesc;
            //}



            //if (model.FWorkLoad != null)
            //{
            //    parameters[15].Value = model.FWorkLoad;
            //}



            //if (model.Secretgrade != null)
            //{
            //    parameters[16].Value = model.Secretgrade;
            //}



            //if (model.Cuegrade != null)
            //{
            //    parameters[17].Value = model.Cuegrade;
            //}



            //if (model.IsDoctorItem != null)
            //{
            //    parameters[18].Value = model.IsDoctorItem;
            //}



            //if (model.IschargeItem != null)
            //{
            //    parameters[19].Value = model.IschargeItem;
            //}



            //if (model.price != null)
            //{
            //    parameters[20].Value = model.price;
            //}



            //if (model.IsCombiItem != null)
            //{
            //    parameters[21].Value = model.IsCombiItem;
            //}



            //if (model.Color != null)
            //{
            //    parameters[22].Value = model.Color;
            //}
            //if (model.SuperGroupNo != null)
            //{
            //    parameters[23].Value = model.SuperGroupNo;
            //}

            //return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string ItemNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TestItem ");
            strSql.Append(" where ItemNo='" + ItemNo + "' ");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());

        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TestItem ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());

        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.TestItem GetModel(string ItemNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ItemNo, CName, EName, ShortName, ShortCode, DiagMethod, Unit, IsCalc, Visible, DispOrder, Prec, IsProfile, OrderNo, StandardCode, ItemDesc, FWorkLoad, Secretgrade, Cuegrade, IsDoctorItem, IschargeItem, price, isCombiItem, Color ,SuperGroupNo  ");
            strSql.Append(" from TestItem ");
            strSql.Append(" where ItemNo='" + ItemNo + "' ");
            //SqlParameter[] parameters = {
            //        new SqlParameter("@ItemNo", SqlDbType.VarChar,50)};
            //parameters[0].Value = ItemNo;


            ZhiFang.Model.TestItem model = new ZhiFang.Model.TestItem();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {

                if (ds.Tables[0].Rows[0]["ItemNo"].ToString() != "")
                {
                    model.ItemNo = ds.Tables[0].Rows[0]["ItemNo"].ToString();
                }

                model.CName = ds.Tables[0].Rows[0]["CName"].ToString();

                model.EName = ds.Tables[0].Rows[0]["EName"].ToString();

                model.ShortName = ds.Tables[0].Rows[0]["ShortName"].ToString();

                model.ShortCode = ds.Tables[0].Rows[0]["ShortCode"].ToString();

                model.DiagMethod = ds.Tables[0].Rows[0]["DiagMethod"].ToString();

                model.Unit = ds.Tables[0].Rows[0]["Unit"].ToString();

                if (ds.Tables[0].Rows[0]["IsCalc"].ToString() != "")
                {
                    model.IsCalc = int.Parse(ds.Tables[0].Rows[0]["IsCalc"].ToString());
                }

                if (ds.Tables[0].Rows[0]["Visible"].ToString() != "")
                {
                    model.Visible = int.Parse(ds.Tables[0].Rows[0]["Visible"].ToString());
                }

                if (ds.Tables[0].Rows[0]["DispOrder"].ToString() != "")
                {
                    model.DispOrder = int.Parse(ds.Tables[0].Rows[0]["DispOrder"].ToString());
                }

                if (ds.Tables[0].Rows[0]["Prec"].ToString() != "")
                {
                    model.Prec = int.Parse(ds.Tables[0].Rows[0]["Prec"].ToString());
                }

                if (ds.Tables[0].Rows[0]["IsProfile"].ToString() != "")
                {
                    model.IsProfile = int.Parse(ds.Tables[0].Rows[0]["IsProfile"].ToString());
                }

                model.OrderNo = ds.Tables[0].Rows[0]["OrderNo"].ToString();

                model.StandardCode = ds.Tables[0].Rows[0]["StandardCode"].ToString();

                model.ItemDesc = ds.Tables[0].Rows[0]["ItemDesc"].ToString();

                if (ds.Tables[0].Rows[0]["FWorkLoad"].ToString() != "")
                {
                    model.FWorkLoad = decimal.Parse(ds.Tables[0].Rows[0]["FWorkLoad"].ToString());
                }

                if (ds.Tables[0].Rows[0]["Secretgrade"].ToString() != "")
                {
                    model.Secretgrade = int.Parse(ds.Tables[0].Rows[0]["Secretgrade"].ToString());
                }

                if (ds.Tables[0].Rows[0]["Cuegrade"].ToString() != "")
                {
                    model.Cuegrade = int.Parse(ds.Tables[0].Rows[0]["Cuegrade"].ToString());
                }

                if (ds.Tables[0].Rows[0]["IsDoctorItem"].ToString() != "")
                {
                    model.IsDoctorItem = int.Parse(ds.Tables[0].Rows[0]["IsDoctorItem"].ToString());
                }

                if (ds.Tables[0].Rows[0]["IschargeItem"].ToString() != "")
                {
                    model.IschargeItem = int.Parse(ds.Tables[0].Rows[0]["IschargeItem"].ToString());
                }

                if (ds.Tables[0].Rows[0]["Price"].ToString() != "")
                {
                    model.Price = decimal.Parse(ds.Tables[0].Rows[0]["Price"].ToString());
                }

                if (ds.Tables[0].Rows[0]["isCombiItem"].ToString() != "")
                {
                    model.IsCombiItem = int.Parse(ds.Tables[0].Rows[0]["isCombiItem"].ToString());
                }

                model.Color = ds.Tables[0].Rows[0]["Color"].ToString();
                if (ds.Tables[0].Rows[0]["SuperGroupNo"].ToString() != "")
                {
                    model.SuperGroupNo = int.Parse(ds.Tables[0].Rows[0]["SuperGroupNo"].ToString());
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
            strSql.Append("select * ");
            strSql.Append(" FROM TestItem ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            try
            {
                Common.Log.Log.Info("方法名GetList,参数strWhere：" + strSql.ToString());
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
            catch
            {
                Common.Log.Log.Debug(DbHelperSQL.ConnectionString);
                return null;
            }
        }


        /// <summary>
        /// 根据实体获取DataSet
        /// </summary>
        public DataSet GetList(ZhiFang.Model.TestItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select concat(concat('(',CONCAT(ItemNo,')')),CName) as ItemNoName,TestItem.* ");
            strSql.Append(" FROM TestItem ");
            if (model.IfUseLike == "1")
            {
                strSql.Append(" where (1=2 ");

                if (model.ItemNo != null)
                {
                    strSql.Append(" or ItemNo like '%" + model.ItemNo + "%' ");
                }
                if (model.CName != null)
                {
                    strSql.Append(" or CName like '%" + model.CName + "%' ");
                }
                if (model.ShortName != null)
                {
                    strSql.Append(" or ShortName like '%" + model.ShortName + "%' ");
                }
                if (model.ShortCode != null)
                {
                    strSql.Append(" or ShortCode like '%" + model.ShortCode + "%' ");
                }

                strSql.Append(" ) ");

            }
            else
            {
                strSql.Append(" where 1=1 ");
                if (model.DispOrder != null)
                {
                    strSql.Append(" and DispOrder=" + model.DispOrder + " ");
                }
                if (model.SuperGroupNo != null)
                {
                    strSql.Append(" and SuperGroupNo=" + model.SuperGroupNo + " ");
                }
                if (model.Prec != null)
                {
                    strSql.Append(" and Prec=" + model.Prec + " ");
                }

                if (model.IsProfile != null && model.IsProfile != -2)
                {
                    strSql.Append(" and IsProfile=" + model.IsProfile + " ");
                }

                if (model.OrderNo != null)
                {
                    strSql.Append(" and OrderNo='" + model.OrderNo + "' ");
                }

                if (model.StandardCode != null)
                {
                    strSql.Append(" and StandardCode='" + model.StandardCode + "' ");
                }

                if (model.ItemDesc != null)
                {
                    strSql.Append(" and ItemDesc='" + model.ItemDesc + "' ");
                }

                if (model.FWorkLoad != null)
                {
                    strSql.Append(" and FWorkLoad=" + model.FWorkLoad + " ");
                }

                if (model.Secretgrade != null)
                {
                    strSql.Append(" and Secretgrade=" + model.Secretgrade + " ");
                }

                if (model.Cuegrade != null)
                {
                    strSql.Append(" and Cuegrade=" + model.Cuegrade + " ");
                }

                if (model.ItemNo != null)
                {
                    strSql.Append(" and ItemNo='" + model.ItemNo + "' ");
                }

                if (model.IsDoctorItem != null)
                {
                    strSql.Append(" and IsDoctorItem=" + model.IsDoctorItem + " ");
                }

                if (model.IschargeItem != null)
                {
                    strSql.Append(" and IschargeItem=" + model.IschargeItem + " ");
                }

                if (model.HisDispOrder != null)
                {
                    strSql.Append(" and HisDispOrder=" + model.HisDispOrder + " ");
                }

                if (model.Price != null)
                {
                    strSql.Append(" and Price=" + model.Price + " ");
                }

                if (model.IsCombiItem != null)
                {
                    strSql.Append(" and IsCombiItem=" + model.IsCombiItem + " ");
                }
                if (model.ItemList != null)
                {
                    strSql.Append(" and ItemNo not in (" + model.ItemList + ")");
                }
                if (model.CName != null)
                {
                    strSql.Append(" and CName like '%" + model.CName + "%' ");
                }

                if (model.IsHistoryItem != null)
                {
                    strSql.Append(" and IsHistoryItem=" + model.IsHistoryItem + " ");
                }

                if (model.IsNurseItem != null)
                {
                    strSql.Append(" and IsNurseItem='" + model.IsNurseItem + "' ");
                }


                if (model.EName != null)
                {
                    strSql.Append(" and EName='" + model.EName + "' ");
                }

                if (model.StandCode != null)
                {
                    strSql.Append(" and StandCode='" + model.StandCode + "' ");
                }

                if (model.ZFStandCode != null)
                {
                    strSql.Append(" and ZFStandCode='" + model.ZFStandCode + "' ");
                }

                if (model.ShortName != null)
                {
                    strSql.Append(" and ShortName='" + model.ShortName + "' ");
                }

                if (model.ShortCode != null)
                {
                    strSql.Append(" and ShortCode='" + model.ShortCode + "' ");
                }

                if (model.DiagMethod != null)
                {
                    strSql.Append(" and DiagMethod='" + model.DiagMethod + "' ");
                }

                if (model.Unit != null)
                {
                    strSql.Append(" and Unit='" + model.Unit + "' ");
                }

                if (model.IsCalc != null)
                {
                    strSql.Append(" and IsCalc=" + model.IsCalc + " ");
                }
            }
            Common.Log.Log.Info("方法名GetList,参数model：" + strSql.ToString());
            //ZhiFang.Common.Log.Log.Debug("Weblis.TestItem.GetList(ZhiFang.Model.TestItem model).strSql=" + strSql.ToString());
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
        public DataSet GetListLike(ZhiFang.Model.TestItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select TestItem.*,ItemNo as LabNo,concat(concat(ItemNo,'_'),CNAME) as LabNo_Value,concat(concat(concat(CName,'('),ItemNo),')') as LabNoAndName_Text ");
            strSql.Append(" FROM TestItem  ");
            if (model.CName != null)
            {
                strSql.Append(" where 1=2 ");
                strSql.Append(" or CName like '%" + model.CName + "%' ");
            }

            if (model.ItemNo != null)
            {
                if (strSql.ToString().IndexOf("where 1=2") < 0)
                {
                    strSql.Append(" where 1=2 ");
                }
                strSql.Append(" or ItemNo like '%" + model.ItemNo + "%' ");
            }

            if (model.ShortName != null)
            {
                if (strSql.ToString().IndexOf("where 1=2") < 0)
                {
                    strSql.Append(" where 1=2 ");
                }
                strSql.Append(" or ShortName like '%" + model.ShortName + "%' ");
            }

            if (model.ShortCode != null)
            {
                if (strSql.ToString().IndexOf("where 1=2") < 0)
                {
                    strSql.Append(" where 1=2 ");
                }
                strSql.Append(" or ShortCode like '%" + model.ShortCode + "%' ");
            }
            Common.Log.Log.Info("方法名GetListLike,参数model：" + strSql.ToString());
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 获取总记录
        /// </summary>
        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM TestItem ");
            Common.Log.Log.Info("方法名GetTotalCount：" + strSql.ToString());
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
        public int GetTotalCount(ZhiFang.Model.TestItem model)
        {
            StringBuilder strSql = new StringBuilder();
            string likesql = " and TestItem.ItemNo is not null and TestItem.ItemNo<>'' and TestItem.CName is not null and TestItem.CName <> '' ";
            if (model.TestItemLikeKey != null && model.TestItemLikeKey.Trim() != "")
            {
                likesql = "  and  ( TestItem.ItemNo like '%" + model.TestItemLikeKey + "%'  or TestItem.CNAME like '%" + model.TestItemLikeKey + "%'  or TestItem.EName like '%" + model.TestItemLikeKey + "%'  or TestItem.ShortName like '%" + model.TestItemLikeKey + "%' or TestItem.ShortCode like '%" + model.TestItemLikeKey + "%' ) ";
            }
            switch (model.TestItemSuperGroupClass)
            {
                case Common.Dictionary.TestItemSuperGroupClass.ALL:
                    strSql.Append("select count(*) FROM TestItem where 1=1 " + likesql);
                    break;
                case Common.Dictionary.TestItemSuperGroupClass.DOCTORCOMBICHARGE:
                    strSql.Append("select count(*) FROM TestItem where 1=1  and (IsDoctorItem=1 or isCombiItem=1 or IschargeItem=1) " + likesql); break;
                case Common.Dictionary.TestItemSuperGroupClass.OFTEN:
                    strSql.Append("select count(*) FROM TestItem where 1=1 " + likesql); break;
                case Common.Dictionary.TestItemSuperGroupClass.COMBI:
                    strSql.Append("select count(*) FROM TestItem where 1=1  and (isCombiItem=1) " + likesql); break;
                case Common.Dictionary.TestItemSuperGroupClass.CHARGE:
                    strSql.Append("select count(*) FROM TestItem where 1=1  and ( IschargeItem=1)  " + likesql); break;//and (isCombiItem=0)
                case Common.Dictionary.TestItemSuperGroupClass.SUPERGROUP:
                    strSql.Append("select count(*) FROM TestItem INNER JOIN    SuperGroup ON TestItem.SuperGroupNo = SuperGroup.SuperGroupNo where 1=1  and SuperGroup.SuperGroupNo =" + model.SuperGroupNo + " " + likesql); break;
                default: strSql.Append("select count(*) FROM TestItem where 1=1 " + likesql); break;
            }

            StringBuilder strWhere = new StringBuilder();
            // strSql.Append("select count(*) FROM TestItem where 1=1 ");
            if (model != null)
            {

                if (model.ItemNo != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or ItemNo like '%" + model.ItemNo + "%' ");
                    else
                        strWhere.Append(" and ( ItemNo like '%" + model.ItemNo + "%' ");
                }
                if (model.CName != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or CName like '%" + model.CName + "%' ");
                    else
                        strWhere.Append(" and ( CName like '%" + model.CName + "%' ");
                }
                if (model.EName != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or EName like '%" + model.EName + "%' ");
                    else
                        strWhere.Append(" and ( EName like '%" + model.EName + "%' ");
                }
                if (model.ShortName != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or ShortName like '%" + model.ShortName + "%' ");
                    else
                        strWhere.Append(" and ( ShortName like '%" + model.ShortName + "%' ");
                }
                if (model.ShortCode != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or ShortCode like '%" + model.ShortCode + "%' ");
                    else
                        strWhere.Append(" and ( ShortCode like '%" + model.ShortCode + "%' ");
                }
                if (model.IsDoctorItem != null)
                {
                    strWhere.Append(" and ( IsDoctorItem =" + model.IsDoctorItem);
                }
                if (strWhere.Length > 0)
                    strWhere.Append(" ) ");
            }
            strSql.Append(strWhere.ToString());
            Common.Log.Log.Info("方法名GetTotalCount,参数model：" + strSql.ToString());
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
        public DataSet GetListByPage(ZhiFang.Model.TestItem model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            if (model != null)
            {

                if (model.ItemNo != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or TestItem.ItemNo like '%" + model.ItemNo + "%'  ");
                    else
                        strWhere.Append(" and ( TestItem.ItemNo like '%" + model.ItemNo + "%'  ");
                }
                if (model.CName != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or TestItem.CName like '%" + model.CName + "%'  ");
                    else
                        strWhere.Append(" and ( TestItem.CName like '%" + model.CName + "%'  ");
                }
                if (model.EName != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or TestItem.EName like '%" + model.EName + "%'  ");
                    else
                        strWhere.Append(" and ( TestItem.EName like '%" + model.EName + "%'  ");
                }
                if (model.ShortName != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or TestItem.ShortName like '%" + model.ShortName + "%'  ");
                    else
                        strWhere.Append(" and ( TestItem.ShortName like '%" + model.ShortName + "%'  ");
                }
                if (model.ShortCode != null)
                {
                    if (strWhere.Length > 0)
                        strWhere.Append(" or TestItem.ShortCode like '%" + model.ShortCode + "%'  ");
                    else
                        strWhere.Append(" and ( TestItem.ShortCode like '%" + model.ShortCode + "%'  ");
                }
                if (strWhere.Length > 0)
                    strWhere.Append(" ) ");
            }
            string strOrderBy = "";
            if (model.OrderField == "ItemID")
            {
                strOrderBy = "TestItem.ItemNo";
            }
            else if (model.OrderField.ToLower().IndexOf("control") >= 0)
            {
                strOrderBy = "B_TestItemControl." + model.OrderField;
            }
            else
            {
                strOrderBy = "TestItem." + model.OrderField;
            }
            if (model != null && model.LabCode != null)
            {
                strSql.Append(" select   * from TestItem left join B_TestItemControl on TestItem.ItemNo=B_TestItemControl.ItemNo ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and B_TestItemControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append("where  ROWNUM <= '" + nowPageSize + "' and TestItem.ItemNo not in ( ");
                strSql.Append("select TestItem.ItemNo from  TestItem left join B_TestItemControl on TestItem.ItemNo=B_TestItemControl.ItemNo ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and B_TestItemControl.ControlLabNo='" + model.LabCode + "' ");
                }
                strSql.Append(" where 1=1 and ROWNUM <= '" + (nowPageSize * nowPageNum) + "' " + strWhere.ToString() + " ) " + strWhere.ToString() + " order by " + strOrderBy + " ");
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }
            else
            {
                //strSql.Append("select top " + nowPageSize + "  * from TestItem where ItemNo not in  ");
                //strSql.Append("(select top " + (nowPageSize * nowPageNum) + " ItemNo from TestItem where 1=1 " + strWhere.ToString() + " order by " + model.OrderField + ") " + strWhere.ToString() + " order by " + model.OrderField + "  ");
                if (model.OrderField == null || model.OrderField == "ItemID")
                    model.OrderField = "ItemNo";
                string likesql = "";
                if (model.TestItemLikeKey != null && model.TestItemLikeKey.Trim() != "")
                {
                    likesql = " and TestItem.ItemNo is not null and TestItem.ItemNo<>'' and TestItem.CName is not null and TestItem.CName <> '' and  ( TestItem.ItemNo like '%" + model.TestItemLikeKey + "%'  or TestItem.CNAME like '%" + model.TestItemLikeKey + "%'  or TestItem.EName like '%" + model.TestItemLikeKey + "%'  or TestItem.ShortName like '%" + model.TestItemLikeKey + "%' or TestItem.ShortCode like '%" + model.TestItemLikeKey + "%' ) ";
                }
                switch (model.TestItemSuperGroupClass)
                {
                    case Common.Dictionary.TestItemSuperGroupClass.ALL:
                        strSql.Append("select  * from TestItem where  ROWNUM <= '" + nowPageSize + "' and ItemNo not in  ");
                        strSql.Append("(select ItemNo from TestItem where 1=1 and ROWNUM <= '" + (nowPageSize * nowPageNum) + "' " + likesql + " ) ");
                        strSql.Append(likesql);
                        strSql.Append(" order by ItemNo  "); break;
                    case Common.Dictionary.TestItemSuperGroupClass.DOCTORCOMBICHARGE:
                        strSql.Append("select  * from TestItem where  ROWNUM <= '" + nowPageSize + "' and ItemNo not in  ");
                        strSql.Append("(select ItemNo from TestItem where 1=1 and ROWNUM <= '" + (nowPageSize * nowPageNum) + "' and (IsDoctorItem=1 or isCombiItem=1 or IschargeItem=1) " + likesql + " ) ");
                        strSql.Append(" and (IsDoctorItem=1 or isCombiItem=1 or IschargeItem=1)");
                        strSql.Append(likesql);
                        strSql.Append(" order by ItemNo  "); break;
                    case Common.Dictionary.TestItemSuperGroupClass.COMBI:
                        strSql.Append("select * from TestItem where  ROWNUM <= '" + nowPageSize + "' and ItemNo not in  ");
                        strSql.Append("(select ItemNo from TestItem where 1=1 and ROWNUM <= '" + (nowPageSize * nowPageNum) + "' and (isCombiItem=1)  " + likesql + " ) ");
                        strSql.Append(" and (isCombiItem=1) ");
                        strSql.Append(likesql);
                        strSql.Append(" order by ItemNo  "); break;
                    case Common.Dictionary.TestItemSuperGroupClass.CHARGE:
                        strSql.Append("select   * from TestItem where  ROWNUM <= '" + nowPageSize + "' and ItemNo not in  ");
                        strSql.Append("(select ItemNo from TestItem where 1=1 and ROWNUM <= '" + (nowPageSize * nowPageNum) + "' and ( IschargeItem=1) and (isCombiItem=0)  " + likesql + " ) ");
                        strSql.Append(" and ( IschargeItem=1) and (isCombiItem=0) ");
                        strSql.Append(likesql);
                        strSql.Append(" order by ItemNo  "); break;
                    case Common.Dictionary.TestItemSuperGroupClass.SUPERGROUP:
                        strSql.Append("select  * from TestItem INNER JOIN    SuperGroup ON TestItem.SuperGroupNo = SuperGroup.SuperGroupNo where  ROWNUM <= '" + nowPageSize + "' and TestItem.ItemNo not in  ");
                        strSql.Append("(select ItemNo from TestItem INNER JOIN    SuperGroup ON TestItem.SuperGroupNo = SuperGroup.SuperGroupNo where 1=1 and ROWNUM <= '" + (nowPageSize * nowPageNum) + "' and  SuperGroup.SuperGroupNo =" + model.SuperGroupNo + "  " + likesql + " ) ");
                        strSql.Append(" and SuperGroup.SuperGroupNo =" + model.SuperGroupNo + " ");
                        strSql.Append(likesql);
                        strSql.Append(" order by TestItem.ItemNo  ");
                        break;
                    default: strSql.Append("select * from TestItem where  ROWNUM <= '" + nowPageSize + "' and ItemNo not in  ");
                        strSql.Append("(select ItemNo from TestItem where 1=1 and ROWNUM <= '" + (nowPageSize * nowPageNum) + "' " + likesql + " ) ");
                        strSql.Append(likesql);
                        strSql.Append(" order by " + model.OrderField + "  "); break;
                }
                Common.Log.Log.Info("方法名GetListByPage,参数model,nowPageNum,nowPageSize：" + strSql.ToString());
                return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            }

        }

        public bool Exists(string ItemNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from TestItem ");
            strSql.Append(" where ItemNo ='" + ItemNo + "'");
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
        public bool Exists(System.Collections.Hashtable ht)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from TestItem where 1=1 ");
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

        #region IDBatchCopy 成员

        public bool CopyToLab(List<string> lst)
        {
            ///////////////////////////
            System.Collections.ArrayList arrySql = new System.Collections.ArrayList();
            string LabTableName = "B_TestItem";
            LabTableName = "B_Lab_" + LabTableName.Substring(LabTableName.IndexOf("_") + 1, LabTableName.Length - (LabTableName.IndexOf("_") + 1));
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSqlControl = new StringBuilder();
            string TableKey = "ItemNo";
            string TableKeySub = TableKey;
            if (TableKey.ToLower().Contains("no"))
            {
                TableKeySub = TableKey.Substring(0, TableKey.ToLower().IndexOf("no"));
            }
            try
            {
                if (lst.Count > 0)
                {
                    if (lst[0].Trim() == "CopyToLab_LabFirstSelect")
                    {
                        //项目选择性批量复制到客户端
                        if (lst.Count == 1)
                            return true;

                        for (int i = 1; i < lst.Count; i++)
                        {
                            string strItemNos = this.GetCombiItems(lst[i].Trim().Split('|')[0].Trim(), lst[i].Trim().Split('|')[1].Trim());

                            strSql.Append("insert into " + LabTableName + "( LabCode,");
                            strSql.Append(" Visible , DispOrder , Prec , IsProfile ,OrderNo ,StandardCode ,ItemDesc ,FWorkLoad ,Secretgrade ,Cuegrade ,ItemNo ,IsDoctorItem ,IschargeItem,Price,IsCombiItem ,CName ,EName , ShortName , ShortCode , DiagMethod , Unit , IsCalc,Color,UseFlag ");
                            strSql.Append(") select '" + lst[i].Trim().Split('|')[0].Trim() + "' as LabCode, ");
                            strSql.Append("Visible,DispOrder,Prec,IsProfile,OrderNo,StandardCode,ItemDesc,FWorkLoad,Secretgrade,Cuegrade,ItemNo,IsDoctorItem,IschargeItem,Price,IsCombiItem,CName,EName,ShortName,ShortCode,DiagMethod,Unit,IsCalc,Color,1 as UseFlag");
                            strSql.Append(" from TestItem where ItemNo in " + strItemNos + " ");

                            strSqlControl.Append("insert into B_TestItemControl ( ");
                            strSqlControl.Append(" " + TableKeySub + "ControlNo," + TableKey + ",ControlLabNo,Control" + TableKey + " ");
                            strSqlControl.Append(")  select ");
                            strSqlControl.Append("  concat(concat(concat(concat(" + lst[i].Trim().Split('|')[0].Trim() + ",'_')," + TableKey + "),'_')," + TableKey + ") as " + TableKeySub + "ControlNo," + TableKey + ",'" + lst[i].Trim().Split('|')[0].Trim() + "' as ControlLabNo," + TableKey + " ");
                            strSqlControl.Append(" from TestItem where ItemNo in " + strItemNos + " ");

                            arrySql.Add(strSql.ToString());
                            arrySql.Add(strSqlControl.ToString());

                            strSql = new StringBuilder();
                            strSqlControl = new StringBuilder();

                        }
                    }
                    else
                    {

                        //把整个表批量复制到客户端
                        for (int i = 0; i < lst.Count; i++)
                        {
                            string strControlItems = this.GetControlItems(lst[i].Trim());//获取已对照的项目编码

                            strSql.Append("insert into " + LabTableName + "( LabCode,");
                            strSql.Append("Visible , DispOrder , Prec , IsProfile ,OrderNo ,StandardCode ,ItemDesc ,FWorkLoad ,Secretgrade ,Cuegrade ,ItemNo ,IsDoctorItem ,IschargeItem,Price,IsCombiItem ,CName ,EName , ShortName , ShortCode , DiagMethod , Unit , IsCalc,Color ");
                            strSql.Append(") select '" + lst[i].Trim() + "' as LabCode, ");
                            strSql.Append("Visible,DispOrder,Prec,IsProfile,OrderNo,StandardCode,ItemDesc,FWorkLoad,Secretgrade,Cuegrade,ItemNo,IsDoctorItem,IschargeItem,Price,IsCombiItem,CName,EName,ShortName,ShortCode,DiagMethod,Unit,IsCalc,Color");
                            strSql.Append(" from TestItem ");
                            if (strControlItems.Trim() != "")
                                strSql.Append(" where ItemNo not in (" + strControlItems + ")");

                            strSqlControl.Append("insert into B_TestItemControl ( ");
                            strSqlControl.Append(" " + TableKeySub + "ControlNo," + TableKey + ",ControlLabNo,Control" + TableKey + " ");
                            strSqlControl.Append(")  select ");
                            strSqlControl.Append("  concat(concat(concat(concat(" + lst[i].Trim() + ",'_')," + TableKey + "),'_')," + TableKey + ") as " + TableKeySub + "ControlNo," + TableKey + ",'" + lst[i].Trim() + "' as ControlLabNo," + TableKey + " ");
                            strSqlControl.Append(" from TestItem ");
                            if (strControlItems.Trim() != "")
                                strSqlControl.Append(" where ItemNo not in (" + strControlItems + ")");

                            arrySql.Add(strSql.ToString());
                            arrySql.Add(strSqlControl.ToString());

                            strSql = new StringBuilder();
                            strSqlControl = new StringBuilder();

                        }
                    }
                }


                DbHelperSQL.BatchUpdateWithTransaction(arrySql);

                //d_log.OperateLog("TestItem", "", "", DateTime.Now, 1);
                return true;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.TestItem.CopyToLab异常 ", ex);
                return false;
            }

        }

        public string GetCombiItems(string strLabCode, string strCopyList)
        {
            B_Lab_TestItem labdal = new B_Lab_TestItem();
            try
            {
                string strReturn = "";
                strCopyList = strCopyList.Trim().Substring(1, strCopyList.Trim().Length - 2);
                string[] strArr = strCopyList.Split(',');
                for (int i = 0; i < strArr.Length; i++)
                {
                    Model.TestItem model = new Model.TestItem();
                    model.ItemNo = strArr[i].Trim().Replace("'", "");
                    DataSet ds = this.GetList(model, "2");//组套内项目
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                        {
                            bool b = labdal.Exists(strLabCode, ds.Tables[0].Rows[j]["ItemNo"].ToString().Trim());
                            if (!b)
                            {
                                if (strReturn.Trim() == "")
                                    strReturn = "'" + ds.Tables[0].Rows[j]["ItemNo"].ToString().Trim() + "'";
                                else
                                    strReturn = strReturn + ",'" + ds.Tables[0].Rows[j]["ItemNo"].ToString().Trim() + "'";
                            }
                        }
                    }
                }
                if (strReturn.Trim() == "")
                    return "(" + strCopyList + ")";
                else
                    return "(" + strCopyList + "," + strReturn + ")";
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.TestItem.GetCombiItems异常 ", ex);
                return strCopyList;
            }
        }

        public string GetControlItems(string strLabCode)
        {
            string str = "";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ItemNo from B_TestItemControl where ControlLabNo=" + strLabCode);
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (str == "")
                        str = "'" + dr["ItemNo"].ToString().Trim() + "'";
                    else
                        str += ",'" + dr["ItemNo"].ToString().Trim() + "'";
                }
            }
            return str;
        }
        #endregion

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ItemNo", "TestItem");
        }

        public DataSet GetList(int Top, ZhiFang.Model.TestItem model, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");

            strSql.Append(" * ");
            strSql.Append(" FROM TestItem ");



            if (model.ItemNo != null)
            {
                strSql.Append(" and ItemNo='" + model.ItemNo + "' ");
            }


            if (model.CName != null)
            {
                strSql.Append(" and CName='" + model.CName + "' ");
            }


            if (model.EName != null)
            {
                strSql.Append(" and EName='" + model.EName + "' ");
            }


            if (model.ShortName != null)
            {
                strSql.Append(" and ShortName='" + model.ShortName + "' ");
            }


            if (model.ShortCode != null)
            {
                strSql.Append(" and ShortCode='" + model.ShortCode + "' ");
            }


            if (model.DiagMethod != null)
            {
                strSql.Append(" and DiagMethod='" + model.DiagMethod + "' ");
            }


            if (model.Unit != null)
            {
                strSql.Append(" and Unit='" + model.Unit + "' ");
            }


            if (model.IsCalc != null)
            {
                strSql.Append(" and IsCalc='" + model.IsCalc + "' ");
            }


            if (model.DispOrder != null)
            {
                strSql.Append(" and DispOrder='" + model.DispOrder + "' ");
            }


            if (model.Prec != null)
            {
                strSql.Append(" and Prec='" + model.Prec + "' ");
            }


            if (model.IsProfile != null)
            {
                strSql.Append(" and IsProfile='" + model.IsProfile + "' ");
            }


            if (model.OrderNo != null)
            {
                strSql.Append(" and OrderNo='" + model.OrderNo + "' ");
            }


            if (model.StandardCode != null)
            {
                strSql.Append(" and StandardCode='" + model.StandardCode + "' ");
            }


            if (model.ItemDesc != null)
            {
                strSql.Append(" and ItemDesc='" + model.ItemDesc + "' ");
            }


            if (model.FWorkLoad != null)
            {
                strSql.Append(" and FWorkLoad='" + model.FWorkLoad + "' ");
            }


            if (model.Secretgrade != null)
            {
                strSql.Append(" and Secretgrade='" + model.Secretgrade + "' ");
            }


            if (model.Cuegrade != null)
            {
                strSql.Append(" and Cuegrade='" + model.Cuegrade + "' ");
            }


            if (model.IsDoctorItem != null)
            {
                strSql.Append(" and IsDoctorItem='" + model.IsDoctorItem + "' ");
            }


            if (model.IschargeItem != null)
            {
                strSql.Append(" and IschargeItem='" + model.IschargeItem + "' ");
            }


            if (model.Price != null)
            {
                strSql.Append(" and price='" + model.Price + "' ");
            }


            if (model.IsCombiItem != null)
            {
                strSql.Append(" and isCombiItem='" + model.IsCombiItem + "' ");
            }


            if (model.Color != null)
            {
                strSql.Append(" and Color='" + model.Color + "' ");
            }
            if (model.SuperGroupNo != null)
            {
                strSql.Append("and SuperGroupNo='" + model.SuperGroupNo + "'");
            }

            strSql.Append(" and ROWNUM <= '" + Top + "'");

            strSql.Append(" order by " + filedOrder);
            Common.Log.Log.Info("方法名GetList,参数Top,model,filedOrder：" + strSql.ToString());
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

                        if (this.Exists(ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim()))
                        {
                            count += this.UpdateByDataRow(dr);
                        }
                        else
                        {
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
                strSql.Append("insert into TestItem (");
                strSql.Append("ItemNo,CName,EName,ShortName,ShortCode,DiagMethod,Unit,IsCalc,Visible,DispOrder,Prec,IsProfile,OrderNo,StandardCode,ItemDesc,FWorkLoad,Secretgrade,Cuegrade,IsDoctorItem,IschargeItem,price,isCombiItem,Color,SuperGroupNo ");
                strSql.Append(") values (");

                if (dr.Table.Columns["ItemNo"] != null && dr.Table.Columns["ItemNo"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["ItemNo"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["CName"] != null && dr.Table.Columns["CName"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["CName"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["EName"] != null && dr.Table.Columns["EName"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["EName"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["ShortName"] != null && dr.Table.Columns["ShortName"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["ShortName"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["ShortCode"] != null && dr.Table.Columns["ShortCode"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["ShortCode"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["DiagMethod"] != null && dr.Table.Columns["DiagMethod"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["DiagMethod"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["Unit"] != null && dr.Table.Columns["Unit"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["Unit"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["IsCalc"] != null && dr.Table.Columns["IsCalc"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["IsCalc"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["Visible"] != null && dr.Table.Columns["Visible"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["Visible"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["DispOrder"] != null && dr.Table.Columns["DispOrder"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["DispOrder"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["Prec"] != null && dr.Table.Columns["Prec"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["Prec"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["IsProfile"] != null && dr.Table.Columns["IsProfile"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["IsProfile"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["OrderNo"] != null && dr.Table.Columns["OrderNo"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["OrderNo"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["StandardCode"] != null && dr.Table.Columns["StandardCode"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["StandardCode"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["ItemDesc"] != null && dr.Table.Columns["ItemDesc"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["ItemDesc"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["FWorkLoad"] != null && dr.Table.Columns["FWorkLoad"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["FWorkLoad"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["Secretgrade"] != null && dr.Table.Columns["Secretgrade"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["Secretgrade"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["Cuegrade"] != null && dr.Table.Columns["Cuegrade"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["Cuegrade"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["IsDoctorItem"] != null && dr.Table.Columns["IsDoctorItem"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["IsDoctorItem"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["IschargeItem"] != null && dr.Table.Columns["IschargeItem"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["IschargeItem"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["price"] != null && dr.Table.Columns["price"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["price"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["isCombiItem"] != null && dr.Table.Columns["isCombiItem"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["isCombiItem"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }

                if (dr.Table.Columns["Color"] != null && dr.Table.Columns["Color"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["Color"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["SuperGroupNo "] != null && dr.Table.Columns["SuperGroupNo "].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["SuperGroupNo "].ToString().Trim() + "', ");
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
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.TestItem.AddByDataRow 同步数据时异常：", ex);
                return 0;
            }
        }
        public int UpdateByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update TestItem set ");


                if (dr.Table.Columns["CName"] != null && dr.Table.Columns["CName"].ToString().Trim() != "")
                {
                    strSql.Append(" CName = '" + dr["CName"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["EName"] != null && dr.Table.Columns["EName"].ToString().Trim() != "")
                {
                    strSql.Append(" EName = '" + dr["EName"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["ShortName"] != null && dr.Table.Columns["ShortName"].ToString().Trim() != "")
                {
                    strSql.Append(" ShortName = '" + dr["ShortName"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["ShortCode"] != null && dr.Table.Columns["ShortCode"].ToString().Trim() != "")
                {
                    strSql.Append(" ShortCode = '" + dr["ShortCode"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["DiagMethod"] != null && dr.Table.Columns["DiagMethod"].ToString().Trim() != "")
                {
                    strSql.Append(" DiagMethod = '" + dr["DiagMethod"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["Unit"] != null && dr.Table.Columns["Unit"].ToString().Trim() != "")
                {
                    strSql.Append(" Unit = '" + dr["Unit"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["IsCalc"] != null && dr.Table.Columns["IsCalc"].ToString().Trim() != "")
                {
                    strSql.Append(" IsCalc = '" + dr["IsCalc"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["Visible"] != null && dr.Table.Columns["Visible"].ToString().Trim() != "")
                {
                    strSql.Append(" Visible = '" + dr["Visible"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["DispOrder"] != null && dr.Table.Columns["DispOrder"].ToString().Trim() != "")
                {
                    strSql.Append(" DispOrder = '" + dr["DispOrder"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["Prec"] != null && dr.Table.Columns["Prec"].ToString().Trim() != "")
                {
                    strSql.Append(" Prec = '" + dr["Prec"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["IsProfile"] != null && dr.Table.Columns["IsProfile"].ToString().Trim() != "")
                {
                    strSql.Append(" IsProfile = '" + dr["IsProfile"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["OrderNo"] != null && dr.Table.Columns["OrderNo"].ToString().Trim() != "")
                {
                    strSql.Append(" OrderNo = '" + dr["OrderNo"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["StandardCode"] != null && dr.Table.Columns["StandardCode"].ToString().Trim() != "")
                {
                    strSql.Append(" StandardCode = '" + dr["StandardCode"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["ItemDesc"] != null && dr.Table.Columns["ItemDesc"].ToString().Trim() != "")
                {
                    strSql.Append(" ItemDesc = '" + dr["ItemDesc"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["FWorkLoad"] != null && dr.Table.Columns["FWorkLoad"].ToString().Trim() != "")
                {
                    strSql.Append(" FWorkLoad = '" + dr["FWorkLoad"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["Secretgrade"] != null && dr.Table.Columns["Secretgrade"].ToString().Trim() != "")
                {
                    strSql.Append(" Secretgrade = '" + dr["Secretgrade"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["Cuegrade"] != null && dr.Table.Columns["Cuegrade"].ToString().Trim() != "")
                {
                    strSql.Append(" Cuegrade = '" + dr["Cuegrade"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["IsDoctorItem"] != null && dr.Table.Columns["IsDoctorItem"].ToString().Trim() != "")
                {
                    strSql.Append(" IsDoctorItem = '" + dr["IsDoctorItem"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["IschargeItem"] != null && dr.Table.Columns["IschargeItem"].ToString().Trim() != "")
                {
                    strSql.Append(" IschargeItem = '" + dr["IschargeItem"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["price"] != null && dr.Table.Columns["price"].ToString().Trim() != "")
                {
                    strSql.Append(" price = '" + dr["price"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["isCombiItem"] != null && dr.Table.Columns["isCombiItem"].ToString().Trim() != "")
                {
                    strSql.Append(" isCombiItem = '" + dr["isCombiItem"].ToString().Trim() + "' , ");
                }


                if (dr.Table.Columns["Color"] != null && dr.Table.Columns["Color"].ToString().Trim() != "")
                {
                    strSql.Append(" Color = '" + dr["Color"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["SuperGroupno"] != null && dr.Table.Columns["SuperGroupno"].ToString().Trim() != "")
                {
                    strSql.Append(" SuperGroupno = '" + dr["SuperGroupno"].ToString().Trim() + "' , ");
                }
                int n = strSql.ToString().LastIndexOf(",");
                strSql.Remove(n, 1);
                strSql.Append(" where ItemNo='" + dr["ItemNo"].ToString().Trim() + "' ");

                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.TestItem .UpdateByDataRow同步数据时异常：", ex);
                return 0;
            }
        }
        public int DeleteByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                if (dr.Table.Columns["ItemNo"] != null && dr.Table.Columns["ItemNo"].ToString().Trim() != "")
                {
                    strSql.Append("delete from TestItem ");
                    strSql.Append(" where ItemNo='" + dr["ItemNo"].ToString().Trim() + "' ");
                    return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.TestItem .DeleteByDataRow同步数据时异常：", ex);
                return 0;
            }
        }

        #region IDGetListByTimeStampe 成员

        public DataSet GetListByTimeStampe(string LabCode, int dTimeStampe)
        {
            DataSet dsAll = new DataSet();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select TestItem.*,'" + LabCode + "' as LabCode,ItemNo as LabItemNo from TestItem where 1=1 ");
            if (dTimeStampe != -999999)
            {
                strSql.Append(" and TO_CHAR(DTimeStampe,'YYYY-MM-DD:HH24:MI:SS') > '" + dTimeStampe + "' ");
            }
            DataTable dtServer = DbHelperSQL.ExecuteDataSet(strSql.ToString()).Tables[0];
            dtServer.TableName = "ServerDatas";

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("select B_Lab_TestItem.*,LabItemNo as ItemNo from B_Lab_TestItem where 1=1 ");
            if (LabCode.Trim() != "")
            {
                strSql2.Append(" and LabCode= '" + LabCode.Trim() + "' ");
            }
            if (dTimeStampe != -999999)
            {
                strSql2.Append(" and TO_CHAR(DTimeStampe,'YYYY-MM-DD:HH24:MI:SS') > '" + dTimeStampe + "' ");
            }
            DataTable dtLab = DbHelperSQL.ExecuteDataSet(strSql2.ToString()).Tables[0];
            dtLab.TableName = "LabDatas";

            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("select * from B_TestItemControl where 1=1 ");
            if (LabCode.Trim() != "")
            {
                strSql3.Append(" and ControlLabNo= '" + LabCode.Trim() + "' ");
            }
            if (dTimeStampe != -999999)
            {
                strSql3.Append(" and TO_CHAR(DTimeStampe,'YYYY-MM-DD:HH24:MI:SS') > '" + dTimeStampe + "' ");
            }
            Common.Log.Log.Info("方法名GetListByTimeStampe：" + strSql.ToString());
            DataTable dtControl = DbHelperSQL.ExecuteDataSet(strSql3.ToString()).Tables[0];
            dtControl.TableName = "ControlDatas";

            dsAll.Tables.Add(dtServer.Copy());
            dsAll.Tables.Add(dtLab.Copy());
            dsAll.Tables.Add(dtControl.Copy());
            return dsAll;
        }

        #endregion



        #region IDTestItem 成员
        public DataSet GetList(ZhiFang.Model.TestItem model, string flag)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select concat(concat('(',CONCAT(ItemNo,')')),CName) as ItemNoName,TestItem.* ");
            strSql.Append(" FROM TestItem where 1=1 ");
            if (flag == "1")
            {//组套外项目
                //strSql.Append(" and (isCombiItem=1 or IsDoctorItem=1 or IschargeItem=1) ");
                if (model.ItemNo != null && model.ItemNo.Trim() != "")
                {
                    strSql.Append(" and ItemNo<>'" + model.ItemNo + "' and ItemNo not in (select ItemNo from GroupItem where PItemNo='" + model.ItemNo + "') ");
                }
                if (model.SearchKey != null && model.SearchKey.Trim() != "")
                {
                    strSql.Append(" and (itemno like '%" + model.SearchKey + "%' or cname like '%" + model.SearchKey + "%' or ShortCode like '%" + model.SearchKey + "%' or ShortName like '%" + model.SearchKey + "%' ) ");
                }
                if (model.PItemNos != null && model.PItemNos.Trim() != "")
                {
                    strSql.Append(" and ItemNo not in (" + model.PItemNos + ") ");
                }
            }
            else if (flag == "2")
            { //组套内项目
                strSql.Append(" and ItemNo in (select ItemNo from GroupItem where PItemNo='" + model.ItemNo + "') ");
            }
            else
            { }
            Common.Log.Log.Info("方法名GetList 参数：model，flag：" + strSql.ToString());
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        #endregion


        public DataSet getItemCName(string ItemNo)
        {
            DataSet dsAll = new DataSet();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select*from TestItem ");
            if (ItemNo != "")
            {
                strSql.Append(" where ItemNo in (" + ItemNo + ")");
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        //public DataSet getTestItemByCombiItem(string SuperGroup)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    switch ((ZhiFang.Common.Dictionary.TestItemSuperGroupClass)Enum.Parse(typeof(ZhiFang.Common.Dictionary.TestItemSuperGroupClass), SuperGroup.ToUpper()))
        //    {
        //        case ZhiFang.Common.Dictionary.TestItemSuperGroupClass.ALL:
        //            strSql.Append("select * from TestItem ");
        //            break;
        //        case ZhiFang.Common.Dictionary.TestItemSuperGroupClass.COMBI:
        //            strSql.Append(" select * from TestItem where isCombiItem = 1");
        //            break;
        //    }
        //    return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        //}


        public bool IsExist(string labCodeNo)
        {
            throw new NotImplementedException();
        }

        public bool DeleteByLabCode(string LabCodeNo)
        {
            throw new NotImplementedException();
        }


        public int Add(List<Model.TestItem> modelList)
        {
            throw new NotImplementedException();
        }


        public int Update(List<Model.TestItem> modelList)
        {
            throw new NotImplementedException();
        }
    }
}

