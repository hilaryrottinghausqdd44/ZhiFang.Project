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
    public partial class B_Lab_TestItem : BaseDALLisDB, IDLab_TestItem, IDBatchCopy
    {

        public B_Lab_TestItem(string dbsourceconn)
        {
            DbHelperSQL = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public B_Lab_TestItem()
        {

        }
        D_LogInfo d_log = new D_LogInfo();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.Lab_TestItem model)
        {

            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.LabCode != null)
            {
                strSql1.Append("LabCode,");
                strSql2.Append("'" + model.LabCode + "',");
            }
            if (model.LabItemNo != null)
            {
                strSql1.Append("ItemNo,");
                strSql2.Append("'" + model.LabItemNo + "',");
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
            if (model.IsCombiItem != null)
            {
                strSql1.Append("isCombiItem,");
                strSql2.Append("" + model.IsCombiItem + ",");
            }
            if (model.Color != null)
            {
                strSql1.Append("Color,");
                strSql2.Append("'" + model.Color + "',");
            }
            strSql1.Append("DTimeStampe,");
            strSql2.Append("Systimestamp,");

            if (model.AddTime != null)
            {
                strSql1.Append("AddTime,");
                strSql2.Append(" to_date('" + model.AddTime.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");
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
            if (model.LabSuperGroupNo != null)
            {
                strSql1.Append("LabSuperGroupNo,");
                strSql2.Append("" + model.LabSuperGroupNo + ",");
            }
            if (model.Price != null)
            {
                strSql1.Append("Price,");
                strSql2.Append("'" + model.Price + "',");
            }
            if (model.PhysicalFlag != null)
            {
                strSql1.Append("PhysicalFlag,");
                strSql2.Append("" + model.PhysicalFlag + ",");
            }
            strSql.Append("insert into B_Lab_TestItem(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            if (DbHelperSQL.ExecuteNonQuery(strSql.ToString()) > 0)
            {
                return d_log.OperateLog("B_Lab_TestItem", "", "", DateTime.Now, 1);
            }
            else
                return -1;

            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("insert into B_Lab_TestItem(");
            //strSql.Append("IsCalc,Visible,DispOrder,Prec,IsProfile,OrderNo,StandardCode,ItemDesc,FWorkLoad,Secretgrade,LabCode,Cuegrade,IsDoctorItem,IschargeItem,price,isCombiItem,Color,StandCode,ZFStandCode,ItemNo,UseFlag,CName,EName,ShortName,ShortCode,DiagMethod,Unit,LabSuperGroupNo");
            //strSql.Append(") values (");
            //strSql.Append("@IsCalc,@Visible,@DispOrder,@Prec,@IsProfile,@OrderNo,@StandardCode,@ItemDesc,@FWorkLoad,@Secretgrade,@LabCode,@Cuegrade,@IsDoctorItem,@IschargeItem,@price,@isCombiItem,@Color,@StandCode,@ZFStandCode,@ItemNo,@UseFlag,@CName,@EName,@ShortName,@ShortCode,@DiagMethod,@Unit,@LabSuperGroupNo");
            //strSql.Append(") ");

            //SqlParameter[] parameters = {
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
            //            new SqlParameter("@LabCode", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@Cuegrade", SqlDbType.Int,4) ,            
            //            new SqlParameter("@IsDoctorItem", SqlDbType.Int,4) ,            
            //            new SqlParameter("@IschargeItem", SqlDbType.Int,4) ,            
            //            new SqlParameter("@price", SqlDbType.Float,8) ,            
            //            new SqlParameter("@isCombiItem", SqlDbType.Int,4) ,            
            //            new SqlParameter("@Color", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@ZFStandCode", SqlDbType.VarChar,50) ,            
            //            new SqlParameter("@ItemNo", SqlDbType.VarChar,500) ,            
            //            new SqlParameter("@UseFlag", SqlDbType.Int,4) ,            
            //            new SqlParameter("@CName", SqlDbType.VarChar,255) ,            
            //            new SqlParameter("@EName", SqlDbType.VarChar,40) ,            
            //            new SqlParameter("@ShortName", SqlDbType.VarChar,40) ,            
            //            new SqlParameter("@ShortCode", SqlDbType.VarChar,40) ,            
            //            new SqlParameter("@DiagMethod", SqlDbType.VarChar,40) ,            
            //            new SqlParameter("@Unit", SqlDbType.VarChar,40),    
            //            new SqlParameter("@LabSuperGroupNo",SqlDbType.Int,4)

            //};

            //parameters[0].Value = model.IsCalc;
            //parameters[1].Value = model.Visible;
            //parameters[2].Value = model.DispOrder;
            //parameters[3].Value = model.Prec;
            //parameters[4].Value = model.IsProfile;
            //parameters[5].Value = model.OrderNo;
            //parameters[6].Value = model.StandardCode;
            //parameters[7].Value = model.ItemDesc;
            //parameters[8].Value = model.FWorkLoad;
            //parameters[9].Value = model.Secretgrade;
            //parameters[10].Value = model.LabCode;
            //parameters[11].Value = model.Cuegrade;
            //parameters[12].Value = model.IsDoctorItem;
            //parameters[13].Value = model.IschargeItem;
            //parameters[14].Value = model.price;
            //parameters[15].Value = model.IsCombiItem;
            //parameters[16].Value = model.Color;
            //parameters[17].Value = model.StandCode;
            //parameters[18].Value = model.ZFStandCode;
            //parameters[19].Value = model.LabItemNo;
            //parameters[20].Value = model.UseFlag;
            //parameters[21].Value = model.CName;
            //parameters[22].Value = model.EName;
            //parameters[23].Value = model.ShortName;
            //parameters[24].Value = model.ShortCode;
            //parameters[25].Value = model.DiagMethod;
            //parameters[26].Value = model.Unit;
            //parameters[27].Value = model.LabSuperGroupNo;
            //return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.Lab_TestItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update B_Lab_TestItem set ");
            if (model.CName != null)
            {
                strSql.Append("CName='" + model.CName + "',");
            }
            if (model.EName != null)
            {
                strSql.Append("EName='" + model.EName + "',");
            }
            if (model.ShortName != null)
            {
                strSql.Append("ShortName='" + model.ShortName + "',");
            }
            if (model.ShortCode != null)
            {
                strSql.Append("ShortCode='" + model.ShortCode + "',");
            }
            if (model.DiagMethod != null)
            {
                strSql.Append("DiagMethod='" + model.DiagMethod + "',");
            }
            if (model.Unit != null)
            {
                strSql.Append("Unit='" + model.Unit + "',");
            }
            if (model.IsCalc != null)
            {
                strSql.Append("IsCalc=" + model.IsCalc + ",");
            }
            if (model.Visible != null)
            {
                strSql.Append("Visible=" + model.Visible + ",");
            }
            if (model.DispOrder != null)
            {
                strSql.Append("DispOrder=" + model.DispOrder + ",");
            }
            if (model.Prec != null)
            {
                strSql.Append("Prec=" + model.Prec + ",");
            }
            if (model.IsProfile != null)
            {
                strSql.Append("IsProfile=" + model.IsProfile + ",");
            }
            if (model.OrderNo != null)
            {
                strSql.Append("OrderNo='" + model.OrderNo + "',");
            }
            if (model.StandardCode != null)
            {
                strSql.Append("StandardCode='" + model.StandardCode + "',");
            }
            if (model.ItemDesc != null)
            {
                strSql.Append("ItemDesc='" + model.ItemDesc + "',");
            }
            if (model.FWorkLoad != null)
            {
                strSql.Append("FWorkLoad=" + model.FWorkLoad + ",");
            }
            if (model.Secretgrade != null)
            {
                strSql.Append("Secretgrade=" + model.Secretgrade + ",");
            }
            if (model.Cuegrade != null)
            {
                strSql.Append("Cuegrade=" + model.Cuegrade + ",");
            }
            if (model.IsDoctorItem != null)
            {
                strSql.Append("IsDoctorItem=" + model.IsDoctorItem + ",");
            }
            if (model.IschargeItem != null)
            {
                strSql.Append("IschargeItem=" + model.IschargeItem + ",");
            }
            if (model.Price != null)
            {
                strSql.Append("Price=" + model.Price + ",");
            }
            if (model.IsCombiItem != null)
            {
                strSql.Append("isCombiItem=" + model.IsCombiItem + ",");
            }
            if (model.Color != null)
            {
                strSql.Append("Color='" + model.Color + "',");
            }
            if (model.AddTime != null)
            {
                strSql.Append(" AddTime=to_date('" + model.AddTime.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");
            }
            if (model.StandCode != null)
            {
                strSql.Append("StandCode='" + model.StandCode + "',");
            }
            if (model.ZFStandCode != null)
            {
                strSql.Append("ZFStandCode='" + model.ZFStandCode + "',");
            }
            if (model.UseFlag != null)
            {
                strSql.Append("UseFlag=" + model.UseFlag + ",");
            }
            if (model.LabSuperGroupNo != null)
            {
                strSql.Append("LabSuperGroupNo=" + model.LabSuperGroupNo + ",");
            }
            if (model.PhysicalFlag != null)
            {
                strSql.Append("PhysicalFlag=" + model.PhysicalFlag + ",");
            }
            strSql.Append("DTimeStampe = Systimestamp,");
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where LabCode='" + model.LabCode + "' and ItemNo='" + model.LabItemNo + "'  ");

            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());

            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("update B_Lab_TestItem set ");

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
            //strSql.Append(" LabCode = @LabCode , ");
            //strSql.Append(" Cuegrade = @Cuegrade , ");
            //strSql.Append(" IsDoctorItem = @IsDoctorItem , ");
            //strSql.Append(" IschargeItem = @IschargeItem , ");
            //strSql.Append(" price = @price , ");
            //strSql.Append(" isCombiItem = @isCombiItem , ");
            //strSql.Append(" Color = @Color , ");
            //strSql.Append(" StandCode = @StandCode , ");
            //strSql.Append(" ZFStandCode = @ZFStandCode , ");
            //strSql.Append(" ItemNo = @ItemNo , ");
            //strSql.Append(" UseFlag = @UseFlag , ");
            //strSql.Append(" CName = @CName , ");
            //strSql.Append(" EName = @EName , ");
            //strSql.Append(" ShortName = @ShortName , ");
            //strSql.Append(" ShortCode = @ShortCode , ");
            //strSql.Append(" DiagMethod = @DiagMethod , ");
            //strSql.Append(" Unit = @Unit,  ");
            //strSql.Append("LabSuperGroupNo=@LabSuperGroupNo");

            //strSql.Append(" where LabCode=@LabCode and ItemNo=@ItemNo  ");

            //SqlParameter[] parameters = {


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

            //new SqlParameter("@LabCode", SqlDbType.VarChar,50) ,            	

            //new SqlParameter("@Cuegrade", SqlDbType.Int,4) ,            	

            //new SqlParameter("@IsDoctorItem", SqlDbType.Int,4) ,            	

            //new SqlParameter("@IschargeItem", SqlDbType.Int,4) ,            	

            //new SqlParameter("@price", SqlDbType.Float,8) ,            	

            //new SqlParameter("@isCombiItem", SqlDbType.Int,4) ,            	

            //new SqlParameter("@Color", SqlDbType.VarChar,50) ,            	



            //new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,            	

            //new SqlParameter("@ZFStandCode", SqlDbType.VarChar,50) ,            	

            //new SqlParameter("@ItemNo", SqlDbType.VarChar,500) ,            	

            //new SqlParameter("@UseFlag", SqlDbType.Int,4) ,            	

            //new SqlParameter("@CName", SqlDbType.VarChar,255) ,            	

            //new SqlParameter("@EName", SqlDbType.VarChar,40) ,            	

            //new SqlParameter("@ShortName", SqlDbType.VarChar,40) ,            	

            //new SqlParameter("@ShortCode", SqlDbType.VarChar,40) ,            	

            //new SqlParameter("@DiagMethod", SqlDbType.VarChar,40) ,            	

            //new SqlParameter("@Unit", SqlDbType.VarChar,40) ,   
            //new SqlParameter("@LabSuperGroupNo",SqlDbType.Int,4)

            //};




            //if (model.IsCalc != null)
            //{
            //    parameters[0].Value = model.IsCalc;
            //}



            //if (model.Visible != null)
            //{
            //    parameters[1].Value = model.Visible;
            //}



            //if (model.DispOrder != null)
            //{
            //    parameters[2].Value = model.DispOrder;
            //}



            //if (model.Prec != null)
            //{
            //    parameters[3].Value = model.Prec;
            //}



            //if (model.IsProfile != null)
            //{
            //    parameters[4].Value = model.IsProfile;
            //}



            //if (model.OrderNo != null)
            //{
            //    parameters[5].Value = model.OrderNo;
            //}



            //if (model.StandardCode != null)
            //{
            //    parameters[6].Value = model.StandardCode;
            //}



            //if (model.ItemDesc != null)
            //{
            //    parameters[7].Value = model.ItemDesc;
            //}



            //if (model.FWorkLoad != null)
            //{
            //    parameters[8].Value = model.FWorkLoad;
            //}



            //if (model.Secretgrade != null)
            //{
            //    parameters[9].Value = model.Secretgrade;
            //}



            //if (model.LabCode != null)
            //{
            //    parameters[10].Value = model.LabCode;
            //}



            //if (model.Cuegrade != null)
            //{
            //    parameters[11].Value = model.Cuegrade;
            //}



            //if (model.IsDoctorItem != null)
            //{
            //    parameters[12].Value = model.IsDoctorItem;
            //}



            //if (model.IschargeItem != null)
            //{
            //    parameters[13].Value = model.IschargeItem;
            //}



            //if (model.price != null)
            //{
            //    parameters[14].Value = model.price;
            //}



            //if (model.IsCombiItem != null)
            //{
            //    parameters[15].Value = model.IsCombiItem;
            //}



            //if (model.Color != null)
            //{
            //    parameters[16].Value = model.Color;
            //}







            //if (model.StandCode != null)
            //{
            //    parameters[17].Value = model.StandCode;
            //}



            //if (model.ZFStandCode != null)
            //{
            //    parameters[18].Value = model.ZFStandCode;
            //}



            //if (model.LabItemNo != null)
            //{
            //    parameters[19].Value = model.LabItemNo;
            //}



            //if (model.UseFlag != null)
            //{
            //    parameters[20].Value = model.UseFlag;
            //}



            //if (model.CName != null)
            //{
            //    parameters[21].Value = model.CName;
            //}



            //if (model.EName != null)
            //{
            //    parameters[22].Value = model.EName;
            //}



            //if (model.ShortName != null)
            //{
            //    parameters[23].Value = model.ShortName;
            //}



            //if (model.ShortCode != null)
            //{
            //    parameters[24].Value = model.ShortCode;
            //}



            //if (model.DiagMethod != null)
            //{
            //    parameters[25].Value = model.DiagMethod;
            //}



            //if (model.Unit != null)
            //{
            //    parameters[26].Value = model.Unit;
            //}
            //if (model.LabSuperGroupNo != null)
            //{
            //    parameters[27].Value = model.LabSuperGroupNo;
            //}

            //return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);
        }

        //更新项目颜色 ganwh add 2015-12-14
        public int UpdateColor(ZhiFang.Model.Lab_TestItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update B_Lab_TestItem set ");
            strSql.Append(" Color = @Color ");
            strSql.Append(" where LabCode=@LabCode and ItemNo=@ItemNo  ");

            SqlParameter[] parameters = {
            new SqlParameter("@LabCode", SqlDbType.VarChar,50) ,
            new SqlParameter("@Color", SqlDbType.VarChar,50) ,
            new SqlParameter("@ItemNo", SqlDbType.VarChar,500)
            };

            if (model.LabCode != null)
            {
                parameters[0].Value = model.LabCode;
            }
            if (model.Color != null)
            {
                parameters[1].Value = model.Color;
            }

            if (model.LabItemNo != null)
            {
                parameters[2].Value = model.LabItemNo;
            }

            return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string LabCode, string ItemNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from B_Lab_TestItem ");
            strSql.Append(" where LabCode=@LabCode and ItemNo=@ItemNo ");
            SqlParameter[] parameters = {
                    new SqlParameter("@LabCode", SqlDbType.VarChar,50),
                    new SqlParameter("@ItemNo", SqlDbType.VarChar,500)};
            parameters[0].Value = LabCode;
            parameters[1].Value = ItemNo;


            return DbHelperSQL.ExecuteNonQuery(strSql.ToString(), parameters);

        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int DeleteList(string ItemIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from B_Lab_TestItem ");
            strSql.Append(" where ID in (" + ItemIDlist + ")  ");
            return DbHelperSQL.ExecuteNonQuery(strSql.ToString());

        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.Lab_TestItem GetModel(string LabCode, string ItemNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ItemID, LabCode, ItemNo, CName, EName, ShortName, ShortCode, DiagMethod, Unit, IsCalc, Visible, DispOrder, Prec, IsProfile, OrderNo, StandardCode, ItemDesc, FWorkLoad, Secretgrade, Cuegrade, IsDoctorItem, IschargeItem, price, DTimeStampe, AddTime, StandCode, ZFStandCode, UseFlag ,LabSuperGroupNo,PhysicalFlag ");
            strSql.Append("  from B_Lab_TestItem ");
            strSql.Append(" where LabCode='" + LabCode + "' and ItemNo='" + ItemNo + "' ");
            //strSql.Append(" where LabCode=@LabCode and ItemNo=@ItemNo ");
            //SqlParameter[] parameters = {
            //        new SqlParameter("@LabCode", SqlDbType.VarChar,50),
            //        new SqlParameter("@ItemNo", SqlDbType.VarChar,500)};
            //parameters[0].Value = LabCode;
            //parameters[1].Value = ItemNo;


            ZhiFang.Model.Lab_TestItem model = new ZhiFang.Model.Lab_TestItem();
            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ItemID"].ToString() != "")
                {
                    model.ItemID = int.Parse(ds.Tables[0].Rows[0]["ItemID"].ToString());
                }
                model.LabCode = ds.Tables[0].Rows[0]["LabCode"].ToString();
                if (ds.Tables[0].Rows[0]["ItemNo"].ToString() != "")
                {
                    model.LabItemNo = ds.Tables[0].Rows[0]["ItemNo"].ToString();
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
                if (ds.Tables[0].Rows[0]["price"].ToString() != "")
                {
                    model.Price = decimal.Parse(ds.Tables[0].Rows[0]["price"].ToString());
                }
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
                if (ds.Tables[0].Rows[0]["LabSuperGroupNo"].ToString() != "")
                {
                    model.LabSuperGroupNo = int.Parse(ds.Tables[0].Rows[0]["LabSuperGroupNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PhysicalFlag"].ToString() != "")
                {
                    model.PhysicalFlag = int.Parse(ds.Tables[0].Rows[0]["PhysicalFlag"].ToString());
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
            strSql.Append(" FROM B_Lab_TestItem ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            Common.Log.Log.Info("B_Lab_TestItem:" + strSql.ToString());
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");

            strSql.Append(" * ");
            strSql.Append(" FROM B_Lab_TestItem ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
                if (Top > 0)
                    strSql.Append(" and ROWNUM <= '" + Top + "'");
            }
            else if (Top > 0)
            {
                strSql.Append(" where ROWNUM <= '" + Top + "'");
            }
            if (filedOrder.Trim() != "")
                strSql.Append(" order by " + filedOrder);
            Common.Log.Log.Info(strSql.ToString());
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 根据实体获取DataSet
        /// </summary>
        public DataSet GetList(ZhiFang.Model.Lab_TestItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ItemNo as LabItemNo,B_Lab_TestItem.* ");
            strSql.Append(" FROM B_Lab_TestItem ");

            if (model.IfUseLike == "1")
            {
                StringBuilder sb = new StringBuilder();
                StringBuilder sb2 = new StringBuilder();
                if (model.LabCode != null)
                {
                    sb.Append(" where 1=1 and LabCode='" + model.LabCode + "' ");
                }

                sb2.Append(" (1=2 ");

                if (model.LabItemNo != null && model.LabItemNo != "")
                {
                    sb2.Append(" or ItemNo like '%" + model.LabItemNo + "%' ");
                }
                if (model.CName != null)
                {
                    sb2.Append(" or CName like '%" + model.CName + "%' ");
                }
                if (model.ShortName != null)
                {
                    sb2.Append(" or ShortName like '%" + model.ShortName + "%' ");
                }
                if (model.ShortCode != null)
                {
                    sb2.Append(" or ShortCode like '%" + model.ShortCode + "%' ");
                }

                sb2.Append(" ) ");

                if (sb.Length > 0)
                {
                    strSql.Append(sb.ToString() + " and " + sb2.ToString());
                }
                else
                {
                    strSql.Append(" where " + sb2.ToString());
                }

            }
            else
            {
                strSql.Append(" where 1=1 ");

                if (model.LabCode != null)
                {
                    strSql.Append(" and LabCode='" + model.LabCode + "' ");
                }

                if (model.LabItemNo != null)
                {
                    strSql.Append(" and ItemNo='" + model.LabItemNo + "' ");
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
                    strSql.Append(" and IsCalc=" + model.IsCalc + " ");
                }

                if (model.DispOrder != null)
                {
                    strSql.Append(" and DispOrder=" + model.DispOrder + " ");
                }

                if (model.Prec != null)
                {
                    strSql.Append(" and Prec=" + model.Prec + " ");
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

                if (model.IsDoctorItem != null)
                {
                    strSql.Append(" and IsDoctorItem=" + model.IsDoctorItem + " ");
                }

                if (model.IschargeItem != null)
                {
                    strSql.Append(" and IschargeItem=" + model.IschargeItem + " ");
                }

                if (model.Price != null)
                {
                    strSql.Append(" and price=" + model.Price + " ");
                }

                if (model.DTimeStampe != null)
                {
                    strSql.Append(" and DTimeStampe='" + model.DTimeStampe + "' ");
                }

                if (model.StandCode != null)
                {
                    strSql.Append(" and StandCode='" + model.StandCode + "' ");
                }

                if (model.ZFStandCode != null)
                {
                    strSql.Append(" and ZFStandCode='" + model.ZFStandCode + "' ");
                }


            }
            Common.Log.Log.Info("B_Lab_TestItem:GetList(model)" + strSql.ToString());
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }
        public DataSet GetListByLike(ZhiFang.Model.Lab_TestItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM B_Lab_TestItem where 1=1 ");
            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }

            if (model.LabItemNo != null)
            {
                strSql.Append(" or ItemNo like '%" + model.LabItemNo + "%' ");
            }

            if (model.CName != null)
            {
                strSql.Append(" or CName like '%" + model.CName + "%' ");
            }

            if (model.EName != null)
            {
                strSql.Append(" or EName like '%" + model.EName + "%' ");
            }

            if (model.ShortName != null)
            {
                strSql.Append(" or ShortName like '%" + model.ShortName + "%' ");
            }

            if (model.ShortCode != null)
            {
                strSql.Append(" or ShortCode like '%" + model.ShortCode + "%' ");
            }

            if (model.DiagMethod != null)
            {
                strSql.Append(" or DiagMethod like '%" + model.DiagMethod + "%' ");
            }

            if (model.Unit != null)
            {
                strSql.Append(" or Unit like '%" + model.Unit + "%' ");
            }

            if (model.IsCalc != null)
            {
                strSql.Append(" or IsCalc like '%" + model.IsCalc + "%' ");
            }

            if (model.Visible != null)
            {
                strSql.Append(" or Visible like '%" + model.Visible + "%' ");
            }

            if (model.DispOrder != null)
            {
                strSql.Append(" or DispOrder like '%" + model.DispOrder + "%' ");
            }

            if (model.Prec != null)
            {
                strSql.Append(" or Prec like '%" + model.Prec + "%' ");
            }

            if (model.IsProfile != null)
            {
                strSql.Append(" or IsProfile like '%" + model.IsProfile + "%' ");
            }

            if (model.OrderNo != null)
            {
                strSql.Append(" or OrderNo like '%" + model.OrderNo + "%' ");
            }

            if (model.StandardCode != null)
            {
                strSql.Append(" or StandardCode like '%" + model.StandardCode + "%' ");
            }

            if (model.ItemDesc != null)
            {
                strSql.Append(" or ItemDesc like '%" + model.ItemDesc + "%' ");
            }

            if (model.FWorkLoad != null)
            {
                strSql.Append(" or FWorkLoad like '%" + model.FWorkLoad + "%' ");
            }

            if (model.Secretgrade != null)
            {
                strSql.Append(" or Secretgrade like '%" + model.Secretgrade + "%' ");
            }

            if (model.Cuegrade != null)
            {
                strSql.Append(" or Cuegrade like '%" + model.Cuegrade + "%' ");
            }

            if (model.IsDoctorItem != null)
            {
                strSql.Append(" or IsDoctorItem like '%" + model.IsDoctorItem + "%' ");
            }

            if (model.IschargeItem != null)
            {
                strSql.Append(" or IschargeItem like '%" + model.IschargeItem + "%' ");
            }

            if (model.Price != null)
            {
                strSql.Append(" or price like '%" + model.Price + "%' ");
            }

            if (model.AddTime != null)
            {
                strSql.Append(" or AddTime like '%" + model.AddTime + "%' ");
            }

            if (model.StandCode != null)
            {
                strSql.Append(" or StandCode like '%" + model.StandCode + "%' ");
            }

            if (model.ZFStandCode != null)
            {
                strSql.Append(" or ZFStandCode like '%" + model.ZFStandCode + "%' ");
            }

            if (model.UseFlag != null)
            {
                strSql.Append(" or UseFlag like '%" + model.UseFlag + "%' ");
            }
            if (model.LabSuperGroupNo != null)
            {
                strSql.Append(" or LabSuperGroupNo like '%" + model.LabSuperGroupNo + "%' ");
            }
            if (model.PhysicalFlag != null)
            {
                strSql.Append(" or PhysicalFlag like '%" + model.PhysicalFlag + "%' ");
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 获取总记录
        /// </summary>
        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_Lab_TestItem ");
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
        public int GetTotalCount(ZhiFang.Model.Lab_TestItem model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            if (model.TestItemSuperGroupClass != null)
            {
                switch (model.TestItemSuperGroupClass)
                {
                    case Common.Dictionary.TestItemSuperGroupClass.ALL:
                        strSql.Append("select count(*) FROM B_Lab_TestItem where 1=1 ");
                        break;
                    case Common.Dictionary.TestItemSuperGroupClass.DOCTORCOMBICHARGE:
                        strSql.Append("select count(*) FROM B_Lab_TestItem where 1=1  and (IsDoctorItem=1 or isCombiItem=1 or IschargeItem=1)"); break;
                    case Common.Dictionary.TestItemSuperGroupClass.OFTEN:
                        strSql.Append("select count(*) FROM B_Lab_TestItem where 1=1 "); break;
                    case Common.Dictionary.TestItemSuperGroupClass.COMBI:
                        strSql.Append("select count(*) FROM B_Lab_TestItem where 1=1  and (isCombiItem=1 or IsProfile=1) "); break;
                    case Common.Dictionary.TestItemSuperGroupClass.CHARGE:
                        strSql.Append("select count(*) FROM B_Lab_TestItem where 1=1  and ( IschargeItem=1) "); break;//and (isCombiItem=0) 
                }
            }
            else
            {
                strSql.Append("select count(*) FROM B_Lab_TestItem where 1=1 ");
            }
            if (model.LabCode != null && model.LabCode != "")
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }
            if (model.LabSuperGroupNo != null)
            {
                strSql.Append(" and LabSuperGroupNo='" + model.LabSuperGroupNo + "' ");
            }
            if (model.IsDoctorItem != null)
            {
                strSql.Append(" and IsDoctorItem='" + model.IsDoctorItem + "' ");
            }
            if (model.UseFlag != null)
            {
                strSql.Append(" and UseFlag=" + model.UseFlag);
            }
            if (model != null)
            {
                if (model.LabItemNo != null)
                {
                    strWhere.Append(" and ( ItemNo like '%" + model.LabItemNo + "%' ");
                }
                if (model.CName != null && model.CName != "")
                {
                    if (strWhere.Length == 0)
                        strWhere.Append(" and ( CName like '%" + model.CName + "%' ");
                    else
                        strWhere.Append(" or CName like '%" + model.CName + "%' ");
                }
                if (model.ShortName != null && model.ShortName != "")
                {
                    if (strWhere.Length == 0)
                        strWhere.Append(" and ( ShortName like '%" + model.ShortName + "%' ");
                    else
                        strWhere.Append(" or ShortName like '%" + model.ShortName + "%' ");
                }
                if (model.ShortCode != null && model.ShortCode != "")
                {
                    if (strWhere.Length == 0)
                        strWhere.Append(" and ( ShortCode like '%" + model.ShortCode + "%' ");
                    else
                        strWhere.Append(" or ShortCode like '%" + model.ShortCode + "%' ");
                }
                if (strWhere.Length != 0)
                    strWhere.Append(" ) ");
            }

            strSql.Append(strWhere.ToString());
            ZhiFang.Common.Log.Log.Info("B_Lab_TestItem----GetTotalCount：" + strSql.ToString());
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

        //#region 对照状态
        //DataSet ds = ibll.GetListByPage(model, pagenum, pagesize);

        //#endregion

        /// <summary>
        /// 利用标识列分页
        /// </summary>
        /// <param name="model">model=null时为中心字典表分页；model!=null时为中心--实验室 对照关系分页</param>
        /// <param name="nowPageNum">第几页</param>
        /// <param name="nowPageSize">每页多少行</param>
        /// <returns>DataSet</returns>
        public DataSet GetListByPage(ZhiFang.Model.Lab_TestItem model, int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();


            if (model.LabItemNo != null)
            {
                strWhere.Append(" and ( ItemNo like '%" + model.LabItemNo + "%' ");
            }
            if (model.CName != null && model.CName != "")
            {
                if (strWhere.Length == 0)
                    strWhere.Append(" and ( CName like '%" + model.CName + "%' ");
                else
                    strWhere.Append(" or CName like '%" + model.CName + "%' ");
            }
            if (model.EName != null && model.EName != "")
            {
                if (strWhere.Length == 0)
                    strWhere.Append(" and ( EName like '%" + model.EName + "%' ");
                else
                    strWhere.Append(" or EName like '%" + model.EName + "%' ");
            }
            if (model.ShortName != null && model.ShortName != "")
            {
                if (strWhere.Length == 0)
                    strWhere.Append(" and ( ShortName like '%" + model.ShortName + "%' ");
                else
                    strWhere.Append(" or ShortName like '%" + model.ShortName + "%' ");
            }
            if (model.ShortCode != null && model.ShortCode != "")
            {
                if (strWhere.Length == 0)
                    strWhere.Append(" and ( ShortCode like '%" + model.ShortCode + "%' ");
                else
                    strWhere.Append(" or ShortCode like '%" + model.ShortCode + "%' ");
            }
            if (model.LabSuperGroupNo != null)
            {
                strWhere.Append(" and ( LabSuperGroupNo = " + model.LabSuperGroupNo);
            }
            if (strWhere.Length != 0)
                strWhere.Append(" ) ");
            if (model.TestItemSuperGroupClass != null)
            {
                switch (model.TestItemSuperGroupClass)
                {
                    case Common.Dictionary.TestItemSuperGroupClass.ALL:
                        strSql.Append("select  concat(concat('(',CONCAT(ItemNo,')')),CName) as ItemNoName,ItemNo as LabItemNo,B_Lab_TestItem.* from B_Lab_TestItem where 1=1  ");
                        if (model.LabCode != null && model.LabCode != "")
                        {
                            strSql.Append(" and LabCode='" + model.LabCode + "' ");
                        }
                        strSql.Append(" " + strWhere.ToString() + " and  ROWNUM <= '" + nowPageSize + "' and ItemID not in ");
                        strSql.Append("(select ItemID from B_Lab_TestItem where 1=1 and ROWNUM <= '" + (nowPageSize * nowPageNum) + "' ");
                        if (model.LabCode != null)
                        {
                            strSql.Append(" and LabCode='" + model.LabCode + "' ");
                        }
                        strSql.Append(" " + strWhere.ToString() + " ) order by " + model.OrderField + "  ");
                        break;
                    case Common.Dictionary.TestItemSuperGroupClass.DOCTORCOMBICHARGE:
                        strSql.Append("select concat(concat('(',CONCAT(ItemNo,')')),CName) as ItemNoName,ItemNo as LabItemNo,B_Lab_TestItem.* from B_Lab_TestItem where 1=1  ");
                        if (model.LabCode != null && model.LabCode != "")
                        {
                            strSql.Append(" and LabCode='" + model.LabCode + "' ");
                        }
                        strSql.Append(" " + strWhere.ToString() + " and  ROWNUM <= '" + nowPageSize + "' and ItemID not in ");
                        strSql.Append("(select ItemID from B_Lab_TestItem where 1=1 and ROWNUM <= '" + (nowPageSize * nowPageNum) + "' and (IsDoctorItem=1 or isCombiItem=1 or IschargeItem=1) ");
                        if (model.LabCode != null)
                        {
                            strSql.Append(" and LabCode='" + model.LabCode + "' ");
                        }
                        strSql.Append(strWhere.ToString() + " ) ");
                        strSql.Append(" and (IsDoctorItem=1 or isCombiItem=1 or IschargeItem=1)");
                        strSql.Append(strWhere.ToString());
                        strSql.Append(" order by ItemNo  "); break;
                    case Common.Dictionary.TestItemSuperGroupClass.COMBI:
                        strSql.Append("select  concat(concat('(',CONCAT(ItemNo,')')),CName) as ItemNoName,ItemNo as LabItemNo,B_Lab_TestItem.* from B_Lab_TestItem where 1=1  ");
                        if (model.LabCode != null && model.LabCode != "")
                        {
                            strSql.Append(" and LabCode='" + model.LabCode + "' ");
                        }
                        if (model.UseFlag != null)
                        {
                            strSql.Append(" and UseFlag='" + model.UseFlag + "' ");
                        }
                        strSql.Append(" " + strWhere.ToString() + " and  ROWNUM <= '" + nowPageSize + "' and ItemID not in ");
                        strSql.Append("(select  ItemID from B_Lab_TestItem where 1=1 and ROWNUM <= '" + (nowPageSize * nowPageNum) + "' and (isCombiItem=1 or IsProfile=1 ) ");
                        if (model.LabCode != null)
                        {
                            strSql.Append(" and LabCode='" + model.LabCode + "' ");
                        }
                        if (model.UseFlag != null)
                        {
                            strSql.Append(" and UseFlag='" + model.UseFlag + "' ");
                        }
                        strSql.Append(strWhere.ToString() + " ) ");
                        strSql.Append(" and (isCombiItem=1 or IsProfile=1)");
                        strSql.Append(strWhere.ToString());
                        strSql.Append(" order by ItemNo  "); break;
                    case Common.Dictionary.TestItemSuperGroupClass.CHARGE:
                        strSql.Append("select concat(concat('(',CONCAT(ItemNo,')')),CName) as ItemNoName,ItemNo as LabItemNo,B_Lab_TestItem.* from B_Lab_TestItem where 1=1  ");
                        if (model.LabCode != null && model.LabCode != "")
                        {
                            strSql.Append(" and LabCode='" + model.LabCode + "' ");
                        }
                        strSql.Append(" " + strWhere.ToString() + " and  ROWNUM <= '" + nowPageSize + "' and ItemID not in ");
                        strSql.Append("(select ItemID from B_Lab_TestItem where 1=1 and ROWNUM <= '" + (nowPageSize * nowPageNum) + "' and ( IschargeItem=1) and (isCombiItem=0) ");
                        if (model.LabCode != null)
                        {
                            strSql.Append(" and LabCode='" + model.LabCode + "' ");
                        }
                        strSql.Append(strWhere.ToString() + " ) ");
                        strSql.Append(" and ( IschargeItem=1) and (isCombiItem=0) ");
                        strSql.Append(strWhere.ToString());
                        strSql.Append(" order by ItemNo  "); break;
                }
            }
            else
            {
                strSql.Append("select  concat(concat('(',CONCAT(ItemNo,')')),CName) as ItemNoName,ItemNo as LabItemNo,B_Lab_TestItem.* from B_Lab_TestItem where 1=1  ");
                if (model.LabCode != null && model.LabCode != "")
                {
                    strSql.Append(" and LabCode='" + model.LabCode + "' ");
                }
                if (model.IsDoctorItem != null)
                {
                    strSql.Append(" and IsDoctorItem='" + model.IsDoctorItem + "' ");
                }

                strSql.Append(" " + strWhere.ToString() + " and  ROWNUM <= '" + nowPageSize + "' and ItemID not in ");
                strSql.Append("(select ItemID from B_Lab_TestItem where 1=1 and ROWNUM <= '" + (nowPageSize * nowPageNum) + "' ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and LabCode='" + model.LabCode + "' ");
                }
                if (model.IsDoctorItem != null)
                {
                    strSql.Append(" and IsDoctorItem='" + model.IsDoctorItem + "' ");
                }
                strSql.Append(" " + strWhere.ToString() + " ) order by " + model.OrderField + "  ");
            }
            Common.Log.Log.Info("B_Lab_TestItem---GetListByPage:" + strSql.ToString());
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
            //return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public bool Exists(string LabCode, string ItemNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from B_Lab_TestItem ");
            strSql.Append(" where LabCode='" + LabCode + "' and ItemNo=" + ItemNo + " ");
            //SqlParameter[] parameters = {
            //        new SqlParameter("@LabCode", SqlDbType.VarChar,50),
            //        new SqlParameter("@ItemNo", SqlDbType.VarChar,50)};
            //parameters[0].Value = LabCode;
            //parameters[1].Value = ItemNo;


            DataSet ds = DbHelperSQL.ExecuteDataSet(strSql.ToString());
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0 && ds.Tables[0].Rows[0][0].ToString().Trim() != "0")
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
            strSql.Append("select count(1) from B_Lab_TestItem where 1=1 ");
            if (ht.Count > 0)
            {
                foreach (System.Collections.DictionaryEntry item in ht)
                {
                    if (item.Key.ToString().Trim() == "LabItemNo")
                        strSql.Append(" and ItemNo = '" + item.Value + "' ");
                    else
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
            return DbHelperSQL.GetMaxID("LabCode,ItemNo", "B_Lab_TestItem");
        }

        public DataSet GetList(int Top, ZhiFang.Model.Lab_TestItem model, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");

            strSql.Append(" * ");
            strSql.Append(" FROM B_Lab_TestItem ");


            if (model.LabCode != null)
            {

                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }

            if (model.LabItemNo != null)
            {
                strSql.Append(" and ItemNo='" + model.LabItemNo + "' ");
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
                strSql.Append(" and IsCalc=" + model.IsCalc + " ");
            }

            if (model.Visible != null)
            {
                strSql.Append(" and Visible=" + model.Visible + " ");
            }

            if (model.DispOrder != null)
            {
                strSql.Append(" and DispOrder=" + model.DispOrder + " ");
            }

            if (model.Prec != null)
            {
                strSql.Append(" and Prec=" + model.Prec + " ");
            }

            if (model.IsProfile != null)
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

            if (model.IsDoctorItem != null)
            {
                strSql.Append(" and IsDoctorItem=" + model.IsDoctorItem + " ");
            }

            if (model.IschargeItem != null)
            {
                strSql.Append(" and IschargeItem=" + model.IschargeItem + " ");
            }

            if (model.Price != null)
            {
                strSql.Append(" and price=" + model.Price + " ");
            }

            if (model.DTimeStampe != null)
            {

                strSql.Append(" and DTimeStampe='" + model.DTimeStampe + "' ");
            }

            if (model.AddTime != null)
            {
                strSql.Append(" and AddTime=to_date('" + model.AddTime.ToString() + "','YYYY-MM-DD HH24:MI:SS'),");

            }

            if (model.StandCode != null)
            {

                strSql.Append(" and StandCode='" + model.StandCode + "' ");
            }

            if (model.ZFStandCode != null)
            {

                strSql.Append(" and ZFStandCode='" + model.ZFStandCode + "' ");
            }

            if (model.UseFlag != null)
            {
                strSql.Append(" and UseFlag=" + model.UseFlag + " ");
            }
            if (model.PhysicalFlag != null)
            {
                strSql.Append(" and PhysicalFlag=" + model.PhysicalFlag + " ");
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
                        if (this.Exists(ds.Tables[0].Rows[i]["LabCode"].ToString().Trim(), ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim()))
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
                strSql.Append("insert into B_Lab_TestItem (");
                strSql.Append("LabCode,ItemNo,CName,EName,ShortName,ShortCode,DiagMethod,Unit,IsCalc,Visible,DispOrder,Prec,IsProfile,OrderNo,StandardCode,ItemDesc,FWorkLoad,Secretgrade,Cuegrade,IsDoctorItem,IschargeItem,price,StandCode,ZFStandCode,UseFlag,PhysicalFlag");
                strSql.Append(") values (");
                if (dr.Table.Columns["LabCode"] != null && dr.Table.Columns["LabCode"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["LabCode"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
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
                    strSql.Append(" '" + dr["UseFlag"].ToString().Trim() + "' ");
                }
                else
                {
                    strSql.Append(" null ");
                }
                if (dr.Table.Columns["PhysicalFlag"] != null && dr.Table.Columns["PhysicalFlag"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["PhysicalFlag"].ToString().Trim() + "' ");
                }
                else
                {
                    strSql.Append(" null ");
                }
                strSql.Append(") ");
                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.B_Lab_TestItem.AddByDataRow 同步数据时异常：", ex);
                return 0;
            }
        }
        public int UpdateByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update B_Lab_TestItem set ");

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

                if (dr.Table.Columns["PhysicalFlag"] != null && dr.Table.Columns["PhysicalFlag"].ToString().Trim() != "")
                {
                    strSql.Append(" PhysicalFlag = '" + dr["PhysicalFlag"].ToString().Trim() + "' , ");
                }
                int n = strSql.ToString().LastIndexOf(",");
                strSql.Remove(n, 1);
                strSql.Append(" where LabCode='" + dr["LabCode"].ToString().Trim() + "' and  ItemNo='" + dr["ItemNo"].ToString().Trim() + "' ");

                return DbHelperSQL.ExecuteNonQuery(strSql.ToString());
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.B_Lab_TestItem .UpdateByDataRow同步数据时异常：", ex);
                return 0;
            }
        }

        public DataSet GetList(Model.Lab_TestItem model, string flag)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select concat(concat('(',CONCAT(ItemNo,')')),CName) as ItemNoName,ItemNo as LabItemNo,B_Lab_TestItem.*  ");
            strSql.Append(" FROM B_Lab_TestItem where 1=1 ");
            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }
            if (flag == "0")
            {
                if (model.LabItemNo != null)
                {
                    strSql.Append(" and ( ItemNo like '%" + model.LabItemNo + "%' ");
                }
                if (model.CName != null)
                {
                    strSql.Append(" or CName like '%" + model.CName + "%' ) ");
                }
            }
            else if (flag == "1")
            {//组套外项目
                //strSql.Append(" and (isCombiItem=1 or IsDoctorItem=1 or IschargeItem=1) ");
                if (model.LabItemNo.Trim() != "")
                {
                    strSql.Append(" and ItemNo<>'" + model.LabItemNo + "' and ItemNo not in (select ItemNo from B_Lab_GroupItem where PItemNo='" + model.LabItemNo + "' ");
                    if (model.LabCode != null)
                    {
                        strSql.Append(" and LabCode='" + model.LabCode + "' ");
                    }
                    strSql.Append(")");
                }
                if (model.SearchKey.Trim() != "")
                {
                    strSql.Append(" and (itemno like '%" + model.SearchKey + "%' or cname like '%" + model.SearchKey + "%' or ShortCode like '%" + model.SearchKey + "%' or ShortName like '%" + model.SearchKey + "%' ) ");
                }
                if (model.PItemNos.Trim() != "")
                {
                    strSql.Append(" and ItemNo not in (" + model.PItemNos + ") ");
                }
            }
            else if (flag == "2")
            { //组套内项目
                strSql.Append(" and ItemNo in (select ItemNo from B_Lab_GroupItem where PItemNo='" + model.LabItemNo + "' ");
                if (model.LabCode != null)
                {
                    strSql.Append(" and LabCode='" + model.LabCode + "' ");
                }
                strSql.Append(")");
            }
            else
            { }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }

        public bool CopyToLab(List<string> lst)
        {
            System.Collections.ArrayList arrySql = new System.Collections.ArrayList();
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSqlControl = new StringBuilder();
            StringBuilder strSqlGroupItem = new StringBuilder();
            try
            {
                if (lst.Count > 0)
                {
                    if (lst[0].Trim().Split('#')[0].Trim() == "CopyToLab_LabFirstSelect")
                    {
                        //项目选择性批量复制到客户端
                        if (lst.Count == 1)
                            return true;

                        for (int i = 1; i < lst.Count; i++)
                        {
                            string strItemNos = this.GetCombiItems(lst[0].Trim().Split('#')[1].Trim(), lst[i].Trim().Split('|')[1].Trim());

                            strSql.Append("insert into B_Lab_TestItem ( LabCode,");
                            strSql.Append(" Visible , DispOrder , Prec , IsProfile ,OrderNo ,StandardCode ,ItemDesc ,FWorkLoad ,Secretgrade ,Cuegrade ,ItemNo ,IsDoctorItem ,IschargeItem,Price,IsCombiItem ,CName ,EName , ShortName , ShortCode , DiagMethod , Unit , IsCalc,Color,StandCode,ZFStandCode,UseFlag,PhysicalFlag ");
                            strSql.Append(") select '" + lst[i].Trim().Split('|')[0].Trim() + "' as LabCode, ");
                            strSql.Append("Visible,DispOrder,Prec,IsProfile,OrderNo,StandardCode,ItemDesc,FWorkLoad,Secretgrade,Cuegrade,ItemNo,IsDoctorItem,IschargeItem,Price,IsCombiItem,CName,EName,ShortName,ShortCode,DiagMethod,Unit,IsCalc,Color,StandCode,ZFStandCode,UseFlag,PhysicalFlag");
                            strSql.Append(" from B_Lab_TestItem where ItemNo in (" + strItemNos + ") and LabCode='" + lst[0].Trim().Split('#')[1].Trim() + "' ");

                            strSqlControl.Append("insert into B_TestItemControl ( ");
                            strSqlControl.Append(" ItemControlNo,ItemNo,ControlLabNo,ControlItemNo ");
                            strSqlControl.Append(")  select ");
                            strSqlControl.Append(" concat(concat(concat(concat(lis[i].Trim().Split('|')[0].Trim(),'_'),ItemNo),'_'),ControlItemNo) as ItemControlNo,ItemNo,'" + lst[i].Trim().Split('|')[0].Trim() + "' as ControlLabNo,ControlItemNo ");
                            strSqlControl.Append(" from B_TestItemControl where ItemNo in (" + strItemNos + ") and ControlLabNo='" + lst[0].Trim().Split('#')[1].Trim() + "' ");

                            strSqlGroupItem.Append("insert into B_Lab_Groupitem (PItemNo,ItemNo,LabCode) ");
                            strSqlGroupItem.Append("select PItemNo,ItemNo,'" + lst[i].Trim().Split('|')[0].Trim() + "' as LabCode from B_Lab_Groupitem ");
                            strSqlGroupItem.Append("where PItemNo in (" + strItemNos + ") and LabCode='" + lst[0].Trim().Split('#')[1].Trim() + "' ");

                            arrySql.Add(strSql.ToString());
                            arrySql.Add(strSqlControl.ToString());
                            arrySql.Add(strSqlGroupItem.ToString());

                            strSql = new StringBuilder();
                            strSqlControl = new StringBuilder();
                            strSqlGroupItem = new StringBuilder();

                        }
                    }
                    else
                    {
                        //把整个表批量复制到客户端
                        for (int i = 1; i < lst.Count; i++)
                        {
                            strSql.Append("insert into B_Lab_TestItem ( LabCode,");
                            strSql.Append("Visible , DispOrder , Prec , IsProfile ,OrderNo ,StandardCode ,ItemDesc ,FWorkLoad ,Secretgrade ,Cuegrade ,ItemNo ,IsDoctorItem ,IschargeItem,Price,IsCombiItem ,CName ,EName , ShortName , ShortCode , DiagMethod , Unit , IsCalc,Color,StandCode,ZFStandCode,UseFlag,PhysicalFlag ");
                            strSql.Append(") select '" + lst[i].Trim() + "' as LabCode, ");
                            strSql.Append("Visible,DispOrder,Prec,IsProfile,OrderNo,StandardCode,ItemDesc,FWorkLoad,Secretgrade,Cuegrade,ItemNo,IsDoctorItem,IschargeItem,Price,IsCombiItem,CName,EName,ShortName,ShortCode,DiagMethod,Unit,IsCalc,Color,StandCode,ZFStandCode,UseFlag,PhysicalFlag");
                            strSql.Append(" from B_Lab_TestItem where LabCode='" + lst[0].Trim() + "' ");

                            strSqlControl.Append("insert into B_TestItemControl ( ");
                            strSqlControl.Append(" ItemControlNo,ItemNo,ControlLabNo,ControlItemNo ");
                            strSqlControl.Append(")  select ");
                            strSqlControl.Append("  concat(concat(concat(concat(" + lst[i].Trim().Split('|')[0].Trim() + ",'_'),ItemNo),'_'),ControlItemNo) as ItemControlNo,ItemNo,'" + lst[i].Trim() + "' as ControlLabNo,ControlItemNo ");
                            strSqlControl.Append(" from B_TestItemControl where ControlLabNo='" + lst[0].Trim() + "' ");

                            strSqlGroupItem.Append("insert into B_Lab_Groupitem (PItemNo,ItemNo,LabCode) ");
                            strSqlGroupItem.Append("select PItemNo,ItemNo,'" + lst[i].Trim() + "' as LabCode from B_Lab_Groupitem ");
                            strSqlGroupItem.Append("where LabCode='" + lst[0].Trim() + "' ");

                            arrySql.Add(strSql.ToString());
                            arrySql.Add(strSqlControl.ToString());
                            arrySql.Add(strSqlGroupItem.ToString());

                            strSql = new StringBuilder();
                            strSqlControl = new StringBuilder();
                            strSqlGroupItem = new StringBuilder();

                        }
                    }
                }


                DbHelperSQL.BatchUpdateWithTransaction(arrySql);

                //d_log.OperateLog("TestItem", "", "", DateTime.Now, 1);
                return true;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.B_Lab_TestItem.CopyToLab异常-->", ex);
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
                    Model.Lab_TestItem model = new Model.Lab_TestItem();
                    model.LabCode = strLabCode;
                    model.LabItemNo = strArr[i].Trim().Replace("'", "");
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
                    return strCopyList;
                else
                    return strCopyList + "," + strReturn;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.Oracle.weblis.TestItem.GetCombiItems异常 ", ex);
                return strCopyList;
            }
        }

        public int DeleteByDataRow(DataRow dr)
        {
            throw new NotImplementedException();
        }



        public DataSet GetLabTestItemByItemNo(string labCode, string ItemNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT   B_Lab_TestItem.* ");
            strSql.Append("FROM   B_Lab_TestItem INNER JOIN");
            strSql.Append(" B_TestItemControl ON B_Lab_TestItem.LabCode = B_TestItemControl.ControlLabNo AND ");
            strSql.Append(" B_Lab_TestItem.ItemNo = B_TestItemControl.ControlItemNo");
            strSql.Append(" where 1=1");

            if (labCode != "")
            {
                strSql.Append(" and B_TestItemControl.ControlLabNo=" + labCode);
            }
            if (ItemNo != "")
            {
                strSql.Append(" and B_TestItemControl.ItemNo='" + ItemNo + "'");
            }
            return DbHelperSQL.ExecuteDataSet(strSql.ToString());
        }


        public bool IsExist(string labCodeNo)
        {
            throw new NotImplementedException();
        }

        public bool DeleteByLabCode(string LabCodeNo)
        {
            throw new NotImplementedException();
        }

        public DataSet GetListByPage(Lab_TestItem model, int nowPageNum, int nowPageSize, string sort, string order)
        {
            throw new NotImplementedException();
        }
    }
}

