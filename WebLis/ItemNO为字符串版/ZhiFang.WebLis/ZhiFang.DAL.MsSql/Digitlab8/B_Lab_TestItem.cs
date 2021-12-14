using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DBUtility;
using ZhiFang.IDAL;
using ZhiFang.Model;

namespace ZhiFang.DAL.MsSql.Digitlab8
{
    //B_Lab_TestItem
    public partial class B_Lab_TestItem : IDLab_TestItem
    {
        DBUtility.IDBConnection idb;
        public B_Lab_TestItem(string dbsourceconn)
        {
            idb = DBUtility.DBFactory.CreateDB(dbsourceconn);
        }
        public B_Lab_TestItem()
        {
            idb = DBUtility.DBFactory.CreateDB(ZhiFang.Common.Dictionary.DBSource.LisDB());
        }
        D_LogInfo d_log = new D_LogInfo();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.Lab_TestItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into B_Lab_TestItem(");
            strSql.Append("IsCalc,Visible,DispOrder,Prec,IsProfile,OrderNo,StandardCode,ItemDesc,FWorkLoad,Secretgrade,LabCode,Cuegrade,IsDoctorItem,IschargeItem,HisDispOrder,SuperGroupNo,price,SpecialType,SpecialSection,LowPrice,SpecTypeNo,LabItemNo,IsCombiItem,IsHistoryItem,IsNurseItem,DefaultSType,SpecName,zdy1,zdy2,code_1,code_2,code_3,CName,StandCode,ZFStandCode,UseFlag,EName,ShortName,ShortCode,DiagMethod,Unit");
            strSql.Append(") values (");
            strSql.Append("@IsCalc,@Visible,@DispOrder,@Prec,@IsProfile,@OrderNo,@StandardCode,@ItemDesc,@FWorkLoad,@Secretgrade,@LabCode,@Cuegrade,@IsDoctorItem,@IschargeItem,@HisDispOrder,@SuperGroupNo,@price,@SpecialType,@SpecialSection,@LowPrice,@SpecTypeNo,@LabItemNo,@IsCombiItem,@IsHistoryItem,@IsNurseItem,@DefaultSType,@SpecName,@zdy1,@zdy2,@code_1,@code_2,@code_3,@CName,@StandCode,@ZFStandCode,@UseFlag,@EName,@ShortName,@ShortCode,@DiagMethod,@Unit");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@IsCalc", SqlDbType.Int,4) ,            
                        new SqlParameter("@Visible", SqlDbType.Int,4) ,            
                        new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            
                        new SqlParameter("@Prec", SqlDbType.Int,4) ,            
                        new SqlParameter("@IsProfile", SqlDbType.Int,4) ,            
                        new SqlParameter("@OrderNo", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@StandardCode", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@ItemDesc", SqlDbType.VarChar,1024) ,            
                        new SqlParameter("@FWorkLoad", SqlDbType.Float,8) ,            
                        new SqlParameter("@Secretgrade", SqlDbType.Int,4) ,            
                        new SqlParameter("@LabCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Cuegrade", SqlDbType.Int,4) ,            
                        new SqlParameter("@IsDoctorItem", SqlDbType.Int,4) ,            
                        new SqlParameter("@IschargeItem", SqlDbType.Int,4) ,            
                        new SqlParameter("@HisDispOrder", SqlDbType.Int,4) ,            
                        new SqlParameter("@SuperGroupNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@price", SqlDbType.Float,8) ,            
                        new SqlParameter("@SpecialType", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@SpecialSection", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@LowPrice", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@SpecTypeNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@LabItemNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@IsCombiItem", SqlDbType.Int,4) ,            
                        new SqlParameter("@IsHistoryItem", SqlDbType.Int,4) ,            
                        new SqlParameter("@IsNurseItem", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@DefaultSType", SqlDbType.Int,4) ,            
                        new SqlParameter("@SpecName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@zdy1", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@zdy2", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@code_1", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@code_2", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@code_3", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@CName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ZFStandCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@UseFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@EName", SqlDbType.VarChar,40) ,            
                        new SqlParameter("@ShortName", SqlDbType.VarChar,40) ,            
                        new SqlParameter("@ShortCode", SqlDbType.VarChar,40) ,            
                        new SqlParameter("@DiagMethod", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@Unit", SqlDbType.VarChar,40)             
              
            };

            parameters[0].Value = model.IsCalc;
            parameters[1].Value = model.Visible;
            parameters[2].Value = model.DispOrder;
            parameters[3].Value = model.Prec;
            parameters[4].Value = model.IsProfile;
            parameters[5].Value = model.OrderNo;
            parameters[6].Value = model.StandardCode;
            parameters[7].Value = model.ItemDesc;
            parameters[8].Value = model.FWorkLoad;
            parameters[9].Value = model.Secretgrade;
            parameters[10].Value = model.LabCode;
            parameters[11].Value = model.Cuegrade;
            parameters[12].Value = model.IsDoctorItem;
            parameters[13].Value = model.IschargeItem;
            parameters[14].Value = model.HisDispOrder;
            parameters[15].Value = model.SuperGroupNo;
            parameters[16].Value = model.Price;
            parameters[17].Value = model.SpecialType;
            parameters[18].Value = model.SpecialSection;
            parameters[19].Value = model.LowPrice;
            parameters[20].Value = model.SpecTypeNo;
            parameters[21].Value = model.LabItemNo;
            parameters[22].Value = model.IsCombiItem;
            parameters[23].Value = model.IsHistoryItem;
            parameters[24].Value = model.IsNurseItem;
            parameters[25].Value = model.DefaultSType;
            parameters[26].Value = model.SpecName;
            parameters[27].Value = model.zdy1;
            parameters[28].Value = model.zdy2;
            parameters[29].Value = model.code_1;
            parameters[30].Value = model.code_2;
            parameters[31].Value = model.code_3;
            parameters[32].Value = model.CName;
            parameters[33].Value = model.StandCode;
            parameters[34].Value = model.ZFStandCode;
            parameters[35].Value = model.UseFlag;
            parameters[36].Value = model.EName;
            parameters[37].Value = model.ShortName;
            parameters[38].Value = model.ShortCode;
            parameters[39].Value = model.DiagMethod;
            parameters[40].Value = model.Unit;
            if (idb.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
            {
                return d_log.OperateLog("TestItem", "", "", DateTime.Now, 1);
            }
            else
                return -1;
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.Lab_TestItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update B_Lab_TestItem set ");

            strSql.Append(" IsCalc = @IsCalc , ");
            strSql.Append(" Visible = @Visible , ");
            strSql.Append(" DispOrder = @DispOrder , ");
            strSql.Append(" Prec = @Prec , ");
            strSql.Append(" IsProfile = @IsProfile , ");
            strSql.Append(" OrderNo = @OrderNo , ");
            strSql.Append(" StandardCode = @StandardCode , ");
            strSql.Append(" ItemDesc = @ItemDesc , ");
            strSql.Append(" FWorkLoad = @FWorkLoad , ");
            strSql.Append(" Secretgrade = @Secretgrade , ");
            strSql.Append(" LabCode = @LabCode , ");
            strSql.Append(" Cuegrade = @Cuegrade , ");
            strSql.Append(" IsDoctorItem = @IsDoctorItem , ");
            strSql.Append(" IschargeItem = @IschargeItem , ");
            strSql.Append(" HisDispOrder = @HisDispOrder , ");
            strSql.Append(" SuperGroupNo = @SuperGroupNo , ");
            strSql.Append(" price = @price , ");
            strSql.Append(" SpecialType = @SpecialType , ");
            strSql.Append(" SpecialSection = @SpecialSection , ");
            strSql.Append(" LowPrice = @LowPrice , ");
            strSql.Append(" SpecTypeNo = @SpecTypeNo , ");
            strSql.Append(" LabItemNo = @LabItemNo , ");
            strSql.Append(" IsCombiItem = @IsCombiItem , ");
            strSql.Append(" IsHistoryItem = @IsHistoryItem , ");
            strSql.Append(" IsNurseItem = @IsNurseItem , ");
            strSql.Append(" DefaultSType = @DefaultSType , ");
            strSql.Append(" SpecName = @SpecName , ");
            strSql.Append(" zdy1 = @zdy1 , ");
            strSql.Append(" zdy2 = @zdy2 , ");
            strSql.Append(" code_1 = @code_1 , ");
            strSql.Append(" code_2 = @code_2 , ");
            strSql.Append(" code_3 = @code_3 , ");
            strSql.Append(" CName = @CName , ");
            strSql.Append(" StandCode = @StandCode , ");
            strSql.Append(" ZFStandCode = @ZFStandCode , ");
            strSql.Append(" UseFlag = @UseFlag , ");
            strSql.Append(" EName = @EName , ");
            strSql.Append(" ShortName = @ShortName , ");
            strSql.Append(" ShortCode = @ShortCode , ");
            strSql.Append(" DiagMethod = @DiagMethod , ");
            strSql.Append(" Unit = @Unit  ");
            strSql.Append(" where LabCode=@LabCode and LabItemNo=@LabItemNo  ");

            SqlParameter[] parameters = {
			            	
                           
            new SqlParameter("@IsCalc", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@Visible", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@DispOrder", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@Prec", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@IsProfile", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@OrderNo", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@StandardCode", SqlDbType.VarChar,100) ,            	
                           
            new SqlParameter("@ItemDesc", SqlDbType.VarChar,1024) ,            	
                           
            new SqlParameter("@FWorkLoad", SqlDbType.Float,8) ,            	
                           
            new SqlParameter("@Secretgrade", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@LabCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@Cuegrade", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@IsDoctorItem", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@IschargeItem", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@HisDispOrder", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@SuperGroupNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@price", SqlDbType.Float,8) ,            	
                           
            new SqlParameter("@SpecialType", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@SpecialSection", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@LowPrice", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@SpecTypeNo", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@LabItemNo", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@IsCombiItem", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@IsHistoryItem", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@IsNurseItem", SqlDbType.VarChar,10) ,            	
                           
            new SqlParameter("@DefaultSType", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@SpecName", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@zdy1", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@zdy2", SqlDbType.VarChar,20) ,            	
                           
            new SqlParameter("@code_1", SqlDbType.NVarChar,50) ,            	
                           
            new SqlParameter("@code_2", SqlDbType.NVarChar,50) ,            	
                           
            new SqlParameter("@code_3", SqlDbType.NVarChar,50) ,            	
                           
            new SqlParameter("@CName", SqlDbType.VarChar,50) ,            	
                        	
                        	
                           
            new SqlParameter("@StandCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@ZFStandCode", SqlDbType.VarChar,50) ,            	
                           
            new SqlParameter("@UseFlag", SqlDbType.Int,4) ,            	
                           
            new SqlParameter("@EName", SqlDbType.VarChar,40) ,            	
                           
            new SqlParameter("@ShortName", SqlDbType.VarChar,40) ,            	
                           
            new SqlParameter("@ShortCode", SqlDbType.VarChar,40) ,            	
                           
            new SqlParameter("@DiagMethod", SqlDbType.VarChar,100) ,            	
                           
            new SqlParameter("@Unit", SqlDbType.VarChar,40)             	
              
            };




            if (model.IsCalc != null)
            {
                parameters[0].Value = model.IsCalc;
            }



            if (model.Visible != null)
            {
                parameters[1].Value = model.Visible;
            }



            if (model.DispOrder != null)
            {
                parameters[2].Value = model.DispOrder;
            }



            if (model.Prec != null)
            {
                parameters[3].Value = model.Prec;
            }



            if (model.IsProfile != null)
            {
                parameters[4].Value = model.IsProfile;
            }



            if (model.OrderNo != null)
            {
                parameters[5].Value = model.OrderNo;
            }



            if (model.StandardCode != null)
            {
                parameters[6].Value = model.StandardCode;
            }



            if (model.ItemDesc != null)
            {
                parameters[7].Value = model.ItemDesc;
            }



            if (model.FWorkLoad != null)
            {
                parameters[8].Value = model.FWorkLoad;
            }



            if (model.Secretgrade != null)
            {
                parameters[9].Value = model.Secretgrade;
            }



            if (model.LabCode != null)
            {
                parameters[10].Value = model.LabCode;
            }



            if (model.Cuegrade != null)
            {
                parameters[11].Value = model.Cuegrade;
            }



            if (model.IsDoctorItem != null)
            {
                parameters[12].Value = model.IsDoctorItem;
            }



            if (model.IschargeItem != null)
            {
                parameters[13].Value = model.IschargeItem;
            }



            if (model.HisDispOrder != null)
            {
                parameters[14].Value = model.HisDispOrder;
            }



            if (model.SuperGroupNo != null)
            {
                parameters[15].Value = model.SuperGroupNo;
            }



            if (model.Price != null)
            {
                parameters[16].Value = model.Price;
            }



            if (model.SpecialType != null)
            {
                parameters[17].Value = model.SpecialType;
            }



            if (model.SpecialSection != null)
            {
                parameters[18].Value = model.SpecialSection;
            }



            if (model.LowPrice != null)
            {
                parameters[19].Value = model.LowPrice;
            }



            if (model.SpecTypeNo != null)
            {
                parameters[20].Value = model.SpecTypeNo;
            }



            if (model.LabItemNo != null)
            {
                parameters[21].Value = model.LabItemNo;
            }



            if (model.IsCombiItem != null)
            {
                parameters[22].Value = model.IsCombiItem;
            }



            if (model.IsHistoryItem != null)
            {
                parameters[23].Value = model.IsHistoryItem;
            }



            if (model.IsNurseItem != null)
            {
                parameters[24].Value = model.IsNurseItem;
            }



            if (model.DefaultSType != null)
            {
                parameters[25].Value = model.DefaultSType;
            }



            if (model.SpecName != null)
            {
                parameters[26].Value = model.SpecName;
            }



            if (model.zdy1 != null)
            {
                parameters[27].Value = model.zdy1;
            }



            if (model.zdy2 != null)
            {
                parameters[28].Value = model.zdy2;
            }



            if (model.code_1 != null)
            {
                parameters[29].Value = model.code_1;
            }



            if (model.code_2 != null)
            {
                parameters[30].Value = model.code_2;
            }



            if (model.code_3 != null)
            {
                parameters[31].Value = model.code_3;
            }



            if (model.CName != null)
            {
                parameters[32].Value = model.CName;
            }







            if (model.StandCode != null)
            {
                parameters[33].Value = model.StandCode;
            }



            if (model.ZFStandCode != null)
            {
                parameters[34].Value = model.ZFStandCode;
            }



            if (model.UseFlag != null)
            {
                parameters[35].Value = model.UseFlag;
            }



            if (model.EName != null)
            {
                parameters[36].Value = model.EName;
            }



            if (model.ShortName != null)
            {
                parameters[37].Value = model.ShortName;
            }



            if (model.ShortCode != null)
            {
                parameters[38].Value = model.ShortCode;
            }



            if (model.DiagMethod != null)
            {
                parameters[39].Value = model.DiagMethod;
            }



            if (model.Unit != null)
            {
                parameters[40].Value = model.Unit;
            }


            if (idb.ExecuteNonQuery(strSql.ToString(), parameters) > 0)
            {
                return d_log.OperateLog("TestItem", "", "", DateTime.Now, 1);
            }
            else
                return -1;
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

            return idb.ExecuteNonQuery(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string LabCode, string LabItemNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from B_Lab_TestItem ");
            strSql.Append(" where LabCode=@LabCode and LabItemNo=@LabItemNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@LabCode", SqlDbType.VarChar,50),
					new SqlParameter("@LabItemNo", SqlDbType.VarChar,50)};
            parameters[0].Value = LabCode;
            parameters[1].Value = LabItemNo;


            return idb.ExecuteNonQuery(strSql.ToString(), parameters);
            
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.Lab_TestItem GetModel(string LabCode, string LabItemNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ItemID, IsCalc, Visible, DispOrder, Prec, IsProfile, OrderNo, StandardCode, ItemDesc, FWorkLoad, Secretgrade, LabCode, Cuegrade, IsDoctorItem, IschargeItem, HisDispOrder, SuperGroupNo, price, SpecialType, SpecialSection, LowPrice, SpecTypeNo, LabItemNo, IsCombiItem, IsHistoryItem, IsNurseItem, DefaultSType, SpecName, zdy1, zdy2, code_1, code_2, code_3, CName, DTimeStampe, AddTime, StandCode, ZFStandCode, UseFlag, EName, ShortName, ShortCode, DiagMethod, Unit  ");
            strSql.Append("  from B_Lab_TestItem ");
            strSql.Append(" where LabCode=@LabCode and LabItemNo=@LabItemNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@LabCode", SqlDbType.VarChar,50),
					new SqlParameter("@LabItemNo", SqlDbType.VarChar,50)};
            parameters[0].Value = LabCode;
            parameters[1].Value = LabItemNo;


            ZhiFang.Model.Lab_TestItem model = new ZhiFang.Model.Lab_TestItem();
            DataSet ds = idb.ExecuteDataSet(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ItemID"].ToString() != "")
                {
                    model.ItemID = int.Parse(ds.Tables[0].Rows[0]["ItemID"].ToString());
                }
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
                model.LabCode = ds.Tables[0].Rows[0]["LabCode"].ToString();
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
                if (ds.Tables[0].Rows[0]["HisDispOrder"].ToString() != "")
                {
                    model.HisDispOrder = int.Parse(ds.Tables[0].Rows[0]["HisDispOrder"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SuperGroupNo"].ToString() != "")
                {
                    model.SuperGroupNo = int.Parse(ds.Tables[0].Rows[0]["SuperGroupNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["price"].ToString() != "")
                {
                    model.Price = decimal.Parse(ds.Tables[0].Rows[0]["price"].ToString());
                }
                model.SpecialType = ds.Tables[0].Rows[0]["SpecialType"].ToString();
                model.SpecialSection = ds.Tables[0].Rows[0]["SpecialSection"].ToString();
                model.LowPrice = ds.Tables[0].Rows[0]["LowPrice"].ToString();
                if (ds.Tables[0].Rows[0]["SpecTypeNo"].ToString() != "")
                {
                    model.SpecTypeNo = int.Parse(ds.Tables[0].Rows[0]["SpecTypeNo"].ToString());
                }
                model.LabItemNo = ds.Tables[0].Rows[0]["LabItemNo"].ToString();
                if (ds.Tables[0].Rows[0]["IsCombiItem"].ToString() != "")
                {
                    model.IsCombiItem = int.Parse(ds.Tables[0].Rows[0]["IsCombiItem"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsHistoryItem"].ToString() != "")
                {
                    model.IsHistoryItem = int.Parse(ds.Tables[0].Rows[0]["IsHistoryItem"].ToString());
                }
                model.IsNurseItem = ds.Tables[0].Rows[0]["IsNurseItem"].ToString();
                if (ds.Tables[0].Rows[0]["DefaultSType"].ToString() != "")
                {
                    model.DefaultSType = int.Parse(ds.Tables[0].Rows[0]["DefaultSType"].ToString());
                }
                model.SpecName = ds.Tables[0].Rows[0]["SpecName"].ToString();
                model.zdy1 = ds.Tables[0].Rows[0]["zdy1"].ToString();
                model.zdy2 = ds.Tables[0].Rows[0]["zdy2"].ToString();
                model.code_1 = ds.Tables[0].Rows[0]["code_1"].ToString();
                model.code_2 = ds.Tables[0].Rows[0]["code_2"].ToString();
                model.code_3 = ds.Tables[0].Rows[0]["code_3"].ToString();
                model.CName = ds.Tables[0].Rows[0]["CName"].ToString();
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
                model.EName = ds.Tables[0].Rows[0]["EName"].ToString();
                model.ShortName = ds.Tables[0].Rows[0]["ShortName"].ToString();
                model.ShortCode = ds.Tables[0].Rows[0]["ShortCode"].ToString();
                model.DiagMethod = ds.Tables[0].Rows[0]["DiagMethod"].ToString();
                model.Unit = ds.Tables[0].Rows[0]["Unit"].ToString();

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
            strSql.Append("select '('+LabItemNo+')'+CName as ItemNoName,LabItemNo as ItemNo,* ");
            strSql.Append(" FROM B_Lab_TestItem ");
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
            strSql.Append(" '('+LabItemNo+')'+CName as ItemNoName,LabItemNo as ItemNo,* ");
            strSql.Append(" FROM B_Lab_TestItem ");
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
        public DataSet GetList(ZhiFang.Model.Lab_TestItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select '('+LabItemNo+')'+CName as ItemNoName,LabItemNo as ItemNo,* ");
            strSql.Append(" FROM B_Lab_TestItem where 1=1 ");
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

            if (model.IsProfile != 0)
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

            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
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

            if (model.HisDispOrder != null)
            {
                strSql.Append(" and HisDispOrder=" + model.HisDispOrder + " ");
            }

            if (model.SuperGroupNo != null)
            {
                strSql.Append(" and SuperGroupNo=" + model.SuperGroupNo + " ");
            }

            if (model.Price != null)
            {
                strSql.Append(" and price=" + model.Price + " ");
            }

            if (model.SpecialType != null)
            {
                strSql.Append(" and SpecialType='" + model.SpecialType + "' ");
            }

            if (model.SpecialSection != null)
            {
                strSql.Append(" and SpecialSection='" + model.SpecialSection + "' ");
            }

            if (model.LowPrice != null)
            {
                strSql.Append(" and LowPrice='" + model.LowPrice + "' ");
            }

            if (model.SpecTypeNo != null)
            {
                strSql.Append(" and SpecTypeNo=" + model.SpecTypeNo + " ");
            }

            if (model.LabItemNo != null)
            {
                strSql.Append(" and LabItemNo='" + model.LabItemNo + "' ");
            }

            if (model.IsCombiItem != null)
            {
                strSql.Append(" and IsCombiItem=" + model.IsCombiItem + " ");
            }

            if (model.IsHistoryItem != null)
            {
                strSql.Append(" and IsHistoryItem=" + model.IsHistoryItem + " ");
            }

            if (model.IsNurseItem != null)
            {
                strSql.Append(" and IsNurseItem='" + model.IsNurseItem + "' ");
            }

            if (model.DefaultSType != null)
            {
                strSql.Append(" and DefaultSType=" + model.DefaultSType + " ");
            }

            if (model.SpecName != null)
            {
                strSql.Append(" and SpecName='" + model.SpecName + "' ");
            }

            if (model.zdy1 != null)
            {
                strSql.Append(" and zdy1='" + model.zdy1 + "' ");
            }

            if (model.zdy2 != null)
            {
                strSql.Append(" and zdy2='" + model.zdy2 + "' ");
            }

            if (model.code_1 != null)
            {
                strSql.Append(" and code_1='" + model.code_1 + "' ");
            }

            if (model.code_2 != null)
            {
                strSql.Append(" and code_2='" + model.code_2 + "' ");
            }

            if (model.code_3 != null)
            {
                strSql.Append(" and code_3='" + model.code_3 + "' ");
            }

            if (model.CName != null)
            {
                strSql.Append(" and CName='" + model.CName + "' ");
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
            return idb.ExecuteDataSet(strSql.ToString());
        }
        public DataSet GetList(ZhiFang.Model.Lab_TestItem model, string flag)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select '('+LabItemNo+')'+CName as ItemNoName,LabItemNo as ItemNo,*  ");
            strSql.Append(" FROM B_Lab_TestItem where 1=1 ");
            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }
            if (flag == "0")
            {
                if (model.LabItemNo != null)
                {
                    strSql.Append(" and ( LabItemNo like '%" + model.LabItemNo + "%' ");
                }
                if (model.CName != null)
                {
                    strSql.Append(" or CName like '%" + model.CName + "%' ) ");
                }
            }
            else if (flag == "1")
            {//组套外项目
                strSql.Append(" and (isCombiItem=1 or IsDoctorItem=1 or IschargeItem=1) ");
                if (model.LabItemNo.Trim() != "")
                {
                    strSql.Append(" and LabItemNo<>'" + model.LabItemNo + "' and LabItemNo not in (select ItemNo from B_Lab_GroupItem where PItemNo='" + model.LabItemNo + "') ");
                }
                if (model.SearchKey.Trim() != "")
                {
                    strSql.Append(" and (labitemno like '%" + model.SearchKey + "%' or cname like '%" + model.SearchKey + "%' or ShortCode like '%" + model.SearchKey + "%' or ShortName like '%" + model.SearchKey + "%' ) ");
                }
                if (model.PItemNos.Trim() != "")
                {
                    strSql.Append(" and LabItemNo not in (" + model.PItemNos + ") ");
                }
            }
            else if (flag == "2")
            { //组套内项目
                strSql.Append(" and LabItemNo in (select ItemNo from B_Lab_GroupItem where PItemNo='" + model.LabItemNo + "') ");
            }
            else
            { }
            return idb.ExecuteDataSet(strSql.ToString());
        }
        
        /// <summary>
        /// 获取总记录的数量
        /// </summary>
        /// <returns></returns>
        public int GetTotalCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) FROM B_Lab_TestItem ");           
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
        public int GetTotalCount(ZhiFang.Model.Lab_TestItem model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            strSql.Append("select count(*) FROM B_Lab_TestItem where 1=1 ");
            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }
            if (model != null)
            {
                if (model.LabItemNo != null)
                {
                    strWhere.Append(" and ( LabItemNo like '%" + model.LabItemNo + "%' ");
                }
                if (model.CName != null)
                {
                    if (strWhere.Length==0)
                        strWhere.Append(" and ( CName like '%" + model.CName + "%' ");
                    else
                        strWhere.Append(" or CName like '%" + model.CName + "%' ");
                }
                if (model.ShortName != null)
                {
                    if (strWhere.Length == 0)
                        strWhere.Append(" and ( ShortName like '%" + model.ShortName + "%' ");
                    else
                        strWhere.Append(" or ShortName like '%" + model.ShortName + "%' ");
                }
                if (model.ShortCode != null)
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
            //ZhiFang.Common.Log.Log.Info(strSql.ToString());
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
        /// <param name="nowPageNum">第几页</param>
        /// <param name="nowPageSize">每页多少行</param>
        /// <param name="OrderBy">排序字段</param>
        /// <returns>DataSet</returns>
        public DataSet GetListByPage(ZhiFang.Model.Lab_TestItem model,int nowPageNum, int nowPageSize)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            if (model.LabItemNo != null)
            {
                strWhere.Append(" and ( LabItemNo like '%" + model.LabItemNo + "%' ");
            }
            if (model.CName != null)
            {
                if (strWhere.Length == 0)
                    strWhere.Append(" and ( CName like '%" + model.CName + "%' ");
                else
                    strWhere.Append(" or CName like '%" + model.CName + "%' ");
            }
            if (model.EName != null)
            {
                if (strWhere.Length == 0)
                    strWhere.Append(" and ( EName like '%" + model.EName + "%' ");
                else
                    strWhere.Append(" or EName like '%" + model.EName + "%' ");
            }
            if (model.ShortName != null)
            {
                if (strWhere.Length == 0)
                    strWhere.Append(" and ( ShortName like '%" + model.ShortName + "%' ");
                else
                    strWhere.Append(" or ShortName like '%" + model.ShortName + "%' ");
            }
            if (model.ShortCode != null)
            {
                if (strWhere.Length == 0)
                    strWhere.Append(" and ( ShortCode like '%" + model.ShortCode + "%' ");
                else
                    strWhere.Append(" or ShortCode like '%" + model.ShortCode + "%' ");
            }
            if (strWhere.Length != 0)
                strWhere.Append(" ) ");

            strSql.Append("select top " + nowPageSize + " '('+LabItemNo+')'+CName as ItemNoName,LabItemNo as ItemNo,* from B_Lab_TestItem where 1=1  "); 
            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }
            strSql.Append(" " + strWhere.ToString() + " and ItemID not in ");
            strSql.Append("(select top " + (nowPageSize * nowPageNum) + " ItemID from B_Lab_TestItem where 1=1  ");
            if (model.LabCode != null)
            {
                strSql.Append(" and LabCode='" + model.LabCode + "' ");
            }
            strSql.Append(" " + strWhere.ToString() + " order by " + model.OrderField + ") order by " + model.OrderField + "  ");
            return idb.ExecuteDataSet(strSql.ToString());
        }
        public bool Exists(string LabCode, string LabItemNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from B_Lab_TestItem ");
            strSql.Append(" where LabCode ='" + LabCode + "' and LabItemNo ='" +  LabItemNo + "' ");
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
            strSql.Append("select count(1) from B_Lab_TestItem where 1=1 ");
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

        #region IDataBase<Lab_TestItem> 成员

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(int Top, Model.Lab_TestItem model, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM B_Lab_TestItem ");


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

            if (model.LabCode != null)
            {

                strSql.Append(" and LabCode='" + model.LabCode + "' ");
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

            if (model.HisDispOrder != null)
            {
                strSql.Append(" and HisDispOrder=" + model.HisDispOrder + " ");
            }

            if (model.SuperGroupNo != null)
            {
                strSql.Append(" and SuperGroupNo=" + model.SuperGroupNo + " ");
            }

            if (model.Price != null)
            {
                strSql.Append(" and price=" + model.Price + " ");
            }

            if (model.SpecialType != null)
            {

                strSql.Append(" and SpecialType='" + model.SpecialType + "' ");
            }

            if (model.SpecialSection != null)
            {

                strSql.Append(" and SpecialSection='" + model.SpecialSection + "' ");
            }

            if (model.LowPrice != null)
            {

                strSql.Append(" and LowPrice='" + model.LowPrice + "' ");
            }

            if (model.SpecTypeNo != null)
            {
                strSql.Append(" and SpecTypeNo=" + model.SpecTypeNo + " ");
            }

            if (model.LabItemNo != null)
            {

                strSql.Append(" and LabItemNo='" + model.LabItemNo + "' ");
            }

            if (model.IsCombiItem != null)
            {
                strSql.Append(" and IsCombiItem=" + model.IsCombiItem + " ");
            }

            if (model.IsHistoryItem != null)
            {
                strSql.Append(" and IsHistoryItem=" + model.IsHistoryItem + " ");
            }

            if (model.IsNurseItem != null)
            {

                strSql.Append(" and IsNurseItem='" + model.IsNurseItem + "' ");
            }

            if (model.DefaultSType != null)
            {
                strSql.Append(" and DefaultSType=" + model.DefaultSType + " ");
            }

            if (model.SpecName != null)
            {

                strSql.Append(" and SpecName='" + model.SpecName + "' ");
            }

            if (model.zdy1 != null)
            {

                strSql.Append(" and zdy1='" + model.zdy1 + "' ");
            }

            if (model.zdy2 != null)
            {

                strSql.Append(" and zdy2='" + model.zdy2 + "' ");
            }

            if (model.code_1 != null)
            {

                strSql.Append(" and code_1='" + model.code_1 + "' ");
            }

            if (model.code_2 != null)
            {

                strSql.Append(" and code_2='" + model.code_2 + "' ");
            }

            if (model.code_3 != null)
            {

                strSql.Append(" and code_3='" + model.code_3 + "' ");
            }

            if (model.CName != null)
            {

                strSql.Append(" and CName='" + model.CName + "' ");
            }

            if (model.DTimeStampe != null)
            {

                strSql.Append(" and DTimeStampe='" + model.DTimeStampe + "' ");
            }

            if (model.AddTime != null)
            {

                strSql.Append(" and AddTime='" + model.AddTime + "' ");
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

            strSql.Append(" order by " + filedOrder);
            return idb.ExecuteDataSet(strSql.ToString());
        }

        public DataSet GetAllList()
        {
            return GetList("");
        }

        #endregion

        #region IDataBase<Lab_TestItem> 成员
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
                        if (this.Exists(ds.Tables[0].Rows[i]["LabCode"].ToString().Trim(),ds.Tables[0].Rows[i]["LabItemNo"].ToString().Trim()))
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
                strSql.Append("insert into B_Lab_TestItem (");
                strSql.Append("IsCalc,Visible,DispOrder,Prec,IsProfile,OrderNo,StandardCode,ItemDesc,FWorkLoad,Secretgrade,LabCode,Cuegrade,IsDoctorItem,IschargeItem,HisDispOrder,SuperGroupNo,price,SpecialType,SpecialSection,LowPrice,SpecTypeNo,LabItemNo,IsCombiItem,IsHistoryItem,IsNurseItem,DefaultSType,SpecName,zdy1,zdy2,code_1,code_2,code_3,CName,StandCode,ZFStandCode,UseFlag,EName,ShortName,ShortCode,DiagMethod,Unit");
                strSql.Append(") values (");
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
                if (dr.Table.Columns["LabCode"] != null && dr.Table.Columns["LabCode"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["LabCode"].ToString().Trim() + "', ");
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
                if (dr.Table.Columns["HisDispOrder"] != null && dr.Table.Columns["HisDispOrder"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["HisDispOrder"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["SuperGroupNo"] != null && dr.Table.Columns["SuperGroupNo"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["SuperGroupNo"].ToString().Trim() + "', ");
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
                if (dr.Table.Columns["SpecialType"] != null && dr.Table.Columns["SpecialType"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["SpecialType"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["SpecialSection"] != null && dr.Table.Columns["SpecialSection"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["SpecialSection"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["LowPrice"] != null && dr.Table.Columns["LowPrice"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["LowPrice"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["SpecTypeNo"] != null && dr.Table.Columns["SpecTypeNo"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["SpecTypeNo"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["LabItemNo"] != null && dr.Table.Columns["LabItemNo"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["LabItemNo"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["IsCombiItem"] != null && dr.Table.Columns["IsCombiItem"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["IsCombiItem"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["IsHistoryItem"] != null && dr.Table.Columns["IsHistoryItem"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["IsHistoryItem"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["IsNurseItem"] != null && dr.Table.Columns["IsNurseItem"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["IsNurseItem"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["DefaultSType"] != null && dr.Table.Columns["DefaultSType"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["DefaultSType"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["SpecName"] != null && dr.Table.Columns["SpecName"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["SpecName"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["zdy1"] != null && dr.Table.Columns["zdy1"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["zdy1"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["zdy2"] != null && dr.Table.Columns["zdy2"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["zdy2"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["code_1"] != null && dr.Table.Columns["code_1"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["code_1"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["code_2"] != null && dr.Table.Columns["code_2"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["code_2"].ToString().Trim() + "', ");
                }
                else
                {
                    strSql.Append(" null, ");
                }
                if (dr.Table.Columns["code_3"] != null && dr.Table.Columns["code_3"].ToString().Trim() != "")
                {
                    strSql.Append(" '" + dr["code_3"].ToString().Trim() + "', ");
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
                    strSql.Append(" '" + dr["Unit"].ToString().Trim() + "' ");
                }
                else
                {
                    strSql.Append(" null ");
                }
                strSql.Append(") ");
                return idb.ExecuteNonQuery(strSql.ToString());
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Digitlab8.B_Lab_TestItem.AddByDataRow 同步数据时异常：", ex);
                return 0;
            }
        }
        public int UpdateByDataRow(DataRow dr)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update B_Lab_TestItem set ");

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

                if (dr.Table.Columns["HisDispOrder"] != null && dr.Table.Columns["HisDispOrder"].ToString().Trim() != "")
                {
                    strSql.Append(" HisDispOrder = '" + dr["HisDispOrder"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["SuperGroupNo"] != null && dr.Table.Columns["SuperGroupNo"].ToString().Trim() != "")
                {
                    strSql.Append(" SuperGroupNo = '" + dr["SuperGroupNo"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["price"] != null && dr.Table.Columns["price"].ToString().Trim() != "")
                {
                    strSql.Append(" price = '" + dr["price"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["SpecialType"] != null && dr.Table.Columns["SpecialType"].ToString().Trim() != "")
                {
                    strSql.Append(" SpecialType = '" + dr["SpecialType"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["SpecialSection"] != null && dr.Table.Columns["SpecialSection"].ToString().Trim() != "")
                {
                    strSql.Append(" SpecialSection = '" + dr["SpecialSection"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["LowPrice"] != null && dr.Table.Columns["LowPrice"].ToString().Trim() != "")
                {
                    strSql.Append(" LowPrice = '" + dr["LowPrice"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["SpecTypeNo"] != null && dr.Table.Columns["SpecTypeNo"].ToString().Trim() != "")
                {
                    strSql.Append(" SpecTypeNo = '" + dr["SpecTypeNo"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["IsCombiItem"] != null && dr.Table.Columns["IsCombiItem"].ToString().Trim() != "")
                {
                    strSql.Append(" IsCombiItem = '" + dr["IsCombiItem"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["IsHistoryItem"] != null && dr.Table.Columns["IsHistoryItem"].ToString().Trim() != "")
                {
                    strSql.Append(" IsHistoryItem = '" + dr["IsHistoryItem"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["IsNurseItem"] != null && dr.Table.Columns["IsNurseItem"].ToString().Trim() != "")
                {
                    strSql.Append(" IsNurseItem = '" + dr["IsNurseItem"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["DefaultSType"] != null && dr.Table.Columns["DefaultSType"].ToString().Trim() != "")
                {
                    strSql.Append(" DefaultSType = '" + dr["DefaultSType"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["SpecName"] != null && dr.Table.Columns["SpecName"].ToString().Trim() != "")
                {
                    strSql.Append(" SpecName = '" + dr["SpecName"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["zdy1"] != null && dr.Table.Columns["zdy1"].ToString().Trim() != "")
                {
                    strSql.Append(" zdy1 = '" + dr["zdy1"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["zdy2"] != null && dr.Table.Columns["zdy2"].ToString().Trim() != "")
                {
                    strSql.Append(" zdy2 = '" + dr["zdy2"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["code_1"] != null && dr.Table.Columns["code_1"].ToString().Trim() != "")
                {
                    strSql.Append(" code_1 = '" + dr["code_1"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["code_2"] != null && dr.Table.Columns["code_2"].ToString().Trim() != "")
                {
                    strSql.Append(" code_2 = '" + dr["code_2"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["code_3"] != null && dr.Table.Columns["code_3"].ToString().Trim() != "")
                {
                    strSql.Append(" code_3 = '" + dr["code_3"].ToString().Trim() + "' , ");
                }

                if (dr.Table.Columns["CName"] != null && dr.Table.Columns["CName"].ToString().Trim() != "")
                {
                    strSql.Append(" CName = '" + dr["CName"].ToString().Trim() + "' , ");
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
                    strSql.Append(" Unit = '" + dr["Unit"].ToString().Trim() + "'  ");
                }

                int n = strSql.ToString().LastIndexOf(",");
                strSql.Remove(n, 1);
                strSql.Append(" where LabCode='" + dr["LabCode"].ToString().Trim() + "' and LabItemNo='" + dr["LabItemNo"].ToString().Trim() + "' ");

                return idb.ExecuteNonQuery(strSql.ToString());
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.DAL.MsSql.Digitlab8.B_Lab_TestItem .UpdateByDataRow同步数据时异常：", ex);
                return 0;
            }
        }
        
        #endregion


        public DataSet GetLabTestItemByItemNo(string labCode, string ItemNo)
        {
            throw new NotImplementedException();
        }

        public DataSet GetListByPage(Lab_TestItem model, int nowPageNum, int nowPageSize, string sort, string order)
        {
            throw new NotImplementedException();
        }
    }
}

